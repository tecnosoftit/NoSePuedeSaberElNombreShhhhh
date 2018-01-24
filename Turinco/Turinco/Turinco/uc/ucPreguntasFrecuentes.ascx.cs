using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using Ssoft.Pages;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucPreguntasFrecuentes : System.Web.UI.UserControl
{
    csSeccionesInformativas csRefere = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            csRefere.CargarSeccionInformativa(
                this,
                Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_PREGUNTAS_FRECUENTES,
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
}
