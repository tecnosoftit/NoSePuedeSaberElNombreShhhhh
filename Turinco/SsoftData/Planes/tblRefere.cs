//Version 4.0.0.1
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Ssoft.Sql;
using System.Data;
using System.Configuration;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;
using Ssoft.DataNet;

namespace SsoftQuery.Planes.Refere
{
    /// <summary>
    /// Clase de la tabla de Refere
    /// Manejo de referencias
    /// </summary>
    /// Autor:          José Faustino Posas
    /// Company:        Ssoft Colombia
    /// Fecha:          01/20/2009
    /// Control de Cambios
    /// Autor:          
    /// Fecha:          
    /// Descripción:    
    public class tblRefere
    {
        #region [Atributos y Propiedades]

        /// <summary>
        /// Definición de instancias del método
        /// </summary>
        //private static string sFormatoFecha = clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");
        private static DataSql pclsDataSql = new DataSql();
        private static Sql pclsSql = new Sql();
        string pstrSql = string.Empty;

        /// <summary>
        /// Definición de atributos
        /// </summary>
        protected string gstrNameTable = "tblRefere";
        protected string gstrConexionTable = string.Empty;

        protected field gintidRefere = new field();
        protected field gintidTipoRefere = new field();
        protected field gintNivel = new field();
        protected field gstrRefere = new field();
        protected field gstrDetalle = new field();
        protected field gstrValor = new field();
        protected field gstrValorAdic = new field();
        protected field gintProxNivel = new field();
        protected field gbitExterna = new field();
        protected field gstrTable = new field();
        protected field gbitActivo = new field();
        protected field gstrImagen = new field();
        protected field gstrLink = new field();
        protected field gintOrden = new field();
        protected field gintAplicacion = new field();
        protected field gstrIdioma = new field();
        protected bool gbolRespuesta = false;

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

        public field intidTipoRefere
        {
            get { return gintidTipoRefere; }
            set { gintidTipoRefere = value; }
        }

        public field intNivel
        {
            get { return gintNivel; }
            set { gintNivel = value; }
        }

        public field strRefere
        {
            get { return gstrRefere; }
            set { gstrRefere = value; }
        }

        public field strDetalle
        {
            get { return gstrDetalle; }
            set { gstrDetalle = value; }
        }

        public field strValor
        {
            get { return gstrValor; }
            set { gstrValor = value; }
        }

        public field strValorAdic
        {
            get { return gstrValorAdic; }
            set { gstrValorAdic = value; }
        }

        public field intProxNivel
        {
            get { return gintProxNivel; }
            set { gintProxNivel = value; }
        }

        public field bitExterna
        {
            get { return gbitExterna; }
            set { gbitExterna = value; }
        }

        public field strTable
        {
            get { return gstrTable; }
            set { gstrTable = value; }
        }

        public field intidRefere
        {
            get { return gintidRefere; }
            set { gintidRefere = value; }
        }

        public field bitActivo
        {
            get { return gbitActivo; }
            set { gbitActivo = value; }
        }

        public field strImagen
        {
            get { return gstrImagen; }
            set { gstrImagen = value; }
        }

        public field strLink
        {
            get { return gstrLink; }
            set { gstrLink = value; }
        }

        public field intOrden
        {
            get { return gintOrden; }
            set { gintOrden = value; }
        }

        public field intAplicacion
        {
            get { return gintAplicacion; }
            set { gintAplicacion = value; }
        }

