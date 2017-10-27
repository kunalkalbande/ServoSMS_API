/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/

using System;
using System.Data ;
using System.Data.SqlClient  ;

namespace Servosms.Sysitem.Classes
{
	/// <summary>
	/// Summary description for Master.
	/// </summary>
	public class MasterClass
	{
		SqlConnection SqlCon;
		SqlCommand SqlCmd;
		SqlDataReader SqlDtRed;
		SqlDataAdapter sqdr;
		DataSet ds;

		#region Vars & Prop

		string _Slip_Book_ID;
		string _Book_No;
		string _Start_No;
		string _End_No;
		string _Cust_ID;
		string _Shift_ID;
		string _Shift_Name;
		string _Time_From;
		string _Time_To;
		string _Remark;
        string _CategoryID;
		string _CategoryName;
        string _Product_ID;
		string _Product_Name;
		string _Category;
		string _Package_ID;
		string _PackageID;
		string _PackageType;
		string _Unit;
		string _Total_Qty;
		
		public string PackageID
		{
			get
			{
				return _PackageID;
			}
			set
			{
				_PackageID=value;
			}
		}
		
		public string PackageType
		{
			get
			{
				return _PackageType;
			}
			set
			{
				_PackageType=value;
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
		
		public string Total_Qty
		{
			get
			{
				return _Total_Qty;
			}
			set
			{
				_Total_Qty=value;
			}
		}

	

		public string Product_ID
		{
			get
			{
				return _Product_ID;
			}
			set
			{
				_Product_ID=value;
			}
		}
		
		public string Product_Name
		{
			get
			{
				return _Product_Name;
			}
			set
			{
				_Product_Name=value;
			}
		}
		
		public string Category
		{
			get
			{
				return _Category;
			}
			set
			{
				_Category=value;
			}
		}
		
		public string Package_ID
		{
			get
			{
				return _Package_ID;
			}
			set
			{
				_Package_ID=value;
			}
		}
		
		
	
		
		public string CategoryID
		{
			get
			{
				return _CategoryID;
			}
			set
			{
				_CategoryID=value;
			}
		}
		
		public string CategoryName
		{
			get
			{
				return _CategoryName;
			}
			set
			{
				_CategoryName=value;
			}
		}


		public string Slip_Book_ID
		{
			get
			{
				return _Slip_Book_ID;
			}
			set
			{
				_Slip_Book_ID=value;
			}
		}
		public string Book_No
		{
			get
			{
				return _Book_No;
			}
			set
			{
				_Book_No=value;
			}
		}
		public string Start_No
		{
			get
			{
				return _Start_No;
			}
			set
			{
				_Start_No=value;
			}
		}
		public string End_No
		{
			get
			{
				return _End_No;
			}
			set
			{
				_End_No=value;
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
		public string Shift_ID
		{
			get
			{
				return  _Shift_ID;
			}
			set
			{
				_Shift_ID=value;
			}
		}
		public string Shift_Name
		{
			get
			{
				return  _Shift_Name;
			}
			set
			{
				_Shift_Name=value;
			}
		}
		public string Time_From
		{
			get
			{
				return _Time_From;
			}
			set
			{
				_Time_From=value;
			}
		}
		public string Time_To
		{
			get
			{
				return _Time_To;
			}
			set
			{
				_Time_To=value;
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
		#endregion

		#region Constructor: Opens the connection with the Database  & Destructor

		public MasterClass()
		{
			SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			SqlCon.Open();	
		}

		#endregion

		/// <summary>
		/// Returns the SqlDataReader object containing the Max. Slip Book ID from table Slip
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextSlipID()
		{			
			SqlCmd=new SqlCommand("select max(Slip_Book_ID)+1 from Slip",SqlCon);
			SqlDtRed =SqlCmd.ExecuteReader();  
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProInsertSlip to insert the Slip Details
		/// </summary>
		public void InsertSlip_Book()
		{ 				
			SqlCmd=new SqlCommand("ProInsertSlip",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Slip_Book_ID",Slip_Book_ID);
			SqlCmd.Parameters .Add("@Book_No",Book_No);
			SqlCmd.Parameters .Add("@Start_No",Start_No);
			SqlCmd.Parameters .Add("@End_No",End_No);
			SqlCmd.Parameters .Add("@Cust_ID",Cust_ID);
			SqlCmd.ExecuteNonQuery();

		}

		/// <summary>
		/// Returns the SqlDataReader Object containing the max. shift Id from Shift.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextShiftID()
		{
			SqlCmd=new SqlCommand ("select max(Shift_ID)+1 from Shift",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
					
		}

		/// <summary>
		/// Returns the SqlDataReader containing the max. product id from ProductMaster table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextProductID()
		{
			SqlCmd=new SqlCommand ("select max(Product_ID)+1 from ProductMaster",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
					
		}
		
		/// <summary>
		/// Returns the SqlDataReader containing the max. Category ID from category table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextCategoryID()
		{
			SqlCmd=new SqlCommand ("select max(CategoryID)+1 from Category",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Returnns the SqlDataReader containing the Category ID for all the Categories from table Category
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowCategory()
		{
			SqlCmd=new SqlCommand ("select CategoryID from Category",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Returns the SqlDataReader containing the Product ID for all the products from table ProductMaster
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowProductID()
		{
			SqlCmd=new SqlCommand ("select Product_ID from ProductMaster",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
					
		}

		/// <summary>
		/// Returns the SqlDataReader containing the Package ID for all the packages from table package
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowPackageID()
		{
			SqlCmd=new SqlCommand ("select PackageID from Package",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
		}
		
		/// <summary>
		/// Returns the SqlDataReader containing the Max. Package ID from table Package
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextpackageID()
		{
			SqlCmd=new SqlCommand ("select max(PackageID)+1 from Package",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Returns the SqlDataReader containing the Shift_name for all the shifts from table shift.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  GetShiftID()
		{
			SqlCmd =new SqlCommand ("select Shift_Name from Shift",SqlCon );
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProInsertShift to insert the Shift Details
		/// </summary>
		public void InsertShift()
		{ 
			SqlCmd=new SqlCommand("proInsertShift",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@Shift_ID",Shift_ID );
			SqlCmd.Parameters.Add("@Shift_Name",Shift_Name);
			SqlCmd.Parameters.Add("@Time_From",Time_From );
			SqlCmd.Parameters.Add("@Time_To",Time_To );
			SqlCmd.Parameters.Add("@Remark",Remark);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProUpdateShift to Update the Shift Details
		/// </summary>
		public void UpdateShift()
		{ 
			SqlCmd=new SqlCommand("proUpdateShift",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@Shift_ID",Shift_ID );
			SqlCmd.Parameters.Add("@Shift_Name",Shift_Name);
			SqlCmd.Parameters.Add("@Time_From",Time_From );
			SqlCmd.Parameters.Add("@Time_To",Time_To );
			SqlCmd.Parameters.Add("@Remark",Remark);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProDeleteShift to Delete the Shift Details
		/// </summary>
		public void DeleteShift()
		{ 
			SqlCmd=new SqlCommand("proDeleteShift",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@Shift_ID",Shift_ID );
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProInsertCategory to insert the Category Details
		/// </summary>
		public void InsertCategory()
		{ 
			SqlCmd=new SqlCommand("ProInsertCategory",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@CategoryID",CategoryID );
			SqlCmd.Parameters.Add("@CategoryName",CategoryName );
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProInsertProductData to insert the Product Details
		/// </summary>
		public void InsertProduct()
		{
			SqlCmd=new SqlCommand("ProInsertproductdata",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@Product_ID",Product_ID);
			SqlCmd.Parameters.Add("@Product_Name",Product_Name );
			SqlCmd.Parameters.Add("@Category",Category );
			SqlCmd.Parameters.Add("@Package_ID",Package_ID );
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProInsertPackageData to insert the Package Details
		/// </summary>
		public void InsertPackage()
		{ 
			SqlCmd=new SqlCommand("ProInsertPackagedata",SqlCon );
			SqlCmd.CommandType=CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@PackageID",PackageID );
			SqlCmd.Parameters.Add("@PackageType",PackageType );
			SqlCmd.Parameters.Add("@Unit",Unit );
			SqlCmd.Parameters.Add("@Total_Qty",Total_Qty );
			SqlCmd.ExecuteNonQuery();
		}
	
		/// <summary>
		/// Calls the Procedure ProShowcategory and Returns the SqlDataReader containing the Category Details.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  ShowCategoryName()
		{
			SqlCmd =new SqlCommand ("procShowCategory",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@CategoryID",CategoryID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure showpackage  and Retruns the SqlDataReader Object containing the package details
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  ShowPackage()
		{
			SqlCmd =new SqlCommand ("showpackage",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@PackageID",PackageID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProDeletecategory to Delete the Category Details
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  DeleteCategory()
		{
			SqlCmd =new SqlCommand ("procShowdelete",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@CategoryID",CategoryID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProDeleteProduct and Returns the SqlDataReader Object
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  DeleteProduct()
		{
			SqlCmd =new SqlCommand ("procShowdeleteproduct",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@Product_ID",Product_ID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProDeletePackage and Returns the SqlDataReader object.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  DeletePackage()
		{
			SqlCmd =new SqlCommand ("ProDeletepackage",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@PackageID",PackageID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProShowEditProduct and Returns the SqlDataReader Object
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  editproduct()
		{
			SqlCmd =new SqlCommand ("proShowProductEdit",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@Product_ID",Product_ID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProCategoryUpdate to update the Product Category
		/// </summary>
		public void UpdateCategory()
		{ 
			SqlCmd=new SqlCommand("ProCategoryUpdate",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@CategoryID",CategoryID );
			SqlCmd.Parameters .Add("@CategoryName",CategoryName );
			SqlCmd.ExecuteNonQuery();       
		}
		
		/// <summary>
		/// Calls the Procedure ProPackageUpdateData to Update the Package Details
		/// </summary>
		public void UpdatePackage()
		{
			SqlCmd=new SqlCommand("ProPackegeUpdatedata",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@PackageID",PackageID );
			SqlCmd.Parameters .Add("@PackageType ",PackageType  );
			SqlCmd.Parameters .Add("@Unit",Unit );
			SqlCmd.Parameters .Add("@Total_Qty ",Total_Qty  );
			SqlCmd.ExecuteNonQuery();       
		}
	
		/// <summary>
		/// Calls the Procedure ProProductUpdate to Update the Product Details
		/// </summary>
		public void UpdateProduct()
		{ 
			SqlCmd=new SqlCommand("ProProductUpdate",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Product_ID",Product_ID );
			SqlCmd.Parameters .Add("@Product_Name",Product_Name );
			SqlCmd.Parameters .Add("@Category",Category );
			SqlCmd.Parameters .Add("@Package_ID",Package_ID );
			SqlCmd.ExecuteNonQuery();       
		}

		/// <summary>
		/// Calls the Procedure Proshoecategoryinfo and returns the DataSet Object.
		/// </summary>
		/// <returns></returns>
		public DataSet ShowCategoryInfo()
		{
			sqdr=new SqlDataAdapter("proshowcategoryinfo",SqlCon);
			ds=new DataSet();
			sqdr.Fill(ds);
			SqlCon.Close();
			SqlCon.Dispose();
			return ds;
		}

		/// <summary>
		/// Calls the Procedure ProshowPackageinfo and returns DataSet Object
		/// </summary>
		/// <returns></returns>
		public DataSet ShowPackageInfo()
		{
			sqdr=new SqlDataAdapter("proshowPackageinfo",SqlCon);
			ds=new DataSet();
			sqdr.Fill(ds);
			SqlCon.Close();
			SqlCon.Dispose();
			return ds;
		}

		/// <summary>
		/// Calls the Procedure ProProductInfo and Returns the DataSet Object
		/// </summary>
		/// <returns></returns>
		public DataSet ShowProductInfo()
		{
			sqdr=new SqlDataAdapter("proshowProductinfo",SqlCon);
			ds=new DataSet();
			sqdr.Fill(ds);
			SqlCon.Close();
			SqlCon.Dispose();
			return ds;
		}
	}
}