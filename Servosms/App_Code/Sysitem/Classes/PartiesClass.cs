/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
using System;
using System.Data.SqlClient;
using System.Data;

namespace Servosms.Sysitem.Classes
{
	/// <summary>
	/// Summary description for PartiesClass.
	/// </summary>
	public class PartiesClass
	{
		SqlConnection sqcon;
		SqlConnection SqlCon;
		SqlCommand sqcom;
		SqlCommand SqlCmd;
		SqlDataReader sqred;
		SqlDataAdapter SqlAdp;
		SqlCommand sqcomm;
		SqlDataReader sqdr;
		SqlDataReader SqlDtRed;
		DataSet ds;

		#region Vars & Prop
		string _Supp_ID;
		string _Cust_ID;
		string _sadbhavnacd;
		string _Supp_Name;
		string _Cust_Name;
		string _Type;
		string _Cust_Type;
		string _Address;
		string _City;
		string _State;
		string _Country;
		string _SSR;
		string _Tel_Res;
		string _Tel_Off;
		string _Mobile;
		string _EMail;
		string _EntryDate;
		string _Op_Balance;
		string _Contact_Person;
		string _CR_Limit;
		string _CR_Days;
		string _u_type;
		string _u_name;
		string _u_pass;
		string _User_Type;
		string _User_Name;
		string _User_Password;
		string _First_Name;
		string _MiDrope_Name;
		string _Last_Name;
		string _Per_Address;
		string _Local_Address;
		string _PinCode;
		string _Phone_Number;
		string _EMail_ID;
		string _Date_OF_Birth;
		string _Age;
		string _Gender;
		string _Father_Name;
		string _Mother_Name;
		string _marital_Status;
		string _Emp_ID;
		string _OT_Date;
		string _OT_From;
		string _OT_To;
		string _Shift_ID;
		string _Time_From;
		string _Time_To;
		string _Remark;
		string _Status;
		string _Tank_ID;
		string _Tank_Name;
		string _Capacity;
		string _Temperature;
		string _Product;
		string _Dip_ID;
		string _Dip_Date;
		string _Reading;
		string _Machine_ID;
		string _Tank_Id;
		string _No_of_Nozzel;
		string _Meter_ID;
		string _Reading_Date;
		string _Nozel_ID;
		string _Start_Reading;
		string _End_reading;
		string _Item_ID;
		string _Prod_ID;
		string _Qty;
		string _Nozzle_ID;		
		string _WH_ID;
		string _WH_Name;
		string _Location;
		string _Phone;
		string _Batch;
		string _PetrolID;
		string _PetrolRate;
		string _Beat_No;
		string _Beat_Name;
		string _Balance_Type;
		string _Tin_No;
		string _TempCustName;
		string _ContactPerson;
		string _DateTo;
		string _DateFrom;


		public string DateTo
		{
			get
			{
				return _DateTo;
			}
			set
			{
				_DateTo =value;
			}

		}
		public string DateFrom
		{
			get
			{
				return _DateFrom;
			}
			set
			{
				_DateFrom =value;
			}

		}
		public string Balance_Type
		{
			get
			{
				return _Balance_Type;
			}
			set
			{
				_Balance_Type =value;
			}

		}
		public string TempCustName
		{
			get
			{
				return _TempCustName;
			}
			set
			{
				_TempCustName =value;
			}

		}

		public string  EntryDate
		{
			get
			{
				return _EntryDate;
			}
			set
			{
				_EntryDate=value;
			}
		}
		public string Beat_No
		{
			get
			{
				return _Beat_No ;
			}
			set
			{
				_Beat_No =value;
			}

		}
		public string Beat_Name
		{
			get
			{
				return _Beat_Name;
			}
			set
			{
				_Beat_Name=value;
			}

		}
		public string PetrolID
		{
			get
			{
				return _PetrolID;
			}
			set
			{
				_PetrolID=value;
			}

		}
		public string PetrolRate
		{
			get
			{
				return _PetrolRate;
			}
			set
			{
				_PetrolRate=value;
			}

		}
	
		public string Batch
		{
			get
			{
				return _Batch;
			}
			set
			{
				_Batch=value;
			}

		}
	
		public string WH_ID
		{
			get
			{
				return _WH_ID;
			}
			set
			{
				_WH_ID=value;
			}

		}
		
		public string WH_Name
		{
			get
			{
				return _WH_Name;
			}
			set
			{
				_WH_Name=value;
			}

		}
		
		public string Location
		{
			get
			{
				return _Location;
			}
			set
			{
				_Location=value;
			}

		}
		
		public string Phone
		{
			get
			{
				return _Phone;
			}
			set
			{
				_Phone=value;
			}

		}
	
		
		public string Nozzle_ID
		{
			get
			{
				return _Nozzle_ID;
			}
			set
			{
				_Nozzle_ID=value;
			}

		}
		
		
	
	
		public string Tank_ID
		{
			get
			{
				return _Tank_ID;
			}
			set
			{
				_Tank_ID=value;
			}

		}
		public string Reading
		{
			get
			{
				return _Reading;
			}
			set
			{
				_Reading=value;
			}
		}
	
