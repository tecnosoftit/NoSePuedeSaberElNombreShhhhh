using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaBanners;

public partial class uc_ucSecInfoVuelos : System.Web.UI.UserControl
{
    csBanners csRefere = new csBanners();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //csRefere.setLlenarBanners(this, 2);
        }
    }
}