using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Utils;
using SsoftQuery.Planes;
using System.Data;
using Ssoft.Rules.Generales;
using Ssoft.ManejadorExcepciones;
using System.Web;

namespace Ssoft.Pages.PaginaPlanes
{
    public class csBuscadorPlanes
    {
        /// <summary>
        /// Metodo que ejecuta el command del boton del buscador de plan y que direcciona al metodo principal de busqueda
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-12
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public void setCommand(UserControl PageSource, object source, CommandEventArgs e)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    switch (e.CommandName)
                    {
                        case "PlanesC":
                            GuardarParametrosBusquedaCircular(PageSource);
                            break;
                        case "PlanesD":
                            GuardarParametrosBusquedaCircularD(PageSource);
                            break;
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Metodo que guarda los parametros de busqueda de planes en el objeto correspondiente y direcciona a la pagina de resultados
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-12
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public void GuardarParametrosBusquedaCircular(UserControl PageSource)
        {
            try
            {
                TextBox txtFiltroTexto = (TextBox)PageSource.FindControl("txtFiltroTexto");
                csGeneralsPag.Idioma(PageSource);
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    if (!csGeneralsPag.Externo())
                    {
                        List<string> listaValores = new List<string>();
                        DropDownList ddlZonaGeo = (DropDownList)PageSource.FindControl("ddlZonaGeo");
                        DropDownList ddlPais = (DropDownList)PageSource.FindControl("ddlPais");
                        DropDownList ddlCiudad = (DropDownList)PageSource.FindControl("ddlCiudad");
                        DropDownList ddlTipologia = (DropDownList)PageSource.FindControl("ddlTipologia");
                        DropDownList ddlTipoPlan = (DropDownList)PageSource.FindControl("ddlTipoPlan");
                        RadioButtonList modal_plan = (RadioButtonList)PageSource.FindControl("modal_plan");
                        TextBox txtFechaViaje = (TextBox)PageSource.FindControl("txtFechaViaje");
                        cCache.DatosAdicionales = null;


                        if (ddlZonaGeo != null)
                        {
                            if (ddlZonaGeo.SelectedItem != null)
                                listaValores.Add(ddlZonaGeo.SelectedItem.Value);
                            else
                                listaValores.Add("");
                        }
                        else
                        {
                            listaValores.Add("");
                        }
                        if (ddlPais != null)
                        {
                            if (ddlPais.SelectedItem != null)
                                listaValores.Add(ddlPais.SelectedItem.Value);
                            else
                                listaValores.Add("");
                        }
                        else
                        {
                            listaValores.Add("");
                        }
                        if (ddlCiudad != null)
                        {
                            if (ddlCiudad.SelectedItem != null)
                                listaValores.Add(ddlCiudad.SelectedItem.Value);
                            else
                                listaValores.Add("");
                        }
                        else
                        {
                            listaValores.Add("");
                        }
                        if (ddlTipologia != null)
                        {
                            if (ddlTipologia.SelectedItem != null)
                                listaValores.Add(ddlTipologia.SelectedItem.Value);
                            else
                                listaValores.Add("");
                        }
                        else
                        {
                            listaValores.Add("");
                        }
                        if (ddlTipoPlan != null)
                        {
                            if (ddlTipoPlan.SelectedItem != null)
                                listaValores.Add(ddlTipoPlan.SelectedItem.Value);
                            else
                                listaValores.Add("");
                        }
                        else
                        {
                            listaValores.Add("");
                        }
                        if (modal_plan != null)
                        {
                            if (modal_plan.SelectedItem != null)
                            {
                                listaValores.Add(modal_plan.SelectedItem.Value);
                            }
                            else
                            {
                                listaValores.Add("");
                            }
                        }
                        if (txtFechaViaje != null && !(txtFechaViaje.Text.ToUpper().Contains("MM")))
                            listaValores.Add(txtFechaViaje.Text.Trim());

                        cCache.DatosAdicionales = listaValores;
                        csCache.ActualizarCache(cCache);
                    }
                }
                string sFiltroPais = "";
                string sFiltroZona = "";
                string sFiltroTipoPlan = "";
                string filtroTexto = "";

                if (HttpContext.Current.Request.QueryString["PaisDestino"] != null)
                    sFiltroPais = "&PaisDestino=" + HttpContext.Current.Request.QueryString["PaisDestino"];
                if (HttpContext.Current.Request.QueryString["ZonaGeo"] != null)
                    sFiltroZona = "&ZonaGeo=" + HttpContext.Current.Request.QueryString["ZonaGeo"];
                if (HttpContext.Current.Request.QueryString["TipoPlan"] != null)
                    sFiltroTipoPlan = "&TipoPlan=" + HttpContext.Current.Request.QueryString["TipoPlan"];
                if (HttpContext.Current.Request.QueryString["FiltroTexto"] != null)
                    filtroTexto = "&FiltroTexto=" + HttpContext.Current.Request.QueryString["FiltroTexto"];

                //if (HttpContext.Current.Request.QueryString["FiltroTexto"] != null)
                //    filtroTexto = "&FiltroTexto=" + HttpContext.Current.Request.QueryString["FiltroTexto"];

                //filtro de texto
                //if (txtFiltroTexto != null &&
                //    !string.IsNullOrWhiteSpace(txtFiltroTexto.Text))
                //{
                filtroTexto = "&FiltroTexto=" + txtFiltroTexto.Text;
                //}


                clsValidaciones.RedirectPagina("ResultadoPlanes.aspx?ORIGEN=BUSCADOR"
                    + sFiltroPais
                    + sFiltroZona
                    + sFiltroTipoPlan
                    + filtroTexto);

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
                cParametros.Metodo = "setGuardarParametrosBusqueda";
                cParametros.Complemento = "Error en al guardar los parametros de busqueda en traslados";
                ExceptionHandled.Publicar(cParametros);
            }

        }

