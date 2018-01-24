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

public partial class uc_ucMenuInferior : System.Web.UI.UserControl
{
    //csSeccionesInformativas cRefere = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //cRefere.Load_Hoteles_Menu_Inferior(this);
        }
    }
}
