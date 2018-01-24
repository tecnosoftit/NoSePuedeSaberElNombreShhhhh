using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using Ssoft.Pages;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucNoticia : System.Web.UI.UserControl
{
    csSeccionesInformativas csSeccion = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sCodSec = "NULL";
            if (Request.QueryString["CODSEC"] != null)
            {
                sCodSec = Request.QueryString["CODSEC"];
            }

            csSeccion.CargarSeccionInformativa(
              this,
              Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_NOTICIAS,
              Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
              "0",
              null,
              null,
              null,
              sCodSec,
              null,
              null);
        }
    }
}
