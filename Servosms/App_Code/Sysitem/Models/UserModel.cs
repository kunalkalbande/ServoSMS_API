﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserModel
/// </summary>
public class UserModel
{
    public string UserID { get; set; }
    public string LoginName { get; set; }
    public string UserName { get; set; }
    public string Role_ID { get; set; }
    public string RoleName{ get; set; }
    public string Password { get; set; }
    public UserModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}