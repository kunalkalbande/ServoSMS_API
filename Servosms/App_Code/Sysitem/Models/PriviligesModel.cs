using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PriviligesModel
/// </summary>
public class PriviligesModel
{
    public string User_ID { get; set; }
    public string Module_ID { get; set; }
    public string SubModule_ID { get; set; }
    public string ViewFlag { get; set; }
    public string Add_Flag { get; set; }
    public string Edit_Flag { get; set; }
    public string Del_Flag { get; set; }
    public string Login_Name { get; set; }
    public PriviligesModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}