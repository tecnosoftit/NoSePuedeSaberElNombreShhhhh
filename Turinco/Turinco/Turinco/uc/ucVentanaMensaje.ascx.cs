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
using Ssoft.Utils;

public partial class uc_ucVentanaMensaje : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public void SetMensaje(string strMensaje)
    {
        this.lblMensaje.Text = strMensaje;
    }
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        clsCacheControl cCacheControl = new clsCacheControl();
        string sSesion = cCacheControl.RecuperarSesionId((Page)HttpContext.Current.Handler);
        Response.Redirect("Index.aspx" + "?idSesion=" + sSesion);
    }
}
