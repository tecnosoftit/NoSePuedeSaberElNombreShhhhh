using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;
namespace WS_SsoftSabre.Air
{
    public class clsTravelItineraryAddInfoLLSRQ
    {
        string Session_ = String.Empty;
        Ssoft.ValueObjects.VO_Credentials objvo_Credentials;

        public clsTravelItineraryAddInfoLLSRQ()
        {
            Session_ = AutenticacionSabre.GET_SabreSession();
        }

        public WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRS _Sabre_AgregarInformacionPNR(List<VO_DataTravelItineraryAddInfo> InformacionItinerario_)
        {
            objvo_Credentials = clsSesiones.getCredentials();
            WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRS TravelResultado_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRS();
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            StringBuilder consulta = new StringBuilder();
            clsCache cCache = new csCache().cCache();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
            try
            {

                WebService_TravelItineraryAddInfoLLS.MessageHeader Mensaje_ = clsSabreBase.__ISabre_TravelItineraryAddInfoLLS();

                if (Mensaje_ != null)
                {

                    WebService_TravelItineraryAddInfoLLS.Security Seguridad_ = new WebService_TravelItineraryAddInfoLLS.Security();
                    Seguridad_.BinarySecurityToken = Session_;
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQ Travel_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQ();
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQPOS TravelPos_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQPOS();
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQPOSSource TravelSource_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQPOSSource();
                    TravelSource_.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                    TravelPos_.Source = TravelSource_;
                    Travel_.POS = TravelPos_;

                    #region [ DATOS AGENCIA ]
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfo Travel_Agencia_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfo();
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoAddress Travel_AgenciaDireccion_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoAddress();
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoAddressTPA_Extensions Travel_AgenciaDireccionExt_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoAddressTPA_Extensions();
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoTelephone[] Travel_AgenciaTelefonoArray_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoTelephone[1];
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoTicketing Travel_AgenciaTickete_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoTicketing();
                    Travel_AgenciaDireccionExt_.AgencyName = objvo_Credentials.Agencia_Nombre;//ConfigurationManager.AppSettings["Agencia_Nombre"];
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoAddressStreetNmbr Travel_AgenciaDireccionNumero_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoAddressStreetNmbr();
                    Travel_AgenciaDireccionNumero_.PO_Box = objvo_Credentials.Agencia_Direccion;//ConfigurationManager.AppSettings["Agencia_Direccion"];
                    Travel_AgenciaDireccion_.StreetNmbr = Travel_AgenciaDireccionNumero_;
                    Travel_AgenciaDireccion_.CityName = objvo_Credentials.Agencia_Ciudad;// ConfigurationManager.AppSettings["Agencia_Ciudad"];
                    Travel_AgenciaDireccion_.PostalCode = objvo_Credentials.Agencia_CodigoPostal;// ConfigurationManager.AppSettings["Agencia_CodigoPostal"];
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoAddressStateCountyProv Travel_AgenciaDireccionProv_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoAddressStateCountyProv();
                    Travel_AgenciaDireccionProv_.StateCode = objvo_Credentials.Agencia_CodigoEstado;// ConfigurationManager.AppSettings["Agencia_CodigoEstado"];
                    Travel_AgenciaDireccion_.StateCountyProv = Travel_AgenciaDireccionProv_;
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoAddressCountryName Travel_AgenciaDireccionName_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoAddressCountryName();
                    Travel_AgenciaDireccionName_.Code = objvo_Credentials.Agencia_CodigoPais;// ConfigurationManager.AppSettings["Agencia_CodigoPais"];
                    Travel_AgenciaDireccion_.CountryName = Travel_AgenciaDireccionName_;
                    Travel_AgenciaDireccion_.TPA_Extensions = Travel_AgenciaDireccionExt_;
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoTelephone Travel_AgenciaTelefono_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQAgencyInfoTelephone();
                    Travel_AgenciaTelefono_.AreaCityCode = objvo_Credentials.Agencia_CodigoArea;// ConfigurationManager.AppSettings["Agencia_CodigoArea"];
                    Travel_AgenciaTelefono_.PhoneLocationType = objvo_Credentials.Agencia_CodigoLocalizacion;// ConfigurationManager.AppSettings["Agencia_CodigoLocalizacion"];
                    Travel_AgenciaTelefono_.PhoneNumber = objvo_Credentials.Agencia_Telefono;// ConfigurationManager.AppSettings["Agencia_Telefono"];
                    Travel_AgenciaTelefonoArray_[0] = Travel_AgenciaTelefono_;
                    //Travel_AgenciaTickete_.TicketingDate = ConfigurationManager.AppSettings["Agencia_TicketeFecha"];
                    Travel_AgenciaTickete_.QueueID = objvo_Credentials.Agencia_TiketeId;// ConfigurationManager.AppSettings["Agencia_TicketeId"];
                    Travel_AgenciaTickete_.Manual = bool.Parse(objvo_Credentials.Agencia_TiketeManual/*ConfigurationManager.AppSettings["Agencia_TicketeManual"]*/);
                    Travel_AgenciaTickete_.TicketTimeLimit = objvo_Credentials.Agencia_TiketTimeLimit;//ConfigurationManager.AppSettings["Agencia_TicketTimeLimit"];
                    Travel_Agencia_.Address = Travel_AgenciaDireccion_;
                    Travel_Agencia_.Telephone = Travel_AgenciaTelefonoArray_;
                    Travel_Agencia_.Ticketing = Travel_AgenciaTickete_;
                    #endregion

                    #region [ DATOS VIAJEROS ]
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfo Travel_InformacionCliente_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfo();
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoPersonName[] Travel_InformacionClienteArray_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoPersonName[InformacionItinerario_.Count];
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoPassengerType[] Travel_InformacionCliente_TipoArray_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoPassengerType[InformacionItinerario_.Count];
                    List<WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoTelephone> Travel_InformacionCliente_TelefonoArray_ = new List<WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoTelephone>();
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoEmail[] Travel_InformacionCliente_EmailArray_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoEmail[InformacionItinerario_.Count];
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoCustLoyalty[] Travel_InformacionCliente_CustLoyaltyArray_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoCustLoyalty[InformacionItinerario_.Count];
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoCustomerIdentifier[] Travel_InformacionClienteIdentifierArray_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoCustomerIdentifier[InformacionItinerario_.Count];
                    int i = 0;
                    int y = 1;
                    foreach (VO_DataTravelItineraryAddInfo Informacion_ in InformacionItinerario_)
                    {
                        WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoEmail Travel_InformacionCliente_Email_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoEmail();
                        WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoCustLoyalty Travel_InformacionCliente_CustLoyalty_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoCustLoyalty();
                        WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoPersonName Travel_InformacionClientePerson_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoPersonName();
                        WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoPersonNameInfant Travel_InformacionCliente_Infant_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoPersonNameInfant();
                        WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoPassengerType Travel_InformacionCliente_Tipo_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoPassengerType();
                        WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoCustomerIdentifier Travel_InformacionClienteIdentifier_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoCustomerIdentifier();

                        if (Informacion_.Telefono_ != null && Informacion_.Telefono_.Count > 0)
                        {

                            for (int l = 0; l < Informacion_.Telefono_.Count; l++)
                            {
                                WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoTelephone Travel_InformacionCliente_Telefono_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoTelephone();
                                if (Informacion_.Telefono_[l].Length.Equals(0))
                                {
                                    Travel_InformacionCliente_Telefono_.AreaCityCode = objvo_Credentials.Agencia_CodigoArea;
                                    Travel_InformacionCliente_Telefono_.PhoneLocationType = objvo_Credentials.Agencia_CodigoLocalizacion;
                                    Travel_InformacionCliente_Telefono_.PhoneNumber = objvo_Credentials.Agencia_Telefono;
                                }
                                else
                                {
                                    Travel_InformacionCliente_Telefono_.AreaCityCode = Informacion_.CodigoArea_;
                                    Travel_InformacionCliente_Telefono_.PhoneLocationType = Informacion_.CodigoLocalizacion_;
                                    Travel_InformacionCliente_Telefono_.PhoneNumber = Informacion_.Telefono_[l];
                                }
                                Travel_InformacionCliente_TelefonoArray_.Add(Travel_InformacionCliente_Telefono_);
                            }
                        }
                        else
                        {
                            WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoTelephone Travel_InformacionCliente_Telefono_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoRQCustomerInfoTelephone();

                            Travel_InformacionCliente_Telefono_.AreaCityCode = objvo_Credentials.Agencia_CodigoArea;
                            Travel_InformacionCliente_Telefono_.PhoneLocationType = objvo_Credentials.Agencia_CodigoLocalizacion;
                            Travel_InformacionCliente_Telefono_.PhoneNumber = objvo_Credentials.Agencia_Telefono; 
                            Travel_InformacionCliente_TelefonoArray_.Add(Travel_InformacionCliente_Telefono_);
                        }
                        //if (Informacion_.Documento_ != null && Informacion_.Documento_.Length > 0)
                        //{
                        //    Travel_InformacionClienteIdentifier_.Identifier = "NI" + Informacion_.Documento_;
                        //    Travel_InformacionClienteIdentifierArray_[i] = Travel_InformacionClienteIdentifier_;
                        //}

                        if (Informacion_.Email_ != null && Informacion_.Email_.Length > 0)
                        {
                            Travel_InformacionCliente_Email_.EmailAddress = Informacion_.Email_;
                            Travel_InformacionCliente_Email_.NameNumber = Convert.ToString(Convert.ToString(y));
                            Travel_InformacionCliente_EmailArray_[i] = Travel_InformacionCliente_Email_;
                        }
                        Travel_InformacionCliente_Infant_.Ind = Informacion_.Infante_;
                        Travel_InformacionCliente_Infant_.IndSpecified = true;
                        Travel_InformacionClientePerson_.GivenName = Informacion_.Nombre_;
                        Travel_InformacionClientePerson_.Surname = Informacion_.Apellido_;
                        Travel_InformacionClientePerson_.Infant = Travel_InformacionCliente_Infant_;
                        Travel_InformacionClientePerson_.RPH = Convert.ToString(y);
                        Travel_InformacionClienteArray_[i] = Travel_InformacionClientePerson_;
                        Travel_InformacionCliente_Tipo_.Code = Informacion_.Tipo_;
                        //Travel_InformacionCliente_Tipo_.NameNumber = Convert.ToString(Informacion_.Id_) + ".1";
                        Travel_InformacionCliente_Tipo_.NameNumber = Convert.ToString(y) + ".1";
                        Travel_InformacionCliente_TipoArray_[i] = Travel_InformacionCliente_Tipo_;
                        Travel_InformacionCliente_.PassengerType = Travel_InformacionCliente_TipoArray_;
                        try
                        {
                            // Se incluye el envio del pasajero frecuente
                            if (Informacion_.ViajeroFrecuente_ != null && Informacion_.ViajeroFrecuente_ != String.Empty)
                            {
                                Travel_InformacionCliente_CustLoyalty_.RPH = Convert.ToString(y);
                                Travel_InformacionCliente_CustLoyalty_.NameNumber = Convert.ToString(y) + ".1";
                                Travel_InformacionCliente_CustLoyalty_.ProgramID = Informacion_.Aeroliena_;
                                Travel_InformacionCliente_CustLoyalty_.MembershipID = Informacion_.ViajeroFrecuente_;
                                Travel_InformacionCliente_CustLoyaltyArray_[i] = Travel_InformacionCliente_CustLoyalty_;
                            }
                        }
                        catch { }
                        i++;
                        y++;
                    }
                    Travel_InformacionCliente_.Telephone = Travel_InformacionCliente_TelefonoArray_.ToArray();
                    Travel_InformacionCliente_.PersonName = Travel_InformacionClienteArray_;
                    Travel_InformacionCliente_.Email = Travel_InformacionCliente_EmailArray_;
                    Travel_InformacionCliente_.CustLoyalty = Travel_InformacionCliente_CustLoyaltyArray_;
                    //Travel_InformacionCliente_.CustomerIdentifier = Travel_InformacionClienteIdentifierArray_;

                    #endregion

                    Travel_.AgencyInfo = Travel_Agencia_;
                    Travel_.CustomerInfo = Travel_InformacionCliente_;
                    Travel_.Version = clsSabreBase.SABRE_VERSION_TRAVELITINERARYADDINFO;
                    WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoService TravelServicio_ = new WebService_TravelItineraryAddInfoLLS.TravelItineraryAddInfoService();
                    TravelServicio_.MessageHeaderValue = Mensaje_;
                    TravelServicio_.SecurityValue = Seguridad_;
                    TravelServicio_.Url = objvo_Credentials.UrlWebServices;

                    //string Comando_ = "*N";
                    //string sRespuesta = string.Empty;
                    //try
                    //{
                    //    sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(Comando_);
                    //}
                    //catch { }
                    TravelResultado_ = TravelServicio_.TravelItineraryAddInfoRQ(Travel_);

                    //try
                    //{
                    //    sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(Comando_);
                    //}
                    //catch { }
                    if (TravelResultado_.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.TipoLog = Enum_Error.Log;
                        cParametros.Code = TravelResultado_.Errors.Error.ErrorCode;
                        cParametros.Info = TravelResultado_.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = TravelResultado_.Errors.Error.ErrorMessage;
                        cParametros.Severity = TravelResultado_.Errors.Error.Severity;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Metodo = "_Sabre_AgregarInformacionPNR";
                        cParametros.Complemento = "HostCommand: " + TravelResultado_.TPA_Extensions.HostCommand;
                        cParametros.ViewMessage.Add("Error al intentar incluir los pasajeros");
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
                                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                                consulta.AppendLine("Session Sabre: " + Session_.ToString());
                            }
                        }
                        catch { }
                        cParametros.TargetSite = consulta.ToString();
                        try
                        {
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
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.TipoLog = Enum_Error.Transac;
                        cParametros.Metodo = "_Sabre_AgregarInformacionPNR";
                        cParametros.Complemento = "HostCommand: " + TravelResultado_.TPA_Extensions.HostCommand;
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
                                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                                consulta.AppendLine("Session Sabre: " + Session_.ToString());
                            }
                        }
                        catch { }
                        cParametros.TargetSite = consulta.ToString();
                        try
                        {
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

                        clsSesiones.SET_LOAD_PASAJERO(true);
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = "_Sabre_AgregarInformacionPNR";
                cParametros.Complemento = "Envio de pasajeros";
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
                        consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                        consulta.AppendLine("Session Sabre: " + Session_.ToString());
                    }
                }
                catch { }
                cParametros.TargetSite = consulta.ToString();
                try
                {
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
                cParametros.ViewMessage.Add("Error al intentar incluir los pasajeros");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return TravelResultado_;
        }
    }
}
