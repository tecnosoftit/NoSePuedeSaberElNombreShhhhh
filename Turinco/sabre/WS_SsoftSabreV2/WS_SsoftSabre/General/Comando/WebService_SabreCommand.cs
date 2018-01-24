using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Text;
using WebService_SabreCommandLLS = WS_SsoftSabre.SabreCommandLLS;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;
using Ssoft.ValueObjects;

public class WebService_SabreCommand
{
    #region [ ATRIBUTOS ]

    protected string session_;

    #endregion

    #region [ CONSTRUCTOR ]

    public WebService_SabreCommand()
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

    public WebService_SabreCommandLLS.SabreCommandLLSRS _Sabre_EjecutarComando(string Comando_)
    {
        WebService_SabreCommandLLS.SabreCommandLLSRS SabreCommandRespuesta_ = new WebService_SabreCommandLLS.SabreCommandLLSRS();
        clsParametros cParametros = new clsParametros();
        VO_Credentials objvo_Credentials = clsSesiones.getCredentials();
        StringBuilder consulta = new StringBuilder();
        cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
        try
        {

            WebService_SabreCommandLLS.MessageHeader Mensaje_ = WS_SsoftSabre.Air.clsSabreBase.SabreCommandLLS();

            if (Mensaje_ != null)
            {
                WebService_SabreCommandLLS.Security Seguridad_ = new WebService_SabreCommandLLS.Security();
                Seguridad_.BinarySecurityToken = Session_;
                WebService_SabreCommandLLS.SabreCommandLLSRQ SabreCommand_ = new WebService_SabreCommandLLS.SabreCommandLLSRQ();
                WebService_SabreCommandLLS.SabreCommandLLSRQRequest oSabreComandReques = new WS_SsoftSabre.SabreCommandLLS.SabreCommandLLSRQRequest();
                oSabreComandReques.HostCommand = Comando_;
                SabreCommand_.Request = oSabreComandReques;
                SabreCommand_.Version = WS_SsoftSabre.Air.clsSabreBase.SABRE_VERSION_SABRECOMMANDLLS;
                WebService_SabreCommandLLS.SabreCommandLLSService SabreCommandServicio_ = new WebService_SabreCommandLLS.SabreCommandLLSService();
                SabreCommandServicio_.MessageHeaderValue = Mensaje_;
                SabreCommandServicio_.SecurityValue = Seguridad_;
                SabreCommandRespuesta_ = SabreCommandServicio_.SabreCommandLLSRQ(SabreCommand_);

                if (SabreCommandRespuesta_.ErrorRS != null)
                {
                    cParametros.Id = 0;
                    cParametros.Code = SabreCommandRespuesta_.ErrorRS.Errors.Error.ErrorCode;
                    cParametros.Info = SabreCommandRespuesta_.ErrorRS.Errors.Error.ErrorInfo.Message;
                    cParametros.Message = SabreCommandRespuesta_.ErrorRS.Errors.Error.ErrorMessage;
                    cParametros.Severity = SabreCommandRespuesta_.ErrorRS.Errors.Error.Severity;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.Complemento = "HostCommand: " + Comando_;
                    cParametros.ViewMessage.Add("Error al ejecutar comando");
                    cParametros.Sugerencia.Add("");
                    cParametros.Message = SabreCommandRespuesta_.ErrorRS.Errors.Error.ErrorMessage;
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
                else
                {
                    cParametros.Id = 1;
                    cParametros.TipoLog = Enum_Error.Transac;
                    cParametros.Message = SabreCommandRespuesta_.ToString();
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.Complemento = "HostCommand: " + Comando_;
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
            cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            cParametros.Complemento = "Error al ejecutar Comando " + Comando_ + "  Sabre";
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

        return SabreCommandRespuesta_;
    }

    #endregion

    #region [ DESTRUCTOR ]

    ~WebService_SabreCommand() { }

    #endregion
}
