using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using WebService_SabreCommandLLS = WS_SsoftSabre.SabreCommandLLS;
using WS_SsoftSabre.Air;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;
using System.Web.UI;
using System.Text;

public class Negocios_WebServiceSabreCommand
{
    #region [ CONSTRUCTOR ]

    public Negocios_WebServiceSabreCommand() { }

    #endregion

    #region [ METODOS ]

    public static string _EjecutarComando(string Comando_)
    {/*EJECUTAMOS EL METODO BASE*/
        return ComandoGen(Comando_);
    }

    public static void _EjecutarComandoSinRetorno(string Comando_)
    {/*EJECUTAMOS EL METODO BASE*/
        try
        {
            string sComando = ComandoGen(Comando_);
        }
        catch { }
    }

    public static clsParametros _EjecutarComandoGen(string Comando_)
    {/*EJECUTAMOS EL METODO BASE*/
        return Comando(Comando_);
    }

    private static clsParametros Comando(string Comando_)
    {
        /*METODO BASE QUE SIRVE PARA EJECUTAR COMANDOS*/
        clsParametros cParametros = new clsParametros();

        try
        {
            clsSabreCommandLLS objClsSabreCommandLLS = new clsSabreCommandLLS();
            /*ASIGNAMOS LA SESSION DE SABRE*/
            objClsSabreCommandLLS.StrSesion = AutenticacionSabre.GET_SabreSession();

            if (objClsSabreCommandLLS.StrSesion != null)
            {
                WebService_SabreCommandLLS.SabreCommandLLSRS SabreCommandRespuesta_ = objClsSabreCommandLLS._Sabre_EjecutarComando(Comando_);

                if (SabreCommandRespuesta_ != null)
                {
                    if (SabreCommandRespuesta_.Response != null)
                    {
                        cParametros.Id = 1;
                        cParametros.TipoLog = Enum_Error.Transac;
                        cParametros.Message = SabreCommandRespuesta_.Response;
                        cParametros.Tipo = clsTipoError.Library;
                        cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        cParametros.Info = "Comando Sabre: " + Comando_; 
                        cParametros.Complemento = "Response: " + SabreCommandRespuesta_.Response;
                        ExceptionHandled.Publicar(cParametros);
                    }
                    else
                    {
                        WebService_SabreCommandLLS.SabreCommandLLSRSErrorRSErrorsError SabreErrores_ = SabreCommandRespuesta_.ErrorRS.Errors.Error;
                        WebService_SabreCommandLLS.SabreCommandLLSRSErrorRSErrorsErrorErrorInfo SabreErroresInfo_ = SabreErrores_.ErrorInfo;

                        if (SabreErrores_ != null)
                        {
                            cParametros.Id = 0;
                            cParametros.TipoLog = Enum_Error.Log;
                            cParametros.Message = SabreErrores_.ErrorMessage;
                            cParametros.Code = SabreErrores_.ErrorCode;
                            cParametros.Info = SabreErrores_.ErrorInfo.Message;
                            cParametros.Severity = SabreErrores_.Severity;
                            cParametros.Tipo = clsTipoError.Library;
                            cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                            cParametros.Complemento = "Error al ejecutar Comando " + Comando_ + "  Sabre";
                            ExceptionHandled.Publicar(cParametros);
                            try
                            {
                                setEmailError(cParametros, "Error al ejecutar comando");
                            }
                            catch { }
                        }
                    }
                }
            }
            else
            {
                cParametros.Id = 0;
                cParametros.Message = "Sesion no iniciada al ejecutar Comando " + Comando_ + "  Sabre";
                cParametros.TipoLog = Enum_Error.Log;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.ViewMessage.Add("Sesion de sabre no iniciada!!!");
                cParametros.Sugerencia.Add("Por favor verifique las credenciales!!!");
                ExceptionHandled.Publicar(cParametros);
                try
                {
                    setEmailError(cParametros, "Error al ejecutar comando");
                }
                catch { }
            }
        }
        catch (Exception Ex)
        {
            cParametros.Id = 0;
            cParametros.Message = Ex.Message.ToString();
            cParametros.Source = Ex.Source.ToString();
            cParametros.TipoLog = Enum_Error.Log;
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
            cParametros.StackTrace = Ex.StackTrace.ToString();
            cParametros.Complemento = "Error al ejecutar Comando " + Comando_ + "  Sabre";
            ExceptionHandled.Publicar(cParametros);
            if (cParametros.Message.Contains("limit of Host TAs allocated"))
            {
                cParametros.ViewMessage.Add("En este momento hay mas de 2000 usuarios consultado nuestras Promociones!!!");
                cParametros.Sugerencia.Add("Por favor intente de nuevo en 5 minutos!!!");
                clsSesiones.setParametrosError(cParametros);
                ExceptionHandled.Publicar(cParametros);
                Page PageActual = (Page)HttpContext.Current.Handler;
                clsValidaciones.RedirectPagina("ErrorBusqueda.aspx", true);
            }
            try
            {
                setEmailError(cParametros, "Error al Ejecutar Commando");
            }
            catch { }
        }
        return cParametros;
    }
    private static string ComandoGen(string Comando_)
    {
        /*METODO BASE QUE SIRVE PARA EJECUTAR COMANDOS*/
        string Datos_ = String.Empty;
        clsParametros cParametros = new clsParametros();

        try
        {
            clsSabreCommandLLS objClsSabreCommandLLS = new clsSabreCommandLLS();
            /*ASIGNAMOS LA SESSION DE SABRE*/
            objClsSabreCommandLLS.StrSesion = AutenticacionSabre.GET_SabreSession();
            if (objClsSabreCommandLLS.StrSesion != null)
            {
                WebService_SabreCommandLLS.SabreCommandLLSRS SabreCommandRespuesta_ = objClsSabreCommandLLS._Sabre_EjecutarComando(Comando_);

                if (SabreCommandRespuesta_ != null)
                {
                    if (SabreCommandRespuesta_.Response != null)
                    {
                        Datos_ = SabreCommandRespuesta_.Response;
                        cParametros.Id = 1;
                        cParametros.TipoLog = Enum_Error.Transac;
                        cParametros.Message = SabreCommandRespuesta_.Response;
                        cParametros.Tipo = clsTipoError.Library;
                        cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        cParametros.Info = "Comando Sabre: " + Comando_;
                        cParametros.Complemento = "Response: " + SabreCommandRespuesta_.Response;
                        ExceptionHandled.Publicar(cParametros);
                    }
                    else
                    {
                        WebService_SabreCommandLLS.SabreCommandLLSRSErrorRSErrorsError SabreErrores_ = SabreCommandRespuesta_.ErrorRS.Errors.Error;
                        WebService_SabreCommandLLS.SabreCommandLLSRSErrorRSErrorsErrorErrorInfo SabreErroresInfo_ = SabreErrores_.ErrorInfo;

                        if (SabreErrores_ != null)
                        {
                            cParametros.Id = 0;
                            cParametros.Message = SabreErrores_.ErrorMessage;
                            cParametros.TipoLog = Enum_Error.Log;
                            cParametros.Tipo = clsTipoError.Library;
                            cParametros.Severity = clsSeveridad.Alta;
                            cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                            cParametros.Complemento = "Error al ejecutar Comando " + Comando_ + "  Sabre";
                            ExceptionHandled.Publicar(cParametros);
                            try
                            {
                                setEmailError(cParametros, "Error al Ejecutar Commando");
                            }
                            catch { }
                        }
                    }
                }
            }
            else
            {
                cParametros.Id = 0;
                cParametros.Message = "Sesion no iniciada al ejecutar Comando " + Comando_ + "  Sabre";
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.TipoLog = Enum_Error.Log;
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.ViewMessage.Add("Sesion de sabre no iniciada!!!");
                cParametros.Sugerencia.Add("Por favor verifique las credenciales!!!");
                ExceptionHandled.Publicar(cParametros);
                Datos_ = "Sesion de sabre no iniciada, Por favor verifique las credenciales";
                try
                {
                    setEmailError(cParametros, "Error al Ejecutar Commando");
                }
                catch { }
            }
        }
        catch (Exception Ex)
        {
            cParametros.Id = 0;
            cParametros.Message = Ex.Message.ToString();
            cParametros.Source = Ex.Source.ToString();
            cParametros.TipoLog = Enum_Error.Log;
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.StackTrace = Ex.StackTrace.ToString();
            cParametros.Complemento = "Error al ejecutar Comando " + Comando_ + "  Sabre";
            ExceptionHandled.Publicar(cParametros);
            if (cParametros.Message.Contains("limit of Host TAs allocated"))
            {
                cParametros.ViewMessage.Add("En este momento hay mas de 2000 usuarios consultado nuestras Promociones!!!");
                cParametros.Sugerencia.Add("Por favor intente de nuevo en 5 minutos!!!");
                clsSesiones.setParametrosError(cParametros);
                ExceptionHandled.Publicar(cParametros);
                clsCache cCache = new csCache().cCache();
                Page PageActual = (Page)HttpContext.Current.Handler;
                clsValidaciones.RedirectPagina("ErrorBusqueda.aspx", true);
            }
            try
            {
                setEmailError(cParametros, "Error al Ejecutar Commando");
            }
            catch { }
            Datos_ = null;
        }
        return Datos_;
    }