        public void GuardarParametrosBusquedaCircularD(UserControl PageSource)
        {
            try
            {
                TextBox txtFiltroTexto = (TextBox)PageSource.FindControl("txtFiltroTexto");
                csGeneralsPag.Idioma(PageSource);
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    if (!csGeneralsPag.Externo())
                    {
                        List<string> listaValores = new List<string>();
                        DropDownList ddlZonaGeo = (DropDownList)PageSource.FindControl("ddlZonaGeo");
                        DropDownList ddlPais = (DropDownList)PageSource.FindControl("ddlPais");
                        DropDownList ddlCiudad = (DropDownList)PageSource.FindControl("ddlCiudad");
                        DropDownList ddlTipologia = (DropDownList)PageSource.FindControl("ddlTipologia");
                        DropDownList ddlTipoPlan = (DropDownList)PageSource.FindControl("ddlTipoPlan");
                        RadioButtonList modal_plan = (RadioButtonList)PageSource.FindControl("modal_plan");
                        TextBox txtFechaViaje = (TextBox)PageSource.FindControl("txtFechaViaje");
                        cCache.DatosAdicionales = null;


                        if (ddlZonaGeo != null)
                        {
                            if (ddlZonaGeo.SelectedItem != null)
                                listaValores.Add(ddlZonaGeo.SelectedItem.Value);
                            else
                                listaValores.Add("");
                        }
                        else
                        {
                            listaValores.Add("");
                        }
                        if (ddlPais != null)
                        {
                            if (ddlPais.SelectedItem != null)
                                listaValores.Add(ddlPais.SelectedItem.Value);
                            else
                                listaValores.Add("");
                        }
                        else
                        {
                            listaValores.Add("");
                        }
                        if (ddlCiudad != null)
                        {
                            if (ddlCiudad.SelectedItem != null)
                                listaValores.Add(ddlCiudad.SelectedItem.Value);
                            else
                                listaValores.Add("");
                        }
                        else
                        {
                            listaValores.Add("");
                        }
                        if (ddlTipologia != null)
                        {
                            if (ddlTipologia.SelectedItem != null)
                                listaValores.Add(ddlTipologia.SelectedItem.Value);
                            else
                                listaValores.Add("");
                        }
                        else
                        {
                            listaValores.Add("");
                        }
                        if (ddlTipoPlan != null)
                        {
                            if (ddlTipoPlan.SelectedItem != null)
                                listaValores.Add(ddlTipoPlan.SelectedItem.Value);
                            else
                                listaValores.Add("");
                        }
                        else
                        {
                            listaValores.Add("");
                        }
                        if (modal_plan != null)
                        {
                            if (modal_plan.SelectedItem != null)
                            {
                                listaValores.Add(modal_plan.SelectedItem.Value);
                            }
                            else
                            {
                                listaValores.Add("");
                            }
                        }
                        if (txtFechaViaje != null && !(txtFechaViaje.Text.ToUpper().Contains("MM")))
                            listaValores.Add(txtFechaViaje.Text.Trim());

                        cCache.DatosAdicionales = listaValores;
                        csCache.ActualizarCache(cCache);
                    }
                }
                string sFiltroPais = "";
                string sFiltroZona = "";
                string sFiltroTipoPlan = "";
                string filtroTexto = "";

                if (HttpContext.Current.Request.QueryString["PaisDestino"] != null)
                    sFiltroPais = "&PaisDestino=" + HttpContext.Current.Request.QueryString["PaisDestino"];
                if (HttpContext.Current.Request.QueryString["ZonaGeo"] != null)
                    sFiltroZona = "&ZonaGeo=" + HttpContext.Current.Request.QueryString["ZonaGeo"];
                if (HttpContext.Current.Request.QueryString["TipoPlan"] != null)
                    sFiltroTipoPlan = "&TipoPlan=" + HttpContext.Current.Request.QueryString["TipoPlan"];
                if (HttpContext.Current.Request.QueryString["FiltroTexto"] != null)
                    filtroTexto = "&FiltroTexto=" + HttpContext.Current.Request.QueryString["FiltroTexto"];

                //if (HttpContext.Current.Request.QueryString["FiltroTexto"] != null)
                //    filtroTexto = "&FiltroTexto=" + HttpContext.Current.Request.QueryString["FiltroTexto"];

                //filtro de texto
                //if (txtFiltroTexto != null &&
                //    !string.IsNullOrWhiteSpace(txtFiltroTexto.Text))
                //{
                filtroTexto = "&FiltroTexto=" + txtFiltroTexto.Text;
                //}


                clsValidaciones.RedirectPagina("Planes.aspx?ORIGEN=BUSCADOR"
                    + sFiltroPais
                    + sFiltroZona
                    + sFiltroTipoPlan
                    + filtroTexto);

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
                cParametros.Metodo = "setGuardarParametrosBusqueda";
                cParametros.Complemento = "Error en al guardar los parametros de busqueda en traslados";
                ExceptionHandled.Publicar(cParametros);
            }

        }

