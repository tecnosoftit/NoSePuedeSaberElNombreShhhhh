using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// Descripción breve de VO_Impuesto
    /// </summary>
    public class VO_Impuesto
    {
        #region [ ATRIBUTOS ]
        private decimal dValor;
        private string sCodigo;
        private string sCodigoMoneda;
        private string sDecimales;
        public const string TA = "TA";
        public const string IVA_TA = "ITA";
        public const string GASOLINA = "YQ";
        public const string IVA = "YS";
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_Impuesto()
        {
        }
        public VO_Impuesto(
            decimal dValor,
            string sCodigo,
            string sCodigoMoneda,
            string sDecimales)
        {
            this.dValor = dValor;
            this.sCodigo = sCodigo;
            this.sCodigoMoneda = sCodigoMoneda;
            this.sDecimales = sDecimales;
        }
        #endregion

        #region [ PROPIEADES ]
        public decimal DValor
        {
            get { return dValor; }
            set { dValor = value; }
        }
        public string SCodigo
        {
            get { return sCodigo; }
            set { sCodigo = value; }
        }
        public string SCodigoMoneda
        {
            get { return sCodigoMoneda; }
            set { sCodigoMoneda = value; }
        }
        public string SDecimales
        {
            get { return sDecimales; }
            set { sDecimales = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_Impuesto() { }
        #endregion

    }
}