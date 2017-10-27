<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Import namespace="RMG"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="DBOperations"%>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Page language="c#" Inherits="Servosms.Module.Master.CustomerUpdation" CodeFile="CustomerUpdation.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Customer Updation</title><!--
Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
No part of this software shall be reproduced, stored in a 
retrieval system, or transmitted by any means, electronic 
mechanical, photocopying, recording  or otherwise, or for
any  purpose  without the express  written  permission of
bbnisys Technologies.
-->
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
		function CheckSearchOption(t)
		{
			var index = t.selectedIndex
			var f = document.Form1;
			if(index != 0)
			{
				if(index == 1)
					f.texthiddenprod.value=f.tempCustName.value;
				else if(index == 2)
					f.texthiddenprod.value=f.tempCustType.value;
				else if(index == 3)
					f.texthiddenprod.value=f.tempDistrict.value;
				else if(index == 4)
					f.texthiddenprod.value=f.tempPlace.value;
				else if(index == 5)
					f.texthiddenprod.value=f.tempSSR.value;
				f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
			}
			else
				f.texthiddenprod.value="";
			document.Form1.DropValue.value="All";
			document.Form1.DropProdName.style.visibility="hidden"
			//alert(f.texthiddenprod.value)
		}
		</script>
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
		function Check(t,e)
		{
			if(window.event) 
			{
				var	key = e.keyCode;
				if(key==13)
				{
					if(t!=null)
						t.focus();
				}
			}
		}
		function CheckUniqueCode(t,ssc)
		{
			if(t.value!="")
			{
				var arrCode = new Array();
				var Ucode = document.Form1.tempUniqueCode.value;
				if(Ucode!="")
				{
					arrCode = Ucode.split(",");
					for(var i=0;i<arrCode.length;i++)
					{
						if(t.value!=ssc.value)
						{
							if(t.value==arrCode[i])
							{
								alert("Unique Code already Exist");
								t.value="";
								t.focus();
								break
							}
						}
					}
				}
			}
		}
		</script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
  </HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<input type="hidden" name="tempUniqueCode" id="tempUniqueCode" runat="server"> <input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server">
			<input id="tempCustType" style="WIDTH: 1px" type="hidden" name="tempCustType" runat="server">
			<input id="tempDistrict" style="WIDTH: 1px" type="hidden" name="tempDistrict" runat="server">
			<input id="tempSSR" style="WIDTH: 1px" type="hidden" name="tempSSR" runat="server">
			<input id="tempPlace" style="WIDTH: 1px" type="hidden" name="tempPlace" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="278" width="778" align="center">
				<TR>
					<TH align="center" valign="top" height="20" colspan="5">
						<font color="#ce4848">Customer Updation</font><hr>
					</TH>
				</TR>
				
				<tr valign="top">
					<TD width="10%">Search By</TD>
					<td width="15%"><asp:dropdownlist id="DropSearchBy" Width="125px" Runat="server" onchange="CheckSearchOption(this)"
							CssClass="fontstyle" onselectedindexchanged="DropSearchBy_SelectedIndexChanged">
							<asp:ListItem Value="All">All</asp:ListItem>
							<asp:ListItem Value="Customer Name">Customer Name</asp:ListItem>
							<asp:ListItem Value="Customer Type">Customer Type</asp:ListItem>
							<asp:ListItem Value="District">District</asp:ListItem>
							<asp:ListItem Value="Place">Place</asp:ListItem>
							<asp:ListItem Value="SSR">SSR</asp:ListItem>
						</asp:dropdownlist></td>
					<td width="5%" align="right">
						Value
					</td>
					<td width="40%"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
							onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.txtview)"
							style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 150px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
							value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
							readOnly type="text" name="temp"><br>
						<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onmousemove="MouseMove(this)" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.txtview)"
								id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.txtview,document.Form1.DropValue)"
								style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 170px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
								type="select-one"></select></div>
					</td>
					<td width="50%"><asp:button id="txtview" runat="server" Width="60px" Text="View"  Height="24" onclick="txtview_Click"></asp:button></td>
				</tr>
				<%
				if(chkPriv)
				{
				%>
				<tr>
					<td align="center" valign="top" colspan="5">
						<table cellSpacing="0" cellPadding="0" border="1">
							<%
							InventoryClass obj=new InventoryClass();
							InventoryClass obj1=new InventoryClass();
							DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
							//coment by vikas 27.05.09 SqlDataReader rdr =obj.GetRecordSet("select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,Op_Balance,Balance_type,tel_off,mobile,tin_no,cr_limit,cr_days from Customer order by Cust_Name");
							SqlDataReader rdr =obj.GetRecordSet(sqlstr);
							int j=0;
							if(rdr.HasRows)
							{
								%>
							<tr bgColor="#ce4848">
								<td align="center">
									<font color="white">Unique Code</font></td>
								<td align="center">
									<font color="white">Customer Name</font></td>
								<td align="center">
									<font color="white">Address</font></td>
								<td align="center">
									<font color="white">Customer Type</font></td>
								<td align="center">
									<font color="white">SSR</font></td>
								<td align="center">
									<font color="white">City</font></td>
								<td align="center">
									<font color="white">Contact Person</font></td>
								<td align="center">
									<font color="white">Contact No</font></td>
								<td align="center">
									<font color="white">Mobile</font></td>
								<td align="center">
									<font color="white">Tin No</font></td>
								<td align="center">
									<font color="white">Cr. Limit</font></td>
								<td align="center">
									<font color="white">Cr. Days</font></td>
								<td align="center">
									<font color="white">Balance</font></td>
								<td align="center">
									<font color="white">Type</font></td>
							</tr>
							<%	
								while(rdr.Read())
								{ 
									int i=0,k=1;%>
							<tr>
								<td><input type=hidden name="txtInfo<%=j%>To<%=i%>" value="<%=rdr.GetValue(i).ToString()%>" class=dropdownlist>
									<input type=text name="txtInfo<%=j%>To<%=++i%>" value="<%=rdr.GetValue(i).ToString()%>" class=dropdownlist size=7 maxlength=15 onkeyup="Check(document.Form1.txtInfo<%=j+1%>To<%=k%>,event);" onkeypress="return GetAnyNumber(this, event);" onblur="CheckUniqueCode(this,document.Form1.txtSSCInfo<%=j%>To<%=i%>);">
									<input type=hidden name="txtSSCInfo<%=j%>To<%=i%>" value="<%=rdr.GetValue(i).ToString()%>" class=dropdownlist></td>
								<td><input type=text name="txtInfo<%=j%>To<%=++i%>" value="<%=rdr.GetValue(i).ToString()%>" class=dropdownlist maxlength=49 onkeyup="Check(document.Form1.txtInfo<%=j+1%>To<%=k%>,event);" onkeypress="return GetAnyNumber(this, event);"></td>
								<td><input type=text name="txtInfo<%=j%>To<%=++i%>" value="<%=rdr.GetValue(i).ToString()%>" class=dropdownlist maxlength=99 onkeyup="Check(document.Form1.txtInfo<%=j+1%>To<%=k%>,event);" onkeypress="return GetAnyNumber(this, event);"></td>
								<td><select name="txtInfo<%=j%>To<%=++i%>" class=dropdownlist>
										<option value="Select">Select</option>
										<%
											SqlDataReader rdr1 =obj1.GetRecordSet("select distinct CustomerTypeName from CustomerType order by CustomerTypeName");
											while(rdr1.Read())
											{
												if(rdr.GetValue(i).ToString()==rdr1.GetValue(0).ToString())
												{
													%>
										<option value="<%=rdr1.GetValue(0).ToString()%>" selected><%=rdr1.GetValue(0).ToString()%></option>
										<%
												}
												else
												{
													%>
										<option value="<%=rdr1.GetValue(0).ToString()%>"><%=rdr1.GetValue(0).ToString()%></option>
										<%
												}
											}
											rdr1.Close();%>
									</select></td>
								<td><select name="txtInfo<%=j%>To<%=++i%>" class=dropdownlist>
										<option value="Select">Select</option>
										<%
											rdr1 =obj1.GetRecordSet("select Emp_Name from employee where designation='Servo Sales Representative' order by Emp_Name");
											SqlDataReader rdr2=null;
											string empname="";
											while(rdr1.Read())
											{
												dbobj.SelectQuery("Select Emp_Name from Employee where emp_id='"+rdr.GetValue(i).ToString()+"'",ref rdr2);
												if(rdr2.Read())
												{
													empname=rdr2.GetValue(0).ToString();
												}
												else
												{
													empname="";
												}
												rdr2.Close();
												if(empname==rdr1.GetValue(0).ToString())
												{
													%>
										<option value="<%=rdr1.GetValue(0).ToString()%>" selected><%=rdr1.GetValue(0).ToString()%></option>
										<%
												}
												else
												{
													%>
										<option value="<%=rdr1.GetValue(0).ToString()%>"><%=rdr1.GetValue(0).ToString()%></option>
										<%
												}
											}
											rdr1.Close();
										%>
									</select></td>
								<td><select name="txtInfo<%=j%>To<%=++i%>" class=dropdownlist>
										<option value="Select">Select</option>
										<%
												rdr1 =obj1.GetRecordSet("select City from Beat_Master order by City");
												while(rdr1.Read())
												{
													if(rdr.GetValue(i).ToString()==rdr1.GetValue(0).ToString())
													{
														%>
										<option value="<%=rdr1.GetValue(0).ToString()%>" selected><%=rdr1.GetValue(0).ToString()%></option>
										<%
													}
													else
													{
														%>
										<option value="<%=rdr1.GetValue(0).ToString()%>"><%=rdr1.GetValue(0).ToString()%></option>
										<%
													}
												}
												rdr1.Close();%>
									</select></td>
								<td><input size=7 type=text style="WIDTH: 100px" name="txtInfo<%=j%>To<%=++i%>" onkeyup="Check(document.Form1txtInfo<%=j+1%>To<%=k%>,event)" value="<%=rdr.GetValue(i).ToString()%>" class=dropdownlist maxlength=27></td>
								<%
											for(i=8; i<13; i++)
											{
												if(i==10)
												{
													%>
								<td><input size=7 type=text name="txtInfo<%=j%>To<%=i%>" onkeyup="Check(document.Form1.txtInfo<%=j+1%>To<%=k%>,event)" value="<%=rdr.GetValue(i).ToString()%>" class=dropdownlist maxlength=11></td>
								<%
												}
												else
												{
													%>
								<td><input size=7 type=text name="txtInfo<%=j%>To<%=i%>" onkeyup="Check(document.Form1.txtInfo<%=j+1%>To<%=k%>,event)" value="<%=rdr.GetValue(i).ToString()%>" class=dropdownlist onkeypress="return GetOnlyNumbers(this, event,false,false);" maxlength=11></td>
								<%
												}
											}
											%>
								<td><input size=5 type=text name="txtInfo<%=j%>To<%=i%>" onkeyup="Check(document.Form1.txtInfo<%=j+1%>To<%=k%>,event)" value="<%=rdr.GetValue(i).ToString()%>" class=dropdownlist onkeypress="return GetOnlyNumbers(this, event,false,true);" maxlength=11></td>
								<td><select name="txtInfo<%=j%>To<%=++i%>" class=dropdownlist>
										<%
											if(rdr.GetValue(i).ToString()=="Dr.")
											{
												%>
										<option value="<%=rdr.GetValue(i).ToString()%>" ><%=rdr.GetValue(i).ToString()%></option>
										<%
											}
											else
											{
												%>
										<option value="Dr.">Dr.</option>
										<%
											}
											if(rdr.GetValue(i).ToString()=="Cr.")
											{
												%>
										<option value="<%=rdr.GetValue(i).ToString()%>" selected><%=rdr.GetValue(i).ToString()%></option>
										<%
											}
											else
											{
												%>
										<option value="Cr.">Cr.</option>
										<%
											}
											%>
									</select></td>
							</tr>
							<%
									j++;
								}
							}
							else
							{
							MessageBox.Show("Data Not available");
							}
							rdr.Close();%>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" colspan="5"><asp:Button TabIndex="1000" ID="btnSubmit" Text="Submit" Runat="server" Width="80"
							 onclick="btnSubmit_Click"></asp:Button>
						<input type=hidden name="Total_Cust" value="<%=j%>">
					</td>
				</tr>
				<%}%>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>
