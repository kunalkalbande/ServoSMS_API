<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.inventory.schemediscountentry" CodeFile="SchemeDiscountentry.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: SchemeDiscountentry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
			
		function getChild(t,type,sch)
		{
			if(t.checked==true)
			{
				
				//if(prd.value!="Type" && qty.value!="")
				//{
					//var Result="";
					if(sch!=null)
					{
						childWin=window.open("groupchild.aspx?chk="+t.name+":"+type.value+":0", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=no,width=205,height=326");
					}
					else
					{
						childWin=window.open("groupchild.aspx?chk="+t.name+":"+type.value+":"+document.Form1.dropschid.value, "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=no,width=205,height=326");
					}
					
				//}
				//else
				//{
					//alert("Please Select The Prod & Fill The Qty");
					//t.checked=false;
				//}
			}
		}
		</script>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 101; LEFT: 136px; POSITION: absolute; TOP: 16px" runat="server"
				Visible="False" Width="8px"></asp:textbox>
			<table height="200" cellSpacing="0" cellPadding="0" width="778" align="center">
				<TR>
					<TH align="center">
						<font color="#ce4848">Scheme&nbsp;Discount Entry&nbsp;</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE cellSpacing="5" cellPadding="5">
							<TBODY>
								<TR>
									<TD colSpan="3" style="HEIGHT: 22px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										Scheme ID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="lblschid" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove"
											CssClass="fontstyle" ></asp:textbox><asp:dropdownlist id="dropschid" runat="server" Width="296px" CssClass="dropdownlist" AutoPostBack="True" onselectedindexchanged="dropschid_SelectedIndexChanged"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;
										<asp:button id="btschid" runat="server" Width="20px" Text="..." CausesValidation="False" 
											 onclick="btschid_Click"></asp:button></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="3">&nbsp;Scheme Type&nbsp;<FONT color="red">*&nbsp;&nbsp;</FONT>&nbsp;&nbsp;<asp:dropdownlist id="DropShiftID" runat="server" Width="165px" CssClass="dropdownlist" AutoPostBack="True" onselectedindexchanged="DropShiftID_SelectedIndexChanged">
											<asp:ListItem Value="Select">Select</asp:ListItem>
											<asp:ListItem Value="Primary(Free Scheme)">Primary(Free Scheme)</asp:ListItem>
											<asp:ListItem Value="Primary(LTR&% Scheme)">Primary(LTR&% Scheme)</asp:ListItem>
											<asp:ListItem Value="Primary(LTR&% Addl Scheme)">Primary(LTR&% Addl Scheme)</asp:ListItem>
											<asp:ListItem Value="Secondry(Free Scheme)">Secondry(Free Scheme)</asp:ListItem>
											<asp:ListItem Value="Secondry(LTR Scheme)">Secondry(LTR Scheme)</asp:ListItem>
											<asp:ListItem Value="Secondry SP(LTRSP Scheme)">Secondry SP(LTRSP Scheme)</asp:ListItem>
										</asp:dropdownlist><asp:comparevalidator id="cvShiftName" runat="server" ValueToCompare="Select" Operator="NotEqual" ControlToValidate="DropShiftID"
											ErrorMessage="Please select the Scheme Type">*</asp:comparevalidator>
										Scheme Name&nbsp;&nbsp;&nbsp;
										<asp:textbox id="txtschname" runat="server" Width="204px" CssClass="dropdownlist" BorderStyle="Groove"></asp:textbox></TD>
								<tr>
									<td vAlign="top" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										Transaction Type&nbsp;
										<asp:dropdownlist id="DropType" CssClass="dropdownlist" Runat="server" onselectedindexchanged="DropType_SelectedIndexChanged">
											<asp:ListItem Value="Sales" Selected="True">Sales</asp:ListItem>
											<asp:ListItem Value="Purchase">Purchase</asp:ListItem>
										</asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										&nbsp;<asp:CheckBox ID="chkGroup" Runat="server" Text="Customer Group" onclick="getChild(this,document.Form1.DropType,document.Form1.lblschid)"></asp:CheckBox>&nbsp; 
										Date From&nbsp;
										<asp:requiredfieldvalidator id="rfvDateFrom" runat="server" ControlToValidate="txtDateFrom" ErrorMessage="Please Select From Date From the Calender">*</asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:textbox id="txtDateFrom" runat="server" Width="70px" CssClass="dropdownlist" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										Date To&nbsp;&nbsp;
										<asp:requiredfieldvalidator id="rfvDateTo" runat="server" ControlToValidate="txtDateTo" ErrorMessage="Please Select To Date From the Calender">*</asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:textbox id="txtDateTo" runat="server" Width="70px" CssClass="dropdownlist" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></td>
								<TR>
								<TR>
									<TD align="center"><FONT color="#000066">Products Available</FONT></TD>
									<TD></TD>
									<TD align="center"><FONT color="#000066">Products Assigned <FON*</FONT></FONT></TD>
								</TR>
								<TR>
									<TD><asp:listbox id="ListEmpAvailable" runat="server" Width="350px" Font-Size="8pt" SelectionMode="Multiple"
											Height="160px" onselectedindexchanged="ListEmpAvailable_DoubleClick"></asp:listbox></TD>
									<TD>
										<P><asp:button id="btnIn" runat="server" Width="50px" Text=">" CausesValidation="False" 
												 Font-Bold="True" onclick="btnIn_Click"></asp:button></P>
										<P dir="ltr" align="justify"><asp:button id="btnout" runat="server" Width="50px" Text="<" CausesValidation="False" 
												 Font-Bold="True" onclick="buttonout_Click"></asp:button></P>
										<P><asp:button id="btn1" runat="server" Width="50px" Text=">>" CausesValidation="False" 
												 Height="25px" Font-Bold="True" onclick="btnOut_Click"></asp:button></P>
									</TD>
									<TD><asp:listbox id="ListEmpAssigned" runat="server" Width="350px" CssClass="Dropdownlist" SelectionMode="Multiple"
											Height="160px" onselectedindexchanged="ListEmpAssigned_SelectedIndexChanged"></asp:listbox></TD>
								</TR>
								<tr>
									<td colSpan="3"><asp:panel id="Panel1" Visible="False" Runat="server">
											<TABLE>
												<TR>
													<TD align="right" width="230">Scheme&nbsp;Discount</TD>
													<TD width="180">
														<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtrs" runat="server"
															Width="70px" CssClass="dropdownlist" BorderStyle="Groove"></asp:textbox>
														<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="can not be Blank" ControlToValidate="txtrs">*</asp:requiredfieldvalidator>&nbsp;
														<asp:dropdownlist id="DropDisType" CssClass="dropdownlist" Runat="server">
															<asp:ListItem Value="Rs">Rs</asp:ListItem>
															<asp:ListItem Value="%">%</asp:ListItem>
															<asp:ListItem Value="Unit">Unit</asp:ListItem>
														</asp:dropdownlist></TD>
												</TR>
											</TABLE>
										</asp:panel></td>
								</tr>
								<tr>
									<td colSpan="3"><asp:panel id="Panel2" Visible="False" Runat="server">
											<TABLE cellSpacing="0" cellPadding="0">
												<TR>
													<TD>&nbsp;&nbsp;&nbsp;FOC Products Name With Pack</TD>
													<TD width="80">On Every</TD>
													<TD align="center" width="50">Unit</TD>
													<TD align="center" width="60" colSpan="1">Free</TD>
													<TD align="center" width="60" colSpan="1">Pack Type</TD>
												</TR>
												<TR>
													<TD align="center" width="180" colSpan="1">
														<asp:dropdownlist id="dropfoc" runat="server" Width="358px" CssClass="dropdownlist" DataTextFormatString="Editable"></asp:dropdownlist></TD>
													<TD width="80">
														<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtevery" runat="server"
															Width="60px" CssClass="dropdownlist" BorderStyle="Groove"></asp:textbox></TD>
													<TD align="center" width="50" colSpan="1">
														<asp:dropdownlist id="dropUnit" runat="server" Width="50px" CssClass="dropdownlist" DataTextFormatString="Editable">
															<asp:ListItem Value="Nos.">Nos.</asp:ListItem>
															<asp:ListItem Value="Ltr.">Ltr.</asp:ListItem>
															<asp:ListItem Value="Kg.">Kg.</asp:ListItem>
														</asp:dropdownlist></TD>
													<TD width="80">
														<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfree" runat="server"
															Width="70px" CssClass="dropdownlist" BorderStyle="Groove"></asp:textbox></TD>
													<TD align="center" width="50" colSpan="1">
														<asp:dropdownlist id="DropPackType" runat="server" Width="60px" CssClass="dropdownlist" DataTextFormatString="Editable">
															<asp:ListItem Value="Singal">Singal</asp:ListItem>
															<asp:ListItem Value="Combo">Combo</asp:ListItem>
														</asp:dropdownlist></TD>
												</TR>
											</TABLE>
										</asp:panel></td>
								</tr>
								<TR>
									<TD align="center" colSpan="3"><asp:button id="btnSubmit" runat="server" Width="75px" Text="Submit" 
											 onclick="btnSubmit_Click"></asp:button><asp:button id="btnupdate" runat="server" Width="75px" Text="Update" 
											 onclick="btnupdate_Click"></asp:button></TD>
								</TR>
								<TR>
									<TD colSpan="3"><asp:validationsummary id="vsShiftAssignment" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></TD>
								</TR>
							</TBODY>
						</TABLE>
					</td>
				</tr>
			</table>
			</TD></TR></TBODY></TABLE><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174" scrolling="no"
				height="189"> </iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
