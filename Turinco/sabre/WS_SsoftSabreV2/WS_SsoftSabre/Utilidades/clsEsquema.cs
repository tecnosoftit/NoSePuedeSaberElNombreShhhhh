using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Schema;
using WS_SsoftSabre.OTA_AirLowFareSearch;
using WS_SsoftSabre.OTA_AirAvail;
using WS_SsoftSabre.OTA_AirBook;
using WS_SsoftSabre.OTA_AirPrice;
using WS_SsoftSabre.DisplayPriceQuote;
using WS_SsoftSabre.ShortSell;
using WS_SsoftSabre.OTA_TravelItineraryRead;
using Ssoft.ManejadorExcepciones;
//using Ssoft.Utils;
using Ssoft.ValueObjects;
using WS_SsoftSabre.DesignatePrinterRQ;
using WS_SsoftSabre.AirTicketRQ;

namespace WS_SsoftSabre.Utilidades
{
    public class clsEsquema
    {
        public static DataSet GetDatasetSabreAir(OTA_AirLowFareSearchRS objOTA_AirLowFareSearchRS)
        {
            /*CREAMOS EL SERIALIZADOR DEL OBJETO*/
            XmlSerializer xmlSerial = new XmlSerializer(typeof(OTA_AirLowFareSearchRS));
            StringWriter sWriter = new StringWriter();
            xmlSerial.Serialize(sWriter, objOTA_AirLowFareSearchRS);
            XmlDocument xmlDocumento = new XmlDocument();
            /*AGREGAMOS EL STRING DEL OBJETO SERIALIZADO A UN DOCUMENTO XML */
            xmlDocumento.LoadXml(sWriter.ToString());
            DataSet dsDataset = new DataSet();
            /*LEEMOS EL DOCUMENTO  XML Y LO AGREGAMOS AL LECTOR XML*/
            XmlTextReader txtReader = new XmlTextReader(new StringReader(xmlDocumento.OuterXml));

            try
            {
                dsDataset.ReadXmlSchema(Get_XSD_Clase(typeof(OTA_AirLowFareSearchRS), "http://webservices.sabre.com/sabreXML/2003/07"));
                /*LEEMOS EL XML Y LO AGREGAMOS AL DATASET*/
                try { dsDataset.ReadXml(txtReader, XmlReadMode.Auto); }
                catch { }
                /*CERRAMO EL LECTOR DEL XML*/
                txtReader.Close();
                dsDataset.Dispose();
                /*ACEPTAMOS L0S CAMBIOS EN EL DATASET*/
                dsDataset.AcceptChanges();
                /*RETORNAMOS EL DATASET*/
                return dsDataset;
            }
            catch (Exception Ex)
            {
                /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
                txtReader.Close();
                return null;
            }
        }

        /// <summary>
        /// Esquema del BFM
        /// hceron
        /// </summary>
        /// <param name="objOTA_AirLowFareSearchRS"></param>
        /// <returns></returns>
        public static DataSet GetDatasetSabreAirMax(WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRS objOTA_AirLowFareSearchRS)
        {
            /*CREAMOS EL SERIALIZADOR DEL OBJETO*/
            XmlSerializer xmlSerial = new XmlSerializer(typeof(WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRS));
            StringWriter sWriter = new StringWriter();
            xmlSerial.Serialize(sWriter, objOTA_AirLowFareSearchRS);
            XmlDocument xmlDocumento = new XmlDocument();
            /*AGREGAMOS EL STRING DEL OBJETO SERIALIZADO A UN DOCUMENTO XML */
            xmlDocumento.LoadXml(sWriter.ToString());
            DataSet dsDataset = new DataSet();
            /*LEEMOS EL DOCUMENTO  XML Y LO AGREGAMOS AL LECTOR XML*/
            XmlTextReader txtReader = new XmlTextReader(new StringReader(xmlDocumento.OuterXml));

            try
            {
                dsDataset.ReadXmlSchema(Get_XSD_Clase(typeof(WS_SsoftSabre.SWS_BargainFinderMaxRQ.OTA_AirLowFareSearchRS), "http://www.opentravel.org/OTA/2003/05"));//"http://webservices.sabre.com/sabreXML/2003/07"));
                /*LEEMOS EL XML Y LO AGREGAMOS AL DATASET*/
                dsDataset.EnforceConstraints = false;
                try { dsDataset.ReadXml(txtReader, XmlReadMode.Auto); }
                catch { }
                /*CERRAMO EL LECTOR DEL XML*/
                txtReader.Close();
                dsDataset.Dispose();
                /*ACEPTAMOS L0S CAMBIOS EN EL DATASET*/
                dsDataset.AcceptChanges();
                /*RETORNAMOS EL DATASET*/
                return dsDataset;
            }
            catch (Exception Ex)
            {
                /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
                txtReader.Close();
                return null;
            }
        }


