using System;
using System.Collections.Generic;
using System.Text;
using WS_SsoftSabre.SabreCommandLLS;
using Ssoft.ValueObjects;
using WebService_SabreCommandLLS = WS_SsoftSabre.SabreCommandLLS;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;

namespace WS_SsoftSabre.Air
{
    public class clsSabreCommandLLS
    {
        #region [ ATRIBUTOS ]

        protected string strSesion;

        #endregion

        #region [ CONSTRUCTOR ]

        public clsSabreCommandLLS()
        {
        }

        #endregion

        #region [ PROPIEDADES ]

        public string StrSesion
        {
            get { return strSesion; }
            set { strSesion = value; }
        }

        #endregion

        #region [ METODOS ]

        public SabreCommandLLSRS getEjecutarComando(VO_SabreCommandLLSRS vo_SabreCommandLLSRS)
        {
            SabreCommandLLSRS SabreCommandRespuesta_ = new SabreCommandLLSRS();
            MessageHeader srMensaje = clsSabreBase.SabreCommandLLS();
            clsParametros cParametros = new clsParametros();
            clsCache cCache = new csCache().cCache();
            VO_Credentials objvo_Credentials;
            objvo_Credentials = clsSesiones.getCredentials();
            StringBuilder consulta = new StringBuilder();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;

            if (srMensaje != null)
            {
                Security Seguridad_ = new Security();
                Seguridad_.BinarySecurityToken = StrSesion;
                SabreCommandLLSRQ SabreCommand_ = new SabreCommandLLSRQ();
                SabreCommandLLSRQRequest oSabreCommandLLSRQRequest = new SabreCommandLLSRQRequest();
                //oSabreCommandLLSRQRequest.Output = vo_SabreCommandLLSRS.ESalida;
                oSabreCommandLLSRQRequest.CDATA = vo_SabreCommandLLSRS.BCDATA;
                oSabreCommandLLSRQRequest.HostCommand = vo_SabreCommandLLSRS.StrComando;
                SabreCommand_.Request = oSabreCommandLLSRQRequest;
                SabreCommand_.Version = clsSabreBase.COMMANDLLS_VERSION;
                SabreCommandLLSService SabreCommandServicio_ = new SabreCommandLLSService();
                SabreCommandServicio_.MessageHeaderValue = srMensaje;
                SabreCommandServicio_.SecurityValue = Seguridad_;
                SabreCommandServicio_.Url = objvo_Credentials.UrlWebServices;

                SabreCommandRespuesta_ = SabreCommandServicio_.SabreCommandLLSRQ(SabreCommand_);

                if (SabreCommandRespuesta_.ErrorRS != null)
                {
                    cParametros.Id = 0;
                    cParametros.Code = SabreCommandRespuesta_.ErrorRS.Errors.Error.ErrorCode;
                    cParametros.Info = SabreCommandRespuesta_.ErrorRS.Errors.Error.ErrorInfo.Message;
                    cParametros.Message = SabreCommandRespuesta_.ErrorRS.Errors.Error.ErrorMessage;
                    cParametros.Severity = SabreCommandRespuesta_.ErrorRS.Errors.Error.Severity;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.ViewMessage.Add("");
                    cParametros.Sugerencia.Add("");
                    cParametros.Message = SabreCommandRespuesta_.ErrorRS.Errors.Error.ErrorMessage;
                    cParametros.Complemento = "SabreCommand";
                    cParametros.InnerException = "HostCommand: " + vo_SabreCommandLLSRS.StrComando;
                    cParametros.TipoLog = Enum_Error.Log;
                    try
                    {
                        cParametros.TargetSite = "Response: " + SabreCommandRespuesta_.Response;
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
                            consulta.AppendLine("Session Sabre: " + strSesion.ToString());
                            consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                        }
                    }
                    catch { }
                    cParametros.StackTrace = consulta.ToString();

                    try
                    {
                        cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
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
                    //if (clsValidaciones.GetKeyOrAdd("ValidaLogTransac", "False").ToUpper().Equals("TRUE"))
                    //{
                        cParametros.Id = 1;
                        cParametros.Complemento = "SabreCommand";
                        cParametros.Message = "HostCommand: " + vo_SabreCommandLLSRS.StrComando;
                        cParametros.Info = "Response: " + SabreCommandRespuesta_.Response;
                        cParametros.Severity = clsSeveridad.Moderada;
                        cParametros.Tipo = clsTipoError.WebServices;
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
                                consulta.AppendLine("Session Sabre: " + strSesion.ToString());
                                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                            }
                        }
                        catch { }
                        cParametros.StackTrace = consulta.ToString();
                        try
                        {
                            cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                            cParametros.StackTrace = "Session Sabre: " + strSesion.ToString();
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
                    //}
                }
            }

