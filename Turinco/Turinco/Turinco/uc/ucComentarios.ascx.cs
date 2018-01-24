using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ssoft.Pages;
using Ssoft.Utils;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucComentarios : System.Web.UI.UserControl
{
    csSeccionesInformativas csSeccion = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {

        string sCodSec = "NULL";
        if (Request.QueryString["CODSEC"] != null)
        {
            sCodSec = Request.QueryString["CODSEC"];
        }
        if (!IsPostBack)
        {
            csSeccion.CargarSeccionInformativa(
                  this,
                  Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_NOTICIAS,
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
