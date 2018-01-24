using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucSeguros : System.Web.UI.UserControl
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
               1,
               null,
               null);
        }
    }
}