        public static DataSet GetDatasetSabreAir(OTA_AirAvailRS objOTA_AirLowFareSearchRS)
        {
            /*CREAMOS EL SERIALIZADOR DEL OBJETO*/
            XmlSerializer xmlSerial = new XmlSerializer(typeof(OTA_AirAvailRS));
            StringWriter sWriter = new StringWriter();
            xmlSerial.Serialize(sWriter, objOTA_AirLowFareSearchRS);
            XmlDocument xmlDocumento = new XmlDocument();
            /*AGREGAMOS EL STRING DEL OBJETO SERIALIZADO A UN DOCUMENTO XML */
            xmlDocumento.LoadXml(sWriter.ToString());
            DataSet dsDataset = new DataSet();
            /*LEEMOS EL DOCUMENTO  XML Y LO AGREGAMOS AL LECTOR XML*/
            XmlTextReader txtReader = new XmlTextReader(new StringReader(xmlDocumento.OuterXml));

            try
            {
                dsDataset.ReadXmlSchema(Get_XSD_Clase(typeof(OTA_AirAvailRS), "http://webservices.sabre.com/sabreXML/2003/07"));
                /*LEEMOS EL XML Y LO AGREGAMOS AL DATASET*/
                try { dsDataset.ReadXml(txtReader, XmlReadMode.Auto); }
                catch { }
                /*CERRAMO EL LECTOR DEL XML*/
                txtReader.Close();
                dsDataset.Dispose();
                /*ACEPTAMOS L0S CAMBIOS EN EL DATASET*/
                dsDataset.AcceptChanges();
                /*RETORNAMOS EL DATASET*/
                return dsDataset;
            }
            catch (Exception Ex)
            {
                /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
                txtReader.Close();
                return null;
            }
        }
        public static DataSet GetDatasetSabreAir(OTA_AirPriceRS objOTA_AirLowFareSearchRS)
        {
            /*CREAMOS EL SERIALIZADOR DEL OBJETO*/
            XmlSerializer xmlSerial = new XmlSerializer(typeof(OTA_AirPriceRS));
            StringWriter sWriter = new StringWriter();
            xmlSerial.Serialize(sWriter, objOTA_AirLowFareSearchRS);
            XmlDocument xmlDocumento = new XmlDocument();
            /*AGREGAMOS EL STRING DEL OBJETO SERIALIZADO A UN DOCUMENTO XML */
            xmlDocumento.LoadXml(sWriter.ToString());
            DataSet dsDataset = new DataSet();
            /*LEEMOS EL DOCUMENTO  XML Y LO AGREGAMOS AL LECTOR XML*/
            XmlTextReader txtReader = new XmlTextReader(new StringReader(xmlDocumento.OuterXml));

            try
            {
                dsDataset.ReadXmlSchema(Get_XSD_Clase(typeof(OTA_AirPriceRS), "http://webservices.sabre.com/sabreXML/2003/07"));
                /*LEEMOS EL XML Y LO AGREGAMOS AL DATASET*/
                try { dsDataset.ReadXml(txtReader, XmlReadMode.Auto); }
                catch { }
                /*CERRAMO EL LECTOR DEL XML*/
                txtReader.Close();
                dsDataset.Dispose();
                /*ACEPTAMOS L0S CAMBIOS EN EL DATASET*/
                dsDataset.AcceptChanges();
                /*RETORNAMOS EL DATASET*/
                return dsDataset;
            }
            catch (Exception Ex)
            {
                /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
                txtReader.Close();
                return null;
            }
        }
        public static DataSet GetDatasetSabreAir(OTA_AirBookRS objOTA_AirLowFareSearchRS)
        {
            /*CREAMOS EL SERIALIZADOR DEL OBJETO*/
            XmlSerializer xmlSerial = new XmlSerializer(typeof(OTA_AirBookRS));
            StringWriter sWriter = new StringWriter();
            xmlSerial.Serialize(sWriter, objOTA_AirLowFareSearchRS);
            XmlDocument xmlDocumento = new XmlDocument();
            /*AGREGAMOS EL STRING DEL OBJETO SERIALIZADO A UN DOCUMENTO XML */
            xmlDocumento.LoadXml(sWriter.ToString());
            DataSet dsDataset = new DataSet();
            /*LEEMOS EL DOCUMENTO  XML Y LO AGREGAMOS AL LECTOR XML*/
            XmlTextReader txtReader = new XmlTextReader(new StringReader(xmlDocumento.OuterXml));

            try
            {
                dsDataset.ReadXmlSchema(Get_XSD_Clase(typeof(OTA_AirBookRS), "http://webservices.sabre.com/sabreXML/2003/07"));
                /*LEEMOS EL XML Y LO AGREGAMOS AL DATASET*/
                try { dsDataset.ReadXml(txtReader, XmlReadMode.Auto); }
                catch { }
                /*CERRAMO EL LECTOR DEL XML*/
                txtReader.Close();
                dsDataset.Dispose();
                /*ACEPTAMOS L0S CAMBIOS EN EL DATASET*/
                dsDataset.AcceptChanges();
                /*RETORNAMOS EL DATASET*/
                return dsDataset;
            }
            catch (Exception Ex)
            {
                /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
                txtReader.Close();
                return null;
            }
        }
        public static DataSet GetDatasetSabreAir(ShortSellRS objOTA_AirLowFareSearchRS)
        {
            /*CREAMOS EL SERIALIZADOR DEL OBJETO*/
            XmlSerializer xmlSerial = new XmlSerializer(typeof(ShortSellRS));
            StringWriter sWriter = new StringWriter();
            xmlSerial.Serialize(sWriter, objOTA_AirLowFareSearchRS);
            XmlDocument xmlDocumento = new XmlDocument();
            /*AGREGAMOS EL STRING DEL OBJETO SERIALIZADO A UN DOCUMENTO XML */
            xmlDocumento.LoadXml(sWriter.ToString());
            DataSet dsDataset = new DataSet();
            /*LEEMOS EL DOCUMENTO  XML Y LO AGREGAMOS AL LECTOR XML*/
            XmlTextReader txtReader = new XmlTextReader(new StringReader(xmlDocumento.OuterXml));

            try
            {
                dsDataset.ReadXmlSchema(Get_XSD_Clase(typeof(ShortSellRS), "http://webservices.sabre.com/sabreXML/2003/07"));
                /*LEEMOS EL XML Y LO AGREGAMOS AL DATASET*/
                try { dsDataset.ReadXml(txtReader, XmlReadMode.Auto); }
                catch { }
                /*CERRAMO EL LECTOR DEL XML*/
                txtReader.Close();
                dsDataset.Dispose();
                /*ACEPTAMOS L0S CAMBIOS EN EL DATASET*/
                dsDataset.AcceptChanges();
                /*RETORNAMOS EL DATASET*/
                return dsDataset;
            }
            catch (Exception Ex)
            {
                /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
                txtReader.Close();
                return null;
            }
        }
        public static DataSet GetDatasetSabreAir(OTA_TravelItineraryRS objOTA_TravelItineraryRS)
        {
            /*CREAMOS EL SERIALIZADOR DEL OBJETO*/
            XmlSerializer xmlSerial = new XmlSerializer(typeof(OTA_TravelItineraryRS));
            StringWriter sWriter = new StringWriter();
            xmlSerial.Serialize(sWriter, objOTA_TravelItineraryRS);
            XmlDocument xmlDocumento = new XmlDocument();
            /*AGREGAMOS EL STRING DEL OBJETO SERIALIZADO A UN DOCUMENTO XML */
            xmlDocumento.LoadXml(sWriter.ToString());
            DataSet dsDataset = new DataSet();
            /*LEEMOS EL DOCUMENTO  XML Y LO AGREGAMOS AL LECTOR XML*/
            XmlTextReader txtReader = new XmlTextReader(new StringReader(xmlDocumento.OuterXml));

            try
            {
                dsDataset.ReadXmlSchema(Get_XSD_Clase(typeof(OTA_TravelItineraryRS), "http://webservices.sabre.com/sabreXML/2003/07"));
                /*LEEMOS EL XML Y LO AGREGAMOS AL DATASET*/
                try { dsDataset.ReadXml(txtReader, XmlReadMode.Auto); }
                catch { }
                /*CERRAMO EL LECTOR DEL XML*/
                txtReader.Close();
                dsDataset.Dispose();
                /*ACEPTAMOS L0S CAMBIOS EN EL DATASET*/
                dsDataset.AcceptChanges();
                /*RETORNAMOS EL DATASET*/
                return dsDataset;
            }
            catch (Exception Ex)
            {
                /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
                txtReader.Close();
                return null;
            }
        }
        public static DataSet GetDatasetSabreAir(DisplayPriceQuoteRS objDisplayPriceQuoteRS)
        {
            /*CREAMOS EL SERIALIZADOR DEL OBJETO*/
            XmlSerializer xmlSerial = new XmlSerializer(typeof(DisplayPriceQuoteRS));
            StringWriter sWriter = new StringWriter();
            xmlSerial.Serialize(sWriter, objDisplayPriceQuoteRS);
            XmlDocument xmlDocumento = new XmlDocument();
            /*AGREGAMOS EL STRING DEL OBJETO SERIALIZADO A UN DOCUMENTO XML */
            xmlDocumento.LoadXml(sWriter.ToString());
            DataSet dsDataset = new DataSet();
            /*LEEMOS EL DOCUMENTO  XML Y LO AGREGAMOS AL LECTOR XML*/
            XmlTextReader txtReader = new XmlTextReader(new StringReader(xmlDocumento.OuterXml));

            try
            {
                dsDataset.ReadXmlSchema(Get_XSD_Clase(typeof(DisplayPriceQuoteRS), "http://webservices.sabre.com/sabreXML/2003/07"));
                /*LEEMOS EL XML Y LO AGREGAMOS AL DATASET*/
                try { dsDataset.ReadXml(txtReader, XmlReadMode.Auto); }
                catch { }
                /*CERRAMO EL LECTOR DEL XML*/
                txtReader.Close();
                dsDataset.Dispose();
                /*ACEPTAMOS L0S CAMBIOS EN EL DATASET*/
                dsDataset.AcceptChanges();
                /*RETORNAMOS EL DATASET*/
                return dsDataset;
            }
            catch (Exception Ex)
            {
                /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
                txtReader.Close();
                return null;
            }
        }
        public static DataSet GetDatasetSabreAir(DesignatePrinterRS objDesignatePrinterRS)
        {
            /*CREAMOS EL SERIALIZADOR DEL OBJETO*/
            XmlSerializer xmlSerial = new XmlSerializer(typeof(DesignatePrinterRS));
            StringWriter sWriter = new StringWriter();
            xmlSerial.Serialize(sWriter, objDesignatePrinterRS);
            XmlDocument xmlDocumento = new XmlDocument();
            /*AGREGAMOS EL STRING DEL OBJETO SERIALIZADO A UN DOCUMENTO XML */
            xmlDocumento.LoadXml(sWriter.ToString());
            DataSet dsDataset = new DataSet();
            /*LEEMOS EL DOCUMENTO  XML Y LO AGREGAMOS AL LECTOR XML*/
            XmlTextReader txtReader = new XmlTextReader(new StringReader(xmlDocumento.OuterXml));

            try
            {
                dsDataset.ReadXmlSchema(Get_XSD_Clase(typeof(DesignatePrinterRS), "http://webservices.sabre.com/sabreXML/2003/07"));
                /*LEEMOS EL XML Y LO AGREGAMOS AL DATASET*/
                try { dsDataset.ReadXml(txtReader, XmlReadMode.Auto); }
                catch { }
                /*CERRAMO EL LECTOR DEL XML*/
                txtReader.Close();
                dsDataset.Dispose();
                /*ACEPTAMOS L0S CAMBIOS EN EL DATASET*/
                dsDataset.AcceptChanges();
                /*RETORNAMOS EL DATASET*/
                return dsDataset;
            }
            catch (Exception Ex)
            {
                /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
                txtReader.Close();
                return null;
            }
        }
        public static DataSet GetDatasetSabreAir(AirTicketRS objAirTicketRS)
        {
            /*CREAMOS EL SERIALIZADOR DEL OBJETO*/
            XmlSerializer xmlSerial = new XmlSerializer(typeof(AirTicketRS));
            StringWriter sWriter = new StringWriter();
            xmlSerial.Serialize(sWriter, objAirTicketRS);
            XmlDocument xmlDocumento = new XmlDocument();
            /*AGREGAMOS EL STRING DEL OBJETO SERIALIZADO A UN DOCUMENTO XML */
            xmlDocumento.LoadXml(sWriter.ToString());
            DataSet dsDataset = new DataSet();
            /*LEEMOS EL DOCUMENTO  XML Y LO AGREGAMOS AL LECTOR XML*/
            XmlTextReader txtReader = new XmlTextReader(new StringReader(xmlDocumento.OuterXml));

            try
            {
                dsDataset.ReadXmlSchema(Get_XSD_Clase(typeof(AirTicketRS), "http://webservices.sabre.com/sabreXML/2003/07"));
                /*LEEMOS EL XML Y LO AGREGAMOS AL DATASET*/
                try { dsDataset.ReadXml(txtReader, XmlReadMode.Auto); }
                catch { }
                /*CERRAMO EL LECTOR DEL XML*/
                txtReader.Close();
                dsDataset.Dispose();
                /*ACEPTAMOS L0S CAMBIOS EN EL DATASET*/
                dsDataset.AcceptChanges();
                /*RETORNAMOS EL DATASET*/
                return dsDataset;
            }
            catch (Exception Ex)
            {
                /*SI OCURRE UNA EXCEPCION CUALQUIERA*/
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(cParametros);
                txtReader.Close();
                return null;
            }
        }
        public static XmlTextReader Get_XSD_Clase(Type Tipo_Searializar, String strNamespace)
        {
            XmlReflectionImporter importer = new XmlReflectionImporter();
            XmlSchemas schemas = new XmlSchemas();
            XmlSchemaExporter exporter = new XmlSchemaExporter(schemas);            

            XmlTypeMapping mapping = importer.ImportTypeMapping(Tipo_Searializar, strNamespace);
            exporter.ExportTypeMapping(mapping);
            XmlSchema schema;
            if (!String.IsNullOrEmpty(strNamespace))
                schema = schemas[strNamespace];
            else
                schema = schemas[0];
            StringWriter sWriter = new StringWriter();
            schema.Write(sWriter);
            XmlTextReader txtReader = new XmlTextReader(new StringReader(sWriter.ToString()));
            return txtReader;
        }
        //public static PricedItineraries GetClassSabreAir(OTA_AirLowFareSearchRS objOTA_AirLowFareSearchRS)
        //{
        //    PricedItineraries oPricedItineraries = new PricedItineraries();
        //    PricedItinerariesPricedItinerary[] oPricedItinerarys = new PricedItinerariesPricedItinerary[objOTA_AirLowFareSearchRS.PricedItineraries.Length];
        //    try
        //    {
        //        int iItinerary = 0;
        //        foreach (OTA_AirLowFareSearchRSPricedItinerary drItinerario in objOTA_AirLowFareSearchRS.PricedItineraries)
        //        {
        //            PricedItinerariesPricedItinerary oPricedItinerary = new PricedItinerariesPricedItinerary();
        //            oPricedItinerary.SequenceNumber = drItinerario.SequenceNumber;

