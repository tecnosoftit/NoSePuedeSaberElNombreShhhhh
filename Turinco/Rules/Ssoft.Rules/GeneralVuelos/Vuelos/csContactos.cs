using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Rules.Generales;
using System.Data;
using System.Web.UI.WebControls;
using Ssoft.Sql;
using Ssoft.Utils;
using System.Configuration;
using Ssoft.ManejadorExcepciones;

namespace Ssoft.Rules.Administrador
{
    public class csContactos
    {
        protected string strConexion = string.Empty;
        protected csGenerales Generales = new csGenerales();

        public string Conexion
        {
            set { this.strConexion = value; }
            get { return this.strConexion; }
        }
        public csContactos()
        {
            strConexion = clsSesiones.getConexion();
        }       
        public DataSet ConsultaDatosContacto(string Contacto, string strTipoPasajero)
        {
            StringBuilder consulta = new StringBuilder();
            DataSet dsData = new DataSet();

            consulta.AppendLine(" SELECT  tblcontactos.*, '" + strTipoPasajero + "' As strTipoPasajero  ");
            consulta.AppendLine(" FROM  tblcontactos ");
            consulta.AppendLine(" where intContacto=" + Contacto);


            DataSql dsConsulta = new DataSql();
            dsConsulta.Conexion = strConexion;
            dsData = dsConsulta.Select(consulta.ToString());

            return dsData;
        }
        public DataSet ContactosNivel(string sContacto)
        {
            Generales.Conexion = this.Conexion;
            DataSet dsData = new DataSet();

            StringBuilder Consulta = new StringBuilder();

            Consulta.AppendLine(" SELECT     tblcontactos.*, tblcontactos.intContactoPadre AS intContactoEmpresa, tblcontactos_1.intContactoPadre AS intContactoAgencia ");
            Consulta.AppendLine("  FROM         tblcontactos INNER JOIN ");
            Consulta.AppendLine("  tblcontactos AS tblcontactos_1 ON tblcontactos.intAplicacion = tblcontactos_1.intAplicacion AND  ");
            Consulta.AppendLine("  tblcontactos.intContactoPadre = tblcontactos_1.intContacto ");
            Consulta.AppendLine("   ");
            Consulta.AppendLine("   ");
            Consulta.AppendLine("  WHERE     (tblcontactos.intContacto = " + sContacto + ")  ");

            dsData = Generales.ConsultaTabla(Consulta.ToString());
            return dsData;
        }
    }
}
