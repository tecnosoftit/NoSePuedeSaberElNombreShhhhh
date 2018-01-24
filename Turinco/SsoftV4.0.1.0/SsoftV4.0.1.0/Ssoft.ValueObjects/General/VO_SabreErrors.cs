using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;

namespace Ssoft.ValueObjects
{
    public class VO_SabreErrors
    {
        #region [ ATRIBUTOS ]

        private string error_;
        private List<string> solucion_;

        #endregion

        #region [ CONSTRUCTOR ]

        public VO_SabreErrors(string error_, List<string> solucion_)
        {
            this.error_ = error_;
            this.solucion_ = solucion_;
        }

        #endregion

        #region [ PROPIEDADES ]

        public string Error_
        {
            get { return error_; }
            set { error_ = value; }
        }

        public List<string> Solucion_
        {
            get { return solucion_; }
            set { solucion_ = value; }
        }

        #endregion

        #region [ DESTRUCTOR ]

        ~VO_SabreErrors() { }

        #endregion
    }
}