using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ssoft.ManejadorExcepciones;
using System.Configuration;
using Ssoft.Sql;
using Ssoft.Data;
using System.IO;

namespace Ssoft.Utils
{
    public static class clsValidaciones
    {
        private const string FormatDate_ = "yyyy-MM-dd";
        private const string FormatDateDataBase_ = "yyyy-MM-dd";

        public static string sFormatoFecha = GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");//clsSesiones.getFormatoFecha().ToString();
        public static string sFormatoFechaBD = GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd"); //clsSesiones.getFormatoFechaBD().ToString();
        public static string sFormatoFechaSabre = GetKeyOrAdd("FormatoFechaSabre", "yyyy-MM-dd");//clsSesiones.getFormatoFecha().ToString();

        public const string FORMATO_NUMEROS = "#,##0";
        public const string FORMATO_NUMEROS_DECIMAL = "#,##0.00";
        public const string FORMATO_DECIMAL = "####0.00";


        public static decimal getDecimal(string Valor)
        {
            decimal dResultado = 0;
            try
            {
                if (Valor != null && Valor.Length > 0)
                {
                    string decimalSeparator =
                        System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                    Valor = Valor.Replace(".", decimalSeparator);
                    Valor = Valor.Replace(",", decimalSeparator);

                    decimal dec = 0;
                    if (decimal.TryParse(Valor, out dec))
                    {
                        dResultado = Decimal.Round(dec, MidpointRounding.ToEven);
                    }
                }
            }
            catch { }
            return dResultado;
        }
        public static decimal getDecimalRound(string Valor)
        {
            decimal dResultado = 0;
            try
            {
                dResultado = getDecimalRound(Valor, 0, true);
                //if (Valor != null && Valor.Length > 0)
                //{
                //    string decimalSeparator =
                //        System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                //    Valor = Valor.Replace(".", decimalSeparator);
                //    Valor = Valor.Replace(",", decimalSeparator);

                //    decimal dec = 0;
                //    if (decimal.TryParse(Valor, out dec))
                //    {
                //        dResultado = Decimal.Round(dec, MidpointRounding.AwayFromZero);
                //        //if (dResultado < Convert.ToDecimal(Valor))
                //            //dResultado = dResultado + 1;
                //    }
                //}
            }
            catch { }
            return dResultado;
        }
        public static decimal getDecimalRound(string Valor, int iDecimales, bool bSube)
        {
            decimal dResultado = 0;
            try
            {
                dResultado = getDecimalNotRound(Valor);
                decimal dec = dResultado;
                dResultado = Decimal.Round(dec, iDecimales);
            }
            catch { }
            return dResultado;
        }
        public static decimal getDecimalNotGroup(string Valor)
        {
            decimal dResultado = 0;
            try
            {
                if (Valor != null && Valor.Length > 0)
                {
                    string groupSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;

                    Valor = Valor.Replace(groupSeparator, "");

                    decimal dec = 0;
                    if (decimal.TryParse(Valor, out dec))
                    {
                        dResultado = dec;
                    }
                }
            }
            catch { }
            return dResultado;
        }
        public static decimal getDecimalNotRound(string Valor)
        {
            decimal dResultado = 0;
            try
            {
                if (Valor != null && Valor.Length > 0)
                {
                    string decimalSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                    Valor = Valor.Replace(".", decimalSeparator);
                    Valor = Valor.Replace(",", decimalSeparator);

                    dResultado = Convert.ToDecimal(Valor);
                    //decimal dec = Convert.ToDecimal(Valor);
                    //dResultado = Decimal.Round(dec, 2);

                    //Valor = Valor.Replace(".", decimalSeparator);
                    //Valor = Valor.Replace(",", decimalSeparator);

                    //decimal dec = 0;
                    //if (decimal.TryParse(Valor, out dec))
                    //{
                    //    dResultado = dec;
                    //}
                }
            }
            catch { }
            return dResultado;
        }
        public static string getDecimalBD(string Valor)
        {
            try
            {
                if (Valor != null && Valor.Length > 0)
                {
                    Valor = Valor.Replace(",", ".");
                }
            }
            catch { }
            return Valor;
        }
        public static DateTime getDatetime(string Fecha)
        {
            DateTime oDateTime = DateTime.Now;
            try
            {
                IFormatProvider provider = new System.Globalization.CultureInfo("es-Co", true);
                String datetime = Fecha.Trim();
                oDateTime = DateTime.Parse(datetime, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            }
            catch { }
            return oDateTime;
        }
        /// <summary>
        /// Si es un valor de solo alfabetico
        /// </summary>
        /// <param name="Text_">String del texto</param>
        /// <returns>false o true, dependiendo de la validacion</returns>
        public static bool IS_ALPHABETIC(string Text_)
        {
            bool bResp = false;
            try
            {
                Regex Validador_ = new Regex("^[a-zA-Z's. ]{1,250}$");
                Match Math_ = Validador_.Match(Text_.ToUpper());
                bResp = Math_.Success;
            }
            catch { }
            return bResp;
        }
        /// <summary>
        /// Si es un correo valido
        /// </summary>
        /// <param name="Text_">String del correo</param>
        /// <returns>false o true, dependiendo de la validacion</returns>
        public static bool IS_EMAIL(string Text_)
        {
            bool bResp = false;
            try
            {
                Regex Validador_ = new Regex("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");
                Match Math_ = Validador_.Match(Text_.ToUpper());
                bResp = Math_.Success;
            }
            catch { }
            return bResp;
        }
        /// <summary>
        /// Si es un valor numerico
        /// </summary>
        /// <param name="Text_">String del numero</param>
        /// <returns>false o true, dependiendo de la validacion</returns>
        public static bool IS_NUMERIC(string Text_)
        {
            bool bResp = false;
            try
            {
                Regex Validador_ = new Regex("^([0-9]){1,}$");
                Match Math_ = Validador_.Match(Text_.ToUpper());
                bResp = Math_.Success;
            }
            catch { }
            return bResp;
        }
        /// <summary>
        /// Si es un valor numerico
        /// </summary>
        /// <param name="Text_">String del numero</param>
        /// <returns>false o true, dependiendo de la validacion</returns>
        public static bool IS_NULL(string Text_)
        {
            bool bResp = false;
            try
            {
                if (Text_.Length.Equals(0))
                    bResp = true;
            }
            catch { }
            return bResp;
        }
        public static bool IS_INTEGER(string Text_)
        {
            bool bResp = false;
            try
            {
                Regex Validador_ = new Regex("^([0-9])");
                Match Math_ = Validador_.Match(Text_.ToUpper());
                bResp = Math_.Success;
            }
            catch { }
            return bResp;
        }
        /// <summary>
        /// Si es un valor numerico
        /// </summary>
        /// <param name="Text_">String del numero</param>
        /// <returns>false o true, dependiendo de la validacion (envie el nombre de la pagina sin el .aspx</returns>
        public static bool IS_PAGE(string Text_)
        {
            bool bResp = false;
            try
            {
                if (!getPaginaActual().ToString().IndexOf(Text_).Equals(-1))
                    bResp = true;
            }
            catch { }
            return bResp;
        }
        public static bool IS_FECHA(string Text_)
        {
            bool bFecha = false;
            try
            {
                DateTime FechaSel = Convert.ToDateTime(Text_);
                bFecha = true;
            }
            catch { bFecha = false; }
            return bFecha;
        }
        /// <summary>
        /// method to validate an IP address
        /// using regular expressions. The pattern
        /// being used will validate an ip address
        /// with the range of 1.0.0.0 to 255.255.255.255
        /// </summary>
        /// <param name="addr">Address to validate</param>
        /// <returns></returns>
        public static bool IS_IP(string Text_)
        {
            //create our match pattern
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
            bool valid = false;
            //check to make sure an ip address was provided
            if (Text_ == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(Text_, 0);
            }
            //return the results
            return valid;
        }
        public static string FORMAT_DATE_DATABASE(string Fecha_)
        {
            return DateTime.Parse(Fecha_, new CultureInfo("fr-FR", true)).ToString(FormatDateDataBase_);
        }
        public static string FORMAT_DATE_DATABASE(DateTime Fecha_)
        {
            return Fecha_.ToString(FormatDateDataBase_);
        }
        public static string FORMAT_LTIME(DateTime Fecha_)
        {
            return Fecha_.ToLongTimeString();
        }
        public static string FORMAT_LTIME(string Fecha_)
        {
            return DateTime.Parse(Fecha_, new CultureInfo("es-Co", true)).ToShortTimeString();
        }
        public static bool SOBREPASA_CARACTERES(string Cadena, int NumCaracteres)
        {
            bool bResp = false;
            try
            {
                if (Cadena.Length > NumCaracteres)
                    bResp = true;
            }
            catch { }
            return bResp;
        }
        public static bool ExistColumn(DataTable dtTabla, string sColumna)
        {
            bool bResp = false;
            try
            {
                int iCounColumns = dtTabla.Columns.Count;
                for (int c = 0; c < iCounColumns; c++)
                {
                    if (dtTabla.Columns[c].ColumnName.Equals(sColumna))
                        bResp = true;
                }
            }
            catch { }
            return bResp;
        }
        public static string FormatearTexto(string sTexto)
        {
            string sTextovalida = "<p><input";
            string sTextoEnvia = sTexto;
            try
            {
                int iPosIni = sTexto.IndexOf(sTextovalida);
                if (iPosIni > -1)
                    sTextoEnvia = sTexto.Remove(iPosIni);
            }
            catch { }
            return sTextoEnvia;
        }
        public static string ValidarStringDB(string sTexto)
        {
            string[] sLista = Lista(sTexto, "'");
            string sTextoNuevo = sTexto;
            try
            {
                int iCant = sLista.Length;
                for (int i = 0; i < iCant; i++)
                {
                    if (i.Equals(0))
                        sTextoNuevo = sLista[i];
                    else
                        sTextoNuevo += sLista[i];
                }
            }
            catch { }
            return sTextoNuevo;
        }
        public static string setFechaSabre(string sFecha)
        {
            // Se reciben fechas con solo formato yyyy/MM/dd
            string sFechaSabre = string.Empty;
            try
            {
                string[] strFecha = null;
                Utils cUtil = new Utils();
                if (sFecha.Contains("/"))
                {
                    strFecha = Lista(sFecha, "/");
                }
                if (sFecha.Contains("-"))
                {
                    strFecha = Lista(sFecha, "-");
                }
                string strMes = RetornaMesLetrasCorto(strFecha[1].ToString(), "en").ToUpper();
                if (strMes.Length.Equals(1))
                    strMes = "0" + strMes;

                string strDia = strFecha[2].ToString();
                if (strDia.Length.Equals(1))
                    strDia = "0" + strDia;
                sFechaSabre = strDia + strMes;
            }
            catch
            {
                sFechaSabre = "01JAN";
            }
            return sFechaSabre;
        }
        public static string getFechaSabre(string sFecha)
        {
            // Se reciben fechas con solo formato yyyy/MM/dd
            string sFechaSabre = string.Empty;
            try
            {
                Utils cUtil = new Utils();
                DateTime dDateNow = DateTime.Now;

                string strMes = RetornaMesNumerosCorto(sFecha.Substring(2, 3).ToUpper(), "en");
                string strDia = sFecha.Substring(0, 2);
                string strYear = "20" + sFecha.Substring(5, 2);
                sFechaSabre = strYear + "/" + strMes + "/" + strDia;
                try
                {
                    DateTime dtSabre = DateTime.Parse(sFechaSabre);
                    if (dtSabre > dDateNow)
                    {
                        strYear = "19" + sFecha.Substring(5, 2);
                        sFechaSabre = strYear + "/" + strMes + "/" + strDia;
                    }
                }
                catch { }
            }
            catch
            {
                sFechaSabre = "1980/01/01";
            }
            return sFechaSabre;
        }
        public static string setFechaSabreNac(string sFecha)
        {
            // Se reciben fechas con solo formato yyyy/MM/dd
            string sFechaSabre = string.Empty;

            try
            {
                string[] strFecha = null;
                Utils cUtil = new Utils();
                if (sFecha.Contains("/"))
                {
                    strFecha = Lista(sFecha, "/");
                }
                if (sFecha.Contains("-"))
                {
                    strFecha = Lista(sFecha, "-");
                }
                string strMes = RetornaMesLetrasCorto(strFecha[1].ToString(), "en").ToUpper();
                if (strMes.Length.Equals(1))
                    strMes = "0" + strMes;

                string strDia = strFecha[2].ToString();
                if (strDia.Length.Equals(1))
                    strDia = "0" + strDia;

                string strYear = strFecha[0].ToString().Substring(2, 2);

                sFechaSabre = strDia + strMes + strYear;
            }
            catch
            {
                sFechaSabre = "01JAN";
            }
            return sFechaSabre;
        }
        /*VALIDA SI ES UN NUMERO, DEVUELVE BOOLEAN TRUE O FALSE*/
        public static bool IsNumeric(string strNumero)
        {
            bool IsNumeric = true;
            decimal Numero = 0;
            try
            {
                Numero = Convert.ToDecimal(strNumero);
            }
            catch
            {
                IsNumeric = false;
            }
            return IsNumeric;
        }
        public static bool bValidaFechas(TextBox txtYear, TextBox txtMes, TextBox txtDia)
        {
            bool bValida = true;
            string sYearActual = DateTime.Now.Year.ToString();
            try
            {
                if (txtDia != null)
                {
                    if (txtMes != null)
                    {
                        if (txtYear != null)
                        {
                            if (IS_INTEGER(txtDia.Text))
                            {
                                if (IS_INTEGER(txtMes.Text))
                                {
                                    if (IS_INTEGER(txtYear.Text))
                                    {
                                        string sYear = txtYear.Text;
                                        string sMes = txtMes.Text;
                                        string sDia = txtDia.Text;

                                        if (sMes.Length.Equals(1))
                                            sMes = "0" + sMes;

                                        if (sDia.Length.Equals(1))
                                            sDia = "0" + sDia;

                                        string sFechaNac = sYear + "/" + sMes + "/" + sDia;
                                        bValida = IS_FECHA(sFechaNac);
                                        if (bValida)
                                        {
                                            int iDiasMes = RetornaDiasMes(int.Parse(sMes), int.Parse(sYear));
                                            if (int.Parse(sDia) > iDiasMes)
                                            {
                                                bValida = false;
                                            }
                                            if (int.Parse(sMes) > 12)
                                            {
                                                bValida = false;
                                            }
                                            if (int.Parse(sYear) > int.Parse(sYearActual))
                                            {
                                                bValida = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        bValida = false;
                                    }
                                }
                                else
                                {
                                    bValida = false;
                                }
                            }
                            else
                            {
                                bValida = false;
                            }
                        }
                    }
                }
            }
            catch { bValida = false; }
            return bValida;
        }
        public static string[] FechaPax(string sFecha, string sFormatoFecha)
        {
            string sSeparador = SeparadorFecha();
            if (sFecha.Contains("/"))
                sSeparador = "/";
            if (sFecha.Contains("-"))
                sSeparador = "-";
            if (sFecha.Contains("."))
                sSeparador = ".";
            string sFechaFin = ConverFecha(sFecha, sFormatoFecha, sSeparador, Enum_FormatoFecha.YMD, sSeparador);
            string[] sFechaEnvia = Lista(sFechaFin, sSeparador);
            return sFechaEnvia;
        }
        public static string[] FechaPax(string sFecha)
        {
            string sSeparador = SeparadorFecha();
            string sFechaResult = sFecha;
            try
            {
                if (sFecha.Contains("/"))
                {
                    sSeparador = "/";
                }
                else
                {
                    if (sFecha.Contains("-"))
                    {
                        sSeparador = "-";
                    }
                    else
                    {
                        if (sFecha.Contains("."))
                        {
                            sSeparador = ".";
                        }
                        else
                        {
                            if (!IS_NUMERIC(sFecha))
                            {
                                sFechaResult = getFechaSabre(sFecha);
                                sSeparador = "/";
                            }
                        }
                    }
                }
            }
            catch
            {
                try
                {
                    sFechaResult = DateTime.Now.ToString(sFormatoFechaBD);
                    sSeparador = SeparadorFecha();
                }
                catch { }
            }
            string[] sFechaEnvia = Lista(sFechaResult, sSeparador);
            return sFechaEnvia;
        }
        #region[METODOS PARA RETORNAR LOS MESES DEL AÑO EN DIFERENTES FORMATOS]
        public static string RetornaMesLetrasCorto(string Mes)
        {
            string sMes = string.Empty;
            switch (Mes)
            {
                case "01":
                    sMes = "Ene";
                    break;

                case "1":
                    sMes = "Ene";
                    break;

                case "02":
                    sMes = "Feb";
                    break;

                case "2":
                    sMes = "Feb";
                    break;

                case "03":
                    sMes = "Mar";
                    break;

                case "3":
                    sMes = "Mar";
                    break;

                case "04":
                    sMes = "Abr";
                    break;

                case "4":
                    sMes = "Abr";
                    break;

                case "05":
                    sMes = "May";
                    break;

                case "5":
                    sMes = "May";
                    break;

                case "06":
                    sMes = "Jun";
                    break;

                case "6":
                    sMes = "Jun";
                    break;

                case "07":
                    sMes = "Jul";
                    break;

                case "7":
                    sMes = "Jul";
                    break;

                case "08":
                    sMes = "Ago";
                    break;

                case "8":
                    sMes = "Ago";
                    break;

                case "09":
                    sMes = "Sep";
                    break;

                case "9":
                    sMes = "Sep";
                    break;

                case "10":
                    sMes = "Oct";
                    break;

                case "11":
                    sMes = "Nov";
                    break;

                case "12":
                    sMes = "Dic";
                    break;
            }
            return sMes;
        }
        public static string RetornaMesLetrasCorto(string Mes, string strCultura)
        {
            string sMes = string.Empty;
            switch (Mes)
            {
                case "01":
                    sMes = "Jan";
                    break;

                case "1":
                    sMes = "Jan";
                    break;

                case "02":
                    sMes = "Feb";
                    break;

                case "2":
                    sMes = "Feb";
                    break;

                case "03":
                    sMes = "Mar";
                    break;

                case "3":
                    sMes = "Mar";
                    break;

                case "04":
                    sMes = "Apr";
                    break;

                case "4":
                    sMes = "Apr";
                    break;

                case "05":
                    sMes = "May";
                    break;

                case "5":
                    sMes = "May";
                    break;

                case "06":
                    sMes = "Jun";
                    break;

                case "6":
                    sMes = "Jun";
                    break;

                case "07":
                    sMes = "Jul";
                    break;

                case "7":
                    sMes = "Jul";
                    break;

                case "08":
                    sMes = "Aug";
                    break;

                case "8":
                    sMes = "Aug";
                    break;

                case "09":
                    sMes = "Sep";
                    break;

                case "9":
                    sMes = "Sep";
                    break;

                case "10":
                    sMes = "Oct";
                    break;

                case "11":
                    sMes = "Nov";
                    break;

                case "12":
                    sMes = "Dec";
                    break;
            }
            return sMes;
        }
        public static string RetornaMesLetrasLargo(string Mes)
        {
            string sMes = string.Empty;
            switch (Mes)
            {
                case "01":
                    sMes = "Enero";
                    break;

                case "1":
                    sMes = "Enero";
                    break;

                case "02":
                    sMes = "Febrero";
                    break;

                case "2":
                    sMes = "Febrero";
                    break;

                case "03":
                    sMes = "Marzo";
                    break;

                case "3":
                    sMes = "Marzo";
                    break;

                case "04":
                    sMes = "Abril";
                    break;

                case "4":
                    sMes = "Abril";
                    break;

                case "05":
                    sMes = "Mayo";
                    break;

                case "5":
                    sMes = "Mayo";
                    break;

                case "06":
                    sMes = "Junio";
                    break;

                case "6":
                    sMes = "Junio";
                    break;

                case "07":
                    sMes = "Julio";
                    break;

                case "7":
                    sMes = "Julio";
                    break;

                case "08":
                    sMes = "Agosto";
                    break;

                case "8":
                    sMes = "Agosto";
                    break;

                case "09":
                    sMes = "Septiembre";
                    break;

                case "9":
                    sMes = "Septiembre";
                    break;

                case "10":
                    sMes = "Octubre";
                    break;

                case "11":
                    sMes = "Noviembre";
                    break;

                case "12":
                    sMes = "Diciembre";
                    break;
            }
            return sMes;
        }
        public static string RetornaMesNumerosCorto(string Mes)
        {
            string sMes = string.Empty;
            switch (Mes.ToUpper())
            {
                case "ENE":
                    sMes = "01";
                    break;

                case "FEB":
                    sMes = "02";
                    break;

                case "MAR":
                    sMes = "03";
                    break;

                case "ABR":
                    sMes = "04";
                    break;

                case "MAY":
                    sMes = "05";
                    break;

                case "JUN":
                    sMes = "06";
                    break;

                case "JUL":
                    sMes = "07";
                    break;

                case "AGO":
                    sMes = "08";
                    break;

                case "SEP":
                    sMes = "09";
                    break;

                case "OCT":
                    sMes = "10";
                    break;

                case "NOV":
                    sMes = "11";
                    break;

                case "DIC":
                    sMes = "12";
                    break;
            }
            return sMes;
        }
        public static string RetornaMesNumerosCorto(string Mes, string Cultura)
        {
            string sMes = string.Empty;
            switch (Mes.ToUpper())
            {
                case "JAN":
                    sMes = "01";
                    break;

                case "FEB":
                    sMes = "02";
                    break;

                case "MAR":
                    sMes = "03";
                    break;

                case "APR":
                    sMes = "04";
                    break;

                case "MAY":
                    sMes = "05";
                    break;

                case "JUN":
                    sMes = "06";
                    break;

                case "JUL":
                    sMes = "07";
                    break;

                case "AUG":
                    sMes = "08";
                    break;

                case "SEP":
                    sMes = "09";
                    break;

                case "OCT":
                    sMes = "10";
                    break;

                case "NOV":
                    sMes = "11";
                    break;

                case "DEC":
                    sMes = "12";
                    break;
            }
            return sMes;
        }
        public static string RetornaMesNumerosLargo(string Mes)
        {
            string sMes = string.Empty;
            switch (Mes.ToUpper())
            {
                case "ENERO":
                    sMes = "01";
                    break;

                case "FEBRERO":
                    sMes = "02";
                    break;

                case "MARZO":
                    sMes = "03";
                    break;

                case "ABRIL":
                    sMes = "04";
                    break;

                case "MAYO":
                    sMes = "05";
                    break;

                case "JUNIO":
                    sMes = "06";
                    break;

                case "JULIO":
                    sMes = "07";
                    break;

                case "AGOSTO":
                    sMes = "08";
                    break;

                case "SEPTIEMBRE":
                    sMes = "09";
                    break;

                case "OCTUBRE":
                    sMes = "10";
                    break;

                case "NOVIEMBRE":
                    sMes = "11";
                    break;

                case "DICIEMBRE":
                    sMes = "12";
                    break;
            }
            return sMes;
        }
        #endregion
        #region[METODOS PARA CONVERSION DE FECHAS]
        public static string ConverFecha(string sFecha, string sFormatoFecha, string sSeparador, Enum_FormatoFecha eFormatoFechaRetorno, string sSeparadorRetorno)
        {
            string sFechaFin = sFecha;
            try
            {
                Utils cUtil = new Utils();
                string[] strFecha = null;
                if (sFecha.Contains(" "))
                {
                    strFecha = sFecha.Split(' ');
                }
                if (strFecha != null)
                {
                    sFecha = strFecha[0];
                }

                string[] strFechaF = null;
                if (sFecha.Contains(sSeparador))
                {
                    strFechaF = Lista(sFecha, sSeparador);
                }

                string[] strFechaFormat = null;
                if (sFormatoFecha.Contains(sSeparador))
                {
                    strFechaFormat = Lista(sFormatoFecha, sSeparador);
                }
                //if (sFecha.Contains("-"))
                //{
                //    strFechaFormat = sFecha.Split('-');
                //}

                int iD = 0;
                int iM = 1;
                int iY = 2;
                if (strFechaFormat[0].ToString().ToUpper().Contains("D"))
                {
                    iD = 0;
                }
                if (strFechaFormat[1].ToString().ToUpper().Contains("D"))
                {
                    iD = 1;
                }
                if (strFechaFormat[2].ToString().ToUpper().Contains("D"))
                {
                    iD = 2;
                }

                if (strFechaFormat[0].ToString().ToUpper().Contains("M"))
                {
                    iM = 0;
                }
                if (strFechaFormat[1].ToString().ToUpper().Contains("M"))
                {
                    iM = 1;
                }
                if (strFechaFormat[2].ToString().ToUpper().Contains("M"))
                {
                    iM = 2;
                }

                if (strFechaFormat[0].ToString().ToUpper().Contains("Y"))
                {
                    iY = 0;
                }
                if (strFechaFormat[1].ToString().ToUpper().Contains("Y"))
                {
                    iY = 1;
                }
                if (strFechaFormat[2].ToString().ToUpper().Contains("Y"))
                {
                    iY = 2;
                }

                string sD = strFechaF[iD];
                string sM = strFechaF[iM];
                string sY = strFechaF[iY];

                if (sD.Length.Equals(1))
                {
                    sD = "0" + sD;
                }

                if (sM.Length.Equals(1))
                {
                    sM = "0" + sM;
                }
                if (sY.Length.Equals(2))
                {
                    sY = "20" + sY;
                }
                else
                {
                    sY = sY.Substring(0, 4);
                }
                sFechaFin = sY + sSeparadorRetorno + sM + sSeparadorRetorno + sD;
                bool bHora = false;
                switch (eFormatoFechaRetorno)
                {
                    case Enum_FormatoFecha.DMY:
                        sFechaFin = sD + sSeparadorRetorno + sM + sSeparadorRetorno + sY;
                        break;

                    case Enum_FormatoFecha.MDY:
                        sFechaFin = sM + sSeparadorRetorno + sD + sSeparadorRetorno + sY;
                        break;

                    case Enum_FormatoFecha.YDM:
                        sFechaFin = sY + sSeparadorRetorno + sD + sSeparadorRetorno + sM;
                        break;

                    case Enum_FormatoFecha.YMD:
                        sFechaFin = sY + sSeparadorRetorno + sM + sSeparadorRetorno + sD;
                        break;

                    case Enum_FormatoFecha.DYM:
                        sFechaFin = sD + sSeparadorRetorno + sY + sSeparadorRetorno + sM;
                        break;

                    case Enum_FormatoFecha.MYD:
                        sFechaFin = sM + sSeparadorRetorno + sY + sSeparadorRetorno + sD;
                        break;

                    case Enum_FormatoFecha.YMD_:
                        sFechaFin = sY + sM + sD;
                        break;

                    case Enum_FormatoFecha.DMYH:
                        sFechaFin = sD + sSeparadorRetorno + sM + sSeparadorRetorno + sY;
                        bHora = true;
                        break;

                    case Enum_FormatoFecha.MDYH:
                        sFechaFin = sM + sSeparadorRetorno + sD + sSeparadorRetorno + sY;
                        bHora = true;
                        break;

                    case Enum_FormatoFecha.YDMH:
                        sFechaFin = sY + sSeparadorRetorno + sD + sSeparadorRetorno + sM;
                        bHora = true;
                        break;

                    case Enum_FormatoFecha.YMDH:
                        sFechaFin = sY + sSeparadorRetorno + sM + sSeparadorRetorno + sD;
                        bHora = true;
                        break;

                    case Enum_FormatoFecha.DYMH:
                        sFechaFin = sD + sSeparadorRetorno + sY + sSeparadorRetorno + sM;
                        bHora = true;
                        break;

                    case Enum_FormatoFecha.MYDH:
                        sFechaFin = sM + sSeparadorRetorno + sY + sSeparadorRetorno + sD;
                        bHora = true;
                        break;

                    case Enum_FormatoFecha.YMDH_:
                        sFechaFin = sY + sM + sD;
                        bHora = true;
                        break;
                }
                try
                {
                    if (bHora)
                    {
                        if (strFecha.Length > 1)
                            sFechaFin += " " + strFecha[1];
                        if (strFecha.Length > 2)
                            sFechaFin += " " + strFecha[2];
                    }
                }
                catch { }
            }
            catch { }
            return sFechaFin;
        }
        public static string ConverFecha(string sFecha, Enum_FormatoFecha eFormatoFecha, string sSeparador, Enum_FormatoFecha eFormatoFechaRetorno, string sSeparadorRetorno)
        {
            string sFechaFin = sFecha;
            try
            {
                string[] strFecha = null;
                if (sFecha.Contains(" "))
                {
                    strFecha = sFecha.Split(' ');
                }
                if (strFecha != null)
                {
                    sFecha = strFecha[0];
                }
                string[] strFechaF = null;
                if (sFecha.Contains(sSeparador))
                {
                    strFechaF = Lista(sFecha, sSeparador);
                }

                int iD = 0;
                int iM = 1;
                int iY = 2;

                switch (eFormatoFecha)
                {
                    case Enum_FormatoFecha.DMY:
                        iD = 0;
                        iM = 1;
                        iY = 2;
                        break;

                    case Enum_FormatoFecha.MDY:
                        iD = 1;
                        iM = 0;
                        iY = 2;
                        break;

                    case Enum_FormatoFecha.YDM:
                        iD = 1;
                        iM = 2;
                        iY = 0;
                        break;

                    case Enum_FormatoFecha.YMD:
                        iD = 2;
                        iM = 1;
                        iY = 0;
                        break;

                    case Enum_FormatoFecha.DYM:
                        iD = 0;
                        iM = 2;
                        iY = 1;
                        break;

                    case Enum_FormatoFecha.MYD:
                        iD = 2;
                        iM = 0;
                        iY = 1;
                        break;
                }

                string sD = strFechaF[iD];
                string sM = strFechaF[iM];
                string sY = strFechaF[iY];

                if (sD.Length.Equals(1))
                {
                    sD = "0" + sD;
                }

                if (sM.Length.Equals(1))
                {
                    sM = "0" + sM;
                }
                if (sY.Length.Equals(2))
                {
                    sY = "20" + sY;
                }
                else
                {
                    sY = sY.Substring(0, 4);
                }
                sFechaFin = sY + sSeparadorRetorno + sM + sSeparadorRetorno + sD;
                bool bHora = false;

                switch (eFormatoFechaRetorno)
                {
                    case Enum_FormatoFecha.DMY:
                        sFechaFin = sD + sSeparadorRetorno + sM + sSeparadorRetorno + sY;
                        break;

                    case Enum_FormatoFecha.MDY:
                        sFechaFin = sM + sSeparadorRetorno + sD + sSeparadorRetorno + sY;
                        break;

                    case Enum_FormatoFecha.YDM:
                        sFechaFin = sY + sSeparadorRetorno + sD + sSeparadorRetorno + sM;
                        break;

                    case Enum_FormatoFecha.YMD:
                        sFechaFin = sY + sSeparadorRetorno + sM + sSeparadorRetorno + sD;
                        break;

                    case Enum_FormatoFecha.DYM:
                        sFechaFin = sD + sSeparadorRetorno + sY + sSeparadorRetorno + sM;
                        break;

                    case Enum_FormatoFecha.MYD:
                        sFechaFin = sM + sSeparadorRetorno + sY + sSeparadorRetorno + sD;
                        break;

                    case Enum_FormatoFecha.YMD_:
                        sFechaFin = sY + sM + sD;
                        break;

                    case Enum_FormatoFecha.DMYH:
                        sFechaFin = sD + sSeparadorRetorno + sM + sSeparadorRetorno + sY;
                        bHora = true;
                        break;

                    case Enum_FormatoFecha.MDYH:
                        sFechaFin = sM + sSeparadorRetorno + sD + sSeparadorRetorno + sY;
                        bHora = true;
                        break;

                    case Enum_FormatoFecha.YDMH:
                        sFechaFin = sY + sSeparadorRetorno + sD + sSeparadorRetorno + sM;
                        bHora = true;
                        break;

                    case Enum_FormatoFecha.YMDH:
                        sFechaFin = sY + sSeparadorRetorno + sM + sSeparadorRetorno + sD;
                        bHora = true;
                        break;

                    case Enum_FormatoFecha.DYMH:
                        sFechaFin = sD + sSeparadorRetorno + sY + sSeparadorRetorno + sM;
                        bHora = true;
                        break;

                    case Enum_FormatoFecha.MYDH:
                        sFechaFin = sM + sSeparadorRetorno + sY + sSeparadorRetorno + sD;
                        bHora = true;
                        break;

                    case Enum_FormatoFecha.YMDH_:
                        sFechaFin = sY + sM + sD;
                        bHora = true;
                        break;
                }
                try
                {
                    if (bHora)
                    {
                        if (strFecha.Length > 1)
                            sFechaFin += " " + strFecha[1];
                        if (strFecha.Length > 2)
                            sFechaFin += " " + strFecha[2];
                    }
                }
                catch { }
            }
            catch { }
            return sFechaFin;
        }
        public static string ConverFecha(string sFecha, Enum_FormatoFecha eFormatoFechaRetorno)
        {
            string sFechaFin = sFecha;
            try
            {
                string sFormatoFecha = FormatoFechaShort();
                string sSeparador = SeparadorFecha();
                sFechaFin = ConverFecha(sFecha, sFormatoFecha, sSeparador, eFormatoFechaRetorno, sSeparador);
            }
            catch { }
            return sFechaFin;
        }
        public static string ConverFecha(string sFecha, Enum_FormatoFecha eFormatoFechaRetorno, string sSeparadorRetorno)
        {
            string sFechaFin = sFecha;
            try
            {
                string sFormatoFecha = FormatoFechaShort();
                string sSeparador = SeparadorFecha();
                sFechaFin = ConverFecha(sFecha, sFormatoFecha, sSeparador, eFormatoFechaRetorno, sSeparadorRetorno);
            }
            catch { }
            return sFechaFin;
        }
        public static string ConverFecha(string sFecha, string sFormatoFecha, Enum_FormatoFecha eFormatoFechaRetorno)
        {
            string sFechaFin = sFecha;
            try
            {
                string sFechaTemp = sFecha;
                if (sFecha.Length > 10)
                {
                    sFechaTemp = sFecha.Substring(0, 10);
                }
                string sSeparador = SeparadorFecha();
                if (sFechaTemp.Contains("/"))
                    sSeparador = "/";
                if (sFechaTemp.Contains("-"))
                    sSeparador = "-";
                if (sFechaTemp.Contains("."))
                    sSeparador = ".";
                sFechaFin = ConverFecha(sFecha, sFormatoFecha, sSeparador, eFormatoFechaRetorno, sSeparador);
            }
            catch { }
            return sFechaFin;
        }
        public static string ConverFecha(string sFecha, string sFormatoFecha, string sFormatoFechaRetorno)
        {
            string sFechaFin = sFecha;
            try
            {
                Enum_FormatoFecha eFormatoFechaRetorno = new Enum_FormatoFecha();
                eFormatoFechaRetorno = Enum_FormatoFecha.YDM;
                string sFechaTemp = sFecha;
                if (sFecha.Length > 10)
                {
                    sFechaTemp = sFecha.Substring(0, 10);
                }

                string sSeparador = SeparadorFecha();
                if (sFechaTemp.Contains("/"))
                    sSeparador = "/";
                if (sFechaTemp.Contains("-"))
                    sSeparador = "-";
                if (sFechaTemp.Contains("."))
                    sSeparador = ".";

                switch (sFormatoFechaRetorno.ToUpper())
                {
                    case "DD/MM/YYYY":
                        eFormatoFechaRetorno = Enum_FormatoFecha.DMY;
                        break;

                    case "MM/DD/YYYY":
                        eFormatoFechaRetorno = Enum_FormatoFecha.MDY;
                        break;

                    case "YYYY/MM/DD":
                        eFormatoFechaRetorno = Enum_FormatoFecha.YMD;
                        break;

                    case "YYYY/DD/MM":
                        eFormatoFechaRetorno = Enum_FormatoFecha.YDM;
                        break;

                    case "DD/YYYY/MM":
                        eFormatoFechaRetorno = Enum_FormatoFecha.DYM;
                        break;

                    case "MM/YYYY/DD":
                        eFormatoFechaRetorno = Enum_FormatoFecha.MYD;
                        break;

                    case "YYYYMMDD":
                        eFormatoFechaRetorno = Enum_FormatoFecha.YMD_;
                        break;

                    case "DD/MM/YYYY:H":
                        eFormatoFechaRetorno = Enum_FormatoFecha.DMYH;
                        break;

                    case "MM/DD/YYYY:H":
                        eFormatoFechaRetorno = Enum_FormatoFecha.MDYH;
                        break;

                    case "YYYY/MM/DD:H":
                        eFormatoFechaRetorno = Enum_FormatoFecha.YMDH;
                        break;

                    case "YYYY/DD/MM:H":
                        eFormatoFechaRetorno = Enum_FormatoFecha.YDMH;
                        break;

                    case "DD/YYYY/MM:H":
                        eFormatoFechaRetorno = Enum_FormatoFecha.DYMH;
                        break;

                    case "MM/YYYY/DD:H":
                        eFormatoFechaRetorno = Enum_FormatoFecha.MYDH;
                        break;

                    case "YYYYMMDD:H":
                        eFormatoFechaRetorno = Enum_FormatoFecha.YMDH_;
                        break;
                }
                sFechaFin = ConverFecha(sFecha, sFormatoFecha, sSeparador, eFormatoFechaRetorno, sSeparador);
            }
            catch { }
            return sFechaFin;
        }
        /// <summary>
        /// Convierte un fecha especifica al formato de fecha del sistema, utilizando un metodo adicional
        /// </summary>
        /// <param name="sFecha">Fecha enviada</param>
        /// <param name="eFormatoFecha">Enumerado del formato de fecha que se envia</param>
        /// <returns></returns>
        public static DateTime ConverFechaDateTime(string sFecha, Enum_FormatoFecha eFormatoFecha)
        {
            DateTime FechaSel = DateTime.Now;
            try
            {
                FechaSel = Convert.ToDateTime(ConverFechaDate(sFecha, eFormatoFecha));
            }
            catch
            {
            }
            return FechaSel;
        }
        /// <summary>
        /// Convierte un fecha especifica al formato de fecha del sistema
        /// </summary>
        /// <param name="sFecha">Fecha enviada</param>
        /// <param name="eFormatoFecha">Enumerado del formato de fecha que se envia</param>
        /// <returns></returns>
        public static string ConverFechaDate(string sFecha, Enum_FormatoFecha eFormatoFecha)
        {
            string sFechaFin = sFecha;
            try
            {
                Enum_FormatoFecha eFormatoFechaRetorno = FormatoFecha();
                string sSeparador = SeparadorFecha();
                sFechaFin = ConverFecha(sFecha, eFormatoFecha, sSeparador, eFormatoFechaRetorno, sSeparador);
            }
            catch { }
            return sFechaFin;
        }
        /// <summary>
        /// Convierte el formato de fecha del sistema al formato de fecha que se requiere, proceso contrario a ConverFechaDate
        /// </summary>
        /// <param name="sFecha">Fecha que se envia</param>
        /// <param name="eFormatoFechaRetorno">Formato de retorno, si no se envia separador, el sistema toma el separador por default</param>
        /// <returns></returns>
        public static string ConverFechaDateRetorna(string sFecha, Enum_FormatoFecha eFormatoFechaRetorno)
        {
            string sFechaFin = sFecha;
            try
            {
                Enum_FormatoFecha eFormatoFecha = FormatoFecha();
                string sSeparador = SeparadorFecha();
                sFechaFin = ConverFecha(sFecha, eFormatoFecha, sSeparador, eFormatoFechaRetorno, sSeparador);
            }
            catch { }
            return sFechaFin;
        }
        public static string ConverFechaSinSeparadorYMD(string sFecha)
        {
            string sFechaFin = sFecha;
            try
            {
                string sSeparador = SeparadorFecha();
                sFechaFin = sFecha.Substring(0, 4) + sSeparador + sFecha.Substring(4, 2) + sSeparador + sFecha.Substring(6, 2);
            }
            catch { }
            return sFechaFin;
        }
        /// <summary>
        /// Convierte el formato de fecha del sistema al formato de fecha que se requiere, proceso contrario a ConverFechaDate
        /// </summary>
        /// <param name="sFecha">Fecha que se envia</param>
        /// <param name="eFormatoFechaRetorno">Formato de retorno</param>
        /// <param name="sSeparadorRetorna">Separador de retorno</param>
        /// <returns></returns>
        public static string ConverFechaDateRetorna(string sFecha, Enum_FormatoFecha eFormatoFechaRetorno, string sSeparadorRetorna)
        {
            Enum_FormatoFecha eFormatoFecha = FormatoFecha();
            string sSeparador = SeparadorFecha();
            string sFechaFin = ConverFecha(sFecha, eFormatoFecha, sSeparador, eFormatoFechaRetorno, sSeparadorRetorna);
            return sFechaFin;
        }
        public static string ConverYMD(string sFecha)
        {
            string sFormatoFecha = FormatoFechaShort();
            string sSeparador = SeparadorFecha();
            string sFechaFin = ConverFecha(sFecha, sFormatoFecha, sSeparador, Enum_FormatoFecha.YMD, sSeparador);
            return sFechaFin;
        }
        public static string ConverYMD(string sFecha, string sFormatoFecha)
        {
            string sSeparador = "/";
            if (sFecha.Contains("-"))
            {
                sSeparador = "-";
            }
            if (sFecha.Contains("."))
            {
                sSeparador = ".";
            }
            string sFechaFin = ConverFecha(sFecha, sFormatoFecha, sSeparador, Enum_FormatoFecha.YMD, sSeparador);
            return sFechaFin;
        }
        public static string ConverYMD(string sFecha, string sFormatoFecha, string sSeparadorRetorno)
        {
            string sSeparador = "/";
            if (sFecha.Contains("-"))
            {
                sSeparador = "-";
            }
            string sFechaFin = ConverFecha(sFecha, sFormatoFecha, sSeparador, Enum_FormatoFecha.YMD, sSeparadorRetorno);
            return sFechaFin;
        }
        public static string ConverYMDFormato(string sFecha, string sFormatoFecha)
        {
            string sSeparador = "/";
            if (sFecha.Contains("-"))
            {
                sSeparador = "-";
            }
            int iEspacio = 0;

            if (sFecha.Contains(" "))
            {
                iEspacio = sFecha.IndexOf(" ");
                sFecha = sFecha.Remove(iEspacio);
            }
            string[] sListaFecha = null;
            string[] sListaFechaRet = null;

            string sDay = string.Empty;
            string sMonth = string.Empty;
            string sYear = string.Empty;

            string sFechaRetorna = sFecha;

            if (sFormatoFecha.Contains(sSeparador))
            {
                sListaFecha = sFormatoFecha.Split(new char[] { Convert.ToChar(sSeparador) });
                sListaFechaRet = sFecha.Split(new char[] { Convert.ToChar(sSeparador) });
            }
            if (sListaFechaRet[0].Length.Equals(2))
            {
                sYear = "20" + sListaFechaRet[0].Trim();
            }
            else
            {
                sYear = sListaFechaRet[0].Trim();
            }
            if (sListaFechaRet[1].Length.Equals(1))
            {
                sMonth = "0" + sListaFechaRet[1].Trim();
            }
            else
            {
                sMonth = sListaFechaRet[1].Trim();
            }
            if (sListaFechaRet[2].Length.Equals(1))
            {
                sDay = "0" + sListaFechaRet[2].Trim();
            }
            else
            {
                sDay = sListaFechaRet[2].Trim();
            }
            Enum_FormatoFecha eFormato = FormatoFecha(sFormatoFecha);

            switch (eFormato)
            {
                case Enum_FormatoFecha.DMY:
                    sFechaRetorna = sDay + sSeparador + sMonth + sSeparador + sYear;
                    break;

                case Enum_FormatoFecha.MDY:
                    sFechaRetorna = sMonth + sSeparador + sDay + sSeparador + sYear;
                    break;

                case Enum_FormatoFecha.YMD:
                    sFechaRetorna = sYear + sSeparador + sMonth + sSeparador + sDay;
                    break;

                case Enum_FormatoFecha.YDM:
                    sFechaRetorna = sYear + sSeparador + sDay + sSeparador + sMonth;
                    break;
            }
            return sFechaRetorna;
        }
        public static string Converymd_MDY(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(4, 2) + sSeparador + sFecha.Substring(6, 2) + sSeparador + sFecha.Substring(0, 4);
            return strFecha;
        }
        public static string Converymd_DMY(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(6, 2) + sSeparador + sFecha.Substring(4, 2) + sSeparador + sFecha.Substring(0, 4);
            return strFecha;
        }
        public static string Converymd_YMD(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(0, 4) + sSeparador + sFecha.Substring(4, 2) + sSeparador + sFecha.Substring(6, 2);
            return strFecha;
        }
        public static string Converymd_DMMY(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(6, 2) + sSeparador + RetornaMesLetrasCorto(sFecha.Substring(4, 2)) + sSeparador + sFecha.Substring(0, 4);
            return strFecha;
        }
        public static string ConverDMY_DMMMY(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(0, 2) + sSeparador + RetornaMesLetrasLargo(sFecha.Substring(3, 2)) + sSeparador + sFecha.Substring(6, 4);
            return strFecha;
        }
        public static string ConverDMMYtoYMD(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(7, 4) + sSeparador + RetornaMesNumerosCorto(sFecha.Substring(3, 3)) + sSeparador + sFecha.Substring(0, 2);
            return strFecha;
        }
        public static string ConverDMMYtoDMMY(string sFecha)
        {
            string strFecha = sFecha.Substring(0, 2) + sFecha.Substring(3, 3) + sFecha.Substring(9, 2);
            return strFecha;
        }
        public static string ConverYMDtoDMMY(string sFecha, string sSeparador)
        {
            // busca error
            string strFecha = sFecha.Substring(8, 2) + sSeparador + RetornaMesLetrasCorto(sFecha.Substring(5, 2)) + sSeparador + sFecha.Substring(0, 4);

            // string sMensajeEror = "ConverYMDtoDMMY:      Fecha envia " + sFecha + "   fecha retorna  " + strFecha;

            //ExceptionHandled.Publicar(sMensajeEror);


            return strFecha;
        }
        public static string ConverDMYtoYMD(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(6, 4) + sSeparador + sFecha.Substring(3, 2) + sSeparador + sFecha.Substring(0, 2);
            return strFecha;
        }
        public static string Convert_DMYtoYMD(string sFecha, string sSeparador)
        {
            string[] arr_strFecha = sFecha.Split(Convert.ToChar(sSeparador));
            return arr_strFecha[2] + sSeparador + arr_strFecha[1] + sSeparador + arr_strFecha[0];
        }
        public static string ConverMDYtoYMD(string sFecha, string sSeparador)
        {
            // busca error
            string[] strFecha = null;
            if (sFecha.Contains("/"))
            {
                strFecha = sFecha.Split('/');
            }
            if (sFecha.Contains("-"))
            {
                strFecha = sFecha.Split('-');
            }
            string sFechaRetorna = strFecha[2] + sSeparador + strFecha[0] + sSeparador + strFecha[1];
            return sFechaRetorna;
        }
        public static string ConverMDYtoYMD(string sFecha)
        {
            // busca error
            string[] strFecha = null;
            if (sFecha.Contains("/"))
            {
                strFecha = sFecha.Split('/');
            }
            if (sFecha.Contains("-"))
            {
                strFecha = sFecha.Split('-');
            }

            string sFechaRetorna = strFecha[2] + strFecha[0] + strFecha[1];

            return sFechaRetorna;

        }
        public static string ConverMDYtoDMMY(string sFecha)
        {
            string[] strFecha = null;
            if (sFecha.Contains("/"))
            {
                strFecha = sFecha.Split('/');
            }
            if (sFecha.Contains("-"))
            {
                strFecha = sFecha.Split('-');
            }

            string sFechaRetorna = strFecha[1] + RetornaMesLetrasCorto(strFecha[0], "EN") + strFecha[2].Substring(2);

            return sFechaRetorna;

        }
        public static string ConverYDMtoYMD(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(0, 4) + sSeparador + sFecha.Substring(8, 2) + sSeparador + sFecha.Substring(5, 2);
            return strFecha;
        }
        public static string ConverYMDtoDMY(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(8, 2) + sSeparador + sFecha.Substring(5, 2) + sSeparador + sFecha.Substring(0, 4);
            return strFecha;
        }
        public static string ConverYMDtoYDM(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(0, 4) + sSeparador + sFecha.Substring(5, 2) + sSeparador + sFecha.Substring(8, 2);
            return strFecha;
        }
        public static string ConverYMDtoYMD(string sFecha)
        {
            string strFecha = sFecha.Substring(0, 4) + SeparadorFecha() + sFecha.Substring(4, 2) + SeparadorFecha() + sFecha.Substring(6, 2);
            return strFecha;
        }
        public static string ConverMDYtoDMY(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(3, 2) + sSeparador + sFecha.Substring(0, 2) + sSeparador + sFecha.Substring(6, 4);
            return strFecha;
        }
        public static string ConverYMDtoMDY(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(5, 2) + sSeparador + sFecha.Substring(8, 2) + sSeparador + sFecha.Substring(0, 4);
            return strFecha;
        }
        public static string ConverYMDtoMDY(string sFecha)
        {
            string strFecha = sFecha.Substring(4, 2) + "/" + sFecha.Substring(6, 2) + "/" + sFecha.Substring(0, 4);
            return strFecha;
        }
        public static string ConverDMYtoMDY(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(3, 2) + sSeparador + sFecha.Substring(0, 2) + sSeparador + sFecha.Substring(6, 4);
            return strFecha;
        }
        public static DateTime ConverMDYtoDMY_Dt(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(3, 2) + sSeparador + sFecha.Substring(0, 2) + sSeparador + sFecha.Substring(6, 4);
            return Convert.ToDateTime(strFecha);
        }
        public static DateTime ConverDMYtoDMY_Dt(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(0, 2) + sSeparador + sFecha.Substring(3, 2) + sSeparador + sFecha.Substring(6, 4);
            return Convert.ToDateTime(strFecha);
        }
        public static DateTime ConverYMDtoDMY_Dt(string sFecha, string sSeparador)
        {
            string strFecha = sFecha.Substring(8, 2) + sSeparador + sFecha.Substring(5, 2) + sSeparador + sFecha.Substring(0, 4);
            return Convert.ToDateTime(strFecha);
        }
        /// <summary>
        /// Formato de fecha del servidor
        /// </summary>
        /// <returns>Retorna enumerado del formato de fecha que tiene el servidor</returns>
        public static Enum_FormatoFecha FormatoFecha()
        {
            string sFecha = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            return FormatoFecha(sFecha);
        }
        /// <summary>
        /// Formato de fecha del cliente
        /// </summary>
        /// <returns>Retorna enumerado del formato de fecha que tiene el servidor</returns>
        public static Enum_FormatoFecha FormatoFecha(string sFormatoFecha)
        {
            Enum_FormatoFecha eFormato = new Enum_FormatoFecha();
            string sSeparador = SeparadorFecha(sFormatoFecha);
            string[] strFechaFormat = null;

            if (sFormatoFecha.Contains(sSeparador))
            {
                strFechaFormat = Lista(sFormatoFecha, sSeparador);
            }

            int iD = 0;
            int iM = 1;
            int iY = 2;
            if (strFechaFormat[0].ToString().ToUpper().Contains("D"))
            {
                iD = 0;
            }
            if (strFechaFormat[1].ToString().ToUpper().Contains("D"))
            {
                iD = 1;
            }
            if (strFechaFormat[2].ToString().ToUpper().Contains("D"))
            {
                iD = 2;
            }

            if (strFechaFormat[0].ToString().ToUpper().Contains("M"))
            {
                iM = 0;
            }
            if (strFechaFormat[1].ToString().ToUpper().Contains("M"))
            {
                iM = 1;
            }
            if (strFechaFormat[2].ToString().ToUpper().Contains("M"))
            {
                iM = 2;
            }

            if (strFechaFormat[0].ToString().ToUpper().Contains("Y"))
            {
                iY = 0;
            }
            if (strFechaFormat[1].ToString().ToUpper().Contains("Y"))
            {
                iY = 1;
            }
            if (strFechaFormat[2].ToString().ToUpper().Contains("Y"))
            {
                iY = 2;
            }

            //string sD = "DD";
            //string sM = "MM";
            //string sY = "YYYY";

            string sFormatoFinal = "YYYY/MM/DD";

            if (iD.Equals(0))
            {
                if (iM.Equals(1))
                {
                    sFormatoFinal = "DD/MM/YYYY";
                }
                else
                {
                    sFormatoFinal = "DD/YYYY/MM";
                }
            }
            else
            {
                if (iD.Equals(1))
                {
                    if (iM.Equals(0))
                    {
                        sFormatoFinal = "MM/DD/YYYY";
                    }
                    else
                    {
                        sFormatoFinal = "YYYY/DD/MM";
                    }
                }
                else
                {
                    if (iM.Equals(0))
                    {
                        sFormatoFinal = "MM/YYYY/DD";
                    }
                    else
                    {
                        sFormatoFinal = "YYYY/MM/DD";
                    }
                }
            }

            switch (sFormatoFinal.ToUpper())
            {
                case "DD/MM/YYYY":
                    eFormato = Enum_FormatoFecha.DMY;
                    break;

                case "MM/DD/YYYY":
                    eFormato = Enum_FormatoFecha.MDY;
                    break;

                case "YYYY/MM/DD":
                    eFormato = Enum_FormatoFecha.YMD;
                    break;

                case "YYYY/DD/MM":
                    eFormato = Enum_FormatoFecha.YDM;
                    break;

                case "DD/YYYY/MM":
                    eFormato = Enum_FormatoFecha.DYM;
                    break;

                case "MM/YYYY/DD":
                    eFormato = Enum_FormatoFecha.MYD;
                    break;
            }
            return eFormato;
        }
        public static string SeparadorFecha(string sFecha)
        {
            string sSeparador = "/";
            if (sFecha.Contains("-"))
            {
                sSeparador = "-";
            }
            if (sFecha.Contains("/"))
            {
                sSeparador = "/";
            }
            if (sFecha.Contains("."))
            {
                sSeparador = ".";
            }
            return sSeparador;
        }
        public static string FormatoFechaS(string sFormatoFecha)
        {
            string sSeparador = "/";
            string[] strFechaFormat = null;

            if (sFormatoFecha.Contains("-"))
            {
                sSeparador = "-";
            }
            if (sFormatoFecha.Contains("/"))
            {
                sSeparador = "/";
            }
            if (sFormatoFecha.Contains("."))
            {
                sSeparador = ".";
            }
            if (sFormatoFecha.Contains(sSeparador))
            {
                strFechaFormat = Lista(sFormatoFecha, sSeparador);
            }

            int iD = 0;
            int iM = 1;
            int iY = 2;
            if (strFechaFormat[0].ToString().ToUpper().Contains("D"))
            {
                iD = 0;
            }
            if (strFechaFormat[1].ToString().ToUpper().Contains("D"))
            {
                iD = 1;
            }
            if (strFechaFormat[2].ToString().ToUpper().Contains("D"))
            {
                iD = 2;
            }

            if (strFechaFormat[0].ToString().ToUpper().Contains("M"))
            {
                iM = 0;
            }
            if (strFechaFormat[1].ToString().ToUpper().Contains("M"))
            {
                iM = 1;
            }
            if (strFechaFormat[2].ToString().ToUpper().Contains("M"))
            {
                iM = 2;
            }

            if (strFechaFormat[0].ToString().ToUpper().Contains("Y"))
            {
                iY = 0;
            }
            if (strFechaFormat[1].ToString().ToUpper().Contains("Y"))
            {
                iY = 1;
            }
            if (strFechaFormat[2].ToString().ToUpper().Contains("Y"))
            {
                iY = 2;
            }

            string sFormatoFinal = "YYYY/MM/DD";

            if (iD.Equals(0))
            {
                if (iM.Equals(1))
                {
                    sFormatoFinal = "DD/MM/YYYY";
                }
                else
                {
                    sFormatoFinal = "DD/YYYY/MM";
                }
            }
            else
            {
                if (iD.Equals(1))
                {
                    if (iM.Equals(0))
                    {
                        sFormatoFinal = "MM/DD/YYYY";
                    }
                    else
                    {
                        sFormatoFinal = "YYYY/DD/MM";
                    }
                }
                else
                {
                    if (iM.Equals(0))
                    {
                        sFormatoFinal = "MM/YYYY/DD";
                    }
                    else
                    {
                        sFormatoFinal = "YYYY/MM/DD";
                    }
                }
            }
            return sFormatoFinal;
        }
        /// <summary>
        /// Formato de fecha del sistema
        /// </summary>
        /// <returns>Retorna enumerado del formato de fecha del sistema</returns>
        public static Enum_FormatoDecimal FormatoDecimal()
        {
            Enum_FormatoDecimal eFormato = new Enum_FormatoDecimal();
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (decimalSeparator.Equals(","))
                eFormato = Enum_FormatoDecimal.Coma;
            else
                eFormato = Enum_FormatoDecimal.Punto;
            return eFormato;
        }

        #endregion
        #region[METODOS PARA REDONDEAR VALORES]
        public static int Redondeo(double dValor)
        {
            double dValorNuevo = dValor;
            double dValorRedondeo = dValor;
            try
            {
                dValorRedondeo = Convert.ToDouble(dValorNuevo.ToString("###,##0"));

                if (dValorRedondeo < dValorNuevo)
                {
                    dValorNuevo = dValorRedondeo + 1;
                }
                dValorNuevo = Convert.ToDouble(dValorNuevo.ToString("###,##0"));
                return int.Parse(dValorNuevo.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static int Redondeo(Decimal dValor)
        {
            decimal dValorNuevo = dValor;
            decimal dValorRedondeo = dValor;
            try
            {
                dValorRedondeo = Convert.ToDecimal(dValorNuevo);

                if (dValorRedondeo < dValorNuevo)
                {
                    dValorNuevo = dValorRedondeo + 1;
                }
                dValorNuevo = Convert.ToDecimal(dValorNuevo);
                return int.Parse(dValorNuevo.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion
        public static String ModificarPrecios(String sPrecioAux)
        {
            String sPrecio = "";
            sPrecio = sPrecioAux;
            String[] sPrecioNuevo = new String[sPrecio.Length];
            String sPrecioFinal = "";
            try
            {
                int i = 0;
                int ii = 1;

                while (i < sPrecio.Length)
                {
                    sPrecioNuevo[i] = sPrecio.Substring(i, ii);
                    i++;
                }

                sPrecioFinal = sPrecioNuevo[0];
                i = 1;

                while (i < sPrecio.Length)
                {
                    if (i == sPrecio.Length - 3 || i == sPrecio.Length - 6)
                    {
                        sPrecioFinal = sPrecioFinal + "," + sPrecioNuevo[i];
                    }
                    else
                    {
                        sPrecioFinal = sPrecioFinal + sPrecioNuevo[i];
                    }
                    i++;
                }
            }
            catch { }
            return sPrecioFinal;
        }
        public static int getDiasDiferencia(string sFechaInicial, string sFechafinal)
        {
            DateTime dFechaFinal;
            DateTime dFechaInicial;
            TimeSpan diffResult;
            int iDiferencia;

            iDiferencia = 0;

            try
            {
                dFechaFinal = DateTime.Parse(sFechafinal);
                dFechaInicial = DateTime.Parse(sFechaInicial);

                diffResult = dFechaFinal - dFechaInicial;

                iDiferencia = diffResult.Days + 1;

            }
            catch
            {
            }
            return iDiferencia;

        }
        public static int getNochesDiferencia(string sFechaInicial, string sFechafinal)
        {
            DateTime dFechaFinal;
            DateTime dFechaInicial;
            TimeSpan diffResult;
            int iDiferencia;

            iDiferencia = 0;

            try
            {
                dFechaFinal = DateTime.Parse(sFechafinal);
                dFechaInicial = DateTime.Parse(sFechaInicial);

                diffResult = dFechaFinal - dFechaInicial;

                iDiferencia = diffResult.Days;

            }
            catch
            {
            }
            return iDiferencia;
        }
        public static int getDiferenciaHoras(string sFechaInicial, string sFechafinal)
        {
            int iDiferencia = 0;
            try
            {
                DateTime dFechaFinal = DateTime.Parse(sFechafinal);
                DateTime dFechaInicial = DateTime.Parse(sFechaInicial);
                TimeSpan tsDiferencia = dFechaFinal.Subtract(dFechaInicial);
                iDiferencia = tsDiferencia.Hours;
                if (tsDiferencia.TotalMinutes < 0)
                {
                    iDiferencia = -1;
                }
            }
            catch { }
            return iDiferencia;
        }
        public static List<string> getSubString(string sTexto, int iNumeroCaracteres)
        {
            List<string> lsResultado = new List<string>();
            try
            {
                if (sTexto != null)
                {
                    string sDivisiones = String.Empty;
                    //int iNumeroDicisiones = 0;
                    //double dNumeroDicisiones = 0;
                    int iLength = sTexto.Length;
                    decimal deDD = sTexto.Length;

                    deDD = Decimal.Divide(deDD, iNumeroCaracteres);

                    decimal dDivisiones = Decimal.Ceiling(deDD);

                    if (dDivisiones > 0)
                    {
                        int iInicial = 0;
                        int iFinal = iNumeroCaracteres;

                        for (int iContador = 0; iContador < dDivisiones; iContador++)
                        {
                            string sValor = String.Empty;
                            if (iFinal > iLength)
                            {
                                iNumeroCaracteres = iLength - iInicial;
                            }
                            sValor += sTexto.Substring(iInicial, iNumeroCaracteres);

                            lsResultado.Add(sValor);
                            iInicial += iNumeroCaracteres;
                            iFinal = iInicial + iNumeroCaracteres;
                        }
                    }
                }
            }
            catch { }
            return lsResultado;
        }
        public static bool getValidarString(string sValor)
        {
            if (sValor != null && sValor.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string getFecha(string sFecha)
        {
            string sFechaTotal = sFecha;

            if (sFecha.Contains("T"))
            {
                sFechaTotal = sFecha.Substring(0, sFecha.IndexOf('T'));
            }
            return sFechaTotal;
        }
        public static string getFechaFormato(string sFecha)
        {
            string sFechaTotal = sFecha;

            if (sFecha.Contains("T"))
            {
                sFechaTotal = sFecha.Substring(0, sFecha.IndexOf('T'));
            }
            try
            {
                sFechaTotal = ConverFecha(sFechaTotal, eFormatoFecha(sFormatoFechaSabre), SeparadorFecha(sFechaTotal), eFormatoFecha(sFormatoFecha), SeparadorFecha(sFormatoFecha));
            }
            catch { }
            return sFechaTotal;
        }
        public static string getHora(string sHora)
        {
            string sHoraTotal = sHora;

            if (sHora.Contains("T"))
            {
                sHoraTotal = sHora.Substring(sHora.IndexOf('T') + 1);
            }

            return sHoraTotal;
        }
        public static string getRetornaFormatoFecha(string sFecha)
        {
            string sFechaRetorna = sFecha;
            try
            {
                string[] sFormatoLista = null;
                if (sFecha.Contains("/"))
                    sFormatoLista = Lista(sFecha, "/");

                if (sFecha.Contains("-"))
                    sFormatoLista = Lista(sFecha, "-");

                if (sFecha.Contains("."))
                    sFormatoLista = Lista(sFecha, ".");

                if (sFormatoLista[0].ToUpper().Contains("Y"))
                {
                    if (sFormatoLista[1].ToUpper().Contains("M"))
                    {
                        sFechaRetorna = "YYYY/MM/DD";
                    }
                    else
                    {
                        sFechaRetorna = "YYYY/DD/MM";
                    }
                }
                else
                {
                    if (sFormatoLista[0].ToUpper().Contains("M"))
                    {
                        if (sFormatoLista[1].ToUpper().Contains("Y"))
                        {
                            sFechaRetorna = "MM/YYYY/DD";
                        }
                        else
                        {
                            sFechaRetorna = "MM/DD/YYYY";
                        }
                    }
                    else
                    {
                        if (sFormatoLista[1].ToUpper().Contains("M"))
                        {
                            sFechaRetorna = "DD/MM/YYYY";
                        }
                        else
                        {
                            sFechaRetorna = "DD/YYYY/MM";
                        }
                    }
                }
            }
            catch { }
            return sFechaRetorna;
        }
        public static Enum_FormatoFecha eFormatoFecha(string sFormato)
        {
            Enum_FormatoFecha eFormato = Enum_FormatoFecha.YMD;
            try
            {
                string sFecha = getRetornaFormatoFecha(sFormato);

                if (sFecha.Equals("YYYY/MM/DD"))
                {
                    eFormato = Enum_FormatoFecha.YMD;
                }
                else
                {
                    if (sFecha.Equals("YYYY/DD/MM"))
                    {
                        eFormato = Enum_FormatoFecha.YDM;
                    }
                    else
                    {
                        if (sFecha.Equals("DD/MM/YYYY"))
                        {
                            eFormato = Enum_FormatoFecha.DMY;
                        }
                        else
                        {
                            if (sFecha.Equals("DD/YYYY/MM"))
                            {
                                eFormato = Enum_FormatoFecha.DYM;
                            }
                            else
                            {
                                if (sFecha.Equals("MM/DD/YYYY"))
                                {
                                    eFormato = Enum_FormatoFecha.MDY;
                                }
                                else
                                {
                                    eFormato = Enum_FormatoFecha.MYD;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return eFormato;
        }
        public static bool _DROP_BARGAIN(DropDownList DropDown_)
        {
            if (DropDown_.SelectedIndex.Equals(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool _DROP_BARGAIN(string sHoras)
        {
            if (sHoras.Equals("T0"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string _Seleccionar_Precio(string Texto_)
        {
            string Datos_ = null;

            try
            {
                for (int i = 0; i < Texto_.Length; i++)
                {
                    if (Texto_.Substring(i, 3).CompareTo("COP") == 0)
                    {
                        Datos_ = Texto_.Substring((i + 3), 10);
                        i = Texto_.Length;
                    }
                }
            }
            catch
            {
            }
            return Datos_;
        }
        public static string _Seleccionar_PrecioDolar(string Texto_)
        {
            // Buscar error
            string Datos_ = null;

            try
            {
                for (int i = 0; i < Texto_.Length; i++)
                {
                    string aa = Texto_.Substring(i, 4);
                    if (Texto_.Substring(i, 4).CompareTo("TRUN") == 0)
                    {
                        Datos_ = Texto_.Substring((i - 19), 12);
                        i = Texto_.Length;
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
            return Datos_.Trim();
        }
        public static string _Seleccionar_Dias(string Texto_)
        {
            string Datos_ = "0";

            try
            {
                for (int i = 0; i < Texto_.Length; i++)
                {
                    if (Texto_.Substring(i, 1).CompareTo(".") == 0)
                    {
                        Datos_ = Texto_.Substring(0, i);
                        i = Texto_.Length;
                    }
                }
            }
            catch
            {
            }
            return Datos_;
        }
        public static string _Seleccionar_FechaExpedicionTickete(string Texto_)
        {
            string Datos_ = null;

            try
            {
                for (int i = 0; i < Texto_.Length - 16; i++)
                {
                    if (Texto_.Substring(i, 8).CompareTo("PURCHASE") == 0)
                    {
                        Datos_ = Texto_.Substring((i + 9), 10);
                        i = Texto_.Length;
                    }
                }
            }
            catch
            {
            }
            return Datos_;
        }
        public static String Get_Fecha(Double dbl_Dias_Adicionales, String str_Separador, string str_Formato)
        {
            String str_Fecha_Retorno = String.Empty;
            DateTime dtm_Fecha_Actual = DateTime.Now;
            if (dbl_Dias_Adicionales != 0)
                dtm_Fecha_Actual.AddDays(dbl_Dias_Adicionales);
            if (!String.IsNullOrEmpty(str_Formato))
                str_Fecha_Retorno = dtm_Fecha_Actual.Month + str_Separador + dtm_Fecha_Actual.Day + str_Separador + dtm_Fecha_Actual.Year;
            return str_Fecha_Retorno;
        }
        public static string GetKey(string strKey)
        {
            string str_Retorno = strKey;
            try
            {
                str_Retorno = ConfigurationManager.AppSettings[strKey].ToString();
                if (str_Retorno.Equals(null))
                    str_Retorno = strKey;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                try
                {
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                }
                catch { }
                cParametros.Info = "No se encontro la llave: " + strKey;
                ExceptionHandled.Publicar(cParametros);
            }
            return str_Retorno;
        }
        public static string GetKey(string strKey, string strValueDefault)
        {
            string str_Retorno = strValueDefault;
            try
            {
                str_Retorno = ConfigurationManager.AppSettings[strKey].ToString();
                if (str_Retorno.Equals(null))
                    str_Retorno = strValueDefault;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                try
                {
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                }
                catch { }
                cParametros.Info = "No se encontro la llave: " + strKey;
                ExceptionHandled.Publicar(cParametros);
            }
            return str_Retorno;
        }
        /// <summary>
        /// Busca la llave y si no la encuentra la crea con el valor por defecto que se envia por parametro.
        /// </summary>
        /// <param name="llave">llave a buscar</param>
        /// <param name="valorPorDefecto">En caso de no encontrar la llave la crea con el valor por defecto,en el web.config.
        /// puede ser null para no crear la llave en el web.config.</param>
        /// <returns>El valor de la llave.</returns>
        public static string GetKeyOrAdd(string llave, string valorPorDefecto)
        {
            string sValor = valorPorDefecto;
            try
            {
                sValor = ConfigurationManager.AppSettings[llave];
                if (sValor == null)
                {
                   //ExceptionHandled.Publicar("No se encontro la llave y no se creo: " + llave + "  Valor por defecto " + valorPorDefecto);
                    sValor = valorPorDefecto;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                try
                {
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.StackTrace = Ex.TargetSite.Module.Name.ToString();
                    cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                    cParametros.TargetSite = Ex.TargetSite.Name.ToString();
                }
                catch { }
                cParametros.Complemento = "No se encontro la llave y no se creo: " + llave + "  Valor por defecto " + valorPorDefecto;
                ExceptionHandled.Publicar(cParametros);
            }
            return sValor;
            //#line hidden
            //            System.Configuration.ExeConfigurationFileMap mapaArchivo =
            //            new System.Configuration.ExeConfigurationFileMap();

            //            mapaArchivo.ExeConfigFilename =
            //            HttpContext.Current.Request.PhysicalApplicationPath + "Web.config";

            //            System.Configuration.Configuration ArchivoConfiguracion =
            //            ConfigurationManager.OpenMappedExeConfiguration(mapaArchivo, ConfigurationUserLevel.None);

            //            if (ArchivoConfiguracion.AppSettings.Settings[llave] == null)
            //            {
            //                try
            //                {
            //                    if (valorPorDefecto != null)
            //                    {
            //                        ArchivoConfiguracion.AppSettings.Settings.Add(llave, valorPorDefecto);
            //                        ArchivoConfiguracion.Save();
            //                    }
            //                }
            //                catch (ConfigurationErrorsException Ex)
            //                {
            //                    clsParametros cParametros = new clsParametros();
            //                    cParametros.Id = 0;
            //                    cParametros.Message = Ex.Message.ToString();
            //                    cParametros.Tipo = clsTipoError.Library;
            //                    cParametros.Severity = clsSeveridad.Moderada;
            //                    try
            //                    {
            //                        cParametros.Source = Ex.Source.ToString();
            //                        cParametros.StackTrace = Ex.StackTrace.ToString();
            //                        cParametros.Complemento = Ex.TargetSite.Module.Name.ToString();
            //                        cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            //                        cParametros.TargetSite = Ex.TargetSite.Name.ToString();
            //                    }
            //                    catch { }
            //                    cParametros.Info = "No se encontro la llave y no se creo: " + llave;
            //                    ExceptionHandled.Publicar(cParametros);
            //                }
            //            }
            //            else
            //            {
            //                valorPorDefecto = ConfigurationManager.AppSettings[llave];
            //            }
            //return valorPorDefecto;
        }
        public static string GetKeyOrAdd(string llave)
        {
            return GetKeyOrAdd(llave, llave);
        }
        public static String Reemplazar_IdSesion(String strUrl)
        {
            try
            {
                if (!String.IsNullOrEmpty(strUrl))
                {
                    if (strUrl.Contains("idSesion"))
                    {
                        String[] arr_Parametros = strUrl.Split(new char[] { '&', '?' });
                        Int32 int_index = strUrl.IndexOf("idSesion");

                        for (int c = 0; c < arr_Parametros.Length; c++)
                        {
                            if (arr_Parametros[c].Contains("idSesion") && HttpContext.Current.Request.QueryString["idSesion"] != null)
                                strUrl = strUrl.Replace(arr_Parametros[c], "idSesion=" + HttpContext.Current.Request.QueryString["idSesion"]);
                        }
                    }
                    else
                    {
                        if (strUrl.Contains("?"))
                            strUrl += "&idSesion=" + HttpContext.Current.Request.QueryString["idSesion"];
                        else
                            strUrl += "?idSesion=" + HttpContext.Current.Request.QueryString["idSesion"];
                    }
                }
            }
            catch (Exception)
            {
                return strUrl;
            }
            return strUrl;
        }
        public static int idAplicacion()
        {
            int iAplicacion = 1;
            bool bEntra = true;
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    if (!cCache.Aplicacion.Length.Equals(0))
                    {
                        bEntra = false;
                        iAplicacion = int.Parse(cCache.Aplicacion);
                    }
                }
                if (bEntra)
                {
                    iAplicacion = ConsultaIdAplicacion();
                }
            }
            catch { }
            return iAplicacion;
        }

        public static int ConsultaIdAplicacion()
        {
            int iAplicacion = 1;
            try
            {
                string sRefere = GetKeyOrAdd("AplicacionWeb", "1");
                string sTipoRefere = GetKeyOrAdd("Aplicacion", "Aplicacion");

                string Consulta = null;
                Consulta = " SELECT INTCODE ";
                Consulta = Consulta + " FROM TBLAPLICACIONES ";
                Consulta = Consulta + " WHERE STRDESCRIPTION='" + sRefere + "' ";

                DataSql pclsDataSql = new DataSql();
                pclsDataSql.Conexion = clsSesiones.getConexion();
                DataSet dsData = new DataSet();
                dsData = pclsDataSql.Select(Consulta);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    iAplicacion = int.Parse(dsData.Tables[0].Rows[0][0].ToString());
                }
            }
            catch { }
            return iAplicacion;
        }
        public static clsParametros setValidaField(field cCampo)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                cParametros.Id = 1;
                if (IS_NULL(cCampo.Value))
                {
                    cParametros.Id = 0;
                    cParametros.ViewMessage.Add(cCampo.Description + " Nulo");
                    cParametros.Sugerencia.Add("");
                }
            }
            catch
            {
                cParametros.Id = 0;
                cParametros.ViewMessage.Add(cCampo.Description + " Nulo");
                cParametros.Sugerencia.Add("");
            }
            return cParametros;
        }
        #region [ validaciones de fechas y calculo de dias ]
        /// <summary>
        /// Diferencia en dias entre fechas
        /// </summary>
        /// <param name="FechaIni">Fecha inicial en formato (YYYY/MM/DD)</param>
        /// <param name="FechaFin">Fecha final en formato (YYYY/MM/DD)</param>
        /// <returns>Valor numerico de dias</returns>
        public static string CalcularFechaIni(string FechaIni)
        {
            DateTime dtFecha = DateTime.Now;
            DateTime dtFechaIni = DateTime.Now;
            string sFechaRetorno = FechaIni;
            try
            {

                dtFechaIni = DateTime.Parse(FechaIni);
                if (dtFechaIni < dtFecha)
                {
                    sFechaRetorno = dtFecha.AddDays(2).ToString(sFormatoFechaBD);
                }
            }
            catch { }
            return sFechaRetorno;
        }
        public static int CalcularDias(string FechaIni, string FechaFin)
        {
            int iDias = 1;
            DateTime dtFecha = DateTime.Now;
            DateTime dtFechaFin = DateTime.Now;
            try
            {
                dtFecha = DateTime.Parse(FechaIni);
                dtFechaFin = DateTime.Parse(FechaFin);
                TimeSpan diffResult;

                diffResult = dtFechaFin - dtFecha;

                iDias = diffResult.Days;
            }
            catch { }
            return iDias;
        }
        public static string CalculaVctoDia(int iDias)
        {
            DateTime dtFecha = DateTime.Now.AddDays(iDias);
            return dtFecha.ToString(sFormatoFechaBD); ;
        }
        public static string CalcularFechaDias(string FechaIni, int iDias)
        {
            DateTime dtFecha = DateTime.Now;
            DateTime dtFechaFin = DateTime.Now;
            try
            {
                dtFecha = DateTime.Parse(FechaIni);
                dtFechaFin = dtFecha.AddDays(iDias);
            }
            catch { }
            return dtFechaFin.ToString(sFormatoFechaBD);
        }
        public static int CalculoYear(string FechaNac)
        {
            int iYear = 1;
            DateTime dtFecha = DateTime.Now;
            DateTime dtFechaFin = DateTime.Now;
            try
            {
                dtFecha = DateTime.Parse(FechaNac);
                TimeSpan diffResult;

                diffResult = dtFechaFin - dtFecha;
                iYear = diffResult.Days / 365;
            }
            catch
            {

            }
            return iYear;
        }
        //private int DateDiff(string howtocompare, System.DateTime startDate, System.DateTime endDate)
        //{
        //    int diff = 0;
        //    try
        //    {
        //        System.TimeSpan TS = new System.TimeSpan(startDate.Ticks - endDate.Ticks);
        //        switch (howtocompare.ToLower())
        //        {
        //            case "m":
        //                diff = Convert.ToDouble(TS.TotalMinutes);
        //                break;
        //            case "s":
        //                diff = Convert.ToDouble(TS.TotalSeconds);
        //                break;
        //            case "t":
        //                diff = Convert.ToDouble(TS.Ticks);
        //                break;
        //            case "mm":
        //                diff = Convert.ToDouble(TS.TotalMilliseconds);
        //                break;
        //            case "yyyy":
        //                diff = Convert.ToInt64(TS.TotalDays / 365);
        //                break;
        //            case "q":
        //                diff = Convert.ToDouble((TS.TotalDays / 365) / 4);
        //                break;
        //            default:
        //                diff = Convert.ToDouble(TS.TotalDays);
        //                break;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        diff = -1;
        //    }
        //    return diff;
        //}
        /// <summary>
        /// Formato datetime del sistema
        /// </summary>
        /// <param name="Fecha">Fecha en formato (YYY/MM/DD)</param>
        /// <returns>Datatime de fecha</returns>
        public static DateTime RetornaFecha(string Fecha)
        {
            DateTime dtFecha = DateTime.Now;

            Enum_FormatoFecha eFormato = FormatoFecha();

            switch (eFormato)
            {
                case Enum_FormatoFecha.YDM:
                    dtFecha = DateTime.Parse(ConverYMDtoYDM(Fecha, "/"));
                    break;

                case Enum_FormatoFecha.YMD:
                    dtFecha = DateTime.Parse(Fecha);
                    break;

                case Enum_FormatoFecha.DMY:
                    dtFecha = DateTime.Parse(ConverYMDtoDMY(Fecha, "/"));
                    break;

                case Enum_FormatoFecha.MDY:
                    dtFecha = DateTime.Parse(ConverYMDtoMDY(Fecha, "/"));
                    break;
            }
            return dtFecha;
        }
        /// <summary>
        /// Separador de fecha
        /// </summary>
        /// <returns>Retorna string con el separador de fecha del sistema</returns>
        public static string SeparadorFecha()
        {
            string sFecha = CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator;
            return sFecha;
        }
        /// <summary>
        /// Formato corto de fecha
        /// </summary>
        /// <returns>string con el formato corto de fecha del sistema</returns>
        public static string FormatoFechaShort()
        {
            string sFecha = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            return FormatoFechaS(sFecha);
        }
        /// <summary>
        /// Separador decimal del sistema
        /// </summary>
        /// <returns>string del separador decimal del sistema</returns>
        public static string SeparadorDecimal()
        {
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            return decimalSeparator;
        }
        #endregion
        public static Page getPaginaActual()
        {
            Page PageActual = (Page)HttpContext.Current.Handler;
            return PageActual;
        }
        public static string[] Lista(string sTexto, string sSeparador)
        {
            string[] sLista = null;

            if (sTexto.Contains(sSeparador))
            {
                sLista = sTexto.Split(new char[] { Convert.ToChar(sSeparador) });
            }
            else
            {
                string[] sListaTemp = new string[1];
                sListaTemp[0] = sTexto;
                sLista = sListaTemp;
            }
            return sLista;
        }
        public static string CambiarCaracter(string sTexto)
        {
            string sTextoNuevo = RetornaLetras(sTexto);
            sTextoNuevo = sTextoNuevo.Replace("ñ", "n");
            sTextoNuevo = sTextoNuevo.Replace("Ñ", "N");

            sTextoNuevo = sTextoNuevo.Replace("á", "a");
            sTextoNuevo = sTextoNuevo.Replace("é", "e");
            sTextoNuevo = sTextoNuevo.Replace("í", "i");
            sTextoNuevo = sTextoNuevo.Replace("ó", "o");
            sTextoNuevo = sTextoNuevo.Replace("ú", "u");
            sTextoNuevo = sTextoNuevo.Replace("ü", "u");

            sTextoNuevo = sTextoNuevo.Replace("Á", "A");
            sTextoNuevo = sTextoNuevo.Replace("É", "E");
            sTextoNuevo = sTextoNuevo.Replace("Í", "I");
            sTextoNuevo = sTextoNuevo.Replace("Ó", "O");
            sTextoNuevo = sTextoNuevo.Replace("Ú", "U");
            sTextoNuevo = sTextoNuevo.Replace("Ü", "U");

            return sTextoNuevo;
        }
        public static string CambiarCaracterNormal(string sTexto)
        {
            string sTextoNuevo = sTexto;
            sTextoNuevo = sTextoNuevo.Replace("ñ", "n");
            sTextoNuevo = sTextoNuevo.Replace("Ñ", "N");

            sTextoNuevo = sTextoNuevo.Replace("á", "a");
            sTextoNuevo = sTextoNuevo.Replace("é", "e");
            sTextoNuevo = sTextoNuevo.Replace("í", "i");
            sTextoNuevo = sTextoNuevo.Replace("ó", "o");
            sTextoNuevo = sTextoNuevo.Replace("ú", "u");
            sTextoNuevo = sTextoNuevo.Replace("ü", "u");

            sTextoNuevo = sTextoNuevo.Replace("Á", "A");
            sTextoNuevo = sTextoNuevo.Replace("É", "E");
            sTextoNuevo = sTextoNuevo.Replace("Í", "I");
            sTextoNuevo = sTextoNuevo.Replace("Ó", "O");
            sTextoNuevo = sTextoNuevo.Replace("Ú", "U");
            sTextoNuevo = sTextoNuevo.Replace("Ü", "U");

            return sTextoNuevo;
        }
        public static string CambiarCaracter(string sTexto, string sCharOld, string sCharNew)
        {
            string sTextoNuevo = sTexto;
            sTextoNuevo = sTextoNuevo.Replace(sCharOld, sCharNew);
            return sTextoNuevo;
        }
        public static string RecortaParrafo(string sTexto, int iParrafos)
        {
            string sTextoNew = sTexto;
            try
            {
                //StringBuilder sbTexto = new StringBuilder();
                int iPos = sTextoNew.IndexOf("</p>");
                sTextoNew = sTextoNew.Substring(0, iPos);
                sTextoNew = sTextoNew + "</p>";
                //string[] slTexto = Lista(sTextoNew, "</p>");
                //for (int i = 0; i < iParrafos; i++)
                //{
                //    sbTexto.Append(slTexto[i] + "</p>");
                //}
                //sTextoNew = sbTexto.ToString();
            }
            catch { }
            return sTextoNew;
        }
        /// <summary>
        /// Metodo que elimina todos los caracteres no numericos, incluidos signos de puntuacion y retorna un string de numeros
        /// </summary>
        /// <param name="sValor">Valor a validar</param>
        /// <returns>string de solo numeros</returns>
        /// Autor:  Jose faustino Posas
        /// Fecha:  2009-09-30
        /// Fecha Ultima modificacion:  
        public static string RetornaNumero(string sValor)
        {
            int iCant = sValor.Length;
            string sValorFinal = string.Empty;

            for (int i = 0; i < iCant; i++)
            {
                char sChar = Convert.ToChar(sValor.Substring(i, 1).ToString());
                if (Char.IsNumber(sChar))
                {
                    sValorFinal += sChar;
                }
            }
            try
            {
                if (sValorFinal.Length.Equals(0))
                    sValorFinal = "0";
            }
            catch
            {
                sValorFinal = "0";
            }
            return sValorFinal;
        }
        public static string RetornaNumeroCompleto(string sValor)
        {
            string sSeparador = SeparadorDecimal();
            return RetornaNumeroCompleto(sValor, sSeparador);
        }
        public static string RetornaNumeroCompleto(string sValor, string sSeparador)
        {
            int iCant = sValor.Length;
            string sValorFinal = string.Empty;
            for (int i = 0; i < iCant; i++)
            {
                char sChar = Convert.ToChar(sValor.Substring(i, 1).ToString());
                if (Char.IsNumber(sChar))
                {
                    sValorFinal += sChar;
                }
                else
                {
                    if (sValor.Substring(i, 1).ToString().Equals(sSeparador))
                    {
                        sValorFinal += sValor.Substring(i, 1).ToString();
                    }
                }
            }
            try
            {
                if (sValorFinal.Length.Equals(0))
                    sValorFinal = "0";
            }
            catch
            {
                sValorFinal = "0";
            }
            return sValorFinal;
        }
        public static int RetornaDiasMes(int Mes, int Year)
        {
            int iDias = 30;
            try
            {
                switch (Mes)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        iDias = 31;
                        break;

                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        iDias = 30;
                        break;

                    case 2:
                        decimal iYear02 = Year / 4;
                        if (IS_INTEGER(iYear02.ToString()))
                            iDias = 29;
                        else
                            iDias = 28;
                        break;
                }
            }
            catch { }
            return iDias;
        }
        public static string RetornaLetras(string sValor)
        {
            int iCant = sValor.Length;
            string sValorFinal = string.Empty;

            for (int i = 0; i < iCant; i++)
            {
                char sChar = Convert.ToChar(sValor.Substring(i, 1).ToString());
                if (Char.IsLetter(sChar))
                {
                    sValorFinal += sChar;
                }
                else
                {
                    if (Char.IsWhiteSpace(sChar))
                    {
                        sValorFinal += sChar;
                    }
                }
            }
            return sValorFinal;
        }
        public static Int64 RedondearACentenas(Int64 valor, string CaracterDecimal)
        {
            string Entero = valor.ToString().Substring(0, valor.ToString().Length - 2);
            string Decimal = valor.ToString().Substring(valor.ToString().Length - 2);
            double final = Convert.ToDouble(Entero + CaracterDecimal + Decimal);
            Int64 Redondeado = Convert.ToInt64(final);
            string RedondeadoCent = Redondeado.ToString() + "00";
            return Convert.ToInt64(RedondeadoCent);
        }
        public static void ActualiceDatatable(DataTable datatable, string strColumna, string strTexto, PosicionText enPosicion, string strColumnaAdic)
        {
            try
            {
                switch (enPosicion)
                {
                    case PosicionText.Final:
                        for (int i = 0; i < datatable.Rows.Count; i++)
                        {
                            datatable.Rows[i][strColumna] = datatable.Rows[i][strColumna].ToString() + strTexto;
                        }
                        break;
                    case PosicionText.Inicio:
                        for (int i = 0; i < datatable.Rows.Count; i++)
                        {
                            datatable.Rows[i][strColumna] = strTexto + datatable.Rows[i][strColumna].ToString();
                        }
                        break;
                    case PosicionText.Total:
                        for (int i = 0; i < datatable.Rows.Count; i++)
                        {
                            datatable.Rows[i][strColumna] = strTexto + datatable.Rows[i][strColumnaAdic].ToString();
                        }
                        break;
                    case PosicionText.Formato:
                        for (int i = 0; i < datatable.Rows.Count; i++)
                        {
                            datatable.Rows[i][strColumna] = Formato(datatable.Rows[i][strColumna].ToString(), Format.Numeric, null);
                        }
                        break;
                }
            }
            catch { }
        }
        public static void ActualiceDatatableImage(DataTable datatable, string strColumna, string strRuta)
        {
            try
            {
                if (strRuta.Length.Equals(0) || strRuta == null)
                    strRuta = ObtenerUrlPlanes();

                for (int i = 0; i < datatable.Rows.Count; i++)
                {
                    if (!datatable.Rows[i][strColumna].ToString().Length.Equals(0))
                    {
                        datatable.Rows[i][strColumna] = strRuta + datatable.Rows[i][strColumna].ToString();
                    }
                    else
                    {
                        datatable.Rows[i][strColumna] = strRuta + "spacer.gif";
                    }
                }
            }
            catch { }
        }
        public static string Formato(string text, Format Formato, string mascara)
        {
            string txtformat = text;
            switch (Formato)
            {
                case Format.Money:
                    txtformat = mascara + " " + Convert.ToDecimal(txtformat).ToString("###,###,##0.00");
                    break;
                case Format.Numeric:
                    txtformat = Convert.ToDecimal(txtformat).ToString("###,###,##0.00");
                    break;
                case Format.DateTime:
                    txtformat = Convert.ToDateTime(txtformat).ToString(mascara);
                    break;
            }
            return txtformat;
        }
        public static string FormatoSuprimir(string text, Format Formato, string mascara)
        {
            string txtformat = text;
            switch (Formato)
            {
                case Format.Money:
                    txtformat = txtformat.Substring(2);
                    break;
                case Format.Numeric:
                    txtformat = Convert.ToDecimal(txtformat).ToString();
                    break;
                case Format.DateTime:
                    txtformat = Convert.ToDateTime(txtformat).ToString();
                    break;
            }
            return txtformat;
        }
        /// <summary>
        /// Metodo para eliminar archivos temporales de una carpeta en un termino de dias
        /// </summary>
        /// <param name="strRuta">Carpeta donde se encuentran los archivos</param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static void BorrarArchivos(string strRuta)
        {
            try
            {
                int iDias = int.Parse(clsValidaciones.GetKeyOrAdd("BorradoCacheDias", "1"));
                BorrarArchivos(strRuta, iDias);
            }
            catch { }
        }
        /// <summary>
        /// Metodo para el bvorrado de archivos sobre una carpeta, este borrado es en dias
        /// </summary>
        /// <param name="strRuta">Carpeta donde se encuentran los archivos</param>
        /// <param name="intDias">Dias para el borrado</param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static void BorrarArchivos(string strRuta, int intDias)
        {
            DirectoryInfo d = new DirectoryInfo(strRuta);
            FileInfo[] f = d.GetFiles("*.*");
            intDias = intDias * -1;
            for (int i = 0; i < f.Length; i++)
            {
                try
                {
                    if (f[i].LastWriteTime <= DateTime.Now.AddDays(intDias))
                    {
                        f[i].Delete();
                    }
                }
                catch { }
            }
        }
        /// <summary>
        /// Metodo para el borrado de archivos en minutos, se utiliza basicamente para la cache
        /// </summary>
        /// <param name="strRuta">Ruta donde se encuentran los archivos</param>
        /// <param name="intMinutos">Minutos a borrar</param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static void BorrarArchivosMinutos(string strRuta, double intMinutos)
        {
            DirectoryInfo d = new DirectoryInfo(strRuta);
            FileInfo[] f = d.GetFiles("*.*");
            intMinutos = intMinutos * -1;
            for (int i = 0; i < f.Length; i++)
            {
                try
                {
                    if (f[i].LastWriteTime <= DateTime.Now.AddMinutes(intMinutos))
                    {
                        f[i].Delete();
                    }
                }
                catch { }
            }
        }
        /// <summary>
        /// Metodo para el borrado de archivos en Horas
        /// </summary>
        /// <param name="strRuta">Ruta donde se encuentran los archivos</param>
        /// <param name="intHoras">Horas a borrar</param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static void BorrarArchivosHoras(string strRuta, double intHoras)
        {
            DirectoryInfo d = new DirectoryInfo(strRuta);
            FileInfo[] f = d.GetFiles("*.*");
            intHoras = intHoras * -1;
            for (int i = 0; i < f.Length; i++)
            {
                try
                {
                    if (f[i].LastWriteTime <= DateTime.Now.AddHours(intHoras))
                    {
                        f[i].Delete();
                    }
                }
                catch { }
            }
        }
        /// <summary>
        /// Metodo para el borrado de archivos en general si tiempo de espera
        /// </summary>
        /// <param name="strRuta">Ruta donde se encuentran los archivos</param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-28
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static void BorrarArchivosGeneral(string strRuta)
        {
            DirectoryInfo d = new DirectoryInfo(strRuta);
            FileInfo[] f = d.GetFiles("*.*");
            for (int i = 0; i < f.Length; i++)
            {
                try
                {
                    f[i].Delete();
                }
                catch { }
            }
        }
        public static void BorrarArchivos(string lstrRuta, string strFileName, bool bolCache)
        {
            if (bolCache)
            {
                strFileName = "Cache_" + strFileName;
            }
            if (File.Exists(lstrRuta + "\\" + strFileName))
            {
                File.Delete(lstrRuta + "\\" + strFileName);
            }
        }
        /// <summary>
        /// Validahorarios para pagos y buscador
        /// </summary>
        /// <param name="dtData">dtData, datatable de horarios</param>
        /// <returns>bHorario, Validacion d ehorarios</returns>
        /// <remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-08
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:          
        /// Descripción:    
        /// </remarks>
        public static bool ValidaHorario(DataTable dtData)
        {
            bool bHorario = true;
            try
            {
                const string COLUMN_DIA = "strValor";
                const string COLUMN_HORA_INI = "strHoraIni";
                const string COLUMN_HORA_FIN = "strHoraFin";
                const string COLUMN_ACTIVO = "bitActivo";
                DateTime tDate = tFechaHoraServidor();
                bool bFestivo = ConsultaFestivo(tDate); ;

                int iCant = dtData.Rows.Count;
                for (int i = 0; i < iCant; i++)
                {
                    Enum_Horarios eHorario = eHorarioEnum(dtData.Rows[i][COLUMN_DIA].ToString());
                    TimeSpan tHoraIni = TimeSpan.Parse(dtData.Rows[i][COLUMN_HORA_INI].ToString());
                    TimeSpan tHoraFin = TimeSpan.Parse(dtData.Rows[i][COLUMN_HORA_FIN].ToString());

                    if (bFestivo)
                    {
                        if (eHorario.Equals(Enum_Horarios.Festivo))
                        {
                            if (bool.Parse(dtData.Rows[i][COLUMN_ACTIVO].ToString()).Equals(true))
                            {
                                if (tDate.TimeOfDay >= tHoraIni && tDate.TimeOfDay <= tHoraFin)
                                {
                                    bHorario = true;
                                }
                                else
                                {
                                    bHorario = false;
                                }
                            }
                            else
                            {
                                bHorario = false;
                            }
                            break;
                        }
                    }
                    else
                    {
                        if (tDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            if (eHorario.Equals(Enum_Horarios.Domingo))
                            {
                                if (bool.Parse(dtData.Rows[i][COLUMN_ACTIVO].ToString()).Equals(true))
                                {
                                    if (tDate.TimeOfDay >= tHoraIni && tDate.TimeOfDay <= tHoraFin)
                                    {
                                        bHorario = true;
                                    }
                                    else
                                    {
                                        bHorario = false;
                                    }
                                }
                                else
                                {
                                    bHorario = false;
                                }
                                break;
                            }
                        }
                        else
                        {
                            if (tDate.DayOfWeek == DayOfWeek.Saturday)
                            {
                                if (eHorario.Equals(Enum_Horarios.Sábado))
                                {
                                    if (bool.Parse(dtData.Rows[i][COLUMN_ACTIVO].ToString()).Equals(true))
                                    {
                                        if (tDate.TimeOfDay >= tHoraIni && tDate.TimeOfDay <= tHoraFin)
                                        {
                                            bHorario = true;
                                        }
                                        else
                                        {
                                            bHorario = false;
                                        }
                                    }
                                    else
                                    {
                                        bHorario = false;
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (eHorario.Equals(Enum_Horarios.Lunes_Viernes))
                                {
                                    if (bool.Parse(dtData.Rows[i][COLUMN_ACTIVO].ToString()).Equals(true))
                                    {
                                        if (tDate.TimeOfDay >= tHoraIni && tDate.TimeOfDay <= tHoraFin)
                                        {
                                            bHorario = true;
                                        }
                                        else
                                        {
                                            bHorario = false;
                                        }
                                    }
                                    else
                                    {
                                        bHorario = false;
                                    }
                                    break;
                                }
                                else
                                {
                                    if (bool.Parse(dtData.Rows[i][COLUMN_ACTIVO].ToString()).Equals(true))
                                    {
                                        if (tDate.TimeOfDay >= tHoraIni && tDate.TimeOfDay <= tHoraFin)
                                        {
                                            bHorario = true;
                                        }
                                        else
                                        {
                                            bHorario = false;
                                        }
                                    }
                                    else
                                    {
                                        bHorario = false;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return bHorario;
        }
        /// <summary>
        /// Metodo que consulta si un dia es festivo
        /// </summary>
        /// <param name="tDate">Fecha del dia</param>
        /// <returns>bValidaDF, Validacion del dia festivo</returns>
        /// <remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-08
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:          
        /// Descripción:    
        /// </remarks>
        private static bool ConsultaFestivo(DateTime tDate)
        {
            bool bValidaDF = false;
            try
            {
                DataSet dsData = new DataSet();
                DataSql SqlData = new DataSql();

                StringBuilder Consulta = new StringBuilder();
                string sIdioma = clsSesiones.getIdioma();
                string sAplicacion = clsSesiones.getAplicacion().ToString();
                string sDiasFestivo = clsValidaciones.GetKeyOrAdd("DiasFestivos", "DiasFestivos");
                string sFestivo = tDate.ToString(sFormatoFechaBD);

                Consulta.AppendLine(" SELECT     tblRelaRefere.strValor ");
                Consulta.AppendLine(" FROM         tblRefere INNER JOIN ");
                Consulta.AppendLine(" tblRelaRefere ON tblRefere.intAplicacion = tblRelaRefere.intAplicacion AND  ");
                Consulta.AppendLine(" tblRefere.intidTipoRefere = tblRelaRefere.intIdTipoRefere AND tblRefere.intidRefere = tblRelaRefere.intidRefere INNER JOIN ");
                Consulta.AppendLine(" tblTipoRefere ON tblRefere.intidTipoRefere = tblTipoRefere.intidTipoRefere ");
                Consulta.AppendLine(" WHERE     (tblRefere.strIdioma = '" + sIdioma + "') ");
                Consulta.AppendLine(" AND (tblRefere.intAplicacion = " + sAplicacion + ") ");
                Consulta.AppendLine(" AND (tblTipoRefere.strTipoRefere = '" + sDiasFestivo + "') ");
                Consulta.AppendLine(" AND (tblRelaRefere.strValor = '" + sFestivo + "') ");
                dsData = SqlData.ConsultaTabla(Consulta.ToString());
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    bValidaDF = true;
                }
            }
            catch{}
            return bValidaDF;
        }
        private static Enum_Horarios eHorarioEnum(string sIdHorario)
        {
            Enum_Horarios eHorarios = Enum_Horarios.Lunes_Viernes;
            if (sIdHorario.Equals(Enum_Horarios.Lunes_Viernes.GetHashCode().ToString()))
            {
                eHorarios = Enum_Horarios.Lunes_Viernes;
            }
            if (sIdHorario.Equals(Enum_Horarios.Sábado.GetHashCode().ToString()))
            {
                eHorarios = Enum_Horarios.Sábado;
            }
            if (sIdHorario.Equals(Enum_Horarios.Domingo.GetHashCode().ToString()))
            {
                eHorarios = Enum_Horarios.Domingo;
            }
            if (sIdHorario.Equals(Enum_Horarios.Festivo.GetHashCode().ToString()))
            {
                eHorarios = Enum_Horarios.Festivo;
            }
            return eHorarios;
        }
        public static TimeSpan tHoraServidor()
        {
            DataSql pclsDataSql = new DataSql();
            string pstrSql = " select convert(varchar, CURRENT_TIMESTAMP, 108)";
            TimeSpan tmTime = DateTime.Now.TimeOfDay;
            try
            {
                DataSet dsData = pclsDataSql.Select(pstrSql);
                if (dsData.Tables[0] != null)
                    tmTime = TimeSpan.Parse(dsData.Tables[0].Rows[0][0].ToString());
            }
            catch { }
            return tmTime;
        }
        public static DateTime tFechaServidor()
        {
            DataSql pclsDataSql = new DataSql();
            string pstrSql = " select convert(varchar, CURRENT_TIMESTAMP, 111)";
            DateTime tmTime = DateTime.Now;
            try
            {
                DataSet dsData = pclsDataSql.Select(pstrSql);
                if (dsData.Tables[0] != null)
                    tmTime = DateTime.Parse(dsData.Tables[0].Rows[0][0].ToString());
            }
            catch { }
            return tmTime;
        }
        public static DateTime tFechaHoraServidor()
        {
            DataSql pclsDataSql = new DataSql();
            string pstrSql = " select convert(varchar, CURRENT_TIMESTAMP, 111) + ' ' + convert(varchar, CURRENT_TIMESTAMP, 108) as Time";
            DateTime tmTime = DateTime.Now;
            try
            {
                DataSet dsData = pclsDataSql.Select(pstrSql);
                if (dsData.Tables[0] != null)
                    tmTime = DateTime.Parse(dsData.Tables[0].Rows[0][0].ToString());
            }
            catch { }
            return tmTime;
        }
        public static void CambiarIdiomaDelServidorSql(Enum_Idioma EnumIdioma)
        {
            DataSql pclsDataSql = new DataSql();
            string idioma = "us_english";

            switch (EnumIdioma)
            {
                case Enum_Idioma.Espanol:
                    idioma = "Español";
                    break;
                case Enum_Idioma.Ingles:
                    idioma = "us_english";
                    break;
                default:
                    idioma = "us_english";
                    break;
            }
            string pstrSql = "SET LANGUAGE " + idioma;

            try { pclsDataSql.UpdateInsert(pstrSql); }
            catch { }

        }
        public static string GetIdiomaServidorSql()
        {
            DataSql pclsDataSql = new DataSql();
            string pstrSql = "SELECT @@LANGUAGE AS 'Idioma';";
            string idiomaRetorno = string.Empty;
            try
            {
                DataSet dsData = pclsDataSql.Select(pstrSql);
                if (dsData.Tables[0] != null)
                    idiomaRetorno = dsData.Tables[0].Rows[0][0].ToString();
            }
            catch { }
            return idiomaRetorno;
        }
        /// <summary>
        /// Obtiene el formato de la fecha servidor sql server. 
        /// </summary>
        /// <returns></returns>
        public static string GetFD()
        {
            //DataSql pclsDataSql = new DataSql();
            //string pstrSql = "sp_helplanguage @@language;";
            //string formatoFechaRetorno = string.Empty;
            //try
            //{
            //    pclsDataSql.Conexion = GetKeyOrAdd("strConexion");
            //    DataSet dsData = pclsDataSql.Select(pstrSql);
            //    if (dsData != null &&
            //        dsData.Tables.Count != 0 &&
            //        dsData.Tables[0].Rows.Count != 0)
            //    {
            //        string formatoFecha = dsData.Tables[0].Rows[0]["dateformat"].ToString();

            //        if (formatoFecha.Equals("mdy"))
            //            formatoFechaRetorno = "MM/dd/yyyy";
            //        else if (formatoFecha.Equals("ymd"))
            //            formatoFechaRetorno = "yyyy/MM/dd";
            //        else if (formatoFecha.Equals("dmy"))
            //            formatoFechaRetorno = "dd/MM/yyyy";
            //    }
            //}
            //catch { }
            //return formatoFechaRetorno;
            return GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");
        }
        /// <summary>
        /// Obtiene la fecha convertida al formato del servidor de sql server.
        /// </summary>
        /// <param name="anio">año</param>
        /// <param name="mes">mes</param>
        /// <param name="dia">dia</param>
        /// <returns>Fecha convertida.</returns>
        public static string GetFBD(string anio, string mes, string dia)
        {
            return new DateTime(int.Parse(anio), int.Parse(mes), int.Parse(dia)).ToString(GetFD());
        }
        /// <summary>
        /// Procedimiento para obtener la Ip del cliente. Ej. 201.201.201.201
        /// </summary>
        /// <returns></returns>
        public static string ObtenerIp()
        {
            string sResult = string.Empty;
            try
            {
                sResult = HttpContext.Current.Request.UserHostAddress.ToString();
            }
            catch { }
            return sResult;
        }
        public static string ObtenerUrlRaiz()
        {
            string sPagina = string.Empty;
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                sPagina = PageActual.Request.Url.Scheme + "://" + PageActual.Request.Url.Authority;
                int iSegment = PageActual.Request.Url.Segments.Length;
                iSegment--;
                iSegment--;
                for (int i = 0; i < iSegment; i++)
                {
                    sPagina += PageActual.Request.Url.Segments[i].ToString();
                }
            }
            catch { }
            return sPagina;
        }
        public static string ObtenerUltimoRuta(string sPagina)
        {
            string sPaginaTemp = ObtenerPosRuta(sPagina, 1);
            return sPaginaTemp;
        }
        public static string ObtenerPosRuta(string sPagina, int iPos)
        {
            string sPaginaTemp = sPagina;
            try
            {
                string sCaracter = "/";
                if (sPagina.Contains(@"\"))
                    sCaracter = @"\";
                sPaginaTemp = ObtenerPosTexto(sPagina, sCaracter, iPos);
            }
            catch { }
            return sPaginaTemp;
        }
        public static string ObtenerPosTexto(string sTexto, string sCaracter, int iPos)
        {
            string sTextoTemp = sTexto;
            try
            {
                string[] sTextoUltima = Lista(sTexto, sCaracter);
                int iCountPos = sTextoUltima.Length;
                iPos--;
                if (iCountPos > 1)
                {
                    iCountPos--;
                    if (sTextoUltima[iCountPos].ToString().Length.Equals(0))
                    {
                        iCountPos--;
                    }
                    sTextoTemp = string.Empty;
                    int iCantPos = iCountPos - iPos;
                    for (int i = iCantPos; i <= iCountPos; i++)
                    {
                        sTextoTemp += sTextoUltima[i];
                        sTextoTemp += sCaracter;
                    }
                }
            }
            catch { }
            return sTextoTemp;
        }
        public static string ObtenerUrlPagina()
        {
            string sPagina = string.Empty;
            try
            {
                Page PageActual = (Page)HttpContext.Current.Handler;
                sPagina = PageActual.Request.Url.Scheme + "://" + PageActual.Request.Url.Authority;
                int iSegment = PageActual.Request.Url.Segments.Length;
                iSegment--;
                for (int i = 0; i < iSegment; i++)
                {
                    sPagina += PageActual.Request.Url.Segments[i].ToString();
                }
            }
            catch { }
            return sPagina;
        }
        /// <summary>
        /// Se pasa
        /// </summary>
        /// <param name="PageSource"></param>
        /// <returns></returns>
        public static string UserControl(UserControl PageSource)
        {
            string sPagina = string.Empty;
            try
            {
                string[] sControls = clsValidaciones.Lista(PageSource.AppRelativeVirtualPath.ToString(), "/");
                int iSegment = sControls.Length;
                iSegment--;
                sPagina = sControls[iSegment].ToString();
            }
            catch { }
            return sPagina;
        }
        /// <summary>
        /// Procedimiento para obtener el Host del cliente. Ej. WWW.Test.com
        /// </summary>
        /// <returns></returns>
        public static string ObtenerHostName()
        {
            string sResult = string.Empty;
            try
            {
                sResult = HttpContext.Current.Request.UserHostName.ToString();
            }
            catch { }
            return sResult;
        }
        /// <summary>
        /// Procedimiento para obtener el Page. Ej. WWW.Test.com
        /// </summary>
        /// <returns></returns>
        public static string ObtenerUrl()
        {
            string sResult = string.Empty;
            try
            {
                sResult = HttpContext.Current.Request.Url.ToString();
            }
            catch { }
            return sResult;
        }
        /// <summary>
        /// Procedimiento para obtener el explorador del cliente. Ej. IE
        /// </summary>
        /// <returns></returns>
        public static string ObtenerBrowser()
        {
            string sResult = string.Empty;
            try
            {
                sResult = HttpContext.Current.Request.Browser.Browser.ToString();
            }
            catch { }
            return sResult;
        }
        /// <summary>
        /// Procedimiento para obtener el Version Browser, Ej. 7.0
        /// </summary>
        /// <returns></returns>
        public static string ObtenerBrowserVersion()
        {
            string sResult = string.Empty;
            try
            {
                sResult = HttpContext.Current.Request.Browser.Version.ToString();
            }
            catch { }
            return sResult;
        }
        /// <summary>
        /// Procedimiento para obtener el User Browser. Ej. Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US)
        /// </summary>
        /// <returns></returns>
        public static string ObtenerBrowserUser()
        {
            string sResult = string.Empty;
            try
            {
                sResult = HttpContext.Current.Request.UserAgent.ToString();
            }
            catch { }
            return sResult;
        }
        /// <summary>
        /// Procedimiento para obtener el User Browser, Ej. IE7
        /// </summary>
        /// <returns></returns>
        public static string ObtenerBrowserType()
        {
            string sResult = string.Empty;
            try
            {
                sResult = HttpContext.Current.Request.Browser.Type.ToString();
            }
            catch { }
            return sResult;
        }
        /// <summary>
        /// Procedimiento para obtener el User Browser, Ej. WinXP
        /// </summary>
        /// <returns></returns>
        public static string ObtenerBrowserPlataform()
        {
            string sResult = string.Empty;
            try
            {
                sResult = HttpContext.Current.Request.Browser.Platform.ToString();
            }
            catch { }
            return sResult;
        }
        //public static string ObtenerUrlImages()
        //{
        //    string sIdioma = string.Empty;
        //    string sPaginaIdioma = GetKeyOrAdd("sPaginaIdioma", "False");
        //    if (sPaginaIdioma.ToUpper().Equals("TRUE"))
        //    {
        //        sIdioma = clsSesiones.getIdioma() + "/";
        //    }
        //    return ObtenerUrlImages(sIdioma);
        //}
        public static string ObtenerUrlRutaPage(string sPagina)
        {
            return sPagina;
        }
        //public static string ObtenerUrlPlanes()        //public static string ObtenerUrlImages()
        //{
        //    string sIdioma = string.Empty;
        //    string sPaginaIdioma = GetKeyOrAdd("sPaginaIdioma", "False");
        //    if (sPaginaIdioma.ToUpper().Equals("TRUE"))
        //    {
        //        sIdioma = clsSesiones.getIdioma() + "/";
        //    }
        //    return ObtenerUrlImages(sIdioma);
        //}
        //public static string ObtenerUrlPlanes()
        //{
        //    string sIdioma = string.Empty;
        //    string sPaginaIdioma = GetKeyOrAdd("sPaginaIdioma", "False");
        //    if (sPaginaIdioma.ToUpper().Equals("TRUE"))
        //    {
        //        sIdioma = clsSesiones.getIdioma() + "/";
        //    }
        //    return ObtenerUrlPlanes(sIdioma);
        //}
        public static string ObtenerUrlImages()
        {
            return ObtenerUrlRaiz() + "App_Themes/Imagenes/";
        }      
        public static string ObtenerUrlPlanes()
        {
            return GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "http://www.ssoftcolombia.com/Pagina/Imagenes/Planes/");
        }
        public static string ObtenerUrlImages(string sIdioma)
        {
            return ObtenerUrlRaiz() + "App_Themes/Imagenes/" + sIdioma;
        }
        public static string ObtenerUrlPlanes(string sIdioma)
        {
            return GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "http://www.ssoftcolombia.com/Pagina/Imagenes/Planes/") + sIdioma;
        }
        public static string ObtenerUrlPlanesWs()
        {
            return GetKeyOrAdd("RUTA_IMAGENES_PLANESWS", "http://www.ssoftcolombia.com/Pagina/Imagenes/Planes/");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GeneraLink(string sImagen)
        {
            StringBuilder sLink = new StringBuilder();
            try
            {
                //string Ruta = ObtenerUrlPlanes()W;
                string Ruta = ObtenerUrlPlanes();

                sLink.AppendLine("<iframe id='iTarifario' src='" + Ruta + sImagen + "' frameborder='0' width='100%' height='450px'></iframe>");
                //sLink.AppendLine("<a href='" + Ruta + sImagen + "' target='_blank'>");
                //sLink.AppendLine("<span style='font-size: medium;'>");
                //sLink.AppendLine(sTexto);
                //sLink.AppendLine("</span></a></p>");
            }
            catch { }
            return sLink.ToString();
        }
        public static string DocumentosTempCrea()
        {
            string sPath = @"c:\Temp\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"Documentos\Temp\"; }
            catch { sPath = @"c:\Temp\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static string DocumentosCrea()
        {
            string sPath = @"c:\Temp\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"Documentos\"; }
            catch { sPath = @"c:\Temp\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static string HTMLTempCrea()
        {
            string sPath = @"c:\HTML\Contacto\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"HTML\Contacto\"; }
            catch { sPath = @"c:\HTML\Contacto\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static string HTMLGenCrea()
        {
            string sPath = @"c:\HTML\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"HTML\"; }
            catch { sPath = @"c:\HTML\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static string XMLTempCrea()
        {
            string sPath = @"c:\XML\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"XML\"; }
            catch { sPath = @"c:\XML\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static string CacheTempCrea()
        {
            string sPath = @"c:\XML\Cache\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"XML\Cache\"; }
            catch { sPath = @"c:\XML\Cache\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static string CreaRutaImages()
        {
            string sPath = @"c:\XML\Cache\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"Imagenes\"; }
            catch { sPath = @"c:\XML\Cache\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static string CreaRutaImagesGen()
        {
            string sPath = @"c:\XML\Cache\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"App_Themes\Imagenes\"; }
            catch { sPath = @"c:\XML\Cache\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static string CreaRutaImagesPlanes()
        {
            string sPath = @"c:\XML\Cache\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"Imagenes\Planes\"; }
            catch { sPath = @"c:\XML\Cache\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static string XMLDatasetCrea()
        {
            string sPath = @"c:\XML\DataSet\Temp\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"XML\DataSet\Temp\"; }
            catch { sPath = @"c:\XML\DataSet\Temp\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static string XMLDatasetCreaGen()
        {
            string sPath = @"c:\XML\DataSet\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"XML\DataSet\"; }
            catch { sPath = @"c:\XML\DataSet\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static string DocumentosUrl()
        {
            string sRuta = RutaDocumentosGen();
            try
            {
                sRuta = ObtenerUrlRaiz() + "Documentos/";
            }
            catch { }
            return sRuta;
        }
        public static string DocumentosUrlTemp()
        {
            string sRuta = RutaDocumentosGen();
            try
            {
                sRuta = ObtenerUrlRaiz() + "Documentos/Temp/";
            }
            catch { }
            return sRuta;
        }
        public static string RutaImagesGen()
        {
            return GetKeyOrAdd("RUTA_IMAGENES_GEN", "../App_Themes/Imagenes/");

        }
        public static string RutaImagesAirGen()
        {
            return RutaSsoftGen() + "App_Themes/Imagenes/Airline/";
        }
        public static string RutaImagesCarGen()
        {
            return RutaSsoftGen() + "App_Themes/Imagenes/Autos/";
        }
        public static string RutaHoteleCotelcoGen()
        {
            return GetKeyOrAdd("RUTA_HOTELES_COTELCO", "http://www.reservashoteleras.com.co/");
        }
        public static string RutaXmlGen()
        {
            return RutaSsoftGen() + "App_Themes/Xml/";
        }
        public static string RutaSsoftGen()
        {
            return GetKeyOrAdd("RUTA_SSOFT", "http://www.ssoftcolombia.com/");
        }
        public static string RutaSsoftMapa()
        {
            return GetKeyOrAdd("RUTA_MAPA", "http://66.165.133.50/Index.aspx");
        }
        public static string RutaImagesBanner()
        {
            return GetKeyOrAdd("RUTA_IMAGENES_BANNER", "http://www.ssoftcolombia.com/App_Themes/Imagenes/logo.png");
        }
        public static string RutaTrenesReserva(string sBockingId)
        {
            string sGsa = GetKeyOrAdd("TrenesGsa", "80260");
            return GetKeyOrAdd("RUTA_TRENES_RESERVA", "http://www.railengine.com:8080/ure/admin.do?re_oper=confmail&re_xml=true&re_gsa=" + sGsa + "&re_order=" + sBockingId);
        }
        public static string RutaDocumentosGen()
        {
            return GetKeyOrAdd("RUTA_DOCUMENTOSW", "http://www.tutiquete.com/Pagina/Documentos/");
        }
        public static string RutaPage()
        {
            string sPath = @"c:\Temp\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"Presentacion\"; }
            catch { sPath = @"c:\Temp\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static string IndexInicio()
        {
            string sPath = @"c:\Temp\";

            try { sPath = HttpContext.Current.Request.PhysicalApplicationPath + @"Presentacion\"; }
            catch { sPath = @"c:\Temp\"; }
            try
            {
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            catch { }
            return sPath;
        }
        public static void DocumentosTempElimina()
        {
            try
            {
                int iDias = 7;
                try
                {
                    iDias = int.Parse(GetKeyOrAdd("BorradoTempDias", "7"));
                }
                catch { }
                BorrarArchivos(DocumentosTempCrea(), iDias);
            }
            catch { }
        }
        public static void XMLDatasetElimina()
        {
            try
            {
                int iDias = 7;
                try
                {
                    iDias = int.Parse(GetKeyOrAdd("BorradoDatasetDias", "1"));
                }
                catch { }
                BorrarArchivos(XMLDatasetCrea(), iDias);
            }
            catch { }
        }
        public static string CrearFechaString()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
        public static void RedirectPaginaIni(string sPagina, bool bValue)
        {
            try
            {
                string sUrl = ObtenerUrl();
                clsParametros cParametros = new clsParametros();
                cParametros.Message = "RedirectPaginaIni - Se redirecciona la pagina";
                cParametros.Complemento = sPagina;
                cParametros.Info = "Llamada desde " + sUrl;
                ExceptionHandled.Publicar(cParametros);

                HttpContext.Current.Response.Redirect(sPagina, bValue);
            }
            catch { }
        }
        public static void RedirectPagina(string sPagina, bool bValue)
        {
            string sPaginaTemp = sPagina;
            try
            {
                string sUrl = ObtenerUrl();
                clsParametros cParametros = new clsParametros();
                cParametros.Message = "RedirectPagina - Se redirecciona la pagina";
                cParametros.Complemento = sPagina;
                cParametros.Info = "Llamada desde " + sUrl;
                ExceptionHandled.Publicar(cParametros);


                sPaginaTemp = AdicionaUser(sPaginaTemp);
                //HttpContext.Current.Response.Redirect(sPaginaTemp, bValue);
                HttpContext.Current.Response.Redirect(sPaginaTemp, bValue);
                //HttpContext.Current.Server.Transfer(sPaginaTemp);
            }
            catch { }
        }
        public static void RedirectPagina(string sPagina)
        {
            try
            {
                RedirectPagina(sPagina, true);
            }
            catch { }
        }
        public static string GeneraRedirectPagina(string sPagina)
        {
            string sPaginaTemp = sPagina;
            try
            {
                string sUrl = ObtenerUrl();
                clsParametros cParametros = new clsParametros();
                cParametros.Message = "GeneraRedirectPagina - Se redirecciona la pagina";
                cParametros.Complemento = sPagina;
                cParametros.Info = "Llamada desde " + sUrl;
                ExceptionHandled.Publicar(cParametros);


                sPaginaTemp = AdicionaUser(sPaginaTemp);
            }
            catch { }
            return sPaginaTemp;
        }
        public static void RedirectPaginaSesion(string sPagina, bool bValue)
        {
            string sPaginaTemp = sPagina;
            try
            {
                string sUrl = ObtenerUrl();
                clsParametros cParametros = new clsParametros();
                cParametros.Message = "RedirectPaginaSesion - Se redirecciona la pagina";
                cParametros.Complemento = sPagina;
                cParametros.Info = "Llamada desde " + sUrl;
                ExceptionHandled.Publicar(cParametros);

                sPaginaTemp = AdicionaUser(sPaginaTemp);
                sPaginaTemp = AdicionaSesion(sPaginaTemp);
                HttpContext.Current.Response.Redirect(sPaginaTemp, bValue);
                //HttpContext.Current.Server.Transfer(sPaginaTemp);
            }
            catch { }
        }
        public static void RedirectPaginaSesion(string sPagina)
        {
            try
            {
                RedirectPaginaSesion(sPagina, true);
            }
            catch { }
        }
        public static string GeneraRedirectPaginaSesion(string sPagina)
        {
            string sPaginaTemp = sPagina;
            try
            {
                sPaginaTemp = AdicionaUser(sPaginaTemp);
                sPaginaTemp = AdicionaSesion(sPaginaTemp);
            }
            catch { }
            return sPaginaTemp;
        }
        public static string AdicionaSesion(string sPagina)
        {
            string sPaginaNew = sPagina;
            try
            {
                //string sSesionId = new clsCacheControl().RecuperarSesionId();
                //string sUnion = "?";
                //string sSesion = "idSesion=";
                //if (!sPagina.Contains(sSesion))
                //{
                //    if (sPagina.Contains(sUnion))
                //        sUnion = "&";
                //    sPaginaNew += sUnion + sSesion + sSesionId;
                //}
            }
            catch { }
            return sPaginaNew;
        }
        public static string AdicionaUser(string sPagina)
        {
            string sPaginaNew = sPagina;
            string sParam = string.Empty;
            clsCache cCache = new csCache().cCache();
            try
            {
                if (GetKeyOrAdd("bMarcaBlanca", "False").ToUpper().Equals("TRUE"))
                {
                    bool bEntra = true;
                    if (HttpContext.Current.Request.QueryString["idC"] == null)
                    {
                        if (cCache != null)
                        {
                            if (cCache.Contacto != null)
                            {
                                if (cCache.Contacto != "0")
                                {
                                    sParam = "idC=" + cCache.Contacto;
                                }
                            }
                        }
                    }
                    if (HttpContext.Current.Request.QueryString["idE"] == null)
                    {
                        if (cCache != null)
                        {
                            if (cCache.Empresa != null)
                            {
                                if (cCache.Empresa != "0")
                                {
                                    if (sParam.Length.Equals(0))
                                        sParam = "idE=" + cCache.Empresa;
                                    else
                                        sParam = "&idE=" + cCache.Empresa;
                                    bEntra = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!sPagina.Contains("idE="))
                        {
                            sParam = "idE=" + HttpContext.Current.Request.QueryString["idE"];
                        }
                        bEntra = false;
                    }
                    if (bEntra)
                    {
                        if (sParam.Length.Equals(0))
                            sParam = "idE=" + GetKeyOrAdd("idEmpresa", "3");
                        else
                            sParam = "&idE=" + GetKeyOrAdd("idEmpresa", "3");
                    }
                    if (!sParam.Length.Equals(0))
                    {
                        string sUnion = "?";
                        if (sPagina.Contains(sUnion))
                            sUnion = "&";
                        sPaginaNew += sUnion + sParam;
                    }
                    sPaginaNew = AdicionaSesion(sPaginaNew);
                }
            }
            catch { }
            return sPaginaNew;
        }
        //Modificacion Faustino ECU
        public static string[] RetornaArregloPos(string sText, int iCant)
        {
            if (iCant.Equals(0))
            {
                iCant = sText.Length;
            }
            string[] slText = new string[iCant];
            try
            {
                for (int i = 0; i < iCant; i++)
                {
                    slText[i] = sText.Substring(i, 1);
                }
            }
            catch { }
            return slText;
        }
    }
}
