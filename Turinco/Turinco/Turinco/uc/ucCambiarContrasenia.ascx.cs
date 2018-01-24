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
using Ssoft.Pages.PaginaMiCuenta;

public partial class uc_ucCambiarContrasenia : System.Web.UI.UserControl
{
   protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clsCache cCache = new csCache().cCache();
            txtEmail.Text = cCache.Email;
        }
    }

    protected void setCommand(object sender, CommandEventArgs e)
    {
        if (txtConfContrasena.Text.Equals(txtContrasena.Text))
        {
            csDatosUsuario Usuario = new csDatosUsuario();
            Usuario.cambiar_contrasenia(txtContrasena.Text, lblError);
        }
        else
        {
            lblError.Text = "Los campos de contraseña y confirmación no coinciden, por favor revisa la información";
        }
    }
}
