<%@ Page language="c#" Inherits="Servosms.Module.Inventory.ChildWinCAS" CodeFile="ChildWinCAS.aspx.cs" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="DBOperations"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Group</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript><LINK href="../../Sysitem/Styles.css" type=text/css rel=stylesheet >
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
<script language=javascript>

function getclose()
{
	window.close();
}

function selectAll()
{
	var f=document.Form1
	if(f.chkSelectAll.checked)
		for(var i=0;i<f.length;i++)
			f.elements[i].checked=true
	else
		for(var i=0;i<f.length;i++)
			f.elements[i].checked=false
}
</script>
  </HEAD>
<body>
<form id=Form1 method=post runat="server">
<input id="tempinfo" runat=server style="WIDTH: 1px" type="hidden" name="tempinfo" runat="server">

      
      <%
      try
      {
			%>
			<table cellSpacing=0 cellPadding=0 border=1 width=160>
						<tr><th class=fontstyle colSpan=3><font color=#ce4848>Select Group</FONT><hr SIZE=0></TH></TR>
						<tr><th class=fontstyle><font color=#000000>SN</FONT></TH>
						<th class=fontstyle><font color=#000000>Group Name</FONT></TH>
						<th class=fontstyle><font color=#000000>Select</FONT></TH></TR>
				<%
				EmployeeClass obj=new EmployeeClass();
				SqlDataReader SqlDtr;
				string sql;
				int Row_No=0;
				int i=1;
				int flage=0;
				sql="select distinct group_Name from customertype";
				SqlDtr=obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					flage=0;
					if(SqlDtr["group_Name"].ToString()!=null && SqlDtr["group_Name"].ToString()!="")
					{
						%>
						<tr><td>&nbsp;<%=i%></td>
							<td>&nbsp;<%=SqlDtr["group_Name"].ToString()%><input type=hidden name=tempvalue<%=i%>></td>
							<%
							{
								%>
								<td>&nbsp;<input type=checkbox value="'<%=SqlDtr["group_Name"].ToString()%>'" name=chk<%=i%> checked=true></td></tr>
								<%
							}
							i++;
					}
				}
				SqlDtr.Close();
				Tot_Rows=i;
			}
			catch(Exception ex)
			{
			
			}
		%>
      <tr><th colspan=2 align=right>Select All&nbsp;&nbsp;</th><td align=left>&nbsp;<input type=checkbox name=chkSelectAll onClick="selectAll();"></td></tr>
					<tr><td colspan=3 align=center><asp:Button ID=btnSubmit Runat=server Text=Submit 
								 onclick="btnSubmit_Click"></asp:Button></td></tr></TABLE></FORM>
	</body>
</HTML>
