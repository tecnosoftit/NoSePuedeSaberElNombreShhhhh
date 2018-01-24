using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Ssoft.Utils;
using System.Web.UI.WebControls;
using Ssoft.ManejadorExcepciones;
using Ssoft.Rules.Hoteles;
using Ssoft.Ssoft.ValueObjects.Hoteles;
using WS_SsoftHotelBeds.Hoteles;
using System.Data;
using Ssoft.ValueObjects;
using System.Web;
using SsoftQuery.Vuelos;
using Ssoft.Rules.Generales;
using Ssoft.Pages.Reserva;
using Ssoft.Rules.Reservas;
using Ssoft.Rules.Hoteles;
using Ssoft.Pages;
using Ssoft.Pages.PaginaContactenos;

namespace Ssoft.Pages.PaginaHoteles
{
    public class csBusquedaHoteles
    {
        // Tablas 
        private const string TABLA_HOTELS = "hotels";
        private const string TABLA_PLANS = "plans";
        private const string TABLA_ROOMS = "rooms";
        private const string TABLA_PLANSERVICES = "planservices";
        private const string TABLA_ALLOTMEN = "allotment";
        private const string TABLA_PAYMENTSFEATURES = "PaymentsFeatures";
        private const string TABLA_GENERALITIES = "HotelGenaralities";

        private const string TABLA_HOTEL_INFO = "HotelInfo";
        private const string TABLA_HOTEL_ROOMS = "HotelRoom";

        // Relaciones
        private const string RELACION_HOTELS_PLANS = "FK_HotelsPlans";
        private const string RELACION_PLANS_ROOMS = "FK_PlansRooms";
        private const string RELACION_PLANS_PLANSERVICES = "FK_PlansPlanservices";
        private const string RELACION_ROOMS_ALLOTMEN = "FK_RoomsAllotment";

        // Columnas Id
        private const string COLUMN_HOTEL_ID = "id_hotel";
        private const string COLUMN_PLAN_ID = "id_plan";
        private const string COLUMN_ROOM_ID = "id_room";
        private const string COLUMN_URL = "URL";
        private const string COLUMN_DETALLES_URL = "DetallesURL";

        // Hoteles
        private const string COLUMN_HOTEL = "ds_hotel";
        private const string COLUMN_DISTANCE = "am_distance";
        private const string COLUMN_HOTEL_FEATURE1 = "ds_hotel_feature1";
        private const string COLUMN_HOTEL_FEATURE2 = "ds_hotel_feature2";
        private const string COLUMN_HOTEL_FEATURE3 = "ds_hotel_feature3";
        private const string COLUMN_HOTEL_FEATURE4 = "ds_hotel_feature4";
        private const string COLUMN_HOTEL_FEATURE5 = "ds_hotel_feature5";
        private const string COLUMN_CD_CATEGORY = "cd_category";
        private const string COLUMN_DS_CATEGORY = "ds_category";
        private const string COLUMN_VIRTUAL_TOUR = "in_virtual_tour";
        private const string COLUMN_IMAGES_GALLERY = "in_images_gallery";

        // Planes
        private const string COLUMN_PROMOTION = "in_promotion";
        private const string COLUMN_DS_PLAN = "ds_plan";
        private const string COLUMN_CHILD_AGE = "am_child_age";
        private const string COLUMN_INCLUYE = "ds_included";
        private const string COLUMN_NO_INCLUYE = "ds_not_included";

        // Planservices
        private const string COLUMN_ID = "id";
        private const string COLUMN_DS = "ds";
        private const string COLUMN_IMAGE_PATH = "image_path";
        private const string COLUMN_IMAGE_WIDTH = "image_width";
        private const string COLUMN_IMAGE_HEIGHT = "image_height";

        // Rooms
        private const string COLUMN_ROOM_TYPE = "ds_roomtype";
        private const string COLUMN_ROOM_CLASS = "ds_roomclass";
        private const string COLUMN_ADULTS_TOTAL = "am_adults_total";
        private const string COLUMN_CHILDREN_TOTAL = "am_children_total";
        private const string COLUMN_FREE_CHILDREN = "am_free_children";
        private const string COLUMN_PRICE_ROOM = "am_price_room";
        private const string COLUMN_PRICE_ADDITIONAL = "am_price_additional";
        private const string COLUMN_PRICE_CHILD = "am_price_child";
        private const string COLUMN_PRICE_AVERAGE = "am_price_average";
        private const string COLUMN_PRICE_TOTAL = "am_price_total";

        // Tasas
        private const string COLUMN_PRICE_ROOM_TAX = "am_tax_room";
        private const string COLUMN_PRICE_ADDITIONAL_TAX = "am_tax_additional";
        private const string COLUMN_PRICE_CHILD_TAX = "am_tax_child";
        private const string COLUMN_PRICE_TOTAL_TAX = "am_tax_total";

        // Tarifas sin Iva
        private const string COLUMN_PRICE_ROOM_TARIFA = "am_tarifa_room";
        private const string COLUMN_PRICE_ADDITIONAL_TARIFA = "am_tarifa_additional";
        private const string COLUMN_PRICE_CHILD_TARIFA = "am_tarifa_child";
        private const string COLUMN_PRICE_TOTAL_TARIFA = "am_tarifa_total";

        private const string COLUMN_BOOKING_DAYS = "min_booking_days";
        private const string COLUMN_IN_PRICE_ROOM = "in_price_room";
        private const string COLUMN_IN_PRICE_ADDITIONAL = "in_price_additional";
        private const string COLUMN_IN_PRICE_CHILD = "in_price_child";
        private const string COLUMN_IN_OFFER = "in_offer";
        private const string COLUMN_AM_DAY_ROOM = "am_day_room";
        private const string COLUMN_DT_DAY = "dt_day";
        private const string COLUMN_ID_DAY = "id_day";
        private const string COLUMN_ID_MONTH = "id_month";
        private const string COLUMN_ID_YEAR = "id_year";
        private const string COLUMN_SUM_AM_DAY = "sum_am_day";
        private const string COLUMN_LEFT_AM_DAY = "left_am_day";
        private const string COLUMN_OTHER_ROOMS = "other_rooms";
        private const string COLUMN_IN_INTER_PLAN = "in_inter_plan";

        // allotment
        private const string COLUMN_DW = "dw";
        private const string COLUMN_DD = "dd";
        private const string COLUMN_MM = "mm";
        private const string COLUMN_YY = "yy";
        private const string COLUMN_PRICE_NIGHT = "am_price_night";
        private const string COLUMN_ROOMS_LEFT = "am_rooms_left";
        /*Seleccion*/
        private const string COLUMN_HOTEL_ROOM_OCUPATION = "Id";
        private const string COLUMN_HOTEL_ROOM_ID = "HotelRoom_Id";
        private const string COLUMN_SHRUI = "SHRUI";
        private const string COLUMN_ROOM_TYPE_TEXT = "RoomType_Text";
        private const string COLUMN_CODE = "code";
        private const string COLUMN_CHARACTERISTIC = "characteristic";
        private const string COLUMN_SERVICEHOTEL_ID = "ServiceHotel_Id";
        private const string COLUMN_SELECCION = "Seleccion";
        public const string COLUMN_NON_REEMBOLSABLE = "Reembolsable"; // Si es 0, es reembolsable, si es 1 es no reembolsable

        private static string sFormatoFecha = clsSesiones.getFormatoFecha();
        private static string sFormatoFechaBD = clsSesiones.getFormatoFechaBD();
        private const string sFormatoNumeros = "#,##0";
        // varios
        private const string COLUMN_CODIGO_EXTERNO = "strCodigoExterno";
        private const string PAGE_CIRCULAR = "Circular";
        private const string COLUMN_WS_SELECT = "WsSelect";
        //
        #region[SeccionZeus]
        private const string DDL_ROUND_EDAD = "ddlEdadNinio";
        private const string DDL_ROUND_MESES = "ddlMeses";

        private const string TABLA_PLAN = "plans";
        private const string COLUMNA_COD_PLAN_ZEUS = "idp";
        private const string TABLA_PLANES = "plans";
        private const string TABLA_TARIFAS = "rooms";
        private const string TABLA_PASAJEROS = "dtPasajeros";
        private const string TABLA_ERROR = "ErrorSsoft";
        private const string COLUMN_CODEZEUS = "Code";
        private const string COLUMN_MESSAGE = "Message";
        private const string COLUMNA_NOMBRE_PLAN = "strNombrePlan";
        private const string COLUMNA_DESCRIPCION = "strDescripcion";
        private const string COLUMNA_RESTRICCION = "strRestriccion";
        private const string COLUMNA_INCLUYE = "strIncluye";
        private const string COLUMNA_NOINCLUYE = "strNoIncluye";
        private const string COLUMNA_COD_PLAN = "intCodigo";
        private const string COLUMNA_IMAGEN = "strImagen";
        private const string COLUMNA_LINK = "strLink";
        private const string COLUMNA_SELECCION = "strSeleccion";
        private const string COLUMNA_CODIGO_HABITACION = "idr";
        private const string COLUMNA_CODIGO_PLAN_LOCAL = "strCodPlan";
        private const string COLUMNA_CANTIDAD = "strCantidad";
        private const string COLUMNA_TIPO_PASAJERO = "strTipoPasajero";
        #endregion

