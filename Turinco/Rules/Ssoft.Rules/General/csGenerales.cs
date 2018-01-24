using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using Ssoft.Sql;
using Ssoft.Utils;
using Ssoft.Data;
using Ssoft.ManejadorExcepciones;
using System.IO;
using System.Net;
using System.Configuration;
using System.Web;
using System.Linq;
using SsoftQuery.Vuelos;

namespace Ssoft.Rules.Generales
{
    public class csGenerales
    {
        protected DataSql dsConsulta = new DataSql();

        protected string gstrConexion = string.Empty;

        public string Conexion
        {
            set { this.gstrConexion = value; }
            get { return this.gstrConexion; }
        }
        public csGenerales()
        {
            Conexion = clsSesiones.getConexion();
        }
   
        public DataSet Refere(string strTipoRefere)
        {
            dsConsulta.Conexion = Conexion;
            string pstrSql = string.Empty;
            DataSet dstDatos = new DataSet();
            try
            {

                pstrSql = " SELECT   tblTipoRefere.intidTipoRefere, tblTipoRefere.strDescripcion, tblRefere.* " +
                " FROM         tblTipoRefere INNER JOIN " +
                  "    tblRefere ON tblTipoRefere.intidTipoRefere = tblRefere.intidTipoRefere " +
                " WHERE     (tblTipoRefere.strTipoRefere = '" + strTipoRefere + "')  ";

                dstDatos = dsConsulta.Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                ExceptionHandled.Publicar("Consulta:  " + pstrSql);
            }
            return dstDatos;
        }
        public DataSet Refere(string strTipoRefere, string strRefere)
        {
            dsConsulta.Conexion = Conexion;
            string pstrSql = string.Empty;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT   tblTipoRefere.strDescripcion, tblRefere.* " +
                " FROM         tblTipoRefere INNER JOIN " +
                  "    tblRefere ON tblTipoRefere.intidTipoRefere = tblRefere.intidTipoRefere " +
                " WHERE     (tblTipoRefere.strTipoRefere = '" + strTipoRefere + "') AND   " +
                "           (tblRefere.strRefere = '" + strRefere + "') ";

                dstDatos = dsConsulta.Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                ExceptionHandled.Publicar("Consulta:  " + pstrSql);
            }
            return dstDatos;
        }
        public DataSet Refere(string strTipoRefere, int intValor)
        {
            dsConsulta.Conexion = Conexion;
            string pstrSql = string.Empty;
            DataSet dstDatos = new DataSet();
            string aplicacion = clsSesiones.getAplicacion().ToString();
            string idioma = clsSesiones.getIdioma();
            try
            {
                pstrSql = " SELECT   tblTipoRefere.strDescripcion, tblRefere.* " +
                " FROM         tblTipoRefere INNER JOIN " +
                  "    tblRefere ON tblTipoRefere.intidTipoRefere = tblRefere.intidTipoRefere " +
                " WHERE     (tblTipoRefere.strTipoRefere = '" + strTipoRefere + "') AND  " +
                "           (tblRefere.intProxNivel = " + intValor + ") " +
                " AND (tblRefere.intAplicacion = " + aplicacion + ") " +
                " AND (tblRefere.strIdioma = '" + idioma + "') " +
                " ORDER BY tblRefere.strRefere ";

                dstDatos = dsConsulta.Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                ExceptionHandled.Publicar("Consulta:  " + pstrSql);
            }
            return dstDatos;
        }
        //Consulta el id de una referencia mediante su llave, es decir el campo stroRefere
        public string ConsultarIdRefere(string Refere)
        {
            string Consulta = string.Empty;
            string IdRefere = string.Empty;
            try
            {
                Consulta = " SELECT intidRefere ";
                Consulta = Consulta + " FROM tblrefere ";
                Consulta = Consulta + " WHERE strRefere='" + Refere + "' and bitactivo=1";
                IdRefere = ConsultarCodigos(Consulta);//llamamos el metodo que nos devolvera el string con el dato solicitado
            }
            catch
            {
                ExceptionHandled.Publicar("Consulta:  " + Consulta + "/n  Respuesta:  " + IdRefere);
            }
            return IdRefere;
        }
        //Consulta el id de una referencia mediante su llave, es decir el campo stroRefere
        public string ConsultarIdRefere(string Refere, string strTipoRefere)
        {
            string Consulta = string.Empty;
            string IdRefere = string.Empty;
            try
            {
                Consulta = " SELECT a.intidRefere ";
                Consulta = Consulta + " FROM tblrefere a  INNER JOIN tblTipoRefere b on  a.intidtiporefere=b.intidtiporefere ";
                Consulta = Consulta + " WHERE a.strRefere='" + Refere + "' and b.strtiporefere='" + strTipoRefere + "' ";
                IdRefere = ConsultarCodigos(Consulta);//llamamos el metodo que nos devolvera el string con el dato solicitado
            }
            catch
            {
                ExceptionHandled.Publicar("Consulta:  " + Consulta + "/n  Respuesta:  " + IdRefere);
            }
            return IdRefere;
        }
        //metodo que devuelve la primera posicion de la primera fila del resultado de una consulta determinada
        public string ConsultarCodigos(string Consulta)
        {
            string Rta = null;
            try
            {
                //Utils.Utils pclsUtils = new Utils.Utils();
                DataSet pdtsGrid = new DataSet();
                dsConsulta.Conexion = Conexion;
                pdtsGrid = dsConsulta.Select(Consulta);
                if (pdtsGrid.Tables[0].Rows.Count > 0)
                {
                    //extraemos unicamente la primera posicion
                    Rta = pdtsGrid.Tables[0].Rows[0].ItemArray[0].ToString();
                }
            }
            catch
            {
                ExceptionHandled.Publicar("Consulta:  " + Consulta);
            }
            return Rta;
        }
    //Carga un control con el contenido de un DataTable determinado, devuelve un string si la operacion fue exitosa y un null si no lo fue
        public string LlenarControlData(Object obj, Enum_Controls Enum_Controls, string Value, string Text, bool Blank, bool Checked, string Valuecompare, DataTable dtConsulta)
        {
            Utils.Utils pclsUtils = new Utils.Utils();
            DataSet pdtsGrid = new DataSet();
            pdtsGrid = pclsUtils.AddDataset(dtConsulta);
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            cParametros.Message = "OK";
            try
            {
                if (pdtsGrid.Tables[0].Rows.Count > 0)
                {
                    switch (Enum_Controls)
                    {
                        case Enum_Controls.DropDownList:
                            clsControls.LlenaControl(((DropDownList)obj), pdtsGrid, Text, Value, Blank);
                            break;
                        case Enum_Controls.GridView:
                            clsControls.LlenaControl(((GridView)obj), pdtsGrid);
                            break;                       
                        case Enum_Controls.Repeater:
                            clsControls.LlenaControl(((Repeater)obj), pdtsGrid);
                            break;
                        case Enum_Controls.BulletedList:
                            clsControls.LlenaControl(((BulletedList)obj), pdtsGrid, Text, Value, Blank);
                            break;
                        case Enum_Controls.CheckBoxList:
                            if (Valuecompare == null || Valuecompare.Equals(""))
                            {
                                clsControls.LlenaControl(((CheckBoxList)obj), pdtsGrid, Text, Value, Blank, Checked);
                            }
                            else
                            {
                                clsControls.LlenaControl(((CheckBoxList)obj), pdtsGrid, Text, Value, Valuecompare, Blank, Checked);
                            }
                            break;
                        case Enum_Controls.DataList:
                            clsControls.LlenaControl(((DataList)obj), pdtsGrid);
                            break;
                    }
                    cParametros.Message = pdtsGrid.Tables[0].Rows[0].ItemArray[0].ToString();//obtenemos el primer item de la primera fila del dataset y la retornamos
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.Complemento = "CargarGridPlanes control WebDataGrid";
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros.Message;
        }
     //Carga un control con el contenido de un DataSet determinado, devuelve un string si la operacion fue exitosa y un null si no lo fue
        public string LlenarControlData(Object obj, Enum_Controls Enum_Controls, string Value, string Text, bool Blank, bool Checked, string Valuecompare, DataSet pdtsGrid)
        {
           
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            cParametros.Message = "OK";
            try
            {
                if (pdtsGrid.Tables[0].Rows.Count > 0)
                {
                    switch (Enum_Controls)
                    {
                        case Enum_Controls.DropDownList:
                            clsControls.LlenaControl(((DropDownList)obj), pdtsGrid, Text, Value, Blank);
                            break;
                        case Enum_Controls.GridView:
                            clsControls.LlenaControl(((GridView)obj), pdtsGrid);
                            break;                       
                        case Enum_Controls.Repeater:
                            clsControls.LlenaControl(((Repeater)obj), pdtsGrid);
                            break;
                        case Enum_Controls.BulletedList:
                            clsControls.LlenaControl(((BulletedList)obj), pdtsGrid, Text, Value, Blank);
                            break;
                        case Enum_Controls.CheckBoxList:
                            if (Valuecompare == null || Valuecompare.Equals(""))
                            {
                                clsControls.LlenaControl(((CheckBoxList)obj), pdtsGrid, Text, Value, Blank, Checked);
                            }
                            else
                            {
                                clsControls.LlenaControl(((CheckBoxList)obj), pdtsGrid, Text, Value, Valuecompare, Blank, Checked);
                            }
                            break;
                        case Enum_Controls.DataList:
                            clsControls.LlenaControl(((DataList)obj), pdtsGrid);
                            break;
                    }
                    cParametros.Message = pdtsGrid.Tables[0].Rows[0].ItemArray[0].ToString();//obtenemos el primer item de la primera fila del dataset y la retornamos
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.Complemento = "CargarGridPlanes control WebDataGrid";
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros.Message;
        }
  //Devuelve un dataset con los resultados de una consulta determinada
        public DataSet ConsultaTabla(string Consulta)
        {
            DataSet Rta = new DataSet();
            try
            {
                dsConsulta.Conexion = Conexion;
                Rta = dsConsulta.Select(Consulta);
            }
            catch
            {
                ExceptionHandled.Publicar("Consulta:  " + Consulta);
            }
            return Rta;
        }
        /// <summary>
        /// Manejo de Meta tags y Titulos de la pagina
        /// </summary>
        /// <param name="sPagina">Nombre de la pagina</param>
        /// <param name="bSoloActivo">se indica si muestra solamente los activos</param>
        /// <returns></returns>
        public DataSet MetaTag(string sPagina, bool bSoloActivo)
        {
            DataSet dsData = new DataSet();
            try
            {
                string sIdioma = clsSesiones.getIdioma();
                string sAplicacion = clsSesiones.getAplicacion().ToString();
                string sTipoRefereTag = clsValidaciones.GetKeyOrAdd("MetaTag", "MetaTag");
                string sTipoRefereEquiv = clsValidaciones.GetKeyOrAdd("HttpEquiv", "HttpEquiv");
                string sIdEmpresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "2");
                string sPaginaNew = sPagina;

                if (sPaginaNew.Contains("."))
                {
                    sPaginaNew = sPaginaNew.Substring(0, sPagina.IndexOf("."));
                }
                else
                {
                    sPaginaNew = sPaginaNew + ".aspx";
                }
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    sIdEmpresa = cCache.Empresa;
                }
                StringBuilder Consulta = new StringBuilder();

                Consulta.AppendLine(" SELECT     strDetalle As Title ");
                Consulta.AppendLine(" FROM         tblRefere ");
                Consulta.AppendLine(" WHERE (strIdioma = '" + sIdioma + "') ");
                Consulta.AppendLine(" AND (intidRefere = " + sAplicacion + ") ");
                Consulta.AppendLine(" AND (intAplicacion = " + sAplicacion + ") ");
                Consulta.AppendLine("  ");
                Consulta.AppendLine("  ");
                Consulta.AppendLine(" SELECT     tblRefere.strRefere As Name, tblRefere.strValor As Content ");
                Consulta.AppendLine(" FROM         tblRefere INNER JOIN ");
                Consulta.AppendLine(" tblTipoRefere ON tblRefere.intidTipoRefere = tblTipoRefere.intidTipoRefere ");
                Consulta.AppendLine(" WHERE     (tblTipoRefere.strTipoRefere = '" + sTipoRefereTag + "') ");
                Consulta.AppendLine(" AND (tblRefere.strIdioma = '" + sIdioma + "') ");
                Consulta.AppendLine(" AND (tblRefere.intAplicacion = " + sAplicacion + ") ");
                if (bSoloActivo)
                    Consulta.AppendLine(" AND (tblRefere.bitActivo = 1) ");

                Consulta.AppendLine("  ");
                Consulta.AppendLine("  ");
                Consulta.AppendLine(" SELECT     tblRefere.strRefere As HttpEquiv, tblRefere.strValor As Content ");
                Consulta.AppendLine(" FROM         tblRefere INNER JOIN ");
                Consulta.AppendLine(" tblTipoRefere ON tblRefere.intidTipoRefere = tblTipoRefere.intidTipoRefere ");
                Consulta.AppendLine(" WHERE     (tblTipoRefere.strTipoRefere = '" + sTipoRefereEquiv + "') ");
                Consulta.AppendLine(" AND (tblRefere.strIdioma = '" + sIdioma + "') ");
                Consulta.AppendLine(" AND (tblRefere.intAplicacion = " + sAplicacion + ") ");
                if (bSoloActivo)
                    Consulta.AppendLine(" AND (tblRefere.bitActivo = 1) ");

                Consulta.AppendLine("  ");
                Consulta.AppendLine("  ");
                Consulta.AppendLine(" SELECT     tblRefere.strRefere As Name, tblRelaRefere.strDetalle AS Content ");
                Consulta.AppendLine(" FROM         tblRefere AS tblRefere_1 INNER JOIN tblRelaRefere INNER JOIN ");
                Consulta.AppendLine(" tblRefere ON tblRelaRefere.intidRefere = tblRefere.intidRefere AND tblRelaRefere.intIdTipoRefere = tblRefere.intidTipoRefere AND  ");
                Consulta.AppendLine(" tblRelaRefere.intAplicacion = tblRefere.intAplicacion INNER JOIN ");
                Consulta.AppendLine(" tblTipoRefere ON tblRefere.intidTipoRefere = tblTipoRefere.intidTipoRefere ON tblRefere_1.intidRefere = tblRelaRefere.intConsecutivo ");
                Consulta.AppendLine(" WHERE     (tblTipoRefere.strTipoRefere = '" + sTipoRefereTag + "') ");
                Consulta.AppendLine(" AND (tblRefere.strIdioma = '" + sIdioma + "') ");
                Consulta.AppendLine(" AND (tblRelaRefere.strIdioma = '" + sIdioma + "') ");
                Consulta.AppendLine(" AND (tblRefere.intAplicacion = " + sAplicacion + ") ");
                Consulta.AppendLine(" AND (tblRefere_1.strRefere = '" + sPagina + "') ");
                Consulta.AppendLine(" AND (tblRelaRefere.intCodigo = " + sIdEmpresa + ") ");
                if (bSoloActivo)
                    Consulta.AppendLine(" AND (tblRelaRefere.bitActivo = 1) ");
                Consulta.AppendLine("  ");
                Consulta.AppendLine("  ");
                Consulta.AppendLine("  SELECT     tblcontactos.* ");
                Consulta.AppendLine("  FROM         tblcontactos ");
                Consulta.AppendLine("  WHERE     (intContacto = " + sIdEmpresa + ") ");
                if (bSoloActivo)
                    Consulta.AppendLine(" AND (bitActivo = 1) ");

                Consulta.AppendLine("  ");
                Consulta.AppendLine("  ");
                Consulta.AppendLine(" SELECT     tblRefere.strRefere As Name, tblRelaRefere.strDetalle AS Content ");
                Consulta.AppendLine(" FROM         tblRefere AS tblRefere_1 INNER JOIN tblRelaRefere INNER JOIN ");
                Consulta.AppendLine(" tblRefere ON tblRelaRefere.intidRefere = tblRefere.intidRefere AND tblRelaRefere.intIdTipoRefere = tblRefere.intidTipoRefere AND  ");
                Consulta.AppendLine(" tblRelaRefere.intAplicacion = tblRefere.intAplicacion INNER JOIN ");
                Consulta.AppendLine(" tblTipoRefere ON tblRefere.intidTipoRefere = tblTipoRefere.intidTipoRefere ON tblRefere_1.intidRefere = tblRelaRefere.intConsecutivo ");
                Consulta.AppendLine(" WHERE     (tblTipoRefere.strTipoRefere = '" + sTipoRefereTag + "') ");
                Consulta.AppendLine(" AND (tblRefere.strIdioma = '" + sIdioma + "') ");
                Consulta.AppendLine(" AND (tblRelaRefere.strIdioma = '" + sIdioma + "') ");
                Consulta.AppendLine(" AND (tblRefere.intAplicacion = " + sAplicacion + ") ");
                Consulta.AppendLine(" AND (tblRefere_1.strRefere = '" + sPaginaNew + "') ");
                Consulta.AppendLine(" AND (tblRelaRefere.intCodigo = " + sIdEmpresa + ") ");
                if (bSoloActivo)
                    Consulta.AppendLine(" AND (tblRelaRefere.bitActivo = 1) ");

                dsData = ConsultaTabla(Consulta.ToString());
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "Meta Tag - Consulta ";
                ExceptionHandled.Publicar(cParametros);
            }
            return dsData;
        }
        public void sConsultaGeneros(DropDownList ddlGenero, bool vBlanco)
        {
            string sIdioma = new csCache().cCache().Idioma;
            DataTable dtData = new CsConsultasVuelos().SPConsultaTabla("SPCONSULTAGENERO", new string[1] { sIdioma });
            clsControls.LlenaControl(ddlGenero, dtData, "STRDESCRIPCION", "INTCODE", vBlanco);
        }
        public void sConsultaTposIdentificacion(DropDownList ddlGenero, bool vBlanco)
        {
            string sIdioma = new csCache().cCache().Idioma;
            DataTable dtData = new CsConsultasVuelos().SPConsultaTabla("SPConsultaTpoidentifica", new string[1] { sIdioma });
            clsControls.LlenaControl(ddlGenero, dtData, "STRDESCRIPCION", "INTCODE", vBlanco);
        }
    }
}
