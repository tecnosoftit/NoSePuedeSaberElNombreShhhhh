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
using Ssoft.Rules.Generales;
using Ssoft.Sql;
using Ssoft.Rules.Reservas;
using Ssoft.Rules.Administrador;
using Ssoft.Ssoft.ValueObjects.Hoteles;

namespace WS_SsoftHotelBeds.Hoteles
{
    public class clsServiceAddHB
    {
        // Tabla
        private const string TABLA_HOTEL_ROOM = "HotelRoom";
        private const string TABLA_ERROR = "Error";

        // Columnas
        private const string COLUMN_CODE = "code";
        private const string COLUMN_SERVICE_HOTEL_ID = "ServiceHotel_Id";
        private const string COLUMN_AVAIL_TOKEN = "availToken";
        private const string COLUMN_CONTRACT_NAME = "Contract_Name";
        private const string COLUMN_INCOMING_CODE = "Incoming_Code";
        private const string COLUMN_CURRENCY_TEST = "Currency_Text";
        private const string COLUMN_CURRENCY_CODE = "Currency_Code";
        private const string COLUMN_HOTEL_INFO_ID = "HotelInfo_Id";
        private const string COLUMN_URL = "Url";
        private const string COLUMN_HOTEL_ROOM_ID = "HotelRoom_Id";
        private const string COLUMN_SHRUI = "SHRUI";
        private const string COLUMN_AVAIL_COUNT = "availCount";
        private const string COLUMN_ON_REQUEST = "onRequest";
        private const string COLUMN_BOARD_TEXT = "Board_Text";
        private const string COLUMN_BOARD_CODE = "Board_Code";
        private const string COLUMN_BOARD_SHORT_NAME = "Board_shortname";
        private const string COLUMN_CHARASTERISTIC = "characteristic";
        private const string COLUMN_ROOM_TYPE_TEXT = "RoomType_Text";
        private const string COLUMN_AMOUNT = "Amount";
        private const string COLUMN_AMOUNT_TEXT = "AmountText";
        private const string COLUMN_SELECCION = "Seleccion";

        private const string COLUMN_DATE_FROM = "Date_From";
        private const string COLUMN_DATE_TO = "Date_To";
        private const string COLUMN_DESTINATION_CODE = "Destination_Code";
        private const string COLUMN_HOTEL_INFO_CODE = "HotelInfo_Code";

        //private const string COLUMN_ROOM_COUNT = "RoomCount";
        //private const string COLUMN_HOTEL_OCCUPANCY_ID = "HotelOccupancy_Id";
        //private const string COLUMN_ADULT_COUNT = "AdultCount";
        //private const string COLUMN_CHILD_COUNT = "ChildCount";

        private const string COLUMN_MESSAGE = "Message";
        private const string COLUMN_DETAIL_MESSAGE = "DetailedMessage";

