using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using Ssoft.Rules.Generales;
using Ssoft.Data;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;
using SsoftQuery.Vuelos;

namespace Ssoft.Rules.Pagina
{
    public class clsNegocioSabre
    {
        private string gstrConexion;

        public string Conexion
        {
            set { this.gstrConexion = value; }
            get { return this.gstrConexion; }
        }

        public clsNegocioSabre()
        {
            try
            {
                this.Conexion = Conexion.ToString();
            }
            catch 
            {
                
            }
        }



        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="strSession"></param>
        /// <param name="strProveedorWs"></param>
        /// <param name="strAccion"></param>
        /// <param name="strOrigen"></param>
        /// <param name="strDestino"></param>
        /// <param name="strFechaSalida"></param>
        /// <param name="strAdultos"></param>
        /// <param name="strIp"></param>
        public void GuardarSession(string strSession,
                                   string strProveedorWs,
                                   string strAccion,
                                   string strOrigen,
                                   string strDestino,
                                   string strFechaSalida,
                                   string strAdultos,
                                   string strIp)
        {
            string pstrSql = string.Empty;
            try
            {
                pstrSql = " INSERT INTO tblSessiones (strSession, strProveedorWs, strAccion, strOrigen, strDestino, strFechaSalida, intAdultos, strIp) " +
                          " VALUES  ('" +
                          strSession + "', '" +
                          strProveedorWs + "', '" +
                          strAccion + "', '" +
                          strOrigen + "', '" +
                          strDestino + "', '" +
                          strFechaSalida + "', " +
                          strAdultos + ", '" +
                          strIp + "') ";

                AccesoSQL.ExecuteNonQuery(this.Conexion, CommandType.Text, pstrSql);
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar("Error generado, SQL ejecutado " + pstrSql);
                ExceptionHandled.Publicar(Ex);
            }
        }
        public List<string> CargarPlacas(List<string> lsAerolineas)
        {
          
            try
            {
                DataTable dtAirIATA = new CsConsultasVuelos().Consultatabla(null,"TBLAIRLINE","CODE",null);

                if (dtAirIATA.Rows.Count > 0)
                {
                    int iCount = dtAirIATA.Rows.Count;
                    for (int i = 0; i < iCount; i++)
                    {
                        lsAerolineas.Add(dtAirIATA.Rows[i]["CODE"].ToString());
                    }
                }
            }
            catch(Exception Ex)
            {
                ExceptionHandled.Publicar("Error generado");
                ExceptionHandled.Publicar(Ex);
            }
            return lsAerolineas;
        }
       
    }
}
