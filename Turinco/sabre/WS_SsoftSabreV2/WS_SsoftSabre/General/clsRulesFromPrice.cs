using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Ssoft.ValueObjects;
using WS_SsoftSabre.Air;
using System.Configuration;
using Ssoft.ManejadorExcepciones;
using System.IO;
using WS_SsoftSabre.RulesFromPrice;
using Ssoft.Utils;

namespace WS_SsoftSabre.General
{
    public class clsRulesFromPrice
    {
        string strSesion;
        Ssoft.ValueObjects.VO_Credentials objvo_Credentials;
        public string StrSesion
        {
            get { return strSesion; }
            set { strSesion = value; }
        }

        public RulesFromPriceRS getRules(VO_RulesFromPriceRQ vo_RulesFromPriceRQ)
        {
            RulesFromPriceRS oRulesFromPriceRS = new RulesFromPriceRS();
            objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();
            MessageHeader strMensaje = clsSabreBase.RulesFromPrice();
            try
            {
                if (strMensaje != null)
                {
                    int iCountCategory = vo_RulesFromPriceRQ.LsRuleCategoryNumber.Count;
                    int iCountSegment = vo_RulesFromPriceRQ.LvoSegmentSelect.Count;
                    RulesFromPriceRQ oRulesFromPriceRQ = new RulesFromPriceRQ();
                    RulesFromPriceRQPOS oPOS = new RulesFromPriceRQPOS();
                    RulesFromPriceRQPOSSource oPOSSource = new RulesFromPriceRQPOSSource();

                    oPOSSource.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                    oPOS.Source = oPOSSource;
                    oRulesFromPriceRQ.POS = oPOS;

                    //clase
                    RulesFromPriceRQRuleReqInfo oRuleReqInfo = new RulesFromPriceRQRuleReqInfo();
                    RulesFromPriceRQRuleReqInfoFareBasis oRuleReqInfoFareBasis = new RulesFromPriceRQRuleReqInfoFareBasis();
                    RulesFromPriceRQRuleReqInfoPassenger oRuleReqInfoPassenger = new RulesFromPriceRQRuleReqInfoPassenger();
                    RulesFromPriceRQRuleReqInfoRuleCategory[] oRuleReqInfoRuleCategorys = new RulesFromPriceRQRuleReqInfoRuleCategory[iCountCategory];
                    RulesFromPriceRQRuleReqInfoRuleCategory oRuleReqInfoRuleCategory = new RulesFromPriceRQRuleReqInfoRuleCategory();
                    RulesFromPriceRQRuleReqInfoSegmentSelect[] oRuleReqInfoSegmentSelecs = new RulesFromPriceRQRuleReqInfoSegmentSelect[iCountSegment];
                    RulesFromPriceRQRuleReqInfoSegmentSelect oRuleReqInfoSegmentSelec = new RulesFromPriceRQRuleReqInfoSegmentSelect();

                    oRuleReqInfoPassenger.TypeNumber = vo_RulesFromPriceRQ.VPassenger.SCantidad;
                    oRuleReqInfoPassenger.Code = vo_RulesFromPriceRQ.VPassenger.SCodigo;

                    for (int i = 0; i < iCountCategory; i++)
                    {
                        oRuleReqInfoRuleCategory.Number = vo_RulesFromPriceRQ.LsRuleCategoryNumber[i];
                        oRuleReqInfoRuleCategorys[i] = oRuleReqInfoRuleCategory;
                    }

                    for (int i = 0; i < iCountSegment; i++)
                    {
                        oRuleReqInfoSegmentSelec.Number = vo_RulesFromPriceRQ.LvoSegmentSelect[i].SNumber;
                        //oRuleReqInfoSegmentSelec.EndNumber = vo_RulesFromPriceRQ.LvoSegmentSelect[i].SEndNumber;
                        oRuleReqInfoSegmentSelecs[i] = oRuleReqInfoSegmentSelec;
                    }

                    if (!vo_RulesFromPriceRQ.SFareBasisCode.Length.Equals(0))
                    {
                        oRuleReqInfoFareBasis.Code = vo_RulesFromPriceRQ.SFareBasisCode;
                    }

                    oRuleReqInfo.FareBasis = oRuleReqInfoFareBasis;
                    oRuleReqInfo.Passenger = oRuleReqInfoPassenger;
                    oRuleReqInfo.RuleCategory = oRuleReqInfoRuleCategorys;
                    oRuleReqInfo.SegmentSelect = oRuleReqInfoSegmentSelecs;

                    oRulesFromPriceRQ.RuleReqInfo = oRuleReqInfo;

                    RulesFromPriceRQTPA_Extensions oTPA_Extensions = new RulesFromPriceRQTPA_Extensions();
                    RulesFromPriceRQTPA_ExtensionsMessagingDetails oTPA_ExtensionsMessagingDetails = new RulesFromPriceRQTPA_ExtensionsMessagingDetails();
                    RulesFromPriceRQTPA_ExtensionsMessagingDetailsMDRSubset oTPA_ExtensionsMessagingDetailsMDRSubset = new RulesFromPriceRQTPA_ExtensionsMessagingDetailsMDRSubset();

                    if (!vo_RulesFromPriceRQ.SMDRSubset.Length.Equals(0))
                    {
                        oTPA_ExtensionsMessagingDetailsMDRSubset.Code = vo_RulesFromPriceRQ.SMDRSubset;
                    }

                    oTPA_ExtensionsMessagingDetails.MDRSubset = oTPA_ExtensionsMessagingDetailsMDRSubset;
                    oTPA_Extensions.MessagingDetails = oTPA_ExtensionsMessagingDetails;

                    oRulesFromPriceRQ.TPA_Extensions = oTPA_Extensions;

                    oRulesFromPriceRQ.Version = clsSabreBase.RULES_FROM_PRICE_VERSION;

                    Security oSecurity = new Security();
                    oSecurity.BinarySecurityToken = strSesion;

                    RulesFromPriceService oRulesFromPriceService = new RulesFromPriceService();
                    oRulesFromPriceService.MessageHeaderValue = strMensaje;
                    oRulesFromPriceService.SecurityValue = oSecurity;
                    oRulesFromPriceRS = oRulesFromPriceService.RulesFromPriceRQ(oRulesFromPriceRQ);
                }
                if (oRulesFromPriceRS.Errors != null)
                {
                    //clsParametros cMensaje = new clsParametros();
                    //cMensaje.Id = 0;
                    //cMensaje.Code = oRulesFromPriceRS.Errors.Error.ErrorCode;
                    //cMensaje.Info = oRulesFromPriceRS.Errors.Error.ErrorInfo.Message;
                    //cMensaje.Message = oRulesFromPriceRS.Errors.Error.ErrorMessage;
                    //cMensaje.Severity = oRulesFromPriceRS.Errors.Error.Severity;
                    //cMensaje.Tipo = clsTipoError.WebServices;
                    //cMensaje.Metodo = "RulesFromPriceRS";
                    //cMensaje.Complemento = "Reglas de tarifas aereas";
                    //ExceptionHandled.Publicar(cMensaje);
                }
            }
            catch 
            {
                //clsParametros cMensaje = new clsParametros();
                //cMensaje.Id = 0;
                //cMensaje.Message = Ex.Message;
                //cMensaje.Severity = clsSeveridad.Alta;
                //cMensaje.Tipo = clsTipoError.WebServices;
                //cMensaje.Metodo = "RulesFromPriceRS";
                //cMensaje.Complemento = "Reglas de tarifas aereas";
                //cMensaje.Source = Ex.Source;
                //cMensaje.StackTrace = Ex.StackTrace;
                //ExceptionHandled.Publicar(cMensaje);
            }
            return oRulesFromPriceRS;
        }
        public RulesFromPriceRS getRulesGen()
        {
            List<VO_SegmentSelect> lvo_SegmentSelect = new List<VO_SegmentSelect>();
            VO_SegmentSelect vo_SegmentSelect = new VO_SegmentSelect();
            VO_Pasajero vo_Pasajero = new VO_Pasajero();

            vo_Pasajero.SCantidad = "1";
            vo_Pasajero.SCodigo = VO_Pasajero.ADULTO;

            vo_SegmentSelect.SNumber = "1";
            vo_SegmentSelect.SEndNumber = "1";

            lvo_SegmentSelect.Add(vo_SegmentSelect);

            List<string> lsCategory = new List<string>();

            lsCategory.Add("2");
            lsCategory.Add("15");
            lsCategory.Add("16");
            lsCategory.Add("19");
            lsCategory.Add("22");

            RulesFromPriceRS oRulesFromPriceRS = new RulesFromPriceRS();

            VO_RulesFromPriceRQ vo_RulesFromPriceRQ = new VO_RulesFromPriceRQ(lsCategory, vo_Pasajero, lvo_SegmentSelect, string.Empty, string.Empty);

            oRulesFromPriceRS = getRules(vo_RulesFromPriceRQ);
            // termina metodo
            return oRulesFromPriceRS;
        }
        public string getRulesCommand()
        {
            string sRespuesta = string.Empty;
            try
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Message = "Entra getRulesCommand";
                cParametros.Complemento = "Reserva Vuelos";
                ExceptionHandled.Publicar(cParametros);


                VO_SabreCommandLLSRS vo = new VO_SabreCommandLLSRS();
                vo.BCDATA = true;
                vo.StrComando = "WPRD*PADT¥C2/15/16/19/22¥S1";

                clsSabreCommandLLS oclsSabreCommandLLS = new clsSabreCommandLLS();
                oclsSabreCommandLLS.StrSesion = strSesion;
                WS_SsoftSabre.SabreCommandLLS.SabreCommandLLSRS respuesta = oclsSabreCommandLLS.getEjecutarComando(vo);
                int iPosIni = respuesta.Response.IndexOf("02.DAY/TIME");
                if (iPosIni == -1)
                    iPosIni = 0;
                //int iPosFin = respuesta.Response.Length;

                sRespuesta = respuesta.Response.Remove(0, iPosIni);
            }
            catch { }
            return sRespuesta;
        }        
        public clsParametros getRulesSegment()
        {
            clsParametros objParametros = new clsParametros();
            VO_SabreCommandLLSRS vo = new VO_SabreCommandLLSRS();
            DateTime dtm_Fecha_Segmento = DateTime.Now;
            /*SE SUMAN LOS 330 DIAS*/
            int iDias = 330;
            try { iDias = clsSesiones.getCredentials().SegmentoFuturo; }
            catch { }
            if (iDias.Equals(0))
            {
                objParametros.Id = 0;
                objParametros.Code = "0";
                objParametros.Info = "Segmento no incluido por ser valor 0";
                objParametros.Message = "No icluye OTH";
                objParametros.Severity = clsSeveridad.Baja;
                objParametros.Metodo = "clsRulesFromPrice.getRulesSegment()";
                objParametros.Tipo = clsTipoError.WebServices;
                Ssoft.ManejadorExcepciones.ExceptionHandled.Publicar(objParametros);
            }
            else
            {
                dtm_Fecha_Segmento = dtm_Fecha_Segmento.AddDays(iDias);
                /*OBTENEMOS EL MES EN LETRAS, EN INGLES*/
                string str_Mes_Letras = clsValidaciones.RetornaMesLetrasCorto(dtm_Fecha_Segmento.Month.ToString(), "en");
                String str_Dia = dtm_Fecha_Segmento.Day.ToString();
                if (str_Dia.Length == 1)
                    str_Dia = "0" + str_Dia;
                String str_Comando = "0OTHAAGK1BOG" + str_Dia + str_Mes_Letras.ToUpper();
                vo.BCDATA = true;
                vo.StrComando = str_Comando;
                clsSabreCommandLLS oclsSabreCommandLLS = new clsSabreCommandLLS();
                oclsSabreCommandLLS.StrSesion = strSesion;
                WS_SsoftSabre.SabreCommandLLS.SabreCommandLLSRS respuesta = oclsSabreCommandLLS.getEjecutarComando(vo);

                if (respuesta.ErrorRS != null)
                {
                    objParametros.Id = 0;
                    objParametros.Code = respuesta.ErrorRS.Errors.Error.ErrorCode;
                    objParametros.Info = respuesta.ErrorRS.Errors.Error.ErrorInfo.Message;
                    objParametros.Message = respuesta.ErrorRS.Errors.Error.ErrorMessage;
                    objParametros.Severity = respuesta.ErrorRS.Errors.Error.Severity;
                    objParametros.Metodo = "clsRulesFromPrice.getRulesSegment()";
                    objParametros.Tipo = Ssoft.ManejadorExcepciones.clsTipoError.WebServices;
                    Ssoft.ManejadorExcepciones.ExceptionHandled.Publicar(objParametros);
                }
                else
                {
                    objParametros.Id = 1;
                    objParametros.Message = respuesta.Response;
                    objParametros.DatoAdic = respuesta.AltLangID;
                    objParametros.DatoAdicArr.Add(respuesta.EchoToken);
                    objParametros.Data = respuesta.PrimaryLangID;
                    objParametros.Metodo = "clsRulesFromPrice.getRulesSegment()";
                    Ssoft.ManejadorExcepciones.ExceptionHandled.Publicar(objParametros);
                }
            }
            return objParametros;
        }
    }
}
