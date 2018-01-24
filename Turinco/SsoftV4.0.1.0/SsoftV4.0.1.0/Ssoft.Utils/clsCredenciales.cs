using System;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using Ssoft.ValueObjects;
using Ssoft.ManejadorExcepciones;

namespace Ssoft.Utils
{
    public class clsCredenciales
    {
        public static VO_Credentials Credenciales(Enum_ProveedorWebServices eWebServices)
        {
            VO_Credentials vo_Credentials = new VO_Credentials();
            try
            {
                switch (eWebServices)
                {
                    case Enum_ProveedorWebServices.Sabre:
                        vo_Credentials = clsConfiguracionSabre.Credentials();
                        break;
                    case Enum_ProveedorWebServices.POL:
                        vo_Credentials = clsConfiguracionPOL.Credentials();
                        break;
                    case Enum_ProveedorWebServices.TotalTrip:
                        vo_Credentials = clsConfiguracionTotalTrip.Credentials();
                        break;
                    default:
                        break;
                }
            }
            catch { }
            return vo_Credentials;
        }
    }

    public class clsConfiguracionSabre
    {
        /// <summary>
        /// Obtiene la ruta del servicio Xml de Hotelbeds
        /// </summary>
        public static string UrlWebService
        {
            get { return (string)XmlADS["UrlWebservices"]; }
        }
        /// <summary>
        /// Indica si Hotelbeds se encuentra en línea o fuera de línea
        /// </summary>
        /// <summary>
        /// Permite acceso a los recursos del web.config
        /// </summary>
        private static NameValueCollection XmlADS
        {
            get
            {
                NameValueCollection objXmlADS;

                objXmlADS = (NameValueCollection)ConfigurationManager.
                                            GetSection("system.web/XmlSabreADS");
                if (objXmlADS == null)
                {
                    objXmlADS = (NameValueCollection)ConfigurationManager.
                                          GetSection("XmlSabreADS");
                }

                return objXmlADS;
            }
        }
        /// <summary>
        /// Obtiene el usuario del servidor proxy
        /// </summary>
        public static string User
        {
            get { return (string)XmlADS["User"]; }
        }

