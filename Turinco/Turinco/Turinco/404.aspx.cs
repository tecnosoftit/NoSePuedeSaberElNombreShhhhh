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

public partial class _404 : System.Web.UI.Page
{
    /// <summary>
    /// Evento Load de la página 404.aspx
    /// </summary>
    /// <param name="sender">Objeto tipo Contexto.</param>
    /// <param name="e">Argumentos que son enviados con la página.</param>
    private void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            csGeneralsPag.PageError();
        }
    }
}
