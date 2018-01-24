using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// rutas devueltas por el WS OTA_AirLowFareSearch
    /// </summary>
    public class VO_SegmentoAereo
    {
        #region [ ATRIBUTOS ]
        private string sFechaSalida;
        private string sFechaLlegada;
        private string sNumeroVuelo;
        private string sClase;
        private string sConexiones;
        private string sTiempoVuelo;
        private VO_Aeropuerto vo_AeropuertoOrigen;
        private VO_Aeropuerto vo_AeropuertoDestino;
        private string sAerolineaValidadora;
        private string sTipoAvion;
        private string sAerolineaOperadora;
        private bool bCambioSalida;//EJ: llega al JF Kennedy y sale de la Guardia o de un terminal diferente al que arrivo
        private string sGrupoCasados;
        private string sTipoCabina;//Lo mismo que la clase
        private string sCodigoComida;
        private string sConexion;//Indica que el segmento es una conexion
        private string sTiempoZonaSalida;//zona GMT de salida
        private string sTiempoZonaLlegada;//zona GMT de Llegada
        private bool bETicket;//Permite emitir tiquete electronico
        private string sCodigoDivisionRes;//represents the booking code if the reservation needs to be divided
        private List<string> sObservacionesTarifa;
        private VO_Aeropuerto vo_AeropuertoIntermedio;
        private VO_SegmentoAereo vo_SegmentoIntermedio;

        /*private string sIntermedioSalida;
        private string sIntermedioLlegada;
        private string sIntermedioTrascurrido;
        private string sIntermedioDuracion;
        private string sIntermedioTiempoZona;//zona GMT de punto intermedio*/

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_SegmentoAereo()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #endregion

        #region [ PROPIEADES ]
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
        public string SNumeroVuelo
        {
            get { return sNumeroVuelo; }
            set { sNumeroVuelo = value; }
        }
        public string SClase
        {
            get { return sClase; }
            set { sClase = value; }
        }
        public string SConexiones
        {
            get { return sConexiones; }
            set { sConexiones = value; }
        }
        public string STiempoVuelo
        {
            get { return sTiempoVuelo; }
            set { sTiempoVuelo = value; }
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
        public string SAerolineaValidadora
        {
            get { return sAerolineaValidadora; }
            set { sAerolineaValidadora = value; }
        }
        public string STipoAvion
        {
            get { return sTipoAvion; }
            set { sTipoAvion = value; }
        }
        public string SAerolineaOperadora
        {
            get { return sAerolineaOperadora; }
            set { sAerolineaOperadora = value; }
        }
        public bool BCambioSalida
        {
            get { return bCambioSalida; }
            set { bCambioSalida = value; }
        }
        public string SGrupoCasados
        {
            get { return sGrupoCasados; }
            set { sGrupoCasados = value; }
        }
        public string STipoCabina
        {
            get { return sTipoCabina; }
            set { sTipoCabina = value; }
        }
        public string SCodigoComida
        {
            get { return sCodigoComida; }
            set { sCodigoComida = value; }
        }
        public string SConexion
        {
            get { return sConexion; }
            set { sConexion = value; }
        }
        public string STiempoZonaSalida
        {
            get { return sTiempoZonaSalida; }
            set { sTiempoZonaSalida = value; }
        }
        public string STiempoZonaLlegada
        {
            get { return sTiempoZonaLlegada; }
            set { sTiempoZonaLlegada = value; }
        }
        public bool BETicket
        {
            get { return bETicket; }
            set { bETicket = value; }
        }
        public string SCodigoDivisionRes
        {
            get { return sCodigoDivisionRes; }
            set { sCodigoDivisionRes = value; }
        }
        public List<string> SObservacionesTarifa
        {
            get { return sObservacionesTarifa; }
            set { sObservacionesTarifa = value; }
        }
        public VO_Aeropuerto Vo_AeropuertoIntermedio
        {
            get { return vo_AeropuertoIntermedio; }
            set { vo_AeropuertoIntermedio = value; }
        }
        public VO_SegmentoAereo Vo_SegmentoIntermedio
        {
            get { return vo_SegmentoIntermedio; }
            set { vo_SegmentoIntermedio = value; }
        }

        #endregion

        #region [ DESTRUCTOR ]
        ~VO_SegmentoAereo() { }
        #endregion
    }
}
