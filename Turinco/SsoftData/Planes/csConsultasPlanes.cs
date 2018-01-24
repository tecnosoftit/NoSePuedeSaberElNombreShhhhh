using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ssoft.Sql;
using Ssoft.Utils;
using SsoftQuery.Vuelos;

namespace SsoftQuery.Planes
{
    public class csConsultasPlanes
    {
        private static string sFormatoFecha = clsSesiones.getFormatoFecha();
        private static string sFormatoFechaBD = clsSesiones.getFormatoFechaBD();

        /// <summary>
        /// Metodo que consulta la referencia de paises usando el metodo general que genera la consulta
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-13
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable ConReferenciaPaises(string sReferePais)
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian
            string sSelect = "SELECT *";
            string sFrom = "FROM  Tblpais INNER JOIN tblPaisIdioma ON Tblpais.IntCode = tblPaisIdioma.intCodigoPais";
            string sWhere = "WHERE Tblpais.StrCountryCode = '" + sReferePais + "' AND tblPaisIdioma.strIdioma = '" + sIdioma + "'";

            string[] ParametrosSp = new string[4];
            ParametrosSp[0] = sSelect;
            ParametrosSp[1] = sFrom;
            ParametrosSp[2] = sWhere;
            ParametrosSp[3] = "";

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
        /// Metodo que consulta la referencia de paises usando el metodo general que genera la consulta filtrando por el id de pais
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-14
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable ConReferenciaPaisesId(string sIdPais)
        {
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian
            string sIdioma = clsSesiones.getIdioma();
            string sSelect = "SELECT *";
            string sFrom = "FROM  Tblpais INNER JOIN tblPaisIdioma ON Tblpais.IntCode = tblPaisIdioma.intCodigoPais";
            string sWhere = "WHERE Tblpais.intCode = " + sIdPais + " AND tblPaisIdioma.strIdioma = '" + sIdioma + "'";

            string[] ParametrosSp = new string[4];
            ParametrosSp[0] = sSelect;
            ParametrosSp[1] = sFrom;
            ParametrosSp[2] = sWhere;
            ParametrosSp[3] = "";

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
        /// Metodo que consulta la referencia de ciudades usando el metodo general que genera la consulta filtrando por el id de ciudad
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-14
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable ConReferenciaCiudadesId(string sIdCiudad)
        {
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian
            string sIdioma = clsSesiones.getIdioma();
            string sSelect = "SELECT *";
            string sFrom = "FROM  TblCiudadesPlanes INNER JOIN TblCiudadesPlanesIdioma ON TblCiudadesPlanes.IntCode = TblCiudadesPlanesIdioma.intCodCiudad";
            string sWhere = "WHERE TblCiudadesPlanes.intCode = " + sIdCiudad + " AND TblCiudadesPlanesIdioma.strIdioma = '" + sIdioma + "'";

            string[] ParametrosSp = new string[4];
            ParametrosSp[0] = sSelect;
            ParametrosSp[1] = sFrom;
            ParametrosSp[2] = sWhere;
            ParametrosSp[3] = "";

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
        /// Metodo que consulta la referencia de zonas geograficas usando el metodo general que genera la consulta
        /// </summary>
        /// <param name="sRefereZona"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-13
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable ConReferenciaZonasGeograficas(string sRefereZona)
        {
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian
            string sIdioma = clsSesiones.getIdioma();
            string sSelect = "SELECT *";
            string sFrom = "FROM  TblZonaGeografica INNER JOIN TblZonaGeograficaIdioma ON TblZonaGeografica.IntIdZona = TblZonaGeograficaIdioma.IntZonaGeografica";
            string sWhere = "WHERE TblZonaGeografica.StrCode = '" + sRefereZona + "' AND TblZonaGeograficaIdioma.strIdioma = '" + sIdioma + "'";

            string[] ParametrosSp = new string[4];
            ParametrosSp[0] = sSelect;
            ParametrosSp[1] = sFrom;
            ParametrosSp[2] = sWhere;
            ParametrosSp[3] = "";

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
        /// Metodo que devuelve las referencias de ubicacion asociadas a un plan mediante la multiciudad y validando que el plan este activo
        /// </summary>
        /// <param name="TipoReferencia">Tipo de referencia</param>
        /// <param name="CodPadre">Referencia padre</param>
        /// <param name="CampoAsociado">Campo de planes para validacion</param>
        /// <param name="TipoPlan">Tipo de plan (opcional)</param>
        /// <returns>Tabla de referencias</returns>
        ///<remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-13
        /// -------------------
        /// Control de Cambios       
        /// -------------------
        /// </remarks>
        public DataTable ConsultarPaises_CiudadesPlanes(bool bConsultaPais, int CodPadre, string Empresa)
        {
            string sIdioma = clsSesiones.getIdioma();
            string sSelect = "";
            int iPais = 0;
            int iZona = 0;
            if (bConsultaPais)
            {
                sSelect = "SELECT DISTINCT tblpais.intCode, tblpais.strCountryCode, tblpaisidioma.strDescripcion";
                iZona = CodPadre;
            }
            else
            {
                sSelect = "SELECT DISTINCT tblCiudadesPlanes.intCode, tblCiudadesPlanes.strcode, tblCiudadesPlanesIdioma.strDescripcion ";
                iPais = CodPadre;
            }

            string[] ParametrosSp = new string[5];
            ParametrosSp[0] = iPais.ToString();
            ParametrosSp[1] = iZona.ToString();
            ParametrosSp[2] = sIdioma;
            ParametrosSp[3] = sSelect;
            ParametrosSp[4] = Empresa;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaPaisesCiudadesPlanes", ParametrosSp);
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
        public DataTable ConsultarPaises_ZonasSeguros(string Empresa, string sTipoPlan)
        {
            string sIdioma = clsSesiones.getIdioma();
            sTipoPlan = new CsConsultasVuelos().ConsultaCodigo(sTipoPlan, "tblTiposPlan", "intCodigo", "strCode");

            string[] ParametrosSp = new string[3];
            ParametrosSp[0] = sTipoPlan.ToString();
            ParametrosSp[1] = sIdioma;
            ParametrosSp[2] = Empresa;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaPaisesZonasPlanes", ParametrosSp);
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
        public DataTable ConsultarTipologias(string Empresa)
        {

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            //string strConsulta = "SELECT DISTINCT TBLTIPOLOGIASPLAN.INTCODIGO,STRDESCRIPCION FROM TBLPLANES INNER JOIN TBLTIPOLOGIASPLAN_PLAN ON TBLTIPOLOGIASPLAN_PLAN.INTCODPLAN=TBLPLANES.INTCODIGO INNER JOIN TBLTIPOLOGIASPLAN ON TBLTIPOLOGIASPLAN.INTCODIGO=TBLTIPOLOGIASPLAN_PLAN.INTCODTIPOLOGIAPLAN INNER JOIN TBLTIPOLOGIASPLANIDIOMA ON TBLTIPOLOGIASPLAN.INTCODIGO=TBLTIPOLOGIASPLANIDIOMA.INTCODIGO WHERE TBLPLANES.BITACTIVO=1 AND INTEMPRESA='" + Empresa + "'";
            string strConsulta = "SELECT DISTINCT TBLTIPOLOGIASPLAN.INTCODIGO,STRDESCRIPCION FROM TBLPLANES INNER JOIN TBLTIPOLOGIASPLAN_PLAN ON TBLTIPOLOGIASPLAN_PLAN.INTCODPLAN=TBLPLANES.INTCODIGO INNER JOIN TBLTIPOLOGIASPLAN ON TBLTIPOLOGIASPLAN.INTCODIGO=TBLTIPOLOGIASPLAN_PLAN.INTCODTIPOLOGIAPLAN INNER JOIN TBLTIPOLOGIASPLANIDIOMA ON TBLTIPOLOGIASPLAN.INTCODIGO=TBLTIPOLOGIASPLANIDIOMA.INTCODIGO WHERE TBLPLANES.BITACTIVO=1";
            DataSet dsDatosConsulta = dsConsulta.Select(strConsulta);
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
        public DataTable ConsultarPaisesCiudad(string idioma, string idCiudad)
        {

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            string strConsulta = "SELECT TBLPAISIDIOMA.INTCODIGOPAIS,TBLPAISIDIOMA.STRDESCRIPCION FROM TBLCIUDADESPLANES INNER JOIN TBLDEPARTAMENTO ON TBLCIUDADESPLANES.INTCODIGODEPARTAMENTO=TBLDEPARTAMENTO.INTDEPARTAMENTO INNER JOIN TBLPAIS ON TBLDEPARTAMENTO.INTPAISID=TBLPAIS.INTCODE INNER JOIN TBLPAISIDIOMA ON TBLPAIS.INTCODE=TBLPAISIDIOMA.INTCODIGOPAIS WHERE TBLCIUDADESPLANES.INTCODE='" + idCiudad + "' AND TBLPAISIDIOMA.STRIDIOMA='" + idioma + "'";

            DataSet dsDatosConsulta = dsConsulta.Select(strConsulta);
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
        /// genera la consulta de planes
        /// </summary>
        /// <param name="Proyecto">Tutiquete</param>
        /// <param name="Aplicacion">17084</param>
        /// <param name="Idioma">es</param>
        /// <returns>datatable</returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-14
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable ConsultaResultadoPlanes(
          string idReferePais,
          string[] referesTipoPlan,
          Enum_TipoDestino destino,
          string sClasificacionPlan,
          string idRefereCiudad,
          string sFechaViaje,
          string Empresa,
          string FiltroTexto = null)
        {
            string sIdioma = clsSesiones.getIdioma();

            string[] ParametrosSp = new string[8];

            if (referesTipoPlan != null && referesTipoPlan.Length != 0)
            {
                if (referesTipoPlan.Length == 1)
                    ParametrosSp[0] = referesTipoPlan[0];
                else
                    ParametrosSp[0] = "0";
            }
            else
            {
                ParametrosSp[0] = "0";
            }

            ParametrosSp[1] = sIdioma;
            ParametrosSp[2] = clsValidaciones.ObtenerUrlPlanes();
            if (idReferePais != "" && idReferePais != null)
                ParametrosSp[3] = idReferePais;
            else
                ParametrosSp[3] = "0";

            if (idRefereCiudad != "" && idRefereCiudad != null)
                ParametrosSp[4] = idRefereCiudad;
            else
                ParametrosSp[4] = "0";

            if (sClasificacionPlan != "" && sClasificacionPlan != null)
                ParametrosSp[5] = sClasificacionPlan;
            else
                ParametrosSp[5] = "0";

            if (sFechaViaje != "" && sFechaViaje != null)
                ParametrosSp[6] = clsValidaciones.ConverFecha(sFechaViaje, sFormatoFecha, sFormatoFechaBD);
            else
                ParametrosSp[6] = "0";

            if (Empresa != "" && Empresa != null)
                ParametrosSp[7] = Empresa;
            else
                ParametrosSp[7] = "0";

            if (FiltroTexto != "" && FiltroTexto != null)
                ParametrosSp[8] = FiltroTexto;
            else
                ParametrosSp[8] = "0";

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsResultado = dsConsulta.Select("SPConsultaResultadosPlanes", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0];
            }
            else
                return null;
        }

        /// <summary>
        /// /jarevalo crucero
        /// </summary>
        /// <param name="idReferePais"></param>
        /// <param name="referesTipoPlan"></param>
        /// <param name="destino"></param>
        /// <param name="sClasificacionPlan"></param>
        /// <param name="idRefereCiudad"></param>
        /// <param name="sFechaViaje"></param>
        /// <param name="Empresa"></param>
        /// <param name="sTipologia"></param>
        /// <param name="sZona"></param>
        /// <returns></returns>
        /// 
        public DataTable ConsultaResultadoPlanesCrucero(
         string idReferePais,
         string[] referesTipoPlan,
         Enum_TipoDestino destino,
         string sClasificacionPlan,
         string idRefereCiudad,
         string sFechaViaje,
         string Empresa,
         string clasificacion,
         string Categoria,
         string sTipologia,
         string sZona)
        {
            string sIdioma = clsSesiones.getIdioma();

            string[] ParametrosSp = new string[11];

            if (referesTipoPlan != null && referesTipoPlan.Length != 0)
            {
                if (referesTipoPlan.Length == 1)
                    ParametrosSp[0] = referesTipoPlan[0];
                else
                    ParametrosSp[0] = "0";
            }
            else
            {
                ParametrosSp[0] = "0";
            }

            ParametrosSp[1] = sIdioma;
            ParametrosSp[2] = clsValidaciones.ObtenerUrlPlanes();
            if (idReferePais != "" && idReferePais != null)
                ParametrosSp[3] = idReferePais;
            else
                ParametrosSp[3] = "0";

            if (idRefereCiudad != "" && idRefereCiudad != null)
                ParametrosSp[4] = idRefereCiudad;
            else
                ParametrosSp[4] = "0";

            if (sClasificacionPlan != "" && sClasificacionPlan != null)
                ParametrosSp[5] = sClasificacionPlan;
            else
                ParametrosSp[5] = "0";

            if (sFechaViaje != "" && sFechaViaje != null)
                ParametrosSp[6] = clsValidaciones.ConverFecha(sFechaViaje, sFormatoFecha, sFormatoFechaBD);
            else
                ParametrosSp[6] = "0";

            if (Empresa != "" && Empresa != null)
                ParametrosSp[7] = Empresa;
            else
                ParametrosSp[7] = "0";

            if (Categoria != "" && Categoria != null)
                ParametrosSp[8] = Categoria;
            else
                ParametrosSp[8] = "0";

            if (sTipologia != "" && sTipologia != null)
                ParametrosSp[9] = sTipologia;
            else
                ParametrosSp[9] = "0";

            if (sZona != "" && sZona != null)
                ParametrosSp[10] = sZona;
            else
                ParametrosSp[10] = "0";


            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsResultado = dsConsulta.Select("SPConsultaResultadosCruceros", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0];
            }
            else
                return null;
        }

        public DataTable ConsultaResultadoPlanes(
         string idReferePais,
         string[] referesTipoPlan,
         Enum_TipoDestino destino,
         string sClasificacionPlan,
         string idRefereCiudad,
         string sFechaViaje,
         string Empresa,
         string sTipologia,
         string sZona,
         string filtroTexto = null
            )
        {
            string sIdioma = clsSesiones.getIdioma();

            string[] ParametrosSp = new string[12];

            if (referesTipoPlan != null && referesTipoPlan.Length != 0)
            {
                if (referesTipoPlan.Length == 1)
                    ParametrosSp[0] = referesTipoPlan[0];
                else
                    ParametrosSp[0] = "0";
            }
            else
            {
                ParametrosSp[0] = "0";
            }

            ParametrosSp[1] = sIdioma;
            ParametrosSp[2] = clsValidaciones.ObtenerUrlPlanes();
            if (idReferePais != "" && idReferePais != null)
                ParametrosSp[3] = idReferePais;
            else
                ParametrosSp[3] = "0";

            if (idRefereCiudad != "" && idRefereCiudad != null)
                ParametrosSp[4] = idRefereCiudad;
            else
                ParametrosSp[4] = "0";

            if (sClasificacionPlan != "" && sClasificacionPlan != null)
                ParametrosSp[5] = sClasificacionPlan;
            else
                ParametrosSp[5] = "0";

            if (sFechaViaje != "" && sFechaViaje != null)
                ParametrosSp[6] = clsValidaciones.ConverFecha(sFechaViaje, sFormatoFecha, sFormatoFechaBD);
            else
                ParametrosSp[6] = "0";

            if (Empresa != "" && Empresa != null)
                ParametrosSp[7] = Empresa;
            else
                ParametrosSp[7] = "0";

            if (sTipologia != "" && sTipologia != null)
                ParametrosSp[8] = sTipologia;
            else
                ParametrosSp[8] = "0";

            if (sZona != "" && sZona != null)
                ParametrosSp[9] = sZona;
            else
                ParametrosSp[9] = "0";

            if (filtroTexto != "" && filtroTexto != null)
                ParametrosSp[10] = filtroTexto;
            else
                ParametrosSp[10] = "0";

            //tipo de destino
            switch (destino)
            {
                case Enum_TipoDestino.Internacional:
                    ParametrosSp[11] = "1";
                    break;
                case Enum_TipoDestino.Nacional:
                    ParametrosSp[11] = "2";
                    break;
                case Enum_TipoDestino.Todos:
                    ParametrosSp[11] = "0";
                    break;
                case Enum_TipoDestino.Vacio:
                    ParametrosSp[11] = "0";
                    break;
                default:
                    ParametrosSp[11] = "0";
                    break;
            }



            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsResultado = dsConsulta.Select("SPConsultaResultadosPlanesTipologia", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0];
            }
            else
                return null;
        }

        /// <summary>
        /// Metodo que consulta los textos e imagenes de un plan determinado
        /// </summary>
        /// <param name="sCodPais"></param>
        /// <param name="bConsultarImagenes"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-20
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable ConsultaTextosImagenesPlanes(string sCodPlan, bool bConsultarImagenes)
        {
            string sIdioma = clsSesiones.getIdioma();

            string[] ParametrosSp = new string[3];

            ParametrosSp[0] = sCodPlan;
            ParametrosSp[1] = sIdioma;

            if (bConsultarImagenes)
                ParametrosSp[2] = "1";
            else
                ParametrosSp[2] = "0";

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsResultado = dsConsulta.Select("SPConsultaAdicionalesPlan", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0];
            }
            else
                return null;
        }

        /// <summary>
        /// metodo general de consulta del detalle de plan
        /// </summary>
        /// <param name="sCodPlan"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-21
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable ConsultaPlan(string sCodPlan)
        {
            string sIdioma = clsSesiones.getIdioma();

            string[] ParametrosSp = new string[3];

            ParametrosSp[0] = sIdioma;
            ParametrosSp[1] = clsValidaciones.ObtenerUrlPlanes();
            ParametrosSp[2] = sCodPlan;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsResultado = dsConsulta.Select("SPConsultaDetallePlan", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0];
            }
            else
                return null;
        }

        /// <summary>
        /// metodo que consulta los años de las vigencias para llenar los combos de salidas en los planes
        /// </summary>
        /// <param name="CodigoPlan"></param>
        /// <param name="FechaConsulta"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-21
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable ConsultaAniosVigencias(string CodigoPlan, string FechaConsulta)
        {
            string[] ParametrosSp = new string[2];

            ParametrosSp[0] = CodigoPlan;
            ParametrosSp[1] = FechaConsulta;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsResultado = dsConsulta.Select("SPConsultaAniosVigencias", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0];
            }
            else
                return null;
        }

        /// <summary>
        /// metodo que consulta las categorias de un plan completo o de una temporada particular
        /// </summary>
        /// <param name="strCodigoPlan"></param>
        /// <param name="stridTemporada"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-21
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataSet ConsultaCategorias(string strCodigoPlan, string stridTemporada, string sTipoPlan)
        {
            string sIdioma = clsSesiones.getIdioma();
            string[] ParametrosSp = new string[3];

            ParametrosSp[0] = strCodigoPlan;
            ParametrosSp[1] = stridTemporada;
            ParametrosSp[2] = sIdioma;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsData = dsConsulta.Select("SPConsultaCategoriasPlan", ParametrosSp);
            if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                if (sTipoPlan != null && sTipoPlan != "")
                {
                    #region Validacion Tabla Hoteles
                    /*------------------- PENDIENTE REVISAR CUANDO SEA NECESARIO UTILIZARLO -------------------*/
                    //DataTable tblDatosConsulta = dsData.Tables[0];
                    //dsData.Tables.Add(tblDatosConsulta.Copy());
                    //dsData.Tables[0].TableName = "tblCategorias";
                    //if (sTipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")))
                    //{
                    //    DataTable tblHotelesComp = new DataTable();
                    //    int iCon = 0;
                    //    while (tblDatosConsulta.Rows.Count > iCon)
                    //    {
                    //        DataTable tblHoteles = this.CosultarHoteles(strCodigoPlan.ToString(), tblDatosConsulta.Rows[iCon]["intIdCategoria"].ToString(), sAplicacion, sProducto, sIdioma);
                    //        if (tblHoteles != null && tblHoteles.Rows.Count > 0)
                    //        {
                    //            if (iCon == 0)
                    //            {
                    //                tblHotelesComp = tblHoteles.Clone();
                    //            }
                    //            for (int i = 0; i < tblHoteles.Rows.Count; i++)
                    //            {
                    //                tblHotelesComp.Rows.Add(tblHoteles.Rows[i].ItemArray);
                    //            }
                    //        }
                    //        iCon++;
                    //    }
                    //    if (tblHotelesComp.Rows.Count > 0)
                    //    {
                    //        dsData.Tables.Add(tblHotelesComp.Copy());
                    //        dsData.Tables[1].TableName = "tblHoteles";
                    //        return dsData;
                    //    }
                    //    else
                    //    {
                    //        return null;
                    //    }
                    //}
                    #endregion
                }
                else
                {

                }
                return dsData;
            }
            else
                return null;
        }

