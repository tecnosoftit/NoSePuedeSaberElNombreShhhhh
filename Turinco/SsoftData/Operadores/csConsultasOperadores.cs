using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ssoft.Utils;
using Ssoft.Sql;

namespace SsoftQuery.Operadores
{
    public class csConsultasOperadores
    {
        /// <summary>
        /// Metodo que consulta el detalle de un operador
        /// </summary>
        /// <param name="strCategoria"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-02-27
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable Cosulta_detalle_operador(string strCodigo)
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian

            string[] ParametrosSp = new string[2];
            ParametrosSp[0] = strCodigo;
            ParametrosSp[1] = sIdioma;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaDetalleOperador", ParametrosSp);
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