        /// <summary>
        /// Metodo general de carga de buscador de planes y llenado de controles 
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-12
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public void setPlan(UserControl PageSource)
        {
            try
            {
                //CargarCategoriaPlan(PageSource);
                //CargarTipologia(PageSource);
                /*NECESARIO PARA EL BUSCADOR DE IROTAMA*/
                //CargarTiposDePlan(PageSource);
                clsCache cCache = new csCache().cCache();

                List<string> listaValores = cCache.DatosAdicionales;

                AjaxControlToolkit.CascadingDropDown cddZona =
                    PageSource.FindControl("cddZona") as AjaxControlToolkit.CascadingDropDown;
                AjaxControlToolkit.CascadingDropDown cddPais =
                    PageSource.FindControl("cddPais") as AjaxControlToolkit.CascadingDropDown;
                AjaxControlToolkit.CascadingDropDown cddCiudad =
                    PageSource.FindControl("cddCiudad") as AjaxControlToolkit.CascadingDropDown;
                DropDownList ddlTipologia =
                    PageSource.FindControl("ddlTipologia") as DropDownList;
                DropDownList ddlTipoPlan =
                    PageSource.FindControl("ddlTipoPlan") as DropDownList;
                TextBox txtFechaViaje = (TextBox)PageSource.FindControl("txtFechaViaje");

                if (cCache != null && cCache.DatosAdicionales != null)
                {
                    DateTime dt = new DateTime();
                    if (DateTime.TryParse(listaValores[5].ToString(), out dt) || listaValores[5].ToString() == "")
                    {
                        if (cddZona != null)
                        {
                            try
                            {
                                cddZona.SelectedValue = listaValores[0];

                            }
                            catch { }
                        }
                        if (cddPais != null)
                        {
                            try
                            {
                                cddPais.SelectedValue = listaValores[1];
                            }
                            catch { }
                        }
                        if (cddCiudad != null)
                        {
                            try
                            {
                                cddCiudad.SelectedValue = listaValores[2];
                            }
                            catch { }
                        }
                        //if (ddlTipologia != null)
                        //{
                        //    try
                        //    {
                        //        ddlTipologia.SelectedValue = listaValores[3];
                        //    }
                        //    catch { }
                        //}
                        //if (ddlTipoPlan != null)
                        //{
                        //    try
                        //    {
                        //        ddlTipoPlan.SelectedValue = listaValores[4];
                        //    }
                        //    catch { }
                        //}
                        //if (txtFechaViaje != null)
                        //{
                        //    try
                        //    {
                        //        txtFechaViaje.Text = listaValores[5];
                        //    }
                        //    catch { }
                        //}
                    }
                }

                Boolean esNacionales =
                            PageSource.Request.QueryString["TIPODESTINO"] != null &&
                            PageSource.Request.QueryString["TIPODESTINO"].Equals("NACIONAL");
                Boolean esInternacionales =
                    PageSource.Request.QueryString["TIPODESTINO"] != null &&
                    PageSource.Request.QueryString["TIPODESTINO"].Equals("INTERNACIONAL");

                if (esNacionales)
                {
                    try
                    {
                        csConsultasPlanes cConsPlanes = new csConsultasPlanes();
                        DataTable tblZonas = cConsPlanes.ConReferenciaZonasGeograficas("SUA");
                        if (tblZonas != null)
                            cddZona.SelectedValue = tblZonas.Rows[0]["IntIdZona"].ToString();


                        string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");
                        DataTable tblPaises = cConsPlanes.ConReferenciaPaises(sPaisDefault);
                        if (tblPaises != null)
                            cddPais.SelectedValue = tblPaises.Rows[0]["IntCode"].ToString();
                    }
                    catch { }
                }
            }
            catch { }
        }

