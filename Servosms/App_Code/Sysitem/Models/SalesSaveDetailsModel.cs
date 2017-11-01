﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SalesSaveDetailsModel
/// </summary>
public class SalesSaveDetailsModel
{
    #region Vars & Prop
    string _scheme;
    string _sch;
    string _schdiscount;
    string _balancetype;
    string _balance;
    string _custType;
    string _custname;
    string _Birdless;
    string _Tradeless;
    string _Tradeval;
    string _Foc_Discount_Type;
    string _Foc_Discount;
    string _Entry_Tax_Type;
    string _Entry_Tax1;
    string _Ebird_Discount;
    string _Ebird;
    string _Trade_Discount;
    string _Invoice_No;
    string _Order_No;
    string _Prod_Name;
    DateTime _Invoice_Date;
    DateTime _Order_Date;
    string _Sales_Type;
    string _Under_SalesMan;
    string _Cust_Name;
    string _Place;
    string _Vehicle_No;
    string _Grand_Total;
    string _Discount;
    string _CustBankName;
    string _Disc_Type;
    string _Cash_Discount;
    string _Cash_Disc_Type;
    string _VAT_Amount;
    string _SGST_Amount;
    string _CGST_Amount;
    string _Net_Amount;
    string _Promo_Scheme;
    string _Remark;
    string _Entry_By;
    DateTime _Entry_Time;
    string _EntryTime;
    string _Product_Name;
    string _Qty;
    string _Rate;
    int _sno;
    string _SchPerDiscType;
    string _SchPerDisc;
    string _SchStktDiscType;
    string _SchStktDisc;
    string _Amount;
    string _Mode_of_Payment;
    string _Vendor_ID;
    string _Vendor_Invoice_No;
    string _Vendor_Invoice_Date;
    string _Prod_ID;
    string _Category;
    string _Pack_Type;
    string _Total_Qty;
    string _Opening_Stock;
    string _Unit;
    string _Store_In;
    string _Eff_Date;
    string _Pur_Rate;
    string _Sal_Rate;
    string _HSN;
    string _IGST;
    string _CGST;
    string _SGST;
    string _Slip_No;
    string _Pack_Unit;
    string _Rec_No;
    string _SubRec_No;
    string _rec_Date;
    string _Rec_Amount;
    string _Density_in_Physical;
    string _Temp_in_Physical;
    string _Conv_Density_Phy;
    string _Density_in_Invoice_conv;
    string _Density_Variation;
    string _Den_After_Dec;
    string _Temp_After_Dec;
    string _Conv_Den_After_Dec;
    string _Total_Tax;
    string _Vendor_Name;
    string _City;
    string _Cr_Plus;
    string _Dr_Plus;
    string _Compartment;
    string _Reduction;
    string _discount;
    string _discountid1;
    string _discountid2;
    string _schemetype;
    string _discounttype;
    string _Entry_Tax;
    string _RPG_Charge;
    string _RPG_Surcharge;
    string _LTC;
    string _Trans_Charge;
    string _OLV;
    string _LST;
    string _LST_Surcharge;
    string _LFR;
    string _DOFOBC_Charge;

    string _InvoiceNo;
    string _ToDate;
    string _CustomerName;
    string _Vndr_Invoice_No;
    string _Vndr_Invoice_Date;
    string _QtyTemp;
    string _Inv_date;
    string _Pre_Amount;
    string _Credit_Limit;
    string _Prod_Code;

    //		string _Place;
    string _DueDate;
    string _CurrentBalance;
    string _VehicleNo;

    string _InvoiceDate;
    string _VendorName;
    string _vendorInvoiceNo;
    string _vendorInvoiceDate;

    string _Prod1;
    string _Prod2;
    string _Prod3;
    string _Prod4;
    string _Prod5;
    string _Prod6;
    string _Prod7;
    string _Prod8;
    string _Qty1;
    string _Qty2;
    string _Qty3;
    string _Qty4;
    string _Qty5;
    string _Qty6;
    string _Qty7;
    string _Qty8;
    string _Rate1;
    string _Rate2;
    string _Rate3;
    string _Rate4;
    string _Rate5;
    string _Rate6;
    string _Rate7;
    string _Rate8;
    string _Amt1;
    string _Amt2;
    string _Amt3;
    string _Amt4;
    string _Amt5;
    string _Amt6;
    string _Amt7;
    string _Amt8;
    string _Total;
    string _Promo;
    string _Remarks;

    string _VendorInvoiceNo;
    string _VendorInvoiceDate;

    string _ProdName1;
    string _ProdName2;
    string _ProdName3;
    string _ProdName4;
    string _QtyInLtr1;
    string _QtyInLtr2;
    string _QtyInLtr3;
    string _QtyInLtr4;
    string _ReducOther1;
    string _ReducOther2;
    string _ReducOther3;

    string _ReducOther4;
    string _EntryTax1;
    string _EntryTax2;
    string _EntryTax3;
    string _EntryTax4;
    string _RpgCharge1;
    string _RpgCharge2;
    string _RpgCharge3;
    string _RpgCharge4;
    string _Ltc1;
    string _Ltc2;
    string _Ltc3;
    string _Ltc4;
    string _TranCharge1;
    string _TranCharge2;
    string _TranCharge3;
    string _TranCharge4;
    string _Olv1;
    string _Olv2;
    string _Olv3;
    string _Olv4;
    string _Lst1;
    string _Lst2;
    string _Lst3;
    string _Lst4;

    string _Lfr1;
    string _Lfr2;
    string _Lfr3;
    string _Lfr4;
    string _ChallanNo;
    string _SecSPDisc;
    string _ChallanDate;
    string _DfbCharge1;
    string _DfbCharge2;
    string _DfbCharge3;
    string _DfbCharge4;

    string _DenPhy1;
    string _DenPhy2;
    string _DenPhy3;
    string _DenPhy4;

    string _TemPhy1;
    string _TemPhy2;
    string _TemPhy3;
    string _TemPhy4;

    string _ConvDenPhy1;
    string _ConvDenPhy2;
    string _ConvDenPhy3;
    string _ConvDenPhy4;

    string _DenInv1;

    string _DenInv2;
    string _DenInv3;
    string _DenInv4;

    string _TemInv1;
    string _TemInv2;
    string _TemInv3;
    string _TemInv4;



    string _ConvDenInv1;
    string _ConvDenInv2;
    string _ConvDenInv3;
    string _ConvDenInv4;

    string _DensVaria1;
    string _DensVaria2;
    string _DensVaria3;
    string _DensVaria4;


    string _DenAftDec1;
    string _DenAftDec2;
    string _DenAftDec3;
    string _DenAftDec4;

    string _TempAftDec1;
    string _TempAftDec2;
    string _TempAftDec3;
    string _TempAftDec4;
    string _ConvDenAft1;
    string _ConvDenAft2;
    string _ConvDenAft3;
    string _ConvDenAft4;

    string _TotalAmount;
    string _TotalAmount1;
    string _TotalAmount2;
    string _TotalAmount3;
    string _NetAmount;
    string _DenP1;
    string _DenP2;
    string _DenP3;
    string _DenP4;
    string _TemP1;
    string _TemP2;
    string _TemP3;
    string _TemP4;
    string _BankName;
    string _ChequeDate;
    string _ChequeNo;
    string _Mode;
    string _Receipt;
    /// <summary>
    string _CompanyID;
    string _DealerName;
    string _DealerShip;
    string _Address;
    //string _City;
    string _State;
    string _Country;
    string _PhoneNo;
    string _FaxNo;
    string _Email;
    string _Website;
    string _TinNo;
    string _ExplosiveNo;
    string _FoodLicNO;
    string _WM;
    string _Drive;
    string _Logo;
    string _tempQty;
    string _Actual_Amount;
    string _sub0;
    string _sub1;
    string _sub2;
    string _sub3;
    string _sub4;
    string _sub5;
    string _sub6;
    string _sub7;
    string _sub8;
    string _sub9;
    string _sub10;
    string _sub11;
    string _sub12;
    string _sub13;
    string _sub14;
    string _sub15;
    string _sub16;
    string _sub17;
    string _sub18;
    string _sub19;
    string _sub20;
    string _sub21;
    string _Month;
    string _MinLabel;
    string _MaxLabel;
    string _ReOrderLabel;
    string _BatchNo;
    string _MRP;
    string _New_fixeddiscAmount; //Add by vikas 5.11.2012
    string _New_fixeddisc;       //Add by vikas 5.11.2012
    string _Group_Name;          //Add by vikas 23.10.2012
    string _SPack_Type;          //Add by vikas 7.11.2012
    string _OVD_No;          //Add by vikas 12.11.2012