        //            PricedItinerariesPricedItineraryAirItinerary oAirItinerary = new PricedItinerariesPricedItineraryAirItinerary();
        //            PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptions oOriginDestinationOptions = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptions();
        //            PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegment[] oFlightSegments = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegment[drItinerario.AirItinerary.OriginDestinationOptions.OriginDestinationOption.Length];
        //            int iDestination = 0;
        //            foreach (FlightSegmentType drOriginDestinationOption in objOTA_AirLowFareSearchRS.PricedItineraries[iItinerary].AirItinerary.OriginDestinationOptions.OriginDestinationOption)
        //            {
        //                PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegment oOriginDestinationOption = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegment();

        //                PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentArrivalAirport oFlightSegmentArrivalAirport = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentArrivalAirport();
        //                PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentDepartureAirport oFlightSegmentDepartureAirport = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentDepartureAirport();
        //                PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentEquipment oFlightSegmentEquipment = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentEquipment();
        //                PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentMarketingAirline oFlightSegmentMarketingAirline = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentMarketingAirline();
        //                PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentMarketingCabin oFlightSegmentMarketingCabin = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentMarketingCabin();
        //                PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentOperatingAirline oFlightSegmentOperatingAirline = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentOperatingAirline();

        //                oFlightSegmentArrivalAirport.CodeContext = drOriginDestinationOption.ArrivalAirport.CodeContext;
        //                oFlightSegmentArrivalAirport.LocationCode = drOriginDestinationOption.ArrivalAirport.LocationCode;
        //                oFlightSegmentArrivalAirport.LocationName = "NameAirPort";
        //                oOriginDestinationOption.ArrivalDateTime = drOriginDestinationOption.ArrivalDateTime;

