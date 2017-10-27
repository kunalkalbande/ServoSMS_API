<%@ Register TagPrefix="uc1" TagName="Footer" Src="../HeaderFooter/HomeFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header1" Src="../HeaderFooter/HomeHeader.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.LoginHome.HomePage" smartNavigation="False" CodeFile="HomePage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Home Page</title> 
		<!--%@ Register TagPrefix="uc1" TagName="Header" Src="Header.ascx" %-->  <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Sysitem/Styles.css" type="text/css" rel="stylesheet">
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
		<form method="post" runat="server">
			<uc1:header1 id="Header11" runat="server"></uc1:header1>
			<TABLE height="490" cellSpacing="0" cellPadding="0" width="1300" align="center">
				<TR>
					<TH align="center" colSpan="12">
						<hr>
						<asp:label id="lblMessaeg" runat="server"></asp:label></TH></TR>
				<TR>
					<TD colSpan="2"><STRONG><FONT color="#ce4848">Administration</FONT></STRONG></TD>
					<TD colSpan="2"><STRONG><FONT color="#ce4848">&nbsp;Employees</FONT></STRONG></TD>
					<TD colSpan="2"><STRONG><FONT color="#ce4848">Master</FONT></STRONG></TD>
					<TD colSpan="2"><STRONG><FONT color="#ce4848">Purchase / Sales/ Inventory</FONT></STRONG></TD>
					<TD colSpan="2"><STRONG><FONT color="#ce4848">Report/ MIS</FONT></STRONG></TD>
					<TD colSpan="2"><STRONG><FONT color="#ce4848">Report/ MIS</FONT></STRONG></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink5" runat="server" NavigateUrl="../Module/Admin/BackupRestore.aspx">BackUpRestore</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="HyperLink4" runat="server" NavigateUrl="../Module/Employee/Attandance_Register.aspx">Attendance Register</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink25" runat="server" NavigateUrl="../Module/Master/BeatMaster_Entry.aspx">Beat Entry</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink45" runat="server" NavigateUrl="../Module/Inventory/LY_PS_SALES.aspx">LY_PS_SALES</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink78" runat="server" NavigateUrl="../Module/Reports/AttendenceReport.aspx">Attendance Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink48" runat="server" NavigateUrl="../Module/Reports/PrimSecDiscount.aspx">Prim Sec Discount</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink9" runat="server" NavigateUrl="../Module/Admin/Privileges.aspx">Privileges</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="HyperLink2" runat="server" NavigateUrl="../Module/Employee/Employee_Entry.aspx"> Employee Entry </asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink24" runat="server" NavigateUrl="../Module/Master/Customer_Entry.aspx">Customer Entry</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink86" runat="server" NavigateUrl="../Module/Inventory/PerformaInvoice.aspx">Performa Invoice</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink111" runat="server" NavigateUrl="../Module/Reports/BackOrder_Report.aspx">BackOrder Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink83" runat="server" NavigateUrl="../Module/Reports/PrimarySecSalesAnalysis.aspx">Primary Sec Sales Analysis</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink441" runat="server" NavigateUrl="../Module/Admin/OrganisationDetails.aspx">Organization Details</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="HyperLink3" runat="server" NavigateUrl="../Module/Employee/Employee_List.aspx"> Employee List</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink23" runat="server" NavigateUrl="../Module/Master/Customer_List.aspx"> Customer List</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink20" runat="server" NavigateUrl="../Module/Inventory/Price_Updation.aspx">Price Updation</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink62" runat="server" NavigateUrl="../Module/Reports/BalanceSheet.aspx">Balance Sheet</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink101" runat="server" NavigateUrl="../Module/Reports/Prod_Promo_Dis_Claim_Report.aspx">Product Promo Scheme Report</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink7" runat="server" NavigateUrl="../Module/Admin/Roles.aspx">Roles</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="HyperLink12" runat="server" NavigateUrl="../Module/Employee/Leave_Register.aspx">Leave Application</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink92" runat="server" NavigateUrl="../Module/Master/CustomerMapping.aspx">Customer Mapping</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink22" runat="server" NavigateUrl="../Module/Inventory/Product_Entry.aspx"> Product Entry</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink69" runat="server" NavigateUrl="../Module/Reports/BankReconcillation.aspx">Bank Reconcilltion</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink44" runat="server" NavigateUrl="../Module/Reports/ProductReport.aspx">Product Report</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink8" runat="server" NavigateUrl="../Module/Admin/User_Profile.aspx">User Profile</asp:hyperlink></TD>
					<td></td>
					<TD><asp:hyperlink id="Hyperlink14" runat="server" NavigateUrl="../Module/Employee/Leave_Assignment.aspx">Leave Sanction</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink70" runat="server" NavigateUrl="../Module/Master/CustomerMechanicEntry.aspx">Customer Mechanic Entry </asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink18" runat="server" NavigateUrl="../Module/Inventory/PurchaseInvoice.aspx">Purchase Invoice</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink15" runat="server" NavigateUrl="../Module/Reports/BatchWiseStock.aspx">Batch Wise Stock</asp:hyperlink></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink41" runat="server" NavigateUrl="../Module/Reports/ProductWiseSales.aspx">Product Wise Sales</asp:hyperlink></b></TD>
				</TR>
				<TR>
					<TD colSpan="2"><STRONG><FONT color="#ce4848">Account</FONT></STRONG></TD>
					<TD></TD>
					<TD><asp:hyperlink id="HyperLink6" runat="server" NavigateUrl="../Module/Employee/Salary_Statement.aspx">Salary Statement</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink49" runat="server" NavigateUrl="../Module/Master/Customertype.aspx">Customer Type </asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink58" runat="server" NavigateUrl="../Module/Inventory/PurchaseReturn.aspx">Purchase Return</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink26" runat="server" NavigateUrl="../Module/Reports/BatchWiseStockLedger.aspx">Batch Wise Stock Ledger</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink13" runat="server" NavigateUrl="../Module/Reports/ProfitAnalysis1.aspx">Profit Analysis</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink40" runat="server" NavigateUrl="../Module/Accounts/Ledger.aspx">Ledger Creation</asp:hyperlink></TD>
					<TD colSpan="2"><asp:hyperlink id="Hyperlink107" runat="server" NavigateUrl="../Module/Employee/OT_Compensation.aspx">OT Compensation</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink81" runat="server" NavigateUrl="../Module/Master/CustomerUpdation.aspx">Customer Updation</asp:hyperlink></TD>
					<td></td>
					<td><asp:hyperlink id="Hyperlink60" runat="server" NavigateUrl="../Module/Inventory/OrderCollection.aspx">Order Collection</asp:hyperlink></td>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink103" runat="server" NavigateUrl="../Module/Reports/StockMovement_Batch.aspx">Batch Wise Stock Movement</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink31" runat="server" NavigateUrl="../Module/Reports/ProposedLubeIndent.aspx">Proposed Lube Indent</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:hyperlink id="HyperLink51" runat="server" NavigateUrl="../Module/Accounts/Payment.aspx"> Payment</asp:hyperlink></TD>
					<TD></TD>
					<TD><STRONG><FONT color="#ce4848">Logistics</FONT></STRONG></TD>
					<TD></TD>
					<td><asp:hyperlink id="Hyperlink10" runat="server" NavigateUrl="../Module/Inventory/Fleet_OEDiscountEntry.aspx">Fleet/OE Discount Entry</asp:hyperlink></td>					
					<TD></TD>
                    <TD><asp:hyperlink id="Hyperlink17" runat="server" NavigateUrl="../Module/Inventory/SalesInvoice.aspx">Sales Invoice</asp:hyperlink></TD>
                    <TD></TD>
					<TD><asp:hyperlink id="Hyperlink82" runat="server" NavigateUrl="../Module/Reports/ClaimAnalysis.aspx">Claim Analysis</asp:hyperlink></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink11" runat="server" NavigateUrl="../Module/Reports/PurchaseBook.aspx">Purchase Book</asp:hyperlink></b></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink21" runat="server" NavigateUrl="../Module/Inventory/Payment_Receipt.aspx"> Receipt</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink56" runat="server" NavigateUrl="../Module/Logistics/routeedit.aspx"> Route Master</asp:hyperlink></TD>
					<TD></TD>
					<td><asp:hyperlink id="Hyperlink75" runat="server" NavigateUrl="../Module/Master/MarketCustEntry.aspx">Market Customer Entry</asp:hyperlink></td>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink55" runat="server" NavigateUrl="../Module/Inventory/SalesReturn.aspx">Sales Return</asp:hyperlink></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink79" runat="server" NavigateUrl="../Module/Reports/CreditPeriodAnalysisSheetReport.aspx">Credit Analysis Sheet</asp:hyperlink></b></TD>
					<TD></TD>                    
					<TD><asp:hyperlink id="Hyperlink29" runat="server" NavigateUrl="../Module/Reports/PurchaseListIOCL.aspx">Purchase Statment IOCL</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink52" runat="server" NavigateUrl="../Module/Accounts/voucher.aspx">Voucher Entry</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink50" runat="server" NavigateUrl="../Module/Logistics/Vehicle_logbook.aspx"> Vehicle Daily Log Book </asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink100" runat="server" NavigateUrl="../Module/Master/Prod_Promo_Dis_Entry.aspx">Prod Promo Dis Entry</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink59" runat="server" NavigateUrl="../Module/Inventory/StockAdjustment.aspx">Stock Adjustment</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink43" runat="server" NavigateUrl="../Module/Reports/Customer_Bill_Ageing.aspx">Customer Bill Ageing</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink112" runat="server" NavigateUrl="../Module/Reports/Return_Report.aspx">Pur./Sales Return Report</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink57" runat="server" NavigateUrl="../Module/Logistics/Vechile_entryform.aspx"> Vehicle Entry</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink93" runat="server" NavigateUrl="../Module/Master/salesPersonAssignment.aspx">Sales Person Assignment</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink110" runat="server" NavigateUrl="../Module/Reports/Back_Order_Process.aspx">Back Order Process</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink66" runat="server" NavigateUrl="../Module/Reports/CustomerDataMining.aspx">Customer Data Mining</asp:hyperlink></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink98" runat="server" NavigateUrl="../Module/Reports/Quaterly_Targets.aspx">Quaterly Targets Report</asp:hyperlink></b></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink19" runat="server" NavigateUrl="../Module/Inventory/SchemeDiscountentry.aspx">Scheme/Discount Entry</asp:hyperlink></TD>
					<TD></TD>
					<TD>&nbsp;</TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink42" runat="server" NavigateUrl="../Module/Reports/CustomerWiseOutstanding.aspx">Customer Outstanding</asp:hyperlink></b></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink108" runat="server" NavigateUrl="../Module/Reports/Reward_Report.aspx">Ro-Bazzar Reward Report</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink96" runat="server" NavigateUrl="../Module/Inventory/ServoStockistDiscountEntry.aspx">Servo Stkt Discount Entry</asp:hyperlink></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink77" runat="server" NavigateUrl="../Module/Reports/Salesreport1.aspx">Customer Sales Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink32" runat="server" NavigateUrl="../Module/Reports/ServoSadbhavnalist.aspx">Sadbhavna Enrolment List</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink27" runat="server" NavigateUrl="../Module/Master/Supplier_Entry.aspx">Vendor Entry</asp:hyperlink></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink68" runat="server" NavigateUrl="../Module/Reports/DayBookReport.aspx">Day Book Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink33" runat="server" NavigateUrl="../Module/Reports/SadbhavnaSchemeMonthWise.aspx">Sadbhavna Monthly Points</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink1" runat="server" NavigateUrl="../Module/Master/Supplier_List.aspx">Vendor List</asp:hyperlink></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink80" runat="server" NavigateUrl="../Module/Reports/DistrictWiseChannelSales.aspx">District Wise Channel Sales</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink36" runat="server" NavigateUrl="../Module/Reports/SadbhavnaSchemeYearWise.aspx">Sadbhavna Yearly Point</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink91" runat="server" NavigateUrl="../Module/Reports/DocumentCancelReport.aspx">Document Cancel Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink35" runat="server" NavigateUrl="../Module/Reports/SaleBook.aspx">Sales Book</asp:hyperlink></b></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink102" runat="server" NavigateUrl="../Module/Reports/Expences_Details.aspx">Expences Details Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink73" runat="server" NavigateUrl="../Module/Reports/Schemelist.aspx">Scheme List</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink30" runat="server" NavigateUrl="../Module/Reports/FleetOeDiscountReport.aspx">Fleet OE Disc. Sheet</asp:hyperlink></b></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink28" runat="server" NavigateUrl="../Module/Reports/Claimsheet.aspx">Secondory Sales Claim</asp:hyperlink></b></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink105" runat="server" NavigateUrl="../Module/Reports/FOC_Management_Report.aspx">FOC Management Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink95" runat="server" NavigateUrl="../Module/Reports/SchemeSecondrySales.aspx">Special Scheme Report</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink34" runat="server" NavigateUrl="../Module/Reports/LeaveReport.aspx">Leave Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink109" runat="server" NavigateUrl="../Module/Reports/SRSM_Format_Report.aspx">SRSM Report</asp:hyperlink></b></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="HyperLink64" runat="server" NavigateUrl="../Module/Reports/LedgerReport.aspx">Ledger Report</asp:hyperlink></b></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink87" runat="server" NavigateUrl="../Module/Reports/SSRIncentiveSheet.aspx">SSR Incentive Sheet</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink67" runat="server" NavigateUrl="../Module/Reports/LubeIndent.aspx">Lube Indent Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink88" runat="server" NavigateUrl="../Module/Reports/SSRPerforma.aspx">SSR Performance Sheet</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink46" runat="server" NavigateUrl="../Module/Reports/LY_PS_SALESReport.aspx">LY_PS_SALES Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink94" runat="server" NavigateUrl="../Module/Reports/SSRWiseSales.aspx">SSR Wise Sales Figure</asp:hyperlink></b></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink76" runat="server" NavigateUrl="../Module/Reports/Marketpotentialreport.aspx">Market Potential Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink97" runat="server" NavigateUrl="../Module/Reports/SSRWiseTargets.aspx">SSR Wise Targets Report</asp:hyperlink></b></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink74" runat="server" NavigateUrl="../Module/Reports/districtWiseReport.aspx">Master List</asp:hyperlink></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="HyperLink63" runat="server" NavigateUrl="../Module/Reports/StockLedgerReport.aspx">Stock Ledger Report</asp:hyperlink></b></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink72" runat="server" NavigateUrl="../Module/Reports/MechanicReport.aspx" Width="115px">Mechanic Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink38" runat="server" NavigateUrl="../Module/Reports/StockMovement.aspx">Stock Movement</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="HyperLink53" runat="server" NavigateUrl="../Module/Reports/CustomerwiseSalesReport.aspx">Mont. Cust. Sec. Sales Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink65" runat="server" NavigateUrl="../Module/Reports/StockReorderReport.aspx">Stock Reordering Report</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink71" runat="server" NavigateUrl="../Module/Reports/Monwiseprodsal.aspx">Mont. Prod. Secon. Sales</asp:hyperlink></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink39" runat="server" NavigateUrl="../Module/Reports/StockReport.aspx">Stock Report</asp:hyperlink></b></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink47" runat="server" NavigateUrl="../Module/Reports/MonthlyClaimLatter.aspx">MonthlyClaimLetter</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink84" runat="server" NavigateUrl="../Module/Reports/TargetVsAchivement.aspx">Target Vs Achievement</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink113" runat="server" NavigateUrl="../Module/Reports/HSD_MS_Report.aspx">MS / HSD Report</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink89" runat="server" NavigateUrl="../Module/Reports/TradingAccount.aspx">Trading Account</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink106" runat="server" NavigateUrl="../Module/Reports/NillSellingRO_Report.aspx">Nill / Selling  Customer</asp:hyperlink></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink54" runat="server" NavigateUrl="../Module/Reports/VAT_Report.aspx">VAT Report</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink16" runat="server" NavigateUrl="../Module/Reports/PartyWiseSalesFigure.aspx">Party Wise Sales Figure</asp:hyperlink></b></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink90" runat="server" NavigateUrl="../Module/Reports/VehicleLogReport.aspx">Vehicle Log Book Report</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink85" runat="server" NavigateUrl="../Module/Reports/PriceCalculation.aspx">Price Calculation</asp:hyperlink></b></TD>
					<TD></TD>
					<TD><asp:hyperlink id="Hyperlink99" runat="server" NavigateUrl="../Module/Reports/Yearly_Targets.aspx">Yearly Targets Report</asp:hyperlink></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink37" runat="server" NavigateUrl="../Module/Reports/PriceList.aspx">Price List</asp:hyperlink></b></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink104" runat="server" NavigateUrl="../Module/Reports/FourTZoom_Report.aspx">4T Sales Report</asp:hyperlink></b></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD><b><asp:hyperlink id="Hyperlink115" runat="server" NavigateUrl="../Module/Reports/VATRDReport.aspx">VATRDReport</asp:hyperlink></b></TD>
				</TR>
			</TABLE>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
