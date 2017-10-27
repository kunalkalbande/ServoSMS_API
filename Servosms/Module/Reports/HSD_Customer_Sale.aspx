<%@ Page language="c#" Inherits="Servosms.Module.Reports.HSD_Customer_Sale" CodeFile="HSD_Customer_Sale.aspx.cs" %>
<%@ import namespace="System.Data.SqlClient"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ import namespace="RMG"%>
<%@ import namespace="System.IO"%>
<%@ import namespace="System.Net.Sockets"%>
<%@ import namespace="System.Net"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %> 
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %> 
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>ServoSMS: HSD Customer Sale</title>
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
			
		function NewCheck1(t)
		{
			var str=t.value
			if(t.value!="" && str.indexOf(".")>0)
			{
				str=str.substring(0,str.lastIndexOf("."));
				if(t.value.toLowerCase().indexOf(".xls")>0)
				{
					document.Form1.btnShow.disabled=false;
					document.Form1.tempPath.value=str;
				}
				else
				{
					alert("sdfPlease Select Appropriate '.xls' File");
					document.Form1.btnShow.disabled=true;
					return;
				}
			}
			else
			{
				alert("Please Select The 'xls' File");
				document.Form1.btnShow.disabled=false;
				return;
			}
			//alert(document.Form1.tempPath.value+" == "+str)
		}
  
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
		
	function enableText(t,t1,t2,t3)
	{
	  if(t.checked)
	  {
	    t1.disabled=false
	    t2.disabled=false
	    t3.disabled=false
	  }
	  else
	  {
		t1.disabled=true
		t2.disabled=true
		t3.disabled=true
	  }
	}
	
		</script>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <script language=javascript id=Validations src="../../Sysitem/JS/Validations.js"></script>
    <LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body onkeydown="change(event)">
    <form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<input type="hidden" id="tempPath" name="tempPath" runat="server">
			<input type="hidden" id="count" name="count" runat="server">
			<table height="290px" width="778" align="center">
				<TR>
					<TH align="center" valign="top" height="20">
						<font color="#CE4848">MS&nbsp;/&nbsp;HSD Customer Sale&nbsp;</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td valign=top align=center>
						<table valign=top align=center>
							<tr>
								<td colspan=6 valign=top align=center>
									<P align="center"><FONT size="5" color="#00ffff"><input id=file1 type=file name="file1" style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" size=70 onchange="NewCheck1(this);">&nbsp;<asp:button id="btnShow" runat="server" 
							Text="Read Excel File" Width="120px" onclick="btnShow_Click"></asp:button>&nbsp;</font></p></td></tr>
				<TR>
				<TD colspan=5 vAlign=top align="center">&nbsp;Period Name&nbsp;<asp:TextBox ID=txtName Runat=server Width=200 BorderStyle=Groove></asp:TextBox>&nbsp;<asp:DropDownList ID=dropName Runat=server AutoPostBack=True CssClass="dropdownlist" Visible=False onselectedindexchanged="dropName_SelectedIndexChanged"><asp:ListItem Value="Select">Select</asp:ListItem></asp:DropDownList>
					&nbsp;Date From&nbsp;
					<asp:textbox id="txtDateFrom" runat="server" Width="80px" BorderStyle="Groove" CssClass="dropdownlist"
						ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
						align="absbottom" border="0"></A>&nbsp;Date To&nbsp;
						<asp:textbox id="txtDateTo" runat="server" Width="80px" BorderStyle="Groove" CssClass="dropdownlist"
						ReadOnly="True">&nbsp;</asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
						align="absbottom" border="0"></A>&nbsp;&nbsp;<asp:button id="btnSave" runat="server" 
							Text="Submit" Width="75px" onclick="btnSave_Click"></asp:button>&nbsp;<asp:button id="btnEdit" runat="server" 
							Text="Edit" Width="75px" onclick="btnEdit_Click"></asp:button></TD>
				</TR>
				<tr>
					<td colspan=5 vAlign="top" align="center" height="20"><asp:button id="btnPrint" runat="server" 
							Text="Print" Width="75px" Visible=False onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnExcel" runat="server" 
							Text="Excel" Width="75px" Visible=False onclick="btnExcel_Click"></asp:button>&nbsp;</td></tr>
				<%
				if(flage==1)
				{	
					if(dropName.Visible==false)
					{
						i=1;
						string Cust_type="",Place="";
						try
						{
							if(dtr.HasRows)
							{
								%>
								<tr>
									<th bgcolor="#CE4848"><font color=#ffffff>SNo</font></th>
									<th bgcolor="#CE4848"><font color=#ffffff>Customer Name</font></th>
									<th bgcolor="#CE4848"><font color=#ffffff>Cust Type</font></th>
									<th bgcolor="#CE4848"><font color=#ffffff>Place</font></th>
									<th bgcolor="#CE4848"><font color=#ffffff>SM</font></th>
									<th bgcolor="#CE4848"><font color=#ffffff>HSD</font></th>
								</tr>
								<%
								while(dtr.Read())
								{
									int pos=0;
									string cust_name=dtr.GetValue(0).ToString();
									pos=cust_name.IndexOf("'");
									if(pos>0)
									{
										cust_name=cust_name.Substring(0,cust_name.IndexOf("'"));
									}
									else
									{
										//if(cust_name.Length>10)
										cust_name=cust_name.Substring(0,4);
									}	
									InventoryClass obj=new InventoryClass();
									SqlDataReader dtr2=null;
									string sql="select Cust_id,Cust_Name,cust_type,city from Customer where Cust_Name like '"+cust_name.ToString()+"%' and cust_type in (select customertypename from customertype where group_name like 'Ro%')";
									dtr2=obj.GetRecordSet(sql);
									if(dtr2.HasRows)
									{
										%>
										<tr>
											<td><%=i.ToString()%></td>
											<td><select style="width:200; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" name="dropcustomer<%=i%>" class=dropdownlist>
												<%
												while(dtr2.Read())
												{
													%><option value="<%=dtr2.GetValue(1).ToString()+" : "+dtr2.GetValue(0).ToString()%>" selected><%=dtr2.GetValue(1).ToString()+" : "+dtr2.GetValue(0).ToString()%></option>
													<%
													Cust_type=dtr2.GetValue(3).ToString();
													Place=dtr2.GetValue(2).ToString();
												}
												dtr2.Close();
												%>
											</select>
											</td><%
											%>
											<td><input type=text name=Cust_Type<%=i%> width=70px value="<%=Cust_type.ToString()%>" style="width:80; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle></td>
											<td><input type=text name=Cust_Place<%=i%> width=70px value="<%=Place.ToString()%>" style="width:80; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle></td>
											<td><input type=text name=MS<%=i%> width=70px value="<%=dtr.GetValue(1).ToString()%>" style="width:60; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle></td>
											<td><input type=text name=HSD<%=i%> width=70px value="<%=dtr.GetValue(2).ToString()%>" style="width:60; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle></td>
											</tr>
											<%
											i++;
									}
									else
									{
										%><tr>
											<td><%=i.ToString()%></td>
											<td><select style="width:200; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" name="dropcustomer<%=i%>" class=dropdownlist>
												<option value="Select:0">Select</option>
												</select></td>
											<td><input type=text name=Cust_Type<%=i%> width=70px value="<%=Cust_type.ToString()%>" style="width:80; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle></td>
											<td><input type=text name=Cust_Place<%=i%> width=70px value="<%=Place.ToString()%>" style="width:80; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle></td>
											<td><input type=text name=MS<%=i%> width=70px value="<%=dtr.GetValue(1).ToString()%>" style="width:60; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle></td>
											<td><input type=text name=HSD<%=i%> width=70px value="<%=dtr.GetValue(2).ToString()%>" style="width:60; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle></td>
										</tr>
										<%
										i++;
									}
								}
								dtr.Close();
								flage=i;
							}
						}
						catch(Exception ex)
						{
							MessageBox.Show(ex.Message.ToString());
						}
					}
					else
					{
						i=1;
						string Cust_type="",Place="";
						try
						{
						
								InventoryClass obj1=new InventoryClass();
								SqlDataReader dtr=null;
								string sql="select c.Cust_id,Cust_Name,cust_type,city,ms,hsd,datefrom,dateto from Customer c,Cust_sale_ms_hsd CSMH where c.cust_id=csmh.cust_id and period_name='"+dropName.SelectedItem.Text.ToString()+"'";
								dtr=obj1.GetRecordSet(sql);
								if(dtr.HasRows)
								{
									%>
									<tr>
										<th bgcolor="#CE4848"><font color=#ffffff>SNo</font></th>
										<th bgcolor="#CE4848"><font color=#ffffff>Customer Name</font></th>
										<th bgcolor="#CE4848"><font color=#ffffff>Cust Type</font></th>
										<th bgcolor="#CE4848"><font color=#ffffff>Place</font></th>
										<th bgcolor="#CE4848"><font color=#ffffff>SM</font></th>
										<th bgcolor="#CE4848"><font color=#ffffff>HSD</font></th>
									</tr>
									<%	
									while(dtr.Read())
									{
										
										%>
										<tr>
											<td><%=i.ToString()%></td>
											<td><select style="width:200; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" name="dropcustomer<%=i%>" class=dropdownlist>
												<option value="<%=dtr["Cust_Name"].ToString()+" : "+dtr["Cust_Id"].ToString()%>"><%=dtr["Cust_Name"].ToString()+" : "+dtr["Cust_Id"].ToString()%></option>
												</select></td>
											<td><input type=text name=Cust_Type<%=i%> width=70px value="<%=dtr["Cust_Type"].ToString()%>" style="width:80; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle></td>
											<td><input type=text name=Cust_Place<%=i%> width=70px value="<%=dtr["City"].ToString()%>" style="width:80; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle></td>
											<td><input type=text name=MS<%=i%> width=70px value="<%=dtr["MS"].ToString()%>" style="width:60; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle></td>
											<td><input type=text name=HSD<%=i%> width=70px value="<%=dtr["HSD"].ToString()%>" style="width:60; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle></td>
											</tr>
											<%
											i++;
												
								}
								dtr.Close();
								flage=i;
							}
						}
						catch(Exception ex)
						{
							MessageBox.Show(ex.Message.ToString());
						}
					}
				} 
					%>
				</table>
				</td>
				</tr>
				<tr>
					<td><asp:ValidationSummary ID="Validationsummary1" Runat="server" ShowSummary="False" ShowMessageBox="True"></asp:ValidationSummary></td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
				scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>
