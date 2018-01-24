using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.ValueObjects;
using WS_SsoftSabre.OTA_AirPrice;
using System.Configuration;
using Espacio_validaciones = WS_SsoftSabre.Utilidades;
using OTA_AirPriceRQ = WS_SsoftSabre.OTA_AirPrice;
using Ssoft.Utils;
using Ssoft.Rules.WebServices;
using Ssoft.ManejadorExcepciones;


namespace WS_SsoftSabre.Air
{
    public class clsOTA_AirPrice
    {
        #region [ CONSTRUCTOR ]
        public clsOTA_AirPrice()
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
        public OTA_AirPriceRQ.OTA_AirPriceRS _Sabre_BuscarTarifa(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
            OTA_AirPriceRQ.OTA_AirPriceRS BargainResultado_ = new OTA_AirPriceRQ.OTA_AirPriceRS();
            objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();
            StringBuilder consulta = new StringBuilder();
            clsParametros cParametros = new clsParametros();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
            try
            {
                OTA_AirPriceRQ.MessageHeader Mensaje_ = clsSabreBase.__ISabre_OTA_AirPrice();

                if (Mensaje_ != null)
                {
                    OTA_AirPriceRQ.Security Seguridad_ = new OTA_AirPriceRQ.Security();
                    Seguridad_.BinarySecurityToken = Session_;

                    OTA_AirPriceRQ.OTA_AirPriceRQ Bargain_ = new OTA_AirPriceRQ.OTA_AirPriceRQ();
                    OTA_AirPriceRQ.OTA_AirPriceRQPOS BargainPos_ = new OTA_AirPriceRQ.OTA_AirPriceRQPOS();
                    OTA_AirPriceRQ.OTA_AirPriceRQPOSSource BargainSource_ = new OTA_AirPriceRQ.OTA_AirPriceRQPOSSource();

                    BargainSource_.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                    BargainPos_.Source = BargainSource_;
                    Bargain_.POS = BargainPos_;

                    //OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummary Bargain_Info_ = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummary();
                    OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummary oTravelerInfoSummary = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummary();
                    OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_Extensions oTPA_Extensions = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_Extensions();
                    OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsBargainFinder oExtensionsBargainFinder = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsBargainFinder();
                    OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsBargainFinderRebook oRebook = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsBargainFinderRebook();
                    OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsPriceRetention oPriceRetention = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsPriceRetention();

                    //oRebook.Ind = false;
                    //oPriceRetention.Default = true;
                    //oExtensionsBargainFinder.Ind = false;

                    oRebook.Ind = true;
                    oPriceRetention.Default = true;
                    oExtensionsBargainFinder.Ind = true;

                    oExtensionsBargainFinder.Rebook = oRebook;
                    oTPA_Extensions.BargainFinder = oExtensionsBargainFinder;
                    oTPA_Extensions.PriceRetention = oPriceRetention;
                    oTravelerInfoSummary.TPA_Extensions = oTPA_Extensions;
                    #region [ PRICEREQUESTINFORMATION ]

                    string sMonedaCotizar = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion;
                    string sTarifaNegociada = vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada;
                    OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryPriceRequestInformation Bargain_InfoPrice_ = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryPriceRequestInformation();
                    OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryPriceRequestInformationTPA_Extensions Bargain_InfoPriceTPA_ = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryPriceRequestInformationTPA_Extensions();
                    OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryPriceRequestInformationTPA_ExtensionsCorporate oTourCode = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryPriceRequestInformationTPA_ExtensionsCorporate();
                    OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryPriceRequestInformationTPA_ExtensionsAccount oAccountCode = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryPriceRequestInformationTPA_ExtensionsAccount();
                    if (clsValidaciones.getValidarString(sTarifaNegociada))
                    {
                        oAccountCode.Code = sTarifaNegociada;
                        //Bargain_InfoPriceTPA_.Corporate = oTourCode;
                        Bargain_InfoPriceTPA_.Account = oAccountCode;
                        Bargain_InfoPrice_.TPA_Extensions = Bargain_InfoPriceTPA_;
                        if (clsValidaciones.getValidarString(sMonedaCotizar))
                        {
                            Bargain_InfoPrice_.CurrencyCode = sMonedaCotizar;
                        }
                        oTravelerInfoSummary.PriceRequestInformation = Bargain_InfoPrice_;
                    }
                    else
                    {
                        if (clsValidaciones.getValidarString(sMonedaCotizar))
                        {
                            Bargain_InfoPrice_.CurrencyCode = sMonedaCotizar;
                        }
                        oTravelerInfoSummary.PriceRequestInformation = Bargain_InfoPrice_;
                    }

                    #endregion

                    System.Collections.Generic.List<VO_Pasajero> lvo_Pasajeros = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros;
                    // Para tomar tarifa de niños como en la busqueda normal, pero no retorna resultados reales
                    int iContPaxTotal = 0;
                    if (lvo_Pasajeros != null)
                    {
                        foreach (VO_Pasajero vo_PasajeroTotal in lvo_Pasajeros)
                        {
                            if (vo_PasajeroTotal.SCodigo.Equals("CNN"))
                            {
                                iContPaxTotal += int.Parse(vo_PasajeroTotal.SCantidad);
                            }
                            else
                            {
                                iContPaxTotal++;
                            }
                        }
                    }
                    OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsPassengerType[] oPassengerType_ = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsPassengerType[iContPaxTotal];

                    if (lvo_Pasajeros != null)
                    {
                        int iContador = 0;
                        foreach (VO_Pasajero vo_Pasajero in lvo_Pasajeros)
                        {
                            if (!(vo_Pasajero.SCantidad.Equals("0")))
                            {
                                if (vo_Pasajero.SCodigo.Equals("CNN"))
                                {
                                    foreach (VO_ClasificaPasajero vo_CatPasajero in vo_Pasajero.LvPasajeroNino)
                                    {
                                        OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsPassengerType oPassengerType = null;
                                        oPassengerType = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsPassengerType();
                                        oPassengerType.Quantity = vo_CatPasajero.SCantidad;
                                        oPassengerType.Code = vo_CatPasajero.SCodigo;
                                        oPassengerType.AlternatePassengerType = true;
                                        oPassengerType.AlternatePassengerTypeSpecified = true;

                                        oPassengerType_.SetValue(oPassengerType, iContador++);
                                    }
                                }
                                else
                                {
                                    OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsPassengerType oPassengerType = null;
                                    oPassengerType = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsPassengerType();
                                    oPassengerType.Quantity = vo_Pasajero.SCantidad;
                                    oPassengerType.Code = vo_Pasajero.SCodigo;
                                    oPassengerType.AlternatePassengerType = true;
                                    oPassengerType.AlternatePassengerTypeSpecified = true;

                                    oPassengerType_.SetValue(oPassengerType, iContador++);
                                }
                            }
                        }
                    }

                    //int iContPaxTotal = 0;
                    //if (lvo_Pasajeros != null)
                    //{
                    //    iContPaxTotal = lvo_Pasajeros.Count;
                    //}
                    //OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsPassengerType[] oPassengerType_ = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsPassengerType[iContPaxTotal];

                    //if (lvo_Pasajeros != null)
                    //{
                    //    int iContador = 0;
                    //    foreach (VO_Pasajero vo_Pasajero in lvo_Pasajeros)
                    //    {
                    //        if (!(vo_Pasajero.SCantidad.Equals("0")))
                    //        {
                    //            OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsPassengerType oPassengerType = null;
                    //            oPassengerType = new OTA_AirPriceRQ.OTA_AirPriceRQTravelerInfoSummaryTPA_ExtensionsPassengerType();
                    //            oPassengerType.Quantity = vo_Pasajero.SCantidad;
                    //            oPassengerType.Code = vo_Pasajero.SCodigo;
                    //            oPassengerType.AlternatePassengerType = true;
                    //            oPassengerType.AlternatePassengerTypeSpecified = true;

                    //            oPassengerType_.SetValue(oPassengerType, iContador++);
                    //        }
                    //    }
                    //}

                    Bargain_.TravelerInfoSummary = oTravelerInfoSummary;
                    Bargain_.TravelerInfoSummary.TPA_Extensions.PassengerType = oPassengerType_;

                    //Bargain_.TravelerInfoSummary = oTravelerInfoSummary;
                    //Bargain_.TravelerInfoSummary.TPA_Extensions.PassengerType = oPassengerType_;

                    Bargain_.Version = clsSabreBase.SABRE_VERSION_OTA_AIRPRICE;

                    OTA_AirPriceRQ.OTA_AirPriceService BargainServicio_ = new OTA_AirPriceRQ.OTA_AirPriceService();
                    BargainServicio_.MessageHeaderValue = Mensaje_;
                    BargainServicio_.SecurityValue = Seguridad_;
                    BargainServicio_.Url = objvo_Credentials.UrlWebServices;

                    BargainResultado_ = BargainServicio_.OTA_AirPriceRQ(Bargain_);
                    // Para verificar la tarifa //
                    //string sComand = "WPP" + "JCB" + "¥NCB";
                    //string sVenta = Negocios_WebServiceSabreCommand._EjecutarComando(sComand);
                    // Termina

                    if (BargainResultado_.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = BargainResultado_.Errors.Error.ErrorCode;
                        cParametros.Info = BargainResultado_.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = BargainResultado_.Errors.Error.ErrorMessage;
                        cParametros.Severity = BargainResultado_.Errors.Error.Severity;
                        cParametros.Complemento = "HostCommand: " + BargainResultado_.TPA_Extensions.HostCommand;
                        cParametros.Metodo = "_Sabre_BuscarTarifa";
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
                        cParametros.TipoLog = Enum_Error.Log;
                        ExceptionHandled.Publicar(cParametros);
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.TipoLog = Enum_Error.Transac;
                        cParametros.Message = BargainResultado_.Success;
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
                        cParametros.Metodo = "_Sabre_BuscarTarifa";
                        cParametros.Complemento = "HostCommand: " + BargainResultado_.TPA_Extensions.HostCommand;
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
                cParametros.Metodo = "_Sabre_BuscarTarifa";
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
            }
            return BargainResultado_;
        }
        #endregion
    }
}
