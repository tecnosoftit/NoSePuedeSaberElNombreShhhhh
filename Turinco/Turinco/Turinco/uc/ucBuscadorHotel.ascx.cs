using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaHoteles;

public partial class uc_ucBuscadorHotel : System.Web.UI.UserControl
{
    csBusquedaHoteles oBusqueda = new csBusquedaHoteles();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void setCommand(object sender, CommandEventArgs e)
    {
        oBusqueda.setBuscarHotel(this);
    }
}