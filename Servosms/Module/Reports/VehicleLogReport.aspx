<%@ Page language="c#" Inherits="Servosms.Module.Reports.VehicleLogReport" CodeFile="VehicleLogReport.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Vehicle Log Book Report</title> 
		<!--
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
		<script language="javascript">
		function checkValidity()
		{
		var index = document.Form1.Dropvehicleno.selectedIndex; 
		var ErrMessage = "";
		var flag = 0;
		if(index == 0)
		{
		   ErrMessage = "_Please Select Vehicle No.\n";
		   flag = 1;
		}
		if(document.Form1.txtDateFrom.value == "")
		 {
		 ErrMessage = ErrMessage + "_Please Enter Form Date\n";
		 flag = 1;
		 }
		 if(document.Form1.txtDateTo.value == "")
		 {
		 ErrMessage = ErrMessage + "_Please Enter To Date\n";
		 flag = 1;
		 }
		 if(document.Form1.txtDateFrom.value != "" && document.Form1.txtDateTo.value != "")
		 {
		    if(document.Form1.txtDateTo.value < document.Form1.txtDateFrom.value)
		    {
		        ErrMessage = ErrMessage + "_Date To must be greater than Date From";
		        flag = 1;
		        }		          
		 }
		 
		 if(flag == 1)
		 {
		   alert(ErrMessage);		 
		   return false;
		   }
		   else
		   {
		   return true;
		   }
		
		
		}
		</script>
</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Width="8px" Visible="False"></asp:textbox>
			<table height="299" width="778" align=center>
				<TBODY>
					<TR>
						<TH style="COLOR: #003366; HEIGHT: 20px">
							<font color="#ce4848">Vehicle Log Book Report</font> &nbsp;
							<hr>
						</TH>
					</TR>
					<TR valign=top>
						<TD align="center">
							<TABLE>
								<TR>
									<TD>Vehicle No.</TD>
									<td><asp:dropdownlist id="Dropvehicleno" runat="server" Width="170px" CssClass="fontstyle">