        public clsResultados getServices(string sId)
        {
            clsSerializer cDataXml = new clsSerializer();
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            VO_ServiceAddRQ vo_ServiceAddRQ = getParametros(sId);
            try
            {
                string sAdulto = clsValidaciones.GetKeyOrAdd("AdultoHB", "AD");

                string sInfante = clsValidaciones.GetKeyOrAdd("InfanteHB", "CH");

                string sXml = clsEstilosXmlHB.ServiceAddRQ_Hotel + ".xml";
                string sRuta = clsConfiguracionHB.RutaArchivosXml;
                string sUrl = clsConfiguracionHB.UrlWebService;
                XmlDocument xmlDoc = cDataXml.RecuperarXML(sRuta, sXml);
                clsInterfaceWSHttp cInterface = new clsInterfaceWSHttp();

                clsCredencialesHB cCredenciales = new clsCredencialesHB();

                xmlDoc = cCredenciales.Credenciales(xmlDoc, vo_ServiceAddRQ.Credentials, clsEstilosXmlHB.ServiceAddRQ_Hotel, false);

                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "availToken", "Service", vo_ServiceAddRQ.AvailToken);
                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "code", "IncomingOffice", vo_ServiceAddRQ.IncomingOffice);
                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "date", "DateFrom", vo_ServiceAddRQ.DateFrom);
                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "date", "DateTo", vo_ServiceAddRQ.DateTo);

                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "code", "Destination", vo_ServiceAddRQ.HotelInfo.Destination);
                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Code", vo_ServiceAddRQ.HotelInfo.Code, 0);
                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Name", vo_ServiceAddRQ.ContractName, 0);

                int intHabitaciones = vo_ServiceAddRQ.lHotelOccupancy.Count;

                XmlNode objNodoGen = cDataXml.AsignarNodo(xmlDoc, "AvailableRoom", 0);
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
                        cDataXml.AsignarNodo(xmlDoc, "Service", 0).AppendChild(objNodo);

                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "RoomCount", vo_ServiceAddRQ.lHotelOccupancy[intIndex].RoomCount.ToString(), intIndex);
                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "AdultCount", vo_ServiceAddRQ.lHotelOccupancy[intIndex].Occupancy.AdultCount.ToString(), intIndex);
                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "ChildCount", vo_ServiceAddRQ.lHotelOccupancy[intIndex].Occupancy.ChildCount.ToString(), intIndex);

                    XmlNode objNodoOcupancy = cDataXml.AsignarNodo(xmlDoc, "Occupancy", intIndex);

                    if (vo_ServiceAddRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList.Count == 0)
                    {
                        objNodoOcupancy.RemoveChild(objNodeAgeGen);
                    }
                    else
                    {
                        for (int x = 0; x < vo_ServiceAddRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList.Count; x++)
                        {
                            if (vo_ServiceAddRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList[x].Type == sInfante)
                            {
                                XmlNode objNodoAge = null;

                                // La primera vez actualiza el elemento, las otras veces lo copia para adicionarlo
                                if (x == 0)
                                    objNodoAge = objNodeAgeGen;
                                else
                                    objNodoAge = objNodeAgeGen.Clone();
                                if (x > 0)
                                    cDataXml.AsignarNodo(xmlDoc, "GuestList", intIndex).AppendChild(objNodoAge);

                                xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "type", "Customer", vo_ServiceAddRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList[x].Type, iInfant);
                                xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Age", vo_ServiceAddRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList[x].Age.ToString(), iInfant);
                                iInfant++;
                            }
                            else
                            {
                                XmlNode objNodoAge = null;

                                // La primera vez actualiza el elemento, las otras veces lo copia para adicionarlo
                                if (x == 0)
                                {
                                    objNodoAge = objNodeAgeGen;
                                    xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "type", "Customer", vo_ServiceAddRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList[x].Type, iInfant);
                                    xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Age", vo_ServiceAddRQ.lHotelOccupancy[intIndex].Occupancy.lGuestList[x].Age.ToString(), iInfant);
                                    iInfant++;
                                }
                            }
                        }
                    }
                    //xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "SHRUI", "HotelRoom", vo_ServiceAddRQ.HotelRoom.SHRUI, intIndex);
                    //xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "code", "Board", vo_ServiceAddRQ.HotelRoom.BoardCode, intIndex);
                    //xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "code", "RoomType", vo_ServiceAddRQ.HotelRoom.RoomTypeCode, intIndex);
                    //xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "characteristic", "RoomType", vo_ServiceAddRQ.HotelRoom.RoomTypeCharacteristic, intIndex);

                    xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "SHRUI", "HotelRoom", vo_ServiceAddRQ.lHotelOccupancy[intIndex].HotelRoom.SHRUI, intIndex);
                    xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "code", "Board", vo_ServiceAddRQ.lHotelOccupancy[intIndex].HotelRoom.BoardCode, intIndex);
                    xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "code", "RoomType", vo_ServiceAddRQ.lHotelOccupancy[intIndex].HotelRoom.RoomTypeCode, intIndex);
                    xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "characteristic", "RoomType", vo_ServiceAddRQ.lHotelOccupancy[intIndex].HotelRoom.RoomTypeCharacteristic, intIndex);

                }

                string sXmlRS = sRuta + clsSolicitudesXmlHB.ServiceAddRS_Hotel + ".xml";
                string sResponse = cInterface.ObtenerHttpWebResponse(xmlDoc.InnerXml, sUrl, clsConfiguracionHB.FormatoXml);
                try
                {
                    cDataXml.SaveXML(sXmlRS, sResponse);
                }
                catch { }
                //// 1 
                //string sXmlRS = clsSolicitudesXml.ServiceAddRS_Hotel + ".xml";
                //XmlDocument xmlDocS = cDataXml.RecuperarXML(sRuta, sXmlRS);
                //// 1 
                //string sResponse = xmlDocS.InnerXml;

                cResultados.dsResultados = cDataXml.CrearDataSet(sResponse);
                clsSesiones.setReservaHotel(cResultados.dsResultados);
                if (cResultados.dsResultados.Tables.Count < 5)
                {
                    cParametros.Id = 0;
                    cParametros.Code = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_CODE].ToString();
                    cParametros.Info = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_DETAIL_MESSAGE].ToString();
                    cParametros.Message = cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_MESSAGE].ToString();
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = "ServiceAddRQ";
                    cParametros.Complemento = "Reserva de Hoteles";
                    cParametros.ViewMessage.Add(cResultados.dsResultados.Tables[TABLA_ERROR].Rows[0][COLUMN_MESSAGE].ToString());
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    cParametros.Code = "503";
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
                cParametros.Code = "503";
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = "ServiceAddRQ";
                cParametros.Complemento = "Reserva de Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.HotelBeds;

                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
        private VO_ServiceAddRQ getParametros(string sId)
        {
            VO_ServiceAddRQ vo_ServiceAddRQ = new VO_ServiceAddRQ();
            VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();
            VO_HotelInfo vo_HotelInfo = new VO_HotelInfo();
            VO_HotelRoom vo_HotelRoom = new VO_HotelRoom();

            DataSet dsRoomType = clsSesiones.getResultadoHotel();
            string sWhere = COLUMN_SELECCION + "=" + sId + "";

            vo_ServiceAddRQ.Credentials = vo_HotelValuedAvailRQ.Credentials;
            vo_ServiceAddRQ.lHotelOccupancy = vo_HotelValuedAvailRQ.lHotelOccupancy;

            // Traemos la tabla
            DataTable dtHotelRoom = dsRoomType.Tables[TABLA_HOTEL_ROOM];

            DataRow[] drHotelRoom = dtHotelRoom.Select(sWhere);

            vo_ServiceAddRQ.AvailToken = drHotelRoom[0][COLUMN_AVAIL_TOKEN].ToString();
            vo_ServiceAddRQ.IncomingOffice = drHotelRoom[0][COLUMN_INCOMING_CODE].ToString();
            vo_ServiceAddRQ.ContractName = drHotelRoom[0][COLUMN_CONTRACT_NAME].ToString();
            vo_ServiceAddRQ.DateFrom = drHotelRoom[0][COLUMN_DATE_FROM].ToString();
            vo_ServiceAddRQ.DateTo = drHotelRoom[0][COLUMN_DATE_TO].ToString();

            vo_HotelInfo.Code = drHotelRoom[0][COLUMN_HOTEL_INFO_CODE].ToString();
            vo_HotelInfo.Destination = vo_HotelValuedAvailRQ.Destination;

            vo_HotelRoom.BoardCode = drHotelRoom[0][COLUMN_BOARD_CODE].ToString();
            vo_HotelRoom.RoomTypeCharacteristic = drHotelRoom[0][COLUMN_CHARASTERISTIC].ToString();
            vo_HotelRoom.RoomTypeCode = drHotelRoom[0][COLUMN_CODE].ToString();
            vo_HotelRoom.SHRUI = drHotelRoom[0][COLUMN_SHRUI].ToString();
            try
            {
                for (int j = 0; j < vo_ServiceAddRQ.lHotelOccupancy.Count; j++)
                {
                    VO_HotelRoom vo_HotelRoomId = new VO_HotelRoom();

                    vo_HotelRoomId.BoardCode = drHotelRoom[j][COLUMN_BOARD_CODE].ToString();
                    vo_HotelRoomId.RoomTypeCharacteristic = drHotelRoom[j][COLUMN_CHARASTERISTIC].ToString();
                    vo_HotelRoomId.RoomTypeCode = drHotelRoom[j][COLUMN_CODE].ToString();
                    vo_HotelRoomId.SHRUI = drHotelRoom[j][COLUMN_SHRUI].ToString();

                    vo_ServiceAddRQ.lHotelOccupancy[j].HotelRoom = vo_HotelRoomId;
                }
            }
            catch { }
            vo_ServiceAddRQ.HotelInfo = vo_HotelInfo;
            vo_ServiceAddRQ.HotelRoom = vo_HotelRoom;

            return vo_ServiceAddRQ;
        }
    }
}
