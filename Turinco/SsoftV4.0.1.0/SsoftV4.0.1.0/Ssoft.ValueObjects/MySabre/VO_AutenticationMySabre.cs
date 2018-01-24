using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    public class VO_AutenticationMySabre
    {
        #region [ ATRIBUTOS ]
        private VO_TicketMySabre vTicketMySabre;
        private VO_EmployeeProfile vEmployeeProfile;
        private VO_AgencyInfo vAgencyInfo;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_AutenticationMySabre()
        {
        }
        #endregion

        #region [ PROPIEADES ]
        public VO_TicketMySabre TicketMySabre
        {
            get { return vTicketMySabre; }
            set { vTicketMySabre = value; }
        }
        public VO_EmployeeProfile EmployeeProfile
        {
            get { return vEmployeeProfile; }
            set { vEmployeeProfile = value; }
        }
        public VO_AgencyInfo AgencyInfo
        {
            get { return vAgencyInfo; }
            set { vAgencyInfo = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_AutenticationMySabre() { }
        #endregion
    }
    public class VO_TicketMySabre
    {
        #region [ ATRIBUTOS ]
        private string sNumber;
        private bool bValid;
        private string sText;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_TicketMySabre()
        {
        }

        public VO_TicketMySabre(
                string sNumber,
                bool bValid,
                string sText)
        {
            this.sNumber = sNumber;
            this.bValid = bValid;
            this.sText = sText;
        }
        #endregion

        #region [ PROPIEADES ]
        public string Number
        {
            get { return sNumber; }
            set { sNumber = value; }
        }
        public bool Valid
        {
            get { return bValid; }
            set { bValid = value; }
        }
        public string Text
        {
            get { return sText; }
            set { sText = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_TicketMySabre() { }
        #endregion
    }
    public class VO_EmployeeProfile
    {
        #region [ ATRIBUTOS ]
        private string sUserid;
        private string sName;
        private string sLastName;
        private string sDutyCodes;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_EmployeeProfile()
        {
        }

        public VO_EmployeeProfile(
                string sUserid,
                string sName,
                string sLastName,
                string sDutyCodes)
        {
            this.sUserid = sUserid;
            this.sName = sName;
            this.sLastName = sLastName;
            this.sDutyCodes = sDutyCodes;
        }
        #endregion

        #region [ PROPIEADES ]
        public string Userid
        {
            get { return sUserid; }
            set { sUserid = value; }
        }
        public string Name
        {
            get { return sName; }
            set { sName = value; }
        }
        public string LastName
        {
            get { return sLastName; }
            set { sLastName = value; }
        }
        public string DutyCodes
        {
            get { return sDutyCodes; }
            set { sDutyCodes = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_EmployeeProfile() { }
        #endregion
    }
    public class VO_AgencyInfo
    {
        #region [ ATRIBUTOS ]
        private string sPseudoCityCode;
        private string sCountry;
        private string sTelephone;
        private string sArciataNumber;
        private List<string> lPseudoCode;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_AgencyInfo()
        {
        }

        public VO_AgencyInfo(
                string sPseudoCityCode,
                string sCountry,
                string sTelephone,
                string sArciataNumber)
        {
            this.sPseudoCityCode = sPseudoCityCode;
            this.sCountry = sCountry;
            this.sTelephone = sTelephone;
            this.sArciataNumber = sArciataNumber;
        }
        #endregion

        #region [ PROPIEADES ]
        public string PseudoCityCode
        {
            get { return sPseudoCityCode; }
            set { sPseudoCityCode = value; }
        }
        public string Country
        {
            get { return sCountry; }
            set { sCountry = value; }
        }
        public string Telephone
        {
            get { return sTelephone; }
            set { sTelephone = value; }
        }
        public string ArciataNumber
        {
            get { return sArciataNumber; }
            set { sArciataNumber = value; }
        }
        public List<string> PseudoCode
        {
            get { return lPseudoCode; }
            set { lPseudoCode = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_AgencyInfo() { }
        #endregion
    }
}
