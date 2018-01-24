﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Ssoft.ValueObjects;
using WS_SsoftSabre.OTA_AirLowFareSearch;
using WS_SsoftSabre.OTA_AirAvail;
using WS_SsoftSabre.OTA_AirBook;
using WS_SsoftSabre.OTA_AirPrice;
using WS_SsoftSabre.ShortSell;
using WS_SsoftSabre.Air;
using WS_SsoftSabre.DisplayPriceQuote;
using EndTransactionRQ = WS_SsoftSabre.EndTransactionRQ;
using Ssoft.Utils;
using Ssoft.Rules.Reservas;
using Ssoft.Rules.Pagina;
using Ssoft.ManejadorExcepciones;
using Ssoft.Rules.Administrador;
using Ssoft.Rules.Generales;
using clsPassenger = Ssoft.ValueObjects.VO_Passenger;
using clsEdad = Ssoft.ValueObjects.clsEdad;
using WS_SsoftSabre.Utilidades;
using WS_SsoftSabre.General;
using WS_SsoftSabre.OTA_TravelItineraryRead;
using WS_SsoftSabre.OTA_Cancel;
using Ssoft.Rules.Corporativo;
using Ssoft.Rules.WebServices;
using System.IO;
using System.Xml.Serialization;
using Ssoft.DataNet;
using WS_SsoftSabre.DesignatePrinterRQ;
using WS_SsoftSabre.AirTicketRQ;
using System.Text;
using System.Linq;
using SsoftQuery.Vuelos;

public class Negocios_WebService_OTA_AirLowFareSearch
{
    #region [ METODOS ]

    public Negocios_WebService_OTA_AirLowFareSearch()
    {
    }
    /*-------------BUSCAR--------------*/
    private OTA_AirLowFareSearchRS GetBusquedaGeneneral(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        /*METODO BASICO DE BUSQUEDA SABRE*/
        String USD = "0";
        /*OBTENEMOS VALOR DEL DOLAR*/
        USD = getValorUSD();
        /*PREPARAMOS EL OBJETO DE RETORNO DE SABRE*/
        OTA_AirLowFareSearchRS oOTA_AirLowFareSearchRS = new OTA_AirLowFareSearchRS();
        /*OBTENEMOS LOS DATOS RETORNADOS DE SABRE*/
        if (vo_OTA_AirLowFareSearchLLSRQ != null)
        {
            clsAirOTA_AirLowFareSearch test = new clsAirOTA_AirLowFareSearch();

            oOTA_AirLowFareSearchRS = (OTA_AirLowFareSearchRS)test.getBusqueda(vo_OTA_AirLowFareSearchLLSRQ);
        }
        return oOTA_AirLowFareSearchRS;
    }
    /// <summary>
    /// serach multiDestination 19 result
    /// hceron 23052012
    /// </summary>
    /// <param name="vo_OTA_AirLowFareSearchLLSRQ"></param>
    /// <returns></returns>
    private OTA_AirLowFareSearchRS GetBusquedaGeneneralMulti(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        /*METODO BASICO DE BUSQUEDA SABRE*/
        String USD = "0";
        /*OBTENEMOS VALOR DEL DOLAR*/
        USD = getValorUSD();
        /*PREPARAMOS EL OBJETO DE RETORNO DE SABRE*/
        OTA_AirLowFareSearchRS oOTA_AirLowFareSearchRS = new OTA_AirLowFareSearchRS();
        /*OBTENEMOS LOS DATOS RETORNADOS DE SABRE*/
        if (vo_OTA_AirLowFareSearchLLSRQ != null)
        {
            oOTA_AirLowFareSearchRS = new clsAirOTA_AirLowFareSearch().getBusqueda(vo_OTA_AirLowFareSearchLLSRQ);
        }
        return oOTA_AirLowFareSearchRS;
    }
    #region BFM
    /// <summary>
    /// get BFM
    /// </summary>
    /// <param name="vo_OTA_AirLowFareSearchLLSRQ"></param>
    /// <returns></returns>
    private WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRS GetBusquedaGeneneralMax(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        /*METODO BASICO DE BUSQUEDA SABRE*/
        String USD = "0";
        /*OBTENEMOS VALOR DEL DOLAR*/
        USD = getValorUSD();
        /*PREPARAMOS EL OBJETO DE RETORNO DE SABRE*/
        WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRS oOTA_AirLowFareSearchRS = new WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRS();
        /*OBTENEMOS LOS DATOS RETORNADOS DE SABRE*/
        if (vo_OTA_AirLowFareSearchLLSRQ != null)
        {
            clsAirOTA_AirLowFareSearch ObjeSearch = new clsAirOTA_AirLowFareSearch();

            oOTA_AirLowFareSearchRS = (WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRS)ObjeSearch.getBusquedaMax(vo_OTA_AirLowFareSearchLLSRQ);
        }
        return oOTA_AirLowFareSearchRS;
    }
    /// <summary>
    /// Para Efectos de BFM
    /// </summary>
    /// <param name="vo_OTA_AirBookRQ"></param>
    /// <returns></returns>
    public clsResultados GetDsVentaSabreAirHora(VO_OTA_AirBookRQ vo_OTA_AirBookRQ)
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsVentaSabreAir = new DataSet();
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        clsVuelos objVuelos = new clsVuelos();

        OTA_AirBookRS oOTA_AirBookRS = new OTA_AirBookRS();

        if (vo_OTA_AirBookRQ != null)
        {
            oOTA_AirBookRS = new clsOTA_AirBook().getItinerarioHora(vo_OTA_AirBookRQ);
        }

        try
        {
            if (oOTA_AirBookRS != null)
            {
                if (oOTA_AirBookRS.Success != null)
                {
                    /*CONVERTIMOS EL OBJETO EN UN DATASET*/
                    dsVentaSabreAir = clsEsquema.GetDatasetSabreAir(oOTA_AirBookRS);
                    //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                    if (dsVentaSabreAir != null && dsVentaSabreAir.Tables.Count > 0)
                    {
                        objResultados.dsResultados = dsVentaSabreAir;
                        objParametros.Id = 1;
                    }
                    else
                    {   /*SI EL DATASET ES NULLO O VACIO*/
                        objResultados.dsResultados = null;
                        objParametros.Id = 0;
                        objParametros.Message = "Dataset null o vacio";
                        objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                        objParametros.Severity = clsSeveridad.Media;
                    }
                }
                else
                {
                    /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                    objParametros.Id = 0;
                    objParametros.Tipo = clsTipoError.WebServices;
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objParametros.Code = oOTA_AirBookRS.Errors[0].Error.ErrorCode;
                    objParametros.Info = oOTA_AirBookRS.Errors[0].Error.ErrorInfo.Message;
                    objParametros.ValidaInfo = true;
                    objParametros.Message = oOTA_AirBookRS.Errors[0].Error.ErrorMessage;
                    objParametros.Severity = oOTA_AirBookRS.Errors[0].Error.Severity;
                    //objParametros.ViewMessage.Add(oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message);
                    objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                    ExceptionHandled.Publicar(objParametros);
                }
            }
            else
            {
                /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                objResultados.dsResultados = null;
                objParametros.Id = 0;
                objParametros.Message = "Hubo una excepcion tratando de convertir los resultados";
                objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objParametros.Severity = clsSeveridad.Alta;
                objParametros.Tipo = clsTipoError.WebServices;
                objParametros.ViewMessage.Add("No hay resultados para la busqueda");
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            objResultados.dsResultados = null;
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Complemento = "Hubo una excepcion tratando de convertir los resultados";
            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
            objParametros.Severity = clsSeveridad.Alta;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
        }
        if (objParametros.Id == 0)
        {
            objParametros.ErrorConfigura[0] = csReferencias.csEmpresa();
            objParametros.MessageBD = true;
            objParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        objResultados.Error = objParametros;

        return objResultados;
    }
    public bool setclsOTA_AirBook(RepeaterCommandEventArgs e, UserControl ucControl, HtmlGenericControl dPanel, int iTipo = 0)
    {
        clsResultados objResultados = new clsResultados();

        clsOTA_AirBook objAir = new clsOTA_AirBook();

        VO_OTA_AirBookRQ objData = new VO_OTA_AirBookRQ();
        VO_OrigenDestinationOption objDestination = new VO_OrigenDestinationOption();
        
        VO_AirItinerary voair = new VO_AirItinerary();
        Ssoft.Ssoft.ValueObjects.Vuelos.VO_OTA_AirRulesRQ objRule;
        DataTable dtFightsement = null;  
        DataTable tbllSegmentosError = null;
        int intCantidadPax = 0;
        try
        {
            string intIdItinerario = e.CommandArgument.ToString();
            string AirItinerary_Id = string.Empty;
            try
            {
                string sWhere = "SequenceNumber = " + e.CommandArgument.ToString();
                DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
                DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
                DataTable dtTablaRetornada = clsDataNet.dsDataWhere(sWhere, dtPricedItinerary);
                intIdItinerario = dtTablaRetornada.Rows[0]["PricedItinerary_Id"].ToString();// dtTablaRetornada.Rows[0]["PricedItinerary_Id"].ToString().Equals("0")==true? "1" : dtTablaRetornada.Rows[0]["PricedItinerary_Id"].ToString();

                sWhere = "PricedItinerary_Id=" + intIdItinerario;
                DataTable dtAirItinerary = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["AirItinerary"]);
                AirItinerary_Id = dtAirItinerary.Rows[0]["AirItinerary_Id"].ToString();

                sWhere = "AirItinerary_Id=" + AirItinerary_Id;
                DataTable dtOriginsDest = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["OriginDestinationOptions"]);
                string OriginIds = dtOriginsDest.Rows[0]["OriginDestinationOptions_Id"].ToString();

                sWhere = "  OriginDestinationOptions_Id=" + OriginIds;
                //Obtenemos el Id del OriginDestinationOption_Id
                DataTable dtOriginDest = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["OriginDestinationOption"]);
                //serach all the values OriginDestinationOption_Id
                int i = 0;
                string vlues = string.Empty;
                foreach (DataRow Item in dtOriginDest.Rows)
                {

                    if (i < 1)
                    {
                        vlues = " OriginDestinationOption_Id=" + Item["OriginDestinationOption_Id"];
                    }
                    else
                    {
                        vlues = vlues + "OR  OriginDestinationOption_Id=" + Item["OriginDestinationOption_Id"];
                    }
                    i = i + 1;
                }


                dtFightsement = clsDataNet.dsDataWhere(vlues, dsSabreAir.Tables["FlightSegment"]);





            }
            catch { }
            string strTipoPlan = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
            DataTable dtItinerario = new DataTable();
            dtItinerario = new clsVuelos().GetDtGetItinerario(intIdItinerario);
            objData.TipoWs = strTipoPlan;
            //itinerary from selected grid
            objDestination.IItinerary = Convert.ToInt16(e.CommandArgument.ToString());

            VO_AirItinerary Itinerary;
            objDestination.Lvo_AirItinerary = new List<VO_AirItinerary>();

            DataTable dtPassengerTypeQuantity = new clsVuelos().GetDtPassengerTypeQuantity(intIdItinerario);
            
            for (int k = 0; k < dtPassengerTypeQuantity.Rows.Count; k++)
            {
                intCantidadPax += Convert.ToInt32(dtPassengerTypeQuantity.Rows[k]["Quantity"]);
            }

            foreach (DataRow iRow in dtFightsement.Rows)
            {

                #region Validacion disponibilidad segmentos
                OTA_AirAvailRQ OTA_AirAvailRQ1 = new OTA_AirAvailRQ();
                OTA_AirAvailRS OTA_AirAvailRS1 = new OTA_AirAvailRS();

                OTA_AirAvailRQSpecificFlightInfo SpecificFlightInfo = new OTA_AirAvailRQSpecificFlightInfo();
                OTA_AirAvailRQSpecificFlightInfoFlightNumber FlightNumber = new OTA_AirAvailRQSpecificFlightInfoFlightNumber();
                FlightNumber.Number = iRow["FlightNumber"].ToString();
                OTA_AirAvailRQSpecificFlightInfoAirline sFIAirline = new OTA_AirAvailRQSpecificFlightInfoAirline();
                sFIAirline.Code = iRow["strMarketingAirline"].ToString();
                OTA_AirAvailRQSpecificFlightInfoBookingClassPref BookingClassPref = new OTA_AirAvailRQSpecificFlightInfoBookingClassPref();
                BookingClassPref.ResBookDesigCode = iRow["strMarketingCabin"].ToString();

                SpecificFlightInfo.Airline = sFIAirline;
                SpecificFlightInfo.FlightNumber = FlightNumber;
                SpecificFlightInfo.BookingClassPref = BookingClassPref;

                OTA_AirAvailRQSpecificFlightInfoTPA_Extensions SFITPA = new OTA_AirAvailRQSpecificFlightInfoTPA_Extensions();
                OTA_AirAvailRQSpecificFlightInfoTPA_ExtensionsDepartureDateTime SFITPADepDateTime = new OTA_AirAvailRQSpecificFlightInfoTPA_ExtensionsDepartureDateTime();
                SFITPADepDateTime.DateTime = Convert.ToDateTime(iRow["dtmFechaSalida"].ToString()).ToString(clsValidaciones.GetKeyOrAdd("FormatoFechaSabre"));
                SFITPA.DepartureDateTime = SFITPADepDateTime;

                OTA_AirAvailRQSpecificFlightInfoTPA_ExtensionsOriginLocation SFITPAOriginLoc = new OTA_AirAvailRQSpecificFlightInfoTPA_ExtensionsOriginLocation();
                SFITPAOriginLoc.LocationCode = iRow["strDepartureAirport"].ToString(); ;
                SFITPAOriginLoc.CodeContext = iRow["strCodeContext"].ToString(); ;
                SFITPA.OriginLocation = SFITPAOriginLoc;

                OTA_AirAvailRQSpecificFlightInfoTPA_ExtensionsDestinationLocation SFITPADestinationLoc = new OTA_AirAvailRQSpecificFlightInfoTPA_ExtensionsDestinationLocation();
                SFITPADestinationLoc.LocationCode = iRow["strArrivalAirport"].ToString(); ;
                SFITPADestinationLoc.CodeContext = iRow["strCodeContext"].ToString(); ;
                SFITPA.DestinationLocation = SFITPADestinationLoc;

                SpecificFlightInfo.TPA_Extensions = SFITPA;
                OTA_AirAvailRQ1.SpecificFlightInfo = SpecificFlightInfo;

                OTA_AirAvailRQTravelPreferences OAATravelPref = new OTA_AirAvailRQTravelPreferences();
                OTA_AirAvailRQTravelPreferencesTPA_Extensions OAATravelPrefTPAExt = new OTA_AirAvailRQTravelPreferencesTPA_Extensions();
                OTA_AirAvailRQTravelPreferencesTPA_ExtensionsDirectAccess OAATravelPrefTPAExtDirect = new OTA_AirAvailRQTravelPreferencesTPA_ExtensionsDirectAccess();
                OAATravelPrefTPAExtDirect.Ind = true;
                OAATravelPrefTPAExt.DirectAccess = OAATravelPrefTPAExtDirect;
                OAATravelPref.TPA_Extensions = OAATravelPrefTPAExt;
                OTA_AirAvailRQ1.TravelPreferences = OAATravelPref;

                //clsSabreBase.SessionCreateRQ();
                OTA_AirAvailService OAAService = new OTA_AirAvailService();
                OAAService.MessageHeaderValue = clsSabreBase.__ISabre_OTA_AirAvailLLSRQ();

                OTA_AirAvailRQPOS pos = new OTA_AirAvailRQPOS();
                OTA_AirAvailRQPOSSource source = new OTA_AirAvailRQPOSSource();
                Ssoft.ValueObjects.VO_Credentials objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();
                source.PseudoCityCode = objvo_Credentials.Ipcc;
                pos.Source = source;
                OTA_AirAvailRQ1.POS = pos;
                OTA_AirAvailRQ1.Version = "1.9.1";	// Specify the service version

                WS_SsoftSabre.OTA_AirAvail.Security Seguridad_ = new WS_SsoftSabre.OTA_AirAvail.Security();
                Seguridad_.BinarySecurityToken = AutenticacionSabre.GET_SabreSession();
                OAAService.SecurityValue = Seguridad_;

                OTA_AirAvailRS1 = OAAService.OTA_AirAvailRQ(OTA_AirAvailRQ1);

                if (clsValidaciones.GetKeyOrAdd("bEscribirXMLAvailRQ", "False").ToUpper().Equals("TRUE"))
                {
                    try
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(OTA_AirAvailRQ1.GetType());
                        TextWriter textWriter = (TextWriter)new StreamWriter(clsValidaciones.GetKeyOrAdd("sRutaEscribirXMLAvailRQ", "C:/") + "OTA_AirAvailRQ.xml");
                        xmlSerializer.Serialize(textWriter, (object)OTA_AirAvailRQ1);
                        textWriter.Close();
                        xmlSerializer = new XmlSerializer(OAAService.MessageHeaderValue.GetType());
                        textWriter = (TextWriter)new StreamWriter(clsValidaciones.GetKeyOrAdd("sRutaEscribirXMLAvailRQ", "C:/") + "OTA_AirAvailServiceMessageHeader.xml");
                        xmlSerializer.Serialize(textWriter, (object)OAAService.MessageHeaderValue);
                        textWriter.Close();
                        xmlSerializer = new XmlSerializer(OTA_AirAvailRS1.GetType());
                        textWriter = (TextWriter)new StreamWriter(clsValidaciones.GetKeyOrAdd("sRutaEscribirXMLAvailRQ", "C:/") + "OTA_AirAvailRS.xml");
                        xmlSerializer.Serialize(textWriter, (object)OTA_AirAvailRS1);
                        textWriter.Close();
                    }
                    catch
                    {
                        ExceptionHandled.Publicar("/////////////*********************** La ruta de escritura del XML AirAvail no es valida");
                    }
                }

                if (OTA_AirAvailRS1.OriginDestinationOptions == null || OTA_AirAvailRS1.OriginDestinationOptions.Length == 0)
                {
                    tbllSegmentosError.Rows.Add(iRow.ItemArray);
                }
                else
                {
                    bool bCoincidencia = false;
                    foreach (OTA_AirAvailRSOriginDestinationOption RSODOptions in OTA_AirAvailRS1.OriginDestinationOptions)
                    {


                        bool bCoincideClase = false;
                        int iCoincidencias = 0;
                        OTA_AirAvailRSOriginDestinationOptionFlightSegment[] Segments = RSODOptions.FlightSegment;
                        foreach (OTA_AirAvailRSOriginDestinationOptionFlightSegment Segment in Segments)
                        {
                            OTA_AirAvailRSOriginDestinationOptionFlightSegmentBookingClassAvail[] Clases = Segment.BookingClassAvail;
                            foreach (OTA_AirAvailRSOriginDestinationOptionFlightSegmentBookingClassAvail Clase in Clases)
                            {
                                int iCuposDisp = Convert.ToInt32(Clase.Availability);
                                if (Clase.ResBookDesigCode.Equals(iRow["strMarketingCabin"].ToString()) && iCuposDisp >= intCantidadPax)
                                    bCoincideClase = true;
                            }

                            if (bCoincideClase)
                                iCoincidencias++;
                        }
                        if (iCoincidencias == Segments.Length)
                            bCoincidencia = true;
                    }
                    if (!bCoincidencia)
                        tbllSegmentosError.Rows.Add(iRow.ItemArray);
                }
                #endregion


                Itinerary = new VO_AirItinerary();
                Itinerary.BAirBook = true;
                Itinerary = getAirItinerary(objDestination.IItinerary);

                Itinerary.SAirEquip = iRow["strEquipment"].ToString();
                Itinerary.SFechaSalida = Convert.ToDateTime(iRow["dtmFechaSalida"]).ToString(Constant.FORMATO_TIME_STAMP);

                Itinerary.SFechaLlegada = Convert.ToDateTime(iRow["dtmFechaLlegada"]).ToString(Constant.FORMATO_TIME_STAMP);
                Itinerary.SNroVuelo = iRow["FlightNumber"].ToString();
                Itinerary.SMarketingAirLine = iRow["strMarketingAirLine"].ToString();
                Itinerary.SOperatingAirLine = Itinerary.SMarketingAirLine;//iRow["strOperatingAirline"].ToString();
                Itinerary.SAirEquip = iRow["strEquipment"].ToString();
                Itinerary.SActionCode = "NN";

                Itinerary.SNroPassenger = Ssoft.Utils.clsSesiones.getNumeroPasajeros().ToString();// "1";//verificar
                Itinerary.BAirBook = true;
                Itinerary.SClase = iRow["ResBookDesigCode"].ToString();


                Itinerary.Vo_AeropuertoOrigen = new VO_Aeropuerto(iRow["strDepartureAirport"].ToString(), iRow["strAeropuerto_Salida"].ToString());

                //getAeropuerto(0, objDestination.IItinerary, 0);
                Itinerary.Vo_AeropuertoDestino = new VO_Aeropuerto(iRow["strArrivalAirport"].ToString(), iRow["strCiudad_Llegada"].ToString());// getAeropuerto(1, objDestination.IItinerary, 0);
                //objAro.SCodigo =;
                //objAro.SContexto = dtFlightS.Rows[0]["strCiudad_Llegada"].ToString();

