using System;
using System.Collections.Generic;
using System.Text;
using WS_SsoftSabre.SWS_BargainFinderMaxRQ;
using Ssoft.ValueObjects;
using System.Xml.Serialization;
using Ssoft.Rules.WebServices;
using Ssoft.Utils;

namespace WS_SsoftSabre.Air
{
    class ClsBargainFinderMaxRQ : ClsSabreBase
    {
        /// <summary>
        /// Returns 50 itineraries in the response. The action code in the SOAP header must be set to BargainFinderMaxRQ.
        /// </summary>
        private const string str50 = "50ITINS";
        /// <summary>
        /// Returns 100 itineraries. The action code in the SOAP header must be set to BargainFinderMaxRQ.
        /// </summary>
        private const string str100 = "100ITINS";
        /// <summary>
        /// Returns 200 itineraries. The action code in the SOAP header must be set to BargainFinderMaxRQ.
        /// </summary>
        private const string str200 = "200ITINS";
        #region [CAMPOS]
        string session_ = string.Empty;
        VO_Credentials objvo_Credentials;
        #endregion
        #region PROPERTIES

        public string session
        {
            get { return session_; }
            set { session_ = value; }
        }
        #endregion
        /// <summary>
        /// getBargainFinderMaxRQ returns up to 200 diferent options for the availability
        /// </summary> 
        /// <param name="vo_BargainFinderMax_ADRQ"></param>
        public override object getExecuteSWS(params object[] ota_AirPriceRQ)
        {
            VO_BargainFinderMax_ADRQ vo_BargainFinderMax_ADRQ = (VO_BargainFinderMax_ADRQ)ota_AirPriceRQ[0];

            OTA_AirLowFareSearchRQ ota_AirLowFareSearchRQ = new OTA_AirLowFareSearchRQ();
            session_ = AutenticacionSabre.GET_SabreSession();

            objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();

            SWS_BargainFinderMaxRQ.Security security = new SWS_BargainFinderMaxRQ.Security();
            security.BinarySecurityToken = session_;// vo_BargainFinderMax_ADRQ.Vo_SessionCreateRQ.;

            //exluimos aerolineas
            //hceron
            //29042013
            csVuelos cVuelos = new csVuelos();
            List<string> lsExcluirAerol = cVuelos.ExcluirAerolineas();

            #region [ POS ]
            //Source 
            SourceType[] sourceTypes = new SourceType[1];
            SourceType sourceType = new SourceType();

            VO_SessionCreateRQ vo_SessionCreateRQ = vo_BargainFinderMax_ADRQ.Vo_SessionCreateRQ;
            sourceType.PseudoCityCode = objvo_Credentials.Ipcc;

            //RequestorID
            UniqueID_Type uniqueID_Type = new UniqueID_Type();
            uniqueID_Type.ID = "1"; //"ID" (required) Not used for processing. Use a value of "1".
            uniqueID_Type.Type = "1"; //"Type" (required) Not used for processing. Use a value of "1".

            CompanyNameType companyNameType = new CompanyNameType();
            companyNameType.Code = "TN";
            uniqueID_Type.CompanyName = companyNameType; //"Code" (required) Customer code. Use the value "TN".
            sourceType.RequestorID = uniqueID_Type;

            sourceTypes[0] = sourceType;
            ota_AirLowFareSearchRQ.POS = sourceTypes;
            #endregion

            #region [ VERSION ]
            ota_AirLowFareSearchRQ.Version = WS_SsoftSabre.Air.Constant.SWS_Sevirce_Versions.BARGAINFINDERMAXRQ;
            #endregion

            #region [ ORIGINDESTINATIONINFORMATION ]

            List<string> lsContadorOpciones = new List<string>();
            List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation = vo_BargainFinderMax_ADRQ.Lvo_Segments;

            if (lvo_OriginDestinationInformation == null)
            {
                throw new Exception("lvo_OriginDestinationInformation is null,Pls check");
            }
            else
            {
                int iSegmentsCounter = 0;
                OTA_AirLowFareSearchRQOriginDestinationInformation[] aota_AirLowFareSearchRQOriginDestinationInformation =
                    new OTA_AirLowFareSearchRQOriginDestinationInformation[lvo_OriginDestinationInformation.Count];

                int iOriginDestinationInformation = 0;
                foreach (VO_OriginDestinationInformation vo_OriginDestinationInformation in lvo_OriginDestinationInformation)
                {
                    WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRQOriginDestinationInformation ota_AirLowFareSearchRQOriginDestinationInformation = new WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRQOriginDestinationInformation();

                    TimeInstantType timeInstantType = new TimeInstantType();
                    timeInstantType.Value = Convert.ToDateTime(vo_OriginDestinationInformation.SFechaSalida).ToString(Constant.FORMATO_TIME_STAMP);
                    ota_AirLowFareSearchRQOriginDestinationInformation.Item = timeInstantType;
                    ota_AirLowFareSearchRQOriginDestinationInformation.ItemElementName = ItemChoiceType.DepartureDateTime;

                    ota_AirLowFareSearchRQOriginDestinationInformation.DepartureWindow = vo_OriginDestinationInformation.SIntervaloSalida;
                    ota_AirLowFareSearchRQOriginDestinationInformation.ArrivalWindow = vo_OriginDestinationInformation.SIntervaloSalida;

                    //OriginLocation

                    LocationType originLocationType = new LocationType();
                    originLocationType.CodeContext = vo_OriginDestinationInformation.Vo_AeropuertoOrigen.SContexto;
                    originLocationType.LocationCode = vo_OriginDestinationInformation.Vo_AeropuertoOrigen.SCodigo;
                    ota_AirLowFareSearchRQOriginDestinationInformation.OriginLocation = originLocationType;

                    //DestinationLocation

                    LocationType destinationLocationType = new LocationType();
                    destinationLocationType.LocationCode = vo_OriginDestinationInformation.Vo_AeropuertoDestino.SCodigo;
                    destinationLocationType.CodeContext = vo_OriginDestinationInformation.Vo_AeropuertoDestino.SContexto;
                    ota_AirLowFareSearchRQOriginDestinationInformation.DestinationLocation = destinationLocationType;

                    #region [ TPA_EXTENSIONS ]

                    OTA_AirLowFareSearchRQOriginDestinationInformationTPA_Extensions originDestinationInformationTPA_Extensions =
                        new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_Extensions();

                    //SegmentType
                    OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentType oTPA_ExtensionsSegmentType =
                        new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentType();

                    oTPA_ExtensionsSegmentType.CodeSpecified = true;
                    switch (vo_OriginDestinationInformation.OTipoSegmento.ToString())
                    {
                        case TipoSegmento.O:
                            oTPA_ExtensionsSegmentType.Code = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                            break;

                        case TipoSegmento.ARUNK:
                            oTPA_ExtensionsSegmentType.Code = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                            break;

                        case TipoSegmento.X:
                            oTPA_ExtensionsSegmentType.Code = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.X;
                            break;
                    }

                    // oTPA_ExtensionsSegmentType.Code = (OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode)vo_OriginDestinationInformation.;
                    originDestinationInformationTPA_Extensions.SegmentType = oTPA_ExtensionsSegmentType;

                    //AlternateTime
                    string StrAlternateTime = vo_OriginDestinationInformation.STiempoAlternativo;
                    if (!String.IsNullOrEmpty(StrAlternateTime))
                    {
                        OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateTime oTPA_ExtensionsAlternateTime =
                            new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsAlternateTime();

                        oTPA_ExtensionsAlternateTime.PlusMinus = StrAlternateTime;
                        originDestinationInformationTPA_Extensions.AlternateTime = oTPA_ExtensionsAlternateTime;
                    }

                    //CabinPrefrencial
                    //VO_CabinPref vo_CabinPref = vo_OriginDestinationInformation.;
                    //if (vo_CabinPref != null)
                    //{
                    //CabinPrefType cabinPrefType = new CabinPrefType();
                    //cabinPrefType.Cabin = (CabinType)vo_CabinPref.Enum_CabinType;
                    //cabinPrefType.CabinSpecified = true;
                    //cabinPrefType.PreferLevel = (PreferLevelType)vo_CabinPref.Enum_PreferLevelType;
                    //originDestinationInformationTPA_Extensions.CabinPref = cabinPrefType;
                    // }

                    //ConnectionTime por definir usa
                    //hceron
                    int intConnectionTimeMax = 0; //vo_OriginDestinationInformation.IntConnectionTimeMax;
                    int intConnectionTimeMin = 0; //vo_OriginDestinationInformation.IntConnectionTimeMin;
                    if (intConnectionTimeMax > 0 && intConnectionTimeMin > 0)
                    {
                        OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsConnectionTime connectionTime = new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsConnectionTime();
                        connectionTime.Max = intConnectionTimeMax.ToString();
                        connectionTime.Min = intConnectionTimeMin.ToString();
                        originDestinationInformationTPA_Extensions.ConnectionTime = connectionTime;
                    }

                    //VendorPref por definr
                    //hceron
                    VO_VendorPref[] vo_VendorPrefs = null;// vo_BargainFinderMax_ADRQ.Vo_VendorPref;

                    if (vo_VendorPrefs != null && vo_VendorPrefs.Length > 0)
                    {
                        int iContadorAerolinea = 0;
                        OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsIncludeVendorPref[] includeVendorPrefs = new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsIncludeVendorPref[vo_VendorPrefs.Length];

                        foreach (string sOpcion in lsContadorOpciones)
                        {
                            foreach (VO_VendorPref Vo_VendorPref in vo_VendorPrefs)
                            {
                                OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsIncludeVendorPref includeVendorPref = new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsIncludeVendorPref();
                                includeVendorPref.Code = Vo_VendorPref.StrCode;
                                includeVendorPrefs.SetValue(includeVendorPref, iContadorAerolinea);
                                iContadorAerolinea++;
                            }
                        }
                        originDestinationInformationTPA_Extensions.IncludeVendorPref = includeVendorPrefs;
                    }

                    //TPA_Extensions
                    ota_AirLowFareSearchRQOriginDestinationInformation.TPA_Extensions = originDestinationInformationTPA_Extensions;

                    #endregion

                    iOriginDestinationInformation = iSegmentsCounter + 1;
                    lsContadorOpciones.Add(iOriginDestinationInformation.ToString());
                    ota_AirLowFareSearchRQOriginDestinationInformation.RPH = iOriginDestinationInformation.ToString();
                    aota_AirLowFareSearchRQOriginDestinationInformation[iSegmentsCounter] =
                        ota_AirLowFareSearchRQOriginDestinationInformation;

                    iSegmentsCounter++;
                }
                ota_AirLowFareSearchRQ.OriginDestinationInformation =
                    aota_AirLowFareSearchRQOriginDestinationInformation;
            }
            #endregion

            #region [ TRAVELPREFERENCES ]

            AirSearchPrefsType airSearchPrefsType = new AirSearchPrefsType();

            AirSearchPrefsType airSearchExcluPrefsType = new AirSearchPrefsType();
            string sMaximasParadas = vo_BargainFinderMax_ADRQ.SMaxStopsQuantity;

            if (!String.IsNullOrEmpty(sMaximasParadas))
            {
                if (!sMaximasParadas.Equals("0"))
                {
                    airSearchPrefsType.MaxStopsQuantity = sMaximasParadas;
                }
            }

            VO_CabinPref[] vo_CabinPrefs = null;//vo_BargainFinderMax_ADRQ.Vo_CabinPrefs;
            if (vo_CabinPrefs != null && vo_CabinPrefs.Length > 0)
            {
                int intCounterCabinPref = 0;
                CabinPrefType[] CabinPrefTypes = new CabinPrefType[vo_CabinPrefs.Length];
                foreach (VO_CabinPref vo_CabinPref in vo_CabinPrefs)
                {
                    CabinPrefType cabinPrefType = new CabinPrefType();
                    cabinPrefType.Cabin = (CabinType)vo_CabinPref.Enum_CabinType;
                    cabinPrefType.CabinSpecified = true;
                    cabinPrefType.PreferLevel = (PreferLevelType)vo_CabinPref.Enum_PreferLevelType;
                    CabinPrefTypes.SetValue(cabinPrefType, intCounterCabinPref);
                    intCounterCabinPref++;
                }
                airSearchPrefsType.CabinPref = CabinPrefTypes;
            }






            //VendorPref
            VO_VendorPref[] lvo_VendorPref = null;//vo_BargainFinderMax_ADRQ.Vo_VendorPref;
            if (lvo_VendorPref != null && lvo_VendorPref.Length > 0)
            {
                int intCounterVendorPref = 0;
                CompanyNamePrefType[] companyNamePrefTypes = new CompanyNamePrefType[lvo_VendorPref.Length];
                foreach (VO_VendorPref vo_VendorPref in lvo_VendorPref)
                {
                    CompanyNamePrefType companyNamePrefType = new CompanyNamePrefType();
                    companyNamePrefType.PreferLevel = (PreferLevelType)vo_VendorPref.Enum_PreferLevelType;
                    companyNamePrefType.Code = vo_VendorPref.StrCode;
                    companyNamePrefTypes.SetValue(companyNamePrefType, intCounterVendorPref);
                    intCounterVendorPref++;
                }
                airSearchPrefsType.VendorPref = companyNamePrefTypes;
            }


            //Excluir Aerolineas
            // 

            #region [ Exlude Airline ]

             if (lsExcluirAerol.Count > 0)
            {
                List<AirSearchPrefsTypeTPA_ExtensionsExcludeVendorPref> companyNameExcludeTypes = new List<AirSearchPrefsTypeTPA_ExtensionsExcludeVendorPref>();

                foreach (string sExludeCode in lsExcluirAerol)
                {
                    AirSearchPrefsTypeTPA_ExtensionsExcludeVendorPref companyNamePrefType = new AirSearchPrefsTypeTPA_ExtensionsExcludeVendorPref();
                    companyNamePrefType.Code = sExludeCode;
                    companyNameExcludeTypes.Add(companyNamePrefType);
                }

                airSearchPrefsType.TPA_Extensions = new AirSearchPrefsTypeTPA_Extensions();
                airSearchPrefsType.TPA_Extensions.ExcludeVendorPref = companyNameExcludeTypes.ToArray();
            }
            #endregion

            //TravelPreferences
            ota_AirLowFareSearchRQ.TravelPreferences = airSearchPrefsType;
            ota_AirLowFareSearchRQ.TravelPreferences.ValidInterlineTicket = true;

            #endregion

            #region [ TRAVELERINFORMATION ]

            TravelerInfoSummaryType travelerInfoSummaryType = new TravelerInfoSummaryType();
            List<VO_Pasajero> lvo_Passengers = vo_BargainFinderMax_ADRQ.Lvo_Passengers;

            if (lvo_Passengers == null)
            {
                throw new Exception("lvo_Passengers mandatory");
            }
            else
            {
                TravelerInformationType travelerInformationType = new TravelerInformationType();
             

                int iContPasajeros = 0;
                int iContPaxTotal = 0;
                foreach (VO_Pasajero vo_PasajeroTotal in lvo_Passengers)
                {
                    if (vo_PasajeroTotal.SCodigo.Equals("CNN"))
                    {
                        iContPaxTotal += int.Parse(vo_PasajeroTotal.SCantidad);
                    }
                    else
                    {
                        iContPaxTotal++;
                    }
                }
                //best to get search
                //hceron 06062013
                PassengerTypeQuantityType[] passengerTypeQuantityTypes = new PassengerTypeQuantityType[iContPaxTotal];
                #region paxes MAX


                foreach (VO_Pasajero vo_Pasajero in lvo_Passengers)
                {
                    if (vo_Pasajero.SCodigo.Equals("CNN"))
                    {
                        foreach (VO_ClasificaPasajero vo_CatPasajero in vo_Pasajero.LvPasajeroNino)
                        {
                            PassengerTypeQuantityType passengerTypeQuantityType = new PassengerTypeQuantityType();
                            passengerTypeQuantityType.Code = vo_CatPasajero.SCodigo;
                            passengerTypeQuantityType.Quantity = vo_CatPasajero.SCantidad;
                            passengerTypeQuantityTypes[iContPasajeros] = passengerTypeQuantityType;
                            iContPasajeros++;



                        }
                    }
                    else
                    {
                        PassengerTypeQuantityType passengerTypeQuantityType = new PassengerTypeQuantityType();


                        passengerTypeQuantityType.Code = vo_Pasajero.SCodigo;
                        passengerTypeQuantityType.Quantity = vo_Pasajero.SCantidad;

                        passengerTypeQuantityTypes[iContPasajeros] = passengerTypeQuantityType;
                        iContPasajeros++;

                    }
                }
                #endregion
                //foreach (VO_Pasajero vo_Passenger in lvo_Passengers)
                //{
                //    PassengerTypeQuantityType passengerTypeQuantityType = new PassengerTypeQuantityType();
                //    passengerTypeQuantityType.Code = "ADT";//vo_Passenger.;
                //    passengerTypeQuantityType.Quantity = "1";// vo_Passenger.ToString();

                //    passengerTypeQuantityTypes[iContPasajeros] = passengerTypeQuantityType;
                //    iContPasajeros++;
                //}
                travelerInformationType.PassengerTypeQuantity = passengerTypeQuantityTypes;

                TravelerInformationType[] travelerInformationTypes = new TravelerInformationType[] { travelerInformationType };
                travelerInfoSummaryType.AirTravelerAvail = travelerInformationTypes;
            }

            PriceRequestInformationType priceRequestInformationType = new PriceRequestInformationType();
            if (vo_BargainFinderMax_ADRQ.StrNegotiatedFareCode != null && vo_BargainFinderMax_ADRQ.StrNegotiatedFareCode.Length > 0)
            {
                PriceRequestInformationTypeNegotiatedFareCode[] NegotiatedFareCode = new PriceRequestInformationTypeNegotiatedFareCode[1];

                int intContadorCorporateID = 0;
                foreach (string strCorporateID in vo_BargainFinderMax_ADRQ.StrNegotiatedFareCode)
                {
                    NegotiatedFareCode[intContadorCorporateID] = new PriceRequestInformationTypeNegotiatedFareCode();
                    NegotiatedFareCode[intContadorCorporateID].Code = vo_BargainFinderMax_ADRQ.StrNegotiatedFareCode[intContadorCorporateID];
                    NegotiatedFareCode[intContadorCorporateID].Supplier = new CompanyNameType[] { new CompanyNameType() { Code = "AAA" } };
                    intContadorCorporateID++;
                }
                priceRequestInformationType.Items = NegotiatedFareCode;
            }

            if (vo_BargainFinderMax_ADRQ.Vo_Priority.isValid())
            {
                PriceRequestInformationTypeTPA_Extensions priceRequestInformationTypeTPA_Extensions = new PriceRequestInformationTypeTPA_Extensions();

                priceRequestInformationTypeTPA_Extensions.Priority = new PriceRequestInformationTypeTPA_ExtensionsPriority()
                {
                    DirectFlights = new PriceRequestInformationTypeTPA_ExtensionsPriorityDirectFlights() { Priority = vo_BargainFinderMax_ADRQ.Vo_Priority.IntDirectFlights },
                    Price = new PriceRequestInformationTypeTPA_ExtensionsPriorityPrice() { Priority = vo_BargainFinderMax_ADRQ.Vo_Priority.IntPrice },
                    Time = new PriceRequestInformationTypeTPA_ExtensionsPriorityTime() { Priority = vo_BargainFinderMax_ADRQ.Vo_Priority.IntTime },
                    Vendor = new PriceRequestInformationTypeTPA_ExtensionsPriorityVendor() { Priority = vo_BargainFinderMax_ADRQ.Vo_Priority.IntVendor }
                };

                priceRequestInformationType.TPA_Extensions = priceRequestInformationTypeTPA_Extensions;
            }

            PriceRequestInformationTypeTPA_Extensions priceRequestInformationTypeTPA_Extensions_tkt = new PriceRequestInformationTypeTPA_Extensions();
            priceRequestInformationTypeTPA_Extensions_tkt.Indicators = new PriceRequestInformationTypeTPA_ExtensionsIndicators()
            {
                ResTicketing = new PriceRequestInformationTypeTPA_ExtensionsIndicatorsResTicketing() { Ind = true }
            };
            priceRequestInformationType.TPA_Extensions = priceRequestInformationTypeTPA_Extensions_tkt;


            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            if (vo_BargainFinderMax_ADRQ != null)
            {
                priceRequestInformationType.CurrencyCode = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString();
                travelerInfoSummaryType.PriceRequestInformation = priceRequestInformationType;
            }
            else
            {
                travelerInfoSummaryType.PriceRequestInformation = priceRequestInformationType;
            }
            
            ota_AirLowFareSearchRQ.TravelerInfoSummary = travelerInfoSummaryType;

            #endregion

            OTA_AirLowFareSearchRQTPA_Extensions ota_AirLowFareSearchRQTPA_Extensions = new OTA_AirLowFareSearchRQTPA_Extensions();
            TransactionType transactionType = new TransactionType();
            TransactionTypeClientSessionID transactionTypeClientSessionID = new TransactionTypeClientSessionID();
            transactionTypeClientSessionID.Value = session_;
            transactionType.ClientSessionID = transactionTypeClientSessionID;

            TransactionTypeRequestType transactionTypeRequestType = new TransactionTypeRequestType();
            transactionTypeRequestType.Name = str200;
            switch (vo_BargainFinderMax_ADRQ.Enum_IntelliSellTransaction)
            {
                case Enum_IntelliSellTransaction.BFM_50:
                    transactionTypeRequestType.Name = str50;
                    break;
                case Enum_IntelliSellTransaction.BFM_100:
                    transactionTypeRequestType.Name = str100;
                    break;
                case Enum_IntelliSellTransaction.BFM_200:
                    transactionTypeRequestType.Name = str200;
                    break;
            }

            transactionType.RequestType = transactionTypeRequestType;
            ota_AirLowFareSearchRQTPA_Extensions.IntelliSellTransaction = transactionType;

            ota_AirLowFareSearchRQ.TPA_Extensions = ota_AirLowFareSearchRQTPA_Extensions;

            BargainFinderMaxService bargainFinderMaxService = new BargainFinderMaxService();

            SWS_BargainFinderMaxRQ.MessageHeader messageHeader =
                  (SWS_BargainFinderMaxRQ.MessageHeader)getMessageHeader(typeof(MessageHeader), vo_BargainFinderMax_ADRQ.Vo_MessageHeader);
            bargainFinderMaxService.MessageHeaderValue = messageHeader;
            bargainFinderMaxService.SecurityValue = security;
            OTA_AirLowFareSearchRS ota_AirLowFareSearchRS = bargainFinderMaxService.BargainFinderMaxRQ(ota_AirLowFareSearchRQ);

            //XmlSerializer mySerializer = new XmlSerializer(typeof(OTA_AirLowFareSearchRQ));
            ////To write to a file, create a StreamWriter object.
            //System.IO.StreamWriter myWriter = new System.IO.StreamWriter("D://bfmRQ-Integradov5" + DateTime.Now.Hour + DateTime.Now.Minute + ".xml");
            //mySerializer.Serialize(myWriter, ota_AirLowFareSearchRQ);
            //myWriter.Close();

            //mySerializer = new XmlSerializer(typeof(OTA_AirLowFareSearchRS));
            ////To write to a file, create a StreamWriter object.
            //myWriter = new System.IO.StreamWriter("D://bfmRS-Integradov6" + DateTime.Now.Hour + DateTime.Now.Minute + ".xml");
            //mySerializer.Serialize(myWriter, ota_AirLowFareSearchRS);
            //myWriter.Close();

            return ota_AirLowFareSearchRS;
        }
    }
}
