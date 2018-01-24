using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ssoft.Utils;
using System.Data;
using Ssoft.Sql;

namespace SsoftQuery.Banners
{
    public class csConsultasBanners
    {
        /// <summary>
        /// Metodo que consulta consulta los banners haciendo el llamado al SP especifico
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="iPos"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-27
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataSet ConsultaBanners(string Pagina, string Empresa)
        {
            string sIdioma = clsSesiones.getIdioma();
            string[] ParametrosSp = new string[3];

            ParametrosSp[0] = sIdioma;
            ParametrosSp[1] = Empresa;
            ParametrosSp[2] = Pagina;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsResultado = dsConsulta.Select("SPConsultaBanners", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado;
            }
            else
                return null;
        }
    }
}
