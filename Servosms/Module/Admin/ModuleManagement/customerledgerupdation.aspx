<!--
Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
No part of this software shall be reproduced, stored in a 
retrieval system, or transmitted by any means, electronic 
mechanical, photocopying, recording  or otherwise, or for
any  purpose  without the express  written  permission of
bbnisys Technologies.
-->
<%@ Page language="c#" Inherits="Servosms.Module.Admin.ModuleManagement.customerledgerupdation" CodeFile="customerledgerupdation.aspx.cs" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Import namespace="DBOperations"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="RMG"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../../HeaderFooter/Footer.ascx" %>
<!--!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" --> 
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
		
		function check1(t,t1,t2,t3,t4)
		{
		
		var temp = t.value;
		var temp2=t1.value;
		var temp1 = t2.value;
		if(temp != "")
		{
		  if(temp1 == "Fuel")
		  {
		    temp2  = eval(temp2)/1000; 
		   if (temp2 >= temp)
		   {
		   alert("Sales Rate of Product "+t3.value+" should be greater than "+makeRound(temp2));
		   t4.checked = false;
		   t.disabled=true
	       t1.disabled=true
		   
		   return false;
		   }
		  }
		 }
		return true;
		}
	function enableText(t,t1,t2)
	{
	  if(t.checked)
	  {
	    t1.disabled=false
	    t2.disabled=false
	  }
	  else
	  {
	   t1.disabled=true
	   t2.disabled=true
	  }
	}	
		
	function fff()
	{
	var ff=document.f1
	for(var i=3;i<ff.length-1;i+=5)
	{
		ff.elements[i].disabled=false;
		if(i>500)
		{
		break
		}
	}
	}
	
	function selectAll(t)
	{
		var f=document.f1
		
		if(t.checked)
		{
			for(var i=5;i<f.length-1;i+=5)
			{
				 f.elements[i].checked=true
			}
		   for(var i=4;i<f.length-1;i+=5)
			{
				 f.elements[i].disabled=false
			}
		   
		 for(var i=3;i<f.length-1;i+=5)
		  {
		  
		  f.elements[i].disabled=false;
		   
              }
         }	
		else
		{
			for(var i=5;i<f.length-1;i+=5)
			{
				 f.elements[i].checked=false
			}
		   for(var i=4;i<f.length-1;i+=5)
			{
				 f.elements[i].disabled=true
			}
		}	
	}
	
	function Validate()
	{
		
	}
	</script>
    <title>ServoSMS: Customer Ledger Balance Updation</title>
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
					<TH align="center"><font color=#CE4848>Customer Ledger Balance Updation</font><hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<table border=1 cellpadding=0 cellspacing=0 borderColor="#deba84">
							<tr>
								<th bgcolor="#CE4848"><font color=#ffffff>Customer Name</font></th>
								<th bgcolor="#CE4848"><font color=#ffffff>Type</font></th>
								<th bgcolor="#CE4848"><font color=#ffffff>Balance</font></th>
								<th bgcolor="#CE4848"><font color=#ffffff>Update Balance</font></th>
								<th bgcolor="#CE4848"><font color=#ffffff>Bal Type</font></th>
								<th bgcolor="#CE4848"><font color=#ffffff>Select</font></th>
							</tr>
							<%
								DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
								InventoryClass obj=new InventoryClass ();
								SqlDataReader SqlDtr,rdr=null;
								string sql;
								int Prod_No=0;
									string pname="";
								//sql="select cust_name + ':' + city,cust_type from Customer order by cust_name";
								//sql="select cust_name,city,cust_type from Customer order by cust_name";
								sql="select cust_name,city,cust_type,Op_Balance,Balance_Type from Customer order by cust_name";
								SqlDtr=obj.GetRecordSet(sql);
								while(SqlDtr.Read())
								{
								pname=SqlDtr.GetValue(0).ToString()+ ':' +SqlDtr.GetValue(1).ToString();
								string[] arr=pname.Split(new char[]{':'},pname.Length);  
								dbobj.SelectQuery("select Top 1 balance,balancetype from CustomerLedgerTable where custID=(select Cust_ID from customer where cust_Name='"+ arr[0] +"' and Cust_type='"+ SqlDtr.GetValue(2).ToString() +"')order by entrydate",ref rdr);
							//**	dbobj.SelectQuery("select Top 1 balance,balancetype from CustomerLedgerTable where custID=(select Cust_ID from customer where cust_Name='"+ SqlDtr.GetValue(0).ToString() +"' and Cust_type='"+ SqlDtr.GetValue(1).ToString() +"')order by entrydate",ref rdr);
	%>
							<tr>
								
								<!--td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><%=SqlDtr.GetValue(0).ToString()%><input type=hidden name=lblcust<%=Prod_No%> value="<%=SqlDtr.GetValue(0).ToString()%>"></font></td-->
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><%=pname%><input type=hidden name=lblcust<%=Prod_No%> value="<%=pname%>"></font></td>
								<!--td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><%=SqlDtr.GetValue(0).ToString()%><input type=hidden name=lblcust<%=Prod_No%> value="<%=SqlDtr.GetValue(0).ToString()%>"></font></td-->
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><%=SqlDtr.GetValue(2).ToString()%><input type=hidden name=lblcategory<%=Prod_No%> value="<%=SqlDtr.GetValue(2).ToString()%>"></font></td>
								
								<% 
								if(rdr.Read())
								{
								%>
								
								<td bgcolor=#fff7e7><font color=#8c4510>&nbsp;<%=rdr["balance"].ToString()%> <%=rdr["balancetype"].ToString()%></font></td>
								</font>	
								<td bgcolor=#fff7e7><font color=#8c4510><input disabled name="txtupdatebal<%=Prod_No%>" onkeypress="return GetOnlyNumbers(this, event, false,true);" type=text style="width:80; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle maxlength=9></font></td>
								
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510>
								
								<Select disabled name="dropscheme<%=Prod_No%>" style="width: 60" class=fontstyle>
								<option Value="Dr." style="BACKGROUND-COLOR: #fff7e7; COLOR: #8c4510">Dr.</option>
								<option Value="Cr." style="BACKGROUND-COLOR: #fff7e7; COLOR: #8c4510">Cr.</option>
								</Select></font>
								</td>
								<td align=center  bgcolor=#fff7e7><font color=#8c4510>
								<!--input type=checkbox name=chk<%=Prod_No%> ></font></td-->
								<input type=checkbox style="BACKGROUND-COLOR: #fff7e7" name=chk<%=Prod_No%> onclick="enableText(this,document.f1.txtupdatebal<%=Prod_No%>,document.f1.dropscheme<%=Prod_No%>);"></font></td>
							
							<%	Prod_No++;
							
								}
								else
								{
							%>
							<td bgcolor=#fff7e7><font color=#8c4510>&nbsp;<%=SqlDtr["Op_Balance"].ToString()%> <%=SqlDtr["Balance_Type"].ToString()%></font></td>
								</font>	
								<td bgcolor=#fff7e7><font color=#8c4510><input disabled name="txtupdatebal<%=Prod_No%>" onkeypress="return GetOnlyNumbers(this, event, false,true);" type=text style="width:80; COLOR: #8c4510; BACKGROUND-COLOR: #fff7e7; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" class=fontstyle maxlength=9></font></td>
								
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510>
								
								<Select disabled name="dropscheme<%=Prod_No%>" style="width: 60" class=fontstyle>
								<option Value="Dr." style="BACKGROUND-COLOR: #fff7e7; COLOR: #8c4510">Dr.</option>
								<option Value="Cr." style="BACKGROUND-COLOR: #fff7e7; COLOR: #8c4510">Cr.</option>
								</Select></font>
								</td>
								<td align=center  bgcolor=#fff7e7><font color=#8c4510>
								<!--input type=checkbox name=chk<%=Prod_No%> ></font></td-->
								<input type=checkbox style="BACKGROUND-COLOR: #fff7e7" name=chk<%=Prod_No%> onclick="enableText(this,document.f1.txtupdatebal<%=Prod_No%>,document.f1.dropscheme<%=Prod_No%>);"></font></td>
							
							<%	Prod_No++;
							
								}
							%>
							</tr>
							<%	
								}
							%>
							
							<tr><td bgcolor=#fff7e7><font color=#8c4510><input type=hidden name=Total_cust value=<%=Prod_No%>></font></td></tr>
							<tr><td colspan=4 align=right bgcolor=#fff7e7><font color=#8c4510><input type=hidden name=n1 onclick="Validate();"></font>
							<asp:Button ID=Btnsubmit1 Text=submit Runat=server OnClick="update" Width=80 OnLoad="Page_Load"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
							<td  align=right bgcolor=#fff7e7><font color=#8c4510>Select All</font></td><td align=center bgcolor=#fff7e7><input type=checkbox name=chkSelectAll onclick="selectAll(this);"></td></tr>
							
							
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
			
		/*	try
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
				string SubModule="9";
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
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				}
				if(Add_Flag =="0")
				  Btnsubmit1.Enabled = false; 
				if(Edit_Flag =="0")
				  Btnsubmit1.Enabled = false; 
				#endregion
			}*/
		}
		string Ledger_id="";
		public void GetNextLedgerID()
		{
			InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string sql;
	
			sql="select max(Ledger_ID)+1 from Ledger_Master";
			SqlDtr=obj.GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				Ledger_id =SqlDtr.GetValue(0).ToString ();				
				if(Ledger_id=="")
					Ledger_id ="1001";
			}
			SqlDtr.Close ();
		}
		public void update(Object sender, EventArgs e )
		{  
			try
			{
				/*////////////////////////////bhal start////////////////////////////////////*/
				DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
				SqlDataReader SqlDtr1=null,rdr=null;
				SqlDataReader rdr1=null;
				int Total_cust=0;
				string prod_cat="";
				int flag = 0;
				string j="",dropscheme="";
				int x=0;
				InventoryClass obj=new InventoryClass ();
			
				Total_cust=System.Convert.ToInt32(Request.Params.Get("Total_cust"));
				//****bhal add******
				/*	
				for(int i=0;i<Total_cust;i++)
				{ 
					if(Request.Params.Get("Chk"+i)!=null)
					{ 
						//dbobj1.SelectQuery("select  cust_ID from customer where cust_Name='"+ Request.Params.Get("lblcust"+i)+"' and cust_Type='"+ Request.Params.Get("lblcategory"+i)+"' ",ref SqlDtr1);
						if(Request.Params.Get("dropscheme"+i)=="Select")
						{
							MessageBox.Show("Please select Balance Type For Customer Name: "+Request.Params.Get("lblcust"+i));
							return;	                  
						}
					}
				}
				*/
				//****bhal end*****
				for(int i=0;i<Total_cust;i++)
				{
					if(Request.Params.Get("dropscheme"+i)=="Dr.")
						dropscheme="Dr";
					else
						dropscheme="Cr";
					if(Request.Params.Get("Chk"+i)!=null)
					{
						Ledger_id="";
						string Acc_Date_From="",Acc_Date_To="";
						string str=Request.Params.Get("lblcust"+i);
						string[] arr1=str.Split(new char[]{':'},str.Length);
						string s="select * from Organisation";
						rdr1=obj.GetRecordSet(s);
						if(rdr1.Read())
						{
							Acc_Date_From=rdr1["Acc_Date_From"].ToString();
							Acc_Date_To=rdr1["Acc_Date_To"].ToString();
						}
						else
						{
							MessageBox.Show("Please Set The Account Period in Organisation Form");
							return;
						}
						rdr1.Close();
						s="select Ledger_ID from Ledger_Master where Ledger_Name='"+arr1[0]+"'";
						rdr1=obj.GetRecordSet(s);
						if(rdr1.Read())
						{
							Ledger_id=rdr1["Ledger_ID"].ToString();
						}
						else
						{
							GetNextLedgerID();
							dbobj1.Insert_or_Update("insert into Ledger_Master values("+Ledger_id+",'"+arr1[0]+"','115','"+Acc_Date_From+"','"+Acc_Date_To+"','"+Request.Params.Get("txtupdatebal"+i)+"','"+dropscheme+"')",ref x);
						}
						rdr1.Close();
					
						//**dbobj1.SelectQuery("select  cust_ID from customer where cust_Name='"+ Request.Params.Get("lblcust"+i)+"' and cust_Type='"+ Request.Params.Get("lblcategory"+i)+"' ",ref SqlDtr1);
						dbobj1.SelectQuery("select  cust_ID from customer where cust_Name='"+ arr1[0]+"' and cust_Type='"+ Request.Params.Get("lblcategory"+i)+"' ",ref SqlDtr1); 
						if(SqlDtr1.Read())
						{
							//******
						    dbobj1.SelectQuery("select * from customerledgertable where custID='"+SqlDtr1["cust_ID"].ToString().Trim()+"' and Particular='Opening Balance'",ref rdr);
						    if(rdr.HasRows)
						    {
								//******
								//MessageBox.Show(SqlDtr1["cust_ID"].ToString().Trim());
								dbobj1.Insert_or_Update("update customerledgertable set balance='"+Request.Params.Get("txtupdatebal"+i)+"',balancetype='"+Request.Params.Get("dropscheme"+i)+"'where custID='"+SqlDtr1["cust_ID"].ToString().Trim()+"' and Particular='Opening Balance'",ref x);
								// dbobj1.Insert_or_Update("update customerledgertable set balance='"+Request.Params.Get("txtupdatebal"+i)+"',balancetype='"+dropscheme+"'where custID='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref x);
							}
							else
							{
								dbobj1.Insert_or_Update("insert into customerledgertable values("+SqlDtr1["cust_ID"].ToString().Trim()+",'"+Acc_Date_From+"','Opening Balance',0,0,'"+Request.Params.Get("txtupdatebal"+i)+"','"+Request.Params.Get("dropscheme"+i)+"')",ref x);
							}
							if(Request.Params.Get("dropscheme"+i)=="Cr.")
							{
								dbobj1.SelectQuery("select * from customer_balance where cust_ID='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref rdr);
								if(rdr.HasRows)
									dbobj1.Insert_or_Update("update customer_balance set Cr_Amount='"+Request.Params.Get("txtupdatebal"+i)+"',Dr_Amount='0' where cust_ID='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref x);  
								else
									dbobj1.Insert_or_Update("insert into customer_balance values("+SqlDtr1["cust_ID"].ToString().Trim()+",'"+Request.Params.Get("txtupdatebal"+i)+"',0)",ref x);
								dbobj1.Insert_or_Update("update customerledgertable set CreditAmount='"+Request.Params.Get("txtupdatebal"+i)+"', debitAmount='0' where custID='"+SqlDtr1["cust_ID"].ToString().Trim()+"' and Particular='Opening Balance'",ref x);
								//Mahesh dbobj1.Insert_or_Update("update AccountsLedgerTable set Credit_Amount='"+Request.Params.Get("txtupdatebal"+i)+"',balance='"+Request.Params.Get("txtupdatebal"+i)+"',bal_type='"+Request.Params.Get("dropscheme"+i)+"',debit_Amount='0' where ledger_ID='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref x);
								dbobj1.SelectQuery("select * from AccountsLedgerTable where Ledger_ID='"+Ledger_id+"' and Particulars='Opening Balance'",ref rdr);
								if(rdr.HasRows)
									dbobj1.Insert_or_Update("update AccountsLedgerTable set Credit_Amount='"+Request.Params.Get("txtupdatebal"+i)+"',balance='"+Request.Params.Get("txtupdatebal"+i)+"',bal_type='"+dropscheme+"',debit_Amount='0' where ledger_ID='"+Ledger_id+"' and Particulars='Opening Balance'",ref x);
								else
									dbobj1.Insert_or_Update("insert into AccountsLedgerTable values("+Ledger_id+",'"+Acc_Date_From+"','Opening Balance',0,'"+Request.Params.Get("txtupdatebal"+i)+"','"+Request.Params.Get("txtupdatebal"+i)+"','"+dropscheme+"')",ref x);
								//dbobj1.Insert_or_Update("update AccountsLedgerTable set Credit_Amount='"+Request.Params.Get("txtupdatebal"+i)+"',balance='"+Request.Params.Get("txtupdatebal"+i)+"',bal_type='"+dropscheme+"',debit_Amount='0' where ledger_ID='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref x);
							}
							else
							{
								dbobj1.SelectQuery("select * from customer_balance where cust_ID='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref rdr);
								if(rdr.HasRows)
							  		dbobj1.Insert_or_Update("update customer_balance set Dr_Amount='"+Request.Params.Get("txtupdatebal"+i)+"',Cr_Amount='0' where cust_ID='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref x);  
								else
									dbobj1.Insert_or_Update("insert into customer_balance values("+SqlDtr1["cust_ID"].ToString().Trim()+",0,'"+Request.Params.Get("txtupdatebal"+i)+"')",ref x);
								dbobj1.Insert_or_Update("update customerledgertable set DebitAmount='"+Request.Params.Get("txtupdatebal"+i)+"',balancetype='"+Request.Params.Get("dropscheme"+i)+"',CreditAmount='0' where custID='"+SqlDtr1["cust_ID"].ToString().Trim()+"' and Particular='Opening Balance'",ref x);
		                        //dbobj1.Insert_or_Update("update customerledgertable set DebitAmount='"+Request.Params.Get("txtupdatebal"+i)+"',balancetype='"+dropscheme+"',CreditAmount='0' where custID='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref x);
		                        //Mahesh dbobj1.Insert_or_Update("update AccountsLedgerTable set Debit_Amount='"+Request.Params.Get("txtupdatebal"+i)+"',balance='"+Request.Params.Get("txtupdatebal"+i)+"',bal_type='"+Request.Params.Get("dropscheme"+i)+"',Credit_Amount='0' where ledger_ID='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref x);
								dbobj1.SelectQuery("select * from AccountsLedgerTable where Ledger_ID='"+Ledger_id+"' and Particulars='Opening Balance'",ref rdr);
								if(rdr.HasRows)
									dbobj1.Insert_or_Update("update AccountsLedgerTable set Debit_Amount='"+Request.Params.Get("txtupdatebal"+i)+"',balance='"+Request.Params.Get("txtupdatebal"+i)+"',bal_type='"+dropscheme+"',Credit_Amount='0' where ledger_ID='"+Ledger_id+"' and Particulars='Opening Balance'",ref x);
								else
									dbobj1.Insert_or_Update("insert into AccountsLedgerTable values("+Ledger_id+",'"+Acc_Date_From+"','Opening Balance','"+Request.Params.Get("txtupdatebal"+i)+"',0,'"+Request.Params.Get("txtupdatebal"+i)+"','"+dropscheme+"')",ref x);
								// dbobj1.Insert_or_Update("update AccountsLedgerTable set Debit_Amount='"+Request.Params.Get("txtupdatebal"+i)+"',balance='"+Request.Params.Get("txtupdatebal"+i)+"',bal_type='"+dropscheme+"',Credit_Amount='0' where ledger_ID='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref x);
							}
							//Mahesh dbobj1.Insert_or_Update("update Ledger_master set Op_balance='"+Request.Params.Get("txtupdatebal"+i)+"',bal_type='"+Request.Params.Get("dropscheme"+i)+"' where ledger_id='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref x);  
							dbobj1.Insert_or_Update("update Ledger_master set Op_balance='"+Request.Params.Get("txtupdatebal"+i)+"',bal_type='"+dropscheme+"' where ledger_id='"+Ledger_id+"'",ref x);  
							//dbobj1.Insert_or_Update("update Ledger_master set Op_balance='"+Request.Params.Get("txtupdatebal"+i)+"',bal_type='"+dropscheme+"' where ledger_id='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref x);  
							dbobj1.Insert_or_Update("update customer set Op_balance='"+Request.Params.Get("txtupdatebal"+i)+"',balance_type='"+Request.Params.Get("dropscheme"+i)+"' where cust_id='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref x);  
							// dbobj1.Insert_or_Update("update customer set Op_balance='"+Request.Params.Get("txtupdatebal"+i)+"',balance_type='"+dropscheme+"' where cust_id='"+SqlDtr1["cust_ID"].ToString().Trim()+"'",ref x);  
							
							//*****dbobj1.SelectQuery("select * from LedgDetails where Cust_ID='"+SqlDtr1["cust_ID"].ToString().Trim()+"' and Bill_No='O/B'",ref rdr);
							//*****if(rdr.HasRows)
							//*****	dbobj1.Insert_or_Update("update LedgDetails set Amount='"+Request.Params.Get("txtupdatebal"+i)+"' where cust_id='"+SqlDtr1["cust_ID"].ToString().Trim()+"' and Bill_No='O/B'",ref x);  
							//*****else
							//*****	dbobj1.Insert_or_Update("insert into LedgDetails values("+SqlDtr1["cust_ID"].ToString().Trim()+",'O/B','"+Acc_Date_From+"','"+Request.Params.Get("txtupdatebal"+i)+"')",ref x);  
							CreateLogFiles.ErrorLog("Form:customerledgerUpdation.aspx,Method:update() Updated Balance is "+Request.Params.Get("txtupdatebal"+i)+" "+Request.Params.Get("dropscheme"+i)+" Customer Name & Category is "+Request.Params.Get("lblcust"+i)+"("+Request.Params.Get("lblcategory"+i)+")");
						}
						SqlDtr1.Close();
					}
				}
				MessageBox.Show("customer balance Updated");
				
			}
/*//////////////////////end///////////////////////////////////////////*/ 
		 
		/*	InventoryClass obj=new InventoryClass(); 
			int Total_customer=0;
			string prod_cat="";
			int flag = 0;
			 

			Total_customer=System.Convert.ToInt32(Request.Params.Get("Total_cust"));
			for(int i=0;i<Total_customer;i++)
			{ 
				if(Request.Params.Get("Chk"+i)!=null)
				{ 										
					obj.custname=Request.Params.Get("lblcust"+i); 
					obj.custType=Request.Params.Get("lblcategory"+i); 
					obj.balance=Request.Params.Get("txtupdatebal"+i); 
					obj.balancetype=Request.Params.Get("dropscheme"+i); 
					obj.InsertcustbalanceUpdation();
				//	CreateLogFiles.ErrorLog("Form:customerledgerUpdation.aspx,Method:update().   balance Updated of customer " +Request.Params.Get("lblcust"+i)+" User_ID: "+Session["User_Name"].ToString());
							
				}
			}
				MessageBox.Show("balance Updated");
		 }*/
		 catch(Exception ex)
		 {
		 MessageBox.Show(ex.Message);
		 CreateLogFiles.ErrorLog("Form:customerledgerUpdation.aspx,Method:update().   EXCEPTION " +ex.Message );
		 // CreateLogFiles.ErrorLog("Form:customerledgerUpdation.aspx,Method:update().   EXCEPTION " +ex.Message +"  User_ID : "+ Session["User_Name"].ToString());
		 }
			
		}
</script>