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
using Ssoft.Rules.Reservas;
using Ssoft.ManejadorExcepciones;
using Ssoft.ValueObjects;
using Ssoft.Rules.Generales;
using Ssoft.Rules.Pagina;
using Ssoft.Rules.Corporativo;
using WS_SsoftSabre.Utilidades;
using Ssoft.DataNet;
using Ssoft.Rules.Administrador;
using WS_SsoftSabre.Air;
using WS_SsoftSabre.OTA_AirRules;
using System.Linq;
using SSOFT.ENCRYPT;
using SsoftQuery.Vuelos;


namespace Ssoft.Pages
{
    public class csResultadoVuelos
    {
        private const string COLUMN_ID_REFERE = "intidRefere";
        private const string COLUMN_REFERE = "strRefere";
        private const string COLUMN_DETALLE = "strDetalle";

        private static string sFormatoFecha = clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");
        private static string sFormatoFechaBD = clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");
        private const string sFormatoNumeros = "#,##0.00";

        protected string strConexion = default(string);
        /// <summary>    
        /// Estable u obtiene es string de conexion
        /// </summary>
        public string Conexion
        {
            set { strConexion = value; }
            get { return strConexion; }
        }
        public csResultadoVuelos()
        {
            strConexion = clsSesiones.getConexion();
        }
        public void setActualizaSesion(DataSet dsLogin, clsCache cCache, string[] sValues)
        {
            try
            {
                string sTipoPagina = clsValidaciones.GetKeyOrAdd("Cliente", "UF");
                string sAplicacion = clsSesiones.getAplicacion().ToString();
                string sEmpresa = clsValidaciones.GetKeyOrAdd("Empresa", "EM");
                string sBorrarCache = clsValidaciones.GetKeyOrAdd("BorradoCacheMinutos", "0");

            
                cCache.User = dsLogin.Tables[0].Rows[0]["strEmail"].ToString();
           
                cCache.Pass = dsLogin.Tables[0].Rows[0]["strPassword"].ToString();
                
                cCache.Identificacion = dsLogin.Tables[0].Rows[0]["strIdentificacion"].ToString();
                cCache.Nombres = dsLogin.Tables[0].Rows[0]["strNombre"].ToString();
                cCache.Apellidos = dsLogin.Tables[0].Rows[0]["strApellido"].ToString();
                cCache.Direccion = dsLogin.Tables[0].Rows[0]["strUbicacion"].ToString();
                cCache.Telefono = dsLogin.Tables[0].Rows[0]["strTelefono"].ToString();
                cCache.Celular = dsLogin.Tables[0].Rows[0]["strCelular"].ToString();
               
              
                cCache.Ciudad = dsLogin.Tables[0].Rows[0]["intCiudad"].ToString();
               

          
                    cCache.Aplicacion = sAplicacion;


                    if (cCache.Empresa.Length.Equals(0))
                    {
                        if (cCache.Empresa != "" && cCache.Empresa != "0")
                        {
                            cCache.Empresa = sValues[1];
                        }
                    }
              
               
                
               
                cCache.Email = dsLogin.Tables[0].Rows[0]["strEmail"].ToString();
                try
                {
                    cCache.TipoIdentificacion = dsLogin.Tables[0].Rows[0]["INTTIPOIDENT"].ToString();
                }
                catch { }
              
               
              
             
                try
                {
                    cCache.Genero = dsLogin.Tables[0].Rows[0]["INTGENERO"].ToString();
                    cCache.FechaNac = dsLogin.Tables[0].Rows[0]["dtmFechaNac"].ToString();
                }
                catch { }

                cCache.Viajero = sValues[0];
                cCache.Contacto = sValues[0];
              

                if (!sValues[0].ToString().Equals("0"))
                {
                    cCache.Verifica = true;
                }
               
               
                try
                {
                    string sWeb = string.Empty;
                    if (!cCache.Empresa.Equals("0"))
                    {
                        sWeb = csReferencias.csWeb(cCache.Empresa).Trim();
                    }
                   
                   
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
                try
                {                   
                        cCache.Terminos = true;
                }
                catch { }

                csCache.ActualizarCache(cCache);
                clsSesiones.setAplicacion(int.Parse(cCache.Aplicacion));
              
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
        }
        public void setActualizaSesion(DataTable dtLogin, clsCache cCache, string[] sValues)
        {
            try
            {

                string sTipoPagina = clsValidaciones.GetKeyOrAdd("Cliente", "UF");
                string sAplicacion = clsSesiones.getAplicacion().ToString();
                string sEmpresa = clsValidaciones.GetKeyOrAdd("Empresa", "EM");
                string sBorrarCache = clsValidaciones.GetKeyOrAdd("BorradoCacheMinutos", "0");


                cCache.User = dtLogin.Rows[0]["strEmail"].ToString();

                cCache.Pass = dtLogin.Rows[0]["strPassword"].ToString();

                cCache.Identificacion = dtLogin.Rows[0]["strIdentificacion"].ToString();
                cCache.Nombres = dtLogin.Rows[0]["strNombre"].ToString();
                cCache.Apellidos = dtLogin.Rows[0]["strApellido"].ToString();
                cCache.Direccion = dtLogin.Rows[0]["strUbicacion"].ToString();
                cCache.Telefono = dtLogin.Rows[0]["strTelefono"].ToString();
                cCache.Celular = dtLogin.Rows[0]["strCelular"].ToString();


                cCache.Ciudad = dtLogin.Rows[0]["intCiudad"].ToString();



                cCache.Aplicacion = sAplicacion;


                if (cCache.Empresa.Length.Equals(0))
                {
                    if (cCache.Empresa != "" && cCache.Empresa != "0")
                    {
                        cCache.Empresa = sValues[1];
                    }
                }




                cCache.Email = dtLogin.Rows[0]["strEmail"].ToString();
                try
                {
                    cCache.TipoIdentificacion = dtLogin.Rows[0]["INTTIPOIDENT"].ToString();
                }
                catch { }




                try
                {
                    cCache.Genero = dtLogin.Rows[0]["INTGENERO"].ToString();
                    cCache.FechaNac = dtLogin.Rows[0]["dtmFechaNac"].ToString();
                }
                catch { }

                cCache.Viajero = sValues[0];
                cCache.Contacto = sValues[0];


                if (!sValues[0].ToString().Equals("0"))
                {
                    cCache.Verifica = true;
                }


                try
                {
                    string sWeb = string.Empty;
                    if (!cCache.Empresa.Equals("0"))
                    {
                        sWeb = csReferencias.csWeb(cCache.Empresa).Trim();
                    }


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
                try
                {
                    cCache.Terminos = true;
                }
                catch { }

                csCache.ActualizarCache(cCache);
                clsSesiones.setAplicacion(int.Parse(cCache.Aplicacion));

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
                cParametros.Complemento = "setActualizaSesion DataTable dtLogin, clsCache cCache, string[] sValues ";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public clsParametros setLogin(string strUsuario, string strPassword, bool bitEnviarMail, UserControl PageSource, Enum_Login eLigin, bool bretorno)
        {
            Label lblError1 = (Label)PageSource.FindControl("lblError1");
            csGenerales Generales = new csGenerales();
            Generales.Conexion = Conexion;
            clsLogin cLogin = new clsLogin();
            DataSet dsLogin = new DataSet();
            DataTable dtLogin = new DataTable();
            lblError1.Text = string.Empty;
            string sSesion = string.Empty;
            clsParametros cParametros = new clsParametros();
            clsCache cCache = new csCache().cCache();
            cParametros.Id = 1;

            string sUsuario = clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF");
            string sEmpresa = clsValidaciones.GetKeyOrAdd("Empresa", "EM");
            string sPropietario = clsValidaciones.GetKeyOrAdd("Propietario", "PT");
            string sViajero = clsValidaciones.GetKeyOrAdd("Viajero", "VJ");

            if (cCache.Empresa.ToString().Equals("") || cCache.Empresa.ToString().Equals("0"))
                cCache.Empresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "0");
         
          
          
            string strViajero = "0";
            string strEmpresa = "0";
         
            string strImagen = string.Empty;
            string[] sValues = new string[4];


            try
            {
                if (HttpContext.Current.Request.QueryString["idSesion"] != null)
                {
                    sSesion = HttpContext.Current.Request.QueryString["idSesion"].ToString();
                }
                else
                {
                    sSesion = HttpContext.Current.Session.SessionID;
                }
                cLogin.Conexion = Conexion;
                dtLogin = new CsConsultasVuelos().SPConsultaTabla("SPValidaUsuarioFinal",new string[4] {strUsuario,clsSesiones.getAplicacion().ToString(),cCache.Empresa.ToString(),clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF")});

                if (dtLogin.Rows.Count > 0)
                {
                    ExceptionHandled.Publicar("*************************////////////////////////////-- USUARIO ENCONTRADO MAIL: " + strUsuario + " & PASSWORD: " + strPassword + " & SESION: " + clsSesiones.getSesionID() + "  --////////////////////////");

                    strEmpresa = dtLogin.Rows[0]["intEmpresa"].ToString();
                    strViajero = dtLogin.Rows[0]["intUsuario"].ToString();

                    sValues[0] = strViajero;
                    sValues[1] = strEmpresa;

                    setActualizaSesion(dtLogin, cCache, sValues);

                    try { PageSource.Cache.Insert(sSesion, cCache); }
                    catch { }

                    if (bitEnviarMail)
                    {
                        //string ruta = default(string);
                        //csGenerales csGene = new csGenerales();
                        //Utils.Utils cUtil = new Utils.Utils();
                        //ruta = cUtil.ObtenerRutaWeb(PageSource, "PlantillaNuevoUsuario.aspx?user_id=" + cCache.Contacto + "&idSesion=" + cCache.SessionID);

                        //string strHtml = csGene.ObtenerPlantillaHTML(ruta);
                        //string sNuevoUsuario = clsValidaciones.GetKeyOrAdd("idNuevoUsuario", "2");
                        //if (strHtml != null && sNuevoUsuario != null)
                        //{

                        //    setEnviar(strHtml, cCache.User, sNuevoUsuario);
                        //} 
                    }

                    string sPantallaRespuesta = clsSesiones.getPantalleRespuestaLogin();

                    if (sPantallaRespuesta == null || sPantallaRespuesta.Length <= 0)
                    {
                        sSesion = cCache.SessionID;
                        clsValidaciones.RedirectPaginaSesion(csGeneralsPag.MiCuenta() + "?codigo=" + cCache.Contacto);
                    }
                    else
                    {
                        if (clsValidaciones.GetKeyOrAdd("RegistroFormReserva", "False").ToUpper().Equals("TRUE"))
                        {

                        }
                        else
                        {
                            //if (sPantallaRespuesta.Equals("0"))
                            //    lblError1.Text = "El usuario ha sido creado con exito";
                            //else
                            //    HttpContext.Current.Response.Redirect(sPantallaRespuesta);
                        }
                    }
                }
                else
                {
                    cParametros.Id = 0;
                    lblError1.Text = "El suario no existe";
                    ExceptionHandled.Publicar("*************************////////////////////////////-- USUARIO NO ENCONTRADO, LA CONTRASEÑA NO CORRESPONDE MAIL: " + strUsuario + " & PASSWORD: " + strPassword + " & SESION: " + clsSesiones.getSesionID() + "  --////////////////////////");
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
                cParametros.Complemento = "SetLogin; no se pudo actualizar el cache con los datos del contacto";
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        public string SetEntrarTopFlight(string Usuario, string Password,string url)
        {            
            string sEmpresa = clsValidaciones.GetKeyOrAdd("IdEmpresaTopFlight", "3");
            DataTable dtLogin = new CsConsultasVuelos().SPConsultaTabla("SPValidaUsuarioFinal", new string[4] { Usuario, clsSesiones.getAplicacion().ToString(),sEmpresa, clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF") });
            if (dtLogin != null)
            {
                if (dtLogin.Rows[0]["strPassword"].ToString().Trim().ToUpper().Equals(Password.Trim().ToUpper()))
                {
                    url += "&ids=" + Usuario + "&Ps=" + Password;
                }
                else
                {
                    url = "";
                }
            }

            return url;
        
        }
        /// Metodo registro
        /// </summary>
        /// <param name="PageSource">UserControl</param>
        /// <remarks>
        /// Autor:          Jullian arevalo
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-06-04
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:         Jullian arevalo   
        /// Fecha:           2012-06-04
        /// Descripción:    se agrega validacion para que no ingrese a login segun llave
        public clsParametros setCrearNoRegistroVuelos(UserControl parent, UserControl PageSource, Enum_Login eLigin, bool bFacturacion)
        {
            ExceptionHandled.Publicar("***///////-- INICIO DE LA CREACION DEL USUARIO & SESION: " + clsSesiones.getSesionID() + " --/////");
            clsParametros cParametros = new clsParametros();
            bool Vfail = false;
            csGeneralsPag.Idioma(PageSource);
            HiddenField hdfContactofactura = (HiddenField)PageSource.FindControl("hdfContactofactura");
            Label lblError1 = PageSource.FindControl("lblError1") as Label;
            DataList dtlPasajeros = (DataList)parent.FindControl("dtlPasajeros");
            String strClave = "";
            TextBox txtCiudad = (TextBox)PageSource.FindControl("txtCiudad");
            TextBox txtApellido = null;
            TextBox txtNombre = null;
            TextBox txtEdad1 = null;
            DropDownList ddlTipoIdentificaion = null;
            DropDownList ddlGenero = null;
            TextBox txtDocumento = null;
             string sIdioma = clsSesiones.getIdioma();
      
        if (sIdioma.Equals(""))
            sIdioma = clsValidaciones.GetKeyOrAdd("sIdioma", "es");

            try
            {
                if (dtlPasajeros != null && dtlPasajeros.Items.Count > 0)
                {
                    txtApellido = (TextBox)dtlPasajeros.Items[0].FindControl("txtApellido1");
                    txtNombre = (TextBox)dtlPasajeros.Items[0].FindControl("txtNombre1");
                    ddlTipoIdentificaion = (DropDownList)dtlPasajeros.Items[0].FindControl("ddlTipoDoc");
                    ddlGenero = (DropDownList)dtlPasajeros.Items[0].FindControl("ddlGenero");
                    txtEdad1 = (TextBox)dtlPasajeros.Items[0].FindControl("txtEdad1");
                    txtDocumento = (TextBox)dtlPasajeros.Items[0].FindControl("txtDocumento1");

                }
                else
                {
                    Vfail = true;
                }
            }
            catch
            {
                Vfail = true;
            }

            TextBox txtMailPersonal = (TextBox)PageSource.FindControl("txtMailPersonal");
            TextBox txtTelefono = (TextBox)PageSource.FindControl("txtTelefono");
            TextBox txtCelular = (TextBox)PageSource.FindControl("txtCelular");
            TextBox txtDireccion = (TextBox)PageSource.FindControl("txtDireccion");
            CheckBox chkCondicionesRegistro = PageSource.FindControl("chkCondicionesRegistro") as CheckBox;
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

                if (txtNombre.Text.Trim().Equals("") || txtApellido.Text.Trim().Equals("") || txtTelefono.Text.Trim().Equals("") ||
            txtCiudad.Text.Trim().Equals("") || txtMailPersonal.Text.Trim().Equals("") ||
            txtMailPersonal.Text.Trim().Equals(""))
                {
                    fail = true;
                }


                if (fail && Vfail)
                {
                    lblError1.Text = "Por favor diligencie todos los campos marcados como obligatorios (*)";
                    cParametros.Id = 0;
                }
                else
                {


                    lblError1.Text = string.Empty;
                    //bool bVerifica = true;
                    string strTelefono = string.Empty;
                    txtCiudad.Visible = true;
                    if (lblCiudadR != null)
                        lblCiudadR.Visible = true;

                    if (lblCiudadR1 != null)
                        lblCiudadR1.Visible = true;

                    txtTelefono.Visible = true;
                    if (lblTelf != null)
                        lblTelf.Visible = true;

                    txtCelular.Visible = true;
                    lblTCelular.Visible = true;

                    if (lblTCelular1 != null)
                        lblTCelular1.Visible = true;

                    strClave = clsValidaciones.GetKeyOrAdd("ClaveDefectoUsuario", "Tutiquete2012");

                    /*Validamos que exista la llave idEmpresa para identificar si es una aplicacion corporaivo o cliente final*/
                    string Empresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "0");

                    try
                    {
                        clsCache cCache = new csCache().cCache();
                        if (cCache != null)
                        {
                            if(cCache.Empresa != "" && cCache.Empresa!="0")
                               Empresa = cCache.Empresa;
                            else
                                cCache.Empresa = Empresa;

                        }
                    }
                    catch (Exception)
                    {
                    }


                    string valida = "";
                    DataTable dt = new CsConsultasVuelos().SPConsultaTabla("SPValidaUsuarioFinal", new string[4] { txtMailPersonal.Text, clsSesiones.getAplicacion().ToString(), Empresa, clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF") });
                    string StrCiudad = new CsConsultasVuelos().EjecutaProcedimiento("SPConsultaCiudadNombre", new string[2] { txtCiudad.Text.Trim(), sIdioma });
                    if (StrCiudad == "")
                    {
                        StrCiudad = "1";
                    }
                    if (dt != null)
                    {
                        valida = dt.Rows[0]["strpassword"].ToString();
                        string strUbicacion = "'Ning'";
                        string bit = new CsConsultasVuelos().EjecutarSPConsulta("SPModificarDatosUsuario", new string[11] { dt.Rows[0]["intUsuario"].ToString(), "'" + txtNombre.Text + "'", "'" + txtApellido.Text + "'", ddlTipoIdentificaion.SelectedItem.Value, "'" + txtDocumento.Text + "'", ddlGenero.SelectedItem.Value, "'" + txtEdad1.Text + "'", strUbicacion, "'" + txtTelefono.Text.Trim() + "'", "'" + txtCelular.Text.Trim() + "'", "'" + StrCiudad + "'" });
                        cParametros.Id = Convert.ToInt32(bit);
                    }

                    if (valida != null && valida != "")
                    {
                        HttpContext.Current.Session["enviacorreo"] = "false";

                    }
                    else
                    {
                        string StrNivel = new CsConsultasVuelos().ConsultaCodigo(clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF"),"TBLNIVELUSUARIOS","INTCODE","REFERETIPOUSUARIO");
                        
                        string strUbicacion = "'Ning'";
                        //string bit = new csGenerales().ConsultarCodigos("EXEC SPCreausuario " + clsSesiones.getAplicacion().ToString() + "," + Convert.ToInt32(StrNivel) + "," + Convert.ToInt32(Empresa) + ",'0'," + Convert.ToInt32(ddlTipoIdentificaion.SelectedItem.Value) + ",'" + txtDocumento.Text + "' , '" + txtNombre.Text + "' , '" + txtApellido.Text + "' ," + Convert.ToInt32(ddlGenero.SelectedItem.Value) + ",'" + txtEdad1.Text + "',' '," + Convert.ToInt32(StrCiudad) + ", '" + txtTelefono.Text.Trim() + "','" + txtCelular.Text.Trim() + "','" + txtMailPersonal.Text + "','" + strClave + "'," + 0);
                        string bit = new CsConsultasVuelos().EjecutarSPConsulta("SPCreausuario", new string[17] { clsSesiones.getAplicacion().ToString(), StrNivel, Empresa, "0", ddlTipoIdentificaion.SelectedItem.Value, "'" + txtDocumento.Text + "'", "'" + txtNombre.Text + "'", "'" + txtApellido.Text + "'", ddlGenero.SelectedItem.Value, "'" + txtEdad1.Text + "'", strUbicacion, "'" + StrCiudad + "'", "'" + txtTelefono.Text.Trim() + "'", "'" + txtCelular.Text.Trim() + "'", "'" + txtMailPersonal.Text + "'", "'" + strClave + "'", "0" });
                        cParametros.Id = Convert.ToInt32(bit);
                        HttpContext.Current.Session["enviacorreo"] = "true";
                    }

                    if (cParametros.Id.Equals(1))
                    {

                        if (eLigin != Enum_Login.LoginCarro)
                        {
                            if (HttpContext.Current.Session["enviacorreo"] != null)
                            {
                                Enviacorreo = bool.Parse(HttpContext.Current.Session["enviacorreo"].ToString());
                                HttpContext.Current.Session["enviacorreo"] = null;
                            }
                            cParametros = setLogin(txtMailPersonal.Text, strClave, Enviacorreo, PageSource, eLigin, true);
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
            ExceptionHandled.Publicar("**//-- FIN DE LA CREACION DEL USUARIO & SESION: " + clsSesiones.getSesionID() + " --//");
            return cParametros;
        }

        #region filtros Air mayor
        public enum eFiltro { PrecioMayorMenor = 1, PrecioMenorMayor = 2 };


        public void AplicarFiltro(Control Page, eFiltro Aplica)
        {
            DataList rptItinerario = (DataList)Page.FindControl("ucResultadoVuelos").FindControl("rptItinerario").FindControl("dtlSegmentos");


            try
            {

                var values = rptItinerario.DataSource;





                rptItinerario.DataSource = values;
                rptItinerario.DataBind();



            }
            finally
            {
            }






        }
        public void FiltrarResultadosHoras(UserControl PageSource, object sender, EventArgs e)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                string ciudad = null;

                if (cCache != null)
                {
                    try
                    {
                        ciudad = cCache.AeropuertoOrigen.SCodigo;
                        RadioButtonList chkLista = sender as RadioButtonList;
                        String str_Lista_valor_orden = chkLista.SelectedItem.Value;
                        new clsVuelos().FiltrarResultadosHoras(PageSource, str_Lista_valor_orden, ciudad);
                    }
                    catch { }
                }
            }
            catch
            {
            }
        }
        #endregion
        public void setCargar(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {

                    RadioButtonList rblFormasPago = (RadioButtonList)PageSource.FindControl("rblFormasPago");

                    if (rblFormasPago != null)
                    {
                        DataTable dtFOP = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFOPEmpresa", new string[3] { cCache.Empresa, cCache.Idioma, "AIR" });
                        rblFormasPago.DataSource = dtFOP;
                        rblFormasPago.DataTextField = "strdescripcion";
                        rblFormasPago.DataValueField = "strCodigo";
                        rblFormasPago.DataBind();

                        //if (rblFormasPago.Items.Count > 0)
                        //{
                        //    for (int i = 0; i < rblFormasPago.Items.Count; i++)
                        //    {
                        //        rblFormasPago.Items[i].Attributes.Add("onclick", "javascript:ActivarDivFP(this)");
                        //    }
                        //}


                    }

                    DropDownList ddlFranquicia = (DropDownList)PageSource.FindControl("ddlFranquicia");
                    if (ddlFranquicia != null)
                    {

                        DataTable dtFranquicias = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFranquicias", new string[2] { cCache.Empresa, "AIR" });
                        if (dtFranquicias != null)
                        {
                            ddlFranquicia.DataSource = dtFranquicias;
                            ddlFranquicia.DataTextField = "strDescripcion";
                            ddlFranquicia.DataValueField = "strcodFranquicia";
                            ddlFranquicia.DataBind();
                        }
                        else
                        {
                            ddlFranquicia.Items.Add(new ListItem("Sin Franquicias", "-1"));
                        }

                    }
                    if (cCache.Verifica == false)
                    {
                        if (clsValidaciones.GetKeyOrAdd("RegistroFormReserva", "False").ToUpper() != "TRUE")
                        {
                            clsValidaciones.RedirectPagina("Login.aspx", true);
                        }
                        else
                        {
                            new Negocios_WebService_OTA_AirLowFareSearch().GetResultadosReserva(PageSource);
                            setValidaPasajero(PageSource);

                            //las codiciones son llamdas por demanda
                            // setValidaCondiciones(PageSource);
                        }
                    }
                    else
                    {
                        new Negocios_WebService_OTA_AirLowFareSearch().GetResultadosReserva(PageSource);
                        setValidaPasajero(PageSource);

                        //las codiciones son llamdas por demanda
                        // setValidaCondiciones(PageSource);
                    }
                    setLlenarDatosVencimiento(PageSource);
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
                cParametros.Complemento = "Confirmacion Vuelos";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void setLlenarDatosVencimiento(UserControl PageSource)
        {
            try
            {
                DropDownList ddlMesVencimiento = (DropDownList)PageSource.FindControl("ddlMesVencimientoPOL");
                DropDownList txtAnioVencimiento = (DropDownList)PageSource.FindControl("txtAnioVencimientoPOL");

                string[] sMeses = new string[12];
                sMeses[0] = "Enero|01";
                sMeses[1] = "Febrero|02";
                sMeses[2] = "Marzo|03";
                sMeses[3] = "Abril|04";
                sMeses[4] = "Mayo|05";
                sMeses[5] = "Junio|06";
                sMeses[6] = "Julio|07";
                sMeses[7] = "Agosto|08";
                sMeses[8] = "Septiembre|09";
                sMeses[9] = "Octubre|10";
                sMeses[10] = "Noviembre|11";
                sMeses[11] = "Diciembre|12";
                ListItem li = null;
                if (ddlMesVencimiento != null)
                {
                    int Imes = DateTime.Now.Month;
                    for (int i = Imes - 1; i < sMeses.Length; i++)
                    {
                        li = new ListItem(sMeses[i].Split('|')[0], sMeses[i].Split('|')[1]);
                        ddlMesVencimiento.Items.Add(li);
                    }
                }

                if (txtAnioVencimiento != null)
                {
                    int IAnio = DateTime.Now.Year;
                    for (int x = 0; x <= 10; x++)
                    {
                        li = new ListItem((IAnio + x).ToString(), (IAnio + x).ToString());
                        txtAnioVencimiento.Items.Add(li);
                    }
                }
            }
            catch { }
        }
        //public void setCargar(UserControl PageSource)
        //{
        //    csGeneralsPag.Idioma(PageSource);

        //    clsParametros cParametros = new clsParametros();
        //    try
        //    {
        //        clsCache cCache = new csCache().cCache();
        //        if (cCache != null)
        //        {

        //            RadioButtonList rblFormasPago = (RadioButtonList)PageSource.FindControl("rblFormasPago");

        //            if (rblFormasPago != null)
        //            {
        //                DataTable dtFOP = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFOPEmpresa", new string[3] { cCache.Empresa, cCache.Idioma, "AIR" });
        //                rblFormasPago.DataSource = dtFOP;
        //                rblFormasPago.DataTextField = "strdescripcion";
        //                rblFormasPago.DataValueField = "strCodigo";
        //                rblFormasPago.DataBind();

                       


        //            }

        //            DropDownList ddlFranquicia = (DropDownList)PageSource.FindControl("ddlFranquicia");
        //            if (ddlFranquicia != null)
        //            {

        //                DataTable dtFranquicias = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFranquicias", new string[2] { cCache.Empresa, "AIR" });
        //                if (dtFranquicias != null)
        //                {
        //                    ddlFranquicia.DataSource = dtFranquicias;
        //                    ddlFranquicia.DataTextField = "strDescripcion";
        //                    ddlFranquicia.DataValueField = "strcodFranquicia";
        //                    ddlFranquicia.DataBind();
        //                }
        //                else
        //                {
        //                    ddlFranquicia.Items.Add(new ListItem("Sin Franquicias", "-1"));
        //                }

        //            }
        //            if (cCache.Verifica == true)
        //            {
        //                if (clsValidaciones.GetKeyOrAdd("RegistroFormReserva", "False").ToUpper() != "TRUE")
        //                {
        //                    clsValidaciones.RedirectPagina("Login.aspx", true);
        //                }
        //                else
        //                {
        //                    new Negocios_WebService_OTA_AirLowFareSearch().GetResultadosReserva(PageSource);
        //                    setValidaPasajero(PageSource);
                            
        //                    //las codiciones son llamdas por demanda
        //                    // setValidaCondiciones(PageSource);
        //                }
        //            }
        //            else
        //            {
        //                new Negocios_WebService_OTA_AirLowFareSearch().GetResultadosReserva(PageSource);
        //                setValidaPasajero(PageSource);

        //                //las codiciones son llamdas por demanda
        //                // setValidaCondiciones(PageSource);


        //            }
        //        }
        //        else
        //        {
        //            csGeneralsPag.FinSesion();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        cParametros.Id = 0;
        //        cParametros.Message = Ex.Message.ToString();
        //        cParametros.Source = Ex.Source.ToString();
        //        cParametros.Tipo = clsTipoError.Library;
        //        cParametros.Severity = clsSeveridad.Moderada;
        //        cParametros.StackTrace = Ex.StackTrace.ToString();
        //        cParametros.Complemento = "Confirmacion Vuelos";
        //        ExceptionHandled.Publicar(cParametros);
        //    }
        //}

        public void setCargarFranquiciasFOP(DropDownList ddlFranquicia, string sFOP)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    if (ddlFranquicia != null)
                    {
                        DataTable dtFranquicias = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFranquiciasFOP", new string[3] { cCache.Empresa, "AIR", sFOP });
                        if (dtFranquicias != null)
                        {
                            ddlFranquicia.DataSource = dtFranquicias;
                            ddlFranquicia.DataTextField = "strDescripcion";
                            ddlFranquicia.DataValueField = "strcodFranquicia";
                            ddlFranquicia.DataBind();
                        }
                        else
                        {
                            ddlFranquicia.Items.Add(new ListItem("Sin Franquicias", "-1"));
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
                cParametros.Complemento = "Confirmacion Vuelos";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        /// <summary>
        /// Valida Condiciones via OtaAirRule
        /// </summary>
        /// <param name="PageSource"></param>
        public void setValidaCondiciones(UserControl PageSource)
        {
            try
            {
                string sPax = "ADT";
                Ssoft.ValueObjects.Vuelos.VO_OTA_AirRulesRQ clMsgrule = new Ssoft.ValueObjects.Vuelos.VO_OTA_AirRulesRQ();
                OTA_AirRulesRS clsRsMsg = new OTA_AirRulesRS();
                clsOTA_AirRules clsObj = new clsOTA_AirRules();
                List<VO_OriginDestinationInformation> ListOrDestino = null;
                
                try
                {
                 
                    DataTable dtPassengerQuantity = clsSesiones.GetDatasetSabreAir().Tables["PassengerTypeQuantity"];
                    if (dtPassengerQuantity != null)
                        sPax = dtPassengerQuantity.Rows[0]["Code"].ToString();

                    ListOrDestino = clsSesiones.getParametrosAirBargain().Lvo_Rutas;
                }
                catch { }

                Repeater rptItinerario = (Repeater)PageSource.FindControl("rptItinerario");
                string sRespuesta = string.Empty;
                string sComando = "WPRD*P" + sPax + "¥C2/15/16/19/22¥S1";

                //Invoke WS Rules sabre
                if (HttpContext.Current.Session["$OtaRule"] != null)
                {
                    clMsgrule = (Ssoft.ValueObjects.Vuelos.VO_OTA_AirRulesRQ)(HttpContext.Current.Session["$OtaRule"]);
                    clMsgrule.Vo_AeropuertoDestino = ListOrDestino[0].Vo_AeropuertoDestino;
                    clsObj.Session_ = AutenticacionSabre.GET_SabreSession();
                    clsRsMsg = clsObj.getRules(clMsgrule);
                   
                    if (!(clsRsMsg.Errors != null))
                    {
                        sRespuesta = OtaRule(clsRsMsg.FareRuleResponseInfo.FareRuleInfo.TPA_Extensions.FareType.Text);
                    }
                }


            

                /*POLITICAS DE SABRE*/
                if (rptItinerario != null)
                {
                   
                    Label lblCondicionesSabre = (Label)PageSource.FindControl("pnlUpdate").FindControl("pnlSabre").FindControl("lblCondicionesSabre");
                    if (lblCondicionesSabre != null)
                    {
                        lblCondicionesSabre.Text = lblCondicionesSabre.Text + "</br>" + sRespuesta;
                    }
                }
            }
            catch { }
        }
        private string OtaRule(string[] msg)
        {
            string sMsg = string.Empty;


            for (int i = 0; i < msg.Length; i++)
            {
                if (i == 0)
                {
                    sMsg = msg[i];
                }
                else
                {
                    sMsg = sMsg + "," + msg[i];
                }

            }
            return sMsg;
        }
        public void setCargarResult(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        new Negocios_WebService_OTA_AirLowFareSearch().getBusqueda(PageSource);
                        setResumen(PageSource);
                    }
                    catch { }
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
                cParametros.Complemento = "Confirmacion Vuelos";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
               
            }
        }
        /// <summary>
        /// Find 19 result to multidestination
        /// hceron 23052013
        /// </summary>
        /// <param name="PageSource"></param>
        public void setCargarResultMulti19(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        new Negocios_WebService_OTA_AirLowFareSearch().getBusquedaMulti(PageSource);
                        setResumen(PageSource);
                    }
                    catch { }
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
                cParametros.Complemento = "Confirmacion Vuelos";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                
            }
        }
        public void setItinerario(UserControl PageSource, object source, RepeaterCommandEventArgs e)
        {
            csGeneralsPag.Idioma(PageSource);
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    string sItinerario = "0";
                    if (clsValidaciones.GetKeyOrAdd("bMostrarResultadosBFM", "False").ToUpper().Equals("TRUE"))
                    {
                        string sVueloIda = "0";
                        string sVueloReg = "0";
                        RepeaterItem rptItem = e.Item;
                        Repeater dlIda = (Repeater)rptItem.FindControl("RptSegmentosIda");
                        Repeater dlRegreso = (Repeater)rptItem.FindControl("RptSegmentosReg");
                        if (dlIda != null)
                            sVueloIda = getNoVueloSel(dlIda);

                        if (dlRegreso != null)
                            sVueloReg = getNoVueloSel(dlRegreso);

                        sItinerario = getItinerarioSelVuelos(sVueloIda, sVueloReg);
                    }
                    else
                    {
                        int iSecuencia = int.Parse(e.CommandArgument.ToString());
                        sItinerario = iSecuencia.ToString();
                    }

                    string sPagina = "";
                    if (clsValidaciones.GetKeyOrAdd("ReservaTarjeta", "TRUE").Trim().ToUpper().Equals("TRUE"))
                    {
                        sPagina = "ReservaVuelosTarjeta.aspx?ITIID=" + sItinerario;
                    }
                    else
                    {
                        sPagina = "ReservaVuelos.aspx?ITIID=" + sItinerario;                    
                    }
                    try
                    {
                        String strTipoPlan = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
                        //csPlanes objPlanes = new csPlanes();
                      
                       
                        //DataTable dtPlanes = objPlanes.ConsultaPlanes(strTipoPlan, clsSesiones.getIdioma(), clsSesiones.getAplicacion().ToString());
                        //if (dtPlanes != null && dtPlanes.Rows.Count > 0)
                        //    sPagina = dtPlanes.Rows[0]["strLink"].ToString() + "?ITIID=" + sItinerario;
                    }
                    catch (Exception){}
                  
                    clsSesiones.setPantalleRespuestaLogin(sPagina);
                    HtmlGenericControl dPanel = (HtmlGenericControl)PageSource.FindControl("dPanel");
                    try
                    {
                        bool bItinerario = new Negocios_WebService_OTA_AirLowFareSearch().setclsOTA_AirBook(Convert.ToInt32(sItinerario), PageSource, dPanel);
                        if (bItinerario)
                        {
                            try
                            {
                                if (clsSesiones.getAerolineaValidadora() != null)
                                {
                                    cCache.AerolineaValidadora = clsSesiones.getAerolineaValidadora();
                                    csCache.ActualizarCache(cCache);
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            sPagina = "ResultadoVuelos.aspx?Msjpop=El itinerario solicitado no tiene disponibilidad, por favor seleccione otro&Fijo=1";
                        }

                        //if (HttpContext.Current.Session["$Tbl_SEgmentos_No_Disp"] == null)
                        //{
                            clsValidaciones.RedirectPagina(sPagina, false);
                        //}
                    }
                    catch { }
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch
            {
            }
        }
        //Validacion itinerarios por segmento
        //Juan Camilo Diaz 
        //2013-10-03
        public bool setItinerarioValida(UserControl PageSource, object source, RepeaterCommandEventArgs e)
        {
            csGeneralsPag.Idioma(PageSource);
            bool bItinerario = false;
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    int iSecuencia = int.Parse(e.CommandArgument.ToString());
                    string sPagina = "ReservaVuelos.aspx?ITIID=" + iSecuencia.ToString();

                    clsSesiones.setPantalleRespuestaLogin(sPagina);



                    HtmlGenericControl dPanel = (HtmlGenericControl)PageSource.FindControl("dPanel");
                    try
                    {
                        //jarevalo change method to sell
                        //29/05/2013
                        bItinerario = new Negocios_WebService_OTA_AirLowFareSearch().setclsOTA_AirBook(e, PageSource, dPanel);

                        if (bItinerario)
                        {
                            try
                            {
                                if (clsSesiones.getAerolineaValidadora() != null)
                                {
                                    cCache.AerolineaValidadora = clsSesiones.getAerolineaValidadora();
                                    csCache.ActualizarCache(cCache);
                                }
                            }
                            catch { }
                        }
                        
                    }
                    catch { }
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch
            {
            }
            return bItinerario;
        }
        #region multudestinos
        //hceron
        //24052012
        public void setItinerarioMulti(UserControl PageSource, object source, RepeaterCommandEventArgs e)
        {
            csGeneralsPag.Idioma(PageSource);
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    string sItinerario = "0";
                   
                  
                        int iSecuencia = int.Parse(e.CommandArgument.ToString());
                        sItinerario = iSecuencia.ToString();
                  

                    string sPagina = "ReservaVuelosTarjeta.aspx?ITIID=" + sItinerario;
                    try
                    {
                        String strTipoPlan = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
                      
                      
                       
                        //DataTable dtPlanes = objPlanes.ConsultaPlanes(strTipoPlan, clsSesiones.getIdioma(), clsSesiones.getAplicacion().ToString());
                        //if (dtPlanes != null && dtPlanes.Rows.Count > 0)
                        //    sPagina = dtPlanes.Rows[0]["strLink"].ToString() + "?ITIID=" + sItinerario;
                    }
                    catch (Exception) { }
                    clsSesiones.setPantalleRespuestaLogin(sPagina);
                    HtmlGenericControl dPanel = (HtmlGenericControl)PageSource.FindControl("dPanel");
                    try
                    {
                        bool bItinerario = new Negocios_WebService_OTA_AirLowFareSearch().setclsOTA_AirBook(Convert.ToInt32(sItinerario), PageSource, dPanel,1);
                        if (bItinerario)
                        {
                            try
                            {
                                if (clsSesiones.getAerolineaValidadora() != null)
                                {
                                    cCache.AerolineaValidadora = clsSesiones.getAerolineaValidadora();
                                    csCache.ActualizarCache(cCache);
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            sPagina = "ResultadoVuelosMulti.aspx?Msjpop=El itinerario solicitado no tiene disponibilidad, por favor seleccione otro&Fijo=1";
                        }
                        clsValidaciones.RedirectPagina(sPagina, false);
                    }
                    catch { }
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch
            {
            }
        }
        #endregion
        public string getNoVueloSel(Repeater dlDatos)
        {
            string sVuelo = "0";
            try
            {
                for (int i = 0; i < dlDatos.Items.Count; i++)
                {
                    RadioButton rbSel = (RadioButton)dlDatos.Items[i].FindControl("rbtnSel");
                    if (rbSel != null)
                    {
                        if (rbSel.Checked)
                        {
                            Label lblNoVuelo = (Label)dlDatos.Items[i].FindControl("lblFly");
                            if (lblNoVuelo != null)
                            {
                                sVuelo = lblNoVuelo.Text;
                                break;
                            }
                        }
                    }
                }
            }
            catch { }
            return sVuelo;
        }
        public string getItinerarioSelVuelos(string sVueloIda, string sVueloReg)
        {
            string sItinerario = "0";
            try
            {
                DataSet dsSabreAir = new DataSet();
                dsSabreAir = clsSesiones.GetDatasetSabreAir();
                DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];

               
                foreach (DataRow drItinerario in dtItinerario.Rows)
                {
                    int CountCoincidencias = 0;
                    foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("PricedItinerary_AirItinerary"))
                    {
                        foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItinerary_OriginDestinationOptions"))
                        {
                            foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("OriginDestinationOptions_OriginDestinationOption"))
                            {
                                foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("OriginDestinationOption_FlightSegment"))
                                {
                                    if (sVueloReg == "0")
                                    {
                                        if (drRelacionCuatro["FlightNumber"].ToString().Equals(sVueloIda))
                                            CountCoincidencias++;
                                    }
                                    else
                                    {
                                        if (drRelacionCuatro["FlightNumber"].ToString().Equals(sVueloIda) || drRelacionCuatro["FlightNumber"].ToString().Equals(sVueloReg))
                                            CountCoincidencias++;
                                    }
                                }
                            }
                            if (sVueloReg == "0")
                            {
                                if (CountCoincidencias > 0)
                                {
                                    sItinerario = drItinerario["SequenceNumber"].ToString();

                                    throw new Exception("end loop");
                                }
                            }
                            else
                            {
                                if (CountCoincidencias > 1)
                                {
                                    sItinerario = drItinerario["SequenceNumber"].ToString();
                                    throw new  Exception("end loop");
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return sItinerario;
        }
        /// <summary>
        /// get value of SequenceNumber from OriginDestinationOption_Id
        /// </summary>
        /// <param name="sOrgId"></param>
        /// <returns></returns>
        public DataTable getItinerarioSelVuelos(string sOrgId, DataSet dsSabreAir)
        {

            DataTable dtItinerario = null;
            try
            {
             
              
                DataTable dtAirItinerary = dsSabreAir.Tables["AirItinerary"];
                DataTable dtOriginDest = dsSabreAir.Tables["OriginDestinationOption"];

                string dtAirItId = string.Empty;
                string OriginDId = string.Empty;
                string OriginId = string.Empty;
                string sWhere = string.Empty;


                sWhere = "OriginDestinationOption_Id = " + sOrgId.ToString();
                //Obtenemos el Id del OriginDestinationOptions_Id
                DataTable dtOriginDests = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["OriginDestinationOption"]);
                OriginDId = dtOriginDests.Rows[0]["OriginDestinationOptions_Id"].ToString();

                sWhere = "OriginDestinationOptions_Id = " + OriginDId.ToString();
                DataTable dtOrgs= clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["OriginDestinationOptions"]);
                OriginId = dtOrgs.Rows[0]["AirItinerary_Id"].ToString();

                sWhere = "AirItinerary_Id = " + OriginId.ToString();
                DataTable dtAirIt = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["AirItinerary"]);
                dtAirItId = dtAirIt.Rows[0]["PricedItinerary_Id"].ToString();


                sWhere = "PricedItinerary_Id = " + dtAirItId.ToString();
                 dtItinerario = clsDataNet.dsDataWhere(sWhere, dsSabreAir.Tables["PricedItinerary"]);
               
            }
            catch { }
            return dtItinerario;
        }
        public void setItinerarioTarifa(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    string[] sValue = csValue(PageSource);
                    Negocios_WebService_OTA_AirLowFareSearch Busqueda = new Negocios_WebService_OTA_AirLowFareSearch();

                    HtmlGenericControl dPanel = (HtmlGenericControl)PageSource.FindControl("dPanel");
                    try
                    {
                        SetShorSell(PageSource, dPanel, sValue[4]);
                        try
                        {
                            getCotizarPlan(PageSource, dPanel);
                        }
                        catch { }
                    }
                    catch { }
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
                cParametros.Complemento = "Itinerarios Vuelos";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void getCotizarPlan(UserControl PageSource, HtmlGenericControl dPanel)
        {
            clsResultados objResultados = new clsResultados();
            clsParametros objParametros = new clsParametros();
            clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
            clsVuelos objVuelos = new clsVuelos();
            Negocios_WebService_OTA_AirLowFareSearch Busqueda = new Negocios_WebService_OTA_AirLowFareSearch();
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirProceRQ = clsSesiones.getParametrosAirBargain(); ;
                    objResultados = Busqueda.GetDsCotizaSabreAir(vo_OTA_AirProceRQ);

                    if (objResultados.Error.Id == 1)
                    {
                        DataSet dsSabreAir = objResultados.dsResultados;
                        objVuelos.ModificarDatasetSabreAirCotiza(dsSabreAir, vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto, vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo);
                        try
                        {
                            objVuelos.InicializarPaginaPlanes(PageSource, dsSabreAir);
                        }
                        catch { }
                    }
                    else if (objResultados.Error.Id == 0)
                    {
                        objErrorMensaje.getError(objResultados.Error, dPanel);
                        ExceptionHandled.Publicar(objResultados.Error.Ex);
                    }
                }
            }
            catch (Exception Ex)
            {
                objParametros.Id = 0;
                objParametros.Message = Ex.Message;
                objParametros.Metodo = Ex.TargetSite.Name;
                objParametros.Source = Ex.Source;
                objParametros.StackTrace = Ex.StackTrace;
                if (objParametros.ViewMessage.Count.Equals(0))
                {
                    objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                    objParametros.Sugerencia.Add("Por favor intente de nuevo");
                }
                else
                {
                    objParametros.ViewMessage[0] = "No se encontraron vuelos disponibles";
                    objParametros.Sugerencia[0] = "Por favor intente de nuevo";
                }
                objParametros.Severity = clsSeveridad.Media;
                objParametros.Tipo = clsTipoError.WebServices;
                ExceptionHandled.Publicar(objParametros);
                objErrorMensaje.getError(objParametros, dPanel);
            }
        }
        public bool SetShorSell(UserControl ucControl, HtmlGenericControl dPanel, string sRPH)
        {
            string strMensaje = string.Empty;
            string sVenta = string.Empty;
            string sItinerario = string.Empty;
            clsResultados objResultados = new clsResultados();
            clsParametros objParametros = new clsParametros();
            clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
            bool bItinerario = false;
            string intIdItinerario = sRPH;
            try
            {
                Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno("XIA");
                sVenta = Negocios_WebServiceSabreCommand._EjecutarComando("JR0" + intIdItinerario);
                try
                {
                    objParametros.Id = 1;
                    objParametros.TipoLog = Enum_Error.Transac;
                    objParametros.Severity = clsSeveridad.Media;
                    objParametros.Tipo = clsTipoError.WebServices;
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objParametros.Complemento = "HostCommand: JR0" + intIdItinerario + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                    try
                    {
                        objParametros.TargetSite = "Response  " + sVenta;
                        clsCache cCache = new csCache().cCache();
                        if (cCache != null)
                        {
                            objParametros.Source = "Sesion Local: " + cCache.SessionID.ToString();
                        }
                        else
                        {
                            objParametros.Source = "Sesion Local: No hay cache ";
                        }
                    }
                    catch
                    {
                        objParametros.Source = "Sesion Local: Error ";
                    }
                    ExceptionHandled.Publicar(objParametros);
                    objParametros.TipoLog = Enum_Error.Log;
                }
                catch { }
                if (string.IsNullOrEmpty(sVenta))
                {
                    objParametros.Id = 0;
                    objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                    objParametros.Sugerencia.Add("Por favor intente de nuevo");
                    objParametros.Severity = clsSeveridad.Media;
                    objParametros.Tipo = clsTipoError.WebServices;
                    objParametros.Complemento = "HostCommand: JR0" + intIdItinerario + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                    objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    objErrorMensaje.getError(objParametros, dPanel);
                    ExceptionHandled.Publicar(objParametros);
                }
                else
                {
                    if (sVenta.Trim().Contains("NO JOURNEY RECORD PRESENT"))
                    {
                        objParametros.Id = 0;
                        objParametros.Info = sVenta;
                        objParametros.Message = sVenta;
                        objParametros.ViewMessage.Add("No se encontraron vuelos disponibles.");
                        objParametros.Sugerencia.Add("Por favor intente de nuevo");
                        objParametros.Severity = clsSeveridad.Media;
                        objParametros.Tipo = clsTipoError.WebServices;
                        objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                        objParametros.Complemento = "HostCommand: JR0" + intIdItinerario + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                        objErrorMensaje.getError(objParametros, dPanel);
                        ExceptionHandled.Publicar(objParametros);
                    }
                    else
                    {
                        if (sVenta.Contains("UNABLE TO SELL SEGMENTS") || sVenta.Contains("ERROR"))
                        {
                            objParametros.Id = 0;
                            objParametros.Message = sVenta;
                            objParametros.Info = sVenta;
                            objParametros.ViewMessage.Add("No se encontraron vuelos disponibles.");
                            objParametros.Sugerencia.Add("Por favor intente de nuevo");
                            objParametros.Severity = clsSeveridad.Media;
                            objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                            objParametros.Tipo = clsTipoError.WebServices;
                            objParametros.Complemento = "HostCommand: JR0" + intIdItinerario + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                            objErrorMensaje.getError(objParametros, dPanel);
                            ExceptionHandled.Publicar(objParametros);
                        }
                        else
                        {
                            bItinerario = true;
                            if (sVenta.Contains("VALIDATING CARRIER -"))
                            {
                                string strAerolinea = sVenta.Substring(sVenta.IndexOf("VALIDATING CARRIER -") + 21, 2);
                                clsSesiones.setAerolineaValidadora(strAerolinea);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                objParametros.Id = 0;
                objParametros.Message = Ex.Message;
                objParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                objParametros.Source = Ex.Source;
                objParametros.StackTrace = Ex.StackTrace;
                if (objParametros.ViewMessage.Count.Equals(0))
                {
                    objParametros.ViewMessage.Add("No se encontraron vuelos disponibles");
                    objParametros.Sugerencia.Add("Por favor intente de nuevo");
                }
                else
                {
                    objParametros.ViewMessage[0] = "No se encontraron vuelos disponibles";
                    objParametros.Sugerencia[0] = "Por favor intente de nuevo";
                }
                objParametros.Severity = clsSeveridad.Media;
                objParametros.Tipo = clsTipoError.WebServices;
                objParametros.Complemento = "HostCommand: JR0" + intIdItinerario + ".  Disponibilidad: " + sVenta + ".  Itinerario: " + sItinerario;
                objErrorMensaje.getError(objParametros, dPanel);
                ExceptionHandled.Publicar(objParametros);
            }
            return bItinerario;
        }
        public void setItinerarioTarifaReserva(UserControl PageSource, object source, RepeaterCommandEventArgs e)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    string sSecuencia = e.CommandArgument.ToString();
                    clsSesiones.setPantalleRespuestaLogin("ReservaVuelos.aspx?ITIID=" + sSecuencia);
                    Negocios_WebService_OTA_AirLowFareSearch Busqueda = new Negocios_WebService_OTA_AirLowFareSearch();

                    clsVuelos cVuelos = new clsVuelos();
                    HtmlGenericControl dPanel = (HtmlGenericControl)PageSource.FindControl("dPanel");
                    try
                    {
                        SetShorSell(PageSource, dPanel, sSecuencia);
                        try
                        {
                            csBuscador cBuscador = new csBuscador();
                            cBuscador.setCambiaHorasConvenio();
                            cVuelos.GetCrearDatasetSelectSabrePlanes(sSecuencia);
                            Busqueda.getCotizar(PageSource, dPanel);
                        }
                        catch { }
                    }
                    catch { }
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
                cParametros.Complemento = "Itinerarios Vuelos";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void setOrdenar(UserControl PageSource, object sender, EventArgs e)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        RadioButtonList chkLista = sender as RadioButtonList;
                        String str_Lista_valor_orden = chkLista.SelectedItem.Value;
                        new clsVuelos().OrdenarResultados(PageSource, str_Lista_valor_orden);
                    }
                    catch { }
                }
            }
            catch
            {
            }
        }
        #region Ordenar Resultados BFM
        /// <summary>
        /// Order by DDlist
        /// hceron
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void setOrdenarDDlist(UserControl PageSource, object sender, EventArgs e)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        DropDownList chkLista = sender as DropDownList;
                        String str_Lista_valor_orden = chkLista.SelectedValue;
                        new clsVuelos().OrdenarResultadosBFM(PageSource, str_Lista_valor_orden);
                    }
                    catch { }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Load detail of Fly
        /// </summary>
        /// <param name="PAGE"></param>
        /// <param name="iNumberFly"></param>
        public void setLoadShowModal(Control PAGE, string iValue)
        {

            DataSet dsSabreAir = new DataSet();
            dsSabreAir = clsSesiones.GetDatasetSabreAir();
          
            char[] delimiterChars = { '|' };
            string[] svalues = iValue.Split(delimiterChars);

            UserControl ucResultadoVuelos = (UserControl)PAGE.FindControl("ucResultadoVuelos");
            AjaxControlToolkit.ModalPopupExtender sModal = ucResultadoVuelos.FindControl("mdlDetail") as AjaxControlToolkit.ModalPopupExtender;
            Repeater rptItinerario = ucResultadoVuelos.FindControl("rptItineraryModal") as Repeater;


            rptItinerario.DataSource = getItinerarioSelVuelos(svalues[1].ToString(), dsSabreAir);
            rptItinerario.DataBind();
            try
            {
                //strTrayecto

                new clsVuelos().CargarSegmentosModal(rptItinerario, svalues[2]);
            }
            catch { }

            sModal.Show();


        }


        #endregion
        public void setFiltrar(UserControl PageSource, RepeaterCommandEventArgs e)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        new clsVuelos().FiltrarResultados(PageSource, e.CommandArgument.ToString());
                    }
                    catch { }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Filter to hedaer in result
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="e"></param>
        public void setFiltrar(UserControl PageSource,  CommandEventArgs e)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        new clsVuelos().FiltrarResultados(PageSource, e.CommandArgument.ToString());
                    }
                    catch { }
                }
            }
            catch
            {
            }
        }
        #region Otros filtros

        public void setOtrosFiltros(Control PAGE, string e, UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        new clsVuelos().FiltrarResultadosOtros(PAGE, e);
                    }
                    catch { }
                }
            }
            catch
            {
            }
        }

        public void setOtrosFiltrosAerolinea(Control PAGE, string e, UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        new clsVuelos().FiltrarResultadosAerolinea(PAGE, e);
                    }
                    catch { }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Filter per Air
        /// hceron
        /// </summary>
        /// <param name="PAGE"></param>
        /// <param name="e"></param>
        /// <param name="PageSource"></param>
        public void setOtrosFiltrosAerolineaBFM(Control PAGE, string e, UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        new clsVuelos().FiltrarResultadosAerolineaBFM(PAGE, e);
                    }
                    catch { }
                }
            }
            catch
            {
            }
        }
        #endregion
        public void setItinerarioIda(UserControl PageSource, object source, RepeaterCommandEventArgs e)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            int iPosDiv = 0;
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    setResumen(PageSource);
                    if (e.CommandArgument.Equals("Cotizar"))
                    {
                        setItinerarioHoras(PageSource, source, e);
                    }
                    else
                    {
                        if (e.CommandArgument.Equals("Regresar"))
                        {
                            csGeneralsPag.Buscador(); ;
                        }
                        else
                        {
                            clsVuelos objVuelos = new clsVuelos();
                            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                            Negocios_WebService_OTA_AirLowFareSearch Busqueda = new Negocios_WebService_OTA_AirLowFareSearch();
                            HtmlGenericControl dPanel = (HtmlGenericControl)PageSource.FindControl("dPanel");
                            try
                            {
                                try
                                {
                                    vo_OTA_AirLowFareSearchLLSRQ.LsRPH.Add(e.CommandArgument.ToString());
                                    clsSesiones.setParametrosAirBargain(vo_OTA_AirLowFareSearchLLSRQ);
                                }
                                catch (Exception Ex) { string efecto = Ex.Message; }
                                try
                                {
                                    if (vo_OTA_AirLowFareSearchLLSRQ.Ruta.Equals(0))
                                    {
                                        objVuelos.GetCrearDatasetSelectSabre(0, null);
                                        iPosDiv = 0;
                                    }
                                    else
                                    {
                                        objVuelos.GetCrearDatasetSelectSabre(vo_OTA_AirLowFareSearchLLSRQ.Ruta, vo_OTA_AirLowFareSearchLLSRQ.LsRPH[vo_OTA_AirLowFareSearchLLSRQ.Ruta - 1]);
                                        vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                                        iPosDiv = 1;
                                    }
                                }
                                catch { }
                                string intIdItinerario = e.CommandArgument.ToString();
                                string strTipoPlan = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
                                DataTable dtItinerario = new DataTable();
                                clsSesiones.SetDataTameAir(null);

                                dtItinerario = new clsVuelos().GetDtGetItinerarioHora(intIdItinerario);
                                if (dtItinerario.Rows[0]["Ws"].ToString().Equals(strTipoPlan))
                                {
                                    Busqueda.getVentaSegmentoCommand(dPanel);
                                    if (vo_OTA_AirLowFareSearchLLSRQ.Ruta.Equals(2))
                                    {
                                        Busqueda.getCotizar(PageSource, dPanel);
                                        objVuelos.InicializarPaginaHoras(PageSource, null);
                                        iPosDiv = 2;
                                    }
                                    else
                                    {
                                        Busqueda.getBusqueda(PageSource);
                                        iPosDiv = 1;
                                    }
                                }

                                try
                                {
                                    Repeater rptSeleccion = (Repeater)PageSource.FindControl("rptSeleccion");
                                    HtmlGenericControl divTituloxhora = (HtmlGenericControl)PageSource.FindControl("divTituloxhora");
                                    Label lblTituloSelect = (Label)PageSource.FindControl("lblTituloSelect");
                                    HtmlGenericControl divRegreso = (HtmlGenericControl)rptSeleccion.Items[0].FindControl("divRegreso");
                                    HtmlGenericControl divIda = (HtmlGenericControl)rptSeleccion.Items[0].FindControl("divIda");

                                    if (iPosDiv.Equals(2))
                                    {
                                        divRegreso.Style.Add("display", "block");
                                        divIda.Style.Add("display", "block");
                                        divTituloxhora.Style.Add("display", "none");
                                    }
                                    else
                                    {
                                        divTituloxhora.Style.Add("display", "block");
                                        if (iPosDiv.Equals(0))
                                        {
                                            lblTituloSelect.Text = "Seleccione el itinerario de Ida";
                                            divRegreso.Style.Add("display", "none");
                                            divIda.Style.Add("display", "none");
                                        }
                                        else
                                        {
                                            lblTituloSelect.Text = "Seleccione el itinerario de Regreso";
                                            divRegreso.Style.Add("display", "none");
                                            divIda.Style.Add("display", "block");
                                        }
                                    }
                                }
                                catch { }
                            }
                            catch { }
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
                cParametros.Complemento = "Itinerarios Vuelos";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void setConfirmaVentana(UserControl PageSource, string sRecord)
        {
            try
            {
                string bRemarkOracle = clsValidaciones.GetKeyOrAdd("bRemarkOracle", "False");
                if (bRemarkOracle.ToUpper().Equals("TRUE"))
                {
                    HtmlGenericControl cPanel = (HtmlGenericControl)PageSource.FindControl("cPanel");
                    if (cPanel != null)
                    {
                        string sEnvioResExterna = clsValidaciones.GetKeyOrAdd("sEnvioResExterna", "http://66.165.144.42/Aviatur/Aviatur/Default.aspx");
                        string sPagina = sEnvioResExterna + "?RECORD=" + sRecord;
                        StringBuilder dPanel = new StringBuilder();
                        dPanel.AppendLine(" <iframe  id=iframe scrolling='auto' src='" + sPagina + "' frameBorder=0  width=100% height=100></iframe>");
                        cPanel.Controls.Clear();
                        cPanel.InnerHtml = dPanel.ToString();
                    }
                }
            }
            catch { }
        }
        public void setItinerarioHoras(UserControl PageSource, object source, RepeaterCommandEventArgs e)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    HtmlGenericControl dPanel = (HtmlGenericControl)PageSource.FindControl("dPanel");
                    try
                    {
                        clsSesiones.setPantalleRespuestaLogin("ReservaVuelos.aspx?ITIID=" + e.Item.ItemIndex.ToString());
                        clsValidaciones.RedirectPagina("Login.aspx", false);
                    }
                    catch { }
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
                cParametros.Complemento = "Itinerarios Vuelos";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void setOrdenarHoras(UserControl PageSource, object sender, EventArgs e)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        CheckBoxList chkLista = sender as CheckBoxList;
                        String str_Lista_valor_orden = chkLista.SelectedItem.Value;
                        new clsVuelos().OrdenarResultadosHoras(PageSource, str_Lista_valor_orden);
                    }
                    catch { }
                }
            }
            catch
            {
            }
        }
        public bool bValidaFechas(UserControl PageSource)
        {
            ExceptionHandled.Publicar("*************************////////////////////////////-- INICIO DE LA VALIDACION DE FECHAS DE VUELOS & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");
            bool bValida = true;
            try
            {
                bool bValidaTemp = true;

                Label lblError = PageSource.FindControl("lblError") as Label;
                int iYearMinADT = 12;
                int iYearMinCNN = 2;
                int iYearMinINF = 0;

                int iYearMaxADT = 100;
                int iYearMaxCNN = 12;
                int iYearMaxINF = 2;

                DataList dtlPasajeros = PageSource.FindControl("dtlPasajeros") as DataList;
                for (int d = 0; d < dtlPasajeros.Items.Count; d++)
                {
                    TextBox txtDia = dtlPasajeros.Items[d].FindControl("txtDia") as TextBox;
                    TextBox txtMes = dtlPasajeros.Items[d].FindControl("txtMes") as TextBox;
                    TextBox txtYear = dtlPasajeros.Items[d].FindControl("txtYear") as TextBox;
                    TextBox txtEdad1 = dtlPasajeros.Items[d].FindControl("txtEdad1") as TextBox;

                    Label lblErrorFecha = dtlPasajeros.Items[d].FindControl("lblErrorFecha") as Label;
                    TextBox txtTipoPasajero1 = dtlPasajeros.Items[d].FindControl("txtTipoPasajero1") as TextBox;
                    TextBox txtNombre1 = dtlPasajeros.Items[d].FindControl("txtNombre1") as TextBox;
                    TextBox txtApellido1 = dtlPasajeros.Items[d].FindControl("txtApellido1") as TextBox;
                    TextBox txtDocumento1 = dtlPasajeros.Items[d].FindControl("txtDocumento1") as TextBox;
                    TextBox txtDocumento = dtlPasajeros.Items[d].FindControl("txtDocumento") as TextBox;

                    if (lblError != null)
                    {
                        lblError.Text = string.Empty;
                    }
                    if (clsValidaciones.GetKeyOrAdd("ValidaPaxFechaInf", "True").ToUpper().Equals("TRUE"))
                    {
                        if (txtTipoPasajero1.Text.ToUpper().Contains("INF"))
                        {
                            if (txtYear != null)
                            {
                                bValidaTemp = clsValidaciones.bValidaFechas(txtYear, txtMes, txtDia);
                                if (!bValidaTemp)
                                {
                                    bValida = bValidaTemp;
                                    if (lblError != null)
                                    {
                                        lblError.Text = "Debes completar todos los campos de fechas para Infantes";
                                        return bValida;
                                    }
                                }
                            }
                            else
                            {
                                if (txtEdad1 != null)
                                {
                                    if (txtEdad1.Text.Length.Equals(0))
                                    {
                                        bValida = false;
                                        if (lblError != null)
                                        {
                                            lblError.Text = "Debes completar todos los campos de fecha de nacimiento para infantes";
                                            return bValida;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (clsValidaciones.GetKeyOrAdd("ValidaPaxFechaGen", "False").ToUpper().Equals("TRUE"))
                            {
                                if (txtYear != null)
                                {
                                    bValidaTemp = clsValidaciones.bValidaFechas(txtYear, txtMes, txtDia);
                                    if (!bValidaTemp)
                                    {
                                        bValida = bValidaTemp;
                                        if (lblError != null)
                                        {
                                            lblError.Text = "Debes completar todos los campos de fechas para todos los pasajeros";
                                            return bValida;
                                        }
                                    }
                                }
                                else
                                {
                                    if (txtEdad1 != null)
                                    {
                                        if (txtEdad1.Text.Length.Equals(0))
                                        {
                                            bValida = false;
                                            if (lblError != null)
                                            {
                                                lblError.Text = "Debes completar todos los campos de fecha de nacimiento";
                                                return bValida;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (txtNombre1.Text.Length.Equals(0))
                    {
                        bValida = false;
                        if (lblError != null)
                        {
                            lblError.Text = "Debes completar todos los campos de nombres y apellidos";
                            return bValida;
                        }
                    }
                    if (txtApellido1.Text.Length.Equals(0))
                    {
                        bValida = false;
                        if (lblError != null)
                        {
                            lblError.Text = "Debes completar todos los campos de nombres y apellidos";
                            return bValida;
                        }
                    }
                    if (clsValidaciones.GetKeyOrAdd("bValidaDocGen", "False").ToUpper().Equals("TRUE"))
                    {
                        if (txtDocumento1 != null)
                        {
                            if (txtDocumento1.Text.Length.Equals(0))
                            {
                                bValida = false;
                                if (lblError != null)
                                {
                                    lblError.Text = "Debes completar todos los campos de documentos";
                                    return bValida;
                                }
                            }
                        }
                        if (txtDocumento != null)
                        {
                            if (txtDocumento.Text.Length.Equals(0))
                            {
                                bValida = false;
                                if (lblError != null)
                                {
                                    lblError.Text = "Debes completar todos los campos de documentos";
                                    return bValida;
                                }
                            }
                        }
                    }
                    if (txtTipoPasajero1 != null)
                    {
                        if (txtEdad1 != null)
                        {
                            if (!txtEdad1.Text.Length.Equals(0))
                            {
                                string sFechaNac = clsValidaciones.ConverFecha(txtEdad1.Text, sFormatoFecha, sFormatoFechaBD);
                                int iYear = clsValidaciones.CalculoYear(sFechaNac);
                                if (txtTipoPasajero1.Text.ToUpper().Contains("AD"))
                                {
                                    if (clsValidaciones.GetKeyOrAdd("ValidaEdadPas", "TRUE").ToUpper().Equals("FALSE"))
                                    {
                                        if (!(iYear > iYearMinADT && iYear <= iYearMaxADT))
                                        {
                                            bValida = false;
                                            if (lblError != null)
                                            {
                                                lblError.Text = "Fecha de nacimiento no corresponde, adulto debe ser mayor a 12 años";
                                                return bValida;
                                            }
                                        }
                                    }
                                }
                                if (txtTipoPasajero1.Text.ToUpper().Contains("NI"))
                                {
                                    if (!(iYear >= iYearMinCNN && iYear <= iYearMaxCNN))
                                    {
                                        bValida = false;
                                        if (lblError != null)
                                        {
                                            lblError.Text = "Fecha de nacimiento no corresponde, niños entre 2 y 12 años";
                                            return bValida;
                                        }
                                    }
                                }
                                if (txtTipoPasajero1.Text.ToUpper().Contains("INF"))
                                {
                                    if (!(iYear >= iYearMinINF && iYear <= iYearMaxINF))
                                    {
                                        bValida = false;
                                        if (lblError != null)
                                        {
                                            lblError.Text = "Fecha de nacimiento no corresponde, infantes entre 0 y 2 años";
                                            return bValida;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { bValida = false; }
            ExceptionHandled.Publicar("*************************////////////////////////////-- FIN DE LA VALIDACION DE FECHAS DE VUELOS & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");
            return bValida;
        }
        public bool bValidaDoc(UserControl PageSource)
        {
            bool bValida = true;
            try
            {
                Label lblError = PageSource.FindControl("lblError") as Label;

                DataList dtlPasajeros = PageSource.FindControl("dtlPasajeros") as DataList;
                for (int d = 0; d < dtlPasajeros.Items.Count; d++)
                {
                    TextBox txtDocumento1 = dtlPasajeros.Items[d].FindControl("txtDocumento1") as TextBox;
                    TextBox txtDocumento = dtlPasajeros.Items[d].FindControl("txtDocumento") as TextBox;
                    if (lblError != null)
                    {
                        lblError.Text = string.Empty;
                    }
                    if (txtDocumento1 != null)
                    {
                        if (txtDocumento1.Text.Length.Equals(0))
                        {
                            bValida = false;
                            if (lblError != null)
                            {
                                lblError.Text = "Debe Ingresar el numero de documento";
                                return bValida;
                            }
                        }
                    }
                    if (txtDocumento != null)
                    {
                        if (txtDocumento.Text.Length.Equals(0))
                        {
                            bValida = false;
                            if (lblError != null)
                            {
                                lblError.Text = "Debe completar todos los campos de documentos";
                                return bValida;
                            }
                        }
                    }
                }
            }
            catch { bValida = false; }
            return bValida;
        }
        /// <summary>
        /// Metodo que valida que todas las listas de la reserva de vuelos esten seleccionadas
        /// </summary>
        /// <param name="PageSource">UserControl</param>
        /// <returns>Indicador de seleccion</returns>
        ///   /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          26-04-2012
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        public bool bValidaListas(UserControl PageSource)
        {
            ExceptionHandled.Publicar("*************************////////////////////////////-- INICIO DE LA VALIDACION DE LISTA DE PASAJEROS DE VUELOS & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");
            bool bValida = true;
            try
            {
                if (clsValidaciones.GetKeyOrAdd("ValidaListasReservaVuelos", "False").ToUpper().Equals("TRUE"))
                {
                    Label lblError = PageSource.FindControl("lblError") as Label;

                    DataList dtlPasajeros = PageSource.FindControl("dtlPasajeros") as DataList;
                    for (int d = 0; d < dtlPasajeros.Items.Count; d++)
                    {
                        DropDownList ddlTipoDoc = dtlPasajeros.Items[d].FindControl("ddlTipoDoc") as DropDownList;
                        DropDownList ddlGenero = dtlPasajeros.Items[d].FindControl("ddlGenero") as DropDownList;
                        DropDownList ddlTrato1 = dtlPasajeros.Items[d].FindControl("ddlTrato1") as DropDownList;
                        if (lblError != null)
                        {
                            lblError.Text = string.Empty;
                        }
                        if (ddlTipoDoc != null)
                        {
                            if (ddlTipoDoc.SelectedValue.Length.Equals(0) && ddlTipoDoc.SelectedValue != "0")
                            {
                                bValida = false;
                                if (lblError != null)
                                {
                                    lblError.Text = "Debe seleccionar el tipo de documento de todos los pasajeros";
                                    return bValida;
                                }
                            }
                        }
                        if (ddlGenero != null)
                        {
                            if (ddlGenero.SelectedValue.Length.Equals(0) && ddlGenero.SelectedValue != "0")
                            {
                                bValida = false;
                                if (lblError != null)
                                {
                                    lblError.Text = "Debe seleccionar el genero de todos los pasajeros";
                                    return bValida;
                                }
                            }
                        }
                        if (ddlTrato1 != null)
                        {
                            if (ddlTrato1.SelectedValue.Length.Equals(0) && ddlTrato1.SelectedValue != "0")
                            {
                                bValida = false;
                                if (lblError != null)
                                {
                                    lblError.Text = "Debe seleccionar el trato para todos los pasajeros";
                                    return bValida;
                                }
                            }
                        }
                    }
                }
            }
            catch { bValida = false; }
            ExceptionHandled.Publicar("*************************////////////////////////////-- FIN DE LA VALIDACION DE LISTA DE PASAJEROS VUELOS & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");
            return bValida;
        }
        /// <summary>
        /// Metodo qu econfirma la reserva de vuelos
        /// </summary>
        /// <param name="sender">Objeto boton</param>
        /// <param name="e">Evento</param>
        /// <param name="PageSource">UserControl</param>
        /// <remarks>
        /// Autor:          
        /// Company:        Ssoft Colombia
        /// Fecha:          
        /// -------------------
        /// Control de Cambios
        /// ------------------- 
        /// Autor:          Juan Camilo Diaz      
        /// Fecha:          2012-04-26
        /// Descripcion:    Se incluye la validacion para la seleccion de las listas en los pasajeros de la reserva de vuelos (Aviamarketing)
        /// Autor:          Jeison vargas      
        /// Fecha:          2012-07-17
        /// Descripcion:    Se incluye el campo txtcodigoascesor para validar el campo de codigoasesor
        /// </remarks>

        private void setConfirmaReservaVuelo(object sender, EventArgs e, UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);
            string sUrlFinal = "";
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            HiddenField hRecord = (HiddenField)PageSource.FindControl("strRecord");

            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {

                string sUrl = "CarroCompras.aspx?RECORD=";

                if (clsValidaciones.GetKeyOrAdd("ReservaTarjeta", "FALSE").Trim().ToUpper().Equals("TRUE"))
                {
                    sUrl = "";
                }


                bool bEntraRes = true;
                string[] sValue = csValue(PageSource);
                string intIdItinerario = sValue[0];
                TextBox txtcodigoascesor = (TextBox)PageSource.FindControl("txtcodigoascesor");

                if (txtcodigoascesor != null)
                {
                    if (txtcodigoascesor.Text != "" && txtcodigoascesor.Text != null)
                    {
                        HttpContext.Current.Session["codigoascesor"] = txtcodigoascesor.Text;
                    }

                }
                HtmlGenericControl dPanel = PageSource.FindControl("dPanel") as HtmlGenericControl;
                HtmlGenericControl dContenido = PageSource.FindControl("dContenido") as HtmlGenericControl;
                clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
                DataTable dtItinerario = new DataTable();

                Negocios_WebService_OTA_AirLowFareSearch cWebServices = new Negocios_WebService_OTA_AirLowFareSearch();
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();

                if (vo_OTA_AirLowFareSearchLLSRQ.BHoras)
                {
                    DataSet dsDataRes = clsSesiones.GetDatasetSelectSabreAir();
                    VO_OTA_AirBookRQ vo_OTA_AirBookRQ = clsSesiones.getParametrosAirHoras();
                }
                else
                {
                    try
                    {
                        string sWhere = "SequenceNumber = " + intIdItinerario;
                        DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
                        DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
                        DataTable dtTablaRetornada = clsDataNet.dsDataWhere(sWhere, dtPricedItinerary);
                        intIdItinerario = dtTablaRetornada.Rows[0]["PricedItinerary_Id"].ToString();
                    }
                    catch { }
                    try
                    {
                        dtItinerario = new clsVuelos().GetDtGetItinerario(intIdItinerario);
                    }
                    catch { }
                }
                if (bEntraRes)
                {
                    try
                    {

                        if (cCache.CodPlan != null && !cCache.CodPlan.ToString().Length.Equals(0))
                        {
                            string sCommand = "Oferta aplicada No " + cCache.CodPlan;
                            Negocios_WebServiceRemark._ADD(Enum_TipoRemark.Historico, sCommand);
                        }
                    }
                    catch { }
                    ExceptionHandled.Publicar("*************************////////////////////////////-- INICIO DE LA CONFIRMACION DE LA RESERVA DE VUELOS & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");

                    cResultados = Confirmar_Reserva(sender, e, PageSource);
                    string sRecord = string.Empty;
                    try
                    {
                        sRecord = cResultados.strResultados;
                    }
                    catch { }
                    ExceptionHandled.Publicar("*************************////////////////////////////-- FIN DE LA CONFIRMACION DE LA RESERVA DE VUELOS & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");
                    if (cResultados.Error.Id == 1)
                    {
                        sRecord = cResultados.strResultados;
                        if (!String.IsNullOrEmpty(sRecord) && sRecord.Length.Equals(6))
                        {
                            if (clsValidaciones.GetKeyOrAdd("ReservaTarjeta", "FALSE").Trim().ToUpper().Equals("FALSE"))
                            {
                                sUrl += sRecord + "&ITIID=" + intIdItinerario;
                                sUrl += "&TipoPlan=" + clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
                            }
                            try
                            {
                                HttpContext.Current.Session["sRecord"] = sRecord;
                                Label lblTextoConfirm = (Label)PageSource.FindControl("lblTextoConfirm");
                                if (lblTextoConfirm != null)
                                    lblTextoConfirm.Text = clsValidaciones.GetKeyOrAdd("sTextoConfirmacionReserva", "Tu reserva ha sido confirmada bajo el record");

                                Label lblRecord = (Label)PageSource.FindControl("lblRecord");
                                if (lblRecord != null)
                                    lblRecord.Text = sRecord;

                                if (hRecord != null)
                                    hRecord.Value = sRecord;                           

                            }
                            catch { }
                            #region Datos de tarjeta
                            ExceptionHandled.Publicar("*************************////////////////////////////-- INICIO DE LA INSERCION DE DATOS DE TARJETAS VUELOS RECORD: " + sRecord + " & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");
                            RadioButtonList rblFormasPago = (RadioButtonList)PageSource.FindControl("rblFormasPago");
                            clsParametros cParam = new clsParametros();
                            if (rblFormasPago != null)
                            {
                                ExceptionHandled.Publicar(" Reserva " + sRecord + " forma de pago:" + rblFormasPago.SelectedValue);
                                string sTC = clsValidaciones.GetKeyOrAdd("TarjetaCredito", "TC");
                                string sEfec = clsValidaciones.GetKeyOrAdd("Efectivo", "EFE");
                                string sPSE = clsValidaciones.GetKeyOrAdd("PSE", "PSE");
                                string sTCPOL = clsValidaciones.GetKeyOrAdd("TarjetaCreditoPOL", "TCPOL");

                                int iValor = 0;
                                try
                                {
                                    Repeater rptItinerario = (Repeater)PageSource.FindControl("rptItinerario");
                                    if (rptItinerario != null && rptItinerario.Items.Count > 0)
                                    {
                                        Label lblPrecioTotal = (Label)rptItinerario.Items[0].FindControl("lblPrecioTotal");
                                        if (lblPrecioTotal != null)
                                            iValor = Convert.ToInt32(lblPrecioTotal.Text.Trim());
                                    }
                                }
                                catch { }

                                try
                                {
                                    new Reserva.csReserva().setTotalReserva(PageSource);
                                }
                                catch { }

                                if (rblFormasPago.SelectedValue.Equals(sTC))
                                {
                                    if (rblFormasPago.SelectedValue.Equals(sTC))
                                    {                                  

                                        ExceptionHandled.Publicar("---------------------setInsertarFormaPago-----------------------");
                                        new csUtilitarios().setCorreos(sRecord, rblFormasPago.SelectedItem.Value, "RSABRE");
                                        Reserva.csReserva cRes = new Reserva.csReserva();
                                        cRes.setInsertarFormaPago(sTC, PageSource, "air", sRecord);                                        
                                        cRes.setPagoPacificard(PageSource);                                

                                        //string sPagRedir = new Reserva.csReserva().ObtenerUrlAgradecimiento(cCache, rblFormasPago.SelectedValue, iValor,
                                        //   clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR"), rblFormasPago.SelectedItem.Text);

                                        csCarrito cCar = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
                                        cCar.LimpiarCarrito();
                                        Negocios_WebServiceSession._CerrarSesion();                                      
                                       
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if (!sRecord.Length.Equals(0))
                                            {
                                                cResultados.Error.ViewMessage[0] += ". Reserva " + sRecord;

                                            }
                                            objErrorMensaje.getError(cResultados.Error, dPanel);
                                            Negocios_WebServiceSession._CerrarSesion();
                                            ExceptionHandled.Publicar(cResultados.Error);
                                            dContenido.Visible = false;
                                        }
                                        catch { }
                                    }
                                }
                                else if (rblFormasPago.SelectedValue.Equals(sEfec))
                                {
                                    try
                                    {
                                        new Reserva.csReserva().setInsertarFormaPago(rblFormasPago.SelectedItem.Value, PageSource, "air", sRecord);
                                        new csUtilitarios().setCorreos(sRecord, rblFormasPago.SelectedItem.Value, "RSABRE");
                                        //string sPagRedir = new Reserva.csReserva().ObtenerUrlAgradecimiento(cCache, rblFormasPago.SelectedValue, iValor,
                                        //  clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR"), rblFormasPago.SelectedItem.Text);
                                        //if (sPagRedir.Length > 0)
                                        //{
                                        //    sUrlFinal = sPagRedir;
                                        //}
                                        //else
                                        //{
                                        //    ExceptionHandled.Publicar("No se encontro la pagina de agradecimiento, se redireccionara al carro de compras, Record: " + sRecord);

                                        //}
                                    }
                                    catch { }
                                   
                                    ExceptionHandled.Publicar("La reserva no fue pagada con TC record: " + sRecord);

                                }
                                else if (rblFormasPago.SelectedValue.Equals(sPSE))
                                {
                                    try
                                    {
                                        new Reserva.csReserva().EnviarValoresCompleto(rblFormasPago.SelectedValue.ToString(), PageSource, "air", rblFormasPago.SelectedItem.Text);
                                    }
                                    catch { }

                                }
                                else if (rblFormasPago.SelectedValue.Equals(sTCPOL))
                                {
                                    try
                                    {
                                        new Reserva.csReserva().setTotalReserva(PageSource);
                                    }
                                    catch { }
                                    try
                                    {
                                        ExceptionHandled.Publicar("---------------------setInsertarFormaPagos-----------------------");
                                        new Reserva.csReserva().setInsertarFormaPago(sTCPOL, PageSource, "air", sRecord);
                                        new csUtilitarios().setCorreos(sRecord, rblFormasPago.SelectedItem.Value, "RSABRE");
                                    }
                                    catch
                                    {
                                        ExceptionHandled.Publicar("ERROR METODO setInsertarFormaPago");
                                    }
                                    string sResul = new Reserva.csReserva().setSolicitarAutorizacionPOLWS(PageSource, cCache, sRecord);
                                    if (sResul == "")
                                    {
                                        ExceptionHandled.Publicar("No se pudo proicesar la transaccion de POL WS, Record: " + sRecord);
                                    }

                                    string sPagRedir = new Reserva.csReserva().ObtenerUrlAgradecimiento(cCache, rblFormasPago.SelectedValue,
                                           rblFormasPago.SelectedItem.Text, sResul, clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP"));

                                    if (sPagRedir.Length > 0)
                                    {
                                        Label lblUrlRedireccionV = (Label)PageSource.FindControl("lblUrlRedireccion");
                                        if (lblUrlRedireccionV != null)
                                            lblUrlRedireccionV.Text = sPagRedir;
                                    }
                                }
                            }
                            else
                            {
                                ExceptionHandled.Publicar("No existe el RadioButton de medios de pago, se redireccionara al carro de compras, Record: " + sRecord);
                                //sUrlFinal = sUrl;
                                //clsValidaciones.RedirectPaginaSesion(sUrl, true);
                            }

                            #endregion
                        }
                        else
                        {
                            Negocios_WebServiceSession._CerrarSesion();
                            Utils.clsSesiones.CLEAR_SESSION_AIR();
                            objErrorMensaje.getError(cResultados.Error, dPanel);
                            dContenido.Visible = false;
                        }
                    }
                    else if (cResultados.Error.Id == 0)
                    {
                        if (!sRecord.Length.Equals(0))
                        {
                            cResultados.Error.ViewMessage[0] += ". Reserva " + sRecord;
                        }
                        objErrorMensaje.getError(cResultados.Error, dPanel);
                        Negocios_WebServiceSession._CerrarSesion();
                        ExceptionHandled.Publicar(cResultados.Error);
                        dContenido.Visible = false;
                    }
                }
            }
            else
            {
                csGeneralsPag.FinSesion();
            }
            csCarrito cCarrito = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
            cCarrito.LimpiarCarrito();

        }
        private clsResultados getCerrarReserva(clsCache cCache, String intItinerary_Id)
        {
            csReservas cReserva = new csReservas();
            clsResultados objResultados = new clsResultados();
            clsParametros objParametros = new clsParametros();

            string sReserva = string.Empty;
            Negocios_WebService_OTA_AirLowFareSearch objNegocios_WebService_OTA_AirLowFareSearch = new Negocios_WebService_OTA_AirLowFareSearch();

            try
            {
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                string sOrigen = string.Empty;
                string sDestino = string.Empty;
                string sFechaIni = string.Empty;
                bool bHoras = false;
                try
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ != null)
                    {
                        sOrigen = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo.ToString();
                        sDestino = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoDestino.SCodigo.ToString();
                        sFechaIni = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaSalida.ToString();
                        bHoras = vo_OTA_AirLowFareSearchLLSRQ.BHoras;
                    }
                }
                catch { }
                Negocios_WebServiceSabreCommand.setPQ();
                /*SEGMENTO FUTURO*/
                ExceptionHandled.Publicar(Negocios_WebServiceSabreCommand._EjecutarComando("*A"));
                objParametros = setCerrar();
                if (!objParametros.Id.Equals(0))
                {
                    sReserva = objParametros.DatoAdic;
                    try
                    {
                        Negocios_WebServiceRemark._ADDRemarkSsoft(sReserva);
                    }
                    catch { }
                    try
                    {
                        WS_SsoftSabre.General.clsRulesFromPrice obj_RulesFromPrice = new WS_SsoftSabre.General.clsRulesFromPrice();
                        obj_RulesFromPrice.StrSesion = AutenticacionSabre.GET_SabreSession();
                        obj_RulesFromPrice.getRulesSegment();
                    }
                    catch
                    {
                    }

                    Utils.clsSesiones.SET_RECORD(objParametros.DatoAdic);
                    cReserva.Conexion = ConfigurationManager.AppSettings["strConexion"].ToString();
                   
                    /*GUARDAMOS LOS DATOS DE LA RESERVA*/
                    objParametros = setGuardarDatos(objParametros.DatoAdic, cCache, intItinerary_Id, vo_OTA_AirLowFareSearchLLSRQ);
                }
                else
                {
                    objParametros.Tipo = clsTipoError.Library;
                    objParametros.Severity = clsSeveridad.Moderada;
                    objParametros.Sugerencia.Add("Por favor intente de nuevo, con otras opciones de busqueda");
                    objParametros.ViewMessage.Add("La reserva no se confirmo");
                }
                Negocios_WebServiceSabreCommand.setQP(sReserva);            
                objResultados.strResultados = sReserva;
            }
            catch (Exception Ex)
            {
                Negocios_WebServiceSabreCommand.setQP(sReserva);
                setCerrarSesion();
                Utils.clsSesiones.CLEAR_SESSION_AIR();
                objParametros.Id = 0;
                objParametros.Ex = Ex;
                if (!sReserva.Length.Equals(0))
                {
                    objParametros.ViewMessage.Add(sReserva);
                }
                else
                {
                    objParametros.ViewMessage.Add("La reserva no se confirmo");
                }
                objParametros.Sugerencia.Add("Por favor intente de nuevo, con otras opciones de busqueda");
                objParametros.Tipo = clsTipoError.Library;
                objParametros.Severity = clsSeveridad.Moderada;
                objResultados.strResultados = null;
                ExceptionHandled.Publicar(Ex);
            }
            /*AGREGAMOS LOS PARAMETROS AL OBJETO DE RESULTADOS*/
            objResultados.Error = objParametros;

            return objResultados;
        }
        public clsParametros setGuardarDatos(string sRecord, clsCache cCache, String intItinerary_Id,VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
            /*GUARDAMOS LA RESERVA EN LA BASE DE DATOS */
            clsParametros objParametros = new clsParametros();
            string strError = String.Empty;
            try
            {
                csReservas cReserva = new csReservas();
                DataSet dsData = cReserva.CrearTablaReserva();
                DataTable tblReserva = dsData.Tables["tblReserva"];
                string sFechaInicio = String.Empty;
                string sFechaFin = String.Empty;

                /*GUARDAMOS LA RESERVA Y LA ENVIAMOS AL CARRITO DE COMPRAS*/
                objParametros = EnviarACarrito(sRecord, intItinerary_Id, vo_OTA_AirLowFareSearchLLSRQ);

                if (objParametros.Id.Equals(0))
                {
                    try
                    {
                        if (objParametros.Code.Equals("0"))
                            strError = objParametros.ViewMessage[0] + ". " + objParametros.Sugerencia[0];
                        else
                            strError = objParametros.ViewMessage[0] + sRecord + ". " + objParametros.Sugerencia[0];
                    }
                    catch { }
                    ExceptionHandled.Publicar(objParametros);
                }
            }
            catch (Exception Ex)
            {
                objParametros.Id = 0;
                objParametros.Sugerencia.Add("Vuelva a intentarlo");
                objParametros.StackTrace = Ex.StackTrace;
                objParametros.Tipo = clsTipoError.Library;
                objParametros.DatoAdic = Ex.Message;
                objParametros.DatoAdicArr.Add(Ex.HelpLink);
                strError = "La reserva no pudo ser confirmada. el record de la reserva es: " + sRecord;
                objParametros.ViewMessage.Add(strError);
                objParametros.Ex = Ex;
                ExceptionHandled.Publicar(objParametros);
            }
            return objParametros;
        }
        private void setCerrarSesion()
        {
            Negocios_WebServiceSession._CerrarSesion();
        }
        /*-------------------CARRITO_COMPRAS---------------------------*/
        /// <summary>
        /// Metodo que envia los valores a carrito y guarda la reserva en BD
        /// </summary>
        /// <param name="sRecord">Record de reserva</param>
        /// <param name="intItinerary_Id">Id Itinerario</param>
        /// <returns>Clase de parametros</returns>
        /// <remarks>
        /// Autor:          
        /// Company:        Ssoft Colombia
        /// Fecha:          
        /// -------------------
        /// Control de Cambios
        /// -------------------        
        /// Autor:          Juan Camilo Diaz 
        /// Fecha:          2012-03-08
        /// Descripcion:    Se modifica la redireccion para validacion del calculo de bonos
        /// </remarks>
        public clsParametros EnviarACarrito(String sRecord, string intItinerary_Id, VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
            clsParametros objMensajes = new clsParametros();
            const string strNombreCarroCompras = "CarritoCompras";
            string strConexion = clsSesiones.getConexion();
            string sOrigen = string.Empty;
            string sDestino = string.Empty;
            string sIdioma = clsSesiones.getIdioma();

            if (sIdioma.Equals(""))
                sIdioma = clsValidaciones.GetKeyOrAdd("sIdioma", "es");


            //Se toma la informacion de origen y destino del session y los paramteros incialmente indicados en la busqueda
            //hceron
            if (vo_OTA_AirLowFareSearchLLSRQ != null)
            {
                sOrigen = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo.ToString();
                sDestino = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoDestino.SCodigo.ToString();
               
            }


            try
            {
                int iSegmentoManual = 1;
                clsCache cCache = new csCache().cCache();
                csCarrito objCarritoCompras = new csCarrito("Reserva" + cCache.SessionID, strNombreCarroCompras);
               
                DataTable tblBeneficios = null;
                try
                {
                    tblBeneficios = clsSesiones.GetTablaBeneficios();
                }
                catch (Exception) { }

                if (tblBeneficios != null && tblBeneficios.Rows.Count != 0)
                {
                    objCarritoCompras.IntConvenio = tblBeneficios.Rows[0]["ID_BENEFICIO"].ToString();
                }
               
                //tblRefere objtblRefere = new tblRefere();
                //objtblRefere.Conexion = strConexion;
                string sFechaIniCarrito = string.Empty;
                string sFechaFinCarrito = string.Empty;
                /*CODIGO DEL PLAN*/
                String strCodigoPlan = "0";
                string strDuraciuonVuelo = "0:00:00";
                String strTipoPlan = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
                DataTable dtItinerario = new DataTable();
                try
                {
                    dtItinerario = new clsVuelos().GetDtGetItinerario(intItinerary_Id);
                    if (!dtItinerario.Rows[0]["Ws"].ToString().Length.Equals(0))
                    {
                        strTipoPlan = dtItinerario.Rows[0]["Ws"].ToString();
                       
                    }
                }
                catch { }

                try
                {
                   strCodigoPlan = new CsConsultasVuelos().ConsultaCodigo(strTipoPlan,"TBLTPOSERVICIO","INTID","STRCODIGO");
                }
                catch (Exception)
                {
                    strCodigoPlan = "0";
                }

                /*TIPO PLAN*/
                String idRefereTipoPlan = strCodigoPlan;
                /*ESTADO SOLICITADA*/
                String int_Id_EstadoSolicitada = new CsConsultasVuelos().ConsultaCodigo(clsValidaciones.GetKeyOrAdd("EstadoReservaInicialAereo", "HK"),"TBLESTADOS_RESERVA","INTCODE","STRCODE");
                if (int_Id_EstadoSolicitada == null)
                    int_Id_EstadoSolicitada = "1";
                /*MONEDA*/
         
                String strTipoMoneda = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion;
                String id_Refere_Tipo_Moneda = new CsConsultasVuelos().ConsultaCodigo(strTipoMoneda,"TBLMONEDAS","INTCODE","STRCODE");
                if (id_Refere_Tipo_Moneda == null)
                    id_Refere_Tipo_Moneda = "1";
                /*TABLA DE SEGMENTOS*/
                DataTable dtSegmento = new DataTable();

                DataTable dtPasajeros = new DataTable();

                /*TABLA DE TIPOS PASAJEROS*/
                DataTable dtPassengerTypeQuantity = new DataTable();
                string sVuelosXhora = clsValidaciones.GetKeyOrAdd("sVueloXhoras", "False");
                if (sVuelosXhora.ToUpper().Equals("TRUE"))
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ.BHoras)
                    {
                        dtSegmento = new clsVuelos().GetDtFlightSegmentoHorasCotiza();
                        dtPasajeros = new clsVuelos().GetDtPasajeros();
                        dtPassengerTypeQuantity = new clsVuelos().GetDtPassengerTypeQuantity();
                    }
                    else
                    {
                        dtSegmento = new clsVuelos().GetDtFlightSegmento(intItinerary_Id.ToString());
                        dtPasajeros = new clsVuelos().GetDtPasajeros();
                        dtPassengerTypeQuantity = new clsVuelos().GetDtPassengerTypeQuantity(intItinerary_Id);
                    }
                }
                else
                {
                    dtSegmento = new clsVuelos().GetDtFlightSegmento(intItinerary_Id.ToString());
                    dtPasajeros = new clsVuelos().GetDtPasajeros();
                    dtPassengerTypeQuantity = new clsVuelos().GetDtPassengerTypeQuantity(intItinerary_Id);
                }
                try
                {
                }
                catch { }
                /*TABLA DE PASAJEROS*/
                /*NUMERO PASAJEROS*/
                Int32 intNumeroPasajeros = dtPasajeros.Rows.Count;
                /*GUARDAMOS LOS DATOS BASICOS DE LOS SEGMENTOS*/
               

                foreach (DataRow drFilaSegmento in dtSegmento.Rows)
                {
                    String strAnio = string.Empty;
                    String strMes = string.Empty;
                    String strDia = string.Empty;
                    /*RECORD*/
                    objCarritoCompras.StrDuracion = strDuraciuonVuelo;
                    objCarritoCompras.StrCodigoReserva = sRecord;
                    objCarritoCompras.StrConfirmacion = sRecord;
                    objCarritoCompras.IntCodigoPlan = strCodigoPlan;
                    /*FECHA LIMITE DE TIQUETEO*/
                    DateTime dtFecha_vencimiento = clsSesiones.GET_TICKETE();
                    System.Text.StringBuilder strFecha_Vencimiento = new System.Text.StringBuilder();
                    strFecha_Vencimiento.Append(dtFecha_vencimiento.Month + "/");
                    strFecha_Vencimiento.Append(dtFecha_vencimiento.Day + "/");
                    strFecha_Vencimiento.Append(dtFecha_vencimiento.Year + " ");
                    strFecha_Vencimiento.Append(dtFecha_vencimiento.Hour + ":");
                    strFecha_Vencimiento.Append(dtFecha_vencimiento.Minute);
                    objCarritoCompras.StrFechaVencimiento = strFecha_Vencimiento.ToString();
                    /*IDENTIFICADOR DEL PLAN*/
                    objCarritoCompras.StrIdentificadorDelPlan = strTipoPlan;
                    /*SEGMENTO*/
                    objCarritoCompras.IntCodigoTarifa = drFilaSegmento["FlightSegment_Id"].ToString();
                    /*NUMERO DEL VUELO*/
                    objCarritoCompras.StrNombrePlan = drFilaSegmento["strNombre_Aerolinea"].ToString();
                    objCarritoCompras.BolImpuestos = bool.Parse(dtItinerario.Rows[0]["bolImpuestos"].ToString());
                    clsSesiones.setImpuestos(objCarritoCompras.BolImpuestos);
                    try
                    {
                        cCache.Impuestos = objCarritoCompras.BolImpuestos;
                        clsCacheControl cCacheControl = new clsCacheControl();
                        cCacheControl.ActualizaXML(cCache);
                    }
                    catch { }
                    /*CANTIDAD PASAJEROS*/
                    objCarritoCompras.IntcantidadPersonas = intNumeroPasajeros.ToString();
                    objCarritoCompras.StrPasajeros = intNumeroPasajeros.ToString();
                    /*TIPO PLAN*/
                    objCarritoCompras.StrTipoPlan = strTipoPlan;
                    objCarritoCompras.IntTipoPlan = strCodigoPlan;
                    /*MONEDA*/
                    objCarritoCompras.StrTipoMoneda = strTipoMoneda;
                    objCarritoCompras.IntTipoMoneda = id_Refere_Tipo_Moneda.ToString();
                    /*TOTAL VALOR*/
                    Decimal Fee = 0;
                    /*Validamos si trae fee adicional de la agencia para sumarselo al valor total*/
                    for (int iPas = 0; iPas < dtPasajeros.Rows.Count; iPas++)
                    {
                        if (!dtPasajeros.Rows[iPas]["strFee"].ToString().Equals(""))
                        {
                            Fee += clsValidaciones.getDecimalNotRound(dtPasajeros.Rows[iPas]["strFee"].ToString());
                        }
                    }
                    objCarritoCompras.IntValorTotal = clsValidaciones.getDecimalNotRound((clsValidaciones.getDecimalNotRound(dtItinerario.Rows[0]["intTotalPesos"].ToString()) + Fee).ToString()).ToString();
                    if (!dtItinerario.Rows[0]["IntPrecioOferta"].ToString().Equals("0"))
                        objCarritoCompras.IntValorOferta = clsValidaciones.getDecimalNotRound(dtItinerario.Rows[0]["IntPrecioOferta"].ToString()).ToString();

                    if (tblBeneficios != null &&
                        tblBeneficios.Rows.Count != 0)
                    {
                        objCarritoCompras.IntValorTotal = tblBeneficios.Rows[0]["VALOR_TOTAL_PESOS"].ToString();
                    }
                   
                    objCarritoCompras.IntSegmento = iSegmentoManual.ToString();
                    /*ORIGEN Y DESTINO*/
                    objCarritoCompras.IntOrigen = drFilaSegmento["IntId_Aeropuerto_Salida"].ToString();
                    objCarritoCompras.IntDestino = drFilaSegmento["IntId_Aeropuerto_Llegada"].ToString();  
    
                    objCarritoCompras.StrOrigen = drFilaSegmento["strDepartureAirport"].ToString();
                    objCarritoCompras.StrDestino = drFilaSegmento["strArrivalAirport"].ToString();
                   
                    //Se mdoifica para que los valores sean tomados desde la bsuqueda
                    //Hceron 09082013
                    if (sOrigen != string.Empty && sDestino != string.Empty)
                    {
                        sOrigen=objCarritoCompras.StrOrigen;
                        sDestino=objCarritoCompras.StrDestino;

                        string iOrigen = new CsConsultasVuelos().ConsultaCodigo(sOrigen, "TBLIATA", "INTCODE", "STRCODE");
                        string iDestino = new CsConsultasVuelos().ConsultaCodigo(sDestino, "TBLIATA", "INTCODE", "STRCODE");
                        if (iOrigen != "" && iDestino != "")
                        {
                            objCarritoCompras.IntOrigen = iOrigen;
                            objCarritoCompras.IntDestino = iDestino;
                        }

                    }
                   
                    int iPosTemp = 1;
                    foreach (DataRow drFilaSegmentos in dtSegmento.Rows)
                    {
                        objCarritoCompras.StrCiudad += drFilaSegmentos["strCiudad_Salida"].ToString() + " - " + drFilaSegmentos["strCiudad_Llegada"].ToString();
                        if (!iPosTemp.Equals(dtSegmento.Rows.Count))
                        {
                            objCarritoCompras.StrCiudad += ", ";
                        }
                        iPosTemp++;
                    }
                    objCarritoCompras.StrAcomodacion = intNumeroPasajeros.ToString();
                    /*OBTENEMOS LA FECHA DE SALIDA*/
                    DateTime dtmFechaSalida = Convert.ToDateTime(drFilaSegmento["dtmFechaSalida"]);
                    strAnio = dtmFechaSalida.Year.ToString();
                    strMes = dtmFechaSalida.Month.ToString();
                    strDia = dtmFechaSalida.Day.ToString();
                    sFechaIniCarrito = strMes + "/" + strDia + "/" + strAnio;
                    objCarritoCompras.StrFechaInicial = sFechaIniCarrito;
                    /*OBTENEMOS LA FECHA DE LLEGADA*/
                    DateTime dtm_Fecha_LLegada = Convert.ToDateTime(drFilaSegmento["dtmFechaLlegada"]);
                    strAnio = dtm_Fecha_LLegada.Year.ToString();
                    strMes = dtm_Fecha_LLegada.Month.ToString();
                    strDia = dtm_Fecha_LLegada.Day.ToString();
                    sFechaFinCarrito = strMes + "/" + strDia + "/" + strAnio;
                    objCarritoCompras.StrFechaFinal = sFechaFinCarrito;
                    /*LA HORA*/
                    objCarritoCompras.StrHoraIni = Convert.ToDateTime(drFilaSegmento["dtmFechaSalida"]).ToString("HH:mm:ss");
                    objCarritoCompras.StrHoraFin = Convert.ToDateTime(drFilaSegmento["dtmFechaLlegada"]).ToString("HH:mm:ss");
                    /*ID AEROLINEA*/
                    objCarritoCompras.IntProveedor = drFilaSegmento["intId_Aerolinea"].ToString();
                    /*ESTADO SOLICITADA*/
                    objCarritoCompras.IntEstado = "1";
                    /*CODIGO DEL LA AEROLINEA*/
                    objCarritoCompras.StrObservacion = drFilaSegmento["strMarketingAirline"].ToString();
                    String strTipoTrayecto = drFilaSegmento["strTipoTrayecto"].ToString().Substring(0, 1);
                    clsSesiones.setAerolineaValidadora(drFilaSegmento["strMarketingAirline"].ToString() + strTipoTrayecto);
                    objCarritoCompras.StrOperador = drFilaSegmento["FlightNumber"].ToString();
                    objCarritoCompras.StrCodigo = "0";
                    objCarritoCompras.StrDetalles = "";
                    objCarritoCompras.StrRestricciones = "";
                    objCarritoCompras.StrBeneFicios = "";
                    objCarritoCompras.StrEncuenta = "";
                    objCarritoCompras.StrZonaGeografica = "";
                    objCarritoCompras.AddFields();
                    iSegmentoManual++;
                }
             
                /*GUARDAMOS TIPO DE PASAJERO*/
                Int32 intIdTipoPax = default(Int32);
                foreach (DataRow drFilaTipoPax in dtPassengerTypeQuantity.Rows)
                {
                    DataTable dtFare = new clsVuelos().GetDtPassengerFare(drFilaTipoPax["PTC_FareBreakdown_Id"].ToString());
                    Decimal ImpuestoFee = 0;
                    Decimal dbl_total_Con_impuestos_tasas = 0;
                    Decimal dbl_total_Sin_impuestos_tasas = 0;
                    Decimal dbl_total_impuestos_tasas = 0;
                    if (!dtPasajeros.Rows[intIdTipoPax]["strFee"].ToString().Equals(""))
                    {
                        ImpuestoFee = clsValidaciones.getDecimalNotRound(dtPasajeros.Rows[intIdTipoPax]["strFee"].ToString());
                    }
                    if (ImpuestoFee != 0)
                    {
                        dbl_total_Con_impuestos_tasas = clsValidaciones.getDecimalNotRound(dtFare.Rows[0]["IntTotalTarifaConTAXPersona"].ToString()) + ImpuestoFee;
                        dbl_total_Sin_impuestos_tasas = clsValidaciones.getDecimalNotRound(dtFare.Rows[0]["intBaseFare"].ToString());
                        dbl_total_impuestos_tasas = clsValidaciones.getDecimalNotRound(dtFare.Rows[0]["intTotalImpuestosTasas"].ToString()) + ImpuestoFee;
                    }
                    else
                    {
                        dbl_total_Con_impuestos_tasas = clsValidaciones.getDecimalNotRound(dtFare.Rows[0]["IntTotalTarifaConTAXPersona"].ToString());
                        dbl_total_Sin_impuestos_tasas = clsValidaciones.getDecimalNotRound(dtFare.Rows[0]["intBaseFare"].ToString());
                        dbl_total_impuestos_tasas = clsValidaciones.getDecimalNotRound(dtFare.Rows[0]["intTotalImpuestosTasas"].ToString());
                    }
                    String str_Tipo_Pasajero = drFilaTipoPax["Code"].ToString();
                    string sDetallePax = str_Tipo_Pasajero;

                   

                    String id_Refere_Tipo_Pasajero = new CsConsultasVuelos().EjecutaProcedimiento("SPConsultaTipoPasajero",new string[2]{str_Tipo_Pasajero,sIdioma});

                    decimal dblbeneficio = default(decimal);

                    try
                    {
                        if (tblBeneficios != null && tblBeneficios.Rows.Count != 0)
                        {
                            for (int c = 0; c < tblBeneficios.Rows.Count; c++)
                            {
                                if (tblBeneficios.Rows[c]["CODETIPOPAX"].ToString().Equals(str_Tipo_Pasajero))
                                {
                                    dblbeneficio = clsValidaciones.getDecimalNotRound(tblBeneficios.Rows[c]["VALOR_BENEFICIO_TIPOPAX"].ToString());
                                }
                            }
                        }
                    }
                    catch (Exception) { }
                    objCarritoCompras.SaveTipoPax(str_Tipo_Pasajero, Convert.ToInt32(id_Refere_Tipo_Pasajero), clsValidaciones.getDecimalBD(dbl_total_Con_impuestos_tasas.ToString()).ToString(), clsValidaciones.getDecimalBD(dbl_total_Sin_impuestos_tasas.ToString()).ToString(), clsValidaciones.getDecimalBD(dbl_total_impuestos_tasas.ToString()).ToString(), dblbeneficio.ToString(), "0");
                    intIdTipoPax++;
                }
                /*GUARDAMOS CADA PASAJERO*/
                int iPosPax = 1;
                for (int i = 0; i < dtPasajeros.Rows.Count; i++)
                {
                    String strTipoPasajero = dtPasajeros.Rows[i]["strTipoPasajero"].ToString();
                    try
                    {
                        strTipoPasajero = dtPasajeros.Rows[i]["strCode"].ToString();
                    }
                    catch { }
                    string sDetallePax = strTipoPasajero;
                    String idRefereTipoPasajero = new CsConsultasVuelos().EjecutaProcedimiento("SPConsultaTipoPasajero",new string[2]{strTipoPasajero,sIdioma});

                    String str_Nombre_Completo = dtPasajeros.Rows[i]["strPrimerNombre"].ToString() + "/" + dtPasajeros.Rows[i]["strPrimerApellido"].ToString();
                    String str_Telefono = dtPasajeros.Rows[i]["strTelefono"].ToString();
                    String str_TipoDocumento = "0";
                    String str_PaxFrecuente = dtPasajeros.Rows[i]["strPasajeroFrecuente"].ToString();
                   
                    str_TipoDocumento = dtPasajeros.Rows[i]["strTipoDocumento"].ToString();
                    
                    String str_Documento = dtPasajeros.Rows[i]["strDocumento"].ToString();
                    string strFechaNac = null;
                    try
                    {
                        strFechaNac = clsValidaciones.getFechaSabre(dtPasajeros.Rows[i]["strFechaNacimiento"].ToString());
                        strFechaNac = clsValidaciones.ConverFecha(strFechaNac, "yyyy/MM/dd", clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd"));
                    }
                    catch { }
                    objCarritoCompras.SavePerson(str_Nombre_Completo, strFechaNac, strTipoPasajero, Convert.ToInt32(idRefereTipoPasajero), null, 0, str_Telefono);
                    objCarritoCompras.UpdatePerson(iPosPax, null, null, null, null, null, str_TipoDocumento, str_Documento, str_PaxFrecuente);
                    iPosPax++;
                }
                /*GUARDAMOS LAS TASAS DE CADA TIPO PASAJERO*/
                for (int I = 0; I < dtPassengerTypeQuantity.Rows.Count; I++)
                {
                    DataTable dtFare = new clsVuelos().GetDtPassengerFare(dtPassengerTypeQuantity.Rows[I]["PTC_FareBreakdown_Id"].ToString());
                    String str_Tipo_Pasajero = dtPassengerTypeQuantity.Rows[I]["Code"].ToString();
                    /*OBTENEMOS EL IDREFERE TIPO PAX*/
                    string sDetallePax = str_Tipo_Pasajero;
             
                   
                    String id_Refere_Tipo_Pasajero = new CsConsultasVuelos().EjecutaProcedimiento("SPConsultaTipoPasajero",new string[2]{str_Tipo_Pasajero,sIdioma});
                    if (id_Refere_Tipo_Pasajero == "") id_Refere_Tipo_Pasajero = "1";
                    for (int F = 0; F < dtFare.Rows.Count; F++)
                    {
                        DataTable dtFareTax = new clsVuelos().GetDtPassengerFareTax(dtFare.Rows[F]["PassengerFare_Id"].ToString());
                        DataTable dtFareTaxcopy = dtFareTax.Copy();
                        if (!dtPasajeros.Rows[I]["strFee"].ToString().Equals(""))
                        {
                            VO_Credentials vo_Credentials = csReferencias.csCredenciales(Enum_ProveedorWebServices.Sabre);

                            if (!vo_Credentials.PccPais.ToString().Equals(clsValidaciones.GetKeyOrAdd("PccPais")) /*vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString().Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano"))*/)
                            {
                                Decimal ValIva = clsValidaciones.getDecimalNotRound("1,16");
                                Decimal ValorSinIva = clsValidaciones.getDecimalNotRound((clsValidaciones.getDecimalNotRound(dtPasajeros.Rows[I]["strFee"].ToString()) / ValIva).ToString());
                                Decimal Iva = clsValidaciones.getDecimalNotRound((clsValidaciones.getDecimalNotRound(dtPasajeros.Rows[I]["strFee"].ToString()) - ValorSinIva).ToString());

                                DataRow drFilaTax = dtFareTaxcopy.NewRow();
                                drFilaTax["TaxCode"] = clsValidaciones.GetKeyOrAdd("FEE_Adicional", "ADFE");
                                drFilaTax["CurrencyCode"] = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString();
                                drFilaTax["DecimalPlaces"] = "0";
                                drFilaTax["Amount"] = ValorSinIva.ToString();
                                drFilaTax["Taxes_Id"] = "0";
                                drFilaTax["Tax_Amount_Usd"] = ValorSinIva.ToString();
                                drFilaTax["strNombre_Impuesto"] = "FeeAdicional";

                                DataRow drFilaImpuesto = dtFareTaxcopy.NewRow();
                                drFilaImpuesto["TaxCode"] = clsValidaciones.GetKeyOrAdd("IVA_FEE_Adicional", "IADFE");
                                drFilaImpuesto["CurrencyCode"] = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString();
                                drFilaImpuesto["DecimalPlaces"] = "0";
                                drFilaImpuesto["Amount"] = Iva.ToString();
                                drFilaImpuesto["Taxes_Id"] = "0";
                                drFilaImpuesto["Tax_Amount_Usd"] = Iva.ToString();
                                drFilaImpuesto["strNombre_Impuesto"] = "Iva Fee Adicional";

                                dtFareTaxcopy.Rows.Add(drFilaTax);
                                dtFareTaxcopy.Rows.Add(drFilaImpuesto);
                                dtFareTax.Clear();
                                dtFareTax = dtFareTaxcopy;
                            }
                            else
                            {
                                DataRow drFilaTax = dtFareTaxcopy.NewRow();
                                drFilaTax["TaxCode"] = clsValidaciones.GetKeyOrAdd("FEE_Adicional", "ADFE");
                                drFilaTax["CurrencyCode"] = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString();
                                drFilaTax["DecimalPlaces"] = "0";
                                drFilaTax["Amount"] = dtPasajeros.Rows[I]["strFee"].ToString();
                                drFilaTax["Taxes_Id"] = "0";
                                drFilaTax["Tax_Amount_Usd"] = dtPasajeros.Rows[I]["strFee"].ToString();
                                drFilaTax["strNombre_Impuesto"] = "FeeAdicional";
                                dtFareTaxcopy.Rows.Add(drFilaTax);
                                dtFareTax.Clear();
                                dtFareTax = dtFareTaxcopy;
                            }
                        }

                        foreach (DataRow drFilaTax in dtFareTax.Rows)
                        {
                            String strRefereTasa = drFilaTax["TaxCode"].ToString();
                            String intIdValorTax = drFilaTax["Amount"].ToString();
                            /*OBTENEMOS LOS DOS PRIMEROS CODIGOS DE LOS IMPUESTOS DIFERENTES DE EL TA Y ITA*/
                           
                            if (tblBeneficios != null && tblBeneficios.Rows.Count != 0)
                            {
                                if (strRefereTasa == clsValidaciones.GetKeyOrAdd("TASA_ADMINISTRATIVA", "TA"))
                                {
                                    intIdValorTax = tblBeneficios.Rows[0]["DESC_VALOR_TA"].ToString();
                                }
                                else if (strRefereTasa == clsValidaciones.GetKeyOrAdd("IVA_TA", "ITA"))
                                {
                                    intIdValorTax = tblBeneficios.Rows[0]["DESC_VALOR_IVATA"].ToString();
                                }
                            }
                            /*OBTENEMOS EL IDREFERE DE LA TASA*/
                           

                            String intIdCodigoTax = new CsConsultasVuelos().EjecutaProcedimiento("SPConsultaImpSabre",new string[1]{drFilaTax["TaxCode"].ToString()});
                            objCarritoCompras.AddTasa(intIdCodigoTax, "0", clsValidaciones.getDecimalBD(intIdValorTax), id_Refere_Tipo_Moneda, id_Refere_Tipo_Pasajero);
                        }
                    }
                }
                /*GUARDAMOS EN XML*/
                objCarritoCompras.Save();
                /*GUARDAMOS LOS DATOS GENERALES DEL PROYECTO*/
                GuardarDatosProyecto();
                csReservas csRes = new csReservas();
                csRes.Conexion = clsValidaciones.GetKeyOrAdd("strConexion");
                objMensajes = csRes.GuardaReservaGen(objCarritoCompras.GetDsReservas());
                /*GUARDAMOS EL SQL QUE GENERO*/
                ExceptionHandled.Publicar(objMensajes.Complemento);
                /*ACUTALIZAMO EL CODIGO DE PROYECTO*/
                if (clsSesiones.getProyecto() == "0")
                    clsSesiones.setProyecto(objMensajes.DatoAdicArr[0]);
                /*ACTUALIZAMOS EL CODIGO DEL INSERCION*/
                objCarritoCompras.Save_Update("1");
                clsSesiones.setPantalleRespuestaLogin("ConfirmacionVuelo.aspx?RECORD=" + sRecord);
                return objMensajes;

            }
            catch (Exception Ex)
            {
                clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
                clsParametros objParametros = new clsParametros();
                objParametros.ViewMessage.Add("No se pudo realizar la solicitud");
                objParametros.Sugerencia.Add("Ha ocurrido un error procesando su solicitud");
                objParametros.Tipo = clsTipoError.Library;
                ExceptionHandled.Publicar(Ex);
            }
            return objMensajes;
        }
        private void GuardarDatosProyecto()
        {
            /*FECHA LIMITE DE PAGO*/
            clsCache cCache = new csCache().cCache();

          
            const string strNombreCarroCompras = "CarritoCompras";
            csCarrito csCarCompras = new csCarrito("Reserva" + cCache.SessionID, strNombreCarroCompras);
            string idRecord = clsSesiones.getProyecto();
          
            string iEstadoReserva = "0";
            string iEstadoPago = "0";
            string iFormaPago = "0";

            string sEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoReservaInicialAereo", "HK");
            string sEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPagoInicial", "PP");
            string sFormaPago = clsValidaciones.GetKeyOrAdd("EstadoFormaPagoInicial", "EFE");
            string sTipoEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoReserva", "EstadoReserva");
            string sTipoEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPago", "EstadoPago");
            string sTipoFormaPago = clsValidaciones.GetKeyOrAdd("FormasPago", "FP");

            string sContacto = cCache.Contacto;
            string sCodCoordinador = cCache.Contacto;
            string sCoordinador = clsValidaciones.GetKeyOrAdd("Coordinador", "CE");

            if (cCache.Viajero != "0")
            {
                sContacto = cCache.Viajero;
            }
           

            //tblRefere otblRefere = new tblRefere();
            //otblRefere.Get(sTipoEstadoReserva, sEstadoReserva);
            //if (otblRefere.Respuesta)
            //    iEstadoReserva = otblRefere.intidRefere.Value;

            //otblRefere.Get(sTipoEstadoPago, sEstadoPago);
            //if (otblRefere.Respuesta)
            //    iEstadoPago = otblRefere.intidRefere.Value;

            //otblRefere.Get(sTipoFormaPago, sFormaPago);
            //if (otblRefere.Respuesta)
            //    iFormaPago = otblRefere.intidRefere.Value;

           
            csCarCompras.SaveDataProject(idRecord, sCodCoordinador, sContacto, "0", iEstadoReserva, iFormaPago, iEstadoPago);
        }
        private clsParametros setCerrar()
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                string sRecord = string.Empty;
                ExceptionHandled.Publicar(Negocios_WebServiceSabreCommand._EjecutarComando("*A"));
                cParametros = _CerrarReserva(ref sRecord);
            }
            catch
            {
            }
            return cParametros;
        }
        public clsParametros _CerrarReserva(ref String Record_)
        {
            #region [ CERRAR RESERVA ]
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            clsEndTransactionLLS cEndTrasaction = new clsEndTransactionLLS();
            VO_Credentials objvo_Credentials;

            string Return_ = "XXXXXX";
            string ReturnTotal_ = "YYYYYY";
            string NotComplete_ = "ZZZZZZ";
            clsParametros cParametros = new clsParametros();
            objvo_Credentials = clsSesiones.getCredentials();
            bool bReservaNormal = true;
            cParametros.DatoAdic = Record_;
            List<VO_SabreErrors> SabreErrors_ = WS_SsoftSabre.Utilidades.clsValidacionesVuelos._SabreErrors();
            int iCount = 0;
            while (!clsValidaciones.IS_ALPHABETIC(cParametros.DatoAdic))
            {
                cParametros = cEndTrasaction._GuardarReserva();
                

                if (cParametros.Message.Contains("Unable to connect"))
                {
                    if (iCount < 3)
                    {
                        cParametros.DatoAdic = Record_;
                        iCount++;
                    }
                    else
                    {
                        cParametros.DatoAdic = NotComplete_;
                    }
                }
                else
                {
                    if (cParametros.DatoAdic == null) cParametros.DatoAdic = String.Empty;

                    if (cParametros.DatoAdic.Length != 6 || !Utils.clsValidaciones.IS_ALPHABETIC(cParametros.DatoAdic))
                    {
                        for (int i = 0; i < SabreErrors_.Count; i++)
                        {
                            if (cParametros.DatoAdic.Trim().CompareTo(SabreErrors_[i].Error_) == 0)
                            {
                                #region [ FILTRAR ]

                                if (SabreErrors_[i].Solucion_[0].CompareTo("RETURN") == 0) cParametros.DatoAdic = Return_;
                                else if (SabreErrors_[i].Solucion_[0].CompareTo("RETURNCOMPLETE") == 0) cParametros.DatoAdic = ReturnTotal_;
                                else
                                {
                                    foreach (string Solucion_ in SabreErrors_[i].Solucion_)
                                    {
                                        if (SabreErrors_[i].Error_.Contains("NEED ADDRESS - USE W-"))
                                        {
                                            Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno(Solucion_ + objvo_Credentials.Agencia_Nombre);
                                        }
                                        else
                                        {
                                            if (SabreErrors_[i].Error_.Contains("NEED PHONE FIELD - USE 9"))
                                            {
                                                Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno(Solucion_ + objvo_Credentials.Agencia_Telefono);
                                            }
                                            else
                                            {
                                                if (SabreErrors_[i].Error_.Contains("INFANT DETAILS REQUIRED IN SSR - ENTER 3INFT/..."))
                                                {
                                                    Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno(Solucion_);
                                                    bReservaNormal = false;
                                                }
                                                else
                                                {
                                                    ///jvargas22062013
                                                    if (SabreErrors_[i].Error_.Contains("VERIFY ORDER OF ITINERARY SEGMENTS - MODIFY OR END TRANSACTION"))
                                                    {
                                                        if (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto.ToString().Trim().ToUpper().Contains("MULTI"))
                                                        {
                                                            try
                                                            {
                                                                cParametros = Negocios_WebServiceSabreCommand._EjecutarComandoGen(Solucion_);
                                                                bReservaNormal = false;
                                                                cParametros.DatoAdic = "";
                                                                cParametros.DatoAdic = cParametros.Message.Substring(8, 6);
                                                            }
                                                            catch (Exception Ex)
                                                            {
                                                                cParametros.Id = 0;
                                                                cParametros.Message = Ex.Message;
                                                                cParametros.StackTrace = Ex.StackTrace;
                                                                cParametros.Source = Ex.Source;
                                                                cParametros.TargetSite = Ex.TargetSite.ToString();
                                                                cParametros.Severity = clsSeveridad.Alta;
                                                                cParametros.Metodo = "_CerrarReserva validacion para multiples destinos ";
                                                                cParametros.Tipo = clsTipoError.WebServices;
                                                                ExceptionHandled.Publicar(cParametros);                                                            
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno(Solucion_);
                                                    }
                                                }
                                            }
                                        }
                                        System.Threading.Thread.Sleep(50);
                                    }
                                    if (bReservaNormal)
                                    {
                                        cParametros = cEndTrasaction._GuardarReserva();
                                    }
                                    else
                                    {
                                        if (!vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto.ToString().Trim().ToUpper().Contains("MULTI"))
                                        {
                                            string sComando = "*P6";
                                            cParametros = Negocios_WebServiceSabreCommand._EjecutarComandoGen(sComando);
                                            cParametros.Complemento = "Command de reserva: " + cParametros.Message;
                                            ExceptionHandled.Publicar(cParametros);
                                            cParametros.DatoAdic = clsValidacionesVuelos.setResultComado(cParametros.Message.ToString(), 1, 27, 6);
                                        }
                                    }
                                    if (cParametros.DatoAdic == null) cParametros.DatoAdic = String.Empty;

                                    if (cParametros.DatoAdic.Length != 6 || !Utils.clsValidaciones.IS_ALPHABETIC(cParametros.DatoAdic))
                                    {
                                        if (iCount < 3)
                                        {
                                            iCount++;
                                            i = -1;
                                        }
                                        else
                                        {
                                            cParametros.DatoAdic = Return_;
                                            i = SabreErrors_.Count;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                #endregion
                            }
                            if (i.CompareTo(SabreErrors_.Count - 1) == 0) cParametros.DatoAdic = NotComplete_;
                        }
                    }
                }
            }
            #endregion
            Record_ = cParametros.DatoAdic;
            if ((cParametros.DatoAdic == Return_) || (cParametros.DatoAdic == ReturnTotal_) || (cParametros.DatoAdic == NotComplete_))
            {
                cParametros.Data = "La reserva no se pudo confirmar";
                Record_ = cParametros.Data;
            }
            return cParametros;
        }
        public clsResultados Confirmar_Reserva(object sender, EventArgs e, UserControl ucControl)
        {
            string intIdItinerario = ucControl.Request.QueryString["ITIID"];
            try
            {
                string sWhere = "SequenceNumber = " + intIdItinerario;
                DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
                DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
                DataTable dtTablaRetornada = clsDataNet.dsDataWhere(sWhere, dtPricedItinerary);
                intIdItinerario = dtTablaRetornada.Rows[0]["PricedItinerary_Id"].ToString();
            }
            catch { }

            clsVuelos objVuelos = new clsVuelos();
            objVuelos.GuardarDatosPasajeros(ucControl);
            clsSesiones.SET_LVO_DataTravelItineraryAddInfo(GetListaDatosPasajeros());
            clsSesiones.SET_LOAD_PASAJERO(true);
            
            clsResultados objResultados = GetCerrar(intIdItinerario);
            return objResultados;
        }
        private List<VO_DataTravelItineraryAddInfo> GetListaDatosPasajeros()
        {
            List<VO_DataTravelItineraryAddInfo> listaPasajeros = new List<VO_DataTravelItineraryAddInfo>();
            VO_DataTravelItineraryAddInfo vo_DataTravelItineraryAddInfo;
            clsVuelos objVuelos = new clsVuelos();
            /*OBTENEMOS LOS DATOS DE LOS PASAJEROS INGRESADOS A LA TABLA DE PASAJEROS DEL DATASET*/
            DataTable dtPasajeros = objVuelos.GetDtPasajeros();
            /*RECORREMOS EL DATATABLE Y ASIGNAMOS LOS VALORES A vo_DataTravelItineraryAddInfo */
            if (dtPasajeros != null && dtPasajeros.Rows.Count > 0)
            {
                foreach (DataRow drFila in dtPasajeros.Rows)
                {
                    string strNombre = string.Empty;
                    string sTipoPax = drFila["strTipoPasajero"].ToString();
                    try
                    {
                        sTipoPax = drFila["strCode"].ToString();
                    }
                    catch { }
                    string sTipoPaxGen = sTipoPax;
                    try
                    {
                        sTipoPaxGen = drFila["strDetalleTipo"].ToString();
                    }
                    catch { }
                

                    if (sTipoPaxGen.Equals("ADT"))
                        strNombre = drFila["strPrimerNombre"].ToString() + " " + drFila["strTrato"].ToString();
                    else
                        strNombre = drFila["strPrimerNombre"].ToString() + " " + sTipoPaxGen;

                    vo_DataTravelItineraryAddInfo = new VO_DataTravelItineraryAddInfo(Convert.ToInt32(drFila["intIdPasajero"]),
                        strNombre,
                        drFila["strPrimerApellido"].ToString(),
                        Convert.ToBoolean(drFila["blInfante"]),
                        sTipoPax

                );
                    List<string> lTelefonos = new List<string>();
                    lTelefonos.Add(drFila["strTelefono"].ToString());
                    vo_DataTravelItineraryAddInfo.Telefono_ = lTelefonos;

                    if (!sTipoPaxGen.ToString().Equals("INF"))
                        vo_DataTravelItineraryAddInfo.Email_ = drFila["strEmail"].ToString();

              
                    String str_Fecha_Nacimiento = drFila["strFechaNacimiento"].ToString();
                   
                    vo_DataTravelItineraryAddInfo.Fecha_ = str_Fecha_Nacimiento;
                   
                    if (!drFila["strPasajeroFrecuente"].ToString().Equals(string.Empty))
                        vo_DataTravelItineraryAddInfo.ViajeroFrecuente_ = drFila["strPasajeroFrecuente"].ToString();

                    if (!drFila["strAerolinea"].ToString().Equals(string.Empty))
                        vo_DataTravelItineraryAddInfo.Aeroliena_ = drFila["strAerolinea"].ToString();

                    if (!drFila["strTipoDocumento"].ToString().Equals(string.Empty))
                        vo_DataTravelItineraryAddInfo.TipoDocumento_ = drFila["strTipoDocumento"].ToString();

                    if (!drFila["strDocumento"].ToString().Equals(string.Empty))
                        vo_DataTravelItineraryAddInfo.Documento_ = drFila["strDocumento"].ToString();
                    try
                    {
                        if (!drFila["strGenero"].ToString().Equals(string.Empty))
                        {
                            if (drFila["strGenero"].ToString().Contains("F"))
                            {
                                vo_DataTravelItineraryAddInfo.Genero_ = "F";
                            }
                            else
                            {
                                vo_DataTravelItineraryAddInfo.Genero_ = "M";
                            }
                        }
                        else
                        {
                            vo_DataTravelItineraryAddInfo.Genero_ = "M";
                        }
                    }
                    catch { vo_DataTravelItineraryAddInfo.Genero_ = "M"; }
                    listaPasajeros.Add(vo_DataTravelItineraryAddInfo);
                }
            }
            return listaPasajeros;
        }
        /*------------CERRAR_RESERVA---------------------*/
        public clsResultados GetCerrar(String intItinerary_Id)
        {
            /*CERRAMOS RESERVA*/
            string sRecord = string.Empty;
            string sRrr = clsSesiones.GET_RECORD();
            String strMensaje = string.Empty;
            clsResultados objResultados = new clsResultados();
            clsCache cCache = new csCache().cCache();
            Negocios_WebService_OTA_AirLowFareSearch objNegocios_WebService_OTA_AirLowFareSearch = new Negocios_WebService_OTA_AirLowFareSearch();

            if (sRrr == null)
            {
                
                setPasajeros(clsSesiones.GET_LVO_DataTravelItineraryAddInfo());
            }
            /*NOS RETORNA EL RECORD*/
            objResultados = getCerrarReserva(cCache, intItinerary_Id);

            return objResultados;
        }
        public void setPasajeros(List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo)
        {
            // Utilizado para hacer seguimiento del itinerario
            clsParametros cParametros = new clsParametros();
            // termina seguimiento
            Negocios_WebService_OTA_AirLowFareSearch objNegocios_WebService_OTA_AirLowFareSearch = new Negocios_WebService_OTA_AirLowFareSearch();

            // termina seguimiento
            objNegocios_WebService_OTA_AirLowFareSearch._AgregarInformacionPNR(lvo_DataTravelItineraryAddInfo);
            /*SE QUITO PARA PROBAR SIN LOS COMANDOS DE DE LOS NIÑOS E INFANTES */
            try
            {
                setFechas(lvo_DataTravelItineraryAddInfo);
            }
            catch { }
     
            try
            {
                setInfantes(lvo_DataTravelItineraryAddInfo);
            }
            catch { }
     
        } 
        private void setFechas(List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo)
        {
            if (lvo_DataTravelItineraryAddInfo != null)
            {
                int iPosision = 1;
                foreach (VO_DataTravelItineraryAddInfo vo_DataTravelItineraryAddInfo in lvo_DataTravelItineraryAddInfo)
                {
                    if (!vo_DataTravelItineraryAddInfo.Infante_)
                    {
                        if (!vo_DataTravelItineraryAddInfo.Fecha_.Length.Equals(0))
                        {
                            string sSexo = "M";
                            try
                            {
                                if (vo_DataTravelItineraryAddInfo.Genero_.Contains("F"))
                                    sSexo = "F";
                            }
                            catch { }
                            string sComando = "3DOCSA/DB/" + vo_DataTravelItineraryAddInfo.Fecha_ + "/" + sSexo + "/" + vo_DataTravelItineraryAddInfo.Apellido_ + "/" + vo_DataTravelItineraryAddInfo.Nombre_ + "-" + iPosision + ".1";
                            string sComandoAA = "4DOCSA/DB/" + vo_DataTravelItineraryAddInfo.Fecha_ + "/" + sSexo + "/" + vo_DataTravelItineraryAddInfo.Apellido_ + "/" + vo_DataTravelItineraryAddInfo.Nombre_ + "-" + iPosision + ".1";
                            string srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                            if (!srespuesta.Contains("* \n"))
                                srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComandoAA);
                        }
                    }
                    iPosision++;
                }
            }
        }
        /// <summary>
        /// Metodo para inclui los documentos de viajeros
        /// </summary>
        /// <param name="lvo_DataTravelItineraryAddInfo">Objeto donde estan los datos de los pax</param>
        /// <remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-01-30
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:         
        /// Fecha:     
        /// Descripción:  
        /// </remarks>
        private void setFoid(List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo)
        {
            if (lvo_DataTravelItineraryAddInfo != null)
            {
                int iPosision = 1;
                foreach (VO_DataTravelItineraryAddInfo vo_DataTravelItineraryAddInfo in lvo_DataTravelItineraryAddInfo)
                {
                    if (vo_DataTravelItineraryAddInfo.Documento_ != null)
                    {
                        if (vo_DataTravelItineraryAddInfo.Documento_.Length > 0)
                        {
                            string sComando = "3FOID/NI" + vo_DataTravelItineraryAddInfo.Documento_ + "-" + iPosision + ".1";
                            string sComandoAA = "4FOID/NI" + vo_DataTravelItineraryAddInfo.Documento_ + "-" + iPosision + ".1";
                            string srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                            if (!srespuesta.Contains("* \n"))
                                srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComandoAA);
                        }
                    }
                    iPosision++;
                }
            }
        }
        private void setInfantes(List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo, List<string> lsEdadesInfantes)
        {
            if (lvo_DataTravelItineraryAddInfo != null && lsEdadesInfantes != null)
            {
                int iPosision = 1;
                int iContador = 0;
                foreach (VO_DataTravelItineraryAddInfo vo_DataTravelItineraryAddInfo in lvo_DataTravelItineraryAddInfo)
                {
                    if (vo_DataTravelItineraryAddInfo.Infante_)
                    {
                        string sEdad = getValidadEdad(lsEdadesInfantes[iContador]);
                        string sComando = "3INFT/" + vo_DataTravelItineraryAddInfo.Apellido_ + "/" + vo_DataTravelItineraryAddInfo.Nombre_ + "/" + vo_DataTravelItineraryAddInfo.Fecha_ + "-1.1";
                        string sComandoAA = "4INFT/" + vo_DataTravelItineraryAddInfo.Apellido_ + "/" + vo_DataTravelItineraryAddInfo.Nombre_ + "/" + vo_DataTravelItineraryAddInfo.Fecha_ + "-1.1";
                        string srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                        if (!srespuesta.Contains("* \n"))
                            srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComandoAA);
                        iContador++;
                    }
                    iPosision++;
                }
            }
        }
        private void setInfantes(List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo)
        {
            if (lvo_DataTravelItineraryAddInfo != null)
            {
                int iPosision = 1;
                foreach (VO_DataTravelItineraryAddInfo vo_DataTravelItineraryAddInfo in lvo_DataTravelItineraryAddInfo)
                {
                    if (vo_DataTravelItineraryAddInfo.Infante_)
                    {
                        string sComando = "3INFT/" + vo_DataTravelItineraryAddInfo.Apellido_ + "/" + vo_DataTravelItineraryAddInfo.Nombre_ + "/" + vo_DataTravelItineraryAddInfo.Fecha_ + "-1.1";
                        string sComandoAA = "4INFT/" + vo_DataTravelItineraryAddInfo.Apellido_ + "/" + vo_DataTravelItineraryAddInfo.Nombre_ + "/" + vo_DataTravelItineraryAddInfo.Fecha_ + "-1.1";
                        string srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                        if (!srespuesta.Contains("* \n"))
                            srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComandoAA);
                    }
                    iPosision++;
                }
            }
        }
        private void setNinios(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
            if (vo_OTA_AirLowFareSearchLLSRQ != null && vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios != null)
            {
                int iInicioNinios = getInicioNinios(vo_OTA_AirLowFareSearchLLSRQ);
                List<string> lsEdadesNinios = vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios;
                foreach (string sEdadesNinios in lsEdadesNinios)
                {
                    string sEdad = getValidadEdad(sEdadesNinios);

                    string sComando = "PDTC" + sEdad + "-" + iInicioNinios + ".1";
                    string srespuesta = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                    iInicioNinios++;
                }
            }
        }
        private string getValidadEdad(string sEdad)
        {
            string sEdadTotal = sEdad;
            if (sEdad.Length.Equals(1))
            {
                sEdadTotal = "0" + sEdad;
            }
            return sEdadTotal;
        }
        private int getInicioNinios(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
            int iInicio = 0;
            int.TryParse(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[0].SCantidad, out iInicio);

            iInicio++;

            return iInicio;
        }
        public void setPopupMensaje(TemplateControl PageSource, string sMensaje)
        {
            Label lblMensaje = PageSource.FindControl("lblMensaje") as Label;
            AjaxControlToolkit.ModalPopupExtender MPEMensaje = PageSource.FindControl("MPEConfirmacion") as AjaxControlToolkit.ModalPopupExtender;
            lblMensaje.Text = sMensaje;
            MPEMensaje.Show();
        }
        public clsParametros setSubirReserva(string sReserva, string sContacto, string sProyecto)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            try
            {
                cResultados = csWebServices.SubirReserva(sReserva, Enum_ProveedorWebServices.Sabre);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "WebServices ";
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados.Error;
        }
        public void setSubirReserva(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            try
            {
                string[] sValue = csValue(PageSource);

                cResultados = csWebServices.SubirReservaSabre(sValue[1], sValue[2], sValue[3]);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "WebServices ";
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void setBotonPago(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            List<VO_TarifaPago> lvoTarifa = new List<VO_TarifaPago>();

            try
            {
                string[] sValue = csValue(PageSource);

                cParametros.Id = 1;
                Negocios_WebService_OTA_AirLowFareSearch cReserva = new Negocios_WebService_OTA_AirLowFareSearch();
                lvoTarifa = cReserva.getBotonPagoReserva(sValue[1], sValue[2], sValue[3]);

            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "WebServices ";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void setValidaCheck(UserControl PageSource)
        {
            Button btnConfirmar = (Button)PageSource.FindControl("btnConfirmar");
            CheckBox cbAcepto = (CheckBox)PageSource.FindControl("cbAcepto");
            try
            {
                if (btnConfirmar != null)
                {
                    if (cbAcepto != null)
                    {
                        if (cbAcepto.Checked)
                        {
                            btnConfirmar.Enabled = true;
                        }
                        else
                        {
                            btnConfirmar.Enabled = false;
                        }
                    }
                }
            }
            catch { }
        }
        public void LlenarCombosReservaVuelos(UserControl ucReservaVuelos)
        {
             DataList dtlPasajeros = (DataList)ucReservaVuelos.FindControl("dtlPasajeros");
             string sIdioma = string.Empty;
             try
             {
                 sIdioma = new csCache().cCache().Idioma;
             }
             catch { }
             if (sIdioma == "" || sIdioma == null)
             {
                 sIdioma = clsValidaciones.GetKeyOrAdd("strIdioma", "es");
             }

            if (dtlPasajeros != null)
            {
                if (dtlPasajeros.Items.Count > 0)
                {
                    for (int i = 0; i < dtlPasajeros.Items.Count; i++)
                    {
                        DropDownList ddlgenero = (DropDownList)dtlPasajeros.Items[i].FindControl("ddlGenero");
                        DropDownList ddlTipoDoc = (DropDownList)dtlPasajeros.Items[i].FindControl("ddlTipoDoc");
                        DataTable dtGenero = new CsConsultasVuelos().SPConsultaTabla("SPCONSULTAGENERO",new string[1] {sIdioma});  
                        DataTable dtTipoDocumento =new CsConsultasVuelos().SPConsultaTabla("SPConsultaTpoidentifica",new string[1] {sIdioma});
                   

                        if (dtGenero != null)
                        {
                            ddlgenero.DataSource = dtGenero;
                            ddlgenero.DataTextField = "STRDESCRIPCION";
                            ddlgenero.DataValueField = "INTCODE";
                            ddlgenero.DataBind();
                        }

                        if (dtTipoDocumento != null)
                        {
                            ddlTipoDoc.DataSource = dtTipoDocumento;
                            ddlTipoDoc.DataTextField = "STRDESCRIPCION";
                            ddlTipoDoc.DataValueField = "INTCODE";
                            ddlTipoDoc.DataBind();
                        }


                    }
                }

            }


        }
        public void setValidaPasajero(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        DataList dtlPasajeros = PageSource.FindControl("dtlPasajeros") as DataList;
                        string sReferUsuario = csReferencias.csTipoContactoRefere();
                        string sAutorizador = clsValidaciones.GetKeyOrAdd("Autorizador", "AV");
                        string sViajero = clsValidaciones.GetKeyOrAdd("Viajero", "VJ");
                        setBloqueoFechaPasajero(dtlPasajeros);
                        if (clsValidaciones.GetKeyOrAdd("ValidaPax", "False").ToUpper().Equals("TRUE"))
                        {
                            if (sReferUsuario.Equals(sViajero) || sReferUsuario.Equals(sAutorizador) || cCache.Passenger != null)
                                dtlPasajeros.Enabled = false;
                        }
                    }
                    catch { }
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
                cParametros.Complemento = "Confirmacion Vuelos";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }
        private void setBloqueoFechaPasajero(DataList dtlPasajeros)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        if (clsValidaciones.GetKeyOrAdd("BloquePaxFecha", "True").ToUpper().Equals("TRUE"))
                        {
                            for (int d = 0; d < dtlPasajeros.Items.Count; d++)
                            {
                                TextBox txtDia = dtlPasajeros.Items[d].FindControl("txtDia") as TextBox;
                                TextBox txtMes = dtlPasajeros.Items[d].FindControl("txtMes") as TextBox;
                                TextBox txtYear = dtlPasajeros.Items[d].FindControl("txtYear") as TextBox;
                                TextBox txtTipoPasajero1 = dtlPasajeros.Items[d].FindControl("txtTipoPasajero1") as TextBox;
                                if (!txtTipoPasajero1.Text.Contains("INF"))
                                {
                                    txtDia.Enabled = false;
                                    txtMes.Enabled = false;
                                    txtYear.Enabled = false;
                                }
                            }
                        }
                    }
                    catch { }
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
                cParametros.Complemento = "Confirmacion Vuelos";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PageSource"></param>
        private void setResumen(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                if (vo_OTA_AirLowFareSearchLLSRQ != null)
                {
                    Label lblOrigen = (Label)PageSource.FindControl("lblOrigen");
                    Label lblDestino = (Label)PageSource.FindControl("lblDestino");
                    Label lblFechaSalida = (Label)PageSource.FindControl("lblFechaSalida");
                    Label lblFechaLlegada = (Label)PageSource.FindControl("lblFechaLlegada");
                    Label lblPax = (Label)PageSource.FindControl("lblPax");
                    Label lblResultados = (Label)PageSource.FindControl("lblResultados");

                    // Datos adicionales segun diseño
                    Label lblTrayecto = (Label)PageSource.FindControl("lblTrayecto");
                    Label lblVueloIda = (Label)PageSource.FindControl("lblVueloIda");
                    Label lblVueloRegreso = (Label)PageSource.FindControl("lblVueloRegreso");
                    HtmlGenericControl divRegreso = (HtmlGenericControl)PageSource.FindControl("divRegreso");

                    if (lblVueloIda != null)
                        lblVueloIda.Text = "Vuelo Ida";

                    if (lblVueloRegreso != null)
                        lblVueloRegreso.Text = "Vuelo de Regreso";

                    if (lblTrayecto != null)
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto.Equals(Enum_TipoTrayecto.IdaRegreso))
                        {
                            lblTrayecto.Text = "Trayecto Ida y Vuelta";
                            if (divRegreso != null)
                                divRegreso.Style.Add("display", "block");
                        }
                        else
                        {
                            if (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto.Equals(Enum_TipoTrayecto.Ida))
                            {
                                lblTrayecto.Text = "Trayecto Solo Ida";
                            }
                            else
                            {
                                lblTrayecto.Text = "Trayecto Multiple";
                                if (divRegreso != null)
                                    divRegreso.Style.Add("display", "block");
                            }
                        }
                    }

                    int iPosFinal = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Count;
                    iPosFinal--;
                    if (lblOrigen != null)
                        lblOrigen.Text = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SDetalle;
                    if (lblDestino != null)
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto.Equals(Enum_TipoTrayecto.Ida))
                            lblDestino.Text = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoDestino.SDetalle;
                        else
                            lblDestino.Text = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[iPosFinal].Vo_AeropuertoOrigen.SDetalle;
                    }

                    string sFechaSale = clsValidaciones.ConverYMDtoDMMY(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaSalida, " ");
                    if (lblFechaSalida != null)
                        lblFechaSalida.Text = sFechaSale;

                    sFechaSale = clsValidaciones.ConverYMDtoDMMY(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[iPosFinal].SFechaSalida, " ");
                    if (lblFechaLlegada != null)
                        lblFechaLlegada.Text = sFechaSale;

                    int iPax = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros.Count;
                    int iADT = 0;
                    int iINF = 0;
                    int iCNN = 0;
                   
                    if (vo_OTA_AirLowFareSearchLLSRQ.CodigoPlan == null)
                    {
                        for (int i = 0; i < iPax; i++)
                        {
                            if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCodeGen.Contains("INF"))
                                iINF += int.Parse(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCantidad);
                            else
                                if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCodeGen.Contains("C"))
                                    iCNN += int.Parse(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCantidad);
                                else
                                    iADT += int.Parse(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCantidad);
                        }
                    }
                    else
                    {
                       
                        string sTipoProducto = clsValidaciones.GetKeyOrAdd("Producto", "ProductoID");
                        string sProductoId = clsValidaciones.GetKeyOrAdd("ProductoRelacionOfertasWS", "tblOfertasWS");
                        //string iProducto = "1";
                        //tblRefere otblRefere = new tblRefere();
                        //otblRefere.Get(sTipoProducto, sProductoId);
                        //if (otblRefere.Respuesta)
                        //    iProducto = otblRefere.intidRefere.Value;

                        string sIdioma = clsSesiones.getIdioma();
                        int iAplicacion = clsSesiones.getAplicacion();

                        //DataTable dtData = cPlanes.ConsultarRelacionesPlanPax(vo_OTA_AirLowFareSearchLLSRQ.CodigoPlan, iAplicacion, iProducto, sIdioma);
                        DataTable dtData = null;
                        for (int i = 0; i < iPax; i++)
                        {
                            string sWhere = string.Empty;
                            sWhere = "strValor = '" + vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCodigo + "'";
                            DataTable dtDataTemp = clsDataNet.dsDataWhere(sWhere, dtData);
                            if (dtDataTemp.Rows[0]["strRefere"].ToString().ToUpper().Contains("IN"))
                                iINF += int.Parse(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCantidad);
                            else
                                if (dtDataTemp.Rows[0]["strRefere"].ToString().ToUpper().Contains("C"))
                                    iCNN += int.Parse(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCantidad);
                                else
                                    iADT += int.Parse(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCantidad);

                        }
                    }
                    string sValidaSaltoPax = clsValidaciones.GetKeyOrAdd("bValidaSaltoPax", "False");
                    string sAdultosText = string.Empty;
                    string sNinosText = string.Empty;
                    string sInfanteText = string.Empty;

                    if (sValidaSaltoPax.ToUpper().Equals("TRUE"))
                    {
                        if (iADT.Equals(1))
                            sAdultosText = " Adulto <br />";
                        else
                            sAdultosText = " Adultos <br />";

                        if (iCNN.Equals(1))
                            sNinosText = " Niño <br />";
                        else
                            sNinosText = " Niños <br />";

                        if (iINF.Equals(1))
                            sInfanteText = " Infante ";
                        else
                            sInfanteText = " Infantes ";
                    }
                    else
                    {
                        if (iADT.Equals(1))
                            sAdultosText = " Adulto - ";
                        else
                            sAdultosText = " Adultos - ";

                        if (iCNN.Equals(1))
                            sNinosText = " Niño - ";
                        else
                            sNinosText = " Niños - ";

                        if (iINF.Equals(1))
                            sInfanteText = " Infante ";
                        else
                            sInfanteText = " Infantes ";
                    }
                    if (lblPax != null)
                        lblPax.Text = iADT.ToString() + sAdultosText + iCNN.ToString() + sNinosText + iINF.ToString() + sInfanteText;
                    if (lblResultados != null)
                        lblResultados.Text = vo_OTA_AirLowFareSearchLLSRQ.SVuelosARetornar;
                }
            }
            catch
            {
            }
        }
        private string[] setObtenerValor(string sValor, string sMoneda)
        {
            string[] sValorRetorna = new string[2];
            string sValorNew = string.Empty;
            clsParametros cParametros = new clsParametros();
            string sSeparador = ".";
            try
            {
                sValorNew = clsValidacionesVuelos.setResultComadoUltimo(sValor, "1-");
                sValorRetorna[0] = sMoneda;
                sValorRetorna[1] = clsValidaciones.RetornaNumeroCompleto(sValorNew, sSeparador);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Confirmacion Vuelos";
                ExceptionHandled.Publicar(cParametros);
            }
            return sValorRetorna;
        }
        private string[] csValue(UserControl PageSource)
        {
            string[] sValue = new string[6];
            try
            {
                if (PageSource.Request.QueryString["ITIID"] != null)
                {
                    sValue[0] = PageSource.Request.QueryString["ITIID"].ToString();
                }
                else
                {
                    sValue[0] = "0";
                }
                if (PageSource.Request.QueryString["RECORD"] != null)
                {
                    sValue[1] = PageSource.Request.QueryString["RECORD"].ToString();
                }
                else
                {
                    sValue[1] = "0";
                }
                if (PageSource.Request.QueryString["TA"] != null)
                {
                    sValue[2] = PageSource.Request.QueryString["TA"].ToString();
                }
                else
                {
                    sValue[2] = "0";
                }
                if (PageSource.Request.QueryString["ITA"] != null)
                {
                    sValue[3] = PageSource.Request.QueryString["ITA"].ToString();
                }
                else
                {
                    sValue[3] = "0";
                }
                if (PageSource.Request.QueryString["Id"] != null)
                {
                    sValue[4] = PageSource.Request.QueryString["Id"].ToString();
                }
                else
                {
                    sValue[4] = "0";
                }

                if (PageSource.Request.QueryString["TipoPlan"] != null)
                {
                    sValue[5] = PageSource.Request.QueryString["TipoPlan"].ToString();
                }
                else
                {
                    sValue[5] = "0";
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
        public void setCommand(UserControl PageSource, object sender, CommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Confirmar":
                        setConfirmaReservaVuelo(sender, e, PageSource);
                        break;
                    case "Cancelar":
                        csGeneralsPag.Buscador();
                        break;
                    case "Buscar":
                        csGeneralsPag.Buscador();
                        break;
                }
            }
            catch
            {
            }
        }
        public void SetOcultarbanner(Page PageSource)
        {
            UserControl bannerNal = (UserControl)PageSource.FindControl("UcBannersNal1");
            UserControl bannerInt = (UserControl)PageSource.FindControl("UcBannersInt1");

            string[] sValue = csValue(bannerNal);
            if (sValue[5] != clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR"))
            {
                if (bannerNal != null)
                    bannerNal.Visible = false;

                if (bannerInt != null)
                    bannerInt.Visible = false;
            }
            else
            {
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                if (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo == Enum_TipoVuelo.Internacional)
                {
                    bannerInt.Visible = true;
                    bannerNal.Visible = false;
                }

                if (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo == Enum_TipoVuelo.Nacional)
                {
                    bannerNal.Visible = true;
                    bannerInt.Visible = false;
                }
            }
        }

        public clsParametros SetEntrar(string strUsuario, string strPassword, UserControl PageSource)
        {
            Label lblError1 = (Label)PageSource.FindControl("lblError");
            csGenerales Generales = new csGenerales();
            Generales.Conexion = Conexion;
            clsLogin cLogin = new clsLogin();
            DataSet dsLogin = new DataSet();
            DataTable dtLogin = new DataTable();
            lblError1.Text = string.Empty;
            string sSesion = string.Empty;
            clsParametros cParametros = new clsParametros();
            clsCache cCache = new csCache().cCache();
            cParametros.Id = 1;

            string sUsuario = clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF");
            string sEmpresa = clsValidaciones.GetKeyOrAdd("Empresa", "EM");
            string sPropietario = clsValidaciones.GetKeyOrAdd("Propietario", "PT");
            string sViajero = clsValidaciones.GetKeyOrAdd("Viajero", "VJ");

            if (cCache.Empresa.ToString().Equals("") || cCache.Empresa.ToString().Equals("0"))
                cCache.Empresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "0");



            string strViajero = "0";
            string strEmpresa = "0";

            string strImagen = string.Empty;
            string[] sValues = new string[4];


            try
            {
                if (HttpContext.Current.Request.QueryString["idSesion"] != null)
                {
                    sSesion = HttpContext.Current.Request.QueryString["idSesion"].ToString();
                }
                else
                {
                    sSesion = HttpContext.Current.Session.SessionID;
                }
                cLogin.Conexion = Conexion;
                dtLogin = new CsConsultasVuelos().SPConsultaTabla("SPValidaUsuarioFinal", new string[4] { strUsuario, clsSesiones.getAplicacion().ToString(), cCache.Empresa.ToString(), clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF") });

                if (dtLogin != null)
                {

                    if (dtLogin.Rows.Count > 0)
                    {
                        if (dtLogin.Rows[0]["strPassword"].ToString().Equals(strPassword))
                        {
                            ExceptionHandled.Publicar("*************************////////////////////////////-- USUARIO ENCONTRADO MAIL: " + strUsuario + " & PASSWORD: " + strPassword + " & SESION: " + clsSesiones.getSesionID() + "  --////////////////////////");

                            strEmpresa = dtLogin.Rows[0]["intEmpresa"].ToString();
                            strViajero = dtLogin.Rows[0]["intUsuario"].ToString();

                            sValues[0] = strViajero;
                            sValues[1] = strEmpresa;

                            setActualizaSesion(dtLogin, cCache, sValues);

                            try { PageSource.Cache.Insert(sSesion, cCache); }
                            catch { }


                            string sPantallaRespuesta = clsSesiones.getPantalleRespuestaLogin();

                            if (sPantallaRespuesta == "")
                                clsValidaciones.RedirectPaginaSesion("../Presentacion/Index.aspx");
                            else
                                clsValidaciones.RedirectPaginaSesion(sPantallaRespuesta);
                         
                        }
                        else
                        {
                            cParametros.Id = 0;
                            lblError1.Text = clsValidaciones.GetKeyOrAdd("Por favor Verifique su Usuario y Contraseña");
                            ExceptionHandled.Publicar("*************************////////////////////////////-- USUARIO NO ENCONTRADO, LA CONTRASEÑA NO CORRESPONDE MAIL: " + strUsuario + " & PASSWORD: " + strPassword + " & SESION: " + clsSesiones.getSesionID() + "  --////////////////////////");
                        }
                    }
                    else
                    {
                        cParametros.Id = 0;
                        lblError1.Text = clsValidaciones.GetKeyOrAdd("Por favor Verifique su Usuario y Contraseña");
                        ExceptionHandled.Publicar("*************************////////////////////////////-- USUARIO NO ENCONTRADO, LA CONTRASEÑA NO CORRESPONDE MAIL: " + strUsuario + " & PASSWORD: " + strPassword + " & SESION: " + clsSesiones.getSesionID() + "  --////////////////////////");
                    }
                }
                else
                {
                    string sUrl = clsValidaciones.ObtenerUrl();
                    ExceptionHandled.Publicar("***url" + sUrl + "******");
                    ExceptionHandled.Publicar("***url" + sUrl.Replace("LoginTTQ", "Index") + "******");
                    clsValidaciones.RedirectPagina(sUrl.Replace("LoginTTQ", "Index"), false);
                    cParametros.Id = 0;
                    lblError1.Text = clsValidaciones.GetKeyOrAdd("Por favor Verifique su Usuario y Contraseña");
                    ExceptionHandled.Publicar("*************************////////////////////////////-- USUARIO NO ENCONTRADO, LA CONTRASEÑA NO CORRESPONDE MAIL: " + strUsuario + " & PASSWORD: " + strPassword + " & SESION: " + clsSesiones.getSesionID() + "  --////////////////////////");
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
                cParametros.Complemento = "SetEntrar; no se pudo actualizar el cache con los datos del contacto";
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }

        public bool SetCrearUsuario(UserControl PageSource, bool bValiaduserttq)
        {
            bool bValidaUser = true;
            try
            {

                clsParametros cParametros = new clsParametros();
                bool Enviacorreo = true;
                string sUrl = string.Empty;
                TextBox txtidentificacion = (TextBox)PageSource.FindControl("txtidentificacion");
                TextBox txtNombre = (TextBox)PageSource.FindControl("txtNombre");
                TextBox txtApellido = (TextBox)PageSource.FindControl("txtApellido");
                TextBox txtCiudad = (TextBox)PageSource.FindControl("txtCiudad");
                TextBox txtCelular = (TextBox)PageSource.FindControl("txtCelular");
                TextBox txtEmail = (TextBox)PageSource.FindControl("txtMailPersonal");
                TextBox txtPassword = (TextBox)PageSource.FindControl("txtclave");
                TextBox txtclaveConfir = (TextBox)PageSource.FindControl("txtclaveConfir");
                TextBox txtFechaNacimiento = (TextBox)PageSource.FindControl("txtEdad1");
                string sFecha = clsValidaciones.ConverMDYtoYMD(txtFechaNacimiento.Text, "/");
                string Nivel = new CsConsultasVuelos().ConsultaCodigo(clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF"), "tblnivelusuarios", "Intcode", "referetipousuario");

                string Empresa = new csCache().cCache().Empresa;

                if (Empresa == "")
                {
                    if (HttpContext.Current.Request.QueryString["idE"] != null)
                    {
                        Empresa = HttpContext.Current.Request.QueryString["idE"].ToString();
                    }
                    else if (HttpContext.Current.Request.Cookies["sEmpresa"] != null)
                    {
                        Empresa = HttpContext.Current.Request.Cookies["sEmpresa"]["idEmpresa"];
                    }
                    else
                    {
                        Empresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "3");
                    }

                }

                string StrCiudad = "1";
                string strClave = txtPassword.Text;

                string valida = "";
                DataTable dt = new CsConsultasVuelos().SPConsultaTabla("SPValidaUsuarioFinal", new string[4] { txtEmail.Text, clsSesiones.getAplicacion().ToString(), Empresa, clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF") });
                if (dt != null)
                {
                    Enviacorreo = false;
                    bValidaUser = false;
                }
                else
                {
                    string bit = new CsConsultasVuelos().EjecutarSPConsulta("SPCreausuario", new string[17] { clsSesiones.getAplicacion().ToString(), Nivel, Empresa, "0", "1", "'" + txtidentificacion.Text + "'", "'" + txtNombre.Text + "'", "'" + txtApellido.Text + "'", "1", "'" + txtFechaNacimiento.Text + "'", "'SIN DATOS'", "'" + StrCiudad + "'", "'" + "SIN DATOS" + "'", "'" + txtCelular.Text.Trim() + "'", "'" + txtEmail.Text + "'", "'" + strClave + "'", "0" });
                    if (bit != "" && bit != null)
                        bValidaUser = true;
                    else
                        bValidaUser = false;

                    if (bValidaUser)
                    {
                        HttpContext.Current.Session["UsuariosTopFlight"] = "1";
                        cParametros = new csResultadoVuelos().setLogin(txtEmail.Text, strClave, Enviacorreo, PageSource, Enum_Login.LoginCarro, true);
                    }
                }

                if (valida != null && valida != "")
                {
                    Enviacorreo = true;

                }



                if (bValiaduserttq && bValidaUser)
                {
                    sUrl = clsValidaciones.ObtenerUrl();

                    clsValidaciones.RedirectPagina(sUrl.Replace("login", "Index"), false);
                }
                else if (bValidaUser)
                {
                    clsValidaciones.RedirectPagina("Beneficios.aspx", false);
                }


            }
            catch
            {

                bValidaUser = false;
            }
            return bValidaUser;
        }

        public clsParametros SetCrearEmpresa(string strNombre, string strNit, string strDireccion, string strCiudad, string strTelefono, string strPcontacto, string strCargo, string Email, UserControl PageSource)
        {
            Label lblError1 = (Label)PageSource.FindControl("lblError");
            AjaxControlToolkit.ModalPopupExtender modalMensajes = (AjaxControlToolkit.ModalPopupExtender)PageSource.FindControl("MPEMensajes");
            clsParametros cParametros = new clsParametros();
            clsCache cCache = new csCache().cCache();
            string bRespuesta = string.Empty;


            string strEmpresa = string.Empty;

            if (cCache.Empresa != "0" && cCache.Empresa != "")
            {
                strEmpresa = cCache.Empresa;
            }
            else
            {
                strEmpresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "6");
            }

            //strEmpresa = "6";
            string strAplicacion = "1";
            string strNivel = "3";
            string intCiudad = "";
          
            string strTpoidentifica = new CsConsultasVuelos().ConsultaCodigo("NI", "TblTpoIdentifica", "intcode", "strCode");
            DataTable dtciudades = new CsConsultasVuelos().Consultatabla("SELECT INTCODE FROM TBLCIUDADESIDIOMAS WHERE STRDESCRIPTION LIKE '" + strCiudad + "%'");
            if (dtciudades != null)
            {
                if (dtciudades.Rows.Count > 0)
                {
                    intCiudad = dtciudades.Rows[0]["INTCODE"].ToString();
                }
                else
                {
                    intCiudad = "1";
                }
            }
            else
            {
                intCiudad = "1";
            }

            cParametros.Id = 1;
            try
            {
                string bValida = new CsConsultasVuelos().ConsultaCodigo(strNit, "TBLEMPRESAS", "INTEMPRESA", "STRIDENTIFICACION");
                if (bValida != "" && bValida != null)
                {
                    lblError1.Text = "Lo sentimos Este nit ya se encuentra registrado por favor verifiquelo he intente nuevamente ";
                }
                else
                {
                    bRespuesta = new CsConsultasVuelos().EjecutarSPConsulta("SPCreaEmpresasGuiaderuta", new string[10] { "'" + strEmpresa + "'", "'" + strAplicacion + "'", "'" + strNivel + "'", "'" + strTpoidentifica + "'", "'" + strNit + "'", "'" + strNombre + "'", "'" + strDireccion + "'", "'" + intCiudad + "'", "'" + strTelefono + "'", "'" + "Correo:" + Email + "-Cargo:" + strCargo + "-Persona de Contacto:" + strPcontacto + "-CiudadDigitada:" + strCiudad + "'" });
                    if (bRespuesta != string.Empty && bRespuesta != "" && bRespuesta != "0")
                    {
                        lblError1.Text = "La agencia " + strNombre + clsValidaciones.GetKeyOrAdd("textoRegistroGuiaderuta", " ha sido registrada por favor envia los documentos de afiliación al correo Comercialfreelance@tutiquete.com y espera la llamada de nuestro ejecutivo de cuenta");
                        try
                        {

                            List<string> datosCorreo = new List<string>();
                            datosCorreo.Add(strEmpresa);
                            datosCorreo.Add(strNit);
                            datosCorreo.Add(strDireccion);
                            datosCorreo.Add(strCiudad);
                            datosCorreo.Add(strTelefono);
                            datosCorreo.Add(strNombre);
                            datosCorreo.Add(strCargo);
                            datosCorreo.Add(Email);

                            //enviamos correo                    
                            csUtilitarios utilitarios = new csUtilitarios();
                            utilitarios.setCorreos(Email, "EFE", clsValidaciones.GetKeyOrAdd("TipoCorreoRegistroEmpresa", "TCRE"), datosCorreo.ToArray());
                        }
                        catch (Exception Ex)
                        {
                            cParametros.Id = 0;
                            cParametros.Message = Ex.Message.ToString();
                            cParametros.Source = Ex.Source.ToString();
                            cParametros.Tipo = clsTipoError.Library;
                            cParametros.Severity = clsSeveridad.Moderada;
                            cParametros.StackTrace = Ex.StackTrace.ToString();
                            cParametros.Complemento = "No se pudo enviar correo";
                            ExceptionHandled.Publicar(cParametros);
                            lblError1.Text = "No se pudo enviar correo";
                            ExceptionHandled.Publicar("Ocurrio un error y no se pudo enviar correo electronico");
                            modalMensajes.Show();
                        }
                    }
                    else
                    {
                        ExceptionHandled.Publicar("Ocurrio un error y la agencia no se creo datos:" + "'" + strEmpresa + "'" + "'" + strAplicacion + "'" + "'" + strNivel + "'" + "'" + strTpoidentifica + "'" + "'" + strNit + "'" + "'" + strNombre + "'" + "'" + strDireccion + "'" + "'" + strCiudad + "'" + "'" + strTelefono + "'" + "'" + "E-mail:" + Email + "  Cargo:" + strCargo + " Persona de Contacto:" + strPcontacto + " CiudadDigitada:" + strCiudad + "'");
                        lblError1.Text = "Lo sentimos Por favor verifique sus datos he intente nuevamente";
                    }

                }

                modalMensajes.Show();
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "SetEntrar; no se pudo actualizar el cache con los datos del contacto";
                ExceptionHandled.Publicar(cParametros);
                lblError1.Text = "Por favor verifique sus datos he intente nuevamente";
                ExceptionHandled.Publicar("Ocurrio un error y la agencia no se creo datos:" + "'" + strEmpresa + "'" + "'" + strAplicacion + "'" + "'" + strNivel + "'" + "'" + strTpoidentifica + "'" + "'" + strNit + "'" + "'" + strNombre + "'" + "'" + strDireccion + "'" + "'" + strCiudad + "'" + "'" + strTelefono + "'" + "'" + "E-mail:" + Email + "  Cargo:" + strCargo + " Persona de Contacto:" + strPcontacto + " CiudadDigitada:" + strCiudad + "'");
                modalMensajes.Show();
            }
            return cParametros;
        }
      
        public DataTable SetBuscarUsuarios(string Contacto, string strEmpresa)
        {
            DataTable dtUser = new DataTable();
            DataTable dtUsuarios = new CsConsultasVuelos().SPConsultaTabla("SPConsultaUsuariosyAfiliados", new string[2] { Contacto, strEmpresa });
            try
            {
                if (dtUsuarios != null)
                {
                    if (dtUsuarios.Rows.Count > 0)
                    {

                        dtUser.Columns.Add("intUsuario");
                        dtUser.Columns.Add("stridentificacion");
                        dtUser.Columns.Add("strNombre");
                        dtUser.Columns.Add("strApellido");
                        dtUser.Columns.Add("intGenero");
                        dtUser.Columns.Add("dtmFechanac");
                        dtUser.Columns.Add("inttipoident");
                        dtUser.Columns.Add("strtipouser");

                        DataRow drUser = dtUser.NewRow();
                        drUser[0] = dtUsuarios.Rows[0][0].ToString();
                        drUser[1] = dtUsuarios.Rows[0][1].ToString();
                        drUser[2] = dtUsuarios.Rows[0][2].ToString();
                        drUser[3] = dtUsuarios.Rows[0][3].ToString();
                        drUser[4] = dtUsuarios.Rows[0][4].ToString();
                        drUser[5] = dtUsuarios.Rows[0][5].ToString();
                        drUser[6] = dtUsuarios.Rows[0][6].ToString();
                        drUser[7] = "SUSCRIPTOR";

                        dtUser.Rows.Add(drUser);

                        if (dtUsuarios.Rows.Count >= 1)
                        {
                            for (int i = 0; i < dtUsuarios.Rows.Count; i++)
                            {
                                if (dtUsuarios.Rows[i][7].ToString() != "")
                                {
                                    DataRow dr = dtUser.NewRow();
                                    dr[0] = dtUsuarios.Rows[i][7].ToString();
                                    dr[1] = dtUsuarios.Rows[i][8].ToString();
                                    dr[2] = dtUsuarios.Rows[i][9].ToString();
                                    dr[3] = dtUsuarios.Rows[i][10].ToString();
                                    dr[4] = dtUsuarios.Rows[i][11].ToString();
                                    dr[5] = dtUsuarios.Rows[i][12].ToString();
                                    dr[6] = dtUsuarios.Rows[i][13].ToString();
                                    if (i == 0)
                                    {
                                        dr[7] = "ELEGIDOS REGISTRADOS";
                                    }
                                    dtUser.Rows.Add(dr);
                                }
                            }
                        }


                    }
                }

            }
            catch
            {
                return null;

            }
            return dtUser;
        }
    
    }
}
