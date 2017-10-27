<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Import namespace="Servosms.Sysitem.Classes" %>
<%@ import namespace="System.Data.SqlClient" %>
<%@ import namespace="RMG" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.Prod_Promo_Dis_Claim_Report" CodeFile="Prod_Promo_Dis_Claim_Report.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Prod_Promo_Dis_Claim_Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" cellSpacing="0" cellPadding="0" width="778" align="center" border="0">
				<tbody>
					<tr vAlign="top" height="20">
						<th colSpan="5">
							<font color="#ce4848">Product Promotion Scheme Discount Claim Report</font>
							<hr>
						</th>
					</tr>
					<tr vAlign="top">
						<td align="center">
							<table cellSpacing="0" align="center" border="0">
								<tr height="20">
									<td align="center">Date From</td>
									<td><asp:textbox id="txtDateFrom" runat="server" ReadOnly="True" Width="70px" BorderStyle="Groove"
											CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></td>
									<td>To</td>
									<td><asp:textbox id="txtDateTo" runat="server" ReadOnly="True" Width="70px" BorderStyle="Groove"
											CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></td>
									<td colSpan="3">&nbsp;&nbsp;<asp:button id="btnview" runat="server" Width="60px" Text="View" 
											onclick="btnview_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnprint" runat="server" Width="60px" Text="Print"
											onclick="btnprint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" runat="server" Width="60px" Text="Excel"
											 onclick="btnExcel_Click"></asp:button></td>
								</tr>
								<tr>
									<td colspan="7">Scheme Name&nbsp;&nbsp;<asp:DropDownList CssClass="dropdownlist" Width="250px" ID="DropSchemName" Runat="server"></asp:DropDownList></td>
								</tr>
								<%
								if(flage==1)
								{
									%>
								<tr>
									<td align="center" colSpan="7">
										<table borderColor="#deba84" cellSpacing="0" align="center" border="1">
											<%             Qty_Tot=0;
                                                Qty_Tot_ltr=0;
                                                string  sql="";
                                                int flage1=0;
                                                if(DropSchemName.SelectedIndex==0)
                                                    //Coment by Vikas 01.08.09 sql="select sp.supp_name,pm.vndr_invoice_no,pm.vndr_invoice_date,p.prod_name+':'+p.pack_type,pd.qty from supplier sp,purchase_master pm,purchase_details pd,products p where pm.invoice_no=pd.invoice_no and sp.supp_id=pm.vendor_id and p.prod_id=pd.prod_id  and pm.vndr_invoice_date>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and pm.vndr_invoice_date<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and pd.prod_id in (select prodid from Prod_Promo_Grade_Entry where datefrom >='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and dateto<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"') order by p.prod_name,p.pack_type";
                                                    sql="select sp.supp_name,pm.vndr_invoice_no,pm.vndr_invoice_date,p.prod_name+':'+p.pack_type,pd.qty from supplier sp,purchase_master pm,purchase_details pd,products p where pm.invoice_no=pd.invoice_no and sp.supp_id=pm.vendor_id and p.prod_id=pd.prod_id  and pm.vndr_invoice_date>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"])+"' and pm.vndr_invoice_date<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"])+"' and pd.prod_id in (select prodid from Prod_Promo_Grade_Entry where datefrom <='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"])+"' and prodid in(select prodid from Prod_Promo_Grade_Entry where dateto >='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"])+"')) order by p.prod_name,p.pack_type";
									else
										//Coment by Vikas 01.08.09 sql="select sp.supp_name,pm.vndr_invoice_no,pm.vndr_invoice_date,p.prod_name+':'+p.pack_type,pd.qty from supplier sp,purchase_master pm,purchase_details pd,products p where pm.invoice_no=pd.invoice_no and sp.supp_id=pm.vendor_id and p.prod_id=pd.prod_id  and pm.vndr_invoice_date>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and pm.vndr_invoice_date<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and pd.prod_id in (select prodid from Prod_Promo_Grade_Entry where schname='"+DropSchemName.SelectedItem.Value.ToString()+"' and datefrom >='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and dateto<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"') order by p.prod_name,p.pack_type";
										sql="select sp.supp_name,pm.vndr_invoice_no,pm.vndr_invoice_date,p.prod_name+':'+p.pack_type,pd.qty from supplier sp,purchase_master pm,purchase_details pd,products p where pm.invoice_no=pd.invoice_no and sp.supp_id=pm.vendor_id and p.prod_id=pd.prod_id  and pm.vndr_invoice_date>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"])+"' and pm.vndr_invoice_date<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"])+"' and pd.prod_id in (select prodid from Prod_Promo_Grade_Entry where schname='"+DropSchemName.SelectedItem.Value.ToString()+"' and datefrom <='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"])+"' and prodid in(select prodid from Prod_Promo_Grade_Entry where schname='"+DropSchemName.SelectedItem.Value.ToString()+"' and dateto >='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"])+"')) order by p.prod_name,p.pack_type";

                                                    InventoryClass obj = new InventoryClass();
                                                    SqlDataReader rdr = obj.GetRecordSet(sql);
                                                    if(rdr.HasRows)
                                                    {
										%>
										<tr bgColor="#ce4848">
												<th>
													<font color="white">Vendor Name</font></th>
												<th>
													<font color="white">Invoice No.</font></th>
												<th>
													<font color="white">Invoice Date</font></th>
												<th>
													<font color="white">Product Name</font></th>
												<th>
													<font color="white">Qty in Nos.</font></th>
												<th>
													<font color="white">Qty in Liter</font></th></tr>
										<%
									}
									else
									{
										MessageBox.Show("Data Not Available");
									}
									while(rdr.Read())
									{
										%>
											<tr>
												<td><%=rdr.GetValue(0).ToString()%></td>
												<td><%=rdr.GetValue(1).ToString()%></td>
												<td><%=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr.GetValue(2).ToString()))%></td>
												<td><%=rdr.GetValue(3).ToString()%></td>
												<td align="right"><%=rdr.GetValue(4).ToString()%></td>
												<td align="right"><%=Qtyinltr(rdr.GetValue(3).ToString(),rdr.GetValue(4).ToString())%></td>
												<%Qty_Tot+=Convert.ToDouble(rdr.GetValue(4).ToString());%>
											</tr>
											<%
										flage1=1;
									}
									rdr.Close();
									if(flage1==1)
									{
										%>
											<tr bgColor="#ce4848">
												<th colSpan="4">
													<font color="white">Total</font></th>
												<th align="right">
													<font color="white">
														<%=Qty_Tot%>
													</font>
												</th>
												<th align="right">
													<font color="white">
														<%=Qty_Tot_ltr%>
													</font>
												</th>
											</tr>
										<%
									}
								}
								%>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</tbody>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
