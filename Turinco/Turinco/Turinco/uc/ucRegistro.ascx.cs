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
using Ssoft.Pages;
using Ssoft.Utils;
using SsoftQuery.Vuelos;

public partial class uc_ucRegistro : System.Web.UI.UserControl
{

    //csLogin cRefere = new csLogin();
    //csSeccionesInformativas secciones = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //secciones.CargarSeccionInformativa(
            //            this,
            //            Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_CONDICIONES_REGISTRO,
            //            Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaRegistro,
            //            Ssoft.Utils.Enum_Seccion_Informativa.CONDICIONES_REGISTRO,
            //            null,
            //            null,
            //            null,
            //            null,
            //            null,
            //            null
            //            );

            //try { cRefere.setCargar(this, Enum_Login.LoginGen, "8242"); }//8242 = COLOMBIA
            //catch { cRefere.setCargar(this, Enum_Login.LoginGen); }
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
    }

    protected void chkCondicionesRegistro_CheckedChanged(object sender, EventArgs e)
    {
        //cRefere.setAceptarCondiciones(this);
    }

    protected void lbCrear_Click(object sender, EventArgs e)
    {
        //cRefere.setCrear(this, Enum_Login.LoginGen);
    }

    protected void txtMailPersonal_TextChanged(object sender, EventArgs e)
    {
        //cRefere.setConsultaUsuarioReserva(((UserControl)this.Parent), this, ((TextBox)sender).Text);
    }
}
