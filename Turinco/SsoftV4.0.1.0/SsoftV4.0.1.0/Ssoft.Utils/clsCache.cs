using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using Ssoft.ManejadorExcepciones;
using Ssoft.Sql;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;
using Ssoft.ValueObjects;
using Ssoft.Ssoft.ValueObjects.Hoteles;

namespace Ssoft.Utils
{
    [Serializable]
    public class clsCache
    {
        // Cache Perfil
        private string _gstrNombre = string.Empty;
        private string _gstrSessionID = string.Empty;
        private string _gstrUser = string.Empty;
        private string _gstrNivel = string.Empty;
        private string _gstrPass = string.Empty;
        private string _gstrPagina = string.Empty;
        private string _gstrContacto = string.Empty;

        //Vo planes busqueda
        private VO_ParametrosTarifas _gvParametrosPlanes;
      

        private string _gstrTipoContacto = string.Empty;
        private string _gstrRefereContacto = string.Empty;
        private string _gstrUrlActual = string.Empty;
        private string _gstrUrlAnterior = string.Empty;
        private string _gstrPantallaRespuestaLogin = string.Empty;
        private string _gstrAerolineaValidadora = string.Empty;

        // Configuraciones generales
        private string _gstrAplicacion = string.Empty;
        private string _gstrIdioma = string.Empty;
        private string _gstrCultura = string.Empty;
        private string _gstrFormatoFecha = string.Empty;
        private string _gstrFormatoFechaBD = string.Empty;
        private Enum_TipoVuelo _genmTipoVuelo = Enum_TipoVuelo.Nacional;
        private Enum_WebServices _genmTipoWebServices = Enum_WebServices.CotelcoHotel;
        private bool _gbolImpuestos = true;
        private string _gstrUNegocio = string.Empty;

        // Se anexan estas variables para llevar el registro del cliente

        private string _gstrNombres = string.Empty;
        private string _gstrApellidos = string.Empty;
        private string _gstrDireccion = string.Empty;
        private string _gstrTelefono = string.Empty;
        private string _gstrCelular = string.Empty;

        private string _gstrCiudad = string.Empty;
        private string _gstrTipo = string.Empty;
        private string _gstrTipoIdentificacion = string.Empty;
        private string _gstrEmail = string.Empty;
        private string _gstrIdentificacion = string.Empty;
        private string _gstrFechaNac = string.Empty;
        private string _gstrGenero = string.Empty;
        private string _gstrCodigoExterno = string.Empty;
        private string _gstrViajero = string.Empty;
        private string _gstrEmpresa = string.Empty;     
        private string _gstrProveedor = string.Empty;
       

        // Seguridad 
      
      
        private string _gintIndexSeleccion = string.Empty;

        //Corporativo
        private bool _gbolVerifica = false;

        // Proyecto 
        private string _gstrProyecto = string.Empty;
        private string _gstrCodPlan = string.Empty;
        // Column para el manejo de puntos de Visa
        private decimal _gdecPuntos = 1;
        private List<string> _gstrReserva;
        // Tiempo de inactividad 
        private int _gintTiempoCache = 0;
        private string _gstrTiempoInicial = string.Empty;
        private string _gstrTiempoFinal = string.Empty;
        // Origen - destino
        private VO_Aeropuerto _gvAeropuertoOrigen;
        private VO_Aeropuerto _gvAeropuertoDestino;
        //Pasajeros
        private List<VO_Passenger> _glvPassenger;
        // Datos Adicionales
        private List<string> _glDatosAdicionales;
        // Se adiciona ultimo
        private bool _gbolFacturacion = false;
        //Servicios Contratados
        private List<Enum_Global> _glvServiciosMB;
        private bool _gbolTerminos = false;

        private string _gstrPin = string.Empty;
        private string _gstrImagenTipoTC = string.Empty;
        private string _gstrImagenBanco = string.Empty;
        private string _gstrTipoTC = string.Empty;
        private string _gstrBanco = string.Empty;
        private string _gstrMoneda = string.Empty;


        public string Moneda
        {
            get { return _gstrMoneda; }
            set { _gstrMoneda = value; }
        }
        public string Nombre
        {
            get { return _gstrNombre; }
            set { _gstrNombre = value; }
        }

        public string SessionID
        {
            get { return _gstrSessionID; }
            set { _gstrSessionID = value; }
        }

        public string User
        {
            get { return _gstrUser; }
            set { _gstrUser = value; }
        }

