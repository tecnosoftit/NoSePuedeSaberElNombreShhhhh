using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// Descripción breve de VO_Prioridades
    /// </summary>
    public class VO_Prioridades
    {
        #region [ ATRIBUTOS ]
        private string sPrecio;
        private string sDirecto;
        private string sHora;
        private string sAerolinea;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_Prioridades()
        {
        }

        public VO_Prioridades(
            string sPrecio,
            string sDirecto,
            string sHora,
            string sAerolinea)
        {
            this.sPrecio = sPrecio;
            this.sDirecto = sDirecto;
            this.sHora = sHora;
            this.sAerolinea = sAerolinea;
        }
        #endregion

        #region [ PROPIEADES ]
        public string SPrecio
        {
            get { return sPrecio; }
            set { sPrecio = value; }
        }
        public string SDirecto
        {
            get { return sDirecto; }
            set { sDirecto = value; }
        }
        public string SHora
        {
            get { return sHora; }
            set { sHora = value; }
        }
        public string SAerolinea
        {
            get { return sAerolinea; }
            set { sAerolinea = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_Prioridades() { }
        #endregion

    }
}
