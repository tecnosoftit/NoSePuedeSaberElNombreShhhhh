using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;
using Ssoft.ValueObjects;
using WS_SsoftSabre.OTA_TravelItineraryRead;
using WS_SsoftSabre.Air;
using WS_SsoftSabre.Utilidades;

public class Negocios_WebServiceRemark
{
    #region [ CONSTRUCTOR ]

    public Negocios_WebServiceRemark() { }

    #endregion

    #region [ METODOS ]
    /// <summary>
    /// Metodo para incluir el registro de Ssoft
    /// </summary>
    /// <param name="sPNR">Record</param>
    /// <remarks>
    /// Autor:          José Faustino Posas
    /// Company:        Ssoft Colombia
    /// Fecha:          2012-02-06
    /// -------------------
    /// Control de Cambios
    /// -------------------
    /// Autor:          
    /// Fecha:          
    /// Descripción:    
    /// </remarks>
    public static void _ADDRemarkSsoftOpen(string sPNR)
    {
        try
        {
            string sCommand = "I";
            string sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);
             
            sCommand = "*" + sPNR;
            sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);

            sCommand = "ASD/SS/" + sPNR + "/" + DateTime.Now.Year.ToString();
            _ADD(Enum_TipoRemark.Historico, sCommand);

            sCommand = "6WEB";
            sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);

            sCommand = "E";
            sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);
        }
        catch 
        {
        }
    }
    public static void _ADDRemarkSsoft(string sPNR)
    {
        try
        {
            string sCommand = "ASD/SS/" + sPNR + "/" + DateTime.Now.Year.ToString();
            _ADD(Enum_TipoRemark.Historico, sCommand);
        }
        catch
        {
        }
    }
    /// <summary>
    /// Metodo para incluir el segmento futuro despues de cerrada la reserva
    /// Se debe tener en el web.config, la llave SegmentoFuturo, dentro del espacio de credenciales de sabre, si esta en 0, no lo toma
    /// </summary>
    /// <param name="sPNR">Record</param>
    /// <remarks>
    /// Autor:          José Faustino Posas
    /// Company:        Ssoft Colombia
    /// Fecha:          2012-02-14
    /// -------------------
    /// Control de Cambios
    /// -------------------
    /// Autor:          
    /// Fecha:          
    /// Descripción:    
    /// </remarks>
    public static void _ADDSegmentoFuturo(string sPNR)
    {
        clsParametros objParametros = new clsParametros();
        try
        {
            int iDias = 280;
            try
            {
                iDias = clsSesiones.getCredentials().SegmentoFuturo;
            }
            catch { }
            if (iDias.Equals(0))
            {
                objParametros.Id = 0;
                objParametros.Code = "0";
                objParametros.Info = "Segmento no incluido por ser valor 0";
                objParametros.Message = "No icluye OTH";
                objParametros.Severity = clsSeveridad.Baja;
                objParametros.Metodo = "clsRulesFromPrice.getRulesSegment()";
                objParametros.Tipo = clsTipoError.WebServices;
                Ssoft.ManejadorExcepciones.ExceptionHandled.Publicar(objParametros);
            }
            else
            {
                string sCommand = "I";
                string sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);

                sCommand = "*" + sPNR;
                sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);

                DateTime dtm_Fecha_Segmento = DateTime.Now;
                /*SE SUMAN LOS 330 DIAS*/
                dtm_Fecha_Segmento = dtm_Fecha_Segmento.AddDays(iDias);
                /*OBTENEMOS EL MES EN LETRAS, EN INGLES*/
                string str_Mes_Letras = clsValidaciones.RetornaMesLetrasCorto(dtm_Fecha_Segmento.Month.ToString(), "en");
                String str_Dia = dtm_Fecha_Segmento.Day.ToString();
                if (str_Dia.Length == 1)
                    str_Dia = "0" + str_Dia;

                sCommand = "0OTHAAGK1BOG" + str_Dia + str_Mes_Letras.ToUpper();
                sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);

                sCommand = "6WEB";
                sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);

                sCommand = "ET";
                sRespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sCommand);
            }
        }
        catch (Exception Ex)
        {
            objParametros.Id = 0;
            objParametros.Code = "0";
            objParametros.Info = "Segmento no incluido por ser error al ejecutar comando";
            objParametros.Message = Ex.Message.ToString();
            objParametros.Complemento = "No icluye OTH";
            objParametros.Severity = clsSeveridad.Baja;
            objParametros.Metodo = "clsRulesFromPrice.getRulesSegment()";
            objParametros.Tipo = clsTipoError.WebServices;
            Ssoft.ManejadorExcepciones.ExceptionHandled.Publicar(objParametros);
        }
    }
    /// <summary>
    /// Metodo para incluir remarks por tipo, consultando la reserva
    /// </summary>
    /// <param name="param">Tipode Remark</TypeRemark_>
    /// <param name="TextRemark_">Remarks</param>
    /// <param name="sRecord">Record donde se incluira el remarks</param>
    /// <returns>clsParametros, objeto de error</returns>
    /// <remarks>
    /// Autor:          José Faustino Posas
    /// Company:        Ssoft Colombia
    /// Fecha:          2012-02-06
    /// -------------------
    /// Control de Cambios
    /// -------------------
    /// Autor:          
    /// Fecha:          
    /// Descripción:    
    /// </remarks>
    public static clsParametros _ADD(Enum_TipoRemark TypeRemark_, List<string> TextRemark_, string sRecord)
    {
        clsParametros cParametros = new clsParametros();
        try
        {
            if (TextRemark_ != null && TextRemark_.Count > 0)
            {
                clsOTA_TravelItineraryRead ota_TravelItineraryRead = new clsOTA_TravelItineraryRead();
                OTA_TravelItineraryRS ota_TravelItineraryRS = ota_TravelItineraryRead._Sabre_LeerInformacionPNR(sRecord);
                cParametros = new WebService_Remark()._Sabre_AgregarObservaciones(TypeRemark_, TextRemark_);
                Negocios_WebServiceSabreCommand.setER();
                Negocios_WebServiceSession._CerrarSesion();
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
            cParametros.Complemento = "Error al ejecutar Reamrk de Sabre";
            ExceptionHandled.Publicar(cParametros);
        }
        return cParametros;
    }
    public static clsParametros _ADD(Enum_TipoRemark TypeRemark_, List<string> TextRemark_)
    {
        clsParametros cParametros = new clsParametros();
        try
        {
            if (TextRemark_ != null && TextRemark_.Count > 0)
            {
                cParametros = new WebService_Remark()._Sabre_AgregarObservaciones(TypeRemark_, TextRemark_);
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
            cParametros.Complemento = "Error al ejecutar Reamrk de Sabre";
            ExceptionHandled.Publicar(cParametros);
        }
        return cParametros;
    }

    public static clsParametros _ADD(Enum_TipoRemark TypeRemark_, string Remark_)
    {
        clsParametros cParametros = new clsParametros();
        try
        {
            List<string> TextRemark_ = new List<string>();
            TextRemark_.Add(Remark_);

            cParametros = new WebService_Remark()._Sabre_AgregarObservaciones(TypeRemark_, TextRemark_);
        }
        catch (Exception Ex)
        {
            cParametros.Id = 0;
            cParametros.Message = Ex.Message.ToString();
            cParametros.Source = Ex.Source.ToString();
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.StackTrace = Ex.StackTrace.ToString();
            cParametros.Complemento = "Error al ejecutar Reamrk " + Remark_ + "  Sabre";
            ExceptionHandled.Publicar(cParametros);
        }
        return cParametros;
    }
    public static clsParametros _ADD(List<VO_Remarks> vlRemark)
    {
        clsParametros cParametros = new clsParametros();
        try
        {
            cParametros = new WebService_Remark()._Sabre_AgregarObservaciones(vlRemark);
        }
        catch (Exception Ex)
        {
            cParametros.Id = 0;
            cParametros.Message = Ex.Message.ToString();
            cParametros.Source = Ex.Source.ToString();
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.StackTrace = Ex.StackTrace.ToString();
            cParametros.Complemento = "Error al ejecutar Reamrk  Sabre";
            ExceptionHandled.Publicar(cParametros);
        }
        return cParametros;
    }

    public static clsParametros _ADD(List<VO_Remarks> vlRemark, string sRecord)
    {
        clsParametros cParametros = new clsParametros();
        try
        {
            clsOTA_TravelItineraryRead ota_TravelItineraryRead = new clsOTA_TravelItineraryRead();
            OTA_TravelItineraryRS ota_TravelItineraryRS = ota_TravelItineraryRead._Sabre_LeerInformacionPNR(sRecord);
            cParametros = new WebService_Remark()._Sabre_AgregarObservaciones(vlRemark);
            Negocios_WebServiceSabreCommand.setER();
            Negocios_WebServiceSession._CerrarSesion();
        }
        catch (Exception Ex)
        {
            cParametros.Id = 0;
            cParametros.Message = Ex.Message.ToString();
            cParametros.Source = Ex.Source.ToString();
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.StackTrace = Ex.StackTrace.ToString();
            cParametros.Complemento = "Error al ejecutar Reamrk  Sabre";
            ExceptionHandled.Publicar(cParametros);
        }
        return cParametros;
    }
    public static clsParametros _ADDPerfilAg()
    {
        clsParametros cParametros = new clsParametros();
        string sComando = "N*¤";
        string sComandoRemark = "NM";
        try
        {
            cParametros = Negocios_WebServiceSabreCommand._EjecutarComandoGen(sComando);

            //if (clsValidaciones.GetKeyOrAdd("ValidaLogTransac", "False").ToUpper().Equals("TRUE"))
            //{
                cParametros.TipoLog = Enum_Error.Transac;
                cParametros.Complemento = "Ejecucion del comando del perfil de sabre - Agencia";
                cParametros.Metodo = sComando;
                ExceptionHandled.Publicar(cParametros);
            //}
            cParametros = Negocios_WebServiceSabreCommand._EjecutarComandoGen(sComandoRemark);

            //if (clsValidaciones.GetKeyOrAdd("ValidaLogTransac", "False").ToUpper().Equals("TRUE"))
            //{
                cParametros.TipoLog = Enum_Error.Transac;
                cParametros.Complemento = "Ejecucion del comando de confirmacion del perfil de sabre - Agencia";
                cParametros.Metodo = sComandoRemark;
                ExceptionHandled.Publicar(cParametros);
            //}
        }
        catch (Exception Ex)
        {
            cParametros.Id = 0;
            cParametros.TipoLog = Enum_Error.Log;
            cParametros.Complemento = Ex.Message.ToString();
            cParametros.Source = Ex.Source.ToString();
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.StackTrace = Ex.StackTrace.ToString();
            cParametros.Info = "Error al ejecutar comando de sabre para los perfiles - Agencia. Comandos; " + sComando + "  ... Segubdo: " + sComandoRemark;
            ExceptionHandled.Publicar(cParametros);
        }
        return cParametros;
    }
    public static clsParametros _ADDPerfil(string PseudoPerfil, string PerfilEmpresa, string PerfilAdicional, string sSeparador)
    {
        clsParametros cParametros = new clsParametros();
        string sTexto = string.Empty;
        string sValue = "0";
        string sComando = "N*-" + PseudoPerfil + "-" + PerfilEmpresa + sSeparador + PerfilAdicional;
        string sComandoRemark = "NM";
        try
        {
            cParametros = Negocios_WebServiceSabreCommand._EjecutarComandoGen(sComando);

            //if (clsValidaciones.GetKeyOrAdd("ValidaLogTransac", "False").ToUpper().Equals("TRUE"))
            //{
                cParametros.TipoLog = Enum_Error.Transac;
                cParametros.Complemento = "Ejecucion del comando del perfil de sabre - Usuario";
                cParametros.Metodo = sComando;
                ExceptionHandled.Publicar(cParametros);
            //}

            sTexto = "  -" + PerfilAdicional.Substring(0,3);
            sValue = clsValidaciones.RetornaNumero(clsValidacionesVuelos.setResultComado(cParametros.Message, sTexto).Substring(0,5));
            if (!sValue.Equals("0"))
            {
                sComandoRemark += "X" + sValue;
            }
            else
            {
                int iCant = 3;
                int iCantInicio = 3;
                for (int i = 0; i < iCant; i++)
                {
                    if (i.Equals(0))
                    {
                        sTexto = "  -" + PerfilAdicional.Substring(0, iCantInicio) + " ";
                    }
                    else
                    {
                        sTexto = "  -" + PerfilAdicional.Substring(0, iCantInicio) + " " + PerfilAdicional.Substring(iCantInicio + 1, i);
                    }
                    sValue = clsValidaciones.RetornaNumero(clsValidacionesVuelos.setResultComado(cParametros.Message, sTexto).Substring(0, 5));
                    if (!sValue.Equals("0"))
                    {
                        sComandoRemark += "X" + sValue;
                        break;
                    }
                    iCantInicio--;
                }
            }

            cParametros = Negocios_WebServiceSabreCommand._EjecutarComandoGen(sComandoRemark);

            //if (clsValidaciones.GetKeyOrAdd("ValidaLogTransac", "False").ToUpper().Equals("TRUE"))
            //{
                cParametros.TipoLog = Enum_Error.Transac;
                cParametros.Complemento = "Ejecucion del comando de confirmacion del perfil de sabre - Usuario";
                cParametros.Metodo = sComandoRemark;
                ExceptionHandled.Publicar(cParametros);
            //}
        }
        catch (Exception Ex)
        {
            cParametros.Id = 0;
            cParametros.TipoLog = Enum_Error.Log;
            cParametros.Complemento = Ex.Message.ToString();
            cParametros.Source = Ex.Source.ToString();
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.StackTrace = Ex.StackTrace.ToString();
            cParametros.Info = "Error al ejecutar comando de sabre para los perfiles. Comandos; " + sComando + "  ... Segubdo: " + sComandoRemark + "   .... Value: " + sValue + "    .... Texto " + sTexto;
            ExceptionHandled.Publicar(cParametros);
        }
        return cParametros;
    }
    public static clsParametros _ADDPerfil(string PseudoPerfil, string PerfilComunidad)
    {
        clsParametros cParametros = new clsParametros();
        string sTexto = string.Empty;
        string sValue = "0";
        string sComando = "N*-" + PseudoPerfil + "-" + PerfilComunidad;
        string sComandoRemark = "NM";
        try
        {
            cParametros = Negocios_WebServiceSabreCommand._EjecutarComandoGen(sComando);

            //if (clsValidaciones.GetKeyOrAdd("ValidaLogTransac", "False").ToUpper().Equals("TRUE"))
            //{
                cParametros.TipoLog = Enum_Error.Transac;
                cParametros.Complemento = "Ejecucion del comando del perfil de sabre - Comunidad";
                cParametros.Metodo = sComando;
                ExceptionHandled.Publicar(cParametros);
            //}
            sTexto = "  -" + PerfilComunidad.Substring(0, 4);
            sValue = clsValidaciones.RetornaNumero(clsValidacionesVuelos.setResultComado(cParametros.Message, sTexto).Substring(0, 5));
            if (!sValue.Equals("0"))
            {
                sComandoRemark += "X" + sValue;
            }
            //else
            //{
            //    int iCant = 3;
            //    int iCantInicio = 3;
            //    for (int i = 0; i < iCant; i++)
            //    {
            //        if (i.Equals(0))
            //        {
            //            sTexto = "  -" + PerfilComunidad.Substring(0, iCantInicio) + " ";
            //        }
            //        else
            //        {
            //            sTexto = "  -" + PerfilComunidad.Substring(0, iCantInicio) + " " + PerfilComunidad.Substring(iCantInicio + 1, i);
            //        }
            //        sValue = clsValidaciones.RetornaNumero(clsValidacionesVuelos.setResultComado(cParametros.Message, sTexto).Substring(0, 5));
            //        if (!sValue.Equals("0"))
            //        {
            //            sComandoRemark += "X" + sValue;
            //            break;
            //        }
            //        iCantInicio--;
            //    }
            //}

            cParametros = Negocios_WebServiceSabreCommand._EjecutarComandoGen(sComandoRemark);

            //if (clsValidaciones.GetKeyOrAdd("ValidaLogTransac", "False").ToUpper().Equals("TRUE"))
            //{
                cParametros.TipoLog = Enum_Error.Transac;
                cParametros.Complemento = "Ejecucion del comando de confirmacion del perfil de sabre - Comunidad";
                cParametros.Metodo = sComandoRemark;
                ExceptionHandled.Publicar(cParametros);
            //}
        }
        catch (Exception Ex)
        {
            cParametros.Id = 0;
            cParametros.TipoLog = Enum_Error.Log;
            cParametros.Complemento = Ex.Message.ToString();
            cParametros.Source = Ex.Source.ToString();
            cParametros.Tipo = clsTipoError.Library;
            cParametros.Severity = clsSeveridad.Alta;
            cParametros.StackTrace = Ex.StackTrace.ToString();
            cParametros.Info = "Error al ejecutar comando de sabre para los perfiles. Comandos; " + sComando + "  ... Segubdo: " + sComandoRemark + "   .... Value: " + sValue + "    .... Texto " + sTexto;
            ExceptionHandled.Publicar(cParametros);
        }
        return cParametros;
    }
    #endregion

    #region [ DESTRUCTOR ]

    ~Negocios_WebServiceRemark() { }

    #endregion 
}