        public field strIdioma
        {
            get { return gstrIdioma; }
            set { gstrIdioma = value; }
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Constructor
        /// </summary>
        public tblRefere()
        {
            try
            {
                this.Conexion = clsSesiones.getConexion();
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
            intidRefere.Value = "0"; intidRefere.Name = "intidRefere"; intidRefere.NameTable = Nombre + "." + intidRefere.Name.ToString(); intidRefere.Description = "Id de Referencia"; intidRefere.TypeDat = TipoCampo.Numeric;
            intidTipoRefere.Value = "0"; intidTipoRefere.Name = "intidTipoRefere"; intidTipoRefere.NameTable = Nombre + "." + intidTipoRefere.Name.ToString(); intidTipoRefere.Description = "TId ipo de Referencia"; intidTipoRefere.TypeDat = TipoCampo.Numeric;
            intNivel.Value = "0"; intNivel.Name = "intNivel"; intNivel.NameTable = Nombre + "." + intNivel.Name.ToString(); intNivel.Description = "Nivel"; intNivel.TypeDat = TipoCampo.Numeric;
            strRefere.Value = string.Empty; strRefere.Name = "strRefere"; strRefere.NameTable = Nombre + "." + strRefere.Name.ToString(); strRefere.Description = "Referencia";
            strDetalle.Value = string.Empty; strDetalle.Name = "strDetalle"; strDetalle.NameTable = Nombre + "." + strDetalle.Name.ToString(); strDetalle.Description = "Detalle";
            strValor.Value = string.Empty; strValor.Name = "strValor"; strValor.NameTable = Nombre + "." + strValor.Name.ToString(); strValor.Description = "Valor";
            strValorAdic.Value = string.Empty; strValorAdic.Name = "strValorAdic"; strValorAdic.NameTable = Nombre + "." + strValorAdic.Name.ToString(); strValorAdic.Description = "ValorAdicional";
            intProxNivel.Value = "0"; intProxNivel.Name = "intProxNivel"; intProxNivel.NameTable = Nombre + "." + intProxNivel.Name.ToString(); intProxNivel.Description = "Próximo Nivel"; intProxNivel.TypeDat = TipoCampo.Numeric;
            bitExterna.Value = "0"; bitExterna.Name = "bitExterna"; bitExterna.NameTable = Nombre + "." + bitExterna.Name.ToString(); bitExterna.Description = "Reporte externo"; bitExterna.TypeDat = TipoCampo.Bit;
            strTable.Value = string.Empty; strTable.Name = "strTable"; strTable.NameTable = Nombre + "." + strTable.Name.ToString(); strTable.Description = "tabla";
            bitActivo.Value = "1"; bitActivo.Name = "bitActivo"; bitActivo.NameTable = Nombre + "." + bitActivo.Name.ToString(); bitActivo.Description = "Estado"; bitActivo.TypeDat = TipoCampo.Bit;
            strImagen.Value = string.Empty; strImagen.Name = "strImagen"; strImagen.NameTable = Nombre + "." + strImagen.Name.ToString(); strImagen.Description = "Imagen";
            strLink.Value = string.Empty; strLink.Name = "strLink"; strLink.NameTable = Nombre + "." + strLink.Name.ToString(); strLink.Description = "Link";
            intOrden.Value = "0"; intOrden.Name = "intOrden"; intOrden.NameTable = Nombre + "." + intOrden.Name.ToString(); intOrden.Description = "Orden"; intOrden.TypeDat = TipoCampo.Numeric;
            intAplicacion.Value = clsSesiones.getAplicacion().ToString(); intAplicacion.Name = "intAplicacion"; intAplicacion.NameTable = Nombre + "." + intAplicacion.Name.ToString(); intAplicacion.Description = "Aplicacion dueña del plan"; intAplicacion.TypeDat = TipoCampo.Numeric;
            strIdioma.Value = clsSesiones.getIdioma(); strIdioma.Name = "strIdioma"; strIdioma.NameTable = Nombre + "." + strIdioma.Name.ToString(); strIdioma.Description = "Idioma";
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
                    intidRefere.Value = dsFields.Tables[0].Rows[0][intidRefere.Name.ToString()].ToString();
                    intidTipoRefere.Value = dsFields.Tables[0].Rows[0][intidTipoRefere.Name.ToString()].ToString();
                    intNivel.Value = dsFields.Tables[0].Rows[0][intNivel.Name.ToString()].ToString();
                    strRefere.Value = dsFields.Tables[0].Rows[0][strRefere.Name.ToString()].ToString();
                    strDetalle.Value = dsFields.Tables[0].Rows[0][strDetalle.Name.ToString()].ToString();
                    strValor.Value = dsFields.Tables[0].Rows[0][strValor.Name.ToString()].ToString();
                    strValorAdic.Value = dsFields.Tables[0].Rows[0][strValorAdic.Name.ToString()].ToString();
                    intProxNivel.Value = dsFields.Tables[0].Rows[0][intProxNivel.Name.ToString()].ToString();
                    bitExterna.Value = dsFields.Tables[0].Rows[0][bitExterna.Name.ToString()].ToString();
                    strTable.Value = dsFields.Tables[0].Rows[0][strTable.Name.ToString()].ToString();
                    bitActivo.Value = dsFields.Tables[0].Rows[0][bitActivo.Name.ToString()].ToString();
                    strImagen.Value = dsFields.Tables[0].Rows[0][strImagen.Name.ToString()].ToString();
                    strLink.Value = dsFields.Tables[0].Rows[0][strLink.Name.ToString()].ToString();
                    intOrden.Value = dsFields.Tables[0].Rows[0][intOrden.Name.ToString()].ToString();
                    intAplicacion.Value = dsFields.Tables[0].Rows[0][intAplicacion.Name.ToString()].ToString();
                    strIdioma.Value = dsFields.Tables[0].Rows[0][strIdioma.Name.ToString()].ToString();
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

        /// <summary>
        /// Método que guarda el registro en la tabla
        /// </summary>
        public void Save()
        {
            int iNumeroCampos = 16;
            int iKeys = 3;
            string[] pstrFields = new string[iNumeroCampos];
            string[] pstrValues = new string[iNumeroCampos];
            TipoCampo[] pstrTipo = new TipoCampo[iNumeroCampos];
            int iContador = 0;
            string sIdioma = clsValidaciones.GetKeyOrAdd("TipoRefereIdioma", "Idiomas");

            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;
            string pstrOrder = null;

            int pintMaxRefere = 0;

            pclsDataSql.Conexion = Conexion;

            if (intidRefere.Value.ToString() == "0")
            {
                pintMaxRefere = pclsDataSql.MaxConsec(Nombre.ToString(), intidRefere.Name.ToString(), intidRefere.TypeDat);
                pintMaxRefere++;
                intidRefere.Value = pintMaxRefere.ToString();
            }

            pstrFrom = gstrNameTable;

            iContador = 0;
            pstrFields[iContador++] = intidTipoRefere.Name.ToString();
            pstrFields[iContador++] = intNivel.Name.ToString();
            pstrFields[iContador++] = strRefere.Name.ToString();
            pstrFields[iContador++] = strDetalle.Name.ToString();
            pstrFields[iContador++] = strValor.Name.ToString();
            pstrFields[iContador++] = strValorAdic.Name.ToString();
            pstrFields[iContador++] = intProxNivel.Name.ToString();
            pstrFields[iContador++] = bitExterna.Name.ToString();
            pstrFields[iContador++] = strTable.Name.ToString();
            pstrFields[iContador++] = strImagen.Name.ToString();
            pstrFields[iContador++] = bitActivo.Name.ToString();
            pstrFields[iContador++] = strLink.Name.ToString();
            pstrFields[iContador++] = intOrden.Name.ToString();
            pstrFields[iContador++] = intidRefere.Name.ToString();
            pstrFields[iContador++] = intAplicacion.Name.ToString();
            pstrFields[iContador++] = strIdioma.Name.ToString();

            iContador = 0;
            pstrTipo[iContador++] = intidTipoRefere.TypeDat;
            pstrTipo[iContador++] = intNivel.TypeDat;
            pstrTipo[iContador++] = strRefere.TypeDat;
            pstrTipo[iContador++] = strDetalle.TypeDat;
            pstrTipo[iContador++] = strValor.TypeDat;
            pstrTipo[iContador++] = strValorAdic.TypeDat;
            pstrTipo[iContador++] = intProxNivel.TypeDat;
            pstrTipo[iContador++] = bitExterna.TypeDat;
            pstrTipo[iContador++] = strTable.TypeDat;
            pstrTipo[iContador++] = strImagen.TypeDat;
            pstrTipo[iContador++] = bitActivo.TypeDat;
            pstrTipo[iContador++] = strLink.TypeDat;
            pstrTipo[iContador++] = intOrden.TypeDat;
            pstrTipo[iContador++] = intidRefere.TypeDat;
            pstrTipo[iContador++] = intAplicacion.TypeDat;
            pstrTipo[iContador++] = strIdioma.TypeDat;

            iContador = 0;
            pstrValues[iContador++] = intidTipoRefere.Value;
            pstrValues[iContador++] = intNivel.Value;
            pstrValues[iContador++] = strRefere.Value;
            pstrValues[iContador++] = strDetalle.Value;
            pstrValues[iContador++] = strValor.Value;
            pstrValues[iContador++] = strValorAdic.Value;
            pstrValues[iContador++] = intProxNivel.Value;
            pstrValues[iContador++] = bitExterna.Value;
            pstrValues[iContador++] = strTable.Value;
            pstrValues[iContador++] = strImagen.Value;
            pstrValues[iContador++] = bitActivo.Value;
            pstrValues[iContador++] = strLink.Value;
            pstrValues[iContador++] = intOrden.Value;
            pstrValues[iContador++] = intidRefere.Value;
            pstrValues[iContador++] = intAplicacion.Value;
            pstrValues[iContador++] = strIdioma.Value;

            if (pintMaxRefere == 0)
            {
                if (intAplicacion.Value.ToString().Equals("0"))
                {
                    pstrWhere = intidRefere.Name.ToString() + " = " + intidRefere.Value.ToString();
                }
                else
                {
                    if (strIdioma.Value.ToString().Equals(""))
                    {
                        pstrWhere = intidRefere.Name.ToString() + " = " + intidRefere.Value.ToString() + " AND " + intAplicacion.Name.ToString() + " = " + intAplicacion.Value.ToString();
                    }
                    else
                    {
                        pstrWhere = intidRefere.Name.ToString() + " = " + intidRefere.Value.ToString() + " AND " + intAplicacion.Name.ToString() + " = " + intAplicacion.Value.ToString() + " AND " + strIdioma.Name.ToString() + " = '" + strIdioma.Value.ToString() + "' ";
                    }

                }
                pstrSql = pstrSql + pclsSql.SqlSentencia(TipoComando.Update, pstrFields, pstrValues, pstrTipo, pstrFrom, pstrWhere, pstrOrder, (iNumeroCampos - iKeys)) + "   ";
            }
            else
            {
                tblRefere Idiomas = new tblRefere();
                Idiomas.Conexion = this.Conexion;
                /*Consultamos los idiomas de la aplicacion*/
                DataSet dsIdiomas = Idiomas.GetTipoRefere(sIdioma, intAplicacion.Value, "es");
                if (dsIdiomas != null && dsIdiomas.Tables.Count > 0)
                {
                    if (dsIdiomas.Tables.Count > 0)
                    {
                        int i = 0;
                        while (i < dsIdiomas.Tables[0].Rows.Count)
                        {
                            /*valor idioma*/
                            pstrValues[15] = dsIdiomas.Tables[0].Rows[i][Idiomas.strRefere.Name].ToString();
                            /*Consultamos las aplicaciones*/
                            DataSet dsAplicaciones = Idiomas.GetTipoRefere(ConfigurationManager.AppSettings["Aplicacion"], intAplicacion.Value, "es");
                            if (dsAplicaciones != null && dsAplicaciones.Tables.Count > 0)
                            {
                                if (dsAplicaciones.Tables.Count > 0)
                                {
                                    int l = 0;
                                    while (l < dsAplicaciones.Tables[0].Rows.Count)
                                    {
                                        /*valor aplicacion*/
                                        pstrValues[14] = dsAplicaciones.Tables[0].Rows[l]["intidRefere"].ToString();
                                        pstrSql = pstrSql + pclsSql.SqlSentencia(TipoComando.Insert, pstrFields, pstrValues, pstrTipo, pstrFrom, pstrWhere, pstrOrder, iNumeroCampos) + "   ";
                                        l++;
                                    }
                                }
                                else
                                {
                                    throw new Exception("Error al consultar las aplicaciones e insertar las referencias");
                                }
                            }
                            i++;
                        }
                    }
                    else
                    {
                        throw new Exception("Error al consultar los idiomas e insertar las referencias");
                    }
                }
                else
                {
                    pstrValues[iContador++] = strIdioma.Value;
                    pstrSql = pstrSql + pclsSql.SqlSentencia(TipoComando.Insert, pstrFields, pstrValues, pstrTipo, pstrFrom, pstrWhere, pstrOrder, iNumeroCampos) + "   ";
                }

            }
            pclsDataSql.UpdateInsert(pstrSql);
        }
        public clsParametros SaveError()
        {

            clsParametros cMensaje = new clsParametros();
            cMensaje.DatoAdic = "0";

            try
            {
                Save();
                cMensaje.Id = 1;
                cMensaje.ViewMessage.Add("Los datos se guardaron exitosamente");
                cMensaje.Sugerencia.Add("");
                cMensaje.DatoAdic = intidRefere.Value;
                return cMensaje;
            }
            catch (Exception Ex)
            {
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.DataBase;
                cMensaje.Severity = clsSeveridad.Alta;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Complemento = "Los datos no se guardaron, SQL: " + pstrSql;
                cMensaje.ViewMessage.Add("Error al guardar en la base de datos");
                cMensaje.Sugerencia.Add("Por favor revise el tipo de informacion");
                cMensaje.Sugerencia.Add("Comuniquese con el administrador");
                ExceptionHandled.Publicar(cMensaje);
                return cMensaje;
            }
        }

        /// <summary>
        /// Método que recupera un registro de la tabla en la base de datos, actualiza el objeto (através del método Fill) y retorna un dataset.
        /// </summary>
        /// <param name="Codigo">intProyecto:  Código del proyecto que se envía como condición</param>
        /// <returns>DataSet con los datos del registro solicitado</returns>
        public DataSet Get(string Codigo)
        {
            string Idioma = clsSesiones.getIdioma();
            string Aplicacion = clsSesiones.getAplicacion().ToString();
            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;
            string pstrOrder = null;
            DataSet dsData = new DataSet();

            pclsDataSql.Conexion = Conexion;

            pstrFrom = gstrNameTable;
            pstrWhere = intidRefere.Name.ToString() + " LIKE " + Codigo + "  AND " + intAplicacion.Name + "=" + Aplicacion + "  AND " + strIdioma.Name + "='" + Idioma + "' ";
            pstrSql = pclsSql.SqlSentencia(null, pstrFrom, pstrWhere, pstrOrder);

            dsData = pclsDataSql.Select(pstrSql);
            Fill(dsData);
            return dsData;
        }
        public DataSet Get(string Codigo, bool bValidaAplicacion)
        {
            string Idioma = clsSesiones.getIdioma();
            string Aplicacion = clsSesiones.getAplicacion().ToString();
            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;
            string pstrOrder = null;
            DataSet dsData = new DataSet();

            pclsDataSql.Conexion = Conexion;

            pstrFrom = gstrNameTable;
            if (bValidaAplicacion)
                pstrWhere = intidRefere.Name.ToString() + " LIKE " + Codigo + "  AND " + intAplicacion.Name + "=" + Aplicacion + "  AND " + strIdioma.Name + "='" + Idioma + "' ";
            else
                pstrWhere = intidRefere.Name.ToString() + " LIKE " + Codigo + "  AND " + strIdioma.Name + "='" + Idioma + "' ";
            pstrSql = pclsSql.SqlSentencia(null, pstrFrom, pstrWhere, pstrOrder);

            dsData = pclsDataSql.Select(pstrSql);
            Fill(dsData);
            return dsData;
        }

        /// <summary>
        /// Método que recupera un registro de la tabla en la base de datos, actualiza el objeto (através del método Fill) y retorna un dataset.
        /// </summary>
        /// <param name="TipoRefere">String Tipo de Referencia</param>
        /// <param name="Refere">String Referencia</param>
        /// <returns>DataSet con los datos del registro solicitado</returns>
        public bool ExistRefere(string TipoRefere, string Refere)
        {
            string Idioma = clsSesiones.getIdioma();
            string Aplicacion = clsSesiones.getAplicacion().ToString();
            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;
            string pstrOrder = null;
            string pstrSelect = null;
            DataSet dsData = new DataSet();
            tblTipoRefere ptblTipoRefere = new tblTipoRefere();
            pclsDataSql.Conexion = Conexion;
            bool bExist = false;
            pstrSelect = gstrNameTable + ".* ";
            pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
            pstrWhere = strRefere.NameTable.ToString() + " = '" + Refere + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + intAplicacion.NameTable + "=" + Aplicacion + "  AND " + strIdioma.NameTable + "='" + Idioma + "' ";
            pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
            try
            {
                dsData = pclsDataSql.Select(pstrSql);

                if (dsData.Tables[0].Rows.Count == 0)
                {
                    bExist = false;
                }
                else
                {
                    bExist = true;
                }
            }
            catch { Respuesta = false; }
            return bExist;

        }

        /// <summary>
        /// dsGetData, retorna los registros de la tabla de referencia relacionada con tiporefere por idioma y aplicacion
        /// </summary>
        /// <param name="sIdioma">Idioma</param>
        /// <param name="sAplicacion">Aplicacion</param>
        /// <returns></returns>                
        public DataTable dtGetData(string sIdioma, string sAplicacion, string sTipoRefere)
        {
            DataTable dtData = new DataTable();
            try
            {
                DataSet dsDataNew = dsGetData(sIdioma, sAplicacion, sTipoRefere, true);
                //DataSet dsDataNew = spGetTipoRefere(sAplicacion, sIdioma, sTipoRefere, false);
                if (dsDataNew.Tables[0].Rows.Count > 0)
                {
                    dtData = dsDataNew.Tables[0];
                }
            }
            catch { }
            return dtData;
        }
        public DataTable dtGetData(string sIdioma, string sAplicacion, string sTipoRefere, bool bActivo)
        {
            DataTable dtData = new DataTable();
            try
            {
                DataSet dsDataNew = dsGetData(sIdioma, sAplicacion, sTipoRefere, bActivo);
                //DataSet dsDataNew = spGetTipoRefere(sAplicacion, sIdioma, sTipoRefere, bActivo);
                if (dsDataNew.Tables[0].Rows.Count.Equals(0))
                {
                    dtData = dsDataNew.Tables[0];
                }
            }
            catch { }
            return dtData;
        }
        [Obsolete]
        private DataSet dsGetData(string sIdioma, string sAplicacion, string sTipoRefere, bool bActivo)
        {
            DataSet dsData = new DataSet();
            try
            {
                string pstrSql = string.Empty;
                string pstrFrom = null;
                string pstrWhere = null;
                string pstrOrder = null;
                string pstrSelect = null;

                tblTipoRefere ptblTipoRefere = new tblTipoRefere();
                pclsDataSql.Conexion = Conexion;

                pstrSelect = gstrNameTable + ".*, " + ptblTipoRefere.strTipoRefere.Name.ToString() + ", " + ptblTipoRefere.strDescripcion.Name.ToString();
                pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
                if (bActivo)
                    pstrWhere = intAplicacion.NameTable + "=" + sAplicacion + "  AND " + strIdioma.NameTable + "='" + sIdioma + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + sTipoRefere + "'  AND " + bitActivo.Name.ToString() + " = 1 ";
                else
                    pstrWhere = intAplicacion.NameTable + "=" + sAplicacion + "  AND " + strIdioma.NameTable + "='" + sIdioma + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + sTipoRefere + "' ";
                pstrOrder = intOrden.NameTable;
                pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
                dsData = pclsDataSql.Select(pstrSql);
                if (dsData.Tables[0].Rows.Count.Equals(0))
                {
                    Fill(dsData);
                }
                else
                {
                    Inicialize();
                    Respuesta = false;
                }
            }
            catch { }
            return dsData;
        }
        private DataSet dsGetData(string sIdioma, string sAplicacion, int iTipoRefere, bool bActivo)
        {
            DataSet dsData = new DataSet();
            try
            {
                dsData = dsGetData(sIdioma, sAplicacion, iTipoRefere.ToString(), bActivo);
            }
            catch { }
            return dsData;
        }
        [Obsolete]
        private DataSet dsGetData(string sIdioma, string sAplicacion)
        {
            DataSet dsData = new DataSet();
            try
            {
                string pstrSql = string.Empty;
                string pstrFrom = null;
                string pstrWhere = null;
                string pstrOrder = null;
                string pstrSelect = null;

                tblTipoRefere ptblTipoRefere = new tblTipoRefere();
                pclsDataSql.Conexion = Conexion;

                pstrSelect = gstrNameTable + ".*, " + ptblTipoRefere.strTipoRefere.Name.ToString() + ", " + ptblTipoRefere.strDescripcion.Name.ToString();
                pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
                pstrWhere = intAplicacion.NameTable + "=" + sAplicacion + "  AND " + strIdioma.NameTable + "='" + sIdioma + "' ";
                pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
                dsData = pclsDataSql.Select(pstrSql);
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
                cParametros.Complemento = "Consulta refere =  " + pstrSql;
                ExceptionHandled.Publicar(cParametros);
            }
            return dsData;
        }
        /// <summary>
        /// Método que recupera un registro de la tabla en la base de datos, actualiza el objeto (através del método Fill) y retorna un dataset.
        /// </summary>
        /// <param name="TipoRefere">String Tipo de Referencia</param>
        /// <param name="Refere">String Referencia</param>
        /// <returns>DataSet con los datos del registro solicitado</returns>
        public DataSet Get(string TipoRefere, string Refere)
        {
            DataSet dsData = new DataSet();
            try
            {
                string Idioma = clsSesiones.getIdioma();
                string Aplicacion = clsSesiones.getAplicacion().ToString();
                string pstrSql = string.Empty;
                string pstrFrom = null;
                string pstrWhere = null;
                string pstrOrder = null;
                string pstrSelect = null;

                tblTipoRefere ptblTipoRefere = new tblTipoRefere();
                pclsDataSql.Conexion = Conexion;

                pstrSelect = gstrNameTable + ".*, " + ptblTipoRefere.strTipoRefere.Name.ToString() + ", " + ptblTipoRefere.strDescripcion.Name.ToString();
                pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
                pstrWhere = intAplicacion.NameTable + "=" + Aplicacion + "  AND " + strIdioma.NameTable + "='" + Idioma + "' AND " + strRefere.NameTable.ToString() + " = '" + Refere + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "'";
                pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
                dsData = pclsDataSql.Select(pstrSql);
                if (dsData != null)
                {
                    Fill(dsData);
                }
                else
                {
                    Inicialize();
                    Respuesta = false;
                }
            }
            catch
            {
                Inicialize();
                Respuesta = false;
            }
            return dsData;
        }
        /// <summary>
        /// Método que recupera un registro de la tabla en la base de datos, actualiza el objeto (através del método Fill) y retorna un dataset.
        /// recibe la aplicacion y la validad dependiendo del valor
        /// </summary>
        /// <param name="TipoRefere">String Tipo de Referencia</param>
        /// <param name="Refere">String Referencia</param>
        /// <param name="Aplicacion">int Aplicacion</param>
        /// <returns>DataSet con los datos del registro solicitado</returns>
        public DataSet Get(string TipoRefere, string Refere, int Aplicacion, bool ValidaAplicacion)
        {
            string Idioma = clsSesiones.getIdioma();
            string sFiltroAplicacion = "";
            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;
            string pstrOrder = null;
            string pstrSelect = null;
            DataSet dsData = new DataSet();

            tblTipoRefere ptblTipoRefere = new tblTipoRefere();
            pclsDataSql.Conexion = Conexion;

            if (ValidaAplicacion)
                sFiltroAplicacion = "AND " + intAplicacion.NameTable + "=" + Aplicacion;

            pstrSelect = gstrNameTable + ".* ";
            pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
            pstrWhere = strRefere.NameTable.ToString() + " = '" + Refere + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' " + sFiltroAplicacion + "  AND " + strIdioma.NameTable + "='" + Idioma + "' ";
            pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
            try
            {
                dsData = pclsDataSql.Select(pstrSql);
                if (dsData.Tables[0].Rows.Count == 0)
                {
                    Inicialize();
                }
                else
                {
                    Fill(dsData);
                }
            }
            catch { Respuesta = false; }
            return dsData;

        }
        /// <summary>
        /// Método que recupera un registro de la tabla en la base de datos, actualiza el objeto (através del método Fill) y retorna un dataset.
        /// </summary>
        /// <param name="TipoRefere">String Tipo de Referencia</param>
        /// <param name="Refere">String Referencia</param>
        /// <param name="Detalle">String Detalle</param>
        /// <returns>DataSet con los datos del registro solicitado</returns>
        public DataSet Get(string TipoRefere, string Refere, string Detalle)
        {
            string Idioma = clsSesiones.getIdioma();
            string Aplicacion = clsSesiones.getAplicacion().ToString();
            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;
            string pstrOrder = null;
            string pstrSelect = null;
            DataSet dsData = new DataSet();
            tblTipoRefere ptblTipoRefere = new tblTipoRefere();
            pclsDataSql.Conexion = Conexion;

            pstrSelect = gstrNameTable + ".* ";
            pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
            pstrWhere = strRefere.NameTable.ToString() + " = '" + Refere + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + intAplicacion.NameTable + "=" + Aplicacion + "  AND " + strIdioma.NameTable + "='" + Idioma + "' ";
            pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
            try
            {
                dsData = pclsDataSql.Select(pstrSql);
                if (dsData.Tables[0].Rows.Count == 0)
                {
                    pstrSelect = ptblTipoRefere.intidTipoRefere.Name.ToString() + " ";
                    pstrFrom = ptblTipoRefere.Nombre.ToString();
                    pstrWhere = ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + ptblTipoRefere.strIdioma.NameTable + "='" + Idioma + "' "; ;
                    pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);

                    dsData = pclsDataSql.Select(pstrSql);

                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        Inicialize();
                        ptblTipoRefere.intidTipoRefere.Value = dsData.Tables[0].Rows[0][0].ToString();
                        intidTipoRefere.Value = dsData.Tables[0].Rows[0][0].ToString();
                        strDetalle.Value = Detalle;
                        strRefere.Value = Refere;
                        Save();

                        pstrSelect = gstrNameTable + ".* ";
                        pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
                        pstrWhere = strRefere.NameTable.ToString() + " = '" + Refere + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + intAplicacion.NameTable + "=" + Aplicacion + "  AND " + strIdioma.NameTable + "='" + Idioma + "' ";
                        pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
                        dsData = pclsDataSql.Select(pstrSql);
                    }
                }
            }
            catch
            {
                pstrSelect = ptblTipoRefere.intidTipoRefere.Name.ToString() + " ";
                pstrFrom = ptblTipoRefere.Nombre.ToString();
                pstrWhere = ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + ptblTipoRefere.strIdioma.NameTable + "='" + Idioma + "' "; ;
                pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);

                dsData = pclsDataSql.Select(pstrSql);

                if (dsData.Tables[0].Rows.Count > 0)
                {
                    Inicialize();
                    ptblTipoRefere.intidTipoRefere.Value = dsData.Tables[0].Rows[0][0].ToString();
                    strDetalle.Value = Detalle;
                    strRefere.Value = Refere;
                    Save();

                    pstrSelect = gstrNameTable + ".* ";
                    pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
                    pstrWhere = strRefere.NameTable.ToString() + " = '" + Refere + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + intAplicacion.NameTable + "=" + Aplicacion + "  AND " + strIdioma.NameTable + "='" + Idioma + "' ";
                    pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
                    dsData = pclsDataSql.Select(pstrSql);
                }
            }
            Fill(dsData);
            return dsData;
        }
        public DataSet Get(string TipoRefere, string Refere, string Detalle, string Valor)
        {
            string Idioma = clsSesiones.getIdioma();
            string Aplicacion = clsSesiones.getAplicacion().ToString();
            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;
            string pstrOrder = null;
            string pstrSelect = null;
            DataSet dsData = new DataSet();
            tblTipoRefere ptblTipoRefere = new tblTipoRefere();
            pclsDataSql.Conexion = Conexion;

            pstrSelect = gstrNameTable + ".* ";
            pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
            pstrWhere = strRefere.NameTable.ToString() + " = '" + Refere + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + intAplicacion.NameTable + "=" + Aplicacion + "  AND " + strIdioma.NameTable + "='" + Idioma + "' ";
            pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
            try
            {
                dsData = pclsDataSql.Select(pstrSql);
                if (dsData.Tables[0].Rows.Count == 0)
                {
                    pstrSelect = ptblTipoRefere.intidTipoRefere.Name.ToString() + " ";
                    pstrFrom = ptblTipoRefere.Nombre.ToString();
                    pstrWhere = ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + ptblTipoRefere.strIdioma.NameTable + "='" + Idioma + "' "; ;
                    pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);

                    dsData = pclsDataSql.Select(pstrSql);

                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        Inicialize();
                        ptblTipoRefere.intidTipoRefere.Value = dsData.Tables[0].Rows[0][0].ToString();
                        intidTipoRefere.Value = dsData.Tables[0].Rows[0][0].ToString();
                        strDetalle.Value = Detalle;
                        strRefere.Value = Refere;
                        strValor.Value = Valor;
                        Save();

                        pstrSelect = gstrNameTable + ".* ";
                        pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
                        pstrWhere = strRefere.NameTable.ToString() + " = '" + Refere + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + intAplicacion.NameTable + "=" + Aplicacion + "  AND " + strIdioma.NameTable + "='" + Idioma + "' ";
                        pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
                        dsData = pclsDataSql.Select(pstrSql);
                    }
                }
            }
            catch
            {
                pstrSelect = ptblTipoRefere.intidTipoRefere.Name.ToString() + " ";
                pstrFrom = ptblTipoRefere.Nombre.ToString();
                pstrWhere = ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + ptblTipoRefere.strIdioma.NameTable + "='" + Idioma + "' "; ;
                pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);

