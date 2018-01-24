using System;
using System.Collections.Generic;
using System.Text;
using WS_SsoftSabre.SessionCreateRQ;
using WS_SsoftSabre.Air;
using Ssoft.ValueObjects;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;
using System.Web;
using System.Web.UI;

namespace WS_SsoftSabre.General
{
    public class clsSessionCreateRQ
    {
        public string getSesion(VO_SessionCreateRQ vo_SessionCreateRQ)
        {
            string strSession = null;
            MessageHeader oMensaje = clsSabreBase.SessionCreateRQ();
            clsParametros cParametros = new clsParametros();

            if (oMensaje != null)
            {
                string Usuario_ = vo_SessionCreateRQ.StrUsuario;
                string Clave_ = vo_SessionCreateRQ.StrClave;
                string IPCC_ = vo_SessionCreateRQ.StrIpcc;
                string PCC_ = vo_SessionCreateRQ.StrPcc;
                string Dominio_ = vo_SessionCreateRQ.StrDominio;

                Security Seguridad_ = new Security();
                SecurityUsernameToken SeguridadUsuario_ = new SecurityUsernameToken();

                SeguridadUsuario_.Username = Usuario_;
                SeguridadUsuario_.Password = Clave_;
                SeguridadUsuario_.Organization = IPCC_;
                SeguridadUsuario_.Domain = Dominio_;
                Seguridad_.UsernameToken = SeguridadUsuario_;

                SessionCreateRQ.SessionCreateRQ CrearSesion_ = new SessionCreateRQ.SessionCreateRQ();
                SessionCreateRQPOS CrearSesionPos_ = new SessionCreateRQPOS();
                SessionCreateRQPOSSource CrearSesionSource_ = new SessionCreateRQPOSSource();

                CrearSesionSource_.PseudoCityCode = PCC_;
                CrearSesionPos_.Source = CrearSesionSource_;
                CrearSesion_.POS = CrearSesionPos_;

                SessionCreateRQService CrearSesionServicio_ = new SessionCreateRQService();

                CrearSesionServicio_.MessageHeaderValue = oMensaje;
                CrearSesionServicio_.SecurityValue = Seguridad_;

                SessionCreateRS SessionResultado_ = CrearSesionServicio_.SessionCreateRQ(CrearSesion_);

                if (SessionResultado_.Errors != null)
                {
                    string sMensaje = clsValidaciones.GetKeyOrAdd("sMensajeSesionSabre", "En este momento hay mas de 2000 personas consultado nuestras Promociones!!!");
                    string sSugerencia = clsValidaciones.GetKeyOrAdd("sSugerenciaSesionSabre", "Por favor intente de nuevo en 5 minutos!!!");
                    /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                    cParametros.Id = 0;
                    cParametros.Message = SessionResultado_.Errors.Error.ErrorMessage;
                    cParametros.Info = SessionResultado_.Errors.Error.ErrorInfo.Message;
                    cParametros.Code = SessionResultado_.Errors.Error.ErrorCode;
                    cParametros.Complemento = "Error al abrir la sesion de sabre";
                    cParametros.Severity = SessionResultado_.Errors.Error.Severity;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.ViewMessage.Add(sMensaje);
                    cParametros.Sugerencia.Add(sSugerencia);
                    clsSesiones.setParametrosError(cParametros);
                    ExceptionHandled.Publicar(cParametros);
                    clsValidaciones.RedirectPagina("ErrorBusqueda.aspx", true);
                }
                else
                {
                    Seguridad_ = CrearSesionServicio_.SecurityValue;
                    strSession = Seguridad_.BinarySecurityToken;

                    cParametros.Id = 1;
                    cParametros.TipoLog = Enum_Error.Transac;
                    cParametros.Message = "Cerrar Sesion";
                    cParametros.Metodo = "_Cerrar Sesion";
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Severity = clsSeveridad.Moderada;
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
            return strSession;
        }
    }
}
