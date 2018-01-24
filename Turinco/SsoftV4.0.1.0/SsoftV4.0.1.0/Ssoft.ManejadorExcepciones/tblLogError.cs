using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Sql;
using System.Data;
using Ssoft.ManejadorExcepciones;
using System.Configuration;
using Ssoft.Utils;

namespace Ssoft.ManejadorExcepciones
{
    /// <summary>
    /// Clase de la tabla de GroupUser
    /// Manejo de grupos de usuarios
    /// </summary>
    /// Autor:          José Faustino Posas
    /// Company:        Ssoft Colombia
    /// Fecha:          01/20/2009
    /// Control de Cambios
    /// Autor:          
    /// Fecha:          
    /// Descripción:
    public class tblLogError
    {
        #region [Atributos y Propiedades]

        /// <summary>
        /// Definición de instancias del método
        /// </summary>
        private static string sFormatoFecha = clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");
        private static string sFormatoFechaBD = clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");
        private static DataSql pclsDataSql = new DataSql();
        private static Sql.Sql pclsSql = new Sql.Sql();

        /// <summary>
        /// Definición de atributos
        /// </summary>
        protected string gstrNameTable = "tblLogError";
        protected string gstrConexionTable = string.Empty;

        protected field gintidCodigo = new field();
        protected field gintAplicacion = new field();
        protected field gintId = new field();
        protected field gstrCode = new field();
        protected field gstrTipoLog = new field();
        protected field gstrTipo = new field();
        protected field gstrSeverity = new field();
        protected field gstrMetodo = new field();
        protected field gstrMessage = new field();
        protected field gstrInfo = new field();
        protected field gstrSource = new field();
        protected field gstrStackTrace = new field();
        protected field gstrTargetSite = new field();
        protected field gstrInnerException = new field();
        protected field gstrComplemento = new field();
        protected field gstrViewMessage = new field();
        protected field gstrSugerencia = new field();
        protected field gstrIp = new field();
        protected field gstrExplorer = new field();
        protected field gstrPage = new field();
        protected field gstrPlataform = new field();
        protected field gstrHostName = new field();
        protected field gstrSesion = new field();
        protected field gdtmCFecha = new field();
        protected field gdtmMFecha = new field();
        protected field gintContactoUser = new field();
        protected bool gbolRespuesta = true;

        public bool Respuesta
        {
            get { return gbolRespuesta; }
            set { gbolRespuesta = value; }
        }

        /// <summary>
        /// Definición de propiedades
        /// </summary>
        public string Nombre
        {
            get { return gstrNameTable; }
        }

        public string Conexion
        {
            get { return gstrConexionTable; }
            set { gstrConexionTable = value; }
        }

        public field intidCodigo
        {
            get { return gintidCodigo; }
            set { gintidCodigo = value; }
        }

        public field intAplicacion
        {
            get { return gintAplicacion; }
            set { gintAplicacion = value; }
        }

        public field intId
        {
            get { return gintId; }
            set { gintId = value; }
        }

        public field strCode
        {
            get { return gstrCode; }
            set { gstrCode = value; }
        }

        public field strTipoLog
        {
            get { return gstrTipoLog; }
            set { gstrTipoLog = value; }
        }

        public field strTipo
        {
            get { return gstrTipo; }
            set { gstrTipo = value; }
        }

        public field strSeverity
        {
            get { return gstrSeverity; }
            set { gstrSeverity = value; }
        }

        public field strMetodo
        {
            get { return gstrMetodo; }
            set { gstrMetodo = value; }
        }

        public field strMessage
        {
            get { return gstrMessage; }
            set { gstrMessage = value; }
        }

        public field strInfo
        {
            get { return gstrInfo; }
            set { gstrInfo = value; }
        }

        public field strSource
        {
            get { return gstrSource; }
            set { gstrSource = value; }
        }

        public field strStackTrace
        {
            get { return gstrStackTrace; }
            set { gstrStackTrace = value; }
        }

        public field strTargetSite
        {
            get { return gstrTargetSite; }
            set { gstrTargetSite = value; }
        }

        public field strInnerException
        {
            get { return gstrInnerException; }
            set { gstrInnerException = value; }
        }

        public field strComplemento
        {
            get { return gstrComplemento; }
            set { gstrComplemento = value; }
        }

