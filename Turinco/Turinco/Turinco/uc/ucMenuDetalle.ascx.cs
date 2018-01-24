using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucMenuDetalle : System.Web.UI.UserControl
{
    csSeccionesInformativas csSeccion = new csSeccionesInformativas();
    private String ruta;

    public String Ruta
    {
        get { return ruta; }
        set { ruta = value; }
    }
    private Ssoft.Utils.Enum_Tipo_Seccion_Publicacion tipoSeccion;

    public Ssoft.Utils.Enum_Tipo_Seccion_Publicacion TipoSeccion
    {
        get { return tipoSeccion; }
        set { tipoSeccion = value; }
    }

    private string codigo;
    public string Codigo
    {
        get
        {
            if (Request.QueryString["CODSEC"] != null)
            {
                return Request.QueryString["CODSEC"];
            }
            return codigo;
        }
        set { codigo = value; }
    }

    private string nivel;
    public string Nivel
    {
        get { return nivel; }
        set { nivel = value; }
    }

    private string orden;
    public string Orden
    {
        get { return orden; }
        set { orden = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csSeccion.CargarSeccionInformativa(
              this,
              TipoSeccion,
              Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,              
              Nivel,
              Codigo,
              Orden,
              null,
              null,
              null,
              null
              );
        }
    }
}