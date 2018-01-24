using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Ssoft.Utils;
using Ssoft.ValueObjects;
using Ssoft.ManejadorExcepciones;
using WS_SsoftTotalTrip.Utilidades;

namespace WS_SsoftTotalTrip.Hoteles
{
    public class clsRoomSelect
    {
        public clsResultados getServices()
        {
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            clsHotelConfirm cHotelConfirm = new clsHotelConfirm();
            try
            {
                DataSet dsResultados = clsSesiones.getResultadoHotel();
                cParametros = new clsEsquema().GetDatasetRoomSelect(dsResultados);
                if (!cParametros.Id.Equals(0))
                {
                    clsSesiones.setReservaHotel(dsResultados);
                    cResultados = cHotelConfirm.getServices();
                    if (cResultados.Error.Id.Equals(0))
                    {
                        clsSesiones.setReservaHotel(null);
                    }
                    else
                    {
                      
                        cResultados.Error = cParametros;
                    }
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Message = cResultados.dsResultados.Tables[clsEsquema.TABLA_ERROR].Rows[0][clsEsquema.COLUMN_MESSAGE].ToString();
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = "HotelValuedAvailRQ";
                    cParametros.Complemento = "Resultados de Hoteles";
                    cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    cParametros.Code = "0";
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
                cParametros.Complemento = "Resultados de Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda");
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
