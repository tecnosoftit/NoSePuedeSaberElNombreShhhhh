using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Ssoft.ValueObjects;
using OTA_AirLowFareSearchRQ = WS_SsoftSabre.OTA_AirLowFareSearch;
using OTA_AirPriceRQ = WS_SsoftSabre.OTA_AirPrice;
using Ssoft.Utils;

namespace WS_SsoftSabre.Air
{
    class clsSabreBase
    {
        public const string OTA_AIR_RULES_VERSION = "2003A.TsabreXML1.4.1";
        public const string COMMANDLLS_VERSION = "2003A.TsabreXML1.6.1";
        public const string OTA_VEH_AVIAL_RATE = "2003A.TsabreXML1.9.1";
        public const string OTA_VEHLOCDETAIL = "2003A.TsabreXML1.4.1";
        public const string OTA_VEHRES = "2003A.TsabreXML1.9.1";
        public const string OTA_VEH_LOCATION_LIST = "2003A.TsabreXML1.2.1";
        public const string OTA_HOTEL_AVIAL = "2003A.TsabreXML1.8.1";
        public const string HOTEL_PROPERTY_DESCRIPTION = "2003A.TsabreXML1.10.1";
        public const string HOTEL_RATE_DESCRIPTION = "2003A.TsabreXML1.6.1";
        public const string OTA_HOTEL_RES = "2003A.TsabreXML1.3.1";


        public const string SABRE_VERSION_ADDREMARK = "2003A.TsabreXML1.0.1";
        public const string SABRE_VERSION_SABRECOMMANDLLS = "2003A.TsabreXML1.6.1";
        public const string SABRE_VERSION_OTA_AIRLOWFARESEARCH = "2003A.TsabreXML1.9.1";
        public const string SABRE_VERSION_OTA_AIRPRICE = "2003A.TsabreXML1.13.1";
        public const string SABRE_VERSION_OTA_AIRBOOK = "2003A.TsabreXML1.4.1";
        public const string SABRE_VERSION_OTA_AIRAVAIL = "2003A.TsabreXML1.9.1";
        public const string SABRE_VERSION_ENDTRANSACTION = "2003A.TsabreXML1.4.1";
        public const string SABRE_VERSION_TRAVELITINERARYADDINFO = "2003A.TsabreXML1.3.1";
        public const string RULES_FROM_PRICE_VERSION = "2003A.TsabreXML1.0.1";
        public const string SABRE_VERSION_TRAVELITINERARYREADLLS = "2003A.TsabreXML1.9.1";
        public const string SABRE_VERSION_CANCEL = "2003A.TsabreXML1.0.1";
        public const string SABRE_VERSION_SHORTSELL = "2003A.TsabreXML1.0.1";
        public const string SABRE_VERSION_DISPLAYPRICE = "2003A.TsabreXML1.5.1";

        public const string SABRE_VERSION_DESIGNATEPRINTER = "2003A.TsabreXML1.1.1";
        public const string SABRE_VERSION_AIRTICKET = "2003A.TsabreXML1.8.1";

