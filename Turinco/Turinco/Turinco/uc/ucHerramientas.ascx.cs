using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucHerramientas : System.Web.UI.UserControl
{
    csSeccionesInformativas csSeccion = new csSeccionesInformativas();  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (HttpContext.Current.Request.QueryString["orden"] != null)
            {

                csSeccion.CargarSeccionInformativa(
                  this,
                    Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_RECOMENDACIONES,
                   Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
                  "0",
                  null,
                  HttpContext.Current.Request.QueryString["orden"].ToString(),
                  null,
                  null,
                  1,
                  null
                  );
            }
        }
    }
}