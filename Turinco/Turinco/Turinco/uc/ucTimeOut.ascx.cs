using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Utils;

public partial class uc_ucTimeOut : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTextoTimeOut.Text = clsValidaciones.GetKeyOrAdd("TextoTimeOut", "Lo sentimos, su sesión ha expirado, para continuar navegando debe regresar al Inicio.");
    }
}