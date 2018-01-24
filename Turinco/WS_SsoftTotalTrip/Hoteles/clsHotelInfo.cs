using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Ssoft.Utils;
using Ssoft.ValueObjects;
using Ssoft.ManejadorExcepciones;
using WS_SsoftTotalTrip.GetHotelInfos;
using WS_SsoftTotalTrip.Utilidades;
using Ssoft.DataNet;

namespace WS_SsoftTotalTrip.Hoteles
{
    public class clsHotelInfo
    {
        public void getServices(DataSet dsResultados)
        {
            clsParametros cParametros = new clsParametros();
            StringBuilder consulta = new StringBuilder();
            VO_Credentials vo_Credentials = clsCredenciales.Credenciales(Enum_ProveedorWebServices.TotalTrip);

            try
            {
                HotelInfoRQ oHotelInfoRQ = new HotelInfoRQ();
                HotelInfoRS oHotelInfoRS = new HotelInfoRS();

                DataTable dtData = dsResultados.Tables[clsEsquema.TABLA_HOTEL_INFO];
                int iTotal = dtData.Rows.Count;
                int[] iHotelCodes = new int[iTotal];
                int i = 0;
                string sCodeHotel = clsEsquema.COLUMN_HOTELCODE;
                string sCodeCity = string.Empty;
                try
                {
                    sCodeCity = dtData.Rows[0][clsEsquema.COLUMN_DESTINATION_CODE].ToString();
                }
                catch { }
                foreach (DataRow drData in dtData.Rows)
                {
                    iHotelCodes[i] = int.Parse(drData[sCodeHotel].ToString());
                    i++;
                }
                oHotelInfoRQ.CityCode = sCodeCity;

                oHotelInfoRQ.HotelCodes = iHotelCodes;

                oHotelInfoRQ.Username = vo_Credentials.LoginUser;
                oHotelInfoRQ.Password = vo_Credentials.PasswordUser;

                HotelInfoService oHotelInfoService = new HotelInfoService();
                oHotelInfoService.Url = clsEsquema.setConexionWs(oHotelInfoService.Url);
                oHotelInfoRS = oHotelInfoService.GetHotelInfos(oHotelInfoRQ);
                new clsEsquema().GetDatasetHotelInfo(dsResultados, oHotelInfoRS);
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
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "0";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.TotalTrip;
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public clsResultados getServicesWs(string sCodeCity)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            StringBuilder consulta = new StringBuilder();
            VO_Credentials vo_Credentials = clsCredenciales.Credenciales(Enum_ProveedorWebServices.TotalTrip);
            try
            {
                string sWhere = "ID = '" + sCodeCity + "'";
                HotelInfoRQ oHotelInfoRQ = new HotelInfoRQ();
                HotelInfoRS oHotelInfoRS = new HotelInfoRS();
                clsSerializer cSerializer = new clsSerializer();

                DataSet dsData = new DataSet();
                string strPathXML = clsValidaciones.XMLDatasetCreaGen();
                string strArchivo = "CityTT.xml";
                string strFile = strPathXML + strArchivo;
                dsData = cSerializer.XMLDatasetGen(strFile);
                DataTable dtData = new DataTable();
                string sCounty = sCodeCity;
                string sCountyId = sCodeCity;
                string sCity = sCodeCity;
                string sCityId = sCodeCity;
                try
                {
                    if (dsData != null)
                    {
                        dtData = clsDataNet.dsDataWhere(sWhere, dsData.Tables[1]);
                    }
                }
                catch { }
                int iTotal = dtData.Rows.Count;
                int[] iHotelCodes = new int[iTotal];
                int i = 0;
                string sCodeHotel = "inCodigo";
                try
                {
                    sCounty = dtData.Rows[0]["Value_Country"].ToString();
                    sCountyId = dtData.Rows[0]["ID_Country"].ToString();
                    sCity = dtData.Rows[0]["Value"].ToString();
                    sCityId = dtData.Rows[0]["ID"].ToString();
                }
                catch { }
                foreach (DataRow drData in dtData.Rows)
                {
                    iHotelCodes[i] = int.Parse(drData[sCodeHotel].ToString());
                    i++;
                }
                oHotelInfoRQ.CityCode = sCodeCity;

                oHotelInfoRQ.HotelCodes = iHotelCodes;

                oHotelInfoRQ.Username = vo_Credentials.LoginUser;
                oHotelInfoRQ.Password = vo_Credentials.PasswordUser;
                DataSet dsDataResult = new DataSet();
                HotelInfoService oHotelInfoService = new HotelInfoService();
                oHotelInfoService.Url = clsEsquema.setConexionWs(oHotelInfoService.Url);
                oHotelInfoRS = oHotelInfoService.GetHotelInfos(oHotelInfoRQ);
                new clsEsquema().GetDatasetHotelInfoWs(dsDataResult, oHotelInfoRS, sCounty, sCountyId, sCity, sCityId);
                cResultados.dsResultados = dsDataResult;
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
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "0";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.TotalTrip;
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
                clsSerializer cSerializer = new clsSerializer();
                string sWhere = "ID = '" + sCodeCity + "'";
                string strPathXML = clsValidaciones.XMLDatasetCreaGen();
                string strArchivo = "CityTT.xml";
                string strFile = strPathXML + strArchivo;
                dsData = cSerializer.XMLDatasetGen(strFile);
                try
                {
                    if (dsData != null)
                    {
                        dtData = clsDataNet.dsDataWhere(sWhere, dsData.Tables[1]);
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
