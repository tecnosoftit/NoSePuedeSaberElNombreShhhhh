using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaPlanes;
using Ssoft.Utils;

public partial class uc_ucPlanesHome : System.Web.UI.UserControl
{
    csResultadoPlanes csRefere = new csResultadoPlanes();
    protected void Page_Load(object sender, EventArgs e)
    {
        clsCache cCache = new csCache().cCache();
        if (cCache != null)
        {
            cCache.DatosAdicionales = null;
            new clsCacheControl().ActualizaXML(cCache);
        }
        csRefere.CargarPlanes(this, Enum_TipoDestino.Todos, true, clsValidaciones.GetKeyOrAdd("ClasificacionHome", "OFR"), 0, clsValidaciones.GetKeyOrAdd("NumplanesHome", "6"), false, null, false, false, true, null);
    }
    protected void dtlPaginador_ItemCommand(object source, DataListCommandEventArgs e)
    {
        clsSesiones.setPage(e.Item.ItemIndex.ToString());
        //   csRefere.LlenarDatosPlanes_GeneralCompartidos(this, e.Item.ItemIndex, true);
        csRefere.CargarPlanes(this, Enum_TipoDestino.Todos, true, clsValidaciones.GetKeyOrAdd("ClasificacionHome", "OFR"), e.Item.ItemIndex, clsValidaciones.GetKeyOrAdd("NumplanesHome", "6"), false, null, false, false, true, null);
    }


    protected void Button1_Command(object sender, CommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(Ssoft.Utils.clsSesiones.getPage()))
        {
            int pagina = int.Parse(Ssoft.Utils.clsSesiones.getPage());
            if (e.CommandName.Equals("Next"))
                pagina += 1;
            if (e.CommandName.Equals("Back"))
                pagina -= 1;

            if (this.dtlPaginador.Items.Count > 1 &&
                (!(this.dtlPaginador.Items.Count < 0) && !(this.dtlPaginador.Items.Count < pagina)))
            {
                dtlPaginador_ItemCommand(dtlPaginador, new DataListCommandEventArgs(dtlPaginador.Items[pagina], sender, e));
            }
        }
    }

}