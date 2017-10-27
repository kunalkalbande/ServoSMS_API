<%@ Page language="c#" Inherits="Servosms.Module.Reports.SalesBook_PrintPreview" CodeFile="SalesBook_PrintPreview.aspx.cs" %>
<%@ Import namespace="DBOperations"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="System.Data.SqlClient"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Sales Book Report Print Preview</title> <!--
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
  </HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="278" width="778">
				<TR>
					<TH style="HEIGHT: 4px" align="center">
						<U><font color=#CE4848>Sales Book 
      Report From
							<%=Session["From_Date"].ToString()%>
							                                   To
							<%=Session["To_Date"].ToString()%></font>
						</U>
					</TH>
				</TR>
				<tr>
					<td vAlign="top" align="center">
						<TABLE id="Table1" cellSpacing="0" border="1">
							<TR>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Customer<br>
										ID </font></STRONG>
								</TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Customer Name</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Place</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Customer<br>
										Category</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Invoice<br>
										No.</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Invoice<br>
										Date</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Under Salesman</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Package<br>
										Type</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Product Name</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Quantity<br>
										in<br>
										Lit's</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Price</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Discount<br>
										(if any)</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Invoice Amount</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Credit<br>
										Days</font></STRONG></TD>
								<TD align="center" bgcolor=#CE4848><STRONG><font color=#ffffff>Due Date</font></STRONG></TD>
							</TR>
							<%
								DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
								 SqlDataReader SqlDtr = null;
								// string order_by = Session["Order_By"].ToString();
								 dbobj.SelectQuery("select * from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Session["From_Date"].ToString())+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Session["To_Date"].ToString())+"' order by invoice_no",ref SqlDtr);
								 while(SqlDtr.Read())
								 {								    
								%>
							<TR>
								<TD bgcolor=#fff7e7><font color=#8c4510><%=SqlDtr["Cust_ID"].ToString()%></font></TD>
								<TD bgcolor=#fff7e7><font color=#8c4510><%=SqlDtr["Cust_Name"].ToString().Equals("")?"&nbsp":SqlDtr["Cust_Name"].ToString()%></font></TD>
								<TD bgcolor=#fff7e7><font color=#8c4510><%=SqlDtr["City"].ToString().Equals("")?"&nbsp":SqlDtr["City"].ToString()%></font></TD>
								<TD bgcolor=#fff7e7><font color=#8c4510><%=SqlDtr["Cust_Type"].ToString().Equals("")?"&nbsp":SqlDtr["Cust_Type"].ToString()%></font></TD>
								<TD bgcolor=#fff7e7><font color=#8c4510><%=SqlDtr["Invoice_no"].ToString().Equals("")?"&nbsp":SqlDtr["Invoice_No"].ToString()%></font></TD>
								<TD bgcolor=#fff7e7><font color=#8c4510><%=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["Invoice_date"].ToString().Equals("")?"&nbsp":SqlDtr["Invoice_date"].ToString()))%></font></TD>
								<TD bgcolor=#fff7e7><font color=#8c4510><%=SqlDtr["Under_Salesman"].ToString().Equals("")?"&nbsp":SqlDtr["Under_Salesman"].ToString()%></font></TD>
								<TD bgcolor=#fff7e7><font color=#8c4510><%=SqlDtr["Pack_type"].ToString().Equals("")?"&nbsp":SqlDtr["Pack_Type"].ToString()%></font></TD>
								<TD bgcolor=#fff7e7><font color=#8c4510><%=SqlDtr["Prod_name"].ToString().Equals("")?"&nbsp":SqlDtr["Prod_Name"].ToString()%></font></TD>
								
								<TD bgcolor=#fff7e7><font color=#8c4510><%=Multiply(SqlDtr["quant"].ToString().Trim()+"X" +SqlDtr["Pack_Type"].ToString().Trim()).ToString()%></font></TD>
								<!--TD bgcolor=#fff7e7><font color=#8c4510><%=SqlDtr["Qty"].ToString().Equals("")?"&nbsp":SqlDtr["Qty"].ToString()%></font></TD-->
								<TD align="right" bgcolor=#fff7e7><font color=#8c4510><%=GenUtil.strNumericFormat(SqlDtr["Rate"].ToString().Equals("")?"&nbsp":SqlDtr["Rate"].ToString())%></font></TD>
								<TD align="center" bgcolor=#fff7e7><font color=#8c4510><%=SqlDtr["Discount"].ToString().Equals("")?"&nbsp":SqlDtr["Discount"].ToString()%></font></TD>
								<TD bgcolor=#fff7e7><font color=#8c4510><%=Multiply1(SqlDtr["Invoice_No"].ToString().Trim()).ToString()%></font></TD>
								<!--TD bgcolor=#fff7e7><font color=#8c4510><%=SqlDtr["Promo_Scheme"].ToString().Equals("")?"&nbsp":SqlDtr["Promo_Scheme"].ToString()%></font></TD-->
								<TD bgcolor=#fff7e7><font color=#8c4510><%=SqlDtr["Cr_Days"].ToString().Equals("")?"&nbsp":SqlDtr["Cr_Days"].ToString()%></font></TD>
								<TD bgcolor=#fff7e7><font color=#8c4510><%=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["Due_Date"].ToString().Equals("")?"&nbsp":SqlDtr["Due_Date"].ToString()))%></font></TD>
							</TR>
							<%
							    }
							    SqlDtr.Close();
							
							%>
							<tr>
							<td bgcolor=#fff7e7><font color=#8c4510>Total:</font></td>
							<td bgcolor=#fff7e7><font color=#8c4510></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510><%=Cache["totltr"].ToString()%></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510><%=GenUtil.strNumericFormat(Cache["am"].ToString())%></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510></font></td>
							<td bgcolor=#fff7e7><font color=#8c4510></font></td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
				scrolling="no" height="189"></iframe>
		</form>
	</body>
</HTML>
