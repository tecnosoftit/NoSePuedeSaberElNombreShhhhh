using System;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
using Ssoft.ManejadorExcepciones;

namespace Ssoft.Utils
{
    public class clsInterfaceWSHttp
    {
        #region Métodos HttpWebResponse

        /// <summary>
        /// Obtiene una respuesta de un servicio web a partir de una solicitud xml
        /// </summary>
        /// <param name="strRequest">Xml de la solicitud</param>
        /// <param name="strUrl">Dirección del servicios xml</param>
        /// <returns>cadena con el resultado de la solicitud</returns>
        /// <remarks>
        /// Autor	: EADE
        /// Fecha	: 2006-06-16
        /// </remarks>
        public string ObtenerHttpWebResponse(string strRequest, string strUrl, Enum_Encoding eEnconding)
        {
            string strHeader = "Accept-Encoding:gzip, deflate";

            return this.ObtenerHttpWebResponse(strUrl,
                "text/xml", "POST", strRequest, "", "", "", eEnconding, strHeader);
        }

        public string ObtenerHttpWebResponse(string strUrl, Enum_Encoding eEnconding)
        {
            string strHeader = "Accept-Encoding:gzip, deflate";

            return this.ObtenerHttpWebResponse(strUrl,
                "text/xml", "POST", "", "", "", "", eEnconding, strHeader);
        }

        /// <summary>
        /// Obtiene una respuesta de un servicio web a partir de una solicitud xml
        /// </summary>
        /// <param name="strRequest">Xml de la solicitud</param>
        /// <param name="strUrl">Dirección del servicios xml</param>
        /// <returns>cadena con el resultado de la solicitud</returns>
        /// <remarks>
        /// Autor	: EADE
        /// Fecha	: 2006-06-16
        /// </remarks>
        public string ObtenerHttpWebResponse(string strRequest, string strUrl,
                            string strProxy, string strProxyUser, string strProxyPassword, Enum_Encoding eEnconding)
        {
            string strHeader = "Accept-Encoding:gzip, deflate";

            return this.ObtenerHttpWebResponse(strUrl, "text/xml", "POST",
                strRequest, strProxy, strProxyUser, strProxyPassword, eEnconding, strHeader);
        }

        /// <summary>
        /// Obtiene una respuesta de un servicio web a partir de una solicitud xml
        /// </summary>
        /// <param name="strUrl">Dirección del servicio xml</param>
        /// <param name="strContentType">Tipo de contenido de la solicitud</param>
        /// <param name="strMethod">Método de obtención del resultado Get o Post</param>
        /// <param name="strContent">Xml con el detalle de la solicitud</param>
        /// <param name="arrHeaders">Arreglo con las opciones condicionales del encabezado de la solicitud</param>
        /// <returns>cadena con el resultado de la solicitud</returns>
        /// <remarks>
        /// Autor	: José Faustino Posas
        /// Fecha	: 2009-11-03
        /// </remarks>
        public string ObtenerHttpWebResponse(
            string strUrl, string strContentType, string strMethod, string strContent,
            string strProxy, string strProxyUser, string strProxyPassword, Enum_Encoding eEnconding,
            params string[] arrHeaders)
        {

            HttpWebRequest hwrRequest = (HttpWebRequest)WebRequest.Create(strUrl);
            string strXml = "";

            foreach (string strHeader in arrHeaders)
                if (strHeader.Length > 0)
                    hwrRequest.Headers.Add(strHeader);

            if (strMethod.Length > 0)
                hwrRequest.Method = strMethod;

            if (strContentType.Length > 0)
                hwrRequest.ContentType = strContentType;

            if (strProxy != null && strProxy != "")
            {
                // 2006-08-08: Edwin. Control para cuando se ejecuta este módulo desde
                // una red que requiere autenticación con proxy
                WebProxy wpProxy;
                wpProxy = WebProxy.GetDefaultProxy();

                wpProxy.Address = new Uri(strProxy);

                // 2006-09-12 EADE
                if (strProxyUser != "")
                    wpProxy.Credentials = new NetworkCredential(strProxyUser, strProxyPassword);

                hwrRequest.Proxy = wpProxy;
                hwrRequest.KeepAlive = true;
                hwrRequest.ProtocolVersion = HttpVersion.Version10;
            }

            //Encoding isoEncoding = Encoding.GetEncoding("ISO-8859-1");
            Encoding isoEncoding = Encoding.GetEncoding(clsHttpZipper.EncodingIn(eEnconding));

            if (strContent.Length > 0)
            {
                byte[] postBytes = isoEncoding.GetBytes(strContent);

                hwrRequest.ContentLength = postBytes.Length;
                Stream postStream = hwrRequest.GetRequestStream();
                postStream.Write(postBytes, 0, postBytes.Length);
                postStream.Close();
            }

            HttpWebResponse hwrResponse = (HttpWebResponse)hwrRequest.GetResponse();

            // 2006-10-05 Edwin. Mejora de rendimiento
            Stream responseStream = clsHttpZipper.GetResponseStream(hwrResponse);
            StreamReader sr = new StreamReader(responseStream, isoEncoding);
            String respString = sr.ReadToEnd();
            if (respString.Contains("\r\n"))
                respString = respString.Replace("\r\n", "");

            XmlDocument Doc = new XmlDocument();
            Doc.LoadXml(respString);
            strXml = Doc.InnerXml;

            hwrResponse.Close();

            if (clsValidaciones.GetKeyOrAdd("HotelBedsReqRes").ToUpper() == "TRUE")
            {
                ExceptionHandled.Publicar("Sistema para Verificar Request & response");
                ExceptionHandled.Publicar(strContent);
                ExceptionHandled.Publicar("Enter Simulado");
                ExceptionHandled.Publicar(strXml);
            }

            return strXml;


        }

