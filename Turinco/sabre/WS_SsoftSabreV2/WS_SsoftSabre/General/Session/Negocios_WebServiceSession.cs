using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Xml;
using WebService_SessionClose = WS_SsoftSabre.SessionClose;
using Ssoft.ValueObjects;
using WS_SsoftSabre.General;
using Ssoft.ManejadorExcepciones;
using WS_SsoftSabre.SabreCommandLLS;
using WS_SsoftSabre.Air;
using Ssoft.Utils;
using System.Text;

public class Negocios_WebServiceSession
{
    #region [ CONSTRUCTOR ]

    public Negocios_WebServiceSession() { }

    #endregion

    #region [ METODOS ]

    public static string _CrearSesion()
    {
        string Session_ = null;
        clsCache cCache = new csCache().cCache();

        StringBuilder consulta = new StringBuilder();
        clsParametros cParametros = new clsParametros();
        VO_Credentials objvo_Credentials = clsSesiones.getCredentials();
        string sMensaje = clsValidaciones.GetKeyOrAdd("sMensajeSesionSabre", "En este momento hay mas de 2000 personas consultado nuestras Promociones!!!");
        string sSugerencia = clsValidaciones.GetKeyOrAdd("sSugerenciaSesionSabre", "Por favor intente de nuevo en 5 minutos!!!");
        try
        {
            /*ASIGNAMOS LOS CRITERIOS BASICOS PARA CREAR LA SESSION*/
            VO_SessionCreateRQ vo_SessionCreateRQ = new VO_SessionCreateRQ(objvo_Credentials.User,
                                                                            objvo_Credentials.Password,
                                                                            objvo_Credentials.Ipcc,
                                                                            objvo_Credentials.Pcc,
                                                                            objvo_Credentials.Dominio
                                                                            );

            clsSessionCreateRQ objclsSessionCreateRQ = new clsSessionCreateRQ();
            try
            {
                Session_ = objclsSessionCreateRQ.getSesion(vo_SessionCreateRQ);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.Complemento = "_CrearSesion";
                cParametros.Info = cParametros.Message;
                consulta.AppendLine("Credenciales: ");
                try
                {
                    if (objvo_Credentials != null)
                    {
                        consulta.AppendLine("User: " + objvo_Credentials.User);
                        consulta.AppendLine("Password: " + objvo_Credentials.Password);
                        consulta.AppendLine("Ipcc: " + objvo_Credentials.Ipcc);
                        consulta.AppendLine("Pcc: " + objvo_Credentials.Pcc);
                        consulta.AppendLine("Dominio: " + objvo_Credentials.Dominio);
                        consulta.AppendLine("Session Sabre: " + Session_.ToString());
                        consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                    }
                }
                catch { }
                cParametros.StackTrace = consulta.ToString();
                try
                {
                    if(Session_ != null)
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
                try
                {
                    if (cCache != null)
                    {
                        cParametros.ErrorConfigura[0] = cCache.Empresa;
                    }
                    else
                    {
                        cParametros.ErrorConfigura[0] = "0";
                    }
                }
                catch { }
                if (cParametros.Message.Contains("limit of Host TAs allocated"))
                {
                    cParametros.ViewMessage.Add(sMensaje);
                    cParametros.Sugerencia.Add(sSugerencia);
                    cParametros.Code = "109";
                }
                else
                {
                    cParametros.Code = "106";
                }
                cParametros.ValidaInfo = false;
                clsSesiones.setParametrosError(cParametros);
                ExceptionHandled.Publicar(cParametros);
                clsValidaciones.RedirectPagina("ErrorBusqueda.aspx", true);
            }

            VO_SabreCommandLLSRS vo = new VO_SabreCommandLLSRS();
            vo.BCDATA = true;
            vo.StrComando = "AAA" + objvo_Credentials.Pcc;

            clsSabreCommandLLS oclsSabreCommandLLS = new clsSabreCommandLLS();
            oclsSabreCommandLLS.StrSesion = Session_;
            SabreCommandLLSRS respuesta = oclsSabreCommandLLS.getEjecutarComando(vo);
            //if (clsValidaciones.GetKeyOrAdd("ValidaLogTransac", "False").ToUpper().Equals("TRUE"))
            //{
                consulta.AppendLine("Credenciales: ");
                if (objvo_Credentials != null)
                {
                    consulta.AppendLine("User: " + objvo_Credentials.User);
                    consulta.AppendLine("Password: " + objvo_Credentials.Password);
                    consulta.AppendLine("Ipcc: " + objvo_Credentials.Ipcc);
                    consulta.AppendLine("Pcc: " + objvo_Credentials.Pcc);
                    consulta.AppendLine("Dominio: " + objvo_Credentials.Dominio);
                    consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                }
                cParametros.Id = 1;
                cParametros.TipoLog = Enum_Error.Transac;
                cParametros.Message = "Ejecucion del comando AAA y perfil de agencia";
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Severity = clsSeveridad.Baja;
                cParametros.TargetSite = "Comando: " + vo.StrComando;
                cParametros.StackTrace = "Respuesta" + respuesta.Response;
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.Complemento = consulta.ToString();
                try
                {
                    if(Session_ != null)
                        cParametros.Info = "Session Sabre: " + Session_.ToString();
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
        catch (Exception Ex)
        {
            consulta.AppendLine("Credenciales: ");
            if (objvo_Credentials != null)
            {
                consulta.AppendLine("User: " + objvo_Credentials.User);
                consulta.AppendLine("Password: " + objvo_Credentials.Password);
                consulta.AppendLine("Ipcc: " + objvo_Credentials.Ipcc);
                consulta.AppendLine("Pcc: " + objvo_Credentials.Pcc);
                consulta.AppendLine("Dominio: " + objvo_Credentials.Dominio);
                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
            }
            cParametros.Id = 0;
            cParametros.TipoLog = Enum_Error.Log;
            cParametros.Message = Ex.Message.ToString();
            cParametros.Source = Ex.Source.ToString();
            cParametros.Tipo = clsTipoError.WebServices;
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            cParametros.Complemento = consulta.ToString();
            cParametros.Info = cParametros.Message;
            try
            {
                if (cCache != null)
                {
                    cParametros.ErrorConfigura[0] = cCache.Empresa;
                }
                else
                {
                    cParametros.ErrorConfigura[0] = "0";
                }
            }
            catch { }
            if (cParametros.Message.Contains("limit of Host TAs allocated"))
            {
                cParametros.ViewMessage.Add(sMensaje);
                cParametros.Sugerencia.Add(sSugerencia);
                cParametros.Code = "109";
            }
            else
            {
                cParametros.Code = "106";
            }
            cParametros.ValidaInfo = false;
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
            clsSesiones.setParametrosError(cParametros);
            ExceptionHandled.Publicar(cParametros);
            clsValidaciones.RedirectPagina("ErrorBusqueda.aspx", true);
        }
        return Session_;
    }

    public static void _CerrarSesion()
    {
        clsCache cCache = new csCache().cCache();
        clsParametros cParametros = new clsParametros();
        string sMensaje = clsValidaciones.GetKeyOrAdd("sMensajeSesionSabre", "En este momento hay mas de 2000 personas consultado nuestras Promociones!!!");
        string sSugerencia = clsValidaciones.GetKeyOrAdd("sSugerenciaSesionSabre", "Por favor intente de nuevo en 5 minutos!!!");
        try
        {
            clsSessionClose objClsSessionClose = new clsSessionClose();
            /*CERRAMOS LA SESSION OBTENIENDO LA SESSION DE SABRE*/
            WebService_SessionClose.SessionCloseRS CerrarSesionRespuesta_ = null;
            try
            {
                string sValidaSesion = AutenticacionSabre.GET_SabreSessionValida();
                if(sValidaSesion != null)
                    CerrarSesionRespuesta_ = objClsSessionClose.CerrarSesion(sValidaSesion);
            }
            catch 
            {
            }

            if (CerrarSesionRespuesta_ != null)
            {
                if (CerrarSesionRespuesta_.status.CompareTo("Approved") == 0)
                {
                    AutenticacionSabre.SET_SabreSession(null);
                }
                else
                {
                    WebService_SessionClose.SessionCloseRSErrorsError Error_ = CerrarSesionRespuesta_.Errors.Error;
                    WebService_SessionClose.SessionCloseRSErrorsErrorErrorInfo ErrorInfo_ = Error_.ErrorInfo;

                    if (CerrarSesionRespuesta_.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Message = CerrarSesionRespuesta_.Errors.Error.ErrorMessage;
                        cParametros.Code = CerrarSesionRespuesta_.Errors.Error.ErrorCode;
                        cParametros.Info = CerrarSesionRespuesta_.Errors.Error.ErrorInfo.Message;
                        cParametros.Severity = CerrarSesionRespuesta_.Errors.Error.Severity;
                        cParametros.Tipo = clsTipoError.Library;
                        try
                        {
                            if (cCache != null)
                            {
                                cParametros.ErrorConfigura[0] = cCache.Empresa;
                            }
                            else
                            {
                                cParametros.ErrorConfigura[0] = "0";
                            }
                        }
                        catch { }
                        cParametros.ValidaInfo = true;
                        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        cParametros.MessageBD = true;
                        cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
                        cParametros.Complemento = "_CerrarSesion";
                        ExceptionHandled.Publicar(cParametros);
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.TipoLog = Enum_Error.Transac;
                        cParametros.Message = CerrarSesionRespuesta_.Success.ToString();
                        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        cParametros.Complemento = "HostCommand: ";
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Severity = clsSeveridad.Moderada;
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
                        cParametros.TipoLog = Enum_Error.Log;
                    }
                }
            }
        }
        catch 
        {
        }
    }

    #endregion

    #region [ DESTRUCTOR ]

    ~Negocios_WebServiceSession() { }

    #endregion
}
