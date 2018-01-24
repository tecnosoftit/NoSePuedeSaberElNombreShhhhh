using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucSeccionInformativaHome : System.Web.UI.UserControl
{
    csSeccionesInformativas csSeccion = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            csSeccion.CargarSeccionInformativa(
               this,
               Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_TURISMO_ESP,
               Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
               "0",
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