        public string Nivel
        {
            get { return _gstrNivel; }
            set { _gstrNivel = value; }
        }

        public string Pass
        {
            get { return _gstrPass; }
            set { _gstrPass = value; }
        }

        public string Pagina
        {
            get { return _gstrPagina; }
            set { _gstrPagina = value; }
        }

       
        public string UrlActual
        {
            get { return _gstrUrlActual; }
            set { _gstrUrlActual = value; }
        }

        public string UrlAnterior
        {
            get { return _gstrUrlAnterior; }
            set { _gstrUrlAnterior = value; }
        }
        public string PantallaRespuestaLogin
        {
            get { return _gstrPantallaRespuestaLogin; }
            set { _gstrPantallaRespuestaLogin = value; }
        }
        public string AerolineaValidadora
        {
            get { return _gstrAerolineaValidadora; }
            set { _gstrAerolineaValidadora = value; }
        }

        public string TipoContacto
        {
            get { return _gstrTipoContacto; }
            set { _gstrTipoContacto = value; }
        }

        public string RefereContacto
        {
            get { return _gstrRefereContacto; }
            set { _gstrRefereContacto = value; }
        }

        // Idioma y pais
        public string Idioma
        {
            get { return _gstrIdioma; }
            set { _gstrIdioma = value; }
        }

        public string Cultura
        {
            get { return _gstrCultura; }
            set { _gstrCultura = value; }
        }

        public string Contacto
        {
            get { return _gstrContacto; }
            set { _gstrContacto = value; }
        }

        public string Identificacion
        {
            get { return _gstrIdentificacion; }
            set { _gstrIdentificacion = value; }
        }

        public string FechaNac
        {
            get { return _gstrFechaNac; }
            set { _gstrFechaNac = value; }
        }

        public string Genero
        {
            get { return _gstrGenero; }
            set { _gstrGenero = value; }
        }

        public string Nombres
        {
            get { return _gstrNombres; }
            set { _gstrNombres = value; }
        }

        public string Apellidos
        {
            get { return _gstrApellidos; }
            set { _gstrApellidos = value; }
        }

        public string Direccion
        {
            get { return _gstrDireccion; }
            set { _gstrDireccion = value; }
        }

        public string Telefono
        {
            get { return _gstrTelefono; }
            set { _gstrTelefono = value; }
        }

        public string Celular
        {
            get { return _gstrCelular; }
            set { _gstrCelular = value; }
        }  

        public string Ciudad
        {
            get { return _gstrCiudad; }
            set { _gstrCiudad = value; }
        }

        public string Aplicacion
        {
            get { return _gstrAplicacion; }
            set { _gstrAplicacion = value; }
        }

        public string Tipo
        {
            get { return _gstrTipo; }
            set { _gstrTipo = value; }
        }

        public string TipoIdentificacion
        {
            get { return _gstrTipoIdentificacion; }
            set { _gstrTipoIdentificacion = value; }
        }

        public string Email
        {
            get { return _gstrEmail; }
            set { _gstrEmail = value; }
        }

        public string Viajero
        {
            get { return _gstrViajero; }
            set { _gstrViajero = value; }
        }

        public string Empresa
        {
            get { return _gstrEmpresa; }
            set { _gstrEmpresa = value; }
        }

        public string UNegocio
        {
            get { return _gstrUNegocio; }
            set { _gstrUNegocio = value; }
        }

        public bool Verifica
        {
            get { return _gbolVerifica; }
            set { _gbolVerifica = value; }
        }

        public VO_ParametrosTarifas ParametrosPlanes
        {
            get { return _gvParametrosPlanes; }
            set { _gvParametrosPlanes = value; }
        }

        public string IndexSeleccion
        {
            get { return _gintIndexSeleccion; }
            set { _gintIndexSeleccion = value; }
        }

        public string Proyecto
        {
            get { return _gstrProyecto; }
            set { _gstrProyecto = value; }
        }

        public string CodPlan
        {
            get { return _gstrCodPlan; }
            set { _gstrCodPlan = value; }
        }

        public List<string> Reserva
        {
            get { return _gstrReserva; }
            set { _gstrReserva = value; }
        }

        public List<string> DatosAdicionales
        {
            get { return _glDatosAdicionales; }
            set { _glDatosAdicionales = value; }
        }

        public string FormatoFecha
        {
            get { return _gstrFormatoFecha; }
            set { _gstrFormatoFecha = value; }
        }

        public string FormatoFechaBD
        {
            get { return _gstrFormatoFechaBD; }
            set { _gstrFormatoFechaBD = value; }
        }