    /*************Add by vikas 12.11.2012********/
    public string OVD_No
    {
        get
        {
            return _OVD_No;
        }
        set
        {
            _OVD_No = value;
        }
    }
    /*************Add by vikas 7.11.2012********/
    public string SPack_Type
    {
        get
        {
            return _SPack_Type;
        }
        set
        {
            _SPack_Type = value;
        }
    }
    /*************Add by vikas 23.10.2012********/
    public string Group_Name
    {
        get
        {
            return _Group_Name;
        }
        set
        {
            _Group_Name = value;
        }
    }

    /************Add by vikas 5.11.2012***************/
    public string New_fixeddisc
    {
        get
        {
            return _New_fixeddisc;
        }
        set
        {
            _New_fixeddisc = value;
        }
    }
    public string New_fixeddiscAmount
    {
        get
        {
            return _New_fixeddiscAmount;
        }
        set
        {
            _New_fixeddiscAmount = value;
        }
    }
    /********************End****************************/

    public string ReOrderLabel
    {
        get
        {
            return _ReOrderLabel;
        }
        set
        {
            _ReOrderLabel = value;
        }
    }
    public string BatchNo
    {
        get
        {
            return _BatchNo;
        }
        set
        {
            _BatchNo = value;
        }
    }
    public string MRP
    {
        get
        {
            return _MRP;
        }
        set
        {
            _MRP = value;
        }
    }
    public string MaxLabel
    {
        get
        {
            return _MaxLabel;
        }
        set
        {
            _MaxLabel = value;
        }
    }
    public string MinLabel
    {
        get
        {
            return _MinLabel;
        }
        set
        {
            _MinLabel = value;
        }
    }
    public string sub0
    {
        get
        {
            return _sub0;
        }
        set
        {
            _sub0 = value;
        }
    }
    public string sub1
    {
        get
        {
            return _sub1;
        }
        set
        {
            _sub1 = value;
        }
    }
    public string sub2
    {
        get
        {
            return _sub2;
        }
        set
        {
            _sub2 = value;
        }
    }
    public string sub3
    {
        get
        {
            return _sub3;
        }
        set
        {
            _sub3 = value;
        }
    }
    public string sub4
    {
        get
        {
            return _sub4;
        }
        set
        {
            _sub4 = value;
        }
    }
    public string sub5
    {
        get
        {
            return _sub5;
        }
        set
        {
            _sub5 = value;
        }
    }
    public string sub6
    {
        get
        {
            return _sub6;
        }
        set
        {
            _sub6 = value;
        }
    }
    public string sub7
    {
        get
        {
            return _sub7;
        }
        set
        {
            _sub7 = value;
        }
    }
    public string sub8
    {
        get
        {
            return _sub8;
        }
        set
        {
            _sub8 = value;
        }
    }
    public string sub9
    {
        get
        {
            return _sub9;
        }
        set
        {
            _sub9 = value;
        }
    }
    public string sub10
    {
        get
        {
            return _sub10;
        }
        set
        {
            _sub10 = value;
        }
    }
    public string sub11
    {
        get
        {
            return _sub11;
        }
        set
        {
            _sub11 = value;
        }
    }
    public string sub12
    {
        get
        {
            return _sub12;
        }
        set
        {
            _sub12 = value;
        }
    }
    public string sub13
    {
        get
        {
            return _sub13;
        }
        set
        {
            _sub13 = value;
        }
    }
    public string sub14
    {
        get
        {
            return _sub14;
        }
        set
        {
            _sub14 = value;
        }
    }
    public string sub15
    {
        get
        {
            return _sub15;
        }
        set
        {
            _sub15 = value;
        }
    }
    public string sub16
    {
        get
        {
            return _sub16;
        }
        set
        {
            _sub16 = value;
        }
    }
    public string sub17
    {
        get
        {
            return _sub17;
        }
        set
        {
            _sub17 = value;
        }
    }
    public string sub18
    {
        get
        {
            return _sub18;
        }
        set
        {
            _sub18 = value;
        }
    }
    public string sub19
    {
        get
        {
            return _sub19;
        }
        set
        {
            _sub19 = value;
        }
    }
    public string sub20
    {
        get
        {
            return _sub20;
        }
        set
        {
            _sub20 = value;
        }
    }
    public string sub21
    {
        get
        {
            return _sub21;
        }
        set
        {
            _sub21 = value;
        }
    }
    public string Month
    {
        get
        {
            return _Month;
        }
        set
        {
            _Month = value;
        }
    }
    public string CompanyID
    {
        get
        {
            return _CompanyID;
        }
        set
        {
            _CompanyID = value;
        }
    }

    public string DealerName

    {
        get
        {
            return _DealerName;
        }
        set
        {
            _DealerName = value;
        }
    }

    public string DealerShip

    {
        get
        {
            return _DealerShip;
        }
        set
        {
            _DealerShip = value;
        }
    }

    public string Address

    {
        get
        {
            return _Address;
        }
        set
        {
            _Address = value;
        }
    }

    //		public string City
    //
    //		{
    //			get
    //			{
    //				return _City;
    //			}
    //			set
    //			{
    //				_City=value;
    //			}
    //		}	

    public string State

    {
        get
        {
            return _State;
        }
        set
        {
            _State = value;
        }
    }

    public string Country

    {
        get
        {
            return _Country;
        }
        set
        {
            _Country = value;
        }
    }

    public string PhoneNo

    {
        get
        {
            return _PhoneNo;
        }
        set
        {
            _PhoneNo = value;
        }
    }


    public string FaxNo

    {
        get
        {
            return _FaxNo;
        }
        set
        {
            _FaxNo = value;
        }
    }


    public string Email

    {
        get
        {
            return _Email;
        }
        set
        {
            _Email = value;
        }
    }


    public string Website

    {
        get
        {
            return _Website;
        }
        set
        {
            _Website = value;
        }
    }


    public string TinNo

    {
        get
        {
            return _TinNo;
        }
        set
        {
            _TinNo = value;
        }
    }

    public string ExplosiveNo

    {
        get
        {
            return _ExplosiveNo;
        }
        set
        {
            _ExplosiveNo = value;
        }
    }
    public string Receipt
    {
        get
        {
            return _Receipt;
        }
        set
        {
            _Receipt = value;
        }
    }
    public string BankName
    {
        get
        {
            return _BankName;
        }
        set
        {
            _BankName = value;
        }
    }
    public string ChequeNo
    {
        get
        {
            return _ChequeNo;
        }
        set
        {
            _ChequeNo = value;
        }
    }
    public string Mode
    {
        get
        {
            return _Mode;
        }
        set
        {
            _Mode = value;
        }
    }
    public string ChequeDate
    {
        get
        {
            return _ChequeDate;
        }
        set
        {
            _ChequeDate = value;
        }
    }
    public string FoodLicNO
    {
        get
        {
            return _FoodLicNO;
        }
        set
        {
            _FoodLicNO = value;
        }
    }

    public string WM

    {
        get
        {
            return _WM;
        }
        set
        {
            _WM = value;
        }
    }

    public string Drive

    {
        get
        {
            return _Drive;
        }
        set
        {
            _Drive = value;
        }
    }

    public string Logo

    {
        get
        {
            return _Logo;
        }
        set
        {
            _Logo = value;
        }
    }

    /// </summary>

    public string Prod_Name

