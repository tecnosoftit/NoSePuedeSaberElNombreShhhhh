using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using Ssoft.Pages;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;
using Ssoft.Pages.PaginaPlanes;
using Ssoft.Rules.Reservas;
using Ssoft.Rules.Generales;
using System.Web.UI.WebControls;

public partial class uc_ucReservaTour : System.Web.UI.UserControl
{
    csReservaPlanes csResultadoPlanes = new csReservaPlanes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csResultadoPlanes.SetFormularioReservaCotizador(this);
        }
    }

    protected void EliminarConstraints(ref DataSet ds)
    {
        ds.Tables["tblTarifa"].Constraints.Clear();
        ds.Tables["tblTax"].Constraints.Clear();
        ds.Tables["tblPax"].Constraints.Clear();
        ds.Tables["tblTransac"].Constraints.Clear();
        ds.Tables["tblreserva"].Constraints.Clear();
        ds.Tables["tblHabitaciones"].Constraints.Clear();
        ds.Tables["TablaDatosVisibles"].Constraints.Clear();
        ds.Tables["CarritoCompras"].Constraints.Clear();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        csCarrito cCar = new csCarrito("Reserva" + new csCache().cCache().SessionID, "CarritoCompras");
        cCar.LimpiarCarrito();
        clsValidaciones.RedirectPaginaIni("Planes.aspx", true);
    }

    protected void btnReservar_Click(object sender, EventArgs e)
    {
        AjaxControlToolkit.ModalPopupExtender mpterminos = (AjaxControlToolkit.ModalPopupExtender)Parent.FindControl("MPEEBanner");

        //MPEEReserva.Show();
        if (rblFormasPago.Items.Count > 0)
        {
            if (ValidacamposTarjeta())
            {
                if (!cbAcepto.Checked)
                {
                    rbltapspagos();
                    lblError.Text = "Por favor acepta los terminos y condicones";
                    return;
                }
                else
                {
                    csResultadoPlanes.btnReservarCotizador_Click(sender, e, this);
                    if (this.Session["$CodigoReservaPlan"] != null)
                    {
                        //MPEEReserva.Hide();
                        lblRecord.Text = this.Session["$CodigoReservaPlan"].ToString();
                        if (mpterminos != null)
                        {
                            mpterminos.Hide();
                        }

                        MPEEConfirm.Show();
                    }
                }
            }
        }
        //MPEEReserva.Hide();sValidaciones.GetKeyOrAdd("sMensajeNoCreacionUsuario", "No se pudo generar tu reserva, por favor completa la información obligatoria o contacta un asesor");
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        csCarrito cCar = new csCarrito("Reserva" + new csCache().cCache().SessionID, "CarritoCompras");
        cCar.LimpiarCarrito();
        clsValidaciones.RedirectPaginaIni("IndexInterno.aspx", true);
    }

    protected void rblLugar_SelectedIndexChanged(object sender, EventArgs e)
    {       
        //csResultadoPlanes.setMostrarPanelesReserva(this, sender);
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
                    txtBanco.Text.Trim().Equals("") || /*txtAnioVencimiento.Text.Trim().Equals("") ||*/ txtCodSeguridad.Text.Trim().Equals("") ||
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
                                int iAnio = Convert.ToInt32(txtAnioVencimiento.SelectedValue);
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

    protected void btnSeleccionPasajeros_Click(object sender, EventArgs e)
    {
        rbltapspagos();
        Button btnSeleccion = (Button)sender;
        RepeaterItem RpPasajeros = (RepeaterItem)btnSeleccion.Parent;
        Repeater RpCabina = (Repeater)RpPasajeros.Parent;
        RepeaterItem RpiCabina = (RepeaterItem)RpCabina.Parent;
        lbliditemseleccionado.Text = RpiCabina.ItemIndex.ToString() + "|" + RpPasajeros.ItemIndex.ToString();

        DataTable dtLista = new csResultadoVuelos().SetBuscarUsuarios(new csCache().cCache().Contacto, new csCache().cCache().Empresa);
        int Afiliados = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumAfiliados", "6"));

        new csGenerales().sConsultaGeneros(ddlGeneroR, false);
        new csGenerales().sConsultaTposIdentificacion(ddlTpoDocumentoR, false);



        if (dtLista.Rows.Count >= Afiliados)
        {
            btnRegistrar.Visible = false;
        }

        if (dtLista.Rows.Count == 1)
        {
            lbltexto.Text = clsValidaciones.GetKeyOrAdd("textoSinAfiliados", "Aun no haz regístrado tus elegidos, haz Click en el botón Registrar Nuevo para Registrarlos.");
        }
        else if (dtLista.Rows.Count < Afiliados)
        {
            int elegidos = Afiliados - dtLista.Rows.Count;
            if (elegidos == 1)
            {
                lbltexto.Text = "Aún puedes registrar un (1) elegido más.";
            }
            else
            {
                lbltexto.Text = "Aún puedes registrar  (" + elegidos.ToString() + ") elegidos más.";

            }

        }

        if (dtLista != null)
        {
            rptpasajeros.DataSource = dtLista;
            rptpasajeros.DataBind();
            HttpContext.Current.Session["dtLista"] = dtLista;
        }

        MPAfiliados.Show();
    }

    protected void btnseleccionar_Click(object sender, EventArgs e)
    {
        rbltapspagos();
        if (HttpContext.Current.Session["dtLista"] != null)
        {

            Button btnseleciono = (Button)sender;
            RepeaterItem rptSeleccion = (RepeaterItem)btnseleciono.Parent;
            int itemseleccionado = rptSeleccion.ItemIndex;

            DataTable dtIvalores = (DataTable)HttpContext.Current.Session["dtLista"];
            string[] Items = lbliditemseleccionado.Text.Split('|');
            int Cabina = Convert.ToInt32(Items[0].ToString());
            int item = Convert.ToInt32(Items[1].ToString());
            Repeater rpPasajero = (Repeater)rptCabinas.Items[Cabina].FindControl("rptPasajeros");

            if (ValidaPasajerosRepetidos(dtIvalores.Rows[itemseleccionado][1].ToString()))
            {
                TextBox txtnombre = (TextBox)rpPasajero.Items[item].FindControl("txtNombre");
                TextBox txtApellido = (TextBox)rpPasajero.Items[item].FindControl("txtApellido");
                DropDownList ddlGenero = (DropDownList)rpPasajero.Items[item].FindControl("ddlGenero");
                DropDownList ddlTipoDoc = (DropDownList)rpPasajero.Items[item].FindControl("ddlTipoIdent");
                TextBox txtEdad1 = (TextBox)rpPasajero.Items[item].FindControl("txtNacimiento");
                TextBox txtDocumento = (TextBox)rpPasajero.Items[item].FindControl("txtPasaporte");

                string sFecha = dtIvalores.Rows[itemseleccionado][5].ToString();
                sFecha = clsValidaciones.ConverFecha(sFecha, Enum_FormatoFecha.MDY);
                txtnombre.Text = dtIvalores.Rows[itemseleccionado][2].ToString();
                txtApellido.Text = dtIvalores.Rows[itemseleccionado][3].ToString();
                txtDocumento.Text = dtIvalores.Rows[itemseleccionado][1].ToString();
                txtEdad1.Text = sFecha;
                ddlTipoDoc.ClearSelection();

                ddlTipoDoc.Items.FindByValue(dtIvalores.Rows[itemseleccionado][6].ToString()).Selected = true;
                ddlGenero.ClearSelection();
                ddlGenero.Items.FindByValue(dtIvalores.Rows[itemseleccionado][4].ToString()).Selected = true;


                lblError.Text = "";

            }
            else
            {
                lblError.Text = "Este Pasajero ya fue Seleccionado Por Favor Seleccione otro";
            }

        }


    }

    private bool ValidaPasajerosRepetidos(string strDocumento)
    {
        bool bValida = true;

        try
        {
            if (rptCabinas != null)
            {
                if (rptCabinas.Items.Count > 0)
                {
                    for (int b = 0; b < rptCabinas.Items.Count; b++)
                    {
                        Repeater RpPasajeros = (Repeater)rptCabinas.Items[b].FindControl("rptPasajeros");
                        if (RpPasajeros != null)
                        {
                            if (RpPasajeros.Items.Count > 0)
                            {
                                for (int i = 0; i < RpPasajeros.Items.Count; i++)
                                {
                                    TextBox txtDoc = (TextBox)RpPasajeros.Items[i].FindControl("txtPasaporte");
                                    if (txtDoc != null)
                                    {
                                        if (txtDoc.Text.Trim().Equals(strDocumento))
                                        {
                                            bValida = false;
                                        }
                                    }

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
        catch
        {
            return false;
        }

        return bValida;

    }

    protected void lbCrear_Click(object sender, EventArgs e)
    {
        rbltapspagos();
        int contactoPadre = Convert.ToInt32(new csCache().cCache().Contacto);
        bool bValida = new csReservaPlanes().SetCrearAfiliadosPlanes(this, contactoPadre);

        DataTable dtLista = new csResultadoVuelos().SetBuscarUsuarios(new csCache().cCache().Contacto, new csCache().cCache().Empresa);
        int Afiliados = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumAfiliados", "6"));

        new csGenerales().sConsultaGeneros(ddlGeneroR, false);
        new csGenerales().sConsultaTposIdentificacion(ddlTpoDocumentoR, false);

        if (dtLista.Rows.Count >= Afiliados)
        {
            btnRegistrar.Visible = false;
        }
        else if (dtLista.Rows.Count == 1)
        {
            lbltexto.Text = clsValidaciones.GetKeyOrAdd("textoSinAfiliados", "Usted no tiene usuarios afiliados. Haga click en el botón Registrar Nuevo para crearlos.");
        }

        if (dtLista != null)
        {
            rptpasajeros.DataSource = dtLista;
            rptpasajeros.DataBind();
            HttpContext.Current.Session["dtLista"] = dtLista;
        }
        MPAfiliados.Show();
        if (bValida)
        {
            lbltexto.Text = "Su afiliado fue creado Exitosamente";

        }
        else
        {
            lbltexto.Text = "Lo sentimos Por Favor Vuelva a crear su afiliado ";
        }
    }

    protected void rbltapspagos()
    {
        if (rblFormasPago.Items.Count > 0)
        {
            for (int i = 0; i < rblFormasPago.Items.Count; i++)
            {
                rblFormasPago.Items[i].Attributes.Add("onclick", "javascript:ActivarDivFP(this)");
            }
        }
        if (rblFormasPago.SelectedItem != null)
            rblFormasPago.SelectedItem.Selected = false;
    }
}
