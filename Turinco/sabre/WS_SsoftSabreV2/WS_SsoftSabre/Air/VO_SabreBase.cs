using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.ValueObjects;

namespace WS_SsoftSabre.Air
{
    public  class VO_SabreBase
    {
        #region ATTRIBUTES
        //public abstract void setInitialize();

        private VO_SessionCreateRQ vo_SessionCreateRQ;
        private VO_MessageHeader vo_MessageHeader;
        private String strEnviroment;


        #endregion

        #region PROPERTIES
        public VO_SessionCreateRQ Vo_SessionCreateRQ
        {
            get { return vo_SessionCreateRQ; }
            set { vo_SessionCreateRQ = value; }
        }
        public VO_MessageHeader Vo_MessageHeader
        {
            get { return vo_MessageHeader; }
            set { vo_MessageHeader = value; }
        }
        /// <summary>
        /// Rerturns the Enviroment (CERT/PROD) to send 
        /// the SWS request. this attribute can be modify in the
        /// settings.settings file
        /// </summary>
        internal String StrEnviroment
        {
            get { return strEnviroment; }
        }
        #endregion

        #region CONSTRUCTOR
        public VO_SabreBase(String strEnviroment)
        {
            this.strEnviroment = strEnviroment;
        }
        public VO_SabreBase()
        {
            this.strEnviroment = "https://webservices.sabre.com/websvc";
        }
        #endregion

        #region METODOS
        #endregion

        #region EVENTOS
        #endregion

        #region DESTRUCTOR
        ~VO_SabreBase() { }
        #endregion


        internal string ConversationId()
        {
            return "Sabre CO";
        }
    }
}
