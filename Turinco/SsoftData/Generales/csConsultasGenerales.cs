using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ssoft.Utils;
using Ssoft.Sql;

namespace SsoftQuery.Generales
{
    public class csConsultasGenerales
    {
        /// <summary>
        /// Metodo que llama al procedimiento almacenado para consultar generos
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-02
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        public DataTable ConReferenciaSexo()
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian          

            string[] ParametrosSp = new string[1];
            ParametrosSp[0] = sIdioma;         

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPCONSULTAGENERO", ParametrosSp);
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
        /// Metodo que llama al procedimiento almacenado para consultar tipos de pasajeros
        /// </summary>
        /// <param name="sTipoPax"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-02
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        public DataTable ConReferenciaTiposPax(string sTipoPax)
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian          

            string[] ParametrosSp = new string[2];
            ParametrosSp[0] = sTipoPax;
            ParametrosSp[1] = sIdioma;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaTipoPasajero", ParametrosSp);
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
        /// Metodo que llama al procedimiento almacenado para consultar tipos de identificacion
        /// </summary>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-02
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        public DataTable ConReferenciaTiposIdentificacion()
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian          

            string[] ParametrosSp = new string[1];
            ParametrosSp[0] = sIdioma;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaTpoidentifica", ParametrosSp);
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
        /// Metodo que llama al procedimiento almacenado para consultar un usuario especifico
        /// </summary>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-02
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        public DataTable ConsultaUsuario(string sIdUsuario)
        {
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian
            string[] ParametrosSp = new string[1];
            ParametrosSp[0] = sIdUsuario;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaUsuario", ParametrosSp);
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
        /// Metodo que consulta la referencia de paises usando el metodo general que genera la consulta
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-01-27
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable listado_paises()
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian
            string sSelect = "SELECT * ";
            string sFrom = "FROM  Tblpais INNER JOIN tblPaisIdioma ON Tblpais.IntCode = tblPaisIdioma.intCodigoPais ";
            string sWhere = "WHERE tblPaisIdioma.strIdioma = '" + sIdioma + "'";
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
        /// Metodo que consulta la referencia ciudades de un pais usando el metodo general que genera la consulta
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-01-27
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable listado_ciudades_pais(string sPais)
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian
            string sSelect = "SELECT * ";
            string sFrom = "FROM TblCiudades INNER JOIN tblCiudadesIdiomas ON TblCiudades.IntCode=tblCiudadesIdiomas.intCode ";
            string sWhere = "WHERE tblCiudadesIdiomas.strIdioma = '" + sIdioma + "' AND TblCiudades.intCodigoPais=" + sPais;
            string sOrder = "ORDER BY strDescription";

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
    }
}
