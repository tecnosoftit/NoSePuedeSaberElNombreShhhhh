using System;
using System.Collections.Generic;
using System.Text;

namespace WS_SsoftHotelBeds.ValueObjects
{
    public class VO_ServiceAddRQ
    {
        #region [ ATRIBUTOS ]
        private VO_Credentials sCredentials;
        private string savailToken;
        private string sContractName;
        private string sIncomingOffice;
        private string sDateFrom;
        private string sDateTo;
        private VO_HotelInfo vHotelInfo;
        private List<VO_HotelOccupancy> lvHotelOccupancy;
        private VO_HotelRoom vHotelRoom;
        
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_ServiceAddRQ()
        {
        }

        public VO_ServiceAddRQ(
            VO_Credentials sCredentials,
            string savailToken,
            string sContractName,
            string sIncomingOffice,
            string sDateFrom,
            string sDateTo,
            VO_HotelInfo vHotelInfo,
            List<VO_HotelOccupancy> lvHotelOccupancy,
            VO_HotelRoom vHotelRoom)
        {
            this.sCredentials = sCredentials;
            this.savailToken = savailToken;
            this.sContractName = sContractName;
            this.sIncomingOffice = sIncomingOffice;
            this.sDateFrom = sDateFrom;
            this.sDateTo = sDateTo;
            this.vHotelInfo = vHotelInfo;
            this.lvHotelOccupancy = lvHotelOccupancy;
            this.vHotelRoom = vHotelRoom;
        }
        #endregion

        #region [ PROPIEADES ]
        public VO_Credentials Credentials
        {
            get { return sCredentials; }
            set { sCredentials = value; }
        }
        public string AvailToken
        {
            get { return savailToken; }
            set { savailToken = value; }
        }
        public string IncomingOffice
        {
            get { return sIncomingOffice; }
            set { sIncomingOffice = value; }
        }
        public string ContractName
        {
            get { return sContractName; }
            set { sContractName = value; }
        }
        public string DateFrom
        {
            get { return sDateFrom; }
            set { sDateFrom = value; }
        }
        public string DateTo
        {
            get { return sDateTo; }
            set { sDateTo = value; }
        }
        public VO_HotelInfo HotelInfo
        {
            get { return vHotelInfo; }
            set { vHotelInfo = value; }
        }
        public List<VO_HotelOccupancy> lHotelOccupancy
        {
            get { return lvHotelOccupancy; }
            set { lvHotelOccupancy = value; }
        }
        public VO_HotelRoom HotelRoom
        {
            get { return vHotelRoom; }
            set { vHotelRoom = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_ServiceAddRQ() { }
        #endregion    }    
    }
}