                dsData = pclsDataSql.Select(pstrSql);

                if (dsData.Tables[0].Rows.Count > 0)
                {
                    Inicialize();
                    ptblTipoRefere.intidTipoRefere.Value = dsData.Tables[0].Rows[0][0].ToString();
                    strDetalle.Value = Detalle;
                    strRefere.Value = Refere;
                    strValor.Value = Valor;
                    Save();

                    pstrSelect = gstrNameTable + ".* ";
                    pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
                    pstrWhere = strRefere.NameTable.ToString() + " = '" + Refere + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + intAplicacion.NameTable + "=" + Aplicacion + "  AND " + strIdioma.NameTable + "='" + Idioma + "' ";
                    pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
                    dsData = pclsDataSql.Select(pstrSql);
                }
            }
            Fill(dsData);
            return dsData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoRefere"></param>
        /// <param name="Refere"></param>
        /// <param name="idRefereProxNivel"></param>
        /// <returns></returns>
        public DataSet Get(string TipoRefere, string Refere, int idRefereProxNivel)
        {
            DataSet dsData = new DataSet();
            try
            {
                string Idioma = clsSesiones.getIdioma();
                string Aplicacion = clsSesiones.getAplicacion().ToString();
                string pstrFrom = null;
                string pstrWhere = null;
                string pstrOrder = null;
                string pstrSelect = null;

                tblTipoRefere ptblTipoRefere = new tblTipoRefere();
                pclsDataSql.Conexion = Conexion;

                pstrSelect = gstrNameTable + ".*, " + ptblTipoRefere.strTipoRefere.Name.ToString() + ", " + ptblTipoRefere.strDescripcion.Name.ToString();
                pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
                pstrWhere = intAplicacion.NameTable + "=" + Aplicacion + "  AND " + strIdioma.NameTable + "='" + Idioma + "' AND " + strRefere.NameTable.ToString() + " = '" + Refere + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + intProxNivel.NameTable.ToString() + " = '" + idRefereProxNivel + "'";
                pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
                dsData = pclsDataSql.Select(pstrSql);
                if (dsData != null)
                {
                    Fill(dsData);
                }
                else
                {
                    Inicialize();
                    Respuesta = false;
                }
            }
            catch
            {
                Inicialize();
                Respuesta = false;
            }
            return dsData;
        }

        /// <summary>
        /// Método que recupera los registro de la tabla en la base de datos y retorna un dataset.
        /// </summary>
        /// <param name="TipoRefere">String Tipo de Referencia</param>
        /// <returns>DataSet con los datos del registro solicitado</returns>
        public DataSet GetTipoRefere(string TipoRefere)
        {
            return GetTipoRefere(TipoRefere, false);
        }
        public DataSet GetTipoRefere(int iTipoRefere)
        {
            return GetTipoRefere(iTipoRefere, false);
        }
        public DataSet GetTipoRefere(string TipoRefere, string Aplicacion)
        {
            DataSet dsData = new DataSet();
            try
            {
                string Idioma = clsSesiones.getIdioma();
                dsData = dsGetData(Idioma, Aplicacion, TipoRefere, false);

                if (dsData.Tables[0].Rows.Count.Equals(0))
                {
                    Inicialize();
                    Respuesta = false;
                }
                else
                {
                    Fill(dsData);
                }
            }
            catch
            {
                Inicialize();
                Respuesta = false;
            }
            return dsData;
        }

        public DataSet GetTipoRefere(string TipoRefere, string Aplicacion, string Idioma)
        {
            DataSet dsData = new DataSet();
            try
            {
                dsData = dsGetData(Idioma, Aplicacion, TipoRefere, false);

                if (dsData.Tables[0].Rows.Count.Equals(0))
                {
                    Inicialize();
                    Respuesta = false;
                }
                else
                {
                    Fill(dsData);
                }
            }
            catch
            {
                Inicialize();
                Respuesta = false;
            }
            return dsData;
        }
        public DataSet GetTipoRefere(string TipoRefere, string Aplicacion, string Idioma, bool SoloActivo)
        {
            DataSet dsData = new DataSet();
            try
            {
                if (SoloActivo)
                    dsData = dsGetData(Idioma, Aplicacion, TipoRefere, true);
                else
                    dsData = dsGetData(Idioma, Aplicacion, TipoRefere, false);

                if (dsData.Tables[0].Rows.Count.Equals(0))
                {
                    Inicialize();
                    Respuesta = false;
                }
                else
                {
                    Fill(dsData);
                }
            }
            catch
            {
                Inicialize();
                Respuesta = false;
            }
            return dsData;
        }
        /// <summary>
        /// Procedimiento para ganerar la tabla de un tipo de referencia, utilizada en consultas externas
        /// </summary>
        /// <param name="TipoRefere"></param>
        /// <param name="Aplicacion"></param>
        /// <param name="Idioma"></param>
        /// <param name="SoloActivo">Identifica si se se envian todos o solo los activos</param>
        /// <returns></returns>
        public DataTable GetTipoRefereTable(string TipoRefere, string Aplicacion, string Idioma, bool SoloActivo)
        {
            DataTable dtData = new DataTable();
            try
            {
                DataSet dsData = new DataSet();
                if (SoloActivo)
                    dsData = dsGetData(Idioma, Aplicacion, TipoRefere, true);
                else
                    dsData = dsGetData(Idioma, Aplicacion, TipoRefere, false);

                if (!dsData.Tables[0].Rows.Count.Equals(0))
                {
                    dtData = dsData.Tables[0];
                }
            }
            catch
            {
            }
            return dtData;
        }
        /// <summary>
        /// Utilizado para filtrar una referencia
        /// </summary>
        /// <param name="TipoRefere"></param>
        /// <param name="Refere"></param>
        /// <returns></returns>
        public DataTable GetRefereTable(string TipoRefere, string Refere, string Aplicacion, string Idioma, bool SoloActivo)
        {
            DataTable dtData = new DataTable();
            try
            {
                DataSet dsData = new DataSet();
                if (SoloActivo)
                    dsData = dsGetData(Idioma, Aplicacion, TipoRefere, true);
                else
                    dsData = dsGetData(Idioma, Aplicacion, TipoRefere, false);

                if (!dsData.Tables[0].Rows.Count.Equals(0))
                {
                    string pstrWhere = null;

                    pstrWhere = strRefere.Name.ToString() + " = '" + Refere + "'";
                    dtData = clsDataNet.dsDataWhere(pstrWhere, dsData.Tables[0]);
                }
            }
            catch
            {
            }
            return dtData;
        }
        /// <summary>
        /// Método que recupera los registro de la tabla en la base de datos y retorna un dataset.
        /// </summary>
        /// <param name="TipoRefere"></param>
        /// <param name="Activo">Parametro que indica si se listan las referencias con estado activo o no</param>
        /// <returns></returns>
        public DataSet GetTipoRefere(string TipoRefere, bool SoloActivo)
        {
            string Idioma = clsSesiones.getIdioma();
            string Aplicacion = clsSesiones.getAplicacion().ToString();
            DataSet dsData = new DataSet();
            try
            {
                dsData = dsGetData(Idioma, Aplicacion, TipoRefere, SoloActivo);
                if (dsData.Tables[0].Rows.Count.Equals(0))
                {
                    Inicialize();
                    Respuesta = false;
                }
                else
                {
                    Fill(dsData);
                }
            }
            catch
            {
                Inicialize();
                Respuesta = false;
            }
            return dsData;
        }
        public DataSet GetTipoRefere(int iTipoRefere, bool SoloActivo)
        {
            string Idioma = clsSesiones.getIdioma();
            string Aplicacion = clsSesiones.getAplicacion().ToString();
            DataSet dsData = new DataSet();
            try
            {
                dsData = dsGetData(Idioma, Aplicacion, iTipoRefere, SoloActivo);
                if (dsData.Tables[0].Rows.Count.Equals(0))
                {
                    Inicialize();
                    Respuesta = false;
                }
                else
                {
                    Fill(dsData);
                }
            }
            catch
            {
                Inicialize();
                Respuesta = false;
            }
            return dsData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoRefere"></param>
        /// <param name="Aplicacion"></param>
        /// <returns></returns>
        public DataSet GetTipoRefereIata(string TipoRefere)
        {
            DataSet dsData = new DataSet();
            try
            {
                string sIdioma = clsValidaciones.GetKeyOrAdd("TipoRefereIdioma", "Idiomas");
                string sAplicacion = clsSesiones.getAplicacion().ToString();

                string pstrSql = string.Empty;
                pclsDataSql.Conexion = Conexion;

                pstrSql = " SELECT     tblRefere.*, tblRefere.strRefere + ' ' + tblRefere.strValorAdic + ' (' + tblRefere.strDetalle + ') ' As Iata, tblRefere_1.strDetalle AS Valor ";
                pstrSql += " FROM         tblRefere AS tblRefere_1 RIGHT OUTER JOIN  tblRefere INNER JOIN tblTipoRefere ON tblRefere.intidTipoRefere = tblTipoRefere.intidTipoRefere ON CONVERT(varchar(10), tblRefere_1.intidRefere) = CONVERT(varchar(10), tblRefere.strValor) ";
                pstrSql += " WHERE     (tblTipoRefere.strTipoRefere = '" + TipoRefere + "') AND (tblRefere.strIdioma = '" + sIdioma + "') AND (tblRefere.intAplicacion = " + sAplicacion + ") ";

                dsData = pclsDataSql.Select(pstrSql);
            }
            catch { }
            return dsData;
        }
        public DataSet GetTipoRefereIata(string TipoRefere, string sIdioma, string sAplicacion)
        {
            DataSet dsData = new DataSet();
            try
            {
                string pstrSql = string.Empty;
                pclsDataSql.Conexion = Conexion;

                pstrSql = " SELECT     tblRefere.*, tblRefere.strRefere + ' ' + tblRefere.strValorAdic + ' ' + tblRefere_1.strDetalle + ' (' + tblRefere.strDetalle + ')' As Iata, tblRefere_1.strDetalle AS Valor ";
                pstrSql += " FROM         tblRefere AS tblRefere_1 RIGHT OUTER JOIN  tblRefere INNER JOIN tblTipoRefere ON tblRefere.intidTipoRefere = tblTipoRefere.intidTipoRefere ON CONVERT(varchar(10), tblRefere_1.intidRefere) = CONVERT(varchar(10), tblRefere.strValor) ";
                pstrSql += " AND tblRefere_1.intAplicacion = tblRefere.intAplicacion AND tblRefere_1.strIdioma = tblRefere.strIdioma ";
                pstrSql += " WHERE     (tblTipoRefere.strTipoRefere = '" + TipoRefere + "') AND (tblRefere.strIdioma = '" + sIdioma + "') AND (tblRefere.intAplicacion = " + sAplicacion + ") ";

                dsData = pclsDataSql.Select(pstrSql);
            }
            catch { }
            return dsData;
        }
        /// <summary>
        /// Manejo de  la IATA con Filtro adicional para el manejo
        /// via SP
        /// hceron
        /// 30/10/202
        /// </summary>
        /// <param name="TipoRefere"></param>
        /// <param name="sIdioma"></param>
        /// <param name="sAplicacion"></param>
        /// <returns></returns>
        public DataSet GetTipoRefereIataCriteria(string TipoRefere, string sIdioma, string sAplicacion, string sRefere)
        {
            DataSet dsData = new DataSet();
            try
            {
                string pstrSql = string.Empty;
                pclsDataSql.Conexion = Conexion;
                //string sWhere = " AND strValorAdic like'" + sRefere + "%' OR strDetalle like '" + sRefere + "%' OR strRefere like '"+ sRefere + "%'";
                pstrSql = " EXEC ObtenerIATA '" + sRefere + "','" + TipoRefere + "','" + sIdioma + "'," + sAplicacion + "";
                //pstrSql = " SELECT      tblRefere.strRefere + ' ' + tblRefere.strValorAdic + ' ' + tblRefere_1.strDetalle + ' (' + tblRefere.strDetalle + ')' As Iata, tblRefere_1.strDetalle AS Valor ";
                //pstrSql += " FROM         tblRefere AS tblRefere_1 RIGHT OUTER JOIN  tblRefere INNER JOIN tblTipoRefere ON tblRefere.intidTipoRefere = tblTipoRefere.intidTipoRefere ON CONVERT(varchar(10), tblRefere_1.intidRefere) = CONVERT(varchar(10), tblRefere.strValor) ";
                //pstrSql += " AND tblRefere_1.intAplicacion = tblRefere.intAplicacion AND tblRefere_1.strIdioma = tblRefere.strIdioma ";
                //pstrSql += " WHERE     (tblTipoRefere.strTipoRefere = '" + TipoRefere + "') AND (tblRefere.strIdioma = '" + sIdioma + "') AND (tblRefere.intAplicacion = " + sAplicacion + ") ";
                           

                dsData = pclsDataSql.Select(pstrSql);
            }
            catch { }
            return dsData;
        }


        public string GetTipoRefereIata(string TipoRefere, string sRefere, string sIdioma, string sAplicacion)
        {
            DataSet dsData = new DataSet();
            string sIata = sRefere;
            try
            {
                string sWhere = "strValorAdic='" + sRefere + "'";
                string pstrSql = string.Empty;
                pclsDataSql.Conexion = Conexion;

                pstrSql = " SELECT     tblRefere.*, tblRefere.strRefere + ' ' + tblRefere.strValorAdic + ' (' + tblRefere.strDetalle + ') ' + tblRefere_1.strDetalle As Iata, tblRefere_1.strDetalle AS Valor ";
                pstrSql += " FROM         tblRefere AS tblRefere_1 RIGHT OUTER JOIN  tblRefere INNER JOIN tblTipoRefere ON tblRefere.intidTipoRefere = tblTipoRefere.intidTipoRefere ON CONVERT(varchar(10), tblRefere_1.intidRefere) = CONVERT(varchar(10), tblRefere.strValor) ";
                pstrSql += " AND tblRefere_1.intAplicacion = tblRefere.intAplicacion AND tblRefere_1.strIdioma = tblRefere.strIdioma ";
                pstrSql += " WHERE     (tblTipoRefere.strTipoRefere = '" + TipoRefere + "') AND (tblRefere.strIdioma = '" + sIdioma + "') AND (tblRefere.intAplicacion = " + sAplicacion + ") ";

                dsData = pclsDataSql.Select(pstrSql);
                if (dsData != null)
                {
                    DataTable dtData = clsDataNet.dsDataWhere(sWhere, dsData.Tables[0]);
                    if (dtData.Rows.Count > 0)
                    {

                        //Se Modifica para que no tome el valor  por la posicion 0
                        // si no por aeropuerto metropolitan
                        // si encuntra filtra aun mas tblRefere.strDetalle like
                        string sNewWhere = sWhere + "and  strDetalle like '%Todos%'";
                        DataTable dtDataNewFilter = clsDataNet.dsDataWhere(sNewWhere, dsData.Tables[0]);
                          sIata = dtData.Rows[0]["Iata"].ToString();
                          if (dtDataNewFilter.Rows.Count > 0) sIata = dtDataNewFilter.Rows[0]["Iata"].ToString();
                      
                    }
                    else
                    {
                        sWhere = "strValorAdic like '%" + sRefere + "%'";
                        dtData = clsDataNet.dsDataWhere(sWhere, dsData.Tables[0]);
                        if (dtData.Rows.Count > 0)
                        {
                            sIata = dtData.Rows[0]["Iata"].ToString();
                        }
                    }
                }
            }
            catch { }
            return sIata;
        }
        /// <summary>
        /// Método que genera el script para guardar los datos en la tabla
        /// </summary>
        /// <returns>String con el código SQL para ser ejecutado</returns>
        /// <summary>
        /// Método que recupera los registro de la tabla en la base de datos y retorna un dataset.
        /// </summary>
        /// <param name="TipoRefere"></param>
        /// <param name="Activo">Parametro que indica si se listan las referencias con estado activo o no</param>
        /// <returns></returns>
        public DataSet GetTipoRefereAplicacion()
        {
            tblTipoRefere ptblTipoRefere = new tblTipoRefere();
            string pstrSql = string.Empty;
            DataSet dsData = new DataSet();
            pclsDataSql.Conexion = Conexion;
            ptblTipoRefere.Conexion = Conexion;
            string sAplicacion = "1";

            ptblTipoRefere.GetTipoRefere(clsValidaciones.GetKeyOrAdd("Aplicacion", "Aplicacion"));
            if (ptblTipoRefere.Respuesta)
                sAplicacion = ptblTipoRefere.intidTipoRefere.Value;

            pstrSql = " SELECT   intidRefere As Aplicacion";
            pstrSql += " FROM         tblRefere ";
            pstrSql += " WHERE     (intidTipoRefere = " + sAplicacion + ") ";
            pstrSql += " GROUP BY intidrefere ";

            dsData = pclsDataSql.Select(pstrSql);

            return dsData;
        }
        /// <summary>
        /// Método que recupera un registro de la tabla en la base de datos, actualiza el objeto (através del método Fill) y retorna un dataset.
        /// </summary>
        /// <param name="TipoRefere">String Tipo de Referencia</param>
        /// <param name="Refere">String Referencia</param>
        /// <returns>DataSet con los datos del registro solicitado</returns>
        public DataSet GetValor(string TipoRefere, string Valor)
        {
            string Idioma = clsSesiones.getIdioma();
            string Aplicacion = clsSesiones.getAplicacion().ToString();
            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;
            string pstrOrder = null;
            string pstrSelect = null;
            DataSet dsData = new DataSet();

            tblTipoRefere ptblTipoRefere = new tblTipoRefere();
            pclsDataSql.Conexion = Conexion;

            pstrSelect = gstrNameTable + ".* ";
            pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
            pstrWhere = strValor.NameTable.ToString() + " = '" + Valor + "' AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + intAplicacion.NameTable + "=" + Aplicacion + "  AND " + strIdioma.NameTable + "='" + Idioma + "' ";
            pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
            try
            {
                dsData = pclsDataSql.Select(pstrSql);
                if (dsData.Tables[0].Rows.Count == 0)
                {
                    Inicialize();
                }
                else
                {
                    Fill(dsData);
                }
            }
            catch { Respuesta = false; }
            return dsData;

        }
        public DataSet GetExterna(string TipoRefere, bool bExterna)
        {
            string Idioma = clsSesiones.getIdioma();
            string Aplicacion = clsSesiones.getAplicacion().ToString();
            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;
            string pstrOrder = null;
            string pstrSelect = null;
            DataSet dsData = new DataSet();
            int iValorExterna = 0;
            if (bExterna)
                iValorExterna = 1;

            tblTipoRefere ptblTipoRefere = new tblTipoRefere();
            pclsDataSql.Conexion = Conexion;

            pstrSelect = gstrNameTable + ".* ";
            pstrFrom = gstrNameTable + " INNER JOIN " + ptblTipoRefere.Nombre.ToString() + " ON " + intidTipoRefere.NameTable.ToString() + " = " + ptblTipoRefere.intidTipoRefere.NameTable.ToString();
            pstrWhere = bitExterna.NameTable.ToString() + " = " + iValorExterna.ToString() + " AND " + ptblTipoRefere.strTipoRefere.NameTable.ToString() + " = '" + TipoRefere + "' AND " + intAplicacion.NameTable + "=" + Aplicacion + "  AND " + strIdioma.NameTable + "='" + Idioma + "' ";
            pstrSql = pclsSql.SqlSentencia(pstrSelect, pstrFrom, pstrWhere, pstrOrder);
            try
            {
                dsData = pclsDataSql.Select(pstrSql);
                if (dsData.Tables[0].Rows.Count == 0)
                {
                    Inicialize();
                }
                else
                {
                    Fill(dsData);
                }
            }
            catch { Respuesta = false; }
            return dsData;

        }
        public string SaveString()
        {
            int iNumeroCampos = 16;
            int iKeys = 3;
            string[] pstrFields = new string[iNumeroCampos];
            string[] pstrValues = new string[iNumeroCampos];
            TipoCampo[] pstrTipo = new TipoCampo[iNumeroCampos];
            int iContador = 0;

            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;
            string pstrOrder = null;

            int pintMaxRefere = 0;

            pclsDataSql.Conexion = Conexion;

            if (intidRefere.Value.ToString() == "0")
            {
                pintMaxRefere = pclsDataSql.MaxConsec(Nombre.ToString(), intidRefere.Name.ToString(), intidRefere.TypeDat);
                pintMaxRefere++;
                intidRefere.Value = pintMaxRefere.ToString();
            }

            pstrFrom = gstrNameTable;

            iContador = 0;
            pstrFields[iContador++] = intidTipoRefere.Name.ToString();
            pstrFields[iContador++] = intNivel.Name.ToString();
            pstrFields[iContador++] = strRefere.Name.ToString();
            pstrFields[iContador++] = strDetalle.Name.ToString();
            pstrFields[iContador++] = strValor.Name.ToString();
            pstrFields[iContador++] = strValorAdic.Name.ToString();
            pstrFields[iContador++] = intProxNivel.Name.ToString();
            pstrFields[iContador++] = bitExterna.Name.ToString();
            pstrFields[iContador++] = strTable.Name.ToString();
            pstrFields[iContador++] = strImagen.Name.ToString();
            pstrFields[iContador++] = bitActivo.Name.ToString();
            pstrFields[iContador++] = strLink.Name.ToString();
            pstrFields[iContador++] = intOrden.Name.ToString();
            pstrFields[iContador++] = intidRefere.Name.ToString();
            pstrFields[iContador++] = intAplicacion.Name.ToString();
            pstrFields[iContador++] = strIdioma.Name.ToString();

            iContador = 0;
            pstrTipo[iContador++] = intidTipoRefere.TypeDat;
            pstrTipo[iContador++] = intNivel.TypeDat;
            pstrTipo[iContador++] = strRefere.TypeDat;
            pstrTipo[iContador++] = strDetalle.TypeDat;
            pstrTipo[iContador++] = strValor.TypeDat;
            pstrTipo[iContador++] = strValorAdic.TypeDat;
            pstrTipo[iContador++] = intProxNivel.TypeDat;
            pstrTipo[iContador++] = bitExterna.TypeDat;
            pstrTipo[iContador++] = strTable.TypeDat;
            pstrTipo[iContador++] = strImagen.TypeDat;
            pstrTipo[iContador++] = bitActivo.TypeDat;
            pstrTipo[iContador++] = strLink.TypeDat;
            pstrTipo[iContador++] = intOrden.TypeDat;
            pstrTipo[iContador++] = intidRefere.TypeDat;
            pstrTipo[iContador++] = intAplicacion.TypeDat;
            pstrTipo[iContador++] = strIdioma.TypeDat;

            iContador = 0;
            pstrValues[iContador++] = intidTipoRefere.Value;
            pstrValues[iContador++] = intNivel.Value;
            pstrValues[iContador++] = strRefere.Value;
            pstrValues[iContador++] = strDetalle.Value;
            pstrValues[iContador++] = strValor.Value;
            pstrValues[iContador++] = strValorAdic.Value;
            pstrValues[iContador++] = intProxNivel.Value;
            pstrValues[iContador++] = bitExterna.Value;
            pstrValues[iContador++] = strTable.Value;
            pstrValues[iContador++] = strImagen.Value;
            pstrValues[iContador++] = bitActivo.Value;
            pstrValues[iContador++] = strLink.Value;
            pstrValues[iContador++] = intOrden.Value;
            pstrValues[iContador++] = intidRefere.Value;
            //pstrValues[iContador++] = intAplicacion.Value;
            //pstrValues[iContador++] = strIdioma.Value;


            if (pintMaxRefere == 0)
            {
                if (intAplicacion.Value.ToString().Equals("0"))
                {
                    pstrWhere = intidRefere.Name.ToString() + " = " + intidRefere.Value.ToString();
                }
                else
                {
                    if (strIdioma.Value.ToString().Equals(""))
                    {
                        pstrWhere = intidRefere.Name.ToString() + " = " + intidRefere.Value.ToString() + " AND " + intAplicacion.Name.ToString() + " = " + intAplicacion.Value.ToString();
                    }
                    else
                    {
                        pstrWhere = intidRefere.Name.ToString() + " = " + intidRefere.Value.ToString() + " AND " + intAplicacion.Name.ToString() + " = " + intAplicacion.Value.ToString() + " AND " + strIdioma.Name.ToString() + " = '" + strIdioma.Value.ToString() + "' ";
                    }

                }
                pstrSql = pstrSql + pclsSql.SqlSentencia(TipoComando.Update, pstrFields, pstrValues, pstrTipo, pstrFrom, pstrWhere, pstrOrder, (iNumeroCampos - iKeys)) + "   ";
            }
            else
            {
                DataSet dsAplicacion = GetTipoRefereAplicacion();
                if (dsAplicacion != null && dsAplicacion.Tables.Count > 0)
                {
                    int j = 0;
                    while (j < dsAplicacion.Tables[0].Rows.Count)
                    {
                        DataSet dsIdiomas = GetTipoRefere(clsValidaciones.GetKeyOrAdd("TipoRefereIdioma", "Idiomas"), dsAplicacion.Tables[0].Rows[j]["Aplicacion"].ToString(), "es");
                        if (dsIdiomas != null && dsIdiomas.Tables.Count > 0)
                        {
                            int i = 0;
                            while (i < dsIdiomas.Tables[0].Rows.Count)
                            {
                                pstrValues[iContador++] = dsAplicacion.Tables[0].Rows[j]["Aplicacion"].ToString();
                                pstrValues[iContador++] = dsIdiomas.Tables[0].Rows[i][strRefere.Name].ToString();
                                pstrSql = pstrSql + pclsSql.SqlSentencia(TipoComando.Insert, pstrFields, pstrValues, pstrTipo, pstrFrom, pstrWhere, pstrOrder, iNumeroCampos) + "   ";
                                i++;
                                iContador--;
                            }
                        }
                        else
                        {
                            pstrValues[iContador++] = dsAplicacion.Tables[0].Rows[j]["Aplicacion"].ToString();
                            pstrValues[iContador++] = strIdioma.Value;
                            pstrSql = pstrSql + pclsSql.SqlSentencia(TipoComando.Insert, pstrFields, pstrValues, pstrTipo, pstrFrom, pstrWhere, pstrOrder, iNumeroCampos) + "   ";
                            j++;
                            iContador--;
                        }
                    }
                }
                else
                {
                    pstrValues[iContador++] = intAplicacion.Value;
                    pstrValues[iContador++] = strIdioma.Value;
                    pstrSql = pstrSql + pclsSql.SqlSentencia(TipoComando.Insert, pstrFields, pstrValues, pstrTipo, pstrFrom, pstrWhere, pstrOrder, iNumeroCampos) + "   ";
                }
            }
            return pstrSql;
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla
        /// </summary>
        public void Delete()
        {
            string pstrSql = string.Empty;
            string pstrFrom = null;
            string pstrWhere = null;

            pclsDataSql.Conexion = Conexion;

            pstrFrom = gstrNameTable;

            if (intAplicacion.Value.ToString().Equals("0"))
            {
                pstrWhere = intidRefere.Name.ToString() + " = " + intidRefere.Value.ToString();
            }
            else
            {
                if (strIdioma.Value.ToString().Equals(""))
                {
                    pstrWhere = intidRefere.Name.ToString() + " = " + intidRefere.Value.ToString() + " AND " + intAplicacion.Name.ToString() + " = " + intAplicacion.Value.ToString();
                }
                else
                {
                    pstrWhere = intidRefere.Name.ToString() + " = " + intidRefere.Value.ToString() + " AND " + intAplicacion.Name.ToString() + " = " + intAplicacion.Value.ToString() + " AND " + strIdioma.Name.ToString() + " = '" + strIdioma.Value.ToString() + "' ";
                }

            }
            pstrSql = pstrSql + pclsSql.SqlSentencia(TipoComando.Delete, null, null, null, pstrFrom, pstrWhere, null, 0) + "   ";
            pclsDataSql.UpdateInsert(pstrSql);
        }

        /// <summary>
        /// Métodp para inicializar el objeto
        /// </summary>
        public void Inicialize()
        {
            intidRefere.Value = "0";
            intidTipoRefere.Value = "0";
            intNivel.Value = "0";
            strRefere.Value = string.Empty;
            strDetalle.Value = string.Empty;
            strValor.Value = string.Empty;
            strValorAdic.Value = string.Empty;
            intProxNivel.Value = "0";
            bitExterna.Value = "0";
            strTable.Value = string.Empty;
            bitActivo.Value = "1";
            strImagen.Value = string.Empty;
            strLink.Value = string.Empty;
            intOrden.Value = "0";
            intAplicacion.Value = clsSesiones.getAplicacion().ToString(); Respuesta = false;
            strIdioma.Value = clsSesiones.getIdioma();
        }
        #endregion

        //#region [Procedures]
        //public DataSet spGetRefere(string aplicacion, string idioma, string codigo, bool valida = true)
        //{
        //    DataSet dsData = new DataSet();

        //    pclsDataSql.Conexion = Conexion;

        //    dsData = pclsDataSql.SelectSp("spGetRefere"
        //        , new string[] { 
        //            aplicacion
        //            , idioma
        //            , codigo
        //            , valida.ToString()
        //        });

        //    Fill(dsData);
        //    return dsData;
        //}

        //public DataSet spGetTipoRefere(string aplicacion, string idioma, string tipoRefere, bool activo = false)
        //{
        //    DataSet dsData = new DataSet();

        //    pclsDataSql.Conexion = Conexion;

        //    dsData = pclsDataSql.SelectSp("spGetTipoRefere"
        //        , new string[] { 
        //            aplicacion
        //            , idioma
        //            , tipoRefere
        //            , activo.ToString()
        //        });

        //    Fill(dsData);
        //    return dsData;
        //}

        //public DataSet spGetExt(string aplicacion, string idioma, string refere, string tipoRefere, bool valida = true)
        //{
        //    DataSet dsData = new DataSet();

        //    pclsDataSql.Conexion = Conexion;

        //    dsData = pclsDataSql.SelectSp("spGetRefereExt"
        //        , new string[] { 
        //            aplicacion
        //            , idioma
        //            , refere
        //            , valida.ToString()
        //        });

        //    Fill(dsData);
        //    return dsData;
        //}
        //#endregion
    }
}
