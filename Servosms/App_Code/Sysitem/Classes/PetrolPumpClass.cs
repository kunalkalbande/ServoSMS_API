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
	/// Summary description for PetrolPumpClass.
	/// </summary>
	public class PetrolPumpClass
	{
		SqlConnection SqlCon;
		SqlCommand SqlCmd;
		SqlDataReader SqlDtr;

		#region Vars & Prop
		string _TankID;
		string _TankName;
		string _ProdName;
		string _ProdAbbName;
		string _Capacity;
		string _Temprature;
		string _WaterStock;
		string _ReserveStock;
		string _OpeningStock;
		string _MachineID;
		string _MachineName;
		string _MachineType;
		string _NozzelID;
		string _NozzelName;
		string _EntryDate;
		string _Reading;
		string _ShiftID;
		string _Density;
		string _ConvertedDensity;
		string _TankDip;
		string _WaterDip;
		string _Testing;
		string _Remark;



		public string Testing
		{
			get
			{
				return _Testing;
			}
			set
			{
				_Testing=value;
			}
		}
		public string TankID
		{
			get
			{
				return _TankID;
			}
			set
			{
				_TankID=value;
			}
		}
		public string TankName
		{
			get
			{
				return _TankName;
			}
			set
			{
				_TankName=value;
			}
		}
		public string ProdName
		{
			get
			{
				return _ProdName;
			}
			set
			{
				_ProdName=value;
			}
		}
		public string ProdAbbName
		{
			get
			{
				return _ProdAbbName;
			}
			set
			{
				_ProdAbbName=value;
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
		public string Temprature
		{
			get
			{
				return _Temprature;
			}
			set
			{
				_Temprature=value;
			}
		}
		public string WaterStock
		{
			get
			{
				return _WaterStock;
			}
			set
			{
				_WaterStock=value;
			}
		}
		public string ReserveStock
		{
			get
			{
				return _ReserveStock;
			}
			set
			{
				_ReserveStock=value;
			}
		}
		public string OpeningStock
		{
			get
			{
				return _OpeningStock;
			}
			set
			{
				_OpeningStock=value;
			}
		}
		public string MachineID
		{
			get
			{
				return _MachineID;
			}
			set
			{
				_MachineID=value;
			}
		}
		public string MachineName
		{
			get
			{
				return _MachineName;
			}
			set
			{
				_MachineName=value;
			}
		}
		public string MachineType
		{
			get
			{
				return _MachineType;
			}
			set
			{
				_MachineType=value;
			}
		}
		public string NozzelID
		{
			get
			{
				return _NozzelID;
			}
			set
			{
				_NozzelID=value;
			}
		}
		public string NozzelName
		{
			get
			{
				return _NozzelName;
			}
			set
			{
				_NozzelName=value;
			}
		}
		public string EntryDate
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
		public string Shift_ID
		{
			get
			{
				return _ShiftID;
			}
			set
			{
				_ShiftID=value;
			}
		}
		public string Density
		{
			get
			{
				return _Density;
			}
			set
			{
				_Density=value;
			}
		}
		public string ConvertedDensity
		{
			get
			{
				return _ConvertedDensity;
			}
			set
			{
				_ConvertedDensity=value;
			}
		}
		public string TankDip
		{
			get
			{
				return _TankDip;
			}
			set
			{
				_TankDip=value;
			}
		}
		public string WaterDip
		{
			get
			{
				return _WaterDip;
			}
			set
			{
				_WaterDip=value;
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

		#region Constructor Opens the onnection with database server
		public PetrolPumpClass()
		{
			SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			SqlCon.Open();	
		}
		#endregion
		
		/// <summary>
		/// Returns the SqlDataReader containing the out of the passed Query as a string parameter.
		/// </summary>
		/// <param name="Sql"></param>
		/// <returns></returns>
		public SqlDataReader GetRecordSet(string Sql)
		{
			SqlCmd=new SqlCommand (Sql,SqlCon );
			SqlDtr = SqlCmd.ExecuteReader();
			return SqlDtr ;	
		}

		/// <summary>
		/// Method use to Insert the Record , from the Query passing as a string parameter
		/// </summary>
		/// <param name="Sql"></param>
		public void InsertRecord(string Sql)
		{
			SqlCmd=new SqlCommand(Sql,SqlCon);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Method Used to Delete the Record for the passing Query as a Parameter string.
		/// </summary>
		/// <param name="Sql"></param>
		public void DeleteRecord(string Sql)
		{
			SqlCmd=new SqlCommand(Sql,SqlCon);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProTankEntry to insert the Tank Details
		/// </summary>
		public void InsertTank()
		{ 				
			SqlCmd=new SqlCommand("ProTankEntry",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@TankID",TankID.Length>0?Int32.Parse(TankID):0);
			SqlCmd.Parameters.Add("@TankName",TankName);
			SqlCmd.Parameters.Add("@ProdName",ProdName);
			SqlCmd.Parameters.Add("@Prod_AbbName",ProdAbbName);
			SqlCmd.Parameters.Add("@Capacity",Capacity.Length>0?float.Parse(Capacity):0.0);
			SqlCmd.Parameters.Add("@WaterStock",WaterStock.Length>0?float.Parse(WaterStock):0.0);
			SqlCmd.Parameters.Add("@ReserveStock",ReserveStock.Length>0?float.Parse(ReserveStock):0.0);
			SqlCmd.Parameters.Add("@OpeningStock",OpeningStock.Length>0?float.Parse(OpeningStock):0.0);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProTankUpdate to Update the Tank Details
		/// </summary>
		public void UpdateTank()
		{ 				
			SqlCmd=new SqlCommand("ProTankUpdate",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@TankID",TankID.Length>0?Int32.Parse(TankID):0);
			SqlCmd.Parameters.Add("@TankName",TankName);
			SqlCmd.Parameters.Add("@ProdName",ProdName);
			SqlCmd.Parameters.Add("@Prod_AbbName",ProdAbbName);
			SqlCmd.Parameters.Add("@Capacity",Capacity.Length>0?float.Parse(Capacity):0.0);
			SqlCmd.Parameters.Add("@WaterStock",WaterStock.Length>0?float.Parse(WaterStock):0.0);
			SqlCmd.Parameters.Add("@ReserveStock",ReserveStock.Length>0?float.Parse(ReserveStock):0.0);
			SqlCmd.Parameters.Add("@OpeningStock",OpeningStock.Length>0?float.Parse(OpeningStock):0.0);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProMachineEntry to insert the Machine Entry
		/// </summary>
		public void InsertMachine()
		{ 				
			SqlCmd=new SqlCommand("ProMachineEntry",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@MachineID",MachineID.Length>0?Int32.Parse(MachineID):0);
			SqlCmd.Parameters.Add("@MachineName",MachineName);
			SqlCmd.Parameters.Add("@MachineType",MachineType);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProNozzleEntry to insert the Nozzle Details
		/// </summary>
		public void InsertNozzle()
		{ 				
			SqlCmd=new SqlCommand("ProNozzleEntry",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@NozzleID",NozzelID.Length>0?Int32.Parse(NozzelID):0);
			SqlCmd.Parameters.Add("@NozzleName",NozzelName);
			SqlCmd.Parameters.Add("@MachineID",MachineID.Length>0?Int32.Parse(MachineID):0);
			SqlCmd.Parameters.Add("@TankID",TankID.Length>0?Int32.Parse(TankID):0);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProNozzleUpdate to Update the Nozzle Details
		/// </summary>
		public void UpdateNozzle()
		{ 				
			SqlCmd=new SqlCommand("ProNozzleUpdate",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@NozzleID",NozzelID.Length>0?Int32.Parse(NozzelID):0);
			SqlCmd.Parameters.Add("@NozzleName",NozzelName);
			SqlCmd.Parameters.Add("@MachineID",MachineID.Length>0?Int32.Parse(MachineID):0);
			SqlCmd.Parameters.Add("@TankID",TankID.Length>0?Int32.Parse(TankID):0); // NozzelID
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProDailyTankReading to insert the Daily Tank Reading Details.
		/// </summary>
		public void InsertDailyTankReading()
		{ 				
			SqlCmd=new SqlCommand("ProDailyTankReading",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@EntryDate",EntryDate);
			SqlCmd.Parameters.Add("@ProdName",ProdName);
			SqlCmd.Parameters.Add("@TankName",TankName);
			SqlCmd.Parameters.Add("@Density",float.Parse(Density));
			SqlCmd.Parameters.Add("@Temprature",float.Parse(Temprature));
			SqlCmd.Parameters.Add("@ConvertedDensity",float.Parse(ConvertedDensity));
			SqlCmd.Parameters.Add("@OpeningStock",float.Parse(OpeningStock));
			SqlCmd.Parameters.Add("@TankDip",float.Parse(TankDip));
			SqlCmd.Parameters.Add("@WaterDip",WaterDip);
			SqlCmd.Parameters.Add("@Testing",Testing);
			SqlCmd.Parameters.Add("@Remark",Remark);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Calls the Procedure ProDailyMeterReading to insert the Daily Meter Reading Details.
		/// </summary>
		public void InsertDailyMeterReading()
		{ 				
			SqlCmd=new SqlCommand("ProDailyMeterReading",SqlCon);
			SqlCmd.CommandType = CommandType.StoredProcedure;
			SqlCmd.Parameters.Add("@EntryDate",EntryDate);
			SqlCmd.Parameters.Add("@MachineName",MachineName);
			SqlCmd.Parameters.Add("@NozzleName",NozzelName);
			//SqlCmd.Parameters.Add("@Reading",float.Parse(Reading));
			SqlCmd.Parameters.Add("@Reading",Reading);
			SqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Returns the No. of fuel products in the form of Integer Value
		/// </summary>
		/// <returns></returns>
		public int GetTotalFuelProducts()
		{
			int n=0;
			string sql;

			#region Fetch Total Fuel Products
			sql="select  count(distinct Prod_Name) from tank";
			SqlDtr=GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				n=System.Convert.ToInt32(SqlDtr.GetValue(0)); 
			}
			SqlDtr.Close(); 
			#endregion

			return(n);
		}

		/// <summary>
		/// Returns the string[] array containing the name of all the Fuel Products.
		/// </summary>
		/// <returns></returns>
		public string[] GetFuelProducts()
		{
			int n=GetTotalFuelProducts();
			string[] Products=new string[n];	
			string sql;	

			#region Fetch All Fuel Products Name
			sql="select distinct Prod_Name from Tank";
			SqlDtr=GetRecordSet(sql);
			for(int i=0;SqlDtr.Read();i++)
				Products[i]=SqlDtr.GetValue(0).ToString(); 
			SqlDtr.Close();
			#endregion

			return(Products);
		}

		/// <summary>
		/// Retruns the int[] array conataining the no. of tanks of each fuel products
		/// </summary>
		/// <returns></returns>
		public int[] GetFuelWiseTanks()
		{
			int n=GetTotalFuelProducts();
			int[] TotalTanks=new int[n];	
			string sql;	

			#region Fetch Total Tanks in Each Product
			sql="select count(*) from Tank	group by Prod_Name";
			SqlDtr=GetRecordSet(sql);
			for(int i=0;SqlDtr.Read();i++)
				TotalTanks[i]=System.Convert.ToInt32(SqlDtr.GetValue(0)); 
			SqlDtr.Close();
			#endregion

			return(TotalTanks);
		}


		/// <summary>
		/// Returns the No. of machine from table machine as a int value.
		/// </summary>
		/// <returns></returns>
		public int GetTotalMachines()
		{
			int n=0;
			string sql;

			#region Fetch Total Machines Available
			sql="select  count(distinct Machine_ID) from Nozzle";
			SqlDtr=GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				n=System.Convert.ToInt32(SqlDtr.GetValue(0)); 
			}
			SqlDtr.Close(); 
			#endregion

			return(n);
		}

		/// <summary>
		/// Retruns string[] array containing the machine names from table machine.
		/// </summary>
		/// <returns></returns>
		public string[] GetMachines()
		{
			int n=GetTotalMachines();
			string[] Machines=new string[n];	
			string sql;	

			#region Fetch All Machine Name
			sql="select distinct Machine_Name from Machine";
			SqlDtr=GetRecordSet(sql);
			for(int i=0;SqlDtr.Read();i++)
				Machines[i]=SqlDtr.GetValue(0).ToString(); 
			SqlDtr.Close();
			#endregion

			return(Machines);
		}


		/// <summary>
		/// Returns the int[] array containing the total no. of nozzle connected to each machine.
		/// </summary>
		/// <returns></returns>
		public int[] GetNozzles()
		{
			int n=GetTotalMachines();
			int[] Nozzles=new int[n];			
			string sql;	

			#region Fetch Total Nozzles in Each Machines
			sql="select count(*) from Nozzle group by Machine_ID";
			SqlDtr=GetRecordSet(sql);
			for(int i=0;SqlDtr.Read();i++)
				Nozzles[i]=System.Convert.ToInt32(SqlDtr.GetValue(0)); 
			SqlDtr.Close();
			#endregion

			return(Nozzles);
		}

		/// <summary>
		/// Returns the Product Name for the passing Machine name and Nozzle as a parameter.
		/// </summary>
		/// <param name="MachineName"></param>
		/// <param name="NozzleName"></param>
		/// <returns></returns>
		public string GetNozzleProduct(string MachineName,string NozzleName)
		{   
			string str="";
			string sql;	

			#region Fetch Product Name in a Particular Nozzle
			sql="select Prod_AbbName from Tank where Tank_ID="+
				"(select Tank_ID from Nozzle where Nozzle_ID="+
				"(select Nozzle_ID from Nozzle where Nozzle_Name='"+NozzleName+"' and Machine_ID="+
				"(select Machine_ID from Machine where Machine_Name='"+MachineName+"')))";
			SqlDtr=GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				str=SqlDtr.GetValue(0).ToString();
			}
			SqlDtr.Close();
			#endregion

			return(str);
		}
	}
}
