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
using Ssoft.Rules.Administrador;
using Ssoft.Utils;
using Ssoft.Pages;
using Ssoft.Pages.PaginaSeccionesInformativas;

public partial class uc_ucMediosPago : System.Web.UI.UserControl
{
    csSeccionesInformativas csRefere = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            csRefere.CargarSeccionInformativa(
                this,
                Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_FORMAS_DE_PAGO,
                Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
                "0",
                null,
                null,
                null,
                null,
                null,
                null);
        }
    }
    protected void rptBotones_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //if (!string.IsNullOrEmpty(e.CommandName) &&
        //    e.CommandArgument != null &&
        //    e.CommandName.Equals("BUZON") &&
        //    !e.CommandArgument.ToString().Equals("0"))
        //{
        //    try
        //    {
        //        string url = clsValidaciones.GetUrlBuzonContacto(e.CommandArgument.ToString(), Request.QueryString["idSesion"]);
        //        Response.Redirect(url);
        //    }
        //    catch (Exception) { }

        //}
    }

}
