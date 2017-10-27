<%@ Import namespace="System.Data.SqlClient"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="DBOperations"%>
<%@ Page language="c#" Inherits="Servosms.Module.Master.CustDataMining_PrintPreview" CodeFile="CustDataMining_PrintPreview.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Customer Data Mining Print Preview</title> <!--
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
		
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table style="WIDTH: 778px; HEIGHT: 180px" height="270" width="778">
				<TBODY>
					<TR>
						<TH style="COLOR: #003366; HEIGHT: 20px">
							<U><font color=#CE4848>Customer Data Mining</font></U>&nbsp;&nbsp;
						</TH>
					</TR>
					<TR>
						<TD vAlign="top" align="center">
							<TABLE id="Table1" cellSpacing="0" border="1">
								<TR>
									<TD style="HEIGHT: 32px" vAlign="middle" align="center" rowSpan="2"><STRONG>Customer 
											Name</STRONG></TD>
									<TD style="HEIGHT: 32px" vAlign="middle" align="center" rowSpan="2"><STRONG>Type</STRONG></TD>
									<TD style="HEIGHT: 32px" vAlign="middle" align="center" rowSpan="2"><STRONG>Address</STRONG></TD>
									<TD style="HEIGHT: 32px" vAlign="middle" align="center" rowSpan="2"><STRONG>City</STRONG></TD>
									<TD style="HEIGHT: 32px" vAlign="middle" align="center" rowSpan="2"><STRONG>State</STRONG></TD>
									<TD style="HEIGHT: 32px" vAlign="middle" align="center" rowSpan="2"><STRONG>Country</STRONG></TD>
									<TD vAlign="middle" align="center" colSpan="3"><STRONG>Contact No&nbsp;</STRONG></TD>
									<TD vAlign="middle" align="center" rowSpan="2"><STRONG>EMail</STRONG></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 16px" vAlign="middle" align="center"><STRONG>Office</STRONG></TD>
									<TD vAlign="middle" align="center"><STRONG>Residence</STRONG></TD>
									<TD vAlign="middle" align="center"><STRONG>Mobile</STRONG></TD>
								</TR>
								<%
								DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
								 SqlDataReader SqlDtr = null;
								 string order_by = Session["Order_By"].ToString();
								 dbobj.SelectQuery("Select * from Customer order by "+order_by,ref SqlDtr);
								 while(SqlDtr.Read())
								 {								    
								%>
								<TR>
									<TD><%=SqlDtr["Cust_Name"].ToString()%></TD>
									<TD><%=SqlDtr["Cust_Type"].ToString().Equals("")?"&nbsp":SqlDtr["Cust_Type"].ToString()%></TD>
									<TD><%=SqlDtr["Address"].ToString().Equals("")?"&nbsp":SqlDtr["Address"].ToString()%></TD>
									<TD><%=SqlDtr["City"].ToString().Equals("")?"&nbsp":SqlDtr["City"].ToString()%></TD>
									<TD><%=SqlDtr["State"].ToString().Equals("")?"&nbsp":SqlDtr["State"].ToString()%></TD>
									<TD><%=SqlDtr["Country"].ToString().Equals("")?"&nbsp":SqlDtr["Country"].ToString()%></TD>
									<TD><%=SqlDtr["Tel_Off"].ToString().Equals("0")?"&nbsp":SqlDtr["Tel_Off"].ToString()%></TD>
									<TD><%=SqlDtr["Tel_Res"].ToString().Equals("0")?"&nbsp":SqlDtr["tel_Res"].ToString()%></TD>
									<TD><%=SqlDtr["Mobile"].ToString().Equals("0")?"&nbsp":SqlDtr["Mobile"].ToString()%></TD>
									<TD><%=SqlDtr["Email"].ToString().Equals("")?"&nbsp":SqlDtr["Email"].ToString()%></TD>
								</TR>
								<%
								}
								SqlDtr.Close();
								%>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD align="right"><A href="javascript:window.print()"></A></TD>
					</TR>
				</TBODY>
			</table>
			</TD></TR></TBODY></TABLE><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174" scrolling="no" height="189">
			</iframe>
		</form>
		</FORM>
		<script language="C#" runat="server">
		private void Page_Load(object sender, System.EventArgs e)
		{
			string uid="";
			
			try
			{
				
		 uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
			CreateLogFiles.ErrorLog("Form:CustDataMining_printpreview,Method:Page_load  userid "+ uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;					
			}
			
			
		}
		</script>
	</body>
</HTML>
