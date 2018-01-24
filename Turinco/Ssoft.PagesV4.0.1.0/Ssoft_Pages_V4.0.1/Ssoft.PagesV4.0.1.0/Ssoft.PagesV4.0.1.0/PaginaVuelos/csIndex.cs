using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using Ssoft.Utils;
using Ssoft.Rules.Corporativo;
using Ssoft.ManejadorExcepciones;
using Ssoft.Rules.Administrador;

namespace Ssoft.Pages
{
    public class csIndex : TemplateControl
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
        public csIndex()
        {
            strConexion = clsSesiones.getConexion();
        }
        public void setCargar(UserControl PageSource)
        {
            try
            {
                csGeneralsPag.Idioma(PageSource);
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    HtmlGenericControl buscador = (HtmlGenericControl)PageSource.FindControl("buscador");
                    HtmlGenericControl empresa = (HtmlGenericControl)PageSource.FindControl("empresa");
                  

                    string sValue = csValue(PageSource);

                    switch (sValue)
                    {
                        case "0":
                            buscador.Visible = true;
                            empresa.Visible = false;
                         
                            break;

                        case "1":
                            buscador.Visible = false;
                            empresa.Visible = true;
                            //csEmpresas(PageSource, cCache.ContactoPadre.ToString());
                          
                            break;

                        case "2":
                            buscador.Visible = false;
                            empresa.Visible = false;
                           
                            break;

                        case "3":
                            clsValidaciones.RedirectPagina("TareasProyectos.aspx", true);
                            break;
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
                cParametros.Complemento = "Gastos ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                new csCache().setError(PageSource, cParametros);
            }
        }
      
        public void setNuevo(UserControl PageSource)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    string sEmpresa = clsValidaciones.GetKeyOrAdd("Empresa", "EM");

                    string sLink = "Contacto=0&Tipo=" + sEmpresa;
                    clsValidaciones.RedirectPagina("ModificarUsuario.aspx?" + sLink, true);
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
                cParametros.Complemento = "Gastos ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                new csCache().setError(PageSource, cParametros);
            }
        }
        public void setEditar(UserControl PageSource)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    string sEmpresa = clsValidaciones.GetKeyOrAdd("Empresa","EM");

                    DropDownList ddlEmpresa = (DropDownList)PageSource.FindControl("ddlEmpresa");
                    string sLink = "Contacto=" + ddlEmpresa.SelectedValue.ToString() + "&Tipo=" + sEmpresa;
                    if (!ddlEmpresa.SelectedValue.ToString().Equals("0"))
                    {
                        clsValidaciones.RedirectPagina("ModificarUsuario.aspx?" + sLink);
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
                cParametros.Complemento = "Gastos ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                new csCache().setError(PageSource, cParametros);
            }
        }
       
        private string csValue(UserControl PageSource)
        {
            string sValue = "0";
            try
            {
                if (PageSource.Request.QueryString["Value"] != null)
                    sValue = PageSource.Request.QueryString["Value"].ToString();
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
       
    }
}
