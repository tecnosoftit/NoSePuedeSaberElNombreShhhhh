using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.ValueObjects;
using WS_SsoftSabre.DesignatePrinterRQ;
using WS_SsoftSabre.AirTicketRQ;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;

namespace WS_SsoftSabre.Air
{
    public class clsDesignatePrinter
    {
        #region [ CONSTRUCTOR ]
        public clsDesignatePrinter()
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
        public DesignatePrinterRS getDesignatePrinter(VO_PrintersTicketsRQ vo_PrintersTicketsRQ)
        {
            /*METODO PRINCICPAL QUE RETORNA EL OBJETO DE RESULTADOS DE SABRE*/
            StringBuilder consulta = new StringBuilder();
            clsParametros cParametros = new clsParametros();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
            DesignatePrinterRQ.DesignatePrinterRQ oDesignatePrinterRQ = new WS_SsoftSabre.DesignatePrinterRQ.DesignatePrinterRQ();
            DesignatePrinterRS oDesignatePrinterRS = new DesignatePrinterRS();

            objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();
            try
            {
                List<string> lsContadorOpciones = new List<string>();
                DesignatePrinterRQ.MessageHeader Mensaje_ = clsSabreBase.DesignatePrinter();

                if (Mensaje_ != null)
                {
                    DesignatePrinterRQ.Security Seguridad_ = new DesignatePrinterRQ.Security();
                    Seguridad_.BinarySecurityToken = Session_;

                    #region [ POS ]
                    DesignatePrinterRQPOS oDesignatePrinterRQPOS = new DesignatePrinterRQPOS();
                    DesignatePrinterRQPOSSource oDesignatePrinterRQPOSSource = new DesignatePrinterRQPOSSource();

                    oDesignatePrinterRQPOSSource.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                    oDesignatePrinterRQPOS.Source = oDesignatePrinterRQPOSSource;
                    oDesignatePrinterRQ.POS = oDesignatePrinterRQPOS;
                    #endregion

                    #region [ VERSION ]
                    oDesignatePrinterRQ.Version = clsSabreBase.SABRE_VERSION_DESIGNATEPRINTER;
                    #endregion

                    #region [ DESIGNATEPRINTERS ]

                    DesignatePrinterRQPrinters oDesignatePrinterRQPrinters = new DesignatePrinterRQPrinters();
                    DesignatePrinterRQPrintersHardcopy oDesignatePrinterRQPrintersHardcopy = new DesignatePrinterRQPrintersHardcopy();
                    DesignatePrinterRQPrintersInvoiceItinerary oDesignatePrinterRQPrintersInvoiceItinerary = new DesignatePrinterRQPrintersInvoiceItinerary();
                    DesignatePrinterRQPrintersTicket oDesignatePrinterRQPrintersTicket = new DesignatePrinterRQPrintersTicket();
                    DesignatePrinterRQPrintersBoardingPass oDesignatePrinterRQPrintersBoardingPass = new DesignatePrinterRQPrintersBoardingPass();

                    oDesignatePrinterRQPrintersHardcopy.LineAddress = vo_PrintersTicketsRQ.PrtItinerario;
                    oDesignatePrinterRQPrintersInvoiceItinerary.LineAddress = vo_PrintersTicketsRQ.PrtInvoice;
                    oDesignatePrinterRQPrintersTicket.LineAddress = vo_PrintersTicketsRQ.PrtTicket;
                    oDesignatePrinterRQPrintersTicket.CountryCode = vo_PrintersTicketsRQ.Countrycode;

                    oDesignatePrinterRQPrinters.Hardcopy = oDesignatePrinterRQPrintersHardcopy;
                    oDesignatePrinterRQPrinters.InvoiceItinerary = oDesignatePrinterRQPrintersInvoiceItinerary;
                    oDesignatePrinterRQPrinters.Ticket = oDesignatePrinterRQPrintersTicket;

                    oDesignatePrinterRQ.Printers = oDesignatePrinterRQPrinters;

                    #endregion

                    DesignatePrinterService oDesignatePrinterService = new DesignatePrinterService();

                    oDesignatePrinterService.MessageHeaderValue = Mensaje_;
                    oDesignatePrinterService.SecurityValue = Seguridad_;
                    oDesignatePrinterService.Url = objvo_Credentials.UrlWebServices;

                    oDesignatePrinterRS = oDesignatePrinterService.DesignatePrinterRQ(oDesignatePrinterRQ);
                    if (oDesignatePrinterRS.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = oDesignatePrinterRS.Errors.Error.ErrorCode;
                        cParametros.Info = oDesignatePrinterRS.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = oDesignatePrinterRS.Errors.Error.ErrorMessage;
                        cParametros.Severity = oDesignatePrinterRS.Errors.Error.Severity;
                        cParametros.Complemento = "HostCommand: " + oDesignatePrinterRS.TPA_Extensions.HostCommand;
                        cParametros.Metodo = "getDesignatePrinter";
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
                        cParametros.Message = oDesignatePrinterRS.Success;
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
                        cParametros.Metodo = "getBusqueda";
                        cParametros.Complemento = "HostCommand: " + oDesignatePrinterRS.TPA_Extensions.HostCommand;
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
            return oDesignatePrinterRS;
        }
        public AirTicketRS getAirTicketRS(VO_PrintersTicketsRQ vo_PrintersTicketsRQ)
        {
            /*METODO PRINCICPAL QUE RETORNA EL OBJETO DE RESULTADOS DE SABRE*/
            StringBuilder consulta = new StringBuilder();
            clsParametros cParametros = new clsParametros();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
            AirTicketRQ.AirTicketRQ oAirTicketRQ = new WS_SsoftSabre.AirTicketRQ.AirTicketRQ();
            AirTicketRS oAirTicketRS = new AirTicketRS();

            objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();
            try
            {
                List<string> lsContadorOpciones = new List<string>();
                AirTicketRQ.MessageHeader Mensaje_ = clsSabreBase.AirTicket();

                if (Mensaje_ != null)
                {
                    AirTicketRQ.Security Seguridad_ = new AirTicketRQ.Security();
                    Seguridad_.BinarySecurityToken = Session_;

                    #region [ POS ]
                    AirTicketRQPOS oAirTicketRQPOS = new AirTicketRQPOS();
                    AirTicketRQPOSSource oAirTicketRQPOSSource = new AirTicketRQPOSSource();

                    oAirTicketRQPOSSource.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                    oAirTicketRQPOS.Source = oAirTicketRQPOSSource;
                    oAirTicketRQ.POS = oAirTicketRQPOS;
                    #endregion

                    #region [ VERSION ]
                    oAirTicketRQ.Version = clsSabreBase.SABRE_VERSION_AIRTICKET;
                    #endregion

                    #region [ DESIGNATEPRINTERS ]

                    AirTicketRQOptionalQualifiers oAirTicketRQOptionalQualifiers = new AirTicketRQOptionalQualifiers();
                    AirTicketRQOptionalQualifiersPricingQualifiers oPricingQualifiers = new AirTicketRQOptionalQualifiersPricingQualifiers();
                    AirTicketRQOptionalQualifiersPricingQualifiersBasicPrice[] oPricingQualifiersBasicPrices = new AirTicketRQOptionalQualifiersPricingQualifiersBasicPrice[vo_PrintersTicketsRQ.PQNumber.Count];
                    AirTicketRQOptionalQualifiersMiscQualifiers oMiscQualifiers = new AirTicketRQOptionalQualifiersMiscQualifiers();
                    AirTicketRQOptionalQualifiersMiscQualifiersCommission oMiscQualifiersCommission = new AirTicketRQOptionalQualifiersMiscQualifiersCommission();
                    int iPosPQ = 0;
                    foreach (string sPQNumber in vo_PrintersTicketsRQ.PQNumber)
                    {
                        AirTicketRQOptionalQualifiersPricingQualifiersBasicPrice oPricingQualifiersBasicPrice = new AirTicketRQOptionalQualifiersPricingQualifiersBasicPrice();
                        oPricingQualifiersBasicPrice.PQNumber = vo_PrintersTicketsRQ.PQNumber[iPosPQ].ToString();

                        //oPricingQualifiersBasicPrice.EndPQNumber = vo_PrintersTicketsRQ.EndPQNumber;
                        oPricingQualifiersBasicPrices[iPosPQ] = oPricingQualifiersBasicPrice;
                        iPosPQ++;
                    }
                    oPricingQualifiers.BasicPrice = oPricingQualifiersBasicPrices;
                    oAirTicketRQOptionalQualifiers.PricingQualifiers = oPricingQualifiers;

                    oMiscQualifiersCommission.Percentage = vo_PrintersTicketsRQ.CommisionPercent;
                    oMiscQualifiers.Commission = oMiscQualifiersCommission;

                    oAirTicketRQ.OptionalQualifiers = oAirTicketRQOptionalQualifiers;

                    #endregion

                    AirTicketService oAirTicketService = new AirTicketService();

                    oAirTicketService.MessageHeaderValue = Mensaje_;
                    oAirTicketService.SecurityValue = Seguridad_;
                    oAirTicketService.Url = objvo_Credentials.UrlWebServices;

                    oAirTicketRS = oAirTicketService.AirTicketRQ(oAirTicketRQ);
                    if (oAirTicketRS.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = oAirTicketRS.Errors.Error.ErrorCode;
                        cParametros.Info = oAirTicketRS.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = oAirTicketRS.Errors.Error.ErrorMessage;
                        cParametros.Severity = oAirTicketRS.Errors.Error.Severity;
                        cParametros.Complemento = "HostCommand: " + oAirTicketRS.TPA_Extensions.HostCommand;
                        cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
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
                        try
                        {
                            Negocios_WebServiceSabreCommand.setEmailError(cParametros, "Error al emitir tiquetes");
                        }
                        catch { }
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.Message = oAirTicketRS.Success;
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
                        cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        cParametros.Complemento = "HostCommand: " + oAirTicketRS.TPA_Extensions.HostCommand;
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
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
                try
                {
                    Negocios_WebServiceSabreCommand.setEmailError(cParametros, "Error al Emitir tiquetes");
                }
                catch { }
            }
            return oAirTicketRS;
        }
        #endregion
    }
}



