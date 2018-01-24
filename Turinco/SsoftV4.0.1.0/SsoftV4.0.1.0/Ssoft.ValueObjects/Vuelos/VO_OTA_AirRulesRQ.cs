using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.ValueObjects;

namespace Ssoft.Ssoft.ValueObjects.Vuelos
{
    public class VO_OTA_AirRulesRQ
    {

        public VO_OTA_AirRulesRQ()
        {
        }


        public VO_OTA_AirRulesRQ(string strClase, VO_Aeropuerto vo_AeropuertoOrigen, VO_Aeropuerto vo_AeropuertoDestino, DateTime dtmFechaSalida)
        {
            StrClase = strClase;
            DtmFechaSalida = dtmFechaSalida;
            Vo_AeropuertoOrigen = vo_AeropuertoOrigen;
            Vo_AeropuertoDestino = vo_AeropuertoDestino;
        }
    
        public string sRPH { get; set; }
        public DateTime DtmFechaSalida { get; set; }
        public string StrClase { get; set; }
        public string StrCodigoAerolinea { get; set; }
        public VO_Aeropuerto Vo_AeropuertoDestino { get; set; }
        public VO_Aeropuerto Vo_AeropuertoOrigen { get; set; }
    }
}
