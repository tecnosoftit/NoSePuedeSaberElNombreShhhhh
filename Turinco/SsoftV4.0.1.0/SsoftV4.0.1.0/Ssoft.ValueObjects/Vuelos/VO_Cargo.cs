using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// Descripción breve de VO_Cargo
    /// </summary>
    public class VO_Cargo
    {
        #region [ ATRIBUTOS ]
        private string sIndicador;
        private string sTipo;
        private string sValor;


        #endregion

        #region [ CONSTRUCTOR ]
        public VO_Cargo()
        {
        }
        public VO_Cargo(
            string sIndicador,
            string sTipo,
            string sValor)
        {
            this.sIndicador = sIndicador;
            this.sTipo = sTipo;
            this.sValor = sValor;
        }
        #endregion

        #region [ PROPIEADES ]
        public string SIndicador
        {
            get { return sIndicador; }
            set { sIndicador = value; }
        }
        public string STipo
        {
            get { return sTipo; }
            set { sTipo = value; }
        }
        public string SValor
        {
            get { return sValor; }
            set { sValor = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_Cargo() { }
        #endregion
    }
}