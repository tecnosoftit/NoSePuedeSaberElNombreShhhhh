﻿using System;
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

public partial class uc_ucMiCuenta : System.Web.UI.UserControl
{
    csBuscador csRefere = new csBuscador();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {   /*CARGA PARAMETROS DE BUSQUEDA*/

        }
        //clsSesiones
        //clsSesiones.setPantalleRespuestaLogin("../Presentacion/ReservaCircuito.aspx?Codigo=" + PageSource.Request.QueryString["id"] + "&TipoPlan=" + PageSource.Request.QueryString["TipoPlan"] + sDispAereo + "&ControlCupos=" + PageSource.Request.QueryString["ControlCupos"] + "&PaxControl=" + iPaxControl);
        //clsValidaciones.RedirectPagina("../Presentacion/Login.aspx");
    }

}
