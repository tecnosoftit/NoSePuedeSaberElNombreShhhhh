using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Ssoft.Utils;

namespace Ssoft.Rules.General
{
   public class clsIATAVirtual
    {
        public string sIda { get; set; }
        public string sVuelta { get; set; }

        public clsIATAVirtual()
        {
        }

       /// <summary>
       /// Obtenemos los IATAvalidadas
       /// </summary>
       /// <param name="sItaVirtualIda"></param>
       /// <param name="sItaVirtualVuelta"></param>
       /// <returns></returns>
        public clsIATAVirtual sObtenerIataVirtual(string sItaVirtualIda, string sItaVirtualVuelta)
        {
            clsIATAVirtual ObIta = new clsIATAVirtual();
            string sName = "IataVirtual.xml";
            string sRuta = clsValidaciones.XMLTempCrea();
            string sValueIda = sItaVirtualIda;
            string sValueVuelta = sItaVirtualVuelta;
            try
            {
                XmlDocument XMLIATA = new XmlDocument();
                XMLIATA.Load(sRuta + sName);

                XmlNodeList xnListIda = XMLIATA.SelectNodes("/CIUDAD/IATA[@ID='" + sItaVirtualIda + "']/AEROPUERTO");
                XmlNodeList xnListVuelta = XMLIATA.SelectNodes("/CIUDAD/IATA[@ID='" + sItaVirtualVuelta + "']/AEROPUERTO");

                int x = 0;
                //Existe Iata virtuales to From
                foreach (XmlNode xn in xnListIda)
                {
                    sValueIda = sValueIda + "," + xn.InnerText;
                    if (x == 0 || xnListIda.Count == 1)
                    {
                        sValueIda = xn.InnerText;
                    }
                    x++;
                }
                x = 0;
                foreach (XmlNode xn in xnListVuelta)
                {
                    sValueVuelta = sValueVuelta + "," + xn.InnerText;
                    if (x == 0 || xnListVuelta.Count == 1)
                    {
                        sValueVuelta = xn.InnerText;
                    }
                    x++;
                }
            }
            catch { 

            }
            ObIta.sIda = sValueIda;
            ObIta.sVuelta = sValueVuelta;
           //Existe Iata virtuales to From

            return ObIta;
        }
    }
}
