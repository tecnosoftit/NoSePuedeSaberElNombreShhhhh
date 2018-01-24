using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Ssoft.Utils;
using Ssoft.ValueObjects;
using Ssoft.ManejadorExcepciones;
using WS_SsoftTotalTrip.HotelRes;
using WS_SsoftTotalTrip.Utilidades;
using Ssoft.Ssoft.ValueObjects.Hoteles;

namespace WS_SsoftTotalTrip.Hoteles
{
    public class clsHotelRes
    {
        public clsResultados getServices()
        {
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            StringBuilder consulta = new StringBuilder();
            VO_Credentials vo_Credentials = clsCredenciales.Credenciales(Enum_ProveedorWebServices.TotalTrip);
            try
            {
                cResultados.dsResultados = clsSesiones.getReservaHotel();
                VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();

                HotelResRQ oHotelResRQ = new HotelResRQ();
                HotelResRS oHotelResRS = new HotelResRS();

                HotelFare oHotelFare = getHotelFare(cResultados.dsResultados);

                oHotelResRQ.HotelFare = oHotelFare;
                oHotelResRQ.Language = "ES";
                oHotelResRQ.Username = vo_Credentials.LoginUser; ;
                oHotelResRQ.Password = vo_Credentials.PasswordUser;
                HotelResService oHotelResService = new HotelResService();
                oHotelResService.Url = clsEsquema.setConexionWs(oHotelResService.Url);
                string sRutaGen = clsValidaciones.XMLDatasetCrea();
                string sHotelResRQ = "HotelResRQ";
                string sHotelResRS = "HotelResRS";
                clsCache cCache = new csCache().cCache();
                try
                {
                    if (cCache != null)
                    {
                        sHotelResRQ += cCache.SessionID;
                        sHotelResRS += cCache.SessionID;
                    }
                }
                catch { }
                try
                {
                    clsXML.ClaseXML(oHotelResRQ, sRutaGen + sHotelResRQ + ".xml");
                }
                catch { }

                oHotelResRS = oHotelResService.HotelRes(oHotelResRQ);
                try
                {
                    clsXML.ClaseXML(oHotelResRS, sRutaGen + sHotelResRS + ".xml");
                }
                catch { }
                cParametros = new clsEsquema().GetDatasetHotelReserva(cResultados.dsResultados, oHotelResRS);

                if (!cParametros.Id.Equals(0))
                {
                    clsSesiones.setConfirmaHotel(cResultados.dsResultados);
                    cResultados.Error = cParametros;
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Message = cResultados.dsResultados.Tables[clsEsquema.TABLA_ERROR].Rows[0][clsEsquema.COLUMN_MESSAGE].ToString();
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = "HotelResRQ";
                    cParametros.Complemento = "Reserva de Hoteles";
                    consulta.AppendLine("Credenciales: ");
                    try
                    {
                        if (vo_Credentials != null)
                        {
                            consulta.AppendLine("User: " + vo_Credentials.LoginUser);
                            consulta.AppendLine("Password: " + vo_Credentials.PasswordUser);
                            consulta.AppendLine("Url: " + vo_Credentials.UrlWebServices);
                            if (cCache != null)
                            {
                                consulta.AppendLine("Sesion Local: " + cCache.SessionID.ToString());
                            }
                        }
                    }
                    catch { }
                    cParametros.Info = consulta.ToString();
                    cParametros.ViewMessage.Add("No se realizo la reserva");
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    cParametros.Code = "503";
                    cParametros.ValidaInfo = false;
                    cParametros.MessageBD = true;
                    cParametros.TipoWs = Enum_ProveedorWebServices.TotalTrip;

                    cResultados.Error = cParametros;
                    ExceptionHandled.Publicar(cParametros);
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.Complemento = "Reserva de Hoteles";
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
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("La reserva no se confirmo");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "503";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.TotalTrip;
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
        public HotelFare getHotelFare(DataSet dsData)
        {
            clsParametros cParametros = new clsParametros();
            StringBuilder consulta = new StringBuilder();
            VO_Credentials vo_Credentials = clsCredenciales.Credenciales(Enum_ProveedorWebServices.TotalTrip);
            HotelFare oHotelFare = new HotelFare();

            bool bSoloTT = false;
            try
            {
                bSoloTT = bool.Parse(clsValidaciones.GetKeyOrAdd("bSoloTT", "False"));
            }
            catch { }

            try
            {
                VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();


                DataTable dtHotelInfo = dsData.Tables[clsEsquema.TABLA_HOTEL_INFO];
                DataTable dtHotelRoom = dsData.Tables[clsEsquema.TABLA_HOTEL_ROOM];
                DataTable dtPriceDate = dsData.Tables[clsEsquema.TABLA_PRICE];
                DataTable dtCancelationPolity = dsData.Tables[clsEsquema.TABLA_CANCELATION_POLICY];
                if (dtHotelInfo.Rows.Count > 0)
                {
                    string sWhere = string.Empty;
                    int sHotel = int.Parse(dtHotelInfo.Rows[0][clsEsquema.COLUMN_HOTELCODE].ToString());
                    string sCityCode = dtHotelInfo.Rows[0][clsEsquema.COLUMN_DESTINATION_CODE].ToString();
                    decimal dAgencyComision = clsValidaciones.getDecimalNotRound(dtHotelInfo.Rows[0][clsEsquema.COLUMN_AGENCY_COMISION].ToString());
                    decimal dIva = clsValidaciones.getDecimalNotRound(dtHotelInfo.Rows[0][clsEsquema.COLUMN_IVA].ToString());
                    int iSource = int.Parse(dtHotelInfo.Rows[0][clsEsquema.COLUMN_SOURCE].ToString());

                    Hotel oHotel = new Hotel();

                    string sAdulto = clsValidaciones.GetKeyOrAdd("AdultoHB", "AD");
                    string sInfante = clsValidaciones.GetKeyOrAdd("InfanteHB", "CH");

                    int iRoom = dtHotelRoom.Rows.Count;

                    HotelRoom[] oRoomArray = new HotelRoom[iRoom];

                    
                    oHotel.CityCode = sCityCode;
                    oHotel.HotelCode = sHotel;
                    oHotel.Source = iSource;

                    for (int i = 0; i < iRoom; i++)
                    {
                        HotelRoomType[] oHotelRoomTypeArray = new HotelRoomType[1];
                        sWhere = clsEsquema.COLUMN_HOTEL_ROOM_ID + " = " + dtHotelRoom.Rows[i][clsEsquema.COLUMN_HOTEL_ROOM_ID].ToString();
                        HotelRoom oRoom = new HotelRoom();
                        HotelRoomType oHotelRoomType = new HotelRoomType();

                        int iPax = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList.Count;
                        HotelRoomPax[] oPaxArray = new HotelRoomPax[iPax];
                        for (int j = 0; j < iPax; j++)
                        {
                            HotelRoomPax oPax = new HotelRoomPax();
                            oPax.Age = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[j].Age;
                            if (vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[j].Type.Equals(sAdulto))
                                oPax.PaxType = PaxType.Adult;
                            else
                                oPax.PaxType = PaxType.Child;

                            oPax.EmailAddress = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[j].Email;
                            oPax.FirstName = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[j].Name;
                            oPax.LastName = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[j].LastName;
                            oPax.PhoneNumber = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[j].Phone;

                            oPaxArray[j] = oPax;
                        }
                        oRoom.Paxes = oPaxArray;
                        DataRow[] drRoomsPrices = dtPriceDate.Select(sWhere);
                        int iFechas = drRoomsPrices.Length;
                        EffectiveDate[] eFechasArray = new EffectiveDate[iFechas];
                        int k = 0;
                        foreach (DataRow drRoomPrice in drRoomsPrices)
                        {
                            EffectiveDate eFechas = new EffectiveDate();
                            eFechas.Date = DateTime.Parse(drRoomPrice[clsEsquema.COLUMN_DATE].ToString()); ;
                            eFechas.AmountAfterTax = decimal.Parse(drRoomPrice[clsEsquema.COLUMN_PRICE_AMOUN_AFTER_TAX].ToString());
                            eFechas.AmountBeforeTax = decimal.Parse(drRoomPrice[clsEsquema.COLUMN_PRICE_AMOUN_BEFORE_TAX].ToString());
                            eFechas.DiscountAfterTax = decimal.Parse(drRoomPrice[clsEsquema.COLUMN_PRICE_DISCOUNT_AFTER_TAX].ToString());
                            eFechas.DiscountBeforeTax = decimal.Parse(drRoomPrice[clsEsquema.COLUMN_PRICE_DISCOUNT_BEFORE_TAX].ToString());
                            eFechasArray[k] = eFechas;

                            try
                            {
                                eFechas.Currency = drRoomPrice[clsEsquema.COLUMN_CURRENCY].ToString();
                                eFechas.ProviderCurrency = drRoomPrice[clsEsquema.COLUMN_PROVIDER_CURRENCY].ToString();
                                eFechas.ROE = decimal.Parse(drRoomPrice[clsEsquema.COLUMN_ROE].ToString());
                            }
                            catch { }

                            k++;
                        }
                        oHotelRoomType.EffectiveDates = eFechasArray;

                        DataRow[] drCancelationPolitys = dtCancelationPolity.Select(sWhere);
                        int iCancelation = drCancelationPolitys.Length;
                        CancelationPolicy[] aCancelationPolicy = new CancelationPolicy[iCancelation];
                        int m = 0;
                        foreach (DataRow drCancelationPolity in drCancelationPolitys)
                        {
                            CancelationPolicy oCancelationPolicy = new CancelationPolicy();
                            oCancelationPolicy.Amount = decimal.Parse(drCancelationPolity[clsEsquema.COLUMN_AMOUNT].ToString()); ;
                            oCancelationPolicy.FeesInclusive = bool.Parse(drCancelationPolity[clsEsquema.COLUMN_CANCELATION_FEES].ToString());
                            oCancelationPolicy.Multiplier = int.Parse(drCancelationPolity[clsEsquema.COLUMN_CANCELATION_MULTIPLIER].ToString());
                            oCancelationPolicy.NmbrOfNights = int.Parse(drCancelationPolity[clsEsquema.COLUMN_CANCELATION_TOTAL_DAYS].ToString());
                            oCancelationPolicy.OffsetDropTime = (OffsetDropTime)drCancelationPolity[clsEsquema.COLUMN_CANCELATION_DROPTIME];
                            oCancelationPolicy.OffsetTimeUnit = (OffsetTimeUnit)drCancelationPolity[clsEsquema.COLUMN_CANCELATION_TIMEOUT];
                            oCancelationPolicy.TaxAmount = decimal.Parse(drCancelationPolity[clsEsquema.COLUMN_CANCELATION_TAXAMOUNT].ToString()); ;
                            aCancelationPolicy[m] = oCancelationPolicy;
                            m++;
                        }

                        oHotelRoomType.InvBlockCode = dtHotelRoom.Rows[i][clsEsquema.COLUMN_CHARASTERISTIC].ToString();
                        oHotelRoomType.RatePlanType = dtHotelRoom.Rows[i][clsEsquema.COLUMN_CONFIRM_RATEPLANTYPE].ToString();
                        oHotelRoomType.RoomDesc = dtHotelRoom.Rows[i][clsEsquema.COLUMN_ROOM_TYPE_TEXT].ToString();
                        oHotelRoomType.RoomRate = dtHotelRoom.Rows[i][clsEsquema.COLUMN_SHRUI].ToString();
                        oHotelRoomType.RoomType = dtHotelRoom.Rows[i][clsEsquema.COLUMN_TYPE].ToString();
                        oHotelRoomType.AmountIncludedBoardSurcharge = decimal.Parse(dtHotelRoom.Rows[i][clsEsquema.COLUMN_ROOM_AMOUNT_SURCHARGE].ToString());
                        try
                        {
                            if (!dtHotelRoom.Rows[i][clsEsquema.COLUMN_ROOM_TTL].ToString().Equals("True"))
                                oHotelRoomType.TTL = DateTime.Parse(dtHotelRoom.Rows[i][clsEsquema.COLUMN_ROOM_TTL].ToString());
                        }
                        catch { }
                        oHotelRoomTypeArray[0] = oHotelRoomType;
                        oRoom.RoomTypes = oHotelRoomTypeArray;
                        oRoom.CancelationPolicies = aCancelationPolicy;

                        oRoomArray[i] = oRoom;
                    }

                    if (bSoloTT)
                        oHotelFare.ContentType = ContentType.Exclusive;
                    else
                        oHotelFare.ContentType = ContentType.NonExclusive;

                    oHotelFare.TTL = DateTime.Now.AddDays(1);
                    oHotelFare.Hot = oHotel;
                    oHotelFare.Rooms = oRoomArray;
                    oHotelFare.TTL = DateTime.Now.AddDays(1);
                    oHotelFare.AgencyCommission = dAgencyComision;
                    oHotelFare.Iva = dIva;
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
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda: " + cParametros.Message);
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "503";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.TotalTrip;
                ExceptionHandled.Publicar(cParametros);
            }
            return oHotelFare;
        }
    }
}
