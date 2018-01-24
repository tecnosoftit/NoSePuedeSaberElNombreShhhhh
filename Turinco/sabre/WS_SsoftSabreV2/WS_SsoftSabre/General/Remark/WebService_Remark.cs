using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Text;
using System.Collections.Generic;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;
using WS_SsoftSabre.Air;
using Ssoft.ValueObjects;

public class WebService_Remark
{
    #region [ ATRIBUTOS ]

    protected string session_;
    Ssoft.ValueObjects.VO_Credentials objvo_Credentials;

    #endregion

    #region [ CONSTRUCTOR ]

    public WebService_Remark()
    {
        this.session_ = AutenticacionSabre.GET_SabreSession();
    }

    #endregion

    #region [ PROPIEDADES ]

    public string Session_
    {
        get { return session_; }
        set { session_ = value; }
    }

    #endregion

    #region [ METODOS ]

    public clsParametros _Sabre_AgregarObservaciones(Enum_TipoRemark TypeRemark_, List<string> ListTextRemark_)
    {
        WebService_AddRemarkLLS.AddRemarkRS RemarkResultado_ = new WebService_AddRemarkLLS.AddRemarkRS();
        clsParametros cParametros = new clsParametros();
        objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();
        StringBuilder consulta = new StringBuilder();
        cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;

        try
        {
            WebService_AddRemarkLLS.MessageHeader Mensaje_ = clsSabreBase.__ISabre_AddRemarkLLSRQ();

            if (Mensaje_ != null)
            {
                WebService_AddRemarkLLS.Security Seguridad_ = new WebService_AddRemarkLLS.Security();
                Seguridad_.BinarySecurityToken = Session_;

                WebService_AddRemarkLLS.AddRemarkRQ Remark_ = new WebService_AddRemarkLLS.AddRemarkRQ();
                WebService_AddRemarkLLS.AddRemarkRQPOS RemarkPos_ = new WebService_AddRemarkLLS.AddRemarkRQPOS();
                WebService_AddRemarkLLS.AddRemarkRQPOSSource RemarkSource_ = new WebService_AddRemarkLLS.AddRemarkRQPOSSource();
                // pcc viejo
                //RemarkSource_.PseudoCityCode = ConfigurationManager.AppSettings["Sabre_Ipcc"];
                RemarkSource_.PseudoCityCode = objvo_Credentials.Pcc;
                RemarkPos_.Source = RemarkSource_;
                Remark_.POS = RemarkPos_;

                #region [ Tipo Remark ]

                switch (TypeRemark_)
                {
                    #region " Libre "

                    case Enum_TipoRemark.Libre:

                        List<WebService_AddRemarkLLS.AddRemarkRQBasicRemark> ListBasikRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQBasicRemark>();

                        foreach (string TextRemark_ in ListTextRemark_)
                        {
                            WebService_AddRemarkLLS.AddRemarkRQBasicRemark BasikRemark_ = new WebService_AddRemarkLLS.AddRemarkRQBasicRemark("X/-" + TextRemark_);
                            ListBasikRemark_.Add(BasikRemark_);
                        }

                        Remark_.BasicRemark = ListBasikRemark_.ToArray();

                        break;

                    #endregion

                    #region " Compuesto "

                    case Enum_TipoRemark.Compuesto:

                        List<WebService_AddRemarkLLS.AddRemarkRQAlphaCodedRemark> ListAlphaCodedRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQAlphaCodedRemark>();

                        foreach (string TextRemark_ in ListTextRemark_)
                        {
                            WebService_AddRemarkLLS.AddRemarkRQAlphaCodedRemark AlphaCodedRemark_ = new WebService_AddRemarkLLS.AddRemarkRQAlphaCodedRemark("H", TextRemark_);
                            ListAlphaCodedRemark_.Add(AlphaCodedRemark_);
                        }

                        Remark_.AlphaCodedRemark = ListAlphaCodedRemark_.ToArray();

                        break;

                    #endregion

                    #region " Direccion Cliente "

                    case Enum_TipoRemark.DireccionCliente:

                        List<WebService_AddRemarkLLS.AddRemarkRQClientAddressRemark> ListClientAddressRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQClientAddressRemark>();

                        foreach (string TextRemark_ in ListTextRemark_)
                        {
                            WebService_AddRemarkLLS.AddRemarkRQClientAddressRemark ClientAddressRemark_ = new WebService_AddRemarkLLS.AddRemarkRQClientAddressRemark(TextRemark_);
                            ListClientAddressRemark_.Add(ClientAddressRemark_);
                        }

                        Remark_.ClientAddressRemark = ListClientAddressRemark_.ToArray();

                        break;

                    #endregion

                    #region " Direccion "

                    case Enum_TipoRemark.Direccion:

                        List<WebService_AddRemarkLLS.AddRemarkRQDeliveryAddressRemark> ListDeliveryAddressRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQDeliveryAddressRemark>();

                        foreach (string TextRemark_ in ListTextRemark_)
                        {
                            WebService_AddRemarkLLS.AddRemarkRQDeliveryAddressRemark DeliveryAddressRemark_ = new WebService_AddRemarkLLS.AddRemarkRQDeliveryAddressRemark(TextRemark_);
                            ListDeliveryAddressRemark_.Add(DeliveryAddressRemark_);
                        }

                        Remark_.DeliveryAddressRemark = ListDeliveryAddressRemark_.ToArray();

                        break;

                    #endregion

                    #region " Impresion "

                    case Enum_TipoRemark.Impresion:

                        List<WebService_AddRemarkLLS.AddRemarkRQInvoiceRemark> ListInvoiceRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQInvoiceRemark>();

                        foreach (string TextRemark_ in ListTextRemark_)
                        {
                            WebService_AddRemarkLLS.AddRemarkRQInvoiceRemark InvoiceRemark_ = new WebService_AddRemarkLLS.AddRemarkRQInvoiceRemark(TextRemark_);
                            ListInvoiceRemark_.Add(InvoiceRemark_);
                        }

                        Remark_.InvoiceRemark = ListInvoiceRemark_.ToArray();

                        break;

                    #endregion

                    #region " Simple "

                    case Enum_TipoRemark.Simple:

                        List<WebService_AddRemarkLLS.AddRemarkRQItineraryRemark> ListItineraryRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQItineraryRemark>();

                        foreach (string TextRemark_ in ListTextRemark_)
                        {
                            WebService_AddRemarkLLS.AddRemarkRQItineraryRemark ItineraryRemark_ = new WebService_AddRemarkLLS.AddRemarkRQItineraryRemark(TextRemark_);
                            ListItineraryRemark_.Add(ItineraryRemark_);
                        }

                        Remark_.ItineraryRemark = ListItineraryRemark_.ToArray();

                        break;

                    #endregion

                    #region " Grupo "

                    case Enum_TipoRemark.Grupo:

                        List<WebService_AddRemarkLLS.AddRemarkRQGroupNameRemark> ListGroupNameRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQGroupNameRemark>();

                        foreach (string TextRemark_ in ListTextRemark_)
                        {
                            WebService_AddRemarkLLS.AddRemarkRQGroupNameRemark GroupNameRemark_ = new WebService_AddRemarkLLS.AddRemarkRQGroupNameRemark(TextRemark_);
                            ListGroupNameRemark_.Add(GroupNameRemark_);
                        }

                        Remark_.GroupNameRemark = ListGroupNameRemark_.ToArray();

                        break;

                    #endregion

                    #region " Historico "

                    case Enum_TipoRemark.Historico:

                        List<WebService_AddRemarkLLS.AddRemarkRQHistoricalRemark> ListHistoricalRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQHistoricalRemark>();

                        foreach (string TextRemark_ in ListTextRemark_)
                        {
                            WebService_AddRemarkLLS.AddRemarkRQHistoricalRemark HistoricalRemark_ = new WebService_AddRemarkLLS.AddRemarkRQHistoricalRemark(TextRemark_);
                            ListHistoricalRemark_.Add(HistoricalRemark_);
                        }

                        Remark_.HistoricalRemark = ListHistoricalRemark_.ToArray();

                        break;

                    #endregion

                    #region " Oculto "

                    case Enum_TipoRemark.Oculto:

                        List<WebService_AddRemarkLLS.AddRemarkRQHiddenRemark> ListHiddenRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQHiddenRemark>();

                        foreach (string TextRemark_ in ListTextRemark_)
                        {
                            WebService_AddRemarkLLS.AddRemarkRQHiddenRemark HiddenRemark_ = new WebService_AddRemarkLLS.AddRemarkRQHiddenRemark(TextRemark_);
                            ListHiddenRemark_.Add(HiddenRemark_);
                        }

                        Remark_.HiddenRemark = ListHiddenRemark_.ToArray();

                        break;

                    #endregion
                }

                #endregion

                Remark_.Version = clsSabreBase.SABRE_VERSION_ADDREMARK;

                WebService_AddRemarkLLS.AddRemarkService Servicio_ = new WebService_AddRemarkLLS.AddRemarkService();

                Servicio_.MessageHeaderValue = Mensaje_;
                Servicio_.SecurityValue = Seguridad_;

                RemarkResultado_ = Servicio_.AddRemarkRQ(Remark_);

                if (RemarkResultado_.Errors != null)
                {
                    WebService_AddRemarkLLS.AddRemarkRSErrorsError Error_ = RemarkResultado_.Errors.Error;
                    WebService_AddRemarkLLS.AddRemarkRSErrorsErrorErrorInfo ErrorInfo_ = Error_.ErrorInfo;

                    cParametros.Id = 0;
                    cParametros.TipoLog = Enum_Error.Transac;
                    cParametros.Code = RemarkResultado_.Errors.Error.ErrorCode;
                    cParametros.Info = RemarkResultado_.Errors.Error.ErrorInfo.Message;
                    cParametros.Message = RemarkResultado_.Errors.Error.ErrorMessage;
                    cParametros.Severity = RemarkResultado_.Errors.Error.Severity;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = "Remarks";
                    cParametros.Complemento = "HostCommand: " + RemarkResultado_.TPA_Extensions.HostCommand;
                    cParametros.Message = RemarkResultado_.Errors.Error.ErrorMessage;
                    ExceptionHandled.Publicar(cParametros);
                    cParametros.TipoLog = Enum_Error.Log;
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
                        }
                    }
                    catch { }
                    cParametros.StackTrace = consulta.ToString();
                    ExceptionHandled.Publicar(cParametros);
                }
                else
                {
                    cParametros.Id = 1;
                    cParametros.TipoLog = Enum_Error.Transac;
                    cParametros.Message = "Response: " + RemarkResultado_.Success.ToString();
                    cParametros.Metodo = "_Remark_Observaciones";
                    try
                    {
                        cParametros.Complemento = "HostCommand: " + RemarkResultado_.TPA_Extensions.HostCommand;
                    }
                    catch { }
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
                    cParametros.TipoLog = Enum_Error.Log;
                    ExceptionHandled.Publicar(cParametros);
                }
            }
        }
        catch (Exception Ex)
        {
            cParametros.Id = 0;
            cParametros.Message = Ex.Message.ToString();
            cParametros.Source = Ex.Source.ToString();
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.StackTrace = Ex.StackTrace.ToString();
            cParametros.Complemento = "Error al ejecutar Reamrk Sabre";
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
                }
            }
            catch { }
            cParametros.TargetSite = consulta.ToString();
            ExceptionHandled.Publicar(cParametros);
        }
        return cParametros;
    }

    public clsParametros _Sabre_AgregarObservaciones(List<VO_Remarks> ListRemark_)
    {
        WebService_AddRemarkLLS.AddRemarkRS RemarkResultado_ = new WebService_AddRemarkLLS.AddRemarkRS();
        clsParametros cParametros = new clsParametros();
        objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();
        StringBuilder consulta = new StringBuilder();

        try
        {
            WebService_AddRemarkLLS.MessageHeader Mensaje_ = clsSabreBase.__ISabre_AddRemarkLLSRQ();

            if (Mensaje_ != null)
            {
                WebService_AddRemarkLLS.Security Seguridad_ = new WebService_AddRemarkLLS.Security();
                Seguridad_.BinarySecurityToken = Session_;

                WebService_AddRemarkLLS.AddRemarkRQ Remark_ = new WebService_AddRemarkLLS.AddRemarkRQ();
                WebService_AddRemarkLLS.AddRemarkRQPOS RemarkPos_ = new WebService_AddRemarkLLS.AddRemarkRQPOS();
                WebService_AddRemarkLLS.AddRemarkRQPOSSource RemarkSource_ = new WebService_AddRemarkLLS.AddRemarkRQPOSSource();
                // pcc viejo
                //RemarkSource_.PseudoCityCode = ConfigurationManager.AppSettings["Sabre_Ipcc"];
                RemarkSource_.PseudoCityCode = objvo_Credentials.Pcc;
                RemarkPos_.Source = RemarkSource_;
                Remark_.POS = RemarkPos_;

                // Generacion de arreglos
                List<WebService_AddRemarkLLS.AddRemarkRQBasicRemark> ListBasikRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQBasicRemark>();
                List<WebService_AddRemarkLLS.AddRemarkRQAlphaCodedRemark> ListAlphaCodedRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQAlphaCodedRemark>();
                List<WebService_AddRemarkLLS.AddRemarkRQClientAddressRemark> ListClientAddressRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQClientAddressRemark>();
                List<WebService_AddRemarkLLS.AddRemarkRQDeliveryAddressRemark> ListDeliveryAddressRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQDeliveryAddressRemark>();
                List<WebService_AddRemarkLLS.AddRemarkRQInvoiceRemark> ListInvoiceRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQInvoiceRemark>();
                List<WebService_AddRemarkLLS.AddRemarkRQItineraryRemark> ListItineraryRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQItineraryRemark>();
                List<WebService_AddRemarkLLS.AddRemarkRQGroupNameRemark> ListGroupNameRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQGroupNameRemark>();
                List<WebService_AddRemarkLLS.AddRemarkRQHistoricalRemark> ListHistoricalRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQHistoricalRemark>();
                List<WebService_AddRemarkLLS.AddRemarkRQHiddenRemark> ListHiddenRemark_ = new List<WebService_AddRemarkLLS.AddRemarkRQHiddenRemark>();

                #region [ Tipo Remark ]
                foreach (VO_Remarks vo_Remark in ListRemark_)
                {
                    switch (vo_Remark.TipoRemark)
                    {
                        #region " Libre "

                        case Enum_TipoRemark.Libre:
                            WebService_AddRemarkLLS.AddRemarkRQBasicRemark BasikRemark_ = new WebService_AddRemarkLLS.AddRemarkRQBasicRemark("X/-" + vo_Remark.Remark);
                            ListBasikRemark_.Add(BasikRemark_);

                            Remark_.BasicRemark = ListBasikRemark_.ToArray();

                            break;

                        #endregion

                        #region " Compuesto "

                        case Enum_TipoRemark.Compuesto:
                            WebService_AddRemarkLLS.AddRemarkRQAlphaCodedRemark AlphaCodedRemark_ = new WebService_AddRemarkLLS.AddRemarkRQAlphaCodedRemark("H", vo_Remark.Remark);
                            ListAlphaCodedRemark_.Add(AlphaCodedRemark_);

                            Remark_.AlphaCodedRemark = ListAlphaCodedRemark_.ToArray();

                            break;

                        #endregion

                        #region " Direccion Cliente "

                        case Enum_TipoRemark.DireccionCliente:
                            WebService_AddRemarkLLS.AddRemarkRQClientAddressRemark ClientAddressRemark_ = new WebService_AddRemarkLLS.AddRemarkRQClientAddressRemark(vo_Remark.Remark);
                            ListClientAddressRemark_.Add(ClientAddressRemark_);

                            Remark_.ClientAddressRemark = ListClientAddressRemark_.ToArray();

                            break;

                        #endregion

                        #region " Direccion "

                        case Enum_TipoRemark.Direccion:
                            WebService_AddRemarkLLS.AddRemarkRQDeliveryAddressRemark DeliveryAddressRemark_ = new WebService_AddRemarkLLS.AddRemarkRQDeliveryAddressRemark(vo_Remark.Remark);
                            ListDeliveryAddressRemark_.Add(DeliveryAddressRemark_);

                            Remark_.DeliveryAddressRemark = ListDeliveryAddressRemark_.ToArray();

                            break;

                        #endregion

                        #region " Impresion "

                        case Enum_TipoRemark.Impresion:
                            WebService_AddRemarkLLS.AddRemarkRQInvoiceRemark InvoiceRemark_ = new WebService_AddRemarkLLS.AddRemarkRQInvoiceRemark(vo_Remark.Remark);
                            ListInvoiceRemark_.Add(InvoiceRemark_);

                            Remark_.InvoiceRemark = ListInvoiceRemark_.ToArray();

                            break;

                        #endregion

                        #region " Simple "

                        case Enum_TipoRemark.Simple:
                            WebService_AddRemarkLLS.AddRemarkRQItineraryRemark ItineraryRemark_ = new WebService_AddRemarkLLS.AddRemarkRQItineraryRemark(vo_Remark.Remark);
                            ListItineraryRemark_.Add(ItineraryRemark_);

                            Remark_.ItineraryRemark = ListItineraryRemark_.ToArray();

                            break;

                        #endregion

                        #region " Grupo "

                        case Enum_TipoRemark.Grupo:
                            WebService_AddRemarkLLS.AddRemarkRQGroupNameRemark GroupNameRemark_ = new WebService_AddRemarkLLS.AddRemarkRQGroupNameRemark(vo_Remark.Remark);
                            ListGroupNameRemark_.Add(GroupNameRemark_);

                            Remark_.GroupNameRemark = ListGroupNameRemark_.ToArray();

                            break;

                        #endregion

                        #region " Historico "

                        case Enum_TipoRemark.Historico:
                            WebService_AddRemarkLLS.AddRemarkRQHistoricalRemark HistoricalRemark_ = new WebService_AddRemarkLLS.AddRemarkRQHistoricalRemark(vo_Remark.Remark);
                            ListHistoricalRemark_.Add(HistoricalRemark_);

                            Remark_.HistoricalRemark = ListHistoricalRemark_.ToArray();

                            break;

                        #endregion

                        #region " Oculto "

                        case Enum_TipoRemark.Oculto:
                            WebService_AddRemarkLLS.AddRemarkRQHiddenRemark HiddenRemark_ = new WebService_AddRemarkLLS.AddRemarkRQHiddenRemark(vo_Remark.Remark);
                            ListHiddenRemark_.Add(HiddenRemark_);

                            Remark_.HiddenRemark = ListHiddenRemark_.ToArray();

                            break;
                        #endregion
                    }
                }

                #endregion

                Remark_.Version = clsSabreBase.SABRE_VERSION_ADDREMARK;

                WebService_AddRemarkLLS.AddRemarkService Servicio_ = new WebService_AddRemarkLLS.AddRemarkService();

                Servicio_.MessageHeaderValue = Mensaje_;
                Servicio_.SecurityValue = Seguridad_;

                RemarkResultado_ = Servicio_.AddRemarkRQ(Remark_);

                if (RemarkResultado_.Errors != null)
                {
                    WebService_AddRemarkLLS.AddRemarkRSErrorsError Error_ = RemarkResultado_.Errors.Error;
                    WebService_AddRemarkLLS.AddRemarkRSErrorsErrorErrorInfo ErrorInfo_ = Error_.ErrorInfo;

                    cParametros.Id = 0;
                    cParametros.Code = RemarkResultado_.Errors.Error.ErrorCode;
                    cParametros.Info = RemarkResultado_.Errors.Error.ErrorInfo.Message;
                    cParametros.Message = RemarkResultado_.Errors.Error.ErrorMessage;
                    cParametros.Severity = RemarkResultado_.Errors.Error.Severity;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = "Remarks";
                    try
                    {
                        cParametros.Complemento = "HostCommand: " + RemarkResultado_.TPA_Extensions.HostCommand;
                    }
                    catch { }
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
                        }
                    }
                    catch { }
                    cParametros.TargetSite = consulta.ToString();
                    cParametros.Message = RemarkResultado_.Errors.Error.ErrorMessage;
                    ExceptionHandled.Publicar(cParametros);
                    cParametros.TipoLog = Enum_Error.Log;
                    ExceptionHandled.Publicar(cParametros);
                }
                else
                {
                    cParametros.Id = 1;
                    cParametros.TipoLog = Enum_Error.Transac;
                    cParametros.Message = "Response: " + RemarkResultado_.Success.ToString();
                    cParametros.Metodo = "_Remark_Observaciones";
                    cParametros.Complemento = "HostCommand: " + RemarkResultado_.TPA_Extensions.HostCommand;
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
                    cParametros.TipoLog = Enum_Error.Log;
                }
            }
        }
        catch (Exception Ex)
        {
            cParametros.Id = 0;
            cParametros.Message = Ex.Message.ToString();
            cParametros.Source = Ex.Source.ToString();
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.StackTrace = Ex.StackTrace.ToString();
            cParametros.Complemento = "Error al ejecutar Reamrk Sabre";
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
                }
            }
            catch { }
            cParametros.TargetSite = consulta.ToString();
            ExceptionHandled.Publicar(cParametros);
        }
        return cParametros;
    }
    #endregion

    #region [ DESTRUCTOR ]

    ~WebService_Remark() { }

    #endregion
}
