﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrganizationModel
/// </summary>
public class OrganizationModel
{
    public string InvoiceNo { get; set; }
    public string CompanyID { get; set; }
    public string DealerName { get; set; }
    public string DealerShip { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public string StateOffice { get; set; }
    public string Country { get; set; }
    public string PhoneOff { get; set; }
    public string FaxNo { get; set; }
    public string EMail { get; set; }
    public string Website { get; set; }
    public string Tinno { get; set; }
    public string Entrytax { get; set; }
    public string FoodLicNO { get; set; }
    public string WMlic { get; set; }
    public string Logo { get; set; }
    public string DivOffice { get; set; }
    public string Message { get; set; }
    public string VATRate { get; set; }
    public string DateFrom { get; set; }
    public string DateTo { get; set; }    
    public OrganizationModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}