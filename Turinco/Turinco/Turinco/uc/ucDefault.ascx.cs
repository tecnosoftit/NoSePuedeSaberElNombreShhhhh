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

public partial class uc_ucDefault : System.Web.UI.UserControl
{
    //csLogin cRefere = new csLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //cRefere.setCargar(this, Enum_Login.LoginDefault);
        }
    }
    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        //cRefere.setEntrar(this, Enum_Login.LoginCorp);
    }
    protected void lbtnRecordarContrasena_Click(object sender, EventArgs e)
    {
        //cRefere.setOlvido(this);
    }
}