        //                oFlightSegmentDepartureAirport.CodeContext = drOriginDestinationOption.DepartureAirport.CodeContext;
        //                oFlightSegmentDepartureAirport.LocationCode = drOriginDestinationOption.DepartureAirport.LocationCode;
        //                oFlightSegmentDepartureAirport.LocationName = "NameAirPort";
        //                oOriginDestinationOption.DepartureDateTime = drOriginDestinationOption.DepartureDateTime;

        //                oFlightSegmentEquipment.AirEquipType = drOriginDestinationOption.Equipment.AirEquipType;

        //                oFlightSegmentMarketingAirline.Code = drOriginDestinationOption.MarketingAirline.Code;
        //                oFlightSegmentMarketingAirline.Name = "NameAirLine";
        //                oFlightSegmentMarketingCabin.CabinType = drOriginDestinationOption.MarketingCabin.CabinType;
        //                oFlightSegmentMarketingCabin.CabinName = "NameCabin";

        //                oFlightSegmentOperatingAirline.Code = drOriginDestinationOption.OperatingAirline.Code;
        //                oFlightSegmentOperatingAirline.Name = "NameAirlineOperation";

        //                oOriginDestinationOption.ElapsedTime = drOriginDestinationOption.ElapsedTime;
        //                oOriginDestinationOption.FlightNumber = drOriginDestinationOption.FlightNumber;
        //                oOriginDestinationOption.ResBookDesigCode = drOriginDestinationOption.ResBookDesigCode;
        //                oOriginDestinationOption.RPH = drOriginDestinationOption.RPH;
        //                oOriginDestinationOption.StopQuantity = drOriginDestinationOption.StopQuantity;
        //                oOriginDestinationOption.MarriageGrp = drOriginDestinationOption.MarriageGrp;