        public bool Impuestos
        {
            get { return _gbolImpuestos; }
            set { _gbolImpuestos = value; }
        }

        public Enum_TipoVuelo TipoVuelo
        {
            get { return _genmTipoVuelo; }
            set { _genmTipoVuelo = value; }
        }
        public Enum_WebServices TipoWebServices
        {
            get { return _genmTipoWebServices; }
            set { _genmTipoWebServices = value; }
        }
        public List<VO_Passenger> Passenger
        {
            get { return _glvPassenger; }
            set { _glvPassenger = value; }
        }

        public VO_Aeropuerto AeropuertoOrigen
        {
            get { return _gvAeropuertoOrigen; }
            set { _gvAeropuertoOrigen = value; }
        }

        public VO_Aeropuerto AeropuertoDestino
        {
            get { return _gvAeropuertoDestino; }
            set { _gvAeropuertoDestino = value; }
        }

        // Ultimo
        public bool Facturacion
        {
            get { return _gbolFacturacion; }
            set { _gbolFacturacion = value; }
        }

        // Ultimo
        public bool Terminos
        {
            get { return _gbolTerminos; }
            set { _gbolTerminos = value; }
        }

        public List<Enum_Global> ServiciosMB
        {
            get { return _glvServiciosMB; }
            set { _glvServiciosMB = value; }
        }

        // Tiempo de inactividad
        public int TiempoCache
        {
            get { return _gintTiempoCache; }
            set { _gintTiempoCache = value; }
        }
        public string TiempoInicial
        {
            get { return _gstrTiempoInicial; }
            set { _gstrTiempoInicial = value; }
        }
        public string TiempoFinal
        {
            get { return _gstrTiempoFinal; }
            set { _gstrTiempoFinal = value; }
        }
//Datos Vasa
        public string Pin
        {
            get { return _gstrPin; }
            set { _gstrPin = value; }
        }
        public string ImagenTipoTC
        {
            get { return _gstrImagenTipoTC; }
            set { _gstrImagenTipoTC = value; }
        }
        public string ImagenBanco
        {
            get { return _gstrImagenBanco; }
            set { _gstrImagenBanco = value; }
        }
        public string TipoTC
        {
            get { return _gstrTipoTC; }
            set { _gstrTipoTC = value; }
        }
        public string Banco
        {
            get { return _gstrBanco; }
            set { _gstrBanco = value; }
        }

        public clsCache()
        {
        }
    }
 
  
  
    public class clsRemark
    {
        #region [ ATRIBUTOS ]
        private Enum_ProveedorWebServices eOperador;
        private List<VO_Remarks> lsRemark;
        #endregion
        #region [ CONSTRUCTOR ]
        public clsRemark()
        {
        }
        #endregion
        #region [ PROPIEADES ]
        public Enum_ProveedorWebServices Operador
        {
            get { return eOperador; }
            set { eOperador = value; }
        }
        public List<VO_Remarks> Remark
        {
            get { return lsRemark; }
            set { lsRemark = value; }
        }
        #endregion
        #region [ DESTRUCTOR ]
        ~clsRemark() { }
        #endregion
    }
    public class clsConfiguracion
    {
        #region [ ATRIBUTOS ]
        private Enum_ProveedorWebServices eOperador;
        private List<clsPseudos> lsPseudos;
        private List<clsException> lsException;
        #endregion
        #region [ CONSTRUCTOR ]
        public clsConfiguracion()
        {
        }
        #endregion
        #region [ PROPIEADES ]
        public Enum_ProveedorWebServices Operador
        {
            get { return eOperador; }
            set { eOperador = value; }
        }
        public List<clsPseudos> lPseudos
        {
            get { return lsPseudos; }
            set { lsPseudos = value; }
        }
        public List<clsException> lException
        {
            get { return lsException; }
            set { lsException = value; }
        }
        #endregion
        #region [ DESTRUCTOR ]
        ~clsConfiguracion() { }
        #endregion
    }
    public class clsPseudos
    {
        #region [ ATRIBUTOS ]
        private int iIdPais;
        private string sPais;
        private string sPseudo;
        private int iIdPseudo;
        private bool bPseudoDefault;
        private bool bPol;
        private bool bValidation;

