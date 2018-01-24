using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// Especifica una ruta a cotizar
    /// </summary>
    public class VO_OriginDestinationInformation
    {
        #region [ ATRIBUTOS ]
        private string sFechaSalida;
        private string sIntervaloSalida;//Permite especificar un intervalo de horas para la salida
        private string sFechaLlegada;
        private string sIntervaloLlegada;//Permite especificar un intervalo de horas para la llegada
        private VO_Aeropuerto vo_AeropuertoOrigen;
        private VO_Aeropuerto vo_AeropuertoDestino;
        private string oTipoSegmento;//Utiliza la case tipo de segmento, O, TRUNK, X
        //perimite especificar el tipo de segmento O,X,ARUNK
        private bool bSinDisponibilidad;//indica que se busquen vuelos asi  no tengan disponibilidad
        private string sTiempoAlternativo;// horas adicionales con las que se va a buscar.ej:si pongo "2" me buscara dos horas antes y despues
        private VO_AeropuertoAlterno vo_AeropuertoAlterno;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_OriginDestinationInformation()
        {
        }

        public VO_OriginDestinationInformation(
            string sFechaSalida,
            string sIntervaloSalida,
            string sFechaLlegada,
            string sIntervaloLlegada,
            VO_Aeropuerto vo_AeropuertoOrigen,
            VO_Aeropuerto vo_AeropuertoDestino,
            string oTipoSegmento,
            bool bSinDisponibilidad,
            string sTiempoAlternativo,
            VO_AeropuertoAlterno vo_AeropuertoAlterno
            )
        {
            this.sFechaSalida = sFechaSalida;
            this.sIntervaloSalida = sIntervaloSalida;
            this.sFechaLlegada = sFechaLlegada;
            this.sIntervaloLlegada = sIntervaloLlegada;
            this.vo_AeropuertoOrigen = vo_AeropuertoOrigen;
            this.vo_AeropuertoDestino = vo_AeropuertoDestino;
            this.oTipoSegmento = oTipoSegmento;
            this.bSinDisponibilidad = bSinDisponibilidad;
            this.sTiempoAlternativo = sTiempoAlternativo;
            this.vo_AeropuertoAlterno = vo_AeropuertoAlterno;

        }
        #endregion

        #region [ PROPIEDADES ]

        public string SFechaSalida
        {
            get { return sFechaSalida; }
            set { sFechaSalida = value; }
        }
        public string SIntervaloSalida
        {
            get { return sIntervaloSalida; }
            set { sIntervaloSalida = value; }
        }
        public string SFechaLlegada
        {
            get { return sFechaLlegada; }
            set { sFechaLlegada = value; }
        }
        public string SIntervaloLlegada
        {
            get { return sIntervaloLlegada; }
            set { sIntervaloLlegada = value; }
        }
        public VO_Aeropuerto Vo_AeropuertoOrigen
        {
            get { return vo_AeropuertoOrigen; }
            set { vo_AeropuertoOrigen = value; }
        }
        public string OTipoSegmento
        {
            get { return oTipoSegmento; }
            set { oTipoSegmento = value; }
        }
        public VO_Aeropuerto Vo_AeropuertoDestino
        {
            get { return vo_AeropuertoDestino; }
            set { vo_AeropuertoDestino = value; }
        }
        public bool BSinDisponibilidad
        {
            get { return bSinDisponibilidad; }
            set { bSinDisponibilidad = value; }
        }
        public string STiempoAlternativo
        {
            get { return sTiempoAlternativo; }
            set { sTiempoAlternativo = value; }
        }
        public VO_AeropuertoAlterno Vo_AeropuertoAlterno
        {
            get { return vo_AeropuertoAlterno; }
            set { vo_AeropuertoAlterno = value; }
        }

        #endregion

        #region [ DESTRUCTOR ]
        ~VO_OriginDestinationInformation() { }
        #endregion

    }
    public class TipoSegmento
    {
        // Tipos de segmentos
        public const string O = "O";
        public const string ARUNK = "ARUNK";
        public const string X = "X";
    }
}
