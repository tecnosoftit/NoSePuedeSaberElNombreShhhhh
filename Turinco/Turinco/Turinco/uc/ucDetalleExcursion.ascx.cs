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

public partial class uc_ucDetalleExcursion : System.Web.UI.UserControl
{
    csResultadoPlanes csRefere = new csResultadoPlanes();
    csTarifasPlanes csRefereTar = new csTarifasPlanes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csRefereTar.setCargarPaginaPrincipal(this);
            csRefere.setDetalleGeneral(this);
            strNombrePlan.Text = System.Web.HttpUtility.HtmlDecode(strNombrePlan.Text);
            strDescripcion.Text = System.Web.HttpUtility.HtmlDecode(strDescripcion.Text);
            strIncluye.Text = System.Web.HttpUtility.HtmlDecode(strIncluye.Text);
            strNoIncluye.Text = System.Web.HttpUtility.HtmlDecode(strNoIncluye.Text);
            strEncuenta.Text = System.Web.HttpUtility.HtmlDecode(strEncuenta.Text);
            strRestriccion.Text = System.Web.HttpUtility.HtmlDecode(strRestriccion.Text);
            //lblItinerario.Text = System.Web.HttpUtility.HtmlDecode(lblItinerario.Text);
            strTarifaCuotas.Text = System.Web.HttpUtility.HtmlDecode(strTarifaCuotas.Text);
            strTarifaReferencia.Text = System.Web.HttpUtility.HtmlDecode(strTarifaReferencia.Text);
            //csRefereBan.setLlenarBanners(this, 7);
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
}

