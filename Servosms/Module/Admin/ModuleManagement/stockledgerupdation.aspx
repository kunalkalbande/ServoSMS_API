<!--
Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
No part of this software shall be reproduced, stored in a 
retrieval system, or transmitted by any means, electronic 
mechanical, photocopying, recording  or otherwise, or for
any  purpose  without the express  written  permission of
bbnisys Technologies.
-->
<%@ Page language="c#" Inherits="Servosms.Module.Admin.ModuleManagement.stockledgerupdation" CodeFile="stockledgerupdation.aspx.cs" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Import namespace="DBOperations"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="RMG"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<HTML>
	<HEAD>
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
	<script language=JavaScript>
	function makeRound(t)
	{

	var str = t;
	if(str != "")
	{
	str = eval(str)*100;

	str  = Math.round(str);

	str = eval(str)/100;

	t = str;
	return t;
	}
	
	}
		
		
	function enableText(t,t1)
	{
	  if(t.checked)
	  {
	    t1.disabled=false
	  }
	  else
	  {
	   t1.disabled=true
	  }
	}	
	
	function selectAll()
	{
		var f=document.f1
		if(f.chkSelectAll.checked)
		{
		   for(var i=0;i<f.length;i++)
		   {	
		      if(f.elements[i].type=="text")
	          {    
	               f.elements[i].disabled=false
              }
			  f.elements[i].checked=true
			  
		   }
		}	
		else
		{
		   for(var i=0;i<f.length;i++)
		   {	
		      if(f.elements[i].type=="text")
	          {    
	               f.elements[i].disabled=true
              }
					f.elements[i].checked=false  
		      }
		 }	
		 
	}
	
	
	function Validate()
	{
	  
		
	}
	</script>
    <title>ServoSMS: Stock Ledger Balance Updation</title>
    <script id="Validations" language="javascript" src="../../../Sysitem/JS/Validations.js"></script>
	<LINK rel="stylesheet" type="text/css" href="../../../Sysitem/Styles.css">
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body onkeydown="change(event)">
		<form id="f1" method="post" runat=server>
			<uc1:Header id="Header1" runat="server"></uc1:Header>
			<table width=778 height=290px align=center cellpadding=0 cellspacing=0>
				<TR>
					<TH align="center"><font color=#CE4848>Stock Ledger Balance Updation</font><hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<table border=1 cellpadding=0 cellspacing=0 borderColor="#deba84">
							<tr bgcolor=#CE4848>
								<th><font color=white>Category</font></th>
								<th><font color=white>Product Name</font></th>
								<th><font color=white>Pack Type</font></th>
								<th><font color=white>Stock</font></th>
								<th><font color=white>Update Stock</font></th>
								<th><font color=white>Select</font></th>
							</tr>
							<%
								DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
								InventoryClass obj=new InventoryClass ();
								//InventoryClass obj1=new InventoryClass ();
								SqlDataReader SqlDtr,rdr=null;
								string sql;
								int Prod_No=0;

								sql="select Category,Prod_Name,Pack_Type from Products order by Prod_Name";
								SqlDtr=obj.GetRecordSet(sql);
								while(SqlDtr.Read())
								{
								//dbobj.SelectQuery("select top 1 closing_stock from stock_master where productID=(select prod_ID from Products where prod_Name='"+ SqlDtr.GetValue(1).ToString() +"' and Pack_type='"+ SqlDtr.GetValue(2).ToString() +"' and category='"+ SqlDtr.GetValue(0).ToString() +"')order by stock_date desc",ref rdr);
								dbobj.SelectQuery("select Opening_Stock from Products where prod_Name='"+ SqlDtr.GetValue(1).ToString() +"' and Pack_type='"+ SqlDtr.GetValue(2).ToString() +"' and category='"+ SqlDtr.GetValue(0).ToString() +"'",ref rdr);
								
	                           %>
							<tr>
								
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><%=SqlDtr.GetValue(0).ToString()%><input type=hidden name=lblcategory<%=Prod_No%> value="<%=SqlDtr.GetValue(0).ToString()%>"></font></td>
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><%=SqlDtr.GetValue(1).ToString()%><input type=hidden name=lblname<%=Prod_No%> value="<%=SqlDtr.GetValue(1).ToString()%>"></font></td>
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><%=SqlDtr.GetValue(2).ToString()%><input type=hidden name=lbltype<%=Prod_No%> value="<%=SqlDtr.GetValue(2).ToString()%>"></font></td>
								
								<% 
								if(rdr.Read())
								{
								%>
								
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510> <%=rdr["Opening_Stock"].ToString()%></font> </td>
								</font>	
								<td bgcolor=#fff7e7><input disabled name="txtupdatestk<%=Prod_No%>"  class=dropdownlist style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" onkeypress="return GetOnlyNumbers(this, event, false,true);" style="COLOR: #8c4510"  style="BACKGROUND-COLOR: #fff7e7" style="width:80" type=text maxlength=8 class=fontstyle></td>
								
								
								<td align=center  bgcolor=#fff7e7><font color=#8c4510>
								<!--input type=checkbox name=chk<%=Prod_No%> ></font></td-->
								<input type=checkbox name=chk<%=Prod_No%> onclick="enableText(this,document.f1.txtupdatestk<%=Prod_No%>);" class=dropdownlist></font></td>
							<%
							}
							else
							{
							%>
								<td bgcolor=#fff7e7>&nbsp; <font color=#8c4510>0</font> </td>
								</font>	
								<td bgcolor=#fff7e7><input disabled name="txtupdatestk<%=Prod_No%>"   onkeypress="return GetOnlyNumbers(this, event, false,true);" style="COLOR: #8c4510"  style="BACKGROUND-COLOR: #fff7e7" style="width:80" type=text maxlength=8 class=fontstyle></td>
								<td align=center  bgcolor=#fff7e7><font color=#8c4510>
								<!--input type=checkbox name=chk<%=Prod_No%> ></font></td-->
								<input type=checkbox name=chk<%=Prod_No%> onclick="enableText(this,document.f1.txtupdatestk<%=Prod_No%>);" class=dropdownlist></font></td>
							
							</tr>
							<%								
								}
								Prod_No++;
								}
								SqlDtr.Close();
							%>
							
							<tr><td bgcolor=#fff7e7><font color=#8c4510><input type=hidden name=Total_cust value=<%=Prod_No%>></font></td></tr>
							<tr><td colspan=4 align=right bgcolor=#fff7e7><font color=#8c4510><input type=hidden name=n1 onclick="Validate();"></font>
							<asp:Button ID=Btnsubmit1 Text=submit Runat=server OnClick="update" Width=80 OnLoad="Page_Load" ></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
							<td  align=right bgcolor=#fff7e7><font color=#8c4510>Select All</font></td><td align=center bgcolor=#fff7e7><input type=checkbox name=chkSelectAll onclick="selectAll();"></td></tr>
							
							
						</table>
					</td>
				</tr>
				
			</table><uc1:Footer id="Footer1" runat="server"></uc1:Footer>
		</form>
	</body>
