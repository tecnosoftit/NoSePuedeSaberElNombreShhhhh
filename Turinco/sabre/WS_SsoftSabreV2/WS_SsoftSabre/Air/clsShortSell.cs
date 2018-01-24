using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.ValueObjects;
using WS_SsoftSabre.ShortSell;
using System.Configuration;
using Espacio_validaciones = WS_SsoftSabre.Utilidades;
using ShortSell = WS_SsoftSabre.ShortSell;
using Ssoft.Utils;
using Ssoft.Rules.WebServices;
using Ssoft.ManejadorExcepciones;

namespace WS_SsoftSabre.Air
{
    public class clsShortSell
    {
        #region [ CONSTRUCTOR ]
        public clsShortSell()
        {
            session_ = AutenticacionSabre.GET_SabreSession();
        }
        #endregion

        #region [CAMPOS]
        string session_ = string.Empty;
        VO_Credentials objvo_Credentials;
        #endregion

        #region [ PROPIEDADES ]

        public string Session_
        {
            get { return session_; }
            set { session_ = value; }
        }

        #endregion

        #region [ METODOS]

        public ShortSellRS getItinerarioHora(VO_OTA_AirBookRQ vo_OTA_AirBookRQ)
        {
            /*METODO PRINCICPAL QUE RETORNA EL OBJETO DE RESULTADOS DE SABRE*/
            clsParametros cParametros = new clsParametros();
            ShortSellRQ oShortSellRQ = new ShortSellRQ();
            ShortSellRS oShortSellRS = new ShortSellRS();
            objvo_Credentials = clsSesiones.getCredentials();
            StringBuilder consulta = new StringBuilder();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;

            try
            {
                List<string> lsContadorOpciones = new List<string>();
                ShortSell.MessageHeader Mensaje_ = clsSabreBase.ShortSell();

                if (Mensaje_ != null)
                {
                    ShortSell.Security Seguridad_ = new ShortSell.Security();
                    Seguridad_.BinarySecurityToken = Session_;

                    #region [ POS ]
                    ShortSellRQPOS oShortSellRQPOS = new ShortSellRQPOS();
                    ShortSellRQPOSSource oShortSellRQPOSSource = new ShortSellRQPOSSource();

                    oShortSellRQPOSSource.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                    oShortSellRQPOS.Source = oShortSellRQPOSSource;
                    oShortSellRQ.POS = oShortSellRQPOS;
                    #endregion

                    #region [ VERSION ]
                    oShortSellRQ.Version = clsSabreBase.SABRE_VERSION_SHORTSELL;
                    #endregion

                    #region [ ITINERARIO }

                    int iPosRuta = vo_OTA_AirBookRQ.IRutaActual - 1;
                    VO_OrigenDestinationOption vRuta = vo_OTA_AirBookRQ.Lvo_OrigenDestinationOption[iPosRuta];
                    ShortSellRQAirItinerary AirItinerary = new ShortSellRQAirItinerary();
                    ShortSellRQAirItineraryOriginDestinationOptions AirItineraryOriginDestinationOptions = new ShortSellRQAirItineraryOriginDestinationOptions();

                    int iOriginDestinationOption = vRuta.Lvo_AirItinerary.Count;
                    ShortSellRQAirItineraryOriginDestinationOptionsFlightSegment[] AirSegmentArray_ = new ShortSellRQAirItineraryOriginDestinationOptionsFlightSegment[iOriginDestinationOption];

                    int i = 0;
                    foreach (VO_AirItinerary vItinerario in vRuta.Lvo_AirItinerary)
                    {
                        ShortSellRQAirItineraryOriginDestinationOptionsFlightSegment AirSegment_ = new ShortSellRQAirItineraryOriginDestinationOptionsFlightSegment();
                        ShortSellRQAirItineraryOriginDestinationOptionsFlightSegmentArrivalAirport AirSegment_Arrival_ = new ShortSellRQAirItineraryOriginDestinationOptionsFlightSegmentArrivalAirport();
                        ShortSellRQAirItineraryOriginDestinationOptionsFlightSegmentDepartureAirport AirSegment_Departure_ = new ShortSellRQAirItineraryOriginDestinationOptionsFlightSegmentDepartureAirport();
                        ShortSellRQAirItineraryOriginDestinationOptionsFlightSegmentMarketingAirline AirSegment_Airline_ = new ShortSellRQAirItineraryOriginDestinationOptionsFlightSegmentMarketingAirline();
                        ShortSellRQAirItineraryOriginDestinationOptionsFlightSegmentMarriageGrp AirSegment_Marriage_ = new ShortSellRQAirItineraryOriginDestinationOptionsFlightSegmentMarriageGrp();
                        ShortSellRQAirItineraryOriginDestinationOptionsFlightSegmentTPA_Extensions AirSegment_TPA_Extensions_ = new ShortSellRQAirItineraryOriginDestinationOptionsFlightSegmentTPA_Extensions();
                        ShortSellRQAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsLine AirSegment_TPA_Extensions_Line_ = new ShortSellRQAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsLine();

                        AirSegment_Arrival_.CodeContext = vItinerario.Vo_AeropuertoDestino.SContexto;
                        AirSegment_Arrival_.LocationCode = vItinerario.Vo_AeropuertoDestino.SCodigo;
                        AirSegment_.ArrivalAirport = AirSegment_Arrival_;

                        AirSegment_Departure_.CodeContext = vItinerario.Vo_AeropuertoOrigen.SContexto;
                        AirSegment_Departure_.LocationCode = vItinerario.Vo_AeropuertoOrigen.SCodigo;
                        AirSegment_.DepartureAirport = AirSegment_Departure_;

                        AirSegment_Airline_.Code = vItinerario.SMarketingAirLine;
                        AirSegment_.MarketingAirline = AirSegment_Airline_;

                        AirSegment_Marriage_.Ind = vItinerario.BAirBook;
                        AirSegment_.MarriageGrp = AirSegment_Marriage_;

                        AirSegment_.FlightNumber = vItinerario.SNroVuelo;
                        AirSegment_.DepartureDateTime = vItinerario.SFechaSalida;
                        AirSegment_.ResBookDesigCode = vItinerario.SClase;
                        AirSegment_.ActionCode = vItinerario.SActionCode;

                        AirSegment_.NumberInParty = vItinerario.SNroPassenger;
                        
                        // Opcional, vemos como se comporta y si lo necesita
                        //AirSegment_TPA_Extensions_Line_.Number = "1";
                        //AirSegment_TPA_Extensions_.Line = AirSegment_TPA_Extensions_Line_;
                        //AirSegment_.TPA_Extensions = AirSegment_TPA_Extensions_;

                        AirSegmentArray_[i] = AirSegment_;
                        i++;
                    }

                    AirItineraryOriginDestinationOptions.OriginDestinationOption = AirSegmentArray_;
                    AirItinerary.OriginDestinationOptions = AirItineraryOriginDestinationOptions;

                    oShortSellRQ.AirItinerary = AirItinerary;

                    #endregion
                    ShortSellService oShortSellService = new ShortSellService();

                    oShortSellService.MessageHeaderValue = Mensaje_;
                    oShortSellService.SecurityValue = Seguridad_;
                    oShortSellService.Url = objvo_Credentials.UrlWebServices;

                    oShortSellRS = oShortSellService.ShortSellRQ(oShortSellRQ);
                    if (oShortSellRS.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = oShortSellRS.Errors.Error.ErrorCode;
                        cParametros.Info = oShortSellRS.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = oShortSellRS.Errors.Error.ErrorMessage;
                        cParametros.Severity = oShortSellRS.Errors.Error.Severity;
                        cParametros.Complemento = "HostCommand: " + oShortSellRS.TPA_Extensions.HostCommand;
                        cParametros.Metodo = "getBusqueda";
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
                        ExceptionHandled.Publicar(cParametros);
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.TipoLog = Enum_Error.Transac;
                        cParametros.Message = oShortSellRS.Success;
                        cParametros.Metodo = "getBusqueda";
                        cParametros.Complemento = "HostCommand: " + oShortSellRS.TPA_Extensions.HostCommand;
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
                        ExceptionHandled.Publicar(cParametros);
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
                cParametros.Metodo = "getBusqueda";
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
            }
            return oShortSellRS;
        }
        public clsParametros getItinerarioHoraCommand(VO_OTA_AirBookRQ vo_OTA_AirBookRQ)
        {
            /*METODO PRINCICPAL QUE RETORNA EL OBJETO DE RESULTADOS DE SABRE*/
            clsParametros cParametros = new clsParametros();

            try
            {
                #region [ ITINERARIO }

                int iPosRuta = vo_OTA_AirBookRQ.IRutaActual - 1;
                VO_OrigenDestinationOption vRuta = vo_OTA_AirBookRQ.Lvo_OrigenDestinationOption[iPosRuta];

                int i = 0;
                foreach (VO_AirItinerary vItinerario in vRuta.Lvo_AirItinerary)
                {
                    StringBuilder sCommand = new StringBuilder();

                    sCommand.Append("0");

                    string[] sFecha = clsValidaciones.Lista(vItinerario.SFechaSalida, "T");
                    string sFechaSabre = clsValidaciones.setFechaSabre(sFecha[0]);
                    sCommand.Append(vItinerario.SMarketingAirLine);
                    sCommand.Append(vItinerario.SNroVuelo);
                    sCommand.Append(vItinerario.SClase);
                    sCommand.Append(sFechaSabre);
                    sCommand.Append(vItinerario.Vo_AeropuertoOrigen.SCodigo);
                    sCommand.Append(vItinerario.Vo_AeropuertoDestino.SCodigo);
                    sCommand.Append("NN" + vItinerario.SNroPassenger);

                    cParametros.DatoAdicArr.Add(sCommand.ToString());
                    i++;
                }
                #endregion
                cParametros.Id = 1;
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Source = Ex.Source;
                cParametros.TargetSite = Ex.TargetSite.ToString();
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Metodo = "getBusqueda";
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        #endregion
    }
}