        public static OTA_AirRules.MessageHeader OTA_AirRules()
        {
            Ssoft.ValueObjects.VO_SabreBase vo_SabreBase = new Ssoft.ValueObjects.VO_SabreBase();
            WS_SsoftSabre.OTA_AirRules.MessageHeader Mensaje_ = new WS_SsoftSabre.OTA_AirRules.MessageHeader();

            Mensaje_.ConversationId = vo_SabreBase.ConversationId();

            DateTime Fecha_ = DateTime.UtcNow;
            string Tiempo_ = Fecha_.ToString("s") + "Z";

            WS_SsoftSabre.OTA_AirRules.From Desde_ = new WS_SsoftSabre.OTA_AirRules.From();
            WS_SsoftSabre.OTA_AirRules.PartyId DesdePartyID_ = new WS_SsoftSabre.OTA_AirRules.PartyId();
            WS_SsoftSabre.OTA_AirRules.PartyId[] DesdePartyIdArray_ = new WS_SsoftSabre.OTA_AirRules.PartyId[1];

            DesdePartyID_.Value = vo_SabreBase.From();
            DesdePartyIdArray_[0] = DesdePartyID_;
            Desde_.PartyId = DesdePartyIdArray_;
            Mensaje_.From = Desde_;

            WS_SsoftSabre.OTA_AirRules.To A_ = new WS_SsoftSabre.OTA_AirRules.To();
            WS_SsoftSabre.OTA_AirRules.PartyId APartyID_ = new WS_SsoftSabre.OTA_AirRules.PartyId();
            WS_SsoftSabre.OTA_AirRules.PartyId[] APartyIdArray_ = new WS_SsoftSabre.OTA_AirRules.PartyId[1];

            APartyID_.Value = vo_SabreBase.To();
            APartyIdArray_[0] = APartyID_;
            A_.PartyId = APartyIdArray_;
            Mensaje_.To = A_;

            //-------------------------------------------------------------------------------------

            WS_SsoftSabre.OTA_AirRules.Service Servicio_ = new WS_SsoftSabre.OTA_AirRules.Service();
            WS_SsoftSabre.OTA_AirRules.MessageData MensajeDatos_ = new WS_SsoftSabre.OTA_AirRules.MessageData();

            Mensaje_.CPAId = vo_SabreBase.CPAId();
            Mensaje_.Action = "OTA_AirRulesLLSRQ";

            Servicio_.Value = "OTA_AirRulesLLSRQ";
            Mensaje_.Service = Servicio_;

            MensajeDatos_.MessageId = vo_SabreBase.MessageId();
            MensajeDatos_.Timestamp = Tiempo_;
            Mensaje_.MessageData = MensajeDatos_;

            return Mensaje_;
        }

        public static WS_SsoftSabre.SabreCommandLLS.MessageHeader SabreCommandLLS()
        {
            Ssoft.ValueObjects.VO_SabreBase vo_SabreBase = new Ssoft.ValueObjects.VO_SabreBase();
            WS_SsoftSabre.SabreCommandLLS.MessageHeader Mensaje_ = new WS_SsoftSabre.SabreCommandLLS.MessageHeader();

            Mensaje_.ConversationId = vo_SabreBase.ConversationId();

            DateTime Fecha_ = DateTime.UtcNow;
            string Tiempo_ = Fecha_.ToString("s") + "Z";

            WS_SsoftSabre.SabreCommandLLS.From Desde_ = new WS_SsoftSabre.SabreCommandLLS.From();
            WS_SsoftSabre.SabreCommandLLS.PartyId DesdePartyID_ = new WS_SsoftSabre.SabreCommandLLS.PartyId();
            WS_SsoftSabre.SabreCommandLLS.PartyId[] DesdePartyIdArray_ = new WS_SsoftSabre.SabreCommandLLS.PartyId[1];

            DesdePartyID_.Value = vo_SabreBase.From();
            DesdePartyIdArray_[0] = DesdePartyID_;
            Desde_.PartyId = DesdePartyIdArray_;
            Mensaje_.From = Desde_;

            WS_SsoftSabre.SabreCommandLLS.To A_ = new WS_SsoftSabre.SabreCommandLLS.To();
            WS_SsoftSabre.SabreCommandLLS.PartyId APartyID_ = new WS_SsoftSabre.SabreCommandLLS.PartyId();
            WS_SsoftSabre.SabreCommandLLS.PartyId[] APartyIdArray_ = new WS_SsoftSabre.SabreCommandLLS.PartyId[1];

            APartyID_.Value = vo_SabreBase.To();
            APartyIdArray_[0] = APartyID_;
            A_.PartyId = APartyIdArray_;
            Mensaje_.To = A_;

            //-------------------------------------------------------------------------------------

            WS_SsoftSabre.SabreCommandLLS.Service Servicio_ = new WS_SsoftSabre.SabreCommandLLS.Service();
            WS_SsoftSabre.SabreCommandLLS.MessageData MensajeDatos_ = new WS_SsoftSabre.SabreCommandLLS.MessageData();

            Mensaje_.CPAId = vo_SabreBase.CPAId();
            Mensaje_.Action = "SabreCommandLLSRQ";

            Servicio_.Value = "SabreCommand";
            Mensaje_.Service = Servicio_;

            MensajeDatos_.MessageId = vo_SabreBase.MessageId();
            MensajeDatos_.Timestamp = Tiempo_;
            Mensaje_.MessageData = MensajeDatos_;

            return Mensaje_;
        }

       

