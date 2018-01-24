using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Rules.Generales;
using Ssoft.Utils;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Ssoft.ManejadorExcepciones;
using Ssoft.ValueObjects;

namespace Ssoft.Pages
{
    public class csFinSesion
    {
        protected string strConexion = default(string);
        /// <summary>
        /// Estable u obtiene es string de conexion
        /// </summary>
        public string Conexion
        {
            set { strConexion = value; }
            get { return strConexion; }
        }
        public csFinSesion()
        {
            strConexion = clsSesiones.getConexion();
        }
        public void setCargar(UserControl PageSource)
        {
            try
            {
                clsParametros cParametros = new clsParametros();
                string[] sValue = csValue(PageSource);
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    cParametros.Id = 0;
                    cParametros.Message = "Cache activa";
                    cParametros.Source = "Contacto: " + cCache.Contacto;
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.StackTrace = "Usuario: " + cCache.Nombres;
                    cParametros.Metodo = "FinSesion ";
                    cParametros.Complemento = "Sesion: " + cCache.SessionID;
                    ExceptionHandled.Publicar(cParametros);
                    csGeneralsPag.SesionIdPage(cCache);
                   
                }
                else
                {
                    try
                    {
                        if (HttpContext.Current.Session["SessionIDLocal"] != null)
                        {
                            cCache = (clsCache)PageSource.Cache[HttpContext.Current.Session["SessionIDLocal"].ToString()];
                            if (cCache != null)
                            {
                                csGeneralsPag.SesionIdPage(cCache);
                            }
                        }
                    }
                    catch { }

                    cParametros.Id = 0;
                    cParametros.Message = "Perdida de Cache";
                    cParametros.Source = "Parametro: " + sValue[0];
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.Complemento = "FinSesion ";
                    ExceptionHandled.Publicar(cParametros);
                }
                CargarError(PageSource, sValue[0].ToString());
            }
            catch { CargarError(PageSource, "0"); }
        }
        public void setCargarRetorno(UserControl PageSource, bool bEnvioRetorno)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    clsCacheControl cCacheControl = new clsCacheControl();
                    cCacheControl.EliminarCache(cCache.SessionID);
                    cCache = null;
                }
            }
            catch { }
            if (bEnvioRetorno)
                csGeneralsPag.PaginaRetorno();
        }
        public void setEnviarRetorno(UserControl PageSource)
        {
            csGeneralsPag.PaginaRetorno();
        }
        public void setEnviar(UserControl PageSource)
        {
            HiddenField hdfUrl = (HiddenField)PageSource.FindControl("hdfUrl");
            try
            {
                clsValidaciones.RedirectPagina(hdfUrl.Value);
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
                cParametros.Complemento = "FinSesion ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                new csCache().setError(PageSource, cParametros);
            }
        }
        private string[] csValue(UserControl PageSource)
        {
            string[] sValue = new string[1];
            try
            {
                if (PageSource.Request.QueryString["TipoError"] != null)
                {
                    sValue[0] = PageSource.Request.QueryString["TipoError"].ToString();
                }
                else
                {
                    sValue[0] = "0";
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
            return sValue;
        }
        private void CargarError(UserControl PageSource, string TipoError)
        {
            HtmlGenericControl dPanel = (HtmlGenericControl)PageSource.FindControl("dPanel");
            HiddenField hdfUrl = (HiddenField)PageSource.FindControl("hdfUrl");

            clsParametros cParametros = new clsParametros();
            cParametros.Id = 0;
            cParametros.Tipo = clsTipoError.Aplication;

            string sUrl = "../Default.aspx";
            try { sUrl = clsValidaciones.GetKeyOrAdd("UrlInicial"); }
            catch { }

            string sUrlIndex = "Index.aspx";
            try { sUrlIndex = clsValidaciones.GetKeyOrAdd("urlIndex"); }
            catch { }

            clsCache cCache = new csCache().cCache();

            if (clsSesiones.getParametrosError() != null)
            {
                cParametros = clsSesiones.getParametrosError();
                clsSesiones.setParametrosError(null);
            }
            else
            {
                switch (TipoError)
                {
                    case "0":
                        cParametros.ViewMessage.Add("Su sesion ha terminado");
                        cParametros.Sugerencia.Add("Por favor presione regresar para autenticarse de nuevo");
                        cParametros.Severity = clsSeveridad.Media;
                        break;

                    case "1":
                        cParametros.ViewMessage.Add("Su usuario no tiene permisos para este módulo");
                        cParametros.Sugerencia.Add("Por favor presione regresar");
                        if (cCache != null)
                        {
                            sUrl = sUrlIndex;
                        }
                        cParametros.Severity = clsSeveridad.Media;
                        break;

                    case "E":
                        cParametros.ViewMessage.Add("Su sesion ha terminado");
                        cParametros.Sugerencia.Add("Por favor presione regresar para ingresar de nuevo");
                        cParametros.Severity = clsSeveridad.Alta;
                        break;
                }
            }
            hdfUrl.Value = sUrl;

            clsErrorMensaje cError = new clsErrorMensaje();
            cError.getError(cParametros, dPanel);
        }
        public void setErrorBusqueda(UserControl PageSource)
        {
            clsParametros cParametros = clsSesiones.getParametrosError();
            try
            {
                string sTipoPagina = clsValidaciones.GetKeyOrAdd("Cliente", "UF");
                if (sTipoPagina.Equals("AG"))
                {
                    clsCache cCache = new csCache().cCache();
                    if (cCache == null)
                    {
                        clsSesiones.setParametrosError(null);
                        csGeneralsPag.FinSesion();
                    }
                }
                else
                {
                    csGeneralsPag.ValidarSesionPag();
                }
                new csCache().setError(PageSource, cParametros);
                clsSesiones.setParametrosError(null);
            }
            catch { }
        }
        public void setCancelaSesion()
        {
            try
            {
                Negocios_WebServiceSession._CerrarSesion();
            }
            catch { }
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    clsCacheControl cCacheControl = new clsCacheControl();
                    cCacheControl.EliminarCache(cCache.SessionID.ToString());
                }
            }
            catch { }
            try
            {
                HttpContext.Current.Session.Abandon();
            }
            catch { }
            try
            {
                csGeneralsPag.EliminaCookie();
            }
            catch { }
        }
        public void setCerrarPagina(UserControl PageSource)
        {
            setCancelaSesion();
            HttpContext.Current.Response.Write("<script>window.close();</script>");
        }
        public void setCerrarPagina(Page PageSource)
        {
            try
            {
                Negocios_WebServiceSession._CerrarSesion();
            }
            catch { }
            try
            {
                string sIndex = clsValidaciones.GetKeyOrAdd("GeneraIndexTmp", "False");
                if (sIndex.ToUpper().Equals("TRUE"))
                {
                   
                }

            }
            catch { }



            try
            {
                clsCache cCache = new csCache().cCache();

                clsCacheControl cCacheControl = new clsCacheControl();
                cCacheControl.EliminarCache(cCache.SessionID.ToString());
            }
            catch { }
            try
            {
                HttpContext.Current.Session.Abandon();
            }
            catch { }
            try
            {
                csGeneralsPag.EliminaCookie();
            }
            catch { }
            HttpContext.Current.Response.Write("<script>window.close();</script>");
        }
        public void setFinRewatds(UserControl PageSource)
        {
            Literal ltrFinRewards = (Literal)PageSource.FindControl("ltrFinRewards");
            string[] sValue = csValue(PageSource);
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    csGeneralsPag.Idioma(PageSource);
                    clsAccount cAccount = new clsAccount();
                    VO_Account vo_Account = cAccount.RecuperarAccount();

                    clsCacheControl cCacheControl = new clsCacheControl();

                    //string sTipo = clsValidaciones.GetKeyOrAdd("FormaPagoVasa", "VASA");
                    string sConfirmacion = clsValidaciones.GetKeyOrAdd("VasaConfirmacion", "[1000]");
                    if (vo_Account.Confirmation.Equals(sConfirmacion))
                    {
                        ltrFinRewards.Text = "Agradecemos su preferencia por haber utilizado nuestro Programa de Beneficios VISA REWARDS, esperamos servirle de nuevo en un futuro cercano.";
                    }
                    else
                    {
                        ltrFinRewards.Text = "Agradecemos su preferencia por haber utilizado nuestro Programa de Beneficios VISA REWARDS, lo sentimos la transacción NO fué exitosa por favor comuníquese con un asesor.";
                    }
                    cCacheControl.EliminarCache(cCache.SessionID.ToString());
                }
                else
                {
                    if (!sValue[0].Equals("0"))
                    {
                        ltrFinRewards.Text = "Agradecemos su preferencia por haber utilizado nuestro Programa de Beneficios VISA REWARDS, esperamos servirle de nuevo en un futuro cercano.";
                    }
                    else
                    {
                        ltrFinRewards.Text = "Agradecemos su preferencia por haber utilizado nuestro Programa de Beneficios VISA REWARDS, lo sentimos la transacción NO fué exitosa por favor comuníquese con un asesor.";
                    }
                }
            }
            catch 
            {
                if (!sValue[0].Equals("0"))
                {
                    ltrFinRewards.Text = "Agradecemos su preferencia por haber utilizado nuestro Programa de Beneficios VISA REWARDS, esperamos servirle de nuevo en un futuro cercano.";
                }
                else
                {
                    ltrFinRewards.Text = "Agradecemos su preferencia por haber utilizado nuestro Programa de Beneficios VISA REWARDS, lo sentimos la transacción NO fué exitosa por favor comuníquese con un asesor.";
                }
            }
        }
    }
}
