using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ssoft.Utils;
using Ssoft.Sql;

namespace SsoftQuery.Usuarios
{
    public class csConsultasUsuarios
    {
        /// <summary>
        /// Metodo que consultaun usuario especifico con toda su informacion
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-01-22
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable consulta_usuario(string Usuario)
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian

            string[] ParametrosSp = new string[2];
            ParametrosSp[0] = Usuario;
            ParametrosSp[1] = sIdioma;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaUsuarioCompleto", ParametrosSp);
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
        /// Metodo que modifica la contraseña de un usuario especifico
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
        public DataTable modificar_contrasenia_usuario(string Usuario, string Contrasenia)
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian

            string[] ParametrosSp = new string[2];
            ParametrosSp[0] = Usuario;
            ParametrosSp[1] = Contrasenia;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPCambiarContraseniaUsuario", ParametrosSp);
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
        /// Metodo que modifica la contraseña de un usuario especifico
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
        public DataTable modificar_datos_usuario(string Usuario, string Nombre, string Apellido, string TipoIdentificacion, string Identificacion,
            string Genero, string Fechanacimiento, string Direccion, string Telefono, string Celular, string ciudad)
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian

            string[] ParametrosSp = new string[11];
            ParametrosSp[0] = Usuario;
            ParametrosSp[1] = Nombre;
            ParametrosSp[2] = Apellido;
            ParametrosSp[3] = TipoIdentificacion;
            ParametrosSp[4] = Identificacion;
            ParametrosSp[5] = Genero;
            ParametrosSp[6] = Fechanacimiento;
            ParametrosSp[7] = Direccion;
            ParametrosSp[8] = Telefono;
            ParametrosSp[9] = Celular;
            ParametrosSp[10] = ciudad;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPModificarDatosUsuario", ParametrosSp);
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