        /// <summary>
        /// Metodo que consulta las referencias de monedas
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-21
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataSet ConReferenciaMonedas()
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian
            string sSelect = "SELECT *";
            string sFrom = "FROM   tblMonedas LEFT JOIN tblMonedasIdioma ON  tblMonedas.intCode = dbo.tblMonedasIdioma.intCodMoneda";
            string sWhere = "WHERE tblMonedasIdioma.strIdioma = '" + sIdioma + "'";

            string[] ParametrosSp = new string[4];
            ParametrosSp[0] = sSelect;
            ParametrosSp[1] = sFrom;
            ParametrosSp[2] = sWhere;
            ParametrosSp[3] = "";

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaGeneralReferencias", ParametrosSp);
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
        /// Metodo que consulta las vigencias de un plan para llenar las salidas del mismo
        /// </summary>
        /// <param name="strTipoCategoria"></param>
        /// <param name="sCodPlan"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-27
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable ConsultaVigencias(string strTipoCategoria, string sCodPlan)
        {
            string[] ParametrosSp = new string[2];

            ParametrosSp[0] = sCodPlan;
            ParametrosSp[1] = strTipoCategoria;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsResultado = dsConsulta.Select("SPConsultaVigenciasPlan", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0];
            }
            else
                return null;
        }

