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

public partial class uc_ucResultadoVuelosHora : System.Web.UI.UserControl
{
    csResultadoVuelos cRefere = new csResultadoVuelos();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cRefere.setCargarResult(this);
        }
    }
    protected void rptItinerario_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        cRefere.setItinerarioIda(this, source, e);
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        cRefere.setOrdenarHoras(this, sender, e);
    }
}