<script language=C# runat =server >
public void Print(Object sender, EventArgs e)
{
	/*
	try
	{
		InventoryClass obj=new InventoryClass();
		System.Data.SqlClient.SqlDataReader rdr=null;
		string home_drive = Environment.SystemDirectory;
		home_drive = home_drive.Substring(0,2); 
		string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\LubeIndent.txt";
		StreamWriter sw = new StreamWriter(path);
		string sql="select * from Indent_Lube il,products p where ssaid='"+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+"' and il.prodcode=p.prod_code";
		string info = "";
		sw.Write((char)27);//added by vishnu
		sw.Write((char)67);//added by vishnu
		sw.Write((char)0);//added by vishnu
		sw.Write((char)12);//added by vishnu
		
		sw.Write((char)27);//added by vishnu
		sw.Write((char)78);//added by vishnu
		sw.Write((char)5);//added by vishnu
						
		sw.Write((char)27);//added by vishnu
		sw.Write((char)15);
		//**********
		string des="-----------------------------------------------------------------------------------------------------------------------------------------";
		string Address=GenUtil.GetAddress();
		string[] addr=Address.Split(new char[] {':'},Address.Length);
		sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
		sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
		sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
		sw.WriteLine(des);
		//******S***
		sw.WriteLine(GenUtil.GetCenterAddr("=================================",des.Length));
		sw.WriteLine(GenUtil.GetCenterAddr("Lube Indent Report For "+DropMonth.SelectedItem.Text,des.Length));
		sw.WriteLine(GenUtil.GetCenterAddr("=================================",des.Length));
		sw.WriteLine("+-------+-------+-----------+----------+---------+--------+------------+--------------------+------+-------+--------+-------------------+");
		sw.WriteLine("|  RSE  |SUP.EX.|RETAIL MPSO| SKY TYPE |PACK CODE|PACK QTY|PRODUCT CODE| SKU NAME WITH PACK |INDENT|RECEIPT|DIFFRENT|    REMARK         |");
		sw.WriteLine("+-------+-------+-----------+----------+---------+--------+------------+--------------------+------+-------+--------+-------------------+");
		//             1234567 1234567 12345678901 1234567890 123456789 12345678 123456789012 12345678901234567890 123456 1234567 12345678 1234567890123456789
		int i=0;
		info = " {0,-7:S} {1,-7:S} {2,-11:S} {3,-10:S} {4,-9:S} {5,-8:S} {6,-12:S} {7,-20:S} {8,6:S} {9,7:S} {10,8:S} {11,-19:S}";
		rdr=obj.GetRecordSet(sql);
		if(rdr.HasRows)
		{
			while(rdr.Read())
			{
				sw.WriteLine(info,GenUtil.TrimLength(rdr["rse"].ToString(),7),
					GenUtil.TrimLength(rdr["supex"].ToString(),7),
					GenUtil.TrimLength(rdr["retailmpso"].ToString(),11),
					GenUtil.TrimLength(rdr["skutype"].ToString(),10),
					rdr["packcode"].ToString(),
					rdr["packqty"].ToString(),
					rdr["prodcode"].ToString(),
					GenUtil.TrimLength(rdr["skunamewithpack"].ToString(),20),
					rdr["indent"].ToString(),
					getReceipt(rdr["prodcode"].ToString()),
					getDiff(getReceipt(rdr["prodcode"].ToString()),rdr["Indent"].ToString()),
					GenUtil.TrimLength(Request.Params.Get("txtRemark"+i++),19)
					);
			}
		}
		sw.WriteLine("+-------+-------+-----------+----------+---------+--------+------------+--------------------+------+-------+--------+-------------------+");
		//dbobj.Dispose();
		sw.Close();
		CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:Print() Indent Updated For "+DropMonth.SelectedItem.Text+" 2007");
		
	}
	catch(Exception ex)
	{
		CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:update().   EXCEPTION " +ex.Message );
	}
	*/
}
public void Excel(Object sender, EventArgs e)
{
	
}
</script>
