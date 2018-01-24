using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;

public class VO_ResultTravelItineraryAddInfo 
{
    #region [ ATRIBUTOS ]

        private List<VO_ResultTravelItineraryAddInfo_Itinerario> itinerario_;
        private List<VO_ResultTravelItineraryAddInfo_Pasajeros> pasajeros_;

    #endregion

    #region [ CONSTRUCTOR ]

        public VO_ResultTravelItineraryAddInfo(List<VO_ResultTravelItineraryAddInfo_Itinerario> itinerario_,
                                         List<VO_ResultTravelItineraryAddInfo_Pasajeros> pasajeros_                                         
                                        )
	    {
            this.itinerario_ = itinerario_;
            this.pasajeros_ = pasajeros_;
        }

    #endregion

    #region [ PROPIEADES ]

        public List<VO_ResultTravelItineraryAddInfo_Itinerario> Itinerario_
        {
            get { return itinerario_; }
            set { itinerario_ = value; }
        }

        public List<VO_ResultTravelItineraryAddInfo_Pasajeros> Pasajeros_
        {
            get { return pasajeros_; }
            set { pasajeros_ = value; }
        }

    #endregion

    #region [ DESSTRUCTOR ]

        ~VO_ResultTravelItineraryAddInfo() { }

    #endregion
}
