<!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
<%@ Import namespace="RMG" %>
<%@ Import namespace="System.Data.SqlClient" %>
<%@ Import namespace="Servosms.Sysitem.Classes" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.BankReconcillation" CodeFile="BankReconcillation.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
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
function Check(t,temp)
{
	var index=t.selectedIndex;
	var tempvalue=t.options[index].text;
	temp.value=tempvalue;
	
}
function Check1(t,temp)
{
	var index=t.selectedIndex;
	temp.value=index;
	
}
function EnaDes(t,Day,Mon,Year)
{
	if(t.checked)
	{
		Day.disabled=false;
		Mon.disabled=false;
		Year.disabled=false;
	}
	else
	{
		Day.disabled=true;
		Mon.disabled=true;
		Year.disabled=true;
	}
}
/*
function SetCheck()
{
	var i= "hello";
	alert(i);
}*/
  </script>
		<title>ServoSMS: BankReconcillation</title> 
		
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="../../Sysitem/Styles.css" type=text/css rel=stylesheet >
  </HEAD>
<body onload="SetCheck()" onkeydown="change(event)">
<form id=Form1 method=post runat="server"><uc1:header id=Header1 runat="server"></uc1:header>
<table height=290px width=778 align=center border=0>
  <tr>
    <TH align=center height=20><font color=#CE4848>Bank Reconcillation</FONT> 
      <hr>
    </TH></TR>
  <tr height=10>
    <td align=center>From Date&nbsp;<asp:textbox id="txtDateFrom" runat="server" Width="120px" ReadOnly="True" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox>
    <A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle" border="0"></A>&nbsp;
    To Date&nbsp;<asp:textbox id="txtDateTo" runat="server" Width="120px" ReadOnly="True" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox>
    <A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle" border="0"></A>
    Bank Name&nbsp;<asp:comparevalidator id="Comparevalidator1" runat="server" ValueToCompare="Select" Operator="NotEqual" ErrorMessage="Please Select Bank Name" ControlToValidate="DropBank">*</asp:comparevalidator>&nbsp;<asp:DropDownList ID=DropBank Runat=server Width="200" CssClass=fontstyle><asp:ListItem Value="Select">Select</asp:ListItem></asp:DropDownList>&nbsp;
