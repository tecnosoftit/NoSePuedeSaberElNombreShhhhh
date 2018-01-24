using System;
using System.Collections.Generic;
using System.Text;

namespace WS_SsoftHotelBeds.ValueObjects
{
    public class VO_HotelDetailRQ
    {
        #region [ ATRIBUTOS ]
        private VO_Credentials sCredentials;
        private string sHotelCode;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_HotelDetailRQ()
        {
        }

        public VO_HotelDetailRQ(
            VO_Credentials sCredentials,
            string sHotelCode)
        {
            this.sCredentials = sCredentials;
            this.sHotelCode = sHotelCode;
        }
        #endregion

        #region [ PROPIEADES ]
        public VO_Credentials Credentials
        {
            get { return sCredentials; }
            set { sCredentials = value; }
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
