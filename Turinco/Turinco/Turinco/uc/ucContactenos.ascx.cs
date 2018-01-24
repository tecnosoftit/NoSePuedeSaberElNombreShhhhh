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
using AjaxPro;
using Ssoft.Pages.PaginaSeccionesInformativas;
using Ssoft.Pages.PaginaContactenos;
using Ssoft.Pages.PaginaBanners;
using Ssoft.Utils;
public partial class uc_ucContactenos : System.Web.UI.UserControl
{
    //csContactenos cRefere = new csContactenos();
    csSeccionesInformativas cRefere2 = new csSeccionesInformativas();
    csBanners csRefere = new csBanners();
    protected void Page_Load(object sender, EventArgs e)
    {
       
            csRefere.setLlenarBanners(this, 1);
            lblTextoContactenos.Text = clsValidaciones.GetKeyOrAdd("TextoContactenos", "Comunicate con nosotros en breve uno de nuestros asesores se comunicara contigo.");
            cRefere2.CargarSeccionInformativa(
                      this,
                      Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_CONTACTENOS,
                      Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
                      "0",
                      null,
                      null,
                      null,
                      null,
                      null,
                      null,
                      null);
        
    }

}
