using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.ValueObjects;

namespace WS_SsoftSabre.Air
{
    public enum Enum_IntelliSellTransaction
    {
        CALENDAR_SHOPPING_AD1,
        CALENDAR_SHOPPING_AD3,
        BFM_50,
        BFM_100,
        BFM_200,

    }
    
    /// <summary>
    /// Set the information for BargainFinderMax_ADRQservice 
    /// </summary>
    public class VO_BargainFinderMax_ADRQ : VO_SabreBase
    {
        #region ATTRIBUTES
        public const string AD1 = "AD1";
        public const string AD3 = "AD3";
        private List<VO_OriginDestinationInformation> lvo_Segments;//Rutas a cotizar
        private string sMaxStopsQuantity;
        private VO_CabinPref[] vo_CabinPrefs;
        private VO_VendorPref[] vo_VendorPref;
        private int[] intMaxConnections;
        private List<VO_Pasajero> lvo_Passengers;
        private string[] strNegotiatedFareCode;
        private Enum_IntelliSellTransaction enum_IntelliSellTransaction;
        private VO_Priority vo_Priority;
        private List<VO_TravelerInfoSummary> lvo_TravelerInfoSummary;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Information to access BFM ADRQ (Calendar shopping)
        /// </summary>
        public VO_BargainFinderMax_ADRQ(string strEnviroment)
            : base(strEnviroment)
        {
        }

        public VO_BargainFinderMax_ADRQ()
        {
        }

        /// <summary>
        /// Information to access BFM ADRQ (Calendar shopping)
        /// </summary>
        /// <param name="lvo_Segments">Origin Destination information </param>
        /// <param name="sMaximasParadas">indicates the max number of stops requested in each leg. Itineraries with more stops than the specified value will not be returned. When specified, it disables the FlightTypePref@MaxConnections option.</param>
        /// <param name="vo_CabinPrefs"> Cabin for this leg or segment.</param>
        /// <param name="vo_VendorPref">Cannot combine Vo_VendorPref with VO_OriginDestinationInformation.vo_VendorPref</param>
        /// <param name="intMaxConnections">Maximum number of connections requested in each leg. Itineraries with more connections than the specified value will not be returned.</param>
        public VO_BargainFinderMax_ADRQ(
            List<VO_OriginDestinationInformation> lvo_Segments,
            string sMaximasParadas,
            VO_CabinPref[] vo_CabinPrefs,
            VO_VendorPref[] vo_VendorPref,
            int[] intMaxConnections,
            Enum_IntelliSellTransaction enum_IntelliSellTransaction,
            string strEnviroment)
            : base(strEnviroment)
        {
            this.sMaxStopsQuantity = sMaximasParadas;
            this.vo_CabinPrefs = vo_CabinPrefs;
            this.vo_VendorPref = vo_VendorPref;
            this.intMaxConnections = intMaxConnections;
            this.enum_IntelliSellTransaction = enum_IntelliSellTransaction;
        }
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Origin Destination information 
        /// </summary>
        public List<VO_OriginDestinationInformation> Lvo_Segments
        {
            get { return lvo_Segments; }
            set { lvo_Segments = value; }
        }
        /// <summary>
        /// indicates the max number of stops requested in each leg. Itineraries with more 
        /// stops than the specified value will not be returned. When specified, 
        /// it disables the FlightTypePref@MaxConnections option.
        /// </summary>
        public string SMaxStopsQuantity
        {
            get { return sMaxStopsQuantity; }
            set { sMaxStopsQuantity = value; }
        }
        /// <summary>
        ///  Cabin for this leg or segment.
        /// </summary>
        public VO_CabinPref[] Vo_CabinPrefs
        {
            get { return vo_CabinPrefs; }
            set { vo_CabinPrefs = value; }
        }
        /// <summary>
        /// Cannot combine Vo_VendorPref with VO_OriginDestinationInformation.vo_VendorPref
        /// </summary>
        public VO_VendorPref[] Vo_VendorPref
        {
            get { return vo_VendorPref; }
            set { vo_VendorPref = value; }
        }
        /// <summary>
        /// Maximum number of connections requested in each leg. Itineraries with more connections than the specified value will not be returned.
        /// </summary>
        public int[] IntMaxConnections
        {
            get { return intMaxConnections; }
            set { intMaxConnections = value; }
        }
        /// <summary>
        /// Passager information
        /// </summary>
        public List<VO_Pasajero> Lvo_Passengers
        {
            get { return lvo_Passengers; }
            set { lvo_Passengers = value; }
        }
        /// <summary>
        ///  Corporate ID to retrieve fares filed with this corporate ID.
        /// You can specify up to four Corporate IDs or Account codes combined in a single request.
        /// </summary>
        public string[] StrNegotiatedFareCode
        {
            get { return strNegotiatedFareCode; }
            set { strNegotiatedFareCode = value; }
        }
        /// <summary>
        /// Define wheter returns itineraries for up to 9 or 49 alternate date combinations 
        /// </summary>
        public Enum_IntelliSellTransaction Enum_IntelliSellTransaction
        {
            get { return enum_IntelliSellTransaction; }
            set { enum_IntelliSellTransaction = value; }
        }
        /// <summary>
        /// Especify the order to return the availability options
        /// </summary>
        public VO_Priority Vo_Priority
        {
            get { return vo_Priority; }
            set { vo_Priority = value; }
        }
        /// <summary>
        /// This attribute is used to send the information for the gruops for 
        /// BFM_SAPT sws
        /// </summary>
        public List<VO_TravelerInfoSummary> Lvo_TravelerInfoSummary
        {
            get { return lvo_TravelerInfoSummary; }
            set { lvo_TravelerInfoSummary = value; }
        }
        #endregion

        #region METODOS
        public  void setInitialize()
        {
            VO_MessageHeader vo_MessageHeader = new VO_MessageHeader();
            vo_MessageHeader.StrAction = "BargainFinderMax_ADRQ";
            vo_MessageHeader.StrValue = "Air Shopping Service";
            this.Vo_MessageHeader = vo_MessageHeader;
        }
        #endregion

        #region DESTRUCTOR
        ~VO_BargainFinderMax_ADRQ() { }
        #endregion
    }
}
