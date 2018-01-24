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
using Ssoft.Utils;
using Ssoft.Pages.PaginaPlanes;

public partial class uc_ucResultadoPlanes : System.Web.UI.UserControl
{
    csResultadoPlanes csRefere = new csResultadoPlanes();
    //csTarifas csRefere1 = new csTarifas();
    clsCache cCache = new csCache().cCache();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (HttpContext.Current.Request["PaQ"] != null)
            {
                if (cCache != null && cCache.DatosAdicionales.Count > 0)
                {
                    cCache.DatosAdicionales = null;
                    new clsCacheControl().ActualizaXML(cCache);
                }
            }

            //if (string.IsNullOrEmpty(Ssoft.Utils.clsSesiones.getPage()))
            //    clsSesiones.setPage("0");
            //csRefere.LlenarDatosPlanes_GeneralCompartidos(this, 0,true);
            //csRefere1.setLimpiarParametrosBusquedaPlanes(this);
            //CARGARMOS LOS PLANES
            this.cargarPlanes(0);
        }
    }

    protected void dtlPaginador_ItemCommand(object source, DataListCommandEventArgs e)
    {
        clsSesiones.setPage(e.Item.ItemIndex.ToString());
        this.cargarPlanes(e.Item.ItemIndex);
    }
    protected void cargarPlanes(int ItemIndex)
    {
        csRefere.CargarPlanes(this, Enum_TipoDestino.Internacional, true, null, ItemIndex, clsValidaciones.GetKeyOrAdd("NumplanesHome", "4"), false, null, false, false, true, null);
    }
    //protected void dtlPaginador_ItemCommand(object source, DataListCommandEventArgs e)
    //{
    //    clsSesiones.setPage(e.Item.ItemIndex.ToString());  
    //    csRefere.LlenarDatosPlanes_GeneralCompartidos(this, e.Item.ItemIndex, true);
    //}
    protected void dlPlanes_ItemCommand(object source, DataListCommandEventArgs e)
    {
        //csRefere.dlPlanes_ItemCommandCircular(source, e, this);
    }
    protected void dtlOfertas_ItemCommand1(object source, RepeaterCommandEventArgs e)
    {
        //csRefere.dlPlanes_ItemCommandCircular(source, e, this);
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        //csRefere.setFiltrarCircular(this);
    }
    protected void dlPlanes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //csRefere.dlPlanes_ItemDataBound(sender, e);
    }
    protected void Button1_Command(object sender, CommandEventArgs e)
    {
        if (string.IsNullOrEmpty(Ssoft.Utils.clsSesiones.getPage()) && this.dtlPaginador.Items.Count > 0)
        {
            Ssoft.Utils.clsSesiones.setPage("0");
        }
        if (!string.IsNullOrEmpty(Ssoft.Utils.clsSesiones.getPage()) && this.dtlPaginador.Items.Count > 1)
        {
            int pagina = int.Parse(Ssoft.Utils.clsSesiones.getPage());
            if (e.CommandName.Equals("Next"))
                pagina += 1;
            if (e.CommandName.Equals("Back"))
            {
                pagina -= 1;
            }
            if (pagina < 0)
            {
                pagina = 0;
            }
            if (this.dtlPaginador.Items.Count > 1)
            {
                if (pagina > this.dtlPaginador.Items.Count - 1)
                {
                    pagina = Convert.ToInt32(this.dtlPaginador.Items.Count - 1);
                }
            }

            if (this.dtlPaginador.Items.Count > 1 &&
                (!(this.dtlPaginador.Items.Count < 0) && !(this.dtlPaginador.Items.Count < pagina)))
            {
                dtlPaginador_ItemCommand(dtlPaginador, new DataListCommandEventArgs(dtlPaginador.Items[pagina], sender, e));
            }
        }
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        //csRefere.setCommand(this, sender, new CommandEventArgs("Ordenar", sender));
    }
}
