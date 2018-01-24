using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucTemasSociales : System.Web.UI.UserControl
{
    csSeccionesInformativas csRefere = new csSeccionesInformativas();

    private string orden;
    public string Orden
    {
        get { return orden; }
        set { orden = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            csRefere.CargarSeccionInformativa(
                this,
                Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_FORMAS_DE_PAGO,
                Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
                null,
                Orden,
                null,
                null,
                null,
                null,
                null);
        }
    }
   
}
