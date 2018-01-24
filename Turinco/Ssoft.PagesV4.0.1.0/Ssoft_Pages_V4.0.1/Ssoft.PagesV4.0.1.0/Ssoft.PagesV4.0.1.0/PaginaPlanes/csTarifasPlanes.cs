using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Ssoft.Rules.Generales;
using SsoftQuery.Planes;
using Ssoft.ValueObjects;
using Ssoft.Rules.Reservas;
using SsoftQuery.Vuelos;
using SsoftQuery.Usuarios;
using SsoftQuery.Generales;
using SsoftQuery.Operadores;

namespace Ssoft.Pages.PaginaPlanes
{
    public class csTarifasPlanes
    {
        public static string sTipoPlanCircuito = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
        public static string sNormal = clsValidaciones.GetKeyOrAdd("ControlCuposNormal", "Normal");
        public static string sControlCupos = clsValidaciones.GetKeyOrAdd("ControlCuposCon", "ConControl");
        public static string sControlCuposSin = clsValidaciones.GetKeyOrAdd("ControlCuposSin", "SinControl");
        public static string sControlCuposPersonas = clsValidaciones.GetKeyOrAdd("TipoControlCuposPersona", "ConControlPersona");
        public static string sControlCuposPropiedad = clsValidaciones.GetKeyOrAdd("TipoControlCuposPropiedad", "ConControPropiedades");
        public static string sControlPersonaPropiedad = clsValidaciones.GetKeyOrAdd("TipoControlCuposPersonaPropiedad", "ConControlPersonaPropiedad");
        public static string sPlantillaMultiHotel = clsValidaciones.GetKeyOrAdd("PlantillaMultiHotel", "PLMULTIHT");
        public static string sCotizacionNormal = clsValidaciones.GetKeyOrAdd("CotizacionNormal", "Normal");
        private static string sFormatoFecha = clsSesiones.getFormatoFecha();
        private static string sFormatoFechaBD = clsSesiones.getFormatoFechaBD();
        private static string sCaracterDecimal = clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",");
        private static string sFormatoDecimales = clsValidaciones.GetKeyOrAdd("FormatoDecimalesGen", "###.###,##");

