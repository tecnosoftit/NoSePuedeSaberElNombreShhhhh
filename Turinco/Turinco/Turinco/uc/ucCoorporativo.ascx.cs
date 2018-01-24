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
using Ssoft.Pages.PaginaSeccionesInformativas;
using Ssoft.Utils;


public partial class uc_ucCoorporativo : System.Web.UI.UserControl
{
    csSeccionesInformativas csSeccion = new csSeccionesInformativas();
    protected void Page_Load(object sender, EventArgs e)
    {
        csSeccion.CargarSeccionInformativa(
                this,
                Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_BENEFICIOS_CORPORATIVOS,
                Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
                "0",
                null,
                null,
                null,
                null,
                null,
                null,
                null);
    }

    protected void dtlCorporativo_ItemCommand1(object source, DataListCommandEventArgs e)
    {
        //csSeccion.dtlCorporativo_ItemCommand1(source, e, this);
    }
    protected void btnContactenos_Click(object sender, EventArgs e)
    {
        //clsCache cCache = new csCache().cCache();
        //Response.Redirect("../Presentacion/Contactenos.aspx?idSesion=" + cCache.SessionID);
    }
}