    {
        get
        {
            return _Prod_Name;
        }
        set
        {
            _Prod_Name = value;
        }
    }
    public string Vndr_Invoice_No
    {
        get
        {
            return _Vndr_Invoice_No;
        }
        set
        {
            _Vndr_Invoice_No = value;
        }
    }
    public string Vndr_Invoice_Date
    {
        get
        {
            return _Vndr_Invoice_Date;
        }
        set
        {
            _Vndr_Invoice_Date = value;
        }
    }

    public string DenP1
    {
        get
        {
            return _DenP1;
        }
        set
        {
            _DenP1 = value;
        }
    }

    public string DenP2
    {
        get
        {
            return _DenP2;
        }
        set
        {
            _DenP2 = value;
        }
    }

    public string DenP3
    {
        get
        {
            return _DenP3;
        }
        set
        {
            _DenP3 = value;
        }
    }

    public string DenP4
    {
        get
        {
            return _DenP4;
        }
        set
        {
            _DenP4 = value;
        }
    }

    public string TemP1
    {
        get
        {
            return _TemP1;
        }
        set
        {
            _TemP1 = value;
        }
    }

    public string TemP2
    {
        get
        {
            return _TemP2;
        }
        set
        {
            _TemP2 = value;
        }
    }

    public string TemP3
    {
        get
        {
            return _TemP3;
        }
        set
        {
            _TemP3 = value;
        }
    }

    public string TemP4
    {
        get
        {
            return _TemP4;
        }
        set
        {
            _TemP4 = value;
        }
    }

    public string TotalAmount
    {
        get
        {
            return _TotalAmount;
        }
        set
        {
            _TotalAmount = value;
        }
    }

    public string Credit_Limit { get; set; }

    public string schdiscount { get; set; }

    string _foediscount = "";
    public string foediscount { get; set; }

    string _foediscounttype = "";
    public string foediscounttype { get; set; }

    string _SecSPDiscType = "";
    public string SecSPDiscType
    {
        get
        {
            return _SecSPDiscType;
        }
        set
        {
            _SecSPDiscType = value;
        }
    }
    string _foediscountrs = "";
    public string foediscountrs { get; set; }

    public string TotalAmount1
    {
        get
        {
            return _TotalAmount1;
        }
        set
        {
            _TotalAmount1 = value;
        }
    }

    public string TotalAmount2
    {
        get
        {
            return _TotalAmount2;
        }
        set
        {
            _TotalAmount2 = value;
        }
    }

    public string TotalAmount3
    {
        get
        {
            return _TotalAmount3;
        }
        set
        {
            _TotalAmount3 = value;
        }
    }


    public string NetAmount
    {
        get
        {
            return _NetAmount;
        }
        set
        {
            _NetAmount = value;
        }
    }

    public string VendorName
    {
        get
        {
            return _VendorName;
        }
        set
        {
            _VendorName = value;
        }
    }
    public string VendorInvoiceNo
    {
        get
        {
            return _VendorInvoiceNo;
        }
        set
        {
            _VendorInvoiceNo = value;
        }
    }
    public string VendorInvoiceDate
    {
        get
        {
            return _VendorInvoiceDate;
        }
        set
        {
            _VendorInvoiceDate = value;
        }
    }
    public string VehicleNo
    {
        get
        {
            return _VehicleNo;
        }
        set
        {
            _VehicleNo = value;
        }
    }
    public string ProdName1
    {
        get
        {
            return _ProdName1;
        }
        set
        {
            _ProdName1 = value;
        }
    }
    public string ProdName2
    {
        get
        {
            return _ProdName2;
        }
        set
        {
            _ProdName2 = value;
        }
    }
    public string ProdName3
    {
        get
        {
            return _ProdName3;
        }
        set
        {
            _ProdName3 = value;
        }
    }

    public string ProdName4
    {
        get
        {
            return _ProdName4;
        }
        set
        {
            _ProdName4 = value;
        }
    }
    public string QtyInLtr1
    {
        get
        {
            return _QtyInLtr1;
        }
        set
        {
            _QtyInLtr1 = value;
        }
    }
    public string QtyInLtr2
    {
        get
        {
            return _QtyInLtr2;
        }
        set
        {
            _QtyInLtr2 = value;
        }
    }
    public string QtyInLtr3
    {
        get
        {
            return _QtyInLtr3;
        }
        set
        {
            _QtyInLtr3 = value;
        }
    }
    public string QtyInLtr4
    {
        get
        {
            return _QtyInLtr4;
        }
        set
        {
            _QtyInLtr4 = value;
        }
    }
    public string ReducOther1
    {
        get
        {
            return _ReducOther1;
        }
        set
        {
            _ReducOther1 = value;
        }
    }
    public string ReducOther2
    {
        get
        {
            return _ReducOther2;
        }
        set
        {
            _ReducOther2 = value;
        }
    }
    public string ReducOther3
    {
        get
        {
            return _ReducOther3;
        }
        set
        {
            _ReducOther3 = value;
        }
    }
    public string ReducOther4
    {
        get
        {
            return _ReducOther4;
        }
        set
        {
            _ReducOther4 = value;
        }
    }
    public string EntryTax1
    {
        get
        {
            return _EntryTax1;
        }
        set
        {
            _EntryTax1 = value;
        }
    }
    public string EntryTax2
    {
        get
        {
            return _EntryTax2;
        }
        set
        {
            _EntryTax2 = value;
        }
    }
    public string EntryTax3
    {
        get
        {
            return _EntryTax3;
        }
        set
        {
            _EntryTax3 = value;
        }
    }
    public string EntryTax4
    {
        get
        {
            return _EntryTax4;
        }
        set
        {
            _EntryTax4 = value;
        }
    }
    public string RpgCharge1
    {
        get
        {
            return _RpgCharge1;
        }
        set
        {
            _RpgCharge1 = value;
        }
    }
    public string RpgCharge2
    {
        get
        {
            return _RpgCharge2;
        }
        set
        {
            _RpgCharge2 = value;
        }
    }
    public string RpgCharge3
    {
        get
        {
            return _RpgCharge3;
        }
        set
        {
            _RpgCharge3 = value;
        }
    }
    public string RpgCharge4
    {
        get
        {
            return _RpgCharge4;
        }
        set
        {
            _RpgCharge4 = value;
        }
    }

    public string Ltc1
    {
        get
        {
            return _Ltc1;
        }
        set
        {
            _Ltc1 = value;
        }
    }
    public string Ltc2
    {
        get
        {
            return _Ltc2;
        }
        set
        {
            _Ltc2 = value;
        }
    }
    public string Ltc3
    {
        get
        {
            return _Ltc3;
        }
        set
        {
            _Ltc3 = value;
        }
    }
    public string Ltc4
    {
        get
        {
            return _Ltc4;
        }
        set
        {
            _Ltc4 = value;
        }
    }
    public string TranCharge1
    {
        get
        {
            return _TranCharge1;
        }
        set
        {
            _TranCharge1 = value;
        }
    }
    public string TranCharge2
    {
        get
        {
            return _TranCharge2;
        }
        set
        {
            _TranCharge2 = value;
        }
    }

    public string TranCharge3
    {
        get
        {
            return _TranCharge3;
        }
        set
        {
            _TranCharge3 = value;
        }
    }
    public string TranCharge4
    {
        get
        {
            return _TranCharge4;
        }
        set
        {
            _TranCharge4 = value;
        }
    }
    public string Olv1
    {
        get
        {
            return _Olv1;
        }
        set
        {
            _Olv1 = value;
        }
    }


    public string Olv2
    {
        get
        {
            return _Olv2;
        }
        set
        {
            _Olv2 = value;
        }
    }

    public string QtyTemp
    {
        get
        {
            return _QtyTemp;
        }
        set
        {
            _QtyTemp = value;
        }
    }
    public string Olv3
    {
        get
        {
            return _Olv3;
        }
        set
        {
            _Olv3 = value;
        }
    }
    public string Olv4
    {
        get
        {
            return _Olv4;
        }
        set
        {
            _Olv4 = value;
        }
    }


