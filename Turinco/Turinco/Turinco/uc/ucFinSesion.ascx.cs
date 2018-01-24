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

public partial class ucFinSesion : System.Web.UI.UserControl
{
    csFinSesion cRefere = new csFinSesion();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cRefere.setCargar(this);
        }
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        cRefere.setEnviar(this);
    }
}