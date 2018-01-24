using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.ValueObjects;
using WS_SsoftSabre.OTA_TravelItineraryRead;
using WS_SsoftSabre.OTA_Cancel;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;

namespace WS_SsoftSabre.Air
{
    public class clsOTA_TravelItineraryRead
    {
        #region [ CONSTRUCTOR ]
        public clsOTA_TravelItineraryRead()
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

        #region [ METODOS ]

        public OTA_TravelItineraryRS _Sabre_LeerInformacionPNR(string Record_)
        {
            OTA_TravelItineraryRS TravelResultado_ = new OTA_TravelItineraryRS();
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            StringBuilder consulta = new StringBuilder();

            try
            {
                objvo_Credentials = clsSesiones.getCredentials();
                OTA_TravelItineraryRead.MessageHeader Mensaje_ = clsSabreBase.OTA_TravelItineraryRead();

                if (Mensaje_ != null)
                {
                    OTA_TravelItineraryRead.Security Seguridad_ = new OTA_TravelItineraryRead.Security();
                    Seguridad_.BinarySecurityToken = Session_;

                    OTA_TravelItineraryReadRQ Travel_ = new OTA_TravelItineraryReadRQ();
                    OTA_TravelItineraryReadRQPOS TravelPos_ = new OTA_TravelItineraryReadRQPOS();
                    OTA_TravelItineraryReadRQPOSSource TravelSource_ = new OTA_TravelItineraryReadRQPOSSource();

                    TravelSource_.PseudoCityCode = objvo_Credentials.Pcc;
                    TravelPos_.Source = TravelSource_;
                    Travel_.POS = TravelPos_;

                    OTA_TravelItineraryReadRQUniqueID Travel_UniqueID_ = new OTA_TravelItineraryReadRQUniqueID();
                    OTA_TravelItineraryReadRQUniqueIDTPA_ExtensionsTransaction[] aTPA_ExtensionsTransaction = new OTA_TravelItineraryReadRQUniqueIDTPA_ExtensionsTransaction[1];
                    OTA_TravelItineraryReadRQUniqueIDTPA_ExtensionsTransaction oTPA_ExtensionsTransaction = new OTA_TravelItineraryReadRQUniqueIDTPA_ExtensionsTransaction();
                    OTA_TravelItineraryReadRQUniqueIDTPA_Extensions oIDTPA_Extensions = new OTA_TravelItineraryReadRQUniqueIDTPA_Extensions();

                    Travel_UniqueID_.ID = Record_;
                    oTPA_ExtensionsTransaction.Code = "AIT";
                    aTPA_ExtensionsTransaction.SetValue(oTPA_ExtensionsTransaction, 0);
                    oIDTPA_Extensions.Transaction = aTPA_ExtensionsTransaction;
                    //Travel_UniqueID_.TPA_Extensions = oIDTPA_Extensions;                    
                    Travel_.UniqueID = Travel_UniqueID_;

                    OTA_TravelItineraryReadRQUniqueIDTPA_Extensions oUniqueIDTPA_Extensions = new OTA_TravelItineraryReadRQUniqueIDTPA_Extensions();
                    OTA_TravelItineraryReadRQUniqueIDTPA_ExtensionsRedisplay oUniqueIDTPA_ExtensionsRedisplay = new OTA_TravelItineraryReadRQUniqueIDTPA_ExtensionsRedisplay();

                    oUniqueIDTPA_ExtensionsRedisplay.Ind = false;
                    oUniqueIDTPA_Extensions.Redisplay = oUniqueIDTPA_ExtensionsRedisplay;
                    Travel_UniqueID_.TPA_Extensions = oUniqueIDTPA_Extensions;

                    OTA_TravelItineraryReadRQTPA_Extensions oTPA_Extensions = new OTA_TravelItineraryReadRQTPA_Extensions();
                    OTA_TravelItineraryReadRQTPA_ExtensionsMessagingDetails oMessagingDetails = new OTA_TravelItineraryReadRQTPA_ExtensionsMessagingDetails();
                    OTA_TravelItineraryReadRQTPA_ExtensionsMessagingDetailsMDRSubset oMDRSubset = new OTA_TravelItineraryReadRQTPA_ExtensionsMessagingDetailsMDRSubset();

                    oMDRSubset.Code = "PN12";
                    oMessagingDetails.MDRSubset = oMDRSubset;
                    oTPA_Extensions.MessagingDetails = oMessagingDetails;
                    Travel_.TPA_Extensions = oTPA_Extensions;

                    Travel_.Version = clsSabreBase.SABRE_VERSION_TRAVELITINERARYREADLLS;

                    OTA_TravelItineraryService TravelServicio_ = new OTA_TravelItineraryService();

                    TravelServicio_.MessageHeaderValue = Mensaje_;
                    TravelServicio_.SecurityValue = Seguridad_;
                    TravelServicio_.Url = objvo_Credentials.UrlWebServices;

                    TravelResultado_ = TravelServicio_.OTA_TravelItineraryReadRQ(Travel_);

                    if (TravelResultado_.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = TravelResultado_.Errors.Error.ErrorCode;
                        cParametros.Info = TravelResultado_.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = TravelResultado_.Errors.Error.ErrorMessage;
                        cParametros.Severity = TravelResultado_.Errors.Error.Severity;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        cParametros.Complemento = "Recuperacion de la reserva " + Record_;
                        cParametros.ViewMessage.Add("Error al intentar recuperar la reserva");
                        cParametros.Sugerencia.Add("Por favor intente de nuevo");
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
                                consulta.AppendLine("Reserva: " + Record_);
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
                        cResultados.Error = cParametros;
                        ExceptionHandled.Publicar(cParametros);
                        try
                        {
                            Negocios_WebServiceSabreCommand.setEmailError(cParametros, "Error al Recuperar la reserva " + Record_);
                        }
                        catch { }
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.TipoLog = Enum_Error.Transac;
                        cParametros.Message = TravelResultado_.Success;
                        cParametros.Metodo = "Informacion PNR";
                        cParametros.Complemento = "HostCommand: " + TravelResultado_.TPA_Extensions.HostCommand;
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
                                consulta.AppendLine("Reserva: " + Record_);
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
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Complemento = "Recuperacion de la reserva " + Record_;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("Error al intentar recuperar la reserva");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
                try
                {
                    Negocios_WebServiceSabreCommand.setEmailError(cParametros, "Error al Recuperar la reserva " + Record_);
                }
                catch { }
            }
            return TravelResultado_;
        }
        public OTA_CancelRS _Sabre_CancelRecord()
        {
            OTA_CancelRS TravelResultado_ = new OTA_CancelRS();
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            StringBuilder consulta = new StringBuilder();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;

            try
            {
                objvo_Credentials = clsSesiones.getCredentials();
                OTA_Cancel.MessageHeader Mensaje_ = clsSabreBase.OTA_Cancel();

                if (Mensaje_ != null)
                {
                    OTA_Cancel.Security Seguridad_ = new OTA_Cancel.Security();
                    Seguridad_.BinarySecurityToken = Session_;

                    OTA_CancelRQ Travel_ = new OTA_CancelRQ();
                    OTA_CancelRQPOS TravelPos_ = new OTA_CancelRQPOS();
                    OTA_CancelRQPOSSource TravelSource_ = new OTA_CancelRQPOSSource();

                    TravelSource_.PseudoCityCode = objvo_Credentials.Pcc;
                    TravelPos_.Source = TravelSource_;
                    Travel_.POS = TravelPos_;

                    OTA_CancelRQTPA_Extensions oTPA_Extensions = new OTA_CancelRQTPA_Extensions();
                    OTA_CancelRQTPA_ExtensionsMessagingDetails oMessagingDetails = new OTA_CancelRQTPA_ExtensionsMessagingDetails();
                    OTA_CancelRQTPA_ExtensionsMessagingDetailsMDRSubset oMDRSubset = new OTA_CancelRQTPA_ExtensionsMessagingDetailsMDRSubset();

                    //oMDRSubset.Code = "PN12";
                    //oMessagingDetails.MDRSubset = oMDRSubset;
                    //oTPA_Extensions.MessagingDetails = oMessagingDetails;

                    OTA_CancelRQTPA_ExtensionsSegmentCancel oSegmentCancel = new OTA_CancelRQTPA_ExtensionsSegmentCancel();
                    OTA_CancelRQTPA_ExtensionsSegmentCancelSegment oSegmentCancelSegment = new OTA_CancelRQTPA_ExtensionsSegmentCancelSegment();
                    OTA_CancelRQTPA_ExtensionsSegmentCancelSegment[] oSegmentCancelSegments = new OTA_CancelRQTPA_ExtensionsSegmentCancelSegment[1];

                    //oSegmentCancelSegment.Number = "1";
                    //oSegmentCancelSegment.EndNumber = iSegmentos.ToString();
                    //oSegmentCancelSegments[0] = oSegmentCancelSegment;
                    //oSegmentCancel.Segment = oSegmentCancelSegments;

                    // "Type" refers to the segment type that the user wants to cancel.  Acceptable values for "Type" are "Air", "Car", "Hotel", "Other", or "Entire".  These formats look like this: "XIA", "XIC", "XIH", "XIO", or "XI".
                    oSegmentCancel.Type = "Entire";
                    oTPA_Extensions.SegmentCancel = oSegmentCancel;
                    Travel_.TPA_Extensions = oTPA_Extensions;

                    Travel_.Version = clsSabreBase.SABRE_VERSION_CANCEL;

                    OTA_CancelService TravelServicio_ = new OTA_CancelService();

                    TravelServicio_.MessageHeaderValue = Mensaje_;
                    TravelServicio_.SecurityValue = Seguridad_;
                    TravelServicio_.Url = objvo_Credentials.UrlWebServices;

                    TravelResultado_ = TravelServicio_.OTA_CancelRQ(Travel_);

                    if (TravelResultado_.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = TravelResultado_.Errors.Error.ErrorCode;
                        cParametros.Message = TravelResultado_.Errors.Error.ErrorMessage;
                        cParametros.Severity = TravelResultado_.Errors.Error.Severity;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        cParametros.Complemento = "Cancelacion de la reserva";
                        cParametros.ViewMessage.Add("Error al intentar cancelar la reserva");
                        cParametros.Sugerencia.Add("Por favor intente de nuevo");
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
                        cResultados.Error = cParametros;
                        ExceptionHandled.Publicar(cParametros);
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.TipoLog = Enum_Error.Transac;
                        cParametros.Message = TravelResultado_.Success;
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
                        cParametros.Metodo = "CancelRecord";
                        cParametros.Complemento = "HostCommand: " + TravelResultado_.TPA_Extensions.HostCommand;
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
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Complemento = "Cancelacion de la reserva";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("Error al intentar cancelar la reserva");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return TravelResultado_;
        }
        //public PassengerDetailsRQ.PassengerDetailsRS _Sabre_PassengerDetailsRQ(string Record_)
        //{
        //    PassengerDetailsRQ.PassengerDetailsRS TravelRespuesta_ = new PassengerDetailsRQ.PassengerDetailsRS();

        //    try
        //    {
        //        PassengerDetailsRQ.MessageHeader Mensaje_ = WebService_Base.__ISabre_PassengerDetailsRQ();

        //        if (Mensaje_ != null)
        //        {
        //            PassengerDetailsRQ.Security Seguridad_ = new PassengerDetailsRQ.Security();
        //            Seguridad_.BinarySecurityToken = Session_;

        //            PassengerDetailsRQ.PassengerDetailsRQ Travel_ = new PassengerDetailsRQ.PassengerDetailsRQ();
        //            PassengerDetailsRQ.PassengerDetailsRQPOS TravelPos_ = new PassengerDetailsRQ.PassengerDetailsRQPOS();
        //            PassengerDetailsRQ.PassengerDetailsRQPOSSource TravelSource_ = new PassengerDetailsRQ.PassengerDetailsRQPOSSource();

        //            TravelSource_.PseudoCityCode = ConfigurationManager.AppSettings["Sabre_Ipcc"];
        //            TravelPos_.Source = TravelSource_;
        //            Travel_.POS = TravelPos_;

        //            PassengerDetailsRQ.PassengerDetailsRQProfileRef Travel_UniqueID_ = new PassengerDetailsRQ.PassengerDetailsRQProfileRef();
        //            PassengerDetailsRQ.PassengerDetailsRQProfileRefUniqueID oPassengerDetailsRQProfileRefUniqueID = new PassengerDetailsRQ.PassengerDetailsRQProfileRefUniqueID();
        //            oPassengerDetailsRQProfileRefUniqueID.ID = Record_;


        //            Travel_UniqueID_.UniqueID = oPassengerDetailsRQProfileRefUniqueID;
        //            Travel_.ProfileRef = Travel_UniqueID_;

        //            Travel_.Version = WebService_Base.SABRE_VERSION_TRAVELITINERARYREADLLS;

        //            PassengerDetailsRQ.PassengerDetailsService TravelServicio_ = new PassengerDetailsRQ.PassengerDetailsService();

        //            TravelServicio_.MessageHeaderValue = Mensaje_;
        //            TravelServicio_.SecurityValue = Seguridad_;

        //            TravelRespuesta_ = TravelServicio_.PassengerDetailsRQ(Travel_);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        ControlErrores_Archivos._ArchivoError(Ex, "Modulo WebService_TravelItinerary ['WS' LEER INFORMACION PNR( Record_ )]");
        //    }

        //    return TravelRespuesta_;
        //}

        #endregion

        #region [ DESTRUCTOR ]

        ~clsOTA_TravelItineraryRead() { }

        #endregion
    }
}
