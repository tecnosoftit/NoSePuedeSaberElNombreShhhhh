using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    public class VO_AirItinerary
    {
        #region [ ATRIBUTOS ]
        private string sFechaSalida;
        private string sFechaLlegada;
        private string sNroVuelo;
        private string sClase;
        private string sActionCode;
        private string sNroPassenger;
        private string sOperatingAirLine;
        private string sMarketingAirLine;
        private string sAirEquip;
        private VO_Aeropuerto vo_AeropuertoOrigen;
        private VO_Aeropuerto vo_AeropuertoDestino;
        private bool bAirBook;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_AirItinerary()
        {
        }

        public VO_AirItinerary(
            string sFechaSalida,
            string sFechaLlegada,
            string sNroVuelo,
            string sClase,
            string sActionCode,
            string sNroPassenger,
            string sOperatingAirLine,
            string sMarketingAirLine,
            string sAirEquip,
            VO_Aeropuerto vo_AeropuertoOrigen,
            VO_Aeropuerto vo_AeropuertoDestino,
            bool bAirBook
            )
        {
            this.sFechaSalida = sFechaSalida;
            this.sFechaLlegada = sFechaLlegada;
            this.sNroVuelo = sNroVuelo;
            this.sClase = sClase;
            this.sActionCode = sActionCode;
            this.sNroPassenger = sNroPassenger;
            this.sOperatingAirLine = sOperatingAirLine;
            this.sMarketingAirLine = sMarketingAirLine;
            this.sAirEquip = sAirEquip;
            this.vo_AeropuertoOrigen = vo_AeropuertoOrigen;
            this.vo_AeropuertoDestino = vo_AeropuertoDestino;
            this.bAirBook = bAirBook;

        }
        #endregion

        #region [ PROPIEDADES ]

        public string SFechaSalida
        {
            get { return sFechaSalida; }
            set { sFechaSalida = value; }
        }
        public string SFechaLlegada
        {
            get { return sFechaLlegada; }
            set { sFechaLlegada = value; }
        }
        public string SNroVuelo
        {
            get { return sNroVuelo; }
            set { sNroVuelo = value; }
        }
        public string SClase
        {
            get { return sClase; }
            set { sClase = value; }
        }
        public string SActionCode
        {
            get { return sActionCode; }
            set { sActionCode = value; }
        }
        public string SNroPassenger
        {
            get { return sNroPassenger; }
            set { sNroPassenger = value; }
        }
        public string SOperatingAirLine
        {
            get { return sOperatingAirLine; }
            set { sOperatingAirLine = value; }
        }
        public string SMarketingAirLine
        {
            get { return sMarketingAirLine; }
            set { sMarketingAirLine = value; }
        }
        public string SAirEquip
        {
            get { return sAirEquip; }
            set { sAirEquip = value; }
        }
        public VO_Aeropuerto Vo_AeropuertoOrigen
        {
            get { return vo_AeropuertoOrigen; }
            set { vo_AeropuertoOrigen = value; }
        }
        public VO_Aeropuerto Vo_AeropuertoDestino
        {
            get { return vo_AeropuertoDestino; }
            set { vo_AeropuertoDestino = value; }
        }
        public bool BAirBook
        {
            get { return bAirBook; }
            set { bAirBook = value; }
        }

        #endregion

        #region [ DESTRUCTOR ]
        ~VO_AirItinerary() { }
        #endregion
    }
}
