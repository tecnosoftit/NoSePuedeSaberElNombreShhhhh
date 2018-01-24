using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using Ssoft.ManejadorExcepciones;
using Ssoft.Sql;
using System.IO;
using System.Xml.Serialization;
using System.Web;
using System.Configuration;
using System.Xml;
using Ssoft.ValueObjects;
using System.Xml.Xsl;
using System.Xml.XPath;
using Ssoft.DataNet;

namespace Ssoft.Utils
{
    public class clsSerializer
    {
        public clsSerializer()
        {
        }
        public void EstructuraXML(clsEstructura cEstructura, string strArchivoXML)
        {
            string strPathXML = clsValidaciones.CacheTempCrea();
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(clsEstructura));
            StreamWriter WriterRQ = new StreamWriter(strPathXML + strArchivoXML + ".xml");
            try
            {
                SerializerRQ.Serialize(WriterRQ, cEstructura);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public void DatasetXML(DataSet dsDataset, string strArchivoXML)
        {
            string strPathXML = clsValidaciones.CacheTempCrea();
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(DataSet));
            StreamWriter WriterRQ = new StreamWriter(strPathXML + strArchivoXML + ".xml");
            try
            {
                SerializerRQ.Serialize(WriterRQ, dsDataset);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public void DatasetXML(DataTable dtDataTable, string strArchivoXML)
        {
            string strPathXML = clsValidaciones.CacheTempCrea();
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(DataTable));
            StreamWriter WriterRQ = new StreamWriter(strPathXML + strArchivoXML + ".xml");
            try
            {
                SerializerRQ.Serialize(WriterRQ, dtDataTable);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public DataSet XMLDataset(string strArchivoXML)
        {
            DataSet dsDataset = new DataSet();
            string strPathXML = clsValidaciones.CacheTempCrea();
            TextReader txtReader = new StreamReader(strPathXML + strArchivoXML + ".xml");
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(DataSet));
                dsDataset = (DataSet)SerializerRS.Deserialize(txtReader);

                txtReader.Close();
                txtReader.Dispose();

                return dsDataset;
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
                return null;
            }
        }
        #region Creacion de un dataset desde un schema
        public DataSet XMLDatasetXsd(string sInnerText, string sRutaScheme)
        {
            clsHttpZipper cConvert = new clsHttpZipper();

            string sC1 = cConvert.Compress(sInnerText, Enum_Encoding.Unicode);
            string sC2 = cConvert.DeCompress(sC1, Enum_Encoding.UTF8);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(sInnerText);
            /*CREAMOS EL SERIALIZADOR DEL OBJETO*/
            XmlSerializer xmlSerial = new XmlSerializer(typeof(XmlDocument));
            StringWriter sWriter = new System.IO.StringWriter();
            xmlSerial.Serialize(sWriter, xDoc);
            XmlDocument xmlDocumento = new XmlDocument();

            /*AGREGAMOS EL STRING DEL OBJETO SERIALIZADO A UN DOCUMENTO XML */
            xmlDocumento.LoadXml(sWriter.ToString());
            DataSet dsDataset = new DataSet();

            /*LEEMOS EL DOCUMENTO  XML Y LO AGREGAMOS AL LECTOR XML*/
            XmlTextReader txtReader = new XmlTextReader(new StringReader(xmlDocumento.OuterXml));
            try
            {
                HttpServerUtility objServer = HttpContext.Current.Server;
                dsDataset.ReadXmlSchema(sRutaScheme);

                /*LEEMOS EL XML Y LO AGREGAMOS AL DATASET*/
                dsDataset.ReadXml(txtReader);

                /*CERRAMO EL LECTOR DEL XML*/
                txtReader.Close();

                /*ACEPTAMOS L0S CAMBIOS EN EL DATASET*/
                dsDataset.AcceptChanges();

                /*RETORNAMOS EL DATASET*/
                return dsDataset;
            }
            catch (Exception)
            {
                txtReader.Close();
                return null;
            }
        }

        public DataSet XMLDataset(XmlDocument oXmlDocumento, string strURLShema)
        {
            DataSet dsDataset = new DataSet();
            XmlTextReader txtReader = new XmlTextReader(new StringReader(oXmlDocumento.OuterXml));
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(DataSet));
                dsDataset.ReadXmlSchema(strURLShema);
                dsDataset.ReadXml(txtReader, XmlReadMode.ReadSchema);
                txtReader.Close();

                return dsDataset;
            }
            catch
            {
                txtReader.Close();
                return null;
            }
        }
        #endregion
        public void XMLFile(XmlDocument xmlDoc, string strPathXML, string strFile)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(XmlDocument));
            StreamWriter WriterRQ = new StreamWriter(strPathXML + strFile + ".xml");
            try
            {
                SerializerRQ.Serialize(WriterRQ, xmlDoc);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public void XMLFile(string xmlDoc, string strPathXML, string strFile)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(string));
            StreamWriter WriterRQ = new StreamWriter(strPathXML + strFile + ".xml");
            try
            {
                SerializerRQ.Serialize(WriterRQ, xmlDoc);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public DataSet XMLDatasetSabre(XmlDocument oXmlDocumento, string strURLShema)
        {
            //DataSet dsDataset = new DataSet();
            //string strPathXML = clsValidaciones.CacheTempCrea();
            //XmlReader txtReader = XmlReader.Create(new StringReader(oXmlDocumento.OuterXml));
            //try
            //{
            //    OTA_VehAvailRateRS oOTA_VehAvailRateRS = servicio.OTA_VehAvailRateRQ(oOTA_VehAvailRateRQ);
            //    ///
            //    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(OTA_VehAvailRateRS));
            //    System.IO.StringWriter sw = new System.IO.StringWriter();
            //    xs.Serialize(sw, oOTA_VehAvailRateRS);
            //    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //    doc.LoadXml(sw.ToString());
            //    System.Data.DataSet dsResult = new clsSerializer().XMLDatasetSabre(doc, "http://webservices.sabre.com/wsdl/sabreXML1.0.00/tpf/OTA_VehAvailRateLLS1.9.1RS.xsd");

            //    XmlSerializer SerializerRS = new XmlSerializer(typeof(DataSet));
            //    XmlValidatingReader valida = new XmlValidatingReader(txtReader);
            //    dsDataset = (DataSet)SerializerRS.Deserialize(txtReader);

            //    txtReader.Close();
            //    txtReader.Dispose();

            //    return dsDataset;
            //}
            //catch (Exception Ex)
            //{
            //    txtReader.Close();
            //    txtReader.Dispose();
            //    return null;
            //}

            DataSet dsDataset = new DataSet();
            XmlTextReader txtReader = new XmlTextReader(new StringReader(oXmlDocumento.OuterXml));
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(DataSet));
                //dsDataset.ReadXmlSchema("http://webservices.sabre.com/wsdl/sabreXML1.0.00/tpf/OTA_AirLowFareSearchLLS1.12.1RS.xsd");
                dsDataset.ReadXmlSchema(strURLShema);
                dsDataset.ReadXml(txtReader, XmlReadMode.ReadSchema);
                //dsDataset = (DataSet)SerializerRS.Deserialize(txtReader);
                txtReader.Close();

                return dsDataset;
            }
            catch
            {
                txtReader.Close();
                return null;
            }
        }
        public DataTable XMLDataTable(string strArchivoXML)
        {
            DataTable dtDataTable = new DataTable();
            string strPathXML = clsValidaciones.CacheTempCrea();
            TextReader txtReader = new StreamReader(strPathXML + strArchivoXML + ".xml");
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(DataTable));
                dtDataTable = (DataTable)SerializerRS.Deserialize(txtReader);

                txtReader.Close();
                txtReader.Dispose();
                return dtDataTable;
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
                return null;
            }
        }
        public DataTable TextDataTable(string strText)
        {
            DataSet dsData = new DataSet();
            DataTable dtData = new DataTable();
            TextReader txtReader = new StringReader(strText);
            try
            {
                dsData.ReadXml(txtReader);
                txtReader.Close();
                txtReader.Dispose();
                if (dsData.Tables.Count > 0)
                    dtData = dsData.Tables[0].Copy();
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
            }
            return dtData;
        }
        public Xml XMLxml(string strArchivoXML)
        {
            Xml xmDataset = new Xml();
            string strPathXML = clsValidaciones.CacheTempCrea();
            TextReader txtReader = new StreamReader(strPathXML + strArchivoXML + ".xml");
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(Xml));
                xmDataset = (Xml)SerializerRS.Deserialize(txtReader);

                txtReader.Close();
                txtReader.Dispose();

                return xmDataset;
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
                return null;
            }
        }
        public void SaveXML(string strArchivoXML, string strXmlContenido)
        {
            string strPathXML = clsValidaciones.XMLDatasetCrea();
            if (!strArchivoXML.ToUpper().Contains(".XML"))
                strArchivoXML += ".xml";
            strPathXML += strArchivoXML;

            StreamWriter WriterRQ = new StreamWriter(strPathXML);

            try
            {
                WriterRQ.WriteLine(strXmlContenido);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch (Exception)
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public void SaveFile(string strArchivo, string strContenido)
        {
            string strPathXML = clsValidaciones.DocumentosCrea();
            if (!strArchivo.ToUpper().Contains("."))
                strArchivo += ".xml";
            strPathXML += strArchivo;

            StreamWriter WriterRQ = new StreamWriter(strPathXML);

            try
            {
                WriterRQ.WriteLine(strContenido);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch (Exception)
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public void SaveFileGen(string strArchivo, string strContenido)
        {
            StreamWriter WriterRQ = new StreamWriter(strArchivo);

            try
            {
                WriterRQ.WriteLine(strContenido);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch (Exception)
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public void SaveXSD(string strArchivoXSD, string strXmlContenido)
        {
            string strPathXML = clsValidaciones.CacheTempCrea();
            if (!strArchivoXSD.ToUpper().Contains(".XSD"))
                strArchivoXSD += ".xsd";
            strPathXML += strArchivoXSD;

            StreamWriter WriterRQ = new StreamWriter(strPathXML);

            try
            {
                WriterRQ.WriteLine(strXmlContenido);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch (Exception)
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public string RecuperaXML(string strArchivoXML)
        {
            String stDataset = string.Empty;
            string strPathXML = clsValidaciones.CacheTempCrea();
            if (!strArchivoXML.ToUpper().Contains(".XML"))
                strArchivoXML += ".xml";
            strPathXML += strArchivoXML;

            TextReader txtReader = new StreamReader(strPathXML);
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(String));
                stDataset = (String)SerializerRS.Deserialize(txtReader);

                txtReader.Close();
                txtReader.Dispose();
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
            }
            return stDataset;
        }
        public string XMLstring(string strArchivoXML)
        {
            String stDataset;
            string strPathXML = clsValidaciones.CacheTempCrea();
            TextReader txtReader = new StreamReader(strPathXML + strArchivoXML + ".xml");
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(String));
                stDataset = (String)SerializerRS.Deserialize(txtReader);

                txtReader.Close();
                txtReader.Dispose();

                return stDataset;
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
                return null;
            }
        }
        //public string RecuperarXML(string strPathXML, string strArchivoXML)
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
        //    TextReader txtReader = new StreamReader(strPathXML + strArchivoXML);
        //    try
        //    {
        //        XmlSerializer SerializerRS = new XmlSerializer(typeof(XmlDocument));
        //        xmlDoc = (XmlDocument)SerializerRS.Deserialize(txtReader);

        //        txtReader.Close();
        //        txtReader.Dispose();

        //        return xmlDoc;
        //    }
        //    catch (Exception Ex)
        //    {
        //        txtReader.Close();
        //        txtReader.Dispose();
        //        return xmlDoc;
        //    }
        //}
        public void ClaseXML(Object csClase, string strArchivoXML)
        {
            string pathXML = clsValidaciones.CacheTempCrea();

            XmlSerializer SerializerRQ = new XmlSerializer(typeof(Object));
            StreamWriter WriterRQ = new StreamWriter(pathXML + strArchivoXML + ".xml");
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public Object XMLClase(string strArchivoXML)
        {
            Object csObject = new Object();
            string strPathXML = clsValidaciones.CacheTempCrea();
            TextReader txtReader = new StreamReader(strPathXML + strArchivoXML + ".xml");
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(Object));
                csObject = (Object)SerializerRS.Deserialize(txtReader);

                txtReader.Close();
                txtReader.Dispose();
                return csObject;
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
                return null;
            }
        }
      
        #region Utilidades de XmlData
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dsData"></param>
        /// <returns></returns>
        public XmlDocument CrearXmlDs(DataSet dsData)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(DataSet));
            StringWriter WriterRQ = new StringWriter();

            SerializerRQ.Serialize(WriterRQ, dsData);
            xmlDoc.LoadXml(WriterRQ.ToString());

            return xmlDoc;
        }
        public XmlDocument CrearXmlDs(DataSet dsData, Enum_Encoding eEncoding)
        {
            string sEncoding = clsHttpZipper.EncodingIn(eEncoding);
            string sEncoding8 = clsHttpZipper.EncodingIn(Enum_Encoding.UTF8);
            string sEncoding16 = clsHttpZipper.EncodingIn(Enum_Encoding.Unicode);

            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(DataSet));
            StringWriter WriterRQ = new StringWriter();

            SerializerRQ.Serialize(WriterRQ, dsData);

            xmlDoc.LoadXml(WriterRQ.ToString());

            xmlDoc.InnerXml = xmlDoc.InnerXml.Replace(sEncoding8, sEncoding);
            xmlDoc.InnerXml = xmlDoc.InnerXml.Replace(sEncoding16, sEncoding);
            xmlDoc.InnerXml = xmlDoc.InnerXml.Replace(sEncoding8.ToLower(), sEncoding);
            xmlDoc.InnerXml = xmlDoc.InnerXml.Replace(sEncoding16.ToLower(), sEncoding);

            return xmlDoc;
        }

        /// <summary>
        /// Permite crear un objeto DataSet a partir de un documento xml
        /// </summary>
        /// <param name="strXml">Documento xml</param>
        /// <returns>Objeto dataset</returns>
        /// <remarks>
        /// Autor	: Jose Faustino Posas
        /// Fecha	: 2009-10-01
        /// </remarks>
        public DataSet CrearDataSet(string strXml)
        {
            DataSet dsXml = new DataSet();
            XmlTextReader oReader;
            StringReader oStream;

            oStream = new StringReader(strXml);
            oReader = new XmlTextReader(oStream);

            dsXml.ReadXml(oReader);

            oReader.Close();
            oStream.Close();

            return dsXml;
        }
        /// <summary>
        /// Permite crear un objeto DataSet a partir de un nodo xml
        /// </summary>
        /// <param name="strXml">Parametro xmlNode</param>
        /// <returns></returns>
        /// <remarks>
        /// Autor	: Jose Faustino Posas
        /// Fecha	: 2009-10-01
        /// </remarks>
        public DataSet CrearDataSet(XmlNode strXml)
        {
            XmlNodeReader xmlReaderResultatos;
            DataSet dsXml = new DataSet();

            xmlReaderResultatos = new XmlNodeReader(strXml);
            dsXml.ReadXml(xmlReaderResultatos);
            return dsXml;
        }
        /// <summary>
        /// Crea un archivo Xml en disco y retorna un documento xml a partir de un datatable
        /// </summary>
        /// <param name="dsData">Dataset que contiene la tabla</param>
        /// <param name="sNameTable">Nombre que se le dara a la tabla para los nodos, si no existe se creara como Referencia</param>
        /// <param name="sPathFile">Ruta fisica donde se creara el xml</param>
        /// <param name="sNameXml">Nombre del Xml, sin extencion</param>
        /// <param name="iIndexTable">Indice de la tabla en el dataset</param>
        /// <returns></returns>
        public XmlDocument CrearXmlTable(DataSet dsData, string sNameTable, string sPathFile, string sNameXml, int iIndexTable, Enum_Encoding eEncoding)
        {
            try
            {
                string sEncoding = clsHttpZipper.EncodingIn(eEncoding);

                XmlDocument xmlDoc = new XmlDocument();
                DataTable dtData = new DataTable();
                string sPath = sPathFile + sNameXml + ".xml";
                dtData = dsData.Tables[iIndexTable];

                if (sNameTable.Length.Equals(0))
                    dtData.TableName = "Referencias";
                else
                    dtData.TableName = sNameTable;

                dtData.WriteXml(sPath);

                xmlDoc.Load(sPath);
                xmlDoc.InnerXml = xmlDoc.InnerXml.Replace("standalone=", "encoding=");
                xmlDoc.InnerXml = xmlDoc.InnerXml.Replace("yes", sEncoding);
                return xmlDoc;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region Utilidades de XPath

        //public XmlDocument AsignarParametro(XmlDocument xmlDoc, string xPathQuery, string strValor)
        //{
        //    string strValorElemento = (strValor == null || strValor.Trim() == "") ? "" : strValor;

        //    XmlNode oNodo = xmlDoc.SelectSingleNode(xPathQuery);
        //    oNodo.InnerText = strValorElemento;

        //    return xmlDoc;
        //}
        /// <summary>
        /// Asigna el parámetro al documento xml de la solicitd 
        /// </summary>
        /// <param name="xmlDoc">Documento xml de la solicitud</param>
        /// <param name="xPathQuery">Consulta para obtener los datos</param>
        /// <param name="strValor">Valor a asignar</param>
        /// <remarks>
        /// Autor	: Jose Faustino Posas
        /// Fecha	: 2009-10-01
        /// </remarks>
        public XmlDocument AsignarParametro(XmlDocument xmlDoc, string xPathQuery, string strValor)
        {
            string strValorElemento = (strValor == null || strValor.Trim() == "") ? "" : strValor;

            XmlNodeList xNodo = xmlDoc.GetElementsByTagName(xPathQuery);
            xNodo[0].InnerText = strValorElemento;

            return xmlDoc;
        }
        public XmlDocument AsignarParametro(XmlDocument xmlDoc, string xPathQuery, string strValor, int iPos)
        {
            string strValorElemento = (strValor == null || strValor.Trim() == "") ? "" : strValor;

            XmlNodeList xNodo = xmlDoc.GetElementsByTagName(xPathQuery);
            xNodo[iPos].InnerText = strValorElemento;

            return xmlDoc;
        }
        /// <summary>
        /// Asigna el atributo a un nodo del documento xml de la solicitd 
        /// </summary>
        /// <param name="xmlDoc">Documento xml de la solicitud</param>
        /// <param name="xPathAtributo">Atributo</param>
        /// <param name="xPathQuery">Nodo</param>
        /// <param name="strValor">Valor a asignar</param>
        /// <returns></returns>
        public XmlDocument AsignarAtrributo(XmlDocument xmlDoc, string xPathAtributo, string xPathQuery, string strValor)
        {
            string strValorElemento = (strValor == null || strValor.Trim() == "") ? "" : strValor;

            XmlNodeList xNodo = xmlDoc.GetElementsByTagName(xPathQuery);
            XmlAttribute oAtrribute = xNodo[0].Attributes[xPathAtributo];
            oAtrribute.Value = strValorElemento;

            return xmlDoc;
        }
        public XmlDocument AsignarAtrributo(XmlDocument xmlDoc, string xPathAtributo, string xPathQuery, string strValor, int iPos)
        {
            string strValorElemento = (strValor == null || strValor.Trim() == "") ? "" : strValor;

            XmlNodeList xNodo = xmlDoc.GetElementsByTagName(xPathQuery);
            XmlAttribute oAtrribute = xNodo[iPos].Attributes[xPathAtributo];
            oAtrribute.Value = strValorElemento;

            return xmlDoc;
        }
        public XmlNodeList AsignarNodoList(XmlNode xmlDoc)
        {
            XmlNodeList xNodo = xmlDoc.ChildNodes;
            return xNodo;
        }
        public XmlNode AsignarAtrributo(XmlNode xNodo, string xPathAtributo, string xPathQuery, string strValor)
        {
            string strValorElemento = (strValor == null || strValor.Trim() == "") ? "" : strValor;

            XmlAttribute oAtrribute = xNodo.Attributes[xPathAtributo];
            oAtrribute.Value = strValorElemento;

            return xNodo;
        }
        public XmlNode AsignarParametro(XmlNode xmlDoc, string strValor)
        {
            string strValorElemento = (strValor == null || strValor.Trim() == "") ? "" : strValor;
            xmlDoc.InnerText = strValorElemento;
            return xmlDoc;
        }
        public XmlNode AsignarNodo(XmlDocument xmlDoc, string xPathQuery, int iPos)
        {
            XmlNodeList xNodo = xmlDoc.GetElementsByTagName(xPathQuery);
            XmlNode oNodo = xNodo[iPos];
            return oNodo;
        }
        public XmlNodeList AsignarNodoList(XmlDocument xmlDoc, string xPathQuery)
        {
            XmlNodeList xNodo = xmlDoc.GetElementsByTagName(xPathQuery);

            return xNodo;
        }
        public XmlNodeList AsignarNodoList(XmlNode xmlDoc, string xPathQuery)
        {
            XmlNodeList xNodo = xmlDoc.SelectNodes(xPathQuery);

            return xNodo;
        }
        public XmlDocument RecuperarXML(string sRuta, string sXml)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(sRuta + sXml);

            return xmlDoc;
        }
        /// <summary>
        /// Asigna el parámetro al documento xml de la solicitd 
        /// </summary>
        /// <param name="xmlDoc">Documento xml de la solicitud</param>
        /// <param name="xPathQuery">Consulta para obtener los datos</param>
        /// <param name="intValor">Valor a asignar</param>
        /// <param name="blnCeroEsValido">Indica si el cero es válido o no</param>
        /// <remarks>
        /// Autor	: Jose Faustino Posas
        /// Fecha	: 2009-10-01
        /// </remarks>
        public XmlDocument AsignarParametro(XmlDocument xmlDoc, string xPathQuery,
                                            int intValor, bool blnCeroEsValido)
        {
            // 2006-09-14 y 2006-09-19 EADE
            string strValor = "";

            if (!blnCeroEsValido)
                strValor = (intValor == 0) ? "" : intValor.ToString();
            else
                strValor = intValor.ToString();

            return AsignarParametro(xmlDoc, xPathQuery, strValor);
        }

        /// <summary>
        /// Crea un nuevo parámetro dentro del documento xml
        /// </summary>
        /// <param name="xmlDoc">Documento xml</param>
        /// <param name="xPathQuery">Consulta para el nuevo parámetro</param>
        /// <param name="strValor">Valor que se debe asignar al nuevo elemento</param>
        /// <returns>Nuevo documento xml</returns>
        /// Autor	: Jose Faustino Posas
        /// Fecha	: 2009-10-01
        public XmlDocument CrearParametro(XmlDocument xmlDoc, string xPathQuery, string strValor)
        {
            string strValorElemento = (strValor == null || strValor.Trim() == "") ? "" : strValor;
            string strParentNode = "";
            string strChildNode = "";
            XmlNode oNodo;
            XmlElement newElem;

            strParentNode = xPathQuery.Substring(0, xPathQuery.LastIndexOf("/"));
            strChildNode = xPathQuery.Substring(xPathQuery.LastIndexOf("/") + 1);

            oNodo = xmlDoc.SelectSingleNode(strParentNode);

            newElem = xmlDoc.CreateElement(strChildNode);
            newElem.InnerText = strValorElemento;

            oNodo.AppendChild(newElem);

            return xmlDoc;
        }
        #endregion

        #region Creacion de datset de un xml

        public void CrearDataSet(DataSet dsData, XmlDocument xmlDoc, string sEncabezado)
        {
            try
            {
                clsEstructura cEstructura = new clsEstructura();
                cEstructura = cEstructura.getEstructura(dsData);

                int iCounTables = cEstructura.lTable.Count;
                for (int t = 0; t < iCounTables; t++)
                {
                    string sTableName = cEstructura.lTable[t].TableName;
                    XmlNode XMLTables_ = xmlDoc[sEncabezado][sTableName];

                    foreach (XmlNode XMLTable_ in XMLTables_.ChildNodes)
                    {
                        DataRow RowTable_ = dsData.Tables[sTableName].NewRow();
                        int iCountColumns = cEstructura.lTable[t].lColumns.Count;

                        for (int c = 0; c < iCountColumns; c++)
                        {
                            RowTable_[cEstructura.lTable[t].lColumns[c].ColumnName] = XMLTable_[cEstructura.lTable[t].lColumns[c].ColumnName].InnerText;
                        }
                        dsData.Tables[sTableName].Rows.Add(RowTable_);
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Metodo = "clsSerialzer";
                cParametros.Complemento = "Creando dataset " + dsData.DataSetName.ToString();
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                ExceptionHandled.Publicar(cParametros);
            }
        }

        public DataSet CrearDataSet(XmlDocument xmlDoc, string sEncabezado)
        {
            DataSet dsData = new DataSet();
            try
            {
                foreach (XmlNode XMLTable_ in xmlDoc[sEncabezado].ChildNodes)
                {
                    DataSet dsDataNew = new DataSet();
                    dsDataNew = CrearDataSet(XMLTable_);
                    try
                    {
                        if (dsDataNew != null || dsDataNew.Tables.Count > 0)
                        {
                            clsDataNet.dsDataTableAdd(dsData, dsDataNew);
                        }
                    }
                    catch { }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Metodo = "clsSerialzer";
                cParametros.Complemento = "Creando dataset " + dsData.DataSetName.ToString();
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                ExceptionHandled.Publicar(cParametros);
            }
            return dsData;
        }

        public static System.Xml.XmlTextReader Get_XSD_Clase(Type Tipo_Searializar, String strNamespace)
        {
            XmlReflectionImporter importer = new XmlReflectionImporter();
            XmlSchemas schemas = new XmlSchemas();
            XmlSchemaExporter exporter = new XmlSchemaExporter(schemas);
            XmlTypeMapping mapping = importer.ImportTypeMapping(Tipo_Searializar);
            exporter.ExportTypeMapping(mapping);
            System.Xml.Schema.XmlSchema schema;
            if (!String.IsNullOrEmpty(strNamespace))
                schema = schemas[strNamespace];
            else
                schema = schemas[0];
            System.IO.StringWriter sWriter = new System.IO.StringWriter();
            schema.Write(sWriter);
            System.Xml.XmlTextReader txtReader = new System.Xml.XmlTextReader(new System.IO.StringReader(sWriter.ToString()));
            return txtReader;
        }

        public static String Get_XSD_String_Clase(Type Tipo_Searializar)
        {
            XmlReflectionImporter importer = new XmlReflectionImporter();
            XmlSchemas schemas = new XmlSchemas();
            XmlSchemaExporter exporter = new XmlSchemaExporter(schemas);
            XmlTypeMapping mapping = importer.ImportTypeMapping(Tipo_Searializar);
            exporter.ExportTypeMapping(mapping);
            System.Xml.Schema.XmlSchema schema = schemas[0];
            System.IO.StringWriter sWriter = new System.IO.StringWriter();
            schema.Write(sWriter);
            return sWriter.ToString();
        }
        #endregion
        #region Creacion de Xml de onobject
        public static string XmlSerializeObject(Object xmlObject)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            XmlSerializer xs = new XmlSerializer(xmlObject.GetType());
            StringBuilder sb = new StringBuilder();
            xs.Serialize(new StringWriter(sb), xmlObject, ns);

            sb = sb.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>" + (char)13 + (char)10, String.Empty);
            return sb.ToString();
        }
        #endregion
        public string RecuperaFileGen(string strArchivo)
        {
            string strTexto = string.Empty;
            TextReader txtReader = new StreamReader(strArchivo);
            try
            {
                strTexto = txtReader.ReadToEnd();
                txtReader.Close();
                txtReader.Dispose();
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
            }
            return strTexto;
        }
        public string RecuperaFileTemp(string strArchivo)
        {
            string strPathXML = clsValidaciones.DocumentosTempCrea();
            if (!strArchivo.ToUpper().Contains("."))
                strArchivo += ".xml";
            strPathXML += strArchivo;

            string strTexto = string.Empty;
            TextReader txtReader = new StreamReader(strPathXML);
            try
            {
                strTexto = txtReader.ReadToEnd();
                txtReader.Close();
                txtReader.Dispose();
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
            }
            return strTexto;
        }
        public void SaveFileTemp(string strArchivo, string strContenido)
        {
            string strPathXML = clsValidaciones.DocumentosTempCrea();
            if (!strArchivo.ToUpper().Contains("."))
                strArchivo += ".xml";
            strPathXML += strArchivo;

            StreamWriter WriterRQ = new StreamWriter(strPathXML);
            try
            {
                WriterRQ.WriteLine(strContenido);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch (Exception)
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public DataSet XMLDatasetTemp(string strArchivo)
        {
            string strPathXML = clsValidaciones.DocumentosTempCrea();
            if (!strArchivo.ToUpper().Contains("."))
                strArchivo += ".xml";
            strPathXML += strArchivo;

            DataSet dsDataset = new DataSet();
            TextReader txtReader = new StreamReader(strPathXML);
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(DataSet));
                dsDataset = (DataSet)SerializerRS.Deserialize(txtReader);

                txtReader.Close();
                txtReader.Dispose();
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
            }
            return dsDataset;
        }
        public DataSet XMLDatasetGen(string strArchivo)
        {
            DataSet dsDataset = new DataSet();
            TextReader txtReader = new StreamReader(strArchivo);
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(DataSet));
                dsDataset = (DataSet)SerializerRS.Deserialize(txtReader);

                txtReader.Close();
                txtReader.Dispose();
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
            }
            return dsDataset;
        }
        public void DatasetXMLGen(DataSet dsDataset, string strArchivo)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(DataSet));
            StreamWriter WriterRQ = new StreamWriter(strArchivo);
            try
            {
                SerializerRQ.Serialize(WriterRQ, dsDataset);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch (Exception Ex)
            {
                WriterRQ.Flush();
                WriterRQ.Close();

                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Crear archivo refere: Archivo = " + strArchivo;
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void DatasetXMLTemp(DataSet dsDataset, string strArchivo)
        {
            string strPathXML = clsValidaciones.DocumentosTempCrea();
            if (!strArchivo.ToUpper().Contains("."))
                strArchivo += ".xml";
            strPathXML += strArchivo;
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(DataSet));
            StreamWriter WriterRQ = new StreamWriter(strPathXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, dsDataset);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public DataTable XMLDataTableTemp(string strArchivo)
        {
            string strPathXML = clsValidaciones.DocumentosTempCrea();
            if (!strArchivo.ToUpper().Contains("."))
                strArchivo += ".xml";
            strPathXML += strArchivo;

            DataTable dtDataset = new DataTable();
            TextReader txtReader = new StreamReader(strPathXML);
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(DataTable));
                dtDataset = (DataTable)SerializerRS.Deserialize(txtReader);

                txtReader.Close();
                txtReader.Dispose();
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
            }
            return dtDataset;
        }
        public DataTable XMLDataTableGen(string strArchivo)
        {
            DataTable dtDataset = new DataTable();
            TextReader txtReader = new StreamReader(strArchivo);
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(DataTable));
                dtDataset = (DataTable)SerializerRS.Deserialize(txtReader);

                txtReader.Close();
                txtReader.Dispose();
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
            }
            return dtDataset;
        }
        public void DataTableXMLGen(DataTable dtDataset, string strArchivo)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(DataTable));
            StreamWriter WriterRQ = new StreamWriter(strArchivo);
            try
            {
                SerializerRQ.Serialize(WriterRQ, dtDataset);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public void DataTableXMLTemp(DataTable dtDataset, string strArchivo)
        {
            string strPathXML = clsValidaciones.DocumentosTempCrea();
            if (!strArchivo.ToUpper().Contains("."))
                strArchivo += ".xml";
            strPathXML += strArchivo;
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(DataTable));
            StreamWriter WriterRQ = new StreamWriter(strPathXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, dtDataset);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public XmlDocument XMLString(string strTexto)
        {
            XmlDocument xmDataset = new XmlDocument();
            TextReader txtReader = new StreamReader(strTexto);
            try
            {
                XmlSerializer SerializerRS = new XmlSerializer(typeof(XmlDocument));
                xmDataset = (XmlDocument)SerializerRS.Deserialize(txtReader);

                txtReader.Close();
                txtReader.Dispose();
            }
            catch
            {
                txtReader.Close();
                txtReader.Dispose();
            }
            return xmDataset;
        }
    }
}
