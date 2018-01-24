using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Rules.WS;

namespace WS_SsoftHotelBeds.ValueObjects
{
    public class VO_HotelValuedAvailRQ
    {
        #region [ ATRIBUTOS ]
        private VO_Credentials sCredentials;
        private string sPaginationData;
        private string sCheckInDate;
        private string sCheckOutDate;
        private string sDestination;
        private List<VO_HotelOccupancy> vlHotelOccupancy;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_HotelValuedAvailRQ()
        {
        }

        public VO_HotelValuedAvailRQ(
            VO_Credentials sCredentials,
            string sPaginationData,
            string sCheckInDate,
            string sCheckOutDate,
            string sDestination,
            List<VO_HotelOccupancy> vlHotelOccupancy)
        {
            this.sCredentials = sCredentials;
            this.sPaginationData = sPaginationData;
            this.sCheckInDate = sCheckInDate;
            this.sCheckOutDate = sCheckOutDate;
            this.sDestination = sDestination;
            this.vlHotelOccupancy = vlHotelOccupancy;
        }
        #endregion

        #region [ PROPIEADES ]
        public VO_Credentials Credentials
        {
            get { return sCredentials; }
            set { sCredentials = value; }
        }
        public string PaginationData
        {
            get { return sPaginationData; }
            set { sPaginationData = value; }
        }
        public string CheckInDate
        {
            get { return sCheckInDate; }
            set { sCheckInDate = value; }
        }
        public string CheckOutDate
        {
            get { return sCheckOutDate; }
            set { sCheckOutDate = value; }
        }
        public string Destination
        {
            get { return sDestination; }
            set { sDestination = value; }
        }
        public List<VO_HotelOccupancy> lHotelOccupancy
        {
            get { return vlHotelOccupancy; }
            set { vlHotelOccupancy = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_HotelValuedAvailRQ() { }
        #endregion
    }
}
