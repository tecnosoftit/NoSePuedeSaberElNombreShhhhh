using System;
using System.Collections.Generic;
using System.Text;

namespace WS_SsoftHotelBeds.ValueObjects
{
    public class VO_HotelOccupancy
    {
        #region [ ATRIBUTOS ]
        private int iRoomCount;
        private VO_Occupancy vOccupancy;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_HotelOccupancy()
        {
        }

        public VO_HotelOccupancy(
            int iRoomCount,
            VO_Occupancy vOccupancy)
        {
            this.iRoomCount = iRoomCount;
            this.vOccupancy = vOccupancy;
        }
        #endregion

        #region [ PROPIEADES ]
        public int RoomCount
        {
            get { return iRoomCount; }
            set { iRoomCount = value; }
        }
        public VO_Occupancy Occupancy
        {
            get { return vOccupancy; }
            set { vOccupancy = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_HotelOccupancy() { }
        #endregion
    }
    public class VO_Occupancy
    {
        #region [ ATRIBUTOS ]
        private int iAdultCount;
        private int iChildCount;
        private List<VO_GuestList> lvGuestList;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_Occupancy()
        {
        }

        public VO_Occupancy(
            int iAdultCount,
            int iChildCount,
            List<VO_GuestList> lvGuestList)
        {
            this.iAdultCount = iAdultCount;
            this.iChildCount = iChildCount;
            this.lvGuestList = lvGuestList;
        }
        #endregion

        #region [ PROPIEADES ]
        public int AdultCount
        {
            get { return iAdultCount; }
            set { iAdultCount = value; }
        }
        public int ChildCount
        {
            get { return iChildCount; }
            set { iChildCount = value; }
        }
        public List<VO_GuestList> lGuestList
        {
            get { return lvGuestList; }
            set { lvGuestList = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_Occupancy() { }
        #endregion
    }
    public class VO_GuestList
    {
        #region [ ATRIBUTOS ]
        private string sCustomertype;
        private string sCustomerAge;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_GuestList()
        {
        }

        public VO_GuestList(
            string sCustomertype,
            string sCustomerAge)
        {
            this.sCustomertype = sCustomertype;
            this.sCustomerAge = sCustomerAge;
        }
        #endregion

        #region [ PROPIEADES ]
        public string Customertype
        {
            get { return sCustomertype; }
            set { sCustomertype = value; }
        }
        public string CustomerAge
        {
            get { return sCustomerAge; }
            set { sCustomerAge = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_GuestList() { }
        #endregion
    }
}
