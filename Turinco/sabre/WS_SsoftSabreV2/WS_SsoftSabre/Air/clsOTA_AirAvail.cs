using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.ValueObjects;
using WS_SsoftSabre.OTA_AirAvail;
using System.Configuration;
using Espacio_validaciones = WS_SsoftSabre.Utilidades;
using OTA_AirAvailRQ = WS_SsoftSabre.OTA_AirAvail;
using Ssoft.Utils;
using Ssoft.Rules.WebServices;
using Ssoft.ManejadorExcepciones;
using System.Xml.Serialization;
using System.IO;

namespace WS_SsoftSabre.Air
{
    public class clsOTA_AirAvail
    {
        #region [ CONSTRUCTOR ]
        public clsOTA_AirAvail()
        {
            session_ = AutenticacionSabre.GET_SabreSession();
        }
        #endregion

        #region [CAMPOS]
        string session_ = string.Empty;
        VO_Credentials objvo_Credentials;
        #endregion

        #region [ PROPIEDADES ]

        public string Session_
        {
            get { return session_; }
            set { session_ = value; }
        }

        #endregion

        #region [ METODOS]
        public OTA_AirAvailRS getBusquedaHora(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
            /*METODO PRINCICPAL QUE RETORNA EL OBJETO DE RESULTADOS DE SABRE*/
            clsParametros cParametros = new clsParametros();
            StringBuilder consulta = new StringBuilder();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
            csVuelos cVuelos = new csVuelos();
            try
            {
                if (vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida.Count.Equals(0))
                {
                    vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = cVuelos.AerolineasPreferidas();
                }
            }
            catch { }
            if (vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida.Count.Equals(0))
                vo_OTA_AirLowFareSearchLLSRQ.LsExcluirAerolinea = cVuelos.ExcluirAerolineas();
            //bool PriorityTime = true;

            OTA_AirAvailRQ.OTA_AirAvailRQ oOTA_AirLowFareSearchRQ = new OTA_AirAvailRQ.OTA_AirAvailRQ();
            OTA_AirAvailRS oOTA_AirLowFareSearchRS = new OTA_AirAvailRS();
            objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();

            try
            {
                List<string> lsContadorOpciones = new List<string>();
                OTA_AirAvail.MessageHeader Mensaje_ = clsSabreBase.OTA_AirAvail();

                if (Mensaje_ != null)
                {
                    OTA_AirAvail.Security Seguridad_ = new OTA_AirAvail.Security();
                    Seguridad_.BinarySecurityToken = Session_;

                    #region [ POS ]
                    OTA_AirAvailRQPOS oOTA_AirLowFareSearchRQPOS = new OTA_AirAvailRQPOS();
                    OTA_AirAvailRQPOSSource oOTA_AirLowFareSearchRQPOSSource = new OTA_AirAvailRQPOSSource();

                    oOTA_AirLowFareSearchRQPOSSource.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                    oOTA_AirLowFareSearchRQPOS.Source = oOTA_AirLowFareSearchRQPOSSource;
                    oOTA_AirLowFareSearchRQ.POS = oOTA_AirLowFareSearchRQPOS;
                    #endregion

                    #region [ VERSION ]
                    oOTA_AirLowFareSearchRQ.Version = clsSabreBase.SABRE_VERSION_OTA_AIRAVAIL;
                    #endregion

                    #region [ ORIGINDESTINATIONINFORMATION ]

                    List<VO_OriginDestinationInformation> lvo_Rutas = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas;

                    if (lvo_Rutas == null)
                    {
                        throw new Exception("No se recibieron rutas a procesar");
                    }
                    else
                    {
                        int iOriginDestinationInformation = vo_OTA_AirLowFareSearchLLSRQ.Ruta;
                        VO_OriginDestinationInformation vo_OriginDestinationInformation = lvo_Rutas[iOriginDestinationInformation];
                        OTA_AirAvailRQOriginDestinationInformation oOTA_AirLowFareSearchRQOriginDestinationInformation = new OTA_AirAvailRQOriginDestinationInformation();

                        OTA_AirAvailRQOriginDestinationInformationDepartureDateTime DepartureDateTime =
                            new OTA_AirAvailRQOriginDestinationInformationDepartureDateTime();

                        //oOTA_AirLowFareSearchRQOriginDestinationInformation.ExcludeCodeShare = false;

                        DepartureDateTime.DateTime = vo_OriginDestinationInformation.SFechaSalida;
                        oOTA_AirLowFareSearchRQOriginDestinationInformation.DepartureDateTime = DepartureDateTime;

                        if (vo_OriginDestinationInformation.Vo_AeropuertoOrigen != null)
                        {
                            VO_Aeropuerto vo_Origen = vo_OriginDestinationInformation.Vo_AeropuertoOrigen;

                            OTA_AirAvailRQOriginDestinationInformationOriginLocation oOriginLocation = new OTA_AirAvailRQOriginDestinationInformationOriginLocation();

                            oOriginLocation.CodeContext = vo_Origen.SContexto;
                            oOriginLocation.LocationCode = vo_Origen.SCodigo;

                            oOTA_AirLowFareSearchRQOriginDestinationInformation.OriginLocation = oOriginLocation;

                        }
                        VO_Aeropuerto vo_Destino = vo_OriginDestinationInformation.Vo_AeropuertoDestino;
                        OTA_AirAvailRQOriginDestinationInformationDestinationLocation oDestinationLocation =
                            new OTA_AirAvailRQOriginDestinationInformationDestinationLocation();
                        oDestinationLocation.LocationCode = vo_Destino.SCodigo;
                        oDestinationLocation.CodeContext = vo_Destino.SContexto;
                        oOTA_AirLowFareSearchRQOriginDestinationInformation.DestinationLocation = oDestinationLocation;

                        #region [ TPA_EXTENSIONS ]

                        //OTA_AirAvailRQOriginDestinationInformationTPA_Extensions oOriginDestinationInformationTPA_Extensions =
                        //    new OTA_AirAvailRQOriginDestinationInformationTPA_Extensions();

                        //OTA_AirAvailRQOriginDestinationInformationTPA_ExtensionsArrivalDateTime TPA_ExtensionsArrivalDateTime =
                        //    new OTA_AirAvailRQOriginDestinationInformationTPA_ExtensionsArrivalDateTime();

                        //OTA_AirAvailRQOriginDestinationInformationTPA_ExtensionsScan TPA_ExtensionsScan =
                        //    new OTA_AirAvailRQOriginDestinationInformationTPA_ExtensionsScan();

                        //TPA_ExtensionsArrivalDateTime.DateTime = vo_OriginDestinationInformation.SFechaSalida;
                        //TPA_ExtensionsScan.Ind = true;
                        //TPA_ExtensionsScan.IndSpecified = true;

                        //oOriginDestinationInformationTPA_Extensions.ArrivalDateTime = TPA_ExtensionsArrivalDateTime;
                        //oOriginDestinationInformationTPA_Extensions.Scan = TPA_ExtensionsScan;

                        //oOTA_AirLowFareSearchRQOriginDestinationInformation.TPA_Extensions =
                        //                            oOriginDestinationInformationTPA_Extensions;

                        #endregion

                        oOTA_AirLowFareSearchRQ.OriginDestinationInformation =
                            oOTA_AirLowFareSearchRQOriginDestinationInformation;
                    }
                    #endregion

                    #region [ TRAVELPREFERENCES ]

                    OTA_AirAvailRQTravelPreferences oOTA_AirLowFareSearchRQTravelPreferences =
                        new OTA_AirAvailRQTravelPreferences();

                    string sMaximasParadas = vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas;

                    if (clsValidaciones.getValidarString(sMaximasParadas))
                    {
                        if (sMaximasParadas.Equals("0"))
                        {
                            oOTA_AirLowFareSearchRQTravelPreferences.MaxStopsQuantity = sMaximasParadas;
                        }
                    }

                    List<string> lsAerolineaPreferida = vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida;

                    if (lsAerolineaPreferida != null)
                    {
                        int iContadorAerolinea = 0;

                        OTA_AirAvailRQTravelPreferencesVendorPref[] aOTA_AirLowFareSearchRQTravelPreferencesVendorPref =
                            new OTA_AirAvailRQTravelPreferencesVendorPref[lsAerolineaPreferida.Count];

                        foreach (string sAerolineaPreferida in lsAerolineaPreferida)
                        {
                            OTA_AirAvailRQTravelPreferencesVendorPref oOTA_AirLowFareSearchRQTravelPreferencesVendorPref =
                                new OTA_AirAvailRQTravelPreferencesVendorPref();
                            oOTA_AirLowFareSearchRQTravelPreferencesVendorPref.Code = sAerolineaPreferida.Trim();
                            aOTA_AirLowFareSearchRQTravelPreferencesVendorPref[iContadorAerolinea] =
                                oOTA_AirLowFareSearchRQTravelPreferencesVendorPref;
                            iContadorAerolinea++;
                        }

                        oOTA_AirLowFareSearchRQTravelPreferences.VendorPref = aOTA_AirLowFareSearchRQTravelPreferencesVendorPref;
                    }


                    List<string> lsClases = vo_OTA_AirLowFareSearchLLSRQ.LsClase;
                    try
                    {
                        if (lsClases != null)
                        {
                            if (lsClases.Count > 0)
                            {
                                OTA_AirAvailRQTravelPreferencesCabinPref oCabinPref =
                                    new OTA_AirAvailRQTravelPreferencesCabinPref();
                                oCabinPref.Cabin = lsClases[0];
                                oOTA_AirLowFareSearchRQTravelPreferences.CabinPref =
                                    oCabinPref;
                            }
                        }
                    }
                    catch { }
                    #endregion

                    #region [ TPA_EXTENSIONS ]

                    OTA_AirAvailRQTravelPreferencesTPA_Extensions oTravelPreferencesTPA_Extensions =
                        new OTA_AirAvailRQTravelPreferencesTPA_Extensions();

                    OTA_AirAvailRQTravelPreferencesTPA_ExtensionsDirectAccess oDirectAccess =
                        new OTA_AirAvailRQTravelPreferencesTPA_ExtensionsDirectAccess();

                    oDirectAccess.Ind = false;
                    oTravelPreferencesTPA_Extensions.DirectAccess = oDirectAccess;

                    List<string> lsNoAerolineas = vo_OTA_AirLowFareSearchLLSRQ.LsExcluirAerolinea;

                    if (lsNoAerolineas != null)
                    {
                        int iContNoAerolineas = 0;
                        OTA_AirAvailRQTravelPreferencesTPA_ExtensionsExcludeVendorPref[] aOTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref =
                            new OTA_AirAvailRQTravelPreferencesTPA_ExtensionsExcludeVendorPref[lsNoAerolineas.Count];

                        foreach (string sAerolinea in lsNoAerolineas)
                        {
                            OTA_AirAvailRQTravelPreferencesTPA_ExtensionsExcludeVendorPref oAerolinea =
                                new OTA_AirAvailRQTravelPreferencesTPA_ExtensionsExcludeVendorPref();
                            oAerolinea.Code = sAerolinea;
                            aOTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref[iContNoAerolineas] = oAerolinea;
                            iContNoAerolineas++;
                        }
                        oTravelPreferencesTPA_Extensions.ExcludeVendorPref =
                            aOTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsExcludeVendorPref;
                    }

                    /*string sViajes = vo_OTA_AirLowFareSearchLLSRQ.SVuelosARetornar;
                    if (Validaciones.getValidarString(sViajes))
                    {
                        OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsNumTrips oNumTrips =
                            new OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsNumTrips();
                        oNumTrips.Number = sViajes;
                        oTravelPreferencesTPA_Extensions.NumTrips = oNumTrips;
                    }*/


                    oOTA_AirLowFareSearchRQTravelPreferences.TPA_Extensions = oTravelPreferencesTPA_Extensions;
                    oOTA_AirLowFareSearchRQ.TravelPreferences = oOTA_AirLowFareSearchRQTravelPreferences;

                    #endregion

                    #region [ TRAVELERINFORMATION ]

                    #endregion

                    #region [ PRICEREQUESTINFORMATION ]

                    #endregion

                    #region [ TPA_EXTENSIONS ]

                    #endregion

                    OTA_AirAvailService oOTA_AirLowFareSearchService = new OTA_AirAvailService();

                    oOTA_AirLowFareSearchService.MessageHeaderValue = Mensaje_;
                    oOTA_AirLowFareSearchService.SecurityValue = Seguridad_;
                    oOTA_AirLowFareSearchService.Url = objvo_Credentials.UrlWebServices;

                    oOTA_AirLowFareSearchRS = oOTA_AirLowFareSearchService.OTA_AirAvailRQ(oOTA_AirLowFareSearchRQ);

                    // XML
                    //string pathXML = clsValidaciones.XMLDatasetCrea() + "OTA_AirAvailRQ.xml";

                    //XmlSerializer SerializerRQ = new XmlSerializer(typeof(OTA_AirAvailRQ.OTA_AirAvailRQ));
                    //StreamWriter WriterRQ = new StreamWriter(pathXML);
                    //try
                    //{
                    //    SerializerRQ.Serialize(WriterRQ, oOTA_AirLowFareSearchRQ);
                    //}
                    //catch
                    //{
                    //    WriterRQ.Flush();
                    //    WriterRQ.Close();
                    //}

                    //pathXML = clsValidaciones.XMLDatasetCrea() + "OTA_AirAvailRS.xml";

                    //XmlSerializer SerializerRS = new XmlSerializer(typeof(OTA_AirAvailRS));
                    //StreamWriter WriterRS = new StreamWriter(pathXML);
                    //try
                    //{
                    //    SerializerRS.Serialize(WriterRS, oOTA_AirLowFareSearchRS);
                    //}
                    //catch
                    //{
                    //    WriterRS.Flush();
                    //    WriterRS.Close();
                    //}
                    //termina XML


                    if (oOTA_AirLowFareSearchRS.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.Code = oOTA_AirLowFareSearchRS.Errors.Error.ErrorCode;
                        cParametros.Info = oOTA_AirLowFareSearchRS.Errors.Error.ErrorInfo.Message;
                        cParametros.Message = oOTA_AirLowFareSearchRS.Errors.Error.ErrorMessage;
                        cParametros.Severity = oOTA_AirLowFareSearchRS.Errors.Error.Severity;
                        cParametros.Complemento = "HostCommand: " + oOTA_AirLowFareSearchRS.TPA_Extensions.HostCommand;
                        cParametros.Metodo = "getBusqueda";
                        consulta.AppendLine("Credenciales: ");
                        try
                        {
                            if (objvo_Credentials != null)
                            {
                                consulta.AppendLine("User: " + objvo_Credentials.User);
                                consulta.AppendLine("Password: " + objvo_Credentials.Password);
                                consulta.AppendLine("Ipcc: " + objvo_Credentials.Ipcc);
                                consulta.AppendLine("Pcc: " + objvo_Credentials.Pcc);
                                consulta.AppendLine("QNumber: " + objvo_Credentials.QNumber);
                                consulta.AppendLine("Dominio: " + objvo_Credentials.Dominio);
                                consulta.AppendLine("Session Sabre: " + Session_.ToString());
                                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                            }
                        }
                        catch { }
                        cParametros.Tipo = clsTipoError.WebServices;
                        ExceptionHandled.Publicar(cParametros);
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.TipoLog = Enum_Error.Transac;
                        cParametros.Message = oOTA_AirLowFareSearchRS.Success;
                        cParametros.Metodo = "getBusqueda";
                        cParametros.Complemento = "HostCommand: " + oOTA_AirLowFareSearchRS.TPA_Extensions.HostCommand;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Severity = clsSeveridad.Moderada;
                        consulta.AppendLine("Credenciales: ");
                        try
                        {
                            if (objvo_Credentials != null)
                            {
                                consulta.AppendLine("User: " + objvo_Credentials.User);
                                consulta.AppendLine("Password: " + objvo_Credentials.Password);
                                consulta.AppendLine("Ipcc: " + objvo_Credentials.Ipcc);
                                consulta.AppendLine("Pcc: " + objvo_Credentials.Pcc);
                                consulta.AppendLine("QNumber: " + objvo_Credentials.QNumber);
                                consulta.AppendLine("Dominio: " + objvo_Credentials.Dominio);
                                consulta.AppendLine("Session Sabre: " + Session_.ToString());
                                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                            }
                        }
                        catch { }
                        cParametros.TargetSite = consulta.ToString();
                        ExceptionHandled.Publicar(cParametros);
                        vo_OTA_AirLowFareSearchLLSRQ.Ruta++;
                        clsSesiones.setParametrosAirBargain
                           (
                              vo_OTA_AirLowFareSearchLLSRQ
                           );
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Source = Ex.Source;
                cParametros.TargetSite = Ex.TargetSite.ToString();
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Metodo = "getBusqueda";
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
            }
            return oOTA_AirLowFareSearchRS;
        }
        #endregion
    }
}
