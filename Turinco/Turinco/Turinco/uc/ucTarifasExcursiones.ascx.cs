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
using Ssoft.Utils;
using System.Text;
using Ssoft.Pages;
using Ssoft.ValueObjects;
using Ssoft.Pages.PaginaPlanes;

public partial class uc_ucTarifasExcursiones : System.Web.UI.UserControl
{
    csTarifasPlanes cRefere = new csTarifasPlanes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {   /*CARGA PARAMETROS DE BUSQUEDA*/
            cRefere.setValidarCargueToures(this);
        }
    }    
    protected void ddlCabinas_SelectedIndexChanged(object sender, EventArgs e)
    {
        cRefere.setLlenarRepetidorPax(this);
    }
    protected void btn_Command(object sender, CommandEventArgs e)
    {
        cRefere.setCommand(this, sender, e);
    }
    
    protected void rbTarifa_CheckedChanged(object sender, EventArgs e)
    {
        cRefere.setValidarTarifaSeleccionadaCircuitos(this, sender, e);
    }
    
}