    public string Lst1
    {
        get
        {
            return _Lst1;
        }
        set
        {
            _Lst1 = value;
        }
    }
    public string Lst2
    {
        get
        {
            return _Lst2;

        }
        set
        {
            _Lst2 = value;
        }
    }
    public string Lst4
    {
        get
        {
            return _Lst4;
        }
        set
        {
            _Lst4 = value;
        }
    }

    public string Lst3
    {
        get
        {
            return _Lst3;
        }
        set
        {
            _Lst3 = value;
        }
    }
    public string Lfr1
    {
        get
        {
            return _Lfr1;
        }
        set
        {
            _Lfr1 = value;
        }
    }
    public string Lfr2
    {
        get
        {
            return _Lfr2;
        }
        set
        {
            _Lfr2 = value;
        }
    }


    public string Lfr3
    {
        get
        {
            return _Lfr3;
        }
        set
        {
            _Lfr3 = value;
        }
    }
    public string Lfr4
    {
        get
        {
            return _Lfr4;
        }
        set
        {
            _Lfr4 = value;
        }
    }


    public string DfbCharge1
    {
        get
        {
            return _DfbCharge1;
        }
        set
        {
            _DfbCharge1 = value;
        }
    }
    public string ChallanNo { get; set; }

    public string SecSPDisc { get; set; }

    public string ChallanDate { get; set; }

    public string DfbCharge2
    {
        get
        {
            return _DfbCharge2;
        }
        set
        {
            _DfbCharge2 = value;
        }
    }
    public string DfbCharge3
    {
        get
        {
            return _DfbCharge3;
        }
        set
        {
            _DfbCharge3 = value;
        }
    }
    public string DfbCharge4
    {
        get
        {
            return _DfbCharge4;
        }
        set
        {
            _DfbCharge4 = value;
        }
    }


    public string DenPhy1
    {
        get
        {
            return _DenPhy1;
        }
        set
        {
            _DenPhy1 = value;
        }
    }
    public string DenPhy2
    {
        get
        {
            return _DenPhy2;
        }
        set
        {
            _DenPhy2 = value;
        }
    }
    public string DenPhy3
    {
        get
        {
            return _DenPhy3;
        }
        set
        {
            _DenPhy3 = value;
        }
    }


    public string DenPhy4
    {
        get
        {
            return _DenPhy4;
        }
        set
        {
            _DenPhy4 = value;
        }
    }



    public string TemPhy1
    {
        get
        {
            return _TemPhy1;
        }
        set
        {
            _TemPhy1 = value;
        }
    }
    public string TemPhy2
    {
        get
        {
            return _TemPhy2;
        }
        set
        {
            _TemPhy2 = value;
        }
    }


    public string TemPhy3
    {
        get
        {
            return _TemPhy3;
        }
        set
        {
            _TemPhy3 = value;
        }
    }
    public string TemPhy4
    {
        get
        {
            return _TemPhy4;
        }
        set
        {
            _TemPhy4 = value;
        }
    }


    public string ConvDenPhy1
    {
        get
        {
            return _ConvDenPhy1;
        }
        set
        {
            _ConvDenPhy1 = value;
        }
    }


    public string ConvDenPhy2
    {
        get
        {
            return _ConvDenPhy2;
        }
        set
        {
            _ConvDenPhy2 = value;
        }
    }
    public string ConvDenPhy3
    {
        get
        {
            return _ConvDenPhy3;
        }
        set
        {
            _ConvDenPhy3 = value;
        }
    }
    public string ConvDenPhy4
    {
        get
        {
            return _ConvDenPhy4;
        }
        set
        {
            _ConvDenPhy4 = value;
        }
    }


    public string DenInv1
    {
        get
        {
            return _DenInv1;
        }
        set
        {
            _DenInv1 = value;
        }
    }
    public string DenInv2
    {
        get
        {
            return _DenInv2;
        }
        set
        {
            _DenInv2 = value;
        }
    }
    public string DenInv3
    {
        get
        {
            return _DenInv3;
        }
        set
        {
            _DenInv3 = value;
        }
    }


    public string DenInv4
    {
        get
        {
            return _DenInv4;
        }
        set
        {
            _DenInv4 = value;
        }
    }


    public string TemInv1
    {
        get
        {
            return _TemInv1;
        }
        set
        {
            _TemInv1 = value;
        }
    }
    public string TemInv2
    {
        get
        {
            return _TemInv2;
        }
        set
        {
            _TemInv2 = value;
        }
    }

    public string TemInv3
    {
        get
        {
            return _TemInv3;
        }
        set
        {
            _TemInv3 = value;
        }
    }
    public string TemInv4
    {
        get
        {
            return _TemInv4;
        }
        set
        {
            _TemInv4 = value;
        }
    }
    public string ConvDenInv1
    {
        get
        {
            return _ConvDenInv1;
        }
        set
        {
            _ConvDenInv1 = value;
        }
    }

    public string ConvDenInv2
    {
        get
        {
            return _ConvDenInv2;
        }
        set
        {
            _ConvDenInv2 = value;
        }
    }
    public string ConvDenInv3
    {
        get
        {
            return _ConvDenInv3;
        }
        set
        {
            _ConvDenInv3 = value;
        }
    }
    public string ConvDenInv4
    {
        get
        {
            return _ConvDenInv4;
        }
        set
        {
            _ConvDenInv4 = value;
        }
    }


    public string DensVaria1
    {
        get
        {
            return _DensVaria1;
        }
        set
        {
            _DensVaria1 = value;
        }
    }
    public string DensVaria2
    {
        get
        {
            return _DensVaria2;
        }
        set
        {
            _DensVaria2 = value;
        }
    }
    public string DensVaria3
    {
        get
        {
            return _DensVaria3;
        }
        set
        {
            _DensVaria3 = value;
        }
    }


    public string DensVaria4
    {
        get
        {
            return _DensVaria4;
        }
        set
        {
            _DensVaria4 = value;
        }
    }


    public string DenAftDec1
    {
        get
        {
            return _DenAftDec1;
        }
        set
        {
            _DenAftDec1 = value;
        }
    }
    public string DenAftDec2
    {
        get
        {
            return _DenAftDec2;
        }
        set
        {
            _DenAftDec2 = value;
        }
    }


    public string DenAftDec3
    {
        get
        {
            return _DenAftDec3;
        }
        set
        {
            _DenAftDec3 = value;
        }
    }
    public string DenAftDec4
    {
        get
        {
            return _DenAftDec4;
        }
        set
        {
            _DenAftDec4 = value;
        }
    }

    public string TempAftDec1
    {
        get
        {
            return _TempAftDec1;
        }
        set
        {
            _TempAftDec1 = value;
        }
    }


    public string TempAftDec2
    {
        get
        {
            return _TempAftDec2;
        }
        set
        {
            _TempAftDec2 = value;
        }
    }
    public string TempAftDec3
    {
        get
        {
            return _TempAftDec3;
        }
        set
        {
            _TempAftDec3 = value;
        }
    }
    public string TempAftDec4
    {
        get
        {
            return _TempAftDec4;
        }
        set
        {
            _TempAftDec4 = value;
        }
    }



    public string ConvDenAft1
    {
        get
        {
            return _ConvDenAft1;
        }
        set
        {
            _ConvDenAft1 = value;
        }
    }
    public string ConvDenAft2
    {
        get
        {
            return _ConvDenAft2;
        }
        set
        {
            _ConvDenAft2 = value;
        }
    }
    public string ConvDenAft3
    {
        get
        {
            return _ConvDenAft3;
        }
        set
        {
            _ConvDenAft3 = value;
        }
    }
    public string ConvDenAft4
    {
        get
        {
            return _ConvDenAft4;
        }
        set
        {
            _ConvDenAft4 = value;
        }
    }


    //dfdfdf

    public string InvoiceDate
    {
        get
        {
            return _InvoiceDate;
        }
        set
        {
            _InvoiceDate = value;
        }
    }



