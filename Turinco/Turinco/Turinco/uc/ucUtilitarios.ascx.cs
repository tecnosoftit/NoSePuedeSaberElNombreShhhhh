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
using Ssoft.Pages.PaginaPlanes;
using Ssoft.Pages.PaginaSeccionesInformativas;
using Ssoft.Utils;

public partial class uc_ucUtilitarios : System.Web.UI.UserControl
{
    string sCategoria = "0";
    string sTipologia = clsValidaciones.GetKeyOrAdd("CodTipologiaCrucero", "26");
    csSeccionesInformativas csSeccion = new csSeccionesInformativas();
    csResultadoPlanes csRefere = new csResultadoPlanes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csSeccion.CargarSeccionInformativa(
               this,
               Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_RECOMENDACIONES,
               Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
               "1",
               null,
               null,
               null,
               null,
               null,
               null,
               null);
        }
    }
    protected void imgUtilidades_Command(object sender, CommandEventArgs e)
    {
        clsSesiones.SET_COMMANDARGUMENT(e.CommandArgument.ToString());
        clsValidaciones.RedirectPagina("Documento.aspx");
    }

    protected void dtlPaginador_ItemCommand(object source, DataListCommandEventArgs e)
    {
        clsSesiones.setPage(e.Item.ItemIndex.ToString());
        csRefere.CargarPlanesCrucero(this, Enum_TipoDestino.Todos, true, null, e.Item.ItemIndex, clsValidaciones.GetKeyOrAdd("NumplanesHome", "6"), true, null, false, false, true, null, sCategoria, sTipologia);
    }


    protected void Button1_Command(object sender, CommandEventArgs e)
    {
        if (string.IsNullOrEmpty(Ssoft.Utils.clsSesiones.getPage()) && this.dtlPaginador.Items.Count > 0)
        {
            Ssoft.Utils.clsSesiones.setPage("0");
        }
        if (!string.IsNullOrEmpty(Ssoft.Utils.clsSesiones.getPage()) && this.dtlPaginador.Items.Count > 1)
        {
            int pagina = int.Parse(Ssoft.Utils.clsSesiones.getPage());
            if (e.CommandName.Equals("Next"))
                pagina += 1;
            if (e.CommandName.Equals("Back"))
            {
                pagina -= 1;
            }
            if (pagina < 0)
            {
                pagina = 0;
            }
            if (this.dtlPaginador.Items.Count > 1)
            {
                if (pagina > this.dtlPaginador.Items.Count - 1)
                {
                    pagina = Convert.ToInt32(this.dtlPaginador.Items.Count - 1);
                }
            }

            if (this.dtlPaginador.Items.Count > 1 &&
                (!(this.dtlPaginador.Items.Count < 0) && !(this.dtlPaginador.Items.Count < pagina)))
            {
                dtlPaginador_ItemCommand(dtlPaginador, new DataListCommandEventArgs(dtlPaginador.Items[pagina], sender, e));
            }
        }
    }

}

