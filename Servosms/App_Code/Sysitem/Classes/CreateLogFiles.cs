/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
using System;
using System.IO;
using System.Text;

namespace Servosms.Sysitem.Classes
{
	/// <summary>
	/// Summary description for CreateLogFiles.
	/// </summary>
	public class CreateLogFiles
	{
		//private string sErrorTime;
		static string fileName;
		static string sLogFormat;
		public CreateLogFiles()
		{
			//if (System.IO.Directory.Exists(Server.MapPath("LOG")))
			//{
				//System.IO.Directory.CreateDirectory(Server.MapPath("LOG"));
			//}
			//sLogFormat used to create log files format :
			// dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
			//this variable used to create log filename format "
			//for example filename : ErrorLogYYYYMMDD
			//	string sYear	= DateTime.Now.Year.ToString();
			//string sMonth	= DateTime.Now.Month.ToString();
			//	string sDay	= DateTime.Now.Day.ToString();
			//sErrorTime = sYear+sMonth+sDay;
		}

		/// <summary>
		/// static method to create and write into the log file
		/// </summary>
		public static void ErrorLog(string sErrMsg)
		{
			//string home_drive = Environment.SystemDirectory;
			//home_drive = home_drive.Substring(0,2); 
			//fileName = home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\Logs\\" +DateTime.Today.Day.ToString()+ "-" +DateTime.Today.Month.ToString() + "-" + DateTime.Today.Year.ToString() + ".log";
			//sLogFormat = DateTime.Now.ToShortDateString().ToString()+" "+DateTime.Now.ToLongTimeString().ToString()+" ==> ";
			////StreamWriter sw = new StreamWriter(@"C:\Inetpub\wwwroot\Servosms\Logs"+sErrorTime,true);
			//StreamWriter sw = new StreamWriter(fileName,true);

			//sw.WriteLine(sLogFormat + sErrMsg);
			//sw.Flush();
			//sw.Close();
		}
	}
}
//CreateLogFiles.ErrorLog("Form:ProductWiseSales.aspx,Method:cmdrpt_Click" + ex.Message);