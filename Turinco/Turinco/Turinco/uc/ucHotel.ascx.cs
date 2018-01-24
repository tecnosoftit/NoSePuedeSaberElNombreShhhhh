using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using Ssoft.Pages;
using Ssoft.Pages.PaginaPlanes;

public partial class uc_ucHotel : System.Web.UI.UserControl
{
    csResultadoPlanes ResultadoPlanes = new csResultadoPlanes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResultadoPlanes.setDetallesHotel(this);
        }
    }
}