        #endregion
        #region [ CONSTRUCTOR ]
        public clsPseudos()
        {
        }
        #endregion
        #region [ PROPIEADES ]
        public int IdPais
        {
            get { return iIdPais; }
            set { iIdPais = value; }
        }
        public string Pais
        {
            get { return sPais; }
            set { sPais = value; }
        }
        public string Pseudo
        {
            get { return sPseudo; }
            set { sPseudo = value; }
        }
        public int IdPseudo
        {
            get { return iIdPseudo; }
            set { iIdPseudo = value; }
        }
        public bool PseudoDefault
        {
            get { return bPseudoDefault; }
            set { bPseudoDefault = value; }
        }
        public bool Pol
        {
            get { return bPol; }
            set { bPol = value; }
        }
        public bool Validation
        {
            get { return bValidation; }
            set { bValidation = value; }
        }
        #endregion
        #region [ DESTRUCTOR ]
        ~clsPseudos() { }
        #endregion
    }
    public class clsException
    {
        #region [ ATRIBUTOS ]
        private int iIdPais;
        private string sPais;
        private string sDescription;

        #endregion
        #region [ CONSTRUCTOR ]
        public clsException()
        {
        }
        #endregion
        #region [ PROPIEADES ]
        public int IdPais
        {
            get { return iIdPais; }
            set { iIdPais = value; }
        }
        public string Pais
        {
            get { return sPais; }
            set { sPais = value; }
        }
        public string Description
        {
            get { return sDescription; }
            set { sDescription = value; }
        }
        #endregion
        #region [ DESTRUCTOR ]
        ~clsException() { }
        #endregion
    }
   
    public class clsCacheControl
    {
       
