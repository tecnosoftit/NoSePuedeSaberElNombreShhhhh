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
using Ssoft.Pages.PaginaBanners;

public partial class uc_ucBannersInfPlanes : System.Web.UI.UserControl
{
    csBanners csRefere = new csBanners();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csRefere.setLlenarBanners(this, 2);
        }
    }

}
