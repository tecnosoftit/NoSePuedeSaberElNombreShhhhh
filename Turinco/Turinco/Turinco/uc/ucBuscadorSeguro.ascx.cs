using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaPlanes;
using Ssoft.Utils;
using System.Data;

public partial class uc_ucBuscadorSeguro : System.Web.UI.UserControl
{
    csBuscadorPlanes csRefere = new csBuscadorPlanes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csRefere.setPlan(this);
            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {
                csRefere.setPaisesZona(this,cCache.Empresa, "CIRC");                
            }

            if (rptEdadPax.Items.Count == 0)
            {
                DataTable dt = new DataTable();
                DataColumn dcEdades = new DataColumn("intEdaPax");
                DataColumn dcPax = new DataColumn("strPax");
                dt.Columns.Add(dcEdades);
                dt.Columns.Add(dcPax);
                DataRow drEdadPax = dt.NewRow();
                dt.Rows.Add(drEdadPax);
                dt.Rows[0]["strPax"] = "Fecha nacimiento viajero " + Convert.ToString(0 + 1);
                dt.AcceptChanges();
                rptEdadPax.DataSource = dt;
                rptEdadPax.DataBind();

            }


        }
    }
    protected void setCommand(object sender, CommandEventArgs e)
    {
        csRefere.GuardarParametrosBusquedaSeguros(this);
    }

    protected void ddlCantidadPax_Selected(object sender, EventArgs e)
    {
        csRefere.setMostrar(this, false);
    }

}