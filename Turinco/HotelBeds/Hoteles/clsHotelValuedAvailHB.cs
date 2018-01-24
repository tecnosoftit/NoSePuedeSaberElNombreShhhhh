using System;
using System.Collections.Generic;
using System.Text;
using WS_SsoftHotelBeds.FrontendService;
using Ssoft.ValueObjects;
using System.Data;
using WS_SsoftHotelBeds.Utilidades;
using System.Xml;
using System.Xml.XPath;
using System.Configuration;
using Ssoft.Utils;
using System.Web;
using Ssoft.ManejadorExcepciones;
using Ssoft.DataNet;
using Ssoft.Rules.Generales;
using Ssoft.Sql;
using Ssoft.Rules.Reservas;
using Ssoft.Rules.Administrador;
using Ssoft.Ssoft.ValueObjects.Hoteles;


namespace WS_SsoftHotelBeds.Hoteles
{
    public class clsHotelValuedAvailHB
    {
        // Tabla
        private const string TABLA_ERROR = "Error";
        private const string TABLA_PAGINATION_DATA = "PaginationData";
        private const string TABLA_HOTEL_INFO = "HotelInfo";
        private const string TABLA_HOTEL_VALUE_AVAIL = "HotelValuedAvailRS";
        private const string TABLA_HOTEL_OCCUPANCY = "HotelOccupancy";
        private const string TABLA_HOTEL_ROOM = "HotelRoom";

        //// Nuevo
        //private const string TABLA_ROOM = "HotelRoom";
        //private const string TABLA_IMAGES = "HotelImages";
        //private const string TABLA_DINING = "HotelDining";
        //private const string TABLA_LOCATION = "HotelLocations";
        //private const string TABLA_PAYMENTSFEATURES = "PaymentsFeatures";
        //private const string TABLA_ROOMFEATURES = "RoomsFeatures";
        //private const string TABLA_AMENITYFEATURES = "AmenityFeatures";
        //private const string TABLA_GENERALITIES = "HotelGenaralities";
        //private const string TABLA_PARAMETERS = "ParametersHotelDetail";
        //private const string TABLA_HOTELDISTANCE = "HotelDistances";

        //public const string TABLA_MASTER = "tblReserva";
        //public const string TABLA_SEGMENTOS = "tblTransac";
        //public const string TABLA_PAX = "tblPax";
        //public const string TABLA_TARIFA = "tblTarifa";
        //public const string TABLA_TASAS = "tblTax";
        //public const string TABLA_HABITACIONES = "tblHabitaciones";

        //// Tablas WebServices
        //private const string TABLA_HOTEL_VALUE_AVAIL = "HotelValuedAvailRS";
        //private const string TABLA_AUDIT_DATA = "AuditData";
        //private const string TABLA_PAGINATION_DATA = "PaginationData";
        //private const string TABLA_SERVICE_HOTEL = "ServiceHotel";
        //private const string TABLA_CONTRACT_LIST = "ContractList";
        //private const string TABLA_CONTRACT = "Contract";
        //private const string TABLA_INCOMING_OFFICE = "IncomingOffice";
        //private const string TABLA_CLASSIFICATION = "Classification";
        //private const string TABLA_DATE_FROM = "DateFrom";
        //private const string TABLA_DATE_TO = "DateTo";
        //private const string TABLA_CURRENCY = "Currency";
        //private const string TABLA_HOTEL_INFO = "HotelInfo";
        //private const string TABLA_IMAGE_LIST = "ImageList";
        //private const string TABLA_IMAGE = "Image";
        //private const string TABLA_CATEGORY = "Category";
        //private const string TABLA_DESTINATION = "Destination";
        //private const string TABLA_ZONE_LIST = "ZoneList";
        //private const string TABLA_ZONE = "Zone";
        //private const string TABLA_CHILD_AGE = "ChildAge";
        //private const string TABLA_POSITION = "Position";
        //private const string TABLA_AVAILABLE_ROOM = "AvailableRoom";
        //private const string TABLA_BOARD = "Board";
        //private const string TABLA_ROOM_TYPE = "RoomType";
        //private const string TABLA_PRICE = "Price";

