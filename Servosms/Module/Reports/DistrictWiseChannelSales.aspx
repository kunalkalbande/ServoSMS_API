<%@ Reference Control="~/HeaderFooter/Header1.ascx" %>
<%@ Reference Control="~/HeaderFooter/Header.ascx" %>
<%@ Import namespace="Servosms.Sysitem.Classes" %>
<%@ Import namespace="DBOperations" %>
<%@ Import namespace="System.Data.SqlClient" %>
<%@ Import namespace="RMG" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.DistrictWiseChannelSales" CodeFile="DistrictWiseChannelSales.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: DistrictWiseChannelSales</title> 
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
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center" border="0">
				<TR>
					<TH align="center" height="20" valign="top" colspan="2">
						<font color="#ce4848">District Wise Channel Sales</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td vAlign="top" align="right" width="65%">Date From&nbsp;&nbsp;
						<asp:textbox id="txtDateFrom" runat="server" ReadOnly="True" Width="80px" CssClass="fontstyle"
							BorderStyle="Groove"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
								border="0"></A> Date To&nbsp;&nbsp;
						<asp:textbox id="txtDateTo" runat="server" ReadOnly="True" Width="80px" CssClass="fontstyle"
							BorderStyle="Groove"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
								border="0"></A><asp:RadioButton ID=radDetails Runat=server GroupName=radio Text=Details></asp:RadioButton><asp:RadioButton ID="radSummerized" Runat=server GroupName=radio Text=Summerized Checked=True></asp:RadioButton>
					</td>
					<td vAlign="top" width="35%">&nbsp;&nbsp;<asp:button id="btnShow" runat="server" Width="75px" 
							 Text="View" onclick="btnShow_Click"></asp:button>&nbsp;<asp:button id="BtnPrint" Width="75px" 
							Text="Print  " Runat="server" onclick="BtnPrint_Click"></asp:button>&nbsp;<asp:button id="btnExcel" Width="75px" 
							Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<%if(View==1){%>
				<tr>
					<td align="center" colspan="2">
						<table borderColor="#deba84" cellSpacing="0" cellPadding="0" align="center" bgColor="#fff7e7"
							border="1">
							<%
				DBUtil dbobj2=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
				SqlDataReader rdr1=null;
				ArrayList arrType = new ArrayList();
				ArrayList arrState = new ArrayList();
				double[] arrTotal = null;
				//dbobj2.SelectQuery("select distinct state from customer order by state",ref rdr1);
				//dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') group by cust_type)",ref rdr1);
				if(radDetails.Checked)
					dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') group by substring(cust_type,1,2)) union (select customertypename from customertype where (substring(customertypename,1,2)!='Ro' and substring(customertypename,1,2)!='oe') group by customertypename)",ref rdr1);
				else
					dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' when substring(cust_type,1,2)='Ks' then 'KSK' when substring(cust_type,1,2)='N-' then 'N-KSK' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe' or substring(cust_type,1,2)='Ks' or substring(cust_type,1,2)='n-') group by substring(cust_type,1,2)) union (select customertypename from customertype where (substring(customertypename,1,2)!='Ro' and substring(customertypename,1,2)!='oe' and substring(customertypename,1,2)!='ks' and substring(customertypename,1,2)!='N-') group by customertypename)",ref rdr1);
				if(rdr1.HasRows){%>
							<tr bgColor="#ce4848">
								<td align="center" width="15%"><b><font color="white">Type / Distict</font></b></td>
								<%while(rdr1.Read()){arrType.Add(rdr1.GetValue(0).ToString());%>
								<td align="center" width="10%"><b><font color="white"><%=rdr1.GetValue(0).ToString()%></font></b></td>
								<%}%>
								<td align="center" width="15%"><b><font color="white">Total Sales</font></b></td>
								<td align="center" width="15%"><b><font color="white">Monthly Avr.</font></b></td>
							</tr>
							<%}rdr1.Close();
				arrTotal=new double[arrType.Count];
				double Total=0,GTotal=0,GAvrTotal=0;
				//dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') group by cust_type)",ref rdr1);
				dbobj2.SelectQuery("select distinct state from customer order by state",ref rdr1);
				while(rdr1.Read()){arrState.Add(rdr1.GetValue(0).ToString());}rdr1.Close();
				for(int i=0;i<arrState.Count;i++){Total=0;%>
							<tr>
								<td>&nbsp;<b><font color="#8c4510">&nbsp;<%=arrState[i].ToString()%></font></b></FONT></td>
								<%for(int j=0;j<arrType.Count;j++){
					//dbobj2.SelectQuery("select case when sum(net_amount) is null then '0' else sum(net_amount) end from sales_master sm,customer c where sm.cust_id=c.cust_id and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
					if(radDetails.Checked)
					{
						if(arrType[j].ToString().ToLower()=="ksk")
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type ='"+arrType[j].ToString()+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						else
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
					}
					else
					{
						if(arrType[j].ToString().ToLower()=="ksk")
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						if(arrType[j].ToString().ToLower()=="n-ksk")
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						else
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
					}
					if(rdr1.Read()){arrTotal[j]+=double.Parse(rdr1.GetValue(0).ToString());Total+=double.Parse(rdr1.GetValue(0).ToString());%>
								<td align="right"><font color="#8c4510"><%=rdr1.GetValue(0).ToString()%></font>&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<%}}%>
								<td align="right"><font color="#8c4510"><%=Total.ToString()%><%GTotal+=Total;%></font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right"><font color="#8c4510"><%=GenUtil.strNumericFormat(System.Convert.ToString(Total/double.Parse(Count.ToString())))%><%GAvrTotal+=Total/double.Parse(Count.ToString());%></font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
							</tr>
							<%}%>
							<tr bgColor="#ce4848">
								<td align="center"><font color="white"><b>Total</b></font></td>
								<%for(int k=0;k<arrTotal.Length;k++){%>
								<td align="right"><font color="white"><b><%=arrTotal[k].ToString()%></b></font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<%}%>
								<td align="right"><font color="white"><b><%=GTotal.ToString()%></b></font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td align="right"><font color="white"><b><%=GenUtil.strNumericFormat(GAvrTotal.ToString())%></b></font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<%}%>
				<asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary>
				<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
					name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
					scrolling="no" height="189"></iframe>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
