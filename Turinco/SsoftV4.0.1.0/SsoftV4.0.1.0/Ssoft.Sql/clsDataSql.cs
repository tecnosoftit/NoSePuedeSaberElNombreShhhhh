using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Ssoft.Data;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;
using System.Configuration;

namespace Ssoft.Sql
{
    public class DataSql
    {
        protected string gstrConexion = string.Empty;

        public DataSql()
        {
            this.Conexion = clsSesiones.getConexion();
        }
        public string Conexion
        {
            set { this.gstrConexion = value; }
            get { return this.gstrConexion; }
        }
        public DataSet ConsultaTabla(string Sql)
        {
            DataSet dstDatos = new DataSet();
            try
            {
                dstDatos = AccesoSQL.ExecuteDataset(this.Conexion, CommandType.Text, Sql);
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Alta;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Complemento = "Libreria: DataSql.  Conexion " + Conexion + ". Procedimiento: ConsultaTabla. Sql: " + Sql;
                ExceptionHandled.Publicar(cMensaje);
            }
            return dstDatos;
        }
        public DataSet Select(string Sql)
        {
            DataSet dstDatos = new DataSet();
            try
            {
                dstDatos = AccesoSQL.ExecuteDataset(this.Conexion, CommandType.Text, Sql);
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Alta;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Complemento = "Libreria: DataSql.  Conexion " + Conexion + ". Procedimiento: Select. Sql: " + Sql;
                ExceptionHandled.Publicar(cMensaje);
            }
            return dstDatos;
        }
        public DataSet Select(string sSp, string[] sParameters)
        {
            DataSet dstDatos = new DataSet();
            try
            {
                SqlParameter[] param = AccesoSQLParameterCache.GetSpParameterSet(this.Conexion, sSp);
                for (int i = 0; i < sParameters.Length; i++)
                {
                    param[i].Value = sParameters[i].ToString();
                }
                dstDatos = AccesoSQL.ExecuteDataset(this.Conexion, CommandType.StoredProcedure, sSp, param);
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Alta;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Complemento = "Libreria: DataSql.  Conexion " + Conexion + ". Procedimiento: Select con Sp y Parameros";
                ExceptionHandled.Publicar(cMensaje);
            }
            return dstDatos;
        }
        public DataSet SelectSp(string sSp, string[] sParameters)
        {
            DataSet dstDatos = new DataSet();
            try
            {
                SqlParameter[] param = AccesoSQLParameterCache.GetSpParameterSet(this.Conexion, sSp);
                for (int i = 0; i < sParameters.Length; i++)
                {
                    param[i].Value = sParameters[i].ToString();
                }
                dstDatos = AccesoSQL.ExecuteDataset(this.Conexion, CommandType.StoredProcedure, sSp, param);
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Alta;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Complemento = "Libreria: DataSql.  Conexion " + Conexion + ". Procedimiento: Select con Sp y Parameros";
                ExceptionHandled.Publicar(cMensaje);
            }
            return dstDatos;
        }
        public DataTable SelectSpTable(string sSp, string[] sParameters)
        {
            DataSet dstDatos = new DataSet();
            DataTable dtDatos = new DataTable();
            try
            {
                SqlParameter[] param = AccesoSQLParameterCache.GetSpParameterSet(this.Conexion, sSp);
                for (int i = 0; i < sParameters.Length; i++)
                {
                    if (param[i].DbType.ToString().Equals("Boolean"))
                    {
                        param[i].Value =Convert.ToInt16(sParameters[i].ToString());
                    }                  
                    else
                    {
                        param[i].Value = sParameters[i].ToString();
                    }
                }
                dstDatos = AccesoSQL.ExecuteDataset(this.Conexion, CommandType.StoredProcedure, sSp, param);

                if (dstDatos.Tables.Count > 0)
                {
                    dtDatos = dstDatos.Tables[0];

                }
                else
                {
                    dtDatos = null;
                }
                
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Alta;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Complemento = "Libreria: DataSql.  Conexion " + Conexion + ". Procedimiento: Select con Sp y Parameros";
                ExceptionHandled.Publicar(cMensaje);
            }
            return dtDatos;
        }
        public string Consulta(string SConsulta)
        {
            DataSet dstDatos = new DataSet();
            string sResult=string.Empty;
            try
            {
                dstDatos = AccesoSQL.ExecuteDataset(this.Conexion, CommandType.Text, SConsulta);
                if (dstDatos.Tables.Count > 0)
                { 
                  if( dstDatos.Tables[0].Rows.Count > 0)
                    {                       
                        sResult=dstDatos.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Alta;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Complemento = "Libreria: DataSql.  Conexion " + Conexion + ". Procedimiento: ConsultaTabla. Sql: " + SConsulta;
                ExceptionHandled.Publicar(cMensaje);
            }
            return sResult;
        }
        public DataSet SelectSp(string sSp)
        {
            DataSet dstDatos = new DataSet();
            try
            {
                dstDatos = AccesoSQL.ExecuteDataset(this.Conexion, CommandType.StoredProcedure, sSp);
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Alta;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Complemento = "Libreria: DataSql.  Conexion " + Conexion + ". Procedimiento: Select con Sp sin Parameros";
                ExceptionHandled.Publicar(cMensaje);
            }
            return dstDatos;
        }
        public void UpdateInsert(string Sql)
        {
            int Respuesta;
            Respuesta = AccesoSQL.ExecuteNonQuery(this.Conexion, CommandType.Text, Sql);
        }
        public void UpdateInsert(string Sql, string SqlAdicional)
        {
            int Respuesta;
            try
            {
                Respuesta = AccesoSQL.ExecuteNonQuery(this.Conexion, CommandType.Text, Sql);
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Alta;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Complemento = "Libreria: DataSql.  Conexion " + Conexion + ". Procedimiento: UpdateInsert. Sql: " + Sql;
                ExceptionHandled.Publicar(cMensaje);
                try
                {
                    Respuesta = AccesoSQL.ExecuteNonQuery(this.Conexion, CommandType.Text, SqlAdicional);
                }
                catch (Exception Exp)
                {
                    clsParametros cMensaje1 = new clsParametros();
                    cMensaje1.Id = 0;
                    cMensaje1.Message = Exp.Message.ToString();
                    cMensaje1.Source = Exp.Source.ToString();
                    cMensaje1.Tipo = clsTipoError.Library;
                    cMensaje1.Severity = clsSeveridad.Alta;
                    cMensaje1.StackTrace = Exp.StackTrace.ToString();
                    cMensaje1.Complemento = "Libreria: DataSql.  Conexion " + Conexion + ". Procedimiento: UpdateInsert. Sql: " + SqlAdicional;
                    ExceptionHandled.Publicar(cMensaje1);
                }
            }
        }
        public int MaxConsec(string ptblTable, string pstrField, TipoCampo pTipoCampo)
        {
            string pstrSql;
            int pintConsecutivo = 0;
            if (pTipoCampo == TipoCampo.String)
                pstrSql = "SELECT     MAX(Convert(int, " + pstrField + ")) AS Consecutivo FROM  " + ptblTable;
            else
                pstrSql = "SELECT     MAX(" + pstrField + ") AS Consecutivo FROM  " + ptblTable;

            DataSet dstDatos = new DataSet();
            try
            {
                dstDatos = new DataSql().Select(pstrSql);
                if (dstDatos.Tables[0].Rows.Count > 0)
                {
                    pintConsecutivo = int.Parse(dstDatos.Tables[0].Rows[0]["Consecutivo"].ToString());
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Alta;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Complemento = "Libreria: DataSql. Procedimiento: MaxConsec";
                ExceptionHandled.Publicar(cMensaje);
            }
            return pintConsecutivo;
        }
        public int MaxConsec(string ptblTable, string pstrField, TipoCampo pTipoCampo, string pstrWhere)
        {
            string pstrSql;
            int pintConsecutivo = 0;
            if (pTipoCampo == TipoCampo.String)
                pstrSql = "SELECT     MAX(Convert(int, " + pstrField + ")) AS Consecutivo FROM  " + ptblTable + " Where " + pstrWhere;
            else
                pstrSql = "SELECT     MAX(" + pstrField + ") AS Consecutivo FROM  " + ptblTable + " Where " + pstrWhere;

            DataSet dstDatos = new DataSet();
            try
            {
                dstDatos = new DataSql().Select(pstrSql);
                if (dstDatos.Tables[0].Rows.Count > 0)
                {
                    pintConsecutivo = int.Parse(dstDatos.Tables[0].Rows[0]["Consecutivo"].ToString());
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Alta;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Complemento = "Libreria: DataSql. Procedimiento: MaxConsec";
                ExceptionHandled.Publicar(cMensaje);
            }
            return pintConsecutivo;
        }
    }
}
