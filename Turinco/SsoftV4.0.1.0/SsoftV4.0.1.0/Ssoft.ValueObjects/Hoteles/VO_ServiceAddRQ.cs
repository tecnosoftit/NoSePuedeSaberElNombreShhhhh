//Version 4.0.1.1
using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Ssoft.ValueObjects.Hoteles;

namespace Ssoft.ValueObjects
{
    public class VO_ServiceAddRQ
    {
        #region [ ATRIBUTOS ]
        private VO_Credentials vCredentials;
        private string savailToken;
        private string sContractName;
        private string sIncomingOffice;
        private string sDateFrom;
        private string sDateTo;
        private VO_HotelInfo vHotelInfo;
        private List<VO_HotelOccupancy> lvHotelOccupancy;
        private VO_HotelPlans vHotelPlans;
        private VO_HotelRoom vHotelRoom;
        private int iTotalRoom;
        private int iTotalAdult;
        private int iTotalChild;
        private int iTotalInf;
        private int iTotalNights;
        private decimal dValorNeto;
        private decimal dValorImpuestos;
        private decimal dValorTotal;
        private string sDivisa;
        private string sEstado;
        
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_ServiceAddRQ()
        {
        }

        public VO_ServiceAddRQ(
            VO_Credentials vCredentials,
            string savailToken,
            string sContractName,
            string sIncomingOffice,
            string sDateFrom,
            string sDateTo,
            VO_HotelInfo vHotelInfo,
            List<VO_HotelOccupancy> lvHotelOccupancy,
            VO_HotelRoom vHotelRoom)
        {
            this.vCredentials = vCredentials;
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
            get { return vCredentials; }
            set { vCredentials = value; }
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
        public VO_HotelPlans HotelPlans
        {
            get { return vHotelPlans; }
            set { vHotelPlans = value; }
        }
        public int TotalRoom
        {
            get { return iTotalRoom; }
            set { iTotalRoom = value; }
        }
        public int TotalAdult
        {
            get { return iTotalAdult; }
            set { iTotalAdult = value; }
        }
        public int TotalChild
        {
            get { return iTotalChild; }
            set { iTotalChild = value; }
        }
        public int TotalInf
        {
            get { return iTotalInf; }
            set { iTotalInf = value; }
        }
        public int TotalNights
        {
            get { return iTotalNights; }
            set { iTotalNights = value; }
        }
        public decimal ValorNeto
        {
            get { return dValorNeto; }
            set { dValorNeto = value; }
        }
        public decimal ValorImpuestos
        {
            get { return dValorImpuestos; }
            set { dValorImpuestos = value; }
        }
        public decimal ValorTotal
        {
            get { return dValorTotal; }
            set { dValorTotal = value; }
        }
        public string Divisa
        {
            get { return sDivisa; }
            set { sDivisa = value; }
        }
        public string Estado
        {
            get { return sEstado; }
            set { sEstado = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_ServiceAddRQ() { }
        #endregion    }    
    }
}
