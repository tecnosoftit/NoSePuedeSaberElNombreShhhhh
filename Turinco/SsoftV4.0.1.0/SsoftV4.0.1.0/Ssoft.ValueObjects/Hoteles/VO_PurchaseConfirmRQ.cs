using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ssoft.ValueObjects;

namespace Ssoft.Ssoft.ValueObjects.Hoteles
{
    public class VO_PurchaseConfirmRQ
    {
        #region [ ATRIBUTOS ]
        private VO_Credentials vCredentials;
        private VO_Holder vHolder;
        private string spurchaseToken;
        private string sAgencyReference;
        private List<VO_ServiceData> lvServiceData;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_PurchaseConfirmRQ()
        {
        }

        public VO_PurchaseConfirmRQ(
            VO_Credentials vCredentials,
            VO_Holder vHolder,
            string spurchaseToken,
            string sAgencyReference,
            List<VO_ServiceData> lvServiceData)
        {
            this.vCredentials = vCredentials;
            this.vHolder = vHolder;
            this.spurchaseToken = spurchaseToken;
            this.sAgencyReference = sAgencyReference;
            this.lvServiceData = lvServiceData;
        }
        #endregion

        #region [ PROPIEADES ]
        public VO_Credentials Credentials
        {
            get { return vCredentials; }
            set { vCredentials = value; }
        }
        public VO_Holder Holder
        {
            get { return vHolder; }
            set { vHolder = value; }
        }
        public string PurchaseToken
        {
            get { return spurchaseToken; }
            set { spurchaseToken = value; }
        }
        public string AgencyReference
        {
            get { return sAgencyReference; }
            set { sAgencyReference = value; }
        }
        public List<VO_ServiceData> lServiceData
        {
            get { return lvServiceData; }
            set { lvServiceData = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_PurchaseConfirmRQ() { }
        #endregion
    }
    public class VO_ServiceData
    {
        #region [ ATRIBUTOS ]
        private string sSPUI;
        private List<VO_Customer> lvCustomer;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_ServiceData()
        {
        }

        public VO_ServiceData(
            string sSPUI,
            List<VO_Customer> lvCustomer)
        {
            this.sSPUI = sSPUI;
            this.lvCustomer = lvCustomer;
        }
        #endregion

        #region [ PROPIEADES ]
        public string SPUI
        {
            get { return sSPUI; }
            set { sSPUI = value; }
        }
        public List<VO_Customer> lCustomer
        {
            get { return lvCustomer; }
            set { lvCustomer = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_ServiceData() { }
        #endregion
    }
    public class VO_Customer
    {
        #region [ ATRIBUTOS ]
        private string sType;
        private int iCustomerId;
        private int iAge;
        private string sName;
        private string sLastName;
        private string sEmail;
        private string sPhone;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_Customer()
        {
            this.iCustomerId = 1;
            this.iAge = 30;
        }

        public VO_Customer(
            string sType,
            int iCustomerId,
            int iAge,
            string sName,
            string sLastName)
        {
            this.sType = sType;
            this.iCustomerId = iCustomerId;
            this.iAge = iAge;
            this.sName = sName;
            this.sLastName = sLastName;
        }
        #endregion

        #region [ PROPIEADES ]
        public string Type
        {
            get { return sType; }
            set { sType = value; }
        }
        public int CustomerId
        {
            get { return iCustomerId; }
            set { iCustomerId = value; }
        }
        public int Age
        {
            get { return iAge; }
            set { iAge = value; }
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
        public string Email
        {
            get { return sEmail; }
            set { sEmail = value; }
        }
        public string Phone
        {
            get { return sPhone; }
            set { sPhone = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_Customer() { }
        #endregion
    }
    public class VO_Holder
    {
        #region [ ATRIBUTOS ]
        private string sType;
        private string sName;
        private string sLastName;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_Holder()
        {
        }

        public VO_Holder(
            string sType,
            string sName,
            string sLastName)
        {
            this.sType = sType;
            this.sName = sName;
            this.sLastName = sLastName;
        }
        #endregion

        #region [ PROPIEADES ]
        public string Type
        {
            get { return sType; }
            set { sType = value; }
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
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_Holder() { }
        #endregion
    }
}
