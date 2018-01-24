using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ssoft.Pages.PaginaHoteles;
using System.IO;
using Ssoft.Pages;
using Ssoft.Utils;


public partial class uc_ucResultadoHotel : System.Web.UI.UserControl
{
    csBusquedaHoteles csRefere = new csBusquedaHoteles();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpContext.Current.Session["$Estrellas"] = null;
            HttpContext.Current.Session["$Busqueda"] = null;
            csRefere.setCargar(this);

        }
    }
    protected void setShorSell(object sender, EventArgs e)
    {
        csRefere.setShorSell(this);
    }
    protected void dlPagina_ItemCommand(object source, DataListCommandEventArgs e)
    {
        csRefere.setPaginar(this, int.Parse(e.CommandArgument.ToString()));
    }
    protected void chkCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkCategoria.SelectedValue.ToString() != "0")
        {
            HttpContext.Current.Session["$Estrellas"] = chkCategoria.SelectedValue.ToString();
        }
        else
        {
            HttpContext.Current.Session["$Estrellas"] = null;
        }

        csRefere.setFiltro(this, 1);
    }
    protected void Search_Command(object sender, CommandEventArgs e)
    {
        csRefere.setCommand(this, sender, e);
    }

    protected void ddlZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        csRefere.setFiltroZona(this);
    }

    protected void rbtSeleccion_CheckedChanged(object sender, EventArgs e)
    {
        clsControls.setSeleccionRadio(sender, "rbtSeleccion");
    }
    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        if (txtnombrehotel.Text != "")
        {
            csRefere.setFiltro(this, txtnombrehotel.Text.TrimStart().TrimEnd() + "|");
            //HttpContext.Current.Session["$Busqueda"] = null;
            //txtnombrehotel.Text = "";
        }

    }

    protected void btnBorrarFiltro_Click(object sender, EventArgs e)
    {
        txtnombrehotel.Text = "";
        HttpContext.Current.Session["$Busqueda"] = null;
        csRefere.setFiltro(this, txtnombrehotel.Text);


    }

    protected void RptPagina_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        csRefere.setFiltro(this, int.Parse(e.CommandArgument.ToString()));
    }
}