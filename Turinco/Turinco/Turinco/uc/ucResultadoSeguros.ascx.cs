using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Ssoft.Pages;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaPlanes;

public partial class uc_ucResultadoSeguros : System.Web.UI.UserControl
{
    csResultadoPlanes csRefere = new csResultadoPlanes();   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csRefere.setResumenBusquedaSeguros(this);
            csRefere.setConsultarTarifasSeguros(this);
        }
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        //Carrito.setLimpiarDatosCache(this);
        //csRefere.setCommand(this, sender, e);
        new csTarifasPlanes().setCommand(this, sender, e);
    }

    protected void btndetalles_Click(object sender, EventArgs e)
    {
        Button btnseleccion=(Button)sender;
        RepeaterItem item=(RepeaterItem)btnseleccion.Parent;

        AjaxControlToolkit.ModalPopupExtender mpe = (AjaxControlToolkit.ModalPopupExtender)item.FindControl("MPEEGeneral");
        if (mpe != null)
        {
            mpe.Show();
        }
    }
}
