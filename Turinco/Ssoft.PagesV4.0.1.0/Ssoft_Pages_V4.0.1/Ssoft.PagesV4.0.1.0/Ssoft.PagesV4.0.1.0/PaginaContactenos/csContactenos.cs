using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;
using System.Web.UI.WebControls;
using System.Web;
using SsoftQuery.Generales;
using System.Data;
using Ssoft.Rules.Generales;
using SsoftQuery.Contactenos;
using System.Web.UI.HtmlControls;
using Ssoft.Rules.Hoteles;

namespace Ssoft.Pages.PaginaContactenos
{
    public class csContactenos
    {
        public void setBuzonDinamico(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                if (!PageSource.Page.IsPostBack)
                {
                    clsIdioma cIdioma = new clsIdioma();
                    cIdioma.LoadIdioma(csGeneralsPag.PaginaActual(), PageSource);

                    CargarCombos(PageSource);
                    //try
                    //{
                    //DropDownList ddlTemamensaje = (DropDownList)PageSource.FindControl("ddlTemamensaje");
                    //ddlTemamensajeDinamico_SelectedIndexChanged(ddlTemamensaje, PageSource);
                    //}
                    //catch (Exception) { }
                    /*CACHE PARA QUE EXPIRE CADA VEZ QUE SE PIDE EL FORMULARIO*/
                    HttpContext.Current.Response.Expires = -1;
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
                cParametros.Complemento = "setBuzon";
                ExceptionHandled.Publicar(cParametros);
            }

        }

