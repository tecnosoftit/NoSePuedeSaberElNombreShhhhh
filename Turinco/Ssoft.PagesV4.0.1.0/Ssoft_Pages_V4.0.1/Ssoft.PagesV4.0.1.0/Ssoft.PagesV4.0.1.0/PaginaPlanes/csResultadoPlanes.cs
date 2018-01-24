using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;
using System.Web.UI.WebControls;
using System.Web.UI;
using SsoftQuery.Planes;
using System.Data;
using Ssoft.Rules.Planes;
using System.Web.UI.HtmlControls;
using System.Web;
using SsoftQuery.Operadores;
using Ssoft.Rules.Generales;

namespace Ssoft.Pages.PaginaPlanes
{
    public class csResultadoPlanes
    {
        csReglasPlanes cReglas = new csReglasPlanes();
        /// <summary>
        /// Metodo general que valida parametros y ejecuta la busqueda de planes segun parametros recibidos
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-14
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public void LlenarDatosPlanes_GeneralCompartidos(UserControl PageSource, int iPage, bool bIncluyeExcursiones)
        {
            csGeneralsPag.Idioma(PageSource);
            clsParametros cParametros = new clsParametros();
            clsSesiones.setPage(iPage.ToString());
            /*LLENAMOS LOS COMBOS DE LOS FILTROS*/
            //if (!PageSource.IsPostBack)
            //    LlenarCombos_FiltroResultadosPlanes(PageSource);
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    Label lblError = (Label)PageSource.FindControl("lblError");
                    DataList dlPlanes = (DataList)PageSource.FindControl("dlPlanes");
                    DataList dtlPaginador = (DataList)PageSource.FindControl("dtlPaginador");
                    Repeater dtlOfertas = (Repeater)PageSource.FindControl("dtlOfertas");

                    //csGenerales Generales = new csGenerales();
                    //Generales.Conexion = clsSesiones.getConexion();
                    Enum_TipoDestino enumTipoDestino = Enum_TipoDestino.Todos;

                    if (PageSource.Request.QueryString["TD"] != null)
                    {
                        if (PageSource.Request.QueryString["TD"].Equals("IN"))
                        {
                            enumTipoDestino = Enum_TipoDestino.Internacional;
                        }
                        else if (PageSource.Request.QueryString["TD"].Equals("NA"))
                        {
                            enumTipoDestino = Enum_TipoDestino.Nacional;
                        }
                    }
                    if (PageSource.Request.QueryString["TIPODESTINO"] != null)
                    {
                        if (PageSource.Request.QueryString["TIPODESTINO"].Equals("INTERNACIONAL"))
                        {
                            enumTipoDestino = Enum_TipoDestino.Internacional;
                        }
                        else if (PageSource.Request.QueryString["TIPODESTINO"].Equals("NACIONAL"))
                        {
                            enumTipoDestino = Enum_TipoDestino.Nacional;
                        }

                    }
                    string sTipoPlan = null;
                    if (PageSource.Request.QueryString["TipoPlan"] != null)
                        sTipoPlan = PageSource.Request.QueryString["TipoPlan"];
                    if (dlPlanes != null || dtlOfertas != null)
                    {
                        try
                        {
                            if (cCache.DatosAdicionales.Count > 4)
                            {
                                if (cCache.DatosAdicionales[4] != "" && cCache.DatosAdicionales[4] != "0")
                                    bIncluyeExcursiones = true;
                            }
                        }
                        catch { }
                        CargarPlanes(PageSource, enumTipoDestino, bIncluyeExcursiones, null, iPage, clsValidaciones.GetKeyOrAdd("NumPlanesPagina", "8"),
                            false, "intOrden DESC, dblPrecio ASC", false, false, true, sTipoPlan);
                    }
                    SeleccionarFiltroBuscador(PageSource);
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "LlenarDatosPlanes_GeneralCompartidos";
                ExceptionHandled.Publicar(cParametros);
            }
        }

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
        public void CargarPlanes(UserControl PageSource, Enum_TipoDestino eTipoDestino, bool bIncluyeExcursiones, string sClasificacionPlan,
            int iPage, string sTamanioPagina, bool bRandom, string sOrdenamiento, bool bControlCupos, bool bPlanesRelacionados, bool bBuscador,
            string sTipoPlanUnico)
        {
            clsParametros cParametros = new clsParametros();
            try
            {

                //Llenamos el filtro de texto
                string filtroTexto = PageSource.Request.QueryString["FiltroTexto"];

                if (!string.IsNullOrWhiteSpace(filtroTexto))
                {
                    TextBox txtFiltroTexto = (TextBox)PageSource.FindControl("txtFiltroTexto");
                    if (txtFiltroTexto != null)
                    {
                        txtFiltroTexto.Text = filtroTexto;
                    }
                }

                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    Label lblError = (Label)PageSource.FindControl("lblError");
                    DataTable dtPLanesOfer = cReglas.ReglasConsultaPlanes(PageSource, eTipoDestino, bIncluyeExcursiones, sClasificacionPlan,
                        bControlCupos, bBuscador, sTipoPlanUnico);

                    #region GUARDAR DATASET
                    if (dtPLanesOfer != null)
                    {
                        if (dtPLanesOfer.DataSet != null)
                        {
                            clsSesiones.setResultadoPlanes(dtPLanesOfer.DataSet);
                        }
                        else
                        {
                            DataSet dsResultadoPlanes = new DataSet();
                            dsResultadoPlanes.Tables.Add(dtPLanesOfer);
                        }
                    }
                    else
                    {
                        if (lblError != null)
                            lblError.Text = "Lo sentimos, no existen resultados para tu busqueda, por favor cambia los parametros";

                        LinkButton lbResultadosDestino = (LinkButton)PageSource.FindControl("lbResultadosDestino");
                        if (lbResultadosDestino != null)
                            lbResultadosDestino.Visible = true;
                    }

                    #endregion
                    #region [Llenado]
                    dtPLanesOfer = LlenarRepetidor(
                        PageSource,
                        iPage,
                        sTamanioPagina,
                        bRandom,
                        sOrdenamiento,
                        cParametros,
                        dtPLanesOfer);

                    #endregion
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "CargarPlanes";
                ExceptionHandled.Publicar(cParametros);
            }
        }


        ////crucero

        public void CargarPlanesCrucero(UserControl PageSource, Enum_TipoDestino eTipoDestino, bool bIncluyeExcursiones, string sClasificacionPlan,
           int iPage, string sTamanioPagina, bool bRandom, string sOrdenamiento, bool bControlCupos, bool bPlanesRelacionados, bool bBuscador,
           string sTipoPlanUnico, string sCategoria, string sTipologia)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    Label lblError = (Label)PageSource.FindControl("lblError");
                    DataTable dtPLanesOfer = cReglas.ReglasConsultaPlanesCrucero(PageSource, eTipoDestino, bIncluyeExcursiones, sClasificacionPlan,
                        bControlCupos, bBuscador, sTipoPlanUnico, sCategoria, sTipologia);

                    #region GUARDAR DATASET
                    if (dtPLanesOfer != null)
                    {
                        if (dtPLanesOfer.DataSet != null)
                        {
                            clsSesiones.setResultadoPlanes(dtPLanesOfer.DataSet);
                        }
                        else
                        {
                            DataSet dsResultadoPlanes = new DataSet();
                            dsResultadoPlanes.Tables.Add(dtPLanesOfer);
                        }
                    }
                    else
                    {
                        if (lblError != null)
                            lblError.Text = "Lo sentimos, no existen resultados para tu busqueda, por favor cambia los parametros";

                        LinkButton lbResultadosDestino = (LinkButton)PageSource.FindControl("lbResultadosDestino");
                        if (lbResultadosDestino != null)
                            lbResultadosDestino.Visible = true;
                    }

                    #endregion
                    #region [Llenado]
                    dtPLanesOfer = LlenarRepetidor(
                        PageSource,
                        iPage,
                        sTamanioPagina,
                        bRandom,
                        sOrdenamiento,
                        cParametros,
                        dtPLanesOfer);