        public static SessionCreateRQ.MessageHeader SessionCreateRQ()
        {
            WS_SsoftSabre.SessionCreateRQ.MessageHeader Mensaje_ = new WS_SsoftSabre.SessionCreateRQ.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                WS_SsoftSabre.SessionCreateRQ.From Desde_ = new WS_SsoftSabre.SessionCreateRQ.From();
                WS_SsoftSabre.SessionCreateRQ.PartyId DesdePartyID_ = new WS_SsoftSabre.SessionCreateRQ.PartyId();
                WS_SsoftSabre.SessionCreateRQ.PartyId[] DesdePartyIdArray_ = new WS_SsoftSabre.SessionCreateRQ.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                WS_SsoftSabre.SessionCreateRQ.To A_ = new WS_SsoftSabre.SessionCreateRQ.To();
                WS_SsoftSabre.SessionCreateRQ.PartyId APartyID_ = new WS_SsoftSabre.SessionCreateRQ.PartyId();
                WS_SsoftSabre.SessionCreateRQ.PartyId[] APartyIdArray_ = new WS_SsoftSabre.SessionCreateRQ.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                WS_SsoftSabre.SessionCreateRQ.Service Servicio_ = new WS_SsoftSabre.SessionCreateRQ.Service();
                WS_SsoftSabre.SessionCreateRQ.MessageData MensajeDatos_ = new WS_SsoftSabre.SessionCreateRQ.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "SessionCreateRQ";

                Servicio_.Value = "SessionCreate";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Mensaje_;
        }

        public static SessionClose.MessageHeader SessionClose()
        {
            WS_SsoftSabre.SessionClose.MessageHeader Mensaje_ = new WS_SsoftSabre.SessionClose.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                WS_SsoftSabre.SessionClose.From Desde_ = new WS_SsoftSabre.SessionClose.From();
                WS_SsoftSabre.SessionClose.PartyId DesdePartyID_ = new WS_SsoftSabre.SessionClose.PartyId();
                WS_SsoftSabre.SessionClose.PartyId[] DesdePartyIdArray_ = new WS_SsoftSabre.SessionClose.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                WS_SsoftSabre.SessionClose.To A_ = new WS_SsoftSabre.SessionClose.To();
                WS_SsoftSabre.SessionClose.PartyId APartyID_ = new WS_SsoftSabre.SessionClose.PartyId();
                WS_SsoftSabre.SessionClose.PartyId[] APartyIdArray_ = new WS_SsoftSabre.SessionClose.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                WS_SsoftSabre.SessionClose.Service Servicio_ = new WS_SsoftSabre.SessionClose.Service();
                WS_SsoftSabre.SessionClose.MessageData MensajeDatos_ = new WS_SsoftSabre.SessionClose.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "SessionCloseRQ";

                Servicio_.Value = "SessionClose";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Mensaje_;
        }

        public static OTA_AirAvail.MessageHeader __ISabre_OTA_AirAvailLLSRQ()
        {
            OTA_AirAvail.MessageHeader Mensaje_ = new OTA_AirAvail.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;//"123456@webservices.sabre.com"

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                OTA_AirAvail.From Desde_ = new OTA_AirAvail.From();
                OTA_AirAvail.PartyId DesdePartyID_ = new OTA_AirAvail.PartyId();
                OTA_AirAvail.PartyId[] DesdePartyIdArray_ = new OTA_AirAvail.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                OTA_AirAvail.To A_ = new OTA_AirAvail.To();
                OTA_AirAvail.PartyId APartyID_ = new OTA_AirAvail.PartyId();
                OTA_AirAvail.PartyId[] APartyIdArray_ = new OTA_AirAvail.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                OTA_AirAvail.Service Servicio_ = new OTA_AirAvail.Service();
                OTA_AirAvail.MessageData MensajeDatos_ = new OTA_AirAvail.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "OTA_AirAvailLLSRQ";

                Servicio_.Value = "Low Fare Search";
                Servicio_.type = "sabreXML";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;//"cf5d80f2-575e-4969-b30e-84a1f0a18cb1@34"
                //MensajeDatos_.RefToMessageId = "mid:20001209-133003-2333@clientofsabre.com";
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Mensaje_;
        }

