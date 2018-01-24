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
using Ssoft.Ssoft.ValueObjects.Hoteles;

namespace WS_SsoftHotelBeds.Hoteles
{
    public class clsPurchaseConfirmHB
    {
        // Tabla
        private const string TABLA_HOTEL_ROOM = "HotelRoom";
        private const string TABLA_ERROR = "Error";

        // Columnas
        private const string COLUMN_SPUI = "SPUI";
        private const string COLUMN_AGENCY_CODE = "Agency_Code";
        private const string COLUMN_PURCHASE_TOKEN = "purchaseToken";
        private const string COLUMN_BRANCH = "Branch";

        private const string COLUMN_CODE = "Code";
        private const string COLUMN_MESSAGE = "Message";
        private const string COLUMN_DETAIL_MESSAGE = "DetailedMessage";

        public clsResultados getServices()
        {
            clsSerializer cDataXml = new clsSerializer();
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            VO_PurchaseConfirmRQ vo_PurchaseConfirmRQ = getParametros();

            try
            {

                DataTable dtAdult = (DataTable)HttpContext.Current.Session["TableID"];
                DataTable dtCh = (DataTable)HttpContext.Current.Session["TableIDCH"];



                string sAdulto = clsValidaciones.GetKeyOrAdd("AdultoHB", "AD");
                int passajer = 0;
                string sInfante = clsValidaciones.GetKeyOrAdd("InfanteHB", "CH");

                string sXml = clsEstilosXmlHB.PurchaseConfirmRQ + ".xml";
                //string sXml = clsSolicitudesXml.PurchaseConfirmRS + ".xml";
                string sRuta = clsConfiguracionHB.RutaArchivosXml;
                XmlDocument xmlDoc = cDataXml.RecuperarXML(sRuta, sXml);
                clsInterfaceWSHttp cInterface = new clsInterfaceWSHttp();
                XmlNode objNodo2 = null;
                clsCredencialesHB cCredenciales = new clsCredencialesHB();
                xmlDoc = cCredenciales.Credenciales(xmlDoc, vo_PurchaseConfirmRQ.Credentials, clsEstilosXmlHB.PurchaseConfirmRQ, false);

                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "purchaseToken", "ConfirmationData", vo_PurchaseConfirmRQ.PurchaseToken);
                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "type", "Holder", vo_PurchaseConfirmRQ.Holder.Type);

                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Name", vo_PurchaseConfirmRQ.Holder.Name, 0);
                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "LastName", vo_PurchaseConfirmRQ.Holder.LastName, 0);
                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "AgencyReference", vo_PurchaseConfirmRQ.AgencyReference, 0);

                int intHabitaciones = vo_PurchaseConfirmRQ.lServiceData.Count;
                int j = 0;
                int k = 1;
                int childs = 0;
                int Adultos = 0;
                int Adults = 0;
                bool infante = true;
                XmlNode objNodoGen = cDataXml.AsignarNodo(xmlDoc, "ServiceData", 0);
                XmlNode objNodeAgeGen = cDataXml.AsignarNodo(xmlDoc, "Customer", 0);
               
                int iInfant = 0;

                for (int lcostumer = 0; lcostumer < intHabitaciones; lcostumer++)
                {
                    passajer = passajer + vo_PurchaseConfirmRQ.lServiceData[lcostumer].lCustomer.Count;
                
                }


                for (int intIndex = 0; intIndex < intHabitaciones; intIndex++)
                {
                    XmlNode objNodo = null;

                    // La primera vez actualiza el elemento, las otras veces lo copia para adicionarlo
                    if (intIndex == 0)
                    {
                        objNodo = objNodoGen;
                        objNodo2 = objNodo;
                    }
                    else
                        objNodo = objNodo2.Clone();
                 
                    if (intIndex < 1)
                    {
                        xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "SPUI", "ServiceData", vo_PurchaseConfirmRQ.lServiceData[intIndex].SPUI, intIndex);
                    }

                   
                    if (vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer.Count > 0)
                    {
                       

                        for (int x = 0; x < vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer.Count; x++)
                        {      
                          
                            if (vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer[x].Type == sAdulto)
                            {
                                XmlNode objNodoAge = null;

                                // La primera vez actualiza el elemento, las otras veces lo copia para adicionarlo


                                if (Adults < 1)
                                {
                                    for (int r = 0; r < passajer - 1; r++)
                                    {
                                        objNodo = objNodeAgeGen.Clone();
                                        cDataXml.AsignarNodo(xmlDoc, "CustomerList", intIndex).AppendChild(objNodo);
                                    }
                                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Name", vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer[x].Name.ToString(), 0);
                                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "LastName", vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer[x].LastName.ToString(), 0);
                                    Adults = 1;
                                }

                                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "type", "Customer", vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer[x].Type, j);
                                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Age", vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer[x].Age.ToString(), j);
                                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "CustomerId", dtAdult.Rows[Adultos]["customerId"].ToString(), j);                  
                                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Name", vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer[x].Name.ToString(),k);                                
                                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "LastName", vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer[x].LastName.ToString(), k);
                                j++;
                                k++;
                                Adultos++;
                            }
                        }
                        for (int x = 0; x < vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer.Count; x++)
                        {
                            if (vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer[x].Type == sInfante)
                            {
                                XmlNode objNodoAge = null;

                                    xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "type", "Customer",vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer[x].Type, j);
                                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Age", vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer[x].Age.ToString(), j);
                                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "CustomerId", dtCh.Rows[childs]["customerId"].ToString(), j);
                                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Name", vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer[x].LastName.ToString(),k);                                             
                                   xmlDoc = cDataXml.AsignarParametro(xmlDoc, "LastName", vo_PurchaseConfirmRQ.lServiceData[intIndex].lCustomer[x].LastName.ToString(),k);

                                childs++;
                                j++;
                                k++;
                            }
                        }
                    }
                }

                string sXmlRS = sRuta + clsSolicitudesXmlHB.PurchaseConfirmRS + ".xml";
                xmlDoc.InnerXml = xmlDoc.InnerXml.ToString().Replace("<LastName></LastName>", "");
                xmlDoc.InnerXml = xmlDoc.InnerXml.ToString().Replace("<Name></Name>", "");

                HttpContext.Current.Session["TableID"] = null;
                HttpContext.Current.Session["TableIDCH"] = null;
                string sResponse = cInterface.ObtenerHttpWebResponse(xmlDoc.InnerXml, vo_PurchaseConfirmRQ.Credentials.UrlWebServices, clsConfiguracionHB.FormatoXml);
                try
                {
                    cDataXml.SaveXML(sXmlRS, sResponse);
                }
                catch { }
                //string sResponse = xmlDoc.InnerXml;
                cResultados.dsResultados = cDataXml.CrearDataSet(sResponse);

                clsSesiones.setConfirmaHotel(cResultados.dsResultados);
                if (cResultados.dsResultados.Tables.Count < 5)
                {
                    cParametros.Id = 0;
                    cParametros.Code = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_CODE].ToString();
                    cParametros.Info = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_DETAIL_MESSAGE].ToString();
                    cParametros.Message = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_MESSAGE].ToString();
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = "PurchaseConfirmRQ";
                    cParametros.Complemento = "Confirmacion de Hoteles";
                    cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    cParametros.Code = "502";
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
                cParametros.Metodo = "PurchaseConfirmRQ";
                cParametros.Complemento = "Confirmacion de Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "502";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.HotelBeds;

                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
        /// <summary>
        /// Retornamos la cantidad maxima de pasajeros
        /// </summary>
        /// <param name="vo_PurchaseConfirmRQ"></param>
        /// <returns></returns>
        private int getTotalPax(VO_PurchaseConfirmRQ vo_PurchaseConfirmRQ)
        {
            int total = 0;
                 
            foreach( VO_ServiceData lServiceData  in vo_PurchaseConfirmRQ.lServiceData)
            {
                if(total<lServiceData.lCustomer.Count)
                {
                    total=lServiceData.lCustomer.Count;
                }
            }
            
            return total;

        }
        private VO_PurchaseConfirmRQ getParametros()
        {
            VO_PurchaseConfirmRQ vo_PurchaseConfirmRQ = new VO_PurchaseConfirmRQ();
            VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();
            VO_Holder vo_Holder = new VO_Holder();
            List<VO_ServiceData> lvo_ServiceData = new List<VO_ServiceData>();
            //List<VO_Customer> lvo_Customer = new List<VO_Customer>();

            string sAdulto = string.Empty;
            try { sAdulto = ConfigurationManager.AppSettings["AdultoHB"].ToString(); }
            catch { sAdulto = "AD"; }

            DataSet dsRoomType = clsSesiones.getReservaHotel();

            DataTable dtHotelRoom = dsRoomType.Tables[TABLA_HOTEL_ROOM];
            DataRow[] drHotelRoom = dtHotelRoom.Select();

            vo_PurchaseConfirmRQ.Credentials = vo_HotelValuedAvailRQ.Credentials;

            int iPos = vo_HotelValuedAvailRQ.lHotelOccupancy.Count;

            for (int i = 0; i < iPos; i++)
            {
                VO_ServiceData vo_ServiceData = new VO_ServiceData();
                vo_ServiceData.lCustomer = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList;
                vo_ServiceData.SPUI = drHotelRoom[0][COLUMN_SPUI].ToString();
                lvo_ServiceData.Add(vo_ServiceData);
            }

            vo_PurchaseConfirmRQ.lServiceData = lvo_ServiceData;

            int iContador = lvo_ServiceData[0].lCustomer.Count;
            for (int j = 0; j < iContador; j++)
            {
                if (lvo_ServiceData[0].lCustomer[j].Type.Equals(sAdulto))
                {
                    vo_Holder.Name = lvo_ServiceData[0].lCustomer[j].Name;
                    vo_Holder.LastName = lvo_ServiceData[0].lCustomer[j].LastName;
                    vo_Holder.Type = lvo_ServiceData[0].lCustomer[j].Type;
                    break;
                }
            }

            vo_PurchaseConfirmRQ.Holder = vo_Holder;

            // Traemos la tabla
            /*Recuperamos la cache para traer el id de la empresa y enviarlo en el AgencyReference*/
            clsCache cCache = new csCache().cCache();

            vo_PurchaseConfirmRQ.AgencyReference = cCache.Empresa;//drHotelRoom[0][COLUMN_BRANCH].ToString(); 
            vo_PurchaseConfirmRQ.PurchaseToken = drHotelRoom[0][COLUMN_PURCHASE_TOKEN].ToString();

            return vo_PurchaseConfirmRQ;
        }
    }
    //public class clsPurchaseCancelRQ
    //{
    //    public clsResultados getServices(VO_PurchaseReference vo_PurchaseReference)
    //    {
    //        clsSerializer cDataXml = new clsSerializer();
    //        clsResultados cResultados = new clsResultados();
    //        clsParametros cParametros = new clsParametros();

    //        try
    //        {
    //            string sXml = clsEstilosXmlHB.PurchaseCancelRQ + ".xml";
    //            string sRuta = clsConfiguracionHB.RutaArchivosXml;
    //            XmlDocument xmlDoc = cDataXml.RecuperarXML(sRuta, sXml);
    //            clsInterfaceWSHttp cInterface = new clsInterfaceWSHttp();

    //            clsCredencialesHB cCredenciales = new clsCredencialesHB();
    //            xmlDoc = cCredenciales.Credenciales(xmlDoc, vo_PurchaseReference.Credentials, clsEstilosXmlHB.PurchaseCancelRQ, false);

    //            xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "code", "IncomingOffice", vo_PurchaseReference.IncomingOffice);
    //            xmlDoc = cDataXml.AsignarParametro(xmlDoc, "FileNumber", vo_PurchaseReference.FileNumber, 0);
    //            string sXmlRS = sRuta + clsSolicitudesXmlHB.PurchaseCancelRS + ".xml";
    //            string sResponse = cInterface.ObtenerHttpWebResponse(xmlDoc.InnerXml, vo_PurchaseReference.Credentials.UrlWebServices, clsConfiguracionHB.FormatoXml);

    //            cDataXml.SaveXML(sXmlRS, sResponse);

    //            cResultados.dsResultados = cDataXml.CrearDataSet(sResponse);

    //            clsSesiones.setResultadoHotel(cResultados.dsResultados);
    //            if (cResultados.dsResultados.Tables.Count < 5)
    //            {
    //                cParametros.Id = 0;
    //                //cParametros.Code = ota_HotelAvailRS.Errors.Error.ErrorCode;
    //                //cParametros.Info = ota_HotelAvailRS.Errors.Error.ErrorInfo.Message;
    //                //cParametros.Message = ota_HotelAvailRS.Errors.Error.ErrorMessage;
    //                //cParametros.Severity = ota_HotelAvailRS.Errors.Error.Severity;
    //                cParametros.Tipo = clsTipoError.WebServices;
    //                cParametros.Metodo = "PurchaseCancelRQ";
    //                cParametros.Complemento = "Cancelacion de Hoteles";
    //                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
    //                cParametros.Sugerencia.Add("Por favor intente de nuevo");
    //                //cParametros.Message = ota_HotelAvailRS.Errors.Error.ErrorMessage;

    //                cResultados.Error = cParametros;
    //                ExceptionHandled.Publicar(cParametros);
    //            }
    //            else
    //            {
    //                cParametros.Id = 1;
    //                cResultados.Error = cParametros;
    //            }
    //        }
    //        catch (Exception Ex)
    //        {
    //            cParametros.Id = 0;
    //            cParametros.Message = Ex.Message;
    //            cParametros.Severity = clsSeveridad.Alta;
    //            cParametros.Tipo = clsTipoError.WebServices;
    //            cParametros.Metodo = "HotelDetailRQ";
    //            cParametros.Complemento = "Detalle de Hoteles";
    //            cParametros.Source = Ex.Source;
    //            cParametros.StackTrace = Ex.StackTrace;
    //            cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
    //            cParametros.Sugerencia.Add("Por favor intente de nuevo");

    //            cResultados.Error = cParametros;
    //            ExceptionHandled.Publicar(cParametros);
    //        }
    //        return cResultados;
    //    }
    //}
}
