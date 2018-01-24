using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using Ssoft.Utils;
using Ssoft.Rules.Generales;
using Ssoft.ManejadorExcepciones;
using Ssoft.ValueObjects;
using Ssoft.Sql;
using SsoftQuery.Vuelos;
using System.Web.UI.WebControls;
using Ssoft.Rules.Corporativo;

namespace Ssoft.Rules.WebServices
{
    public class csVuelos
    {
        protected csGenerales Generales = new csGenerales();

        private DataSql pclsDataSql = new DataSql();

        protected string strConexion = default(string);
        /// <summary>
        /// Estable u obtiene es string de conexion
        /// </summary>
        public string Conexion
        {
            set { strConexion = value; }
            get { return strConexion; }
        }
        public csVuelos()
        {
            Conexion = clsSesiones.getConexion();
            pclsDataSql.Conexion = clsSesiones.getConexion();
        }
        public List<string> ExcluirAerolineas()
        {
            List<string> lsExcluirAerolinea = new List<string>();
            try
            {
               
                DataTable dtData = new DataTable();
                string sTipoRefere = clsValidaciones.GetKeyOrAdd("AIRLINESABRE");

                int iCantidad = int.Parse(clsValidaciones.GetKeyOrAdd("BloqueoAir","15"));

                dtData = new CsConsultasVuelos().Consultatabla("0", "AIRLINE", "CODE", "BITACTIVO");
               
                    if (dtData.Rows.Count > 0)
                    {
                        int iTotal = dtData.Rows.Count;
                        if (iTotal <= iCantidad)
                        {
                            for (int i = 0; i < iTotal; i++)
                            {
                                lsExcluirAerolinea.Add(dtData.Rows[i]["CODE"].ToString().TrimStart().TrimEnd());
                            }
                        }
                        else
                        {
                            for (int i = 0; i < iCantidad; i++)
                            {
                                lsExcluirAerolinea.Add(dtData.Rows[i]["CODE"].ToString().TrimStart().TrimEnd());
                            }
                        }
                    }                
               
            }
            catch 
            {
            }
            return lsExcluirAerolinea;
        }
        public List<string> AerolineasPreferidas()
        {
            List<string> lsAerolineaPreferida = new List<string>();
            try
            {
                csGenerales cGeneral = new csGenerales();
                DataSet dsData = new DataSet();
                string sTipoRefere = clsValidaciones.GetKeyOrAdd("AIRLINESABRE");

                //dsData = AerolineasConEmision();
                //if (dsData.Tables[0].Rows.Count > 0)
                //{
                //    int iTotal = dsData.Tables[0].Rows.Count;
                //    if (iTotal < 15)
                //    {
                //        for (int i = 0; i < iTotal; i++)
                //        {
                //            lsAerolineaPreferida.Add(dsData.Tables[0].Rows[i]["strRefere"].ToString());
                //        }
                //    }
                //}

                int iCantidad = int.Parse(clsValidaciones.GetKeyOrAdd("PreferencesAir", "99"));

                dsData = cGeneral.Refere(sTipoRefere, 2);

                if (dsData.Tables.Count > 0)
                {

                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        int iTotal = dsData.Tables[0].Rows.Count;
                        if (iTotal <= iCantidad)
                        {
                            for (int i = 0; i < iTotal; i++)
                            {
                                lsAerolineaPreferida.Add(dsData.Tables[0].Rows[i]["strRefere"].ToString());
                            }
                        }
                        else
                        {
                            for (int i = 0; i < iCantidad; i++)
                            {
                                lsAerolineaPreferida.Add(dsData.Tables[0].Rows[i]["strRefere"].ToString());
                            }
                        }
                    }
                }

            }
            catch 
            {
            }
            return lsAerolineaPreferida;
        }
        public DataSet AerolineasConEmision()
        {
            DataSet dsDatos = new DataSet();
            string sIdioma = clsSesiones.getIdioma();
            string sAplicacion = clsSesiones.getAplicacion().ToString();
            string sPseudo = "0";
            StringBuilder consulta = new StringBuilder();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    //int iCorporativo = cCache.Corporativo.Count;
                    //for (int i = 0; i < iCorporativo; i++)
                    //{
                    //    if (cCache.Corporativo[i].ProveedorWs.Equals(Enum_ProveedorWebServices.Sabre))
                    //    {
                    //        sPseudo = cCache.Corporativo[i].Credentials.Pseudo.ToString();
                    //    }
                    //}
                    consulta.AppendLine(" SELECT     tblOperaResult.intPccPais, tblOperaResult.intOperador, tblOperaResult.bitEmision, tblOperaResult.intMoneda,   ");
                    consulta.AppendLine(" tblOperaResult.dblComision, tblOperaResult.bitPago, tblOperaResult.bitActivo, tblRefere.strRefere,  ");
                    consulta.AppendLine(" tblRefere_1.strRefere AS strMoneda, tblRefere_1.strDetalle AS strDetalleMoneda ");
                    consulta.AppendLine(" FROM         tblOperaResult INNER JOIN ");
                    consulta.AppendLine(" tblRefere ON tblOperaResult.intOperador = tblRefere.intidRefere INNER JOIN ");
                    consulta.AppendLine(" tblRefere AS tblRefere_1 ON tblOperaResult.intMoneda = tblRefere_1.intidRefere ");
                    consulta.AppendLine(" WHERE    (tblRefere.intAplicacion = " + sAplicacion + ") ");
                    consulta.AppendLine(" AND    (tblRefere.strIdioma = '" + sIdioma + "') ");
                    consulta.AppendLine(" AND    (tblRefere_1.intAplicacion = " + sAplicacion + ") ");
                    consulta.AppendLine(" AND    (tblRefere_1.strIdioma = '" + sIdioma + "') ");
                    consulta.AppendLine(" AND    (tblOperaResult.intPccPais = " + sPseudo + ") ");

                    dsDatos = pclsDataSql.Select(consulta.ToString());
                }
            }
            catch 
            {
            }
            return dsDatos;
        }
        public Enum_TipoVuelo getValidarTipoTrayecto(List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation)
        {
            string sIdioma = clsSesiones.getIdioma();
            Enum_TipoVuelo eTipoVuelo = Enum_TipoVuelo.Internacional;
            if (sIdioma.Equals(""))
                sIdioma = clsValidaciones.GetKeyOrAdd("sIdioma", "es");

            string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");


            DataTable dtConsulta = new CsConsultasVuelos().SPConsultaTabla("SPCONSULTAPAIS", new string[2] { sPaisDefault,sIdioma});

            if (dtConsulta != null)
            {
                string strCodigoCOL = dtConsulta.Rows[0]["INTCODE"].ToString();
                try
                {
                    foreach (VO_OriginDestinationInformation vo_OriginDestinationInformation in lvo_OriginDestinationInformation)
                    {
                        VO_Aeropuerto vo_AeropuertoOrigen = vo_OriginDestinationInformation.Vo_AeropuertoOrigen;
                        VO_Aeropuerto vo_AeropuertoDestino = vo_OriginDestinationInformation.Vo_AeropuertoDestino;

                        string strConexion = this.Conexion;


                        string strOrigen = new CsConsultasVuelos().ConsultaCodigo(vo_AeropuertoOrigen.SCodigo,"TBLIATA","STRCOUNTRY","STRCODE");

                       
                        string strDestino = new CsConsultasVuelos().ConsultaCodigo(vo_AeropuertoDestino.SCodigo,"TBLIATA","STRCOUNTRY","STRCODE");

                        if ((strOrigen.Equals(strCodigoCOL) && strDestino.Equals(strCodigoCOL)))
                        {
                            eTipoVuelo = Enum_TipoVuelo.Nacional;
                        }
                        else
                        {
                            eTipoVuelo = Enum_TipoVuelo.Internacional;
                            break;
                        }
                    }
                }
                catch 
                {
                }
            }
            clsSesiones.setTipoVuelo(eTipoVuelo);
            return eTipoVuelo;
        }
        /// <summary>
        /// Para validar el tipo de salida, si sale de colombia y verificar si es internacional
        /// </summary>
        /// <param name="lvo_OriginDestinationInformation">Parametros de destinos</param>
        /// <returns>Tipo de salida (NAl o InterNal)</returns>
        public Enum_TipoVuelo getValidarTipoSalida(List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation)
        {
           
            Enum_TipoVuelo eTipoSalida = Enum_TipoVuelo.Internacional;
            string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");





            string strCodigoCOL = new CsConsultasVuelos().ConsultaCodigo(sPaisDefault,"TBLPAIS","INTCODE","STRCOUNTRYCODE");
                try
                {
                    foreach (VO_OriginDestinationInformation vo_OriginDestinationInformation in lvo_OriginDestinationInformation)
                    {
                        VO_Aeropuerto vo_AeropuertoOrigen = vo_OriginDestinationInformation.Vo_AeropuertoOrigen;
                        VO_Aeropuerto vo_AeropuertoDestino = vo_OriginDestinationInformation.Vo_AeropuertoDestino;

                        string strConexion = this.Conexion;

                        DataTable dt = new CsConsultasVuelos().SPConsultaTabla("SPCONSULTAAEROPUERTO",new string[1]{vo_AeropuertoOrigen.SCodigo});
                        string strOrigen = dt.Rows[0][1].ToString();

                        dt = new CsConsultasVuelos().SPConsultaTabla("SPCONSULTAAEROPUERTO", new string[1] { vo_AeropuertoDestino.SCodigo });

                        string strDestino = dt.Rows[0][1].ToString();

                        if ((strOrigen.Equals(strCodigoCOL) && !strDestino.Equals(strCodigoCOL)))
                        {
                            eTipoSalida = Enum_TipoVuelo.Nacional;
                            break;
                        }
                    }
                }
                catch 
                {
                }
            
            return eTipoSalida;
        }
        public string getValidarPais(List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation)
        {
          
            string sPaisRetorna = string.Empty;
            bool bRetorna = true;
            DataSet dsData = new DataSet();
            try
            {
                dsData = csConsultaPcc();
                if (dsData.Tables.Count > 0)
                {
                    int iTotal = dsData.Tables[0].Rows.Count;
                    
                    for (int i = 0; i < iTotal; i++)
                    {
                        string sPais = dsData.Tables[0].Rows[i]["intPais"].ToString();
                        bRetorna = true;
                        string strCodigoCOL = sPais;

                        foreach (VO_OriginDestinationInformation vo_OriginDestinationInformation in lvo_OriginDestinationInformation)
                        {
                            VO_Aeropuerto vo_AeropuertoOrigen = vo_OriginDestinationInformation.Vo_AeropuertoOrigen;
                            VO_Aeropuerto vo_AeropuertoDestino = vo_OriginDestinationInformation.Vo_AeropuertoDestino;
                            string strDestino="0";
                            string strOrigen="0";
                            string strConexion = this.Conexion;

                        
                            DataTable dt = new CsConsultasVuelos().SPConsultaTabla("SPCONSULTAAEROPUERTO",new string[1] {vo_AeropuertoOrigen.SCodigo});
                            if(dt != null)
                               strOrigen=dt.Rows[0][1].ToString();

                             dt = new CsConsultasVuelos().SPConsultaTabla("SPCONSULTAAEROPUERTO",new string[1] {vo_AeropuertoOrigen.SCodigo});
                             if (dt != null)
                            {
                               strDestino = dt.Rows[0][1].ToString();
                            }
                            if (!(strOrigen.Equals(strCodigoCOL) && strDestino.Equals(strCodigoCOL)))
                            {
                                bRetorna = false;
                                break;
                            }
                        }
                        if (bRetorna)
                        {
                            sPaisRetorna = sPais;
                            break;
                        }
                    }
                }
            }
            catch 
            {
            }
            return sPaisRetorna;
        }

        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="lvo_OriginDestinationInformation"></param>
        /// <returns></returns>
        public string getValidarRuta(List<VO_OriginDestinationInformation> lvo_OriginDestinationInformation)
        {
          
            string sPaisRetorna = string.Empty;
            bool bRetorna = false;
            DataSet dsData = new DataSet();
            try
            {
                //dsData = csConsultaException();
                if (dsData.Tables.Count > 0)
                {
                    int iTotal = dsData.Tables[0].Rows.Count;
                  
                    for (int i = 0; i < iTotal; i++)
                    {
                        string sPais = string.Empty;
                        string strCodigoOrigen = dsData.Tables[0].Rows[i]["intOrigen"].ToString();
                        string strCodigoDestino = dsData.Tables[0].Rows[i]["intDestino"].ToString();

                        foreach (VO_OriginDestinationInformation vo_OriginDestinationInformation in lvo_OriginDestinationInformation)
                        {
                            VO_Aeropuerto vo_AeropuertoOrigen = vo_OriginDestinationInformation.Vo_AeropuertoOrigen;
                            VO_Aeropuerto vo_AeropuertoDestino = vo_OriginDestinationInformation.Vo_AeropuertoDestino;

                            string strConexion = this.Conexion;

                            string strOrigen = new CsConsultasVuelos().ConsultaCodigo(vo_AeropuertoOrigen.SCodigo,"TBLIATA","STRCOUNTRY","STRCODE");

                            string strDestino = new CsConsultasVuelos().ConsultaCodigo(vo_AeropuertoDestino.SCodigo,"TBLIATA","STRCOUNTRY","STRCODE");

                            if (strOrigen.Equals(strCodigoOrigen) && strDestino.Equals(strCodigoDestino))
                            {
                                bRetorna = true;
                                sPais = dsData.Tables[0].Rows[i]["intPais"].ToString();
                                break;
                            }
                        }
                        if (bRetorna)
                        {
                            sPaisRetorna = sPais;
                            break;
                        }
                    }
                }
            }
            catch 
            {
            }
            return sPaisRetorna;
        }
        public DataSet csConsultaPcc()
        {
            DataSet dsDatos = new DataSet();
            StringBuilder consulta = new StringBuilder();
           

            consulta.AppendLine(" SELECT     intPais ");
            consulta.AppendLine(" FROM         tblPaisPseudo  ");
            consulta.AppendLine(" WHERE    (bitActivo = 1) ");
           

            dsDatos = pclsDataSql.Select(consulta.ToString());
            return dsDatos;
        }
        public DataSet csConsultaException()
        {
            DataSet dsDatos = new DataSet();
            StringBuilder consulta = new StringBuilder();
           

            consulta.AppendLine(" SELECT     intOrigen,  intDestino, intPais ");
            consulta.AppendLine(" FROM         tblExceptionAir  ");
            consulta.AppendLine(" WHERE    (bitActivo = 1) ");
           

            dsDatos = pclsDataSql.Select(consulta.ToString());
            return dsDatos;
        }
                     
       
        /// <summary>
        /// metodo pendiente por revision
        /// Metodo para validar si el vuelo toca el pais por default
        /// </summary>
        /// <param name="dtData">datatable del itinerario</param>
        /// <returns>bValida, indicador de verificacion</returns>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-01-16
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static bool getValidarPaisDefault(DataTable dtData)
        {
           
            bool bValida = false;

            string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");
            string sPais = clsValidaciones.GetKeyOrAdd("Paises", "Pais");

            string strCodigoCOL = new CsConsultasVuelos().ConsultaCodigo(sPaisDefault, "TBLPAIS", "INTCODE", "STRCOUNTRYCODE");
            if (strCodigoCOL != "" && strCodigoCOL!=null)
            {
              
                try
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                       DataTable dt = new CsConsultasVuelos().SPConsultaTabla("SPCONSULTAAEROPUERTO",new string[1] {row["intOrigen"].ToString()});
                       if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0][1].ToString().Equals(strCodigoCOL))
                            {
                                bValida = true;
                                break;
                            }
                        }
                       dt = new CsConsultasVuelos().SPConsultaTabla("SPCONSULTAAEROPUERTO", new string[1] { row["intDestino"].ToString() });

                       if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0][1].ToString().Equals(strCodigoCOL))
                            {
                                bValida = true;
                                break;
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            return bValida;
        }
        public static string csConvenio()
        {
            string sConvenio = string.Empty;
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    //if (cCache.Corporativo != null)
                    //{
                    //    int iTotal = cCache.Corporativo.Count;
                    //    for (int i = 0; i < iTotal; i++)
                    //    {
                    //        if (cCache.Corporativo[i].ProveedorWs.Equals(Enum_ProveedorWebServices.Sabre))
                    //        {
                    //            try
                    //            {
                    //                int iTotalConvenio = cCache.Corporativo[i].Convenio.Count;
                    //                for (int j = 0; j < iTotalConvenio; j++)
                    //                {
                    //                    int iTotalOpertaConvenio = cCache.Corporativo[i].Convenio[j].OperaConvenio.Count;
                    //                    for (int k = 0; k < iTotalOpertaConvenio; k++)
                    //                    {
                    //                        if (cCache.Corporativo[i].Convenio[j].OperaConvenio[k].Operador.Equals("SnapCode"))
                    //                        {
                    //                            sConvenio = cCache.Corporativo[i].Convenio[j].OperaConvenio[k].Convenio;
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //            catch { }
                    //        }
                    //    }
                    //}
                }
                if (sConvenio.Length.Equals(0))
                {
                    VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                    if (vo_OTA_AirLowFareSearchLLSRQ != null)
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada != null)
                            sConvenio = vo_OTA_AirLowFareSearchLLSRQ.SCodTarifaNegociada;
                    }
                }
            }
            catch
            {
            }
            return sConvenio;
        }

       
    }
}
