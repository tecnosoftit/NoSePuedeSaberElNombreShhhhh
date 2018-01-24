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
using Ssoft.DataNet;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Data.Common;
using Ssoft.Rules.General;

public partial class uc_ucResultadoVuelos : System.Web.UI.UserControl
{
    csResultadoVuelos cRefere = new csResultadoVuelos();
    DataSet dsSabreAir = null;
    Enum_TipoTrayecto Etipo;
    public static clsCache cCache = new csCache().cCache();
    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {
            if (clsSesiones.getParametrosAirBargain() != null)
            {
                Etipo = clsSesiones.getParametrosAirBargain().ETipoTrayecto;
                if (Request.QueryString["Msjpop"] != null)
                {
                    lblmensaje.Text = Request.QueryString["Msjpop"].ToString();
                    MPEEAdicionales.Show();
                }
                Stopwatch stopWatch = new Stopwatch();
                TimeSpan ts;
                stopWatch.Start();
                //Vraible de ssion para el filrtro se incializa
                Session["$DsFilter"] = null;
                Session["$DsAir"] = null;
                cRefere.setCargarResult(this);

                loadGranFilter();
                loadFilter();
                stopWatch.Stop();
                ts = stopWatch.Elapsed;
                this.lblTime.Text = "Sec" + ts.Seconds.ToString();

                if (rptItinerarioBFM.Items.Count > 0)
                {
                    for (int i = 0; i < rptItinerarioBFM.Items.Count; i++)
                    {

                        double dblValores = 0;
                        Label lblValorSinImp = (Label)rptItinerarioBFM.Items[i].FindControl("lblValorSinImp");
                        Label lblSequence = (Label)rptItinerarioBFM.Items[i].FindControl("lblSequence");
                        if (lblSequence != null)
                        {
                            lblSequence.Text = (i + 1).ToString();
                        }

                        Repeater RptTiposPasajeros = (Repeater)rptItinerarioBFM.Items[i].FindControl("RptTiposPasajeros");
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

                string paramertros = string.Empty;



                if (clsSesiones.getParametrosAirBargain() != null)
                {
                    string strDir = clsValidaciones.GetKeyOrAdd("urlpagtopflight", "162.248.52.117");
                    paramertros = strDir;
                    if (clsSesiones.getParametrosAirBargain().ETipoTrayecto != null)
                    {
                        if (clsSesiones.getParametrosAirBargain().ETipoTrayecto.ToString().Trim().ToUpper().Equals("IDA"))
                        {

                            paramertros += "?Externo=1&";
                            paramertros += "modal_vuelos=1&";
                            paramertros += "TB=Buscar vuelos&";
                            paramertros += "ETIPOSALIDA=" + clsSesiones.getParametrosAirBargain().ETipoSalida + "&";
                            paramertros += "txtFechaMultiO1=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[0].SFechaSalida + "&";
                            paramertros += "txt_Multi_O1=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo + " " + "&";
                            paramertros += "txt_Multi_D1=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[0].Vo_AeropuertoDestino.SCodigo + " " + "&";
                            paramertros += Pasajeros();

                        }
                        else if (clsSesiones.getParametrosAirBargain().ETipoTrayecto.ToString().Trim().ToUpper().Equals("IDAREGRESO"))
                        {
                            paramertros += "?Externo=1&";
                            paramertros += "modal_vuelos=0&";
                            paramertros += "TB=Buscar vuelos&";
                            paramertros += "ETIPOSALIDA=" + clsSesiones.getParametrosAirBargain().ETipoSalida + "&";
                            paramertros += "txtFechaMultiO1=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[0].SFechaSalida + "&";
                            paramertros += "txt2VFechaMulti=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[1].SFechaSalida + "&";
                            paramertros += "txt_Multi_O1=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo + " " + "&";
                            paramertros += "txt_Multi_D1=" + clsSesiones.getParametrosAirBargain().Lvo_Rutas[0].Vo_AeropuertoDestino.SCodigo + " " + "&";
                            paramertros += Pasajeros();

                        }


                    }
                }


                lbltopflight.Text = paramertros;
                
            }
        }
    }
    /// <summary>
    /// Validate if  existe option selected
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void rptItinerario_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Repeater RptSegmentosIda = null;
        Repeater RptSegmentosReg = null;

        RepeaterItem rptItinerarioItem = e.Item;
        bool bOk = false;
        bool breturn = false;

        if (clsSesiones.getParametrosAirBargain().ETipoTrayecto == Enum_TipoTrayecto.Ida)
        {
            RptSegmentosIda = e.Item.FindControl("RptSegmentosIda") as Repeater;
            bOk = RadioSelected(RptSegmentosIda);

        }
        if (clsSesiones.getParametrosAirBargain().ETipoTrayecto == Enum_TipoTrayecto.IdaRegreso)
        {

            StringBuilder MyStringBuilder = new StringBuilder();
           
            RptSegmentosIda = e.Item.FindControl("RptSegmentosIda") as Repeater;
            RptSegmentosReg = e.Item.FindControl("RptSegmentosReg") as Repeater;
            bOk = RadioSelected(RptSegmentosIda);
            breturn = RadioSelected(RptSegmentosReg);

           
            if (!bOk)
            {
                bOk = false;
                if (!breturn) MyStringBuilder.Append("Por favor seleccione vuelo de ida y vuelta");
                if (breturn) MyStringBuilder.Append("Por favor seleccione vuelo de ida");

            }
            else
            {
                if (!breturn)
                {
                    MyStringBuilder.Append("Por favor seleccione vuelo de vuelta");
                    bOk = false;
                }

            }
            lblMsg.Text = MyStringBuilder.ToString();
        }

        lblMsg.Visible = true;
        if (bOk)
        {
            lblMsg.Visible = false;
            cRefere.setItinerario(this, source, e);

            DataTable tblDispo = (DataTable)Session["$Tbl_SEgmentos_No_Disp"];
            rptDispo.DataSource = tblDispo;
            rptDispo.DataBind();
            MPEEDisponibilidad.Show();
            Session["$Tbl_SEgmentos_No_Disp"] = null;
        }
        else
        {
            mdodalvalida.Show();
          
        }

    }
    /// <summary>
    /// Get value from is true
    /// </summary>
    /// <param name="Dida"></param>
    /// <param name="Dvuelta"></param>
    /// <returns></returns>
    private bool RadioSelected(Repeater Dida = null, Repeater Dvuelta = null)
    {
        bool bCorrect = false;

        RadioButton radioButton = null;
        if (Dida != null)
        {
            foreach (  RepeaterItem i in Dida.Items)
            {
                radioButton = (RadioButton)i.FindControl("rbtnSel");
                if (radioButton != null && radioButton.Checked)
                {
                    bCorrect = true;
                    break;
                }
            }
        }

        if (Dvuelta != null && bCorrect == true)
        {
            bCorrect = false;
            foreach (RepeaterItem i in Dvuelta.Items)
            {
                radioButton = (RadioButton)i.FindControl("rbtnSel");
                if (radioButton != null && radioButton.Checked)
                {
                    bCorrect = true;
                    break;
                }
            }
        }

        return bCorrect;
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        cRefere.setOrdenar(this, sender, e);
    }
    protected void dtlAir_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        cRefere.setFiltrar(this, e);
    }
    /// <summary>
    /// Load Hour &&minuts
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RptSegmentosReg_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //clsCache cCache = new csCache().cCache();
        //if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

        DataTable dtNewvalue = null;
        DataRowView drv = (DataRowView)(e.Item.DataItem);
        string IdFly = drv.Row["FlightNumber"].ToString();
        TimeSpan dHour;
        RadioButton rb = (RadioButton)e.Item.FindControl("rbtnSel");
        string script = "SetUniqueRadioButton('RptSegmentosReg.*', '" + rb.ClientID + "')";
        rb.Attributes.Add("onclick", script);

        var lblOdeId = ((Label)e.Item.FindControl("lblOdeId")).Text;
        HtmlTable tblIda = ((HtmlTable)e.Item.FindControl("tblVuelta"));

        //Hide tbale when exist in teh current datalist
        //NoShowSegmnet(sender, IdFly, tblIda);
        try
        {
            dtNewvalue = getRealArrival(lblOdeId.ToString(), "R");
        }
        catch { }
        //if (IdFly.Equals("2582"))
        //{
        //    int i = 9;
        //}
        int IndicadorFila = 0;
        for (int i = 0; i < dtNewvalue.Rows.Count; i++)
        {
            if (dtNewvalue.Rows[i]["strDepartureAirport"].ToString().Equals(cCache.AeropuertoDestino.SCodigo))
                IndicadorFila = i;
        }

        ((Label)e.Item.FindControl("lblTimeFly")).Text = ((Label)e.Item.FindControl("lblTimeFly")).Text + "Min";
        if (dtNewvalue != null && dtNewvalue.Rows.Count > 1)
        {
            ((Label)e.Item.FindControl("lblCiudadLlegada")).Text = dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["strCiudad_LLegada"].ToString();
            ((LinkButton)e.Item.FindControl("lblHeder")).Text = (dtNewvalue.Rows.Count - 1).ToString() + " Paradas";//dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["strParadas"].ToString() + "Paradas";
            ((Label)e.Item.FindControl("lblHourArrival")).Text = Convert.ToDateTime(dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["dtmFechaLlegada"]).ToString("HH:mm:ss");
            ((Label)e.Item.FindControl("lblCiudadLlegadaCod")).Text = dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["strArrivalAirport"].ToString();

            dHour = Convert.ToDateTime((dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["dtmFechaLlegada"])).Subtract(Convert.ToDateTime(((Label)e.Item.FindControl("lblHourTotal")).Text));

            ((Label)e.Item.FindControl("lblTimeFly")).Text = dHour.Minutes.ToString() + " Min";
            if (dHour.TotalMinutes >= 60)
            {
                int iHora = Convert.ToInt16((dHour.TotalHours));
                int iMinuts = dHour.Minutes;
                //INT iMinuts = Math.Round(iHora - (Convert.ToInt32(iHora)),2);
                ((Label)e.Item.FindControl("lblTimeFly")).Text = iHora.ToString() + " Hr " + iMinuts.ToString() + " Min";
            }
        }
        else
        {
            dHour = Convert.ToDateTime((dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["dtmFechaLlegada"])).Subtract(Convert.ToDateTime(((Label)e.Item.FindControl("lblHourTotal")).Text));

            ((Label)e.Item.FindControl("lblTimeFly")).Text = dHour.Minutes.ToString() + " Min";
            if (dHour.TotalMinutes >= 60)
            {
                int iHora = (dHour.Hours);
                int iMinuts = dHour.Minutes;
                //INT iMinuts = Math.Round(iHora - (Convert.ToInt32(iHora)),2);
                ((Label)e.Item.FindControl("lblTimeFly")).Text = iHora.ToString() + " Hr " + iMinuts.ToString() + " Min";
            }
            try
            {
                if (Convert.ToInt16(((LinkButton)e.Item.FindControl("lblHeder")).Text) > 0) ((LinkButton)e.Item.FindControl("lblHeder")).Text = ((LinkButton)e.Item.FindControl("lblHeder")).Text + " Parada";
            }
            catch { }
        }
        //Hide tbale when exist in teh current datalist
        string HoraSalida = Convert.ToDateTime(drv.Row["dtmFechaSalida"]).ToString("HH:mm:ss");
        try
        {
            NoShowSegmnet(sender, IdFly, tblIda, HoraSalida, ((Label)e.Item.FindControl("lblHourArrival")).Text, false, dtNewvalue.Rows[IndicadorFila]["strDepartureAirport"].ToString());
        }
        catch { }
    }
    /// <summary>
    /// Load Hour &&minuts
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RptSegmentosIda_ItemDataBound(object sender,RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

        //clsCache cCache = new csCache().cCache();
        DataTable dtNewvalue = null;
        DataRowView drv = (DataRowView)(e.Item.DataItem);
        string IdFly = drv.Row["FlightNumber"].ToString();
        TimeSpan dHour;

        List<clsTotal> ListTotal = new List<clsTotal>();
        clsTotal objTota = new clsTotal();
        RadioButton rb = (RadioButton)e.Item.FindControl("rbtnSel");
        string script = "SetUniqueRadioButton('RptSegmentosIda.*', '" + rb.ClientID + "')";
        rb.Attributes.Add("onclick", script);
        var lblOdeId = ((Label)e.Item.FindControl("lblOdeId")).Text;

        HtmlTable tblIda = ((HtmlTable)e.Item.FindControl("tblIda"));

        //if (IdFly.Equals("14"))
        //{
        //    int i = 0;
        //}

        if (Session["$DsFilter"] != null)
        {
            ListTotal = (List<clsTotal>)Session["$DsFilter"];
        }

        ////Hide tbale when exist in teh current datalist
        //NoShowSegmnet(sender, IdFly, tblIda);
        string sIdaVuleta = "I";
        if (Etipo == Enum_TipoTrayecto.Ida) sIdaVuleta = "R";
            dtNewvalue = getRealArrival(lblOdeId.ToString(), sIdaVuleta);

        int IndicadorFila = 0;
        for (int i = 0; i < dtNewvalue.Rows.Count; i++)
        {
            if (dtNewvalue.Rows[i]["strDepartureAirport"].ToString().Equals(cCache.AeropuertoOrigen.SCodigo))
                IndicadorFila = i;
        }

        ((Label)e.Item.FindControl("lblTimeFly")).Text = ((Label)e.Item.FindControl("lblTimeFly")).Text + "Min";
        if (dtNewvalue != null && dtNewvalue.Rows.Count > 1)
        {

            ((Label)e.Item.FindControl("lblCiudadLlegada")).Text = dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["strCiudad_LLegada"].ToString();
            ((LinkButton)e.Item.FindControl("lblHederIda")).Text = (dtNewvalue.Rows.Count - 1).ToString() + " Paradas";//dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["strParadas"].ToString() + "Paradas";
            ((Label)e.Item.FindControl("lblHourArrival")).Text = Convert.ToDateTime(dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["dtmFechaLlegada"]).ToString("HH:mm:ss");
            ((Label)e.Item.FindControl("lblCiudadLlegadaCod")).Text = dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["strArrivalAirport"].ToString();

            //Diferncia total en horas del trayecto
            dHour = Convert.ToDateTime((dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["dtmFechaLlegada"])).Subtract(Convert.ToDateTime(((Label)e.Item.FindControl("lblHourTotal")).Text));

            ((Label)e.Item.FindControl("lblTimeFly")).Text = dHour.Minutes.ToString() + " Min";
            if (dHour.TotalMinutes >= 60)
            {
                int iHora = Convert.ToInt16((dHour.TotalHours));
                int iMinuts = dHour.Minutes;
                //INT iMinuts = Math.Round(iHora - (Convert.ToInt32(iHora)),2);
                ((Label)e.Item.FindControl("lblTimeFly")).Text = iHora.ToString() + " Hr " + iMinuts.ToString() + " Min";
            }
            objTota.StopQuantity = (dtNewvalue.Rows.Count - 1).ToString();
            objTota.strMarketingAirline = dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["strMarketingAirline"].ToString();
            objTota.strNombre_Aerolinea = dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["strNombre_Aerolinea"].ToString();
            objTota.urlImagenAerolinea = dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["urlImagenAerolinea"].ToString();
            objTota.intPrecioDesde = this.getTotal(lblOdeId);
        }
        else
        {

            dHour = Convert.ToDateTime((dtNewvalue.Rows[dtNewvalue.Rows.Count - 1]["dtmFechaLlegada"])).Subtract(Convert.ToDateTime(((Label)e.Item.FindControl("lblHourTotal")).Text));
            ((Label)e.Item.FindControl("lblTimeFly")).Text = dHour.Minutes.ToString() + " Min";
            if (dHour.TotalMinutes >= 60)
            {
                int iHora = Convert.ToInt16((dHour.TotalHours));
                int iMinuts = dHour.Minutes;
                //INT iMinuts = Math.Round(iHora - (Convert.ToInt32(iHora)),2);
                ((Label)e.Item.FindControl("lblTimeFly")).Text = iHora.ToString() + " Hr " + iMinuts.ToString() + " Min";
            }
            try
            {
                if (Convert.ToInt16(((LinkButton)e.Item.FindControl("lblHederIda")).Text) > 0) ((LinkButton)e.Item.FindControl("lblHederIda")).Text = ((LinkButton)e.Item.FindControl("lblHederIda")).Text + " Parada";
            }
            catch { }


            objTota.StopQuantity = "0";
            objTota.strMarketingAirline = ((Label)e.Item.FindControl("lblMarketingAirline")).Text;
            objTota.strNombre_Aerolinea = ((Label)e.Item.FindControl("lblNameAir")).Text;
            objTota.urlImagenAerolinea = ((Image)e.Item.FindControl("ImgAir")).ImageUrl;
            objTota.intPrecioDesde = this.getTotal(lblOdeId);
        }


        if (
            (ListTotal.Exists(delegate(clsTotal p) { return (p.strMarketingAirline == objTota.strMarketingAirline && objTota.intPrecioDesde < p.intPrecioDesde); }))
            || (!ListTotal.Exists(delegate(clsTotal p) { return (p.strMarketingAirline == objTota.strMarketingAirline); }))
             || (ListTotal.Count == 0)
            )
        {
            ListTotal.Add(objTota);
            ListTotal.RemoveAll(delegate(clsTotal p) { return (p.strMarketingAirline == objTota.strMarketingAirline && p.intPrecioDesde > objTota.intPrecioDesde); });
        }
        //Cuando es la primera vez asigna el valor
        if (Session["$DsFilter"] == null)
        {
            Session["$DsFilter"] = ListTotal;
        }
        ////Hide tbale when exist in teh current datalist
        string HoraSalida = Convert.ToDateTime(drv.Row["dtmFechaSalida"]).ToString("HH:mm:ss");
        try
        {
            NoShowSegmnet(sender, IdFly, tblIda, HoraSalida, ((Label)e.Item.FindControl("lblHourArrival")).Text, true, dtNewvalue.Rows[IndicadorFila]["strDepartureAirport"].ToString());
        }
        catch { }
    } 
    /// <summary>
    /// Load in runtime the Filter
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dtlFilter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            clsTotal dbr = (clsTotal)e.Item.DataItem;
            if (dbr.StopQuantity == "0")
            {                
                ((Label)e.Item.FindControl("lblParadas")).Text = Convert.ToDecimal(dbr.intPrecioDesde).ToString("###,###.##");
                ((Label)e.Item.FindControl("lblParada1")).Text = "--";
                ((Label)e.Item.FindControl("lblParada2")).Text = "--";
            }
            if (dbr.StopQuantity == "1")
            {
                ((Label)e.Item.FindControl("lblParada1")).Text = Convert.ToDecimal(dbr.intPrecioDesde).ToString("###,###.##");
                ((Label)e.Item.FindControl("lblParadas")).Text ="--";
                ((Label)e.Item.FindControl("lblParada2")).Text = "--";
            }
            if ( Convert.ToInt32(dbr.StopQuantity) >1)
            {
                ((Label)e.Item.FindControl("lblParada2")).Text = Convert.ToDecimal(dbr.intPrecioDesde).ToString("###,###.##");
                ((Label)e.Item.FindControl("lblParadas")).Text = "--";
                ((Label)e.Item.FindControl("lblParada1")).Text = "--";
            }
        } 
    }
    private void loadGranFilter()
    {
        List<clsTotal> lstFilter = (List<clsTotal>)Session["$DsFilter"];


        this.dsFilter.DataSource = lstFilter;
        dsFilter.DataBind();

    }
    //ordenamiento
    protected void lstOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        cRefere.setOrdenarDDlist(this, sender, e);
    }
    private void loadFilter()
    {
      
         List<clsTotal> dtList= (List<clsTotal>)this.dsFilter.DataSource;

        CheckBoxList chk = (CheckBoxList)this.Page.FindControl("ucFiltros").FindControl("chkaerolinea");
        if (dtList != null)
        {
            chk.Items.Add(new ListItem("Todas", "0"));
            foreach (clsTotal i in dtList)
            {
                chk.Items.Add(new ListItem(string.Format("<img src='{0}' alt='' />",  i.urlImagenAerolinea), i.strMarketingAirline));
            }
          

           
        }

    }
    protected void lnkCustDetails_Click(Object sender, CommandEventArgs e)
    {
        // Fetch the customer id
        cRefere.setLoadShowModal(this.Parent, e.CommandArgument.ToString());
        // ModalPopupExtender1.Show();

    }
    /// <summary>
    /// get the las segmente to fioll the  correct data
    /// hceron 29.01.2013
    /// </summary>
    /// <param name="sIdOrgin"></param>
    /// <returns></returns>
    private DataTable getRealArrival(string sIdOrgin, string ITrayecto = "I")
    {

        if (dsSabreAir == null)
        {
            dsSabreAir = clsSesiones.GetDatasetSabreAir();
            Session["$DsAir"] = dsSabreAir;
        }
        DataTable dtPricedItineraryFilter = dsSabreAir.Tables["FlightSegment"];
        DataTable dtPricedItinerary;

    //    string sWhere = "MarriageGrp='" + IMarriageGrp + "' AND OriginDestinationOption_Id=" + sIdOrgin;
        string sWhere = "strTrayecto='" + ITrayecto + "' AND OriginDestinationOption_Id=" + sIdOrgin;
        if (ITrayecto.Equals("N"))
        {
            sWhere = "  OriginDestinationOption_Id=" + sIdOrgin;
        }
        //  string sWhere = "strMarketingAirline";
        dtPricedItinerary = clsDataNet.dsDataWhere(sWhere, dtPricedItineraryFilter.DefaultView.ToTable());

        return dtPricedItinerary;
    }
    private decimal getTotal(string sIdOrgin)
    {

        if (dsSabreAir == null)
        {
            dsSabreAir = clsSesiones.GetDatasetSabreAir();
            Session["$DsAir"] = dsSabreAir;
        }
        DataTable OriginDestinationOption = dsSabreAir.Tables["OriginDestinationOption"];
        string sAirItinerary = string.Empty;


        string sWhere = "  OriginDestinationOption_Id=" + sIdOrgin;

        //Obtenemos el Id del OriginDestinationOption_Id
        DataTable dtOriginDest = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["OriginDestinationOption"]);
        string OriginIds = dtOriginDest.Rows[0]["OriginDestinationOptions_Id"].ToString();

        sWhere = "  OriginDestinationOptions_Id=" + OriginIds;
        DataTable dtOriginsDest = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["OriginDestinationOptions"]);
        sAirItinerary = dtOriginsDest.Rows[0]["AirItinerary_Id"].ToString();

        sWhere = "AirItinerary_Id=" + OriginIds;
        DataTable dtAirItinerary = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["AirItinerary"]);
        string idpriced = dtAirItinerary.Rows[0]["PricedItinerary_Id"].ToString();

        sWhere = "PricedItinerary_Id = " + idpriced.ToString();
        DataTable dtPricedItinerary = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["PricedItinerary"]);

        decimal Total = Convert.ToDecimal(dtPricedItinerary.Rows[0]["IntTotalPesos"]);





        return Total;
    }
    /// <summary>
    /// Concat values to Popup detail from gridview 
    /// </summary>
    /// <param name="objText1"></param>
    /// <param name="objText2"></param>
    /// <returns></returns>
    public  string ConcatColumns(object objText1, object objText2, string sIdaVuleta)
    {
        if (Etipo == Enum_TipoTrayecto.Ida) sIdaVuleta = "R";

        return objText1.ToString() + "|" + objText2.ToString() + "|" + sIdaVuleta;
    }
    /// <summary>
    /// Hide the row bcz exist in.
    /// </summary>
    /// <param name="e"></param>
    /// <param name="sFlightNumber"></param>
    /// <param name="hTable"></param>
    private void NoShowSegmnet(object sender, string sFlightNumber, HtmlTable hTable, string HoraSalida, string HoraLlegada,
        bool Ida, string sAeropuertoOrigen)
    {
        //clsCache cCache = new csCache().cCache();
        try
        {
            clsIATAVirtual csIata = new clsIATAVirtual();
            csIata = csIata.sObtenerIataVirtual(cCache.AeropuertoOrigen.SCodigo, cCache.AeropuertoDestino.SCodigo);
            if (Ida)
            {
                bool bEncontrado = false;
                string[] sAeropuertos = csIata.sIda.Split(',');
                for (int a = 0; a < sAeropuertos.Length; a++)
                {
                    if (cCache.AeropuertoOrigen.SCodigo == sAeropuertos[a])
                    {
                        bEncontrado = true;
                    }
                }
                if (!bEncontrado)
                {
                    hTable.Visible = false;
                }
                else
                {
                    for (int i = 0; i < ((Repeater)sender).Items.Count; i++)
                    {
                        Label lbl = (Label)((Repeater)sender).Items[i].FindControl("lblFly");
                        Label lblHoraSalida = (Label)((Repeater)sender).Items[i].FindControl("lblHourDeparture");
                        Label lblHoraLlegada = (Label)((Repeater)sender).Items[i].FindControl("lblHourArrival");

                        if (lbl.Text.Equals(sFlightNumber) && lblHoraSalida.Text.Equals(HoraSalida) && lblHoraLlegada.Text.Equals(HoraLlegada))
                            hTable.Visible = false;
                    }
                }
            }
            else
            {
                bool bEncontrado = false;
                string[] sAeropuertos = csIata.sIda.Split(',');
                for (int a = 0; a < sAeropuertos.Length; a++)
                {
                    if (cCache.AeropuertoOrigen.SCodigo == sAeropuertos[a])
                    {
                        bEncontrado = true;
                    }
                }
                if (!bEncontrado)
                {
                    hTable.Visible = false;
                }
                else
                {
                    for (int i = 0; i < ((Repeater)sender).Items.Count; i++)
                    {
                        Label lbl = (Label)((Repeater)sender).Items[i].FindControl("lblFly");
                        Label lblHoraSalida = (Label)((Repeater)sender).Items[i].FindControl("lblHourDeparture");
                        Label lblHoraLlegada = (Label)((Repeater)sender).Items[i].FindControl("lblHourArrival");

                        if (lbl.Text.Equals(sFlightNumber) && lblHoraSalida.Text.Equals(HoraSalida) && lblHoraLlegada.Text.Equals(HoraLlegada))
                            hTable.Visible = false;
                    }
                }
            }
        }
        catch { }
    }
    protected void FilteHeaderAir_Command(object sender, CommandEventArgs e)
    {

        string sQuery = "strMarketingAirline='" + e.CommandArgument.ToString() + "'";
        cRefere.setOtrosFiltrosAerolineaBFM(this.Parent, sQuery, this);
    }
    protected void Page_PreRender(Object o, EventArgs e)
    {
        if (Session["$DsAir"] == null) Response.Redirect("../Presentacion/index.aspx");
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
                Pasajeros += "ddlMultiAdultos=" + clsSesiones.getParametrosAirBargain().Lvo_Pasajeros[i].SCantidad + "" + "&";
            }
            else if (clsSesiones.getParametrosAirBargain().Lvo_Pasajeros[i].SDetalle.Trim().ToUpper().Contains("NIÑO"))
            {
                Pasajeros += "ddlMultiNinios=" + clsSesiones.getParametrosAirBargain().Lvo_Pasajeros[i].SCantidad + "" + "&";
                bValidaNiños = false;
            }
            else if (clsSesiones.getParametrosAirBargain().Lvo_Pasajeros[i].SDetalle.Trim().ToUpper().Contains("INFAN"))
            {
                Pasajeros += "ddlMultiBebes=" + clsSesiones.getParametrosAirBargain().Lvo_Pasajeros[i].SCantidad + "" + "&";
                bValidaInf = false;
            }

        }

        if(bValidaNiños)
            Pasajeros += "ddlMultiNinios=0&";
        if (bValidaInf)
            Pasajeros += "ddlMultiBebes=0&";

        return Pasajeros;
    }

    
}