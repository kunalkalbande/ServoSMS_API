using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using DBOperations;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;
using RMG;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for SSRWiseTragets.
	/// </summary>
	public partial class SSRWiseTargets : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden temptotmonth;
		protected System.Web.UI.HtmlControls.HtmlInputHidden temptotemp;
		public string Next_id="0";
		public static int View; 
		public string[] DateFrom = null;
		public static int count=0;
		public double[] TotalSum = null;
		public double[] Weeks_Total = new double[48];
		public double[] Tar_Total = new double[48];
		//public double[] IIWeek_Total = new double[48];
		//public double[] IIIWeek_Total = new double[48];
		//public double[] IVWeek_Total = new double[48];
		public string[] DateTo = null;
		public int ds11=0;
		public int ds12=0;
		public int ds21=0;
		public int ds22=0;
		public int ds10=0;
		public int ds20=0;
		public double Tot_Achiv_I=0;
		public double Tot_Achiv_II=0;
		public double Tot_Achiv_III=0;
		public double Tot_Achiv_IV=0;
		string UID;
		public string[] New_todate=new string[4];
		public string[] New_fromdate=new string[4];

		public double[] Achivment=new double[4];
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				UID=(Session["User_Name"].ToString());
				
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Salesreport.aspx,Class:PetrolPumpClass.cs,Method: page_load " + ex.Message+"  EXCEPTION " +" userid  "+UID);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			try
			{
				if(!Page.IsPostBack)
				{
					fill_Drop();
					ArrayList TotalSum = new ArrayList();
					View=0;
					count=0;
					txttodate.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					txtfromdate.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());

					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="60";
					string[,] Priv=(string[,]) Session["Privileges"];
					for(i=0;i<Priv.GetLength(0);i++)
					{
						if(Priv[i,0]== Module &&  Priv[i,1]==SubModule)
						{						
							View_flag=Priv[i,2];
							Add_Flag=Priv[i,3];
							Edit_Flag=Priv[i,4];
							Del_Flag=Priv[i,5];
							break;
						}
					}	
					if(View_flag=="0")
					{
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					}
					#endregion
				}
				
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Salesreport.aspx,Class:PetrolPumpClass.cs,Method: page_load " + ex.Message+"  EXCEPTION " +" userid  "+UID);
			}
            txtfromdate.Text = Request.Form["txtfromdate"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtfromdate"].ToString().Trim();
            txttodate.Text = Request.Form["txttodate"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txttodate"].ToString().Trim();
        }

		public void fill_Drop()
		{
			DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			InventoryClass obj9=new InventoryClass ();
			SqlDataReader rdr=null;
			//coment by vikas string sql="select emp_name,emp_id from employee where designation='Servo Sales Representative'";
			string sql="select emp_name,emp_id from employee where designation='Servo Sales Representative' and status=1";
			rdr=obj9.GetRecordSet(sql);
			DropSSR.Items.Clear();
			DropSSR.Items.Add("All");
			while(rdr.Read())
			{
				DropSSR.Items.Add(rdr.GetValue(1).ToString().Trim()+":"+rdr.GetValue(0).ToString().Trim());
				
			}
			rdr.Close();
			
			
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

		public static string month(string s1)
		{
			string[] ds1 =s1.IndexOf("/")>0? s1.Split(new char[] {'/'},s1.Length): s1.Split(new char[] { '-' }, s1.Length);
			ds1[0]="31";
			return ds1[1] + "/" + ds1[0] + "/" + ds1[2];
		}
		
		public void Gen_Next_Id()
		{
			try
			{
				PartiesClass obj=new PartiesClass ();
				SqlDataReader SqlDtr;
				SqlDtr = obj.GetRecordSet ("select max(swtid)+1 from ssr_wise_Targets");
				while(SqlDtr.Read ())
				{
					Next_id=SqlDtr.GetValue(0).ToString ();
					if(Next_id=="")
					{
						Next_id="1";
					}
				}
				SqlDtr.Close();
				//return Next_id;
			}
			catch(Exception ex)
			{
				//return Next_id;
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method:FillID().  EXCEPTION "+ ex.Message+"  "+UID);
			}
		
		}

		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
				SqlConnection cn=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				cn.Open();
				SqlCommand cmd;
				InventoryClass obj=new InventoryClass ();
				InventoryClass obj1=new InventoryClass ();
				SqlDataReader SqlDtr,rdr=null;
				string sql="",sql2="";
				int Tar=0,check=0;
				int Tot_Achiv=0,Tot_Tar=0,Tot_check=0,tot_mon=0,tot_emp=0;
				if(View==1) 
				{ 
					int frmDay=0,frmMonth=0,frmYear=0,toDay=0,toMonth=0,toYear=0;
					string todate="";
					string fromdate="";
					todate=txttodate.Text;
					fromdate=txtfromdate.Text;
					string[] Todt =todate.IndexOf("/")>0? todate.Split(new char[] {'/'},todate.Length) : todate.Split(new char[] { '-' }, todate.Length);
					string[] Frmdt =fromdate.IndexOf("/")>0? fromdate.Split(new char[] {'/'},fromdate.Length): fromdate.Split(new char[] { '-' }, fromdate.Length);
					frmDay=System.Convert.ToInt32(Frmdt[0]);
					frmMonth=System.Convert.ToInt32(Frmdt[1]);
					frmYear=System.Convert.ToInt32(Frmdt[2]);
					toDay=System.Convert.ToInt32(Todt[0]);
					toMonth=System.Convert.ToInt32(Todt[1]);
					toYear=System.Convert.ToInt32(Todt[2]);
					int ssr_id=0;
					string txtfirtar="";
					string txtSectar="";
					string txtThtar="";
					string txtFoutar="";
					InventoryClass obj2=new InventoryClass ();
					SqlDataReader rdr2=null;
					int SN=1;
					if(DropSSR.SelectedIndex==0) 
					{ 
						sql="select emp_name,emp_id from employee where designation='Servo Sales Representative'";
						rdr=obj.GetRecordSet(sql);
						while(rdr.Read())
						{
							ssr_id=Convert.ToInt32(rdr.GetValue(1));
							if(frmYear==toYear)
							{
								for(int p=frmMonth;p<=toMonth;p++)
								{
									txtfirtar=Request.Params.Get("txtFirTar"+SN+++"");
									txtSectar=Request.Params.Get("txtSecTar"+SN+++"");
									txtThtar=Request.Params.Get("txtThTar"+SN+++"");
									txtFoutar=Request.Params.Get("txtFouTar"+SN+++"");
									if(txtfirtar!=null && txtfirtar.ToString()!="0")
									{
										sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=1";
										rdr2=obj2.GetRecordSet(sql2);
										if(!rdr2.HasRows)
										{
											sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",1,'"+txtfirtar.ToString().Trim()+"')";
										}
										else
										{
											sql="update SSR_wise_Targets set target='"+txtfirtar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=1";
										}
										cmd=new SqlCommand(sql,cn);
										cmd.ExecuteNonQuery();
										cmd.Dispose();
										rdr2.Close();
									}
									if(txtSectar!=null && txtSectar.ToString()!="0")
									{
										sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=2";
										rdr2=obj2.GetRecordSet(sql2);
										if(!rdr2.HasRows)
										{
											sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",2,'"+txtSectar.ToString().Trim()+"')";
										}
										else
										{
											sql="update SSR_wise_Targets set target='"+txtSectar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=2";
										}
										cmd=new SqlCommand(sql,cn);
										cmd.ExecuteNonQuery();
										cmd.Dispose();
										rdr2.Close();
									}
									if(txtThtar!=null && txtThtar.ToString()!="0")
									{
										sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=3";
										rdr2=obj2.GetRecordSet(sql2);
										if(!rdr2.HasRows)
										{
											sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",3,'"+txtThtar.ToString().Trim()+"')";
										}
										else
										{
											sql="update SSR_wise_Targets set target='"+txtThtar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=3";
										}
										cmd=new SqlCommand(sql,cn);
										cmd.ExecuteNonQuery();
										cmd.Dispose();
										rdr2.Close();
									}
									if(txtFoutar!=null && txtFoutar.ToString()!="0")
									{
										int day=DateTime.DaysInMonth(frmYear,p);
										sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=4";
										rdr2=obj2.GetRecordSet(sql2);
										if(!rdr2.HasRows)
										{
											sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",4,'"+txtFoutar.ToString().Trim()+"')";
										}
										else
										{
											sql="update SSR_wise_Targets set target='"+txtFoutar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=4";
										}
										cmd=new SqlCommand(sql,cn);
										cmd.ExecuteNonQuery();
										cmd.Dispose();
										rdr2.Close();
									}
								}
							}
							else
							{
								for(int p=frmMonth;p<=12;p++)
								{
									txtfirtar=Request.Params.Get("txtFirTar"+SN+++"");
									txtSectar=Request.Params.Get("txtSecTar"+SN+++"");
									txtThtar=Request.Params.Get("txtThTar"+SN+++"");
									txtFoutar=Request.Params.Get("txtFouTar"+SN+++"");
									if(txtfirtar!=null && txtfirtar.ToString()!="0")
									{
										sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=1";
										rdr2=obj2.GetRecordSet(sql2);
										if(!rdr2.HasRows)
										{
											sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",1,'"+txtfirtar.ToString().Trim()+"')";
										}
										else
										{
											sql="update SSR_wise_Targets set target='"+txtfirtar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=1";
										}
										cmd=new SqlCommand(sql,cn);
										cmd.ExecuteNonQuery();
										cmd.Dispose();
										rdr2.Close();
									}
									if(txtSectar!=null && txtSectar.ToString()!="0")
									{
										sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=2";
										rdr2=obj2.GetRecordSet(sql2);
										if(!rdr2.HasRows)
										{
											sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",2,'"+txtSectar.ToString().Trim()+"')";
										}
										else
										{
											sql="update SSR_wise_Targets set target='"+txtSectar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=2";
										}
										cmd=new SqlCommand(sql,cn);
										cmd.ExecuteNonQuery();
										cmd.Dispose();
										rdr2.Close();
									}
									if(txtThtar!=null && txtThtar.ToString()!="0")
									{
										sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=3";
										rdr2=obj2.GetRecordSet(sql2);
										if(!rdr2.HasRows)
										{
											sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",3,'"+txtThtar.ToString().Trim()+"')";
										}
										else
										{
											sql="update SSR_wise_Targets set target='"+txtThtar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=3";
										}
										cmd=new SqlCommand(sql,cn);
										cmd.ExecuteNonQuery();
										cmd.Dispose();
										rdr2.Close();
									}
									if(txtFoutar!=null && txtFoutar.ToString()!="0")
									{
										int day=DateTime.DaysInMonth(frmYear,p);
										sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=4";
										rdr2=obj2.GetRecordSet(sql2);
										if(!rdr2.HasRows)
										{
											sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",4,'"+txtFoutar.ToString().Trim()+"')";
										}
										else
										{
											sql="update SSR_wise_Targets set target='"+txtFoutar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=4";
										}
										cmd=new SqlCommand(sql,cn);
										cmd.ExecuteNonQuery();
										cmd.Dispose();
										rdr2.Close();
									}
								}

								for(int p=1;p<=toMonth;p++)
								{
									txtfirtar=Request.Params.Get("txtFirTar"+SN+++"");
									txtSectar=Request.Params.Get("txtSecTar"+SN+++"");
									txtThtar=Request.Params.Get("txtThTar"+SN+++"");
									txtFoutar=Request.Params.Get("txtFouTar"+SN+++"");
									if(txtfirtar!=null && txtfirtar.ToString()!="0")
									{
										sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+toYear+"and month="+p+" and week=1";
										rdr2=obj2.GetRecordSet(sql2);
										if(!rdr2.HasRows)
										{
											sql="insert into SSR_wise_Targets values("+ssr_id+","+toYear+","+p+",1,'"+txtfirtar.ToString().Trim()+"')";
										}
										else
										{
											sql="update SSR_wise_Targets set target='"+txtfirtar+"' where ssrid="+ssr_id+" and year="+toYear+" and month="+p+" and week=1";
										}
										cmd=new SqlCommand(sql,cn);
										cmd.ExecuteNonQuery();
										cmd.Dispose();
										rdr2.Close();
									}
									if(txtSectar!=null && txtSectar.ToString()!="0")
									{
										sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+toYear+"and month="+p+" and week=2";
										rdr2=obj2.GetRecordSet(sql2);
										if(!rdr2.HasRows)
										{
											sql="insert into SSR_wise_Targets values("+ssr_id+","+toYear+","+p+",2,'"+txtSectar.ToString().Trim()+"')";
										}
										else
										{
											sql="update SSR_wise_Targets set target='"+txtSectar+"' where ssrid="+ssr_id+" and year="+toYear+" and month="+p+" and week=2";
										}
										cmd=new SqlCommand(sql,cn);
										cmd.ExecuteNonQuery();
										cmd.Dispose();
										rdr2.Close();
									}
									if(txtThtar!=null && txtThtar.ToString()!="0")
									{
										sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+toYear+"and month="+p+" and week=3";
										rdr2=obj2.GetRecordSet(sql2);
										if(!rdr2.HasRows)
										{
											sql="insert into SSR_wise_Targets values("+ssr_id+","+toYear+","+p+",3,'"+txtThtar.ToString().Trim()+"')";
										}
										else
										{
											sql="update SSR_wise_Targets set target='"+txtThtar+"' where ssrid="+ssr_id+" and year="+toYear+" and month="+p+" and week=3";
										}
										cmd=new SqlCommand(sql,cn);
										cmd.ExecuteNonQuery();
										cmd.Dispose();
										rdr2.Close();
									}
									if(txtFoutar!=null && txtFoutar.ToString()!="0")
									{
										int day=DateTime.DaysInMonth(frmYear,p);
										sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+toYear+"and month="+p+" and week=4";
										rdr2=obj2.GetRecordSet(sql2);
										if(!rdr2.HasRows)
										{
											sql="insert into SSR_wise_Targets values("+ssr_id+","+toYear+","+p+",4,'"+txtFoutar.ToString().Trim()+"')";
										}
										else
										{
											sql="update SSR_wise_Targets set target='"+txtFoutar+"' where ssrid="+ssr_id+" and year="+toYear+" and month="+p+" and week=4";
										}
										cmd=new SqlCommand(sql,cn);
										cmd.ExecuteNonQuery();
										cmd.Dispose();
										rdr2.Close();
									}
								}
								
							}



						}
						tot_emp++;
					}
					else
					{
						string SSR=DropSSR.SelectedValue;
						SSR=SSR.Substring(0,SSR.IndexOf(":"));
						ssr_id=Convert.ToInt32(SSR);
						if(frmYear==toYear)
						{
								
							for(int p=frmMonth;p<=toMonth;p++)
							{
								txtfirtar=Request.Params.Get("txtFirTar"+SN+++"");
								txtSectar=Request.Params.Get("txtSecTar"+SN+++"");
								txtThtar=Request.Params.Get("txtThTar"+SN+++"");
								txtFoutar=Request.Params.Get("txtFouTar"+SN+++"");
								if(txtfirtar!=null || txtfirtar.ToString()!="0")
								{
									sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=1";
									rdr2=obj2.GetRecordSet(sql2);
									if(!rdr2.HasRows)
									{
										sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",1,'"+txtfirtar.ToString().Trim()+"')";
									}
									else
									{
										sql="update SSR_wise_Targets set target='"+txtfirtar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=1";
									}
									cmd=new SqlCommand(sql,cn);
									cmd.ExecuteNonQuery();
									cmd.Dispose();
									rdr2.Close();
								}
								if(txtSectar!=null || txtSectar.ToString()!="0")
								{
									sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=2";
									rdr2=obj2.GetRecordSet(sql2);
									if(!rdr2.HasRows)
									{
										sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",2,'"+txtSectar.ToString().Trim()+"')";
									}
									else
									{
										sql="update SSR_wise_Targets set target='"+txtSectar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=2";
									}
									cmd=new SqlCommand(sql,cn);
									cmd.ExecuteNonQuery();
									cmd.Dispose();
									rdr2.Close();
								}
								if(txtThtar!=null || txtThtar.ToString()!="0")
								{
									sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=3";
									rdr2=obj2.GetRecordSet(sql2);
									if(!rdr2.HasRows)
									{
										sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",3,'"+txtThtar.ToString().Trim()+"')";
									}
									else
									{
										sql="update SSR_wise_Targets set target='"+txtThtar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=3";
									}
									cmd=new SqlCommand(sql,cn);
									cmd.ExecuteNonQuery();
									cmd.Dispose();
									rdr2.Close();
								}
								if(txtFoutar!=null || txtFoutar.ToString()!="0")
								{
									int day=DateTime.DaysInMonth(frmYear,p);
									sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=4";
									rdr2=obj2.GetRecordSet(sql2);
									if(!rdr2.HasRows)
									{
										sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",4,'"+txtFoutar.ToString().Trim()+"')";
									}
									else
									{
										sql="update SSR_wise_Targets set target='"+txtFoutar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=4";
									}
									cmd=new SqlCommand(sql,cn);
									cmd.ExecuteNonQuery();
									cmd.Dispose();
									rdr2.Close();
								}
							}
						}
						else
						{
							for(int p=frmMonth;p<=12;p++)
							{
								txtfirtar=Request.Params.Get("txtFirTar"+SN+++"");
								txtSectar=Request.Params.Get("txtSecTar"+SN+++"");
								txtThtar=Request.Params.Get("txtThTar"+SN+++"");
								txtFoutar=Request.Params.Get("txtFouTar"+SN+++"");
								if(txtfirtar!=null || txtfirtar.ToString()!="0")
								{
									sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=1";
									rdr2=obj2.GetRecordSet(sql2);
									if(!rdr2.HasRows)
									{
										sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",1,'"+txtfirtar.ToString().Trim()+"')";
									}
									else
									{
										sql="update SSR_wise_Targets set target='"+txtfirtar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=1";
									}
									cmd=new SqlCommand(sql,cn);
									cmd.ExecuteNonQuery();
									cmd.Dispose();
									rdr2.Close();
								}
								if(txtSectar!=null || txtSectar.ToString()!="0")
								{
									sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=2";
									rdr2=obj2.GetRecordSet(sql2);
									if(!rdr2.HasRows)
									{
										sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",2,'"+txtSectar.ToString().Trim()+"')";
									}
									else
									{
										sql="update SSR_wise_Targets set target='"+txtSectar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=2";
									}
									cmd=new SqlCommand(sql,cn);
									cmd.ExecuteNonQuery();
									cmd.Dispose();
									rdr2.Close();
								}
								if(txtThtar!=null || txtThtar.ToString()!="0")
								{
									sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=3";
									rdr2=obj2.GetRecordSet(sql2);
									if(!rdr2.HasRows)
									{
										sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",3,'"+txtThtar.ToString().Trim()+"')";
									}
									else
									{
										sql="update SSR_wise_Targets set target='"+txtThtar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=3";
									}
									cmd=new SqlCommand(sql,cn);
									cmd.ExecuteNonQuery();
									cmd.Dispose();
									rdr2.Close();
								}
								if(txtFoutar!=null || txtFoutar.ToString()!="0")
								{
									int day=DateTime.DaysInMonth(frmYear,p);
									sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+frmYear+"and month="+p+" and week=4";
									rdr2=obj2.GetRecordSet(sql2);
									if(!rdr2.HasRows)
									{
										sql="insert into SSR_wise_Targets values("+ssr_id+","+frmYear+","+p+",4,'"+txtFoutar.ToString().Trim()+"')";
									}
									else
									{
										sql="update SSR_wise_Targets set target='"+txtFoutar+"' where ssrid="+ssr_id+" and year="+frmYear+" and month="+p+" and week=4";
									}
									cmd=new SqlCommand(sql,cn);
									cmd.ExecuteNonQuery();
									cmd.Dispose();
									rdr2.Close();
								}
							}

							for(int p=1;p<=toMonth;p++)
							{
								txtfirtar=Request.Params.Get("txtFirTar"+SN+++"");
								txtSectar=Request.Params.Get("txtSecTar"+SN+++"");
								txtThtar=Request.Params.Get("txtThTar"+SN+++"");
								txtFoutar=Request.Params.Get("txtFouTar"+SN+++"");
								if(txtfirtar!=null || txtfirtar.ToString()!="0")
								{
									sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+toYear+"and month="+p+" and week=1";
									rdr2=obj2.GetRecordSet(sql2);
									if(!rdr2.HasRows)
									{
										sql="insert into SSR_wise_Targets values("+ssr_id+","+toYear+","+p+",1,'"+txtfirtar.ToString().Trim()+"')";
									}
									else
									{
										sql="update SSR_wise_Targets set target='"+txtfirtar+"' where ssrid="+ssr_id+" and year="+toYear+" and month="+p+" and week=1";
									}
									cmd=new SqlCommand(sql,cn);
									cmd.ExecuteNonQuery();
									cmd.Dispose();
									rdr2.Close();
								}
								if(txtSectar!=null || txtSectar.ToString()!="0")
								{
									sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+toYear+"and month="+p+" and week=2";
									rdr2=obj2.GetRecordSet(sql2);
									if(!rdr2.HasRows)
									{
										sql="insert into SSR_wise_Targets values("+ssr_id+","+toYear+","+p+",2,'"+txtSectar.ToString().Trim()+"')";
									}
									else
									{
										sql="update SSR_wise_Targets set target='"+txtSectar+"' where ssrid="+ssr_id+" and year="+toYear+" and month="+p+" and week=2";
									}
									cmd=new SqlCommand(sql,cn);
									cmd.ExecuteNonQuery();
									cmd.Dispose();
									rdr2.Close();
								}
								if(txtThtar!=null || txtThtar.ToString()!="0")
								{
									sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+toYear+"and month="+p+" and week=3";
									rdr2=obj2.GetRecordSet(sql2);
									if(!rdr2.HasRows)
									{
										sql="insert into SSR_wise_Targets values("+ssr_id+","+toYear+","+p+",3,'"+txtThtar.ToString().Trim()+"')";
									}
									else
									{
										sql="update SSR_wise_Targets set target='"+txtThtar+"' where ssrid="+ssr_id+" and year="+toYear+" and month="+p+" and week=3";
									}
									cmd=new SqlCommand(sql,cn);
									cmd.ExecuteNonQuery();
									cmd.Dispose();
									rdr2.Close();
								}
								if(txtFoutar!=null || txtFoutar.ToString()!="0")
								{
									int day=DateTime.DaysInMonth(frmYear,p);
									sql2="select * from SSR_Wise_Targets where ssrid="+ssr_id+" and year="+toYear+"and month="+p+" and week=4";
									rdr2=obj2.GetRecordSet(sql2);
									if(!rdr2.HasRows)
									{
										sql="insert into SSR_wise_Targets values("+ssr_id+","+toYear+","+p+",4,'"+txtFoutar.ToString().Trim()+"')";
									}
									else
									{
										sql="update SSR_wise_Targets set target='"+txtFoutar+"' where ssrid="+ssr_id+" and year="+toYear+" and month="+p+" and week=4";
									}
									cmd=new SqlCommand(sql,cn);
									cmd.ExecuteNonQuery();
									cmd.Dispose();
									rdr2.Close();
								}
							}
						}
					}
					MessageBox.Show("SSR Wise Targets Successfully Save");
					txtfromdate.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
					txttodate.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
					DropSSR.SelectedIndex=0;
				}
			}
			catch(Exception ex)
			{
				//MessageBox.Show(ex.Message.ToString());
			}
		}


		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			
			string s1="";
			string s2="";
			s1=txttodate.Text;
			s2=txtfromdate.Text;
			string[] ds1 =s2.IndexOf("/")>0? s2.Split(new char[] {'/'},s2.Length): s2.Split(new char[] { '-' }, s2.Length);
			string[] ds2 =s1.IndexOf("/")>0? s1.Split(new char[] {'/'},s1.Length) : s1.Split(new char[] { '-' }, s1.Length);

			ds10=System.Convert.ToInt32(ds1[0]);

			ds20=System.Convert.ToInt32(ds2[0]);

			ds11=System.Convert.ToInt32(ds1[1]);

			ds12=System.Convert.ToInt32(ds1[2]);

			ds21=System.Convert.ToInt32(ds2[1]);

			ds22=System.Convert.ToInt32(ds2[2]);

			if(ds12==ds22 && ds11 > ds21)
			{
				MessageBox.Show("Please Select Greater Month in DateTo");
				View=0;
				return;
			}
			if(ds10 >ds20 && ds12==ds22 && ds11 == ds21 )
			{
				MessageBox.Show("Please Select Greater Date");
				View=0;
				return;
			}
			if((ds22-ds12) > 1)
			{
				MessageBox.Show("Please Select date between one year");
				View=0;
				return;
			}
			if((ds22-ds12) == -1 || ((ds22-ds12) >= 1 && ds21 >=ds11))
			{
				MessageBox.Show("Please Select date between one year");
				View=0;
				return;
			}
			getDate(ds10,ds11,ds12,ds20,ds21,ds22);
			try
			{
				if(View == 1)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:SSR_Wise_TargetReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   SSR_Wise_TargetReport Convert Into Excel Format, userid  "+UID);
				}
				else
				{
					MessageBox.Show("Please Click the View Button First");
					return;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:TargetVsAchivement.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    TargetVsAchivement Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+UID);
			}
		}

		public void ConvertToExcel()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\SSR_Wise_Target_Report.xls";
			StreamWriter sw = new StreamWriter(path);

			try
			{
				DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
				InventoryClass obj=new InventoryClass ();
				InventoryClass obj1=new InventoryClass ();
				SqlDataReader SqlDtr,rdr=null;
				string sql="",sql2="";
				int Achiv=0,Tar=0,check=0;
				int Tot_Achiv=0,Tot_Tar=0,Tot_check=0,tot_mon=0,tot_emp=0;
				if(View==1) 
				{ 
					int frmDay=0,frmMonth=0,frmYear=0,toDay=0,toMonth=0,toYear=0;
					string todate="";
					string fromdate="";
					todate=txttodate.Text;
					fromdate=txtfromdate.Text;
					string[] Todt = todate.Split(new char[] {'/'},todate.Length);
					string[] Frmdt = fromdate.Split(new char[] {'/'},fromdate.Length);
					frmDay=System.Convert.ToInt32(Frmdt[0]);
					frmMonth=System.Convert.ToInt32(Frmdt[1]);
					frmYear=System.Convert.ToInt32(Frmdt[2]);
					toDay=System.Convert.ToInt32(Todt[0]);
					toMonth=System.Convert.ToInt32(Todt[1]);
					toYear=System.Convert.ToInt32(Todt[2]);
					string ssr_id="";
					tot_mon=DateFrom.Length;
					InventoryClass obj2=new InventoryClass ();
					SqlDataReader rdr2=null;
					string	txtfirtar="";
					string	txtSectar="";
					string	txtThtar="";
					string	txtFoutar="";
					if(DropSSR.SelectedIndex==0) 
					{ 
						sw.Write("Servo_Sales_Representative"+"\t");
						for(int m=0;m<DateFrom.Length;m++)
						{ 
							if(m!=DateFrom.Length-1)
								sw.Write("\t"+GetMonthName(DateFrom[m].ToString())+"\t\t\t");
							else
								sw.WriteLine("\t"+GetMonthName(DateFrom[m].ToString())+"\t\t\t");
							
							//sw.WriteLine("Ist\tIInd\tIIIrd\tLast");
						}
						sw.Write("\t");
						for(int m=0;m<DateFrom.Length;m++)
						{ 
							if(m!=(DateFrom.Length)-1)
								sw.Write("Ist\tIInd\tIIIrd\tLast\t");
							else
								sw.WriteLine("Ist\tIInd\tIIIrd\tLast\t");
							
							//sw.WriteLine("Ist\tIInd\tIIIrd\tLast");
						}
						int SN=1;
						sql="select emp_name,emp_id from employee where designation='Servo Sales Representative'";
						rdr=obj.GetRecordSet(sql);
						while(rdr.Read())
						{
							sw.Write(rdr.GetValue(0).ToString().Trim()+"Target\t");
							/*for(int m=0;m<DateFrom.Length;m++)
							{
								if(m!=(DateFrom.Length)-1)
									sw.Write("\t\t\t\t");
								else
									sw.WriteLine("\t\t\t\t");
							}*/
							
							
							for(int m=0;m<DateFrom.Length;m++)
							{
								txtfirtar=Request.Params.Get("txtFirTar"+SN+++"");
								txtSectar=Request.Params.Get("txtSecTar"+SN+++"");
								txtThtar=Request.Params.Get("txtThTar"+SN+++"");
								txtFoutar=Request.Params.Get("txtFouTar"+SN+++"");
								if(m!=(DateFrom.Length)-1)
									sw.Write(txtfirtar+"\t"+txtSectar+"\t"+txtThtar+"\t"+txtFoutar+"\t");
								else
									sw.WriteLine(txtfirtar+"\t"+txtSectar+"\t"+txtThtar+"\t"+txtFoutar+"\t");
							}




							sw.Write(rdr.GetValue(0).ToString().Trim()+"Achivment\t");
							ssr_id=rdr.GetValue(1).ToString().Trim();
							if(frmYear==toYear)
							{
								for(int p=frmMonth;p<=toMonth;p++)
								{
									//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
									sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
									rdr2=obj2.GetRecordSet(sql2);
									while(rdr2.Read())
									{
										if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										{
											sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
											Tot_Achiv_I+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
										}
										else
											sw.Write("0\t");
									}
									rdr2.Close();
									//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
									sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
									rdr2=obj2.GetRecordSet(sql2);
									while(rdr2.Read())
									{
										if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										{
											sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
											Tot_Achiv_II+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
										}
										else
										{
											sw.Write("0\t");
										}
									}
									rdr2.Close();
									//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
									sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
									rdr2=obj2.GetRecordSet(sql2);
									while(rdr2.Read())
									{
										if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										{
											sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
											Tot_Achiv_III+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
										}
										else
										{
											sw.Write("0\t");
										}
									}
									rdr2.Close();
									int day=DateTime.DaysInMonth(frmYear,p);
									//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
									sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
									rdr2=obj2.GetRecordSet(sql2);
									while(rdr2.Read())
									{
										if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										{
											Tot_Achiv_IV+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
											if(p!=toMonth)
												sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
											else
												sw.WriteLine(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
										}
										else
										{
											if(p!=toMonth)
												sw.Write("0\t");
											else
												sw.WriteLine("0\t");

										}
									}
									rdr2.Close();
								}
							}
							else
							{
								for(int p=frmMonth;p<=12;p++)
								{
									//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
									sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
									rdr2=obj2.GetRecordSet(sql2);
									while(rdr2.Read())
									{
										if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										{
											sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
											Tot_Achiv_I+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
										}
										else
										{
											sw.Write("0\t");
										}
									}
									rdr2.Close();
									//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
									sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
									rdr2=obj2.GetRecordSet(sql2);
									while(rdr2.Read())
									{
										if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										{
											sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
											Tot_Achiv_II+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
										}
										else
										{
											sw.Write("0\t");
										}
									}
									rdr2.Close();
									//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
									sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
									rdr2=obj2.GetRecordSet(sql2);
									while(rdr2.Read())
									{
										if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										{
											sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
											Tot_Achiv_III+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
										}
										else
										{
											sw.Write("0\t");
										}
									}
									rdr2.Close();
									int day=DateTime.DaysInMonth(frmYear,p);
									//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
									sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
									rdr2=obj2.GetRecordSet(sql2);
									while(rdr2.Read())
									{
										if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										{
											sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
											Tot_Achiv_IV+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
										}
										else
										{
											sw.Write("0\t");
										}
									}	
									rdr2.Close();
																															   
								}
								for(int p=1;p<=toMonth;p++)
								{
									//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/1/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/7/"+toYear.ToString()+"'";
									sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/1/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/7/"+toYear.ToString()+"'";
									rdr2=obj2.GetRecordSet(sql2);
									while(rdr2.Read())
									{
										if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										{
											sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
											Tot_Achiv_I+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
										}
										else
										{
											sw.Write("0\t");
										}
									}	
									rdr2.Close();
									//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/8/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/14/"+toYear.ToString()+"'";
									sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/8/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/14/"+toYear.ToString()+"'";
									rdr2=obj2.GetRecordSet(sql2);
									while(rdr2.Read())
									{
										if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										{
											sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
											Tot_Achiv_II+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
										}
										else
										{
											sw.Write("0\t");
										}
									}
									rdr2.Close();
									//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/15/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/21/"+toYear.ToString()+"'";
									sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/15/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/21/"+toYear.ToString()+"'";
									rdr2=obj2.GetRecordSet(sql2);
									while(rdr2.Read())
									{
										if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										{
											sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
											Tot_Achiv_III+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
										}
										else
										{
											sw.Write("0\t");
										}
									}
									rdr2.Close();
									int day=DateTime.DaysInMonth(toYear,p);
									//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+ssr_id.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/22/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/"+day.ToString()+"/"+toYear.ToString()+"'";
									sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+ssr_id.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/22/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/"+day.ToString()+"/"+toYear.ToString()+"'";
									rdr2=obj2.GetRecordSet(sql2);
									while(rdr2.Read())
									{
										if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										{
											Tot_Achiv_IV+=Math.Round(double.Parse(rdr2.GetValue(0).ToString()));
											if(p!=toMonth)
												sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
											else
												sw.WriteLine(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
										}
										else
										{
											if(p!=toMonth)
												sw.Write("0\t");
											else
												sw.WriteLine("0\t");
										}
									}
									rdr2.Close();
								}
							}
							tot_emp++;
						}
						rdr.Close();
					}
					else
					{
						string SSR=DropSSR.SelectedValue;
						SSR=SSR.Substring(0,SSR.IndexOf(":"));
						
						sw.Write("Servo_Sales_Representative"+"\t");
						for(int m=0;m<DateFrom.Length;m++)
						{ 
							if(m!=DateFrom.Length-1)
								sw.Write("\t"+GetMonthName(DateFrom[m].ToString())+"\t\t\t");
							else
								sw.WriteLine("\t"+GetMonthName(DateFrom[m].ToString())+"\t\t\t");
							
						}
						sw.Write("\t");
						for(int m=0;m<DateFrom.Length;m++)
						{ 
							if(m!=(DateFrom.Length)-1)
								sw.Write("Ist\tIInd\tIIIrd\tLast\t");
							else
								sw.WriteLine("Ist\tIInd\tIIIrd\tLast\t");
							
						}
								
						
						sw.Write("Target\t");
						/*for(int m=0;m<DateFrom.Length;m++)
						{
							if(m!=(DateFrom.Length)-1)
								sw.Write("\t\t\t\t");
								//txtfirtar+"\t"+txtSectar+"\t"+txtThtar+"\t"+txtFoutar+"\t"
							else
								sw.WriteLine("\t\t\t\t");
						}*/

						for(int m=0;m<DateFrom.Length;m++)
						{
							if(m!=(DateFrom.Length)-1)
								sw.Write(txtfirtar+"\t"+txtSectar+"\t"+txtThtar+"\t"+txtFoutar+"\t");
							else
								sw.WriteLine(txtfirtar+"\t"+txtSectar+"\t"+txtThtar+"\t"+txtFoutar+"\t");
						}

						sw.Write("Achivment\t");
						if(frmYear==toYear)
						{
							for(int p=frmMonth;p<=toMonth;p++)
							{
								//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
								sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
								rdr2=obj2.GetRecordSet(sql2);
								while(rdr2.Read())
								{
									if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
										sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
									else
										sw.Write("0\t");
								}
								rdr2.Close();
								//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
								sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
								rdr2=obj2.GetRecordSet(sql2);
								while(rdr2.Read())
								{
									if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
									{
										sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
									}
									else
									{
										sw.Write("0\t");
									}
								}
								rdr2.Close();
								//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
								sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/14/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
								rdr2=obj2.GetRecordSet(sql2);
								while(rdr2.Read())
								{
									if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
									{
										sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
									}
									else
									{
										sw.Write("0\t");
									}
								}
								rdr2.Close();
								int day=DateTime.DaysInMonth(frmYear,p);
								//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
								sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
								rdr2=obj2.GetRecordSet(sql2);
								while(rdr2.Read())
								{
									if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
									{
										if(p!=toMonth)
											sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
										else
											sw.WriteLine(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
									}
									else
									{
										if(p!=toMonth)
											sw.Write("0\t");
										else
											sw.WriteLine("0\t");

									}
								}
								rdr2.Close();
							}
						}
						else
						{
							for(int p=frmMonth;p<=12;p++)
							{
								//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
								sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/1/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/7/"+frmYear.ToString()+"'";
								rdr2=obj2.GetRecordSet(sql2);
								while(rdr2.Read())
								{
									if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
									{
										sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
									}
									else
									{
										sw.Write("0\t");
									}
								}
								rdr2.Close();
								//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
								sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/8/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/14/"+frmYear.ToString()+"'";
								rdr2=obj2.GetRecordSet(sql2);
								while(rdr2.Read())
								{
									if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
									{
										sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
									}
									else
									{
										sw.Write("0\t");
									}
								}
								rdr2.Close();
								//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
								sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/15/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/21/"+frmYear.ToString()+"'";
								rdr2=obj2.GetRecordSet(sql2);
								while(rdr2.Read())
								{
									if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
									{
										sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
									}
									else
									{
										sw.Write("0\t");
									}
								}
								rdr2.Close();
								int day=DateTime.DaysInMonth(frmYear,p);
								//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and entrydate<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
								sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/22/"+frmYear.ToString()+"' and invoice_date<='"+p.ToString()+"/"+day.ToString()+"/"+frmYear.ToString()+"'";
								rdr2=obj2.GetRecordSet(sql2);
								while(rdr2.Read())
								{
									if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
									{
										sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
									}
									else
									{
										sw.Write("0\t");
									}
								}	
								rdr2.Close();
																															   
							}
							for(int p=1;p<=toMonth;p++)
							{
								//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/1/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/7/"+toYear.ToString()+"'";
								sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/1/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/7/"+toYear.ToString()+"'";
								rdr2=obj2.GetRecordSet(sql2);
								while(rdr2.Read())
								{
									if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
									{
										sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
									}
									else
									{
										sw.Write("0\t");
									}
								}	
								rdr2.Close();
								//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/8/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/14/"+toYear.ToString()+"'";
								sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/8/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/14/"+toYear.ToString()+"'";
								rdr2=obj2.GetRecordSet(sql2);
								while(rdr2.Read())
								{
									if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
									{
										sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
									}
									else
									{
										sw.Write("0\t");
									}
								}
								rdr2.Close();
								//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/15/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/21/"+toYear.ToString()+"'";
								sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/15/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/21/"+toYear.ToString()+"'";
								rdr2=obj2.GetRecordSet(sql2);
								while(rdr2.Read())
								{
									if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
									{
										sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
									}
									else
									{
										sw.Write("0\t");
									}
								}
								rdr2.Close();
								int day=DateTime.DaysInMonth(toYear,p);
								//sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+SSR.ToString().Trim()+"' and entrydate>='"+p.ToString()+"/22/"+toYear.ToString()+"' and entrydate<='"+p.ToString()+"/"+day.ToString()+"/"+toYear.ToString()+"'";
								sql2="select sum(qty*total_qty) from sales_details sd,sales_master sm, customer c, products p where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and p.prod_id=sd.prod_id and ssr='"+SSR.ToString().Trim()+"' and invoice_date>='"+p.ToString()+"/22/"+toYear.ToString()+"' and invoice_date<='"+p.ToString()+"/"+day.ToString()+"/"+toYear.ToString()+"'";
								rdr2=obj2.GetRecordSet(sql2);
								while(rdr2.Read())
								{
									if(rdr2.GetValue(0)!=null && rdr2.GetValue(0).ToString()!="")
									{
										if(p!=toMonth)
											sw.Write(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
										else
											sw.WriteLine(Math.Round(double.Parse(rdr2.GetValue(0).ToString()))+"\t");
									}
									else
									{
										if(p!=toMonth)
											sw.Write("0\t");
										else
											sw.WriteLine("0\t");
									}
								}
								rdr2.Close();
																																																								  
							}
						}
																																																													  
					}
									
					Tot_Tar=Tar;
					Tot_Achiv=Achiv;
					Tot_check=check;
																																																													  
				}
				sw.Close();
			}
			catch(Exception ex)
			{
				//MessageBox.Show(ex.Message.ToString());
			}


		}
		protected void btnView_Click(object sender, System.EventArgs e)
		{
			View=1;
			string s1="";
			string s2="";
			s1=txttodate.Text;
			s2=txtfromdate.Text;
			
			string[] ds1 = s2.Split(new char[] {'/'},s2.Length);
			string[] ds2 = s1.Split(new char[] {'/'},s1.Length);

			ds10=System.Convert.ToInt32(ds1[0]);

			ds20=System.Convert.ToInt32(ds2[0]);

			ds11=System.Convert.ToInt32(ds1[1]);

			ds12=System.Convert.ToInt32(ds1[2]);

			ds21=System.Convert.ToInt32(ds2[1]);

			ds22=System.Convert.ToInt32(ds2[2]);

			if(ds12==ds22 && ds11 > ds21)
			{
				MessageBox.Show("Please Select Greater Month in DateTo");
				View=0;
				return;
			}
			if(ds10 >ds20 && ds12==ds22 && ds11 == ds21 )
			{
				MessageBox.Show("Please Select Greater Date");
				View=0;
				return;
			}
			if((ds22-ds12) > 1)
			{
				MessageBox.Show("Please Select date between one year");
				View=0;
				return;
			}
			if((ds22-ds12) == -1 || ((ds22-ds12) >= 1 && ds21 >=ds11))
			{
				MessageBox.Show("Please Select date between one year");
				View=0;
				return;
			}
			getDate(ds10,ds11,ds12,ds20,ds21,ds22);
		}

		public string GetMonthName(string mon)
		{
			if(mon.IndexOf("/")>0)
			{
				string[] month=mon.Split(new char[] {'/'},mon.Length);
				if(month[0].ToString()=="1")
					return "January "+month[2].ToString();
				else if(month[0].ToString()=="2")
					return "February "+month[2].ToString();
				else if(month[0].ToString()=="3")
					return "March "+month[2].ToString();
				else if(month[0].ToString()=="4")
					return "April "+month[2].ToString();
				else if(month[0].ToString()=="5")
					return "May "+month[2].ToString();
				else if(month[0].ToString()=="6")
					return "June "+month[2].ToString();
				else if(month[0].ToString()=="7")
					return "July "+month[2].ToString();
				else if(month[0].ToString()=="8")
					return "August "+month[2].ToString();
				else if(month[0].ToString()=="9")
					return "September "+month[2].ToString();
				else if(month[0].ToString()=="10")
					return "October "+month[2].ToString();
				else if(month[0].ToString()=="11")
					return "November "+month[2].ToString();
				else if(month[0].ToString()=="12")
					return "December "+month[2].ToString();
			}
			return "";
		}
		public void Show_Record(string id)
		{
			/*int frmDay=0,frmMonth=0,frmYear=0,toDay=0,toMonth=0,toYear=0;
			string todate="";
			string fromdate="";
			
			todate=txttodate.Text;
			fromdate=txtfromdate.Text;

			string[] Todt = todate.Split(new char[] {'/'},todate.Length);
			string[] Frmdt = fromdate.Split(new char[] {'/'},fromdate.Length);

			frmDay=System.Convert.ToInt32(Frmdt[0]);
			frmMonth=System.Convert.ToInt32(Frmdt[1]);
			frmYear=System.Convert.ToInt32(Frmdt[2]);
			toDay=System.Convert.ToInt32(Todt[0]);
			toMonth=System.Convert.ToInt32(Todt[1]);
			toYear=System.Convert.ToInt32(Todt[2]);
			
			if(frmYear==toYear)
			{
				for(int a=frmMonth;a<=12;a++)
				{
					int i=0;
					int k=1,m=7;
					while(i<=3)
					{
						New_fromdate[i]=frmMonth+"/"+k+"/"+frmYear;
						fromdate=frmMonth+"/"+k+"/"+frmYear;
						if(i==3)
						{
							int day=DateTime.DaysInMonth(frmYear,frmMonth);
							New_todate[i]=frmMonth+"/"+day.ToString()+"/"+frmYear;
							todate=frmMonth+"/"+day.ToString()+"/"+frmYear;
						}
						else
						{
							New_todate[i]=frmMonth+"/"+m+"/"+frmYear;
							todate=frmMonth+"/"+m+"/"+frmYear;
						}
						i++;
						k+=7;
						m+=7;
					}
				}
			
			}

			InventoryClass obj2=new InventoryClass ();
			InventoryClass obj3=new InventoryClass ();
			InventoryClass obj4=new InventoryClass ();
			InventoryClass obj5=new InventoryClass ();
			string sql2="",sql3="",sql4="",sql5="";
			SqlDataReader rdr2=null,rdr3=null,rdr4=null,rdr5=null;
			sql2="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+id.ToString().Trim()+"' and entrydate>='"+New_fromdate[0].ToString()+"' and entrydate<='"+New_todate[0].ToString().Trim()+"'";
			sql3="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+id.ToString().Trim()+"' and entrydate>='"+New_fromdate[1].ToString()+"' and entrydate<='"+New_todate[1].ToString().Trim()+"'";
			sql4="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+id.ToString().Trim()+"' and entrydate>='"+New_fromdate[2].ToString()+"' and entrydate<='"+New_todate[2].ToString().Trim()+"'";
			sql5="select sum(balance) from customerledgertable clt,customer c where cust_id=custid and ssr='"+id.ToString().Trim()+"' and entrydate>='"+New_fromdate[3].ToString()+"' and entrydate<='"+New_todate[3].ToString().Trim()+"'";
			rdr2=obj2.GetRecordSet(sql2);
			rdr3=obj3.GetRecordSet(sql3);
			rdr4=obj4.GetRecordSet(sql4);
			rdr5=obj5.GetRecordSet(sql5);
			if(rdr2.Read())
			{
				Achivment[0]=Convert.ToInt32(rdr2.GetValue(0));
			}
			rdr2.Close();
			if(rdr3.Read())
			{
				Achivment[1]=Convert.ToInt32(rdr3.GetValue(0));
			}
			rdr3.Close();
			if(rdr4.Read())
			{
				Achivment[2]=Convert.ToInt32(rdr4.GetValue(0));
			}
			rdr4.Close();
			if(rdr5.Read())
			{
				Achivment[3]=Convert.ToInt32(rdr5.GetValue(0));
			}
			rdr5.Close();
			
			*/
		}
		public void getDate(int From1,int From2,int From3,int To1,int To2,int To3)
		{
			if(From2<=To2)
			{
				count=To2-From2;
				DateFrom = new string[count+1];
				DateTo = new string[count+1];
				TotalSum = new double[(count+1)*2];
			}
			else
			{
				count=13-From2;
				count+=To2;
				DateFrom = new string[count];
				DateTo = new string[count];
				TotalSum = new double[count*2];
			}
			int c=0;
			if(From2<=To2)
			{
				for(int i=From2,j=0;i<=To2;i++,j++)
				{
					if(c==0)
					{
						DateFrom[j]=i.ToString()+"/"+From1.ToString()+"/"+From3.ToString();
						c=1;
					}
					else
						DateFrom[j]=i.ToString()+"/"+"1"+"/"+From3.ToString();
					if(i==To2)
					{
						DateTo[j]=i.ToString()+"/"+To1.ToString()+"/"+To3.ToString();
						c=2;
					}
					else
					{
						int day=DateTime.DaysInMonth(From3,i);
						DateTo[j]=i.ToString()+"/"+day.ToString()+"/"+To3.ToString();
					}
				}
			}
			else
			{
				for(int i=From2,j=0;i<=12;i++,j++)
				{
					if(c==0)
						DateFrom[j]=i.ToString()+"/"+From1.ToString()+"/"+From3.ToString();
					else
						DateFrom[j]=i.ToString()+"/"+"1"+"/"+From3.ToString();
					int day=DateTime.DaysInMonth(From3,i);
					DateTo[j]=i.ToString()+"/"+day.ToString()+"/"+From3.ToString();
					c++;
				}
				for(int i=1,j=c;i<=To2;i++,j++)
				{
					DateFrom[j]=i.ToString()+"/"+"1"+"/"+To3.ToString();
					if(i==To2)
						DateTo[j]=i.ToString()+"/"+To1.ToString()+"/"+To3.ToString();
					else
					{
						int day=DateTime.DaysInMonth(To3,i);
						DateTo[j]=i.ToString()+"/"+day.ToString()+"/"+To3.ToString();
					}
				}
			}
		}
	}
}
