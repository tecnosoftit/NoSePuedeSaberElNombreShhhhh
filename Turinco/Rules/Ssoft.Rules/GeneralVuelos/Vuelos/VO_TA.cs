using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Ssoft.Rules.Pagina
{
    public class VO_TA1
    {
        #region [ ATRIBUTOS ]
        private decimal dTA;
        private decimal dImpuesto;
        private string strMoneda;
        private decimal dTACop;
        private decimal dImpuestoCop;
        #endregion

        #region [ CONSTRUCTOR ]

        public VO_TA1() { }

        public VO_TA1(decimal ta_, decimal impuesto_, string moneda_)
        {
            this.dTA = ta_;
            this.dImpuesto = impuesto_;
            this.strMoneda = moneda_;
        }

        public VO_TA1(decimal ta_, decimal impuesto_, string moneda_, decimal taCOP_, decimal impuestoCOP_)
        {
            this.dTA = ta_;
            this.dImpuesto = impuesto_;
            this.strMoneda = moneda_;
            this.dTACop = taCOP_;
            this.dImpuestoCop = impuestoCOP_;
        }

        #endregion

        #region [ PROPIEDADES ]
        public decimal DTA
        {
            get { return dTA; }
            set { dTA = value; }
        }
        public decimal DImpuesto
        {
            get { return dImpuesto; }
            set { dImpuesto = value; }
        }
        public string StrMoneda
        {
            get { return strMoneda; }
            set { strMoneda = value; }
        }
        public decimal DTACop
        {
            get { return dTACop; }
            set { dTACop = value; }
        }
        public decimal DImpuestoCop
        {
            get { return dImpuestoCop; }
            set { dImpuestoCop = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]

        ~VO_TA1() { }

        #endregion

    }
}
