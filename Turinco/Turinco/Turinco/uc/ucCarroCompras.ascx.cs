using System;
using System.Data;
using System.Web.UI.WebControls;
using Ssoft.Rules.Reservas;
using System.Web.UI.HtmlControls;
using Ssoft.Pages;
using Ssoft.Utils;


public partial class uc_ucCarroCompras : System.Web.UI.UserControl
{
   
    csCarrito Carrito = new csCarrito();
    
   
    #region Atributos
    private const string USUARIOID = "usuarioId=";
    private const string REFVENTA = "refVenta=";
    private const string DESCRIPCION = "descripcion=";
    public const string VALOR = "valor=";
    public const string IVA = "iva=";
    private const string BASEDEVOLUCIONIVA = "baseDevolucionIva=";
    public const string EMAILCOMPRADOR = "emailComprador=";
    private const string LNG = "lng=";
    public const string MONEDA = "moneda=";
    public const string VALORADICIONAL = "valorAdicional=";
    private const string NOMBRECOMPRADOR = "nombreComprador=";
    private const string DOCUMENTOIDENTIFICACION = "documentoIdentificacion=";
    private const string TIPODOCUMENTOIDENTIFICACION = "tipoDocumentoIdentificacion=";
    private const string TELEFONOMOVIL = "telefonoMovil=";
    public const string EXTRA1 = "extra1=";
    public const string EXTRA2 = "extra2=";
    private const string URL_RESPUESTA = "url_respuesta=";
    private const string URL_CONFIRMACION = "url_confirmacion=";
    private const string PRUEBA = "prueba=";
    public const string AEROLINEA = "aerolinea=";
    public const string TARIFAADMINISTRATIVA = "tarifaAdministrativa=";
    public const string IVATARIFAADMINISTRATIVA = "ivaTarifaAdministrativa=";
    public const string BASEDEVOLUCIONTARIFAADMINISTRATIVA = "baseDevolucionTarifaAdministrativa=";
    public const string NOMBRE_USUARIO = "NombreUsuario=";
    public const string EXTRA3 = "extra3=";
    public const string EXTRA4 = "extra4=";
    public const string EXTRA5 = "extra5=";
    public const string PLANTILLA = "plantilla=";
    private const string FORMATO_NUMEROS = "#,##0.00";
    private const string CIUDAD_ENVIO = "ciudadEnvio=";
    private const string DIR_ENVIO = "direccionEnvio=";
    private bool CreditoDispersion = true;


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        {
            clsCache cCache = new csCache().cCache();
            Carrito.LimpiarCarrito();
            string ses = cCache.SessionID;
            DataSet ds = Carrito.GetDsReservas();
            csCarrito csCarCompUnion = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
            csCarCompUnion.EliminarItemDelCarrito("1");
            DataTable TablaPlanes = csCarCompUnion.RecuperarTabla();
            //Carrito.setCargar(this);
           // csRefere1.setLimpiarParametrosBusquedaPlanes(this);
           // csRefere.CargarSeccionInformativa(
           //                 this,
           //                 Ssoft.Utils.Enum_Tipo_Seccion_Publicacion.SP_INF_CARROCOMPRAS,
           //                 Ssoft.Utils.Enum_Tipo_Plantilla_Seccion.PlantillaUno,
           //                 Ssoft.Utils.Enum_Seccion_Informativa.NINGUNA,
           //                 "0",
           //                 null,
           //                 null,
           //                 null,
           //                 null,
           //                 null);
           //cRefere.setCargar(this, Enum_Login.LoginCarro);
        }
    }

    protected void rptCircuitos_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //Carrito.setCircuitos(this, source, e);
    }

    protected void lbtnCancelar_Click(object sender, EventArgs e)
    {
        //Carrito.setCancelar(this);
        Response.Redirect("index.aspx",false);
    }

    protected void lbtnAgregar_Click(object sender, EventArgs e)
    {
        //Carrito.serSeguirComprando(this);
    }

    protected void rblFormasPago_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    protected void btnPagar_Click(object sender, EventArgs e)
    {
      
    }

   
    protected void lbCrear_Click(object sender, EventArgs e)
    {
        if (txtClave.Text == "") 
        {
            txtClave.Text = "ssoft00";
        }
        if (txtclaveConfir.Text == "")
        {
            txtclaveConfir.Text = "ssoft00";
        }
        

    }
    protected void clcikBtnv(object sender, EventArgs e)
    {
       
    }

}


