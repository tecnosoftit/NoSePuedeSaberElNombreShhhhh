using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.ValueObjects;
using WS_SsoftSabre.OTA_AirBook;
using System.Configuration;
using Espacio_validaciones = WS_SsoftSabre.Utilidades;
using OTA_AirAvailRQ = WS_SsoftSabre.OTA_AirBook;
using Ssoft.Utils;
using Ssoft.Rules.WebServices;
using Ssoft.ManejadorExcepciones;
using System.Xml.Serialization;

namespace WS_SsoftSabre.Air
{
    public class clsOTA_AirBook
    {
        #region [ CONSTRUCTOR ]
        public clsOTA_AirBook()
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

        //public OTA_AirBookRS getItinerarioHora(VO_OTA_AirBookRQ vo_OTA_AirBookRQ)
        //{
        //    /*METODO PRINCICPAL QUE RETORNA EL OBJETO DE RESULTADOS DE SABRE*/
        //    clsParametros cParametros = new clsParametros();
        //    OTA_AirBookRQ oOTA_AirBookRQ = new OTA_AirBookRQ();
        //    OTA_AirBookRS oOTA_AirBookRS = new OTA_AirBookRS();
        //    objvo_Credentials = clsSesiones.getCredentials();

        //    try
        //    {
        //        List<string> lsContadorOpciones = new List<string>();
        //        OTA_AirBook.MessageHeader Mensaje_ = clsSabreBase.OTA_AirBook();

        //        if (Mensaje_ != null)
        //        {
        //            OTA_AirBook.Security Seguridad_ = new OTA_AirBook.Security();
        //            Seguridad_.BinarySecurityToken = Session_;

        //            #region [ POS ]
        //            OTA_AirBookRQPOS oOTA_AirBookRQPOS = new OTA_AirBookRQPOS();
        //            OTA_AirBookRQPOSSource oOTA_AirBookRQPOSSource = new OTA_AirBookRQPOSSource();

        //            oOTA_AirBookRQPOSSource.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
        //            oOTA_AirBookRQPOS.Source = oOTA_AirBookRQPOSSource;
        //            oOTA_AirBookRQ.POS = oOTA_AirBookRQPOS;
        //            #endregion

        //            #region [ VERSION ]
        //            oOTA_AirBookRQ.Version = clsSabreBase.SABRE_VERSION_OTA_AIRBOOK;
        //            #endregion

        //            #region [ ORIGINDESTINATIONINFORMATION ]

        //            VO_OrigenDestinationOption vo_Rutas = vo_OTA_AirBookRQ.Lvo_OrigenDestinationOption[0];

        //            OTA_AirBookRQAirItinerary AirItinerary = new OTA_AirBookRQAirItinerary();

        //            if (vo_Rutas == null)
        //            {
        //                throw new Exception("No se recibieron rutas a procesar");
        //            }
        //            else
        //            {
        //                int iOriginDestinationOption = vo_Rutas.Lvo_AirItinerary.Count;
        //                int iContadorRutas = 0;

        //                //OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegment[][] bAirItineraryOriginDestinationOptionFlightSegment =
        //                //    new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegment[iOriginDestinationOption];

        //                OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegment[][] aAirItineraryOriginDestinationOptionFlightSegment =
        //                    new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegment[iOriginDestinationOption][];

        //                foreach (VO_AirItinerary vItinerario in vo_Rutas.Lvo_AirItinerary)
        //                {
        //                    OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegment oAirItineraryOriginDestinationOptionFlightSegment =
        //                        new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegment();

        //                    OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentArrivalAirport ArrivalAirport = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentArrivalAirport();
        //                    OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentDepartureAirport DepartureAirport = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentDepartureAirport();
        //                    OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentEquipment[] Equipments = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentEquipment[1];
        //                    OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentEquipment Equipment = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentEquipment();
        //                    OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentMarketingAirline MarketingAirline = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentMarketingAirline();
        //                    OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentMarriageGrp MarriageGrp = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentMarriageGrp();
        //                    OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentOperatingAirline OperatingAirline = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentOperatingAirline();

        //                    DepartureAirport.CodeContext = vItinerario.Vo_AeropuertoOrigen.SContexto;
        //                    DepartureAirport.LocationCode = vItinerario.Vo_AeropuertoOrigen.SCodigo;

        //                    ArrivalAirport.CodeContext = vItinerario.Vo_AeropuertoDestino.SContexto;
        //                    ArrivalAirport.LocationCode = vItinerario.Vo_AeropuertoDestino.SCodigo;

