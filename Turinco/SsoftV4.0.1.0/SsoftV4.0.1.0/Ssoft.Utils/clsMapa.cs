using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.ManejadorExcepciones;
namespace Ssoft.Utils
{
    public class clsMapa
    {
        private string strUrl = string.Empty;
        private string strLongitud = string.Empty;
        private string strLatitud = string.Empty;
        private string strEncabezado = string.Empty;
        private string strWidth = string.Empty;
        private string strHeight = string.Empty;
        private string strDireccion = string.Empty;
        private Panel pMapa = null;

        public string Url
        {
            get { return strUrl; }
            set { strUrl = value; }
        }

        public string Longitud
        {
            get { return strLongitud; }
            set { strLongitud = value; }
        }

        public string Latitud
        {
            get { return strLatitud; }
            set { strLatitud = value; }
        }

        public string Encabezado
        {
            get { return strEncabezado; }
            set { strEncabezado = value; }
        }
        public string Width
        {
            get { return strWidth; }
            set { strWidth = value; }
        }

        public string Height
        {
            get { return strHeight; }
            set { strHeight = value; }
        }

        public string Direccion
        {
            get { return strDireccion; }
            set { strDireccion = value; }
        }

        public Panel Mapa
        {
            get { return pMapa; }
            set { pMapa = value; }
        }

        public clsMapa()
        {
            this.Url = clsValidaciones.RutaSsoftMapa();
        }

        public clsMapa(string sUrl)
        {
            this.Url = sUrl;
        }

        private string Valida(string sValor, Enum_Mapa eMapa)
        {
            string sRespuesta = sValor;
            switch (eMapa)
            {
                case Enum_Mapa.Ubicacion:
                    try
                    {
                        string[] sLista = null;
                        if (sValor.Length != 0)
                        {
                            Utils cUtil = new Utils();
                            sLista = clsValidaciones.Lista(sValor, "|");
                            if (sLista[0].Length.Equals(0))
                            {
                                if (sLista[2].Length.Equals(0))
                                {
                                    sRespuesta = "COLOMBIA";
                                }
                                else
                                {
                                    sRespuesta = sLista[2].ToString();
                                }
                            }
                            else
                            {
                                if (sLista[1].Length.Equals(0))
                                {
                                    if (sLista[2].Length.Equals(0))
                                    {
                                        sRespuesta = "COLOMBIA";
                                    }
                                    else
                                    {
                                        sRespuesta = sLista[2].ToString();
                                    }
                                }
                                else
                                {
                                    sRespuesta = sLista[0].ToString() + " " + sLista[1].ToString();
                                }
                            }
                        }
                        else
                        {
                            sRespuesta = "COLOMBIA";
                        }
                    }
                    catch
                    {
                        sRespuesta = "COLOMBIA";
                    }
                    break;

                case Enum_Mapa.Posicion:
                    try
                    {
                        if (sValor.Length == 0)
                        {
                            sRespuesta = "500";
                        }
                    }
                    catch
                    {
                        sRespuesta = "500";
                    }
                    break;

                case Enum_Mapa.Url:
                    try
                    {
                        if (sValor.Length == 0)
                        {
                            sRespuesta = "http://66.165.133.50/Mapa/Index.aspx";
                        }
                    }
                    catch
                    {
                        sRespuesta = "http://66.165.133.50/Mapa/Index.aspx";
                    }
                    break;
            }
            return sRespuesta;
        }

        private string Reduce(string sValor, int iPos)
        {
            string sValorNew = sValor;
            try
            {
                if (sValor.Length > 0)
                {
                    int iWidth = int.Parse(sValor);
                    iWidth = iWidth - 10;
                    sValorNew = iWidth.ToString();
                }
            }
            catch { }
            return sValorNew;
        }

        public void setMapa()
        {
            this.Width = Valida(this.Width, Enum_Mapa.Posicion);
            this.Height = Valida(this.Height, Enum_Mapa.Posicion);

            string WidthMapa = Reduce(this.Width, 10);
            string HeightMapa = Reduce(this.Height, 10);
            string sUbica = this.Latitud + "|" + this.Longitud + "|" + this.Direccion;
            string Ubicacion = Valida(sUbica, Enum_Mapa.Ubicacion);

            try
            {
                string sLink = this.Url + "?coord=" + Ubicacion + "&text=" + this.Encabezado + "&W=" + WidthMapa + "&H=" + HeightMapa;
                Table tMapa = new Table();

                TableRow trMapa = new TableRow();
                TableCell tcMapa = new TableCell();
                Label lMapa = new Label();

                tMapa.Width = new Unit(100, UnitType.Percentage);
                //lMapa.Text = "<iframe id='iframe' src='" + sLink + "' frameborder='0' width='" + this.Width + "' scrolling='no' height='" + this.Height + "' marginheight='0' marginwidth='0'></iframe>";
                lMapa.Text = "<iframe id='iframe' src='" + "https://maps.google.com/maps?text=eee;hl=es;geocode=&amp;q=" + this.Encabezado + "&amp;sll=" + this.Latitud + "," + this.Longitud + "&amp;ie=UTF8&amp;tex&;z=0&amp;output=embed" + "' frameborder='0' width='" + this.Width + "' scrolling='no' height='" + this.Height + "' marginheight='0' marginwidth='0'></iframe>";
                tcMapa.Controls.Add(lMapa);
                trMapa.Controls.Add(tcMapa);
                tMapa.Controls.Add(trMapa);

                Mapa.Controls.Clear();

                Mapa.Controls.Add(tMapa);
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
    }
}
