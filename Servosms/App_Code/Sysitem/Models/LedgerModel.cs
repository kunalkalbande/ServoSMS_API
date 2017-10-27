using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LedgerModel
/// </summary>
public class LedgerModel
{
    public string LedgerID { get; set; }
    public string LedgerName { get; set; }
    public string SubGroupID { get; set; }
    public string SubGroupName { get; set; }
    public string GroupName { get; set; }
    public string GroupNature { get; set; }
    public string OpeningBalance { get; set; }
    public string BalanceType { get; set; }
    public LedgerModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}