        //                oOriginDestinationOption.ArrivalAirport = oFlightSegmentArrivalAirport;
        //                oOriginDestinationOption.DepartureAirport = oFlightSegmentDepartureAirport;
        //                oOriginDestinationOption.Equipment = oFlightSegmentEquipment;
        //                oOriginDestinationOption.MarketingAirline = oFlightSegmentMarketingAirline;
        //                oOriginDestinationOption.MarketingCabin = oFlightSegmentMarketingCabin;
        //                oOriginDestinationOption.OperatingAirline = oFlightSegmentOperatingAirline;

        //                oFlightSegments[iDestination] = oOriginDestinationOption;
        //                iDestination++;
        //            }
        //            oOriginDestinationOptions.OriginDestinationOption = oFlightSegments;
        //            oAirItinerary.OriginDestinationOptions = oOriginDestinationOptions;
        //            oPricedItinerary.AirItinerary = oAirItinerary;

        //            PricedItinerariesPricedItineraryAirItineraryPricingInfo oPricingInfo = new PricedItinerariesPricedItineraryAirItineraryPricingInfo();
        //            PricedItinerariesPricedItineraryAirItineraryPricingInfoItinTotalFare oPricingInfoItinTotalFare = new PricedItinerariesPricedItineraryAirItineraryPricingInfoItinTotalFare();
        //            PricedItinerariesPricedItineraryAirItineraryPricingInfoItinTotalFareTotalFare oPricingInfoItinTotalFareTotalFare = new PricedItinerariesPricedItineraryAirItineraryPricingInfoItinTotalFareTotalFare();

