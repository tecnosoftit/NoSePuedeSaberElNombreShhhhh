using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using Ssoft.Utils;
using Ssoft.ValueObjects;
using Ssoft.Rules.Pagina;

namespace WS_SsoftSabre.Utilidades
{
    public class SesionesWs
    {
        #region [ CONSTRUCTOR ]

        public SesionesWs() { }

        #endregion

       
        #region " Hotel "

      

        

        public static void GuardarSession(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
            clsNegocioSabre cWs = new clsNegocioSabre();
            string sAdultos = "0";
            string sTrayecto = vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto.ToString() + " " + vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo.ToString();
            string sIp = HttpContext.Current.Request.UserHostAddress;            

            for (int i = 0; i < vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros.Count; i++)
            {
                if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCodigo == "ADT")
                {
                    sAdultos = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCantidad.ToString();
                }
            }
            cWs.Conexion = ConfigurationManager.AppSettings["strConexion"].ToString();
            cWs.GuardarSession(AutenticacionSabre.GET_SabreSession(), "Sabre", sTrayecto, vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo, vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoDestino.SCodigo, vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaSalida.ToString(), sAdultos, sIp);            
        }
        #endregion
        #region [ DESTRUCTOR ]

        ~SesionesWs() { }

        #endregion
    }
}