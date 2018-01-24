using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ssoft.Sql;
using System.Data;
using Ssoft.Data;
using System.Configuration;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;

namespace SsoftQuery.Vuelos
{
    public  class CsConsultasVuelos
    {
        protected DataSql dConsulta = new DataSql();
        protected DataSet dsConsulta = new DataSet(); 
        protected DataTable dtConsulta = new DataTable();
        protected string strConsulta = string.Empty;
        protected string strConexion = ConfigurationManager.AppSettings["strConexion"].ToString();
        /// <summary>
        /// Metodo que consulta una tabla y retorna un string  
        /// </summary>
        /// <param name="StrCodigoReferencia">codigo de referencia a consultar</param>
        /// <param name="strTable">tabla a consultar</param>
        /// <param name="strColumn">Columna a consultar</param>
        /// <returns>String con  el valor de la columna  enviada por parametro</returns>
        public string ConsultaCodigo(string StrCodigoReferencia,string strTable, string strColumn,string sWhere)
        {

            dsConsulta = dConsulta.ConsultaTabla("SELECT TOP(1) " + strColumn + " FROM " + strTable + " WHERE " + sWhere + "='" + StrCodigoReferencia + "'");
            if (dsConsulta.Tables.Count > 0)
            {
                if (dsConsulta.Tables[0].Rows.Count > 0)
                {
                    strConsulta = dsConsulta.Tables[0].Rows[0][0].ToString();
                }
            }


            return strConsulta;
        }
        public DataTable Consultatabla(string StrCodigoReferencia, string strTable, string[] strColumn,string sWhere)
        {
            string strColumnas = string.Empty;
            try
            {
                if (strColumn.Length > 0)
                {
                    for (int i = 0; i <= strColumn.Length-1; i++)
                    {
                        strColumnas += strColumn[i].ToString();
                        if (i < strColumn.Length - 1)
                        {
                            strColumnas += ",";
                        }
                    }
                   

                }
                else
                {
                    strColumnas = strColumn[0].ToString();
                }

                if (sWhere != "" && sWhere != null && StrCodigoReferencia != "" && StrCodigoReferencia != null)
                {
                    sWhere = "WHERE " + sWhere+ "='" + StrCodigoReferencia + "'";
                }
                else
                {
                    sWhere = "";       
                }

                if (strColumnas == "" || strColumnas==null)
                {
                    strColumnas = "*";                
                }
            }
            catch { }

            dsConsulta = dConsulta.ConsultaTabla("SELECT " + strColumnas + " FROM " + strTable + " " + sWhere );
            if (dsConsulta.Tables.Count > 0)
            {
                if (dsConsulta.Tables[0].Rows.Count > 0)
                {
                    dtConsulta = dsConsulta.Tables[0];
                }
            }

            return dtConsulta;

        }
        public DataTable Consultatabla(string StrCodigoReferencia, string strTable, string strColumn, string sWhere)
        {
           
            try
            {
                if (strColumn == "" || strColumn == null)
                {
                    strColumn = "*";
                }

                if (sWhere != "" && sWhere != null && StrCodigoReferencia != "" && StrCodigoReferencia != null)
                {
                    sWhere = "WHERE " + sWhere + "='" + StrCodigoReferencia + "'";
                }
                else
                {
                    sWhere = "";
                }
            }
            catch { }

            dsConsulta = dConsulta.ConsultaTabla("SELECT " + strColumn + " FROM " + strTable + " " + sWhere);
            if (dsConsulta.Tables.Count > 0)
            {
                if (dsConsulta.Tables[0].Rows.Count > 0)
                {
                    dtConsulta = dsConsulta.Tables[0];
                }
            }

            return dtConsulta;

        }
        public DataTable Consultatabla(string Sql)
        {

            dsConsulta = dConsulta.ConsultaTabla(Sql);
            if (dsConsulta.Tables.Count > 0)
            {
                if (dsConsulta.Tables[0].Rows.Count > 0)
                {
                    dtConsulta = dsConsulta.Tables[0];
                }
                else
                {
                    dtConsulta = null;
                }
            }

            return dtConsulta;

        }
        public void EjecutaUpdate(string strTable, string[] strColumn, string[] strValores, string sWhere, string strCodigoFiltro)
        {

            if (strColumn.Length == strValores.Length && sWhere != "" && strCodigoFiltro !="")
            {
                string strColumnas = string.Empty;
                try
                {
                    if (strColumn.Length > 0)
                    {
                        for (int i = 0; i <= strColumn.Length - 1; i++)
                        {
                            if (strColumn[i] != "" && strValores[i] != "")
                            {
                                strColumnas += strColumn[i].ToString();
                                strColumnas += '=' + strValores[i];
                                if (i < strColumn.Length - 1)
                                {
                                    strColumnas += ",";
                                }
                            }
                        }

                    }
                    else
                    {
                        strColumnas = strColumn[0].ToString();
                        strColumnas +="="+strValores[0].ToString();
                    }

                    if (sWhere != "" && sWhere != null && strCodigoFiltro != "" && strCodigoFiltro != null)
                    {
                        sWhere = "WHERE " + sWhere + "='" + strCodigoFiltro + "'";
                    }
                    else
                    {
                        sWhere = "WHERE No_valido='novalido'";
                    }

                    if (strColumnas == "" || strColumnas == null)
                    {
                        strColumnas = "*";
                    }

                    dsConsulta = dConsulta.ConsultaTabla("UPDATE " + strTable + " SET values(" + strColumn + ") " + sWhere + " " + strCodigoFiltro);


                }
                catch { }
            }
        }
        public DataTable SPConsultaTabla(string sPNombre, string[] sParametros)
        {
            DataTable dtRta = null;
            dtConsulta = new DataSql().SelectSpTable(sPNombre, sParametros);
            if (dtConsulta != null)
            {
                if (dtConsulta.Rows.Count > 0)
                    dtRta = dtConsulta;
            }


            return dtRta;
        }
        public string EjecutaProcedimiento(string sPNombre, string[] sParametros)
        {
            DataTable dtRta = null;            
            dtConsulta = new DataSql().SelectSpTable(sPNombre, sParametros);
            if (dtConsulta != null)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    dtRta = dtConsulta;
                    strConsulta = dtRta.Rows[0][0].ToString();
                }
            }


