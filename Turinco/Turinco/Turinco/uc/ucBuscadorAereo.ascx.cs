using System;
using System.Data;
using System.Web.UI.WebControls;
using Ssoft.Pages;
using Ssoft.Rules.Generales;
using Ssoft.Utils;

public partial class uc_ucBuscadorAereo : System.Web.UI.UserControl
{
    csBuscador csRefere = new csBuscador();
    csGenerales csRefere1 = new csGenerales();
    DataTable dtDatos = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csRefere.setCargarAereo(this);

        }
    }

    protected void setCommand(object sender, CommandEventArgs e)
    {
        csRefere.setCommand(this, sender, e);
    }  

}