                objDestination.Lvo_AirItinerary.Add(Itinerary);
            }


            //obetnermos el codetext para Airrule
            //hceron 15.01.2012
            string sBasicCode_Text = "";
            if (iTipo == 0) sBasicCode_Text = getCode_Text(intIdItinerario);
            //Keep the parameter to VO_OTA_AirRulesRQ
            //hceron 05/01/2013
            if (objDestination.Lvo_AirItinerary.Count > 0)
            {
                Itinerary = new VO_AirItinerary();
                Itinerary = objDestination.Lvo_AirItinerary[0];
                objRule = new Ssoft.Ssoft.ValueObjects.Vuelos.VO_OTA_AirRulesRQ(sBasicCode_Text, Itinerary.Vo_AeropuertoOrigen, Itinerary.Vo_AeropuertoDestino, Convert.ToDateTime(Itinerary.SFechaSalida));
                objRule.StrCodigoAerolinea = Itinerary.SMarketingAirLine;
                objRule.sRPH = null;// e.CommandArgument.ToString();//Selected Option of datalist
                HttpContext.Current.Session["$OtaRule"] = objRule;
            }

            objData.Lvo_OrigenDestinationOption = new List<VO_OrigenDestinationOption>() { objDestination };
            // objData. = strTipoPlan;
        }
        catch { }

        objAir.getItinerarioHora(objData);
        return true;


    }
    /// <summary>
    /// Overload parameter to giev the itinerary
    /// hceron
    /// </summary>
    /// <param name="e">Itinrery int</param>
    /// <param name="ucControl"></param>
    /// <param name="dPanel"></param>
    /// <returns></returns>
    public bool setclsOTA_AirBook(int e, UserControl ucControl, HtmlGenericControl dPanel,int iTipo=0)
    {
        clsResultados objResultados = new clsResultados();
        clsOTA_AirBook objAir = new clsOTA_AirBook();
        VO_OTA_AirBookRQ objData = new VO_OTA_AirBookRQ();
        VO_OrigenDestinationOption objDestination = new VO_OrigenDestinationOption();
        VO_AirItinerary voair = new VO_AirItinerary();
        Ssoft.Ssoft.ValueObjects.Vuelos.VO_OTA_AirRulesRQ objRule;
        string AirItinerary_Id = string.Empty;
        DataTable dtFightsement = null;
        DataTable tbllSegmentosError = null;
        int intCantidadPax = 0;

        try
        {
            string intIdItinerario = e.ToString();
            try
            {
                string sWhere = "SequenceNumber = " + e.ToString();
                DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
                DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
                DataTable dtTablaRetornada = clsDataNet.dsDataWhere(sWhere, dtPricedItinerary);
                intIdItinerario = dtTablaRetornada.Rows[0]["PricedItinerary_Id"].ToString();// dtTablaRetornada.Rows[0]["PricedItinerary_Id"].ToString().Equals("0")==true? "1" : dtTablaRetornada.Rows[0]["PricedItinerary_Id"].ToString();
                sWhere = "PricedItinerary_Id=" + intIdItinerario;
                DataTable dtAirItinerary = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["AirItinerary"]);
                AirItinerary_Id = dtAirItinerary.Rows[0]["AirItinerary_Id"].ToString();

                sWhere = "AirItinerary_Id=" + AirItinerary_Id;
                DataTable dtOriginsDest = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["OriginDestinationOptions"]);
                string OriginIds = dtOriginsDest.Rows[0]["OriginDestinationOptions_Id"].ToString();

                sWhere = "  OriginDestinationOptions_Id=" + OriginIds;
                //Obtenemos el Id del OriginDestinationOption_Id
                DataTable dtOriginDest = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["OriginDestinationOption"]);
                //serach all the values OriginDestinationOption_Id
                int i = 0;
                string vlues = string.Empty;
                foreach (DataRow Item in dtOriginDest.Rows)
                {

                    if (i < 1)
                    {
                        vlues = " OriginDestinationOption_Id=" + Item["OriginDestinationOption_Id"];
                    }
                    else
                    {
                        vlues = vlues + "OR  OriginDestinationOption_Id=" + Item["OriginDestinationOption_Id"];
                    }
                    i = i + 1;
                }


                dtFightsement = clsDataNet.dsDataWhere(vlues, dsSabreAir.Tables["FlightSegment"]);

            }
            catch { }
            string strTipoPlan = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
            DataTable dtItinerario = new DataTable();
            dtItinerario = new clsVuelos().GetDtGetItinerario(intIdItinerario);
            objData.TipoWs = strTipoPlan;
            //itinerary from selected grid after find
            objDestination.IItinerary = e;

            VO_AirItinerary Itinerary;
            objDestination.Lvo_AirItinerary = new List<VO_AirItinerary>();

            DataTable dtPassengerTypeQuantity = new clsVuelos().GetDtPassengerTypeQuantity(intIdItinerario);

            for (int k = 0; k < dtPassengerTypeQuantity.Rows.Count; k++)
            {
                intCantidadPax += Convert.ToInt32(dtPassengerTypeQuantity.Rows[k]["Quantity"]);
            }

            tbllSegmentosError = dtFightsement.Clone();

            foreach (DataRow iRow in dtFightsement.Rows)
            {


                #region Validacion disponibilidad segmentos
                OTA_AirAvailRQ OTA_AirAvailRQ1 = new OTA_AirAvailRQ();
                OTA_AirAvailRS OTA_AirAvailRS1 = new OTA_AirAvailRS();

                OTA_AirAvailRQSpecificFlightInfo SpecificFlightInfo = new OTA_AirAvailRQSpecificFlightInfo();
                OTA_AirAvailRQSpecificFlightInfoFlightNumber FlightNumber = new OTA_AirAvailRQSpecificFlightInfoFlightNumber();
                FlightNumber.Number = iRow["FlightNumber"].ToString();
                OTA_AirAvailRQSpecificFlightInfoAirline sFIAirline = new OTA_AirAvailRQSpecificFlightInfoAirline();
                sFIAirline.Code = iRow["strMarketingAirline"].ToString();
                OTA_AirAvailRQSpecificFlightInfoBookingClassPref BookingClassPref = new OTA_AirAvailRQSpecificFlightInfoBookingClassPref();
                BookingClassPref.ResBookDesigCode = iRow["strMarketingCabin"].ToString();

                SpecificFlightInfo.Airline = sFIAirline;
                SpecificFlightInfo.FlightNumber = FlightNumber;
                SpecificFlightInfo.BookingClassPref = BookingClassPref;

                OTA_AirAvailRQSpecificFlightInfoTPA_Extensions SFITPA = new OTA_AirAvailRQSpecificFlightInfoTPA_Extensions();
                OTA_AirAvailRQSpecificFlightInfoTPA_ExtensionsDepartureDateTime SFITPADepDateTime = new OTA_AirAvailRQSpecificFlightInfoTPA_ExtensionsDepartureDateTime();
                SFITPADepDateTime.DateTime = Convert.ToDateTime(iRow["dtmFechaSalida"].ToString()).ToString(clsValidaciones.GetKeyOrAdd("FormatoFechaSabre"));
                SFITPA.DepartureDateTime = SFITPADepDateTime;

                OTA_AirAvailRQSpecificFlightInfoTPA_ExtensionsOriginLocation SFITPAOriginLoc = new OTA_AirAvailRQSpecificFlightInfoTPA_ExtensionsOriginLocation();
                SFITPAOriginLoc.LocationCode = iRow["strDepartureAirport"].ToString(); ;
                SFITPAOriginLoc.CodeContext = iRow["strCodeContext"].ToString(); ;
                SFITPA.OriginLocation = SFITPAOriginLoc;

                OTA_AirAvailRQSpecificFlightInfoTPA_ExtensionsDestinationLocation SFITPADestinationLoc = new OTA_AirAvailRQSpecificFlightInfoTPA_ExtensionsDestinationLocation();
                SFITPADestinationLoc.LocationCode = iRow["strArrivalAirport"].ToString(); ;
                SFITPADestinationLoc.CodeContext = iRow["strCodeContext"].ToString(); ;
                SFITPA.DestinationLocation = SFITPADestinationLoc;

                SpecificFlightInfo.TPA_Extensions = SFITPA;
                OTA_AirAvailRQ1.SpecificFlightInfo = SpecificFlightInfo;

                OTA_AirAvailRQTravelPreferences OAATravelPref = new OTA_AirAvailRQTravelPreferences();
                OTA_AirAvailRQTravelPreferencesTPA_Extensions OAATravelPrefTPAExt = new OTA_AirAvailRQTravelPreferencesTPA_Extensions();
                OTA_AirAvailRQTravelPreferencesTPA_ExtensionsDirectAccess OAATravelPrefTPAExtDirect = new OTA_AirAvailRQTravelPreferencesTPA_ExtensionsDirectAccess();
                OAATravelPrefTPAExtDirect.Ind = true;
                OAATravelPrefTPAExt.DirectAccess = OAATravelPrefTPAExtDirect;
                OAATravelPref.TPA_Extensions = OAATravelPrefTPAExt;
                OTA_AirAvailRQ1.TravelPreferences = OAATravelPref;

                //clsSabreBase.SessionCreateRQ();
                OTA_AirAvailService OAAService = new OTA_AirAvailService();
                OAAService.MessageHeaderValue = clsSabreBase.__ISabre_OTA_AirAvailLLSRQ();

                OTA_AirAvailRQPOS pos = new OTA_AirAvailRQPOS();
                OTA_AirAvailRQPOSSource source = new OTA_AirAvailRQPOSSource();
                Ssoft.ValueObjects.VO_Credentials objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();
                source.PseudoCityCode = objvo_Credentials.Ipcc;
                pos.Source = source;
                OTA_AirAvailRQ1.POS = pos;
                OTA_AirAvailRQ1.Version = "1.9.1";	// Specify the service version

                WS_SsoftSabre.OTA_AirAvail.Security Seguridad_ = new WS_SsoftSabre.OTA_AirAvail.Security();
                Seguridad_.BinarySecurityToken = AutenticacionSabre.GET_SabreSession();
                OAAService.SecurityValue = Seguridad_;

                OTA_AirAvailRS1 = OAAService.OTA_AirAvailRQ(OTA_AirAvailRQ1);

                if (clsValidaciones.GetKeyOrAdd("bEscribirXMLAvailRQ", "False").ToUpper().Equals("TRUE"))
                {
                    try
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(OTA_AirAvailRQ1.GetType());
                        TextWriter textWriter = (TextWriter)new StreamWriter(clsValidaciones.GetKeyOrAdd("sRutaEscribirXMLAvailRQ", "C:/") + "OTA_AirAvailRQ.xml");
                        xmlSerializer.Serialize(textWriter, (object)OTA_AirAvailRQ1);
                        textWriter.Close();
                        xmlSerializer = new XmlSerializer(OAAService.MessageHeaderValue.GetType());
                        textWriter = (TextWriter)new StreamWriter(clsValidaciones.GetKeyOrAdd("sRutaEscribirXMLAvailRQ", "C:/") + "OTA_AirAvailServiceMessageHeader.xml");
                        xmlSerializer.Serialize(textWriter, (object)OAAService.MessageHeaderValue);
                        textWriter.Close();
                        xmlSerializer = new XmlSerializer(OTA_AirAvailRS1.GetType());
                        textWriter = (TextWriter)new StreamWriter(clsValidaciones.GetKeyOrAdd("sRutaEscribirXMLAvailRQ", "C:/") + "OTA_AirAvailRS.xml");
                        xmlSerializer.Serialize(textWriter, (object)OTA_AirAvailRS1);
                        textWriter.Close();
                    }
                    catch
                    {
                        ExceptionHandled.Publicar("/////////////*********************** La ruta de escritura del XML AirAvail no es valida");
                    }
                }

                if (OTA_AirAvailRS1.OriginDestinationOptions == null || OTA_AirAvailRS1.OriginDestinationOptions.Length == 0)
                {
                    tbllSegmentosError.Rows.Add(iRow.ItemArray);
                }
                else
                {
                    bool bCoincidencia = false;
                    foreach (OTA_AirAvailRSOriginDestinationOption RSODOptions in OTA_AirAvailRS1.OriginDestinationOptions)
                    {


                        bool bCoincideClase = false;
                        int iCoincidencias = 0;
                        OTA_AirAvailRSOriginDestinationOptionFlightSegment[] Segments = RSODOptions.FlightSegment;
                        foreach (OTA_AirAvailRSOriginDestinationOptionFlightSegment Segment in Segments)
                        {
                            OTA_AirAvailRSOriginDestinationOptionFlightSegmentBookingClassAvail[] Clases = Segment.BookingClassAvail;
                            foreach (OTA_AirAvailRSOriginDestinationOptionFlightSegmentBookingClassAvail Clase in Clases)
                            {
                                int iCuposDisp = Convert.ToInt32(Clase.Availability);
                                if (Clase.ResBookDesigCode.Equals(iRow["strMarketingCabin"].ToString()) && iCuposDisp >= intCantidadPax)
                                    bCoincideClase = true;
                            }

                            if (bCoincideClase)
                                iCoincidencias++;
                        }
                        if (iCoincidencias == Segments.Length)
                            bCoincidencia = true;
                    }
                    if (!bCoincidencia)
                        tbllSegmentosError.Rows.Add(iRow.ItemArray);
                }
                #endregion


                Itinerary = new VO_AirItinerary();
                Itinerary.BAirBook = true;
                Itinerary = getAirItinerary(objDestination.IItinerary);

                Itinerary.SAirEquip = iRow["strEquipment"].ToString();
                Itinerary.SFechaSalida = Convert.ToDateTime(iRow["dtmFechaSalida"]).ToString(Constant.FORMATO_TIME_STAMP);

                Itinerary.SFechaLlegada = Convert.ToDateTime(iRow["dtmFechaLlegada"]).ToString(Constant.FORMATO_TIME_STAMP);
                Itinerary.SNroVuelo = iRow["FlightNumber"].ToString();
                Itinerary.SMarketingAirLine = iRow["strMarketingAirLine"].ToString();
                Itinerary.SOperatingAirLine = Itinerary.SMarketingAirLine;//iRow["strOperatingAirline"].ToString();
                Itinerary.SAirEquip = iRow["strEquipment"].ToString();

                Itinerary.SActionCode = "NN";

                Itinerary.SNroPassenger = Ssoft.Utils.clsSesiones.getNumeroPasajeros().ToString();// "1";//verificar
                Itinerary.BAirBook = true;
                Itinerary.SClase = iRow["ResBookDesigCode"].ToString();


                Itinerary.Vo_AeropuertoOrigen = new VO_Aeropuerto(iRow["strDepartureAirport"].ToString(), iRow["strAeropuerto_Salida"].ToString());

                //getAeropuerto(0, objDestination.IItinerary, 0);
                Itinerary.Vo_AeropuertoDestino = new VO_Aeropuerto(iRow["strArrivalAirport"].ToString(), iRow["strCiudad_Llegada"].ToString());// getAeropuerto(1, objDestination.IItinerary, 0);
                //objAro.SCodigo =;
                //objAro.SContexto = dtFlightS.Rows[0]["strCiudad_Llegada"].ToString();

                objDestination.Lvo_AirItinerary.Add(Itinerary);
            }
            //obetnermos el codetext para Airrule
            //hceron 15.01.2012
            string sBasicCode_Text = "";
            if (iTipo==0) sBasicCode_Text = getCode_Text(intIdItinerario);
            //Keep the parameter to VO_OTA_AirRulesRQ
            //hceron 05/01/2013
            if (objDestination.Lvo_AirItinerary.Count > 0)
            {
                Itinerary = new VO_AirItinerary();
                Itinerary = objDestination.Lvo_AirItinerary[0];
                objRule = new Ssoft.Ssoft.ValueObjects.Vuelos.VO_OTA_AirRulesRQ(sBasicCode_Text, Itinerary.Vo_AeropuertoOrigen, Itinerary.Vo_AeropuertoDestino, Convert.ToDateTime(Itinerary.SFechaSalida));
                objRule.StrCodigoAerolinea = Itinerary.SMarketingAirLine;
                objRule.sRPH = null; ;//Selected Option of datalist
                // objRule.Vo_AeropuertoDestino = objDestination.Lvo_AirItinerary[objDestination.Lvo_AirItinerary.Count()].Vo_AeropuertoDestino;
                HttpContext.Current.Session["$OtaRule"] = objRule;
            }


            if (tbllSegmentosError.Rows.Count > 0)
            {
                ucControl.Session["$Tbl_SEgmentos_No_Disp"] = tbllSegmentosError;
            }
            else
            {
                ucControl.Session["$Tbl_SEgmentos_No_Disp"] = null;
            }

            objData.Lvo_OrigenDestinationOption = new List<VO_OrigenDestinationOption>() { objDestination };
            // objData. = strTipoPlan;
        }
        catch { }

        objAir.getItinerarioHora(objData);
        return true;


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dtItinerario"></param>
    /// <param name="iOrgenDestino"></param>
    /// <returns></returns>
    private VO_Aeropuerto getAeropuerto(int iOrgenDestino, int iTienerary, int iSegmnet)
    {
        //1 Segmneto deaprture =0
        //Segment Return=1
        VO_Aeropuerto objAro = new VO_Aeropuerto();
        DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
        string sWhere = "PricedItinerary_Id = " + iTienerary.ToString();
        //   DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
        string dtAirItId = string.Empty;
        string OriginDId = string.Empty;
        string OriginId = string.Empty;
        DataTable dtAirIt = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["AirItinerary"]);

        dtAirItId = dtAirIt.Rows[0]["AirItinerary_Id"].ToString();
        sWhere = "AirItinerary_Id = " + dtAirItId.ToString();

        //Obtenmos el OriginDestinationOptions id
        DataTable dtOriginDests = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["OriginDestinationOptions"]);
        OriginDId = dtOriginDests.Rows[0]["OriginDestinationOptions_Id"].ToString();
        sWhere = "OriginDestinationOptions_Id = " + OriginDId.ToString();

        //Obtenemos el Id del OriginDestinationOption_Id
        DataTable dtOriginDest = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["OriginDestinationOption"]);
        OriginId = dtOriginDest.Rows[0]["OriginDestinationOption_Id"].ToString();

        //Fill object 
        sWhere = "OriginDestinationOption_Id = " + OriginId.ToString();
        DataTable dtFlightS = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["FlightSegment"]);

        if (iOrgenDestino == 0)//Departure Air
        {
            objAro.SCodigo = dtFlightS.Rows[0]["strDepartureAirport"].ToString();
            objAro.SContexto = dtFlightS.Rows[0]["strAeropuerto_Salida"].ToString();
        }
        if (iOrgenDestino == 1)//Arrivale Air
        {
            objAro.SCodigo = dtFlightS.Rows[0]["strArrivalAirport"].ToString();
            objAro.SContexto = dtFlightS.Rows[0]["strCiudad_Llegada"].ToString();

        }


        return objAro;
    }
    private VO_AirItinerary getAirItinerary(int iTienerary)
    {
        //1 Segmneto deaprture =0
        //Segment Return=1
        VO_AirItinerary objAro = new VO_AirItinerary();

        DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
        string sWhere = "PricedItinerary_Id = " + iTienerary.ToString();
        //   DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
        string dtAirItId = string.Empty;
        string OriginDId = string.Empty;
        string OriginId = string.Empty;
        DataTable dtAirIt = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["AirItinerary"]);


        dtAirItId = dtAirIt.Rows[0]["AirItinerary_Id"].ToString();
        sWhere = "AirItinerary_Id = " + dtAirItId.ToString();

        //Obtenmos el OriginDestinationOptions id
        DataTable dtOriginDests = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["OriginDestinationOptions"]);
        OriginDId = dtOriginDests.Rows[0]["OriginDestinationOptions_Id"].ToString();
        sWhere = "OriginDestinationOptions_Id = " + OriginDId.ToString();

        //Obtenemos el Id del OriginDestinationOption_Id
        DataTable dtOriginDest = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["OriginDestinationOption"]);
        OriginId = dtOriginDest.Rows[0]["OriginDestinationOption_Id"].ToString();

        //Fill object 
        sWhere = "OriginDestinationOption_Id = " + OriginId.ToString();
        DataTable dtFlightS = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["FlightSegment"]);


        objAro.SAirEquip = dtFlightS.Rows[0]["strEquipment"].ToString();
        objAro.SFechaSalida = Convert.ToDateTime(dtFlightS.Rows[0]["dtmFechaSalida"]).ToString(Constant.FORMATO_TIME_STAMP);

        objAro.SFechaLlegada = Convert.ToDateTime(dtFlightS.Rows[0]["dtmFechaLlegada"]).ToString(Constant.FORMATO_TIME_STAMP);
        objAro.SNroVuelo = dtFlightS.Rows[0]["FlightNumber"].ToString();
        objAro.SOperatingAirLine = dtFlightS.Rows[0]["strOperatingAirline"].ToString();
        objAro.SAirEquip = dtFlightS.Rows[0]["strEquipment"].ToString();
        objAro.SMarketingAirLine = objAro.SOperatingAirLine;
        objAro.SActionCode = "NN";

        objAro.SNroPassenger = Ssoft.Utils.clsSesiones.getNumeroPasajeros().ToString();// "1";//verificar
        objAro.BAirBook = true;
        objAro.SClase = dtFlightS.Rows[0]["strClase"].ToString();
        //objAro.SNroPassenger = "1";//numero de sillas 

        return objAro;
    }
    /// <summary>
    /// Codigo del iteineraio para el Rule
    /// </summary>
    /// <param name="iTienerary"></param>
    /// <returns></returns>
    private string getCode_Text(string iTienerary)
    {
        string sCode = string.Empty;
        string Info_Id = string.Empty;
        string sdowns_Id = string.Empty;
        string sdown_Id = string.Empty;
        string sFareBasisCode_Id = string.Empty;
        DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();

        string sWhere = "PricedItinerary_Id = " + iTienerary.ToString();
        DataTable dtAirIt = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["AirItineraryPricingInfo"]);


        Info_Id = dtAirIt.Rows[0]["AirItineraryPricingInfo_Id"].ToString();
        sWhere = "AirItineraryPricingInfo_Id = " + Info_Id.ToString();
        DataTable dBreakdowns = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["PTC_FareBreakdowns"]);

        sdowns_Id = dBreakdowns.Rows[0]["PTC_FareBreakdowns_Id"].ToString();
        sWhere = "PTC_FareBreakdowns_Id = " + sdowns_Id.ToString();
        DataTable dBreakdown = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["PTC_FareBreakdown"]);

        sdown_Id = dBreakdown.Rows[0]["PTC_FareBreakdown_Id"].ToString();
        sWhere = "PTC_FareBreakdown_Id = " + sdown_Id.ToString();
        DataTable dBasisCodes = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["FareBasisCodes"]);

        sFareBasisCode_Id = dBasisCodes.Rows[0]["FareBasisCodes_Id"].ToString();
        sWhere = "FareBasisCodes_Id = " + sFareBasisCode_Id.ToString();
        DataTable dBasisCode = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["FareBasisCode"]);

        sCode = dBasisCode.Rows[0]["FareBasisCode_text"].ToString();


        return sCode;
    }
    #endregion
    private OTA_AirAvailRS GetBusquedaGeneneralHora(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        /*METODO BASICO DE BUSQUEDA SABRE*/
        String USD = "0";
        /*OBTENEMOS VALOR DEL DOLAR*/
        USD = getValorUSD();
        /*PREPARAMOS EL OBJETO DE RETORNO DE SABRE*/
        OTA_AirAvailRS oOTA_AirLowFareSearchRS = new OTA_AirAvailRS();
        /*OBTENEMOS LOS DATOS RETORNADOS DE SABRE*/
        if (vo_OTA_AirLowFareSearchLLSRQ != null)
        {
            oOTA_AirLowFareSearchRS = new clsOTA_AirAvail().getBusquedaHora(vo_OTA_AirLowFareSearchLLSRQ);
        }
        return oOTA_AirLowFareSearchRS;
    }
    public clsResultados GetDsVentaSabreAirHoraCommand(VO_OTA_AirBookRQ vo_OTA_AirBookRQ)
    {
        string strMensaje = string.Empty;
        string sVenta = string.Empty;
        string sItinerario = string.Empty;

        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        try
        {
            if (vo_OTA_AirBookRQ.IRutaActual.Equals(1))
                Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno("XIA");

            objParametros = new clsShortSell().getItinerarioHoraCommand(vo_OTA_AirBookRQ);

            if (objParametros.Id.Equals(1))
            {
                foreach (string sComand in objParametros.DatoAdicArr)
                {
                    sVenta = Negocios_WebServiceSabreCommand._EjecutarComando(sComand);
                    //sItinerario = Negocios_WebServiceSabreCommand._EjecutarComando("*I");
                    if (string.IsNullOrEmpty(sVenta))
                    {
                        objParametros.Id = 0;
                        objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                        objParametros.Sugerencia.Add("Por favor intente de nuevo");
                        objParametros.Severity = clsSeveridad.Media;
                        objParametros.Tipo = clsTipoError.WebServices;
                        objParametros.Complemento = "Comandos.  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                        ExceptionHandled.Publicar(objParametros);
                    }
                    else
                    {
                        if (sVenta.Trim().Contains("NO JOURNEY RECORD PRESENT"))
                        {
                            objParametros.Id = 0;
                            objParametros.Message = sVenta;
                            objParametros.ViewMessage.Add("No se encontraron vuelos disponibles.");
                            objParametros.Sugerencia.Add("Por favor intente de nuevo");
                            objParametros.Severity = clsSeveridad.Media;
                            objParametros.Tipo = clsTipoError.WebServices;
                            objParametros.Complemento = "Comandos.  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                            ExceptionHandled.Publicar(objParametros);
                        }
                        else
                        {
                            if (sVenta.Contains("UNABLE TO SELL SEGMENTS") || sVenta.Contains("ERROR"))
                            {
                                objParametros.Id = 0;
                                objParametros.Message = sVenta;
                                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles.");
                                objParametros.Sugerencia.Add("Por favor intente de nuevo");
                                objParametros.Severity = clsSeveridad.Media;
                                objParametros.Tipo = clsTipoError.WebServices;
                                objParametros.Complemento = "Comandos.  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                                ExceptionHandled.Publicar(objParametros);
                            }
                            else
                            {
                                if (sVenta.Contains("AVAIL EXPIRED"))
                                {
                                    objParametros.Id = 0;
                                    objParametros.Message = sVenta;
                                    objParametros.ViewMessage.Add("No se encontraron vuelos disponibles.");
                                    objParametros.Sugerencia.Add("Por favor intente de nuevo");
                                    objParametros.Severity = clsSeveridad.Media;
                                    objParametros.Tipo = clsTipoError.WebServices;
                                    objParametros.Complemento = "Comandos.  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                                    ExceptionHandled.Publicar(objParametros);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (objParametros.ViewMessage.Count.Equals(0))
                {
                    objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                    objParametros.Sugerencia.Add("Por favor intente de nuevo");
                }
                else
                {
                    objParametros.ViewMessage[0] = "No se encontraron vuelos disponibles";
                    objParametros.Sugerencia[0] = "Por favor intente de nuevo";
                }
                objParametros.Complemento = "Comandos.  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                objParametros.Severity = clsSeveridad.Media;
                objParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(objParametros);
            }
        }
        catch (Exception Ex)
        {
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (objParametros.ViewMessage.Count.Equals(0))
            {
                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                objParametros.Sugerencia.Add("Por favor intente de nuevo");
            }
            else
            {
                objParametros.ViewMessage[0] = "No se encontraron vuelos disponibles";
                objParametros.Sugerencia[0] = "Por favor intente de nuevo";
            }
            objParametros.Severity = clsSeveridad.Media;
            objParametros.Tipo = clsTipoError.WebServices;
            objParametros.Complemento = "Comandos.  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
            ExceptionHandled.Publicar(objParametros);
        }
        objResultados.Error = objParametros;
        return objResultados;
    }
    public clsResultados GetDsVentaSabreAirHora(string[] sRph)
    {
        string strMensaje = string.Empty;
        string sVenta = string.Empty;
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        try
        {
            if (sRph[0].Equals("1"))
                Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno("XIA");

            sVenta = Negocios_WebServiceSabreCommand._EjecutarComando("0" + sRph[2] + sRph[3] + sRph[1] + "*");
            if (string.IsNullOrEmpty(sVenta))
            {
                objParametros.Id = 0;
                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                objParametros.Sugerencia.Add("Por favor intente de nuevo");
                objParametros.Severity = clsSeveridad.Media;
                objParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(objParametros);
            }
            else
            {
                if (sVenta.Trim().Contains("NO JOURNEY RECORD PRESENT"))
                {
                    objParametros.Id = 0;
                    objParametros.Message = sVenta;
                    objParametros.ViewMessage.Add("No se encontraron vuelos disponibles.");
                    objParametros.Sugerencia.Add("Por favor intente de nuevo");
                    objParametros.Severity = clsSeveridad.Media;
                    objParametros.Tipo = clsTipoError.WebServices;
                    ExceptionHandled.Publicar(objParametros);
                }
                else
                {
                    if (sVenta.Contains("UNABLE TO SELL SEGMENTS") || sVenta.Contains("ERROR"))
                    {
                        objParametros.Id = 0;
                        objParametros.Message = sVenta;
                        objParametros.ViewMessage.Add("No se encontraron vuelos disponibles.");
                        objParametros.Sugerencia.Add("Por favor intente de nuevo");
                        objParametros.Severity = clsSeveridad.Media;
                        objParametros.Tipo = clsTipoError.WebServices;
                        ExceptionHandled.Publicar(objParametros);
                    }
                    else
                    {
                        if (sVenta.Contains("AVAIL EXPIRED"))
                        {
                            objParametros.Id = 0;
                            objParametros.Message = sVenta;
                            objParametros.ViewMessage.Add("No se encontraron vuelos disponibles.");
                            objParametros.Sugerencia.Add("Por favor intente de nuevo");
                            objParametros.Severity = clsSeveridad.Media;
                            objParametros.Tipo = clsTipoError.WebServices;
                            ExceptionHandled.Publicar(objParametros);
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Source = Ex.Source;
            objParametros.StackTrace = Ex.StackTrace;
            if (objParametros.ViewMessage.Count.Equals(0))
            {
                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                objParametros.Sugerencia.Add("Por favor intente de nuevo");
            }
            else
            {
                objParametros.ViewMessage[0] = "No se encontraron vuelos disponibles";
                objParametros.Sugerencia[0] = "Por favor intente de nuevo";
            }
            objParametros.Severity = clsSeveridad.Media;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
        }
        objResultados.Error = objParametros;
        return objResultados;
    }
    public clsResultados GetDsVentaSabreAirHoraSegmento(VO_OTA_AirBookRQ vo_OTA_AirBookRQ)
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsVentaSabreAir = new DataSet();
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        clsVuelos objVuelos = new clsVuelos();

        ShortSellRS oShortSellRS = new ShortSellRS();

        if (vo_OTA_AirBookRQ != null)
        {
            oShortSellRS = new clsShortSell().getItinerarioHora(vo_OTA_AirBookRQ);
        }

        try
        {
            if (oShortSellRS != null)
            {
                if (oShortSellRS.Success != null)
                {
                    /*CONVERTIMOS EL OBJETO EN UN DATASET*/
                    dsVentaSabreAir = clsEsquema.GetDatasetSabreAir(oShortSellRS);
                    //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                    if (dsVentaSabreAir != null && dsVentaSabreAir.Tables.Count > 0)
                    {
                        objResultados.dsResultados = dsVentaSabreAir;
                        objParametros.Id = 1;
                    }
                    else
                    {   /*SI EL DATASET ES NULLO O VACIO*/
                        objResultados.dsResultados = null;
                        objParametros.Id = 0;
                        objParametros.Message = "Dataset null o vacio";
                        objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                        objParametros.Severity = clsSeveridad.Media;
                    }
                }
                else
                {
                    /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                    objParametros.Id = 0;
                    objParametros.Tipo = clsTipoError.WebServices;
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objParametros.Code = oShortSellRS.Errors.Error.ErrorCode;
                    objParametros.Info = oShortSellRS.Errors.Error.ErrorInfo.Message;
                    objParametros.Message = oShortSellRS.Errors.Error.ErrorMessage;
                    objParametros.Severity = oShortSellRS.Errors.Error.Severity;
                    //objParametros.ViewMessage.Add(oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message);
                    objParametros.ValidaInfo = true;
                    objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                    ExceptionHandled.Publicar(objParametros);
                }
            }
            else
            {
                /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                objResultados.dsResultados = null;
                objParametros.Id = 0;
                objParametros.Message = "Hubo una excepcion tratando de convertir los resultados";
                objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objParametros.Severity = clsSeveridad.Alta;
                objParametros.Tipo = clsTipoError.WebServices;
                objParametros.ViewMessage.Add("No hay resultados para la busqueda");
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            objResultados.dsResultados = null;
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Complemento = "Hubo una excepcion tratando de convertir los resultados";
            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
            objParametros.Severity = clsSeveridad.Alta;
            objParametros.Tipo = clsTipoError.WebServices;
            objParametros.Ex = Ex;
            ExceptionHandled.Publicar(objParametros);
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        if (objParametros.Id == 0)
        {
            objParametros.ErrorConfigura[0] = csReferencias.csEmpresa();
            objParametros.MessageBD = true;
            objParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
        }
        objResultados.Error = objParametros;

        return objResultados;
    }
    public clsResultados GetDsCotizaSabreAir(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirProceRQ)
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsVentaSabreAir = new DataSet();
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        clsVuelos objVuelos = new clsVuelos();

        OTA_AirPriceRS oOTA_AirPriceRS = new OTA_AirPriceRS();

        oOTA_AirPriceRS = new clsOTA_AirPrice()._Sabre_BuscarTarifa(vo_OTA_AirProceRQ);
        //string sComando = "PQ";
        //string sVenta = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
        try
        {
            if (oOTA_AirPriceRS != null)
            {
                if (oOTA_AirPriceRS.Success != null)
                {
                    /*CONVERTIMOS EL OBJETO EN UN DATASET*/
                    dsVentaSabreAir = clsEsquema.GetDatasetSabreAir(oOTA_AirPriceRS);
                    //string sEstructura = new Utils().DatasetStructura(dsVentaSabreAir);
                    if (dsVentaSabreAir != null && dsVentaSabreAir.Tables.Count > 0)
                    {
                        objResultados.dsResultados = dsVentaSabreAir;
                        objParametros.Id = 1;
                    }
                    else
                    {   /*SI EL DATASET ES NULLO O VACIO*/
                        objResultados.dsResultados = null;
                        objParametros.Id = 0;
                        objParametros.Message = "Dataset null o vacio";
                        objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        objParametros.Sugerencia.Add("Por favor, realice otra busqueda");
                        objParametros.ViewMessage.Add("No se encontraron tarifas disponibles para estos itinerarios");
                        objParametros.Severity = clsSeveridad.Media;
                    }
                }
                else
                {
                    /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                    objParametros.Id = 0;
                    objParametros.Tipo = clsTipoError.WebServices;
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objParametros.Code = oOTA_AirPriceRS.Errors.Error.ErrorCode;
                    objParametros.Info = oOTA_AirPriceRS.Errors.Error.ErrorInfo.Message;
                    objParametros.Message = oOTA_AirPriceRS.Errors.Error.ErrorMessage;
                    objParametros.ValidaInfo = true;
                    objParametros.Severity = oOTA_AirPriceRS.Errors.Error.Severity;
                    objParametros.Sugerencia.Add("Por favor, realice otra busqueda");
                    objParametros.ViewMessage.Add("No se encontraron tarifas disponibles para estos itinerarios");
                    ExceptionHandled.Publicar(objParametros);
                }
            }
            else
            {
                /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                objResultados.dsResultados = null;
                objParametros.Id = 0;
                objParametros.Message = "Hubo una excepcion tratando de convertir los resultados";
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objParametros.Severity = clsSeveridad.Alta;
                objParametros.Tipo = clsTipoError.WebServices;
                objParametros.Sugerencia.Add("Por favor, realice otra busqueda");
                objParametros.ViewMessage.Add("No se encontraron tarifas disponibles para estos itinerarios");
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            objResultados.dsResultados = null;
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Complemento = "Hubo una excepcion tratando de convertir los resultados";
            objParametros.Sugerencia.Add("Por favor, realice otra busqueda");
            objParametros.ViewMessage.Add("No se encontraron tarifas disponibles para estos itinerarios");
            objParametros.Severity = clsSeveridad.Alta;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
        }
        if (objParametros.Id == 0)
        {
            objParametros.ErrorConfigura[0] = csReferencias.csEmpresa();
            objParametros.MessageBD = true;
            objParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        objResultados.Error = objParametros;

        return objResultados;
    }
    public clsResultados GetDsBusquedaSabreAirHora(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsSabreAir = new DataSet();
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        clsVuelos objVuelos = new clsVuelos();
        if (vo_OTA_AirLowFareSearchLLSRQ.Ruta.Equals(0))
        {
            objVuelos.GetCrearDatasetSelectSabre(0, null);
        }
        OTA_AirAvailRS oOTA_AirLowFareSearchRS = GetBusquedaGeneneralHora(vo_OTA_AirLowFareSearchLLSRQ);

        try
        {
            if (oOTA_AirLowFareSearchRS != null)
            {
                if (oOTA_AirLowFareSearchRS.Success != null)
                {
                    /*CONVERTIMOS EL OBJETO EN UN DATASET*/
                    dsSabreAir = clsEsquema.GetDatasetSabreAir(oOTA_AirLowFareSearchRS);
                    //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                    if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                    {
                        objResultados.dsResultados = dsSabreAir;
                        objParametros.Id = 1;
                    }
                    else
                    {   /*SI EL DATASET ES NULLO O VACIO*/
                        objResultados.dsResultados = null;
                        objParametros.Id = 0;
                        objParametros.Message = "Dataset null o vacio";
                        objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                        objParametros.Code = "0";
                        objParametros.Severity = clsSeveridad.Media;
                    }
                }
                else
                {
                    if (clsValidaciones.GetKeyOrAdd("bTame", "False").ToUpper().Equals("TRUE"))
                    {
                        DataSet dsData = clsSesiones.GetDataTameAir();
                        if (dsData != null)
                        {
                            dsSabreAir = clsEsquema.GetDatasetSabreAir(oOTA_AirLowFareSearchRS);
                            //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                            if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                            {
                                objResultados.dsResultados = dsSabreAir;
                                objParametros.Id = 1;
                            }
                            else
                            {   /*SI EL DATASET ES NULLO O VACIO*/
                                objResultados.dsResultados = null;
                                objParametros.Id = 0;
                                objParametros.Message = "Dataset null o vacio";
                                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                                objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                                // Para mensaje de error personalizado
                                objParametros.Code = "0";
                                objParametros.Severity = clsSeveridad.Media;
                            }
                        }
                    }
                    else
                    {
                        {
                            /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                            objParametros.Id = 0;
                            objParametros.Tipo = clsTipoError.WebServices;
                            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                            objParametros.Code = oOTA_AirLowFareSearchRS.Errors.Error.ErrorCode;
                            objParametros.Info = oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message;
                            objParametros.ValidaInfo = true;
                            objParametros.Message = oOTA_AirLowFareSearchRS.Errors.Error.ErrorMessage;
                            objParametros.Severity = oOTA_AirLowFareSearchRS.Errors.Error.Severity;
                            //objParametros.ViewMessage.Add(oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message);
                            // Para mensaje de error personalizado
                            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                            ExceptionHandled.Publicar(objParametros);
                        }
                    }
                }
            }
            else
            {
                /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                objResultados.dsResultados = null;
                objParametros.Id = 0;
                objParametros.Code = "0";
                objParametros.Message = "Hubo una excepcion tratando de convertir los resultados";
                objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objParametros.Severity = clsSeveridad.Alta;
                objParametros.Tipo = clsTipoError.WebServices;
                objParametros.ViewMessage.Add("No hay resultados para la busqueda");
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            objResultados.dsResultados = null;
            objParametros.Id = 0;
            objParametros.Code = "0";
            objParametros.Message = Ex.Message;
            objParametros.Complemento = "Hubo una excepcion tratando de convertir los resultados";
            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
            objParametros.Severity = clsSeveridad.Alta;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
        }
        if (objParametros.Id == 0)
        {
            objParametros.ErrorConfigura[0] = csReferencias.csEmpresa();
            objParametros.MessageBD = true;
            objParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        objResultados.Error = objParametros;

        return objResultados;
    }
    public void GetDsBusquedaSabreAirHoraGenera()
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsSabreAir = new DataSet();
        OTA_AirAvailRS oOTA_AirLowFareSearchRS = new OTA_AirAvailRS();

        try
        {
            if (clsValidaciones.GetKeyOrAdd("bTame", "False").ToUpper().Equals("TRUE"))
            {
                DataSet dsData = clsSesiones.GetDataTameAir();
                if (dsData != null)
                {
                    dsSabreAir = clsEsquema.GetDatasetSabreAir(oOTA_AirLowFareSearchRS);
                    if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                    {
                        Ssoft.Utils.clsSesiones.SetDatasetSabreAir(dsSabreAir);
                    }
                }
            }
        }
        catch
        {
        }
    }
    public void GetDsBusquedaSabreAirGenera()
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsSabreAir = new DataSet();

        OTA_AirLowFareSearchRS oOTA_AirLowFareSearchRS = new OTA_AirLowFareSearchRS();
        try
        {
            if (clsValidaciones.GetKeyOrAdd("bTame", "False").ToUpper().Equals("TRUE"))
            {
                DataSet dsData = clsSesiones.GetDataTameAir();
                if (dsData != null)
                {
                    dsSabreAir = clsEsquema.GetDatasetSabreAir(oOTA_AirLowFareSearchRS);
                    Ssoft.Utils.clsSesiones.SetDatasetSabreAir(dsSabreAir);
                }
            }
        }
        catch
        {
        }
    }
    public void GetDsCotizaSabreAirGenera()
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsSabreAir = new DataSet();

        OTA_AirPriceRS oOTA_AirPriceRS = new OTA_AirPriceRS();
        try
        {
            if (clsValidaciones.GetKeyOrAdd("bTame", "False").ToUpper().Equals("TRUE"))
            {
                DataSet dsData = clsSesiones.GetDataTameAir();
                if (dsData != null)
                {
                    dsSabreAir = clsEsquema.GetDatasetSabreAir(oOTA_AirPriceRS);
                    Ssoft.Utils.clsSesiones.SetDatasetSabreAir(dsSabreAir);
                }
            }
        }
        catch
        {
        }
    }
    public clsResultados GetDsBusquedaSabreAir(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsSabreAir = new DataSet();
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();



        WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRS oOTA_AirLowFareSearchRS = GetBusquedaGeneneralMax(vo_OTA_AirLowFareSearchLLSRQ);

        //PricedItineraries oPrices = clsEsquema.GetClassSabreAir(oOTA_AirLowFareSearchRS);

        try
        {
            if (oOTA_AirLowFareSearchRS != null)
            {
                if (oOTA_AirLowFareSearchRS.Success != null)
                {
                    /*CONVERTIMOS EL OBJETO EN UN DATASET*/
                    dsSabreAir = clsEsquema.GetDatasetSabreAirMax(oOTA_AirLowFareSearchRS);
                    //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                    if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                    {
                        objResultados.dsResultados = dsSabreAir;
                        objParametros.Id = 1;
                    }
                    else
                    {   /*SI EL DATASET ES NULLO O VACIO*/
                        objResultados.dsResultados = null;
                        objParametros.Id = 0;
                        objParametros.Message = "Dataset null o vacio";
                        objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                        // Para mensaje de error personalizado
                        objParametros.Code = "0";
                        objParametros.Severity = clsSeveridad.Media;
                    }
                }
                else
                {
                    if (clsValidaciones.GetKeyOrAdd("bTame", "False").ToUpper().Equals("TRUE"))
                    {
                        DataSet dsData = clsSesiones.GetDataTameAir();
                        if (dsData != null)
                        {
                            dsSabreAir = clsEsquema.GetDatasetSabreAirMax(oOTA_AirLowFareSearchRS);
                            //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                            if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                            {
                                objResultados.dsResultados = dsSabreAir;
                                objParametros.Id = 1;
                            }
                            else
                            {   /*SI EL DATASET ES NULLO O VACIO*/
                                objResultados.dsResultados = null;
                                objParametros.Id = 0;
                                objParametros.Message = "Dataset null o vacio";
                                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                                objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                                // Para mensaje de error personalizado
                                objParametros.Code = "0";
                                objParametros.Severity = clsSeveridad.Media;
                            }
                        }
                    }
                    else
                    {
                        {
                            /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                            objParametros.Id = 0;
                            objParametros.Tipo = clsTipoError.WebServices;
                            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                            objParametros.Code = "Error sabre"; //oOTA_AirLowFareSearchRS.Errors.Error.ErrorCode;
                            objParametros.Info = "Error sabreBFM";//oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message;
                            objParametros.ValidaInfo = true;
                            objParametros.Message = "NOO";
                            objParametros.Severity = "A";
                            //objParametros.ViewMessage.Add(oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message);
                            // Para mensaje de error personalizado
                            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                            ExceptionHandled.Publicar(objParametros);
                        }
                    }
                }
            }
            else
            {
                if (clsValidaciones.GetKeyOrAdd("bTame", "False").ToUpper().Equals("TRUE"))
                {
                    DataSet dsData = clsSesiones.GetDataTameAir();
                    if (dsData != null)
                    {
                        dsSabreAir = clsEsquema.GetDatasetSabreAirMax(oOTA_AirLowFareSearchRS);
                        //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                        if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                        {
                            objResultados.dsResultados = dsSabreAir;
                            objParametros.Id = 1;
                        }
                        else
                        {   /*SI EL DATASET ES NULLO O VACIO*/
                            objResultados.dsResultados = null;
                            objParametros.Id = 0;
                            objParametros.Message = "Dataset null o vacio";
                            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                            // Para mensaje de error personalizado
                            objParametros.Code = "0";
                            objParametros.Severity = clsSeveridad.Media;
                        }
                    }
                }
                else
                {
                    /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                    objResultados.dsResultados = null;
                    objParametros.Id = 0;
                    objParametros.Message = "Hubo una excepcion tratando de convertir los resultados";
                    objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objParametros.Severity = clsSeveridad.Alta;
                    objParametros.Tipo = clsTipoError.WebServices;
                    // Para mensaje de error personalizado
                    objParametros.Code = "0";
                    objParametros.ViewMessage.Add("No hay resultados para la busqueda");
                }
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            objResultados.dsResultados = null;
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Complemento = "Hubo una excepcion tratando de convertir los resultados";
            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
            objParametros.Severity = clsSeveridad.Alta;
            objParametros.Tipo = clsTipoError.WebServices;
            // Para mensaje de error personalizado
            objParametros.Code = "0";
            ExceptionHandled.Publicar(objParametros);
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        if (objParametros.Id == 0)
        {
            objParametros.ErrorConfigura[0] = csReferencias.csEmpresa();
            objParametros.MessageBD = true;
            objParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
        }
        objResultados.Error = objParametros;

        return objResultados;
    }
    #region Search MultiDestination
    /// <summary>
    /// hceron multidestination
    /// 23052013
    /// </summary>
    /// <param name="vo_OTA_AirLowFareSearchLLSRQ"></param>
    /// <returns></returns>
    public clsResultados GetDsBusquedaSabreAirMulti(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsSabreAir = new DataSet();
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();

        OTA_AirLowFareSearchRS oOTA_AirLowFareSearchRS = GetBusquedaGeneneralMulti(vo_OTA_AirLowFareSearchLLSRQ);

        //PricedItineraries oPrices = clsEsquema.GetClassSabreAir(oOTA_AirLowFareSearchRS);

        try
        {
            if (oOTA_AirLowFareSearchRS != null)
            {
                if (oOTA_AirLowFareSearchRS.Success != null)
                {
                    /*CONVERTIMOS EL OBJETO EN UN DATASET*/
                    dsSabreAir = clsEsquema.GetDatasetSabreAir(oOTA_AirLowFareSearchRS);
                    //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                    if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                    {
                        objResultados.dsResultados = dsSabreAir;
                        objParametros.Id = 1;
                    }
                    else
                    {   /*SI EL DATASET ES NULLO O VACIO*/
                        objResultados.dsResultados = null;
                        objParametros.Id = 0;
                        objParametros.Message = "Dataset null o vacio";
                        objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                        // Para mensaje de error personalizado
                        objParametros.Code = "0";
                        objParametros.Severity = clsSeveridad.Media;
                    }
                }
                else
                {
                    if (clsValidaciones.GetKeyOrAdd("bTame", "False").ToUpper().Equals("TRUE"))
                    {
                        DataSet dsData = clsSesiones.GetDataTameAir();
                        if (dsData != null)
                        {
                            dsSabreAir = clsEsquema.GetDatasetSabreAir(oOTA_AirLowFareSearchRS);
                            //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                            if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                            {
                                objResultados.dsResultados = dsSabreAir;
                                objParametros.Id = 1;
                            }
                            else
                            {   /*SI EL DATASET ES NULLO O VACIO*/
                                objResultados.dsResultados = null;
                                objParametros.Id = 0;
                                objParametros.Message = "Dataset null o vacio";
                                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                                objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                                // Para mensaje de error personalizado
                                objParametros.Code = "0";
                                objParametros.Severity = clsSeveridad.Media;
                            }
                        }
                    }
                    else
                    {
                        {
                            /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                            objParametros.Id = 0;
                            objParametros.Tipo = clsTipoError.WebServices;
                            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                            objParametros.Code = oOTA_AirLowFareSearchRS.Errors.Error.ErrorCode;
                            objParametros.Info = oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message;
                            objParametros.ValidaInfo = true;
                            objParametros.Message = oOTA_AirLowFareSearchRS.Errors.Error.ErrorMessage;
                            objParametros.Severity = oOTA_AirLowFareSearchRS.Errors.Error.Severity;
                            //objParametros.ViewMessage.Add(oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message);
                            // Para mensaje de error personalizado
                            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                            ExceptionHandled.Publicar(objParametros);
                        }
                    }
                }
            }
            else
            {
                if (clsValidaciones.GetKeyOrAdd("bTame", "False").ToUpper().Equals("TRUE"))
                {
                    DataSet dsData = clsSesiones.GetDataTameAir();
                    if (dsData != null)
                    {
                        dsSabreAir = clsEsquema.GetDatasetSabreAir(oOTA_AirLowFareSearchRS);
                        //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                        if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                        {
                            objResultados.dsResultados = dsSabreAir;
                            objParametros.Id = 1;
                        }
                        else
                        {   /*SI EL DATASET ES NULLO O VACIO*/
                            objResultados.dsResultados = null;
                            objParametros.Id = 0;
                            objParametros.Message = "Dataset null o vacio";
                            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                            // Para mensaje de error personalizado
                            objParametros.Code = "0";
                            objParametros.Severity = clsSeveridad.Media;
                        }
                    }
                }
                else
                {
                    /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                    objResultados.dsResultados = null;
                    objParametros.Id = 0;
                    objParametros.Message = "Hubo una excepcion tratando de convertir los resultados";
                    objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objParametros.Severity = clsSeveridad.Alta;
                    objParametros.Tipo = clsTipoError.WebServices;
                    // Para mensaje de error personalizado
                    objParametros.Code = "0";
                    objParametros.ViewMessage.Add("No hay resultados para la busqueda");
                }
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            objResultados.dsResultados = null;
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Complemento = "Hubo una excepcion tratando de convertir los resultados";
            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
            objParametros.Severity = clsSeveridad.Alta;
            objParametros.Tipo = clsTipoError.WebServices;
            // Para mensaje de error personalizado
            objParametros.Code = "0";
            ExceptionHandled.Publicar(objParametros);
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        if (objParametros.Id == 0)
        {
            objParametros.ErrorConfigura[0] = csReferencias.csEmpresa();
            objParametros.MessageBD = true;
            objParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
        }
        objResultados.Error = objParametros;

        return objResultados;
    }
    #endregion
    /// <summary>
    /// obtenmos el listado de datos de BFM MAX
    ///hceron
    /// </summary>
    /// <param name="vo_OTA_AirLowFareSearchLLSRQ"></param>
    /// <returns></returns>
    public clsResultados GetDsBusquedaSabreAirMax(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsSabreAir = new DataSet();
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();

        WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRS oOTA_AirLowFareSearchRS = GetBusquedaGeneneralMax(vo_OTA_AirLowFareSearchLLSRQ);

        //PricedItineraries oPrices = clsEsquema.GetClassSabreAir(oOTA_AirLowFareSearchRS);

        try
        {
            if (oOTA_AirLowFareSearchRS != null)
            {
                if (oOTA_AirLowFareSearchRS.Success != null)
                {
                    /*CONVERTIMOS EL OBJETO EN UN DATASET*/
                    dsSabreAir = clsEsquema.GetDatasetSabreAirMax(oOTA_AirLowFareSearchRS);
                    //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                    if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                    {
                        objResultados.dsResultados = dsSabreAir;
                        objParametros.Id = 1;
                    }
                    else
                    {   /*SI EL DATASET ES NULLO O VACIO*/
                        objResultados.dsResultados = null;
                        objParametros.Id = 0;
                        objParametros.Message = "Dataset null o vacio";
                        objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                        // Para mensaje de error personalizado
                        objParametros.Code = "0";
                        objParametros.Severity = clsSeveridad.Media;
                    }
                }
                else
                {
                    if (clsValidaciones.GetKeyOrAdd("bTame", "False").ToUpper().Equals("TRUE"))
                    {
                        DataSet dsData = clsSesiones.GetDataTameAir();
                        if (dsData != null)
                        {
                            dsSabreAir = clsEsquema.GetDatasetSabreAirMax(oOTA_AirLowFareSearchRS);
                            //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                            if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                            {
                                objResultados.dsResultados = dsSabreAir;
                                objParametros.Id = 1;
                            }
                            else
                            {   /*SI EL DATASET ES NULLO O VACIO*/
                                objResultados.dsResultados = null;
                                objParametros.Id = 0;
                                objParametros.Message = "Dataset null o vacio";
                                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                                objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                                // Para mensaje de error personalizado
                                objParametros.Code = "0";
                                objParametros.Severity = clsSeveridad.Media;
                            }
                        }
                    }
                    else
                    {
                        {
                            /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                            objParametros.Id = 0;
                            objParametros.Tipo = clsTipoError.WebServices;
                            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                            //objParametros.Code = oOTA_AirLowFareSearchRS.Errors.Error.ErrorCode;
                            //objParametros.Info = oOTA_AirLowFareSearchRS.Errors..Error.ErrorInfo.Message;
                            objParametros.ValidaInfo = true;
                            //objParametros.Message = oOTA_AirLowFareSearchRS.Errors.Error.ErrorMessage;
                            //objParametros.Severity = oOTA_AirLowFareSearchRS.Errors.Error.Severity;
                            //objParametros.ViewMessage.Add(oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message);
                            // Para mensaje de error personalizado
                            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                            ExceptionHandled.Publicar(objParametros);
                        }
                    }
                }
            }
            else
            {
                if (clsValidaciones.GetKeyOrAdd("bTame", "False").ToUpper().Equals("TRUE"))
                {
                    DataSet dsData = clsSesiones.GetDataTameAir();
                    if (dsData != null)
                    {
                        dsSabreAir = clsEsquema.GetDatasetSabreAirMax(oOTA_AirLowFareSearchRS);
                        //new clsSerializer().DatasetXML(dsSabreAir, "antes");
                        if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                        {
                            objResultados.dsResultados = dsSabreAir;
                            objParametros.Id = 1;
                        }
                        else
                        {   /*SI EL DATASET ES NULLO O VACIO*/
                            objResultados.dsResultados = null;
                            objParametros.Id = 0;
                            objParametros.Message = "Dataset null o vacio";
                            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                            // Para mensaje de error personalizado
                            objParametros.Code = "0";
                            objParametros.Severity = clsSeveridad.Media;
                        }
                    }
                }
                else
                {
                    /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                    objResultados.dsResultados = null;
                    objParametros.Id = 0;
                    objParametros.Message = "Hubo una excepcion tratando de convertir los resultados";
                    objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objParametros.Severity = clsSeveridad.Alta;
                    objParametros.Tipo = clsTipoError.WebServices;
                    // Para mensaje de error personalizado
                    objParametros.Code = "0";
                    objParametros.ViewMessage.Add("No hay resultados para la busqueda");
                }
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            objResultados.dsResultados = null;
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Complemento = "Hubo una excepcion tratando de convertir los resultados";
            objParametros.ViewMessage.Add("No se encontraron resultados para su búsqueda");
            objParametros.Severity = clsSeveridad.Alta;
            objParametros.Tipo = clsTipoError.WebServices;
            // Para mensaje de error personalizado
            objParametros.Code = "0";
            ExceptionHandled.Publicar(objParametros);
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        if (objParametros.Id == 0)
        {
            objParametros.ErrorConfigura[0] = csReferencias.csEmpresa();
            objParametros.MessageBD = true;
            objParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
        }
        objResultados.Error = objParametros;

        return objResultados;
    }
    
    /// <summary>
    /// Obtener informacion de rerserva
    /// </summary>
    /// <param name="Record_"></param>
    /// <returns></returns>
    public clsResultados GetDsBusquedaRecordSabreAir(string Record_)
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsSabreAir = new DataSet();
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        try
        {
            OTA_TravelItineraryRS oOTA_TravelItineraryRS = new clsOTA_TravelItineraryRead()._Sabre_LeerInformacionPNR(Record_);
            if (oOTA_TravelItineraryRS != null)
            {
                if (oOTA_TravelItineraryRS.Success != null)
                {
                    /*CONVERTIMOS EL OBJETO EN UN DATASET*/
                    dsSabreAir = clsEsquema.GetDatasetSabreAir(oOTA_TravelItineraryRS);

                    if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                    {
                        objResultados.dsResultados = dsSabreAir;
                        objParametros.Id = 1;
                    }
                    else
                    {   /*SI EL DATASET ES NULLO O VACIO*/
                        objResultados.dsResultados = null;
                        objParametros.Id = 0;
                        objParametros.Message = "Reserva no existe";
                        objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        objParametros.ViewMessage.Add("Reserva no existe");
                        objParametros.Complemento = "Reserva: " + Record_;
                        objParametros.Severity = clsSeveridad.Media;
                        objParametros.Code = "0";
                        objParametros.Sugerencia.Add("");
                    }
                }
                else
                {
                    /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                    objParametros.Id = 0;
                    objParametros.Tipo = clsTipoError.WebServices;
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objParametros.Code = oOTA_TravelItineraryRS.Errors.Error.ErrorCode;
                    objParametros.Info = oOTA_TravelItineraryRS.Errors.Error.ErrorInfo.Message;
                    objParametros.Message = oOTA_TravelItineraryRS.Errors.Error.ErrorMessage;
                    objParametros.ValidaInfo = true;
                    objParametros.Severity = oOTA_TravelItineraryRS.Errors.Error.Severity;
                    //objParametros.ViewMessage.Add(oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message);
                    objParametros.Complemento = "Reserva: " + Record_;
                    objParametros.ViewMessage.Add("Reserva no existe");
                    objParametros.Sugerencia.Add("");
                    ExceptionHandled.Publicar(objParametros);
                }
            }
            else
            {
                /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                objResultados.dsResultados = null;
                objParametros.Id = 0;
                objParametros.Message = "Hubo una excepcion tratando de convertir los resultados";
                objParametros.ViewMessage.Add("No se encontraron resultados para su reserva");
                objParametros.Sugerencia.Add("");
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objParametros.Severity = clsSeveridad.Alta;
                objParametros.Complemento = "Reserva: " + Record_;
                objParametros.Code = "0";
                objParametros.Tipo = clsTipoError.WebServices;
                objParametros.ViewMessage.Add("Reserva no existe");
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            objResultados.dsResultados = null;
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Complemento = "Hubo una excepcion tratando de convertir los resultados";
            objParametros.ViewMessage.Add("Reserva no existe");
            objParametros.Complemento = "Reserva: " + Record_;
            objParametros.Code = "0";
            objParametros.Sugerencia.Add("");
            objParametros.Severity = clsSeveridad.Alta;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        //try { Negocios_WebServiceSession._CerrarSesion(); }
        //catch { }
        if (objParametros.Id == 0)
        {
            objParametros.ErrorConfigura[0] = csReferencias.csEmpresa();
            objParametros.MessageBD = true;
            objParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
        }
        objResultados.Error = objParametros;

        return objResultados;
    }
    public clsResultados GetDsBusquedaTarifaSabreAir()
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsSabreAir = new DataSet();
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();

        try
        {
            DisplayPriceQuoteRS oDisplayPriceQuoteRS = new clsDisplayPriceQuote().getTarifa();
            if (oDisplayPriceQuoteRS != null)
            {
                if (oDisplayPriceQuoteRS.Success != null)
                {
                    /*CONVERTIMOS EL OBJETO EN UN DATASET*/
                    dsSabreAir = clsEsquema.GetDatasetSabreAir(oDisplayPriceQuoteRS);
                    //Negocios_WebServiceSession._CerrarSesion();
                    try
                    {
                        List<VO_TarifaPago> lvoTarifaPago = new List<VO_TarifaPago>();
                        lvoTarifaPago = GetVoBusquedaTarifaSabreAirConvert(oDisplayPriceQuoteRS);
                        clsSesiones.setTarifaPagoAir(lvoTarifaPago);
                    }
                    catch { }
                    if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
                    {
                        objResultados.dsResultados = dsSabreAir;
                        objParametros.Id = 1;
                    }
                    else
                    {   /*SI EL DATASET ES NULLO O VACIO*/
                        objResultados.dsResultados = null;
                        objParametros.Id = 0;
                        objParametros.Message = "No se encontraro tarifas para la reserva";
                        objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        objParametros.Code = "0";
                        objParametros.ViewMessage.Add("No se encontraro tarifas para la reserva");
                        objParametros.Severity = clsSeveridad.Media;
                        objParametros.Sugerencia.Add("");
                    }
                }
                else
                {
                    /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                    objParametros.Id = 0;
                    objParametros.Tipo = clsTipoError.WebServices;
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objParametros.Code = oDisplayPriceQuoteRS.Errors.Error.ErrorCode;
                    objParametros.Info = oDisplayPriceQuoteRS.Errors.Error.ErrorInfo.Message;
                    objParametros.ValidaInfo = true;
                    objParametros.Message = oDisplayPriceQuoteRS.Errors.Error.ErrorMessage;
                    objParametros.Severity = oDisplayPriceQuoteRS.Errors.Error.Severity;
                    //objParametros.ViewMessage.Add(oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message);
                    objParametros.ViewMessage.Add("No se encontraro tarifas para la reserva");
                    objParametros.Sugerencia.Add("");
                    ExceptionHandled.Publicar(objParametros);
                }
            }
            else
            {
                /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                objResultados.dsResultados = null;
                objParametros.Id = 0;
                objParametros.Code = "0";
                objParametros.Message = "Hubo una excepcion tratando de convertir los resultados";
                objParametros.ViewMessage.Add("No se encontraro tarifas para la reserva");
                objParametros.Sugerencia.Add("");
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objParametros.Severity = clsSeveridad.Alta;
                objParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(objParametros);
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            objResultados.dsResultados = null;
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Code = "0";
            objParametros.Complemento = "Hubo una excepcion tratando de convertir los resultados";
            objParametros.ViewMessage.Add("No se encontraro tarifas para la reserva");
            objParametros.Sugerencia.Add("");
            objParametros.Severity = clsSeveridad.Alta;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        //try { Negocios_WebServiceSession._CerrarSesion(); }
        //catch { }
        if (objParametros.Id == 0)
        {
            objParametros.ErrorConfigura[0] = csReferencias.csEmpresa();
            objParametros.MessageBD = true;
            objParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
        }
        objResultados.Error = objParametros;

        return objResultados;
    }
    public List<VO_TarifaPago> GetVoBusquedaTarifaSabreAir()
    {
        clsParametros objParametros = new clsParametros();
        List<VO_TarifaPago> lvoTarifaPago = new List<VO_TarifaPago>();

        try
        {
            DisplayPriceQuoteRS oDisplayPriceQuoteRS = new clsDisplayPriceQuote().getTarifa();
            if (oDisplayPriceQuoteRS != null)
            {
                lvoTarifaPago = GetVoBusquedaTarifaSabreAirConvert(oDisplayPriceQuoteRS);
            }
            else
            {
                /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                objParametros.Id = 0;
                objParametros.Message = "Hubo una excepcion tratando de convertir los resultados";
                objParametros.ViewMessage.Add("No se encontraron resultados para su reserva");
                objParametros.Sugerencia.Add("");
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objParametros.Severity = clsSeveridad.Alta;
                objParametros.Tipo = clsTipoError.WebServices;
                objParametros.ViewMessage.Add("No hay resultados para la tarifa");
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Source = Ex.Source;
            objParametros.Complemento = "Hubo una excepcion tratando de convertir los resultados";
            objParametros.ViewMessage.Add("No se encontraron resultados para su tarifa");
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Sugerencia.Add("");
            objParametros.Severity = clsSeveridad.Alta;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        //try { Negocios_WebServiceSession._CerrarSesion(); }
        //catch { }
        return lvoTarifaPago;
    }
    public List<VO_TarifaPago> GetVoBusquedaTarifaSabreAirConvert(DisplayPriceQuoteRS oDisplayPriceQuoteRS)
    {
        clsParametros objParametros = new clsParametros();
        List<VO_TarifaPago> lvoTarifaPago = new List<VO_TarifaPago>();

        try
        {
            if (oDisplayPriceQuoteRS != null)
            {
                List<VO_OriginDestinationInformation> lvo_OrigenDestino = new List<VO_OriginDestinationInformation>();
                string sComandoPD = Negocios_WebServiceSabreCommand._EjecutarComando("PD");
                List<string[]> lsData = clsValidacionesVuelos.setComadoPD(sComandoPD); ;
                int iEnumerar = 1;
                if (oDisplayPriceQuoteRS.Success != null)
                {
                    if (oDisplayPriceQuoteRS.PriceQuote != null)
                    {
                        int iPosTotal = oDisplayPriceQuoteRS.PriceQuote.Length;
                        for (int i = 0; i < iPosTotal; i++)
                        {
                            int iPosData = lsData.Count;
                            for (int m = 0; m < iPosData; m++)
                            {
                                if (lsData[m][1].ToString().Trim().Equals(oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.PTC_FareBreakdown.PassengerTypeQuantity[0].Code.ToString().Trim()))
                                {
                                    VO_TarifaPago voTarifaPago = new VO_TarifaPago();
                                    VO_Pasajero vPasajero = new VO_Pasajero();
                                    List<VO_Impuesto> lvo_Impuesto = new List<VO_Impuesto>();
                                    //Ssoft.ValueObjects.VO_TA vTa = new Ssoft.ValueObjects.VO_TA();
                                    vPasajero.SCodigo = oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.PTC_FareBreakdown.PassengerTypeQuantity[0].Code;
                                    vPasajero.SCantidad = iEnumerar.ToString();
                                    if (oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.ItinTotalFare.BaseFare.CurrencyCode.ToString().Equals("COP"))
                                    {
                                        voTarifaPago.Tarifa = oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.ItinTotalFare.BaseFare.Amount.ToString();
                                    }
                                    else
                                    {
                                        voTarifaPago.Tarifa = oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.ItinTotalFare.EquivFare.Amount.ToString();
                                    }
                                    voTarifaPago.Impuestos = oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.ItinTotalFare.Taxes.TotalAmount.ToString();
                                    voTarifaPago.Total = oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount.ToString();
                                    voTarifaPago.Pasajero = vPasajero;
                                    voTarifaPago.TipoVuelo = Enum_TipoVuelo.Nacional;
                                    try
                                    {
                                        voTarifaPago.Nombre = lsData[m][2].ToString().Replace("/", " ");
                                        voTarifaPago.Pos = int.Parse(lsData[m][3].ToString());
                                    }
                                    catch { }
                                    if (oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax != null)
                                    {
                                        int iPosTotalImpuestos = oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax.Length;
                                        for (int j = 0; j < iPosTotalImpuestos; j++)
                                        {
                                            VO_Impuesto vImpuesto = new VO_Impuesto();
                                            vImpuesto.SCodigo = oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[j].TaxCode;
                                            vImpuesto.DValor = oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[j].Amount;
                                            lvo_Impuesto.Add(vImpuesto);
                                        }
                                        voTarifaPago.LImpuestos = lvo_Impuesto;
                                    }
                                    lvoTarifaPago.Add(voTarifaPago);
                                    iEnumerar++;
                                }
                            }
                            try
                            {
                                if (i.Equals(0))
                                {
                                    if (oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.TPA_Extensions.FlightSegment != null)
                                    {
                                        int iPosTotalSegment = oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.TPA_Extensions.FlightSegment.Length;
                                        for (int k = 0; k < iPosTotalSegment; k++)
                                        {
                                            VO_OriginDestinationInformation vo_OrigenDestino = new VO_OriginDestinationInformation();
                                            VO_Aeropuerto vo_AeropuertoOrigen = new VO_Aeropuerto();
                                            VO_Aeropuerto vo_AeropuertoDestino = new VO_Aeropuerto();

                                            vo_AeropuertoDestino.SCodigo = oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.TPA_Extensions.FlightSegment[k].ArrivalAirport.LocationCode;
                                            vo_AeropuertoDestino.SContexto = "IATA";

                                            vo_AeropuertoOrigen.SCodigo = oDisplayPriceQuoteRS.PriceQuote[i].PricedItinerary.AirItineraryPricingInfo.TPA_Extensions.FlightSegment[k].DepartureAirport.LocationCode;
                                            vo_AeropuertoOrigen.SContexto = "IATA";

                                            vo_OrigenDestino.Vo_AeropuertoOrigen = vo_AeropuertoOrigen;
                                            vo_OrigenDestino.Vo_AeropuertoDestino = vo_AeropuertoDestino;
                                            lvo_OrigenDestino.Add(vo_OrigenDestino);
                                        }
                                    }
                                }
                            }
                            catch { }
                        }
                        objParametros.Id = 1;
                    }
                    try
                    {
                        if (lvoTarifaPago != null)
                        {
                            if (lvoTarifaPago.Count > 0)
                            {
                                csVuelos csValidacionesVuelos = new csVuelos();
                                lvoTarifaPago[0].TipoVuelo = csValidacionesVuelos.getValidarTipoTrayecto(lvo_OrigenDestino);
                            }
                        }
                    }
                    catch { }
                }
                else
                {
                    /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                    objParametros.Id = 0;
                    objParametros.Tipo = clsTipoError.WebServices;
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objParametros.Code = oDisplayPriceQuoteRS.Errors.Error.ErrorCode;
                    objParametros.Info = oDisplayPriceQuoteRS.Errors.Error.ErrorInfo.Message;
                    objParametros.Message = oDisplayPriceQuoteRS.Errors.Error.ErrorMessage;
                    objParametros.Severity = oDisplayPriceQuoteRS.Errors.Error.Severity;
                    //objParametros.ViewMessage.Add(oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message);
                    objParametros.ViewMessage.Add("No se encontraron resultados para la tarifa");
                    objParametros.Sugerencia.Add("");
                    ExceptionHandled.Publicar(objParametros);
                }
            }
            else
            {
                /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                objParametros.Id = 0;
                objParametros.Message = "Hubo una excepcion tratando de convertir los resultados";
                objParametros.ViewMessage.Add("No se encontraron resultados para su reserva");
                objParametros.Sugerencia.Add("");
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objParametros.Severity = clsSeveridad.Alta;
                objParametros.Tipo = clsTipoError.WebServices;
                objParametros.ViewMessage.Add("No hay resultados para la tarifa");
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Source = Ex.Source;
            objParametros.Complemento = "Hubo una excepcion tratando de convertir los resultados";
            objParametros.ViewMessage.Add("No se encontraron resultados para su tarifa");
            objParametros.Sugerencia.Add("");
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Severity = clsSeveridad.Alta;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        //try { Negocios_WebServiceSession._CerrarSesion(); }
        //catch { }
        return lvoTarifaPago;
    }
    public clsParametros GetCancelRecordSabre(string Record_)
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        clsParametros cParametros = new clsParametros();
        bool bCancel = false;
        string sResponse = string.Empty;
        try
        {
            OTA_TravelItineraryRS oOTA_TravelItineraryRS = new OTA_TravelItineraryRS();
            try
            {
                oOTA_TravelItineraryRS = new clsOTA_TravelItineraryRead()._Sabre_LeerInformacionPNR(Record_);
            }
            catch { }
            if (oOTA_TravelItineraryRS != null)
            {
                if (oOTA_TravelItineraryRS.Success != null)
                {
                    if (oOTA_TravelItineraryRS.TravelItinerary.ItineraryInfo.Ticketing != null)
                    {
                        try
                        {
                            for (int i = 0; i < oOTA_TravelItineraryRS.TravelItinerary.ItineraryInfo.Ticketing.Length; i++)
                            {
                                if (oOTA_TravelItineraryRS.TravelItinerary.ItineraryInfo.Ticketing[i].eTicketNumber == null)
                                {
                                    bCancel = true;
                                }
                                else
                                {
                                    string sTikete = string.Empty;
                                    try { sTikete = oOTA_TravelItineraryRS.TravelItinerary.ItineraryInfo.Ticketing[i].eTicketNumber.ToString(); }
                                    catch { }
                                    cParametros.Id = 2;
                                    cParametros.Info = "Reserva " + Record_ + " contiene tiquete emitido" + sTikete;
                                    cParametros.Message = "Reserva " + Record_ + " contiene tiquete emitido " + sTikete;
                                    cParametros.ViewMessage.Add("Reserva " + Record_ + " contiene tiquete emitido " + sTikete);
                                    cParametros.Sugerencia.Add("Por favor tramite la cancelación a traves de la agencia");
                                    cParametros.DatoAdic = sTikete;
                                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                                    cParametros.Severity = clsSeveridad.Alta;
                                    cParametros.Tipo = clsTipoError.WebServices;
                                    ExceptionHandled.Publicar(cParametros);
                                    bCancel = false;
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            sResponse = Negocios_WebServiceSabreCommand._EjecutarComando("*T");
                            if (sResponse.Contains("2."))
                            {
                                cParametros.Id = 2;
                                cParametros.Info = "Reserva " + Record_ + " contiene tiquete emitido" + sResponse;
                                cParametros.Message = "Reserva " + Record_ + " contiene tiquete emitido " + sResponse;
                                cParametros.ViewMessage.Add("Reserva " + Record_ + " contiene tiquete emitido " + sResponse);
                                cParametros.Sugerencia.Add("Por favor tramite la cancelación a traves de la agencia");
                                cParametros.DatoAdic = sResponse;
                                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                                cParametros.Severity = clsSeveridad.Alta;
                                cParametros.Tipo = clsTipoError.WebServices;
                                ExceptionHandled.Publicar(cParametros);
                            }
                            else
                            {
                                bCancel = true;
                            }
                        }
                    }
                    else
                    {
                        bCancel = true;
                    }
                }
                else
                {
                    /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                    cParametros.Id = 0;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    if (oOTA_TravelItineraryRS.Errors != null)
                    {
                        cParametros.Code = oOTA_TravelItineraryRS.Errors.Error.ErrorCode;
                        cParametros.Info = oOTA_TravelItineraryRS.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = oOTA_TravelItineraryRS.Errors.Error.ErrorMessage;
                        cParametros.Severity = oOTA_TravelItineraryRS.Errors.Error.Severity;
                    }
                    else
                    {
                        cParametros.Message = "Error al consultar el metodo";
                        cParametros.Severity = clsSeveridad.Alta;
                    }
                    cParametros.ViewMessage.Add("Reserva " + Record_ + ". No encontrada");
                    cParametros.DatoAdic = string.Empty;
                    cParametros.Sugerencia.Add("");
                    ExceptionHandled.Publicar(cParametros);
                    try
                    {
                        sResponse = Negocios_WebServiceSabreCommand._EjecutarComando("*" + Record_);
                        if (sResponse.Contains(Record_))
                        {
                            sResponse = Negocios_WebServiceSabreCommand._EjecutarComando("*T");
                            if (sResponse.Contains("2."))
                            {
                                cParametros.Id = 2;
                                cParametros.Info = "Reserva " + Record_ + " contiene tiquete emitido" + sResponse;
                                cParametros.Message = "Reserva " + Record_ + " contiene tiquete emitido " + sResponse;
                                cParametros.ViewMessage.Add("Reserva " + Record_ + " contiene tiquete emitido " + sResponse);
                                cParametros.Sugerencia.Add("Por favor tramite la cancelación a traves de la agencia");
                                cParametros.DatoAdic = sResponse;
                                cParametros.Metodo = "GetCancelRecordSabre";
                                cParametros.Severity = clsSeveridad.Alta;
                                cParametros.Tipo = clsTipoError.WebServices;
                                ExceptionHandled.Publicar(cParametros);
                            }
                            else
                            {
                                bCancel = true;
                            }
                        }
                    }
                    catch { }
                }
            }
            else
            {
                /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                cParametros.Id = 0;
                cParametros.Info = "Reserva " + Record_ + ". No encontrada";
                cParametros.Message = "Reserva " + Record_ + ". No encontrada";
                cParametros.ViewMessage.Add("Reserva " + Record_ + ". No encontrada");
                cParametros.Sugerencia.Add("");
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.DatoAdic = string.Empty;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
                try
                {
                    sResponse = Negocios_WebServiceSabreCommand._EjecutarComando("*" + Record_);
                    if (sResponse.Contains(Record_))
                    {
                        sResponse = Negocios_WebServiceSabreCommand._EjecutarComando("*T");
                        if (sResponse.Contains("2."))
                        {
                            cParametros.Id = 2;
                            cParametros.Info = "Reserva " + Record_ + " contiene tiquete emitido" + sResponse;
                            cParametros.Message = "Reserva " + Record_ + " contiene tiquete emitido " + sResponse;
                            cParametros.ViewMessage.Add("Reserva " + Record_ + " contiene tiquete emitido " + sResponse);
                            cParametros.Sugerencia.Add("Por favor tramite la cancelación a traves de la agencia");
                            cParametros.DatoAdic = sResponse;
                            cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                            cParametros.Severity = clsSeveridad.Alta;
                            cParametros.Tipo = clsTipoError.WebServices;
                            ExceptionHandled.Publicar(cParametros);
                        }
                        else
                        {
                            bCancel = true;
                        }
                    }
                }
                catch { }
            }
            if (bCancel)
            {
                OTA_CancelRS oOTA_CancelRS = new clsOTA_TravelItineraryRead()._Sabre_CancelRecord();
                if (oOTA_CancelRS != null)
                {
                    if (oOTA_CancelRS.Success != null)
                    {
                        string Comando = Negocios_WebServiceSabreCommand._EjecutarComando("6P");
                        Comando += Negocios_WebServiceSabreCommand._EjecutarComando("ER");
                        new clsEndTransactionLLS()._CerrarReserva(ref Record_);
                        if (Comando.Length > 0)
                        {
                            cParametros.Id = 1;
                            cParametros.ViewMessage.Add("Reserva Cancelada con Exito");
                            cParametros.Sugerencia.Add("");
                            cParametros.DatoAdic = string.Empty;
                        }
                        else
                        {
                            cParametros.Id = 0;
                            cParametros.Info = "Reserva " + Record_ + " no fue cancelada";
                            cParametros.Message = "Reserva " + Record_ + " no fue cancelada";
                            cParametros.ViewMessage.Add("Reserva " + Record_ + " no fue cancelada");
                            cParametros.Sugerencia.Add("Por favor tramite la cancelación a traves de la agencia");
                            cParametros.DatoAdic = string.Empty;
                            cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                            cParametros.Severity = clsSeveridad.Alta;
                            cParametros.Tipo = clsTipoError.WebServices;
                            ExceptionHandled.Publicar(cParametros);
                        }
                    }
                    else
                    {
                        cParametros.Id = 0;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        cParametros.Code = oOTA_CancelRS.Errors.Error.ErrorCode;
                        cParametros.Info = oOTA_CancelRS.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = oOTA_CancelRS.Errors.Error.ErrorMessage;
                        cParametros.Severity = oOTA_CancelRS.Errors.Error.Severity;
                        cParametros.DatoAdic = string.Empty;
                        cParametros.ViewMessage.Add("Reserva " + Record_ + " no fue cancelada");
                        cParametros.Sugerencia.Add("");
                        ExceptionHandled.Publicar(cParametros);
                    }
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Info = "Reserva " + Record_ + " no fue cancelada";
                    cParametros.Message = "Reserva " + Record_ + " no fue cancelada";
                    cParametros.ViewMessage.Add("Reserva " + Record_ + " no fue cancelada");
                    cParametros.Sugerencia.Add("Por favor tramite la cancelación a traves de la agencia");
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.DatoAdic = string.Empty;
                    cParametros.Tipo = clsTipoError.WebServices;
                    ExceptionHandled.Publicar(cParametros);
                }
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            cParametros.Id = 0;
            cParametros.Complemento = "Hubo una excepcion tratando cancelar la reserva";
            cParametros.Message = Ex.Message;
            cParametros.Metodo = Ex.TargetSite.Name;
            cParametros.Source = Ex.Source;
            cParametros.StackTrace = Ex.StackTrace;
            cParametros.ViewMessage.Add("Reserva " + Record_ + " no se cancelo");
            cParametros.Sugerencia.Add("");
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.Tipo = clsTipoError.WebServices;
            cParametros.DatoAdic = string.Empty;
            cParametros.Ex = Ex;
            ExceptionHandled.Publicar(cParametros);
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        try { Negocios_WebServiceSession._CerrarSesion(); }
        catch { }
        return cParametros;
    }
    public String getValorUSD()
    {
        string sUSD = String.Empty;

        if (Ssoft.Utils.clsSesiones.GET_USD_SABRE() == null || Ssoft.Utils.clsSesiones.GET_USD_SABRE().Equals(string.Empty))
        {
            string sValor = clsValidaciones.GetKeyOrAdd("Sabre_ta", "2000");
            clsVuelos objVuelos = new clsVuelos();
            sUSD = objVuelos.GetPrecioDolar();
            if (sUSD.Equals(string.Empty))
            {
                try
                {
                    string Comando_ = Negocios_WebServiceSabreCommand._EjecutarComando("DC¥USD1.00/COP");
                    if (Comando_ != null)
                    {
                        sUSD = clsValidaciones._Seleccionar_PrecioDolar(Comando_);
                        Ssoft.Utils.clsSesiones.SET_USD_SABRE(sUSD);
                        try
                        {
                            objVuelos.InsertUsdIata(DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToLongTimeString(), sUSD);
                        }
                        catch { }
                    }
                }
                catch
                {
                    Ssoft.Utils.clsSesiones.SET_USD_SABRE(sValor);
                    sUSD = sValor;
                }
            }
            else
            {
                Ssoft.Utils.clsSesiones.SET_USD_SABRE(sUSD);
            }
        }
        else
        {
            sUSD = Ssoft.Utils.clsSesiones.GET_USD_SABRE();
        }

        return sUSD;
    }
    public String _AgregarInformacionPNR(List<VO_DataTravelItineraryAddInfo> InformacionItinerario_)
    {
        /*ENVIAMOS LA INFORMACION DE LOS PASAJEROS DEL ITINERARIO SELECCIONADO*/
        string Datos_ = null;

        try
        {

            WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRS TravelResultado_ = new clsTravelItineraryAddInfoLLSRQ()._Sabre_AgregarInformacionPNR(InformacionItinerario_);

            if (TravelResultado_ != null)
            {
                if (TravelResultado_.TPA_Extensions != null)
                {
                    Datos_ = TravelResultado_.TPA_Extensions.HostCommand;
                }
                else
                {
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRSErrorsError Error_ = TravelResultado_.Errors.Error;
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRSErrorsErrorErrorInfo ErrorInfo_ = Error_.ErrorInfo;

                    if (TravelResultado_.Errors != null)
                    {
                        clsParametros objParametros = new clsParametros();
                        objParametros.Id = 0;
                        objParametros.Code = TravelResultado_.Errors.Error.ErrorCode;
                        objParametros.Message = TravelResultado_.Errors.Error.ErrorInfo.Message;
                        objParametros.InnerException = TravelResultado_.Errors.Error.ErrorMessage;
                        objParametros.Severity = TravelResultado_.Errors.Error.Severity;
                        ExceptionHandled.Publicar(objParametros);

                    }
                }
            }
        }
        catch (Exception Ex)
        {
            Datos_ = null;
            ExceptionHandled.Publicar(Ex);
        }

        return Datos_;
    }
    public bool SetShorSell(RepeaterCommandEventArgs e, UserControl ucControl, HtmlGenericControl dPanel)
    {
        string strMensaje = string.Empty;
        string sVenta = string.Empty;
        string sItinerario = string.Empty;
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
        bool bItinerario = false;
        try
        {
            string intIdItinerario = e.CommandArgument.ToString();
            try
            {
                string sWhere = "SequenceNumber = " + e.CommandArgument.ToString();
                DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
                DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
                DataTable dtTablaRetornada = clsDataNet.dsDataWhere(sWhere, dtPricedItinerary);
                intIdItinerario = dtTablaRetornada.Rows[0]["PricedItinerary_Id"].ToString();
            }
            catch { }
            string strTipoPlan = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
            DataTable dtItinerario = new DataTable();
            dtItinerario = new clsVuelos().GetDtGetItinerario(intIdItinerario);

            Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno("XIA");
            sVenta = Negocios_WebServiceSabreCommand._EjecutarComando("JR0" + e.CommandArgument.ToString());
            try
            {
                objParametros.Id = 1;
                objParametros.TipoLog = Enum_Error.Transac;
                objParametros.Severity = clsSeveridad.Media;
                objParametros.Tipo = clsTipoError.WebServices;
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objParametros.Complemento = "HostCommand: JR0" + e.CommandArgument.ToString() + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                try
                {
                    objParametros.TargetSite = "Response  " + sVenta;
                    clsCache cCache = new csCache().cCache();
                    if (cCache != null)
                    {
                        objParametros.Source = "Sesion Local: " + cCache.SessionID.ToString();
                    }
                    else
                    {
                        objParametros.Source = "Sesion Local: No hay cache ";
                    }
                }
                catch
                {
                    objParametros.Source = "Sesion Local: Error ";
                }
                ExceptionHandled.Publicar(objParametros);
                objParametros.TipoLog = Enum_Error.Log;
            }
            catch { }
            if (string.IsNullOrEmpty(sVenta))
            {
                objParametros.Id = 0;
                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                objParametros.Sugerencia.Add("Por favor intente de nuevo");
                objParametros.Severity = clsSeveridad.Media;
                objParametros.Tipo = clsTipoError.WebServices;
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objParametros.Complemento = "HostCommand: JR0" + e.CommandArgument.ToString() + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                objErrorMensaje.getError(objParametros, dPanel);
                ExceptionHandled.Publicar(objParametros);
            }
            else
            {
                if (sVenta.Trim().Contains("NO JOURNEY RECORD PRESENT"))
                {
                    objParametros.Id = 0;
                    objParametros.Info = sVenta;
                    objParametros.Message = sVenta;
                    objParametros.ViewMessage.Add("No se encontraron vuelos disponibles.");
                    objParametros.Sugerencia.Add("Por favor intente de nuevo");
                    objParametros.Severity = clsSeveridad.Media;
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objParametros.Tipo = clsTipoError.WebServices;
                    objParametros.Complemento = "HostCommand: JR0" + e.CommandArgument.ToString() + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                    objErrorMensaje.getError(objParametros, dPanel);
                    ExceptionHandled.Publicar(objParametros);
                }
                else
                {
                    if (sVenta.Contains("UNABLE TO SELL SEGMENTS") || sVenta.Contains("ERROR"))
                    {
                        objParametros.Id = 0;
                        objParametros.Message = sVenta;
                        objParametros.Info = sVenta;
                        objParametros.ViewMessage.Add("No se encontraron vuelos disponibles.");
                        objParametros.Sugerencia.Add("Por favor intente de nuevo");
                        objParametros.Severity = clsSeveridad.Media;
                        objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        objParametros.Tipo = clsTipoError.WebServices;
                        objParametros.Complemento = "HostCommand: JR0" + e.CommandArgument.ToString() + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                        objErrorMensaje.getError(objParametros, dPanel);
                        ExceptionHandled.Publicar(objParametros);
                    }
                    else
                    {
                        bItinerario = true;
                        if (sVenta.Contains("VALIDATING CARRIER -"))
                        {
                            string strAerolinea = sVenta.Substring(sVenta.IndexOf("VALIDATING CARRIER -") + 21, 2);
                            clsSesiones.setAerolineaValidadora(strAerolinea);
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Source = Ex.Source;
            objParametros.StackTrace = Ex.StackTrace;
            if (objParametros.ViewMessage.Count.Equals(0))
            {
                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                objParametros.Sugerencia.Add("Por favor intente de nuevo");
            }
            else
            {
                objParametros.ViewMessage[0] = "No se encontraron vuelos disponibles";
                objParametros.Sugerencia[0] = "Por favor intente de nuevo";
            }
            objParametros.Severity = clsSeveridad.Media;
            objParametros.Tipo = clsTipoError.WebServices;
            objParametros.Complemento = "HostCommand: JR0" + e.CommandArgument.ToString() + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
            objErrorMensaje.getError(objParametros, dPanel);
            ExceptionHandled.Publicar(objParametros);
        }
        return bItinerario;
    }
    public bool SetShorSell(UserControl ucControl, HtmlGenericControl dPanel, string sRPH)
    {
        string strMensaje = string.Empty;
        string sVenta = string.Empty;
        string sItinerario = string.Empty;
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
        bool bItinerario = false;
        string intIdItinerario = sRPH;
        try
        {
            Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno("XIA");
            sVenta = Negocios_WebServiceSabreCommand._EjecutarComando("JR0" + intIdItinerario);
            try
            {
                objParametros.Id = 1;
                objParametros.TipoLog = Enum_Error.Transac;
                objParametros.Severity = clsSeveridad.Media;
                objParametros.Tipo = clsTipoError.WebServices;
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objParametros.Complemento = "HostCommand: JR0" + intIdItinerario + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                try
                {
                    objParametros.TargetSite = "Response  " + sVenta;
                    clsCache cCache = new csCache().cCache();
                    if (cCache != null)
                    {
                        objParametros.Source = "Sesion Local: " + cCache.SessionID.ToString();
                    }
                    else
                    {
                        objParametros.Source = "Sesion Local: No hay cache ";
                    }
                }
                catch
                {
                    objParametros.Source = "Sesion Local: Error ";
                }
                ExceptionHandled.Publicar(objParametros);
                objParametros.TipoLog = Enum_Error.Log;
            }
            catch { }
            if (string.IsNullOrEmpty(sVenta))
            {
                objParametros.Id = 0;
                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                objParametros.Sugerencia.Add("Por favor intente de nuevo");
                objParametros.Severity = clsSeveridad.Media;
                objParametros.Tipo = clsTipoError.WebServices;
                objParametros.Complemento = "HostCommand: JR0" + intIdItinerario + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objErrorMensaje.getError(objParametros, dPanel);
                ExceptionHandled.Publicar(objParametros);
            }
            else
            {
                if (sVenta.Trim().Contains("NO JOURNEY RECORD PRESENT"))
                {
                    objParametros.Id = 0;
                    objParametros.Info = sVenta;
                    objParametros.Message = sVenta;
                    objParametros.ViewMessage.Add("No se encontraron vuelos disponibles.");
                    objParametros.Sugerencia.Add("Por favor intente de nuevo");
                    objParametros.Severity = clsSeveridad.Media;
                    objParametros.Tipo = clsTipoError.WebServices;
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objParametros.Complemento = "HostCommand: JR0" + intIdItinerario + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                    objErrorMensaje.getError(objParametros, dPanel);
                    ExceptionHandled.Publicar(objParametros);
                }
                else
                {
                    if (sVenta.Contains("UNABLE TO SELL SEGMENTS") || sVenta.Contains("ERROR"))
                    {
                        objParametros.Id = 0;
                        objParametros.Message = sVenta;
                        objParametros.Info = sVenta;
                        objParametros.ViewMessage.Add("No se encontraron vuelos disponibles.");
                        objParametros.Sugerencia.Add("Por favor intente de nuevo");
                        objParametros.Severity = clsSeveridad.Media;
                        objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        objParametros.Tipo = clsTipoError.WebServices;
                        objParametros.Complemento = "HostCommand: JR0" + intIdItinerario + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                        objErrorMensaje.getError(objParametros, dPanel);
                        ExceptionHandled.Publicar(objParametros);
                    }
                    else
                    {
                        bItinerario = true;
                        if (sVenta.Contains("VALIDATING CARRIER -"))
                        {
                            string strAerolinea = sVenta.Substring(sVenta.IndexOf("VALIDATING CARRIER -") + 21, 2);
                            clsSesiones.setAerolineaValidadora(strAerolinea);
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Source = Ex.Source;
            objParametros.StackTrace = Ex.StackTrace;
            if (objParametros.ViewMessage.Count.Equals(0))
            {
                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                objParametros.Sugerencia.Add("Por favor intente de nuevo");
            }
            else
            {
                objParametros.ViewMessage[0] = "No se encontraron vuelos disponibles";
                objParametros.Sugerencia[0] = "Por favor intente de nuevo";
            }
            objParametros.Severity = clsSeveridad.Media;
            objParametros.Tipo = clsTipoError.WebServices;
            objParametros.Complemento = "HostCommand: JR0" + intIdItinerario + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
            objErrorMensaje.getError(objParametros, dPanel);
            ExceptionHandled.Publicar(objParametros);
        }
        return bItinerario;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ucControl"></param>
    /// <param name="dPanel"></param>
    /// <param name="sRPH"></param>
    /// <returns></returns>
    public void getBusqueda(UserControl ucControl)
    {
        VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
        if (vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda.Equals(Enum_OrigenBusqueda.Normal))
        {
            if (getBusquedaDisponibilidad(ucControl))
            {
                getBusquedaMostrar(ucControl);
            }
        }
        else
        {
            DataSet dsSabreAir = new DataSet();
            dsSabreAir = clsSesiones.GetDatasetSabreAir();
            if (dsSabreAir != null)
            {
                getBusquedaMostrar(ucControl);
            }
            else
            {
                if (getBusquedaDisponibilidad(ucControl))
                {
                    getBusquedaMostrar(ucControl);
                }
            }
        }
    }
    #region MultDestination 19 result
    /// <summary>
    /// Find result with mildiDestination
    /// hceron
    /// 23052013
    /// </summary>
    /// <param name="ucControl"></param>
    public void getBusquedaMulti(UserControl ucControl)
    {
        VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
        if (vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda.Equals(Enum_OrigenBusqueda.Normal))
        {
            if (getBusquedaDisponibilidadMulti(ucControl))
            {
                getBusquedaMostrarMulti(ucControl);
            }
        }
        else
        {
            DataSet dsSabreAir = new DataSet();
            dsSabreAir = clsSesiones.GetDatasetSabreAir();
            if (dsSabreAir != null)
            {
                getBusquedaMostrarMulti(ucControl);
            }
            else
            {
                if (getBusquedaDisponibilidadMulti(ucControl))
                {
                    getBusquedaMostrarMulti(ucControl);
                }
            }
        }
    }
    public bool getBusquedaDisponibilidadMulti(UserControl ucControl)
    {
        bool bDisponibilidad = false;
        try
        {
            HtmlGenericControl dPanel = ucControl.FindControl("dPanel") as HtmlGenericControl;

            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            Ssoft.Rules.Pagina.clsVuelos objVuelos = new clsVuelos();
            clsResultados objResultados = new clsResultados();
            Negocios_WebService_OTA_AirLowFareSearch objNegociosWebsServices = new Negocios_WebService_OTA_AirLowFareSearch();
            string sVuelosXhora = clsValidaciones.GetKeyOrAdd("sVueloXhoras", "False");
            if (sVuelosXhora.ToUpper().Equals("TRUE"))
            {
                if (vo_OTA_AirLowFareSearchLLSRQ.BHoras)
                {
                    SesionesWs.GuardarSession(vo_OTA_AirLowFareSearchLLSRQ);
                    objResultados = objNegociosWebsServices.GetDsBusquedaSabreAirHora(vo_OTA_AirLowFareSearchLLSRQ);

                    if (objResultados.Error.Id == 1)
                    {
                        DataSet dsSabreAir = objResultados.dsResultados;
                        objVuelos.ModificarDatasetSabreAirHoras(dsSabreAir, vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto, vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo);
                        Ssoft.Utils.clsSesiones.SetDatasetSabreAir(dsSabreAir);
                        bDisponibilidad = true;
                    }
                    else if (objResultados.Error.Id == 0)
                    {
                        clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
                        objErrorMensaje.getError(objResultados.Error, dPanel);
                        ExceptionHandled.Publicar(objResultados.Error.Ex);
                    }
                }
                else
                {

                    SesionesWs.GuardarSession(vo_OTA_AirLowFareSearchLLSRQ);
                    //WSDL otalowfaresearch
                    //hceron 19 result 23052013
                    objResultados = objNegociosWebsServices.GetDsBusquedaSabreAirMulti(vo_OTA_AirLowFareSearchLLSRQ);
                    if (objResultados.Error.Id == 1)
                    {
                        DataSet dsSabreAir = objResultados.dsResultados;
                        //WSDL ModificarDatasetSabreAir
                        //hceron 23052013
                        objVuelos.ModificarDatasetSabreAir(dsSabreAir, vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto, vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo);
                        Ssoft.Utils.clsSesiones.SetDatasetSabreAir(dsSabreAir);
                        bDisponibilidad = true;
                    }
                    else if (objResultados.Error.Id == 0)
                    {
                        clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
                        objErrorMensaje.getError(objResultados.Error, dPanel);
                        ExceptionHandled.Publicar(objResultados.Error.Ex);
                    }
                }
            }
            else
            {
                SesionesWs.GuardarSession(vo_OTA_AirLowFareSearchLLSRQ);
                objResultados = objNegociosWebsServices.GetDsBusquedaSabreAirMulti(vo_OTA_AirLowFareSearchLLSRQ);

                if (objResultados.Error.Id == 1)
                {
                    DataSet dsSabreAir = objResultados.dsResultados;
                    objVuelos.ModificarDatasetSabreAir(dsSabreAir, vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto, vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo);
                    Ssoft.Utils.clsSesiones.SetDatasetSabreAir(dsSabreAir);
                    bDisponibilidad = true;
                }
                else if (objResultados.Error.Id == 0)
                {
                    clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
                    objErrorMensaje.getError(objResultados.Error, dPanel);
                    ExceptionHandled.Publicar(objResultados.Error.Ex);
                }
            }
        }
        catch { }
        return bDisponibilidad;
    }
    public void getBusquedaMostrarMulti(UserControl ucControl)
    {
        HtmlGenericControl dPanel = ucControl.FindControl("dPanel") as HtmlGenericControl;
        DataSet dsSabreAir = new DataSet();
        dsSabreAir = clsSesiones.GetDatasetSabreAir();
        VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
        Ssoft.Rules.Pagina.clsVuelos objVuelos = new clsVuelos();
        string sVuelosXhora = clsValidaciones.GetKeyOrAdd("sVueloXhoras", "False");
        if (sVuelosXhora.ToUpper().Equals("TRUE"))
        {
            if (vo_OTA_AirLowFareSearchLLSRQ.BHoras)
            {
                objVuelos.InicializarPaginaHoras(ucControl, dsSabreAir);
            }
            else
            {
                objVuelos.InicializarPagina(ucControl, dsSabreAir);
            }
        }
        else
        {
            objVuelos.InicializarPagina(ucControl, dsSabreAir);
        }
    }
    #endregion
    public bool getBusquedaDisponibilidad(UserControl ucControl)
    {
        bool bDisponibilidad = false;
        try
        {
            HtmlGenericControl dPanel = ucControl.FindControl("dPanel") as HtmlGenericControl;

            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            Ssoft.Rules.Pagina.clsVuelos objVuelos = new clsVuelos();
            clsResultados objResultados = new clsResultados();
            Negocios_WebService_OTA_AirLowFareSearch objNegociosWebsServices = new Negocios_WebService_OTA_AirLowFareSearch();
            string sVuelosXhora = clsValidaciones.GetKeyOrAdd("sVueloXhoras", "False");
            if (sVuelosXhora.ToUpper().Equals("TRUE"))
            {
                if (vo_OTA_AirLowFareSearchLLSRQ.BHoras)
                {
                    SesionesWs.GuardarSession(vo_OTA_AirLowFareSearchLLSRQ);
                    objResultados = objNegociosWebsServices.GetDsBusquedaSabreAirHora(vo_OTA_AirLowFareSearchLLSRQ);

                    if (objResultados.Error.Id == 1)
                    {
                        DataSet dsSabreAir = objResultados.dsResultados;
                        objVuelos.ModificarDatasetSabreAirHoras(dsSabreAir, vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto, vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo);
                        Ssoft.Utils.clsSesiones.SetDatasetSabreAir(dsSabreAir);
                        bDisponibilidad = true;
                    }
                    else if (objResultados.Error.Id == 0)
                    {
                        clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
                        objErrorMensaje.getError(objResultados.Error, dPanel);
                        ExceptionHandled.Publicar(objResultados.Error.Ex);
                    }
                }
                else
                {

                    SesionesWs.GuardarSession(vo_OTA_AirLowFareSearchLLSRQ);
                    //WSDL BFM
                    //hceron
                    objResultados = objNegociosWebsServices.GetDsBusquedaSabreAirMax(vo_OTA_AirLowFareSearchLLSRQ);
                    if (objResultados.Error.Id == 1)
                    {
                        DataSet dsSabreAir = objResultados.dsResultados;
                        //Modificaion con BFM
                        //hceron
                        objVuelos.ModificarDatasetSabreBFMAir(dsSabreAir, vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto, vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo);
                        Ssoft.Utils.clsSesiones.SetDatasetSabreAir(dsSabreAir);
                        bDisponibilidad = true;
                    }
                    else if (objResultados.Error.Id == 0)
                    {
                        clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
                        objErrorMensaje.getError(objResultados.Error, dPanel);
                        ExceptionHandled.Publicar(objResultados.Error.Ex);
                    }
                }
            }
            else
            {
                SesionesWs.GuardarSession(vo_OTA_AirLowFareSearchLLSRQ);
                objResultados = objNegociosWebsServices.GetDsBusquedaSabreAir(vo_OTA_AirLowFareSearchLLSRQ);

                if (objResultados.Error.Id == 1)
                {
                    DataSet dsSabreAir = objResultados.dsResultados;
                    objVuelos.ModificarDatasetSabreAir(dsSabreAir, vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto, vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo);
                    Ssoft.Utils.clsSesiones.SetDatasetSabreAir(dsSabreAir);
                    bDisponibilidad = true;
                }
                else if (objResultados.Error.Id == 0)
                {
                    clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
                    objErrorMensaje.getError(objResultados.Error, dPanel);
                    ExceptionHandled.Publicar(objResultados.Error.Ex);
                }
            }
        }
        catch { }
        return bDisponibilidad;
    }
    public void getBusquedaMostrar(UserControl ucControl)
    {
        HtmlGenericControl dPanel = ucControl.FindControl("dPanel") as HtmlGenericControl;
        DataSet dsSabreAir = new DataSet();
        dsSabreAir = clsSesiones.GetDatasetSabreAir();
        VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
        Ssoft.Rules.Pagina.clsVuelos objVuelos = new clsVuelos();
        string sVuelosXhora = clsValidaciones.GetKeyOrAdd("sVueloXhoras", "False");
        if (sVuelosXhora.ToUpper().Equals("TRUE"))
        {
            if (vo_OTA_AirLowFareSearchLLSRQ.BHoras)
            {
                objVuelos.InicializarPaginaHoras(ucControl, dsSabreAir);
            }
            else
            {
                if (clsValidaciones.GetKeyOrAdd("bMostrarResultadosBFM", "False").ToUpper().Equals("TRUE"))
                    objVuelos.InicializarPaginaBFM(ucControl, dsSabreAir);
                else
                    objVuelos.InicializarPagina(ucControl, dsSabreAir);
            }
        }
        else
        {
            objVuelos.InicializarPagina(ucControl, dsSabreAir);
        }
    }
    public List<VO_TarifaPago> getBotonPagoReserva(string sRecord, string sTA, string sITA)
    {
        clsParametros cParametros = new clsParametros();
        clsResultados objResultados = new clsResultados();
        List<VO_TarifaPago> lvoTarifa = new List<VO_TarifaPago>();
        objResultados = GetDsBusquedaRecordSabreAir(sRecord);

        if (objResultados.Error.Id == 1)
        {

            string sEmail = clsValidaciones.GetKeyOrAdd("strEmailEnvio", "info@ssoftcolombia.com");
            try
            {
                string sEmailTemp = objResultados.dsResultados.Tables["Email"].Rows[0]["Email_Column"].ToString();
                sEmail = sEmailTemp.Replace("Â", "");
            }
            catch { }

            lvoTarifa = GetVoBusquedaTarifaSabreAir();
            try { Negocios_WebServiceSession._CerrarSesion(); }
            catch { }
            if (lvoTarifa.Count > 0)
            {
                Ssoft.ValueObjects.VO_TA vTa = new Ssoft.ValueObjects.VO_TA();
                vTa.DTA = decimal.Parse(clsValidaciones.RetornaNumero(sTA));
                vTa.DImpuesto = decimal.Parse(clsValidaciones.RetornaNumero(sITA));
                vTa.DTACop = decimal.Parse(clsValidaciones.RetornaNumero(sTA));
                vTa.DImpuestoCop = decimal.Parse(clsValidaciones.RetornaNumero(sITA));
                try
                {
                    for (int m = 0; m < lvoTarifa.Count; m++)
                    {
                        if (decimal.Parse(lvoTarifa[m].Tarifa.ToString()) > 0)
                            lvoTarifa[m].TaTotal = vTa;
                    }
                }
                catch { }
                try
                {
                    if (lvoTarifa[0].TipoVuelo.Equals(Enum_TipoVuelo.Nacional))
                    {
                        lvoTarifa[0].AerolineaValidadora = objResultados.dsResultados.Tables["MarketingAirline"].Rows[0]["Code"].ToString() + "N";
                    }
                    else
                    {
                        lvoTarifa[0].AerolineaValidadora = objResultados.dsResultados.Tables["MarketingAirline"].Rows[0]["Code"].ToString() + "I";
                    }
                    for (int m = 0; m < lvoTarifa.Count; m++)
                    {
                        lvoTarifa[m].Email = sEmail;
                    }
                }
                catch { }
            }
        }
        else if (objResultados.Error.Id == 0)
        {
            //clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
            //objErrorMensaje.getError(objResultados.Error, dPanel);
            ExceptionHandled.Publicar(objResultados.Error.Ex);
        }
        return lvoTarifa;
    }
    public clsResultados getSubirReserva(string sRecord, string sTA, string sITA)
    {
        clsParametros cParametros = new clsParametros();
        clsReservas cReserva = new clsReservas();
        clsResultados objResultados = new clsResultados();
        clsResultados objResultadosTarifa = new clsResultados();
        //Negocios_WebService_OTA_AirLowFareSearch objNegociosWebsServices = new Negocios_WebService_OTA_AirLowFareSearch();
        objResultados = GetDsBusquedaRecordSabreAir(sRecord);

        if (objResultados.Error.Id == 1)
        {
            DataSet dsSabreAir = objResultados.dsResultados;
            objResultadosTarifa = GetDsBusquedaTarifaSabreAir();
            DataSet dsSabreAirTarifa = objResultadosTarifa.dsResultados;
            Ssoft.Utils.clsSesiones.SetDatasetSabreAir(dsSabreAir);
            try { Negocios_WebServiceSession._CerrarSesion(); }
            catch { }
            objResultados = cReserva.GetReservaAirSabre(dsSabreAir, sTA, sITA, dsSabreAirTarifa);
            //objVuelos.InicializarPaginaHoras(ucControl, dsSabreAir);
        }
        else if (objResultados.Error.Id == 0)
        {
            //clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
            //objErrorMensaje.getError(objResultados.Error, dPanel);
            ExceptionHandled.Publicar(objResultados.Error.Ex);
        }
        return objResultados;
    }
    /// <summary>
    /// Proceso para subir reservas de sabre - SRSABRE
    /// </summary>
    /// <param name="sRecord">Record de la reserva</param>
    /// <param name="sTA">valor de TA</param>
    /// <param name="sITA">Valor de Iva de la TA</param>
    /// <param name="sProyecto">Proyecto a subir</param>
    /// <param name="sContacto">Contactoi a asociar</param>
    /// <returns>Clase de resultados, clsResultados con codigos de error</returns>
    ///<remarks>
    /// Autor:          José Faustino Posas
    /// Company:        Ssoft Colombia
    /// Fecha:          2012-09-13
    /// -------------------
    /// Control de Cambios
    /// -------------------
    /// Autor:          
    /// Fecha:         
    /// Descripción:    
    /// </remarks>
    public clsResultados getSubirReserva(string sRecord, string sTA, string sITA, string sProyecto, string sContacto)
    {
        clsParametros cParametros = new clsParametros();
        clsReservas cReserva = new clsReservas();
        clsResultados objResultados = new clsResultados();
        clsResultados objResultadosTarifa = new clsResultados();
        //Negocios_WebService_OTA_AirLowFareSearch objNegociosWebsServices = new Negocios_WebService_OTA_AirLowFareSearch();
        objResultados = GetDsBusquedaRecordSabreAir(sRecord);

        if (objResultados.Error.Id == 1)
        {
            DataSet dsSabreAir = objResultados.dsResultados;
            objResultadosTarifa = GetDsBusquedaTarifaSabreAir();
            if (objResultadosTarifa.Error.Id == 0)
            {
                Negocios_WebServiceSabreCommand.setPQ();
                objResultadosTarifa = GetDsBusquedaTarifaSabreAir();
            }
            DataSet dsSabreAirTarifa = objResultadosTarifa.dsResultados;
            Ssoft.Utils.clsSesiones.SetDatasetSabreAir(dsSabreAir);
            try { Negocios_WebServiceSession._CerrarSesion(); }
            catch { }
            if (objResultadosTarifa.Error.Id == 1)
            {
                objResultados = cReserva.GetReservaAirSabre(dsSabreAir, sTA, sITA, dsSabreAirTarifa, sProyecto, sContacto);
            }
            else
            {
                try
                {
                    objResultados.Error = objResultadosTarifa.Error;
                }
                catch { }
            }
            //objVuelos.InicializarPaginaHoras(ucControl, dsSabreAir);
        }
        else if (objResultados.Error.Id == 0)
        {
            //clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
            //objErrorMensaje.getError(objResultados.Error, dPanel);
            ExceptionHandled.Publicar(objResultados.Error.Ex);
        }
        return objResultados;
    }  
    public void getVenta()
    {
        try
        {
            clsResultados objResultados = new clsResultados();
            VO_OTA_AirBookRQ vo_OTA_AirBookRQ = clsSesiones.getParametrosAirHoras();
            objResultados = GetDsVentaSabreAirHora(vo_OTA_AirBookRQ);
        }
        catch { }
    }
    public void getVentaSegmento(HtmlGenericControl dPanel)
    {
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
        try
        {
            VO_OTA_AirBookRQ vo_OTA_AirBookRQ = clsSesiones.getParametrosAirHoras();
            objResultados = GetDsVentaSabreAirHoraSegmento(vo_OTA_AirBookRQ);
            if (objResultados.Error.Id.Equals(0))
            {
                objErrorMensaje.getError(objResultados.Error, dPanel);
            }
        }
        catch (Exception Ex)
        {
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Source = Ex.Source;
            objParametros.StackTrace = Ex.StackTrace;
            if (objParametros.ViewMessage.Count.Equals(0))
            {
                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                objParametros.Sugerencia.Add("Por favor intente de nuevo");
            }
            else
            {
                objParametros.ViewMessage[0] = "No se encontraron vuelos disponibles";
                objParametros.Sugerencia[0] = "Por favor intente de nuevo";
            }
            objParametros.Severity = clsSeveridad.Media;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
            objErrorMensaje.getError(objParametros, dPanel);
        }
    }
    public void getVentaSegmentoCommand(HtmlGenericControl dPanel)
    {
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
        try
        {
            VO_OTA_AirBookRQ vo_OTA_AirBookRQ = clsSesiones.getParametrosAirHoras();
            objResultados = GetDsVentaSabreAirHoraCommand(vo_OTA_AirBookRQ);
            if (objResultados.Error.Id.Equals(0))
            {
                objErrorMensaje.getError(objResultados.Error, dPanel);
            }
        }
        catch (Exception Ex)
        {
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Source = Ex.Source;
            objParametros.StackTrace = Ex.StackTrace;
            if (objParametros.ViewMessage.Count.Equals(0))
            {
                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                objParametros.Sugerencia.Add("Por favor intente de nuevo");
            }
            else
            {
                objParametros.ViewMessage[0] = "No se encontraron vuelos disponibles";
                objParametros.Sugerencia[0] = "Por favor intente de nuevo";
            }
            objParametros.Severity = clsSeveridad.Media;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
            objErrorMensaje.getError(objParametros, dPanel);
        }
    }
    public void getVenta(string iItinerario, string sRPH, HtmlGenericControl dPanel)
    {
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        clsErrorMensaje objErrorMensaje = new clsErrorMensaje();

        try
        {
            string[] slRPH = new string[4];
            int iPos = int.Parse(iItinerario) - 1;
            slRPH[0] = iItinerario;
            slRPH[1] = sRPH;
            VO_OTA_AirBookRQ vo_OTA_AirBookRQ = clsSesiones.getParametrosAirHoras();
            slRPH[2] = vo_OTA_AirBookRQ.Lvo_OrigenDestinationOption[iPos].Lvo_AirItinerary[0].SNroPassenger;
            slRPH[3] = vo_OTA_AirBookRQ.Lvo_OrigenDestinationOption[iPos].Lvo_AirItinerary[0].SClase;

            objResultados = GetDsVentaSabreAirHora(slRPH);
            if (objResultados.Error.Id.Equals(0))
            {
                objErrorMensaje.getError(objResultados.Error, dPanel);
            }
        }
        catch (Exception Ex)
        {
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Source = Ex.Source;
            objParametros.StackTrace = Ex.StackTrace;
            if (objParametros.ViewMessage.Count.Equals(0))
            {
                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                objParametros.Sugerencia.Add("Por favor intente de nuevo");
            }
            else
            {
                objParametros.ViewMessage[0] = "No se encontraron vuelos disponibles";
                objParametros.Sugerencia[0] = "Por favor intente de nuevo";
            }
            objParametros.Severity = clsSeveridad.Media;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
            objErrorMensaje.getError(objParametros, dPanel);
        }
    }
    public void getCotizar(UserControl PageSource, HtmlGenericControl dPanel)
    {
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
        clsVuelos objVuelos = new clsVuelos();
        VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
        try
        {
            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirProceRQ = clsSesiones.getParametrosAirBargain(); ;
                objResultados = GetDsCotizaSabreAir(vo_OTA_AirProceRQ);

                if (objResultados.Error.Id == 1)
                {
                    DataSet dsSabreAir = objResultados.dsResultados;
                    objVuelos.ModificarDatasetSabreAirCotiza(dsSabreAir, vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto, vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo);
                    clsSesiones.SetDatasetSabreAir(dsSabreAir);
                    //HttpContext.Current.Response.Redirect("Login.aspx?idSesion=" + cCache.SessionID, false);
                }
                else if (objResultados.Error.Id == 0)
                {
                    objErrorMensaje.getError(objResultados.Error, dPanel);
                    ExceptionHandled.Publicar(objResultados.Error.Ex);
                }
            }
        }
        catch (Exception Ex)
        {
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Source = Ex.Source;
            objParametros.StackTrace = Ex.StackTrace;
            if (objParametros.ViewMessage.Count.Equals(0))
            {
                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                objParametros.Sugerencia.Add("Por favor intente de nuevo");
            }
            else
            {
                objParametros.ViewMessage[0] = "No se encontraron vuelos disponibles";
                objParametros.Sugerencia[0] = "Por favor intente de nuevo";
            }
            objParametros.Severity = clsSeveridad.Media;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
            objErrorMensaje.getError(objParametros, dPanel);
        }
    }
    public void getCotizarPlan(UserControl PageSource, HtmlGenericControl dPanel)
    {
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();
        clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
        clsVuelos objVuelos = new clsVuelos();
        VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
        try
        {
            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirProceRQ = clsSesiones.getParametrosAirBargain(); ;
                objResultados = GetDsCotizaSabreAir(vo_OTA_AirProceRQ);

                if (objResultados.Error.Id == 1)
                {
                    DataSet dsSabreAir = objResultados.dsResultados;
                    objVuelos.ModificarDatasetSabreAirCotiza(dsSabreAir, vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto, vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo);
                    try
                    {
                        objVuelos.InicializarPaginaPlanes(PageSource, dsSabreAir);
                    }
                    catch { }
                }
                else if (objResultados.Error.Id == 0)
                {
                    objErrorMensaje.getError(objResultados.Error, dPanel);
                    ExceptionHandled.Publicar(objResultados.Error.Ex);
                }
            }
        }
        catch (Exception Ex)
        {
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            objParametros.Source = Ex.Source;
            objParametros.StackTrace = Ex.StackTrace;
            if (objParametros.ViewMessage.Count.Equals(0))
            {
                objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                objParametros.Sugerencia.Add("Por favor intente de nuevo");
            }
            else
            {
                objParametros.ViewMessage[0] = "No se encontraron vuelos disponibles";
                objParametros.Sugerencia[0] = "Por favor intente de nuevo";
            }
            objParametros.Severity = clsSeveridad.Media;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
            objErrorMensaje.getError(objParametros, dPanel);
        }
    }
    public clsResultados getBusqueda(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        Ssoft.Rules.Pagina.clsVuelos objVuelos = new clsVuelos();
        Negocios_WebService_OTA_AirLowFareSearch objNegociosWebsServices = new Negocios_WebService_OTA_AirLowFareSearch();
        clsResultados objResultados = objNegociosWebsServices.GetDsBusquedaSabreAir(vo_OTA_AirLowFareSearchLLSRQ);

        if (objResultados.Error.Id == 1)
        {
            DataSet dsSabreAir = objResultados.dsResultados;
            objVuelos.ModificarDatasetSabreAir(dsSabreAir, vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto, vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo);
            objResultados.dsResultados = dsSabreAir;
        }
        return objResultados;
    }
    private void GetDatosAdicionales(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        Enum_TipoTrayecto EnumTipoTrayecto = vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto;
        Enum_TipoVuelo EnumTipoVuelo = vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo;
    }
    /*-----------ENVIAR PASAJEROS----------------*/
    public void setPasajeros(List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo)
    {
        // Utilizado para hacer seguimiento del itinerario
        clsParametros cParametros = new clsParametros();
        string sItinerario = string.Empty;
        // sItinerario = Negocios_WebServiceSabreCommand._EjecutarComando("*I");
        cParametros.Id = 0;
        cParametros.Message = "Itinerario";
        cParametros.Severity = clsSeveridad.Baja;
        cParametros.Tipo = clsTipoError.WebServices;
        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
        cParametros.Complemento = sItinerario;
        try
        {
            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {
                cParametros.Source = "Sesion Local: " + cCache.SessionID.ToString();
            }
            else
            {
                cParametros.Source = "Sesion Local: No hay cache ";
            }
        }
        catch
        {
            cParametros.Source = "Sesion Local: Error ";
        }
        ExceptionHandled.Publicar(cParametros);
        // termina seguimiento
        _AgregarInformacionPNR(lvo_DataTravelItineraryAddInfo);
        /*SE QUITO PARA PROBAR SIN LOS COMANDOS DE DE LOS NIÑOS E INFANTES */
        try
        {
            setFechas(lvo_DataTravelItineraryAddInfo);
        }
        catch { }
        try
        {
            setFoid(lvo_DataTravelItineraryAddInfo);
        }
        catch { }
        try
        {
            setInfantes(lvo_DataTravelItineraryAddInfo);
        }
        catch { }
    }
    private void setFechas(List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo)
    {
        if (lvo_DataTravelItineraryAddInfo != null)
        {
            int iPosision = 1;
            foreach (VO_DataTravelItineraryAddInfo vo_DataTravelItineraryAddInfo in lvo_DataTravelItineraryAddInfo)
            {
                if (!vo_DataTravelItineraryAddInfo.Infante_)
                {
                    if (!vo_DataTravelItineraryAddInfo.Fecha_.Length.Equals(0))
                    {
                        string sSexo = "M";
                        try
                        {
                            if (vo_DataTravelItineraryAddInfo.Genero_.Contains("F"))
                                sSexo = "F";
                        }
                        catch { }
                        string sComando = "3DOCSA/DB/" + vo_DataTravelItineraryAddInfo.Fecha_ + "/" + sSexo + "/" + vo_DataTravelItineraryAddInfo.Apellido_ + "/" + vo_DataTravelItineraryAddInfo.Nombre_ + "-" + iPosision + ".1";
                        string sComandoAA = "4DOCSA/DB/" + vo_DataTravelItineraryAddInfo.Fecha_ + "/" + sSexo + "/" + vo_DataTravelItineraryAddInfo.Apellido_ + "/" + vo_DataTravelItineraryAddInfo.Nombre_ + "-" + iPosision + ".1";
                        string srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                        if (!srespuesta.Contains("* \n"))
                            srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComandoAA);
                    }
                }
                iPosision++;
            }
        }
    }
    private void setFoid(List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo)
    {
        if (lvo_DataTravelItineraryAddInfo != null)
        {
            int iPosision = 1;
            foreach (VO_DataTravelItineraryAddInfo vo_DataTravelItineraryAddInfo in lvo_DataTravelItineraryAddInfo)
            {
                if (vo_DataTravelItineraryAddInfo.Documento_ != null)
                {
                    if (vo_DataTravelItineraryAddInfo.Documento_.Length > 0)
                    {
                        string sComando = "3FOID/NI" + vo_DataTravelItineraryAddInfo.Documento_ + "-" + iPosision + ".1";
                        string sComandoAA = "4FOID/NI" + vo_DataTravelItineraryAddInfo.Documento_ + "-" + iPosision + ".1";
                        string srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                        if (!srespuesta.Contains("* \n"))
                            srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComandoAA);
                    }
                }
                iPosision++;
            }
        }
    }
    private void setInfantes(List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo, List<string> lsEdadesInfantes)
    {
        if (lvo_DataTravelItineraryAddInfo != null && lsEdadesInfantes != null)
        {
            int iPosision = 1;
            int iContador = 0;
            foreach (VO_DataTravelItineraryAddInfo vo_DataTravelItineraryAddInfo in lvo_DataTravelItineraryAddInfo)
            {
                if (vo_DataTravelItineraryAddInfo.Infante_)
                {
                    string sEdad = getValidadEdad(lsEdadesInfantes[iContador]);
                    string sComando = "3INFT/" + vo_DataTravelItineraryAddInfo.Apellido_ + "/" + vo_DataTravelItineraryAddInfo.Nombre_ + "/" + vo_DataTravelItineraryAddInfo.Fecha_ + "-" + iPosision + ".1";
                    string sComandoAA = "4INFT/" + vo_DataTravelItineraryAddInfo.Apellido_ + "/" + vo_DataTravelItineraryAddInfo.Nombre_ + "/" + vo_DataTravelItineraryAddInfo.Fecha_ + "-" + iPosision + ".1";
                    string srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                    if (!srespuesta.Contains("* \n"))
                        srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComandoAA);
                    iContador++;
                }
                iPosision++;
            }
        }
    }
    private void setInfantes(List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo)
    {
        if (lvo_DataTravelItineraryAddInfo != null)
        {
            int iPosision = 1;
            foreach (VO_DataTravelItineraryAddInfo vo_DataTravelItineraryAddInfo in lvo_DataTravelItineraryAddInfo)
            {
                if (vo_DataTravelItineraryAddInfo.Infante_)
                {
                    string sComando = "3INFT/" + vo_DataTravelItineraryAddInfo.Apellido_ + "/" + vo_DataTravelItineraryAddInfo.Nombre_ + "/" + vo_DataTravelItineraryAddInfo.Fecha_ + "-" + iPosision + ".1";
                    string sComandoAA = "4INFT/" + vo_DataTravelItineraryAddInfo.Apellido_ + "/" + vo_DataTravelItineraryAddInfo.Nombre_ + "/" + vo_DataTravelItineraryAddInfo.Fecha_ + "-" + iPosision + ".1";
                    string srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                    if (!srespuesta.Contains("* \n"))
                        srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComandoAA);
                }
                iPosision++;
            }
        }
    }
    private void setNinios(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        if (vo_OTA_AirLowFareSearchLLSRQ != null && vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios != null)
        {
            int iInicioNinios = getInicioNinios(vo_OTA_AirLowFareSearchLLSRQ);
            List<string> lsEdadesNinios = vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios;
            foreach (string sEdadesNinios in lsEdadesNinios)
            {
                string sEdad = getValidadEdad(sEdadesNinios);

                string sComando = "PDTC" + sEdad + "-" + iInicioNinios + ".1";
                string srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                iInicioNinios++;
            }
        }
    }
    private string getValidadEdad(string sEdad)
    {
        string sEdadTotal = sEdad;
        if (sEdad.Length.Equals(1))
        {
            sEdadTotal = "0" + sEdad;
        }
        return sEdadTotal;
    }
    private int getInicioNinios(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        int iInicio = 0;
        int.TryParse(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[0].SCantidad, out iInicio);

        iInicio++;

        return iInicio;
    }
    /*------------RESULTADOS__RESERVAS--------*/
    public void GetResultadosReserva(UserControl ucControl)
    {
        /*OBTENEMOS LAS POLITICAS DE SABRE*/
        //clsRulesFromPrice oclsRulesFromPrice = new clsRulesFromPrice();
        //oclsRulesFromPrice.StrSesion = AutenticacionSabre.GET_SabreSession();
        //String str_Rules_Sabre = oclsRulesFromPrice.getRulesCommand();
        //clsSesiones.setRulesFromPrice(str_Rules_Sabre);
        /*ESTABLECEMOS LA FECHA LIMITE DE TIQUETE*/
        clsValidacionesVuelos.setFechaLimiteTiquete();
        clsVuelos objVuelos = new clsVuelos();
        VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();

        string sVuelosXhora = clsValidaciones.GetKeyOrAdd("sVueloXhoras", "False");
        if (sVuelosXhora.ToUpper().Equals("TRUE"))
        {
            if (vo_OTA_AirLowFareSearchLLSRQ.BHoras)
            {
                objVuelos.InicializarPaginaReservaCotiza(ucControl, CrearListaPasajeros(vo_OTA_AirLowFareSearchLLSRQ));
            }
            else
            {
                objVuelos.InicializarPaginaReserva(ucControl, CrearListaPasajeros(vo_OTA_AirLowFareSearchLLSRQ));
            }
        }
        else
        {
            objVuelos.InicializarPaginaReserva(ucControl, CrearListaPasajeros(vo_OTA_AirLowFareSearchLLSRQ));
        }
    }
    /// <summary>
    /// metodo pendiente por revision
    /// </summary>
    /// <param name="vo_OTA_AirLowFareSearchLLSRQ"></param>
    /// <returns></returns>
    private List<clsPassenger> CrearListaPasajeros(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
    {
        clsVuelos objVuelos = new clsVuelos();
        DataTable dtPasajeros = objVuelos.CreateTablePassenger();
        /*CREAMOS UNA LISTA DE clsPassenger PARA ALMECENAR LAS EDADES DE LOS NIÑOS EN INFANTES QUE ESTAN EN VO_OTA_AirLowFareSearchLLSRQ*/
        List<clsPassenger> objListaObjPasajeros = new List<clsPassenger>();
        /*EDADES ADULTOS*/
        string strEdad = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[0].SCantidad;
        string strCodigo = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[0].SCodigo;
        //clsCache cCache = new csCache().cCache();
        //if (cCache.Passenger != null)
        //{
        //    objListaObjPasajeros = cCache.Passenger;
        //}
        //else
        //{
        if (clsValidaciones.getValidarString(vo_OTA_AirLowFareSearchLLSRQ.SCodPaxNegociada))
        {
            foreach (VO_Pasajero Pasajero in vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros)
            {
                clsPassenger objPassengerAdulto = new clsPassenger();
                objPassengerAdulto.Pos = vo_OTA_AirLowFareSearchLLSRQ.SCodPaxNegociada;
                clsEdad objEdad = new clsEdad();
                objEdad.Edad = "0";
                objPassengerAdulto.Edad = new List<clsEdad>();
                objPassengerAdulto.Edad.Add(objEdad);
                objListaObjPasajeros.Add(objPassengerAdulto);
            }
        }
        else
        {
            if (vo_OTA_AirLowFareSearchLLSRQ.CodigoPlan == null)
            {
                if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros != null && vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros.Count > 0)
                {
                    foreach (VO_Pasajero Pasajero in vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros)
                    {
                        if (Pasajero.SCodigo == "ADT")
                        {
                            clsPassenger objPassengerAdulto = new clsPassenger();
                            objPassengerAdulto.Pos = "ADT";
                            clsEdad objEdad = new clsEdad();
                            objEdad.Edad = "0";
                            objPassengerAdulto.Edad = new List<clsEdad>();
                            objPassengerAdulto.Edad.Add(objEdad);
                            objListaObjPasajeros.Add(objPassengerAdulto);
                        }
                    }
                    //}
                }
            }
            else
            {
             
                string sTipoProducto = clsValidaciones.GetKeyOrAdd("Producto", "ProductoID");
                string sProductoId = clsValidaciones.GetKeyOrAdd("ProductoRelacionOfertasWS", "tblOfertasWS");
                string iProducto = "0";
                //tblRefere otblRefere = new tblRefere();
                //otblRefere.Get(sTipoProducto, sProductoId);
                //if (otblRefere.Respuesta)
                //    iProducto = otblRefere.intidRefere.Value;

                string sIdioma = clsSesiones.getIdioma();
                int iAplicacion = clsSesiones.getAplicacion();

                //DataTable dtData = cPlanes.ConsultarRelacionesPlanPax(vo_OTA_AirLowFareSearchLLSRQ.CodigoPlan, iAplicacion, iProducto, sIdioma);
                DataTable dtData = null;
                foreach (VO_Pasajero Pasajero in vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros)
                {
                    string sWhere = string.Empty;
                    sWhere = "strValor = '" + Pasajero.SCodigo + "'";
                    DataTable dtDataTemp = clsDataNet.dsDataWhere(sWhere, dtData);

                    if (dtDataTemp.Rows[0]["strRefere"].ToString().ToUpper().Contains("ADT"))
                    {
                        clsPassenger objPassengerAdulto = new clsPassenger();
                        objPassengerAdulto.Pos = "ADT";
                        clsEdad objEdad = new clsEdad();
                        objEdad.Edad = "0";
                        objPassengerAdulto.Edad = new List<clsEdad>();
                        objPassengerAdulto.Edad.Add(objEdad);
                        objListaObjPasajeros.Add(objPassengerAdulto);
                    }
                }
            }
            /*EDADES NIÑOS*/
            if (vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios != null && vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios.Count > 0)
            {
                foreach (string intEdad in vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios)
                {
                    clsPassenger objPassengerNinos = new clsPassenger();
                    objPassengerNinos.Pos = "CNN";
                    clsEdad objEdad = new clsEdad();
                    objEdad.Edad = intEdad;
                    objPassengerNinos.Edad = new List<clsEdad>();
                    objPassengerNinos.Edad.Add(objEdad);
                    objListaObjPasajeros.Add(objPassengerNinos);
                }
            }

            /*EDADES INFANTES*/
            if (vo_OTA_AirLowFareSearchLLSRQ.LsEdadesInfantes != null && vo_OTA_AirLowFareSearchLLSRQ.LsEdadesInfantes.Count > 0)
            {
                foreach (string intEdad in vo_OTA_AirLowFareSearchLLSRQ.LsEdadesInfantes)
                {
                    clsPassenger objPassengerInfantes = new clsPassenger();
                    objPassengerInfantes.Pos = "INF";
                    clsEdad objEdad = new clsEdad();
                    objEdad.Edad = intEdad;
                    objPassengerInfantes.Edad = new List<clsEdad>();
                    objPassengerInfantes.Edad.Add(objEdad);
                    objListaObjPasajeros.Add(objPassengerInfantes);
                }
            }
        }
        return objListaObjPasajeros;
    }
    private List<VO_DataTravelItineraryAddInfo> GetListaDatosPasajeros()
    {
        List<VO_DataTravelItineraryAddInfo> listaPasajeros = new List<VO_DataTravelItineraryAddInfo>();
        VO_DataTravelItineraryAddInfo vo_DataTravelItineraryAddInfo;
        clsVuelos objVuelos = new clsVuelos();
        /*OBTENEMOS LOS DATOS DE LOS PASAJEROS INGRESADOS A LA TABLA DE PASAJEROS DEL DATASET*/
        DataTable dtPasajeros = objVuelos.GetDtPasajeros();
        /*RECORREMOS EL DATATABLE Y ASIGNAMOS LOS VALORES A vo_DataTravelItineraryAddInfo */
        if (dtPasajeros != null && dtPasajeros.Rows.Count > 0)
        {
            foreach (DataRow drFila in dtPasajeros.Rows)
            {
                string strNombre = string.Empty;
                string sTipoPax = drFila["strTipoPasajero"].ToString();
                try
                {
                    sTipoPax = drFila["strCode"].ToString();
                }
                catch { }
                string sTipoPaxGen = sTipoPax;
                try
                {
                    sTipoPaxGen = drFila["strDetalleTipo"].ToString();
                }
                catch { }
                //if (sTipoPax.Contains("C"))
                //    sTipoPax = "CNN";

                if (sTipoPaxGen.Equals("ADT"))
                    strNombre = drFila["strPrimerNombre"].ToString() + " " + drFila["strTrato"].ToString();
                else
                    strNombre = drFila["strPrimerNombre"].ToString() + " " + sTipoPaxGen;

                vo_DataTravelItineraryAddInfo = new VO_DataTravelItineraryAddInfo(Convert.ToInt32(drFila["intIdPasajero"]),
                    strNombre,
                    drFila["strPrimerApellido"].ToString(),
                    Convert.ToBoolean(drFila["blInfante"]),
                    sTipoPax

            );
                List<string> lTelefonos = new List<string>();
                lTelefonos.Add(drFila["strTelefono"].ToString());
                vo_DataTravelItineraryAddInfo.Telefono_ = lTelefonos;

                if (!sTipoPaxGen.ToString().Equals("INF"))
                    vo_DataTravelItineraryAddInfo.Email_ = drFila["strEmail"].ToString();

                //if (drFila["strTipoPasajero"].ToString().Equals("INF"))
                //{
                String str_Fecha_Nacimiento = drFila["strFechaNacimiento"].ToString();
                //vo_DataTravelItineraryAddInfo.Fecha_ = clsValidaciones.ConverMDYtoDMMY(str_Fecha_Nacimiento);
                vo_DataTravelItineraryAddInfo.Fecha_ = str_Fecha_Nacimiento;
                //}
                if (!drFila["strPasajeroFrecuente"].ToString().Equals(string.Empty))
                    vo_DataTravelItineraryAddInfo.ViajeroFrecuente_ = drFila["strPasajeroFrecuente"].ToString();

                if (!drFila["strTipoDocumento"].ToString().Equals(string.Empty))
                    vo_DataTravelItineraryAddInfo.TipoDocumento_ = drFila["strTipoDocumento"].ToString();

                if (!drFila["strDocumento"].ToString().Equals(string.Empty))
                    vo_DataTravelItineraryAddInfo.Documento_ = drFila["strDocumento"].ToString();
                try
                {
                    if (!drFila["strGenero"].ToString().Equals(string.Empty))
                    {
                        if (drFila["strGenero"].ToString().Contains("F"))
                        {
                            vo_DataTravelItineraryAddInfo.Genero_ = "F";
                        }
                        else
                        {
                            vo_DataTravelItineraryAddInfo.Genero_ = "M";
                        }
                    }
                    else
                    {
                        vo_DataTravelItineraryAddInfo.Genero_ = "M";
                    }
                }
                catch { vo_DataTravelItineraryAddInfo.Genero_ = "M"; }
                listaPasajeros.Add(vo_DataTravelItineraryAddInfo);
            }
        }
        return listaPasajeros;
    }
    public clsResultados Confirmar_Reserva(object sender, EventArgs e, UserControl ucControl)
    {
        string intIdItinerario = ucControl.Request.QueryString["ITIID"];
        try
        {
            string sWhere = "SequenceNumber = " + intIdItinerario;
            DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
            DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
            DataTable dtTablaRetornada = clsDataNet.dsDataWhere(sWhere, dtPricedItinerary);
            intIdItinerario = dtTablaRetornada.Rows[0]["PricedItinerary_Id"].ToString();
        }
        catch { }

        clsVuelos objVuelos = new clsVuelos();
        objVuelos.GuardarDatosPasajeros(ucControl);
        clsSesiones.SET_LVO_DataTravelItineraryAddInfo(GetListaDatosPasajeros());
        clsSesiones.SET_LOAD_PASAJERO(true);
        Negocios_WebService_OTA_AirLowFareSearch objNegocios_WebService_OTA_AirLowFareSearch = new Negocios_WebService_OTA_AirLowFareSearch();
        clsResultados objResultados = objNegocios_WebService_OTA_AirLowFareSearch.GetCerrar(intIdItinerario);
        return objResultados;
    }
    /*------------CERRAR_RESERVA---------------------*/
    public clsResultados GetCerrar(String intItinerary_Id)
    {
        /*CERRAMOS RESERVA*/
        string sRecord = string.Empty;
        string sRrr = Ssoft.Utils.clsSesiones.GET_RECORD();
        String strMensaje = string.Empty;
        clsResultados objResultados = new clsResultados();
        clsCache cCache = new csCache().cCache();

        if (sRrr == null)
        {
            //setPasajeros(clsSesiones.GET_LVO_DataTravelItineraryAddInfo());
            setPasajeros(clsSesiones.GET_LVO_DataTravelItineraryAddInfo());
        }
        clsCacheControl cCacheControl = new clsCacheControl();

        /*NOS RETORNA EL RECORD*/
        objResultados = getCerrarReserva(cCache, intItinerary_Id);

        return objResultados;
    }
    public clsParametros setGuardarDatos(string sRecord, clsCache cCache, String intItinerary_Id)
    {
        /*GUARDAMOS LA RESERVA EN LA BASE DE DATOS */
        clsParametros objParametros = new clsParametros();
        string strError = String.Empty;
        try
        {
            csReservas cReserva = new csReservas();
            DataSet dsData = cReserva.CrearTablaReserva();
            DataTable tblReserva = dsData.Tables["tblReserva"];
            string sFechaInicio = String.Empty;
            string sFechaFin = String.Empty;

            /*GUARDAMOS LA RESERVA Y LA ENVIAMOS AL CARRITO DE COMPRAS*/
            objParametros = EnviarACarrito(sRecord, intItinerary_Id);

            if (objParametros.Id.Equals(0))
            {
                try
                {
                    if (objParametros.Code.Equals("0"))
                        strError = objParametros.ViewMessage[0] + ". " + objParametros.Sugerencia[0];
                    else
                        strError = objParametros.ViewMessage[0] + sRecord + ". " + objParametros.Sugerencia[0];
                }
                catch { }
                ExceptionHandled.Publicar(objParametros);
            }
        }
        catch (Exception Ex)
        {
            objParametros.Id = 0;
            objParametros.Sugerencia.Add("Vuelva a intentarlo");
            objParametros.StackTrace = Ex.StackTrace;
            objParametros.Tipo = clsTipoError.Library;
            objParametros.DatoAdic = Ex.Message;
            objParametros.DatoAdicArr.Add(Ex.HelpLink);
            strError = "La reserva no pudo ser confirmada. el record de la reserva es: " + sRecord;
            objParametros.ViewMessage.Add(strError);
            objParametros.Ex = Ex;
            ExceptionHandled.Publicar(objParametros);
        }
        return objParametros;
    }
    public bool getVerificaItinerario(string sPNR, string sItinerario)
    {
        bool bResponse = false;
        try
        {
            if (sItinerario.Length.Equals(0))
            {
                bResponse = true;
            }
            else
            {
                string sCommand = "I";
                string sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);

                sCommand = "*" + sPNR;
                sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);

                sCommand = "*I";
                sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);

                if (clsValidacionesVuelos.setValidaReserva(sRespuesta, sItinerario))
                {
                    bResponse = true;
                }
                else
                {
                    clsParametros cParametros = new clsParametros();
                    try
                    {
                        sCommand = "CANCELADA POR DIFERENCIA EN ITINERARIO";
                        Negocios_WebServiceRemark._ADD(Enum_TipoRemark.Historico, sCommand);
                        cParametros = GetCancelRecordSabre(sPNR);
                    }
                    catch (Exception Ex)
                    {
                        cParametros.Id = 0;
                        cParametros.Message = Ex.Message.ToString();
                        cParametros.Source = Ex.Source.ToString();
                        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        cParametros.Tipo = clsTipoError.Library;
                        cParametros.Severity = clsSeveridad.Moderada;
                        cParametros.StackTrace = Ex.StackTrace.ToString();
                        cParametros.Complemento = "Error al cancelar itiberario diferente ";
                        ExceptionHandled.Publicar(cParametros);
                    }
                }
            }
        }
        catch { }
        return bResponse;
    }
    public bool getVerificaItinerario(string sPNR, string sItinerario, string sTarifa)
    {
        bool bResponse = false;
        try
        {
            clsParametros cParametros = new clsParametros();
            string sCommand = "I";
            string sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);

            sCommand = "*" + sPNR;
            sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);

            sCommand = "*I";
            sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);

            if (clsValidacionesVuelos.setValidaReserva(sRespuesta, sItinerario))
            {
                sCommand = "*PQ";
                string sRespTarifa = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);
                if (clsValidacionesVuelos.setValidaTarifa(sRespTarifa, sTarifa))
                {
                    bResponse = true;
                }
                else
                {
                    try
                    {
                        sCommand = "CANCELADA POR DIFERENCIA EN TARIFA";
                        Negocios_WebServiceRemark._ADD(Enum_TipoRemark.Historico, sCommand);

                        cParametros = GetCancelRecordSabre(sPNR);
                    }
                    catch (Exception Ex)
                    {
                        cParametros.Id = 0;
                        cParametros.Message = Ex.Message.ToString();
                        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        cParametros.Source = Ex.Source.ToString();
                        cParametros.Tipo = clsTipoError.Library;
                        cParametros.Severity = clsSeveridad.Moderada;
                        cParametros.StackTrace = Ex.StackTrace.ToString();
                        cParametros.Complemento = "Error al cancelar tarifa diferente ";
                        ExceptionHandled.Publicar(cParametros);
                    }
                }
            }
            else
            {
                try
                {
                    sCommand = "CANCELADA POR DIFERENCIA EN ITINERARIO";
                    Negocios_WebServiceRemark._ADD(Enum_TipoRemark.Historico, sCommand);
                    cParametros = GetCancelRecordSabre(sPNR);
                }
                catch (Exception Ex)
                {
                    cParametros.Id = 0;
                    cParametros.Message = Ex.Message.ToString();
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = "Error al cancelar itinerario diferente ";
                    ExceptionHandled.Publicar(cParametros);
                }
            }
        }
        catch { }
        return bResponse;
    }
    private clsResultados getCerrarReserva(clsCache cCache, String intItinerary_Id)
    {
        csReservas cReserva = new csReservas();
        clsResultados objResultados = new clsResultados();
        clsParametros objParametros = new clsParametros();

        string sReserva = string.Empty;

        try
        {
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            string sOrigen = string.Empty;
            string sDestino = string.Empty;
            string sFechaIni = string.Empty;
            bool bHoras = false;
            try
            {
                if (vo_OTA_AirLowFareSearchLLSRQ != null)
                {
                    sOrigen = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo.ToString();
                    sDestino = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoDestino.SCodigo.ToString();
                    sFechaIni = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaSalida.ToString();
                    bHoras = vo_OTA_AirLowFareSearchLLSRQ.BHoras;
                }
            }
            catch { }
            // Metodos para verificar la tarifa almacenada y el itinerario
            string sCommand = "*I";
            string sRespItinerario = string.Empty;
            if (bHoras)
            {
                sRespItinerario = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);
            }
            //sCommand = "*PQ";
            //string sResTarifa = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);
            Negocios_WebServiceSabreCommand.setPQ();
            /*SEGMENTO FUTURO*/
            objParametros = setCerrar();
            if (!objParametros.Id.Equals(0))
            {
                sReserva = objParametros.DatoAdic;
                //Negocios_WebServiceRemark._ADDRemarkSsoft(sReserva);
                if (getVerificaItinerario(sReserva, sRespItinerario))
                {
                    try
                    {
                        WS_SsoftSabre.General.clsRulesFromPrice obj_RulesFromPrice = new clsRulesFromPrice();
                        obj_RulesFromPrice.StrSesion = AutenticacionSabre.GET_SabreSession();
                        //obj_RulesFromPrice.getRulesSegment(Convert.ToDateTime(SesionesWs.getParametrosAirBargain().Lvo_Rutas[0].SFechaSalida));
                        obj_RulesFromPrice.getRulesSegment();
                    }
                    catch
                    {
                    }

                    Ssoft.Utils.clsSesiones.SET_RECORD(objParametros.DatoAdic);
                    cReserva.Conexion = ConfigurationManager.AppSettings["strConexion"].ToString();
                    cReserva.GuardarLogReserva(sReserva, 0, 0, sOrigen, sDestino, sFechaIni);
                    /*GUARDAMOS LOS DATOS DE LA RESERVA*/
                    objParametros = setGuardarDatos(objParametros.DatoAdic, cCache, intItinerary_Id);
                }
                else
                {
                    cReserva.GuardarLogReserva(sReserva, 0, 0, sOrigen, sDestino, sFechaIni);
                    objParametros.Id = 0;
                    objParametros.Tipo = clsTipoError.Library;
                    objParametros.Severity = clsSeveridad.Moderada;
                    objParametros.Sugerencia.Add("Por favor intente de nuevo, con otras opciones de busqueda");
                    objParametros.ViewMessage.Add("Algunos de los itinerarios seleccionados, no esta disponible");
                }
            }
            else
            {
                objParametros.Tipo = clsTipoError.Library;
                objParametros.Severity = clsSeveridad.Moderada;
                objParametros.Sugerencia.Add("Por favor intente de nuevo, con otras opciones de busqueda");
                objParametros.ViewMessage.Add("La reserva no se confirmo");
            }
            Negocios_WebServiceSabreCommand.setQP(sReserva);
            setCerrarSesion();
            Ssoft.Utils.clsSesiones.CLEAR_SESSION_AIR();
            objResultados.strResultados = sReserva;
        }
        catch (Exception Ex)
        {
            Negocios_WebServiceSabreCommand.setQP(sReserva);
            setCerrarSesion();
            Ssoft.Utils.clsSesiones.CLEAR_SESSION_AIR();
            objParametros.Id = 0;
            objParametros.Ex = Ex;
            if (!sReserva.Length.Equals(0))
            {
                objParametros.ViewMessage.Add(sReserva);
            }
            else
            {
                objParametros.ViewMessage.Add("La reserva no se confirmo");
            }
            objParametros.Sugerencia.Add("Por favor intente de nuevo, con otras opciones de busqueda");
            objParametros.Tipo = clsTipoError.Library;
            objParametros.Severity = clsSeveridad.Moderada;
            objResultados.strResultados = null;
            ExceptionHandled.Publicar(Ex);
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE RESULTADOS*/
        objResultados.Error = objParametros;

        return objResultados;
    }
    private clsParametros setCerrar()
    {
        clsParametros cParametros = new clsParametros();
        try
        {
            string sRecord = string.Empty;
            cParametros = new clsEndTransactionLLS()._CerrarReserva(ref sRecord);
        }
        catch (Exception Ex)
        {
            cParametros.Id = 0;
            cParametros.Message = Ex.Message;
            cParametros.StackTrace = Ex.StackTrace;
            cParametros.Source = Ex.Source;
            cParametros.TargetSite = Ex.TargetSite.ToString();
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            cParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(cParametros);
        }
        return cParametros;
    }
    private void setCerrarSesion()
    {
        Negocios_WebServiceSession._CerrarSesion();
    }
    /*-------------------CARRITO_COMPRAS---------------------------*/
    /// <summary>
    /// metodo pendiente por revision
    /// </summary>
    /// <param name="sRecord"></param>
    /// <param name="intItinerary_Id"></param>
    /// <returns></returns>
    public clsParametros EnviarACarrito(String sRecord, string intItinerary_Id)
    {
        clsParametros objMensajes = new clsParametros();
        const string strNombreCarroCompras = "CarritoCompras";
        string strConexion = clsSesiones.getConexion();


        try
        {
            int iSegmentoManual = 1;
            clsCache cCache = new csCache().cCache();
            csCarrito objCarritoCompras = new csCarrito("Reserva" + cCache.SessionID, strNombreCarroCompras);
          
            DataTable tblBeneficios = null;
            try
            {
                tblBeneficios = clsSesiones.GetTablaBeneficios();
            }
            catch (Exception) { }

            if (tblBeneficios != null && tblBeneficios.Rows.Count != 0)
            {
                objCarritoCompras.IntConvenio = tblBeneficios.Rows[0]["ID_BENEFICIO"].ToString();
            }
            //csPlan.Conexion = strConexion;
            //tblRefere objtblRefere = new tblRefere();
            //objtblRefere.Conexion = strConexion;
            string sFechaIniCarrito = string.Empty;
            string sFechaFinCarrito = string.Empty;
            /*CODIGO DEL PLAN*/
            String strCodigoPlan = "1";
            String strTipoPlan = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
            DataTable dtItinerario = new DataTable();
            try
            {
                dtItinerario = new clsVuelos().GetDtGetItinerario(intItinerary_Id);
                if (!dtItinerario.Rows[0]["Ws"].ToString().Length.Equals(0))
                    strTipoPlan = dtItinerario.Rows[0]["Ws"].ToString();
            }
            catch { }

            //try
            //{
            //    csPlanes objPlanes = new csPlanes();
          
            //    General.Conexion = strConexion;
            //    objPlanes.Conexion = strConexion;
            //    DataTable dtPlanes = objPlanes.ConsultaPlanes(strTipoPlan, clsSesiones.getIdioma(), clsSesiones.getAplicacion().ToString());
            //    strCodigoPlan = dtPlanes.Rows[0]["intCodigo"].ToString();
            //}
            //catch (Exception)
            //{
            //    strCodigoPlan = "0";
            //}

            /*TIPO PLAN*/
            String idRefereTipoPlan = new CsConsultasVuelos().ConsultaCodigo(strTipoPlan,"TBLTPOSERVICIO","INTID","STRCODIGO");
            /*ESTADO SOLICITADA*/
            String int_Id_EstadoSolicitada = new CsConsultasVuelos().ConsultaCodigo( clsValidaciones.GetKeyOrAdd("EstadoReservaInicialAereo", "HK"),"TBLESTADOS_RESERVA","INTCODE","STRCODE"); 

            /*MONEDA*/
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            String strTipoMoneda = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion;
            String id_Refere_Tipo_Moneda = new CsConsultasVuelos().ConsultaCodigo(strTipoMoneda,"TBLMONEDAS","INTCODE","STRCODE");

            /*TABLA DE SEGMENTOS*/
            DataTable dtSegmento = new DataTable();

            DataTable dtPasajeros = new DataTable();

            /*TABLA DE TIPOS PASAJEROS*/
            DataTable dtPassengerTypeQuantity = new DataTable();
            string sVuelosXhora = clsValidaciones.GetKeyOrAdd("sVueloXhoras", "False");
            if (sVuelosXhora.ToUpper().Equals("TRUE"))
            {
                if (vo_OTA_AirLowFareSearchLLSRQ.BHoras)
                {
                    dtSegmento = new clsVuelos().GetDtFlightSegmentoHorasCotiza();
                    dtPasajeros = new clsVuelos().GetDtPasajeros();
                    dtPassengerTypeQuantity = new clsVuelos().GetDtPassengerTypeQuantity();
                }
                else
                {
                    dtSegmento = new clsVuelos().GetDtFlightSegmento(intItinerary_Id.ToString());
                    dtPasajeros = new clsVuelos().GetDtPasajeros();
                    dtPassengerTypeQuantity = new clsVuelos().GetDtPassengerTypeQuantity(intItinerary_Id);
                }
            }
            else
            {
                dtSegmento = new clsVuelos().GetDtFlightSegmento(intItinerary_Id.ToString());
                dtPasajeros = new clsVuelos().GetDtPasajeros();
                dtPassengerTypeQuantity = new clsVuelos().GetDtPassengerTypeQuantity(intItinerary_Id);
            }
            try
            {
            }
            catch { }
            /*TABLA DE PASAJEROS*/
            /*NUMERO PASAJEROS*/
            Int32 intNumeroPasajeros = dtPasajeros.Rows.Count;
            /*GUARDAMOS LOS DATOS BASICOS DE LOS SEGMENTOS*/
            //int iPos = 0;

            foreach (DataRow drFilaSegmento in dtSegmento.Rows)
            {
                String strAnio = string.Empty;
                String strMes = string.Empty;
                String strDia = string.Empty;
                /*RECORD*/
                objCarritoCompras.StrCodigoReserva = sRecord;
                objCarritoCompras.StrConfirmacion = sRecord;
                objCarritoCompras.IntCodigoPlan = strCodigoPlan;
                /*FECHA LIMITE DE TIQUETEO*/
                DateTime dtFecha_vencimiento = Ssoft.Utils.clsSesiones.GET_TICKETE();
                System.Text.StringBuilder strFecha_Vencimiento = new System.Text.StringBuilder();
                strFecha_Vencimiento.Append(dtFecha_vencimiento.Month + "/");
                strFecha_Vencimiento.Append(dtFecha_vencimiento.Day + "/");
                strFecha_Vencimiento.Append(dtFecha_vencimiento.Year);
                objCarritoCompras.StrFechaVencimiento = strFecha_Vencimiento.ToString();
                /*IDENTIFICADOR DEL PLAN*/
                objCarritoCompras.StrIdentificadorDelPlan = strTipoPlan;
                /*SEGMENTO*/
                objCarritoCompras.IntCodigoTarifa = drFilaSegmento["FlightSegment_Id"].ToString();
                /*NUMERO DEL VUELO*/
                objCarritoCompras.StrNombrePlan = drFilaSegmento["strNombre_Aerolinea"].ToString();
                objCarritoCompras.BolImpuestos = bool.Parse(dtItinerario.Rows[0]["bolImpuestos"].ToString());
                clsSesiones.setImpuestos(objCarritoCompras.BolImpuestos);
                try
                {
                    cCache.Impuestos = objCarritoCompras.BolImpuestos;
                    clsCacheControl cCacheControl = new clsCacheControl();
                    cCacheControl.ActualizaXML(cCache);
                }
                catch { }
                /*CANTIDAD PASAJEROS*/
                objCarritoCompras.IntcantidadPersonas = intNumeroPasajeros.ToString();
                objCarritoCompras.StrPasajeros = intNumeroPasajeros.ToString();
                /*TIPO PLAN*/
                objCarritoCompras.StrTipoPlan = strTipoPlan;
                objCarritoCompras.IntTipoPlan = idRefereTipoPlan.ToString();
                /*MONEDA*/
                objCarritoCompras.StrTipoMoneda = strTipoMoneda;
                objCarritoCompras.IntTipoMoneda = id_Refere_Tipo_Moneda.ToString();
                /*TOTAL VALOR*/
                Decimal Fee = 0;
                /*Validamos si trae fee adicional de la agencia para sumarselo al valor total*/
                for (int iPas = 0; iPas < dtPasajeros.Rows.Count; iPas++)
                {
                    if (!dtPasajeros.Rows[iPas]["strFee"].ToString().Equals(""))
                    {
                        Fee += clsValidaciones.getDecimalNotRound(dtPasajeros.Rows[iPas]["strFee"].ToString());
                    }
                }
                objCarritoCompras.IntValorTotal = clsValidaciones.getDecimalNotRound((clsValidaciones.getDecimalNotRound(dtItinerario.Rows[0]["intTotalPesos"].ToString()) + Fee).ToString()).ToString();
                if (tblBeneficios != null &&
                    tblBeneficios.Rows.Count != 0)
                {
                    objCarritoCompras.IntValorTotal = tblBeneficios.Rows[0]["VALOR_TOTAL_PESOS"].ToString();
                }
                /*RPH*/
                //objCarritoCompras.IntSegmento = drFilaSegmento["RPH"].ToString();
                objCarritoCompras.IntSegmento = iSegmentoManual.ToString();
                /*ORIGEN Y DESTINO*/
                objCarritoCompras.IntOrigen = drFilaSegmento["IntId_Aeropuerto_Salida"].ToString();
                objCarritoCompras.IntDestino = drFilaSegmento["IntId_Aeropuerto_Llegada"].ToString();
                objCarritoCompras.StrOrigen = drFilaSegmento["strDepartureAirport"].ToString();
                objCarritoCompras.StrDestino = drFilaSegmento["strArrivalAirport"].ToString();

                //objCarritoCompras.IntOrigen = drFilaSegmento["IntId_Aeropuerto_Salida"].ToString();
                //objCarritoCompras.IntDestino = drFilaSegmento["IntId_Aeropuerto_Llegada"].ToString();
                //objCarritoCompras.StrOrigen = drFilaSegmento["strDepartureAirport"].ToString();
                //objCarritoCompras.StrDestino = drFilaSegmento["strArrivalAirport"].ToString();
                int iPosTemp = 1;
                foreach (DataRow drFilaSegmentos in dtSegmento.Rows)
                {
                    objCarritoCompras.StrCiudad += drFilaSegmentos["strCiudad_Salida"].ToString() + " - " + drFilaSegmentos["strCiudad_Llegada"].ToString();
                    if (!iPosTemp.Equals(dtSegmento.Rows.Count))
                    {
                        objCarritoCompras.StrCiudad += ", ";
                    }
                    iPosTemp++;
                }
                objCarritoCompras.StrAcomodacion = intNumeroPasajeros.ToString();
                /*OBTENEMOS LA FECHA DE SALIDA*/
                DateTime dtmFechaSalida = Convert.ToDateTime(drFilaSegmento["dtmFechaSalida"]);
                strAnio = dtmFechaSalida.Year.ToString();
                strMes = dtmFechaSalida.Month.ToString();
                strDia = dtmFechaSalida.Day.ToString();
                sFechaIniCarrito = strMes + "/" + strDia + "/" + strAnio;
                objCarritoCompras.StrFechaInicial = sFechaIniCarrito;
                /*OBTENEMOS LA FECHA DE LLEGADA*/
                DateTime dtm_Fecha_LLegada = Convert.ToDateTime(drFilaSegmento["dtmFechaLlegada"]);
                strAnio = dtm_Fecha_LLegada.Year.ToString();
                strMes = dtm_Fecha_LLegada.Month.ToString();
                strDia = dtm_Fecha_LLegada.Day.ToString();
                sFechaFinCarrito = strMes + "/" + strDia + "/" + strAnio;
                objCarritoCompras.StrFechaFinal = sFechaFinCarrito;
                /*LA HORA*/
                objCarritoCompras.StrHoraIni = Convert.ToDateTime(drFilaSegmento["dtmFechaSalida"]).ToString("HH:mm:ss");
                objCarritoCompras.StrHoraFin = Convert.ToDateTime(drFilaSegmento["dtmFechaLlegada"]).ToString("HH:mm:ss");
                /*ID AEROLINEA*/
                objCarritoCompras.IntProveedor = drFilaSegmento["intId_Aerolinea"].ToString();
                /*ESTADO SOLICITADA*/
                objCarritoCompras.IntEstado = int_Id_EstadoSolicitada;
                /*CODIGO DEL LA AEROLINEA*/
                objCarritoCompras.StrObservacion = drFilaSegmento["strMarketingAirline"].ToString();
                String strTipoTrayecto = drFilaSegmento["strTipoTrayecto"].ToString().Substring(0, 1);
                clsSesiones.setAerolineaValidadora(drFilaSegmento["strMarketingAirline"].ToString() + strTipoTrayecto);
                objCarritoCompras.StrOperador = drFilaSegmento["FlightNumber"].ToString();
                objCarritoCompras.StrCodigo = "0";
                objCarritoCompras.StrDetalles = "";
                objCarritoCompras.StrRestricciones = "";
                objCarritoCompras.StrBeneFicios = "";
                objCarritoCompras.StrEncuenta = "";
                objCarritoCompras.StrZonaGeografica = "";
                objCarritoCompras.AddFields();
                iSegmentoManual++;
            }
            /*En esta parte se hace el recorrido nuevamente para que en el carrito se muestre la fecha inicial y final del viaje*/
            //foreach (DataRow drFilaSegmento in dtSegmento.Rows)
            //{
            //    String strAnio = string.Empty;
            //    String strMes = string.Empty;
            //    String strDia = string.Empty;
            //    if (iPos.Equals(0))
            //    {
            //        /*OBTENEMOS LA FECHA DE SALIDA*/
            //        DateTime dtmFechaSalida = Convert.ToDateTime(drFilaSegmento["dtmFechaSalida"]);
            //        strAnio = dtmFechaSalida.Year.ToString();
            //        strMes = dtmFechaSalida.Month.ToString();
            //        strDia = dtmFechaSalida.Day.ToString();
            //        sFechaIniCarrito = strMes + "/" + strDia + "/" + strAnio;

            //        /*OBTENEMOS LA FECHA DE LLEGADA*/
            //        DateTime dtm_Fecha_LLegada = Convert.ToDateTime(drFilaSegmento["dtmFechaLlegada"]);
            //        strAnio = dtm_Fecha_LLegada.Year.ToString();
            //        strMes = dtm_Fecha_LLegada.Month.ToString();
            //        strDia = dtm_Fecha_LLegada.Day.ToString();
            //        sFechaFinCarrito = strMes + "/" + strDia + "/" + strAnio;
            //    }
            //    else
            //    {
            //        /*OBTENEMOS LA FECHA DE LLEGADA*/
            //        DateTime dtm_Fecha_LLegada = Convert.ToDateTime(drFilaSegmento["dtmFechaLlegada"]);
            //        strAnio = dtm_Fecha_LLegada.Year.ToString();
            //        strMes = dtm_Fecha_LLegada.Month.ToString();
            //        strDia = dtm_Fecha_LLegada.Day.ToString();
            //        sFechaFinCarrito = strMes + "/" + strDia + "/" + strAnio;
            //    }
            //    iPos++;
            //}
            //objCarritoCompras.StrFechaInicial = sFechaIniCarrito;
            //objCarritoCompras.StrFechaFinal = sFechaFinCarrito;
            /*GUARDAMOS TIPO DE PASAJERO*/
            Int32 intIdTipoPax = default(Int32);
            foreach (DataRow drFilaTipoPax in dtPassengerTypeQuantity.Rows)
            {
                DataTable dtFare = new clsVuelos().GetDtPassengerFare(drFilaTipoPax["PTC_FareBreakdown_Id"].ToString());
                Decimal ImpuestoFee = 0;
                Decimal dbl_total_Con_impuestos_tasas = 0;
                Decimal dbl_total_Sin_impuestos_tasas = 0;
                Decimal dbl_total_impuestos_tasas = 0;
                if (!dtPasajeros.Rows[intIdTipoPax]["strFee"].ToString().Equals(""))
                {
                    ImpuestoFee = clsValidaciones.getDecimalNotRound(dtPasajeros.Rows[intIdTipoPax]["strFee"].ToString());
                }
                if (ImpuestoFee != 0)
                {
                    dbl_total_Con_impuestos_tasas = clsValidaciones.getDecimalNotRound(dtFare.Rows[0]["IntTotalTarifaConTAXPersona"].ToString()) + ImpuestoFee;
                    dbl_total_Sin_impuestos_tasas = clsValidaciones.getDecimalNotRound(dtFare.Rows[0]["intBaseFare"].ToString());
                    dbl_total_impuestos_tasas = clsValidaciones.getDecimalNotRound(dtFare.Rows[0]["intTotalImpuestosTasas"].ToString()) + ImpuestoFee;
                }
                else
                {
                    dbl_total_Con_impuestos_tasas = clsValidaciones.getDecimalNotRound(dtFare.Rows[0]["IntTotalTarifaConTAXPersona"].ToString());
                    dbl_total_Sin_impuestos_tasas = clsValidaciones.getDecimalNotRound(dtFare.Rows[0]["intBaseFare"].ToString());
                    dbl_total_impuestos_tasas = clsValidaciones.getDecimalNotRound(dtFare.Rows[0]["intTotalImpuestosTasas"].ToString());
                }
                String str_Tipo_Pasajero = drFilaTipoPax["Code"].ToString();
                string sDetallePax = str_Tipo_Pasajero;
                //if (str_Tipo_Pasajero.Contains("C"))
                //{
                //    if (!str_Tipo_Pasajero.Equals("CNN"))
                //    {
                //        int iEdadPax = int.Parse(clsValidaciones.RetornaNumero(str_Tipo_Pasajero.Substring(1)));
                //        sDetallePax = "Niño " + iEdadPax.ToString() + " Años";
                //    }
                //}
                //objtblRefere.Get(clsValidaciones.GetKeyOrAdd("TiposPasajero"), str_Tipo_Pasajero, sDetallePax);
                String id_Refere_Tipo_Pasajero = new CsConsultasVuelos().ConsultaCodigo(str_Tipo_Pasajero,"TBLTPOPAX","INTCODE","STRTIPOSABRE");

                decimal dblbeneficio = default(decimal);

                try
                {
                    if (tblBeneficios != null && tblBeneficios.Rows.Count != 0)
                    {
                        for (int c = 0; c < tblBeneficios.Rows.Count; c++)
                        {
                            if (tblBeneficios.Rows[c]["CODETIPOPAX"].ToString().Equals(str_Tipo_Pasajero))
                            {
                                dblbeneficio = clsValidaciones.getDecimalNotRound(tblBeneficios.Rows[c]["VALOR_BENEFICIO_TIPOPAX"].ToString());
                            }
                        }
                    }
                }
                catch (Exception) { }
                objCarritoCompras.SaveTipoPax(str_Tipo_Pasajero, Convert.ToInt32(id_Refere_Tipo_Pasajero), clsValidaciones.getDecimalBD(dbl_total_Con_impuestos_tasas.ToString()).ToString(), clsValidaciones.getDecimalBD(dbl_total_Sin_impuestos_tasas.ToString()).ToString(), clsValidaciones.getDecimalBD(dbl_total_impuestos_tasas.ToString()).ToString(), dblbeneficio.ToString(), "0");
                intIdTipoPax++;
            }
            /*GUARDAMOS CADA PASAJERO*/
            int iPosPax = 1;
            for (int i = 0; i < dtPasajeros.Rows.Count; i++)
            {
                String strTipoPasajero = dtPasajeros.Rows[i]["strTipoPasajero"].ToString();
                try
                {
                    strTipoPasajero = dtPasajeros.Rows[i]["strCode"].ToString();
                }
                catch { }
                string sDetallePax = strTipoPasajero;
                //if (strTipoPasajero.Contains("C"))
                //{
                //    if (!strTipoPasajero.Equals("CNN"))
                //    {
                //        int iEdadPax = int.Parse(clsValidaciones.RetornaNumero(strTipoPasajero.Substring(1)));
                //        sDetallePax = "Niño " + iEdadPax.ToString() + " Años";
                //    }
                //}
                //objtblRefere.Get(clsValidaciones.GetKeyOrAdd("TiposPasajero"), strTipoPasajero, sDetallePax);

                String idRefereTipoPasajero = new CsConsultasVuelos().ConsultaCodigo(strTipoPasajero,"TBLTPOPAX","INTCODE","STRTIPOSABRE");
                String str_Nombre_Completo = dtPasajeros.Rows[i]["strPrimerNombre"].ToString() + "/" + dtPasajeros.Rows[i]["strPrimerApellido"].ToString();
                String str_Telefono = dtPasajeros.Rows[i]["strTelefono"].ToString();
                String str_TipoDocumento = "0";
                //objtblRefere.Get(clsValidaciones.GetKeyOrAdd("TipoDocumento", "TD"), dtPasajeros.Rows[i]["strTipoDocumento"].ToString(), dtPasajeros.Rows[i]["strTipoDocumento"].ToString());

                //if (objtblRefere.Respuesta)
                str_TipoDocumento = new CsConsultasVuelos().ConsultaCodigo(dtPasajeros.Rows[i]["strTipoDocumento"].ToString(),"TBLTPOIDENTIFICA","INTCODE","STRCODE");

                String str_Documento = dtPasajeros.Rows[i]["strDocumento"].ToString();
                string strFechaNac = null;
                try
                {
                    strFechaNac = clsValidaciones.getFechaSabre(dtPasajeros.Rows[i]["strFechaNacimiento"].ToString());
                    strFechaNac = clsValidaciones.ConverFecha(strFechaNac, "yyyy/MM/dd", clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd"));
                }
                catch { }
                objCarritoCompras.SavePerson(str_Nombre_Completo, strFechaNac, strTipoPasajero, Convert.ToInt32(idRefereTipoPasajero), null, 0, str_Telefono);
                objCarritoCompras.UpdatePerson(iPosPax, null, null, null, null, null, str_TipoDocumento, str_Documento, null);
                iPosPax++;
            }
            /*GUARDAMOS LAS TASAS DE CADA TIPO PASAJERO*/
            for (int I = 0; I < dtPassengerTypeQuantity.Rows.Count; I++)
            {
                DataTable dtFare = new clsVuelos().GetDtPassengerFare(dtPassengerTypeQuantity.Rows[I]["PTC_FareBreakdown_Id"].ToString());
                String str_Tipo_Pasajero = dtPassengerTypeQuantity.Rows[I]["Code"].ToString();
                /*OBTENEMOS EL IDREFERE TIPO PAX*/
                string sDetallePax = str_Tipo_Pasajero;
                //if (str_Tipo_Pasajero.Contains("C"))
                //{
                //    if (!str_Tipo_Pasajero.Equals("CNN"))
                //    {
                //        int iEdadPax = int.Parse(clsValidaciones.RetornaNumero(str_Tipo_Pasajero.Substring(1)));
                //        sDetallePax = "Niño " + iEdadPax.ToString() + " Años";
                //    }
                //}
                //objtblRefere.Get(clsValidaciones.GetKeyOrAdd("TiposPasajero"), str_Tipo_Pasajero, sDetallePax);

                String id_Refere_Tipo_Pasajero = new CsConsultasVuelos().ConsultaCodigo(str_Tipo_Pasajero,"TBLTPOPAX","INTCODE","STRTIPOSABRE");

                for (int F = 0; F < dtFare.Rows.Count; F++)
                {
                    DataTable dtFareTax = new clsVuelos().GetDtPassengerFareTax(dtFare.Rows[F]["PassengerFare_Id"].ToString());
                    DataTable dtFareTaxcopy = dtFareTax.Copy();
                    if (!dtPasajeros.Rows[I]["strFee"].ToString().Equals(""))
                    {
                        VO_Credentials vo_Credentials = csReferencias.csCredenciales(Enum_ProveedorWebServices.Sabre);

                        if (!vo_Credentials.PccPais.ToString().Equals(clsValidaciones.GetKeyOrAdd("PccPais")) /*vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString().Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano"))*/)
                        {
                            Decimal ValIva = clsValidaciones.getDecimalNotRound("1,16");
                            Decimal ValorSinIva = clsValidaciones.getDecimalNotRound((clsValidaciones.getDecimalNotRound(dtPasajeros.Rows[I]["strFee"].ToString()) / ValIva).ToString());
                            Decimal Iva = clsValidaciones.getDecimalNotRound((clsValidaciones.getDecimalNotRound(dtPasajeros.Rows[I]["strFee"].ToString()) - ValorSinIva).ToString());

                            DataRow drFilaTax = dtFareTaxcopy.NewRow();
                            drFilaTax["TaxCode"] = clsValidaciones.GetKeyOrAdd("FEE_Adicional", "ADFE");
                            drFilaTax["CurrencyCode"] = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString();
                            drFilaTax["DecimalPlaces"] = "0";
                            drFilaTax["Amount"] = ValorSinIva.ToString();
                            drFilaTax["Taxes_Id"] = "0";
                            drFilaTax["Tax_Amount_Usd"] = ValorSinIva.ToString();
                            drFilaTax["strNombre_Impuesto"] = "FeeAdicional";

                            DataRow drFilaImpuesto = dtFareTaxcopy.NewRow();
                            drFilaImpuesto["TaxCode"] = clsValidaciones.GetKeyOrAdd("IVA_FEE_Adicional", "IADFE");
                            drFilaImpuesto["CurrencyCode"] = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString();
                            drFilaImpuesto["DecimalPlaces"] = "0";
                            drFilaImpuesto["Amount"] = Iva.ToString();
                            drFilaImpuesto["Taxes_Id"] = "0";
                            drFilaImpuesto["Tax_Amount_Usd"] = Iva.ToString();
                            drFilaImpuesto["strNombre_Impuesto"] = "Iva Fee Adicional";

                            dtFareTaxcopy.Rows.Add(drFilaTax);
                            dtFareTaxcopy.Rows.Add(drFilaImpuesto);
                            dtFareTax.Clear();
                            dtFareTax = dtFareTaxcopy;
                        }
                        else
                        {
                            DataRow drFilaTax = dtFareTaxcopy.NewRow();
                            drFilaTax["TaxCode"] = clsValidaciones.GetKeyOrAdd("FEE_Adicional", "ADFE");
                            drFilaTax["CurrencyCode"] = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString();
                            drFilaTax["DecimalPlaces"] = "0";
                            drFilaTax["Amount"] = dtPasajeros.Rows[I]["strFee"].ToString();
                            drFilaTax["Taxes_Id"] = "0";
                            drFilaTax["Tax_Amount_Usd"] = dtPasajeros.Rows[I]["strFee"].ToString();
                            drFilaTax["strNombre_Impuesto"] = "FeeAdicional";
                            dtFareTaxcopy.Rows.Add(drFilaTax);
                            dtFareTax.Clear();
                            dtFareTax = dtFareTaxcopy;
                        }
                    }
                    foreach (DataRow drFilaTax in dtFareTax.Rows)
                    {
                        String strRefereTasa = drFilaTax["TaxCode"].ToString();
                        String intIdValorTax = drFilaTax["Amount"].ToString();
                        /*OBTENEMOS LOS DOS PRIMEROS CODIGOS DE LOS IMPUESTOS DIFERENTES DE EL TA Y ITA*/
                        //if (strRefereTasa != clsValidaciones.GetKeyOrAdd("TASA_ADMINISTRATIVA") && strRefereTasa != clsValidaciones.GetKeyOrAdd("IVA_TA") && !strRefereTasa.Contains("CO") && !strRefereTasa.Contains("FR") && !strRefereTasa.Contains(clsValidaciones.GetKeyOrAdd("FEE_Adicional")) && !strRefereTasa.Contains(clsValidaciones.GetKeyOrAdd("IVA_FEE_Adicional")))
                        //    strRefereTasa = strRefereTasa.Substring(0, 2);
                        if (tblBeneficios != null && tblBeneficios.Rows.Count != 0)
                        {
                            if (strRefereTasa == clsValidaciones.GetKeyOrAdd("TASA_ADMINISTRATIVA", "TA"))
                            {
                                intIdValorTax = tblBeneficios.Rows[0]["DESC_VALOR_TA"].ToString();
                            }
                            else if (strRefereTasa == clsValidaciones.GetKeyOrAdd("IVA_TA", "ITA"))
                            {
                                intIdValorTax = tblBeneficios.Rows[0]["DESC_VALOR_IVATA"].ToString();
                            }
                        }
                        /*OBTENEMOS EL IDREFERE DE LA TASA*/
                        //objtblRefere.Get(clsValidaciones.GetKeyOrAdd("SABRETAX", "SABRETAX"), drFilaTax["TaxCode"].ToString(), drFilaTax["TaxCode"].ToString());
                        String intIdCodigoTax = new CsConsultasVuelos().ConsultaCodigo(drFilaTax["TaxCode"].ToString(),"TBLIMPUESTOSSABRE","INTCODE","STRCODE");
                        objCarritoCompras.AddTasa(intIdCodigoTax, "0", clsValidaciones.getDecimalBD(intIdValorTax), id_Refere_Tipo_Moneda, id_Refere_Tipo_Pasajero);
                    }
                }

            }
            /*GUARDAMOS EN XML*/
            objCarritoCompras.Save();
            /*GUARDAMOS LOS DATOS GENERALES DEL PROYECTO*/
            GuardarDatosProyecto();
            csReservas csRes = new csReservas();
            csRes.Conexion = clsValidaciones.GetKeyOrAdd("strConexion");
            objMensajes = csRes.GuardaReservaGen(objCarritoCompras.GetDsReservas());
            /*GUARDAMOS EL SQL QUE GENERO*/
            ExceptionHandled.Publicar(objMensajes.Complemento);
            /*ACUTALIZAMO EL CODIGO DE PROYECTO*/
            if (clsSesiones.getProyecto() == "0")
                clsSesiones.setProyecto(objMensajes.DatoAdicArr[0]);
            /*ACTUALIZAMOS EL CODIGO DEL INSERCION*/
            objCarritoCompras.Save_Update("1");
            clsSesiones.setPantalleRespuestaLogin("ConfirmacionVuelo.aspx?RECORD=" + sRecord);
            return objMensajes;

        }
        catch (Exception Ex)
        {
            clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
            clsParametros objParametros = new clsParametros();
            objParametros.ViewMessage.Add("No se pudo realizar la solicitud");
            objParametros.Sugerencia.Add("Ha ocurrido un error procesando su solicitud");
            objParametros.Tipo = Ssoft.ManejadorExcepciones.clsTipoError.Library;
            ExceptionHandled.Publicar(Ex);
        }
        return objMensajes;
    }
    /// <summary>
    /// METODO pendiente por revision
    /// </summary>
    private void GuardarDatosProyecto()
    {
        /*FECHA LIMITE DE PAGO*/
        clsCache cCache = new csCache().cCache();

        //DateTime dPlazo = Ssoft.Utils.clsSesiones.GET_TICKETE();
        const string strNombreCarroCompras = "CarritoCompras";
        csCarrito csCarCompras = new csCarrito("Reserva" + cCache.SessionID, strNombreCarroCompras);
        string idRecord = clsSesiones.getProyecto();
       
        string iEstadoReserva = "0";
        string iEstadoPago = "0";
        string iFormaPago = "0";

        string sEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoReservaInicialAereo", "HK");
        string sEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPagoInicial", "PP");
        string sFormaPago = clsValidaciones.GetKeyOrAdd("EstadoFormaPagoInicial", "EFE");
        string sTipoEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoReserva", "EstadoReserva");
        string sTipoEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPago", "EstadoPago");
        string sTipoFormaPago = clsValidaciones.GetKeyOrAdd("FormasPago", "FP");

        string sContacto = cCache.Contacto;
        string sCodCoordinador = cCache.Contacto;
        string sCoordinador = clsValidaciones.GetKeyOrAdd("Coordinador", "CE");

        if (cCache.Viajero != "0")
        {
            sContacto = cCache.Viajero;
        }
        //if (!sCoordinador.Equals(csReferencias.csTipoContactoRefere()))
        //    sCodCoordinador = cCache.Viajero;

        //tblRefere otblRefere = new tblRefere();
        //otblRefere.Get(sTipoEstadoReserva, sEstadoReserva);
        //if (otblRefere.Respuesta)
        iEstadoReserva = new CsConsultasVuelos().ConsultaCodigo(sEstadoReserva,"TBLESTADOS_RESERVA","INTCODE","STRCODE");

        //otblRefere.Get(sTipoEstadoPago, sEstadoPago);
        //if (otblRefere.Respuesta)
            iEstadoPago = new CsConsultasVuelos().ConsultaCodigo(sEstadoPago,"TBLESTADOS_RESERVA","INTCODE","STRCODE");

        //otblRefere.Get(sTipoFormaPago, sFormaPago);
        //if (otblRefere.Respuesta)
            iFormaPago = "1";

        
        csCarCompras.SaveDataProject(idRecord, sCodCoordinador, sContacto, "0", iEstadoReserva, iFormaPago, iEstadoPago);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Record_"></param>
    /// <param name="vo_PrintersTicketsRQ"></param>
    /// <returns></returns>
    public clsResultados GetDsEmitirTicketSabreAir(string Record_, VO_PrintersTicketsRQ vo_PrintersTicketsRQ)
    {
        /*METODO QUE RETORNA UN DATASET CON LOS RESULTADOS DEL OBJETO DE SABRE*/
        DataSet dsSabreAirPrinter = new DataSet();
        DataSet dsSabreAirTicket = new DataSet();
        clsResultados objResultadosPrinter = new clsResultados();
        clsResultados objResultadosTicket = new clsResultados();
        clsParametros objParametros = new clsParametros();
        try
        {
            Negocios_WebServiceSession._CerrarSesion();
            //Utils.clsSesiones.CLEAR_SESSION_AIR();

            bool bEntra = true;
            objResultadosPrinter = GetDsBusquedaRecordSabreAir(Record_);
            if (objResultadosPrinter.Error.Id.Equals(0))
            {
                string sResponse = new clsEndTransactionLLS().Abrir_ReservaCommand(Record_);
                if (!sResponse.Equals("Error"))
                {
                    if (sResponse.Contains("SECURED PNR"))
                    {
                        objParametros.ViewMessage.Add("SECURED PNR");
                        bEntra = false;
                    }
                }
            }
            else
            {
                try
                {
                    DataTable dtEticket = objResultadosPrinter.dsResultados.Tables["Ticketing"];
                    if (dtEticket.Rows.Count > 0)
                    {
                        int iPosT = 1;
                        for (int t = 0; t < dtEticket.Rows.Count; t++)
                        {
                            if (dtEticket.Rows[t]["eTicketNumber"].ToString().Length > 0)
                            {
                                if (iPosT.Equals(1))
                                {
                                    objParametros.ViewMessage.Add("Tiene boleto emitidos: ");
                                    bEntra = false;
                                }
                                objParametros.ViewMessage.Add(iPosT.ToString() + ". " + dtEticket.Rows[t]["eTicketNumber"].ToString());
                                iPosT++;
                            }
                        }
                        objParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        objParametros.Message = "Tiene boletos emitidos, Record: " + Record_;
                        try
                        {
                            Negocios_WebServiceSabreCommand.setEmailError(objParametros, "Emision de boletos " + Record_);
                        }
                        catch { }
                    }
                    DataTable dtReservation = objResultadosPrinter.dsResultados.Tables["ReservationItems"];
                    if (dtReservation.Rows.Count == 0)
                    {
                        objParametros.ViewMessage.Add("Reserva Cancelada");
                        objParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        objParametros.Message = "Reserva Cancelada " + Record_;
                        try
                        {
                            Negocios_WebServiceSabreCommand.setEmailError(objParametros, "Emision de boletos " + Record_);
                        }
                        catch { }
                        bEntra = false;
                    }
                }
                catch { }
            }
            if (bEntra)
            {
                string sComando = "*I";
                string sReponse = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                if (!sReponse.Contains("NO ITIN"))
                {
                    sComando = "*PQ";
                    sReponse = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                    vo_PrintersTicketsRQ.PQNumber = clsValidacionesVuelos.setBuscaPQ(sReponse);
                    // LINEAS PARA EJECUCION MANUAL
                    //string sComando = "W*CO44AAAF";
                    sComando = "W*" + vo_PrintersTicketsRQ.Countrycode + vo_PrintersTicketsRQ.PrtTicket;

                    //string sResponse = new clsEndTransactionLLS().Abrir_ReservaCommand(Record_);
                    sReponse = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                    //sComando = "PTR/44AAAE";
                    sComando = "PTR/" + vo_PrintersTicketsRQ.PrtItinerario;
                    sReponse = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);

                    //DesignatePrinterRS oDesignatePrinterRS = new clsDesignatePrinter().getDesignatePrinter(vo_PrintersTicketsRQ);
                    //if (oDesignatePrinterRS != null)
                    //{
                    //    if (oDesignatePrinterRS.Success != null)
                    //    {
                    /*CONVERTIMOS EL OBJETO EN UN DATASET*/
                    //dsSabreAirPrinter = clsEsquema.GetDatasetSabreAir(oDesignatePrinterRS);

                    //if (dsSabreAirPrinter != null && dsSabreAirPrinter.Tables.Count > 0)
                    //{
                    //    objResultadosPrinter.dsResultados = dsSabreAirPrinter;
                    objParametros.Id = 1;
                    AirTicketRS oAirTicketRS = new clsDesignatePrinter().getAirTicketRS(vo_PrintersTicketsRQ);
                    if (oAirTicketRS != null)
                    {
                        if (oAirTicketRS.Success != null)
                        {
                            sComando = "ER";
                            sReponse = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                            objResultadosPrinter = GetDsBusquedaRecordSabreAir(Record_);
                            try
                            {
                                DataTable dtEticketFin = objResultadosPrinter.dsResultados.Tables["Ticketing"];
                                if (dtEticketFin.Rows.Count > 0)
                                {
                                    if (dtEticketFin.Rows[0]["eTicketNumber"].ToString().Length > 0)
                                    {
                                        int iPosTE = 1;
                                        for (int i = 0; i < dtEticketFin.Rows.Count; i++)
                                        {
                                            if (dtEticketFin.Rows[i]["eTicketNumber"].ToString().Length > 0)
                                            {
                                                if (iPosTE.Equals(1))
                                                {
                                                    objParametros.ViewMessage.Add("Tiquetes emitidos: ");
                                                    bEntra = false;
                                                }
                                                objParametros.ViewMessage.Add(iPosTE.ToString() + ". " + dtEticketFin.Rows[i]["eTicketNumber"].ToString());
                                                iPosTE++;
                                            }
                                        }
                                    }
                                }
                            }
                            catch { }
                            objResultadosPrinter.dsResultados = dsSabreAirTicket;
                            objResultadosPrinter.Error = objParametros;
                            /*CONVERTIMOS EL OBJETO EN UN DATASET*/
                            //dsSabreAirTicket = clsEsquema.GetDatasetSabreAir(oAirTicketRS);

                            //if (dsSabreAirTicket != null && dsSabreAirTicket.Tables.Count > 0)
                            //{
                            //    objResultadosTicket.dsResultados = dsSabreAirTicket;
                            objParametros.Id = 1;
                            //    for (int i = 0; i < oAirTicketRS.NameSelect.Length; i++)
                            //    {
                            //        objParametros.DatoAdicArr.Add(oAirTicketRS.NameSelect[i].NameNumber);
                            //    }
                            //}
                            //else
                            //{   /*SI EL DATASET ES NULLO O VACIO*/
                            //    objResultadosTicket.dsResultados = null;
                            //    objParametros.Id = 0;
                            //    objParametros.Message = "Error al guardar datos";
                            //    objParametros.Metodo = "GetDsBusquedaSabreAir";
                            //    objParametros.ViewMessage.Add("Error al guardar datos");
                            //    objParametros.Severity = clsSeveridad.Media;
                            //    objParametros.Code = "0";
                            //    objParametros.Sugerencia.Add("");
                            //}
                        }
                        else
                        {
                            /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                            objParametros.Id = 0;
                            objParametros.Tipo = clsTipoError.WebServices;
                            objParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                            objParametros.ViewMessage.Add("Error al generar los Tikets");
                            objParametros.Message = "Error al generar los tickets ";
                            try
                            {
                                if (oAirTicketRS.Errors != null)
                                {
                                    objParametros.Code = oAirTicketRS.Errors.Error.ErrorCode;
                                    objParametros.Info = oAirTicketRS.Errors.Error.ErrorInfo.Message;
                                    objParametros.Message = oAirTicketRS.Errors.Error.ErrorMessage;
                                    objParametros.ViewMessage.Add(oAirTicketRS.Errors.Error.ErrorInfo.Message);
                                }
                            }
                            catch { }
                            //objParametros.Code = oDesignatePrinterRS.Errors.Error.ErrorCode;
                            //objParametros.Info = oDesignatePrinterRS.Errors.Error.ErrorInfo.Message;
                            objParametros.ValidaInfo = true;
                            //objParametros.Severity = oDesignatePrinterRS.Errors.Error.Severity;
                            objParametros.Sugerencia.Add("");
                            ExceptionHandled.Publicar(objParametros);
                        }
                    }
                    else
                    {
                        /*SI LOS RESULTADOS DE LABUSQUEDA TRAEN ERRORES DE SABRE*/
                        objResultadosTicket.dsResultados = null;
                        objParametros.Id = 0;
                        objParametros.Message = "Hubo una excepcion tratando de convertir los resultados";
                        objParametros.ViewMessage.Add("Emision fallida");
                        objParametros.Sugerencia.Add("");
                        objParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        objParametros.Severity = clsSeveridad.Alta;
                        objParametros.Code = "0";
                        objParametros.Tipo = clsTipoError.WebServices;
                        //objParametros.ViewMessage.Add("Reserva no existe");
                    }
                }
                else
                {
                    objResultadosTicket.dsResultados = null;
                    objParametros.Id = 0;
                    objParametros.Message = "Sin itinerario";
                    objParametros.ViewMessage.Add("Reserva sin itinerario");
                    objParametros.Sugerencia.Add("");
                    objParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    objParametros.Severity = clsSeveridad.Alta;
                    objParametros.Code = "0";
                    objParametros.Tipo = clsTipoError.WebServices;
                    objParametros.Tipo = clsTipoError.WebServices;
                }
            }
            else
            {
                objResultadosPrinter.dsResultados = null;
                objParametros.Id = 0;
                objParametros.Message = "Reserva con boletos";
                objParametros.Sugerencia.Add("");
                objParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                objParametros.Severity = clsSeveridad.Alta;
                objParametros.Code = "0";
                objParametros.Tipo = clsTipoError.WebServices;
                try
                {
                    Negocios_WebServiceSabreCommand.setEmailError(objParametros, "Error Sabre");
                }
                catch { }
            }
        }
        catch (Exception Ex)
        {
            /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
            objResultadosPrinter.dsResultados = null;
            objParametros.Id = 0;
            objParametros.Message = Ex.Message;
            objParametros.Metodo = Ex.TargetSite.Name;
            objParametros.Complemento = "Hubo una excepcion tratando de convertir los resultados";
            objParametros.ViewMessage.Add("Error al consutar la Reserva");
            objParametros.Code = "0";
            objParametros.Sugerencia.Add("");
            objParametros.Severity = clsSeveridad.Alta;
            objParametros.Tipo = clsTipoError.WebServices;
            ExceptionHandled.Publicar(objParametros);
        }
        /*AGREGAMOS LOS PARAMETROS AL OBJETO DE BUSQUEDA*/
        //try { Negocios_WebServiceSession._CerrarSesion(); }
        //catch { }
        if (objParametros.Id == 0)
        {
            objParametros.ErrorConfigura[0] = csReferencias.csEmpresa();
            objParametros.MessageBD = true;
            objParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
        }
        objResultadosTicket.Error = objParametros;
        objResultadosPrinter.Error = objParametros;

        return objResultadosTicket;
    }

    #endregion
}
