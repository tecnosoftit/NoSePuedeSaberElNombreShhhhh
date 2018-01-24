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
using Ssoft.Rules.Reservas;
using System.Collections.Generic;

public partial class uc_ucBanner : System.Web.UI.UserControl
{
    csMenu cRefere = new csMenu();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cRefere.setBanner(this);
            //Label lbltelefono = (Label)this.lbltelefono;
            Label lbltelefonoag = new Label();
            lbltelefonoag = (Label)this.FindControl("lbltelefono");
            if (lbltelefonoag != null)
            {
                lbltelefonoag.Text = clsValidaciones.GetKeyOrAdd("telefonoag", "563-4-3836682");
            }
            Label lblcorreoag = new Label();
            lblcorreoag = (Label)this.FindControl("lblcorreo");
            if (lblcorreoag != null)
            {
                lblcorreoag.Text = clsValidaciones.GetKeyOrAdd("Correoag", "ventas@nactur.com");
            }

        }

    }
    protected void btn_Command1(object source, CommandEventArgs e)
    {
        //cRefere1.setCommand(this, source, e, Enum_Login.LoginDefault);
    }
    protected void btn_Command(object source, CommandEventArgs e)
    {
        //cRefere.setCommand(this, source, e);
    }
    protected void Unnamed1_Command(object sender, CommandEventArgs e)
    {
        clsSesiones.setPantalleRespuestaLogin("../Presentacion/MiCuenta.aspx");
        clsValidaciones.RedirectPagina("../Presentacion/Login.aspx");
    }

    public void setCommand(object source, CommandEventArgs e)
    {
        cRefere.setCommand(this, source, e);
    }
}
