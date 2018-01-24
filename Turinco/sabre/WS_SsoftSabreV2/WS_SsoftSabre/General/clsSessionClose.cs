using System;
using System.Collections.Generic;
using System.Text;
using WS_SsoftSabre.SessionClose;
using WS_SsoftSabre.Air;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;

namespace WS_SsoftSabre.General
{
    public class clsSessionClose
    {
        public void setCerrar(string strSesion)
        {
            SessionCloseRS oServicioCerrar = new SessionCloseRS();
            try
            {
                MessageHeader oMensaje = clsSabreBase.SessionClose();
                clsParametros cParametros = new clsParametros();
                if (oMensaje != null)
                {
                    Security oSeguridad = new Security();
                    oSeguridad.BinarySecurityToken = strSesion;

                    SessionCloseRQ oCerrarSesion = new SessionCloseRQ();
                    SessionCloseRQPOS oCerrarSesionPos = new SessionCloseRQPOS();
                    SessionCloseRQPOSSource oCerrarSesionSource = new SessionCloseRQPOSSource();

                    oCerrarSesionPos.Source = oCerrarSesionSource;
                    oCerrarSesion.POS = oCerrarSesionPos;

                    SessionCloseRQService oServicio = new SessionCloseRQService();

                    oServicio.MessageHeaderValue = oMensaje;
                    oServicio.SecurityValue = oSeguridad;

                    oServicioCerrar = oServicio.SessionCloseRQ(oCerrarSesion);

                    if (oServicioCerrar.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = oServicioCerrar.Errors.Error.ErrorCode;
                        cParametros.Info = oServicioCerrar.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = oServicioCerrar.Errors.Error.ErrorMessage;
                        cParametros.Severity = oServicioCerrar.Errors.Error.Severity;
                        cParametros.Complemento = "HostCommand: ";
                        cParametros.Metodo = "CerrarSesion";
                        cParametros.Tipo = clsTipoError.WebServices;
                        ExceptionHandled.Publicar(cParametros);
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.TipoLog = Enum_Error.Transac;
                        cParametros.Message = oServicioCerrar.Success.ToString();
                        cParametros.Metodo = "_Remark_Observaciones";
                        cParametros.Complemento = "HostCommand: ";
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
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Complemento = "Error al abrir la sesion de sabre";
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Ex = Ex;
                ExceptionHandled.Publicar(cParametros);
            }
        }

        public SessionCloseRS CerrarSesion(string strSesion)
        {
            SessionCloseRS oServicioCerrar = new SessionCloseRS();
            try
            {
                MessageHeader oMensaje = clsSabreBase.SessionClose();

                if (oMensaje != null)
                {
                    Security oSeguridad = new Security();
                    oSeguridad.BinarySecurityToken = strSesion;

                    SessionCloseRQ oCerrarSesion = new SessionCloseRQ();
                    SessionCloseRQPOS oCerrarSesionPos = new SessionCloseRQPOS();
                    SessionCloseRQPOSSource oCerrarSesionSource = new SessionCloseRQPOSSource();

                    oCerrarSesionPos.Source = oCerrarSesionSource;

                    oCerrarSesion.POS = oCerrarSesionPos;

                    SessionCloseRQService oServicio = new SessionCloseRQService();

                    oServicio.MessageHeaderValue = oMensaje;
                    oServicio.SecurityValue = oSeguridad;

                    oServicioCerrar = oServicio.SessionCloseRQ(oCerrarSesion);

                    if (oServicioCerrar.Errors != null)
                    {
                        throw new Exception(oServicioCerrar.Errors.Error.ErrorMessage);
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Complemento = "Error al abrir la sesion de sabre";
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Ex = Ex;
                ExceptionHandled.Publicar(cParametros);
            }
            return oServicioCerrar;
        }
    }
}