<asp:button id="btnShow" runat="server" CausesValidation=True  Width="55" Text="View"></asp:button>&nbsp;
<asp:button id=btnView runat="server" CausesValidation=True  Width="80" Text="Reconciled" OnClick="Recon"></asp:button>&nbsp;
<asp:button id=btnPrint runat="server" CausesValidation=True  Width="55" Text="Print" onclick="btnPrint_Click"></asp:button>&nbsp;
<asp:button id=btnExcel runat="server" CausesValidation=True  Width="55" Text="Excel" onclick="btnExcel_Click"></asp:button></TD></TR>
  <tr>
    <td align=center>
    <%
    if(DropBank.SelectedIndex!=0)
    {
		InventoryClass obj = new InventoryClass();
		SqlDataReader rdr;
		//string str="(select lm.Ledger_Name Cust_Name,alt.Particulars Type,alt.Debit_Amount Debit,alt.Credit_Amount Credit,alt.Entry_Date Entry from accountsledgertable alt,ledger_master lm where (alt.particulars like 'payment%' or alt.particulars like 'receipt%') and alt.ledger_id=lm.ledger_id and alt.particulars not in(select vouchartype from reconcillation) and (lm.sub_grp_id='117' or lm.sub_grp_id='126' or lm.sub_grp_id='127'))union(select lm.Ledger_Name Cust_Name,alt.Particulars Type,alt.Debit_Amount Debit,alt.Credit_Amount Credit,alt.Entry_Date Entry from accountsledgertable alt,ledger_master lm where alt.particulars like 'contra%' and Credit_amount>0 and alt.ledger_id=lm.ledger_id and alt.particulars not in(select vouchartype from reconcillation) and (lm.sub_grp_id='117' or lm.sub_grp_id='126' or lm.sub_grp_id='127'))";
		//string str="(select lm.Ledger_Name Cust_Name,alt.Particulars Type,alt.Debit_Amount Debit,alt.Credit_Amount Credit,alt.Entry_Date Entry from accountsledgertable alt,ledger_master lm where lm.Ledger_Name='"+DropBank.SelectedItem.Text+"' and (alt.particulars like 'payment%' or alt.particulars like 'receipt%') and alt.ledger_id=lm.ledger_id and alt.particulars not in(select vouchartype from reconcillation))union(select lm.Ledger_Name Cust_Name,alt.Particulars Type,alt.Debit_Amount Debit,alt.Credit_Amount Credit,alt.Entry_Date Entry from accountsledgertable alt,ledger_master lm where lm.Ledger_Name='"+DropBank.SelectedItem.Text+"' and alt.particulars like 'contra%' and Credit_amount>0 and alt.ledger_id=lm.ledger_id and alt.particulars not in(select vouchartype from reconcillation))";
		string str="(select lm.Ledger_Name Cust_Name,alt.Particulars Type,alt.Debit_Amount Debit,alt.Credit_Amount Credit,alt.Entry_Date Entry from accountsledgertable alt,ledger_master lm where cast(floor(cast(entry_date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(entry_date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text)  +"' and lm.Ledger_Name='"+DropBank.SelectedItem.Text+"' and (alt.particulars like 'payment%' or alt.particulars like 'receipt%') and alt.ledger_id=lm.ledger_id and alt.particulars not in(select vouchartype from reconcillation))union(select lm.Ledger_Name Cust_Name,alt.Particulars Type,alt.Debit_Amount Debit,alt.Credit_Amount Credit,alt.Entry_Date Entry from accountsledgertable alt,ledger_master lm where cast(floor(cast(entry_date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(entry_date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text)  +"' and lm.Ledger_Name='"+DropBank.SelectedItem.Text+"' and alt.particulars like 'contra%' and Credit_amount>0 and alt.ledger_id=lm.ledger_id and alt.particulars not in(select vouchartype from reconcillation))";
		rdr = obj.GetRecordSet(str);
		int i=0;
		double Dr=0,Cr=0;
		if(rdr.HasRows)
		{
    %>
      <table border=1 cellspacing=0 bordercolor=#DEBA84 id=Table1>
        <tr bgcolor=#CE4848 height=22>
          <td align=center><font color=white><b>Bank Name</b></font></Td>
          <td align=center><font color=white><b>Vouchar Type</b></font></Td>
          <td align=center><font color=white><b>Debit Amount</b></font></Td>
          <td align=center><font color=white><b>Credit Amount</b></font></td>
          <td align=center><font color=white><b>Posted On</b></font></td>
          <td align=center><font color=white><b>Reconciled On</b></font></td>
          <td align=center><font color=white><b>Select</b></font></td>
        </TR>
      <%while(rdr.Read())
      {%>
		<tr>
		<td><%=rdr.GetValue(0).ToString()%><input type=hidden name=tempBankName<%=i%> value="<%=rdr.GetValue(0).ToString()%>"></td>
		<td><%=rdr.GetValue(1).ToString()%><input type=hidden name=tempVoucharType<%=i%> value="<%=rdr.GetValue(1).ToString()%>"></td>
		<td align=right><%=rdr.GetValue(2).ToString()%><input type=hidden name=tempDebit<%=i%> value="<%=rdr.GetValue(2).ToString()%>"></td>
		<td align=right><%=rdr.GetValue(3).ToString()%><input type=hidden name=tempCredit<%=i%> value="<%=rdr.GetValue(3).ToString()%>"></td>
		<td align=center><%=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr.GetValue(4).ToString()))%><input type=hidden name=tempPosted<%=i%> value="<%=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr.GetValue(4).ToString()))%>"></td>
		<%
		Dr+=double.Parse(rdr.GetValue(2).ToString());
		Cr+=double.Parse(rdr.GetValue(3).ToString());
		%>
		<td><input type=hidden name=tempDay<%=i%>><select name=DropDay<%=i%> onchange="Check(this,document.Form1.tempDay<%=i%>)">
		<option value="Select">Select</option>
		<%for(int j=1;j<=31;j++){%>
		<option value="<%=j%>"><%=j%></option>
		<%}%>
		</select><input type=hidden name=tempMonth<%=i%>>
		<select name=DropMonth<%=i%> onchange="Check1(this,document.Form1.tempMonth<%=i%>)">
		<option value="Select">Select</option>
		<option value="Jan">Jan</option>
		<option value="Feb">Feb</option>
		<option value="Mar">Mar</option>
		<option value="April">April</option>
		<option value="May">May</option>
		<option value="Jun">Jun</option>
		<option value="July">July</option>
		<option value="August">August</option>
		<option value="Sept">Sept</option>
		<option value="Oct">Oct</option>
		<option value="Nov">Nov</option>
		<option value="Dec">Dec</option>
		</select><input type=hidden name=tempYear<%=i%>>
		<select name=DropYear<%=i%> onchange="Check(this,document.Form1.tempYear<%=i%>)">
		<option value="Select">Select</option>
		<option value="2005">2005</option>
		<option value="2006">2006</option>
		<option value="2007">2007</option>
		<option value="2008">2008</option>
		<option value="2009">2009</option>
		<option value="2010">2010</option>
		<option value="2011">2011</option>
		<option value="2012">2012</option>
		<option value="2013">2013</option>
		<option value="2014">2014</option>
		<option value="2015">2015</option>
		</select>
		</td>
		<td align=center><input type=checkbox name=chk<%=i%> checked onclick="EnaDes(this,document.Form1.DropDay<%=i%>,document.Form1.DropMonth<%=i%>,document.Form1.DropYear<%=i%>)"></td>
		</tr>
      <%
      i++;
      }%>
      <tr bgcolor=#F7DFB5 height=20>
      <td align=center><font color=#8C4510><b>Total</b></font></td>
      <td>&nbsp;</td>
      <td align=right><font color=#8C4510><b><%=Dr.ToString()%></b></font></td>
      <td align=right><font color=#8C4510><b><%=Cr.ToString()%></b></font></td>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
      </tr>
      <tr>
      <td><input type=hidden name=Count value="<%=i%>"></td>
      </tr>
      </TABLE>
    <%}
    else
    {
		MessageBox.Show("Data Not Available");
    }
    }%>
    </TD>
  </TR>
  <tr><td><asp:ValidationSummary Runat=server ID=ValidationSummary1 ShowSummary=false ShowMessageBox=true></asp:ValidationSummary></td></tr>
