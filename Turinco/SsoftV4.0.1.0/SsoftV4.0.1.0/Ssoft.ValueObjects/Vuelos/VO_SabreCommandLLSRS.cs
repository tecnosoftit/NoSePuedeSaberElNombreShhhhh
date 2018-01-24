using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;

namespace Ssoft.ValueObjects
{
    public class VO_SabreCommandLLSRS
    {
        #region [ ATRIBUTOS ]

        private string strComando;
        private bool bCDATA;
        //private VO_Aeropuerto vo_AeropuertoOrigen;
        //private VO_Aeropuerto vo_AeropuertoDestino;
        private Enum_SabreCommandLLSRQRequestOutput eSalida;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_SabreCommandLLSRS()
        {
        }
        public VO_SabreCommandLLSRS
            (string strComando,
            bool bCDATA,
            Enum_SabreCommandLLSRQRequestOutput eSalida)
        {
            this.strComando = strComando;
            this.bCDATA = bCDATA;
            this.eSalida = eSalida;
        }

        #endregion

        #region [ PROPIEADES ]

        public string StrComando
        {
            get { return strComando; }
            set { strComando = value; }
        }
        public bool BCDATA
        {
            get { return bCDATA; }
            set { bCDATA = value; }
        }
        public Enum_SabreCommandLLSRQRequestOutput ESalida
        {
            get { return eSalida; }
            set { eSalida = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_SabreCommandLLSRS() { }
        #endregion
    }
}
