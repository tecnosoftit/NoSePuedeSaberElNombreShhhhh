using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.IO;
using System.Configuration;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;
using Ssoft.Rules.WebServices;
using Ssoft.Rules.Generales;
using Ssoft.ValueObjects;
using Ssoft.Rules.Administrador;
using Ssoft.Rules.Pagina;
using Ssoft.Rules.Corporativo;
using Ssoft.Rules.Reservas;
using WS_SsoftSabre.Utilidades;
using Ssoft.DataNet;
using SsoftQuery.Vuelos;

namespace Ssoft.Pages
{
    public class csBuscador : TemplateControl
    {
        private const string DDL_ROUND_EDAD = "ddlEdadNinio";
        private const string DDL_ROUND_MESES = "ddlMeses";
        private const string DDL_ONE_EDAD = "ddlOneEdadNinio";
        private const string DDL_ONE_MESES = "ddlOneMeses";
        private const string DDL_MULTI_EDAD = "ddlMultiEdad";
        private const string DDL_MULTI_MESES = "ddlMultiMeses";
        private const string TD_ROUND_EDAD = "tdEdad";
        private const string TD_ROUND_MESES = "tdMeses";
        private const string TD_ONE_EDAD = "tdOneEdad";
        private const string TD_ONE_MESES = "tdOneMeses";
        private const string TD_MULTI_EDAD = "trMultiEdad";
        private const string TD_MULTI_MESES = "trMultiMeses";
        /*NOMBRE APLICACION*/     
        public const string PAGE_TUTIQUETE = "TTQ";       
        private const string idRefere = "intidRefere";
        private const string Detalle = "strDetalle";
        private const string DetalleRefere = "strDetalleRefere";
        private const string idContacto = "intContacto";
        private const string Nombre = "strRazonSocial";
        private const string MPEEBuscador = "MPEEBuscador";
        private const string ucResultadoVuelos = "ucResultadoVuelos";
        private static string sFormatoFecha = clsSesiones.getFormatoFecha();
        private static string sFormatoFechaBD = clsSesiones.getFormatoFechaBD();

