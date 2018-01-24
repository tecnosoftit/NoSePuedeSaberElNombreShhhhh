using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ssoft.Utils;
using Ssoft.Pages;
using Ssoft.ManejadorExcepciones;
using AjaxControlToolkit;
using SsoftQuery.Vuelos;

public partial class uc_ucReservaVuelosTarjeta : System.Web.UI.UserControl
{
  
    csResultadoVuelos csRefere = new csResultadoVuelos();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        clsCache cCache = new csCache().cCache();
        DataSet dtAutocompletar = new CsConsultasVuelos().EjecutaSpProcedimiento("SPConsultaTodasCiudad", new string[1] { "es" }); ;        
        string autocompletar = "var autocompletar = new Array(";
        for (int i = 0; i < dtAutocompletar.Tables[0].Rows.Count; i++)
        {
            if (i != (dtAutocompletar.Tables[0].Rows.Count - 1))
            {
                autocompletar = autocompletar + "\"" + dtAutocompletar.Tables[0].Rows[i][1].ToString() + "\", ";
            }
            else
            {
                autocompletar = autocompletar + "\"" + dtAutocompletar.Tables[0].Rows[i][1].ToString() + "\");\n";
            }
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();        
        sb.Append("<script type='text/javascript'>\n");
        //sb.Append("var autocompletar;\n");
        //sb.Append("$(document).ready(function () {\n");
        sb.Append(autocompletar);
        //sb.Append("var autocompletar = new Array('Hola','Prueba');\n");
        //sb.Append("});\n");
        sb.Append("</script>");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString(), false);                    
        if (!IsPostBack)
        {
            csRefere.setCargar(this);

            try
            {
                if (rblFormasPago.SelectedIndex == -1)
                {
                    if (rblFormasPago.Items.FindByValue("TCPOL") != null)
                    {
                        rblFormasPago.Items.FindByValue("TCPOL").Selected = true;
                    }
                    else if (rblFormasPago.Items.FindByValue("EFE") != null)
                    {
                        rblFormasPago.Items.FindByValue("EFE").Selected = true;
                    }
                }
            }
            catch { }
            // JFPH
            csRefere.setCargarFranquiciasFOP(ddlFranquiciaPOL, rblFormasPago.SelectedValue);
            //if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("TC"))
            //{
            //    csRefere.setCargarFranquiciasFOP(ddlFranquicia, rblFormasPago.SelectedValue);                
            //}           
            //else if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("TCPOL"))
            //{
            //    csRefere.setCargarFranquiciasFOP(ddlFranquiciaPOL, rblFormasPago.SelectedValue);               
            //}
        }
    }    
    protected void btnReserva_Command(object sender, CommandEventArgs e)
    {
        ExceptionHandled.Publicar("....:::::::::::::::::::::::/******************* Se oprime el boton de reservar vuelos *******************/:::::::::::::::::::::::....");
        if (((Button)sender).CommandName.Equals("Confirmar"))
        {
            if (csRefere.bValidaFechas(this))
            {
                if (csRefere.bValidaListas(this))
                {
                    if (!Validacampos())
                    {
                        lblError.Text = "Por favor diligencia todos los campos marcados con (*)";
                        MPEEReserva.Hide();
                    }
                    else
                    {

                        if (!cbAcepto.Checked)
                        {
                            lblError.Text = "Por favor acepta los terminos y condicones";
                            MPEEReserva.Hide();
                            return;
                        }

                        clsParametros Registro = csRefere.setCrearNoRegistroVuelos(this, ucRegistro, Enum_Login.LoginGen, false);
                        if (Registro.Id != 0)
                        {
                            csRefere.setCommand(this, sender, e);
                            btnPagar.Visible = false;
                            btnGuardar.Visible = false;
                            btnPagar.Enabled = false;
                            btnGuardar.Enabled = false;
                            rblFormasPago.Enabled = false;
                            MPEEReserva.Hide();
                            MPERecord.Show();
                        }
                        else
                        {
                            MPEEReserva.Hide();
                            clsParametros objParametros = new clsParametros();
                            objParametros.Tipo = clsTipoError.Library;
                            objParametros.Severity = clsSeveridad.Moderada;
                            objParametros.Sugerencia.Add("No se pudo confirmar tu solicitud");
                            objParametros.ViewMessage.Add("Por favor intentalo nuevamente o comunicate con nosotros");
                            clsErrorMensaje objErrorMensaje = new clsErrorMensaje();
                            objErrorMensaje.getError(objParametros, dPanel);
                            Negocios_WebServiceSession._CerrarSesion();
                        }


                        MPEEReserva.Hide();

                    }

                }
                else
                {
                    MPEEReserva.Hide();
                }
            }
            else
            {
                MPEEReserva.Hide();
            }
    
        }
        else
        {
            csRefere.setCommand(this, sender, e);
            MPEEReserva.Hide();
            btnPagar.Visible = false;
            btnGuardar.Visible = false;
            btnPagar.Enabled = false;
            btnGuardar.Enabled = false;
            rblFormasPago.Enabled = false;
        }
    }
    private bool Validacampos()
    {
        bool validafecha = true;
       

        ExceptionHandled.Publicar("*************************////////////////////////////-- INICIO DE LA VALIDACION DE CAMPOS DE PASAJEROS & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");
        for (int c = 0; c < dtlPasajeros.Items.Count; c++)
        {
            TextBox txtNombre = (TextBox)dtlPasajeros.Items[c].FindControl("txtNombre1");
            TextBox txtApellido = (TextBox)dtlPasajeros.Items[c].FindControl("txtApellido1");
            TextBox txtPasaporte = (TextBox)dtlPasajeros.Items[c].FindControl("txtDocumento1");

            TextBox txtDireccion = (TextBox)ucRegistro.FindControl("txtDireccion");
            TextBox txtCiudad = (TextBox)ucRegistro.FindControl("txtCiudad");
            TextBox txtMail = (TextBox)ucRegistro.FindControl("txtMailPersonal");
            TextBox txtCelular = (TextBox)ucRegistro.FindControl("txtCelular");

           

            if (txtNombre.Text == "" || txtApellido.Text == "" || txtPasaporte.Text == "" || txtCelular.Text == string.Empty || txtCiudad.Text == string.Empty)
            {

                lblError.Text = "Por favor diligencia todos los campos marcados con (*) ";
                validafecha = false;
            }
        }
        ExceptionHandled.Publicar("*************************////////////////////////////-- FIN DE LA VALIDACION DE DATOS DE PASAJEROS & SESION: " + clsSesiones.getSesionID() + " --////////////////////////");

        return validafecha;
    }
   
    
    protected void hlkSabre_Click(object sender, EventArgs e)
    {
        csRefere.setValidaCondiciones(this);
    }
    protected void Page_PreRender(Object o, EventArgs e)
    {
        if (clsSesiones.GetDatasetSabreAir() == null) Response.Redirect("../Presentacion/index.aspx");
    }
    protected void rblFormasPago_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("EFE"))
        {
            DivTC.Style.Add("display", "none");
            DivTCPOL.Style.Add("display", "none");
            DivPSE.Style.Add("display", "none");
            DivEfe.Style.Add("display", "block");
            DivContAsesor.Style.Add("display", "none");
        }
        else if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("TC"))
        {
            // JFPH
            csRefere.setCargarFranquiciasFOP(ddlFranquicia, rblFormasPago.SelectedValue);
            DivTC.Style.Add("display", "block");
            DivTCPOL.Style.Add("display", "none");
            DivPSE.Style.Add("display", "none");
            DivEfe.Style.Add("display", "none");
            DivContAsesor.Style.Add("display", "none");
        }
        else if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("PSE"))
        {
            DivTC.Style.Add("display", "none");
            DivTCPOL.Style.Add("display", "none");
            DivPSE.Style.Add("display", "block");
            DivEfe.Style.Add("display", "none");
            DivContAsesor.Style.Add("display", "none");
        }
        else if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("NAP"))
        {
            DivTC.Style.Add("display", "none");
            DivTCPOL.Style.Add("display", "none");
            DivPSE.Style.Add("display", "none");
            DivEfe.Style.Add("display", "none");
            DivContAsesor.Style.Add("display", "block");
        }
        else if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("TCPOL"))
        {
            csRefere.setCargarFranquiciasFOP(ddlFranquiciaPOL, rblFormasPago.SelectedValue);
            DivTC.Style.Add("display", "none");
            DivTCPOL.Style.Add("display", "block");
            DivPSE.Style.Add("display", "none");
            DivEfe.Style.Add("display", "none");
            DivContAsesor.Style.Add("display", "none");
        }
    }
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        //if (lblUrlRedireccion.Text != "")
            //clsValidaciones.RedirectPagina(lblUrlRedireccion.Text);
        //else
            clsValidaciones.RedirectPagina("Index.aspx");
        
       //clsValidaciones.RedirectPagina(lblUrlRedireccion.Text);
    }
}


