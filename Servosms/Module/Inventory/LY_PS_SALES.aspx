<%@ Page language="c#" Inherits="Servosms.Module.Inventory.LY_PS_SALES" CodeFile="LY_PS_SALES.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Import namespace="DBOperations" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="RMG"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: LY_PS_SALES</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
			<table height="290px" width="778" cellSpacing="0" cellPadding="0" align="center" border="0">
				<tr vAlign="top" height="20">
					<td align="center" colSpan="23"><b><font color="#CE4848">LAST YEAR PRIMARY / SECONDARY 
								SALES DATA UPDATION FORM</font></b><hr>
					</td>
				</tr>
				<tr vAlign="top">
					<td colSpan="23">&nbsp;&nbsp;YearFrom&nbsp;&nbsp;<asp:dropdownlist id="DropYearFrom" Runat="server" CssClass="dropdownlist">
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
						</asp:dropdownlist>&nbsp;&nbsp;YearTo&nbsp;&nbsp;<asp:dropdownlist id="DropYearTo" Runat="server" CssClass="dropdownlist">
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
						</asp:dropdownlist>&nbsp;&nbsp;Discription&nbsp;&nbsp;<asp:TextBox ID=txtDiscription Runat=server CssClass=dropdownlist MaxLength=49 Width="200px" BorderStyle=Groove onkeypress="return GetAnyNumber(this, event);"></asp:TextBox>
							&nbsp;<asp:RadioButton ID=RadSummarized Runat=server Text=Summarized GroupName=Radio></asp:RadioButton>
							&nbsp;<asp:RadioButton ID="RadDetails" Runat=server Text=Details GroupName=Radio Checked=True></asp:RadioButton>
						&nbsp;&nbsp;<asp:button id="btnView"  Width="65"
							Text="View" Runat="server" onclick="btnView_Click"></asp:button>&nbsp;
						<asp:button id="btnSubmit" Width="65"
							Text="Submit" Runat="server" onclick="btnSubmit_Click"></asp:button></td>
				</tr>
				<tr>
					<td>
						<table borderColor="#deba84" cellSpacing="0" cellPadding="0" border="1" align=center>
							<%if(View==1){%>
							<tr bgColor="#CE4848">
								<%if(RadDetails.Checked){%>
								<th rowSpan="3">
								<%}else{%>
								<th rowSpan="2">
								<%}%>
									<font color="#ffffff">Month</font></th>
								<%if(RadDetails.Checked){%>
								<th colSpan="5">
								<%}else{%>
								<th colSpan="1">
								<%}%>
									<font color="#ffffff">Primary Sales</font></th>
								<%if(RadDetails.Checked){%>
								<th colSpan="30">
								<%}else{%>
								<th colSpan="1">
								<%}%>
									<font color="#ffffff">Secondary Sales</font></th></tr>
			<%
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr;
			string[] Mon={"Apr."+DropYearFrom.SelectedItem.Text,"May."+DropYearFrom.SelectedItem.Text,"Jun."+DropYearFrom.SelectedItem.Text,"Jul."+DropYearFrom.SelectedItem.Text,"Aug."+DropYearFrom.SelectedItem.Text,"Sep."+DropYearFrom.SelectedItem.Text,"Oct."+DropYearFrom.SelectedItem.Text,"Nov."+DropYearFrom.SelectedItem.Text,"Dec."+DropYearFrom.SelectedItem.Text,"Jan."+DropYearTo.SelectedItem.Text,"Feb."+DropYearTo.SelectedItem.Text,"Mar."+DropYearTo.SelectedItem.Text};
			int count=7;
			if(RadDetails.Checked)
			{
			%>
							<tr bgColor="#CE4848">
								<td align="center" rowSpan="2"><font color="#ffffff">Purchase</font></td>
								<td align="center" rowSpan="2"><font color="#ffffff">Purchase<br>
										FOC</font></td>
								<td align="center" rowSpan="2"><font color="#ffffff">Genuine<br>
										Oils</font></td>
								<td align="center" rowSpan="2"><font color="#ffffff">Greases</font></td>
								<td align="center" rowSpan="2"><font color="#ffffff">Total<br>Purchase</font></td>
								<%
			//int count=5;
			ArrayList arrstr = new ArrayList();
			//string str="select distinct case when customertypename like 'oe%' then 'Oe' else customertypename end as customertypename from customertype order by customertypename";
			string str="select distinct custtype,custtypeid from tempcustomertype order by custtypeid";
			SqlDtr=obj.GetRecordSet(str);
			if(SqlDtr.HasRows)
			{
				while(SqlDtr.Read())
				{
					arrstr.Add(SqlDtr.GetValue(0).ToString());
					if(SqlDtr.GetValue(0).ToString().ToLower().StartsWith("ro") || SqlDtr.GetValue(0).ToString().ToLower().StartsWith("bazzar") || SqlDtr.GetValue(0).ToString().ToLower().StartsWith("bazar"))
					{%>
						<td align="center" colSpan="2"><font color="#ffffff"><%=SqlDtr.GetValue(0).ToString()%></font></td>
					<%
					}
					else
					{%>
						<td align="center" rowspan="2"><font color="#ffffff"><%=SqlDtr.GetValue(0).ToString()%></font></td>
					<%
					}
				}
				%>
				<td align="center" rowspan=2><font color="#ffffff">Total<br>Sales</font></td>
				<%
			}
			SqlDtr.Close();
			%>
			</tr>
			<tr bgColor="#CE4848">
			<%for(int n=0;n<arrstr.Count;n++)
			{
				if(arrstr[n].ToString().ToLower().StartsWith("ro") || arrstr[n].ToString().ToLower().StartsWith("bazzar") || arrstr[n].ToString().ToLower().StartsWith("bazar"))
				{count+=2;%>
								<td align="center"><font color="#ffffff">Lube</font></td>
								<td align="center"><font color="#ffffff">2T/4T</font></td>
								<%}else{count++;}
			}
           %>
							</tr>
							<%
			//string[] Mon={"Apr."+DropYearFrom.SelectedItem.Text,"May."+DropYearFrom.SelectedItem.Text,"Jun."+DropYearFrom.SelectedItem.Text,"Jul."+DropYearFrom.SelectedItem.Text,"Aug."+DropYearFrom.SelectedItem.Text,"Sep."+DropYearFrom.SelectedItem.Text,"Oct."+DropYearFrom.SelectedItem.Text,"Nov."+DropYearFrom.SelectedItem.Text,"Dec."+DropYearFrom.SelectedItem.Text,"Jan."+DropYearTo.SelectedItem.Text,"Feb."+DropYearTo.SelectedItem.Text,"Mar."+DropYearTo.SelectedItem.Text};
			str="select "+ColumnName+" from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"'";
			SqlDtr=obj.GetRecordSet(str);
			if(SqlDtr.HasRows)
			{
			int ii=0;
			while(SqlDtr.Read())
			{
			%>
				<tr>
					<td bgColor="#fff7e7">&nbsp;&nbsp;<%=SqlDtr.GetValue(2).ToString()%><input type=hidden value="<%=SqlDtr.GetValue(2).ToString()%>" name="Month<%=ii%>"></td>
					<%
					int s=0;
					for(int j=1;j<count-1;j++)
					{
						if(j==5){
						%>
							<td bgColor="#fff7e7"><input class=formTextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" type=text maxLength=10 size=3 name="txtTotalPurchase<%=ii%>" value="<%=SqlDtr.GetValue(j+2).ToString()%>"></td>
						<%}else{%>
							<td bgColor="#fff7e7"><input class=formTextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" type=text maxLength=10 size=3 name="txt<%=ii%>To<%=j%>" value="<%=SqlDtr.GetValue(j+2).ToString()%>"></td>
						<%}s=j+1;
					}%>
					<td bgColor="#fff7e7"><input class=formTextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" type=text maxLength=10 size=3 name="txtTotalSales<%=ii%>" value="<%=SqlDtr.GetValue(s+2).ToString()%>"></td>
					<%ii++;%>
				</tr>
			<%}
			}
			else
			{
			
			for(int i=0;i<Mon.Length;i++)
			{
			%>
							<tr>
								<td bgColor="#fff7e7">&nbsp;&nbsp;<%=Mon[i]%><input type=hidden value="<%=Mon[i]%>" name="Month<%=i%>"></td>
								<%	
								for(int j=1;j<count-1;j++)
								{
								if(j==5){
								%>
								<td bgColor="#fff7e7"><input class=formTextBox 
            onkeypress="return GetOnlyNumbers(this, event, false,true);" 
            type=text maxLength=10 size=3 name="txtTotalPurchase<%=i%>" 
            style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"></td>
								<%}else{
							%>
								<td bgColor="#fff7e7"><input class=formTextBox 
            onkeypress="return GetOnlyNumbers(this, event, false,true);" 
            type=text maxLength=10 size=3 name="txt<%=i%>To<%=j%>" 
            style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"></td>
								<%}}%>
							<td bgColor="#fff7e7"><input class=formTextBox 
            onkeypress="return GetOnlyNumbers(this, event, false,true);" 
            type=text maxLength=10 size=3 name="txtTotalSales<%=i%>" 
            style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"></td>
							</tr>
							<%
							}
			}				
			}
			else
			{
			%>
			<tr bgColor="#CE4848">
				<td align=center><font color=white>Total Purchase</font></td>
				<td align=center><font color=white>Total Sales</font></td>
			</tr>
			
			<%
			string str="select Month,total_purchase,total_sales from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"'";
			SqlDtr=obj.GetRecordSet(str);
			if(SqlDtr.HasRows)
			{
				int i=0;
				while(SqlDtr.Read())
				{
					%>
					<tr>
						<td bgColor="#fff7e7">&nbsp;&nbsp;<%=SqlDtr.GetValue(0).ToString()%><input type=hidden value="<%=SqlDtr.GetValue(0).ToString()%>" name="Month<%=i%>"></td>
						<td bgColor="#fff7e7"><input class=fontstyle onkeypress="return GetOnlyNumbers(this, event, false,true);" type=text maxLength=12 style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"width:100%" name="txtTotalPurchase<%=i%>" value="<%=SqlDtr.GetValue(1).ToString()%>"></td>
						<td bgColor="#fff7e7"><input class=fontstyle onkeypress="return GetOnlyNumbers(this, event, false,true);" type=text maxLength=12 style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"width:100%" name="txtTotalSales<%=i%>" value="<%=SqlDtr.GetValue(2).ToString()%>"></td>
					</tr>
			<%i++;}
			}
			else
			{
			for(int i=0;i<Mon.Length;i++){%>
				<tr>
					<td bgColor="#fff7e7" align=center width=100>&nbsp;&nbsp;<%=Mon[i]%><input type=hidden value="<%=Mon[i]%>" name="Month<%=i%>"></td>
					<td bgColor="#fff7e7"><input class=fontstyle onkeypress="return GetOnlyNumbers(this, event, false,true);" type=text maxLength=12 style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"width:100%" name="txtTotalPurchase<%=i%>"></td>
					<td bgColor="#fff7e7"><input class=fontstyle onkeypress="return GetOnlyNumbers(this, event, false,true);" type=text maxLength=12 style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"width:100%" name="txtTotalSales<%=i%>"></td>
				</tr>
			<%}count=3;}}%>
			<tr>
				<td><input type=hidden value="<%=Mon.Length%>" name=M><input type=hidden value="<%=count%>" name=col></td>
			</tr>
			<%}%>
						</table>
					</td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
		<script language="C#" runat="server">
	/*public void update(Object sender, EventArgs e )
	{  
		try
		{
			InventoryClass obj=new InventoryClass(); 
			int count = int.Parse(Request.Params.Get("M"));
			int j=0,m=1;
			//GetNextID();
			for(int i=0;i<count;i++)
			{
				
				j=1;
				//obj.foid=NextID.ToString();
				obj.foid=System.Convert.ToString(m);
				obj.Month=Request.Params.Get("Month"+i);
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub0="";
				else
					obj.sub0=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub1="";
				else
					obj.sub1=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub2="";
				else
					obj.sub2=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub3="";
				else
					obj.sub3=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub4="";
				else
					obj.sub4=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub5="";
				else
					obj.sub5=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub6="";
				else
					obj.sub6=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub7="";
				else
					obj.sub7=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub8="";
				else
					obj.sub8=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub9="";
				else
					obj.sub9=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub10="";
				else
					obj.sub10=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub11="";
				else
					obj.sub11=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub12="";
				else
					obj.sub12=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub13="";
				else
					obj.sub13=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub14="";
				else
					obj.sub14=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub15="";
				else
					obj.sub15=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub16="";
				else
					obj.sub16=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub17="";
				else
					obj.sub17=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub18="";
				else
					obj.sub18=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub19="";
				else
					obj.sub19=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub20="";
				else
					obj.sub20=Request.Params.Get("txt"+i+"To"+j);
				j++;
				if(Request.Params.Get("txt"+i+"To"+j)==null)
					obj.sub21="";
				else
					obj.sub21=Request.Params.Get("txt"+i+"To"+j);
									
				obj.InsertLY_PS_SALES(); 	
				m++;	
			}
			MessageBox.Show("Data Inserted Successfully");
			CreateLogFiles.ErrorLog("Form:LY_PS_SALES.aspx,Method:update().  Data Inserted, User_ID: "+Session["User_Name"].ToString());
		}
		catch(Exception ex)
		{
			CreateLogFiles.ErrorLog("Form:LY_PS_SALES.aspx,Method:update().   EXCEPTION " +ex.Message +"  User_ID : "+ Session["User_Name"].ToString());
		}
	}*/
	/*string NextID="";
	public void GetNextID()
	{
		InventoryClass obj=new InventoryClass();
		SqlDataReader SqlDtr;
		string str="select max(LY_PS_SALES)+1 from LY_PS_SALES";
		SqlDtr=obj.GetRecordSet(str);
		if(SqlDtr.Read())
		{
			NextID=SqlDtr.GetValue(0).ToString();
		}
		if(NextID=="")
			NextID="1";
		
	}*/
		</script>
	</body>
</HTML>