        //            oPricingInfoItinTotalFareTotalFare.Amount = drItinerario.AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount;
        //            oPricingInfoItinTotalFareTotalFare.CurrencyCode = drItinerario.AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode;
        //            oPricingInfoItinTotalFareTotalFare.DecimalPlaces = drItinerario.AirItineraryPricingInfo.ItinTotalFare.TotalFare.DecimalPlaces;

        //            oPricingInfoItinTotalFare.TotalFare = oPricingInfoItinTotalFareTotalFare;
        //            oPricingInfo.ItinTotalFare = oPricingInfoItinTotalFare;

        //            PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdown[] oPricingInfoPTC_FareBreakdowns = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdown[objOTA_AirLowFareSearchRS.PricedItineraries[iItinerary].AirItineraryPricingInfo.PTC_FareInfo.PTC_FareBreakdown.Length];
        //            int iRuleInfo = 0;
        //            foreach (OTA_AirLowFareSearchRSPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdown drFareInfoFareBreakdown in objOTA_AirLowFareSearchRS.PricedItineraries[iItinerary].AirItineraryPricingInfo.PTC_FareInfo.PTC_FareBreakdown)
        //            {
        //                PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdown oPricingInfoPTC_FareBreakdown = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdown();
        //                PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFare oPassengerFare = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFare();
        //                PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareBaseFare oPassengerFareBaseFare = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareBaseFare();
        //                PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareEquivFare oPassengerFareEquivFare = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareEquivFare();
        //                PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareTotalFare oPassengerFareTotalFare = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareTotalFare();
        //                PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareTPA_Extensions oPassengerFareTPA_Extensions = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareTPA_Extensions();
        //                PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareTPA_ExtensionsValidatingCarrier oPassengerFareTPA_ExtensionsValidatingCarrier = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareTPA_ExtensionsValidatingCarrier();

