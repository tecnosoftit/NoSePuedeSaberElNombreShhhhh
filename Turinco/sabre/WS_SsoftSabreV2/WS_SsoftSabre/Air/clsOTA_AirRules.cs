using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using WS_SsoftSabre.OTA_AirRules;

using System.Xml.Serialization;
using Ssoft.Ssoft.ValueObjects.Vuelos;

namespace WS_SsoftSabre.Air
{
    public class clsOTA_AirRules
    {
        #region [CAMPOS]
        string session_ = string.Empty;
        Ssoft.ValueObjects.VO_Credentials objvo_Credentials;
        #endregion

        #region [ PROPIEDADES ]

        public string Session_
        {
            get { return session_; }
            set { session_ = value; }
        }

        #endregion

        #region [ METODOS]
        public OTA_AirRulesRS getRules(VO_OTA_AirRulesRQ vo_OTA_AirRulesRQ)
        {
            OTA_AirRulesRS oOTA_AirRulesRS = null;
              MessageHeader strMensaje = clsSabreBase.OTA_AirRules();
                        objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();

              if (strMensaje != null)
              {

                  OTA_AirRulesRQ oOTA_AirRulesRQ = new OTA_AirRulesRQ();
                  OTA_AirRulesRQRuleReqInfo oOTA_AirRulesRQRuleReqInfo = new OTA_AirRulesRQRuleReqInfo();


                  #region [ POS ]
                  OTA_AirRulesRQPOS oOTA_AirRulesRQPOS = new OTA_AirRulesRQPOS();
                  OTA_AirRulesRQPOSSource oOTA_AirLowFareSearchRQPOSSource = new OTA_AirRulesRQPOSSource();

                  oOTA_AirLowFareSearchRQPOSSource.PseudoCityCode = objvo_Credentials.Ipcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                  oOTA_AirRulesRQPOS.Source = oOTA_AirLowFareSearchRQPOSSource;
                  oOTA_AirRulesRQ.POS = oOTA_AirRulesRQPOS;
                  
                  #endregion
                  
                  //clase
                  OTA_AirRulesRQRuleReqInfoFareReference oOTA_AirRulesRQRuleReqInfoFareReference= new OTA_AirRulesRQRuleReqInfoFareReference();
                  oOTA_AirRulesRQRuleReqInfoFareReference.Code=vo_OTA_AirRulesRQ.StrClase;
                  oOTA_AirRulesRQRuleReqInfo.FareReference = oOTA_AirRulesRQRuleReqInfoFareReference;

                  //AEROLINEA
                  OTA_AirRulesRQRuleReqInfoFilingAirline oOTA_AirRulesRQRuleReqInfoFilingAirline=new OTA_AirRulesRQRuleReqInfoFilingAirline();
                  oOTA_AirRulesRQRuleReqInfoFilingAirline.Code=vo_OTA_AirRulesRQ.StrCodigoAerolinea;
                  oOTA_AirRulesRQRuleReqInfo.FilingAirline = oOTA_AirRulesRQRuleReqInfoFilingAirline;
                  
                  //ORIGEN
                  OTA_AirRulesRQRuleReqInfoDepartureAirport oOTA_AirRulesRQRuleReqInfoDepartureAirport=new OTA_AirRulesRQRuleReqInfoDepartureAirport();
                  oOTA_AirRulesRQRuleReqInfoDepartureAirport.CodeContext = vo_OTA_AirRulesRQ.Vo_AeropuertoOrigen.SContexto;
                  oOTA_AirRulesRQRuleReqInfoDepartureAirport.LocationCode = vo_OTA_AirRulesRQ.Vo_AeropuertoOrigen.SCodigo;
                  oOTA_AirRulesRQRuleReqInfo.DepartureAirport = oOTA_AirRulesRQRuleReqInfoDepartureAirport;

                  //DESTINO
                  OTA_AirRulesRQRuleReqInfoArrivalAirport oOTA_AirRulesRQRuleReqInfoArrivalAirport = new OTA_AirRulesRQRuleReqInfoArrivalAirport();
                  oOTA_AirRulesRQRuleReqInfoArrivalAirport.CodeContext = vo_OTA_AirRulesRQ.Vo_AeropuertoDestino.SContexto;
                  oOTA_AirRulesRQRuleReqInfoArrivalAirport.LocationCode = vo_OTA_AirRulesRQ.Vo_AeropuertoDestino.SCodigo;
                  oOTA_AirRulesRQRuleReqInfo.ArrivalAirport = oOTA_AirRulesRQRuleReqInfoArrivalAirport;

                  //FECHA SALIDA
                  OTA_AirRulesRQRuleReqInfoDepartureDate oOTA_AirRulesRQRuleReqInfoDepartureDate=new OTA_AirRulesRQRuleReqInfoDepartureDate();
                  oOTA_AirRulesRQRuleReqInfoDepartureDate.DateTime = vo_OTA_AirRulesRQ.DtmFechaSalida.ToString(Ssoft.ValueObjects.VO_SabreBase.FORMATO_TIME_STAMP);
                  oOTA_AirRulesRQRuleReqInfo.DepartureDate = oOTA_AirRulesRQRuleReqInfoDepartureDate;
                  //oOTA_AirRulesRQRuleReqInfo.RPH = vo_OTA_AirRulesRQ.sRPH;
               
                  oOTA_AirRulesRQ.RuleReqInfo = oOTA_AirRulesRQRuleReqInfo;
                  
                  //VERSION
                  oOTA_AirRulesRQ.Version = clsSabreBase.OTA_AIR_RULES_VERSION;
                
                  OTA_AirRulesRQTPA_Extensions oOTA_AirRulesRQTPA_Extensions = new OTA_AirRulesRQTPA_Extensions();
                  
                  OTA_AirRulesRQTPA_ExtensionsMessagingDetails oOTA_AirRulesRQTPA_ExtensionsMessagingDetails = new OTA_AirRulesRQTPA_ExtensionsMessagingDetails();
                  OTA_AirRulesRQTPA_ExtensionsMessagingDetailsMDRSubset oOTA_AirRulesRQTPA_ExtensionsMessagingDetailsMDRSubset=new OTA_AirRulesRQTPA_ExtensionsMessagingDetailsMDRSubset();
                  oOTA_AirRulesRQTPA_ExtensionsMessagingDetails.MDRSubset=oOTA_AirRulesRQTPA_ExtensionsMessagingDetailsMDRSubset;
                  oOTA_AirRulesRQTPA_ExtensionsMessagingDetailsMDRSubset.Code = "PN05";
                  oOTA_AirRulesRQTPA_ExtensionsMessagingDetails.MDRSubset = oOTA_AirRulesRQTPA_ExtensionsMessagingDetailsMDRSubset;
                  oOTA_AirRulesRQTPA_Extensions.MessagingDetails = oOTA_AirRulesRQTPA_ExtensionsMessagingDetails;
                  //oOTA_AirRulesRQ.TPA_Extensions = oOTA_AirRulesRQTPA_Extensions;

             
                  Security oSecurity = new Security();
                  oSecurity.BinarySecurityToken = Session_;

                  OTA_AirRulesService oOTA_AirRulesService = new OTA_AirRulesService();
                  oOTA_AirRulesService.MessageHeaderValue = strMensaje;
                  oOTA_AirRulesService.SecurityValue = oSecurity;
                  oOTA_AirRulesService.Url = objvo_Credentials.UrlWebServices;

                  oOTA_AirRulesRS = oOTA_AirRulesService.OTA_AirRulesRQ(oOTA_AirRulesRQ);



                  //XmlSerializer mySerializer = new XmlSerializer(typeof(OTA_AirRulesRQ));
                  // To write to a file, create a StreamWriter object.
                  //System.IO.StreamWriter myWriter = new System.IO.StreamWriter("D://bfmRQ-OTA_AirRulesRQ" + DateTime.Now.Hour + DateTime.Now.Minute + ".xml");
                  //mySerializer.Serialize(myWriter, oOTA_AirRulesRQ);
                  //myWriter.Close();

                  //mySerializer = new XmlSerializer(typeof(OTA_AirRulesRS));
                  // To write to a file, create a StreamWriter object.
                  //myWriter = new System.IO.StreamWriter("D://bfmRS-OTA_AirRulesRS" + DateTime.Now.Hour + DateTime.Now.Minute + ".xml");
                  //mySerializer.Serialize(myWriter, oOTA_AirRulesRS);
                  //myWriter.Close();


              }
              return oOTA_AirRulesRS;
        }
        #endregion
    }
}
