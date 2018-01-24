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

namespace WS_SsoftHotelBeds.Hoteles
{
    public class clsPurchaseCancel
    {
        public clsResultados getServices(VO_PurchaseReference vo_PurchaseReference)
        {
            clsSerializer cDataXml = new clsSerializer();
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            string sesion = new clsCacheControl().CrearSession() + HttpContext.Current.Session.SessionID.ToString();
            //VO_PurchaseReference vo_PurchaseReference = clsSesiones.getPurchaseReference();
            try
            {
                string sXml = clsEstilosXmlHB.PurchaseCancelRQ + ".xml";
                string sRuta = clsConfiguracionHB.RutaArchivosXml;
                XmlDocument xmlDoc = cDataXml.RecuperarXML(sRuta, sXml);
                clsInterfaceWSHttp cInterface = new clsInterfaceWSHttp();

                clsCredencialesHB cCredenciales = new clsCredencialesHB();

                VO_Credentials vo_Credentials = new VO_Credentials();

                try { vo_Credentials.Language = ConfigurationManager.AppSettings[clsSesiones.getIdioma()].ToString(); }
                catch { vo_Credentials.Language = "CAS"; }


                vo_Credentials.User = clsConfiguracionHB.User;
                vo_Credentials.Password = clsConfiguracionHB.Password;
                vo_Credentials.SessionId = sesion;
                vo_Credentials.UrlWebServices = clsConfiguracionHB.UrlWebService;
                vo_PurchaseReference.Credentials = vo_Credentials; 
                xmlDoc = cCredenciales.Credenciales(xmlDoc, vo_PurchaseReference.Credentials, clsEstilosXmlHB.PurchaseCancelRQ, false);

                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "code", "IncomingOffice", vo_PurchaseReference.IncomingOffice);
                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "FileNumber", vo_PurchaseReference.FileNumber, 0);
                string sXmlRS = sRuta + clsSolicitudesXmlHB.PurchaseCancelRS + ".xml";
                string sResponse = cInterface.ObtenerHttpWebResponse(xmlDoc.InnerXml, vo_PurchaseReference.Credentials.UrlWebServices, clsConfiguracionHB.FormatoXml);

                cDataXml.SaveXML(sXmlRS, sResponse);

                cResultados.dsResultados = cDataXml.CrearDataSet(sResponse);
                if (cResultados.dsResultados.Tables.Count < 5)
                {
                    cParametros.Id = 0;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = "PurchaseCancelRQ";
                    cParametros.Complemento = "Cancelacion de Hoteles";
                    cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    cParametros.Code = "0";
                    cParametros.ValidaInfo = false;
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
                cParametros.Metodo = "PurchaseCancelRQ";
                cParametros.Complemento = "Cancelacion de Hoteles";
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
    }
}
