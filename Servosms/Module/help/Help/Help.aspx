<%@ Page Language="c#" Inherits="Servosms.Module.help.Help.Help" CodeFile="Help.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<HTML>
	<HEAD>
		<title>Servosms: User Manual</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="False" name="vs_showGrid">
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<style type="text/css"> <!-- .t { font-family: Arial, Helvetica, sans-serif, "Arial Black"; font-size: 12px; font-weight: normal; color: #000000; text-decoration: none; }
	.h { font-family: Arial, Helvetica, sans-serif, "Arial Black"; font-size: 18px; font-weight: bold; color: #990000; text-decoration: underline; }
	.f { font-family: Arial, Helvetica, sans-serif, "Arial Black"; font-size: 14px; font-weight: bold; color: #FF9900; text-decoration: underline; }
	.uh { font-family: Arial, Helvetica, sans-serif, "Arial Black"; font-size: 12px; font-weight: bold; color: #000066; }
	.uhCopy { font-family: Arial, Helvetica, sans-serif, "Arial Black"; font-size: 12px; font-weight: bold; color: #000000; }
	.head { font-family: Arial, Helvetica, sans-serif, "Arial Black"; font-size: 14px; font-weight: bold; color: #CC0000; }
	.hl { font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #990000; text-decoration: none; }
	a.hl:hover { font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #FF9900; }
	--></style>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<table cellSpacing="1" cellPadding="0" width="90%" align="center" border="0">
			<tr>
				<td>
					<div class="h" align="center">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="h" width="90%">
									<div align="center">SERVO Stockist Management System : Servosms</div>
								</td>
								<td vAlign="baseline" width="10%">
									<div class="hl" align="right"><font color="#990000"><A class="hl" href="../../../LoginHome/HomePage.aspx">Home 
												Page </A></font>
									</div>
								</td>
							</tr>
						</table>
					</div>
				</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>
					<div class="f" align="center">
						<p class="f">User Manual
						</p>
					</div>
				</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="uh"><a name="toc"></a>Table Of Contents</td>
			</tr>
			<tr>
				<td class="uh">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">1.0 <A href="#intro">Introduction</A></td>
			</tr>
			<tr>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">2.0 <A href="#lp">The Login Process</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.1 <A href="#aa">Authentication 
						and Authorization</A></td>
			</tr>
			<tr>
				<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="uhCopy">2.1.1 <A href="#authenticate">Authentication</A></span></td>
			</tr>
			<tr>
				<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="uhCopy">2.1.2 <A href="#authorize">Authorization</A></span></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.2 <A href="#asm">Additional 
						Security Measures</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.2.1
					<A href="#se">Session Expiration</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.2.2
					<A href="#el">Event Logger</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.3 <A href="#user">Default 
						User Type, Login Name and Password</A></td>
			</tr>
			<tr>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">3.0 <A href="#gs">Getting Started</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.1 <A href="#ser">Starting 
						Servosms</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.2 <A href="#ls">Login 
						Screen</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.3 <A href="#hp">Home 
						Page</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.3.1
					<A href="#lo">Logout</A></td>
			</tr>
			<tr>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">4.0 <A href="#ad">Administrative Module</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.1 <A href="#roles">Roles</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.1.1
					<A href="#nr">Create a new Role</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.1.2
					<A href="#dr">Update or Delete a Role</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2 <A href="#up">User 
						Profile</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2.1
					<A href="#np">Create a new Profile</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2.2
					<A href="#dp">Update or Delete a Profile</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.3 <A href="#prv">Privileges</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.3.1
					<A href="#aprv">Allocate Privileges to a User</A></td>
			</tr>
			<tr>
				<td class="uhCopy" height="15">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.3.2
					<A href="#uprv">Update Privileges of a User</A>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.4 <A href="#org">Organization 
						Details</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.5 <A href="#br">Backup 
						&amp; Restore</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">5.0 <A href="#ac">Accounts</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.1 <A href="#rct">Receipt</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.2 <A href="#pay">Payment</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.2.1
					<A href="#mpay">Make a Payment</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.2.2
					<A href="#dpay">Edit or Delete an existing Payment</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.3 <A href="#vcr">Voucher 
						Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.3.1
					<A href="#nvcr">Create a new Voucher</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.3.2
					<A href="#dvcr">Edit or Delete an existing Voucher</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.4 <A href="#lc">Ledger 
						Creation</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.4.1
					<A href="#nl">Create a new Ledger</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.4.2
					<A href="#dl">Edit or Delete an existing Ledger</A></td>
			</tr>
			<tr>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">6.0 <A href="#sae">Shift and Employees</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.1 <A href="#ee">Employee 
						Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.2 <A href="#empl">Employee 
						List</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.2.1
					<A href="#semp">Search Employees</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.2.2
					<A href="#eemp">Edit an Employee</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.2.3
					<A href="#demp">Delete an Employee</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.3 <A href="#ss">Salary 
						Statement</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.4 <A href="#areg">Attendance 
						Register</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.5 <A href="#lapp">Leave 
						Application</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.6 <A href="#lsanc">Leave 
						Sanction</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.7 <A href="#lsleave">Leave 
						Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">7.0 <A href="#logistics">Logistics</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.1 <A href="#ve">Vehicle 
						Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.1.1
					<A href="#nve">Add a new Vehicle Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.1.2
					<A href="#dve">Edit or Delete an existing Vehicle Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.2 <A href="#vdlb">Vehicle 
						Daily Log Book</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.2.1
					<A href="#nvdlb">Add a new Vehicle Log Book Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.2.2
					<A href="#dvdlb">Edit or Delete an existing Vehicle Log Book Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.3 <A href="#rm">Route 
						Master</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">8.0 <A href="#part">Master</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.1 <A href="#be">Beat 
						Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.1.1
					<A href="#nb">Create a new Beat</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.1.2
					<A href="#eb">Edit a Beat</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.1.3
					<A href="#db">Delete a Beat</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.2 <A href="#ce">Customer 
						Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.3 <A href="#sde">Scheme 
						Discount Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.3.1
					<A href="#nsde">Add a new Scheme Discount Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.3.2
					<A href="#dsde">Edit an existing Scheme Discount Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.4 <A href="#mce">Market 
						Customer Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.4.1
					<A href="#nmce">Add a new Market Customer Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.4.2
					<A href="#emce">Edit an existing Market Customer Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.5 <A href="#ven">Vendor 
						Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.6 <A href="#cl">Customer 
						List</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.6.1
					<A href="#lcmr">List all Customer</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.6.2
					<A href="#ec">Edit a Customer</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.6.3
					<A href="#dc">Delete a Customer</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.7 <A href="#vl">Vendor 
						List</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.7.1
					<A href="#ev">Edit a Vendor</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.7.2
					<A href="#dv">Delete a Vendor</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.8 <A href="#cdm">Customer 
						Data Mining</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.9 <A href="#cme">Customer 
						Mechanic Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.9.1
					<A href="#ncme">Add a new Customer Mechanic Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.9.2
					<A href="#ecme">Edit an existing Customer Mechanic Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.10 <A href="#cte">Customer 
						Type Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.10.1
					<A href="#ncte">Add a new Customer Type Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.10.2
					<A href="#ecte">Edit an existing Customer Type Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.11 <A href="#fde">Fleet/OE 
						Discount Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.11.1
					<A href="#nfde">Add a new Fleet/OE Discount Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.11.2<A href="#efde">
						Edit an existing Fleet/OE Discount Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.12 <A href="#custupd">Customer 
						Updation</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.13 <A href="#custmap">Customer 
						Mapping</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.14 <A href="#salesperass">
						Sales Person Assignment</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.15 <A href="#ssde">Servo 
						Stockist Discount Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.15.1
					<A href="#nssde">Add a new Servo Stockist Discount Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.15.2
					<A href="#essde">Edit an existing Scheme Discount Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">9.0 <A href="#psi">Purchase/Sales/Inventory</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.1 <A href="#pe">Product 
						Entry</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.1.1
					<A href="#npro">Enter a new Product</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.1.2
					<A href="#ep">Edit an existing Product</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.2 <A href="#pu">Price 
						Updation</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.3 <A href="#pi">Purchase 
						Invoice</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.3.1
					<A href="#npi">Create a new Purchase Invoice</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.3.2
					<A href="#epi">Edit an existing Purchase Invoice</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.4 <A href="#si">Sales 
						Invoice</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.4.1
					<A href="#nsi">Create a new Sales Invoice</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.4.2
					<A href="#esi">Edit an existing Sales Invoice</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.5 <A href="#lypssales">Py_Ps_Sales</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.6 <A href="#sa">Stock 
						Adjustment</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.7 <A href="#sr">Sales 
						Return</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.8 <A href="#pre">Purchase 
						Return</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.9 <A href="#clu">Customer 
						Ledger Updation</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.10 <A href="#slu">Stock 
						Ledger Updation</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.11 <A href="#Perinvoice">
						Performa Invoice</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.12 <A href="#Ordercol">Order 
						Collection</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.12.1
					<A href="#noi">Create a new Order Invoice</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.12.2
					<A href="#eoi">Edit an existing Order Invoice</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.13 <A href="#modvatcenvat">
						Mod Vat / Cen Vat Invoice</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">10.0 <A href="#report">Reports/MIS</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.1 <A href="#cws">Customer 
						Wise Sales</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.2 <A href="#vlbr">Vehicle 
						Log Book Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.3 <A href="#slr">Stock 
						Ledger Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.4 <A href="#lr">Ledger 
						Report</A>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.5 <A href="#sl">Scheme 
						List</A>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.6 <A href="#pl">Price 
						List</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.7 <A href="#mpsr">Monthly 
						Product Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.8 <A href="#csr">Customer 
						Sales Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.9 <A href="#mpr">Market 
						Potential Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.10 <A href="#vr">VAT 
						Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.11 <A href="#ta">Trading 
						Account</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.12 <A href="#sm">Stock 
						Movement</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.13 <A href="#srepo">Stock 
						Report</A>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.14 <A href="#sb">Sales 
						Book</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.15 <A href="#pb">Purchase 
						Book</A>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.16 <A href="#pws">Product 
						Wise Sales</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.17 <A href="#co">Customer 
						Outstanding</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.18 <A href="#cba">Customer 
						Bill Ageing</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.19 <A href="#dwr">District 
						Report</A>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.20 <A href="#mr">Mechanic 
						Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.21 <A href="#bs">Balance 
						Sheet</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.22 <A href="#pa">Profit 
						Analysis</A>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.23 <A href="#1">Claim 
						Sheet with Stock Movement Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.24 <A href="#2">Fleet/OE 
						Discount Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.25 <A href="#3">Primary/Secondary 
						Sales Discount Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.26 <A href="#4">Purchase 
						List for IOCL</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.27 <A href="#5">Servo 
						Sadbhavna Enrollment List</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.28 <A href="#6">Servo 
						Sadbhavna Scheme Month Wise Sales &amp; Points Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.29 <A href="#29">Servo 
						Sadbhavna Scheme Year Wise Sales &amp; Points Report for Disbursement of Gifts</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.30 <A href="#30">Leave 
						Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.31 <A href="#31">Product 
						Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.32 <A href="#32">Ly_Ps_Sales 
						Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.33 <A href="#33">Party 
						Wise Sales Figure</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.34 <A href="#34">Monthly 
						Claim Letter Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.35 <A href="#35">Attendance 
						Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.36 <A href="#36">Bank 
						Reconcillation</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.37 <A href="#37">Batch 
						Wise Stock</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.38 <A href="#38">Batch 
						Wise Stock Ledger Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.39 <A href="#39">Credit 
						Period Analysis Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.40 <A href="#40">Day 
						Book Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.41 <A href="#41">District 
						Wise Channel Sales Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.42 <A href="#42">Lube 
						Indent Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.43 <A href="#43">Proposed 
						Lube Indent Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.44 <A href="#44">Stock 
						Reorder Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.45 <A href="#45">Claim 
						Analysis Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.46 <A href="#46">Primary 
						Sec. Sales Analysis Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.47 <A href="#47">Target 
						Vs Achievement Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.48 <A href="#48">Price 
						Calculation Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.49 <A href="#49">SSR 
						Incentive Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.50 <A href="#50">SSR 
						Performance</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.51 <A href="#51">SSR 
						Wise Sales Figure</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.52 <A href="#52">Document 
						Cancel Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.53 <A href="#53">Scheme 
						Secondry Sales Report</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">11.0 <A href="#pwser">How to start the Print_WindowsService</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">12.0 <A href="#InvoiceDesigner">How to design Sales Invoice print 
						layout With the help of InvoiceDesigner.exe</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">13.0 <A href="#logout">Logout</A></td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="intro"></a>1.0</font> Introduction</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy" vAlign="top">
					<div align="center"><IMG class="head" src="images/Forms/Login.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div class="uh" align="center">Figure 1.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Welcome to <strong>Servosms</strong> the user-friendly and most 
						comprehensive solution for <strong>Stockist/Distributor </strong>from <strong>bbnisys</strong>
						<strong>Technologies</strong>. <strong>Servosms</strong> provides the state-of 
						-the-art technology and is dedicated to serving the needs of Distribution and 
						Stockist Management by delivering the ultimate tool to run a better business.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="lp"></a>2.0</font> The Login 
					Process</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="aa"></a>2.1</font> Authentication 
					and Authorization</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">The Login process is the <strong>entry </strong>point into the 
						Servosms System. It performs <strong>authentication</strong> and <strong>authorization</strong>
						so that only valid and authentic user gain access to the <strong>Servosms</strong>
						System and safe guard the integrity of your confidential data.
					</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="authenticate"></a>2.1.1</font> Authentication</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">The <strong>Authentication</strong> process is meant for the <strong>
							Servosms</strong> system to check whether a <strong>valid</strong> user is 
						trying to gain access to the system, invalid users should not be allowed to 
						proceed and use the system. <strong>Authentication</strong> requires you to 
						select the Type of User and then input the Login name and Password. Each user 
						should be given a unique Login Name and Password, this step should be performed 
						by the <strong>administrator</strong> only, except that the administrator 
						should allow the user to input a password of their <strong>choice</strong>. 
						Users should not <strong>disclose</strong> their password to others, should the 
						password become known, you should immediately ask the administrator to help you <strong>
							change</strong> the <strong>old</strong> password.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="authorize"></a>2.1.2</font> Authorization</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">After authentication comes <strong>Authorization</strong>, 
						this process is meant for <strong>Servosms</strong> to check and keep track 
						which user has what <strong>rights</strong> and <strong>privileges</strong> to 
						access different part of the system. Not all users of <strong>Servosms</strong> can 
						have the same privileges, an <strong>Administrator</strong> has <strong>all</strong>
						the rights and privileges to the system, compared to an administrator, an <strong>accountant</strong>
						should have limited access to the Servosms System, i.e. should only access the <strong>
							Accounts</strong> Module.</div>
					<p align="justify">The <strong>Servosms</strong> System provides <strong>Roles</strong>
						and <strong>Privileges</strong> to authorize users to gain access to only their 
						modules and further restricts the user from operation like <strong>add</strong>,
						<strong>view</strong>, <strong>edit</strong>, and <strong>delete</strong>.
					</p>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="asm"></a>2.2</font> Additional 
					Security Measures</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Authentication and Authorization is not the end of the story, 
						further <strong>security measures</strong> are still required to safe guard the 
						integrity of your confidential data.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="se"></a>2.2.1</font> Session 
					Expiration</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Even after a valid user has been authenticated and authorized, 
						chances are that the user may stay away from the desktop for prolonged time, 
						some other user may use the <strong>Servosms</strong> system in your absence, 
						and hence after a stipulated time if the <strong>Servosms </strong>system is <strong>
							idle</strong> the <strong>session</strong> <strong>expires</strong>, and 
						you are forced to <strong>Login</strong> again. A <strong>Session</strong> is 
						an event of time from the point of login and till the <strong>Servosms</strong> System 
						is actively being used, too much idle time leads to a timeout.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="el"></a>2.2.2</font> Event Logger</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">The <strong>Servosms</strong> System maintains on a daily basis 
						a <strong>log</strong> file which contains useful information related to the 
						events which occurred during normal and smooth functioning and also whenever an 
						error or exception occurs, all such events are <strong>recorded</strong>. The 
						Administrator should view these log files on a regular basis to take stock of 
						different events which has taken place as a precautionary measure.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="user"></a>2.3</font> Default User 
					Type, Login Name and Password</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">After the initial Servosms System installation, to start with 
						the <strong>default User Type</strong> is <strong>Administrator</strong> the<strong>
							Login Name</strong> is <strong>admin</strong>, and the <strong>Password</strong>
						is <strong>admin</strong>. The administrator should at least change the <strong>password</strong>
						after the initial installation.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="gs"></a>3.0</font> Getting Started</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ser"></a>3.1 </font>Starting 
					Servosms
				</td>
			</tr>
			<tr>
				<td class="t">To start the <strong>Servosms</strong> System launch an Internet 
					Explorer Browser and type the following URL (Uniform Resource Locator) at the <strong>
						Address</strong> bar.
					<p align="center"><A href="http://localhost/EPetro/Login.aspx">http://localhost/Servosms/Login.aspx</A>
					</p>
					<p>as shown below in figure 2.<br>
					</p>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div class="uh" align="center"></div>
				</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ls"></a>3.2</font> Login Screen
				</td>
			</tr>
			<tr>
				<td class="t">The <strong>Login screen</strong> appears as shown in figure 2.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy" vAlign="top">
					<div align="center"><IMG class="head" src="images/Forms/Login.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 2.
					</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">The Login Screen requires you to select your user Type and 
						additionally input your Login Name and Password. For example select <strong>Administrator</strong>
						from the drop-down, type Servosms in the User Login text box, followed by the 
						password <strong>Servosms</strong> in the Password text box, and then press the 
						Sign in button.
					</div>
					<p align="justify">Once the User Login and Password are inputted. The <strong>Servosms</strong>
						System performs the necessary authentication and authorization and allows the 
						user to gain access.</p>
					<p align="justify">If your Login name and Password are inputted correctly the Home 
						Page appears as shown in figure 3.</p>
					<p align="justify">In case an user inputs a wrong Login name or Password, the 
						following dialog box appears :<br>
					</p>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="center"><IMG height="119" src="images/image007.jpg" width="252"></div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Simply press the <strong>OK</strong> button, and re-enter your 
						Login Name and Password again.
					</div>
					<p align="justify"><strong>Note:</strong> This User manual assumes that the <strong>Print_WindowService</strong>
						is up and running as a Windows Service, if not (or any problem with printing) 
						refer to the Installation Guide for the topic <A href="#how">How to start the 
							Print_WindowService</A> for <strong>DOS</strong> based printing.</p>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="hp"></a>3.3</font> Home Page</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="center"><IMG src="images/Forms/HomePage.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 3.</div>
				</td>
			</tr>
			<tr>
				<td class="uh">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">The <strong>Home Page</strong> is the central point to 
						navigate through the <strong>Servosms</strong> System and provides the menus and 
						links necessary to operate the <strong>Servosms</strong> System. All the 
						Web-Pages in the <strong>Servosms</strong> System provides a <strong>Home</strong>
						link at the upper right corner to come back to the Home Page.
						<p>The <strong>Home Page</strong> displays the following <strong>Modules</strong>.</p>
						<p><strong><IMG height="5" src="images/b.jpg" width="5"> Administrator<br>
								<IMG height="5" src="images/b.jpg" width="5"> Account<br>
								<IMG height="5" src="images/b.jpg" width="5"> Shift And Employees
								<br>
								<IMG height="5" src="images/b.jpg" width="5"> Logistics<br>
								<IMG height="5" src="images/b.jpg" width="5"> Master<br>
								<IMG height="5" src="images/b.jpg" width="5"> Purchase/Sales/Inventory
								<br>
								<IMG height="5" src="images/b.jpg" width="5"> Report/MIS </strong>
						</p>
					</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="lo"></a>3.3.1</font> Logout</td>
			</tr>
			<tr>
				<td class="t">To Log out of <strong>Servosms</strong> System press the <strong>Logout</strong>
					button at the upper right corner.
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ad"></a>4.0</font> Administrative 
					Module</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">The <strong>Administrative Module</strong> can be<strong> only 
							accessed</strong> by the <strong>Administrator</strong> and is used to 
						perform the following task.
					</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="roles"></a>4.1</font> Roles</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Different users play different<strong> roles</strong> in using 
						the Servosms System like an <strong>administrator</strong>, <strong>accountant</strong>,
						<strong>sales manager</strong> etc.<br>
						These roles help to <strong>delineate</strong> the users to access only those 
						modules for which they have a right, thereby safe guarding the overall security 
						of the <strong>Servosms</strong> System.
					</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="nr"></a>4.1.1</font> Create a new 
					Role</td>
			</tr>
			<tr>
				<td class="t">To create a new Role click on the link <strong>Roles</strong> in the 
					Home Page. The screen will display the screen as shown below in figure 4.</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="center"><IMG src="images/Forms/Roles.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 4.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Enter a Role Name for e.g. Accountant in the text box against <strong>
							Role Name</strong> and a short description about this role in the text area 
						against <strong>Description</strong> for e.g. Can access only accounts module. 
						Press the <strong>Save</strong> button to save this role. You can create as 
						much roles as required for different users.
					</div>
					<p>Click the <strong>Home Link</strong> to return to the Home Page.</p>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="dr"></a>4.1.2</font> Update or 
					Delete a Role</td>
			</tr>
			<tr>
				<td class="t">To update or delete an existing Role, press the Button besides the <strong>
						Role ID</strong>, the screen will look as shown in Figure 5.</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="center"><IMG src="images/Forms/Roles_edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 5.
					</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">To update an existing role select the Role from the drop-down, 
						its details like the Role Name and Description, appears, do the necessary 
						modification and press the <strong>Save</strong> button. Similarly to delete an 
						existing role select the role from the drop-down and press the <strong>Delete</strong>
						button.</p>
					<p align="justify">Click the <strong>Home </strong>Link to return to the Home Page.</p>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="up"></a>4.2</font> User Profile
				</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">The User Profile contains information like the <strong>User Name</strong>,
						<strong>Login Name</strong>, <strong>Full Name</strong> and the<strong> Role</strong>
						played. Without a valid profile <strong>no</strong> user can access the <strong>Servosms</strong>
						System.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="np"></a>4.2.1</font> Create a new 
					Profile
				</td>
			</tr>
			<tr>
				<td class="t">To create a new<strong> User Profile</strong> click on the <strong>User 
						Profile</strong> link on the Home Page.
					<p>The following page appears as shown below in figure 6.
					</p>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="center"><IMG src="images/Forms/User Profile.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 6.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Enter the <strong>Login Name</strong>, <strong>Password</strong>,
						<strong>user's full name</strong> and select an appropriate <strong>Role</strong>
						from the drop-down and press the <strong>Save</strong> <strong>Profile</strong> 
						button.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="dp"></a>4.2.2</font> Update or 
					Delete a Profile</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To update or delete an existing User Profile press the <strong>Update</strong>
						button besides the <strong>User ID</strong>. The following screen appears as 
						shown in figure 7.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="center"><IMG src="images/Forms/User Profile_edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 7.
					</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the <strong>User ID</strong> from the drop-down, its 
						details appear on the screen, do the necessary updating and press the <strong>Save 
							Profile</strong> button to update. To delete an existing User profile 
						select an existing profile and simply press the <strong>Delete</strong> button.
					</div>
					<p align="justify">Click the <strong>Home Link</strong> to return to the Home Page.</p>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="prv"></a>4.3</font> Privileges</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">The <strong>privileges</strong> decides what level of 
						operation an user can perform in the <strong>Servosms</strong> System. Different 
						modules provide the means to access different aspect of the entire <strong>Servosms</strong>
						System, not all user should have the rights to gain easy access in its 
						entirety, and pose a danger to the security of confidential data the <strong>Servosms</strong>
						System is maintaining. The privileges of a user decides which of the following 
						operations namely<strong> Add</strong>, <strong>View</strong>, <strong>Update</strong>,
						<strong>Delete</strong> can a user perform once logged successfully into the <strong>
							Servosms</strong> System.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="aprv"></a>4.3.1</font> Allocate 
					Privileges to a User
				</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">An administrator can allocate privileges to a user by clicking <strong>
							Privileges </strong>link in the Home Page. The following screen appears as 
						shown in figure 8.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy" vAlign="top">
					<div align="center"><IMG src="images/Forms/Priviledges.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 8.
					</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select a <strong>User ID</strong> from the drop down, its <strong>User 
							Name</strong> appears in the text box besides.
					</div>
					<p align="justify">The initial screen lists the following modules which are 
						governed by privileges because they provide the facility to Add, View Edit and 
						Delete, unlike modules like Reports which are only Viewed or Printed.</p>
					<p align="justify">To allocate the privileges for <strong>Account Module</strong>, 
						simply click the check box beside.</p>
					<p align="justify">The initial screen is expanded and appears as shown below in 
						figure 9.</p>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy" vAlign="top">
					<div align="center">
						<p><IMG src="images/Forms/Priviledges_Accounts.jpg" width="575"></p>
					</div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 9.</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Depending on the rights of the user the Administrator can now 
						click the check boxes to select or deselect the View, Add, Edit and Delete 
						options. If the check box is selected then that operation can be performed by 
						that particular user.
					</div>
					<p align="justify">This step should be <strong>repeated</strong> for all the <strong>modules</strong>
						listed above; so that the entire range of modular operations of Servosms are 
						governed by safety and devoid of misuse by unauthorized users.</p>
					<p align="justify">After allocation the privileges of all modules press <strong>Save</strong>
						button.
					</p>
					<p align="justify">Click the<strong> Home</strong> Link to return to the Home Page.</p>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="uprv"></a>4.3.2</font> Update 
					Privileges of a User</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To update the privileges simply perform the above steps, 
						change the necessary settings of the check boxes and resave the modified 
						privileges by pressing <strong>Save</strong> button.
					</div>
					<p align="justify">Click the <strong>Home</strong> Link to return to the Home Page.</p>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="org"></a>4.4 </font>Organization 
					Details</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">This section is meant to input one time details of the 
						organization i.e. the Petrol Station. This is a very important one time step 
						because details like Dealer Name, address, city etc. entered during this 
						process are used subsequently in Report printing, and as headers on some pages 
						like Credit Bill.
					</div>
					<p align="justify">To enter the organization details click the link <strong>Organization 
							Details</strong> on the Home Page.</p>
					<p align="justify">The following screen appears as shown in figure 10.</p>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy" vAlign="top">
					<div align="center"><IMG src="images/Forms/OrganisationBlank.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 10.
					</div>
				</td>
			</tr>
			<tr>
				<td class="uhCopy">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the Dealership from<strong> Dealership drop down</strong>. 
						If the dealer name is not given in drop down then select the <strong>Other</strong>
						option, the <strong>Organization Details</strong> page will change and a text 
						box appears to enter the Dealership as shown in following figure 11.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Organization Details.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 11.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Enter the organization details. Press <strong>Browse</strong> button 
					to select an image, this image is used on the Credit Bill page. Enter the VAT 
					Rate and Message to apply and display in all invoices and specify the Accounts 
					Period. Once all the information is inputted press the <strong>Save Profile</strong>
					button.
					<p>Click the <strong>Home </strong>Link to return to the Home Page.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="br"></a>4.5</font> Backup &amp; 
					Restore
				</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">The mere existence of any system is data, hence it is very 
						import to take periodical backups of your important data failing which could 
						lead to loss of several days/months of efforts. The backup process copies your 
						data into a safe location and maintains three different copies, in a folder 
						termed as GrandFather, Father and Son, the Son contains the latest version of 
						your data in the database. Hence you should periodically take backups. In case 
						of any catastrophe like database being corrupt or lost we can restore the data 
						back through the Restore process.
					</div>
					<p align="justify"><strong>Note</strong>: You should not unnecessarily start a 
						Restore process, that can accidentally corrupt your valuable data. Restore 
						should be done only after a catastrophic loss of your original database.</p>
					<p align="justify">To backup and restore the <strong>Servosms</strong> database 
						click on <strong>Backup &amp; Restore</strong> from <strong>Administration</strong>
						menu.</p>
					<p align="justify">The following screen appears as shown in figure 12.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Backup_Restore.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 12 .</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">You must stop the <strong>MS-SQL Server</strong> before taking 
						a Backup or Restore. Press <strong>Backup</strong> button to backup your 
						database and to restore the database press Restore button.
					</div>
					<p align="justify">Note: This User manual assumes that the <strong>Print_WindowService</strong>
						is up and running as a Windows Service, if not (or any problem with backup or 
						restore) refer to the Installation Guide for the topic <A href="#how">How to start 
							the Print_WindowService</A> for <strong>DOS</strong> based printing.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ac"></a>5.0</font> Accounts</td>
			</tr>
			<tr>
				<td class="t">The Accounts module provides <strong>Ledger Creation</strong>, <strong>Payment 
						Receipt</strong>, <strong>subGroup</strong>, <strong>Cash Payment </strong>and
					<strong>Voucher Type</strong>.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="rct"></a>5.1</font> Receipt</td>
			</tr>
			<tr>
				<td class="t">To make a Receipt click on the<strong> Receipt</strong> link on the 
					Home Page. The following screen appears as shown in figure 13 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Receipt.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 13.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the <strong>Customer</strong> from the drop-down list, 
						the <strong>Place</strong> is displayed in the text box. Details of Payment Due 
						are populated automatically. Enter the amount in words in the text box below. 
						Select the payment<strong> Mode</strong> from the drop-down and enter the <strong>Amount</strong>
						in numeric form, click on the text box under <strong>Final Dues After Payment</strong>, 
						the <strong>Amount</strong> is computed and displayed. Press <strong>Save</strong>
						button to save the Payment Receipt details. To generate a printed report press<strong>
							Print </strong>button.
					</div>
					<p align="justify"><strong>Note</strong>: The Cash and bank account must be 
						available before a Receipt can be made.
					</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="pay"></a>5.2</font> Payment</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="mpay"></a>5.2.1</font> Make a 
					Payment</td>
			</tr>
			<tr>
				<td class="t">To make a Payment click on the<strong> Payment</strong> link on the 
					Home Page, the following screen appears as shown in figure 14.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Payment.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 14.<br>
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select a Ledger Name from the drop-down list. This drop-down 
						display all Ledgers except 'Cash in Hand' and 'Bank' types. The drop-down By 
						displays all the Ledgers of type 'Cash in Hand' and 'Bank', select the 
						appropriate ledger. Enter the Bank Name, Cheque No. and Date only if the By 
						Ledger Name is of type Bank. Next enter the Amount and Narrations. Press the <strong>
							Add</strong> button to save the Payment entry.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="dpay"></a>5.2.2</font> Edit or 
					Delete an existing Payment</td>
			</tr>
			<tr>
				<td class="t">To Edit or Delete an existing Payment entry press the button besides 
					the drop-down of <strong>Ledger Name</strong> in the above figure 14. The 
					following screen appears as shown in figure 15.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Payment_edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 15.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the Ledger Name from the first drop-down, the details 
						of Payment is automatically populated on the screen, to modify the Ledger Name 
						select the appropriate Ledger Name from the drop-down besides. To modify By 
						select the Ledger Name from the drop-down. To modify Bank details enter the 
						Bank Name Cheque No and Date. To modify the Amount type the new Amount in the 
						text box. To modify the Narrations type the narrations in the text area. To 
						save the modification press the<strong> Edit </strong>button.
					</div>
					<p align="justify">To Delete a Payment select the appropriate Ledger Name from the 
						drop-down and Press the <strong>Delete</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="vcr"></a>5.3</font> Voucher Entry</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="nvcr"></a>5.3.1</font> Create a New 
					Voucher</td>
			</tr>
			<tr>
				<td class="t">To create a new Voucher press the <strong>Voucher</strong> link on 
					the Home Page. The Following screen appears as shown in figure 16.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><img src="images/Forms/Voucher entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 16.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select a Voucher Type from the drop-down, the Voucher ID is 
						populated automatically. All the drop-downs under Account Name are populated 
						with Ledger Names, according to Voucher type. Next select an Account Name, and 
						enter the Amount, select DR. or Cr. The Dr./Cr. On the right hand side is 
						automatically selected. Select the Account Name and enter the Amount. Repeat 
						this step for all Account Names. Enter a Narration if any in the text area and 
						press the <strong>Add </strong>button to save the Voucher entry.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="dvcr"></a>5.3.2</font> Edit or 
					Delete an existing Voucher</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To Edit an existing Voucher entry press the button besides the <strong>
							Voucher ID</strong>. The following screen appears as shown in figure 17.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center">
						<p><IMG src="images/Forms/Voucher entry_edit.jpg" width="575"></p>
					</div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 17.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select a Voucher Type, and a Voucher ID, the details are 
						populated for one entry, the rest of the entries are disabled. Select an 
						Account Name, enter the Amount and select Dr. or Cr. The Dr. or Cr. On the 
						right hand side are automatically selected. Select the Account Name and Amount, 
						Modify the Narration if required. And press the<strong> Edit b</strong>utton to 
						save the modifications.
					</div>
					<p align="justify">To Delete a Voucher entry simply select the appropriate Voucher 
						ID and press the <strong>Delete</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="lc"></a>5.4 </font>Ledger Creation</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="nl"></a>5.4.1</font> Create a new 
					Ledger</td>
			</tr>
			<tr>
				<td class="t">To create a Ledger click on the <strong>Ledger Creation</strong> link 
					on the Home Page. The following screen appears as shown in figure 18.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Ledger Creation.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 18.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Enter a Ledger name in the text box. Then select SubGroup Name 
						from the drop-down, the related Group names will be populated in the drop-down 
						below. In case the SubGroup name is not available in the drop-down then select 
						the option Other in the drop-down, the text box besides is enabled to enter the 
						SubGroup name, similarly if the Group Name is not available then select the 
						option Other and enter a new Group name.
					</div>
					<p align="justify">The Nature of Group is automatically selected in the list of 
						radio buttons when the SubGroup is selected, the user can however select a 
						different Nature of Group by selecting the radio buttons.</p>
					<p align="justify">Enter the Opening Balance, and select Dr. or Cr. From the 
						drop-down beside and press <strong>Add</strong> button to save the Ledger 
						entry.</p>
					<p align="justify"><strong>Note</strong>: The Ledger Name should not be same as a 
						Customer or Vendor name. And Ledger Name cannot be repeated for the same 
						SubGroup.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="dl"></a>5.4.2</font> Edit or Delete 
					an existing Ledger</td>
			</tr>
			<tr>
				<td class="t">To Edit or Delete a Ledger entry click on the button besides the text 
					box <strong>Ledger Name</strong> as shown above in figure 18.
					<p>The following screen appears as shown below in figure 19.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center">
						<p><IMG src="images/Forms/Ledger Creation_edit.jpg" width="575"></p>
					</div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 19.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the Ledger Name from the drop-down (the drop-down also 
						displays the ID), the Ledger name is displayed in the text box besides, the 
						user can now modify the Ledger Name. To modify the SubGroup select the 
						appropriate SubGroup Name, to modify the Group Name select the appropriate 
						Group Name from the drop-down. Select the Nature of Group from the list of 
						radio buttons. The Opening Balance can be modified in the text box. Press the <strong>
							Edit</strong> button to save the modifications.
					</div>
					<p align="justify">To Delete a particular Ledger entry simply select the 
						appropriate Ledger Name from the drop-down and press the <strong>Delete</strong>
						button.</p>
					<p align="justify"><strong>Note</strong>: The Ledger Name should not be same as a 
						Customer or Vendor name. And Ledger Name cannot be repeated for the same 
						SubGroup.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="sae"></a>6.0</font> Shift And 
					Employees</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">This module provides the facility to create Employees, create 
						and manage their Shifts, take daily attendance, assign or sanction leave, 
						assign overtime and print salary statements.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ee"></a>6.1</font> Employee Entry
				</td>
			</tr>
			<tr>
				<td class="t">To Enter the details of a new Employee, click on the Employee Entry 
					link on the Home Page.
					<p>The Employee Entry page appears as shown in the Figure 20 below.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Employee entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 20.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Field marked as <font color="#ff0000">* </font>are mandatory. 
						Enter the Name, address, select the city from the drop-down, enter the phone 
						numbers, and email address if any. Select the<strong> Designation</strong> from 
						the drop-down, if the designation is <strong>Driver</strong> then the <strong>Employee 
							Entry</strong> form will change, and the fields regarding driver's license 
						and insurance appears as shown in following figure 21.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/EmployeeEntryDriver.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 21.
						<br>
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Enter the Driver License No., LIC No. and validity dates. Next 
						enter the salary and OverTime compensation. Press the <strong>Save</strong> <strong>
							Profile</strong> button to save the Employee details. To return back to the 
						Home Page click the link<strong> Home Page</strong>.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a id="empl" name="empl"></a>6.2</font> Employee 
					List</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To view a List of all employees click on the Employee List 
						link on the Home Page, the screen appears as shown in figure 22 below.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/employeelist.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 22.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a id="semp" name="semp"></a>6.2.1</font> Search 
					Employees</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Press the <strong>Search</strong> button to view all the employees as 
					shown below in figure 23.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/EmployeeListDetails.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 23.<br>
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">If there are more employees than use the <strong>Prev</strong> or <strong>
						Next</strong> button to navigate backward or forward.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="eemp"></a>6.2.2</font> Edit an 
					Employee</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To Edit an Employee press the<strong> Edit</strong> link in 
						the above figure. For example if the Employee with ID 1001 is clicked, the 
						following screen appears as shown in figure 24.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/EmployeeUpdate.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 24.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Do the necessary updation, for the designation of <strong>Driver</strong>
						the screen will change as given in above figure 24. Press the <strong>Update 
							Profile</strong> button to save the updation.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="demp"></a>6.2.3</font> Delete an 
					Employee</td>
			</tr>
			<tr>
				<td class="t">To Delete an Employee click on the appropriate Delete link after the 
					search is over.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ss"></a>6.3</font> Salary Statement</td>
			</tr>
			<tr>
				<td class="t">
					<div align="left"><font color="#000000">The Salary Statement is used to make statement 
							of the salary for employees as shown in figure 25 below.</font></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/forms/Salary Statement.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 25.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="areg"></a>6.4</font> Attendance 
					Register</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">The Attendance Register is used to mark the attendance of an 
						employee, click the link<strong> Attendance Register</strong> on the Home Page, 
						the following screen appears as shown in figure 26 below.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Attendance_Register.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 26.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">The <strong>Attendance Register</strong> displays the list of 
						all the employees whose attendance has <strong>not</strong> yet been marked. To<strong>
							mark </strong>the attendance select the check box for all those employees 
						who are present. To mark all the employees select the check box <strong>Select All</strong>, 
						and press <strong>Submit</strong>.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="lapp"></a>6.5</font> Leave 
					Application</td>
			</tr>
			<tr>
				<td class="t">To apply for a Leave Application, click the link <strong>Leave 
						Application</strong>, the following screen appears as shown below in figure 
					27.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Leave Application.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 27.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the Employee from the drop-down, click the datepicker 
						button, a calendar appears, select the Date From, similarly select the Date To 
						by pressing the datepicker besides. Type the <strong>Reason</strong>, for the 
						leave, and press the <strong>Apply</strong> button.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uhCopy">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="lsanc"></a>6.6</font> Leave 
					Sanction
				</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">When employees apply for a leave, these leave have to be 
						sanctioned, click the link<strong> Leave Sanction</strong> on the Home Page, 
						the leave sanction page appears on the screen as shown below in figure 28.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Leave Sanction.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 28.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">A list of all employees who have applied for leave appear, 
						select the check box under the label Accept to sanction the leave, once all the 
						leave have been sanction press the<strong> Submit</strong> button.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="logistics"></a>7.0</font> Logistics</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ve"></a>7.1</font> Vehicle Entry</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="nve"></a>7.1.1</font> Add a New 
					Vehicle Entry</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To enter a new vehicle click on Vehicle Entry link on the Home 
						Page. The following screen appears as shown below in Figure 29.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Vehicle Entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 29.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To add a new Vehicle select the Vehicle Type from the 
						drop-down then fill the vehicle and insurance related information in the 
						corresponding text box, select the Vehicle route from the drop-down then select 
						the Fuel Used from drop-down and enter the quantity in the text box beside the 
						drop-down and enter the Fuel Starting Quantity in text box. In Fuel/Lubricants 
						Uses section select the Oil and Lubricants from all the drop-downs, enter the 
						used quantity in the text box besides the drop-downs, select the entry Date and 
						enter the total KM. Enter Vehicle Ave rage and press the <strong>Save</strong> button 
						to save the new Vehicle Entry.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="dve"></a>7.1.2</font> Edit or 
					Delete an existing Vehicle Entry</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To Edit or Delete an existing Vehicle Entry press the button 
						beside the Vehicle ID as shown in above Figure 29. The following screen appears 
						as shown in Figure 30.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Vehicle Entry_Edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 30.<br>
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To edit the Vehicle Entry select the Vehicle ID from 
						drop-down, the Vehicle details appears on the screen for the selected Vehicle 
						ID. Modify the required fields and press the <strong>Edit</strong> button to 
						save the modification.
					</div>
					<p align="justify">To delete the Vehicle Entry select the Vehicle ID from the 
						drop-down and press the <strong>Delete</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="vdlb"></a>7.2</font> Vehicle Daily 
					Log Book</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="nvdlb"></a>7.2.1</font> Add a new 
					Vehicle Daily Log Book Entry</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To add a new Vehicle Daily Log Book Entry click on Vehicle 
						Daily Log Book link on the Home Page. The following screen appears as shown in 
						figure 31.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Vehicle Daily Log Book.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 31.<br>
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To add a new Vehicle Log Entry select the Vehicle No from the 
						drop-down the Vehicle Name, Driver's Name and Meter Reading (Previous Day) 
						appears automatically, enter the Current Day Meter Reading in text box, select 
						the Vehicle Route from drop-down then select the Fuel, Oil and Lubricants used 
						from drop-downs and enter the used Quantity in the text box. Finally fills the 
						Other Expenses and press the <strong>Save</strong> button to save the Vehicle 
						Daily Log Book entry.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="dvdlb"></a>7.2.2</font> Edit or 
					Delete an existing Vehicle Log Book Entry</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To edit or delete the Vehicle Log Entry press the Button 
						beside the VDLB ID as shown in above Figure 31. The following screen appears as 
						shown as Figure 32.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Vehicle Daily Log Book_Edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 32.<br>
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To edit the Vehicle Log Book entry select a VDLB ID from the 
						drop-down the details appears on the screen for the selected VDLB ID. Modify 
						the required fields and press the<strong> Edit</strong> button to save the 
						modifications.
					</div>
					<p align="justify">To delete the Vehicle Log Book Entry simply select the VDLB ID 
						from the drop-down and press the <strong>Delete</strong> button.</p>
					<p align="justify">To print the Vehicle Log Book Details fill all the fields for a 
						new entry or select the VDLB ID for the existing entry then press the <strong>Print</strong>
						Button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="rm"></a>7.3</font> Route Master</td>
			</tr>
			<tr>
				<td class="t">To enter a route click the <strong>Route Master</strong> link on the 
					Home Page. The following screen appears as shown below in figure 33.
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Route Insertion.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 33.<br>
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To add a route enter the Route Name in the text box, Next 
						enter Route KM in the text t box and press<strong> Add </strong>button.
					</div>
					<p align="justify">To edit en existing route select a Route Name from the drop-down 
						and press the Edit button. Modify the route details and press the <strong>Edit</strong>
						button again.</p>
					<p align="justify">To delete the route details simply select the route name from 
						drop-down and press the<strong> Delete</strong> Button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="part"></a>8.0</font> Master</td>
			</tr>
			<tr>
				<td class="t">This module deals with Place, Customer, Vendor and Slip information.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="be"></a>8.1</font> Beat Entry</td>
			</tr>
			<tr>
				<td class="t">The Beat Entry deal with Place information like City, State and 
					Country, City is mandatory.
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="nb"></a>8.1.1</font> Create a new 
					Beat</td>
			</tr>
			<tr>
				<td class="t">To enter a new Beat, click on the <strong>Beat Entry</strong> link<strong>
					</strong>on the Home Page, the screen will look as shown in figure 34.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Beat Entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 34.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Entry the City, followed by the State and Country. Press <strong>Add</strong>
					button to save the Beat information.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="eb"></a>8.1.2</font> Edit a Beat</td>
			</tr>
			<tr>
				<td class="t">To edit a Beat information press the<strong> Edit </strong>button, 
					the edit screen will appear as shown in figure 35.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Beat entry_Edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 35.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the Beat No. the details appear on the screen, do the 
						necessary modification and press the Edit button again to save the changes.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="db"></a>8.1.3</font> Delete a Beat</td>
			</tr>
			<tr>
				<td class="t">Select the <strong>Beat No.</strong> the details appear on the 
					screen, press the <strong>Delete</strong> button to delete the Beat entry.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ce"></a>8.2</font> Customer Entry</td>
			</tr>
			<tr>
				<td class="t">To enter Customer details, click <strong>Customer Entry</strong> link 
					on the Home Page, the following screen appears as shown in figure 36.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Customer Entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 36.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Enter the Customer Name, then select the Customer Type from 
						the drop-down list, enter the Address of the customer, select the City, the 
						State and Country are automatically populated. City is mandatory. Enter the 
						Contact numbers and the Email address if any.<br>
						<br>
						Next enter the Credit Limit, and select the Credit Days from the drop-down 
						list. Finally enter the Opening Balance of the Customer. Press the <strong>Save 
							Profile</strong> button to Save the customer information.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="sde"></a>8.3</font> Scheme Discount 
					Entry</td>
			</tr>
			<tr>
				<td class="t">It deals with discount provided to customers on different products.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="nsde"></a>8.3.1</font> Add a new 
					Scheme Discount Entry</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To enter Scheme Discount, click<strong> Scheme Discount</strong>
						link on the Home Page, the following screen appears as shown in figure 37.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Scheme Discount Entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 37.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="dsde"></a>8.3.2</font> Edit an 
					existing Scheme Discount Entry</td>
			</tr>
			<tr>
				<td class="t">To edit an existing Scheme Discount Entry click the <strong>Update</strong>
					link as shown in figure 38.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Scheme Discount Entry edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 38.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="mce"></a>8.4</font> Market Customer 
					Entry</td>
			</tr>
			<tr>
				<td class="t">Allows the user to list all Market Customers, and edit or delete a 
					Customer.</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="nmce"></a>8.4.1</font> Add a new 
					Market Customer Entry</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To enter Market Customer, click<strong> Market Customer</strong>
						link on the Home Page, the following screen appears as shown in figure 39.</div>
				</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head" vAlign="top">
					<div align="center"><IMG src="images/Forms/Market Customer Entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 39.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="emce"></a>8.4.2</font> Edit an 
					existing Market Customer Entry</td>
			</tr>
			<tr>
				<td class="t">To edit an existing Market Customer Entry, click on the <strong>Edit</strong>
					button as shown in figure 39 above.<br>
					<br>
					The following screen appears as shown in figure 40.
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head" vAlign="top">
					<div align="center"><IMG src="images/Forms/Market Customer entry edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 40.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a id="ven" name="ven"></a>8.5</font> Vendor 
					Entry</td>
			</tr>
			<tr>
				<td class="t">To enter Vendor information, click <strong>Vendor Entry</strong> link 
					on the Home Page, the following screen appears as shown in figure 41.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Vendor Entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 41.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Enter the full <strong>Name</strong> of the Vendor, then 
						select the <strong>Type</strong> of Vendor from the drop-down list. Type the 
						vendor's <strong>Address</strong> in the text-area , and select the <strong>City</strong>, 
						the State and Country are automatically populated. Enter the <strong>Contact</strong>
						numbers, and <strong>Email</strong> address if any. Enter the <strong>Opening 
							Balance</strong> and select the<strong> Balance type</strong> Cr./Dr. and 
						select the <strong>Credit Days</strong> from the drop-down list. Press the <strong>Save 
							Profile</strong> button to save the vendor information.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="cl"></a>8.6</font> Customer List</td>
			</tr>
			<tr>
				<td class="t">Allows the user to list all Customers, and edit or delete a Customer.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a id="lcmr" name="lcmr"></a>8.6.1</font> List 
					all Customer</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To search and view all Customers click on the <strong>Customer 
							List</strong> link on the Home page. The following screen appears as shown 
						in figure 42.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/CustomerList.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 42.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Press the <strong>Search</strong> button, the Customer list will be 
					displayed as shown in figure 43.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Customer List.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 43.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">If there are more customers than can fit the screen, use the<strong> Prev/Next</strong>
					buttons to navigate.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ec"></a>8.6.2</font> Edit a 
					Customer</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To Edit a particular Customer click its <strong>Edit </strong>link 
						in figure 43. The following screen appears as shown in figure 44, populated 
						with the customer details.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Customer update.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 44.<br>
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Do the necessary updation and press the <strong>Update</strong> button 
					to save the changes.
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="dc"></a>8.6.3</font> Delete a 
					Customer</td>
			</tr>
			<tr>
				<td class="t">To Delete a particular customer, click the <strong>Delete</strong> link 
					in figure 44, for that Customer.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="vl"></a>8.7</font> Vendor List</td>
			</tr>
			<tr>
				<td class="t">To list all vendors click the<strong> Vendor List</strong> link on 
					the Home Page, the following screen appears as shown in figure 45.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center">
						<p><IMG src="images/forms/VendorList.jpg" width="575"></p>
					</div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 45.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Click the <strong>Search</strong> button, to view a list of all 
					vendors as shown in figure 46.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Vendor List.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 46.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ev"></a>8.7.1</font>Edit a Vendor</td>
			</tr>
			<tr>
				<td class="t">To edit information of a Vendor click the <strong>Edit</strong> link 
					of that particular Vendor in figure 46.<br>
					<br>
					The following screen appears with the Vendor details as shown in figure 47.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/VendorUpdate.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 47.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Do the necessary modification and press the<strong> Update</strong> button 
					to save your changes.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="dv"></a>8.7.2</font> Delete a 
					Vendor</td>
			</tr>
			<tr>
				<td class="t">To delete a particular Vendor click on the appropriate Delete link in 
					figure 47 above.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="cdm"></a>8.8</font> Customer Data 
					Mining</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To view the details of all the customers, click on <strong>Customer 
							Data Mining</strong> link on the Home Page. The following screen appears as 
						shown in figure 48 below.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Customer Data Mining.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 48.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To view the customer details select an option from <strong>Order 
							By</strong> drop down. This drop down contains the order like, Customer 
						Name, Customer Type and Customer City. Select your choice to see the data 
						mining report.
					</div>
					<p align="justify">Press <strong>Print </strong>button to print the details.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="cme"></a>8.9</font> Customer 
					Mechanic Entry</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ncme"></a>8.9.1</font> Add a new 
					Customer Mechanic Entry</td>
			</tr>
			<tr>
				<td class="t">To add a new Customer Mechanic Entry click on its link given in Home 
					Page as shown below in figure 49.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Customer mechanic Entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 49.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ecme"></a>8.9.2</font> Edit an 
					existing Customer Mechanic Entry</td>
			</tr>
			<tr>
				<td class="t">To edit information of a Vendor click the <strong>Edit</strong> link 
					as shown in figure 49.<br>
					<br>
					The following screen appears as shown in figure 50.</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head" vAlign="top">
					<div align="center"><IMG src="images/Forms/Customer Mechanic entry edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 50.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="cte"></a>8.10</font> Customer Type 
					Entry</td>
			</tr>
			<tr>
				<td class="t">It provides the facility to produce customer type.</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ncte"></a>8.10.1</font> Add a new 
					Customer Type Entry</td>
			</tr>
			<tr>
				<td class="t">To add a new Customer Type Entry click on its link given in Home Page 
					as shown below in figure 51.</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head" vAlign="top">
					<div align="center"><IMG src="images/Forms/Customer Type entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 51.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ecte"></a>8.10.2</font> Edit an 
					existing Customer Type Entry</td>
			</tr>
			<tr>
				<td class="t">To edit information of a Vendor click the <strong>Edit</strong> link 
					as shown in figure 51.<br>
					<br>
					The following screen appears as shown in figure 52.</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head" vAlign="top">
					<div align="center"><IMG src="images/Forms/Customer Type entry edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 52.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="fde"></a>8.11</font> Fleet/OE 
					Discount Entry</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="nfde"></a>8.11.1</font> Add a new 
					Fleet/OE Discount Entry</td>
			</tr>
			<tr>
				<td class="t">To add a new Fleet/OE Discount Entry click on its link given in Home 
					Page as shown below in figure 53.</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head">
					<div align="center"><IMG src="images/Forms/Fleet_OE Discount Entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 53.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="efde"></a>8.11.2</font> Edit an 
					existing Fleet/OE Discount Entry</td>
			</tr>
			<tr>
				<td class="t">To edit an existing Fleet / OE Discount Entry click on the<strong> Update</strong>
					link as shown in the figure 54 below.</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head" vAlign="top">
					<div align="center"><IMG src="images/Forms/Fleet_Oe Discount entry edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 54.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t" height="20">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="custupd"></a>8.12</font> Customer 
					Updation</td>
			</tr>
			<tr>
				<td class="t">To update an existing all Customer Information click on the<strong> Customer 
						Update</strong> link as shown in the figure 55 below.</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head" vAlign="top">
					<div align="center"><IMG src="images/Forms/Customer updation.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 55.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t" height="20">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="custmap"></a>8.13</font> Customer 
					Mapping</td>
			</tr>
			<tr>
				<td class="t">To insert or update an existing Customer SSR Information according to 
					select particular SSR(employee) from dropdown list, click on the<strong> Customer 
						Mapping</strong> link as shown in the figure 56 below.</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head" vAlign="top">
					<div align="center"><IMG src="images/Forms/CustomerMapping.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 56.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t" height="20">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="salesperass"></a>8.14</font> Sales 
					Person Assignment</td>
			</tr>
			<tr>
				<td class="t">To insert or update an existing SSR Beat Information according to 
					select particular SSR(employee) from dropdown list, click on the<strong> Sales 
						Person Assignment</strong> link as shown in the figure 57 below.</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head" vAlign="top">
					<div align="center"><IMG src="images/Forms/SalesPersonAssignment.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 57.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t" height="20">&nbsp;</td>
			</tr>
			<tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ssde"></a>8.15</font> Servo 
					Stockist Discount Entry</td>
			</tr>
			<tr>
				<td class="t">It deals with Servo Stockist Discount provided on different-2 
					products.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="nssde"></a>8.15.1</font> Add a new 
					Servo Stockist Discount Entry</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To enter Servo Stockist Discount, click<strong> Servo Stockist 
							Discount Entry</strong> link on the Home Page, the following screen appears 
						as shown in figure 37.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/StockistDiscount.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 37.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="essde"></a>8.15.2</font> Edit an 
					existing Scheme Discount Entry</td>
			</tr>
			<tr>
				<td class="t">To edit an existing Servo Stockist Discount Entry click the <strong>Update</strong>
					link as shown in figure 38.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/StockistDiscountEdit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 38.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<TR>
				<td class="head"><font color="#000000"><a name="psi"></a>9.0</font> Purchase/Sales/Inventory</td>
			</TR>
			<tr>
				<td class="t">
					<div align="justify">This section allows the user to enter a new product, update 
						its price, generate purchase, sales invoice, credit bills and enter tax details 
						of the fuel products.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="pe"></a>9.1</font> Product Entry</td>
			</tr>
			<tr>
				<td class="t">Allows the user to enter a new product or update the product.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a id="npro" name="npro"></a>9.1.1</font> Enter 
					a new Product</td>
			</tr>
			<tr>
				<td class="t">To enter a new Product click the <strong>Product Entry </strong>link 
					on the Home Page, the following screen appears as shown in figure 58.
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Product Entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 58.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Enter a Product Name in the text box. Select a Product Type 
						from the drop-down, if the Type is not available in the drop-down then specify 
						another in the text box. When another Category Type is specified in the text 
						box, it is automatically populated during subsequent product entry.
					</div>
					<p align="justify">Select a Package Type from the drop-down, if the Type is not 
						available in the drop-down then specify another in the text box. When another 
						Package Type is specified in the text box, it is automatically populated during 
						subsequent product entry. The Package Qty. field is automatically calculated 
						and displayed.
					</p>
					<p align="justify">Enter the Opening Stock, and select the Package Qty. units from 
						the drop-down besides.</p>
					<p align="justify">Select the Units from the drop-down, if it not available specify 
						another in the text field besides, this unit will automatically be available 
						during subsequent product entries.</p>
					<p align="justify">Finally select the Store In from the drop-down and press <strong>Save</strong>
						button to save the product details.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ep"></a>9.1.2</font> Edit an 
					existing Product</td>
			</tr>
			<tr>
				<td class="t">To edit an existing Product click the button besides the Product ID. 
					The screen will appear as shown in figure 59 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Product entry edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 59.<br>
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the Product from the drop-down the product details are 
						populated on the screen. Do the necessary updation and press the <strong>Save</strong>
						button to update the Product details.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="pu"></a>9.2</font> Price Updation</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Allows the user to enter the price of newly created product or 
						update the price of an existing product. Click on the <strong>Price Updation</strong>
						link on the Home Page. The following screen appears as shown in figure 60.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Price Updation.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 60.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">A list of all Products are displayed on the screen. <strong>Note</strong>
						that Products of <strong>Category Fuel</strong> do <strong>not </strong>have a 
						Package Type, and their <strong>Purchase Rate</strong> is in <strong>KL</strong>, 
						however their <strong>Sales Rate</strong> is in <strong>liters</strong>.
					</div>
					<p align="justify">To enter a new price or update an existing one, first click on 
						the check-box and enter the Purchase Rate and Sales Rate and press <strong>Submit</strong>
						button.</p>
					<p align="justify">Note that for non-fuel products the Sales Rate should be greater 
						than the Purchase Rate.</p>
					<p align="justify">If <strong>all</strong> the prices are to be entered or updated, 
						you can conveniently press the <strong>Select All</strong> check-box to select 
						all the products instead of selecting the check-boxes one-by-one.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="pi"></a>9.3</font> Purchase Invoice</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">It is used to generate a new <strong>Purchase Invoice</strong> 
						or edit an existing one.
					</div>
					<p align="justify"><strong>Note</strong>: Before a new Purchase Invoice is created, 
						it is necessary to do the following steps to ensure that the Price and Tax 
						related information are available.<br>
						<br>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<IMG height="5" src="images/b.jpg" width="5">
						<strong>Price Updation</strong> SHOULD be done to enter the <strong>Purchase Rate</strong>
						and <strong>Sales Rate</strong><br>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong><IMG height="5" src="images/b.jpg" width="5">
							Tax Entry</strong> SHOULD be done to enter the<strong> Tax</strong> related 
						entries</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="npi"></a>9.3.1</font> Create a new 
					Purchase Invoice</td>
			</tr>
			<tr>
				<td class="t">To create a new Purchase Invoice click on the<strong> Purchase Invoice</strong>
					link on the Home Page.
					<p>The following screen appears as shown in figure 61.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><IMG src="images/Forms/Purchases Invoice.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 61.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">To make a Fuel purchase click on the<strong> Purchase</strong> link.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="epi"></a>9.3.2</font> Edit an 
					existing Purchase Invoice</td>
			</tr>
			<tr>
				<td class="t">To edit a<strong> Purchase Invoice</strong> click on the button 
					besides the Invoice No. in figure 62.
					<p>The following screen appears as shown in figure&nbsp;62 below.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Purchases Invoice edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 62.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the<strong> Invoice No.</strong> from the drop-down, 
						all the invoice details are populated automatically. Do the necessary 
						modification and press the <strong>Save &amp; Print</strong> button to save the 
						changes, and generate a printed invoice report.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="si"></a>9.4</font> Sales Invoice</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To create and edit a <strong>Sales Invoice</strong> click the <strong>
							Sales Invoice</strong> link on the Home Page. The following screen appears 
						as shown in figure 63 below.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="nsi"></a>9.4.1</font> Create a new 
					Sales Invoice</td>
			</tr>
			<tr>
				<td class="t">To create a new Sales Invoice click on the<strong> Sales Invoice</strong>
					link on the Home Page.
					<p>The following screen appears as shown in figure 63.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Sales Invoice.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 63.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select a Customer Name from the drop-down list, The Place 
						Due-Date and Current Balance are populated automatically. Enter the Vehicle No. 
						and select the Sales Type from the drop-down list.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Select the<strong> Under Sales Man</strong> from the drop-down 
						list.</p>
					<p align="justify">Next select the <strong>Product Type</strong>, based on this 
						type the Product Names will appear in the drop-down list, when the Product is 
						selected the<strong> Available Stock</strong> and <strong>Rate</strong> are 
						automatically populated.
					</p>
					<p align="justify"><strong>Note</strong>: If the Product Type is <strong>Fuel </strong>
						the <strong>Package</strong> is not displayed.</p>
					<p align="justify">If the Product Type is <strong>not Fuel </strong>then select the <strong>
							Product Type</strong>. Next enter the <strong>Quantity</strong> and click 
						the amount text box to compute and display the <strong>Amount</strong>. A 
						maximum of<strong> twelve</strong> products can be entered in one sales 
						invoice.</p>
					<p align="justify">Enter the <strong>Promo Scheme</strong>, <strong>Remarks</strong>
						and<strong> Message</strong>, and click the <strong>Grand Total</strong> text 
						box, the amount is computed and displayed. Next select the <strong>Cash Discount</strong>
						type and enter the value, click on green radio button to apply the VAT or click 
						red button to remove the applied VAT from amount, select the<strong> Discount type</strong>
						and enter the amount and click the <strong>Net Amount</strong> text box, the 
						amount is computed and displayed. Press <strong>Save &amp; Print</strong> button 
						to save the invoice details and generate a printed invoice report.</p>
					<p align="justify">Note: This User manual assumes that the <strong>Print_WindowService</strong>
						is up and running as a Windows Service, if not (or any problem with printing) 
						refer to the Installation Guide for the topic <A href="#how">How to start the 
							Print_WindowService</A> for <strong>DOS</strong> based printing.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="esi"></a>9.4.2</font> Edit an 
					existing Sales Invoice</td>
			</tr>
			<tr>
				<td class="t">To edit a Sales Invoice click on the button besides the Invoice No. 
					in figure 64.
					<p>The following screen appears as shown in figure 64 below.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/forms/Sales Invoice edit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 64.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the<strong> Invoice No.</strong> from the drop-down 
						list, the invoice details are automatically populated. Do the necessary 
						modification and press <strong>Save &amp; Print</strong> button to save the 
						changes and generate the printed Sales Invoice report.
					</div>
					<p align="justify">The <strong>Quantity </strong>entered should be less than<strong> Available 
							Stock</strong> or an error message will popup as shown below.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG height="119" src="images/image114.jpg" width="197"></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Press <strong>OK </strong>button and proceed to enter the right 
					Quantity.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="lypssales"></a>9.5</font> Ly_Ps_Sales</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">This form is used to fill the primary and secondary sales from 
						select the year from the dropdown list. If exist the data in selected period 
						then show the data otherwise show the blank textboxes. This report view in two 
						way first is Details(default) and second is summerized, Click on <strong>Ly_Ps_Sales
						</strong>link, the following screen appears as shown in figure 65, by default 
						select <b>details</b> radio button.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/LS_PS_Sales.jpg" width="574"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 65.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">This report view in summerized then click on <b>Summerized </b>
						option, the following screen appears as shown in figure 66.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/LY_PS -2.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 66.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="sa"></a>9.6</font> Stock Adjustment</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">This is used to shift the stock of a product from one location 
						to another. Click on <strong>Stock Adjustment </strong>link, the following 
						screen appears as shown in figure 67.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Stock Adjustment Voucher entry.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 67.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">This contains two sections<strong> OUT</strong> and<strong> IN</strong>. 
						Select the out products from <strong>OUT</strong> section and in products from<strong>
							IN</strong> section.
					</div>
					<p align="justify">Select the product from<strong> Product Name &amp; Package </strong>
						drop down, the location of the selected product will automatically appears in <strong>
							Store In</strong> text box then enter the quantity in <strong>Qty. in Ltr</strong>
						text box for products of <strong>Fuel</strong> category or<strong> Loose Oil </strong>
						and enter quantity in<strong> Qty. in Pack</strong> for other type products.</p>
					<p align="justify">The out &amp; in quantity of a product should be same or an 
						error message popup will appear.</p>
					<p align="justify">Press the <strong>Save &amp; Print</strong> to save and print 
						the stock adjustment details.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="sr"></a>9.7</font> Sales Return
				</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To create the Sales Return credit note click on <strong>Sales 
							Return</strong> link on Home Page. The following screen appears as shown in 
						figure 68.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/forms/SalesReturn.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 68.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Select the <strong>Invoice no.</strong> from Invoice No drop down, 
					the invoice details appears as shown in figure 69 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Sales Return credit note.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 69.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To return a particular product, click on corresponding check 
						box and enter the return quantity in <strong>Qty </strong>text box. The <strong>Net 
							Amount </strong>shows the calculated amount of return products (Including 
						the <strong>Cash Discount</strong>, <strong>VAT</strong> and <strong>Discount</strong>
						if applied in selected sales invoice).
					</div>
					<p align="justify">The<strong> Return Quantity</strong> entered of a selected 
						product should be less than or equal to <strong>Sales Quantity</strong> otherwise 
						an error message will popup.</p>
					<p align="justify">Press the <strong>Save &amp; Print</strong> to save and print 
						the sales return details.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a id="pre" name="pre"></a>9.8</font> Purchase 
					Return</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To create the purchase return credit note click on <strong>Purchase 
							Return</strong> on Home Page. The following screen appears as shown in 
						Figure 70.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/PurchaseReturn.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 70.<br>
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Select the Invoice no. from <strong>Invoice No.</strong> drop down, 
					the invoice details appears as shown in figure&nbsp;71 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Purchases Return credit note.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 71.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To return a particular product, click on corresponding check 
						box and enter the return quantity in<strong> Qty </strong>text box. The<strong> Net 
							Amount </strong>shows the calculated amount of return products (Including 
						the<strong> Cash Discount</strong>, <strong>VAT</strong> and <strong>Discount</strong>
						if applied in selected purchase invoice).
					</div>
					<p align="justify">The Return Quantity entered of selected product should be less 
						than or equal to Purchase Quantity otherwise an error message will popup.</p>
					<p align="justify">Press the<strong> Save &amp; Print</strong> to save and print 
						the purchase return details.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="clu"></a>9.9</font> Customer Ledger 
					Updation</td>
			</tr>
			<tr>
				<td class="t">It provides the facility to update Customer Ledger as shown below in 
					figure 72.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Customer Ledger Balance Updation.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 72.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="slu"></a>9.10</font> Stock Ledger 
					Updation</td>
			</tr>
			<tr>
				<td class="t">It provides the facility to update Stock Ledger as shown below in 
					figure 73.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Stock Ledger Balance Updation.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 73.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="perinvoice"></a>9.11</font> Performa 
					Invoice</td>
			</tr>
			<tr>
				<td class="t">It provides the facility to Print the Invoice in any format and not 
					effected in Customer and Products stock, as shown below in figure 74.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/Performa invoice.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 74.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ordercol"></a>9.12</font> Order 
					Collection</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To create and edit a <strong>Order Invoice</strong> click the <strong>
							Order Invoice</strong> link on the Home Page. The following screen appears 
						as shown in figure&nbsp;75 below.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="noi"></a>9.12.1</font> Create a new 
					Order Invoice</td>
			</tr>
			<tr>
				<td class="t">To create a new Order Invoice click on the<strong> Order Invoice</strong>
					link on the Home Page.
					<p>The following screen appears as shown in figure 75.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/OrderCollection.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 75.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select a Customer Name from the drop-down list, The Place 
						Due-Date and Current Balance are populated automatically. Enter the Vehicle No. 
						and select the Sales Type from the drop-down list.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Select the<strong> Under Sales Man</strong> from the drop-down 
						list.</p>
					<p align="justify">Next select the <strong>Product Type</strong>, based on this 
						type the Product Names will appear in the drop-down list, when the Product is 
						selected the<strong> Available Stock</strong> and <strong>Rate</strong> are 
						automatically populated.
					</p>
					<p align="justify">If the Product Type is <strong>not Fuel </strong>then select the <strong>
							Product Type</strong>. Next enter the <strong>Quantity</strong> and click 
						the amount text box to compute and display the <strong>Amount</strong>. A 
						maximum of<strong> twelve</strong> products can be entered in one Order 
						invoice.</p>
					<p align="justify">Enter the <strong>Promo Scheme</strong>, <strong>Remarks</strong>
						and<strong> Message</strong>, and click the <strong>Grand Total</strong> text 
						box, the amount is computed and displayed. Next select the <strong>Cash Discount</strong>
						type and enter the value, click on green radio button to apply the VAT or click 
						red button to remove the applied VAT from amount, select the<strong> Discount type</strong>
						and enter the amount and click the <strong>Net Amount</strong> text box, the 
						amount is computed and displayed. Press <strong>Save &amp; Print</strong> button 
						to save the invoice details and generate a printed invoice report.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="eoi"></a>9.12.2</font> Edit an 
					existing Order Invoice</td>
			</tr>
			<tr>
				<td class="t" height="48">To edit a Order Invoice click on the button besides the 
					Order No. in figure 76.
					<p>The following screen appears as shown in figure&nbsp;76 below.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/forms/OrderCollectionEdit.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 76.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the<strong> Order Invoice No.</strong> from the 
						drop-down list, the order invoice details are automatically populated. Do the 
						necessary modification and press <strong>Save &amp; Print</strong> button to 
						save the changes and generate the printed Order Invoice report.
					</div>
					<p align="justify">The <strong>Quantity </strong>entered should be less than<strong> Available 
							Stock</strong> or an error message will popup.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="modvatcenvat"></a>9.13</font> Mod 
					Vat / Cen Vat Invoice</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">To create and edit a <strong>Cen vat Invoice</strong> click 
						the <strong>Cen vat Invoice</strong> link on the Home Page. The following 
						screen appears as shown in figure&nbsp;77 below.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="nci"></a>9.13.1</font> Create a new 
					Mod vat / Cen vat Invoice</td>
			</tr>
			<tr>
				<td class="t">To create a new Cen vat Invoice click on the<strong> Cen vat Invoice</strong>
					link on the Home Page.
					<p>The following screen appears as shown in figure 77.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Forms/ModVatCenvat.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 77.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select a Customer Name from the drop-down list, The Place 
						Due-Date and Current Balance are populated automatically. Enter the Vehicle No. 
						and select the Sales Type from the drop-down list.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Select the<strong> Under Sales Man</strong> from the drop-down 
						list.</p>
					<p align="justify">Next select the <strong>Product Type</strong>, based on this 
						type the Product Names will appear in the drop-down list, when the Product is 
						selected the<strong> Available Stock</strong> and <strong>Rate</strong> are 
						automatically populated.
					</p>
					<p align="justify">If the Product Type is <strong>not Fuel </strong>then select the <strong>
							Product Type</strong>. Next enter the <strong>Quantity</strong> and click 
						the amount text box to compute and display the <strong>Amount</strong>. A 
						maximum of<strong> twelve</strong> products can be entered in one Order 
						invoice.</p>
					<p align="justify">Enter the <strong>Promo Scheme</strong>, <strong>Remarks</strong>
						and<strong> Message</strong>, and click the <strong>Grand Total</strong> text 
						box, the amount is computed and displayed. Next select the <strong>Cash Discount</strong>
						type and enter the value, click on green radio button to apply the VAT or click 
						red button to remove the applied VAT from amount, select the<strong> Discount type</strong>
						and enter the amount and click the <strong>Net Amount</strong> text box, the 
						amount is computed and displayed. Press <strong>Save &amp; Print</strong> button 
						to save the invoice details and generate a printed invoice report.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="report"></a>10.0</font> Reports/MIS</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">One of the most intriguing aspect of <strong>Servosms</strong> System 
						is that even today, in the age of <strong>Web-based Application</strong>, it 
						supports<strong> DOS-based Printing</strong>, which is by far the most<strong> economical
						</strong>means in terms of <strong>cost</strong>, <strong>time</strong> and <strong>
							maintenance</strong>.
						<br>
						<strong>Servosms</strong> achieves <strong>DOS-based printing</strong> through 
						its own propriety based<strong> Windows Service</strong>.
					</div>
					<p align="justify">This service is provided by <strong>Print_WindowService</strong> 
						which should normally start when your computer boots. Refer to the Installation 
						Guide for the topic <A href="#how">How to start the Print_WindowService</A> for <strong>
							DOS</strong>-based printing.</p>
					<p align="justify">This User-Manual assumes that a <strong>Printer</strong> is 
						connected to your computer system, is powered <strong>ON</strong> and in<strong> online</strong>
						mode, with adequate <strong>stationary</strong> available.</p>
					<p align="justify">Some of the reports in this module requires you to put the 
						printer in the <strong>condensed mode</strong>, wherever applicable a foot-note 
						is provided to indicate the change of mode.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="cws"></a>10.1 </font>Customer Wise 
					Sales</td>
			</tr>
			<tr>
				<td class="t">Click on the <strong>Customer Wise Sales</strong> link and view the 
					report on the screen press the <strong>View </strong>button. the screen appears 
					as shown in figure 78 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Month. Cust.Sec. Sales Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 78.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the <strong>Date From</strong> by clicking the 
						datepicker. Select the <strong>Date To</strong> by clicking the datepicker 
						Select the <strong>Product Group</strong> from the drop-down. Select the <strong>Customer 
							Category</strong> from the drop-down.
					</div>
					<p align="justify">The various Customer categories supported are: <strong>All</strong>,
						<strong>General</strong>, <strong>Fleet</strong>, <strong>Key Customers</strong>,
						<strong>Government</strong> and <strong>Contractor</strong>.</p>
					<p align="justify">To view the<strong> Customer Wise Sales</strong> press <strong>View</strong>
						button, to generate a printout press<strong> Print</strong> button and generate 
						the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="vlbr"></a>10.2</font> Vehicle Log 
					Book Report</td>
			</tr>
			<tr>
				<td class="t">Click on the <strong>Vehicle Log Book Report</strong> link and view 
					the report on the screen press the <strong>View </strong>button. the screen 
					appears as shown in figure 79 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/VehicleLogBookReport.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 79.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Select the<strong> Vehicle No.</strong>, <strong>From Date</strong> and
					<strong>To Date</strong>.
					<p>To view the report press the <strong>View</strong> button and to generate the 
						printout press the <strong>Print</strong> button and generate the excel sheet 
						report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="slr"></a>10.3</font> Stock Ledger 
					Report</td>
			</tr>
			<tr>
				<td class="t" height="29">
					<div align="justify">This report displays all the transaction details of a product. 
						Click on <strong>Stock Ledger Report</strong> link on the Home Page and view 
						the report on the screen press the <strong>View </strong>button. the following 
						screen appears as shown in figure 80.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Stock Ledger Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 80.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the <strong>From Date</strong> and <strong>To Date</strong>
						then select product name from <strong>Product Name</strong> drop down and 
						select the transaction type from <strong>Transaction Type</strong>. Press the <strong>
							View</strong> button to view the report with transaction details between 
						selected<strong> From Date</strong> and <strong>To Date</strong> of a selected 
						product.
					</div>
					<p align="justify">Press the<strong> Print</strong> button to take the printout of 
						generated report and generate the excel sheet report press <strong>Excel</strong>
						button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="lr"></a>10.4</font> Ledger Report</td>
			</tr>
			<tr>
				<td class="t">It provides the facility to make Ledger Report as shown below in 
					figure 81.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head">
					<div align="center"><IMG src="images/Report/Ledger Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 81.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="sl"></a>10.5</font> Scheme List
				</td>
			</tr>
			<tr>
				<td class="t">It provides the facility to view and print list of all the schemes as 
					shown below in figure 82.</td>
			</tr>
			<tr>
				<td class="head">&nbsp;</td>
			</tr>
			<tr>
				<td class="head">
					<div align="center"><IMG src="images/Report/Scheme List Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 82.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="pl"></a>10.6</font> Price List</td>
			</tr>
			<tr>
				<td class="t">Click on the<strong> Price List</strong> link and view the report on 
					the screen press the <strong>View </strong>button. the screen appears as shown 
					in figure&nbsp;83 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Price List Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 83.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">To view the<strong> Price List Report</strong> press <strong>View</strong>
					button, to generate a printout press <strong>Print</strong> button and generate 
					the excel sheet report press <strong>Excel</strong> button.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="mpsr"></a>10.7</font> Monthly 
					Product Sales Report</td>
			</tr>
			<tr>
				<td class="t">It provides the facility to view and print month, product and channel 
					wise sales report as shown below in figure 84.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/MonthWiseProdSales.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 84.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="csr"></a>10.8</font> Customer Sales 
					Report</td>
			</tr>
			<tr>
				<td class="t">It provides the facility to view and print customer, channel and 
					district wise sales report as shown in figure&nbsp;85 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Customerwisesales.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 85.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="mpr"></a>10.9</font> Market 
					Potential Report</td>
			</tr>
			<tr>
				<td class="t">It provides the facility to view and print Market Potential Report as 
					shown in figure 86 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Market Potential Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 86.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="vr"></a>10.10</font> VAT Report</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">This report shows the details of VAT applied to <strong>Sales</strong>
						and <strong>Purchase</strong> invoices. Click on<strong> VAT Report</strong> link 
						on Home Page, the following screen appears as shown in figure 87.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Vat Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 87.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the<strong> Report Type </strong>(i.e. Sales Report, 
						Purchase Report and Both) then select the<strong> Date From</strong> &amp; <strong>To</strong>
						from corresponding date pickers.
					</div>
					<p align="justify">Press<strong> View </strong>button to view the report and to 
						generate the printout press the <strong>Print</strong> button and generate the 
						excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="ta"></a>10.11</font> Trading 
					Account</td>
			</tr>
			<tr>
				<td class="t">Click on<strong> Trading Account</strong> link on a Home Page. The 
					following screen appears as shown in figure 88.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Tradeing Account.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 88.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Select <strong>Date From</strong> and<strong> Date To</strong> dates 
					from date pickers.
					<p>Press the<strong> View </strong>button to view the<strong> Trading Account </strong>
						and <strong>Profit &amp; Loss Account</strong> details between selected <strong>From</strong>
						and <strong>To</strong> dates.</p>
					<p>Press the <strong>Print </strong>button to take the printout and generate the 
						excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="sm"></a>10.12</font> Stock Movement</td>
			</tr>
			<tr>
				<td class="t">Click on the<strong> Stock Movement</strong> link and view the report 
					on the screen press the <strong>View </strong>button. the screen appears as 
					shown in figure 89 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Stock Movement report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 89.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Select the Date From by clicking the datepicker. Select the 
						Date To by clicking the datepicker. Select the Stock Location from the 
						drop-down list.
					</div>
					<p align="justify">To view the <strong>Stock Movement</strong> press<strong> View</strong>
						button, to generate a printout press<strong> Print</strong> button and generate 
						the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a id="srepo" name="srepo"></a>10.13</font> Stock 
					Report</td>
			</tr>
			<tr>
				<td class="t">Click on the <strong>Stock Report</strong> link and view the report 
					on the screen press the <strong>View </strong>button. the screen appears as 
					shown in figure&nbsp;90 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Stock Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 90.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Select the Date by clicking the datepicker. Select the Location from 
					the drop-down list.
					<p>To view the <strong>Stock Report </strong>press<strong> View</strong> button, to 
						generate a printout press <strong>Print </strong>button and generate the excel 
						sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="sb"></a>10.14</font> Sales Book</td>
			</tr>
			<tr>
				<td class="t">Click on the <strong>Sales Book</strong> link and view the report on 
					the screen press the <strong>View </strong>button. the screen appears as shown 
					in figure&nbsp;91 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Sales Book report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 91.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Select the Date From and Date To by clicking the datepickers.
					<p>To view the <strong>Sale Book Report</strong> press <strong>View</strong> button, 
						to generate a printout press<strong> Print</strong> button and generate the 
						excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="pb"></a>10.15</font> Purchase Book</td>
			</tr>
			<tr>
				<td class="t">Click on the <strong>Purchase Book</strong> link and view the report 
					on the screen press the <strong>View </strong>button. the screen appears as 
					shown in figure&nbsp;92 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Purchases book Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 92.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Select the<strong> Date From</strong> and<strong> Date To</strong> by 
					clicking the datepickers
					<p>To view the Purchase Book Report press View button, to generate a printout press 
						Print button and generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="pws"></a>10.16</font> Product Wise 
					Sales</td>
			</tr>
			<tr>
				<td class="t">Click on the<strong> Product Wise Sales</strong> link and view the 
					report on the screen press the <strong>View </strong>button. the screen appears 
					as shown in figure&nbsp;93 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Product Wise report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 93.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Select the Date From and Date To by clicking the datepicker.
					<p>To view the <strong>Product Wise Sales Report </strong>press<strong> View</strong>
						button, to generate a printout press <strong>Print</strong> button and generate 
						the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="co"></a>10.17</font> Customer 
					Outstanding</td>
			</tr>
			<tr>
				<td class="t">Click on the <strong>Customer Outstanding</strong> link and view the 
					report on the screen press the <strong>View </strong>button. the screen appears 
					as shown in figure&nbsp;94 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Customer Wise OutStanding report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 94.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Select the <strong>Date From </strong>and <strong>Date To</strong> by 
					clicking the datepickers. Select the Customer Category from the drop-down list. 
					Select the Balance Type from the drop-down.
					<p>To view the <strong>Customer Wise Outstanding Report</strong> press<strong> View</strong>
						button, to generate a printout press <strong>Print </strong>button and generate 
						the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="cba"></a>10.18</font> Customer Bill 
					Ageing</td>
			</tr>
			<tr>
				<td class="t">Click on the<strong> Customer Bill Ageing</strong> link and view the 
					report on the screen press the <strong>View </strong>button. the screen appears 
					as shown in figure&nbsp;95 below.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Customer Bill Ageing report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 95.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Select the<strong> Date From</strong> and <strong>Date To</strong> by 
					clicking the datepickers.
					<p>To view the <strong>Customer Bill Ageing Report </strong>press <strong>View</strong>
						button, to generate a printout press <strong>Print </strong>button and generate 
						the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="dwr"></a>10.19</font> District Wise 
					Report
				</td>
			</tr>
			<tr>
				<td class="t">It provides the facility to view, print and convert into excel sheet 
					of District Wise Report as shown below in figure 96.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/District Wise Channel Sales.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 96.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="mr"></a>10.20</font> Mechanic 
					Report</td>
			</tr>
			<tr>
				<td class="t">It provides the facility to View and Print Mechanic Report as shown 
					below in figure 97. and also convert into excel sheet with the help of <b>Excel</b>
					button</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Mechenic Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 97.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="bs"></a>10.21</font> Balance Sheet</td>
			</tr>
			<tr>
				<td class="t">Click on <strong>Balance Sheet</strong> link on Home Page and view 
					the report on the screen press the <strong>View </strong>button. the following 
					screen appears as shown in figure 98.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Balance Sheet.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 98.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Select <strong>Date From</strong> and<strong> Date To</strong> dates 
					from date pickers and press the<strong> View</strong> button to view the<strong> Balance 
						Sheet</strong>.
					<p>To take print out of a <strong>Balance Sheet</strong> click the <strong>Print</strong>
						button and generate the excel sheet report press <strong>Excel</strong> button.
					</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="pa"></a>10.22</font> Profit 
					Analysis
				</td>
			</tr>
			<tr>
				<td class="t">It provides the facility to analyze, view and print profit for a 
					given period as shown in figure 99 below and also convert into excel sheet with 
					the help of <b>Excel</b> button.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Profit Analisys.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 99.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="1"></a>10.23</font> Claim Sheet 
					with Stock Movement Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 100.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Sec. Sales claim report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 100.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="2"></a>10.24</font> Fleet/OE 
					Discount Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 101.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Fleet Oe Discount.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 101.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="3"></a>10.25</font> Primary/Secondary 
					Sales Discount Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 102.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t" vAlign="top">
					<div align="center"><img src="images/Report/Primary_Secon. sales Discount.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 102.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="4"></a>10.26 </font>Purchase List 
					for IOCL</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 103.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Purchases List foe IOCL.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 103.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="5"></a>10.27</font> Servo Sadbhavna 
					Enrollment List</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 104.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Sarvo Sadhwavna Enrollment list.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 104.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="6"></a>10.28</font> Servo Sadbhavna 
					Scheme Month Wise Sales &amp; Points Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 105.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/SD SchemeMonthWise.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 105.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="29"></a>10.29 </font>Servo 
					Sadbhavna Scheme Year Wise Sales &amp; Points Report for Disbursement of Gifts</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 106.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/sd Schema year Wise.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 106.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="#30"></a>10.30 </font>Leave Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 107.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Leave Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh" height="14">
					<div align="center">Figure 107.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="31"></a>10.31 </font>Product Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 108.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Product Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 108.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="32"></a>10.32 </font>LY_PS Sales 
					Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 109.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Ly_Ps_Salesreport.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 109.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="33"></a>10.33 </font>Party Wise 
					Sales Figure</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 110.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Party Wise Sales Figure.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 110.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="34"></a>10.34 </font>Monthly Claim 
					Letter</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 111.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Monthly Claim Latter.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 111.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="35"></a>10.35 </font>Attendance 
					Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 112.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Attendance Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 112.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="36"></a>10.36 </font>Bank 
					Reconcilation Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 113.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Bank Reconcillation.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 113.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="37"></a>10.37 </font>Batch Wise 
					Stock Ledger</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 114.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Batch Stock Report.jpg" width="575" w></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 114.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="38"></a>10.38 </font>Batch Wise 
					Stock Ledger Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 115.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Batch Stock Ledger Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 115.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="39"></a>10.39 </font>Credit 
					Analysis Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 116.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Credit period Analisys sheet Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 116.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="40"></a>10.40 </font>Day Book 
					Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 117.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Day Book Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 117.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="37"></a>10.37 </font>District Wise 
					Channel Sales Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 118.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/District Wise Channel Sales.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 118.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="42"></a>10.42 </font>Lube Indent 
					Report</td>
			</tr>
			<tr>
				<td class="t">To viears as sw the report on the screen press the <strong>View </strong>
					button. the following screen appehown in figure 119.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/ssa Lube Indent.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 119.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="43"></a>10.43 </font>Proposed Lube 
					Indent Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 120.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/ProposedLubeIndent.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 120.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="44"></a>10.44 </font>Stock Reorder 
					Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 121.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Reordering report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 121.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="45"></a>10.45 </font>Claim Analysis 
					Report</td>
			</tr>
			<tr>
				<td class="t" height="14">To view the report on the screen press the <strong>View </strong>
					button. the following screen appears as shown in figure 122.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Claim Analisys Report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 122.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="46"></a>10.46 </font>Primary Sec. 
					Sales Analysis Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 123.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Primary Secondry Sales Analisys report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 123.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="47"></a>10.47 </font>Target Vs 
					Achievement Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 124.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Terget Vs Achivment report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 124.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="48"></a>10.48 </font>Price 
					Calculation Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 125.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/Price Calculation report.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 125.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="49"></a>10.49 </font>SSR Incentive 
					Sheet Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 126.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/SSR Incentive Sheet.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 126.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="50"></a>10.50 </font>SSR 
					Performance</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 127.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><img src="images/Report/SSR%20Performance.jpg" width="575" height="330"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 127.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="51"></a>10.51 </font>SSR Wise Sales 
					Figure</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 128.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/SSRWiseSalesFigure.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 128.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="52"></a>10.52 </font>Document 
					Cancel Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 129.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/DocumentCancelReport.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 129.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Print </strong>button to print the details and 
						generate the excel sheet report press <strong>Excel</strong> button.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="53"></a>10.53 </font>Scheme 
					Secondry Sales Report</td>
			</tr>
			<tr>
				<td class="t">To view the report on the screen press the <strong>View </strong>button. 
					the following screen appears as shown in figure 129.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG src="images/Report/SchemSecondrySales.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 129.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<p align="justify">Press <strong>Excel </strong>button to generate the excel sheet 
						report in excel format.</p>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<TR>
				<TD class="t"></TD>
			</TR>
			<TR>
				<TD class="t"></TD>
			</TR>
			<tr>
				<td class="head"><font color="#000000"><a id="pwser" name="pwser"></a>11</font> How 
					to Start the Print_WindowsService</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Incase the <strong>Print_WindowsService</strong> is stopped 
						then to start the <strong>Print_WindowsService</strong> as a windows service, 
						right click on <strong>My Computer</strong> then the following popup menu 
						appears as shown in figure 130.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG height="186" src="images/image205.jpg" width="178"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 130.<br>
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Click on<strong> Manage </strong>to open the <strong>Computer Management</strong>
					window as shown in following figure 131.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG height="425" src="images/image207.jpg" width="638"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 131.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">Double click on <strong>Services and</strong> <strong>Applications</strong>
					to expand as shown in following figure 132.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG height="385" src="images/image209.jpg" width="638"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 132.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Click on<strong> Services</strong>, you will see the list of 
						all the window services on right side of the<strong> Computer Management</strong>
						window. Find the <strong>Print_WindowsServices</strong> and right click on it, 
						as shown in following figure 133.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG height="424" src="images/Services.jpg" width="638"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 133.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Click on <strong>Start</strong> to start the<strong> Print_WindowsService</strong>. 
						After starting the service the status of <strong>Print_WindowsService</strong> will 
						change to <strong>Started</strong> as shown following figure 134.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="center"><IMG height="385" src="images/ServicesStarted.jpg" width="638"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 134.
					</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="t">
					<div align="justify">Close the <strong>Computer Management</strong> and check the <strong>
							Print_WindowsService</strong> is started or not from the <strong>Servosms</strong>
						application, if not started then again <strong>restart</strong> the service.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="InvoiceDesigner"></a>12</font> How 
					to design Sales Invoice print layout With the help of InvoiceDesigner.exe</td>
			</tr>
			<tr>
				<td class="t">All values must be enter in inches according to page height and page 
					width as shown following figure 135.</td>
			</tr>
			<tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
			<tr>
				<td class="t">
					<div align="center"><IMG height="421" src="images/Invoicedesigner with Value.jpg" width="575"></div>
				</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="center">Figure 135.</div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
			<tr>
				<td class="t">Height and width should be started from effective print.</td>
			</tr>
			<tr>
				<td class="t">Position1 and Position2 are indicate the width of Party Name and 
					Document No from effective print.</td>
			</tr>
			<tr>
				<td class="t">If checkbox is checked then this field are display in Sales Invoice 
					form in printing time.</td>
			</tr>
			<tr>
				<td class="t">If click the Generate Template button then generate and save the 
					template in 
					c:\inetpub\wwwroot\ServoSMS\InvoiceDesigner\SalesInvoicePrePrintTemplate.INI.</td>
			</tr>
			<tr>
				<td class="t">If click the Load button then load the template from any location but 
					that file extension should be .INI.</td>
			</tr>
			<tr>
				<td class="t">If click the SaveAs button then save the template in any location 
					with .INI extension.</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="uh">
					<div align="right"><A href="#toc">Back</A></div>
				</td>
			</tr>
			<tr>
				<td class="t">&nbsp;</td>
			</tr>
			<tr>
				<td class="head"><font color="#000000"><a name="logout"></a>13</font> Logout</td>
			</tr>
			<tr>
				<td class="t">After you are finished using the <strong>Servosms</strong> system, you 
					have to <strong>Log-Out</strong>. To keep away malicious users from misusing 
					your confidential data it is mandatory to Log-Out from the Servosms system.
					<br>
					<br>
					To Log Out click on the link<font color="#cc0000"><strong> LogOut </strong></font>
					on Home Page.<br>
					<br>
					When you logout the<strong> Login</strong> screen appears.<br>
					<br>
					<strong>Thank you</strong> for using <strong>Servosms</strong> system. For any <strong>
						queries</strong> please feel free to contact <strong>bbnisys Technologies</strong>. 
					The<strong> Help</strong> menu has <strong>Contact Details</strong> which will 
					help<strong> you</strong> to reach<strong> us</strong>.
				</td>
			</tr>
		</table>
	</body>
</HTML>
