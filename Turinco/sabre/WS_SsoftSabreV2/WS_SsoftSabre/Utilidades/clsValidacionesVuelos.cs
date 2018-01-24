using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Ssoft.ValueObjects;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;

namespace WS_SsoftSabre.Utilidades
{
    public class clsValidacionesVuelos
    {
        private string strConexion;

        public string Conexion
        {
            get { return strConexion; }
            set { strConexion = value; }
        }
        public static void setFechaLimiteTiquete()
        {

            Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno("WP");
            string ComandoPQ_ = Negocios_WebServiceSabreCommand._EjecutarComando("PQ");
            DateTime FechaPQ_ = new DateTime();

            ComandoPQ_ = clsValidaciones._Seleccionar_FechaExpedicionTickete(ComandoPQ_);

            if (ComandoPQ_ == null)
            {
                FechaPQ_ = DateTime.Today.AddDays(1);
                FechaPQ_ = FechaPQ_.AddMinutes(-1);
            }
            else
            {
                if (ComandoPQ_.Contains("/"))
                {
                    string[] sDate = ComandoPQ_.Split('/');
                    ComandoPQ_ = sDate[0] + DateTime.Now.Year;
                    ComandoPQ_ += " " + sDate[1].Insert(2, ":");
                }
                DateTime.TryParse(ComandoPQ_, out FechaPQ_);
            }
            int iHoras = 8;
            try
            {
                iHoras = clsSesiones.getCredentials().TimeLimit;
            }
            catch { }
            try
            {
                FechaPQ_ = FechaPQ_.AddHours(-iHoras);
            }
            catch
            {
                FechaPQ_ = DateTime.Today.AddDays(1);
                FechaPQ_ = FechaPQ_.AddMinutes(-1);
            }
            Ssoft.Utils.clsSesiones.SET_TICKETE(FechaPQ_);
        }