        /// <summary>
        /// Obtiene la clave del usuario del servidor proxy
        /// </summary>
        public static string Password
        {
            get { return (string)XmlADS["Password"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Ipcc
        {
            get { return (string)XmlADS["Ipcc"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Pcc
        {
            get { return (string)XmlADS["Pcc"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string PccPais
        {
            get { return (string)XmlADS["PccPais"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string From
        {
            get { return (string)XmlADS["From"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string To
        {
            get { return (string)XmlADS["To"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Dominio
        {
            get { return (string)XmlADS["Dominio"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string QNumber
        {
            get { return (string)XmlADS["QNumber"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Conversacion
        {
            get { return (string)XmlADS["Conversacion"]; }
        }
        //<!-- *************************   Agencia   ************************ -->
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Mensaje
        {
            get { return (string)XmlADS["Mensaje"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Year
        {
            get { return (string)XmlADS["Year"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Con
        {
            get { return (string)XmlADS["Con"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Namespace
        {
            get { return (string)XmlADS["Namespace"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_Nombre
        {
            get { return (string)XmlADS["Agencia_Nombre"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_Direccion
        {
            get { return (string)XmlADS["Agencia_Direccion"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_Ciudad
        {
            get { return (string)XmlADS["Agencia_Ciudad"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_CodigoPostal
        {
            get { return (string)XmlADS["Agencia_CodigoPostal"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_CodigoEstado
        {
            get { return (string)XmlADS["Agencia_CodigoEstado"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_CodigoPais
        {
            get { return (string)XmlADS["Agencia_CodigoPais"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_CodigoArea
        {
            get { return (string)XmlADS["Agencia_CodigoArea"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_Telefono
        {
            get { return (string)XmlADS["Agencia_Telefono"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_CodigoLocalizacion
        {
            get { return (string)XmlADS["Agencia_CodigoLocalizacion"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_TicketeFecha
        {
            get { return (string)XmlADS["Agencia_TicketeFecha"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_TicketeId
        {
            get { return (string)XmlADS["Agencia_TicketeId"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_TicketeManual
        {
            get { return (string)XmlADS["Agencia_TicketeManual"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string Agencia_TicketTimeLimit
        {
            get { return (string)XmlADS["Agencia_TicketTimeLimit"]; }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static int SegmentoFuturo
        {
            get
            {
                int iSegmentoFuturo = 0;
                try
                {
                    iSegmentoFuturo = (int)int.Parse(XmlADS["SegmentoFuturo"].ToString());
                }
                catch { }
                return iSegmentoFuturo;
            }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static int TimeLimit
        {
            get
            {
                int iTimeLimit = 8;
                try
                {
                    iTimeLimit = (int)int.Parse(XmlADS["TimeLimit"].ToString());
                }
                catch { }
                return iTimeLimit;
            }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string SnapCode
        {
            get
            {
                string sSnapCode = null;
                try
                {
                    sSnapCode = (string)XmlADS["SnapCode"];
                }
                catch { }
                return sSnapCode;
            }
        }
        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static string PseudoPerfil
        {
            get
            {
                string sPseudoPerfil = null;
                try
                {
                    sPseudoPerfil = (string)XmlADS["PseudoPerfil"];
                }
                catch { }
                return sPseudoPerfil;
            }
        }
        public static VO_Credentials Credentials()
        {
            VO_Credentials vo_Credentials = new VO_Credentials();
            clsParametros cParametros = new clsParametros();
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            bool bCredentials = false;
            string sPccPais = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");
            try
            {
                if (clsConfiguracionSabre.PccPais != null)
                    sPccPais = clsConfiguracionSabre.PccPais;

            }
            catch { }
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    //if (cCache.Corporativo != null)
                    //{
                    //    int iTotal = cCache.Corporativo.Count;
                    //    for (int i = 0; i < iTotal; i++)
                    //    {
                    //        if (cCache.Corporativo[i].ProveedorWs.Equals(Enum_ProveedorWebServices.Sabre))
                    //        {
                    //            vo_Credentials = cCache.Corporativo[i].Credentials;
                             bCredentials = false;
                    //        }
                    //    }
                    //}
                }
                if (!bCredentials)
                {
                    vo_Credentials.User = clsConfiguracionSabre.User;
                    vo_Credentials.Password = clsConfiguracionSabre.Password;
                    vo_Credentials.Conversacion = clsConfiguracionSabre.Conversacion;
                    vo_Credentials.Dominio = clsConfiguracionSabre.Dominio;
                    vo_Credentials.From = clsConfiguracionSabre.From;
                    vo_Credentials.Ipcc = clsConfiguracionSabre.Ipcc;
                    vo_Credentials.Pcc = clsConfiguracionSabre.Pcc;
                    vo_Credentials.PccDefault = clsConfiguracionSabre.Pcc;
                    vo_Credentials.Pseudo = 0;
                    vo_Credentials.PseudoDefault = 0;
                    vo_Credentials.PccDefaultPais = sPccPais;
                    vo_Credentials.Mensaje = clsConfiguracionSabre.Mensaje;
                    vo_Credentials.QNumber = clsConfiguracionSabre.QNumber;
                    vo_Credentials.To = clsConfiguracionSabre.To;
                    vo_Credentials.SnapCode = clsConfiguracionSabre.SnapCode;
                    vo_Credentials.PseudoPerfil = clsConfiguracionSabre.PseudoPerfil;
                    vo_Credentials.UrlWebServices = clsConfiguracionSabre.UrlWebService;
                    vo_Credentials.Agencia_Ciudad = clsConfiguracionSabre.Agencia_Ciudad;
                    vo_Credentials.Agencia_CodigoArea = clsConfiguracionSabre.Agencia_CodigoArea;
                    vo_Credentials.Agencia_CodigoEstado = clsConfiguracionSabre.Agencia_CodigoEstado;
                    vo_Credentials.Agencia_CodigoLocalizacion = clsConfiguracionSabre.Agencia_CodigoLocalizacion;
                    vo_Credentials.Agencia_CodigoPais = clsConfiguracionSabre.Agencia_CodigoPais;
                    vo_Credentials.Agencia_CodigoPostal = clsConfiguracionSabre.Agencia_CodigoPostal;
                    vo_Credentials.Agencia_Direccion = clsConfiguracionSabre.Agencia_Direccion;
                    vo_Credentials.Agencia_Nombre = clsConfiguracionSabre.Agencia_Nombre;
                    vo_Credentials.Agencia_Telefono = clsConfiguracionSabre.Agencia_Telefono;
                    vo_Credentials.Agencia_TiketeId = clsConfiguracionSabre.Agencia_TicketeId;
                    vo_Credentials.Agencia_TiketeManual = clsConfiguracionSabre.Agencia_TicketeManual;
                    vo_Credentials.Agencia_TiketTimeLimit = clsConfiguracionSabre.Agencia_TicketTimeLimit;
                    vo_Credentials.NameEspace = clsConfiguracionSabre.Namespace;
                    vo_Credentials.SegmentoFuturo = clsConfiguracionSabre.SegmentoFuturo;
                    vo_Credentials.TimeLimit = clsConfiguracionSabre.TimeLimit;
                }
                try
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ != null)
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.SPseudoPlanes != null)
                        {
                            vo_Credentials.Pcc = vo_OTA_AirLowFareSearchLLSRQ.SPseudoPlanes.ToString();
                        }
                    }
                }
                catch { }
            }
            catch 
            {
                if (!bCredentials)
                {
                    try
                    {
                        vo_Credentials.User = clsConfiguracionSabre.User;
                        vo_Credentials.Password = clsConfiguracionSabre.Password;
                        vo_Credentials.Conversacion = clsConfiguracionSabre.Conversacion;
                        vo_Credentials.Dominio = clsConfiguracionSabre.Dominio;
                        vo_Credentials.From = clsConfiguracionSabre.From;
                        vo_Credentials.Ipcc = clsConfiguracionSabre.Ipcc;
                        vo_Credentials.Pcc = clsConfiguracionSabre.Pcc;
                        vo_Credentials.PccDefault = clsConfiguracionSabre.Pcc;
                        vo_Credentials.Pseudo = 0;
                        vo_Credentials.PseudoDefault = 0;
                        vo_Credentials.PccDefaultPais = sPccPais;
                        vo_Credentials.Mensaje = clsConfiguracionSabre.Mensaje;
                        vo_Credentials.QNumber = clsConfiguracionSabre.QNumber;
                        vo_Credentials.To = clsConfiguracionSabre.To;
                        vo_Credentials.SnapCode = clsConfiguracionSabre.SnapCode;
                        vo_Credentials.PseudoPerfil = clsConfiguracionSabre.PseudoPerfil;
                        vo_Credentials.UrlWebServices = clsConfiguracionSabre.UrlWebService;
                        vo_Credentials.Agencia_Ciudad = clsConfiguracionSabre.Agencia_Ciudad;
                        vo_Credentials.Agencia_CodigoArea = clsConfiguracionSabre.Agencia_CodigoArea;
                        vo_Credentials.Agencia_CodigoEstado = clsConfiguracionSabre.Agencia_CodigoEstado;
                        vo_Credentials.Agencia_CodigoLocalizacion = clsConfiguracionSabre.Agencia_CodigoLocalizacion;
                        vo_Credentials.Agencia_CodigoPais = clsConfiguracionSabre.Agencia_CodigoPais;
                        vo_Credentials.Agencia_CodigoPostal = clsConfiguracionSabre.Agencia_CodigoPostal;
                        vo_Credentials.Agencia_Direccion = clsConfiguracionSabre.Agencia_Direccion;
                        vo_Credentials.Agencia_Nombre = clsConfiguracionSabre.Agencia_Nombre;
                        vo_Credentials.Agencia_Telefono = clsConfiguracionSabre.Agencia_Telefono;
                        vo_Credentials.Agencia_TiketeId = clsConfiguracionSabre.Agencia_TicketeId;
                        vo_Credentials.Agencia_TiketeManual = clsConfiguracionSabre.Agencia_TicketeManual;
                        vo_Credentials.Agencia_TiketTimeLimit = clsConfiguracionSabre.Agencia_TicketTimeLimit;
                        vo_Credentials.NameEspace = clsConfiguracionSabre.Namespace;
                        vo_Credentials.SegmentoFuturo = clsConfiguracionSabre.SegmentoFuturo;
                        vo_Credentials.TimeLimit = clsConfiguracionSabre.TimeLimit;
                    }
                    catch 
                    {
                    }
                }
                try
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ != null)
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.SPseudoPlanes != null)
                        {
                            vo_Credentials.Pcc = vo_OTA_AirLowFareSearchLLSRQ.SPseudoPlanes.ToString();
                        }
                    }
                }
                catch { }
            }
            return vo_Credentials;
        }
    }
    public class clsConfiguracionPOL
    {
        /// <summary>
        /// Indica si Hotelbeds se encuentra en línea o fuera de línea
        /// </summary>
        /// <summary>
        /// Permite acceso a los recursos del web.config
        /// </summary>
        private static NameValueCollection XmlADS
        {
            get
            {
                NameValueCollection objXmlADS;

                objXmlADS = (NameValueCollection)ConfigurationManager.
                                            GetSection("system.web/XmlPOLADS");
                if (objXmlADS == null)
                {
                    objXmlADS = (NameValueCollection)ConfigurationManager.
                                          GetSection("XmlPOLADS");
                }

                return objXmlADS;
            }
        }
        /// <summary>
        /// usuario local de la BD de POL
        /// </summary>
        public static string Usuario
        {
            get { return (string)XmlADS["Usuario"]; }
        }
        /// <summary>
        /// Plantilla personalizada para Visa
        /// </summary>
        public static string PlantillaVisa
        {
            get { return (string)XmlADS["PlantillaVisa"]; }
        }
        /// <summary>
        /// Plantilla personalizada para Diners
        /// </summary>
        public static string PlantillaDiners
        {
            get { return (string)XmlADS["PlantillaDiners"]; }
        }

        /// <summary>
        /// Plantilla personalizada para PSE
        /// </summary>
        public static string PlantillaPSE
        {
            get { return (string)XmlADS["PlantillaPSE"]; }
        }

        /// <summary>
        /// Plantilla personalizada para Amex
        /// </summary>
        public static string PlantillaAmex
        {
            get { return (string)XmlADS["PlantillaAmex"]; }
        }

        /// <summary>
        /// Plantilla personalizada para Master
        /// </summary>
        public static string PlantillaMaster
        {
            get { return (string)XmlADS["PlantillaMaster"]; }
        }

        /// <summary>
        /// Extra 2 (direccion url de retorno)
        /// </summary>
        public static string Extra2
        {
            get { return (string)XmlADS["Extra2"]; }
        }

        /// <summary>
        /// Ciudad Envio
        /// </summary>
        public static string CiudadEnvio
        {
            get { return (string)XmlADS["CiudadEnvio"]; }
        }

        /// <summary>
        /// Direccion Envio
        /// </summary>
        public static string DireccionEnvio
        {
            get { return (string)XmlADS["DireccionEnvio"]; }
        }

        /// <summary>
        /// Url Envio pago
        /// </summary>
        public static string UrlPago
        {
            get { return (string)XmlADS["UrlPago"]; }
        }

        /// <summary>
        /// Variable de prueba
        /// </summary>
        public static string Prueba
        {
            get { return (string)XmlADS["Prueba"]; }
        }

        /// <summary>
        /// Obtiene si el codigo de la ciudad login es especificacdo. 
        /// </summary>
        public static VO_Credentials Credentials()
        {
            VO_Credentials vo_Credentials = new VO_Credentials();
            clsParametros cParametros = new clsParametros();
            bool bCredentials = false;

            vo_Credentials.UrlWebServices = clsConfiguracionPOL.UrlPago;
            vo_Credentials.Prueba = clsConfiguracionPOL.Prueba;

            try
            {               
                
                    vo_Credentials.User = clsConfiguracionPOL.Usuario;
                    vo_Credentials.PlantillaVisa = clsConfiguracionPOL.PlantillaVisa;
                    vo_Credentials.PlantillaDiners = clsConfiguracionPOL.PlantillaDiners;
                    vo_Credentials.PlantillaAmex = clsConfiguracionPOL.PlantillaAmex;
                    vo_Credentials.PlantillaMaster = clsConfiguracionPOL.PlantillaMaster;
                    vo_Credentials.PlantillaPse = clsConfiguracionPOL.PlantillaPSE;
                    vo_Credentials.Extra2 = clsConfiguracionPOL.Extra2;
                    vo_Credentials.CiudadEnvio = clsConfiguracionPOL.CiudadEnvio;
                    vo_Credentials.DireccionEnvio = clsConfiguracionPOL.DireccionEnvio;
                
            }
            catch
            {
                if (!bCredentials)
                {
                    try
                    {
                        vo_Credentials.User = clsConfiguracionPOL.Usuario;
                        vo_Credentials.PlantillaVisa = clsConfiguracionPOL.PlantillaVisa;
                        vo_Credentials.PlantillaDiners = clsConfiguracionPOL.PlantillaDiners;
                        vo_Credentials.PlantillaAmex = clsConfiguracionPOL.PlantillaAmex;
                        vo_Credentials.PlantillaMaster = clsConfiguracionPOL.PlantillaMaster;
                        vo_Credentials.PlantillaPse = clsConfiguracionPOL.PlantillaPSE;
                        vo_Credentials.Extra2 = clsConfiguracionPOL.Extra2;
                        vo_Credentials.CiudadEnvio = clsConfiguracionPOL.CiudadEnvio;
                        vo_Credentials.DireccionEnvio = clsConfiguracionPOL.DireccionEnvio;
                    }
                    catch
                    {
                    }
                }
            }
            return vo_Credentials;
        }
    }
    public class clsConfiguracionTotalTrip
    {
        /// <summary>
        /// Obtiene la ruta del servicio Xml de Hotelbeds
        /// </summary>
        public static string UrlWebService
        {
            get { return (string)XmlADS["UrlWebservices"]; }
        }
        /// <summary>
        /// Indica si Hotelbeds se encuentra en línea o fuera de línea
        /// </summary>
        /// <summary>
        /// Permite acceso a los recursos del web.config
        /// </summary>
        private static NameValueCollection XmlADS
        {
            get
            {
                NameValueCollection objXmlADS;

                objXmlADS = (NameValueCollection)ConfigurationManager.
                                            GetSection("system.web/XmlTotalTripADS");
                if (objXmlADS == null)
                {
                    objXmlADS = (NameValueCollection)ConfigurationManager.
                                          GetSection("XmlTotalTripADS");
                }

                return objXmlADS;
            }
        }
        /// <summary>
        /// Obtiene la clave del usuario del servidor proxy
        /// </summary>
        public static string Pcc
        {
            get { return (string)XmlADS["Pcc"]; }
        }
        /// <summary>
        /// Obtiene el usuario del servidor proxy
        /// </summary>
        public static string LoginUser
        {
            get { return (string)XmlADS["LoginUser"]; }
        }
        /// <summary>
        /// Obtiene la clave del usuario del servidor proxy
        /// </summary>
        public static string PasswordUser
        {
            get { return (string)XmlADS["PasswordUser"]; }
        }
        /// <summary>
        /// Obtiene si el codigo de la ciudad login es especificacdo. 
        /// </summary>
        public static VO_Credentials Credentials()
        {
            VO_Credentials vo_Credentials = new VO_Credentials();
            clsParametros cParametros = new clsParametros();
            //bool bCredentials = false;
            try
            {
                vo_Credentials.LoginUser = clsConfiguracionTotalTrip.LoginUser;
                vo_Credentials.PasswordUser = clsConfiguracionTotalTrip.PasswordUser;
                vo_Credentials.User = clsConfiguracionTotalTrip.Pcc;
                vo_Credentials.UrlWebServices = clsConfiguracionTotalTrip.UrlWebService;


            }
            catch
            {

            }
            return vo_Credentials;
        }
    }

    public class clsConfiguracionHB
    {
        /// <summary>
        /// Obtiene la ruta de los archivos xml
        /// </summary>
        public static string RutaArchivosXml
        {
            get
            {
                string strRutaXml = clsValidaciones.RutaXmlGen();
                return strRutaXml;
            }
        }

        /// <summary>
        /// Obtiene la ruta del servicio Xml de Hotelbeds
        /// </summary>
        public static string UrlWebService
        {
            get { return (string)XmlADS["UrlWebservices"]; }
        }

        /// <summary>
        /// Obtiene la ruta donde se encuentran los archivos de recursos
        /// </summary>
        public static string RutaArchivoRecursos
        {
            get
            {
                string strRutaRecursos = "";

                if (HttpContext.Current != null)
                    strRutaRecursos = (HttpContext.Current.Request.PhysicalApplicationPath + "bin");
                else
                    strRutaRecursos = System.Environment.CurrentDirectory + "\\bin";

                return strRutaRecursos;
            }
        }

        /// <summary>
        /// Indica si Hotelbeds se encuentra en línea o fuera de línea
        /// </summary>
        public static bool HotelbedsOnline
        {
            get { return (((string)XmlADS["Online"]).ToUpper() == "TRUE"); }
        }

        /// <summary>
        /// Indica si se debe o no hacer trace de las respuestas de Hotelbeds
        /// </summary>
        public static bool RealizarTrace
        {
            get { return (((string)XmlADS["TraceResponse"]).ToUpper() == "TRUE"); }
        }

        /// <summary>
        /// Permite acceso a los recursos del web.config
        /// </summary>
        private static NameValueCollection XmlADS
        {
            get
            {
                NameValueCollection objXmlHotelbedsADS;

                objXmlHotelbedsADS = (NameValueCollection)ConfigurationManager.
                                            GetSection("system.web/XmlHotelbedsADS");

                if (objXmlHotelbedsADS == null)
                {

                    objXmlHotelbedsADS = (NameValueCollection)ConfigurationManager.
                                                GetSection("XmlHotelbedsADS");

                }
                return objXmlHotelbedsADS;
            }
        }

        /// <summary>
        /// Indica si se debe utilizar proxy para acceder a Hotelbeds
        /// </summary>
        public static bool UsarProxy
        {
            get { return (((string)XmlADS["UsarProxy"]).ToUpper() == "TRUE"); }
        }

        /// <summary>
        /// Obtiene la dirección del servidor proxy
        /// </summary>
        public static string ProxyUrl
        {
            get { return (string)XmlADS["ProxyUrl"]; }
        }

        /// <summary>
        /// Obtiene la dirección del servidor proxy
        /// </summary>
        public static Enum_Encoding FormatoXml
        {
            get
            {
                string strFormatoXml = (string)XmlADS["FormatoXML"];
                Enum_Encoding eEncoding = clsHttpZipper.EncodingOut(strFormatoXml);
                return eEncoding;
            }
        }

        /// <summary>
        /// Obtiene el usuario del servidor proxy
        /// </summary>
        public static string User
        {
            get { return (string)XmlADS["User"]; }
        }

        /// <summary>
        /// Obtiene la clave del usuario del servidor proxy
        /// </summary>
        public static string Password
        {
            get { return (string)XmlADS["Password"]; }
        }

        /// <summary>
        /// Obtiene la cantidad de ofertas a mostrar
        /// </summary>
        public static int MaxOfertas
        {
            get { return Convert.ToInt32(XmlADS["MaxOfertas"]); }
        }

        /// <summary>
        /// Cantidad de días a partir del acceso al sistema para realizar
        /// la reserva de una oferta
        /// </summary>
        public static int DiasCheckInOferta
        {
            get { return Convert.ToInt32(XmlADS["DiasCheckInOferta"]); }
        }

        /// <summary>
        /// Cantidad de días a partir del checkin de la reserva de la oferta para realizar
        /// el checkout del hotel
        /// </summary>
        public static int DiasCheckOutOferta
        {
            get { return Convert.ToInt32(XmlADS["DiasCheckOutOferta"]); }
        }

        /// <summary>
        /// Cantidad de adultos que se tienen en cuenta para realizar la reserva
        /// </summary>
        public static int AdultosOferta
        {
            get { return Convert.ToInt32(XmlADS["AdultosOferta"]); }
        }
        public static VO_Credentials Credentials()
        {
            VO_Credentials vo_Credentials = new VO_Credentials();
            clsParametros cParametros = new clsParametros();
            try
            {
                //clsCache cCache = new csCache().cCache();
                //if (cCache != null)
                //{
                string sIdiomaGen = clsSesiones.getIdioma();
                vo_Credentials.Language = clsValidaciones.GetKeyOrAdd(sIdiomaGen, "CAS");

                vo_Credentials.User = clsConfiguracionHB.User;
                vo_Credentials.Password = clsConfiguracionHB.Password;
                vo_Credentials.UrlWebServices = clsConfiguracionHB.UrlWebService;
            }
            catch
            {
            }
            return vo_Credentials;
        }
    }
}
