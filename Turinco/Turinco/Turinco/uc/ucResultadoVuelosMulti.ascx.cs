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
using Ssoft.Utils;

public partial class uc_ucResultadoVuelosMulti : System.Web.UI.UserControl
{
    csResultadoVuelos cRefere = new csResultadoVuelos();
    string paramertros = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Msjpop"] != null)
            {
                lblmensaje.Text = Request.QueryString["Msjpop"].ToString();
                MPEEAdicionales.Show();
            }


            cRefere.setCargarResultMulti19(this);

            if (clsSesiones.getParametrosAirBargain() != null)
            {

                if (clsSesiones.getParametrosAirBargain().ETipoTrayecto != null)
                {                    
                        paramertros = "Externo=1&";
                        paramertros += "modal_vuelos=2&";
                        paramertros += "TB=Buscar vuelos&";
                        paramertros += "ETIPOSALIDA=" + clsSesiones.getParametrosAirBargain().ETipoSalida + "&";
                        
                      

                        paramertros += "txt_Multi_O1=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo + " " + "&";
                        paramertros += "txt_Multi_D1=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[0].Vo_AeropuertoDestino.SCodigo + " " + "&";
                        paramertros += "txtFechaMultiO1=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[0].SFechaSalida + "&";

                        if (clsSesiones.getParametrosAirBargain().Lvo_Rutas.Count > 1)
                        {
                            paramertros += "txt_Multi_O2=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[1].Vo_AeropuertoOrigen.SCodigo + " " + "&";
                            paramertros += "txt_Multi_D2=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[1].Vo_AeropuertoDestino.SCodigo + " " + "&";
                            paramertros += "txtFechaMultiO2=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[1].SFechaSalida + "&";
                        }

                        if (clsSesiones.getParametrosAirBargain().Lvo_Rutas.Count > 2)
                        {
                            paramertros += "txt_Multi_O3=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[2].Vo_AeropuertoOrigen.SCodigo + " " + "&";
                            paramertros += "txt_Multi_D3=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[2].Vo_AeropuertoDestino.SCodigo + " " + "&";
                            paramertros += "txtFechaMultiO3=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[2].SFechaSalida + "&";
                        }

                        if (clsSesiones.getParametrosAirBargain().Lvo_Rutas.Count > 3)
                        {
                            paramertros += "txt_Multi_O4=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[3].Vo_AeropuertoOrigen.SCodigo + " " + "&";
                            paramertros += "txt_Multi_D4=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[3].Vo_AeropuertoDestino.SCodigo + " " + "&";
                            paramertros += "txtFechaMultiO4=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[3].SFechaSalida + "&";
                        }

                        if (clsSesiones.getParametrosAirBargain().Lvo_Rutas.Count > 4)
                        {
                            paramertros += "txt_Multi_O5=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[4].Vo_AeropuertoOrigen.SCodigo + " " + "&";
                            paramertros += "txt_Multi_D5=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[4].Vo_AeropuertoDestino.SCodigo + " " + "&";
                            paramertros += "txtFechaMultiO5=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[4].SFechaSalida + "&";
                        }

                        if (clsSesiones.getParametrosAirBargain().Lvo_Rutas.Count > 5)
                        {
                            paramertros += "txt_Multi_O6=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[5].Vo_AeropuertoOrigen.SCodigo + " " + "&";
                            paramertros += "txt_Multi_D6=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[5].Vo_AeropuertoDestino.SCodigo + " " + "&";
                            paramertros += "txtFechaMultiO5=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[5].SFechaSalida + "&";
                        }

                        paramertros += Pasajeros();

                    
                }
            }

            if (rptItinerario != null)
            {
                if (rptItinerario.Items.Count > 0)
                {
                    for (int i = 0; i < rptItinerario.Items.Count; i++)
                    {
                        double dblValores = 0;
                        Label lblValorSinImp = (Label)rptItinerario.Items[i].FindControl("lblValorSinImp");
                        Repeater RptTiposPasajeros = (Repeater)rptItinerario.Items[i].FindControl("RptTiposPasajeros");
                        if (RptTiposPasajeros != null)
                        {
                            if (RptTiposPasajeros.Items.Count > 0)
                            {
                                for (int b = 0; b < RptTiposPasajeros.Items.Count; b++)
                                {
                                    Label lblvalores = (Label)RptTiposPasajeros.Items[b].FindControl("lblValorSinImp");
                                    if (lblvalores != null)
                                    {
                                        if (lblvalores.Text != "")
                                            dblValores += Convert.ToDouble(lblvalores.Text);
                                    }
                                }
                            }

                        }
                        if (lblValorSinImp != null)
                            lblValorSinImp.Text = dblValores.ToString("###,###.##");
                    }
                }
            }

        }
    }
    protected void rptItinerario_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        cRefere.setItinerarioMulti(this, source, e);
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        cRefere.setOrdenar(this, sender, e);
    }
    protected void dtlAir_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        cRefere.setFiltrar(this, e);
    }
    protected string Pasajeros()
    {
        string Pasajeros = string.Empty;
        bool bValidaNiños = true;
        bool bValidaInf = true;

        for (int i = 0; i < clsSesiones.getParametrosAirBargain().Lvo_Pasajeros.Count; i++)
        {
            if (clsSesiones.getParametrosAirBargain().Lvo_Pasajeros[i].SDetalle.Trim().ToUpper().Contains("ADU"))
            {
                Pasajeros += "ddlMultiAdultos=" + clsSesiones.getParametrosAirBargain().Lvo_Pasajeros[i].SCantidad + " " + "&";
            }
            else if (clsSesiones.getParametrosAirBargain().Lvo_Pasajeros[i].SDetalle.Trim().ToUpper().Contains("NIÑO"))
            {
                Pasajeros += "ddlMultiNinios=" + clsSesiones.getParametrosAirBargain().Lvo_Pasajeros[i].SCantidad + " " + "&";
            }
            else if (clsSesiones.getParametrosAirBargain().Lvo_Pasajeros[i].SDetalle.Trim().ToUpper().Contains("INFAN"))
            {
                Pasajeros += "ddlMultiBebes=" + clsSesiones.getParametrosAirBargain().Lvo_Pasajeros[i].SCantidad + " " + "&";
            }

        }

         if(!bValidaNiños)
            Pasajeros += "ddlMultiNinios=0&";
         if (!bValidaInf)
             Pasajeros += "ddlMultiBebes=0&";

        return Pasajeros;
    }
}