        public static OTA_AirLowFareSearch.MessageHeader __ISabre_OTA_AirLowFareSearchLLSRQ()
        {
            OTA_AirLowFareSearchRQ.MessageHeader Mensaje_ = new OTA_AirLowFareSearchRQ.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                OTA_AirLowFareSearchRQ.From Desde_ = new OTA_AirLowFareSearchRQ.From();
                OTA_AirLowFareSearchRQ.PartyId DesdePartyID_ = new OTA_AirLowFareSearchRQ.PartyId();
                OTA_AirLowFareSearchRQ.PartyId[] DesdePartyIdArray_ = new OTA_AirLowFareSearchRQ.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                OTA_AirLowFareSearchRQ.To A_ = new OTA_AirLowFareSearchRQ.To();
                OTA_AirLowFareSearchRQ.PartyId APartyID_ = new OTA_AirLowFareSearchRQ.PartyId();
                OTA_AirLowFareSearchRQ.PartyId[] APartyIdArray_ = new OTA_AirLowFareSearchRQ.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                OTA_AirLowFareSearchRQ.Service Servicio_ = new OTA_AirLowFareSearchRQ.Service();
                OTA_AirLowFareSearchRQ.MessageData MensajeDatos_ = new OTA_AirLowFareSearchRQ.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "OTA_AirLowFareSearchLLSRQ";

                Servicio_.Value = "OTA_AirLowFareSearchRQ.";
                Servicio_.type = "sabreXML";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Mensaje_;
        }

        public static OTA_AirPriceRQ.MessageHeader __ISabre_OTA_AirPrice()
        {
            OTA_AirPriceRQ.MessageHeader Mensaje_ = new OTA_AirPriceRQ.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                OTA_AirPriceRQ.From Desde_ = new OTA_AirPriceRQ.From();
                OTA_AirPriceRQ.PartyId DesdePartyID_ = new OTA_AirPriceRQ.PartyId();
                OTA_AirPriceRQ.PartyId[] DesdePartyIdArray_ = new OTA_AirPriceRQ.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                OTA_AirPriceRQ.To A_ = new OTA_AirPriceRQ.To();
                OTA_AirPriceRQ.PartyId APartyID_ = new OTA_AirPriceRQ.PartyId();
                OTA_AirPriceRQ.PartyId[] APartyIdArray_ = new OTA_AirPriceRQ.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                OTA_AirPriceRQ.Service Servicio_ = new OTA_AirPriceRQ.Service();
                OTA_AirPriceRQ.MessageData MensajeDatos_ = new OTA_AirPriceRQ.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "OTA_AirPriceLLSRQ";

                Servicio_.Value = "Air Price";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Mensaje_;
        }

        public static EndTransactionRQ.MessageHeader __ISabre_EndTransactionLLSUpdated()
        {
            EndTransactionRQ.MessageHeader Mensaje_ = new EndTransactionRQ.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                EndTransactionRQ.From Desde_ = new EndTransactionRQ.From();
                EndTransactionRQ.PartyId DesdePartyID_ = new EndTransactionRQ.PartyId();
                EndTransactionRQ.PartyId[] DesdePartyIdArray_ = new EndTransactionRQ.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                EndTransactionRQ.To A_ = new EndTransactionRQ.To();
                EndTransactionRQ.PartyId APartyID_ = new EndTransactionRQ.PartyId();
                EndTransactionRQ.PartyId[] APartyIdArray_ = new EndTransactionRQ.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                EndTransactionRQ.Service Servicio_ = new EndTransactionRQ.Service();
                EndTransactionRQ.MessageData MensajeDatos_ = new EndTransactionRQ.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "EndTransactionLLSRQ";

                Servicio_.Value = "End Transaction";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Mensaje_;
        }

