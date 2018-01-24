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
using System.Text;
using System.Collections.Generic;
using Ssoft.Utils;
using Ssoft.Rules.Pagina;
using Ssoft.Rules.Generales;
using SsoftQuery.Vuelos;
using Ssoft.Rules.Hoteles;

public partial class Pagina : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["q"] != null && Request.QueryString["limit"] != null && Request.QueryString["TipoRefere"] != null)
                /*CONSULTA CIUDADES O REFERENCIAS DE LA TABLA REFERE*/
                ProcesarSolicitud(Request.QueryString["q"], Request.QueryString["limit"], Request.QueryString["TipoRefere"]);

            /*CIUDADES HOTELES DE HOTELBETS*/
            //ConsultaCiudadesHotelBets(Request.QueryString["q"]);
        }
    }

    private void ProcesarSolicitud(string strPalabra, string intLimite, string strTipoRefere)
    {
        if (strTipoRefere != "HOTELES")
        {
            StringBuilder strDatos = GenerarFiltro(strPalabra, intLimite);
            GenerarSalida(strDatos.ToString());
        }
        else
        {
            ConsultaCiudadesHotelBets(Request.QueryString["q"]);
        }
    }

    private void ConsultaCiudadesHotelBets(String strPalabra)
    {
        Planes objPlanes = new Planes();
        if (strPalabra != null)
        {
            csHoteles cHoteles = new csHoteles();
            string sIdioma = "CAS";
            DataTable dsCiudadesIATA = cHoteles.dsHotelbedsCity(sIdioma);
            StringBuilder strSalida = new StringBuilder();
            DataTable dtResultados = dsCiudadesIATA;
            DataView dtvistaFiltro = new DataView(dtResultados);
            dtvistaFiltro.RowFilter = "strValorAdic like '*" + strPalabra + "*' OR strDetalle like '*" + strPalabra + "*' OR strRefere like '*" + strPalabra + "*'";
            dtvistaFiltro.Sort = "strDetalle  ASC";
            dtResultados = dtvistaFiltro.ToTable();

            foreach (DataRow drfila in dtResultados.Rows)
            {
                strSalida.AppendLine(drfila["strRefere"].ToString() + "  " + drfila["strDetalle"].ToString() + "|" + drfila["strRefere"].ToString());
            }
            GenerarSalida(strSalida.ToString());
        }
    }

    private StringBuilder GenerarFiltro(string strPalabra, string intLimite)
    {
        string sIdioma = clsSesiones.getIdioma();

        if (sIdioma.Equals(""))
            sIdioma = clsValidaciones.GetKeyOrAdd("sIdioma", "es");

        DataTable tblCiudades = new DataTable();
        StringBuilder strDatos = new StringBuilder();
        StringBuilder stringArray = new StringBuilder();
        DataTable tblCiudNeworder = new DataTable();

        tblCiudades = new CsConsultasVuelos().Consultatabla("EXEC SPAEROPUERTOS '" + strPalabra.ToUpper() + "%' , '" + sIdioma + "'");


        int limite = Convert.ToInt32(intLimite);

        if (tblCiudades != null)
        {
            List<clsAero> tList = new List<clsAero>();
            clsAero Temp;
            if (limite <= tblCiudades.Rows.Count)
            {
                for (int c = 0; c < limite; c++)
                {

                    if (tList.Exists(x => x.strValorAdic == (tblCiudades.Rows[c]["strdescription"].ToString())))
                    {
                        if (tblCiudades.Rows[c]["Iata"].ToString() != string.Empty)
                        {
                            strDatos.AppendLine("&nbsp;&nbsp;" + tblCiudades.Rows[c]["Iata"].ToString());
                        }
                    }
                    else
                    {
                        Temp = new clsAero(tblCiudades.Rows[c]["strCode"].ToString(), tblCiudades.Rows[c]["strdescription"].ToString(), tblCiudades.Rows[c]["strCountry"].ToString(), tblCiudades.Rows[c]["strAirport"].ToString());
                        tList.Add(Temp);
                        //<img  src='http://www.tiquetesyviajes.com/pagina/App_Themes/Imagenes/home_icon.png' />
                        if (Temp.strValorAdic != string.Empty && Temp.siata != string.Empty)
                        {
                            strDatos.AppendLine("" + Temp.strValorAdic + "<BR>");
                            strDatos.AppendLine("&nbsp;&nbsp;" + Temp.strRefere + " " + Temp.siata);
                        }
                    }

                }
            }
            else
            {
                for (int c = 0; c < tblCiudades.Rows.Count; c++)
                {
                    if (tList.Exists(x => x.strValorAdic == (tblCiudades.Rows[c]["strdescription"].ToString())))
                    {

                        strDatos.AppendLine("&nbsp;&nbsp;" + tblCiudades.Rows[c]["Iata"].ToString());
                    }
                    else
                    {
                        //<img  src='http://www.tiquetesyviajes.com/pagina/App_Themes/Imagenes/home_icon.png'/>
                        Temp = new clsAero(tblCiudades.Rows[c]["strCode"].ToString(), tblCiudades.Rows[c]["strdescription"].ToString(), tblCiudades.Rows[c]["strCountry"].ToString(), tblCiudades.Rows[c]["strAirport"].ToString());
                        tList.Add(Temp);

                        if (Temp.strValorAdic != string.Empty && Temp.siata != string.Empty)
                        {
                            if (c == 0)
                            {
                                strDatos.AppendLine("" + Temp.strDetalle + "<BR>");
                            }
                            strDatos.AppendLine("&nbsp;&nbsp;" + Temp.strValorAdic + " " + Temp.strDetalle + " (" + Temp.siata + ")");
                        }
                    }
                }
            }
        }
        else
        {
            strDatos.AppendLine("&nbsp;&nbsp;" + "No se encontraron ciudades que coincidan con:" + strPalabra);
        }
        return strDatos;
    }

    private DataTable Consultar(string strTipoRefere)
    {
        //tblRefere otblRefere = new tblRefere();
        //otblRefere.Conexion = clsValidaciones.GetKeyOrAdd("strConexion", "");
        DataTable tblCiudades = default(DataTable);
        try
        {
            string sAplicacion = clsSesiones.getAplicacion().ToString();
            string sIdioma = clsSesiones.getIdioma();
            if (Session["tblDatos"] == null && Session["strTipoRefere"] == null)
            {
                //tblCiudades = otblRefere.GetTipoRefereIata(strTipoRefere, sIdioma, sAplicacion).Tables[0];
                //Session.Add("tblDatos", tblCiudades);
                //Session.Add("strTipoRefere", strTipoRefere);
            }
            else
            {
                tblCiudades = (DataTable)Session["tblDatos"];
                if (tblCiudades.Rows.Count == 0)
                {
                    //tblCiudades = otblRefere.GetTipoRefereIata(strTipoRefere, sIdioma, sAplicacion).Tables[0];
                    //Session["tblDatos"] = tblCiudades;
                }
                if (!strTipoRefere.Equals(Session["strTipoRefere"].ToString()))
                {
                    //tblCiudades = otblRefere.GetTipoRefereIata(strTipoRefere, sIdioma, sAplicacion).Tables[0];
                    //Session["strTipoRefere"] = strTipoRefere;
                    //Session["tblDatos"] = tblCiudades;
                }
            }
        }
        catch { }
        return tblCiudades;
    }

    private void GenerarSalida(string strResultado)
    {
        Response.Expires = -1;
        Response.Clear();
        Response.ContentType = "text/plain";
        Response.Write(strResultado);
        Response.End();
    }





    public class clsAero
    {



        public string strValorAdic { get; set; }// ciudad
        public string strDetalle { get; set; }//Aeropuerto
        public string strRefere { get; set; }//codigo ciudad
        public string siata { get; set; }//descripcion total

        public clsAero(string ValorAdic, string sDetalle, string Refere, string sIata)
        {
            strValorAdic = ValorAdic;
            strDetalle = sDetalle;
            strRefere = Refere;
            siata = sIata;
        }

    }
}
