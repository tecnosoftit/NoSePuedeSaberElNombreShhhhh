using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;
using Ssoft.ValueObjects;
using Ssoft.ManejadorExcepciones;
using WS_SsoftTotalTrip.HotelReservationInfo;
using WS_SsoftTotalTrip.Utilidades;

namespace WS_SsoftTotalTrip.Hoteles
{
    public class clsHotelReservationInfo
    {
        public clsResultados getServices()
        {
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            StringBuilder consulta = new StringBuilder();
            VO_Credentials vo_Credentials = clsCredenciales.Credenciales(Enum_ProveedorWebServices.TotalTrip);
            try
            {
                HotelReservationInfoRQ oHotelReservationInfoRQ = new HotelReservationInfoRQ();
                HotelReservationInfoRS oHotelReservationInfoRS = new HotelReservationInfoRS();

                oHotelReservationInfoRQ.TotalTripId = 1;
                oHotelReservationInfoRQ.TripId = "1";

                oHotelReservationInfoRQ.Username = vo_Credentials.LoginUser; ;
                oHotelReservationInfoRQ.Password = vo_Credentials.PasswordUser;
                HotelReservationInfoService oHotelReservationInfoService = new HotelReservationInfoService();
                oHotelReservationInfoService.Url = clsEsquema.setConexionWs(oHotelReservationInfoService.Url);
                oHotelReservationInfoRS = oHotelReservationInfoService.HotelReservationInfo(oHotelReservationInfoRQ);
                cResultados = new clsEsquema().GetDatasetHotel(cResultados.dsResultados, oHotelReservationInfoRS);
                if (!cResultados.Error.Id.Equals(0))
                {
                    //                    clsSesiones..setConfirmaHotel(cResultados.dsResultados);
                }
                else
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
                cParametros.Complemento = "Confirmacion de reserva de Hoteles";
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
                cParametros.ViewMessage.Add("La reserva no se confirmo");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "0";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.TotalTrip;
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
    }
}
