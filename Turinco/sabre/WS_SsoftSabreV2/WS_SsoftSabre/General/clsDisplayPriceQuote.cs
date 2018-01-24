using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.ValueObjects;
using WS_SsoftSabre.DisplayPriceQuote;
using System.Configuration;
using Ssoft.Utils;
using Ssoft.Rules.WebServices;
using Ssoft.ManejadorExcepciones;
using WS_SsoftSabre.Air;

namespace WS_SsoftSabre.General
{
    public class clsDisplayPriceQuote
    {
        #region [ CONSTRUCTOR ]
        public clsDisplayPriceQuote()
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
        public DisplayPriceQuoteRS getTarifa()
        {
            /*METODO PRINCICPAL QUE RETORNA EL OBJETO DE RESULTADOS DE SABRE*/
            clsParametros cParametros = new clsParametros();
            csVuelos cVuelos = new csVuelos();

            DisplayPriceQuoteRQ oDisplayPriceQuoteRQ = new DisplayPriceQuoteRQ();
            DisplayPriceQuoteRS oDisplayPriceQuoteRS = new DisplayPriceQuoteRS();
            objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();

            try
            {
                DisplayPriceQuote.MessageHeader Mensaje_ = clsSabreBase.DisplayPriceQuote();

                if (Mensaje_ != null)
                {
                    DisplayPriceQuote.Security Seguridad_ = new DisplayPriceQuote.Security();
                    Seguridad_.BinarySecurityToken = Session_;

                    #region [ POS ]
                    DisplayPriceQuoteRQPOS oDisplayPriceQuoteRQPOS = new DisplayPriceQuoteRQPOS();
                    DisplayPriceQuoteRQPOSSource oDisplayPriceQuoteRQPOSSource = new DisplayPriceQuoteRQPOSSource();

                    oDisplayPriceQuoteRQPOSSource.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                    oDisplayPriceQuoteRQPOS.Source = oDisplayPriceQuoteRQPOSSource;
                    oDisplayPriceQuoteRQ.POS = oDisplayPriceQuoteRQPOS;
                    #endregion

                    #region [ VERSION ]
                    oDisplayPriceQuoteRQ.Version = clsSabreBase.SABRE_VERSION_DISPLAYPRICE;
                    #endregion

                    #region [ RESERVA ]
                    DisplayPriceQuoteRQAirItineraryPricingInfo AirItineraryPricingInfo = new DisplayPriceQuoteRQAirItineraryPricingInfo();
                    DisplayPriceQuoteRQAirItineraryPricingInfoRecord AirItineraryPricingInfoRecord = new DisplayPriceQuoteRQAirItineraryPricingInfoRecord();
 

                    #endregion

                    DisplayPriceQuoteService oDisplayPriceQuoteService = new DisplayPriceQuoteService();

                    oDisplayPriceQuoteService.MessageHeaderValue = Mensaje_;
                    oDisplayPriceQuoteService.SecurityValue = Seguridad_;

                    oDisplayPriceQuoteRS = oDisplayPriceQuoteService.DisplayPriceQuoteRQ(oDisplayPriceQuoteRQ);
                    if (oDisplayPriceQuoteRS.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = oDisplayPriceQuoteRS.Errors.Error.ErrorCode;
                        cParametros.Info = oDisplayPriceQuoteRS.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = oDisplayPriceQuoteRS.Errors.Error.ErrorMessage;
                        cParametros.Severity = oDisplayPriceQuoteRS.Errors.Error.Severity;
                        cParametros.Complemento = "HostCommand: " + oDisplayPriceQuoteRS.TPA_Extensions.HostCommand;
                        cParametros.Metodo = "getBusqueda";
                        cParametros.Tipo = clsTipoError.WebServices;
                        ExceptionHandled.Publicar(cParametros);
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.TipoLog = Enum_Error.Transac;
                        cParametros.Message = oDisplayPriceQuoteRS.Success;
                        cParametros.Metodo = "_Remark_Observaciones";
                        cParametros.Complemento = "HostCommand: " + oDisplayPriceQuoteRS.TPA_Extensions.HostCommand;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Severity = clsSeveridad.Moderada;
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
                        cParametros.TipoLog = Enum_Error.Log;
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
            return oDisplayPriceQuoteRS;
        }
        #endregion
    }
}
