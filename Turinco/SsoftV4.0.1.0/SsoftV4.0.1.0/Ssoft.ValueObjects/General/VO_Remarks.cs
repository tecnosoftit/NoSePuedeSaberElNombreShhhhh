using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;

namespace Ssoft.ValueObjects
{
    public class VO_Remarks
    {
    #region [ ATRIBUTOS ]

        private Enum_TipoRemark tipoRemark_;
        private string remark_;

    #endregion

    #region [ CONSTRUCTOR ]

    public VO_Remarks() { }

        public VO_Remarks(Enum_TipoRemark tipoRemark_,
                      string remark_)
    {
        this.tipoRemark_ = tipoRemark_;
        this.remark_ = remark_;
    }

    #endregion

    #region [ PROPIEADAES ]

        public Enum_TipoRemark TipoRemark
    {
        get { return tipoRemark_; }
        set { tipoRemark_ = value; }
    }

        public string Remark
    {
        get { return remark_; }
        set { remark_ = value; }
    }

    #endregion

    #region [ DESTRUCTOR ]

        ~VO_Remarks() { }

    #endregion
    }
}