        /// <summary>
        /// Metodo de consulta de paises por zona geografica 
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz 
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-13
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public void setPaisesZona(UserControl PageSource, string Empresa)
        {
            try
            {
                AjaxControlToolkit.CascadingDropDown cddZona =
                    PageSource.FindControl("cddZona") as AjaxControlToolkit.CascadingDropDown;
                if (cddZona == null)
                {
                    try
                    {
                        csConsultasPlanes cConsPlanes = new csConsultasPlanes();
                        DataTable dtDatosUbic = new DataTable();
                        DropDownList ddlPais = (DropDownList)PageSource.FindControl("ddlPais");
                        int iCodpadre = 0;
                        if (PageSource.Request.QueryString["ZonaGeo"] != null && PageSource.Request.QueryString["ZonaGeo"] != "")
                            iCodpadre = Convert.ToInt32(PageSource.Request.QueryString["ZonaGeo"]);
                        dtDatosUbic = cConsPlanes.ConsultarPaises_CiudadesPlanes(true, iCodpadre, Empresa);
                        csGenerales csRefere = new csGenerales();
                        if (dtDatosUbic != null && dtDatosUbic.Rows.Count > 0)
                            csRefere.LlenarControlData(ddlPais, Enum_Controls.DropDownList, "intCode", "strDescripcion", true, false, null, dtDatosUbic);
                    }
                    catch { }
                }


            }
            catch { }
        }
        public void setPaisesZona(UserControl PageSource, string Empresa, int strZona)
        {
            try
            {
                AjaxControlToolkit.CascadingDropDown cddZona =
                    PageSource.FindControl("cddZona") as AjaxControlToolkit.CascadingDropDown;
                if (cddZona == null)
                {
                    try
                    {
                        csConsultasPlanes cConsPlanes = new csConsultasPlanes();
                        DataTable dtDatosUbic = new DataTable();
                        DropDownList ddlPais = (DropDownList)PageSource.FindControl("ddlPais");

                        dtDatosUbic = cConsPlanes.ConsultarPaises_CiudadesPlanes(true, strZona, Empresa);
                        csGenerales csRefere = new csGenerales();
                        if (dtDatosUbic != null && dtDatosUbic.Rows.Count > 0)
                            csRefere.LlenarControlData(ddlPais, Enum_Controls.DropDownList, "intCode", "strDescripcion", true, false, null, dtDatosUbic);
                    }
                    catch { }
                }


            }
            catch { }
        }
        public void setPaisesZona(UserControl PageSource, string Empresa, string strTipoplan)
        {
            try
            {
                DropDownList ddlZonaGeo = (DropDownList)PageSource.FindControl("ddlZonaGeo");
                if (ddlZonaGeo != null)
                {
                    try
                    {
                        csConsultasPlanes cConsPlanes = new csConsultasPlanes();
                        DataTable dtDatosUbic = new DataTable();


                        dtDatosUbic = cConsPlanes.ConsultarPaises_ZonasSeguros(Empresa, strTipoplan);
                        csGenerales csRefere = new csGenerales();
                        if (dtDatosUbic != null && dtDatosUbic.Rows.Count > 0)
                            csRefere.LlenarControlData(ddlZonaGeo, Enum_Controls.DropDownList, "intZonaGeografica", "STRDESCRIPCION", true, false, null, dtDatosUbic);
                    }
                    catch { }
                }

                var ZonaGeo = PageSource.Request.QueryString["ZonaGeo"];
                if (!string.IsNullOrEmpty(ZonaGeo) && ZonaGeo != "0")
                {
                    ddlZonaGeo.SelectedValue = ZonaGeo;
                }

            }
            catch { }
        }
        public void setTipologias(UserControl PageSource, string Empresa)
        {
            try
            {

                csConsultasPlanes cConsPlanes = new csConsultasPlanes();
                DataTable dtDatosUbic = new DataTable();
                DropDownList ddlTipologia = (DropDownList)PageSource.FindControl("ddlTipologia");
                if (ddlTipologia != null)
                {

                    dtDatosUbic = cConsPlanes.ConsultarTipologias(Empresa);
                    csGenerales csRefere = new csGenerales();
                    if (dtDatosUbic != null && dtDatosUbic.Rows.Count > 0)
                        csRefere.LlenarControlData(ddlTipologia, Enum_Controls.DropDownList, "INTCODIGO", "STRDESCRIPCION", true, false, null, dtDatosUbic);

                }
                var IdTipologia = PageSource.Request.QueryString["IdTipologia"];
                if (!string.IsNullOrEmpty(IdTipologia) && IdTipologia != "0")
                {
                    ddlTipologia.SelectedValue = IdTipologia;
                }


            }
            catch { }
        }

