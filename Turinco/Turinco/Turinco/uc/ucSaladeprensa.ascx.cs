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
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucSaladeprensa : System.Web.UI.UserControl
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

            string sCodSec = "NULL";
            if (Request.QueryString["SCODSEC"] != null)
            {
                sCodSec = Request.QueryString["SCODSEC"];
            }

            csRefere.CargarSeccionInformativa(
                 this,
                 Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_SALA_PRENSA,
                 Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaCarruselHome,
                 "1",
                 null,
                 null,
                 null,
                 null,
                 null,
                 this.Orden,
                 null);

            //DataSet dstitulo = new DataSet();

            //dstitulo=new tblSeccionInf().Get(sCodSec);
            //lblTituloSeccionPadre.Text = dstitulo.Tables[0].Rows[0]["strtitulo"].ToString();
       
        }
    }

   
}