</HTML>
<script language=C# runat =server >

		private void Page_Load(object sender, System.EventArgs e)
		{
			/*
			try
			{
				string pass;
				pass=(Session["User_Name"].ToString());
			}
			catch
			{
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="4";
				string SubModule="10";
				string[,] Priv=(string[,]) Session["Privileges"];
				for(i=0;i<Priv.GetLength(0);i++)
				{
					if(Priv[i,0]== Module &&  Priv[i,1]==SubModule)
					{						
						View_flag=Priv[i,2];
						Add_Flag=Priv[i,3];
						Edit_Flag=Priv[i,4];
						Del_Flag=Priv[i,5];
						break; 
					}
				}	
				if(View_flag=="0")
				{
					//string msg="UnAthourized Visit to Price Updation Page";
				//	dbobj.LogActivity(msg,Session["User_Name"].ToString());  
					MessageBox.Show("hello");
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				}
				if(Add_Flag =="0")
				  Btnsubmit1.Enabled = false; 
				if(Edit_Flag =="0")
				  Btnsubmit1.Enabled = false; 
				#endregion
			}
			 */
		}
		
		public void update(Object sender, EventArgs e )
		{  
		 try
		 {
		DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		 
		 //InventoryClass obj1=new InventoryClass ();
			SqlDataReader SqlDtr1=null;
			SqlDataReader rdr1=null;
			int Total_prod=0;
			string prod_cat="",sql;
			int flag = 0;
			string j="";
			int x=0;
			InventoryClass obj=new InventoryClass ();
			
			Total_prod=System.Convert.ToInt32(Request.Params.Get("Total_cust"));
			for(int i=0;i<Total_prod;i++)
			{ 
				if(Request.Params.Get("Chk"+i)!=null)
				{  
	                dbobj1.SelectQuery("select prod_ID from Products where prod_Name='"+ Request.Params.Get("lblname"+i)+"' and Pack_type='"+Request.Params.Get("lbltype"+i)+"' and category= '"+Request.Params.Get("lblcategory"+i)+"' ",ref SqlDtr1);
	                //**
					//sql="select prod_ID from Products where prod_Name='"+ Request.Params.Get("lblname"+i)+"' and Pack_type='"+Request.Params.Get("lbltype"+i)+"' and category= '"+Request.Params.Get("lblcategory"+i)+"' ";
					//SqlDtr1=obj.GetRecordSet(sql);
					//**
	                if(SqlDtr1.Read())
					{
				    // MessageBox.Show(SqlDtr1["prod_ID"].ToString().Trim());
					//dbobj1.Insert_or_Update("update stock_master set  closing_stock ='"+Request.Params.Get("txtupdatestk"+i)+"',opening_stock ='"+Request.Params.Get("txtupdatestk"+i)+"',receipt='"+0+"',sales='"+0+"' where productID='"+SqlDtr1["prod_ID"].ToString().Trim()+"' ",ref x);
					//************************
					obj.Product_Name=Request.Params.Get("lblname"+i);
					obj.Category=Request.Params.Get("lblcategory"+i);
					obj.Package_Type=Request.Params.Get("lbltype"+i);
					obj.Opening_Stock=Request.Params.Get("txtupdatestk"+i);
					obj.InsertorUpdate();
					CreateLogFiles.ErrorLog("Form:stockledgerUpdation.aspx,Method:update() Updated Stock is "+Request.Params.Get("txtupdatestk"+i)+" Product Name & Pack is "+Request.Params.Get("lblname"+i)+" "+Request.Params.Get("lbltype"+i));
					}
				}
			}
				MessageBox.Show("Stock Updated");
				
		 }
		 catch(Exception ex)
		 {
		 CreateLogFiles.ErrorLog("Form:stockledgerUpdation.aspx,Method:update().   EXCEPTION " +ex.Message );
		 // CreateLogFiles.ErrorLog("Form:stockledgerUpdation.aspx,Method:update().   EXCEPTION " +ex.Message +"  User_ID : "+ Session["User_Name"].ToString());
		 }
			
		}
</script>
