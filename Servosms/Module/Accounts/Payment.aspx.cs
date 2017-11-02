/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
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
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using RMG;
using System.IO;
using System.Text;
using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace Servosms.Module.Accounts
{
    /// <summary>
    /// Summary description for Payment.
    /// </summary>
    public partial class Payment : System.Web.UI.Page
    {
        DBOperations.DBUtil dbobj = new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        string uid;
        static string strCash = "";
        static ArrayList LedgerID = new ArrayList();
        static bool PrintFlag = false;
        static string Invoice_Date = "", Acc_Date = "";
        string BaseUri = "http://localhost:64862";

        /// <summary>
        /// Put user code to initialize the page here
        /// This method is used for setting the Session variable for userId and 
        /// after that filling the required dropdowns with database values 
        /// and also check accessing priviledges for particular user
        /// and generate the next ID also.
        /// </summary>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                uid = (Session["User_Name"].ToString());
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Payment,Method:PageLoad   " + " EXCEPTION " + ex.Message + " userid " + uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                checkPrivileges();
                pnlBankInfo.Visible = false;
                txtNarrartion.Value = "";
                fillCombo();
                panLedgerName1.Visible = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                txtDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                txtchkDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                LedgerID = new ArrayList();
                Invoice_Date = "";
                Acc_Date = "";
                string str = "";
                InventoryClass obj = new InventoryClass();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/payment/page_load").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        str = JsonConvert.DeserializeObject<string>(id);
                    }
                }
                Acc_Date = str;
                //SqlDataReader rdr = obj.GetRecordSet("select Acc_Date_from from Organisation");
                //if (rdr.Read())
                //{
                //    Acc_Date = GenUtil.trimDate(rdr["Acc_Date_from"].ToString());
                //}
                //rdr.Close();
            }
            txtDate.Text = Request.Form["txtDate"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDate"].ToString().Trim();
            txtchkDate.Text = Request.Form["txtchkDate"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtchkDate"].ToString().Trim();
        }

        /// <summary>
        /// Method to check the user previleges
        /// </summary>
        public void checkPrivileges()
        {
            #region Check Privileges
            int i;
            string View_flag = "0", Add_Flag = "0", Edit_Flag = "0", Del_Flag = "0";
            string Module = "1";
            string SubModule = "2";
            string[,] Priv = (string[,])Session["Privileges"];
            for (i = 0; i < Priv.GetLength(0); i++)
            {
                if (Priv[i, 0] == Module && Priv[i, 1] == SubModule)
                {
                    View_flag = Priv[i, 2];
                    Add_Flag = Priv[i, 3];
                    Edit_Flag = Priv[i, 4];
                    Del_Flag = Priv[i, 5];
                    break;
                }
            }
            if (View_flag == "0")
            {
                string msg = "UnAthourized Visit to Cash Payment Page";
                CreateLogFiles.ErrorLog("Form:Payment,Method:PageLoad " + msg + "  " + uid);
                Response.Redirect("../../Sysitem/AccessDeny.aspx", false);
                return;
            }
            if (Add_Flag == "0")
                btnSave.Enabled = false;
            if (Edit_Flag == "0")
                btnEdit.Enabled = false;
            if (Del_Flag == "0")
                btnDelete.Enabled = false;
            #endregion
        }

        /// <summary>
        /// Method to fill the combos of Ledger Name and  By.
        /// Ledger Name Combo contains all ledger Names except the type Bank & cash
        /// By combo contains only Ledger names of the type bank and cash.
        /// </summary>
        public void fillCombo()
        {
            try
            {
                //SqlDataReader SqlDtr = null;
                dbobj.Dispose();
                string str="";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/payment/fillCombo").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        str = JsonConvert.DeserializeObject<string>(id);
                    }
                }
                texthiddenprod.Value = str;
                //dbobj.SelectQuery("Select Ledger_Name,Ledger_ID from Ledger_Master lm,Ledger_master_sub_grp lmsg  where  lm.sub_grp_id = lmsg.sub_grp_id and lmsg.sub_grp_name not like 'Bank%' and lmsg.sub_grp_name <> 'Cash in hand' and lmsg.sub_grp_name <> 'Discount' Order by Ledger_Name", ref SqlDtr);
                //if (SqlDtr.HasRows)
                //{
                //    texthiddenprod.Value = "Select,";
                //    while (SqlDtr.Read())
                //    {
                //        texthiddenprod.Value += SqlDtr["Ledger_Name"].ToString() + ";" + SqlDtr["Ledger_ID"].ToString() + ",";
                //    }
                //}
                //SqlDtr.Close();

                dbobj.Dispose();
                PaymentModels payment = new PaymentModels();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/payment/fillCombo2").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        payment = JsonConvert.DeserializeObject<PaymentModels>(id);
                    }
                }
                foreach (var item in payment.DropBy1)
                {
                    DropBy.Items.Add(item);
                }                
                strCash = payment.strCash;
                //dbobj.SelectQuery("Select Ledger_Name,sub_grp_name from Ledger_Master lm,Ledger_master_sub_grp lmsg  where  lm.sub_grp_id = lmsg.sub_grp_id  and (sub_grp_name='Cash in hand' or sub_grp_name like'Bank%')  Order by Ledger_Name", ref SqlDtr);
                //    while (SqlDtr.Read())
                //    {
                //        str = SqlDtr["sub_grp_name"].ToString();
                //        if (str.Equals("Cash in hand") || str.IndexOf("Bank") > -1)
                //        {
                //            DropBy.Items.Add(SqlDtr["Ledger_Name"].ToString());
                //            if (str.Equals("Cash in hand"))
                //                strCash = SqlDtr["Ledger_Name"].ToString();
                //        }
                //    }
                //SqlDtr.Close();

            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Payment,Method:fillCombo() Exception: " + ex.Message + "  User: " + uid);
            }
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

        /// <summary>
        /// This method is used to insert the record
        /// </summary>
        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                var dt = System.Convert.ToDateTime(Acc_Date);
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString()));
                if (DateTime.Compare(dt, dt2) > 0)
                {
                    MessageBox.Show("Please Select Date Must be Greater than Opening Date");
                }

                else
                {
                    Save();
                    PrintFlag = true;
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Payment,Method:btnSave_click Exception: " + ex.Message + "  User: " + uid);
            }
        }

        /// <summary>
        /// This method is used to insert the record
        /// </summary>
        public void Save()
        {
            string Ledger_Name = "";
            string By_Name = "";
            string Bank_name = "";
            string Cheque_No = "";
            string chkDate = "";
            string Amount = "";
            string narration = "";
            string Ledger_ID = "";
            string By_ID = "";
            string Vouch_ID = "";
            DateTime Entry_Date = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString()) + " " + DateTime.Now.TimeOfDay.ToString());
            Ledger_Name = DropLedgerName.Value.Trim();
            By_Name = DropBy.SelectedItem.Text.Trim();
            Bank_name = txtBankname.Text.Trim();
            Cheque_No = txtCheque.Text.Trim();
            chkDate = txtchkDate.Text.Trim();
            chkDate = GenUtil.str2DDMMYYYY(chkDate);
            DateTime dtDate = System.Convert.ToDateTime(chkDate);
            Amount = txtAmount.Text.Trim();
            narration = txtNarrartion.Value.Trim();

            //SqlDataReader SqlDtr = null;
            string strNew = DropLedgerName.Value;
            string[] arrstrNew = strNew.Split(new char[] { ';' }, strNew.Length);
            if (strNew == "Select")
                MessageBox.Show("Please Select a Ledger");
            else
            {
                Ledger_ID = arrstrNew[1].ToString();

                PaymentModels payment = new PaymentModels();
                payment.Entry_Date = Entry_Date.ToString();
                payment.Ledger_Name = Ledger_Name;
                payment.By_Name = By_Name;
                payment.Bank_name = Bank_name;
                payment.Cheque_No = Cheque_No;
                payment.dtDate = dtDate.ToString();
                payment.Amount = Amount;
                payment.narration = narration;
                payment.Ledger_ID = Ledger_ID;
                payment.uid = uid;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(payment);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/payment/SavePayment", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        payment = Newtonsoft.Json.JsonConvert.DeserializeObject<PaymentModels>(responseString);
                    }
                }
                //payment.c=

                //dbobj.SelectQuery("Select Ledger_ID from Ledger_Master where Ledger_Name ='" + By_Name + "'", ref SqlDtr);
                //if (SqlDtr.Read())
                //{
                //    By_ID = SqlDtr["Ledger_ID"].ToString();
                //}
                //SqlDtr.Close();

                //dbobj.SelectQuery("Select top 1 (voucher_ID+1)  from Payment_Transaction order by voucher_ID desc", ref SqlDtr);
                //if (SqlDtr.Read())
                //{
                //    Vouch_ID = SqlDtr.GetValue(0).ToString();
                //}
                //else
                //{
                //    Vouch_ID = "50001";
                //}
                //SqlDtr.Close();

                //int c = 0;

                //dbobj.Insert_or_Update("insert into payment_transaction values(" + Vouch_ID + ",'Payment'," + Ledger_ID + "," + Amount + "," + By_ID + "," + Amount + ",'" + Bank_name + "','" + Cheque_No + "',CONVERT(datetime,'" + dtDate + "', 103),'" + narration + "','" + uid + "', CONVERT(datetime,'" + Entry_Date + "', 103))", ref c);
                //object obj = null;
                //dbobj.ExecProc(DBOperations.OprType.Insert, "ProInsertAccountsLedger", ref obj, "@Ledger_ID", Ledger_ID, "@Particulars", "Payment (" + Vouch_ID + ")", "@Debit_Amount", Amount, "@Credit_Amount", "0.0", "@type", "Dr", "@Invoice_Date", Entry_Date);
                //dbobj.ExecProc(DBOperations.OprType.Insert, "ProInsertAccountsLedger", ref obj, "@Ledger_ID", By_ID, "@Particulars", "Payment (" + Vouch_ID + ")", "@Debit_Amount", "0.0", "@Credit_Amount", Amount, "@type", "Cr", "@Invoice_Date", Entry_Date);
                //dbobj.ExecProc(DBOperations.OprType.Insert, "ProCustomerLedgerEntry", ref obj, "@Voucher_ID", Vouch_ID, "@Ledger_ID", Ledger_ID, "@Amount", Amount, "@Type", "Dr.", "@Invoice_Date", Entry_Date);
                //dbobj.ExecProc(DBOperations.OprType.Insert, "ProCustomerLedgerEntry", ref obj, "@Voucher_ID", Vouch_ID, "@Ledger_ID", By_ID, "@Amount", Amount, "@Type", "Cr.", "@Invoice_Date", Entry_Date);
                CustomerInsertUpdate(Ledger_ID);
                if (payment.c != 0)
                {
                    MessageBox.Show("Payment Saved");
                    CreateLogFiles.ErrorLog("Form:Payment,Method:btnSave_click Payment of Ledger name " + Ledger_Name + " with voucher_id " + Vouch_ID + " Saved  User: " + uid);
                    makingReport();
                    clear();
                }
                checkPrivileges();
                btnPrint.CausesValidation = false;
            }
        }

        /// <summary>
        /// this function use to making report
        /// </summary>
        public void makingReport()
        {
            try
            {
                /*********************************************
				 * This Code is comment by Mahesh on 04.04.008
				 * this printing is used to plan paper.
				 * *******************************************/

                string home_drive = Environment.SystemDirectory;
                home_drive = home_drive.Substring(0, 2);
                string path = home_drive + @"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\Payment.txt";
                StreamWriter sw = new StreamWriter(path);
                string info = " {0,-14:S} {1,-50:S} {2,20:S} {3,-46:S}";//Party Name & Address
                string info1 = " {0,-22:S} {1,-22:S} {2,-15:S} {3,-50:S} {4,20:S}";
                string info2 = " {0,16:S} {1,-114:S}";
                //string info3=" {0,7:S} {1,-100:S}";
                string info3 = " {0,-14:S} {1,-50:S} {2,20:S} {3,46:S}";//Party Name & Address
                                                                        // Condensed
                sw.Write((char)27);
                sw.Write((char)67);
                sw.Write((char)0);
                sw.Write((char)12);

                sw.Write((char)27);
                sw.Write((char)78);
                sw.Write((char)5);

                sw.Write((char)27);
                sw.Write((char)15);

                sw.WriteLine("                                                         PAYMENT");
                for (int i = 0; i < 8; i++)
                {
                    sw.WriteLine("");
                }
                string addr = "", city = "";
                string name = DropLedgerName.Value;
                if (name.IndexOf(";") > 0)
                {
                    string[] arrLName = name.Split(new char[] { ';' }, name.Length);
                   // SqlDataReader rdr = null;
                    string str;
                    str = arrLName[1];
                    PaymentModels payment = new PaymentModels();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/payment/makingReport?str="+str ).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            payment = JsonConvert.DeserializeObject<PaymentModels>(id);
                        }
                    }
                    addr = payment.addr;
                    city = payment.city;
                    //string str = "select address,city from customer,ledger_master where ledger_name=cust_name and ledger_id='" + arrLName[1] + "'";
                    //dbobj.SelectQuery(str, ref rdr);
                    //if (rdr.Read())
                    //{
                    //    addr = rdr.GetValue(0).ToString();
                    //    city = rdr.GetValue(1).ToString();
                    //}
                    //rdr.Close();
                }
                else
                {

                }
                sw.WriteLine(info, "", DropLedgerName.Value.ToUpper(), "", "");
                sw.WriteLine(info, "", addr, "", txtDate.Text);
                sw.WriteLine(info, "", city, "", "---");
                sw.WriteLine(info, "", "", "", "---");
                sw.WriteLine(info1, "Mode Of Payment", "Cheque / DDNo", "Date", "Bank", "Amount  ");
                sw.WriteLine();
                sw.WriteLine();
                if (DropBy.SelectedItem.Text == "Cash")
                    sw.WriteLine(info1, DropBy.SelectedItem.Text, txtCheque.Text, txtDate.Text, txtBankname.Text, GenUtil.strNumericFormat(txtAmount.Text));
                else
                    sw.WriteLine(info1, DropBy.SelectedItem.Text, txtCheque.Text, txtchkDate.Text, txtBankname.Text, GenUtil.strNumericFormat(txtAmount.Text));
                for (int i = 0; i < 34; i++)
                {
                    sw.WriteLine("");
                }
                sw.Write((char)27);   //added by vishnu
                sw.Write((char)69);   //added by vishnu
                sw.WriteLine(info2, "", "Narration : " + txtNarrartion.Value);
                sw.Write((char)27);   //added by vishnu
                sw.Write((char)70);   //added by vishnu
                sw.WriteLine(info3, "", "", "", "Total Amount : " + GenUtil.strNumericFormat(txtAmount.Text));
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine(info2, "", GenUtil.ConvertNoToWord(txtAmount.Text));
                sw.WriteLine();
                sw.Close();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Payment,Method:makingReport() Exception: " + ex.Message + "  User: " + uid);
            }
        }

        /// <summary>
        /// This method are not used.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string checkStr(string str)
        {
            if (str.IndexOf("\r\n") > 0)
            {
                str = str.Replace("\r\n", " ");
            }
            return str;
        }

        /// <summary>
        /// contacst the Print_WiindowServices via socket and sends the Payment.txt file name to print.
        /// </summary>
        public void Print()
        {
            byte[] bytes = new byte[1024];
            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // The name of the
                // remote device is "host.contoso.com".
                IPHostEntry ipHostInfo = Dns.Resolve("127.0.0.1");
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 62000);

                // Create a TCP/IP  socket.
                Socket sender1 = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender1.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender1.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    string home_drive = Environment.SystemDirectory;
                    home_drive = home_drive.Substring(0, 2);
                    byte[] msg = Encoding.ASCII.GetBytes(home_drive + "\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\Payment.txt<EOF>");

                    // Send the data through the socket.
                    int bytesSent = sender1.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender1.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.
                    sender1.Shutdown(SocketShutdown.Both);
                    sender1.Close();
                    CreateLogFiles.ErrorLog("Form:Payment.aspx,Method:print. Report Printed   userid  " + uid);
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    CreateLogFiles.ErrorLog("Form:Payment.aspx,Method:print" + " EXCEPTION " + ane.Message + "  userid  " + uid);
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                    CreateLogFiles.ErrorLog("Form:Payment.aspx,Method:print" + " EXCEPTION " + se.Message + "  userid  " + uid);
                }
                catch (Exception es)
                {
                    Console.WriteLine("Unexpected exception : {0}", es.ToString());
                    CreateLogFiles.ErrorLog("Form:Payment.aspx,Method:print" + " EXCEPTION " + es.Message + "  userid  " + uid);
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Payment.aspx,Method:print  EXCEPTION " + ex.Message + "  userid  " + uid);
            }
        }

        /// <summary>
        /// clear the form
        /// </summary>
        public void clear()
        {
            DropBy.SelectedIndex = 0;
            DropLedgerName.Value = "Select";
            txtBankname.Text = "";
            txtCheque.Text = "";
            txtchkDate.Text = "";
            txtAmount.Text = "";
            txtNarrartion.Value = "";
            pnlBankInfo.Visible = false;
            txtDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            txtchkDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            CheckCashMode = "";
            tempPaymentID.Value = "";
            LedgerID = new ArrayList();
        }

        /// <summary>
        /// This method is used to fatch the Ledger Name with Ledger ID and fill into the dropdownlist.
        /// </summary>
        protected void btnEdit1_Click(object sender, System.EventArgs e)
        {
            try
            {
                clear();
                DropLedgerName1.Items.Clear();
                DropLedgerName1.Items.Add("Select");
                btnEdit1.Visible = false;
                panLedgerName1.Visible = true;
                btnSave.Enabled = false;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                //SqlDataReader SqlDtr = null;

                List<string> LedgerName = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/payment/FillLedgerName").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        LedgerName = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                }
                if (LedgerName != null)
                {
                    foreach (var Ledger in LedgerName)
                        DropLedgerName1.Items.Add(Ledger);
                }


                //dbobj.SelectQuery("select Ledger_Name+';'+cast(Ledger_ID_Dr as varchar)+':'+cast(voucher_id as varchar) from Payment_transaction pt, Ledger_Master lm where pt.Ledger_ID_Dr = lm.Ledger_ID  order by Voucher_id",ref SqlDtr);
                //while(SqlDtr.Read())
                //{
                //	DropLedgerName1.Items.Add(SqlDtr.GetValue(0).ToString());     
                //}
                //SqlDtr.Close();
                checkPrivileges();
                btnPrint.CausesValidation = true;
                PrintFlag = false;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Payment,Method:btnEdit1_Click Exception: " + ex.Message + "  User: " + uid);
            }
        }

        public static string CheckCashMode = "";
        /// <summary>
        /// This method is used to fatch the particular ledger information select from dropdownlist.
        /// </summary>
        protected void DropLedgerName1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                clear();
                if (DropLedgerName1.SelectedIndex == 0)
                {
                    MessageBox.Show("Please select Ledger Name");
                    return;
                }
                string str = DropLedgerName1.SelectedItem.Text;
                string[] strArr = str.Split(new char[] { ':' }, str.Length);

                PaymentModels payment = new PaymentModels();
                string VoucherId = strArr[1].Trim();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/payment/LedgerName_SelectedIndexChanged?VoucherId=" + VoucherId).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        payment = JsonConvert.DeserializeObject<PaymentModels>(id);
                    }

                }

                //SqlDataReader SqlDtr = null;
                //SqlDataReader SqlDtr1 = null;

                //dbobj.SelectQuery("Select * from payment_transaction where voucher_Id = "+strArr[1].Trim(),ref SqlDtr); 
                //while(SqlDtr.Read())
                //{
                tempPaymentID.Value = payment.tempPaymentID;
                DropLedgerName.Value = strArr[0];
                txtBankname.Text = payment.txtBankname;
                txtCheque.Text = payment.txtCheque;
                txtchkDate.Text = payment.txtchkDate;
                txtAmount.Text = payment.txtAmount;
                txtNarrartion.Value = payment.txtNarrartion;
                txtDate.Text = payment.txtDate;
                Invoice_Date = payment.Invoice_Date;
                LedgerID.Add(payment.LedgerID);
                LedgerID.Add(payment.LedgerID1);
                DropBy.SelectedIndex = DropBy.Items.IndexOf(DropBy.Items.FindByText(payment.CheckCashMode));
                CheckCashMode = payment.CheckCashMode;
                if (DropBy.SelectedItem.Text.Equals(strCash))
                    pnlBankInfo.Visible = false;
                else
                    pnlBankInfo.Visible = true;
                //}

                checkPrivileges();
                btnPrint.CausesValidation = true;
                PrintFlag = false;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Payment,Method:DropLedgerName1_SelectedIndexChanged Exception: " + ex.Message + "  User: " + uid);
            }
        }

        /// <summary>
        /// This method is used to given date is blank or not.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string checkDate(string str)
        {
            if (!str.Trim().Equals(""))
            {
                if (str.Trim().Equals("1/1/1900"))
                    str = "";
            }
            return str;
        }

        /// <summary>
        /// This method is used to seprate time from date and returns only date in mm/dd/yyyy
        /// </summary>
        public string trimDate(string strDate)
        {
            int pos = strDate.IndexOf(" ");
            if (pos != -1)
            {
                strDate = strDate.Substring(0, pos);
            }
            else
            {
                strDate = "";
            }
            return strDate;
        }

        /// <summary>
        /// This method is used to update the record in edit time.
        /// </summary>
        protected void btnEdit_Click(object sender, System.EventArgs e)
        {
            try
            {
                var dt = System.Convert.ToDateTime(Acc_Date);
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString()));
                if (DateTime.Compare(dt, dt2) > 0)
                {
                    MessageBox.Show("Please Select Date Must be Greater than Opening Date");
                }
                else
                {
                    Update();
                    PrintFlag = true;
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Payment,Method:btnEdit_Click Exception: " + ex.Message + "  User: " + uid);
            }
        }

        /// <summary>
        /// This method is used to update the record in edit time.
        /// </summary>
        public void Update()
        {
            if (DropLedgerName1.SelectedIndex == 0)
            {
                MessageBox.Show("Please select Ledger Name");
                return;
            }
            string Ledger_Name1 = "";
            string By_Name1 = "";
            string Bank_name1 = "";
            string Cheque_No1 = "";
            string Date1 = "";
            string Amount1 = "";
            string narration1 = "";
            string Ledger_ID1 = "", OldLedger_ID = "";
            string By_ID1 = "";
            DateTime Entry_Date = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString()) + " " + DateTime.Now.TimeOfDay.ToString());
            int c = 0;

            string strOld = DropLedgerName1.SelectedItem.Text;
            string strNew = DropLedgerName.Value;
            string[] arrstrOld = strOld.Split(new char[] { ':' }, strOld.Length);
            string[] arrOldLedger_ID = arrstrOld[0].Split(new char[] { ';' }, arrstrOld[0].Length);
            string[] arrstrNew = strNew.Split(new char[] { ';' }, strNew.Length);
            By_Name1 = DropBy.SelectedItem.Text.Trim();
            Bank_name1 = txtBankname.Text.Trim();
            Cheque_No1 = txtCheque.Text.Trim();
            Date1 = txtchkDate.Text.Trim();
            Date1 = GenUtil.str2DDMMYYYY(Date1);
            DateTime dtDate = System.Convert.ToDateTime(Date1);
            Amount1 = txtAmount.Text.Trim();
            narration1 = txtNarrartion.Value.Trim();
            //SqlDataReader SqlDtr = null;
            Ledger_ID1 = arrstrNew[1].ToString();
            OldLedger_ID = arrOldLedger_ID[1].ToString();
            Ledger_Name1 = arrstrNew[0].ToString().Trim();
            //PaymentModels payment = new PaymentModels();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/payment/SelectLedgerId?Ledger_Name1=" + Ledger_Name1).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var id = Res.Content.ReadAsStringAsync().Result;
                    string pay = JsonConvert.DeserializeObject<string>(id);
                    By_ID1 = pay;
                }
            }


            //dbobj.SelectQuery("Select Ledger_ID from Ledger_Master where Ledger_Name ='" + By_Name1 + "'", ref SqlDtr);
            //if (SqlDtr.Read())
            //   By_ID1 = SqlDtr["Ledger_ID"].ToString();
            //SqlDtr.Close();

            string str = DropLedgerName1.SelectedItem.Text;
            string[] strArr = str.Split(new char[] { ':' }, str.Length);

            //SqlDataReader rdr = null;
            string Cust_ID = "", OldCust_ID = "", Vouch_ID = "";
            Vouch_ID = tempPaymentID.Value;

            PaymentModels payment = new PaymentModels();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/payment/SelectCustId?Ledger_ID1=" + Ledger_ID1 + "&OldLedger_ID=" + OldLedger_ID).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var id = Res.Content.ReadAsStringAsync().Result;
                    payment = JsonConvert.DeserializeObject<PaymentModels>(id);
                    Cust_ID = payment.Ledger_ID1;
                    OldCust_ID = payment.OldLedger_ID;
                }
            }

            //dbobj.SelectQuery("select Cust_ID from Customer,Ledger_Master where Ledger_Name = Cust_Name and Ledger_ID = '" + Ledger_ID1 + "'", ref rdr);
            //if (rdr.Read())
            //    Cust_ID = rdr["Cust_ID"].ToString();
            //rdr.Close();
            //dbobj.SelectQuery("select Cust_ID from Customer,Ledger_Master where Ledger_Name = Cust_Name and Ledger_ID = '" + OldLedger_ID + "'", ref rdr);
            //if (rdr.Read())
            //    OldCust_ID = rdr["Cust_ID"].ToString();
            //rdr.Close();

            PaymentModels payment1 = new PaymentModels();
            if (arrstrOld[0].ToString().Equals(DropLedgerName.Value))
            {
                payment1.Ledger_ID1 = Ledger_ID1;
                payment1.Amount1 = Amount1;
                payment1.By_ID1 = By_ID1;
                payment1.Bank_name1 = Bank_name1;
                payment1.Cheque_No1 = Cheque_No1;
                payment1.dtDate = System.Convert.ToDateTime(dtDate).ToString();
                payment1.narration1 = narration1;
                payment1.uid = uid;
                payment1.Entry_Date = System.Convert.ToDateTime(Entry_Date).ToString();
                payment1.VoucherId = strArr[1].Trim();
                payment1.CheckCashMode = CheckCashMode;
                payment1.DropBy = DropBy.SelectedItem.Text;
                payment1.Vouch_ID = Vouch_ID;
                payment1.OldCust_ID = OldCust_ID;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(payment1);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/payment/UpdatePayment", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        payment1 = Newtonsoft.Json.JsonConvert.DeserializeObject<PaymentModels>(responseString);
                    }
                }

                //dbobj.Insert_or_Update("Update Payment_transaction set Ledger_ID_Dr = " + Ledger_ID1 + ",Amount1 = " + Amount1 + ",Ledger_ID_Cr = " + By_ID1 + ",Amount2 = " + Amount1 + ",Bank_Name='" + Bank_name1 + "',Cheque_No='" + Cheque_No1 + "',Cheque_date = CONVERT(datetime,'" + dtDate + "', 103),Narration ='" + narration1 + "',Entered_By = '" + uid + "', Entry_Date= CONVERT(datetime,'" + Entry_Date + "', 103) where Voucher_ID = " + strArr[1].Trim(), ref c);
                //object obj = null;
                //if (CheckCashMode.Equals(DropBy.SelectedItem.Text))
                //{
                //    dbobj.ExecProc(DBOperations.OprType.Update, "ProUpdateAccountsLedger", ref obj, "@Voucher_ID", strArr[1].Trim(), "@Ledger_ID", Ledger_ID1, "@Amount", Amount1, "@Type", "Dr", "@Invoice_Date", Entry_Date);
                //    dbobj.ExecProc(DBOperations.OprType.Update, "ProUpdateAccountsLedger", ref obj, "@Voucher_ID", strArr[1].Trim(), "@Ledger_ID", By_ID1, "@Amount", Amount1, "@Type", "Cr", "@Invoice_Date", Entry_Date);

                //}
                //else
                //{
                //    dbobj.Insert_or_Update("delete from CustomerLedgerTable where Particular = 'Voucher(" + strArr[1].Trim() + ")' and CustID='" + OldCust_ID + "'", ref c);
                //    dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Payment (" + strArr[1].Trim() + ")'", ref c);
                //    dbobj.ExecProc(DBOperations.OprType.Insert, "ProInsertAccountsLedger", ref obj, "@Ledger_ID", Ledger_ID1, "@Particulars", "Payment (" + Vouch_ID + ")", "@Debit_Amount", Amount1, "@Credit_Amount", "0.0", "@type", "Dr", "@Invoice_Date", Entry_Date);
                //    dbobj.ExecProc(DBOperations.OprType.Insert, "ProInsertAccountsLedger", ref obj, "@Ledger_ID", By_ID1, "@Particulars", "Payment (" + Vouch_ID + ")", "@Debit_Amount", "0.0", "@Credit_Amount", Amount1, "@type", "Cr", "@Invoice_Date", Entry_Date);
                //    dbobj.ExecProc(DBOperations.OprType.Insert, "ProCustomerLedgerEntry", ref obj, "@Voucher_ID", Vouch_ID, "@Ledger_ID", Ledger_ID1, "@Amount", Amount1, "@Type", "Dr.", "@Invoice_Date", Entry_Date);
                //    dbobj.ExecProc(DBOperations.OprType.Insert, "ProCustomerLedgerEntry", ref obj, "@Voucher_ID", Vouch_ID, "@Ledger_ID", By_ID1, "@Amount", Amount1, "@Type", "Cr.", "@Invoice_Date", Entry_Date);
                //}
            }
            else
            {
                payment1.Ledger_ID1 = Ledger_ID1;
                payment1.Amount1 = Amount1;
                payment1.By_ID1 = By_ID1;
                payment1.Bank_name1 = Bank_name1;
                payment1.Cheque_No1 = Cheque_No1;
                payment1.dtDate = System.Convert.ToDateTime(dtDate).ToString();
                payment1.narration1 = narration1;
                payment1.uid = uid;
                payment1.Entry_Date = System.Convert.ToDateTime(Entry_Date).ToString();
                payment1.VoucherId = strArr[1].Trim();
                payment1.CheckCashMode = CheckCashMode;
                payment1.DropBy = DropBy.SelectedItem.Text;
                payment1.Vouch_ID = Vouch_ID;
                payment1.OldCust_ID = OldCust_ID;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(payment1);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/payment/DeleteAndUpdatePayment", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        payment1 = Newtonsoft.Json.JsonConvert.DeserializeObject<PaymentModels>(responseString);
                    }
                }

                //dbobj.Insert_or_Update("delete from payment_transaction where voucher_id = " + strArr[1].Trim(), ref c);
                //dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Payment (" + strArr[1].Trim() + ")'", ref c);
                //if (OldCust_ID != "")
                //{
                //    dbobj.Insert_or_Update("delete from CustomerLedgerTable where Particular = 'Voucher(" + strArr[1].Trim() + ")' and CustID='" + OldCust_ID + "'", ref c);
                //}

                //dbobj.Insert_or_Update("insert into payment_transaction values(" + Vouch_ID + ",'Payment'," + Ledger_ID1 + "," + Amount1 + "," + By_ID1 + "," + Amount1 + ",'" + Bank_name1 + "','" + Cheque_No1 + "',CONVERT(datetime,'" + dtDate + "', 103),'" + narration1 + "','" + uid + "',CONVERT(datetime,'" + Entry_Date + "', 103))", ref c);
                //object obj = null;
                //dbobj.ExecProc(DBOperations.OprType.Insert, "ProInsertAccountsLedger", ref obj, "@Ledger_ID", Ledger_ID1, "@Particulars", "Payment (" + Vouch_ID + ")", "@Debit_Amount", Amount1, "@Credit_Amount", "0.0", "@type", "Dr", "@Invoice_Date", Entry_Date);
                //dbobj.ExecProc(DBOperations.OprType.Insert, "ProInsertAccountsLedger", ref obj, "@Ledger_ID", By_ID1, "@Particulars", "Payment (" + Vouch_ID + ")", "@Debit_Amount", "0.0", "@Credit_Amount", Amount1, "@type", "Cr", "@Invoice_Date", Entry_Date);
                //dbobj.ExecProc(DBOperations.OprType.Insert, "ProCustomerLedgerEntry", ref obj, "@Voucher_ID", Vouch_ID, "@Ledger_ID", Ledger_ID1, "@Amount", Amount1, "@Type", "Dr.", "@Invoice_Date", Entry_Date);
                //dbobj.ExecProc(DBOperations.OprType.Insert, "ProCustomerLedgerEntry", ref obj, "@Voucher_ID", Vouch_ID, "@Ledger_ID", By_ID1, "@Amount", Amount1, "@Type", "Cr.", "@Invoice_Date", Entry_Date);
                LedgerID.Add(payment1.Ledger_ID1);
            }
            if (payment1.c != 0)
            {
                CreateLogFiles.ErrorLog("Form:Payment,Method:btnEdit_Click Payment of Ledger name " + Ledger_Name1 + " with voucher_id " + strArr[1].Trim() + " Updated.  User : " + uid);
                makingReport();
                CustomerUpdate();
                MessageBox.Show("Payment Updated");
                panLedgerName1.Visible = false;
                btnEdit1.Visible = true;
                clear();
                btnSave.Enabled = true;
            }
            checkPrivileges();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnPrint.CausesValidation = false;
        }

        /// <summary>
        /// This method is used to delete the particular record who select the dropdownlist.
        /// </summary>
        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (DropLedgerName1.SelectedIndex == 0)
                {
                    MessageBox.Show("Please select Ledger Name");
                    return;
                }
                string str = DropLedgerName1.SelectedItem.Text;
                string[] strArr = str.Split(new char[] { ':' }, str.Length);
                string[] strLedger_ID = strArr[0].Split(new char[] { ';' }, strArr[0].Length);
                string Cust_ID = "";
                //SqlDataReader rdr=null;

                PaymentModels payment = new PaymentModels();
                string CustName = strLedger_ID[0].ToString();
                string VoucherId = strArr[1].Trim();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/payment/DeletePayment?CustName=" + CustName + "&VoucherId=" + VoucherId).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        payment = JsonConvert.DeserializeObject<PaymentModels>(id);
                    }

                }

                //dbobj.SelectQuery("select Cust_id from Customer where Cust_Name='" + strLedger_ID[0].ToString() + "'", ref rdr);
                //if(rdr.Read())
                //{
                Cust_ID = payment.CustId;

                //}
                //rdr.Close();
                //int c = 0;
                //dbobj.Insert_or_Update("delete from payment_transaction where voucher_id = "+strArr[1].Trim(),ref c);
                //dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Payment ("+strArr[1].Trim()+")'",ref c);
                //if(Cust_ID!="")
                //{
                //	dbobj.Insert_or_Update("delete from CustomerLedgerTable where Particular = 'Voucher("+strArr[1].Trim()+")'",ref c);
                //}
                CustomerUpdate();
                MessageBox.Show("Payment Deleted");
                CreateLogFiles.ErrorLog("Form:Payment,Method:btnDelete_Click Payment of  voucher_id " + strArr[1].Trim() + " Deleted.  User : " + uid);
                panLedgerName1.Visible = false;
                btnEdit1.Visible = true;
                clear();
                btnSave.Enabled = true;
                checkPrivileges();
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Payment,Method:btnDelete_Click Exception: " + ex.Message + "  User: " + uid);
            }
        }

        /// <summary>
        /// This method is used to show or hide the cash and bank info from user when u select the apropride option.
        /// </summary>
        protected void DropBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (DropBy.SelectedItem.Text.Equals(strCash))
            {
                pnlBankInfo.Visible = false;
                txtchkDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                txtCheque.Text = "";
                txtBankname.Text = "";
            }
            else
                pnlBankInfo.Visible = true;
            PrintFlag = false;
            btnPrint.CausesValidation = true;
        }

        /// <summary>
        /// Prepares the report file Payment.txt for printing.
        /// </summary>
        protected void btnPrint_Click(object sender, System.EventArgs e)
        {
            var dt = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Acc_Date));
            var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString()));

            if (DateTime.Compare(dt, dt2) > 0)
                MessageBox.Show("Please Select Date Must be Greater than Opening Date");
            else
            {
                if (PrintFlag == false)
                {
                    if (panLedgerName1.Visible == true)
                        Update();
                    else
                        Save();
                }
            }
            Print();
            PrintFlag = false;
            btnPrint.CausesValidation = true;
        }

        /// <summary>
        /// This method is used to update only cash account after update the record.
        /// </summary>
        public void SeqCashAccount()
        {
            //SqlDataReader rdr = null;
            //SqlCommand cmd;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

            PaymentModels payment = new PaymentModels();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/payment/SeqCashAccount?Invoice_Date="+ Invoice_Date).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var id = Res.Content.ReadAsStringAsync().Result;
                    payment = JsonConvert.DeserializeObject<PaymentModels>(id);
                }
            }

            //dbobj.SelectQuery("select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master where sub_grp_id=118) and Entry_Date>='" + Invoice_Date + "' order by entry_date", ref rdr);
            //double Bal = 0;
            //string BalType = "";
            //int i = 0;
            //while (rdr.Read())
            //{
            //    if (i == 0)
            //    {
            //        BalType = rdr["Bal_Type"].ToString();
            //        i++;
            //    }
            //    else
            //    {
            //        if (double.Parse(rdr["Credit_Amount"].ToString()) != 0)
            //        {
            //            if (BalType == "Cr")
            //            {
            //                Bal += double.Parse(rdr["Credit_Amount"].ToString());
            //                BalType = "Cr";
            //            }
            //            else
            //            {
            //                Bal -= double.Parse(rdr["Credit_Amount"].ToString());
            //                if (Bal < 0)
            //                {
            //                    Bal = double.Parse(Bal.ToString().Substring(1));
            //                    BalType = "Cr";
            //                }
            //                else
            //                    BalType = "Dr";
            //            }
            //        }
            //        else if (double.Parse(rdr["Debit_Amount"].ToString()) != 0)
            //        {
            //            if (BalType == "Dr")
            //                Bal += double.Parse(rdr["Debit_Amount"].ToString());
            //            else
            //            {
            //                Bal -= double.Parse(rdr["Debit_Amount"].ToString());
            //                if (Bal < 0)
            //                {
            //                    Bal = double.Parse(Bal.ToString().Substring(1));
            //                    BalType = "Dr";
            //                }
            //                else
            //                    BalType = "Cr";
            //            }
            //        }
            //        Con.Open();
            //        cmd = new SqlCommand("update AccountsLedgerTable set Balance='" + Bal.ToString() + "',Bal_Type='" + BalType + "' where Ledger_ID='" + rdr["Ledger_ID"].ToString() + "' and Particulars='" + rdr["Particulars"].ToString() + "' ", Con);
            //        cmd.ExecuteNonQuery();
            //        cmd.Dispose();
            //        Con.Close();
            //    }
            //}
            //rdr.Close();
        }

        /// <summary>
        /// This method is used to update the customer balance after update the record.
        /// </summary>
        public void CustomerUpdate()
        {
            //SqlDataReader rdr = null;
            InventoryClass obj = new InventoryClass();
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            //object obj1 = null;
            if (Invoice_Date.IndexOf(" ") > 0)
            {
                string[] CheckDate = Invoice_Date.Split(new char[] { ' ' }, Invoice_Date.Length);
                var dt = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(CheckDate[0].ToString()));
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString()));
                if (DateTime.Compare(dt, dt2) > 0)
                    Invoice_Date = GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString());

                else
                    Invoice_Date = GenUtil.str2DDMMYYYY(CheckDate[0].ToString());

            }
            else
                Invoice_Date = GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString());
            for (int k = 0; k < LedgerID.Count; k++)
            {
                string str = LedgerID[k].ToString();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/payment/CustomerUpdate?str="+str +"&Invoice_Date="+Invoice_Date).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        //LedgerName = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                }

                //dbobj.ExecProc(DBOperations.OprType.Insert, "UpdateAccountsLedgerForCustomer", ref obj1, "@Ledger_ID", LedgerID[k].ToString(), "@Invoice_Date", Invoice_Date);
                //dbobj.SelectQuery("select cust_id from customer,ledger_master where ledger_name=cust_name and ledger_id='" + LedgerID[k].ToString() + "'", ref rdr);
                //if (rdr.Read())
                //{
                //    dbobj.ExecProc(DBOperations.OprType.Insert, "UpdateCustomerLedgerForCustomer", ref obj1, "@Cust_ID", rdr["Cust_ID"].ToString(), "@Invoice_Date", Convert.ToDateTime(Invoice_Date));
                //}
                //rdr.Close();
            }
        }

        /// <summary>
        /// This method is used to update the customer balance after insert the record.
        /// </summary>
        public void CustomerInsertUpdate(string Ledger_ID)
        {
            //SqlDataReader rdr = null;
            //SqlCommand cmd;
            InventoryClass obj = new InventoryClass();
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            //double Bal = 0;
            //string BalType = "", str = "";
            //int i = 0;
            if (Invoice_Date.IndexOf(" ") > 0)
            {
                string[] CheckDate = Invoice_Date.Split(new char[] { ' ' }, Invoice_Date.Length);
                var dt = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(CheckDate[0].ToString()));
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString()));
                if (DateTime.Compare(dt, dt2) > 0)
                    Invoice_Date = GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString());
            }
            else
                Invoice_Date = GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString());

            string Ledger_ID1 = "";
            Ledger_ID1 = Ledger_ID.ToString();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/payment/CustomerInsertUpdate?Ledger_ID1=" + Ledger_ID1 + "&Invoice_Date=" + Invoice_Date).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var id = Res.Content.ReadAsStringAsync().Result;
                    //LedgerName = JsonConvert.DeserializeObject<List<string>>(id);
                }
            }

            //rdr = obj.GetRecordSet("select top 1 Entry_Date from AccountsLedgerTable where Ledger_ID='" + Ledger_ID.ToString() + "' and Entry_Date<=Convert(datetime,'" + Invoice_Date + "',103) order by entry_date desc");
            //if (rdr.Read())
            //{
            //    var entry_date = GenUtil.str2MMDDYYYY(rdr.GetValue(0).ToString());
            //    str = "select * from AccountsLedgerTable where Ledger_ID='" + Ledger_ID + "' and Entry_Date>='" + entry_date + "' order by entry_date";
            //}
            //else
            //    str = "select * from AccountsLedgerTable where Ledger_ID='" + Ledger_ID + "' order by entry_date";
            //rdr.Close();

            //rdr = obj.GetRecordSet(str);
            //Bal = 0;
            //BalType = "";
            //i = 0;
            //while (rdr.Read())
            //{
            //    if (i == 0)
            //    {
            //        BalType = rdr["Bal_Type"].ToString();
            //        Bal = double.Parse(rdr["Balance"].ToString());
            //        i++;
            //    }
            //    else
            //    {
            //        if (double.Parse(rdr["Credit_Amount"].ToString()) != 0)
            //        {
            //            if (BalType == "Cr")
            //            {
            //                string ss = rdr["Credit_Amount"].ToString();
            //                Bal += double.Parse(rdr["Credit_Amount"].ToString());
            //                BalType = "Cr";
            //            }
            //            else
            //            {
            //                string ss = rdr["Credit_Amount"].ToString();
            //                Bal -= double.Parse(rdr["Credit_Amount"].ToString());
            //                if (Bal < 0)
            //                {
            //                    Bal = double.Parse(Bal.ToString().Substring(1));
            //                    BalType = "Cr";
            //                }
            //                else
            //                    BalType = "Dr";
            //            }
            //        }
            //        else if (double.Parse(rdr["Debit_Amount"].ToString()) != 0)
            //        {
            //            if (BalType == "Dr")
            //            {
            //                string ss = rdr["Debit_Amount"].ToString();
            //                Bal += double.Parse(rdr["Debit_Amount"].ToString());
            //            }
            //            else
            //            {
            //                string ss = rdr["Debit_Amount"].ToString();
            //                Bal -= double.Parse(rdr["Debit_Amount"].ToString());
            //                if (Bal < 0)
            //                {
            //                    Bal = double.Parse(Bal.ToString().Substring(1));
            //                    BalType = "Dr";
            //                }
            //                else
            //                    BalType = "Cr";
            //            }
            //        }
            //        Con.Open();
            //        string str11 = "update AccountsLedgerTable set Balance='" + Bal.ToString() + "',Bal_Type='" + BalType + "' where Ledger_ID='" + rdr["Ledger_ID"].ToString() + "' and Particulars='" + rdr["Particulars"].ToString() + "'";
            //        cmd = new SqlCommand("update AccountsLedgerTable set Balance='" + Bal.ToString() + "',Bal_Type='" + BalType + "' where Ledger_ID='" + rdr["Ledger_ID"].ToString() + "' and Particulars='" + rdr["Particulars"].ToString() + "'", Con);
            //        cmd.ExecuteNonQuery();
            //        cmd.Dispose();
            //        Con.Close();
            //    }
            //}
            //rdr.Close();

            //rdr = obj.GetRecordSet("select top 1 EntryDate from CustomerLedgerTable where CustID=(select Cust_ID from Customer,Ledger_Master where Ledger_Name=Cust_Name and Ledger_ID='" + Ledger_ID.ToString() + "') and EntryDate<=Convert(datetime,'" + Invoice_Date + "',103) order by entrydate desc");
            //if (rdr.Read())
            //    str = "select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer,Ledger_Master where Ledger_Name=Cust_Name and Ledger_ID='" + Ledger_ID + "') and  EntryDate>=Convert(datetime,'" + rdr.GetValue(0).ToString() + "',103) order by entrydate";
            //else
            //    str = "select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer c,Ledger_Master l where Ledger_Name=Cust_Name and Ledger_ID='" + Ledger_ID + "') order by entrydate";
            //rdr.Close();
            //rdr = obj.GetRecordSet(str);
            //Bal = 0;
            //i = 0;
            //BalType = "";
            //while (rdr.Read())
            //{
            //    if (i == 0)
            //    {
            //        BalType = rdr["BalanceType"].ToString();
            //        Bal = double.Parse(rdr["Balance"].ToString());
            //        i++;
            //    }
            //    else
            //    {
            //        if (double.Parse(rdr["CreditAmount"].ToString()) != 0)
            //        {
            //            if (BalType == "Cr.")
            //            {
            //                Bal += double.Parse(rdr["CreditAmount"].ToString());
            //                BalType = "Cr.";
            //            }
            //            else
            //            {
            //                Bal -= double.Parse(rdr["CreditAmount"].ToString());
            //                if (Bal < 0)
            //                {
            //                    Bal = double.Parse(Bal.ToString().Substring(1));
            //                    BalType = "Cr.";
            //                }
            //                else
            //                    BalType = "Dr.";
            //            }
            //        }
            //        else if (double.Parse(rdr["DebitAmount"].ToString()) != 0)
            //        {
            //            if (BalType == "Dr.")
            //                Bal += double.Parse(rdr["DebitAmount"].ToString());
            //            else
            //            {
            //                Bal -= double.Parse(rdr["DebitAmount"].ToString());
            //                if (Bal < 0)
            //                {
            //                    Bal = double.Parse(Bal.ToString().Substring(1));
            //                    BalType = "Dr.";
            //                }
            //                else
            //                    BalType = "Cr.";
            //            }
            //        }
            //        Con.Open();
            //        cmd = new SqlCommand("update CustomerLedgerTable set Balance='" + Bal.ToString() + "',BalanceType='" + BalType + "' where CustID='" + rdr["CustID"].ToString() + "' and Particular='" + rdr["Particular"].ToString() + "'", Con);
            //        cmd.ExecuteNonQuery();
            //        cmd.Dispose();
            //        Con.Close();
            //    }
            //}
            //rdr.Close();
        }
    }
}