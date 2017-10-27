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
using Servosms.Sysitem.Classes;

namespace Servosms.Module.Inventory
{
	/// <summary>
	/// Summary description for SchemeDiscount.
	/// </summary>
	public partial class SchemeDiscount : System.Web.UI.Page
	{
		string uid;
		public static string[] str=null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				uid=Session["User_Name"].ToString();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SchemeDiscount.aspx,Method:pageload"+ex.Message+"  EXCEPTION "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			string s=Request.Params.Get("chk");
			str= s.Split(new char[] {':'},s.Length);
			InventoryClass obj = new InventoryClass();
			chkinfo.Value=str[0];
			btnSubmit.Attributes.Add("OnClick","check();");
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
	}
}
