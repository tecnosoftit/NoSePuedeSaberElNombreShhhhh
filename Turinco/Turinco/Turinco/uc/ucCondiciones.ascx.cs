using System;
using System.Collections;
using System.Web.Security;
using Ssoft.Pages;
using Ssoft.Pages.PaginaPlanes;

public partial class class_uc_ucCondiciones : System.Web.UI.UserControl
{
    csReservaPlanes cRefere = new csReservaPlanes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cRefere.setCondiciones(this);
        }
    }
}
