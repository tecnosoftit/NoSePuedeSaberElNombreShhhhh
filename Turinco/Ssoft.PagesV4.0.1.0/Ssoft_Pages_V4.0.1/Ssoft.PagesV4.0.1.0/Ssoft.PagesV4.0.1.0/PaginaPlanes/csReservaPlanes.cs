using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;
using System.Web.UI.WebControls;
using Ssoft.Rules.Reservas;
using System.Data;
using Ssoft.Rules.Generales;
using SsoftQuery.Generales;
using SsoftQuery.Vuelos;
using System.Web;
using SsoftQuery.Reserva;
using Ssoft.ValueObjects;

namespace Ssoft.Pages.PaginaPlanes
{
    public class csReservaPlanes
    {
        private static string sFormatoFecha = clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");
        private static string sCaracterDecimal = clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",");
        private static string sFormatoFechaBD = clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");

        /// <summary>
        /// Metodo principal de llenado de detalle del plan para reserva, se llenan los datos del plan con el DS del carro de compras
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-05
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        public void SetFormularioReservaCotizador(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != null)
                {
                    csGeneralsPag.Idioma(PageSource);
                    clsCache cCache = new csCache().cCache();

                    if (cCache != null)
                    {

                        Label lblTextPrecioSuscripcion = (Label)PageSource.FindControl("lblTextPrecioSuscripcion");
                        Label lblPrecioSuscripcion = (Label)PageSource.FindControl("lblPrecioSuscripcion");
                        HiddenField TotalCarritoSinFormato = (HiddenField)PageSource.FindControl("TotalCarritoSinFormato");
                        Label lblConsecRes = (Label)PageSource.FindControl("lblConsecRes");
                        Label lblLimitePago = (Label)PageSource.FindControl("lblLimitePago");
                        Label lblNombreUsuario = (Label)PageSource.FindControl("lblNombreUsuario");
                        Label lblError = (Label)PageSource.FindControl("lblError");
                        Repeater rptCircuitos = (Repeater)PageSource.FindControl("rptCircuitos");
                        Repeater rptCabinas = (Repeater)PageSource.FindControl("rptCabinas");
                        Label lblTotalCarrito = (Label)PageSource.FindControl("lblTotalCarrito");
                        Label lblMonedaTotal = (Label)PageSource.FindControl("lblMonedaTotal");

                        if (cCache != null)
                        {
                            lblNombreUsuario.Text = cCache.Nombre;
                        }
                        if (PageSource.Request.QueryString["Msj"] != null)
                        {
                            lblError.Text = PageSource.Request.QueryString["Msj"];
                        }
                        /*SELECCIONA EL FORMULARIO QUE VA A CARGAR*/
                        csCarrito csCarCompUnion = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
                        DataSet dsRes = csCarCompUnion.GetDsReservas();
                        DataTable TablaPlanes = dsRes.Tables["CarritoCompras"];
                        DataTable tblPlanReserva = LlenarRepeaterCotizador(TablaPlanes, PageSource.Request.QueryString["TipoPlan"], "");
                        lblConsecRes.Text = tblPlanReserva.Rows[tblPlanReserva.Rows.Count - 1]["intConsecRes"].ToString();
                        lblLimitePago.Text = Convert.ToDateTime(tblPlanReserva.Rows[tblPlanReserva.Rows.Count - 1]["StrFechaVencimiento"].ToString()).ToString("dd MMM yyyy");
                        rptCircuitos.DataSource = tblPlanReserva;
                        rptCircuitos.DataBind();
                        DataTable dtTablaTransac = csCarCompUnion.GetDsReservas().Tables["tblTransac"].Copy();
                        CalcularValorTotalCotizador(dtTablaTransac, lblTotalCarrito, lblMonedaTotal, Convert.ToInt32(tblPlanReserva.Rows[tblPlanReserva.Rows.Count - 1]["intConsecRes"]),
                             tblPlanReserva.Rows[tblPlanReserva.Rows.Count - 1]["strTipoMoneda"].ToString(), TablaPlanes);
                        csCarCompUnion.GuardarDataSetOActualizarDataSet("Reserva" + cCache.SessionID, dsRes);
                        dtTablaTransac = csCarCompUnion.GetDsReservas().Tables["tblTransac"].Copy();
                        InsertarCabinas(PageSource, rptCabinas, Convert.ToInt32(tblPlanReserva.Rows[tblPlanReserva.Rows.Count - 1]["intConsecRes"]),
                            dtTablaTransac, tblPlanReserva.Rows[tblPlanReserva.Rows.Count - 1]["strTipoMoneda"].ToString());

                        RadioButtonList rblFormasPago = (RadioButtonList)PageSource.FindControl("rblFormasPago");

                        if (rblFormasPago != null)
                        {
                            string sCodigoplan = "0";
                            if (HttpContext.Current.Request.QueryString["Codigo"] != null)
                            {
                                sCodigoplan = HttpContext.Current.Request.QueryString["Codigo"].ToString();
                            }
                            else
                            {
                                if (TablaPlanes.Rows.Count > 0)
                                    sCodigoplan = dsRes.Tables["tblreserva"].Rows[0]["intcodigoplan"].ToString();
                            }

                            DataTable tblDetallePlan = (DataTable)PageSource.Session["$TablaDetallePlan"];

                            DataTable dtFOP = new DataTable();
                            if (tblDetallePlan != null && tblDetallePlan.Rows.Count > 0)
                                dtFOP = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFormasPagoPlan", new string[3] { tblDetallePlan.Rows[0]["intEmpresa"].ToString(), cCache.Idioma, sCodigoplan });
                            else
                                dtFOP = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFormasPagoPlan", new string[3] { cCache.Empresa, cCache.Idioma, sCodigoplan });

                            rblFormasPago.DataSource = dtFOP;
                            rblFormasPago.DataTextField = "strdescripcion";
                            rblFormasPago.DataValueField = "strCodigo";
                            rblFormasPago.DataBind();

                            if (rblFormasPago.Items.Count > 0)
                            {
                                for (int i = 0; i < rblFormasPago.Items.Count; i++)
                                {
                                    rblFormasPago.Items[i].Attributes.Add("onclick", "javascript:ActivarDivFP(this)");
                                }
                            }

                            rblFormasPago.SelectedValue = clsValidaciones.GetKeyOrAdd("Efectivo", "EFE");
                        }

                        //string bvalida = new CsConsultasVuelos().ConsultaCodigo(new csCache().cCache().Email, "tblUsuarios", "bitSuscriptor", "strEmail");
                        //if (bvalida.ToUpper().Equals("FALSE"))
                        //{
                        //    lblTextPrecioSuscripcion.Text = clsValidaciones.GetKeyOrAdd("NombreSuscripcion", "ValorSubscripcion");
                        //    lblPrecioSuscripcion.Text = clsValidaciones.GetKeyOrAdd("ValorSubscripcion", "99000");

                        //    lblTotalCarrito.Text = (Convert.ToDecimal(lblTotalCarrito.Text) + Convert.ToDecimal(lblPrecioSuscripcion.Text)).ToString();
                        //    TotalCarritoSinFormato.Value = lblTotalCarrito.Text;

                        //}
                    }
                    else
                    {
                        csGeneralsPag.FinSesion();
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
                cParametros.Complemento = "SetFormularioReservaCotizador";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// metodo que retorna la tabla para llenado del repetidos de detalle del plan
        /// </summary>
        /// <param name="TablaPrincipal"></param>
        /// <param name="Tipoplan"></param>
        /// <param name="SubtipoPlan"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-05
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        public DataTable LlenarRepeaterCotizador(DataTable TablaPrincipal, string Tipoplan, string SubtipoPlan)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("StrNombrePlan");
            tbl.Columns.Add("StrCiudad");
            tbl.Columns.Add("StrPasajeros");
            tbl.Columns.Add("StrDescripcion");
            tbl.Columns.Add("IndiceTablaPrincipal");
            tbl.Columns.Add("IntValorTotal");
            tbl.Columns.Add("StrFechaInicial");
            tbl.Columns.Add("StrFechaFinal");
            tbl.Columns.Add("StrTipoMoneda");
            tbl.Columns.Add("IntNumeroDias");
            tbl.Columns.Add("IntNumeroNoches");
            tbl.Columns.Add("StrCategoria");
            tbl.Columns.Add("StrAcomodacion");
            tbl.Columns.Add("StrTipoHabitacion");
            tbl.Columns.Add("StrSubTipoPlan");
            tbl.Columns.Add("PosTablaPrincipal");
            tbl.Columns.Add("IntNochesAdicionales");
            tbl.Columns.Add("StrObservacion");
            tbl.Columns.Add("StrDuracion");
            tbl.Columns.Add("IntConsecRes");
            tbl.Columns.Add("StrFechaVencimiento");
            tbl.Columns.Add("StrZonaGeografica");

            int i = 0;
            int j = 0;
            while (i < TablaPrincipal.Rows.Count)
            {
                if (TablaPrincipal.Rows[i]["StrTipoPlan"].ToString().Equals(Tipoplan) && (TablaPrincipal.Rows[i]["StrReserva"].ToString().Equals("") || TablaPrincipal.Rows[i]["StrReserva"].ToString().Equals("0")))
                {
                    DataRow drFila = tbl.NewRow();
                    tbl.Rows.Add(drFila);
                    tbl.AcceptChanges();

                    tbl.Rows[j]["StrNombrePlan"] = TablaPrincipal.Rows[i]["StrNombrePlan"].ToString();
                    tbl.Rows[j]["StrCiudad"] = TablaPrincipal.Rows[i]["StrCiudad"].ToString();
                    tbl.Rows[j]["StrPasajeros"] = TablaPrincipal.Rows[i]["StrPasajeros"].ToString();
                    tbl.Rows[j]["StrDescripcion"] = TablaPrincipal.Rows[i]["StrDescripcion"].ToString();
                    tbl.Rows[j]["IndiceTablaPrincipal"] = i.ToString();
                    tbl.Rows[j]["IntValorTotal"] = Convert.ToDecimal(TablaPrincipal.Rows[i]["IntValorTotal"].ToString()).ToString("###,###.##");
                    tbl.Rows[j]["StrFechaInicial"] = TablaPrincipal.Rows[i]["StrFechaInicial"].ToString();
                    tbl.Rows[j]["StrFechaFinal"] = TablaPrincipal.Rows[i]["StrFechaFinal"].ToString();
                    tbl.Rows[j]["StrTipoMoneda"] = TablaPrincipal.Rows[i]["StrTipoMoneda"].ToString();
                    tbl.Rows[j]["IntNumeroDias"] = TablaPrincipal.Rows[i]["IntNumeroDias"].ToString();
                    tbl.Rows[j]["IntNumeroNoches"] = TablaPrincipal.Rows[i]["IntNumeroNoches"].ToString();
                    tbl.Rows[j]["StrCategoria"] = TablaPrincipal.Rows[i]["StrCategoria"].ToString();
                    tbl.Rows[j]["StrAcomodacion"] = TablaPrincipal.Rows[i]["StrAcomodacion"].ToString();
                    tbl.Rows[j]["StrTipoHabitacion"] = TablaPrincipal.Rows[i]["StrTipoHabitacion"].ToString();
                    tbl.Rows[j]["StrSubTipoPlan"] = TablaPrincipal.Rows[i]["StrSubTipoPlan"].ToString();
                    tbl.Rows[j]["PosTablaPrincipal"] = i.ToString();
                    tbl.Rows[j]["IntNochesAdicionales"] = TablaPrincipal.Rows[i]["IntNochesAdicionales"].ToString();
                    tbl.Rows[j]["StrObservacion"] = TablaPrincipal.Rows[i]["StrObservacion"].ToString();
                    tbl.Rows[j]["StrDuracion"] = TablaPrincipal.Rows[i]["StrDuracion"].ToString();
                    tbl.Rows[j]["IntConsecRes"] = TablaPrincipal.Rows[i]["IntConsecRes"].ToString();
                    tbl.Rows[j]["StrFechaVencimiento"] = TablaPrincipal.Rows[i]["StrFechaVencimiento"].ToString();
                    tbl.Rows[j]["StrZonaGeografica"] = TablaPrincipal.Rows[i]["StrZonaGeografica"].ToString();
                    j++;
                }
                i++;
            }
            return tbl;
        }

        /// <summary>
        /// metodo que retorna el valor total de la reserva
        /// </summary>
        /// <param name="tblTransac"></param>
        /// <param name="lblTotalCarrito"></param>
        /// <param name="lblMonedaTotal"></param>
        /// <param name="iConsecRes"></param>
        /// <param name="sTipoMoneda"></param>
        /// <param name="tblPricipal"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-05
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        protected void CalcularValorTotalCotizador(DataTable tblTransac, Label lblTotalCarrito, Label lblMonedaTotal, int iConsecRes,
            string sTipoMoneda, DataTable tblPricipal)
        {
            decimal Total = 0;
            int i = 0;
            DataTable tblTRansacFiltrada = tblTransac.Clone();
            clsCache cCache = new csCache().cCache();
            for (int x = 0; x < tblTransac.Rows.Count; x++)
            {
                if (tblTransac.Rows[x]["intConsecRes"].ToString().Equals(iConsecRes.ToString()))
                {
                    tblTRansacFiltrada.Rows.Add(tblTransac.Rows[x].ItemArray);
                }
            }
            while (i < tblTRansacFiltrada.Rows.Count)
            {
                Total = Total + decimal.Parse(tblTRansacFiltrada.Rows[i]["dblValor"].ToString());
                i++;
            }
            if (tblTRansacFiltrada.Rows.Count > 0)
            {
                for (int x = 0; x < tblPricipal.Rows.Count; x++)
                {
                    if (tblPricipal.Rows[x]["intConsecRes"].ToString().Equals(iConsecRes.ToString()))
                    {
                        if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                            tblPricipal.Rows[x]["IntValorTotal"] = Convert.ToInt32(Total);
                        else
                            tblPricipal.Rows[x]["IntValorTotal"] = Total.ToString();
                    }
                }
                if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                    lblTotalCarrito.Text = Convert.ToDecimal(Total.ToString()).ToString("###,###.##");
                else
                    lblTotalCarrito.Text = Convert.ToDecimal(Total.ToString()).ToString("###,##0.00");

                lblMonedaTotal.Text = sTipoMoneda;
            }
            else
            {
                lblTotalCarrito.Text = "0";
                lblMonedaTotal.Text = "";
            }
        }

        /// <summary>
        /// Metodo de llenado del repetidor de cabinas
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="rptCabinas"></param>
        /// <param name="iConsec"></param>
        /// <param name="tblTransac"></param>
        /// <param name="sTipoMoneda"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-05
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        protected void InsertarCabinas(UserControl PageSource, Repeater rptCabinas, int iConsec, DataTable tblTransac, string sTipoMoneda)
        {
            if (PageSource.Request.QueryString["TipoPlan"] != null)
            {
                tblTransac.Columns.Add("strTipoMoneda");
                DataTable tblTRansacFiltrada = tblTransac.Clone();
                clsCache cCache = new csCache().cCache();
                for (int i = 0; i < tblTransac.Rows.Count; i++)
                {
                    if (tblTransac.Rows[i]["intConsecRes"].ToString().Equals(iConsec.ToString()))
                    {
                        tblTransac.Rows[i]["strTipoMoneda"] = sTipoMoneda;
                        tblTRansacFiltrada.Rows.Add(tblTransac.Rows[i].ItemArray);
                    }
                }
                tblTRansacFiltrada.Columns.Add("Cabina");
                for (int i = 0; i < tblTRansacFiltrada.Rows.Count; i++)
                {
                    if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE")))
                    {
                        tblTRansacFiltrada.Rows[i]["Cabina"] = "Cabina " + Convert.ToString(i + 1);
                    }
                    else
                    {
                        if (!PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTarjetas", "TJAS")))
                            tblTRansacFiltrada.Rows[i]["Cabina"] = "Habitación " + Convert.ToString(i + 1);
                    }
                }
                rptCabinas.DataSource = tblTRansacFiltrada;
                rptCabinas.DataBind();
                //setLlenarCombosIdiomaServicio(rptCabinas);
                InsertarPasajerosCotizador(rptCabinas, tblTRansacFiltrada, PageSource);
            }
        }

        /// <summary>
        /// Metodo de llenado del repetidor de pasajeros por cabina
        /// </summary>
        /// <param name="rptCabinas"></param>
        /// <param name="tblCabinas"></param>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-05
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        protected void InsertarPasajerosCotizador(Repeater rptCabinas, DataTable tblCabinas, UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                Label lblConsecRes = (Label)PageSource.FindControl("lblConsecRes");
                csCarrito csCarCompUnion = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
                DataTable dtTablaPax = csCarCompUnion.GetDsReservas().Tables["tblPax"].Copy();

                for (int i = 0; i < tblCabinas.Rows.Count; i++)
                {
                    DataTable tblPaxFiltrada = dtTablaPax.Clone();
                    tblPaxFiltrada.Columns.Add("strTipoServicio");

                    for (int x = 0; x < dtTablaPax.Rows.Count; x++)
                    {
                        if (dtTablaPax.Rows[x]["IntConsecRes"].ToString().Equals(lblConsecRes.Text) && dtTablaPax.Rows[x]["IntSegmento"].ToString().Equals(Convert.ToString(i + 1)))
                        {
                            tblPaxFiltrada.Rows.Add(dtTablaPax.Rows[x].ItemArray);
                            tblPaxFiltrada.Rows[tblPaxFiltrada.Rows.Count - 1]["strTipoServicio"] = tblCabinas.Rows[i]["strTipoAcomodacion"].ToString();
                        }
                    }
                    tblPaxFiltrada = setInsertarImpuestosPax(tblPaxFiltrada, tblCabinas.Rows[i]["intTipoHabitacion"].ToString(),
                        tblCabinas.Rows[i]["intTipoAcomodacion"].ToString(), tblCabinas.Rows[i]["intIdVigencia"].ToString(), PageSource);
                    Repeater rptPasajeros = (Repeater)rptCabinas.Items[i].FindControl("rptPasajeros");
                    rptPasajeros.DataSource = tblPaxFiltrada;
                    rptPasajeros.DataBind();
                    setLlenarCombosPax(rptPasajeros);
                    //setDatosPaxUsuarioReserva(rptPasajeros, cCache);
                    InsertarFechaNacPax(rptPasajeros, PageSource);
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
                cParametros.Complemento = "InsertarPasajerosCotizador";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo de llenado de impuestos de pasajeros
        /// </summary>
        /// <param name="dtPax"></param>
        /// <param name="sTipoHab"></param>
        /// <param name="sTipoAcom"></param>
        /// <param name="sVigencia"></param>
        /// <param name="Pagesource"></param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-05
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        protected DataTable setInsertarImpuestosPax(DataTable dtPax, string sTipoHab, string sTipoAcom, string sVigencia, UserControl Pagesource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                decimal dValorConversionMonedaUnica = 1;
                try
                {
                    dValorConversionMonedaUnica = (decimal)Pagesource.Session["dValorConversionMonedaUnica"];
                }
                catch { }
                DataTable tblTarifasPax = new DataTable();
                /*vareficamos que existan las columnas.*/
                try
                {
                    tblTarifasPax = ((DataTable)Pagesource.Session["tbltarifasgeneral"]).Copy();
                }
                catch { }
                if (!dtPax.Columns.Contains("strHtmlImpuestos"))
                    dtPax.Columns.Add("strHtmlImpuestos");
                if (!tblTarifasPax.Columns.Contains("Seleccionado"))
                    tblTarifasPax.Columns.Add("Seleccionado", typeof(Boolean)).ReadOnly = false;
                if (!tblTarifasPax.Columns.Contains("Cantidad"))
                    tblTarifasPax.Columns.Add("Cantidad", typeof(int)).ReadOnly = false;
                for (int i = 0; i < dtPax.Rows.Count; i++)
                {
                    for (int x = 0; x < tblTarifasPax.Rows.Count; x++)
                    {
                        if (dtPax.Rows[i]["intTipoPax"].ToString().Equals(tblTarifasPax.Rows[x]["TipoPax"].ToString()) &&
                            sTipoHab.Equals(tblTarifasPax.Rows[x]["TipoHabitacion"].ToString()) &&
                            sTipoAcom.Equals(tblTarifasPax.Rows[x]["TipoAcomodacion"].ToString()) &&
                            sVigencia.Equals(tblTarifasPax.Rows[x]["IdVigencia"].ToString()))
                        {
                            decimal dblValorBase = Convert.ToDecimal(tblTarifasPax.Rows[x]["Tarifa"]) * dValorConversionMonedaUnica;
                            DataTable tblImp = (DataTable)tblTarifasPax.Rows[x]["tImpuestos"];
                            StringBuilder sbHtml = new StringBuilder();
                            decimal dValorImp = 0;
                            if (tblImp != null)
                            {
                                /*marcamos la tarifa seleccionada y la cantidad.*/
                                try
                                {
                                    int cantidad = 0;
                                    int.TryParse(tblTarifasPax.Rows[x]["Cantidad"].ToString(), out cantidad);
                                    tblTarifasPax.Rows[x]["Seleccionado"] = true;
                                    if (cantidad == 0)
                                        tblTarifasPax.Rows[x]["Cantidad"] = 1;
                                    else
                                        tblTarifasPax.Rows[x]["Cantidad"] = (cantidad + 1);

                                }
                                catch (Exception) { }
                                sbHtml.AppendLine("<Table width='100%'>");
                                sbHtml.AppendLine("<tr>");
                                sbHtml.AppendLine("<td width='100%' colspan='2'><strong><italic>Detalle de impuestos:</italic></strong></td>");
                                sbHtml.AppendLine("</tr>");
                                if (tblImp.Rows.Count > 0)
                                {
                                    for (int y = 0; y < tblImp.Rows.Count; y++)
                                    {
                                        decimal dValorImpConver = 0;
                                        sbHtml.AppendLine("<tr>");
                                        if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                                        {
                                            dValorImpConver = Convert.ToDecimal(Convert.ToInt32(tblImp.Rows[y]["valor"])) * dValorConversionMonedaUnica;
                                            sbHtml.AppendLine("<td width='80%'><strong>" + tblImp.Rows[y]["strImp"].ToString() + "</strong></td><td>" + dValorImpConver.ToString("###,###,###") + "</td>");
                                        }
                                        else
                                        {
                                            dValorImpConver = Convert.ToDecimal(tblImp.Rows[y]["valor"]) * dValorConversionMonedaUnica;
                                            sbHtml.AppendLine("<td width='80%'><strong>" + tblImp.Rows[y]["strImp"].ToString() + "</strong></td><td>" + dValorImpConver.ToString("###,##0.00") + "</td>");
                                        }
                                        sbHtml.AppendLine("</tr>");
                                        dValorImp += dValorImpConver;
                                    }
                                }
                                else
                                {
                                    sbHtml.AppendLine("<tr>");
                                    sbHtml.AppendLine("<td width='100%'><strong>Todos los impuestos incluidos en la tarifa</strong></td>");
                                    sbHtml.AppendLine("</tr>");
                                }
                                sbHtml.AppendLine("</Table>");
                            }
                            else
                            {
                                sbHtml.AppendLine("<Table width='100%'>");
                                sbHtml.AppendLine("<tr>");
                                sbHtml.AppendLine("<td width='100%'><strong>Todos los impuestos incluidos en la tarifa</strong></td>");
                                sbHtml.AppendLine("</tr>");
                                sbHtml.AppendLine("</Table>");
                            }
                            dtPax.Rows[i]["strHtmlImpuestos"] = sbHtml.ToString();
                            decimal dValorTotal = dblValorBase + dValorImp;
                            string sValorstr = "";
                            string sTarifastr = "";
                            if (clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP").Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP")))
                            {
                                sValorstr = dValorTotal.ToString("###,###,###");
                                sTarifastr = dblValorBase.ToString("###,###,###");
                            }
                            else
                            {
                                sValorstr = dValorTotal.ToString("###,##0.00");
                                sTarifastr = dblValorBase.ToString("###,###,###");
                            }

                            //dtPax.Rows[i]["strValor"] = sValorstr;
                            //dtPax.Rows[i]["strTarifa"] = sTarifastr;
                        }

                    }
                }
                try { Pagesource.Session["tbltarifasgeneral"] = tblTarifasPax; }
                catch (Exception) { }
                return dtPax;
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "setInsertarImpuestosPax (detalle impuestos reserva), se devuelve tabla sin impuestos";
                ExceptionHandled.Publicar(cParametros);
                return dtPax;
            }
        }

        /// <summary>
        /// metodo de llenado de los combos de parametrizacion de pasajeros
        /// </summary>
        /// <param name="rptPax"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-05
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        public void setLlenarCombosPax(Repeater rptPax)
        {
            csConsultasGenerales cConsGen = new csConsultasGenerales();
            csGenerales cGen = new csGenerales();
            for (int i = 0; i < rptPax.Items.Count; i++)
            {
                DropDownList ddlGenero = (DropDownList)rptPax.Items[i].FindControl("ddlGenero");
                DropDownList ddlTipoIdent = (DropDownList)rptPax.Items[i].FindControl("ddlTipoIdent");
                DropDownList ddlTipoPax = (DropDownList)rptPax.Items[i].FindControl("ddlTipoPax");

                DataTable dtResul = cConsGen.ConReferenciaSexo();
                if (dtResul != null)
                    cGen.LlenarControlData(ddlGenero, Enum_Controls.DropDownList, "intCode", "strDescripcion", true, false, null, dtResul);

                dtResul = cConsGen.ConReferenciaTiposIdentificacion();
                if (dtResul != null)
                    cGen.LlenarControlData(ddlTipoIdent, Enum_Controls.DropDownList, "intCode", "strDescripcion", true, false, null, dtResul);

                dtResul = cConsGen.ConReferenciaTiposPax("");
                if (dtResul != null)
                    cGen.LlenarControlData(ddlTipoPax, Enum_Controls.DropDownList, "intCode", "strDescripcion", true, false, null, dtResul);
            }
        }

        /// <summary>
        /// Metodo que llena los datos del usuario que esta generando la reserva
        /// </summary>
        /// <param name="rptPasajeros"></param>
        /// <param name="cCache"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-05
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        protected void setDatosPaxUsuarioReserva(Repeater rptPasajeros, clsCache cCache)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                if (rptPasajeros.Items.Count > 0)
                {
                    TextBox txtNombre = (TextBox)rptPasajeros.Items[0].FindControl("txtNombre");
                    TextBox txtApellido = (TextBox)rptPasajeros.Items[0].FindControl("txtApellido");
                    DropDownList ddlGenero = (DropDownList)rptPasajeros.Items[0].FindControl("ddlGenero");
                    DropDownList ddlTipoIdent = (DropDownList)rptPasajeros.Items[0].FindControl("ddlTipoIdent");
                    TextBox txtNacionalidad = (TextBox)rptPasajeros.Items[0].FindControl("txtNacionalidad");
                    TextBox txtEdad = (TextBox)rptPasajeros.Items[0].FindControl("txtEdad");
                    TextBox txtPasaporte = (TextBox)rptPasajeros.Items[0].FindControl("txtPasaporte");
                    TextBox txtPasaporteFecha = (TextBox)rptPasajeros.Items[0].FindControl("txtPasaporteFecha");
                    TextBox txtNacimiento = (TextBox)rptPasajeros.Items[0].FindControl("txtNacimiento");
                    TextBox txtPaisResidencia = (TextBox)rptPasajeros.Items[0].FindControl("txtPaisResidencia");

                    if (clsValidaciones.GetKeyOrAdd("RegistroFormReserva", "False").ToUpper() != "TRUE")
                    {
                        csConsultasGenerales cGen = new csConsultasGenerales();
                        DataTable tblUser = cGen.ConsultaUsuario(cCache.Contacto);
                        if (tblUser != null)
                        {
                            if (txtNombre != null)
                                txtNombre.Text = tblUser.Rows[0]["strNombre"].ToString();// + " " + tblUser.Rows[0]["strNombre"].ToString();

                            if (txtApellido != null)
                                txtApellido.Text = tblUser.Rows[0]["strApellido"].ToString();

                            if (ddlGenero != null)
                                ddlGenero.SelectedValue = tblUser.Rows[0]["intgenero"].ToString();

                            if (ddlTipoIdent != null)
                                ddlTipoIdent.SelectedValue = tblUser.Rows[0]["inttipoident"].ToString();

                            if (txtNacimiento != null)
                                txtNacimiento.Text = Convert.ToDateTime(tblUser.Rows[0]["dtmfechanac"].ToString()).ToString(sFormatoFecha);

                            //if (txtPaisResidencia != null)
                            //    txtPaisResidencia.Text = cCache.Pais;

                            if (txtPasaporte != null)
                                txtPasaporte.Text = tblUser.Rows[0]["strIdentificacion"].ToString();
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
                cParametros.Complemento = "setDatosPaxUsuarioReserva";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo que inserta la fecha de nacimientode pasajeros para reserva
        /// </summary>
        /// <param name="rptPasajeros"></param>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-05
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        protected void InsertarFechaNacPax(Repeater rptPasajeros, UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                if (PageSource.Request.QueryString["TipoPlan"] != clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC") &&
                    PageSource.Request.QueryString["TipoPlan"] != clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS"))
                {
                    clsCache cCache = new csCache().cCache();
                    string[] sFechas = cCache.DatosAdicionales[4].Split(',');
                    if (sFechas.Length == rptPasajeros.Items.Count)
                    {
                        for (int i = 0; i < rptPasajeros.Items.Count; i++)
                        {
                            TextBox txtNacimiento = (TextBox)rptPasajeros.Items[i].FindControl("txtNacimiento");
                            if (txtNacimiento != null)
                            {
                                if (sFechas[i].Length >= 8)
                                    txtNacimiento.Text = sFechas[i];
                            }
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
                cParametros.Complemento = "InsertarFechaNacPax";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        public void setCondiciones(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    if (!PageSource.Page.IsPostBack)
                    {
                        /*obtenemos la aplicacion que pertenece el plan.*/
                        Repeater rptCondicionesTraslados = (Repeater)PageSource.FindControl("rptCondicionesTraslados");
                        csCarrito csCarCompUnion = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
                        DataTable TablaPlanes = csCarCompUnion.RecuperarTabla();

                        DataTable tblTextosPlanes = (DataTable)PageSource.Session["$TablaTextosPlanes"];
                        if (tblTextosPlanes != null && tblTextosPlanes.Rows.Count > 0)
                        {
                            csTarifasPlanes ctar = new csTarifasPlanes();
                            string sCondiciones = ctar.GetTextosPlan(clsValidaciones.GetKeyOrAdd("TipoTextoPlanCondiciones", "COND"), tblTextosPlanes);
                            /*SE AGREGAN LAS CONDICIONES DEL PLAN*/
                            TablaPlanes.Rows[0]["strRestricciones"] = sCondiciones;
                        }
                        rptCondicionesTraslados.DataSource = TablaPlanes;
                        rptCondicionesTraslados.DataBind();
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
                cParametros.Complemento = "setCondiciones";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        public clsParametros setCrearNoRegistro(UserControl parent, UserControl PageSource, Enum_Login eLigin, bool bFacturacion)
        {
            string sIdioma = clsSesiones.getIdioma();
            ExceptionHandled.Publicar("*************************////////////////////////////-- INICIO DE LA CREACION DEL USUARIO & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");
            clsParametros cParametros = new clsParametros();
            csResultadoVuelos cVuelos = new csResultadoVuelos();

            //UpdatePanel upError = PageSource.FindControl("upError") as UpdatePanel;
            HiddenField hdfContactofactura = (HiddenField)PageSource.FindControl("hdfContactofactura");
            Label lblError1 = parent.FindControl("lblError") as Label;
            if (lblError1 == null)
                lblError1 = PageSource.FindControl("lblError1") as Label;

            Repeater rptPasajeros = null;
            Repeater rptCabinas = (Repeater)parent.FindControl("rptCabinas");
            if (rptCabinas != null && rptCabinas.Items.Count > 0)
                rptPasajeros = (Repeater)rptCabinas.Items[0].FindControl("rptPasajeros");

            TextBox txtCiudad = (TextBox)PageSource.FindControl("txtCiudad");
            TextBox txtclaveConfir = (TextBox)PageSource.FindControl("txtclaveConfir");

            TextBox txtApellido = null;
            TextBox txtNombre = null;
            DropDownList ddlGenero = null;
            TextBox txtNacimiento = null;
            TextBox txtPasaporte = null;
            DropDownList ddlTipoIdent = null;
            if (rptPasajeros != null && rptPasajeros.Items.Count > 0)
            {
                txtApellido = (TextBox)rptPasajeros.Items[0].FindControl("txtApellido");
                txtNombre = (TextBox)rptPasajeros.Items[0].FindControl("txtNombre");
                ddlGenero = (DropDownList)rptPasajeros.Items[0].FindControl("ddlGenero");
                txtNacimiento = (TextBox)rptPasajeros.Items[0].FindControl("txtNacimiento");
                txtPasaporte = (TextBox)rptPasajeros.Items[0].FindControl("txtPasaporte");
                ddlTipoIdent = (DropDownList)rptPasajeros.Items[0].FindControl("ddlTipoIdent");
            }
            else
            {
                //Juan Camilo
                //-- validacion para campos de hoteles internacionales
                if (PageSource.ToString().ToUpper().Contains("INTERNAL"))
                {
                    txtApellido = (TextBox)parent.FindControl("txtApellido1");
                    txtNombre = (TextBox)parent.FindControl("txtNombre1");
                }
                else
                {
                    txtApellido = (TextBox)PageSource.FindControl("txtApellido");
                    txtNombre = (TextBox)PageSource.FindControl("txtNombre");
                }
            }

            TextBox txtMailPersonal = (TextBox)PageSource.FindControl("txtMailPersonal");
            TextBox txtTelefono = (TextBox)PageSource.FindControl("txtTelefono");
            TextBox txtClave = (TextBox)PageSource.FindControl("txtClave");
            TextBox txtCelular = (TextBox)PageSource.FindControl("txtCelular");
            TextBox txtDireccion = (TextBox)PageSource.FindControl("txtDireccion");
            //TextBox txtDocumento = (TextBox)PageSource.FindControl("txtDocumento");
            DropDownList ddlPais = PageSource.FindControl("ddlPais") as DropDownList;
            //DropDownList ddlTipoIdentificaion = PageSource.FindControl("ddlTipoIdentificaion") as DropDownList;
            CheckBox chkCondicionesRegistro = PageSource.FindControl("chkCondicionesRegistro") as CheckBox;
            Label lblIdUsuario = PageSource.FindControl("lblIdUsuario") as Label;
            Label lblTelf = PageSource.FindControl("lblTelf") as Label;
            Label lblCiudadR = PageSource.FindControl("lblCiudadR") as Label;
            Label lblCiudadR1 = PageSource.FindControl("lblCiudadR1") as Label;
            Label lblTCelular = PageSource.FindControl("lblTCelular") as Label;
            Label lblTCelular1 = PageSource.FindControl("lblTCelular1") as Label;
            bool Enviacorreo = true;

            cParametros.Id = 1;

            try
            {
                bool fail = false;

                if (/*txtNombre.Text.Trim().Equals("") || txtApellido.Text.Trim().Equals("") || */txtTelefono.Text.Trim().Equals("") ||
                txtCiudad.Text.Trim().Equals("") || txtMailPersonal.Text.Trim().Equals("") || txtCelular.Text.Trim().Equals(""))
                {
                    fail = true;
                }
                if (fail)
                {
                    lblError1.Text = "Por favor diligencia todos los campos marcados como obligatorios (*)";
                    cParametros.Id = 0;
                }
                else
                {
                    string Empresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "0");
                    try
                    {
                        clsCache cCache = new csCache().cCache();
                        if (cCache != null)
                            Empresa = cCache.Empresa;
                    }
                    catch (Exception)
                    {
                    }
                    clsLogin cLogin = new clsLogin();
                    DataSet dsLogin = new DataSet();
                    lblError1.Text = string.Empty;
                    csGenerales Generales = new csGenerales();

                    DataTable dt = new CsConsultasVuelos().SPConsultaTabla("SPValidaUsuarioFinal", new string[4] { txtMailPersonal.Text, clsSesiones.getAplicacion().ToString(), Empresa, clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF") });

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cParametros = cVuelos.setLogin(txtMailPersonal.Text, dt.Rows[0]["strPassword"].ToString(), false, PageSource, eLigin, true);
                    }
                    else
                    {
                        ExceptionHandled.Publicar("*************************////////////////////////////-- USUARIO NO EXISTENTE, SE CREARA, MAIL: " + txtMailPersonal.Text + " & SESION: " + clsSesiones.getSesionID() + "  --////////////////////////");

                        /*Validamos que exista la llave idEmpresa para identificar si es una aplicacion corporaivo o cliente final*/
                        string sDocumento = "";
                        string sDireccion = "";
                        string sPassword = "";
                        if (txtPasaporte != null)
                            sDocumento = txtPasaporte.Text;
                        if (txtDireccion != null)
                            sDireccion = txtDireccion.Text;

                        if (txtClave != null && txtClave.Text != "")
                        {
                            sPassword = txtClave.Text;
                        }
                        else
                        {
                            sPassword = clsValidaciones.GetKeyOrAdd("ClaveDefectoUsuario", "Tutiquete2012");
                            if (txtClave != null)
                                txtClave.Text = sPassword;
                        }
                        string StrNivel = new CsConsultasVuelos().ConsultaCodigo(clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF"), "TBLNIVELUSUARIOS", "INTCODE", "REFERETIPOUSUARIO");
                        string StrCiudad = new CsConsultasVuelos().EjecutaProcedimiento("SPConsultaCiudadNombre", new string[2] { "'" + txtCiudad.Text.Trim() + "'", "'" + sIdioma + "'" });
                        if (StrCiudad == "")
                        {
                            StrCiudad = "1";
                        }

                        string bit = new CsConsultasVuelos().EjecutarSPConsulta("SPCreausuario", new string[17] { clsSesiones.getAplicacion().ToString(), StrNivel, 
                                Empresa, "0", ddlTipoIdent.SelectedValue, "'" + sDocumento + "'", "'" + txtNombre.Text + "'", 
                                "'" + txtApellido.Text + "'", ddlGenero.SelectedItem.Value, "'" + txtNacimiento.Text + "'", "'" + sDireccion + "'", StrCiudad, 
                                "'" + txtTelefono.Text.Trim() + "'", "'" + txtCelular.Text.Trim() + "'", "'" + txtMailPersonal.Text + "'", "'" + txtClave.Text + "'", "0" });
                        cParametros.Id = Convert.ToInt32(bit);

                        if (cParametros.Id.Equals(1))
                        {
                            switch (eLigin)
                            {
                                case Enum_Login.LoginGen:
                                    break;
                                case Enum_Login.LoginCarro:
                                    lblError1.Text = "El usuario de facturacion fue creado satisfactoriamente!";
                                    break;
                                case Enum_Login.LoginCorp:
                                    break;
                                case Enum_Login.LoginMayorista:
                                    //cContactos.CrearUsuarioMayorista(ptblContactos, 0);
                                    break;
                                case Enum_Login.LoginDefault:
                                    break;
                                case Enum_Login.LoginConcurso:
                                    break;
                                case Enum_Login.LoginAtenea:
                                    if (ddlTipoIdent != null)
                                    {
                                        /*verificamos el usuario de atenea*/
                                        //SetVerificarUsuarioAtenea(ptblContactos.strIdentificacion.Value, ddlTipoIdentificaion.SelectedItem.Value);
                                    }
                                    break;
                                default:
                                    break;
                            }

                            if (eLigin != Enum_Login.LoginCarro)
                            {
                                if (PageSource.Session["enviacorreo"] != null)
                                {
                                    Enviacorreo = bool.Parse(PageSource.Session["enviacorreo"].ToString());
                                    PageSource.Session["enviacorreo"] = null;

                                }

                                cParametros = cVuelos.setLogin(txtMailPersonal.Text, txtClave.Text, Enviacorreo, PageSource, eLigin, true);
                            }
                        }
                        else
                        {
                            if (cParametros.ViewMessage.Count > 0)
                            {
                                lblError1.Text = cParametros.ViewMessage[0];
                                if (cParametros.Sugerencia.Count > 0)
                                    lblError1.Text += ". " + cParametros.Sugerencia[0];
                            }
                            else
                            {
                                lblError1.Text = "Ya existe un usuario registrado con ese e-mail";
                            }
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
                cParametros.Complemento = "setCrearNoRegistro; no se pudo crear o consultar el usuario";
                ExceptionHandled.Publicar(cParametros);
            }

            ExceptionHandled.Publicar("*************************////////////////////////////-- FIN DE LA CREACION DEL USUARIO & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");
            return cParametros;
        }

        /// <summary>
        /// Metodo general de reserva de planes
        /// </summary>
        /// <param name="sender">Boton</param>
        /// <param name="e">Evento</param>
        /// <param name="PageSource">Usercontrol</param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-05
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// </remarks>
        public void btnReservarCotizador_Click(object sender, EventArgs e, UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();

                if (cCache != null)
                {
                    TextBox txtcodigoascesor = (TextBox)PageSource.FindControl("txtcodigoascesor");
                    HiddenField hRecord = (HiddenField)PageSource.FindControl("strRecord");
                    HiddenField TotalCarritoSinFormato = (HiddenField)PageSource.FindControl("TotalCarritoSinFormato");
                    Label lblRecord = (Label)PageSource.FindControl("lblRecord");
                    Label lblError = (Label)PageSource.FindControl("lblError");
                    Repeater rptCabinas = (Repeater)PageSource.FindControl("rptCabinas");
                    Repeater rptCircuitos = (Repeater)PageSource.FindControl("rptCircuitos");
                    UserControl UcVentanaConfirmacion1 = (UserControl)PageSource.FindControl("UcVentanaConfirmacion1");
                    bool Valida;
                    bool Valida2;

                    Valida = ValidarPaxVacios(rptCabinas);
                    Valida2 = bValidaFechas(PageSource, rptCabinas);

                    if (Valida)
                    {
                        lblError.Text = "Please fill all fields marked as required";
                    }
                    else
                    {
                        if (!Valida2)
                        {

                        }
                        else
                        {

                            if (txtcodigoascesor != null)
                            {
                                if (txtcodigoascesor.Text != "" && txtcodigoascesor.Text != null)
                                {
                                    PageSource.Session["codigoascesor"] = txtcodigoascesor.Text;
                                }

                            }

                            csGenerales cGeneral = new csGenerales();
                            cGeneral.Conexion = clsValidaciones.GetKeyOrAdd("strConexion");
                            /*GUARDA LOS PASAJEROS*/
                            ActualizarPasajerosCotizador(rptCabinas, PageSource);

                            cCache = new csCache().cCache();

                            csCarrito csCarCompUnion = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
                            DataSet dsRes = csCarCompUnion.GetDsReservas();
                            DataTable TablaPlanes = dsRes.Tables["CarritoCompras"];
                            DataTable TablaReserva = dsRes.Tables["tblReserva"];
                            DataTable TablaTransac = dsRes.Tables["tblTransac"];
                            DataTable TablaFare = dsRes.Tables["tblTarifa"];
                            DataTable TablaPax = dsRes.Tables["tblPax"];

                            string sConsec = TablaPlanes.Rows[TablaPlanes.Rows.Count - 1]["intConsecRes"].ToString();
                            string sidPlan = TablaReserva.Rows[TablaReserva.Rows.Count - 1]["intCodigoPlan"].ToString();
                            string sFechaInicial = TablaTransac.Rows[TablaTransac.Rows.Count - 1]["dtmFechaIni"].ToString();
                            TimeSpan ts = new TimeSpan(1, 0, 0, 0);
                            string sFechaFinal = Convert.ToDateTime(TablaTransac.Rows[TablaTransac.Rows.Count - 1]["dtmFechaFin"].ToString()).Subtract(ts).ToString("yyyy/MM/dd"); ;

                            DataTable tblTransacReserva = getTransacReserva(Convert.ToInt32(sConsec), TablaTransac);

                            LlenarDatosAdicionalesGuardarCotizador(rptCabinas, TablaTransac, PageSource);
                            /*GUARDAMOS LOS VALORES SI HAY BENEFICIOS*/
                            #region [BENEFICIOS]
                            //SetBeneficios(dsRes, TablaPlanes, TablaReserva, TablaTransac);
                            #endregion
                            csCarCompUnion.GuardarDataSetOActualizarDataSet("Reserva" + cCache.SessionID, dsRes);

                            csCarCompUnion = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");

                            string idRecord = clsSesiones.getProyecto();

                            string sControlCuposCon = clsValidaciones.GetKeyOrAdd("ControlCuposCon", "ConControl");
                            string sControlCuposPersonas = clsValidaciones.GetKeyOrAdd("TipoControlCuposPersona", "ConControlPersona");
                            string sControlCuposPropiedad = clsValidaciones.GetKeyOrAdd("TipoControlCuposPropiedad", "ConControPropiedades");
                            string sControlPersonaPropiedad = clsValidaciones.GetKeyOrAdd("TipoControlCuposPersonaPropiedad", "ConControlPersonaPropiedad");

                            string sEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoReservaInicialAereo", "HK");
                            string sEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPagoInicial", "PP");
                            string sFormaPago = clsValidaciones.GetKeyOrAdd("EstadoFormaPagoInicial", "EFE");
                            string iEstadoReserva = new CsConsultasVuelos().ConsultaCodigo(sEstadoReserva, "TBLESTADOS_RESERVA", "INTCODE", "STRCODE");
                            if (iEstadoReserva == null || iEstadoReserva == "")
                                iEstadoReserva = "0";
                            string iEstadoPago = new CsConsultasVuelos().ConsultaCodigo(sEstadoPago, "TBLESTADOS_RESERVA", "INTCODE", "STRCODE");
                            if (iEstadoPago == null || iEstadoPago == "")
                                iEstadoPago = "0";
                            string iFormaPago = "1";


                            if (PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposCon) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPersonas) ||
                                PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPropiedad) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlPersonaPropiedad) ||
                                PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTarjetas", "TJAS")))
                            {
                                csCarCompUnion.SaveDataProject(idRecord, cCache.Contacto, cCache.Contacto, "0", iEstadoReserva, iFormaPago, iEstadoPago);
                            }
                            else
                            {
                                csCarCompUnion.SaveDataProject(idRecord, cCache.Contacto, cCache.Contacto, "0", iEstadoReserva, iFormaPago, iEstadoPago);
                            }

                            csReservas cReserva = new csReservas();
                            cReserva.Conexion = clsValidaciones.GetKeyOrAdd("strConexion");

                            bool bReserva = true;
                            csGenerales cGen = new csGenerales();
                            //se obtiene la referencia de control de cupos
                            string sControlCupos = cGen.ConsultarIdRefere(clsValidaciones.GetKeyOrAdd("ControlCuposCon", "ConControl"),
                                clsValidaciones.GetKeyOrAdd("TipoRefControlCupos", "ControlCupos"));


                            if (PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposCon) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPersonas) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPropiedad)
                                || PageSource.Request.QueryString["ControlCupos"].Equals(sControlPersonaPropiedad))
                            {
                                //bReserva = setControlarCupos(sFechaInicial, sFechaFinal, sidPlan, tblTransacReserva, PageSource);
                            }

                            if (bReserva)
                            {
                                clsParametros Resultados = cReserva.GuardaReservaGen(csCarCompUnion.GetDsReservas());
                                if (Resultados.Id != 0)
                                {
                                    cCache = new csCache().cCache();
                                    if (clsSesiones.getProyecto() == "0")
                                        clsSesiones.setProyecto(cCache.Proyecto);
                                    csCarCompUnion.Save_Update("1");
                                    dsRes = csCarCompUnion.GetDsReservas();
                                    TablaPlanes = dsRes.Tables["CarritoCompras"];
                                    TablaReserva = dsRes.Tables["tblReserva"];



                                    csConsultasReserva ReservaCons = new csConsultasReserva();
                                    DataTable dsResCons = ReservaCons.ConReservasProyectos(cCache.Proyecto);

                                    //if (PageSource.Request.QueryString["TipoPlan"].ToString() == "CASA")
                                    //    guardarOcupacion(TablaReserva.Rows[TablaReserva.Rows.Count - 1]["dtmFechaIni"].ToString(), TablaReserva.Rows[TablaReserva.Rows.Count - 1]["dtmFechaFin"].ToString(), "", dsResCons.Tables[0].Rows[dsResCons.Tables[0].Rows.Count - 1]["strReserva"].ToString(), TablaReserva.Rows[TablaReserva.Rows.Count - 1]["intEstado"].ToString());

                                    //LlenarDatosAdicionales(rptCabinas, TablaPlanes);
                                    if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")))
                                    {
                                        InsertarCodReserva(rptCircuitos, TablaReserva, clsValidaciones.GetKeyOrAdd("TipoServicioPlan", "PLN"), dsResCons,
                                            clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC"), PageSource);
                                        InsertarCodReserva(rptCircuitos, TablaPlanes, clsValidaciones.GetKeyOrAdd("TipoServicioPlan", "PLN"), dsResCons,
                                            clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC"), PageSource);

                                        InsertarCodReservaCirc(rptCabinas, TablaTransac, clsValidaciones.GetKeyOrAdd("TipoServicioPlan", "PLN"), dsResCons);
                                    }
                                    else if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTarjetas", "TJAS")))
                                    {
                                        InsertarCodReserva(rptCircuitos, TablaReserva, clsValidaciones.GetKeyOrAdd("TipoServicioPlan", "PLN"), dsResCons,
                                            clsValidaciones.GetKeyOrAdd("TipoPlanTarjetas", "TJAS"), PageSource);
                                        InsertarCodReserva(rptCircuitos, TablaPlanes, clsValidaciones.GetKeyOrAdd("TipoServicioPlan", "PLN"), dsResCons,
                                            clsValidaciones.GetKeyOrAdd("TipoPlanTarjetas", "TJAS"), PageSource);

                                        InsertarCodReservaCirc(rptCabinas, TablaTransac, clsValidaciones.GetKeyOrAdd("TipoServicioPlan", "PLN"), dsResCons);
                                    }
                                    else
                                    {
                                        InsertarCodReserva(rptCircuitos, TablaReserva, clsValidaciones.GetKeyOrAdd("TipoServicioPlan", "PLN"), dsResCons,
                                            clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC"), PageSource);
                                        InsertarCodReserva(rptCircuitos, TablaPlanes, clsValidaciones.GetKeyOrAdd("TipoServicioPlan", "PLN"), dsResCons,
                                            clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC"), PageSource);

                                        InsertarCodReserva(rptCircuitos, TablaTransac, clsValidaciones.GetKeyOrAdd("TipoServicioPlan", "PLN"), dsResCons,
                                            clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC"), PageSource);
                                    }
                                    tblTransacReserva = getTransacReserva(Convert.ToInt32(sConsec), TablaTransac);

                                    try
                                    {
                                        string strNombreCont = "";
                                        if (TablaPax.Rows.Count > 0)
                                            strNombreCont = TablaPax.Rows[0]["strNombre"].ToString();
                                        //decimal dValorTotal = 0;
                                        //DataTable tblPax = dsResCons.Tables["tblPax"];
                                        //for (int i = 0; i < tblPax.Rows.Count; i++)
                                        //{
                                        //    dValorTotal += Convert.ToDecimal(tblPax.Rows[i]["dblTotal"].ToString().Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal));
                                        //}
                                        //new csCorreo().setDatosCorreoReservaEticaTours(PageSource, dsResCons.Tables[0].Rows[dsResCons.Tables[0].Rows.Count - 1]["strReserva"].ToString());//,
                                        //dValorTotal.ToString(), strNombreCont);
                                    }
                                    catch { }


                                    bool bInsertarCupos = true;
                                    if (PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposCon) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPersonas) || PageSource.Request.QueryString["ControlCupos"].Equals(sControlCuposPropiedad)
                                        || PageSource.Request.QueryString["ControlCupos"].Equals(sControlPersonaPropiedad))
                                    {
                                        string sReserva = TablaReserva.Rows[TablaReserva.Rows.Count - 1]["strReserva"].ToString();
                                        //bInsertarCupos = InsertarOcupaciones(tblTransacReserva, sidPlan, sReserva, PageSource, tPlan.intAplicacion.Value);
                                    }

                                    if (bInsertarCupos)
                                    {
                                        csCarCompUnion.GuardarDataSetOActualizarDataSet("Reserva" + cCache.SessionID, dsRes);
                                        clsCacheControl cCacheControl = new clsCacheControl();
                                        string sSesion = cCacheControl.RecuperarSesionId((Page)HttpContext.Current.Handler);

                                    }
                                    else
                                    {
                                        lblError.Text = "There was a problem while the transaction confirmation please contact the agency";
                                    }

                                    if (lblRecord != null)
                                        lblRecord.Text = TablaReserva.Rows[TablaReserva.Rows.Count - 1]["strReserva"].ToString();

                                    if (hRecord != null)
                                        hRecord.Value = TablaReserva.Rows[TablaReserva.Rows.Count - 1]["strReserva"].ToString();




                                    RadioButtonList rblFormasPago = (RadioButtonList)PageSource.FindControl("rblFormasPago");
                                    clsParametros cParam = new clsParametros();
                                    if (rblFormasPago != null)
                                    {
                                        string sTC = clsValidaciones.GetKeyOrAdd("TarjetaCredito", "TC");
                                        string sEfec = clsValidaciones.GetKeyOrAdd("Efectivo", "EFE");
                                        string sPSE = clsValidaciones.GetKeyOrAdd("PSE", "PSE");
                                        if (rblFormasPago.SelectedValue.Equals(sTC))
                                        {

                                            string sResult = new Reserva.csReserva().setInsertarTarjeta(PageSource, cCache);
                                            if (sResult != "OK")
                                            {
                                                cParam.Id = 1;
                                            }
                                            else
                                            {
                                                new Reserva.csReserva().setInsertarFormaPagoPlanes(sTC, TablaReserva.Rows[TablaReserva.Rows.Count - 1]["strReserva"].ToString());
                                            }
                                        }
                                        else if (rblFormasPago.SelectedValue.Equals(sEfec))
                                        {
                                            try
                                            {
                                                new Reserva.csReserva().setInsertarFormaPagoPlanes(sEfec, TablaReserva.Rows[TablaReserva.Rows.Count - 1]["strReserva"].ToString());
                                            }
                                            catch { }

                                        }
                                        else if (rblFormasPago.SelectedValue.Equals(sPSE))
                                        {

                                            new Reserva.csReserva().EnviarValoresCompleto(rblFormasPago.SelectedValue.ToString(), PageSource, "PLANES",
                                                rblFormasPago.SelectedItem.Text);

                                        }

                                        if (lblRecord != null)
                                        {
                                            new csUtilitarios().setCorreos(lblRecord.Text, rblFormasPago.SelectedValue, "RPL");
                                        }
                                    }

                                    string sPagRedir = "";
                                    if (sPagRedir.Length > 0)
                                    {
                                        Label lblUrlRedireccion = (Label)PageSource.FindControl("lblUrlRedireccion");
                                        if (lblUrlRedireccion != null)
                                            lblUrlRedireccion.Text = sPagRedir;
                                    }
                                    else
                                    {
                                        ExceptionHandled.Publicar("No se encontro la pagina de agradecimiento, se redireccionara al carro de compras, Record: " + lblRecord.Text);
                                    }
                                }
                                else
                                {
                                    lblError.Text = "There was a problem while the transaction confirmation please contact the agency";
                                }
                                clsSesiones.setPantalleRespuestaLogin(null);
                            }
                            else
                            {
                                lblError.Text = "Sorry, the space was taken, please try later";
                            }
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
                cParametros.Complemento = "btnReservarCruceroCotizador_Click";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        protected bool ValidarPaxVacios(Repeater rptCircuitos)
        {
            bool Vacios = false;
            clsCache cCache = new csCache().cCache();
            int i = 0;
            while (i < rptCircuitos.Items.Count)
            {
                csCarrito csCarCompUnion = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
                Repeater rptPax = ((Repeater)rptCircuitos.Items[i].FindControl("rptPasajeros"));
                int x = 0;
                while (x < rptPax.Items.Count)
                {
                    if (((TextBox)rptPax.Items[x].FindControl("txtNombre")).Text.Trim().Equals(""))
                    {
                        Vacios = true;
                    }
                    x++;
                }
                i++;
            }
            return Vacios;
        }

        public bool bValidaFechas(UserControl PageSource, Repeater rptCircuitos)
        {
            bool bValida = true;
            try
            {
                bool bValidaTemp = true;

                Label lblError = PageSource.FindControl("lblError") as Label;

                for (int i = 0; i < rptCircuitos.Items.Count; i++)
                {
                    Repeater rptPasajeros = ((Repeater)rptCircuitos.Items[i].FindControl("rptPasajeros"));
                    for (int d = 0; d < rptPasajeros.Items.Count; d++)
                    {
                        TextBox txtDia = rptPasajeros.Items[d].FindControl("txtDia") as TextBox;
                        TextBox txtMes = rptPasajeros.Items[d].FindControl("txtMes") as TextBox;
                        TextBox txtYear = rptPasajeros.Items[d].FindControl("txtYear") as TextBox;
                        TextBox txtNacimiento = rptPasajeros.Items[d].FindControl("txtNacimiento") as TextBox;
                        TextBox txtFechaPasaporte = rptPasajeros.Items[d].FindControl("txtPasaporteFecha") as TextBox;

                        Label lblErrorFecha = rptPasajeros.Items[d].FindControl("lblErrorFecha") as Label;
                        if (txtYear != null || txtMes != null || txtDia != null)
                        {
                            if (txtYear.Text.Trim() != "" || txtMes.Text.Trim() != "" || txtDia.Text.Trim() != "")
                            {
                                if (lblError != null)
                                {
                                    lblError.Text = string.Empty;
                                }

                                bValidaTemp = clsValidaciones.bValidaFechas(txtYear, txtMes, txtDia);
                                if (!bValidaTemp)
                                {
                                    bValida = bValidaTemp;
                                    if (lblErrorFecha != null)
                                    {
                                        lblErrorFecha.Text = "(!)";
                                        if (lblError != null)
                                        {
                                            lblError.Text = "Exiaten inconsistencias con las fechas de nacimiento, por favor corrijalas e intente de nuevo";
                                            return bValida;
                                        }
                                    }
                                }
                                else
                                {
                                    if (lblErrorFecha != null)
                                        lblErrorFecha.Text = "";
                                    if (txtNacimiento != null)
                                        txtNacimiento.Text = txtYear.Text + "/" + txtMes.Text + "/" + txtDia.Text;
                                    if (txtFechaPasaporte != null)
                                        txtFechaPasaporte.Text = txtYear.Text + "/" + txtMes.Text + "/" + txtDia.Text;
                                }
                            }
                        }
                    }
                }
            }
            catch { bValida = false; }
            return bValida;
        }

        protected void ActualizarPasajerosCotizador(Repeater rptCircuitos, UserControl PageSource)
        {

            //string sMail = "";
            //string sNombre = "";
            string sNombreCont = "";
            string sIdent = "0";
            int i = 0;
            clsCache cCache = new csCache().cCache();
            //List<DatosReserva> lDatosRes = new List<DatosReserva>();
            //lDatosRes = cCache.DatosReserva;
            //List<DatosPaxesReserva> lDatosPax = new List<DatosPaxesReserva>();
            //DatosPaxesReserva oDatosPax = null;
            while (i < rptCircuitos.Items.Count)
            {
                csCarrito csCarCompUnion = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
                Repeater rptPax = ((Repeater)rptCircuitos.Items[i].FindControl("rptPasajeros"));
                int x = 0;
                while (x < rptPax.Items.Count)
                {
                    #region Cruceros
                    if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCruceros", "CRUCE")))
                    {
                        string sEdad = "0";
                        string sGenero = "0";
                        string sTipoIdent = "0";
                        string sFechaPasaporte = DateTime.Now.ToString(clsSesiones.getFormatoFechaBD());
                        string sFechaNac = DateTime.Now.ToString(clsSesiones.getFormatoFechaBD());
                        if (((TextBox)rptPax.Items[x].FindControl("txtEdad")).Text.Trim() != "")
                            sEdad = ((TextBox)rptPax.Items[x].FindControl("txtEdad")).Text;
                        if (((DropDownList)rptPax.Items[x].FindControl("ddlGenero")).SelectedValue.Trim() != "")
                            sGenero = ((DropDownList)rptPax.Items[x].FindControl("ddlGenero")).SelectedValue.Trim();
                        if (((DropDownList)rptPax.Items[x].FindControl("ddlTipoIdent")) != null)
                        {
                            if (((DropDownList)rptPax.Items[x].FindControl("ddlTipoIdent")).SelectedValue.Trim() != "")
                                sTipoIdent = ((DropDownList)rptPax.Items[x].FindControl("ddlTipoIdent")).SelectedValue.Trim();
                        }
                        if (((TextBox)rptPax.Items[x].FindControl("txtFechaPasaporte")).Text.Trim() != "")
                            sFechaPasaporte = Convert.ToDateTime(((TextBox)rptPax.Items[x].FindControl("txtFechaPasaporte")).Text).ToString(clsSesiones.getFormatoFechaBD());

                        if (((TextBox)rptPax.Items[x].FindControl("txtNacimiento")) != null && ((TextBox)rptPax.Items[x].FindControl("txtNacimiento")).Text.Trim() != "")
                            sFechaNac = Convert.ToDateTime(((TextBox)rptPax.Items[x].FindControl("txtNacimiento")).Text).ToString(clsSesiones.getFormatoFechaBD());
                        else
                        {
                            TextBox txtYear = rptPax.Items[x].FindControl("txtYear") as TextBox;
                            TextBox txtMes = rptPax.Items[x].FindControl("txtMes") as TextBox;
                            TextBox txtDia = rptPax.Items[x].FindControl("txtDia") as TextBox;

                            if (txtYear != null && txtMes != null && txtYear != null &&
                               !string.IsNullOrEmpty(txtYear.Text) && !string.IsNullOrEmpty(txtMes.Text) && !string.IsNullOrEmpty(txtDia.Text))
                            {
                                sFechaNac = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMes.Text), int.Parse(txtDia.Text)).ToString(clsSesiones.getFormatoFechaBD());
                            }
                        }

                        csCarCompUnion.UpdatePerson(Convert.ToInt32(((Label)rptPax.Items[x].FindControl("lblidPax")).Text), ((TextBox)rptPax.Items[x].FindControl("txtNombre")).Text, sFechaNac, null, null,
                            Convert.ToInt32(sEdad), null, sGenero, ((TextBox)rptPax.Items[x].FindControl("txtNacionalidad")).Text,
                            ((TextBox)rptPax.Items[x].FindControl("txtPasaporte")).Text, sFechaPasaporte, ((TextBox)rptPax.Items[x].FindControl("txtPaisResidencia")).Text);
                    }
                    #endregion
                    #region Otros
                    else
                    {
                        try
                        {
                            if (x == 0)
                            {
                                cCache.User = ((TextBox)rptPax.Items[x].FindControl("txtMail")).Text;
                                csCache.ActualizarCache(cCache);
                            }
                        }
                        catch { }

                        string sGeneroText = "0";
                        string sGenero = "0";
                        string sTipoIdent = "0";
                        string sFechaNac = DateTime.Now.ToString(clsSesiones.getFormatoFechaBD());
                        if (((DropDownList)rptPax.Items[x].FindControl("ddlGenero")).SelectedValue.Trim() != "")
                        {
                            sGeneroText = ((DropDownList)rptPax.Items[x].FindControl("ddlGenero")).SelectedItem.Text.Trim();
                            sGenero = ((DropDownList)rptPax.Items[x].FindControl("ddlGenero")).SelectedValue.Trim();
                        }
                        if (((DropDownList)rptPax.Items[x].FindControl("ddlTipoIdent")) != null)
                        {
                            if (((DropDownList)rptPax.Items[x].FindControl("ddlTipoIdent")).SelectedValue.Trim() != "")
                                sTipoIdent = ((DropDownList)rptPax.Items[x].FindControl("ddlTipoIdent")).SelectedValue.Trim();
                        }
                        if (((TextBox)rptPax.Items[x].FindControl("txtPasaporte")) != null)
                        {
                            sIdent = ((TextBox)rptPax.Items[x].FindControl("txtPasaporte")).Text;
                        }
                        if (((TextBox)rptPax.Items[x].FindControl("txtNacimiento")) != null && ((TextBox)rptPax.Items[x].FindControl("txtNacimiento")).Text.Trim() != "")
                        {
                            Enum_FormatoFecha fFormatoFecha;
                            if (sFormatoFecha == "MM/dd/yyyy")
                                fFormatoFecha = Enum_FormatoFecha.MDY;
                            else
                                fFormatoFecha = Enum_FormatoFecha.DMY;

                            sFechaNac = clsValidaciones.ConverFechaDateTime(((TextBox)rptPax.Items[x].FindControl("txtNacimiento")).Text, fFormatoFecha).ToString(sFormatoFechaBD);
                        }
                        else
                        {
                            TextBox txtYear = rptPax.Items[x].FindControl("txtYear") as TextBox;
                            TextBox txtMes = rptPax.Items[x].FindControl("txtMes") as TextBox;
                            TextBox txtDia = rptPax.Items[x].FindControl("txtDia") as TextBox;

                            if (txtYear != null && txtMes != null && txtYear != null &&
                               !string.IsNullOrEmpty(txtYear.Text) && !string.IsNullOrEmpty(txtMes.Text) && !string.IsNullOrEmpty(txtDia.Text))
                            {
                                sFechaNac = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMes.Text), int.Parse(txtDia.Text)).ToString(clsSesiones.getFormatoFechaBD());
                            }
                        }

                        string sNombrePax = "";
                        if (((TextBox)rptPax.Items[x].FindControl("txtNombre")) != null && ((TextBox)rptPax.Items[x].FindControl("txtApellido")) != null)
                            sNombrePax = ((TextBox)rptPax.Items[x].FindControl("txtNombre")).Text + " " + ((TextBox)rptPax.Items[x].FindControl("txtApellido")).Text;
                        else
                            sNombrePax = ((TextBox)rptPax.Items[x].FindControl("txtNombre")).Text;

                        if (x == 0)
                            sNombreCont = sNombrePax;

                        csCarCompUnion.UpdatePerson(Convert.ToInt32(((Label)rptPax.Items[x].FindControl("lblidPax")).Text), sNombrePax,
                           sFechaNac, null, null, null, null, sGenero, null, sIdent, null, null, sTipoIdent);

                        //oDatosPax = new DatosPaxesReserva();
                        //oDatosPax.sNombrePax = sNombrePax;

                        //DropDownList rblTrato = (DropDownList)rptPax.Items[x].FindControl("rblTrato");
                        //if (rblTrato != null)
                        //    oDatosPax.sTrato = rblTrato.SelectedItem.Text;

                        //Label lblTipoPax = (Label)rptPax.Items[x].FindControl("lblTipoPax");
                        //if (lblTipoPax != null)
                        //    oDatosPax.sTipoPax = lblTipoPax.Text;

                        //oDatosPax.sGenero = sGeneroText;
                        //oDatosPax.sTelefono = "";
                        //oDatosPax.sPais = "";

                        //TextBox txtMail = (TextBox)rptPax.Items[x].FindControl("txtMail");
                        //if (txtMail != null)
                        //    oDatosPax.sCorreo = txtMail.Text;

                        //oDatosPax.sEventos = "";
                        //oDatosPax.sObservaciones = "";
                        //lDatosPax.Add(oDatosPax);
                    }
                    #endregion
                    //if (x == 0)
                    //{
                    //    TextBox txtMail = rptPax.Items[x].FindControl("txtMail") as TextBox;
                    //    if (txtMail != null)
                    //        sMail = txtMail.Text;

                    //    TextBox txtNombre = rptPax.Items[x].FindControl("txtNombre") as TextBox;
                    //    TextBox txtApellido = rptPax.Items[x].FindControl("txtApellido") as TextBox;
                    //    if (txtNombre != null)
                    //        sNombre = txtNombre.Text;
                    //    if (txtApellido != null)
                    //        sNombre = sNombre + " " + txtApellido.Text;
                    //}
                    x++;
                }

                //if (i == 0)
                //{
                //    tblContactos tContactos = new tblContactos();
                //    clsParametros cParam = null;
                //    csLogin cLogin = new csLogin();
                //    tContactos.Get(sMail, clsSesiones.getAplicacion());
                //    if (tContactos.Respuesta)
                //    {
                //        cParam = cLogin.setAutenticarSinRedireccion(PageSource, tContactos.strEmail.Value,
                //           tContactos.strPassword.Value, Enum_Login.LoginGen);
                //        if (cParam.Id == 0)
                //            ExceptionHandled.Publicar("//*********************--------------- Error al validar el cliente, no se pudo consultar (reserva de circuitos)");
                //    }
                //    else
                //    {
                //        cParam = cLogin.setCrearSinRedireccion(PageSource, sNombre, "", sMail, "",
                //            clsValidaciones.GetKeyOrAdd("sClaveDefecto", "ABC123"), sIdent, Enum_Login.LoginGen);
                //        if (cParam.Id == 0)
                //            ExceptionHandled.Publicar("//*********************--------------- Error al crear el cliente, no se pudo consultar (reserva de circuitos)");
                //    }
                //}
                //lDatosRes[i].lDatosPaxes = lDatosPax;
                //lDatosRes[i].sNombreContacto = sNombreCont;
                i++;
            }
            csCache.ActualizarCache(cCache);
        }

        protected DataTable getTransacReserva(int iConsec, DataTable tblTransac)
        {
            DataTable tblTRansacFiltrada = tblTransac.Clone();
            for (int i = 0; i < tblTransac.Rows.Count; i++)
            {
                if (tblTransac.Rows[i]["intConsecRes"].ToString().Equals(iConsec.ToString()))
                {
                    tblTRansacFiltrada.Rows.Add(tblTransac.Rows[i].ItemArray);
                }
            }
            return tblTRansacFiltrada;
        }

        protected void LlenarDatosAdicionalesGuardarCotizador(Repeater rpt, DataTable TablaPrincipal, UserControl PageSource)
        {
            if (PageSource.Request.QueryString["Codigo"] != null && PageSource.Request.QueryString["TipoPlan"] != null)
            {
                int i = 0;
                while (i < rpt.Items.Count)
                {
                    string iConsec = ((Label)rpt.Items[i].FindControl("lblConsecRes")).Text;
                    string iSegmento = ((Label)rpt.Items[i].FindControl("lblSegmento")).Text;
                    for (int x = 0; x < TablaPrincipal.Rows.Count; x++)
                    {
                        if (TablaPrincipal.Rows[x]["intConsecRes"].ToString().Equals(iConsec) && TablaPrincipal.Rows[x]["intSegmento"].ToString().Equals(iSegmento))
                        {
                            Panel pDirInicio = (Panel)rpt.Items[i].FindControl("pDirInicio");
                            Panel pDirFin = (Panel)rpt.Items[i].FindControl("pDirFin");
                            Panel pDatosVuelo = (Panel)rpt.Items[i].FindControl("pDatosVuelo");
                            Panel pDatosVueloFin = (Panel)rpt.Items[i].FindControl("pDatosVueloFin");
                            DropDownList ddlIdioma = (DropDownList)rpt.Items[i].FindControl("ddlIdioma");

                            if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS"))
                                || PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC"))
                                || PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTarjetas", "TJAS")))
                            {
                                StringBuilder sbObservaciones = new StringBuilder();
                                if (ddlIdioma != null && ddlIdioma.SelectedItem != null)
                                    sbObservaciones.AppendLine("- Idioma del servicio: " + ddlIdioma.SelectedItem.Text + " <br />");

                                if (PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanTraslados", "TRAS"))
                                    || PageSource.Request.QueryString["TipoPlan"].Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")))
                                {
                                    sbObservaciones.AppendLine("- Hora servicio: " + ((DropDownList)rpt.Items[i].FindControl("ddlHora")).Text + " : " + ((DropDownList)rpt.Items[i].FindControl("ddlMinuto")).Text + "<br />");
                                    if (((RadioButtonList)rpt.Items[i].FindControl("rblLugarInicio")).SelectedItem != null)
                                        sbObservaciones.AppendLine("- Lugar inicio del servicio: " + ((RadioButtonList)rpt.Items[i].FindControl("rblLugarInicio")).SelectedItem.Value + "<br />");
                                    if (pDirInicio != null && pDirInicio.Visible)
                                        sbObservaciones.AppendLine("- Dirección Inicio del servicio: " + ((TextBox)rpt.Items[i].FindControl("txtDirInicio")).Text + "<br />");
                                    if (pDatosVuelo != null && pDatosVuelo.Visible)
                                    {
                                        sbObservaciones.AppendLine("* Datos del vuelo: " + "<br />");
                                        sbObservaciones.AppendLine("- Aerolinea: " + ((TextBox)rpt.Items[i].FindControl("txtAerolinea")).Text + "<br />");
                                        sbObservaciones.AppendLine("- No. Vuelo: " + ((TextBox)rpt.Items[i].FindControl("txtVuelo")).Text + "<br />");
                                        sbObservaciones.AppendLine("- Origen: " + ((TextBox)rpt.Items[i].FindControl("txtOrigen")).Text + "<br />");
                                        if (((DropDownList)rpt.Items[i].FindControl("ddlHoraSalida")) != null)
                                            sbObservaciones.AppendLine("- Hora de salida: " + ((DropDownList)rpt.Items[i].FindControl("ddlHoraSalida")).Text + " : " + ((DropDownList)rpt.Items[i].FindControl("ddlMinutosSalida")).Text + "<br />");
                                        else
                                            sbObservaciones.AppendLine("- Hora de salida: " + ((TextBox)rpt.Items[i].FindControl("txtHoraSalida")).Text + "<br />");
                                        sbObservaciones.AppendLine("- Destino: " + ((TextBox)rpt.Items[i].FindControl("txtDestino")).Text + "<br />");
                                        if (((DropDownList)rpt.Items[i].FindControl("ddlHoraLlegada")) != null)
                                            sbObservaciones.AppendLine("- Hora de llegada: " + ((DropDownList)rpt.Items[i].FindControl("ddlHoraLlegada")).Text + " : " + ((DropDownList)rpt.Items[i].FindControl("ddlMinutosLlegada")).Text + "<br />");
                                        else
                                            sbObservaciones.AppendLine("- Hora de llegada: " + ((TextBox)rpt.Items[i].FindControl("txtHoraLlegada")).Text + "<br />");
                                    }
                                    if (((RadioButtonList)rpt.Items[i].FindControl("rblLugarFin")).SelectedItem != null)
                                        sbObservaciones.AppendLine("- Lugar fin del servicio: " + ((RadioButtonList)rpt.Items[i].FindControl("rblLugarFin")).SelectedItem.Value + "<br />");
                                    if (pDirFin != null && pDirFin.Visible)
                                        sbObservaciones.AppendLine("- Dirección final del servicio: " + ((TextBox)rpt.Items[i].FindControl("txtDirFin")).Text + "<br />");
                                    if (pDatosVueloFin != null && pDatosVueloFin.Visible)
                                    {
                                        sbObservaciones.AppendLine("* Datos del vuelo: " + "<br />");
                                        sbObservaciones.AppendLine("- Aerolinea: " + ((TextBox)rpt.Items[i].FindControl("txtAerolineaFin")).Text + "<br />");
                                        sbObservaciones.AppendLine("- No. Vuelo: " + ((TextBox)rpt.Items[i].FindControl("txtVueloFin")).Text + "<br />");
                                        sbObservaciones.AppendLine("- Origen: " + ((TextBox)rpt.Items[i].FindControl("txtOrigenFin")).Text + "<br />");
                                        if (((DropDownList)rpt.Items[i].FindControl("ddlHoraSalidaFin")) != null)
                                            sbObservaciones.AppendLine("- Hora de salida: " + ((DropDownList)rpt.Items[i].FindControl("ddlHoraSalidaFin")).Text + " : " + ((DropDownList)rpt.Items[i].FindControl("ddlMinutosSalidaFin")).Text + "<br />");
                                        else
                                            sbObservaciones.AppendLine("- Hora de salida: " + ((TextBox)rpt.Items[i].FindControl("txtHoraSalidaFin")).Text + "<br />");
                                        sbObservaciones.AppendLine("- Destino: " + ((TextBox)rpt.Items[i].FindControl("txtDestinoFin")).Text + "<br />");
                                        if (((DropDownList)rpt.Items[i].FindControl("ddlHoraLlegadaFin")) != null)
                                            sbObservaciones.AppendLine("- Hora de llegada: " + ((DropDownList)rpt.Items[i].FindControl("ddlHoraLlegadaFin")).Text + " : " + ((DropDownList)rpt.Items[i].FindControl("ddlMinutosLlegadaFin")).Text + "<br />");
                                        else
                                            sbObservaciones.AppendLine("- Hora de llegada: " + ((TextBox)rpt.Items[i].FindControl("txtHoraLlegadaFin")).Text + "<br />");
                                    }
                                }
                                if (((TextBox)rpt.Items[i].FindControl("txtNombreContacto")) != null)
                                {
                                    sbObservaciones.AppendLine("* Notificar novedades en la reserva a: " + "<br />");
                                    sbObservaciones.AppendLine("- Nombre: " + ((TextBox)rpt.Items[i].FindControl("txtNombreContacto")).Text + "<br />");
                                    if (((TextBox)rpt.Items[i].FindControl("txtTelContacto")) != null)
                                        sbObservaciones.AppendLine("- Teléfono: " + ((TextBox)rpt.Items[i].FindControl("txtTelContacto")).Text + "<br />");
                                    if (((TextBox)rpt.Items[i].FindControl("txtEmpresaContacto")) != null)
                                        sbObservaciones.AppendLine("- Empresa: " + ((TextBox)rpt.Items[i].FindControl("txtEmpresaContacto")).Text + "<br />");
                                }
                                if (((TextBox)rpt.Items[i].FindControl("txtObservaciones")) != null)
                                    sbObservaciones.AppendLine("Observaciones: " + ((TextBox)rpt.Items[i].FindControl("txtObservaciones")).Text + "<br />");
                                TablaPrincipal.Rows[x]["StrObservacion"] = sbObservaciones.ToString();
                            }
                            else
                            {

                                if (TablaPrincipal.Rows[x]["StrHoteles"].ToString().Trim() != "")
                                {
                                    TablaPrincipal.Rows[x]["StrObservacion"] = "Hoteles seleccionados: " + TablaPrincipal.Rows[x]["StrHoteles"].ToString() + "<br /><br />" +
                                        ((TextBox)rpt.Items[i].FindControl("txtObservaciones")).Text;
                                }
                                else
                                {
                                    TablaPrincipal.Rows[x]["StrObservacion"] = ((TextBox)rpt.Items[i].FindControl("txtObservaciones")).Text;
                                }
                            }
                            //}
                        }
                    }
                    i++;
                }
            }
        }

        protected void InsertarCodReserva(Repeater rpt, DataTable TablaPrincipal, string TipoServicio, DataTable TablaConsultada,
            string TipoPlan, UserControl PageSource)
        {
            string sFiltroTipoPlan = "";
            if (TipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanCircuito", "CIRC")))
                sFiltroTipoPlan = " and strReserva  LIKE 'CI*'";
            if (TipoPlan.Equals(clsValidaciones.GetKeyOrAdd("TipoPlanExcursion", "EXC")))
                sFiltroTipoPlan = " and strReserva  LIKE 'EX*'";


            csGenerales Generales = new csGenerales();
            Generales.Conexion = clsSesiones.getConexion();
            DataView ViewSec = new DataView();
            ViewSec.Table = TablaConsultada;
            ViewSec.RowFilter = "strCodigo = '" + TipoServicio + "'" + sFiltroTipoPlan;
            DataTable tblFiltro = ViewSec.ToTable();
            clsCache cCache = new csCache().cCache();
            //List<DatosReserva> lDatosRes = new List<DatosReserva>();
            //lDatosRes = cCache.DatosReserva;
            if (rpt.Items.Count > 0)
            {
                int i = 0;
                int j = 0;
                while (i < rpt.Items.Count)
                {
                    int Pos = int.Parse(((Label)rpt.Items[i].FindControl("lblPosicion")).Text);
                    TablaPrincipal.Rows[Pos]["strReserva"] = tblFiltro.Rows[tblFiltro.Rows.Count - 1]["strReserva"].ToString();
                    //lDatosRes[i].sCodReserva = TablaPrincipal.Rows[Pos]["strReserva"].ToString();
                    i++;
                    j++;
                }
            }
            csCache.ActualizarCache(cCache);
            PageSource.Session["$CodigoReservaPlan"] = tblFiltro.Rows[tblFiltro.Rows.Count - 1]["strReserva"].ToString();
        }

        protected void InsertarCodReservaCirc(Repeater rpt, DataTable TablaPrincipal, string TipoServicio, DataTable TablaConsultada)
        {
            csGenerales Generales = new csGenerales();
            Generales.Conexion = clsSesiones.getConexion();
            DataView ViewSec = new DataView();
            string stipoTarjeta = "";
            if (HttpContext.Current.Request.QueryString["TipoPlan"] != null)
            {
                if (HttpContext.Current.Request.QueryString["TipoPlan"].ToString().Equals("TJAS"))
                    stipoTarjeta = "TJ";

            }

            ViewSec.Table = TablaConsultada;
            if (stipoTarjeta != "")
            {
                ViewSec.RowFilter = "strCodigo = '" + TipoServicio + "' and strReserva  LIKE 'TJ*'";
            }
            else
            {
                ViewSec.RowFilter = "strCodigo = '" + TipoServicio + "' and strReserva  LIKE 'CI*'";
            }

            DataTable tblFiltro = ViewSec.ToTable();
            if (rpt.Items.Count > 0)
            {
                int i = 1;
                while (i <= rpt.Items.Count)
                {
                    TablaPrincipal.Rows[TablaPrincipal.Rows.Count - i]["strReserva"] = tblFiltro.Rows[tblFiltro.Rows.Count - 1]["strReserva"].ToString();
                    i++;
                }
            }
        }

        public bool SetCrearAfiliadosPlanes(UserControl PageSource, int intContacto)
        {
            bool bValida = true;
            string registro = "0";
            string sCodigo = string.Empty;
            int CantAfiliados = -1;
            Label lblError = (Label)PageSource.FindControl("lblerror");

            try
            {
                CantAfiliados = Convert.ToInt32(new CsConsultasVuelos().ConsultaCodigo(intContacto.ToString(), "tblAfiliados", "Count(intAfiliado)", "intUsuario"));
                int sCantidadAfiliados = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("sCantidadAfiliados", "5"));
                if (CantAfiliados != -1)
                {
                    TextBox txtNombre = (TextBox)PageSource.FindControl("txtNombre");
                    TextBox txtApellido = (TextBox)PageSource.FindControl("txtApellido");
                    TextBox txtDocumentosid = (TextBox)PageSource.FindControl("txtDocumentosid");
                    TextBox txtMailPersonal = (TextBox)PageSource.FindControl("txtMailPersonal");
                    TextBox txtTelefono = (TextBox)PageSource.FindControl("txtTelefono");
                    TextBox txtFnacimiento = (TextBox)PageSource.FindControl("txtEdad1");
                    DropDownList ddlTpoDocumentoR = (DropDownList)PageSource.FindControl("ddlTpoDocumentoR");
                    DropDownList ddlGeneroR = (DropDownList)PageSource.FindControl("ddlGeneroR");
                    string strFecha = DateTime.Today.ToString("yyyy/MM/dd");
                    string fechaNaci = clsValidaciones.ConverMDYtoYMD(txtFnacimiento.Text, "/");

                    sCodigo = new CsConsultasVuelos().ConsultaCodigo(txtDocumentosid.Text, "tblAfiliados", "strIdentificacion", "strIdentificacion");
                    if (sCodigo != null && sCodigo != "")
                    {

                        lblError.Text = "Este usuario ya se encuentra registrado";
                    }
                    else
                    {
                        registro = new CsConsultasVuelos().EjecutarSPConsulta("SPCreaAfiliado", new string[10] { intContacto.ToString(), "'" + ddlTpoDocumentoR.SelectedItem.Value.ToString() + "'", "'" + txtDocumentosid.Text + "'", "'" + txtNombre.Text + "'", "'" + txtApellido.Text + "'", ddlGeneroR.SelectedItem.Value.ToString(), "'" + fechaNaci + "'", "'" + txtTelefono.Text + "'", "'" + txtMailPersonal.Text + "'", "'" + strFecha + "'" });
                    }
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtDocumentosid.Text = "";
                    txtMailPersonal.Text = "";
                    txtTelefono.Text = "";
                    txtFnacimiento.Text = "";

                    if (registro == "0")
                    {
                        bValida = false;

                    }

                }


            }
            catch { }

            return bValida;




        }
    }
}
