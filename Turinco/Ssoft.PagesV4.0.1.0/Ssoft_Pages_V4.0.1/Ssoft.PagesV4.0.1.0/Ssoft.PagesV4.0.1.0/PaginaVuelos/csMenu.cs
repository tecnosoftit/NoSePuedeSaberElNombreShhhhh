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
using System.IO;
using Ssoft.ManejadorExcepciones;
using Ssoft.Rules.Reservas;
using Ssoft.Rules.Administrador;
using Ssoft.ValueObjects;

namespace Ssoft.Pages
{
    public class csMenu
    {
        private string FORMATO_NUMEROS_VIEW = clsValidaciones.GetKeyOrAdd("sFormatoView", "#,##0");
        protected string strConexion = default(string);
        /// <summary>
        /// Estable u obtiene es string de conexion
        /// </summary>
        public string Conexion
        {
            set { strConexion = value; }
            get { return strConexion; }
        }
        public csMenu()
        {
            Conexion = clsSesiones.getConexion();
        }
        public void setCommand(UserControl PageSource, object source, CommandEventArgs e)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    switch (e.CommandName)
                    {
                        case "Logout":
                            setCerarSesion(PageSource);
                            break;

                        case "LogoutIndex":
                            setCerarSesion(PageSource, true);
                            break;

                        case "Login":
                            setMiCuenta(PageSource);
                            break;

                        case "Idioma":
                            setIdioma(e.CommandArgument.ToString());
                            break;

                        case "Carro":
                            setCarro(PageSource);
                            break;

                        case "Buzon":
                            setContactenos(PageSource);
                            break;

                        case "Link":
                            clsValidaciones.RedirectPagina(e.CommandArgument.ToString());
                            break;
                    }
                }
            }
            catch
            {
            }
        }

        public string setLeerCarrito(clsCache cCache)
        {
            string sCarrito = "(0)";
            try
            {
                string sSesion = new clsCacheControl().RecuperarSesionId(); ;
                csGeneralsPag.ValidarSesionPag();
                if (sSesion != null)
                {
                    csCarrito csCarCompUnion = new csCarrito("Reserva" + sSesion, "CarritoCompras");
                    DataTable TablaPlanes = csCarCompUnion.RecuperarTabla();
                    sCarrito = "(" + TablaPlanes.Rows.Count.ToString() + ")";
                }
            }
            catch
            {
            }
            return sCarrito;
        }

        public void setBanner(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            csGeneralsPag.Idioma(PageSource);
            try
            {
                string sImagenGen = clsValidaciones.ObtenerUrlImages();
                string sImagen = sImagenGen + "logo.png";

                clsCache cCache = new csCache().cCache();
                LinkButton lbCerrarSesion = (LinkButton)PageSource.FindControl("lbCerrarSesion");
                Label lblUsuario = (Label)PageSource.FindControl("lblUsuario");
                Label lblFecha = (Label)PageSource.FindControl("lblFecha");
                Image imgBanner = (Image)PageSource.FindControl("imgBanner");
                Panel pnUsuario = (Panel)PageSource.FindControl("pnUsuario");
                Panel pnLogin = (Panel)PageSource.FindControl("pnLogin");
                Label lblServCarro = (Label)PageSource.FindControl("lblServCarro");
                Label lblServCar = (Label)PageSource.FindControl("lblServCar");
                Label lblTelefono = (Label)PageSource.FindControl("lblTelefono");

                Literal ltlUsuario = (Literal)PageSource.FindControl("ltlUsuario");
                Literal ltlFecha = (Literal)PageSource.FindControl("ltlFecha");
                Literal ltlPuntos = (Literal)PageSource.FindControl("ltlPuntos");
                Literal ltlPuntosVence = (Literal)PageSource.FindControl("ltlPuntosVence");
                Literal ltlExpiran = (Literal)PageSource.FindControl("ltlExpiran");
                //HtmlGenericControl Expire = (HtmlGenericControl)PageSource.FindControl("Expire");

                if (lblTelefono != null)
                {
                    lblTelefono.Text = clsValidaciones.GetKeyOrAdd("Telefono_Agencia", "3791200");
                }

                if (cCache != null)
                {
                    bool bVerifica = true;
                    try
                    {
                        bVerifica = cCache.Verifica;
                    }
                    catch { }
                    if (bVerifica)
                    {

                        if (pnLogin != null && pnUsuario != null)
                        {
                            if (clsValidaciones.GetKeyOrAdd("RegistroFormReserva", "True").ToUpper().Equals("TRUE"))
                            {
                                if (clsValidaciones.GetKeyOrAdd("idContacto", "96") == cCache.Contacto)
                                {
                                    pnUsuario.Visible = true;
                                    pnLogin.Visible = false;
                                }
                                else
                                {
                                    pnUsuario.Visible = false;
                                    pnLogin.Visible = true;
                                }
                            }
                            else
                            {
                                pnUsuario.Visible = false;
                                pnLogin.Visible = true;
                            }
                        }
                        if (lblServCarro != null)
                        {
                            csCarrito csCarCompUnion = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
                            DataTable TablaPlanes = csCarCompUnion.RecuperarTabla();
                            lblServCarro.Text = "(" + TablaPlanes.Rows.Count.ToString() + ")";
                            if (TablaPlanes.Rows.Count == 0)
                            {
                                lblServCarro.CssClass = "carroCompras";

                                if (lblServCar != null)
                                {
                                    lblServCar.CssClass = "carroCompras";
                                }
                            }
                            else if (TablaPlanes.Rows.Count > 0)
                            {
                                lblServCarro.CssClass = "";
                                if (lblServCar != null)
                                {
                                    lblServCar.CssClass = "";
                                }
                            }

                        }
                        if (lbCerrarSesion != null)
                            lbCerrarSesion.Visible = true;
                        if (lblUsuario != null)
                            lblUsuario.Text = cCache.Nombres;
                        if (lblFecha != null)
                            lblFecha.Text = DateTime.Now.ToLongDateString();
                    }
                    else
                    {
                        if (pnLogin != null && pnUsuario != null)
                        {
                            pnLogin.Visible = false;
                            pnUsuario.Visible = true;
                            lblUsuario.Text = "";
                        }/*se valida que no sea nulo para evitar excepciones.*/

                    }


                }
                else
                {
                    if (pnLogin != null && pnUsuario != null)
                    {
                        pnLogin.Visible = false;
                        pnUsuario.Visible = true;
                        lblUsuario.Text = "";
                    }
                    if (imgBanner != null)
                    {
                        imgBanner.ImageUrl = sImagen;
                    }
                }
                //csLogin clogin = new csLogin();
                string[] sValor = csValue();
                if (!sValor[0].Length.Equals(0))
                {
                    setIdioma(sValor[0]);
                }
                else
                {
                    if (!sValor[1].Length.Equals(0))
                    {
                        setParametrosLogin(PageSource);
                        switch (sValor[1])
                        {
                            case "Logout":
                                setCerarSesion(PageSource);
                                break;

                            case "Login":
                                setMiCuenta(PageSource);
                                break;

                            case "Idioma":
                                setIdioma(sValor[1]);
                                break;

                            case "Carro":
                                setCarro(PageSource);
                                break;

                            case "Buzon":
                                setContactenos(PageSource);
                                break;

                            case "Link":
                                clsValidaciones.RedirectPagina(sValor[1]);
                                break;

                            case "Entrar":
                                //clogin.setEntrar(PageSource, Enum_Login.LoginGen);
                                break;

                            case "Olvido":
                                //clogin.setOlvido(PageSource);
                                break;

                            case "Crear":
                                //clogin.setCrear(PageSource, Enum_Login.LoginGen);
                                break;


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
                cParametros.Complemento = "Banner";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        public void setCerarSesion(UserControl PageSource)
        {
            setCerarSesion(PageSource, true);
        }

        /// <summary>
        /// setCerarSesion - metodo que cierra sesion de usuario
        /// </summary>
        /// <param name="PageSource">Usercontrol</param>
        /// <param name="bRedirectIndex">Redireccion al index</param>
        /// Autor:  
        /// Fecha:  
        /// -- Control de cambios --
        /// Descripcion: Se cambia el direccionamiento hacia el index, ya que enviaba parametros
        /// Fecha: 2011-09-15       
        /// Responsable: Camilo Diaz 
        public void setCerarSesion(UserControl PageSource, bool bRedirectIndex)
        {
            clsParametros cParametros = new clsParametros();
            clsCache cCache = new csCache().cCache();
            try
            {
                string sCliente = clsValidaciones.GetKeyOrAdd("Cliente", "AG");
                string sUrl = csGeneralsPag.UrlCompleta();
                if (sCliente.Equals("AG"))
                {
                    sUrl = csGeneralsPag.LoginSesion();
                    bRedirectIndex = false;
                    new clsCacheControl().EliminarCache();
                    if (bRedirectIndex)
                        clsValidaciones.RedirectPaginaIni(sUrl, true);
                    else
                        clsValidaciones.RedirectPagina(sUrl, true);
                }
                else
                {
                    if (cCache != null)
                    {
                        cCache.Contacto = "0";
                        cCache.Viajero = "0";
                        cCache.Verifica = false;
                        csCache.ActualizarCache(cCache);
                        if (bRedirectIndex)
                            clsValidaciones.RedirectPaginaIni(sUrl, true);
                        else
                            clsValidaciones.RedirectPagina(sUrl, true);
                    }
                    else
                    {
                        csGeneralsPag.CrearCacheVacia(sUrl);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="sIdioma"></param>
        public void setIdioma(string sIdioma)
        {
            string sPaginaIdioma = clsValidaciones.GetKeyOrAdd("sPaginaIdioma", "False");
            //if (sPaginaIdioma.ToUpper().Equals("TRUE"))
            //{
            //    csGeneralsPag.Idioma();
            //    new clsIdioma().CambiarIdioma(sIdioma);
            //    csGeneralsPag.RedirectPaginaActual();
            //}
        }

        public void setMiCuenta(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {
                clsSesiones.setPantalleRespuestaLogin(null);
                clsValidaciones.RedirectPagina("Login.aspx");
            }
            else
            {
                csGeneralsPag.FinSesion();
            }
        }

        public void setContactenos(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {
                clsValidaciones.RedirectPagina("Contactenos.aspx");
            }
            else
            {
                csGeneralsPag.FinSesion();
            }
        }

        public void setCarro(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {
                clsValidaciones.RedirectPagina("CarroCompras.aspx");
            }
            else
            {
                csGeneralsPag.FinSesion();
            }
        }

        public void setNoItemsCarro(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsCache cCache = new csCache().cCache();
            string sSesion = cCache.SessionID;
            if (sSesion == null)
            {
                sSesion = clsSesiones.getSesionIDLocal();
            }
            if (sSesion != null)
            {
                Label lblServCarro = (Label)PageSource.FindControl("lblServCarro");
                if (lblServCarro != null)
                {
                    csCarrito csCarCompUnion = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
                    DataTable TablaPlanes = csCarCompUnion.RecuperarTabla();
                    lblServCarro.Text = "(" + TablaPlanes.Rows.Count.ToString() + ")";
                }
            }
        }

        public bool ValidaParametros()
        {
            bool bValida = true;
            try
            {
                string[] sValor = csValue();
                if (!sValor[0].Length.Equals(0))
                {
                    bValida = false;
                }
                else
                {
                    if (!sValor[1].Length.Equals(0))
                    {
                        bValida = false;
                    }
                }
            }
            catch { }
            return bValida;
        }

        private string[] csValue()
        {
            string[] sValue = new string[4];
            try
            {
                if (HttpContext.Current.Request.QueryString["Idioma"] != null)
                {
                    sValue[0] = HttpContext.Current.Request.QueryString["Idioma"].ToString();
                }
                else
                {
                    sValue[0] = string.Empty;
                }
                if (HttpContext.Current.Request.QueryString["ParamHtm"] != null)
                {
                    sValue[1] = HttpContext.Current.Request.QueryString["ParamHtm"].ToString();
                }
                else
                {
                    sValue[1] = string.Empty;
                }
                if (HttpContext.Current.Request.QueryString["User"] != null)
                {
                    sValue[2] = HttpContext.Current.Request.QueryString["User"].ToString();
                }
                else
                {
                    sValue[2] = string.Empty;
                }
                if (HttpContext.Current.Request.QueryString["Pass"] != null)
                {
                    sValue[3] = HttpContext.Current.Request.QueryString["Pass"].ToString();
                }
                else
                {
                    sValue[3] = string.Empty;
                }
            }
            catch
            {
            }
            return sValue;
        }

        private void setParametrosLogin(UserControl PageSource)
        {
            try
            {
                csGeneralsPag.Idioma(PageSource);

                TextBox txtUsuario = (TextBox)PageSource.FindControl("txtUsuario");
                TextBox txtPassword = (TextBox)PageSource.FindControl("txtPassword");
                if (txtUsuario != null && txtPassword != null)
                {
                    string[] sValor = csValue();
                    if (sValor[2] != "0" && !sValor[2].Length.Equals(0))
                    {
                        txtUsuario.Text = sValor[2];
                    }
                    if (sValor[3] != "0" && !sValor[3].Length.Equals(0))
                    {
                        txtPassword.Text = sValor[3];
                    }
                }
            }
            catch { }
        }

        public void setValidaTerminos()
        {
            try
            {
                string sPagina = "IndexTerminos.aspx";
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    if (!cCache.Terminos)
                    {
                        clsValidaciones.RedirectPagina(sPagina, true);
                    }
                }
            }
            catch { }
        }
    }
}