    public string vendorInvoiceNo
    {
        get
        {
            return _vendorInvoiceNo;
        }
        set
        {
            _vendorInvoiceNo = value;
        }
    }

    public string vendorInvoiceDate
    {
        get
        {
            return _vendorInvoiceDate;
        }
        set
        {
            _vendorInvoiceDate = value;
        }
    }





    public string Rate1
    {
        get
        {
            return _Rate1;
        }
        set
        {
            _Rate1 = value;
        }
    }
    public string Total
    {
        get
        {
            return _Total;
        }
        set
        {
            _Total = value;
        }
    }
    public string Promo
    {
        get
        {
            return _Promo;
        }
        set
        {
            _Promo = value;
        }
    }
    public string Remarks
    {
        get
        {
            return _Remarks;
        }
        set
        {
            _Remarks = value;
        }
    }


    public string Rate2
    {
        get
        {
            return _Rate2;
        }
        set
        {
            _Rate2 = value;
        }
    }
    public string Rate3
    {
        get
        {
            return _Rate3;
        }
        set
        {
            _Rate3 = value;
        }
    }
    public string Rate4
    {
        get
        {
            return _Rate4;
        }
        set
        {
            _Rate4 = value;
        }
    }
    public string Rate5
    {
        get
        {
            return _Rate5;
        }
        set
        {
            _Rate5 = value;
        }
    }
    public string Rate6
    {
        get
        {
            return _Rate6;
        }
        set
        {
            _Rate6 = value;
        }
    }
    public string Rate7
    {
        get
        {
            return _Rate7;
        }
        set
        {
            _Rate7 = value;
        }
    }
    public string Rate8
    {
        get
        {
            return _Rate8;
        }
        set
        {
            _Rate8 = value;
        }
    }
    public string Amt1
    {
        get
        {
            return _Amt1;
        }
        set
        {
            _Amt1 = value;
        }
    }
    public string Amt2
    {
        get
        {
            return _Amt2;
        }
        set
        {
            _Amt2 = value;
        }
    }
    public string Amt3
    {
        get
        {
            return _Amt3;
        }
        set
        {
            _Amt3 = value;
        }
    }
    public string Amt4
    {
        get
        {
            return _Amt4;
        }
        set
        {
            _Amt4 = value;
        }
    }
    public string Amt5
    {
        get
        {
            return _Amt5;
        }
        set
        {
            _Amt5 = value;
        }
    }
    public string Amt6
    {
        get
        {
            return _Amt6;
        }
        set
        {
            _Amt6 = value;
        }
    }
    public string Amt7
    {
        get
        {
            return _Amt7;
        }
        set
        {
            _Amt7 = value;
        }
    }
    public string Amt8
    {
        get
        {
            return _Amt8;
        }
        set
        {
            _Amt8 = value;
        }
    }





    public string Prod1
    {
        get
        {
            return _Prod1;
        }
        set
        {
            _Prod1 = value;
        }
    }




    public string Prod2
    {
        get
        {
            return _Prod2;
        }
        set
        {
            _Prod2 = value;
        }
    }


    public string Prod3
    {
        get
        {
            return _Prod3;
        }
        set
        {
            _Prod3 = value;
        }
    }

    public string Prod4
    {
        get
        {
            return _Prod4;
        }
        set
        {
            _Prod4 = value;
        }
    }
    public string Prod5
    {
        get
        {
            return _Prod5;
        }
        set
        {
            _Prod5 = value;
        }
    }
    public string Prod6
    {
        get
        {
            return _Prod6;
        }
        set
        {
            _Prod6 = value;
        }
    }
    public string Prod7
    {
        get
        {
            return _Prod7;
        }
        set
        {
            _Prod7 = value;
        }
    }
    public string Prod8
    {
        get
        {
            return _Prod8;
        }
        set
        {
            _Prod8 = value;
        }
    }
    public string Qty1
    {
        get
        {
            return _Qty1;
        }
        set
        {
            _Qty1 = value;
        }
    }
    public string Qty2
    {
        get
        {
            return _Qty2;
        }
        set
        {
            _Qty2 = value;
        }
    }
    public string Qty3
    {
        get
        {
            return _Qty3;
        }
        set
        {
            _Qty3 = value;
        }

    }
    public string Qty4
    {
        get
        {
            return _Qty4;
        }
        set
        {
            _Qty4 = value;
        }
    }
    public string Qty5
    {
        get
        {
            return _Qty5;
        }
        set
        {
            _Qty5 = value;
        }
    }
    public string Qty6
    {
        get
        {
            return _Qty6;
        }
        set
        {
            _Qty6 = value;
        }
    }
    public string Qty7
    {
        get
        {
            return _Qty7;
        }
        set
        {
            _Qty7 = value;
        }
    }
    public string Qty8
    {
        get
        {
            return _Qty8;
        }
        set
        {
            _Qty8 = value;
        }
    }



    public string InvoiceNo
    {
        get
        {
            return _InvoiceNo;
        }
        set
        {
            _InvoiceNo = value;
        }
    }

    public string ToDate
    {
        get
        {
            return _ToDate;
        }
        set
        {
            _ToDate = value;
        }
    }

    public string CustomerName
    {
        get
        {
            return _CustomerName;
        }
        set
        {
            _CustomerName = value;
        }
    }

    public string Place { get; set; }

    public string DueDate
    {
        get
        {
            return _DueDate;
        }
        set
        {
            _DueDate = value;
        }
    }

    public string CurrentBalance
    {
        get
        {
            return _CurrentBalance;
        }
        set
        {
            _CurrentBalance = value;
        }

    }





    public string Compartment
    {
        get
        {
            return _Compartment;
        }
        set
        {
            _Compartment = value;
        }
    }
    public string Reduction
    {
        get
        {
            return _Reduction;
        }
        set
        {
            _Reduction = value;
        }
    }

    public string Entry_Tax
    {
        get
        {
            return _Entry_Tax;
        }
        set
        {
            _Entry_Tax = value;
        }
    }

    public string RPG_Charge
    {
        get
        {
            return _RPG_Charge;
        }
        set
        {
            _RPG_Charge = value;
        }
    }

    public string RPG_Surcharge
    {
        get
        {
            return _RPG_Surcharge;
        }
        set
        {
            _RPG_Surcharge = value;
        }
    }

    public string LTC
    {
        get
        {
            return _LTC;
        }
        set
        {
            _LTC = value;
        }
    }


    public string Trans_Charge
    {
        get
        {
            return _Trans_Charge;
        }
        set
        {
            _Trans_Charge = value;
        }
    }

    public string OLV
    {
        get
        {
            return _OLV;
        }
        set
        {
            _OLV = value;
        }
    }

    public string LST
    {
        get
        {
            return _LST;
        }
        set
        {
            _LST = value;
        }
    }

    public string LST_Sucharge
    {
        get
        {
            return _LST_Surcharge;
        }
        set
        {
            _LST_Surcharge = value;
        }
    }


    public string LFR
    {
        get
        {
            return _LFR;
        }
        set
        {
            _LFR = value;
        }
    }

    public string DOFOBC_Charge
    {
        get
        {
            return _DOFOBC_Charge;
        }
        set
        {
            _DOFOBC_Charge = value;
        }
    }









    public string Cr_Plus { get; set; }

    public string Pre_Amount
    {
        get
        {
            return _Pre_Amount;
        }
        set
        {
            _Pre_Amount = value;
        }
    }
    public string Dr_Plus { get; set; }

