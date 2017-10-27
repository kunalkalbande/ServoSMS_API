using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesInvoiceDateSelectionFilter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblInvoiceFromDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            lblInvoiceToDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
        }
    }


    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    Session["FromDate"] = lblInvoiceFromDate.Text;
    //    Session["ToDate"] = lblInvoiceToDate.Text;
    //    btnSubmit.Attributes.Add("OnClick", "closeWindow();");
    //    //Response.Redirect("~/Module/Inventory/SalesInvoice.aspx");
    //}
}