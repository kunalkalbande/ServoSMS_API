/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/

using System;
using System.Data;
using System.Data.SqlClient;

namespace Servosms.Sysitem.Classes
{
	/// <summary>
	/// Summary description for InventoryClass1.
	/// </summary>
	public class InventoryClass1
	{
		SqlConnection SqlCon;
		SqlCommand SqlCmd;
		SqlDataReader SqlDtr;

		#region	Vars & Properties
		string _Prod_ID;
		string _Prod_Name;
		string _Prod_Type;
		string _Unit;
		string _Pur_ID;
		string _Pur_Date;
		string _Cheque_No;
		string _Cheque_Date;
		string _Supp_ID;
		string _Qty;
		string _Rate;
		string _Amount;
		string _Discount;
		string _Promo_Scheme;
		string _Sal_ID;
		string _Sal_Date;
		string _Cust_ID;
		string _Vendor_ID;
		string _Mode_of_Payment;
		string _vendor_Invoice_No;
		string _Vendor_Invoice_Date;
		string _Density_in_Invoice;
		string _Density_in_Physical;
		string _Density_Variation;
		string _Invoice_No;
		string _Invoice_Date;
		string _Sales_Type;
		string _Under_Sales_Man;
		string _Message;
		string _Vehicle_No;
		string _Prod_Balance;
		string _Due_Date;
		string _Slip_No;
		string _Pur_Return_ID;
		string _Pur_Return_Date;
		string _Sal_Return_ID;
		string _Sal_Return_Date;
		string _Remark;
		string _PurchaseRate;
		string _PurchaseSale;
   
		public string PurchaseRate
		{
			get
			{
				return _PurchaseRate;
			}
			set
			{
				_PurchaseRate=value;
			}
		}
		
		public string PurchaseSale
		{
			get
			{
				return _PurchaseSale;
			}
			set
			{
				_PurchaseSale=value;
			}
		}
		
		public string Prod_ID
		{
			get
			{
				return _Prod_ID;
			}
			set
			{
				_Prod_ID=value;
			}
		}