        public static WebService_TravelItineraryAddInfoLLS.MessageHeader __ISabre_TravelItineraryAddInfoLLS()
        {
            WebService_TravelItineraryAddInfoLLS.MessageHeader Mensaje_ = new WebService_TravelItineraryAddInfoLLS.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                WebService_TravelItineraryAddInfoLLS.From Desde_ = new WebService_TravelItineraryAddInfoLLS.From();
                WebService_TravelItineraryAddInfoLLS.PartyId DesdePartyID_ = new WebService_TravelItineraryAddInfoLLS.PartyId();
                WebService_TravelItineraryAddInfoLLS.PartyId[] DesdePartyIdArray_ = new WebService_TravelItineraryAddInfoLLS.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                WebService_TravelItineraryAddInfoLLS.To A_ = new WebService_TravelItineraryAddInfoLLS.To();
                WebService_TravelItineraryAddInfoLLS.PartyId APartyID_ = new WebService_TravelItineraryAddInfoLLS.PartyId();
                WebService_TravelItineraryAddInfoLLS.PartyId[] APartyIdArray_ = new WebService_TravelItineraryAddInfoLLS.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                WebService_TravelItineraryAddInfoLLS.Service Servicio_ = new WebService_TravelItineraryAddInfoLLS.Service();
                WebService_TravelItineraryAddInfoLLS.MessageData MensajeDatos_ = new WebService_TravelItineraryAddInfoLLS.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "TravelItineraryAddInfoLLSRQ";

                Servicio_.Value = "TravelItineraryAddInfo";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Mensaje_;
        }

        
        public static SessionValidate.MessageHeader SessionValidate()
        {
            WS_SsoftSabre.SessionValidate.MessageHeader Mensaje_ = new WS_SsoftSabre.SessionValidate.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                WS_SsoftSabre.SessionValidate.From Desde_ = new WS_SsoftSabre.SessionValidate.From();
                WS_SsoftSabre.SessionValidate.PartyId DesdePartyID_ = new WS_SsoftSabre.SessionValidate.PartyId();
                WS_SsoftSabre.SessionValidate.PartyId[] DesdePartyIdArray_ = new WS_SsoftSabre.SessionValidate.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                WS_SsoftSabre.SessionValidate.To A_ = new WS_SsoftSabre.SessionValidate.To();
                WS_SsoftSabre.SessionValidate.PartyId APartyID_ = new WS_SsoftSabre.SessionValidate.PartyId();
                WS_SsoftSabre.SessionValidate.PartyId[] APartyIdArray_ = new WS_SsoftSabre.SessionValidate.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                WS_SsoftSabre.SessionValidate.Service Servicio_ = new WS_SsoftSabre.SessionValidate.Service();
                WS_SsoftSabre.SessionValidate.MessageData MensajeDatos_ = new WS_SsoftSabre.SessionValidate.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "SessionValidateRQ";

                Servicio_.Value = "Session";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Mensaje_;
        }

     

   

