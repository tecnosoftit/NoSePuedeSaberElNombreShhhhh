using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using Ssoft.Pages;
using Ssoft.Pages.PaginaPlanes;

public partial class uc_ucHotelesMulti : System.Web.UI.UserControl
{
    csTarifasPlanes ResultadoPlanes = new csTarifasPlanes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResultadoPlanes.setDetallesHotelesMulti(this);
        }
    }
}
