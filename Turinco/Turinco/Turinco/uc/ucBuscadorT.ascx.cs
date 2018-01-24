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

public partial class uc_ucBuscador : System.Web.UI.UserControl
{
    csBuscador csRefere = new csBuscador();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {   /*CARGA PARAMETROS DE BUSQUEDA*/
            csRefere.setCargarGen(this);
        }
    }
    protected void ddlNinos_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void ddlActividad_SelectedIndexChanged1(object sender, EventArgs e)
    {
    }
    #region [HOTELES]
    protected void btnHotel_Click(object sender, EventArgs e)
    {
       
    }
    #endregion
    protected void setCommand(object sender, CommandEventArgs e)
    {
        csRefere.setCommand(this, sender, e);
    }
    protected void lbtnPaquetes_Click1(object sender, EventArgs e)
    {
        //csRefere.GuardarParametrosBusquedaCircular(this);
    }
}
