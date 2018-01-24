using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaSeccionesInformativas;
using Ssoft.Utils;
using SsoftQuery.Vuelos;
using SsoftQuery.Planes;
using Ssoft.Pages.PaginaBanners;
using System.Data;
public partial class uc_ucDestinosRecomendados : System.Web.UI.UserControl
{
    csSeccionesInformativas cRefere2 = new csSeccionesInformativas();
    csBanners csRefere = new csBanners();
    private string codigo;

    public string Codigo
    {
        get
        {
            if (Request.QueryString["CODSEC"] != null)
            {
                return Request.QueryString["CODSEC"];
            }
            return codigo;
        }
        set { codigo = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csRefere.setLlenarBanners(this, 1);
            cRefere2.Load_Destinos(this);
          
            if (ddlDestinos.Items.Count > 0 && HttpContext.Current.Request.QueryString["CODSEC"] == null && HttpContext.Current.Request.QueryString["ICiudad"] == null)
            {
                string[] sCode = ddlDestinos.Items[0].Value.ToString().Split('|');

                if (sCode.Length > 1)
                {
                    DataTable dt = new csConsultasPlanes().ConsultarPaisesCiudad("ES", sCode[1]);

                    if (dt != null)
                    {
                        sCode[1] = dt.Rows[0]["intCodigoPais"].ToString();
                    }

                    clsValidaciones.RedirectPagina("../Presentacion/DestinosRecomendados.aspx?CODSEC=" + sCode[0] + "&PaisDestino=" + sCode[1]);
                }

            }
            cRefere2.CargarSeccionInformativa(
                          this,
                          Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_DESTINOS_DESTACADOS,
                          Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
                          "0",
                          null,
                          null,
                          null,
                          Codigo,
                          null,
                          null,
                          null);
            
        }
    }
    protected void ddlDestinos_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] sCode = ddlDestinos.SelectedValue.ToString().Split('|');

        if (sCode.Length > 1)
        {
            DataTable dt = new csConsultasPlanes().ConsultarPaisesCiudad("ES", sCode[1]);

            if (dt != null)
            {
                sCode[1] = dt.Rows[0]["intCodigoPais"].ToString();
            }

            clsValidaciones.RedirectPagina("../Presentacion/DestinosRecomendados.aspx?CODSEC=" + sCode[0] + "&PaisDestino=" + sCode[1]);
        }
        else if (sCode.Length > 0)
        {
            clsValidaciones.RedirectPagina("../Presentacion/DestinosRecomendados.aspx?CODSEC=" + sCode[0]);
        }
        else
        {
            clsValidaciones.RedirectPagina("../Presentacion/DestinosRecomendados.aspx");
        }
        //    clsValidaciones.RedirectPagina("../Presentacion/DetalleDestino.aspx?CODSEC=" + ddlDestinos.SelectedValue + "&IdDestino=" + ddlDestinos.SelectedValue + " &IdPais=" + pais);
        //else
        //    clsValidaciones.RedirectPagina("../Presentacion/DetalleDestino.aspx?CODSEC=" + ddlDestinos.SelectedValue + "&IdDestino=" + ddlDestinos.SelectedValue);

    }
    public void SaveProfile(object sender, EventArgs e)
    {
    }
}