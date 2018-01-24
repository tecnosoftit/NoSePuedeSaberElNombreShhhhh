using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Diagnostics;

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Ssoft.Utils;
using System.Data;
using Ssoft.Sql;
using System.Threading;

namespace Ssoft.ManejadorExcepciones
{
    /// <summary>
    /// Encargado del Manejo de Excepciones, se configura por medio del Web.Config manejando 3 tipos de excepcion:
    /// Txt(archivos plano), Log (Visor de Sucesos), Xml (archivos Xml).
    /// Tambien se configura por medio del Web.Config la ruta donde se colocaran los archivos(txt,xml).
    /// </summary>
    /// <remarks>
    /// Autor: Ssoft - Cmazo
    /// URL: http://www.consultinltda.com
    /// Email: consultinltda@hotmail.com
    /// Fecha: 2006-04-03
    /// </remarks>
    public class ExceptionHandled
    {
        string _gstrTipoExcepcion = string.Empty;
        public ExceptionHandled() { }

        /// <summary>
        /// Recibe 2 parametros y selecciona el Tipo de Excepción que se trabajará(txt,xml,log).
        /// en Caso que no se tenga permiso para el manejo del log o que sea mal configurado
        /// el Web.Config se manejara el tipo de excepción Txt.
        /// </summary>
        /// <param name="excepcion">Mensaje del Error provocado</param>
        /// <param name="tipoExcepcion">Tipo de excepcion a registrar en el sistema</param>
        /// <remarks>
        /// Autor: Ssoft - Cmazo
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-04-03
        /// </remarks>
        public static void Publicar(Exception excepcion, string tipoExcepcion)
        {
        //tipo de excepción (txt,log.xml)
        begin:
            switch (tipoExcepcion.ToLower().Trim())
            {
                case "txt":
                    EventoTexto(excepcion);
                    break;
                case "log":
                    EventoVisorSucesos(excepcion);
                    break;
                default:
                    tipoExcepcion = "txt";
                    goto begin;
            }
        }

        public static void Publicar(clsParametros Excepcion, string tipoExcepcion)
        {
            //tipo de excepción (txt,log.xml)
            if (tipoExcepcion == null)
                return;
        begin:
            switch (tipoExcepcion.ToLower().Trim())
            {
                case "txt":
                    EventoTexto(Excepcion);
                    break;
                case "db":
                    new tblLogError().Save(Excepcion);
                    break;
                case "log":
                    EventoVisorSucesos(Excepcion);
                    break;
                default:
                    tipoExcepcion = "txt";
                    goto begin;
            }
        }
        /// <summary>
        /// Recibe el parametro de excepcion, y se captura del Web.Config el tipo de excepción (txt,xml,log)
        /// y se sobrecarga el metodo publicar para realizar el proceso adecuado.
        /// </summary>
        /// <param name="excepcion">Mensaje del Error provocado</param>
        /// <remarks>
        /// Autor: Ssoft - Cmazo
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-04-03
        /// </remarks>
        public static void Publicar(Exception excepcion)
        {
            string pvstrTipoExcepcion = clsValidaciones.GetKeyOrAdd("TipoExcepcion", "txt");
            Publicar(excepcion, pvstrTipoExcepcion);
        }

        /// <summary>
        /// Se realiza la sobrecargar para cuando no se envia ningun parametro.
        /// </summary>
        public static void Publicar()
        {
            string pvstrTipoExcepcion = string.Empty;
            string tmpMsgExcepcion;

            tmpMsgExcepcion = "Error en Aplicativo Ssoft, Intente nuevamente.";
            pvstrTipoExcepcion = clsValidaciones.GetKeyOrAdd("TipoExcepcion", "txt");
            Publicar(new Exception(tmpMsgExcepcion), pvstrTipoExcepcion);
        }
        /// <summary>
        /// Se realiza la sobrecarga para enviar un mensaje personalizado
        /// </summary>
        /// <param name="sError"> Mensaje de error</param>
        public static void Publicar(string sError)
        {
            string pvstrTipoExcepcion = string.Empty;
            string tmpMsgExcepcion;

            tmpMsgExcepcion = sError;
            pvstrTipoExcepcion = clsValidaciones.GetKeyOrAdd("TipoExcepcion", "txt");
            Publicar(new Exception(tmpMsgExcepcion), pvstrTipoExcepcion);
        }

        public static void Publicar(clsParametros sMensaje)
        {
            string pvstrTipoExcepcion = clsValidaciones.GetKeyOrAdd("TipoExcepcion", "txt");
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    sMensaje.SesionLocal = cCache.SessionID;
                }
            }
            catch { }

