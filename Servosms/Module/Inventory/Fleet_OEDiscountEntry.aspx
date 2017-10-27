<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Inventory.Fleet_OEDiscountEntry" CodeFile="Fleet_OEDiscountEntry.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" ><!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Fleet/OEDiscountEntry</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="../../Sysitem/Styles.css" type=text/css rel=stylesheet >
<script language=javascript>
function GetCustTypeName(t)
{
	if(t.value=="OE")
	{
		var hidtext=document.Form1.tempCustOE.value;
		mainarr=hidtext.split(',')
		alert(mainarr.length)
		//for(var i=0;i<(mainarr.length-1);i++)
		//{
		//}
	}
}
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
<body onkeydown=change(event)>
<form id=Form1 method=post runat="server"><uc1:header id=Header1 runat="server"></uc1:header><asp:textbox id=TextBox1 style="Z-INDEX: 101; LEFT: 136px; POSITION: absolute; TOP: 16px" runat="server" Width="8px" Visible="False"></asp:textbox><input 
id=tempCustFleet 
style="Z-INDEX: 101; LEFT: 136px; WIDTH: 1px; POSITION: absolute; TOP: 16px" 
type=hidden name=tempCustFleet runat="server"> <input 
id=tempCustOE 
style="Z-INDEX: 101; LEFT: 142px; WIDTH: 1px; POSITION: absolute; TOP: 16px" 
type=hidden name=tempCustOE runat="server"> 
<table height=290 cellSpacing=0 cellPadding=0 width=778 align=center>
  <TR>
    <TH align=center><font color=#ce4848 
      >Fleet/OE Discount Entry</font> 
      <hr>
    </TH></TR>
  <tr>
    <td align=center>
      <TABLE cellSpacing=5 cellPadding=5>
        <TBODY>
        <TR>
          <TD colSpan=3>FO/OE ID&nbsp;&nbsp;&nbsp;<asp:label id=lblschid runat="server" Width="54px" ForeColor="#000099"></asp:label> 
<asp:dropdownlist id=dropschid runat="server" Width="250px" AutoPostBack="True" CssClass="dropdownlist" onselectedindexchanged="dropschid_SelectedIndexChanged"></asp:dropdownlist>&nbsp;<asp:comparevalidator id=cv1 Runat="server" Operator="NotEqual" ErrorMessage="Please Select The FOE ID" ControlToValidate="dropschid" ValueToCompare="Select">*</asp:comparevalidator>
              <asp:button id=btschid runat="server" Width="20px"  CausesValidation="False" Text="..." onclick="btschid_Click"></asp:button>&nbsp;&nbsp; 
            &nbsp;Description&nbsp; <asp:textbox id=txtschname runat="server" Width="208px" CssClass="dropdownlist" BorderStyle="Groove"></asp:textbox></TD></TR>
        <tr>
          <td vAlign=top colSpan=3 style="HEIGHT: 6px">Date From&nbsp; <asp:textbox id=txtDateFrom runat="server" Width="65px" CssClass="dropdownlist" BorderStyle="Groove" ReadOnly="True"></asp:textbox><A 
            onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;" 
            ><IMG class=PopcalTrigger alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align=absMiddle border=0 ></A>Date To&nbsp; <asp:textbox id=txtDateTo runat="server" Width="65px" CssClass="dropdownlist" BorderStyle="Groove" ReadOnly="True"></asp:textbox><A 
            onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;" 
            ><IMG class=PopcalTrigger alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align=absMiddle border=0 ></A>Type of 
            Disc&nbsp; <asp:dropdownlist id=DropShiftID runat="server" Width="60px" CssClass="dropdownlist" AutoPostBack="True" onselectedindexchanged="DropShiftID_SelectedIndexChanged">
<asp:ListItem Value="OE">OE</asp:ListItem>
<asp:ListItem Value="Fleet">Fleet</asp:ListItem>
										</asp:dropdownlist>&nbsp;Rate of Disc&nbsp;<asp:textbox id=txtrs runat="server" Width="70px" CssClass="dropdownlist" BorderStyle="Groove" MaxLength="6"></asp:textbox><asp:requiredfieldvalidator id=RequiredFieldValidator1 runat="server" ErrorMessage="Please Enter The Rate Of Dis." ControlToValidate="txtrs">*</asp:requiredfieldvalidator><asp:dropdownlist id=dropdiscount runat="server" Width="40px" CssClass="dropdownlist">