        //                    Equipment.AirEquipType = vItinerario.SAirEquip;
        //                    Equipments[0] = Equipment;
        //                    MarketingAirline.Code = vItinerario.SMarketingAirLine;
        //                    MarriageGrp.Ind = vItinerario.BAirBook;
        //                    OperatingAirline.Code = vItinerario.SOperatingAirLine;

        //                    oAirItineraryOriginDestinationOptionFlightSegment.ActionCode = vItinerario.SActionCode;
        //                    oAirItineraryOriginDestinationOptionFlightSegment.ArrivalDateTime = vItinerario.SFechaLlegada;
        //                    oAirItineraryOriginDestinationOptionFlightSegment.DepartureDateTime = vItinerario.SFechaSalida;
        //                    oAirItineraryOriginDestinationOptionFlightSegment.FlightNumber = vItinerario.SNroVuelo;
        //                    oAirItineraryOriginDestinationOptionFlightSegment.NumberInParty = vItinerario.SNroPassenger;
        //                    oAirItineraryOriginDestinationOptionFlightSegment.ResBookDesigCode = vItinerario.SClase;

        //                    oAirItineraryOriginDestinationOptionFlightSegment.ArrivalAirport = ArrivalAirport;
        //                    oAirItineraryOriginDestinationOptionFlightSegment.DepartureAirport = DepartureAirport;
        //                    oAirItineraryOriginDestinationOptionFlightSegment.Equipment = Equipments;
        //                    oAirItineraryOriginDestinationOptionFlightSegment.MarketingAirline = MarketingAirline;
        //                    oAirItineraryOriginDestinationOptionFlightSegment.MarriageGrp = MarriageGrp;
        //                    oAirItineraryOriginDestinationOptionFlightSegment.OperatingAirline = OperatingAirline;

        //                    aAirItineraryOriginDestinationOptionFlightSegment[iContadorRutas][0] = oAirItineraryOriginDestinationOptionFlightSegment;
        //                    iContadorRutas++;
        //                }
        //                //bAirItineraryOriginDestinationOptionFlightSegment[0] = aAirItineraryOriginDestinationOptionFlightSegment;

        //                AirItinerary.OriginDestinationOptions = aAirItineraryOriginDestinationOptionFlightSegment;
        //            }
        //            oOTA_AirBookRQ.AirItinerary = AirItinerary;

        //            #endregion
        //            OTA_AirBookService oOTA_AirBookService = new OTA_AirBookService();

        //            oOTA_AirBookService.MessageHeaderValue = Mensaje_;
        //            oOTA_AirBookService.SecurityValue = Seguridad_;

