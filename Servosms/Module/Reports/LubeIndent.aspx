<%@ Page language="c#" Inherits="Servosms.Module.Reports.LubeIndent" CodeFile="LubeIndent.aspx.cs" %>
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
    <title>ServoSMS: SSA Lube Indent</title>
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
    <script language=javascript>
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
			<table height="290px" width="778" align="center">
				<TR>
					<TH align="center" valign="top" height="20">
						<font color="#CE4848">SSA Lube Indent For The Month Of&nbsp;
							<asp:DropDownList ID="DropMonth" Runat="server" Width="100" CssClass="dropdownlist" AutoPostBack=true>
								<asp:ListItem Value="Select">Select</asp:ListItem>
								<asp:ListItem Value="January">January</asp:ListItem>
								<asp:ListItem Value="February">February</asp:ListItem>
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
							</asp:DropDownList><asp:CompareValidator ID=cv1 Runat=server ValueToCompare=Select ControlToValidate=DropMonth Operator=NotEqual ErrorMessage="Please Select The Month">*</asp:CompareValidator>&nbsp; 
							<asp:DropDownList ID="DropYear" Runat="server" Width="100" CssClass=fontstyle>
								<asp:ListItem Value="2001">2001</asp:ListItem>
								<asp:ListItem Value="2002">2002</asp:ListItem>
								<asp:ListItem Value="2003">2003</asp:ListItem>
								<asp:ListItem Value="2004">2004</asp:ListItem>
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
							</asp:DropDownList></font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td vAlign="top" align="center" height="20"><asp:button id="btnPrint" runat="server" 
							Text="Print" Width="75px" onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnExcel" runat="server" 
							Text="Excel" Width="75px" onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<%try{%>
				<tr>
					<td align="center" valign="top">
						<%
						int i=0;
						
						if(DropMonth.SelectedIndex!=0)
						{
					InventoryClass obj = new InventoryClass();
			SqlDataReader rdr;
			rdr = obj.GetRecordSet("select * from Indent_Lube il,products p where ssaid='"+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+"' and il.prodcode=p.prod_code and il.packqty=p.total_qty order by prodcode");
			
			if(rdr.HasRows)
			{
					%>
						<TABLE border=1 BorderColor="#deba84" cellpadding=0 cellspacing=0 width=750>
							<TR height=25 bgcolor="#CE4848">
								<TD align="center"><font color=#ffffff><b>RSE</b></font></TD>
								<TD align="center"><font color=#ffffff><b>Sup Ex</b></font></TD>
								<TD align="center" width=8%><font color=#ffffff><b>Retail MPSO</b></font></TD>
								<TD align="center"><font color=#ffffff><b>SKU Type</b></font></TD>
								<TD align="center"><font color=#ffffff><b>Pack Code</b></font></TD>
								<TD align="center"><font color=#ffffff><b>Pack Qty</b></font></TD>
								<TD align="center"><font color=#ffffff><b>Product Code</b></font></TD>
								<TD align="center" width=30%><font color=#ffffff><b>SKU Name With Pack</b></font></TD>
								<TD align="center"><font color=#ffffff><b>Indent</b></font></TD>
								<TD align="center"><font color=#ffffff><b>Receipt</b></font></TD>
								<TD align="center"><font color=#ffffff><b>Difference</b></font></TD>
								<TD align="center"><font color=#ffffff><b>Remark</b></font></TD>
							</TR>
				<%
			while(rdr.Read())
			{
			%>
			<TR>
								<TD>&nbsp;<%=rdr["rse"].ToString()%></TD>
								<TD>&nbsp;<%=rdr["supex"].ToString()%></TD>
								<TD>&nbsp;<%=rdr["retailmpso"].ToString()%></TD>
								<TD>&nbsp;<%=rdr["skutype"].ToString()%></TD>
								<TD align="center"><%=rdr["packcode"].ToString()%></TD>
								<TD align="center"><%=rdr["packqty"].ToString()%></TD>
								<TD align="center"><%=rdr["prodcode"].ToString()%><input type=hidden value="<%=rdr["prodcode"].ToString()%>" name="txtProdCode<%=i%>"></TD>
								<TD>&nbsp;<%=rdr["skunamewithpack"].ToString()%><input type=hidden value="<%=rdr["skunamewithpack"].ToString()%>" name="txtProdName<%=i%>"></TD>
								<TD align="center"><%=rdr["Indent"].ToString()%></TD>
								<TD align=center><%=getReceipt(rdr["prodcode"].ToString(),rdr["packqty"].ToString())%></TD>
								<TD align=center><%=getDiff(getReceipt(rdr["prodcode"].ToString(),rdr["packqty"].ToString()),rdr["Indent"].ToString())%></TD>
								<TD><input type=text name=txtRemark<%=i%> onkeyup="Check(document.Form1.txtRemark<%=i+1%>,event)" size=15 class=dropdownlist maxlength=99 style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" onkeypress="return GetAnyNumber(this, event);"></TD>
							</TR>
			<%
			i++;
			}
			
				%>
						</TABLE>
				<%}
				else
				{
					MessageBox.Show("Data Not Available");
				}
				rdr.Close();
				}
				
				%>
				</td>
				</tr>
				<tr>
				<td><input type=hidden value="<%=i%>" name=count><asp:ValidationSummary ID=val1 Runat=server ShowSummary=False ShowMessageBox=True></asp:ValidationSummary></td>
				</tr>
				<%}
				catch(Exception ex)
				{
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Class:PetrolPumpClass.cs,Method:DropMonth_SelectedIndex, "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
				MessageBox.Show("All Products Code Should be Unique");
				}%>
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
