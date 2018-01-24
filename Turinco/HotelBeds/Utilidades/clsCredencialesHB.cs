using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Ssoft.ValueObjects;
using Ssoft.Utils;

namespace WS_SsoftHotelBeds.Utilidades
{
    internal class clsCredencialesHB
    {
        public XmlDocument Credenciales(XmlDocument xmlDoc, VO_Credentials vo_Credentials, string sRQ, bool bSession)
        {
            clsSerializer cDataXml = new clsSerializer();
            try
            {
                if (bSession)
                    xmlDoc = cDataXml.AsignarAtrributo(xmlDoc, "sessionId", sRQ, vo_Credentials.SessionId);
            }
            catch { }
            xmlDoc = cDataXml.AsignarParametro(xmlDoc, "User", vo_Credentials.User);
            xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Password", vo_Credentials.Password);
            xmlDoc = cDataXml.AsignarParametro(xmlDoc, "Language", vo_Credentials.Language);
            return xmlDoc;
        }
    }
}
