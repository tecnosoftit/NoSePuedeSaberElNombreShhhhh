using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Ssoft.Utils;
using System.Web.UI.WebControls;
using Ssoft.ManejadorExcepciones;
using System.Data;
using SsoftQuery.Planes;

namespace Ssoft.Rules.Planes
{
    public class csReglasPlanes
    {
        private csConsultasPlanes cConsPlanes = new csConsultasPlanes();
        /// <summary>
        /// Metodo que consulta los planes dependiendo de las validaciones aplicadas
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="eTipoDestino"></param>
        /// <param name="bIncluyeExcursiones"></param>
        /// <param name="sClasificacionPlan"></param>
        /// <param name="iPage"></param>
        /// <param name="sTamanioPagina"></param>
        /// <param name="bRandom"></param>
        /// <param name="sOrdenamiento"></param>
        /// <param name="bControlCupos"></param>
        /// <param name="bPlanesRelacionados"></param>
        /// <param name="bBuscador"></param>
        /// <param name="sTipoPlanUnico"></param> 
        /// <param name="datosAdicionales"></param>
        /// <remarks>        
        /// Company:        Ssoft Colombia
        /// Autor:         Juan Camilo Diaz 
        /// Fecha:          2013-11-14
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public DataTable ReglasConsultaPlanes(UserControl PageSource, Enum_TipoDestino eTipoDestino, bool bIncluyeExcursiones, string sClasificacionPlan,
            bool bControlCupos, bool bBuscador, string sTipoPlanUnico)
        {
            clsCache cCache = new csCache().cCache();
            clsParametros cParametros = new clsParametros();
            DataTable dtPLanesOfer = null;
            /*LLENAMOS LOS COMBOS DE LOS FILTROS*/
            try
            {
                String cadenaConexion = clsValidaciones.GetKeyOrAdd("strConexion");
                #region [Validaciones]
                string sPais = null;
                string sCiudad = null;
                string sFechaConsulta = null;
                string sFiltroTexto = null;
                //agrego filtro por texto
                if (PageSource.Request.QueryString["FiltroTexto"] != "0")
                    sFiltroTexto = PageSource.Request.QueryString["FiltroTexto"];



                #region Validacion tipo de destino
                if (eTipoDestino != Enum_TipoDestino.Todos)
                {
                    string paisLocal = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");
                    DataTable dtData = cConsPlanes.ConReferenciaPaisesId(paisLocal);

                    if (dtData != null)
                        sPais = dtData.Rows[0]["intCodigoPais"].ToString();

                    if (eTipoDestino == Enum_TipoDestino.Vacio)
                    {
                        if (PageSource.Request.QueryString["TIPODESTINO"].ToString() != "0")
                        {
                            if (PageSource.Request.QueryString["TIPODESTINO"].ToString() == "NACIONAL")
                            {
                                eTipoDestino = Enum_TipoDestino.Nacional;
                            }
                            else if (PageSource.Request.QueryString["TIPODESTINO"].ToString() == "INTERNACIONAL")
                            {
                                eTipoDestino = Enum_TipoDestino.Internacional;
                            }
                            else
                            {
                                eTipoDestino = Enum_TipoDestino.Todos;
                            }
                        }
                        if (PageSource.Request.QueryString["Clasif"] != null)
                        {
                            sClasificacionPlan = PageSource.Request.QueryString["Clasif"];
                        }
                        if (PageSource.Request.QueryString["bExc"] != null)
                        {
                            if (PageSource.Request.QueryString["bExc"] == "F")
                            {
                                bIncluyeExcursiones = false;
                            }
                            else
                            {
                                bIncluyeExcursiones = true;
                            }
                        }
                    }
                }
                else
                {
                    Label lblPaisDestino = (Label)PageSource.FindControl("lblPaisDestino");
                    if (lblPaisDestino != null)
                    {
                        sPais = lblPaisDestino.Text;
                    }
                    else
                    {
                        if (PageSource.Request.QueryString["PaisDestino"] != "0")
                        {
                            sPais = PageSource.Request.QueryString["PaisDestino"];
                        }

                        if (cCache.DatosAdicionales != null && cCache.DatosAdicionales.Count > 9)
                            sCiudad = cCache.DatosAdicionales[9];
                    }
                }
                #endregion

                /*SI VIENE DEL LINK DE OFERTAS*/
                #region Validacion tipos de plan
                string[] sReferesTipoPlan = null;
                try
                {
                    if (cCache.DatosAdicionales.Count > 0 && PageSource.Request.QueryString["TipoPlanDestino"] != "CASA")
                    {
                        if (cCache.DatosAdicionales[5].Length > 0)
                        {
                            string sTipoPlan = "CIRC";
                            sReferesTipoPlan = new string[1];
                            if (cCache.DatosAdicionales[5].ToString().Equals("PAQ"))
                            {
                                sTipoPlan = "CIRC";
                            }
                            else
                            {
                                if (cCache.DatosAdicionales[5].ToString().Equals("TRA"))
                                {
                                    sTipoPlan = "EXC";
                                }
                                else
                                {
                                    if (cCache.DatosAdicionales[5].ToString().Equals("SOU"))
                                    {
                                        sTipoPlan = "SOUV";
                                    }
                                    else
                                    {
                                        if (!bBuscador)
                                        {
                                            sTipoPlan = cCache.DatosAdicionales[5].ToString();
                                            sFechaConsulta = DateTime.Now.ToString(clsValidaciones.GetKeyOrAdd("FormatoFecha"));
                                        }
                                        else
                                        {
                                            sFechaConsulta = cCache.DatosAdicionales[5].ToString();
                                        }
                                    }
                                }
                            }
                            sReferesTipoPlan[0] = sTipoPlan;
                        }
                        else
                        {
                            if (sTipoPlanUnico != null && sTipoPlanUnico != "")
                            {
                                sReferesTipoPlan = new string[1];
                                sReferesTipoPlan[0] = sTipoPlanUnico;
                            }
                            else
                            {
                                if (bIncluyeExcursiones)
                                {
                                    sReferesTipoPlan = new string[4];
                                    sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                                    sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                                    sReferesTipoPlan[2] = clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC");
                                    sReferesTipoPlan[3] = clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS");
                                }
                                else
                                {
                                    sReferesTipoPlan = new string[2];
                                    sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                                    sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PageSource.Request.QueryString["TipoPlanDestino"] != "0" && PageSource.Request.QueryString["TipoPlanDestino"] != null)
                        {
                            sReferesTipoPlan = new string[1];
                            sReferesTipoPlan[0] = PageSource.Request.QueryString["TipoPlanDestino"];
                        }
                        else
                        {
                            if (sTipoPlanUnico != null && sTipoPlanUnico != "")
                            {
                                sReferesTipoPlan = new string[1];
                                sReferesTipoPlan[0] = sTipoPlanUnico;
                            }
                            else
                            {
                                if (bIncluyeExcursiones)
                                {
                                    sReferesTipoPlan = new string[4];
                                    sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                                    sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                                    sReferesTipoPlan[2] = clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC");
                                    sReferesTipoPlan[3] = clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS");
                                }
                                else
                                {
                                    sReferesTipoPlan = new string[2];
                                    sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                                    sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                                }
                            }
                        }
                    }
                }
                catch
                {
                    if (sTipoPlanUnico != null && sTipoPlanUnico != "")
                    {
                        sReferesTipoPlan = new string[1];
                        sReferesTipoPlan[0] = sTipoPlanUnico;
                    }
                    else if (bIncluyeExcursiones)
                    {
                        sReferesTipoPlan = new string[4];
                        sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                        sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                        sReferesTipoPlan[2] = clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC");
                        sReferesTipoPlan[3] = clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS");
                    }
                    else
                    {
                        sReferesTipoPlan = new string[2];
                        sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                        sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                    }
                }
                #endregion

                string[] sReferesControlCupos = null;
                if (bControlCupos)
                {
                    sReferesControlCupos = new string[2];
                    sReferesControlCupos[0] = clsValidaciones.GetKeyOrAdd("ControlCuposCon", "ConControl");
                    sReferesControlCupos[1] = clsValidaciones.GetKeyOrAdd("ControlCuposSin", "SinControl");
                }
                #endregion

                #region [Consulta]
                if (!bBuscador)
                {
                    if (PageSource.Request.QueryString["ORIGEN"] != null)
                    {
                        if (PageSource.Request.QueryString["ORIGEN"].ToUpper().Contains("BUSCADOR"))
                            bBuscador = true;
                    }
                }

                if (bBuscador)
                {
                    if (cCache.DatosAdicionales.Count > 6)
                    {
                        #region Busqueda por presupuesto
                        if (cCache.DatosAdicionales[cCache.DatosAdicionales.Count - 1].ToUpper().Equals("PRESUPUESTO"))
                        {
                            //dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                            //            clsSesiones.getIdioma(),
                            //            clsSesiones.getAplicacion().ToString(),
                            //            cCache.DatosAdicionales[1],
                            //            "1",
                            //            sReferesTipoPlan,
                            //            eTipoDestino,
                            //            sClasificacionPlan,
                            //            cCache.DatosAdicionales[3],
                            //            sReferesControlCupos,
                            //            null,
                            //            null,
                            //            cCache.DatosAdicionales[4],
                            //            cCache.DatosAdicionales[0],
                            //            cCache.DatosAdicionales[2],
                            //            sFechaConsulta,
                            //            cCache.DatosAdicionales[6],
                            //            cCache.DatosAdicionales[7]);
                        }
                        else
                        {
                            //dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                            //          clsSesiones.getIdioma(),
                            //          clsSesiones.getAplicacion().ToString(),
                            //          cCache.DatosAdicionales[1],
                            //          "1",
                            //          sReferesTipoPlan,
                            //          eTipoDestino,
                            //          sClasificacionPlan,
                            //          cCache.DatosAdicionales[3],
                            //          sReferesControlCupos,
                            //          null,
                            //          null,
                            //          cCache.DatosAdicionales[4],
                            //          cCache.DatosAdicionales[0],
                            //          cCache.DatosAdicionales[2],
                            //          cCache.DatosAdicionales[6]);
                        }
                        #endregion
                    }
                    else
                    {
                        if (cCache.DatosAdicionales.Count > 0)
                        {
                            string sZona = "";
                            string sTipologia = "";
                            if (cCache.DatosAdicionales[0] == "" || cCache.DatosAdicionales[0] == "0")
                            {
                                if (PageSource.Request.QueryString["ZonaGeo"] != "0")
                                    sZona = PageSource.Request.QueryString["ZonaGeo"];
                            }
                            else
                            {
                                sZona = cCache.DatosAdicionales[0];
                            }
                            if (cCache.DatosAdicionales[3] != "" && cCache.DatosAdicionales[3] != "0")
                            {
                                sTipologia = cCache.DatosAdicionales[3];
                            }

                            if (sTipologia != "" || sZona != "")
                            {
                                if (sTipoPlanUnico != null && sTipoPlanUnico != "" && sTipoPlanUnico == "TJAS")
                                {
                                    dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                                            "0",
                                            sReferesTipoPlan,
                                            eTipoDestino,
                                            sClasificacionPlan,
                                            "0",
                                            cCache.DatosAdicionales[1],
                                            cCache.Empresa
                                            , "0", sZona);
                                }
                                else
                                {
                                    dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                                            cCache.DatosAdicionales[1],
                                            sReferesTipoPlan,
                                            eTipoDestino,
                                            sClasificacionPlan,
                                            cCache.DatosAdicionales[2],
                                            sFechaConsulta,
                                            cCache.Empresa
                                            , sTipologia, sZona, filtroTexto: sFiltroTexto);
                                }
                            }
                            else
                            {
                                dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                                          cCache.DatosAdicionales[1],
                                          sReferesTipoPlan,
                                          eTipoDestino,
                                          sClasificacionPlan,
                                          cCache.DatosAdicionales[2],
                                          sFechaConsulta,
                                          cCache.Empresa, null, null, filtroTexto: sFiltroTexto);
                            }

                        }
                        else
                        {
                            string sZona = "";
                            if (PageSource.Request.QueryString["ZonaGeo"] != "0")
                                sZona = PageSource.Request.QueryString["ZonaGeo"];

                            if (PageSource.Request.QueryString["IdPais"] == "0")
                            {
                                sPais = null;
                            }
                            else
                            {
                                sPais = PageSource.Request.QueryString["IdPais"];
                            }

                            if (PageSource.Request.QueryString["IdCiudad"] != "0")
                                sCiudad = PageSource.Request.QueryString["IdCiudad"];

                            string sTipologia = null;
                            if (PageSource.Request.QueryString["IdTipologia"] != "0")
                                sTipologia = PageSource.Request.QueryString["IdTipologia"];



                            dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                                      sPais,
                                      sReferesTipoPlan,
                                      eTipoDestino,
                                      sClasificacionPlan,
                                      sCiudad,
                                      sFechaConsulta,
                                      cCache.Empresa, sTipologia, sZona,
                                      filtroTexto: sFiltroTexto
                                      );
                        }
                    }
                }
                else if (PageSource.Request.QueryString["PaisDestino"] != "0" && PageSource.Request.QueryString["PaisDestino"] != null)
                {
                    sPais = PageSource.Request.QueryString["PaisDestino"].ToString();
                    if (sPais != "")
                    {
                        if (bIncluyeExcursiones)
                        {
                            sReferesTipoPlan = new string[4];
                            sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                            sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                            sReferesTipoPlan[2] = clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC");
                            sReferesTipoPlan[3] = clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS");
                        }
                        else
                        {
                            sReferesTipoPlan = new string[2];
                            sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                            sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                        }

                        dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                                         sPais,
                                         sReferesTipoPlan,
                                         eTipoDestino,
                                         sClasificacionPlan,
                                         sCiudad,
                                         sFechaConsulta,
                                         cCache.Empresa);

                    }

                }
                else if (PageSource.Request.QueryString["ICiudad"] != null)
                {
                    sCiudad = PageSource.Request.QueryString["ICiudad"].ToString();
                    if (sCiudad != "")
                    {
                        if (bIncluyeExcursiones)
                        {
                            sReferesTipoPlan = new string[4];
                            sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                            sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                            sReferesTipoPlan[2] = clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC");
                            sReferesTipoPlan[3] = clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS");
                        }
                        else
                        {
                            sReferesTipoPlan = new string[2];
                            sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                            sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                        }

                        dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                                         sPais,
                                         sReferesTipoPlan,
                                         eTipoDestino,
                                         sClasificacionPlan,
                                         sCiudad,
                                         sFechaConsulta,
                                         cCache.Empresa);

                    }

                }

                if (dtPLanesOfer != null && dtPLanesOfer.Rows.Count == 0)
                    dtPLanesOfer = null;
                #endregion
            }
            catch (Exception Ex)
            {
                dtPLanesOfer = null;
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "CargarPlanes";
                ExceptionHandled.Publicar(cParametros);
            }
            return dtPLanesOfer;
        }

        ///crucero
        ///
        /// </remarks>
        public DataTable ReglasConsultaPlanesCrucero(UserControl PageSource, Enum_TipoDestino eTipoDestino, bool bIncluyeExcursiones, string sClasificacionPlan,
            bool bControlCupos, bool bBuscador, string sTipoPlanUnico, string scategoria, string sTipologia)
        {
            clsCache cCache = new csCache().cCache();
            clsParametros cParametros = new clsParametros();
            DataTable dtPLanesOfer = null;
            /*LLENAMOS LOS COMBOS DE LOS FILTROS*/
            try
            {
                String cadenaConexion = clsValidaciones.GetKeyOrAdd("strConexion");
                #region [Validaciones]
                string sPais = null;
                string sCiudad = null;
                string sFechaConsulta = null;
                #region Validacion tipo de destino
                if (eTipoDestino != Enum_TipoDestino.Todos)
                {
                    DataTable dtData = cConsPlanes.ConReferenciaPaisesId(clsValidaciones.GetKeyOrAdd("PaisDefault", "COL"));
                    if (dtData != null)
                        sPais = dtData.Rows[0]["strDescripcion"].ToString();

                    if (eTipoDestino == Enum_TipoDestino.Vacio)
                    {
                        if (PageSource.Request.QueryString["TIPODESTINO"].ToString() != "0")
                        {
                            if (PageSource.Request.QueryString["TIPODESTINO"].ToString() == "NACIONAL")
                            {
                                eTipoDestino = Enum_TipoDestino.Nacional;
                            }
                            else if (PageSource.Request.QueryString["TIPODESTINO"].ToString() == "INTERNACIONAL")
                            {
                                eTipoDestino = Enum_TipoDestino.Internacional;
                            }
                            else
                            {
                                eTipoDestino = Enum_TipoDestino.Todos;
                            }
                        }
                        if (PageSource.Request.QueryString["Clasif"] != null)
                        {
                            sClasificacionPlan = PageSource.Request.QueryString["Clasif"];
                        }
                        if (PageSource.Request.QueryString["bExc"] != null)
                        {
                            if (PageSource.Request.QueryString["bExc"] == "F")
                            {
                                bIncluyeExcursiones = false;
                            }
                            else
                            {
                                bIncluyeExcursiones = true;
                            }
                        }
                    }
                }
                else
                {
                    Label lblPaisDestino = (Label)PageSource.FindControl("lblPaisDestino");
                    if (lblPaisDestino != null)
                    {
                        sPais = lblPaisDestino.Text;
                    }
                    else
                    {
                        if (PageSource.Request.QueryString["PaisDestino"] != "0")
                        {
                            sPais = PageSource.Request.QueryString["PaisDestino"];
                        }

                        if (cCache.DatosAdicionales != null && cCache.DatosAdicionales.Count > 9)
                            sCiudad = cCache.DatosAdicionales[9];
                    }
                }
                #endregion

                /*SI VIENE DEL LINK DE OFERTAS*/
                #region Validacion tipos de plan
                string[] sReferesTipoPlan = null;
                try
                {
                    if (cCache.DatosAdicionales.Count > 0 && PageSource.Request.QueryString["TipoPlanDestino"] != "CASA")
                    {
                        if (cCache.DatosAdicionales[5].Length > 0)
                        {
                            string sTipoPlan = "CIRC";
                            sReferesTipoPlan = new string[1];
                            if (cCache.DatosAdicionales[5].ToString().Equals("PAQ"))
                            {
                                sTipoPlan = "CIRC";
                            }
                            else
                            {
                                if (cCache.DatosAdicionales[5].ToString().Equals("TRA"))
                                {
                                    sTipoPlan = "EXC";
                                }
                                else
                                {
                                    if (cCache.DatosAdicionales[5].ToString().Equals("SOU"))
                                    {
                                        sTipoPlan = "SOUV";
                                    }
                                    else
                                    {
                                        if (!bBuscador)
                                        {
                                            sTipoPlan = cCache.DatosAdicionales[5].ToString();
                                            sFechaConsulta = DateTime.Now.ToString(clsValidaciones.GetKeyOrAdd("FormatoFecha"));
                                        }
                                        else
                                        {
                                            sFechaConsulta = cCache.DatosAdicionales[5].ToString();
                                        }
                                    }
                                }
                            }
                            sReferesTipoPlan[0] = sTipoPlan;
                        }
                        else
                        {
                            if (sTipoPlanUnico != null && sTipoPlanUnico != "")
                            {
                                sReferesTipoPlan = new string[1];
                                sReferesTipoPlan[0] = sTipoPlanUnico;
                            }
                            else
                            {
                                if (bIncluyeExcursiones)
                                {
                                    sReferesTipoPlan = new string[4];
                                    sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                                    sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                                    sReferesTipoPlan[2] = clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC");
                                    sReferesTipoPlan[3] = clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS");
                                }
                                else
                                {
                                    sReferesTipoPlan = new string[2];
                                    sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                                    sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PageSource.Request.QueryString["TipoPlanDestino"] != "0" && PageSource.Request.QueryString["TipoPlanDestino"] != null)
                        {
                            sReferesTipoPlan = new string[1];
                            sReferesTipoPlan[0] = PageSource.Request.QueryString["TipoPlanDestino"];
                        }
                        else
                        {
                            if (sTipoPlanUnico != null && sTipoPlanUnico != "")
                            {
                                sReferesTipoPlan = new string[1];
                                sReferesTipoPlan[0] = sTipoPlanUnico;
                            }
                            else
                            {
                                if (bIncluyeExcursiones)
                                {
                                    sReferesTipoPlan = new string[4];
                                    sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                                    sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                                    sReferesTipoPlan[2] = clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC");
                                    sReferesTipoPlan[3] = clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS");
                                }
                                else
                                {
                                    sReferesTipoPlan = new string[2];
                                    sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                                    sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                                }
                            }
                        }
                    }
                }
                catch
                {
                    if (sTipoPlanUnico != null && sTipoPlanUnico != "")
                    {
                        sReferesTipoPlan = new string[1];
                        sReferesTipoPlan[0] = sTipoPlanUnico;
                    }
                    else if (bIncluyeExcursiones)
                    {
                        sReferesTipoPlan = new string[4];
                        sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                        sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                        sReferesTipoPlan[2] = clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC");
                        sReferesTipoPlan[3] = clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS");
                    }
                    else
                    {
                        sReferesTipoPlan = new string[2];
                        sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                        sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                    }
                }
                #endregion

                string[] sReferesControlCupos = null;
                if (bControlCupos)
                {
                    sReferesControlCupos = new string[2];
                    sReferesControlCupos[0] = clsValidaciones.GetKeyOrAdd("ControlCuposCon", "ConControl");
                    sReferesControlCupos[1] = clsValidaciones.GetKeyOrAdd("ControlCuposSin", "SinControl");
                }
                #endregion

                #region [Consulta]
                if (!bBuscador)
                {
                    if (PageSource.Request.QueryString["ORIGEN"] != null)
                    {
                        if (PageSource.Request.QueryString["ORIGEN"].ToUpper().Contains("BUSCADOR"))
                            bBuscador = true;
                    }
                }

                if (bBuscador)
                {
                    if (cCache.DatosAdicionales.Count > 6)
                    {
                        #region Busqueda por presupuesto
                        if (cCache.DatosAdicionales[cCache.DatosAdicionales.Count - 1].ToUpper().Equals("PRESUPUESTO"))
                        {
                            //dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                            //            clsSesiones.getIdioma(),
                            //            clsSesiones.getAplicacion().ToString(),
                            //            cCache.DatosAdicionales[1],
                            //            "1",
                            //            sReferesTipoPlan,
                            //            eTipoDestino,
                            //            sClasificacionPlan,
                            //            cCache.DatosAdicionales[3],
                            //            sReferesControlCupos,
                            //            null,
                            //            null,
                            //            cCache.DatosAdicionales[4],
                            //            cCache.DatosAdicionales[0],
                            //            cCache.DatosAdicionales[2],
                            //            sFechaConsulta,
                            //            cCache.DatosAdicionales[6],
                            //            cCache.DatosAdicionales[7]);
                        }
                        else
                        {
                            //dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                            //          clsSesiones.getIdioma(),
                            //          clsSesiones.getAplicacion().ToString(),
                            //          cCache.DatosAdicionales[1],
                            //          "1",
                            //          sReferesTipoPlan,
                            //          eTipoDestino,
                            //          sClasificacionPlan,
                            //          cCache.DatosAdicionales[3],
                            //          sReferesControlCupos,
                            //          null,
                            //          null,
                            //          cCache.DatosAdicionales[4],
                            //          cCache.DatosAdicionales[0],
                            //          cCache.DatosAdicionales[2],
                            //          cCache.DatosAdicionales[6]);
                        }
                        #endregion
                    }
                    else
                    {
                        if (cCache.DatosAdicionales.Count > 0)
                        {
                            string sZona = "";
                            //string sTipologia = "";
                            if (cCache.DatosAdicionales[0] == "" || cCache.DatosAdicionales[0] == "0")
                            {
                                if (PageSource.Request.QueryString["ZonaGeo"] != "0")
                                    sZona = PageSource.Request.QueryString["ZonaGeo"];
                            }
                            else
                            {
                                sZona = cCache.DatosAdicionales[0];
                            }
                            if (cCache.DatosAdicionales[3] != "" && cCache.DatosAdicionales[3] != "0")
                            {
                                sTipologia = cCache.DatosAdicionales[3];
                            }

                            if (sTipologia != "" || sZona != "")
                            {
                                if (sTipoPlanUnico != null && sTipoPlanUnico != "" && sTipoPlanUnico == "TJAS")
                                {
                                    dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                                            "0",
                                            sReferesTipoPlan,
                                            eTipoDestino,
                                            sClasificacionPlan,
                                            "0",
                                            cCache.DatosAdicionales[1],
                                            cCache.Empresa
                                            , "0", sZona);
                                }
                                else
                                {
                                    dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                                            cCache.DatosAdicionales[1],
                                            sReferesTipoPlan,
                                            eTipoDestino,
                                            sClasificacionPlan,
                                            cCache.DatosAdicionales[2],
                                            sFechaConsulta,
                                            cCache.Empresa
                                            , sTipologia, sZona);
                                }
                            }
                            else
                            {
                                dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                                          cCache.DatosAdicionales[1],
                                          sReferesTipoPlan,
                                          eTipoDestino,
                                          sClasificacionPlan,
                                          cCache.DatosAdicionales[2],
                                          sFechaConsulta,
                                          cCache.Empresa);
                            }

                        }
                        else
                        {
                            string sZona = "";
                            if (PageSource.Request.QueryString["ZonaGeo"] != "0")
                                sZona = PageSource.Request.QueryString["ZonaGeo"];

                            dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanesCrucero(
                                      sPais,
                                      sReferesTipoPlan,
                                      eTipoDestino,
                                      sClasificacionPlan,
                                      null,
                                      sFechaConsulta,
                                      cCache.Empresa,
                                      null,
                                      scategoria,
                                      sTipologia,
                                      null
                                    );
                        }
                    }
                }
                else if (PageSource.Request.QueryString["PaisDestino"] != "0" && PageSource.Request.QueryString["PaisDestino"] != null)
                {
                    sPais = PageSource.Request.QueryString["PaisDestino"].ToString();
                    if (sPais != "")
                    {
                        if (bIncluyeExcursiones)
                        {
                            sReferesTipoPlan = new string[4];
                            sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                            sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                            sReferesTipoPlan[2] = clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC");
                            sReferesTipoPlan[3] = clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS");
                        }
                        else
                        {
                            sReferesTipoPlan = new string[2];
                            sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                            sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                        }

                        dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                                         sPais,
                                         sReferesTipoPlan,
                                         eTipoDestino,
                                         sClasificacionPlan,
                                         sCiudad,
                                         sFechaConsulta,
                                         cCache.Empresa);

                    }

                }
                else if (PageSource.Request.QueryString["ICiudad"] != null)
                {
                    sCiudad = PageSource.Request.QueryString["ICiudad"].ToString();
                    if (sCiudad != "")
                    {
                        if (bIncluyeExcursiones)
                        {
                            sReferesTipoPlan = new string[4];
                            sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                            sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                            sReferesTipoPlan[2] = clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC");
                            sReferesTipoPlan[3] = clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS");
                        }
                        else
                        {
                            sReferesTipoPlan = new string[2];
                            sReferesTipoPlan[0] = clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC");
                            sReferesTipoPlan[1] = clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE");
                        }

                        dtPLanesOfer = cConsPlanes.ConsultaResultadoPlanes(
                                         sPais,
                                         sReferesTipoPlan,
                                         eTipoDestino,
                                         sClasificacionPlan,
                                         sCiudad,
                                         sFechaConsulta,
                                         cCache.Empresa);

                    }

                }

                if (dtPLanesOfer != null && dtPLanesOfer.Rows.Count == 0)
                    dtPLanesOfer = null;
                #endregion
            }
            catch (Exception Ex)
            {
                dtPLanesOfer = null;
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "CargarPlanes";
                ExceptionHandled.Publicar(cParametros);
            }
            return dtPLanesOfer;
        }
    }
}