        /// <summary>
        /// Metodo de consulta de ciudades por zona geografica 
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-11-13
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public void setCiudadesZona(UserControl PageSource, string Empresa)
        {
            try
            {
                if (PageSource.Request.QueryString["PaisDestino"] != null)
                {
                    int iCodPais = 0;
                    try
                    {
                        iCodPais = Convert.ToInt32(PageSource.Request.QueryString["PaisDestino"].ToString());
                    }
                    catch { }
                    DataTable dtDatos = new DataTable();
                    csConsultasPlanes cConsPlanes = new csConsultasPlanes();
                    DropDownList ddlCiudad = (DropDownList)PageSource.FindControl("ddlCiudad");
                    DropDownList ddlPais = (DropDownList)PageSource.FindControl("ddlPais");
                    ddlPais.SelectedValue = PageSource.Request.QueryString["PaisDestino"].ToString();
                    dtDatos = cConsPlanes.ConsultarPaises_CiudadesPlanes(false, iCodPais, Empresa);
                    csGenerales csRefere = new csGenerales();
                    if (dtDatos != null && dtDatos.Rows.Count > 0)
                        csRefere.LlenarControlData(ddlCiudad, Enum_Controls.DropDownList, "intCode", "strDescripcion", true, false, null, dtDatos);
                }
            }
            catch { }
        }

