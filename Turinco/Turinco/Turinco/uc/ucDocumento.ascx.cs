using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using Ssoft.Pages;
using Ssoft.Utils;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucDocumento : System.Web.UI.UserControl
{
    csSeccionesInformativas Secciones = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        Secciones.setCargar(this, clsSesiones.GET_COMMANDARGUMENT());
    }
}