    public static void setPQ()
    {
        _EjecutarComandoSinRetorno("WP");
      
        _EjecutarComandoSinRetorno("PQ");
      
        _EjecutarComandoSinRetorno("7T-");
       
    }

    public static void setER()
    {
        _EjecutarComandoSinRetorno("ER");
    }

    public static string setPcc(string sPcc)
    {
        return _EjecutarComando("AAA" + sPcc);
    }

    public static void setQP(String sReserva)
    {
        Ssoft.ValueObjects.VO_Credentials objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();
        setQP(sReserva, objvo_Credentials.QNumber);
    }
    public static void setQP(string sReserva, string QNumber)
    {
        _EjecutarComandoSinRetorno("QP/" + QNumber + "/11*" + sReserva);
    }
    /*---------------CANCELAR-----------------*/
    public static bool _CancelarReserva(String strRecord, string COMANDO_)
    {
        bool Result_ = false;

        try
        {
            Result_ = _Sabre_CancelarReserva(COMANDO_);
            new clsEndTransactionLLS()._CerrarReserva(ref strRecord);
            Negocios_WebServiceSession._CerrarSesion();
        }
        catch (Exception Ex)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 0;
            cParametros.Message = Ex.Message.ToString();
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Media;
            cParametros.Complemento = "Error al cerrar reserva en sabre";
            try
            {
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
            }
            catch
            {
                cParametros.Metodo = "Cerrar rserva";
            }
            ExceptionHandled.Publicar(cParametros);
            Result_ = false;
        }
        return Result_;
    }

    private static bool _Sabre_CancelarReserva(string COMANDO_)
    {
        bool Result_ = false;

        try
        {
            string Comando_ = _EjecutarComando(COMANDO_);
            Result_ = _CerrarReserva();
        }
        catch (Exception Ex)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 0;
            cParametros.Message = Ex.Message.ToString();
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Media;
            cParametros.Complemento = "Error al cerrar reserva en sabre";
            try
            {
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
            }
            catch
            {
                cParametros.Metodo = "Cerrar rserva";
            }
            ExceptionHandled.Publicar(cParametros);
            Result_ = false;
        }

        return Result_;
    }

    /*---------------Abrir Reserva -----------------*/
    public static bool _AbrirReserva(string sPNR)
    {
        bool Result_ = false;

        try
        {
            string sCommand = "I";
            string sRespuesta = _EjecutarComando(sCommand);

            sCommand = "*" + sPNR;
            sRespuesta = _EjecutarComando(sCommand);
            Result_ = true;
        }
        catch 
        {
        }
        return Result_;
    }

    public static bool _CerrarReserva()
    {
        bool Result_ = false;

        try
        {
            string sCommand = "6WEB";
            string sRespuesta = _EjecutarComando(sCommand);

            sCommand = "ER";
            sRespuesta = _EjecutarComando(sCommand);
            Result_ = true;
        }
        catch 
        {
        }

        return Result_;
    }
    public static void setEmailError(clsParametros objParametros, string sAsunto)
    {
        try
        {
            string bEnvioError = clsValidaciones.GetKeyOrAdd("bEnvioErrorSabre", "False");
            if (bEnvioError.ToUpper().Equals("TRUE"))
            {
                StringBuilder consulta = new StringBuilder();
                try
                {
                    consulta.AppendLine("---- Code: ");
                    consulta.AppendLine(objParametros.Code);
                    consulta.AppendLine("---- Mensaje:");
                    consulta.AppendLine(objParametros.Message);
                    consulta.AppendLine("---- Metodo:");
                    consulta.AppendLine(objParametros.Complemento);
                    consulta.AppendLine("---- Complemento:");
                    consulta.AppendLine(objParametros.Metodo);
                    consulta.AppendLine("---- Info:");
                    consulta.AppendLine(objParametros.Info);
                    sAsunto = "ERROR SABRE - " + sAsunto;
                }
                catch { }

                clsEmail cEmail = new clsEmail();
                string sCC = clsValidaciones.GetKeyOrAdd("strEmailCC", "amelo@ssoftcolombia.com");
                string sTo = clsValidaciones.GetKeyOrAdd("strEmailTo", "achaves@ssoftcolombia.com");
                string sCCO = clsValidaciones.GetKeyOrAdd("strEmailCCO", "fposas@ssoftcolombia.com");
                string sFrom = clsValidaciones.GetKeyOrAdd("strEmailEnvio", "info@ssoftcolombia.com");

                cEmail.EnviarMensaje(consulta.ToString(),
                        sAsunto,
                        OperacionEmail.Email,
                        sTo,
                        sCC,
                        sCCO,
                        FormatMail.Text, sFrom);
            }
        }
        catch (Exception Ex)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 0;
            cParametros.Message = Ex.Message.ToString();
            cParametros.Source = Ex.Source.ToString();
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Moderada;
            cParametros.StackTrace = Ex.StackTrace.ToString();
            cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ExceptionHandled.Publicar(cParametros);
        }
    }

    #endregion

    #region [ DESTRUCTOR ]

    ~Negocios_WebServiceSabreCommand() { }

    #endregion
}
