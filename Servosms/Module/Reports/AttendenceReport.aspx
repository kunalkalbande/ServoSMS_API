<%@ Page language="c#" Inherits="Servosms.Module.Reports.AttendenceReport" CodeFile="AttendenceReport.aspx.cs" %>
<%@ Import namespace="RMG"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="DBOperations"%>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Attendence Report</title> 
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
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
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
			<table height="290" width="778" align="center">
				<tr vAlign="top" height="20">
					<th colSpan="5">
						<font color="#ce4848">Attendence Report</font>
						<hr>
					</th>
				</tr>
				<tr>
					<td vAlign="top">
						<table width="600" align="center">
							<tr align="center">
								<td align="center">Month</td>
								<td align="center"><asp:dropdownlist id="DropMonth" Runat="server" CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="January">January</asp:ListItem>
										<asp:ListItem Value="Fabruary">February</asp:ListItem>
										<asp:ListItem Value="March">March</asp:ListItem>
										<asp:ListItem Value="April">April</asp:ListItem>
										<asp:ListItem Value="May">May</asp:ListItem>
										<asp:ListItem Value="June">June</asp:ListItem>
										<asp:ListItem Value="July">July</asp:ListItem>
										<asp:ListItem Value="August">August</asp:ListItem>
										<asp:ListItem Value="September">September</asp:ListItem>
										<asp:ListItem Value="October">October</asp:ListItem>
										<asp:ListItem Value="November">November</asp:ListItem>
										<asp:ListItem Value="December">December</asp:ListItem>
									</asp:dropdownlist></td>
								<td align="center">Year</td>
								<td align="center"><asp:dropdownlist id="DropYear" Runat="server" CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2005">2005</asp:ListItem>
										<asp:ListItem Value="2006">2006</asp:ListItem>
										<asp:ListItem Value="2007">2007</asp:ListItem>
										<asp:ListItem Value="2008">2008</asp:ListItem>
										<asp:ListItem Value="2009">2009</asp:ListItem>
										<asp:ListItem Value="2010">2010</asp:ListItem>
										<asp:ListItem Value="2011">2011</asp:ListItem>
										<asp:ListItem Value="2012">2012</asp:ListItem>
										<asp:ListItem Value="2013">2013</asp:ListItem>
										<asp:ListItem Value="2014">2014</asp:ListItem>
										<asp:ListItem Value="2015">2015</asp:ListItem>
										<asp:ListItem Value="2016">2016</asp:ListItem>
										<asp:ListItem Value="2017">2017</asp:ListItem>
										<asp:ListItem Value="2018">2018</asp:ListItem>
										<asp:ListItem Value="2019">2019</asp:ListItem>
										<asp:ListItem Value="2020">2020</asp:ListItem>
										<asp:ListItem Value="2021">2021</asp:ListItem>
										<asp:ListItem Value="2022">2022</asp:ListItem>
										<asp:ListItem Value="2023">2023</asp:ListItem>
										<asp:ListItem Value="2024">2024</asp:ListItem>
										<asp:ListItem Value="2025">2025</asp:ListItem>
										<asp:ListItem Value="2026">2026</asp:ListItem>
										<asp:ListItem Value="2027">2027</asp:ListItem>
										<asp:ListItem Value="2028">2028</asp:ListItem>
										<asp:ListItem Value="2029">2029</asp:ListItem>
										<asp:ListItem Value="2030">2030</asp:ListItem>
									</asp:dropdownlist></td>
								<td align="center"><asp:button id="btnView" Runat="server" Width="70" 
										 Text="View" onclick="btnView_Click"></asp:button>&nbsp;&nbsp;
									<asp:button id="btnPrint" Runat="server" Width="70" 
										 Text="Print" onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;
									<asp:button id="btnExcel" Runat="server" Width="70" 
										 Text="Excel" onclick="btnExcel_Click"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<%
				try
				{
					if(DropMonth.SelectedIndex!=0 && DropYear.SelectedIndex!=0 && Flag==1)
					{
						string[] DayName = new string[Day];   //add by vikas 20.12.2012
						for(int i=0,j=1;i<Day;i++,j++)
						{
							DateTime D_Name=new DateTime(int.Parse(DropYear.SelectedItem.Text),DropMonth.SelectedIndex,j);
							string Days=D_Name.DayOfWeek.ToString();
							//Days=Days.Substring(0,1);
							DayName[i]=Days;
						}
						%>
				<tr>
					<td vAlign="top" colSpan="5">
						<table borderColor="#deba84" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr bgColor="#ce4848">
								<td align="center"><font color="#ffffff"><b>Employee Name / Day</b></font></td>
								<%
										int k=0;
										for(int i=1;i<=Day;i++)
										{
											string Days=DayName[k].Substring(0,1);
											%>
								<td align="center"><font color="#ffffff"><b><%=i.ToString()%><br>
											<%=Days.ToString()%>
										</b></font>
								</td>
								<%
											k++;
										}
										%>
								<td align="center"><font color="#ffffff"><b>Total<br>
											P</b></font></td>
								<td align="center"><font color="#ffffff"><b>Total<br>
											A</b></font></td>
							</tr>
							<%
                                DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
                                string[] arr1 = new string[Day];
                                string[] arr2 = new string[Day];
                                string FromDate=DropMonth.SelectedIndex+"/1/"+DropYear.SelectedItem.Text;
                                string ToDate=DropMonth.SelectedIndex+"/"+Day.ToString()+"/"+DropYear.SelectedItem.Text;
                                InventoryClass obj = new InventoryClass();
                                SqlDataReader rdr,rdr1=null;
                                for(int i=0,j=1;i<Day;i++,j++)
                                {
                                    if (j > 9)
                                    {
                                        arr1[i] = DropMonth.SelectedIndex + "/" + j + "/" + DropYear.SelectedItem.Text;
                                    }
                                    else
                                    {
                                        arr1[i] = DropMonth.SelectedIndex + "/" + "0" + j + "/" + DropYear.SelectedItem.Text;
                                    }
                                    //arr2[i]="A";
                                }
                                string emp="";
                                //coment by vikas 21.11.2012 dbobj.SelectQuery("select emp_id,emp_name from employee",ref rdr1);
                                dbobj.SelectQuery("select emp_id,emp_name from employee where status=1",ref rdr1);
                                while(rdr1.Read())
                                {
                                    for(int i=0;i<Day;i++)
                                    {
                                        //arr1[i]=DropMonth.SelectedIndex+"/"+j+"/"+DropYear.SelectedItem.Text;
                                        //coment by vikas 20.12.2012 arr2[i]="A";
                                        if(DayName[i]=="Sunday")
                                            arr2[i]="S";
                                        else
                                            arr2[i]="A";
                                    }
                                    int countP=0,countA=0;
                                    emp=rdr1.GetValue(1).ToString();
                                    rdr = obj.GetRecordSet("select * from attandance_register where att_date>='"+FromDate+"' and att_date<='"+ToDate+"' and emp_id='"+rdr1.GetValue(0).ToString()+"' and status=1 order by att_date");
                                    while(rdr.Read())
                                    {
                                        for(int i=0;i<arr1.Length;i++)
                                        {
                                            string strTrimDate = GenUtil.trimDate(rdr.GetValue(0).ToString());
                                            string strDate = GenUtil.str2MMDDYYYY(strTrimDate);
                                            if(strDate.StartsWith("0"))
                                            {
                                                strDate = strDate.Remove(0, 1);
                                            }


                                            if(GenUtil.trimDate(strDate).Equals(arr1[i].ToString()))
                                            {
                                                arr2[i]="P";
                                                countP++;
                                                break;
                                            }
                                        }
                                    }
                                    rdr.Close();
                                    countA=Day-countP;
										%>
							<tr>
								<td>&nbsp;<%=emp%></td>
								<%
											for(int i=0;i<arr1.Length;i++)
											{
												if(DayName[i]=="Sunday")
												{
													%>
								<td bgColor="#ce4848" align="center" width="15"><font color="#ffffff"><b><%=arr2[i].ToString()%></b></font></td>
								<%
												}
												else
												{
													if(arr2[i].ToString()=="P")
													{
														%>
								<td align="center" width="15"><%=arr2[i].ToString()%></td>
								<%
													}
													else
													{
														%>
								<td align="center" width="15"><font color="#ff0000"><%=arr2[i].ToString()%></font></td>
								<%
													}
												}
											}
											%>
								<td align="right"><b><%=countP.ToString()%></b></td>
								<td align="right"><b><%=countA.ToString()%></b></td>
							</tr>
							<%
									}
									%>
						</table>
					</td>
				</tr>
				<%
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message.ToString());	
				}%>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
