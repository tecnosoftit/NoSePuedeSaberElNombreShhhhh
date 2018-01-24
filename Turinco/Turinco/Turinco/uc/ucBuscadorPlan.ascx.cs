using System;
using System.Data;
using System.Web.UI.WebControls;
using Ssoft.Pages;
using Ssoft.Rules.Generales;
using Ssoft.Utils;
using Ssoft.Pages.PaginaPlanes;
using SsoftQuery.Planes;

public partial class uc_ucBuscadorPlan : System.Web.UI.UserControl
{
    csBuscadorPlanes csRefere = new csBuscadorPlanes();
    csGenerales csRefere1 = new csGenerales();
    DataTable dtDatos = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csRefere.setPlan(this);
            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {
                csRefere.setPaisesZona(this, cCache.Empresa, "CIRC");
                csRefere.setTipologias(this, cCache.Empresa);

                if (!string.IsNullOrWhiteSpace(Request.QueryString["FiltroTexto"]))
                {
                    this.txtFiltroTexto.Text = Request.QueryString["FiltroTexto"];
                }
            }
        }
    }

    protected void setCommand(object sender, CommandEventArgs e)
    {
        csRefere.setCommand(this, sender, e);
    }

    //protected void rbtSeleccion(object sender, EventArgs e)
    //{
    //    csGeneralsPag.setSeleccionRadio(sender, "rbtRentadora");
    //}
    protected void ddlZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCache cCache = new csCache().cCache();
        if (cCache != null)
        {

            csConsultasPlanes cConsPlanes = new csConsultasPlanes();
            csRefere.setPaisesZona(this, cCache.Empresa, Convert.ToInt32(ddlZonaGeo.SelectedValue.ToString()));

            //csRefere1.LlenarControlData(ddlCiudad, Enum_Controls.DropDownList, "intCode", "strDescripcion",
            //    true, false, null, dtDatos);
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
}
