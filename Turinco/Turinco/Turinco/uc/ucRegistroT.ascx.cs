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

public partial class uc_ucRegistro : System.Web.UI.UserControl
{

    csResultadoVuelos csRefere = new csResultadoVuelos();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void chkCondicionesRegistro_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCondicionesRegistro.Checked)
            btncrear.Enabled = true;
        else
            btncrear.Enabled = false;

        ModalPopupExtender1.Show();

    }

    protected void lbCrear_Click(object sender, EventArgs e)
    {
        csRefere.SetCrearEmpresa(txtNombre.Text, txtDocumento.Text, txtDireccion.Text, txtCiudad.Text, txtTelefono.Text, txtPersonaContacto.Text, txtCargo.Text, txtMailPersonal.Text, this);
        txtNombre.Text = "";
        txtDocumento.Text = "";
        txtMailPersonal.Text = "";
        txtCargo.Text = "";
        txtPersonaContacto.Text = "";
        txtTelefono.Text = "";
        txtDireccion.Text = "";
        txtCiudad.Text = "";
        chkCondicionesRegistro.Checked = false;

    }

}
