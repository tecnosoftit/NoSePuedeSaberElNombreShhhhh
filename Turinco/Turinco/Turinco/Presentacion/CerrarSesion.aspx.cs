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
using Ssoft.Pages;

public partial class Presentacion_CerrarSesion : System.Web.UI.Page
{
    csFinSesion cRefere = new csFinSesion();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cRefere.setCerrarPagina(this);
        }
    }
}
