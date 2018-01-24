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
using Ssoft.Pages.PaginaContactenos;
//using AjaxPro;

//[AjaxPro.AjaxNamespace("uc_ucFormularioContacto")]
public partial class uc_ucFormularioContacto : System.Web.UI.UserControl
{
    csContactenos cRefere = new csContactenos();
    //csSeccionesInformativas cRefere2 = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(uc_ucFormularioContacto));

            cRefere.setBuzonDinamico(this);
            ////cRefere2.setLLenarTextoContactenos(this);
            //cRefere2.CargarSeccionInformativa(
            //          this,
            //          Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_CONTACTENOS,
            //          Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaContacto,
            //          Ssoft.Utils.Enum_Seccion_Informativa.NINGUNA,
            //          "0",
            //          null,
            //          null,
            //          null,
            //          null,
            //          null);
        }
    }
    #region [EVENTOS]
    //[AjaxPro.AjaxMethod()]
    //public string csContacto(string sHtml)
    //{
        //cRefere.setEnviarDinamico(sHtml);
        //return "1";
    //}

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        if (txtEmail.Text != "")
        {
            HttpContext.Current.Session["$CorreoContacto"] = txtEmail.Text;
        }
        if (txtCiudad.Text == "")
        {
            txtCiudad.Text = "..";
        }
        if (txtApellidos.Text.Equals(""))
        {
            txtApellidos.Text = "..";
        }
        cRefere.setEnviarDinamico(this);
    }
    protected void ddlTipoMensaje_SelectedIndexChanged(object sender, EventArgs e)
    {
        cRefere.ddlTipoMensaje_SelectedIndexChanged(this);
    }
    protected void ddlTemamensaje_SelectedIndexChanged(object sender, EventArgs e)
    {
        //cRefere.ddlTemamensajeDinamico_SelectedIndexChanged(sender, this);
    }
    #endregion
}