</TABLE><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
				scrolling="no" height="189"></iframe>
<uc1:footer id=Footer1 runat="server"></uc1:footer>
</FORM>
	</body>
</HTML>
<script runat=server language=C#>
public void Recon(Object sender, EventArgs e)
{
	SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
	SqlCommand Cmd;
	int Count=System.Convert.ToInt32(Request.Params.Get("Count"));
	int Flag=0;
	for(int i=0;i<Count;i++)
	{
		if(Request.Params.Get("chk"+i)!=null)
		{
			if(Request.Params.Get("tempDay"+i)!="" && Request.Params.Get("tempMonth"+i)!="" && Request.Params.Get("tempYear"+i)!="")
			{
				SqlCon.Open();
				Cmd=new SqlCommand("insert into Reconcillation values('"+Request.Params.Get("tempBankName"+i)+"','"+Request.Params.Get("tempVoucharType"+i)+"','"+Request.Params.Get("tempDebit"+i)+"','"+Request.Params.Get("tempCredit"+i)+"','"+GenUtil.str2MMDDYYYY(Request.Params.Get("tempPosted"+i))+"','"+GenUtil.str2MMDDYYYY(Request.Params.Get("tempDay"+i)+"/"+Request.Params.Get("tempMonth"+i)+"/"+Request.Params.Get("tempYear"+i))+"','Yes')",SqlCon);
				//Cmd=new SqlCommand("insert into Reconcillation values('"+Request.Params.Get("tempBankName"+i)+"','"+Request.Params.Get("tempVoucharType"+i)+"','"+Request.Params.Get("tempDebit"+i)+"','"+Request.Params.Get("tempCredit"+i)+"','"+GenUtil.str2MMDDYYYY(Request.Params.Get("tempPosted"+i))+"','','Yes')",SqlCon);
				Cmd.ExecuteNonQuery();
				Cmd.Dispose();
				SqlCon.Close();
				Flag=1;
			}
		}
		/*
		else
		{
			SqlCon.Open();
			if(Request.Params.Get("tempDay"+i)!="" && Request.Params.Get("tempMonth"+i)!="" && Request.Params.Get("tempYear"+i)!="")
				Cmd=new SqlCommand("insert into Reconcillation values('"+Request.Params.Get("tempBankName"+i)+"','"+Request.Params.Get("tempVoucharType"+i)+"','"+Request.Params.Get("tempDebit"+i)+"','"+Request.Params.Get("tempCredit"+i)+"','"+GenUtil.str2MMDDYYYY(Request.Params.Get("tempPosted"+i))+"','"+GenUtil.str2MMDDYYYY(Request.Params.Get("tempDay"+i)+"/"+Request.Params.Get("tempMonth"+i)+"/"+Request.Params.Get("tempYear"+i))+"','No')",SqlCon);
			else
				Cmd=new SqlCommand("insert into Reconcillation values('"+Request.Params.Get("tempBankName"+i)+"','"+Request.Params.Get("tempVoucharType"+i)+"','"+Request.Params.Get("tempDebit"+i)+"','"+Request.Params.Get("tempCredit"+i)+"','"+GenUtil.str2MMDDYYYY(Request.Params.Get("tempPosted"+i))+"','','No')",SqlCon);
			Cmd.ExecuteNonQuery();
			Cmd.Dispose();
			SqlCon.Close();
		}
		*/
	}
	if(Flag==0)
		MessageBox.Show("Please Select Atleest One Record");
	else
		MessageBox.Show("Data Save Successfully");
	DropBank.SelectedIndex=0;
}
</script>
