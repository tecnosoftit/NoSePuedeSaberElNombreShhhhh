﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ssoft.Rules.Reservas;
using Ssoft.Utils;
using Ssoft.Pages;

public partial class uc_ucMenuSuperiorOff : System.Web.UI.UserControl
{
    csMenu Menu = new csMenu();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
        }
    }
}
