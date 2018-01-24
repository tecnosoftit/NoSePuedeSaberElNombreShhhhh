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
using WS_SsoftTotalTrip.CancelHotel;
using WS_SsoftTotalTrip.Utilidades;

namespace WS_SsoftTotalTrip.Hoteles
{
    public class clsHotelCancel
    {
        /// <summary>
        /// Metodo de cancelacion de reserva
        /// </summary>
        /// <param name="sRecord">Record a cancelar</param>
        /// <returns>cParametros, objeto de error</returns>
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
        public clsParametros getServices(string sRecord)
        {
            clsParametros cParametros = new clsParametros();
            StringBuilder consulta = new StringBuilder();
            VO_Credentials vo_Credentials = clsCredenciales.Credenciales(Enum_ProveedorWebServices.TotalTrip);
            try
            {
                HotelCancelRQ oHotelCancelRQ = new HotelCancelRQ();
                HotelCancelRS oHotelCancelRS = new HotelCancelRS();

                oHotelCancelRQ.Username = vo_Credentials.LoginUser; ;
                oHotelCancelRQ.Password = vo_Credentials.PasswordUser;
                oHotelCancelRQ.ID = int.Parse(sRecord);

                HotelCancelService oHotelCancelService = new HotelCancelService();
                oHotelCancelService.Url = clsEsquema.setConexionWs(oHotelCancelService.Url);
                string sRutaGen = clsValidaciones.XMLDatasetCrea();
                string sHotelCancelRQ = "HotelCancelRQ";
                string sHotelCancelRS = "HotelCancelRS";
                clsCache cCache = new csCache().cCache();
                try
                {
                    if (cCache != null)
                    {
                        sHotelCancelRQ += cCache.SessionID;
                        sHotelCancelRS += cCache.SessionID;
                    }
                }
                catch { }
                try
                {
                    clsXML.ClaseXML(oHotelCancelRQ, sRutaGen + sHotelCancelRQ + ".xml");
                }
                catch { }

                oHotelCancelRS = oHotelCancelService.CancelHotel(oHotelCancelRQ);
                try
                {
                    clsXML.ClaseXML(oHotelCancelRS, sRutaGen + sHotelCancelRS + ".xml");
                }
                catch { }

                cParametros = new clsEsquema().GetDatasetHotelCancel(oHotelCancelRS);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.Complemento = "Cancelacion de reserva de Hoteles";
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
                cParametros.DatoAdic = "0";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("La cancelacion no se confirmo");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "503";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.TotalTrip;
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
    }
}
