using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    public class VO_OTA_AirPriceRQ
    {
        #region [ ATRIBUTOS ]

        private string strCodigoAcuerdo;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_OTA_AirPriceRQ()
        {
        }
        public VO_OTA_AirPriceRQ
            (string strCodigoAcuerdo)
        {
            
            // atributos del ws
            //oOTA_AirPriceRQ.AltLangID;
            //oOTA_AirPriceRQ.EchoToken;
            //oOTA_AirPriceRQ.POS;
            //oOTA_AirPriceRQ.PrimaryLangID;
            //oOTA_AirPriceRQ.SequenceNmbr;
            //oOTA_AirPriceRQ.Target;
            //oOTA_AirPriceRQ.TimeStamp;
            //oOTA_AirPriceRQ.TPA_Extensions;
            //oOTA_AirPriceRQ.TravelerInfoSummary;
            //oOTA_AirPriceRQ.Version;

        }

        #endregion

        #region [ PROPIEADES ]

        public string StrCodigoAcuerdo
        {
            get { return strCodigoAcuerdo; }
            set { strCodigoAcuerdo = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_OTA_AirPriceRQ() { }
        #endregion
    }
}
