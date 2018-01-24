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
using System.Collections.Generic;
using WS_SsoftSabre.OTA_AirLowFareSearch;
using Ssoft.Utils;
using System.Text;
using Ssoft.Pages;
using Ssoft.Pages.PaginaMiCuenta;

public partial class uc_ucDetalleReserva : System.Web.UI.UserControl
{
    csBuscador csRefere = new csBuscador();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csListaReservas Reservas = new csListaReservas();
            clsCache cCache = new csCache().cCache();
            Reservas.mostrar_detalle_reserva(rptReservaPlanes, rptReservaAereas, Request.QueryString["strLocalizador"], lblLocalizador);
        }
    }
   
}