        //                oPassengerFareBaseFare.Amount = drFareInfoFareBreakdown.PassengerFare.BaseFare.Amount;
        //                oPassengerFareBaseFare.CurrencyCode = drFareInfoFareBreakdown.PassengerFare.BaseFare.CurrencyCode;
        //                oPassengerFareBaseFare.DecimalPlaces = drFareInfoFareBreakdown.PassengerFare.BaseFare.DecimalPlaces;
        //                try
        //                {
        //                    if (drFareInfoFareBreakdown.PassengerFare.EquivFare != null)
        //                    {
        //                        oPassengerFareEquivFare.Amount = drFareInfoFareBreakdown.PassengerFare.EquivFare.Amount;
        //                        oPassengerFareEquivFare.CurrencyCode = drFareInfoFareBreakdown.PassengerFare.EquivFare.CurrencyCode;
        //                        oPassengerFareEquivFare.DecimalPlaces = drFareInfoFareBreakdown.PassengerFare.EquivFare.DecimalPlaces;
        //                    }
        //                }
        //                catch { }

        //                oPassengerFareTotalFare.Amount = drFareInfoFareBreakdown.PassengerFare.TotalFare.Amount;
        //                oPassengerFareTotalFare.CurrencyCode = drFareInfoFareBreakdown.PassengerFare.TotalFare.CurrencyCode;
        //                oPassengerFareTotalFare.DecimalPlaces = drFareInfoFareBreakdown.PassengerFare.TotalFare.DecimalPlaces;