                    #endregion
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "CargarPlanes";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que llena el resumen de parametros de busqueda
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-14
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public void SeleccionarFiltroBuscador(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();

                if (cCache != null)
                {
                    if (cCache.DatosAdicionales != null || cCache.DatosAdicionales.Count > 0)
                    {
                        List<string> listaValores = cCache.DatosAdicionales;

                        Label lblPais = (Label)PageSource.FindControl("lblPais");
                        Label lblCiudad = (Label)PageSource.FindControl("lblCiudad");
                        Label lblFecha = (Label)PageSource.FindControl("lblFecha");

                        csConsultasPlanes cConsPlanes = new csConsultasPlanes();
                        DataTable dtData;

                        if (lblPais != null)
                        {
                            if (listaValores[1] != "" && listaValores[1] != "0")
                            {
                                dtData = cConsPlanes.ConReferenciaPaisesId(listaValores[1]);
                                if (dtData != null)
                                    lblPais.Text = dtData.Rows[0]["strDescripcion"].ToString();
                            }
                            else
                            {
                                lblPais.Text = "Todos";
                            }
                        }

                        if (lblCiudad != null)
                        {
                            if (listaValores.Count > 1)
                            {
                                if (listaValores[2] != "" && listaValores[2] != "0")
                                {
                                    dtData = cConsPlanes.ConReferenciaCiudadesId(listaValores[2]);
                                    if (dtData != null)
                                        lblPais.Text = dtData.Rows[0]["strDescripcion"].ToString();
                                }
                                else
                                {
                                    lblCiudad.Text = "Todas";
                                }
                            }
                        }

                        if (lblFecha != null)
                        {
                            if (listaValores.Count > 6)
                                lblFecha.Text = listaValores[6];
                        }
                    }
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "SeleccionarFiltroBuscador";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que llena el repetidor con los resultados de planes
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="iPage"></param>
        /// <param name="sTamanioPagina"></param>
        /// <param name="bRandom"></param>
        /// <param name="sOrdenamiento"></param>
        /// <param name="cParametros"></param>
        /// <param name="dtPLanesOfer"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-14
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        private DataTable LlenarRepetidor(UserControl PageSource, int iPage, string sTamanioPagina, bool bRandom, string sOrdenamiento,
            clsParametros cParametros, DataTable dtPLanesOfer)
        {
            DataList dtlPaginador = (DataList)PageSource.FindControl("dtlPaginador");
            DataList dtlPaginadorInf = (DataList)PageSource.FindControl("dtlPaginadorInf");

            Repeater rptPlanes = (Repeater)PageSource.FindControl("dtlOfertas");
            string strOferta = clsValidaciones.GetKeyOrAdd("ClasificaCionOferta", "OFR");
            if (dtPLanesOfer != null)
            {
                dtPLanesOfer = setUrlPlan(PageSource, dtPLanesOfer);
                int iPageSize = 0;
                if (sTamanioPagina != "0" && sTamanioPagina != null)
                    iPageSize = Convert.ToInt32(sTamanioPagina);
                else
                    iPageSize = dtPLanesOfer.Rows.Count;

                if (bRandom)
                {

                    dtPLanesOfer = new Utils.Utils().dtRandom(dtPLanesOfer, iPageSize, false);
                }
                else
                {
                    DataView dvOrden = new DataView(dtPLanesOfer);
                    if (!string.IsNullOrEmpty(sOrdenamiento))
                    {
                        dvOrden.Sort = sOrdenamiento;
                    }
                    dtPLanesOfer = dvOrden.ToTable();
                }

                #region [ExisteRepeater]
                if (rptPlanes != null)
                {
                    if (dtlPaginadorInf != null)
                    {
                        csGeneralsPag.Paginar(rptPlanes, dtPLanesOfer, iPage, dtlPaginadorInf, iPageSize);
                    }

                    if (dtlPaginador != null)
                    {
                        csGeneralsPag.Paginar(rptPlanes, dtPLanesOfer, iPage, dtlPaginador, iPageSize);
                    }
                    else
                    {
                        rptPlanes.DataSource = dtPLanesOfer;
                        rptPlanes.DataBind();
                    }

                    for (int c = 0; c < rptPlanes.Items.Count; c++)
                    {
                        Image imgOferta = rptPlanes.Items[c].FindControl("imgOferta") as Image;

                        if (imgOferta != null && dtPLanesOfer.Rows[c]["strRefereClasificacion"].ToString().Equals(strOferta))
                        {
                            imgOferta.Visible = true;
                        }
                    }
                }
                #endregion
            }
            else
            {
                if (rptPlanes != null)
                {
                    rptPlanes.DataSource = null;
                    rptPlanes.DataBind();
                }


                /*si ,no hay resultados mostramos el mensaje */
                cParametros.Id = 0;
                cParametros.ViewMessage.Add("No se encontraron resultados para tu busqueda");
                cParametros.Sugerencia.Add("Por favor intenta de nuevo");
                cParametros.Severity = clsSeveridad.Media;
                cParametros.Tipo = clsTipoError.Library;
                HtmlGenericControl dPanel = PageSource.FindControl("dPanel") as HtmlGenericControl;
                clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
                objErrorMensaje.getError(cParametros, dPanel);
            }
            return dtPLanesOfer;
        }

        /// <summary>
        /// Metodo que genera la url de detalle de plan
        /// </summary>
        /// <param name="PageSource">Usercontrol</param>
        /// <param name="tbl">TablaPlanes</param>
        /// <returns>TablaPlanes con comlumna de url</returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-14
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        public DataTable setUrlPlan(UserControl PageSource, DataTable tbl)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                string sTemasMsjCot = "";
                string sTipoMsjCot = "";
                string sFiltroDestino = "";
                //if (clsValidaciones.GetKeyOrAdd("TemaTipoCotizacionPlanes", "False").ToUpper().Equals("TRUE"))
                //{
                //    sTemasMsjCot = "&TPOMSJ=" + clsValidaciones.GetKeyOrAdd("TipoMsjSolicitud", "TM003");

                //    tblRefere tRefere = new tblRefere();
                //    tRefere.Get(clsValidaciones.GetKeyOrAdd("TemasMensajes", "TMMSJ"),
                //        clsValidaciones.GetKeyOrAdd("TemaMensajeCotizacionOfertas", "COTOFR"));
                //    if (tRefere.Respuesta)
                //    {
                //        sTipoMsjCot = "&TMMSJ=" + tRefere.intidRefere.Value;
                //    }
                //}               
                if (PageSource.Request.QueryString["Destino"] != null)
                    sFiltroDestino = "&Destino=" + PageSource.Request.QueryString["Destino"];
                tbl.Columns.Add("Url");
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    /*si es circuitos o excurciones*/
                    if (tbl.Rows[i]["strRefereTipoPlan"].ToString() == clsValidaciones.GetKeyOrAdd("TipoPlanWs", "PLNWS"))
                    {
                        tbl.Rows[i]["Url"] =
                             "../Presentacion/DetallePlanWs.aspx?id=" + tbl.Rows[i]["intCodigo"].ToString() +
                                              "&Codigo=" + tbl.Rows[i]["intCodigo"].ToString() +
                                              "&TipoPlan=" + tbl.Rows[i]["strRefereTipoPlan"].ToString() +
                                              "&Detalle=" + tbl.Rows[i]["Nombre"].ToString() +
                                              "&ControlCupos=" + tbl.Rows[i]["strRefereControlCupos"].ToString() +
                                              "&Plantilla=" + tbl.Rows[i]["strReferePlantilla"].ToString() +
                                              "&Cotizacion=" + tbl.Rows[i]["strRefereCotizacion"].ToString() + sTemasMsjCot + sTipoMsjCot + sFiltroDestino;
                    }/*si es circuitos o excurciones*/
                    if (tbl.Rows[i]["strRefereTipoPlan"].ToString() == "CIRC")
                    {
                        tbl.Rows[i]["Url"] =
                             "../Presentacion/DetallePlan.aspx?id=" + tbl.Rows[i]["intCodigo"].ToString() +
                                              "&Codigo=" + tbl.Rows[i]["intCodigo"].ToString() +
                                              "&TipoPlan=" + tbl.Rows[i]["strRefereTipoPlan"].ToString() +
                                              "&Detalle=" + tbl.Rows[i]["Nombre"].ToString() +
                                              "&ControlCupos=" + tbl.Rows[i]["strRefereControlCupos"].ToString() +
                                              "&Plantilla=" + tbl.Rows[i]["strReferePlantilla"].ToString() +
                                              "&Cotizacion=" + tbl.Rows[i]["strRefereCotizacion"].ToString() + sTemasMsjCot + sTipoMsjCot + sFiltroDestino;
                    }/*casas*/
                    if (tbl.Rows[i]["strRefereTipoPlan"].ToString() == "CASA")
                    {
                        tbl.Rows[i]["Url"] =
                            "../Presentacion/DetalleApartamento.aspx?id=" + tbl.Rows[i]["intCodigo"].ToString() +
                                            "&Codigo=" + tbl.Rows[i]["intCodigo"].ToString() +
                                            "&TipoPlan=" + tbl.Rows[i]["strRefereTipoPlan"].ToString() +
                                            "&Detalle=" + tbl.Rows[i]["Nombre"].ToString() +
                                            "&ControlCupos=" + tbl.Rows[i]["strRefereControlCupos"].ToString() +
                                            "&Plantilla=" + tbl.Rows[i]["strReferePlantilla"].ToString() +
                                            "&Cotizacion=" + tbl.Rows[i]["strRefereCotizacion"].ToString() + sTemasMsjCot + sTipoMsjCot + sFiltroDestino;

                    }
                    /*Apartamentos*/
                    if (tbl.Rows[i]["strRefereTipoPlan"].ToString() == clsValidaciones.GetKeyOrAdd("TipoPlanApartamento", "APTO"))
                    {
                        tbl.Rows[i]["Url"] =
                            "../Presentacion/DetalleApartamento.aspx?id=" + tbl.Rows[i]["intCodigo"].ToString() +
                                            "&Codigo=" + tbl.Rows[i]["intCodigo"].ToString() +
                                            "&TipoPlan=" + tbl.Rows[i]["strRefereTipoPlan"].ToString() +
                                            "&Detalle=" + tbl.Rows[i]["Nombre"].ToString() +
                                            "&ControlCupos=" + tbl.Rows[i]["strRefereControlCupos"].ToString() +
                                            "&Plantilla=" + tbl.Rows[i]["strReferePlantilla"].ToString() +
                                            "&Cotizacion=" + tbl.Rows[i]["strRefereCotizacion"].ToString() + sTemasMsjCot + sTipoMsjCot + sFiltroDestino;

                    }
                    /*cruceros*/
                    if (tbl.Rows[i]["strRefereTipoPlan"].ToString() == "CRUCE")
                    {
                        tbl.Rows[i]["Url"] =
                            "../Presentacion/DetalleCrucero.aspx?id=" + tbl.Rows[i]["intCodigo"].ToString() +
                                             "&Codigo=" + tbl.Rows[i]["intCodigo"].ToString() +
                                             "&TipoPlan=" + tbl.Rows[i]["strRefereTipoPlan"].ToString() +
                                             "&Detalle=" + tbl.Rows[i]["Nombre"].ToString() +
                                             "&ControlCupos=" + tbl.Rows[i]["strRefereControlCupos"].ToString() +
                                             "&Plantilla=" + tbl.Rows[i]["strReferePlantilla"].ToString() +
                                             "&Cotizacion=" + tbl.Rows[i]["strRefereCotizacion"].ToString() + sTemasMsjCot + sTipoMsjCot + sFiltroDestino;
                    }
                    if (tbl.Rows[i]["strRefereTipoPlan"].ToString() == "TRAS" || tbl.Rows[i]["strRefereTipoPlan"].ToString() == "EXC")
                    {
                        tbl.Rows[i]["Url"] =
                            "../Presentacion/DetalleExcursion.aspx?id=" + tbl.Rows[i]["intCodigo"].ToString() +
                                             "&Codigo=" + tbl.Rows[i]["intCodigo"].ToString() +
                                             "&TipoPlan=" + tbl.Rows[i]["strRefereTipoPlan"].ToString() +
                                             "&Detalle=" + tbl.Rows[i]["Nombre"].ToString() +
                                             "&ControlCupos=" + tbl.Rows[i]["strRefereControlCupos"].ToString() +
                                             "&Plantilla=" + tbl.Rows[i]["strReferePlantilla"].ToString() +
                                             "&Cotizacion=" + tbl.Rows[i]["strRefereCotizacion"].ToString() + sTemasMsjCot + sTipoMsjCot + sFiltroDestino;
                    }
                    if (tbl.Rows[i]["strRefereTipoPlan"].ToString() == clsValidaciones.GetKeyOrAdd("TipoPlanSouvenir", "SOUV"))
                    {
                        tbl.Rows[i]["Url"] =
                            "../Presentacion/DetalleSouvenir.aspx?id=" + tbl.Rows[i]["intCodigo"].ToString() +
                                             "&Codigo=" + tbl.Rows[i]["intCodigo"].ToString() +
                                             "&TipoPlan=" + tbl.Rows[i]["strRefereTipoPlan"].ToString() +
                                             "&Detalle=" + tbl.Rows[i]["Nombre"].ToString() +
                                             "&ControlCupos=" + tbl.Rows[i]["strRefereControlCupos"].ToString() +
                                             "&Plantilla=" + tbl.Rows[i]["strReferePlantilla"].ToString() +
                                             "&Cotizacion=" + tbl.Rows[i]["strRefereCotizacion"].ToString() + sTemasMsjCot + sTipoMsjCot + sFiltroDestino;
                    }
                    if (tbl.Rows[i]["strRefereTipoPlan"].ToString() == clsValidaciones.GetKeyOrAdd("TipoPlanHotel", "HOTEL"))
                    {
                        tbl.Rows[i]["Url"] =
                            "../Presentacion/DetallePlanDisney.aspx?id=" + tbl.Rows[i]["intCodigo"].ToString() +
                                             "&Codigo=" + tbl.Rows[i]["intCodigo"].ToString() +
                                             "&TipoPlan=" + tbl.Rows[i]["strRefereTipoPlan"].ToString() +
                                             "&Detalle=" + tbl.Rows[i]["Nombre"].ToString() +
                                             "&ControlCupos=" + tbl.Rows[i]["strRefereControlCupos"].ToString() +
                            //"&Plantilla=" + tbl.Rows[i]["strReferePlantilla"].ToString() +
                                             "&Cotizacion=" + tbl.Rows[i]["strRefereCotizacion"].ToString() + sTemasMsjCot + sTipoMsjCot + sFiltroDestino;
                    }
                    if (tbl.Rows[i]["strRefereTipoPlan"].ToString() == clsValidaciones.GetKeyOrAdd("TipoPlanParques", "PQ"))
                    {
                        if (PageSource.Request.QueryString["ClasifPasaporte"] == null)
                        {
                            tbl.Rows[i]["Url"] =
                                "../Presentacion/DetallePasaporte.aspx?id=" + tbl.Rows[i]["intCodigo"].ToString() +
                                                 "&Codigo=" + tbl.Rows[i]["intCodigo"].ToString() +
                                                 "&TipoPlan=" + tbl.Rows[i]["strRefereTipoPlan"].ToString() +
                                                 "&Detalle=" + tbl.Rows[i]["Nombre"].ToString() +
                                                 "&ControlCupos=" + tbl.Rows[i]["strRefereControlCupos"].ToString() +
                                //"&Plantilla=" + tbl.Rows[i]["strReferePlantilla"].ToString() +
                                                 "&Cotizacion=" + tbl.Rows[i]["strRefereCotizacion"].ToString() + sTemasMsjCot + sTipoMsjCot + sFiltroDestino;

                        }
                        else
                        {
                            tbl.Rows[i]["Url"] =
                                "../Presentacion/DetallePasaporteNoDisney.aspx?id=" + tbl.Rows[i]["intCodigo"].ToString() +
                                                 "&Codigo=" + tbl.Rows[i]["intCodigo"].ToString() +
                                                 "&TipoPlan=" + tbl.Rows[i]["strRefereTipoPlan"].ToString() +
                                                 "&Detalle=" + tbl.Rows[i]["Nombre"].ToString() +
                                                 "&ControlCupos=" + tbl.Rows[i]["strRefereControlCupos"].ToString() +
                                //"&Plantilla=" + tbl.Rows[i]["strReferePlantilla"].ToString() +
                                                 "&Cotizacion=" + tbl.Rows[i]["strRefereCotizacion"].ToString() +
                                                 "&ClasifPasaporte=" + PageSource.Request.QueryString["ClasifPasaporte"] + sTemasMsjCot + sTipoMsjCot + sFiltroDestino;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "setUrlPlan";
                ExceptionHandled.Publicar(cParametros);
            }
            return tbl;
        }

        /// <summary>
        /// Metodo general del llenado de detalle de plan
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-18
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        public void setDetalleGeneral(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();

            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    string strCodigo = PageSource.Request.QueryString["Codigo"].ToString();

                    PageSource.Session["Adultos"] = "1";
                    PageSource.Session["Ninos"] = "0";

                    CargarDetalle(strCodigo, PageSource);

                    Label lblTEnCuenta = PageSource.FindControl("lblTEnCuenta") as Label;
                    Label strEncuenta = PageSource.FindControl("strEncuenta") as Label;
                    if (strEncuenta != null)
                    {
                        if (strEncuenta.Text.Trim().Equals(""))
                        {
                            if (lblTEnCuenta != null)
                                lblTEnCuenta.Visible = false;
                            strEncuenta.Visible = false;
                        }
                    }
                    Label lblTQueIncluye = PageSource.FindControl("lblTQueIncluye") as Label;
                    Label strIncluye = PageSource.FindControl("strIncluye") as Label;
                    if (strIncluye != null)
                    {
                        if (strIncluye.Text.Trim().Equals(""))
                        {
                            if (lblTQueIncluye != null)
                                lblTQueIncluye.Visible = false;
                            strIncluye.Visible = false;
                        }
                    }
                    Label lblTQueNoIncluye = PageSource.FindControl("lblTQueNoIncluye") as Label;
                    Label strNoIncluye = PageSource.FindControl("strNoIncluye") as Label;
                    if (strNoIncluye != null)
                    {
                        if (strNoIncluye.Text.Trim().Equals(""))
                        {
                            if (lblTQueNoIncluye != null)
                                lblTQueNoIncluye.Visible = false;
                            strNoIncluye.Visible = false;
                        }
                    }
                    Label lblTCondiciones = PageSource.FindControl("lblTCondiciones") as Label;
                    Label strRestriccion = PageSource.FindControl("strRestriccion") as Label;
                    if (strRestriccion != null)
                    {
                        if (strRestriccion.Text.Trim().Equals(""))
                        {
                            if (lblTCondiciones != null)
                                lblTCondiciones.Visible = false;
                            strRestriccion.Visible = false;
                        }
                    }
                    Label strHTML = PageSource.FindControl("strHTML") as Label;
                    if (strHTML != null)
                    {
                        if (strHTML.Text.Trim().Equals(""))
                        {
                            Panel pnCalificacion = PageSource.FindControl("pnCalificacion") as Panel;
                            if (pnCalificacion != null)
                                pnCalificacion.Visible = false;
                        }
                    }
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "LlenarDatosPlanes";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo general de carga de detalle, informacion general, textos e imagenes
        /// </summary>
        /// <param name="sCodigo"></param>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-20
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        private void CargarDetalle(string sCodigo, UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                csGeneralsPag.Idioma(PageSource);
                clsCache cCache = new csCache().cCache();

                if (cCache != null)
                {
                    string sTipoPlanWs = clsValidaciones.GetKeyOrAdd("TipoPlanWs", "PLNWS");
                    string sIdioma = clsSesiones.getIdioma();
                    #region Definicion de controles
                    Image strImagen = (Image)PageSource.FindControl("strImagen");
                    Image imgITinerario = (Image)PageSource.FindControl("imgITinerario");
                    Label strRestriccion = (Label)PageSource.FindControl("strRestriccion");
                    Label strEncuenta = (Label)PageSource.FindControl("strEncuenta");
                    Label lblMas = (Label)PageSource.FindControl("lblMas");
                    Label lblVerMas = (Label)PageSource.FindControl("lblVerMas");
                    Label lblDescBreve = (Label)PageSource.FindControl("lblDescBreve");

                    Label lblMasEnCuenta = (Label)PageSource.FindControl("lblMasEnCuenta");
                    Label lblVerMasEnCuenta = (Label)PageSource.FindControl("lblVerMasEnCuenta");

                    Label strNombrePlan = (Label)PageSource.FindControl("strNombrePlan");
                    Label strDescripcion = (Label)PageSource.FindControl("strDescripcion");

                    Label lblDuracion = PageSource.FindControl("lblDuracion") as Label;
                    Label lblAcomodacion = PageSource.FindControl("lblAcomodacion") as Label;
                    Label lblPlan = PageSource.FindControl("lblPlan") as Label;

                    csConsultasPlanes cPlanes = new csConsultasPlanes();
                    Label strCiudad = (Label)PageSource.FindControl("strCiudad");
                    Label strIncluye = PageSource.FindControl("strIncluye") as Label;
                    Label strNoIncluye = PageSource.FindControl("strNoIncluye") as Label;

                    Label lblVigenciaDesde = PageSource.FindControl("lblVigenciaDesde") as Label;
                    Label lblVigenciaHasta = PageSource.FindControl("lblVigenciaHasta") as Label;

                    Label strCodigoExterno = PageSource.FindControl("strCodigoExterno") as Label;

                    Label dblPrecio = (Label)PageSource.FindControl("dblPrecio");

                    Label lblItinerario = (Label)PageSource.FindControl("lblItinerario");
                    Label lblVerMasTexto = (Label)PageSource.FindControl("lblVerMasTexto");
                    Label lblClasificacion = (Label)PageSource.FindControl("lblClasificacion");
                    #endregion

                    DataTable dtPlanes = new DataTable();
                    dtPlanes = cPlanes.ConsultaPlan(sCodigo);

                    PageSource.Session["$TablaDetallePlan"] = dtPlanes;

                    if (dtPlanes != null)
                    {
                        new Utils.Utils().ModificarTablaRPTImagenes(dtPlanes, PageSource, true);
                        DataTable tblTextosPlan = cPlanes.ConsultaTextosImagenesPlanes(sCodigo, false);
                        if (tblTextosPlan != null)
                        {
                            PageSource.Session["$TablaTextosPlanes"] = tblTextosPlan;
                            dtPlanes = CargarTextosPlanPrimero(clsValidaciones.GetKeyOrAdd("TipoTextoPlanDesc", "DESC"), "strDescripcion", tblTextosPlan, dtPlanes);
                            dtPlanes = CargarTextosPlanPrimero(clsValidaciones.GetKeyOrAdd("TipoTextoPlanIncluye", "INC"), "strIncluye", tblTextosPlan, dtPlanes);
                            dtPlanes = CargarTextosPlanPrimero(clsValidaciones.GetKeyOrAdd("TipoTextoPlanNoIncluye", "NOINC"), "strNoIncluye", tblTextosPlan, dtPlanes);
                            /*Habilitamos o deshabilitamos el boton de itinerario*/
                            HabilitarODeshabilatarBotonTexto(PageSource, "btnItinerario", clsValidaciones.GetKeyOrAdd("TipoTextoPlanItinerario", "ITIN"), tblTextosPlan);
                            #region Textos
                            //<-------- Restricciones ----------->
                            CargarTextosPlan(clsValidaciones.GetKeyOrAdd("TipoTextoPlanCondiciones", "COND"), strRestriccion, lblMas, tblTextosPlan);
                            //<-------- Fin Restricciones ----------->

                            //<-------- Tenga en cuenta ----------->
                            CargarTextosPlan(clsValidaciones.GetKeyOrAdd("TipoTextoPlanTengaCuenta", "ENCUENTA"), strEncuenta, lblMasEnCuenta, tblTextosPlan);
                            //<-------- Fin Tenga en Cuenta ----------->

                            //<-------- Itinerario ----------->
                            CargarTextosPlan(clsValidaciones.GetKeyOrAdd("TipoTextoPlanItinerario", "ITIN"), lblItinerario, lblVerMasTexto, tblTextosPlan);
                            //<-------- Fin Itinerario ----------->
                            CargarTextosPlan(clsValidaciones.GetKeyOrAdd("TipoTextoDescripcionBreve", "TARREF"), lblDescBreve, null, tblTextosPlan);

                            //<-------- Clasificacion ----------->
                            if (lblClasificacion != null)
                                CargarTextosPlan(clsValidaciones.GetKeyOrAdd("TipoTextoPlanCalificacion", "CALIF"), lblClasificacion, null, tblTextosPlan);
                            //<-------- Fin Clasificacion ----------->

                            #endregion
                        }

                        #region LLenado Controles
                        if (lblPlan != null)
                            lblPlan.Text = dtPlanes.Rows[0]["strNombrePlan"].ToString();

                        if (strCodigoExterno != null)
                            strCodigoExterno.Text = dtPlanes.Rows[0]["strCodigoExterno"].ToString();

                        if (strNombrePlan != null)
                            strNombrePlan.Text = dtPlanes.Rows[0]["strNombrePlan"].ToString();

                        if (strDescripcion != null)
                            strDescripcion.Text = dtPlanes.Rows[0]["strDescripcion"].ToString();

                        if (strIncluye != null)
                            strIncluye.Text = dtPlanes.Rows[0]["strIncluye"].ToString();

                        if (strNoIncluye != null)
                            strNoIncluye.Text = dtPlanes.Rows[0]["strNoIncluye"].ToString();

                        strImagen.AlternateText = dtPlanes.Rows[0]["strNombrePlan"].ToString();
                        try
                        {
                            if (strCiudad != null)
                                strCiudad.Text = " - " + dtPlanes.Rows[0]["strCiudad"].ToString();
                        }
                        catch { }
                        /*CODIGO DE LA CIUDAD*/
                        HiddenField hidenCiudad = PageSource.FindControl("hidenCiudad") as HiddenField;
                        if (hidenCiudad != null && dtPlanes != null && dtPlanes.Rows.Count > 0)
                            hidenCiudad.Value = dtPlanes.Rows[0]["intCiudad"].ToString();

                        //Ocultar la imagen de oferta en caso de que no lo sea
                        Image imgOferta = PageSource.FindControl("imgOferta") as Image;
                        if (imgOferta != null)
                        {
                            if (dtPlanes.Rows[0]["strRefereClasificacion"].ToString() != clsValidaciones.GetKeyOrAdd("ClasificaCionOferta", "OFR"))
                                imgOferta.Visible = false;
                        }

                        //Agregar la url de la imagen de la categoria
                        Image imgCategoria = PageSource.FindControl("imgCategoria") as Image;
                        if (imgCategoria != null)
                            imgCategoria.ImageUrl = dtPlanes.Rows[0]["urlImagenCategoria"].ToString();

                        //Agregar la duracion en caso de que sea excursion
                        if (lblDuracion != null)
                            lblDuracion.Text = "Duración: " + dtPlanes.Rows[0]["strDetalleDuracion"].ToString();

                        if (lblVigenciaDesde != null)
                            lblVigenciaDesde.Text = dtPlanes.Rows[0]["dtmVDesde"].ToString();

                        if (lblVigenciaHasta != null)
                            lblVigenciaHasta.Text = dtPlanes.Rows[0]["dtmVHasta"].ToString();

                        clsValidaciones.ActualiceDatatable(dtPlanes, "dblPrecio", null, PosicionText.Formato, null);

                        if (dblPrecio != null)
                            dblPrecio.Text = Convert.ToDecimal(dblPrecio.Text).ToString("###,###.###");


                        #endregion

                        DataTable tblImagenesPlan = cPlanes.ConsultaTextosImagenesPlanes(sCodigo, true);
                        #region Imagenes
                        string Ruta = clsValidaciones.ObtenerUrlPlanes();
                        if (tblImagenesPlan != null && tblImagenesPlan.Rows.Count > 0)
                        {
                            DataView dvFiltro = new DataView(tblImagenesPlan);
                            dvFiltro.RowFilter = " strCodigo = '" + clsValidaciones.GetKeyOrAdd("TipoImagenGrande", "IMGGRANDE") + "'";
                            if (dvFiltro.ToTable() != null && dvFiltro.ToTable().Rows.Count > 0)
                            {
                                strImagen.ImageUrl = Ruta + dvFiltro.ToTable().Rows[0]["strNombreImagen"].ToString();
                            }
                            else 
                            {
                                strImagen.ImageUrl = "";
                            }
                               

                            dvFiltro = new DataView(tblImagenesPlan);
                            dvFiltro.RowFilter = " strCodigo = '" + clsValidaciones.GetKeyOrAdd("TipoImagenItinerario", "IMGITIN") + "'";
                            if (dvFiltro.ToTable() != null && dvFiltro.ToTable().Rows.Count > 0)
                                imgITinerario.ImageUrl = Ruta + dvFiltro.ToTable().Rows[0]["strNombreImagen"].ToString();

                            Repeater rptGaleria = (Repeater)PageSource.FindControl("rptGaleria");
                            if (rptGaleria != null)
                            {
                                dvFiltro = new DataView(tblImagenesPlan);
                                dvFiltro.RowFilter = " strCodigo = '" + clsValidaciones.GetKeyOrAdd("TipoImagenGaleria", "GALERIA") + "'";
                                if (dvFiltro.ToTable() != null && dvFiltro.ToTable().Rows.Count > 0)
                                {
                                    DataTable dtGaleria = dvFiltro.ToTable();
                                    for (int i = 0; i < dtGaleria.Rows.Count; i++)
                                    {
                                        dtGaleria.Rows[i]["strNombreImagen"] = Ruta + dtGaleria.Rows[i]["strNombreImagen"].ToString();
                                    }
                                    rptGaleria.DataSource = dtGaleria;
                                    rptGaleria.DataBind();
                                }
                            }
                        }

                        #endregion
                    }

                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "CargarDetalle";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que habilita un boton dependiendo si el plan tiene cargado el texto correspondiente
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="sNombreBoton"></param>
        /// <param name="sTipoTexto"></param>
        /// <param name="dtTextosPlanes"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-20
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        private static void HabilitarODeshabilatarBotonTexto(UserControl PageSource, string sNombreBoton, string sTipoTexto, DataTable dtTextosPlanes)
        {
            try
            {
                Button btn = PageSource.FindControl(sNombreBoton) as Button;
                if (btn != null && dtTextosPlanes != null)
                {
                    DataView dvFiltro = new DataView(dtTextosPlanes);
                    dvFiltro.RowFilter = "strCodTexto = '" + sTipoTexto + "'";
                    DataTable tblResul = dvFiltro.ToTable();
                    if (tblResul != null && tblResul.Rows.Count > 0)
                    {
                        btn.Enabled = true;
                    }
                    else
                    {
                        btn.Enabled = false;
                        btn.CssClass = "botonItinerarioOff";
                    }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Metodo que carga el primer texto de un determinado tipo para que sea el texto de mostrar, los demas se cargaran en el ver mas
        /// </summary>
        /// <param name="sCodigoTexto"></param>
        /// <param name="strColumnaTablaPlan"></param>
        /// <param name="tblTextosCompletos"></param>
        /// <param name="tblPlan"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-20
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        private DataTable CargarTextosPlanPrimero(string sCodigoTexto, string strColumnaTablaPlan, DataTable tblTextosCompletos, DataTable tblPlan)
        {
            try
            {
                DataView dvFiltro = new DataView(tblTextosCompletos);
                dvFiltro.RowFilter = "strCodTexto = '" + sCodigoTexto + "'";
                DataTable tblResul = dvFiltro.ToTable();
                tblPlan.Rows[0][strColumnaTablaPlan] = tblResul.Rows[0]["strTexto"].ToString();
            }
            catch { }
            return tblPlan;
        }

        /// <summary>
        /// Metodo que carga un texto del plan en el label correspondiente
        /// </summary>
        /// <param name="sCodigoTexto"></param>
        /// <param name="lblTexto"></param>
        /// <param name="lblVerMasTexto"></param>
        /// <param name="tblTextosCompletos"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-18
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        private void CargarTextosPlan(string sCodigoTexto, Label lblTexto, Label lblVerMasTexto, DataTable tblTextosCompletos)
        {
            try
            {
                string strTexto = string.Empty;
                if (lblTexto != null)
                    strTexto = lblTexto.Text;

                //filtramos la tabla de textos con el tipo correspondiente
                DataView dvFiltro = new DataView(tblTextosCompletos);
                dvFiltro.RowFilter = "strCodTexto = '" + sCodigoTexto + "'";
                DataTable tblResul = dvFiltro.ToTable();

                if (tblResul != null && tblResul.Rows.Count > 0)
                {
                    //el primer registro de texto sera el general de mostrar
                    if (lblTexto != null)
                        lblTexto.Text = strTexto + tblResul.Rows[0]["strTexto"].ToString(); ;

                    //los demas registros seran el ver mas
                    string sTextoAdicional = string.Empty;
                    for (int i = 1; i < tblResul.Rows.Count; i++)
                    {
                        sTextoAdicional += "<separar/>" + tblResul.Rows[i]["strTexto"].ToString();
                    }
                    if (lblVerMasTexto != null)
                    {
                        lblVerMasTexto.Text = sTextoAdicional;
                        if (lblVerMasTexto.Text.Trim() == "")
                            lblVerMasTexto.Visible = false;
                    }
                }
            }
            catch { }
        }

        public void setDetallesHotel(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();

                if (cCache != null)
                {
                    HtmlImage Imagen = (HtmlImage)PageSource.FindControl("Imagen");
                    string idHotel = HttpContext.Current.Request.QueryString["intIdHotel"];
                    csConsultasOperadores cPlanes = new csConsultasOperadores();
                    //csGenerales cGeneral = new csGenerales();
                    Utils.Utils cCombo = new Utils.Utils();

                    DataTable dtHotel = cPlanes.Cosulta_detalle_operador(idHotel);
                    if (dtHotel != null)
                    {
                        cCombo.ModificarTablaRPTImagenes(dtHotel, PageSource, true);
                        if (Imagen != null)
                            Imagen.Src = dtHotel.Rows[0]["Imagen"].ToString();
                        //if (Imagen.Src.Contains("spacer"))
                        //{
                        //    Imagen.Visible = false;
                        //}
                        Label Descripcion = (Label)PageSource.FindControl("Descripcion");
                        if (Descripcion != null)
                            Descripcion.Text = System.Web.HttpUtility.HtmlDecode(Descripcion.Text);
                        //LlenarForm(idHotel, idAplicacion, PageSource);
                    }
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "setDetallesHotel";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        private string[] csValue(UserControl PageSource)
        {
            string[] sValue = new string[15];
            try
            {
                if (PageSource.Request.QueryString["Codigo"] != null)
                {
                    sValue[0] = PageSource.Request.QueryString["Codigo"].ToString();
                }
                else
                {
                    sValue[0] = "0";
                }
                if (PageSource.Request.QueryString["TipoPlan"] != null)
                {
                    sValue[1] = PageSource.Request.QueryString["TipoPlan"].ToString();
                }
                else
                {
                    sValue[1] = "0";
                }
                if (PageSource.Request.QueryString["IdTipologia"] != null)
                {
                    sValue[2] = PageSource.Request.QueryString["IdTipologia"].ToString();
                }
                else
                {
                    sValue[2] = "0";
                }
                if (PageSource.Request.QueryString["IdPais"] != null)
                {
                    sValue[3] = PageSource.Request.QueryString["IdPais"].ToString();
                }
                else
                {
                    sValue[3] = "0";
                }
                if (PageSource.Request.QueryString["intIdCat"] != null)
                {
                    sValue[4] = PageSource.Request.QueryString["intIdCat"].ToString();
                }
                else
                {
                    sValue[4] = "0";
                }
                if (PageSource.Request.QueryString["id"] != null)
                {
                    sValue[5] = PageSource.Request.QueryString["id"].ToString();
                }
                else
                {
                    sValue[5] = "0";
                }
                if (PageSource.Request.QueryString["Aereo"] != null)
                {
                    sValue[6] = PageSource.Request.QueryString["Aereo"].ToString();
                }
                else
                {
                    sValue[6] = "0";
                }
                if (PageSource.Request.QueryString["TIPODESTINO"] != null)
                {
                    sValue[7] = PageSource.Request.QueryString["TIPODESTINO"].ToString();
                }
                else
                {
                    sValue[7] = "0";
                }
                if (PageSource.Request.QueryString["IdCiudad"] != null)
                {
                    sValue[8] = PageSource.Request.QueryString["IdCiudad"].ToString();
                }
                else
                {
                    sValue[8] = "0";
                }
                if (PageSource.Request.QueryString["TIPOPLANCONS"] != null)
                {
                    sValue[9] = PageSource.Request.QueryString["TIPOPLANCONS"].ToString();
                }
                else
                {
                    sValue[9] = "0";
                }
                if (PageSource.Request.QueryString["ControlCupos"] != null)
                {
                    sValue[10] = PageSource.Request.QueryString["ControlCupos"].ToString();
                }
                else
                {
                    sValue[10] = "0";
                }
                if (PageSource.Request.QueryString["Plantilla"] != null)
                {
                    sValue[11] = PageSource.Request.QueryString["Plantilla"].ToString();
                }
                else
                {
                    sValue[11] = "0";
                }
                if (PageSource.Request.QueryString["ORIGEN"] != null)
                {
                    if (PageSource.Request.QueryString["ORIGEN"].ToUpper().Contains("BUSCADOR"))
                        sValue[12] = "1";
                    else
                        sValue[12] = "0";
                }
                else
                {
                    sValue[12] = "0";
                }
                if (PageSource.Request.QueryString["CODCIU"] != null)
                {
                    if (PageSource.Request.QueryString["CODCIU"].ToString().Equals("0"))
                        sValue[13] = "1";
                    else
                        sValue[13] = PageSource.Request.QueryString["CODCIU"].ToString();
                }
                else
                {
                    sValue[13] = "0";
                }
                if (PageSource.Request.QueryString["PaxControl"] != null)
                {
                    sValue[14] = PageSource.Request.QueryString["PaxControl"].ToString();
                }
                else
                {
                    sValue[14] = "0";
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "csValue";
                cParametros.Complemento = "csResyltadoPlanes";
                ExceptionHandled.Publicar(cParametros);
            }
            return sValue;
        }

        //public void ddlPais_SelectedIndexChanged(object sender, EventArgs e, UserControl PageSource)
        //{
        //    csGeneralsPag.Idioma(PageSource);

        //    DropDownList ddlPais = (DropDownList)PageSource.FindControl("ddlPais");
        //    DropDownList ddlCiudad = (DropDownList)PageSource.FindControl("ddlCiudad");

        //    csReglasPlanes cPlanes = new csReglasPlanes();
        //    tblRefere otblRefere = new tblRefere();
        //    otblRefere.Get(clsValidaciones.GetKeyOrAdd("Estado", "EST"), clsValidaciones.GetKeyOrAdd("EstadoActivo", "act"));
        //    DataTable dtCiudades = cPlanes.ConsultarMulticiudadCircular(clsSesiones.getIdioma(), ddlPais.SelectedValue,
        //        otblRefere.intidRefere.Value, clsSesiones.getAplicacion().ToString());
        //    if (dtCiudades != null && dtCiudades.Rows.Count > 0)
        //    {
        //        clsControls.LlenaControl(ddlCiudad, dtCiudades.DataSet, "Referencia", "intidRefere", true);
        //        csBuscador cBusc = new csBuscador();
        //        cBusc.CargarTextoItem(ddlCiudad, "Todas las ciudades");
        //    }
        //}
        public void setFiltrarOfertas(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);
            clsParametros cParametros = new clsParametros();

            TextBox txtFiltroTexto = (TextBox)PageSource.FindControl("txtFiltroTexto");
            try
            {
                clsCache cCache = new csCache().cCache();

                if (cCache != null)
                {
                    string[] sValue = csValue(PageSource);
                    csGenerales Generales = new csGenerales();
                    DropDownList ddlZonaGeo = (DropDownList)PageSource.FindControl("ddlZonaGeo");
                    DropDownList ddlPais = (DropDownList)PageSource.FindControl("ddlPais");
                    DropDownList ddlCiudad = (DropDownList)PageSource.FindControl("ddlCiudad");
                    DropDownList ddlTipologia = (DropDownList)PageSource.FindControl("ddlTipologia");
                    DropDownList ddlTipoPlan = (DropDownList)PageSource.FindControl("ddlTipoPlan");

                    string sFiltroPais = "";
                    string sFiltroCiudad = "";
                    string sFiltroCategoria = "";
                    string sFiltroTipologia = "";
                    string sFiltroTipoDestino = "";
                    string sFiltroZonaGeo = "";
                    string filtroTexto = "";


                    string sPage = csGeneralsPag.PaginaActual();//"ResultadoOfertas.aspx";
                    //string sSesion = cCache.SessionID;
                    if (PageSource.ClientID.ToUpper().Contains("RESULTADOPLAN"))
                        sPage = "ResultadoPlanes.aspx";
                    string sRuta = clsValidaciones.ObtenerUrlRutaPage(sPage);

                    if (ddlZonaGeo != null && ddlZonaGeo.SelectedValue != "")
                    {
                        if (sRuta.Contains("?"))
                            sRuta += sFiltroZonaGeo = "&ZonaGeo=" + ddlZonaGeo.SelectedValue;
                        else
                            sRuta += sFiltroZonaGeo = "?ZonaGeo=" + ddlZonaGeo.SelectedValue;
                    }

                    if (ddlCiudad != null && ddlCiudad.SelectedValue != "")
                    {
                        if (sRuta.Contains("?"))
                            sRuta += sFiltroCiudad = "&IdCiudad=" + ddlCiudad.SelectedValue;
                        else
                            sRuta += sFiltroCiudad = "?IdCiudad=" + ddlCiudad.SelectedValue;
                    }
                    if (ddlPais != null && ddlPais.SelectedValue != "")
                    {
                        if (sRuta.Contains("?"))
                            sRuta += sFiltroPais = "&IdPais=" + ddlPais.SelectedValue;
                        else
                            sRuta += sFiltroPais = "?IdPais=" + ddlPais.SelectedValue;
                    }

                    if (ddlTipologia != null && ddlTipologia.SelectedValue != "")
                    {
                        if (sRuta.Contains("?"))
                            sRuta += sFiltroTipologia = "&IdTipologia=" + ddlTipologia.SelectedValue;
                        else
                            sRuta += sFiltroTipologia = "?IdTipologia=" + ddlTipologia.SelectedValue;
                    }

                    if (ddlTipoPlan != null && ddlTipoPlan.SelectedValue != "")
                    {
                        if (sRuta.Contains("?"))
                            sRuta += sFiltroCategoria = "&intIdCat=" + ddlTipoPlan.SelectedValue;
                        else
                            sRuta += sFiltroCategoria = "?intIdCat=" + ddlTipoPlan.SelectedValue;
                    }

                    //filtro de texto
                    if (txtFiltroTexto != null &&
                        !string.IsNullOrWhiteSpace(txtFiltroTexto.Text))
                    {
                        if (sRuta.Contains("?"))
                            sRuta += "&FiltroTexto=" + txtFiltroTexto.Text;
                        else
                            sRuta += "?FiltroTexto=" + txtFiltroTexto.Text;

                    }


                    if (sValue[7] != "0")
                    {
                        if (sRuta.Contains("?"))
                            sRuta += sFiltroTipoDestino = "&TIPODESTINO=" + sValue[7];
                        else
                            sRuta += sFiltroTipoDestino = "?TIPODESTINO=" + sValue[7];
                    }
                    try
                    {
                        if (PageSource.Request.QueryString["Clasif"] != null)
                        {
                            if (sRuta.Contains("?"))
                                sRuta += "&Clasif=" + PageSource.Request.QueryString["Clasif"];
                            else
                                sRuta += "?Clasif=" + PageSource.Request.QueryString["Clasif"];
                            clsValidaciones.RedirectPagina(sRuta);
                        }
                        else
                        {
                            clsValidaciones.RedirectPagina(sRuta);
                        }
                    }
                    catch (Exception) { }
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "setFiltrarOfertas";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        #region seguros
        public void setResumenBusquedaSeguros(UserControl PageSource)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    Label lblDestino = (Label)PageSource.FindControl("lblDestino");
                    Label lblFechaSalida = (Label)PageSource.FindControl("lblFechaSalida");
                    Label lblFechaRegreso = (Label)PageSource.FindControl("lblFechaRegreso");
                    Label lblPersonas = (Label)PageSource.FindControl("lblPersonas");

                    if (lblDestino != null && HttpContext.Current.Request.QueryString["ZnaTex"] != null)
                        lblDestino.Text = HttpContext.Current.Request.QueryString["ZnaTex"].ToString();

                    if (lblFechaSalida != null)
                        lblFechaSalida.Text = Convert.ToDateTime(clsValidaciones.ConverMDYtoYMD(cCache.DatosAdicionales[1], "/")).ToString("dddd, dd MMM yyyy");

                    if (lblFechaRegreso != null)
                        lblFechaRegreso.Text = Convert.ToDateTime(clsValidaciones.ConverMDYtoYMD(cCache.DatosAdicionales[2], "/")).ToString("dddd, dd MMM yyyy");

                    if (lblPersonas != null)
                        lblPersonas.Text = cCache.DatosAdicionales[3] + " Adulto(s)";

                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setResumenBusquedaSeguros";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public DataTable setInflarTarifasSeguros(DataTable tblTarifas, string sFechasNac, UserControl PageSource)
        {
            try
            {
                string[] sFechas = sFechasNac.Split(',');

                //columnas necesarias para la agrupacion por tipo de habitacion    
                tblTarifas.Columns.Add("dblTotalImpuestos");
                tblTarifas.Columns.Add("dblTotalSinImpuestos");
                tblTarifas.Columns.Add("dblTotalImpuestosFormato");
                tblTarifas.Columns.Add("dblTotalSinImpuestosFormato");
                tblTarifas.Columns.Add("tTarifasCompletoRegistro", tblTarifas.GetType());
                tblTarifas.Columns.Add("intSegmento");

                //se obtiene una tabla base con los valores de los adultos y los tipos de habitacion
                DataView dvFiltroADT = new DataView();
                dvFiltroADT.Table = tblTarifas;
                dvFiltroADT.RowFilter = "strRefereTipoPasajero = '" + clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT") + "'";
                DataTable tblFiltrada = dvFiltroADT.ToTable();

                for (int i = 0; i < tblFiltrada.Rows.Count; i++)
                {
                    DataTable tblTarifasNumPax = tblTarifas.Clone();
                    decimal dTarifa = 0;
                    decimal dTotal = 0;
                    for (int x = 0; x < sFechas.Length; x++)
                    {
                        //DateTime fNac = DateTime.Parse(sFechas[x]);                       
                        //int iAnios = 50;//aqui la validacion de los años del pax
                        string sFechaNac = clsValidaciones.ConverFecha(sFechas[x], "dd/MM/yyyy", "yyyy/MM/dd");
                        int iAnios = clsValidaciones.CalculoYear(sFechaNac);
                        tblTarifasNumPax.Rows.Add(tblFiltrada.Rows[i].ItemArray);
                        if (iAnios >= Convert.ToInt32(tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["EdadMaxPax"]))
                        {
                            int iPorcentaje = 0;
                            try
                            {
                                iPorcentaje = Convert.ToInt32(tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["PorIncTarifas"]);
                            }
                            catch
                            {

                            }

                            tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["Tarifa"] = Convert.ToDecimal(tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["Tarifa"]) * (1 + (iPorcentaje / 100));
                            tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["Precio"] = Convert.ToDecimal(tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["Precio"]) * (1 + (iPorcentaje / 100));
                            dTarifa = dTarifa + Convert.ToDecimal(tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["Tarifa"]);
                            dTotal = dTotal + Convert.ToDecimal(tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["Precio"]);

                        }
                        else
                        {
                            tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["Tarifa"] = Convert.ToDecimal(tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["Tarifa"]);
                            tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["Precio"] = Convert.ToDecimal(tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["Precio"]);
                            dTarifa = dTarifa + Convert.ToDecimal(tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["Tarifa"]);
                            dTotal = dTotal + Convert.ToDecimal(tblTarifasNumPax.Rows[tblTarifasNumPax.Rows.Count - 1]["Precio"]);
                        }
                    }
                    tblFiltrada.Rows[i]["Tarifa"] = dTarifa;
                    tblFiltrada.Rows[i]["Precio"] = dTotal;
                    tblFiltrada.Rows[i]["dblTotalImpuestos"] = dTotal;
                    tblFiltrada.Rows[i]["dblTotalSinImpuestos"] = dTarifa;
                    tblFiltrada.Rows[i]["tTarifasCompletoRegistro"] = tblTarifasNumPax;
                    tblFiltrada.Rows[i]["intSegmento"] = "1";//Convert.ToString(i + 1);
                }

                #region [Llenado totales]
                for (int i = 0; i < tblFiltrada.Rows.Count; i++)
                {
                    if (!clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                    {
                        tblFiltrada.Rows[i]["dblTotalImpuestosFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["Precio"].ToString()).ToString("###,##0.00");
                        tblFiltrada.Rows[i]["dblTotalSinImpuestosFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString()).ToString("###,##0.00");
                    }
                    else
                    {
                        tblFiltrada.Rows[i]["dblTotalImpuestosFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["Precio"].ToString()).ToString("###,###,###");
                        tblFiltrada.Rows[i]["dblTotalSinImpuestosFormato"] = Convert.ToDecimal(tblFiltrada.Rows[i]["Tarifa"].ToString()).ToString("###,###,###");
                    }
                }
                #endregion

                return tblFiltrada;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setInflarTarifasSeguros";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);

                return null;
            }
        }
        public void setConsultarTarifasSeguros(UserControl PageSource)
        {
            try
            {
                Label lblerror = (Label)PageSource.FindControl("lblerror");
                Repeater rptPlanes = (Repeater)PageSource.FindControl("dtlOfertas");
                csTarifasPlanes cPlanes = new csTarifasPlanes();
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    List<string> lDatos = cCache.DatosAdicionales;
                    HttpContext.Current.Session["tbltarifascontrol"] = null;
                    HttpContext.Current.Session["tbltarifas"] = null;


                    csConsultasPlanes cPlan = new csConsultasPlanes();
                    DataTable tblTarifasTodas = new DataTable();
                    DataTable tblTarifasGuardadas = new DataTable();

                    string sAplicacion = clsSesiones.getAplicacion().ToString();
                    bool bTodas = true;

                    DataTable dtPLanesOfer = cReglas.ReglasConsultaPlanes(PageSource, Enum_TipoDestino.Todos, false, "0",
                       false, true, "TJAS");

                    if (dtPLanesOfer != null && rptPlanes != null)
                    {
                        try
                        {
                            dtPLanesOfer.Columns.Add("Validatarifa", typeof(string));
                            dtPLanesOfer.Columns.Add("Precio", typeof(string));
                            dtPLanesOfer.Columns.Add("idTarifa", typeof(string));
                            foreach (DataRow drp in dtPLanesOfer.Rows)
                            {
                                drp["Validatarifa"] = "1";
                            }
                        }
                        catch { }

                        rptPlanes.DataSource = dtPLanesOfer;
                        rptPlanes.DataBind();



                        foreach (RepeaterItem rp in rptPlanes.Items)
                        {
                            Repeater rptSeguros = (Repeater)rp.FindControl("rptSeguros");
                            Label lblCodigoplan = (Label)rp.FindControl("lblCodigoPlan");
                            DateTime dFechaInicio = Convert.ToDateTime(clsValidaciones.ConverMDYtoYMD(lDatos[1], "/"));
                            DateTime dFechaFinal = Convert.ToDateTime(clsValidaciones.ConverMDYtoYMD(lDatos[2], "/"));
                            int iDiasDif = clsValidaciones.getDiasDiferencia(dFechaInicio.ToString("yyyy/MM/dd"), dFechaFinal.ToString("yyyy/MM/dd"));
                            DataTable dtTarifas = cPlan.ConsultaTarifasCotizadorSeguros(Convert.ToInt32(lblCodigoplan.Text), lDatos[3], clsValidaciones.ConverMDYtoYMD(lDatos[1], "/"), clsValidaciones.ConverMDYtoYMD(lDatos[2], "/"), iDiasDif.ToString());
                            tblTarifasGuardadas = dtTarifas;



                            if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                            {
                                //validamos que las fechas del viaje estene en una misma vigencia, de lo contrario hacemos las validaciones correcpondientes para tomar los valores de 2 vigencias
                                DataTable dtTarifasSecundaria = null;
                                DateTime dtmFechaFinVig = Convert.ToDateTime(dtTarifas.Rows[0]["Hasta"].ToString());
                                DateTime dtmFechaFinal = Convert.ToDateTime(clsValidaciones.ConverMDYtoYMD(lDatos[2], "/"));
                                bool bVigSecundaria = false;
                                int diasDifVigenciaNueva = 0;

                                if (dtmFechaFinal > dtmFechaFinVig)
                                {
                                    bVigSecundaria = true;

                                    diasDifVigenciaNueva = (dtmFechaFinal - dtmFechaFinVig).Days;
                                }
                                //dos vigencias
                                #region [Tabla secundaria]
                                if (bVigSecundaria)
                                {
                                    dtTarifasSecundaria = cPlan.ConsultaTarifasCotizadorSeguros(Convert.ToInt32(lblCodigoplan.Text), lDatos[3], clsValidaciones.ConverMDYtoYMD(lDatos[2], "/"), clsValidaciones.ConverMDYtoYMD((dtmFechaFinVig.AddDays(diasDifVigenciaNueva).ToString("MM/dd/yyyy")).ToString(), "/"), diasDifVigenciaNueva.ToString());

                                    if (dtTarifasSecundaria != null && dtTarifasSecundaria.Rows.Count > 0)
                                    {
                                        //se consultan los impuestos de cada tarifa
                                        dtTarifas = cPlanes.setInsertarImpuestosTarifas(dtTarifas, false, 0, 0, sAplicacion, "");
                                        dtTarifasSecundaria = cPlanes.setInsertarImpuestosTarifas(dtTarifasSecundaria, false, 0, 0, sAplicacion, "");
                                        //se calculan los valores del plan utilizando las 2 vigencias de acuerdo a validaciones establecidas 
                                        dtTarifas = cPlanes.setLlenarTablasRotativos(PageSource, dtTarifas, dtTarifasSecundaria, lDatos[1], lDatos[2]);
                                    }
                                    else
                                    {
                                        dtTarifas = null;
                                    }
                                }
                                #endregion
                                //una vigencia
                                #region [Tabla Unica]
                                else
                                {
                                    //int iNochesTarifa = Convert.ToInt32(dtTarifas.Rows[0]["intOcupacion"].ToString());
                                    int iNochesElegidas = iDiasDif;
                                    //se consultan los impuestos de cada tarifa
                                    dtTarifas = cPlanes.setInsertarImpuestosTarifas(dtTarifas, true, iNochesElegidas, 1, sAplicacion, "");
                                    //se calculan los valores del plan de acuerdo a las noches adicionales necesarias
                                    //dtTarifas =new csTarifasPlanes().setCalcularValoresNochesAdicUnoAUno(dtTarifas, iNochesElegidas);

                                }
                                #endregion
                                //se llena la tabla general de tarifas por tipo de pax y con el numero de la cabina

                                //se relacionan los valores de pasajeros e impuestos segun tipo de habitacion
                                if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                                {
                                    dtTarifas = setInflarTarifasSeguros(dtTarifas, lDatos[4], PageSource);
                                }

                                if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                                {

                                    rptSeguros.DataSource = dtTarifas;
                                    rptSeguros.DataBind();
                                    foreach (DataRow drn in dtPLanesOfer.Rows)
                                    {
                                        if (drn["intCodigo"].ToString().Equals(lblCodigoplan.Text))
                                        {
                                            drn["Precio"] = dtTarifas.Rows[0]["dblTotalImpuestosFormato"].ToString();
                                            drn["idTarifa"] = dtTarifas.Rows[0]["idTarifa"].ToString();
                                            break;
                                        }
                                    }

                                    if (/*a == 0 && */HttpContext.Current.Session["tbltarifas"] == null)
                                    {
                                        tblTarifasTodas = dtTarifas.Copy();
                                    }
                                    else
                                    {
                                        tblTarifasTodas = (DataTable)HttpContext.Current.Session["tbltarifas"];
                                        for (int x = 0; x < dtTarifas.Rows.Count; x++)
                                        {
                                            tblTarifasTodas.Rows.Add(dtTarifas.Rows[x].ItemArray);
                                        }
                                    }
                                    HttpContext.Current.Session["tbltarifas"] = tblTarifasTodas;

                                }

                            }
                            else
                            {
                                foreach (DataRow drp in dtPLanesOfer.Rows)
                                {
                                    if (drp["intcodigo"].ToString().Equals(lblCodigoplan.Text))
                                    {
                                        drp["Validatarifa"] = "0";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (HttpContext.Current.Session["tbltarifas"] == null)
                    {
                        bTodas = false;
                    }
                    dtPLanesOfer.DefaultView.RowFilter = "Validatarifa = '1'";
                    dtPLanesOfer = dtPLanesOfer.DefaultView.ToTable();

                    if (dtPLanesOfer.Rows.Count == 0)
                    {
                        bTodas = false;

                        if (lblerror != null)
                            lblerror.Text = "Lo sentimos no hay Seguros disponibles con las opciones seleccionadas";

                        rptPlanes.DataSource = null;
                        rptPlanes.DataBind();
                    }
                    else
                    {
                        rptPlanes.DataSource = dtPLanesOfer;
                        rptPlanes.DataBind();

                        foreach (RepeaterItem rp in rptPlanes.Items)
                        {
                            Label lblCodigoplan = (Label)rp.FindControl("lblCodigoPlan");
                            DataTable tblTextosPlan = new csConsultasPlanes().ConsultaTextosImagenesPlanes(lblCodigoplan.Text, false);
                            if (tblTextosPlan != null)
                            {
                                Label strRestriccion = (Label)rp.FindControl("strRestriccion");
                                Label strDescripcion = (Label)rp.FindControl("strDescripcion");
                                Label strIncluye = (Label)rp.FindControl("strIncluye");
                                Label strNoIncluye = (Label)rp.FindControl("strNoIncluye");

                                DataTable dt = tblTextosPlan;

                                if (strDescripcion != null)
                                {
                                    dt.DefaultView.RowFilter = "strcodtexto='" + clsValidaciones.GetKeyOrAdd("TipoTextoPlanDesc", "DESC") + "'";
                                    dt = dt.DefaultView.ToTable();
                                    if (dt.Rows.Count > 0)
                                        strDescripcion.Text = System.Web.HttpUtility.HtmlDecode(dt.Rows[0]["strtexto"].ToString());
                                }

                                dt = tblTextosPlan;
                                if (strIncluye != null)
                                {
                                    dt.DefaultView.RowFilter = "strcodtexto='" + clsValidaciones.GetKeyOrAdd("TipoTextoPlanIncluye", "INC") + "'";
                                    dt = dt.DefaultView.ToTable();
                                    if (dt.Rows.Count > 0)
                                        strIncluye.Text = System.Web.HttpUtility.HtmlDecode(dt.Rows[0]["strtexto"].ToString());

                                }

                                dt = tblTextosPlan;
                                if (strNoIncluye != null)
                                {
                                    dt.DefaultView.RowFilter = "strcodtexto='" + clsValidaciones.GetKeyOrAdd("TipoTextoPlanTengaCuenta", "ENCUENTA") + "'";
                                    dt = dt.DefaultView.ToTable();
                                    if (dt.Rows.Count > 0)
                                        strNoIncluye.Text = System.Web.HttpUtility.HtmlDecode(dt.Rows[0]["strtexto"].ToString());

                                }

                                dt = tblTextosPlan;
                                if (strRestriccion != null)
                                {
                                    dt.DefaultView.RowFilter = "strcodtexto='" + clsValidaciones.GetKeyOrAdd("TipoTextoPlanCondiciones", "COND") + "'";
                                    dt = dt.DefaultView.ToTable();
                                    if (dt.Rows.Count > 0)
                                        strRestriccion.Text = System.Web.HttpUtility.HtmlDecode(dt.Rows[0]["strtexto"].ToString());

                                }


                            }
                        }
                    }

                    Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");

                    if (!bTodas)
                    {
                        lblErrorGen.Text = "no se encontraron resultados, por favor cambia los parametros de búsqueda";
                        //rptSeguros.DataSource = null;
                        //rptSeguros.DataBind();

                    }
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "setConsultarTarifaSeguros";
                cParametros.Complemento = "Circular";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }

        #endregion

        public void getAbrirPDFPlan(UserControl PageSource)
        {
            Label lblClasificacion = (Label)PageSource.FindControl("lblClasificacion");
            try
            {
                if (lblClasificacion != null)
                {
                    string sRutaPDF = clsValidaciones.GetKeyOrAdd("sRutaPDFPlanes", "http://162.248.52.194/TuViaje/ssoftcontent/Documentos/PDFPlanes/") + lblClasificacion.Text;
                    PageSource.Response.Write("<script>");
                    PageSource.Response.Write("window.open('" + sRutaPDF + "','_blank')");
                    PageSource.Response.Write("</script>");
                }
            }
            catch
            {
            }
        }
    }
}