            return strConsulta;
        }
        public string EjecutarSPConsulta(string sPNombre, string[] StrListaParametros)
        {
            string sConsulta = string.Empty;
            sConsulta = "EXEC " + sPNombre + " ";
            for (int i = 0; i < StrListaParametros.Length; i++)
            {
                sConsulta += StrListaParametros[i].ToString();
                if (i != StrListaParametros.Length - 1)
                {
                    sConsulta += ",";
                }
            }
            string sResult = dConsulta.Consulta(sConsulta);
            return sResult;
        }
        public DataSet ConsultarReserva(int intProyecto)
        {
            string pstrSql = string.Empty;
            DataSet dstDatos = new DataSet();

            try
            {
                pstrSql = " SELECT   TBLPROYECTO.INTPROYECTO, TBLPROYECTO.DTMVENCIMIENTO,TBLRESMASTER.STRRESERVA, TBLRESMASTER.INTTIPOSERVICIO,   " +
                            "  TBLTPOSERVICIO.STRCODIGO,TBLRESMASTER.DTMMFECHA, TBLUSUARIOS.STRNOMBRE, TBLUSUARIOS.STRAPELLIDO,   " +
                            "  TBLRESMASTER.INTFORMAPAGO, TBLFOPIDIOMA.STRDESCRIPCION, TBLRESMASTER.INTESTADOPAGO,   " +
                            "  TBLESTADOSRESERVAIDIOMA.STRDESCRIPCION AS ESTADOPAGO,TBLRESMASTER.STRMOTIVOCANCEL, TBLRESMASTER.DBLVALORCANCEL,TBLRESMASTER.STROBSERVACION, TBLRESMASTER.STRLOCALIZADOREXT   " +
                            "  FROM  TBLPROYECTO" +
                            "  INNER JOIN  TBLRESMASTER ON TBLPROYECTO.INTPROYECTO = TBLRESMASTER.INTPROYECTO" +
                            "  LEFT OUTER JOIN    TBLUSUARIOS ON TBLRESMASTER.INTCONTACTO = TBLUSUARIOS.INTUSUARIO " +
                            "  LEFT OUTER JOIN    TBLESTADOS_RESERVA ON TBLRESMASTER.INTESTADO = TBLESTADOS_RESERVA.INTCODE   " +
                            "  INNER JOIN         TBLESTADOSRESERVAIDIOMA ON TBLESTADOSRESERVAIDIOMA.INTESTADORESERVACODE=TBLESTADOS_RESERVA.INTCODE AND TBLESTADOSRESERVAIDIOMA.STRIDIOMA='" + new csCache().cCache().Idioma + "'" +
                            "  LEFT OUTER JOIN    TBLFOP  ON TBLRESMASTER.INTFORMAPAGO = TBLFOP.INTIDFOP " +
                            "  INNER JOIN         TBLFOPIDIOMA ON TBLFOPIDIOMA.INTFOPID=TBLFOP.INTIDFOP AND TBLFOPIDIOMA.STRIDIOMA='" + new csCache().cCache().Idioma + "'" +
                            "  LEFT OUTER JOIN    TBLESTADOSPAGO ON TBLRESMASTER.INTESTADOPAGO = TBLESTADOSPAGO.INTCODIGO " +
                            "  INNER JOIN         TBLESTADOSPAGOIDIOMA ON TBLESTADOSPAGO.INTCODIGO=TBLESTADOSPAGOIDIOMA.INTCODIGO AND TBLESTADOSPAGOIDIOMA.STRIDIOMA='" + new csCache().cCache().Idioma + "'" +
                            "  LEFT OUTER JOIN    TBLTPOSERVICIO ON TBLRESMASTER.INTTIPOSERVICIO = TBLTPOSERVICIO.INTID " +
                            "  WHERE  (TBLRESMASTER.INTPROYECTO = " + intProyecto + ") " +
                            "  ORDER BY TBLRESMASTER.STRRESERVA ";

                pstrSql = pstrSql + "  SELECT tblproyecto.intProyecto, tblproyecto.dtmVencimiento, tblresmaster.strReserva,   " +
                            "  tblresmaster.intTipoServicio AS TipoPlan, tblrestransac.intSegmento, tblrestransac.intEstado,  " +
                            "  Tblestadosreservaidioma.strdescripcion AS EstadoRes, tblrestransac.strOperador, tblrestransac.dtmFechaIni, tblrestransac.strHoraIni,   " +
                            "  tblrestransac.dtmFechaFin, tblrestransac.strHoraFin, tblrestransac.intOrigen,tblrestransac.intDestino, tblrestransac.intCantidadPersonas, " +
                            "  tblrestransac.intProveedor,tblrestransac.strObservacion, tblrestransac.intTipoAcomodacion,tblrestransac.intTipoHabitacion   " +
                            "  FROM  tblproyecto " +
                            "  INNER JOIN  tblresmaster ON tblproyecto.intProyecto = tblresmaster.intProyecto   " +
                            "  INNER JOIN  tblrestransac ON tblresmaster.intCodigo = tblrestransac.intCodigoMaster  " +
                            "  LEFT OUTER JOIN tblEstados_Reserva ON tblresmaster.intEstado = tblEstados_Reserva.intcode " +
                            "  INNER JOIN  Tblestadosreservaidioma ON Tblestadosreservaidioma.intestadoreservacode=tblEstados_Reserva.intcode and Tblestadosreservaidioma.stridioma='" + new csCache().cCache().Idioma + "'" +
                            "  WHERE  (tblresmaster.intProyecto = " + intProyecto + ") " +
                            "  ORDER BY tblresmaster.strReserva, tblrestransac.intSegmento ";

                pstrSql = pstrSql + "  SELECT TBLRESMASTER.INTPROYECTO,TBLRESMASTER.STRRESERVA,TBLRESPAX.INTTIPOPAX,TBLTPOPAX.STRTIPOPAX,TBLTPOPAXIDIOMA.STRDESCRIPCION, " +
                                    "  TBLRESPAX.STRNOMBRE,TBLRESPAX.INTEDAD,TBLRESPAX.DTMFECHANAC,TBLRESFARE.INTMONEDA,TBLMONEDASIDIOMA.STRDESCRIPCION,TBLRESFARE.DBLVALOR,   " +
                                    "  TBLRESFARE.DBLTAX, TBLRESFARE.DBLTOTAL, TBLRESFARE.DBLPENALIDAD,DBLDESCUENTO    " +
                                    "  FROM  TBLRESFARE" +
                                    "  RIGHT OUTER JOIN TBLRESPAX ON TBLRESFARE.INTCODIGOMASTER = TBLRESPAX.INTCODIGOMASTER" +
                                    "  LEFT OUTER JOIN    TBLMONEDAS ON TBLRESFARE.INTMONEDA = TBLMONEDAS.INTCODE  " +
                                    "  INNER JOIN        TBLMONEDASIDIOMA ON TBLMONEDAS.INTCODE=TBLMONEDASIDIOMA.INTCODMONEDA AND TBLMONEDASIDIOMA.STRIDIOMA='" + new csCache().cCache().Idioma + "'" +
                                    "  RIGHT OUTER JOIN    TBLRESMASTER ON TBLRESPAX.INTCODIGOMASTER = TBLRESMASTER.INTCODIGO   " +
                                    "  LEFT OUTER JOIN    TBLTPOPAX ON TBLRESPAX.INTTIPOPAX = TBLTPOPAX.INTCODE   " +
                                    "  INNER JOIN     TBLTPOPAXIDIOMA ON TBLTPOPAX.INTCODE=TBLTPOPAXIDIOMA.INTTPOPAXID AND TBLTPOPAXIDIOMA.STRIDIOMA='" + new csCache().cCache().Idioma + "'" +
                                    "  WHERE  (TBLRESMASTER.INTPROYECTO = " + intProyecto + ") " +
                                    "  ORDER BY  TBLRESMASTER.STRRESERVA ";

                pstrSql = pstrSql + "  SELECT  TBLRESMASTER.INTPROYECTO,TBLRESMASTER.STRRESERVA,TBLRESFARE.INTTIPOPAX,TBLTPOPAX.STRTIPOPAX,TBLTPOPAXIDIOMA.STRDESCRIPCION," +
                                     "  TBLRESPAX.STRNOMBRE,TBLRESPAX.INTEDAD,TBLRESPAX.DTMFECHANAC,TBLRESFARETAX.INTCODIGO,TBLIMPUESTOSSABRE.STRCODE,TBLIMPUESTOSSABRE.STRDESCRIPTION,   " +
                                     "  TBLRESFARETAX.DBLPORCENT, TBLRESFARETAX.INTMONEDA,TBLMONEDASIDIOMA.STRDESCRIPCION,TBLRESFARETAX.DBLVALORTAX " +
                                     "  FROM  TBLRESFARE " +
                                     "  INNER JOIN TBLRESFARETAX ON TBLRESFARE.INTCODIGO = TBLRESFARETAX.INTCODIGOFARE" +
                                     "  INNER JOIN TBLRESMASTER ON TBLRESMASTER.INTCODIGO=TBLRESFARE.INTCODIGOMASTER  " +
                                     "  INNER JOIN TBLRESPAX  ON  TBLRESMASTER.INTCODIGO=TBLRESPAX.INTCODIGOMASTER" +
                                     "  INNER JOIN TBLIMPUESTOSSABRE ON TBLIMPUESTOSSABRE.INTCODE = TBLRESFARETAX.INTTPOTAX" +
                                     "  INNER JOIN TBLMONEDAS ON TBLMONEDAS.INTCODE=TBLRESFARE.INTMONEDA  " +
                                     "  INNER JOIN TBLMONEDASIDIOMA ON TBLMONEDAS.INTCODE=TBLMONEDASIDIOMA.INTCODMONEDA AND TBLMONEDASIDIOMA.STRIDIOMA='" + new csCache().cCache().Idioma + "'" +
                                     "  INNER JOIN TBLTPOPAX ON TBLRESFARE.INTTIPOPAX = TBLTPOPAX.INTCODE" +
                                     "  INNER JOIN TBLTPOPAXIDIOMA ON TBLTPOPAX.INTCODE=TBLTPOPAXIDIOMA.INTTPOPAXID AND TBLTPOPAXIDIOMA.STRIDIOMA='" + new csCache().cCache().Idioma + "'" +
                                     "  WHERE  (TBLRESMASTER.INTPROYECTO = " + intProyecto + ") " +
                                     "  ORDER BY  TBLRESMASTER.STRRESERVA ";


                dstDatos = dConsulta.Select(pstrSql);
                dstDatos.Tables[0].TableName = "tblReserva";
                dstDatos.Tables[1].TableName = "tblTransac";
                dstDatos.Tables[2].TableName = "tblPax";
                //dstDatos.Tables[3].TableName = "tblFare";
                dstDatos.Tables[3].TableName = "tblFareTax";

            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar("Error generado, SQL ejecutado " + pstrSql);
                ExceptionHandled.Publicar(Ex);
            }
            return dstDatos;
        }
        public DataSet EjecutaSpProcedimiento(string sPNombre, string[] sParametros)
        {
            dsConsulta = new DataSql().Select(sPNombre, sParametros);
            if (dsConsulta != null)
            {
                if (dsConsulta.Tables.Count <= 0)
                {
                    dsConsulta = null;
                }

            }


            return dsConsulta;
        }
    }
}