        public clsCache RecuperarXML(string idSession)
        {
            clsCache objCache = new clsCache();
            try
            {
                string FileCache = "Cache_" + idSession;
                string strPathXML = clsValidaciones.CacheTempCrea();

                if (File.Exists(strPathXML + FileCache + ".xml"))
                {

                    TextReader txtReader = new StreamReader(strPathXML + FileCache + ".xml");

                    XmlSerializer SerializerRS = new XmlSerializer(typeof(clsCache));

                    objCache = (clsCache)SerializerRS.Deserialize(txtReader);
                    HttpContext.Current.Session["SessionIDLocal"] = idSession;
                    HttpContext.Current.Session["SessionID"] = idSession;

                    txtReader.Close();
                    txtReader.Dispose();
                    GuardaSesion(idSession);
                }
            }
            catch
            { objCache = null; }
            return objCache;
        }
        public clsCache ActualizaXML(clsCache objCache)
        {
            string FileCache = "Cache_" + objCache.SessionID.ToString();
         
            string strPathXML = clsValidaciones.CacheTempCrea();
            try
            {
                XmlSerializer SerializerRQ = new XmlSerializer(typeof(clsCache));
                StreamWriter WriterRQ = new StreamWriter(strPathXML + FileCache + ".xml");
                SerializerRQ.Serialize(WriterRQ, objCache);
                WriterRQ.Flush();
                WriterRQ.Close();
                clsSesiones.setCache(objCache);
                clsSesiones.setSesionIDLocal(objCache.SessionID.ToString());
            }
            catch { }
            GuardaSesion(objCache.SessionID.ToString());
            return objCache;
        }
        public void RemoverSesion()
        {
            try
            {
                HttpContext.Current.Session.Remove("Contacto");
                HttpContext.Current.Session.Remove("Nombre");
                HttpContext.Current.Session.Remove("Nombres");
                HttpContext.Current.Session.Remove("User");
                HttpContext.Current.Session.Remove("Nivel");
                HttpContext.Current.Session.Remove("Pass");
                HttpContext.Current.Session.Remove("Pagina");

                HttpContext.Current.Session.Remove("UrlActual");
                HttpContext.Current.Session.Remove("UrlAnterior");
                HttpContext.Current.Session.Remove("$pantallaLogin");
                HttpContext.Current.Session.Remove("AerolineaValidadora");

                HttpContext.Current.Session.Remove("Idioma");
                HttpContext.Current.Session.Remove("Cultura");
                HttpContext.Current.Session.Remove("Identificacion");
                HttpContext.Current.Session.Remove("Apellidos");
                HttpContext.Current.Session.Remove("Telefono");
                HttpContext.Current.Session.Remove("Celular");
                HttpContext.Current.Session.Remove("Pais");
                HttpContext.Current.Session.Remove("Estado");
                HttpContext.Current.Session.Remove("Ciudad");
                HttpContext.Current.Session.Remove("RazonSocial");
                HttpContext.Current.Session.Remove("Aplicacion");
                HttpContext.Current.Session.Remove("Tipo");
                HttpContext.Current.Session.Remove("TipoIdentificacion");
                HttpContext.Current.Session.Remove("Email");
                HttpContext.Current.Session.Remove("CodigoConvenio");
                HttpContext.Current.Session.Remove("TipoConvenio");
                HttpContext.Current.Session.Remove("SessionIDLocal");
                HttpContext.Current.Session.Remove("Viajero");
                HttpContext.Current.Session.Remove("Empresa");
                HttpContext.Current.Session.Remove("Comunidad");
                HttpContext.Current.Session.Remove("Agencia");
                HttpContext.Current.Session.Remove("Propietario");
                HttpContext.Current.Session.Remove("LogoAgencia");
                HttpContext.Current.Session.Remove("UNegocio");
                HttpContext.Current.Session.Remove("Verifica");
                HttpContext.Current.Session.Remove("IndexSeleccion");
                HttpContext.Current.Session.Remove("Direccion");
                HttpContext.Current.Session.Remove("Proveedor");
                HttpContext.Current.Session.Remove("FechaNac");
                HttpContext.Current.Session.Remove("Genero");

                HttpContext.Current.Session.Remove("TipoContacto");
                HttpContext.Current.Session.Remove("RefereContacto");
                HttpContext.Current.Session.Remove("GroupUser");
                HttpContext.Current.Session.Remove("ContactoPadre");
                HttpContext.Current.Session.Remove("Corporativo");
                HttpContext.Current.Session.Remove("Parametros");
                HttpContext.Current.Session.Remove("Passenger");

                HttpContext.Current.Session.Remove("AnalisisAhorro");
                //HttpContext.Current.Session.Remove("Remarks");

                HttpContext.Current.Session.Remove("FormatoFecha");
                HttpContext.Current.Session.Remove("FormatoFechaBD");

                HttpContext.Current.Session.Remove("Impuestos");
                HttpContext.Current.Session.Remove("TipoVuelo");

                HttpContext.Current.Session.Remove("Proyecto");
                HttpContext.Current.Session.Remove("Reserva");
                HttpContext.Current.Session.Remove("DatosAdicionales");

                HttpContext.Current.Session.Remove("VoOtaAirLowFareSearchLLSRQ");
                HttpContext.Current.Session.Remove("VoOtaVehAvailRateLLSRQ");
                //HttpContext.Current.Session.Remove("Vo_HotelValuedAvailRQ");

                HttpContext.Current.Session.Remove("ConvenioDescuento");
                HttpContext.Current.Session.Remove("TipoCliente");
                HttpContext.Current.Session.Remove("IsUserAtenea");
                HttpContext.Current.Session.Abandon();
            }
            catch { }
        }
        public void EliminarCache(string idSession)
        {
            string strPathXML = clsValidaciones.CacheTempCrea();
            try
            {
                string FileCache = "Cache_" + idSession;
                string FileReserva = "Reserva" + idSession;
                //string strPathXML = clsValidaciones.CacheTempCrea();

                if (File.Exists(strPathXML + FileCache + ".xml"))
                {
                    File.Delete(strPathXML + FileCache + ".xml");
                }
                if (File.Exists(strPathXML + idSession + ".xml"))
                {
                    File.Delete(strPathXML + idSession + ".xml");
                }
                if (File.Exists(strPathXML + FileReserva + ".xml"))
                {
                    File.Delete(strPathXML + FileReserva + ".xml");
                }
                
                Cache cCache = new Cache();
                clsSesiones.CLEAR_SESSION_ALL();
                clsSesiones.CLEAR_SESSION_CACHE();
                clsSesiones.CLEAR_SESSION_CORPORATIVO();
                clsSesiones.CLEAR_SESSION_CREDENCIALES();
                clsSesiones.CLEAR_SESSION_RESERVA();
                clsSesiones.CLEAR_SESSION_PROYECTO();
                clsSesiones.CLEAR_SESSION_PARAMETROS_BUSQUEDA();
                clsAccount.EliminarAccount();
                try
                {
                    cCache.Remove(idSession);
                }
                catch
                {
                }
            }
            catch { }
            try { clsValidaciones.BorrarArchivos(strPathXML); }
            catch { }
            try { clsValidaciones.DocumentosTempElimina(); }
            catch { }
            try { clsValidaciones.XMLDatasetElimina(); }
            catch { }
        }
        /// <summary>
        /// Metodo para el boorado de la cache dependiendo de los minutos
        /// </summary>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public void BorrarCache()
        {
            string strPathXML = clsValidaciones.CacheTempCrea();
            clsValidaciones.BorrarArchivos(strPathXML);
        }
        public void EliminarCache()
        {
            try
            {
                string idSession = RecuperarSesionId();
                EliminarCache(idSession);
            }
            catch { }
        }
        public string RecuperarSesionId()
        {
            try
            {
                Page PaginaActual = (Page)HttpContext.Current.Handler;
                return RecuperarSesionId(PaginaActual);
            }
            catch { return null; }
        }
        public void GuardaSesion()
        {
            try
            {
                RecuperarSesionId();
            }
            catch { }
        }
        public void GuardaSesion(string SesionId)
        {
            try
            {
                if (SesionId.Length > 0)
                {
                    clsSesiones.setSesionIDLocal(SesionId);
                    clsSesiones.setSesionID(SesionId);
                    try
                    {
                        HttpCookie cookieSet = new HttpCookie("SessionID", SesionId);
                        cookieSet.Expires = DateTime.Today.AddDays(1).AddSeconds(-1);
                        HttpContext.Current.Response.Cookies.Add(cookieSet);
                        
                    }
                    catch { }
                    try
                    {
                        Page PaginaActual = (Page)HttpContext.Current.Handler;
                        HiddenField hdSesion = (HiddenField)PaginaActual.FindControl("hdfSesionId");
                        if (hdSesion != null)
                            hdSesion.Value = SesionId;
                    }
                    catch { }
                }
            }
            catch { }
        }
        public string RecuperarSesionId(Page PaginaActual)
        {
            bool bSesion = false;
            string sSesion = clsSesiones.getSesionIDLocal();

            HiddenField hdSesion = (HiddenField)PaginaActual.FindControl("hdfSesionId");

            try
            {
                if (hdSesion != null)
                {
                    if (!hdSesion.Value.Length.Equals(0))
                    {
                        sSesion = hdSesion.Value;
                        GuardaSesion(sSesion);
                        bSesion = true;
                    }
                    else
                    {
                        if (HttpContext.Current.Request.QueryString["idSesion"] != null)
                        {
                            try
                            {
                                if (!HttpContext.Current.Request.QueryString["idSesion"].ToString().Length.Equals(0))
                                {
                                    sSesion = HttpContext.Current.Request.QueryString["idSesion"].ToString();
                                    GuardaSesion(sSesion);
                                    bSesion = true;
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            if (sSesion != null)
                            {
                                sSesion = hdSesion.Value;
                                GuardaSesion(sSesion);
                                bSesion = true;
                            }
                            else
                            {
                                sSesion = clsSesiones.getSesionID();
                                if (sSesion != null)
                                {
                                    sSesion = hdSesion.Value;
                                    GuardaSesion(sSesion);
                                    bSesion = true;
                                }
                                else
                                {
                                    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("SessionID");
                                    if (cookie != null)
                                    {
                                        try
                                        {
                                            if (!cookie.Value.ToString().Length.Equals(0))
                                            {
                                                if (cookie.Expires < DateTime.Now)
                                                {
                                                    sSesion = cookie.Value.ToString();
                                                    GuardaSesion(sSesion);
                                                    bSesion = true;
                                                }
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                    }
                }
                if (!bSesion)
                {
                    if (sSesion == null)
                        sSesion = string.Empty;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Alta;
                try
                {
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.InnerException = Ex.InnerException.Message.ToString();
                    cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                }
                catch { }
                cParametros.Info = "No se puede recuperar la sesion";
                ExceptionHandled.Publicar(cParametros);
            }
            return sSesion;
        }
        public void RecuperarSesionId(string sSesion)
        {
            Page PaginaActual = (Page)HttpContext.Current.Handler;
            HiddenField hdSesion = (HiddenField)PaginaActual.FindControl("hdfSesionId");
            clsParametros cParametros = new clsParametros();
            try
            {
                if (hdSesion != null)
                {
                    hdSesion.Value = sSesion;
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Message = "El campo oculto hdfSesionId, no existe en la pagina " + PaginaActual.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Alta;
                    try
                    {
                        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    }
                    catch { }
                    cParametros.Info = "Sesion recuperada " + sSesion;
                    ExceptionHandled.Publicar(cParametros);
                }
                GuardaSesion(sSesion);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                try
                {
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                }
                catch { }
                cParametros.Info = "No se puede recuperar la sesion.";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public clsCache RecuperarSesion(string SesionId)
        {
            clsCache cCache = null;
            try { cCache = RecuperarXML(SesionId); }
            catch { }
            return cCache;
        }
        public string CrearSession()
        {/*CREAMOS UN STRING CON LOS TICKS DE LA FECHA ACTUAL*/
            //return string.Empty;
            return DateTime.Now.Ticks.ToString();
        }
       
    }
    public class csCache
    {
        public csCache()
        {
        }
        public clsCache cCache()
        {
            clsCacheControl cCacheControl = new clsCacheControl();
            Page PageActual = null;
            if (HttpContext.Current.Handler is Page)
                PageActual = HttpContext.Current.Handler as Page;

            string sSesion = null;
            clsCache cCache = new clsCache();
            if (PageActual != null)
            {
                cCache = clsSesiones.getCache(PageActual);
            }
            else
            {
                sSesion = cCacheControl.RecuperarSesionId();
                if (sSesion != null)
                {
                    cCache = cCacheControl.RecuperarSesion(sSesion);
                    cCacheControl.GuardaSesion(sSesion);
                }
                else
                {
                    cCache = null;
                }
            }
            return cCache;
        }
        public clsCache cCache(Page PageActual)
        {
            clsCacheControl cCacheControl = new clsCacheControl();
            clsCache cCache = new clsCache();
            cCache = clsSesiones.getCache(PageActual);
            string sSesion = null;
            if (cCache != null)
            {
                cCacheControl.GuardaSesion(sSesion);
            }
            else
            {
                sSesion = cCacheControl.RecuperarSesionId(PageActual);
                if (sSesion != null)
                {
                    cCache = cCacheControl.RecuperarSesion(sSesion);
                }
                else
                {
                    cCache = null;
                }
            }
            return cCache;
        }
        public void setError(UserControl PageSource, clsParametros cParametros)
        {
            HtmlGenericControl dPanel = (HtmlGenericControl)PageSource.FindControl("dPanel");

            clsErrorMensaje cError = new clsErrorMensaje();
            cError.getError(cParametros, dPanel);
        }
        public static void IniciaProyecto(clsCache cCache)
        {
            try
            {
                clsSesiones.CLEAR_SESSION_PROYECTO();
            }
            catch { }
        }
        public static void IniciaProyecto()
        {
            try
            {
                clsSesiones.CLEAR_SESSION_PROYECTO();
            }
            catch { }
        }
        public static void ActualizarCache(clsCache cCache)
        {
            try
            {
                clsCacheControl cCacheControl = new clsCacheControl();
                cCacheControl.ActualizaXML(cCache);
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                try
                {
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                }
                catch { }
                cParametros.Info = "Actualizar cache";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public static void GuardarPaxCache(List<VO_Passenger> lvo_Passenger)
        {
            clsCache cCache = new csCache().cCache();
            cCache.Passenger = lvo_Passenger;
            ActualizarCache(cCache);
        }
        public string csCodigoExternoidE()
        {
            string sValue = "0";
            try
            {
                if (HttpContext.Current.Request.QueryString["idE"] != null)
                {
                    sValue = HttpContext.Current.Request.QueryString["idE"].ToString();
                    if (sValue.Contains(","))
                        sValue = sValue.Substring(0, sValue.IndexOf(","));
                }
                else
                {
                    if (HttpContext.Current.Request.QueryString["idEmpresa"] != null)
                    {
                        sValue = HttpContext.Current.Request.QueryString["idEmpresa"].ToString();
                    }
                    else
                    {
                        sValue = clsValidaciones.GetKeyOrAdd("idEmpresa", "3");
                    }
                    if (sValue.Contains(","))
                        sValue = sValue.Substring(0, sValue.IndexOf(","));
                }
            }
            catch
            {
            }
            return sValue;
        }
        public string csCodigoExternoidC()
        {
            string sValue = "0";
            try
            {
                if (HttpContext.Current.Request.QueryString["idC"] != null)
                {
                    sValue = HttpContext.Current.Request.QueryString["idC"].ToString();
                    if (sValue.Contains(","))
                        sValue = sValue.Substring(0, sValue.IndexOf(","));
                }
                else
                {
                    sValue = clsValidaciones.GetKeyOrAdd("idContacto", "0");
                }
                if (sValue.Contains(","))
                    sValue = sValue.Substring(0, sValue.IndexOf(","));
            }
            catch
            {
            }
            return sValue;
        }
        public string csCodigoExterno()
        {
            string sValue = csCodigoExternoidC();
            try
            {
                if (sValue.Equals("0"))
                {
                    sValue = csCodigoExternoidE();
                }
            }
            catch
            {
            }
            return sValue;
        }     
    }

    public class csCacheParam
    {
        public csCacheParam()
        {
        }
        public void cGuardaParamAir(VO_OTA_AirLowFareSearchLLSRQ vo_Object)
        {
            try
            {
                string idSession = new clsCacheControl().RecuperarSesionId();
                string FileCache = "vo_OTA_AirLowFareSearchLLSRQ_" + idSession;
                string strPathXML = clsValidaciones.CacheTempCrea();
                try
                {
                    XmlSerializer SerializerRQ = new XmlSerializer(typeof(VO_OTA_AirLowFareSearchLLSRQ));
                    StreamWriter WriterRQ = new StreamWriter(strPathXML + FileCache + ".xml");
                    SerializerRQ.Serialize(WriterRQ, vo_Object);
                    WriterRQ.Flush();
                    WriterRQ.Close();
                }
                catch { }
            }
            catch { }
        }
        public VO_OTA_AirLowFareSearchLLSRQ cRecuperaParamAir()
        {
            VO_OTA_AirLowFareSearchLLSRQ vo_Object = null;
            try
            {
                string idSession = new clsCacheControl().RecuperarSesionId();
                string FileCache = "vo_OTA_AirLowFareSearchLLSRQ_" + idSession;
                string strPathXML = clsValidaciones.CacheTempCrea();
                TextReader txtReader = new StreamReader(strPathXML + FileCache + ".xml");
                XmlSerializer SerializerRS = new XmlSerializer(typeof(VO_OTA_AirLowFareSearchLLSRQ));
                vo_Object = (VO_OTA_AirLowFareSearchLLSRQ)SerializerRS.Deserialize(txtReader);
                txtReader.Close();
                txtReader.Dispose();
            }
            catch { }
            return vo_Object;
        }    
        public void cGuardaDsResult(DataSet dsData, string sObjeto)
        {
            try
            {
                string idSession = new clsCacheControl().RecuperarSesionId();
                string FileCache = sObjeto + "_" + idSession;
                new clsSerializer().DatasetXML(dsData, FileCache);
            }
            catch { }
        }
        public DataSet cRecuperaDsResult(string sObjeto)
        {
            DataSet dsData = null;
            try
            {
                string idSession = new clsCacheControl().RecuperarSesionId();
                string FileCache = sObjeto + "_" + idSession;
                dsData = new clsSerializer().XMLDataset(FileCache);
            }
            catch { }
            return dsData;
        }
        public void cEliminaXml(string sObject)
        {
            string idSession = new clsCacheControl().RecuperarSesionId();
            try
            {
                string FileCache = sObject + "_" + idSession;
                string strPathXML = clsValidaciones.CacheTempCrea();

                if (File.Exists(strPathXML + FileCache + ".xml"))
                {
                    File.Delete(strPathXML + FileCache + ".xml");
                }
            }
            catch { }
        }
        public void cGuardaParamHot(VO_HotelValuedAvailRQ vo_Object)
        {
            try
            {
                string idSession = new clsCacheControl().RecuperarSesionId();
                string FileCache = "vo_HotelValuedAvailRQ" + idSession;
                string strPathXML = clsValidaciones.CacheTempCrea();
                try
                {
                    XmlSerializer SerializerRQ = new XmlSerializer(typeof(VO_HotelValuedAvailRQ));
                    StreamWriter WriterRQ = new StreamWriter(strPathXML + FileCache + ".xml");
                    SerializerRQ.Serialize(WriterRQ, vo_Object);
                    WriterRQ.Flush();
                    WriterRQ.Close();
                }
                catch { }
            }
            catch { }
        }
        public VO_HotelValuedAvailRQ cRecuperaParamHot()
        {
            VO_HotelValuedAvailRQ vo_Object = null;
            try
            {
                string idSession = new clsCacheControl().RecuperarSesionId();
                string FileCache = "vo_HotelValuedAvailRQ" + idSession;
                string strPathXML = clsValidaciones.CacheTempCrea();
                TextReader txtReader = new StreamReader(strPathXML + FileCache + ".xml");
                XmlSerializer SerializerRS = new XmlSerializer(typeof(VO_HotelValuedAvailRQ));
                vo_Object = (VO_HotelValuedAvailRQ)SerializerRS.Deserialize(txtReader);
                txtReader.Close();
                txtReader.Dispose();
            }
            catch { }
            return vo_Object;
        }
    }
}