		public string Prod_Name
		{
			get
			{
				return _Prod_Name;
			}
			set
			{
				_Prod_Name=value;
			}
		}
		public string Prod_Type
		{
			get
			{
				return _Prod_Type;
			}
			set
			{
				_Prod_Type=value;
			}
		}
		public string Mode_of_Payment
		{
			get
			{
				return _Mode_of_Payment;
			}
			set
			{
				_Mode_of_Payment=value;
			}
		}
		public string Pack_Type
		{
			get
			{
				return _Prod_Type;
			}
			set
			{
				_Prod_Type=value;
			}
		}
		public string Unit
		{
			get
			{
				return _Unit;
			}
			set
			{
				_Unit=value;
			}
		}
		public string Pur_ID
		{
			get
			{
				return _Pur_ID ;
			}
			set
			{
				_Pur_ID =value;
			}
		}
		public string Pur_Date
		{
			get
			{
				return _Pur_Date ;
			}
			set
			{
				_Pur_Date =value;
			}
		}
		public string Cheque_No
		{
			get
			{
				return _Cheque_No  ;
			}
			set
			{
				_Cheque_No  =value;
			}
		}
		public string Cheque_Date
		{
			get
			{
				return _Cheque_Date  ;
			}
			set
			{
				_Cheque_Date  =value;
			}
		}
		public string Supp_ID
		{
			get
			{
				return _Supp_ID  ;
			}
			set
			{
				_Supp_ID =value;
			}
		}
		public string Qty
		{
			get
			{
				return _Qty;
			}
			set
			{
				_Qty =value;
			}
		}
		public string Rate
		{
			get
			{
				return _Rate ;
			}
			set
			{
				_Rate =value;
			}
		}
		public string Amount
		{
			get
			{
				return _Amount ;
			}
			set
			{
				_Amount =value;
			}
		}
		public string Discount
		{
			get
			{
				return _Discount ;
			}
			set
			{
				_Discount =value;
			}
		}
		public string Promo_Scheme
		{
			get
			{
				return _Promo_Scheme;
			}
			set
			{
				_Promo_Scheme=value;
			}
		}
		public string Invoice_No
		{
			get
			{
				return _Invoice_No ;
			}
			set
			{
				_Invoice_No=value;
			}
		}
		public string Invoice_Date
		{
			get
			{
				return _Invoice_Date ;
			}
			set
			{
				_Invoice_Date=value;
			}
		}
		public string Sales_Type
		{
			get
			{
				return _Sales_Type;
			}
			set
			{
				_Sales_Type=value;
			}
		}
		public string Under_Sales_Man
		{
			get
			{
				return _Under_Sales_Man;
			}
			set
			{
				_Under_Sales_Man=value;
			}
		}
		public string Message
		{
			get
			{
				return _Message;
			}
			set
			{
				_Message=value;
			}
		}
		public string Customer_ID
		{
			get
			{
				return _Cust_ID ;
			}
			set
			{
				_Cust_ID =value;
			}
		}
		public string Vendor_ID
		{
			get
			{
				return _Vendor_ID  ;
			}
			set
			{
				_Vendor_ID  =value;
			}
		}
		public string Vendor_Invoice_No
		{
			get
			{
				return _vendor_Invoice_No   ;
			}
			set
			{
				_vendor_Invoice_No   =value;
			}
		}
		public string Vendor_Invoice_Date
		{
			get
			{
				return _Vendor_Invoice_Date   ;
			}
			set
			{
				_Vendor_Invoice_Date   =value;
			}
		}
		public string Density_in_Invoice
		{
			get
			{
				return _Density_in_Invoice ;
			}
			set
			{
				_Density_in_Invoice =value;
			}
		}
		public string Density_in_Physical
		{
			get
			{
				return _Density_in_Physical ;
			}
			set
			{
				_Density_in_Physical =value;
			}
		}
		public string Density_Variation
		{
			get
			{
				return _Density_Variation ;
			}
			set
			{
				_Density_Variation =value;
			}
		}
		public string Vehicle_No
		{
			get
			{
				return _Vehicle_No;
			}
			set
			{
				_Vehicle_No=value;
			}
		}
		public string Product_Balance
		{
			get
			{
				return _Prod_Balance ;
			}
			set
			{
				_Prod_Balance =value;
			}
		}
		public string Due_Date
		{
			get
			{
				return _Due_Date;
			}
			set
			{
				_Due_Date=value;
			}
		}
		public string Pur_Return_ID
		{
			get
			{
				return _Pur_Return_ID;
			}
			set
			{
				_Pur_Return_ID=value;
			}
		}
		public string Pur_Return_Date
		{
			get
			{
				return _Pur_Return_Date;
			}
			set
			{
				_Pur_Return_Date=value;
			}
		}
		public string Sal_Return_ID
		{
			get
			{
				return _Sal_Return_ID;
			}
			set
			{
				_Sal_Return_ID=value;
			}
		}
		public string Sal_Return_Date
		{
			get
			{
				return _Sal_Return_Date;
			}
			set
			{
				_Sal_Return_Date=value;
			}
		}
		public string Remark
		{
			get
			{
				return _Remark;
			}
			set
			{
				_Remark=value;
			}
		}
		public string Sal_ID
		{
			get
			{
				return _Sal_ID;
			}
			set
			{
				_Sal_ID=value;
			}
		}
		public string Sal_Date
		{
			get
			{
				return _Sal_Date;
			}
			set
			{
				_Sal_Date=value;
			}
		}
		public string Cust_ID
		{
			get
			{
				return _Cust_ID;
			}
			set
			{
				_Cust_ID=value;
			}
		}
		public string Slip_No
		{
			get
			{
				return _Slip_No;
			}
			set
			{
				_Slip_No=value;
			}
		}
		#endregion

		#region Constructor opens the connection to the database server & Destructor

		public InventoryClass1()
		{
			SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			SqlCon.Open();	
		}

		#endregion

		#region Product Module
		// Returns the SqlDataReader object containing the max product ID from table product.
		public SqlDataReader  GetNextProductID()
		{			
			SqlCmd=new SqlCommand("select max(Prod_ID)+1 from Product",SqlCon);
			SqlDtr =SqlCmd.ExecuteReader();  
			return SqlDtr;
		}

		// Returns SqlDataReader containing the product IDs From table product.
		public SqlDataReader GetProductID()
		{			
			SqlCmd=new SqlCommand("select Prod_ID from Product",SqlCon);
			SqlDtr =SqlCmd.ExecuteReader();  
			return SqlDtr;
		}

