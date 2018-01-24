using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ssoft.Utils;
using Ssoft.Sql;

namespace SsoftQuery.Reserva
{
    public class csConsultasReserva
    {
        public DataTable ConReservasProyectos(string sProyecto)
        {
            string[] ParametrosSp = new string[1];
            ParametrosSp[0] = sProyecto;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaReservasProyecto", ParametrosSp);
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
        /// Metodo que consulta las reservas ya sea de un usuario especifico o completas si como pueden ser vigentes o historicas
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-01-23
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable consulta_reservas_usuario(string Contacto, string MesSolicitud, string AnioSolicitud, bool Vigentes, string Empresa)
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian

            string[] ParametrosSp = new string[6];

            ParametrosSp[0] = sIdioma;
            ParametrosSp[1] = Empresa;
            if (Vigentes)
                ParametrosSp[2] = "1";
            else
                ParametrosSp[2] = "0";

            ParametrosSp[3] = MesSolicitud;
            ParametrosSp[4] = AnioSolicitud;
            ParametrosSp[5] = Contacto;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaReservasGeneral", ParametrosSp);
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
        /// Metodo que consulta los datos generales de una reserva especifica
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-01-24
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataSet consulta_detalle_general(string Localizador)
        {
            string sIdioma = clsSesiones.getIdioma();
            string[] ParametrosSp = new string[2];
            ParametrosSp[0] = sIdioma;
            ParametrosSp[1] = Localizador;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaDetallereservaGeneral", ParametrosSp);
            if (dsDatosConsulta != null && dsDatosConsulta.Tables.Count > 0 && dsDatosConsulta.Tables[0].Rows.Count > 0)
            {
                //se retorna la tabla
                return dsDatosConsulta;
            }
            else
            {
                //si no hay resultados se retorna null
                return null;
            }
        }

        /// <summary>
        /// Metodo que consulta los datos de los pasajeros de una reserva especifica
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-01-25
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable consulta_pasajeros_reserva(string Reserva, string segmento)
        {
            string sIdioma = clsSesiones.getIdioma();
            string[] ParametrosSp = new string[3];
            ParametrosSp[0] = sIdioma;
            ParametrosSp[1] = Reserva;
            ParametrosSp[2] = segmento;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaPasajerosReserva", ParametrosSp);
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
        /// Metodo que consulta los datos especificos de una reserva de a cuerdo al tipo de servicio
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-01-24
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable consulta_detalle_tipo_servicio(string Localizador, string Reserva, string TipoServicio)
        {
            string sIdioma = clsSesiones.getIdioma();
            string[] ParametrosSp = new string[4];
            ParametrosSp[0] = TipoServicio;
            ParametrosSp[1] = sIdioma;
            ParametrosSp[2] = Localizador;
            ParametrosSp[3] = Reserva;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaDetallereservaTipoServicio", ParametrosSp);
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
        /// Metodo que consulta las paginas de agradecimiento dependiendo del tipo de servicio y forma de pago
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-04-10
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable consulta_pagina_agradecimiento(string TipoDestino, string TipoTrayecto, string TipoServicio,
            string FormaPago, string Empresa, string sEstadoPago)
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian

            string[] ParametrosSp = new string[7];

            ParametrosSp[0] = TipoDestino;
            ParametrosSp[1] = TipoTrayecto;
            ParametrosSp[2] = TipoServicio;
            ParametrosSp[3] = FormaPago;
            ParametrosSp[4] = sIdioma;
            ParametrosSp[5] = Empresa;
            ParametrosSp[6] = sEstadoPago;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaPaginaAgradecimiento", ParametrosSp);
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