        protected void CargarCombos(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            clsCache cCache = new csCache().cCache();
            try
            {
                HiddenField HFCliente = (HiddenField)PageSource.FindControl("HFCliente");
                DropDownList ddlTipoMensaje = (DropDownList)PageSource.FindControl("ddlTipoMensaje");
                DropDownList ddlPais = (DropDownList)PageSource.FindControl("ddlPais");
                Panel PanelCotizacion = PageSource.FindControl("pnlCotizacion") as Panel;
                DropDownList ddlPaisVisas = PageSource.FindControl("ddlPaisVisas") as DropDownList;
                csConsultasGenerales cGenerales = new csConsultasGenerales();
                csGenerales csRefere = new csGenerales();
                csConsultasContactenos csCont = new csConsultasContactenos();
                /*TIPOS MENSAJE*/
                DataTable dtTipMsjContacto = csCont.listado_tipos_mensaje();
                if (dtTipMsjContacto != null)
                    csRefere.LlenarControlData(ddlTipoMensaje, Enum_Controls.DropDownList, "intCodigo", "strDescripcion", true, false, null, dtTipMsjContacto);

                /*TEMA MENSAJE*/
                //DropDownList ddlTemamensaje = (DropDownList)PageSource.FindControl("ddlTemamensaje");
                /*PAIS */
                //string sDefault = "";
                //bool bDefault = false;

                //tRefere.Get(clsValidaciones.GetKeyOrAdd("Paises", "Pais"), clsValidaciones.GetKeyOrAdd("PaisDefault", "COL"));
                //if (tRefere.Respuesta)
                //{
                //    sDefault = tRefere.intidRefere.Value;
                //    bDefault = true;
                //}

                DataTable dtPaisesContacto = cGenerales.listado_paises();
                if (dtPaisesContacto != null)
                    csRefere.LlenarControlData(ddlPais, Enum_Controls.DropDownList, "intCode", "strDescripcion", true, false, null, dtPaisesContacto);

                /*TIPO SERVICIO*/
                //DropDownList ddlTipoServicio =
                //            (DropDownList)PageSource.FindControl("ddlTipoServicio");

                #region RevisionFiltros por Tipo mjs
                //try
                //{
                //    if (HttpContext.Current.Request.QueryString[clsValidaciones.GetKeyOrAdd("TiposMensajes", "TPOMSJ")] != null)
                //    {

                //        if (ddlTipoMensaje != null && ddlTipoMensaje.Items.Count != 0)
                //        {
                //            tblRefere objTblRefere = new tblRefere();


                //            objTblRefere.Get(clsValidaciones.GetKeyOrAdd("TipoCliente", "TPO_CLIENTE"),
                //                                    clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF"));
                //            int idRefereTipoUsuario = int.Parse(objTblRefere.intidRefere.Value);

                //            try
                //            {
                //                objTblRefere.Get(
                //                    clsValidaciones.GetKeyOrAdd("TiposMensajes", "TPOMSJ"),
                //                    HttpContext.Current.Request.QueryString[clsValidaciones.GetKeyOrAdd("TiposMensajes", "TPOMSJ")],
                //                    idRefereTipoUsuario);
                //            }
                //            catch (Exception) { }

                //            if (objTblRefere.intidRefere != null)
                //            {
                //                ddlTipoMensaje.SelectedValue = objTblRefere.intidRefere.Value;
                //            }
                //            /*Segun el tipo de mensaje llenamos el tema de mensaje.*/

                //            if (sValue[0] == "0")
                //                LlenarCBFiltrosTemas(ddlTemamensaje, clsValidaciones.GetKeyOrAdd("TemasMensajes", "TMMSJ"), int.Parse(ddlTipoMensaje.SelectedValue), "0");
                //            else
                //                LlenarCBFiltrosTemas(ddlTemamensaje, clsValidaciones.GetKeyOrAdd("TemasMensajes", "TMMSJ"), int.Parse(ddlTipoMensaje.SelectedValue), "0",
                //                    true, "strRefere", sValue[0]);
                //        }

                //        if (HttpContext.Current.Request.QueryString[clsValidaciones.GetKeyOrAdd("TemasMensajes", "TMMSJ")] != null)
                //        {
                //            if (ddlTemamensaje != null && ddlTemamensaje.Items.Count != 0)
                //            {
                //                ddlTemamensaje.SelectedValue = HttpContext.Current.Request.QueryString[clsValidaciones.GetKeyOrAdd("TemasMensajes", "TMMSJ")];
                //            }
                //        }
                //    }
                //    else if (HttpContext.Current.Request.QueryString["IDTMM"] != null)
                //    {
                //        tblRefere refere = new tblRefere();
                //        refere.Get(HttpContext.Current.Request.QueryString["IDTMM"]);
                //        ddlTipoMensaje.SelectedValue = refere.strValor.Value;
                //        /*Segun el tipo de mensaje llenamos el tema de mensaje.*/
                //        LlenarCBFiltrosTemas(ddlTemamensaje, clsValidaciones.GetKeyOrAdd("TemasMensajes", "TMMSJ"), int.Parse(ddlTipoMensaje.SelectedValue), "0");
                //        ddlTemamensaje.SelectedValue = refere.intidRefere.Value;
                //    }
                //}
                //catch (Exception)
                //{

                //}

                //try
                //{
                //    if (PanelCotizacion != null)
                //    {
                //        CheckBoxList cblServicios =
                //                        (CheckBoxList)PanelCotizacion.FindControl("cblServicios");
                //        if (cblServicios != null)
                //        {
                //            generales.LlenarCBLTipoRefere(cblServicios,
                //                   clsValidaciones.GetKeyOrAdd("Servicios", "serv"),
                //                   false,
                //                   clsSesiones.getIdioma(),
                //                   clsSesiones.getAplicacion().ToString());
                //        }
                //    }

                //    if (ddlTipoServicio != null)
                //    {
                //        /*consultamos los tipos de planes*/
                //        generales.LlenarDDLTipoRefereRefere(ddlTipoServicio,
                //            clsValidaciones.GetKeyOrAdd("TipoPlan"),
                //            true,
                //            clsSesiones.getIdioma(),
                //            clsSesiones.getAplicacion().ToString());
                //        if (ddlTipoServicio.Items.Count > 0)
                //        {
                //            setFiltrarServicios(ddlTipoServicio);
                //        }
                //    }
                //}
                //catch (Exception) {/*No se encontro el control*/ }
                #endregion
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "CargarCombos";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// metodo que envia el correo de buzon de contacto y registra el buzon en la BD
        /// </summary>
        /// <param name="PageSource">Usercontrol</param>    
        /// <remarks>
        /// Autor:  Camilo Diaz 
        /// Fecha:  
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Descripcion: se adiciona codigo para borrado de campos
        /// Fecha: 2011-10-05       
        /// Responsable: Camilo Diaz 
        /// ------------------------
        /// Descripcion: se adiciona validacion y borrado de campos nombres2, apellidos2 y pais
        /// Fecha: 2012-04-16       
        /// Responsable: Camilo Diaz 
        /// </remarks>
        public void setEnviarDinamico(UserControl PageSource)
        {
            clsIdioma cIdioma = new clsIdioma();
            cIdioma.LoadIdioma(csGeneralsPag.PaginaActual(), PageSource);
            string sData = string.Empty; ;
            try
            {
                sData = HttpContext.Current.Session["FromData"].ToString();
                HttpContext.Current.Session["FromData"] = null;
            }
            catch { }
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    Label LblError = (Label)PageSource.FindControl("LblError");
                    TextBox txtNombres = (TextBox)PageSource.FindControl("txtNombres");
                    TextBox txtApellidos = (TextBox)PageSource.FindControl("txtApellidos");
                    TextBox txtNombres2 = (TextBox)PageSource.FindControl("txtNombres2");
                    TextBox txtApellidos2 = (TextBox)PageSource.FindControl("txtApellidos2");
                    TextBox txtCiudad = (TextBox)PageSource.FindControl("txtCiudad");
                    TextBox txtTelefono = (TextBox)PageSource.FindControl("txtTelefono");
                    TextBox txtEmail = (TextBox)PageSource.FindControl("txtEmail");
                    TextBox txtComentarios = (TextBox)PageSource.FindControl("txtComentarios");

                    DropDownList ddlTipoMensaje = (DropDownList)PageSource.FindControl("ddlTipoMensaje");
                    DropDownList ddlTemamensaje = (DropDownList)PageSource.FindControl("ddlTemamensaje");
                    DropDownList ddlPais = (DropDownList)PageSource.FindControl("ddlPais");
                    HiddenField HFCliente = (HiddenField)PageSource.FindControl("HFCliente");
                    HiddenField HidenRefereTemaMensaje = (HiddenField)PageSource.FindControl("HidenRefereTemaMensaje");

                    HtmlGenericControl dvFormulario = (HtmlGenericControl)PageSource.FindControl("dvFormulario");


                    bool bContinua = true;
                    if (txtNombres2 != null && txtApellidos2 != null)
                    {
                        if (txtNombres.Text.Trim().Equals("") || txtApellidos.Text.Trim().Equals("") ||
                            //txtNombres2.Text.Trim().Equals("") || txtApellidos2.Text.Trim().Equals("") ||
                        txtCiudad.Text.Trim().Equals("") || txtEmail.Text.Trim().Equals("") ||
                        txtTelefono.Text.Trim().Equals("") || //txtComentarios.Text.Trim().Equals("") ||
                        ddlTipoMensaje.SelectedValue.Equals("") || ddlTipoMensaje.SelectedValue.Equals("0") ||
                        ddlTemamensaje.SelectedValue.Equals("") || ddlTemamensaje.SelectedValue.Equals("0") ||
                        ddlTipoMensaje.SelectedValue.ToUpper().Equals("SELECCIONAR") || ddlTemamensaje.SelectedValue.ToUpper().Equals("SELECCIONAR") ||
                        ddlPais.SelectedValue.Equals("") || ddlPais.SelectedValue.Equals("0"))
                        {
                            bContinua = false;
                        }
                    }
                    else
                    {
                        if (txtNombres.Text.Trim().Equals("") || txtApellidos.Text.Trim().Equals("") ||
                        txtCiudad.Text.Trim().Equals("") || txtEmail.Text.Trim().Equals("") ||
                        txtTelefono.Text.Trim().Equals("") || //txtComentarios.Text.Trim().Equals("") ||
                        ddlTipoMensaje.SelectedValue.Equals("") || ddlTipoMensaje.SelectedValue.Equals("0") ||
                        ddlTemamensaje.SelectedValue.Equals("") || ddlTemamensaje.SelectedValue.Equals("0") ||
                        ddlTipoMensaje.SelectedValue.ToUpper().Equals("SELECCIONAR") || ddlTemamensaje.SelectedValue.ToUpper().Equals("SELECCIONAR") ||
                        ddlPais.SelectedValue.Equals("") || ddlPais.SelectedValue.Equals("0"))
                        {
                            bContinua = false;
                        }
                    }

                    #region [ GUARDAR ]
                    if (!bContinua)
                    {
                        LblError.Text = "Por favor diligencie todos los campos marcados como obligatorios";
                    }
                    else
                    {
                        /*LISTAMOS LOS CAMPOS PARA GUARDARLOS Y ENVIARLOS*/
                        //csPlanes csPlan = new csPlanes();
                        Dictionary<string, string> dicLista = new Dictionary<string, string>();
                        dicLista.Add(ddlTipoMensaje.ID, "Tipo de Mensaje");
                        dicLista.Add(ddlTemamensaje.ID, "Tema de Mensaje");
                        dicLista.Add(txtNombres.ID, "Nombres");
                        dicLista.Add(txtApellidos.ID, "Apellidos");
                        dicLista.Add(txtCiudad.ID, "Ciudad");
                        dicLista.Add(txtTelefono.ID, "Telefono");
                        dicLista.Add(txtComentarios.ID, "Comentarios");
                        dicLista.Add(ddlPais.ID, "Pais");
                        dicLista.Add(txtEmail.ID, "Email");
                        dicLista.Add(HidenRefereTemaMensaje.Value, "HidenRefereTemaMensaje");
                        dicLista.Add(HFCliente.ID, "HFCliente");

                        /*ENVIO DE CORREO*/
                        DataTable dtDatosFormulario = GetDatosFormulario(PageSource, dicLista);
                        StringBuilder strContenido = new StringBuilder();
                        List<string> listaParametros = new List<string>();

                        if (dtDatosFormulario != null && dtDatosFormulario.Rows.Count != 0 && SaveForm(dtDatosFormulario, sData))
                        {
                            foreach (KeyValuePair<string, string> valores in dicLista)
                            {
                                if (dtDatosFormulario.Rows.Contains(valores.Key) && !valores.Value.Contains("Hiden"))
                                {
                                    if (!valores.Value.Contains("HFCliente"))
                                    {
                                        strContenido.AppendLine(valores.Value + " : " + dtDatosFormulario.Rows.Find(valores.Key)["valorControl"].ToString() + "<br>");
                                        listaParametros.Add(dtDatosFormulario.Rows.Find(valores.Key)["valorControl"].ToString());
                                    }
                                }
                            }
                            listaParametros.Add(sData);


                            HttpContext.Current.Session["$DatosContacto"] = listaParametros;
                            new csUtilitarios().setCorreos("", "EFE", clsValidaciones.GetKeyOrAdd("TpoCorreoContactenos", "CTS"));

                            //setEnviar_Correo(strContenido.ToString(), "PlantillaBuzonContacto.aspx", ddlTemamensaje.SelectedItem.Text + " " + ddlTipoMensaje.SelectedItem.Text, txtEmail.Text, ddlTemamensaje.SelectedItem.Value, PageSource, listaParametros.ToArray());

                            //LblError.Text = "El Correo ha sido enviado satisfactoriamente";
                            LblError.Text = "Tu solicitud ha sido ingresada satisfactoriamente";

                            txtNombres.Text = "";
                            txtApellidos.Text = "";

                            if (txtNombres2 != null)
                            {
                                if (txtNombres2.Text != null)
                                    txtNombres2.Text = "";
                            }

                            if (txtApellidos2 != null)
                            {
                                if (txtApellidos2 != null)
                                    txtApellidos2.Text = "";
                            }

                            txtCiudad.Text = "";
                            txtTelefono.Text = "";
                            txtEmail.Text = "";
                            txtComentarios.Text = "";
                            ddlTemamensaje.Items.Clear();

                            //---------------------- Borrado pais
                            csGenerales generales = new csGenerales();
                            //string sDefault = "";
                            //bool bDefault = false;
                            //tblRefere tRefere = new tblRefere();
                            //tRefere.Get(clsValidaciones.GetKeyOrAdd("Paises", "Pais"), clsValidaciones.GetKeyOrAdd("PaisDefault", "COL"));
                            //if (tRefere.Respuesta)
                            //{
                            //    sDefault = tRefere.intidRefere.Value;
                            //    bDefault = true;
                            //}
                            //generales.LlenarDDLTipoRefere(ddlPais, clsValidaciones.GetKeyOrAdd("Paises", "Pais"), true,
                            //    clsSesiones.getIdioma(), clsSesiones.getAplicacion().ToString(), bDefault, sDefault);
                            ddlPais.SelectedValue = "0";
                            //-----------------------------------
                            if (ddlTipoMensaje.Items.Count > 0)
                            {
                                for (int i = 0; i < ddlTipoMensaje.Items.Count; i++)
                                {
                                    if (ddlTipoMensaje.Items[i].Value.Equals("") || ddlTipoMensaje.Items[i].Value.Equals("0") ||
                                        ddlTipoMensaje.Items[i].Value.Equals("Seleccionar"))
                                    {
                                        ddlTipoMensaje.SelectedValue = ddlTipoMensaje.Items[i].Value;
                                    }
                                }
                            }
                            if (ddlTemamensaje.Items.Count > 0)
                            {
                                for (int i = 0; i < ddlTemamensaje.Items.Count; i++)
                                {
                                    if (ddlTemamensaje.Items[i].Value.Equals("") || ddlTemamensaje.Items[i].Value.Equals("0") ||
                                        ddlTemamensaje.Items[i].Value.Equals("Seleccionar"))
                                    {
                                        ddlTemamensaje.SelectedValue = ddlTemamensaje.Items[i].Value;
                                    }
                                }
                            }
                            setBuzonDinamico(PageSource);
                        }
                        else
                        {
                            LblError.Text = "Lo sentimos, tu solicitud no pudo ser procesada, por favor contacta a un asesor";
                        }
                    }
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
                cParametros.Complemento = "setEnviar";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        /// <summary>
        /// metodo que envia el correo de buzon de Fallo de reserva y registra el buzon en la BD
        /// </summary>
        /// <param name="PageSource">Usercontrol</param>    
        /// <remarks>
        /// Autor:  Camilo Diaz 
        /// Fecha: 
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Descripcion: Cambio de sistema para fallo de reserva hoteles
        /// Fecha:  04/09/2015
        /// Responsable: Omar Moreno
        /// </remarks>
        public void setEnviarFalloHoteles(UserControl PageSource)
        {
            clsIdioma cIdioma = new clsIdioma();
            cIdioma.LoadIdioma(csGeneralsPag.PaginaActual(), PageSource);
            string sData = string.Empty; ;
            try
            {
                sData = HttpContext.Current.Session["FromData"].ToString();
                HttpContext.Current.Session["FromData"] = null;
            }
            catch { }
            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    Label LblError = (Label)PageSource.FindControl("LblError");
                    TextBox txtNombre = (TextBox)PageSource.FindControl("txtNombre");
                    TextBox txtApellido = (TextBox)PageSource.FindControl("txtApellido");
                    TextBox txtNombres2 = (TextBox)PageSource.FindControl("txtNombres2");
                    TextBox txtApellidos2 = (TextBox)PageSource.FindControl("txtApellidos2");
                    TextBox txtCiudad = (TextBox)PageSource.FindControl("txtCiudad");
                    TextBox txtCelular = (TextBox)PageSource.FindControl("txtCelular");
                    TextBox txtMailPersonal = (TextBox)PageSource.FindControl("txtMailPersonal");
                    //Label txtComentarios = (Label)PageSource.FindControl("txtComentarios");
                    Repeater rptHabitaciones = (Repeater)PageSource.FindControl("rptHabitaciones");
                    Repeater rptDatosReserva = (Repeater)PageSource.FindControl("rptDatosReserva");
                    //Label txtComentarios = new Label();
                    //txtComentarios.ID = "txtComentarios";
                    //txtComentarios.
                    String txtComentarios = "";


                    if (rptHabitaciones != null && rptDatosReserva != null)
                    {
                        csHoteles cHoteles = new csHoteles();
                        DataSet dsResultados = cHoteles.setTablaReserva();
                        Label lblNombre = (Label)PageSource.FindControl("lblNombre");
                        Label lblMoneda = (Label)PageSource.FindControl("lblMoneda");
                        Label lblPrecioTotal = (Label)PageSource.FindControl("lblPrecioTotal");
                        String[] Arr1 = new String[rptHabitaciones.Items.Count];
                        String[] Arr2 = new String[rptDatosReserva.Items.Count];
                        String[] Arr3 = new String[rptDatosReserva.Items.Count];
                        String[] Arr4 = new String[rptDatosReserva.Items.Count];
                        int i = 0;

                        try
                        {
                            foreach (RepeaterItem item in rptHabitaciones.Items)
                            {
                                Label Label14 = (Label)item.FindControl("Label14");
                                Label lblTipoHabitacion = (Label)item.FindControl("lblTipoHabitacion");
                                Label Label15 = (Label)item.FindControl("Label15");
                                Label lblCheckIn = (Label)item.FindControl("lblCheckIn");
                                Label Label18 = (Label)item.FindControl("Label18");
                                Label lblCheckOut = (Label)item.FindControl("lblCheckOut");
                                Arr1[i] = Label14.Text.ToString() + " " + lblTipoHabitacion.Text.ToString() + " " + Label15.Text.ToString() + " " + lblCheckIn.Text.ToString() + " - " + Label18.Text.ToString() + " " + lblCheckOut.Text.ToString() + " - ";
                                i++;
                            }
                            i = 0;
                            foreach (RepeaterItem item in rptDatosReserva.Items)
                            {
                                Label lblhab_Counter = (Label)item.FindControl("lblhab_Counter");
                                TextBox txtNombre1 = (TextBox)item.FindControl("txtNombre1");
                                TextBox txtApellido1 = (TextBox)item.FindControl("txtApellido1");
                                Arr2[i] = lblhab_Counter.Text;
                                Arr3[i] = txtNombre1.Text + " " + txtApellido1.Text;
                                i++;
                            }
                            i = 0;
                            foreach (RepeaterItem item in rptDatosReserva.Items)
                            {
                                Label lblhab_Counter = (Label)item.FindControl("lblhab_Counter");
                                Arr4[i] = lblhab_Counter.Text;
                                i++;
                            }
                            i = 0;

                            //txtComentarios.Text = "Reserva en el Hotel " + lblNombre.Text + " Con un valor de " + lblMoneda.Text + lblPrecioTotal.Text;

                            for (i = 0; i < Arr2.Length; i++)
                            {
                                txtComentarios = "A nombre de " + Arr3[i].ToString() + " - " + "Reserva en el Hotel " + lblNombre.Text + " Con un valor de " + lblMoneda.Text + " " + lblPrecioTotal.Text + " - " + Arr1[i].ToString() + " - " + "Habitación " + (i + 1);
                            }
                        }
                        catch (Exception e)
                        {
                            LblError.Text = "Error en captura de datos o arreglos al querer envir el correo";
                        }
                    }

                    String ddlTipoMensaje = "Solicitud";
                    String ddlTemamensaje = "Cotizacion";
                    String ddlPais = "Colombia";
                    //HiddenField HFCliente = new HiddenField();
                    //HFCliente.Value = "0";
                    //HiddenField HidenRefereTemaMensaje = new HiddenField();
                    //HidenRefereTemaMensaje.Value = "0";
                    HiddenField HFCliente = (HiddenField)PageSource.FindControl("HFCliente");
                    HiddenField HidenRefereTemaMensaje = (HiddenField)PageSource.FindControl("HidenRefereTemaMensaje");

                    HtmlGenericControl dvFormulario = (HtmlGenericControl)PageSource.FindControl("dvFormulario");

                    bool bContinua = true;
                    if (txtNombres2 != null && txtApellidos2 != null)
                    {
                        if (txtNombre.Text.Trim().Equals("") || txtApellido.Text.Trim().Equals("") ||
                            //txtNombres2.Text.Trim().Equals("") || txtApellidos2.Text.Trim().Equals("") ||
                        txtCiudad.Text.Trim().Equals("") || txtMailPersonal.Text.Trim().Equals("") ||
                        txtCelular.Text.Trim().Equals("") || //txtComentarios.Text.Trim().Equals("") ||
                        ddlTipoMensaje.Equals("") || ddlTipoMensaje.Equals("0") ||
                        ddlTemamensaje.Equals("") || ddlTemamensaje.Equals("0") ||
                        ddlPais.Equals("") || ddlPais.Equals("0"))
                        {
                            bContinua = false;
                        }
                    }
                    else
                    {
                        if (txtNombre.Text.Trim().Equals("") || txtApellido.Text.Trim().Equals("") ||
                        txtCiudad.Text.Trim().Equals("") || txtMailPersonal.Text.Trim().Equals("") ||
                        txtCelular.Text.Trim().Equals("") || //txtComentarios.Text.Trim().Equals("") ||
                        ddlTipoMensaje.Equals("") || ddlTipoMensaje.Equals("0") ||
                        ddlTemamensaje.Equals("") || ddlTemamensaje.Equals("0") ||
                        ddlPais.Equals("") || ddlPais.Equals("0"))
                        {
                            bContinua = false;
                        }
                    }

                    #region [ GUARDAR ]
                    if (!bContinua)
                    {
                        LblError.Text = "Error en envio correo para reserva Fallida.";
                    }
                    else
                    {
                        /*LISTAMOS LOS CAMPOS PARA GUARDARLOS Y ENVIARLOS*/
                        //csPlanes csPlan = new csPlanes();
                        try
                        {
                            Dictionary<string, string> dicLista = new Dictionary<string, string>();
                            dicLista.Add(txtNombre.ID, "Nombres");
                            dicLista.Add(txtApellido.ID, "Apellidos");
                            dicLista.Add(txtCiudad.ID, "Ciudad");
                            dicLista.Add(txtCelular.ID, "Telefono");
                            dicLista.Add(txtComentarios, "Comentarios");
                            dicLista.Add(ddlTipoMensaje, "Tipo de Mensaje");
                            dicLista.Add(ddlTemamensaje, "Tema de Mensaje");
                            dicLista.Add(ddlPais, "Pais");
                            dicLista.Add(txtMailPersonal.ID, "Email");
                            dicLista.Add(HidenRefereTemaMensaje.Value, "HidenRefereTemaMensaje");
                            dicLista.Add(HFCliente.ID, "HFCliente");

                            /*ENVIO DE CORREO*/
                            DataTable dtDatosFormulario = GetDatosFormularioReservaF(PageSource, dicLista);
                            StringBuilder strContenido = new StringBuilder();
                            List<string> listaParametros = new List<string>();

                            if (dtDatosFormulario != null && dtDatosFormulario.Rows.Count != 0 && SaveFormFailHotels(dtDatosFormulario, sData, txtMailPersonal.Text, txtComentarios, ddlTipoMensaje, ddlTemamensaje, ddlPais, txtCelular.Text, txtNombre.Text, txtApellido.Text))
                            {
                                foreach (KeyValuePair<string, string> valores in dicLista)
                                {
                                    if (dtDatosFormulario.Rows.Contains(valores.Key) && !valores.Value.Contains("Hiden"))
                                    {
                                        if (!valores.Value.Contains("HFCliente"))
                                        {
                                            strContenido.AppendLine(valores.Value + " : " + dtDatosFormulario.Rows.Find(valores.Key)["valorControl"].ToString() + "<br>");
                                            listaParametros.Add(dtDatosFormulario.Rows.Find(valores.Key)["valorControl"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (!valores.Value.Contains("HFCliente"))
                                        {
                                            strContenido.AppendLine(valores.Value + " : " + valores.Key + "<br>");
                                            listaParametros.Add(valores.Key);
                                        }
                                    }
                                }
                                listaParametros.Add(sData);


                                HttpContext.Current.Session["$DatosContacto"] = listaParametros;
                                new csUtilitarios().setCorreosHotelFail("", "EFE", clsValidaciones.GetKeyOrAdd("TpoCorreoContactenos", "CTS"), txtMailPersonal.Text, txtComentarios);

                                //setEnviar_Correo(strContenido.ToString(), "PlantillaBuzonContacto.aspx", ddlTemamensaje.SelectedItem.Text + " " + ddlTipoMensaje.SelectedItem.Text, txtEmail.Text, ddlTemamensaje.SelectedItem.Value, PageSource, listaParametros.ToArray());

                                //LblError.Text = "El Correo ha sido enviado satisfactoriamente";
                                LblError.Text = "Tu solicitud ha sido ingresada satisfactoriamente";

                                txtNombre.Text = "";
                                txtApellido.Text = "";

                                if (txtNombres2 != null)
                                {
                                    if (txtNombres2.Text != null)
                                        txtNombres2.Text = "";
                                }

                                if (txtApellidos2 != null)
                                {
                                    if (txtApellidos2 != null)
                                        txtApellidos2.Text = "";
                                }

                                txtCiudad.Text = "";
                                txtCelular.Text = "";
                                txtMailPersonal.Text = "";
                                txtComentarios = "";
                                //ddlTemamensaje.Items.Clear();

                                //---------------------- Borrado pais
                                csGenerales generales = new csGenerales();
                                //string sDefault = "";
                                //bool bDefault = false;
                                //tblRefere tRefere = new tblRefere();
                                //tRefere.Get(clsValidaciones.GetKeyOrAdd("Paises", "Pais"), clsValidaciones.GetKeyOrAdd("PaisDefault", "COL"));
                                //if (tRefere.Respuesta)
                                //{
                                //    sDefault = tRefere.intidRefere.Value;
                                //    bDefault = true;
                                //}
                                //generales.LlenarDDLTipoRefere(ddlPais, clsValidaciones.GetKeyOrAdd("Paises", "Pais"), true,
                                //    clsSesiones.getIdioma(), clsSesiones.getAplicacion().ToString(), bDefault, sDefault);
                                //ddlPais.SelectedValue = "0";
                                //-----------------------------------
                                //if (ddlTipoMensaje.Items.Count > 0)
                                //{
                                //    for (int i = 0; i < ddlTipoMensaje.Items.Count; i++)
                                //    {
                                //        if (ddlTipoMensaje.Items[i].Value.Equals("") || ddlTipoMensaje.Items[i].Value.Equals("0") ||
                                //            ddlTipoMensaje.Items[i].Value.Equals("Seleccionar"))
                                //        {
                                //            ddlTipoMensaje.SelectedValue = ddlTipoMensaje.Items[i].Value;
                                //        }
                                //    }
                                //}
                                //if (ddlTemamensaje.Items.Count > 0)
                                //{
                                //    for (int i = 0; i < ddlTemamensaje.Items.Count; i++)
                                //    {
                                //        if (ddlTemamensaje.Items[i].Value.Equals("") || ddlTemamensaje.Items[i].Value.Equals("0") ||
                                //            ddlTemamensaje.Items[i].Value.Equals("Seleccionar"))
                                //        {
                                //            ddlTemamensaje.SelectedValue = ddlTemamensaje.Items[i].Value;
                                //        }
                                //    }
                                //}
                                setBuzonDinamico(PageSource);
                            }
                            else
                            {
                                LblError.Text = "Error no enviado por error general de reserva";
                            }
                        }
                        catch (Exception j)
                        {
                            LblError.Text = "Error almacenando la lista de envio o enviando el correo";
                        }
                    }
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
                cParametros.Complemento = "setEnviar";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        public void ddlTipoMensaje_SelectedIndexChanged(UserControl PageSource)
        {
            clsIdioma cIdioma = new clsIdioma();
            cIdioma.LoadIdioma(csGeneralsPag.PaginaActual(), PageSource);
            clsParametros cParametros = new clsParametros();
            try
            {
                // clsCache cCache = new csCache().cCache();
                //if (cCache != null)
                //{
                try
                {
                    DropDownList ddlTemamensaje = (DropDownList)PageSource.FindControl("ddlTemamensaje");
                    DropDownList ddlTipoMensaje = (DropDownList)PageSource.FindControl("ddlTipoMensaje");
                    csConsultasContactenos csCont = new csConsultasContactenos();
                    csGenerales csRefere = new csGenerales();
                    //if (clsValidaciones.GetKeyOrAdd("AplicacionWeb").Equals(csBuscador.PAGE_IROTAMA) && !PageSource.ID.Equals(clsValidaciones.GetKeyOrAdd("UCBuzonContacto", "ucContactenos")))
                    //{
                    //    if (sValue[0] == "0")
                    //        LlenarCBFiltrosTemas(ddlTemamensaje, clsValidaciones.GetKeyOrAdd("TemasMensajes"), int.Parse(ddlTipoMensaje.SelectedValue), null);
                    //    else
                    //        LlenarCBFiltrosTemas(ddlTemamensaje, clsValidaciones.GetKeyOrAdd("TemasMensajes"), int.Parse(ddlTipoMensaje.SelectedValue),
                    //            null, true, "strRefere", sValue[0]);
                    //}
                    //else
                    //{
                    //    if (sValue[0] == "0")
                    DataTable dtTemaMsjContacto = csCont.listado_temas_mensaje(ddlTipoMensaje.SelectedValue);
                    if (dtTemaMsjContacto != null)
                        csRefere.LlenarControlData(ddlTemamensaje, Enum_Controls.DropDownList, "intCodigo", "strDescripcion", true, false, null, dtTemaMsjContacto);
                    //LlenarCBFiltrosTemas(ddlTemamensaje, clsValidaciones.GetKeyOrAdd("TemasMensajes"), int.Parse(ddlTipoMensaje.SelectedValue), "0");
                    //    else
                    //        LlenarCBFiltrosTemas(ddlTemamensaje, clsValidaciones.GetKeyOrAdd("TemasMensajes"), int.Parse(ddlTipoMensaje.SelectedValue), "0",
                    //            true, "strRefere", sValue[0]);
                    //}
                }
                catch (Exception Ex)
                {
                    cParametros.Id = 0;
                    cParametros.Message = Ex.Message.ToString();
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = "ddlTipoMensaje_SelectedIndexChanged";
                    ExceptionHandled.Publicar(cParametros);
                }
                //}
                //else
                //{
                //    csGeneralsPag.FinSesion();
                //}
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "ddlTipoMensaje_SelectedIndexChanged";
                ExceptionHandled.Publicar(cParametros);
            }
        }

        public DataTable GetDatosFormulario(UserControl ucFormulario, Dictionary<string, string> dCamposFormulario)
        {
            /*OBTENEMOS TODOS LOS CAMPOS DEL USERCONTROL Y LOS ALMACENAMOS EN UN DATATABLE*/
            DataTable dtFormulario = new DataTable();

            dtFormulario.Columns.Add("idControl", typeof(string));
            dtFormulario.Columns.Add("valorControl", typeof(string));
            dtFormulario.Columns.Add("Control", typeof(Control));
            dtFormulario.Columns.Add("Enum_Controls", typeof(Type));
            dtFormulario.Columns.Add("idControlAsociado", typeof(string));
            dtFormulario.PrimaryKey = new DataColumn[] { dtFormulario.Columns["idControl"] };

            foreach (KeyValuePair<string, string> dItem in dCamposFormulario)
            {
                DataRow drNevaFila = dtFormulario.NewRow();

                Control control = ucFormulario.FindControl(dItem.Key);

                drNevaFila["idControlAsociado"] = dItem.Key;

                if (control is TextBox)
                {
                    drNevaFila["idControl"] = (control as TextBox).ID;

                    drNevaFila["valorControl"] = (control as TextBox).Text;

                    drNevaFila["Enum_Controls"] = control.GetType();
                }
                else if (control is RadioButtonList)
                {
                    drNevaFila["idControl"] = (control as RadioButtonList).ID;

                    drNevaFila["valorControl"] = (control as RadioButtonList).SelectedItem.Value;

                    drNevaFila["Enum_Controls"] = control.GetType();
                }
                else if (control is HiddenField)
                {
                    drNevaFila["idControl"] = (control as HiddenField).ID;

                    drNevaFila["valorControl"] = (control as HiddenField).Value;

                    drNevaFila["Enum_Controls"] = control.GetType();
                }
                else if (control is DropDownList)
                {
                    drNevaFila["idControl"] = (control as DropDownList).ID;

                    drNevaFila["valorControl"] = (control as DropDownList).SelectedItem.Text;

                    drNevaFila["control"] = control;

                    drNevaFila["Enum_Controls"] = control.GetType();
                }
                else if (control is HtmlGenericControl)
                {
                    drNevaFila["idControl"] = (control as HtmlGenericControl).ID;

                    drNevaFila["valorControl"] = (control as HtmlGenericControl).InnerHtml;

                    drNevaFila["control"] = control;

                    drNevaFila["Enum_Controls"] = control.GetType();
                }
                if (!drNevaFila["idControl"].ToString().Equals(string.Empty))
                {
                    dtFormulario.Rows.Add(drNevaFila);
                }

            }
            dtFormulario.DefaultView.Sort = "idControlAsociado";
            return dtFormulario;
        }

        public DataTable GetDatosFormularioReservaF(UserControl PageSource, Dictionary<string, string> dCamposFormulario)
        {
            /*OBTENEMOS TODOS LOS CAMPOS DEL USERCONTROL Y LOS ALMACENAMOS EN UN DATATABLE*/
            DataTable dtFormulario = new DataTable();

            dtFormulario.Columns.Add("idControl", typeof(string));
            dtFormulario.Columns.Add("valorControl", typeof(string));
            dtFormulario.Columns.Add("Control", typeof(Control));
            dtFormulario.Columns.Add("Enum_Controls", typeof(Type));
            dtFormulario.Columns.Add("idControlAsociado", typeof(string));
            dtFormulario.PrimaryKey = new DataColumn[] { dtFormulario.Columns["idControl"] };

            foreach (KeyValuePair<string, string> dItem in dCamposFormulario)
            {
                //int i = 0;
                DataRow drNevaFila = dtFormulario.NewRow();

                Control control = PageSource.FindControl(dItem.Key);

                //if (control == null)
                //{
                //    if (i == 0)
                //        dItem.Key= txtComentarios.key;
                //        control = txtComentarios;
                //    i++;
                //    if (i == 1)
                //        control = ddlPais;
                //    i++;
                //    if (i == 2)
                //        control = ddlTipoMensaje;
                //    i++;
                //    if (i == 3)
                //        control = ddlTemaMensaje;
                //    i++;
                //}

                drNevaFila["idControlAsociado"] = dItem.Key;
                try
                {
                    if (control is Label)
                    {
                        drNevaFila["idControl"] = (control as Label).ID;

                        drNevaFila["valorControl"] = (control as Label).Text;

                        drNevaFila["Enum_Controls"] = control.GetType();
                    }
                }
                catch (Exception j)
                {
                }
                if (control is TextBox)
                {
                    drNevaFila["idControl"] = (control as TextBox).ID;

                    drNevaFila["valorControl"] = (control as TextBox).Text;

                    drNevaFila["Enum_Controls"] = control.GetType();
                }
                else if (control is RadioButtonList)
                {
                    drNevaFila["idControl"] = (control as RadioButtonList).ID;

                    drNevaFila["valorControl"] = (control as RadioButtonList).SelectedItem.Value;

                    drNevaFila["Enum_Controls"] = control.GetType();
                }
                else if (control is HiddenField)
                {
                    drNevaFila["idControl"] = (control as HiddenField).ID;

                    drNevaFila["valorControl"] = (control as HiddenField).Value;

                    drNevaFila["Enum_Controls"] = control.GetType();
                }
                else if (control is DropDownList)
                {
                    drNevaFila["idControl"] = (control as DropDownList).ID;

                    drNevaFila["valorControl"] = (control as DropDownList).SelectedItem.Text;

                    drNevaFila["control"] = control;

                    drNevaFila["Enum_Controls"] = control.GetType();
                }
                else if (control is HtmlGenericControl)
                {
                    drNevaFila["idControl"] = (control as HtmlGenericControl).ID;

                    drNevaFila["valorControl"] = (control as HtmlGenericControl).InnerHtml;

                    drNevaFila["control"] = control;

                    drNevaFila["Enum_Controls"] = control.GetType();
                }
                if (!drNevaFila["idControl"].ToString().Equals(string.Empty))
                {
                    dtFormulario.Rows.Add(drNevaFila);
                }

            }
            dtFormulario.DefaultView.Sort = "idControlAsociado";
            return dtFormulario;
        }

        public bool SaveForm(DataTable dtDatosForm, string sAdicionales)
        {
            /*GUARDAMOS LOS DATOS DEL FORMULARIO EN LA TABLA "TBLCASOS" */
            bool blGuardo = false;
            if (dtDatosForm != null && dtDatosForm.Rows.Count > 0)
            {
                try
                {
                    clsCache cCache = new csCache().cCache();
                    csConsultasContactenos csCont = new csConsultasContactenos();
                    csGenerales csRefere = new csGenerales();
                    string sEstadoInicialMsj = "0";
                    DataTable tblEstado = csCont.codigo_estado_buzon(clsValidaciones.GetKeyOrAdd("EstadoMsjRecibido", "ESTMSJ2"));
                    if (tblEstado != null)
                        sEstadoInicialMsj = tblEstado.Rows[0]["intCodigo"].ToString();

                    string sTipoMsj = FindValue("ddlTipoMensaje", dtDatosForm);
                    string temaMensaje = FindValue("ddlTemamensaje", dtDatosForm);
                    if (temaMensaje.Equals("Seleccionar"))
                        temaMensaje = "0";
                    string sFechaSolicitud = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    string sHoraSolicitud = DateTime.Now.ToString("HH:mm:ss");
                    string strSolicitante = FindValue("txtNombres", dtDatosForm) + "  " + FindValue("txtApellidos", dtDatosForm);
                    string strEmail = FindValue("txtEmail", dtDatosForm);
                    string strCodPais = FindValue("ddlPais", dtDatosForm);
                    string strCiudad = FindValue("txtCiudad", dtDatosForm);
                    string strTelefono = FindValue("txtTelefono", dtDatosForm);
                    string strComentarios = "";
                    if (!FindValue("txtComentarios", dtDatosForm).Equals(string.Empty) && !FindValue("txtComentarios", dtDatosForm).Equals("0"))
                    {
                        strComentarios = FindValue("txtComentarios", dtDatosForm) + "<br /><br />" + sAdicionales;
                    }

                    DataTable tblResp = csCont.insertar_buzon(cCache.Empresa, "0", sTipoMsj, temaMensaje, sEstadoInicialMsj, strComentarios, sFechaSolicitud,
                        sHoraSolicitud, strSolicitante, strEmail, strCodPais, "0", sFechaSolicitud, sHoraSolicitud, cCache.Contacto, strTelefono, strCiudad);
                    if (tblResp != null && tblResp.Rows[0][0].ToString() != "0")
                        blGuardo = true;
                }
                catch (Exception)
                {

                }
            }

            return blGuardo;
        }

        public bool SaveFormFailHotels(DataTable dtDatosForm, string sAdicionales, string email, string txtComentarios, string ddlTipoMensaje, string ddlTemamensaje, string ddlPais, string txtcelular, string nombre, string apellido)
        {
            /*GUARDAMOS LOS DATOS DEL FORMULARIO EN LA TABLA "TBLCASOS" */
            bool blGuardo = false;
            if (dtDatosForm != null && dtDatosForm.Rows.Count > 0)
            {
                try
                {
                    clsCache cCache = new csCache().cCache();
                    csConsultasContactenos csCont = new csConsultasContactenos();
                    csGenerales csRefere = new csGenerales();
                    string sEstadoInicialMsj = "0";
                    DataTable tblEstado = csCont.codigo_estado_buzon(clsValidaciones.GetKeyOrAdd("EstadoMsjRecibido", "ESTMSJ2"));
                    if (tblEstado != null)
                        sEstadoInicialMsj = tblEstado.Rows[0]["intCodigo"].ToString();

                    string sTipoMsj = FindValue("ddlTipoMensaje", dtDatosForm);
                    if (sTipoMsj == "" || sTipoMsj == null || sTipoMsj == "0")
                    {
                        sTipoMsj = "1";
                    }
                    string temaMensaje = FindValue("ddlTemamensaje", dtDatosForm);
                    if (temaMensaje == "" || temaMensaje == null || temaMensaje == "0")
                    {
                        temaMensaje = "1";
                    }
                    if (temaMensaje.Equals("Seleccionar"))
                        temaMensaje = "0";
                    string sFechaSolicitud = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    string sHoraSolicitud = DateTime.Now.ToString("HH:mm:ss");
                    string strSolicitante = FindValue("txtNombres", dtDatosForm) + "  " + FindValue("txtApellidos", dtDatosForm);
                    strSolicitante = nombre + " " + apellido;
                    string strEmail = FindValue("txtEmail", dtDatosForm);
                    if (strEmail == "" || strEmail == null || strEmail == "0")
                    {
                        strEmail = email;
                    }
                    string strCodPais = FindValue("ddlPais", dtDatosForm);
                    if (strCodPais == "" || strCodPais == null || strCodPais == "0")
                    {
                        strCodPais = "0";
                    }
                    string strCiudad = FindValue("txtCiudad", dtDatosForm);
                    string strTelefono = FindValue("txtTelefono", dtDatosForm);
                    if (strTelefono == "" || strTelefono == null || strTelefono == "0")
                    {
                        strTelefono = txtcelular;
                    }
                    string strComentarios = txtComentarios;
                    if (!FindValue("txtComentarios", dtDatosForm).Equals(string.Empty) && !FindValue("txtComentarios", dtDatosForm).Equals("0"))
                    {
                        strComentarios = FindValue("txtComentarios", dtDatosForm) + "<br /><br />" + sAdicionales;
                    }

                    DataTable tblResp = csCont.insertar_buzon(cCache.Empresa, "0", sTipoMsj, temaMensaje, sEstadoInicialMsj, strComentarios, sFechaSolicitud,
                        sHoraSolicitud, strSolicitante, strEmail, strCodPais, "0", sFechaSolicitud, sHoraSolicitud, cCache.Contacto, strTelefono, strCiudad);
                    if (tblResp != null && tblResp.Rows[0][0].ToString() != "0")
                        blGuardo = true;
                }
                catch (Exception)
                {

                }
            }

            return blGuardo;
        }

        private string FindValue(string strIdControl, DataTable dtDatosForm)
        {
            /*OBTENEMOS EL VALOR DEPENDIENDO DEL CONTROL*/
            string strValor = default(string);

            Control control = default(Control);
            if (!dtDatosForm.TableName.Equals("dtDatosControles"))
            {

                if (dtDatosForm.Rows.Contains(strIdControl))
                {
                    if (dtDatosForm.Rows.Find(strIdControl)["control"] != null && dtDatosForm.Rows.Find(strIdControl)["control"] is Control)
                    {
                        control = dtDatosForm.Rows.Find(strIdControl)["control"] as Control;

                        if (control is DropDownList)
                        {
                            strValor = (control as DropDownList).SelectedValue;
                        }
                    }
                    else
                    {

                        if (dtDatosForm.Rows.Find(strIdControl)["valorControl"] == null || dtDatosForm.Rows.Find(strIdControl)["valorControl"].ToString().Trim().Equals(""))
                        {
                            strValor = "0";
                        }
                        else
                        {
                            strValor = dtDatosForm.Rows.Find(strIdControl)["valorControl"].ToString().Trim();
                        }
                    }
                }
                else
                {
                    strValor = "0";
                }

            }
            else
            {
                if (dtDatosForm.Rows.Contains(strIdControl))
                    if (dtDatosForm.Rows.Find(strIdControl)["ControlId"] != null)
                        if (dtDatosForm.Rows.Find(strIdControl)["ControlType"].ToString().Equals(typeof(DropDownList).Name))
                            strValor = dtDatosForm.Rows.Find(strIdControl)["ControlDdlValue"].ToString();
                        else
                            strValor = dtDatosForm.Rows.Find(strIdControl)["ControlValue"].ToString();
                    else
                        strValor = "0";
            }
            return strValor;
        }

        /// <summary>
        /// Guardar datos del formuario de buzon de "Quiero que me contacten"
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="correo"></param>
        /// <param name="telefono"></param>
        /// <param name="comentarios"></param>
        /// <param name="nombrePlan"></param>
        /// <returns></returns>
        public bool GuardarDatosBuzonQuieroQueMeContacten(string nombre, string correo, string telefono, string comentarios, string nombrePlan)
        {
            /*GUARDAMOS LOS DATOS DEL FORMULARIO EN LA TABLA "TBLCASOS" */
            bool blGuardo = false;

            try
            {
                clsCache cCache = new csCache().cCache();
                csConsultasContactenos csCont = new csConsultasContactenos();
                csGenerales csRefere = new csGenerales();
                string sEstadoInicialMsj = "0";
                DataTable tblEstado = csCont.codigo_estado_buzon(clsValidaciones.GetKeyOrAdd("EstadoMsjRecibido", "ESTMSJ2"));
                if (tblEstado != null)
                    sEstadoInicialMsj = tblEstado.Rows[0]["intCodigo"].ToString();

                string sTipoMsj = "1";
                string temaMensaje = "1";
                string sFechaSolicitud = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string sHoraSolicitud = DateTime.Now.ToString("HH:mm:ss");
                string strSolicitante = nombre;
                string strEmail = correo;
                string strCodPais = "1";
                string strCiudad = "1";
                string strTelefono = telefono;
                string strComentarios = "Comentarios: " + comentarios + "</br> Nombre plan: " + nombrePlan;

                DataTable tblResp = csCont.insertar_buzon(cCache.Empresa, "0", sTipoMsj, temaMensaje, sEstadoInicialMsj, strComentarios, sFechaSolicitud,
                    sHoraSolicitud, strSolicitante, strEmail, strCodPais, "0", sFechaSolicitud, sHoraSolicitud, cCache.Contacto, strTelefono, strCiudad);
                if (tblResp != null && tblResp.Rows[0][0].ToString() != "0")
                    blGuardo = true;
            }
            catch (Exception)
            {
                blGuardo = false;
            }

            return blGuardo;
        }


        /// <summary>
        /// Guardar datos del formuario de buzon de "Quiero que me contacten"
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="correo"></param>
        /// <param name="telefono"></param>
        /// <param name="comentarios"></param>
        /// <param name="nombrePlan"></param>
        /// <returns></returns>
        public bool GuardarDatosBuzonRegistroEmpresa(string razonSocial, string nit, string direccion, string ciudad, string telefono, string contacto, string cargo, string correoElectronico)
        {
            /*GUARDAMOS LOS DATOS DEL FORMULARIO EN LA TABLA "TBLCASOS" */
            bool blGuardo = false;

            try
            {
                clsCache cCache = new csCache().cCache();
                csConsultasContactenos csCont = new csConsultasContactenos();
                csGenerales csRefere = new csGenerales();
                string sEstadoInicialMsj = "0";
                DataTable tblEstado = csCont.codigo_estado_buzon(clsValidaciones.GetKeyOrAdd("EstadoMsjRecibido", "ESTMSJ2"));
                if (tblEstado != null)
                    sEstadoInicialMsj = tblEstado.Rows[0]["intCodigo"].ToString();

                string sTipoMsj = "1";
                string temaMensaje = "1";
                string sFechaSolicitud = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string sHoraSolicitud = DateTime.Now.ToString("HH:mm:ss");
                string strSolicitante = razonSocial;
                string strEmail = correoElectronico;
                string strCodPais = "1";
                string strCiudad = "1";
                string strTelefono = telefono;
                string strDatosAdicionales = "Ciudad: " + ciudad;
                strDatosAdicionales += "</br> Direccion: " + direccion;
                strDatosAdicionales += "</br> Contacto: " + contacto;
                strDatosAdicionales += "</br> Cargo: " + cargo;
                strDatosAdicionales += "</br> Correo Electronico: " + correoElectronico;

                DataTable tblResp = csCont.insertar_buzon(cCache.Empresa, "0", sTipoMsj, temaMensaje, sEstadoInicialMsj, strDatosAdicionales, sFechaSolicitud,
                    sHoraSolicitud, strSolicitante, strEmail, strCodPais, "0", sFechaSolicitud, sHoraSolicitud, cCache.Contacto, strTelefono, strCiudad);
                if (tblResp != null && tblResp.Rows[0][0].ToString() != "0")
                    blGuardo = true;
            }
            catch (Exception)
            {
                blGuardo = false;
            }

            return blGuardo;
        }
    }
}
