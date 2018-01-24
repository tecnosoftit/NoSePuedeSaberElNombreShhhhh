using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class VO_DetalleTarifa
    {
        #region [ ATRIBUTOS ]
        private List<VO_Pasajero> lvo_Pasajero;
        private string[] sCodigoTarifaBase;
        private VO_Impuesto vo_Base;
        private VO_Impuesto vo_Equivalente;
        private List<VO_Impuesto> lvo_Impuestos;
        private List<VO_Cargo> lvo_CargosAdicionales;
        private string[] sObservaciones;
        private string sFareCalc;
        private bool bTarifaWeb;
        private string sTarifaPrivada;
        private string sAerolineaValidadora;
        private string sCorporacionID;
        private string[] sAdvertencias;
        private string sTarifaPasCotizado;//Especifica el codigo de pasajero de la tarifa
        private VO_Impuesto vo_TarifaBase;
        private VO_Impuesto vo_TarifaTotal;
        private VO_TA vo_TaPasajero;//valor de la TA por para un pasajero
        private VO_TA vo_TaTotal;//valor de la TA por el total de los pasajeros
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_DetalleTarifa()
        {
        }
        #endregion

        #region [ PROPIEADES ]
        public List<VO_Pasajero> Lvo_Pasajero
        {
            get { return lvo_Pasajero; }
            set { lvo_Pasajero = value; }
        }
        public string[] SCodigoTarifaBase
        {
            get { return sCodigoTarifaBase; }
            set { sCodigoTarifaBase = value; }
        }
        public VO_Impuesto Vo_Base
        {
            get { return vo_Base; }
            set { vo_Base = value; }
        }
        public VO_Impuesto Vo_Equivalente
        {
            get { return vo_Equivalente; }
            set { vo_Equivalente = value; }
        }
        public List<VO_Impuesto> Lvo_Impuestos
        {
            get { return lvo_Impuestos; }
            set { lvo_Impuestos = value; }
        }
        public List<VO_Cargo> Lvo_CargosAdicionales
        {
            get { return lvo_CargosAdicionales; }
            set { lvo_CargosAdicionales = value; }
        }
        public string[] SObservaciones
        {
            get { return sObservaciones; }
            set { sObservaciones = value; }
        }
        public string SFareCalc
        {
            get { return sFareCalc; }
            set { sFareCalc = value; }
        }
        public bool BTarifaWeb
        {
            get { return bTarifaWeb; }
            set { bTarifaWeb = value; }
        }
        public string STarifaPrivada
        {
            get { return sTarifaPrivada; }
            set { sTarifaPrivada = value; }
        }
        public string SAerolineaValidadora
        {
            get { return sAerolineaValidadora; }
            set { sAerolineaValidadora = value; }
        }
        public string SCorporacionID
        {
            get { return sCorporacionID; }
            set { sCorporacionID = value; }
        }
        public string[] SAdvertencias
        {
            get { return sAdvertencias; }
            set { sAdvertencias = value; }
        }
        public string STarifaPasCotizado
        {
            get { return sTarifaPasCotizado; }
            set { sTarifaPasCotizado = value; }
        }
        public VO_Impuesto Vo_TarifaBase
        {
            get { return vo_TarifaBase; }
            set { vo_TarifaBase = value; }
        }
        public VO_Impuesto Vo_TarifaTotal
        {
            get { return vo_TarifaTotal; }
            set { vo_TarifaTotal = value; }
        }
        public VO_TA Vo_TaPasajero
        {
            get { return vo_TaPasajero; }
            set { vo_TaPasajero = value; }
        }
        public VO_TA Vo_TaTotal
        {
            get { return vo_TaTotal; }
            set { vo_TaTotal = value; }
        }

        #endregion

        #region [ DESTRUCTOR ]
        ~VO_DetalleTarifa() { }
        #endregion
    }
}