		public string Item_ID
		{
			get
			{
				return _Item_ID;
			}
			set
			{
				_Item_ID=value;
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
		public string Qty
		{
			get
			{
				return _Qty;
			}
			set
			{
				_Qty=value;
			}
		}
		public string Meter_ID
		{
			get
			{
				return _Meter_ID;
			}
			set
			{
				_Meter_ID=value;
			}
		}
		public string Reading_Date
		{
			get
			{
				return _Reading_Date;
			}
			set
			{
				_Reading_Date=value;
			}
		}
	
		public string Nozel_ID
		{
			get
			{
				return _Nozel_ID;
			}
			set
			{
				_Nozel_ID=value;
			}
		}
		public string Start_Reading
		{
			get
			{
				return _Start_Reading;
			}
			set
			{
				_Start_Reading=value;
			}

		}
		public string End_reading
		{
			get
			{
				return _End_reading;
			}
			set
			{
				_End_reading=value;
			}
		}

		public string Machine_ID
		{
			get
			{
				return _Machine_ID;
			}
			set
			{
				_Machine_ID=value;
			}
		}
		public string Tank_Id
		{
			get
			{
				return _Tank_Id;
			}
			set
			{
				_Tank_Id=value;
			}
		}
		public string No_of_Nozzel
		{
			get
			{
				return _No_of_Nozzel;
			}
			set
			{
				_No_of_Nozzel=value;
			}
		}


		public string Dip_ID
		{
			get
			{
				return _Dip_ID;
			}
			set
			{
				_Dip_ID=value;
			}
		}
		public string Dip_Date
		{
			get
			{
				return _Dip_Date;
			}
			set
			{
				_Dip_Date=value;
			}
		}

		
		public string Tank_Name
		{
			get
			{
				return _Tank_Name;
			}
			set
			{
				_Tank_Name=value;
			}
		}
		public string Capacity
		{
			get
			{
				return _Capacity;
			}
			set
			{
				_Capacity=value;
			}
		}
		public string Temperature
		{
			get
			{
				return _Temperature;
			}
			set
			{
				_Temperature=value;
			}
		}
		public string Product
		{
			get
			{
				return _Product;
			}
			set
			{
				_Product=value;
			}
		}

     
		public string Status
		{
			get
			{
				return _Status;
			}
			set
			{
				_Status=value;
			}
		}
		public string Shift_ID
		{
			get
			{
				return _Shift_ID;
			}
			set
			{
				_Shift_ID=value;
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
	
		public string OT_Date
		{
			get
			{
				return _OT_Date;
			}
			set
			{
				_OT_Date=value;
			}
		}
		
		public string OT_From
		{
			get
			{
				return _OT_From;
			}
			set
			{
				_OT_From=value;
			}
		}
		
		public string OT_To
		{
			get
			{
				return _OT_To;
			}
			set
			{
				_OT_To=value;
			}
		}

		public string Emp_ID
		{
			get
			{
				return _Emp_ID;
			}
			set
			{
				_Emp_ID=value;
			}
		}
		public string First_Name
		{
			get
			{
				return _First_Name;
			}
			set
			{
				_First_Name=value;
			}
		}
		public string MiDrope_Name
		{
			get
			{
				return _MiDrope_Name;
			}
			set
			{
				_MiDrope_Name=value;
			}
		}
		public string Last_Name
		{
			get
			{
				return _Last_Name;
			}
			set
			{
				_Last_Name=value;
			}
		}
		public string Per_Address
		{
			get
			{
				return _Per_Address;
			}
			set
			{
				_Per_Address=value;
			}
		}
		public string Local_Address
		{
			get
			{
				return _Local_Address;
			}
			set
			{
				_Local_Address=value;
			}
		}
		

		public string PinCode
		{
			get
			{
				return _PinCode;
			}
			set
			{
				_PinCode=value;
			}
		}
		public string Phone_Number
		{
			get
			{
				return _Phone_Number;
			}
			set
			{
				_Phone_Number=value;
			}
		}
		public string EMail_ID
		{
			get
			{
				return _EMail_ID;
			}
			set
			{
				_EMail_ID=value;
			}
		}
		public string Date_OF_Birth
		{
			get
			{
				return _Date_OF_Birth;
			}
			set
			{
				_Date_OF_Birth=value;
			}
		}

		public string Age
		{
			get
			{
				return _Age;
			}
			set
			{
				_Age=value;
			}
		}
		public string Gender
		{
			get
			{
				return _Gender;
			}
			set
			{
				_Gender=value;
			}
		}
		public string Father_Name
		{
			get
			{
				return _Father_Name;
			}
			set
			{
				_Father_Name=value;
			}
		}
		public string Mother_Name
		{
			get
			{
				return _Mother_Name;
			}
			set
			{
				_Mother_Name=value;
			}
		}
		
		public string marital_Status
		{
			get
			{
				return _marital_Status;
			}
			set
			{
				_marital_Status=value;
			}
		}
		


		public string User_Type
		{
			get
			{
				return _User_Type;
			}
			set
			{
				_User_Type=value;
			}
		}
		public string User_Name
		{
			get
			{
				return _User_Name;
			}
			set
			{
				_User_Name=value;
			}
		}
		public string User_Password
		{
			get
			{
				return _User_Password;
			}
			set
			{
				_User_Password=value;
			}
		}

		public string usertype
		{
			get
			{
				return _u_type;
			}
			set
			{
				_u_type=value;
			}
		}
		public string username
		{
			get
			{
				return _u_name;
			}
			set
			{
				_u_name=value;
			}
		}
		public string userpass
		{
			get
			{
				return _u_pass;
			}
			set
			{
				_u_pass=value;
			}
		}
	
	   


		public string Supp_ID
		{
			get
			{
				return _Supp_ID;
			}
			set
			{
				_Supp_ID=value;
			}

		}

		public string Supp_Name
		{
			get
			{
				return _Supp_Name;
			}
			set
			{
				_Supp_Name=value;
			}

		}
		public string Supp_Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type=value;
			}

		}
		public string Address
		{
			get
			{
				return _Address;
			}
			set
			{
				_Address=value;
			}

		}
		public string City
		{
			get
			{
				return _City;
			}
			set
			{
				_City=value;
			}

		}
		public string State
		{
			get
			{
				return _State;
			}
			set
			{
				_State=value;
			}


		}
		public string Country
		{
			get
			{
				return _Country;
			}
			set
			{
				_Country=value;
			}

		}
		public string SSR
		{
			get
			{
				return _SSR;
			}
			set
			{
				_SSR=value;
			}

		}
		public string Tel_Res
		{
			get
			{
				return _Tel_Res;
			}
			set
			{
				_Tel_Res=value;
			}

		}
		public string Tel_Off
		{
			get
			{
				return _Tel_Off;
			}
			set
			{
				_Tel_Off=value;
			}

		}
		public string Mobile
		{
			get
			{
				return _Mobile;
			}
			set
			{
				_Mobile=value;
			}

		}
		public string EMail
		{
			get
			{
				return _EMail;
			}
			set
			{
				_EMail=value;
			}

		}
		public string Op_Balance
		{
			get
			{
				return _Op_Balance;
			}
			set
			{
				_Op_Balance=value;
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

		public string @sadbhavnacd
		{
			get
			{
				return _sadbhavnacd;
			}
			set
			{
				_sadbhavnacd=value;
			}

		}

		public string Cust_Name
		{
			get
			{
				return _Cust_Name;
			}
			set
			{
				_Cust_Name=value;
			}

		}
		public string Cust_Type
		{
			get
			{
				return _Cust_Type;
			}
			set
			{
				_Cust_Type=value;
			}

		}
		public string CR_Limit
		{
			get
			{
				return _CR_Limit;
			}
			set
			{
				_CR_Limit=value;
			}

		}
		public string CR_Days
		{
			get
			{
				return _CR_Days;
			}
			set
			{
				_CR_Days=value;
			}

		}
		public string Tin_No
		{
			get
			{
				return _Tin_No;
			}
			set
			{
				_Tin_No=value;
			}

		}
		public string ContactPerson
		{
			get
			{
				return _ContactPerson;
			}
			set
			{
				_ContactPerson=value;
			}

		}
		
		#endregion

		#region Constructor: Opens the connection to the server
		public PartiesClass()
		{
			
			sqcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			sqcon.Open();
			SqlCon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			SqlCon.Open();
		}


		#endregion	
				
		#region Customer Module
		/// <summary>
		/// Returns the SqlDataReader object containing the Max. Customer ID from Customer Table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextCustomerID()
		{
			SqlCmd=new SqlCommand ("select max(Cust_ID)+1 from Customer",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
			
		}
			
		/// <summary>
		/// Returns the SqlDataReader object containing the Max. Scheme ID from OilScheme Table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextschemeID()
		{
			SqlCmd=new SqlCommand ("select max(sch_ID)+1 from oilscheme",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
			
		}

		/// <summary>
		/// Returns the SqlDataReader object containing the Max. FOID ID from FOE Table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextFOID()
		{
			SqlCmd=new SqlCommand ("select max(foID)+1 from foe",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
			
		}

		/// <summary>
		/// Returns the SqlDataReader Object Containing the Max. Company ID from Organisation table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextCustomerID1()
		{
			SqlCmd=new SqlCommand ("select max(CompanyID)+1 from Organisation",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
					
		}

		/// <summary>
		/// Returns the SqlDataReader containing the Customer ID for all the Customers from Customer Table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetCustomerID()
		{
			SqlCmd=new SqlCommand ("select Cust_ID from Customer",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProCustomerEntry to insert the Customer Details
		/// and also insert CustomerLedgerTable,Ledger_Master with get max Ledger ID, AccountsLedgerTable
		/// and Customer_Balance table.
		/// </summary>
		public void InsertCustomer()
		{ 
			sqcom=new SqlCommand("ProCustomerEntry",sqcon);
			sqcom.CommandType =CommandType.StoredProcedure;
			sqcom.Parameters .Add("@Cust_ID",Cust_ID );
			sqcom.Parameters .Add("@sadbhavnacd",sadbhavnacd);
			sqcom.Parameters .Add("@EntryDate",EntryDate );
			sqcom.Parameters .Add("@Cust_Name",Cust_Name );
			sqcom.Parameters .Add("@Cust_Type",Cust_Type );
			sqcom.Parameters .Add("@Address",Address );
			sqcom.Parameters .Add("@City",City);
			sqcom.Parameters .Add("@State",State );
			sqcom.Parameters .Add("@Country",Country );
			sqcom.Parameters .Add("@Tel_Res",Tel_Res);
			sqcom.Parameters .Add("@Tel_Off",Tel_Off);
			sqcom.Parameters .Add("@Mobile",Mobile);
			sqcom.Parameters .Add("@EMail",EMail);
			sqcom.Parameters .Add("@CR_Limit",CR_Limit);
			sqcom.Parameters .Add("@CR_Days",CR_Days);
			sqcom.Parameters .Add("@Op_Balance",Op_Balance );
			sqcom.Parameters .Add("@Balance_Type",Balance_Type);
			sqcom.Parameters .Add("@Tin_No",Tin_No);
			sqcom.Parameters .Add("@SSR",SSR);
			sqcom.Parameters .Add("@ContactPerson",ContactPerson);
			sqcom.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProCustomerUpdate to Update the Customer Details.
		/// and also update the CustomerLedgerTable,Ledger_Master, AccountsLedgerTable and Customer_Balance table.
		/// </summary>
		public void UpdateCustomer()
		{ 
			sqcom=new SqlCommand("ProCustomerUpdate",sqcon);
			sqcom.CommandType =CommandType.StoredProcedure;
			sqcom.Parameters .Add("@Cust_ID",Cust_ID);
			sqcom.Parameters .Add("@Cust_Name",Cust_Name);
			sqcom.Parameters .Add("@TempCustName",TempCustName);
			sqcom.Parameters .Add("@Cust_Type",Cust_Type);
			sqcom.Parameters .Add("@Address",Address);
			sqcom.Parameters .Add("@City",City);
			sqcom.Parameters .Add("@State",State );
			sqcom.Parameters .Add("@Country",Country );
			sqcom.Parameters .Add("@Tel_Res",Tel_Res);
			sqcom.Parameters .Add("@Tel_Off",Tel_Off);
			sqcom.Parameters .Add("@Mobile",Mobile);
			sqcom.Parameters .Add("@EMail",EMail);
			sqcom.Parameters .Add("@CR_Limit",CR_Limit);
			sqcom.Parameters .Add("@CR_Days",CR_Days);
			sqcom.Parameters .Add("@Op_Balance",Op_Balance );
			sqcom.Parameters .Add("@Balance_Type",Balance_Type);
			sqcom.Parameters .Add("@Tin_No",Tin_No);
			sqcom.Parameters .Add("@SSR",SSR);
			sqcom.Parameters .Add("@ContactPerson",ContactPerson);
			sqcom.Parameters .Add("@sadbhavnacd",sadbhavnacd);
			sqcom.ExecuteNonQuery();          
		}
		/// <summary>
		/// Calls the Procedure ProCustomerUpdate to Update the all Customer Details at a time.
		/// and also update the CustomerLedgerTable,Ledger_Master, AccountsLedgerTable and Customer_Balance table.
		/// </summary>
		public void UpdateCustomerAll()
		{ 
			sqcom=new SqlCommand("ProCustomerUpdateAll",sqcon);
			sqcom.CommandType =CommandType.StoredProcedure;
			sqcom.Parameters .Add("@Cust_ID",Cust_ID);
			sqcom.Parameters .Add("@Cust_Name",Cust_Name);
			//sqcom.Parameters .Add("@TempCustName",TempCustName);
			//sqcom.Parameters .Add("@Cust_Type",Cust_Type);
			sqcom.Parameters .Add("@Address",Address);
			sqcom.Parameters .Add("@City",City);
			sqcom.Parameters .Add("@State",State );
			sqcom.Parameters .Add("@Country",Country );
			//sqcom.Parameters .Add("@Tel_Res",Tel_Res);
			sqcom.Parameters .Add("@Tel_Off",Tel_Off);
			sqcom.Parameters .Add("@Mobile",Mobile);
			//sqcom.Parameters .Add("@EMail",EMail);
			sqcom.Parameters .Add("@CR_Limit",CR_Limit);
			sqcom.Parameters .Add("@CR_Days",CR_Days);
			sqcom.Parameters .Add("@Op_Balance",Op_Balance );
			sqcom.Parameters .Add("@Balance_Type",Balance_Type);
			sqcom.Parameters .Add("@Tin_No",Tin_No);
			sqcom.Parameters .Add("@SSR",SSR);
			sqcom.Parameters .Add("@Cust_Type",Cust_Type);
			sqcom.Parameters .Add("@sadbhavnacd",sadbhavnacd);
			sqcom.Parameters .Add("@ContactPerson",ContactPerson);                  //Add by vikas sharma17.07.09
			sqcom.ExecuteNonQuery();          
		}
		/// <summary>
		/// Returns the SqlDataReader Containing  the Customer information for the passing parameter 
		/// i.e. Customer ID, Customer Name, Customers City.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="name"></param>
		/// <param name="place"></param>
		/// <returns></returns>
		public SqlDataReader CustomerList(string ID, string name, string place)
		{
			#region Query
			string sql;
			int wherestatus=0;
			sql="select * from Customer";

			if(ID!="")
			{
				sql=sql+" where Cust_ID=" + ID;
				wherestatus=1;
			}
			if(name!="")
			{
				if (wherestatus==0)
				{
					sql=sql+" where Cust_Name like('%" + name +"%')";
					wherestatus=1;
				}
				else
				{
					sql=sql+" and Cust_Name like('%" + name +"%')";
				}
			}
			if(place!="")
			{
				if (wherestatus==0)
				{
					sql=sql+" where City='" + place +"'";
					wherestatus=1;
				}
				else
				{
					sql=sql+" and City='" + place +"'";
				}
			}

			#endregion

			SqlCmd=new SqlCommand (sql,SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;			
		}

		/// <summary>
		/// Returns the DataSet Object containing the Customer information for the passing Customer ID , 
		/// Name and City as a Parameters
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="name"></param>
		/// <param name="place"></param>
		/// <returns></returns>
		public DataSet ShowCustomerInfo(string ID,string name, string place)
		{
			#region Query
			string sql;
			int wherestatus=0;
			//sql="select Cust_ID,Cust_Name,City from Customer";
			sql="select Cust_ID,Cust_Name,City,Ledger_ID from Customer c,Ledger_Master lm where c.Cust_Name=lm.Ledger_Name";
			if(ID!="")
			{
				//sql=sql+" where Cust_ID=" + ID;
				//sql=sql+" where Cust_ID like('" + ID +"%')";
				sql=sql+" and Cust_ID like('" + ID +"%')";
				wherestatus=1;
			}
			if(name!="" && place=="")
			{  
							
				if (wherestatus==0)
				{
					//sql=sql+" where  Cust_Name like('"+name+"%')";
					sql=sql+" and  Cust_Name like('"+name+"%')";
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where Cust_Name like('%" + name +"%')";
					sql=sql+" and Cust_Name like('%" + name +"%')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where Cust_Name like('%" + name +"')";
					sql=sql+" and Cust_Name like('%" + name +"')";
					wherestatus=1;
				}
			}
			if(place!="" && name=="" )
			{
				if (wherestatus==0)
				{
					//sql=sql+" where City like('" + place +"%')";
					sql=sql+" and City like('" + place +"%')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where City like('%" + place +"')";
					sql=sql+" and City like('%" + place +"')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					sql=sql+" where City like('%" + place +"%')";
					wherestatus=1;
				}
				
			}
			if(name!="" && place!="" )
			{
				if (wherestatus==0)
				{
					//sql=sql+" where City like('" + place +"%') and  Cust_Name like('" + name +"%')";
					sql=sql+" and City like('" + place +"%') and  Cust_Name like('" + name +"%')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where City like('%" + place +"') and  Cust_Name like('%" + name +"')";
					sql=sql+" and City like('%" + place +"') and  Cust_Name like('%" + name +"')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where City like('%" + place +"%') and  Cust_Name like('%" + name +"%')";
					sql=sql+" and City like('%" + place +"%') and  Cust_Name like('%" + name +"%')";
					wherestatus=1;
				}
				
			}
			#endregion
			#endregion
			SqlAdp= new SqlDataAdapter(sql,sqcon);
			ds=new DataSet();
			SqlAdp.Fill(ds);
			return ds;		
		}

		#region Supplier Module

		/// <summary>
		/// Returns the SqlDataReader Object containing the max. Supplier ID from table Supplier.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetNextSupplierID()
		{
			SqlCmd=new SqlCommand ("select max(Supp_ID)+1 from Supplier",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
				
		}

		/// <summary>
		/// Returns the SqlDataReader Object Containing the Supplier ID for all the Suppliers from supplier table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader GetSupplierID()
		{
			SqlCmd=new SqlCommand ("select Supp_ID from Supplier",SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProSupplierEntry to insert the data in Supplier Details,
		/// vendorLedgerTable, AccountsLedgerTable, Ledger_Master and set the sub_group_id from 
		/// Ledger_Master_sub_grp table and insert into Ledger_Master table.
		/// </summary>
		public void InsertSupplier()
		{ 
			sqcom=new SqlCommand("ProsupplierEntry",sqcon);
			sqcom.CommandType=CommandType.StoredProcedure;
			sqcom.Parameters .Add("@Supp_ID",Supp_ID );
			sqcom.Parameters .Add("@Supp_Name",Supp_Name );
			sqcom.Parameters .Add("@Supp_Type",Supp_Type );
			sqcom.Parameters .Add("@Address",Address );
			sqcom.Parameters .Add("@City",City);
			sqcom.Parameters .Add("@State",State );
			sqcom.Parameters .Add("@Country",Country );
			sqcom.Parameters .Add("@Tel_Res",Tel_Res);
			sqcom.Parameters .Add("@Tel_Off",Tel_Off);
			sqcom.Parameters .Add("@Mobile",Mobile);
			sqcom.Parameters .Add("@EMail",EMail);
			sqcom.Parameters .Add("@Op_Balance",Op_Balance );
			sqcom.Parameters .Add("@Balance_Type",Balance_Type);
			sqcom.Parameters .Add("@Cr_Days",CR_Days);
			sqcom.Parameters .Add("@Tin_No",Tin_No);
			sqcom.ExecuteNonQuery();
          
		}

		/// <summary>
		/// Calls the Procedure ProSupplierUpdate to Update the data in Supplier Details,
		/// vendorLedgerTable, AccountsLedgerTable, Ledger_Master and set the sub_group_id from 
		/// Ledger_Master_sub_grp table and insert into Ledger_Master table.
		/// </summary>
		public void UpdateSupplier()
		{ 
			sqcom=new SqlCommand("ProsupplierUpdate",sqcon);
			sqcom.CommandType=CommandType.StoredProcedure;
			sqcom.Parameters .Add("@Supp_ID",Supp_ID );
			sqcom.Parameters .Add("@Supp_Name",Supp_Name );
			sqcom.Parameters .Add("@TempSuppName",TempCustName);
			sqcom.Parameters .Add("@Supp_Type",Supp_Type  );
			sqcom.Parameters .Add("@Address",Address );
			sqcom.Parameters .Add("@City",City);
			sqcom.Parameters .Add("@State",State );
			sqcom.Parameters .Add("@Country",Country );
			sqcom.Parameters .Add("@Tel_Res",Tel_Res);
			sqcom.Parameters .Add("@Tel_Off",Tel_Off);
			sqcom.Parameters .Add("@Mobile",Mobile);
			sqcom.Parameters .Add("@EMail",EMail);
			sqcom.Parameters .Add("@Op_Balance",Op_Balance );
			sqcom.Parameters .Add("@Balance_Type",Balance_Type);
			sqcom.Parameters .Add("@Cr_Days",CR_Days);
			sqcom.Parameters .Add("@Tin_No",Tin_No);
			sqcom.ExecuteNonQuery();          
		}

		/// <summary>
		/// Returns the SqlDataREader Containing the the information about the supplier for passing  
		/// Parameter Supplier ID, Name and City.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="name"></param>
		/// <param name="place"></param>
		/// <returns></returns>
		public SqlDataReader SupplierList(string ID, string name, string place)
		{
			#region Query
			string sql;
			int wherestatus=0;
			sql="select * from Supplier";

			if(ID!="")
			{
				sql=sql+" where Supp_ID=" + ID;
				wherestatus=1;
			}
			if(name!="")
			{
				if (wherestatus==0)
				{
					sql=sql+" where Supp_Name like('%" + name +"%')";
					wherestatus=1;
				}
				else
				{
					sql=sql+" and Supp_Name like('%" + name +"%')";
				}
			}
			if(place!="")
			{
				if (wherestatus==0)
				{
					sql=sql+" where City='" + place +"'";
					wherestatus=1;
				}
				else
				{
					sql=sql+" and City='" + place +"'";
				}
			}

			#endregion

			SqlCmd=new SqlCommand (sql,SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;			
		}


		/// <summary>
		/// Returns the  DataSet Object Containing the Supplier details for passing parameters as Supplier ID,
		/// Name and city.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="name"></param>
		/// <param name="place"></param>
		/// <returns></returns>
		public DataSet ShowSupplierInfo(string ID,string name, string place)
		{
			string sql;
			int wherestatus=0;
			//sql="select Supp_ID,Supp_Name,City from Supplier";
			sql="select Supp_ID,Supp_Name,City,Ledger_ID from Supplier s,Ledger_Master lm where lm.Ledger_Name=s.Supp_Name";

			if(ID!="")
			{
				//sql=sql+" where Supp_ID=" + ID;
				//sql=sql+" where Supp_ID like('" + ID +"%')";
				sql=sql+" and Supp_ID like('" + ID +"%')";
				wherestatus=1;
			}
			if(name!="" && place=="")
			{  
				if (wherestatus==0)
				{
					//sql=sql+" where  Supp_Name like('"+ name  +"%')";
					sql=sql+" and  Supp_Name like('"+ name  +"%')";
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where Supp_Name like('%" + name +"%')";
					sql=sql+" and Supp_Name like('%" + name +"%')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where Supp_Name like('%" + name +"')";
					sql=sql+" and Supp_Name like('%" + name +"')";
					wherestatus=1;
				}
			}
			if(place!="" && name=="" )
			{
				if (wherestatus==0)
				{
					//sql=sql+" where City like('" + place +"%')";
					sql=sql+" and City like('" + place +"%')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where City like('%" + place +"')";
					sql=sql+" and City like('%" + place +"')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where City like('%" + place +"%')";
					sql=sql+" and City like('%" + place +"%')";
					wherestatus=1;
				}
			}
			if(name!="" && place!="" )
			{
				if (wherestatus==0)
				{
					//sql=sql+" where City like('" + place +"%') and  Supp_Name like('" + name +"%')";
					sql=sql+" and City like('" + place +"%') and  Supp_Name like('" + name +"%')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where City like('%" + place +"') and  Supp_Name like('%" + name +"')";
					sql=sql+" and City like('%" + place +"') and  Supp_Name like('%" + name +"')";
					wherestatus=1;
				}
				else if(wherestatus==0)
				{
					//sql=sql+" where City like('%" + place +"%') and  Supp_Name like('%" + name +"%')";
					sql=sql+" and City like('%" + place +"%') and  Supp_Name like('%" + name +"%')";
					wherestatus=1;
				}
			}
			#endregion

			SqlAdp= new SqlDataAdapter(sql,sqcon);
			ds=new DataSet();
			SqlAdp.Fill(ds);
			return ds;		
		}

		/// <summary>
		/// Returns the value 1 if login successful as return value 0 ;
		/// </summary>
		/// <returns></returns>
		public int CheckUserlogin()
		{
			sqcomm=new SqlCommand("select User_Type,User_Name,User_Password from UserLogin where User_Type='"+usertype+"' and User_Name='"+username+"' and User_Password='"+userpass+"'",sqcon);
			sqdr=sqcomm.ExecuteReader();
			if(sqdr.Read())
			{
				usertype=sqdr.GetValue(0).ToString().Trim();
				username=sqdr.GetValue(1).ToString().Trim();
				userpass=sqdr.GetValue(2).ToString().Trim();
				return 0;
			}
			else
			{
				return 1;
			}		
		}
		
		/// <summary>
		/// Calls the Procedure ProInsertTankStock to insert the Tank Stock Details
		/// </summary>
		public void InsertTankStock()
		{ 
			sqcom=new SqlCommand("ProInsertTankStock",sqcon);
			sqcom.CommandType=CommandType.StoredProcedure;
			sqcom.Parameters .Add("@Item_ID",Item_ID );
			sqcom.Parameters .Add("@Tank_ID",Tank_ID );
			sqcom.Parameters .Add("@Prod_ID",Prod_ID );
			sqcom.Parameters .Add("@Qty",Qty );
			sqcom.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProInsertNozzle to insert the Nozzle Details.
		/// </summary>
		public void InsertNozzelInfo()
		{ 
			sqcom=new SqlCommand("ProInsertNozzel",sqcon);
			sqcom.CommandType=CommandType.StoredProcedure;
			sqcom.Parameters .Add("@Nozzle_ID",Nozel_ID );
			sqcom.Parameters .Add("@Machine_ID",Machine_ID );
			sqcom.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProInsertTank to insert the Tank Details
		/// </summary>
		public void InsertTank()
		{ 
			sqcom=new SqlCommand("ProInsertTank",sqcon);
			sqcom.CommandType=CommandType.StoredProcedure;
			sqcom.Parameters .Add("@Tank_ID",Tank_ID );
			sqcom.Parameters .Add("@Tank_Name",Tank_Name );
			sqcom.Parameters .Add("@Capacity",Capacity );
			sqcom.Parameters .Add("@Temperature",Temperature );
			sqcom.Parameters .Add("@Product",Product );
			sqcom.ExecuteNonQuery();
		}


		/// <summary>
		/// Calls the Procedure ProInsertWareHouse to insert the Ware House Details
		/// </summary>
		public void InsertWareHouse()
		{ 
			sqcom=new SqlCommand("ProInsertWareHouse",sqcon);
			sqcom.CommandType=CommandType.StoredProcedure;
			sqcom.Parameters .Add("@WH_ID",WH_ID );
			sqcom.Parameters .Add("@WH_Name",WH_Name );
			sqcom.Parameters .Add("@Location",Location );
			sqcom.Parameters .Add("@Phone",Phone );
			sqcom.ExecuteNonQuery();
		}
		
		/// <summary>
		/// Calls the Procedure ProInsertMeterReading to insert the Meter Reading Details
		/// </summary>
		public void InsertMeterReading()
		{ 
			sqcom=new SqlCommand("ProInsertMeterReading",sqcon);
			sqcom.CommandType=CommandType.StoredProcedure;
			sqcom.Parameters.Add("@Meter_ID",Meter_ID );
			sqcom.Parameters.Add("@Reading_Date",Reading_Date );
			sqcom.Parameters.Add("@Shift_ID",Shift_ID );
			sqcom.Parameters.Add("@Nozel_ID",Nozel_ID);
			sqcom.Parameters.Add("@Start_Reading",Start_Reading );
			sqcom.Parameters.Add("@End_reading",End_reading );
			sqcom.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProInsertNewUser to insert the User Information.
		/// </summary>
		public void InsertNewUser()
		{ 
			sqcomm=new SqlCommand("ProInsertNewUser",sqcon);
			sqcomm.CommandType =CommandType  .StoredProcedure;
			sqcomm.Parameters.Add("@First_Name",First_Name );
			sqcomm.Parameters.Add("@MiDrope_Name",MiDrope_Name );
			sqcomm.Parameters.Add("@Last_Name",Last_Name );
			sqcomm.Parameters.Add("@Per_Address",Per_Address );
			sqcomm.Parameters.Add("@Local_Address",Local_Address);
			sqcomm.Parameters.Add("@City",City );
			sqcomm.Parameters.Add("@PinCode",PinCode );
			sqcomm.Parameters.Add("@Phone_Number",Phone_Number);
			sqcomm.Parameters.Add("@EMail_ID",EMail_ID);
			sqcomm.Parameters.Add("@Date_OF_Birth",Date_OF_Birth);
			sqcomm.Parameters.Add("@Age",Age);
			sqcomm.Parameters.Add("@Gender",Gender );
			sqcomm.Parameters.Add("@Father_Name",Father_Name);
			sqcomm.Parameters.Add("@Mother_Name",Mother_Name);
			sqcomm.Parameters.Add("@marital_Status",marital_Status);
			sqcomm.ExecuteNonQuery();
		}
		
		/// <summary>
		/// Calls the Procedure ProInsertUser to insert the User Details
		/// </summary>
		public void InsertUser()
		{ 
			sqcomm=new SqlCommand("proInsertUser",sqcon);
			sqcomm.CommandType =CommandType.StoredProcedure;
			sqcomm.Parameters .Add("@User_Type",User_Type );
			sqcomm.Parameters .Add("@User_Name",User_Name );
			sqcomm.Parameters.Add("@User_Password",User_Password );
		}

		/// <summary>
		/// Calls the Procedure ProInsertMachine to insert the Machine Information.
		/// </summary>
		public void InsertMachine()
		{
			sqcomm=new SqlCommand("ProInsertMachine",sqcon);
			sqcomm.CommandType =CommandType  .StoredProcedure;
			sqcomm.Parameters .Add("@Machine_ID",Machine_ID );
			sqcomm.Parameters .Add("@Tank_Id",Tank_ID );
			sqcomm.Parameters .Add("@No_of_Nozzel",No_of_Nozzel );
			sqcomm.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProInsertDipReading to insert the Tank Dip Reading
		/// </summary>
		public void InsertDipReading()
		{
			sqcomm=new SqlCommand("ProInsertDipReading",sqcon);
			sqcomm.CommandType =CommandType  .StoredProcedure;
			sqcomm.Parameters .Add("@Dip_ID",Dip_ID );
			sqcomm.Parameters .Add("@Dip_Date",Dip_Date );
			sqcomm.Parameters .Add("@Tank_ID",Tank_ID );
			sqcomm.Parameters .Add("@Reading",Reading );
			sqcomm.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProInsertWareStock to insert the Ware Stock Details
		/// </summary>
		public void InsertWHStock()
		{
			sqcomm=new SqlCommand("ProInsertWareStock",sqcon);
			sqcomm.CommandType =CommandType  .StoredProcedure;
			sqcomm.Parameters .Add("@Item_ID ",Item_ID  );
			sqcomm.Parameters .Add("@Pro_ID",Prod_ID );
			sqcomm.Parameters .Add("@Batch",Batch );
			sqcomm.Parameters .Add("@Qty",Qty );
			sqcomm.Parameters .Add("@WH_ID",WH_ID );
			sqcomm.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProBeatMasterEntry to insert the Beat Details
		/// </summary>
		public void InsertBeatMaster()
		{
			sqcomm=new SqlCommand("ProBeatMasterEntry",sqcon);
			sqcomm.CommandType =CommandType.StoredProcedure;
			sqcomm.Parameters .Add("@Beat_No",Beat_No);
			sqcomm.Parameters .Add("@City",City);
			sqcomm.Parameters .Add("@State",State);
			sqcomm.Parameters .Add("@Country",Country);
			sqcomm.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProcancellationPayment to return string
		/// First we fatch the max Payment no in given period from Payment_Transaction table and
		/// set the fixed min Payment no manualy after that find the cancel payment no by while loop
		/// and mising no store in script variable.
		/// </summary>
		/// <returns></returns>
		public string FatchCancelPaymentInvoice()
		{
			sqcomm=new SqlCommand("ProCancellationPayment",sqcon);
			sqcomm.CommandType =CommandType.StoredProcedure;
			sqcomm.Parameters .Add("@fromdate",DateFrom);
			sqcomm.Parameters .Add("@todate",DateTo);
			sqcomm.Parameters.Add(new SqlParameter("@retData",SqlDbType.VarChar,8000));
			sqcomm.Parameters["@retData"].Direction = ParameterDirection.Output;
			sqcomm.ExecuteNonQuery();
			string str= sqcomm.Parameters["@retData"].Value.ToString();
			return str;
		}

		/// <summary>
		/// Calls the Procedure ProcancellationReceiptNo to return string
		/// First we fatch the max and min receipt no in given period from Payment_Receipt table and
		/// after that find the cancel Receipt no by while loop and mising no store in script variable..
		/// </summary>
		/// <returns></returns>
		public string FatchCancelReceiptNo()
		{
			sqcomm=new SqlCommand("ProCancellationReceiptNo",sqcon);
			sqcomm.CommandType =CommandType.StoredProcedure;
			sqcomm.Parameters .Add("@fromdate",DateFrom);
			sqcomm.Parameters .Add("@todate",DateTo);
			sqcomm.Parameters.Add(new SqlParameter("@retData",SqlDbType.VarChar,8000));
			sqcomm.Parameters["@retData"].Direction = ParameterDirection.Output;
			sqcomm.ExecuteNonQuery();
			string str= sqcomm.Parameters["@retData"].Value.ToString();
			return str;
		}

		/// <summary>
		/// Calls the Procedure ProcancellationPurchaseInvoice to return string
		/// First we fatch the max and min invoice no in given period from purchase_master table and
		/// after that find the cancel purchase invoice no by while loop and mising no store in script variable.
		/// </summary>
		/// <returns></returns>
		public string FatchCancelPurchaseInvoice()
		{
			sqcomm=new SqlCommand("ProCancellationPurchaseInvoice",sqcon);
			sqcomm.CommandType =CommandType.StoredProcedure;
			sqcomm.Parameters .Add("@fromdate",DateFrom);
			sqcomm.Parameters .Add("@todate",DateTo);
			sqcomm.Parameters.Add(new SqlParameter("@retData",SqlDbType.VarChar,8000));
			sqcomm.Parameters["@retData"].Direction = ParameterDirection.Output;
			sqcomm.ExecuteNonQuery();
			string str= sqcomm.Parameters["@retData"].Value.ToString();
			return str;
		}

		/// <summary>
		/// Calls the Procedure ProcancellationSalesInvoice to return string
		/// First we fatch the max invoice no in given period from sales_master table and
		/// get the invoice no from organization table after that find the cancel sales invoice no by while loop
		/// and mising no store in script variable.
		/// </summary>
		/// <returns></returns>
		public string FatchCancelSalesInvoice()
		{
			sqcomm=new SqlCommand("ProCancellationSalesInvoice",sqcon);
			sqcomm.CommandType =CommandType.StoredProcedure;
			sqcomm.Parameters .Add("@fromdate",DateFrom);
			sqcomm.Parameters .Add("@todate",DateTo);
			sqcomm.Parameters.Add(new SqlParameter("@retData",SqlDbType.VarChar,8000));
			sqcomm.Parameters["@retData"].Direction = ParameterDirection.Output;
			sqcomm.ExecuteNonQuery();
			string str= sqcomm.Parameters["@retData"].Value.ToString();
			return str;
		}

		/// <summary>
		/// Calls the Procedure ProBeatMasterUpdate to Update the Beat Details
		/// </summary>
		public void UpdateBeatMaster()
		{
			sqcomm=new SqlCommand("ProBeatMasterUpdate",sqcon);
			sqcomm.CommandType =CommandType.StoredProcedure;
			sqcomm.Parameters .Add("@Beat_No",Beat_No);
			sqcomm.Parameters .Add("@City",City);
			sqcomm.Parameters .Add("@State",State);
			sqcomm.Parameters .Add("@Country",Country);
			sqcomm.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProBeatMasterDelete to Delete the Beat Details
		/// </summary>
		public void DeleteBeatMaster()
		{
			sqcomm=new SqlCommand("ProBeatMasterDelete",sqcon);
			sqcomm.CommandType =CommandType.StoredProcedure;
			sqcomm.Parameters .Add("@Beat_No",Beat_No);
			sqcomm.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProInsertRateChart to insert the Rate chart Details
		/// </summary>
		public void InsertRateChart()
		{
			sqcomm=new SqlCommand("ProInsertRateChart",sqcon);
			sqcomm.CommandType =CommandType  .StoredProcedure;
			sqcomm.Parameters .Add("@PetrolID ",PetrolID  );
			sqcomm.Parameters .Add("@PetrolRate",PetrolRate );
			sqcomm.ExecuteNonQuery();
		}
        
		/// <summary>
		/// Returns the SqlDataReader containing the max Nozzle ID from table Nozzle
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  GetNozzelID()
		{
			sqcom=new SqlCommand ("select max(Nozzle_ID)+1 from Nozzle",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Returns the SqlDataReader containing the Meter ID of the Meter Reading
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  ShowMeterID()
		{
			sqcom=new SqlCommand ("select Meter_ID from Meter_Reading",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Returns the SqlDataReader containing the Max Dip ID from Dip_Reading Table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  GetDipReading()
		{
			sqcom=new SqlCommand ("select max(Dip_ID)+1 from Dip_Reading",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Returns the SqlDataReader containing the max. Petrol ID from Rate Chart table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  GetRateChart()
		{
			sqcom=new SqlCommand ("select max(PetrolID)+1 from RateChart",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Returns the SqlDataReader Containing the PetrolID from the RateChart table.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  ShowPetrolIDIncombo()
		{
			sqcom=new SqlCommand ("select PetrolID from RateChart",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Returns the SqlDataReader containing Dip_ID from Dip_Reading
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  ShowDipID()
		{
			sqcom=new SqlCommand ("select Dip_ID from Dip_Reading",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Calls the Procedure ProdeleteDipReading and Returns the SqlDataReader
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  DeleteDipid()
		{
			SqlCmd =new SqlCommand ("prodeleteDipReading",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@Dip_ID",Dip_ID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure Prodeletemachine and returns the SqlDataReader 
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  DeleteMachine()
		{
			SqlCmd =new SqlCommand ("prodeletemachine",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@Machine_ID",Machine_ID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProSelectedPetrolID and returns the SqlDataReader
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  proPetrol()
		{
			SqlCmd =new SqlCommand ("proSelectedPetrolID",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@PetrolID",PetrolID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProDeleteMeter and Returns the SqlDataReader object
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  DeleteMeter()
		{
			SqlCmd =new SqlCommand ("proDeleteMeter",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@Meter_ID",Meter_ID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProDeleteNozzel returns the SqlDataReader object.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  DeleteNozzel()
		{
			SqlCmd =new SqlCommand ("ProDeleteNozzel",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@Nozzle_ID",Nozzle_ID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProDeleteTank and Returns the SqlDataReader
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  DeleteTank()
		{
			SqlCmd =new SqlCommand ("proDeleteTank",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@Tank_ID",Tank_ID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProdeleteTankStock and Returns the SqlDataReader object
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  DeleteTankStock()
		{
			SqlCmd =new SqlCommand ("prodeleteTankstock",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@Item_ID",Item_ID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProDeleteWareHouse and Returns the SqlDataReader object
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  DeleteWareHouse()
		{
			SqlCmd =new SqlCommand ("proDeleteWareHouse",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@WH_ID",WH_ID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// Calls the Procedure ProDeleteWhStock and Returns the SqlDataReader
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  DeleteWareHouseStock()
		{
			SqlCmd =new SqlCommand ("proDeleteWhStock",SqlCon);
			SqlCmd.CommandType=CommandType .StoredProcedure;
			SqlCmd.Parameters.Add("@Item_ID",Item_ID);
			SqlDtRed=SqlCmd.ExecuteReader();
			return SqlDtRed;
		}

		/// <summary>
		/// This method is not used
		/// </summary>
		public void UpdateDip()
		{ 
			SqlCmd=new SqlCommand("ProDipUpdate",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Dip_ID",Dip_ID);
			SqlCmd.Parameters .Add("@Dip_Date",Dip_Date);
			SqlCmd.Parameters .Add("@Tank_ID",Tank_ID);
			SqlCmd.Parameters .Add("@Reading",Reading);
			SqlCmd.ExecuteNonQuery();       
		}

		/// <summary>
		/// This method is not used
		/// </summary>
		public void UpdateRateChart()
		{ 
			SqlCmd=new SqlCommand("ProPetrolUpdate",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@PetrolID",PetrolID);
			SqlCmd.Parameters .Add("@PetrolRate ",PetrolRate );
			SqlCmd.ExecuteNonQuery();       
		}

		/// <summary>
		/// This method is not used
		/// </summary>
		public void UpdateMeter()
		{ 
			SqlCmd=new SqlCommand("ProMeterUpdate",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@Meter_ID",Meter_ID); 
			SqlCmd.Parameters.Add("@Reading_Date",Reading_Date);
			SqlCmd.Parameters.Add("@Shift_ID",Shift_ID);
			SqlCmd.Parameters.Add("@Nozel_ID",Nozel_ID);
			SqlCmd.Parameters.Add("@Start_Reading",Start_Reading);
			SqlCmd.Parameters.Add("@End_reading",End_reading);
			SqlCmd.ExecuteNonQuery();       
		}
		
		/// <summary>
		/// This method is not used
		/// </summary>
		public void UpdateMachine()
		{ 
			SqlCmd=new SqlCommand("ProMachineUpdate",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Machine_ID",Machine_ID);
			SqlCmd.Parameters .Add("@Tank_Id",Tank_Id);
			SqlCmd.Parameters .Add("@No_of_Nozzel",No_of_Nozzel);
			SqlCmd.ExecuteNonQuery();       
		}

		/// <summary>
		/// This method is not used
		/// </summary>
		public void UpdateNozzel()
		{ 
			SqlCmd=new SqlCommand("ProNozzelUpdate",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Nozzle_ID",Nozzle_ID);
			SqlCmd.Parameters .Add("@Machine_ID",Machine_ID);
			SqlCmd.ExecuteNonQuery();       
		}
	
		/// <summary>
		/// This method is not used
		/// </summary>
		public void UpdateTank()
		{ 
			SqlCmd=new SqlCommand("ProTankUpdate",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Tank_ID",Tank_ID);
			SqlCmd.Parameters .Add("@Tank_Name",Tank_Name);
			SqlCmd.Parameters .Add("@Capacity",Capacity);
			SqlCmd.Parameters .Add("@Temperature",Temperature);
			SqlCmd.Parameters .Add("@Product",Product);
			SqlCmd.ExecuteNonQuery();       
		}
		
		/// <summary>
		/// This method is not used
		/// </summary>
		public void UpdateTankStock()
		{ 
			SqlCmd=new SqlCommand("ProTankStockUpdate",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Item_ID",Item_ID);
			SqlCmd.Parameters .Add("@Tank_ID",Tank_ID);
			SqlCmd.Parameters .Add("@Prod_ID",Prod_ID);
			SqlCmd.Parameters .Add("@Qty",Qty);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// This Method is not used.
		/// </summary>
		public void UpdateWHStock()
		{ 
			SqlCmd=new SqlCommand("ProWhStockUpdate",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters .Add("@Item_ID",Item_ID);
			SqlCmd.Parameters .Add("@Pro_ID",Prod_ID);
			SqlCmd.Parameters .Add("@Batch",Batch);
			SqlCmd.Parameters .Add("@Qty",Qty);
			SqlCmd.Parameters .Add("@WH_ID",WH_ID);
			SqlCmd.ExecuteNonQuery();       
		}
 
		/// <summary>
		/// This method is not used.
		/// </summary>
		public void UpdateWareHouse()
		{  
			SqlCmd=new SqlCommand("ProWareHouseUpdate",SqlCon);
			SqlCmd.CommandType =CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@WH_ID",WH_ID);
			SqlCmd.Parameters.Add("@WH_Name",WH_Name);
			SqlCmd.Parameters.Add("@Location",Location);
			SqlCmd.Parameters.Add("@Phone",Phone);
			SqlCmd.ExecuteNonQuery();       
		}

		/// <summary>
		/// Returns the SqlDataReader containing the max. Meter ID from Meter_Reading
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  ShowMeterId()
		{
			sqcom=new SqlCommand ("select max(Meter_ID)+1 from Meter_Reading",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}


		/// <summary>
		/// Returns the SqlDataReader containing the max. Item ID from Tank_Stock
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  ShowStockTank()
		{
			sqcom=new SqlCommand ("select max(Item_ID)+1 from Tank_Stock",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Returns the SqlDataReader Containing the Machine IDs from machine table.
		/// this method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  ShowmachineID()
		{
			sqcom=new SqlCommand ("select Machine_ID from Machine",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Returns the SqlDataReader containing the Nozzle ID for all the Nozzle from table Nozzle.
		/// this method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  ShowNozzelID()
		{
			sqcom=new SqlCommand ("select Nozzle_ID from Nozzle",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
			
		}

		/// <summary>
		/// Returns the SqlDataReader Containing the Tank ID for all the tanks from table Tank.
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  ShowTankIDInCombo()
		{
			sqcom=new SqlCommand ("select Tank_ID from Tank",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

 
		/// <summary>
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  ShowWareStock()
		{
			sqcom=new SqlCommand ("select Max(Item_ID)+1 from WH_Stock",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  ShowWareStockincombo()
		{
			sqcom=new SqlCommand ("select Item_ID from WH_Stock",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}


		/// <summary>
		/// Returns the SqlDataReader containing the max. Tank ID  from Tank.
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowTankID()
		{
			sqcom=new SqlCommand ("select max(Tank_ID)+1 from Tank",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowWareHouseID()
		{
			sqcom=new SqlCommand ("select max(WH_ID)+1 from Ware_House",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowWareHouseIDIncombo()
		{
			sqcom=new SqlCommand ("select WH_ID from Ware_House",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Returns the SqlDataReader containing the Product ID for all the products from table Products.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowProductIDincombo()
		{
			sqcom=new SqlCommand ("select Prod_ID from Product",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowWHIDincombo()
		{
			sqcom=new SqlCommand ("select WH_ID from Ware_House",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Returns the SqlDataReader Object containing the all item ID from WH_stock table.
		/// this method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowWHIDincombo3()
		{
			sqcom=new SqlCommand ("select Item_ID from WH_Stock",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Retruns the SqlDataReader  containing the Tank ID for all the Tanks in Table Tank.
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowTankIDInCombo1()
		{
			sqcom=new SqlCommand ("select Tank_ID from Tank",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Returns the SqlDataReader Object containing the Item ID from Tank_Stock.
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowTankStock()
		{
			sqcom=new SqlCommand ("select Item_ID from Tank_Stock",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		
		}
		
		/// <summary>
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowPackageId()
		{
			sqcom=new SqlCommand ("select PackageID from Package",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader ShowCategoryId()
		{
			sqcom=new SqlCommand ("select CategoryID from Category",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Returns the SqlDataReader containing the max. Machine ID from table machine.
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public SqlDataReader  GetMachineID()
		{
			sqcom=new SqlCommand("select max(Machine_ID)+1 from Machine",sqcon);
			sqred=sqcom.ExecuteReader();
			return sqred;
		}

		/// <summary>
		/// Calls the Procedure showDipReading and Returns the DataSet Object
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public DataSet ShowDipInfo()
		{
			SqlDataAdapter sqdr;
			sqdr=new SqlDataAdapter("showDipReading",SqlCon);
			ds=new DataSet();
			sqdr.Fill(ds);
			SqlCon.Close();
			SqlCon.Dispose();
			return ds;
		}

		/// <summary>
		/// Calls the Procedure ShowMachine and Returns the DataSet Object.
		/// this method is not used.
		/// </summary>
		/// <returns></returns>
		public DataSet ShowMachineInfo()
		{
			SqlDataAdapter sqdr;
			sqdr=new SqlDataAdapter("ShowMachine",SqlCon);
			ds=new DataSet();
			sqdr.Fill(ds);
			SqlCon.Close();
			SqlCon.Dispose();
			return ds;
		}

		/// <summary>
		/// Calls the Procedure ShowMater and returns the DataSet Object
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public DataSet ShowMeterInfo()
		{
			SqlDataAdapter sqdr;
			sqdr=new SqlDataAdapter("ShowMeter",SqlCon);
			ds=new DataSet();
			sqdr.Fill(ds);
			SqlCon.Close();
			SqlCon.Dispose();
			return ds;
		}

		/// <summary>
		/// Calls the Procedure ShowNozzle and Returns the DataSet Object
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public DataSet ShowNozzelInfo()
		{
			SqlDataAdapter sqdr;
			sqdr=new SqlDataAdapter("ShowNozzel",SqlCon);
			ds=new DataSet();
			sqdr.Fill(ds);
			SqlCon.Close();
			SqlCon.Dispose();
			return ds;
		}

		/// <summary>
		/// Calls the Procedure ShowTank and Returns the DataSet Object.
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public DataSet ShowTankInfo()
		{
			SqlDataAdapter sqdr;
			sqdr=new SqlDataAdapter("ShowTank",SqlCon);
			ds=new DataSet();
			sqdr.Fill(ds);
			SqlCon.Close();
			SqlCon.Dispose();
			return ds;
		}

		/// <summary>
		/// Calls the Procedure ShowTankStock and returns the DataSet Object
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public DataSet ShowTankStockInfo()
		{
			SqlDataAdapter sqdr;
			sqdr=new SqlDataAdapter("ShowTankStock",SqlCon);
			ds=new DataSet();
			sqdr.Fill(ds);
			SqlCon.Close();
			SqlCon.Dispose();
			return ds;
		}

		/// <summary>
		/// Calls the Procedure ShowWareHouse and Returns the DataSet Object
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public DataSet ShowWareHouseInfo1()
		{
			SqlDataAdapter sqdr;
			sqdr=new SqlDataAdapter("ShowWareHouse",SqlCon);
			ds=new DataSet();
			sqdr.Fill(ds);
			SqlCon.Close();
			SqlCon.Dispose();
			return ds;
		}

		/// <summary>
		/// Calls the Procedure ShowWareHouseData and Returns the DataSet Object.
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public DataSet ShowWareHouseInformation()
		{
			SqlDataAdapter sqdr;
			sqdr=new SqlDataAdapter("ShowWareHousedata",SqlCon);
			ds=new DataSet();
			sqdr.Fill(ds);
			SqlCon.Close();
			SqlCon.Dispose();
			return ds;
		}

		/// <summary>
		/// Calls the Procedure ShowWareHouseStockdata and Returns the DataSet Object
		/// This method is not used.
		/// </summary>
		/// <returns></returns>
		public DataSet ShowWareHouseStockInformation()
		{
			SqlDataAdapter sqdr;
			sqdr=new SqlDataAdapter("ShowWareHouseStockdata",SqlCon);
			ds=new DataSet();
			sqdr.Fill(ds);
			SqlCon.Close();
			SqlCon.Dispose();
			return ds;
		}

		/// <summary>
		/// Returns the SqlDataReader Containing the output of the Query passed as a Parameter string.
		/// </summary>
		/// <param name="Sql"></param>
		/// <returns></returns>
		public SqlDataReader GetRecordSet(string Sql)
		{
			SqlCmd=new SqlCommand (Sql,SqlCon );
			SqlDtRed  = SqlCmd.ExecuteReader();
			return SqlDtRed;			
		}
	}
}