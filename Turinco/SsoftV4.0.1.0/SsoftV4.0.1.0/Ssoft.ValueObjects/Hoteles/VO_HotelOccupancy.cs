using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ssoft.Ssoft.ValueObjects.Hoteles
{
    public class VO_HotelOccupancy
    {
        #region [ ATRIBUTOS ]
        private int iRoomCount;
        private VO_Occupancy vOccupancy;
        private VO_HotelRoom vHotelRoom;
        private String vCodePlan;
        private List<String> lCodePlanAlimenta;

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
        public VO_HotelRoom HotelRoom
        {
            get { return vHotelRoom; }
            set { vHotelRoom = value; }
        }
        public String CodePlan
        {
            get { return vCodePlan; }
            set { vCodePlan = value; }
        }
        public List<String> CodePlanAlimenta
        {
            get { return lCodePlanAlimenta; }
            set { lCodePlanAlimenta = value; }
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
        private int iBabyCount;
        private int iJuniorCount;

        private List<VO_Customer> lvGuestList;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_Occupancy()
        {
        }

        public VO_Occupancy(
            int iAdultCount,
            int iChildCount,
            int iBabyCount,
            List<VO_Customer> lvGuestList)
        {
            this.iAdultCount = iAdultCount;
            this.iChildCount = iChildCount;
            this.iBabyCount = iBabyCount;
            this.lvGuestList = lvGuestList;
        }
        #endregion

        #region [ PROPIEADES ]
        public int JuniorCount
        {
            get { return iJuniorCount; }
            set { iJuniorCount = value; }
        }
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
        public int BabyCount
        {
            get { return iBabyCount; }
            set { iBabyCount = value; }
        }
        public List<VO_Customer> lGuestList
        {
            get { return lvGuestList; }
            set { lvGuestList = value; }
        }
        #endregion
        #region [ DESTRUCTOR ]
        ~VO_Occupancy() { }
        #endregion
    }
}
