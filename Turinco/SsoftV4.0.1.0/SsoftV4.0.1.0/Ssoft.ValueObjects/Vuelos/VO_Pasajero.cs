using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// Descripción breve de VO_Pasajero
    /// </summary>
    public class VO_Pasajero
    {
        #region [ ATRIBUTOS ]

        public const string ADULTO = "ADT";
        public const string NINIO = "CNN";
        public const string INFANTE = "INF";
        private string sCodigo;
        private string sCantidad;
        private List<VO_ClasificaPasajero> lvPasajeroNino;
        private List<VO_ClasificaPasajero> lvPasajeroInfante;
        private string sDetalle;
        private string sCodeGen;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_Pasajero()
        {
        }
        public VO_Pasajero(
            string sCodigo,
            string sCantidad)
        {
            this.sCodigo = sCodigo;
            this.sCantidad = sCantidad;
        }
        #endregion

        #region [ PROPIEADES ]
        public string SCodigo
        {
            get { return sCodigo; }
            set { sCodigo = value; }
        }
        public string SCantidad
        {
            get { return sCantidad; }
            set { sCantidad = value; }
        }
        public List<VO_ClasificaPasajero> LvPasajeroNino
        {
            get { return lvPasajeroNino; }
            set { lvPasajeroNino = value; }
        }
        public List<VO_ClasificaPasajero> LvPasajeroInfante
        {
            get { return lvPasajeroInfante; }
            set { lvPasajeroInfante = value; }
        }
        public string SDetalle
        {
            get { return sDetalle; }
            set { sDetalle = value; }
        }
        public string SCodeGen
        {
            get { return sCodeGen; }
            set { sCodeGen = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_Pasajero() { }
        #endregion

    }
    public class VO_ClasificaPasajero
    {
        #region [ ATRIBUTOS ]

        private string sCodigo;
        private string sCantidad;
        private string sDetalle;
        private string sCodeGen;
        private string sEdad;
        private string sFecha;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_ClasificaPasajero()
        {
        }
        public VO_ClasificaPasajero(
            string sCodigo,
            string sCantidad,
            string sEdad)
        {
            this.sCodigo = sCodigo;
            this.sCantidad = sCantidad;
            this.sEdad = sEdad;
        }
        #endregion

        #region [ PROPIEADES ]
        public string SCodigo
        {
            get { return sCodigo; }
            set { sCodigo = value; }
        }
        public string SCantidad
        {
            get { return sCantidad; }
            set { sCantidad = value; }
        }
        public string SDetalle
        {
            get { return sDetalle; }
            set { sDetalle = value; }
        }
        public string SCodeGen
        {
            get { return sCodeGen; }
            set { sCodeGen = value; }
        }
        public string SEdad
        {
            get { return sEdad; }
            set { sEdad = value; }
        }
        public string SFecha
        {
            get { return sFecha; }
            set { sFecha = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_ClasificaPasajero() { }
        #endregion

    }
}
