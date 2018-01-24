using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// Descripción breve de VO_ReglasTarifa
    /// </summary>
    public class VO_ReglasTarifa
    {
        #region [ ATRIBUTOS ]


        private string sCodigoPrecio;//Codigo de la tarifa basica usado para cotizar
        private VO_Aeropuerto vo_AeropuertoSalida;
        private VO_Aeropuerto vo_AeropuertoLlegada;
        private string sFilingAirline;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_ReglasTarifa()
        {
        }
        #endregion

        #region [ PROPIEADES ]
        public string SCodigoPrecio
        {
            get { return sCodigoPrecio; }
            set { sCodigoPrecio = value; }
        }
        public VO_Aeropuerto Vo_AeropuertoSalida
        {
            get { return vo_AeropuertoSalida; }
            set { vo_AeropuertoSalida = value; }
        }
        public VO_Aeropuerto Vo_AeropuertoLlegada
        {
            get { return vo_AeropuertoLlegada; }
            set { vo_AeropuertoLlegada = value; }
        }
        public string SFilingAirline
        {
            get { return sFilingAirline; }
            set { sFilingAirline = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_ReglasTarifa() { }
        #endregion
    }
}
