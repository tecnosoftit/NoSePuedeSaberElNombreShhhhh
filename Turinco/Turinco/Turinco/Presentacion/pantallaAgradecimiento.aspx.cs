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

public partial class Presentacion_indice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string sCaracterDecimal = clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",");
            Ssoft.Pages.csGeneralsPag.MetaTag(this);

            //string sRecord = "";
            //string sPortal = "";
            //string sPrecio = "";
            //string sRuta = "";
            //string sItinerario = "";
            //string sIdCliente = "";
            //string sCodFormaPago = "";
            //string sDetalleFormaPago = "";

            //sPortal = clsValidaciones.GetKeyOrAdd("POrtalAgradecimiento", "TuTiquete");

            if (Request.QueryString["Record"] != null)
                lblCodTransaccion.Text = Request.QueryString["Record"];

            if (Request.QueryString["Valor"] != null)
                lblValor.Text = Request.QueryString["Valor"];

            if (Request.QueryString["Moneda"] != null)
                lblMoneda.Text = Request.QueryString["Moneda"];

            //if (Request.QueryString["Valor"] != null)
            //    sPrecio = Convert.ToDecimal(Request.QueryString["Valor"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString();

            if (Request.QueryString["EstadoPago"] != null)
                lblEstado.Text = Request.QueryString["EstadoPago"];

            //if (Request.QueryString["CodFormaPago"] != null)
            //    sCodFormaPago = Request.QueryString["CodFormaPago"];

            //if (Request.QueryString["DetFormaPago"] != null)
            //    sDetalleFormaPago = Request.QueryString["DetFormaPago"];
        }
    }
}
