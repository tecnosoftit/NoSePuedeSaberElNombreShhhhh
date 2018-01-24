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
using Ssoft.ValueObjects;
using Ssoft.Pages.PaginaPlanes;

public partial class uc_ucTarifasCircuitosMulti : System.Web.UI.UserControl
{
    csTarifasPlanes cRefere = new csTarifasPlanes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {   /*CARGA PARAMETROS DE BUSQUEDA*/
            cRefere.setValidarCargueCircuitosMulti(this);
        }
    }
    protected void ddlAnioSalida_SelectedIndexChanged(object sender, EventArgs e)
    {
        cRefere.setLlenarParametrosSalidas(this, sender);
    }
    protected void ddlMesSalida_SelectedIndexChanged(object sender, EventArgs e)
    {
        cRefere.setLlenarParametrosSalidas(this, sender);
    }
    protected void ddlCabinas_SelectedIndexChanged(object sender, EventArgs e)
    {
        cRefere.setLlenarRepetidorPax(this);
    }
    protected void btn_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Reservar")
        {
            if (cRefere.setValidarSeleccionCabina(this))
            {
                Response.Write("<script>");
                Response.Write("Show_Cortinilla_Interna();");
                Response.Write("</script>");
                cRefere.setCommand(this, sender, e);
            }
            else
            {
                string sTextoTipo = "";
                if (Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")))
                    sTextoTipo = "habitación";
                else
                    sTextoTipo = "cabina";

                if (Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")))
                    lblMensajeValida.Text = "Por favor seleccione una tarifa cotizada";
                else
                    lblMensajeValida.Text = "Por favor seleccione una tarifa para cada " + sTextoTipo + " cotizada";

                mdodalvalida.Show();
            }
        }
        else
        {
            if (ddlAnioSalida.SelectedValue.Equals("") || ddlAnioSalida.SelectedValue.Equals("0") ||
                ddlMesSalida.SelectedValue.Equals("") || ddlMesSalida.SelectedValue.Equals("0") ||
                ddlDiaSalida.SelectedValue.Equals("") || ddlDiaSalida.SelectedValue.Equals("0"))
            {
                lblMensajeValida.Text = "Por favor seleccione los parámetros para la salida";
                mdodalvalida.Show();
            }
            else
            {
                cRefere.setCommand(this, sender, e);
            }
        }
    }
    protected void btn_Command1(object sender, CommandEventArgs e)
    {
        cRefere.setCommand(this, sender, e);
    }
    protected void rbTarifa_CheckedChanged(object sender, EventArgs e)
    {
        cRefere.setValidarTarifaSeleccionadaCircuitos(this, sender, e);
    }

    protected void ddlNinos_Selected(object sender, EventArgs e)
    {
        cRefere.setMostrarEdadesNinos(this, sender);
    }
}