        //                PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareTax[] oPassengerFareTaxes = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareTax[drFareInfoFareBreakdown.PassengerFare.Taxes.Length];
        //                int iTax = 0;
        //                foreach (OTA_AirLowFareSearchRSPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareTax drPassengerFareTax in drFareInfoFareBreakdown.PassengerFare.Taxes)
        //                {
        //                    PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareTax oPassengerFareTax = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerFareTax();
        //                    oPassengerFareTax.Amount = drPassengerFareTax.Amount;
        //                    oPassengerFareTax.CurrencyCode = drPassengerFareTax.CurrencyCode;
        //                    oPassengerFareTax.DecimalPlaces = drPassengerFareTax.DecimalPlaces;
        //                    oPassengerFareTax.TaxCode = drPassengerFareTax.TaxCode;
        //                    oPassengerFareTax.TaxName = "TaxName";
        //                    oPassengerFareTaxes[iTax] = oPassengerFareTax;
        //                    iTax++;
        //                }
        //                int iQuantity = 0;
        //                PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerTypeQuantity[] oPassengerTypeQuantitys = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerTypeQuantity[drFareInfoFareBreakdown.PassengerTypeQuantity.Length];
        //                foreach (OTA_AirLowFareSearchRSPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerTypeQuantity drPassengerFareQuantity in drFareInfoFareBreakdown.PassengerTypeQuantity)
        //                {
        //                    PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerTypeQuantity oPassengerTypeQuantity = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfoPTC_FareBreakdownPassengerTypeQuantity();
        //                    oPassengerTypeQuantity.Code = drPassengerFareQuantity.Code;
        //                    oPassengerTypeQuantity.Quantity = drPassengerFareQuantity.Quantity;
        //                    oPassengerTypeQuantitys[iQuantity] = oPassengerTypeQuantity;
        //                    iQuantity++;
        //                }
        //                oPassengerFare.BaseFare = oPassengerFareBaseFare;
        //                oPassengerFare.EquivFare = oPassengerFareEquivFare;
        //                oPassengerFare.Taxes = oPassengerFareTaxes;
        //                oPassengerFare.TotalFare = oPassengerFareTotalFare;

        //                oPricingInfoPTC_FareBreakdown.PassengerFare = oPassengerFare;
        //                //oPricingInfoPTC_FareBreakdown.PassengerTypeQuantity = oPassengerTypeQuantitys;

        //                oPricingInfoPTC_FareBreakdowns[iRuleInfo] = oPricingInfoPTC_FareBreakdown;
        //                iRuleInfo++;
        //            }
        //            oPricedItinerary.AirItineraryPricingInfo = oPricingInfo;
        //            oPricedItinerarys[iItinerary] = oPricedItinerary;
        //            iItinerary++;
        //        }
        //        oPricedItineraries.PricedItinerary = oPricedItinerarys;
        //        //PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_Extensions oFlightSegmentTPA_Extensions = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_Extensions();
        //        //PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsArrivalTimeZone oFlightSegmentTPA_ExtensionsArrivalTimeZone = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsArrivalTimeZone();
        //        //PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsConnectionIndicator oFlightSegmentTPA_ExtensionsConnectionIndicator = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsConnectionIndicator();
        //        //PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsDepartureTimeZone oFlightSegmentTPA_ExtensionsDepartureTimeZone = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsDepartureTimeZone();
        //        //PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsETicket oFlightSegmentTPA_ExtensionsETicket = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsETicket();
        //        //PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsIntermediatePointInfo oFlightSegmentTPA_ExtensionsIntermediatePointInfo = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsIntermediatePointInfo();
        //        //PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsIntermediatePointInfoDateTime oFlightSegmentTPA_ExtensionsIntermediatePointInfoDateTime = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsIntermediatePointInfoDateTime();
        //        //PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsIntermediatePointInfoIntermediatePointTimeZone oFlightSegmentTPA_ExtensionsIntermediatePointInfoIntermediatePointTimeZone = new PricedItinerariesPricedItineraryAirItineraryOriginDestinationOptionsFlightSegmentTPA_ExtensionsIntermediatePointInfoIntermediatePointTimeZone();

        //        //PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfo oFareInfoPTC_FareInfo = new PricedItinerariesPricedItineraryAirItineraryPricingInfoPTC_FareInfo();




        //    }
        //    catch { }

        //    return oPricedItineraries;

        //}
    }
}