        //            oOTA_AirBookRS = oOTA_AirBookService.OTA_AirBookRQ(oOTA_AirBookRQ);
        //            if (oOTA_AirBookRS.Errors != null)
        //            {
        //                cParametros.Id = 0;
        //                cParametros.Code = oOTA_AirBookRS.Errors[0].Error.ErrorCode;
        //                cParametros.Info = oOTA_AirBookRS.Errors[0].Error.ErrorInfo.Message;
        //                cParametros.Message = oOTA_AirBookRS.Errors[0].Error.ErrorMessage;
        //                cParametros.Severity = oOTA_AirBookRS.Errors[0].Error.Severity;
        //                cParametros.Complemento = "HostCommand: " + oOTA_AirBookRS.TPA_Extensions.HostCommand;
        //                cParametros.Metodo = "getBusqueda";
        //                cParametros.Tipo = clsTipoError.WebServices;
        //                ExceptionHandled.Publicar(cParametros);
        //            }
        //            else
        //            {
        //                cParametros.Id = 1;
        //                cParametros.Message = oOTA_AirBookRS.Success;
        //                cParametros.Metodo = "getBusqueda";
        //                cParametros.Complemento = "HostCommand: " + oOTA_AirBookRS.TPA_Extensions.HostCommand;
        //                cParametros.Tipo = clsTipoError.WebServices;
        //                cParametros.Severity = clsSeveridad.Moderada;
        //                ExceptionHandled.Publicar(cParametros);
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        cParametros.Id = 0;
        //        cParametros.Message = Ex.Message;
        //        cParametros.StackTrace = Ex.StackTrace;
        //        cParametros.Source = Ex.Source;
        //        cParametros.TargetSite = Ex.TargetSite.ToString();
        //        cParametros.Severity = clsSeveridad.Alta;
        //        cParametros.Metodo = "getBusqueda";
        //        cParametros.Tipo = clsTipoError.WebServices;
        //        ExceptionHandled.Publicar(cParametros);
        //    }
        //    return oOTA_AirBookRS;
        //}
        public OTA_AirBookRS getItinerarioHora(VO_OTA_AirBookRQ vo_OTA_AirBookRQ)
        {
            /*METODO PRINCICPAL QUE RETORNA EL OBJETO DE RESULTADOS DE SABRE*/
            clsParametros cParametros = new clsParametros();
            OTA_AirBookRQ oOTA_AirBookRQ = new OTA_AirBookRQ();
            OTA_AirBookRS oOTA_AirBookRS = new OTA_AirBookRS();
            StringBuilder consulta = new StringBuilder();
            objvo_Credentials = clsSesiones.getCredentials();

            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;

            try
            {
                List<string> lsContadorOpciones = new List<string>();
                OTA_AirBook.MessageHeader Mensaje_ = clsSabreBase.OTA_AirBook();

                if (Mensaje_ != null)
                {
                    OTA_AirBook.Security Seguridad_ = new OTA_AirBook.Security();
                    Seguridad_.BinarySecurityToken = Session_;

                    #region [ POS ]
                    OTA_AirBookRQPOS oOTA_AirBookRQPOS = new OTA_AirBookRQPOS();
                    OTA_AirBookRQPOSSource oOTA_AirBookRQPOSSource = new OTA_AirBookRQPOSSource();
                    oOTA_AirBookRQPOSSource.PseudoCityCode = objvo_Credentials.Ipcc;
                    // oOTA_AirBookRQPOSSource.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                    oOTA_AirBookRQPOS.Source = oOTA_AirBookRQPOSSource;
                    oOTA_AirBookRQ.POS = oOTA_AirBookRQPOS;
                    #endregion

                    #region [ VERSION ]
                    oOTA_AirBookRQ.Version = clsSabreBase.SABRE_VERSION_OTA_AIRBOOK;
                    #endregion

                    #region [ ITINERARIO }

                    List<VO_OrigenDestinationOption> vo_Rutas = vo_OTA_AirBookRQ.Lvo_OrigenDestinationOption;

                    OTA_AirBookRQAirItinerary Book_Air_ = new OTA_AirBookRQAirItinerary();
                    List<OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegment[]> Book_AirSegmentList_ = new List<OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegment[]>();

                    foreach (VO_OrigenDestinationOption vRuta in vo_Rutas)
                    {
                        int iOriginDestinationOption = vRuta.Lvo_AirItinerary.Count;
                        OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegment[] Book_AirSegmentArray_ = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegment[iOriginDestinationOption];

                        int i = 0;
                        foreach (VO_AirItinerary vItinerario in vRuta.Lvo_AirItinerary)
                        {
                            OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegment Book_AirSegment_ = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegment();
                            OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentArrivalAirport Book_AirSegment_Arrival_ = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentArrivalAirport();
                            OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentDepartureAirport Book_AirSegment_Departure_ = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentDepartureAirport();
                            OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentMarketingAirline Book_AirSegment_Airline_ = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentMarketingAirline();
                            OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentMarriageGrp Book_AirSegment_Marriage_ = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentMarriageGrp();
                            OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentOperatingAirline Book_AirSegment_Operating_ = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentOperatingAirline();
                            OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentEquipment[] Book_AirSegment_Equipments_ = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentEquipment[1];
                            OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentEquipment Book_AirSegment_Equipment_ = new OTA_AirBookRQAirItineraryOriginDestinationOptionFlightSegmentEquipment();

                            // Book_AirSegment_Arrival_.CodeContext = vItinerario.Vo_AeropuertoDestino.SContexto;
                            Book_AirSegment_Arrival_.LocationCode = vItinerario.Vo_AeropuertoDestino.SCodigo;
                            Book_AirSegment_.ArrivalAirport = Book_AirSegment_Arrival_;

                            //Book_AirSegment_Departure_.CodeContext = vItinerario.Vo_AeropuertoOrigen.SContexto;
                            Book_AirSegment_Departure_.LocationCode = vItinerario.Vo_AeropuertoOrigen.SCodigo;
                            Book_AirSegment_.DepartureAirport = Book_AirSegment_Departure_;

                            Book_AirSegment_Airline_.Code = vItinerario.SMarketingAirLine;
                            Book_AirSegment_.MarketingAirline = Book_AirSegment_Airline_;

                            Book_AirSegment_Marriage_.Ind = vItinerario.BAirBook;
                            Book_AirSegment_.MarriageGrp = Book_AirSegment_Marriage_;

                            Book_AirSegment_Operating_.Code = vItinerario.SOperatingAirLine;
                            Book_AirSegment_.OperatingAirline = Book_AirSegment_Operating_;

                            Book_AirSegment_Equipment_.AirEquipType = vItinerario.SAirEquip;
                            Book_AirSegment_Equipments_[0] = Book_AirSegment_Equipment_;
                            Book_AirSegment_.Equipment = Book_AirSegment_Equipments_;

                            Book_AirSegment_.FlightNumber = vItinerario.SNroVuelo;
                            Book_AirSegment_.ArrivalDateTime = vItinerario.SFechaLlegada;
                            Book_AirSegment_.DepartureDateTime = vItinerario.SFechaSalida;
                            Book_AirSegment_.ResBookDesigCode = vItinerario.SClase;
                            Book_AirSegment_.ActionCode = vItinerario.SActionCode;
                            Book_AirSegment_Marriage_.Ind = true;
                            //Book_AirSegment_.RPH = Detalle_.ItinerarioDetalle__Id_.ToString();
                            Book_AirSegment_.NumberInParty = vItinerario.SNroPassenger;

                            Book_AirSegmentArray_[i] = Book_AirSegment_;
                            i++;
                        }
                        //Book_AirSegmentList_.Clear();
                        Book_AirSegmentList_.Add(Book_AirSegmentArray_);
                    }
                    Book_Air_.OriginDestinationOptions = Book_AirSegmentList_.ToArray();
                    oOTA_AirBookRQ.AirItinerary = Book_Air_;

                    #endregion
                    OTA_AirBookService oOTA_AirBookService = new OTA_AirBookService();

                    oOTA_AirBookService.MessageHeaderValue = Mensaje_;
                    oOTA_AirBookService.SecurityValue = Seguridad_;
                    oOTA_AirBookService.Url = objvo_Credentials.UrlWebServices;

                    oOTA_AirBookRS = oOTA_AirBookService.OTA_AirBookRQ(oOTA_AirBookRQ);

                    //XmlSerializer mySerializer = new XmlSerializer(typeof(OTA_AirBookRQ));
                    //// To write to a file, create a StreamWriter object.
                    //System.IO.StreamWriter myWriter = new System.IO.StreamWriter("d://OTA_AirBookRQNewMULTI" + DateTime.Now.Hour + DateTime.Now.Minute + ".xml");
                    //mySerializer.Serialize(myWriter, oOTA_AirBookRQ);
                    //myWriter.Close();


                    //mySerializer = new XmlSerializer(typeof(OTA_AirBookRS));
                    //// To write to a file, create a StreamWriter object.
                    //myWriter = new System.IO.StreamWriter("d://OTA_AirBookRQNewMULTI" + DateTime.Now.Hour + DateTime.Now.Minute + ".xml");
                    //mySerializer.Serialize(myWriter, oOTA_AirBookRS);
                    //myWriter.Close();

                    if (oOTA_AirBookRS.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = oOTA_AirBookRS.Errors[0].Error.ErrorCode;
                        cParametros.Info = oOTA_AirBookRS.Errors[0].Error.ErrorInfo.Message;
                        cParametros.Message = oOTA_AirBookRS.Errors[0].Error.ErrorMessage;
                        cParametros.Severity = oOTA_AirBookRS.Errors[0].Error.Severity;
                        cParametros.Complemento = "HostCommand: " + oOTA_AirBookRS.TPA_Extensions.HostCommand;
                        cParametros.Metodo = "getBusqueda";
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
                        cParametros.Tipo = clsTipoError.WebServices;
                        ExceptionHandled.Publicar(cParametros);
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.Message = oOTA_AirBookRS.Success;
                        cParametros.Metodo = "getBusqueda";
                        cParametros.Complemento = "HostCommand: " + oOTA_AirBookRS.TPA_Extensions.HostCommand;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Severity = clsSeveridad.Moderada;
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
            return oOTA_AirBookRS;
        }
        private int iCountRutas(VO_OTA_AirBookRQ vo_OTA_AirBookRQ)
        {
            int iCountRuta = 0;
            int iCount = vo_OTA_AirBookRQ.Lvo_OrigenDestinationOption.Count;
            for (int i = 0; i < iCount; i++)
            {
                int iCountItinerario = vo_OTA_AirBookRQ.Lvo_OrigenDestinationOption[i].Lvo_AirItinerary.Count;
                for (int j = 0; j < iCountItinerario; j++)
                {
                    iCountRuta++;
                }
            }
            return iCountRuta;
        }

        #endregion
    }
}