    public string Vendor_Name
    {
        get
        {
            return _Vendor_Name;
        }
        set
        {
            _Vendor_Name = value;
        }
    }
    public string City
    {
        get
        {
            return _City;
        }
        set
        {
            _City = value;
        }
    }
    public string Total_Tax
    {
        get
        {
            return _Total_Tax;
        }
        set
        {
            _Total_Tax = value;
        }
    }
    public string Conv_Den_After_Dec
    {
        get
        {
            return _Conv_Den_After_Dec;
        }
        set
        {
            _Conv_Den_After_Dec = value;
        }
    }
    public string Temp_After_Dec
    {
        get
        {
            return _Temp_After_Dec;
        }
        set
        {
            _Temp_After_Dec = value;
        }
    }
    public string Density_After_Dec
    {
        get
        {
            return _Den_After_Dec;
        }
        set
        {
            _Den_After_Dec = value;
        }
    }
    public string Density_Variation
    {
        get
        {
            return _Density_Variation;
        }
        set
        {
            _Density_Variation = value;
        }
    }
    public string Density_in_Invoice_Conv
    {
        get
        {
            return _Density_in_Invoice_conv;
        }
        set
        {
            _Density_in_Invoice_conv = value;
        }
    }
    public string Converted_Density_Phy
    {
        get
        {
            return _Conv_Density_Phy;
        }
        set
        {
            _Conv_Density_Phy = value;
        }
    }

    public string Temp_in_Physical
    {
        get
        {
            return _Temp_in_Physical;
        }
        set
        {
            _Temp_in_Physical = value;
        }
    }

    public string Density_in_Physical
    {
        get
        {
            return _Density_in_Physical;
        }
        set
        {
            _Density_in_Physical = value;
        }
    }

    public string Received_Amount
    {
        get
        {
            return _Rec_Amount;
        }
        set
        {
            _Rec_Amount = value;
        }
    }
    public string Received_Date
    {
        get
        {
            return _rec_Date;
        }
        set
        {
            _rec_Date = value;
        }
    }
    public string Received_No
    {
        get
        {
            return _Rec_No;
        }
        set
        {
            _Rec_No = value;
        }
    }
    public string SubReceived_No
    {
        get
        {
            return _SubRec_No;
        }
        set
        {
            _SubRec_No = value;
        }
    }
    public string Slip_No { get; set; }

    public string schemetype
    {
        get
        {
            return _schemetype;
        }
        set
        {
            _schemetype = value;
        }
    }
    public string discount
    {
        get
        {
            return _discount;
        }
        set
        {
            _discount = value;
        }
    }
    public string discountid1
    {
        get
        {
            return _discountid1;
        }
        set
        {
            _discountid1 = value;
        }
    }
    public string discountid2
    {
        get
        {
            return _discountid2;
        }
        set
        {
            _discountid2 = value;
        }
    }
    public string discounttype
    {
        get
        {
            return _discounttype;
        }
        set
        {
            _discounttype = value;
        }
    }
    public string Eff_Date
    {
        get
        {
            return _Eff_Date;
        }
        set
        {
            _Eff_Date = value;
        }
    }
    public string Pur_Rate
    {
        get
        {
            return _Pur_Rate;
        }
        set
        {
            _Pur_Rate = value;
        }
    }
    public string Sal_Rate
    {
        get
        {
            return _Sal_Rate;
        }
        set
        {
            _Sal_Rate = value;
        }
    }
    public string HSN
    {
        get
        {
            return _HSN;
        }
        set
        {
            _HSN = value;
        }
    }
    public string IGST
    {
        get
        {
            return _IGST;
        }
        set
        {
            _IGST = value;
        }
    }
    public string CGST
    {
        get
        {
            return _CGST;
        }
        set
        {
            _CGST = value;
        }
    }
    public string SGST
    {
        get
        {
            return _SGST;
        }
        set
        {
            _SGST = value;
        }
    }
    public string Invoice_No { get; set; }

    public string Order_No { get; set; }
    //      public string Order_No
    //{
    //	get
    //	{
    //		return _Order_No;
    //	}
    //	set
    //	{
    //		_Order_No=value;
    //	}
    //}
    public DateTime Invoice_Date { get; set; }
    public DateTime Order_Date
    {
        get
        {
            return _Order_Date;
        }
        set
        {
            _Order_Date = value;
        }
    }
    public string Sales_Type { get; set; }

    public string Under_SalesMan { get; set; }

    public string Customer_Name { get; set; }

    public string Vehicle_No { get; set; }

    public string Grand_Total { get; set; }

    public string Discount { get; set; }

    public string CustBankName
    {
        get
        {
            return _CustBankName;
        }
        set
        {
            _CustBankName = value;
        }
    }
    public string Discount_Type { get; set; }


    public string Cash_Discount { get; set; }

    public string Cash_Disc_Type { get; set; }

    //1111111111111111111111111111111
    public string VAT_Amount { get; set; }

    public string SGST_Amount { get; set; }

    public string CGST_Amount { get; set; }

    public string Tradeval
    {
        get
        {
            return _Tradeval;
        }
        set
        {
            _Tradeval = value;
        }
    }

    public string Trade_Discount
    {
        get
        {
            return _Trade_Discount;
        }
        set
        {
            _Trade_Discount = value;
        }
    }
    string _totalqtyltr;
    public string totalqtyltr { get; set; }

    public string Ebird
    {
        get
        {
            return _Ebird;
        }
        set
        {
            _Ebird = value;
        }
    }
    public string Tradeless
    {
        get
        {
            return _Tradeless;
        }
        set
        {
            _Tradeless = value;
        }
    }

    public string Birdless
    {
        get
        {
            return _Birdless;
        }
        set
        {
            _Birdless = value;
        }
    }
    public string Ebird_Discount
    {
        get
        {
            return _Ebird_Discount;
        }
        set
        {
            _Ebird_Discount = value;
        }
    }

    public string Entry_Tax1
    {
        get
        {
            return _Entry_Tax1;
        }
        set
        {
            _Entry_Tax1 = value;
        }
    }

    public string Entry_Tax_Type
    {
        get
        {
            return _Entry_Tax_Type;
        }
        set
        {
            _Entry_Tax_Type = value;
        }
    }

    public string Foc_Discount
    {
        get
        {
            return _Foc_Discount;
        }
        set
        {
            _Foc_Discount = value;
        }
    }
    string _fixed_Discount;
    public string fixed_Discount
    {
        get
        {
            return _fixed_Discount;
        }
        set
        {
            _fixed_Discount = value;
        }
    }
    string _fixed_Discount_Type;
    public string fixed_Discount_Type
    {
        get
        {
            return _fixed_Discount_Type;
        }
        set
        {
            _fixed_Discount_Type = value;
        }
    }
    public string Foc_Discount_Type
    {
        get
        {
            return _Foc_Discount_Type;
        }
        set
        {
            _Foc_Discount_Type = value;
        }
    }
    public string Net_Amount { get; set; }

    public string Promo_Scheme { get; set; }

    public string Remerk { get; set; }

    public string Entry_By { get; set; }

    public DateTime Entry_Time { get; set; }

    public string EntryTime
    {
        get
        {
            return _EntryTime;
        }
        set
        {
            _EntryTime = value;
        }
    }
    public string Product_Name
    {
        get
        {
            return _Product_Name;
        }
        set
        {
            _Product_Name = value;
        }
    }
    public string sch
    {
        get
        {
            return _sch;
        }
        set
        {
            _sch = value;
        }
    }
    string _foe;
    public string foe
    {
        get
        {
            return _foe;
        }
        set
        {
            _foe = value;
        }
    }
    string _foc;
    public string foc
    {
        get
        {
            return _foc;
        }
        set
        {
            _foc = value;
        }
    }
    public string Qty
    {
        get
        {
            return _Qty;
        }
        set
        {
            _Qty = value;
        }
    }
    public string scheme1
    {
        get
        {
            return _scheme;
        }
        set
        {
            _scheme = value;
        }
    }

