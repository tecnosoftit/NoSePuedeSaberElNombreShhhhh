using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucDetalleFolletosCruceros : System.Web.UI.UserControl
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
              Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_ACTIVIDADES,
              Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
              null,
              null,
              null,
              null,
              sCodSec,
              null,
              null);
        }
    }
}