        /// <summary>
        /// Metodo que consulta las tarifas de un plan utilizando los filtrosa proporcionados con el fin de validar que el mismo tenga disponibilidad
        /// </summary>
        /// <param name="intCodigoPadre"></param>
        /// <param name="NumeroPax"></param>
        /// <param name="sFechaInicio"></param>
        /// <param name="sFechaFin"></param>
        /// <param name="idAplicacion"></param>
        /// <param name="sDuracion"></param>
        /// <param name="bCategoria"></param>
        /// <param name="NumeroAdt"></param>
        /// <param name="NumeroCnn"></param>
        /// <param name="sCategoriaHotel"></param>
        /// <param name="sSubTipoPlan"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-27
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable ConsultaTarifasCotizador(int intCodigoPadre, string NumeroPax, string sFechaInicio, string sFechaFin,
            string sDuracion, bool bCategoria, string NumeroAdt, string NumeroCnn, string sCategoriaHotel, string sSubTipoPlan)
        {
            string sIdioma = clsSesiones.getIdioma();
            string[] ParametrosSp = new string[12];

            ParametrosSp[0] = sIdioma;

            if (NumeroCnn != "" && NumeroCnn != null)
                ParametrosSp[2] = NumeroCnn.ToString();
            else
                ParametrosSp[2] = "0";

            //if (bCategoria)
            //{
            //    ParametrosSp[1] = "";
            //    ParametrosSp[3] = intCodigoPadre.ToString(); ;
            //}
            //else
            //{
            ParametrosSp[1] = intCodigoPadre.ToString(); ;
            ParametrosSp[3] = "0";
            //}

            if (sDuracion != null && sDuracion != "" && sDuracion != "0")
                ParametrosSp[4] = sDuracion;
            else
                ParametrosSp[4] = "0";

            if (sCategoriaHotel != null && sCategoriaHotel != "" && sCategoriaHotel != "0")
                ParametrosSp[5] = sCategoriaHotel;
            else
                ParametrosSp[5] = "0";

            if (sSubTipoPlan != null && sSubTipoPlan != "" && sSubTipoPlan != "0")
                ParametrosSp[6] = sSubTipoPlan;
            else
                ParametrosSp[6] = "0";

            if (sFechaInicio != null && sFechaInicio != "")
            {
                if (sFechaFin != null && sFechaFin != "" && sFechaFin != "0")
                    ParametrosSp[7] = sFechaFin;
                else
                    ParametrosSp[7] = "";

                ParametrosSp[8] = NumeroPax;
                ParametrosSp[9] = NumeroAdt;
                ParametrosSp[10] = sFechaInicio;
                if (bCategoria)
                    ParametrosSp[11] = "1";
                else
                    ParametrosSp[11] = "0";
            }
            else
            {
                ParametrosSp[7] = "";
                ParametrosSp[8] = "";
                ParametrosSp[9] = "";
                ParametrosSp[10] = "";
                if (!bCategoria)
                    ParametrosSp[11] = "0";
                else
                    ParametrosSp[11] = "1";
            }

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsResultado = dsConsulta.Select("SPConsultaTarifasCotizador", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0];
            }
            else
                return null;
        }
        public DataTable ConsultaTarifasCotizadorSeguros(int intCodigoPadre, string NumeroPax, string sFechaInicio, string sFechaFin,
           string sDuracion)
        {
            string sIdioma = clsSesiones.getIdioma();
            string[] ParametrosSp = new string[6];

            ParametrosSp[0] = sIdioma;
            ParametrosSp[1] = intCodigoPadre.ToString();

            if (sDuracion != null && sDuracion != "" && sDuracion != "0")
                ParametrosSp[2] = sDuracion;
            else
                ParametrosSp[2] = "0";

            if (sFechaFin != null && sFechaFin != "" && sFechaFin != "0")
                ParametrosSp[3] = sFechaFin;
            else
                ParametrosSp[3] = "0";

            ParametrosSp[4] = NumeroPax;


            if (sFechaFin != null && sFechaFin != "" && sFechaFin != "0")
                ParametrosSp[5] = sFechaInicio;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsResultado = dsConsulta.Select("SPConsultaTarifasCotizadorseguro", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0];
            }
            else
                return null;
        }
        /// <summary>
        /// Metodo que consulta los hoteles pertenecientes a una determinada categoria
        /// </summary>
        /// <param name="strCategoria"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable CosultarHoteles(string strCategoria)
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian

            string[] ParametrosSp = new string[3];
            ParametrosSp[0] = sIdioma;
            ParametrosSp[1] = strCategoria;
            ParametrosSp[2] = clsValidaciones.GetKeyOrAdd("TipoOperHotel", "TPOPHOT");

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("CosultarHoteles", ParametrosSp);
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
        /// Metodo que consulta los impuestos de una determinada tarifa
        /// </summary>
        /// <param name="sTarifa"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable ConsultarImpuestosTarifa(string sTarifa)
        {
            string sIdioma = clsSesiones.getIdioma();

            string[] ParametrosSp = new string[2];
            ParametrosSp[0] = sIdioma;
            ParametrosSp[1] = sTarifa;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPCosultarImpuestosTarirfa", ParametrosSp);
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
        /// Metodo de consulta de cargos sobre impuestos (Pendiente)
        /// </summary>
        /// <param name="sImpuesto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable ConsultarCargosImpuesto(string sImpuesto)
        {
            //string sIdioma = clsSesiones.getIdioma();
            //StringBuilder Consulta = new StringBuilder();

            //Consulta.AppendLine(" SELECT tblCargosImpuestos.*, tblrefere.strRefere, tblrefere.strDetalle,  tblrefere.intProxNivel ");
            //Consulta.AppendLine(" FROM tblCargosImpuestos INNER JOIN tblrefere ON tblCargosImpuestos.intCargo = tblrefere.intidRefere ");
            //Consulta.AppendLine(" WHERE tblCargosImpuestos.intIdImpuesto=" + sImpuesto + " ");
            //Consulta.AppendLine(" AND tblrefere.strIdioma='" + sIdioma + "' AND tblrefere.intAplicacion=" + sAplicacion + " ");

            //DataSet pdtsGrid = new DataSet();
            //pdtsGrid = Generales.ConsultaTabla(Consulta.ToString());
            //if (pdtsGrid != null && pdtsGrid.Tables[0].Rows.Count > 0 && pdtsGrid.Tables.Count > 0)
            //{
            //    if (pdtsGrid.Tables[0].Rows.Count > 0)
            //        return pdtsGrid.Tables[0];
            //    else
            //        return null;
            //}
            //else
            //{
            return null;
            //}
        }

        /// <summary>
        /// Metodo que consulta la referencia de categorias de planes usando el metodo general que genera la consulta
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-19
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataSet ConReferenciaCategoriasPlanes()
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian

            string sSelect = "SELECT * ";
            string sFrom = "FROM tblCategoriasPlan INNER JOIN tblCategoriasPlanIdioma ON tblCategoriasPlan.intCodigo = tblCategoriasPlanIdioma.intCodCategoriaPlan ";
            string sWhere = "WHERE strIdioma = '" + sIdioma + "' ";
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
                return dsDatosConsulta;
            }
            else
            {
                //si no hay resultados se retorna null
                return null;
            }
        }

        /// <summary>
        /// Metodo que consulta la referencia de tipologias de planes usando el metodo general que genera la consulta
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-23
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataSet ConReferenciaTipologiasPlanes()
        {
            string sIdioma = clsSesiones.getIdioma();
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian

            string sSelect = "SELECT * ";
            string sFrom = "FROM tblTipologiasPlan INNER JOIN tblTipologiasPlanIdioma ON tblTipologiasPlan.intCodigo = tblTipologiasPlanIdioma.intCodigo ";
            string sWhere = "WHERE strIdioma = '" + sIdioma + "' ";
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
                return dsDatosConsulta;
            }
            else
            {
                //si no hay resultados se retorna null
                return null;
            }
        }

        /// <summary>
        /// Metodo que consulta el codigo y consecutivo para generacion del codigo de reserva de planes
        /// </summary>
        /// <param name="sTipoPlan"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-09
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable ConsultaCodReservaPlan(string sTipoPlan)
        {
            string[] ParametrosSp = new string[1];
            ParametrosSp[0] = sTipoPlan;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento

            DataSet dsResultado = dsConsulta.Select("SPConsultarCodigoReservaPlan", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0];
            }
            else
                return null;
        }

        /// <summary>
        /// Metodo que los meses o los dias para llenar el buscador de salidas
        /// </summary>
        /// <param name="sTipoPlan"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-03-19
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable ConsultaMesesDiasSalidas(string sCodPlan, string sAnio, string sMes)
        {
            string[] ParametrosSp = new string[3];
            ParametrosSp[0] = sCodPlan;
            ParametrosSp[1] = sAnio;
            ParametrosSp[2] = sMes;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento

            DataSet dsResultado = dsConsulta.Select("SPConsultaMesesDiasSalidas", ParametrosSp);
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0];
            }
            else
                return null;
        }

        /// <summary>
        /// Metodo que consulta la referencia de ciudades (multiciudad) de un plan especifico
        /// </summary>
        /// <param name="sReferePais"></param>
        /// <param name="sIdioma"></param>
        /// <returns></returns>
        ///  /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-04-14
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public DataTable ConMultiCiudadesPlan(string sIdPlan)
        {
            //se arman los parametros para el procedimiento, en este caso seran la propia consulta ya que los joins y nombres de tablas cambian
            string sIdioma = clsSesiones.getIdioma();

            string[] ParametrosSp = new string[2];
            ParametrosSp[0] = sIdPlan;
            ParametrosSp[1] = sIdioma;

            DataSql dsConsulta = new DataSql();
            //se ejecuta el procedimiento
            DataSet dsDatosConsulta = dsConsulta.Select("SPConsultaCiudadesPlan", ParametrosSp);
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