<asp:ListItem Value="Select">Select</asp:ListItem>
										</asp:dropdownlist></td>
									<TD>From Date</TD>
									<td><asp:textbox id="txtDateFrom" runat="server" Width="70px" ReadOnly="True" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
												border="0"></A></td>
									<TD>To Date</TD>
									<td><asp:textbox id="txtDateTo" runat="server" Width="70px" ReadOnly="True" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
												border="0"></A></td>
									<td><asp:button id="btnView" runat="server" Width="60px" 
								 Text="View" onclick="btnView_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="btnPrint" runat="server" Width="60px" 
								 Text="Print" CausesValidation="False" onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;
								<asp:button id=btnExcel runat="server" Width="60px" Text="Excel" CausesValidation="False" onclick="btnExcel_Click"></asp:button></td>
								</TR>
							</TABLE>
							</TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 150px" align="center">
							<%
	
	
	if(IsPostBack)
	
	 {
			     			      
			       %>
							<asp:datagrid id="grdLog" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" CellPadding="1"
								BorderStyle="None" AllowSorting="True" BorderWidth="0px" Font-Size="10pt" Font-Names="Verdana"
								CellSpacing="1" Height="4px" AutoGenerateColumns="False" HorizontalAlign="Center" onselectedindexchanged="grdLog_SelectedIndexChanged">
								<SelectedItemStyle Font-Size="2pt" Font-Bold="True" Height="4px" ForeColor="White" CssClass="DataGridItem"
									BackColor="#738A9C"></SelectedItemStyle>
								<EditItemStyle Height="4px"></EditItemStyle>
								<ItemStyle Font-Size="10pt" Height="4px" ForeColor="#8C4510" CssClass="DataGridItem" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Size="2px" Font-Names="Verdana" Font-Bold="True" Wrap="False" HorizontalAlign="Center"
									Height="4px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Fuel Inward">
										<HeaderStyle Font-Size="Large" Font-Names="Verdana" ForeColor="White"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderTemplate>
											<TABLE borderColor="#ccffff" width="100%" align="center">
												<TR>
													<TD vAlign="top" align="center" width="100%"><font color="white">Fuel Inward</font></TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Fuel_Used_Qty")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Vehicle Route">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<HeaderTemplate>
											<TABLE width="100%" align="center">
												<TR>
													<TD align="center" valign="top" width="100%"><font color="white">Vehicle Route</font></TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<%#getRoute(DataBinder.Eval(Container.DataItem,"Vehicle_Route").ToString())%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Engine Oil Used">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderTemplate>
											<TABLE width="100%" align="center">
												<TR>
													<TD align="center" valign="top" width="100%"><font color="white">Engine Oil Used</font>
													</TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Engine_Oil_Qty")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Gear Oil Used">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderTemplate>
											<TABLE width="100%" align="center">
												<TR>
													<TD align="center" valign="top" width="100%"><font color="white">Gear Oil Used</font></TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Gear_Oil_Qty")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Grease Used">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderTemplate>
											<TABLE width="100%" align="center">
												<TR>
													<TD align="center" valign="top" width="100%"><font color="white">Grease Used</font></TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Grease_Qty")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Brake Oil Used">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderTemplate>
											<TABLE width="100%" align="center">
												<TR>
													<TD align="center" valign="top" width="100%"><font color="white">Brake Oil Used</font></TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Brake_Oil_Qty")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Coolent Used">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderTemplate>
											<TABLE width="100%" align="center">
												<TR>
													<TD align="center" valign="top" width="100%"><font color="white">Coolent Used</font></TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Coolent_Qty")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Trans. Oil Used">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderTemplate>
											<TABLE width="100%" align="center">
												<TR>
													<TD align="center" valign="top" width="100%"><font color="white">Trans. Oil Used</font></TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Trans_Oil_Qty")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Expenses (In Rs.)">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderTemplate>
											<TABLE width="100%" align="center">
												<TR>
													<TD align="center" colSpan="4" width="100%"><font color="White">Expenses (In Rs.)</font>
													</TD>
												</TR>
												<!--tr>
													<td colspan="4" bordercolor="#ffffff"><hr style="COLOR: #ffffff; HEIGHT: 1px">
													</td>
												</tr-->
												<TR>
													<TD align="left" width="60"><font color="White">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Toll</font></TD>
													<TD align="left" width="60"><font color="White">&nbsp;&nbsp;&nbsp;&nbsp;Police</font></TD>
													<TD align="left" width="60"><font color="White">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Food</font></TD>
													<TD align="left" width="60"><font color="White">&nbsp;&nbsp;&nbsp;&nbsp;Misc.</font></TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<TABLE border="0" align="center">
												<TR>
													<TD align="Right" width="60"><font color="#8C4510"><%#DataBinder.Eval(Container.DataItem,"Toll").ToString()%></font></TD>
													<TD align="Right" width="60"><font color="#8C4510"><%#DataBinder.Eval(Container.DataItem,"Police").ToString()%></font></TD>
													<TD align="Right" width="60"><font color="#8C4510"><%#DataBinder.Eval(Container.DataItem,"Food").ToString()%></font></TD>
													<TD align="Right" width="60"><font color="#8C4510"><%#DataBinder.Eval(Container.DataItem,"Misc").ToString()%></font></TD>
												</TR>
											</TABLE>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Opening Meter Reading">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderTemplate>
											<TABLE width="100%" align="center">
												<TR>
													<TD align="center" valign="top" width="100%"><font color="white">Opening Meter Reading</font></TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Meter_Reading_Pre")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Closing Meter Reading">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderTemplate>
											<TABLE width="100%" align="center">
												<TR>
													<TD align="center" valign="top" width="100%"><font color="white">Closing Meter Reading</font></TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Meter_Reading_Cur")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="KM. Move">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderTemplate>
											<TABLE width="100%" align="center">
												<TR>
													<TD align="center" valign="top" width="100%"><font color="white">KM. Move</font></TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"KM")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Mileage">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderTemplate>
											<TABLE width="100%" align="center">
												<TR>
													<TD align="center" valign="top" width="100%"><font color="white">Mileage</font></TD>
												</TR>
											</TABLE>
										</HeaderTemplate>
										<ItemTemplate>
											<%#GenUtil.strNumericFormat(DataBinder.Eval(Container.DataItem,"Mileage").ToString())%>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD align="right"><A href="javascript:window.print()"></A></TD>
					</TR>
				</TBODY>
			</table>
			<%}			
			%>
			</TD></TR></TBODY></TABLE><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174" scrolling="no" height="189">
			</iframe>
		<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
