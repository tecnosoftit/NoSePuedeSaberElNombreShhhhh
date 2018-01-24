using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaPlanes;
using Ssoft.Utils;
using SsoftQuery.Planes;
using System.Data;
using Ssoft.Rules.Generales;

public partial class uc_ucPlanesHome : System.Web.UI.UserControl
{

    csGenerales csRefere1 = new csGenerales();
    csResultadoPlanes csRefere = new csResultadoPlanes();
    DataTable dtDatos = new DataTable();
    csBuscadorPlanes csRefere2 = new csBuscadorPlanes();
    clsCache cCache = new csCache().cCache();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsCache cCache = new csCache().cCache();
        if (!IsPostBack)
        {
            if (Request.QueryString["ORIGEN"] == null)
            {
                if (cCache != null)
                {
                    cCache.DatosAdicionales = null;
                    new clsCacheControl().ActualizaXML(cCache);
                }
            }
            csRefere2.setPaisesZona(this, cCache.Empresa, "CIRC");
            csRefere2.setTipologias(this, cCache.Empresa);

            if (cCache != null)
            {
                csRefere.CargarPlanes(this, Enum_TipoDestino.Todos, true, null, 0, clsValidaciones.GetKeyOrAdd("NumplanesHome", "6"), false, null, false, false, true, null);
            }
            else
            {
                csRefere.CargarPlanes(this, Enum_TipoDestino.Todos, true, null, 0, clsValidaciones.GetKeyOrAdd("NumplanesHome", "6"), false, null, false, false, true, null);
            }
        }
    }
    protected void dtlPaginador_ItemCommand(object source, DataListCommandEventArgs e)
    {
        clsSesiones.setPage(e.Item.ItemIndex.ToString());
        //csRefere.LlenarDatosPlanes_GeneralCompartidos(this, e.Item.ItemIndex, true);
        csRefere.CargarPlanes(this, Enum_TipoDestino.Todos, true, null, e.Item.ItemIndex, clsValidaciones.GetKeyOrAdd("NumplanesHome", "6"), false, null, false, false, true, null);
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
    protected void ddlZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCache cCache = new csCache().cCache();
        if (cCache != null)
        {

            csConsultasPlanes cConsPlanes = new csConsultasPlanes();
            csRefere2.setPaisesZona(this, cCache.Empresa, Convert.ToInt32(ddlZonaGeo.SelectedValue.ToString()));

            csRefere1.LlenarControlData(ddlCiudad, Enum_Controls.DropDownList, "intCode", "strDescripcion",
                true, false, null, dtDatos);
        }
    }
    protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCache cCache = new csCache().cCache();
        if (cCache != null)
        {
            csConsultasPlanes cConsPlanes = new csConsultasPlanes();
            dtDatos = cConsPlanes.ConsultarPaises_CiudadesPlanes(false, Convert.ToInt32(ddlPais.SelectedValue), cCache.Empresa);
            //dtDatos = csRefere1.ConsultarUbicacion(clsValidaciones.GetKeyOrAdd("Ciudades", "Ciudad"), Convert.ToInt32(((DropDownList)sender).SelectedValue), "intCiudad", null);
            csRefere1.LlenarControlData(ddlCiudad, Enum_Controls.DropDownList, "intCode", "strDescripcion",
                true, false, null, dtDatos);
        }
    }


    protected void setCommand(object sender, CommandEventArgs e)
    {
        csRefere2.setCommand(this, sender, e);
    }

}