    public string Rate
    {
        get
        {
            return _Rate;
        }
        set
        {
            _Rate = value;
        }
    }
    public int sno
    {
        get
        {
            return _sno;
        }
        set
        {
            _sno = value;
        }
    }
    public string Amount
    {
        get
        {
            return _Amount;
        }
        set
        {
            _Amount = value;
        }
    }
    public string SchPerDisc
    {
        get
        {
            return _SchPerDisc;
        }
        set
        {
            _SchPerDisc = value;
        }
    }
    public string SchPerDiscType
    {
        get
        {
            return _SchPerDiscType;
        }
        set
        {
            _SchPerDiscType = value;
        }
    }
    public string SchStktDisc
    {
        get
        {
            return _SchStktDisc;
        }
        set
        {
            _SchStktDisc = value;
        }
    }
    public string SchStktDiscType
    {
        get
        {
            return _SchStktDiscType;
        }
        set
        {
            _SchStktDiscType = value;
        }
    }
    public string Mode_of_Payment
    {
        get
        {
            return _Mode_of_Payment;
        }
        set
        {
            _Mode_of_Payment = value;
        }
    }
    public string Vendor_ID
    {
        get
        {
            return _Vendor_ID;
        }
        set
        {
            _Vendor_ID = value;
        }
    }
    public string Vendor_Invoice_No
    {
        get
        {
            return _Vendor_Invoice_No;
        }
        set
        {
            _Vendor_Invoice_No = value;
        }
    }
    public string Vendor_Invoice_Date
    {
        get
        {
            return _Vendor_Invoice_Date;
        }
        set
        {
            _Vendor_Invoice_Date = value;
        }
    }
    public string Prod_ID
    {
        get
        {
            return _Prod_ID;
        }
        set
        {
            _Prod_ID = value;
        }
    }
    public string Category
    {
        get
        {
            return _Category;
        }
        set
        {
            _Category = value;
        }
    }
    public string Package_Type
    {
        get
        {
            return _Pack_Type;
        }
        set
        {
            _Pack_Type = value;
        }
    }
    public string Package_Unit
    {
        get
        {
            return _Pack_Unit;
        }
        set
        {
            _Pack_Unit = value;
        }
    }
    public string Total_Qty
    {
        get
        {
            return _Total_Qty;
        }
        set
        {
            _Total_Qty = value;
        }
    }
    public string Opening_Stock
    {
        get
        {
            return _Opening_Stock;
        }
        set
        {
            _Opening_Stock = value;
        }
    }
    public string Unit
    {
        get
        {
            return _Unit;
        }
        set
        {
            _Unit = value;
        }
    }
    public string Store_In
    {
        get
        {
            return _Store_In;
        }
        set
        {
            _Store_In = value;
        }
    }
    public string Inv_date
    {
        get
        {
            return _Inv_date;
        }
        set
        {
            _Inv_date = value;
        }
    }
    public string Prod_Code
    {
        get
        {
            return _Prod_Code;
        }
        set
        {
            _Prod_Code = value;
        }
    }
    public string tempQty
    {
        get
        {
            return _tempQty;
        }
        set
        {
            _tempQty = value;
        }
    }

    public string Actual_Amount
    {
        get
        {
            return _Actual_Amount;
        }
        set
        {
            _Actual_Amount = value;
        }
    }

    //************************
    string _foid = "";
    public string foid
    {
        get
        {
            return _foid;
        }
        set
        {
            _foid = value;
        }
    }
    string _discription = "";
    public string discription
    {
        get
        {
            return _discription;
        }
        set
        {
            _discription = value;
        }
    }

    string _custtype = "";
    public string custtype
    {
        get
        {
            return _custtype;
        }
        set
        {
            _custtype = value;
        }
    }
    string _cust_id = "";
    public string cust_id
    {
        get
        {
            return _cust_id;
        }
        set
        {
            _cust_id = value;
        }
    }
    string _Narration;
    public string Narration
    {
        get
        {
            return _Narration;
        }
        set
        {
            _Narration = value;
        }
    }
    string _prod_id = "";
    public string prod_id
    {
        get
        {
            return _prod_id;
        }
        set
        {
            _prod_id = value;
        }
    }
    string _SSA_OR_SSI_NAME = "";
    public string SSA_OR_SSI_NAME
    {
        get
        {
            return _SSA_OR_SSI_NAME;
        }
        set
        {
            _SSA_OR_SSI_NAME = value;
        }
    }
    string _SALES_RETURN_NUMBER = "";
    public string SALES_RETURN_NUMBER
    {
        get
        {
            return _SALES_RETURN_NUMBER;
        }
        set
        {
            _SALES_RETURN_NUMBER = value;
        }
    }
    string _SALES_RETURN_GR_DATE = "";
    public string SALES_RETURN_GR_DATE
    {
        get
        {
            return _SALES_RETURN_GR_DATE;
        }
        set
        {
            _SALES_RETURN_GR_DATE = value;
        }
    }
    string _SECONDARY_CUSTOMER_NUMBER = "";
    public string SECONDARY_CUSTOMER_NUMBER
    {
        get
        {
            return _SECONDARY_CUSTOMER_NUMBER;
        }
        set
        {
            _SECONDARY_CUSTOMER_NUMBER = value;
        }
    }
    string _SEC_CUSTOMER_NAME = "";
    public string SEC_CUSTOMER_NAME
    {
        get
        {
            return _SEC_CUSTOMER_NAME;
        }
        set
        {
            _SEC_CUSTOMER_NAME = value;
        }
    }
    string _STOCKIST_GST_NUM = "";
    public string STOCKIST_GST_NUM
    {
        get
        {
            return _STOCKIST_GST_NUM;
        }
        set
        {
            _STOCKIST_GST_NUM = value;
        }
    }
    string _CUSTOMER_GST_NUM = "";
    public string CUSTOMER_GST_NUM
    {
        get
        {
            return _CUSTOMER_GST_NUM;
        }
        set
        {
            _CUSTOMER_GST_NUM = value;
        }
    }
    string _SEC_CUSTOMER_HIERARCHY = "";
    public string SEC_CUSTOMER_HIERARCHY
    {
        get
        {
            return _SEC_CUSTOMER_HIERARCHY;
        }
        set
        {
            _SEC_CUSTOMER_HIERARCHY = value;
        }
    }
    string _SECONDARY_CUSTOMER_CITY = "";
    public string SECONDARY_CUSTOMER_CITY
    {
        get
        {
            return _SECONDARY_CUSTOMER_CITY;
        }
        set
        {
            _SECONDARY_CUSTOMER_CITY = value;
        }
    }
    string _INVOICE_TYPE = "";
    public string INVOICE_TYPE
    {
        get
        {
            return _INVOICE_TYPE;
        }
        set
        {
            _INVOICE_TYPE = value;
        }
    }
    string _SGST_AMOUNT = "";
    public string SGST_AMOUNT
    {
        get
        {
            return _SGST_AMOUNT;
        }
        set
        {
            _SGST_AMOUNT = value;
        }
    }
    string _CGST_AMOUNT = "";
    public string CGST_AMOUNT
    {
        get
        {
            return _CGST_AMOUNT;
        }
        set
        {
            _CGST_AMOUNT = value;
        }
    }
    string _IGST_AMOUNT = "";
    public string IGST_AMOUNT
    {
        get
        {
            return _IGST_AMOUNT;
        }
        set
        {
            _IGST_AMOUNT = value;
        }
    }
    string _TOTAL_TAX_AMOUNT = "";
    public string TOTAL_TAX_AMOUNT
    {
        get
        {
            return _TOTAL_TAX_AMOUNT;
        }
        set
        {
            _TOTAL_TAX_AMOUNT = value;
        }
    }
    string _NET_AMOUNT = "";
    public string NET_AMOUNT
    {
        get
        {
            return _NET_AMOUNT;
        }
        set
        {
            _NET_AMOUNT = value;
        }
    }
    string _ADHOC_DISCOUNT = "";
    public string ADHOC_DISCOUNT
    {
        get
        {
            return _ADHOC_DISCOUNT;
        }
        set
        {
            _ADHOC_DISCOUNT = value;
        }
    }
    string _HO_AI_DISC_JULY17 = "";
    public string HO_AI_DISC_JULY17
    {
        get
        {
            return _HO_AI_DISC_JULY17;
        }
        set
        {
            _HO_AI_DISC_JULY17 = value;
        }
    }
    string _HYUNDAI_DIS_17_18 = "";
    public string HYUNDAI_DIS_17_18
    {
        get
        {
            return _HYUNDAI_DIS_17_18;
        }
        set
        {
            _HYUNDAI_DIS_17_18 = value;
        }
    }
    string _MGO_VC_DIS_16_17 = "";
    public string MGO_VC_DIS_16_17
    {
        get
        {
            return _MGO_VC_DIS_16_17;
        }
        set
        {
            _MGO_VC_DIS_16_17 = value;
        }
    }
    int _PRODUCT_CODE;
    public int PRODUCT_CODE
    {
        get
        {
            return _PRODUCT_CODE;
        }
        set
        {
            _PRODUCT_CODE = value;
        }
    }
    string _PRODUCT_NAME = "";
    public string PRODUCT_NAME
    {
        get
        {
            return _PRODUCT_NAME;
        }
        set
        {
            _PRODUCT_NAME = value;
        }
    }
    int _HSN_NO;
    public int HSN_NO
    {
        get
        {
            return _HSN_NO;
        }
        set
        {
            _HSN_NO = value;
        }
    }
    int _PACK_CODE;
    public int PACK_CODE
    {
        get
        {
            return _PACK_CODE;
        }
        set
        {
            _PACK_CODE = value;
        }
    }
    string _PACK_NAME = "";
    public string PACK_NAME
    {
        get
        {
            return _PACK_NAME;
        }
        set
        {
            _PACK_NAME = value;
        }
    }
    int _SKU_CODE;
    public int SKU_CODE
    {
        get
        {
            return _SKU_CODE;
        }
        set
        {
            _SKU_CODE = value;
        }
    }
    string _SALEABLE_QTY = "";
    public string SALEABLE_QTY
    {
        get
        {
            return _SALEABLE_QTY;
        }
        set
        {
            _SALEABLE_QTY = value;
        }
    }
    string _FREE_QTY = "";
    public string FREE_QTY
    {
        get
        {
            return _FREE_QTY;
        }
        set
        {
            _FREE_QTY = value;
        }
    }
    string _SAMPLE_QTY = "";
    public string SAMPLE_QTY
    {
        get
        {
            return _SAMPLE_QTY;
        }
        set
        {
            _SAMPLE_QTY = value;
        }
    }
    string _SALEABLE_QTY_IN_LTR_OR_KG = "";
    public string SALEABLE_QTY_IN_LTR_OR_KG
    {
        get
        {
            return _SALEABLE_QTY_IN_LTR_OR_KG;
        }
        set
        {
            _SALEABLE_QTY_IN_LTR_OR_KG = value;
        }
    }
    string _FREE_QTY_IN_LTR_OR_KG = "";
    public string FREE_QTY_IN_LTR_OR_KG
    {
        get
        {
            return _FREE_QTY_IN_LTR_OR_KG;
        }
        set
        {
            _FREE_QTY_IN_LTR_OR_KG = value;
        }
    }
    string _SAMPLE_QTY_IN_LTR_OR_KG = "";
    public string SAMPLE_QTY_IN_LTR_OR_KG
    {
        get
        {
            return _SAMPLE_QTY_IN_LTR_OR_KG;
        }
        set
        {
            _SAMPLE_QTY_IN_LTR_OR_KG = value;
        }
    }
    string _SecInvoiceNo = "";
    public string SecInvoiceNo
    {
        get
        {
            return _SecInvoiceNo;
        }
        set
        {
            _SecInvoiceNo = value;
        }
    }
    string _SALES_TYPE = "";
    public string SALES_TYPE
    {
        get
        {
            return _SALES_TYPE;
        }
        set
        {
            _SALES_TYPE = value;
        }
    }
    string _VEHICLE_NO = "";
    public string VEHICLE_NO
    {
        get
        {
            return _VEHICLE_NO;
        }
        set
        {
            _VEHICLE_NO = value;
        }
    }
    int _CHALLAN_NO;
    public int CHALLAN_NO
    {
        get
        {
            return _CHALLAN_NO;
        }
        set
        {
            _CHALLAN_NO = value;
        }
    }
    int _Challan_Id;
    public int Challan_Id
    {
        get
        {
            return _Challan_Id;
        }
        set
        {
            _Challan_Id = value;
        }
    }
    string _City_Name;
    public string City_Name
    {
        get
        {
            return _City_Name;
        }
        set
        {
            _City_Name = value;
        }
    }
    string _Geopgrophical_State;
    string _Stockist_SAP_Code;
    string _Stockist_Name;
    string _Source_Of_Supply;
    string _Bill_Type;
    string _HSN_Code;
    string _GSTIN;

