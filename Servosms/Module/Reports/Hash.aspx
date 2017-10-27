<%@ Page language="c#" Inherits="Servosms.Module.Reports.Hash" CodeFile="Hash.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Hash</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
	function ProdTable()
	{
		/*
		var data=new ActiveXObject("Scripting.Dictionary");
		int i=1;
		data.Add("1","Hello");
		data.Add("2","Indian");
		data.Add("3","Country");
		aa=(new VBArray(data.Items())).toArray()
		alert(aa)
		for(i in aa)
		{
		alert(aa[i])
		}*/
		//alert("aa")
		document.Form1.DropDownList1.ToolTip=document.Form1.DropDownList1.SelectedItem.value
	}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<input type="button" name="btn" onclick="ProdTable();" Width="70">
			<asp:datagrid id="DataGrid1" runat="server"></asp:datagrid>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 102; LEFT: 240px; POSITION: absolute; TOP: 88px" runat="server"></asp:TextBox>
			<asp:DropDownList id="DropDownList1" runat="server" Width="160px" Onmousedown="ProdTable();"></asp:DropDownList>
			<select class="ListBoxBorderStyle" id="DropProdName" style="BORDER-RIGHT: groove; BORDER-TOP: groove; BORDER-LEFT: groove; WIDTH: 245px; BORDER-BOTTOM: groove; BORDER-COLLAPSE: collapse; HEIGHT: 88px"
				name="DropProdName" type="select-one" multiple>
			</select>
			<asp:ListBox ID="dsf" Runat="server" Width="224px" BorderStyle="Groove" BorderWidth="0"></asp:ListBox>
		</form>
	</body>
</HTML>
