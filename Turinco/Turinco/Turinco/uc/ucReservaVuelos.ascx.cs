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
using Ssoft.Rules.Generales;

public partial class uc_ucReservaVuelos : System.Web.UI.UserControl
{
  
    csResultadoVuelos csRefere = new csResultadoVuelos();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csRefere.setCargar(this);
            csRefere.LlenarCombosReservaVuelos(this);
        }
    }
    protected void btnReserva_Command(object sender, CommandEventArgs e)
    {
        ExceptionHandled.Publicar("....:::::::::::::::::::::::/******************* Se oprime el boton de reservar vuelos *******************/:::::::::::::::::::::::....");
        if (((Button)sender).CommandName.Equals("Confirmar"))
        {

            if (!Validacampos()) return;
            
            if (!((CheckBox)rptItinerario.Controls[rptItinerario.Controls.Count-1].FindControl("cbAcepto")).Checked)
            {
                lblError.Text = "Por favor acepte los terminos y condicones";
                return;
            }

            clsParametros Registro = csRefere.setCrearNoRegistroVuelos(this, ucRegistro, Enum_Login.LoginGen, false);
            if (Registro.Id != 0)
                csRefere.setCommand(this, sender, e);
          
        }
        else
        {
            csRefere.setCommand(this, sender, e);
        }
    }
    private bool Validacampos()
    {
        bool validafecha = true;

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

                lblError.Text = "Por favor Diligencie todos los campos marcados con (*) ";
                return false;
            }
        }

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


}
