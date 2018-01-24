using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.Rules.Interface
{
    public class Criterios : ICriterios
    {
        #region [ ATRIBUTOS ]

        public string Prefix_;

        #endregion

        #region [ CONSTRUCTOR ]

        public Criterios(string prefix_)
        {
            this.Prefix_ = prefix_;
        }

        #endregion

        #region [ METODOS ]

        public bool GET_CYTIES(string Data_)
        {
            bool bEsBusqueda = false;

            string sValorAComparar = String.Empty;

            int iLength = 5 + Prefix_.Length;
            if (Data_.Length >= iLength)
            {
                sValorAComparar = Data_.Substring(5, Prefix_.Length).ToUpper();
                bEsBusqueda = sValorAComparar.CompareTo(Prefix_.ToUpper()) == 0;
            }
            return bEsBusqueda;
        }
        public bool GET_CC(string Data_)
        {
            bool bEsBusqueda = false;

            string sValorAComparar = String.Empty;

            int iLength = 0 + Prefix_.Length;
            if (Data_.Length >= iLength)
            {
                sValorAComparar = Data_.Substring(0, Prefix_.Length).ToUpper();
                bEsBusqueda = sValorAComparar.CompareTo(Prefix_.ToUpper()) == 0;
            }
            return bEsBusqueda;
        }
        public bool GET_CYTIES_COLOMBIA(string Data_)
        {
            return Data_.Substring(5, Prefix_.Length).ToUpper().CompareTo(Prefix_.ToUpper()) == 0;
        }
        public bool getAerolineas(string Data_)
        {
            bool bAerolinea = false;
            try
            {
                bAerolinea = Data_.Substring(3, Prefix_.Length).ToUpper().CompareTo(Prefix_.ToUpper()) == 0;
            }
            catch (Exception)
            { }
            return bAerolinea;
        }
        #endregion

        #region [ DESTRUCTOR ]

        ~Criterios() { }

        #endregion
    }
}
