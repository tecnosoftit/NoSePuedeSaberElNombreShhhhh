using System;
using System.Collections.Generic;
using System.Text;

namespace WS_SsoftSabre.Air
{
    public class VO_MessageHeader
    {

        #region ATTRIBUTES
        private string strConversationId;
        private string strFrom;
        private string strTo;
        private string strCPAId;
        private string strMessageId;
        private string strValue;
        private string strAction;
        #endregion

        #region PROPERTIES
        public string StrConversationId
        {
            get { return strConversationId; }
            set { strConversationId = value; }
        }
        public string StrFrom
        {
            get { return strFrom; }
            set { strFrom = value; }
        }
        public string StrTo
        {
            get { return strTo; }
            set { strTo = value; }
        }
        public string StrCPAId
        {
            get { return strCPAId; }
            set { strCPAId = value; }
        }
        public string StrMessageId
        {
            get { return strMessageId; }
            set { strMessageId = value; }
        }
        public string StrValue
        {
            get { return strValue; }
            set { strValue = value; }
        }
        public string StrAction
        {
            get { return strAction; }
            set { strAction = value; }
        }
        #endregion

        #region CONSTRUCTOR
        public VO_MessageHeader()
        {
            strConversationId = "Sabre CO";
            strFrom = "99999";
            strTo = "123123";
            strMessageId = "mid:20001209-133003-2333@clientofsabre.com1";
        }
        #endregion

        #region METODOS
        #endregion

        #region EVENTOS
        #endregion

    }
}
