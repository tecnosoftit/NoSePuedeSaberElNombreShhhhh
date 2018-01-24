using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;
using Ssoft.ValueObjects;

namespace Ssoft.Pages
{
    public class csPlantillas : TemplateControl
    {
        protected string strConexion = default(string);
        /// <summary>
        /// Estable u obtiene es string de conexion
        /// </summary>
        public string Conexion
        {
            set { strConexion = value; }
            get { return strConexion; }
        }
        public csPlantillas()
        {
            strConexion = clsSesiones.getConexion();
        }
      
      
    }
}