        #endregion

        #region WebClient

        /// <summary>
        /// Obtiene a través de web client en resultado de una solicitud xml
        /// </summary>
        /// <param name="strRequest">Contenido de la solicitud</param>
        /// <param name="strUrl">Dirección web del servicio</param>
        /// <returns>cadena con el resultado de la solicitud</returns>
        /// <remarks>
        /// Autor	: EADE
        /// Fecha	: 2006-06-16
        /// </remarks>
        public string ObtenerWebClientResponse(string strRequest, string strUrl, Enum_Encoding eEnconding)
        {
            return ObtenerWebClientResponse(strRequest, strUrl, "", "", "", eEnconding);
        }

        /// <summary>
        /// Obtiene a través de web client en resultado de una solicitud xml
        /// </summary>
        /// <param name="strRequest">Contenido de la solicitud</param>
        /// <param name="strUrl">Dirección web del servicio</param>
        /// <returns>cadena con el resultado de la solicitud</returns>
        /// <remarks>
        /// Autor	: EADE
        /// Fecha	: 2006-06-16
        /// </remarks>
        public string ObtenerWebClientResponse(string strRequest, string strUrl,
            string strProxy, string strProxyUser, string strProxyPassword, Enum_Encoding eEnconding)
        {
            XmlDocument xmlDoc = new XmlDocument();
            WebClient wcClient = new WebClient();

            Encoding isoEncoding = Encoding.GetEncoding(clsHttpZipper.EncodingIn(eEnconding));
            byte[] bytesReq = isoEncoding.GetBytes(strRequest);

            // 2006-09-21: Edwin. Control para cuando se ejecuta este módulo desde
            // una red que requiere autenticación con proxy			
            if (strProxy != null && strProxy != "")
            {
                wcClient.BaseAddress = strProxy;
                wcClient.Credentials = new NetworkCredential(strProxyUser, strProxyPassword);
            }

            wcClient.Headers.Add("Content-Type", "text/xml");
            byte[] respBytes = wcClient.UploadData(strUrl, "POST", bytesReq);

            MemoryStream msStream = new MemoryStream(respBytes, false);

            using (StreamReader srReader = new StreamReader(msStream, isoEncoding))
            {
                xmlDoc.Load(srReader);
            }

            return xmlDoc.InnerXml;
        }

        #endregion
    }
}
