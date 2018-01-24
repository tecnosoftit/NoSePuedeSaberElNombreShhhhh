using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using Ssoft.Utils;
using System.Xml.Serialization;
using Ssoft.ValueObjects;
using Ssoft.ManejadorExcepciones;
using WS_SsoftTotalTrip.HotelShop;
using WS_SsoftTotalTrip.Utilidades;
using Ssoft.Ssoft.ValueObjects.Hoteles;
using System.Web;

namespace WS_SsoftTotalTrip.Hoteles
{
    public class clsHotelShop
    {
        public clsResultados getServices(VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ)
        {
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();

            StringBuilder consulta = new StringBuilder();
            VO_Credentials vo_Credentials = clsCredenciales.Credenciales(Enum_ProveedorWebServices.TotalTrip);
            vo_HotelValuedAvailRQ.Credentials = vo_Credentials;

            bool bSoloTT = false;
            
            try
            {
                bSoloTT = bool.Parse(clsValidaciones.GetKeyOrAdd("bSoloTT", "False"));
               
            }
            catch { }

            try
            {
                clsSesiones.setParametrosHotel(vo_HotelValuedAvailRQ);

                HotelShopRQ oHotelShopRQ = new HotelShopRQ();
                HotelShopRS oHotelShopRS = new HotelShopRS();

                string sAdulto = clsValidaciones.GetKeyOrAdd("AdultoHB", "AD");
                string sInfante = clsValidaciones.GetKeyOrAdd("InfanteHB", "CH");

                int iRoom = vo_HotelValuedAvailRQ.lHotelOccupancy.Count;
                Room[] oRoomArray = new Room[iRoom];
                for (int i = 0; i < iRoom; i++)
                {
                    Room oRoom = new Room();
                    int iPax = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList.Count;
                    Pax[] oPaxArray = new Pax[iPax];
                    for (int j = 0; j < iPax; j++)
                    {
                        Pax oPax = new Pax();
                        oPax.Age = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[j].Age;
                        if (vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[j].Type.Equals(sAdulto))
                            oPax.PaxType = PaxType.Adult;
                        else
                            oPax.PaxType = PaxType.Child;

                        oPaxArray[j] = oPax;
                    }
                    oRoom.Paxes = oPaxArray;
                    oRoomArray[i] = oRoom;
                }

                oHotelShopRQ.CityTo = vo_HotelValuedAvailRQ.Destination;
                oHotelShopRQ.DateFrom = DateTime.Parse(clsValidaciones.ConverFechaSinSeparadorYMD(vo_HotelValuedAvailRQ.CheckInDate));
                oHotelShopRQ.DateTo = DateTime.Parse(clsValidaciones.ConverFechaSinSeparadorYMD(vo_HotelValuedAvailRQ.CheckOutDate));
                oHotelShopRQ.Rooms = oRoomArray;
                oHotelShopRQ.Username = vo_Credentials.LoginUser;
                oHotelShopRQ.Password = vo_Credentials.PasswordUser;
                oHotelShopRQ.Currency = clsValidaciones.GetKeyOrAdd("MonedaHotel", "USD");
                oHotelShopRQ.Language = "ES";
                int iResultados = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("CantidadRHoteles", "500"));

                if (HttpContext.Current.Session["$CantHoteles"] != null)
                {
                    iResultados = Convert.ToInt32(HttpContext.Current.Session["$CantHoteles"].ToString());
                }

                oHotelShopRQ.MaxResults = iResultados;
                if (bSoloTT)
                    oHotelShopRQ.ContentType = ContentType.Exclusive;
                else
                    oHotelShopRQ.ContentType = ContentType.NonExclusive;

                HotelShopService oHotelShopService = new HotelShopService();
                oHotelShopService.Url = clsEsquema.setConexionWs(oHotelShopService.Url);

                string sRutaGen = clsValidaciones.XMLDatasetCrea();
                string sHotelShopRQ = "HotelShopRQ";
                string sHotelShopRS = "HotelShopRS";
                try
                {
                    clsXML.ClaseXML(oHotelShopRQ, sRutaGen + sHotelShopRQ + ".xml");
                }
                catch { }

                if (HttpContext.Current.Session["$HotelShopRShilo"] != null)
                {
                    oHotelShopRS = (HotelShopRS)HttpContext.Current.Session["$HotelShopRShilo"];
                    HttpContext.Current.Session["$HotelShopRShilo"] = null;
                }
                else
                {
                    oHotelShopRS = oHotelShopService.HotelShop(oHotelShopRQ);
                }

                try
                {
                    clsXML.ClaseXML(oHotelShopRS, sRutaGen + sHotelShopRS + ".xml");
                }
                catch { }
                DataSet dsData = new DataSet();
                dsData = new clsEsquema().GetDatasetHotelShop(oHotelShopRS);
                //new clsHotelInfo().getServices(dsData);
                cResultados.dsResultados = dsData;
                clsSesiones.setResultadoHotel(cResultados.dsResultados);

                if (cResultados.dsResultados.Tables[clsEsquema.TABLA_ERROR].Rows.Count > 0)
                {
                    cParametros.Id = 0;
                    cParametros.Message = cResultados.dsResultados.Tables[clsEsquema.TABLA_ERROR].Rows[0][clsEsquema.COLUMN_MESSAGE].ToString();
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = "HotelShopRQ";

                    consulta.AppendLine("Credenciales: ");
                    try
                    {
                        if (vo_Credentials != null)
                        {
                            consulta.AppendLine("User: " + vo_Credentials.LoginUser);
                            consulta.AppendLine("Password: " + vo_Credentials.PasswordUser);
                            consulta.AppendLine("Url: " + vo_Credentials.UrlWebServices);
                            clsCache cCache = new csCache().cCache();
                            if (cCache != null)
                            {
                                consulta.AppendLine("Sesion Local: " + cCache.SessionID.ToString());
                            }
                        }
                    }
                    catch { }
                    cParametros.Info = consulta.ToString();
                    cParametros.Complemento = "Resultados de Hoteles";
                    cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    cParametros.Code = "501";
                    cParametros.ValidaInfo = false;
                    cParametros.MessageBD = true;
                    cParametros.TipoWs = Enum_ProveedorWebServices.TotalTrip;

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
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.Complemento = "Resultados de Hoteles";
                cParametros.Source = Ex.Source;

                consulta.AppendLine("Credenciales: ");
                try
                {
                    if (vo_Credentials != null)
                    {
                        consulta.AppendLine("User: " + vo_Credentials.LoginUser);
                        consulta.AppendLine("Password: " + vo_Credentials.PasswordUser);
                        consulta.AppendLine("Url: " + vo_Credentials.UrlWebServices);
                        clsCache cCache = new csCache().cCache();
                        if (cCache != null)
                        {
                            consulta.AppendLine("Sesion Local: " + cCache.SessionID.ToString());
                        }
                    }
                }
                catch { }
                cParametros.Info = consulta.ToString();
                
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "501";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.TotalTrip;
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
        public HotelShopRS getHoteles(VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ,string strMoneda,string Ruta)
        {
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            StringBuilder consulta = new StringBuilder();
            HotelShopRQ oHotelShopRQ = new HotelShopRQ();
            HotelShopRS oHotelShopRS = new HotelShopRS();
            VO_Credentials vo_Credentials = clsCredenciales.Credenciales(Enum_ProveedorWebServices.TotalTrip);
            vo_HotelValuedAvailRQ.Credentials = vo_Credentials;

            bool bSoloTT = false;

            try
            {
                bSoloTT = bool.Parse(clsValidaciones.GetKeyOrAdd("bSoloTT", "False"));

            }
            catch { }

            try
            {

                string sAdulto = clsValidaciones.GetKeyOrAdd("AdultoHB", "AD");
                string sInfante = clsValidaciones.GetKeyOrAdd("InfanteHB", "CH");

                int iRoom = vo_HotelValuedAvailRQ.lHotelOccupancy.Count;
                Room[] oRoomArray = new Room[iRoom];
                for (int i = 0; i < iRoom; i++)
                {
                    Room oRoom = new Room();
                    int iPax = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList.Count;
                    Pax[] oPaxArray = new Pax[iPax];
                    for (int j = 0; j < iPax; j++)
                    {
                        Pax oPax = new Pax();
                        oPax.Age = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[j].Age;
                        if (vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[j].Type.Equals(sAdulto))
                            oPax.PaxType = PaxType.Adult;
                        else
                            oPax.PaxType = PaxType.Child;

                        oPaxArray[j] = oPax;
                    }
                    oRoom.Paxes = oPaxArray;
                    oRoomArray[i] = oRoom;
                }

                oHotelShopRQ.CityTo = vo_HotelValuedAvailRQ.Destination;
                oHotelShopRQ.DateFrom = DateTime.Parse(clsValidaciones.ConverFechaSinSeparadorYMD(vo_HotelValuedAvailRQ.CheckInDate));
                oHotelShopRQ.DateTo = DateTime.Parse(clsValidaciones.ConverFechaSinSeparadorYMD(vo_HotelValuedAvailRQ.CheckOutDate));
                oHotelShopRQ.Rooms = oRoomArray;
                oHotelShopRQ.Username = vo_Credentials.LoginUser;
                oHotelShopRQ.Password = vo_Credentials.PasswordUser;
                oHotelShopRQ.Currency = strMoneda;
                oHotelShopRQ.Language = "ES";
                int iResultados = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("CantidadRHoteles", "500"));

                try
                {
                    if (HttpContext.Current.Session["$CantHoteles"] != null)
                    {
                        iResultados = Convert.ToInt32(HttpContext.Current.Session["$CantHoteles"].ToString());
                    }
                }
                catch { }

                oHotelShopRQ.MaxResults = iResultados;
                if (bSoloTT)
                    oHotelShopRQ.ContentType = ContentType.Exclusive;
                else
                    oHotelShopRQ.ContentType = ContentType.NonExclusive;

                HotelShopService oHotelShopService = new HotelShopService();
                oHotelShopService.Url = clsEsquema.setConexionWs(oHotelShopService.Url);

                string sRutaGen = Ruta;
                string sHotelShopRQ = "HotelShopRQ";
                string sHotelShopRS = "HotelShopRS";
                try
                {
                    clsXML.ClaseXML(oHotelShopRQ, sRutaGen + sHotelShopRQ + "hilo.xml");
                }
                catch { }

                oHotelShopRS = oHotelShopService.HotelShop(oHotelShopRQ);
                try
                {
                    clsXML.ClaseXML(oHotelShopRS, sRutaGen + sHotelShopRS + "hilo.xml");
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
                cParametros.Complemento = "Resultados de Hoteles";
                cParametros.Source = Ex.Source;

                consulta.AppendLine("Credenciales: ");
                try
                {
                    if (vo_Credentials != null)
                    {
                        consulta.AppendLine("User: " + vo_Credentials.LoginUser);
                        consulta.AppendLine("Password: " + vo_Credentials.PasswordUser);
                        consulta.AppendLine("Url: " + vo_Credentials.UrlWebServices);
                        clsCache cCache = new csCache().cCache();
                        if (cCache != null)
                        {
                            consulta.AppendLine("Sesion Local: " + cCache.SessionID.ToString());
                        }
                    }
                }
                catch { }
                cParametros.Info = consulta.ToString();

                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "501";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.TotalTrip;
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return oHotelShopRS;
        }
    }
}
