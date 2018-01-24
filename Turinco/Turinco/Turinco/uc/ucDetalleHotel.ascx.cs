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
using Ssoft.ManejadorExcepciones;
using Ssoft.Pages;
using Ssoft.Utils;
using Ssoft.Pages.PaginaHoteles;
using Ssoft.Rules.Hoteles;

public partial class Uc_ucDetalleHotel : System.Web.UI.UserControl
{
    csBusquedaHoteles csRefere = new csBusquedaHoteles();
    protected void Page_Load(object sender, EventArgs e)
    {
        csRefere.setDetalle(this);
        clsControls.setSeleccionRadio(sender, "rbtSeleccion");
        if (HttpContext.Current.Session["Mmapa"] != null)
        {
            lblMapa.Text = HttpContext.Current.Session["Mmapa"].ToString();
        }

    }
   
    protected void setShorSell(object sender, EventArgs e)
    {
        csRefere.setShorSell(sender, e, this);    
    }
    protected void Facilidades_ItemDataBound(object sender, RepeaterItemEventArgs  e)
    {
        csRefere.Facilidades(sender, e);
    }
    protected void rbtSeleccion_CheckedChanged(object sender, EventArgs e)
    {
        clsControls.setSeleccionRadio(sender, "rbtSeleccion");
        if (HttpContext.Current.Session["Mmapa"] != null)
        {
            lblMapa.Text = HttpContext.Current.Session["Mmapa"].ToString();
        }
        DataSet dsData = clsSesiones.getResultadoHotel();
        if (dsData != null)
        {
            string ID_ = HttpContext.Current.Request["ID"];
            DataTable table = dsData.Tables["HotelInfo"];
            string filterExpression = "code='" + ID_ + "'";
            DataRow[] rowArray = table.Select(filterExpression);
            int length = rowArray.Length;
            if (length.Equals(0))
            {
                filterExpression = "code = " + ID_;
                rowArray = table.Select(filterExpression);
            }
            new csHoteles().setMapa(pMapa, rowArray[0]);
        }
    }
    
}
