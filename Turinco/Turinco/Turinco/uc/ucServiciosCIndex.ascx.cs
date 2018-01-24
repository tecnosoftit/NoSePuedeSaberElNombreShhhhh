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
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucServiciosCIndex : System.Web.UI.UserControl
{
    csSeccionesInformativas csSeccion = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        //csSeccion.Load_Seccion_Noticias_Index(this);

        if (!IsPostBack)
        {
            csSeccion.CargarSeccionInformativa(
               this,
               Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_SERVICIOS_PARA_AGENCIAS,
               Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
               "1",
               null,
               null,
               null,
               null,
               null,
               null);
        }
    }
}