        //// Tablas Adicionales de ServivesAdd
        //private const string TABLA_PURCHASE = "Purchase";
        //private const string TABLA_AGENCY = "Agency";
        //private const string TABLA_SERVICE_LIST = "ServiceList";
        //private const string TABLA_SERVICE = "Service";
        //private const string TABLA_SUPLIER = "Supplier";
        //private const string TABLA_ADITIONAL_COST_LIST = "AdditionalCostList";
        //private const string TABLA_ADITIONAL_COST = "AdditionalCost";
        //private const string TABLA_MODIFICATION_POLICY_LIST = "ModificationPolicyList";
        //private const string TABLA_MODIFICATION_POLICY = "ModificationPolicy";
        //private const string TABLA_GUEST_LIST = "GuestList";
        //private const string TABLA_CUSTOMER = "Customer";
        //private const string TABLA_BIRTH_DATE = "BirthDate";
        //private const string TABLA_CANCELATION_POLICY = "CancellationPolicy";
        //private const string TABLA_DATE_TIME_FROM = "DateTimeFrom";
        //private const string TABLA_DATE_TIME_TO = "DateTimeTo";

        //// Tablas Adicionales de Confirmacion
        //private const string TABLA_PURCHASE_CONFIRMRS = "PurchaseConfirmRS";
        //private const string TABLA_REFERENCE = "Reference";
        //private const string TABLA_CREATION_DATE = "CreationDate";
        //private const string TABLA_HOLDER = "Holder";
        //private const string TABLA_COMMENT_LIST = "CommentList";
        //private const string TABLA_COMMENT = "Comment";
        //private const string TABLA_HOTEL_ROOM_EXTRA_INFO = "HotelRoomExtraInfo";
        //private const string TABLA_EXTENDED_DATE = "ExtendedData";
        //private const string TABLA_PAYMENT_DATA = "PaymentData";
        //private const string TABLA_PAYMENT_TYPE = "PaymentType";
        
        
        
        // Columnas
        private const string COLUMN_CODE = "Code";
        private const string COLUMN_MESSAGE = "Message";
        private const string COLUMN_DETAIL_MESSAGE = "DetailedMessage";

        // TABLA_PAGINATION_DATA
        private const string COLUMN_CURRENT_PAGE = "currentPage";
        private const string COLUMN_TOTAL_PAGES = "totalPages";
        private const string COLUMN_RESULT_PAGE = "resultPage";
        private const string COLUMN_TOTAL_ITEMS = "totalItems";