        #region Seguros
        /// <summary>
        /// Metodos que realizan el cargue del buscador de seguros
        /// </summary>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Jeison Vargas
        /// Company:        Ssoft Colombia
        /// Fecha:          2014-10-06
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public void setSeguro(UserControl PageSource)
        {
            try
            {
                setMostrar(PageSource, true);
            }
            catch { }
        }
        public void setMostrar(UserControl PageSource, bool bInicial)
        {
            try
            {
                Repeater rptEdadPax = (Repeater)PageSource.FindControl("rptEdadPax");
                DropDownList ddlCantidadPax = (DropDownList)PageSource.FindControl("ddlCantidadPax");
                int iNumNinos = 0;
                if (bInicial)
                    iNumNinos = 1;
                else
                    iNumNinos = Convert.ToInt32(ddlCantidadPax.SelectedValue);
                DataTable tblEdades = new DataTable();
                DataColumn dcEdades = new DataColumn("intEdaPax");
                DataColumn dcPax = new DataColumn("strPax");
                tblEdades.Columns.Add(dcEdades);
                tblEdades.Columns.Add(dcPax);
                for (int i = 0; i < iNumNinos; i++)
                {
                    DataRow drEdadPax = tblEdades.NewRow();
                    tblEdades.Rows.Add(drEdadPax);
                    tblEdades.Rows[i]["strPax"] = "Fecha nacimiento viajero " + Convert.ToString(i + 1);
                }
                tblEdades.AcceptChanges();
                if (rptEdadPax != null)
                {
                    rptEdadPax.DataSource = tblEdades;
                    rptEdadPax.DataBind();
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
                cParametros.Metodo = "setMostrarEdadesNinos";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void GuardarParametrosBusquedaSeguros(UserControl PageSource)
        {
            try
            {
                csGeneralsPag.Idioma(PageSource);

                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    List<string> listaValores = new List<string>();
                    DropDownList ddlZonaGeo = (DropDownList)PageSource.FindControl("ddlZonaGeo");
                    DropDownList ddlCantidadPax = (DropDownList)PageSource.FindControl("ddlCantidadPax");
                    TextBox txtFechaSalidaTarjetas = (TextBox)PageSource.FindControl("txtFechaSalidaTarjetas");
                    TextBox txt2TFechaRegresoTarjetas = (TextBox)PageSource.FindControl("txt2TFechaRegresoTarjetas");
                    Repeater rptEdadPax = (Repeater)PageSource.FindControl("rptEdadPax");

                    if (ddlZonaGeo.SelectedValue.Equals("") || ddlZonaGeo.SelectedValue.Equals("0") ||
                        txtFechaSalidaTarjetas.Text.Trim().Equals("") || txt2TFechaRegresoTarjetas.Text.Trim().Equals("") ||
                        getVaciosFechasNac(rptEdadPax))
                    {
                        Label lblErrorSeguros = (Label)PageSource.FindControl("lblErrorSeguros");
                        if (lblErrorSeguros != null)
                            lblErrorSeguros.Text = "Por favor completa todos los parametros de búsqueda";
                    }
                    else
                    {
                        cCache.DatosAdicionales = null;

                        if (ddlZonaGeo != null)
                        {
                            if (ddlZonaGeo.SelectedItem != null)
                                listaValores.Add(ddlZonaGeo.SelectedItem.Value);
                            else
                                listaValores.Add("");
                        }

                        if (txtFechaSalidaTarjetas != null)
                            listaValores.Add(txtFechaSalidaTarjetas.Text.Trim());

                        if (txt2TFechaRegresoTarjetas != null)
                            listaValores.Add(txt2TFechaRegresoTarjetas.Text.Trim());

                        if (ddlCantidadPax != null)
                        {
                            if (ddlCantidadPax.SelectedItem != null)
                                listaValores.Add(ddlCantidadPax.SelectedItem.Value);
                            else
                                listaValores.Add("");
                        }

                        string sFechasNac = "";
                        for (int i = 0; i < rptEdadPax.Items.Count; i++)
                        {
                            TextBox txtFecha = (TextBox)rptEdadPax.Items[i].FindControl("txtNacimientoFecha");
                            if (i == rptEdadPax.Items.Count - 1)
                                sFechasNac = sFechasNac + txtFecha.Text;
                            else
                                sFechasNac = sFechasNac + txtFecha.Text + ",";
                        }

                        listaValores.Add(sFechasNac);
                        string strUrl = "";
                        if (ddlZonaGeo != null)
                        {
                            if (ddlZonaGeo.SelectedItem != null)
                                strUrl = "&ZnaTex=" + ddlZonaGeo.SelectedItem.Text;
                            else
                                strUrl = "";
                        }

                        cCache.DatosAdicionales = listaValores;
                        csCache.ActualizarCache(cCache);

                        clsValidaciones.RedirectPagina("ResultadoSeguros.aspx?TipoPlan=" + clsValidaciones.GetKeyOrAdd("TipoPlanTarjetas", "TJAS") + "&id=1" + strUrl);
                    }

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
                cParametros.Metodo = "setGuardarParametrosBusqueda";
                cParametros.Complemento = "Error en al guardar los parametros de busqueda en traslados";
                ExceptionHandled.Publicar(cParametros);
            }

        }
        public bool getVaciosFechasNac(Repeater rpt)
        {
            bool bVacios = false;
            try
            {
                for (int i = 0; i < rpt.Items.Count; i++)
                {
                    TextBox txtNacimientoFecha = (TextBox)rpt.Items[i].FindControl("txtNacimientoFecha");
                    if (txtNacimientoFecha.Text.Trim().Equals(""))
                        bVacios = true;
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
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.Complemento = Ex.Message;
                ExceptionHandled.Publicar(cParametros);
                bVacios = true;
            }
            return bVacios;
        }

        #endregion
    }
}