        /// <summary>
        /// metodo que valida si se debe cargar o no el cotizador de rotativos
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-20
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setValidarCargueRotativos(UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["ControlCupos"] != null &&
                    PageSource.Request.QueryString["Plantilla"] != null && PageSource.Request.QueryString["Cotizacion"] != null)
                {
                    if (PageSource.Request.QueryString["TipoPlan"].Equals(sTipoPlanCircuito))
                    {
                        if (PageSource.Request.QueryString["ControlCupos"].Equals(sNormal))
                        {
                            if (PageSource.Request.QueryString["Plantilla"] != sPlantillaMultiHotel)
                            {
                                if (PageSource.Request.QueryString["Cotizacion"].Equals(sCotizacionNormal))
                                {
                                    setCargarCotizador(PageSource);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarCargueRotativos";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// metodo que valida si se debe cargar o no el cotizador de circuitos
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-03-18
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setValidarCargueCircuitos(UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["ControlCupos"] != null &&
                    PageSource.Request.QueryString["Plantilla"] != null && PageSource.Request.QueryString["Cotizacion"] != null)
                {
                    if (PageSource.Request.QueryString["TipoPlan"].Equals(sTipoPlanCircuito))
                    {
                        string sTipoControlCupos = PageSource.Request.QueryString["ControlCupos"];
                        if (sTipoControlCupos.Equals(sControlCupos) || sTipoControlCupos.Equals(sControlCuposSin) || sTipoControlCupos.Equals(sControlCuposPersonas) ||
                        sTipoControlCupos.Equals(sControlCuposPropiedad) || sTipoControlCupos.Equals(sControlPersonaPropiedad))
                        {
                            if (PageSource.Request.QueryString["Plantilla"] != sPlantillaMultiHotel)
                            {
                                if (PageSource.Request.QueryString["Cotizacion"].Equals(sCotizacionNormal))
                                {
                                    setCargarCotizador(PageSource);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarCargueRotativos";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// metodo que valida si se debe cargar o no el cotizador de rotativos con multiples hoteles
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-03-19
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setValidarCargueRotativosMulti(UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != "0" && PageSource.Request.QueryString["ControlCupos"] != "0" &&
                    PageSource.Request.QueryString["Plantilla"] != "0" && PageSource.Request.QueryString["Cotizacion"] != "0")
                {
                    string sTipoPlanCircuito = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                    if (PageSource.Request.QueryString["TipoPlan"].Equals(sTipoPlanCircuito))
                    {
                        string sNormal = clsValidaciones.GetKeyOrAdd("ControlCuposNormal", "Normal");

                        if (PageSource.Request.QueryString["ControlCupos"].Equals(sNormal))
                        {
                            string sPlantillaMultiHotel = clsValidaciones.GetKeyOrAdd("PlantillaMultiHotel", "PLMULTIHT");
                            if (PageSource.Request.QueryString["Plantilla"].Equals(sPlantillaMultiHotel))
                            {
                                string sCotizacionNormal = clsValidaciones.GetKeyOrAdd("CotizacionNormal", "Normal");
                                if (PageSource.Request.QueryString["Cotizacion"].Equals(sCotizacionNormal))
                                {
                                    setCargarCotizador(PageSource);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarCargueRotativosMulti";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// metodo que valida si se debe cargar o no el cotizador de circuitos con multiples hoteles
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-03-19
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setValidarCargueCircuitosMulti(UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != "0" && PageSource.Request.QueryString["ControlCupos"] != "0" &&
                    PageSource.Request.QueryString["Plantilla"] != "0" && PageSource.Request.QueryString["Cotizacion"] != "0")
                {
                    string sTipoPlanCircuito = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                    if (PageSource.Request.QueryString["TipoPlan"].Equals(sTipoPlanCircuito))
                    {
                        string sControlCupos = clsValidaciones.GetKeyOrAdd("ControlCuposCon", "ConControl");
                        string sControlCuposSin = clsValidaciones.GetKeyOrAdd("ControlCuposSin", "SinControl");
                        string sControlCuposPersonas = clsValidaciones.GetKeyOrAdd("TipoControlCuposPersona", "ConControlPersona");
                        string sControlCuposPropiedad = clsValidaciones.GetKeyOrAdd("TipoControlCuposPropiedad", "ConControPropiedades");
                        string sControlPersonaPropiedad = clsValidaciones.GetKeyOrAdd("TipoControlCuposPersonaPropiedad", "ConControlPersonaPropiedad");

                        if (PageSource.Request.QueryString["ControlCupos"].Equals(sControlCupos) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposSin) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPersonas)
                            || PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPropiedad) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlPersonaPropiedad))
                        {
                            string sPlantillaMultiHotel = clsValidaciones.GetKeyOrAdd("PlantillaMultiHotel", "PLMULTIHT");
                            if (PageSource.Request.QueryString["Plantilla"].Equals(sPlantillaMultiHotel))
                            {
                                string sCotizacionNormal = clsValidaciones.GetKeyOrAdd("CotizacionNormal", "Normal");
                                if (PageSource.Request.QueryString["Cotizacion"].Equals(sCotizacionNormal))
                                {
                                    setCargarCotizador(PageSource);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarCargueCircuitosMulti";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// metodo que valida si se debe cargar o no el cotizador de toures
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-03-21
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setValidarCargueToures(UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != "0")
                {
                    string sTipoPlanTour = clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC");
                    if (PageSource.Request.QueryString["TipoPlan"].Equals(sTipoPlanTour))
                    {
                        setCargarCotizador(PageSource);
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarCargueToures";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que carga erl cotizador
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-20
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setCargarCotizador(UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["id"] != null)
                {
                    clsCache cCache = new csCache().cCache();
                    if (cCache != null)
                    {
                        setLlenarDatosPlan(PageSource);
                        //setControlesBusquedaPlanes(PageSource);

                        Page PaginaActual = (Page)HttpContext.Current.Handler;
                        HiddenField hdfTarifa = (HiddenField)PaginaActual.FindControl("hdfTarifa");
                        if (hdfTarifa != null)
                            hdfTarifa.Value = "0";
                    }
                    else
                    {
                        csGeneralsPag.FinSesion();
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setCargarCotizador";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo general de llenado delcotizador de planes
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-21
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setLlenarDatosPlan(UserControl PageSource)
        {
            try
            {
                DropDownList ddlAnioSalida = (DropDownList)PageSource.FindControl("ddlAnioSalida");
                DropDownList ddlMoneda = (DropDownList)PageSource.FindControl("ddlMoneda");
                DropDownList ddlDuracion = (DropDownList)PageSource.FindControl("ddlDuracion");

                HtmlGenericControl dBarco = (HtmlGenericControl)PageSource.FindControl("dBarco");
                Label lblDuracion = (Label)PageSource.FindControl("lblDuracion");
                Label lblMoneda = (Label)PageSource.FindControl("lblMoneda");
                Label lblIdBarco = (Label)PageSource.FindControl("lblIdBarco");
                Label lblTDuracion = (Label)PageSource.FindControl("lblTDuracion");
                Label lblVigencia = (Label)PageSource.FindControl("lblVigencia");

                //tblPlanes tPlan = new tblPlanes();
                //tPlan.Get(PageSource.Request.QueryString["id"], 0);
                csGenerales cGen = new csGenerales();
                csConsultasPlanes cPlan = new csConsultasPlanes();
                DataTable tblPlan = (DataTable)PageSource.Session["$TablaDetallePlan"];

                DataTable tAnios = new DataTable();

                if (PageSource.Request.QueryString["ControlCupos"] != null)
                {
                    //si tiene control de cupos los años para las salidas se toman de la tabla de stock
                    if (PageSource.Request.QueryString["ControlCupos"].Equals(sControlCupos) ||
                        PageSource.Request.QueryString["ControlCupos"].Equals(sControlPersonaPropiedad))
                    {
                        //tAnios = cPlan.ConsultaAniosSalidas(PageSource.Request.QueryString["id"], DateTime.Now.ToString(sFormatoFechaBD));
                    }
                    else
                    {
                        //si es un rotativo se llenan los datos correcpondientes
                        string sTipoPlanHotel = clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL");
                        if (PageSource.Request.QueryString["ControlCupos"].Equals(sNormal) || PageSource.Request.QueryString["TipoPlan"].Equals(sTipoPlanHotel))
                        {
                            setLlenarDatosRotativo(PageSource, PageSource.Request.QueryString["id"], PageSource.Request.QueryString["TipoPlan"]);
                        }
                        //si no tiene control de cupos los años para las salidas se toman de la tabla de vigencias
                        else
                        {
                            tAnios = cPlan.ConsultaAniosVigencias(PageSource.Request.QueryString["id"], DateTime.Now.ToString(sFormatoFechaBD));
                        }
                    }

                    if (ddlDuracion != null && PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS")))
                    {
                        int iDiasIni = Convert.ToInt32(tblPlan.Rows[0]["intDias"].ToString());
                        int iDiasFin = Convert.ToInt32(tblPlan.Rows[0]["intNochesAdic"].ToString());
                        for (int i = iDiasIni; i <= iDiasFin; i++)
                        {
                            ListItem liItem = new ListItem();
                            if (i.ToString().Length < 2)
                                liItem.Text = "0" + i.ToString();
                            else
                                liItem.Text = i.ToString();
                            liItem.Value = i.ToString();
                            ddlDuracion.Items.Add(liItem);
                        }
                    }
                }

                if (tblPlan != null && tblPlan.Rows.Count > 0)
                {
                    if (lblDuracion != null)
                        lblDuracion.Text = tblPlan.Rows[0]["intDias"].ToString() + " Días - " + tblPlan.Rows[0]["intNoches"].ToString() + " Noches";

                    lblMoneda.Text = tblPlan.Rows[0]["strMoneda"].ToString();
                }
                DataSet dsCat = cPlan.ConsultaCategorias(PageSource.Request.QueryString["id"], "", "");
                if (dsCat != null)
                {
                    //si es un tipo de plan crucero la division para el nombre del barco existe, por lo tanto se llena con la funcion ue abre el detalle
                    if (dBarco != null && lblIdBarco != null)
                    {
                        StringBuilder sbBarco = new StringBuilder();
                        sbBarco.AppendLine(dsCat.Tables[1].Rows[0]["strNombre"].ToString());
                        sbBarco.AppendLine("<a href=\"#this\" onclick=\"javascript:SetIdBarco(" + dsCat.Tables[1].Rows[0]["IdHotel"].ToString() + ");\">");
                        sbBarco.AppendLine("Ver más información");
                        sbBarco.AppendLine("</a>");
                        dBarco.InnerHtml = sbBarco.ToString();
                        lblIdBarco.Text = dsCat.Tables[1].Rows[0]["IdHotel"].ToString();
                    }
                }
                if (tAnios != null)
                {
                    // si es um plan diferente a rotativos el dropdownlist de los años debe existir, se llena con los resultados de la consulta en la tabla correspondiente
                    if (ddlAnioSalida != null)
                    {
                        DataSet dsAnios = new DataSet();
                        dsAnios.Tables.Add(tAnios.Copy());
                        cGen.LlenarControlData(ddlAnioSalida, Enum_Controls.DropDownList, "Anio", "Anio", true, false, null, dsAnios);
                    }
                }
                if (ddlMoneda != null)
                {
                    DataSet dsMonedas = cPlan.ConReferenciaMonedas();
                    cGen.LlenarControlData(ddlAnioSalida, Enum_Controls.DropDownList, "Anio", "Anio", true, false, null, dsMonedas);
                    ddlMoneda.SelectedValue = tblPlan.Rows[0]["intMoneda"].ToString();
                }
                if (lblTDuracion != null)
                {
                    if (PageSource.Request.QueryString["Plantilla"] != null)
                    {
                        string sPlantillaDias = clsValidaciones.GetKeyOrAdd("PlantillaCotizacionDias", "PLTRASDIA");
                        string TipoPlantilla = PageSource.Request.QueryString["Plantilla"];
                        if (TipoPlantilla != sPlantillaDias)
                            lblTDuracion.Text = "Cantidad de horas ";
                    }
                }
                if (lblVigencia != null)
                {
                    DataTable tblVigencias = cPlan.ConsultaVigencias("", PageSource.Request.QueryString["id"]);
                    if (tblVigencias != null && tblVigencias.Rows.Count > 0)
                    {
                        lblVigencia.Text = "Tarifas vigentes desde: " + Convert.ToDateTime(tblVigencias.Rows[0]["Desde"]).ToString("dd MMM yyyy") +
                            " - Hasta: " + Convert.ToDateTime(tblVigencias.Rows[tblVigencias.Rows.Count - 1]["Hasta"]).ToString("dd MMM yyyy");
                    }
                }

                setLlenarRepetidorPax(PageSource);

            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setLlenarDatosPlan";
                cParametros.Complemento = "Circular";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que llena el repetidor de pasajeros de la cotizacion de planes
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-21
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setLlenarRepetidorPax(UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["id"] != null)
                {
                    DropDownList ddlCabinas = (DropDownList)PageSource.FindControl("ddlCabinas");
                    Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");

                    DataTable tPax = new DataTable();
                    //se valida el texto dependiendo del tipo de plan 
                    string sTextoRep = "";
                    if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")))
                        sTextoRep = "Habitación ";
                    else
                        sTextoRep = "Cabina ";

                    tPax.Columns.Add("Cabina");
                    int NumCabinas = Convert.ToInt32(ddlCabinas.SelectedValue);
                    for (int i = 0; i < NumCabinas; i++)
                    {
                        DataRow drPax = tPax.NewRow();
                        tPax.Rows.Add(drPax);
                        tPax.AcceptChanges();
                        tPax.Rows[tPax.Rows.Count - 1]["Cabina"] = sTextoRep + Convert.ToString(i + 1);
                    }
                    rptPasajeros.DataSource = tPax;
                    rptPasajeros.DataBind();
                    setLlenarEdadesTipospax(PageSource);
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setLlenarRepetidorPax";
                cParametros.Complemento = "Circular";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que valida y llena las edades de los pasajeros para la cotizacion
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-21
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setLlenarEdadesTipospax(UserControl PageSource)
        {
            try
            {
                DataTable tblPlan = (DataTable)PageSource.Session["$TablaDetallePlan"];
                int iEdadCnn = Convert.ToInt32(tblPlan.Rows[0]["intRangoIniCnn"].ToString());
                int iEdadAdt = Convert.ToInt32(tblPlan.Rows[0]["intRangoIniAdt"].ToString());

                Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");
                for (int i = 0; i < rptPasajeros.Items.Count; i++)
                {
                    Label lblEdadAdt = (Label)rptPasajeros.Items[i].FindControl("lblEdadAdt");
                    Label lblEdadCnn = (Label)rptPasajeros.Items[i].FindControl("lblEdadCnn");

                    if (lblEdadAdt != null)
                        lblEdadAdt.Text = "(+ " + iEdadAdt + " años)";

                    if (lblEdadCnn != null)
                        lblEdadCnn.Text = "(" + iEdadCnn + " a " + Convert.ToString(iEdadAdt - 1) + " años)";
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setLlenarRepetidorPax";
                cParametros.Complemento = "Circular";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo general de llenado de controles de cotizacion para un plan de tipo circuito
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="sidPlan"></param>
        /// <param name="sTipoPlan"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-22
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setLlenarDatosRotativo(UserControl PageSource, string sidPlan, string sTipoPlan)
        {
            try
            {
                DropDownList ddlCategoria = (DropDownList)PageSource.FindControl("ddlCategoria");
                DropDownList ddlDuracion = (DropDownList)PageSource.FindControl("ddlDuracion");

                csGenerales cGen = new csGenerales();

                DataTable tblPlan = (DataTable)PageSource.Session["$TablaDetallePlan"];

                csConsultasPlanes cPlan = new csConsultasPlanes();

                DataSet dsCat = cPlan.ConsultaCategorias(sidPlan, "", "");
                if (dsCat != null && dsCat.Tables.Count > 0)
                {
                    //se filtran los tipos de categoria para que no se repitan
                    DataView dvTpoCat = new DataView();
                    dvTpoCat.Table = dsCat.Tables[0].Copy();
                    string[] sCols = new string[2];
                    sCols[0] = "intIdRefere";
                    sCols[1] = "strCategoria";
                    dsCat.Tables.Clear();
                    dsCat.Tables.Add(dvTpoCat.ToTable(true, sCols));

                    //se llena el control de tipos de categoria
                    string sTipoPlanHotel = clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL");
                    if (sTipoPlan.Equals(sTipoPlanHotel))
                    {
                        cGen.LlenarControlData(ddlCategoria, Enum_Controls.DropDownList, "intIdRefere", "strCategoria", true, false,
                            null, dsCat.Tables[0]);
                    }
                    else
                    {
                        cGen.LlenarControlData(ddlCategoria, Enum_Controls.DropDownList, "intIdRefere", "strCategoria", false, false,
                            null, dsCat.Tables[0]);
                    }

                    setVigenciasCategoria(PageSource, ddlCategoria);
                }

                // se llena el combo de dias de duracion con respecto a los dias del plan y las noches adicionales permitidas
                if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")))
                {
                    int iDiasDesde = Convert.ToInt32(tblPlan.Rows[0]["intDias"].ToString());
                    int iDiasHasta = iDiasDesde + Convert.ToInt32(tblPlan.Rows[0]["intNochesAdic"].ToString());
                    if (ddlDuracion != null)
                    {
                        for (int i = iDiasDesde; i <= iDiasHasta; i++)
                        {
                            ListItem liItem = new ListItem(i.ToString() + " días, " + Convert.ToString(i - 1) + " noches", i.ToString());
                            ddlDuracion.Items.Add(liItem);
                        }
                    }
                }
                setLlenarRepetidorPax(PageSource);
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setLlenarDatosPlan";
                cParametros.Complemento = "Circular";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo de llenado de duracion de vigencias del cotizador de plan
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="sender"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-22
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setVigenciasCategoria(UserControl PageSource, object sender)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    if (PageSource.Request.QueryString["id"] != null)
                    {
                        DropDownList ddlCategoria = (DropDownList)sender;
                        Label lblVigencia = (Label)PageSource.FindControl("lblVigencia");
                        csConsultasPlanes cPlan = new csConsultasPlanes();
                        DataTable dtVigencias = cPlan.ConsultaVigencias(ddlCategoria.SelectedValue, PageSource.Request.QueryString["id"]);
                        if (dtVigencias != null && dtVigencias.Rows.Count > 0)
                        {
                            lblVigencia.Text = "Tarifas vigentes desde: " + Convert.ToDateTime(dtVigencias.Rows[0]["Desde"].ToString()).ToString("dd MMM yyyy") +
                                " Hasta: " + Convert.ToDateTime(dtVigencias.Rows[dtVigencias.Rows.Count - 1]["Hasta"].ToString()).ToString("dd MMM yyyy");
                        }
                    }
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setVigenciasCategoria";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que carga el repetidor de edades de acuerdo con el No de niños seleccionado
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="sender"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-22
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setMostrarEdadesNinos(UserControl PageSource, object sender)
        {
            try
            {
                if (PageSource.Request.QueryString["id"] != null)
                {
                    Repeater rptEdadninos = (Repeater)((RepeaterItem)((DropDownList)sender).Parent).FindControl("rptEdadninos");
                    DropDownList ddlNinos = (DropDownList)sender;
                    int iNumNinos = Convert.ToInt32(ddlNinos.SelectedValue);
                    DataTable tblEdades = new DataTable();
                    DataColumn dcEdades = new DataColumn("intEdaPax");
                    DataColumn dcPax = new DataColumn("strPax");
                    tblEdades.Columns.Add(dcEdades);
                    tblEdades.Columns.Add(dcPax);
                    for (int i = 0; i < iNumNinos; i++)
                    {
                        DataRow drEdadPax = tblEdades.NewRow();
                        tblEdades.Rows.Add(drEdadPax);
                        tblEdades.Rows[i]["strPax"] = "Niño " + Convert.ToString(i + 1);
                    }
                    tblEdades.AcceptChanges();
                    if (rptEdadninos != null)
                    {
                        if (iNumNinos == 0)
                        {
                            rptEdadninos.DataSource = null;
                            rptEdadninos.DataBind();
                        }
                        else
                        {
                            rptEdadninos.DataSource = tblEdades;
                            rptEdadninos.DataBind();
                        }
                    }

                    DataTable tblPlan = (DataTable)PageSource.Session["$TablaDetallePlan"];
                    if (tblPlan != null && tblPlan.Rows.Count > 0)
                    {
                        int iEdadCnn = Convert.ToInt32(tblPlan.Rows[0]["intRangoIniCnn"].ToString());
                        int iEdadAdt = Convert.ToInt32(tblPlan.Rows[0]["intRangoIniAdt"].ToString());

                        for (int b = 0; b < rptEdadninos.Items.Count; b++)
                        {
                            DropDownList ddlEdadNino = (DropDownList)rptEdadninos.Items[b].FindControl("ddlEdadNino");
                            ddlEdadNino.Items.Clear();
                            for (int c = iEdadCnn; c < iEdadAdt; c++)
                            {
                                if (c < 10)
                                {
                                    ListItem liEdad = new ListItem("0" + c.ToString(), c.ToString());
                                    ddlEdadNino.Items.Add(liEdad);
                                }
                                else
                                {
                                    ListItem liEdad = new ListItem(c.ToString(), c.ToString());
                                    ddlEdadNino.Items.Add(liEdad);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setMostrarEdadesNinos";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que llena los controles con los valores de una busqueda anterior y genera la cotizacion
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-22
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setControlesBusquedaPlanes(UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["id"] != null)
                {
                    clsCache cCache = new csCache().cCache();
                    clsCacheControl cCacheCont = new clsCacheControl();

                    Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");
                    DropDownList ddlCabinas = (DropDownList)PageSource.FindControl("ddlCabinas");
                    Label lblCabinas = (Label)PageSource.FindControl("lblCabinas");
                    DropDownList ddlAnioSalida = (DropDownList)PageSource.FindControl("ddlAnioSalida");
                    DropDownList ddlMesSalida = (DropDownList)PageSource.FindControl("ddlMesSalida");
                    DropDownList ddlDiaSalida = (DropDownList)PageSource.FindControl("ddlDiaSalida");
                    DropDownList ddlCategoria = (DropDownList)PageSource.FindControl("ddlCategoria");
                    DropDownList ddlDuracion = (DropDownList)PageSource.FindControl("ddlDuracion");
                    DropDownList ddlMoneda = (DropDownList)PageSource.FindControl("ddlMoneda");
                    TextBox txtFecha = (TextBox)PageSource.FindControl("txtFecha");

                    Label lblFecha = (Label)PageSource.FindControl("lblFecha");
                    Label lblDuracion = (Label)PageSource.FindControl("lblDuracion");
                    Label lblCategoria = (Label)PageSource.FindControl("lblCategoria");

                    if (cCache.ParametrosPlanes != null)
                    {
                        VO_FechaGeneral voFecha = cCache.ParametrosPlanes.FechaIni;

                        if (voFecha != null)
                        {

                            if (ddlAnioSalida != null)
                            {
                                //ddlAnioSalida.SelectedValue = voFecha.Year;
                                //setLlenarParametrosSalidas(PageSource, ddlAnioSalida);
                                //ddlMesSalida.SelectedValue = voFecha.Month;
                                //setLlenarParametrosSalidas(PageSource, ddlMesSalida);
                                //ddlDiaSalida.SelectedValue = voFecha.Day;
                            }
                            if (txtFecha != null)
                            {
                                if (voFecha.Formato == Enum_FormatoFecha.MDY)
                                    txtFecha.Text = voFecha.Month + voFecha.Separador + voFecha.Day + voFecha.Separador + voFecha.Year;
                                else
                                    txtFecha.Text = voFecha.Day + voFecha.Separador + voFecha.Month + voFecha.Separador + voFecha.Year;
                            }
                            if (lblFecha != null)
                            {
                                if (voFecha.Formato == Enum_FormatoFecha.MDY)
                                    lblFecha.Text = voFecha.Month + voFecha.Separador + voFecha.Day + voFecha.Separador + voFecha.Year;
                                else
                                    lblFecha.Text = voFecha.Day + voFecha.Separador + voFecha.Month + voFecha.Separador + voFecha.Year;
                            }
                        }
                        if (ddlCategoria != null)
                        {
                            ddlCategoria.SelectedValue = cCache.ParametrosPlanes.Categoria;
                            if (lblCategoria != null)
                            {
                                lblCategoria.Text = ddlCategoria.SelectedItem.Text;
                            }
                        }

                        if (lblCabinas != null)
                        {
                            lblCabinas.Text = cCache.ParametrosPlanes.Ocupacion.Count.ToString();
                        }
                        if (ddlMoneda != null)
                        {
                            ddlMoneda.SelectedValue = cCache.ParametrosPlanes.Moneda;
                        }
                        if (ddlDuracion != null)
                        {
                            ddlDuracion.SelectedValue = cCache.ParametrosPlanes.Dias;
                        }
                        if (lblDuracion != null)
                        {
                            lblDuracion.Text = cCache.ParametrosPlanes.Dias;
                        }

                        #region Pasajeros
                        if (rptPasajeros != null)
                        {
                            if (cCache.ParametrosPlanes.Ocupacion != null)
                            {
                                DataTable tPax = new DataTable();
                                //se valida el texto dependiendo del tipo de plan 
                                string sTextoRep = "";
                                if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")))
                                    sTextoRep = "Habitación ";
                                else
                                    sTextoRep = "Cabina ";

                                tPax.Columns.Add("Cabina");

                                for (int x = 0; x < cCache.ParametrosPlanes.Ocupacion.Count; x++)
                                {
                                    DataRow drPax = tPax.NewRow();
                                    tPax.Rows.Add(drPax);
                                    tPax.AcceptChanges();
                                    tPax.Rows[tPax.Rows.Count - 1]["Cabina"] = sTextoRep + Convert.ToString(x + 1);
                                }
                                rptPasajeros.DataSource = tPax;
                                rptPasajeros.DataBind();

                                for (int i = 0; i < rptPasajeros.Items.Count; i++)
                                {
                                    if (rptPasajeros.Items[i].FindControl("ddlAdultos") != null)
                                        ((DropDownList)rptPasajeros.Items[i].FindControl("ddlAdultos")).SelectedValue = cCache.ParametrosPlanes.Ocupacion[i].Adultos.ToString();
                                    if (rptPasajeros.Items[i].FindControl("ddlNinos") != null)
                                        ((DropDownList)rptPasajeros.Items[i].FindControl("ddlNinos")).SelectedValue = cCache.ParametrosPlanes.Ocupacion[i].Ninos.ToString();
                                    if (rptPasajeros.Items[i].FindControl("ddlJuniors") != null)
                                        ((DropDownList)rptPasajeros.Items[i].FindControl("ddlJuniors")).SelectedValue = cCache.ParametrosPlanes.Ocupacion[i].Junior.ToString();
                                    if (rptPasajeros.Items[i].FindControl("ddlInfantes") != null)
                                        ((DropDownList)rptPasajeros.Items[i].FindControl("ddlInfantes")).SelectedValue = cCache.ParametrosPlanes.Ocupacion[i].Infantes.ToString();
                                    if (rptPasajeros.Items[i].FindControl("lblAdultos") != null)
                                        ((Label)rptPasajeros.Items[i].FindControl("lblAdultos")).Text = cCache.ParametrosPlanes.Ocupacion[i].Adultos.ToString();
                                    if (rptPasajeros.Items[i].FindControl("lblNinos") != null)
                                        ((Label)rptPasajeros.Items[i].FindControl("lblNinos")).Text = cCache.ParametrosPlanes.Ocupacion[i].Ninos.ToString();
                                    if (rptPasajeros.Items[i].FindControl("lblJuniors") != null)
                                        ((Label)rptPasajeros.Items[i].FindControl("lblJuniors")).Text = cCache.ParametrosPlanes.Ocupacion[i].Junior.ToString();
                                    if (rptPasajeros.Items[i].FindControl("lblInfantes") != null)
                                        ((Label)rptPasajeros.Items[i].FindControl("lblInfantes")).Text = cCache.ParametrosPlanes.Ocupacion[i].Infantes.ToString();

                                    Repeater rptEdadninos = (Repeater)rptPasajeros.Items[i].FindControl("rptEdadninos");
                                    if (rptEdadninos != null)
                                    {
                                        setMostrarEdadesNinos(PageSource, ((DropDownList)rptPasajeros.Items[i].FindControl("ddlNinos")));
                                        setRptEdadesNinosBusqueda(rptEdadninos, cCache.ParametrosPlanes.Ocupacion[i].Edades);
                                    }
                                }
                            }
                        }
                        #endregion
                        setCotizar(PageSource);
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setControlesBusquedaPlanes";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que llena el repetidor de edades de acuerdo a una busqueda anterior
        /// </summary>
        /// <param name="rptEdadesNinos"></param>
        /// <param name="iEdadesNinios"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-22
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setRptEdadesNinosBusqueda(Repeater rptEdadesNinos, List<int> iEdadesNinios)
        {
            try
            {
                for (int x = 0; x < rptEdadesNinos.Items.Count; x++)
                {
                    DropDownList ddlNinos = (DropDownList)rptEdadesNinos.Items[x].FindControl("ddlEdadNino");
                    if (ddlNinos != null)
                        ddlNinos.SelectedValue = iEdadesNinios[x].ToString();
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setRptEdadesNinosBusqueda";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo principal de cotizacion de tarifas de planes
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-22
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setCotizar(UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["id"] != null)
                {
                    clsCache cCache = new csCache().cCache();
                    if (cCache != null)
                    {
                        setParametrosBusquedaPlanes(PageSource);
                        setValidarSalida(PageSource, false);
                    }
                    else
                    {
                        csGeneralsPag.FinSesion();
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setCotizar";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que guarda los parametros de busqueda de planes en el cache
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-22
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setParametrosBusquedaPlanes(UserControl PageSource)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                clsCacheControl cCacheCont = new clsCacheControl();
                if (PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["id"] != null)
                {
                    Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");
                    DropDownList ddlCabinas = (DropDownList)PageSource.FindControl("ddlCabinas");
                    DropDownList ddlAnioSalida = (DropDownList)PageSource.FindControl("ddlAnioSalida");
                    DropDownList ddlMesSalida = (DropDownList)PageSource.FindControl("ddlMesSalida");
                    DropDownList ddlDiaSalida = (DropDownList)PageSource.FindControl("ddlDiaSalida");
                    DropDownList ddlCategoria = (DropDownList)PageSource.FindControl("ddlCategoria");
                    DropDownList ddlDuracion = (DropDownList)PageSource.FindControl("ddlDuracion");
                    DropDownList ddlMoneda = (DropDownList)PageSource.FindControl("ddlMoneda");
                    TextBox txtFecha = (TextBox)PageSource.FindControl("txtFecha");

                    VO_ParametrosTarifas voParam = new VO_ParametrosTarifas();
                    VO_FechaGeneral voFecha = new VO_FechaGeneral();

                    if (sFormatoFecha.Equals("MM/dd/yyyy"))
                        voFecha.Formato = Enum_FormatoFecha.MDY;
                    else
                        voFecha.Formato = Enum_FormatoFecha.DMY;

                    voFecha.Separador = "/";

                    if (ddlAnioSalida != null)
                    {
                        voFecha.Year = ddlAnioSalida.SelectedValue;
                        voFecha.Month = ddlMesSalida.SelectedValue;
                        voFecha.Day = ddlDiaSalida.SelectedValue;
                    }
                    if (txtFecha != null)
                    {
                        DateTime dtFecha = Convert.ToDateTime(clsValidaciones.ConverFecha(txtFecha.Text, sFormatoFecha, sFormatoFechaBD));
                        if (dtFecha.Day.ToString().Length < 2)
                            voFecha.Day = "0" + dtFecha.Day.ToString();
                        else
                            voFecha.Day = dtFecha.Day.ToString();

                        if (dtFecha.Month.ToString().Length < 2)
                            voFecha.Month = "0" + dtFecha.Month.ToString();
                        else
                            voFecha.Month = dtFecha.Month.ToString();
                        voFecha.Year = dtFecha.Year.ToString();
                    }
                    voParam.FechaIni = voFecha;
                    if (ddlCategoria != null)
                    {
                        voParam.Categoria = ddlCategoria.SelectedValue;
                    }
                    if (ddlMoneda != null)
                    {
                        voParam.Moneda = ddlMoneda.SelectedValue;
                    }
                    if (ddlDuracion != null)
                    {
                        voParam.Dias = ddlDuracion.SelectedValue;
                    }
                    if (rptPasajeros != null)
                    {
                        voParam.Ocupacion = new List<VO_OcupacionPlanes>();
                        for (int i = 0; i < rptPasajeros.Items.Count; i++)
                        {
                            VO_OcupacionPlanes voOcupaciones = new VO_OcupacionPlanes();
                            if (rptPasajeros.Items[i].FindControl("ddlAdultos") != null)
                                voOcupaciones.Adultos = Convert.ToInt32(((DropDownList)rptPasajeros.Items[i].FindControl("ddlAdultos")).SelectedValue);
                            if (rptPasajeros.Items[i].FindControl("ddlNinos") != null)
                                voOcupaciones.Ninos = Convert.ToInt32(((DropDownList)rptPasajeros.Items[i].FindControl("ddlNinos")).SelectedValue);
                            if (rptPasajeros.Items[i].FindControl("ddlJuniors") != null)
                                voOcupaciones.Junior = Convert.ToInt32(((DropDownList)rptPasajeros.Items[i].FindControl("ddlJuniors")).SelectedValue);
                            if (rptPasajeros.Items[i].FindControl("ddlInfantes") != null)
                                voOcupaciones.Infantes = Convert.ToInt32(((DropDownList)rptPasajeros.Items[i].FindControl("ddlInfantes")).SelectedValue);

                            Repeater rptEdadesNinos = ((Repeater)rptPasajeros.Items[i].FindControl("rptEdadninos"));
                            if (rptEdadesNinos != null)
                            {
                                List<int> liEdadesVacio = new List<int>();
                                voOcupaciones.Edades = liEdadesVacio;
                                for (int x = 0; x < rptEdadesNinos.Items.Count; x++)
                                {
                                    DropDownList ddlEdadNino = (DropDownList)rptEdadesNinos.Items[x].FindControl("ddlEdadNino");
                                    if (ddlEdadNino != null)
                                        voOcupaciones.Edades.Add(Convert.ToInt32(ddlEdadNino.SelectedValue));
                                }
                            }

                            voParam.Ocupacion.Add(voOcupaciones);
                        }
                    }
                    cCache.ParametrosPlanes = voParam;
                    cCacheCont.ActualizaXML(cCache);
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setParametrosBusquedaPlanes";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que valida el tipo de cotizacion que se va a manejar en la pagina principal de detalle
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-22
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setCargarPaginaPrincipal(UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != "0" && PageSource.Request.QueryString["ControlCupos"] != "0" &&
                    PageSource.Request.QueryString["Plantilla"] != "0")
                {
                    string sTipoPlanCircuito = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                    if (PageSource.Request.QueryString["TipoPlan"].Equals(sTipoPlanCircuito))
                    {
                        Panel pSalidas = (Panel)PageSource.FindControl("pSalidas");

                        Panel pRotativos = (Panel)PageSource.FindControl("pRotativos");
                        Panel pRotativosMulti = (Panel)PageSource.FindControl("pRotativosMulti");
                        Panel pSalidasMulti = (Panel)PageSource.FindControl("pSalidasMulti");
                        Panel pRotativosEdades = (Panel)PageSource.FindControl("pRotativosEdades");

                        string sControlCupos = clsValidaciones.GetKeyOrAdd("ControlCuposCon", "ConControl");
                        string sControlCuposSin = clsValidaciones.GetKeyOrAdd("ControlCuposSin", "SinControl");
                        string sControlCuposPersonas = clsValidaciones.GetKeyOrAdd("TipoControlCuposPersona", "ConControlPersona");
                        string sControlCuposPropiedad = clsValidaciones.GetKeyOrAdd("TipoControlCuposPropiedad", "ConControPropiedades");
                        string sControlPersonaPropiedad = clsValidaciones.GetKeyOrAdd("TipoControlCuposPersonaPropiedad", "ConControlPersonaPropiedad");

                        string sNormal = clsValidaciones.GetKeyOrAdd("ControlCuposNormal", "Normal");
                        string sPlantillaMultiHotel = clsValidaciones.GetKeyOrAdd("PlantillaMultiHotel", "PLMULTIHT");
                        string sPlantillaEdades = clsValidaciones.GetKeyOrAdd("PlantillaEdades", "PLEDADES");

                        if (PageSource.Request.QueryString["ControlCupos"].Equals(sControlCupos) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposSin)
                            || PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPersonas) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPropiedad)
                            || PageSource.Request.QueryString["ControlCupos"].Equals(sControlPersonaPropiedad))
                        {
                            if (PageSource.Request.QueryString["Plantilla"].Equals(sPlantillaMultiHotel))
                            {
                                if (pRotativos != null)
                                    pRotativos.Visible = false;
                                if (pSalidas != null)
                                    pSalidas.Visible = false;
                                if (pRotativosMulti != null)
                                    pRotativosMulti.Visible = false;
                                if (pRotativosEdades != null)
                                    pRotativosEdades.Visible = false;
                                if (pSalidasMulti != null)
                                    pSalidasMulti.Visible = true;
                            }
                            else
                            {
                                if (pRotativos != null)
                                    pRotativos.Visible = false;
                                if (pSalidas != null)
                                    pSalidas.Visible = true;
                                if (pRotativosMulti != null)
                                    pRotativosMulti.Visible = false;
                                if (pRotativosEdades != null)
                                    pRotativosEdades.Visible = false;
                                if (pSalidasMulti != null)
                                    pSalidasMulti.Visible = false;
                            }
                        }
                        else
                        {
                            if (PageSource.Request.QueryString["ControlCupos"].Equals(sNormal))
                            {
                                if (PageSource.Request.QueryString["Plantilla"].Equals(sPlantillaMultiHotel))
                                {
                                    if (pRotativos != null)
                                        pRotativos.Visible = false;
                                    if (pSalidas != null)
                                        pSalidas.Visible = false;
                                    if (pRotativosMulti != null)
                                        pRotativosMulti.Visible = true;
                                    if (pRotativosEdades != null)
                                        pRotativosEdades.Visible = false;
                                    if (pSalidasMulti != null)
                                        pSalidasMulti.Visible = false;
                                }
                                else
                                {
                                    if (PageSource.Request.QueryString["Plantilla"].Equals(sPlantillaEdades))
                                    {
                                        if (pRotativos != null)
                                            pRotativos.Visible = false;
                                        if (pSalidas != null)
                                            pSalidas.Visible = false;
                                        if (pRotativosMulti != null)
                                            pRotativosMulti.Visible = false;
                                        if (pRotativosEdades != null)
                                            pRotativosEdades.Visible = true;
                                        if (pSalidasMulti != null)
                                            pSalidasMulti.Visible = false;
                                    }
                                    else
                                    {
                                        if (pRotativos != null)
                                            pRotativos.Visible = true;
                                        if (pSalidas != null)
                                            pSalidas.Visible = false;
                                        if (pRotativosMulti != null)
                                            pRotativosMulti.Visible = false;
                                        if (pRotativosEdades != null)
                                            pRotativosEdades.Visible = false;
                                        if (pSalidasMulti != null)
                                            pSalidasMulti.Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                if (pRotativos != null)
                                    pRotativos.Visible = false;
                                if (pSalidas != null)
                                    pSalidas.Visible = true;
                                if (pRotativosMulti != null)
                                    pRotativosMulti.Visible = false;
                                if (pRotativosEdades != null)
                                    pRotativosEdades.Visible = false;
                                if (pSalidasMulti != null)
                                    pSalidasMulti.Visible = false;
                            }
                        }

                        LodScript(PageSource);
                    }
                }

            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setCargarCotizador";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que valida el tipo de cotizacion que se va a manejar en la pagina principal de detalle de toures
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-03-21
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setCargarPaginaPrincipalToures(UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != "0" && PageSource.Request.QueryString["Plantilla"] != "0")
                {
                    Panel pTrasladosTrayecto = (Panel)PageSource.FindControl("pTrasladosTrayecto");
                    Panel pTrasladosDias = (Panel)PageSource.FindControl("pTrasladosDias");
                    Panel pExcursiones = (Panel)PageSource.FindControl("pExcursiones");


                    string sTipoRefere = clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC");
                    if (PageSource.Request.QueryString["TipoPlan"].Equals(sTipoRefere))
                    {
                        pTrasladosDias.Visible = false;
                        pTrasladosTrayecto.Visible = false;
                        pExcursiones.Visible = true;
                    }
                    else
                    {
                        sTipoRefere = clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS");
                        if (PageSource.Request.QueryString["TipoPlan"].Equals(sTipoRefere))
                        {
                            string sPlantillaTrayecto = clsValidaciones.GetKeyOrAdd("PlantillaCotizacionTrayecto", "PLTRASTR");
                            if (PageSource.Request.QueryString["Plantilla"].Equals(sPlantillaTrayecto))
                            {
                                pTrasladosTrayecto.Visible = true;
                                pTrasladosDias.Visible = false;
                                pExcursiones.Visible = false;
                            }
                            else
                            {
                                pTrasladosTrayecto.Visible = false;
                                pTrasladosDias.Visible = true;
                                pExcursiones.Visible = false;
                            }
                        }
                        else
                        {
                            pTrasladosDias.Visible = false;
                            pTrasladosTrayecto.Visible = false;
                            pExcursiones.Visible = true;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setCargarPaginaPrincipalCrucero";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que genera el script que llamara el boton de cotizacion dependiendo del tipo d eplan y la plantilla
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-22
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void LodScript(UserControl PageSource)
        {
            string sValuescript = string.Empty;

            //Boton para cotizar y colocar el javascript down
            Button btnCotiza = (Button)PageSource.FindControl("btnpasscotiza");

            if (btnCotiza != null)
            {
                if (PageSource.Request.QueryString["Plantilla"].Equals("PLNORM") && PageSource.Request.QueryString["TipoPlan"].Equals("CIRC"))
                {
                    sValuescript = "javascript:document.getElementById('ucDetallePlan_UcTarifasRotativos1_btnCotizar').focus(); return false; ";
                }
                if (PageSource.Request.QueryString["Plantilla"].Equals("PLNORM") && PageSource.Request.QueryString["Cotizacion"].Equals("Normal"))
                {
                    sValuescript = "javascript:document.getElementById('ucDetallePlan_UcTarifasRotativos1_btnCotizar').focus(); return false; ";
                }
                if (PageSource.Request.QueryString["Plantilla"].Equals("PLNORM") && PageSource.Request.QueryString["Cotizacion"].Equals("Normal") && PageSource.Request.QueryString["ControlCupos"].Equals("SinControl"))
                {
                    sValuescript = "javascript:document.getElementById('ucDetallePlan_UcTarifasCircuitos1_btnCotizar').focus(); return false; ";
                }
                if (PageSource.Request.QueryString["Plantilla"].Equals("PLMULTIHT") && PageSource.Request.QueryString["Cotizacion"].Equals("Normal") && PageSource.Request.QueryString["ControlCupos"].Equals("Normal"))
                {
                    sValuescript = "javascript:document.getElementById('ucDetallePlan_UcTarifasRotativosMulti1_btnCotizar').focus(); return false; ";
                }
                if (PageSource.Request.QueryString["Plantilla"].Equals("PLMULTIHT") && PageSource.Request.QueryString["Cotizacion"].Equals("Normal") && PageSource.Request.QueryString["ControlCupos"].Equals("Normal"))
                {
                    sValuescript = "javascript:document.getElementById('ucDetallePlan_UcTarifasRotativosMulti1_btnCotizar').focus(); return false; ";
                }

                if (PageSource.Request.QueryString["Plantilla"].Equals("PLMULTIHT") && PageSource.Request.QueryString["Cotizacion"].Equals("Normal") && PageSource.Request.QueryString["ControlCupos"].Equals("SinControl"))
                {
                    sValuescript = "javascript:document.getElementById('ucDetallePlan_UcTarifasCircuitos1_btnCotizar').focus(); return false; ";
                }

                if (PageSource.Request.QueryString["Plantilla"].Equals("PLMULTIHT") && PageSource.Request.QueryString["Cotizacion"].Equals("Normal") && PageSource.Request.QueryString["ControlCupos"].Equals("SinControl"))
                {
                    sValuescript = "javascript:document.getElementById('ucDetallePlan_UcTarifasCircuitosMulti1_btnCotizar').focus(); return false; ";
                }

                if (PageSource.Request.QueryString["Plantilla"].Equals("PLNORM") && PageSource.Request.QueryString["TipoPlan"].Equals("CIRC") && PageSource.Request.QueryString["Cotizacion"].Equals("Normal") && PageSource.Request.QueryString["ControlCupos"].Equals("ConControl"))
                {
                    sValuescript = "javascript:document.getElementById('ucDetallePlan_UcTarifasCircuitos1_btnCotizar').focus(); return false; ";
                }

                btnCotiza.OnClientClick = (sValuescript);

            }


        }

        /// <summary>
        /// Metodo para el control del evento command de los botones de la pagina, se valida la operacion a realizar
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-27
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setCommand(UserControl PageSource, object source, CommandEventArgs e)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    switch (e.CommandName)
                    {
                        //case "Add":
                        //    setAgregar(PageSource);
                        //    break;
                        //case "AddSouv":
                        //    setAgregarSouv(PageSource);
                        //    break;
                        case "Cotizar":
                            setCotizar(PageSource);
                            break;
                        //case "CotizarWs":
                        //    setCotizarWs(PageSource);
                        //    break;
                        case "Reservar":
                            setReservarPlanes(PageSource, source);
                            break;
                        //case "ReservarWs":
                        //    setReservarWs(PageSource, source);
                        //    break;
                        //case "VerificarOfertas":
                        //    setValidarDispOfertasAereas(PageSource);
                        //    break;
                        //case "OtrosPlanes":
                        //    setParametrosBusquedaSoloDestino(PageSource);
                        //    break;
                        //case "ReservaTarifaDirecto":
                        //    setSeleccionCabinaReservaDirecta(PageSource, source);
                        //    setReservarCruceros(PageSource, source);
                        //    break;
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setCommand";
                cParametros.Complemento = "Tarifas";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que valida los controles obligatorios para cada tipo de plan o de cotizacion
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="bCotizaAlimentacion"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-27
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setValidarSalida(UserControl PageSource, bool bCotizaAlimentacion)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["id"] != null)
                {
                    DropDownList ddlAnioSalida = (DropDownList)PageSource.FindControl("ddlAnioSalida");
                    DropDownList ddlMesSalida = (DropDownList)PageSource.FindControl("ddlMesSalida");
                    DropDownList ddlDiaSalida = (DropDownList)PageSource.FindControl("ddlDiaSalida");
                    DropDownList ddlCategoria = (DropDownList)PageSource.FindControl("ddlCategoria");
                    DropDownList ddlDuracion = (DropDownList)PageSource.FindControl("ddlDuracion");
                    Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");

                    if (ddlAnioSalida != null)
                    {
                        if (ddlAnioSalida.SelectedValue.Equals("") || ddlAnioSalida.SelectedValue.Equals("0") ||
                            ddlMesSalida.SelectedValue.Equals("") || ddlMesSalida.SelectedValue.Equals("0") ||
                            ddlDiaSalida.SelectedValue.Equals("") || ddlDiaSalida.SelectedValue.Equals("0"))
                        {
                            lblErrorGen.Text = "Por favor selecciona los parámetros para la salida";
                        }
                        else
                        {
                            lblErrorGen.Text = "";
                            setValidarControlCupos(PageSource, bCotizaAlimentacion);
                        }
                    }
                    else
                    {
                        TextBox txtFecha = (TextBox)PageSource.FindControl("txtFecha");
                        if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")) ||
                            PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS")) ||
                            PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanWs", "PLNWS")) ||
                            PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL")) ||
                            PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ")))
                        {
                            if (txtFecha.Text.Trim().Equals(""))
                            {
                                lblErrorGen.Text = "Por favor selecciona la fecha de viaje";
                            }
                            else
                            {
                                lblErrorGen.Text = "";
                                if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanWs", "PLNWS")))
                                {
                                    //setValidarControlCuposWs(PageSource);
                                }
                                else
                                {
                                    setValidarControlCupos(PageSource, bCotizaAlimentacion);
                                }
                            }
                        }
                        else
                        {
                            if ((txtFecha.Text.Trim().Equals("") ||
                                ddlCategoria.SelectedValue.Equals("") || ddlCategoria.SelectedValue.Equals("0") ||
                                ddlDuracion.SelectedValue.Equals("") || ddlDuracion.SelectedValue.Equals("0")) &&
                                PageSource.Request.QueryString["TipoPlan"] != (clsValidaciones.GetKeyOrAdd("TipoPlanSouvenir", "SOUV")))
                            {
                                lblErrorGen.Text = "Por favor selecciona la fecha de viaje";
                            }
                            else
                            {
                                lblErrorGen.Text = "";
                                setValidarControlCupos(PageSource, bCotizaAlimentacion);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setCotizar";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que valida si el plan tiene o no control de cupos
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="bCotizaAlimentacion"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-27
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setValidarControlCupos(UserControl PageSource, bool bCotizaAlimentacion)
        {
            try
            {
                DropDownList ddlAnioSalida = (DropDownList)PageSource.FindControl("ddlAnioSalida");
                DropDownList ddlMesSalida = (DropDownList)PageSource.FindControl("ddlMesSalida");
                DropDownList ddlDiaSalida = (DropDownList)PageSource.FindControl("ddlDiaSalida");
                DropDownList ddlDuracion = (DropDownList)PageSource.FindControl("ddlDuracion");
                TextBox txtFecha = (TextBox)PageSource.FindControl("txtFecha");
                TextBox txtFechaFin = (TextBox)PageSource.FindControl("txtFechaFin");
                if (PageSource.Request.QueryString["TipoPlan"] != null)
                {
                    if (PageSource.Request.QueryString["id"] == null)
                    {
                        #region Cotizacion Pasaportes
                        //Label lblCodigo = null;
                        //if (bCotizaAlimentacion)
                        //{
                        //    try
                        //    {
                        //        RepeaterItem rpItem = (RepeaterItem)PageSource.Parent;
                        //        if (rpItem != null)
                        //        {
                        //            lblCodigo = (Label)rpItem.FindControl("lblCodigo");
                        //            if (lblCodigo != null)
                        //                PageSource.Request.QueryString["id"] = lblCodigo.Text;
                        //        }
                        //    }
                        //    catch (Exception Ex) { }
                        //}
                        //else
                        //{
                        //    PageSource.Request.QueryString["id"] = getObtenerIdPasaporte(PageSource);
                        //}
                        #endregion
                        if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ")))
                        {
                            Repeater rptCabina = (Repeater)PageSource.FindControl("rptCabina");
                            if (rptCabina != null)
                            {
                                rptCabina.DataSource = null;
                                rptCabina.DataBind();
                                Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");
                                if (lblErrorGen != null)
                                    lblErrorGen.Text = "No existen tarifas disponibles, por favor cambie los parametros de busqueda";
                            }
                        }
                    }
                    else
                    {
                        DataTable tblPlan = (DataTable)PageSource.Session["$TablaDetallePlan"];
                        if (tblPlan != null)
                        {
                            csGenerales cGen = new csGenerales();
                            //se obtiene la referencia de control de cupos
                            string sControlCupos = clsValidaciones.GetKeyOrAdd("ControlCuposCon", "ConControl");
                            string sControlCuposSin = clsValidaciones.GetKeyOrAdd("ControlCuposSin", "SinControl");
                            string sControlCuposPersonas = clsValidaciones.GetKeyOrAdd("TipoControlCuposPersona", "ConControlPersona");
                            string sControlCuposPropiedad = clsValidaciones.GetKeyOrAdd("TipoControlCuposPropiedad", "ConControPropiedades");
                            string sControlPersonaPropiedad = clsValidaciones.GetKeyOrAdd("TipoControlCuposPersonaPropiedad", "ConControlPersonaPropiedad");

                            //se obtiene la referencia de rotativos
                            string sNormal = clsValidaciones.GetKeyOrAdd("ControlCuposNormal", "Normal");

                            string sFechaIni = "";
                            if (ddlAnioSalida != null)
                            {
                                sFechaIni = ddlAnioSalida.SelectedValue + "/" + ddlMesSalida.SelectedValue + "/" + ddlDiaSalida.SelectedValue;
                            }
                            else
                            {
                                if (txtFecha != null && txtFecha.Text.Trim() != "")
                                    sFechaIni = clsValidaciones.ConverFecha(txtFecha.Text, sFormatoFecha, sFormatoFechaBD);
                                else
                                    sFechaIni = DateTime.Now.ToString(sFormatoFechaBD);
                            }
                            string sFechaFin = "";
                            if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")) ||
                                PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanSouvenir", "SOUV")) ||
                                PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ")))
                            {
                                sFechaFin = sFechaIni;
                            }
                            else
                            {
                                if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL")))
                                {
                                    sFechaFin = clsValidaciones.ConverFecha(txtFechaFin.Text, sFormatoFecha, sFormatoFechaBD);
                                }
                                else
                                {
                                    sFechaFin = Convert.ToDateTime(sFechaIni).AddDays(Convert.ToDouble(tblPlan.Rows[0]["intNoches"].ToString())).ToString(sFormatoFechaBD);
                                }
                            }
                            Boolean bIndControlCupos;
                            if (PageSource.Request.QueryString["ControlCupos"].Equals(sControlCupos) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPersonas) ||
                                PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPropiedad) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlPersonaPropiedad))
                            {
                                bIndControlCupos = true;
                            }
                            else
                            {
                                bIndControlCupos = false;
                                if (PageSource.Request.QueryString["ControlCupos"].Equals(sNormal))
                                    sFechaFin = Convert.ToDateTime(sFechaIni).AddDays(Convert.ToDouble(ddlDuracion.SelectedValue) - 1).ToString(sFormatoFechaBD);
                            }

                            setConsultarCabinasTarifas(PageSource, sFechaIni, sFechaFin, bIndControlCupos, PageSource.Request.QueryString["id"],
                                       PageSource.Request.QueryString["TipoPlan"], tblPlan.Rows[0]["strControlCupos"].ToString(), PageSource.Request.QueryString["Cotizacion"]);

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarControlCupos";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que valida la disponibilidad de cada una de las habitaciones o cabinas cotizadas, 
        /// se valida que tenga el tipo de pasajero en las tarifas y que los demas parametros generen resultados
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="sFechaIni"></param>
        /// <param name="sFechaFin"></param>
        /// <param name="ControlCupos"></param>
        /// <param name="idPlan"></param>
        /// <param name="sTipoPlan"></param>
        /// <param name="sTipoControl"></param>
        /// <param name="sCotizacion"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-27
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setConsultarCabinasTarifas(UserControl PageSource, string sFechaIni, string sFechaFin, bool ControlCupos,
            string idPlan, string sTipoPlan, string sTipoControl, string sCotizacion)
        {
            try
            {
                #region Controles y variables
                Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");
                Repeater rptCabina = (Repeater)PageSource.FindControl("rptCabina");
                DropDownList ddlCategoria = (DropDownList)PageSource.FindControl("ddlCategoria");
                DropDownList ddlDuracion = (DropDownList)PageSource.FindControl("ddlDuracion");
                Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");
                Button btnReservar = (Button)PageSource.FindControl("btnReservar");
                DataTable dtCabinas = new DataTable();
                DataTable dtTarifasCompletas = new DataTable();
                csConsultasPlanes cPlan = new csConsultasPlanes();
                csGenerales cGen = new csGenerales();
                bool bTodas = true;
                bool bFechas = false;
                bool bPasajeros = false;
                bool bExistenEdades = true;
                string sCabinasError = "";
                string sTextoRes = "";
                string sTipoHabError = "";
                string sAplicacion = "0";
                int iPaxCnnAdt = 0;

                DataTable tblPlan = (DataTable)PageSource.Session["$TablaDetallePlan"];

                string sCotizacionNormal = clsValidaciones.GetKeyOrAdd("CotizacionNormal", "Normal");
                string sPlantillaNormal = clsValidaciones.GetKeyOrAdd("PlantillaNormal", "PLNORM");
                string sPlantillaNinos = clsValidaciones.GetKeyOrAdd("PlantillaNinos", "PL_CRUCE_2");
                string sNormal = clsValidaciones.GetKeyOrAdd("ControlCuposNormal", "Normal");
                string sPlantillaCruceroTodos = clsValidaciones.GetKeyOrAdd("PlantillaCruceroTodos", "PL_CRUCE_3");
                string sPlantillaDias = clsValidaciones.GetKeyOrAdd("PlantillaCotizacionDias", "PLTRASDIA");
                string sPlantillaHoras = clsValidaciones.GetKeyOrAdd("PlantillaCotizacionHoras", "PLTRASHR");
                string sPlantillaMultiHotel = clsValidaciones.GetKeyOrAdd("PlantillaMultiHotel", "PLMULTIHT");
                string sPlantillaEdades = clsValidaciones.GetKeyOrAdd("PlantillaEdades", "PLEDADES");
                string TipoPlantilla = tblPlan.Rows[0]["strReferePlantilla"].ToString();
                //se validan los textos de error segun tipo de plan
                if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")) || sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL")))
                {
                    sTextoRes = "Habitación ";
                    sTipoHabError = "habitacion(es)";
                }
                else
                {
                    sTextoRes = "Cabina ";
                    sTipoHabError = "cabinas(s)";
                }

                #endregion
                DataTable dtTarifas = null;
                for (int i = 0; i < rptPasajeros.Items.Count; i++)
                {
                    #region texto cabina y bumero de pax
                    dtTarifasCompletas = new DataTable();
                    DropDownList ddlAdultos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlAdultos");
                    DropDownList ddlNinos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlNinos");
                    DropDownList ddlJuniors = (DropDownList)rptPasajeros.Items[i].FindControl("ddlJuniors");
                    DropDownList ddlInfantes = (DropDownList)rptPasajeros.Items[i].FindControl("ddlInfantes");
                    Repeater rptEdadninos = (Repeater)rptPasajeros.Items[i].FindControl("rptEdadninos");

                    int iNumpax = 0;
                    //si el combo de junior existe se cuentan los pasajeros elegidos de ese tipo
                    if (ddlJuniors != null)
                    {
                        if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ")))
                            iNumpax = Convert.ToInt32(ddlAdultos.SelectedValue) + Convert.ToInt32(ddlNinos.SelectedValue) + Convert.ToInt32(ddlJuniors.SelectedValue);
                        else if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")))
                            iNumpax = Convert.ToInt32(ddlAdultos.SelectedValue) + Convert.ToInt32(ddlNinos.SelectedValue);//Convert.ToInt32(ddlJuniors.SelectedValue);
                        else
                            iNumpax = Convert.ToInt32(ddlAdultos.SelectedValue) + Convert.ToInt32(ddlJuniors.SelectedValue);
                    }
                    else
                    {
                        if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")) || sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS")))
                        {
                            if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")))
                                iNumpax = Convert.ToInt32(ddlAdultos.SelectedValue) + Convert.ToInt32(ddlNinos.SelectedValue);
                            if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS")))
                                iNumpax = Convert.ToInt32(ddlAdultos.SelectedValue);
                        }
                        else
                        {
                            if (TipoPlantilla.Equals(sPlantillaCruceroTodos))
                            {
                                iNumpax = Convert.ToInt32(ddlAdultos.SelectedValue) + Convert.ToInt32(ddlNinos.SelectedValue)/* + Convert.ToInt32(ddlInfantes.SelectedValue)*/;
                            }
                            else
                            {
                                //if()
                                //else
                                iNumpax = Convert.ToInt32(ddlAdultos.SelectedValue) + Convert.ToInt32(ddlNinos.SelectedValue);
                            }
                        }
                    }
                    #endregion

                    if (sTipoControl.Equals(sNormal))
                    {
                        #region Tipo plan circuito
                        if (sCotizacion.Equals(sCotizacionNormal))
                        {
                            DataTable tblFechas = null;
                            DataTable tblFechaIni = null;
                            DataTable tblFechaFin = null;
                            tblFechas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), "", "", "",
                                "", false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, null, null);

                            DataView dvFechas = new DataView();
                            dvFechas.Table = tblFechas.Copy();
                            dvFechas.RowFilter = "'" + sFechaIni + "' >= Desde and '" + sFechaIni + "' <= Hasta";
                            tblFechaIni = dvFechas.ToTable();
                            dvFechas.Table = tblFechas.Copy();
                            dvFechas.RowFilter = "'" + sFechaFin + "' >= Desde and '" + sFechaFin + "' <= Hasta";
                            tblFechaFin = dvFechas.ToTable();

                            if ((tblFechaIni != null && tblFechaIni.Rows.Count > 0) && (tblFechaFin != null && tblFechaFin.Rows.Count > 0))
                            {
                                if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")))
                                {
                                    dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), iNumpax.ToString(), sFechaIni, sFechaFin,
                                        Convert.ToString(Convert.ToInt32(ddlDuracion.SelectedValue) - 1), false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue,
                                        ddlCategoria.SelectedValue, null);

                                    //validacion para combinacion de vigencias--------------
                                    if (dtTarifas == null)
                                    {
                                        dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), iNumpax.ToString(), sFechaIni, null,
                                        null, false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, ddlCategoria.SelectedValue, null);
                                    }
                                    //------------------------------------------------------
                                }
                                else
                                {
                                    //dtTarifas = cPlan.ConsultaTarifasCotizador(ddlCategoria.SelectedValue, iNumpax.ToString(), sFechaIni,
                                    //    Convert.ToInt32(ddlDuracion.SelectedValue), true, idPlan, sAplicacion, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, null, null);
                                }
                            }
                            else
                            {
                                bFechas = true;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Otros tipos de planes
                        if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")) || sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS"))
                            || sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanSouvenir", "SOUV")) || sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ")))
                        {
                            if (TipoPlantilla != null)
                            {
                                if (TipoPlantilla.Equals(sPlantillaDias) || TipoPlantilla.Equals(sPlantillaHoras))
                                {
                                    dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), iNumpax.ToString(), sFechaIni, null,
                                        ddlDuracion.SelectedValue, false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, null, null);
                                }
                                else
                                {
                                    if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ")))
                                    {
                                        dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), iNumpax.ToString(), sFechaIni, null,
                                            "", false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, null, PageSource.Request.QueryString["ClasifPasaporte"]);
                                    }
                                    else
                                    {
                                        if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")))
                                            dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), iNumpax.ToString(), sFechaIni, sFechaFin,
                                                "", true, "0", ddlNinos.SelectedValue, null, null);
                                        else
                                            dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), iNumpax.ToString(), sFechaIni, null,
                                                "", false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, null, null);
                                    }
                                }
                            }
                            else
                            {
                                #region Parques (Pasaportes)
                                //if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ")))
                                //{
                                //    if (PageSource.ID.ToUpper().Contains("NODISNEY"))
                                //    {
                                //        dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), "0", sFechaIni, null,
                                //           sAplicacion, "", false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, null, PageSource.Request.QueryString["ClasifPasaporte"]);
                                //    }
                                //    else
                                //    {
                                //        if (!PageSource.ID.ToUpper().Contains("COMIDAS"))
                                //        {
                                //            string sDuracion = "";
                                //            string sTipoPasaporte = "";
                                //            if (ddlDuracion != null && ddlDuracion.SelectedValue != "" && ddlDuracion.SelectedValue != "0")
                                //            {
                                //                sDuracion = ddlDuracion.SelectedValue;
                                //            }
                                //            else
                                //            {
                                //                clsCache cCache = new csCache().cCache();
                                //                if (cCache.DatosAdicionales != null && cCache.DatosAdicionales.Count > 8)
                                //                {
                                //                    DropDownList ddlDiaspasaporte = (DropDownList)PageSource.FindControl("ddlDiaspasaporte");
                                //                    if (ddlDiaspasaporte != null && ddlDiaspasaporte.SelectedValue != "" && ddlDiaspasaporte.SelectedValue != "0")
                                //                        sDuracion = ddlDiaspasaporte.SelectedValue;
                                //                    else
                                //                        sDuracion = cCache.DatosAdicionales[6];

                                //                    RadioButtonList rblTipoPasaporte = (RadioButtonList)PageSource.FindControl("rblTipoPasaporte");
                                //                    if (rblTipoPasaporte != null && rblTipoPasaporte.SelectedValue != "" && rblTipoPasaporte.SelectedValue != "0")
                                //                        sTipoPasaporte = rblTipoPasaporte.SelectedValue;
                                //                    else
                                //                        sTipoPasaporte = cCache.DatosAdicionales[7];
                                //                }
                                //            }

                                //            //tRefere.Get(sValue[9]);
                                //            //if (tRefere.Respuesta)
                                //            //{
                                //            //    if (!(tRefere.strRefere.Value.ToUpper().Equals(clsValidaciones.GetKeyOrAdd("DestinoOrlando", "EU001"))))
                                //            //        sTipoPasaporte = "";
                                //            //}
                                //            dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), "0", sFechaIni, null,
                                //               sAplicacion, sDuracion, false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, sTipoPasaporte, PageSource.Request.QueryString["ClasifPasaporte"], true, sValue[9]);
                                //        }
                                //        else
                                //        {
                                //            dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), "0", sFechaIni, null,
                                //               sAplicacion, "", false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, null, PageSource.Request.QueryString["ClasifPasaporte"], false);
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), "0", sFechaIni, null,
                                   "", false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, null, null);
                                //}
                                #endregion
                            }
                        }
                        else
                        {
                            #region Hoiteles
                            //if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL")))
                            //{
                            //    dtTarifas = cPlan.ConsultaTarifasCotizadorHotelesUnion(Convert.ToInt32(idPlan), iNumpax.ToString(), sFechaIni, sFechaFin,
                            //        sAplicacion, "", false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, ddlJuniors.SelectedValue, ddlCategoria.SelectedValue, null);
                            //}
                            //else
                            //{
                            if (sCotizacion.Equals(sCotizacionNormal))
                            {
                                dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), iNumpax.ToString(), sFechaIni, sFechaFin,
                                    "", false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, null, null);
                            }
                            //}
                            #endregion
                        }
                        #endregion
                    }

                    #region Validaciones tabla de tarifas y tipos de pax
                    if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                    {
                        dtTarifasCompletas = dtTarifas.Copy();
                        PageSource.Session["$TablaTarifasPlan"] = dtTarifasCompletas;

                        for (int x = 0; x < dtTarifas.Rows.Count; x++)
                        {
                            dtTarifas.Rows[x]["Desde"] = sFechaIni;
                            dtTarifas.Rows[x]["Hasta"] = sFechaFin;
                        }

                        if (i == 0)
                        {
                            dtCabinas = dtTarifas.Clone();
                        }
                        else
                        {
                            if (dtCabinas.Columns.Count == 0)
                                dtCabinas = dtTarifas.Clone();
                        }
                        dtCabinas.Rows.Add(dtTarifas.Rows[0].ItemArray);
                        DataView dvCat = new DataView();
                        dvCat.Table = dtTarifas.Copy();
                        DataTable dtCat = dvCat.ToTable(true, "IdCategoria");
                        //bool bExistePax = true;
                        //se valida que para todos los tipos de pasajero elegidos existan tarifas, de lo contrario no se muestran resultados excepto para traslados
                        if (sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS") && sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")
                            && sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanSouvenir", "SOUV") && sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanApartamento", "APTO")
                            && sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL"))
                        {
                            #region [Validacion tarifas tipos de Pax]

                            for (int y = 0; y < dtCat.Rows.Count; y++)
                            {
                                bool bExistePax = true;
                                int ContAdt = 0;
                                int ContCnn = 0;
                                int ContCnn2 = 0;
                                int ContJnr = 0;
                                int ContAdt3 = 0;
                                int ContAdt4 = 0;
                                int ContInf = 0;

                                if (TipoPlantilla != null && TipoPlantilla.Equals(sPlantillaEdades))
                                {
                                    Repeater rptEdadesJunior = (Repeater)rptPasajeros.Items[i].FindControl("rptEdadesJunior");
                                    Repeater rptEdadesNinos = (Repeater)rptPasajeros.Items[i].FindControl("rptEdadesNinos");
                                    for (int x = 0; x < dtTarifas.Rows.Count; x++)
                                    {
                                        if (dtCat.Rows[y]["idCategoria"].ToString().Equals(dtTarifas.Rows[x]["idCategoria"].ToString()))
                                        {
                                            if (dtTarifas.Rows[x]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT")))
                                                ContAdt++;
                                        }
                                    }
                                    //SE HACE CON UN RECORRIDO A LA TABLA DE TARIFAS QUITANDO LOS REGISTROS PERTENECIENTES A DICHA CATEGORIA
                                    if (ContAdt == 0)
                                    {
                                        bExistePax = false;
                                    }
                                    clsCache cCache = new csCache().cCache();
                                    if (cCache != null && cCache.ParametrosPlanes != null && cCache.ParametrosPlanes.Ocupacion != null)
                                    {
                                        bool bExistePaxAux = true;
                                        int m = 0;
                                        while (m < cCache.ParametrosPlanes.Ocupacion.Count && bExistePaxAux)
                                        {
                                            if (cCache.ParametrosPlanes.Ocupacion[m].Edades != null)
                                            {
                                                int p = 0;
                                                while (p < cCache.ParametrosPlanes.Ocupacion[m].Edades.Count && bExistePaxAux)
                                                {
                                                    string sRefereTipoPax = "";
                                                    try
                                                    {
                                                        sRefereTipoPax = "ADT";//cPlan.ConsultarTipoPaxRangoEdad(cCache.ParametrosPlanes.Ocupacion[m].Edades[p]);
                                                    }
                                                    catch { }
                                                    int iContCoincidencias = 0;
                                                    for (int x = 0; x < dtTarifas.Rows.Count; x++)
                                                    {
                                                        if (dtCat.Rows[y]["idCategoria"].ToString().Equals(dtTarifas.Rows[x]["idCategoria"].ToString()))
                                                        {
                                                            if (dtTarifas.Rows[x]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT")))
                                                                iContCoincidencias++;
                                                        }
                                                    }
                                                    if (iContCoincidencias < 1)
                                                    {
                                                        bExistePaxAux = false;
                                                        bExistePax = false;
                                                    }
                                                    p++;
                                                }
                                                m++;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int x = 0; x < dtTarifas.Rows.Count; x++)
                                    {
                                        if (dtCat.Rows[y]["idCategoria"].ToString().Equals(dtTarifas.Rows[x]["idCategoria"].ToString()))
                                        {
                                            if (dtTarifas.Rows[x]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT")))
                                                ContAdt++;
                                            if (dtTarifas.Rows[x]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN")))
                                                ContCnn++;
                                            if (dtTarifas.Rows[x]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroJunior", "Jnr")))
                                                ContJnr++;
                                            if (dtTarifas.Rows[x]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdt3", "ADT3")))
                                                ContAdt3++;
                                            if (dtTarifas.Rows[x]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdt4", "ADT4")))
                                                ContAdt4++;
                                            if (dtTarifas.Rows[x]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino2", "CNN2")))
                                                ContCnn2++;
                                            if (dtTarifas.Rows[x]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroInfante", "INF")))
                                                ContInf++;
                                        }
                                    }

                                    //EN CASO DE QUE SE QUIERA QUITAR LA CATEGORIA QUE NO TIENE TARIFAS PARA UN TIPO DE PASAJERO DETERMINADO
                                    //SE HACE CON UN RECORRIDO A LA TABLA DE TARIFAS QUITANDO LOS REGISTROS PERTENECIENTES A DICHA CATEGORIA
                                    if (ContAdt == 0)
                                    {
                                        bExistePax = false;
                                    }
                                    if (ContCnn == 0 && (Convert.ToInt32(ddlNinos.SelectedValue) - iPaxCnnAdt) > 0)
                                    {
                                        bExistePax = false;
                                    }
                                    if (sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC") && sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ"))
                                    {
                                        if (ContAdt3 == 0 && Convert.ToInt32(ddlAdultos.SelectedValue) > 2)
                                        {
                                            bExistePax = false;
                                        }
                                        if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")))
                                        {
                                            if (ContJnr == 0 && Convert.ToInt32(ddlJuniors.SelectedValue) > 0)
                                            {
                                                bExistePax = false;
                                            }
                                        }
                                        else
                                        {
                                            if (ddlInfantes != null)
                                            {
                                                if (ContInf == 0 && Convert.ToInt32(ddlInfantes.SelectedValue) > 0)
                                                {
                                                    bExistePax = false;
                                                }
                                            }
                                            if (ContAdt4 == 0 && Convert.ToInt32(ddlAdultos.SelectedValue) > 3)
                                            {
                                                bExistePax = false;
                                            }
                                            if (sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ"))
                                            {
                                                if (TipoPlantilla.Equals(sPlantillaNinos) && Convert.ToInt32(ddlNinos.SelectedValue) > 1 && ContCnn2 == 0)
                                                {
                                                    bExistePax = false;
                                                }
                                            }
                                        }
                                    }
                                    if (!bExistePax)
                                    {
                                        for (int x = 0; x < dtTarifas.Rows.Count; x++)
                                        {
                                            if (dtCat.Rows[y]["idCategoria"].ToString().Equals(dtTarifas.Rows[x]["idCategoria"].ToString()))
                                            {
                                                dtTarifas.Rows.RemoveAt(x);
                                                x--;
                                            }
                                        }
                                        dtCat.Rows.RemoveAt(y);
                                        y--;
                                    }
                                }
                            }

                            #endregion
                        }
                        HttpContext.Current.Session["tblCatTarifas"] = dtCat;
                        if (dtTarifas.Rows.Count == 0)
                            bTodas = false;
                    }
                    else
                    {
                        bPasajeros = true;
                        bTodas = false;
                        if (i == rptPasajeros.Items.Count - 1)
                            sCabinasError = Convert.ToString(i + 1);
                        else
                            sCabinasError = Convert.ToString(i + 1) + ", ";
                    }
                    #endregion
                }


                if (bTodas)
                {
                    #region Llenado de Cabinas
                    lblErrorGen.Text = "";
                    dtCabinas.Columns.Add("NoCabina");
                    dtCabinas.Columns.Add("dtmDesdeFecha");
                    dtCabinas.Columns.Add("dtmHastaFecha");
                    for (int i = 0; i < dtCabinas.Rows.Count; i++)
                    {
                        DropDownList ddlAdultos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlAdultos");
                        DropDownList ddlNinos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlNinos");
                        DropDownList ddlJuniors = (DropDownList)rptPasajeros.Items[i].FindControl("ddlJuniors");

                        if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL")))
                        {
                            dtCabinas.Rows[i]["NoCabina"] = sTextoRes + Convert.ToString(i + 1) + " - Adultos: " + ddlAdultos.SelectedValue + " / Junior: " + ddlJuniors.SelectedValue + " / Niños: " + ddlNinos.SelectedValue + ";";
                        }
                        else
                        {
                            if (ddlNinos.SelectedValue.Equals("") || ddlNinos.SelectedValue.Equals("0"))
                            {
                                if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanApartamento", "APTO")))
                                    dtCabinas.Rows[i]["NoCabina"] = "Huespedes: " + ddlAdultos.SelectedValue + ";";
                                else
                                    dtCabinas.Rows[i]["NoCabina"] = sTextoRes + Convert.ToString(i + 1) + " / Adultos: " + ddlAdultos.SelectedValue + ";";
                            }
                            else
                            {
                                dtCabinas.Rows[i]["NoCabina"] = sTextoRes + Convert.ToString(i + 1) + " / Adultos: " + ddlAdultos.SelectedValue + " - Menores: " + ddlNinos.SelectedValue + ";";
                            }
                        }
                        dtCabinas.Rows[i]["dtmDesdeFecha"] = Convert.ToDateTime(dtCabinas.Rows[i]["Desde"]).ToString("dd MMM yyyy");
                        dtCabinas.Rows[i]["dtmHastaFecha"] = Convert.ToDateTime(dtCabinas.Rows[i]["Hasta"]).ToString("dd MMM yyyy");

                        if (clsValidaciones.GetKeyOrAdd("dConversionMonedaUnica", "False").ToUpper().Equals("TRUE"))
                        {
                            //tRefere.Get(clsValidaciones.GetKeyOrAdd("Moneda", "Moneda"),
                            //    clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP"));
                            //if (tRefere.Respuesta)
                            //{
                            dtCabinas.Rows[i]["strMoneda"] = tblPlan.Rows[0]["strMoneda"].ToString();//tRefere.strDetalle.Value;
                            //}
                        }
                    }
                    rptCabina.DataSource = dtCabinas;
                    rptCabina.DataBind();
                    if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")))
                    {
                        if (sTipoControl.Equals(sNormal))
                        {

                            if (TipoPlantilla.Equals(sPlantillaMultiHotel))
                            {
                                //setConsultarTarifasRotativosMulti(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan);
                                setConsultarTarifasRotativos(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan, true, iPaxCnnAdt);
                            }
                            else
                            {
                                if (TipoPlantilla.Equals(sPlantillaEdades))
                                {
                                    //setConsultarTarifasRotativosEdades(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan);
                                }
                                else
                                {
                                    setConsultarTarifasRotativos(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan, false, iPaxCnnAdt);
                                }
                            }
                        }
                        else
                        {
                            if (TipoPlantilla.Equals(sPlantillaMultiHotel))
                                //setConsultarTarifasCircuitosMulti(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan);
                                setConsultarTarifasCircuitos(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan, true, iPaxCnnAdt);
                            else
                                setConsultarTarifasCircuitos(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan, false, iPaxCnnAdt);
                        }
                    }
                    else
                    {
                        if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")) || sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS")))
                        {
                            setConsultarTarifasToures(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan);
                        }
                        //else
                        //{
                        //    if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanSouvenir", "SOUV")))
                        //    {
                        //        setConsultarTarifasSouvenirs(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan);
                        //    }
                        //    else
                        //    {
                        //        if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE")))
                        //        {
                        //            setConsultarTarifasCruceros(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan, iPaxCnnAdt);
                        //        }
                        //        else
                        //        {
                        //            if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL")))
                        //            {
                        //                setConsultarTarifasHoteles(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan);
                        //            }
                        //            else
                        //            {
                        //                if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ")))
                        //                {
                        //                    setConsultarTarifasParques(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan);
                        //                }
                        //                else
                        //                {
                        //                    setConsultarTarifasApartamentos(PageSource, sFechaIni, sFechaFin, ControlCupos, idPlan, sTipoPlan);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    btnReservar.Visible = true;
                    #endregion
                }

                //se muestra el texto de error
                else
                {
                    #region Llenado de mensajes de error
                    if (bFechas)
                    {
                        lblErrorGen.Text = "Lo sentimos, no hay tarifas disponibles para la fecha seleccionada, por favor contacta a un asesor";
                        rptCabina.DataSource = null;
                        rptCabina.DataBind();
                    }
                    else
                    {
                        if (bPasajeros)
                        {
                            lblErrorGen.Text = "Lo sentimos, no hay tarifas disponibles para la acomodación seleccionada, por favor cambia los parámetros de adultos o de menores, o contacta a un asesor";
                            rptCabina.DataSource = null;
                            rptCabina.DataBind();
                        }
                        else
                        {
                            if (!bExistenEdades)
                            {
                                lblErrorGen.Text = "Lo sentimos, no hay tarifas disponibles para las edades de pasajeros seleccionada, por favor contacta a un asesor";
                                rptCabina.DataSource = null;
                                rptCabina.DataBind();
                            }
                            else
                            {
                                if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")) || sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS")))
                                    lblErrorGen.Text = "Lo sentimos no encontramos tarifas para los parámetros cotizados, por favor cambia los parámetros y cotiza de nuevo o contacta a un asesor";
                                else
                                    lblErrorGen.Text = "La(s) " + sTipoHabError + " " + sCabinasError + " no presenta(n) resultados, por favor cambia los parámetros de cotización o contacta a un asesor";
                                rptCabina.DataSource = null;
                                rptCabina.DataBind();
                                btnReservar.Visible = false;
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setConsultarCabinasTarifas";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        public void setConsultarTarifasCircuitos(UserControl PageSource, string sFechaIni, string sFechaFin, bool ControlCupos,
            string idPlan, string sTipoPlan, bool bMultiHotel, int iPaxCnnAdt)
        {
            try
            {
                HttpContext.Current.Session["tbltarifascontrol"] = null;
                HttpContext.Current.Session["tbltarifas"] = null;
                Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");
                Repeater rptCabina = (Repeater)PageSource.FindControl("rptCabina");
                Button btnReservar = (Button)PageSource.FindControl("btnReservar");
                DataTable dtCabinas = new DataTable();
                csConsultasPlanes cPlan = new csConsultasPlanes();
                DataTable tblTarifasTodas = new DataTable();
                DataTable tblTarifasGuardadas = new DataTable();
                DataTable tblTarifasTodasControl = new DataTable();
                bool bTodas = false;
                bool bFalloControl = false;
                bool bFalloControlAux = false;
                string sCabinasError = "";
                string sTextoRes = "";
                string sTipoHabError = "";

                DataTable tblPlan = (DataTable)PageSource.Session["$TablaDetallePlan"];

                sTextoRes = "";
                sTipoHabError = "habitacion(es)";

                for (int i = 0; i < rptPasajeros.Items.Count; i++)
                {
                    DropDownList ddlAdultos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlAdultos");
                    DropDownList ddlNinos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlNinos");
                    DropDownList ddlJuniors = (DropDownList)rptPasajeros.Items[i].FindControl("ddlJuniors");
                    TextBox txtNumRealAdt = (TextBox)rptPasajeros.Items[i].FindControl("txtNumRealAdt");
                    TextBox txtNumRealCnn = (TextBox)rptPasajeros.Items[i].FindControl("txtNumRealCnn");
                    Repeater rptEdadninos = (Repeater)rptPasajeros.Items[i].FindControl("rptEdadninos");

                    //obtenemos el numero de pasajeros teniendo en cuenta los junior y niños
                    int iNumpax = Convert.ToInt32(ddlAdultos.SelectedValue) + /*Convert.ToInt32(ddlNinos.SelectedValue)*/0 + Convert.ToInt32(ddlJuniors.SelectedValue);
                    iNumpax = iNumpax + iPaxCnnAdt;
                    int iCantCnn = Convert.ToInt32(ddlNinos.SelectedValue) - iPaxCnnAdt;
                    int iCantAdt = Convert.ToInt32(ddlAdultos.SelectedValue) + iPaxCnnAdt;

                    if (txtNumRealAdt != null)
                        txtNumRealAdt.Text = iCantAdt.ToString();

                    if (txtNumRealCnn != null)
                        txtNumRealCnn.Text = iCantCnn.ToString();

                    DataTable dtTarifas = (DataTable)PageSource.Session["$TablaTarifasPlan"];

                    tblTarifasGuardadas = dtTarifas;
                    //se consultan los hoteles asociados a las tarifas del recultado para poder agrupar por hotel
                    DataTable dtHoteles = null;
                    if (bMultiHotel)
                    {
                        dtHoteles = setFiltrarCategorias(dtTarifas, tblPlan.Rows[0]["intProveedor"].ToString(), idPlan);
                        dtHoteles = getHotelesCatTexto(dtHoteles, idPlan);
                    }
                    else
                    {
                        dtHoteles = setConsultarHotelesRes(dtTarifas, idPlan);
                    }

                    if (dtHoteles != null && dtHoteles.Rows.Count > 0)
                    {
                        //si hay resultados llenamos el repetidor de hoteles, si no hay hoteles asociados no se puede continuar el proceso
                        Repeater rptHoteles = (Repeater)rptCabina.Items[i].FindControl("rptHoteles");
                        rptHoteles.DataSource = dtHoteles;
                        rptHoteles.DataBind();

                        //recorremos el repetidor de hoteles
                        for (int a = 0; a < rptHoteles.Items.Count; a++)
                        {
                            Label lblIdCategoria = (Label)rptHoteles.Items[a].FindControl("lblIdCategoria");
                            Label lblIdHotel = (Label)rptHoteles.Items[a].FindControl("lblIdHotel");
                            string sCategoria = lblIdCategoria.Text;
                            //consultamos las tarifas segun la categoria del hotel
                            //dtTarifas = cPlan.ConsultaTarifasCotizador(sCategoria, iNumpax.ToString(), sFechaIni, sFechaFin, sAplicacion);
                            dtTarifas = getFiltrarCategoriasCodigo(sCategoria, tblTarifasGuardadas);
                            //se consultan los impuestos de cada tarifa
                            dtTarifas = setInsertarImpuestosTarifas(dtTarifas, false, 0, Convert.ToInt32(lblIdHotel.Text), idPlan, "");
                            //se llena la tabla general de tarifas por tipo de pax y con el numero de la cabina
                            setLlenarTablaGeneralTarifas(dtTarifas, i, 0, 0, PageSource);
                            //se relacionan los valores de pasajeros e impuestos segun tipo de habitacion
                            dtTarifas = setInsertarTarifasTipoPax(rptEdadninos, dtTarifas, iCantCnn,
                                iCantAdt, Convert.ToInt32(ddlJuniors.SelectedValue), sTipoPlan, i + 1, idPlan, PageSource);
                            if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                            {
                                if (i == 0 && HttpContext.Current.Session["tbltarifascontrol"] == null)
                                {
                                    tblTarifasTodasControl = dtTarifas.Copy();
                                }
                                else
                                {
                                    tblTarifasTodasControl = (DataTable)HttpContext.Current.Session["tbltarifascontrol"];


                                    for (int x = 0; x < dtTarifas.Rows.Count; x++)
                                    {
                                        tblTarifasTodasControl.Rows.Add(dtTarifas.Rows[x].ItemArray);
                                    }
                                }
                                HttpContext.Current.Session["tbltarifascontrol"] = tblTarifasTodasControl;
                                //se controlan los cupos en caso de ser necesario
                                if (ControlCupos)
                                {
                                    dtTarifas = setControlarCupos(sFechaIni, sFechaFin, idPlan, dtTarifas, PageSource);
                                    if (dtTarifas == null || dtTarifas.Rows.Count == 0)
                                    {
                                        bFalloControl = true;
                                        //bTodas = false;
                                    }
                                    else
                                    {
                                        bFalloControlAux = true;
                                        bTodas = true;
                                    }
                                }
                                if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                                {
                                    //ValidarOfertasAsociadas(PageSource, sAplicacion, sFechaIni, sFechaFin,
                                    //    Convert.ToString(Convert.ToInt32(ddlAdultos.SelectedValue) + Convert.ToInt32(ddlJuniors.SelectedValue)), ddlNinos.SelectedValue, "0");
                                    Repeater rptTarifas = (Repeater)rptHoteles.Items[a].FindControl("rptTarifas");
                                    rptTarifas.DataSource = dtTarifas;
                                    rptTarifas.DataBind();

                                    dtTarifas = setLlenarFechasTarifas(dtTarifas, sFechaIni, Convert.ToDateTime(sFechaFin).AddDays(1).ToString(sFormatoFechaBD));

                                    if (/*a == 0 && */HttpContext.Current.Session["tbltarifas"] == null)
                                    {
                                        tblTarifasTodas = dtTarifas.Copy();
                                    }
                                    else
                                    {
                                        tblTarifasTodas = (DataTable)HttpContext.Current.Session["tbltarifas"];
                                        for (int x = 0; x < dtTarifas.Rows.Count; x++)
                                        {
                                            tblTarifasTodas.Rows.Add(dtTarifas.Rows[x].ItemArray);
                                        }
                                    }
                                    HttpContext.Current.Session["tbltarifas"] = tblTarifasTodas;
                                    bTodas = true;
                                }
                                else
                                {
                                    rptHoteles.Items[a].Visible = false;
                                }
                            }
                            else
                            {
                                /*en caso de que no haya tarifas se vuelve invisible el item del hotel.*/
                                rptHoteles.Items[a].Visible = false;
                            }
                        }
                    }
                    if (HttpContext.Current.Session["tbltarifas"] == null)
                    {
                        bTodas = false;
                        tblTarifasTodas = dtTarifas.Copy();
                        if (i == rptPasajeros.Items.Count - 1)
                            sCabinasError = sTextoRes + Convert.ToString(i + 1);
                        else
                            sCabinasError = sTextoRes + Convert.ToString(i + 1) + ", ";
                    }
                }
                Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");
                btnReservar.Visible = true;
                if (!bTodas)
                {
                    if (bFalloControl && !bFalloControlAux)
                    {
                        lblErrorGen.Text = "Lo sentimos, no tenemos cupos disponibles en este momento";
                        rptCabina.DataSource = null;
                        rptCabina.DataBind();
                        btnReservar.Visible = false;
                    }
                    else
                    {
                        lblErrorGen.Text = "La(s) " + sTipoHabError + " " + sCabinasError + " no presenta(n) resultados, por favor cambie los parametros de busqueda";
                        rptCabina.DataSource = null;
                        rptCabina.DataBind();
                        btnReservar.Visible = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setConsultarTarifas";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        public DataTable getFiltrarCategoriasCodigo(string sCategoria, DataTable tblPrincipal)
        {
            DataView dvFiltro = new DataView(tblPrincipal);
            dvFiltro.RowFilter = "IdCategoria = " + sCategoria;
            return dvFiltro.ToTable();
        }

        /// <summary>
        /// Metodo de consulta de tarifas y de validacion de crices de vigencias, ademas de validacion de control de cupos
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="sFechaIni"></param>
        /// <param name="sFechaFin"></param>
        /// <param name="ControlCupos"></param>
        /// <param name="idPlan"></param>
        /// <param name="sTipoPlan"></param>
        /// <param name="bMultiHotel"></param>
        /// <param name="iPaxCnnAdt"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setConsultarTarifasRotativos(UserControl PageSource, string sFechaIni, string sFechaFin, bool ControlCupos,
            string idPlan, string sTipoPlan, bool bMultiHotel, int iPaxCnnAdt)
        {
            try
            {
                HttpContext.Current.Session["tbltarifascontrol"] = null;
                HttpContext.Current.Session["tbltarifas"] = null;
                Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");
                Repeater rptCabina = (Repeater)PageSource.FindControl("rptCabina");
                Button btnReservar = (Button)PageSource.FindControl("btnReservar");
                DropDownList ddlCategoria = (DropDownList)PageSource.FindControl("ddlCategoria");
                DropDownList ddlDuracion = (DropDownList)PageSource.FindControl("ddlDuracion");
                DataTable dtCabinas = new DataTable();
                csConsultasPlanes cPlan = new csConsultasPlanes();
                DataTable tblTarifasTodas = new DataTable();
                DataTable tblTarifasGuardadas = new DataTable();
                DataTable tblTarifasTodasControl = new DataTable();
                bool bTodas = true;
                string sCabinasError = "";
                string sTextoRes = "";
                string sTipoHabError = "";

                DataTable tblPlan = (DataTable)PageSource.Session["$TablaDetallePlan"];

                sTextoRes = "";
                sTipoHabError = "habitacion(es)";

                for (int i = 0; i < rptPasajeros.Items.Count; i++)
                {
                    DropDownList ddlAdultos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlAdultos");
                    DropDownList ddlNinos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlNinos");
                    DropDownList ddlJuniors = (DropDownList)rptPasajeros.Items[i].FindControl("ddlJuniors");
                    TextBox txtNumRealAdt = (TextBox)rptPasajeros.Items[i].FindControl("txtNumRealAdt");
                    TextBox txtNumRealCnn = (TextBox)rptPasajeros.Items[i].FindControl("txtNumRealCnn");
                    Repeater rptEdadninos = (Repeater)rptPasajeros.Items[i].FindControl("rptEdadninos");

                    int iNumpax = Convert.ToInt32(ddlAdultos.SelectedValue) + /*Convert.ToInt32(ddlNinos.SelectedValue)*/0 + Convert.ToInt32(ddlJuniors.SelectedValue);
                    iNumpax = iNumpax + iPaxCnnAdt;
                    int iCantCnn = Convert.ToInt32(ddlNinos.SelectedValue) - iPaxCnnAdt;
                    int iCantAdt = Convert.ToInt32(ddlAdultos.SelectedValue) + iPaxCnnAdt;

                    if (txtNumRealAdt != null)
                        txtNumRealAdt.Text = iCantAdt.ToString();

                    if (txtNumRealCnn != null)
                        txtNumRealCnn.Text = iCantCnn.ToString();

                    DataTable dtTarifas = (DataTable)PageSource.Session["$TablaTarifasPlan"];

                    tblTarifasGuardadas = dtTarifas;

                    tblTarifasGuardadas = dtTarifas;
                    if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                    {
                        //se consultan los hoteles asociados a las tarifas del recultado para poder agrupar por hotel
                        DataTable dtHoteles = null;
                        if (bMultiHotel)
                        {
                            dtHoteles = setFiltrarCategorias(dtTarifas, tblPlan.Rows[0]["intProveedor"].ToString(), idPlan);
                            dtHoteles = getHotelesCatTexto(dtHoteles, idPlan);
                        }
                        else
                        {
                            dtHoteles = setConsultarHotelesRes(dtTarifas, idPlan);
                        }

                        if (dtHoteles != null && dtHoteles.Rows.Count > 0)
                        {
                            //si hay resultados llenamos el repetidor de hoteles, si no hay hoteles asociados no se puede continuar el proceso
                            Repeater rptHoteles = (Repeater)rptCabina.Items[i].FindControl("rptHoteles");
                            rptHoteles.DataSource = dtHoteles;
                            rptHoteles.DataBind();

                            for (int a = 0; a < rptHoteles.Items.Count; a++)
                            {
                                Label lblIdCategoria = (Label)rptHoteles.Items[a].FindControl("lblIdCategoria");
                                Label lblIdHotel = (Label)rptHoteles.Items[a].FindControl("lblIdHotel");
                                string sCategoria = lblIdCategoria.Text;
                                //consultamos las tarifas segun la categoria del hotel
                                //dtTarifas = cPlan.ConsultaTarifasCotizador(sCategoria, iNumpax.ToString(), sFechaIni, Convert.ToInt32(ddlDuracion.SelectedValue) - 1, false, idPlan, sAplicacion);

                                DataView dvFiltro = new DataView(tblTarifasGuardadas);
                                dvFiltro.RowFilter = "IdCategoria = " + sCategoria;
                                dtTarifas = dvFiltro.ToTable();

                                //validamos que las fechas del viaje estene en una misma vigencia, de lo contrario hacemos las validaciones correcpondientes para tomar los valores de 2 vigencias
                                DataTable dtTarifasSecundaria = null;
                                DateTime dtmFechaFinVig = Convert.ToDateTime(dtTarifas.Rows[0]["Hasta"].ToString());
                                DateTime dtmFechaFinal = Convert.ToDateTime(sFechaFin);
                                bool bVigSecundaria = false;
                                if (dtmFechaFinal > dtmFechaFinVig)
                                    bVigSecundaria = true;
                                //dos vigencias
                                #region [Tabla secundaria]
                                if (bVigSecundaria)
                                {
                                    //dtTarifasSecundaria = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(ddlCategoria.SelectedValue), iNumpax.ToString(), sFechaFin, null,
                                    //    null, true, iCantAdt.ToString(), iCantCnn.ToString(), null, null);
                                    dtTarifasSecundaria = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(sCategoria), Convert.ToString(iNumpax + iCantCnn), sFechaFin, null, null, true,
                                        ddlAdultos.SelectedValue, ddlNinos.SelectedValue, ddlCategoria.SelectedValue, null);
                                    if (dtTarifasSecundaria != null && dtTarifasSecundaria.Rows.Count > 0)
                                    {
                                        //consultamos las tarifas de la segunda vigencia segun la categoria del hotel
                                        //dtTarifasSecundaria = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(sCategoria), iNumpax.ToString(), sFechaFin, null,
                                        //    Convert.ToString(Convert.ToInt32(ddlDuracion.SelectedValue) - 1), false, iCantAdt.ToString(), iCantCnn.ToString(), null, null);
                                        //se consultan los impuestos de cada tarifa
                                        dtTarifas = setInsertarImpuestosTarifas(dtTarifas, false, 0, Convert.ToInt32(lblIdHotel.Text), idPlan, "");
                                        dtTarifasSecundaria = setInsertarImpuestosTarifas(dtTarifasSecundaria, false, 0, Convert.ToInt32(lblIdHotel.Text), idPlan, "");
                                        //se calculan los valores del plan utilizando las 2 vigencias de acuerdo a validaciones establecidas 
                                        dtTarifas = setLlenarTablasRotativos(PageSource, dtTarifas, dtTarifasSecundaria, sFechaIni, sFechaFin);
                                    }
                                }
                                #endregion
                                //una vigencia
                                #region [Tabla Unica]
                                else
                                {
                                    int iNochesTarifa = Convert.ToInt32(dtTarifas.Rows[0]["NumeroNoches"].ToString());
                                    int iNochesElegidas = Convert.ToInt32(ddlDuracion.SelectedValue) - 1;
                                    //se consultan los impuestos de cada tarifa
                                    dtTarifas = setInsertarImpuestosTarifas(dtTarifas, true, iNochesElegidas - iNochesTarifa, Convert.ToInt32(lblIdHotel.Text), idPlan, "");
                                    //se calculan los valores del plan de acuerdo a las noches adicionales necesarias
                                    dtTarifas = setCalcularValoresNochesAdic(dtTarifas, iNochesElegidas - iNochesTarifa);
                                }
                                #endregion
                                //se llena la tabla general de tarifas por tipo de pax y con el numero de la cabina
                                setLlenarTablaGeneralTarifas(dtTarifas, i, 0, 0, PageSource);
                                //se relacionan los valores de pasajeros e impuestos segun tipo de habitacion
                                dtTarifas = setInsertarTarifasTipoPax(rptEdadninos, dtTarifas, iCantCnn,
                                    iCantAdt, Convert.ToInt32(ddlJuniors.SelectedValue), sTipoPlan, i + 1, idPlan, PageSource);
                                if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                                {
                                    if (i == 0 && HttpContext.Current.Session["tbltarifascontrol"] == null)
                                    {
                                        tblTarifasTodasControl = dtTarifas.Copy();
                                    }
                                    else
                                    {
                                        tblTarifasTodasControl = (DataTable)HttpContext.Current.Session["tbltarifascontrol"];
                                        for (int x = 0; x < dtTarifas.Rows.Count; x++)
                                        {
                                            tblTarifasTodasControl.Rows.Add(dtTarifas.Rows[x].ItemArray);
                                        }
                                    }
                                    HttpContext.Current.Session["tbltarifascontrol"] = tblTarifasTodasControl;
                                    //se controlan los cupos en caso de ser necesario
                                    if (ControlCupos)
                                    {
                                        dtTarifas = setControlarCupos(sFechaIni, sFechaFin, idPlan, dtTarifas, PageSource);
                                    }
                                    if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                                    {
                                        ValidarOfertasAsociadas(PageSource, sFechaIni, sFechaFin,
                                            Convert.ToString(iCantAdt + Convert.ToInt32(ddlJuniors.SelectedValue)), iCantCnn.ToString(), "0");
                                        Repeater rptTarifas = (Repeater)rptHoteles.Items[a].FindControl("rptTarifas");
                                        rptTarifas.DataSource = dtTarifas;
                                        rptTarifas.DataBind();


                                        dtTarifas = setLlenarFechasTarifas(dtTarifas, sFechaIni, Convert.ToDateTime(sFechaFin).AddDays(1).ToString(sFormatoFechaBD));

                                        if (/*a == 0 && */HttpContext.Current.Session["tbltarifas"] == null)
                                        {
                                            tblTarifasTodas = dtTarifas.Copy();
                                        }
                                        else
                                        {
                                            tblTarifasTodas = (DataTable)HttpContext.Current.Session["tbltarifas"];
                                            for (int x = 0; x < dtTarifas.Rows.Count; x++)
                                            {
                                                tblTarifasTodas.Rows.Add(dtTarifas.Rows[x].ItemArray);
                                            }
                                        }
                                        HttpContext.Current.Session["tbltarifas"] = tblTarifasTodas;
                                    }
                                    else
                                    {
                                        rptHoteles.Items[a].Visible = false;
                                    }
                                }
                                else
                                {
                                    rptHoteles.Items[a].Visible = false;
                                }
                            }
                        }
                    }
                    if (HttpContext.Current.Session["tbltarifas"] == null)
                    {
                        bTodas = false;
                        tblTarifasTodas = dtTarifas.Copy();
                        if (i == rptPasajeros.Items.Count - 1)
                            sCabinasError = sTextoRes + Convert.ToString(i + 1);
                        else
                            sCabinasError = sTextoRes + Convert.ToString(i + 1) + ", ";
                    }
                }
                Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");
                btnReservar.Visible = true;
                if (!bTodas)
                {
                    lblErrorGen.Text = "La(s) " + sTipoHabError + " " + sCabinasError + " no presenta(n) resultados, por favor cambie los parametros de busqueda";
                    rptCabina.DataSource = null;
                    rptCabina.DataBind();
                    btnReservar.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setConsultarTarifas";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        public void setConsultarTarifasToures(UserControl PageSource, string sFechaIni, string sFechaFin, bool ControlCupos, string idPlan, string sTipoPlan)
        {
            try
            {
                Button btnReservar = (Button)PageSource.FindControl("btnReservar");
                HttpContext.Current.Session["tbltarifascontrol"] = null;
                HttpContext.Current.Session["tbltarifas"] = null;
                Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");
                Repeater rptCabina = (Repeater)PageSource.FindControl("rptCabina");
                DropDownList ddlMoneda = (DropDownList)PageSource.FindControl("ddlMoneda");
                DataTable dtCabinas = new DataTable();
                csConsultasPlanes cPlan = new csConsultasPlanes();
                csGenerales cGen = new csGenerales();
                DataTable tblTarifasTodas = new DataTable();
                DataTable tblTarifasTodasControl = new DataTable();
                bool bTodas = true;
                string sCabinasError = "";
                string sTextoRes = "";
                string sTipoHabError = "";


                DataTable tblPlan = (DataTable)PageSource.Session["$TablaDetallePlan"];
                //csAdministrador cAdmin = new csAdministrador();
                decimal dValorConversion = 1;
                string sMonedaOrigen = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                string sMonedaDestino = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                if (ddlMoneda != null)
                {
                    sMonedaDestino = ddlMoneda.SelectedValue;
                }

                sTextoRes = "";
                sTipoHabError = "cabinas(s)";

                for (int i = 0; i < rptPasajeros.Items.Count; i++)
                {
                    DropDownList ddlAdultos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlAdultos");
                    DropDownList ddlNinos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlNinos");
                    DropDownList ddlInfantes = (DropDownList)rptPasajeros.Items[i].FindControl("ddlInfantes");
                    DropDownList ddlDuracion = (DropDownList)PageSource.FindControl("ddlDuracion");
                    //obtenemos el numero de pasajeros
                    int iNumpax = 0;
                    if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")))
                        iNumpax = Convert.ToInt32(ddlAdultos.SelectedValue) + Convert.ToInt32(ddlNinos.SelectedValue);
                    if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS")))
                        iNumpax = Convert.ToInt32(ddlAdultos.SelectedValue);
                    DataTable dtTarifas = new DataTable();
                    //se calculan las noches adicionales si las hay                   
                    if (ddlDuracion != null)
                    {
                        dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), iNumpax.ToString(), sFechaIni, null,
                            ddlDuracion.SelectedValue, false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, null, null);
                        int iNochesAdicionales = Convert.ToInt32(ddlDuracion.SelectedValue) - 1;
                        //se calculan los valores del plan de acuerdo a las noches adicionales necesarias
                        dtTarifas = setInsertarImpuestosTarifas(dtTarifas, true, iNochesAdicionales, 0, idPlan, sTipoPlan);
                        dtTarifas = setCalcularValoresNochesAdic(dtTarifas, iNochesAdicionales);

                        sMonedaOrigen = dtTarifas.Rows[0]["strRefereMoneda"].ToString();
                        //dValorConversion = cAdmin.ConsultarTasaMoneda(clsSesiones.getAplicacion().ToString(),
                        //    sMonedaOrigen, clsSesiones.getIdioma(), sMonedaDestino);
                        //se llena la tabla general de tarifas por tipo de pax y con el numero de la cabina                       
                        setLlenarTablaGeneralTarifas(dtTarifas, i + 1, 1, dValorConversion, PageSource);
                    }
                    else
                    {
                        dtTarifas = (DataTable)PageSource.Session["$TablaTarifasPlan"];
                        //dtTarifas = cPlan.ConsultaTarifasCotizador(Convert.ToInt32(idPlan), iNumpax.ToString(), sFechaIni, null,
                        //    "", false, ddlAdultos.SelectedValue, ddlNinos.SelectedValue, null, null);
                        //se consultan los impuestos de cada tarifa
                        dtTarifas = setInsertarImpuestosTarifas(dtTarifas, false, 0, 0, idPlan, "");
                        sMonedaOrigen = dtTarifas.Rows[0]["strRefereMoneda"].ToString();
                        //dValorConversion = cAdmin.ConsultarTasaMoneda(clsSesiones.getAplicacion().ToString(),
                        //    sMonedaOrigen, clsSesiones.getIdioma(), sMonedaDestino);
                        if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")))
                        {
                            //se llena la tabla general de tarifas por tipo de pax y con el numero de la cabina
                            setLlenarTablaGeneralTarifas(dtTarifas, i + 1, iNumpax, dValorConversion, PageSource);
                        }
                        else
                        {
                            //se llena la tabla general de tarifas por tipo de pax y con el numero de la cabina
                            setLlenarTablaGeneralTarifas(dtTarifas, i + 1, 1, dValorConversion, PageSource);
                        }
                    }


                    //se relacionan los valores de pasajeros e impuestos segun tipo de habitacion
                    if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")))
                        dtTarifas = setInsertarTarifasTipoPax(null, dtTarifas, Convert.ToInt32(ddlNinos.SelectedValue),
                            Convert.ToInt32(ddlAdultos.SelectedValue), 0, sTipoPlan, i + 1, idPlan, PageSource);
                    if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS")))
                        dtTarifas = setInsertarTarifasTipoPax(null, dtTarifas, 0,
                            Convert.ToInt32(ddlAdultos.SelectedValue), 0, sTipoPlan, i + 1, idPlan, PageSource);

                    if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                    {
                        if (i == 0 && HttpContext.Current.Session["tbltarifascontrol"] == null)
                        {
                            tblTarifasTodasControl = dtTarifas.Copy();
                        }
                        else
                        {
                            tblTarifasTodasControl = (DataTable)HttpContext.Current.Session["tbltarifascontrol"];
                            for (int x = 0; x < dtTarifas.Rows.Count; x++)
                            {
                                tblTarifasTodasControl.Rows.Add(dtTarifas.Rows[x].ItemArray);
                            }
                        }
                        HttpContext.Current.Session["tbltarifascontrol"] = tblTarifasTodasControl;
                        //se controlan los cupos en caso de ser necesario
                        if (ControlCupos)
                        {
                            dtTarifas = setControlarCupos(sFechaIni, sFechaFin, idPlan, dtTarifas, PageSource);
                        }
                        if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                        {
                            Repeater rptTarifas = (Repeater)rptCabina.Items[i].FindControl("rptTarifas");
                            rptTarifas.DataSource = dtTarifas;
                            rptTarifas.DataBind();

                            dtTarifas = setLlenarFechasTarifas(dtTarifas, sFechaIni, Convert.ToDateTime(sFechaFin).AddDays(1).ToString(sFormatoFechaBD));

                            if (/*i == 0 && */HttpContext.Current.Session["tbltarifas"] == null)
                            {
                                tblTarifasTodas = dtTarifas.Copy();
                            }
                            else
                            {
                                tblTarifasTodas = (DataTable)HttpContext.Current.Session["tbltarifas"];
                                for (int x = 0; x < dtTarifas.Rows.Count; x++)
                                {
                                    tblTarifasTodas.Rows.Add(dtTarifas.Rows[x].ItemArray);
                                }
                            }
                            HttpContext.Current.Session["tbltarifas"] = tblTarifasTodas;
                        }
                        //else
                        //{
                        //    bTodas = false;
                        //    if (i == rptPasajeros.Items.Count - 1)
                        //        sCabinasError = sTextoRes + Convert.ToString(i + 1);
                        //    else
                        //        sCabinasError = sTextoRes + Convert.ToString(i + 1) + ", ";
                        //}
                    }
                    if (HttpContext.Current.Session["tbltarifas"] == null)
                    {
                        bTodas = false;
                        tblTarifasTodas = dtTarifas.Copy();
                        if (i == rptPasajeros.Items.Count - 1)
                            sCabinasError = sTextoRes + Convert.ToString(i + 1);
                        else
                            sCabinasError = sTextoRes + Convert.ToString(i + 1) + ", ";
                    }
                }
                btnReservar.Visible = true;
                Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");
                if (!bTodas)
                {
                    lblErrorGen.Text = "No hay resultados que mostrar para la consulta especificada";
                    rptCabina.DataSource = null;
                    rptCabina.DataBind();
                    btnReservar.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setConsultarTarifas";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que filtra las categorias que existan, excluyendo las categorias repetidas
        /// </summary>
        /// <param name="tblTarifas"></param>
        /// <param name="idProv"></param>
        /// <param name="idPlan"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setFiltrarCategorias(DataTable tblTarifas, string idProv, string idPlan)
        {
            try
            {
                DataView dvCategorias = new DataView();
                dvCategorias.Table = tblTarifas.Copy();
                DataTable tblCategorias = dvCategorias.ToTable(true, "idcategoria");
                tblCategorias.Columns.Add("strNombre");
                tblCategorias.Columns.Add("intProveedor");
                tblCategorias.Columns.Add("intidPlan");
                for (int i = 0; i < tblCategorias.Rows.Count; i++)
                {
                    tblCategorias.Rows[i]["strNombre"] = clsValidaciones.GetKeyOrAdd("TextoCatRotativoaMulti", "Opcion hoteles") + " " + Convert.ToString(i + 1);
                    tblCategorias.Rows[i]["intProveedor"] = idProv;
                    tblCategorias.Rows[i]["intidPlan"] = idPlan;
                }
                return tblCategorias;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setFiltrarCategorias";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return null;
            }
        }

        /// <summary>
        /// Metodo que obtiene el nombre de los hoteles de una respectiva categoria concatenados en un unico string
        /// </summary>
        /// <param name="tblCat"></param>
        /// <param name="idPlan"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable getHotelesCatTexto(DataTable tblCat, string idPlan)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                tblCat.Columns.Add("strHoteles");
                csConsultasPlanes cPlan = new csConsultasPlanes();
                for (int i = 0; i < tblCat.Rows.Count; i++)
                {
                    DataTable tHotelesCat = cPlan.CosultarHoteles(tblCat.Rows[i]["idcategoria"].ToString());

                    string strHoteles = "";
                    if (tHotelesCat != null)
                    {
                        for (int x = 0; x < tHotelesCat.Rows.Count; x++)
                        {
                            if (x != tHotelesCat.Rows.Count - 1)
                                strHoteles = strHoteles + tHotelesCat.Rows[x]["Nombre"].ToString() + ", ";
                            else
                                strHoteles = strHoteles + tHotelesCat.Rows[x]["Nombre"].ToString();
                        }
                    }
                    tblCat.Rows[i]["strHoteles"] = strHoteles;
                }
                return tblCat;
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "setDetallesHotelesMulti";
                ExceptionHandled.Publicar(cParametros);
                return null;
            }
        }

        /// <summary>
        /// Metodo que llama a la consulta de los hoteles de cada una de las categorias de las tarifas
        /// </summary>
        /// <param name="tblTarifas"></param>
        /// <param name="idPlan"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setConsultarHotelesRes(DataTable tblTarifas, string idPlan)
        {
            try
            {
                DataTable tblHoteles = null;
                DataView dvCategorias = new DataView();
                dvCategorias.Table = tblTarifas.Copy();
                DataTable tblCategorias = dvCategorias.ToTable(true, "idcategoria");
                // DataTable tblCategorias = (DataTable)HttpContext.Current.Session["tblCatTarifas"];
                csConsultasPlanes cPlan = new csConsultasPlanes();
                for (int i = 0; i < tblCategorias.Rows.Count; i++)
                {
                    DataTable tHotelesCat = cPlan.CosultarHoteles(tblCategorias.Rows[i]["idcategoria"].ToString());
                    if (tblHoteles == null)
                    {
                        tblHoteles = tHotelesCat.Copy();
                    }
                    else
                    {
                        tblHoteles.Merge(tHotelesCat);
                    }
                }
                return tblHoteles;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setConsultarHotelesRes";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return null;
            }
        }

        /// <summary>
        /// Metodo que consulta, hace el llamado al metodo de calculo e inserta el valor de los impuestos de una determinada tarifa
        /// </summary>
        /// <param name="tblTarifas"></param>
        /// <param name="bNochesAdic"></param>
        /// <param name="iCantNoches"></param>
        /// <param name="iProveedor"></param>
        /// <param name="idPlan"></param>
        /// <param name="sTipoPlan"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setInsertarImpuestosTarifas(DataTable tblTarifas, bool bNochesAdic, int iCantNoches, int iProveedor, string idPlan, string sTipoPlan)
        {
            try
            {
                csConsultasPlanes cPlan = new csConsultasPlanes();
                tblTarifas.Columns.Add("tImpuestos", tblTarifas.GetType());
                tblTarifas.Columns.Add("intProveedor");
                DataTable tblFicticia = null;

                for (int i = 0; i < tblTarifas.Rows.Count; i++)
                {
                    if (iCantNoches == 0 && tblTarifas.Columns.Contains("iNochesAplicaVig"))
                        iCantNoches = Convert.ToInt32(tblTarifas.Rows[i]["iNochesAplicaVig"].ToString());

                    string sTarifa = tblTarifas.Rows[i]["IdTarifa"].ToString();
                    DataTable tImpuestos = cPlan.ConsultarImpuestosTarifa(sTarifa);

                    if (tImpuestos != null)
                    {
                        if (tblFicticia == null)
                            tblFicticia = tImpuestos.Clone();
                        if (bNochesAdic)
                        {
                            if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS")))
                            {
                                tImpuestos = setValidarValorImpNochesAdic(tImpuestos, iCantNoches, true);
                                tImpuestos = setValidarValorImp(tImpuestos, 1, false);
                            }
                            else
                            {
                                //if (iCantNoches > 0)
                                //{
                                //    tImpuestos = setValidarValorImpNochesAdic(tImpuestos, iCantNoches, true);
                                //}
                                //tImpuestos = setValidarValorImp(tImpuestos, 1);
                            }
                            if (iCantNoches > 0)
                                tImpuestos = setValidarValorImpPrimaria(tImpuestos, false, true, iCantNoches);
                            else
                                tImpuestos = setValidarValorImpPrimaria(tImpuestos, false, false, 1);
                        }
                        tblTarifas.Rows[i]["tImpuestos"] = tImpuestos;
                    }
                    else
                    {
                        if (tblFicticia == null)
                        {
                            tblFicticia = new DataTable();
                            tblFicticia.Columns.Add("cFicticia");
                        }
                        tblTarifas.Rows[i]["tImpuestos"] = tblFicticia;
                    }
                    tblTarifas.Rows[i]["intProveedor"] = iProveedor;
                }
                return tblTarifas;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setInsertarImpuestosTarifas";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return tblTarifas;
            }
        }

        /// <summary>
        /// Metodo que calcula el valor de los impuestos de la noche adicional, ademas de validar el 
        /// tipo de calculo (Neto o porcentual) y generar la respectiva operacion matematica multiplicandolo por el numero de noches
        /// </summary>
        /// <param name="tblPrincip"></param>
        /// <param name="iCantNoches"></param>
        /// <param name="bNoche"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setValidarValorImpNochesAdic(DataTable tblPrincip, int iCantNoches, bool bNoche)
        {
            DataTable tblPrincipal = tblPrincip.Copy();
            try
            {
                for (int i = 0; i < tblPrincipal.Rows.Count; i++)
                {
                    //si es impuesto para la noche adicional se calcula
                    if (tblPrincipal.Rows[i]["strRefereTipoImp"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoImpuestoPlanNoche", "TPO_IMP_NOCHE")))
                    {
                        //operacion para valores en porcentaje
                        if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("0"))
                        {
                            if (bNoche)
                            {
                                tblPrincipal.Rows[i]["dblvalor"] = (Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) * iCantNoches);
                            }
                            else
                            {
                                tblPrincipal.Rows[i]["dblvalor"] = ((Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) / 100) * Convert.ToDecimal(tblPrincipal.Rows[i]["Tarifa"].ToString().Replace(",",
                                    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")))) * iCantNoches;
                                tblPrincipal.Rows[i]["FormaCalculo"] = "1";
                            }
                        }
                        else
                        {
                            //operacion para valores netos
                            if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("1"))
                            {
                                tblPrincipal.Rows[i]["dblvalor"] = (Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) * iCantNoches);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarValorImp";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
            return tblPrincipal;
        }

        /// <summary>
        /// Metodo que calcula el valor de los impuestos ademas de validar el tipo de calculo (Neto o porcentual) 
        /// y generar la respectiva operacion matematica multiplicandolo por el numero de pasajeros
        /// </summary>
        /// <param name="tblPrincip"></param>
        /// <param name="iCantPax"></param>
        /// <param name="bCargos"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setValidarValorImp(DataTable tblPrincip, int iCantPax, bool bCargos)
        {
            DataTable tblPrincipal = tblPrincip.Copy();
            try
            {
                csConsultasPlanes cPlan = new csConsultasPlanes();
                decimal dBaseValor = 0;
                decimal dvalorCargos = 0;
                for (int i = 0; i < tblPrincipal.Rows.Count; i++)
                {
                    dBaseValor = 0;
                    //operacion para valores de porcentaje
                    if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("0"))
                    {
                        tblPrincipal.Rows[i]["dblvalor"] = ((Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString()) / 100) * Convert.ToDecimal(tblPrincipal.Rows[i]["Tarifa"].ToString())) * iCantPax;
                        tblPrincipal.Rows[i]["FormaCalculo"] = "2";
                    }
                    //operacion para valores de netos
                    if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("1"))
                    {
                        tblPrincipal.Rows[i]["dblvalor"] = (Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString()) * iCantPax);
                        tblPrincipal.Rows[i]["FormaCalculo"] = "2";
                    }

                    if (bCargos)
                    {
                        //dBaseValor = Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                        //    sCaracterDecimal).Replace(".", sCaracterDecimal));
                        //dvalorCargos = 0;
                        //DataTable tCargos = cPlan.ConsultarCargosImpuesto(tblPrincipal.Rows[i]["intidImpuesto"].ToString(), sAplicacion);
                        //if (tCargos != null && tCargos.Rows.Count > 0)
                        //{
                        //    for (int a = 0; a < tCargos.Rows.Count; a++)
                        //    {
                        //        if (tCargos.Rows[i]["intProxNivel"].ToString().Equals("0"))
                        //        {
                        //            dvalorCargos += (dBaseValor * (Convert.ToDecimal(tCargos.Rows[i]["dblvalor"].ToString().Replace(",",
                        //                sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100));
                        //        }
                        //        //operacion para valores de netos
                        //        if (tCargos.Rows[i]["intProxNivel"].ToString().Equals("1"))
                        //        {
                        //            dvalorCargos += Convert.ToDecimal(tCargos.Rows[i]["dblvalor"].ToString().Replace(",",
                        //                sCaracterDecimal).Replace(".", sCaracterDecimal)) * iCantPax;
                        //        }
                        //    }
                        //}
                    }

                    tblPrincipal.Rows[i]["dblvalor"] = Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                        sCaracterDecimal).Replace(".", sCaracterDecimal)) + dvalorCargos;
                }
                return tblPrincipal;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarValorImp";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return tblPrincipal;
            }
        }

        /// <summary>
        /// Metodo que calcula el valor de los impuestos ademas de validar el tipo de calculo (Neto o porcentual) 
        /// y generar la respectiva operacion matematica multiplicandolo por el numero de pasajeros
        /// </summary>
        /// <param name="tblPrincip"></param>
        /// <param name="iCantPax"></param>
        /// <param name="bCargos"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setValidarValorImpTablasNoches(DataTable tblPrincip, int iCantPax, bool bCargos, bool bNoche)
        {
            DataTable tblPrincipal = tblPrincip.Copy();
            try
            {
                csConsultasPlanes cPlan = new csConsultasPlanes();
                decimal dvalorCargos = 0;
                for (int i = 0; i < tblPrincipal.Rows.Count; i++)
                {
                    if (!bNoche)
                    {
                        //operacion para valores de porcentaje
                        if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("0"))
                        {
                            tblPrincipal.Rows[i]["dblvalor"] = ((Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString()) / 100) * Convert.ToDecimal(tblPrincipal.Rows[i]["Tarifa"].ToString())) * iCantPax;
                            tblPrincipal.Rows[i]["FormaCalculo"] = "1";
                        }
                        //operacion para valores de netos
                        if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("1"))
                        {
                            tblPrincipal.Rows[i]["dblvalor"] = (Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString()) * iCantPax);
                            //tblPrincipal.Rows[i]["FormaCalculo"] = "2";
                        }
                    }
                    else
                    {
                        //operacion para valores de porcentaje
                        if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("0"))
                        {
                            tblPrincipal.Rows[i]["dblvalor"] = ((Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString()) / 100) * Convert.ToDecimal(tblPrincipal.Rows[i]["dblTarifaNocheAdic"].ToString())) * iCantPax;
                            tblPrincipal.Rows[i]["FormaCalculo"] = "1";
                        }
                        //operacion para valores de netos
                        if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("1"))
                        {
                            tblPrincipal.Rows[i]["dblvalor"] = (Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString()) * iCantPax);
                            //tblPrincipal.Rows[i]["FormaCalculo"] = "2";
                        }
                    }

                    if (bCargos)
                    {
                        //dBaseValor = Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                        //    sCaracterDecimal).Replace(".", sCaracterDecimal));
                        //dvalorCargos = 0;
                        //DataTable tCargos = cPlan.ConsultarCargosImpuesto(tblPrincipal.Rows[i]["intidImpuesto"].ToString(), sAplicacion);
                        //if (tCargos != null && tCargos.Rows.Count > 0)
                        //{
                        //    for (int a = 0; a < tCargos.Rows.Count; a++)
                        //    {
                        //        if (tCargos.Rows[i]["intProxNivel"].ToString().Equals("0"))
                        //        {
                        //            dvalorCargos += (dBaseValor * (Convert.ToDecimal(tCargos.Rows[i]["dblvalor"].ToString().Replace(",",
                        //                sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100));
                        //        }
                        //        //operacion para valores de netos
                        //        if (tCargos.Rows[i]["intProxNivel"].ToString().Equals("1"))
                        //        {
                        //            dvalorCargos += Convert.ToDecimal(tCargos.Rows[i]["dblvalor"].ToString().Replace(",",
                        //                sCaracterDecimal).Replace(".", sCaracterDecimal)) * iCantPax;
                        //        }
                        //    }
                        //}
                    }

                    tblPrincipal.Rows[i]["dblvalor"] = Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                        sCaracterDecimal).Replace(".", sCaracterDecimal)) + dvalorCargos;
                }
                return tblPrincipal;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarValorImp";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return tblPrincipal;
            }
        }

        /// <summary>
        /// se calcula el valor de los impuestos de la vigencia principal validando si se deben sumar o restar noches utilizando los impuestos de la noche adicional
        /// </summary>
        /// <param name="tblPrincip">Tabla de impuestos</param>
        /// <param name="bResta">indicador de suma o resta</param>
        /// <param name="iCantNoches">cantidad de noches</param>
        /// <returns>Tabla de impuestos</returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-27
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable setValidarValorImpPrimaria(DataTable tblPrincip, bool bResta, bool bNocheAdic, int iCantNoches)
        {
            DataTable tblPrincipal = null;
            try
            {
                DataTable tblImpTarifa = new DataTable();
                DataTable tblImpNoche = new DataTable();
                DataView dvFiltroNoche = new DataView();
                //se filtran los impuestos para la noche adicional

                dvFiltroNoche.Table = tblPrincip.Copy();
                dvFiltroNoche.RowFilter = "strRefereTipoImp = '" + clsValidaciones.GetKeyOrAdd("TipoImpuestoPlanNoche", "TPO_IMP_NOCHE") + "'";
                tblImpNoche = dvFiltroNoche.ToTable();

                dvFiltroNoche.Table = tblPrincip.Copy();
                dvFiltroNoche.RowFilter = "strRefereTipoImp = '" + clsValidaciones.GetKeyOrAdd("TipoImpuestoPlanTarifa", "TPO_IMP_TARIFA") + "'";
                tblImpTarifa = dvFiltroNoche.ToTable();

                //se calculan los valores para noches y para tarifa por separado
                tblImpNoche = setValidarValorImpTablasNoches(tblImpNoche, 1, true, true);
                tblImpTarifa = setValidarValorImpTablasNoches(tblImpTarifa, 1, true, false);

                //se suman los valores agrupandolos por impuesto
                if (bNocheAdic)
                    tblPrincipal = setGenerarTablaImpSec(tblImpTarifa, tblImpNoche, bResta, true, iCantNoches);
                else
                    tblPrincipal = tblImpTarifa;
                return tblPrincipal;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarValorImp";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return tblPrincipal;
            }
        }

        /// <summary>
        /// Metodo que calcula los valores de una tabla de impuestos secundaria (Noche adicional)
        /// </summary>
        /// <param name="tblPrincip">Tabla de impuestos</param>
        /// <param name="tblSec">Tabla de impuestos noche adicional</param>
        /// <param name="bResta">Indicador de suma o resta</param>
        /// <param name="bAdicionales">Indicador de noches adicionales</param>
        /// <param name="iCantNoches">Numero de noches adicionales</param>
        /// <returns>Tabla de impuestos</returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-27
        /// -------------------
        /// Control de Cambios
        /// -------------------       
        /// </remarks>
        public DataTable setGenerarTablaImpSec(DataTable tblPrincip, DataTable tblSec, bool bResta, bool bAdicionales, int iCantNoches)
        {
            try
            {
                DataTable tblPrincipal = tblPrincip.Copy();
                DataTable tblSecundaria = tblSec.Copy();
                if (iCantNoches > 0)
                {
                    for (int i = 0; i < tblPrincipal.Rows.Count; i++)
                    {
                        for (int x = 0; x < tblSecundaria.Rows.Count; x++)
                        {
                            //si el impuesto coincide se suma
                            if (tblPrincipal.Rows[i]["Impuesto"].ToString().Equals(tblSecundaria.Rows[x]["Impuesto"].ToString()))
                            {
                                //se restan los valores porque son noches sobrantes
                                if (bResta)
                                {
                                    tblPrincipal.Rows[i]["dblvalor"] = Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString()) - Convert.ToDecimal(tblSecundaria.Rows[x]["dblvalor"].ToString());
                                }
                                //se suman los valores porque son noches adicionales
                                else
                                {
                                    if (bAdicionales)
                                    {
                                        tblPrincipal.Rows[i]["dblvalor"] = Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString()) + Convert.ToDecimal(tblSecundaria.Rows[x]["dblvalor"].ToString());
                                        //tblPrincipal.Rows[i]["Tarifa"] = Convert.ToDecimal(tblPrincipal.Rows[i]["Tarifa"].ToString().Replace(",",
                                        //    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) +
                                        //    (Convert.ToDecimal(tblPrincipal.Rows[i]["dblTarifaNocheAdic"].ToString().Replace(",",
                                        //    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) * iCantNoches);
                                    }
                                    else
                                    {
                                        tblPrincipal.Rows[i]["dblvalor"] = Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString()) + Convert.ToDecimal(tblSecundaria.Rows[x]["dblvalor"].ToString());
                                    }
                                }
                                tblSecundaria.Rows.RemoveAt(x);
                                x--;
                            }
                        }
                    }

                    //si sobra algun impuesto que no se haya podido agrupar se pone solo
                    for (int x = 0; x < tblSecundaria.Rows.Count; x++)
                    {
                        tblPrincipal.Rows.Add(tblSecundaria.Rows[x].ItemArray);
                    }
                }
                return tblPrincipal;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setGenerarTablaImpSec";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return tblPrincip;
            }
        }

        /// <summary>
        /// Metodo principal para la validacion de combinacion de vigencias, en este metodo se llaman los metodos de validacion
        /// de tabla principal y secundaria y de calculo de valores de cada una de estas 2 tablas
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="tUno"></param>
        /// <param name="tDos"></param>
        /// <param name="sFechaIni"></param>
        /// <param name="sFechaFin"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setLlenarTablasRotativos(UserControl PageSource, DataTable tUno, DataTable tDos, string sFechaIni, string sFechaFin)
        {
            try
            {
                DataTable dtCompleta = new DataTable();
                //se determina la vigencia principal
                DataSet dsTablasOrden = setPrioridadTablas(tUno, tDos, sFechaIni, sFechaFin);
                if (dsTablasOrden.Tables.Count > 0)
                {
                    //set calculan Los valores de las noches correspondientes a cada vigencia
                    dtCompleta = setCalcularValoresSec(dsTablasOrden, sFechaIni, sFechaFin);
                }
                return dtCompleta;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setLlenarTablasRotativos";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return null;
            }
        }

        /// <summary>
        /// Metodo que determina la prioridad de las 2 tablas de combinacion de vigencias, 
        /// dependiendo del numero de dias que tome de cada vigencia
        /// </summary>
        /// <param name="tUno"></param>
        /// <param name="tDos"></param>
        /// <param name="sFechaIni"></param>
        /// <param name="sFechaFin"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataSet setPrioridadTablas(DataTable tUno, DataTable tDos, string sFechaIni, string sFechaFin)
        {
            DataSet dsTablas = new DataSet();
            try
            {
                DataTable dtPrimaria = new DataTable();
                DataTable dtSecundaria = new DataTable();
                DateTime dtmFechaIniVig = Convert.ToDateTime(tDos.Rows[0]["Desde"].ToString());
                DateTime dtmFechaFinVig = Convert.ToDateTime(tUno.Rows[0]["Hasta"].ToString());
                //se valida cual tiene mas noches
                int iDiast1 = clsValidaciones.getNochesDiferencia(sFechaIni, dtmFechaFinVig.ToString(clsSesiones.getFormatoFechaBD()));
                int iDiast2 = clsValidaciones.getNochesDiferencia(dtmFechaIniVig.ToString(clsSesiones.getFormatoFechaBD()), sFechaFin);
                tUno.Columns.Add("intDiasCorresp");
                tDos.Columns.Add("intDiasCorresp");
                for (int i = 0; i < tUno.Rows.Count; i++)
                {
                    tUno.Rows[i]["intDiasCorresp"] = iDiast1.ToString();
                }
                for (int i = 0; i < tDos.Rows.Count; i++)
                {
                    tDos.Rows[i]["intDiasCorresp"] = iDiast2.ToString();
                }
                //if (iDiast1 > iDiast2)
                //{
                dtPrimaria = tUno.Copy();
                dtSecundaria = tDos.Copy();
                //}
                //else
                //{
                //    if (iDiast1 < iDiast2)
                //    {
                //        dtPrimaria = tDos.Copy();
                //        dtSecundaria = tUno.Copy();
                //    }
                //    //si tienen el mismo numero de noches se valida cual es la mas cara y se deja como principal
                //    else
                //    {
                //        DataView dvFiltroADT = new DataView();
                //        dvFiltroADT.Table = tUno;
                //        dvFiltroADT.RowFilter = "strRefereTipoPasajero = '" + clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT") + "'";
                //        decimal dblValor1 = Convert.ToDecimal(dvFiltroADT.ToTable().Rows[0]["Tarifa"].ToString().Replace(",",
                //            clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")));
                //        dvFiltroADT.Table = tDos;
                //        dvFiltroADT.RowFilter = "strRefereTipoPasajero = '" + clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT") + "'";
                //        decimal dblValor2 = Convert.ToDecimal(dvFiltroADT.ToTable().Rows[0]["Tarifa"].ToString().Replace(",",
                //             clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")));
                //        if (dblValor1 > dblValor2)
                //        {
                //            dtPrimaria = tUno.Copy();
                //            dtSecundaria = tDos.Copy();
                //        }
                //        else
                //        {
                //            if (dblValor1 < dblValor2)
                //            {
                //                dtPrimaria = tDos.Copy();
                //                dtSecundaria = tUno.Copy();
                //            }
                //            else
                //            {
                //                dtPrimaria = tUno.Copy();
                //                dtSecundaria = tDos.Copy();
                //            }
                //        }
                //    }
                //}
                dsTablas.Tables.Add(dtPrimaria);
                dsTablas.Tables[0].TableName = "tblPrimaria";
                dsTablas.Tables.Add(dtSecundaria);
                dsTablas.Tables[1].TableName = "tblSecundaria";
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setPrioridadTablas";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
            return dsTablas;
        }

        /// <summary>
        /// Metodo que calcula el valor de la tabla de la vigencia secundaria segun el numero de noches que tome de la misma
        /// </summary>
        /// <param name="dsDatos"></param>
        /// <param name="sFechaIni"></param>
        /// <param name="sFechaFin"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setCalcularValoresSec(DataSet dsDatos, string sFechaIni, string sFechaFin)
        {
            try
            {
                DataTable tblPrimaria = dsDatos.Tables[0];
                DataTable tblSecundaria = dsDatos.Tables[1];

                for (int i = 0; i < tblPrimaria.Rows.Count; i++)
                {
                    bool bRestar = true;
                    DataTable tblImpPrincipal = null;
                    int iDiasSobrantes = Convert.ToInt32(tblPrimaria.Rows[i]["NumeroNoches"].ToString()) - Convert.ToInt32(tblPrimaria.Rows[i]["intDiasCorresp"].ToString());

                    //se obtienen los dias sobrantes o demas de la tarifa y se calcula el valor de los impuestos
                    if (iDiasSobrantes < 0)
                    {
                        //si los dias son de mas se suman los valores de los impuestos de las noches a los de la tarifa la tarifa
                        iDiasSobrantes = iDiasSobrantes * -1;
                        tblPrimaria.Rows[i]["tImpuestos"] = setValidarValorImpNochesAdicComb(((DataTable)tblPrimaria.Rows[i]["tImpuestos"]), iDiasSobrantes, false);
                        tblPrimaria.Rows[i]["tImpuestos"] = setValidarValorImpPrimaria(((DataTable)tblPrimaria.Rows[i]["tImpuestos"]), false, true, 1);
                        bRestar = false;
                    }
                    else
                    {
                        //si los dias son sobrantes se restan los valores de los impuestos de las noches a los de la tarifa la tarifa
                        if (iDiasSobrantes == 0)
                        {
                            tblPrimaria.Rows[i]["tImpuestos"] = setValidarValorImpNochesAdicComb(((DataTable)tblPrimaria.Rows[i]["tImpuestos"]), iDiasSobrantes, false);
                            tblPrimaria.Rows[i]["tImpuestos"] = setValidarValorImpPrimaria(((DataTable)tblPrimaria.Rows[i]["tImpuestos"]), true, true, iDiasSobrantes);
                        }
                        else
                        {
                            tblPrimaria.Rows[i]["tImpuestos"] = setValidarValorImpNochesAdicComb(((DataTable)tblPrimaria.Rows[i]["tImpuestos"]), iDiasSobrantes, false);
                            tblPrimaria.Rows[i]["tImpuestos"] = setValidarValorImpPrimaria(((DataTable)tblPrimaria.Rows[i]["tImpuestos"]), true, true, 1);
                        }
                    }
                    //se obtienen los valores de la tarifa y la noche adicional
                    decimal dblTarifa = Convert.ToDecimal(tblPrimaria.Rows[i]["Tarifa"].ToString().Replace(",",
                            clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")));
                    decimal dblTarifaNoche = Convert.ToDecimal(tblPrimaria.Rows[i]["TarifaNocheAdic"].ToString().Replace(",",
                            clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")));
                    decimal dblPrecio = Convert.ToDecimal(tblPrimaria.Rows[i]["Precio"].ToString().Replace(",",
                            clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")));
                    decimal dblPrecioNoche = Convert.ToDecimal(tblPrimaria.Rows[i]["NocheAdic"].ToString().Replace(",",
                            clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")));
                    decimal dblTotalTarifa = 0;
                    decimal dblTotalPrecio = 0;
                    //si los dias son sobrantes se restan los valores  de las noches a la tarifa la tarifa
                    if (bRestar)
                    {
                        dblTotalTarifa = dblTarifa - (dblTarifaNoche * iDiasSobrantes);
                        dblTotalPrecio = dblPrecio - (dblPrecioNoche * iDiasSobrantes);
                    }
                    //si los dias son de mas se suman los valores  de las noches a la tarifa la tarifa
                    else
                    {
                        dblTotalTarifa = dblTarifa + (dblTarifaNoche * iDiasSobrantes);
                        dblTotalPrecio = dblPrecio + (dblPrecioNoche * iDiasSobrantes);
                    }
                    for (int x = 0; x < tblSecundaria.Rows.Count; x++)
                    {
                        //se calculan los dias correspondientes a la segunda vigencia y se calcula el valor multiplicandolos por el valor de la noche adicional
                        int iDiasSobrantesSec = Convert.ToInt32(tblSecundaria.Rows[x]["intDiasCorresp"].ToString());
                        if (i == 0)
                        {
                            tblSecundaria.Rows[x]["tImpuestos"] = setValidarValorImpNochesAdicCombSec(((DataTable)tblSecundaria.Rows[x]["tImpuestos"]), iDiasSobrantesSec, false);
                        }
                        if (tblPrimaria.Rows[i]["strRefereTipoPasajero"].ToString().Equals(tblSecundaria.Rows[x]["strRefereTipoPasajero"].ToString()) &&
                            tblPrimaria.Rows[i]["EdadMinPax"].ToString().Equals(tblSecundaria.Rows[x]["EdadMinPax"].ToString()) &&
                            tblPrimaria.Rows[i]["EdadMaxPax"].ToString().Equals(tblSecundaria.Rows[x]["EdadMaxPax"].ToString()))
                        {
                            decimal dblTarifaNocheSec = Convert.ToDecimal(tblSecundaria.Rows[x]["TarifaNocheAdic"].ToString().Replace(",",
                               clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) * iDiasSobrantesSec;
                            decimal dblPrecioNocheSec = Convert.ToDecimal(tblSecundaria.Rows[x]["NocheAdic"].ToString().Replace(",",
                               clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) * iDiasSobrantesSec;
                            //se suma el resultado de los dias adicionales a la tarifa de la tabla principal
                            dblTotalTarifa = dblTotalTarifa + dblTarifaNocheSec;
                            dblTotalPrecio = dblTotalPrecio + dblPrecioNocheSec;
                            //se calculan los valores totales de los impuestos de las 2 vigencias
                            tblImpPrincipal = setGenerarTablaImpRegistro(((DataTable)tblPrimaria.Rows[i]["tImpuestos"]), ((DataTable)tblSecundaria.Rows[x]["tImpuestos"]), 1, 0);
                        }
                    }
                    //se agrega todo a una sola tabla para tener un consolidado
                    tblPrimaria.Rows[i]["tImpuestos"] = tblImpPrincipal;
                    tblPrimaria.Rows[i]["Tarifa"] = dblTotalTarifa;
                    tblPrimaria.Rows[i]["Precio"] = dblTotalPrecio;
                }
                return tblPrimaria;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setCalcularValoresSec";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return null;
            }
        }

        /// <summary>
        /// Metodo que valida el valor de los impuestos de la tabla secundaria cuando hay combinacion de vigencias, 
        /// ademas valida el tipo de calculo (neto o porcentual) y genera la operacion matematica correspondiente
        /// </summary>
        /// <param name="tblPrincip"></param>
        /// <param name="iCantNoches"></param>
        /// <param name="bNoche"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setValidarValorImpNochesAdicComb(DataTable tblPrincip, int iCantNoches, bool bNoche)
        {
            int iNochesAux = 0;
            if (iCantNoches > 0)
            {
                iNochesAux = 1;
            }
            else
            {
                iNochesAux = 1;
            }
            DataTable tblPrincipal = tblPrincip.Copy();
            try
            {

                for (int i = 0; i < tblPrincipal.Rows.Count; i++)
                {
                    //si es impuesto para la noche adicional se calcula
                    if (tblPrincipal.Rows[i]["strRefereTipoImp"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoImpuestoPlanTarifa", "TPO_IMP_TARIFA")))
                    {
                        //operacion para valores en porcentaje
                        if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("0"))
                        {
                            if (bNoche)
                            {
                                //tblPrincipal.Rows[i]["dblvalor"] = ((Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                //    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) / 100) * Convert.ToDecimal(tblPrincipal.Rows[i]["dblTarifaNocheAdic"].ToString().Replace(",",
                                //    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")))) * iCantNoches;
                                tblPrincipal.Rows[i]["dblvalor"] = (Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) * iNochesAux);
                            }
                            else
                            {
                                tblPrincipal.Rows[i]["dblvalor"] = ((Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) / 100) * Convert.ToDecimal(tblPrincipal.Rows[i]["Tarifa"].ToString().Replace(",",
                                    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")))) * iNochesAux;
                                tblPrincipal.Rows[i]["FormaCalculo"] = "1";
                            }
                        }
                        else
                        {
                            //operacion para valores netos
                            if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("1"))
                            {
                                tblPrincipal.Rows[i]["dblvalor"] = (Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) * iNochesAux);
                                //tblPrincipal.Rows[i]["FormaCalculo"] = "2";
                            }
                        }
                    }
                }

                if (iCantNoches > 0)
                {
                    for (int i = 0; i < tblPrincipal.Rows.Count; i++)
                    {
                        //si es impuesto para la noche adicional se calcula
                        if (tblPrincipal.Rows[i]["strRefereTipoImp"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoImpuestoPlanNoche", "TPO_IMP_NOCHE")))
                        {
                            //operacion para valores en porcentaje
                            if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("0"))
                            {
                                if (bNoche)
                                {
                                    //tblPrincipal.Rows[i]["dblvalor"] = ((Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                    //    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) / 100) * Convert.ToDecimal(tblPrincipal.Rows[i]["dblTarifaNocheAdic"].ToString().Replace(",",
                                    //    clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")))) * iCantNoches;
                                    tblPrincipal.Rows[i]["dblvalor"] = (Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                        clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) * iCantNoches);
                                }
                                else
                                {
                                    tblPrincipal.Rows[i]["dblvalor"] = ((Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                        clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) / 100) * Convert.ToDecimal(tblPrincipal.Rows[i]["dblTarifaNocheAdic"].ToString().Replace(",",
                                        clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")))) * iCantNoches;
                                    tblPrincipal.Rows[i]["FormaCalculo"] = "1";
                                }
                            }
                            else
                            {
                                //operacion para valores netos
                                if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("1"))
                                {
                                    tblPrincipal.Rows[i]["dblvalor"] = (Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                        clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) * iCantNoches);
                                    //tblPrincipal.Rows[i]["FormaCalculo"] = "2";
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarValorImp";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
            return tblPrincipal;
        }

        /// <summary>
        /// Metodo que calcula el valor total de todas las noches adicionales, multiplicando el valor de una sola por el numero total
        /// </summary>
        /// <param name="dtDatos"></param>
        /// <param name="iNoches"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setCalcularValoresNochesAdic(DataTable dtDatos, int iNoches)
        {
            try
            {
                DataTable tblPrimaria = dtDatos.Copy();
                for (int i = 0; i < tblPrimaria.Rows.Count; i++)
                {
                    //obtenemos el valor  de la tarifa y la noche adicional
                    decimal dblTarifa = Convert.ToDecimal(tblPrimaria.Rows[i]["Tarifa"].ToString().Replace(",",
                            clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")));
                    decimal dblTarifaNoche = Convert.ToDecimal(tblPrimaria.Rows[i]["TarifaNocheAdic"].ToString().Replace(",",
                            clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")));
                    decimal dblPrecio = Convert.ToDecimal(tblPrimaria.Rows[i]["Precio"].ToString().Replace(",",
                            clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")));
                    decimal dblPrecioNoche = Convert.ToDecimal(tblPrimaria.Rows[i]["NocheAdic"].ToString().Replace(",",
                            clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")));
                    decimal dblTotalTarifa = 0;
                    decimal dblTotalPrecio = 0;

                    //se multiplica el valor de la noche adicional por las noches requeridas y se suman a la tarifa
                    dblTotalTarifa = dblTarifa + (dblTarifaNoche * iNoches);
                    dblTotalPrecio = dblPrecio + (dblPrecioNoche * iNoches);

                    //se modifica el precio de la tarifa en la tabla de acuerdo al resultado
                    tblPrimaria.Rows[i]["Tarifa"] = dblTotalTarifa;
                    tblPrimaria.Rows[i]["Precio"] = dblTotalPrecio;
                }
                return tblPrimaria;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setCalcularValoresNochesAdic";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return null;
            }
        }

        /// <summary>
        /// Metodo que calculael valor de los impuestos de la tabla secundaria de vigencias cruzadas
        /// </summary>
        /// <param name="tblPrincip"></param>
        /// <param name="iCantNoches"></param>
        /// <param name="bNoche"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setValidarValorImpNochesAdicCombSec(DataTable tblPrincip, int iCantNoches, bool bNoche)
        {
            DataTable tblPrincipal = tblPrincip.Copy();
            try
            {
                DataView dvFiltro = new DataView(tblPrincipal);
                dvFiltro.RowFilter = "strRefereTipoImp = '" + clsValidaciones.GetKeyOrAdd("TipoImpuestoPlanNoche", "TPO_IMP_NOCHE") + "'";
                tblPrincipal = dvFiltro.ToTable();
                for (int i = 0; i < tblPrincipal.Rows.Count; i++)
                {
                    //si es impuesto para la noche adicional se calcula

                    //operacion para valores en porcentaje
                    if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("0"))
                    {
                        if (bNoche)
                        {
                            tblPrincipal.Rows[i]["dblvalor"] = (Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) * iCantNoches);
                        }
                        else
                        {
                            tblPrincipal.Rows[i]["dblvalor"] = ((Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) / 100) * Convert.ToDecimal(tblPrincipal.Rows[i]["dblTarifaNocheAdic"].ToString().Replace(",",
                                clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")))) * iCantNoches;
                            tblPrincipal.Rows[i]["FormaCalculo"] = "1";
                        }
                    }
                    else
                    {
                        //operacion para valores netos
                        if (tblPrincipal.Rows[i]["FormaCalculo"].ToString().Equals("1"))
                        {
                            tblPrincipal.Rows[i]["dblvalor"] = (Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString().Replace(",",
                                clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",")).Replace(".", clsValidaciones.GetKeyOrAdd("CaracterDecimal", ","))) * iCantNoches);
                            //tblPrincipal.Rows[i]["intProxNivel"] = "2";
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarValorImp";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
            return tblPrincipal;
        }

        /// <summary>
        /// metodo que genera la tabla definitiva de impuestos, verificando que se hayan calculado segun su tipo (Neto o porcentual)
        /// y calculando los que se pudieron haber quedado sin calcular
        /// </summary>
        /// <param name="tblPrincip"></param>
        /// <param name="tblSec"></param>
        /// <param name="iCantPax"></param>
        /// <param name="dValorConversion"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setGenerarTablaImpRegistro(DataTable tblPrincip, DataTable tblSec, int iCantPax, decimal dValorConversion)
        {
            try
            {
                DataTable tblPrincipal = tblPrincip.Copy();
                DataTable tblSecundaria = tblSec.Copy();
                for (int i = 0; i < tblPrincipal.Rows.Count; i++)
                {
                    for (int x = 0; x < tblSecundaria.Rows.Count; x++)
                    {
                        //si el impuesto coincide se suma
                        if (tblPrincipal.Rows[i]["Impuesto"].ToString().Equals(tblSecundaria.Rows[x]["Impuesto"].ToString()))
                        {
                            //operacion para valores de porcentaje
                            if (tblSecundaria.Rows[x]["FormaCalculo"].ToString().Equals("0"))
                            {
                                tblPrincipal.Rows[i]["dblvalor"] = Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString()) + (((Convert.ToDecimal(tblSecundaria.Rows[x]["dblvalor"].ToString()) / 100) * Convert.ToDecimal(tblSecundaria.Rows[x]["Tarifa"].ToString())) * iCantPax);
                                //tblPrincipal.Rows[i]["intProxNivel"] = "2";
                            }
                            //operacion para valores de netos
                            if (tblSecundaria.Rows[x]["FormaCalculo"].ToString().Equals("1"))
                            {
                                tblPrincipal.Rows[i]["dblvalor"] = Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString()) + (Convert.ToDecimal(tblSecundaria.Rows[x]["dblvalor"].ToString()) * iCantPax);
                                //tblPrincipal.Rows[i]["intProxNivel"] = "2";
                            }

                            if (tblSecundaria.Rows[x]["FormaCalculo"].ToString().Equals("2"))
                            {
                                tblPrincipal.Rows[i]["dblvalor"] = Convert.ToDecimal(tblPrincipal.Rows[i]["dblvalor"].ToString()) + Convert.ToDecimal(tblSecundaria.Rows[x]["dblvalor"].ToString());
                                //tblPrincipal.Rows[i]["intProxNivel"] = "2";
                            }
                            tblSecundaria.Rows.RemoveAt(x);
                            x--;
                        }
                    }
                }
                //si sobra algun impuesto que no se haya podido agrupar se pone solo
                for (int x = 0; x < tblSecundaria.Rows.Count; x++)
                {
                    if (tblSecundaria.Rows[x]["FormaCalculo"].ToString().Equals("0"))
                    {
                        tblSecundaria.Rows[x]["dblvalor"] = (((Convert.ToDecimal(tblSecundaria.Rows[x]["dblvalor"].ToString()) / 100) * Convert.ToDecimal(tblSecundaria.Rows[x]["Tarifa"].ToString())) * iCantPax);
                    }
                    if (tblSecundaria.Rows[x]["FormaCalculo"].ToString().Equals("1"))
                    {
                        tblSecundaria.Rows[x]["dblvalor"] = (Convert.ToDecimal(tblSecundaria.Rows[x]["dblvalor"].ToString()) * iCantPax);
                    }
                    tblPrincipal.Rows.Add(tblSecundaria.Rows[x].ItemArray);
                    tblPrincipal.Rows[tblPrincipal.Rows.Count - 1]["FormaCalculo"] = "2";
                }
                if (dValorConversion > 0)
                    tblPrincipal = setValidarValorMoneda(tblPrincipal, dValorConversion, "");
                return tblPrincipal;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setGenerarTablaImpRegistro";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return tblPrincip;
            }
        }

        /// <summary>
        /// Metodo que realiza el calculo de la conversion (en caso de que se cotice en otra moneda) e inserta los nuevos valores en la tabla principal
        /// </summary>
        /// <param name="tblPrincip"></param>
        /// <param name="dValorConversion"></param>
        /// <param name="sMoneda"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setValidarValorMoneda(DataTable tblPrincip, decimal dValorConversion, string sMoneda)
        {
            DataTable tblPrincipal = tblPrincip.Copy();
            try
            {
                string sMonedaCot = "0";
                if (sMoneda != null && sMoneda != "")
                {

                    //tblRefere tRefere = new tblRefere();
                    //tRefere.Get(clsValidaciones.GetKeyOrAdd("moneda", "moneda"), sMoneda);
                    //if (tRefere.Respuesta)
                    //    sMonedaCot = tRefere.intidRefere.Value;
                }
                for (int i = 0; i < tblPrincipal.Rows.Count; i++)
                {
                    if (sMoneda != null && sMoneda != "")
                    {
                        tblPrincipal.Rows[i]["Tarifa"] = Convert.ToDecimal(tblPrincipal.Rows[i]["Tarifa"]) * dValorConversion;
                        tblPrincipal.Rows[i]["Precio"] = Convert.ToDecimal(tblPrincipal.Rows[i]["Precio"]) * dValorConversion;
                        tblPrincipal.Rows[i]["strRefereMoneda"] = sMoneda;
                        tblPrincipal.Rows[i]["intMoneda"] = sMonedaCot;
                    }
                    else
                    {
                        tblPrincipal.Rows[i]["dblValor"] = Convert.ToDecimal(tblPrincipal.Rows[i]["dblValor"]) * dValorConversion;
                    }
                }
                return tblPrincipal;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarValorMoneda";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return tblPrincipal;
            }
        }

        /// <summary>
        /// metodo que llama a la validacion general de tarifas e impuestos para calculo y sube la tabla de tarifas completa a sesion, 
        /// ademas asigna un texto a cada tarifa para verificar a que habitacion corresponde
        /// </summary>
        /// <param name="tblTarifas"></param>
        /// <param name="iNumeroCabina"></param>
        /// <param name="iNumPax"></param>
        /// <param name="dValorConversion"></param>
        /// <param name="PageSource"></param>
        public void setLlenarTablaGeneralTarifas(DataTable tblTarifas, int iNumeroCabina, int iNumPax, decimal dValorConversion, UserControl PageSource)
        {
            try
            {
                DataTable tblTarifasPax = new DataTable();
                if (HttpContext.Current.Session["tbltarifasgeneral"] == null)
                {
                    tblTarifasPax = tblTarifas.Copy();
                }
                else
                {
                    tblTarifasPax = (DataTable)HttpContext.Current.Session["tbltarifasgeneral"];
                    for (int x = 0; x < tblTarifas.Rows.Count; x++)
                    {
                        tblTarifasPax.Rows.Add(tblTarifas.Rows[x].ItemArray);
                    }
                }
                tblTarifasPax = setInsertarNoSegmento(tblTarifasPax, iNumeroCabina);
                for (int x = 0; x < tblTarifasPax.Rows.Count; x++)
                {
                    if (iNumPax > 0)
                        tblTarifasPax.Rows[x]["tImpuestos"] = setValidarValorImp((DataTable)tblTarifasPax.Rows[x]["tImpuestos"], iNumPax, true);
                    else
                        tblTarifasPax.Rows[x]["tImpuestos"] = setValidarValorImp((DataTable)tblTarifasPax.Rows[x]["tImpuestos"], 1, true);
                    if (dValorConversion > 0)
                        tblTarifasPax.Rows[x]["tImpuestos"] = setValidarValorMoneda((DataTable)tblTarifasPax.Rows[x]["tImpuestos"], dValorConversion, "");
                }
                HttpContext.Current.Session["tbltarifasgeneral"] = tblTarifasPax;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setLlenarTablaGeneralTarifas";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que asigna el numero de habitacion a cada tarifa dependiendo del numero de tarifas que se haya consultado
        /// </summary>
        /// <param name="tblTarifas"></param>
        /// <param name="iNumeroCabina"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setInsertarNoSegmento(DataTable tblTarifas, int iNumeroCabina)
        {
            try
            {
                DataTable tblTarifasPax = tblTarifas.Copy();
                if (!tblTarifasPax.Columns.Contains("iNoCabina"))
                    tblTarifasPax.Columns.Add("iNoCabina");
                for (int x = 0; x < tblTarifas.Rows.Count; x++)
                {
                    tblTarifasPax.Rows[x]["iNoCabina"] = iNumeroCabina;
                }
                return tblTarifasPax;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setInsertarNoCabina";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return tblTarifas;
            }
        }

        /// <summary>
        /// Metodo que consulta planes relacionados de un plan especifico
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="sFechaIni"></param>
        /// <param name="sFechaFin"></param>
        /// <param name="sNumeroAdt"></param>
        /// <param name="sNumeroCnn"></param>
        /// <param name="sNumeroInf"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void ValidarOfertasAsociadas(UserControl PageSource, string sFechaIni, string sFechaFin, string sNumeroAdt, string sNumeroCnn, string sNumeroInf)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();

                if (cCache != null)
                {
                    if (PageSource.Request.QueryString["id"] != null)
                    {
                        Panel pOfertasAereas = (Panel)PageSource.FindControl("pOfertasAereas");
                        Label lblDisponibilidad = (Label)PageSource.FindControl("lblDisponibilidad");
                        if (pOfertasAereas != null)
                        {
                            //HttpContext.Current.Session["sFechaIni"] = sFechaIni;
                            //HttpContext.Current.Session["sFechaFin"] = sFechaFin;
                            //HttpContext.Current.Session["sNumeroAdt"] = sNumeroAdt;
                            //HttpContext.Current.Session["sNumeroCnn"] = sNumeroCnn;
                            //HttpContext.Current.Session["sNumeroInf"] = sNumeroInf;
                            //DataSet dsDatos = new csConsultasPlanes().ConsultaOfertasAsociadas(PageSource.Request.QueryString["id"], iAplicacion, clsValidaciones.GetKeyOrAdd("TiposPlan"),
                            //    clsValidaciones.GetKeyOrAdd("ProductoRelacionPlanes"), clsSesiones.getIdioma());
                            //if (dsDatos != null && dsDatos.Tables.Count > 0 && dsDatos.Tables[0].Rows.Count > 0)
                            //{
                            //    DataView dvFiltro = new DataView(dsDatos.Tables[0]);
                            //    dvFiltro.RowFilter = "(dtmVigenciaResDesde <= '" + DateTime.Now.ToString(sFormatoFechaBD) + "' AND dtmVigenciaResHasta >= '" + DateTime.Now.ToString(sFormatoFechaBD) + "') " +
                            //        " AND (dtmVigenciaViajeIni <= '" + sFechaIni + "' AND dtmVigenciaViajeFin >= '" + sFechaFin + "')";
                            //    DataTable tblOrigenes = dvFiltro.ToTable();
                            //    if (tblOrigenes != null && tblOrigenes.Rows.Count > 0)
                            //    {
                            //        csBuscador cBusqueda = new csBuscador();
                            //        HttpContext.Current.Session["tblOrigenesPlanes"] = tblOrigenes;
                            //        UserControl ucBuscadorAereo = (UserControl)PageSource.FindControl("ucBuscadorAereo");
                            //        cBusqueda.setAereoPlan(ucBuscadorAereo);
                            //        pOfertasAereas.Visible = true;
                            //        lblDisponibilidad.Text = "True";
                            //        if (cBusqueda.setValidaPlanAir())
                            //            cBusqueda.BuscarIdaVueltaPlanParametros(ucBuscadorAereo);
                            //    }
                            //    else
                            //    {
                            //        pOfertasAereas.Visible = false;
                            //    }
                            //}
                            //else
                            //{
                            //    pOfertasAereas.Visible = false;
                            //}
                        }
                    }
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "ValidarOfertasAsociadas";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que recorre la tabla de tarifas y modifica la fecha de inicio y fin
        /// </summary>
        /// <param name="tblTarifas"></param>
        /// <param name="sFechaIni"></param>
        /// <param name="sFechaFin"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setLlenarFechasTarifas(DataTable tblTarifas, string sFechaIni, string sFechaFin)
        {
            try
            {
                for (int x = 0; x < tblTarifas.Rows.Count; x++)
                {
                    tblTarifas.Rows[x]["Desde"] = sFechaIni;
                    tblTarifas.Rows[x]["Hasta"] = sFechaFin;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setLlenarFechasTarifas";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
            return tblTarifas;
        }

        /// <summary>
        /// Genera las validaciones correspondientes a cada tipo de pax, asignando un campo dentro del registro de la tarifa a cada uno
        /// </summary>
        /// <param name="rptEdades"></param>
        /// <param name="tblTarifas"></param>
        /// <param name="iNumNinios"></param>
        /// <param name="iNumAdt"></param>
        /// <param name="iNumInf"></param>
        /// <param name="sTipoPlan"></param>
        /// <param name="iSegmento"></param>
        /// <param name="idPlan"></param>
        /// <param name="PageSource"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setInsertarTarifasTipoPax(Repeater rptEdades, DataTable tblTarifas, int iNumNinios, int iNumAdt, int iNumInf, string sTipoPlan, int iSegmento, string idPlan, UserControl PageSource)
        {
            try
            {
                int iRangoIniPaxCnnAdt = 0;
                int iRangoFinPaxCnnAdt = 0;
                decimal dValorConversion = 1;
                DataTable tblTarifasCompletasRegistro = tblTarifas.Copy();
                //*/*********************************** REVISAR
                //csAdministrador cAdmin = new csAdministrador();
                //string sMonedaOrigen = tblTarifas.Rows[0]["strRefereMoneda"].ToString();                
                //DropDownList ddlMoneda = (DropDownList)PageSource.FindControl("ddlMoneda");               
                //if (ddlMoneda != null)
                //{
                //    string sMonedaDestino = ddlMoneda.SelectedValue;
                //    dValorConversion = cAdmin.ConsultarTasaMoneda(clsSesiones.getAplicacion().ToString(),
                //        sMonedaOrigen, clsSesiones.getIdioma(), sMonedaDestino);
                //    tblTarifas = setValidarValorMoneda(tblTarifas, dValorConversion, sMonedaDestino);
                //}

                //columnas necesarias para la agrupacion por tipo de habitacion
                tblTarifas.Columns.Add("dblPrecioAdt3");
                tblTarifas.Columns.Add("dblPrecioAdt4");
                tblTarifas.Columns.Add("dblPrecioNino");
                tblTarifas.Columns.Add("dblPrecioNino2");
                tblTarifas.Columns.Add("dblPrecioInfante");
                tblTarifas.Columns.Add("dblPrecioAdt3Imp");
                tblTarifas.Columns.Add("dblPrecioAdt4Imp");
                tblTarifas.Columns.Add("dblPrecioNinoImp");
                tblTarifas.Columns.Add("dblPrecioNino2Imp");
                tblTarifas.Columns.Add("dblPrecioInfanteImp");
                tblTarifas.Columns.Add("dblTotalImpuestos");
                tblTarifas.Columns.Add("dblTotalSinImpuestos");
                tblTarifas.Columns.Add("dblTotalImpuestosFormato");
                tblTarifas.Columns.Add("dblTotalSinImpuestosFormato");
                tblTarifas.Columns.Add("dblSumaImpuestos");
                tblTarifas.Columns.Add("dblImpuestosAdt3");
                tblTarifas.Columns.Add("dblImpuestosAdt4");
                tblTarifas.Columns.Add("dblImpuestosNino");
                tblTarifas.Columns.Add("dblImpuestosNino2");
                tblTarifas.Columns.Add("dblImpuestosInfante");
                tblTarifas.Columns.Add("dblTarifaFormato");
                tblTarifas.Columns.Add("dblPrecioAdt3Formato");
                tblTarifas.Columns.Add("dblPrecioAdt4Formato");
                tblTarifas.Columns.Add("dblPrecioNinoFormato");
                tblTarifas.Columns.Add("dblPrecioNino2Formato");
                tblTarifas.Columns.Add("dblPrecioInfanteFormato");
                tblTarifas.Columns.Add("dblSumaImpuestosFormato");

                //se obtiene una tabla base con los valores de los adultos y los tipos de habitacion
                DataView dvFiltroADT = new DataView();
                dvFiltroADT.Table = tblTarifas;
                dvFiltroADT.RowFilter = "strRefereTipoPasajero = '" + clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT") + "'";
                DataTable tblFiltrada = dvFiltroADT.ToTable();
                if (tblFiltrada.Rows.Count > 0)
                {
                    iRangoIniPaxCnnAdt = Convert.ToInt32(tblFiltrada.Rows[0]["EdadMinPax"].ToString());
                    iRangoFinPaxCnnAdt = Convert.ToInt32(tblFiltrada.Rows[0]["EdadMaxPax"].ToString());
                }

                decimal dValorConversionMonedaUnica = 1;
                //if (clsValidaciones.GetKeyOrAdd("dConversionMonedaUnica", "False").ToUpper().Equals("TRUE"))
                //{
                //    dValorConversionMonedaUnica = cAdmin.ConsultarTasaMoneda(clsSesiones.getAplicacion().ToString(),
                //      sMonedaOrigen, clsSesiones.getIdioma(), clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP"));
                //}
                HttpContext.Current.Session["dValorConversionMonedaUnica"] = dValorConversionMonedaUnica;

                csConsultasPlanes cPlan = new csConsultasPlanes();
                csGenerales cGen = new csGenerales();
                int iNumNinoProv = 0;
                int iNumNino2 = 0;
                int iNumAdt3 = 0;
                int iNumAdt4 = 0;
                string sAplicacion = "0";


                string sPlantillaNormal = clsValidaciones.GetKeyOrAdd("PlantillaNormal", "PLNORM");
                string sPlantillaNinos = clsValidaciones.GetKeyOrAdd("PlantillaNinos", "PL_CRUCE_2");
                string TipoPlantilla = PageSource.Request.QueryString["Plantilla"];
                if (TipoPlantilla != null)
                {
                    if (TipoPlantilla.Equals(sPlantillaNormal))
                    {
                        iNumNinoProv = iNumNinios;
                        iNumNino2 = 0;
                    }
                    else
                    {
                        if (TipoPlantilla.Equals(sPlantillaNinos))
                        {
                            if (iNumNinios > 1)
                            {
                                iNumNinoProv = 1;
                                iNumNino2 = 1;
                            }
                            else
                            {
                                iNumNinoProv = iNumNinios;
                                iNumNino2 = 0;
                            }
                        }
                        else
                        {
                            iNumNinoProv = iNumNinios;
                            iNumNino2 = 0;
                        }
                    }
                }
                else
                {
                    iNumNinoProv = iNumNinios;
                    iNumNino2 = 0;
                }

                if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS")))
                {
                    for (int a = 0; a < tblFiltrada.Rows.Count; a++)
                    {
                        DataTable tblImpRegistro = new DataTable();
                        if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]).Rows.Count > 0)
                        {
                            tblImpRegistro = (DataTable)tblFiltrada.Rows[a]["tImpuestos"];
                            tblImpRegistro = setValidarValorImp(tblImpRegistro, 1, true);
                            tblImpRegistro = setValidarImpUnicoTipoPax(tblImpRegistro, 1, dValorConversion);
                        }
                        tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                        decimal dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",",
                            sCaracterDecimal).Replace(".", sCaracterDecimal)) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"].ToString().Replace(",",
                            sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                        decimal dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",",
                            sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                        tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                        decimal dValPrecioIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Precio"].ToString().Replace(",",
                            sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                        tblFiltrada.Rows[a]["Precio"] = dValPrecioIncMarkup;
                    }
                }
                else
                {
                    if (iNumAdt < 3 && iNumInf == 0 && iNumNinios == 0)
                    {
                        for (int a = 0; a < tblFiltrada.Rows.Count; a++)
                        {
                            DataTable tblImpRegistro = new DataTable();
                            if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]).Rows.Count > 0)
                            {
                                tblImpRegistro = (DataTable)tblFiltrada.Rows[a]["tImpuestos"];
                                tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumAdt, true);
                                tblImpRegistro = setValidarImpUnicoTipoPax(tblImpRegistro, iNumAdt, dValorConversion);
                            }
                            tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                            decimal dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                            decimal dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                            tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                            decimal dValPrecioIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Precio"].ToString().Replace(",",
                            sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                            tblFiltrada.Rows[a]["Precio"] = dValPrecioIncMarkup;
                        }
                    }
                    else
                    {

                        if (iNumAdt >= 3 && iNumInf == 0 && iNumNinios == 0 && (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")) ||
                            sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanSouvenir", "SOUV")) || sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanApartamento", "APTO"))))
                        {
                            for (int a = 0; a < tblFiltrada.Rows.Count; a++)
                            {
                                DataTable tblImpRegistro = new DataTable();
                                if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]).Rows.Count > 0)
                                {
                                    tblImpRegistro = (DataTable)tblFiltrada.Rows[a]["tImpuestos"];
                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumAdt, true);
                                    tblImpRegistro = setValidarImpUnicoTipoPax(tblImpRegistro, iNumAdt, dValorConversion);
                                }
                                tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                                decimal dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                                decimal dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                                decimal dValPrecioIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Precio"].ToString().Replace(",",
                            sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                tblFiltrada.Rows[a]["Precio"] = dValPrecioIncMarkup;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < tblTarifas.Rows.Count; i++)
                            {
                                decimal dValMarkup = Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)) * (Convert.ToDecimal(tblTarifas.Rows[i]["MakUp"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                                decimal dValIncMarkup = Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                tblTarifas.Rows[i]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                                #region [Pax 3]
                                //para todos los casos en elrecorrido de la tabla de tarifas se valida el tipo de pax luego se recorre la tabla base de valores 
                                //de adultos para encontrar el tipo de habitacion correspondiente y fijar los valores en los registros tanto de tarifas como de impuestos
                                //-------------- Pasajero 3
                                if (tblTarifas.Rows[i]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdt3", "ADT3")))
                                {
                                    for (int a = 0; a < tblFiltrada.Rows.Count; a++)
                                    {
                                        DataTable tblImpRegistro = null;
                                        int iNumPaxAdic = 0;
                                        //tabla base para impuestos
                                        if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]) != null)
                                        {
                                            if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]).Rows.Count > 0 && tblImpRegistro == null)
                                            {
                                                tblImpRegistro = (DataTable)tblFiltrada.Rows[a]["tImpuestos"];
                                                //solo se fijan valores si el numero de pasajeros adultos es mayor que 2
                                                if (iNumAdt > 2)
                                                {
                                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, 2, true);
                                                    //para circuitos se envian los pasajeros adicionales despues de 2 adultos, para cruceros solo se envia 1
                                                    //REVISAR ESTA VALIDACION, SE QUITO PARA METROPOLITAN ERA SI ERA IGUAL A CIRCUITOS
                                                    //if (sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC"))
                                                    //{
                                                    //    iNumPaxAdic = iNumAdt - 2;
                                                    //    iNumAdt3 = iNumAdt - 2;
                                                    //}
                                                    //else
                                                    //{
                                                    iNumPaxAdic = 1;
                                                    iNumAdt3 = 1;
                                                    //}
                                                }
                                                else
                                                {
                                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumAdt, true);
                                                }
                                            }
                                        }
                                        if (tblTarifas.Rows[i]["TipoHabitacion"].ToString().Equals(tblFiltrada.Rows[a]["TipoHabitacion"].ToString()) &&
                                            tblTarifas.Rows[i]["TipoAlim"].ToString().Equals(tblFiltrada.Rows[a]["TipoAlim"].ToString()))
                                        {
                                            if (iNumAdt > 2)
                                            {
                                                tblFiltrada.Rows[a]["dblPrecioAdt3"] = Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",",
                                                    sCaracterDecimal).Replace(".", sCaracterDecimal));
                                                tblFiltrada.Rows[a]["dblPrecioAdt3Imp"] = Convert.ToDecimal(tblTarifas.Rows[i]["Precio"].ToString().Replace(",",
                                                    sCaracterDecimal).Replace(".", sCaracterDecimal));
                                                if (tblTarifas.Rows[i]["tImpuestos"] != DBNull.Value)
                                                {

                                                    if (tblImpRegistro != null)
                                                    {
                                                        //se suman los impuestos del tipo de pax a la tabla base
                                                        tblImpRegistro = setGenerarTablaImpRegistro(tblImpRegistro, (DataTable)tblTarifas.Rows[i]["tImpuestos"], 1, dValorConversion);
                                                    }
                                                    else
                                                    {
                                                        //los impuestos del tipo de pax son la tabla base
                                                        tblImpRegistro = (DataTable)tblTarifas.Rows[i]["tImpuestos"];
                                                        tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumPaxAdic, true);
                                                    }
                                                    tblFiltrada.Rows[a]["dblImpuestosAdt3"] = setDevuelveValorImpuestos((DataTable)tblTarifas.Rows[i]["tImpuestos"]).ToString();
                                                }
                                                if (tblImpRegistro != null)
                                                    tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                                                dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",",
                                                    sCaracterDecimal).Replace(".", sCaracterDecimal)) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"]) / 100);
                                                dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",",
                                                    sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                                                decimal dValPrecioIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Precio"].ToString().Replace(",",
                                                    sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                tblFiltrada.Rows[a]["Precio"] = dValPrecioIncMarkup;
                                            }
                                            else
                                            {
                                                tblFiltrada.Rows[a]["dblPrecioAdt3"] = "0";
                                                tblFiltrada.Rows[a]["dblPrecioAdt3Imp"] = "0";
                                                tblFiltrada.Rows[a]["dblImpuestosAdt3"] = "0";
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region [Pax 4]
                                //REVISAR ESTA VALIDACION, SE QUITO PARA METROPOLITAN
                                //if (sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")) 
                                //{
                                //------------- Pasajero 4
                                if (tblTarifas.Rows[i]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdt4", "ADT4")))
                                {
                                    for (int a = 0; a < tblFiltrada.Rows.Count; a++)
                                    {
                                        DataTable tblImpRegistro = null;
                                        //tabla base para impuestos
                                        if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]) != null)
                                        {
                                            if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]).Rows.Count > 0 && tblImpRegistro == null)
                                            {
                                                tblImpRegistro = (DataTable)tblFiltrada.Rows[a]["tImpuestos"];
                                                if (iNumAdt > 2)
                                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, 2, true);
                                                else
                                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumAdt, true);
                                            }
                                        }
                                        if (tblTarifas.Rows[i]["TipoHabitacion"].ToString().Equals(tblFiltrada.Rows[a]["TipoHabitacion"].ToString()) &&
                                          tblTarifas.Rows[i]["TipoAlim"].ToString().Equals(tblFiltrada.Rows[a]["TipoAlim"].ToString()))
                                        {
                                            //solo se fijan valores si el numero de pasajeros adultos es mayor que 3 en cruceros, no aplica para circuitos
                                            if (iNumAdt > 3)
                                            {
                                                iNumAdt4 = 1;
                                                tblFiltrada.Rows[a]["dblPrecioAdt4"] = Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",",
                                                    sCaracterDecimal).Replace(".", sCaracterDecimal));
                                                tblFiltrada.Rows[a]["dblPrecioAdt4Imp"] = Convert.ToDecimal(tblTarifas.Rows[i]["Precio"].ToString().Replace(",",
                                                    sCaracterDecimal).Replace(".", sCaracterDecimal));
                                                if (tblTarifas.Rows[i]["tImpuestos"] != DBNull.Value)
                                                {
                                                    if (tblImpRegistro != null)
                                                    {
                                                        //se suman los impuestos del tipo de pax a la tabla base
                                                        tblImpRegistro = setGenerarTablaImpRegistro(tblImpRegistro, (DataTable)tblTarifas.Rows[i]["tImpuestos"], 1, dValorConversion);
                                                    }
                                                    else
                                                    {
                                                        //los impuestos del tipo de pax son la tabla base
                                                        tblImpRegistro = (DataTable)tblTarifas.Rows[i]["tImpuestos"];
                                                        tblImpRegistro = setValidarValorImp(tblImpRegistro, 1, true);
                                                    }
                                                    tblFiltrada.Rows[a]["dblImpuestosAdt4"] = setDevuelveValorImpuestos((DataTable)tblTarifas.Rows[i]["tImpuestos"]).ToString();
                                                }
                                                if (tblImpRegistro != null)
                                                    tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                                                dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                    sCaracterDecimal)) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                                                dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                                                decimal dValPrecioIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Precio"].ToString().Replace(",",
                                                   sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                tblFiltrada.Rows[a]["Precio"] = dValPrecioIncMarkup;
                                            }
                                            else
                                            {
                                                tblFiltrada.Rows[a]["dblPrecioAdt4"] = "0";
                                                tblFiltrada.Rows[a]["dblPrecioAdt4Imp"] = "0";
                                                tblFiltrada.Rows[a]["dblImpuestosAdt4"] = "0";
                                            }
                                        }
                                    }
                                }
                                //}
                                #endregion
                                #region [Pax Niño]
                                //------------- Niño
                                if (tblTarifas.Rows[i]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN")))
                                {
                                    for (int a = 0; a < tblFiltrada.Rows.Count; a++)
                                    {
                                        DataTable tblImpRegistro = null;
                                        //tabla base para impuestos
                                        if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]) != null)
                                        {
                                            if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]).Rows.Count > 0 && tblImpRegistro == null)
                                            {
                                                tblImpRegistro = (DataTable)tblFiltrada.Rows[a]["tImpuestos"];
                                                if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")))
                                                {
                                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumAdt, true);
                                                }
                                                else
                                                {
                                                    if (iNumAdt > 2)
                                                        tblImpRegistro = setValidarValorImp(tblImpRegistro, 2, true);
                                                    else
                                                        tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumAdt, true);
                                                }
                                            }
                                        }
                                        if (sTipoPlan == clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC") || sTipoPlan == clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE"))
                                        {
                                            for (int b = 0; b < rptEdades.Items.Count; b++)
                                            {
                                                DropDownList ddlEdadNino = (DropDownList)rptEdades.Items[b].FindControl("ddlEdadNino");
                                                int iEdadCnn = Convert.ToInt32(ddlEdadNino.SelectedValue);

                                                if (iEdadCnn < iRangoIniPaxCnnAdt || iEdadCnn > iRangoFinPaxCnnAdt)
                                                {
                                                    if (tblTarifas.Rows[i]["TipoHabitacion"].ToString().Equals(tblFiltrada.Rows[a]["TipoHabitacion"].ToString()) &&
                                                        tblTarifas.Rows[i]["TipoAlim"].ToString().Equals(tblFiltrada.Rows[a]["TipoAlim"].ToString()) &&
                                                        (Convert.ToInt32(tblTarifas.Rows[i]["EdadMinPax"].ToString()) <= iEdadCnn &&
                                                        Convert.ToInt32(tblTarifas.Rows[i]["EdadMaxPax"].ToString()) >= iEdadCnn))
                                                    {
                                                        //solo se fijan valores si el numero de pasajeros niños es mayor que 0
                                                        if (iNumNinios > 0)
                                                        {
                                                            if (tblFiltrada.Rows[a]["dblPrecioNino"].ToString() != "")
                                                                tblFiltrada.Rows[a]["dblPrecioNino"] = (Convert.ToDecimal(tblFiltrada.Rows[a]["dblPrecioNino"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                                    sCaracterDecimal)) + Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                                    sCaracterDecimal)));
                                                            else
                                                                tblFiltrada.Rows[a]["dblPrecioNino"] = Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                                    sCaracterDecimal));

                                                            if (tblFiltrada.Rows[a]["dblPrecioNinoImp"].ToString() != "")
                                                                tblFiltrada.Rows[a]["dblPrecioNinoImp"] = (Convert.ToDecimal(tblFiltrada.Rows[a]["dblPrecioNinoImp"].ToString().Replace(",",
                                                                    sCaracterDecimal).Replace(".", sCaracterDecimal)) + Convert.ToDecimal(tblTarifas.Rows[i]["Precio"]));
                                                            else
                                                                tblFiltrada.Rows[a]["dblPrecioNinoImp"] = Convert.ToDecimal(tblTarifas.Rows[i]["Precio"].ToString().Replace(",",
                                                                    sCaracterDecimal).Replace(".", sCaracterDecimal));

                                                            if (tblTarifas.Rows[i]["tImpuestos"] != DBNull.Value)
                                                            {
                                                                if (tblImpRegistro != null)
                                                                {
                                                                    //se suman los impuestos del tipo de pax a la tabla base
                                                                    DataTable tblImpSecSum = setValidarValorImp((DataTable)tblTarifas.Rows[i]["tImpuestos"], 1, true);
                                                                    tblImpRegistro = setGenerarTablaImpRegistro(tblImpRegistro, tblImpSecSum, 1, dValorConversion);
                                                                }
                                                                else
                                                                {
                                                                    //los impuestos del tipo de pax son la tabla base
                                                                    tblImpRegistro = (DataTable)tblTarifas.Rows[i]["tImpuestos"];
                                                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumNinoProv, true);
                                                                }
                                                                tblFiltrada.Rows[a]["dblImpuestosNino"] = setDevuelveValorImpuestos((DataTable)tblTarifas.Rows[i]["tImpuestos"]).ToString();
                                                            }
                                                            //tblFiltrada.Rows[a]["tImpuestos"] = setGenerarTablaImpRegistro((DataTable)tblFiltrada.Rows[a]["tImpuestos"], tblImpRegistro, 1, dValorConversion);

                                                        }
                                                        else
                                                        {
                                                            tblFiltrada.Rows[a]["dblPrecioNino"] = "0";
                                                            tblFiltrada.Rows[a]["dblPrecioNinoImp"] = "0";
                                                            tblFiltrada.Rows[a]["dblImpuestosNino"] = "0";
                                                        }
                                                    }
                                                }
                                            }
                                            if (tblImpRegistro != null)
                                                tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                                            dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"]) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"].ToString().Replace(",",
                                                sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                                            dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                            tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                                            decimal dValPrecioIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Precio"].ToString().Replace(",",
                                                   sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                            tblFiltrada.Rows[a]["Precio"] = dValPrecioIncMarkup;
                                        }
                                        else
                                        {
                                            if (sTipoPlan == clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ"))
                                            {
                                                if (tblTarifas.Rows[i]["NumeroNoches"].ToString().Equals(tblFiltrada.Rows[a]["NumeroNoches"].ToString()) &&
                                                tblTarifas.Rows[i]["intidVigencia"].ToString().Equals(tblFiltrada.Rows[a]["intidVigencia"].ToString()))
                                                {
                                                    //solo se fijan valores si el numero de pasajeros niños es mayor que 0
                                                    if (iNumNinios > 0)
                                                    {
                                                        tblFiltrada.Rows[a]["dblPrecioNino"] = Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",",
                                                            sCaracterDecimal).Replace(".", sCaracterDecimal));
                                                        tblFiltrada.Rows[a]["dblPrecioNinoImp"] = Convert.ToDecimal(tblTarifas.Rows[i]["Precio"].ToString().Replace(",",
                                                            sCaracterDecimal).Replace(".", sCaracterDecimal));
                                                        if (tblTarifas.Rows[i]["tImpuestos"] != DBNull.Value)
                                                        {
                                                            if (tblImpRegistro != null)
                                                            {
                                                                //se suman los impuestos del tipo de pax a la tabla base
                                                                tblImpRegistro = setGenerarTablaImpRegistro(tblImpRegistro, (DataTable)tblTarifas.Rows[i]["tImpuestos"], iNumNinoProv, dValorConversion);
                                                            }
                                                            else
                                                            {
                                                                //los impuestos del tipo de pax son la tabla base
                                                                tblImpRegistro = (DataTable)tblTarifas.Rows[i]["tImpuestos"];
                                                                tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumNinoProv, true);
                                                            }
                                                            tblFiltrada.Rows[a]["dblImpuestosNino"] = setDevuelveValorImpuestos((DataTable)tblTarifas.Rows[i]["tImpuestos"]).ToString();
                                                        }
                                                        tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                                                        dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                            sCaracterDecimal)) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                                                        dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                        tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                                                    }
                                                    else
                                                    {
                                                        tblFiltrada.Rows[a]["dblPrecioNino"] = "0";
                                                        tblFiltrada.Rows[a]["dblPrecioNinoImp"] = "0";
                                                        tblFiltrada.Rows[a]["dblImpuestosNino"] = "0";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (sTipoPlan == clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL"))
                                                {
                                                    if (tblTarifas.Rows[i]["intTipoHabita"].ToString().Equals(tblFiltrada.Rows[a]["intTipoHabita"].ToString()) &&
                                                           tblTarifas.Rows[i]["NumeroNoches"].ToString().Equals(tblFiltrada.Rows[a]["NumeroNoches"].ToString()) &&
                                                           tblTarifas.Rows[i]["intTipoAcomoda"].ToString().Equals(tblFiltrada.Rows[a]["intTipoAcomoda"].ToString()) &&
                                                           tblTarifas.Rows[i]["intTipoAlim"].ToString().Equals(tblFiltrada.Rows[a]["intTipoAlim"].ToString()) &&
                                                           tblTarifas.Rows[i]["strTemporada"].ToString().Equals(tblFiltrada.Rows[a]["strTemporada"].ToString()))
                                                    {
                                                        //solo se fijan valores si el numero de pasajeros niños es mayor que 0
                                                        if (iNumNinios > 0)
                                                        {
                                                            tblFiltrada.Rows[a]["dblPrecioNino"] = Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",",
                                                                sCaracterDecimal).Replace(".", sCaracterDecimal)) * dValorConversionMonedaUnica;
                                                            tblFiltrada.Rows[a]["dblPrecioNinoImp"] = Convert.ToDecimal(tblTarifas.Rows[i]["Precio"].ToString().Replace(",",
                                                                sCaracterDecimal).Replace(".", sCaracterDecimal)) * dValorConversionMonedaUnica;
                                                            if (tblTarifas.Rows[i]["tImpuestos"] != DBNull.Value)
                                                            {
                                                                if (tblImpRegistro != null)
                                                                {
                                                                    //se suman los impuestos del tipo de pax a la tabla base
                                                                    tblImpRegistro = setGenerarTablaImpRegistro(tblImpRegistro, (DataTable)tblTarifas.Rows[i]["tImpuestos"], iNumNinoProv, dValorConversion);
                                                                }
                                                                else
                                                                {
                                                                    //los impuestos del tipo de pax son la tabla base
                                                                    tblImpRegistro = (DataTable)tblTarifas.Rows[i]["tImpuestos"];
                                                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumNinoProv, true);
                                                                }
                                                                tblFiltrada.Rows[a]["dblImpuestosNino"] = setDevuelveValorImpuestos((DataTable)tblTarifas.Rows[i]["tImpuestos"]).ToString();
                                                            }
                                                            tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                                                            dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                                sCaracterDecimal)) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                                                            dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                            tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                                                            decimal dValPrecioIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Precio"].ToString().Replace(",",
                                                                sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                            tblFiltrada.Rows[a]["Precio"] = dValPrecioIncMarkup;
                                                        }
                                                        else
                                                        {
                                                            tblFiltrada.Rows[a]["dblPrecioNino"] = "0";
                                                            tblFiltrada.Rows[a]["dblPrecioNinoImp"] = "0";
                                                            tblFiltrada.Rows[a]["dblImpuestosNino"] = "0";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (tblTarifas.Rows[i]["intTipoHabita"].ToString().Equals(tblFiltrada.Rows[a]["intTipoHabita"].ToString()) &&
                                                           tblTarifas.Rows[i]["intTipoAlim"].ToString().Equals(tblFiltrada.Rows[a]["intTipoAlim"].ToString()))
                                                    {
                                                        //solo se fijan valores si el numero de pasajeros niños es mayor que 0
                                                        if (iNumNinios > 0)
                                                        {
                                                            tblFiltrada.Rows[a]["dblPrecioNino"] = Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",",
                                                                sCaracterDecimal).Replace(".", sCaracterDecimal)) * dValorConversionMonedaUnica;
                                                            tblFiltrada.Rows[a]["dblPrecioNinoImp"] = Convert.ToDecimal(tblTarifas.Rows[i]["Precio"].ToString().Replace(",",
                                                                sCaracterDecimal).Replace(".", sCaracterDecimal)) * dValorConversionMonedaUnica;
                                                            if (tblTarifas.Rows[i]["tImpuestos"] != DBNull.Value)
                                                            {
                                                                if (tblImpRegistro != null)
                                                                {
                                                                    //se suman los impuestos del tipo de pax a la tabla base
                                                                    tblImpRegistro = setGenerarTablaImpRegistro(tblImpRegistro, (DataTable)tblTarifas.Rows[i]["tImpuestos"], iNumNinoProv, dValorConversion);
                                                                }
                                                                else
                                                                {
                                                                    //los impuestos del tipo de pax son la tabla base
                                                                    tblImpRegistro = (DataTable)tblTarifas.Rows[i]["tImpuestos"];
                                                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumNinoProv, true);
                                                                }
                                                                tblFiltrada.Rows[a]["dblImpuestosNino"] = setDevuelveValorImpuestos((DataTable)tblTarifas.Rows[i]["tImpuestos"]).ToString();
                                                            }
                                                            tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                                                            dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                                sCaracterDecimal)) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                                                            dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                            tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                                                            decimal dValPrecioIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Precio"].ToString().Replace(",",
                                                                sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                            tblFiltrada.Rows[a]["Precio"] = dValPrecioIncMarkup;
                                                        }
                                                        else
                                                        {
                                                            tblFiltrada.Rows[a]["dblPrecioNino"] = "0";
                                                            tblFiltrada.Rows[a]["dblPrecioNinoImp"] = "0";
                                                            tblFiltrada.Rows[a]["dblImpuestosNino"] = "0";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region [Pax Niño 2]
                                //------------- Niño
                                if (tblTarifas.Rows[i]["strRefereTipoPasajero"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino2", "CNN2")))
                                {
                                    for (int a = 0; a < tblFiltrada.Rows.Count; a++)
                                    {
                                        DataTable tblImpRegistro = null;
                                        //tabla base para impuestos
                                        if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]) != null)
                                        {
                                            if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]).Rows.Count > 0 && tblImpRegistro == null)
                                            {
                                                tblImpRegistro = (DataTable)tblFiltrada.Rows[a]["tImpuestos"];
                                                if (iNumAdt > 2)
                                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, 2, true);
                                                else
                                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumAdt, true);
                                            }
                                        }
                                        if (tblTarifas.Rows[i]["intTipoHabita"].ToString().Equals(tblFiltrada.Rows[a]["intTipoHabita"].ToString()) &&
                                            tblTarifas.Rows[i]["intTipoAlim"].ToString().Equals(tblFiltrada.Rows[a]["intTipoAlim"].ToString()))
                                        {
                                            //solo se fijan valores si el numero de pasajeros niños es mayor que 0
                                            if (iNumNino2 > 0)
                                            {
                                                tblFiltrada.Rows[a]["dblPrecioNino2"] = Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",",
                                                    sCaracterDecimal).Replace(".", sCaracterDecimal));
                                                tblFiltrada.Rows[a]["dblPrecioNino2Imp"] = Convert.ToDecimal(tblTarifas.Rows[i]["Precio"].ToString().Replace(",",
                                                    sCaracterDecimal).Replace(".", sCaracterDecimal));
                                                if (tblTarifas.Rows[i]["tImpuestos"] != DBNull.Value)
                                                {
                                                    if (tblImpRegistro != null)
                                                    {
                                                        //se suman los impuestos del tipo de pax a la tabla base
                                                        tblImpRegistro = setGenerarTablaImpRegistro(tblImpRegistro, (DataTable)tblTarifas.Rows[i]["tImpuestos"], iNumNino2, dValorConversion);
                                                    }
                                                    else
                                                    {
                                                        //los impuestos del tipo de pax son la tabla base
                                                        tblImpRegistro = (DataTable)tblTarifas.Rows[i]["tImpuestos"];
                                                        tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumNinios, true);
                                                    }
                                                    tblFiltrada.Rows[a]["dblImpuestosNino2"] = setDevuelveValorImpuestos((DataTable)tblTarifas.Rows[i]["tImpuestos"]).ToString();
                                                }
                                                if (tblImpRegistro != null)
                                                    tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                                                dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                    sCaracterDecimal)) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                                                dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                                                decimal dValPrecioIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Precio"].ToString().Replace(",",
                                                   sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                tblFiltrada.Rows[a]["Precio"] = dValPrecioIncMarkup;
                                            }
                                            else
                                            {
                                                tblFiltrada.Rows[a]["dblPrecioNino2"] = "0";
                                                tblFiltrada.Rows[a]["dblPrecioNino2Imp"] = "0";
                                                tblFiltrada.Rows[a]["dblImpuestosNino2"] = "0";
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region [Pax Infante]
                                //------------- Infante
                                string sRefereInfJnr = "";
                                //si es un corcuito el pasajero infante se valida como junior
                                if (sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC") && sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ") && sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL"))
                                    sRefereInfJnr = clsValidaciones.GetKeyOrAdd("TipoPasajeroInfante", "INF");
                                else
                                    sRefereInfJnr = clsValidaciones.GetKeyOrAdd("TipoPasajeroJunior", "Jnr");

                                if (tblTarifas.Rows[i]["strRefereTipoPasajero"].ToString().Equals(sRefereInfJnr))
                                {
                                    for (int a = 0; a < tblFiltrada.Rows.Count; a++)
                                    {
                                        DataTable tblImpRegistro = null;
                                        //tabla base para impuestos
                                        if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]) != null)
                                        {
                                            if (((DataTable)tblFiltrada.Rows[a]["tImpuestos"]).Rows.Count > 0 && tblImpRegistro == null)
                                            {
                                                tblImpRegistro = (DataTable)tblFiltrada.Rows[a]["tImpuestos"];
                                                if (iNumAdt > 2)
                                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, 2, true);
                                                else
                                                    tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumAdt, true);
                                            }
                                        }
                                        if (sTipoPlan == clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ"))
                                        {
                                            if (tblTarifas.Rows[i]["NumeroNoches"].ToString().Equals(tblFiltrada.Rows[a]["NumeroNoches"].ToString()) &&
                                                tblTarifas.Rows[i]["intidVigencia"].ToString().Equals(tblFiltrada.Rows[a]["intidVigencia"].ToString()))
                                            {
                                                //solo se fijan valores si el numero de pasajeros infantes(cruceros) o junior(circuitos) es mayor que 0
                                                //tener en cuenta que para cruceros se cargan con tarifa 0 pero con impuestos
                                                if (iNumInf > 0)
                                                {
                                                    tblFiltrada.Rows[a]["dblPrecioInfante"] = Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",",
                                                        sCaracterDecimal).Replace(".", sCaracterDecimal));
                                                    tblFiltrada.Rows[a]["dblPrecioInfanteImp"] = Convert.ToDecimal(tblTarifas.Rows[i]["Precio"].ToString().Replace(",",
                                                        sCaracterDecimal).Replace(".", sCaracterDecimal));
                                                    if (tblTarifas.Rows[i]["tImpuestos"] != DBNull.Value)
                                                    {
                                                        if (tblImpRegistro != null)
                                                        {
                                                            //se suman los impuestos del tipo de pax a la tabla base
                                                            tblImpRegistro = setGenerarTablaImpRegistro(tblImpRegistro, (DataTable)tblTarifas.Rows[i]["tImpuestos"], iNumInf, dValorConversion);
                                                        }
                                                        else
                                                        {
                                                            //los impuestos del tipo de pax son la tabla base
                                                            tblImpRegistro = (DataTable)tblTarifas.Rows[i]["tImpuestos"];
                                                            tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumInf, true);
                                                        }
                                                        tblFiltrada.Rows[a]["dblImpuestosInfante"] = setDevuelveValorImpuestos((DataTable)tblTarifas.Rows[i]["tImpuestos"]).ToString();
                                                    }
                                                    if (tblImpRegistro != null)
                                                        tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                                                    dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                        sCaracterDecimal)) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                                                    dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                    tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                                                    decimal dValPrecioIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Precio"].ToString().Replace(",",
                                                        sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                    tblFiltrada.Rows[a]["Precio"] = dValPrecioIncMarkup;
                                                }
                                                else
                                                {
                                                    tblFiltrada.Rows[a]["dblPrecioInfante"] = "0";
                                                    tblFiltrada.Rows[a]["dblPrecioInfanteImp"] = "0";
                                                    tblFiltrada.Rows[a]["dblImpuestosInfante"] = "0";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (sTipoPlan == clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL"))
                                            {
                                                if (tblTarifas.Rows[i]["intTipoHabita"].ToString().Equals(tblFiltrada.Rows[a]["intTipoHabita"].ToString()) &&
                                                       tblTarifas.Rows[i]["NumeroNoches"].ToString().Equals(tblFiltrada.Rows[a]["NumeroNoches"].ToString()) &&
                                                       tblTarifas.Rows[i]["intTipoAcomoda"].ToString().Equals(tblFiltrada.Rows[a]["intTipoAcomoda"].ToString()) &&
                                                       tblTarifas.Rows[i]["intTipoAlim"].ToString().Equals(tblFiltrada.Rows[a]["intTipoAlim"].ToString()) &&
                                                       tblTarifas.Rows[i]["strTemporada"].ToString().Equals(tblFiltrada.Rows[a]["strTemporada"].ToString()))
                                                {
                                                    //solo se fijan valores si el numero de pasajeros niños es mayor que 0
                                                    if (iNumInf > 0)
                                                    {
                                                        tblFiltrada.Rows[a]["dblPrecioInfante"] = Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                            sCaracterDecimal));
                                                        tblFiltrada.Rows[a]["dblPrecioInfanteImp"] = Convert.ToDecimal(tblTarifas.Rows[i]["Precio"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                            sCaracterDecimal));
                                                        if (tblTarifas.Rows[i]["tImpuestos"] != DBNull.Value)
                                                        {
                                                            if (tblImpRegistro != null)
                                                            {
                                                                //se suman los impuestos del tipo de pax a la tabla base
                                                                tblImpRegistro = setGenerarTablaImpRegistro(tblImpRegistro, (DataTable)tblTarifas.Rows[i]["tImpuestos"], iNumInf, dValorConversion);
                                                            }
                                                            else
                                                            {
                                                                //los impuestos del tipo de pax son la tabla base
                                                                tblImpRegistro = (DataTable)tblTarifas.Rows[i]["tImpuestos"];
                                                                tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumInf, true);
                                                            }
                                                            tblFiltrada.Rows[a]["dblImpuestosInfante"] = setDevuelveValorImpuestos((DataTable)tblTarifas.Rows[i]["tImpuestos"]).ToString();
                                                        }
                                                        tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                                                        dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                            sCaracterDecimal)) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                                                        dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                        tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                                                        decimal dValPrecioIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Precio"].ToString().Replace(",",
                                                            sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                        tblFiltrada.Rows[a]["Precio"] = dValPrecioIncMarkup;
                                                    }
                                                    else
                                                    {
                                                        tblFiltrada.Rows[a]["dblPrecioInfante"] = "0";
                                                        tblFiltrada.Rows[a]["dblPrecioInfanteImp"] = "0";
                                                        tblFiltrada.Rows[a]["dblImpuestosInfante"] = "0";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (tblTarifas.Rows[i]["intTipoHabita"].ToString().Equals(tblFiltrada.Rows[a]["intTipoHabita"].ToString()) &&
                                                    tblTarifas.Rows[i]["intTipoAlim"].ToString().Equals(tblFiltrada.Rows[a]["intTipoAlim"].ToString()))
                                                {
                                                    //solo se fijan valores si el numero de pasajeros infantes(cruceros) o junior(circuitos) es mayor que 0
                                                    //tener en cuenta que para cruceros se cargan con tarifa 0 pero con impuestos
                                                    if (iNumInf > 0)
                                                    {
                                                        tblFiltrada.Rows[a]["dblPrecioInfante"] = Convert.ToDecimal(tblTarifas.Rows[i]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                            sCaracterDecimal));
                                                        tblFiltrada.Rows[a]["dblPrecioInfanteImp"] = Convert.ToDecimal(tblTarifas.Rows[i]["Precio"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                            sCaracterDecimal));
                                                        if (tblTarifas.Rows[i]["tImpuestos"] != DBNull.Value)
                                                        {
                                                            if (tblImpRegistro != null)
                                                            {
                                                                //se suman los impuestos del tipo de pax a la tabla base
                                                                tblImpRegistro = setGenerarTablaImpRegistro(tblImpRegistro, (DataTable)tblTarifas.Rows[i]["tImpuestos"], iNumInf, dValorConversion);
                                                            }
                                                            else
                                                            {
                                                                //los impuestos del tipo de pax son la tabla base
                                                                tblImpRegistro = (DataTable)tblTarifas.Rows[i]["tImpuestos"];
                                                                tblImpRegistro = setValidarValorImp(tblImpRegistro, iNumInf, true);
                                                            }
                                                            tblFiltrada.Rows[a]["dblImpuestosInfante"] = setDevuelveValorImpuestos((DataTable)tblTarifas.Rows[i]["tImpuestos"]).ToString();
                                                        }
                                                        tblFiltrada.Rows[a]["tImpuestos"] = tblImpRegistro;
                                                        dValMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                                            sCaracterDecimal)) * (Convert.ToDecimal(tblFiltrada.Rows[a]["MakUp"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) / 100);
                                                        dValIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                        tblFiltrada.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                                                        decimal dValPrecioIncMarkup = Convert.ToDecimal(tblFiltrada.Rows[a]["Precio"].ToString().Replace(",",
                                                            sCaracterDecimal).Replace(".", sCaracterDecimal)) + dValMarkup;
                                                        tblFiltrada.Rows[a]["Precio"] = dValPrecioIncMarkup;
                                                    }
                                                    else
                                                    {
                                                        tblFiltrada.Rows[a]["dblPrecioInfante"] = "0";
                                                        tblFiltrada.Rows[a]["dblPrecioInfanteImp"] = "0";
                                                        tblFiltrada.Rows[a]["dblImpuestosInfante"] = "0";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }

                #region [Html Impuestos]
                //--------------- se Convierte la tabla de impuestos a un html para poder visualizarla en la pagina
                tblFiltrada.Columns.Add("strHtmlImp");
                tblFiltrada.Columns.Add("intSegmento");
                for (int i = 0; i < tblFiltrada.Rows.Count; i++)
                {
                    decimal dImpuestos = 0;
                    if (tblFiltrada.Rows[i]["tImpuestos"] != DBNull.Value)
                    {
                        DataTable tblImp = (DataTable)tblFiltrada.Rows[i]["tImpuestos"];
                        StringBuilder sbHtml = new StringBuilder();
                        if (tblImp != null)
                        {
                            sbHtml.AppendLine("<Table width='100%'>");
                            sbHtml.AppendLine("<tr>");
                            sbHtml.AppendLine("<td width='100%' colspan='2'><strong><italic>Detalle otros cargos (sujetos a cambios sin previo aviso):</italic></strong></td>");
                            sbHtml.AppendLine("</tr>");
                            if (tblImp.Rows.Count > 0)
                            {
                                for (int x = 0; x < tblImp.Rows.Count; x++)
                                {
                                    sbHtml.AppendLine("<tr>");
                                    if (tblFiltrada.Rows[i]["strRefereMoneda"].ToString().Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                                    {
                                        decimal dValorImpConver = Convert.ToDecimal(Convert.ToInt32(tblImp.Rows[x]["dblvalor"])) * dValorConversionMonedaUnica;
                                        sbHtml.AppendLine("<td width='80%'><strong>" + tblImp.Rows[x]["strImp"].ToString() + "</strong></td><td>" + dValorImpConver.ToString("###,###,###") + "</td>");
                                        dImpuestos = dImpuestos + dValorImpConver;
                                    }
                                    else
                                    {
                                        decimal dValorImpConver = Convert.ToDecimal(tblImp.Rows[x]["dblvalor"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                            sCaracterDecimal)) * dValorConversionMonedaUnica;
                                        sbHtml.AppendLine("<td width='80%'><strong>" + tblImp.Rows[x]["strImp"].ToString() + "</strong></td><td>" + dValorImpConver.ToString("###,##0.00") + "</td>");
                                        dImpuestos = dImpuestos + dValorImpConver;
                                    }
                                    sbHtml.AppendLine("</tr>");
                                }
                            }
                            else
                            {
                                sbHtml.AppendLine("<tr>");
                                sbHtml.AppendLine("<td width='100%'><strong>Todos los impuestos incluidos en la tarifa</strong></td>");
                                sbHtml.AppendLine("</tr>");
                            }
                            sbHtml.AppendLine("</Table>");
                        }
                        else
                        {
                            sbHtml.AppendLine("<Table width='100%'>");
                            sbHtml.AppendLine("<tr>");
                            sbHtml.AppendLine("<td width='100%'><strong>Todos los impuestos incluidos en la tarifa</strong></td>");
                            sbHtml.AppendLine("</tr>");
                            sbHtml.AppendLine("</Table>");
                        }
                        tblFiltrada.Rows[i]["strHtmlImp"] = sbHtml.ToString();
                    }
                    tblFiltrada.Rows[i]["dblSumaImpuestos"] = dImpuestos.ToString();

                    if (tblFiltrada.Rows[i]["strRefereMoneda"].ToString().Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                        tblFiltrada.Rows[i]["dblSumaImpuestosFormato"] = dImpuestos.ToString("###,###.##");
                    else
                        tblFiltrada.Rows[i]["dblSumaImpuestosFormato"] = dImpuestos.ToString("###,##0.00");

                    tblFiltrada.Rows[i]["intSegmento"] = iSegmento.ToString();
                }
                #endregion
                #region [Llenado vacios]
                //--------------- se validan los vacios para no tener problemas al convertir en caso de que no se haya cargado algun tipo de pax
                for (int i = 0; i < tblFiltrada.Rows.Count; i++)
                {
                    if (tblFiltrada.Rows[i]["dblPrecioAdt3"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioAdt3"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioAdt4"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioAdt4"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioNino"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioNino2"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioNino2"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioInfante"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioAdt3Imp"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioAdt3Imp"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioAdt4Imp"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioAdt4Imp"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioNinoImp"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioNinoImp"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioNino2Imp"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioNino2Imp"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioInfanteImp"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioInfanteImp"] = "0";

                    if (tblFiltrada.Rows[i]["dblImpuestosAdt3"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblImpuestosAdt3"] = "0";

                    if (tblFiltrada.Rows[i]["dblImpuestosAdt4"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblImpuestosAdt4"] = "0";

                    if (tblFiltrada.Rows[i]["dblImpuestosNino"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblImpuestosNino"] = "0";

                    if (tblFiltrada.Rows[i]["dblImpuestosNino2"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblImpuestosNino2"] = "0";

                    if (tblFiltrada.Rows[i]["dblImpuestosInfante"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblImpuestosInfante"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioAdt3Formato"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioAdt3Formato"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioAdt4Formato"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioAdt4Formato"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioNinoFormato"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioNinoFormato"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioNino2Formato"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioNino2Formato"] = "0";

                    if (tblFiltrada.Rows[i]["dblPrecioInfanteFormato"].ToString().Equals(""))
                        tblFiltrada.Rows[i]["dblPrecioInfanteFormato"] = "0";


                }
                #endregion
                #region [Validacion Tarifas vacias]
                //--------------- se validan los vacios para no tener problemas al convertir en caso de que no se haya cargado algun tipo de pax y solo para tipos de plan diferentes a traslados
                //if (sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS"))
                //{
                //    for (int i = 0; i < tblFiltrada.Rows.Count; i++)
                //    {
                //        bool bDejar = true;
                //        if (tblFiltrada.Rows[i]["dblPrecioAdt3"].ToString().Equals("0") && tblFiltrada.Rows[i]["dblImpuestosAdt3"].ToString().Equals("0") && iNumAdt3 > 0)
                //            bDejar = false;

                //        if (tblFiltrada.Rows[i]["dblPrecioAdt4"].ToString().Equals("0") && tblFiltrada.Rows[i]["dblImpuestosAdt4"].ToString().Equals("0") && iNumAdt4 > 0)
                //            bDejar = false;

                //        if (tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Equals("0") && tblFiltrada.Rows[i]["dblImpuestosNino"].ToString().Equals("0") && iNumNinios > 0)
                //            bDejar = false;

                //        if (tblFiltrada.Rows[i]["dblPrecioNino2"].ToString().Equals("0") && tblFiltrada.Rows[i]["dblImpuestosNino2"].ToString().Equals("0") && iNumNino2 > 0)
                //            bDejar = false;

                //        if (tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Equals("0") && tblFiltrada.Rows[i]["dblImpuestosInfante"].ToString().Equals("0") && iNumInf > 0)
                //            bDejar = false;

                //        if (!bDejar)
                //        {
                //            tblFiltrada.Rows.RemoveAt(i);
                //            i--;
                //        }
                //    }
                //}
                #endregion
                #region [Llenado totales]
                //--------------- se calculan los totales
                int iNumPax3 = 0;
                if (sTipoPlan == clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS"))
                {
                    iNumAdt = 1;
                    iNumNinios = 0;
                }
                else
                {
                    if (iNumAdt > 2)
                    {
                        if (sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")
                            && sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanSouvenir", "SOUV")
                            && sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ")
                            && sTipoPlan != clsValidaciones.GetKeyOrAdd("TipoPlanApartamento", "APTO"))
                        {
                            //if (sTipoPlan == clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC"))
                            //    iNumPax3 = iNumAdt - 2;
                            //else
                            iNumPax3 = 1;
                            iNumAdt = 2;
                        }
                    }
                }
                for (int i = 0; i < tblFiltrada.Rows.Count; i++)
                {
                    if (sTipoPlan == clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC"))
                    {
                        tblFiltrada.Rows[i]["Tarifa"] = (Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                            sCaracterDecimal)) * iNumAdt) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt3"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                            sCaracterDecimal)) * iNumPax3) + Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt4"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal));
                        if (!tblFiltrada.Rows[i]["strRefereMoneda"].ToString().Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                        {
                            tblFiltrada.Rows[i]["dblTotalImpuestos"] = clsValidaciones.getDecimalNotRound(Convert.ToString((Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal))) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                sCaracterDecimal))) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumInf)
                            + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino2"].ToString()) * iNumNino2) + Convert.ToDecimal(tblFiltrada.Rows[i]["dblSumaImpuestos"].ToString())));

                            tblFiltrada.Rows[i]["dblTotalSinImpuestos"] = clsValidaciones.getDecimalNotRound(Convert.ToString((Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                sCaracterDecimal))) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumInf)
                               + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino2"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumNino2))));
                        }
                        else
                        {
                            tblFiltrada.Rows[i]["dblTotalImpuestos"] = clsValidaciones.getDecimalRound(Convert.ToString((Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal))) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                sCaracterDecimal))) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumInf)
                            + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino2"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumNino2) +
                            Convert.ToDecimal(tblFiltrada.Rows[i]["dblSumaImpuestos"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal))));

                            tblFiltrada.Rows[i]["dblTotalSinImpuestos"] = clsValidaciones.getDecimalRound(Convert.ToString((Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                sCaracterDecimal))) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumInf)
                               + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino2"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumNino2))));
                        }
                    }
                    else
                    {
                        if (!tblFiltrada.Rows[i]["strRefereMoneda"].ToString().Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                        {
                            tblFiltrada.Rows[i]["dblTotalImpuestos"] = clsValidaciones.getDecimalNotRound(Convert.ToString((Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumAdt) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt3"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                sCaracterDecimal)) * iNumPax3) + Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt4"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) +
                                (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumNinoProv) +
                                (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumInf)
                                + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino2"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumNino2) +
                                Convert.ToDecimal(tblFiltrada.Rows[i]["dblSumaImpuestos"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal))));

                            tblFiltrada.Rows[i]["dblTotalSinImpuestos"] = clsValidaciones.getDecimalNotRound(Convert.ToString((Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumAdt) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt3"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                sCaracterDecimal)) * iNumPax3) + Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt4"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) +
                                (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumNinoProv) +
                                (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumInf)
                                + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino2"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumNino2)));
                        }
                        else
                        {
                            tblFiltrada.Rows[i]["dblTotalImpuestos"] = clsValidaciones.getDecimalRound(Convert.ToString((Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumAdt) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt3"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                sCaracterDecimal)) * iNumPax3) + Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt4"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) +
                                (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumNinoProv) +
                                (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumInf)
                                + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino2"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumNino2) +
                                Convert.ToDecimal(tblFiltrada.Rows[i]["dblSumaImpuestos"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal))));

                            tblFiltrada.Rows[i]["dblTotalSinImpuestos"] = clsValidaciones.getDecimalRound(Convert.ToString((Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumAdt) + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt3"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                                sCaracterDecimal)) * iNumPax3) + Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt4"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) +
                                (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumNinoProv) +
                                (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumInf)
                                + (Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino2"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) * iNumNino2)));
                        }
                    }
                    if (!tblFiltrada.Rows[i]["strRefereMoneda"].ToString().Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                    {
                        tblFiltrada.Rows[i]["dblTotalImpuestosFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblTotalImpuestos"].ToString().Replace(",",
                            sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,##0.00");
                        tblFiltrada.Rows[i]["dblTotalSinImpuestosFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblTotalSinImpuestos"].ToString().Replace(",",
                            sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,##0.00");
                    }
                    else
                    {
                        tblFiltrada.Rows[i]["dblTotalImpuestosFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblTotalImpuestos"].ToString().Replace(",",
                            sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,###,###");
                        tblFiltrada.Rows[i]["dblTotalSinImpuestosFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblTotalSinImpuestos"].ToString().Replace(",",
                            sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,###,###");
                    }
                }
                #endregion
                #region [Llenado No aplica]
                //--------------- se validan los vacios y se pone texto
                for (int i = 0; i < tblFiltrada.Rows.Count; i++)
                {
                    if (tblFiltrada.Rows[i]["Tarifa"].ToString().Equals("0") && iNumAdt == 0)
                    {
                        //tblFiltrada.Rows[i]["Tarifa"] = "No aplica";
                        tblFiltrada.Rows[i]["dblTarifaFormato"] = "No aplica";
                    }
                    else
                    {
                        if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                            tblFiltrada.Rows[i]["dblTarifaFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,###,###");
                        else
                            tblFiltrada.Rows[i]["dblTarifaFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,##0.00");
                    }

                    if (tblFiltrada.Rows[i]["dblPrecioAdt3"].ToString().Equals("0") && iNumAdt3 == 0)
                    {
                        tblFiltrada.Rows[i]["dblPrecioAdt3"] = "No aplica";
                        tblFiltrada.Rows[i]["dblPrecioAdt3Formato"] = "No aplica";
                    }
                    else
                    {
                        if (!tblFiltrada.Rows[i]["dblPrecioAdt3"].ToString().Equals("0"))
                        {
                            if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                                tblFiltrada.Rows[i]["dblPrecioAdt3Formato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt3"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,###,###");
                            else
                                tblFiltrada.Rows[i]["dblPrecioAdt3Formato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt3"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,##0.00");
                        }
                    }

                    if (tblFiltrada.Rows[i]["dblPrecioAdt4"].ToString().Equals("0") && iNumAdt4 == 0)
                    {
                        tblFiltrada.Rows[i]["dblPrecioAdt4"] = "No aplica";
                        tblFiltrada.Rows[i]["dblPrecioAdt4Formato"] = "No aplica";
                    }
                    else
                    {
                        if (!tblFiltrada.Rows[i]["dblPrecioAdt4"].ToString().Equals("0"))
                        {
                            if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                                tblFiltrada.Rows[i]["dblPrecioAdt4Formato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt4"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,###,###");
                            else
                                tblFiltrada.Rows[i]["dblPrecioAdt4Formato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioAdt4"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,##0.00");
                        }
                    }

                    if (tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Equals("0") && iNumNinios == 0)
                    {
                        tblFiltrada.Rows[i]["dblPrecioNino"] = "No aplica";
                        tblFiltrada.Rows[i]["dblPrecioNinoFormato"] = "No aplica";
                    }
                    else
                    {
                        if (!tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Equals("0"))
                        {
                            if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                                tblFiltrada.Rows[i]["dblPrecioNinoFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,###,###");
                            else
                                tblFiltrada.Rows[i]["dblPrecioNinoFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,##0.00");
                        }
                    }

                    if (tblFiltrada.Rows[i]["dblPrecioNino2"].ToString().Equals("0"))
                    {
                        if (iNumNino2 > 0)
                        {
                            tblFiltrada.Rows[i]["dblPrecioNino2"] = "0";
                            tblFiltrada.Rows[i]["dblPrecioNino2Formato"] = "0";
                        }
                        else
                        {
                            tblFiltrada.Rows[i]["dblPrecioNino2"] = "No aplica";
                            tblFiltrada.Rows[i]["dblPrecioNino2Formato"] = "No aplica";
                        }
                    }
                    else
                    {
                        if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                            tblFiltrada.Rows[i]["dblPrecioNino2Formato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino2"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,###,###");
                        else
                            tblFiltrada.Rows[i]["dblPrecioNino2Formato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioNino2"].ToString().Replace(",",
                                sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,##0.00");
                    }

                    if (tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Equals("0") && iNumInf == 0)
                    {
                        tblFiltrada.Rows[i]["dblPrecioInfante"] = "No aplica";
                        tblFiltrada.Rows[i]["dblPrecioInfanteFormato"] = "No aplica";
                    }
                    else
                    {
                        if (!tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Equals("0"))
                        {
                            if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                                tblFiltrada.Rows[i]["dblPrecioInfanteFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,###,###");
                            else
                                tblFiltrada.Rows[i]["dblPrecioInfanteFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["dblPrecioInfante"].ToString().Replace(",",
                                    sCaracterDecimal).Replace(".", sCaracterDecimal)).ToString("###,##0.00");
                        }
                    }
                }
                #endregion
                #region [Recorrido aumento markup tabla de tarifas completas]
                for (int a = 0; a < tblTarifasCompletasRegistro.Rows.Count; a++)
                {
                    decimal dValMarkup = Convert.ToDecimal(tblTarifasCompletasRegistro.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                        sCaracterDecimal)) * (Convert.ToDecimal(tblTarifasCompletasRegistro.Rows[a]["MakUp"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                        sCaracterDecimal)) / 100);
                    decimal dValIncMarkup = Convert.ToDecimal(tblTarifasCompletasRegistro.Rows[a]["Tarifa"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                        sCaracterDecimal)) + dValMarkup;
                    decimal dValPrecioIncMarkup = Convert.ToDecimal(tblTarifasCompletasRegistro.Rows[a]["Precio"].ToString().Replace(",", sCaracterDecimal).Replace(".",
                        sCaracterDecimal)) + dValMarkup;
                    tblTarifasCompletasRegistro.Rows[a]["Tarifa"] = dValIncMarkup * dValorConversionMonedaUnica;
                    tblTarifasCompletasRegistro.Rows[a]["Precio"] = dValPrecioIncMarkup * dValorConversionMonedaUnica;

                    DataTable tblImpRegistro = new DataTable();
                    if (((DataTable)tblTarifasCompletasRegistro.Rows[a]["tImpuestos"]).Rows.Count > 0)
                    {
                        tblImpRegistro = (DataTable)tblTarifasCompletasRegistro.Rows[a]["tImpuestos"];
                        tblImpRegistro = setValidarValorImp(tblImpRegistro, 1, true);
                    }
                    tblTarifasCompletasRegistro.Rows[a]["tImpuestos"] = tblImpRegistro;
                }
                #endregion
                tblFiltrada.Columns.Add("tTarifasCompletoRegistro", tblTarifasCompletasRegistro.GetType());
                for (int a = 0; a < tblFiltrada.Rows.Count; a++)
                {
                    tblFiltrada.Rows[a]["tTarifasCompletoRegistro"] = tblTarifasCompletasRegistro;
                }

                return tblFiltrada;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setInsertarTarifasTipoPax";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);

                return null;
            }
        }

        /// <summary>
        /// validacion de impuestospór tipo de pasajero y tipo de impuestos
        /// </summary>
        /// <param name="tblPrincip"></param>
        /// <param name="iCantPax"></param>
        /// <param name="dValorConversion"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setValidarImpUnicoTipoPax(DataTable tblPrincip, int iCantPax, decimal dValorConversion)
        {
            try
            {
                DataView dvImp = new DataView(tblPrincip);
                dvImp.RowFilter = "strRefereTipoImp = '" + clsValidaciones.GetKeyOrAdd("TipoImpuestoPlanTarifa", "TPO_IMP_TARIFA") + "'";
                DataTable tblPrincipal = dvImp.ToTable();
                dvImp.Table = tblPrincip;
                dvImp.RowFilter = "strRefereTipoImp = '" + clsValidaciones.GetKeyOrAdd("TipoImpuestoPlanNoche", "TPO_IMP_NOCHE") + "'";
                DataTable tblSec = dvImp.ToTable();
                tblPrincip = setGenerarTablaImpRegistro(tblPrincipal, tblSec, iCantPax, dValorConversion);
                return tblPrincip;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarImpUnicoTipoPax";
                cParametros.Complemento = "Cotizador";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return tblPrincip;
            }
        }

        /// <summary>
        /// metodo que devuelve de el valor total de todos los impuestos de un tipo determinado
        /// </summary>
        /// <param name="tblTarifas"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public decimal setDevuelveValorImpuestos(DataTable tblTarifas)
        {
            try
            {
                decimal dValor = 0;
                for (int i = 0; i < tblTarifas.Rows.Count; i++)
                {
                    dValor = dValor + Convert.ToDecimal(tblTarifas.Rows[i]["dblValor"].ToString());

                }
                return dValor;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setDevuelveValorImpuestos";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return 0;
            }
        }

        /// <summary>
        /// Metodo que controla los cupos disponibles para mostrar o nolas tarifas
        /// </summary>
        /// <param name="sFechaIni">Fecha inicial</param>
        /// <param name="sFechaFin">Fecha Final</param>
        /// <param name="idPlan">Plan</param>
        /// <param name="tblTarifas">Tabla de tarifas</param>
        /// <param name="PageSource">Usercontrol</param>
        /// <returns>Tabla de tarifas con cupos controlados</returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-29
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setControlarCupos(string sFechaIni, string sFechaFin, string idPlan, DataTable tblTarifas, UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["ControlCupos"] != null)
                {
                    string sControlPersona = clsValidaciones.GetKeyOrAdd("TipoControlCuposPersona", "ConControlPersona");
                    string sControlCuposPropiedad = clsValidaciones.GetKeyOrAdd("TipoControlCuposPropiedad", "ConControPropiedades");
                    string sControlPersonaPropiedad = clsValidaciones.GetKeyOrAdd("TipoControlCuposPersonaPropiedad", "ConControlPersonaPropiedad");

                    if ((PageSource.Request.QueryString["ControlCupos"].Equals(sControlPersona) ||
                        PageSource.Request.QueryString["ControlCupos"].Equals(sControlPersonaPropiedad)) && tblTarifas != null && tblTarifas.Rows.Count > 0)
                    {
                        tblTarifas = setControlarCuposPersona(sFechaIni, sFechaFin, idPlan, tblTarifas, PageSource);
                    }
                    if ((PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPropiedad) ||
                        PageSource.Request.QueryString["ControlCupos"].Equals(sControlPersonaPropiedad)) && tblTarifas != null && tblTarifas.Rows.Count > 0)
                    {
                        tblTarifas = setControlarCuposPropiedad(sFechaIni, sFechaFin, idPlan, tblTarifas, PageSource);
                    }
                    return tblTarifas;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setControlarCupos";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return null;
            }
        }

        /// <summary>
        /// Metodo que controla los cupos disponibles para mostrar o nolas tarifas por persona (Charters)
        /// </summary>
        /// <param name="sFechaIni">Fecha inicial</param>
        /// <param name="sFechaFin">Fecha Final</param>
        /// <param name="idPlan">Plan</param>
        /// <param name="tblTarifas">Tabla de tarifas</param>
        /// <param name="PageSource">Usercontrol</param>
        /// <returns>Tabla de tarifas con cupos controlados</returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-29
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable setControlarCuposPersona(string sFechaIni, string sFechaFin, string idPlan, DataTable tblTarifas, UserControl PageSource)
        {
            try
            {
                DataTable dtLibres = tblTarifas.Copy();
                //#region [Validacion numero de habitaciones por tipo]
                //DataTable tblNumControl = (DataTable)HttpContext.Current.Session["tbltarifascontrol"];
                //DataView dvTpoHab = new DataView();
                //dvTpoHab.Table = dtLibres.Copy();
                //string[] sCols = null;

                //sCols = new string[1];
                //sCols[0] = "intIdVigencia";

                //int iNumPaxTotal = 0;
                //Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");
                //if (rptPasajeros != null)
                //{
                //    for (int i = 0; i < rptPasajeros.Items.Count; i++)
                //    {
                //        DropDownList ddlAdultos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlAdultos");
                //        iNumPaxTotal += Convert.ToInt32(ddlAdultos.SelectedValue);
                //        Repeater rptEdadninos = (Repeater)rptPasajeros.Items[i].FindControl("rptEdadninos");
                //        for (int j = 0; j < rptEdadninos.Items.Count; j++)
                //        {
                //            DropDownList ddlEdadNino = (DropDownList)rptEdadninos.Items[j].FindControl("ddlEdadNino");
                //            if (Convert.ToInt32(ddlEdadNino.SelectedValue) > Convert.ToInt32(clsValidaciones.GetKeyOrAdd("EdadPaxInfante", "2")))
                //                iNumPaxTotal += 1;
                //        }
                //    }
                //}
                //else
                //{
                //    //iNumPaxTotal = Convert.ToInt32(sValue[14]);
                //}

                //DataTable tblTiposHabNum = dvTpoHab.ToTable(true, sCols);
                //tblTiposHabNum.Columns.Add("intNumHabNecesarias");
                //for (int y = 0; y < tblTiposHabNum.Rows.Count; y++)
                //{
                //    tblTiposHabNum.Rows[y]["intNumHabNecesarias"] = "0";
                //    for (int i = 0; i < tblNumControl.Rows.Count; i++)
                //    {
                //        if (tblTiposHabNum.Rows[y]["intidVigencia"].ToString().Equals(tblNumControl.Rows[i]["intidVigencia"].ToString()))
                //        {

                //            tblTiposHabNum.Rows[y]["intNumHabNecesarias"] = iNumPaxTotal;
                //        }
                //    }
                //}
                //#endregion
                //#region [Consulta de propiedades con tipo asociadas al plan]
                //csConsultasPlanes cPlan = new csConsultasPlanes();
                //csGenerales cGen = new csGenerales();


                //string EstadoConfirmada = cGen.ConsultarIdRefere(clsValidaciones.GetKeyOrAdd("EstadoReservaConfirmada", "HK"),
                //    clsValidaciones.GetKeyOrAdd("EstadoReserva", "EstadoReserva"));
                //DataTable tblProp = cPlan.ConsultaPropiedadesPlan(idPlan, sFechaIni, sFechaFin);
                //DataView dvtAcom = new DataView(tblProp);
                //dvtAcom.RowFilter = "intAcomodacion = 0";
                //tblProp = dvtAcom.ToTable();
                //for (int x = 0; x < tblProp.Rows.Count; x++)
                //{
                //    DataTable tblOcupaciones = cPlan.ConsultarOcupacioPropiedad(Convert.ToInt32(tblProp.Rows[x]["intIdPropiedad"].ToString()),
                //        clsSesiones.getIdioma(), sFechaIni, sFechaFin, EstadoConfirmada);
                //    if (tblOcupaciones != null && tblOcupaciones.Rows.Count > 0)
                //        tblProp.Rows[x]["intCantidad"] = Convert.ToInt32(tblProp.Rows[x]["intCantidad"].ToString()) - Convert.ToInt32(tblOcupaciones.Rows[0]["Ocupaciones"].ToString());
                //}

                //#endregion
                //#region [Validacion y eliminacion de la tabla principal de tarifas cuyo tipo de habitacion no tiene el control de cupos]
                //for (int y = 0; y < dtLibres.Rows.Count; y++)
                //{
                //    int iContCoinciden = 0;
                //    for (int z = 0; z < tblProp.Rows.Count; z++)
                //    {
                //        if (dtLibres.Rows[y]["intidVigencia"].ToString().Equals(tblProp.Rows[z]["intidVigencia"].ToString()))
                //        {
                //            iContCoinciden++;
                //        }
                //    }
                //    if (iContCoinciden == 0)
                //    {
                //        dtLibres.Rows.RemoveAt(y);
                //        y--;
                //    }
                //}
                //#endregion
                //#region [Insercion del numero de habitaciones necesarias para cada tipo]
                //tblProp.Columns.Add("intNumHabNecesarias");
                //for (int y = 0; y < tblProp.Rows.Count; y++)
                //{
                //    tblProp.Rows[y]["intNumHabNecesarias"] = "0";
                //    for (int z = 0; z < tblTiposHabNum.Rows.Count; z++)
                //    {
                //        if (tblTiposHabNum.Rows[z]["intidVigencia"].ToString().Equals(tblProp.Rows[y]["intidVigencia"].ToString()))
                //        {
                //            tblProp.Rows[y]["intNumHabNecesarias"] = tblTiposHabNum.Rows[z]["intNumHabNecesarias"].ToString();
                //        }
                //    }
                //}
                //#endregion
                //#region [Validacion numero de habitaciones disponibles verssus numero de habitaciones necesarias]
                //int iHabDisponibles = 0;
                //for (int y = 0; y < tblProp.Rows.Count; y++)
                //{
                //    for (int z = 0; z < dtLibres.Rows.Count; z++)
                //    {
                //        if (dtLibres.Rows[z]["intidVigencia"].ToString().Equals(tblProp.Rows[y]["intidVigencia"].ToString()))
                //        {
                //            int ExistenciasHab = int.Parse(tblProp.Rows[y]["intCantidad"].ToString());
                //            //int HabsOcupadas = cPlan.ConsultaOcupacionPropiedadTipo(tblProp.Rows[y]["intidPropiedad"].ToString(), sFechaIni, sFechaFin, EstadoConfirmada, tblProp.Rows[y]["intidVigencia"].ToString());
                //            //int HabDisponibles = ExistenciasHab - HabsOcupadas;
                //            if (ExistenciasHab < Convert.ToInt32(tblProp.Rows[y]["intNumHabNecesarias"].ToString()))
                //            {
                //                dtLibres.Rows.RemoveAt(z);
                //                z--;
                //                iHabDisponibles = ExistenciasHab;
                //            }
                //        }
                //    }
                //}
                //HttpContext.Current.Session["iHabsDisponibles"] = iHabDisponibles.ToString();
                //#endregion
                return dtLibres;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setControlarCuposPersona";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return null;
            }
        }

        /// <summary>
        /// Metodo que controla los cupos disponibles para mostrar o nolas tarifas por habitacion
        /// </summary>
        /// <param name="sFechaIni">Fecha inicial</param>
        /// <param name="sFechaFin">Fecha Final</param>
        /// <param name="idPlan">Plan</param>
        /// <param name="tblTarifas">Tabla de tarifas</param>
        /// <param name="PageSource">Usercontrol</param>
        /// <returns>Tabla de tarifas con cupos controlados</returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-29
        /// -------------------
        /// Control de Cambios
        /// -------------------  
        /// </remarks>
        public DataTable setControlarCuposPropiedad(string sFechaIni, string sFechaFin, string idPlan, DataTable tblTarifas, UserControl PageSource)
        {
            try
            {
                DataTable dtLibres = tblTarifas.Copy();
                //DataTable dtLibresTotalNecesarias = tblTarifas.Copy();
                //#region [Validacion numero de habitaciones por tipo]
                //DataTable tblNumControl = (DataTable)HttpContext.Current.Session["tbltarifascontrol"];
                //DataView dvTpoHab = new DataView();
                //dvTpoHab.Table = dtLibresTotalNecesarias.Copy();
                //string[] sCols = null;

                //sCols = new string[3];
                //sCols[0] = "intTipoHabita";
                //sCols[1] = "intIdVigencia";
                //sCols[2] = "intTipoAcomoda";

                //int iNumPaxTotal = 0;
                //Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");
                //if (rptPasajeros != null)
                //{
                //    for (int i = 0; i < rptPasajeros.Items.Count; i++)
                //    {
                //        DropDownList ddlAdultos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlAdultos");
                //        iNumPaxTotal += Convert.ToInt32(ddlAdultos.SelectedValue);
                //        Repeater rptEdadninos = (Repeater)rptPasajeros.Items[i].FindControl("rptEdadninos");
                //        for (int j = 0; j < rptEdadninos.Items.Count; j++)
                //        {
                //            DropDownList ddlEdadNino = (DropDownList)rptEdadninos.Items[j].FindControl("ddlEdadNino");
                //            if (Convert.ToInt32(ddlEdadNino.SelectedValue) > Convert.ToInt32(clsValidaciones.GetKeyOrAdd("EdadPaxInfante", "2")))
                //                iNumPaxTotal += 1;
                //        }
                //    }
                //}
                //else
                //{
                //    //iNumPaxTotal = Convert.ToInt32(sValue[14]);
                //}

                //DataTable tblTiposHabNum = dvTpoHab.ToTable(true, sCols);
                //tblTiposHabNum.Columns.Add("intNumHabNecesarias");
                //for (int y = 0; y < tblTiposHabNum.Rows.Count; y++)
                //{
                //    tblTiposHabNum.Rows[y]["intNumHabNecesarias"] = "0";
                //    DataView dvTpoAcomFiltrada = new DataView();
                //    dvTpoAcomFiltrada.Table = tblNumControl.Copy();
                //    //string[] sColumnas = new string[3];
                //    //sColumnas[0] = "intTipoHabita";
                //    //sColumnas[1] = "intidVigencia";
                //    //sColumnas[2] = "intTipoAcomoda";
                //    //DataTable NumControlFiltro = dvTpoAcomFiltrada.ToTable(true, sColumnas);
                //    DataTable NumControlFiltro = dvTpoAcomFiltrada.ToTable();
                //    for (int a = 0; a < NumControlFiltro.Rows.Count - 1; a++)
                //    {
                //        if (NumControlFiltro.Rows[a]["intTipoHabita"].ToString().Equals(NumControlFiltro.Rows[a + 1]["intTipoHabita"].ToString()) &&
                //            NumControlFiltro.Rows[a]["intidVigencia"].ToString().Equals(NumControlFiltro.Rows[a + 1]["intidVigencia"].ToString()) &&
                //            NumControlFiltro.Rows[a]["intTipoAcomoda"].ToString().Equals(NumControlFiltro.Rows[a + 1]["intTipoAcomoda"].ToString()))
                //        {
                //            NumControlFiltro.Rows.RemoveAt(a + 1);
                //            a--;
                //        }
                //    }

                //    for (int i = 0; i < NumControlFiltro.Rows.Count; i++)
                //    {
                //        if (tblTiposHabNum.Rows[y]["intTipoHabita"].ToString().Equals(NumControlFiltro.Rows[i]["intTipoHabita"].ToString()) &&
                //            tblTiposHabNum.Rows[y]["intidVigencia"].ToString().Equals(NumControlFiltro.Rows[i]["intidVigencia"].ToString()) &&
                //            tblTiposHabNum.Rows[y]["intTipoAcomoda"].ToString().Equals(NumControlFiltro.Rows[i]["intTipoAcomoda"].ToString()))
                //        {
                //            tblTiposHabNum.Rows[y]["intNumHabNecesarias"] = Convert.ToInt32(tblTiposHabNum.Rows[y]["intNumHabNecesarias"].ToString()) + 1;

                //        }
                //    }
                //}
                //#endregion
                //#region [Consulta de propiedades con tipo asociadas al plan]
                //csPlanes cPlan = new csPlanes();
                //csGenerales cGen = new csGenerales();
                //string sAplicacion = "0";
                //tblPlanes tPlan = new tblPlanes();
                //tPlan.Get(idPlan, 0);
                //if (tPlan.Respuesta)
                //    sAplicacion = tPlan.intAplicacion.Value;
                //string EstadoConfirmada = cGen.ConsultarIdRefere(clsValidaciones.GetKeyOrAdd("EstadoReservaConfirmada", "HK"),
                //    clsValidaciones.GetKeyOrAdd("EstadoReserva", "EstadoReserva"));
                //DataTable tblProp = cPlan.ConsultaPropiedadesPlan(sAplicacion, idPlan, sFechaIni, sFechaFin);
                //for (int x = 0; x < tblProp.Rows.Count; x++)
                //{
                //    DataTable tblOcupaciones = cPlan.ConsultarOcupacioPropiedad(Convert.ToInt32(tblProp.Rows[x]["intIdPropiedad"].ToString()),
                //        clsSesiones.getIdioma(), sFechaIni, sFechaFin, EstadoConfirmada);
                //    if (tblOcupaciones != null && tblOcupaciones.Rows.Count > 0)
                //        tblProp.Rows[x]["intCantidad"] = Convert.ToInt32(tblProp.Rows[x]["intCantidad"].ToString()) - Convert.ToInt32(tblOcupaciones.Rows[0]["Ocupaciones"].ToString());
                //}

                //#endregion
                //#region [Validacion y eliminacion de la tabla principal de tarifas cuyo tipo de habitacion no tiene el control de cupos]
                //for (int y = 0; y < dtLibres.Rows.Count; y++)
                //{
                //    int iContCoinciden = 0;
                //    for (int z = 0; z < tblProp.Rows.Count; z++)
                //    {
                //        if (dtLibres.Rows[y]["intTipoHabita"].ToString().Equals(tblProp.Rows[z]["intTipoPropiedad"].ToString()) &&
                //            dtLibres.Rows[y]["intidVigencia"].ToString().Equals(tblProp.Rows[z]["intidVigencia"].ToString()) &&
                //            dtLibres.Rows[y]["intTipoAcomoda"].ToString().Equals(tblProp.Rows[z]["intAcomodacion"].ToString()))
                //        {
                //            iContCoinciden++;
                //        }
                //    }
                //    if (iContCoinciden == 0)
                //    {
                //        dtLibres.Rows.RemoveAt(y);
                //        y--;
                //    }
                //}
                //#endregion
                //#region [Insercion del numero de habitaciones necesarias para cada tipo]
                //tblProp.Columns.Add("intNumHabNecesarias");
                //for (int y = 0; y < tblProp.Rows.Count; y++)
                //{
                //    tblProp.Rows[y]["intNumHabNecesarias"] = "0";
                //    for (int z = 0; z < tblTiposHabNum.Rows.Count; z++)
                //    {
                //        if (tblTiposHabNum.Rows[z]["intTipoHabita"].ToString().Equals(tblProp.Rows[y]["intTipoPropiedad"].ToString()) &&
                //            tblTiposHabNum.Rows[z]["intidVigencia"].ToString().Equals(tblProp.Rows[y]["intidVigencia"].ToString()) &&
                //            tblTiposHabNum.Rows[z]["intTipoAcomoda"].ToString().Equals(tblProp.Rows[y]["intAcomodacion"].ToString()))
                //        {
                //            tblProp.Rows[y]["intNumHabNecesarias"] = tblTiposHabNum.Rows[z]["intNumHabNecesarias"].ToString();
                //        }
                //    }
                //}
                //#endregion
                //#region [Validacion numero de habitaciones disponibles verssus numero de habitaciones necesarias]
                //int iHabDisponibles = 0;
                //for (int y = 0; y < tblProp.Rows.Count; y++)
                //{
                //    for (int z = 0; z < dtLibres.Rows.Count; z++)
                //    {
                //        if (dtLibres.Rows[z]["intTipoHabita"].ToString().Equals(tblProp.Rows[y]["intTipoPropiedad"].ToString()) &&
                //            dtLibres.Rows[z]["intidVigencia"].ToString().Equals(tblProp.Rows[y]["intidVigencia"].ToString()) &&
                //            dtLibres.Rows[z]["intTipoAcomoda"].ToString().Equals(tblProp.Rows[y]["intAcomodacion"].ToString()))
                //        {
                //            int ExistenciasHab = int.Parse(tblProp.Rows[y]["intCantidad"].ToString());
                //            //int HabsOcupadas = cPlan.ConsultaOcupacionPropiedadTipo(tblProp.Rows[y]["intidPropiedad"].ToString(), sFechaIni, sFechaFin, EstadoConfirmada, tblProp.Rows[y]["intidVigencia"].ToString());
                //            //int HabDisponibles = ExistenciasHab - HabsOcupadas;
                //            if (ExistenciasHab < Convert.ToInt32(tblProp.Rows[y]["intNumHabNecesarias"].ToString()))
                //            {
                //                dtLibres.Rows.RemoveAt(z);
                //                z--;
                //                iHabDisponibles = ExistenciasHab;
                //            }
                //        }
                //    }
                //}
                //HttpContext.Current.Session["iHabsDisponibles"] = iHabDisponibles.ToString();
                //#endregion
                return dtLibres;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setControlarCuposPropiedad";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return null;
            }
        }

        /// <summary>
        /// metodo que valida la tarifa seleccionada y deselecciona el resto
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-01
        /// -------------------
        /// Control de Cambios
        /// -------------------  
        /// </remarks>
        public void setValidarTarifaSeleccionadaCircuitos(UserControl PageSource, object sender, EventArgs e)
        {
            try
            {
                Repeater rptHoteles = ((Repeater)((RepeaterItem)((Repeater)((RepeaterItem)((RadioButton)sender).Parent).Parent).Parent).Parent);
                for (int i = 0; i < rptHoteles.Items.Count; i++)
                {
                    Repeater rptTarifas = (Repeater)rptHoteles.Items[i].FindControl("rptTarifas");
                    for (int x = 0; x < rptTarifas.Items.Count; x++)
                    {
                        RadioButton rbTarifa = (RadioButton)rptTarifas.Items[x].FindControl("rbTarifa");
                        /*COMPARAMOS LOS RADIOBUTTONS DEL REPETIDOR CON EL QUE GENERO EL EVENTO*/
                        if (!sender.Equals(rbTarifa))
                        {
                            rbTarifa.Checked = false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarTarifaSeleccionada";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo principal para reserva de planes, valida el tipo de plan y genera el proceso correspondiente
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="sender"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-01
        /// -------------------
        /// Control de Cambios
        /// -------------------  
        /// </remarks>
        public void setReservarPlanes(UserControl PageSource, object sender)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["id"] != null)
                {
                    clsCache cCache = new csCache().cCache();
                    if (cCache != null)
                    {
                        if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTarjetas", "TJAS")))
                        {
                            EnviarACarritoDeComprasSeguros(PageSource, sender);
                        }
                        else
                        {
                            if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")))
                            {
                                EnviarACarritoDeComprasCircuitos(PageSource);
                            }
                            else
                            {
                                if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")))
                                {
                                    EnviarACarritoDeComprasToures(PageSource);
                                }
                                else
                                {
                                    if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS")))
                                    {
                                        //EnviarACarritoDeComprasTraslados(PageSource);
                                    }
                                    else
                                    {
                                        if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTarjetas", "TJAS")))
                                        {
                                            //EnviarACarritoDeComprasSeguros(PageSource, sender);
                                        }
                                        else
                                        {
                                            if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanSouvenir", "SOUV")))
                                            {
                                                //EnviarACarritoDeComprasSouvenirs(PageSource);
                                            }
                                            else
                                            {
                                                if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanWs", "PLNWS")))
                                                {
                                                    //EnviarACarritoDeComprasWS(PageSource);
                                                }
                                                else
                                                {
                                                    if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE")))
                                                    {
                                                        //EnviarACarritoDeComprasCruceros(PageSource);
                                                    }
                                                    else
                                                    {
                                                        if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL")))
                                                        {
                                                            //EnviarACarritoDeComprasHoteles(PageSource);
                                                        }
                                                        else
                                                        {
                                                            if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ")))
                                                            {
                                                                //if (!PageSource.ID.ToUpper().Contains("COMIDAS"))
                                                                //    EnviarACarritoDeComprasParques(PageSource);
                                                                //else
                                                                //    EnviarACarritoDeComprasAlimentacion(PageSource);
                                                            }
                                                            else
                                                            {
                                                                //EnviarACarritoDeComprasApartamentos(PageSource);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        csGeneralsPag.FinSesion();
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "SetReservarCruceros";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// metodo que valida que se haya seleccionado una tarifa de la cotizacion para la reserva
        /// </summary>
        /// <param name="PageSource"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-01
        /// -------------------
        /// Control de Cambios
        /// -------------------  
        /// </remarks>
        public bool setValidarSeleccionCabina(UserControl PageSource)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["id"] != null)
                {
                    Repeater rptCabina = (Repeater)PageSource.FindControl("rptCabina");
                    Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");
                    int iContCabinas = 0;
                    for (int i = 0; i < rptCabina.Items.Count; i++)
                    {
                        if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE")) ||
                            PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")) ||
                            PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS")) ||
                            PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanSouvenir", "SOUV")) ||
                            PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanApartamento", "APTO")) ||
                            PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL")) ||
                            PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ")))
                        {
                            Repeater rptTarifas = (Repeater)rptCabina.Items[i].FindControl("rptTarifas");

                            if (rptTarifas != null)
                            {
                                for (int x = 0; x < rptTarifas.Items.Count; x++)
                                {
                                    //si hay algun radiobutton seleccionado se adiciona uno al contador
                                    if (((RadioButton)rptTarifas.Items[x].FindControl("rbTarifa")).Checked)
                                    {
                                        iContCabinas++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Repeater rptHoteles = (Repeater)rptCabina.Items[i].FindControl("rptHoteles");

                            if (rptHoteles != null)
                            {
                                for (int x = 0; x < rptHoteles.Items.Count; x++)
                                {
                                    Repeater rptTarifas = (Repeater)rptHoteles.Items[x].FindControl("rptTarifas");

                                    if (rptTarifas != null)
                                    {
                                        for (int y = 0; y < rptTarifas.Items.Count; y++)
                                        {
                                            //si hay algun radiobutton seleccionado se adiciona uno al contador
                                            if (((RadioButton)rptTarifas.Items[y].FindControl("rbTarifa")).Checked)
                                            {
                                                iContCabinas++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //si el contador de selecciones es igual al numero de items del repetidor de pasajeros si se eligieron todas las tarifas
                    if (iContCabinas == rptPasajeros.Items.Count)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarSeleccionCabina";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                return false;
            }
        }

        /// <summary>
        /// Metodo principal de llenado del dataset del carrito de compras, datos y pax
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-01
        /// -------------------
        /// Control de Cambios
        /// -------------------  
        /// </remarks>
        private void EnviarACarritoDeComprasCircuitos(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != "0" && PageSource.Request.QueryString["id"] != "0" &&
                    PageSource.Request.QueryString["ControlCupos"] != "0")
                {
                    int iPaxControl = 0;
                    clsCache cCache = new csCache().cCache();

                    if (cCache != null)
                    {
                        DropDownList ddlDuracion = (DropDownList)PageSource.FindControl("ddlDuracion");
                        Repeater rptCabina = (Repeater)PageSource.FindControl("rptCabina");
                        Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");
                        DataTable tblDatosTarifas = (DataTable)HttpContext.Current.Session["tbltarifas"];
                        if (rptCabina != null)
                        {
                            decimal dValorConversionMonedaUnica = Convert.ToDecimal(HttpContext.Current.Session["dValorConversionMonedaUnica"]);
                            string sSesion = cCache.SessionID;
                            clsCacheControl cCacheControl = new clsCacheControl();
                            csGenerales cGeneral = new csGenerales();
                            cGeneral.Conexion = clsSesiones.getConexion();
                            csCarrito csCarCompUnion = new csCarrito("Reserva" + sSesion, "CarritoCompras");
                            String int_Id_EstadoSolicitada = new CsConsultasVuelos().ConsultaCodigo(clsValidaciones.GetKeyOrAdd("EstadoReservaInicialAereo", "HK"), "TBLESTADOS_RESERVA", "INTCODE", "STRCODE");

                            Utils.Utils csUtileria = new Utils.Utils();

                            for (int i = 0; i < rptCabina.Items.Count; i++)
                            {
                                Repeater rptHoteles = (Repeater)rptCabina.Items[i].FindControl("rptHoteles");

                                DropDownList ddlAdultos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlAdultos");
                                DropDownList ddlNinos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlNinos");
                                DropDownList ddlJuniors = (DropDownList)rptPasajeros.Items[i].FindControl("ddlJuniors");
                                TextBox txtNumRealAdt = (TextBox)rptPasajeros.Items[i].FindControl("txtNumRealAdt");
                                TextBox txtNumRealCnn = (TextBox)rptPasajeros.Items[i].FindControl("txtNumRealCnn");
                                Repeater rptEdadninos = (Repeater)rptPasajeros.Items[i].FindControl("rptEdadninos");
                                if (rptHoteles != null)
                                {
                                    for (int n = 0; n < rptHoteles.Items.Count; n++)
                                    {
                                        string idProveedor = ((Label)rptHoteles.Items[n].FindControl("lblIdHotel")).Text;
                                        string NombreProveedor = ((Label)rptHoteles.Items[n].FindControl("lblNombreHotel")).Text;
                                        Repeater rptTarifas = (Repeater)rptHoteles.Items[n].FindControl("rptTarifas");
                                        if (rptTarifas != null)
                                        {
                                            for (int x = 0; x < rptTarifas.Items.Count; x++)
                                            {
                                                if (((RadioButton)rptTarifas.Items[x].FindControl("rbTarifa")).Checked)
                                                {
                                                    #region Insercion datos de plan
                                                    int intCodigoTarifa = Convert.ToInt32(((Label)rptTarifas.Items[x].FindControl("lblIdTarifa")).Text);
                                                    DataRow FilaSeleccionada = GetTablaTarifas(tblDatosTarifas, intCodigoTarifa, i + 1);
                                                    FilaSeleccionada = setValidarValorTexto(FilaSeleccionada);

                                                    DataTable tblTarifasCompletas = (DataTable)FilaSeleccionada["tTarifasCompletoRegistro"];

                                                    int iNochesAdic = 0;
                                                    if (ddlDuracion != null)
                                                        iNochesAdic = Convert.ToInt32(ddlDuracion.SelectedValue) - 1 - Convert.ToInt32(FilaSeleccionada["NumeroNoches"].ToString());
                                                    int intNumeroPasajerosAdultos = 0;
                                                    int intNumeroPasajerosNinos = 0;

                                                    if (txtNumRealAdt != null && txtNumRealCnn != null)
                                                    {
                                                        intNumeroPasajerosAdultos = Convert.ToInt32(txtNumRealAdt.Text);
                                                        intNumeroPasajerosNinos = Convert.ToInt32(txtNumRealCnn.Text);
                                                    }
                                                    else
                                                    {
                                                        intNumeroPasajerosAdultos = Convert.ToInt32(ddlAdultos.SelectedValue);
                                                        intNumeroPasajerosNinos = Convert.ToInt32(ddlNinos.SelectedValue);
                                                    }

                                                    int intNumeroPasajerosJunior = Convert.ToInt32(ddlJuniors.SelectedValue);
                                                    int intTotalPasajeros = intNumeroPasajerosAdultos + intNumeroPasajerosNinos + intNumeroPasajerosJunior;
                                                    csCarCompUnion.IntCodigoPlan = PageSource.Request.QueryString["id"];
                                                    csCarCompUnion.StrIdentificadorDelPlan = PageSource.Request.QueryString["TipoPlan"];
                                                    csCarCompUnion.IntCodigoTarifa = FilaSeleccionada["IdTarifa"].ToString();
                                                    csCarCompUnion.IntcantidadPersonas = intTotalPasajeros.ToString();
                                                    csCarCompUnion.StrCategoria = FilaSeleccionada["strCategoria"].ToString();
                                                    csCarCompUnion.IntCategoria = FilaSeleccionada["IdCategoria"].ToString();
                                                    string sCiudadesPlan = GetCiudadesPlan(PageSource.Request.QueryString["id"]);
                                                    csCarCompUnion.StrCiudad = sCiudadesPlan;
                                                    csCarCompUnion.StrFechaInicial = Convert.ToDateTime(FilaSeleccionada["Desde"].ToString()).ToString(sFormatoFecha);
                                                    csCarCompUnion.StrFechaFinal = Convert.ToDateTime(FilaSeleccionada["Hasta"].ToString()).ToString(sFormatoFecha);
                                                    try
                                                    {
                                                        int iNochesPlanes = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("DiasVencimientoPlanes", "3"));
                                                        csCarCompUnion.StrFechaVencimiento = DateTime.Now.AddDays(iNochesPlanes).ToString(sFormatoFecha);
                                                    }
                                                    catch (Exception Ex)
                                                    {
                                                        csCarCompUnion.StrFechaVencimiento = DateTime.Now.AddDays(3).ToString(sFormatoFecha);
                                                    }

                                                    csCarCompUnion.IntNumeroNoches = FilaSeleccionada["intNoches"].ToString();
                                                    csCarCompUnion.IntNumeroDias = FilaSeleccionada["intDias"].ToString();
                                                    csCarCompUnion.IntNochesAdicionales = iNochesAdic.ToString();
                                                    csCarCompUnion.StrTemporada = FilaSeleccionada["strTemporada"].ToString();
                                                    csCarCompUnion.IntTemporada = FilaSeleccionada["idTemporada"].ToString();
                                                    csCarCompUnion.IntTipoHabitacion = FilaSeleccionada["TipoHabitacion"].ToString();
                                                    csCarCompUnion.StrTipoHabitacion = FilaSeleccionada["strTipoHabitacion"].ToString();
                                                    csCarCompUnion.StrTipoPropiedad = FilaSeleccionada["strSubTipoHab"].ToString();

                                                    if (clsValidaciones.GetKeyOrAdd("dConversionMonedaUnica", "False").ToUpper().Equals("TRUE"))
                                                    {
                                                        //otblRefere.Get(clsValidaciones.GetKeyOrAdd("Moneda", "Moneda"),
                                                        //    clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP"));
                                                        //if (otblRefere.Respuesta)
                                                        //{
                                                        //    csCarCompUnion.StrTipoMoneda = otblRefere.strRefere.Value;
                                                        //    csCarCompUnion.IntTipoMoneda = otblRefere.intidRefere.Value;
                                                        //}
                                                    }
                                                    else
                                                    {
                                                        csCarCompUnion.StrTipoMoneda = FilaSeleccionada["strRefereMoneda"].ToString();
                                                        csCarCompUnion.IntTipoMoneda = FilaSeleccionada["Moneda"].ToString();
                                                    }

                                                    string strTipoServicio = clsValidaciones.GetKeyOrAdd("TipoServicioPlan", "PLN");
                                                    string strCodigoTipoServ = new CsConsultasVuelos().ConsultaCodigo(strTipoServicio, "TBLTPOSERVICIO",
                                                        "INTID", "STRCODIGO");
                                                    if (strCodigoTipoServ == null || strCodigoTipoServ == "")
                                                        strCodigoTipoServ = "2";
                                                    csCarCompUnion.IntTipoPlan = strCodigoTipoServ;

                                                    csCarCompUnion.StrTipoPlan = PageSource.Request.QueryString["TipoPlan"];
                                                    if (FilaSeleccionada["strRefereMoneda"].ToString().Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                                                        csCarCompUnion.IntValorTotal = Convert.ToInt32(Convert.ToDecimal(FilaSeleccionada["dblTotalImpuestos"].ToString().Replace(",",
                                                            sCaracterDecimal).Replace(".", sCaracterDecimal))).ToString();
                                                    else
                                                        csCarCompUnion.IntValorTotal = Convert.ToString(Convert.ToDecimal(FilaSeleccionada["dblTotalImpuestos"].ToString().Replace(",",
                                                            sCaracterDecimal).Replace(".", sCaracterDecimal)));

                                                    DataTable tblTextosPlanes = (DataTable)PageSource.Session["$TablaTextosPlanes"];
                                                    if (tblTextosPlanes != null)
                                                    {
                                                        try
                                                        {
                                                            csCarCompUnion.StrNombrePlan = GetTextosPlan(clsValidaciones.GetKeyOrAdd("TipoTextoPlanNombre", "NAME"),
                                                                tblTextosPlanes);

                                                            csCarCompUnion.StrDescripcion = GetTextosPlan(clsValidaciones.GetKeyOrAdd("TipoTextoPlanDesc", "DESC"),
                                                                tblTextosPlanes);

                                                        }
                                                        catch { }
                                                    }
                                                    csCarCompUnion.StrAcomodacion = FilaSeleccionada["strAcomodacion"].ToString();
                                                    csCarCompUnion.IntAcomodacion = FilaSeleccionada["TipoAcomodacion"].ToString();
                                                    csCarCompUnion.IntSegmento = FilaSeleccionada["intSegmento"].ToString();
                                                    csCarCompUnion.StrCantidadPersonas = "Adultos: " + (intNumeroPasajerosAdultos.ToString()) + " / Niños: " + (intNumeroPasajerosNinos.ToString()) + " / Juniors: " + (intNumeroPasajerosJunior.ToString());
                                                    csCarCompUnion.StrDuracion = csCarCompUnion.IntNumeroDias + " Días - " + csCarCompUnion.IntNumeroNoches + " Noches";
                                                    csCarCompUnion.StrRegimen = FilaSeleccionada["strTipoAlim"].ToString();
                                                    csCarCompUnion.IntVigencia = FilaSeleccionada["idVigencia"].ToString();
                                                    csCarCompUnion.StrObservacion = "";
                                                    csCarCompUnion.StrPasajeros = "Adultos: " + (intNumeroPasajerosAdultos.ToString()) + " / Niños: " + (intNumeroPasajerosNinos.ToString()) + " / Juniors: " + (intNumeroPasajerosJunior.ToString());

                                                    if (((Label)rptTarifas.Parent.FindControl("lblHoteles")) != null)
                                                    {
                                                        csCarCompUnion.StrHoteles = ((Label)rptTarifas.Parent.FindControl("lblHoteles")).Text;
                                                        csCarCompUnion.StrProveedor = ((Label)rptTarifas.Parent.FindControl("lblHoteles")).Text;
                                                        csCarCompUnion.StrOperador = ((Label)rptTarifas.Parent.FindControl("lblHoteles")).Text;
                                                    }
                                                    else
                                                    {
                                                        csCarCompUnion.IntProveedor = idProveedor;
                                                        csCarCompUnion.StrProveedor = NombreProveedor;
                                                        csCarCompUnion.StrOperador = NombreProveedor;
                                                    }

                                                    /*ESTADO SOLICITADA*/
                                                    csCarCompUnion.IntEstado = int_Id_EstadoSolicitada;

                                                    csCarCompUnion.AddFields();
                                                    #endregion

                                                    #region Insercion Tipos de pasajeros y pasajeros
                                                    //se agregan los tipos de pasajero
                                                    int iPasajero3 = 0;
                                                    if (intNumeroPasajerosAdultos > 2)
                                                    {
                                                        iPasajero3 = intNumeroPasajerosAdultos - 2;
                                                        intNumeroPasajerosAdultos = 2;
                                                    }

                                                    decimal dValorTarifaAdt = 0;
                                                    decimal dValorTotalAdt = 0;
                                                    decimal dValorTarifaAdt3 = 0;
                                                    decimal dValorTotalAdt3 = 0;
                                                    decimal dValorTarifaCnn = 0;
                                                    decimal dValorTotalCnn = 0;
                                                    decimal dValorTarifaJnr = 0;
                                                    decimal dValorTotalJnr = 0;

                                                    csConsultasGenerales UsuariosCons = new csConsultasGenerales();
                                                    string sTipoPaxAdt = "0";
                                                    string sTipoPaxAdt3 = "0";
                                                    string sTipoPaxCnn = "0";
                                                    string sTipoPaxJnr = "0";
                                                    DataTable dtTipoUser = null;
                                                    if (intNumeroPasajerosAdultos > 0)
                                                    {
                                                        dtTipoUser = UsuariosCons.ConReferenciaTiposPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT"));
                                                        if (dtTipoUser != null)
                                                            sTipoPaxAdt = dtTipoUser.Rows[0]["INTCODE"].ToString();

                                                        dValorTarifaAdt = (Convert.ToDecimal(FilaSeleccionada["Precio"].ToString().Replace(",",
                                                            sCaracterDecimal).Replace(".", sCaracterDecimal)) - Convert.ToDecimal(FilaSeleccionada["Impuesto"].ToString().Replace(",",
                                                            sCaracterDecimal).Replace(".", sCaracterDecimal))) * dValorConversionMonedaUnica;
                                                        dValorTotalAdt = Convert.ToDecimal(FilaSeleccionada["Precio"].ToString().Replace(",",
                                                            sCaracterDecimal).Replace(".", sCaracterDecimal)) * dValorConversionMonedaUnica;
                                                        csCarCompUnion.SaveTipoPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT"), Convert.ToInt32(sTipoPaxAdt),
                                                            dValorTotalAdt.ToString(), dValorTarifaAdt.ToString(), FilaSeleccionada["Impuesto"].ToString(),
                                                            Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()));
                                                    }

                                                    if (iPasajero3 > 0)
                                                    {
                                                        dtTipoUser = UsuariosCons.ConReferenciaTiposPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdt3", "ADT3"));
                                                        if (dtTipoUser != null)
                                                            sTipoPaxAdt3 = dtTipoUser.Rows[0]["INTCODE"].ToString();

                                                        dValorTarifaAdt3 = Convert.ToDecimal(FilaSeleccionada["dblPrecioAdt3"].ToString().Replace(",",
                                                           sCaracterDecimal).Replace(".", sCaracterDecimal)) / iPasajero3;
                                                        dValorTotalAdt3 = Convert.ToDecimal(FilaSeleccionada["dblPrecioAdt3Imp"].ToString().Replace(",",
                                                            sCaracterDecimal).Replace(".", sCaracterDecimal)) / iPasajero3;
                                                        csCarCompUnion.SaveTipoPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdt3", "ADT3"), Convert.ToInt32(sTipoPaxAdt3), dValorTotalAdt3.ToString(), dValorTarifaAdt3.ToString(),
                                                            FilaSeleccionada["dblImpuestosAdt3"].ToString(), Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()));
                                                    }

                                                    if (intNumeroPasajerosNinos > 0)
                                                    {
                                                        dtTipoUser = UsuariosCons.ConReferenciaTiposPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN"));
                                                        if (dtTipoUser != null)
                                                            sTipoPaxCnn = dtTipoUser.Rows[0]["INTCODE"].ToString();

                                                        dValorTarifaCnn = Convert.ToDecimal(FilaSeleccionada["dblPrecioNino"].ToString().Replace(",",
                                                          sCaracterDecimal).Replace(".", sCaracterDecimal)) / intNumeroPasajerosNinos;
                                                        dValorTotalCnn = (Convert.ToDecimal(FilaSeleccionada["dblPrecioNinoImp"].ToString().Replace(",",
                                                            sCaracterDecimal).Replace(".", sCaracterDecimal)) / intNumeroPasajerosNinos) * dValorConversionMonedaUnica;
                                                        csCarCompUnion.SaveTipoPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN"), Convert.ToInt32(sTipoPaxCnn), dValorTotalCnn.ToString(), dValorTarifaCnn.ToString(),
                                                            FilaSeleccionada["dblImpuestosNino"].ToString(), Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()));
                                                    }
                                                    if (intNumeroPasajerosJunior > 0)
                                                    {
                                                        dtTipoUser = UsuariosCons.ConReferenciaTiposPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroJunior", "Jnr"));
                                                        if (dtTipoUser != null)
                                                            sTipoPaxJnr = dtTipoUser.Rows[0]["INTCODE"].ToString();

                                                        dValorTarifaJnr = Convert.ToDecimal(FilaSeleccionada["dblPrecioInfante"].ToString().Replace(",",
                                                         sCaracterDecimal).Replace(".", sCaracterDecimal)) / intNumeroPasajerosJunior;
                                                        dValorTotalJnr = Convert.ToDecimal(FilaSeleccionada["dblPrecioInfanteImp"].ToString().Replace(",",
                                                            sCaracterDecimal).Replace(".", sCaracterDecimal)) / intNumeroPasajerosJunior;
                                                        csCarCompUnion.SaveTipoPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroJunior", "Jnr"), Convert.ToInt32(sTipoPaxJnr), dValorTotalJnr.ToString(), dValorTarifaJnr.ToString(),
                                                            FilaSeleccionada["dblImpuestosInfante"].ToString(), Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()));
                                                    }

                                                    //se hace el recorrido de los tipos de pasajero
                                                    for (int a = 0; a < intNumeroPasajerosAdultos; a++)
                                                    {
                                                        iPaxControl += 1;
                                                        DataView dvFiltroEdad = new DataView(tblTarifasCompletas);
                                                        dvFiltroEdad.RowFilter = "strRefereTipoPasajero = '" + clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT") + "'";
                                                        DataTable dtResFiltro = dvFiltroEdad.ToTable();
                                                        if (dtResFiltro.Rows.Count > 0)
                                                        {
                                                            csCarCompUnion.SavePerson(null, null, "Adulto", Convert.ToInt32(sTipoPaxAdt), null, dValorTotalAdt, "",
                                                                Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()), dValorTarifaAdt);
                                                        }
                                                    }

                                                    for (int a = 0; a < iPasajero3; a++)
                                                    {
                                                        iPaxControl += 1;
                                                        csCarCompUnion.SavePerson(null, null, "Adulto", Convert.ToInt32(sTipoPaxAdt3), null, dValorTotalAdt3, "",
                                                            Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()), dValorTarifaAdt3);
                                                    }

                                                    for (int a = 0; a < intNumeroPasajerosNinos; a++)
                                                    {
                                                        DropDownList ddlEdadNino = (DropDownList)rptEdadninos.Items[a].FindControl("ddlEdadNino");
                                                        if (ddlEdadNino != null)
                                                        {
                                                            if (Convert.ToInt32(ddlEdadNino.SelectedValue) > Convert.ToInt32(clsValidaciones.GetKeyOrAdd("EdadPaxInfante", "2")))
                                                                iPaxControl += 1;
                                                            DataView dvFiltroEdad = new DataView(tblTarifasCompletas);
                                                            dvFiltroEdad.RowFilter = "strRefereTipoPasajero = '" + clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN") +
                                                                "' AND " + ddlEdadNino.SelectedValue + " >= EdadMinPax AND " + ddlEdadNino.SelectedValue + " <= EdadMaxPax";
                                                            DataTable dtResFiltro = dvFiltroEdad.ToTable();
                                                            if (dtResFiltro.Rows.Count > 0)
                                                            {
                                                                csCarCompUnion.SavePerson(null, null, "Niño - " + ddlEdadNino.SelectedValue + " años", Convert.ToInt32(sTipoPaxCnn), null, dValorTotalCnn, "",
                                                                    Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()), dValorTarifaCnn);
                                                            }
                                                        }
                                                    }

                                                    for (int a = 0; a < intNumeroPasajerosJunior; a++)
                                                    {
                                                        csCarCompUnion.SavePerson(null, null, "Junior", Convert.ToInt32(sTipoPaxJnr), null, dValorTotalJnr, "",
                                                            Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()), dValorTarifaJnr);
                                                    }

                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            csCarCompUnion.Save();
                            string sDispAereo = "";
                            Label lblDisponibilidad = (Label)PageSource.FindControl("lblDisponibilidad");
                            if (lblDisponibilidad != null && lblDisponibilidad.Text.ToUpper().Equals("TRUE"))
                                sDispAereo = "&Aereo=SI";
                            if (clsValidaciones.GetKeyOrAdd("RegistroFormReserva", "False").ToUpper() == "TRUE")
                            {
                                clsSesiones.setPantalleRespuestaLogin("0");
                                clsValidaciones.RedirectPagina("../Presentacion/ReservaCircuito.aspx?Codigo=" + PageSource.Request.QueryString["id"] + "&TipoPlan=" + PageSource.Request.QueryString["TipoPlan"] + sDispAereo + "&ControlCupos=" + PageSource.Request.QueryString["ControlCupos"] + "&PaxControl=" + iPaxControl);
                            }
                            else
                            {
                                clsSesiones.setPantalleRespuestaLogin("../Presentacion/ReservaCircuito.aspx?Codigo=" + PageSource.Request.QueryString["id"] + "&TipoPlan=" + PageSource.Request.QueryString["TipoPlan"] + sDispAereo + "&ControlCupos=" + PageSource.Request.QueryString["ControlCupos"] + "&PaxControl=" + iPaxControl);
                                if (cCache.Verifica)
                                {                                    
                                    clsValidaciones.RedirectPagina("../Presentacion/ReservaCircuito.aspx?Codigo=" + PageSource.Request.QueryString["id"] + "&TipoPlan=" + PageSource.Request.QueryString["TipoPlan"] + sDispAereo + "&ControlCupos=" + PageSource.Request.QueryString["ControlCupos"] + "&PaxControl=" + iPaxControl);
                                }
                                else
                                {
                                    clsValidaciones.RedirectPagina("../Presentacion/Login.aspx");
                                }
                            }
                        }
                    }
                    else
                    {
                        csGeneralsPag.FinSesion();
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "EnviarACarritoDeComprasCircuitos";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo principal de llenado del dataset del carrito de compras, datos y pax para toures
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-01
        /// -------------------
        /// Control de Cambios
        /// -------------------  
        /// </remarks>
        private void EnviarACarritoDeComprasToures(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["id"] != null)
                {
                    clsCache cCache = new csCache().cCache();

                    if (cCache != null)
                    {
                        Label lblIdBarco = (Label)PageSource.FindControl("lblIdBarco");
                        Repeater rptCabina = (Repeater)PageSource.FindControl("rptCabina");
                        Repeater rptPasajeros = (Repeater)PageSource.FindControl("rptPasajeros");
                        DataTable tblDatosTarifas = (DataTable)HttpContext.Current.Session["tbltarifas"];
                        if (rptCabina != null)
                        {
                            string sSesion = cCache.SessionID;
                            clsCacheControl cCacheControl = new clsCacheControl();
                            csGenerales cGeneral = new csGenerales();
                            csCarrito csCarCompUnion = new csCarrito("Reserva" + sSesion, "CarritoCompras");
                            //csPlanes csPlan = new csPlanes();
                            Utils.Utils csUtileria = new Utils.Utils();

                            for (int i = 0; i < rptCabina.Items.Count; i++)
                            {
                                Repeater rptTarifas = (Repeater)rptCabina.Items[i].FindControl("rptTarifas");

                                DropDownList ddlAdultos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlAdultos");
                                DropDownList ddlNinos = (DropDownList)rptPasajeros.Items[i].FindControl("ddlNinos");

                                if (rptTarifas != null)
                                {
                                    for (int x = 0; x < rptTarifas.Items.Count; x++)
                                    {
                                        if (((RadioButton)rptTarifas.Items[x].FindControl("rbTarifa")).Checked)
                                        {
                                            #region Insercion datos de plan
                                            int intCodigoTarifa = Convert.ToInt32(((Label)rptTarifas.Items[x].FindControl("lblIdTarifa")).Text);
                                            DataRow FilaSeleccionada = GetTablaTarifas(tblDatosTarifas, intCodigoTarifa, i + 1);
                                            FilaSeleccionada = setValidarValorTexto(FilaSeleccionada);

                                            int intNumeroPasajerosAdultos = Convert.ToInt32(ddlAdultos.SelectedValue);
                                            int intNumeroPasajerosNinos = Convert.ToInt32(ddlNinos.SelectedValue);
                                            int intTotalPasajeros = intNumeroPasajerosAdultos + intNumeroPasajerosNinos;
                                            csCarCompUnion.IntCodigoPlan = PageSource.Request.QueryString["id"];
                                            csCarCompUnion.StrIdentificadorDelPlan = PageSource.Request.QueryString["TipoPlan"];
                                            csCarCompUnion.IntCodigoTarifa = FilaSeleccionada["IdTarifa"].ToString();
                                            csCarCompUnion.IntcantidadPersonas = intTotalPasajeros.ToString();
                                            csCarCompUnion.StrCategoria = FilaSeleccionada["strCategoria"].ToString();
                                            csCarCompUnion.IntCategoria = FilaSeleccionada["IdCategoria"].ToString();
                                            //csCarCompUnion.StrCiudad = FilaSeleccionada["strCiudad"].ToString();
                                            //csCarCompUnion.IntCiudad = FilaSeleccionada["intCiudad"].ToString();
                                            csCarCompUnion.StrFechaInicial = Convert.ToDateTime(FilaSeleccionada["Desde"].ToString()).ToString(sFormatoFecha);
                                            csCarCompUnion.StrFechaFinal = Convert.ToDateTime(FilaSeleccionada["Hasta"].ToString()).ToString(sFormatoFecha);
                                            try
                                            {
                                                int iNochesPlanes = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("DiasVencimientoPlanes", "3"));
                                                csCarCompUnion.StrFechaVencimiento = DateTime.Now.AddDays(iNochesPlanes).ToString(sFormatoFecha);
                                            }
                                            catch (Exception Ex)
                                            {
                                                csCarCompUnion.StrFechaVencimiento = DateTime.Now.AddDays(3).ToString(sFormatoFecha);
                                            }
                                            //csCarCompUnion.StrNombrePlan = FilaSeleccionada["strNombre"].ToString();
                                            csCarCompUnion.IntNumeroNoches = FilaSeleccionada["intNoches"].ToString();
                                            csCarCompUnion.IntNumeroDias = FilaSeleccionada["intDias"].ToString();
                                            csCarCompUnion.IntNochesAdicionales = "0";
                                            csCarCompUnion.StrTemporada = FilaSeleccionada["strTemporada"].ToString();
                                            csCarCompUnion.IntTemporada = FilaSeleccionada["idTemporada"].ToString();
                                            csCarCompUnion.IntTipoHabitacion = FilaSeleccionada["TipoHabitacion"].ToString();
                                            csCarCompUnion.StrTipoHabitacion = FilaSeleccionada["strTipoHabitacion"].ToString();
                                            csCarCompUnion.StrTipoPropiedad = FilaSeleccionada["strSubTipoHab"].ToString();
                                            csCarCompUnion.StrTipoMoneda = FilaSeleccionada["strRefereMoneda"].ToString();
                                            csCarCompUnion.IntTipoMoneda = FilaSeleccionada["Moneda"].ToString();
                                            csCarCompUnion.IntTipoPlan = FilaSeleccionada["intTipoPlan"].ToString();
                                            //csCarCompUnion.StrTipoPlan = PageSource.Request.QueryString["intTipoPlan"];
                                            csCarCompUnion.StrTipoPlan = PageSource.Request.QueryString["TipoPlan"];
                                            if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                                                csCarCompUnion.IntValorTotal = Convert.ToInt32(Convert.ToDecimal(FilaSeleccionada["dblTotalImpuestos"])).ToString();
                                            else
                                                csCarCompUnion.IntValorTotal = Convert.ToDecimal(FilaSeleccionada["dblTotalImpuestos"]).ToString();

                                            //csCarCompUnion.IntZonaGeografica = FilaSeleccionada["intZonaG"].ToString();
                                            //csCarCompUnion.StrZonaGeografica = FilaSeleccionada["strZonaG"].ToString();
                                            DataTable tblTextosPlanes = (DataTable)PageSource.Session["$TablaTextosPlanes"];
                                            if (tblTextosPlanes != null)
                                            {
                                                try
                                                {
                                                    csCarCompUnion.StrNombrePlan = GetTextosPlan(clsValidaciones.GetKeyOrAdd("TipoTextoPlanNombre", "NAME"),
                                                        tblTextosPlanes);
                                                    //csCarCompUnion.StrRestricciones = FilaSeleccionada["strRestriccion"].ToString();
                                                    csCarCompUnion.StrDescripcion = GetTextosPlan(clsValidaciones.GetKeyOrAdd("TipoTextoPlanDesc", "DESC"),
                                                        tblTextosPlanes);
                                                    //csCarCompUnion.StrIncluye = FilaSeleccionada["strIncluye"].ToString();
                                                    //csCarCompUnion.StrNoIncluye = FilaSeleccionada["strNoIncluye"].ToString();
                                                }
                                                catch { }
                                            }
                                            csCarCompUnion.StrAcomodacion = FilaSeleccionada["strAcomodacion"].ToString();
                                            csCarCompUnion.IntAcomodacion = FilaSeleccionada["TipoAcomodacion"].ToString();
                                            csCarCompUnion.IntSegmento = FilaSeleccionada["intSegmento"].ToString();
                                            csCarCompUnion.StrCantidadPersonas = "Cantidad de personas: " + (intNumeroPasajerosAdultos.ToString());
                                            csCarCompUnion.StrDuracion = csCarCompUnion.IntNumeroDias + " Horas";
                                            csCarCompUnion.StrRegimen = FilaSeleccionada["strTipoAlim"].ToString();
                                            csCarCompUnion.IntVigencia = FilaSeleccionada["idVigencia"].ToString();
                                            csCarCompUnion.StrObservacion = "";
                                            csCarCompUnion.StrPasajeros = "Adultos: " + (intNumeroPasajerosAdultos.ToString()) + " / Niños: " + (intNumeroPasajerosNinos.ToString());
                                            csCarCompUnion.IntProveedor = FilaSeleccionada["intProveedorPlan"].ToString();

                                            csCarCompUnion.AddFields();

                                            //csPlanes cPlan = new csPlanes();
                                            //tblRefere tRefere = new tblRefere();
                                            //csGenerales cGen = new csGenerales();
                                            #endregion

                                            #region Insercion Tipos de pasajeros y pasajeros
                                            csConsultasGenerales UsuariosCons = new csConsultasGenerales();
                                            string sTipoPaxAdt = "0";

                                            DataTable dtTipoUser = null;
                                            dtTipoUser = UsuariosCons.ConReferenciaTiposPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT"));
                                            if (dtTipoUser != null)
                                                sTipoPaxAdt = dtTipoUser.Rows[0]["INTCODE"].ToString();

                                            csCarCompUnion.SaveTipoPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT"), Convert.ToInt32(sTipoPaxAdt), FilaSeleccionada["dblTotalImpuestos"].ToString(), FilaSeleccionada["dblTotalSinImpuestos"].ToString(), FilaSeleccionada["Impuesto"].ToString(),
                                               Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()));

                                            csCarCompUnion.SavePerson(null, null, "Adulto", Convert.ToInt32(sTipoPaxAdt), null, Convert.ToInt32(Convert.ToDecimal(FilaSeleccionada["dblTotalImpuestos"])), "",
                                                Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()), Convert.ToInt32(Convert.ToDecimal(FilaSeleccionada["dblTotalSinImpuestos"])));

                                            //if (intNumeroPasajerosAdultos > 0)
                                            //{
                                            //    csCarCompUnion.SaveTipoPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT"), Convert.ToInt32(cGeneral.ConsultarIdRefere(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT"),
                                            //        clsValidaciones.GetKeyOrAdd("TipoPasajero", "Tip_Pasajero"))), FilaSeleccionada["dblPrecio"].ToString(), FilaSeleccionada["dblTarifa"].ToString(), FilaSeleccionada["dblImpuesto"].ToString(),
                                            //        Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()));
                                            //}
                                            //if (intNumeroPasajerosNinos > 0)
                                            //{
                                            //    csCarCompUnion.SaveTipoPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN"), Convert.ToInt32(cGeneral.ConsultarIdRefere(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN"),
                                            //        clsValidaciones.GetKeyOrAdd("TipoPasajero", "Tip_Pasajero"))), FilaSeleccionada["dblPrecioNinoImp"].ToString(), FilaSeleccionada["dblPrecioNino"].ToString(),
                                            //        FilaSeleccionada["dblImpuestosNino"].ToString(), Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()));
                                            //}

                                            //for (int a = 0; a < intNumeroPasajerosAdultos; a++)
                                            //{
                                            //    csCarCompUnion.SavePerson(null, null, "Adulto", Convert.ToInt32(cGeneral.ConsultarIdRefere(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT"),
                                            //        clsValidaciones.GetKeyOrAdd("TipoPasajero", "Tip_Pasajero"))), null, Convert.ToInt32(Convert.ToDecimal(FilaSeleccionada["dblPrecio"])), "",
                                            //        Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()), Convert.ToInt32(Convert.ToDecimal(FilaSeleccionada["dblTarifa"])));
                                            //}

                                            //for (int a = 0; a < intNumeroPasajerosNinos; a++)
                                            //{
                                            //    csCarCompUnion.SavePerson(null, null, "Niño", Convert.ToInt32(cGeneral.ConsultarIdRefere(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN"),
                                            //         clsValidaciones.GetKeyOrAdd("TipoPasajero", "Tip_Pasajero"))), null, Convert.ToInt32(Convert.ToDecimal(FilaSeleccionada["dblPrecioNinoImp"])), "",
                                            //         Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()), Convert.ToInt32(Convert.ToDecimal(FilaSeleccionada["dblPrecioNino"])));
                                            //}
                                            #endregion
                                        }
                                    }
                                }
                            }
                            csCarCompUnion.Save();
                            //clsSesiones.setPantalleRespuestaLogin("../Presentacion/ReservaTour.aspx?Codigo=" + PageSource.Request.QueryString["id"] + "&TipoPlan=" + PageSource.Request.QueryString["TipoPlan"]);
                            //clsValidaciones.RedirectPagina("../Presentacion/Login.aspx");
                            clsValidaciones.RedirectPagina("../Presentacion/ReservaTour.aspx?Codigo=" + PageSource.Request.QueryString["id"] + "&TipoPlan=" + PageSource.Request.QueryString["TipoPlan"] + "&ControlCupos=" + PageSource.Request.QueryString["ControlCupos"]);
                        }
                    }
                    else
                    {
                        csGeneralsPag.FinSesion();
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "EnviarACarritoDeComprasCruceros";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        private void EnviarACarritoDeComprasSeguros(UserControl PageSource, object sender)
        {

            RepeaterItem rptPlanesItem = ((RepeaterItem)((Button)sender).Parent);

            Label strDescripcion = (Label)rptPlanesItem.FindControl("strDescripcion");
            Label strIncluye = (Label)rptPlanesItem.FindControl("strIncluye");
            Label strNoIncluye = (Label)rptPlanesItem.FindControl("strNoIncluye");
            Label strRestriccion = (Label)rptPlanesItem.FindControl("strRestriccion");


            clsParametros cParametros = new clsParametros();
            try
            {

                if (PageSource.Request.QueryString["TipoPlan"] != "0" && PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["TipoPlan"] == "TJAS")
                {
                    clsCache cCache = new csCache().cCache();

                    if (cCache != null)
                    {
                        DataTable tblDatosTarifas = (DataTable)HttpContext.Current.Session["tbltarifas"];

                        string sSesion = cCache.SessionID;
                        string sCodPlan = "0";

                        clsCacheControl cCacheControl = new clsCacheControl();
                        csGenerales cGeneral = new csGenerales();
                        cGeneral.Conexion = clsSesiones.getConexion();
                        csCarrito csCarCompUnion = new csCarrito("Reserva" + sSesion, "CarritoCompras");
                        csTarifasPlanes csPlan = new csTarifasPlanes();
                        Utils.Utils csUtileria = new Utils.Utils();


                        Repeater rptSeguros = (Repeater)PageSource.FindControl("dtlOfertas");

                        if (rptPlanesItem != null)
                        {

                            Label lblNombrePlan = (Label)rptPlanesItem.FindControl("lblNombrePlan");
                            Label lblIdTarifa = (Label)rptPlanesItem.FindControl("lblIdTarifa");
                            int intCodigoTarifa = Convert.ToInt32(lblIdTarifa.Text);
                            DataRow FilaSeleccionada = GetTablaTarifas(tblDatosTarifas, intCodigoTarifa, 1);
                            FilaSeleccionada = setValidarValorTexto(FilaSeleccionada);

                            DataTable tblTarifasCompletas = (DataTable)FilaSeleccionada["tTarifasCompletoRegistro"];

                            int iDuracion = clsValidaciones.getDiasDiferencia(Convert.ToDateTime(clsValidaciones.ConverMDYtoYMD(cCache.DatosAdicionales[1], "/")).ToString("yyyy/MM/dd"), Convert.ToDateTime(clsValidaciones.ConverMDYtoYMD(cCache.DatosAdicionales[2], "/")).ToString("yyyy/MM/dd"));
                            int iNochesAdic = iDuracion - Convert.ToInt32(FilaSeleccionada["NumeroNoches"].ToString());

                            int intNumeroPasajerosAdultos = Convert.ToInt32(cCache.DatosAdicionales[3]);
                            int intTotalPasajeros = intNumeroPasajerosAdultos;
                            csCarCompUnion.IntCodigoPlan = FilaSeleccionada["intCodigo"].ToString();
                            sCodPlan = FilaSeleccionada["intCodigo"].ToString();
                            csCarCompUnion.StrIdentificadorDelPlan = sCodPlan;
                            csCarCompUnion.IntCodigoTarifa = FilaSeleccionada["IdTarifa"].ToString();
                            csCarCompUnion.IntcantidadPersonas = intTotalPasajeros.ToString();
                            csCarCompUnion.StrCategoria = FilaSeleccionada["strCategoria"].ToString();
                            csCarCompUnion.IntCategoria = FilaSeleccionada["IdCategoria"].ToString();
                            csCarCompUnion.StrCiudad = "";
                            csCarCompUnion.IntCiudad = "1";
                            csCarCompUnion.StrFechaInicial = cCache.DatosAdicionales[1];
                            csCarCompUnion.StrFechaFinal = cCache.DatosAdicionales[2];
                            csCarCompUnion.StrFechaVencimiento = DateTime.Now.AddDays(3).ToString(sFormatoFecha);
                            csCarCompUnion.StrNombrePlan = lblNombrePlan.Text;
                            csCarCompUnion.IntNumeroNoches = iDuracion.ToString();
                            csCarCompUnion.IntNumeroDias = iDuracion.ToString();
                            csCarCompUnion.IntNochesAdicionales = iNochesAdic.ToString();
                            csCarCompUnion.StrTemporada = FilaSeleccionada["strTemporada"].ToString();
                            csCarCompUnion.IntTemporada = FilaSeleccionada["idTemporada"].ToString();
                            csCarCompUnion.IntTipoHabitacion = FilaSeleccionada["TipoHabitacion"].ToString();
                            csCarCompUnion.StrTipoHabitacion = FilaSeleccionada["strTipoHabitacion"].ToString();
                            csCarCompUnion.StrTipoPropiedad = FilaSeleccionada["strSubTipoHab"].ToString();
                            csCarCompUnion.StrTipoMoneda = FilaSeleccionada["strRefereMoneda"].ToString();
                            csCarCompUnion.IntTipoMoneda = FilaSeleccionada["Moneda"].ToString();

                            string strTipoServicio = clsValidaciones.GetKeyOrAdd("TipoServicioPlan", "PLN");
                            string strCodigoTipoServ = new CsConsultasVuelos().ConsultaCodigo(strTipoServicio, "TBLTPOSERVICIO",
                                "INTID", "STRCODIGO");

                            if (strCodigoTipoServ == null || strCodigoTipoServ == "")
                                strCodigoTipoServ = "2";

                            csCarCompUnion.IntTipoPlan = strCodigoTipoServ;
                            csCarCompUnion.StrTipoPlan = PageSource.Request.QueryString["TipoPlan"];
                            if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                                csCarCompUnion.IntValorTotal = Convert.ToInt32(Convert.ToDecimal(FilaSeleccionada["dblTotalImpuestos"])).ToString();
                            else
                                csCarCompUnion.IntValorTotal = Convert.ToDecimal(FilaSeleccionada["dblTotalImpuestos"]).ToString();

                            csCarCompUnion.IntZonaGeografica = cCache.DatosAdicionales[0];

                            if (PageSource.Request.QueryString["ZnaTex"] != null)
                            {
                                csCarCompUnion.StrZonaGeografica = PageSource.Request.QueryString["ZnaTex"];
                            }
                            else
                            {
                                csCarCompUnion.StrZonaGeografica = "";
                            }

                            if (strRestriccion != null)
                            {
                                csCarCompUnion.StrRestricciones = strRestriccion.Text;
                            }
                            else
                            {
                                csCarCompUnion.StrRestricciones = "";
                            }

                            if (strDescripcion != null)
                            {
                                csCarCompUnion.StrDescripcion = strDescripcion.Text;
                            }
                            else
                            {
                                csCarCompUnion.StrDescripcion = "";
                            }

                            if (strIncluye != null)
                            {
                                csCarCompUnion.StrIncluye = strIncluye.Text;
                            }
                            else
                            {
                                csCarCompUnion.StrIncluye = "";
                            }

                            if (strNoIncluye != null)
                            {
                                csCarCompUnion.StrNoIncluye = strNoIncluye.Text;
                            }
                            else
                            {
                                csCarCompUnion.StrNoIncluye = "";
                            }

                            csCarCompUnion.StrAcomodacion = FilaSeleccionada["strAcomodacion"].ToString();
                            csCarCompUnion.IntAcomodacion = FilaSeleccionada["TipoAcomodacion"].ToString();
                            csCarCompUnion.IntSegmento = FilaSeleccionada["intSegmento"].ToString();
                            csCarCompUnion.StrCantidadPersonas = "Adultos: " + (intNumeroPasajerosAdultos.ToString());
                            csCarCompUnion.StrDuracion = iDuracion.ToString() + " Días";
                            csCarCompUnion.StrRegimen = FilaSeleccionada["strTipoAlim"].ToString();
                            csCarCompUnion.IntVigencia = FilaSeleccionada["idVigencia"].ToString();
                            csCarCompUnion.StrObservacion = "";
                            csCarCompUnion.StrPasajeros = "Adultos: " + (intNumeroPasajerosAdultos.ToString());

                            csCarCompUnion.AddFields();

                            if (intNumeroPasajerosAdultos > 0)
                            {
                                csCarCompUnion.SaveTipoPax(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT"), Convert.ToInt32(1), FilaSeleccionada["Precio"].ToString(), FilaSeleccionada["Tarifa"].ToString(), FilaSeleccionada["Impuesto"].ToString(),
                                    Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()));
                            }

                            //se hace el recorrido de los tipos de pasajero
                            for (int a = 0; a < intNumeroPasajerosAdultos; a++)
                            {
                                DataView dvFiltroEdad = new DataView(tblTarifasCompletas);
                                dvFiltroEdad.RowFilter = "strRefereTipoPasajero = '" + clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT") + "'";
                                DataTable dtResFiltro = dvFiltroEdad.ToTable();
                                if (dtResFiltro.Rows.Count > 0)
                                {
                                    csCarCompUnion.SavePerson(null, null, "Adulto", Convert.ToInt32(1), null, Convert.ToDecimal(dtResFiltro.Rows[0]["Precio"]), "",
                                        Convert.ToInt32(FilaSeleccionada["intSegmento"].ToString()), Convert.ToDecimal(dtResFiltro.Rows[0]["Tarifa"]));
                                }
                            }
                        }

                        csCarCompUnion.Save();
                        clsSesiones.setPantalleRespuestaLogin("../Presentacion/ReservaSeguro.aspx?Codigo=" + sCodPlan + "&TipoPlan=" + PageSource.Request.QueryString["TipoPlan"] + "&ControlCupos=Normal"/* + sDispAereo*/);
                        clsValidaciones.RedirectPagina("../Presentacion/ReservaSeguro.aspx?Codigo=" + sCodPlan + "&TipoPlan=" + PageSource.Request.QueryString["TipoPlan"] + "&ControlCupos=Normal");

                    }
                    else
                    {
                        csGeneralsPag.FinSesion();
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "EnviarACarritoDeComprasSeguros";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// metodo que valida el registro en la tabla de la tarifa seleccionada
        /// </summary>
        /// <param name="tbldaDatosTarifas"></param>
        /// <param name="intCodigoTarifa"></param>
        /// <param name="iSegmento"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-01
        /// -------------------
        /// Control de Cambios
        /// -------------------  
        /// </remarks>
        private DataRow GetTablaTarifas(DataTable tbldaDatosTarifas, int intCodigoTarifa, int iSegmento)
        {
            DataRow FilaSeleccionada = tbldaDatosTarifas.NewRow();
            for (int i = 0; i < tbldaDatosTarifas.Rows.Count; i++)
            {
                String strCodigoTarifa = tbldaDatosTarifas.Rows[i]["IdTarifa"].ToString();
                int iSegmentoTarifa = Convert.ToInt32(tbldaDatosTarifas.Rows[i]["intSegmento"].ToString());
                if (strCodigoTarifa.Equals(intCodigoTarifa.ToString()) && iSegmentoTarifa == iSegmento)
                {
                    FilaSeleccionada = tbldaDatosTarifas.Rows[i];
                    try
                    {
                        tbldaDatosTarifas.Rows[i]["intSelect"] = 1;
                        tbldaDatosTarifas.AcceptChanges();
                        HttpContext.Current.Session["tbltarifas"] = tbldaDatosTarifas;
                    }
                    catch { }
                    break;
                }
            }
            return FilaSeleccionada;
        }

        /// <summary>
        /// metodo que consulta las ciudades del plan
        /// </summary>
        /// <param name="tbldaDatosTarifas"></param>
        /// <param name="intCodigoTarifa"></param>
        /// <param name="iSegmento"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-04-14
        /// -------------------
        /// Control de Cambios
        /// -------------------  
        /// </remarks>
        private string GetCiudadesPlan(string sCodigoPlan)
        {
            string sCiudadesPlan = "";
            try
            {
                csConsultasPlanes cPlanes = new csConsultasPlanes();
                DataTable tblCiudades = cPlanes.ConMultiCiudadesPlan(sCodigoPlan);
                if (tblCiudades != null)
                {
                    for (int i = 0; i < tblCiudades.Rows.Count; i++)
                    {
                        if (i < tblCiudades.Rows.Count - 1)
                            sCiudadesPlan = sCiudadesPlan + tblCiudades.Rows[i]["strDescripcion"].ToString() + ", ";
                        else
                            sCiudadesPlan = sCiudadesPlan + tblCiudades.Rows[i]["strDescripcion"].ToString();
                    }
                }
            }
            catch { }
            return sCiudadesPlan;
        }

        /// <summary>
        /// Metodo para validar el valor del tipo de pasajero y si es texto devuelve 0
        /// </summary>
        /// <param name="drSeleccionada"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-01
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        private DataRow setValidarValorTexto(DataRow drSeleccionada)
        {
            try
            {
                if (drSeleccionada["dblPrecioAdt3"].ToString().Equals("No aplica"))
                    drSeleccionada["dblPrecioAdt3"] = "0";

                if (drSeleccionada["dblPrecioAdt4"].ToString().Equals("No aplica"))
                    drSeleccionada["dblPrecioAdt4"] = "0";

                if (drSeleccionada["dblPrecioNino"].ToString().Equals("No aplica"))
                    drSeleccionada["dblPrecioNino"] = "0";

                if (drSeleccionada["dblPrecioInfante"].ToString().Equals("No aplica"))
                    drSeleccionada["dblPrecioInfante"] = "0";
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setValidarValorTexto";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
            return drSeleccionada;
        }

        /// <summary>
        /// Metodo que filtra los textos de planes dependiendo del codigo proporcionado
        /// </summary>
        /// <param name="sCodigoTexto"></param>
        /// <param name="tblTextosCompletos"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-05
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        public string GetTextosPlan(string sCodigoTexto, DataTable tblTextosCompletos)
        {
            string strTexto = string.Empty;
            string sTextoAdicional = string.Empty;
            try
            {
                //filtramos la tabla de textos con el tipo correspondiente
                DataView dvFiltro = new DataView(tblTextosCompletos);
                dvFiltro.RowFilter = "strCodTexto = '" + sCodigoTexto + "'";
                DataTable tblResul = dvFiltro.ToTable();

                if (tblResul != null && tblResul.Rows.Count > 0)
                {
                    //el primer registro de texto sera el general de mostrar
                    strTexto = tblResul.Rows[0]["strTexto"].ToString(); ;

                    //los demas registros seran el ver mas                   
                    for (int i = 1; i < tblResul.Rows.Count; i++)
                    {
                        sTextoAdicional += "<br/>" + tblResul.Rows[i]["strTexto"].ToString();
                    }
                }
            }
            catch { }
            return strTexto + sTextoAdicional;
        }

        /// <summary>
        /// Metodo que llena los datos de las fechas de las salidas fijas
        /// </summary>
        /// <param name="sCodigoTexto"></param>
        /// <param name="tblTextosCompletos"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-03-19
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        public void setLlenarParametrosSalidas(UserControl PageSource, object sender)
        {
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != null && PageSource.Request.QueryString["id"] != null)
                {
                    DropDownList ddlAnioSalida = (DropDownList)PageSource.FindControl("ddlAnioSalida");
                    DropDownList ddlMesSalida = (DropDownList)PageSource.FindControl("ddlMesSalida");
                    DropDownList ddlDiaSalida = (DropDownList)PageSource.FindControl("ddlDiaSalida");

                    csConsultasPlanes cPlan = new csConsultasPlanes();
                    csGenerales cGen = new csGenerales();

                    if (PageSource.Request.QueryString["ControlCupos"] != null)
                    {
                        string sControlCupos = clsValidaciones.GetKeyOrAdd("ControlCuposCon", "ConControl");
                        if (((DropDownList)sender).ID.ToUpper().Contains("ANIO"))
                        {
                            DataTable tMeses = new DataTable();
                            if (PageSource.Request.QueryString["ControlCupos"].Equals(sControlCupos))
                                tMeses = null;/*cPlan.ConsultaMesesSalidas(PageSource.Request.QueryString["id"], 
                                    DateTime.Now.ToString(sFormatoFechaBD), ((DropDownList)sender).SelectedValue);*/
                            else
                                tMeses = cPlan.ConsultaMesesDiasSalidas(PageSource.Request.QueryString["id"],
                                    ((DropDownList)sender).SelectedValue, "0");

                            if (tMeses != null)
                            {
                                tMeses = AgregarMesTexto(tMeses);
                                DataSet dsMeses = new DataSet();
                                dsMeses.Tables.Add(tMeses.Copy());
                                cGen.LlenarControlData(ddlMesSalida, Enum_Controls.DropDownList, "NumMes", "Mes", true, false, null, dsMeses);
                            }
                        }
                        else
                        {
                            DataTable tDias = new DataTable();
                            if (PageSource.Request.QueryString["ControlCupos"].Equals(sControlCupos))
                                tDias = null;/*cPlan.ConsultaDiasSalidas(PageSource.Request.QueryString["id"], 
                                    DateTime.Now.ToString(sFormatoFechaBD), ddlAnioSalida.SelectedValue, ((DropDownList)sender).SelectedValue);*/
                            else
                                tDias = cPlan.ConsultaMesesDiasSalidas(PageSource.Request.QueryString["id"],
                                    ddlAnioSalida.SelectedValue, ((DropDownList)sender).SelectedValue);

                            if (tDias != null)
                            {
                                DataSet dsDias = new DataSet();
                                dsDias.Tables.Add(tDias.Copy());
                                cGen.LlenarControlData(ddlDiaSalida, Enum_Controls.DropDownList, "Dia", "Dia", true, false, null, dsDias);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setLlenarParametrosSalidas";
                cParametros.Complemento = "Circular";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que llena una columna de una tabla con el texto de un mes
        /// </summary>
        /// <param name="sCodigoTexto"></param>
        /// <param name="tblTextosCompletos"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-03-19
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        private DataTable AgregarMesTexto(DataTable tblNumMes)
        {
            Utils.Utils Utilidades = new Utils.Utils();
            if (tblNumMes != null)
            {
                tblNumMes.Columns.Add("Mes");
                for (int i = 0; i < tblNumMes.Rows.Count; i++)
                {
                    tblNumMes.Rows[i]["mes"] = clsValidaciones.RetornaMesLetrasLargo(tblNumMes.Rows[i]["NumMes"].ToString());
                }
            }
            return tblNumMes;
        }

        public void setDetallesHotelesMulti(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();

                if (cCache != null)
                {
                    Repeater rptHoteles = (Repeater)PageSource.FindControl("rptHoteles");
                    if (PageSource.Request.QueryString["intIdCat"] != "0" && PageSource.Request.QueryString["id"] != "0")
                    {
                        csConsultasPlanes cPlan = new csConsultasPlanes();
                        csConsultasOperadores cOper = new csConsultasOperadores();
                        DataTable tHotelesCat = cPlan.CosultarHoteles(PageSource.Request.QueryString["intIdCat"]);

                        if (tHotelesCat != null)
                        {
                            DataTable dtHotelTodos = new DataTable();
                            for (int i = 0; i < tHotelesCat.Rows.Count; i++)
                            {
                                DataTable dtHotel = new DataTable();
                                dtHotel = cOper.Cosulta_detalle_operador(tHotelesCat.Rows[i]["Codigo"].ToString());
                                if (dtHotel != null)
                                {
                                    if (i == 0)
                                        dtHotelTodos = dtHotel.Copy();
                                    else
                                        dtHotelTodos.Merge(dtHotel);
                                }
                            }

                            Utils.Utils cCombo = new Utils.Utils();
                            cCombo.ModificarTablaRPTImagenes(dtHotelTodos, PageSource, true);
                            rptHoteles.DataSource = dtHotelTodos;
                            rptHoteles.DataBind();
                        }
                    }
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "setDetallesHotel";
                ExceptionHandled.Publicar(cParametros);
            }
        }
    }
}
