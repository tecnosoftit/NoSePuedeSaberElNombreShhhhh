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

public partial class Presentacion_ConfirmacionVuelo : System.Web.UI.Page
{
     protected void Page_Load(object sender, EventArgs e)
    {
        csResultadoVuelos csRefere = new csResultadoVuelos();
        if (!IsPostBack)
        {
            Ssoft.Pages.csGeneralsPag.MetaTag(this);
        }
        csRefere.SetOcultarbanner(this);

    }
    
}
