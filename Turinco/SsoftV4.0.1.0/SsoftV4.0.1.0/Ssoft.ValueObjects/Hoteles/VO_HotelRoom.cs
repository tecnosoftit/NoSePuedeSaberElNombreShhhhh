using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ssoft.Ssoft.ValueObjects.Hoteles
{
    public class VO_HotelRoom
    {
        #region [ ATRIBUTOS ]
        private string sSHRUI;
        private string sBoardCode;
        private string sRoomTypeCode;
        private string sRoomTypeCharacteristic;
        private String vClass;
        public String Class
        {
            get { return vClass; }
            set { vClass = value; }
        }
        private String vId;

        public String Id
        {
            get { return vId; }
            set { vId = value; }
        }

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_HotelRoom()
        {
        }

        public VO_HotelRoom(
            string sSHRUI,
            string sBoardCode,
            string sRoomTypeCode,
            string sRoomTypeCharacteristic)
        {
            this.sSHRUI = sSHRUI;
            this.sBoardCode = sBoardCode;
            this.sRoomTypeCode = sRoomTypeCode;
            this.sRoomTypeCharacteristic = sRoomTypeCharacteristic;
        }
        #endregion

        #region [ PROPIEADES ]
        public string SHRUI
        {
            get { return sSHRUI; }
            set { sSHRUI = value; }
        }
        public string BoardCode
        {
            get { return sBoardCode; }
            set { sBoardCode = value; }
        }
        public string RoomTypeCode
        {
            get { return sRoomTypeCode; }
            set { sRoomTypeCode = value; }
        }
        public string RoomTypeCharacteristic
        {
            get { return sRoomTypeCharacteristic; }
            set { sRoomTypeCharacteristic = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_HotelRoom() { }
        #endregion
    }
    public class VO_HotelInfo
    {
        #region [ ATRIBUTOS ]
        private string sCode;
        private string sDestination;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_HotelInfo()
        {
        }

        public VO_HotelInfo(
            string sCode,
            string sDestination)
        {
            this.sCode = sCode;
            this.sDestination = sDestination;
        }
        #endregion

        #region [ PROPIEADES ]
        public string Code
        {
            get { return sCode; }
            set { sCode = value; }
        }
        public string Destination
        {
            get { return sDestination; }
            set { sDestination = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_HotelInfo() { }
        #endregion
    }
    public class VO_HotelPlans
    {
        #region [ ATRIBUTOS ]
        private string sCode;
        private string sDestination;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_HotelPlans()
        {
        }

        public VO_HotelPlans(
            string sCode,
            string sDestination)
        {
            this.sCode = sCode;
            this.sDestination = sDestination;
        }
        #endregion

        #region [ PROPIEADES ]
        public string Code
        {
            get { return sCode; }
            set { sCode = value; }
        }
        public string Destination
        {
            get { return sDestination; }
            set { sDestination = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_HotelPlans() { }
        #endregion
    }
}
