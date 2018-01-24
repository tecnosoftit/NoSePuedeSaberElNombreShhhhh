using System;
using System.Collections.Generic;
using System.Text;
using WS_SsoftHotelBeds.FrontendService;
using Ssoft.ValueObjects;
using System.Data;
using WS_SsoftHotelBeds.Utilidades;
using System.Xml;
using System.Configuration;
using Ssoft.Utils;
using System.Web;
using Ssoft.ManejadorExcepciones;
using Ssoft.Rules.Pagina;
using Ssoft.Rules.Hoteles;

namespace WS_SsoftHotelBeds.Hoteles
{
    public class clsHotelDetailHB
    {
        // Tabla
        private const string TABLA_ERROR = "Error";

        // Columnas
        private const string COLUMN_CODE = "Code";
        private const string COLUMN_MESSAGE = "Message";
        private const string COLUMN_DETAIL_MESSAGE = "DetailedMessage";

        public clsResultados getServices(VO_HotelDetailRQ vo_HotelDetailRQ)
        {
            clsSerializer cDataXml = new clsSerializer();
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();

            try
            {
                string sXml = clsEstilosXmlHB.HotelDetailRQ + ".xml";
                string sRuta = clsConfiguracionHB.RutaArchivosXml;
                XmlDocument xmlDoc = cDataXml.RecuperarXML(sRuta, sXml);
                clsInterfaceWSHttp cInterface = new clsInterfaceWSHttp();

                VO_Credentials vo_Credentials = new VO_Credentials();

                try { vo_Credentials.Language = ConfigurationManager.AppSettings[clsSesiones.getIdioma()].ToString(); }
                catch { vo_Credentials.Language = "CAS"; }

                vo_Credentials.User = clsConfiguracionHB.User;
                vo_Credentials.Password = clsConfiguracionHB.Password;
                vo_Credentials.UrlWebServices = clsConfiguracionHB.UrlWebService;
                vo_HotelDetailRQ.Credentials = vo_Credentials;
                clsSesiones.setDetalleHotel(vo_HotelDetailRQ);

                clsCredencialesHB cCredenciales = new clsCredencialesHB();
                xmlDoc = cCredenciales.Credenciales(xmlDoc, vo_HotelDetailRQ.Credentials, clsEstilosXmlHB.HotelDetailRQ, false);

                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "HotelCode", vo_HotelDetailRQ.HotelCode, 0);

                string sXmlRS = sRuta + clsSolicitudesXmlHB.HotelDetailRS + ".xml";
                string sResponse = cInterface.ObtenerHttpWebResponse(xmlDoc.InnerXml, vo_HotelDetailRQ.Credentials.UrlWebServices, clsConfiguracionHB.FormatoXml);

                try
                {
                    cDataXml.SaveXML(sXmlRS, sResponse);
                }
                catch (Exception ex)
                {
                    Ssoft.ManejadorExcepciones.ExceptionHandled.Publicar(ex.Message);
                }

                cResultados.dsResultados = cDataXml.CrearDataSet(sResponse);

                clsSesiones.setResultados(cResultados);
                if (cResultados.dsResultados.Tables.Count < 5)
                {
                    cParametros.Id = 0;
                    cParametros.Code = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_CODE].ToString();
                    cParametros.Info = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_DETAIL_MESSAGE].ToString();
                    cParametros.Message = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_MESSAGE].ToString();
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = "HotelDetailRQ";
                    cParametros.Complemento = "Detalle de Hoteles";
                    cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    cParametros.ValidaInfo = false;
                    cParametros.Code = "0";
                    cParametros.MessageBD = true;
                    cParametros.TipoWs = Enum_ProveedorWebServices.HotelBeds;

                    cResultados.Error = cParametros;
                    ExceptionHandled.Publicar(cParametros);
                }
                else
                {
                    cParametros.Id = 1;
                    cResultados.Error = cParametros;
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = "HotelDetailRQ";
                cParametros.Complemento = "Detalle de Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "0";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.HotelBeds;

                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
        /// <summary>
        /// Metodo que retorna los hoteles de una ciudad (Codigo Iata)
        /// </summary>
        /// <param name="sCodeCity">Codigo Iata de la ciudad</param>
        /// <returns>dtData, DatatTable de resultados</returns>
        /// <remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-12-22
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:          
        /// Descripción:    
        /// </remarks>
        public DataTable getHotels(string sCodeCity)
        {
            DataSet dsData = new DataSet();
            DataTable dtData = new DataTable();
            clsParametros cParametros = new clsParametros();
            try
            {
                csHoteles cHoteles = new csHoteles();
                dsData = cHoteles.getHotels(sCodeCity);
                try
                {
                    if (dsData != null)
                    {
                        dtData = dsData.Tables[0];
                    }
                }
                catch { }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.Complemento = "Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Code = "0";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.TotalTrip;
                ExceptionHandled.Publicar(cParametros);
            }
            return dtData;
        }
    }
}
