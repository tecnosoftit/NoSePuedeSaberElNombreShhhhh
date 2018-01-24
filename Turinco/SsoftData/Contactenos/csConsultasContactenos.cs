using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ssoft.Utils;
using Ssoft.Sql;

namespace SsoftQuery.Contactenos
{
    public class csConsultasContactenos
    {
        /// <summary>
        /// Metodo que consulta la referencia de tipos de mensaje usando el metodo general que genera la consulta
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-02-26
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable listado_tipos_mensaje()
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian
            string sSelect = "SELECT * ";
            string sFrom = "FROM  tblTiposMensaje INNER JOIN tblTiposMensajeIdioma ON tblTiposMensaje.IntCodigo = tblTiposMensajeIdioma.intCodigo ";
            string sWhere = "WHERE tblTiposMensajeIdioma.strIdioma = '" + sIdioma + "'";
            string sOrder = "ORDER BY strDescripcion";

            string[] ParametrosSp = new string[4];
            ParametrosSp[0] = sSelect;
            ParametrosSp[1] = sFrom;
            ParametrosSp[2] = sWhere;
            ParametrosSp[3] = sOrder;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaGeneralReferencias", ParametrosSp);
            if (dsDatosConsulta != null && dsDatosConsulta.Tables.Count > 0 && dsDatosConsulta.Tables[0].Rows.Count > 0)
            {
                //se retorna la tabla
                return dsDatosConsulta.Tables[0];
            }
            else
            {
                //si no hay resultados se retorna null
                return null;
            }
        }

        /// <summary>
        /// Metodo que consulta la referencia de temas de mensaje de un determinado tipo usando el metodo general que genera la consulta
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-02-26
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable listado_temas_mensaje(string sTipoMensaje)
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian
            string sSelect = "SELECT * ";
            string sFrom = "FROM  tblTemasMensaje INNER JOIN tblTemasMensajeIdioma ON tblTemasMensaje.IntCodigo = tblTemasMensajeIdioma.intCodigo ";
            string sWhere = "WHERE tblTemasMensajeIdioma.strIdioma = '" + sIdioma + "' ";
            if (sTipoMensaje != null && sTipoMensaje != "0" && sTipoMensaje != "")
                sWhere = sWhere + "AND tblTemasMensaje.intTipoMensaje = " + sTipoMensaje + "";
            string sOrder = "ORDER BY strDescripcion";

            string[] ParametrosSp = new string[4];
            ParametrosSp[0] = sSelect;
            ParametrosSp[1] = sFrom;
            ParametrosSp[2] = sWhere;
            ParametrosSp[3] = sOrder;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaGeneralReferencias", ParametrosSp);
            if (dsDatosConsulta != null && dsDatosConsulta.Tables.Count > 0 && dsDatosConsulta.Tables[0].Rows.Count > 0)
            {
                //se retorna la tabla
                return dsDatosConsulta.Tables[0];
            }
            else
            {
                //si no hay resultados se retorna null
                return null;
            }
        }

        /// <summary>
        /// Metodo que consulta el codigo de la referencia de estados de buzon usando el metodo general que genera la consulta
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-02-26
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable codigo_estado_buzon(string sEstado)
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian
            string sSelect = "SELECT * ";
            string sFrom = "FROM  tblEstadosBuzonContacto ";
            string sWhere = "WHERE strCode = '" + sEstado + "' ";
            string sOrder = "";

            string[] ParametrosSp = new string[4];
            ParametrosSp[0] = sSelect;
            ParametrosSp[1] = sFrom;
            ParametrosSp[2] = sWhere;
            ParametrosSp[3] = sOrder;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaGeneralReferencias", ParametrosSp);
            if (dsDatosConsulta != null && dsDatosConsulta.Tables.Count > 0 && dsDatosConsulta.Tables[0].Rows.Count > 0)
            {
                //se retorna la tabla
                return dsDatosConsulta.Tables[0];
            }
            else
            {
                //si no hay resultados se retorna null
                return null;
            }
        }

        /// <summary>
        /// Metodo que inserta elregistro del buzon de contacto
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-02-26
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable insertar_buzon(string intEmpresa, string intProyecto,string intTipo,string intTema,string intEstado,
            string strDescripcion,string dtmFecha,string strHora,string strSolicitante,string strEmail,string intPais,string intCiudad,string dtmFechaCierre,
            string strHoraCierre,string intUsuario,string strTelSolicitante,string strCiudad)
        {
            string sIdioma = clsSesiones.getIdioma();

            string[] ParametrosSp = new string[18];
            int cont = 0;
            ParametrosSp[cont++] = intEmpresa;
            ParametrosSp[cont++] = sIdioma;
            ParametrosSp[cont++] = intProyecto;
            ParametrosSp[cont++] = intTipo;
            ParametrosSp[cont++] = intTema;
            ParametrosSp[cont++] = intEstado;
            ParametrosSp[cont++] = strDescripcion;
            ParametrosSp[cont++] = dtmFecha;
            ParametrosSp[cont++] = strHora;
            ParametrosSp[cont++] = strSolicitante;
            ParametrosSp[cont++] = strEmail;
            ParametrosSp[cont++] = intPais;
            ParametrosSp[cont++] = intCiudad;
            ParametrosSp[cont++] = dtmFechaCierre;
            ParametrosSp[cont++] = strHoraCierre;
            ParametrosSp[cont++] = intUsuario;
            ParametrosSp[cont++] = strTelSolicitante;
            ParametrosSp[cont++] = strCiudad;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPInsertarBuzon", ParametrosSp);
            if (dsDatosConsulta != null && dsDatosConsulta.Tables.Count > 0 && dsDatosConsulta.Tables[0].Rows.Count > 0)
            {
                //se retorna la tabla
                return dsDatosConsulta.Tables[0];
            }
            else
            {
                //si no hay resultados se retorna null
                return null;
            }
        }
    }
}
