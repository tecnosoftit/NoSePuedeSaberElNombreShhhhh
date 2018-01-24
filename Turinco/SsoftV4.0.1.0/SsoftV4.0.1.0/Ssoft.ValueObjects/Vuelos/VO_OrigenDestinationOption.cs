using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;

namespace Ssoft.ValueObjects
{
    public class VO_OrigenDestinationOption
    {
        #region [ ATRIBUTOS ]

        private int iItinerary;
        private string sRPH;
        private List<VO_AirItinerary> lvo_AirItinerary;//Rutas a cotizar

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_OrigenDestinationOption()
        {
        }

        public VO_OrigenDestinationOption(
            int iItinerary,
            string sRPH)
        {
            this.iItinerary = iItinerary;
            this.sRPH = sRPH;
        }
        #endregion

        #region [ PROPIEADES ]

        public int IItinerary
        {
            get { return iItinerary; }
            set { iItinerary = value; }
        }
        public string SRPH
        {
            get { return sRPH; }
            set { sRPH = value; }
        }
        public List<VO_AirItinerary> Lvo_AirItinerary
        {
            get { return lvo_AirItinerary; }
            set { lvo_AirItinerary = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_OrigenDestinationOption() { }
        #endregion
    }
}