        public void setBuscarHotel(UserControl PageSource)
        {
            try
            {
                csGeneralsPag.Idioma(PageSource);

                clsSesiones.CLEAR_SESSION_RESULTADO_HOTEL();
                if (ValidarHotel(PageSource))
                {
                    RadioButtonList modal_hotel = (RadioButtonList)PageSource.FindControl("modal_hotel");
                    PageSource.Session["TipoBusqueda"] = "Hoteles";
                    setActualizaAirport(PageSource);

                    string sBusqueda = string.Empty;
                    if (modal_hotel != null)
                    {
                        sBusqueda = modal_hotel.SelectedValue;
                        if (!sBusqueda.Equals("0"))
                            sBusqueda = clsValidaciones.GetKeyOrAdd("TipoBusquedaHotel", "1");
                    }
                    else
                        sBusqueda = clsValidaciones.GetKeyOrAdd("TipoBusquedaHotel", "0");

                    clsCache cCache = new csCache().cCache();
                    string sSesion = string.Empty;
                    clsParametros cParametros = new clsParametros();
                    if (cCache != null)
                    {
                        sSesion = cCache.SessionID;
                        Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");
                        lblErrorGen.Text = string.Empty;
                        csHoteles cOcupacion = new csHoteles();
                        switch (sBusqueda)
                        {
                            case "0":
                                cCache.TipoWebServices = Enum_WebServices.CotelcoHotel;
                                cParametros = cOcupacion.CargarBusqueda(PageSource, cCache.TipoWebServices);
                                break;
                            case "1":
                                cCache.TipoWebServices = Enum_WebServices.HotelBedsHotel;
                                cParametros = cOcupacion.CargarBusqueda(PageSource, cCache.TipoWebServices);
                                break;
                            case "4":
                                cCache.TipoWebServices = Enum_WebServices.SabreHotel;
                                cParametros = cOcupacion.CargarBusqueda(PageSource, cCache.TipoWebServices);
                                break;
                            case "5":
                                cCache.TipoWebServices = Enum_WebServices.HotelInterNal;
                                cParametros = cOcupacion.CargarBusqueda(PageSource, cCache.TipoWebServices);
                                break;
                            case "6":
                                cCache.TipoWebServices = Enum_WebServices.TouricoHotel;
                                cParametros = cOcupacion.CargarBusqueda(PageSource, cCache.TipoWebServices);
                                break;
                        }
                        if (cCache != null)
                        {
                            csCache.ActualizarCache(cCache);
                        }
                        if (ValidarHotel(PageSource))
                        {
                            if (cParametros.Id.Equals(0))
                            {
                                if (cParametros.ViewMessage.Count > 0)
                                    lblErrorGen.Text = cParametros.ViewMessage[0].ToString();
                            }
                            else
                            {
                                setProcesarBusquedaHotel(PageSource);
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
                cParametros.Complemento = "Buscador hoteles ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                cParametros.Metodo = "setBuscarHotel";
                ExceptionHandled.Publicar(cParametros);

            }
        }
        private bool ValidarHotel(UserControl PageSource)
        {
            bool Resp = ValidarCondiciones(PageSource);
            if (Resp)
            {
                try
                {
                    TextBox txtCiudadDestino = (TextBox)PageSource.FindControl("txtCiudadDestino");
                    DropDownList ddlCiudades = (DropDownList)PageSource.FindControl("ddlCiudades");
                    TextBox txtFechaIngreso = (TextBox)PageSource.FindControl("txtFechaIngreso");
                    RadioButtonList modal_hotel = (RadioButtonList)PageSource.FindControl("modal_hotel");

                    /*LABELS DE TITULOS*/
                    Label lblCiudadH = new Label();
                    Label lblTFechaIngresoH = new Label();
                    /*ASIGNACION DE VALORES*/
                    lblCiudadH.Text = "Ciudad";
                    lblTFechaIngresoH.Text = "Fecha de Ingreso";

                    Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");
                    bool bModalHotel = false;
                    bool bBusquedaNal = false;

                    if (modal_hotel != null)
                    {
                        bModalHotel = true;
                        if (modal_hotel.SelectedValue.ToString().Equals("0"))
                            bBusquedaNal = true;
                    }

                    /*CAJAS DE TEXTO*/

                    lblErrorGen.Text = String.Empty;
                    if (bModalHotel)
                    {
                        if (bBusquedaNal)
                        {
                            if (ddlCiudades.SelectedValue.ToString().Equals("0"))
                            {
                                Resp = false;
                                if (lblErrorGen.Text.Length.Equals(0))
                                {
                                    lblErrorGen.Text = "Complete: " + lblCiudadH.Text;
                                }
                                else
                                {
                                    lblErrorGen.Text += ", " + lblCiudadH.Text;
                                }

                            }
                        }
                        else
                        {
                            if (txtCiudadDestino.Text.Length.Equals(0))
                            {
                                Resp = false;
                                if (lblErrorGen.Text.Length.Equals(0))
                                {
                                    lblErrorGen.Text = "Complete: " + lblCiudadH.Text;
                                }
                                else
                                {
                                    lblErrorGen.Text += ", " + lblCiudadH.Text;
                                }

                            }
                        }
                    }
                    else
                    {
                        if (txtCiudadDestino != null)
                        {
                            if (txtCiudadDestino.Text.Length.Equals(0))
                            {
                                Resp = false;
                                if (lblErrorGen.Text.Length.Equals(0))
                                {
                                    lblErrorGen.Text = "Complete: " + lblCiudadH.Text;
                                }
                                else
                                {
                                    lblErrorGen.Text += ", " + lblCiudadH.Text;
                                }

                            }
                        }
                        else
                        {
                            if (ddlCiudades != null)
                            {
                                if (ddlCiudades.SelectedValue.ToString().Length.Equals("0"))
                                {
                                    Resp = false;
                                    if (lblErrorGen.Text.Length.Equals(0))
                                    {
                                        lblErrorGen.Text = "Complete: " + lblCiudadH.Text;
                                    }
                                    else
                                    {
                                        lblErrorGen.Text += ", " + lblCiudadH.Text;
                                    }

                                }
                            }
                        }
                    }

                    if (txtFechaIngreso.Text.Length.Equals(0))
                    {
                        Resp = false;
                        if (lblErrorGen.Text.Length.Equals(0))
                        {
                            lblErrorGen.Text = "Complete: " + lblTFechaIngresoH.Text;
                        }
                        else
                        {
                            lblErrorGen.Text += ", " + lblTFechaIngresoH.Text;
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
                    cParametros.Complemento = "csBusquedaHoteles";
                    cParametros.ViewMessage.Add("Su sesion ha terminado");
                    cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                    cParametros.Metodo = "ValidarHotel";
                    ExceptionHandled.Publicar(cParametros);

                }

            }
            return Resp;
        }
        private bool ValidarCondiciones(UserControl PageSource)
        {
            bool Resp = true;
            CheckBox chkCondiciones = (CheckBox)PageSource.FindControl("chkCondiciones");
            Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");

            if (lblErrorGen != null)
                lblErrorGen.Text = "";

            if (chkCondiciones != null)
            {
                if (chkCondiciones.Checked == false)
                {
                    Resp = false;
                    if (lblErrorGen != null)
                        lblErrorGen.Text = "Debe aceptar los términos y condiciones";
                }
            }
            return Resp;
        }
        private void setActualizaAirport(UserControl PageSource)
        {


            // Hoteles
            TextBox txtCiudadDestino = (TextBox)PageSource.FindControl("txtCiudadDestino");

            string sTipoRefere = clsValidaciones.GetKeyOrAdd("AEROPUERTOS");

            string sAplicacion = clsSesiones.getAplicacion().ToString();
            string sIdioma = clsSesiones.getIdioma();

            try
            {

                if (txtCiudadDestino != null)
                {
                    if (txtCiudadDestino.Text.Length > 3)
                    {
                        if (!txtCiudadDestino.Text.Trim().Equals(" "))
                        {
                            //txtCiudadDestino.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txtCiudadDestino.Text, sIdioma, sAplicacion);
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
                cParametros.Complemento = "csBusquedaHoteles";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                cParametros.Metodo = "setActualizaAirport";
                ExceptionHandled.Publicar(cParametros);

            }
        }
        public void setProcesarBusquedaHotel(UserControl PageSource)
        {
            RadioButtonList modal_hotel = (RadioButtonList)PageSource.FindControl("modal_hotel");
            string sBusqueda = string.Empty;
            if (modal_hotel != null)
            {
                sBusqueda = modal_hotel.SelectedValue;
                if (!sBusqueda.Equals("0"))
                    sBusqueda = clsValidaciones.GetKeyOrAdd("TipoBusquedaHotel", "1");
            }
            else
            {
                sBusqueda = clsValidaciones.GetKeyOrAdd("TipoBusquedaHotel", "0");
            }

            clsCache cCache = new csCache().cCache();

            clsCacheControl cCacheControl = new clsCacheControl();
            string sSesion = string.Empty;
            try
            {
                if (cCache != null)
                {
                    string sCadenaCotizacion = getCadenaCotizacionServicios(clsValidaciones.GetKeyOrAdd("TemaMsjCotHotel", "COTHOT"));
                    sSesion = cCache.SessionID;
                    switch (sBusqueda)
                    {
                        case "0":
                            try { clsValidaciones.RedirectPagina("ResultadoHotelNales.aspx" + sCadenaCotizacion, true); }
                            catch { }

                            break;
                        case "1":
                            try { clsValidaciones.RedirectPagina("ResultadoHotel.aspx" + sCadenaCotizacion, true); }
                            catch { }
                            break;
                        case "4":
                            try { clsValidaciones.RedirectPagina("ResultadoHotel.aspx" + sCadenaCotizacion, true); }
                            catch { }
                            break;
                        case "5":
                            try { clsValidaciones.RedirectPagina("ResultadoHotel.aspx" + sCadenaCotizacion, true); }
                            catch { }
                            break;
                        case "6":
                            try { clsValidaciones.RedirectPagina("ResultadoHotel.aspx" + sCadenaCotizacion, true); }
                            catch { }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    try { csGeneralsPag.FinSesion(); }
                    catch (Exception) { }
                }
            }
            catch
            {
            }
        }
        private string getCadenaCotizacionServicios(string sTemaCotizacion)
        {
            string sCadenaCotizacion = "";
            try
            {
                if (clsValidaciones.GetKeyOrAdd("bCotizacionPorServicio", "False").ToUpper().Equals("TRUE"))
                {
                    string sTipoCotizacion = clsValidaciones.GetKeyOrAdd("TipoMsjCotizacion", "TM003");
                    string sTemasMensaje = clsValidaciones.GetKeyOrAdd("TemasMensajes", "TMMSJ");
                }
                string sIdOferta = csBuscadorMB.csOferta();
                if (!sIdOferta.Equals("0"))
                {
                    if (sCadenaCotizacion.Length.Equals(0))
                    {
                        sCadenaCotizacion = "?Id=" + sIdOferta;
                    }
                    else
                    {
                        sCadenaCotizacion = sCadenaCotizacion + "&IdOferta=" + sIdOferta;
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Aplication;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                cMensaje.Metodo = "Metodo:getCadenaCotizacionServicios";
                ExceptionHandled.Publicar(cMensaje);


            }
            return sCadenaCotizacion;
        }
        public void setPaginar(UserControl PageSource, int iPos, string sOrden, string sWhere)
        {
            csGeneralsPag.Idioma(PageSource);

            string sBusqueda = "0";
            try
            {
                try
                {
                    if (sOrden == null)
                        sOrden = csHoteles.ORDEN_HOTEL_AMOUNT + " ASC";
                    if (sOrden.Length.Equals(0))
                        sOrden = csHoteles.ORDEN_HOTEL_AMOUNT + " ASC";
                }
                catch { }
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    switch (cCache.TipoWebServices)
                    {
                        case Enum_WebServices.HotelInterNal:
                            sBusqueda = "3";
                            break;
                        case Enum_WebServices.SabreHotel:
                            sBusqueda = "2";
                            break;
                        case Enum_WebServices.HotelBedsHotel:
                            sBusqueda = "1";
                            break;
                        case Enum_WebServices.CotelcoHotel:
                            sBusqueda = "0";
                            break;
                        case Enum_WebServices.TouricoHotel:
                            sBusqueda = "4";
                            break;
                        case Enum_WebServices.ZeusHotel:
                            break;
                        default:
                            break;

                    }

                    switch (sBusqueda)
                    {
                        case "0":
                            setResultadosCotelcoFiler(PageSource, sWhere);
                            break;
                        case "1":
                            setResultadosHB(PageSource, iPos, sOrden, sWhere);
                            break;
                        default:
                            break;
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
                cParametros.Complemento = "Buscador hoteles ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void setPaginar(UserControl PageSource, int iPos)
        {
            setFiltro(PageSource, iPos);
        }
        #region HotelesHotelbeds
        public void setResultadosHB(UserControl PageSource, int iPage, string sOrden, string sEstrellas)
        {
            csGeneralsPag.Idioma(PageSource);

            Button btnNext = (Button)PageSource.FindControl("btnNext");

            if (iPage == 1)
            {
                btnNext.Enabled = true;
            }
            clsResultados dsResultado = new clsResultados();
            DropDownList ddlZone = (DropDownList)PageSource.FindControl("ddlZone");
            VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = new VO_HotelValuedAvailRQ();
            clsHotelValuedAvailHB cHotel = new clsHotelValuedAvailHB();
            csHoteles cHoteles = new csHoteles();

            vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();
            vo_HotelValuedAvailRQ.PaginationData = iPage.ToString();
            vo_HotelValuedAvailRQ.CurrentPage = 0;
            if (sEstrellas != null)
            {
                int Nfiltro = sEstrellas.IndexOf("|");
                if (Nfiltro != -1)
                {
                    sEstrellas = sEstrellas.Replace("|", "");
                    HttpContext.Current.Session["$Busqueda"] = sEstrellas;
                }

            }
            else if (sEstrellas == null && HttpContext.Current.Session["$Busqueda"] != null)
            {

                sEstrellas = HttpContext.Current.Session["$Busqueda"].ToString();
                sEstrellas = sEstrellas.Replace("|", "");
            }
            clsSesiones.setParametrosHotel(vo_HotelValuedAvailRQ);
            try
            {
                if (ddlZone != null)
                {
                    DataSet dsData = new DataSet();
                    dsData = cHoteles.dsZones(vo_HotelValuedAvailRQ.Destination);
                    clsControls.LlenaControl(ddlZone, dsData, "ZoneName", "ZoneCode", true);
                    if (vo_HotelValuedAvailRQ.Zone != null)
                        ddlZone.SelectedValue = vo_HotelValuedAvailRQ.Zone;
                }
            }
            catch { }
            try
            {

                dsResultado = cHotel.getServices(vo_HotelValuedAvailRQ);
                if (dsResultado.Error.Id != 0)
                {
                    clsSesiones.setResultadoHotel(dsResultado.dsResultados);
                    cHoteles.setResultados(PageSource, sOrden, sEstrellas);
                }
                else
                {
                    Limpiar(dsResultado.Error, PageSource);
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Aplication;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
                Limpiar(cMensaje, PageSource);
            }
        }
        //public void setFiltro(UserControl PageSource)
        //{
        //    RadioButtonList chkCategoria = (RadioButtonList)PageSource.FindControl("chkCategoria");

        //    string strWhere = String.Empty;
        //    switch (chkCategoria.SelectedValue.ToString())
        //    {
        //        case "1":
        //            strWhere = "1EST";
        //            break;

        //        case "2":
        //            strWhere = "2EST";
        //            break;

        //        case "3":
        //            strWhere = "3EST";
        //            break;

        //        case "4":
        //            strWhere = "4EST";
        //            break;

        //        case "5":
        //            strWhere = "5EST";
        //            break;
        //    }
        //    setPaginar(PageSource, 1, null, strWhere);
        //}
        //public void setFiltro(UserControl PageSource, int iPos)
        //{
        //    RadioButtonList chkCategoria = (RadioButtonList)PageSource.FindControl("chkCategoria");

        //    string strWhere = String.Empty;

        //    if (chkCategoria != null)
        //    {
        //        switch (chkCategoria.SelectedValue.ToString())
        //        {
        //            case "0":
        //                strWhere = null;
        //                break;

        //            case "1":
        //                strWhere = "1EST";
        //                break;

        //            case "2":
        //                strWhere = "2EST";
        //                break;

        //            case "3":
        //                strWhere = "3EST";
        //                break;

        //            case "4":
        //                strWhere = "4EST";
        //                break;

        //            case "5":
        //                strWhere = "5EST";
        //                break;
        //        }
        //    }
        //    setPaginar(PageSource, iPos, null, strWhere);
        //}
        public void setFiltro(UserControl PageSource)
        {
            RadioButtonList chkCategoria = (RadioButtonList)PageSource.FindControl("chkCategoria");

            string strWhere = String.Empty;
            switch (chkCategoria.SelectedValue.ToString())
            {
                case "1":
                    strWhere = "1EST";
                    break;

                case "2":
                    strWhere = "2EST";
                    break;

                case "3":
                    strWhere = "3EST";
                    break;

                case "4":
                    strWhere = "4EST";
                    break;

                case "5":
                    strWhere = "5EST";
                    break;
            }
            setPaginar(PageSource, 1, null, strWhere);
        }
        public void setFiltro(UserControl PageSource, int iPos)
        {
            RadioButtonList chkCategoria = (RadioButtonList)PageSource.FindControl("chkCategoria");

            string strWhere = String.Empty;
            switch (chkCategoria.SelectedValue.ToString())
            {
                case "0":
                    strWhere = null;
                    break;

                case "1":
                    strWhere = "1EST";
                    break;

                case "2":
                    strWhere = "2EST";
                    break;

                case "3":
                    strWhere = "3EST";
                    break;

                case "4":
                    strWhere = "4EST";
                    break;

                case "5":
                    strWhere = "5EST";
                    break;
            }
            if (HttpContext.Current.Session["$Estrellas"] != null)
            {
                strWhere = null;
            }
            setPaginar(PageSource, iPos, null, strWhere);
        }
        public void setFiltro(UserControl PageSource, string strWhere)
        {
            setPaginar(PageSource, 1, null, strWhere);
        }
        public void setFiltroZona(UserControl PageSource)
        {
            DropDownList ddlZone = (DropDownList)PageSource.FindControl("ddlZone");
            Panel pError = (Panel)PageSource.FindControl("pError");
            if (pError != null)
            {
                clsErrorMensaje cError = new clsErrorMensaje();
                cError.getError(null, pError);
            }
            else
            {
                new csCache().setError(PageSource, null);
            }

            try
            {
                csHoteles cHoteles = new csHoteles();
                VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = new VO_HotelValuedAvailRQ();
                vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();
                if (ddlZone.SelectedValue.ToString().Equals("0"))
                {
                    vo_HotelValuedAvailRQ.Zone = null;
                }
                else
                {
                    vo_HotelValuedAvailRQ.Zone = ddlZone.SelectedValue.ToString();
                    vo_HotelValuedAvailRQ.Type = Enum_TypeZone.SIMPLE;
                }
                vo_HotelValuedAvailRQ.PaginationData = "1";
                vo_HotelValuedAvailRQ.CurrentPage = 0;
                clsSesiones.setParametrosHotel(vo_HotelValuedAvailRQ);

                clsSesiones.setParametrosHotel(vo_HotelValuedAvailRQ);
                //cHoteles.setResultados(PageSource, null, null);
                setPaginar(PageSource, 1, null, null);
            }
            catch { }
        }
        public void setShorSell(UserControl PageSource)
        {
            try
            {
                csGeneralsPag.Idioma(PageSource);

                Repeater rptHotel = (Repeater)PageSource.FindControl("rptHotel");
                try
                {
                    for (int i = 0; i < rptHotel.Items.Count; i++)
                    {
                        Label lblError = (Label)rptHotel.Items[i].FindControl("lblError");
                        lblError.Text = string.Empty;
                    }
                }
                catch { }
                string sPantalla = "ReservaHotel.aspx?Seleccion=1&ID=1";
                clsSesiones.setPantalleRespuestaLogin(sPantalla);
                DataTable dtResultados = MarcarSeleccion(PageSource);
                if (Valida(dtResultados, PageSource))
                {
                    clsCacheControl cCacheControl = new clsCacheControl();
                    clsCache cCache = new csCache().cCache();

                    if (cCache == null || cCache.Verifica != true)
                    {
                        sPantalla = "Login.aspx?Seleccion=1&ID=1";
                    }
                }
                else
                {
                    sPantalla = "ResultadoHotel.aspx?Msjpop=solo puede seleccionar un tipo de acomodacion por habitacion&Fijo=1";

                }
                clsValidaciones.RedirectPagina(sPantalla);
            }
            catch { }
        }
        public void setShorSell(UserControl PageSource, object source, CommandEventArgs e, Repeater rptOcupacion)
        {
            csGeneralsPag.Idioma(PageSource);
            string sPantalla = "ReservaHotel.aspx?Seleccion=1&ID=1";
            clsSesiones.setPantalleRespuestaLogin(sPantalla);

            DataTable dtResultados = MarcarSeleccionHotel(source, e, rptOcupacion);

            clsCacheControl cCacheControl = new clsCacheControl();
            clsCache cCache = new csCache().cCache();

            if (cCache == null || cCache.Verifica != true)
                clsValidaciones.RedirectPagina("Login.aspx?Seleccion=1&ID=1");
            else
                clsValidaciones.RedirectPagina(sPantalla);

            Label lblError = PageSource.FindControl("lblError") as Label;
            lblError.Text = lblError.Text = "Debe seleccionar un tipo de acomodacion por habitacion";

        }
        public void setShorSell(object sender, EventArgs e, UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            Button lbReservar = (Button)sender;
            string sPantalla = "ReservaHotel.aspx?Seleccion=1&ID=1";
            clsSesiones.setPantalleRespuestaLogin(sPantalla);
            DataTable dtResultados = MarcarSeleccionDetalle(PageSource);
            if (ValidaDetalle(dtResultados, PageSource))
            {
                clsCacheControl cCacheControl = new clsCacheControl();
                clsCache cCache = clsSesiones.getCache();
                /*se verifica si el usuario esta logeado*/
                if (cCache == null || cCache.Verifica != true)
                    clsValidaciones.RedirectPagina("Login.aspx?Seleccion=1&ID=1");
                else
                    clsValidaciones.RedirectPagina(sPantalla);

            }
            else
            {
                Label lblError = PageSource.FindControl("lblError") as Label;
                if (lblError != null)
                    lblError.Text = lblError.Text = "solo puede seleccionar un tipo de acomodacion por habitacion";
            }
        }
        private DataTable MarcarSeleccionDetalle(UserControl UserControl)
        {
            Repeater rptTiposHabitacion = new Repeater();
            Repeater rptOcupacion = (Repeater)UserControl.FindControl("rptOcupacion");
            DataSet dsResultados = clsSesiones.getResultadoHotel();
            if (!clsValidaciones.ExistColumn(dsResultados.Tables["HotelRoom"], "Seleccion"))
                dsResultados.Tables["HotelRoom"].Columns.Add("Seleccion", typeof(string));

            for (int j = 0; j < rptOcupacion.Items.Count; j++)
            {
                rptTiposHabitacion = (Repeater)rptOcupacion.Items[j].FindControl("rptTiposHabitacion") as Repeater;
                RadioButtonList rblOcupacion = rptOcupacion.Items[j].FindControl("rblOcupacion") as RadioButtonList;
                for (int k = 0; k < rptTiposHabitacion.Items.Count; k++)
                {
                    ImageButton imgRadio = (ImageButton)rptTiposHabitacion.Items[k].FindControl("imgRadio");
                    Label lblCodCategoria = (Label)rptTiposHabitacion.Items[k].FindControl("lblCodCategoria");
                    RadioButton rbtSeleccion = (RadioButton)rptTiposHabitacion.Items[k].FindControl("rbtSeleccion");
                    if (rbtSeleccion != null && rbtSeleccion.Checked)
                    {
                        for (int m = 0; m < dsResultados.Tables["HotelRoom"].Rows.Count; m++)
                        {
                            if (dsResultados.Tables["HotelRoom"].Rows[m]["HotelRoom_Id"].ToString() == lblCodCategoria.Text)
                            {
                                dsResultados.Tables["HotelRoom"].Rows[m]["Seleccion"] = "1";
                            }
                        }
                    }
                    if (rblOcupacion != null)
                    {
                        if (dsResultados != null &&
                            dsResultados.Tables.Count != 0 &&
                            dsResultados.Tables["HotelRoom"].Rows.Count != 0 &&
                            rblOcupacion.SelectedItem != null)
                        {
                            DataRow filaEncontrada =
                                dsResultados.Tables["HotelRoom"].Rows.Find(rblOcupacion.SelectedItem.Value);
                            if (filaEncontrada != null)
                            {
                                filaEncontrada[COLUMN_SELECCION] = "1";
                            }
                        }

                    }
                }
            }
            //
            DataRow[] drResultado = dsResultados.Tables["HotelRoom"].Select("Seleccion ='1'");
            DataTable dtResultados = dsResultados.Tables["HotelRoom"].Clone();
            foreach (DataRow drFila in drResultado)
            {
                DataRow drDatos = dtResultados.NewRow();
                drDatos[COLUMN_HOTEL_ROOM_OCUPATION] = drFila[COLUMN_HOTEL_ROOM_OCUPATION];
                drDatos[COLUMN_HOTEL_ROOM_ID] = drFila[COLUMN_HOTEL_ROOM_ID];
                drDatos[COLUMN_SHRUI] = drFila[COLUMN_SHRUI];
                drDatos[COLUMN_ROOM_TYPE_TEXT] = drFila[COLUMN_ROOM_TYPE_TEXT];
                drDatos[COLUMN_CODE] = drFila[COLUMN_CODE];
                drDatos[COLUMN_CHARACTERISTIC] = drFila[COLUMN_CHARACTERISTIC];
                drDatos[COLUMN_SERVICEHOTEL_ID] = drFila[COLUMN_SERVICEHOTEL_ID];
                drDatos[COLUMN_SELECCION] = drFila[COLUMN_SELECCION];
                dtResultados.Rows.Add(drDatos);
            }
            return dtResultados;
        }
        private bool ValidaDetalle(DataTable dtResultados, UserControl PageSource)
        {
            Repeater rptTiposHabitacion = new Repeater();
            Repeater rptOcupacion = new Repeater();
            DataSet dsResultados = clsSesiones.getResultadoHotel();
            bool valida = true;
            for (int j = 0; j < rptOcupacion.Items.Count; j++)
            {
                rptTiposHabitacion = (Repeater)rptOcupacion.Items[j].FindControl("rptTiposHabitacion") as Repeater;
                Label lblidHabitacion = (Label)rptOcupacion.Items[j].FindControl("lblidHabitacion");
                for (int k = 0; k < rptTiposHabitacion.Items.Count; k++)
                {
                    Label lblCodCategoria = (Label)rptTiposHabitacion.Items[k].FindControl("lblCodCategoria");
                    if (dtResultados.Rows.Count > rptOcupacion.Items.Count)
                    {
                        for (int m = 0; m < dtResultados.Rows.Count; m++)
                        {
                            if (dtResultados.Rows[m]["HotelRoom_Id"].ToString() == lblCodCategoria.Text)
                            {
                                if (dtResultados.Rows[m]["Id"].ToString() == lblidHabitacion.Text)
                                {
                                    if (m > 0)
                                    {
                                        if (dtResultados.Rows[m]["Seleccion"].ToString() == "1" && dtResultados.Rows[m]["Seleccion"].ToString() == dtResultados.Rows[m - 1]["Seleccion"].ToString())
                                        {
                                            BorrarSeleccionDetalle();
                                            for (int n = 0; n < dsResultados.Tables["HotelRoom"].Rows.Count; n++)
                                            {
                                                dsResultados.Tables["HotelRoom"].Rows[n]["Seleccion"] = "";
                                            }
                                            valida = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        valida = false;
                        HttpContext.Current.Response.Write("<script>alert('Debe seleccionar al menos un tipo de habitacion');</script>");
                    }
                }
            }
            clsSesiones.setResultadoHotel(dsResultados);
            return valida;
        }
        private void BorrarSeleccionDetalle()
        {
            Repeater rptTiposHabitacion = new Repeater();
            Repeater rptOcupacion = new Repeater();

            for (int j = 0; j < rptOcupacion.Items.Count; j++)
            {
                rptTiposHabitacion = (Repeater)rptOcupacion.Items[j].FindControl("rptTiposHabitacion") as Repeater;
                for (int k = 0; k < rptTiposHabitacion.Items.Count; k++)
                {
                    RadioButton rbtSeleccion = (RadioButton)rptTiposHabitacion.Items[k].FindControl("rbtSeleccion");

                    if (rbtSeleccion.Checked)
                    {
                        rbtSeleccion.Checked = false;
                    }
                }
            }
        }

        private DataTable MarcarSeleccion(UserControl PageSource)
        {
            Repeater rptHotel = (Repeater)PageSource.FindControl("rptHotel");
            Label lblError = (Label)PageSource.FindControl("lblError");

            Repeater rptTiposHabitacion = new Repeater();
            Repeater rptOcupacion = new Repeater();
            DataSet dsResultados = clsSesiones.getResultadoHotel();
            if (!clsValidaciones.ExistColumn(dsResultados.Tables["HotelRoom"], COLUMN_SELECCION))
                dsResultados.Tables["HotelRoom"].Columns.Add(COLUMN_SELECCION, typeof(string));

            for (int i = 0; i < rptHotel.Items.Count; i++)
            {
                rptOcupacion = (Repeater)rptHotel.Items[i].FindControl("rptOcupacion");
                for (int j = 0; j < rptOcupacion.Items.Count; j++)
                {
                    rptTiposHabitacion = (Repeater)rptOcupacion.Items[j].FindControl("rptTiposHabitacion") as Repeater;
                    RadioButtonList rblOcupacion = rptOcupacion.Items[j].FindControl("rblOcupacion") as RadioButtonList;
                    for (int k = 0; k < rptTiposHabitacion.Items.Count; k++)
                    {
                        ImageButton imgRadio = (ImageButton)rptTiposHabitacion.Items[k].FindControl("imgRadio");
                        Label lblCodCategoria = (Label)rptTiposHabitacion.Items[k].FindControl("lblCodCategoria");
                        RadioButton rbtSeleccion = (RadioButton)rptTiposHabitacion.Items[k].FindControl("rbtSeleccion");
                        if (rblOcupacion != null)
                        {
                            if (dsResultados != null &&
                                dsResultados.Tables.Count != 0 &&
                                dsResultados.Tables["HotelRoom"].Rows.Count != 0 &&
                                rblOcupacion.SelectedItem != null)
                            {
                                DataRow filaEncontrada =
                                    dsResultados.Tables["HotelRoom"].Rows.Find(rblOcupacion.SelectedItem.Value);
                                if (filaEncontrada != null)
                                {
                                    filaEncontrada[COLUMN_SELECCION] = "1";
                                }
                            }

                        }
                        else
                        {
                            if (rbtSeleccion.Checked)
                            {
                                for (int m = 0; m < dsResultados.Tables["HotelRoom"].Rows.Count; m++)
                                {
                                    if (dsResultados.Tables["HotelRoom"].Rows[m]["HotelRoom_Id"].ToString() == lblCodCategoria.Text)
                                    {
                                        dsResultados.Tables["HotelRoom"].Rows[m][COLUMN_SELECCION] = "1";
                                    }
                                }
                            }
                            else
                            {
                                lblError.Text = "Para continuar debe seleccionar una acomodacion por cada habitacion del plan";
                            }
                        }
                    }
                }
            }
            clsSesiones.setResultadoHotel(dsResultados);
            DataRow[] drResultado = dsResultados.Tables["HotelRoom"].Select("Seleccion ='1'");
            DataTable dtResultados = dsResultados.Tables["HotelRoom"].Clone();
            foreach (DataRow drFila in drResultado)
            {
                DataRow drDatos = dtResultados.NewRow();
                drDatos[COLUMN_HOTEL_ROOM_OCUPATION] = drFila[COLUMN_HOTEL_ROOM_OCUPATION];
                drDatos[COLUMN_HOTEL_ROOM_ID] = drFila[COLUMN_HOTEL_ROOM_ID];
                drDatos[COLUMN_SHRUI] = drFila[COLUMN_SHRUI];
                drDatos[COLUMN_ROOM_TYPE_TEXT] = drFila[COLUMN_ROOM_TYPE_TEXT];
                drDatos[COLUMN_CODE] = drFila[COLUMN_CODE];
                drDatos[COLUMN_CHARACTERISTIC] = drFila[COLUMN_CHARACTERISTIC];
                drDatos[COLUMN_SERVICEHOTEL_ID] = drFila[COLUMN_SERVICEHOTEL_ID];
                drDatos[COLUMN_SELECCION] = drFila[COLUMN_SELECCION];
                dtResultados.Rows.Add(drDatos);
            }
            return dtResultados;
        }
        private DataTable MarcarSeleccionHotel(object sender, CommandEventArgs e, Repeater rptOcupacion)
        {
            DataSet dsResultados = clsSesiones.getResultadoHotel();
            if (!clsValidaciones.ExistColumn(dsResultados.Tables["HotelRoom"], COLUMN_SELECCION))
                dsResultados.Tables["HotelRoom"].Columns.Add(COLUMN_SELECCION, typeof(string));

            for (int c = 0; rptOcupacion != null && c < rptOcupacion.Items.Count; c++)
            {

                if (dsResultados != null &&
                    dsResultados.Tables.Count != 0 &&
                    dsResultados.Tables["HotelRoom"].Rows.Count != 0)
                {
                    DataRow filaEncontrada =
                        dsResultados.Tables["HotelRoom"].Rows.Find(((System.Web.UI.WebControls.Button)(sender)).Text);
                    if (filaEncontrada != null)
                    {
                        filaEncontrada[COLUMN_SELECCION] = "1";
                    }
                }

            }

            clsSesiones.setResultadoHotel(dsResultados);
            DataRow[] drResultado = dsResultados.Tables["HotelRoom"].Select("Seleccion ='1'");
            DataTable dtResultados = dsResultados.Tables["HotelRoom"].Clone();

            foreach (DataRow drFila in drResultado)
            {
                DataRow drDatos = dtResultados.NewRow();
                drDatos[COLUMN_HOTEL_ROOM_OCUPATION] = drFila[COLUMN_HOTEL_ROOM_OCUPATION];
                drDatos[COLUMN_HOTEL_ROOM_ID] = drFila[COLUMN_HOTEL_ROOM_ID];
                drDatos[COLUMN_SHRUI] = drFila[COLUMN_SHRUI];
                drDatos[COLUMN_ROOM_TYPE_TEXT] = drFila[COLUMN_ROOM_TYPE_TEXT];
                drDatos[COLUMN_CODE] = drFila[COLUMN_CODE];
                drDatos[COLUMN_CHARACTERISTIC] = drFila[COLUMN_CHARACTERISTIC];
                drDatos[COLUMN_SERVICEHOTEL_ID] = drFila[COLUMN_SERVICEHOTEL_ID];
                drDatos[COLUMN_SELECCION] = drFila[COLUMN_SELECCION];
                dtResultados.Rows.Add(drDatos);
            }
            return dtResultados;
        }
        private bool Valida(DataTable dtResultados, UserControl PageSource)
        {
            Repeater rptHotel = (Repeater)PageSource.FindControl("rptHotel");
            DataSet dsResultados = clsSesiones.getResultadoHotel();
            Repeater rptTiposHabitacion = new Repeater();
            Repeater rptOcupacion = new Repeater();
            bool valida = false;
            for (int i = 0; i < rptHotel.Items.Count; i++)
            {
                rptOcupacion = (Repeater)rptHotel.Items[i].FindControl("rptOcupacion");
                Label lblError1 = (Label)rptHotel.Items[i].FindControl("lblError");
                for (int j = 0; j < rptOcupacion.Items.Count; j++)
                {
                    rptTiposHabitacion = (Repeater)rptOcupacion.Items[j].FindControl("rptTiposHabitacion") as Repeater;
                    Label lblidHabitacion = (Label)rptOcupacion.Items[j].FindControl("lblidHabitacion");
                    for (int k = 0; k < rptTiposHabitacion.Items.Count; k++)
                    {
                        Label lblCodCategoria = (Label)rptTiposHabitacion.Items[k].FindControl("lblCodCategoria");
                        if (dtResultados.Rows.Count > rptOcupacion.Items.Count)
                        {
                            for (int m = 0; m < dtResultados.Rows.Count; m++)
                            {
                                if (dtResultados.Rows[m]["HotelRoom_Id"].ToString() == lblCodCategoria.Text)
                                {
                                    if (dtResultados.Rows[m]["Id"].ToString() == lblidHabitacion.Text)
                                    {
                                        try
                                        {
                                            if (dtResultados.Rows[m][COLUMN_SELECCION].ToString() == "1" && dtResultados.Rows[m]["Seleccion"].ToString() == dtResultados.Rows[m - 1]["Seleccion"].ToString())
                                            {
                                                BorrarSeleccion(PageSource);
                                                for (int n = 0; n < dsResultados.Tables["HotelRoom"].Rows.Count; n++)
                                                {
                                                    dsResultados.Tables["HotelRoom"].Rows[n][COLUMN_SELECCION] = "";
                                                }
                                                valida = false;
                                                lblError1.Text = "solo puede seleccionar un tipo de acomodacion por habitacion";
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                        else if (dtResultados.Rows.Count < rptOcupacion.Items.Count)
                        {
                            lblError1.Text = "Debe seleccionar al menos una Opcion por habitacion";
                            valida = false;
                        }
                        else
                        {
                            valida = true;
                        }
                    }
                }
            }
            return valida;
        }
        private void BorrarSeleccion(UserControl PageSource)
        {
            Repeater rptHotel = (Repeater)PageSource.FindControl("rptHotel");
            Repeater rptTiposHabitacion = new Repeater();
            Repeater rptOcupacion = new Repeater();

            for (int i = 0; i < rptHotel.Items.Count; i++)
            {
                rptOcupacion = (Repeater)rptHotel.Items[i].FindControl("rptOcupacion");
                for (int j = 0; j < rptOcupacion.Items.Count; j++)
                {
                    rptTiposHabitacion = (Repeater)rptOcupacion.Items[j].FindControl("rptTiposHabitacion") as Repeater;
                    for (int k = 0; k < rptTiposHabitacion.Items.Count; k++)
                    {
                        RadioButton rbtSeleccion = (RadioButton)rptTiposHabitacion.Items[k].FindControl("rbtSeleccion");

                        if (rbtSeleccion.Checked)
                        {
                            rbtSeleccion.Checked = false;
                        }
                    }
                }
            }
        }
        public void setCommand(UserControl PageSource, object sender, CommandEventArgs e)
        {
            try
            {
                csGeneralsPag.Idioma(PageSource);
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    switch (e.CommandName)
                    {
                        case "Next":
                            Next_Click(PageSource);
                            break;
                        case "Back":
                            Back_Click(PageSource);
                            break;
                        case "Confirmar":
                            if (bValidaDatos(PageSource))
                            {
                                setReservarHB(PageSource);
                            }
                            break;
                        case "SubirCotelco":
                            setSubirCotelco(PageSource);
                            break;
                        case "Cancelar":
                            csGeneralsPag.Buscador();
                            break;
                    }
                }
            }
            catch
            {

            }
        }
        private void Next_Click(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            int iPage = int.Parse(clsSesiones.getPage()) + 1;
            setPaginar(PageSource, iPage, csHoteles.ORDEN_HOTEL_AMOUNT, null);
        }
        private void Back_Click(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            int iPage = int.Parse(clsSesiones.getPage()) - 1;
            setPaginar(PageSource, iPage, csHoteles.ORDEN_HOTEL_AMOUNT, null);
        }
        public bool bValidaDatos(UserControl PageSource)
        {
            bool bValida = true;
            try
            {
                Label lblError = PageSource.FindControl("lblError") as Label;


                TextBox txtNombre = PageSource.FindControl("txtNombre1") as TextBox;
                TextBox txtApellido = PageSource.FindControl("txtApellido1") as TextBox;

                if (lblError != null)
                {
                    lblError.Text = string.Empty;
                }
                if (txtNombre != null)
                {
                    if (txtNombre.Text.Length.Equals(0))
                    {
                        bValida = false;
                        if (lblError != null)
                        {
                            lblError.Text = "Debe completar todos los campos de nombres y apellidos";

                        }
                    }
                }
                if (txtApellido != null)
                {
                    if (txtApellido.Text.Length.Equals(0))
                    {
                        bValida = false;
                        if (lblError != null)
                        {
                            lblError.Text = "Debe completar todos los campos de nombres y apellidos";

                        }
                    }
                }

            }
            catch { bValida = false; }
            return bValida;
        }
        public void setReservarHB(UserControl PageSource)
        {
            clsResultados dsResultado = new clsResultados();
            clsPurchaseConfirmHB cHotel = new clsPurchaseConfirmHB();
            csHoteles cHoteles = new csHoteles();
            clsCache cCache = new csCache().cCache();
            HiddenField strRecord = (HiddenField)PageSource.FindControl("strRecord");
            AjaxControlToolkit.ModalPopupExtender mpeReserva = (AjaxControlToolkit.ModalPopupExtender)PageSource.FindControl("MPEEConfirm");
            AjaxControlToolkit.ModalPopupExtender mpeFallo = (AjaxControlToolkit.ModalPopupExtender)PageSource.FindControl("MPEEFallo");
            Label ErrorReserva = new Label();
            ErrorReserva.Text = "0";
            try
            {
                setDatos(PageSource);
                DataSet dsResultados = cHoteles.setTablaReserva();
                if (cCache != null)
                {
                    if (validarPoiliticas(PageSource, dsResultados, cCache))
                    {
                        dsResultado = cHotel.getServices();

                        if (dsResultado.Error.Id != 0)
                        {
                            dsResultado.dsResultados = cHoteles.setTablaConfirma();
                            HttpContext.Current.Session["Incoming_Code"] = dsResultado.dsResultados.Tables["HotelRoom"].Rows[0]["Incoming_Code"].ToString();
                            cHoteles.setDetalleConfirma(PageSource, true, true);


                            Label lblRecord = (Label)PageSource.FindControl("lblRecord");
                            lblRecord.Text = dsResultado.dsResultados.Tables["HotelRoom"].Rows[0]["filenumber"].ToString();
                            strRecord.Value = lblRecord.Text;
                            if (mpeReserva != null)
                            {
                                mpeReserva.Show();
                            }
                            else
                            {
                                mpeFallo.Show();
                            }

                            RadioButtonList rblFormasPago = (RadioButtonList)PageSource.FindControl("rblFormasPago");
                            clsParametros cParam = new clsParametros();
                            if (rblFormasPago != null)
                            {
                                string sTC = clsValidaciones.GetKeyOrAdd("TarjetaCredito", "TC");
                                string sEfec = clsValidaciones.GetKeyOrAdd("Efectivo", "EFE");
                                string sPSE = clsValidaciones.GetKeyOrAdd("PSE", "PSE");
                                string sNap = clsValidaciones.GetKeyOrAdd("NAP", "NAP");
                                if (rblFormasPago.SelectedValue.Equals(sTC))
                                {

                                    try
                                    {
                                        string sResult = new Reserva.csReserva().setInsertarTarjeta(PageSource, cCache);
                                    }
                                    catch
                                    {
                                        ExceptionHandled.Publicar("Sucedio un error al tratar de insertar datos de la tarjeta");
                                    }
                                    try
                                    {
                                        new Reserva.csReserva().setInsertarFormaPago(sTC, PageSource, "HINTER", dsResultado.dsResultados.Tables["HotelRoom"].Rows[0]["FileNumber"].ToString());
                                        new csUtilitarios().setCorreos(lblRecord.Text, sTC, "RTT");
                                    }
                                    catch
                                    {

                                        ExceptionHandled.Publicar("Sucedio un error al tratar de insertar datos de FormaPago:metodo setInsertarFormaPago");
                                    }

                                }
                                else if (rblFormasPago.SelectedValue.Equals(sEfec))
                                {
                                    try
                                    {
                                        new Reserva.csReserva().setInsertarFormaPago(sEfec, PageSource, "HINTER", dsResultado.dsResultados.Tables["HotelRoom"].Rows[0]["FileNumber"].ToString());
                                        new csUtilitarios().setCorreos(lblRecord.Text, sEfec, "RTT");
                                    }
                                    catch { }

                                }
                                else if (rblFormasPago.SelectedValue.Equals(sPSE))
                                {

                                    new Reserva.csReserva().EnviarValoresCompleto(rblFormasPago.SelectedValue.ToString(), PageSource, "HOTELES", "");

                                }
                                else if (rblFormasPago.SelectedValue.Equals(sNap))
                                {
                                    try
                                    {
                                        new Reserva.csReserva().setInsertarFormaPago(sNap, PageSource, "HINTER", dsResultado.dsResultados.Tables["HotelRoom"].Rows[0]["FileNumber"].ToString());
                                        new csUtilitarios().setCorreos(lblRecord.Text, sNap, "RTT");
                                    }
                                    catch { }

                                }

                            }


                        }
                        else
                        {
                            LimpiarError(dsResultado.Error, PageSource);
                        }
                    }
                }
                else
                {
                }

                csCarrito cCarrito = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
                cCarrito.LimpiarCarrito();
            }
            catch (Exception Ex)
            {
                //ErrorReserva.Text = "1";
                //mpeFallo.Show();
                csCarrito cCarrito = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
                cCarrito.LimpiarCarrito();
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Aplication;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
                LimpiarError(cMensaje, PageSource);
            }
        }
        private void setDatos(UserControl PageSource)
        {
            Repeater rptHabitaciones = (Repeater)PageSource.FindControl("rptHabitaciones");
            Repeater rptDatosReserva = (Repeater)PageSource.FindControl("rptDatosReserva");

            // se llenan los responsables
            List<VO_Passenger> cPassenger = clsSesiones.getPassenger();
            //csOcupacion cOcupacion = new csOcupacion();
            for (int i = 0; i < cPassenger.Count; i++)
            {
                if (((TextBox)rptHabitaciones.Items[i].FindControl("txtNombre")) != null)
                {
                    cPassenger[i].RespNombre = ((TextBox)rptHabitaciones.Items[i].FindControl("txtNombre")).Text;
                    cPassenger[i].RespApellido = ((TextBox)rptHabitaciones.Items[i].FindControl("txtApellido1")).Text;
                    cPassenger[i].RespTelefono = ((TextBox)rptHabitaciones.Items[i].FindControl("txtTelefonoH")).Text;
                    cPassenger[i].RespDocumento = ((TextBox)PageSource.FindControl("txtDocumento1")).Text;
                    cPassenger[i].RespGenero = ((DropDownList)PageSource.FindControl("ddlGenero")).SelectedValue;
                    cPassenger[i].RespTipoDoc = ((DropDownList)PageSource.FindControl("ddlTipoDoc")).SelectedValue;
                    cPassenger[i].RespTelefono = ((TextBox)PageSource.FindControl("txtCelular")).Text;
                }

                else
                {
                    if (rptDatosReserva.Items[i].FindControl("txtNombre1") != null)
                    {
                        cPassenger[i].RespApellido = ((TextBox)rptDatosReserva.Items[i].FindControl("txtApellido1")).Text;
                        cPassenger[i].RespNombre = ((TextBox)rptDatosReserva.Items[i].FindControl("txtNombre1")).Text;
                        cPassenger[i].RespGenero = ((DropDownList)rptDatosReserva.Items[i].FindControl("ddlGenero")).SelectedValue;
                        cPassenger[i].RespTipoDoc = ((DropDownList)rptDatosReserva.Items[i].FindControl("ddlTipoDoc")).SelectedValue;
                        cPassenger[i].RespDocumento = ((TextBox)rptDatosReserva.Items[i].FindControl("txtDocumento1")).Text;
                        cPassenger[i].RespTelefono = ((TextBox)PageSource.FindControl("txtTelefono")).Text;
                        cPassenger[i].RespTelefono = ((TextBox)PageSource.FindControl("txtCelular")).Text;
                    }
                }


            }
            clsSesiones.setPassenger(cPassenger);
            new csHoteles().CargarNameOccupancy();
        }
        private bool validarPoiliticas(UserControl PageSource, DataSet dsResultados, clsCache cCache)
        {
            csHoteles cHoteles = new csHoteles();
            DataSet dsResultados2 = clsSesiones.getResultadoHotel();
            DataTable dtHabitacion = ModificarDataset(dsResultados2, cCache);
            DataTable dtFechaPoliticas = dsResultados.Tables["DateTimeFrom"];
            Repeater rptHabitaciones = (Repeater)PageSource.FindControl("rptHabitaciones");
            Label lblPrecioTotal = (Label)PageSource.FindControl("lblPrecioTotal");
            int iReembolsable = 0;
            bool Respuesta = true;
            AjaxControlToolkit.ModalPopupExtender mpeFallo = (AjaxControlToolkit.ModalPopupExtender)PageSource.FindControl("MPEEFallo");
            Label ErrorReserva = new Label();
            string a = "0";
            try
            {
                try
                {
                    if (clsValidaciones.GetKeyOrAdd("bValidaReembolsoHotel", "True").ToUpper().Equals("TRUE"))
                        iReembolsable = int.Parse(dsResultados.Tables[TABLA_HOTEL_INFO].Rows[0][COLUMN_NON_REEMBOLSABLE].ToString());
                }
                catch { }
                if (iReembolsable.Equals(0))
                {
                    for (int i = 0; i < rptHabitaciones.Items.Count; i++)
                    {
                        Repeater RptPenalizacion = (Repeater)rptHabitaciones.Items[i].FindControl("RptPenalizacion");
                        for (int j = 0; j < RptPenalizacion.Items.Count; j++)
                        {
                            Label lblValPen = (Label)RptPenalizacion.Items[j].FindControl("lblValPen");
                            Label lblFecIniPen = (Label)RptPenalizacion.Items[j].FindControl("lblFecIniPen");

                            decimal Penalizacion = clsValidaciones.getDecimalNotRound(lblValPen.Text);
                            decimal TotalReserva = clsValidaciones.getDecimalNotRound(lblPrecioTotal.Text);

                            DateTime dtmFecha_vencimiento = default(DateTime);

                            if (dsResultados != null &&
                                dsResultados.Tables.Count != 0)
                            {
                                try
                                {
                                    DataTable dtPoliticasCancelacion =
                                                        dsResultados.Tables["CancellationPolicy"];
                                    string[] arrFecha = dtPoliticasCancelacion.Rows[0]["DateTimeFrom_YMD"].ToString().Split('/');

                                    if (arrFecha.Length != 0)
                                    {
                                        dtmFecha_vencimiento =
                                            new DateTime(
                                            int.Parse(arrFecha[0]/*año*/),
                                            int.Parse(arrFecha[1]/*mes*/),
                                            int.Parse(arrFecha[2]/*dia*/));
                                    }
                                }
                                catch (Exception) { }
                            }

                            if (DateTime.Now >= dtmFecha_vencimiento)
                            {
                                a = "1";
                                Respuesta = false;
                                //se debe colocar
                                ObtenerHTMLPoliticas(PageSource, dsResultados, dtHabitacion, cCache);
                                Label lblError = PageSource.FindControl("lblError") as Label;
                                if (lblError != null)
                                {
                                    lblError.Visible = true;
                                    lblError.Text = "Este hotel no se puede reservar porque la Fecha Límite para Cancelar ya se vencio.";
                                }

                                if (a == "1")
                                {
                                    csContactenos cRefere = new csContactenos();
                                    ErrorReserva.Text = a;
                                    mpeFallo.Show();
                                    cRefere.setEnviarFalloHoteles(PageSource);
                                }

                                //clsValidaciones.RedirectPagina(
                                //         "ReservaHotel.aspx?POLITICAS=TRUE&Msjpop=Debido a que la solicitud se encuentra dentro del plazo límite, un asesor se contactara con usted para continuar el proceso de reserva");

                            }
                        }
                    }
                }
                else
                {
                    Respuesta = false;
                    //se debe colocar
                    ObtenerHTMLPoliticas(PageSource, dsResultados, dtHabitacion, cCache);
                    Label lblError = PageSource.FindControl("lblError") as Label;
                    if (lblError != null)
                    {
                        lblError.Visible = true;
                        lblError.Text = "Este hotel no se puede reservar porque la tarifa es no reembolsable.";
                    }

                    clsValidaciones.RedirectPagina(
                             "ReservaHotel.aspx?POLITICAS=TRUE&Msjpop=Debido a que la tarifa es no reembolsable, un asesor se contactara con usted para continuar el proceso de reserva");
                }
            }
            catch { }
            return Respuesta;
        }
        private DataTable ModificarDataset(DataSet dsResultados, clsCache cCache)
        {

            VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();
            DataTable dtHabitacion = dsResultados.Tables["tblHabitaciones"].Clone();

            if (!clsValidaciones.ExistColumn(dtHabitacion, "Responsable"))
                dtHabitacion.Columns.Add("Responsable");
            for (int j = 0; j < dsResultados.Tables["tblHabitaciones"].Rows.Count; j++)
            {
                DataRow drFila = dtHabitacion.NewRow();
                drFila["id"] = dsResultados.Tables["tblHabitaciones"].Rows[j]["id"].ToString();
                drFila["AdultCount"] = dsResultados.Tables["tblHabitaciones"].Rows[j]["AdultCount"].ToString();
                drFila["ChildCount"] = dsResultados.Tables["tblHabitaciones"].Rows[j]["ChildCount"].ToString();
                dtHabitacion.Rows.Add(drFila);
            }
            for (int i = 0; i < vo_HotelValuedAvailRQ.lHotelOccupancy.Count; i++)
            {
                for (int k = 0; k < vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList.Count; k++)
                {
                    if (vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[k].Name != "")
                    {
                        dtHabitacion.Rows[i]["Responsable"] = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[k].Name + " " + vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[k].LastName;
                    }

                }
            }
            new csHoteles().GuardarTablaOActualizarTabla("Huespedes" + cCache.SessionID.ToString(), dtHabitacion, "tblHuespedes");
            return dtHabitacion;
        }
        protected void ObtenerHTMLPoliticas(UserControl PageSource, DataSet dsResultados, DataTable dtHabitaciones, clsCache cCache)
        {
            //string ruta = default(string);

            Utils.Utils cUtil = new Utils.Utils();

            string sNombre = cCache.Nombre;
            if (sNombre.Trim().Equals(""))
                sNombre = cCache.Nombres + " " + cCache.Apellidos;

            string FormaPago = clsValidaciones.GetKeyOrAdd("FormaPagoNA", "NAP");
            new csHoteles().GuardarDataSetOActualizarDataSet("Resultados" + cCache.SessionID, dsResultados);
            //ruta = cUtil.ObtenerRutaWeb(PageSource, csPlantillaCorreos.PlantillaPoliticas + "?idSesion=" + cCache.SessionID + "&Usuario=" + sNombre + "&Telefono=" + cCache.Telefono + "&Correo=" + cCache.User + "&FormaPago=" + FormaPago);
            //string strHtml = csGene.ObtenerPlantillaHTML(ruta);
            /*GUARDAMOS EL CASO*/
            //SetGuardarCaso(cCache, dtHabitaciones, dsResultados);
            /*ENVIAMOS EL MAIL*/
            //EnviarMailPoliticas(strHtml, cCache, FormaPago, cCache.User);
            clsSesiones.setPantalleRespuestaLogin(null);
        }
        //public void setDetalleTT(UserControl PageSource)
        //{
        //    csGeneralsPag.Idioma(PageSource);

        //    if (!PageSource.Page.IsPostBack)
        //    {
        //        string ID_ = HttpContext.Current.Request["ID"];
        //        if (ID_ != null)
        //        {
        //            csGeneralsPag.Idioma(PageSource);


        //            try
        //            {
        //                clsCache cCache = new csCache().cCache();
        //                if (cCache != null)
        //                {
        //                    new csHoteles().setDetalle(PageSource, ID_);
        //                }
        //                else
        //                {
        //                    csGeneralsPag.FinSesion();
        //                }
        //            }
        //            catch (Exception Ex)
        //            {
        //                clsParametros cParametros = new clsParametros();
        //                cParametros.Id = 0;
        //                cParametros.Message = Ex.Message.ToString();
        //                cParametros.Source = Ex.Source.ToString();
        //                cParametros.Tipo = clsTipoError.Library;
        //                cParametros.Severity = clsSeveridad.Moderada;
        //                cParametros.StackTrace = Ex.StackTrace.ToString();
        //                cParametros.Complemento = "Buscador hoteles ";
        //                cParametros.ViewMessage.Add("Su sesion ha terminado");
        //                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
        //                ExceptionHandled.Publicar(cParametros);
        //            }
        //        }
        //    }
        //}
        public void setDetalle(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            if (!PageSource.Page.IsPostBack)
            {
                string ID_ = HttpContext.Current.Request["ID"];
                if (ID_ != null)
                {
                    csGeneralsPag.Idioma(PageSource);

                    string sBusqueda = "0";
                    try
                    {
                        clsCache cCache = new csCache().cCache();
                        if (cCache != null)
                        {
                            switch (cCache.TipoWebServices)
                            {
                                case Enum_WebServices.HotelInterNal:
                                    sBusqueda = "3";
                                    break;
                                case Enum_WebServices.SabreHotel:
                                    sBusqueda = "2";
                                    break;
                                case Enum_WebServices.HotelBedsHotel:
                                    sBusqueda = "1";
                                    break;
                                case Enum_WebServices.CotelcoHotel:
                                    sBusqueda = "0";
                                    break;
                                case Enum_WebServices.ZeusHotel:
                                    break;
                                case Enum_WebServices.TouricoHotel:
                                    sBusqueda = "4";
                                    break;
                                default:
                                    break;

                            }

                            switch (sBusqueda)
                            {
                                case "0":
                                    //setFormularioDetalles(ID_, PageSource);
                                    break;
                                case "1":
                                    setFormularioDetallesHB(ID_, PageSource);
                                    break;
                                case "2":
                                    //setFormularioDetallesTT(ID_, PageSource);
                                    break;
                                case "3":
                                    //setFormularioDetallesHIN(ID_, PageSource);
                                    break;
                                case "4":
                                    //setFormularioDetallesTRC(ID_, PageSource);
                                    break;
                                default:
                                    break;
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
                        cParametros.Complemento = "Buscador hoteles ";
                        cParametros.ViewMessage.Add("Su sesion ha terminado");
                        cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                        ExceptionHandled.Publicar(cParametros);
                    }
                }
            }
        }
        private void setFormularioDetallesHB(string Id, UserControl PageSource)
        {
            VO_HotelDetailRQ vo_HotelDetailRQ = new VO_HotelDetailRQ();
            clsHotelDetailHB cHotel = new clsHotelDetailHB();
            vo_HotelDetailRQ.HotelCode = Id;

            csHoteles csHoteles = new csHoteles();

            try
            {
                if (Id != null)
                {
                    clsResultados cResultado = cHotel.getServices(vo_HotelDetailRQ);
                    csHoteles.setDetalle(PageSource, Id, cResultado.dsResultados);
                }
                else
                {
                    clsParametros cMensaje = new clsParametros();
                    cMensaje.Id = 0;
                    cMensaje.Message = "Parametro Nul: ID del Hotel";
                    cMensaje.Tipo = clsTipoError.Aplication;
                    cMensaje.Severity = clsSeveridad.Moderada;
                    cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                    cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                    ExceptionHandled.Publicar(cMensaje);
                    Limpiar(cMensaje, PageSource);
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Aplication;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
                Limpiar(cMensaje, PageSource);
            }
        }
        public void setFormularioHB(clsCache cCache, UserControl PageSource)
        {

            RadioButtonList rblFormasPago = (RadioButtonList)PageSource.FindControl("rblFormasPago");
            DropDownList ddlGenero = (DropDownList)PageSource.FindControl("ddlGenero");
            if (ddlGenero != null)
            {
                new csGenerales().sConsultaGeneros(ddlGenero, false);
            }

            DropDownList ddlFranquicia = (DropDownList)PageSource.FindControl("ddlFranquicia");
            if (ddlFranquicia != null)
            {

                DataTable dtFranquicias = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFranquicias", new string[2] { cCache.Empresa, "HINTER" });
                if (dtFranquicias != null)
                {
                    ddlFranquicia.DataSource = dtFranquicias;
                    ddlFranquicia.DataTextField = "strDescripcion";
                    ddlFranquicia.DataValueField = "strcodFranquicia";
                    ddlFranquicia.DataBind();
                }
                else
                {
                    ddlFranquicia.Items.Add(new ListItem("Sin Franquicias", "-1"));
                }

            }

            if (rblFormasPago != null)
            {
                DataTable dtFOP = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFOPEmpresa", new string[3] { cCache.Empresa, cCache.Idioma, "HINTER" });
                rblFormasPago.DataSource = dtFOP;
                rblFormasPago.DataTextField = "strdescripcion";
                rblFormasPago.DataValueField = "strCodigo";
                rblFormasPago.DataBind();

            }

            DataTable dtDataDoc = new CsConsultasVuelos().SPConsultaTabla("SPConsultaTpoidentifica", new string[1] { new csCache().cCache().Idioma });
            DropDownList ddlTipoDoc = PageSource.FindControl("ddlTipoDoc") as DropDownList;
            if (ddlTipoDoc != null)
            {
                if (dtDataDoc != null)
                {
                    if (clsValidaciones.GetKeyOrAdd("ValorBlancoDoc", "False").ToUpper().Equals("TRUE"))
                        clsControls.LlenaControl(ddlTipoDoc, dtDataDoc, "STRDESCRIPCION", "INTCODE", true);
                    else
                        clsControls.LlenaControl(ddlTipoDoc, dtDataDoc, "STRDESCRIPCION", "INTCODE", false);

                }

            }

            clsResultados dsResultado = new clsResultados();
            clsServiceAddHB cHotel = new clsServiceAddHB();
            csHoteles cHoteles = new csHoteles();
            try
            {
                List<VO_Passenger> cPassenger = clsSesiones.getPassenger();
                if (PageSource.Request.QueryString["ID"] != null)
                {
                    dsResultado = cHotel.getServices(PageSource.Request.QueryString["ID"].ToString());

                    if (dsResultado.Error.Id != 0)
                    {
                        try
                        {
                            DataTable dtHotelRoom = dsResultado.dsResultados.Tables["HotelRoom"];
                            DataTable dtHotelOccupancy = dsResultado.dsResultados.Tables["HotelOccupancy"];
                            DataTable dtPrueba = new DataTable();
                            Repeater rptDatosReserva = (Repeater)PageSource.FindControl("rptDatosReserva");
                            Repeater RptPenalizacionGara = (Repeater)PageSource.FindControl("RptPenalizacionGara");
                            String Val1;
                            int val2 = 0;

                            if (dtHotelOccupancy.Rows.Count > 1)
                            {
                                {
                                }

                                rptDatosReserva.DataSource = dtHotelRoom;
                                rptDatosReserva.DataBind();
                            }
                            else
                            {
                                foreach (DataRow row in dtHotelOccupancy.Rows)
                                {
                                    Val1 = row["RoomCount"].ToString();
                                    val2 = Convert.ToInt32(Val1);
                                }

                                dtPrueba.Columns.Add("Proof");

                                for (int i = 0; i < val2; i++)
                                {
                                    DataRow dr2 = dtPrueba.NewRow();
                                    dr2["Proof"] = "1";
                                    dtPrueba.Rows.Add(dr2);
                                }

                                for (int j = 0; j < val2; j++)
                                {

                                    rptDatosReserva.DataSource = dtPrueba;
                                    rptDatosReserva.DataBind();
                                }

                            }
                            if (RptPenalizacionGara != null)
                            {
                                RptPenalizacionGara.DataSource = dtHotelRoom;
                                RptPenalizacionGara.DataBind();
                            }
                        }
                        catch (Exception e)
                        {
                        }

                        if (PageSource.Request.QueryString["RECORD"] == null)
                        {
                            cHoteles.setDatosHuespedes(PageSource, cPassenger.Count, cCache, 1);
                            cHoteles.setDetalleConfirma(PageSource, false, true);
                        }
                    }
                    else
                    {
                        LimpiarError(dsResultado.Error, PageSource);
                    }
                }
                else
                {
                    cHoteles.setDatosHuespedes(PageSource, cPassenger.Count, cCache, 1);
                    cHoteles.setDetalleConfirma(PageSource, false, true);
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Aplication;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
                LimpiarError(cMensaje, PageSource);
            }
        }
        public clsParametros setCrearNoRegistroHotelInter(UserControl parent, Enum_Login eLigin, bool bFacturacion, bool Enviacorreo)
        {
            ExceptionHandled.Publicar("***///////-- INICIO DE LA CREACION DEL USUARIO & SESION: " + clsSesiones.getSesionID() + " --/////");
            clsParametros cParametros = new clsParametros();

            csGeneralsPag.Idioma(parent);
            HiddenField hdfContactofactura = (HiddenField)parent.FindControl("hdfContactofactura");
            Label lblError1 = parent.FindControl("lblError1") as Label;
            String strClave = "";
            TextBox txtCiudad = (TextBox)parent.FindControl("txtCiudad");
            //TextBox txtApellido = null;
            //TextBox txtNombre = null;
            //TextBox txtEdad1 = null;
            TextBox txtMailPersonal = null;
            //DropDownList ddlTipoIdentificaion = null;
            TextBox txtCelular = null;
            //DropDownList ddlGenero = null;
            //TextBox txtDocumento = null;
            string sIdioma = clsSesiones.getIdioma();
            TextBox txtTelefono = null;



            if (sIdioma.Equals(""))
                sIdioma = clsValidaciones.GetKeyOrAdd("sIdioma", "es");
            Repeater rptDatosReserva = (Repeater)parent.FindControl("rptDatosreserva");

            //try
            //{
            //    txtNombre = (TextBox)parent.FindControl("txtNombre1");
            //    txtApellido = (TextBox)parent.FindControl("txtApellido1");
            //    ddlGenero = (DropDownList)parent.FindControl("ddlGenero");
            //    txtEdad1 = (TextBox)parent.FindControl("txtEdad1");
            //    ddlTipoIdentificaion = (DropDownList)parent.FindControl("ddlTipoDoc");
            //    txtDocumento = (TextBox)parent.FindControl("txtIdentificacion");
            //    txtMailPersonal = (TextBox)parent.FindControl("txtMailPersonal");
            //    txtTelefono = (TextBox)parent.FindControl("txtTelefono");
            //    txtCelular = (TextBox)parent.FindControl("txtCelular");

            //}
            //catch
            //{

            //}
            foreach (RepeaterItem item in rptDatosReserva.Items)
            {
                TextBox txtNombre = (TextBox)item.FindControl("txtNombre1");
                TextBox txtApellido = (TextBox)item.FindControl("txtApellido1");
                DropDownList ddlgenero = (DropDownList)item.FindControl("ddlgenero");
                TextBox txtEdad1 = (TextBox)item.FindControl("txtEdad1");
                DropDownList ddlTipoIdentificaion = (DropDownList)item.FindControl("ddlTipoDoc");
                TextBox txtDocumento = (TextBox)item.FindControl("txtDocumento1");
                //DropDownList ddlTrato1 = (DropDownList)item.FindControl("ddlTrato1");
                txtMailPersonal = (TextBox)parent.FindControl("txtMailPersonal");
                txtTelefono = (TextBox)parent.FindControl("txtTelefono");
                txtCelular = (TextBox)parent.FindControl("txtCelular");


                TextBox txtDireccion = (TextBox)parent.FindControl("txtDireccion");
                CheckBox chkCondicionesRegistro = parent.FindControl("chkCondicionesRegistro") as CheckBox;
                Label lblTelf = parent.FindControl("lblTelf") as Label;
                Label lblCiudadR = parent.FindControl("lblCiudadR") as Label;
                Label lblCiudadR1 = parent.FindControl("lblCiudadR1") as Label;
                Label lblTCelular = parent.FindControl("lblTCelular") as Label;
                Label lblTCelular1 = parent.FindControl("lblTCelular1") as Label;


                Enviacorreo = true;

                cParametros.Id = 1;

                try
                {


                    lblError1.Text = string.Empty;
                    string strTelefono = string.Empty;
                    txtCiudad.Visible = true;
                    if (lblCiudadR != null)
                        lblCiudadR.Visible = true;

                    if (lblCiudadR1 != null)
                        lblCiudadR1.Visible = true;

                    txtTelefono.Visible = true;
                    if (lblTelf != null)
                        lblTelf.Visible = true;

                    txtCelular.Visible = true;
                    lblTCelular.Visible = true;

                    if (lblTCelular1 != null)
                        lblTCelular1.Visible = true;

                    strClave = clsValidaciones.GetKeyOrAdd("ClaveDefectoUsuario", "GooCanCun2014");

                    /*Validamos que exista la llave idEmpresa para identificar si es una aplicacion corporaivo o cliente final*/
                    string Empresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "0");

                    try
                    {
                        clsCache cCache = new csCache().cCache();
                        if (cCache != null)
                        {
                            if (cCache.Empresa != "" && cCache.Empresa != "0")
                                Empresa = cCache.Empresa;
                            else
                                cCache.Empresa = Empresa;

                        }
                    }
                    catch (Exception)
                    {
                    }


                    string valida = "";
                    DataTable dt = new CsConsultasVuelos().SPConsultaTabla("SPValidaUsuarioFinal", new string[4] { txtMailPersonal.Text, clsSesiones.getAplicacion().ToString(), Empresa, clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF") });
                    if (dt != null)
                    {
                        valida = dt.Rows[0]["strpassword"].ToString();
                    }

                    if (valida != null && valida != "")
                    {
                        HttpContext.Current.Session["enviacorreo"] = "false";
                    }
                    else
                    {
                        string StrNivel = new CsConsultasVuelos().ConsultaCodigo(clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF"), "TBLNIVELUSUARIOS", "INTCODE", "REFERETIPOUSUARIO");
                        string StrCiudad = new CsConsultasVuelos().EjecutaProcedimiento("SPConsultaCiudadNombre", new string[2] { "'" + txtCiudad.Text.Trim() + "'", "'" + sIdioma + "'" });
                        if (StrCiudad == "")
                        {
                            StrCiudad = "1";
                        }
                        string strUbicacion = "'Ning'";
                        string bit = new CsConsultasVuelos().EjecutarSPConsulta("SPCreausuario", new string[17] { clsSesiones.getAplicacion().ToString(), StrNivel, Empresa, "0", ddlTipoIdentificaion.SelectedItem.Value, "'" + txtDocumento.Text + "'", "'" + txtNombre.Text + "'", "'" + txtApellido.Text + "'", ddlgenero.SelectedItem.Value, "'" + txtEdad1.Text + "'", strUbicacion, "'" + StrCiudad + "'", "'" + txtTelefono.Text.Trim() + "'", "'" + txtCelular.Text.Trim() + "'", "'" + txtMailPersonal.Text + "'", "'" + strClave + "'", "0" });
                        cParametros.Id = Convert.ToInt32(bit);
                        HttpContext.Current.Session["enviacorreo"] = "true";
                    }

                    if (cParametros.Id.Equals(1))
                    {

                        if (eLigin != Enum_Login.LoginCarro)
                        {
                            if (HttpContext.Current.Session["enviacorreo"] != null)
                            {
                                Enviacorreo = bool.Parse(HttpContext.Current.Session["enviacorreo"].ToString());
                                HttpContext.Current.Session["enviacorreo"] = null;
                            }
                            cParametros = new csResultadoVuelos().setLogin(txtMailPersonal.Text, strClave, Enviacorreo, parent, eLigin, true);
                        }
                    }
                    else
                    {
                        if (cParametros.ViewMessage.Count > 0)
                        {
                            lblError1.Text = cParametros.ViewMessage[0];
                            if (cParametros.Sugerencia.Count > 0)
                                lblError1.Text += ". " + cParametros.Sugerencia[0];
                        }
                        else
                        {
                            lblError1.Text = "Ya existe un usuario registrado con ese e-mail";
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
                    cParametros.Complemento = "setCrearNoRegistro; no se pudo crear o consultar el usuario";
                    ExceptionHandled.Publicar(cParametros);
                }
                ExceptionHandled.Publicar("**//-- FIN DE LA CREACION DEL USUARIO & SESION: " + clsSesiones.getSesionID() + " --//");
            }
            return cParametros;

        }
        public void Facilidades(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem != null && e.Item.HasControls())
            {
                Label lblValue = e.Item.FindControl("lblValue") as Label;

                if (lblValue != null)
                {
                    DataRowView vistaFila = e.Item.DataItem as DataRowView;

                    if (vistaFila.Row["value"].ToString().Equals("0"))
                        lblValue.Text = "";
                    else
                        lblValue.Text = " - No incluido";
                }
            }
        }
        #endregion
        #region HotelesCotelco
        private void setResultadosCotelcoFiler(UserControl PageSource, string sWhere)
        {
            //clsParametros cParametros = new clsParametros();
            //clsResultados cResultados = new clsResultados();
            //try
            //{
            //    csGeneralsPag.Idioma(PageSource);
            //    clsCache cCache = new csCache().cCache();
            //    if (cCache != null)
            //    {
            //        clsHotelValuedAvail cHotel = new clsHotelValuedAvail();
            //        cResultados = cHotel.getServices();
            //        if (cResultados.Error.Id.Equals(0))
            //        {
            //            new csCache().setError(PageSource, cResultados.Error);
            //        }
            //        else
            //        {
            //            if (sWhere != null && sWhere != "0")
            //                setResultadoHotelFilterWhere(PageSource, sWhere);
            //            else
            //                setResultadosCotelco(PageSource);
            //        }
            //    }
            //    else
            //    {
            //        csGeneralsPag.FinSesion();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    cParametros.Id = 0;
            //    cParametros.Message = Ex.Message.ToString();
            //    cParametros.Source = Ex.Source.ToString();
            //    cParametros.Tipo = clsTipoError.Library;
            //    cParametros.Severity = clsSeveridad.Moderada;
            //    cParametros.StackTrace = Ex.StackTrace.ToString();
            //    cParametros.Complemento = "Resultado Hoteles";
            //    cParametros.ViewMessage.Add("Su sesion ha terminado");
            //    cParametros.Sugerencia.Add("Por favor confirme con el administrador");
            //    ExceptionHandled.Publicar(cParametros);
            //    cResultados.Error = cParametros;

            //}
        }
        private void setSubirCotelco(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            try
            {
                csGeneralsPag.Idioma(PageSource);
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    Label lblReserva = (Label)PageSource.FindControl("lblReserva");

                    if (cResultados.Error.Id.Equals(0))
                    {
                        lblReserva.Text = cResultados.Error.ViewMessage[0].ToString() + ". " + cResultados.Error.Sugerencia[0].ToString();
                    }
                    else
                    {
                        lblReserva.Text = "Record de la Reserva " + cResultados.Error.DatoAdic.ToString();

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
                cParametros.Complemento = "Referencia de Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        #endregion
        private void Limpiar(clsParametros sMensaje, UserControl PageSource)
        {
            try
            {
                Repeater rptHotel = (Repeater)PageSource.FindControl("rptHotel");
                Repeater RptPagina = (Repeater)PageSource.FindControl("RptPagina");
                Button btnBack = (Button)PageSource.FindControl("btnBack");
                Button btnNext = (Button)PageSource.FindControl("btnNext");
                Panel pError = (Panel)PageSource.FindControl("pError");

                if (rptHotel != null)
                {
                    rptHotel.DataSource = null;
                    rptHotel.DataBind();
                }
                if (RptPagina != null)
                {
                    RptPagina.DataSource = null;
                    RptPagina.DataBind();
                }
                if (btnBack != null)
                {
                    btnBack.Enabled = false;
                }
                if (btnNext != null)
                {
                    btnNext.Enabled = false;
                }
                if (pError != null)
                {
                    clsErrorMensaje cError = new clsErrorMensaje();
                    cError.getError(sMensaje, pError);
                }
                else
                {
                    new csCache().setError(PageSource, sMensaje);
                }
            }
            catch { }
        }
        private void LimpiarError(clsParametros sMensaje, UserControl PageSource)
        {
            Panel pError = (Panel)PageSource.FindControl("pError");

            clsErrorMensaje cError = new clsErrorMensaje();
            cError.getError(sMensaje, pError);
        }
        public bool SetCrearAfiliadosHoteles(UserControl PageSource, int intContacto)
        {
            bool bValida = true;
            string registro = "0";
            string sCodigo = string.Empty;
            int CantAfiliados = -1;
            Label lblError = (Label)PageSource.FindControl("lblerror");

            try
            {
                CantAfiliados = Convert.ToInt32(new CsConsultasVuelos().ConsultaCodigo(intContacto.ToString(), "tblAfiliados", "Count(intAfiliado)", "intUsuario"));
                int sCantidadAfiliados = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("sCantidadAfiliados", "5"));
                if (CantAfiliados != -1)
                {
                    TextBox txtNombre = (TextBox)PageSource.FindControl("txtNombre");
                    TextBox txtApellido = (TextBox)PageSource.FindControl("txtApellido");
                    TextBox txtDocumentosid = (TextBox)PageSource.FindControl("txtDocumentosid");
                    TextBox txtMailPersonal = (TextBox)PageSource.FindControl("txtMailAfiliado");
                    TextBox txtTelefono = (TextBox)PageSource.FindControl("txtTelfonoAfiliado");
                    TextBox txtFnacimiento = (TextBox)PageSource.FindControl("txtEdad");
                    DropDownList ddlTpoDocumentoR = (DropDownList)PageSource.FindControl("ddlTpoDocumentoR");
                    DropDownList ddlGeneroR = (DropDownList)PageSource.FindControl("ddlGeneroR");
                    string strFecha = DateTime.Today.ToString("yyyy/MM/dd");
                    string fechaNaci = clsValidaciones.ConverMDYtoYMD(txtFnacimiento.Text, "/");

                    sCodigo = new CsConsultasVuelos().ConsultaCodigo(txtDocumentosid.Text, "tblAfiliados", "strIdentificacion", "strIdentificacion");
                    if (sCodigo != null && sCodigo != "")
                    {

                        lblError.Text = "Este usuario ya se encuentra registrado";
                    }
                    else
                    {
                        registro = new CsConsultasVuelos().EjecutarSPConsulta("SPCreaAfiliado", new string[10] { intContacto.ToString(), "'" + ddlTpoDocumentoR.SelectedItem.Value.ToString() + "'", "'" + txtDocumentosid.Text + "'", "'" + txtNombre.Text + "'", "'" + txtApellido.Text + "'", ddlGeneroR.SelectedItem.Value.ToString(), "'" + fechaNaci + "'", "'" + txtTelefono.Text + "'", "'" + txtMailPersonal.Text + "'", "'" + strFecha + "'" });
                    }
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtDocumentosid.Text = "";
                    txtMailPersonal.Text = "";
                    txtTelefono.Text = "";
                    txtFnacimiento.Text = "";

                    if (registro == "0")
                    {
                        bValida = false;

                    }

                }


            }
            catch { }

            return bValida;




        }
        public clsParametros CancelarReservaTT(List<string> sReserva)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                clsPurchaseCancel cCancel = new clsPurchaseCancel();
                VO_PurchaseReference lCancel = new VO_PurchaseReference();

                clsResultados sResult = cCancel.getServices(lCancel);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "WebServices ";
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        public void setCargar(UserControl PageSource)
        {
            try
            {
                setPaginar(PageSource, 1, null, null);
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
                cParametros.Complemento = "Buscador hoteles ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }


    }
}
