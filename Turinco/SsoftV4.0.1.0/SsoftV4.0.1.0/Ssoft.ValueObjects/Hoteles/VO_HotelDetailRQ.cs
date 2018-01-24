//Version 4.0.1.1
using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    public class VO_HotelDetailRQ
    {
        #region [ ATRIBUTOS ]
        private VO_Credentials vCredentials;
        private string sHotelCode;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_HotelDetailRQ()
        {
        }

        public VO_HotelDetailRQ(
            VO_Credentials vCredentials,
            string sHotelCode)
        {
            this.vCredentials = vCredentials;
            this.sHotelCode = sHotelCode;
        }
        #endregion

        #region [ PROPIEADES ]
        public VO_Credentials Credentials
        {
            get { return vCredentials; }
            set { vCredentials = value; }
        }
        public string HotelCode
        {
            get { return sHotelCode; }
            set { sHotelCode = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_HotelDetailRQ() { }
        #endregion    
    }
}
