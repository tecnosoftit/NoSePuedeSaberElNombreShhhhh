using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Ssoft.Pages;

public partial class uc_ucBuscadorCrucero : System.Web.UI.UserControl
{
    csBuscador csRefere = new csBuscador();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //csRefere.setCargarUno(this);
        }
    }
    protected void setCommand(object sender, CommandEventArgs e)
    {
        //csRefere.setCommand(this, sender, e);
    }

    protected void ddlCantidadPax_Selected(object sender, EventArgs e)
    {
       // csRefere.setMostrarEdadesPaxTarjetas(this, false);
    }
}
