using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages;

public partial class uc_ucFiltrosdeVuelos : System.Web.UI.UserControl
{
    csResultadoVuelos CsVuelos = new csResultadoVuelos();
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void OnCheckedChangedMayor(object sender, EventArgs e)
    {
        string sQuery = "1=1";
        CheckBoxList chkList = ((CheckBoxList)sender);

        if (chkList.Items.Count > 0 && (!chkList.Items[0].Selected))
        {
            foreach (ListItem x in ((CheckBoxList)sender).Items)
            {

                if ((x.Selected) && (!x.Value.Equals("0")))
                {
                    if (sQuery.Equals("1=1"))
                    { sQuery = " strMarketingAirline='" + x.Value.ToString() + "'"; }
                    else sQuery += " OR strMarketingAirline='" + x.Value.ToString() + "'";
                }

            }
        }
        else
        {
            foreach (ListItem x in ((CheckBoxList)sender).Items)
            {
                x.Selected = false;
                sQuery = "1=1";
            }

        }

        CsVuelos.setOtrosFiltrosAerolineaBFM(this.Parent, sQuery, this);

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {

        CheckBoxList chk = (CheckBoxList)this.Page.FindControl("ucFiltros").FindControl("chkaerolinea"); 
        string sQuery = "0";
        if (this.txtFrom.Text != string.Empty && this.txtTo.Text != string.Empty)
        {
            sQuery = "intTotalPesos  >= " + this.txtFrom.Text + " AND  " + this.txtTo.Text + " >= intTotalPesos";


        }
        if (this.btnSearch.Text.Equals("Reset"))
        {
            sQuery = "0";
            this.btnSearch.Text = "Buscar";
        }
        else
        {
            this.btnSearch.Text = "Reset"; 
        }



        CsVuelos.setOtrosFiltrosAerolineaBFM(this.Parent, sQuery, this);
    
    }
}