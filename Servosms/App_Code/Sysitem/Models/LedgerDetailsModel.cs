using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LedgerDetailsModel
/// </summary>
public class LedgerDetailsModel
{
    public string BillNo { get; set; }
    public string BillDate { get; set; }
    public string Amount { get; set; }
    public string CustomerID { get; set; }
    public LedgerDetailsModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}