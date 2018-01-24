using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Xml;
using System.IO;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;
using Ssoft.Rules.Administrador;
using Ssoft.Rules.Corporativo;
using Ssoft.Rules.Generales;
using Ssoft.DataNet;
using Ssoft.Rules.WebServices;
using Ssoft.Security;
using Ssoft.ValueObjects;
using Ssoft.Utils;
using SsoftQuery.Vuelos;

namespace Ssoft.Pages
{
    public static class csGeneralsPag
    {
        // Columnas de referencia
        public const string COLUMN_ID_REFERE = "intidRefere";
        public const string COLUMN_REFERE = "strRefere";
        public const string COLUMN_DETALLE = "strDetalle";
        public static string sValidaEncabezado = clsValidaciones.GetKeyOrAdd("bValidaEncabezado", "False");
        // Parametros de encabezado
        private const string PARAMETRO_PLAN = "CodigoPlan";
        private const string PARAMETRO_CONVENIO = "Convenio";
        // Paginas a redicecionar
        private const string PARAMETRO_DETALLE_PLAN = "";
        private const string PARAMETRO_DETALLE_CONVENIO = "";
        public static void Paginar(Repeater rpt, DataTable tblDatos)
        {
            rpt.DataSource = tblDatos.DefaultView;
            rpt.DataBind();
        }
        public static void Paginar(Repeater rpt, DataTable tblDatos, int Pagina, DataList dlPag)
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = tblDatos.DefaultView;
            DataTable dtPaginas = new DataTable();
            dtPaginas.Columns.Add("Pagina").AutoIncrement = true;
            dtPaginas.Columns.Add("Class");
            dtPaginas.Columns["Pagina"].AutoIncrementSeed = 1;
            dtPaginas.Columns["Pagina"].AutoIncrementStep = 1;
            pagedData.AllowPaging = true;
            pagedData.PageSize = 30;
            for (int p = 0; p < pagedData.PageCount; p++)
            {
                dtPaginas.Rows.Add();
                if (p.Equals(Pagina))
                {
                    dtPaginas.Rows[p]["Class"] = "csPintar";
                }
                else
                {
                    dtPaginas.Rows[p]["Class"] = "csNoPintar";
                }
            }
            dlPag.DataSource = dtPaginas;
            dlPag.DataBind();
            try
            {
                pagedData.CurrentPageIndex = Pagina;
            }
            catch
            {
                pagedData.CurrentPageIndex = 0;
            }
            rpt.DataSource = pagedData;
            rpt.DataBind();
        }
        public static void Paginar(DataList rpt, DataTable tblDatos, int Pagina, DataList dlPag)
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = tblDatos.DefaultView;
            DataTable dtPaginas = new DataTable();
            dtPaginas.Columns.Add("Pagina").AutoIncrement = true;
            dtPaginas.Columns.Add("Class");
            dtPaginas.Columns["Pagina"].AutoIncrementSeed = 1;
            dtPaginas.Columns["Pagina"].AutoIncrementStep = 1;
            pagedData.AllowPaging = true;
            pagedData.PageSize = 30;
            for (int p = 0; p < pagedData.PageCount; p++)
            {
                dtPaginas.Rows.Add();
                if (p.Equals(Pagina))
                {
                    dtPaginas.Rows[p]["Class"] = "csPintar";
                }
                else
                {
                    dtPaginas.Rows[p]["Class"] = "csNoPintar";
                }
            }
            dlPag.DataSource = dtPaginas;
            dlPag.DataBind();
            try
            {
                pagedData.CurrentPageIndex = Pagina;
            }
            catch
            {
                pagedData.CurrentPageIndex = 0;
            }
            rpt.DataSource = pagedData;
            rpt.DataBind();
        }
        public static void Paginar(Repeater rpt, DataTable tblDatos, int Pagina, DataList dlPag, int PageSize)
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = tblDatos.DefaultView;
            DataTable dtPaginas = new DataTable();
            dtPaginas.Columns.Add("Pagina").AutoIncrement = true;
            dtPaginas.Columns.Add("Class");
            dtPaginas.Columns["Pagina"].AutoIncrementSeed = 1;
            dtPaginas.Columns["Pagina"].AutoIncrementStep = 1;
            pagedData.AllowPaging = true;
            pagedData.PageSize = PageSize;
            for (int p = 0; p < pagedData.PageCount; p++)
            {
                dtPaginas.Rows.Add();
                if (p.Equals(Pagina))
                {
                    dtPaginas.Rows[p]["Class"] = "csPintar";
                }
                else
                {
                    dtPaginas.Rows[p]["Class"] = "csNoPintar";
                }
            }
            dlPag.DataSource = dtPaginas;
            dlPag.DataBind();
            try
            {
                pagedData.CurrentPageIndex = Pagina;
            }
            catch
            {
                pagedData.CurrentPageIndex = 0;
            }
            rpt.DataSource = pagedData;
            rpt.DataBind();
        }
        public static void Paginar(DataList rpt, DataTable tblDatos, int Pagina, DataList dlPag, int PageSize)
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = tblDatos.DefaultView;
            DataTable dtPaginas = new DataTable();
            dtPaginas.Columns.Add("Pagina").AutoIncrement = true;
            dtPaginas.Columns.Add("Class");
            dtPaginas.Columns["Pagina"].AutoIncrementSeed = 1;
            dtPaginas.Columns["Pagina"].AutoIncrementStep = 1;
            pagedData.AllowPaging = true;
            pagedData.PageSize = PageSize;
            for (int p = 0; p < pagedData.PageCount; p++)
            {
                dtPaginas.Rows.Add();
                if (p.Equals(Pagina))
                {
                    dtPaginas.Rows[p]["Class"] = "csPintar";
                }
                else
                {
                    dtPaginas.Rows[p]["Class"] = "csNoPintar";
                }
            }
            dlPag.DataSource = dtPaginas;
            dlPag.DataBind();
            try
            {
                pagedData.CurrentPageIndex = Pagina;
            }
            catch
            {
                pagedData.CurrentPageIndex = 0;
            }
            rpt.DataSource = pagedData;
            rpt.DataBind();
        }
        public static void setPagina(DataList dtlPag, int iTotal, int iPos)
        {
            try
            {
                string sPag = "tblPaginador";
                string stext = "text";
                string svalue = "value";
                string sclass = "class";

                DataTable tblPag = new DataTable(sPag);
                DataColumn dctext = new DataColumn(stext);
                DataColumn dcvalue = new DataColumn(svalue);
                DataColumn dcclass = new DataColumn(sclass);

                tblPag.Columns.Add(dctext);
                tblPag.Columns.Add(dcvalue);
                tblPag.Columns.Add(dcclass);

                for (int j = 1; j <= iTotal; j++)
                {
                    DataRow filaPag = tblPag.NewRow();
                    filaPag[stext] = j.ToString();
                    filaPag[svalue] = j.ToString();
                    if (iPos == j)
                    {
                        filaPag[sclass] = "csPintar";
                    }
                    else
                    {
                        filaPag[sclass] = "csNoPintar";
                    }

                    tblPag.Rows.Add(filaPag);
                }
                dtlPag.DataSource = tblPag;
                dtlPag.DataBind();
            }
            catch { }
        }
        public static void Imprimir()
        {
            Page PageActual = (Page)HttpContext.Current.Handler;
            PageActual.RegisterClientScriptBlock("Imprimir", "<script>window.print();</script>");
        }
        public static void Cortinilla()
        {
            Page PageActual = (Page)HttpContext.Current.Handler;
            PageActual.RegisterClientScriptBlock("Cortinilla", "<script>Show_CortinillaMenu();</script>");
        }
        public static void Regresar()
        {
            Page PageActual = (Page)HttpContext.Current.Handler;
            PageActual.RegisterClientScriptBlock("Regresar", "<script>history.back(-1);</script>");
        }
        public static void Cambiar(string sUrl)
        {
            Page PageActual = (Page)HttpContext.Current.Handler;
            sUrl = UrlPath() + sUrl;
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=Javascript>\n");
            sb.Append("alert(" + sUrl + ");\n");
            sb.Append("parent.location.href = '" + sUrl + "'; \n");
            sb.Append("self.close(); \n");
            sb.Append("</script>");

            PageActual.RegisterStartupScript("script", sb.ToString());
        }
        public static void Cambiar()
        {
            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {
                Cambiar(cCache.Pagina);
            }
        }
        public static void Login()
        {
            Page PageActual = (Page)HttpContext.Current.Handler;
            PageActual.Response.Redirect("../Default.aspx", true);
        }
        public static void EnviarPaginaQueryActual(string sPagina)
        {
            Page PageActual = (Page)HttpContext.Current.Handler;
            string sUrl = sPagina + PageActual.Request.Url.Query.ToString();
            PageActual.Response.Redirect(sUrl, true);
        }
        public static string QueryActual()
        {
            Page PageActual = (Page)HttpContext.Current.Handler;
            string sUrl = PageActual.Request.Url.Query.ToString();
            return sUrl;
        }
        /// <summary>
        /// Metodo para tomar un QueryString segun la posicion
        /// </summary>
        /// <returns></returns>
        public static string QueryForm(int iPos)
        {
            Page PageActual = (Page)HttpContext.Current.Handler;
            string sQuery = string.Empty;
            try
            {
                if (PageActual.Request.Form.Count > iPos)
                    sQuery = PageActual.Request.Form[iPos].ToString();
            }
            catch { }
            return sQuery;
        }
        public static string QueryString(int iPos)
        {
            Page PageActual = (Page)HttpContext.Current.Handler;
            string sQuery = string.Empty;
            try
            {
                if (PageActual.Request.QueryString.Count < iPos)
                    sQuery = PageActual.Request.QueryString[iPos].ToString();
            }
            catch { }
            return sQuery;
        }
        public static void EnviarPaginaQueryAnterior(string sPagina)
        {
            Page PageActual = (Page)HttpContext.Current.Handler;
            string sUrl = sPagina + PageActual.Request.UrlReferrer.Query.ToString();
            PageActual.Response.Redirect(sUrl, true);
        }
        public static string UrlPath()
        {
            string sPagina = string.Empty;
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                sPagina = PageActual.Request.Url.Scheme + "://" + PageActual.Request.Url.Authority;
                int iSegment = PageActual.Request.Url.Segments.Length;
                iSegment--;
                for (int i = 0; i < iSegment; i++)
                {
                    sPagina += PageActual.Request.Url.Segments[i].ToString();
                }
            }
            catch { }
            return sPagina;
        }
        public static string UrlRaiz()
        {
            string sPagina = string.Empty;
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                sPagina = PageActual.Request.Url.Scheme + "://" + PageActual.Request.Url.Authority;
                int iSegment = PageActual.Request.Url.Segments.Length;
                iSegment--;
                iSegment--;
                for (int i = 0; i < iSegment; i++)
                {
                    sPagina += PageActual.Request.Url.Segments[i].ToString();
                }
            }
            catch { }
            return sPagina;
        }
        public static string UrlPathCarpeta(string sCarpeta)
        {
            string sPagina = UrlRaiz();
            try
            {
                string[] sLista = null;
                int iCont = 0;
                if (sCarpeta.Contains("/"))
                {
                    sLista = clsValidaciones.Lista(sCarpeta, "/");
                    iCont = sLista.Length;
                    if (iCont > 0)
                    {
                        iCont--;
                        iCont--;
                        sCarpeta = sLista[iCont].ToString() + "/";
                    }
                }
                else
                {
                    if (sCarpeta.Contains(@"\"))
                    {
                        sLista = clsValidaciones.Lista(sCarpeta, @"\");
                        iCont = sLista.Length;
                        if (iCont > 0)
                        {
                            iCont--;
                            iCont--;
                            sCarpeta = sLista[iCont].ToString() + "/";
                        }
                    }
                }
                sPagina += sCarpeta;
            }
            catch { }
            return sPagina;
        }
        public static string PaginaActual()
        {
            string sPagina = string.Empty;
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                int iSegment = PageActual.Request.Url.Segments.Length;
                iSegment--;
                sPagina = PageActual.Request.Url.Segments[iSegment].ToString();
            }
            catch { }
            return sPagina;
        }
        public static string PaginaAnterior()
        {
            string sPagina = string.Empty;
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                int iSegment = PageActual.Request.UrlReferrer.Segments.Length;
                iSegment--;
                sPagina = PageActual.Request.UrlReferrer.Segments[iSegment].ToString();
            }
            catch { }
            return sPagina;
        }
        public static string UrlAnterior()
        {
            string sPagina = string.Empty;
            string sQuery = string.Empty;
            string sUrl = string.Empty;
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                int iSegment = PageActual.Request.UrlReferrer.Segments.Length;
                iSegment--;
                sPagina = PageActual.Request.UrlReferrer.Segments[iSegment].ToString();
                sQuery = PageActual.Request.UrlReferrer.Query.ToString();
                sUrl = sPagina + sQuery;
            }
            catch { }
            return sUrl;
        }
        public static string UrlActual()
        {
            string sPagina = string.Empty;
            string sQuery = string.Empty;
            string sUrl = string.Empty;
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                int iSegment = PageActual.Request.Url.Segments.Length;
                iSegment--;
                sPagina = PageActual.Request.Url.Segments[iSegment].ToString();
                sQuery = PageActual.Request.Url.Query.ToString();
                if (!sQuery.Contains("Index"))
                {
                    sUrl = sPagina + sQuery;
                }
                else
                {
                    if (new csMenu().ValidaParametros())
                        sUrl = sPagina;
                    else
                        sUrl = sPagina + sQuery;
                }
            }
            catch { }
            return sUrl;
        }
        public static string UrlCompleta()
        {
            string sPagina = string.Empty;
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                sPagina = PageActual.Request.Url.ToString();
            }
            catch { }
            return sPagina;
        }
        public static void GuardarUrl()
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    cCache.Pagina = UrlActual();
                    csCache.ActualizarCache(cCache);
                }
            }
            catch { }
        }
        public static void UrlCache()
        {
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    PageActual.Response.Redirect(cCache.Pagina, true);
                }
                else
                {
                    Login();
                }
            }
            catch { }
        }
        public static void Back()
        {
            Page PageActual = (Page)HttpContext.Current.Handler;
            PageActual.RegisterClientScriptBlock("Back", "<script>javascript:history.back();</script>");
        }
        public static void RedirectPaginaActual()
        {
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                string sPage = UrlActual();
                PageActual.Response.Redirect(sPage, true);
            }
            catch { }
        }
        public static void RedirectPaginaUrl(string sPagina)
        {
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                string sUrl = UrlActual();
                string sPaginaRedirec = sUrl + sPagina;
                PageActual.Response.Redirect(sPaginaRedirec, true);
            }
            catch { }
        }
        public static string LogoAgencia(string sContaco)
        {
            string sImagen = string.Empty;
            try
            {
                csContactos cContactos = new csContactos();
                DataSet dsData = new DataSet();
                dsData = cContactos.ConsultaDatosContacto(sContaco, string.Empty);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    sImagen = dsData.Tables[0].Rows[0]["strImagen"].ToString();
                }
            }
            catch { }
            return sImagen;
        }
        /// <summary>
        /// Metodo para verificar la cache
        /// </summary>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          José Faustino Posas
        /// Fecha:          2011-11-30
        /// Descripción:    Se adiciona validacion de borrado de cache clsCacheControl().BorrarCache()
        /// </remarks>
        /// <returns>bRetorno, booleano que identifica si hay oi bo cache</returns>
        public static bool SesionIdPage()
        {
            try { new clsCacheControl().BorrarCache(); }
            catch { }



            bool bRetorno = false;
            Page PaginaActual = (Page)HttpContext.Current.Handler;
            HiddenField hdSesion = (HiddenField)PaginaActual.FindControl("hdfSesionId");
            clsParametros cParametros = new clsParametros();
            clsCache cCache = new csCache().cCache();
            clsCacheControl cCacheControl = new clsCacheControl();
            if (cCache != null)
            {
                if (cCache.SessionID.ToString() != "")
                {
                    HttpContext.Current.Session["Session"] = null;
                    HttpContext.Current.Session["Session"] = cCache.SessionID.ToString();
                }
                else if (HttpContext.Current.Request["Session"] != null)
                {
                    cCache.SessionID = HttpContext.Current.Request["Session"].ToString();
                    cCacheControl.GuardaSesion(cCache.SessionID.ToString());
                    cCacheControl.RecuperarXML(cCache.SessionID.ToString());
                    HttpContext.Current.Session["Session"] = null;
                    HttpContext.Current.Session["Session"] = cCache.SessionID.ToString();
                }


                if (ValidaTiempoCache(cCache))
                {
                    ActualizaUrls(cCache);
                    if (hdSesion != null)
                    {
                        if (hdSesion.Value.Length.Equals(0))
                        {
                            hdSesion.Value = cCache.SessionID.ToString();
                        }
                    }
                    else
                    {
                        cParametros.Id = 0;
                        cParametros.Message = "El campo oculto hdfSesionId, no existe en la pagina " + PaginaActual.ToString();
                        cParametros.Tipo = clsTipoError.Library;
                        cParametros.Severity = clsSeveridad.Alta;
                        try
                        {
                            cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        }
                        catch { }
                        cParametros.Info = "Sesion recuperada " + cCache.SessionID.ToString();
                        ExceptionHandled.Publicar(cParametros);
                    }
                    cCacheControl.GuardaSesion(cCache.SessionID.ToString());
                    if (cCache.SessionID.ToString() != "")
                    {
                        bRetorno = true;
                    }
                    if (HttpContext.Current.Request["Session"] != null && cCache.SessionID.ToString() == "")
                    {
                        cCacheControl.GuardaSesion(HttpContext.Current.Request["Session"].ToString());
                    }
                }
            }
            try
            {
                if (!bRetorno)
                {
                    FinSesion();
                    bRetorno = true;
                    if (HttpContext.Current.Request["Session"] == null)
                    {
                        if (HttpContext.Current.Session["idSession"] != null)
                        {
                            HttpContext.Current.Session["Session"] = HttpContext.Current.Session["idSession"].ToString();
                            if (cCache != null)
                            {
                                cCache.SessionID = HttpContext.Current.Session["idSession"].ToString();
                            }
                        }

                        cCacheControl.GuardaSesion(cCache.SessionID.ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Alta;
                try
                {
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.InnerException = Ex.InnerException.Message.ToString();
                    cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                }
                catch { }
                cParametros.Info = "No se puede recuperar la sesion";
                ExceptionHandled.Publicar(cParametros);
                bRetorno = false;
            }
            return bRetorno;
        }
        public static void SesionIdPage(clsCache cCache)
        {
            Page PaginaActual = (Page)HttpContext.Current.Handler;
            HiddenField hdSesion = (HiddenField)PaginaActual.FindControl("hdfSesionId");
            clsParametros cParametros = new clsParametros();
            clsCacheControl cCacheControl = new clsCacheControl();
            try
            {
                if (hdSesion != null)
                {
                    hdSesion.Value = cCache.SessionID.ToString();
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Message = "El campo oculto hdfSesionId, no existe en la pagina " + PaginaActual.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Alta;
                    try
                    {
                        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    }
                    catch { }
                    cParametros.Info = "Sesion recuperada " + cCache.SessionID.ToString();
                    ExceptionHandled.Publicar(cParametros);
                }
                cCacheControl.GuardaSesion(cCache.SessionID.ToString());
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Alta;
                try
                {
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.InnerException = Ex.InnerException.Message.ToString();
                    cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                }
                catch { }
                cParametros.Info = "No se puede recuperar la sesion";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public static void ActualizaUrls(clsCache cCache)
        {
            try
            {
                bool bActualiza = false;
                string sUrl = UrlCompleta();
                if (!cCache.UrlAnterior.Equals(sUrl))
                {
                    if (!cCache.UrlActual.Equals(sUrl))
                    {
                        cCache.UrlAnterior = cCache.UrlActual;
                        cCache.UrlActual = sUrl;
                        bActualiza = true;
                    }
                }
                try
                {
                    if (!cCache.TiempoCache.Equals(0))
                    {
                        cCache.TiempoInicial = DateTime.Now.ToString();
                        cCache.TiempoFinal = DateTime.Now.AddMinutes(cCache.TiempoCache).ToString();
                        bActualiza = true;
                    }
                }
                catch { }
                if (bActualiza)
                    csCache.ActualizarCache(cCache);
            }
            catch { }
        }
        public static bool ValidaTiempoCache(clsCache cCache)
        {
            bool bValida = true;
            try
            {
                if (!cCache.TiempoCache.Equals(0))
                {
                    DateTime dtDateFin = Convert.ToDateTime(cCache.TiempoFinal);
                    if (DateTime.Now > dtDateFin)
                    {
                        clsCacheControl cCacheControl = new clsCacheControl();
                        cCacheControl.EliminarCache(cCache.SessionID);
                        cCache = null;
                        bValida = false;
                    }
                }
            }
            catch { }
            return bValida;
        }
        public static void CrearCacheVaciaSesion(string sesion)
        {
            try
            {

                clsCache cCache = new clsCache();
                clsCacheControl cCacheControl = new clsCacheControl();

                string sTipoCliente = clsValidaciones.GetKeyOrAdd("TipoCliente", "TPO_CLIENTE");
                string sCliente = clsValidaciones.GetKeyOrAdd("Cliente", "AG");
                string sUsuario = clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF");

                string sPropietario = clsValidaciones.GetKeyOrAdd("idPropietario", "0");
                string sAgencia = clsValidaciones.GetKeyOrAdd("idAgencia", "0");
                string sEmpresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "0");
                string sComunidad = clsValidaciones.GetKeyOrAdd("idComunidad", "0");
                string sUNegocio = clsValidaciones.GetKeyOrAdd("idUNegocio", "0");
                string sUrlActual = UrlCompleta();
                string sCulture = clsValidaciones.GetKeyOrAdd("culture", "es");
                string sIdioma = clsSesiones.getIdioma();
                string sAplicacion = clsSesiones.getAplicacion().ToString();
                string sFormatoFecha = clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");
                string sFormatoFechaBD = clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");


                HttpContext.Current.Session.Add("SessionIDLocal", sesion);
                cCache.SessionID = sesion;
                cCache.Contacto = "0";
                cCache.Pagina = "default.aspx";
                cCache.Idioma = sIdioma;
                cCache.Cultura = sCulture;
                cCache.Empresa = sEmpresa;
                cCache.UrlActual = sUrlActual;
                cCache.UrlAnterior = sUrlActual;
                cCache.Aplicacion = sAplicacion;
                cCache.Proyecto = "0";
                cCache.Verifica = false;
                cCache.FormatoFecha = sFormatoFecha;
                cCache.FormatoFechaBD = sFormatoFechaBD;

                cCache.RefereContacto = sUsuario;
                cCache.Empresa = sEmpresa;


                cCache.UNegocio = sUNegocio;


                clsSesiones.setProyecto("0");
                csCache.ActualizarCache(cCache);
                //csSeguridad cSeguridad = new csSeguridad();
                //cSeguridad.csConexionWs(cCache, false);
                cCacheControl.RecuperarSesionId(sesion);
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                cParametros.Info = "No se puede generar la Cache, borre el directorio Cache del XML de la aplicacion";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public static void CrearCacheVacia()
        {
            try
            {


                setExterno(new csCache().csCodigoExterno());
                clsCache cCache = new csCache().cCache();

            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                cParametros.Info = "No se puede generar la Cache, borre el directorio Cache del XML de la aplicacion";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        /// <summary>
        /// 
        /// </summary>

        /// <summary>
        /// Metodo para crear la cahce vacia de la empresa
        /// </summary>
        /// <param name="sEmpresa">Id de empresa</param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-12-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:          
        /// Descripción:    
        /// </remarks>
        public static void CrearCacheVaciaEmp(string sEmpresa)
        {
            try
            {
                //csLogin cLogin = new csLogin();
                //cLogin.setExterno(sEmpresa);
                //clsCache cCache = new csCache().cCache();
                //if (cCache != null)
                //{
                //    csSeguridad cSeguridad = new csSeguridad();
                //    cSeguridad.csConexionWs(cCache, false);
                //}
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                cParametros.Info = "No se puede generar la Cache, borre el directorio Cache del XML de la aplicacion";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public static void CrearCacheVacia(string Url)
        {
            try
            {
                string sUrl = Url;

                CrearCacheVacia();

                clsValidaciones.RedirectPagina(sUrl, false);
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                try
                {
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.InnerException = Ex.InnerException.Message.ToString();
                    cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                }
                catch { }
                cParametros.Info = "No se puede generar la Cache, borre el directorio Cache del XML de la aplicacion";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public static void FinSesion()
        {
            try
            {
                string sUrl = UrlActual();
                string sUrlAnterior = sUrl;
                string sTipoPagina = clsValidaciones.GetKeyOrAdd("Cliente", "UF");
                string sPagina = "~/presentacion/Default.aspx";

                if (sTipoPagina.Equals("AG"))
                {
                    clsValidaciones.RedirectPaginaIni(sPagina, true);
                }
                else
                {
                    if (sTipoPagina.ToUpper().Equals("SABRE"))
                    {
                        DefaultSabre(sPagina);
                    }
                    else
                    {

                        if (sUrl.Contains("Reserva") || sUrl.Contains("MiCuenta"))
                        {
                            try
                            {
                                clsCache cCache = new csCache().cCache();
                                if (cCache != null)
                                {
                                    clsCacheControl cCacheControl = new clsCacheControl();
                                    cCache.UrlAnterior = sUrlAnterior;
                                    cCache.UrlActual = sUrl;

                                    cCacheControl.ActualizaXML(cCache);
                                }
                                else
                                {
                                    sUrl = LoginSesion();

                                    clsSesiones.setPantalleRespuestaLogin(sUrlAnterior);
                                    clsParametros cParametros = new clsParametros();
                                    cParametros.Id = 0;
                                    cParametros.ViewMessage.Add("Perdida de sesion");
                                    cParametros.ViewMessage.Add("Por favor registrese nuevamente");
                                    clsSesiones.setParametrosError(cParametros);
                                    clsValidaciones.RedirectPagina(sUrl, false);
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            CrearCacheVacia();
                        }

                    }
                }
            }
            catch
            {
                //cParametros.Info = "No se puede generar la Cache, borre el directorio Cache del XML de la aplicacion";
                //ExceptionHandled.Publicar(cParametros);
            }
        }
        public static void ValidarSesionPag()
        {
            try
            {
                clsCacheControl cCacheControl = new clsCacheControl();
                string sSesion = cCacheControl.RecuperarSesionId();
                if (sSesion == null)
                {
                    SesionIdPage();
                }
            }
            catch { }
        }
        /// <summary>
        /// Validacion de pagos por emision de tiquete del pais por dafault
        /// </summary>
        /// <param name="tblItinerario">tBala de itinerarios</param>
        /// <returns>Validacion</returns>
        /// <remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-18
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:          
        /// Descripción:    
        /// </remarks>
        public static bool setValidarPaisDefaultAir(DataTable tblItinerario)
        {
            bool Valida = true;
            try
            {
                string sValidaPaisDefaultAir = clsValidaciones.GetKeyOrAdd("ValidaPaisDeafultAir", "False");
                if (sValidaPaisDefaultAir.ToUpper().Equals("TRUE"))
                {
                    if (tblItinerario.Rows.Count > 0)
                    {
                        string sPlanAir = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
                        string sTipoPlan = clsValidaciones.GetKeyOrAdd("TiposPlan", "TipoPlan");

                        //tblRefere otblRefere = new tblRefere();
                        //otblRefere.Get(sTipoPlan, sPlanAir);
                        //if (otblRefere.Respuesta)
                        //{
                        //    DataTable dtData = new DataTable();
                        //    string sWhere = "intTipoPlan='" + otblRefere.intidRefere.Value + "'";
                        //    dtData = clsDataNet.dsDataWhere(sWhere, tblItinerario);
                        //    if (dtData.Rows.Count > 0)
                        //    {
                        //        Valida = csVuelos.getValidarPaisDefault(tblItinerario);
                        //    }
                        //}
                    }
                }
            }
            catch
            {
            }
            return Valida;
        }
        public static void DefaultSabre(string sPagina)
        {
            clsParametros cParametros = new clsParametros();
            //string[] sValue = new csLogin().csValue();
            //string ticketNo = sValue[0];

            //cParametros.Id = 0;
            //cParametros.Tipo = clsTipoError.Aplication;
            //cParametros.Severity = clsSeveridad.Alta;
            //cParametros.Message = "Referencia del Ticket - verificando";
            //cParametros.Complemento = "Ticket No " + ticketNo;
            //ExceptionHandled.Publicar(cParametros);
            //clsValidaciones.RedirectPaginaIni(sPagina + "?ticketNo=" + ticketNo, true);
        }
        public static void Default()
        {
            try { new clsCacheControl().EliminarCache(); }
            catch { }
            try { EliminaCookie(); }
            catch { }
            string sTipoPagina = clsValidaciones.GetKeyOrAdd("Cliente", "UF");
            string sPagina = "~/presentacion/Default.aspx";

            if (Externo())
            {
                sPagina = "~/Presentacion/" + IndexExterno();
                clsValidaciones.RedirectPaginaIni(sPagina, false);
            }
            else
            {
                if (sTipoPagina.Equals("UF"))
                {
                    sPagina = "~/Presentacion/" + Index();
                    clsValidaciones.RedirectPaginaIni(sPagina, false);
                }
                else
                {
                    sPagina = "~/Presentacion/" + Index();
                    CrearCacheExterna(sPagina);
                }
            }

        }
        public static void CrearCacheExterna(string Url)
        {
            try
            {
                string sUrl = Url;
                //string sUnion = "?";
                if (clsValidaciones.GetKeyOrAdd("LoginExterno", "False").ToUpper().Equals("TRUE"))
                {
                    //csLogin cLogin = new csLogin();
                    //cLogin.setExterno();
                    clsCache cCache = new csCache().cCache();
                }
                else
                {
                    sUrl = clsValidaciones.ObtenerUrlRutaPage("Default.aspx");
                }
                clsValidaciones.RedirectPaginaIni(sUrl, true);
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                //cParametros.StackTrace = Ex.StackTrace.ToString();
                //cParametros.InnerException = Ex.InnerException.Message.ToString();
                cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                cParametros.Info = "No se puede generar la Cache, borre el directori Cache del XML de la aplicacion";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public static bool Externo()
        {
            bool bValue = false;
            try
            {
                if (HttpContext.Current.Request.QueryString["Externo"] != null)
                {
                    string sTemp = HttpContext.Current.Request.QueryString["Externo"].ToString();
                    //if (sTemp.Equals("1"))
                    bValue = true;
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
                cParametros.Complemento = "Referencia de Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
            return bValue;
        }
        public static bool Oferta()
        {
            bool bValue = false;
            try
            {
                if (HttpContext.Current.Request.QueryString["IdOferta"] != null)
                {
                    string sTemp = HttpContext.Current.Request.QueryString["IdOferta"].ToString();
                    //if (sTemp.Equals("1"))
                    bValue = true;
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
                cParametros.Complemento = "Referencia de Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
            return bValue;
        }
        public static bool Convenio()
        {
            bool bValue = false;
            try
            {
                if (HttpContext.Current.Request.QueryString["Convenio"] != null)
                {
                    bValue = true;
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
                cParametros.Complemento = "Referencia de Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
            return bValue;
        }
        public static string Index()
        {
            string sPagina = "Index.aspx";


            return sPagina;
        }
        public static string IndexExterno()
        {
            string sPagina = "Index.aspx";
            //try
            //{
            //    sPagina = SoloIndex();
            //}
            //catch { }
            try
            {
                sPagina = sPagina + QueryActual();
            }
            catch { }
            return sPagina;
        }
        public static void RedirectIndex()
        {
            try
            {
                string sPagina = Index();
                clsValidaciones.RedirectPagina(sPagina); ;
            }
            catch { }
        }
        public static string LoginSesion()
        {
            string sPagina = "Login.aspx";
            try
            {
                sPagina = clsValidaciones.GetKeyOrAdd("RedirecLogin", "../Default.aspx");
            }
            catch
            {
            }
            return sPagina;
        }
        public static void PaginaRetorno()
        {
            try
            {
                string sUrl = clsValidaciones.GetKeyOrAdd("PaginaRetorno", "Index.aspx");
                if (!sUrl.ToUpper().Contains("INDEX"))
                {
                    csFinSesion cFinSesion = new csFinSesion();
                    cFinSesion.setCancelaSesion();
                }
                clsValidaciones.RedirectPagina(sUrl, true);
            }
            catch
            {
            }
        }
        public static void Buscador()
        {
            string sUrl = clsValidaciones.GetKeyOrAdd("Buscador", "Index.aspx");
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    string sCarrito = new csMenu().setLeerCarrito(cCache);
                    if (!sCarrito.Equals("(0)"))
                    {
                        sUrl = CarroCompras();
                        clsValidaciones.RedirectPagina(sUrl, false);
                    }
                }
                bool bIndex = true;

                if (bIndex)
                {
                    if (sUrl.Contains("Index"))
                        sUrl = Index();
                    clsSesiones.setPantalleRespuestaLogin(null);
                    clsValidaciones.RedirectPagina(sUrl, false);
                }
                else
                {
                    clsSesiones.setPantalleRespuestaLogin(null);
                    clsValidaciones.RedirectPaginaIni(sUrl, false);
                }
            }
            catch
            {
                clsValidaciones.RedirectPagina(sUrl, false);
            }
        }
        public static string CarroCompras()
        {
            return clsValidaciones.GetKeyOrAdd("CarroCompras", "CarroCompras.aspx");
        }
        public static string MiCuenta()
        {
            return clsValidaciones.GetKeyOrAdd("MiCuenta", "MiCuenta.aspx");
        }
        public static string UserControl(UserControl PageSource)
        {
            string sPagina = string.Empty;
            try
            {
                string[] sControls = clsValidaciones.Lista(PageSource.AppRelativeVirtualPath.ToString(), "/");
                int iSegment = sControls.Length;
                iSegment--;
                sPagina = sControls[iSegment].ToString();
            }
            catch { }
            return sPagina;
        }
        public static void Idioma(UserControl PageSource)
        {
            ValidarSesionPag();
            string sPaginaIdioma = clsValidaciones.GetKeyOrAdd("sPaginaIdioma", "False");
            if (sPaginaIdioma.ToUpper().Equals("TRUE"))
            {
                new clsIdioma().LoadIdioma(UserControl(PageSource), PageSource);
            }
        }
        public static void Idioma()
        {
            Page PageSource = (Page)HttpContext.Current.Handler;

            ValidarSesionPag();
            string sPaginaIdioma = clsValidaciones.GetKeyOrAdd("sPaginaIdioma", "False");
            if (sPaginaIdioma.ToUpper().Equals("TRUE"))
            {
                new clsIdioma().LoadIdioma(PaginaActual(), PageSource);
            }
        }
        public delegate void MiDelegado();
        public static void MetaTag()
        {
            Page PaginaActual = (Page)HttpContext.Current.Handler;
            MetaTag(PaginaActual);
        }
        /// <summary>
        /// Metodo pendiente por revision
        /// Metodo para incluir metatags a la pagina, se llama desde el index, donde se redireccionan todas las paginas
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.ssoftcolombia.com
        /// Email: fposas@ssoftcolombia.com
        /// Fecha: 2011-08-15
        /// ------------------
        /// Control de Cambios
        /// ------------------
        /// Descripcion:    Se adiciona el llamado al metodo MetaTagFiltro(dsData), para filtrar en la tabla de los metatags generales, los metatags que vengan especificos por pagina
        /// Fecha:          2011-10-04
        /// Responsable:    José Faustino Posas
        /// ------------------
        /// Descripcion:    Se incluye el titulo especifico por pagina
        /// Fecha:          2011-10-31
        /// Responsable:    José Faustino Posas
        /// -----------------
        /// Descripcion:    Se incluye validacion para que tome el titulo de los objetivos generales;
        /// Fecha:          2011-10-31
        /// Responsable:    Jeison vargas
        /// </remarks>
        /// <param name="pPaginaActual">Nombre de la pagina actual para incluir los metatags, el sistema identifica la pagina para realizar consultas de metatags especificos</param>
        public static void MetaTag(Page pPaginaActual)
        {
            string pPaginaActualId = string.Empty;
            try
            {
                //try
                //{
                //    pPaginaActualId = pPaginaActual.ToString();
                //}
                //catch { }
                //try
                //{
                //    Label lblTitulo = pPaginaActual.FindControl("lblTitulo") as Label;
                //    if (lblTitulo != null)
                //    {
                //        if (pPaginaActual.Request.QueryString["TITULO"] != null)
                //            lblTitulo.Text = pPaginaActual.Request.QueryString["TITULO"].ToString();
                //    }
                //}
                //catch { }
                ////ValidarSesionPag();
                //string sPagina = string.Empty;
                //if (clsValidaciones.GetKeyOrAdd("MetaPagina", "False").ToUpper().Equals("TRUE"))
                //{
                //    sPagina = PaginaActual();
                //}
                //DataSet dsData = new DataSet();


                //dsData = cGeneral.MetaTag(sPagina, true);
                //if (dsData != null)
                //{
                //    // Metodo para filtrar los metatags repetidos
                //    MetaTagFiltro(dsData);
                //    bool bEntra = true;
                //    int iCount = dsData.Tables.Count;
                //    if (dsData.Tables[3].Rows.Count > 0)
                //    {
                //        for (int j = 0; j < dsData.Tables[3].Rows.Count; j++)
                //        {
                //            if (dsData.Tables[3].Rows[j]["Name"].ToString().Equals("title"))
                //            {
                //                pPaginaActual.Title = dsData.Tables[3].Rows[j]["Content"].ToString();
                //                bEntra = false;
                //            }
                //        }
                //    }

                //    if (bEntra)
                //    {
                //        if (dsData.Tables[4].Rows.Count > 0)
                //        {
                //            try
                //            {
                //                if (dsData.Tables[4].Rows[0]["strEmpresa"].ToString().Length > 0)
                //                {
                //                    pPaginaActual.Title = dsData.Tables[4].Rows[0]["strEmpresa"].ToString();
                //                    bEntra = false;
                //                }
                //            }
                //            catch { }
                //        }
                //    }
                //    if (bEntra)
                //    {
                //        if (dsData.Tables[0].Rows.Count >= 0 && dsData.Tables[1].Rows.Count > 0)
                //        {
                //            pPaginaActual.Title = dsData.Tables[1].Rows[0]["content"].ToString();

                //        }
                //        else if (dsData.Tables[0].Rows.Count > 0)
                //        {

                //            pPaginaActual.Title = dsData.Tables[0].Rows[0]["Title"].ToString();
                //        }
                //        else
                //        {
                //            pPaginaActual.Title = clsValidaciones.GetKeyOrAdd("Agencia_Nombre", "Agencia de Viajes");
                //        }

                //    }

                //    try
                //    {
                //        if (dsData.Tables[3].Rows.Count > 0)
                //        {
                //            for (int j = 0; j < dsData.Tables[3].Rows.Count; j++)
                //            {
                //                HtmlMeta htmMeta = new HtmlMeta();
                //                htmMeta.Name = dsData.Tables[3].Rows[j]["Name"].ToString();
                //                htmMeta.Content = dsData.Tables[3].Rows[j]["Content"].ToString();
                //                pPaginaActual.Header.Controls.Add(htmMeta);
                //            }
                //        }
                //        else
                //        {
                //            if (dsData.Tables[5].Rows.Count > 0)
                //            {
                //                for (int j = 0; j < dsData.Tables[5].Rows.Count; j++)
                //                {
                //                    HtmlMeta htmMeta = new HtmlMeta();
                //                    htmMeta.Name = dsData.Tables[5].Rows[j]["Name"].ToString();
                //                    htmMeta.Content = dsData.Tables[5].Rows[j]["Content"].ToString();
                //                    pPaginaActual.Header.Controls.Add(htmMeta);
                //                }
                //            }
                //        }
                //    }
                //    catch { }
                //    if (dsData.Tables[1].Rows.Count > 0)
                //    {
                //        for (int p = 0; p < dsData.Tables[1].Rows.Count; p++)
                //        {
                //            HtmlMeta htmMeta = new HtmlMeta();
                //            htmMeta.Name = dsData.Tables[1].Rows[p]["Name"].ToString();
                //            htmMeta.Content = dsData.Tables[1].Rows[p]["Content"].ToString();
                //            pPaginaActual.Header.Controls.Add(htmMeta);
                //        }
                //    }
                //    if (dsData.Tables[2].Rows.Count > 0)
                //    {
                //        for (int i = 0; i < dsData.Tables[2].Rows.Count; i++)
                //        {
                //            HtmlMeta htmMeta = new HtmlMeta();
                //            htmMeta.HttpEquiv = dsData.Tables[2].Rows[i]["HttpEquiv"].ToString();
                //            htmMeta.Content = dsData.Tables[2].Rows[i]["Content"].ToString();
                //            pPaginaActual.Header.Controls.Add(htmMeta);
                //        }
                //    }
                //}
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
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.Complemento = "Pagina de Referencia" + pPaginaActualId;
                ExceptionHandled.Publicar(cParametros);
            }
        }
        /// <summary>
        /// Metodo para filtrar los metatags generales que esten incluidas en los metatags especificos por pagina
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.ssoftcolombia.com
        /// Email: fposas@ssoftcolombia.com
        /// Fecha: 2011-10-04
        /// ------------------
        /// Control de Cambios
        /// ------------------
        /// Descripcion:    Se cambia nombre de la columna, estaba como Code y es Name    
        /// Fecha:          2011-11-01
        /// Responsable:    José Faustino Posas
        /// </remarks>
        /// <param name="dsData">Dataset de metatags</param>
        private static void MetaTagFiltro(DataSet dsData)
        {
            try
            {
                // Tabla de Metatags Generales
                DataTable dtDataGen = dsData.Tables[1];
                // Tabla de Metatags especificos de la pagina
                DataTable dtDataPag = dsData.Tables[3];

                if (dtDataPag.Rows.Count > 0)
                {
                    int n = 0;
                    int iCount = dtDataPag.Rows.Count;
                    while (n < dtDataGen.Rows.Count)
                    {
                        bool bEliminar = false;
                        for (int i = 0; i < iCount; i++)
                        {
                            if (dtDataPag.Rows[i]["Name"].ToString().Equals(dtDataGen.Rows[n]["Name"].ToString()))
                            {
                                bEliminar = true;
                                break;
                            }
                        }
                        if (bEliminar)
                        {
                            dtDataGen.Rows.Remove(dtDataGen.Rows[n]);
                            n = 0;
                        }
                        else
                        {
                            n++;
                        }
                    }
                }
                dtDataGen.AcceptChanges();
            }
            catch { }
        }
        public static string UrlDocumentos()
        {
            string sPagina = UrlRaiz();
            string sRutaDocumentos = clsValidaciones.RutaDocumentosGen();
            try
            {
                string[] sDocArray = null;
                if (sRutaDocumentos.Contains(@"\"))
                    sDocArray = clsValidaciones.Lista(sRutaDocumentos, @"\");
                else
                    sDocArray = clsValidaciones.Lista(sRutaDocumentos, "/");
                try
                {
                    if (sDocArray[sDocArray.Length - 1].ToString().Equals(""))
                    {
                        sRutaDocumentos = sDocArray[sDocArray.Length - 2].ToString();
                    }
                    else
                    {
                        sRutaDocumentos = sDocArray[sDocArray.Length - 1].ToString();
                    }
                }
                catch { sRutaDocumentos = sDocArray[sDocArray.Length - 2].ToString(); }

            }
            catch { }
            sPagina += sRutaDocumentos;
            return sPagina;
        }
        public static void PageError()
        {
            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {
                Buscador();
            }
            else
            {
                Default();
            }
            //string sPagina = "Presentacion/FinSesion.aspx?TipoError=E";
            //try
            //{
            //    clsValidaciones.RedirectPagina(sPagina, true);
            //}
            //catch { }
        }
        public static void EliminaCookie()
        {
            try
            {
                HttpCookie myCookie = new HttpCookie("SessionID", null);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
            catch { }
            try
            {
                HttpCookie myCookieSsoft = new HttpCookie("UserSsoft", null);
                myCookieSsoft.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookieSsoft);
            }
            catch { }
        }
        /// <summary>
        /// Metodo para borrar cookies que no se necesitan sobre la pagina, elimina toas menos la sesion, ubicacion del index
        /// </summary>
        /// <remarks>
        /// Autor:  Faustino Posas
        /// Fecha:  2011-10-07
        /// -- Control de cambios --
        /// Descripcion:        
        /// Fecha:          
        /// Responsable:    
        /// </remarks>
        public static void DeleteAllCookies()
        {
            string[] cookies = HttpContext.Current.Request.Cookies.AllKeys;
            //StringBuilder consulta = new StringBuilder();
            bool bSesion = true;
            foreach (string cookie in cookies)
            {
                bool bDel = true;
                try
                {
                    switch (cookie)
                    {
                        case "SessionID":
                            if (bSesion)
                            {
                                bDel = false;
                                bSesion = false;
                            }
                            break;
                        case "ASP.NET_SessionId":
                            bDel = false;
                            break;
                        case "intTabIndex":
                            bDel = false;
                            break;
                    }
                }
                catch { }
                //consulta.AppendLine("Deleting " + cookie);
                if (bDel)
                    HttpContext.Current.Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
        }
        public static void Emergente(string sName, bool bShow, string sUserControl)
        {
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                UserControl ucControl = (UserControl)PageActual.FindControl(sUserControl);

                /*MOSTRAMOS LA VENTANA CON EL RECORD Y EL LOCALIZADOR*/
                if (ucControl != null)
                {
                    AjaxControlToolkit.ModalPopupExtender ventanaModalEmergente =
                        ucControl.FindControl(sName) as AjaxControlToolkit.ModalPopupExtender;
                    if (ventanaModalEmergente != null)
                    {
                        if (bShow)
                            ventanaModalEmergente.Show();
                        else
                            ventanaModalEmergente.Hide();
                    }
                }
            }
            catch { }
        }
        //Ejemplos de Meta tags

        //<title>Ssoft Colombia</title>
        //<meta name="title" content="Ssoft Colombia" />
        //<meta name="description" content="Desarrollo Web para el Turismo" />
        //<meta name="keywords" content="Desarrollo Web para Turismo, Tiquetes Aereos, Viajes Terrestres, Cruceros" />
        //<meta name="abstract" content="Conectamos su Agencia con el Mundo" />
        //<meta name="distribution" content="Global" />
        //<meta name="category" content="Turismo y Viajes" />
        //<meta name="language" content="es-co" />
        //<meta name="identifier-url" content="http://ssoftcolombia.com" />
        //<meta name="rating" content="Seguro para niños" />
        //<meta name="author" content="Ssoft Colombia" />
        //<meta name="reply-to" content="info@ssoftcolombia.com" />
        //<meta name="copyright" content="Ssoft Colombia" />
        //<meta name="window-target" content="_parent" />
        //<meta name="resource-type" content="document" />
        //<meta name="robots" content="all" />
        //<meta name="revisit-after" content="5" />

        //<meta http-equiv="imagetoolbar" content="no" />
        //<meta http-equiv="cache-control" content="cache" />
        //<meta http-equiv="pragma" content="cache" />
        //<meta http-equiv="refresh" content="10;URL=http://www.ssoftcolombia.com" />
        //<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
        internal static void html(string p)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public static void DividirTablaCarrusel(DataTable dtPLanesOfer, int cantidaditem, Repeater rptOfertasUl)
        {
            try
            {
                System.Collections.Generic.List<string> items = new System.Collections.Generic.List<string>();
                DataTable dtOfertas = dtPLanesOfer;
                if (rptOfertasUl != null)
                {
                    if (dtOfertas.Rows.Count > cantidaditem)
                    {
                        /*hacemos la division de los planes en 4*/
                        int cantidad = dtOfertas.Rows.Count / cantidaditem;
                        int cantidadTotal = cantidad * cantidaditem;
                        if (dtOfertas.Rows.Count > cantidadTotal)
                            cantidad++;
                        /**/
                        for (int c = 1; c <= cantidad; c++)
                        {
                            items.Add(c.ToString());
                        }

                        /*llenamos el repetidor de "Ul"*/
                        rptOfertasUl.DataSource = items;
                        rptOfertasUl.DataBind();

                        /*llenamos un list con la cantidad de items*/
                        int iTotalPaginas = dtOfertas.Rows.Count;
                        for (int c = 0; c < items.Count; c++)
                        {
                            DataTable dtOfertas4 = dtOfertas.Clone();
                            dtOfertas4.TableName = "dtOfertas" + c.ToString();

                            int iRowPagina = c * cantidaditem;
                            int iMaxRows = cantidaditem * (c + 1);
                            if (iMaxRows > iTotalPaginas)
                                iMaxRows = iTotalPaginas;

                            dtOfertas4 = clsDataNet.dtPaginacionDetalle(iRowPagina, iMaxRows, dtOfertas);
                            /*recorremos el repetidor de "UI" y encontramos cada repetidor de "Div"*/

                            Repeater rptOfertasDiv = rptOfertasUl.Items[c].FindControl("rptOfertasDiv") as Repeater;
                            if (rptOfertasDiv != null)
                            {
                                rptOfertasDiv.DataSource = dtOfertas4;
                                rptOfertasDiv.DataBind();
                                dtOfertas4.Clear();
                            }
                        }
                    }
                    else
                    {
                        items.Add("1");
                        rptOfertasUl.DataSource = items;
                        rptOfertasUl.DataBind();
                        for (int c = 0; c < rptOfertasUl.Items.Count; c++)
                        {
                            Repeater rptOfertasDiv = rptOfertasUl.Items[c].FindControl("rptOfertasDiv") as Repeater;
                            if (rptOfertasDiv != null)
                            {
                                rptOfertasDiv.DataSource = dtOfertas;
                                rptOfertasDiv.DataBind();
                            }
                        }

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
                cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                cParametros.Info = "No se puede Dividir la tabla";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        /// <summary>
        /// divide la tabla para controlar la cantidad de items a mostrar 
        /// </summary>
        /// <param name="Proyecto">tutiquete</param>
        /// <param name="Aplicacion">17084</param>
        /// <param name="Idioma">es</param>
        /// <returns></returns>
        /// <remarks>
        /// Autor:          Jeison Vargas
        /// Company:        Ssoft Colombia
        /// Fecha:          2010-16-11
        /// </remarks>
        public static void DividirTablaLinks(DataTable dtPLanesOfer, int cantidaditem, Repeater rptOfertasUl)
        {
            try
            {
                System.Collections.Generic.List<string> items = new System.Collections.Generic.List<string>();
                DataTable dtOfertas = dtPLanesOfer;
                if (rptOfertasUl != null)
                {
                    if (dtOfertas.Rows.Count > cantidaditem)
                    {
                        /*hacemos la division de los planes en 4*/
                        int cantidad = dtOfertas.Rows.Count / cantidaditem;
                        int cantidadTotal = cantidad * cantidaditem;
                        if (dtOfertas.Rows.Count > cantidadTotal)
                            cantidad++;
                        /**/
                        for (int c = 1; c <= cantidad; c++)
                        {
                            items.Add(c.ToString());
                        }

                        /*llenamos el repetidor de "Ul"*/
                        rptOfertasUl.DataSource = items;
                        rptOfertasUl.DataBind();

                        /*llenamos un list con la cantidad de items*/
                        int iTotalPaginas = dtOfertas.Rows.Count;
                        for (int c = 0; c < items.Count; c++)
                        {
                            DataTable dtOfertas4 = dtOfertas.Clone();
                            dtOfertas4.TableName = "dtOfertas" + c.ToString();

                            int iRowPagina = c * cantidaditem;
                            int iMaxRows = cantidaditem * (c + 1);
                            if (iMaxRows > iTotalPaginas)
                                iMaxRows = iTotalPaginas;

                            dtOfertas4 = clsDataNet.dtPaginacionDetalle(iRowPagina, iMaxRows, dtOfertas);
                            /*recorremos el repetidor de "UI" y encontramos cada repetidor de "Div"*/

                            Repeater rptOfertasDiv = rptOfertasUl.Items[c].FindControl("rptlinks") as Repeater;
                            if (rptOfertasDiv != null)
                            {
                                rptOfertasDiv.DataSource = dtOfertas4;
                                rptOfertasDiv.DataBind();
                                dtOfertas4.Clear();
                            }
                        }
                    }
                    else
                    {
                        items.Add("1");
                        rptOfertasUl.DataSource = items;
                        rptOfertasUl.DataBind();
                        for (int c = 0; c < rptOfertasUl.Items.Count; c++)
                        {
                            Repeater rptOfertasDiv = rptOfertasUl.Items[c].FindControl("rptlinks") as Repeater;
                            if (rptOfertasDiv != null)
                            {
                                rptOfertasDiv.DataSource = dtOfertas;
                                rptOfertasDiv.DataBind();
                            }
                        }

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
                cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                cParametros.Info = "No se puede Dividir la tabla";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// Metodo para seleccionar y quitar la seleccin del radiobutton, este metodo verifica el repeater o datalist que contiene el radiobutton
        /// </summary>
        /// <param name="sender">Objeto radiobutton</param>
        /// <param name="sControl">Nombre del radiobutton</param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-12-30
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static void setSeleccionRadio(object sender, string sControl)
        {
            try
            {
                Repeater rptRepeater = ((Repeater)((RepeaterItem)((RadioButton)sender).Parent).Parent);
                if (rptRepeater != null)
                    clsControls.setSeleccionRadio(rptRepeater, sender, sControl);
            }
            catch
            {
            }
            try
            {
                DataList dtlRepeater = ((DataList)((DataListItem)((RadioButton)sender).Parent).Parent);
                if (dtlRepeater != null)
                    clsControls.setSeleccionRadio(dtlRepeater, sender, sControl);
            }
            catch
            {
            }
        }
        /// <summary>
        /// Metodo para colocar un botón por defecto
        /// </summary>
        /// <param name="btnButton">Instancia del Boton de userControl o Pages</param>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.ssoftcolombia.com
        /// Email: fposas@ssoftcolombia.com
        /// Fecha: 2012-02-19
        /// ------------------
        /// Control de Cambios
        /// ------------------
        /// Descripcion:    
        /// Fecha:          
        /// Responsable:    
        /// </remarks>
        public static void setDefaultButton(Button btnButton)
        {
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                PageActual.Form.DefaultButton = btnButton.UniqueID;
            }
            catch { }
        }
        public static void setPopupMensaje(UserControl PageSource, string sMensaje)
        {
            try
            {
                Label lblMensaje = PageSource.FindControl("lblMensaje") as Label;
                AjaxControlToolkit.ModalPopupExtender MPEMensaje = PageSource.FindControl("MPEMensaje") as AjaxControlToolkit.ModalPopupExtender;
                lblMensaje.Text = sMensaje;
                MPEMensaje.Show();
            }
            catch { }
        }
        public static void setRedirectPopupMensaje(UserControl PageSource)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (PageSource.Request.QueryString["Fijo"] != null)
                {
                    if (PageSource.Request.QueryString["Fijo"].ToString().Equals("1"))
                    {
                        AjaxControlToolkit.ModalPopupExtender MPEMensaje = PageSource.FindControl("MPEMensaje") as AjaxControlToolkit.ModalPopupExtender;
                        MPEMensaje.Hide();
                    }
                    else
                    {
                        if (cCache != null)
                        {
                            cCache.DatosAdicionales = null;
                            csCache.ActualizarCache(cCache);
                        }
                        Buscador();
                    }
                }
                else
                {
                    if (cCache != null)
                    {
                        cCache.DatosAdicionales = null;
                        csCache.ActualizarCache(cCache);
                    }
                    Buscador();
                }
            }
            catch { }
        }

        public static void setExterno(string sCodigo)
        {
            csGeneralsPag.ValidarSesionPag();
            clsParametros cError = new clsParametros();
            cError.Id = 1;
            try
            {
                string sUsuario = clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF");

                string sEmpresa = clsValidaciones.GetKeyOrAdd("Empresa", "EM");


                string sViajero = clsValidaciones.GetKeyOrAdd("Viajero", "VJ");
                string sContabilidad = clsValidaciones.GetKeyOrAdd("Contabilidad", "UC");
                string sProveedor = clsValidaciones.GetKeyOrAdd("Proveedor", "PS");
                string sTipoContacto = clsValidaciones.GetKeyOrAdd("TipoContacto", "Tipo_Contacto");
                string sTipoCliente = clsValidaciones.GetKeyOrAdd("TipoCliente", "TPO_CLIENTE");
                string sCliente = clsValidaciones.GetKeyOrAdd("Cliente");
                string strViajero = "0";
                string strEmpresa = "0";
                if (HttpContext.Current.Request.QueryString["idE"] != null)
                {
                    strEmpresa = HttpContext.Current.Request.QueryString["idE"].ToString();
                }
                else
                {
                    strEmpresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "3");
                }
                string strComunidad = "0";

                string strPropietario = "0";
                string strTipoCliente = "0";
                string strImagen = string.Empty;
                string[] sValues = new string[4];



                strTipoCliente = new CsConsultasVuelos().ConsultaCodigo(sUsuario, "TBLNIVELUSUARIOS", "INTCODE", "REFERETIPOUSUARIO");
                if (strTipoCliente == null || strTipoCliente == "")
                {
                    strTipoCliente = "0";
                }

                clsLogin cLogin = new clsLogin();
                DataSet dsLogin = new DataSet();


                dsLogin = cLogin.LoginExterno(sCodigo, true);
                if (!(dsLogin == null))
                {
                    if (dsLogin.Tables[0].Rows.Count > 0)
                    {

                        string sTipo = dsLogin.Tables[0].Rows[0]["INTNIVEL"].ToString();

                        if (strEmpresa.Equals("0"))
                            strEmpresa = dsLogin.Tables[0].Rows[0]["intEmpresa"].ToString();


                        strViajero = dsLogin.Tables[0].Rows[0]["intusuario"].ToString();
                        //strImagen = csGeneralsPag.LogoAgencia(strEmpresa);
                        if (strViajero.Equals("0"))
                        {
                            if (!new csCache().csCodigoExternoidC().Equals(0))
                            {
                                strViajero = dsLogin.Tables[0].Rows[0]["intusuario"].ToString();
                            }

                        }
                        sValues[0] = strViajero;
                        sValues[1] = strEmpresa;
                        sValues[2] = strImagen;
                        sValues[3] = strTipoCliente;



                        string sesion = new clsCacheControl().CrearSession() + HttpContext.Current.Session.SessionID.ToString();
                        clsCache cCache = new clsCache();
                        cCache = setActualizaSesion(dsLogin, sesion, sValues);
                        HttpContext.Current.Session["idSession"] = sesion;
                        string csCodigo = new Utils.csCache().csCodigoExternoidC();
                        if (!csCodigo.Equals("0"))
                        {
                            cCache.Verifica = true;
                            csCache.ActualizarCache(cCache);
                        }
                        else
                        {
                            if (clsValidaciones.GetKeyOrAdd("RegistroFormReserva", "False").ToUpper().Equals("TRUE"))
                            {
                                cCache.Verifica = true;
                                csCache.ActualizarCache(cCache);
                            }
                            else
                            {
                                cCache.Verifica = false;
                                csCache.ActualizarCache(cCache);
                            }
                        }

                    }
                    else
                    {
                        cError.Id = 0;
                        cError.Message = "El usuario proporcionado no existe o la contraseña es incorrecta";
                        cError.Tipo = clsTipoError.Library;
                        cError.Severity = clsSeveridad.Moderada;
                        ExceptionHandled.Publicar(cError);
                    }
                }
                else
                {
                    cError.Id = 0;
                    cError.Message = "Se ha producido un error al intentar hacer el login, por favor comuniquese con el administrador";
                    cError.Tipo = clsTipoError.Library;
                    cError.Severity = clsSeveridad.Moderada;
                    ExceptionHandled.Publicar(cError);
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
                cParametros.Complemento = "Login Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public static clsCache setActualizaSesion(DataSet dsLogin, string sesion, string[] sValues)
        {
            clsCache cCache = new clsCache();

            clsSesiones.CLEAR_SESSION_ALL();
            try
            {
                string sIdioma = clsSesiones.getIdioma();
                string sCulture = clsValidaciones.GetKeyOrAdd("culture", "es");
                string sFormatoFecha = clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");
                string sFormatoFechaBD = clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");
                string sUNegocio = clsValidaciones.GetKeyOrAdd("idUNegocio", "0");
                string sUrlAnterior = csGeneralsPag.UrlCompleta();
                string sTipoPagina = clsValidaciones.GetKeyOrAdd("Cliente", "UF");
                string sAplicacion = clsSesiones.getAplicacion().ToString();
                string sEmpresa = clsValidaciones.GetKeyOrAdd("Empresa", "EM");
                string sBorrarCache = clsValidaciones.GetKeyOrAdd("BorradoCacheMinutos", "0");
                string sUrlActual = sUrlAnterior;
                try
                {
                    sUrlActual = clsSesiones.getPantalleRespuestaLogin();
                    if (sUrlActual == null || sUrlActual.Length <= 0)
                    {
                        sUrlActual = sUrlAnterior;
                    }
                }
                catch { }
                HttpContext.Current.Session.Add("SessionIDLocal", sesion);

                cCache.SessionID = sesion;
                cCache.UrlActual = sUrlActual;
                cCache.UrlAnterior = sUrlAnterior;
                cCache.UNegocio = sUNegocio;
                cCache.User = dsLogin.Tables[0].Rows[0]["strEmail"].ToString();
                cCache.Nivel = dsLogin.Tables[0].Rows[0]["intnivel"].ToString();
                cCache.Pass = dsLogin.Tables[0].Rows[0]["strPassword"].ToString();
                cCache.Identificacion = dsLogin.Tables[0].Rows[0]["strIdentificacion"].ToString();
                cCache.Pagina = "default.aspx";
                cCache.Idioma = sIdioma;
                cCache.Cultura = sCulture;
                cCache.Nombres = dsLogin.Tables[0].Rows[0]["strNombre"].ToString();
                cCache.Apellidos = dsLogin.Tables[0].Rows[0]["strApellido"].ToString();
                cCache.Direccion = dsLogin.Tables[0].Rows[0]["strUbicacion"].ToString();
                cCache.Telefono = dsLogin.Tables[0].Rows[0]["strTelefono"].ToString();
                cCache.Celular = dsLogin.Tables[0].Rows[0]["strCelular"].ToString();
                cCache.Ciudad = dsLogin.Tables[0].Rows[0]["intCiudad"].ToString();

                if (!sTipoPagina.Equals("UF"))
                {
                    cCache.Aplicacion = dsLogin.Tables[0].Rows[0]["intAplicacion"].ToString();
                }
                else
                {
                    cCache.Aplicacion = sAplicacion;
                }
                cCache.RefereContacto = dsLogin.Tables[0].Rows[0]["intnivel"].ToString();
                if (cCache.RefereContacto.Equals(sEmpresa))
                {
                    cCache.Aplicacion = dsLogin.Tables[0].Rows[0]["intAplicacion"].ToString();
                }
                cCache.Empresa = sValues[1];
                cCache.Email = dsLogin.Tables[0].Rows[0]["strEmail"].ToString();
                cCache.TipoContacto = dsLogin.Tables[0].Rows[0]["intnivel"].ToString();
                cCache.Proyecto = "0";
                cCache.FormatoFecha = sFormatoFecha;
                cCache.FormatoFechaBD = sFormatoFechaBD;
                cCache.Viajero = sValues[0];
                cCache.Contacto = sValues[0];
                if (!sValues[0].ToString().Equals("0"))
                {
                    cCache.Verifica = true;
                }
                try
                {
                    cCache.Genero = dsLogin.Tables[0].Rows[0]["intgenero"].ToString();
                    cCache.FechaNac = dsLogin.Tables[0].Rows[0]["dtmFechaNac"].ToString();
                }
                catch { }

                try
                {
                    cCache.TipoIdentificacion = dsLogin.Tables[0].Rows[0]["inttipoident"].ToString();
                }
                catch { }

                try
                {
                    cCache.TiempoCache = int.Parse(sBorrarCache);
                    if (cCache.TiempoCache.Equals(0))
                    {
                        cCache.TiempoInicial = DateTime.Now.ToString();
                        cCache.TiempoFinal = DateTime.Now.AddDays(2).ToString();
                    }
                    else
                    {
                        cCache.TiempoInicial = DateTime.Now.ToString();
                        cCache.TiempoFinal = DateTime.Now.AddMinutes(cCache.TiempoCache).ToString();
                    }
                }
                catch { }

                csCache.ActualizarCache(cCache);
                clsSesiones.setAplicacion(int.Parse(cCache.Aplicacion));


                clsSesiones.setProyecto("0");
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
                cParametros.Complemento = "Login ";
                ExceptionHandled.Publicar(cParametros);
            }
            return cCache;
        }


    }



}
