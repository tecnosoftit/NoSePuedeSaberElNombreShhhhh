using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    public class VO_SegmentSelect
    {
        #region [ ATRIBUTOS ]

        private string sNumber;
        private string sEndNumber;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_SegmentSelect()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public VO_SegmentSelect(
           string sNumber,
           string sEndNumber)
        {
            this.sNumber = sNumber;
            this.sEndNumber = sEndNumber;
        }
        #endregion

        #region [ PROPIEADES ]
        public string SNumber
        {
            get { return sNumber; }
            set { sNumber = value; }
        }
        public string SEndNumber
        {
            get { return sEndNumber; }
            set { sEndNumber = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_SegmentSelect() { }
        #endregion
    }
}

