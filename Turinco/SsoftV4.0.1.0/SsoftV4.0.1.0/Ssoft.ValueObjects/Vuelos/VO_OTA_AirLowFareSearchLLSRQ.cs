using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// OTA_AirLowFareSearchLLSRQ:
    /// permite realizar cotizaciones con el comando JR.
    /// </summary>
    public class VO_OTA_AirLowFareSearchLLSRQ
    {
        #region [ ATRIBUTOS ]

        private List<VO_OriginDestinationInformation> lvo_Rutas;//Rutas a cotizar
        private string sMaximasParadas;
        private List<string> lsAerolineaPreferida = new List<string>();
        private List<string> lsClase;
        private List<string> lsExcluirAerolinea;
        private string sVuelosARetornar;//nuemero de resultados maximos q espera recivir del WS
        private bool bRetornarMaxResultados;
        private bool bOnline;//Quiere decir que busque vuelos solamente con la misma aerolinea
        private bool bInterLineado;//Quiere decir que permita vuelos con diferentes aerolineas
        private string sFechaTiqueteo;
        private string sPreferenciaIntervaloSal;//Permite especificar un intervalo de horas para la salida
        private string sPreferenciaIntervaloLleg;//Permite especificar un intervalo de horas para la salida
        private List<VO_Pasajero> lvo_Pasajeros;
        private string sCodMonedaCotizacion;
        private string sCodTarifaNegociada;
        private string sCodPaxNegociada;
        private bool bFareCalc;
        private bool bSoloSegmentos;
        private bool bConFarCalc;
        private bool bFarePlublica;
        private bool bFarePrivada;
        private VO_Prioridades vo_Prioridades;//Especifica por q campo se ordenaran los resultados
        private bool bSinImpuestos;
        private List<string> sCodigosImpExcluir;//excluye los impuestos enviados en el list en la cotizacion
        private List<VO_Impuesto> lvo_SobreEscribir;//sobreescribe el valor de una lista de impuestos
        private Enum_TipoTrayecto eTipoTrayecto;
        private Enum_TipoVuelo eTipoVuelo;
        private Enum_TipoVuelo eTipoSalida;
        private List<string> lsEdadesNinios;
        private List<string> lsEdadesInfantes;
        private int iRuta;
        private bool bHoras;
        private List<string> lsRPH = new List<string>();
        private decimal dMarkUp;
        private Enum_OrigenBusqueda eOrigenBusqueda = Enum_OrigenBusqueda.Normal;
        private string sPseudoPlanes;
        private string sCodigoPlan;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_OTA_AirLowFareSearchLLSRQ()
        {
        }

        public VO_OTA_AirLowFareSearchLLSRQ(
            List<VO_OriginDestinationInformation> lvo_Rutas,
            string sMaximasParadas,
            List<string> lsAerolineaPreferida,
            List<string> lsClase,
            List<string> lsExcluirAerolinea,
            string sVuelosARetornar,
            bool bRetornarMaxResultados,
            bool bOnline,
            bool bInterLineado,
            string sFechaTiqueteo,
            string sPreferenciaIntervaloSal,
            string sPreferenciaIntervaloLleg,
             List<VO_Pasajero> lvo_Pasajeros,
            string sCodMonedaCotizacion,
            string sCodTarifaNegociada,
            bool bFareCalc,
            bool bSoloSegmentos,
            bool bConFarCalc,
            bool bFarePlublica,
            bool bFarePrivada,
            VO_Prioridades vo_Prioridades,
            bool bSinImpuestos,
            List<string> sCodigosImpExcluir,
            List<VO_Impuesto> lvo_SobreEscribir,
            Enum_TipoTrayecto eTipoTrayecto,
            Enum_TipoVuelo eTipoVuelo,
            List<string> lsEdadesNinios,
            List<string> lsEdadesInfante)
        {
            this.sMaximasParadas = sMaximasParadas;
            this.lsAerolineaPreferida = lsAerolineaPreferida;
            this.lsClase = lsClase;
            this.lsExcluirAerolinea = lsExcluirAerolinea;
            this.sVuelosARetornar = sVuelosARetornar;
            this.bRetornarMaxResultados = bRetornarMaxResultados;
            this.bOnline = bOnline;
            this.bInterLineado = bInterLineado;
            this.sFechaTiqueteo = sFechaTiqueteo;
            this.sPreferenciaIntervaloSal = sPreferenciaIntervaloSal;
            this.sPreferenciaIntervaloLleg = sPreferenciaIntervaloLleg;
            this.lvo_Pasajeros = lvo_Pasajeros;
            this.sCodMonedaCotizacion = sCodMonedaCotizacion;
            this.sCodTarifaNegociada = sCodTarifaNegociada;
            this.bFareCalc = bFareCalc;
            this.bSoloSegmentos = bSoloSegmentos;
            this.bConFarCalc = bConFarCalc;
            this.bFarePlublica = bFarePlublica;
            this.bFarePrivada = bFarePrivada;
            this.vo_Prioridades = vo_Prioridades;
            this.bSinImpuestos = bSinImpuestos;
            this.sCodigosImpExcluir = sCodigosImpExcluir;
            this.lvo_SobreEscribir = lvo_SobreEscribir;
            this.eTipoTrayecto = eTipoTrayecto;
            this.eTipoVuelo = eTipoVuelo;
            this.lsEdadesNinios = lsEdadesNinios;
            this.lsEdadesInfantes = lsEdadesInfante;
        }
        #endregion

        #region [ PROPIEADES ]

        public List<VO_OriginDestinationInformation> Lvo_Rutas
        {
            get { return lvo_Rutas; }
            set { lvo_Rutas = value; }
        }
        public string SMaximasParadas
        {
            get { return sMaximasParadas; }
            set { sMaximasParadas = value; }
        }
        public List<string> LsAerolineaPreferida
        {
            get { return lsAerolineaPreferida; }
            set { lsAerolineaPreferida = value; }
        }
        public List<string> LsClase
        {
            get { return lsClase; }
            set { lsClase = value; }
        }
        public List<string> LsExcluirAerolinea
        {
            get { return lsExcluirAerolinea; }
            set { lsExcluirAerolinea = value; }
        }
        public string SVuelosARetornar
        {
            get { return sVuelosARetornar; }
            set { sVuelosARetornar = value; }
        }
        public bool BRetornarMaxResultados
        {
            get { return bRetornarMaxResultados; }
            set { bRetornarMaxResultados = value; }
        }
        public bool BOnline
        {
            get { return bOnline; }
            set { bOnline = value; }
        }
        public bool BInterLineado
        {
            get { return bInterLineado; }
            set { bInterLineado = value; }
        }
        public string SFechaTiqueteo
        {
            get { return sFechaTiqueteo; }
            set { sFechaTiqueteo = value; }
        }
        public string SPreferenciaIntervaloSal
        {
            get { return sPreferenciaIntervaloSal; }
            set { sPreferenciaIntervaloSal = value; }
        }
        public string SPreferenciaIntervaloLleg
        {
            get { return sPreferenciaIntervaloLleg; }
            set { sPreferenciaIntervaloLleg = value; }
        }
        public List<VO_Pasajero> Lvo_Pasajeros
        {
            get { return lvo_Pasajeros; }
            set { lvo_Pasajeros = value; }
        }
        public string SCodMonedaCotizacion
        {
            get { return sCodMonedaCotizacion; }
            set { sCodMonedaCotizacion = value; }
        }
        public string SCodTarifaNegociada
        {
            get { return sCodTarifaNegociada; }
            set { sCodTarifaNegociada = value; }
        }
        public string SCodPaxNegociada
        {
            get { return sCodPaxNegociada; }
            set { sCodPaxNegociada = value; }
        }
        public bool BFareCalc
        {
            get { return bFareCalc; }
            set { bFareCalc = value; }
        }
        public bool BSoloSegmentos
        {
            get { return bSoloSegmentos; }
            set { bSoloSegmentos = value; }
        }
        public bool BConFarCalc
        {
            get { return bConFarCalc; }
            set { bConFarCalc = value; }
        }
        public bool BFarePlublica
        {
            get { return bFarePlublica; }
            set { bFarePlublica = value; }
        }
        public bool BFarePrivada
        {
            get { return bFarePrivada; }
            set { bFarePrivada = value; }
        }
        public VO_Prioridades Vo_Prioridades
        {
            get { return vo_Prioridades; }
            set { vo_Prioridades = value; }
        }
        public bool BSinImpuestos
        {
            get { return bSinImpuestos; }
            set { bSinImpuestos = value; }
        }
        public List<string> SCodigosImpExcluir
        {
            get { return sCodigosImpExcluir; }
            set { sCodigosImpExcluir = value; }
        }
        public List<VO_Impuesto> Lvo_SobreEscribir
        {
            get { return lvo_SobreEscribir; }
            set { lvo_SobreEscribir = value; }
        }
        public Enum_TipoTrayecto ETipoTrayecto
        {
            get { return eTipoTrayecto; }
            set { eTipoTrayecto = value; }
        }
        public Enum_TipoVuelo ETipoVuelo
        {
            get { return eTipoVuelo; }
            set { eTipoVuelo = value; }
        }
        public Enum_TipoVuelo ETipoSalida
        {
            get { return eTipoSalida; }
            set { eTipoSalida = value; }
        }
        public List<string> LsEdadesNinios
        {
            get { return lsEdadesNinios; }
            set { lsEdadesNinios = value; }
        }
        public List<string> LsEdadesInfantes
        {
            get { return lsEdadesInfantes; }
            set { lsEdadesInfantes = value; }
        }
        public int Ruta
        {
            get { return iRuta; }
            set { iRuta = value; }
        }
        public bool BHoras
        {
            get { return bHoras; }
            set { bHoras = value; }
        }
        public List<string> LsRPH
        {
            get { return lsRPH; }
            set { lsRPH = value; }
        }
        public decimal DMarkUp
        {
            get { return dMarkUp; }
            set { dMarkUp = value; }
        }
        public Enum_OrigenBusqueda EOrigenBusqueda
        {
            get { return eOrigenBusqueda; }
            set { eOrigenBusqueda = value; }
        }
        public string SPseudoPlanes
        {
            get { return sPseudoPlanes; }
            set { sPseudoPlanes = value; }
        }
        public string CodigoPlan
        {
            get { return sCodigoPlan; }
            set { sCodigoPlan = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_OTA_AirLowFareSearchLLSRQ() { }
        #endregion

    }
}
