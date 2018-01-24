using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using Ssoft.ManejadorExcepciones;
using Ssoft.Sql;
using System.IO;
using System.Xml.Serialization;
using System.Web;
using System.Configuration;

namespace Ssoft.Utils
{
    [Obsolete]
    public class clsCacheReserva
    {
        // Cache Perfil
        private string _gstrNombre = string.Empty;
        private string _gstrSessionID = string.Empty;
        private string _gstrUser = string.Empty;
        private string _gstrNivel = string.Empty;
        private string _gstrPass = string.Empty;
        private string _gstrPagina = string.Empty;
        private string _gstrContacto = string.Empty;

        // Idioma y pais
        private string _gstrIdioma = string.Empty;
        //private string _gstrPais = string.Empty;
        private string _gstrCultura = string.Empty;

        // Se anexan estas variables para llevar el registro del cliente

      

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

        public clsCacheReserva()
        {
        }
    }
    public class clsCacheControlR
    {
        public clsCache CargarXML(string idSession)
        {
            clsCache objCache = new clsCache();

            if (HttpContext.Current.Session["Contacto"] != null)
                objCache.Contacto = HttpContext.Current.Session["Contacto"].ToString();
            if (HttpContext.Current.Session["Nombre"] != null)
                objCache.Nombre = HttpContext.Current.Session["Nombre"].ToString();
            if (HttpContext.Current.Session["User"] != null)
                objCache.User = HttpContext.Current.Session["User"].ToString();
            if (HttpContext.Current.Session["Nivel"] != null)
                objCache.Nivel = HttpContext.Current.Session["Nivel"].ToString();
            if (HttpContext.Current.Session["Pass"] != null)
                objCache.Pass = HttpContext.Current.Session["Pass"].ToString();
            if (HttpContext.Current.Session["Pagina"] != null)
                objCache.Pagina = HttpContext.Current.Session["Pagina"].ToString();
            if (HttpContext.Current.Session["Idioma"] != null)
                objCache.Idioma = HttpContext.Current.Session["Idioma"].ToString();
            if (HttpContext.Current.Session["Cultura"] != null)
                objCache.Cultura = HttpContext.Current.Session["Cultura"].ToString();
            objCache.SessionID = idSession;

            string pathXML = clsValidaciones.CacheTempCrea();

            XmlSerializer SerializerRQ = new XmlSerializer(typeof(clsCache));
            StreamWriter WriterRQ = new StreamWriter(pathXML + idSession + ".xml");
            SerializerRQ.Serialize(WriterRQ, objCache);
            WriterRQ.Close();

            return objCache;
        }
        public void RecuperarXML(string idSession)
        {
            string strPathXML = clsValidaciones.CacheTempCrea();

            TextReader txtReader = new StreamReader(strPathXML + idSession + ".xml");

            XmlSerializer SerializerRS = new XmlSerializer(typeof(clsCache));

            clsCache objCache = (clsCache)SerializerRS.Deserialize(txtReader);

            HttpContext.Current.Session["Nombre"] = objCache.Nombre.ToString();
            HttpContext.Current.Session["Contacto"] = objCache.Contacto.ToString();
            HttpContext.Current.Session["User"] = objCache.User.ToString();
            HttpContext.Current.Session["Nivel"] = objCache.Nivel.ToString();
            HttpContext.Current.Session["Pass"] = objCache.Pass.ToString();
            HttpContext.Current.Session["Pagina"] = objCache.Pagina.ToString();
            HttpContext.Current.Session["Idioma"] = objCache.Idioma.ToString();
            HttpContext.Current.Session["Cultura"] = objCache.Cultura.ToString();
            HttpContext.Current.Session["SessionIDLocal"] = objCache.SessionID.ToString();

        }
    }
}



