using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS_SsoftSabre.Air
{

    public enum Enum_PreferLevelType
    {
        Only,
        Unacceptable,
        Preferred,
    }
    public class VO_VendorPref
    {
        #region ATTRIBUTES
        private string strCode;
        private Enum_PreferLevelType enum_PreferLevelType;
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Airline Code
        /// </summary>
        public string StrCode
        {
            get { return strCode; }
            set { strCode = value; }
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
        /// Cannot combine with lsIncludeVendorPref.
        /// </summary>
        public VO_VendorPref()
        {
        }
        /// <summary>
        /// Cannot combine with lsIncludeVendorPref.
        /// </summary>
        /// <param name="strCode">Airline Code</param>
        /// <param name="enum_PreferLevelType">Only,Unacceptable or Preferred.</param>
        public VO_VendorPref(string strCode, Enum_PreferLevelType enum_PreferLevelType)
        {
            this.strCode = strCode;
            this.enum_PreferLevelType = enum_PreferLevelType;
        }
        #endregion

        #region METODOS
        #endregion

        #region EVENTOS
        #endregion

        #region DESTRUCTOR
        #endregion


        #region [ CODE ]

        #endregion

    }
}
