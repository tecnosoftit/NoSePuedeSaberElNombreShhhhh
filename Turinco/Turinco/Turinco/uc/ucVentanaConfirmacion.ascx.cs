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
using Ssoft.Utils;
using Ssoft.Pages;

public partial class uc_ucVentanaConfirmacion : System.Web.UI.UserControl
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["TipoPlan"] == null)
            btnTour.Visible = false;
        if (Request.QueryString["POLITICAS"] != null)
            SetPoliticas();
    }
    public void SetLocalizador_Record(String strLozalizador, string strRecord)
    {
        if (Request.QueryString["Aereo"] != null)
            btnOfertas.Visible = true;
        else
            btnOfertas.Visible = false;
        this.lblProyecto.Text = strLozalizador;
        this.lblRecord.Text = strRecord;
      
    }
    public void SetLocalizador_Record(String strLozalizador, string strRecord, bool bOfertas)
    {
        if (bOfertas)
            btnOfertas.Visible = true;
        else
            btnOfertas.Visible = false;
        this.lblProyecto.Text = strLozalizador;
        this.lblRecord.Text = strRecord;
       
    }
    protected void btnContinuar_Click(object sender, EventArgs e)
    {

        clsCacheControl cCacheControl = new clsCacheControl();
        string sSesion = cCacheControl.RecuperarSesionId((Page)HttpContext.Current.Handler);
        clsValidaciones.RedirectPagina("CarroCompras.aspx");
    }
    protected void btnTour_Click(object sender, EventArgs e)
    {
        clsValidaciones.RedirectPagina("ResultadoPlanesRela.aspx?Codigo=" + Request.QueryString["Codigo"]);
    }
    protected void btnOfertas_Click(object sender, EventArgs e)
    {
        clsValidaciones.RedirectPagina("ResultadoVuelos.aspx",false);
    }

    protected void btnPoliticas_Click(object sender, EventArgs e)
    {
        clsValidaciones.RedirectPagina("Index.aspx");
    }
    protected void SetPoliticas()
    {
        btnContinuar.Visible = false;
        btnContinuar.Visible = true;
        lblProyecto.Visible = false;
        lblRecord.Visible = false;
        lblTReserva.Text = "Apreciado Cliente, teniendo en cuenta las políticas de cancelación del hotel," +
            "su solicitud de reserva de será atendida directamente por alguno de nuestros asesores, que en breve lo estará contactando";

    }
}
