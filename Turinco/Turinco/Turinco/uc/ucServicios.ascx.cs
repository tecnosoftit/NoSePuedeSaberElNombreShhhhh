using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using Ssoft.Pages;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class ucServicios : System.Web.UI.UserControl
{
    csSeccionesInformativas csSeccion = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csSeccion.CargarSeccionInformativa(
                this,
                Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_SERVICIOS_PARA_AGENCIAS,
                Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
                "0",
                null,
                null,
                null,
                null,
                null,
                null);
        }
    }
    protected void dtlPaginadorRec_ItemCommand(object source, DataListCommandEventArgs e)
    {

        csSeccion.CargarSeccionInformativa(
             this,
             Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_SERVICIOS_PARA_AGENCIAS,
             Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
             "0",
             null,
             null,
             e.Item.ItemIndex,
             null,
             null, 
             null);
    }
}