        public static List<VO_SabreErrors> _SabreErrors()
        {
            List<VO_SabreErrors> SabreErrors_ = new List<VO_SabreErrors>();

            try
            {
                DataSet Datos_ = new DataSet("Error");
                //string sRuta = clsValidaciones.RutaXmlGen();
                string sRuta = clsValidaciones.XMLTempCrea();
                
                string sArchivo = "XML_SabreErrors.xml";
                //Datos_.ReadXml(HttpContext.Current.Server.MapPath(clsValidaciones.GetKeyOrAdd(@"RutaXMLError")));
                Datos_.ReadXml(sRuta + sArchivo);

                DataTable TablaErrores_ = Datos_.Tables["SabreError"];
                DataTable TablaSolucion_ = Datos_.Tables["SabreSolution"];

                foreach (DataRow Row_ in TablaErrores_.Rows)
                {
                    List<string> Soluciones_ = new List<string>();

                    for (int i = 0; i < TablaSolucion_.Rows.Count; i++)
                    {
                        if (Row_[0].ToString().CompareTo(TablaSolucion_.Rows[i][1].ToString()) == 0)
                            Soluciones_.Add(TablaSolucion_.Rows[i][0].ToString());
                    }
                    VO_SabreErrors Errors_ = new VO_SabreErrors(Row_[1].ToString(), Soluciones_);
                    SabreErrors_.Add(Errors_);
                }
                Datos_.Dispose();
            }
            catch (Exception Ex)
            {
                Ssoft.ManejadorExcepciones.clsParametros cParametros = new Ssoft.ManejadorExcepciones.clsParametros();
                cParametros.Id = 0;
                cParametros.Code = "0";
                cParametros.Info = Ex.Source;
                cParametros.Severity = Ssoft.ManejadorExcepciones.clsSeveridad.Alta;
                cParametros.Tipo = Ssoft.ManejadorExcepciones.clsTipoError.Library;
                cParametros.Metodo = "_SabreErrors";
                cParametros.Complemento = "Error al tratar de obtener archivo";
                cParametros.ViewMessage.Add("No se encontro archivo");
                cParametros.Sugerencia.Add("Por favor revise que el archivo xml de errores este en el directorio XMl");
                cParametros.Message = Ex.Message;
                Ssoft.ManejadorExcepciones.ExceptionHandled.Publicar(cParametros);
            }

            return SabreErrors_;
        }
        public static string setResultComado(string sComand, string sSearch, int iPosIni, int iLength)
        {
            string sDato = string.Empty;
            try
            {
                string sComando = Negocios_WebServiceSabreCommand._EjecutarComando(sComand);
                if (sComando.Contains("\n"))
                {
                    string[] strResponse = sComando.Split('\n');
                    int iPosGen = 0;
                    try
                    {
                        iPosGen = strResponse.Length;
                    }
                    catch { }
                    for (int i = 0; i < iPosGen; i++)
                    {
                        if (strResponse[i].ToString().Contains(sSearch))
                        {
                            if (iLength.Equals(0))
                            {
                                sDato = strResponse[i].Trim();
                            }
                            else
                            {
                                sDato = strResponse[i].Substring(iPosIni, iLength).Trim();
                            }
                            break;
                        }
                    }
                }
            }
            catch { }
            return sDato;
        }
        public static string setResultComado(string sComando, int iLine, int iPosIni, int iLength)
        {
            string sDato = string.Empty;
            try
            {
                if (sComando.Contains("\n"))
                {
                    string[] strResponse = sComando.Split('\n');
                    sDato = strResponse[iLine].Substring(iPosIni, iLength).Trim();
                }
            }
            catch { }
            return sDato;
        }
        public static string setResultComado(string sComando, string sTextoLinea, string sTextoParrafo, int iLength)
        {
            string sDato = string.Empty;
            try
            {
                if (sComando.Contains("\n"))
                {
                    string[] strResponse = sComando.Split('\n');
                    int iPosGen = strResponse.Length;
                    for (int i = 0; i < iPosGen; i++)
                    {
                        if (strResponse[i].ToString().Contains(sTextoLinea))
                        {
                            if (strResponse[i].ToString().Contains(sTextoParrafo))
                            {
                                int iPosIni = strResponse[i].ToString().IndexOf(sTextoParrafo);
                                sDato = strResponse[i].Substring(iPosIni, iLength).Trim();
                            }
                            break;
                        }
                    }
                }
                else
                {
                    if (sComando.ToString().Contains(sTextoParrafo))
                    {
                        int iPosIni = sComando.ToString().IndexOf(sTextoParrafo);
                        sDato = sComando.Substring(iPosIni, iLength).Trim();
                    }
                }
            }
            catch { }
            return sDato;
        }
        public static string setResultComado(string sComando, string sTextoLinea, string sTextoParrafo)
        {
            string sDato = string.Empty;
            try
            {
                if (sComando.Contains("\n"))
                {
                    string[] strResponse = sComando.Split('\n');
                    int iPosGen = strResponse.Length;
                    for (int i = 0; i < iPosGen; i++)
                    {
                        if (strResponse[i].ToString().Contains(sTextoLinea))
                        {
                            if (strResponse[i].ToString().Contains(sTextoParrafo))
                            {
                                int iPosIni = strResponse[i].ToString().IndexOf(sTextoParrafo);
                                string sTemp = strResponse[i].Substring(iPosIni).Trim();
                                int iLength = sTemp.Length;
                                if (sTemp.Contains(" "))
                                    iLength = sTemp.ToString().IndexOf(" ");
                                sDato = strResponse[i].Substring(iPosIni, iLength).Trim();
                            }
                            break;
                        }
                    }
                }
                else
                {
                    if (sComando.ToString().Contains(sTextoParrafo))
                    {
                        int iPosIni = sComando.ToString().IndexOf(sTextoParrafo);
                        string sTemp = sComando.Substring(iPosIni).Trim();
                        int iLength = sTemp.Length;
                        if (sTemp.Contains(" "))
                            iLength = sTemp.ToString().IndexOf(" ");
                        sDato = sComando.Substring(iPosIni, iLength).Trim();
                    }
                }
            }
            catch { }
            return sDato;
        }
        public static string setResultComadoUltimo(string sComando, string sTextoLinea)
        {
            string sDato = string.Empty;
            try
            {
                if (sComando.Contains("\n"))
                {
                    string[] strResponse = sComando.Split('\n');
                    int iPosGen = strResponse.Length;
                    for (int i = 0; i < iPosGen; i++)
                    {
                        if (strResponse[i].ToString().Contains(sTextoLinea))
                        {
                            sDato = strResponse[i].Trim();

                            string sTemp = strResponse[i].Trim();
                            int iLength = sTemp.Length;
                            if (sTemp.Contains(" "))
                            {
                                int iPosTotal = 0;
                                int iPosIni = 0;
                                for (int j = iLength; j > 0; j--)
                                {
                                    if (!strResponse[i].Substring(j, 1).ToString().Contains(" "))
                                    {
                                        iPosTotal++;
                                    }
                                    else
                                    {
                                        iPosIni = iLength - iPosTotal;
                                        sDato = strResponse[i].Substring(iPosIni, iPosTotal).Trim();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    sDato = sComando.Trim();

                    string sTemp = sComando.Trim();
                    int iLength = sTemp.Length;
                    if (sTemp.Contains(" "))
                    {
                        int iPosTotal = 0;
                        int iPosIni = 0;
                        for (int j = iLength; j > 0; j--)
                        {
                            if (!sComando.Substring(j, 1).ToString().Contains(" "))
                            {
                                iPosTotal++;
                            }
                            else
                            {
                                iPosIni = iLength - iPosTotal;
                                sDato = sComando.Substring(iPosIni, iPosTotal).Trim();
                                break;
                            }
                        }
                    }
                }
            }
            catch { }
            return sDato;
        }
        public static string setResultComado(string sComando, string sTextoLinea)
        {
            string sDato = sComando;
            try
            {
                if (sComando.Contains("\n"))
                {
                    string[] strResponse = sComando.Split('\n');
                    int iPosGen = strResponse.Length;
                    for (int i = 0; i < iPosGen; i++)
                    {
                        if (strResponse[i].ToString().Contains(sTextoLinea))
                        {
                            sDato = strResponse[i];
                            break;
                        }
                    }
                }
            }
            catch { }
            return sDato;
        }
        public static int setResultComadoPos(string sComando, string sTextoLinea)
        {
            int iDato = 0;
            try
            {
                if (sComando.Contains("\n"))
                {
                    string[] strResponse = sComando.Split('\n');
                    int iPosGen = strResponse.Length;
                    for (int i = 0; i < iPosGen; i++)
                    {
                        if (strResponse[i].ToString().Contains(sTextoLinea))
                        {
                            iDato = i;
                            break;
                        }
                    }
                }
            }
            catch { }
            return iDato;
        }
        public static List<string[]> setComadoPD(string sComando)
        {
            List<string[]> lsData = new List<string[]>();
            try
            {
                if (sComando.Contains("\n"))
                {
                    string[] strResponse = sComando.Split('\n');
                    int iPosGen = strResponse.Length;
                    for (int i = 0; i < iPosGen; i++)
                    {
                        if (strResponse[i].ToString().Contains(".1"))
                        {
                            int iPosTot = strResponse[i].Length - 3;
                            string[] sData = new string[4];
                            string sTemp = string.Empty;
                            sData[0] = strResponse[i].ToString();
                            sData[1] = strResponse[i].ToString().Substring(iPosTot);
                            sTemp = strResponse[i].ToString().Substring(4).Trim();
                            if (sTemp.Contains(" "))
                            {
                                sTemp = sTemp.Substring(0, sTemp.IndexOf(" "));
                            }
                            sData[2] = sTemp.Trim();
                            sData[3] = clsValidaciones.RetornaNumero(strResponse[i].ToString().Substring(0, 2).Trim());
                            lsData.Add(sData);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                string sError = Ex.Message;
            }
            return lsData;
        }
        public static bool setValidaReserva(string sItinerarioRes, string sItinerarioOld)
        {
            bool bResp = true;
            clsParametros cParametros = new clsParametros();
            try
            {
                string[] strResponseOld = null;
                string[] strResponseNew = null;
                if (sItinerarioRes.Contains("\n"))
                {
                    strResponseNew = sItinerarioRes.Split('\n');
                }
                if (sItinerarioOld.Contains("\n"))
                {
                    strResponseOld = sItinerarioOld.Split('\n');
                }
                int iPosGen = strResponseNew.Length;
                string[] strResponse = null;
                if (!strResponseNew.Length.Equals(strResponseOld.Length))
                {
                    int iPosOld = strResponseOld.Length;
                    strResponse = new string[iPosOld];
                    int j = 0;
                    for (int i = 0; i < iPosGen; i++)
                    {
                        if (strResponseNew[i].ToString().Length > 4)
                        {
                            if (!strResponseNew[i].ToString().Substring(0, 3).Equals(strResponseOld[i].ToString().Substring(0, 3)))
                            {
                                strResponse[j] = strResponseNew[i];
                            }
                        }
                        j++;
                    }
                }
                else
                {
                    strResponse = strResponseNew;
                }
                for (int i = 0; i < iPosGen; i++)
                {
                    if (strResponse[i].ToString().Length > 25)
                    {
                        if (!strResponse[i].ToString().Substring(0, 25).Equals(strResponseOld[i].ToString().Substring(0, 25)))
                        {
                            bResp = false;
                            cParametros.Id = 0;
                            cParametros.Message = "Diferencia en itinerarios";
                            cParametros.Metodo = "setValidaReserva ";
                            cParametros.Tipo = clsTipoError.Library;
                            cParametros.Severity = clsSeveridad.Moderada;
                            cParametros.Complemento = "Itinerario de la reserva " + sItinerarioRes + "/n" + "Itinerario de la busqueda " + sItinerarioOld;
                            ExceptionHandled.Publicar(cParametros);
                            break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Metodo = "setValidaReserva ";
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Itinerario de la reserva " + sItinerarioRes + "/n" + "Itinerario de la busqueda " + sItinerarioOld;
                ExceptionHandled.Publicar(cParametros);
            }
            return bResp;
        }
        public static bool setValidaTarifa(string sTarifaRes, string sTarifaOld)
        {
            bool bResp = true;
            clsParametros cParametros = new clsParametros();
            try
            {
                if (!sTarifaRes.ToString().Equals(sTarifaOld.ToString()))
                {
                    bResp = false;
                    cParametros.Id = 0;
                    cParametros.Message = "Diferencia en Tarifas";
                    cParametros.Metodo = "setValidaReserva ";
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.Complemento = "Tarifa de la reserva " + sTarifaRes + "/n" + " Tarifa de la busqueda " + sTarifaOld;
                    ExceptionHandled.Publicar(cParametros);
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Metodo = "setValidaReserva ";
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Tarifa de la reserva " + sTarifaRes + "/n" + " Tarifa de la busqueda " + sTarifaOld;
                ExceptionHandled.Publicar(cParametros);
            }
            return bResp;
        }
        /// <summary>
        /// Metodo que toma los PQ de linea de emision
        /// </summary>
        /// <param name="sComando"></param>
        /// <returns></returns>
        public static List<string> setBuscaPQ(string sComando)
        {
            List<string> slDato = new List<string>();
            try
            {
                if (sComando.Contains("\n"))
                {
                    string[] strResponse = sComando.Split('\n');
                    int iPosGen = strResponse.Length;
                    for (int i = 0; i < iPosGen; i++)
                    {
                        if (strResponse[i].Contains("PQ "))
                        {
                            slDato.Add(strResponse[i].Substring(3, 1));
                        }
                    }
                }
            }
            catch { }
            return slDato;
        }
    }
}
