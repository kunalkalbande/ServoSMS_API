<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Import namespace="Servosms.Sysitem.Classes" %>
<%@ Import namespace="DBOperations" %>
<%@ Import namespace="System.Data.SqlClient" %>
<%@ Import namespace="RMG" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.districtWiseReport" CodeFile="districtWiseReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Master List</title> <!--
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
			<table height="290" cellSpacing="0" cellPadding="0" width="775" align="center" border="0">
				<tr vAlign="top">
					<th height="20">
						<font color="#ce4848">Master List</font>
						<hr>
					</th>
				</tr>
				<tr>
					<td align="center"><asp:button id="btnPrint" Text="Print" Width="70" Runat="server" 
							 onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="btnExcel" Text="Excel" Width="70" Runat="server" 
							 onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<tr>
					<td>
						<%
						try
						{
							DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
							DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
							DBUtil dbobj2=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
							SqlDataReader rdr=null,rdr1=null,SqlDtr=null;
							dbobj.SelectQuery("select distinct state from customer order by state",ref rdr);
							if(rdr.HasRows)
							{
								//int Count=1;
								//dbobj2.SelectQuery("select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else cust_type end from customer",ref rdr1);
								//while(rdr1.Read()){Count++;}
								//double[] srrCount = new double[Count];
								while(rdr.Read())
								{
									%>
						<table width="600" align="center" border="0">
							<%dbobj1.SelectQuery("select * from customer where state='"+rdr["State"].ToString()+"' order by cust_type",ref SqlDtr);
										if(SqlDtr.HasRows)
										{
											%>
							<tr>
								<td colSpan="4">&nbsp;&nbsp;&nbsp;&nbsp;<b>District :<%=rdr["State"].ToString()%></b></td>
							</tr>
							<tr>
								<td>
									<table borderColor="#deba84" cellSpacing="0" cellPadding="0" width="100%" bgColor="#fff7e7"
										border="1">
										<tr bgColor="#ce4848" height="22">
											<td align="center" width="15%">&nbsp;<b><font color="white">District</font></b></td>
											<td align="center" width="50%">&nbsp;<b><font color="white">Firm Name</font></b></td>
											<td align="center" width="20%">&nbsp;<b><font color="white">Place</font></b></td>
											<td align="center" width="15%">&nbsp;<b><font color="white">Category</font></b></td>
										</tr>
										<%while(SqlDtr.Read())
													{
														%>
										<tr>
											<td>&nbsp;<font color="#8c4510"><%=SqlDtr["State"].ToString()%></font></td>
											<td>&nbsp;<font color="#8c4510"><%=SqlDtr["Cust_Name"].ToString()%></font></td>
											<td>&nbsp;<font color="#8c4510"><%=SqlDtr["City"].ToString()%></font></td>
											<td>&nbsp;<font color="#8c4510"><%=SqlDtr["Cust_Type"].ToString()%></font></td>
										</tr>
										<%
													}
													%>
										<%
													dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' group by cust_type)",ref rdr1);
													while(rdr1.Read())
													{
														%>
										<tr bgColor="#ce4848">
											<td colSpan="1">&nbsp;</td>
											<td>&nbsp;<font color="white"><b>Total&nbsp;<%=rdr1.GetValue(0).ToString()%></b></font></td>
											<td colSpan="2">&nbsp;<font color="white"><b><%=rdr1.GetValue(1).ToString()%></b></font></td>
										</tr>
										<%
													}
													rdr1.Close();
													%>
									</table>
								</td>
							</tr>
							<%
									}
									SqlDtr.Close();%>
						</table>
						<%
								}
							}
							else
							{
								MessageBox.Show("Data Not Available");
								return;
							}
						%>
					</td>
				</tr>
				<tr>
					<td>
						<table borderColor="#deba84" cellSpacing="0" cellPadding="0" width="77%" align="center"
							bgColor="#fff7e7" border="1">
							<%
							ArrayList arrType = new ArrayList();
							ArrayList arrState = new ArrayList();
							dbobj2.SelectQuery("select distinct state from customer order by state",ref rdr1);
							if(rdr1.HasRows)
							{
								%>
							<tr bgColor="#ce4848" height="22">
								<td align="center"><b><font color="white">Type / Distict</font></b></td>
								<%
									while(rdr1.Read())
									{
										arrState.Add(rdr1.GetValue(0).ToString());%>
								<td align="center"><b><font color="white"><%=rdr1["State"].ToString()%></font></b></td>
								<%		
									}
									%>
								<td align="center"><b><font color="white">Total</font></b></td>
							</tr>
							<%
							}
							rdr1.Close();
							double Total=0;
							
							double[] Counter=new double[140];
							int count=0;
							dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') group by cust_type)",ref rdr1);
							while(rdr1.Read()){arrType.Add(rdr1.GetValue(0).ToString());}rdr1.Close();
							for(int i=0;i<arrType.Count;i++)
							{
								Total=0;
								%>
							<tr>
								<td>&nbsp;<b><font color="#8c4510">Total&nbsp;<%=arrType[i].ToString()%></font></b></FONT></td>
								<%
									for(int j=0;j<arrState.Count;j++)
									{
										dbobj2.SelectQuery("select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"'",ref rdr1);
										if(rdr1.Read())
										{
											%>
								<td align="center"><font color="#8c4510"><%=rdr1.GetValue(0).ToString()%></font><%Total+=double.Parse(rdr1.GetValue(0).ToString());%></td>
								<%
											Counter[count++]=double.Parse(rdr1.GetValue(0).ToString());
										}
									}
									%>
								<td align="center"><b><font color="#8c4510"><%=Total.ToString()%></font></b></td>
							</tr>
							<%
								
							}
							
							double[] Tot_Counter=new double[10];
							double Tot_=0;
							for(int i=0;i<140;)
							{
								Tot_+=Counter[i];
								Tot_Counter[0]+=Counter[i++];
								Tot_+=Counter[i];
								Tot_Counter[1]+=Counter[i++];
								Tot_+=Counter[i];
								Tot_Counter[2]+=Counter[i++];
								Tot_+=Counter[i];
								Tot_Counter[3]+=Counter[i++];
								Tot_+=Counter[i];
								Tot_Counter[4]+=Counter[i++];
								Tot_+=Counter[i];
								Tot_Counter[5]+=Counter[i++];
								Tot_+=Counter[i];
								Tot_Counter[6]+=Counter[i++];
								Tot_+=Counter[i];
								Tot_Counter[7]+=Counter[i++];
								Tot_+=Counter[i];
								Tot_Counter[8]+=Counter[i++];
								Tot_+=Counter[i];
								Tot_Counter[9]+=Counter[i++];
								
								//MessageBox.Show(Tot_Counter[0].ToString()+":"+Tot_Counter[1].ToString()+":"+Tot_Counter[2].ToString()+":"+Tot_Counter[3].ToString()+":"+Tot_Counter[4].ToString()+":"+Tot_Counter[5].ToString()+":"+Tot_Counter[6].ToString()+":"+Tot_Counter[7].ToString());
								
							}					
							%>
							<tr bgColor="#ce4848">
								<td align="center"><b><font color="white">ToTal</font></b></td>
								<td align="center"><b><font color="white"><%=Tot_Counter[0]%></font></b></td>
								<td align="center"><b><font color="white"><%=Tot_Counter[1]%></font></b></td>
								<td align="center"><b><font color="white"><%=Tot_Counter[2]%></font></b></td>
								<td align="center"><b><font color="white"><%=Tot_Counter[3]%></font></b></td>
								<td align="center"><b><font color="white"><%=Tot_Counter[4]%></font></b></td>
								<td align="center"><b><font color="white"><%=Tot_Counter[5]%></font></b></td>
								<td align="center"><b><font color="white"><%=Tot_Counter[6]%></font></b></td>
								<td align="center"><b><font color="white"><%=Tot_Counter[7]%></font></b></td>
								<td align="center"><b><font color="white"><%=Tot_Counter[8]%></font></b></td>
								<td align="center"><b><font color="white"><%=Tot_Counter[9]%></font></b></td>
								<td align="center"><b><font color="white"><%=Tot_%></font></b></td>
							</tr>
						</table>
					</td>
				</tr>
				<%
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				%>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
