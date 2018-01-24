using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SsoftQuery.Vuelos;
using Ssoft.Pages;
using System.Data;
using Ssoft.Utils;

public partial class uc_ucMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divMenu.InnerHtml = new csUtilitarios().SetMenu();
        }

    }
 


}