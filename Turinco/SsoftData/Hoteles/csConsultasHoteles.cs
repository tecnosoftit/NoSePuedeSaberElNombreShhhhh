using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ssoft.Sql;
using Ssoft.Utils;
using System.Configuration;
using System.Data;
using SsoftQuery.Vuelos;

namespace SsoftQuery.Hoteles
{
    
   public  class csConsultasHoteles
    {
        private const string DISTANCIAS_ID = "40";
        private const string HABITACION_ID = "60";
        private const string INSTALACIONES_ID = "70";
       protected DataSql dConsulta = new DataSql();
       protected DataSet dsConsulta = new DataSet();
       protected DataTable dtConsulta = new DataTable();
       public string sDescription(string sHotelCode, string sLenguaje)
       {
           string sDatos = string.Empty;
           StringBuilder consulta = new StringBuilder();
           string sCadenaConexion = clsValidaciones.GetKeyOrAdd("strConexionHB");
           try
           {
               if (sCadenaConexion != "")
               {
                   consulta.AppendLine("USE  " + clsValidaciones.GetKeyOrAdd("BDHotelbeds", "BDHotelbeds"));
                   consulta.AppendLine(" SELECT     HotelFacilities FROM         dbo.HOTEL_DESCRIPTIONS WHERE     (HotelCode = '" + sHotelCode + "') AND (LanguageCode = '" + sLenguaje + "')  ");

                   DataSql dsConsulta = new DataSql();
                   dsConsulta.Conexion = sCadenaConexion;
                   sDatos = dsConsulta.Select(consulta.ToString()).Tables[0].Rows[0][0].ToString();
               }
           }
           catch
           {
           }
           return sDatos;
       }
       public string sTasasDeCambio(string strCodeOrigen, string strCodeDestino, string strValor)
       {
           string strTasa =null;
           string strEmpresa = "6";

           try
           {
               if (new csCache() != null)
               {
                   strEmpresa = new csCache().cCache().Empresa;
               }

               strCodeOrigen = new CsConsultasVuelos().ConsultaCodigo(strCodeOrigen, "tblMonedas", "intCode", "strCode");
               strCodeDestino = new CsConsultasVuelos().ConsultaCodigo(strCodeDestino, "tblMonedas", "intCode", "strCode");

               dsConsulta = dConsulta.ConsultaTabla("SELECT DBLTASA FROM TBLTASAS INNER JOIN TBLMONEDAS AS MONEDAORIGEN ON MONEDAORIGEN.INTCODE=TBLTASAS.INTMONEDAORIGEN INNER JOIN TBLMONEDAS AS MONEDADESTINO ON MONEDADESTINO.INTCODE=TBLTASAS.INTMONEDADESTINO WHERE INTEMPRESA='" + strEmpresa + "' AND  TBLTASAS.INTMONEDAORIGEN='" + strCodeOrigen + "' AND TBLTASAS.INTMONEDADESTINO='" + strCodeDestino + "'  ORDER BY DTMFECHACREACION DESC");

               if (dsConsulta.Tables.Count > 0)
               {
                   if (dsConsulta.Tables[0].Rows.Count > 0)
                   {
                       strTasa = (Convert.ToDecimal(dsConsulta.Tables[0].Rows[0][0].ToString()) * Convert.ToDecimal(strValor)).ToString();
                   }
                   else
                   {
                       strTasa = null;
                   }
               }
               else
               {
                   strTasa = null;
               }

           }
           catch
           {
               strTasa = null;
           }


           return strTasa;

       }
       public DataTable ConsultadsHotelbedsCity(string sIdioma)
       {
           string sBd = "BDHotelbeds";
           DataSet dsDatos = new DataSet();
           DataTable dtDatos = new DataTable();
           try
           {
               sBd = clsValidaciones.GetKeyOrAdd("BDHotelbeds", "BDHotelbeds");
           }
           catch
           {
               sBd = "BDHotelbeds";
           }
           StringBuilder consulta = new StringBuilder();
           try
           {
               consulta.AppendLine(" USE " + sBd + " ");
               consulta.AppendLine(" SELECT  A.DESTINATIONCODE As strRefere,(B.NAME+'  '+C.NAME) As strDetalle ,C.NAME AS strValorAdic  ");
               consulta.AppendLine("  FROM DESTINATIONS A  JOIN DESTINATION_IDS B");
               consulta.AppendLine("ON A.DESTINATIONCODE = B.DESTINATIONCODE  JOIN COUNTRY_IDS C  ON C.COUNTRYCODE = A.COUNTRYCODE");
               consulta.AppendLine(" WHERE (B.LANGUAGECODE = '" + sIdioma + "') AND (C.LANGUAGECODE ='CAS')");

               DataSql dsConsulta = new DataSql();
               dsDatos = dsConsulta.Select(consulta.ToString());

               if (dsDatos.Tables.Count > 0)
               {
                   if (dsDatos.Tables[0].Rows.Count > 0)
                       dtDatos = dsDatos.Tables[0];
                   else
                       dtDatos = null;
               }
               else
                   dtDatos = null;

           }
           catch
           {
               dtDatos = null;
           }
           return dtDatos;
       }
       public DataSet ConsultaGetHotels(string strCode)
       {

           DataSet dsData = new DataSet();
           string sBd = clsValidaciones.GetKeyOrAdd("BDHotelbeds", "BDHotelbeds");
           StringBuilder consulta = new StringBuilder();
           try
           {
               consulta.AppendLine(" USE " + sBd + " ");
               consulta.AppendLine(" SELECT     HotelCode AS intCodigo, '' AS GlobalExtranetId, HotelCode AS PropertyID, Name AS PropertyName,   ");
               consulta.AppendLine(" DestinationCode AS Airport, ChainCode AS ChainCode, '' AS ChainName, DestinationCode AS ID_Country, '' AS Value_Country,  ");
               consulta.AppendLine(" '' AS Value, HotelCode AS ID  ");
               consulta.AppendLine(" FROM HOTELS  ");
               consulta.AppendLine(" WHERE (DestinationCode = '" + strCode + "') ");

               dsData = dConsulta.ConsultaTabla(consulta.ToString());
               if (dsConsulta.Tables.Count == 0)
               {
                   dsData = null;
               }
           }
           catch
           {
               dsData = null;
           }

           return dsData;
       }
       public DataSet ConsultadsZones(string strCode)
       {
           
           DataSet dsData = new DataSet();
           string sBd = clsValidaciones.GetKeyOrAdd("BDHotelbeds", "BDHotelbeds");
           StringBuilder consulta = new StringBuilder();
           try
           {
               consulta.AppendLine(" USE " + sBd + " ");
               consulta.AppendLine(" SELECT     *  FROM         ZONES WHERE     (DestinationCode = '" + strCode + "')  ");

               dsData = dConsulta.ConsultaTabla(consulta.ToString());
               //if (dsConsulta.Tables.Count == 0)
               //{
               //    dsData = null;
               //}
           }
           catch {
               dsData = null;
           }

           return dsData;
       }
       public DataSet ConsultadsDescription(string strCode, string strIdioma)
       {

           DataSet dsData = new DataSet();
           StringBuilder consulta = new StringBuilder();

           try
           {
               consulta.AppendLine("USE  " + clsValidaciones.GetKeyOrAdd("BDHotelbeds", "BDHotelbeds"));
               consulta.AppendLine(" SELECT     HotelFacilities FROM         dbo.HOTEL_DESCRIPTIONS WHERE     (HotelCode = '" + strCode + "') AND (LanguageCode = '" + strIdioma + "')  ");

               dsData = dConsulta.ConsultaTabla(consulta.ToString());
           }
           catch
           {
           }
          

           return dsData;
       }
       public DataSet ConsultadsHotelbedsFacility(string sIdioma, string idHotel, bool bSoloImagenes)
       {
           string sBd = clsValidaciones.GetKeyOrAdd("BDHotelbeds", "BDHotelbeds");
           DataSet dsDatos = new DataSet();
           string sPath = clsValidaciones.GetKeyOrAdd("PathHotelBeds", "http://www.hotelbeds.com/giata/");
           StringBuilder consulta = new StringBuilder();
           try
           {
               consulta.AppendLine(" USE " + sBd + " ");
               if (!bSoloImagenes)
               {
                   consulta.AppendLine(" SELECT     FACILITIES.HotelCode, FACILITIES.Code, FACILITY_DESCRIPTIONS.Name AS Facilidad,  ");
                   consulta.AppendLine(" FACILITY_GROUPS_DESCRIPTIONS.Name AS name_group, FACILITIES.CarDistance, FACILITIES.Concept ");
                   consulta.AppendLine(" ,FACILITIES.Fee,CASE WHEN FEE = 'S' THEN 1 ELSE 0 END AS \"VALUE\"");
                   consulta.AppendLine(" FROM         FACILITIES INNER JOIN ");
                   consulta.AppendLine(" FACILITY_DESCRIPTIONS ON FACILITIES.Code = FACILITY_DESCRIPTIONS.Code INNER JOIN ");
                   consulta.AppendLine(" FACILITY_GROUPS ON FACILITIES.Group_ = FACILITY_GROUPS.Group_ INNER JOIN ");
                   consulta.AppendLine(" FACILITY_GROUPS_DESCRIPTIONS ON FACILITY_GROUPS.Group_ = FACILITY_GROUPS_DESCRIPTIONS.Group_ INNER JOIN ");
                   consulta.AppendLine(" FACILITY_TYPES ON FACILITIES.Group_ = FACILITY_TYPES.Group_ AND FACILITIES.Code = FACILITY_TYPES.Code AND  ");
                   consulta.AppendLine(" FACILITY_DESCRIPTIONS.Group_ = FACILITY_TYPES.Group_ AND FACILITY_DESCRIPTIONS.Code = FACILITY_TYPES.Code AND  ");
                   consulta.AppendLine(" FACILITY_GROUPS.Group_ = FACILITY_TYPES.Group_ INNER JOIN ");
                   consulta.AppendLine(" FACILITY_TYPOLOGIES ON FACILITY_TYPES.TypologyCode = FACILITY_TYPOLOGIES.TypologyCode ");
                   consulta.AppendLine(" WHERE     (FACILITIES.HotelCode = '" + idHotel + "') AND (FACILITY_DESCRIPTIONS.LanguageCode = '" + sIdioma + "') AND  ");
                   consulta.AppendLine("   (FACILITY_GROUPS_DESCRIPTIONS.LanguageCode = '" + sIdioma + "') AND (FACILITIES.Group_ = '" + INSTALACIONES_ID + "') ");
               }
               if (!bSoloImagenes)
               {
                   consulta.AppendLine(" SELECT     FACILITIES.HotelCode, FACILITIES.Code, FACILITY_DESCRIPTIONS.Name AS Facilidad,  ");
                   consulta.AppendLine(" FACILITY_GROUPS_DESCRIPTIONS.Name AS name_group, FACILITIES.CarDistance, FACILITIES.Concept ");
                   consulta.AppendLine(" ,FACILITIES.Fee,CASE WHEN FEE = 'S' THEN 1 ELSE 0 END AS \"VALUE\"");
                   consulta.AppendLine(" FROM         FACILITIES INNER JOIN ");
                   consulta.AppendLine(" FACILITY_DESCRIPTIONS ON FACILITIES.Code = FACILITY_DESCRIPTIONS.Code INNER JOIN ");
                   consulta.AppendLine(" FACILITY_GROUPS ON FACILITIES.Group_ = FACILITY_GROUPS.Group_ INNER JOIN ");
                   consulta.AppendLine(" FACILITY_GROUPS_DESCRIPTIONS ON FACILITY_GROUPS.Group_ = FACILITY_GROUPS_DESCRIPTIONS.Group_ INNER JOIN ");
                   consulta.AppendLine(" FACILITY_TYPES ON FACILITIES.Group_ = FACILITY_TYPES.Group_ AND FACILITIES.Code = FACILITY_TYPES.Code AND  ");
                   consulta.AppendLine(" FACILITY_DESCRIPTIONS.Group_ = FACILITY_TYPES.Group_ AND FACILITY_DESCRIPTIONS.Code = FACILITY_TYPES.Code AND  ");
                   consulta.AppendLine(" FACILITY_GROUPS.Group_ = FACILITY_TYPES.Group_ INNER JOIN ");
                   consulta.AppendLine(" FACILITY_TYPOLOGIES ON FACILITY_TYPES.TypologyCode = FACILITY_TYPOLOGIES.TypologyCode ");
                   consulta.AppendLine(" WHERE     (FACILITIES.HotelCode = '" + idHotel + "') AND (FACILITY_DESCRIPTIONS.LanguageCode = '" + sIdioma + "') AND  ");
                   consulta.AppendLine("   (FACILITY_GROUPS_DESCRIPTIONS.LanguageCode = '" + sIdioma + "') AND (FACILITIES.Group_ = '" + HABITACION_ID + "') ");
               }

               consulta.AppendLine(" SELECT     IMAGE_DESCRIPTIONS.Name, '" + sPath + "' + HOTEL_IMAGES.ImagePath AS ImagePath  ");
               consulta.AppendLine(" FROM         HOTEL_IMAGES INNER JOIN ");
               consulta.AppendLine(" IMAGE_DESCRIPTIONS ON HOTEL_IMAGES.ImageCode = IMAGE_DESCRIPTIONS.ImageCode ");
               consulta.AppendLine(" WHERE     (HOTEL_IMAGES.HotelCode = '" + idHotel + "') AND (IMAGE_DESCRIPTIONS.LanguageCode = '" + sIdioma + "') ");
               consulta.AppendLine(" ORDER BY HOTEL_IMAGES.Order_ ");

               if (!bSoloImagenes)
               {
                   consulta.AppendLine(" SELECT     FACILITIES.HotelCode, FACILITIES.Code, FACILITY_DESCRIPTIONS.Name AS Facilidad,  ");
                   consulta.AppendLine(" FACILITY_GROUPS_DESCRIPTIONS.Name AS name_group, FACILITIES.CarDistance, FACILITIES.Concept ");
                   consulta.AppendLine(" ,FACILITIES.Fee,CASE WHEN FEE = 'S' THEN 1 ELSE 0 END AS \"VALUE\"");
                   consulta.AppendLine(" FROM         FACILITIES INNER JOIN ");
                   consulta.AppendLine(" FACILITY_DESCRIPTIONS ON FACILITIES.Code = FACILITY_DESCRIPTIONS.Code INNER JOIN ");
                   consulta.AppendLine(" FACILITY_GROUPS ON FACILITIES.Group_ = FACILITY_GROUPS.Group_ INNER JOIN ");
                   consulta.AppendLine(" FACILITY_GROUPS_DESCRIPTIONS ON FACILITY_GROUPS.Group_ = FACILITY_GROUPS_DESCRIPTIONS.Group_ INNER JOIN ");
                   consulta.AppendLine(" FACILITY_TYPES ON FACILITIES.Group_ = FACILITY_TYPES.Group_ AND FACILITIES.Code = FACILITY_TYPES.Code AND  ");
                   consulta.AppendLine(" FACILITY_DESCRIPTIONS.Group_ = FACILITY_TYPES.Group_ AND FACILITY_DESCRIPTIONS.Code = FACILITY_TYPES.Code AND  ");
                   consulta.AppendLine(" FACILITY_GROUPS.Group_ = FACILITY_TYPES.Group_ INNER JOIN ");
                   consulta.AppendLine(" FACILITY_TYPOLOGIES ON FACILITY_TYPES.TypologyCode = FACILITY_TYPOLOGIES.TypologyCode ");
                   consulta.AppendLine(" WHERE     (FACILITIES.HotelCode = '" + idHotel + "') AND (FACILITY_DESCRIPTIONS.LanguageCode = '" + sIdioma + "') AND  ");
                   consulta.AppendLine("   (FACILITY_GROUPS_DESCRIPTIONS.LanguageCode = '" + sIdioma + "') AND (FACILITIES.Group_ = '" + DISTANCIAS_ID + "') ");

               }
               
               dsDatos = dConsulta.ConsultaTabla(consulta.ToString());
           }
           catch
           {
           }
           return dsDatos;
       }
       public DataSet ConsultadsHotelInfo(string HotelCode)
       {
           DataSet dsHotelInfo = new DataSet();
           string sBd = "BDHotelbeds";
           try
           {
               sBd = clsValidaciones.GetKeyOrAdd("BDHotelbeds", "BDHotelbeds");
           }
           catch
           {
               sBd = "BDHotelbeds";
           }
           StringBuilder consulta = new StringBuilder();

           consulta.AppendLine(" USE " + sBd + " ");
           consulta.AppendLine(" SELECT DISTINCT HOTELS.HotelCode, HOTELS.Name, HOTELS.CategoryCode, HOTELS.DestinationCode,  ");
           consulta.AppendLine(" HOTELS.ChainCode, HOTELS.ZoneCode, HOTELS.Licence, HOTELS.Latitude, HOTELS.Longitude, ");
           consulta.AppendLine(" PHONES.PhoneType, PHONES.Number_ ");
           consulta.AppendLine(" FROM HOTELS INNER JOIN ");
           consulta.AppendLine(" PHONES ON HOTELS.HotelCode = PHONES.HotelCode ");
           consulta.AppendLine(" WHERE (HOTELS.HotelCode = '" + HotelCode + "') ");


           dsHotelInfo = dConsulta.ConsultaTabla(consulta.ToString());

           return dsHotelInfo;
       }
    }
}
