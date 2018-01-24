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
using System.Collections.Generic;
using WS_SsoftSabre.OTA_AirLowFareSearch;
using Ssoft.Utils;
using System.Text;
using Ssoft.Pages;
using Ssoft.Pages.Reserva;
using Ssoft.Pages.PaginaMiCuenta;
using Ssoft.Rules.Generales;

public partial class uc_ucDatosUsuario : System.Web.UI.UserControl
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csDatosUsuario Usuarios = new csDatosUsuario();
            clsCache cCache = new csCache().cCache();
            Usuarios.consulta_usuario(rptUsuario, cCache.Contacto);
            //CargarAfiliadosPax();
        }
    }

    protected void ConsultaCiudadesPais(object sender, EventArgs e)
    {
        csDatosUsuario Usuarios = new csDatosUsuario();
        Usuarios.ciudades_pais(rptUsuario);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        csDatosUsuario Usuarios = new csDatosUsuario();
        Usuarios.actualizar_usuario(rptUsuario, lblError);
    }

    //protected void lbCrear_Click(object sender, EventArgs e)
    //{
    //    int contactoPadre = Convert.ToInt32(new csCache().cCache().Contacto);
    //    bool bValida = new csResultadoVuelos().SetCrearAfiliados(this, contactoPadre);

    //    DataTable dtLista = new csResultadoVuelos().SetBuscarUsuarios(new csCache().cCache().Contacto, new csCache().cCache().Empresa);
    //    int Afiliados = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumAfiliados", "6"));

    //    new csGenerales().sConsultaGeneros(ddlGeneroR, false);
    //    new csGenerales().sConsultaTposIdentificacion(ddlTpoDocumentoR, false);

    //    if (dtLista.Rows.Count >= Afiliados)
    //    {
    //        btnAgregarAfiliado.Enabled = false;
    //        lbltexto.Text = "No es posible registrar mas usuarios con tu suscripción.";
    //    }
    //    else if (dtLista.Rows.Count == 1)
    //    {
    //        lbltexto.Text = clsValidaciones.GetKeyOrAdd("textoSinAfiliados", "Usted no tiene usuarios afiliados. Haga click en el botón Registrar Nuevo para crearlos.");
    //    }

    //    if (dtLista != null)
    //    {
    //        rptpasajeros.DataSource = dtLista;
    //        rptpasajeros.DataBind();
    //        HttpContext.Current.Session["dtLista"] = dtLista;
    //    }
    //    MPAfiliados.Hide();
    //    if (bValida)
    //    {
    //        lbltexto.Text = "Su afiliado fue creado Exitosamente";
    //    }
    //    else
    //    {
    //        lbltexto.Text = "Lo sentimos Por Favor Vuelva a crear su afiliado ";
    //    }
    //}

    //protected void CargarAfiliadosPax()
    //{
    //    DataTable dtLista = new csResultadoVuelos().SetBuscarUsuarios(new csCache().cCache().Contacto, new csCache().cCache().Empresa);
    //    int Afiliados = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumAfiliados", "6"));

    //    new csGenerales().sConsultaGeneros(ddlGeneroR, false);
    //    new csGenerales().sConsultaTposIdentificacion(ddlTpoDocumentoR, false);
    //    if (dtLista.Rows.Count >= Afiliados)
    //    {
    //        btnAgregarAfiliado.Enabled = false;
    //        lbltexto.Text = "No es posible registrar mas usuarios con tu suscripción.";
    //    }

    //    if (dtLista.Rows.Count == 1)
    //    {
    //        lbltexto.Text = clsValidaciones.GetKeyOrAdd("textoSinAfiliados", "Aun no haz regístrado tus elegidos, haz Click en el botón Registrar Nuevo para Registrarlos.");
    //    }
    //    else if (dtLista.Rows.Count < Afiliados)
    //    {
    //        int elegidos = Afiliados - dtLista.Rows.Count;
    //        if (elegidos == 1)
    //            lbltexto.Text = "Aún puedes registrar un (1) elegido más.";
    //        else
    //            lbltexto.Text = "Aún puedes registrar  (" + elegidos.ToString() + ") elegidos más.";
    //    }

    //    if (dtLista != null)
    //    {
    //        rptpasajeros.DataSource = dtLista;
    //        rptpasajeros.DataBind();
    //        HttpContext.Current.Session["dtLista"] = dtLista;
    //    }

    //    //MPAfiliados.Show();
    //}
}
