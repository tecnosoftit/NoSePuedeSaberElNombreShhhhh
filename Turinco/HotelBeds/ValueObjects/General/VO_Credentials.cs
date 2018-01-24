using System;
using System.Collections.Generic;
using System.Text;

namespace WS_SsoftHotelBeds.ValueObjects
{
    public class VO_Credentials
    {
        #region [ ATRIBUTOS ]
        private string sUser;
        private string sPassword;
        private string sLanguage;
        private string sSessionId;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_Credentials()
        {
        }

        public VO_Credentials(
            string sUser,
            string sPassword,
            string sLanguage,
            string sSessionId)
        {
            this.sUser = sUser;
            this.sPassword = sPassword;
            this.sLanguage = sLanguage;
            this.sSessionId = sSessionId;
        }
        #endregion

        #region [ PROPIEADES ]
        public string User
        {
            get { return sUser; }
            set { sUser = value; }
        }
        public string Password
        {
            get { return sPassword; }
            set { sPassword = value; }
        }
        public string Language
        {
            get { return sLanguage; }
            set { sLanguage = value; }
        }
        public string SessionId
        {
            get { return sSessionId; }
            set { sSessionId = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_Credentials() { }
        #endregion
    }
}
