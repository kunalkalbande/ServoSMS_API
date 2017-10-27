<!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
<%@ Page language="c#" Inherits="Servosms.Module.Master.OilScheme" CodeFile="OilScheme.aspx.cs" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Import namespace="DBOperations"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="RMG"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>

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
	
	function selectAll()
	{
		var f=document.f1
		var ln=f.length
		//*************
		if(f.elements[ln-2].disabled==true)
		{
			f.chkSelectAll.checked=false
			return
		}
		//*********************************
		if(f.chkSelectAll.checked)
		{
		   for(var i=0;i<f.length;i++)
		   {	
	           
	          f.elements[i].disabled=false
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
            for(var j=4;j<f.length;)
			{
	              f.elements[j].disabled=true
	              f.elements[j+2].disabled=true
	              j=j+7
	              if(j>=(f.length)-1)
	              {
					  return
	              }
             }
				  f.elements[i].checked=false
		}	
	}
	
	function Validate()
	{
	  
	  var flag=0
	  var AnyChecked=0
	  var f=document.f1
	  // DO	NOT ADD/REMOVE ANY COMPONENT FROM THIS FORM
	  // OTHERWISE CHANGE THE INCREMANTATION  FACTOR APPROPRIATELY
	  for(var i=7;i<f.length;i=i+7)
	  { 
	   	if(f.elements[i].checked)
	 	{  
	       AnyChecked=1
	      if(f.elements[i-3].value=="Select" || f.elements[i-2].value=="" || f.elements[i-1].value=="Select")
		 {
			alert("Please Fill Scheme Type,Discount and Discount type of Checked Product")
			return
		 }
		}
	  }

		
		 f.submit()
	
	}
	</script>
		<title>ServoSMS: Scheme Updation</title>
		<script id="Validations" language="javascript" src="../../Sysitem/JS/Validations.js"></script>
		<LINK rel="stylesheet" type="text/css" href="../../Sysitem/Styles.css">
	</HEAD>
	<body onkeydown="change(event)">
		<form name="f1" id="f1" method="post" runat=server >
			<uc1:Header id="Header1" runat="server"></uc1:Header><table width=778 height=278 align=center>
				<TR>
					<TH align="center"><font color=#CE4848><font color=#CE4848>Scheme Updation</font></font><hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<table border=1 cellpadding=0 cellspacing=0  style="Bordercolor:#DEBA84">
							<tr>
								<th align="center" bgcolor=#CE4848><font color=#ffffff>Category</font></th>
								<th align="center" bgcolor=#CE4848><font color=#ffffff>Product Name</font></th>
								<th align="center" bgcolor=#CE4848><font color=#ffffff>Pack Type</font></th>
								<th align="center" bgcolor=#CE4848><font color=#ffffff>Scheme Type</font></th>
								<th align="center" bgcolor=#CE4848><font color=#ffffff>Discount Ltr/Rs</font></th>
								<th align="center" bgcolor=#CE4848><font color=#ffffff>Discount</font></th>
								<th align="center" bgcolor=#CE4848><font color=#ffffff>Select</font></th>
							</tr>
							<%
								DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
								InventoryClass obj=new InventoryClass ();
								SqlDataReader SqlDtr,rdr=null;
								string sql;
								int Prod_No=0;
								sql="select distinct Category, Prod_Name, Pack_Type from Products order by Category,Prod_Name";
								SqlDtr=obj.GetRecordSet(sql);
								while(SqlDtr.Read())
								{
								dbobj.SelectQuery("select top 1 schemetype, discount,discounttype from schemeupdation where Prod_ID=(select Prod_ID from Products where Prod_Name='"+ SqlDtr.GetValue(1).ToString() +"' and Category='"+ SqlDtr.GetValue(0).ToString() +"' and Pack_Type='"+SqlDtr.GetValue(2).ToString()+"') order by Eff_Date desc",ref rdr);
							%>
							<tr>
								
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><%=SqlDtr.GetValue(0).ToString()%><input type=hidden name=lblCat<%=Prod_No%> value="<%=SqlDtr.GetValue(0).ToString()%>"></font></td>
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><%=SqlDtr.GetValue(1).ToString()%><input type=hidden name=lblProd_Name<%=Prod_No%> value="<%=SqlDtr.GetValue(1).ToString()%>"></font></td>
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><%=SqlDtr.GetValue(2).ToString()%><input type=hidden name=lblPack_Type<%=Prod_No%> value="<%=SqlDtr.GetValue(2).ToString()%>"></font></td>
								<% 
								if(rdr.Read())
								{
								%>
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><Select disabled name="dropscheme<%=Prod_No%>" style="width: 90" >
								
								<option Value="Select"><font color=#8c4510>Select</font></option>
								<%
								if(rdr["schemetype"].ToString()=="Primary")
								{
								%>
								<option Value="Secondary"><font color=#8c4510>Secondary</font></option>
								
								<%}
								else
								{
								%><option Value="Primary"><font color=#8c4510>Primary</font></option>
								<%
								}
								%>
								<option  selected Value="<%=rdr["schemetype"].ToString()%>"><%=rdr["schemetype"].ToString()%></option>
								</Select></font>
								</td>
									<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><input value="<%=rdr["discount"].ToString()%>"  disabled type=text size=10 name="txtPurRate<%=Prod_No%>" onkeypress="return GetOnlyNumbers(this, event, false,true);" style="border-style:Groove; FONT-SIZE: 8pt; BACKGROUND-COLOR: #fff7e7" ></font></td>
									<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><Select disabled name=dropdiscount<%=Prod_No%>>
								<option Value="Select"><font color=#8c4510>Select</font></option>
								<%
								if(rdr["discounttype"].ToString()=="Nos")
								{
								%>
								<option Value="Rs"><font color=#8c4510>Rs</font></option>
								<%}
								else
								{
								%><option Value="Nos"><font color=#8c4510>Nos</font></option>
								<%
								}
								%>
								<option  selected Value="<%=rdr["discounttype"].ToString()%>"><%=rdr["discounttype"].ToString()%></option>
								</Select>
								</font>	
								<% }
								 else
								   {
								%>
									<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><Select name=dropscheme<%=Prod_No%>  disabled=true  style="width: 90">
								<option Value="Select"><font color=#8c4510>Select</font></option>
								<option Value="Primary"><font color=#8c4510>Primary</font></option>
								<option Value="Secondary"><font color=#8c4510>Secondary</font></option>
								</Select></font> </td>
									<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><input maxlength=8 disabled="true" type=text  size=10 name=txtPurRate<%=Prod_No%> onkeypress="return GetOnlyNumbers(this, event, false,true);" style="border-style:Groove; FONT-SIZE: 8pt;" ></font></td>
									
								
								<td bgcolor=#fff7e7>&nbsp;<font color=#8c4510><Select name=dropdiscount<%=Prod_No%> disabled=true>
								<option Value="Select"><font color=#8c4510>Select</font></option>
										<option Value="Nos"><font color=#8c4510>Nos</font></option>
										<option Value="Rs"><font color=#8c4510>Rs</font></option>
								</Select>
								</font>
								<% 
								}
								%>
								<td align=center  bgcolor=#fff7e7><font color=#8c4510><input type=checkbox name=chk<%=Prod_No%> onclick="enableText(this,document.f1.txtPurRate<%=Prod_No%>,document.f1.dropscheme<%=Prod_No%>,document.f1.dropdiscount<%=Prod_No%>);"></font></td>
							</tr>
							<%	Prod_No++;
							
								}
							%>
							
							<tr><td bgcolor=#fff7e7><font color=#8c4510><input type=hidden name=Total_Prod value=<%=Prod_No%>></font></td></tr>
							<tr><td colspan=5 align=right bgcolor=#fff7e7><font color=#8c4510><input type=hidden name=n1 onclick="Validate();"></font>
							<asp:Button ID=Btnsubmit1 Text=submit Runat=server OnClick="update" Width=80 OnLoad="Page_Load"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
							<td  align=right bgcolor=#fff7e7><font color=#8c4510>Select All</font></td><td align=center bgcolor=#fff7e7><input type=checkbox name=chkSelectAll onclick="selectAll();"></td></tr>
						</table>
					</td>
				</tr>
				
			</table><uc1:Footer id="Footer1" runat="server"></uc1:Footer>
		</form>
	</body>
</HTML>
<script language=C# runat =server >
		string uid;
		private void Page_Load(object sender, System.EventArgs e)
		//private void Page_Load()
		{
			try
			{
				
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:OilScheme.aspx,Method:Page_Load,   EXCEPTION "+ex.Message+"  userid  "+uid );
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			 if(!IsPostBack)
			{
				#region Check Privileges
				int i;
				string View_Flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="3";
				string SubModule="8";
				string[,] Priv=(string[,]) Session["Privileges"];
				for(i=0;i<Priv.GetLength(0);i++)
				{
					if(Priv[i,0]== Module &&  Priv[i,1]==SubModule)
					{						
						View_Flag=Priv[i,2];
						Add_Flag=Priv[i,3];
						Edit_Flag=Priv[i,4];
						Del_Flag=Priv[i,5];

						break;
					}
				}	
				Cache["Add"]=Add_Flag;
				if(View_Flag=="0")
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				if(Add_Flag=="0")
					Btnsubmit1.Enabled=false;
				#endregion
				CreateLogFiles.ErrorLog("Form:OilScheme.aspx,Method:Page_Load, userid  "+uid );
			}
			 
		}
		
		int Total_Product=0;
		public void update(Object sender, EventArgs e )
		//public void update()
		{  
		 try
		 {
		Validate();
		
			InventoryClass obj=new InventoryClass(); 
			
			string prod_cat="";
			int flag = 0;
			Total_Product=System.Convert.ToInt32(Request.Params.Get("Total_Prod"));
			
			//***********************************
					for(int i=0;i<Total_Product;i++)
			{ 
			
			if(Request.Params.Get("Chk"+i)!=null)
				{ 
					//obj.Eff_Date=DateTime.Now.Date.ToShortDateString();
					
					//obj.Product_Name=Request.Params.Get("lblProd_Name"+i); 
					//obj.Package_Type=Request.Params.Get("lblPack_Type"+i); 
					//prod_cat = Request.Params.Get("lblCat"+i);
					
					string sch=Request.Params.Get("dropscheme"+i); 
					string dis=Request.Params.Get("txtPurRate"+i); 
					string distype=Request.Params.Get("dropdiscount"+i);
					//if(sch.Equals("Select")||dis.Equals("")||distype.Equals("Select")) 
					if(sch.Equals("Select"))
					{
						MessageBox.Show("Please Fill Scheme Type");
						return;
					}
					if(dis.Equals(""))
					{
						MessageBox.Show("Please Fill Discount");
						return;
					}
					if(distype.Equals("Select"))
					{
						MessageBox.Show("Please Fill Discount Type");
						return;
					}
					//MessageBox.Show("Please Fill Scheme Type,Discount and Discount type of Checked Product");
					
					//obj.InsertSchemeUpdation(); 		
					//CreateLogFiles.ErrorLog("Form:Price_Updation.aspx,Method:update().   Price Updated of product " +Request.Params.Get("lblProd_Name"+i)+" User_ID: "+Session["User_Name"].ToString());
							
				}
			}
			///************************************/
			
			for(int i=0;i<Total_Product;i++)
			{ 
			
			if(Request.Params.Get("Chk"+i)!=null)
				{ 
					obj.Eff_Date=DateTime.Now.Date.ToShortDateString();
					
					obj.Product_Name=Request.Params.Get("lblProd_Name"+i); 
					obj.Package_Type=Request.Params.Get("lblPack_Type"+i); 
					prod_cat = Request.Params.Get("lblCat"+i);
					obj.schemetype=Request.Params.Get("dropscheme"+i); 
					obj.discount=Request.Params.Get("txtPurRate"+i); 
					obj.discounttype=Request.Params.Get("dropdiscount"+i); 
					obj.InsertSchemeUpdation(); 		
					//CreateLogFiles.ErrorLog("Form:Price_Updation.aspx,Method:update().   Price Updated of product " +Request.Params.Get("lblProd_Name"+i)+" User_ID: "+Session["User_Name"].ToString());
							
				}
				
			}
			int c=0;
			for(int i=0;i<Total_Product;i++)
			{ 
			if(Request.Params.Get("Chk"+i)==null)
					c++;
			}
			if(c==Total_Product)
			
			MessageBox.Show("Please Select CheckBox");
			else
			MessageBox.Show("Scheme Updated");
			CreateLogFiles.ErrorLog("Form:OilScheme.aspx,Method:btnUpdate, userid  "+uid );
				
		 }
		 catch(Exception ex)
		 {
		   CreateLogFiles.ErrorLog("Form:OilScheme.aspx,Method:btnUpdate,   EXCEPTION "+ex.Message+"  userid  "+uid );
		   //MessageBox.Show(ex.Message+ex.StackTrace);
		 }
					Object Add_Flag=Cache["Add"];
			/*Object Edit_Flag=Cache["Edit"];
			Object Del_Flag=Cache["Del"];*/
			if(System.Convert.ToString(Add_Flag)=="0")
			{
				Btnsubmit1.Enabled=false;
					
			}
			/*if(System.Convert.ToString(Edit_Flag)=="0")
				//btnEdit.Enabled=false;
			if(System.Convert.ToString(Del_Flag)=="0")
				//btnDelete.Enabled=false;*/
		}
		public void Validate(Object sender, EventArgs e)
		{
		int flag=0;
	  int AnyChecked=0;
	  //int f=f1.length;
	 // MessageBox.Show(f);
	  // DO	NOT ADD/REMOVE ANY COMPONENT FROM THIS FORM
	  // OTHERWISE CHANGE THE INCREMANTATION  FACTOR APPROPRIATELY
	  for(int i=0;i<61;i=i+7)
	  { 
	   if(Request.Params.Get("Chk"+i)!=null)
	 	{  
	       
	       AnyChecked=1;
	      //if(f.elements[i-3].value=="Select" || f.elements[i-2].value=="" || f.elements[i-1].value=="Select")
		 if(Request.Params.Get("dropscheme"+i)=="Select")
		 {
			MessageBox.Show("Please Fill Scheme Type,Discount and Discount type of Checked Product");
			return;
		 }
		}
	  }
		}
		
		
</script>