        public static RulesFromPrice.MessageHeader RulesFromPrice()
        {
            WS_SsoftSabre.RulesFromPrice.MessageHeader Mensaje_ = new WS_SsoftSabre.RulesFromPrice.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                WS_SsoftSabre.RulesFromPrice.From Desde_ = new WS_SsoftSabre.RulesFromPrice.From();
                WS_SsoftSabre.RulesFromPrice.PartyId DesdePartyID_ = new WS_SsoftSabre.RulesFromPrice.PartyId();
                WS_SsoftSabre.RulesFromPrice.PartyId[] DesdePartyIdArray_ = new WS_SsoftSabre.RulesFromPrice.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                WS_SsoftSabre.RulesFromPrice.To A_ = new WS_SsoftSabre.RulesFromPrice.To();
                WS_SsoftSabre.RulesFromPrice.PartyId APartyID_ = new WS_SsoftSabre.RulesFromPrice.PartyId();
                WS_SsoftSabre.RulesFromPrice.PartyId[] APartyIdArray_ = new WS_SsoftSabre.RulesFromPrice.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                WS_SsoftSabre.RulesFromPrice.Service Servicio_ = new WS_SsoftSabre.RulesFromPrice.Service();
                WS_SsoftSabre.RulesFromPrice.MessageData MensajeDatos_ = new WS_SsoftSabre.RulesFromPrice.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "RulesFromPriceLLSRQ";

                Servicio_.Value = "RulesFromPrice";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Mensaje_;
        }
        public static OTA_TravelItineraryRead.MessageHeader OTA_TravelItineraryRead()
        {
            OTA_TravelItineraryRead.MessageHeader Mensaje_ = new OTA_TravelItineraryRead.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                OTA_TravelItineraryRead.From Desde_ = new OTA_TravelItineraryRead.From();
                OTA_TravelItineraryRead.PartyId DesdePartyID_ = new OTA_TravelItineraryRead.PartyId();
                OTA_TravelItineraryRead.PartyId[] DesdePartyIdArray_ = new OTA_TravelItineraryRead.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                OTA_TravelItineraryRead.To A_ = new OTA_TravelItineraryRead.To();
                OTA_TravelItineraryRead.PartyId APartyID_ = new OTA_TravelItineraryRead.PartyId();
                OTA_TravelItineraryRead.PartyId[] APartyIdArray_ = new OTA_TravelItineraryRead.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                OTA_TravelItineraryRead.Service Servicio_ = new OTA_TravelItineraryRead.Service();
                OTA_TravelItineraryRead.MessageData MensajeDatos_ = new OTA_TravelItineraryRead.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "OTA_TravelItineraryReadLLSRQ";

                Servicio_.Value = "TravelItineraryRead";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Mensaje_;
        }
        public static OTA_Cancel.MessageHeader OTA_Cancel()
        {
            OTA_Cancel.MessageHeader Mensaje_ = new OTA_Cancel.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                OTA_Cancel.From Desde_ = new OTA_Cancel.From();
                OTA_Cancel.PartyId DesdePartyID_ = new OTA_Cancel.PartyId();
                OTA_Cancel.PartyId[] DesdePartyIdArray_ = new OTA_Cancel.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                OTA_Cancel.To A_ = new OTA_Cancel.To();
                OTA_Cancel.PartyId APartyID_ = new OTA_Cancel.PartyId();
                OTA_Cancel.PartyId[] APartyIdArray_ = new OTA_Cancel.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                OTA_Cancel.Service Servicio_ = new OTA_Cancel.Service();
                OTA_Cancel.MessageData MensajeDatos_ = new OTA_Cancel.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "OTA_CancelLLSRQ";

                Servicio_.Value = "OTA_Cancel";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Mensaje_;
        }
        public static WebService_AddRemarkLLS.MessageHeader __ISabre_AddRemarkLLSRQ()
        {
            WebService_AddRemarkLLS.MessageHeader Mensaje_ = new WebService_AddRemarkLLS.MessageHeader();

            try
            {
                VO_Credentials objvo_Credentials = clsSesiones.getCredentials();
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                WebService_AddRemarkLLS.From Desde_ = new WebService_AddRemarkLLS.From();
                WebService_AddRemarkLLS.PartyId DesdePartyID_ = new WebService_AddRemarkLLS.PartyId();
                WebService_AddRemarkLLS.PartyId[] DesdePartyIdArray_ = new WebService_AddRemarkLLS.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                WebService_AddRemarkLLS.To A_ = new WebService_AddRemarkLLS.To();
                WebService_AddRemarkLLS.PartyId APartyID_ = new WebService_AddRemarkLLS.PartyId();
                WebService_AddRemarkLLS.PartyId[] APartyIdArray_ = new WebService_AddRemarkLLS.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                WebService_AddRemarkLLS.Service Servicio_ = new WebService_AddRemarkLLS.Service();
                WebService_AddRemarkLLS.MessageData MensajeDatos_ = new WebService_AddRemarkLLS.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "AddRemarkLLSRQ";

                Servicio_.Value = "AddRemark";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Mensaje_;
        }
        public static OTA_AirBook.MessageHeader OTA_AirBook()
        {
            WS_SsoftSabre.OTA_AirBook.MessageHeader Mensaje_ = new WS_SsoftSabre.OTA_AirBook.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                WS_SsoftSabre.OTA_AirBook.From Desde_ = new WS_SsoftSabre.OTA_AirBook.From();
                WS_SsoftSabre.OTA_AirBook.PartyId DesdePartyID_ = new WS_SsoftSabre.OTA_AirBook.PartyId();
                WS_SsoftSabre.OTA_AirBook.PartyId[] DesdePartyIdArray_ = new WS_SsoftSabre.OTA_AirBook.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                WS_SsoftSabre.OTA_AirBook.To A_ = new WS_SsoftSabre.OTA_AirBook.To();
                WS_SsoftSabre.OTA_AirBook.PartyId APartyID_ = new WS_SsoftSabre.OTA_AirBook.PartyId();
                WS_SsoftSabre.OTA_AirBook.PartyId[] APartyIdArray_ = new WS_SsoftSabre.OTA_AirBook.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                WS_SsoftSabre.OTA_AirBook.Service Servicio_ = new WS_SsoftSabre.OTA_AirBook.Service();
                WS_SsoftSabre.OTA_AirBook.MessageData MensajeDatos_ = new WS_SsoftSabre.OTA_AirBook.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "OTA_AirBookLLSRQ";

                Servicio_.Value = "OTA_AirBook";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Mensaje_;
        }
        public static OTA_AirAvail.MessageHeader OTA_AirAvail()
        {
            WS_SsoftSabre.OTA_AirAvail.MessageHeader Mensaje_ = new WS_SsoftSabre.OTA_AirAvail.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                WS_SsoftSabre.OTA_AirAvail.From Desde_ = new WS_SsoftSabre.OTA_AirAvail.From();
                WS_SsoftSabre.OTA_AirAvail.PartyId DesdePartyID_ = new WS_SsoftSabre.OTA_AirAvail.PartyId();
                WS_SsoftSabre.OTA_AirAvail.PartyId[] DesdePartyIdArray_ = new WS_SsoftSabre.OTA_AirAvail.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                WS_SsoftSabre.OTA_AirAvail.To A_ = new WS_SsoftSabre.OTA_AirAvail.To();
                WS_SsoftSabre.OTA_AirAvail.PartyId APartyID_ = new WS_SsoftSabre.OTA_AirAvail.PartyId();
                WS_SsoftSabre.OTA_AirAvail.PartyId[] APartyIdArray_ = new WS_SsoftSabre.OTA_AirAvail.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                WS_SsoftSabre.OTA_AirAvail.Service Servicio_ = new WS_SsoftSabre.OTA_AirAvail.Service();
                WS_SsoftSabre.OTA_AirAvail.MessageData MensajeDatos_ = new WS_SsoftSabre.OTA_AirAvail.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "OTA_AirAvailLLSRQ";

                Servicio_.Value = "OTA_AirAvail";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Mensaje_;
        }
        public static DisplayPriceQuote.MessageHeader DisplayPriceQuote()
        {
            WS_SsoftSabre.DisplayPriceQuote.MessageHeader Mensaje_ = new WS_SsoftSabre.DisplayPriceQuote.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                WS_SsoftSabre.DisplayPriceQuote.From Desde_ = new WS_SsoftSabre.DisplayPriceQuote.From();
                WS_SsoftSabre.DisplayPriceQuote.PartyId DesdePartyID_ = new WS_SsoftSabre.DisplayPriceQuote.PartyId();
                WS_SsoftSabre.DisplayPriceQuote.PartyId[] DesdePartyIdArray_ = new WS_SsoftSabre.DisplayPriceQuote.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                WS_SsoftSabre.DisplayPriceQuote.To A_ = new WS_SsoftSabre.DisplayPriceQuote.To();
                WS_SsoftSabre.DisplayPriceQuote.PartyId APartyID_ = new WS_SsoftSabre.DisplayPriceQuote.PartyId();
                WS_SsoftSabre.DisplayPriceQuote.PartyId[] APartyIdArray_ = new WS_SsoftSabre.DisplayPriceQuote.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                WS_SsoftSabre.DisplayPriceQuote.Service Servicio_ = new WS_SsoftSabre.DisplayPriceQuote.Service();
                WS_SsoftSabre.DisplayPriceQuote.MessageData MensajeDatos_ = new WS_SsoftSabre.DisplayPriceQuote.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "DisplayPriceQuoteLLSRQ";

                Servicio_.Value = "DisplayPriceQuote";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Mensaje_;
        }
        public static ShortSell.MessageHeader ShortSell()
        {
            WS_SsoftSabre.ShortSell.MessageHeader Mensaje_ = new WS_SsoftSabre.ShortSell.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                WS_SsoftSabre.ShortSell.From Desde_ = new WS_SsoftSabre.ShortSell.From();
                WS_SsoftSabre.ShortSell.PartyId DesdePartyID_ = new WS_SsoftSabre.ShortSell.PartyId();
                WS_SsoftSabre.ShortSell.PartyId[] DesdePartyIdArray_ = new WS_SsoftSabre.ShortSell.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                WS_SsoftSabre.ShortSell.To A_ = new WS_SsoftSabre.ShortSell.To();
                WS_SsoftSabre.ShortSell.PartyId APartyID_ = new WS_SsoftSabre.ShortSell.PartyId();
                WS_SsoftSabre.ShortSell.PartyId[] APartyIdArray_ = new WS_SsoftSabre.ShortSell.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                WS_SsoftSabre.ShortSell.Service Servicio_ = new WS_SsoftSabre.ShortSell.Service();
                WS_SsoftSabre.ShortSell.MessageData MensajeDatos_ = new WS_SsoftSabre.ShortSell.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "ShortSellRQ";

                Servicio_.Value = "ShortSell";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Mensaje_;
        }
        public static DesignatePrinterRQ.MessageHeader DesignatePrinter()
        {
            WS_SsoftSabre.DesignatePrinterRQ.MessageHeader Mensaje_ = new WS_SsoftSabre.DesignatePrinterRQ.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                WS_SsoftSabre.DesignatePrinterRQ.From Desde_ = new WS_SsoftSabre.DesignatePrinterRQ.From();
                WS_SsoftSabre.DesignatePrinterRQ.PartyId DesdePartyID_ = new WS_SsoftSabre.DesignatePrinterRQ.PartyId();
                WS_SsoftSabre.DesignatePrinterRQ.PartyId[] DesdePartyIdArray_ = new WS_SsoftSabre.DesignatePrinterRQ.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                WS_SsoftSabre.DesignatePrinterRQ.To A_ = new WS_SsoftSabre.DesignatePrinterRQ.To();
                WS_SsoftSabre.DesignatePrinterRQ.PartyId APartyID_ = new WS_SsoftSabre.DesignatePrinterRQ.PartyId();
                WS_SsoftSabre.DesignatePrinterRQ.PartyId[] APartyIdArray_ = new WS_SsoftSabre.DesignatePrinterRQ.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                WS_SsoftSabre.DesignatePrinterRQ.Service Servicio_ = new WS_SsoftSabre.DesignatePrinterRQ.Service();
                WS_SsoftSabre.DesignatePrinterRQ.MessageData MensajeDatos_ = new WS_SsoftSabre.DesignatePrinterRQ.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "DesignatePrinterLLSRQ";

                Servicio_.Value = "DesignatePrinterRQ";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Mensaje_;
        }
        public static AirTicketRQ.MessageHeader AirTicket()
        {
            WS_SsoftSabre.AirTicketRQ.MessageHeader Mensaje_ = new WS_SsoftSabre.AirTicketRQ.MessageHeader();
            VO_Credentials objvo_Credentials = clsSesiones.getCredentials();

            try
            {
                Mensaje_.ConversationId = objvo_Credentials.Conversacion;

                DateTime Fecha_ = DateTime.UtcNow;
                string Tiempo_ = Fecha_.ToString("s") + "Z";

                WS_SsoftSabre.AirTicketRQ.From Desde_ = new WS_SsoftSabre.AirTicketRQ.From();
                WS_SsoftSabre.AirTicketRQ.PartyId DesdePartyID_ = new WS_SsoftSabre.AirTicketRQ.PartyId();
                WS_SsoftSabre.AirTicketRQ.PartyId[] DesdePartyIdArray_ = new WS_SsoftSabre.AirTicketRQ.PartyId[1];

                DesdePartyID_.Value = "99999";
                DesdePartyIdArray_[0] = DesdePartyID_;
                Desde_.PartyId = DesdePartyIdArray_;
                Mensaje_.From = Desde_;

                WS_SsoftSabre.AirTicketRQ.To A_ = new WS_SsoftSabre.AirTicketRQ.To();
                WS_SsoftSabre.AirTicketRQ.PartyId APartyID_ = new WS_SsoftSabre.AirTicketRQ.PartyId();
                WS_SsoftSabre.AirTicketRQ.PartyId[] APartyIdArray_ = new WS_SsoftSabre.AirTicketRQ.PartyId[1];

                APartyID_.Value = "123123";
                APartyIdArray_[0] = APartyID_;
                A_.PartyId = APartyIdArray_;
                Mensaje_.To = A_;

                //-------------------------------------------------------------------------------------

                WS_SsoftSabre.AirTicketRQ.Service Servicio_ = new WS_SsoftSabre.AirTicketRQ.Service();
                WS_SsoftSabre.AirTicketRQ.MessageData MensajeDatos_ = new WS_SsoftSabre.AirTicketRQ.MessageData();

                Mensaje_.CPAId = objvo_Credentials.Ipcc;
                Mensaje_.Action = "AirTicketLLSRQ";

                Servicio_.Value = "AirTicketRQ";
                Mensaje_.Service = Servicio_;

                MensajeDatos_.MessageId = objvo_Credentials.Mensaje;
                MensajeDatos_.Timestamp = Tiempo_;
                Mensaje_.MessageData = MensajeDatos_;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Mensaje_;
        }
    }
}
