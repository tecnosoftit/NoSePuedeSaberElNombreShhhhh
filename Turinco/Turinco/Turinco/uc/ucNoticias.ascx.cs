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
using Ssoft.Utils;
using Ssoft.Pages;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucNoticias : System.Web.UI.UserControl
{
    csSeccionesInformativas csRefere = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            csRefere.CargarSeccionInformativa(
                this,
                Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_NOTICIAS,
                Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
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
