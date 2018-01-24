using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;
using Ssoft.Pages.PaginaHoteles;
using Ssoft.Pages;
using SsoftQuery.Vuelos;
using Ssoft.Rules.Reservas;
using Ssoft.Rules.Generales;


public partial class uc_ucReservaHotel : System.Web.UI.UserControl
{

    csBusquedaHoteles csRefere = new csBusquedaHoteles();
    clsCache cCache = new csCache().cCache();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int abc = 0, i = 0;
            string[] habsAbc = null;
            //Label Error123 = (Label)FindControl("Error123");
            try
            {
                csRefere.setFormularioHB(cCache, this);

                foreach (RepeaterItem item in RptPenalizacionGara.Items)
                {
                    Label lblTxtHoraLimite = (Label)item.FindControl("lblTxtHoraLimite");
                    Label lblTxtFechaLimite = (Label)item.FindControl("lblTxtFechaLimite");
                    Label lbllblTxtPlataLimite = (Label)item.FindControl("lbllblTxtPlataLimite");
                    Label lbllblTxtCurrencyLimite = (Label)item.FindControl("lbllblTxtCurrencyLimite");
                }

                foreach (RepeaterItem item in rptHabitaciones.Items)
                {
                    Label lblTipoHabitacion = (Label)item.FindControl("lblTipoHabitacion");
                    if (lblTipoHabitacion != null)
                    {
                        abc = abc + 1;
                    }
                }
                habsAbc = new string[abc];


                foreach (RepeaterItem item in rptHabitaciones.Items)
                {
                    Label lblTipoHabitacion = (Label)item.FindControl("lblTipoHabitacion");
                    if (lblTipoHabitacion != null)
                    {
                        habsAbc[i] = lblTipoHabitacion.Text;
                        i++;
                    }
                }


            }
            catch { }
            int counterHab = 1;
            i = 0;
            foreach (RepeaterItem item in rptDatosReserva.Items)
            {
                DropDownList ddlGenero = (DropDownList)item.FindControl("ddlGenero");
                if (ddlGenero != null)
                {
                    new csGenerales().sConsultaGeneros(ddlGenero, false);
                }

                DataTable dtDataDoc = new CsConsultasVuelos().SPConsultaTabla("SPConsultaTpoidentifica", new string[1] { new csCache().cCache().Idioma });
                DropDownList ddlTipoDoc = item.FindControl("ddlTipoDoc") as DropDownList;
                if (ddlTipoDoc != null)
                {
                    if (dtDataDoc != null)
                    {
                        if (clsValidaciones.GetKeyOrAdd("ValorBlancoDoc", "False").ToUpper().Equals("TRUE"))
                            clsControls.LlenaControl(ddlTipoDoc, dtDataDoc, "STRDESCRIPCION", "INTCODE", true);
                        else
                            clsControls.LlenaControl(ddlTipoDoc, dtDataDoc, "STRDESCRIPCION", "INTCODE", false);

                    }

                    try
                    {
                        clsCache cCache = new csCache().cCache();

                        txtTelefono.Text = cCache.Telefono;
                        if (txtTelefono.Text.Equals("SIN DATOS") || txtTelefono.Text.Equals(""))
                            txtTelefono.Text = cCache.Celular;

                        txtMailPersonal.Text = cCache.Email;


                        txtCiudad.Text = cCache.Ciudad;
                        txtCiudad.Text = new CsConsultasVuelos().ConsultaCodigo(txtCiudad.Text, "tblCiudadesIdiomas", "strDescription", "strIdioma='" + cCache.Idioma + "' and " + "intCode");

                        txtDocumento.Text = cCache.Identificacion;

                        txtCelular.Text = cCache.Celular;
                        txtNombre.Text = cCache.Nombres;
                        txtApellido.Text = cCache.Apellidos;
                    }
                    catch { }
                }
                //lblGarantia2 = lblGarantia;

                try
                {
                    Label lblhab_Counter = (Label)item.FindControl("lblhab_Counter");
                    if (lblhab_Counter != null)
                    {
                        lblhab_Counter.Text = "Habitación " + counterHab + "(" + habsAbc[i] + ")";
                    }
                    counterHab = counterHab + 1;
                    i++;
                }
                catch (Exception g)
                {
                }
            }
            counterHab = 1;
            i = 0;
            foreach (RepeaterItem item in RptPenalizacionGara.Items)
            {
                Label lblhab_Counter = (Label)item.FindControl("lblhab_Counter");
                if (lblhab_Counter != null)
                {
                    lblhab_Counter.Text = "Habitación " + counterHab + "(" + habsAbc[i] + ")";
                    i++;
                    counterHab++;
                }
            }
        }
    }
    protected void setCommand(object sender, CommandEventArgs e)
    {
        bool edaderror = true;
        //        Label lblErrorEdad123 = new Label();
        foreach (RepeaterItem item in rptDatosReserva.Items)
        {
            TextBox txtEdad1 = (TextBox)item.FindControl("txtEdad1");
            Label txtedad1 = new Label();
            txtedad1.Text = txtEdad1.Text;
            DateTime txtEdad12 = DateTime.ParseExact(txtEdad1.Text, "MM/dd/yyyy", null);
            //DateTime txtEdad12 = DateTime.MinValue;
            //txtEdad12 = Convert.ToDateTime(txtedad1.Text);
            if (DateTime.Now.Subtract(txtEdad12).Days < (18 * 365))
            {
                edaderror = false;
            }
        }
        if (edaderror == true)
        {
            int a = 1;
            foreach (RepeaterItem item in rptDatosReserva.Items)
            {
                TextBox txtApellido1 = (TextBox)item.FindControl("txtApellido1");
                TextBox txtNombre1 = (TextBox)item.FindControl("txtNombre1");
                TextBox txtDocumento1 = (TextBox)item.FindControl("txtDocumento1");
                TextBox txtEdad1 = (TextBox)item.FindControl("txtEdad1");

                if (txtApellido1.Text.Trim() != "" && txtNombre1.Text.Trim() != "" && txtDocumento1.Text.Trim() != "" && txtEdad1.Text.Trim() != "")
                {
                    if (a == 1)
                    {
                        if (rblFormasPago.SelectedValue.ToString() != "")
                        {
                            clsParametros Registro = csRefere.setCrearNoRegistroHotelInter(this, Enum_Login.LoginGen, false, false);
                            if (Registro.Id != 0)
                            {
                                //Label ErrorReserva = new Label();
                                csRefere.setCommand(this, sender, e);
                                //if (lblError.Text != "0" )
                                //{
                                //    try
                                //    {
                                //        AjaxControlToolkit.ModalPopupExtender mpeFallo = (AjaxControlToolkit.ModalPopupExtender)FindControl("MPEEFallo");
                                //        if (mpeFallo !=null)
                                //        {
                                //        mpeFallo.Show();
                                //        }
                                //    }
                                //    catch (Exception j)
                                //    {
                                //    }
                                //}
                                a++;
                            }
                        }
                        else
                        {
                            MPEEReserva.Hide();
                            MPerrores.Show();
                            lblErrores.Text = "Por favor seleccione una forma de pago";
                        }
                    }
                    else
                    {
                        //MPEEReserva.Show();
                        //MPEEConfirm.Show();
                        
                        return;
                    }
                }
                else
                {
                    MPEEReserva.Hide();
                    MPerrores.Show();
                    lblErrores.Text = "Los Campos Marcados con (*) son Obligatorios Por favor Diligencielos";

                }
            }
        }
        else
        {
            foreach (RepeaterItem item in rptDatosReserva.Items)
            {
                Label lblErrorEdad123 = (Label)item.FindControl("lblErrorEdad123");
                lblErrorEdad123.Text = "El pasajero que se registra debe ser mayor de 18 años";
            }
        }
    }
    public void btnCancelar_Click(object sender, EventArgs e)
    {
        csCarrito cCar = new csCarrito("Reserva" + new csCache().cCache().SessionID, "CarritoCompras");
        cCar.LimpiarCarrito();
        clsValidaciones.RedirectPaginaIni("Index.aspx", true);
    }
    private bool ValidacamposTarjeta()
    {

        bool validafecha = true;
        if (rblFormasPago.SelectedValue.Trim().Equals(clsValidaciones.GetKeyOrAdd("TarjetaCredito")))
        {
            ExceptionHandled.Publicar("*************************////////////////////////////-- INICIO DE LA VALIDACION DE DATOS DE TARJETA & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");
            try
            {
                if (ddlFranquicia.SelectedValue.Trim().Equals("") || ddlFranquicia.SelectedValue.Trim().Equals("0") || txtNumTarjeta.Text.Trim().Equals("") ||
                    txtBanco.Text.Trim().Equals("") || txtAnioVencimiento.Text.Trim().Equals("") || txtCodSeguridad.Text.Trim().Equals("") ||
                    ddlMesVencimiento.SelectedValue.Trim().Equals("") || ddlMesVencimiento.SelectedValue.Trim().Equals("0") || txtCuotas.Text.Trim().Equals("") ||
                    txtTitular.Text.Trim().Equals("") || txtIdentificacion.Text.Trim().Equals("") || txtDireccion.Text.Trim().Equals("") ||
                    txtTelefonoOficina.Text.Trim().Equals("") || txtTelefonoOtro.Text.Trim().Equals(""))
                {
                    lblErrorTarjeta.Text = clsValidaciones.GetKeyOrAdd("sErrorCamposTarjetas", "Por favor diligencia toda la información de tu tarjeta");
                    validafecha = false;
                }
                else
                {
                    if (txtNumTarjeta.Text.Trim().Length < 14)
                    {
                        lblErrorTarjeta.Text = clsValidaciones.GetKeyOrAdd("sTextoErrorNoTarjeta", "El número de tarjeta no cumple los requisitos (más de 14 dígitos), por favor revisa la información");
                        validafecha = false;
                    }
                    else
                    {
                        if (txtCodSeguridad.Text.Trim().Length < 3 || txtCodSeguridad.Text.Trim().Length > 4)
                        {
                            lblErrorTarjeta.Text = clsValidaciones.GetKeyOrAdd("sTextoErrorCodSeguridad", "El código de seguridad no cumple los requisitos (más de 3 y menos de 4 dígitos), por favor revisa la información");
                            validafecha = false;
                        }
                        else
                        {
                            try
                            {

                                int iMes = Convert.ToInt32(clsValidaciones.RetornaMesNumerosLargo(ddlMesVencimiento.SelectedValue.Trim()));
                                int iAnio = Convert.ToInt32(txtAnioVencimiento.Text.Trim());
                                if (iAnio.ToString().Length.Equals(2))
                                {
                                    iAnio = 2000 + iAnio;
                                }

                                if (iAnio < DateTime.Now.Year)
                                {
                                    lblErrorTarjeta.Text = clsValidaciones.GetKeyOrAdd("sTextoErrorFechaVencimiento", "La fecha de vencimiento no puede ser menor a la fecha de hoy, por favor revisa la información");
                                    validafecha = false;
                                }
                                else
                                {
                                    if (iAnio == DateTime.Now.Year && iMes < DateTime.Now.Month)
                                    {
                                        lblErrorTarjeta.Text = clsValidaciones.GetKeyOrAdd("sTextoErrorFechaVencimiento", "La fecha de vencimiento no puede ser menor a la fecha de hoy, por favor revisa la información");
                                        validafecha = false;
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            catch
            {
                lblErrorTarjeta.Text = clsValidaciones.GetKeyOrAdd("sTextoErrorCatchTarjeta", "Los datos de tu tarjeta no pudieron ser procesados, por favor revisa la información");
                validafecha = false;
            }

        }
        else
        {
            ExceptionHandled.Publicar("*************************////////////////////////////-- SELECCION DE FORMA DE PAGO: " + rblFormasPago.SelectedValue + " & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");
        }
        return validafecha;
    }


    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["urlpse"] != null)
        {
            clsValidaciones.RedirectPagina(HttpContext.Current.Session["urlpse"].ToString());
            HttpContext.Current.Session["urlpse"] = null;
        }
        else
        {
            clsValidaciones.RedirectPagina("index.aspx");
        }
    }
    public void btnCancelarResrva_Click(object sender, EventArgs e)
    {
        List<string> sReserva = new List<string>();
        sReserva.Add(lblRecord.Text);
        clsParametros iresultado = new csBusquedaHoteles().CancelarReservaTT(sReserva);
        clsValidaciones.RedirectPagina("indexInterno.aspx");
    }
    protected void rblFormasPago_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("EFE"))
        {
            DivTC.Style.Add("display", "none");
            DivPSE.Style.Add("display", "none");
            DivEfe.Style.Add("display", "block");
        }
        else if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("TC"))
        {
            DivTC.Style.Add("display", "block");
            DivPSE.Style.Add("display", "none");
            DivEfe.Style.Add("display", "none");
        }
        else if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("PSE"))
        {
            DivTC.Style.Add("display", "none");
            DivPSE.Style.Add("display", "block");
            DivEfe.Style.Add("display", "none");
        }



    }
}