        protected string strConexion = default(string);
        /// <summary>
        /// Estable u obtiene es string de conexion
        /// </summary>
        public string Conexion
        {
            set { strConexion = value; }
            get { return strConexion; }
        }
        public csBuscador()
        {
            strConexion = clsSesiones.getConexion();
        }
        /// <summary>
        /// Metodo que identifica el tipo de busqueda y redirecciona al correspondiente
        /// </summary>
        /// <param name="PageSource">Usercontrol</param>
        /// <param name="source">Boton</param>
        /// <param name="e">Evento</param>
        /// <remarks>
        /// Autor:      Faustino Posas   
        /// Company:    Ssoft Colombia
        /// Fecha: 
        /// -------------------
        /// Control de Cambios
        /// -------------------   
        /// Autor:          Juan Camilo Diaz        
        /// Fecha:          2011-11-30
        /// Descripcion:    se agrega validacion para planes de alojamiento
        /// -------------------   
        /// Autor:          Juan Camilo Diaz        
        /// Fecha:          2012-01-20
        /// Descripcion:    se agrega validacion para planes de disney
        /// Autor:          Juan Camilo Diaz        
        /// Fecha:          2012-02-22
        /// Descripcion:    se agrega validacion para cotizacion de planes tipo pasaporte solos
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
                        case "Air":
                            setBuscarVuelos(PageSource);
                            break;    
                    }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// metodo que recibe las variables por encabezado y crea un arreglo para poder trabajar con los valores de las vasriables
        /// </summary>
        /// <returns>Arreglo con valores de variables</returns>
        /// // <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-02-03
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>
        private string[] csValue(UserControl PageSource)
        {
            string[] sValue = new string[6];
            try
            {
                if (HttpContext.Current.Request.QueryString["Destino"] != null)
                {
                    sValue[0] = PageSource.Request.QueryString["Destino"].ToString();
                }
                else
                {
                    sValue[0] = "0";
                }
                if (HttpContext.Current.Request.QueryString["TipoPlan"] != null)
                {
                    sValue[1] = PageSource.Request.QueryString["TipoPlan"].ToString();
                }
                else
                {
                    sValue[1] = "0";
                }
                if (HttpContext.Current.Request.QueryString["Comidas"] != null)
                {
                    sValue[2] = PageSource.Request.QueryString["Comidas"].ToString();
                }
                else
                {
                    sValue[2] = "0";
                }
                if (HttpContext.Current.Request.QueryString["PaisDestino"] != null)
                {
                    sValue[3] = PageSource.Request.QueryString["PaisDestino"].ToString();
                }
                else
                {
                    sValue[3] = "0";
                }
                if (HttpContext.Current.Request.QueryString["ZonaGeo"] != null)
                {
                    sValue[4] = PageSource.Request.QueryString["ZonaGeo"].ToString();
                }
                else
                {
                    sValue[4] = "0";
                }
                if (HttpContext.Current.Request.QueryString["TIPODESTINO"] != null)
                {
                    sValue[5] = PageSource.Request.QueryString["TIPODESTINO"].ToString();
                }
                else
                {
                    sValue[5] = null;
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
        public void setCargar(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            try
            {
                bool bEntra = true;
                try
                {
                    HiddenField hdfParam = (HiddenField)PageSource.FindControl("hdfParam");
                    if (hdfParam != null)
                    {
                        if (hdfParam.Value.Equals("0"))
                            hdfParam.Value = "1";
                        else
                            bEntra = false;
                    }
                }
                catch { }
                if (bEntra)
                {
                    LimpiarTodo(PageSource);
                    if (csGeneralsPag.Externo())
                    {
                        new csBuscadorMB().setCargar(PageSource);
                    }
                    else
                    {
                        if (csGeneralsPag.Oferta())
                        {
                            //new csBuscadorMB().setCargarOferta(PageSource);
                        }
                        setAereo(PageSource);
                      
                    }
                }
            }
            catch
            {
            }
        }
        public void setCargarGen(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            try
            {
                string sForma = clsValidaciones.GetKeyOrAdd("TabBuscador", "TabBuscador");
                string sReferencia = clsValidaciones.GetKeyOrAdd("sReferenciaGeneral", "Generales");
                // Se inicializan variables de tabuladores
                //string sVuelos = "Vuelos";
                //string sHoteles = "Hoteles";
                //string sAutos = "Autos";
                //string sPlanes = "Planes";

             
                //bool bEntra = true;
                try
                {
                    string sBuscadorTipo = "ucBuscador_";
                    try
                    {
                        string sControlSup = PageSource.NamingContainer.ClientID.ToString() + "_";
                        string sControlInt = string.Empty;
                        //string sPagina = csGeneralsPag.PaginaActual();
                        if (!sControlSup.Contains("__"))
                        {
                            if (sControlSup.ToUpper().Contains("HOTEL"))
                            {
                                sControlInt = "ucBuscadorHotel_";
                            }
                            else
                            {
                                if (sControlSup.ToUpper().Contains("VUELO"))
                                {
                                    sControlInt = "ucBuscadorAereo_";
                                }
                                else
                                {
                                    if (sControlSup.ToUpper().Contains("COMPRA"))
                                    {
                                        sControlInt = "ucBuscador_";
                                    }
                                    else
                                    {
                                        if (sControlSup.ToUpper().Contains("CARRO"))
                                        {
                                            sControlInt = "ucBuscadorAuto_";
                                        }
                                        else
                                        {
                                            if (sControlSup.ToUpper().Contains("PLAN"))
                                            {
                                                sControlInt = "ucBuscadorPlan_";
                                            }
                                            else
                                            {
                                                if (sControlSup.ToUpper().Contains("ERROR"))
                                                {
                                                    sControlInt = "ucBuscador_";
                                                }
                                                else
                                                {
                                                    if (sControlSup.ToUpper().Contains("MICUENTA"))
                                                    {
                                                        sControlInt = "ucBuscador_";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            sBuscadorTipo = sControlSup + sControlInt;
                        }
                    }
                    catch { }
                    HiddenField hdfParam = (HiddenField)PageSource.FindControl("hdfParam");                   
                    if (hdfParam != null)
                    {
                        if (hdfParam.Value.Equals("0"))
                            hdfParam.Value = "1";
                      
                    }
                }
                catch { }
             
            }
            catch
            {
            }
        }       
        public void setCargarAereo(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);
            try
            {

                if (csGeneralsPag.Externo())
                {
                    new csBuscadorMB().setCargar(PageSource);
                }
                else
                {
                    setAereo(PageSource);
                }
            }
            catch
            {
            }
        }
        public void setCargarParametros(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                csGeneralsPag.Idioma(PageSource);

                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                   
                        setViajero(PageSource);
                        
                   
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
                cParametros.Complemento = "Gastos ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                //new csCache().setError(PageSource, cParametros);
            }
        }
        /// <summary>
        /// Metodo para seleccionar el tipo de busqueda
        /// </summary>
        /// <param name="PageSource"></param>
        public void setBuscarVuelos(UserControl PageSource)
        {
            try
            {
                if (ValidarCondiciones(PageSource))
                {
                    RadioButtonList modal_vuelos = (RadioButtonList)PageSource.FindControl("modal_vuelos");
                    clsSesiones.CLEAR_SESSION_AIR();
                    setActualizaAirport(PageSource);
                    if (modal_vuelos != null)
                    {
                        switch (modal_vuelos.SelectedValue)
                        {
                            case "0":
                                if (ValidarIdaRegreso(PageSource) && ValidarFechas(PageSource))
                                    BuscarIdaVuelta(PageSource);
                                break;
                            case "1":
                                if (ValidarIda(PageSource))
                                    BuscarSoloIda(PageSource);
                                break;
                            case "2":
                                if (ValidarMultiDestino(PageSource))
                                    BuscarMultiDestino(PageSource);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        if (ValidarMultiDestino(PageSource))
                            BuscarMultiDestino(PageSource);
                    }
                }
            }
            catch
            {
            }
        }               
        private void LimpiarTodo(UserControl PageSource)
        {
            // Aereo
            TextBox txt_Multi_D1 = (TextBox)PageSource.FindControl("txt_Multi_D1");
            TextBox txt_Multi_O1 = (TextBox)PageSource.FindControl("txt_Multi_O1");
            TextBox txtFechaMultiO1 = (TextBox)PageSource.FindControl("txtFechaMultiO1");
            TextBox txtFechaMulti2 = (TextBox)PageSource.FindControl("txt2VFechaMulti");
           
            try
            {
                if (txt_Multi_D1 != null)
                    txt_Multi_D1.Text = "";
            }
            catch { }
            try
            {
                if (txt_Multi_O1 != null)
                    txt_Multi_O1.Text = "";
            }
            catch { }
            try
            {
                if (txtFechaMultiO1 != null)
                    txtFechaMultiO1.Text = "";
            }
            catch { }
            try
            {
                if (txtFechaMulti2 != null)
                    txtFechaMulti2.Text = "";
            }
            catch { }           
           
        }     
        private void setViajero(UserControl PageSource)
        {
            try
            {
                HtmlGenericControl dViajero = (HtmlGenericControl)PageSource.FindControl("dViajero");
                if (dViajero != null)
                {
                    string sTipoContacto = csReferencias.csTipoContactoRefere();

                    string sUsuario = clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF");
                    string sComunidad = clsValidaciones.GetKeyOrAdd("Comunidad", "CM");
                    string sAgencia = clsValidaciones.GetKeyOrAdd("Agencia", "AG");
                    string sEmpresa = clsValidaciones.GetKeyOrAdd("Empresa", "EM");
                    string sPropietario = clsValidaciones.GetKeyOrAdd("Propietario", "PT");
                    string sAdministrador = clsValidaciones.GetKeyOrAdd("Administrador", "AD");
                    string sAgente = clsValidaciones.GetKeyOrAdd("Agente", "AA");
                    string sCoordinador = clsValidaciones.GetKeyOrAdd("Coordinador", "CE");
                    string sAutorizador = clsValidaciones.GetKeyOrAdd("Autorizador", "AV");
                    string sViajero = clsValidaciones.GetKeyOrAdd("Viajero", "VJ");
                    string sContabilidad = clsValidaciones.GetKeyOrAdd("Contabilidad", "UC");
                    string sProveedor = clsValidaciones.GetKeyOrAdd("Proveedor", "PS");

                    if (dViajero == null)
                        return;
                    if (sTipoContacto.Equals(sCoordinador))
                    {
                        dViajero.Style.Add("display", "block");
                    }
                    else
                    {
                        dViajero.Style.Add("display", "none");
                    }
                }
            }
            catch { }
        }
        private void setAereo(UserControl PageSource)
        {
            try
            {
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                RadioButtonList modal_vuelos = (RadioButtonList)PageSource.FindControl("modal_vuelos");
                RadioButtonList rblMultiEscala = (RadioButtonList)PageSource.FindControl("rblMultiEscala");

                if (modal_vuelos == null)
                    modal_vuelos = (RadioButtonList)PageSource.FindControl("modal_vuelos1");
                DropDownList ddlClaseMulti = (DropDownList)PageSource.FindControl("ddlClaseMulti");
                DropDownList ddlAerolinea = (DropDownList)PageSource.FindControl("ddlAerolinea");
                try
                {
                    if (ddlAerolinea != null)
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda.Equals(Enum_OrigenBusqueda.Planes))
                        {
                            DataTable tblOrigenes = new DataTable();
                            if (HttpContext.Current.Session["tblOrigenesPlanes"] != null)
                            {
                                tblOrigenes = (DataTable)HttpContext.Current.Session["tblOrigenesPlanes"];
                                if (tblOrigenes != null && tblOrigenes.Rows.Count > 0)
                                {
                                    if (ddlAerolinea != null)
                                    {
                                        List<string> sList = new List<string>();
                                        ddlAerolinea.Items.Clear();
                                        //ddlAerolinea.Items.Add(liItem);
                                        for (int i = 0; i < tblOrigenes.Rows.Count; i++)
                                        {
                                            if (sList.Count.Equals(0))
                                            {
                                                ListItem liItemAir = new ListItem(tblOrigenes.Rows[i]["strDetalleOperador"].ToString(), tblOrigenes.Rows[i]["strOperador"].ToString());
                                                ddlAerolinea.Items.Add(liItemAir);
                                                sList.Add(tblOrigenes.Rows[i]["strOperador"].ToString());
                                            }
                                            else
                                            {
                                                bool bEntra = true;
                                                foreach (string sLista in sList)
                                                {
                                                    if (sLista.Equals(tblOrigenes.Rows[i]["strOperador"].ToString()))
                                                    {
                                                        bEntra = false;
                                                        break;
                                                    }
                                                }
                                                if (bEntra)
                                                {
                                                    ListItem liItemAir = new ListItem(tblOrigenes.Rows[i]["strDetalleOperador"].ToString(), tblOrigenes.Rows[i]["strOperador"].ToString());
                                                    ddlAerolinea.Items.Add(liItemAir);
                                                    sList.Add(tblOrigenes.Rows[i]["strOperador"].ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            csGenerales cGeneral = new csGenerales();
                            DataSet dsData = new DataSet();
                            string sTipoRefere = clsValidaciones.GetKeyOrAdd("AIRLINESABRE", "AIRLINESABRE");
                            dsData = cGeneral.Refere(sTipoRefere, 3);
                            if (dsData != null)
                            {
                                clsControls.LlenaControl(ddlAerolinea, dsData, "strDetalle", "strRefere", true);
                            }
                        }
                    }
                }
                catch { }

                if (clsValidaciones.GetKeyOrAdd("bLimpiaParametros", "False").ToUpper().Equals("FALSE"))
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ != null)
                    {
                        if (rblMultiEscala != null)
                        {
                            rblMultiEscala.SelectedValue = vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas;
                        }
                        Enum_TipoTrayecto eTipoTrayecto = vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto;
                        clsCache cCache = new csCache().cCache();
                        if (ddlClaseMulti != null &&
                            vo_OTA_AirLowFareSearchLLSRQ.LsClase != null
                            && vo_OTA_AirLowFareSearchLLSRQ.LsClase.Count != 0)
                        {

                            ListItem itemEncontrado = ddlClaseMulti.Items.FindByValue(vo_OTA_AirLowFareSearchLLSRQ.LsClase[0]);
                            if (itemEncontrado != null)
                            {
                                ddlClaseMulti.ClearSelection();
                                itemEncontrado.Selected = true;
                            }
                        }
                        if (ddlAerolinea != null &&
                            vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida != null
                            && vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida.Count != 0)
                        {
                            ListItem itemEncontrado = ddlAerolinea.Items.FindByValue(vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida[0]);
                            if (itemEncontrado != null)
                            {
                                ddlAerolinea.ClearSelection();
                                itemEncontrado.Selected = true;
                            }
                        }
                        if (cCache != null)
                        {
                            if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Count > 0)
                            {
                                cCache.AeropuertoOrigen = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen;
                                cCache.AeropuertoDestino = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoDestino;
                                csCache.ActualizarCache(cCache);
                            }
                        }

                        switch (eTipoTrayecto)
                        {
                            case Enum_TipoTrayecto.Ida:
                                modal_vuelos.SelectedValue = "1";
                                setIda(vo_OTA_AirLowFareSearchLLSRQ, PageSource);
                                break;
                            case Enum_TipoTrayecto.IdaRegreso:
                                modal_vuelos.SelectedValue = "0";
                                setIdaRegreso(vo_OTA_AirLowFareSearchLLSRQ, PageSource);
                                break;
                            case Enum_TipoTrayecto.Multidestino:
                                modal_vuelos.SelectedValue = "2";
                                setMulti(vo_OTA_AirLowFareSearchLLSRQ, PageSource);
                                break;
                        }
                        Recuperar_Edades_Session_Vuelos(PageSource);
                    }
                    else
                    {
                        if (modal_vuelos != null)
                        {
                            modal_vuelos.SelectedValue = "0";
                        }
                        if (rblMultiEscala != null)
                        {
                            rblMultiEscala.SelectedValue = "1";
                        }
                    }
                }
                else
                {
                    if (modal_vuelos != null)
                    {
                        modal_vuelos.SelectedValue = "0";
                    }
                    if (rblMultiEscala != null)
                    {
                        rblMultiEscala.SelectedValue = "1";
                    }
                }
            }
            catch { }
        }    
        private void Recuperar_Edades_Session_Vuelos(UserControl PageSource)
        {
            try
            {
                DropDownList ddlMultiNinios = (DropDownList)PageSource.FindControl("ddlMultiNinios");
                DropDownList ddlMultiBebes = (DropDownList)PageSource.FindControl("ddlMultiBebes");
                Panel PanelEdadesNinos = (Panel)PageSource.FindControl("PanelEdadesNinos");
                Panel PanelEdadesInfantes = (Panel)PageSource.FindControl("PanelEdadesInfantes");

                if (ddlMultiNinios.SelectedItem.Value != "0")
                {
                    foreach (Control control in PanelEdadesNinos.Controls)
                    {
                        if (control is DropDownList)
                        {
                            DropDownList ddlEdadNino = control as DropDownList;

                            if (HttpContext.Current.Session[ddlEdadNino.ID] != null)
                            {
                                String intEdad = HttpContext.Current.Session[ddlEdadNino.ID].ToString();
                                ddlEdadNino.SelectedValue = intEdad;
                            }
                        }
                    }
                }
                if (ddlMultiBebes.SelectedItem.Value != "0")
                {
                    foreach (Control control in PanelEdadesInfantes.Controls)
                    {
                        if (control is DropDownList)
                        {
                            DropDownList ddlEdadInf = control as DropDownList;
                            if (HttpContext.Current.Session[ddlEdadInf.ID] != null)
                            {
                                String intEdad = HttpContext.Current.Session[ddlEdadInf.ID].ToString();
                                ddlEdadInf.SelectedValue = intEdad;
                            }
                        }
                    }
                }
            }
            catch { }
        }
        private void setMulti(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ, UserControl PageSource)
        {
            int iContador = 1;
            List<VO_OriginDestinationInformation> lvo_Rutas = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas;

            DropDownList ddlClaseMulti = (DropDownList)PageSource.FindControl("ddlClaseMulti");
            DropDownList ddlMultiAdultos = (DropDownList)PageSource.FindControl("ddlMultiAdultos");
            DropDownList ddlMultiNinios = (DropDownList)PageSource.FindControl("ddlMultiNinios");
            DropDownList ddlMultiBebes = (DropDownList)PageSource.FindControl("ddlMultiBebes");

            if (lvo_Rutas != null)
            {
                int iContadorRutas = lvo_Rutas.Count;

                foreach (VO_OriginDestinationInformation vo_Ruta in lvo_Rutas)
                {
                    if (!isArunk(vo_Ruta.OTipoSegmento))
                    {
                        TextBox txtOrigen = (TextBox)PageSource.FindControl("txt_Multi_O" + iContador);

                        if (txtOrigen != null)
                        {
                            txtOrigen.Text = vo_Ruta.Vo_AeropuertoOrigen.SDetalle;

                            TextBox txtDestino = (TextBox)PageSource.FindControl("txt_Multi_D" + iContador);

                            if (txtDestino != null)
                            {
                                txtDestino.Text = vo_Ruta.Vo_AeropuertoDestino.SDetalle;
                            }

                            TextBox txtFecha = (TextBox)PageSource.FindControl("txtFechaMultiO" + iContador);

                            if (txtFecha != null)
                            {
                                string sFecha = clsValidaciones.getFecha(vo_Ruta.SFechaSalida);
                                txtFecha.Text = clsValidaciones.getDatetime(sFecha).ToString(sFormatoFecha);
                            }

                            DropDownList ddlHora = (DropDownList)PageSource.FindControl("ddlMultiHora01" + iContador);

                            if (ddlHora != null)
                            {
                                string sHora = clsValidaciones.getHora(vo_Ruta.SFechaSalida);
                                ddlHora.SelectedValue = sHora;
                            }
                        }
                        iContador++;
                    }
                    int iADT = 0;
                    int iCNN = 0;
                    int iINF = 0;

                    setNumeroPasajeros(out iADT, out iCNN, out iINF, vo_OTA_AirLowFareSearchLLSRQ);
                    //Keep numpax To thirdparty to 
                    //hceron
                    Utils.clsSesiones.setNumeroPasajeros(iADT + iCNN);
                    if (vo_OTA_AirLowFareSearchLLSRQ.LsClase != null && vo_OTA_AirLowFareSearchLLSRQ.LsClase.Count > 0)
                    {
                        ddlClaseMulti.SelectedValue = vo_OTA_AirLowFareSearchLLSRQ.LsClase[0];
                    }

                    ddlMultiAdultos.SelectedValue = iADT.ToString();
                    ddlMultiNinios.SelectedValue = iCNN.ToString();
                    ddlMultiBebes.SelectedValue = iINF.ToString();
                }
            }
        }
        private void setIdaRegreso(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ, UserControl PageSource)
        {
            TextBox txt_Multi_D1 = (TextBox)PageSource.FindControl("txt_Multi_D1");
            TextBox txt_Multi_O1 = (TextBox)PageSource.FindControl("txt_Multi_O1");
            TextBox txtFechaMultiO1 = (TextBox)PageSource.FindControl("txtFechaMultiO1");
            TextBox txtFechaMulti2 = (TextBox)PageSource.FindControl("txt2VFechaMulti");

            DropDownList ddlMultiHoraO1 = (DropDownList)PageSource.FindControl("ddlMultiHora01");
            DropDownList ddlMultiHoraD2 = (DropDownList)PageSource.FindControl("ddlMultiHoraD2");
            DropDownList ddlMultiAdultos = (DropDownList)PageSource.FindControl("ddlMultiAdultos");
            DropDownList ddlMultiNinios = (DropDownList)PageSource.FindControl("ddlMultiNinios");
            DropDownList ddlMultiBebes = (DropDownList)PageSource.FindControl("ddlMultiBebes");
            RadioButtonList rblMultiEscala = (RadioButtonList)PageSource.FindControl("rblMultiEscala");

            TextBox txtIdaRegresoDestino = txt_Multi_D1;
            TextBox txtIdaRegresoSalida = txt_Multi_O1;
            TextBox txtIdaRegresoFechaSalida = txtFechaMultiO1;
            TextBox txtIdaRegresoFechaRegreso = txtFechaMulti2;

            DropDownList ddlRoundSalida = ddlMultiHoraO1;
            DropDownList ddlRoundLlegada = ddlMultiHoraD2;

            DropDownList ddlRoundAdultos = ddlMultiAdultos;
            DropDownList ddlRoundNinios = ddlMultiNinios;
            DropDownList ddlRoundBebes = ddlMultiBebes;
            RadioButtonList rblRoundEscala = rblMultiEscala;

            List<VO_OriginDestinationInformation> lvo_Rutas = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas;

            VO_OriginDestinationInformation vo_Rutas = lvo_Rutas[0];

            if (vo_Rutas != null)
            {
                txtIdaRegresoDestino.Text = vo_Rutas.Vo_AeropuertoDestino.SDetalle;
                txtIdaRegresoSalida.Text = vo_Rutas.Vo_AeropuertoOrigen.SDetalle;
                if (vo_Rutas.SFechaSalida.Trim().Length > 0)
                {
                    string sFechaSale = clsValidaciones.getFecha(vo_Rutas.SFechaSalida);
                    txtIdaRegresoFechaSalida.Text = Utils.clsValidaciones.getDatetime(sFechaSale).ToString(sFormatoFecha);
                }
                string sHoraSale = clsValidaciones.getHora(vo_Rutas.SFechaSalida);
                ddlRoundSalida.SelectedValue = sHoraSale;

                int iADT = 0;
                int iCNN = 0;
                int iINF = 0;

                setNumeroPasajeros(out iADT, out iCNN, out iINF, vo_OTA_AirLowFareSearchLLSRQ);
                //Keep numpax To thirdparty to 
                //hceron
                Utils.clsSesiones.setNumeroPasajeros(iADT + iCNN);

                ddlRoundAdultos.SelectedValue = iADT.ToString();
                ddlRoundNinios.SelectedValue = iCNN.ToString();
                ddlRoundBebes.SelectedValue = iINF.ToString();

                if (clsValidaciones.getValidarString(vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas))
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas.Equals("0"))
                    {
                        rblRoundEscala.SelectedIndex = 1;
                    }
                }
            }

            vo_Rutas = lvo_Rutas[1];

            if (isArunk(vo_Rutas.OTipoSegmento))
            {
                vo_Rutas = lvo_Rutas[2];
            }

            if (vo_Rutas != null)
            {
                if (vo_Rutas.SFechaSalida.Trim().Length > 0)
                {
                    string sFechaRegreso = clsValidaciones.getFecha(vo_Rutas.SFechaSalida);
                    txtIdaRegresoFechaRegreso.Text = Utils.clsValidaciones.getDatetime(sFechaRegreso).ToString(sFormatoFecha);
                }

                string sHoraRegreso = clsValidaciones.getHora(vo_Rutas.SFechaSalida);
                ddlRoundLlegada.SelectedValue = sHoraRegreso;
            }
        }
        private void setIda(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ, UserControl PageSource)
        {
            /*verificar*/
            TextBox txt_Multi_D1 = (TextBox)PageSource.FindControl("txt_Multi_D1");
            TextBox txt_Multi_O1 = (TextBox)PageSource.FindControl("txt_Multi_O1");
            TextBox txtFechaMultiO1 = (TextBox)PageSource.FindControl("txtFechaMultiO1");

            DropDownList ddlMultiHoraO1 = (DropDownList)PageSource.FindControl("ddlMultiHora01");
            DropDownList ddlMultiHoraD2 = (DropDownList)PageSource.FindControl("ddlMultiHoraD2");
            DropDownList ddlMultiAdultos = (DropDownList)PageSource.FindControl("ddlMultiAdultos");
            DropDownList ddlMultiNinios = (DropDownList)PageSource.FindControl("ddlMultiNinios");
            DropDownList ddlMultiBebes = (DropDownList)PageSource.FindControl("ddlMultiBebes");
            RadioButtonList rblMultiEscala = (RadioButtonList)PageSource.FindControl("rblMultiEscala");

            TextBox txtIdaDestino = txt_Multi_D1;
            TextBox txtIdaOrigen = txt_Multi_O1;
            DropDownList ddlIdaSalida = ddlMultiHoraO1;
            TextBox txtIdaFechaSalida = txtFechaMultiO1;
            RadioButtonList rblIdaEscala = rblMultiEscala;

            DropDownList ddlOneAdultos = ddlMultiAdultos;
            DropDownList ddlOneNinios = ddlMultiNinios;
            DropDownList ddlOneBebes = ddlMultiBebes;

            VO_OriginDestinationInformation vo_Rutas = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0];

            if (vo_Rutas != null)
            {
                txtIdaDestino.Text = vo_Rutas.Vo_AeropuertoDestino.SDetalle;
                txtIdaOrigen.Text = vo_Rutas.Vo_AeropuertoOrigen.SDetalle;
                ddlIdaSalida.SelectedValue = vo_Rutas.SFechaSalida;

                if (vo_Rutas.SFechaSalida.Trim().Length > 0)
                {
                    string sFecha = clsValidaciones.getFecha(vo_Rutas.SFechaSalida);
                    txtIdaFechaSalida.Text = Utils.clsValidaciones.getDatetime(sFecha).ToString(sFormatoFecha);
                }

                string sHora = clsValidaciones.getHora(vo_Rutas.SFechaSalida);
                ddlIdaSalida.SelectedValue = sHora;

                int iADT = 0;
                int iCNN = 0;
                int iINF = 0;

                setNumeroPasajeros(out iADT, out iCNN, out iINF, vo_OTA_AirLowFareSearchLLSRQ);
                //Keep numpax ro thirdparty to 
                //hceron
                Utils.clsSesiones.setNumeroPasajeros(iADT + iCNN);

                ddlOneAdultos.SelectedValue = iADT.ToString();
                ddlOneNinios.SelectedValue = iCNN.ToString();
                ddlOneBebes.SelectedValue = iINF.ToString();

                if (clsValidaciones.getValidarString(vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas))
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas.Equals("0"))
                    {
                        rblIdaEscala.SelectedIndex = 1;
                    }
                }
            }
        }       
        private bool isArunk(string oTipo)
        {
            if (oTipo.Equals
                (TipoSegmento.ARUNK.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void setNumeroPasajeros(out int iADT, out int iCNN, out int iINF, VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
            iADT = 0;
            iCNN = 0;
            iINF = 0;

            List<VO_Pasajero> lvo_Pasajero = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros;

            foreach (VO_Pasajero vo_Pasajero in lvo_Pasajero)
            {
                switch (vo_Pasajero.SCodigo)
                {
                    case VO_Pasajero.ADULTO:
                        int.TryParse(vo_Pasajero.SCantidad, out iADT);
                        break;
                    case VO_Pasajero.NINIO:
                        int.TryParse(vo_Pasajero.SCantidad, out iCNN);
                        break;
                    case VO_Pasajero.INFANTE:
                        int.TryParse(vo_Pasajero.SCantidad, out iINF);
                        break;
                    default:
                        int.TryParse("1", out iADT);
                        break;
                }
            }
        }
        private void Guardar_Edades_Session_Vuelos(UserControl PageSource)
        {
            DropDownList ddlMultiNinios = (DropDownList)PageSource.FindControl("ddlMultiNinios");
            DropDownList ddlMultiBebes = (DropDownList)PageSource.FindControl("ddlMultiBebes");
            Panel PanelEdadesNinos = (Panel)PageSource.FindControl("PanelEdadesNinos");
            Panel PanelEdadesInfantes = (Panel)PageSource.FindControl("PanelEdadesInfantes");

            if (ddlMultiNinios != null && ddlMultiNinios.SelectedItem.Value != "0")
            {
                foreach (Control control in PanelEdadesNinos.Controls)
                {
                    if (control is DropDownList)
                    {
                        DropDownList ddlEdadNino = control as DropDownList;
                        /*GUARDAMOS LA EDAD DE LOS NIÑOS*/
                        HttpContext.Current.Session.Add(ddlEdadNino.ID, ddlEdadNino.SelectedItem.Value);
                    }
                }
            }
            if (ddlMultiBebes != null && ddlMultiBebes.SelectedItem.Value != "0")
            {
                foreach (Control control in PanelEdadesInfantes.Controls)
                {
                    if (control is DropDownList)
                    {
                        DropDownList ddlEdadInf = control as DropDownList;
                        /*GUARDAMOS LA EDAD DE LOS INFANTES*/
                        HttpContext.Current.Session.Add(ddlEdadInf.ID, ddlEdadInf.SelectedItem.Value);
                    }
                }
            }
        }
        private void BuscarMultiDestino(UserControl PageSource)
        {
            //
            DropDownList ddlMultiAdultos = (DropDownList)PageSource.FindControl("ddlMultiAdultos");
            DropDownList ddlMultiNinios = (DropDownList)PageSource.FindControl("ddlMultiNinios");
            DropDownList ddlMultiBebes = (DropDownList)PageSource.FindControl("ddlMultiBebes");
            RadioButtonList rblMultiEscala = (RadioButtonList)PageSource.FindControl("rblMultiEscala");

            DropDownList ddlMultiHoraO1 = (DropDownList)PageSource.FindControl("ddlMultiHora01");
            DropDownList ddlMultiHoraO2 = (DropDownList)PageSource.FindControl("ddlMultiHoraO2");
            DropDownList ddlMultiHoraO3 = (DropDownList)PageSource.FindControl("ddlMultiHoraO3");
            DropDownList ddlMultiHoraO4 = (DropDownList)PageSource.FindControl("ddlMultiHoraO4");
            DropDownList ddlMultiHoraO5 = (DropDownList)PageSource.FindControl("ddlMultiHoraO5");
            DropDownList ddlMultiHoraO6 = (DropDownList)PageSource.FindControl("ddlMultiHoraO6");

            TextBox txtFechaMultiO1 = (TextBox)PageSource.FindControl("txtFechaMultiO1");
            TextBox txtFechaMultiO2 = (TextBox)PageSource.FindControl("txtFechaMultiO2");
            TextBox txtFechaMultiO3 = (TextBox)PageSource.FindControl("txtFechaMultiO3");
            TextBox txtFechaMultiO4 = (TextBox)PageSource.FindControl("txtFechaMultiO4");
            TextBox txtFechaMultiO5 = (TextBox)PageSource.FindControl("txtFechaMultiO5");
            TextBox txtFechaMultiO6 = (TextBox)PageSource.FindControl("txtFechaMultiO6");

            TextBox txt_Multi_O1 = (TextBox)PageSource.FindControl("txt_Multi_O1");
            TextBox txt_Multi_O2 = (TextBox)PageSource.FindControl("txt_Multi_O2");
            TextBox txt_Multi_O3 = (TextBox)PageSource.FindControl("txt_Multi_O3");
            TextBox txt_Multi_O4 = (TextBox)PageSource.FindControl("txt_Multi_O4");
            TextBox txt_Multi_O5 = (TextBox)PageSource.FindControl("txt_Multi_O5");
            TextBox txt_Multi_O6 = (TextBox)PageSource.FindControl("txt_Multi_O6");

            TextBox txt_Multi_D1 = (TextBox)PageSource.FindControl("txt_Multi_D1");
            TextBox txt_Multi_D2 = (TextBox)PageSource.FindControl("txt_Multi_D2");
            TextBox txt_Multi_D3 = (TextBox)PageSource.FindControl("txt_Multi_D3");
            TextBox txt_Multi_D4 = (TextBox)PageSource.FindControl("txt_Multi_D4");
            TextBox txt_Multi_D5 = (TextBox)PageSource.FindControl("txt_Multi_D5");
            TextBox txt_Multi_D6 = (TextBox)PageSource.FindControl("txt_Multi_D6");

            DropDownList ddlMoneda = (DropDownList)PageSource.FindControl("ddlMoneda");

            DropDownList ddlMultiHora1 = ddlMultiHoraO1;
            DropDownList ddlMultiHora2 = ddlMultiHoraO2;
            DropDownList ddlMultiHora3 = ddlMultiHoraO3;
            DropDownList ddlMultiHora4 = ddlMultiHoraO4;
            DropDownList ddlMultiHora5 = ddlMultiHoraO5;
            DropDownList ddlMultiHora6 = new DropDownList();
            if (ddlMultiHoraO6 != null)
                ddlMultiHora6 = ddlMultiHoraO6;

            TextBox txtFechaMulti2 = txtFechaMultiO2;
            TextBox txtFechaMulti3 = txtFechaMultiO3;
            TextBox txtFechaMulti4 = txtFechaMultiO4;
            TextBox txtFechaMulti5 = txtFechaMultiO5;
            TextBox txtFechaMulti6 = new TextBox();
            if (txtFechaMulti6 != null)
                txtFechaMulti6 = txtFechaMultiO6;

            if (ValidarVuelos(Enum_TipoTrayecto.Multidestino, PageSource))
            {
                string sOrigen1 = txt_Multi_O1.Text;
                string sOrigen2 = txt_Multi_O2.Text;
                string sOrigen3 = txt_Multi_O3.Text;
                string sOrigen4 = txt_Multi_O4.Text;
                string sOrigen5 = txt_Multi_O5.Text;
                string sOrigen6 = string.Empty;
                if (txt_Multi_O6 != null)
                    sOrigen6 = txt_Multi_O6.Text;

                string sDestino1 = txt_Multi_D1.Text;
                string sDestino2 = txt_Multi_D2.Text;
                string sDestino3 = txt_Multi_D3.Text;
                string sDestino4 = txt_Multi_D4.Text;
                string sDestino5 = txt_Multi_D5.Text;
                string sDestino6 = string.Empty;
                if (txt_Multi_D6 != null)
                    sDestino6 = txt_Multi_D6.Text;

                string sFecha1 = String.Empty;
                string sFecha2 = String.Empty;
                string sFecha3 = String.Empty;
                string sFecha4 = String.Empty;
                string sFecha5 = String.Empty;
                string sFecha6 = String.Empty;

                string sHora1 = "T" + ddlMultiHora1.SelectedValue;
                string sHora2 = "T" + ddlMultiHora2.SelectedValue;
                string sHora3 = "T" + ddlMultiHora3.SelectedValue;
                string sHora4 = "T" + ddlMultiHora4.SelectedValue;
                string sHora5 = "T" + ddlMultiHora5.SelectedValue;
                string sHora6 = "T" + ddlMultiHora6.SelectedValue;

                string sClase = "Y";
                string sAerolinea = "0";
                bool bBargain = false;
                List<string> lsAerolineas = new List<string>();
                Utils.Utils oUtilidad = new Utils.Utils();
                string sDestinoAnt = string.Empty;

                if (sAerolinea.Trim().Length.Equals(2))
                {
                    lsAerolineas.Add(sAerolinea);
                }
                bBargain = clsValidaciones._DROP_BARGAIN(ddlMultiHoraO1);

                List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation =
                       new List<VO_OriginDestinationInformation>();
                VO_Aeropuerto vo_AeropuertoOrigen = null;
                VO_Aeropuerto vo_AeropuertoDestino = null;
                VO_OriginDestinationInformation vo_Ruta = null;

                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = new VO_OTA_AirLowFareSearchLLSRQ();
                #region[VUELOS]
                if (sDestino1.Length > 2)
                {
                    sFecha1 = clsValidaciones.ConverYMD(txtFechaMultiO1.Text, sFormatoFecha, "-");
                    vo_OTA_AirLowFareSearchLLSRQ.BHoras = false;
                    if (!bBargain)
                    {
                        sFecha1 += sHora1;
                        //vo_OTA_AirLowFareSearchLLSRQ.BHoras = true;
                    }

                    vo_AeropuertoOrigen =
                        new VO_Aeropuerto(sOrigen1.Substring(0, 3), "IATA");
                    vo_AeropuertoDestino =
                        new VO_Aeropuerto(sDestino1.Substring(0, 3), "IATA");

                    vo_AeropuertoOrigen.SDetalle = sOrigen1;
                    vo_AeropuertoDestino.SDetalle = sDestino1;

                    vo_Ruta =
                        new VO_OriginDestinationInformation
                            (
                                sFecha1,
                                null,
                                null,
                                null,
                                vo_AeropuertoOrigen,
                                vo_AeropuertoDestino,
                                TipoSegmento.O,
                                false,
                                null,
                                null
                            );
                    lvo_OriginDestinationInformation.Add(vo_Ruta);
                }

                if (sDestino2.Length > 2)
                {
                    sFecha2 = clsValidaciones.ConverYMD(txtFechaMulti2.Text, sFormatoFecha, "-");

                    if (bBargain)
                    {
                        bBargain = clsValidaciones._DROP_BARGAIN(ddlMultiHora2);
                    }

                    if (!bBargain)
                    {
                        sFecha2 += sHora2;
                    }

                    vo_AeropuertoOrigen =
                       new VO_Aeropuerto(sOrigen2.Substring(0, 3), "IATA");
                    vo_AeropuertoDestino =
                        new VO_Aeropuerto(sDestino2.Substring(0, 3), "IATA");

                    vo_AeropuertoOrigen.SDetalle = sOrigen2;
                    vo_AeropuertoDestino.SDetalle = sDestino2;

                    //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                    //{
                    //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                    //}
                    //else
                    //{
                    //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                    //}
                    sDestinoAnt = vo_AeropuertoDestino.SCodigo;

                    vo_Ruta =
                        new VO_OriginDestinationInformation
                            (
                                sFecha2,
                                null,
                                null,
                                null,
                                vo_AeropuertoOrigen,
                                vo_AeropuertoDestino,
                                TipoSegmento.O,
                                false,
                                null,
                                null
                            );

                    lvo_OriginDestinationInformation.Add(vo_Ruta);
                }

                if (sDestino3.Length > 2)
                {
                    sFecha3 = clsValidaciones.ConverYMD(txtFechaMulti3.Text, sFormatoFecha, "-");
                    if (bBargain)
                    {
                        bBargain = clsValidaciones._DROP_BARGAIN(ddlMultiHora2);
                    }

                    if (!bBargain)
                    {
                        sFecha3 += sHora3;
                    }

                    vo_AeropuertoOrigen =
                       new VO_Aeropuerto(sOrigen3.Substring(0, 3), "IATA");
                    vo_AeropuertoDestino =
                        new VO_Aeropuerto(sDestino3.Substring(0, 3), "IATA");

                    vo_AeropuertoOrigen.SDetalle = sOrigen3;
                    vo_AeropuertoDestino.SDetalle = sDestino3;

                    //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                    //{
                    //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                    //}
                    //else
                    //{
                    //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                    //}
                    sDestinoAnt = vo_AeropuertoDestino.SCodigo;


                    vo_Ruta =
                        new VO_OriginDestinationInformation
                            (
                                sFecha3,
                                null,
                                null,
                                null,
                                vo_AeropuertoOrigen,
                                vo_AeropuertoDestino,
                                TipoSegmento.O,
                                false,
                                null,
                                null
                            );

                    lvo_OriginDestinationInformation.Add(vo_Ruta);
                }
                if (sDestino4.Length > 2)
                {
                    sFecha4 = clsValidaciones.ConverYMD(txtFechaMulti4.Text, sFormatoFecha, "-");

                    if (bBargain)
                    {
                        bBargain = clsValidaciones._DROP_BARGAIN(ddlMultiHora4);
                    }

                    if (!bBargain)
                    {
                        sFecha4 += sHora4;
                    }

                    vo_AeropuertoOrigen =
                       new VO_Aeropuerto(sOrigen4.Substring(0, 3), "IATA");
                    vo_AeropuertoDestino =
                        new VO_Aeropuerto(sDestino4.Substring(0, 3), "IATA");

                    vo_AeropuertoOrigen.SDetalle = sOrigen4;
                    vo_AeropuertoDestino.SDetalle = sDestino4;

                    //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                    //{
                    //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                    //}
                    //else
                    //{
                    //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                    //}
                    sDestinoAnt = vo_AeropuertoDestino.SCodigo;

                    vo_Ruta =
                        new VO_OriginDestinationInformation
                            (
                                sFecha4,
                                null,
                                null,
                                null,
                                vo_AeropuertoOrigen,
                                vo_AeropuertoDestino,
                                TipoSegmento.O,
                                false,
                                null,
                                null
                            );

                    lvo_OriginDestinationInformation.Add(vo_Ruta);
                }
                if (sDestino5.Length > 2)
                {
                    sFecha5 = clsValidaciones.ConverYMD(txtFechaMulti5.Text, sFormatoFecha, "-");

                    if (bBargain)
                    {
                        bBargain = clsValidaciones._DROP_BARGAIN(ddlMultiHora5);
                    }

                    if (!bBargain)
                    {
                        sFecha5 += sHora5;
                    }

                    vo_AeropuertoOrigen =
                       new VO_Aeropuerto(sOrigen5.Substring(0, 3), "IATA");
                    vo_AeropuertoDestino =
                        new VO_Aeropuerto(sDestino5.Substring(0, 3), "IATA");

                    vo_AeropuertoOrigen.SDetalle = sOrigen5;
                    vo_AeropuertoDestino.SDetalle = sDestino5;

                    //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                    //{
                    //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                    //}
                    //else
                    //{
                    //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                    //}
                    sDestinoAnt = vo_AeropuertoDestino.SCodigo;

                    vo_Ruta =
                        new VO_OriginDestinationInformation
                            (
                                sFecha5,
                                null,
                                null,
                                null,
                                vo_AeropuertoOrigen,
                                vo_AeropuertoDestino,
                                TipoSegmento.O,
                                false,
                                null,
                                null
                            );

                    lvo_OriginDestinationInformation.Add(vo_Ruta);
                }
                if (sDestino6.Length > 2)
                {
                    sFecha6 = clsValidaciones.ConverYMD(txtFechaMulti6.Text, sFormatoFecha, "-");

                    if (bBargain)
                    {
                        bBargain = clsValidaciones._DROP_BARGAIN(ddlMultiHora6);
                    }

                    if (!bBargain)
                    {
                        sFecha6 += sHora6;
                    }

                    vo_AeropuertoOrigen =
                       new VO_Aeropuerto(sOrigen6.Substring(0, 3), "IATA");
                    vo_AeropuertoDestino =
                        new VO_Aeropuerto(sDestino6.Substring(0, 3), "IATA");

                    vo_AeropuertoOrigen.SDetalle = sOrigen6;
                    vo_AeropuertoDestino.SDetalle = sDestino6;

                    //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                    //{
                    //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                    //}
                    //else
                    //{
                    //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                    //}
                    sDestinoAnt = vo_AeropuertoDestino.SCodigo;


                    vo_Ruta =
                        new VO_OriginDestinationInformation
                            (
                                sFecha6,
                                null,
                                null,
                                null,
                                vo_AeropuertoOrigen,
                                vo_AeropuertoDestino,
                                TipoSegmento.O,
                                false,
                                null,
                                null
                            );

                    lvo_OriginDestinationInformation.Add(vo_Ruta);
                }
                #endregion

                vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas = setValidarTrayectos(lvo_OriginDestinationInformation);

                List<string> lsClase = new List<string>();
                if (!sClase.Equals("-"))
                {
                    if (!sClase.Equals("0"))
                    {
                        lsClase.Add(sClase);
                    }
                    else
                    {
                        lsClase.Add("Y");
                    }
                }
                else
                {
                    lsClase.Add("Y");
                }
                vo_OTA_AirLowFareSearchLLSRQ.LsClase = lsClase;
                string sConvenio = csVuelos.csConvenio();
                if (!sConvenio.Length.Equals(0))
                    vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada = sConvenio;
                vo_OTA_AirLowFareSearchLLSRQ.BConFarCalc = true;
                vo_OTA_AirLowFareSearchLLSRQ.BRetornarMaxResultados = true;
                vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = lsAerolineas;
                vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas = rblMultiEscala.SelectedValue;
                vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto = Enum_TipoTrayecto.Multidestino;

                string sValidaMoneda = clsValidaciones.GetKeyOrAdd("ValidaMonedaVuelos", "False");
                string sMoneda = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                if (sValidaMoneda.ToUpper().Equals("TRUE"))
                {
                    if (ddlMoneda != null)
                        sMoneda = ddlMoneda.SelectedValue;
                }
                vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion = sMoneda;

                getPasajerosEdadesMeses(vo_OTA_AirLowFareSearchLLSRQ,
                    PageSource,
                    DDL_MULTI_EDAD,
                    DDL_MULTI_MESES);

                csVuelos clsValidacionesVuelos = new csVuelos();
                vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo = clsValidacionesVuelos.getValidarTipoTrayecto(lvo_OriginDestinationInformation);
                try
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo.Equals(Enum_TipoVuelo.Internacional))
                        vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = clsValidacionesVuelos.getValidarTipoSalida(lvo_OriginDestinationInformation);
                    else
                        vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional;
                }
                catch { vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional; }
                if (csGeneralsPag.Oferta())
                {
                    if (clsValidaciones.GetKeyOrAdd("eOfertaFija", "False").ToUpper().Equals("TRUE"))
                        vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda = Enum_OrigenBusqueda.OfertasFijas;
                }
                try
                {
                    setProcesarPseudo(vo_OTA_AirLowFareSearchLLSRQ);
                }
                catch { }
                try
                {
                    vo_OTA_AirLowFareSearchLLSRQ.Ruta = 0;
                }
                catch { }

                clsSesiones.setParametrosAirBargain
                    (
                       vo_OTA_AirLowFareSearchLLSRQ
                    );

                setProcesarBusqueda(PageSource);
            }
        }
        private void BuscarSoloIda(UserControl PageSource)
        {
            DropDownList ddlMultiAdultos = (DropDownList)PageSource.FindControl("ddlMultiAdultos");
            DropDownList ddlMultiNinios = (DropDownList)PageSource.FindControl("ddlMultiNinios");
            DropDownList ddlMultiBebes = (DropDownList)PageSource.FindControl("ddlMultiBebes");
            RadioButtonList rblMultiEscala = (RadioButtonList)PageSource.FindControl("rblMultiEscala");

            DropDownList ddlMultiHoraO1 = (DropDownList)PageSource.FindControl("ddlMultiHora01");
            DropDownList ddlClaseMulti = (DropDownList)PageSource.FindControl("ddlClaseMulti");

            TextBox txtFechaMultiO1 = (TextBox)PageSource.FindControl("txtFechaMultiO1");
            TextBox txt_Multi_O1 = (TextBox)PageSource.FindControl("txt_Multi_O1");
            TextBox txt_Multi_D1 = (TextBox)PageSource.FindControl("txt_Multi_D1");

            DropDownList ddlMoneda = (DropDownList)PageSource.FindControl("ddlMoneda");
            DropDownList ddlAerolinea = (DropDownList)PageSource.FindControl("ddlAerolinea");

            DropDownList ddlClaseIda = ddlClaseMulti;
            TextBox txtIdaDestino = txt_Multi_D1;
            TextBox txtIdaOrigen = txt_Multi_O1;
            TextBox txtIdaFechaSalida = txtFechaMultiO1;
            RadioButtonList rblIdaEscala = rblMultiEscala;
            DropDownList ddlIdaSalida = ddlMultiHoraO1;
            DropDownList ddlOneAdultos = ddlMultiAdultos;
            DropDownList ddlOneNinios = ddlMultiNinios;
            DropDownList ddlOneBebes = ddlMultiBebes;

            if (ValidarVuelos(Enum_TipoTrayecto.Ida, PageSource))
            {
                string sDestino;
                string sFechaSalida;
                string sHora;
                string sOrigen;
                string sClase;
                bool bBargain;
                List<string> lsAerolineas;
                Utils.Utils oUtilidad = new Utils.Utils();

                bBargain = false;
                sClase = ddlClaseIda.SelectedValue;
                sDestino = txtIdaDestino.Text.Substring(0, 3); ;
                sFechaSalida = clsValidaciones.ConverYMD(txtIdaFechaSalida.Text, sFormatoFecha, "-");
                sHora = "T" + ddlIdaSalida.Text;
                sOrigen = txtIdaOrigen.Text.Substring(0, 3); ;

                lsAerolineas = new List<string>();

                string sAerolinea = "0";
                if (ddlAerolinea != null)
                    sAerolinea = ddlAerolinea.SelectedValue.ToString();

                if (sAerolinea.Trim().Length.Equals(2))
                {
                    lsAerolineas.Add(sAerolinea);
                }

                bBargain = clsValidaciones._DROP_BARGAIN(ddlIdaSalida);

                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = new VO_OTA_AirLowFareSearchLLSRQ();

                vo_OTA_AirLowFareSearchLLSRQ.BHoras = false;
                if (!bBargain)
                {
                    sFechaSalida += sHora;
                    //vo_OTA_AirLowFareSearchLLSRQ.BHoras = true;
                }

                List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation =
                   new List<VO_OriginDestinationInformation>();

                VO_Aeropuerto vo_AeropuertoOrigen =
                    new VO_Aeropuerto(sOrigen, "IATA");
                VO_Aeropuerto vo_AeropuertoDestino =
                    new VO_Aeropuerto(sDestino, "IATA");

                vo_AeropuertoOrigen.SDetalle = txtIdaOrigen.Text;
                vo_AeropuertoDestino.SDetalle = txtIdaDestino.Text;

                VO_OriginDestinationInformation vo_Ruta =
                    new VO_OriginDestinationInformation
                        (
                            sFechaSalida,
                            null,
                            null,
                            null,
                            vo_AeropuertoOrigen,
                            vo_AeropuertoDestino,
                            TipoSegmento.O,
                            false,
                            null,
                            null
                        );

                lvo_OriginDestinationInformation.Add(vo_Ruta);

                vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas = lvo_OriginDestinationInformation;

                List<string> lsClase = new List<string>();
                if (!sClase.Equals("-"))
                {
                    if (!sClase.Equals("0"))
                    {
                        lsClase.Add(sClase);
                    }
                    else
                    {
                        lsClase.Add("Y");
                    }
                }
                else
                {
                    lsClase.Add("Y");
                }
                vo_OTA_AirLowFareSearchLLSRQ.LsClase = lsClase;

                string sConvenio = csVuelos.csConvenio();
                if (!sConvenio.Length.Equals(0))
                    vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada = sConvenio;
                vo_OTA_AirLowFareSearchLLSRQ.BConFarCalc = true;
                vo_OTA_AirLowFareSearchLLSRQ.BRetornarMaxResultados = true;
                vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = lsAerolineas;
                vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas = rblIdaEscala.SelectedValue;
                vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto = Enum_TipoTrayecto.Ida;
                string sValidaMoneda = clsValidaciones.GetKeyOrAdd("ValidaMonedaVuelos", "False");
                string sMoneda = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                if (sValidaMoneda.ToUpper().Equals("TRUE"))
                {
                    if (ddlMoneda != null)
                        sMoneda = ddlMoneda.SelectedValue;
                }
                vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion = sMoneda;

                getPasajerosEdadesMeses
                    (vo_OTA_AirLowFareSearchLLSRQ,
                    PageSource,
                    DDL_ONE_EDAD,
                    DDL_ONE_MESES);

                csVuelos clsValidacionesVuelos = new csVuelos();
                vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo = clsValidacionesVuelos.getValidarTipoTrayecto(lvo_OriginDestinationInformation);
                try
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo.Equals(Enum_TipoVuelo.Internacional))
                        vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = clsValidacionesVuelos.getValidarTipoSalida(lvo_OriginDestinationInformation);
                    else
                        vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional;
                }
                catch { vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional; }
                if (csGeneralsPag.Oferta())
                {
                    if (clsValidaciones.GetKeyOrAdd("eOfertaFija", "False").ToUpper().Equals("TRUE"))
                        vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda = Enum_OrigenBusqueda.OfertasFijas;
                }

                try
                {
                    setProcesarPseudo(vo_OTA_AirLowFareSearchLLSRQ);
                }
                catch { }
                try
                {
                    vo_OTA_AirLowFareSearchLLSRQ.Ruta = 0;
                }
                catch { }

                clsSesiones.setParametrosAirBargain
                   (
                      vo_OTA_AirLowFareSearchLLSRQ
                   );
                setProcesarBusqueda(PageSource);
            }
        }
        private void BuscarIdaVuelta(UserControl PageSource)
        {
            DropDownList ddlMultiAdultos = (DropDownList)PageSource.FindControl("ddlMultiAdultos");
            DropDownList ddlMultiNinios = (DropDownList)PageSource.FindControl("ddlMultiNinios");
            DropDownList ddlMultiBebes = (DropDownList)PageSource.FindControl("ddlMultiBebes");
            RadioButtonList rblMultiEscala = (RadioButtonList)PageSource.FindControl("rblMultiEscala");

            DropDownList ddlMultiHoraO1 = (DropDownList)PageSource.FindControl("ddlMultiHora01");
            DropDownList ddlMultiHoraO2 = (DropDownList)PageSource.FindControl("ddlMultiHoraO2");
            DropDownList ddlMultiHoraD2 = (DropDownList)PageSource.FindControl("ddlMultiHoraD2");
            DropDownList ddlClaseMulti = (DropDownList)PageSource.FindControl("ddlClaseMulti");

            TextBox txtFechaMultiO1 = (TextBox)PageSource.FindControl("txtFechaMultiO1");
            TextBox txtFechaMultiO2 = (TextBox)PageSource.FindControl("txtFechaMultiO2");
            TextBox txtFechaMulti2 = (TextBox)PageSource.FindControl("txt2VFechaMulti");

            TextBox txt_Multi_O1 = (TextBox)PageSource.FindControl("txt_Multi_O1");
            TextBox txt_Multi_O2 = (TextBox)PageSource.FindControl("txt_Multi_O2");

            TextBox txt_Multi_D1 = (TextBox)PageSource.FindControl("txt_Multi_D1");
            TextBox txt_Multi_D2 = (TextBox)PageSource.FindControl("txt_Multi_D2");

            DropDownList ddlMoneda = (DropDownList)PageSource.FindControl("ddlMoneda");

            DropDownList ddlClase = new DropDownList();
            TextBox txtIdaRegresoDestino = txt_Multi_D1;
            TextBox txtIdaRegresoSalida = txt_Multi_O1;
            TextBox txtIdaRegresoFechaSalida = txtFechaMultiO1;
            TextBox txtIdaRegresoFechaRegreso = txtFechaMulti2;
            DropDownList ddlRoundAdultos = ddlMultiAdultos;
            DropDownList ddlRoundNinios = ddlMultiNinios;
            DropDownList ddlRoundBebes = ddlMultiBebes;
            DropDownList ddlRoundLlegada = ddlMultiHoraD2;
            DropDownList ddlRoundSalida = ddlMultiHoraO1;
            RadioButtonList rblRoundEscala = rblMultiEscala;
            DropDownList ddlAerolinea = (DropDownList)PageSource.FindControl("ddlAerolinea");

            ddlClase = ddlClaseMulti;

            if (ValidarVuelos(Enum_TipoTrayecto.IdaRegreso, PageSource))
            {
                string sDestino;
                string sFechaSalida;
                string sHoraSalida;
                string sFechaRegreso;
                string sHoraRegreso;
                string sOrigen;
                string sClase;
                List<string> lsAerolineas = new List<string>();
                bool bBargain;
                string sAdt;
                string sCnn;
                string sInf;
                //Utils.Utils oUtilidad = new Utils.Utils();

                bBargain = false;
                sClase = ddlClase.SelectedValue;
                sDestino = txtIdaRegresoDestino.Text.Substring(0, 3);
                sFechaSalida = clsValidaciones.ConverYMD(txtIdaRegresoFechaSalida.Text, sFormatoFecha, "-");
                sHoraSalida = "T" + ddlRoundSalida.Text;
                sFechaRegreso = clsValidaciones.ConverYMD(txtIdaRegresoFechaRegreso.Text, sFormatoFecha, "-");
                sHoraRegreso = "T" + ddlRoundLlegada.Text;
                sOrigen = txtIdaRegresoSalida.Text.Substring(0, 3);
                sAdt = ddlRoundAdultos.SelectedValue;
                sCnn = ddlRoundNinios.SelectedValue;
                sInf = ddlRoundBebes.SelectedValue;
                bBargain = clsValidaciones._DROP_BARGAIN(ddlRoundSalida);

                if (bBargain)
                {
                    bBargain = clsValidaciones._DROP_BARGAIN(ddlRoundLlegada);
                }

                if (!bBargain)
                {
                    bBargain = clsValidaciones._DROP_BARGAIN(ddlRoundLlegada);
                }

                string sAerolinea = "0";
                if (ddlAerolinea != null)
                    sAerolinea = ddlAerolinea.SelectedValue.ToString();
                if (sAerolinea.Trim().Length.Equals(2))
                {
                    lsAerolineas.Add(sAerolinea);
                }
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = new VO_OTA_AirLowFareSearchLLSRQ();

                vo_OTA_AirLowFareSearchLLSRQ.BHoras = false;
                if (!bBargain)
                {
                    sFechaSalida += sHoraSalida;
                    sFechaRegreso += sHoraRegreso;
                    vo_OTA_AirLowFareSearchLLSRQ.BHoras = true;
                }

                List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation =
                    new List<VO_OriginDestinationInformation>();

                VO_Aeropuerto vo_AeropuertoOrigen =
                    new VO_Aeropuerto(sOrigen, "IATA");
                VO_Aeropuerto vo_AeropuertoDestino =
                    new VO_Aeropuerto(sDestino, "IATA");

                vo_AeropuertoOrigen.SDetalle = txtIdaRegresoSalida.Text;
                vo_AeropuertoDestino.SDetalle = txtIdaRegresoDestino.Text;

                VO_OriginDestinationInformation vo_Ruta =
                    new VO_OriginDestinationInformation
                        (
                            sFechaSalida,
                            null,
                            null,
                            null,
                            vo_AeropuertoOrigen,
                            vo_AeropuertoDestino,
                            TipoSegmento.O,
                            false,
                            null,
                            null
                        );
                lvo_OriginDestinationInformation.Add(vo_Ruta);

                //VUELO REGRESO
                vo_Ruta =
                    new VO_OriginDestinationInformation
                        (
                            sFechaRegreso,
                            null,
                            null,
                            null,
                            vo_AeropuertoDestino,
                            vo_AeropuertoOrigen,
                            TipoSegmento.O,
                            false,
                            null,
                            null
                        );

                lvo_OriginDestinationInformation.Add(vo_Ruta);

                vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas = lvo_OriginDestinationInformation;

                List<string> lsClase = new List<string>();
                if (!sClase.Equals("-"))
                {
                    if (!sClase.Equals("0"))
                    {
                        lsClase.Add(sClase);
                    }
                    else
                    {
                        lsClase.Add("Y");
                    }
                }
                else
                {
                    lsClase.Add("Y");
                }
                vo_OTA_AirLowFareSearchLLSRQ.LsClase = lsClase;

                string sConvenio = csVuelos.csConvenio();
                if (!sConvenio.Length.Equals(0))
                    vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada = sConvenio;

                vo_OTA_AirLowFareSearchLLSRQ.BConFarCalc = true;
                vo_OTA_AirLowFareSearchLLSRQ.BRetornarMaxResultados = true;
                vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = lsAerolineas;
                vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas = rblRoundEscala.SelectedValue;
                vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto = Enum_TipoTrayecto.IdaRegreso;
                string sValidaMoneda = clsValidaciones.GetKeyOrAdd("ValidaMonedaVuelos", "False");
                string sMoneda = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                if (sValidaMoneda.ToUpper().Equals("TRUE"))
                {
                    if (ddlMoneda != null)
                        sMoneda = ddlMoneda.SelectedValue;
                }
                vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion = sMoneda;
                bool bEntraNormal = true;
                /*OBTENEMOS LAS EDADES DE LOS NIÑOS Y LOS INFANTES*/
                vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda = Enum_OrigenBusqueda.Normal;
                try
                {
                    if (csGeneralsPag.Oferta())
                    {
                        if (HttpContext.Current.Request.QueryString["IdOferta"] != null)
                        {
                            getPasajerosEdadesMesesPlanes(vo_OTA_AirLowFareSearchLLSRQ,
                                    PageSource,
                                    sAdt, sCnn, sInf, HttpContext.Current.Request.QueryString["IdOferta"].ToString());
                            if (clsValidaciones.GetKeyOrAdd("bLiquidaVuelosNormal", "False").ToUpper().Equals("TRUE"))
                                vo_OTA_AirLowFareSearchLLSRQ.BHoras = true;
                            bEntraNormal = false;
                        }
                        if (clsValidaciones.GetKeyOrAdd("eOfertaFija", "False").ToUpper().Equals("TRUE"))
                            vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda = Enum_OrigenBusqueda.OfertasFijas;
                    }
                }
                catch { }
                if (bEntraNormal)
                {
                    getPasajerosEdadesMeses(vo_OTA_AirLowFareSearchLLSRQ,
                        PageSource,
                        DDL_ROUND_EDAD,
                        DDL_ROUND_MESES);
                }
                csVuelos clsValidacionesVuelos = new csVuelos();
                vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo = clsValidacionesVuelos.getValidarTipoTrayecto(lvo_OriginDestinationInformation);
                try
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo.Equals(Enum_TipoVuelo.Internacional))
                        vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = clsValidacionesVuelos.getValidarTipoSalida(lvo_OriginDestinationInformation);
                    else
                        vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional;
                }
                catch { vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional; }
                try
                {
                    setProcesarPseudo(vo_OTA_AirLowFareSearchLLSRQ);
                }
                catch { }
                try
                {
                    vo_OTA_AirLowFareSearchLLSRQ.Ruta = 0;
                }
                catch { }
                clsSesiones.setParametrosAirBargain
                   (
                      vo_OTA_AirLowFareSearchLLSRQ
                   );

                setProcesarBusqueda(PageSource);
            }
        }   
        public void getPasajerosEdadesMesesPlanes(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ, UserControl PageSource, string sAdt, string sCnn, string sInf, string sCodigo)
        {
            DropDownList ddlMultiAdultos = (DropDownList)PageSource.FindControl("ddlMultiAdultos");
            DropDownList ddlMultiNinios = (DropDownList)PageSource.FindControl("ddlMultiNinios");
            DropDownList ddlMultiBebes = (DropDownList)PageSource.FindControl("ddlMultiBebes");
            Panel PanelEdadesNinos = (Panel)PageSource.FindControl("PanelEdadesNinos");
            Panel PanelEdadesInfantes = (Panel)PageSource.FindControl("PanelEdadesInfantes");

            /*ASIGNAR A VARIABLES*/
            DropDownList ddlEdadesNinios = new DropDownList();
            DropDownList ddlEdadesBebe = new DropDownList();
            VO_Pasajero vo_Pasajero = null;
            List<VO_Pasajero> lvo_Pasajeros = new List<VO_Pasajero>();
            bool bSigue = true;
            bool bPasa = true;
            if (clsValidaciones.getValidarString(sCodigo))
            {
                //csPlanes cPlanes = new csPlanes();
                string sTipoProducto = clsValidaciones.GetKeyOrAdd("Producto", "ProductoID");
                string sProductoId = clsValidaciones.GetKeyOrAdd("ProductoRelacionOfertasWS", "tblOfertasWS");
                string iProducto = "1";
                //tblRefere otblRefere = new tblRefere();
                //otblRefere.Get(sTipoProducto, sProductoId);
                //if (otblRefere.Respuesta)
                //    iProducto = otblRefere.intidRefere.Value;

                string sIdioma = clsSesiones.getIdioma();
                int iAplicacion = clsSesiones.getAplicacion();

                //DataTable dtData = cPlanes.ConsultarRelacionesPlanPax(sCodigo, iAplicacion, iProducto, sIdioma);
                DataTable dtData = null;
                if (dtData != null)
                {
                    int iCantPax = 0;
                    bPasa = true;
                    if (!sInf.Equals("0"))
                    {
                        string sNumBebes = sInf;
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            if (dtData.Rows[i]["strRefere"].ToString().ToUpper().Equals("INF"))
                            {
                                vo_Pasajero = new VO_Pasajero(dtData.Rows[i]["strValor"].ToString().ToUpper(), sNumBebes);
                                vo_Pasajero.SDetalle = "Infante";
                                vo_Pasajero.SCodeGen = "INF";
                                int iNumBebes = 0;
                                int.TryParse(sNumBebes, out iNumBebes);
                                List<string> lsEdadesBebes = new List<string>();
                                List<VO_ClasificaPasajero> lvPasajeroInf = new List<VO_ClasificaPasajero>();
                                int intNumeroDeInfantes = Convert.ToInt32(ddlMultiBebes.SelectedItem.Value);

                                for (int p = 0; p < intNumeroDeInfantes; p++)
                                {
                                    ddlEdadesBebe = PanelEdadesInfantes.FindControl("ddlMultiMeses" + (p + 1)) as DropDownList;

                                    if (ddlEdadesBebe != null)
                                    {
                                        bool bPax = true;
                                        string sEdad = ddlEdadesBebe.SelectedValue.ToString();
                                        if (sEdad.Length.Equals(1))
                                            sEdad = "0" + sEdad;
                                        int iPax = 1;
                                        if (lvPasajeroInf.Count > 0)
                                        {
                                            for (int j = 0; j < lvPasajeroInf.Count; j++)
                                            {
                                                if (lvPasajeroInf[j].SEdad.Equals(sEdad))
                                                {
                                                    iPax = int.Parse(lvPasajeroInf[j].SCantidad.ToString());
                                                    iPax++;
                                                    lvPasajeroInf[j].SCantidad = iPax.ToString();
                                                    lvPasajeroInf[j].SDetalle = "Infante";
                                                    lvPasajeroInf[j].SCodeGen = "INF";
                                                    bPax = false;
                                                }
                                            }
                                        }
                                        if (bPax)
                                        {
                                            VO_ClasificaPasajero vPasajeroInf = new VO_ClasificaPasajero(dtData.Rows[i]["strValor"].ToString().ToUpper(), iPax.ToString(), sEdad);
                                            vPasajeroInf.SDetalle = "Infante";
                                            vPasajeroInf.SCodeGen = "INF";
                                            lvPasajeroInf.Add(vPasajeroInf);
                                        }
                                        lsEdadesBebes.Add(ddlEdadesBebe.SelectedValue);
                                    }
                                }
                                vo_Pasajero.LvPasajeroInfante = lvPasajeroInf;
                                lvo_Pasajeros.Add(vo_Pasajero);
                                vo_OTA_AirLowFareSearchLLSRQ.LsEdadesInfantes = lsEdadesBebes; bPasa = false;
                                break;
                            }
                        }
                        if (bPasa)
                        {
                            iCantPax += int.Parse(sInf);
                        }
                    }
                    bPasa = true;
                    if (!sCnn.Equals("0"))
                    {
                        string sNumNinios = sCnn;

                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            if (dtData.Rows[i]["strRefere"].ToString().ToUpper().Equals("CNN"))
                            {
                                vo_Pasajero = new VO_Pasajero(dtData.Rows[i]["strValor"].ToString().ToUpper(), sNumNinios);
                                vo_Pasajero.SDetalle = "Niño";
                                vo_Pasajero.SCodeGen = "CNN";

                                int iNumNinios = 0;
                                int.TryParse(sNumNinios, out iNumNinios);

                                List<string> lsEdadesNinio = new List<string>();
                                List<VO_ClasificaPasajero> lvPasajeroNino = new List<VO_ClasificaPasajero>();
                                int intNumeroDeNinos = Convert.ToInt32(ddlMultiNinios.SelectedItem.Value);
                                int iPax = 1;

                                for (int p = 0; p < intNumeroDeNinos; p++)
                                {
                                    ddlEdadesNinios = PanelEdadesNinos.FindControl("ddlMultiEdad" + (p + 1)) as DropDownList;
                                    if (ddlEdadesNinios != null)
                                    {
                                        bool bPax = true;
                                        string sEdad = ddlEdadesNinios.SelectedValue.ToString();
                                        if (sEdad.Length.Equals(1))
                                            sEdad = "0" + sEdad;
                                        if (lvPasajeroNino.Count > 0)
                                        {
                                            for (int j = 0; j < lvPasajeroNino.Count; j++)
                                            {
                                                if (lvPasajeroNino[j].SEdad.Equals(sEdad))
                                                {
                                                    iPax = int.Parse(lvPasajeroNino[j].SCantidad.ToString());
                                                    iPax++;
                                                    lvPasajeroNino[j].SCantidad = iPax.ToString();
                                                    lvPasajeroNino[j].SDetalle = "Niño";
                                                    lvPasajeroNino[j].SCodeGen = "C" + sEdad;
                                                    bPax = false;
                                                }
                                            }
                                        }
                                        if (bPax)
                                        {
                                            VO_ClasificaPasajero vPasajeroNino = new VO_ClasificaPasajero(dtData.Rows[i]["strValor"].ToString().ToUpper(), iPax.ToString(), sEdad);
                                            vPasajeroNino.SDetalle = "Niño";
                                            vPasajeroNino.SCodeGen = "C" + sEdad;
                                            lvPasajeroNino.Add(vPasajeroNino);
                                        }
                                        lsEdadesNinio.Add(ddlEdadesNinios.SelectedValue);
                                    }
                                }
                                vo_Pasajero.LvPasajeroNino = lvPasajeroNino;
                                lvo_Pasajeros.Add(vo_Pasajero);
                                vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios = lsEdadesNinio;
                                bPasa = false;
                                break;
                            }
                        }
                        if (bPasa)
                        {
                            iCantPax += int.Parse(sCnn);
                        }
                    }
                    bPasa = true;
                    iCantPax += int.Parse(sAdt);
                    if (!iCantPax.Equals(0))
                    {
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            if (dtData.Rows[i]["strRefere"].ToString().ToUpper().Equals("ADT"))
                            {
                                vo_Pasajero = new VO_Pasajero(dtData.Rows[i]["strValor"].ToString().ToUpper(), iCantPax.ToString());
                                vo_Pasajero.SDetalle = "Adulto";
                                vo_Pasajero.SCodeGen = "ADT";
                                lvo_Pasajeros.Add(vo_Pasajero);
                                bPasa = false;
                                break;
                            }
                        }
                        if (bPasa)
                        {
                            vo_Pasajero = new VO_Pasajero("ADT", iCantPax.ToString());
                            vo_Pasajero.SDetalle = "Adulto";
                            vo_Pasajero.SCodeGen = "ADT";
                            lvo_Pasajeros.Add(vo_Pasajero);
                        }
                    }
                    bSigue = false;
                    try
                    {
                        DataView dtvAir = new DataView(dtData);
                        //string[] slAir = new string[2];
                        //slAir[0] = "strOperador";
                        //slAir[1] = "strDetalleOperador";

                        DataTable dtDataAir = dtvAir.ToTable(true, "strOperador");
                        List<string> lsAerolineas = new List<string>();
                        foreach (DataRow drDataAir in dtDataAir.Rows)
                        {
                            lsAerolineas.Add(drDataAir["strOperador"].ToString());
                        }
                        vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = lsAerolineas;
                        if (!dtData.Rows[0]["strPseudo"].ToString().Length.Equals(0))
                        {
                            vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda = Enum_OrigenBusqueda.Planes;
                            vo_OTA_AirLowFareSearchLLSRQ.SPseudoPlanes = dtData.Rows[0]["strPseudo"].ToString();
                        }
                      
                    }
                    catch { }

                }
                else
                {
                    if (clsValidaciones.getValidarString(vo_OTA_AirLowFareSearchLLSRQ.SCodPaxNegociada))
                    {
                        int iPax = 0;
                        try
                        {
                            iPax = int.Parse(sAdt);
                        }
                        catch { }
                        if (!sCnn.Equals("0"))
                        {
                            try
                            {
                                iPax += int.Parse(sCnn);
                            }
                            catch { }
                        }
                        vo_Pasajero =
                        new VO_Pasajero(vo_OTA_AirLowFareSearchLLSRQ.SCodPaxNegociada,
                            iPax.ToString());
                        vo_Pasajero.SDetalle = "Adulto";
                        vo_Pasajero.SCodeGen = "ADT";
                        lvo_Pasajeros.Add(vo_Pasajero);

                        if (!sInf.Equals("0"))
                        {
                            string sNumBebes = sInf;

                            vo_Pasajero =
                               new VO_Pasajero("INF",
                                  sNumBebes);
                            vo_Pasajero.SDetalle = "Infante";
                            vo_Pasajero.SCodeGen = "INF";

                            int iNumBebes = 0;
                            int.TryParse(sNumBebes, out iNumBebes);
                            List<string> lsEdadesBebes = new List<string>();
                            List<VO_ClasificaPasajero> lvPasajeroInf = new List<VO_ClasificaPasajero>();
                            int intNumeroDeInfantes = Convert.ToInt32(ddlMultiBebes.SelectedItem.Value);

                            for (int p = 0; p < intNumeroDeInfantes; p++)
                            {
                                ddlEdadesBebe = PanelEdadesInfantes.FindControl("ddlMultiMeses" + (p + 1)) as DropDownList;

                                if (ddlEdadesBebe != null)
                                {
                                    bool bPax = true;
                                    string sEdad = ddlEdadesBebe.SelectedValue.ToString();
                                    if (sEdad.Length.Equals(1))
                                        sEdad = "0" + sEdad;
                                    iPax = 1;
                                    if (lvPasajeroInf.Count > 0)
                                    {
                                        for (int j = 0; j < lvPasajeroInf.Count; j++)
                                        {
                                            if (lvPasajeroInf[j].SEdad.Equals(sEdad))
                                            {
                                                iPax = int.Parse(lvPasajeroInf[j].SCantidad.ToString());
                                                iPax++;
                                                lvPasajeroInf[j].SCantidad = iPax.ToString();
                                                lvPasajeroInf[j].SDetalle = "Infante";
                                                lvPasajeroInf[j].SCodeGen = "INF";
                                                bPax = false;
                                            }
                                        }
                                    }
                                    if (bPax)
                                    {
                                        VO_ClasificaPasajero vPasajeroInf = new VO_ClasificaPasajero("INF", iPax.ToString(), sEdad);
                                        vPasajeroInf.SDetalle = "Infante";
                                        vPasajeroInf.SCodeGen = "INF";
                                        lvPasajeroInf.Add(vPasajeroInf);
                                    }
                                    lsEdadesBebes.Add(ddlEdadesBebe.SelectedValue);
                                }
                            }
                            vo_Pasajero.LvPasajeroInfante = lvPasajeroInf;
                            lvo_Pasajeros.Add(vo_Pasajero);
                            vo_OTA_AirLowFareSearchLLSRQ.LsEdadesInfantes = lsEdadesBebes;
                        }
                        bSigue = false;
                    }
                }
            }
            if (bSigue)
            {
                if (!sInf.Equals("0"))
                {
                    string sNumBebes = sInf;

                    vo_Pasajero =
                       new VO_Pasajero("INF",
                          sNumBebes);
                    vo_Pasajero.SDetalle = "Infante";
                    vo_Pasajero.SCodeGen = "INF";

                    int iNumBebes = 0;
                    int.TryParse(sNumBebes, out iNumBebes);
                    List<string> lsEdadesBebes = new List<string>();
                    List<VO_ClasificaPasajero> lvPasajeroInf = new List<VO_ClasificaPasajero>();
                    int intNumeroDeInfantes = Convert.ToInt32(ddlMultiBebes.SelectedItem.Value);

                    for (int p = 0; p < intNumeroDeInfantes; p++)
                    {
                        ddlEdadesBebe = PanelEdadesInfantes.FindControl("ddlMultiMeses" + (p + 1)) as DropDownList;

                        if (ddlEdadesBebe != null)
                        {
                            bool bPax = true;
                            string sEdad = ddlEdadesBebe.SelectedValue.ToString();
                            if (sEdad.Length.Equals(1))
                                sEdad = "0" + sEdad;
                            int iPax = 1;
                            if (lvPasajeroInf.Count > 0)
                            {
                                for (int j = 0; j < lvPasajeroInf.Count; j++)
                                {
                                    if (lvPasajeroInf[j].SEdad.Equals(sEdad))
                                    {
                                        iPax = int.Parse(lvPasajeroInf[j].SCantidad.ToString());
                                        iPax++;
                                        lvPasajeroInf[j].SCantidad = iPax.ToString();
                                        lvPasajeroInf[j].SDetalle = "Infante";
                                        lvPasajeroInf[j].SCodeGen = "INF";
                                        bPax = false;
                                    }
                                }
                            }
                            if (bPax)
                            {
                                VO_ClasificaPasajero vPasajeroInf = new VO_ClasificaPasajero("INF", iPax.ToString(), sEdad);
                                vPasajeroInf.SDetalle = "Infante";
                                vPasajeroInf.SCodeGen = "INF";
                                lvPasajeroInf.Add(vPasajeroInf);
                            }
                            lsEdadesBebes.Add(ddlEdadesBebe.SelectedValue);
                        }
                    }
                    vo_Pasajero.LvPasajeroInfante = lvPasajeroInf;
                    lvo_Pasajeros.Add(vo_Pasajero);
                    vo_OTA_AirLowFareSearchLLSRQ.LsEdadesInfantes = lsEdadesBebes;
                }
                if (!sCnn.Equals("0"))
                {
                    string sNumNinios = sCnn;

                    vo_Pasajero =
                       new VO_Pasajero("CNN",
                           sNumNinios);
                    vo_Pasajero.SDetalle = "Niño";
                    vo_Pasajero.SCodeGen = "CNN";

                    int iNumNinios = 0;
                    int.TryParse(sNumNinios, out iNumNinios);
                    List<string> lsEdadesNinio = new List<string>();
                    List<VO_ClasificaPasajero> lvPasajeroNino = new List<VO_ClasificaPasajero>();
                    int intNumeroDeNinos = Convert.ToInt32(ddlMultiNinios.SelectedItem.Value);

                    for (int p = 0; p < intNumeroDeNinos; p++)
                    {
                        ddlEdadesNinios = PanelEdadesNinos.FindControl("ddlMultiEdad" + (p + 1)) as DropDownList;
                        if (ddlEdadesNinios != null)
                        {
                            bool bPax = true;
                            string sEdad = ddlEdadesNinios.SelectedValue.ToString();
                            if (sEdad.Length.Equals(1))
                                sEdad = "0" + sEdad;
                            if (lvPasajeroNino.Count > 0)
                            {
                                for (int j = 0; j < lvPasajeroNino.Count; j++)
                                {
                                    if (lvPasajeroNino[j].SEdad.Equals(sEdad))
                                    {
                                        int iPax = int.Parse(lvPasajeroNino[j].SCantidad.ToString());
                                        iPax++;
                                        lvPasajeroNino[j].SCantidad = iPax.ToString();
                                        lvPasajeroNino[j].SDetalle = "Niño";
                                        lvPasajeroNino[j].SCodeGen = "C" + sEdad;
                                        bPax = false;
                                    }
                                }
                            }
                            if (bPax)
                            {
                                VO_ClasificaPasajero vPasajeroNino = new VO_ClasificaPasajero("C" + sEdad, "1", sEdad);
                                vPasajeroNino.SDetalle = "Niño";
                                vPasajeroNino.SCodeGen = "C" + sEdad;
                                lvPasajeroNino.Add(vPasajeroNino);
                            }
                            lsEdadesNinio.Add(ddlEdadesNinios.SelectedValue);
                        }
                    }
                    vo_Pasajero.LvPasajeroNino = lvPasajeroNino;
                    lvo_Pasajeros.Add(vo_Pasajero);
                    vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios = lsEdadesNinio;
                }
                vo_Pasajero = new VO_Pasajero("ADT", sAdt);
                vo_Pasajero.SDetalle = "Adulto";
                vo_Pasajero.SCodeGen = "ADT";
                lvo_Pasajeros.Add(vo_Pasajero);
            }
            if (lvo_Pasajeros.Count > 0)
            {
                List<VO_Pasajero> lvo_PasajerosTemp = new List<VO_Pasajero>();
                List<VO_Pasajero> lvo_PasajerosTempRecorre = lvo_Pasajeros;
                int iCant = lvo_PasajerosTempRecorre.Count;
                iCant--;
                for (int p = iCant; p >= 0; p--)
                {
                    VO_Pasajero v_Pax = new VO_Pasajero();
                    int iPax = 0;
                    bool bSuma = true;
                    for (int h = iCant; h >= 0; h--)
                    {
                        if (lvo_PasajerosTempRecorre[h].SCodigo.Equals(lvo_Pasajeros[p].SCodigo))
                        {
                            v_Pax.SCodigo = lvo_PasajerosTempRecorre[h].SCodigo;
                            v_Pax.SDetalle = lvo_PasajerosTempRecorre[h].SDetalle;
                            v_Pax.SCodeGen = lvo_PasajerosTempRecorre[h].SCodeGen;
                            iPax += int.Parse(lvo_PasajerosTempRecorre[h].SCantidad);
                            v_Pax.SCantidad = iPax.ToString();
                            if (lvo_PasajerosTempRecorre[h].LvPasajeroInfante != null)
                            {
                                v_Pax.LvPasajeroInfante = lvo_PasajerosTempRecorre[h].LvPasajeroInfante;
                            }
                            if (lvo_PasajerosTempRecorre[h].LvPasajeroNino != null)
                            {
                                v_Pax.LvPasajeroNino = lvo_PasajerosTempRecorre[h].LvPasajeroNino;
                            }
                        }
                    }
                    if (lvo_PasajerosTemp.Count > 0)
                    {
                        foreach (VO_Pasajero vo_PasajerosT in lvo_PasajerosTemp)
                        {
                            if (vo_PasajerosT.SCodigo.Equals(v_Pax.SCodigo))
                            {
                                bSuma = false;
                                break;
                            }
                        }
                    }
                    if (bSuma)
                    {
                        lvo_PasajerosTemp.Add(v_Pax);
                    }
                }
                //foreach (VO_Pasajero vo_PasajerosTempRecorre in lvo_PasajerosTempRecorre)
                //{
                //    if (vo_PasajerosTempRecorre.SCodigo.Equals())
                //    {
                //    }
                //}
                lvo_Pasajeros = lvo_PasajerosTemp;
            }
            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros = lvo_Pasajeros;
        }
        public void setProcesarPseudo(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
            try
            {
                DataSet dsData = new DataSet();
                if (vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda.Equals(Enum_OrigenBusqueda.Planes) &&
                    vo_OTA_AirLowFareSearchLLSRQ.SPseudoPlanes != "")
                {
                    csReferencias.csActualizaPcc(vo_OTA_AirLowFareSearchLLSRQ.SPseudoPlanes);
                }
                else
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo.Equals(Enum_TipoVuelo.Nacional))
                    {
                     
                        string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");
                        string sPaises = clsValidaciones.GetKeyOrAdd("Paises", "Pais");
                        string pais = new CsConsultasVuelos().ConsultaCodigo(sPaisDefault,"TBLPAIS","INTCODE","STRCOUNTRYCODE");
                        if (pais !="")
                        {
                            string strCodigoCOL = pais;
                            csReferencias.csActualizaPcc(strCodigoCOL, false);
                        }
                    }
                    else
                    {
                        csVuelos clsValidacionesVuelos = new csVuelos();
                        string sPais = clsValidacionesVuelos.getValidarRuta(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas);
                        if (sPais.Length.Equals(0))
                        {
                            sPais = clsValidacionesVuelos.getValidarPais(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas);
                        }
                        if (!sPais.Length.Equals(0))
                        {
                            csReferencias.csActualizaPcc(sPais, false);
                        }
                        else
                        {
                            csReferencias.csActualizaPcc(sPais, true);
                        }
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// Metodo que procesa la busqueda de vuelos
        /// </summary>
        /// <param name="PageSource">UserControl</param>
        /// <remarks>
        /// Autor:      Jose Faustino Posas
        /// Fecha: 
        /// Company:    Ssoft Colombia
        /// -------------------
        /// Control de Cambios
        /// -------------------        
        /// Descripcion:    se adiciona validacion para redireccion a resultado de ofertas o normal
        /// Fecha:          2012-04-17      
        /// Responsable:    Camilo Diaz 
        /// </remarks>
        public void setProcesarBusqueda(UserControl PageSource)
        {
            try
            {
                clsParametros cParametros = new clsParametros();
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                    if (setValidaPaxAir(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros))
                    {
                        Negocios_WebServiceSession._CerrarSesion();
                        string sResultado = "K"; Negocios_WebServiceSabreCommand._EjecutarComando("I");
                        if (sResultado != null)
                        {
                            //cCache.VoOtaAirLowFareSearchLLSRQ = vo_OTA_AirLowFareSearchLLSRQ;
                            //clsCache cCache = new csCache().cCache();

                            if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Count > 0)
                            {
                                cCache.AeropuertoOrigen = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen;
                                cCache.AeropuertoDestino = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoDestino;
                            }
                            cCache.TipoVuelo = vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo;
                            csCache.ActualizarCache(cCache);
                            //= getCadenaCotizacionServicios(clsValidaciones.GetKeyOrAdd("TemaMsjCotVuelos", "COTAIR"));
                            string sCadenaCotizacion = clsValidaciones.GetKeyOrAdd("TemaMsjCotVuelos", "COTAIR");
                            Guardar_Edades_Session_Vuelos(PageSource);
                            if (vo_OTA_AirLowFareSearchLLSRQ.BHoras)
                            {


                                string sVuelosXhora = clsValidaciones.GetKeyOrAdd("sVueloXhoras", "False");
                                if (sVuelosXhora.ToUpper().Equals("TRUE"))
                                {
                                    clsValidaciones.RedirectPagina("ResultadoVuelosHoras.aspx?" + sCadenaCotizacion, true);
                                }
                                else
                                {
                                    if (vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda.Equals(Enum_OrigenBusqueda.OfertasFijas))
                                    {
                                        clsValidaciones.RedirectPagina("ResultadoVuelosOfertas.aspx?" + sCadenaCotizacion, true);
                                    }
                                    else
                                    {
                                        if (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto.Equals(Enum_TipoTrayecto.Multidestino))
                                            clsValidaciones.RedirectPagina("ResultadoVuelosMulti.aspx?" + sCadenaCotizacion, true);
                                        else
                                            clsValidaciones.RedirectPagina("ResultadoVuelos.aspx?" + sCadenaCotizacion, false);
                                    }
                                }
                            }
                            else
                            {
                                //                            PageSource.Response.Redirect("ResultadoVuelos.aspx", true);
                                if (vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda.Equals(Enum_OrigenBusqueda.OfertasFijas))
                                {
                                    clsValidaciones.RedirectPagina("ResultadoVuelosOfertas.aspx?" + sCadenaCotizacion, true);
                                }
                                else
                                {    //hceron resultado multidestino
                                    //23052013
                                    if (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto.Equals(Enum_TipoTrayecto.Multidestino))
                                        clsValidaciones.RedirectPagina("ResultadoVuelosMulti.aspx?" + sCadenaCotizacion, true);
                                    else
                                        clsValidaciones.RedirectPagina("ResultadoVuelos.aspx?" + sCadenaCotizacion, false);
                                }
                            }
                        }
                        else
                        {
                            cParametros.Id = 0;
                            cParametros.Tipo = clsTipoError.WebServices;
                            cParametros.Severity = clsSeveridad.Alta;
                            cParametros.Message = "Error al intentar abrir la sesion de sabre";
                            cParametros.Complemento = "Busqueda ";
                            cParametros.ValidaInfo = false;
                            cParametros.ErrorConfigura[0] = cCache.Empresa;
                            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
                            cParametros.ViewMessage.Add("Su sesion ha terminado");
                            cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                            ExceptionHandled.Publicar(cParametros);
                            clsSesiones.setParametrosError(cParametros);
                            clsValidaciones.RedirectPagina("ErrorBusqueda.aspx", true);
                        }
                    }
                    else
                    {
                        string sMaxPax = clsValidaciones.GetKeyOrAdd("iMaxPaxAir", "8");
                        cParametros.Id = 0;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Severity = clsSeveridad.Alta;
                        cParametros.Message = "Maximo de pasajeros por reserva";
                        cParametros.Code = "103";
                        cParametros.ValidaInfo = false;
                        cParametros.ErrorConfigura[0] = cCache.Empresa;
                        cParametros.ErrorConfigura[2] = sMaxPax;
                        cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
                        cParametros.Complemento = "Busqueda ";
                        cParametros.ViewMessage.Add("Solo se permite un maximo de " + sMaxPax + " pasajeros por solicitud");
                        cParametros.Sugerencia.Add("Pongase en contacto con nuestro asesor");
                        ExceptionHandled.Publicar(cParametros);
                        clsSesiones.setParametrosError(cParametros);
                        clsValidaciones.RedirectPagina("ErrorBusqueda.aspx", true);
                    }
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
        //private void getPasajerosEdadesMeses(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ, UserControl PageSource, string stdIDEdad, string stdIDMeses)
        //{
        //    DropDownList ddlMultiAdultos = (DropDownList)PageSource.FindControl("ddlMultiAdultos");
        //    DropDownList ddlMultiNinios = (DropDownList)PageSource.FindControl("ddlMultiNinios");
        //    DropDownList ddlMultiBebes = (DropDownList)PageSource.FindControl("ddlMultiBebes");
        //    Panel PanelEdadesNinos = (Panel)PageSource.FindControl("PanelEdadesNinos");
        //    Panel PanelEdadesInfantes = (Panel)PageSource.FindControl("PanelEdadesInfantes");

        //    /*ASIGNAR A VARIABLES*/
        //    DropDownList ddlEdadesNinios = new DropDownList();
        //    DropDownList ddlEdadesBebe = new DropDownList();

        //    VO_Pasajero vo_Pasajero = null;
        //    List<VO_Pasajero> lvo_Pasajeros = new List<VO_Pasajero>();

        //    if (!ddlMultiAdultos.SelectedValue.Equals("0"))
        //    {
        //        vo_Pasajero =
        //        new VO_Pasajero("ADT",
        //            ddlMultiAdultos.SelectedValue);
        //        lvo_Pasajeros.Add(vo_Pasajero);
        //    }

        //    if (!ddlMultiNinios.SelectedValue.Equals("0"))
        //    {
        //        string sNumNinios = ddlMultiNinios.SelectedValue;

        //        vo_Pasajero =
        //           new VO_Pasajero("CNN",
        //               sNumNinios);
        //        lvo_Pasajeros.Add(vo_Pasajero);

        //        int iNumNinios = 0;
        //        int.TryParse(sNumNinios, out iNumNinios);
        //        List<string> lsEdadesNinio = new List<string>();

        //        int intNumeroDeNinos = Convert.ToInt32(ddlMultiNinios.SelectedItem.Value);

        //        for (int p = 0; p < intNumeroDeNinos; p++)
        //        {
        //            ddlEdadesNinios = PanelEdadesNinos.FindControl("ddlMultiEdad" + (p + 1)) as DropDownList;
        //            if (ddlEdadesNinios != null)
        //            {
        //                lsEdadesNinio.Add(ddlEdadesNinios.SelectedValue);
        //            }
        //        }
        //        vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios = lsEdadesNinio;
        //    }

        //    if (!ddlMultiBebes.SelectedValue.Equals("0"))
        //    {
        //        string sNumBebes = ddlMultiBebes.SelectedValue;

        //        vo_Pasajero =
        //           new VO_Pasajero("INF",
        //              sNumBebes);
        //        lvo_Pasajeros.Add(vo_Pasajero);

        //        int iNumBebes = 0;
        //        int.TryParse(sNumBebes, out iNumBebes);
        //        List<string> lsEdadesBebes = new List<string>();
        //        int intNumeroDeInfantes = Convert.ToInt32(ddlMultiBebes.SelectedItem.Value);

        //        for (int p = 0; p < intNumeroDeInfantes; p++)
        //        {
        //            ddlEdadesBebe = PanelEdadesInfantes.FindControl("ddlMultiMeses" + (p + 1)) as DropDownList;

        //            if (ddlEdadesBebe != null)
        //            {
        //                lsEdadesBebes.Add(ddlEdadesBebe.SelectedValue);
        //            }
        //        }

        //        vo_OTA_AirLowFareSearchLLSRQ.LsEdadesInfantes = lsEdadesBebes;
        //    }

        //    vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros = lvo_Pasajeros;
        //}
        private void getPasajerosEdadesMeses(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ, UserControl PageSource, string stdIDEdad, string stdIDMeses)
        {
            DropDownList ddlMultiAdultos = (DropDownList)PageSource.FindControl("ddlMultiAdultos");
            DropDownList ddlMultiNinios = (DropDownList)PageSource.FindControl("ddlMultiNinios");
            DropDownList ddlMultiBebes = (DropDownList)PageSource.FindControl("ddlMultiBebes");
            Panel PanelEdadesNinos = (Panel)PageSource.FindControl("PanelEdadesNinos");
            Panel PanelEdadesInfantes = (Panel)PageSource.FindControl("PanelEdadesInfantes");

            /*ASIGNAR A VARIABLES*/
            DropDownList ddlEdadesNinios = new DropDownList();
            DropDownList ddlEdadesBebe = new DropDownList();

            VO_Pasajero vo_Pasajero = null;
            List<VO_Pasajero> lvo_Pasajeros = new List<VO_Pasajero>();
            if (clsValidaciones.getValidarString(vo_OTA_AirLowFareSearchLLSRQ.SCodPaxNegociada))
            {
                int iCantPax = 0;
                if (!ddlMultiAdultos.SelectedValue.Equals("0"))
                {
                    iCantPax += int.Parse(ddlMultiAdultos.SelectedValue.ToString());
                }

                if (!ddlMultiNinios.SelectedValue.Equals("0"))
                {
                    iCantPax += int.Parse(ddlMultiNinios.SelectedValue.ToString());
                }

                if (!ddlMultiBebes.SelectedValue.Equals("0"))
                {
                    iCantPax += int.Parse(ddlMultiNinios.SelectedValue.ToString());
                }
                vo_Pasajero = new VO_Pasajero(vo_OTA_AirLowFareSearchLLSRQ.SCodPaxNegociada, iCantPax.ToString());
                lvo_Pasajeros.Add(vo_Pasajero);
            }
            else
            {
                if (!ddlMultiAdultos.SelectedValue.Equals("0"))
                {
                    string sPax = "ADT";
                    if (clsValidaciones.GetKeyOrAdd("TT_Pax", "False").ToUpper().Equals("TRUE"))
                        sPax = clsValidaciones.GetKeyOrAdd("TT_TipoPax", "WEB");

                    vo_Pasajero =
                    new VO_Pasajero(sPax,
                        ddlMultiAdultos.SelectedValue);
                    vo_Pasajero.SDetalle = "Adulto";
                    vo_Pasajero.SCodeGen = sPax;
                    lvo_Pasajeros.Add(vo_Pasajero);
                }

                if (!ddlMultiNinios.SelectedValue.Equals("0"))
                {
                    string sNumNinios = ddlMultiNinios.SelectedValue;

                    vo_Pasajero =
                       new VO_Pasajero("CNN",
                           sNumNinios);
                    vo_Pasajero.SDetalle = "Niño";
                    vo_Pasajero.SCodeGen = "CNN";

                    int iNumNinios = 0;
                    int.TryParse(sNumNinios, out iNumNinios);
                    List<string> lsEdadesNinio = new List<string>();
                    List<VO_ClasificaPasajero> lvPasajeroNino = new List<VO_ClasificaPasajero>();
                    int intNumeroDeNinos = Convert.ToInt32(ddlMultiNinios.SelectedItem.Value);

                    for (int p = 0; p < intNumeroDeNinos; p++)
                    {
                        ddlEdadesNinios = PanelEdadesNinos.FindControl("ddlMultiEdad" + (p + 1)) as DropDownList;
                        if (ddlEdadesNinios != null)
                        {
                            bool bPax = true;
                            string sEdad = ddlEdadesNinios.SelectedValue.ToString();
                            if (sEdad.Length.Equals(1))
                                sEdad = "0" + sEdad;
                            if (lvPasajeroNino.Count > 0)
                            {
                                for (int j = 0; j < lvPasajeroNino.Count; j++)
                                {
                                    if (lvPasajeroNino[j].SEdad.Equals(sEdad))
                                    {
                                        int iPax = int.Parse(lvPasajeroNino[j].SCantidad.ToString());
                                        iPax++;
                                        lvPasajeroNino[j].SCantidad = iPax.ToString();
                                        lvPasajeroNino[j].SDetalle = "Niño";
                                        lvPasajeroNino[j].SCodeGen = "C" + sEdad;
                                        bPax = false;
                                    }
                                }
                            }
                            if (bPax)
                            {
                                VO_ClasificaPasajero vPasajeroNino = new VO_ClasificaPasajero("C" + sEdad, "1", sEdad);
                                vPasajeroNino.SDetalle = "Niño";
                                vPasajeroNino.SCodeGen = "C" + sEdad;
                                lvPasajeroNino.Add(vPasajeroNino);
                            }
                            lsEdadesNinio.Add(ddlEdadesNinios.SelectedValue);
                        }
                    }
                    vo_Pasajero.LvPasajeroNino = lvPasajeroNino;
                    lvo_Pasajeros.Add(vo_Pasajero);
                    vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios = lsEdadesNinio;
                }

                if (!ddlMultiBebes.SelectedValue.Equals("0"))
                {
                    string sNumBebes = ddlMultiBebes.SelectedValue;

                    vo_Pasajero =
                       new VO_Pasajero("INF",
                          sNumBebes);
                    vo_Pasajero.SDetalle = "Infante";
                    vo_Pasajero.SCodeGen = "INF";

                    int iNumBebes = 0;
                    int.TryParse(sNumBebes, out iNumBebes);
                    List<string> lsEdadesBebes = new List<string>();
                    List<VO_ClasificaPasajero> lvPasajeroInf = new List<VO_ClasificaPasajero>();
                    int intNumeroDeInfantes = Convert.ToInt32(ddlMultiBebes.SelectedItem.Value);

                    for (int p = 0; p < intNumeroDeInfantes; p++)
                    {
                        ddlEdadesBebe = PanelEdadesInfantes.FindControl("ddlMultiMeses" + (p + 1)) as DropDownList;

                        if (ddlEdadesBebe != null)
                        {
                            bool bPax = true;
                            string sEdad = ddlEdadesBebe.SelectedValue.ToString();
                            if (sEdad.Length.Equals(1))
                                sEdad = "0" + sEdad;
                            int iPax = 1;
                            if (lvPasajeroInf.Count > 0)
                            {
                                for (int j = 0; j < lvPasajeroInf.Count; j++)
                                {
                                    if (lvPasajeroInf[j].SEdad.Equals(sEdad))
                                    {
                                        iPax = int.Parse(lvPasajeroInf[j].SCantidad.ToString());
                                        iPax++;
                                        lvPasajeroInf[j].SCantidad = iPax.ToString();
                                        lvPasajeroInf[j].SDetalle = "Infante";
                                        lvPasajeroInf[j].SCodeGen = "INF";
                                        bPax = false;
                                    }
                                }
                            }
                            if (bPax)
                            {
                                VO_ClasificaPasajero vPasajeroInf = new VO_ClasificaPasajero("INF", iPax.ToString(), sEdad);
                                vPasajeroInf.SDetalle = "Infante";
                                vPasajeroInf.SCodeGen = "INF";
                                lvPasajeroInf.Add(vPasajeroInf);
                            }
                            lsEdadesBebes.Add(ddlEdadesBebe.SelectedValue);
                        }
                    }
                    vo_Pasajero.LvPasajeroInfante = lvPasajeroInf;
                    lvo_Pasajeros.Add(vo_Pasajero);
                    vo_OTA_AirLowFareSearchLLSRQ.LsEdadesInfantes = lsEdadesBebes;
                }
            }
            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros = lvo_Pasajeros;

            if (clsValidaciones.GetKeyOrAdd("TT_Convenio", "False").ToUpper().Equals("TRUE"))
                vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada = clsValidaciones.GetKeyOrAdd("TT_CodigoConvenio", "COV013");

        }
        private bool ValidarVuelos(Enum_TipoTrayecto eTipoTrayecto, UserControl PageSource)
        {
            bool Resp = ValidarCondiciones(PageSource);
            string sOrigen = string.Empty;
            string sDestino = string.Empty;
            if (Resp)
            {
                switch (eTipoTrayecto)
                {
                    case Enum_TipoTrayecto.Ida:
                        Resp = ValidarIda(PageSource);
                        break;
                    case Enum_TipoTrayecto.IdaRegreso:
                        Resp = ValidarIdaRegreso(PageSource) && ValidarFechas(PageSource);
                        break;
                    case Enum_TipoTrayecto.Multidestino:
                        Resp = ValidarMultiDestino(PageSource);
                        break;
                }
            }
            return Resp;
        }
        private bool ValidarCondiciones(UserControl PageSource)
        {
            bool Resp = true;
            CheckBox chkCondiciones = (CheckBox)PageSource.FindControl("chkCondiciones");
            Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");

            if (lblErrorGen != null)
                lblErrorGen.Text = "";

            if (chkCondiciones != null)
            {
                if (chkCondiciones.Checked == false)
                {
                    Resp = false;
                    if (lblErrorGen != null)
                        lblErrorGen.Text = "Debe aceptar los términos y condiciones";
                }
            }
            return Resp;
        }
        private bool ValidarIda(UserControl PageSource)
        {
            bool Resp = true;
            try
            {
                TextBox txt_Multi_D1 = (TextBox)PageSource.FindControl("txt_Multi_D1");
                TextBox txt_Multi_O1 = (TextBox)PageSource.FindControl("txt_Multi_O1");
                TextBox txtFechaMultiO1 = (TextBox)PageSource.FindControl("txtFechaMultiO1");
                Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");

                /*CAJAS DE TEXTO*/
                TextBox txtIdaDestino = txt_Multi_D1;
                TextBox txtIdaOrigen = txt_Multi_O1;
                TextBox txtIdaFechaSalida = txtFechaMultiO1;
                /*LABELS*/
                Label lblErrorSoloIda = new Label();
                Label lblIdaOrigen = new Label();
                Label lblIdaOrigenE = new Label();
                Label lblIdaDestino = new Label();
                Label lblIdaDestinoE = new Label();
                Label lblIdaFechaSalida = new Label();
                Label lblIdaFechaSalidaE = new Label();
                /*ASIGNACION*/
                lblIdaOrigen.Text = "Origen";
                lblIdaDestino.Text = "Destino";
                lblIdaFechaSalida.Text = "Fecha Salida";
                /*ASIGNACION DEL LABEL*/
                lblErrorGen.Text = String.Empty;
                lblErrorSoloIda = lblErrorGen;

                if (txtIdaOrigen.Text.Length.Equals(0))
                {
                    Resp = false;
                    if (lblErrorSoloIda.Text.Length.Equals(0))
                    {
                        lblErrorSoloIda.Text = "Complete: " + lblIdaOrigen.Text;
                    }
                    else
                    {
                        lblErrorSoloIda.Text += ", " + lblIdaOrigen.Text;
                    }
                    lblIdaOrigenE.Text = " *";
                }
                else
                {
                    Resp = setValidaAirport(txtIdaOrigen.Text);
                    if (!Resp)
                    {
                        if (lblErrorSoloIda.Text.Length.Equals(0))
                        {
                            lblErrorSoloIda.Text = lblIdaOrigen.Text + " No válido, seleccione una opcion de las sugeridas";
                        }
                        else
                        {
                            lblErrorSoloIda.Text += ", " + lblIdaOrigen.Text + " No válido, seleccione una opcion de las sugeridas";
                        }
                        lblIdaOrigenE.Text = " *";
                    }
                }

                if (txtIdaDestino.Text.Length.Equals(0))
                {
                    Resp = false;
                    if (lblErrorSoloIda.Text.Length.Equals(0))
                    {
                        lblErrorSoloIda.Text = "Complete: " + lblIdaDestino.Text;
                    }
                    else
                    {
                        lblErrorSoloIda.Text += ", " + lblIdaDestino.Text;
                    }
                    lblIdaDestinoE.Text = " *";
                }
                else
                {
                    if (Resp)
                    {
                        Resp = setValidaAirport(txtIdaDestino.Text);
                        if (!Resp)
                        {
                            if (lblErrorSoloIda.Text.Length.Equals(0))
                            {
                                lblErrorSoloIda.Text = lblIdaDestino.Text + " No válido, seleccione una opcion de las sugeridas";
                            }
                            else
                            {
                                lblErrorSoloIda.Text += ", " + lblIdaDestino.Text + " No válido, seleccione una opcion de las sugeridas";
                            }
                            lblIdaDestinoE.Text = " *";
                        }
                    }
                }

                if (txtIdaFechaSalida.Text.Length.Equals(0))
                {
                    Resp = false;
                    if (lblErrorSoloIda.Text.Length.Equals(0))
                    {
                        lblErrorSoloIda.Text = "Complete: " + lblIdaFechaSalida.Text;
                    }
                    else
                    {
                        lblErrorSoloIda.Text += ", " + lblIdaFechaSalida.Text;
                    }
                    lblIdaFechaSalidaE.Text = " *";
                }
                if (Resp)
                {
                    Resp = setValidaDestinos(txtIdaOrigen.Text, txtIdaDestino.Text);
                    if (!Resp)
                    {
                        lblErrorSoloIda.Text = "El destino y Origen no pueden ser iguales";
                        lblIdaDestinoE.Text = " *";
                        lblIdaOrigenE.Text = " *";
                    }
                }
            }
            catch { }
            return Resp;
        }
        private bool ValidarIdaRegreso(UserControl PageSource)
        {
            bool Resp = true;
            try
            {
                TextBox txt_Multi_D1 = (TextBox)PageSource.FindControl("txt_Multi_D1");
                TextBox txt_Multi_O1 = (TextBox)PageSource.FindControl("txt_Multi_O1");
                TextBox txtFechaMultiO1 = (TextBox)PageSource.FindControl("txtFechaMultiO1");
                TextBox txtFechaMulti2 = (TextBox)PageSource.FindControl("txt2VFechaMulti");
                Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");
                DropDownList ddlCiudadOrigenOferta = (DropDownList)PageSource.FindControl("ddlCiudadOrigenOferta");

                /*LABELS DE TITULOS*/
                Label lblIdaRegresoFechaSalida = new Label();
                Label lblIdaRegresoSalida = new Label();
                Label lblIdaRegresoFechaRegreso = new Label();
                Label lblErrorIdayRegreso = new Label();
                Label lblIdaRegresoDestino = new Label();
                /*ASIGNACION DE VALORES*/
                lblIdaRegresoSalida.Text = "Origen";
                lblIdaRegresoDestino.Text = "Destino";
                lblIdaRegresoFechaSalida.Text = "Fecha salida";
                lblIdaRegresoFechaRegreso.Text = "Fecha regreso";

                /*LABELS DE ERRORES*/
                Label lblIdaRegresoSalidaE = new Label();
                Label lblIdaRegresoDestinoE = new Label();
                Label lblIdaRegresoFechaRegresoE = new Label();
                Label lblIdaRegresoFechaSalidaE = new Label();
                /*ASIGNACION DEL LABEL*/
                lblErrorGen.Text = String.Empty;
                lblErrorIdayRegreso = lblErrorGen;
                /*CAJAS DE TEXTO*/
                TextBox txtIdaRegresoDestino = txt_Multi_D1;
                TextBox txtIdaRegresoSalida = new TextBox();
                if (txt_Multi_O1 != null)
                    txtIdaRegresoSalida.Text = txt_Multi_O1.Text;
                else
                    if (ddlCiudadOrigenOferta != null)
                        txtIdaRegresoSalida.Text = ddlCiudadOrigenOferta.SelectedItem.Text;

                TextBox txtIdaRegresoFechaSalida = txtFechaMultiO1;
                TextBox txtIdaRegresoFechaRegreso = txtFechaMulti2;

                if (txtIdaRegresoSalida.Text.Length.Equals(0))
                {
                    Resp = false;
                    if (lblErrorIdayRegreso.Text.Length.Equals(0))
                    {
                        lblErrorIdayRegreso.Text = "Complete: " + lblIdaRegresoSalida.Text;
                    }
                    else
                    {
                        lblErrorIdayRegreso.Text += ", " + lblIdaRegresoSalida.Text;
                    }
                    lblIdaRegresoSalidaE.Text = " *";
                }
                else
                {
                    Resp = setValidaAirport(txtIdaRegresoSalida.Text);
                    if (!Resp)
                    {
                        if (lblErrorIdayRegreso.Text.Length.Equals(0))
                        {
                            lblErrorIdayRegreso.Text = lblIdaRegresoSalida.Text + " No válido, seleccione una opcion de las sugeridas";
                        }
                        else
                        {
                            lblErrorIdayRegreso.Text += ", " + lblIdaRegresoSalida.Text + " No válido, seleccione una opcion de las sugeridas";
                        }
                        lblIdaRegresoSalidaE.Text = " *";
                    }
                }
                if (txtIdaRegresoDestino.Text.Length.Equals(0))
                {
                    Resp = false;
                    if (lblErrorIdayRegreso.Text.Length.Equals(0))
                    {
                        lblErrorIdayRegreso.Text = "Complete: " + lblIdaRegresoDestino.Text;
                    }
                    else
                    {
                        lblErrorIdayRegreso.Text += ", " + lblIdaRegresoDestino.Text;
                    }
                    lblIdaRegresoDestinoE.Text = " *";
                }
                else
                {
                    if (Resp)
                    {
                        Resp = setValidaAirport(txtIdaRegresoDestino.Text);
                        if (!Resp)
                        {
                            if (lblErrorIdayRegreso.Text.Length.Equals(0))
                            {
                                lblErrorIdayRegreso.Text = lblIdaRegresoDestino.Text + " No válido, seleccione una opcion de las sugeridas";
                            }
                            else
                            {
                                lblErrorIdayRegreso.Text += ", " + lblIdaRegresoDestino.Text + " No válido, seleccione una opcion de las sugeridas";
                            }
                            lblIdaRegresoDestinoE.Text = " *";
                        }
                    }
                }

                if (txtIdaRegresoFechaSalida.Text.Length.Equals(0))
                {
                    Resp = false;
                    if (lblErrorIdayRegreso.Text.Length.Equals(0))
                    {
                        lblErrorIdayRegreso.Text = "Complete: " + lblIdaRegresoFechaSalida.Text;
                    }
                    else
                    {
                        lblErrorIdayRegreso.Text += ", " + lblIdaRegresoFechaSalida.Text;
                    }
                    lblIdaRegresoFechaSalidaE.Text = " *";
                }
                if (txtIdaRegresoFechaRegreso.Text.Length.Equals(0))
                {
                    Resp = false;

                    if (lblErrorIdayRegreso.Text.Length.Equals(0))
                    {
                        lblErrorIdayRegreso.Text = "Complete: " + lblIdaRegresoFechaRegreso.Text;
                    }
                    else
                    {
                        lblErrorIdayRegreso.Text += ", " + lblIdaRegresoFechaRegreso.Text;
                    }
                    lblIdaRegresoFechaRegresoE.Text = " *";
                }
                if (Resp)
                {
                    Resp = setValidaDestinos(txtIdaRegresoSalida.Text, txtIdaRegresoDestino.Text);
                    if (!Resp)
                    {
                        lblErrorIdayRegreso.Text = "El destino y Origen no pueden ser iguales";
                        lblIdaRegresoDestinoE.Text = " *";
                        lblIdaRegresoSalidaE.Text = " *";
                    }
                }
            }
            catch { }
            return Resp;
        }
        private bool ValidarMultiDestino(UserControl PageSource)
        {
            bool Resp = true;
            try
            {
                TextBox txtFechaMultiO1 = (TextBox)PageSource.FindControl("txtFechaMultiO1");
                TextBox txtFechaMultiO2 = (TextBox)PageSource.FindControl("txtFechaMultiO2");
                TextBox txtFechaMultiO3 = (TextBox)PageSource.FindControl("txtFechaMultiO3");
                TextBox txtFechaMultiO4 = (TextBox)PageSource.FindControl("txtFechaMultiO4");
                TextBox txtFechaMultiO5 = (TextBox)PageSource.FindControl("txtFechaMultiO5");
                TextBox txtFechaMultiO6 = (TextBox)PageSource.FindControl("txtFechaMultiO6");

                TextBox txt_Multi_O1 = (TextBox)PageSource.FindControl("txt_Multi_O1");
                TextBox txt_Multi_O2 = (TextBox)PageSource.FindControl("txt_Multi_O2");
                TextBox txt_Multi_O3 = (TextBox)PageSource.FindControl("txt_Multi_O3");
                TextBox txt_Multi_O4 = (TextBox)PageSource.FindControl("txt_Multi_O4");
                TextBox txt_Multi_O5 = (TextBox)PageSource.FindControl("txt_Multi_O5");
                TextBox txt_Multi_O6 = (TextBox)PageSource.FindControl("txt_Multi_O6");

                TextBox txt_Multi_D1 = (TextBox)PageSource.FindControl("txt_Multi_D1");
                TextBox txt_Multi_D2 = (TextBox)PageSource.FindControl("txt_Multi_D2");
                TextBox txt_Multi_D3 = (TextBox)PageSource.FindControl("txt_Multi_D3");
                TextBox txt_Multi_D4 = (TextBox)PageSource.FindControl("txt_Multi_D4");
                TextBox txt_Multi_D5 = (TextBox)PageSource.FindControl("txt_Multi_D5");
                TextBox txt_Multi_D6 = (TextBox)PageSource.FindControl("txt_Multi_D6");

                Label lblFechaMulti1E = (Label)PageSource.FindControl("lblFechaMulti1E");
                Label lblFechaMultiE02 = (Label)PageSource.FindControl("lblFechaMultiE02");
                Label lblFechaMultiE03 = (Label)PageSource.FindControl("lblFechaMultiE03");
                Label lblFechaMultiE04 = (Label)PageSource.FindControl("lblFechaMultiE04");
                Label lblFechaMultiE05 = (Label)PageSource.FindControl("lblFechaMultiE05");
                Label lblFechaMultiE06 = (Label)PageSource.FindControl("lblFechaMultiE06");

                Label lbl_Multi_O1E = (Label)PageSource.FindControl("lbl_Multi_O1E");
                Label lbl_Multi_O2E = (Label)PageSource.FindControl("lbl_Multi_O2E");
                Label lbl_Multi_O3E = (Label)PageSource.FindControl("lbl_Multi_O3E");
                Label lbl_Multi_O4E = (Label)PageSource.FindControl("lbl_Multi_O4E");
                Label lbl_Multi_O5E = (Label)PageSource.FindControl("lbl_Multi_O5E");
                Label lbl_Multi_O6E = (Label)PageSource.FindControl("lbl_Multi_O6E");

                Label lbl_Multi_D1E = (Label)PageSource.FindControl("lbl_Multi_D1E");
                Label lbl_Multi_D2E = (Label)PageSource.FindControl("lbl_Multi_D2E");
                Label lbl_Multi_D3E = (Label)PageSource.FindControl("lbl_Multi_D3E");
                Label lbl_Multi_D4E = (Label)PageSource.FindControl("lbl_Multi_D4E");
                Label lbl_Multi_D5E = (Label)PageSource.FindControl("lbl_Multi_D5E");
                Label lbl_Multi_D6E = (Label)PageSource.FindControl("lbl_Multi_D6E");

            

                Label lblErorMulti = (Label)PageSource.FindControl("lblErrorGen");
                Label lbl_Multi_O1 = (Label)PageSource.FindControl("lbl_Multi_O1");
                Label lbl_Multi_D1 = (Label)PageSource.FindControl("lbl_Multi_D1");
                Label lblFechaMulti1 = (Label)PageSource.FindControl("lblFechaMulti1");

                /*ERROR MULTIDESTINO*/
                lblErorMulti.Text = String.Empty;

                if (txt_Multi_O1.Text.Length.Equals(0))
                {
                    Resp = false;
                    if (lblErorMulti.Text.Length.Equals(0))
                    {
                        lblErorMulti.Text = "Complete: " + lbl_Multi_O1.Text;
                    }
                    else
                    {
                        lblErorMulti.Text += ", " + lbl_Multi_O1.Text;
                    }
                    lbl_Multi_O1E.Text = " *";
                }
                else
                {
                    Resp = setValidaAirport(txt_Multi_O1.Text);
                    if (!Resp)
                    {
                        if (lblErorMulti.Text.Length.Equals(0))
                        {
                            lblErorMulti.Text = lbl_Multi_O1.Text + " No válido, seleccione una opcion de las sugeridas";
                        }
                        else
                        {
                            lblErorMulti.Text += ", " + lbl_Multi_O1.Text + " No válido, seleccione una opcion de las sugeridas";
                        }
                        lbl_Multi_O1E.Text = " *";
                    }
                }

                if (txt_Multi_D1.Text.Length.Equals(0))
                {
                    Resp = false;
                    if (lblErorMulti.Text.Length.Equals(0))
                    {
                        lblErorMulti.Text = "Complete: " + lbl_Multi_D1.Text;
                    }
                    else
                    {
                        lblErorMulti.Text += ", " + lbl_Multi_D1.Text;
                    }
                    lbl_Multi_D1E.Text = " *";
                }
                else
                {
                    if (Resp)
                    {
                        Resp = setValidaAirport(txt_Multi_D1.Text);
                        if (!Resp)
                        {
                            if (lblErorMulti.Text.Length.Equals(0))
                            {
                                lblErorMulti.Text = lbl_Multi_D1.Text + " No válido, seleccione una opcion de las sugeridas";
                            }
                            else
                            {
                                lblErorMulti.Text += ", " + lbl_Multi_D1.Text + " No válido, seleccione una opcion de las sugeridas";
                            }
                            lbl_Multi_D1E.Text = " *";
                        }
                    }
                }

                if (txtFechaMultiO1.Text.Length.Equals(0))
                {
                    Resp = false;
                    if (lblErorMulti.Text.Length.Equals(0))
                    {
                        lblErorMulti.Text = "Complete: " + lblFechaMulti1.Text;
                    }
                    else
                    {
                        lblErorMulti.Text += ", " + lblFechaMulti1.Text;
                    }
                    lblFechaMulti1E.Text = " *";
                }
                // Para los otros destinos
                if (!txt_Multi_D2.Text.Length.Equals(0))
                {
                    if (txt_Multi_O2.Text.Length.Equals(0))
                    {
                        Resp = false;
                        if (lblErorMulti.Text.Length.Equals(0))
                        {
                            lblErorMulti.Text = "Complete: " + lbl_Multi_O1.Text;
                        }
                        else
                        {
                            lblErorMulti.Text += ", " + lbl_Multi_O1.Text;
                        }
                        lbl_Multi_O2E.Text = " *";
                    }
                    else
                    {
                        Resp = setValidaAirport(txt_Multi_O2.Text);
                        if (!Resp)
                        {
                            if (lblErorMulti.Text.Length.Equals(0))
                            {
                                lblErorMulti.Text = lbl_Multi_O1.Text + " No válido, seleccione una opcion de las sugeridas";
                            }
                            else
                            {
                                lblErorMulti.Text += ", " + lbl_Multi_O1.Text + " No válido, seleccione una opcion de las sugeridas";
                            }
                            lbl_Multi_O2E.Text = " *";
                        }
                    }
                    if (txt_Multi_D2.Text.Length.Equals(0))
                    {
                        Resp = false;
                        if (lblErorMulti.Text.Length.Equals(0))
                        {
                            lblErorMulti.Text = "Complete: " + lbl_Multi_D1.Text;
                        }
                        else
                        {
                            lblErorMulti.Text += ", " + lbl_Multi_D1.Text;
                        }
                        lbl_Multi_D2E.Text = " *";
                    }
                    else
                    {
                        if (Resp)
                        {
                            Resp = setValidaAirport(txt_Multi_D2.Text);
                            if (!Resp)
                            {
                                if (lblErorMulti.Text.Length.Equals(0))
                                {
                                    lblErorMulti.Text = lbl_Multi_D1.Text + " No válido, seleccione una opcion de las sugeridas";
                                }
                                else
                                {
                                    lblErorMulti.Text += ", " + lbl_Multi_D1.Text + " No válido, seleccione una opcion de las sugeridas";
                                }
                                lbl_Multi_D2E.Text = " *";
                            }
                        }
                    }

                    if (txtFechaMultiO2.Text.Length.Equals(0))
                    {
                        Resp = false;
                        if (lblErorMulti.Text.Length.Equals(0))
                        {
                            lblErorMulti.Text = "Complete: " + lblFechaMulti1.Text;
                        }
                        else
                        {
                            lblErorMulti.Text += ", " + lblFechaMulti1.Text;
                        }
                        lblFechaMultiE02.Text = " *";
                    }
                }
                if (Resp)
                {
                    Resp = setValidaDestinos(txt_Multi_D1.Text, txt_Multi_O1.Text);
                    if (!Resp)
                    {
                        lblErorMulti.Text = "El destino y Origen no pueden ser iguales";
                    }
                }
            }
            catch { }
            return Resp;
        }
        private bool ValidarFechas(UserControl PageSource)
        {
            TextBox txtFechaMultiO1 = (TextBox)PageSource.FindControl("txtFechaMultiO1");
            TextBox txtFechaMulti2 = (TextBox)PageSource.FindControl("txt2VFechaMulti");
            Label lblErrorGen = (Label)PageSource.FindControl("lblErrorGen");

            bool Resp = true;
            /*ASIGNAR A VARIABLES*/
            Label lblIdaRegresoFechaSalida = new Label();
            Label lblIdaRegresoFechaRegreso = new Label();
            Label lblErrorIdayRegreso = new Label();
            Label lblIdaRegresoFechaRegresoE = new Label();
            Label lblIdaRegresoFechaSalidaE = new Label();

            /*TEXBOX*/
            TextBox txtIdaRegresoFechaSalida = txtFechaMultiO1;
            TextBox txtIdaRegresoFechaRegreso = txtFechaMulti2;
            /*ASIGNACION*/
            lblIdaRegresoFechaSalida.Text = "Fecha salida";
            lblIdaRegresoFechaRegreso.Text = "Fecha regreso";
            lblErrorGen.Text = String.Empty;
            lblErrorIdayRegreso = lblErrorGen;

            try
            {
                if (txtIdaRegresoFechaRegreso.Text.Length > 0 || txtIdaRegresoFechaSalida.Text.Length > 0)
                {
                    int iDias = 0;
                    System.IFormatProvider MiFp = new System.Globalization.CultureInfo("en-US", true);
                    DateTime dFechaInicio = new DateTime();
                    DateTime dFechaFin = new DateTime();

                    dFechaInicio = DateTime.Parse(txtIdaRegresoFechaSalida.Text, MiFp);
                    dFechaFin = DateTime.Parse(txtIdaRegresoFechaRegreso.Text, MiFp);
                    TimeSpan tsDias = new TimeSpan();

                    tsDias = dFechaFin.Subtract(dFechaInicio);
                    iDias = tsDias.Days;
                    if (iDias < 0)
                    {
                        Resp = false;
                        if (lblErrorIdayRegreso.Text.Length.Equals(0))
                        {
                            lblErrorIdayRegreso.Text = lblIdaRegresoFechaSalida.Text + " mayor que " + lblIdaRegresoFechaRegreso.Text;
                        }
                        else
                        {
                            lblErrorIdayRegreso.Text += ". " + lblIdaRegresoFechaSalida.Text + " mayor que " + lblIdaRegresoFechaRegreso.Text;
                        }
                        lblIdaRegresoFechaSalidaE.Text = " *";
                        lblIdaRegresoFechaRegresoE.Text = " *";
                    }
                }
            }
            catch
            {
                Resp = false;
            }
            return Resp;
        }
        public List<VO_OriginDestinationInformation> setValidarTrayectos(List<VO_OriginDestinationInformation> lvo_Rutas)
        {
            int iContadorOpcion = 0;
            List<VO_OriginDestinationInformation> lvo_RutasEvaluadas = new List<VO_OriginDestinationInformation>();
            string eTipoConexion = string.Empty;

            if (lvo_Rutas != null)
            {
                int iRutas = lvo_Rutas.Count;
                string sDestinoAnterior = String.Empty;

                while (iContadorOpcion < iRutas)
                {
                    VO_OriginDestinationInformation vo_Ruta = lvo_Rutas[iContadorOpcion];
                    VO_OriginDestinationInformation vo_RutaDestino = null;

                    if (iContadorOpcion != 0)
                    {
                        vo_RutaDestino = lvo_Rutas[iContadorOpcion - 1];
                    }

                    if (vo_RutaDestino != null)
                    {
                        eTipoConexion = getConexion(vo_RutaDestino.Vo_AeropuertoDestino, vo_Ruta.Vo_AeropuertoOrigen);

                        if (isArunk(eTipoConexion))
                        {
                            vo_Ruta.OTipoSegmento = TipoSegmento.O;
                            lvo_RutasEvaluadas.Add(getArunk(vo_Ruta.Vo_AeropuertoOrigen.SCodigo));
                        }
                    }
                    lvo_RutasEvaluadas.Add(vo_Ruta);
                    iContadorOpcion++;
                }
            }
            return lvo_RutasEvaluadas;
        }
        private VO_OriginDestinationInformation getArunk(string sOrigen)
        {
            VO_OriginDestinationInformation vo_OriginDestinationInformation =
                new VO_OriginDestinationInformation();

            //vo_OriginDestinationInformation.Vo_AeropuertoOrigen =
            //    new VO_Aeropuerto(sOrigen.Substring(0, 3), "IATA");

            vo_OriginDestinationInformation.Vo_AeropuertoDestino =
                new VO_Aeropuerto(sOrigen.Substring(0, 3), "IATA");

            vo_OriginDestinationInformation.OTipoSegmento =
                TipoSegmento.ARUNK;

            return vo_OriginDestinationInformation;
        }
        private string getConexion(VO_Aeropuerto vo_AeropuertoOrigen, VO_Aeropuerto vo_AeropuertoDestino)
        {
            string eTipoConexion = string.Empty;

            if (vo_AeropuertoDestino.SCodigo.Equals(String.Empty) || vo_AeropuertoDestino.SCodigo.Equals(vo_AeropuertoOrigen.SCodigo))
            {
                eTipoConexion = TipoSegmento.O.ToString();
            }
            else
            {
                eTipoConexion = TipoSegmento.ARUNK.ToString();
            }

            return eTipoConexion;
        }     
        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="PageSource"></param>
        private void setActualizaAirport(UserControl PageSource)
        {
            // Aereo
            TextBox txt_Multi_O1 = (TextBox)PageSource.FindControl("txt_Multi_O1");
            TextBox txt_Multi_O2 = (TextBox)PageSource.FindControl("txt_Multi_O2");
            TextBox txt_Multi_O3 = (TextBox)PageSource.FindControl("txt_Multi_O3");
            TextBox txt_Multi_O4 = (TextBox)PageSource.FindControl("txt_Multi_O4");
            TextBox txt_Multi_O5 = (TextBox)PageSource.FindControl("txt_Multi_O5");
            TextBox txt_Multi_O6 = (TextBox)PageSource.FindControl("txt_Multi_O6");
            TextBox txt_Multi_D1 = (TextBox)PageSource.FindControl("txt_Multi_D1");
            TextBox txt_Multi_D2 = (TextBox)PageSource.FindControl("txt_Multi_D2");
            TextBox txt_Multi_D3 = (TextBox)PageSource.FindControl("txt_Multi_D3");
            TextBox txt_Multi_D4 = (TextBox)PageSource.FindControl("txt_Multi_D4");
            TextBox txt_Multi_D5 = (TextBox)PageSource.FindControl("txt_Multi_D5");
            TextBox txt_Multi_D6 = (TextBox)PageSource.FindControl("txt_Multi_D6");
            DataTable tblCiudades = null;
        

            string sTipoRefere = clsValidaciones.GetKeyOrAdd("AEROPUERTOS");
          
            string sAplicacion = clsSesiones.getAplicacion().ToString();
            string sIdioma = clsSesiones.getIdioma();

            try
            {
                if (txt_Multi_O1 != null)
                {
                    if (txt_Multi_O1.Text.Length > 3)
                    {
                        if (!txt_Multi_O1.Text.Trim().Equals(" "))
                        {
                            tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + txt_Multi_O1.Text.ToUpper() + "%' , '" + sIdioma + "'");
                            if (tblCiudades != null)
                            {
                                if (tblCiudades.Rows.Count > 0)
                                    txt_Multi_O1.Text = tblCiudades.Rows[0]["STRCODE"].ToString() + " " + tblCiudades.Rows[0]["STRDESCRIPTION"].ToString() + " (" + tblCiudades.Rows[0]["STRAIRPORT"].ToString()+")";
                            }
                            //txt_Multi_O1.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txt_Multi_O1.Text, sIdioma, sAplicacion);
                        }
                    }
                }
                if (txt_Multi_O2 != null)
                {
                    if (txt_Multi_O2.Text.Length > 3)
                    {
                        if (!txt_Multi_O2.Text.Trim().Equals(" "))
                        {
                            tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + txt_Multi_O2.Text.ToUpper() + "%' , '" + sIdioma + "'");
                            if (tblCiudades != null)
                            {
                                if (tblCiudades.Rows.Count > 0)
                                    txt_Multi_O2.Text = tblCiudades.Rows[0]["STRCODE"].ToString() + " " + tblCiudades.Rows[0]["STRDESCRIPTION"].ToString() + " (" + tblCiudades.Rows[0]["STRAIRPORT"].ToString() + ")";
                            }
                            //txt_Multi_O2.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txt_Multi_O2.Text, sIdioma, sAplicacion);
                        }
                    }
                }
                if (txt_Multi_O3 != null)
                {
                    if (txt_Multi_O3.Text.Length > 3)
                    {
                        if (!txt_Multi_O3.Text.Trim().Equals(" "))
                        {
                            tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + txt_Multi_O3.Text.ToUpper() + "%' , '" + sIdioma + "'");
                            if (tblCiudades != null)
                            {
                                if (tblCiudades.Rows.Count > 0)
                                    txt_Multi_O3.Text = tblCiudades.Rows[0]["STRCODE"].ToString() + " " + tblCiudades.Rows[0]["STRDESCRIPTION"].ToString() + " (" + tblCiudades.Rows[0]["STRAIRPORT"].ToString() + ")";
                            }
                            //txt_Multi_O3.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txt_Multi_O3.Text, sIdioma, sAplicacion);
                        }
                    }
                }
                if (txt_Multi_O4 != null)
                {
                    if (txt_Multi_O4.Text.Length > 3)
                    {
                        if (!txt_Multi_O4.Text.Trim().Equals(" "))
                        {
                            tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + txt_Multi_O4.Text.ToUpper() + "%' , '" + sIdioma + "'");
                            if (tblCiudades != null)
                            {
                                if (tblCiudades.Rows.Count > 0)
                                    txt_Multi_O4.Text = tblCiudades.Rows[0]["STRCODE"].ToString() + " " + tblCiudades.Rows[0]["STRDESCRIPTION"].ToString() + " (" + tblCiudades.Rows[0]["STRAIRPORT"].ToString() + ")";
                            }
                            //txt_Multi_O4.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txt_Multi_O4.Text, sIdioma, sAplicacion);
                        }
                    }
                }
                if (txt_Multi_O5 != null)
                {
                    if (txt_Multi_O5.Text.Length > 3)
                    {
                        if (!txt_Multi_O5.Text.Trim().Equals(" "))
                        {
                            tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + txt_Multi_O5.Text.ToUpper() + "%' , '" + sIdioma + "'");
                            if (tblCiudades != null)
                            {
                                if (tblCiudades.Rows.Count > 0)
                                    txt_Multi_O5.Text = tblCiudades.Rows[0]["STRCODE"].ToString() + " " + tblCiudades.Rows[0]["STRDESCRIPTION"].ToString() + " (" + tblCiudades.Rows[0]["STRAIRPORT"].ToString() + ")";
                            }
                            //txt_Multi_O5.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txt_Multi_O5.Text, sIdioma, sAplicacion);
                        }
                    }
                }
                if (txt_Multi_O6 != null)
                {
                    if (txt_Multi_O6.Text.Length > 3)
                    {
                        if (!txt_Multi_O6.Text.Trim().Equals(" "))
                        {
                            tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + txt_Multi_O6.Text.ToUpper() + "%' , '" + sIdioma + "'");
                            if (tblCiudades != null)
                            {
                                if (tblCiudades.Rows.Count > 0)
                                    txt_Multi_O6.Text = tblCiudades.Rows[0]["STRCODE"].ToString() + " " + tblCiudades.Rows[0]["STRDESCRIPTION"].ToString() + " (" + tblCiudades.Rows[0]["STRAIRPORT"].ToString() + ")";
                            }
                            //txt_Multi_O6.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txt_Multi_O6.Text, sIdioma, sAplicacion);
                        }
                    }
                }
                if (txt_Multi_D1 != null)
                {
                    if (txt_Multi_D1.Text.Length > 3)
                    {
                        if (!txt_Multi_D1.Text.Trim().Equals(" "))
                        {
                            tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + txt_Multi_D1.Text.ToUpper() + "%' , '" + sIdioma + "'");
                            if (tblCiudades != null)
                            {
                                if (tblCiudades.Rows.Count > 0)
                                    txt_Multi_D1.Text = tblCiudades.Rows[0]["STRCODE"].ToString() + " " + tblCiudades.Rows[0]["STRDESCRIPTION"].ToString() + " (" + tblCiudades.Rows[0]["STRAIRPORT"].ToString() + ")";
                            }
                            //txt_Multi_D1.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txt_Multi_D1.Text, sIdioma, sAplicacion);
                        }
                    }
                }
                if (txt_Multi_D2 != null)
                {
                    if (txt_Multi_D2.Text.Length > 3)
                    {
                        if (!txt_Multi_D2.Text.Trim().Equals(" "))
                        {
                            tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + txt_Multi_D2.Text.ToUpper() + "%' , '" + sIdioma + "'");
                            if (tblCiudades != null)
                            {
                                if (tblCiudades.Rows.Count > 0)
                                    txt_Multi_D2.Text = tblCiudades.Rows[0]["STRCODE"].ToString() + " " + tblCiudades.Rows[0]["STRDESCRIPTION"].ToString() + " (" + tblCiudades.Rows[0]["STRAIRPORT"].ToString() + ")";
                            }
                            //txt_Multi_D2.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txt_Multi_D2.Text, sIdioma, sAplicacion);
                        }
                    }
                }
                if (txt_Multi_D3 != null)
                {
                    if (txt_Multi_D3.Text.Length > 3)
                    {
                        if (!txt_Multi_D3.Text.Trim().Equals(" "))
                        {
                            tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + txt_Multi_D3.Text.ToUpper() + "%' , '" + sIdioma + "'");
                            if (tblCiudades != null)
                            {
                                if (tblCiudades.Rows.Count > 0)
                                    txt_Multi_D3.Text = tblCiudades.Rows[0]["STRCODE"].ToString() + " " + tblCiudades.Rows[0]["STRDESCRIPTION"].ToString() + " (" + tblCiudades.Rows[0]["STRAIRPORT"].ToString() + ")";
                            }
                            //txt_Multi_D3.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txt_Multi_D3.Text, sIdioma, sAplicacion);
                        }
                    }
                }
                if (txt_Multi_D4 != null)
                {
                    if (txt_Multi_D4.Text.Length > 3)
                    {
                        if (!txt_Multi_D4.Text.Trim().Equals(" "))
                        {
                            tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + txt_Multi_D4.Text.ToUpper() + "%' , '" + sIdioma + "'");
                            if (tblCiudades != null)
                            {
                                if (tblCiudades.Rows.Count > 0)
                                    txt_Multi_D4.Text = tblCiudades.Rows[0]["STRCODE"].ToString() + " " + tblCiudades.Rows[0]["STRDESCRIPTION"].ToString() + " (" + tblCiudades.Rows[0]["STRAIRPORT"].ToString() + ")";
                            }
                            //txt_Multi_D4.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txt_Multi_D4.Text, sIdioma, sAplicacion);
                        }
                    }
                }
                if (txt_Multi_D5 != null)
                {
                    if (txt_Multi_D5.Text.Length > 3)
                    {
                        if (!txt_Multi_D5.Text.Trim().Equals(" "))
                        {
                            tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + txt_Multi_D5.Text.ToUpper() + "%' , '" + sIdioma + "'");
                            if (tblCiudades != null)
                            {
                                if (tblCiudades.Rows.Count > 0)
                                    txt_Multi_D5.Text = tblCiudades.Rows[0]["STRCODE"].ToString() + " " + tblCiudades.Rows[0]["STRDESCRIPTION"].ToString() + " (" + tblCiudades.Rows[0]["STRAIRPORT"].ToString() + ")";
                            }
                            //txt_Multi_D5.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txt_Multi_D5.Text, sIdioma, sAplicacion);
                        }
                    }
                }
                if (txt_Multi_D6 != null)
                {
                    if (txt_Multi_D6.Text.Length > 3)
                    {
                        if (!txt_Multi_D6.Text.Trim().Equals(" "))
                        {
                            tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + txt_Multi_D6.Text.ToUpper() + "%' , '" + sIdioma + "'");
                            if (tblCiudades != null)
                            {
                                if (tblCiudades.Rows.Count > 0)
                                    txt_Multi_D6.Text = tblCiudades.Rows[0]["STRCODE"].ToString() + " " + tblCiudades.Rows[0]["STRDESCRIPTION"].ToString() + " (" + tblCiudades.Rows[0]["STRAIRPORT"].ToString() + ")";
                            }
                            //txt_Multi_D6.Text = otblRefere.GetTipoRefereIata(sTipoRefere, txt_Multi_D6.Text, sIdioma, sAplicacion);
                        }
                    }
                }
              
            }
            catch { }
        }
        public void setCambiaHorasConvenio()
        {
            try
            {
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                string sTarifaNegociada = vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada;
                if (vo_OTA_AirLowFareSearchLLSRQ.BHoras.Equals(false))
                {
                    vo_OTA_AirLowFareSearchLLSRQ.BHoras = true;
                    clsSesiones.setParametrosAirBargain(vo_OTA_AirLowFareSearchLLSRQ);
                }
            }
            catch { }
        }     
        private bool setValidaAirport(string sAir)
        {
            bool bValida = true;
            try
            {
                if (sAir.Length.Equals(0))
                {
                    bValida = false;
                }
                else
                {
                    if (!sAir.Substring(3, 1).Equals(" "))
                    {
                        bValida = false;
                    }
                    else
                    {
                        if (clsValidaciones.GetKeyOrAdd("dValidaAirPort", "True").ToUpper().Equals("TRUE"))
                        {
                            string sTipoRefere = clsValidaciones.GetKeyOrAdd("AEROPUERTOS");
                            string sRefere = sAir;
                            if (sRefere.Length > 3)
                                sRefere = sAir.Substring(0, 3);
                            //tblRefere otblRefere = new tblRefere();
                            //bValida = otblRefere.ExistRefere(sTipoRefere, sRefere);
                        }
                    }
                }
            }
            catch { bValida = false; }
            return bValida;
        }
        private bool setValidaPaxAir(List<VO_Passenger> cPass)
        {
            bool bValida = true;
            try
            {
                int Adulto = 0;
                int Nino = 0;
                int Infante = 0;
                int Pasajeros = 0;
                int iMaxPax = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("iMaxPaxAir", "8"));
                for (int i = 0; i < cPass.Count; i++)
                {
                    Adulto = Adulto + Convert.ToInt32(cPass[i].Adulto.ToString());
                    Nino = Nino + Convert.ToInt32(cPass[i].Nino.ToString());
                    Infante = Infante + Convert.ToInt32(cPass[i].Infante.ToString());
                }
                Pasajeros = Adulto + Nino + Infante;
                if (Pasajeros > iMaxPax)
                {
                    bValida = false;
                }
            }
            catch { bValida = false; }
            return bValida;
        }
        private bool setValidaPaxAir(List<VO_Pasajero> cPass)
        {
            bool bValida = true;
            try
            {
                int Pasajeros = 0;
                int iMaxPax = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("iMaxPaxAir", "8"));
                for (int i = 0; i < cPass.Count; i++)
                {
                    Pasajeros += Convert.ToInt32(cPass[i].SCantidad);
                }
                if (Pasajeros > iMaxPax)
                {
                    bValida = false;
                }
            }
            catch { bValida = false; }
            return bValida;
        }
        private bool setValidaDestinos(string sAirOrigen, string sAirDestino)
        {
            bool bValida = true;
            try
            {
                if (sAirOrigen.Length.Equals(0))
                {
                    bValida = false;
                }
                else
                {
                    if (sAirDestino.Length.Equals(0))
                    {
                        bValida = false;
                    }
                    else
                    {
                        if (sAirOrigen.Length > 3)
                        {
                            if (sAirDestino.Length > 3)
                            {
                                sAirOrigen = sAirOrigen.Substring(0, 3);
                                sAirDestino = sAirDestino.Substring(0, 3);
                                if (sAirOrigen.Equals(sAirDestino))
                                {
                                    bValida = false;
                                }
                                else
                                {
                                    bValida = true;
                                }
                            }
                            else
                            {
                                bValida = false;
                            }
                        }
                        else
                        {
                            bValida = false;
                        }
                    }
                }
            }
            catch { bValida = false; }
            return bValida;
        }   
        private object csPlanes()
        {
            throw new Exception("The method or operation is not implemented.");
        }       
        /// <summary>
        /// Este metodo llena un repetidor con los ddl para pasajeros segun el numero de habitaciones seleccionadas
        /// </summary>
        /// <param name="PageSource">Usercintrol</param>       
        /// <param name="bInicio">indica si es cargue de una sola habitacion</param>       
        /// <remarks>
        /// Autor:      Juan Camilo Diaz     
        /// Company:    Ssoft colombia     
        /// Fecha:      2011-11-30
        /// -------------------
        /// Control de Cambios
        /// -------------------         
        /// </remarks>      
        public bool validaPasajeros(UserControl PageSource)
        {
            bool valida = true;
            try
            {
                DropDownList ddlTipoPlan = (DropDownList)PageSource.FindControl("ddlTipoPlan");
                Label lblError = (Label)PageSource.FindControl("lblError");

                List<VO_Passenger> cPass = clsSesiones.getPassenger();
                valida = setValidaPaxAir(cPass);
                if (!valida)
                {
                    if (ddlTipoPlan.SelectedItem.ToString() == "Alojamiento y Tiquete")
                    {
                        lblError.Text = "Si desea alojamiento y Tiquete solamente puede seleccionar 6 pasajeros maximo";
                    }
                    else
                    {
                        valida = true;
                    }
                }
            }
            catch { valida = false; }
            return valida;
        }  
        #region [IROTAMA]
        public void SetParametrosBusquedaVuelos(
        String sOrigen,
        String sDestino,
        String sFechaSalida,
        String sFechaRegreso,
        Enum_TipoTrayecto tipoTrayecto,
        List<string> lsClase,
        List<string> lsAerolineas,
        String SMaximasParadas,
        String sMoneda,
        Int32 intAdultos,
        Int32 intNinios,
        Int32 intBebes,
        Int32 intJunior,
        List<string> lEdadNinos,
        List<string> lEdadBebes)
        {
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = new VO_OTA_AirLowFareSearchLLSRQ();
            /*horas*/
            vo_OTA_AirLowFareSearchLLSRQ.BHoras = false;

            List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation =
                        new List<VO_OriginDestinationInformation>();
            /*Origen*/
            VO_Aeropuerto vo_AeropuertoOrigen =
                        new VO_Aeropuerto(sOrigen, "IATA");
            /*Destino*/
            VO_Aeropuerto vo_AeropuertoDestino =
                new VO_Aeropuerto(sDestino, "IATA");

            /*VUELO IDA*/
            VO_OriginDestinationInformation vo_Ruta =
                     new VO_OriginDestinationInformation
                         (
                             sFechaSalida,
                             null,
                             sFechaRegreso,
                             null,
                             vo_AeropuertoOrigen,
                             vo_AeropuertoDestino,
                             TipoSegmento.O,
                             false,
                             null,
                             null
                         );

            lvo_OriginDestinationInformation.Add(vo_Ruta);

            /*VUELO REGRESO*/
            if (tipoTrayecto == Enum_TipoTrayecto.IdaRegreso)
            {
                vo_Ruta =
                     new VO_OriginDestinationInformation
                         (
                             sFechaRegreso,
                             null,
                             null,
                             null,
                             vo_AeropuertoDestino,
                             vo_AeropuertoOrigen,
                             TipoSegmento.O,
                             false,
                             null,
                             null
                         );
                lvo_OriginDestinationInformation.Add(vo_Ruta);
            }

            /*RUTAS*/
            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas = lvo_OriginDestinationInformation;
            /*CLASE*/

            if (lsClase == null)
            {
                lsClase = new List<string>();
                lsClase.Add("Y");
            }

            vo_OTA_AirLowFareSearchLLSRQ.LsClase = lsClase;
            vo_OTA_AirLowFareSearchLLSRQ.BConFarCalc = true;
            vo_OTA_AirLowFareSearchLLSRQ.BRetornarMaxResultados = true;
            vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = lsAerolineas ?? new List<string>();
            vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas = SMaximasParadas ?? "1";
            vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto = tipoTrayecto;

            vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion = sMoneda ?? clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");

            /*PASAJEROS*/
            List<VO_Pasajero> lvo_Pasajeros = new List<VO_Pasajero>();
            /*ADULTOS*/
            /*JUNIOR*/
            VO_Pasajero vo_Pasajero = new VO_Pasajero("ADT", (intAdultos + intJunior).ToString());

            lvo_Pasajeros.Add(vo_Pasajero);

            /*NIÑOS*/
            if (intNinios != 0)
            {
                vo_Pasajero = new VO_Pasajero("CNN", intNinios.ToString());
                List<VO_ClasificaPasajero> lvPasajeroNino = new List<VO_ClasificaPasajero>();
                /*Organizamos la lista*/
                OrganizarLista(lEdadNinos);

                foreach (string sEdad in lEdadNinos)
                {
                    string edad = "";
                    if (int.Parse(sEdad) <= 9)
                        edad = "0" + sEdad;

                    VO_ClasificaPasajero vPasajeroNino = new VO_ClasificaPasajero("C" + edad, "1", sEdad);
                    lvPasajeroNino.Add(vPasajeroNino);
                }

                vo_Pasajero.LvPasajeroNino = lvPasajeroNino;
                vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios = lEdadNinos;
                lvo_Pasajeros.Add(vo_Pasajero);
            }

            /*INFANTES*/
            if (intBebes != 0)
            {
                vo_Pasajero = new VO_Pasajero("INF", intBebes.ToString());
                List<VO_ClasificaPasajero> lvPasajeroBebe = new List<VO_ClasificaPasajero>();
                /*Organizamos la lista*/
                OrganizarLista(lEdadBebes);

                foreach (string sEdad in lEdadNinos)
                {
                    VO_ClasificaPasajero vPasajeroBebe = new VO_ClasificaPasajero("C0" + sEdad, "1", sEdad);
                    lvPasajeroBebe.Add(vPasajeroBebe);
                }

                vo_Pasajero.LvPasajeroInfante = lvPasajeroBebe;
                vo_OTA_AirLowFareSearchLLSRQ.LsEdadesInfantes = lEdadBebes;
                lvo_Pasajeros.Add(vo_Pasajero);
            }

            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros = lvo_Pasajeros;
            /*TIPO VUELO*/
            csVuelos clsValidacionesVuelos = new csVuelos();
            vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo = clsValidacionesVuelos.getValidarTipoTrayecto(lvo_OriginDestinationInformation);
            vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo;

            vo_OTA_AirLowFareSearchLLSRQ.Ruta = 0;
            /*Guardamos los datos en la Session*/
            clsSesiones.setParametrosAirBargain(vo_OTA_AirLowFareSearchLLSRQ);
        }

        private static void OrganizarLista(List<string> lista)
        {
            /*removemos los items*/
            for (int c = 0; c < lista.Count; c++)
                for (int i = c; i < lista.Count; i++)
                    if (lista[c] == lista[i] && c != i)
                        lista.Remove(lista[i]);
            /*ordenamos*/
            lista.Sort(new Comparison<string>(delegate(string a, string b) { return int.Parse(a).CompareTo(int.Parse(b)); }));
        }

        public void BuscarItinerario(UserControl PageSource)
        {
            try
            {
               
                    csBuscador buscador = new csBuscador();
                    buscador.setProcesarBusqueda(PageSource);
                
            }
            catch (Exception) { }
        }

    

        public void BuscarVuelos(UserControl PageSource)
        {
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();

            TextBox txtCiudadOrigen = PageSource.FindControl("txtCiudadOrigen") as TextBox;
            DropDownList ddlHoraSalida = PageSource.FindControl("ddlHoraSalida") as DropDownList;
            DropDownList ddlHoraLlegada = PageSource.FindControl("ddlHoraLlegada") as DropDownList;
            RadioButton rbIda = PageSource.FindControl("rbIda") as RadioButton;


            if (vo_OTA_AirLowFareSearchLLSRQ != null)
            {
                /*si la ciudad de origen cambio*/
                if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo != txtCiudadOrigen.Text)
                {
                    vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo =
                        txtCiudadOrigen.Text.Substring(0, 3);
                }

                /*si no tiene la ruta de vuelta*/
                if (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto == Enum_TipoTrayecto.IdaRegreso)
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Count == 1)
                    {
                        vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Add(
                            new VO_OriginDestinationInformation(
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaLlegada,
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SIntervaloSalida,
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaSalida,
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SIntervaloLlegada,
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoDestino,
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen,
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].OTipoSegmento,
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].BSinDisponibilidad,
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].STiempoAlternativo,
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoAlterno));

                    }
                }
                /*verificamos si es busqueda por horas*/
                bool bBargain = clsValidaciones._DROP_BARGAIN(ddlHoraSalida);
                if (bBargain)
                    bBargain = clsValidaciones._DROP_BARGAIN(ddlHoraLlegada);
                if (!bBargain)
                    bBargain = clsValidaciones._DROP_BARGAIN(ddlHoraLlegada);
                /*verificamos si es ida o ida y vuelta*/
                if (rbIda.Checked)
                    vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto =
                        Enum_TipoTrayecto.Ida;
                else
                    vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto =
                        Enum_TipoTrayecto.IdaRegreso;
                /*si es busqueda por horas*/
                if (!bBargain)
                {
                    vo_OTA_AirLowFareSearchLLSRQ.BHoras = true;
                    /*colocamos ruta en 1 para vuelos por horas*/
                    vo_OTA_AirLowFareSearchLLSRQ.Ruta = 0;
                    /*concatenamos la hora a la fecha de trayecto ida*/
                    /*verificamos que no se vuelva a contactenar la hora*/

                    int indice1 = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaSalida.IndexOf("T");
                    if (indice1 != -1)
                    {
                        vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaSalida = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaSalida.Remove(indice1);
                    }

                    vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaSalida =
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaSalida + "T" + ddlHoraSalida.SelectedValue;

                    if (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto == Enum_TipoTrayecto.Ida)
                    {
                        /*colocamos en false horas si es solo ida*/
                        vo_OTA_AirLowFareSearchLLSRQ.BHoras = false;
                        /*si hay trayecto de ida y vuelta lo quitamos*/
                        if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Count == 2)
                        {
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Remove(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[1]);
                        }
                    }
                    if (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto == Enum_TipoTrayecto.IdaRegreso)
                    {
                        /*verificamos que no se vuelva a contactenar la hora*/
                        int indice = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[1].SFechaSalida.IndexOf("T");
                        if (indice != -1)
                        {
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[1].SFechaSalida = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[1].SFechaSalida.Remove(indice);
                        }
                        /*concatenamos la hora a la fecha de trayecto ida y vuelta*/
                        vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[1].SFechaSalida =
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[1].SFechaSalida + "T" + ddlHoraLlegada.SelectedValue;
                    }
                }/*busqueda normal*/
                else
                {
                    vo_OTA_AirLowFareSearchLLSRQ.BHoras = false;

                    if (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto == Enum_TipoTrayecto.Ida)
                    {
                        /*si hay ruta de regreso la quitamos*/
                        if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Count >= 2)
                        {
                            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Remove(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[1]);
                        }
                    }
                }
                /*TIPO VUELO*/
                csVuelos clsValidacionesVuelos = new csVuelos();
                vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo = clsValidacionesVuelos.getValidarTipoTrayecto(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas);
                vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo;
                /*guardamos los parametros en la session*/
                clsSesiones.setParametrosAirBargain(vo_OTA_AirLowFareSearchLLSRQ);
                /*hacemos la busqueda*/
                csBuscador buscador = new csBuscador();
                buscador.BuscarItinerario(PageSource);
            }
        }

        public void CargarParametrosBusquedaBusqueda(UserControl PageSource)
        {
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();

            TextBox txtCiudadOrigen = PageSource.FindControl("txtCiudadOrigen") as TextBox;
            DropDownList ddlHoraSalida = PageSource.FindControl("ddlHoraSalida") as DropDownList;
            DropDownList ddlHoraLlegada = PageSource.FindControl("ddlHoraLlegada") as DropDownList;
            DropDownList ddlClase = PageSource.FindControl("ddlClase") as DropDownList;
            RadioButton rbIda = PageSource.FindControl("rbIda") as RadioButton;
            RadioButton rbIdaVuelta = PageSource.FindControl("rbIdaVuelta") as RadioButton;
            RadioButton rbEscalas = PageSource.FindControl("rbEscalas") as RadioButton;
            RadioButton rbDirecto = PageSource.FindControl("rbDirecto") as RadioButton;

            if (vo_OTA_AirLowFareSearchLLSRQ != null)
            {
                /*tipo trayecto*/
                switch (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto)
                {
                    case Enum_TipoTrayecto.Ida:
                        rbIda.Checked = true;
                        break;
                    case Enum_TipoTrayecto.IdaRegreso:
                        rbIdaVuelta.Checked = true;
                        break;
                    case Enum_TipoTrayecto.Multidestino:
                        break;
                    default:
                        break;
                }
                /*ciudad origen*/
                txtCiudadOrigen.Text = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo;
                /*paradas*/
                if (vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas.Equals("0"))
                    rbDirecto.Checked = true;
                else
                    rbEscalas.Checked = true;
                /*clase*/
                ddlClase.SelectedValue = vo_OTA_AirLowFareSearchLLSRQ.LsClase[0];
                /*horas*/
                if (vo_OTA_AirLowFareSearchLLSRQ.BHoras)
                {

                    int posicion = -1;
                    if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Count != 0)
                    {
                        posicion = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaSalida.IndexOf("T");
                        if (posicion != -1)
                        {
                            ddlHoraSalida.SelectedValue =
                                vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].SFechaSalida.Substring((posicion + 1));
                        }
                        /*si es ida y vuelta*/
                        if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Count >= 2)
                        {
                            posicion = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[1].SFechaSalida.IndexOf("T");
                            if (posicion != -1)
                            {
                                ddlHoraLlegada.SelectedValue =
                                    vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[1].SFechaSalida.Substring((posicion + 1));
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
    public class csBuscadorMB : TemplateControl
    {
        csBuscador cBuscador = new csBuscador();
        private static string sFormatoFecha = clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");
        private static string sFormatoFechaBD = clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");
        public csBuscadorMB()
        {
        }
        public void setCommand(UserControl PageSource, string sTB)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {

                    setBuscarVuelos(PageSource, csTipoVuelo());

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
                cParametros.Complemento = "BuscadorMB ";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void setCargar(UserControl PageSource)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    setCommand(PageSource, csValue());
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
                //new csCache().setError(PageSource, cParametros);
            }
        }
        private static string csValue()
        {
            string sValue = "Aereo";
            try
            {
                if (HttpContext.Current.Request.QueryString["TB"] != null)
                {
                    string sTemp = HttpContext.Current.Request.QueryString["TB"].ToString();
                    int iPos = 0;
                    try
                    {
                        iPos = sTemp.IndexOf(" ");
                    }
                    catch
                    {
                        try
                        {
                            iPos = sTemp.IndexOf("+");
                        }
                        catch { }
                    }
                    sValue = sTemp.Substring(iPos).Trim();
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
        public static string csOferta()
        {
            string sValue = "0";
            try
            {
                if (HttpContext.Current.Request.QueryString["id"] != null)
                {
                    sValue = HttpContext.Current.Request.QueryString["id"].ToString();
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
        private static string csTipoVuelo()
        {
            string sValue = "0";
            try
            {
                if (HttpContext.Current.Request.QueryString["modal_vuelos"] != null)
                {
                    sValue = HttpContext.Current.Request.QueryString["modal_vuelos"].ToString();
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
        private string[] csValueGen()
        {
            string[] sValue = new string[6];
            try
            {
                if (HttpContext.Current.Request.QueryString["idUNegocio"] != null)
                {
                    sValue[0] = HttpContext.Current.Request.QueryString["idUNegocio"].ToString();
                }
                else
                {
                    sValue[0] = clsValidaciones.GetKeyOrAdd("idUNegocio", "0");
                }
                if (HttpContext.Current.Request.QueryString["idPropietario"] != null)
                {
                    sValue[1] = HttpContext.Current.Request.QueryString["idPropietario"].ToString();
                }
                else
                {
                    sValue[1] = clsValidaciones.GetKeyOrAdd("idPropietario", "0");
                }
                if (HttpContext.Current.Request.QueryString["idAgencia"] != null)
                {
                    sValue[2] = HttpContext.Current.Request.QueryString["idAgencia"].ToString();
                }
                else
                {
                    sValue[2] = clsValidaciones.GetKeyOrAdd("idAgencia", "0");
                }
                if (HttpContext.Current.Request.QueryString["idE"] != null)
                {
                    sValue[3] = HttpContext.Current.Request.QueryString["idE"].ToString();
                }
                else
                {
                    sValue[3] = clsValidaciones.GetKeyOrAdd("idEmpresa", "0");
                }
                if (HttpContext.Current.Request.QueryString["idComunidad"] != null)
                {
                    sValue[4] = HttpContext.Current.Request.QueryString["idComunidad"].ToString();
                }
                else
                {
                    sValue[4] = clsValidaciones.GetKeyOrAdd("idComunidad", "0");
                }
                if (HttpContext.Current.Request.QueryString["idC"] != null)
                {
                    sValue[5] = HttpContext.Current.Request.QueryString["idC"].ToString();
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
        public void setBuscarVuelos(UserControl PageSource, string sTipoVuelo)
        {
            try
            {
                clsSesiones.CLEAR_SESSION_AIR();

                switch (sTipoVuelo)
                {
                    case "0":
                        BuscarIdaVuelta(PageSource);
                        break;
                    case "1":
                        BuscarSoloIda(PageSource);
                        break;
                    case "2":
                        BuscarMultiDestino(PageSource);
                        break;
                    default:
                        break;
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
                cParametros.Complemento = "Busqueda de vuelos ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                //new csCache().setError(PageSource, cParametros);
            }
        }
        public void setBuscarVuelosOferta(UserControl PageSource, string sTipoVuelo, DataSet dsData)
        {
            try
            {
                clsSesiones.CLEAR_SESSION_AIR();

                switch (sTipoVuelo)
                {
                    case "0":
                        BuscarIdaVueltaOferta(PageSource, dsData);
                        break;
                    case "1":
                        BuscarSoloIdaOferta(PageSource, dsData);
                        break;
                    case "2":
                        BuscarMultiDestinoOferta(PageSource, dsData);
                        break;
                    default:
                        break;
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
                cParametros.Complemento = "Busqueda de vuelos ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                //new csCache().setError(PageSource, cParametros);
            }
        }
        public void setBloquearVuelosOferta(UserControl PageSource)
        {
            try
            {
                string sUseControl = PageSource.ToString().ToUpper();
                if (sUseControl.Contains("AEREO"))
                {
                    RadioButtonList modal_vuelos = (RadioButtonList)PageSource.FindControl("modal_vuelos");
                    TextBox txt_Multi_O1 = (TextBox)PageSource.FindControl("txt_Multi_O1");
                    TextBox txt_Multi_D1 = (TextBox)PageSource.FindControl("txt_Multi_D1");

                    modal_vuelos.Enabled = false;
                    txt_Multi_O1.ReadOnly = true;
                    txt_Multi_D1.ReadOnly = true;
                }
            }
            catch
            {
            }
        }
        private void BuscarMultiDestino(UserControl PageSource)
        {
            try
            {
                string sAerolinea = "0";
                bool bBargain = false;
                List<string> lsAerolineas = new List<string>();
                Utils.Utils oUtilidad = new Utils.Utils();
                string sDestinoAnt = string.Empty;

                string sOrigen1 = string.Empty;
                string sOrigen2 = string.Empty;
                string sOrigen3 = string.Empty;
                string sOrigen4 = string.Empty;
                string sOrigen5 = string.Empty;
                string sOrigen6 = string.Empty;
                string sDestino1 = string.Empty;
                string sDestino2 = string.Empty;
                string sDestino3 = string.Empty;
                string sDestino4 = string.Empty;
                string sDestino5 = string.Empty;
                string sDestino6 = string.Empty;
                string sFecha1 = string.Empty;
                string sFecha2 = string.Empty;
                string sFecha3 = string.Empty;
                string sFecha4 = string.Empty;
                string sFecha5 = string.Empty;
                string sFecha6 = string.Empty;
                string sHora1 = string.Empty;
                string sHora2 = string.Empty;
                string sHora3 = string.Empty;
                string sHora4 = string.Empty;
                string sHora5 = string.Empty;
                string sHora6 = string.Empty;

                string sClase = "Y";
                string sAdt = "1";
                string sCnn = "0";
                string sInf = "0";
                string sEscala = "1";
                bool bPasa = true;

                string sMoneda = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");

                // Tomar los parametros del encabezado
                if (PageSource.Request.QueryString["txt_Multi_O1"] != null && PageSource.Request.QueryString["txt_Multi_O1"].ToString() != "")
                {
                    sOrigen1 = PageSource.Request.QueryString["txt_Multi_O1"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["txt_Multi_O2"] != null && PageSource.Request.QueryString["txt_Multi_O2"].ToString() != "")
                {
                    sOrigen2 = PageSource.Request.QueryString["txt_Multi_O2"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_O3"] != null && PageSource.Request.QueryString["txt_Multi_O3"].ToString() != "")
                {
                    sOrigen3 = PageSource.Request.QueryString["txt_Multi_O3"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_O4"] != null && PageSource.Request.QueryString["txt_Multi_O4"].ToString() != "")
                {
                    sOrigen4 = PageSource.Request.QueryString["txt_Multi_O4"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_O5"] != null && PageSource.Request.QueryString["txt_Multi_O5"].ToString() != "")
                {
                    sOrigen5 = PageSource.Request.QueryString["txt_Multi_O5"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_O6"] != null && PageSource.Request.QueryString["txt_Multi_O6"].ToString() != "")
                {
                    sOrigen6 = PageSource.Request.QueryString["txt_Multi_O6"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_D1"] != null && PageSource.Request.QueryString["txt_Multi_D1"].ToString() != "")
                {
                    sDestino1 = PageSource.Request.QueryString["txt_Multi_D1"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["txt_Multi_D2"] != null && PageSource.Request.QueryString["txt_Multi_D2"].ToString() != "")
                {
                    sDestino2 = PageSource.Request.QueryString["txt_Multi_D2"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_D3"] != null && PageSource.Request.QueryString["txt_Multi_D3"].ToString() != "")
                {
                    sDestino3 = PageSource.Request.QueryString["txt_Multi_D3"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_D4"] != null && PageSource.Request.QueryString["txt_Multi_D4"].ToString() != "")
                {
                    sDestino4 = PageSource.Request.QueryString["txt_Multi_D4"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_D5"] != null && PageSource.Request.QueryString["txt_Multi_D5"].ToString() != "")
                {
                    sDestino5 = PageSource.Request.QueryString["txt_Multi_D5"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_D6"] != null && PageSource.Request.QueryString["txt_Multi_D6"].ToString() != "")
                {
                    sDestino6 = PageSource.Request.QueryString["txt_Multi_D6"].ToString();
                }
                if (PageSource.Request.QueryString["txtFechaMultiO1"] != null && PageSource.Request.QueryString["txtFechaMultiO1"].ToString() != "")
                {
                    sFecha1 = PageSource.Request.QueryString["txtFechaMultiO1"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["txtFechaMultiO2"] != null && PageSource.Request.QueryString["txtFechaMultiO2"].ToString() != "")
                {
                    sFecha2 = PageSource.Request.QueryString["txtFechaMultiO2"].ToString();
                }
                if (PageSource.Request.QueryString["txtFechaMultiO3"] != null && PageSource.Request.QueryString["txtFechaMultiO3"].ToString() != "")
                {
                    sFecha3 = PageSource.Request.QueryString["txtFechaMultiO3"].ToString();
                }
                if (PageSource.Request.QueryString["txtFechaMultiO4"] != null && PageSource.Request.QueryString["txtFechaMultiO4"].ToString() != "")
                {
                    sFecha4 = PageSource.Request.QueryString["txtFechaMultiO4"].ToString();
                }
                if (PageSource.Request.QueryString["txtFechaMultiO5"] != null && PageSource.Request.QueryString["txtFechaMultiO5"].ToString() != "")
                {
                    sFecha5 = PageSource.Request.QueryString["txtFechaMultiO5"].ToString();
                }
                if (PageSource.Request.QueryString["txtFechaMultiO6"] != null && PageSource.Request.QueryString["txtFechaMultiO6"].ToString() != "")
                {
                    sFecha6 = PageSource.Request.QueryString["txtFechaMultiO6"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiHora01"] != null && PageSource.Request.QueryString["ddlMultiHora01"].ToString() != "")
                {
                    sHora1 = "T" + PageSource.Request.QueryString["ddlMultiHora01"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiHoraO2"] != null && PageSource.Request.QueryString["ddlMultiHoraO2"].ToString() != "")
                {
                    sHora2 = "T" + PageSource.Request.QueryString["ddlMultiHoraO2"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiHoraO3"] != null && PageSource.Request.QueryString["ddlMultiHoraO3"].ToString() != "")
                {
                    sHora3 = "T" + PageSource.Request.QueryString["ddlMultiHoraO3"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiHoraO4"] != null && PageSource.Request.QueryString["ddlMultiHoraO4"].ToString() != "")
                {
                    sHora4 = "T" + PageSource.Request.QueryString["ddlMultiHoraO4"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiHoraO5"] != null && PageSource.Request.QueryString["ddlMultiHoraO5"].ToString() != "")
                {
                    sHora5 = "T" + PageSource.Request.QueryString["ddlMultiHoraO5"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiHoraO6"] != null && PageSource.Request.QueryString["ddlMultiHoraO6"].ToString() != "")
                {
                    sHora6 = "T" + PageSource.Request.QueryString["ddlMultiHoraO6"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiAdultos"] != null && PageSource.Request.QueryString["ddlMultiAdultos"].ToString() != "")
                {
                    sAdt = PageSource.Request.QueryString["ddlMultiAdultos"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlMultiNinios"] != null && PageSource.Request.QueryString["ddlMultiNinios"].ToString() != "")
                {
                    sCnn = PageSource.Request.QueryString["ddlMultiNinios"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlMultiBebes"] != null && PageSource.Request.QueryString["ddlMultiBebes"].ToString() != "")
                {
                    sInf = PageSource.Request.QueryString["ddlMultiBebes"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlClaseMulti"] != null && PageSource.Request.QueryString["ddlClaseMulti"].ToString() != "")
                {
                    sClase = PageSource.Request.QueryString["ddlClaseMulti"].ToString();
                }
                if (PageSource.Request.QueryString["ddlAirMulti"] != null && PageSource.Request.QueryString["ddlAirMulti"].ToString() != "")
                {
                    sAerolinea = PageSource.Request.QueryString["ddlAirMulti"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMoneyMulti"] != null && PageSource.Request.QueryString["ddlMoneyMulti"].ToString() != "")
                {
                    sMoneda = PageSource.Request.QueryString["ddlMoneyMulti"].ToString();
                }
                if (PageSource.Request.QueryString["GrupoOpciones2"] != null && PageSource.Request.QueryString["GrupoOpciones2"].ToString() != "")
                {
                    sEscala = PageSource.Request.QueryString["GrupoOpciones2"].ToString();
                }
                if (bPasa)
                {
                    try
                    {
                        if (sDestino1.Length > 2)
                            sDestino1 = sDestino1.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sDestino2.Length > 2)
                            sDestino2 = sDestino2.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sDestino3.Length > 2)
                            sDestino3 = sDestino3.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sDestino4.Length > 2)
                            sDestino4 = sDestino4.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sDestino5.Length > 2)
                            sDestino5 = sDestino5.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sDestino6.Length > 2)
                            sDestino6 = sDestino6.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen1.Length > 2)
                            sOrigen1 = sOrigen1.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen2.Length > 2)
                            sOrigen2 = sOrigen2.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen3.Length > 2)
                            sOrigen3 = sOrigen3.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen4.Length > 2)
                            sOrigen4 = sOrigen4.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen5.Length > 2)
                            sOrigen5 = sOrigen5.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen6.Length > 2)
                            sOrigen6 = sOrigen6.Substring(0, 3);
                    }
                    catch { }

                    if (sAerolinea.Trim().Length.Equals(2))
                    {
                        lsAerolineas.Add(sAerolinea);
                    }
                    bBargain = clsValidaciones._DROP_BARGAIN(sHora1);

                    List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation =
                           new List<VO_OriginDestinationInformation>();
                    VO_Aeropuerto vo_AeropuertoOrigen = null;
                    VO_Aeropuerto vo_AeropuertoDestino = null;
                    VO_OriginDestinationInformation vo_Ruta = null;

                    VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = new VO_OTA_AirLowFareSearchLLSRQ();
                    #region[VUELOS]
                    if (sDestino1.Length > 2)
                    {
                        sFecha1 = clsValidaciones.ConverYMD(sFecha1, sFormatoFecha, "-");
                        vo_OTA_AirLowFareSearchLLSRQ.BHoras = false;
                        if (!bBargain)
                        {
                            sFecha1 += sHora1;
                            //vo_OTA_AirLowFareSearchLLSRQ.BHoras = true;
                        }

                        vo_AeropuertoOrigen =
                            new VO_Aeropuerto(sOrigen1.Substring(0, 3), "IATA");
                        vo_AeropuertoDestino =
                            new VO_Aeropuerto(sDestino1.Substring(0, 3), "IATA");

                        vo_Ruta =
                            new VO_OriginDestinationInformation
                                (
                                    sFecha1,
                                    null,
                                    null,
                                    null,
                                    vo_AeropuertoOrigen,
                                    vo_AeropuertoDestino,
                                    TipoSegmento.O,
                                    false,
                                    null,
                                    null
                                );
                        lvo_OriginDestinationInformation.Add(vo_Ruta);
                    }

                    if (sDestino2.Length > 2)
                    {
                        sFecha2 = clsValidaciones.ConverYMD(sFecha2, sFormatoFecha, "-");

                        if (bBargain)
                        {
                            bBargain = clsValidaciones._DROP_BARGAIN(sHora2);
                        }

                        if (!bBargain)
                        {
                            sFecha2 += sHora2;
                        }

                        vo_AeropuertoOrigen =
                           new VO_Aeropuerto(sOrigen2.Substring(0, 3), "IATA");
                        vo_AeropuertoDestino =
                            new VO_Aeropuerto(sDestino2.Substring(0, 3), "IATA");
                        //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                        //}
                        //else
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                        //}
                        sDestinoAnt = vo_AeropuertoDestino.SCodigo;

                        vo_Ruta =
                            new VO_OriginDestinationInformation
                                (
                                    sFecha2,
                                    null,
                                    null,
                                    null,
                                    vo_AeropuertoOrigen,
                                    vo_AeropuertoDestino,
                                    TipoSegmento.O,
                                    false,
                                    null,
                                    null
                                );

                        lvo_OriginDestinationInformation.Add(vo_Ruta);
                    }

                    if (sDestino3.Length > 2)
                    {
                        sFecha3 = clsValidaciones.ConverYMD(sFecha3, sFormatoFecha, "-");
                        if (bBargain)
                        {
                            bBargain = clsValidaciones._DROP_BARGAIN(sHora3);
                        }

                        if (!bBargain)
                        {
                            sFecha3 += sHora3;
                        }

                        vo_AeropuertoOrigen =
                           new VO_Aeropuerto(sOrigen3.Substring(0, 3), "IATA");
                        vo_AeropuertoDestino =
                            new VO_Aeropuerto(sDestino3.Substring(0, 3), "IATA");
                        //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                        //}
                        //else
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                        //}
                        sDestinoAnt = vo_AeropuertoDestino.SCodigo;


                        vo_Ruta =
                            new VO_OriginDestinationInformation
                                (
                                    sFecha3,
                                    null,
                                    null,
                                    null,
                                    vo_AeropuertoOrigen,
                                    vo_AeropuertoDestino,
                                    TipoSegmento.O,
                                    false,
                                    null,
                                    null
                                );

                        lvo_OriginDestinationInformation.Add(vo_Ruta);
                    }
                    if (sDestino4.Length > 2)
                    {
                        sFecha4 = clsValidaciones.ConverYMD(sFecha4, sFormatoFecha, "-");

                        if (bBargain)
                        {
                            bBargain = clsValidaciones._DROP_BARGAIN(sHora4);
                        }

                        if (!bBargain)
                        {
                            sFecha4 += sHora4;
                        }

                        vo_AeropuertoOrigen =
                           new VO_Aeropuerto(sOrigen4.Substring(0, 3), "IATA");
                        vo_AeropuertoDestino =
                            new VO_Aeropuerto(sDestino4.Substring(0, 3), "IATA");
                        //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                        //}
                        //else
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                        //}
                        sDestinoAnt = vo_AeropuertoDestino.SCodigo;

                        vo_Ruta =
                            new VO_OriginDestinationInformation
                                (
                                    sFecha4,
                                    null,
                                    null,
                                    null,
                                    vo_AeropuertoOrigen,
                                    vo_AeropuertoDestino,
                                    TipoSegmento.O,
                                    false,
                                    null,
                                    null
                                );

                        lvo_OriginDestinationInformation.Add(vo_Ruta);
                    }
                    if (sDestino5.Length > 2)
                    {
                        sFecha5 = clsValidaciones.ConverYMD(sFecha5, "-");

                        if (bBargain)
                        {
                            bBargain = clsValidaciones._DROP_BARGAIN(sHora5);
                        }

                        if (!bBargain)
                        {
                            sFecha5 += sHora5;
                        }

                        vo_AeropuertoOrigen =
                           new VO_Aeropuerto(sOrigen5.Substring(0, 3), "IATA");
                        vo_AeropuertoDestino =
                            new VO_Aeropuerto(sDestino5.Substring(0, 3), "IATA");
                        //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                        //}
                        //else
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                        //}
                        sDestinoAnt = vo_AeropuertoDestino.SCodigo;

                        vo_Ruta =
                            new VO_OriginDestinationInformation
                                (
                                    sFecha5,
                                    null,
                                    null,
                                    null,
                                    vo_AeropuertoOrigen,
                                    vo_AeropuertoDestino,
                                    TipoSegmento.O,
                                    false,
                                    null,
                                    null
                                );

                        lvo_OriginDestinationInformation.Add(vo_Ruta);
                    }
                    if (sDestino6.Length > 2)
                    {
                        sFecha6 = clsValidaciones.ConverYMD(sFecha6, sFormatoFecha, "-");

                        if (bBargain)
                        {
                            bBargain = clsValidaciones._DROP_BARGAIN(sHora6);
                        }

                        if (!bBargain)
                        {
                            sFecha6 += sHora6;
                        }

                        vo_AeropuertoOrigen =
                           new VO_Aeropuerto(sOrigen6.Substring(0, 3), "IATA");
                        vo_AeropuertoDestino =
                            new VO_Aeropuerto(sDestino6.Substring(0, 3), "IATA");
                        //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                        //}
                        //else
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                        //}
                        sDestinoAnt = vo_AeropuertoDestino.SCodigo;


                        vo_Ruta =
                            new VO_OriginDestinationInformation
                                (
                                    sFecha6,
                                    null,
                                    null,
                                    null,
                                    vo_AeropuertoOrigen,
                                    vo_AeropuertoDestino,
                                    TipoSegmento.O,
                                    false,
                                    null,
                                    null
                                );

                        lvo_OriginDestinationInformation.Add(vo_Ruta);
                    }
                    #endregion

                    vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas = cBuscador.setValidarTrayectos(lvo_OriginDestinationInformation);

                    List<string> lsClase = new List<string>();
                    if (!sClase.Equals("-"))
                    {
                        if (!sClase.Equals("0"))
                        {
                            lsClase.Add(sClase);
                        }
                        else
                        {
                            lsClase.Add("Y");
                        }
                    }
                    else
                    {
                        if (!sClase.Equals("0"))
                        {
                            lsClase.Add(sClase);
                        }
                        else
                        {
                            lsClase.Add("Y");
                        }
                    }
                    vo_OTA_AirLowFareSearchLLSRQ.LsClase = lsClase;
                    string sConvenio = csVuelos.csConvenio();
                    if (!sConvenio.Length.Equals(0))
                        vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada = sConvenio;
                    vo_OTA_AirLowFareSearchLLSRQ.BConFarCalc = true;
                    vo_OTA_AirLowFareSearchLLSRQ.BRetornarMaxResultados = true;
                    vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = lsAerolineas;
                    vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas = sEscala;
                    vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto = Enum_TipoTrayecto.Multidestino;

                    vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion = sMoneda;

                    getPasajerosEdadesMeses(vo_OTA_AirLowFareSearchLLSRQ,
                        PageSource,
                        sAdt,
                        sCnn,
                        sInf);

                    csVuelos clsValidacionesVuelos = new csVuelos();
                    vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo = clsValidacionesVuelos.getValidarTipoTrayecto(lvo_OriginDestinationInformation);
                    try
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo.Equals(Enum_TipoVuelo.Internacional))
                            vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = clsValidacionesVuelos.getValidarTipoSalida(lvo_OriginDestinationInformation);
                        else
                            vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional;
                    }
                    catch { vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional; }
                    try
                    {
                        cBuscador.setProcesarPseudo(vo_OTA_AirLowFareSearchLLSRQ);
                    }
                    catch { }
                    try
                    {
                        vo_OTA_AirLowFareSearchLLSRQ.Ruta = 0;
                    }
                    catch { }

                    clsSesiones.setParametrosAirBargain
                        (
                           vo_OTA_AirLowFareSearchLLSRQ
                        );

                    cBuscador.setProcesarBusqueda(PageSource);
                }
                else
                {
                    csGeneralsPag.Buscador();
                }
            }
            catch { }
        }
        private void BuscarSoloIda(UserControl PageSource)
        {
            try
            {
                string sOrigen = string.Empty;
                string sDestino = string.Empty;
                string sFechaSalida = string.Empty;
                string sHoraSalida = string.Empty;
                string sClase = "Y";
                List<string> lsAerolineas = new List<string>();
                bool bBargain;
                string sAdt = "1";
                string sCnn = "0";
                string sInf = "0";
                string sAerolinea = "0";
                string sEscala = "1";
                bool bPasa = true;

                string sMoneda = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");

                // Tomar los parametros del encabezado
                if (PageSource.Request.QueryString["txt_Multi_O1"] != null && PageSource.Request.QueryString["txt_Multi_O1"].ToString() != "")
                {
                    sOrigen = PageSource.Request.QueryString["txt_Multi_O1"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["txt_Multi_D1"] != null && PageSource.Request.QueryString["txt_Multi_D1"].ToString() != "")
                {
                    sDestino = PageSource.Request.QueryString["txt_Multi_D1"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["txtFechaMultiO1"] != null && PageSource.Request.QueryString["txtFechaMultiO1"].ToString() != "")
                {
                    sFechaSalida = PageSource.Request.QueryString["txtFechaMultiO1"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlMultiHora01"] != null && PageSource.Request.QueryString["ddlMultiHora01"].ToString() != "")
                {
                    sHoraSalida = "T" + PageSource.Request.QueryString["ddlMultiHora01"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiAdultos"] != null && PageSource.Request.QueryString["ddlMultiAdultos"].ToString() != "")
                {
                    sAdt = PageSource.Request.QueryString["ddlMultiAdultos"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlMultiNinios"] != null && PageSource.Request.QueryString["ddlMultiNinios"].ToString() != "")
                {
                    sCnn = PageSource.Request.QueryString["ddlMultiNinios"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlMultiBebes"] != null && PageSource.Request.QueryString["ddlMultiBebes"].ToString() != "")
                {
                    sInf = PageSource.Request.QueryString["ddlMultiBebes"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlClaseMulti"] != null && PageSource.Request.QueryString["ddlClaseMulti"].ToString() != "")
                {
                    sClase = PageSource.Request.QueryString["ddlClaseMulti"].ToString();
                }
                if (PageSource.Request.QueryString["ddlAirMulti"] != null && PageSource.Request.QueryString["ddlAirMulti"].ToString() != "")
                {
                    sAerolinea = PageSource.Request.QueryString["ddlAirMulti"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMoneyMulti"] != null && PageSource.Request.QueryString["ddlMoneyMulti"].ToString() != "")
                {
                    sMoneda = PageSource.Request.QueryString["ddlMoneyMulti"].ToString();
                }
                if (PageSource.Request.QueryString["GrupoOpciones2"] != null && PageSource.Request.QueryString["GrupoOpciones2"].ToString() != "")
                {
                    sEscala = PageSource.Request.QueryString["GrupoOpciones2"].ToString();
                }
                if (bPasa)
                {
                    Utils.Utils oUtilidad = new Utils.Utils();
                    sFechaSalida = clsValidaciones.ConverYMD(sFechaSalida, sFormatoFecha, "-");

                    bBargain = false;
                    try
                    {
                        if (sDestino.Length > 2)
                            sDestino = sDestino.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen.Length > 2)
                            sOrigen = sOrigen.Substring(0, 3);
                    }
                    catch { } lsAerolineas = new List<string>();

                    if (sAerolinea.Trim().Length.Equals(2))
                    {
                        lsAerolineas.Add(sAerolinea);
                    }

                    bBargain = clsValidaciones._DROP_BARGAIN(sHoraSalida);

                    VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = new VO_OTA_AirLowFareSearchLLSRQ();

                    vo_OTA_AirLowFareSearchLLSRQ.BHoras = false;
                    if (!bBargain)
                    {
                        sFechaSalida += sHoraSalida;
                        //vo_OTA_AirLowFareSearchLLSRQ.BHoras = true;
                    }

                    List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation =
                       new List<VO_OriginDestinationInformation>();

                    VO_Aeropuerto vo_AeropuertoOrigen =
                        new VO_Aeropuerto(sOrigen, "IATA");
                    VO_Aeropuerto vo_AeropuertoDestino =
                        new VO_Aeropuerto(sDestino, "IATA");

                    vo_AeropuertoOrigen.SDetalle = sOrigen;
                    vo_AeropuertoDestino.SDetalle = sDestino;

                    VO_OriginDestinationInformation vo_Ruta =
                        new VO_OriginDestinationInformation
                            (
                                sFechaSalida,
                                null,
                                null,
                                null,
                                vo_AeropuertoOrigen,
                                vo_AeropuertoDestino,
                                TipoSegmento.O,
                                false,
                                null,
                                null
                            );

                    lvo_OriginDestinationInformation.Add(vo_Ruta);

                    vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas = lvo_OriginDestinationInformation;

                    List<string> lsClase = new List<string>();
                    if (!sClase.Equals("-"))
                    {
                        if (!sClase.Equals("0"))
                        {
                            lsClase.Add(sClase);
                        }
                        else
                        {
                            lsClase.Add("Y");
                        }
                    }
                    else
                    {
                        if (!sClase.Equals("0"))
                        {
                            lsClase.Add(sClase);
                        }
                        else
                        {
                            lsClase.Add("Y");
                        }
                    }
                    vo_OTA_AirLowFareSearchLLSRQ.LsClase = lsClase;

                    string sConvenio = csVuelos.csConvenio();
                    if (!sConvenio.Length.Equals(0))
                        vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada = sConvenio;
                    vo_OTA_AirLowFareSearchLLSRQ.BConFarCalc = true;
                    vo_OTA_AirLowFareSearchLLSRQ.BRetornarMaxResultados = true;
                    vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = lsAerolineas;
                    vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas = sEscala;
                    vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto = Enum_TipoTrayecto.Ida;

                    vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion = sMoneda;

                    getPasajerosEdadesMeses
                        (vo_OTA_AirLowFareSearchLLSRQ,
                        PageSource,
                        sAdt,
                        sCnn,
                        sInf);

                    csVuelos clsValidacionesVuelos = new csVuelos();
                    vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo = clsValidacionesVuelos.getValidarTipoTrayecto(lvo_OriginDestinationInformation);
                    try
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo.Equals(Enum_TipoVuelo.Internacional))
                            vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = clsValidacionesVuelos.getValidarTipoSalida(lvo_OriginDestinationInformation);
                        else
                            vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional;
                    }
                    catch { vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional; }
                    try
                    {
                        cBuscador.setProcesarPseudo(vo_OTA_AirLowFareSearchLLSRQ);
                    }
                    catch { }
                    try
                    {
                        vo_OTA_AirLowFareSearchLLSRQ.Ruta = 0;
                    }
                    catch { }

                    clsSesiones.setParametrosAirBargain
                       (
                          vo_OTA_AirLowFareSearchLLSRQ
                       );
                    cBuscador.setProcesarBusqueda(PageSource);
                }
                else
                {
                    csGeneralsPag.Buscador();
                }
            }
            catch { }
        }
        private void BuscarIdaVuelta(UserControl PageSource)
        {
            try
            {
                string sOrigen = string.Empty;
                string sDestino = string.Empty;
                string sFechaSalida = string.Empty;
                string sHoraSalida = string.Empty;
                string sFechaRegreso = string.Empty;
                string sHoraRegreso = string.Empty;
                string sClase = "Y";
                List<string> lsAerolineas = new List<string>();
                bool bBargain;
                string sAdt = "1";
                string sCnn = "0";
                string sInf = "0";
                string sAerolinea = "0";
                string sEscala = "1";
                bool bPasa = true;

                string sMoneda = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");

                // Tomar los parametros del encabezado
                if (PageSource.Request.QueryString["txt_Multi_O1"] != null && PageSource.Request.QueryString["txt_Multi_O1"].ToString() != "")
                {
                    sOrigen = PageSource.Request.QueryString["txt_Multi_O1"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["txt_Multi_D1"] != null && PageSource.Request.QueryString["txt_Multi_D1"].ToString() != "")
                {
                    sDestino = PageSource.Request.QueryString["txt_Multi_D1"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["txtFechaMultiO1"] != null && PageSource.Request.QueryString["txtFechaMultiO1"].ToString() != "")
                {
                    sFechaSalida = PageSource.Request.QueryString["txtFechaMultiO1"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["txt2VFechaMulti"] != null && PageSource.Request.QueryString["txt2VFechaMulti"].ToString() != "")
                {
                    sFechaRegreso = PageSource.Request.QueryString["txt2VFechaMulti"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlMultiHora01"] != null && PageSource.Request.QueryString["ddlMultiHora01"].ToString() != "")
                {
                    sHoraSalida = "T" + PageSource.Request.QueryString["ddlMultiHora01"].ToString();
                }
                else
                {
                    sHoraSalida = "T0";
                }
                if (PageSource.Request.QueryString["ddlMultiHoraD2"] != null && PageSource.Request.QueryString["ddlMultiHoraD2"].ToString() != "")
                {
                    sHoraRegreso = "T" + PageSource.Request.QueryString["ddlMultiHoraD2"].ToString();
                }
                else
                {
                    sHoraRegreso = "T0";
                }
                if (PageSource.Request.QueryString["ddlMultiAdultos"] != null && PageSource.Request.QueryString["ddlMultiAdultos"].ToString() != "")
                {
                    sAdt = PageSource.Request.QueryString["ddlMultiAdultos"].ToString().Trim();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlMultiNinios"] != null && PageSource.Request.QueryString["ddlMultiNinios"].ToString() != "")
                {
                    sCnn = PageSource.Request.QueryString["ddlMultiNinios"].ToString().Trim();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlMultiBebes"] != null && PageSource.Request.QueryString["ddlMultiBebes"].ToString() != "")
                {
                    sInf = PageSource.Request.QueryString["ddlMultiBebes"].ToString().Trim();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlClaseMulti"] != null && PageSource.Request.QueryString["ddlClaseMulti"].ToString() != "")
                {
                    sClase = PageSource.Request.QueryString["ddlClaseMulti"].ToString();
                }
                if (PageSource.Request.QueryString["ddlAirMulti"] != null && PageSource.Request.QueryString["ddlAirMulti"].ToString() != "")
                {
                    sAerolinea = PageSource.Request.QueryString["ddlAirMulti"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMoneyMulti"] != null && PageSource.Request.QueryString["ddlMoneyMulti"].ToString() != "")
                {
                    sMoneda = PageSource.Request.QueryString["ddlMoneyMulti"].ToString();
                }
                if (PageSource.Request.QueryString["GrupoOpciones2"] != null && PageSource.Request.QueryString["GrupoOpciones2"].ToString() != "")
                {
                    sEscala = PageSource.Request.QueryString["GrupoOpciones2"].ToString();
                }
                if (bPasa)
                {
                    Utils.Utils oUtilidad = new Utils.Utils();

                    bBargain = false;
                    try
                    {
                        if (sDestino.Length > 2)
                            sDestino = sDestino.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen.Length > 2)
                            sOrigen = sOrigen.Substring(0, 3);
                    }
                    catch { }
                    sFechaSalida = clsValidaciones.ConverYMD(sFechaSalida, sFormatoFecha, "-");
                    sFechaRegreso = clsValidaciones.ConverYMD(sFechaRegreso, sFormatoFecha, "-");
                    bBargain = clsValidaciones._DROP_BARGAIN(sHoraSalida);

                    if (bBargain)
                    {
                        bBargain = clsValidaciones._DROP_BARGAIN(sHoraRegreso);
                    }

                    if (!bBargain)
                    {
                        bBargain = clsValidaciones._DROP_BARGAIN(sHoraRegreso);
                    }

                    if (sAerolinea.Trim().Length.Equals(2))
                    {
                        lsAerolineas.Add(sAerolinea);
                    }
                    VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = new VO_OTA_AirLowFareSearchLLSRQ();
                    clsSesiones.setParametrosAirHoras(null);
                    vo_OTA_AirLowFareSearchLLSRQ.BHoras = false;
                    if (!bBargain)
                    {
                        sFechaSalida += sHoraSalida;
                        sFechaRegreso += sHoraRegreso;
                        vo_OTA_AirLowFareSearchLLSRQ.BHoras = true;
                    }
                    List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation =
                        new List<VO_OriginDestinationInformation>();

                    VO_Aeropuerto vo_AeropuertoOrigen =
                        new VO_Aeropuerto(sOrigen, "IATA");
                    VO_Aeropuerto vo_AeropuertoDestino =
                        new VO_Aeropuerto(sDestino, "IATA");

                    vo_AeropuertoOrigen.SDetalle = sOrigen;
                    vo_AeropuertoDestino.SDetalle = sDestino;

                    VO_OriginDestinationInformation vo_Ruta =
                        new VO_OriginDestinationInformation
                            (
                                sFechaSalida,
                                null,
                                null,
                                null,
                                vo_AeropuertoOrigen,
                                vo_AeropuertoDestino,
                                TipoSegmento.O,
                                false,
                                null,
                                null
                            );
                    lvo_OriginDestinationInformation.Add(vo_Ruta);

                    //VUELO REGRESO
                    vo_Ruta =
                        new VO_OriginDestinationInformation
                            (
                                sFechaRegreso,
                                null,
                                null,
                                null,
                                vo_AeropuertoDestino,
                                vo_AeropuertoOrigen,
                                TipoSegmento.O,
                                false,
                                null,
                                null
                            );

                    lvo_OriginDestinationInformation.Add(vo_Ruta);

                    vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas = lvo_OriginDestinationInformation;

                    List<string> lsClase = new List<string>();
                    if (!sClase.Equals("-"))
                    {
                        if (!sClase.Equals("0"))
                        {
                            lsClase.Add(sClase);
                        }
                        else
                        {
                            lsClase.Add("Y");
                        }
                    }
                    else
                    {
                        if (!sClase.Equals("0"))
                        {
                            lsClase.Add(sClase);
                        }
                        else
                        {
                            lsClase.Add("Y");
                        }
                    }
                    vo_OTA_AirLowFareSearchLLSRQ.LsClase = lsClase;

                    string sConvenio = csVuelos.csConvenio();
                    if (!sConvenio.Length.Equals(0))
                        vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada = sConvenio;
                    vo_OTA_AirLowFareSearchLLSRQ.BConFarCalc = true;
                    vo_OTA_AirLowFareSearchLLSRQ.BRetornarMaxResultados = true;
                    vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = lsAerolineas;
                    vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas = sEscala;
                    vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto = Enum_TipoTrayecto.IdaRegreso;

                    vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion = sMoneda;

                    /*OBTENEMOS LAS EDADES DE LOS NIÑOS Y LOS INFANTES*/
                    getPasajerosEdadesMeses(vo_OTA_AirLowFareSearchLLSRQ,
                        PageSource,
                        sAdt, sCnn, sInf);

                    csVuelos clsValidacionesVuelos = new csVuelos();
                    vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo = clsValidacionesVuelos.getValidarTipoTrayecto(lvo_OriginDestinationInformation);
                    try
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo.Equals(Enum_TipoVuelo.Internacional))
                            vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = clsValidacionesVuelos.getValidarTipoSalida(lvo_OriginDestinationInformation);
                        else
                            vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional;
                    }
                    catch { vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional; }
                    try
                    {
                        cBuscador.setProcesarPseudo(vo_OTA_AirLowFareSearchLLSRQ);
                    }
                    catch { }
                    try
                    {
                        vo_OTA_AirLowFareSearchLLSRQ.Ruta = 0;
                    }
                    catch { }
                    clsSesiones.setParametrosAirBargain
                       (
                          vo_OTA_AirLowFareSearchLLSRQ
                       );

                    cBuscador.setProcesarBusqueda(PageSource);
                }
                else
                {
                    csGeneralsPag.Buscador();
                }
            }
            catch { }
        }
        private void BuscarMultiDestinoOferta(UserControl PageSource, DataSet dsData)
        {
            try
            {
                string sAerolinea = "0";
                bool bBargain = false;
                List<string> lsAerolineas = new List<string>();
                Utils.Utils oUtilidad = new Utils.Utils();
                string sDestinoAnt = string.Empty;

                string sOrigen1 = string.Empty;
                string sOrigen2 = string.Empty;
                string sOrigen3 = string.Empty;
                string sOrigen4 = string.Empty;
                string sOrigen5 = string.Empty;
                string sOrigen6 = string.Empty;
                string sDestino1 = string.Empty;
                string sDestino2 = string.Empty;
                string sDestino3 = string.Empty;
                string sDestino4 = string.Empty;
                string sDestino5 = string.Empty;
                string sDestino6 = string.Empty;
                string sFecha1 = string.Empty;
                string sFecha2 = string.Empty;
                string sFecha3 = string.Empty;
                string sFecha4 = string.Empty;
                string sFecha5 = string.Empty;
                string sFecha6 = string.Empty;
                string sHora1 = string.Empty;
                string sHora2 = string.Empty;
                string sHora3 = string.Empty;
                string sHora4 = string.Empty;
                string sHora5 = string.Empty;
                string sHora6 = string.Empty;

                string sClase = "Y";
                string sAdt = "1";
                string sCnn = "0";
                string sInf = "0";
                string sEscala = "1";
                bool bPasa = true;

                string sMoneda = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");

                // Tomar los parametros del encabezado
                if (PageSource.Request.QueryString["txt_Multi_O1"] != null && PageSource.Request.QueryString["txt_Multi_O1"].ToString() != "")
                {
                    sOrigen1 = PageSource.Request.QueryString["txt_Multi_O1"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["txt_Multi_O2"] != null && PageSource.Request.QueryString["txt_Multi_O2"].ToString() != "")
                {
                    sOrigen2 = PageSource.Request.QueryString["txt_Multi_O2"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_O3"] != null && PageSource.Request.QueryString["txt_Multi_O3"].ToString() != "")
                {
                    sOrigen3 = PageSource.Request.QueryString["txt_Multi_O3"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_O4"] != null && PageSource.Request.QueryString["txt_Multi_O4"].ToString() != "")
                {
                    sOrigen4 = PageSource.Request.QueryString["txt_Multi_O4"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_O5"] != null && PageSource.Request.QueryString["txt_Multi_O5"].ToString() != "")
                {
                    sOrigen5 = PageSource.Request.QueryString["txt_Multi_O5"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_O6"] != null && PageSource.Request.QueryString["txt_Multi_O6"].ToString() != "")
                {
                    sOrigen6 = PageSource.Request.QueryString["txt_Multi_O6"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_D1"] != null && PageSource.Request.QueryString["txt_Multi_D1"].ToString() != "")
                {
                    sDestino1 = PageSource.Request.QueryString["txt_Multi_D1"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["txt_Multi_D2"] != null && PageSource.Request.QueryString["txt_Multi_D2"].ToString() != "")
                {
                    sDestino2 = PageSource.Request.QueryString["txt_Multi_D2"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_D3"] != null && PageSource.Request.QueryString["txt_Multi_D3"].ToString() != "")
                {
                    sDestino3 = PageSource.Request.QueryString["txt_Multi_D3"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_D4"] != null && PageSource.Request.QueryString["txt_Multi_D4"].ToString() != "")
                {
                    sDestino4 = PageSource.Request.QueryString["txt_Multi_D4"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_D5"] != null && PageSource.Request.QueryString["txt_Multi_D5"].ToString() != "")
                {
                    sDestino5 = PageSource.Request.QueryString["txt_Multi_D5"].ToString();
                }
                if (PageSource.Request.QueryString["txt_Multi_D6"] != null && PageSource.Request.QueryString["txt_Multi_D6"].ToString() != "")
                {
                    sDestino6 = PageSource.Request.QueryString["txt_Multi_D6"].ToString();
                }
                if (PageSource.Request.QueryString["txtFechaMultiO1"] != null && PageSource.Request.QueryString["txtFechaMultiO1"].ToString() != "")
                {
                    sFecha1 = PageSource.Request.QueryString["txtFechaMultiO1"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["txtFechaMultiO2"] != null && PageSource.Request.QueryString["txtFechaMultiO2"].ToString() != "")
                {
                    sFecha2 = PageSource.Request.QueryString["txtFechaMultiO2"].ToString();
                }
                if (PageSource.Request.QueryString["txtFechaMultiO3"] != null && PageSource.Request.QueryString["txtFechaMultiO3"].ToString() != "")
                {
                    sFecha3 = PageSource.Request.QueryString["txtFechaMultiO3"].ToString();
                }
                if (PageSource.Request.QueryString["txtFechaMultiO4"] != null && PageSource.Request.QueryString["txtFechaMultiO4"].ToString() != "")
                {
                    sFecha4 = PageSource.Request.QueryString["txtFechaMultiO4"].ToString();
                }
                if (PageSource.Request.QueryString["txtFechaMultiO5"] != null && PageSource.Request.QueryString["txtFechaMultiO5"].ToString() != "")
                {
                    sFecha5 = PageSource.Request.QueryString["txtFechaMultiO5"].ToString();
                }
                if (PageSource.Request.QueryString["txtFechaMultiO6"] != null && PageSource.Request.QueryString["txtFechaMultiO6"].ToString() != "")
                {
                    sFecha6 = PageSource.Request.QueryString["txtFechaMultiO6"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiHora01"] != null && PageSource.Request.QueryString["ddlMultiHora01"].ToString() != "")
                {
                    sHora1 = "T" + PageSource.Request.QueryString["ddlMultiHora01"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiHoraO2"] != null && PageSource.Request.QueryString["ddlMultiHoraO2"].ToString() != "")
                {
                    sHora2 = "T" + PageSource.Request.QueryString["ddlMultiHoraO2"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiHoraO3"] != null && PageSource.Request.QueryString["ddlMultiHoraO3"].ToString() != "")
                {
                    sHora3 = "T" + PageSource.Request.QueryString["ddlMultiHoraO3"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiHoraO4"] != null && PageSource.Request.QueryString["ddlMultiHoraO4"].ToString() != "")
                {
                    sHora4 = "T" + PageSource.Request.QueryString["ddlMultiHoraO4"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiHoraO5"] != null && PageSource.Request.QueryString["ddlMultiHoraO5"].ToString() != "")
                {
                    sHora5 = "T" + PageSource.Request.QueryString["ddlMultiHoraO5"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiHoraO6"] != null && PageSource.Request.QueryString["ddlMultiHoraO6"].ToString() != "")
                {
                    sHora6 = "T" + PageSource.Request.QueryString["ddlMultiHoraO6"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMultiAdultos"] != null && PageSource.Request.QueryString["ddlMultiAdultos"].ToString() != "")
                {
                    sAdt = PageSource.Request.QueryString["ddlMultiAdultos"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlMultiNinios"] != null && PageSource.Request.QueryString["ddlMultiNinios"].ToString() != "")
                {
                    sCnn = PageSource.Request.QueryString["ddlMultiNinios"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlMultiBebes"] != null && PageSource.Request.QueryString["ddlMultiBebes"].ToString() != "")
                {
                    sInf = PageSource.Request.QueryString["ddlMultiBebes"].ToString();
                }
                else
                {
                    bPasa = false;
                }
                if (PageSource.Request.QueryString["ddlClaseMulti"] != null && PageSource.Request.QueryString["ddlClaseMulti"].ToString() != "")
                {
                    sClase = PageSource.Request.QueryString["ddlClaseMulti"].ToString();
                }
                if (PageSource.Request.QueryString["ddlAirMulti"] != null && PageSource.Request.QueryString["ddlAirMulti"].ToString() != "")
                {
                    sAerolinea = PageSource.Request.QueryString["ddlAirMulti"].ToString();
                }
                if (PageSource.Request.QueryString["ddlMoneyMulti"] != null && PageSource.Request.QueryString["ddlMoneyMulti"].ToString() != "")
                {
                    sMoneda = PageSource.Request.QueryString["ddlMoneyMulti"].ToString();
                }
                if (PageSource.Request.QueryString["GrupoOpciones2"] != null && PageSource.Request.QueryString["GrupoOpciones2"].ToString() != "")
                {
                    sEscala = PageSource.Request.QueryString["GrupoOpciones2"].ToString();
                }
                if (bPasa)
                {
                    try
                    {
                        if (sDestino1.Length > 2)
                            sDestino1 = sDestino1.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sDestino2.Length > 2)
                            sDestino2 = sDestino2.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sDestino3.Length > 2)
                            sDestino3 = sDestino3.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sDestino4.Length > 2)
                            sDestino4 = sDestino4.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sDestino5.Length > 2)
                            sDestino5 = sDestino5.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sDestino6.Length > 2)
                            sDestino6 = sDestino6.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen1.Length > 2)
                            sOrigen1 = sOrigen1.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen2.Length > 2)
                            sOrigen2 = sOrigen2.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen3.Length > 2)
                            sOrigen3 = sOrigen3.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen4.Length > 2)
                            sOrigen4 = sOrigen4.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen5.Length > 2)
                            sOrigen5 = sOrigen5.Substring(0, 3);
                    }
                    catch { }
                    try
                    {
                        if (sOrigen6.Length > 2)
                            sOrigen6 = sOrigen6.Substring(0, 3);
                    }
                    catch { }

                    if (sAerolinea.Trim().Length.Equals(2))
                    {
                        lsAerolineas.Add(sAerolinea);
                    }
                    bBargain = clsValidaciones._DROP_BARGAIN(sHora1);

                    List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation =
                           new List<VO_OriginDestinationInformation>();
                    VO_Aeropuerto vo_AeropuertoOrigen = null;
                    VO_Aeropuerto vo_AeropuertoDestino = null;
                    VO_OriginDestinationInformation vo_Ruta = null;

                    VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = new VO_OTA_AirLowFareSearchLLSRQ();
                    #region[VUELOS]
                    if (sDestino1.Length > 2)
                    {
                        sFecha1 = clsValidaciones.ConverYMD(sFecha1, sFormatoFecha, "-");
                        vo_OTA_AirLowFareSearchLLSRQ.BHoras = false;
                        if (!bBargain)
                        {
                            sFecha1 += sHora1;
                            //vo_OTA_AirLowFareSearchLLSRQ.BHoras = true;
                        }

                        vo_AeropuertoOrigen =
                            new VO_Aeropuerto(sOrigen1.Substring(0, 3), "IATA");
                        vo_AeropuertoDestino =
                            new VO_Aeropuerto(sDestino1.Substring(0, 3), "IATA");

                        vo_Ruta =
                            new VO_OriginDestinationInformation
                                (
                                    sFecha1,
                                    null,
                                    null,
                                    null,
                                    vo_AeropuertoOrigen,
                                    vo_AeropuertoDestino,
                                    TipoSegmento.O,
                                    false,
                                    null,
                                    null
                                );
                        lvo_OriginDestinationInformation.Add(vo_Ruta);
                    }

                    if (sDestino2.Length > 2)
                    {
                        sFecha2 = clsValidaciones.ConverYMD(sFecha2, sFormatoFecha, "-");

                        if (bBargain)
                        {
                            bBargain = clsValidaciones._DROP_BARGAIN(sHora2);
                        }

                        if (!bBargain)
                        {
                            sFecha2 += sHora2;
                        }

                        vo_AeropuertoOrigen =
                           new VO_Aeropuerto(sOrigen2.Substring(0, 3), "IATA");
                        vo_AeropuertoDestino =
                            new VO_Aeropuerto(sDestino2.Substring(0, 3), "IATA");
                        //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                        //}
                        //else
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                        //}
                        sDestinoAnt = vo_AeropuertoDestino.SCodigo;

                        vo_Ruta =
                            new VO_OriginDestinationInformation
                                (
                                    sFecha2,
                                    null,
                                    null,
                                    null,
                                    vo_AeropuertoOrigen,
                                    vo_AeropuertoDestino,
                                    TipoSegmento.O,
                                    false,
                                    null,
                                    null
                                );

                        lvo_OriginDestinationInformation.Add(vo_Ruta);
                    }

                    if (sDestino3.Length > 2)
                    {
                        sFecha3 = clsValidaciones.ConverYMD(sFecha3, sFormatoFecha, "-");
                        if (bBargain)
                        {
                            bBargain = clsValidaciones._DROP_BARGAIN(sHora3);
                        }

                        if (!bBargain)
                        {
                            sFecha3 += sHora3;
                        }

                        vo_AeropuertoOrigen =
                           new VO_Aeropuerto(sOrigen3.Substring(0, 3), "IATA");
                        vo_AeropuertoDestino =
                            new VO_Aeropuerto(sDestino3.Substring(0, 3), "IATA");
                        //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                        //}
                        //else
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                        //}
                        sDestinoAnt = vo_AeropuertoDestino.SCodigo;


                        vo_Ruta =
                            new VO_OriginDestinationInformation
                                (
                                    sFecha3,
                                    null,
                                    null,
                                    null,
                                    vo_AeropuertoOrigen,
                                    vo_AeropuertoDestino,
                                    TipoSegmento.O,
                                    false,
                                    null,
                                    null
                                );

                        lvo_OriginDestinationInformation.Add(vo_Ruta);
                    }
                    if (sDestino4.Length > 2)
                    {
                        sFecha4 = clsValidaciones.ConverYMD(sFecha4, sFormatoFecha, "-");

                        if (bBargain)
                        {
                            bBargain = clsValidaciones._DROP_BARGAIN(sHora4);
                        }

                        if (!bBargain)
                        {
                            sFecha4 += sHora4;
                        }

                        vo_AeropuertoOrigen =
                           new VO_Aeropuerto(sOrigen4.Substring(0, 3), "IATA");
                        vo_AeropuertoDestino =
                            new VO_Aeropuerto(sDestino4.Substring(0, 3), "IATA");
                        //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                        //}
                        //else
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                        //}
                        sDestinoAnt = vo_AeropuertoDestino.SCodigo;

                        vo_Ruta =
                            new VO_OriginDestinationInformation
                                (
                                    sFecha4,
                                    null,
                                    null,
                                    null,
                                    vo_AeropuertoOrigen,
                                    vo_AeropuertoDestino,
                                    TipoSegmento.O,
                                    false,
                                    null,
                                    null
                                );

                        lvo_OriginDestinationInformation.Add(vo_Ruta);
                    }
                    if (sDestino5.Length > 2)
                    {
                        sFecha5 = clsValidaciones.ConverYMD(sFecha5, "-");

                        if (bBargain)
                        {
                            bBargain = clsValidaciones._DROP_BARGAIN(sHora5);
                        }

                        if (!bBargain)
                        {
                            sFecha5 += sHora5;
                        }

                        vo_AeropuertoOrigen =
                           new VO_Aeropuerto(sOrigen5.Substring(0, 3), "IATA");
                        vo_AeropuertoDestino =
                            new VO_Aeropuerto(sDestino5.Substring(0, 3), "IATA");
                        //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                        //}
                        //else
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                        //}
                        sDestinoAnt = vo_AeropuertoDestino.SCodigo;

                        vo_Ruta =
                            new VO_OriginDestinationInformation
                                (
                                    sFecha5,
                                    null,
                                    null,
                                    null,
                                    vo_AeropuertoOrigen,
                                    vo_AeropuertoDestino,
                                    TipoSegmento.O,
                                    false,
                                    null,
                                    null
                                );

                        lvo_OriginDestinationInformation.Add(vo_Ruta);
                    }
                    if (sDestino6.Length > 2)
                    {
                        sFecha6 = clsValidaciones.ConverYMD(sFecha6, sFormatoFecha, "-");

                        if (bBargain)
                        {
                            bBargain = clsValidaciones._DROP_BARGAIN(sHora6);
                        }

                        if (!bBargain)
                        {
                            sFecha6 += sHora6;
                        }

                        vo_AeropuertoOrigen =
                           new VO_Aeropuerto(sOrigen6.Substring(0, 3), "IATA");
                        vo_AeropuertoDestino =
                            new VO_Aeropuerto(sDestino6.Substring(0, 3), "IATA");
                        //if (sDestinoAnt.Equals(vo_AeropuertoOrigen.SCodigo))
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O;
                        //}
                        //else
                        //{
                        //    oTPA_ExtensionsSegmentTypeCode = OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK;
                        //}
                        sDestinoAnt = vo_AeropuertoDestino.SCodigo;


                        vo_Ruta =
                            new VO_OriginDestinationInformation
                                (
                                    sFecha6,
                                    null,
                                    null,
                                    null,
                                    vo_AeropuertoOrigen,
                                    vo_AeropuertoDestino,
                                    TipoSegmento.O,
                                    false,
                                    null,
                                    null
                                );

                        lvo_OriginDestinationInformation.Add(vo_Ruta);
                    }
                    #endregion

                    vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas = cBuscador.setValidarTrayectos(lvo_OriginDestinationInformation);

                    vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda = Enum_OrigenBusqueda.Ofertas;
                    if (clsValidaciones.GetKeyOrAdd("eOfertaFija", "False").ToUpper().Equals("TRUE"))
                        vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda = Enum_OrigenBusqueda.OfertasFijas;

                    List<string> lsClase = new List<string>();
                    if (!sClase.Equals("-"))
                    {
                        if (!sClase.Equals("0"))
                        {
                            lsClase.Add(sClase);
                        }
                        else
                        {
                            lsClase.Add("Y");
                        }
                    }
                    else
                    {
                        if (!sClase.Equals("0"))
                        {
                            lsClase.Add(sClase);
                        }
                        else
                        {
                            lsClase.Add("Y");
                        }
                    }
                    vo_OTA_AirLowFareSearchLLSRQ.LsClase = lsClase;
                    string sConvenio = csVuelos.csConvenio();
                    if (!sConvenio.Length.Equals(0))
                        vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada = sConvenio;
                    vo_OTA_AirLowFareSearchLLSRQ.BConFarCalc = true;
                    vo_OTA_AirLowFareSearchLLSRQ.BRetornarMaxResultados = true;
                    vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = lsAerolineas;
                    vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas = sEscala;
                    vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto = Enum_TipoTrayecto.Multidestino;

                    vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion = sMoneda;

                    getPasajerosEdadesMeses(vo_OTA_AirLowFareSearchLLSRQ,
                        PageSource,
                        sAdt,
                        sCnn,
                        sInf);

                    csVuelos clsValidacionesVuelos = new csVuelos();
                    vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo = clsValidacionesVuelos.getValidarTipoTrayecto(lvo_OriginDestinationInformation);
                    try
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo.Equals(Enum_TipoVuelo.Internacional))
                            vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = clsValidacionesVuelos.getValidarTipoSalida(lvo_OriginDestinationInformation);
                        else
                            vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional;
                    }
                    catch { vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional; }
                    try
                    {
                        cBuscador.setProcesarPseudo(vo_OTA_AirLowFareSearchLLSRQ);
                    }
                    catch { }
                    try
                    {
                        vo_OTA_AirLowFareSearchLLSRQ.Ruta = 0;
                    }
                    catch { }

                    clsSesiones.setParametrosAirBargain
                        (
                           vo_OTA_AirLowFareSearchLLSRQ
                        );

                    cBuscador.setProcesarBusqueda(PageSource);
                }
                else
                {
                    csGeneralsPag.Buscador();
                }
            }
            catch { }
        }
        private void BuscarSoloIdaOferta(UserControl PageSource, DataSet dsData)
        {
            try
            {
                string sOrigen = string.Empty;
                string sDestino = string.Empty;
                string sFechaSalida = string.Empty;
                string sHoraSalida = string.Empty;
                string sDetalleOrigen = string.Empty;
                string sDetalleDestino = string.Empty;

                string sClase = "Y";
                List<string> lsAerolineas = new List<string>();
                bool bBargain;
                string sAdt = "1";
                string sCnn = "0";
                string sInf = "0";
                //string sAerolinea = "0";
                //string sEscala = "1";
                //bool bPasa = true;

                string sMoneda = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                bool bFechaBlanco = false;
                if (clsValidaciones.GetKeyOrAdd("FechaOfertasBlanco", "False").ToUpper().Equals("TRUE"))
                {
                    bFechaBlanco = true;
                }
                sOrigen = dsData.Tables[0].Rows[0]["strOrigen"].ToString();
                sDestino = dsData.Tables[0].Rows[0]["strDestino"].ToString();
                sDetalleOrigen = dsData.Tables[0].Rows[0]["strDetalleOrigen"].ToString();
                sDetalleDestino = dsData.Tables[0].Rows[0]["strDetalleDestino"].ToString();
                if (!bFechaBlanco)
                {
                    sFechaSalida = dsData.Tables[0].Rows[0]["dtmVigenciaViajeIni"].ToString();
                }

                bBargain = false;
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = new VO_OTA_AirLowFareSearchLLSRQ();

                vo_OTA_AirLowFareSearchLLSRQ.BHoras = false;

                List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation =
                   new List<VO_OriginDestinationInformation>();

                VO_Aeropuerto vo_AeropuertoOrigen =
                    new VO_Aeropuerto(sOrigen, "IATA");
                vo_AeropuertoOrigen.SDetalle = sDetalleOrigen;

                VO_Aeropuerto vo_AeropuertoDestino =
                    new VO_Aeropuerto(sDestino, "IATA");
                vo_AeropuertoDestino.SDetalle = sDetalleDestino;

                VO_OriginDestinationInformation vo_Ruta =
                    new VO_OriginDestinationInformation
                        (
                            sFechaSalida,
                            null,
                            null,
                            null,
                            vo_AeropuertoOrigen,
                            vo_AeropuertoDestino,
                            TipoSegmento.O,
                            false,
                            null,
                            null
                        );

                lvo_OriginDestinationInformation.Add(vo_Ruta);

                vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas = lvo_OriginDestinationInformation;

                vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda = Enum_OrigenBusqueda.Ofertas;
                if (clsValidaciones.GetKeyOrAdd("eOfertaFija", "False").ToUpper().Equals("TRUE"))
                    vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda = Enum_OrigenBusqueda.OfertasFijas;

                List<string> lsClase = new List<string>();
                if (!sClase.Equals("-"))
                {
                    if (!sClase.Equals("0"))
                    {
                        lsClase.Add(sClase);
                    }
                    else
                    {
                        lsClase.Add("Y");
                    }
                }
                else
                {
                    if (!sClase.Equals("0"))
                    {
                        lsClase.Add(sClase);
                    }
                    else
                    {
                        lsClase.Add("Y");
                    }
                }
                vo_OTA_AirLowFareSearchLLSRQ.LsClase = lsClase;

                string sConvenio = csVuelos.csConvenio();
                if (!sConvenio.Length.Equals(0))
                    vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada = sConvenio;
                vo_OTA_AirLowFareSearchLLSRQ.BConFarCalc = true;
                vo_OTA_AirLowFareSearchLLSRQ.BRetornarMaxResultados = true;
                vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto = Enum_TipoTrayecto.Ida;

                vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion = sMoneda;

                bool bEntraNormal = true;
                /*OBTENEMOS LAS EDADES DE LOS NIÑOS Y LOS INFANTES*/
                try
                {
                    if (csGeneralsPag.Oferta())
                    {
                        if (HttpContext.Current.Request.QueryString["IdOferta"] != null)
                        {
                            new csBuscador().getPasajerosEdadesMesesPlanes(vo_OTA_AirLowFareSearchLLSRQ,
                                    PageSource,
                                    sAdt, sCnn, sInf, HttpContext.Current.Request.QueryString["IdOferta"].ToString());
                            if (clsValidaciones.GetKeyOrAdd("bLiquidaVuelosNormal", "False").ToUpper().Equals("TRUE"))
                                vo_OTA_AirLowFareSearchLLSRQ.BHoras = true;
                            bEntraNormal = false;
                        }
                    }
                }
                catch { }
                if (bEntraNormal)
                {
                    getPasajerosEdadesMeses(vo_OTA_AirLowFareSearchLLSRQ,
                        PageSource,
                        sAdt, sCnn, sInf);
                }

                csVuelos clsValidacionesVuelos = new csVuelos();
                vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo = clsValidacionesVuelos.getValidarTipoTrayecto(lvo_OriginDestinationInformation);
                try
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo.Equals(Enum_TipoVuelo.Internacional))
                        vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = clsValidacionesVuelos.getValidarTipoSalida(lvo_OriginDestinationInformation);
                    else
                        vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional;
                }
                catch { vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional; }
                try
                {
                    cBuscador.setProcesarPseudo(vo_OTA_AirLowFareSearchLLSRQ);
                }
                catch { }
                try
                {
                    vo_OTA_AirLowFareSearchLLSRQ.Ruta = 0;
                }
                catch { }

                clsSesiones.setParametrosAirBargain
                   (
                      vo_OTA_AirLowFareSearchLLSRQ
                   );
            }
            catch { }
        }
        private void BuscarIdaVueltaOferta(UserControl PageSource, DataSet dsData)
        {
            try
            {
                string sOrigen = string.Empty;
                string sDestino = string.Empty;
                string sDetalleOrigen = string.Empty;
                string sDetalleDestino = string.Empty;
                string sFechaSalida = string.Empty;
                string sHoraSalida = string.Empty;
                string sFechaRegreso = string.Empty;
                string sHoraRegreso = string.Empty;
                string sClase = "Y";
                //bool bBargain;
                string sAdt = "1";
                string sCnn = "0";
                string sInf = "0";
                //string sAerolinea = "0";
                //string sEscala = "1";
                // bPasa = true;
                bool bFechaBlanco = false;
                if (clsValidaciones.GetKeyOrAdd("FechaOfertasBlanco", "False").ToUpper().Equals("TRUE"))
                {
                    bFechaBlanco = true;
                }
                string sMoneda = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");

                sOrigen = dsData.Tables[0].Rows[0]["strOrigen"].ToString();
                sDestino = dsData.Tables[0].Rows[0]["strDestino"].ToString();
                sDetalleOrigen = dsData.Tables[0].Rows[0]["strDetalleOrigen"].ToString();
                sDetalleDestino = dsData.Tables[0].Rows[0]["strDetalleDestino"].ToString();
                if (!bFechaBlanco)
                {
                    sFechaSalida = dsData.Tables[0].Rows[0]["dtmVigenciaViajeIni"].ToString();
                    sFechaRegreso = dsData.Tables[0].Rows[0]["dtmVigenciaViajeFin"].ToString();
                }
                Utils.Utils oUtilidad = new Utils.Utils();

                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = new VO_OTA_AirLowFareSearchLLSRQ();
                clsSesiones.setParametrosAirHoras(null);
                vo_OTA_AirLowFareSearchLLSRQ.BHoras = false;

                List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation =
                    new List<VO_OriginDestinationInformation>();

                VO_Aeropuerto vo_AeropuertoOrigen =
                    new VO_Aeropuerto(sOrigen, "IATA");
                vo_AeropuertoOrigen.SDetalle = sDetalleOrigen;

                VO_Aeropuerto vo_AeropuertoDestino =
                    new VO_Aeropuerto(sDestino, "IATA");
                vo_AeropuertoDestino.SDetalle = sDetalleDestino;
                VO_OriginDestinationInformation vo_Ruta =
                    new VO_OriginDestinationInformation
                        (
                            sFechaSalida,
                            null,
                            null,
                            null,
                            vo_AeropuertoOrigen,
                            vo_AeropuertoDestino,
                            TipoSegmento.O,
                            false,
                            null,
                            null
                        );
                lvo_OriginDestinationInformation.Add(vo_Ruta);

                //VUELO REGRESO
                vo_Ruta =
                    new VO_OriginDestinationInformation
                        (
                            sFechaRegreso,
                            null,
                            null,
                            null,
                            vo_AeropuertoDestino,
                            vo_AeropuertoOrigen,
                            TipoSegmento.O,
                            false,
                            null,
                            null
                        );

                lvo_OriginDestinationInformation.Add(vo_Ruta);

                vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas = lvo_OriginDestinationInformation;

                vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda = Enum_OrigenBusqueda.Ofertas;
                if (clsValidaciones.GetKeyOrAdd("eOfertaFija", "False").ToUpper().Equals("TRUE"))
                    vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda = Enum_OrigenBusqueda.OfertasFijas;

                List<string> lsClase = new List<string>();
                if (!sClase.Equals("-"))
                {
                    if (!sClase.Equals("0"))
                    {
                        lsClase.Add(sClase);
                    }
                    else
                    {
                        lsClase.Add("Y");
                    }
                }
                else
                {
                    if (!sClase.Equals("0"))
                    {
                        lsClase.Add(sClase);
                    }
                    else
                    {
                        lsClase.Add("Y");
                    }
                }
                vo_OTA_AirLowFareSearchLLSRQ.LsClase = lsClase;

                string sConvenio = csVuelos.csConvenio();
                if (!sConvenio.Length.Equals(0))
                    vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada = sConvenio;
                vo_OTA_AirLowFareSearchLLSRQ.BConFarCalc = true;
                vo_OTA_AirLowFareSearchLLSRQ.BRetornarMaxResultados = true;
                vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto = Enum_TipoTrayecto.IdaRegreso;

                vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion = sMoneda;

                /*OBTENEMOS LAS EDADES DE LOS NIÑOS Y LOS INFANTES*/
                //getPasajerosEdadesMeses(vo_OTA_AirLowFareSearchLLSRQ,
                //    PageSource,
                //    sAdt, sCnn, sInf);
                bool bEntraNormal = true;
                /*OBTENEMOS LAS EDADES DE LOS NIÑOS Y LOS INFANTES*/
                try
                {
                    if (csGeneralsPag.Oferta())
                    {
                        if (HttpContext.Current.Request.QueryString["IdOferta"] != null)
                        {
                            new csBuscador().getPasajerosEdadesMesesPlanes(vo_OTA_AirLowFareSearchLLSRQ,
                                    PageSource,
                                    sAdt, sCnn, sInf, HttpContext.Current.Request.QueryString["IdOferta"].ToString());
                            if (clsValidaciones.GetKeyOrAdd("bLiquidaVuelosNormal", "False").ToUpper().Equals("TRUE"))
                                vo_OTA_AirLowFareSearchLLSRQ.BHoras = true;
                            bEntraNormal = false;
                        }
                    }
                }
                catch { }
                if (bEntraNormal)
                {
                    getPasajerosEdadesMeses(vo_OTA_AirLowFareSearchLLSRQ,
                        PageSource,
                        sAdt, sCnn, sInf);
                }
                csVuelos clsValidacionesVuelos = new csVuelos();
                vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo = clsValidacionesVuelos.getValidarTipoTrayecto(lvo_OriginDestinationInformation);
                try
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo.Equals(Enum_TipoVuelo.Internacional))
                        vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = clsValidacionesVuelos.getValidarTipoSalida(lvo_OriginDestinationInformation);
                    else
                        vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional;
                }
                catch { vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida = Enum_TipoVuelo.Nacional; }
                try
                {
                    cBuscador.setProcesarPseudo(vo_OTA_AirLowFareSearchLLSRQ);
                }
                catch { }
                try
                {
                    vo_OTA_AirLowFareSearchLLSRQ.Ruta = 0;
                }
                catch { }
                clsSesiones.setParametrosAirBargain
                   (
                      vo_OTA_AirLowFareSearchLLSRQ
                   );
            }
            catch { }
        }
        private void getPasajerosEdadesMeses(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ, UserControl PageSource, string sAdt, string sCnn, string sInf)
        {
            VO_Pasajero vo_Pasajero = null;
            List<VO_Pasajero> lvo_Pasajeros = new List<VO_Pasajero>();
            if (clsValidaciones.getValidarString(vo_OTA_AirLowFareSearchLLSRQ.SCodPaxNegociada))
            {
                int iCantTotal = int.Parse(sAdt) + int.Parse(sCnn) + int.Parse(sInf);
                vo_Pasajero =
                new VO_Pasajero(vo_OTA_AirLowFareSearchLLSRQ.SCodPaxNegociada,
                    iCantTotal.ToString());
                lvo_Pasajeros.Add(vo_Pasajero);
            }
            else
            {
                vo_Pasajero =
                new VO_Pasajero("ADT",
                    sAdt);
                vo_Pasajero.SDetalle = "Adulto";
                vo_Pasajero.SCodeGen = "ADT";
                lvo_Pasajeros.Add(vo_Pasajero);

                if (!sCnn.Equals("0"))
                {
                    string sNumNinios = sCnn;

                    vo_Pasajero =
                       new VO_Pasajero("CNN",
                           sNumNinios);
                    vo_Pasajero.SDetalle = "Niño";
                    vo_Pasajero.SCodeGen = "CNN";

                    int iNumNinios = 0;
                    int.TryParse(sNumNinios, out iNumNinios);
                    List<string> lsEdadesNinio = new List<string>();
                    List<VO_ClasificaPasajero> lvPasajeroNino = new List<VO_ClasificaPasajero>();

                    int intNumeroDeNinos = Convert.ToInt32(sCnn);

                    for (int p = 0; p < intNumeroDeNinos; p++)
                    {
                        if (PageSource.Request.QueryString["ddlMultiEdad" + (p + 1)] != null && PageSource.Request.QueryString["ddlMultiEdad" + (p + 1)].ToString() != "")
                        {
                            bool bPax = true;
                            string sEdad = PageSource.Request.QueryString["ddlMultiEdad" + (p + 1)].ToString();
                            if (sEdad.Length.Equals(1))
                                sEdad = "0" + sEdad;
                            if (lvPasajeroNino.Count > 0)
                            {
                                for (int j = 0; j < lvPasajeroNino.Count; j++)
                                {
                                    if (lvPasajeroNino[j].SEdad.Equals(sEdad))
                                    {
                                        int iPax = int.Parse(lvPasajeroNino[j].SCantidad.ToString());
                                        iPax++;
                                        lvPasajeroNino[j].SCantidad = iPax.ToString();
                                        lvPasajeroNino[j].SDetalle = "Niño";
                                        lvPasajeroNino[j].SCodeGen = "C" + sEdad;
                                        bPax = false;
                                    }
                                }
                            }
                            if (bPax)
                            {
                                VO_ClasificaPasajero vPasajeroNino = new VO_ClasificaPasajero("C" + sEdad, "1", sEdad);
                                vPasajeroNino.SDetalle = "Niño";
                                vPasajeroNino.SCodeGen = "C" + sEdad;
                                lvPasajeroNino.Add(vPasajeroNino);
                            }
                            lsEdadesNinio.Add(PageSource.Request.QueryString["ddlMultiEdad" + (p + 1)].ToString());
                        }
                    }
                    vo_Pasajero.LvPasajeroNino = lvPasajeroNino;
                    lvo_Pasajeros.Add(vo_Pasajero);
                    vo_OTA_AirLowFareSearchLLSRQ.LsEdadesNinios = lsEdadesNinio;
                }

                if (!sInf.Equals("0"))
                {
                    string sNumBebes = sInf;

                    vo_Pasajero =
                       new VO_Pasajero("INF",
                          sNumBebes);
                    vo_Pasajero.SDetalle = "Infante";
                    vo_Pasajero.SCodeGen = "INF";

                    int iNumBebes = 0;
                    int.TryParse(sNumBebes, out iNumBebes);
                    List<string> lsEdadesBebes = new List<string>();
                    int intNumeroDeInfantes = Convert.ToInt32(sInf);
                    List<VO_ClasificaPasajero> lvPasajeroInf = new List<VO_ClasificaPasajero>();

                    for (int p = 0; p < intNumeroDeInfantes; p++)
                    {
                        if (PageSource.Request.QueryString["ddlMultiMeses" + (p + 1)] != null && PageSource.Request.QueryString["ddlMultiMeses" + (p + 1)].ToString() != "")
                        {
                            bool bPax = true;
                            string sEdad = PageSource.Request.QueryString["ddlMultiMeses" + (p + 1)];
                            if (sEdad.Length.Equals(1))
                                sEdad = "0" + sEdad;
                            int iPax = 1;
                            if (lvPasajeroInf.Count > 0)
                            {
                                for (int j = 0; j < lvPasajeroInf.Count; j++)
                                {
                                    if (lvPasajeroInf[j].SEdad.Equals(sEdad))
                                    {
                                        iPax = int.Parse(lvPasajeroInf[j].SCantidad.ToString());
                                        iPax++;
                                        lvPasajeroInf[j].SCantidad = iPax.ToString();
                                        lvPasajeroInf[j].SDetalle = "Infante";
                                        lvPasajeroInf[j].SCodeGen = "INF";
                                        bPax = false;
                                    }
                                }
                            }
                            if (bPax)
                            {
                                VO_ClasificaPasajero vPasajeroInf = new VO_ClasificaPasajero("INF", iPax.ToString(), sEdad);
                                vPasajeroInf.SDetalle = "Infante";
                                vPasajeroInf.SCodeGen = "INF";
                                lvPasajeroInf.Add(vPasajeroInf);
                            }
                            lsEdadesBebes.Add(PageSource.Request.QueryString["ddlMultiMeses" + (p + 1)].ToString());
                        }
                    }
                    vo_Pasajero.LvPasajeroInfante = lvPasajeroInf;
                    lvo_Pasajeros.Add(vo_Pasajero);
                    vo_OTA_AirLowFareSearchLLSRQ.LsEdadesInfantes = lsEdadesBebes;
                }
            }
            vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros = lvo_Pasajeros;
        }      
    }
}
