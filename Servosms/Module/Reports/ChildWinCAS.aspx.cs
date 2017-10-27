using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RMG;
using Servosms.Sysitem.Classes;
using System.Data.SqlClient;

namespace Servosms.Module.Inventory
{
	/// <summary>
	/// Summary description for groupchild.
	/// </summary>
	public partial class ChildWinCAS : System.Web.UI.Page
	{
		string uid;
		public static int Tot_Rows=0;
		public static string[] str=null;
		public static string[] scheme_group=null;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=Session["User_Name"].ToString();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:GroupChild.aspx,Method:pageload"+ex.Message+"  EXCEPTION "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}

			string childinfo=Request.Params.Get("chk");
			str= childinfo.Split(new char[] {':'},childinfo.Length);
			InventoryClass obj = new InventoryClass();
			SqlDataReader dtr=null;
			string tempinfo1="";
			string st=str[0]+" : "+str[1]+" : "+str[2];
			btnSubmit.Attributes.Add("OnClick","getclose();");
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	
		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			try
			{
				string selected_group="";
				for(int i=1;i<Tot_Rows;i++)
				{
					if(Request.Params.Get("chk"+i)!=null)
					{
						selected_group+=Request.Params.Get("chk"+i).ToString()+",";
					}
				}
				Session["group"]=selected_group.ToString();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : GroupChild.aspx,Method : btnSubmit_Click"+ex.Message+"  EXCEPTION "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
		}
	}
}
