using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS_SsoftSabre.Air
{
    public enum Enum_CabinType
    {
        Economy,
        PremiumFirst,
        First,
        PremiumBusiness,
        Business,
        PremiumEconomy,
        Y,
        S,
        C,
        J,
        F,
        P
    }
    public class VO_CabinPref
    {
        #region ATTRIBUTES
        private Enum_CabinType enum_CabinType;
        private Enum_PreferLevelType enum_PreferLevelType;
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Cabin Type Economy,PremiumFirst,First,PremiumBusiness,ETC.
        /// </summary>
        public Enum_CabinType Enum_CabinType
        {
            get { return enum_CabinType; }
            set { enum_CabinType = value; }
        }
        /// <summary>
        /// Only,Unacceptable or Preferred.
        /// </summary>
        public Enum_PreferLevelType Enum_PreferLevelType
        {
            get { return enum_PreferLevelType; }
            set { enum_PreferLevelType = value; }
        }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Basic constructor of the class.
        /// Cabin for this leg or segment.
        /// </summary>
        public VO_CabinPref()
        {

        }
        /// <summary>
        /// Create the VO with all the parameters possible
        /// Cabin for this leg or segment.
        /// </summary>
        /// <param name="enum_CabinType">Cabin Type Economy,PremiumFirst,First,PremiumBusiness,ETC.</param>
        /// <param name="enum_PreferLevelType">Only,Unacceptable or Preferred.</param>
        public VO_CabinPref(Enum_CabinType enum_CabinType, Enum_PreferLevelType enum_PreferLevelType)
        {
            this.enum_CabinType = enum_CabinType;
            this.enum_PreferLevelType = enum_PreferLevelType;
        }
        #endregion

        #region METODOS
        #endregion

        #region EVENTOS
        #endregion

        #region DESTRUCTOR
        #endregion

    }
}