		// Method call the Procedure ProProductEntry to insert the Product Details
		public void InsertProduct()
		{ 				
			SqlCmd=new SqlCommand("ProProductEntry",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Prod_ID",Prod_ID);
			SqlCmd.Parameters .Add("@Prod_Name",Prod_Name);
			SqlCmd.Parameters .Add("@Prod_Type",Prod_Type);
			SqlCmd.Parameters.Add("@PurchaseRate",PurchaseRate);
			SqlCmd.Parameters.Add("@PurchaseSale",PurchaseSale);
			SqlCmd.Parameters .Add("@Pack_Type",Pack_Type);
			SqlCmd.Parameters .Add("@Unit",Unit);
			SqlCmd.ExecuteNonQuery();
		}
		
		// Returns the Product List Containing the  List of all the Products with details from table products for passing product ID as a Parameter.
		public SqlDataReader ProductList(string ID)
		{
			#region Query
			string sql;
			sql="select * from Product";

			if(ID!="")
			{
				sql=sql+" where Prod_ID=" + ID;
			}

			#endregion

			SqlCmd=new SqlCommand (sql,SqlCon );
			SqlDtr   = SqlCmd.ExecuteReader();
			return SqlDtr;			
		}
		#endregion

		/// <summary>
		/// Returns the SqlDataReader Object containing the max. Purchase ID from purchase table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  GetNextPurchaseID()
		{			
			SqlCmd=new SqlCommand("select max(Invoice_No)+1 from Purchase",SqlCon);
			SqlDtr =SqlCmd.ExecuteReader();  
			return SqlDtr;
		}

		/// <summary>
		/// Returns the SqlDataReader Object containing the Purchase ID  from Purchase Table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  GetPurchaseID()
		{			
			SqlCmd=new SqlCommand("select Invoice_No from Purchase",SqlCon);
			SqlDtr =SqlCmd.ExecuteReader();  
			return SqlDtr;
		}

		/// <summary>
		/// Method calls the Procedure ProPurchaseEntry to insert the Purchase Details
		/// </summary>
		public void InsertPurchase()
		{ 				
			SqlCmd=new SqlCommand("ProPurchaseEntry",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Invoice_No",Invoice_No );
			SqlCmd.Parameters .Add("@Invoice_Date",Invoice_Date );
			SqlCmd.Parameters .Add("@Mode_of_Payment",Mode_of_Payment);
			SqlCmd.Parameters .Add("@Cheque_No",Cheque_No);
			SqlCmd.Parameters .Add("@Cheque_Date",Cheque_Date);
			SqlCmd.Parameters .Add("@Vehicle_No",Vehicle_No );
			SqlCmd.Parameters .Add("@Prod_ID",Prod_ID);
			SqlCmd.Parameters .Add("@Qty",Qty);
			SqlCmd.Parameters .Add("@Rate",Rate);
			SqlCmd.Parameters .Add("@Amount",Amount);
			SqlCmd.Parameters .Add("@Discount",Discount);
			SqlCmd.Parameters .Add("@Vendor_ID",Vendor_ID  );
			SqlCmd.Parameters .Add("@Vendor_Invoice_No",Vendor_Invoice_No);
			SqlCmd.Parameters .Add("@Vendor_Invoice_Date",Vendor_Invoice_Date);
			SqlCmd.Parameters .Add("@Density_in_Invoice",Density_in_Invoice);
			SqlCmd.Parameters .Add("@Density_in_Physical",Density_in_Physical );
			SqlCmd.Parameters .Add("@Density_variation",Density_Variation );
			SqlCmd.Parameters .Add("@Due_Date",Due_Date);
			SqlCmd.Parameters .Add("@Promo_Scheme ",Promo_Scheme );
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Returns the SqlDataReader Object containing the max. Purchase Return ID from Purchase_Return Table
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextPurchaseReturnID()
		{			
			SqlCmd=new SqlCommand("select max(PR_ID)+1 from Purchase_Return",SqlCon);
			SqlDtr =SqlCmd.ExecuteReader();  
			return SqlDtr;
		}

		/// <summary>
		/// Method calls the Procedure ProPurchaseReturnEntry to insert the Purchase Return Details
		/// </summary>
		public void InsertPurchaseReturn()
		{ 				
			SqlCmd=new SqlCommand("ProPurchaseReturnEntry",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@PR_ID",Pur_Return_ID);
			SqlCmd.Parameters .Add("@PR_Date",Pur_Return_Date );
			SqlCmd.Parameters .Add("@Against_Pur_ID ",Pur_ID );
			SqlCmd.Parameters .Add("@Qty",Qty);
			SqlCmd.Parameters .Add("@Remark",Remark );
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Returns the SqlDataReader Object containing the max Invoice No. from table sales
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
 		public SqlDataReader  GetNextSalesID()
		{			
			SqlCmd=new SqlCommand("select max(Invoice_No)+1 from Sales",SqlCon);
			SqlDtr =SqlCmd.ExecuteReader();  
			return SqlDtr;
		}


		/// <summary>
		/// Method calls the Procedure ProSalesEntry to insert the sales Details
		/// This method is not used.
		/// </summary>
		public void InsertSales()
		{ 				
			SqlCmd=new SqlCommand("ProSalesEntry",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Invoice_No",Invoice_No );
			SqlCmd.Parameters .Add("@Invoice_Date",Invoice_Date  );
			SqlCmd.Parameters .Add("@Sales_Type",Sales_Type  );
			SqlCmd.Parameters .Add("@Under_SalesMan",Under_Sales_Man );
			SqlCmd.Parameters .Add("@Qty",Qty);
			SqlCmd.Parameters .Add("@Rate",Rate );
			SqlCmd.Parameters .Add("@Amount",Amount);
			SqlCmd.Parameters .Add("@Discount",Discount);
			SqlCmd.Parameters .Add("@Promo_Scheme",Promo_Scheme);
			SqlCmd.Parameters .Add("@Message",Message);
			SqlCmd.Parameters .Add("@Cust_ID",Cust_ID );
			SqlCmd.Parameters .Add("@Vehicle_No",Vehicle_No);
			SqlCmd.Parameters .Add("@Prod_ID",Prod_ID );
			SqlCmd.Parameters .Add("@Prod_Balance",Product_Balance );
			SqlCmd.Parameters .Add("@Due_Date",Due_Date);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Returns the SqlDataReader object containing the max. Credit Sales ID from table Credit sales.
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  GetNextCreditSalesID()
		{			
			SqlCmd=new SqlCommand("select max(Sal_ID)+1 from Credit_Sales",SqlCon);
			SqlDtr =SqlCmd.ExecuteReader();  
			return SqlDtr;
		}

		/// <summary>
		/// Returns the SqlDataReader object containing the Credit Sales  ID from Sales table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  GetCreditSalesID()
		{			
			SqlCmd=new SqlCommand("select Sal_ID from Credit_Sales",SqlCon);
			SqlDtr =SqlCmd.ExecuteReader();  
			return SqlDtr;
		}

		/// <summary>
		/// Method call the Procedure ProCreditSalesEntry to insert the Credit Sales Details
		/// This method is not used.
		/// </summary>
		public void InsertCreditSales()
		{ 				
			SqlCmd=new SqlCommand("ProCreditSalesEntry",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Sal_ID",Sal_ID );
			SqlCmd.Parameters .Add("@Sal_Date",Sal_Date );
			SqlCmd.Parameters .Add("@Cust_ID ",Cust_ID  );
			SqlCmd.Parameters .Add("@Prod_ID",Prod_ID);
			SqlCmd.Parameters .Add("@Slip_No",Slip_No);
			SqlCmd.Parameters .Add("@Qty",Qty);
			SqlCmd.Parameters .Add("@Rate",Rate);
			SqlCmd.Parameters .Add("@Amount",Amount);
			SqlCmd.Parameters .Add("@Discount",Discount);
			SqlCmd.Parameters .Add("@Promo_Scheme ",Promo_Scheme );
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Returns the max Cash Sales ID from table Cash_Sales as a SqlDataReader object.
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextCashSalesID()
		{			
			SqlCmd=new SqlCommand("select max(Sal_ID)+1 from Cash_Sales",SqlCon);
			SqlDtr =SqlCmd.ExecuteReader();  
			return SqlDtr;
		}

		/// <summary>
		/// Method call the Procedure ProCashSalesEntry to insert the Cash Sales Details
		/// This method is not used.
		/// </summary>
		public void InsertCashSales()
		{ 				
			SqlCmd=new SqlCommand("ProCashSalesEntry",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Sal_ID",Sal_ID );
			SqlCmd.Parameters .Add("@Sal_Date",Sal_Date );
			SqlCmd.Parameters .Add("@Prod_ID",Prod_ID);
			SqlCmd.Parameters .Add("@Qty",Qty);
			SqlCmd.Parameters .Add("@Rate",Rate);
			SqlCmd.Parameters .Add("@Amount",Amount);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Returns the SqlDataReader Object containing the max. Sales Return ID from table Sales_Return.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextSalesReturnID()
		{			
			SqlCmd=new SqlCommand("select max(SR_ID)+1 from Sales_Return",SqlCon);
			SqlDtr =SqlCmd.ExecuteReader();  
			return SqlDtr;
		}

		/// <summary>
		/// Method call the Procedure ProSalesReturntEntry to insert the Sales Return Details
		/// </summary>
		public void InsertSalesReturn()
		{ 				
			SqlCmd=new SqlCommand("ProSalesReturnEntry",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@SR_ID",Sal_Return_ID);
			SqlCmd.Parameters .Add("@SR_Date",Sal_Return_Date );
			SqlCmd.Parameters .Add("@Against_Sal_ID ",Sal_ID );
			SqlCmd.Parameters .Add("@Qty",Qty);
			SqlCmd.Parameters .Add("@Remark",Remark );
			SqlCmd.ExecuteNonQuery();
		}
	}
}