            Publicar(sMensaje, pvstrTipoExcepcion);
        }
        /// <summary>
        /// Se realiza la captura de la ruta en donde se almacenara el archivo de tipo txt,
        /// si este no existe es creado y se almacenará el mensaje con la hora y dia, en caso contrario simplemete se
        /// almacenará el mensaje de error con la hora y dia.
        /// </summary>
        /// <param name="ex">Mensaje del Error provocado</param>
        /// <remarks>
        /// Autor: Ssoft - Cmazo
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-04-03
        /// </remarks>
        private static void EventoTexto(Exception ex)
        {
            string Mensaje, Folder, sPath = string.Empty;
            string sArchivo = "Ssoft.txt";
            try
            {
                sArchivo = "Ssoft" + clsValidaciones.CrearFechaString() + ".txt";
            }
            catch { }
            try { sPath = clsValidaciones.DocumentosTempCrea(); }
            catch { sPath = @"c:\Temp\"; }
            sPath += sArchivo;
            Folder = Folder = sPath.Substring(0, sPath.LastIndexOf('\\'));
            Mensaje = ex.Message.ToString();
            // Si no existe la carpeta esta es creada...
            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }
            // Para copiar en el archivo plano las lineas de los mensajes de error
            StreamWriter sw = new StreamWriter(sPath, true);
            if (File.Exists(sPath))
            { 
                WriteLineFinally(Mensaje, sw, ex);
             
            }
            else
            {
                try
                {
                    File.Create(sPath);
                    
                }
                catch { }
                WriteLineFinally(Mensaje, sw, ex);
            }
        
            //EventoVisorSucesos(ex);
        }


        private static void WriteLineFinally(string sMensaje, StreamWriter sw, Exception ex)
        {
            try
            {
                sw.WriteLine("Fecha del evento: " + DateTime.Now.ToString("yyyy/MM/dd hh:mm"));
                sw.WriteLine(Environment.NewLine);
                sw.WriteLine("Mensaje de error: " + sMensaje);
                sw.WriteLine(Environment.NewLine);
                sw.WriteLine("Aplicación: " + ex.Source);
                sw.WriteLine(Environment.NewLine);
                sw.WriteLine("StackTrace: " + ex.StackTrace);
                sw.WriteLine(Environment.NewLine);
            }
            catch
            {
            }
            finally
            {
                try
                {
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                    GC.Collect();
                 
                }
                catch {
                   
                }
                Thread.Sleep(1000);
            }
          
        }

        public static void EventoTexto(clsParametros Ex)
        {
            string Folder, sPath = string.Empty;

            string sArchivo = "Ssoft.txt";
            string sSesion = clsValidaciones.CrearFechaString();
            try
            {
                try
                {
                    if (Ex.TipoLog.Equals(Enum_Error.Transac))
                        sArchivo = "SsoftTransac" + sSesion + ".txt";
                    else
                        sArchivo = "Ssoft" + sSesion + ".txt";
                }
                catch { sArchivo = "Ssoft" + sSesion + ".txt"; }
            }
            catch { }
            try { sPath = clsValidaciones.DocumentosTempCrea(); }
            catch { sPath = @"c:\Temp\"; }
            sPath += sArchivo;
            Folder = Folder = sPath.Substring(0, sPath.LastIndexOf('\\'));

            // Si no existe la carpeta esta es creada...
            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }
            // Para copiar en el archivo plano las lineas de los mensajes de error
            StreamWriter sw = new StreamWriter(sPath, true);
            string strMensaje = ConstructorMensaje(Ex);

            if (File.Exists(sPath))
            {
                sw.WriteLine(strMensaje);
            }
            else
            {
                try
                {
                    File.Create(sPath);
                }
                catch { }

                sw.WriteLine(strMensaje);
            }
            sw.Close();
        }
        private static string ConstructorMensaje(clsParametros Ex)
        {
            StringBuilder sMensaje = new StringBuilder();

            sMensaje.Append("Fecha:           " + DateTime.Now.ToString("yyyy/MM/dd hh:mm"));
            sMensaje.Append(Environment.NewLine);

            if (!Ex.Code.Length.Equals(0))
            {
                sMensaje.Append("Codigo:      " + Ex.Code);
                sMensaje.Append(Environment.NewLine);
            }
            if (!Ex.Tipo.Length.Equals(0))
            {
                sMensaje.Append("Tipo:        " + Ex.Tipo);
                sMensaje.Append(Environment.NewLine);
            }
            if (!Ex.Metodo.Length.Equals(0))
            {
                sMensaje.Append("Método:      " + Ex.Metodo);
                sMensaje.Append(Environment.NewLine);
            }
            if (!Ex.Severity.Length.Equals(0))
            {
                sMensaje.Append("Severidad:   " + Ex.Severity);
                sMensaje.Append(Environment.NewLine);
            }
            if (!Ex.Message.Length.Equals(0))
            {
                sMensaje.Append("Mensaje:     " + Ex.Message);
                sMensaje.Append(Environment.NewLine);
            }
            if (!Ex.Info.Length.Equals(0))
            {
                sMensaje.Append("Info:        " + Ex.Info);
                sMensaje.Append(Environment.NewLine);
            }
            if (!Ex.Source.Length.Equals(0))
            {
                sMensaje.Append("Source:      " + Ex.Source);
                sMensaje.Append(Environment.NewLine);
            }
            if (Ex.StackTrace != null && !Ex.StackTrace.Length.Equals(0))
            {
                sMensaje.Append("StackTrace:  " + Ex.StackTrace);
                sMensaje.Append(Environment.NewLine);
            }
            if (!Ex.InnerException.Length.Equals(0))
            {
                sMensaje.Append("InnerEx:     " + Ex.InnerException);
                sMensaje.Append(Environment.NewLine);
            }
            if (!Ex.TargetSite.Length.Equals(0))
            {
                sMensaje.Append("TargetSite:  " + Ex.TargetSite);
                sMensaje.Append(Environment.NewLine);
            }
            if (!Ex.Data.Length.Equals(0))
            {
                sMensaje.Append("Data:        " + Ex.Data);
                sMensaje.Append(Environment.NewLine);
            }
            if (!Ex.Complemento.Length.Equals(0))
            {
                sMensaje.Append("Complmeneto: " + Ex.Complemento);
                sMensaje.Append(Environment.NewLine);
            }
            sMensaje.Append("Sesion Local: " + Ex.SesionLocal);
            sMensaje.Append(Environment.NewLine);

            sMensaje.Append("Sesion WS: " + Ex.SesionWs);
            sMensaje.Append(Environment.NewLine);

            return sMensaje.ToString();
        }
        /// <summary>
        /// sobrecarga que maneja por defecto el eventLogEntryType igual a Error.
        /// </summary>
        /// <param name="ex">Mensaje del Error provocado</param>
        private static void EventoVisorSucesos(Exception ex)
        {
            EventoVisorSucesos(ex, EventLogEntryType.Error);
        }

        private static void EventoVisorSucesos(clsParametros Ex)
        {
            EventoVisorSucesos(Ex, EventLogEntryType.Error);
        }
        /// <summary>
        /// Si no existe el curso para crear los eventos en el visor de sucesos, es creado,
        /// si ya existe simplemente crea el nuevo evento en el visor de sucesos con el mensaje
        /// del suceso en el registro de aplicación.
        /// Si el usuario no tiene permisos para el manejo del visor de sucesos, se manejara por
        /// el tipo de eventos de tipo txt (archivo plano).
        /// </summary>
        /// <param name="ex">Mensaje del Error provocado</param
        /// <param name="EveLog">Tipo de Entrada que maneja el visor de sucesos(Error,Warning,Information)</param>
        /// <remarks>
        /// Autor: Ssoft - Cmazo
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-04-03
        /// </remarks>		
        private static void EventoVisorSucesos(Exception ex, EventLogEntryType EveLog)
        {
            string _strMensaje = string.Empty;
            _strMensaje += "Mensaje de error: " + ex.Message;
            _strMensaje += "StackTrace: " + ex.StackTrace;

            try
            {
                if (!EventLog.SourceExists("Ssoft"))
                {
                    EventLog.CreateEventSource("Ssoft", "Application");
                    EventLog.WriteEntry("Ssoft", _strMensaje, EveLog);
                }
                else
                {
                    EventLog.WriteEntry("Ssoft", _strMensaje, EveLog);
                }

            }
            catch (Exception Ex)
            {
                string lstrMensaje = ex.Message;
                lstrMensaje += " " + _strMensaje + " Adicionalmente al utilizar la configuración " +
                    " del visor de sucesos produjo el siguiente error :  " +
                    Ex.Message + ".  Verifique los privilegios o contacte al administrador del sistema. ";
                EventoTexto(new Exception(lstrMensaje));
            }
        }
        private static void EventoVisorSucesos(clsParametros Ex, EventLogEntryType EveLog)
        {
            string _strMensaje = ConstructorMensaje(Ex);

            try
            {
                if (!EventLog.SourceExists("Ssoft"))
                {
                    EventLog.CreateEventSource("Ssoft", "Application");
                    EventLog.WriteEntry("Ssoft", _strMensaje, EveLog);
                }
                else
                {
                    EventLog.WriteEntry("Ssoft", _strMensaje, EveLog);
                }

            }
            catch (Exception ex)
            {
                string lstrMensaje = ex.Message;
                lstrMensaje += " " + _strMensaje + " Adicionalmente al utilizar la configuración " +
                    " del visor de sucesos produjo el siguiente error :  " +
                    Ex.Message + ".  Verifique los privilegios o contacte al administrador del sistema. ";
                EventoTexto(new Exception(lstrMensaje));
            }
        }
    }
    public class clsParametros
    {
        private string sTipo = string.Empty;

        public string Tipo
        {
            get { return sTipo; }
            set { sTipo = value; }
        }
        private string sCode = "0";

        public string Code
        {
            get { return sCode; }
            set { sCode = value; }
        }
        private string sInfo = string.Empty;

        public string Info
        {
            get { return sInfo; }
            set { sInfo = value; }
        }
        private string sMessage = string.Empty;

        public string Message
        {
            get { return sMessage; }
            set { sMessage = value; }
        }
        private string sSeverity = string.Empty;

        public string Severity
        {
            get { return sSeverity; }
            set { sSeverity = value; }
        }
        private string sMetodo = string.Empty;

        public string Metodo
        {
            get { return sMetodo; }
            set { sMetodo = value; }
        }
        private string sSource = string.Empty;

        public string Source
        {
            get { return sSource; }
            set { sSource = value; }
        }
        private string sStackTrace = string.Empty;

        public string StackTrace
        {
            get { return sStackTrace; }
            set { sStackTrace = value; }
        }

        private string sInnerException = string.Empty;

        public string InnerException
        {
            get { return sInnerException; }
            set { sInnerException = value; }
        }
        private string sTargetSite = string.Empty;

        public string TargetSite
        {
            get { return sTargetSite; }
            set { sTargetSite = value; }
        }
        private string sData = string.Empty;

        public string Data
        {
            get { return sData; }
            set { sData = value; }
        }
        private string sComplemento = string.Empty;

        public string Complemento
        {
            get { return sComplemento; }
            set { sComplemento = value; }
        }
        private List<string[]> sErrorArr = new List<string[]>();

        public List<string[]> ErrorArr
        {
            get { return sErrorArr; }
            set { sErrorArr = value; }
        }

        // Parametros para visualizar en mensajes de error
        private List<string> sViewMessage = new List<string>();

        public List<string> ViewMessage
        {
            get { return sViewMessage; }
            set { sViewMessage = value; }
        }
        private List<string> sSugerencia = new List<string>();

        public List<string> Sugerencia
        {
            get { return sSugerencia; }
            set { sSugerencia = value; }
        }
        private string sDatoAdic = string.Empty;

        public string DatoAdic
        {
            get { return sDatoAdic; }
            set { sDatoAdic = value; }
        }
        private List<string> lsDatoAdicArr = new List<string>();

        public List<string> DatoAdicArr
        {
            get { return lsDatoAdicArr; }
            set { lsDatoAdicArr = value; }
        }
        private int iMensaje = 0;

        public int Id
        {
            get { return iMensaje; }
            set { iMensaje = value; }
        }

        private Enum_Error eTipoMensaje = Enum_Error.Log;

        public Enum_Error TipoLog
        {
            get { return eTipoMensaje; }
            set { eTipoMensaje = value; }
        }

        private Enum_ProveedorWebServices eTipoWs = Enum_ProveedorWebServices.Planes;
        public Enum_ProveedorWebServices TipoWs
        {
            get { return eTipoWs; }
            set { eTipoWs = value; }
        }

        private bool bMessageBD = true;
        public bool MessageBD
        {
            get { return bMessageBD; }
            set { bMessageBD = value; }
        }

        private bool bValidaInfo = true;
        public bool ValidaInfo
        {
            get { return bValidaInfo; }
            set { bValidaInfo = value; }
        }

        private string[] sErrorConfigura = new string[8];
        // 1. Id Empresa
        // 2. Nombre Contacto
        // 3. Mensaje adicional 
        // 4. Mensaje adicional 1
        // 5. Mensaje adicional 2
        // 6. Sugerenacia adicional 
        // 7. Sugerenacia adicional 1
        // 8. Sugerenacia adicional 2
        public string[] ErrorConfigura
        {
            get { return sErrorConfigura; }
            set { sErrorConfigura = value; }
        }

        private string sSesionLocal = string.Empty;
        public string SesionLocal
        {
            get { return sSesionLocal; }
            set { sSesionLocal = value; }
        }

        private string sSesionWs = string.Empty;
        public string SesionWs
        {
            get { return sSesionWs; }
            set { sSesionWs = value; }
        }

        private Exception _Ex = new Exception();
        public Exception Ex
        {
            get { return _Ex; }
            set { _Ex = value; }
        }

        public clsParametros()
        {
        }
    }
    public class clsTipoError
    {
        // Tipos de error
        public const string DataBase = "Base de Datos";
        public const string WebServices = "WebServices";
        public const string Aplication = "Pagina";
        public const string Library = "Libreria";
        public const string Record = "Reserva";
    }
    public class clsSeveridad
    {
        // Severidad
        public const string Alta = "Alta";
        public const string Media = "Media";
        public const string Moderada = "Moderada";
        public const string Baja = "Baja";
    }
    public class clsErrorMensaje
    {
        public clsErrorMensaje()
        {
        }
        //private string getMensaje(clsParametros cParametros)
        //{
        //    string sRutaImagenes = clsValidaciones.ObtenerUrlImages();
        //    string sImagen = sRutaImagenes + "sinResultados.jpg";
        //    string sTelefono = string.Empty;

        //    switch (cParametros.Tipo)
        //    {
        //        case clsTipoError.Aplication:
        //            try
        //            {
        //                if (cParametros.Severity.Equals(clsSeveridad.Alta))
        //                {
        //                    try
        //                    {
        //                        sImagen = clsValidaciones.GetKeyOrAdd("ErrorCancel", sRutaImagenes + "cancel.png");
        //                    }
        //                    catch
        //                    {
        //                        sImagen = sRutaImagenes + "cancel.png";
        //                    }
        //                }
        //                else
        //                {
        //                    sImagen = clsValidaciones.GetKeyOrAdd("ErrorAplicacion", sRutaImagenes + "advertencia.png");
        //                }
        //            }
        //            catch
        //            {
        //                sImagen = sRutaImagenes + "advertencia.png";
        //            }
        //            break;

        //        case clsTipoError.DataBase:
        //            try
        //            {
        //                if (cParametros.Severity.Equals(clsSeveridad.Alta))
        //                {
        //                    try
        //                    {
        //                        sImagen = clsValidaciones.GetKeyOrAdd("ErrorCancel", sRutaImagenes + "cancel.png");
        //                    }
        //                    catch
        //                    {
        //                        sImagen = sRutaImagenes + "cancel.png";
        //                    }
        //                }
        //                else
        //                {
        //                    sImagen = clsValidaciones.GetKeyOrAdd("ErrorDataBase", sRutaImagenes + "bd.png");
        //                }
        //            }
        //            catch
        //            {
        //                sImagen = sRutaImagenes + "bd.png";
        //            }
        //            break;

        //        case clsTipoError.Library:
        //            try
        //            {
        //                sImagen = clsValidaciones.GetKeyOrAdd("ErrorLibreria", sRutaImagenes + "advertencia.png");
        //            }
        //            catch
        //            {
        //                sImagen = sRutaImagenes + "advertencia.png";
        //            }
        //            break;

        //        case clsTipoError.WebServices:
        //            try
        //            {
        //                sImagen = clsValidaciones.GetKeyOrAdd("ErrorWebServices", sRutaImagenes + "busqueda.png");
        //            }
        //            catch
        //            {
        //                sImagen = sRutaImagenes + "busqueda.png";
        //            }
        //            break;

        //        case clsTipoError.Record:
        //            try
        //            {
        //                sImagen = clsValidaciones.GetKeyOrAdd("ErrorReserva", sRutaImagenes + "Reserva.jpg");
        //            }
        //            catch
        //            {
        //                sImagen = sRutaImagenes + "Reserva.jpg";
        //            }
        //            break;
        //    }
        //    try
        //    {
        //        sTelefono = "Contactarse al " + ConfigurationManager.AppSettings["Agencia_Telefono"].ToString();
        //    }
        //    catch { sTelefono = string.Empty; }

        //    if (cParametros.ViewMessage.Count == 0)
        //    {
        //        cParametros.ViewMessage.Add("Error de la aplicacion");
        //    }
        //    if (cParametros.Sugerencia.Count == 0)
        //    {
        //        cParametros.Sugerencia.Add("Por favor, intente nuevamente");
        //    }
        //    int iContMes = cParametros.ViewMessage.Count;
        //    int iContSug = cParametros.Sugerencia.Count;

        //    StringBuilder sbError = new StringBuilder();


        //    sbError.AppendLine(" <div class='mlb'><div class='mrb'><div class='mbb'><div class='mblc'><div class='mbrc'><div class='mtb'><div class='mtlc'><div class='mtrc'>    ");
        //    sbError.AppendLine("  <div class='contenidoError'>   ");
        //    sbError.AppendLine("  <img alt='' src='" + sImagen + "' />   ");
        //    if (!sTelefono.Length.Equals(0))
        //    {
        //        sbError.AppendLine("  <span class='tituloError'>" + sTelefono + "</span><br /><br />   ");
        //    }

        //    sbError.AppendLine(" <span class='tituloError'>");
        //    for (int i = 0; i < iContMes; i++)
        //    {
        //        sbError.AppendLine(cParametros.ViewMessage[i].ToString() + "<br /> ");
        //    }
        //    sbError.AppendLine("</span><br /> ");

        //    sbError.AppendLine(" <span class='tituloSugerencia'>");
        //    for (int i = 0; i < iContSug; i++)
        //    {
        //        sbError.AppendLine(cParametros.Sugerencia[i].ToString() + "<br /> ");
        //    }
        //    sbError.AppendLine("</span><br /> ");

        //    //sbError.AppendLine("<span class='tituloTipo'>" + cParametros.Tipo + "</span>");

        //    sbError.AppendLine(" </div>    ");
        //    sbError.AppendLine(" </div></div></div></div></div></div></div></div>    ");
        //    // Codigo para incluir el chat

        //    sbError.AppendLine(" <iframe width='400' height='370' src='http://livechat.boldchat.com/aid/6204590666656341798/bc.chat?cwdid=5726033846606487700&amp;rdid=1016295497287474614&amp;vr=&amp;vn=&amp;vi=&amp;ve=&amp;vp=&amp;iq=&amp;curl=&amp;url=' + escape(document.location.href), 'Chat1690904273425048068', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1')'></iframe> ");

        //    return sbError.ToString();
        //}
        private string getMensajeBd(clsParametros cParametros)
        {
            string sRutaImagenes = clsValidaciones.ObtenerUrlImages();
            string sImagen = sRutaImagenes + "sinResultados.jpg";
            string sEmpresa = cParametros.ErrorConfigura[0];
            string sContacto = cParametros.ErrorConfigura[1];
            clsCache cCache = new csCache().cCache();
            if (sEmpresa == null)
            {
                if (cCache != null)
                {
                    sEmpresa = cCache.Empresa;
                }
                else
                {
                    sEmpresa = "0";
                }
            }
            if (sContacto == null || sContacto.Length == 0)
            {
                if (cCache != null)
                {
                    if (!cCache.Empresa.Equals(cCache.Contacto))
                    {
                        if (cCache.Empresa.Length.Equals(0))
                        {
                            sContacto = "0";
                        }
                        else
                        {
                            sContacto = cCache.Empresa;
                        }
                    }
                    else
                    {
                        sContacto = "0";
                    }
                }
                else
                {
                    sContacto = "0";
                }
            }
            int sAplicacion = clsSesiones.getAplicacion();

            StringBuilder sbError = new StringBuilder();
            try
            {
                StringBuilder sConsulta = new StringBuilder();
                DataSql pclsDataSql = new DataSql();

                DataSet dsError = new DataSet();
                string sIdioma = clsSesiones.getIdioma();
                sConsulta.AppendLine(" SELECT     tblMessageError.*, tblRefere.intNivel ");
                sConsulta.AppendLine(" FROM         tblRefere INNER JOIN ");
                sConsulta.AppendLine(" tblMessageError ON tblRefere.intAplicacion = tblMessageError.intAplicacion AND   ");
                sConsulta.AppendLine(" tblRefere.intidRefere = tblMessageError.intTipoPlan AND tblRefere.strIdioma = tblMessageError.strIdioma ");
                sConsulta.AppendLine(" AND (tblMessageError.intAplicacion = " + sAplicacion.ToString() + ") ");
                sConsulta.AppendLine(" AND (tblMessageError.intEmpresa = " + sEmpresa + ")  ");
                sConsulta.AppendLine(" AND (tblMessageError.strIdioma = '" + sIdioma + "') ");
                dsError = pclsDataSql.Select(sConsulta.ToString());
                try
                {
                    if (dsError.Tables[0].Rows.Count == 0)
                    {
                        if (sIdioma != "es")
                        {
                            sConsulta = new StringBuilder();

                            sConsulta.AppendLine(" SELECT     tblMessageError.*, tblRefere.intNivel ");
                            sConsulta.AppendLine(" FROM         tblRefere INNER JOIN ");
                            sConsulta.AppendLine(" tblMessageError ON tblRefere.intAplicacion = tblMessageError.intAplicacion AND   ");
                            sConsulta.AppendLine(" tblRefere.intidRefere = tblMessageError.intTipoPlan AND tblRefere.strIdioma = tblMessageError.strIdioma ");
                            sConsulta.AppendLine(" AND (tblMessageError.intAplicacion = " + sAplicacion.ToString() + ") ");
                            sConsulta.AppendLine(" AND (tblMessageError.intEmpresa = " + sEmpresa + ")  ");
                            sConsulta.AppendLine(" AND (tblMessageError.strIdioma = 'es') ");
                            dsError = pclsDataSql.Select(sConsulta.ToString());

                            if (dsError.Tables[0].Rows.Count == 0)
                            {
                                sConsulta = new StringBuilder();

                                sConsulta.AppendLine(" SELECT     tblMessageError.*, tblRefere.intNivel ");
                                sConsulta.AppendLine(" FROM         tblRefere INNER JOIN ");
                                sConsulta.AppendLine(" tblMessageError ON tblRefere.intAplicacion = tblMessageError.intAplicacion AND   ");
                                sConsulta.AppendLine(" tblRefere.intidRefere = tblMessageError.intTipoPlan AND tblRefere.strIdioma = tblMessageError.strIdioma ");
                                sConsulta.AppendLine(" AND (tblMessageError.intAplicacion = " + sAplicacion.ToString() + ") ");
                                sConsulta.AppendLine(" AND (tblMessageError.intEmpresa = 0)  ");
                                sConsulta.AppendLine(" AND (tblMessageError.strIdioma = 'es') ");
                                dsError = pclsDataSql.Select(sConsulta.ToString());
                            }
                        }
                        else
                        {
                            sConsulta = new StringBuilder();

                            sConsulta.AppendLine(" SELECT     tblMessageError.*, tblRefere.intNivel ");
                            sConsulta.AppendLine(" FROM         tblRefere INNER JOIN ");
                            sConsulta.AppendLine(" tblMessageError ON tblRefere.intAplicacion = tblMessageError.intAplicacion AND   ");
                            sConsulta.AppendLine(" tblRefere.intidRefere = tblMessageError.intTipoPlan AND tblRefere.strIdioma = tblMessageError.strIdioma ");
                            sConsulta.AppendLine(" AND (tblMessageError.intAplicacion = " + sAplicacion.ToString() + ") ");
                            sConsulta.AppendLine(" AND (tblMessageError.intEmpresa = 0)  ");
                            sConsulta.AppendLine(" AND (tblMessageError.strIdioma = 'es') ");
                            dsError = pclsDataSql.Select(sConsulta.ToString());
                        }
                    }
                }
                catch { }
                if (dsError.Tables[0].Rows.Count > 0)
                {
                    StringBuilder sdsData = new StringBuilder();
                    string strTipo = cParametros.TipoWs.GetHashCode().ToString();

                    sdsData.AppendLine(" (intNivel = " + strTipo + ") ");
                    sdsData.AppendLine(" AND (strCode = '" + cParametros.Code + "') ");
                    sdsData.AppendLine(" AND (bitActivo = 1) ");
                    if (cParametros.ValidaInfo)
                    {
                        string sInfoRango = cParametros.Info;
                        int iPosTotal = sInfoRango.Length;
                        if (sInfoRango.Contains("Error response received. The error was: "))
                        {
                            if (iPosTotal > 60)
                            {
                                sdsData.AppendLine(" AND (strInfo LIKE '%" + sInfoRango.Substring(40, 20).Trim() + "%') ");
                            }
                            else
                            {
                                sdsData.AppendLine(" AND (strInfo LIKE '%" + sInfoRango.Substring(40).Trim() + "%') ");
                            }
                        }
                        else
                        {
                            if (iPosTotal > 20)
                            {
                                sdsData.AppendLine(" AND (strInfo LIKE '%" + cParametros.Info.Substring(3, 20).Trim() + "%') ");
                            }
                            else
                            {
                                sdsData.AppendLine(" AND (strInfo LIKE '%" + cParametros.Info.Trim() + "%') ");
                            }
                        }
                    }

                    DataRow[] drErros = dsError.Tables[0].Select(sdsData.ToString());
                    if (drErros.Length.Equals(0))
                    {
                        sdsData = new StringBuilder();

                        sdsData.AppendLine(" (intNivel = " + strTipo + ") ");
                        sdsData.AppendLine(" AND (strCode = '" + cParametros.Code + "') ");
                        sdsData.AppendLine(" AND (bitActivo = 1) ");
                        drErros = dsError.Tables[0].Select(sdsData.ToString());
                    }
                    if (drErros.Length.Equals(0))
                    {
                        sdsData = new StringBuilder();

                        sdsData.AppendLine(" (intNivel = " + strTipo + ") ");
                        sdsData.AppendLine(" AND (strCode = '" + cParametros.Code + "') ");
                        drErros = dsError.Tables[0].Select(sdsData.ToString());
                    }
                    if (drErros.Length.Equals(0))
                    {
                        sdsData = new StringBuilder();

                        sdsData.AppendLine(" (intNivel = " + strTipo + ") ");
                        sdsData.AppendLine(" AND (strCode = '0') ");
                        drErros = dsError.Tables[0].Select(sdsData.ToString());
                    }
                    if (drErros.Length.Equals(0))
                    {
                        strTipo = Enum_ProveedorWebServices.Planes.GetHashCode().ToString();
                        sdsData = new StringBuilder();

                        sdsData.AppendLine(" (intNivel = " + strTipo + ") ");
                        sdsData.AppendLine(" AND (strCode = '0') ");
                        drErros = dsError.Tables[0].Select(sdsData.ToString());
                    }
                    if (!drErros.Length.Equals(0))
                    {
                        if (!drErros[0]["strImagen"].ToString().Length.Equals(0))
                            sImagen = sRutaImagenes + drErros[0]["strImagen"].ToString();

                        sbError.AppendLine("  <div class='contenidoError'>   ");
                        sbError.AppendLine("  <img alt='' src='" + sImagen + "' />   ");

                        sbError.AppendLine(" <span class='tituloError'>");

                        if (sContacto != "0")
                            sbError.AppendLine("Mr(s) " + sContacto + ":<br /> ");

                        if (!drErros[0]["strMensaje"].ToString().Length.Equals(0))
                        {
                            if (cParametros.ErrorConfigura[2] != null)
                                sbError.AppendLine(drErros[0]["strMensaje"].ToString() + " " + cParametros.ErrorConfigura[2].ToString() + "<br /> ");
                            else
                                sbError.AppendLine(drErros[0]["strMensaje"].ToString() + "<br /> ");
                        }
                        if (!drErros[0]["strMensaje1"].ToString().Length.Equals(0))
                        {
                            if (cParametros.ErrorConfigura[3] != null)
                                sbError.AppendLine(drErros[0]["strMensaje1"].ToString() + " " + cParametros.ErrorConfigura[3].ToString() + "<br /> ");
                            else
                                sbError.AppendLine(drErros[0]["strMensaje1"].ToString() + "<br /> ");
                        }
                        if (!drErros[0]["strMensaje2"].ToString().Length.Equals(0))
                        {
                            if (cParametros.ErrorConfigura[4] != null)
                                sbError.AppendLine(drErros[0]["strMensaje2"].ToString() + " " + cParametros.ErrorConfigura[4].ToString() + "<br /> ");
                            else
                                sbError.AppendLine(drErros[0]["strMensaje2"].ToString() + "<br /> ");
                        }
                        sbError.AppendLine("</span><br /> ");

                        sbError.AppendLine(" <span class='tituloSugerencia'>");
                        if (!drErros[0]["strSugerencia"].ToString().Length.Equals(0))
                        {
                            if (cParametros.ErrorConfigura[5] != null)
                                sbError.AppendLine(drErros[0]["strSugerencia"].ToString() + " " + cParametros.ErrorConfigura[5].ToString() + "<br /> ");
                            else
                                sbError.AppendLine(drErros[0]["strSugerencia"].ToString() + "<br /> ");
                        }
                        if (!drErros[0]["strSugerencia1"].ToString().Length.Equals(0))
                        {
                            if (cParametros.ErrorConfigura[6] != null)
                                sbError.AppendLine(drErros[0]["strSugerencia1"].ToString() + " " + cParametros.ErrorConfigura[6].ToString() + "<br /> ");
                            else
                                sbError.AppendLine(drErros[0]["strSugerencia1"].ToString() + "<br /> ");
                        }
                        if (!drErros[0]["strSugerencia2"].ToString().Length.Equals(0))
                        {
                            if (cParametros.ErrorConfigura[7] != null)
                                sbError.AppendLine(drErros[0]["strSugerencia2"].ToString() + " " + cParametros.ErrorConfigura[7].ToString() + "<br /> ");
                            else
                                sbError.AppendLine(drErros[0]["strSugerencia2"].ToString() + "<br /> ");
                        }
                        sbError.AppendLine("</span><br /> ");
                        sbError.AppendLine(" </div>    ");
                        if (drErros[0]["bitActivaChat"].ToString().Equals("True"))
                        {
                            string sChat = drErros[0]["strLinkChat"].ToString();
                            if (sContacto != "0")
                            {
                                sChat = sChat.Replace("&amp;vn=", "&vn=" + sContacto);
                            }
                            sbError.AppendLine(" <div class='chatError'>  ");
                            sbError.AppendLine("  <iframe width='" + drErros[0]["intWidthChat"].ToString() + "' height='" + drErros[0]["intHeightChat"].ToString() + "' src='" + sChat + "')'></iframe>  ");
                            sbError.AppendLine(" </div>    ");
                        }
                    }
                }
            }
            catch { }
            return sbError.ToString();
        }
        private string getMensaje(clsParametros cParametros)
        {
            bool bValida = false;
            StringBuilder sbError = new StringBuilder();
            if (cParametros.MessageBD)
            {
                if (getMensajeBd(cParametros).Length.Equals(0))
                {
                    bValida = true;
                }
                else
                {
                    sbError.AppendLine(getMensajeBd(cParametros));
                }
            }
            else
            {
                bValida = true;
            }
            if (bValida)
            {
                string sRutaImagenes = clsValidaciones.ObtenerUrlImages();
                string sImagen = sRutaImagenes + "sinResultados.jpg";
                string sTelefono = string.Empty;
                string sAgenciaTelefono = clsValidaciones.GetKeyOrAdd("Agencia_Telefono", "");
                switch (cParametros.Tipo)
                {
                    case clsTipoError.Aplication:
                        try
                        {
                            if (cParametros.Severity.Equals(clsSeveridad.Alta))
                            {
                                try
                                {
                                    sImagen = clsValidaciones.GetKeyOrAdd("ErrorCancel", sRutaImagenes + "cancel.png");
                                }
                                catch
                                {
                                    sImagen = sRutaImagenes + "cancel.png";
                                }
                            }
                            else
                            {
                                sImagen = clsValidaciones.GetKeyOrAdd("ErrorAplicacion", sRutaImagenes + "advertencia.png");
                            }
                        }
                        catch
                        {
                            sImagen = sRutaImagenes + "advertencia.png";
                        }
                        break;

                    case clsTipoError.DataBase:
                        try
                        {
                            if (cParametros.Severity.Equals(clsSeveridad.Alta))
                            {
                                try
                                {
                                    sImagen = clsValidaciones.GetKeyOrAdd("ErrorCancel", sRutaImagenes + "cancel.png");
                                }
                                catch
                                {
                                    sImagen = sRutaImagenes + "cancel.png";
                                }
                            }
                            else
                            {
                                sImagen = clsValidaciones.GetKeyOrAdd("ErrorDataBase", sRutaImagenes + "bd.png");
                            }
                        }
                        catch
                        {
                            sImagen = sRutaImagenes + "bd.png";
                        }
                        break;

                    case clsTipoError.Library:
                        try
                        {
                            sImagen = clsValidaciones.GetKeyOrAdd("ErrorLibreria", sRutaImagenes + "advertencia.png");
                        }
                        catch
                        {
                            sImagen = sRutaImagenes + "advertencia.png";
                        }
                        break;

                    case clsTipoError.WebServices:
                        try
                        {
                            sImagen = clsValidaciones.GetKeyOrAdd("ErrorWebServices", sRutaImagenes + "busqueda.png");
                        }
                        catch
                        {
                            sImagen = sRutaImagenes + "busqueda.png";
                        }
                        break;

                    case clsTipoError.Record:
                        try
                        {
                            sImagen = clsValidaciones.GetKeyOrAdd("ErrorReserva", sRutaImagenes + "Reserva.jpg");
                        }
                        catch
                        {
                            sImagen = sRutaImagenes + "Reserva.jpg";
                        }
                        break;
                }
                try
                {
                    if(sAgenciaTelefono.Length > 0)
                        sTelefono = "Contactarse al " + sAgenciaTelefono;
                }
                catch { sTelefono = string.Empty; }

                if (cParametros.ViewMessage.Count == 0)
                {
                    cParametros.ViewMessage.Add("Error de la aplicacion");
                }
                if (cParametros.Sugerencia.Count == 0)
                {
                    cParametros.Sugerencia.Add("Por favor, intente nuevamente");
                }
                int iContMes = cParametros.ViewMessage.Count;
                int iContSug = cParametros.Sugerencia.Count;

                //sbError.AppendLine(" <div class='mlb'><div class='mrb'><div class='mbb'><div class='mblc'><div class='mbrc'><div class='mtb'><div class='mtlc'><div class='mtrc'>    ");
                sbError.AppendLine("  <div class='contenidoError'>   ");
                sbError.AppendLine("  <img alt='' src='" + sImagen + "' />   ");
                if (!sTelefono.Length.Equals(0))
                {
                    sbError.AppendLine("  <span class='tituloError'>" + sTelefono + "</span><br /><br />   ");
                }

                sbError.AppendLine(" <span class='tituloError'>");
                for (int i = 0; i < iContMes; i++)
                {
                    sbError.AppendLine(cParametros.ViewMessage[i].ToString() + "<br /> ");
                }
                sbError.AppendLine("</span><br /> ");

                sbError.AppendLine(" <span class='tituloSugerencia'>");
                for (int i = 0; i < iContSug; i++)
                {
                    sbError.AppendLine(cParametros.Sugerencia[i].ToString() + "<br /> ");
                }
                sbError.AppendLine("</span><br /> ");

                //sbError.AppendLine("<span class='tituloTipo'>" + cParametros.Tipo + "</span>");

                sbError.AppendLine(" </div>    ");
                //sbError.AppendLine(" </div></div></div></div></div></div></div></div>    ");
                // Codigo para incluir el chat

                //sbError.AppendLine(" <div class='chatError'>  ");
                //sbError.AppendLine("  <iframe width='" + "" + "' height='" + "' src='http://livechat.boldchat.com/aid/6204590666656341798/bc.chat?cwdid=5726033846606487700&amp;rdid=1016295497287474614&amp;vr=&amp;vn=&amp;vi=&amp;ve=&amp;vp=&amp;iq=&amp;curl=&amp;url=' + escape(document.location.href), 'Chat1690904273425048068', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1')'></iframe>  ");
                //sbError.AppendLine(" </div>    ");
            }
            return sbError.ToString();
        }
        private Table getMensajeT(clsParametros cParametros)
        {
            string sRutaImagenes = clsValidaciones.ObtenerUrlImages();
            string sImagen = sRutaImagenes + "sinResultados.jpg";
            string sTelefono = string.Empty;
            string sAgenciaTelefono = clsValidaciones.GetKeyOrAdd("Agencia_Telefono", "");

            switch (cParametros.Tipo)
            {
                case clsTipoError.Aplication:
                    try
                    {
                        if (cParametros.Severity.Equals(clsSeveridad.Alta))
                        {
                            try
                            {
                                sImagen = clsValidaciones.GetKeyOrAdd("ErrorCancel", sRutaImagenes + "cancel.png");
                            }
                            catch
                            {
                                sImagen = sRutaImagenes + "cancel.png";
                            }
                        }
                        else
                        {
                            sImagen = clsValidaciones.GetKeyOrAdd("ErrorAplicacion", sRutaImagenes + "advertencia.png");
                        }
                    }
                    catch
                    {
                        sImagen = sRutaImagenes + "advertencia.png";
                    }
                    break;

                case clsTipoError.DataBase:
                    try
                    {
                        if (cParametros.Severity.Equals(clsSeveridad.Alta))
                        {
                            try
                            {
                                sImagen = clsValidaciones.GetKeyOrAdd("ErrorCancel", sRutaImagenes + "cancel.png");
                            }
                            catch
                            {
                                sImagen = sRutaImagenes + "cancel.png";
                            }
                        }
                        else
                        {
                            sImagen = clsValidaciones.GetKeyOrAdd("ErrorDataBase", sRutaImagenes + "bd.png");
                        }
                    }
                    catch
                    {
                        sImagen = sRutaImagenes + "bd.png";
                    }
                    break;

                case clsTipoError.Library:
                    try
                    {
                        sImagen = clsValidaciones.GetKeyOrAdd("ErrorLibreria", sRutaImagenes + "advertencia.png");
                    }
                    catch
                    {
                        sImagen = sRutaImagenes + "advertencia.png";
                    }
                    break;

                case clsTipoError.WebServices:
                    try
                    {
                        sImagen = clsValidaciones.GetKeyOrAdd("ErrorWebServices", sRutaImagenes + "busqueda.png");
                    }
                    catch
                    {
                        sImagen = sRutaImagenes + "busqueda.png";
                    }
                    break;

                case clsTipoError.Record:
                    try
                    {
                        sImagen = clsValidaciones.GetKeyOrAdd("ErrorReserva", sRutaImagenes + "Reserva.jpg");
                    }
                    catch
                    {
                        sImagen = sRutaImagenes + "Reserva.jpg";
                    }
                    break;
            }
            try
            {
                if(sAgenciaTelefono.Length > 0)
                    sTelefono = "Llama ya a nuestro Call Center&nbsp;&nbsp;&nbsp;&nbsp;<br />" + sAgenciaTelefono;
            }
            catch { sTelefono = string.Empty; }

            if (cParametros.ViewMessage.Count == 0)
            {
                cParametros.ViewMessage.Add("Error de la aplicacion");
            }
            if (cParametros.Sugerencia.Count == 0)
            {
                cParametros.Sugerencia.Add("Por favor, intente nuevamente");
            }
            int iContMes = cParametros.ViewMessage.Count;
            int iContSug = cParametros.Sugerencia.Count;

            Table tResultados = new Table();

            TableRow trSuperior = new TableRow();
            TableCell tcSup1 = new TableCell();
            TableCell tcSup2 = new TableCell();
            Label lSup2 = new Label();
            TableCell tcSup3 = new TableCell();

            TableRow trResultado = new TableRow();
            TableCell tcResult1 = new TableCell();
            TableCell tcResult2 = new TableCell();
            TableCell tcResult3 = new TableCell();

            TableRow trInferior = new TableRow();
            TableCell tcInf1 = new TableCell();
            TableCell tcInf2 = new TableCell();
            TableCell tcInf3 = new TableCell();

            Label lDatos;

            tResultados.Rows.Add(getEspacio(30));
            StringBuilder SinResultados = new StringBuilder();

            SinResultados.AppendLine(" <table border='0' cellpadding='0' cellspacing='0'>    ");
            SinResultados.AppendLine(" <tr class='alineacionSuperior'>  ");
            SinResultados.AppendLine(" <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>  ");
            SinResultados.AppendLine(" <td colspan='2'> ");
            SinResultados.AppendLine(" <div class='callcenter'> ");
            SinResultados.AppendLine(" <span style='color:#F60; font-size:15px;'> ");
            if (!sTelefono.Length.Equals(0))
            {
                SinResultados.AppendLine(" <strong>" + sTelefono + "</strong>&nbsp; ");
            }
            SinResultados.AppendLine(" </span> ");
            SinResultados.AppendLine(" </div> ");
            SinResultados.AppendLine(" </td> ");
            SinResultados.AppendLine(" </tr> ");
            SinResultados.AppendLine(" <tr> ");
            SinResultados.AppendLine(" <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>  ");
            SinResultados.AppendLine(" <td> ");
            SinResultados.AppendLine(" <img alt='' src='" + sImagen + "' /> ");
            SinResultados.AppendLine(" </td> ");
            SinResultados.AppendLine(" <td align='center'> ");
            SinResultados.AppendLine(" <span class='tituloError'> ");
            for (int i = 0; i < iContMes; i++)
            {
                SinResultados.AppendLine(" " + cParametros.ViewMessage[i].ToString() + "<br /> ");
            }
            SinResultados.AppendLine(" <br /></span> ");
            SinResultados.AppendLine(" <span class='tituloSugerencia'> ");
            for (int i = 0; i < iContSug; i++)
            {
                SinResultados.AppendLine(" " + cParametros.Sugerencia[i].ToString() + "<br /> ");
            }
            SinResultados.AppendLine(" <br /></span> ");
            SinResultados.AppendLine(" </td> ");
            SinResultados.AppendLine(" </tr> ");
            SinResultados.AppendLine(" </table> ");

            #region [SUPERIOR]
            tcSup1.CssClass = "Resultado_Encabezado_1";
            tcSup2.CssClass = "Resultado_Encabezado_2_3_4";
            tcSup3.CssClass = "Resultado_Encabezado_7";

            lSup2.CssClass = "tituloRuta";

            lSup2.Text = SinResultados.ToString();
            tcSup2.Controls.Add(lSup2);
            tcSup2.Width = new Unit(100, UnitType.Percentage);
            trSuperior.Cells.Add(tcSup1);
            trSuperior.Cells.Add(tcSup2);
            trSuperior.Cells.Add(tcSup3);

            tResultados.Rows.Add(trSuperior);

            #endregion

            trResultado = new TableRow();
            tcResult1 = new TableCell();
            tcResult2 = new TableCell();
            tcResult3 = new TableCell();
            lDatos = new Label();

            tcResult1.CssClass = "resultado_1";
            tcResult2.CssClass = "resultado_2";
            tcResult3.CssClass = "resultado_8";

            tcResult1.Width = new Unit(9);
            lDatos.CssClass = "tituloRuta";
            lDatos.Text = string.Empty;
            tcResult2.Controls.Add(lDatos);

            trResultado.Cells.Add(tcResult1);
            trResultado.Cells.Add(tcResult2);
            trResultado.Cells.Add(tcResult3);

            tResultados.Rows.Add(trResultado);

            #region[INFERIOR]

            tcInf1.CssClass = "resultados_Inf_1";
            tcInf2.CssClass = "resultados_Inf_2";
            tcInf3.CssClass = "resultados_Inf_3";

            trInferior.Cells.Add(tcInf1);
            trInferior.Cells.Add(tcInf2);
            trInferior.Cells.Add(tcInf3);

            tResultados.Rows.Add(trInferior);

            #endregion

            tResultados.CssClass = "textoNormal";
            tResultados.CellPadding = 0;
            tResultados.CellSpacing = 0;
            tResultados.Width = new Unit(100, UnitType.Percentage);
            tResultados.BorderStyle = BorderStyle.None;

            return tResultados;
        }
        private TableRow getEspacio(int iHeight)
        {
            TableRow trEspacio;
            TableCell tcEspacio;

            trEspacio = new TableRow();
            tcEspacio = new TableCell();

            tcEspacio.CssClass = "divisionSpacer";
            trEspacio.CssClass = "divisionSpacer";

            if (!iHeight.Equals(0))
            {
                trEspacio.Height = new Unit(iHeight);
            }
            trEspacio.Cells.Add(tcEspacio);

            return trEspacio;
        }
        public void getError(clsParametros sParametros, Panel cPanel)
        {
            Table tResultados;

            cPanel.Controls.Clear();
            tResultados = new Table();
            if (sParametros != null)
            {
                tResultados = getMensajeT(sParametros);
            }
            cPanel.Controls.Add(tResultados);
        }
        public void getError(clsParametros sParametros, HtmlGenericControl cPanel)
        {
            try
            {
                cPanel.Controls.Clear();
                if (sParametros != null)
                {
                    cPanel.InnerHtml = getMensaje(sParametros);
                }
            }
            catch { }
        }
    }
    public class clsError
    {
        private const string TABLA_ERROR = "ErrorSsoft";

        private const string COLUMN_ID = "Id";
        private const string COLUMN_CODE = "Code";
        private const string COLUMN_MESSAGE = "Message";
        private const string COLUMN_SOURCE = "Source";
        private const string COLUMN_TIPO = "Tipo";
        private const string COLUMN_SEVERIDAD = "Severity";
        private const string COLUMN_STACKTRACE = "StackTrace";
        private const string COLUMN_VIEW_MESSAGE = "ViewMessage";
        private const string COLUMN_SUGERENCIA = "Sugerencia";
        private const string COLUMN_COMPLEMENTO = "Complemento";

        public clsError()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static void getError(clsResultados clError)
        {
            getError(clError.dsResultados, clError.Error);
        }
        public static void getError(DataSet dsData, clsParametros cError)
        {
            if (dsData == null)
            {
                dsData = new DataSet();
            }
            getTablaError(dsData);

            try
            {
                DataTable tblError = dsData.Tables[TABLA_ERROR];

                DataRow fila = tblError.NewRow();
                fila[COLUMN_ID] = cError.Id;
                fila[COLUMN_CODE] = cError.Code;
                fila[COLUMN_MESSAGE] = cError.Message;
                fila[COLUMN_SEVERIDAD] = cError.Severity;
                fila[COLUMN_TIPO] = cError.Tipo;
                fila[COLUMN_SOURCE] = cError.Source;
                fila[COLUMN_STACKTRACE] = cError.StackTrace;

                int iContador = cError.ViewMessage.Count;
                for (int i = 0; i < iContador; i++)
                {
                    if (i.Equals(0))
                    {
                        fila[COLUMN_VIEW_MESSAGE] = cError.ViewMessage[i].ToString();
                    }
                    else
                    {
                        fila[COLUMN_VIEW_MESSAGE] += "|" + cError.ViewMessage[i].ToString();
                    }
                }

                iContador = cError.Sugerencia.Count;
                for (int i = 0; i < iContador; i++)
                {
                    if (i.Equals(0))
                    {
                        fila[COLUMN_SUGERENCIA] = cError.Sugerencia[i].ToString();
                    }
                    else
                    {
                        fila[COLUMN_SUGERENCIA] += "|" + cError.Sugerencia[i].ToString();
                    }
                }

                fila[COLUMN_COMPLEMENTO] = cError.Complemento;
                tblError.Rows.Add(fila);
            }
            catch { }
        }
        public static void getTablaError(DataSet dsDatos)
        {
            DataTable tblError = new DataTable(TABLA_ERROR);
            if (!dsDatos.Tables.Contains(TABLA_ERROR))
            {
                tblError.Columns.Add(COLUMN_ID, typeof(int));
                tblError.Columns.Add(COLUMN_CODE, typeof(string));
                tblError.Columns.Add(COLUMN_MESSAGE, typeof(string));
                tblError.Columns.Add(COLUMN_SEVERIDAD, typeof(string));
                tblError.Columns.Add(COLUMN_TIPO, typeof(string));
                tblError.Columns.Add(COLUMN_SOURCE, typeof(string));
                tblError.Columns.Add(COLUMN_STACKTRACE, typeof(string));
                tblError.Columns.Add(COLUMN_VIEW_MESSAGE, typeof(string));
                tblError.Columns.Add(COLUMN_SUGERENCIA, typeof(string));
                tblError.Columns.Add(COLUMN_COMPLEMENTO, typeof(string));

                dsDatos.Tables.Add(tblError);
            }
        }
    }
}
