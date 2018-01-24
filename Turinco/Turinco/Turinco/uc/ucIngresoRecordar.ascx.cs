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
using Ssoft.ManejadorExcepciones;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucIngreso : System.Web.UI.UserControl
{
    csResultadoVuelos csVuelos = new csResultadoVuelos();
    clsParametros cParametros = new clsParametros();
    csSeccionesInformativas cRefere = new csSeccionesInformativas();

    protected void Page_Load(object sender, EventArgs e)
    {
        //cRefere.setCargar(this, Enum_Login.LoginGen);
        cRefere.CargarSeccionInformativa(
                 this,
                 Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_TEXTO_LOGIN,
                 Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
                 "0",
                 null,
                 null,
                 null,
                 null,
                 null,
                 null);
    }

    protected void lbOlvido_Click(object sender, EventArgs e)
    {
        new csUtilitarios().setOlvido(this);
    }


}