            return SabreCommandRespuesta_;
        }

        public WebService_SabreCommandLLS.SabreCommandLLSRS _Sabre_EjecutarComando(string Comando_)
        {
            WebService_SabreCommandLLS.SabreCommandLLSRS SabreCommandRespuesta_ = new WebService_SabreCommandLLS.SabreCommandLLSRS();
            clsParametros cParametros = new clsParametros();
            VO_Credentials objvo_Credentials;
            objvo_Credentials = clsSesiones.getCredentials();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
            StringBuilder consulta = new StringBuilder();
            clsCache cCache = new csCache().cCache();
            try
            {
                WebService_SabreCommandLLS.MessageHeader Mensaje_ = WS_SsoftSabre.Air.clsSabreBase.SabreCommandLLS();

                if (Mensaje_ != null)
                {
                    WebService_SabreCommandLLS.Security Seguridad_ = new WebService_SabreCommandLLS.Security();
                    Seguridad_.BinarySecurityToken = strSesion;
                    WebService_SabreCommandLLS.SabreCommandLLSRQ SabreCommand_ = new WebService_SabreCommandLLS.SabreCommandLLSRQ();
                    WebService_SabreCommandLLS.SabreCommandLLSRQRequest oSabreComandReques = new WS_SsoftSabre.SabreCommandLLS.SabreCommandLLSRQRequest();
                    oSabreComandReques.HostCommand = Comando_;
                    SabreCommand_.Request = oSabreComandReques;
                    SabreCommand_.Version = WS_SsoftSabre.Air.clsSabreBase.SABRE_VERSION_SABRECOMMANDLLS;
                    WebService_SabreCommandLLS.SabreCommandLLSService SabreCommandServicio_ = new WebService_SabreCommandLLS.SabreCommandLLSService();
                    SabreCommandServicio_.MessageHeaderValue = Mensaje_;
                    SabreCommandServicio_.SecurityValue = Seguridad_;
                    SabreCommandServicio_.Url = objvo_Credentials.UrlWebServices;
                    SabreCommandRespuesta_ = SabreCommandServicio_.SabreCommandLLSRQ(SabreCommand_);

                    if (SabreCommandRespuesta_.ErrorRS != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = SabreCommandRespuesta_.ErrorRS.Errors.Error.ErrorCode;
                        cParametros.Info = SabreCommandRespuesta_.ErrorRS.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = SabreCommandRespuesta_.ErrorRS.Errors.Error.ErrorMessage;
                        cParametros.Severity = SabreCommandRespuesta_.ErrorRS.Errors.Error.Severity;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        cParametros.Complemento = "";
                        cParametros.ViewMessage.Add("");
                        cParametros.Sugerencia.Add("");
                        cParametros.InnerException = "HostCommand: " + Comando_;
                        cParametros.TargetSite = "Response: " + SabreCommandRespuesta_.Response;
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
                                consulta.AppendLine("Session Sabre: " + strSesion.ToString());
                                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                            }
                        }
                        catch { }
                        cParametros.StackTrace = consulta.ToString();
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
                    }
                    else
                    {
                        //if (clsValidaciones.GetKeyOrAdd("ValidaLogTransac", "False").ToUpper().Equals("TRUE"))
                        //{
                        cParametros.Id = 1;
                        cParametros.Message = "SabreCommand";
                        cParametros.Severity = clsSeveridad.Moderada;
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
                                consulta.AppendLine("Session Sabre: " + strSesion.ToString());
                                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                            }
                        }
                        catch { }
                        cParametros.StackTrace = consulta.ToString();
                        cParametros.Tipo = clsTipoError.WebServices;
                        try
                        {
                            cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                            cParametros.StackTrace = "Session Sabre: " + strSesion.ToString();
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
                        //}
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.TipoLog = Enum_Error.Log;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Error al ejecutar Comando " + Comando_ + "  Sabre";
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
                        consulta.AppendLine("Session Sabre: " + strSesion.ToString());
                        consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                    }
                }
                catch { }
                cParametros.TargetSite = consulta.ToString();
                try
                {
                    cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    if (cCache != null)
                    {
                        cParametros.Source = "Sesion Local: " + cCache.SessionID.ToString();
                    }
                    else
                    {
                        cParametros.Source = "Sesion Local: No hay cache ";
                    }
                    cParametros.InnerException = "HostCommand: " + Comando_;
                    cParametros.TargetSite = "Response: " + SabreCommandRespuesta_.Response;
                }
                catch
                {
                    cParametros.Source = "Sesion Local: Error ";
                }
                ExceptionHandled.Publicar(cParametros);
            }

            return SabreCommandRespuesta_;
        }
        #endregion

        #region [ DESTRUCTOR ]

        ~clsSabreCommandLLS() { }

        #endregion
    }
}
