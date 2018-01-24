using System;
using System.Collections.Generic;
using System.Text;
using OTA_AirLowFareSearchRQ = WS_SsoftSabre.OTA_AirLowFareSearch;
using Ssoft.ValueObjects;
using WS_SsoftSabre.OTA_AirLowFareSearch;
using System.Configuration;
using Espacio_validaciones = WS_SsoftSabre.Utilidades;
using Ssoft.Utils;
using Ssoft.Rules.WebServices;
using Ssoft.ManejadorExcepciones;
using System.Xml.Serialization;

namespace WS_SsoftSabre.Air
{
    public class clsAirOTA_AirLowFareSearch
    {
        #region [ CONSTRUCTOR ]
        public clsAirOTA_AirLowFareSearch()
        {
            session_ = AutenticacionSabre.GET_SabreSession();
        }
        #endregion

        #region [CAMPOS]
        string session_ = string.Empty;
        Ssoft.ValueObjects.VO_Credentials objvo_Credentials;
        #endregion

        #region [ PROPIEDADES ]

        public string Session_
        {
            get { return session_; }
            set { session_ = value; }
        }

        #endregion

        #region [ METODOS]
        public OTA_AirLowFareSearchRS getBusqueda(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
            /*METODO PRINCICPAL QUE RETORNA EL OBJETO DE RESULTADOS DE SABRE*/
            StringBuilder consulta = new StringBuilder();
            clsParametros cParametros = new clsParametros();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
            string sIdioma = clsSesiones.getIdioma();

            csVuelos cVuelos = new csVuelos();
            if (vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida.Count.Equals(0))
                vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = cVuelos.AerolineasPreferidas();

            if(vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida.Count.Equals(0))
                vo_OTA_AirLowFareSearchLLSRQ.LsExcluirAerolinea = cVuelos.ExcluirAerolineas();

            bool PriorityTime = false;

            OTA_AirLowFareSearchRQ.OTA_AirLowFareSearchRQ oOTA_AirLowFareSearchRQ = new OTA_AirLowFareSearchRQ.OTA_AirLowFareSearchRQ();
            OTA_AirLowFareSearchRS oOTA_AirLowFareSearchRS = new OTA_AirLowFareSearchRS();
            objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();

            try
            {
                List<string> lsContadorOpciones = new List<string>();
                OTA_AirLowFareSearch.MessageHeader Mensaje_ = clsSabreBase.__ISabre_OTA_AirLowFareSearchLLSRQ();

                if (Mensaje_ != null)
                {
                    OTA_AirLowFareSearch.Security Seguridad_ = new OTA_AirLowFareSearch.Security();
                    Seguridad_.BinarySecurityToken = Session_;

                    #region [ POS ]
                    OTA_AirLowFareSearchRQPOS oOTA_AirLowFareSearchRQPOS = new OTA_AirLowFareSearchRQPOS();
                    OTA_AirLowFareSearchRQPOSSource oOTA_AirLowFareSearchRQPOSSource = new OTA_AirLowFareSearchRQPOSSource();

                    oOTA_AirLowFareSearchRQPOSSource.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                    oOTA_AirLowFareSearchRQPOS.Source = oOTA_AirLowFareSearchRQPOSSource;
                    oOTA_AirLowFareSearchRQ.POS = oOTA_AirLowFareSearchRQPOS;

                    oOTA_AirLowFareSearchRQ.AltLangID = sIdioma;
                    oOTA_AirLowFareSearchRQ.PrimaryLangID = sIdioma;

                    #endregion

                    #region [ VERSION ]
                    oOTA_AirLowFareSearchRQ.Version = clsSabreBase.SABRE_VERSION_OTA_AIRLOWFARESEARCH;
                    #endregion

                    #region [ ORIGINDESTINATIONINFORMATION ]

                    List<VO_OriginDestinationInformation> lvo_Rutas = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas;

                    if (lvo_Rutas == null)
                    {
                        throw new Exception("No se recibieron rutas a procesar");
                    }
                    else
                    {
                        int iContadorRutas = 0;
                        OTA_AirLowFareSearchRQOriginDestinationInformation[] aOTA_AirLowFareSearchRQOriginDestinationInformation =
                            new OTA_AirLowFareSearchRQOriginDestinationInformation[lvo_Rutas.Count];

                        int iOriginDestinationInformation = 0;
                        foreach (VO_OriginDestinationInformation vo_OriginDestinationInformation in lvo_Rutas)
                        {
                            OTA_AirLowFareSearchRQOriginDestinationInformation oOTA_AirLowFareSearchRQOriginDestinationInformation = new OTA_AirLowFareSearchRQOriginDestinationInformation();
                            oOTA_AirLowFareSearchRQOriginDestinationInformation.DepartureDateTime = vo_OriginDestinationInformation.SFechaSalida;
                            oOTA_AirLowFareSearchRQOriginDestinationInformation.DepartureWindow = vo_OriginDestinationInformation.SIntervaloSalida;
                            oOTA_AirLowFareSearchRQOriginDestinationInformation.ArrivalWindow = vo_OriginDestinationInformation.SIntervaloLlegada;
                            oOTA_AirLowFareSearchRQOriginDestinationInformation.ReturnDateTime = vo_OriginDestinationInformation.SFechaLlegada;
                            if (vo_OriginDestinationInformation.SFechaSalida != null)
                            {
                                if (vo_OriginDestinationInformation.SFechaSalida.IndexOf("T") > -1)
                                    PriorityTime = true;
                            }
                            if (vo_OriginDestinationInformation.Vo_AeropuertoOrigen != null)
                            {
                                VO_Aeropuerto vo_Origen = vo_OriginDestinationInformation.Vo_AeropuertoOrigen;

                                OTA_AirLowFareSearchRQOriginDestinationInformationOriginLocation oOriginLocation = new OTA_AirLowFareSearchRQOriginDestinationInformationOriginLocation();

                                oOriginLocation.CodeContext = vo_Origen.SContexto;
                                oOriginLocation.LocationCode = vo_Origen.SCodigo;

                                oOTA_AirLowFareSearchRQOriginDestinationInformation.OriginLocation = oOriginLocation;

                            }
                            VO_Aeropuerto vo_Destino = vo_OriginDestinationInformation.Vo_AeropuertoDestino;
                            OTA_AirLowFareSearchRQOriginDestinationInformationDestinationLocation oDestinationLocation =
                                new OTA_AirLowFareSearchRQOriginDestinationInformationDestinationLocation();
                            oDestinationLocation.LocationCode = vo_Destino.SCodigo;
                            oDestinationLocation.CodeContext = vo_Destino.SContexto;
                            oOTA_AirLowFareSearchRQOriginDestinationInformation.DestinationLocation = oDestinationLocation;

                            #region [ TPA_EXTENSIONS ]

                            OTA_AirLowFareSearchRQOriginDestinationInformationTPA_Extensions oOriginDestinationInformationTPA_Extensions =
                                new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_Extensions();

                            OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentType oTPA_ExtensionsSegmentType =
                                new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentType();

                            oTPA_ExtensionsSegmentType.CodeSpecified = true;
                            switch (vo_OriginDestinationInformation.OTipoSegmento.ToString())
                            {
                                case TipoSegmento.O:
                                    oTPA_ExtensionsSegmentType.Code = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                                    break;

                                case TipoSegmento.ARUNK:
                                    oTPA_ExtensionsSegmentType.Code = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                                    break;

                                case TipoSegmento.X:
                                    oTPA_ExtensionsSegmentType.Code = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.X;
                                    break;
                            }
                            oOriginDestinationInformationTPA_Extensions.SegmentType = oTPA_ExtensionsSegmentType;


                            OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsWithoutAvail oSinSiaponibilidad =
                                new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsWithoutAvail();

                            oSinSiaponibilidad.Ind = false;
                            oSinSiaponibilidad.IndSpecified = true;

                            oOriginDestinationInformationTPA_Extensions.WithoutAvail = oSinSiaponibilidad;

                            if (vo_OriginDestinationInformation.BSinDisponibilidad)
                            {
                                OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsWithoutAvail oTPA_ExtensionsWithoutAvail =
                                    new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsWithoutAvail();

                                oTPA_ExtensionsWithoutAvail.IndSpecified = true;
                                oTPA_ExtensionsWithoutAvail.Ind = true;
                                oOriginDestinationInformationTPA_Extensions.WithoutAvail = oTPA_ExtensionsWithoutAvail;
                            }

                            string sTiempoAlternativo = vo_OriginDestinationInformation.STiempoAlternativo;
                            if (clsValidaciones.getValidarString(sTiempoAlternativo))
                            {
                                OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateTime oTPA_ExtensionsAlternateTime =
                                    new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateTime();

                                oTPA_ExtensionsAlternateTime.PlusMinus = sTiempoAlternativo;
                                oOriginDestinationInformationTPA_Extensions.AlternateTime = oTPA_ExtensionsAlternateTime;
                            }

                            VO_AeropuertoAlterno vo_AeropuertoAlterno = vo_OriginDestinationInformation.Vo_AeropuertoAlterno;
                            if (vo_AeropuertoAlterno != null)
                            {
                                OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirport oTPA_ExtensionsAlternateAirport =
                                    new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirport();

                                OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportSegment oAlternateAirportSegment =
                                    new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportSegment();
                                oAlternateAirportSegment.Number = vo_AeropuertoAlterno.SSegmento;
                                oTPA_ExtensionsAlternateAirport.Segment = oAlternateAirportSegment;

                                OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportSpecifiedLocation oAlternateAirportSpecifiedLocation =
                                    new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportSpecifiedLocation();
                                oAlternateAirportSpecifiedLocation.CodeContext = vo_AeropuertoAlterno.Vo_AeropuertoBase.SCodigo;
                                oAlternateAirportSpecifiedLocation.LocationCode = vo_AeropuertoAlterno.Vo_AeropuertoBase.SContexto;
                                oTPA_ExtensionsAlternateAirport.SpecifiedLocation = oAlternateAirportSpecifiedLocation;

                                OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportMileage oAlternateAirportMileage =
                                    new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportMileage();
                                oAlternateAirportMileage.Number = vo_AeropuertoAlterno.SMillaje;
                                oTPA_ExtensionsAlternateAirport.Mileage = oAlternateAirportMileage;


                                List<VO_Aeropuerto> lvo_Aeropuerto = vo_AeropuertoAlterno.Lvo_AeropuertoesAlternativas;

                                if (lvo_Aeropuerto != null)
                                {
                                    int iContadorCiudades = 0;
                                    OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportAlternateLocation[]
                                        aAlternateAirportAlternateLocation = new
                                        OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportAlternateLocation[lvo_Aeropuerto.Count];

                                    foreach (VO_Aeropuerto vo_AeropuertoAlt in lvo_Aeropuerto)
                                    {
                                        OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportAlternateLocation oAlternateAirportAlternateLocation
                                        = new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportAlternateLocation();

                                        oAlternateAirportAlternateLocation.CodeContext = vo_AeropuertoAlt.SContexto;
                                        oAlternateAirportAlternateLocation.LocationCode = vo_AeropuertoAlt.SCodigo;
                                        aAlternateAirportAlternateLocation[iContadorCiudades] = oAlternateAirportAlternateLocation;
                                        iContadorCiudades++;

                                    }
                                    oTPA_ExtensionsAlternateAirport.AlternateLocation = aAlternateAirportAlternateLocation;
                                }

                                oOriginDestinationInformationTPA_Extensions.AlternateAirport = oTPA_ExtensionsAlternateAirport;
                            }

                            oOTA_AirLowFareSearchRQOriginDestinationInformation.TPA_Extensions =
                                oOriginDestinationInformationTPA_Extensions;

                            #endregion

                            iOriginDestinationInformation = iContadorRutas + 1;
                            lsContadorOpciones.Add(iOriginDestinationInformation.ToString());
                            oOTA_AirLowFareSearchRQOriginDestinationInformation.RPH = iOriginDestinationInformation.ToString();
                            aOTA_AirLowFareSearchRQOriginDestinationInformation[iContadorRutas] =
                                oOTA_AirLowFareSearchRQOriginDestinationInformation;

                            iContadorRutas++;
                        }
                        oOTA_AirLowFareSearchRQ.OriginDestinationInformation = aOTA_AirLowFareSearchRQOriginDestinationInformation;
                    }
                    #endregion

                    #region [ TRAVELPREFERENCES ]

                    OTA_AirLowFareSearchRQTravelPreferences oOTA_AirLowFareSearchRQTravelPreferences =
                        new OTA_AirLowFareSearchRQTravelPreferences();

                    string sMaximasParadas = vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas;

                    if (clsValidaciones.getValidarString(sMaximasParadas))
                    {
                        if (sMaximasParadas.Equals("0"))
                        {
                            oOTA_AirLowFareSearchRQTravelPreferences.MaxStopsQuantity = sMaximasParadas;
                        }
                    }


                    List<string> lsAerolineaPreferida = vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida;

                    if (lsAerolineaPreferida != null)
                    {
                        int iContadorAerolinea = 0;
                        OTA_AirLowFareSearchRQTravelPreferencesVendorPref[] aOTA_AirLowFareSearchRQTravelPreferencesVendorPref =
                            new OTA_AirLowFareSearchRQTravelPreferencesVendorPref[lsContadorOpciones.Count];

                        foreach (string sOpcion in lsContadorOpciones)
                        {
                            foreach (string sAerolineaPreferida in lsAerolineaPreferida)
                            {
                                OTA_AirLowFareSearchRQTravelPreferencesVendorPref oOTA_AirLowFareSearchRQTravelPreferencesVendorPref =
                                    new OTA_AirLowFareSearchRQTravelPreferencesVendorPref();
                                oOTA_AirLowFareSearchRQTravelPreferencesVendorPref.Code = sAerolineaPreferida.Trim();
                                oOTA_AirLowFareSearchRQTravelPreferencesVendorPref.RPH = validarArunk(sOpcion, lvo_Rutas);
                                aOTA_AirLowFareSearchRQTravelPreferencesVendorPref[iContadorAerolinea] =
                                    oOTA_AirLowFareSearchRQTravelPreferencesVendorPref;
                                iContadorAerolinea++;
                            }
                        }

                        oOTA_AirLowFareSearchRQTravelPreferences.VendorPref = aOTA_AirLowFareSearchRQTravelPreferencesVendorPref;
                    }

                    List<string> lsClases = vo_OTA_AirLowFareSearchLLSRQ.LsClase;

                    if (lsClases != null)
                    {
                        int iContadorClases = 0;
                        OTA_AirLowFareSearchRQTravelPreferencesCabinPref[] aOTA_AirLowFareSearchRQTravelPreferencesCabinPref =
                            new OTA_AirLowFareSearchRQTravelPreferencesCabinPref[lsContadorOpciones.Count];

                        foreach (string sOpcion in lsContadorOpciones)
                        {
                            foreach (string sClase in lsClases)
                            {
                                OTA_AirLowFareSearchRQTravelPreferencesCabinPref oCabinPref =
                                    new OTA_AirLowFareSearchRQTravelPreferencesCabinPref();
                                oCabinPref.Code = sClase;
                                oCabinPref.RPH = validarArunk(sOpcion, lvo_Rutas);
                                aOTA_AirLowFareSearchRQTravelPreferencesCabinPref[iContadorClases] = oCabinPref;
                                iContadorClases++;
                            }
                        }

                        oOTA_AirLowFareSearchRQTravelPreferences.CabinPref =
                            aOTA_AirLowFareSearchRQTravelPreferencesCabinPref;
                    }

                    #region [ TPA_EXTENSIONS ]

                    OTA_AirLowFareSearchRQTravelPreferencesTPA_Extensions oTravelPreferencesTPA_Extensions =
                        new OTA_AirLowFareSearchRQTravelPreferencesTPA_Extensions();

                    List<string> lsNoAerolineas = vo_OTA_AirLowFareSearchLLSRQ.LsExcluirAerolinea;

                    if (lsNoAerolineas != null)
                    {
                        int iContNoAerolineas = 0;
                        OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref[] aOTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref =
                            new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref[lsNoAerolineas.Count];

                        foreach (string sAerolinea in lsNoAerolineas)
                        {
                            OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref oAerolinea =
                                new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref();
                            oAerolinea.Code = sAerolinea;
                            aOTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref[iContNoAerolineas] = oAerolinea;
                            iContNoAerolineas++;
                        }
                        oTravelPreferencesTPA_Extensions.ExcludeVendorPref =
                            aOTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref;
                    }

                    /*string sViajes = vo_OTA_AirLowFareSearchLLSRQ.SVuelosARetornar;
                    if (Validaciones.getValidarString(sViajes))
                    {
                        OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsNumTrips oNumTrips =
                            new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsNumTrips();
                        oNumTrips.Number = sViajes;
                        oTravelPreferencesTPA_Extensions.NumTrips = oNumTrips;
                    }*/

                    OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsReturnMaxData oReturnMaxData =
                        new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsReturnMaxData();
                    oReturnMaxData.Ind = vo_OTA_AirLowFareSearchLLSRQ.BRetornarMaxResultados;
                    oTravelPreferencesTPA_Extensions.ReturnMaxData = oReturnMaxData;

                    OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsOnlineIndicator oOnlineIndicator =
                        new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsOnlineIndicator();
                    oOnlineIndicator.Ind = vo_OTA_AirLowFareSearchLLSRQ.BOnline;
                    oTravelPreferencesTPA_Extensions.OnlineIndicator = oOnlineIndicator;


                    OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsInterlineIndicator InterlineIndicator =
                        new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsInterlineIndicator();
                    InterlineIndicator.Ind = vo_OTA_AirLowFareSearchLLSRQ.BInterLineado;
                    oTravelPreferencesTPA_Extensions.InterlineIndicator = InterlineIndicator;

                    oTravelPreferencesTPA_Extensions.TicketingDate = vo_OTA_AirLowFareSearchLLSRQ.SFechaTiqueteo;
                    oTravelPreferencesTPA_Extensions.DepartureWindow = vo_OTA_AirLowFareSearchLLSRQ.SPreferenciaIntervaloSal;
                    oTravelPreferencesTPA_Extensions.ArrivalWindow = vo_OTA_AirLowFareSearchLLSRQ.SPreferenciaIntervaloLleg;

                    OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsNumTrips oNumTrips =
                        new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsNumTrips();
                    oNumTrips.Number = "19";
                    oTravelPreferencesTPA_Extensions.NumTrips = oNumTrips;

                    oOTA_AirLowFareSearchRQTravelPreferences.TPA_Extensions = oTravelPreferencesTPA_Extensions;

                    #endregion


                    oOTA_AirLowFareSearchRQ.TravelPreferences = oOTA_AirLowFareSearchRQTravelPreferences;

                    #endregion

                    #region [ TRAVELERINFORMATION ]

                    List<VO_Pasajero> lvo_Pasajero = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros;

                    if (lvo_Pasajero == null)
                    {
                        throw new Exception("No se especificó el numero y tipo de pasajeros");
                    }
                    else
                    {
                        int iContPasajeros = 0;
                        int iContPaxTotal = 0;
                        foreach (VO_Pasajero vo_PasajeroTotal in lvo_Pasajero)
                        {
                            if (vo_PasajeroTotal.SCodigo.Equals("CNN"))
                            {
                                iContPaxTotal += int.Parse(vo_PasajeroTotal.SCantidad);
                            }
                            else
                            {
                                iContPaxTotal ++;
                            }
                        }

                        OTA_AirLowFareSearchRQTravelerInformation oOTA_AirLowFareSearchRQTravelerInformation =
                            new OTA_AirLowFareSearchRQTravelerInformation();

                        OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity[] aOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity =
                            new OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity[iContPaxTotal];

                        //bool bCodPax = false;

                        //string sPaxNegociada = vo_OTA_AirLowFareSearchLLSRQ.SCodPaxNegociada;
                        //if (clsValidaciones.getValidarString(sPaxNegociada))
                        //{
                        //    bCodPax = true;
                        //}
                        //if (bCodPax)
                        //{
                        //    int iCountPax = 0;
                        //    foreach (VO_Pasajero vo_Pasajero in lvo_Pasajero)
                        //    {
                        //        iCountPax += int.Parse(vo_Pasajero.SCantidad);
                        //    }

                        //    OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity =
                        //        new OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity();

                        //    oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity.Code = sPaxNegociada;
                        //    oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity.Quantity = iCountPax.ToString();

                        //    aOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity[iContPasajeros] =
                        //        oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity;
                        //    iContPasajeros++;

                        //}
                        //else
                        //{
                            foreach (VO_Pasajero vo_Pasajero in lvo_Pasajero)
                            {
                                if (vo_Pasajero.SCodigo.Equals("CNN"))
                                {
                                    foreach (VO_ClasificaPasajero vo_CatPasajero in vo_Pasajero.LvPasajeroNino)
                                    {
                                        OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity =
                                            new OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity();

                                        oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity.Code = vo_CatPasajero.SCodigo;
                                        oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity.Quantity = vo_CatPasajero.SCantidad;

                                        aOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity[iContPasajeros] =
                                            oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity;
                                        iContPasajeros++;
                                    }
                                }
                                else
                                {
                                    OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity =
                                        new OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity();

                                    oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity.Code = vo_Pasajero.SCodigo;
                                    oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity.Quantity = vo_Pasajero.SCantidad;

                                    aOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity[iContPasajeros] =
                                        oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity;
                                    iContPasajeros++;
                                }
                            }
                        //}
                        oOTA_AirLowFareSearchRQTravelerInformation.PassengerTypeQuantity = aOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity;
                        oOTA_AirLowFareSearchRQ.TravelerInformation = oOTA_AirLowFareSearchRQTravelerInformation;
                    }

                    #endregion

                    #region [ PRICEREQUESTINFORMATION ]

                    OTA_AirLowFareSearchRQPriceRequestInformation oOTA_AirLowFareSearchRQPriceRequestInformation =
                        new OTA_AirLowFareSearchRQPriceRequestInformation();

                    string sMonedaCotizar = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion;
                    if (clsValidaciones.getValidarString(sMonedaCotizar))
                    {
                        oOTA_AirLowFareSearchRQPriceRequestInformation.CurrencyCode = sMonedaCotizar;
                    }

                    string sTarifaNegociada = vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada;
                    if (clsValidaciones.getValidarString(sTarifaNegociada))
                    {
                        //OTA_AirLowFareSearchRQPriceRequestInformationNegotiatedFareCode oOTA_AirLowFareSearchRQPriceRequestInformationNegotiatedFareCode =
                        //    new OTA_AirLowFareSearchRQPriceRequestInformationNegotiatedFareCode();

                        //oOTA_AirLowFareSearchRQPriceRequestInformationNegotiatedFareCode.Code = sTarifaNegociada;

                        //oOTA_AirLowFareSearchRQPriceRequestInformation.NegotiatedFareCode = oOTA_AirLowFareSearchRQPriceRequestInformationNegotiatedFareCode;
                    }

                    #region [ TPA_EXTENSIONS ]

                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions oOTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions =
                        new OTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions();

                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsFareCalc oFareCalc =
                        new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsFareCalc();

                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsFareCalcFareBasis oFareCalcFareBasis =
                        new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsFareCalcFareBasis();

                    //oFareCalc.FareBasis=new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsFareCalcFareBasis


                    /*oOTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions.FareCalc=oFareCalc; 
                    
                    oOTA_AirLowFareSearchRQPriceRequestInformation.TPA_Extensions=
                        oOTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions;*/


                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriority oPriority =
                        new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriority();

                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityPrice oPriorityPrice =
                        new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityPrice();
                    if (PriorityTime)
                        oPriorityPrice.Priority = "2";
                    else
                        oPriorityPrice.Priority = "1";

                    oPriority.Price = oPriorityPrice;

                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityVendor oPriorityVendor =
                      new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityVendor();
                    if (PriorityTime)
                        oPriorityVendor.Priority = "3";
                    else
                        oPriorityVendor.Priority = "2";

                    oPriority.Vendor = oPriorityVendor;


                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityTime oPriorityTime =
                      new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityTime();
                    if (PriorityTime)
                        oPriorityTime.Priority = "1";
                    else
                        oPriorityTime.Priority = "3";

                    oPriority.Time = oPriorityTime;

                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityDirectFlights oPriorityDirectFlights =
                     new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityDirectFlights();

                    oPriorityDirectFlights.Priority = "4";

                    oPriority.DirectFlights = oPriorityDirectFlights;

                    oOTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions.Priority = oPriority;

                    oOTA_AirLowFareSearchRQPriceRequestInformation.TPA_Extensions = oOTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions;

                    #endregion

                    oOTA_AirLowFareSearchRQ.PriceRequestInformation = oOTA_AirLowFareSearchRQPriceRequestInformation;

                    #endregion

                    OTA_AirLowFareSearchService oOTA_AirLowFareSearchService = new OTA_AirLowFareSearchService();

                    oOTA_AirLowFareSearchService.MessageHeaderValue = Mensaje_;
                    oOTA_AirLowFareSearchService.SecurityValue = Seguridad_;
                    oOTA_AirLowFareSearchService.Url = objvo_Credentials.UrlWebServices;
                    
                    oOTA_AirLowFareSearchRS = oOTA_AirLowFareSearchService.OTA_AirLowFareSearchRQ(oOTA_AirLowFareSearchRQ);
                    if (oOTA_AirLowFareSearchRS.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = oOTA_AirLowFareSearchRS.Errors.Error.ErrorCode;
                        cParametros.Info = oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = oOTA_AirLowFareSearchRS.Errors.Error.ErrorMessage;
                        cParametros.Severity = oOTA_AirLowFareSearchRS.Errors.Error.Severity;
                        cParametros.Complemento = "HostCommand: " + oOTA_AirLowFareSearchRS.TPA_Extensions.HostCommand;
                        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        cParametros.Tipo = clsTipoError.WebServices;
                        consulta.AppendLine("Credenciales: ");
                        try
                        {
                            if (objvo_Credentials != null)
                            {
                                consulta.AppendLine("User: " + objvo_Credentials.User);
                                consulta.AppendLine("Password: " + objvo_Credentials.Password);
                                consulta.AppendLine("Ipcc: " + objvo_Credentials.Ipcc);
                                consulta.AppendLine("Pcc: " + objvo_Credentials.Pcc);
                                consulta.AppendLine("QNumber: " + objvo_Credentials.QNumber);
                                consulta.AppendLine("Dominio: " + objvo_Credentials.Dominio);
                                consulta.AppendLine("Session Sabre: " + Session_.ToString());
                                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                            }
                        }
                        catch { }
                        cParametros.TargetSite = consulta.ToString();
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
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.Message = oOTA_AirLowFareSearchRS.Success;
                        cParametros.TipoLog = Enum_Error.Transac;
                        consulta.AppendLine("Credenciales: ");
                        try
                        {
                            if (objvo_Credentials != null)
                            {
                                consulta.AppendLine("User: " + objvo_Credentials.User);
                                consulta.AppendLine("Password: " + objvo_Credentials.Password);
                                consulta.AppendLine("Ipcc: " + objvo_Credentials.Ipcc);
                                consulta.AppendLine("Pcc: " + objvo_Credentials.Pcc);
                                consulta.AppendLine("QNumber: " + objvo_Credentials.QNumber);
                                consulta.AppendLine("Dominio: " + objvo_Credentials.Dominio);
                                consulta.AppendLine("Session Sabre: " + Session_.ToString());
                                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                            }
                        }
                        catch { }
                        cParametros.TargetSite = consulta.ToString();
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
                        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        cParametros.Complemento = "HostCommand: " + oOTA_AirLowFareSearchRS.TPA_Extensions.HostCommand;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Severity = clsSeveridad.Moderada;
                        ExceptionHandled.Publicar(cParametros);
                        vo_OTA_AirLowFareSearchLLSRQ.Ruta++;
                        try
                        {
                            vo_OTA_AirLowFareSearchLLSRQ.SVuelosARetornar = oOTA_AirLowFareSearchRS.PricedItineraries.Length.ToString();
                        }
                        catch { }
                        clsSesiones.setParametrosAirBargain
                           (
                              vo_OTA_AirLowFareSearchLLSRQ
                           );
                    }
                }
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
            return oOTA_AirLowFareSearchRS;
        }
        public OTA_AirLowFareSearchRS getBusquedaPorHoras(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
            /*METODO PRINCICPAL QUE RETORNA EL OBJETO DE RESULTADOS DE SABRE*/
            clsParametros cParametros = new clsParametros();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
            StringBuilder consulta = new StringBuilder();
            csVuelos cVuelos = new csVuelos();
            vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = cVuelos.AerolineasPreferidas();
            if (vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida.Count.Equals(0))
                vo_OTA_AirLowFareSearchLLSRQ.LsExcluirAerolinea = cVuelos.ExcluirAerolineas();
            bool PriorityTime = false;

            OTA_AirLowFareSearchRQ.OTA_AirLowFareSearchRQ oOTA_AirLowFareSearchRQ = new OTA_AirLowFareSearchRQ.OTA_AirLowFareSearchRQ();
            OTA_AirLowFareSearchRS oOTA_AirLowFareSearchRS = new OTA_AirLowFareSearchRS();
            objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();

            try
            {
                List<string> lsContadorOpciones = new List<string>();
                OTA_AirLowFareSearch.MessageHeader Mensaje_ = clsSabreBase.__ISabre_OTA_AirLowFareSearchLLSRQ();

                if (Mensaje_ != null)
                {
                    OTA_AirLowFareSearch.Security Seguridad_ = new OTA_AirLowFareSearch.Security();
                    Seguridad_.BinarySecurityToken = Session_;

                    #region [ POS ]
                    OTA_AirLowFareSearchRQPOS oOTA_AirLowFareSearchRQPOS = new OTA_AirLowFareSearchRQPOS();
                    OTA_AirLowFareSearchRQPOSSource oOTA_AirLowFareSearchRQPOSSource = new OTA_AirLowFareSearchRQPOSSource();

                    oOTA_AirLowFareSearchRQPOSSource.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                    oOTA_AirLowFareSearchRQPOS.Source = oOTA_AirLowFareSearchRQPOSSource;
                    oOTA_AirLowFareSearchRQ.POS = oOTA_AirLowFareSearchRQPOS;
                    #endregion

                    #region [ VERSION ]
                    oOTA_AirLowFareSearchRQ.Version = clsSabreBase.SABRE_VERSION_OTA_AIRLOWFARESEARCH;
                    #endregion

                    #region [ ORIGINDESTINATIONINFORMATION ]

                    List<VO_OriginDestinationInformation> lvo_Rutas = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas;

                    if (lvo_Rutas == null)
                    {
                        throw new Exception("No se recibieron rutas a procesar");
                    }
                    else
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.BHoras)
                        {
                            int iContadorRutas = 0;
                            OTA_AirLowFareSearchRQOriginDestinationInformation[] aOTA_AirLowFareSearchRQOriginDestinationInformation =
                                new OTA_AirLowFareSearchRQOriginDestinationInformation[1];

                            int iOriginDestinationInformation = vo_OTA_AirLowFareSearchLLSRQ.Ruta;
                            VO_OriginDestinationInformation vo_OriginDestinationInformation = lvo_Rutas[iOriginDestinationInformation];
                            
                                OTA_AirLowFareSearchRQOriginDestinationInformation oOTA_AirLowFareSearchRQOriginDestinationInformation = new OTA_AirLowFareSearchRQOriginDestinationInformation();
                                oOTA_AirLowFareSearchRQOriginDestinationInformation.DepartureDateTime = vo_OriginDestinationInformation.SFechaSalida;
                                oOTA_AirLowFareSearchRQOriginDestinationInformation.DepartureWindow = vo_OriginDestinationInformation.SIntervaloSalida;
                                oOTA_AirLowFareSearchRQOriginDestinationInformation.ArrivalWindow = vo_OriginDestinationInformation.SIntervaloLlegada;
                                oOTA_AirLowFareSearchRQOriginDestinationInformation.ReturnDateTime = vo_OriginDestinationInformation.SFechaLlegada;
                                if (vo_OriginDestinationInformation.SFechaSalida != null)
                                {
                                    if (vo_OriginDestinationInformation.SFechaSalida.IndexOf("T") > -1)
                                        PriorityTime = true;
                                }
                                if (vo_OriginDestinationInformation.Vo_AeropuertoOrigen != null)
                                {
                                    VO_Aeropuerto vo_Origen = vo_OriginDestinationInformation.Vo_AeropuertoOrigen;

                                    OTA_AirLowFareSearchRQOriginDestinationInformationOriginLocation oOriginLocation = new OTA_AirLowFareSearchRQOriginDestinationInformationOriginLocation();

                                    oOriginLocation.CodeContext = vo_Origen.SContexto;
                                    oOriginLocation.LocationCode = vo_Origen.SCodigo;

                                    oOTA_AirLowFareSearchRQOriginDestinationInformation.OriginLocation = oOriginLocation;

                                }
                                VO_Aeropuerto vo_Destino = vo_OriginDestinationInformation.Vo_AeropuertoDestino;
                                OTA_AirLowFareSearchRQOriginDestinationInformationDestinationLocation oDestinationLocation =
                                    new OTA_AirLowFareSearchRQOriginDestinationInformationDestinationLocation();
                                oDestinationLocation.LocationCode = vo_Destino.SCodigo;
                                oDestinationLocation.CodeContext = vo_Destino.SContexto;
                                oOTA_AirLowFareSearchRQOriginDestinationInformation.DestinationLocation = oDestinationLocation;

                                #region [ TPA_EXTENSIONS ]

                                OTA_AirLowFareSearchRQOriginDestinationInformationTPA_Extensions oOriginDestinationInformationTPA_Extensions =
                                    new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_Extensions();

                                OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentType oTPA_ExtensionsSegmentType =
                                    new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentType();

                                oTPA_ExtensionsSegmentType.CodeSpecified = true;
                                switch (vo_OriginDestinationInformation.OTipoSegmento.ToString())
                                {
                                    case TipoSegmento.O:
                                        oTPA_ExtensionsSegmentType.Code = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                                        break;

                                    case TipoSegmento.ARUNK:
                                        oTPA_ExtensionsSegmentType.Code = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                                        break;

                                    case TipoSegmento.X:
                                        oTPA_ExtensionsSegmentType.Code = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.X;
                                        break;
                                }
                                oOriginDestinationInformationTPA_Extensions.SegmentType = oTPA_ExtensionsSegmentType;


                                OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsWithoutAvail oSinSiaponibilidad =
                                    new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsWithoutAvail();

                                oSinSiaponibilidad.Ind = false;
                                oSinSiaponibilidad.IndSpecified = true;

                                oOriginDestinationInformationTPA_Extensions.WithoutAvail = oSinSiaponibilidad;

                                if (vo_OriginDestinationInformation.BSinDisponibilidad)
                                {
                                    OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsWithoutAvail oTPA_ExtensionsWithoutAvail =
                                        new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsWithoutAvail();

                                    oTPA_ExtensionsWithoutAvail.IndSpecified = true;
                                    oTPA_ExtensionsWithoutAvail.Ind = true;
                                    oOriginDestinationInformationTPA_Extensions.WithoutAvail = oTPA_ExtensionsWithoutAvail;
                                }

                                string sTiempoAlternativo = vo_OriginDestinationInformation.STiempoAlternativo;
                                if (clsValidaciones.getValidarString(sTiempoAlternativo))
                                {
                                    OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateTime oTPA_ExtensionsAlternateTime =
                                        new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateTime();

                                    oTPA_ExtensionsAlternateTime.PlusMinus = sTiempoAlternativo;
                                    oOriginDestinationInformationTPA_Extensions.AlternateTime = oTPA_ExtensionsAlternateTime;
                                }

                                VO_AeropuertoAlterno vo_AeropuertoAlterno = vo_OriginDestinationInformation.Vo_AeropuertoAlterno;
                                if (vo_AeropuertoAlterno != null)
                                {
                                    OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirport oTPA_ExtensionsAlternateAirport =
                                        new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirport();

                                    OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportSegment oAlternateAirportSegment =
                                        new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportSegment();
                                    oAlternateAirportSegment.Number = vo_AeropuertoAlterno.SSegmento;
                                    oTPA_ExtensionsAlternateAirport.Segment = oAlternateAirportSegment;

                                    OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportSpecifiedLocation oAlternateAirportSpecifiedLocation =
                                        new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportSpecifiedLocation();
                                    oAlternateAirportSpecifiedLocation.CodeContext = vo_AeropuertoAlterno.Vo_AeropuertoBase.SCodigo;
                                    oAlternateAirportSpecifiedLocation.LocationCode = vo_AeropuertoAlterno.Vo_AeropuertoBase.SContexto;
                                    oTPA_ExtensionsAlternateAirport.SpecifiedLocation = oAlternateAirportSpecifiedLocation;

                                    OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportMileage oAlternateAirportMileage =
                                        new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportMileage();
                                    oAlternateAirportMileage.Number = vo_AeropuertoAlterno.SMillaje;
                                    oTPA_ExtensionsAlternateAirport.Mileage = oAlternateAirportMileage;


                                    List<VO_Aeropuerto> lvo_Aeropuerto = vo_AeropuertoAlterno.Lvo_AeropuertoesAlternativas;

                                    if (lvo_Aeropuerto != null)
                                    {
                                        int iContadorCiudades = 0;
                                        OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportAlternateLocation[]
                                            aAlternateAirportAlternateLocation = new
                                            OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportAlternateLocation[lvo_Aeropuerto.Count];

                                        foreach (VO_Aeropuerto vo_AeropuertoAlt in lvo_Aeropuerto)
                                        {
                                            OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportAlternateLocation oAlternateAirportAlternateLocation
                                            = new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateAirportAlternateLocation();

                                            oAlternateAirportAlternateLocation.CodeContext = vo_AeropuertoAlt.SContexto;
                                            oAlternateAirportAlternateLocation.LocationCode = vo_AeropuertoAlt.SCodigo;
                                            aAlternateAirportAlternateLocation[iContadorCiudades] = oAlternateAirportAlternateLocation;
                                            iContadorCiudades++;

                                        }
                                        oTPA_ExtensionsAlternateAirport.AlternateLocation = aAlternateAirportAlternateLocation;
                                    }

                                    oOriginDestinationInformationTPA_Extensions.AlternateAirport = oTPA_ExtensionsAlternateAirport;

                                oOTA_AirLowFareSearchRQOriginDestinationInformation.TPA_Extensions =
                                    oOriginDestinationInformationTPA_Extensions;

                                #endregion

                                lsContadorOpciones.Add(iOriginDestinationInformation.ToString());
                                oOTA_AirLowFareSearchRQOriginDestinationInformation.RPH = iOriginDestinationInformation.ToString();
                                aOTA_AirLowFareSearchRQOriginDestinationInformation[iContadorRutas] =
                                    oOTA_AirLowFareSearchRQOriginDestinationInformation;

                            }
                            oOTA_AirLowFareSearchRQ.OriginDestinationInformation =
                                aOTA_AirLowFareSearchRQOriginDestinationInformation;
                        }
                        else
                        {
                        }
                    }
                    #endregion

                    #region [ TRAVELPREFERENCES ]

                    OTA_AirLowFareSearchRQTravelPreferences oOTA_AirLowFareSearchRQTravelPreferences =
                        new OTA_AirLowFareSearchRQTravelPreferences();

                    string sMaximasParadas = vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas;

                    if (clsValidaciones.getValidarString(sMaximasParadas))
                    {
                        if (sMaximasParadas.Equals("0"))
                        {
                            oOTA_AirLowFareSearchRQTravelPreferences.MaxStopsQuantity = sMaximasParadas;
                        }
                    }


                    List<string> lsAerolineaPreferida = vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida;

                    if (lsAerolineaPreferida != null)
                    {
                        int iContadorAerolinea = 0;
                        OTA_AirLowFareSearchRQTravelPreferencesVendorPref[] aOTA_AirLowFareSearchRQTravelPreferencesVendorPref =
                            new OTA_AirLowFareSearchRQTravelPreferencesVendorPref[lsContadorOpciones.Count];

                        foreach (string sOpcion in lsContadorOpciones)
                        {
                            foreach (string sAerolineaPreferida in lsAerolineaPreferida)
                            {
                                OTA_AirLowFareSearchRQTravelPreferencesVendorPref oOTA_AirLowFareSearchRQTravelPreferencesVendorPref =
                                    new OTA_AirLowFareSearchRQTravelPreferencesVendorPref();
                                oOTA_AirLowFareSearchRQTravelPreferencesVendorPref.Code = sAerolineaPreferida.Trim();
                                oOTA_AirLowFareSearchRQTravelPreferencesVendorPref.RPH = validarArunk(sOpcion, lvo_Rutas);
                                aOTA_AirLowFareSearchRQTravelPreferencesVendorPref[iContadorAerolinea] =
                                    oOTA_AirLowFareSearchRQTravelPreferencesVendorPref;
                                iContadorAerolinea++;
                            }
                        }

                        oOTA_AirLowFareSearchRQTravelPreferences.VendorPref = aOTA_AirLowFareSearchRQTravelPreferencesVendorPref;
                    }


                    List<string> lsClases = vo_OTA_AirLowFareSearchLLSRQ.LsClase;

                    if (lsClases != null)
                    {
                        int iContadorClases = 0;
                        OTA_AirLowFareSearchRQTravelPreferencesCabinPref[] aOTA_AirLowFareSearchRQTravelPreferencesCabinPref =
                            new OTA_AirLowFareSearchRQTravelPreferencesCabinPref[lsContadorOpciones.Count];

                        foreach (string sOpcion in lsContadorOpciones)
                        {
                            foreach (string sClase in lsClases)
                            {
                                OTA_AirLowFareSearchRQTravelPreferencesCabinPref oCabinPref =
                                    new OTA_AirLowFareSearchRQTravelPreferencesCabinPref();
                                oCabinPref.Code = sClase;
                                oCabinPref.RPH = validarArunk(sOpcion, lvo_Rutas);
                                aOTA_AirLowFareSearchRQTravelPreferencesCabinPref[iContadorClases] = oCabinPref;
                                iContadorClases++;
                            }
                        }

                        oOTA_AirLowFareSearchRQTravelPreferences.CabinPref =
                            aOTA_AirLowFareSearchRQTravelPreferencesCabinPref;
                    }

                    #region [ TPA_EXTENSIONS ]

                    OTA_AirLowFareSearchRQTravelPreferencesTPA_Extensions oTravelPreferencesTPA_Extensions =
                        new OTA_AirLowFareSearchRQTravelPreferencesTPA_Extensions();

                    List<string> lsNoAerolineas = vo_OTA_AirLowFareSearchLLSRQ.LsExcluirAerolinea;

                    if (lsNoAerolineas != null)
                    {
                        int iContNoAerolineas = 0;
                        OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref[] aOTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref =
                            new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref[lsNoAerolineas.Count];

                        foreach (string sAerolinea in lsNoAerolineas)
                        {
                            OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref oAerolinea =
                                new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref();
                            oAerolinea.Code = sAerolinea;
                            aOTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref[iContNoAerolineas] = oAerolinea;
                            iContNoAerolineas++;
                        }
                        oTravelPreferencesTPA_Extensions.ExcludeVendorPref =
                            aOTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref;
                    }

                    /*string sViajes = vo_OTA_AirLowFareSearchLLSRQ.SVuelosARetornar;
                    if (Validaciones.getValidarString(sViajes))
                    {
                        OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsNumTrips oNumTrips =
                            new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsNumTrips();
                        oNumTrips.Number = sViajes;
                        oTravelPreferencesTPA_Extensions.NumTrips = oNumTrips;
                    }*/

                    OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsReturnMaxData oReturnMaxData =
                        new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsReturnMaxData();
                    oReturnMaxData.Ind = vo_OTA_AirLowFareSearchLLSRQ.BRetornarMaxResultados;
                    oTravelPreferencesTPA_Extensions.ReturnMaxData = oReturnMaxData;

                    OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsOnlineIndicator oOnlineIndicator =
                        new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsOnlineIndicator();
                    oOnlineIndicator.Ind = vo_OTA_AirLowFareSearchLLSRQ.BOnline;
                    oTravelPreferencesTPA_Extensions.OnlineIndicator = oOnlineIndicator;


                    OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsInterlineIndicator InterlineIndicator =
                        new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsInterlineIndicator();
                    InterlineIndicator.Ind = vo_OTA_AirLowFareSearchLLSRQ.BInterLineado;
                    oTravelPreferencesTPA_Extensions.InterlineIndicator = InterlineIndicator;

                    oTravelPreferencesTPA_Extensions.TicketingDate = vo_OTA_AirLowFareSearchLLSRQ.SFechaTiqueteo;
                    oTravelPreferencesTPA_Extensions.DepartureWindow = vo_OTA_AirLowFareSearchLLSRQ.SPreferenciaIntervaloSal;
                    oTravelPreferencesTPA_Extensions.ArrivalWindow = vo_OTA_AirLowFareSearchLLSRQ.SPreferenciaIntervaloLleg;

                    OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsNumTrips oNumTrips =
                        new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsNumTrips();
                    oNumTrips.Number = "19";
                    oTravelPreferencesTPA_Extensions.NumTrips = oNumTrips;

                    oOTA_AirLowFareSearchRQTravelPreferences.TPA_Extensions = oTravelPreferencesTPA_Extensions;

                    #endregion


                    oOTA_AirLowFareSearchRQ.TravelPreferences = oOTA_AirLowFareSearchRQTravelPreferences;

                    #endregion

                    #region [ TRAVELERINFORMATION ]

                    List<VO_Pasajero> lvo_Pasajero = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros;

                    if (lvo_Pasajero == null)
                    {
                        throw new Exception("No se especificó el numero y tipo de pasajeros");
                    }
                    else
                    {
                        int iContPasajeros = 0;
                        OTA_AirLowFareSearchRQTravelerInformation oOTA_AirLowFareSearchRQTravelerInformation =
                            new OTA_AirLowFareSearchRQTravelerInformation();

                        OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity[] aOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity =
                            new OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity[lvo_Pasajero.Count];

                        foreach (VO_Pasajero vo_Pasajero in lvo_Pasajero)
                        {
                            OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity =
                                new OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity();

                            oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity.Code = vo_Pasajero.SCodigo;
                            oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity.Quantity = vo_Pasajero.SCantidad;

                            aOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity[iContPasajeros] =
                                oOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity;
                            iContPasajeros++;
                        }

                        oOTA_AirLowFareSearchRQTravelerInformation.PassengerTypeQuantity = aOTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity;
                        oOTA_AirLowFareSearchRQ.TravelerInformation = oOTA_AirLowFareSearchRQTravelerInformation;
                    }

                    #endregion

                    #region [ PRICEREQUESTINFORMATION ]

                    OTA_AirLowFareSearchRQPriceRequestInformation oOTA_AirLowFareSearchRQPriceRequestInformation =
                        new OTA_AirLowFareSearchRQPriceRequestInformation();

                    string sMonedaCotizar = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion;
                    if (clsValidaciones.getValidarString(sMonedaCotizar))
                    {
                        oOTA_AirLowFareSearchRQPriceRequestInformation.CurrencyCode = sMonedaCotizar;
                    }

                    string sTarifaNegociada = vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada;
                    if (clsValidaciones.getValidarString(sTarifaNegociada))
                    {
                        OTA_AirLowFareSearchRQPriceRequestInformationNegotiatedFareCode oOTA_AirLowFareSearchRQPriceRequestInformationNegotiatedFareCode =
                            new OTA_AirLowFareSearchRQPriceRequestInformationNegotiatedFareCode();

                        oOTA_AirLowFareSearchRQPriceRequestInformationNegotiatedFareCode.Code = sTarifaNegociada;

                        oOTA_AirLowFareSearchRQPriceRequestInformation.NegotiatedFareCode = oOTA_AirLowFareSearchRQPriceRequestInformationNegotiatedFareCode;
                    }

                    #region [ TPA_EXTENSIONS ]

                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions oOTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions =
                        new OTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions();

                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsFareCalc oFareCalc =
                        new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsFareCalc();

                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsFareCalcFareBasis oFareCalcFareBasis =
                        new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsFareCalcFareBasis();

                    //oFareCalc.FareBasis=new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsFareCalcFareBasis


                    /*oOTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions.FareCalc=oFareCalc; 
                    
                    oOTA_AirLowFareSearchRQPriceRequestInformation.TPA_Extensions=
                        oOTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions;*/


                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriority oPriority =
                        new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriority();

                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityPrice oPriorityPrice =
                        new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityPrice();
                    if (PriorityTime)
                        oPriorityPrice.Priority = "2";
                    else
                        oPriorityPrice.Priority = "1";

                    oPriority.Price = oPriorityPrice;

                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityVendor oPriorityVendor =
                      new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityVendor();
                    if (PriorityTime)
                        oPriorityVendor.Priority = "3";
                    else
                        oPriorityVendor.Priority = "2";

                    oPriority.Vendor = oPriorityVendor;


                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityTime oPriorityTime =
                      new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityTime();
                    if (PriorityTime)
                        oPriorityTime.Priority = "1";
                    else
                        oPriorityTime.Priority = "3";

                    oPriority.Time = oPriorityTime;

                    OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityDirectFlights oPriorityDirectFlights =
                     new OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityDirectFlights();

                    oPriorityDirectFlights.Priority = "4";

                    oPriority.DirectFlights = oPriorityDirectFlights;

                    oOTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions.Priority = oPriority;

                    oOTA_AirLowFareSearchRQPriceRequestInformation.TPA_Extensions = oOTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions;

                    #endregion

                    oOTA_AirLowFareSearchRQ.PriceRequestInformation = oOTA_AirLowFareSearchRQPriceRequestInformation;

                    #endregion

                    OTA_AirLowFareSearchService oOTA_AirLowFareSearchService = new OTA_AirLowFareSearchService();

                    oOTA_AirLowFareSearchService.MessageHeaderValue = Mensaje_;
                    oOTA_AirLowFareSearchService.SecurityValue = Seguridad_;
                    oOTA_AirLowFareSearchService.Url = objvo_Credentials.UrlWebServices;

                    oOTA_AirLowFareSearchRS = oOTA_AirLowFareSearchService.OTA_AirLowFareSearchRQ(oOTA_AirLowFareSearchRQ);
                    if (oOTA_AirLowFareSearchRS.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = oOTA_AirLowFareSearchRS.Errors.Error.ErrorCode;
                        cParametros.Info = oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = oOTA_AirLowFareSearchRS.Errors.Error.ErrorMessage;
                        cParametros.Severity = oOTA_AirLowFareSearchRS.Errors.Error.Severity;
                        cParametros.Complemento = "HostCommand: " + oOTA_AirLowFareSearchRS.TPA_Extensions.HostCommand;
                        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        cParametros.Tipo = clsTipoError.WebServices;
                        consulta.AppendLine("Credenciales: ");
                        try
                        {
                            if (objvo_Credentials != null)
                            {
                                consulta.AppendLine("User: " + objvo_Credentials.User);
                                consulta.AppendLine("Password: " + objvo_Credentials.Password);
                                consulta.AppendLine("Ipcc: " + objvo_Credentials.Ipcc);
                                consulta.AppendLine("Pcc: " + objvo_Credentials.Pcc);
                                consulta.AppendLine("QNumber: " + objvo_Credentials.QNumber);
                                consulta.AppendLine("Dominio: " + objvo_Credentials.Dominio);
                                consulta.AppendLine("Session Sabre: " + Session_.ToString());
                                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                            }
                        }
                        catch { }
                        cParametros.TargetSite = consulta.ToString();
                        try
                        {
                            cParametros.StackTrace = "Session Sabre: " + Session_.ToString();
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
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.Message = oOTA_AirLowFareSearchRS.Success;
                        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        cParametros.Complemento = "HostCommand: " + oOTA_AirLowFareSearchRS.TPA_Extensions.HostCommand;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Severity = clsSeveridad.Moderada;
                        consulta.AppendLine("Credenciales: ");
                        try
                        {
                            if (objvo_Credentials != null)
                            {
                                consulta.AppendLine("User: " + objvo_Credentials.User);
                                consulta.AppendLine("Password: " + objvo_Credentials.Password);
                                consulta.AppendLine("Ipcc: " + objvo_Credentials.Ipcc);
                                consulta.AppendLine("Pcc: " + objvo_Credentials.Pcc);
                                consulta.AppendLine("QNumber: " + objvo_Credentials.QNumber);
                                consulta.AppendLine("Dominio: " + objvo_Credentials.Dominio);
                                consulta.AppendLine("Session Sabre: " + Session_.ToString());
                                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                            }
                        }
                        catch { }
                        cParametros.TargetSite = consulta.ToString();
                        try
                        {
                            cParametros.Info = "Session Sabre: " + Session_.ToString();
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
                        vo_OTA_AirLowFareSearchLLSRQ.Ruta++;
                        try
                        {
                            vo_OTA_AirLowFareSearchLLSRQ.SVuelosARetornar = oOTA_AirLowFareSearchRS.PricedItineraries.Length.ToString();
                        }
                        catch { }
                        clsSesiones.setParametrosAirBargain
                           (
                              vo_OTA_AirLowFareSearchLLSRQ
                           );
                    }
                }
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
            return oOTA_AirLowFareSearchRS;
        }
        private string validarArunk(string sOpcion, List<VO_OriginDestinationInformation> lvo_Rutas)
        {
            int iOpcion = 0;
            try
            {
                VO_OriginDestinationInformation vo_OriginDestinationInformation = null;

                if (int.TryParse(sOpcion, out iOpcion))
                {
                    iOpcion = iOpcion - 1;
                    vo_OriginDestinationInformation = lvo_Rutas[iOpcion];

                    if (vo_OriginDestinationInformation.OTipoSegmento.Equals(TipoSegmento.ARUNK))
                    {
                        sOpcion = String.Empty;
                    }
                }
            }
            catch { }
            return sOpcion;
        }
        /// <summary>
        /// Search BFM to Default 2OOTK
        /// hceron
        /// /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRS getBusquedaMax(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
        
        ClsBargainFinderMaxRQ objMax=new ClsBargainFinderMaxRQ();
        objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();
        objMax.session=session_;
        //XmlSerializer mySerializer = new XmlSerializer(typeof(VO_OTA_AirLowFareSearchLLSRQ));
        //// To write to a file, create a StreamWriter object.
        //System.IO.StreamWriter myWriter = new System.IO.StreamWriter("D://VO_OTA_AirLowFareSearchLLSRQAntesBFM" + DateTime.Now.Hour + DateTime.Now.Minute + ".xml");
        //mySerializer.Serialize(myWriter, vo_OTA_AirLowFareSearchLLSRQ);
        //myWriter.Close();




        #region MapVO_BargainFinderMax_ADRQ
        VO_BargainFinderMax_ADRQ OnRequest = new VO_BargainFinderMax_ADRQ();
        OnRequest.Enum_IntelliSellTransaction = Enum_IntelliSellTransaction.BFM_50;
            
        WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRS ota_AirLowFareSearchRS = null;

            OnRequest.Lvo_Segments = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas;
            OnRequest.Lvo_Passengers = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros;

            //csVuelos cVuelos = new csVuelos();
            //if (vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida.Count.Equals(0))
            //    vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = cVuelos.AerolineasPreferidas();

            //if (vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida.Count.Equals(0))
            //    vo_OTA_AirLowFareSearchLLSRQ.LsExcluirAerolinea = cVuelos.ExcluirAerolineas();



           // OnRequest.Vo_VendorPref = vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida;
            #region vo_MessageHeaderBFM

            VO_MessageHeader vo_MessageHeaderBFM = new VO_MessageHeader();
            vo_MessageHeaderBFM.StrAction = "BargainFinderMaxRQ";
            vo_MessageHeaderBFM.StrValue = "Air Shopping Service";
            vo_MessageHeaderBFM.StrCPAId = objvo_Credentials.Ipcc;

            OnRequest.Vo_MessageHeader = vo_MessageHeaderBFM;
            OnRequest.Vo_VendorPref = new VO_VendorPref[]
            {
                new VO_VendorPref("UA", Enum_PreferLevelType.Unacceptable)
            };



            #endregion

            ota_AirLowFareSearchRS = (WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRS)objMax.setExecuteSWS(OnRequest);
            

        #endregion

        return ota_AirLowFareSearchRS;
        }

        
        #endregion
    }
}



