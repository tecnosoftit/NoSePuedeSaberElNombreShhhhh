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
using Ssoft.Pages.PaginaPlanes;

public partial class uc_ucDetallePlan : System.Web.UI.UserControl
{
    csResultadoPlanes csRefere = new csResultadoPlanes();
    csTarifasPlanes csRefereTar = new csTarifasPlanes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            csRefere.setDetalleGeneral(this);
            csRefereTar.setCargarPaginaPrincipal(this);
            strNombrePlan.Text = System.Web.HttpUtility.HtmlDecode(strNombrePlan.Text);
            strDescripcion.Text = System.Web.HttpUtility.HtmlDecode(strDescripcion.Text);
            strIncluye.Text = System.Web.HttpUtility.HtmlDecode(strIncluye.Text);
            strNoIncluye.Text = System.Web.HttpUtility.HtmlDecode(strNoIncluye.Text);
            strEncuenta.Text = System.Web.HttpUtility.HtmlDecode(strEncuenta.Text);
            strRestriccion.Text = System.Web.HttpUtility.HtmlDecode(strRestriccion.Text);
            lblItinerario.Text = System.Web.HttpUtility.HtmlDecode(lblItinerario.Text);
            lblVerMasTexto.Text = System.Web.HttpUtility.HtmlDecode(lblVerMasTexto.Text);
            lblTarifaCuotas.Text = System.Web.HttpUtility.HtmlDecode(strTarifaCuotas.Text);
            strTarifaReferencia.Text = System.Web.HttpUtility.HtmlDecode(strTarifaReferencia.Text);
            lblDescBreve.Text = System.Web.HttpUtility.HtmlDecode(lblDescBreve.Text);
            strImagen.ToolTip = strNombrePlan.Text;
            strImagen.AlternateText = strNombrePlan.Text;
            strImagen1.ImageUrl = (strImagen.ImageUrl);

            if (strImagen.ImageUrl == "")
            {
                strImagen.Style.Add("display", "none");
            }

            if (strImagen1.ImageUrl == "")
            {
                strImagen1.Style.Add("display", "none");
            }
            //csRefereBan.setLlenarBanners(this, 7);
            //new csUtilitarios().setCorreos("CI00000094", "EFE", "RPL");
        }
    }
   
    protected void dlPlanes_ItemCommand(object source, DataListCommandEventArgs e)
    {
        //new csResultadoPlanes().dlPlanes_ItemCommandCircular(source, e, this);
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        //csRefere.setCommand(this, sender, e);
    }
    protected void lbEnviar_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSuNombre.Text != "" && txtTuNombre.Text != "" && txtSuEmail.Text != "")
            {

                string strplan = HttpContext.Current.Request.QueryString["Codigo"].ToString();
                new csUtilitarios().setCorreos(txtSuEmail.Text + "|" + strplan + "|" + txtTuNombre.Text, "EFE", "AMG");
                lblMensaje.Text = "Su mensaje se ha enviado Satisfactoriamente";
            }
            else
            {
                lblMensaje.Text = "Por favor diligencie todos los campos";

            }

            MPEEEnvioAmigoDesc.Show();
        }
        catch
        {
            lblMensaje.Text = "Lo sentimos su mensaje se no se ha enviado.";
        }
    }
}