        public clsResultados getServices(VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ)
        {
            clsSerializer cDataXml = new clsSerializer();
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();

            try
            {
                string sesion = new clsCacheControl().CrearSession() + HttpContext.Current.Session.SessionID.ToString();
                try
                {
                    if (sesion.Length > 25)
                    {
                        sesion = sesion.Substring(0, 25);
                    }
                }
                catch { }
                string sAdulto = clsValidaciones.GetKeyOrAdd("AdultoHB", "AD");

                string sInfante = clsValidaciones.GetKeyOrAdd("InfanteHB", "CH");

                string sRuta = clsConfiguracionHB.RutaArchivosXml;
                string sXml = clsEstilosXmlHB.HotelValuedAvailRQ + ".xml";
                //string sXmlCopia = sRuta + clsEstilosXmlHB.HotelValuedAvailRQ + "_" + sesion + ".xml";
                if (vo_HotelValuedAvailRQ.Zone != null)
                {
                    sXml = clsEstilosXmlHB.HotelValuedAvailZoneRQ + ".xml";
                    //sXmlCopia = sRuta + clsEstilosXmlHB.HotelValuedAvailZoneRQ + "_" + sesion + ".xml";
                }

                XmlDocument xmlDoc = cDataXml.RecuperarXML(sRuta, sXml);
                clsInterfaceWSHttp cInterface = new clsInterfaceWSHttp();

                VO_Credentials vo_Credentials = new VO_Credentials();

                try { vo_Credentials.Language = ConfigurationManager.AppSettings[clsSesiones.getIdioma()].ToString(); }
                catch { vo_Credentials.Language = "CAS"; }

                vo_Credentials.User = clsConfiguracionHB.User;
                vo_Credentials.Password = clsConfiguracionHB.Password;
                vo_Credentials.SessionId = sesion;
                vo_Credentials.UrlWebServices = clsConfiguracionHB.UrlWebService;
                vo_HotelValuedAvailRQ.Credentials = vo_Credentials;
                clsSesiones.setParametrosHotel(vo_HotelValuedAvailRQ);
                //cDataXml.ClaseXML(vo_HotelValuedAvailRQ, clsEstilosXml.HotelValuedAvailRQ);
                clsCredencialesHB cCredenciales = new clsCredencialesHB();
                xmlDoc = cCredenciales.Credenciales(xmlDoc, vo_HotelValuedAvailRQ.Credentials, clsEstilosXmlHB.HotelValuedAvailRQ, true);

                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "pageNumber", "PaginationData", vo_HotelValuedAvailRQ.PaginationData);
                if (HttpContext.Current.Session["$Busqueda"] != null)
                {
                    xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "itemsPerPage", "PaginationData", "2000");
                }
                if (HttpContext.Current.Session["$Estrellas"] != null)
                {
                    string cadena = "<CategoryList><HotelCategory type=" + '"' + "SIMPLE" + '"' + " code=" + '"' + HttpContext.Current.Session["$Estrellas"].ToString() + "EST" + '"' + "/><HotelCategory type=" + '"' + "SIMPLE" + '"' + " code=" + '"' + HttpContext.Current.Session["$Estrellas"].ToString() + "LL" + '"' + "/><HotelCategory type=" + '"' + "SIMPLE" + '"' + " code=" + '"' + "H" + HttpContext.Current.Session["$Estrellas"].ToString() + "_5" + '"' + "/></CategoryList>";
                    xmlDoc.InnerXml = xmlDoc.InnerXml.Replace("$", cadena);
                }
                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "date", "CheckInDate", vo_HotelValuedAvailRQ.CheckInDate);
                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "date", "CheckOutDate", vo_HotelValuedAvailRQ.CheckOutDate);
                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "code", "Destination", vo_HotelValuedAvailRQ.Destination);
                if (vo_HotelValuedAvailRQ.Zone != null)
                {
                    xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "code", "Zone", vo_HotelValuedAvailRQ.Zone);
                    if (vo_HotelValuedAvailRQ.Type.Equals(Enum_TypeZone.SIMPLE))
                    {
                        xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "type", "Zone", "SIMPLE");
                    }
                    else
                    {
                        xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "type", "Zone", "GROUP");
                    }
                }

                int intHabitaciones = vo_HotelValuedAvailRQ.lHotelOccupancy.Count;

                XmlNode objNodoGen = cDataXml.AsignarNodo(xmlDoc, "HotelOccupancy", 0);
                XmlNode objNodeAgeGen = cDataXml.AsignarNodo(xmlDoc, "GuestList", 0);
                int iInfant = 0;
                for (int intIndex = 0; intIndex < intHabitaciones; intIndex++)
                {
                    XmlNode objNodo = null;

                    // La primera vez actualiza el elemento, las otras veces lo copia para adicionarlo
                    if (intIndex == 0)
                        objNodo = objNodoGen;
                    else
                        objNodo = objNodoGen.Clone();

                    if (intIndex > 0)
                        cDataXml.AsignarNodo(xmlDoc, "OccupancyList", 0).AppendChild(objNodo);

                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "RoomCount", vo_HotelValuedAvailRQ.lHotelOccupancy[intIndex].RoomCount.ToString(), intIndex);
                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "AdultCount", vo_HotelValuedAvailRQ.lHotelOccupancy[intIndex].Occupancy.AdultCount.ToString(), intIndex);
                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "ChildCount", vo_HotelValuedAvailRQ.lHotelOccupancy[intIndex].Occupancy.ChildCount.ToString(), intIndex);

                    XmlNode objNodoOcupancy = cDataXml.AsignarNodo(xmlDoc, "Occupancy", intIndex);

                    if (vo_HotelValuedAvailRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList.Count == 0)
                    {
                        objNodoOcupancy.RemoveChild(objNodeAgeGen);
                    }
                    else
                    {
                        for (int x = 0; x < vo_HotelValuedAvailRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList.Count; x++)
                        {
                            if (vo_HotelValuedAvailRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList[x].Type == sInfante)
                            {
                                XmlNode objNodoAge = null;

                                // La primera vez actualiza el elemento, las otras veces lo copia para adicionarlo
                                if (x == 0)
                                    objNodoAge = objNodeAgeGen;
                                else
                                    objNodoAge = objNodeAgeGen.Clone();
                                if (x > 0)
                                {
                                    cDataXml.AsignarNodo(xmlDoc, "GuestList", intIndex).AppendChild(objNodoAge);
                                    //cDataXml.AsignarNodo(xmlDoc, "Customer", intIndex).AppendChild(objNodoAge);
                                }
                                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "type", "Customer", vo_HotelValuedAvailRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList[x].Type, iInfant);
                                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Age", vo_HotelValuedAvailRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList[x].Age.ToString(), iInfant);
                                iInfant++;
                            }
                            else
                            {
                                XmlNode objNodoAge = null;

                                // La primera vez actualiza el elemento, las otras veces lo copia para adicionarlo
                                if (x == 0)
                                {
                                    objNodoAge = objNodeAgeGen;
                                    xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "type", "Customer", vo_HotelValuedAvailRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList[x].Type, iInfant);
                                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Age", vo_HotelValuedAvailRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList[x].Age.ToString(), iInfant);
                                    iInfant++;
                                }
                            }
                        }
                    }
                }
                string sXmlRS = sRuta + clsSolicitudesXmlHB.HotelValuedAvailRS + "_pag_" + vo_HotelValuedAvailRQ.PaginationData.ToString() + ".xml";
                string sResponse = cInterface.ObtenerHttpWebResponse(xmlDoc.InnerXml, vo_HotelValuedAvailRQ.Credentials.UrlWebServices, clsConfiguracionHB.FormatoXml);

                //try
                //{
                //    //cDataXml.SaveXML(sXmlCopia, xmlDoc.InnerXml);
                //    cDataXml.SaveXML(sXmlRS, sResponse);
                //}
                //catch { }
                cResultados.dsResultados = cDataXml.CrearDataSet(sResponse);
                //try
                //{
                    //cDataXml.SaveXML(sXmlCopia, xmlDoc.InnerXml);
                    //cDataXml.DatasetXML(cResultados.dsResultados, "HotelBeds");
                //}
                //catch { }
                
                clsSesiones.setResultadoHotel(cResultados.dsResultados);
                if (cResultados.dsResultados.Tables.Count < 5)
                {
                    cParametros.Id = 0;
                    cParametros.Code = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_CODE].ToString();
                    cParametros.Info = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_DETAIL_MESSAGE].ToString();
                    cParametros.Message = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_MESSAGE].ToString();
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = "HotelValuedAvailRQ";
                    cParametros.Complemento = "Resultados de Hoteles";
                    cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    cParametros.Code = "501";
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
                cParametros.Metodo = "HotelValuedAvailRQ";
                cParametros.Complemento = "Resultados de Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "501";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.HotelBeds;

                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
        public clsResultados getServicesMulti(VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ)
        {
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            clsSerializer cDataXml = new clsSerializer();
            cParametros.Id = 1;
            cResultados.Error = cParametros;

            //cResultados.dsResultados = cDataXml.XMLDataset("HotelBeds");
            cResultados = getServices(vo_HotelValuedAvailRQ);
            try
            {
                if (!cResultados.Error.Id.Equals(0))
                {
                    DataTable dtHotelInfo = cResultados.dsResultados.Tables[TABLA_HOTEL_INFO];
                    DataTable dtPageData = cResultados.dsResultados.Tables[TABLA_PAGINATION_DATA];
                    DataTable dtHotelValue = cResultados.dsResultados.Tables[TABLA_HOTEL_VALUE_AVAIL];

                    int iPaginas = dtHotelInfo.Rows.Count;

                    foreach (DataRow drPageData in dtPageData.Rows)
                    {
                        drPageData[COLUMN_TOTAL_PAGES] = 1;
                    }
                    foreach (DataRow drHotelValue in dtHotelValue.Rows)
                    {
                        drHotelValue[COLUMN_TOTAL_ITEMS] = iPaginas;
                    }
                    dtPageData.AcceptChanges();
                    dtHotelValue.AcceptChanges();

                    clsSesiones.setResultadoHotel(cResultados.dsResultados);
                    if (cResultados.dsResultados.Tables.Count < 5)
                    {
                        cParametros.Id = 0;
                        try
                        {
                            cParametros.Code = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_CODE].ToString();
                            cParametros.Info = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_DETAIL_MESSAGE].ToString();
                            cParametros.Message = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_MESSAGE].ToString();
                        }
                        catch { }
                        cParametros.Severity = clsSeveridad.Alta;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Metodo = "HotelValuedAvailRQ";
                        cParametros.Complemento = "Resultados de Hoteles";
                        cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                        cParametros.Sugerencia.Add("Por favor intente de nuevo");
                        cParametros.Code = "501";
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
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = "HotelValuedAvailRQ";
                cParametros.Complemento = "Resultados de Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "501";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.HotelBeds;

                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
        //public clsResultados getServicesMulti(VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ)
        //{
        //    clsResultados cResultados = new clsResultados();
        //    clsParametros cParametros = new clsParametros();
        //    clsSerializer cDataXml = new clsSerializer();
        //    cParametros.Id = 1;
        //    cResultados.Error = cParametros;

        //    cResultados = getServices(vo_HotelValuedAvailRQ);
        //    try
        //    {
        //        if (!cResultados.Error.Id.Equals(0))
        //        {
        //            DataSet dsResultados = cResultados.dsResultados;

        //            int iPagTotal = int.Parse(cResultados.dsResultados.Tables[TABLA_PAGINATION_DATA].Rows[0][COLUMN_TOTAL_PAGES].ToString());
        //            for (int x = 2; x <= iPagTotal; x++)
        //            {
        //                try
        //                {
        //                    clsResultados cResultadosTemp = new clsResultados();
        //                    vo_HotelValuedAvailRQ.PaginationData = x.ToString();
        //                    cResultadosTemp = getServices(vo_HotelValuedAvailRQ);
        //                    if (!cResultadosTemp.Error.Id.Equals(0))
        //                    {
        //                        DataSet dsResultadosAux = cResultadosTemp.dsResultados;

        //                        DataTable dtHotelInfoAux = dsResultadosAux.Tables[TABLA_HOTEL_INFO];
        //                        DataTable dtHotelRoomAux = dsResultadosAux.Tables[TABLA_HOTEL_ROOM];
        //                        DataTable dtHotelOcupancyAux = dsResultadosAux.Tables[TABLA_HOTEL_OCCUPANCY];

        //                        DataTable dtHotelInfo = dsResultados.Tables[TABLA_HOTEL_INFO];
        //                        DataTable dtHotelRoom = dsResultados.Tables[TABLA_HOTEL_ROOM];
        //                        DataTable dtHotelOcupancy = dsResultados.Tables[TABLA_HOTEL_OCCUPANCY];

        //                        clsDataNet.dsDataTableAdd(dtHotelInfo, dtHotelInfoAux);
        //                        clsDataNet.dsDataTableAdd(dtHotelRoom, dtHotelRoomAux);
        //                        clsDataNet.dsDataTableAdd(dtHotelOcupancy, dtHotelOcupancyAux);
        //                    }
        //                }
        //                catch { }
        //            }
        //        }
        //        clsSesiones.setResultadoHotel(cResultados.dsResultados);
        //        if (cResultados.dsResultados.Tables.Count < 5)
        //        {
        //            cParametros.Id = 0;
        //            cParametros.Code = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_CODE].ToString();
        //            cParametros.Info = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_DETAIL_MESSAGE].ToString();
        //            cParametros.Message = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_MESSAGE].ToString();
        //            cParametros.Severity = clsSeveridad.Alta;
        //            cParametros.Tipo = clsTipoError.WebServices;
        //            cParametros.Metodo = "HotelValuedAvailRQ";
        //            cParametros.Complemento = "Resultados de Hoteles";
        //            cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
        //            cParametros.Sugerencia.Add("Por favor intente de nuevo");
        //            cParametros.Code = "501";
        //            cParametros.ValidaInfo = false;
        //            cParametros.MessageBD = true;
        //            cParametros.TipoWs = Enum_ProveedorWebServices.HotelBeds;

        //            cResultados.Error = cParametros;
        //            ExceptionHandled.Publicar(cParametros);
        //        }
        //        else
        //        {
        //            cParametros.Id = 1;
        //            cResultados.Error = cParametros;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        cParametros.Id = 0;
        //        cParametros.Message = Ex.Message;
        //        cParametros.Severity = clsSeveridad.Alta;
        //        cParametros.Tipo = clsTipoError.WebServices;
        //        cParametros.Metodo = "HotelValuedAvailRQ";
        //        cParametros.Complemento = "Resultados de Hoteles";
        //        cParametros.Source = Ex.Source;
        //        cParametros.StackTrace = Ex.StackTrace;
        //        cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
        //        cParametros.Sugerencia.Add("Por favor intente de nuevo");
        //        cParametros.Code = "501";
        //        cParametros.ValidaInfo = false;
        //        cParametros.MessageBD = true;
        //        cParametros.TipoWs = Enum_ProveedorWebServices.HotelBeds;

        //        cResultados.Error = cParametros;
        //        ExceptionHandled.Publicar(cParametros);
        //    }
        //    return cResultados;
        //}
    }
}