<asp:ListItem Value="Rs." Selected="True">Rs.</asp:ListItem>
<asp:ListItem Value="%">%</asp:ListItem>
										</asp:dropdownlist></td></tr>
        <TR>
          <TD align=center><FONT color=#000066 
            >Customer Available</FONT></TD>
          <TD><FONT color=#cc0033 
            ></FONT></TD>
          <TD align=center><FONT color=#000066 
            >&nbsp;<FONT color=#000066 
            >Customer </FONT>Assigned <FONT color=red 
            >*</FONT></FONT></TD></TR>

        
        <TR>
          <TD vAlign=top><asp:listbox id=ListEmpAvailable runat="server" Width="300px" CssClass="dropdownlist" SelectionMode="Multiple" Height="105px" onselectedindexchanged="ListEmpAvailable_DoubleClick"></asp:listbox></TD>
          <TD vAlign=top align=center><asp:button id=btnIn runat="server" Width="45px"  CausesValidation="False" Text=">"  Font-Bold="True" onclick="btnIn_Click"></asp:button><br 
            ><br><asp:button id=btnout runat="server" Width="45px"  CausesValidation="False" Text="<"  Font-Bold="True" onclick="buttonout_Click"></asp:button><br 
            ><br><asp:button id=btn1 runat="server" Width="45px"  CausesValidation="False" Text=">>"  Font-Bold="True" onclick="btnOut_Click"></asp:button></TD>
          <TD vAlign=top><asp:listbox id=ListEmpAssigned runat="server" Width="300px" CssClass="dropdownlist" SelectionMode="Multiple" Height="110px"></asp:listbox></TD></TR>
        <tr>
          <td colSpan=3 
            >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            If Fleet/Oe Discount for Specific Product <asp:checkbox id=Checkfoe runat="server" AutoPostBack="True" oncheckedchanged="Checkfoe_CheckedChanged"></asp:checkbox></td></tr>
        <tr>
          <td colSpan=3><asp:panel id=Panel1 
             runat="server" Visible="False" >
            <TABLE cellSpacing=5 cellPadding=5>
              <TR>
                <TD vAlign=top>
<asp:listbox id=Listprodavail runat="server" Width="300px" CssClass="dropdownlist" Height="110px" SelectionMode="Multiple"></asp:listbox></TD>
                <TD vAlign=top>
<asp:button id=btngo runat="server" Width="45px"  Text=">" CausesValidation="False" Font-Bold="True" onclick="btngo_Click"></asp:button><BR><BR>
<asp:button id=btnback runat="server" Width="45px"  Text="<" CausesValidation="False" Font-Bold="True" onclick="btnback_Click"></asp:button><BR><BR>
<asp:button id=btnall runat="server" Width="45px"  Text=">>" CausesValidation="False" Height="25px" Font-Bold="True" onclick="btnall_Click"></asp:button></TD>
                <TD vAlign=top> 
<asp:listbox id=Listprodassign runat="server" Width="300px" CssClass="dropdownlist" Height="110px" SelectionMode="Multiple"></asp:listbox></TD></TR></TABLE></asp:panel></td></tr>
        <TR>
          <TD align=center colSpan=3><asp:button id=btnsave runat="server" Width="75px"  Text="Save"  onclick="btnsave_Click"></asp:button>
              <asp:button id=btnSubmit runat="server" Width="75px" Visible="False"  Text="Submit" ></asp:button>
              <asp:button id=btnupdate runat="server" Width="75px"  Text="Update" onclick="btnupdate_Click"></asp:button></TD></TR>
        <TR>
          <TD colSpan=3><asp:validationsummary id=vsShiftAssignment runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></TD></TR></TBODY></TABLE></td></tr></table>
			<IFRAME id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
				scrolling="no" height="189"></IFRAME>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
