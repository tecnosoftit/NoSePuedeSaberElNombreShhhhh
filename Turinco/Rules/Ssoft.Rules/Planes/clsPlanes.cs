using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ssoft.Utils;
//using Ssoft.SsoftQuery;
using Ssoft.Rules;
using Ssoft.Rules.Administrador;

namespace Ssoft.Rules.planes
{
    public class clsPlanes
    {
        #region[E-Explora]
        /*METODO QUER LLENA EL USERCONTROL DE OFERTAS DE PAQUETES UBICADO EN EL INDEX*/
        public void LlenarOfertas(DataTable Tabla, DataList dtlOfertas)   
        {
            int i = 0;
            string Ruta = clsValidaciones.ObtenerUrlImages();
            string PaginaDestino = "DetalleOferta.aspx";
            if (!clsValidaciones.ExistColumn(Tabla, "strLink"))
                Tabla.Columns.Add("strLink");
            while (i < Tabla.Rows.Count)
            {
                switch (Tabla.Rows[i]["TipoPlan"].ToString())
                {
                    case "CIRC":
                        PaginaDestino = "DetallePlan.aspx";
                        break;
                    case "CUCE":
                        PaginaDestino = "DetalleCrucero.aspx";
                        break;
                    case "CASA":
                        PaginaDestino = "DetalleCasa.aspx";
                        break;
                }
                clsCacheControl cCacheControl = new clsCacheControl();
                string sSesion = cCacheControl.RecuperarSesionId((Page)HttpContext.Current.Handler);
                Tabla.Rows[i]["strLink"] = clsValidaciones.ObtenerUrlRutaPage(PaginaDestino + "?Codigo=" + Tabla.Rows[i]["Id"].ToString() + "&idSesion=" + sSesion + "&TipoPlan=" + Tabla.Rows[i]["TipoPlan"].ToString());
                i++;
            }

            ModificarNombrePlan(Tabla);
            DataView ViewSec = new DataView();
            ViewSec.Table = Tabla;
            ViewSec.Sort = "Moneda";

            dtlOfertas.DataSource = ViewSec.ToTable();
            dtlOfertas.DataBind();
        }
        public string[] SepararTexto(string Texto)
        {
            if (Texto != "")
            {
                int PosInicial = 0;
                string[] Textos = new string[2];
                string TextoSeparar = Texto;
                PosInicial = TextoSeparar.IndexOf("</p>");
                Textos[0] = TextoSeparar.Substring(0, PosInicial);
                Textos[0] = Textos[0] + TextoSeparar.Substring(PosInicial, "</p>".Length);
                PosInicial = PosInicial + "</p>".Length;
                Textos[1] = TextoSeparar.Substring(PosInicial);
                return Textos;
            }
            else
            {
                return new string[2];
            }
        }
        private void ModificarNombrePlan(DataTable dtDatos)
        {
            string titulo;
            if (!clsValidaciones.ExistColumn(dtDatos, "NombreModificado"))
                dtDatos.Columns.Add("NombreModificado");
            for (int i = 0; i < dtDatos.Rows.Count; i++)
            {
                titulo = dtDatos.Rows[i]["Nombre"].ToString();
                if (titulo.ToString().Length > 50)
                {
                    titulo = titulo.Substring(0, 44) + "...";
                    dtDatos.Rows[i]["NombreModificado"] = titulo;
                }
                else
                {
                    dtDatos.Rows[i]["NombreModificado"] = titulo;
                }
            }
        }
        #endregion
    }
}
