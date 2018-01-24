using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucSeccionExperiencias : System.Web.UI.UserControl
{
    csSeccionesInformativas csRefere = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            csRefere.CargarSeccionInformativa(
                this,
                Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_EXPERIENCIAS,
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