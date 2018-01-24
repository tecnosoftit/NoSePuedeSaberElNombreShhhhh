using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ssoft.Sql;
using Ssoft.Utils;

namespace SsoftQuery.SeccionesInformativas
{
    public class csConsultaSecciones
    {
        public DataSet ConsultaSeccionesGeneral(string strCodigoSeccion, string strRefereSecPublicacion,
            string intCodigoPadre, string intNivel, string posicion, string Empresa, string cantidadRegistros, string parametroOrden)
        {
            string sIdioma = clsSesiones.getIdioma();
            string[] ParametrosSp = new string[9];

            ParametrosSp[0] = sIdioma;

            if (strCodigoSeccion != null)
                ParametrosSp[1] = strCodigoSeccion;
            else
                ParametrosSp[1] = "0";

            if(strRefereSecPublicacion != null)
                ParametrosSp[2] = strRefereSecPublicacion;
            else
                ParametrosSp[2] = "0";

            if(intCodigoPadre != null)
                ParametrosSp[3] = intCodigoPadre;
            else
                ParametrosSp[3] = "0";

            if(intNivel != null)
                ParametrosSp[4] = intNivel;
            else
                ParametrosSp[4] = "";

             if(posicion != null)
                ParametrosSp[5] = posicion;
             else
                ParametrosSp[5] = "0";

             if(Empresa != null)
                ParametrosSp[6] = Empresa;
             else
                ParametrosSp[6] = "0";

             if(cantidadRegistros != null)
                ParametrosSp[7] = cantidadRegistros;
             else
                ParametrosSp[7] = "0";

             if(parametroOrden != null)
                ParametrosSp[8] = parametroOrden;
             else
                ParametrosSp[8] = "0";

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsResultado = dsConsulta.Select("SPConsultaSeccionInformativa", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado;
            }
            else
                return null;
        }

        public DataSet ConsultaRelacionesSecciones(string strCodigoSeccion, string strTipoRelacion)
        {
            string sIdioma = clsSesiones.getIdioma();
            string[] ParametrosSp = new string[3];

            ParametrosSp[0] = sIdioma;

            if (strCodigoSeccion != null)
                ParametrosSp[1] = strCodigoSeccion;

            //Tipo de relacion 0 = galerias; tipo de relacion  = links
            if (strTipoRelacion != null)
                ParametrosSp[2] = strTipoRelacion;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsResultado = dsConsulta.Select("SPConsultaRelacionesSeccionInformativa", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado;
            }
            else
                return null;
        }
    }
}
