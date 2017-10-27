<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Import namespace="RMG"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="DBOperations" %>
<%@ Import namespace="System.Data.SqlClient"%> 
<%@ Page language="c#" Inherits="Servosms.Module.Reports.SSRWiseTargets" CodeFile="SSRWiseTargets.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML> 
	<HEAD>  
		<title>ServoSMS: SSRWiseTragets</title><!--
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
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<script language=javascript>
		
		function Total_Tar(t)
		{
			/*var Tot_Mon=document.Form1.temptotmonth.value
			var Tot_Emp=document.Form1.temptotemp.value
			var Tot_Count=Tot_Mon*Tot_Emp
			var Firval
			var Secval
			var Thval
			var Fouval
			var count=1
			var name=t.value
			alert(name)
			for(var i=0;i<Tot_Count;i++)
			{
				Firval="txtFirTar"+count;
				if(name==Firval)
				{
					
				}
				alert(Tar_Total[count])
				alert(document.Form1.txtTotTar1.value)
				
				count++
				Secval="txtSecTar"+count;
				if(name==Secval)
				{
					Tar_Total[count]+=eval(t.value)
				}
				count++
				Thval="txtThTar"+count;
				if(name==Thval)
				{
					Tar_Total[count]+=eval(t.value)
				}
				count++
				Fouval="txtFouTar"+count;
				if(name==Fouval)
				{
					Tar_Total[count]+=eval(t.value)
				}
				count++
			}*/
		}
	
		function enabltxt(t)
		{
			var Tot_Mon=document.Form1.temptotmonth.value
			var Tot_Emp=document.Form1.temptotemp.value
			var chk=t.name
			var chk=chk.substring(5,6)
			var val=Tot_Mon*4
			var val2=0
			var i=0
		    for(var j=1;j<Tot_Emp+1;j++)
		    {
				if(chk==j)
				{
					if(chk==1)
					{
						if(t.checked)
						{
							for(i=7;i<val+7;i++)
							{
								document.Form1.elements[i].disabled=false
							}
						}
						else
						{
							for(i=7;i<val+7;i++)
							{
								document.Form1.elements[i].disabled=true
							}
						}
					}
					else if(chk==2)
					{
						val2=(val*2)+8
						if(t.checked)
						{
							for(i=val2;i<val2+val;i++)
							{
								document.Form1.elements[i].disabled=false
							}
						}
						else
						{
							for(i=val2;i<val2+val;i++)
							{
								document.Form1.elements[i].disabled=true
							}
						}
					}
					else if(chk==3)
					{
						val2=(val*4)+9
						if(t.checked)
						{
							for(i=val2;i<val2+val;i++)
							{
								document.Form1.elements[i].disabled=false
							}
						}
						else
						{
							for(i=val2;i<val2+val;i++)
							{
								document.Form1.elements[i].disabled=true
							}
						}
					}
					else if(chk==4)
					{
						val2=(val*6)+10
						if(t.checked)
						{
							for(i=val2;i<val2+val;i++)
							{
								document.Form1.elements[i].disabled=false
							}
						}
						else
						{
							for(i=val2;i<val2+val;i++)
							{
								document.Form1.elements[i].disabled=true
							}
						}
					}
					else if(chk==5)
					{
						val2=(val*8)+11
						if(t.checked)
						{
							for(i=val2;i<val2+val;i++)
							{
								document.Form1.elements[i].disabled=false
							}
						}
						else
						{
							for(i=val2;i<val2+val;i++)
							{
								document.Form1.elements[i].disabled=true
							}
						}
					}
				}
		    }
		}
		
		function enabltxt1(t)
		{
			var Tot_Mon=document.Form1.temptotmonth.value
			var Tot_Emp=document.Form1.temptotemp.value
			var chk=t.name
			var chk=chk.substring(5,6)
			var val=Tot_Mon*4
			var val2=0
			var i=0
			if(chk==1)
					{
						if(t.checked)
						{
							for(i=7;i<val+7;i++)
							{
								document.Form1.elements[i].disabled=false
							}
						}
						else
						{
							for(i=7;i<val+7;i++)
							{
								document.Form1.elements[i].disabled=true
							}
						}
					}
		}
		
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center">
				<TBODY>
					<TR  vAlign="top" height="20">
						<TH align="center">
							<font color="#ce4848">SSR Wise Targets Report</font>
							<hr>
						</TH>
					</TR>
					<%
				try
				{
					DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
					InventoryClass obj=new InventoryClass ();
					InventoryClass obj1=new InventoryClass ();
					SqlDataReader SqlDtr,rdr=null;
					string sql="",sql2="";
					int Achiv=0,Tar=0,check=0;
					int Tot_Achiv=0,Tot_Tar=0,Tot_check=0,tot_mon=0,tot_emp=0;
					%>
					<tr valign=top>
						<td align="center"><table width=100%><tr><td align=left> Search By
							<asp:dropdownlist id="DropSSR" CssClass="fontstyle" Runat="server">
							<asp:ListItem Value="All">All</asp:ListItem>
							</asp:dropdownlist>&nbsp;From Year&nbsp;
							<asp:textbox id="txtfromdate" CssClass="fontstyle" Runat="server" Width="65px" BorderStyle="Groove" ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtfromdate);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle" border="0"></A>&nbsp;To Year&nbsp;
							<asp:textbox id="txttodate" CssClass="fontstyle" Runat="server" Width="65px" BorderStyle="Groove" ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txttodate);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"	align="absMiddle" border="0"></A>&nbsp;&nbsp;
							
							<asp:button id="btnView" Runat="server" Width="70" Text="View" Height="23"  onclick="btnView_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="btnExcel" Runat="server" Width="70" Text="Excel" Height="23" onclick="btnExcel_Click"></asp:button>
							<asp:button id="btnSave" Runat="server" Width="70" Text="Save" Height="23" onclick="btnSave_Click"></asp:button>
						</td></tr></table></td>
					</tr>
					<%
                        if(View==1)
                        {
                            int frmDay=0,frmMonth=0,frmYear=0,toDay=0,toMonth=0,toYear=0;
                            string todate="";
                            string fromdate="";
                            todate=txttodate.Text;
                            fromdate=txtfromdate.Text;
                            string[] Todt =todate.IndexOf("/")>0? todate.Split(new char[] {'/'},todate.Length): todate.Split(new char[] {'-'},todate.Length);
                            string[] Frmdt =fromdate.IndexOf("/")>0? fromdate.Split(new char[] {'/'},fromdate.Length):fromdate.Split(new char[] { '-' },fromdate.Length);
                            frmDay=System.Convert.ToInt32(Frmdt[0]);
                            frmMonth=System.Convert.ToInt32(Frmdt[1]);
                            frmYear=System.Convert.ToInt32(Frmdt[2]);
                            toDay=System.Convert.ToInt32(Todt[0]);
                            toMonth=System.Convert.ToInt32(Todt[1]);
                            toYear=System.Convert.ToInt32(Todt[2]);
                            string ssr_id="";
                            tot_mon=DateFrom.Length;
                            InventoryClass obj2=new InventoryClass ();
                            SqlDataReader rdr2=null;
                            if(DropSSR.SelectedIndex==0)
                            {
                                int Couter=0;
                                Tot_Achiv_I=0;
                                Tot_Achiv_II=0;
                                Tot_Achiv_III=0;
                                Tot_Achiv_IV=0;
							%>
							<tr valign=top>
								<td>
									<table width=150px cellspacing=1>
										<tr bgColor="#ce4848">
											<td width=150px><font color="white">&nbsp;&nbsp;&nbsp;&nbsp;Servo_Sales_Representative&nbsp;&nbsp;&nbsp;&nbsp;</font></td>
											<%
											for(int m=0;m<DateFrom.Length;m++)
											{ 
												%>
												<td colspan=4 align=center><font color="white"><%=GetMonthName(DateFrom[m].ToString())%></font></br>
													<table width=100% cellspacing=1 align=center>
														<tr align=center bgColor="#ce4848">
															<td align=center><font color="white">Ist</font></td>
															<td align=center><font color="white">IInd</font></td>
															<td align=center><font color="white">IIIrd</font></td>
															<td align=center><font color="white">Last</font></td>
														</tr>
													</table>
												</td>
												<%
											}
											%>
										</tr> 
										<%	
										sql="select emp_name,emp_id from employee where designation='Servo Sales Representative'";
										rdr=obj.GetRecordSet(sql);
										while(rdr.Read())
										{
											ssr_id=rdr.GetValue(1).ToString().Trim();
											%>
											<tr>
												<td><%=rdr.GetValue(0).ToString().Trim()%> Target</td>
												<%
												
												if(frmYear==toYear)
												{
													for(int p=frmMonth;p<=toMonth;p++)
													{
														%>
														<td colspan=4 align=center>
															<table width=100% cellspacing=0 align=center>
																<tr align=center>
																<%
																sql2="select target from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=1";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled onblur=Total_Tar(this) onkeypress="return GetOnlyNumbers(this, event);"  value=<%=rdr2.GetValue(0).ToString()%> Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%>  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled onblur=Total_Tar(this) onkeypress="return GetOnlyNumbers(this, event);" Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%> value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled onblur=Total_Tar(this) onkeypress="return GetOnlyNumbers(this, event);" Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%> value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																	<%
																}
																	rdr2.Close();
																sql2="select target from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=2";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled onkeypress="return GetOnlyNumbers(this, event);" value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled onkeypress="return GetOnlyNumbers(this, event);" value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																			<%
																		}
																	}
																}
																else
																{
																		%>
																		<td align=center><input disabled onkeypress="return GetOnlyNumbers(this, event);" value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																		<%
																}
																rdr2.Close();	
																sql2="select target from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=3";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled onkeypress="return GetOnlyNumbers(this, event);" value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled onkeypress="return GetOnlyNumbers(this, event);" value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled onkeypress="return GetOnlyNumbers(this, event);" value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																	<%
																}
																rdr2.Close();	
																
																sql2="select target from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=4";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																	<%
																}	
																rdr2.Close();
																%>
															</tr>
														</table>
													</td>
													<%
												}
											}
											else
											{
												for(int p=frmMonth;p<=12;p++)
												{
													%>
													<td colspan=4 align=center>
														<table width=100% cellspacing=0 align=center>
															<tr align=center>
																<%
																sql2="select target from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=1";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=<%=rdr2.GetValue(0).ToString()%> Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%>  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%> value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled onkeypress="return GetOnlyNumbers(this, event);" Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%> value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																	<%
																}
																rdr2.Close();
																sql2="select target from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=2";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																	<%
																}	
																rdr2.Close();	
													
																sql2="select target from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=3";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																	<%
																}	
																rdr2.Close();	
																
																sql2="select target from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=4";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																	<%
																}
																rdr2.Close();
																%>
															</tr>
														</table>
													</td>
													<%
												}
												
												for(int p=1;p<=toMonth;p++)
												{
													%>
													<td colspan=4 align=center>
														<table width=100% cellspacing=0 align=center>
															<tr align=center>
																<%
																sql2="select target from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+toYear+"and month="+p+" and week=1";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=<%=rdr2.GetValue(0).ToString()%> Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%>  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%> value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled onkeypress="return GetOnlyNumbers(this, event);" Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%> value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																	<%
																}	
																rdr2.Close();
																sql2="select target from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+toYear+"and month="+p+" and week=2";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled onkeypress="return GetOnlyNumbers(this, event);" value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled onkeypress="return GetOnlyNumbers(this, event);" value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																	<%
																}	
																	
																rdr2.Close();	
																
																sql2="select target from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+toYear+"and month="+p+" and week=3";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled onkeypress="return GetOnlyNumbers(this, event);" value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																	<%
																}	
																rdr2.Close();	
																
																sql2="select target from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+toYear+"and month="+p+" and week=4";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																			<%	
																		}
																		else
																		{
																			%>
																			<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input onkeypress="return GetOnlyNumbers(this, event);" disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																	<%
																}
																rdr2.Close();
																%>
															</tr>
														</table>
													</td>
													<%
												}
											}
											
											
											%>
												<td><input id=checkTar<%=check++%> onclick=enabltxt(this) name=check<%=check%> type=checkbox></td>
											</tr>
											<tr>
												<td><%=rdr.GetValue(0).ToString().Trim()%> Achivment</td>
													<%
													
													Couter=0;
													if(frmYear==toYear)
													{
														for(int p=frmMonth;p<=toMonth;p++)
														{
															//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
															sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
															rdr2=obj2.GetRecordSet(sql2);
															%>
															<td colspan=4 align=center>
																<table width=100% cellspacing=0 align=center>
																	<tr align=center>
																		<%
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> id=txtFirAchiv<%=Achiv++%> maxlength=8 Class="fontstyle" style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" name=txtFirAchiv<%=Achiv%> ></td>
																				<%
																				Tot_Achiv_I+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				Weeks_Total[Couter]+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																			}
																			else
																			{
																				%>
																				<td align=center><input value=0 disabled id=txtFirAchiv<%=Achiv++%> maxlength=8 Class="fontstyle" style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" name=txtFirAchiv<%=Achiv%> ></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		Couter++;
																		
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled  value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecAchiv<%=Achiv++%>" name=txtSecAchiv<%=Achiv%>></td>
																				<%
																				//Tot_Achiv_II+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				Weeks_Total[Couter]+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																			}
																			else
																			{
																				%>
																				<td align=center><input value=0 disabled style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecAchiv<%=Achiv++%>" name=txtSecAchiv<%=Achiv%>></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		Couter++;
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtThAchiv<%=Achiv++%>"  name=txtThAchiv<%=Achiv%> ></td>
																				<%
																				//Tot_Achiv_III+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				Weeks_Total[Couter]+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtThAchiv<%=Achiv++%>"  name=txtThAchiv<%=Achiv%> ></td>
																				<%
																			}
																		}
																		
																		rdr2.Close();
																		Couter++;
																		int day=DateTime.DaysInMonth(frmYear,p);
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8  id="txtfouAchiv<%=Achiv++%>" Class="fontstyle" name=txtfouTar<%=Achiv%> ></td>
																				<%
																				//Tot_Achiv_IV+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				Weeks_Total[Couter]+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8  id="txtfouAchiv<%=Achiv++%>" Class="fontstyle" name=txtfouTar<%=Achiv%> ></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		Couter++;
																		%>
																	</tr>
																</table>
															</td>
															<%
														}
													}
													else
													{
														for(int p=frmMonth;p<=12;p++)
														{
															//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
															sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
															rdr2=obj2.GetRecordSet(sql2);
															%>
															<td colspan=4 align=center>
																<table width=100% cellspacing=0 align=center>
																	<tr align=center>
																		<%
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> id=txtFirAchiv<%=Achiv++%> maxlength=8 Class="fontstyle" style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" name=txtFirAchiv<%=Achiv%> ></td>
																				<%
																				//Tot_Achiv_I+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				Weeks_Total[Couter]+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 id=txtFirAchiv<%=Achiv++%> maxlength=8 Class="fontstyle" style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" name=txtFirAchiv<%=Achiv%> ></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		Couter++;
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecAchiv<%=Achiv++%>" name=txtSecAchiv<%=Achiv%>></td>
																				<%
																				//Tot_Achiv_II+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				Weeks_Total[Couter]+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecAchiv<%=Achiv++%>" name=txtSecAchiv<%=Achiv%>></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		Couter++;
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtThAchiv<%=Achiv++%>"  name=txtThAchiv<%=Achiv%> ></td>
																				<%
																				//Tot_Achiv_III+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				Weeks_Total[Couter]+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtThAchiv<%=Achiv++%>"  name=txtThAchiv<%=Achiv%> ></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		Couter++;
																		int day=DateTime.DaysInMonth(frmYear,p);
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8  id="txtfouAchiv<%=Achiv++%>" Class="fontstyle" name=txtfouTar<%=Achiv%> ></td>
																				<%
																				//Tot_Achiv_IV+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				Weeks_Total[Couter]+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8  id="txtfouAchiv<%=Achiv++%>" Class="fontstyle" name=txtfouTar<%=Achiv%> ></td>
																				<%
																			}
																		}	
																		rdr2.Close();
																		Couter++;
																		%>
																	</tr>
																</table>
															</td>
															<%
														}
														
														for(int p=1;p<=toMonth;p++)
														{
															//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/1/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/7/"+toYear.ToString()+"'";
															sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/1/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/7/"+toYear.ToString()+"'";
															rdr2=obj2.GetRecordSet(sql2);
															%>
															<td colspan=4 align=center>
																<table width=100% cellspacing=0 align=center>
																	<tr align=center>
																		<%
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> id=txtFirAchiv<%=Achiv++%> maxlength=8 Class="fontstyle" style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" name=txtFirAchiv<%=Achiv%> ></td>
																				<%
																				//Tot_Achiv_I+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				Weeks_Total[Couter]+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 id=txtFirAchiv<%=Achiv++%> maxlength=8 Class="fontstyle" style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" name=txtFirAchiv<%=Achiv%> ></td>
																				<%
																			}
																		}	
																		rdr2.Close();
																		Couter++;
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/8/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/14/"+toYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/8/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/14/"+toYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecAchiv<%=Achiv++%>" name=txtSecAchiv<%=Achiv%>></td>
																				<%
																				//Tot_Achiv_II+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				Weeks_Total[Couter]+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																			}
																			else
																			{
																				%>
																				<td align=center><input value=0 disabled style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecAchiv<%=Achiv++%>" name=txtSecAchiv<%=Achiv%>></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		Couter++;
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/15/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/21/"+toYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/15/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/21/"+toYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtThAchiv<%=Achiv++%>"  name=txtThAchiv<%=Achiv%> ></td>
																				<%
																				//Tot_Achiv_III+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				Weeks_Total[Couter]+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtThAchiv<%=Achiv++%>"  name=txtThAchiv<%=Achiv%> ></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		Couter++;
																		int day=DateTime.DaysInMonth(toYear,p);
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/22/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/"+day.ToString()+"/"+toYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/22/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/"+day.ToString()+"/"+toYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8  id="txtfouAchiv<%=Achiv++%>" Class="fontstyle" name=txtfouTar<%=Achiv%> ></td>
																				<%
																				//Tot_Achiv_IV+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				Weeks_Total[Couter]+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
																				
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8  id="txtfouAchiv<%=Achiv++%>" Class="fontstyle" name=txtfouTar<%=Achiv%> ></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		Couter++;
																		%>
																	</tr>
																</table>
															</td>
															<%
														}
													}
													
													%>
												</tr>
												<%
												tot_emp++;
											}
											rdr.Close();
											%>
											<tr><td>Total Target</td>
											<%
											int count=0;
											for(int m=0;m<DateFrom.Length;m++)
											{
												%>
												<td colspan=4 align=center>
													<table width=100% cellspacing=0 align=center>
														<tr align=center>
															<td align=center><input disabled Class="fontstyle" value=0  maxlength=8 id=txtTotTar<%=count++%>  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtTotTar<%=count%> ></td>
															<td align=center><input disabled value=0  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtTotTar<%=count++%>" name=txtTotTar<%=count%>></td>
															<td align=center><input disabled value=0  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtTotTar<%=count++%>" Class="fontstyle" name=txtTotTar<%=count%> ></td>
															<td align=center><input disabled value=0  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtTotTar<%=count++%>" name=txtTotTar<%=count%> ></td>
														</tr>
													</table>
												</td>
												<%
												}
												%>
											</tr>
											<tr><td>Total Achivment</td>
											<%
											int count1=0;
											for(int m=0;m<DateFrom.Length;m++)
											{
												%>
												<td colspan=4 align=center>
													<table width=100% cellspacing=0 align=center>
														<tr align=center>
															<td align=center><input disabled Class="fontstyle" value=<%=Weeks_Total[count1]%>  maxlength=8 id=txtTotAch<%=count1++%>  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtTotAch<%=count1%> ></td>
															<td align=center><input disabled value=<%=Weeks_Total[count1]%>  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtTotAch<%=count1++%>" name=txtTotAch<%=count1%>></td>
															<td align=center><input disabled value=<%=Weeks_Total[count1]%>  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtTotAch<%=count1++%>" Class="fontstyle" name=txtTotAch<%=count1%> ></td>
															<td align=center><input disabled value=<%=Weeks_Total[count1]%>  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtTotAch<%=count1++%>" name=txtTotAch<%=count1%> ></td>
														</tr>
													</table>
												</td>
												<%
												}
												%>
											</tr>
										</table>
										<%
								//MessageBox.Show(Tar.ToString());		
						}
						else
						{
							string SSR=DropSSR.SelectedValue;
							SSR=SSR.Substring(0,SSR.IndexOf(":"));
						%>
							<tr valign=top><td>
							<table width=150px cellspacing=1>
								<tr bgColor="#ce4848">
									<td width=150px><font color="white">&nbsp;&nbsp;&nbsp;&nbsp;Servo_Sales_Representative&nbsp;&nbsp;&nbsp;&nbsp;</font></td>
									<%
									for(int m=0;m<DateFrom.Length;m++)
									{
										%>
										<td colspan=4 align=center><font color="white"><%=GetMonthName(DateFrom[m].ToString())%></font></br>
											<table width=100% cellspacing=1 align=center>
												<tr align=center bgColor="#ce4848">
													<td align=center><font color="white">Ist</font></td>
													<td align=center><font color="white">IInd</font></td>
													<td align=center><font color="white">IIIrd</font></td>
													<td align=center><font color="white">Last</font></td>
												</tr>
											</table>
										</td>
										<%
									}
									%>
								</tr>
								<tr>
									<td>Target</td>
									<%		
									
									if(frmYear==toYear)
									{
										for(int p=frmMonth;p<=toMonth;p++)
										{
											%>
											<td colspan=4 align=center>
															<table width=100% cellspacing=0 align=center>
																<tr align=center>
																<%
																sql2="select target from SSR_Wise_Targets where ssrid="+SSR+" and year="+frmYear+"and month="+p+" and week=1";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled value=<%=rdr2.GetValue(0).ToString()%> Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%>  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%> value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%> value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																	<%
																}
																	rdr2.Close();
																sql2="select target from SSR_Wise_Targets where ssrid="+SSR+" and year="+frmYear+"and month="+p+" and week=2";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																			<%
																		}
																	}
																}
																else
																{
																		%>
																		<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																		<%
																}
																rdr2.Close();	
																sql2="select target from SSR_Wise_Targets where ssrid="+SSR+" and year="+frmYear+"and month="+p+" and week=3";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																	<%
																}
																rdr2.Close();	
																
																sql2="select target from SSR_Wise_Targets where ssrid="+SSR+" and year="+frmYear+"and month="+p+" and week=4";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																	<%
																}	
																rdr2.Close();
																%>
															</tr>
														</table>
													</td>
													<%
												}
											}
											else
											{
												for(int p=frmMonth;p<=12;p++)
												{
													%>
													<td colspan=4 align=center>
														<table width=100% cellspacing=0 align=center>
															<tr align=center>
																<%
																sql2="select target from SSR_Wise_Targets where ssrid="+SSR+" and year="+frmYear+"and month="+p+" and week=1";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled value=<%=rdr2.GetValue(0).ToString()%> Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%>  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%> value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%> value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																	<%
																}
																rdr2.Close();
																sql2="select target from SSR_Wise_Targets where ssrid="+SSR+" and year="+frmYear+"and month="+p+" and week=2";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																	<%
																}	
																rdr2.Close();	
													
																sql2="select target from SSR_Wise_Targets where ssrid="+SSR+" and year="+frmYear+"and month="+p+" and week=3";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																	<%
																}	
																rdr2.Close();	
																
																sql2="select target from SSR_Wise_Targets where ssrid="+SSR+" and year="+frmYear+"and month="+p+" and week=4";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																	<%
																}
																rdr2.Close();
																%>
															</tr>
														</table>
													</td>
													<%
												}
												
												for(int p=1;p<=toMonth;p++)
												{
													%>
													<td colspan=4 align=center>
														<table width=100% cellspacing=0 align=center>
															<tr align=center>
																<%
																sql2="select target from SSR_Wise_Targets where ssrid="+SSR+" and year="+toYear+"and month="+p+" and week=1";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled value=<%=rdr2.GetValue(0).ToString()%> Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%>  style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%> value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled Class="fontstyle" maxlength=8 id=txtFirTar<%=Tar++%> value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" name=txtFirTar<%=Tar%> ></td>
																	<%
																}	
																rdr2.Close();
																sql2="select target from SSR_Wise_Targets where ssrid="+SSR+" and year="+toYear+"and month="+p+" and week=2";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecTar<%=Tar++%>" name=txtSecTar<%=Tar%>></td>
																	<%
																}	
																	
																rdr2.Close();	
																
																sql2="select target from SSR_Wise_Targets where ssrid="+SSR+" and year="+toYear+"and month="+p+" and week=3";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																			<%
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 id="txtThTar<%=Tar++%>" Class="fontstyle" name=txtThTar<%=Tar%> ></td>
																	<%
																}	
																rdr2.Close();	
																
																sql2="select target from SSR_Wise_Targets where ssrid="+SSR+" and year="+toYear+"and month="+p+" and week=4";
																rdr2=obj2.GetRecordSet(sql2);
																if(rdr2.HasRows)
																{
																	while(rdr2.Read())
																	{
																		if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																		{
																			%>
																			<td align=center><input disabled value=<%=rdr2.GetValue(0).ToString()%> style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																			<%	
																		}
																		else
																		{
																			%>
																			<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																			<%
																		}
																	}
																}
																else
																{
																	%>
																	<td align=center><input disabled value=0 style="border-style:Groove; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtfouTar<%=Tar++%>" name=txtfouTar<%=Tar%> ></td>
																	<%
																}
																rdr2.Close();
																%>
															</tr>
														</table>
													</td>
													<%
												}
											}
									
									%>
									<td><input id=checkTar<%=check++%> onclick=enabltxt1(this) name=check<%=check%> type=checkbox></td>
								</tr>
								<tr>
									<td>Achivment</td>
									<%
									if(frmYear==toYear)
									{
										for(int p=frmMonth;p<=toMonth;p++)
										{
											//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
											sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
											rdr2=obj2.GetRecordSet(sql2);
											%>
											<td colspan=4 align=center>
												<table width=100% cellspacing=0 align=center>
													<tr align=center>
													<%
													while(rdr2.Read())
													{
														if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
														{
															%>
															<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> id=txtFirAchiv<%=Achiv++%> maxlength=8 Class="fontstyle" style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" name=txtFirAchiv<%=Achiv%> ></td>
															<%
														}
														else
														{
															%>
															<td align=center><input disabled value=0 id=txtFirAchiv<%=Achiv++%> maxlength=8 Class="fontstyle" style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" name=txtFirAchiv<%=Achiv%> ></td>
															<%
														}
													}
													rdr2.Close();
													//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
													sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
													rdr2=obj2.GetRecordSet(sql2);
													while(rdr2.Read())
													{
														if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
														{
															%>
															<td align=center><input disabled  value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecAchiv<%=Achiv++%>" name=txtSecAchiv<%=Achiv%>></td>
															<%
														}
														else
														{
															%>
															<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecAchiv<%=Achiv++%>" name=txtSecAchiv<%=Achiv%>></td>
															<%
														}
													}
													rdr2.Close();
													//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
													sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
													rdr2=obj2.GetRecordSet(sql2);
													while(rdr2.Read())
													{
														if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
														{
															%>
															<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtThAchiv<%=Achiv++%>"  name=txtThAchiv<%=Achiv%> ></td>
															<%
														}
														else
														{
															%>
															<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtThAchiv<%=Achiv++%>"  name=txtThAchiv<%=Achiv%> ></td>
															<%
														}
													}
													rdr2.Close();
													int day=DateTime.DaysInMonth(frmYear,p);
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8  id="txtfouAchiv<%=Achiv++%>" Class="fontstyle" name=txtfouTar<%=Achiv%> ></td>
																				<%
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8  id="txtfouAchiv<%=Achiv++%>" Class="fontstyle" name=txtfouTar<%=Achiv%> ></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		%>
																	</tr>
																</table>
															</td>
															<%
														}
													}
													else
													{
														for(int p=frmMonth;p<=12;p++)
														{
															//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
															sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
															rdr2=obj2.GetRecordSet(sql2);
															%>
															<td colspan=4 align=center>
																<table width=100% cellspacing=0 align=center>
																	<tr align=center>
																		<%
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> id=txtFirAchiv<%=Achiv++%> maxlength=8 Class="fontstyle" style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" name=txtFirAchiv<%=Achiv%> ></td>
																				<%
																			}
																			else
																			{
																				%>
																				<td align=center><input value=0 disabled id=txtFirAchiv<%=Achiv++%> maxlength=8 Class="fontstyle" style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" name=txtFirAchiv<%=Achiv%> ></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecAchiv<%=Achiv++%>" name=txtSecAchiv<%=Achiv%>></td>
																				<%
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecAchiv<%=Achiv++%>" name=txtSecAchiv<%=Achiv%>></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtThAchiv<%=Achiv++%>"  name=txtThAchiv<%=Achiv%> ></td>
																				<%
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtThAchiv<%=Achiv++%>"  name=txtThAchiv<%=Achiv%> ></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		int day=DateTime.DaysInMonth(frmYear,p);
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8  id="txtfouAchiv<%=Achiv++%>" Class="fontstyle" name=txtfouTar<%=Achiv%> ></td>
																				<%
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8  id="txtfouAchiv<%=Achiv++%>" Class="fontstyle" name=txtfouTar<%=Achiv%> ></td>
																				<%
																			}
																		}	
																		rdr2.Close();
																		%>
																	</tr>
																</table>
															</td>
															<%
														}
														
														for(int p=1;p<=toMonth;p++)
														{
															//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/1/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/7/"+toYear.ToString()+"'";
															sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/1/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/7/"+toYear.ToString()+"'";
															rdr2=obj2.GetRecordSet(sql2);
															%>
															<td colspan=4 align=center>
																<table width=100% cellspacing=0 align=center>
																	<tr align=center>
																		<%
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> id=txtFirAchiv<%=Achiv++%> maxlength=8 Class="fontstyle" style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" name=txtFirAchiv<%=Achiv%> ></td>
																				<%
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 id=txtFirAchiv<%=Achiv++%> maxlength=8 Class="fontstyle" style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" name=txtFirAchiv<%=Achiv%> ></td>
																				<%
																			}
																		}	
																		rdr2.Close();
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/8/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/14/"+toYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/8/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/14/"+toYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecAchiv<%=Achiv++%>" name=txtSecAchiv<%=Achiv%>></td>
																				<%
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtSecAchiv<%=Achiv++%>" name=txtSecAchiv<%=Achiv%>></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/15/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/21/"+toYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/15/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/21/"+toYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtThAchiv<%=Achiv++%>"  name=txtThAchiv<%=Achiv%> ></td>
																				<%
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8 Class="fontstyle" id="txtThAchiv<%=Achiv++%>"  name=txtThAchiv<%=Achiv%> ></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		int day=DateTime.DaysInMonth(toYear,p);
																		//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/22/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/"+day.ToString()+"/"+toYear.ToString()+"'";
																		sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/22/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/"+day.ToString()+"/"+toYear.ToString()+"'";
																		rdr2=obj2.GetRecordSet(sql2);
																		while(rdr2.Read())
																		{
																			if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
																			{
																				%>
																				<td align=center><input disabled value=<%=Math.Round(double.Parse(rdr2.GetValue(0).ToString()))%> style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8  id="txtfouAchiv<%=Achiv++%>" Class="fontstyle" name=txtfouTar<%=Achiv%> ></td>
																				<%
																			}
																			else
																			{
																				%>
																				<td align=center><input disabled value=0 style="border-style:None; FONT-SIZE: 8pt; Width: 50px;" maxlength=8  id="txtfouAchiv<%=Achiv++%>" Class="fontstyle" name=txtfouTar<%=Achiv%> ></td>
																				<%
																			}
																		}
																		rdr2.Close();
																		%>
																	</tr>
																</table>
															</td>
															<%
														}
													}
									%>
								</tr>
							</table>
							<%
						}
									
									Tot_Tar=Tar;
									Tot_Achiv=Achiv;
									Tot_check=check;
									%>
									<input id=temptotmonth name=temptotmonth type=hidden value=<%=tot_mon%> style="WIDTH: 1px; HEIGHT: 1px">
									<input id=temptotemp name=temptotemp type=hidden value=<%=tot_emp%> style="WIDTH: 1px; HEIGHT: 1px">
								</td>
							</tr>
							<%
					}
				}
				catch(Exception ex)
				{
					//MessageBox.Show(ex.Message.ToString()+".aspx");
				}
					%>
				</TBODY>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174" scrolling="no"
				height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
		
	</body>
</HTML>
