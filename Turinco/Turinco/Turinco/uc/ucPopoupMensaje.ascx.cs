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

public partial class uc_ucPopupMensaje : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request["Msjpop"] != null)
            {
                //csCarroCompras Carrito = new csCarroCompras();
                //Carrito.setPopupMensaje(this, Request["Msjpop"].ToString());
            }
        }
        catch { }
    }
    protected void lnkIndex_Click(object sender, EventArgs e)
    {
        csGeneralsPag.Buscador();
    }
}
