using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucQuienesSomos : System.Web.UI.UserControl
{
    csSeccionesInformativas csSeccion = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csSeccion.CargarSeccionInformativa(
               this,
               Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_QUIENES_SOMOS,
               Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
               "1",
               null,
               null,
               null,
               null,
               null,
               null,
               null);
        }
    }
}
