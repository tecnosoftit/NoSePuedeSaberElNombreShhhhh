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

public partial class uc_ucReservasHistoricas : System.Web.UI.UserControl
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csListaReservas Reservas = new csListaReservas();
            clsCache cCache = new csCache().cCache();
            Reservas.mostrar_listado_reservas(rptHistoricas, cCache.Empresa, cCache.Contacto, "0", "0", false);
        }
    }


    protected void rptVigentes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.Equals("Ver"))
        {
            try
            {
                int indice = e.Item.ItemIndex;
                Repeater rptSender = (Repeater)source;
                Response.Redirect("./DetalleReserva.aspx?strLocalizador=" + e.CommandArgument.ToString() + "&intActiveTabIndex=1");
            }
            catch{}
        }
    }
}