        public field strViewMessage
        {
            get { return gstrViewMessage; }
            set { gstrViewMessage = value; }
        }

        public field strSugerencia
        {
            get { return gstrSugerencia; }
            set { gstrSugerencia = value; }
        }

        public field strIp
        {
            get { return gstrIp; }
            set { gstrIp = value; }
        }

        public field strExplorer
        {
            get { return gstrExplorer; }
            set { gstrExplorer = value; }
        }

        public field strPage
        {
            get { return gstrPage; }
            set { gstrPage = value; }
        }

        public field strPlataform
        {
            get { return gstrPlataform; }
            set { gstrPlataform = value; }
        }

        public field strHostName
        {
            get { return gstrHostName; }
            set { gstrHostName = value; }
        }

        public field strSesion
        {
            get { return gstrSesion; }
            set { gstrSesion = value; }
        }

        public field dtmCFecha
        {
            get { return gdtmCFecha; }
            set { gdtmCFecha = value; }
        }

        public field dtmMFecha
        {
            get { return gdtmMFecha; }
            set { gdtmMFecha = value; }
        }

        public field intContactoUser
        {
            get { return gintContactoUser; }
            set { gintContactoUser = value; }
        }

        #endregion

        #region [Constructor]
        /// <summary>
        /// Constructor
        /// </summary>
        public tblLogError()
        {
            try
            {
                this.Conexion = clsSesiones.getConexion();
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
            //intidCodigo.Value = "0"; intidCodigo.Name = "intidCodigo"; intidCodigo.NameTable = Nombre + "." + intidCodigo.Name.ToString(); intidCodigo.Description = "Id Codigo"; intidCodigo.TypeDat = TipoCampo.Numeric;
            intAplicacion.Value = "1"; intAplicacion.Name = "intAplicacion"; intAplicacion.NameTable = Nombre + "." + intAplicacion.Name.ToString(); intAplicacion.Description = "Id Aplicacion"; intAplicacion.TypeDat = TipoCampo.Numeric;
            intId.Value = "0"; intId.Name = "intId"; intId.NameTable = Nombre + "." + intId.Name.ToString(); intId.Description = "Id"; intId.TypeDat = TipoCampo.Numeric;
            strCode.Value = string.Empty; strCode.Name = "strCode"; strCode.NameTable = Nombre + "." + strCode.Name.ToString(); strCode.Description = "Code";
            strTipoLog.Value = string.Empty; strTipoLog.Name = "strTipoLog"; strTipoLog.NameTable = Nombre + "." + strTipoLog.Name.ToString(); strTipoLog.Description = "Tipo Log";
            strTipo.Value = string.Empty; strTipo.Name = "strTipo"; strTipo.NameTable = Nombre + "." + strTipo.Name.ToString(); strTipo.Description = "Tipo";
            strSeverity.Value = string.Empty; strSeverity.Name = "strSeverity"; strSeverity.NameTable = Nombre + "." + strSeverity.Name.ToString(); strSeverity.Description = "Severity";
            strMetodo.Value = string.Empty; strMetodo.Name = "strMetodo"; strMetodo.NameTable = Nombre + "." + strMetodo.Name.ToString(); strMetodo.Description = "Metodo";
            strMessage.Value = string.Empty; strMessage.Name = "strMessage"; strMessage.NameTable = Nombre + "." + strMessage.Name.ToString(); strMessage.Description = "Message";
            strInfo.Value = string.Empty; strInfo.Name = "strInfo"; strInfo.NameTable = Nombre + "." + strInfo.Name.ToString(); strInfo.Description = "Info";
            strSource.Value = string.Empty; strSource.Name = "strSource"; strSource.NameTable = Nombre + "." + strSource.Name.ToString(); strSource.Description = "Source";
            strStackTrace.Value = string.Empty; strStackTrace.Name = "strStackTrace"; strStackTrace.NameTable = Nombre + "." + strStackTrace.Name.ToString(); strStackTrace.Description = "StackTrace";
            strTargetSite.Value = string.Empty; strTargetSite.Name = "strTargetSite"; strTargetSite.NameTable = Nombre + "." + strTargetSite.Name.ToString(); strTargetSite.Description = "TargetSite";
            strInnerException.Value = string.Empty; strInnerException.Name = "strInnerException"; strInnerException.NameTable = Nombre + "." + strInnerException.Name.ToString(); strInnerException.Description = "InnerException";
            strComplemento.Value = string.Empty; strComplemento.Name = "strComplemento"; strComplemento.NameTable = Nombre + "." + strComplemento.Name.ToString(); strComplemento.Description = "Complemento";
            strViewMessage.Value = string.Empty; strViewMessage.Name = "strViewMessage"; strViewMessage.NameTable = Nombre + "." + strViewMessage.Name.ToString(); strViewMessage.Description = "ViewMessage";
            strSugerencia.Value = string.Empty; strSugerencia.Name = "strSugerencia"; strSugerencia.NameTable = Nombre + "." + strSugerencia.Name.ToString(); strSugerencia.Description = "Sugerencia";
            strIp.Value = string.Empty; strIp.Name = "strIp"; strIp.NameTable = Nombre + "." + strIp.Name.ToString(); strIp.Description = "Ip";
            strExplorer.Value = string.Empty; strExplorer.Name = "strExplorer"; strExplorer.NameTable = Nombre + "." + strExplorer.Name.ToString(); strExplorer.Description = "Explorer";
            strPage.Value = string.Empty; strPage.Name = "strPage"; strPage.NameTable = Nombre + "." + strPage.Name.ToString(); strPage.Description = "Page";
            strPlataform.Value = string.Empty; strPlataform.Name = "strPlataform"; strPlataform.NameTable = Nombre + "." + strPlataform.Name.ToString(); strPlataform.Description = "Plataform";
            strHostName.Value = string.Empty; strHostName.Name = "strHostName"; strHostName.NameTable = Nombre + "." + strHostName.Name.ToString(); strHostName.Description = "HostName";
            strSesion.Value = string.Empty; strSesion.Name = "strSesion"; strSesion.NameTable = Nombre + "." + strSesion.Name.ToString(); strSesion.Description = "Sesion";
            dtmCFecha.Value = DateTime.Now.ToString(sFormatoFechaBD); dtmCFecha.Name = "dtmCFecha"; dtmCFecha.NameTable = Nombre + "." + dtmCFecha.Name.ToString(); dtmCFecha.Description = "Fecha Creacion"; dtmCFecha.TypeDat = TipoCampo.DateTime;
            dtmMFecha.Value = DateTime.Now.ToString(sFormatoFechaBD); dtmMFecha.Name = "dtmMFecha"; dtmMFecha.NameTable = Nombre + "." + dtmMFecha.Name.ToString(); dtmMFecha.Description = "Fecha Modificacion"; dtmMFecha.TypeDat = TipoCampo.DateTime;
            intContactoUser.Value = "0"; intContactoUser.Name = "intContactoUser"; intContactoUser.NameTable = Nombre + "." + intContactoUser.Name.ToString(); intContactoUser.Description = "Usuario Modifica"; intContactoUser.TypeDat = TipoCampo.Numeric;
        }
        #endregion

        #region [Métodos]
        /// <summary>
        /// Método para el llenado de la libreria a través de un DataSet
        /// </summary>
        /// <param name="dsFields">Dataset con el registro solicitado</param>
        public void Fill(DataSet dsFields)
        {
            try
            {
                if (dsFields.Tables[0].Rows.Count > 0)
                {
                    intidCodigo.Value = dsFields.Tables[0].Rows[0][intidCodigo.Name.ToString()].ToString();
                    intAplicacion.Value = dsFields.Tables[0].Rows[0][intAplicacion.Name.ToString()].ToString();
                    intId.Value = dsFields.Tables[0].Rows[0][intId.Name.ToString()].ToString();
                    strCode.Value = dsFields.Tables[0].Rows[0][strCode.Name.ToString()].ToString();
                    strTipoLog.Value = dsFields.Tables[0].Rows[0][strTipoLog.Name.ToString()].ToString();
                    strTipo.Value = dsFields.Tables[0].Rows[0][strTipo.Name.ToString()].ToString();
                    strSeverity.Value = dsFields.Tables[0].Rows[0][strSeverity.Name.ToString()].ToString();
                    strMetodo.Value = dsFields.Tables[0].Rows[0][strMetodo.Name.ToString()].ToString();
                    strMessage.Value = dsFields.Tables[0].Rows[0][strMessage.Name.ToString()].ToString();
                    strInfo.Value = dsFields.Tables[0].Rows[0][strInfo.Name.ToString()].ToString();
                    strSource.Value = dsFields.Tables[0].Rows[0][strSource.Name.ToString()].ToString();
                    strStackTrace.Value = dsFields.Tables[0].Rows[0][strStackTrace.Name.ToString()].ToString();
                    strTargetSite.Value = dsFields.Tables[0].Rows[0][strTargetSite.Name.ToString()].ToString();
                    strInnerException.Value = dsFields.Tables[0].Rows[0][strInnerException.Name.ToString()].ToString();
                    strComplemento.Value = dsFields.Tables[0].Rows[0][strComplemento.Name.ToString()].ToString();
                    strViewMessage.Value = dsFields.Tables[0].Rows[0][strViewMessage.Name.ToString()].ToString();
                    strSugerencia.Value = dsFields.Tables[0].Rows[0][strSugerencia.Name.ToString()].ToString();
                    strIp.Value = dsFields.Tables[0].Rows[0][strIp.Name.ToString()].ToString();
                    strExplorer.Value = dsFields.Tables[0].Rows[0][strExplorer.Name.ToString()].ToString();
                    strPage.Value = dsFields.Tables[0].Rows[0][strPage.Name.ToString()].ToString();
                    strSesion.Value = dsFields.Tables[0].Rows[0][strSesion.Name.ToString()].ToString();
                    strPlataform.Value = dsFields.Tables[0].Rows[0][strPlataform.Name.ToString()].ToString();
                    strHostName.Value = dsFields.Tables[0].Rows[0][strHostName.Name.ToString()].ToString();
                    dtmCFecha.Value = DateTime.Parse(dsFields.Tables[0].Rows[0][dtmCFecha.Name.ToString()].ToString()).ToString(sFormatoFechaBD);
                    dtmMFecha.Value = DateTime.Parse(dsFields.Tables[0].Rows[0][dtmMFecha.Name.ToString()].ToString()).ToString(sFormatoFechaBD);
                    intContactoUser.Value = dsFields.Tables[0].Rows[0][intContactoUser.Name.ToString()].ToString();
                    Respuesta = true;
                }
                else
                {
                    Respuesta = false;
                }
            }
            catch
            {
                Respuesta = false;
            }
        }

        public void Save(clsParametros cParametros)
        {
            int iNumeroCampos = 23;
            //int iKeys = 0;
            string[] pstrFields = new string[iNumeroCampos];
            string[] pstrValues = new string[iNumeroCampos];
            TipoCampo[] pstrTipo = new TipoCampo[iNumeroCampos];
            int iContador = 0;

            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;
            string pstrOrder = null;
            Inicialize(cParametros);
            pclsDataSql.Conexion = Conexion;
            try
            {
                pstrFrom = gstrNameTable;

                iContador = 0;
                pstrFields[iContador++] = intAplicacion.Name.ToString();
                pstrFields[iContador++] = intId.Name.ToString();
                pstrFields[iContador++] = strCode.Name.ToString();
                pstrFields[iContador++] = strTipoLog.Name.ToString();
                pstrFields[iContador++] = strTipo.Name.ToString();
                pstrFields[iContador++] = strSeverity.Name.ToString();
                pstrFields[iContador++] = strMetodo.Name.ToString();
                pstrFields[iContador++] = strMessage.Name.ToString();
                pstrFields[iContador++] = strInfo.Name.ToString();
                pstrFields[iContador++] = strSource.Name.ToString();
                pstrFields[iContador++] = strStackTrace.Name.ToString();
                pstrFields[iContador++] = strTargetSite.Name.ToString();
                pstrFields[iContador++] = strInnerException.Name.ToString();
                pstrFields[iContador++] = strComplemento.Name.ToString();
                pstrFields[iContador++] = strViewMessage.Name.ToString();
                pstrFields[iContador++] = strSugerencia.Name.ToString();
                pstrFields[iContador++] = strIp.Name.ToString();
                pstrFields[iContador++] = strExplorer.Name.ToString();
                pstrFields[iContador++] = strPage.Name.ToString();
                pstrFields[iContador++] = strPlataform.Name.ToString();
                pstrFields[iContador++] = strHostName.Name.ToString();
                pstrFields[iContador++] = strSesion.Name.ToString();
                //pstrFields[iContador++] = dtmMFecha.Name.ToString();
                pstrFields[iContador++] = intContactoUser.Name.ToString();

                iContador = 0;
                pstrTipo[iContador++] = intAplicacion.TypeDat;
                pstrTipo[iContador++] = intId.TypeDat;
                pstrTipo[iContador++] = strCode.TypeDat;
                pstrTipo[iContador++] = strTipoLog.TypeDat;
                pstrTipo[iContador++] = strTipo.TypeDat;
                pstrTipo[iContador++] = strSeverity.TypeDat;
                pstrTipo[iContador++] = strMetodo.TypeDat;
                pstrTipo[iContador++] = strMessage.TypeDat;
                pstrTipo[iContador++] = strInfo.TypeDat;
                pstrTipo[iContador++] = strSource.TypeDat;
                pstrTipo[iContador++] = strStackTrace.TypeDat;
                pstrTipo[iContador++] = strTargetSite.TypeDat;
                pstrTipo[iContador++] = strInnerException.TypeDat;
                pstrTipo[iContador++] = strComplemento.TypeDat;
                pstrTipo[iContador++] = strViewMessage.TypeDat;
                pstrTipo[iContador++] = strSugerencia.TypeDat;
                pstrTipo[iContador++] = strIp.TypeDat;
                pstrTipo[iContador++] = strExplorer.TypeDat;
                pstrTipo[iContador++] = strPage.TypeDat;
                pstrTipo[iContador++] = strPlataform.TypeDat;
                pstrTipo[iContador++] = strHostName.TypeDat;
                pstrTipo[iContador++] = strSesion.TypeDat;
                //pstrTipo[iContador++] = dtmMFecha.TypeDat;
                pstrTipo[iContador++] = intContactoUser.TypeDat;

                iContador = 0;
                pstrValues[iContador++] = intAplicacion.Value;
                pstrValues[iContador++] = intId.Value;
                pstrValues[iContador++] = strCode.Value;
                pstrValues[iContador++] = strTipoLog.Value;
                pstrValues[iContador++] = strTipo.Value;
                pstrValues[iContador++] = strSeverity.Value;
                pstrValues[iContador++] = strMetodo.Value;
                pstrValues[iContador++] = strMessage.Value;
                pstrValues[iContador++] = strInfo.Value;
                pstrValues[iContador++] = strSource.Value;
                pstrValues[iContador++] = strStackTrace.Value;
                pstrValues[iContador++] = strTargetSite.Value;
                pstrValues[iContador++] = strInnerException.Value;
                pstrValues[iContador++] = strComplemento.Value;
                pstrValues[iContador++] = strViewMessage.Value;
                pstrValues[iContador++] = strSugerencia.Value;
                pstrValues[iContador++] = strIp.Value;
                pstrValues[iContador++] = strExplorer.Value;
                pstrValues[iContador++] = strPage.Value;
                pstrValues[iContador++] = strPlataform.Value;
                pstrValues[iContador++] = strHostName.Value;
                pstrValues[iContador++] = strSesion.Value;
                //pstrValues[iContador++] = dtmMFecha.Value;
                pstrValues[iContador++] = intContactoUser.Value;

                pstrSql = pstrSql + pclsSql.SqlSentencia(TipoComando.Insert, pstrFields, pstrValues, pstrTipo, pstrFrom, pstrWhere, pstrOrder, iNumeroCampos) + "   ";
                pclsDataSql.UpdateInsert(pstrSql);
                cParametros.Complemento = "SQL: " + pstrSql;
                ExceptionHandled.EventoTexto(cParametros);
            }
            catch (Exception Ex)
            {
                ExceptionHandled.EventoTexto(cParametros);
                cParametros.Message = Ex.Message.ToString();
                cParametros.Info = "Log en BD";
                cParametros.Tipo = clsTipoError.DataBase;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Complemento = "Los datos no se guardaron, SQL: " + pstrSql;
                ExceptionHandled.EventoTexto(cParametros);
            }
        }
        /// <summary>
        /// Método que recupera un registro de la tabla en la base de datos, actualiza el objeto (através del método Fill) y retorna un dataset.
        /// </summary>
        /// <param name="Codigo">intProyecto:  Código del proyecto que se envía como condición</param>
        /// <returns>DataSet con los datos del registro solicitado</returns>
        public DataSet Get()
        {
            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;
            string pstrOrder = null;
            DataSet dsData = new DataSet();

            pclsDataSql.Conexion = Conexion;

            pstrFrom = gstrNameTable;
            pstrSql = pclsSql.SqlSentencia(null, pstrFrom, pstrWhere, pstrOrder);

            dsData = pclsDataSql.Select(pstrSql);
            Fill(dsData);
            return dsData;
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla
        /// </summary>
   

        /// <summary>
        /// Métodp para inicializar el objeto
        /// </summary>
        public void Inicialize(clsParametros cParametros)
        {
            try
            {
                intAplicacion.Value = clsSesiones.getAplicacion().ToString();
            }
            catch { }
            try { intId.Value = cParametros.Id.ToString(); }
            catch { intId.Value = "0"; }
            try { strCode.Value = cParametros.Code.ToString(); }
            catch { strCode.Value = string.Empty; }
            try { strTipoLog.Value = cParametros.TipoLog.ToString(); }
            catch { strTipoLog.Value = string.Empty; }
            try { strTipo.Value = cParametros.TipoLog.ToString(); }
            catch { strTipo.Value = string.Empty; }
            try { strSeverity.Value = cParametros.Severity.ToString(); }
            catch { strSeverity.Value = string.Empty; }
            try { strMetodo.Value = cParametros.Metodo.ToString(); }
            catch { strMetodo.Value = string.Empty; }
            try { strMessage.Value = cParametros.Message.ToString(); }
            catch { strMessage.Value = string.Empty; }
            try { strInfo.Value = cParametros.Info.ToString(); }
            catch { strInfo.Value = string.Empty; }
            try { strSource.Value = cParametros.Source.ToString(); }
            catch { strSource.Value = string.Empty; }
            try { strStackTrace.Value = cParametros.StackTrace.ToString(); }
            catch { strStackTrace.Value = string.Empty; }
            try { strTargetSite.Value = cParametros.TargetSite.ToString(); }
            catch { strTargetSite.Value = string.Empty; }
            try { strInnerException.Value = cParametros.InnerException.ToString(); }
            catch { strInnerException.Value = string.Empty; }
            try { strComplemento.Value = cParametros.Complemento.ToString(); }
            catch { strComplemento.Value = string.Empty; }
            try
            {
                if (cParametros.ViewMessage.Count > 0)
                {
                    for (int i = 0; i < cParametros.ViewMessage.Count; i++)
                    {
                        if (i > 0)
                            strViewMessage.Value += " - ";
                        strViewMessage.Value += cParametros.ViewMessage[i].ToString();
                    }
                }
            }
            catch { strViewMessage.Value = string.Empty; }
            try
            {
                if (cParametros.Sugerencia.Count > 0)
                {
                    for (int i = 0; i < cParametros.Sugerencia.Count; i++)
                    {
                        if (i > 0)
                            strSugerencia.Value += " - ";
                        strSugerencia.Value += cParametros.Sugerencia[i].ToString();
                    }
                }
            }
            catch { strSugerencia.Value = string.Empty; }
            try
            {
                strIp.Value = clsValidaciones.ObtenerIp();
            }
            catch { strIp.Value = string.Empty; }
            try
            {
                strExplorer.Value = clsValidaciones.ObtenerBrowserUser();
            }
            catch { strExplorer.Value = string.Empty; }
            try
            {
                strPage.Value = clsValidaciones.ObtenerUrl();
            }
            catch { strPage.Value = string.Empty; }
            try
            {
                strHostName.Value = clsValidaciones.ObtenerHostName();
            }
            catch { strHostName.Value = string.Empty; }
            try
            {
                strPlataform.Value = clsValidaciones.ObtenerBrowserPlataform();
            }
            catch { strPlataform.Value = string.Empty; }
            try
            {
                strSesion.Value = new clsCacheControl().RecuperarSesionId();
            }
            catch { strSesion.Value = string.Empty; }
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    if (cCache.Viajero != null)
                    {
                        if (!cCache.Viajero.Length.Equals(0))
                        {
                            intContactoUser.Value = cCache.Viajero;
                        }
                        else
                        {
                            if (cCache.Empresa != null)
                            {
                                if (!cCache.Empresa.Length.Equals(0))
                                {
                                    intContactoUser.Value = cCache.Empresa;
                                }                              
                            }
                        }
                    }
                }
            }
            catch { intContactoUser.Value = string.Empty; }
        }
        #endregion
    }
}