    int _Item_Qty;
    string _Qty_Ltr_Kg;
    int _SKU_Code;

    string _SKU_Name;
    string _RSP_CDP;
    string _SGST_Tax;
    string _CGST_Tax;
    string _IGST_Tax;
    string _ZSSD;
    string _ZCON;
    string _ZDFI;
    string _ZDCB;
    string _NET_AMT_IN_PAISE;
    public string GSTIN
    {
        get
        {
            return _GSTIN;
        }
        set
        {
            _GSTIN = value;
        }
    }

    public string HSN_Code
    {
        get
        {
            return _HSN_Code;
        }
        set
        {
            _HSN_Code = value;
        }
    }
    public string Geopgrophical_State
    {
        get
        {
            return _Geopgrophical_State;
        }
        set
        {
            _Geopgrophical_State = value;
        }
    }

    public string Stockist_SAP_Code
    {
        get
        {
            return _Stockist_SAP_Code;
        }
        set
        {
            _Stockist_SAP_Code = value;
        }
    }

    public string Stockist_Name
    {
        get
        {
            return _Stockist_Name;
        }
        set
        {
            _Stockist_Name = value;
        }
    }

    public string Source_Of_Supply
    {
        get
        {
            return _Source_Of_Supply;
        }
        set
        {
            _Source_Of_Supply = value;
        }
    }

    public string Bill_Type
    {
        get
        {
            return _Bill_Type;
        }
        set
        {
            _Bill_Type = value;
        }
    }
    public int Item_Qty
    {
        get
        {
            return _Item_Qty;
        }
        set
        {
            _Item_Qty = value;
        }
    }

    public string Qty_Ltr_Kg
    {
        get
        {
            return _Qty_Ltr_Kg;
        }
        set
        {
            _Qty_Ltr_Kg = value;
        }
    }

    public int SKU_Code
    {
        get
        {
            return _SKU_Code;
        }
        set
        {
            _SKU_Code = value;
        }
    }
    public string NET_AMT_IN_PAISE
    {
        get
        {
            return _NET_AMT_IN_PAISE;
        }
        set
        {
            _NET_AMT_IN_PAISE = value;
        }
    }

    public string ZDFI
    {
        get
        {
            return _ZDFI;
        }
        set
        {
            _ZDFI = value;
        }
    }

    public string ZCON
    {
        get
        {
            return _ZCON;
        }
        set
        {
            _ZCON = value;
        }
    }

    public string ZSSD
    {
        get
        {
            return _ZSSD;
        }
        set
        {
            _ZSSD = value;
        }
    }

    public string IGST_Tax
    {
        get
        {
            return _IGST_Tax;
        }
        set
        {
            _IGST_Tax = value;
        }
    }

    public string SGST_Tax
    {
        get
        {
            return _SGST_Tax;
        }
        set
        {
            _SGST_Tax = value;
        }
    }

    public string CGST_Tax
    {
        get
        {
            return _CGST_Tax;
        }
        set
        {
            _CGST_Tax = value;
        }
    }

    public string SKU_Name
    {
        get
        {
            return _SKU_Name;
        }
        set
        {
            _SKU_Name = value;
        }
    }

    public string RSP_CDP
    {
        get
        {
            return _RSP_CDP;
        }
        set
        {
            _RSP_CDP = value;
        }
    }
    public string ZDCB
    {
        get
        {
            return _ZDCB;
        }
        set
        {
            _ZDCB = value;
        }
    }

    //*************************************
    #endregion

    public SalesSaveDetailsModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}