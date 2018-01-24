using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaSeccionesInformativas;
using System.Web.UI.HtmlControls;

public partial class uc_ucMapaDestino : System.Web.UI.UserControl
{
    csSeccionesInformativas csSeccion = new csSeccionesInformativas();
    private String ruta;
    private Ssoft.Utils.Enum_Tipo_Seccion_Publicacion tipoSeccion;
    private string codigo;
    private string nivel;
    private string orden;

    public String Ruta
    {
        get { return ruta; }
        set { ruta = value; }
    }    
    public Ssoft.Utils.Enum_Tipo_Seccion_Publicacion TipoSeccion
    {
        get { return tipoSeccion; }
        set { tipoSeccion = value; }
    }    
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
    public string Nivel
    {
        get { return nivel; }
        set { nivel = value; }
    }    
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

            try
            {
                if (rptSeccion.Items.Count > 0)
                {
                    Label strDescripcion = (Label)rptSeccion.Items[0].FindControl("strDescripcion");
                    Session["Mapa"] = strDescripcion.Text;
                }
            }
            catch { }

        }
    }
}