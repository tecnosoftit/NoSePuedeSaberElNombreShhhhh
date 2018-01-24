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
using Ssoft.DataNet;
using Ssoft.Pages;
using System.Text;
using Ssoft.ManejadorExcepciones;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Data.Common;


public partial class uc_ucReservaVuelosTarjeta2 : System.Web.UI.UserControl
{
   
    csResultadoVuelos csRefere = new csResultadoVuelos();
  
    Enum_TipoTrayecto Etipo;
    csResultadoVuelos cRefere = new csResultadoVuelos();
    DataSet dsSabreAir = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Etipo = clsSesiones.getParametrosAirBargain().ETipoTrayecto;
            csRefere.setCargar(this);
            //Carrito.LlenarrblFormasPago(this);
        }
    }

    protected void btnReserva_Command(object sender, CommandEventArgs e)
    {
        if (((Button)sender).CommandName.Equals("Confirmar"))
        {

            if (!Validacampos()) return;

            if (!((CheckBox)rptItinerarioBFM.Controls[rptItinerarioBFM.Controls.Count - 1].FindControl("cbAcepto")).Checked)
            {
                lblError.Text = "Por favor acepte los terminos y condicones";
                return;
            }

            //clsParametros Registro = csReferelogin.setCrearNoRegistro(this, ucRegistro, Enum_Login.LoginGen, false);
            //if (Registro.Id != 0)
            //    csRefere.setCommand(this, sender, e);
            //else
            //lblError.Text = "La reserva no pudo ser generada";
        }
        else
        {
            csRefere.setCommand(this, sender, e);
        }
    }
    private bool Validacampos()
    {
        bool validafecha = true;

          //TextBox txtCelular=null;
          //txtCelular = (TextBox)FindControl("ucRegistro").FindControl("txtCelular");
          //TextBox txtCiudad = null;
      
          //txtCiudad = (TextBox)FindControl("ucRegistro").FindControl("txtCiudad");
          //if (txtCelular.Text == string.Empty || txtCiudad.Text==string.Empty)
          //{
          //    return false;
          //}


        for (int c = 0; c < dtlPasajeros.Items.Count; c++)
        {

            TextBox txtNombre = (TextBox)dtlPasajeros.Items[c].FindControl("txtNombre1");
            TextBox txtApellido = (TextBox)dtlPasajeros.Items[c].FindControl("txtApellido1");
            TextBox txtPasaporte = (TextBox)dtlPasajeros.Items[c].FindControl("txtDocumento1");

            TextBox txtDireccion = (TextBox)ucRegistro.FindControl("txtDireccion");
            TextBox txtCiudad = (TextBox)ucRegistro.FindControl("txtCiudad");
            TextBox txtMail = (TextBox)ucRegistro.FindControl("txtMailPersonal");
            TextBox txtCelular = (TextBox)ucRegistro.FindControl("txtCelular");

            //if (ddlAño.SelectedItem.Value.ToString() != "0" && ddlMes.SelectedItem.Value.ToString() != "0" && ddlDia.SelectedItem.Value.ToString() != "0")
            //{
            //    int days = DateTime.DaysInMonth(int.Parse(ddlAño.SelectedItem.Value), int.Parse(ddlMes.SelectedItem.Value));
            //    if (int.Parse(ddlDia.SelectedItem.Value) > days)
            //    {
            //        validafecha = true;
            //        lblError.Text = "Por favor verifique su Fecha de nacimiento, el dia de su fecha de nacimiento no es valido ";
            //    }
            //}
            //else
            //{
            //    lblError.Text = "Por favor Diligencie su Fecha de nacimiento";
            //    validafecha = true;

            //}
            //txtCiudad = (TextBox)FindControl("ucRegistro").FindControl("txtCiudad");
          
                if (txtNombre.Text == "" || txtApellido.Text == "" || txtPasaporte.Text == ""||txtCelular.Text == string.Empty || txtCiudad.Text == string.Empty )
                {
                  
                    lblError.Text = "Por favor Diligencie todos los campos marcados con (*) ";
                    return false;
                }


          



        }

        return validafecha;

    }
   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void hlkSabre_Click(object sender, EventArgs e)
    {
        csRefere.setValidaCondiciones(this);
    }

    protected void Page_PreRender(Object o, EventArgs e)
    {
        if (clsSesiones.GetDatasetSabreAir() == null) Response.Redirect("../Presentacion/index.aspx");
    }

    protected void rblFormasPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblError.Text = "";
        clsIdioma cIdioma = new clsIdioma();
        cIdioma.LoadIdioma(csGeneralsPag.PaginaActual(), this);
        if (rblFormasPago.SelectedIndex == 0)
        {
            //btnPagar.Visible = true;
            //btnGuardar.Visible = false;
            ((HtmlGenericControl)upFormasPago.Controls[0].FindControl("DivTC")).Visible = true;
            ((HtmlGenericControl)upFormasPago.Controls[0].FindControl("DivOtros")).Visible = false;            
        }      
        else
        {
            ((HtmlGenericControl)upFormasPago.Controls[0].FindControl("DivTC")).Visible = false;
            ((HtmlGenericControl)upFormasPago.Controls[0].FindControl("DivOtros")).Visible = true;            
            //Carrito.setCargarTextoFormaPago(rblFormasPago.SelectedItem.Value, this);
            if (rblFormasPago.SelectedItem.Value.Equals(clsValidaciones.GetKeyOrAdd("Efectivo", "EFE")))
            {
                //btnPagar.Visible = false;
                //btnGuardar.Visible = true;
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
    /// Concat values to Popup detail from gridview 
    /// </summary>
    /// <param name="objText1"></param>
    /// <param name="objText2"></param>
    /// <returns></returns>
    public string ConcatColumns(object objText1, object objText2, string sIdaVuleta)
    {
        if (Etipo == Enum_TipoTrayecto.Ida) sIdaVuleta = "R";

        return objText1.ToString() + "|" + objText2.ToString() + "|" + sIdaVuleta;
    }

    /// <summary>
    /// Load Hour &&minuts
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RptSegmentosIda_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

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
        NoShowSegmnet(sender, IdFly, tblIda);
        string sIdaVuleta = "I";
        if (Etipo == Enum_TipoTrayecto.Ida) sIdaVuleta = "R";
        dtNewvalue = getRealArrival(lblOdeId.ToString(), sIdaVuleta);



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
    /// Hide the row bcz exist in.
    /// </summary>
    /// <param name="e"></param>
    /// <param name="sFlightNumber"></param>
    /// <param name="hTable"></param>
    private void NoShowSegmnet(object sender, string sFlightNumber, HtmlTable hTable)
    {
        for (int i = 0; i < ((DataList)sender).Items.Count; i++)
        {
            Label lbl = (Label)((DataList)sender).Items[i].FindControl("lblFly");
            if (lbl.Text.Equals(sFlightNumber))
            {
                hTable.Visible = false;
            }
        }

    }

    /// <summary>
    /// Load Hour &&minuts
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RptSegmentosReg_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

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
        NoShowSegmnet(sender, IdFly, tblIda);
        dtNewvalue = getRealArrival(lblOdeId.ToString(), "R");
        //if (IdFly.Equals("2582"))
        //{
        //    int i = 9;
        //}

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
    }

    /// <summary>
    /// Validate if  existe option selected
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void rptItinerario_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //DataList dtlSegmentosIda = (DataList)(this.rptItinerarioBFM.FindControl("dtlSegmentosIda"));
        //

        Repeater RptSegmentosIda = null;
        DataList RptSegmentosReg = null;

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
            //foreach (RepeaterItem ri in from RepeaterItem ri in rptItinerarioItem let tr = (DataList)ri.FindControl("dtlSegmentosIda") where RadioSelected(tr) == true select ri)
            //{
            //RptSegmentosIda = e.Item.FindControl("RptSegmentosIda") as Repeater;
            //RptSegmentosReg = e.Item.FindControl("RptSegmentosReg") as Repeater;
            //bOk = RadioSelected(RptSegmentosIda);
            //breturn = RadioSelected(RptSegmentosReg);

            //foreach (RepeaterItem ri in rptItinerarioItem let tr = (DataList)ri.FindControl("dtlSegmentosIda") where  == true select ri)
            //{
            //    bOk = true;
            //    break;
            //}
            //
            //foreach (RepeaterItem ri in from RepeaterItem ri in rptItinerarioBFM.Items let tr = (DataList)ri.FindControl("dtlSegmentosReg") where RadioSelected(tr) == true select ri)
            //{
            //    breturn = true;
            //    break;
            //}
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
            //lblMsg.Text = MyStringBuilder.ToString();
        }

        //lblMsg.Visible = true;
        if (bOk)
        {
            //lblMsg.Visible = false;
            cRefere.setItinerario(this, source, e);

        }
        else
        {
            //mdodalvalida.Show();

        }
    }

    private bool RadioSelected(Repeater Dida = null, Repeater Dvuelta = null)
    {
        bool bCorrect = false;

        RadioButton radioButton = null;
        if (Dida != null)
        {
            foreach (Repeater i in Dida.Items)
            {
                radioButton = (RadioButton)i.FindControl("rbtnSel");
                if (radioButton != null && radioButton.Checked) bCorrect = true;
            }
        }

        if (Dvuelta != null && bCorrect == true)
        {
            bCorrect = false;
            foreach (Repeater i in Dvuelta.Items)
            {
                radioButton = (RadioButton)i.FindControl("rbtnSel");
                if (radioButton != null && radioButton.Checked) bCorrect = true;
            }
        }

        return bCorrect;
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        cRefere.setOrdenar(this, sender, e);
    }
}
