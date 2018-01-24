using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;
using Ssoft.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;
using Ssoft.Rules.Generales;
using Ssoft.ValueObjects;
using Ssoft.Rules.Administrador;
using Ssoft.Rules.Reservas;
using Ssoft.Sql;
using Ssoft.DataNet;
using Ssoft.Rules.WebServices;
using System.Data.Linq;
using System.Data;
using System.Linq;
using System.Diagnostics;
using Ssoft.Rules.General;
using Ssoft.Rules.Corporativo;
using SsoftQuery.Vuelos;
namespace Ssoft.Rules.Pagina
{
    public class clsVuelos
    {
        DataSet dsSabreAir = new DataSet();
        DataSql sdSql = new DataSql();

        private const string CONTAIN = "CORP ID/ACCNT CODE USED";

        List<VO_Passenger> objPasajeros = new List<VO_Passenger>();
        private string sIdioma;
        private string sAplicacion;
        private string sFormatoFechaBD;
        private const int iEdadMin = 2;
        private const int iEdadMAx = 12;
        // Definicion de tablas
        public const string TABLA_SEGMENT = "FlightSegment";
        public const string TABLA_ORIGENDESTINATIONS = "OriginDestinationOptions";
        public const string TABLA_ORIGENDESTINATION = "OriginDestinationOption";
        public const string TABLA_AIRITINERARY = "AirItinerary";
        public const string TABLA_PRICEDITINERARY = "PricedItinerary";
        public const string TABLA_PRICEDITINERARIES = "PricedItineraries";
        public const string TABLA_AIRLOWFARESHEAR = "OTA_AirLowFareSearchRS";

        public const string TABLA_DEPARTURE = "DepartureAirport";
        public const string TABLA_ARRIVAL = "ArrivalAirport";

        public const string TABLA_DEPARTURE_TIME_ZONE = "DepartureTimeZone";
        public const string TABLA_ARRIVAL_TIME_ZONE = "ArrivalTimeZone";

        public const string TABLA_OPERATING = "OperatingAirline";
        public const string TABLA_EQUIPEMENT = "Equipment";
        public const string TABLA_MARKETING_AIRLINE = "MarketingAirline";
        public const string TABLA_MARKETING_CABIN = "MarketingCabin";
        public const string TABLA_TPA_EXT = "TPA_Extensions";
        public const string TABLA_BOOKCLASS = "BookingClassAvail";
        //Prices
        public const string TABLA_AIRITINERARYPRICE = "AirItineraryPricingInfo";
        public const string TABLA_INTTOTALFARE = "ItinTotalFare";
        public const string TABLA_TOTALFARE = "TotalFare";
        public const string TABLA_PTC_FAREINFO = "PTC_FareInfo";
        public const string TABLA_PTC_FAREBREAK = "PTC_FareBreakdown";
        public const string TABLA_BASEFARE = "BaseFare";
        public const string TABLA_PASSENGERFARE = "PassengerFare";
        public const string TABLA_PASSENGERTYPE = "PassengerTypeQuantity";
        public const string TABLA_EQUIVFARE = "EquivFare";
        public const string TABLA_TAXES = "Taxes";
        public const string TABLA_TAX = "Tax";
        public const string COLUMN_WEBSERVICES = "Ws";
        public const string COLUMN_CONDICION = "Condicion";
        public const string COLUMN_RUTA = "Ruta";

        private string sFormatoFecha = clsSesiones.getFormatoFecha();
        private string FORMATO_NUMEROS_VIEW = clsValidaciones.GetKeyOrAdd("sFormatoView", "#,##0");

        public clsVuelos()
        {
            sIdioma = clsSesiones.getIdioma();
            sAplicacion = clsSesiones.getAplicacion().ToString();
            sFormatoFecha = clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");
            sFormatoFechaBD = clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");
            sdSql.Conexion = clsSesiones.getConexion();
        }

        #region [METODOS]

        #region [GENERALES]

     

        #region BFM

        public void ModificarDatasetSabreBFMAir(DataSet dsSabreAir, Enum_TipoTrayecto EnumTipoTrayecto, Enum_TipoVuelo EnumTipoVuelo)
        {
            clsParametros cParametros = new clsParametros();
            /*METODO QUE INICIA LA MODIFICAION DEL DATASET DE SABRE*/
            if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
            {
                /*OBTENEMOS EL TIPO DE VUELO*/
                try
                {
                    /*ESTABLECEMOS LA CULTURA PARA EL DATASET*/
                    dsSabreAir.Locale = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                }
                catch
                {
                }
                try
                {
                    AdicionarWs(dsSabreAir);
                }
                catch
                {
                }
                try
                {
                    GetMarckup(dsSabreAir);
                }
                catch
                {
                }
                try
                {
                    /*MODIFICAMOS LA TABLA DE FlightSegmento CREAMOS MAS COLUMNAS Y AGREGAMOS VALIDACIONES*/
                    ModificarTablaFlightSegmento(dsSabreAir);
                }
                catch
                {
                }
         
                try
                {
                    ModificarBFMDG(dsSabreAir);
                }
                catch
                {
                }
                /*VERIFICAMOS LOS VALORES NACIONALES E INTERNACIONALES*/
                try
                {
                    /*MODIFICAMOS LA TABLA DE PassengerFare CREAMOS MAS COLUMNAS Y AGREGAMOS VALIDACIONES*/
                    Modificar_Tarifas_Equivalentes(dsSabreAir, EnumTipoVuelo);
                }
                catch
                {
                }

                try
                {
                    /*MODIFICAMOS LA TABLA DE PassengerFare CREAMOS MAS COLUMNAS Y AGREGAMOS VALIDACIONES*/
                    //ModificarTablaPassengerFare(dsSabreAir, EnumTipoVuelo);
                    ModificarTablaPassengerBFMFare(dsSabreAir, EnumTipoVuelo);
                }
                catch
                {
                }
                try
                {
                    ModificarTablaPassengerFareCode(dsSabreAir);
                }
                catch
                {
                }
                try
                {
                    /*MODIFICAMOS EL DATASET DE SABRE AGREGAMOS MAS TABLAS Y RELACIONES*/
                    SetRelacionesDsSabreAir(dsSabreAir);
                }
                catch
                {

                }

                try
                {
                    /*VALIDAMOS EL TIPO DE TRAYECTO SI ES NACIONAL O INTERNACIONAL FlightSegment*/
                    SetTipoTrayecto(dsSabreAir);
                }
                catch
                {
                }
                try
                {
                    /*AGREGAMOS LA TASA ADMINISTRATIVA*/
                    GetTasaAdmin(dsSabreAir, EnumTipoTrayecto, EnumTipoVuelo);
                }
                catch
                {
                }
                try
                {
                    /*AGREGAMOS EL VALOR TOTAL A LA TABLA DE ITINERAIO*/
                    ModificarItinerario(dsSabreAir);
                }
                catch
                {
                }
                try
                {
                    ModificarConsecutivo(dsSabreAir);
                }
                catch
                {
                }
                try
                {
                    ModificarValueView(dsSabreAir);
                }
                catch { }
                try
                {
                    /*ACEPTAMOS LOS CAMBIOS*/
                    dsSabreAir.AcceptChanges();
                    /*ASIGNAMOS EL DATASET A LA VARIABLE DE LA CLASE*/
                    this.dsSabreAir = dsSabreAir;
                   
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Modificamos lo datos y relaciones child en el datasabreAir
        /// </summary>
        /// <param name="dsSabreAir"></param>
        /// <param name="EnumTipoVuelo"></param>
        private void ModificarTablaPassengerBFMFare(DataSet dsSabreAir, Enum_TipoVuelo EnumTipoVuelo)
        {
            DataTable dtPassengerFare = dsSabreAir.Tables["PassengerFare"];
            DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
            DataTable dtPassengerTypeQuantity = dsSabreAir.Tables["PassengerTypeQuantity"];
            DataTable dtTaxes = dsSabreAir.Tables["Taxes"];
            DataTable dtPTC_FareBreakdown = dsSabreAir.Tables["PTC_FareBreakdown"];
            DataTable dtPTC_FareInfo = dsSabreAir.Tables["PTC_FareBreakdowns"];//dtPTC_FareInfo
            DataTable dtBaseFare = dsSabreAir.Tables["BaseFare"];
            DataTable dtEquivFare = dsSabreAir.Tables["EquivFare"];
            DataTable dtTax = dsSabreAir.Tables["Tax"];
            string sMonedaCop = clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP");
            string sMonedaUsd = clsValidaciones.GetKeyOrAdd("MonedaDolar", "USD");

            /*AGREGAMOS LA COLUMNA DE TASA EQUIVALENTE EN USD*/
            try
            {
                dtTax.Columns.Add("Tax_Amount_Usd", typeof(decimal));
            }
            catch { }
            /*AGREGAMOS LA COLUMNA A LA TABLA DEL LISTADO DE TASAS*/
            dtTaxes.Columns.Add("intTotalTax", typeof(decimal), "Sum(Child(Taxes_Tax).Amount)");
            /*TOTAL DE LA TA + ITA*/
            dtTaxes.Columns.Add("intTotalTax_Ta_Iva_Usd", typeof(decimal), "ISNULL(Sum(Child(Taxes_Tax).Tax_Amount_Usd), 0)");
            dtBaseFare.Columns.Add("DecimalBaseFare", typeof(decimal), "Convert(Amount, 'System.Decimal')");

            // Se adicionan columnas para la tasa
            try
            {
                dtTax.Columns.Add("strTaxAmountView", typeof(string));
            }
            catch { }
            try
            {
                dtTax.Columns.Add("strCurrencyCodeView", typeof(string));
            }
            catch { }
            dtTax.AcceptChanges();
            dtTaxes.Columns.Add("strTotalTaxView", typeof(string));

            /*AGREGAMOS LA COLUMNA A LA TABLA DEL LISTADO DE TASAS*/
            dtEquivFare.Columns.Add("DecimalEquivFare", typeof(decimal), "Convert(Amount, 'System.Decimal')");

            /*AGREGAMOS LA TABLA DE PASAJEROS */
            DataTable dtPasajeros = CreateTablePassenger();
            /*AGREGAMOS LA TABLA AL DATASET*/
            dsSabreAir.Tables.Add(dtPasajeros);

            StringBuilder strExpression_strTipoPasajero = new StringBuilder();
            /*VALIDACION DEL CODIGO DE TIPO PASAJERO*/
            strExpression_strTipoPasajero.Append("IIF(Code = 'ADT'");
            strExpression_strTipoPasajero.Append(",'Adulto'");
            strExpression_strTipoPasajero.Append(",IIF(Code = 'CNN'");
            strExpression_strTipoPasajero.Append(",'Niño'");
            for (int c = iEdadMin; c <= iEdadMAx; c++)
            {
                if (c < 10)
                    strExpression_strTipoPasajero.Append(",IIF(Code = 'C0" + c.ToString() + "'");
                else
                    strExpression_strTipoPasajero.Append(",IIF(Code = 'C" + c.ToString() + "'");

                strExpression_strTipoPasajero.Append(",'Niño " + c.ToString() + " Años'");
            }
            strExpression_strTipoPasajero.Append(",IIF(Code = 'INF'");
            strExpression_strTipoPasajero.Append(",'Infante'");
            strExpression_strTipoPasajero.Append(",Code");
            strExpression_strTipoPasajero.Append(")))");
            for (int i = iEdadMin; i <= iEdadMAx; i++)
            {
                strExpression_strTipoPasajero.Append(")");
            }
            /*AGREGA COLUMNA DE TIPO PASAJERO A LA TABLA DE TIPOS DE PASAJEROS Y CATIDADES*/
            //dtPassengerTypeQuantity.Columns.Add("strTipoPasajero", typeof(string), strExpression_strTipoPasajero.ToString());
            dtPassengerTypeQuantity.Columns.Add("strTipoPasajero", typeof(string));
            dtPassengerTypeQuantity.Columns.Add("strDetalleTipo", typeof(string));

            /*VALIDACION DEL CODIGO DE TIPO PASAJERO*/
            strExpression_strTipoPasajero = new StringBuilder();
            strExpression_strTipoPasajero.Append("IIF   ");
            strExpression_strTipoPasajero.Append(",'Adulto'");
            strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'CNN'");
            strExpression_strTipoPasajero.Append(",'Niño'");
            for (int c = iEdadMin; c <= iEdadMAx; c++)
            {
                if (c < 10)
                    strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'C0" + c.ToString() + "'");
                else
                    strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'C" + c.ToString() + "'");

                strExpression_strTipoPasajero.Append(",'Niño " + c.ToString() + " Años'");
            }
            strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'INF'");
            strExpression_strTipoPasajero.Append(",'Infante'");
            strExpression_strTipoPasajero.Append(",strCodTipoPasajero");
            strExpression_strTipoPasajero.Append(")))");
            for (int i = iEdadMin; i <= iEdadMAx; i++)
            {
                strExpression_strTipoPasajero.Append(")");
            }
            /*AGREGAMOS COLUMNAS A LA TABLA DE TARIFAS*/
            dtPassengerFare.Columns.Add("intTPA_Extensions", typeof(int));
            dtPassengerFare.Columns.Add("strDetalleTipo", typeof(string));
            dtPassengerFare.Columns.Add("strCodTipoPasajero", typeof(string));
            //dtPassengerFare.Columns.Add("strTipoPasajero", typeof(string), strExpression_strTipoPasajero.ToString());
            dtPassengerFare.Columns.Add("strTipoPasajero", typeof(string));
            dtPassengerFare.Columns.Add("intCantidad", typeof(int));
            dtPassengerFare.Columns.Add("intEquivFare", typeof(decimal));
            dtPassengerFare.Columns.Add("intEquivFare_Decimal", typeof(decimal));
            dtPassengerFare.Columns.Add("intBaseFare", typeof(decimal));

            dtPassengerFare.Columns.Add("intBaseFareUSD", typeof(decimal));

            dtPassengerFare.Columns.Add("intBaseFare_Decimal", typeof(int));
            dtPassengerFare.Columns.Add("intTotalFare", typeof(decimal));
            dtPassengerFare.Columns.Add("intTotalFare_Decimal", typeof(int)).DefaultValue = 0;

            dtPassengerFare.Columns.Add("strTipoMonedaBaseFare", typeof(string));
            dtPassengerFare.Columns.Add("strTipoMonedaTotalFare", typeof(string));

            /*IMPUESTOS TASAS TOTAL*/
            dtPassengerFare.Columns.Add("IntTotalImpuestosTasas", typeof(decimal), "ISNULL(Sum(Child(PassengerFare_Taxes).intTotalTax),0)");
            /*TOTAL TA + ITA EN DOLARES*/
            dtPassengerFare.Columns.Add("IntTotalImpuestos_Ta_Iva_Usd_Usd", typeof(decimal), "ISNULL(Sum(Child(PassengerFare_Taxes).intTotalTax_Ta_Iva_Usd),0)");

            dtPassengerFare.Columns.Add("strBaseFareView", typeof(string));
            dtPassengerFare.Columns.Add("strTotalImpuestosTasasView", typeof(string));
            dtPassengerFare.Columns.Add("strTotalTarifaConTaXPersonaView", typeof(string));
            dtPassengerFare.Columns.Add("strTotalTarifaConTaView", typeof(string));
            dtPassengerFare.Columns.Add("strTipoMonedaTotalFareView", typeof(string));

            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion == sMonedaCop)
            {
                switch (EnumTipoVuelo)
                {
                    case Enum_TipoVuelo.Nacional:

                        //validate the IdIteneray bcz the option have other trip in USD
                        //hceron 16042013

                        string sIntTotalTarifaConTA = "IIF(strTipoMonedaBaseFare = 'COP',((IntTotalImpuestosTasas + intBaseFare) * intCantidad),((IntTotalImpuestosTasas + intEquivFare) * intCantidad))";

                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), sIntTotalTarifaConTA);
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/

                        string sIntTotalTarifaConTA_Usd = "IIF(strTipoMonedaBaseFare = 'COP',(((IntTotalImpuestosTasas + intBaseFare) * intCantidad) /" + clsSesiones.GET_USD_SABRE() + "),(((IntTotalImpuestosTasas + intEquivFare) * intCantidad) / " + clsSesiones.GET_USD_SABRE() + "))";

                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), sIntTotalTarifaConTA_Usd);//"((IntTotalImpuestosTasas + intBaseFare) * intCantidad) /" + clsSesiones.GET_USD_SABRE()
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/

                        string sIntTotalTarifaConTaXPersona = "IIF(strTipoMonedaBaseFare = 'COP',(IntTotalImpuestosTasas + intBaseFare),(IntTotalImpuestosTasas + intEquivFare))";

                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), sIntTotalTarifaConTaXPersona);//"(IntTotalImpuestosTasas + intBaseFare)"
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        string IntTotalTarifaConTaXPersona_Usd = "IIF(strTipoMonedaBaseFare = 'COP',((IntTotalImpuestosTasas + intBaseFare) /" + clsSesiones.GET_USD_SABRE() + "),((IntTotalImpuestosTasas + intEquivFare)/ " + clsSesiones.GET_USD_SABRE() + "))";


                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), IntTotalTarifaConTaXPersona_Usd);//"(IntTotalImpuestosTasas + intBaseFare) /" + clsSesiones.GET_USD_SABRE()
                        break;
                    case Enum_TipoVuelo.Internacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "((IntTotalImpuestosTasas + intEquivFare) * intCantidad)");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intEquivFare) * intCantidad) / " + clsSesiones.GET_USD_SABRE());
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare)/ " + clsSesiones.GET_USD_SABRE());
                        break;
                    default:
                        break;
                }
            }
            else if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion == sMonedaUsd)
            {
                switch (EnumTipoVuelo)
                {
                    case Enum_TipoVuelo.Nacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intEquivFare) * intCantidad) ");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare) ");
                        break;
                    case Enum_TipoVuelo.Internacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intBaseFare) * intCantidad) ");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) ");
                        break;
                    default:
                        break;
                }
            }

            /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE X CANTIDAD DE PERSONAS*/
            dtPTC_FareBreakdown.Columns.Add("IntTotalTarifaConTAPasajeros", typeof(decimal), "Sum(Child(PTC_FareBreakdown_PassengerFare).IntTotalTarifaConTA)");
            dtPTC_FareInfo.Columns.Add("IntTotalSumaTarifaConTAPasajeros", typeof(decimal), "Sum(Child(PTC_FareBreakdowns_PTC_FareBreakdown).IntTotalTarifaConTAPasajeros)");
            /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE X CANTIDAD DE PERSONAS EN USD*/
            dtPTC_FareBreakdown.Columns.Add("IntTotalTarifaConTAPasajeros_Usd", typeof(decimal), "Sum(Child(PTC_FareBreakdown_PassengerFare).IntTotalTarifaConTA_Usd)");
            dtPTC_FareInfo.Columns.Add("IntTotalSumaTarifaConTAPasajeros_Usd", typeof(decimal), "Sum(Child(PTC_FareBreakdowns_PTC_FareBreakdown).IntTotalTarifaConTAPasajeros_Usd)");
            decimal dValorDolar = Convert.ToDecimal(clsSesiones.GET_USD_SABRE());
            /*RECORREMOS TODAS LAS RELACIONES HASTA LLEGAR PASSENGER FARE*/
            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItineraryPricingInfo_PTC_FareBreakdowns"))
                    {
                        foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("PTC_FareBreakdowns_PTC_FareBreakdown"))
                        {
                            foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                            {
                                switch (EnumTipoVuelo)
                                {
                                    case Enum_TipoVuelo.Nacional:
                                        try
                                        {
                                            if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString().Equals(sMonedaCop))
                                            {
                                                drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound((drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()));
                                            }
                                            else
                                            {
                                                drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound((drRelacionCuatro.GetChildRows("PassengerFare_EquivFare")[0]["DecimalEquivFare"].ToString()));
                                            }
                                        }
                                        catch
                                        { drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()); ; }
                                        break;
                                    case Enum_TipoVuelo.Internacional:

                                        try
                                        { drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_EquivFare")[0]["Amount"].ToString()); }
                                        catch
                                        { drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()); }
                                        try
                                        { drRelacionCuatro["intBaseFareUSD"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()); }
                                        catch
                                        { drRelacionCuatro["intBaseFareUSD"] = "0"; }

                                        break;
                                    default:
                                        break;
                                }

                                try
                                { drRelacionCuatro["strTipoMonedaBaseFare"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["CurrencyCode"].ToString(); }
                                catch
                                { drRelacionCuatro["strTipoMonedaBaseFare"] = "***"; }

                                try
                                { drRelacionCuatro["intBaseFare_Decimal"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalPlaces"].ToString(); }
                                catch
                                { drRelacionCuatro["intBaseFare_Decimal"] = "0"; }

                                try
                                { drRelacionCuatro["intEquivFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_EquivFare")[0]["DecimalEquivFare"].ToString()); }
                                catch
                                { drRelacionCuatro["intEquivFare"] = "0"; }
                                try
                                { drRelacionCuatro["intEquivFare_Decimal"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalPlaces"].ToString(); }
                                catch
                                { drRelacionCuatro["intEquivFare_Decimal"] = "0"; }

                                try
                                { drRelacionCuatro["intTotalFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_TotalFare")[0]["Amount"].ToString()); }
                                catch
                                { drRelacionCuatro["intTotalFare"] = "0"; }

                                try
                                { drRelacionCuatro["strTipoMonedaTotalFare"] = drRelacionCuatro.GetChildRows("PassengerFare_TotalFare")[0]["CurrencyCode"].ToString(); }
                                catch
                                { drRelacionCuatro["strTipoMonedaTotalFare"] = "***"; }

                                try
                                { drRelacionCuatro["intTotalFare_Decimal"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalPlaces"].ToString(); }
                                catch
                                { drRelacionCuatro["intTotalFare_Decimal"] = "0"; }

                                try
                                { drRelacionCuatro["intTPA_Extensions"] = drRelacionCuatro.GetChildRows("PassengerFare_TPA_Extensions")[0]["TPA_Extensions_Id"].ToString(); }
                                catch
                                { drRelacionCuatro["intTPA_Extensions"] = "0"; }

                                foreach (DataRow drTipoPasajero in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerTypeQuantity"))
                                {
                                    try
                                    { drRelacionCuatro["strCodTipoPasajero"] = drTipoPasajero["Code"].ToString(); }
                                    catch
                                    { drRelacionCuatro["strCodTipoPasajero"] = "ADT"; }

                                    try
                                    { drRelacionCuatro["intCantidad"] = drTipoPasajero["Quantity"].ToString(); }
                                    catch
                                    { drRelacionCuatro["intCantidad"] = "0"; }
                                    try
                                    { drRelacionCuatro["strTipoPasajero"] = drTipoPasajero["Code"].ToString(); }
                                    catch
                                    { drRelacionCuatro["strTipoPasajero"] = "ADT"; }
                                    try
                                    { drRelacionCuatro["strDetalleTipo"] = drTipoPasajero["Code"].ToString(); }
                                    catch
                                    { drRelacionCuatro["strDetalleTipo"] = "ADT"; }
                                }
                                if (clsValidaciones.GetKeyOrAdd("CotizaInfante", "true").ToUpper().Equals("FALSE"))
                                {
                                    if (EnumTipoVuelo.Equals(Enum_TipoVuelo.Nacional))
                                    {
                                        if (drRelacionCuatro["strCodTipoPasajero"].ToString().Equals("INF"))
                                        {
                                            drRelacionCuatro["intBaseFare"] = "0";
                                            foreach (DataRow drTaxes in drRelacionCuatro.GetChildRows("PassengerFare_Taxes"))
                                            {
                                                foreach (DataRow drTax in drTaxes.GetChildRows("Taxes_Tax"))
                                                {
                                                    drTax["Amount"] = 0;
                                                    drTax["Tax_Amount_Usd"] = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ModificarBFMDG(DataSet dsSabreAir)
        {
          
            DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
            string sRutaImagen = clsValidaciones.ObtenerUrlImages();
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            Enum_TipoVuelo eTipoSalida = vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida;
            Enum_TipoVuelo eTipoVuelo = vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo;
            bool bDg = true;
            if (eTipoVuelo.Equals(Enum_TipoVuelo.Internacional))
            {
                if (eTipoSalida.Equals(Enum_TipoVuelo.Nacional))
                {
                    bDg = false;
                    string sCodeImpSalida = clsValidaciones.GetKeyOrAdd("CodeSabreDG", "DG");
                    string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");

                    foreach (DataRow drPricedItinerary in dtItinerario.Rows)
                    {
                        drPricedItinerary["strTextoDG"] = "Esta tarifa no incluye impuesto de salida de " + sPaisDefault;
                        drPricedItinerary["bolImpuestos"] = false;

                        drPricedItinerary["imgOferta"] = sRutaImagen + "spacer.gif";
                        drPricedItinerary["imgConvenio"] = sRutaImagen + "spacer.gif";

                        foreach (DataRow drAirItineraryPricingInfo in drPricedItinerary.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                        {
                            foreach (DataRow drPTC_FareInfo in drAirItineraryPricingInfo.GetChildRows("AirItineraryPricingInfo_PTC_FareBreakdowns"))
                            {
                                foreach (DataRow drPTC_FareBreakdown in drPTC_FareInfo.GetChildRows("PTC_FareBreakdowns_PTC_FareBreakdown"))
                                {
                                    foreach (DataRow dr_PassengerFare in drPTC_FareBreakdown.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                                    {
                                        foreach (DataRow drTaxes in dr_PassengerFare.GetChildRows("PassengerFare_Taxes"))
                                        {
                                            foreach (DataRow drTax in drTaxes.GetChildRows("Taxes_Tax"))
                                            {
                                                if (drTax["TaxCode"].ToString().Substring(0, 2).Equals(sCodeImpSalida))
                                                {
                                                    drPricedItinerary["strTextoDG"] = string.Empty;
                                                    drPricedItinerary["bolImpuestos"] = true;
                                                }
                                            }
                                        }
                                        foreach (DataRow drTPAExtensions in dr_PassengerFare.GetChildRows("PassengerFare_TPA_Extensions"))
                                        {
                                            foreach (DataRow drText in drTPAExtensions.GetChildRows("TPA_Extensions_Text"))
                                            {
                                                for (int i = 0; i < drText.ItemArray.Length; i++)
                                                {
                                                    if (drText[i].ToString().Contains(CONTAIN))
                                                    {
                                                        drPricedItinerary["imgConvenio"] = sRutaImagen + "convenios.gif";
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (bDg)
            {
                foreach (DataRow drPricedItinerary in dtItinerario.Rows)
                {
                    drPricedItinerary["bolImpuestos"] = true;

                    drPricedItinerary["imgOferta"] = sRutaImagen + "spacer.gif";
                    drPricedItinerary["imgConvenio"] = sRutaImagen + "spacer.gif";
                }
            }
        }
        #endregion



        public void ModificarDatasetSabreAirHoras(DataSet dsSabreAir, Enum_TipoTrayecto EnumTipoTrayecto, Enum_TipoVuelo EnumTipoVuelo)
        {
            clsParametros cParametros = new clsParametros();
            /*METODO QUE INICIA LA MODIFICAION DEL DATASET DE SABRE*/
            if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
            {
                try
                {
                    /*ESTABLECEMOS LA CULTURA PARA EL DATASET*/
                    dsSabreAir.Locale = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                }
                catch (Exception Ex)
                {
                    cParametros.Id = 0;
                    cParametros.Message = Ex.Message.ToString();
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = "System.Globalization.CultureInfo ";
                    ExceptionHandled.Publicar(cParametros);
                }
                try
                {
                    /*ESTABLECEMOS LA CULTURA PARA EL DATASET*/
                    AdicionarWs(dsSabreAir);
                }
                catch (Exception Ex)
                {
                    cParametros.Id = 0;
                    cParametros.Message = Ex.Message.ToString();
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = "AdicionWs";
                    ExceptionHandled.Publicar(cParametros);
                }
                try
                {
                    /*MODIFICAMOS LA TABLA DE FlightSegmento CREAMOS MAS COLUMNAS Y AGREGAMOS VALIDACIONES*/
                    ModificarTablaFlightSegmentoHoras(dsSabreAir);
                }
                catch (Exception Ex)
                {
                    cParametros.Id = 0;
                    cParametros.Message = Ex.Message.ToString();
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = "ModificarTablaFlightSegmento";
                    ExceptionHandled.Publicar(cParametros);
                }
                try
                {
                    ModificarClasesHoras(dsSabreAir);
                }
                catch (Exception Ex)
                {
                    cParametros.Id = 0;
                    cParametros.Message = Ex.Message.ToString();
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = "ModificarClasesHoras";
                    ExceptionHandled.Publicar(cParametros);
                }
                try
                {
                    /*MODIFICAMOS EL DATASET DE SABRE AGREGAMOS MAS TABLAS Y RELACIONES*/
                    SetRelacionesDsSabreAirHora(dsSabreAir);
                }
                catch (Exception Ex)
                {
                    cParametros.Id = 0;
                    cParametros.Message = Ex.Message.ToString();
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = "SetRelacionesDsSabreAir";
                    ExceptionHandled.Publicar(cParametros);
                }

                try
                {
                    /*VALIDAMOS EL TIPO DE TRAYECTO SI ES NACIONAL O INTERNACIONAL FlightSegment*/
                    SetTipoTrayecto(dsSabreAir);
                }
                catch (Exception Ex)
                {
                    cParametros.Id = 0;
                    cParametros.Message = Ex.Message.ToString();
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = "SetTipoTrayecto";
                    ExceptionHandled.Publicar(cParametros);
                }
                try
                {
                    ModificarConsecutivoHoras(dsSabreAir);
                }
                catch (Exception Ex)
                {
                    cParametros.Id = 0;
                    cParametros.Message = Ex.Message.ToString();
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = "ModificarConsecutivoHoras";
                    ExceptionHandled.Publicar(cParametros);
                }
                try
                {
                    /*ACEPTAMOS LOS CAMBIOS*/
                    dsSabreAir.AcceptChanges();
                    /*ASIGNAMOS EL DATASET A LA VARIABLE DE LA CLASE*/
                    this.dsSabreAir = dsSabreAir;
                }
                catch (Exception Ex)
                {
                    cParametros.Id = 0;
                    cParametros.Message = Ex.Message.ToString();
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Complemento = "AcceptChanges";
                    ExceptionHandled.Publicar(cParametros);
                }
            }
        }
        private void ModificarClasesHoras(DataSet dsSabreAir)
        {
            try
            {
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                DataTable dtOriginDestinationOption = dsSabreAir.Tables["OriginDestinationOption"];
                foreach (string sClases in vo_OTA_AirLowFareSearchLLSRQ.LsClase)
                {
                    clsDataNet.dsDataElimina("strMarketingCabin", sClases, dtOriginDestinationOption);
                }
                dsSabreAir.AcceptChanges();
            }
            catch { }
        }
        public void ModificarDatasetSabreAirCotiza(DataSet dsSabreAir, Enum_TipoTrayecto EnumTipoTrayecto, Enum_TipoVuelo EnumTipoVuelo)
        {
            clsParametros cParametros = new clsParametros();
            /*METODO QUE INICIA LA MODIFICAION DEL DATASET DE SABRE*/
            if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
            {
                /*OBTENEMOS EL TIPO DE VUELO*/

                try
                {
                    /*ESTABLECEMOS LA CULTURA PARA EL DATASET*/
                    dsSabreAir.Locale = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                }
                catch { }
                try
                {
                    AdicionarWs(dsSabreAir);
                }
                catch { }
                try
                {
                    GetMarckup(dsSabreAir);
                }
                catch { }
                try
                {
                    /*MODIFICAMOS LA TABLA DE FlightSegmento CREAMOS MAS COLUMNAS Y AGREGAMOS VALIDACIONES*/
                    ModificarTablaFlightSegmentoCotiza(dsSabreAir);
                }
                catch { }
                try
                {
                    ModificarDGCotiza(dsSabreAir);
                }
                catch { }
                try
                {
                    VerificarTaxCotiza(dsSabreAir);
                }
                catch { }
                /*VERIFICAMOS LOS VALORES NACIONALES E INTERNACIONALES*/
                try
                {
                    /*MODIFICAMOS LA TABLA DE PassengerFare CREAMOS MAS COLUMNAS Y AGREGAMOS VALIDACIONES*/
                    Modificar_Tarifas_Equivalentes(dsSabreAir, EnumTipoVuelo);
                }
                catch { }

                try
                {
                    /*MODIFICAMOS LA TABLA DE PassengerFare CREAMOS MAS COLUMNAS Y AGREGAMOS VALIDACIONES*/
                    ModificarTablaPassengerFareCotiza(dsSabreAir, EnumTipoVuelo);
                }
                catch { }
                try
                {
                    ModificarTablaPassengerFareCode(dsSabreAir);
                }
                catch { }
          
                try
                {
                    /*AGREGAMOS LA TASA ADMINISTRATIVA*/
                    GetTasaAdmin(dsSabreAir, EnumTipoTrayecto, EnumTipoVuelo);
                }
                catch { }
                try
                {
                    /*AGREGAMOS EL VALOR TOTAL A LA TABLA DE ITINERAIO*/
                    ModificarItinerarioCotiza(dsSabreAir);
                }
                catch { }
                try
                {
                    ModificarConsecutivoHoras(dsSabreAir);
                }
                catch { }
                try
                {
                    ModificarValueCotizaView(dsSabreAir);
                }
                catch { }
                try
                {
                    /*ACEPTAMOS LOS CAMBIOS*/
                    dsSabreAir.AcceptChanges();
                    /*ASIGNAMOS EL DATASET A LA VARIABLE DE LA CLASE*/
                    this.dsSabreAir = dsSabreAir;
                }
                catch { }
            }
        }
        public void ModificarDatasetSabreAirCotizaPlan(string sRph)
        {
            clsParametros cParametros = new clsParametros();
            DataSet dsData = clsSesiones.GetDatasetSabreAir();
            DataSet dsSabreAir = clsSesiones.GetDatasetSelectSabreAir();
            try
            {
                try
                {
                    string sWhere = "SequenceNumber = " + sRph;
                    DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
                    DataTable dtTablaRetornada = clsDataNet.dsDataWhere(sWhere, dtPricedItinerary);
                    sRph = dtTablaRetornada.Rows[0]["PricedItinerary_Id"].ToString();
                }
                catch { }
                DataTable dtItinerario = GetDtGetItinerario(sRph, dsSabreAir);

                DataTable dtPassengerQuantity = GetDtPassengerTypeQuantity(sRph, dsSabreAir);

                DataTable dtSegmento = GetDtFlightSegmento(sRph, dsSabreAir);

                DataTable dtTiposPasajeros = GetDtPassengerTypeQuantity(sRph, dsSabreAir);
                DataTable dtTarifas = ClonarTabla(dsSabreAir, "PassengerFare");
                DataTable dt_Tasas = ClonarTabla(dsSabreAir, "Tax");
                DataTable dt_PassengerFare = dsSabreAir.Tables["PassengerFare"];

                for (int c = 0; c < dtTiposPasajeros.Rows.Count; c++)
                {
                    DataTable dtTarifasUno = GetDtPassengerFare(dtTiposPasajeros.Rows[c]["PTC_FareBreakdown_Id"].ToString(), dsSabreAir);
                    clsDataNet.dsDataTableAdd(dtTarifas, dtTarifasUno);
                    for (int h = 0; h < dtTarifasUno.Rows.Count; h++)
                    {
                        DataTable dt_TasasUno = GetDtPassengerFareTax(dtTarifas.Rows[h]["PassengerFare_Id"].ToString(), dsSabreAir);
                        clsDataNet.dsDataTableAdd(dt_Tasas, dt_TasasUno);
                    }
                }
                DataTable dtItinerarioNew = dsSabreAir.Tables["PricedItinerary"];
                DataTable dtPassengerQuantityNew = dsSabreAir.Tables[TABLA_PASSENGERTYPE];
                DataTable dtSegmentoNew = dsSabreAir.Tables[TABLA_SEGMENT];
                DataTable dtTarifasNew = dsSabreAir.Tables[TABLA_PASSENGERFARE];
                DataTable dt_TasasNew = dsSabreAir.Tables[TABLA_TAX];

                DataTable dtItinerarioTemp = dsData.Tables["PricedItinerary"];
                DataTable dtPassengerQuantityTemp = dsData.Tables[TABLA_PASSENGERTYPE];
                DataTable dtSegmentoTemp = dsData.Tables[TABLA_SEGMENT];
                DataTable dtTarifasTemp = dsData.Tables[TABLA_PASSENGERFARE];
                DataTable dt_TasasTemp = dsData.Tables[TABLA_TAX];
                foreach (DataRow drItinerario in dtItinerarioNew.Rows)
                {
                    if (drItinerario["PricedItinerary_Id"].ToString().Equals(sRph))
                    {
                        drItinerario["intTotalPesos"] = dtItinerarioTemp.Rows[0]["intTotalPesos"];
                        drItinerario["intTotalUsd"] = dtItinerarioTemp.Rows[0]["intTotalUsd"];
                        break;
                    }
                }
                int l = dtTarifas.Rows.Count;
                for (int i = 0; i < l; i++)
                {
                    foreach (DataRow drTarifas in dtTarifasNew.Rows)
                    {
                        if (drTarifas["PTC_FareBreakdown_Id"].ToString().Equals(dtTarifas.Rows[i]["PTC_FareBreakdown_Id"].ToString()))
                        {
                            if (drTarifas["PassengerFare_Id"].ToString().Equals(dtTarifas.Rows[i]["PassengerFare_Id"].ToString()))
                            {
                                try
                                {
                                    drTarifas["intBaseFare"] = dtTarifasTemp.Rows[i]["intBaseFare"];
                                    drTarifas["intBaseFareUSD"] = dtTarifasTemp.Rows[i]["intBaseFareUSD"];
                                    drTarifas["intTotalFare"] = dtTarifasTemp.Rows[i]["intTotalFare"];
                                    drTarifas["intTotalImpuestosTasas"] = dtTarifasTemp.Rows[i]["intTotalImpuestosTasas"];
                                    drTarifas["IntTotalImpuestos_Ta_Iva_Usd_Usd"] = dtTarifasTemp.Rows[i]["IntTotalImpuestos_Ta_Iva_Usd_Usd"];
                                    drTarifas["IntTotalTarifaConTA"] = dtTarifasTemp.Rows[i]["IntTotalTarifaConTA"];
                                    drTarifas["IntTotalTarifaConTA_Usd"] = dtTarifasTemp.Rows[i]["IntTotalTarifaConTA_Usd"];
                                    drTarifas["IntTotalTarifaConTaXPersona"] = dtTarifasTemp.Rows[i]["IntTotalTarifaConTaXPersona"];
                                    drTarifas["IntTotalTarifaConTaXPersona_Usd"] = dtTarifasTemp.Rows[i]["IntTotalTarifaConTaXPersona_Usd"];
                                }
                                catch (Exception Ex1)
                                {
                                    string sMens = Ex1.Message;
                                }
                            }
                        }
                    }
                    i++;
                }
                int iTotal = dt_TasasTemp.Rows.Count - 1;
                int y = int.Parse(dt_TasasTemp.Rows[iTotal]["Taxes_id"].ToString());

                for (int p = 0; p <= y; p++)
                {
                    for (int k = 0; k <= iTotal; k++)
                    {
                        foreach (DataRow drTarifas in dtTarifasNew.Rows)
                        {
                            foreach (DataRow drTaxes in drTarifas.GetChildRows("PassengerFare_Taxes"))
                            {
                                if (drTarifas["PTC_FareBreakdown_Id"].ToString().Equals(dtTarifas.Rows[p]["PTC_FareBreakdown_Id"].ToString()))
                                {
                                    if (drTarifas["PassengerFare_Id"].ToString().Equals(dtTarifas.Rows[p]["PassengerFare_Id"].ToString()))
                                    {
                                        foreach (DataRow drTax in drTaxes.GetChildRows("Taxes_Tax"))
                                        {
                                            if (int.Parse(dt_TasasTemp.Rows[iTotal]["Taxes_id"].ToString()).Equals(p))
                                            {
                                                if (drTax["TaxCode"].ToString().Substring(0, 2).Equals(dt_TasasTemp.Rows[k]["TaxCode"].ToString().Substring(0, 2)))
                                                {
                                                    drTax["Amount"] = dt_TasasTemp.Rows[k]["Amount"];
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            try
            {
                dsSabreAir.AcceptChanges();
                clsSesiones.SetDatasetSabreAir(dsSabreAir);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "AcceptChanges";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        /// <summary>
        /// Metodo para actualizar las tarifas equivalentes cuando viene en moneda diferente a la cotizada
        /// </summary>
        /// <param name="dsSabreAir">DataSet de resultados de busqueda aerea</param>
        /// <remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-29
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          José Faustino Posas
        /// Fecha:          2011-12-20
        /// Descripción:    Se modifica el proceso cuando EquivFare llega con datos, estaba presentando la tarifa en dolares y las tasas en pesos (Tutiquete)
        private void Modificar_Tarifas_Equivalentes(DataSet dsSabreAir, Enum_TipoVuelo EnumTipoVuelo)
        {
            /*SI  TRAE TARIFAS EN DOLARES SE INTERCAMBIAN LAS TARIFAS POR PESOS COLOMBIANOS*/
            DataView dtVista_Filtro_Tarifas = dsSabreAir.Tables["BaseFare"].DefaultView;
            DataTable dtTarifas = dsSabreAir.Tables["BaseFare"];
            DataTable dtFiltroEquivalencia = dtVista_Filtro_Tarifas.ToTable(true, "CurrencyCode");
            DataTable dtEquivFare = dsSabreAir.Tables["EquivFare"];

            switch (EnumTipoVuelo)
            {
                case Enum_TipoVuelo.Nacional:
                    if (dtFiltroEquivalencia.Rows.Count > 1)
                    {
                        for (int c = 0; c < dtEquivFare.Rows.Count; c++)
                        {
                            try
                            {
                                dtTarifas.PrimaryKey = new DataColumn[] { dtTarifas.Columns["PassengerFare_Id"] };
                            }
                            catch { }
                            string int_Id_Fare = dtEquivFare.Rows[c]["PassengerFare_Id"].ToString();
                            //validacion cuando esNull
                            if (int_Id_Fare != null && !int_Id_Fare.Equals("")) dtTarifas.Rows.Find(int_Id_Fare).Delete();

                            DataRow drNewFila = dtTarifas.NewRow();
                            /*ADICIONAMOS LA TARIFA*/
                            drNewFila["Amount"] = dtEquivFare.Rows[c]["Amount"];
                            drNewFila["PassengerFare_Id"] = dtEquivFare.Rows[c]["PassengerFare_Id"];
                            drNewFila["CurrencyCode"] = dtEquivFare.Rows[c]["CurrencyCode"];
                            drNewFila["DecimalPlaces"] = dtEquivFare.Rows[c]["DecimalPlaces"];
                            dtTarifas.Rows.Add(drNewFila);
                        }
                        dtTarifas.AcceptChanges();
                    }
                    else
                    {
                        if (dtEquivFare.Rows.Count > 0)
                        {
                            for (int c = 0; c < dtEquivFare.Rows.Count; c++)
                            {
                                dtTarifas.PrimaryKey = new DataColumn[] { dtTarifas.Columns["PassengerFare_Id"] };
                                string int_Id_Fare = dtEquivFare.Rows[c]["PassengerFare_Id"].ToString();
                                //validacion cuando esNull
                                if (int_Id_Fare != null && !int_Id_Fare.Equals("")) dtTarifas.Rows.Find(int_Id_Fare).Delete();
                                DataRow drNewFila = dtTarifas.NewRow();
                                /*ADICIONAMOS LA TARIFA*/
                                drNewFila["Amount"] = dtEquivFare.Rows[c]["Amount"];
                                drNewFila["PassengerFare_Id"] = dtEquivFare.Rows[c]["PassengerFare_Id"];
                                drNewFila["CurrencyCode"] = dtEquivFare.Rows[c]["CurrencyCode"];
                                drNewFila["DecimalPlaces"] = dtEquivFare.Rows[c]["DecimalPlaces"];
                                dtTarifas.Rows.Add(drNewFila);
                            }
                            dtTarifas.AcceptChanges();
                        }
                        else
                        {
                            for (int c = 0; c < dtTarifas.Rows.Count; c++)
                            {
                                DataRow drNewFila = dtEquivFare.NewRow();
                                /*ADICIONAMOS LA TARIFA*/
                                drNewFila["Amount"] = dtTarifas.Rows[c]["Amount"];
                                drNewFila["PassengerFare_Id"] = dtTarifas.Rows[c]["PassengerFare_Id"];
                                drNewFila["CurrencyCode"] = dtTarifas.Rows[c]["CurrencyCode"];
                                drNewFila["DecimalPlaces"] = dtTarifas.Rows[c]["DecimalPlaces"];
                                dtEquivFare.Rows.Add(drNewFila);
                            }
                            dtEquivFare.AcceptChanges();
                        }
                    }
                    break;
                case Enum_TipoVuelo.Internacional:
                    if (dtFiltroEquivalencia.Rows.Count > 1)
                    {
                        for (int c = 0; c < dtEquivFare.Rows.Count; c++)
                        {
                            dtTarifas.PrimaryKey = new DataColumn[] { dtTarifas.Columns["PassengerFare_Id"] };
                            string int_Id_Fare = dtEquivFare.Rows[c]["PassengerFare_Id"].ToString();
                            dtTarifas.Rows.Find(int_Id_Fare).Delete();
                            DataRow drNewFila = dtTarifas.NewRow();
                            /*ADICIONAMOS LA TARIFA*/
                            drNewFila["Amount"] = dtEquivFare.Rows[c]["Amount"];
                            drNewFila["PassengerFare_Id"] = dtEquivFare.Rows[c]["PassengerFare_Id"];
                            drNewFila["CurrencyCode"] = dtEquivFare.Rows[c]["CurrencyCode"];
                            drNewFila["DecimalPlaces"] = dtEquivFare.Rows[c]["DecimalPlaces"];
                            dtTarifas.Rows.Add(drNewFila);
                        }
                        dtTarifas.AcceptChanges();
                    }
                    else
                    {
                        for (int c = 0; c < dtTarifas.Rows.Count; c++)
                        {
                            DataRow drNewFila = dtEquivFare.NewRow();
                            /*ADICIONAMOS LA TARIFA*/
                            drNewFila["Amount"] = dtTarifas.Rows[c]["Amount"];
                            drNewFila["PassengerFare_Id"] = dtTarifas.Rows[c]["PassengerFare_Id"];
                            drNewFila["CurrencyCode"] = dtTarifas.Rows[c]["CurrencyCode"];
                            drNewFila["DecimalPlaces"] = dtTarifas.Rows[c]["DecimalPlaces"];
                            dtEquivFare.Rows.Add(drNewFila);
                        }
                        dtEquivFare.AcceptChanges();
                    }
                    break;
                default:
                    break;
            }
        }
        private void ModificarTablaPassengerFare(DataSet dsSabreAir, Enum_TipoVuelo EnumTipoVuelo)
        {
            DataTable dtPassengerFare = dsSabreAir.Tables["PassengerFare"];
            DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
            DataTable dtPassengerTypeQuantity = dsSabreAir.Tables["PassengerTypeQuantity"];
            DataTable dtTaxes = dsSabreAir.Tables["Taxes"];
            DataTable dtPTC_FareBreakdown = dsSabreAir.Tables["PTC_FareBreakdown"];
            DataTable dtPTC_FareInfo = dsSabreAir.Tables["dtPTC_FareInfo"];//
            DataTable dtBaseFare = dsSabreAir.Tables["BaseFare"];
            DataTable dtEquivFare = dsSabreAir.Tables["EquivFare"];
            DataTable dtTax = dsSabreAir.Tables["Tax"];
            string sMonedaCop = clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP");
            string sMonedaUsd = clsValidaciones.GetKeyOrAdd("MonedaDolar", "USD");

            /*AGREGAMOS LA COLUMNA DE TASA EQUIVALENTE EN USD*/
            dtTax.Columns.Add("Tax_Amount_Usd", typeof(decimal));
            /*AGREGAMOS LA COLUMNA A LA TABLA DEL LISTADO DE TASAS*/
            dtTaxes.Columns.Add("intTotalTax", typeof(decimal), "Sum(Child(Taxes_Tax).Amount)");
            /*TOTAL DE LA TA + ITA*/
            dtTaxes.Columns.Add("intTotalTax_Ta_Iva_Usd", typeof(decimal), "ISNULL(Sum(Child(Taxes_Tax).Tax_Amount_Usd), 0)");
            dtBaseFare.Columns.Add("DecimalBaseFare", typeof(decimal), "Convert(Amount, 'System.Decimal')");

            // Se adicionan columnas para la tasa
            dtTax.Columns.Add("strTaxAmountView", typeof(string));
            dtTax.Columns.Add("strCurrencyCodeView", typeof(string));
            dtTax.AcceptChanges();
            dtTaxes.Columns.Add("strTotalTaxView", typeof(string));

            /*AGREGAMOS LA COLUMNA A LA TABLA DEL LISTADO DE TASAS*/
            dtEquivFare.Columns.Add("DecimalEquivFare", typeof(decimal), "Convert(Amount, 'System.Decimal')");

            /*AGREGAMOS LA TABLA DE PASAJEROS */
            DataTable dtPasajeros = CreateTablePassenger();
            /*AGREGAMOS LA TABLA AL DATASET*/
            dsSabreAir.Tables.Add(dtPasajeros);

            StringBuilder strExpression_strTipoPasajero = new StringBuilder();
            /*VALIDACION DEL CODIGO DE TIPO PASAJERO*/
            strExpression_strTipoPasajero.Append("IIF(Code = 'ADT'");
            strExpression_strTipoPasajero.Append(",'Adulto'");
            strExpression_strTipoPasajero.Append(",IIF(Code = 'CNN'");
            strExpression_strTipoPasajero.Append(",'Niño'");
            for (int c = iEdadMin; c <= iEdadMAx; c++)
            {
                if (c < 10)
                    strExpression_strTipoPasajero.Append(",IIF(Code = 'C0" + c.ToString() + "'");
                else
                    strExpression_strTipoPasajero.Append(",IIF(Code = 'C" + c.ToString() + "'");

                strExpression_strTipoPasajero.Append(",'Niño " + c.ToString() + " Años'");
            }
            strExpression_strTipoPasajero.Append(",IIF(Code = 'INF'");
            strExpression_strTipoPasajero.Append(",'Infante'");
            strExpression_strTipoPasajero.Append(",Code");
            strExpression_strTipoPasajero.Append(")))");
            for (int i = iEdadMin; i <= iEdadMAx; i++)
            {
                strExpression_strTipoPasajero.Append(")");
            }
            /*AGREGA COLUMNA DE TIPO PASAJERO A LA TABLA DE TIPOS DE PASAJEROS Y CATIDADES*/
       
            dtPassengerTypeQuantity.Columns.Add("strTipoPasajero", typeof(string));
            dtPassengerTypeQuantity.Columns.Add("strDetalleTipo", typeof(string));

            /*VALIDACION DEL CODIGO DE TIPO PASAJERO*/
            strExpression_strTipoPasajero = new StringBuilder();
            strExpression_strTipoPasajero.Append("IIF(strCodTipoPasajero = 'ADT'");
            strExpression_strTipoPasajero.Append(",'Adulto'");
            strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'CNN'");
            strExpression_strTipoPasajero.Append(",'Niño'");
            for (int c = iEdadMin; c <= iEdadMAx; c++)
            {
                if (c < 10)
                    strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'C0" + c.ToString() + "'");
                else
                    strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'C" + c.ToString() + "'");

                strExpression_strTipoPasajero.Append(",'Niño " + c.ToString() + " Años'");
            }
            strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'INF'");
            strExpression_strTipoPasajero.Append(",'Infante'");
            strExpression_strTipoPasajero.Append(",strCodTipoPasajero");
            strExpression_strTipoPasajero.Append(")))");
            for (int i = iEdadMin; i <= iEdadMAx; i++)
            {
                strExpression_strTipoPasajero.Append(")");
            }
            /*AGREGAMOS COLUMNAS A LA TABLA DE TARIFAS*/
            dtPassengerFare.Columns.Add("intTPA_Extensions", typeof(int));
            dtPassengerFare.Columns.Add("strDetalleTipo", typeof(string));
            dtPassengerFare.Columns.Add("strCodTipoPasajero", typeof(string));
            //dtPassengerFare.Columns.Add("strTipoPasajero", typeof(string), strExpression_strTipoPasajero.ToString());
            dtPassengerFare.Columns.Add("strTipoPasajero", typeof(string));
            dtPassengerFare.Columns.Add("intCantidad", typeof(int));
            dtPassengerFare.Columns.Add("intEquivFare", typeof(decimal));
            dtPassengerFare.Columns.Add("intEquivFare_Decimal", typeof(decimal));
            dtPassengerFare.Columns.Add("intBaseFare", typeof(decimal));

            dtPassengerFare.Columns.Add("intBaseFareUSD", typeof(decimal));

            dtPassengerFare.Columns.Add("intBaseFare_Decimal", typeof(int));
            dtPassengerFare.Columns.Add("intTotalFare", typeof(decimal));
            dtPassengerFare.Columns.Add("intTotalFare_Decimal", typeof(int)).DefaultValue = 0;

            dtPassengerFare.Columns.Add("strTipoMonedaBaseFare", typeof(string));
            dtPassengerFare.Columns.Add("strTipoMonedaTotalFare", typeof(string));

            /*IMPUESTOS TASAS TOTAL*/
            dtPassengerFare.Columns.Add("IntTotalImpuestosTasas", typeof(decimal), "ISNULL(Sum(Child(PassengerFare_Taxes).intTotalTax),0)");
            /*TOTAL TA + ITA EN DOLARES*/
            dtPassengerFare.Columns.Add("IntTotalImpuestos_Ta_Iva_Usd_Usd", typeof(decimal), "ISNULL(Sum(Child(PassengerFare_Taxes).intTotalTax_Ta_Iva_Usd),0)");

            dtPassengerFare.Columns.Add("strBaseFareView", typeof(string));
            dtPassengerFare.Columns.Add("strTotalImpuestosTasasView", typeof(string));
            dtPassengerFare.Columns.Add("strTotalTarifaConTaXPersonaView", typeof(string));
            dtPassengerFare.Columns.Add("strTotalTarifaConTaView", typeof(string));
            dtPassengerFare.Columns.Add("strTipoMonedaTotalFareView", typeof(string));

            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion == sMonedaCop)
            {
                switch (EnumTipoVuelo)
                {
                    case Enum_TipoVuelo.Nacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intBaseFare) * intCantidad) /" + clsSesiones.GET_USD_SABRE());
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) /" + clsSesiones.GET_USD_SABRE());
                        break;
                    case Enum_TipoVuelo.Internacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intEquivFare) * intCantidad) / " + clsSesiones.GET_USD_SABRE());
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare)/ " + clsSesiones.GET_USD_SABRE());
                        break;
                    default:
                        break;
                }
            }
            else if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion == sMonedaUsd)
            {
                switch (EnumTipoVuelo)
                {
                    case Enum_TipoVuelo.Nacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intEquivFare) * intCantidad) ");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare) ");
                        break;
                    case Enum_TipoVuelo.Internacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intBaseFare) * intCantidad) ");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) ");
                        break;
                    default:
                        break;
                }
            }

            /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE X CANTIDAD DE PERSONAS*/
            dtPTC_FareBreakdown.Columns.Add("IntTotalTarifaConTAPasajeros", typeof(decimal), "Sum(Child(PTC_FareBreakdown_PassengerFare).IntTotalTarifaConTA)");
            dtPTC_FareInfo.Columns.Add("IntTotalSumaTarifaConTAPasajeros", typeof(decimal), "Sum(Child(PTC_FareInfo_PTC_FareBreakdown).IntTotalTarifaConTAPasajeros)");
            /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE X CANTIDAD DE PERSONAS EN USD*/
            dtPTC_FareBreakdown.Columns.Add("IntTotalTarifaConTAPasajeros_Usd", typeof(decimal), "Sum(Child(PTC_FareBreakdown_PassengerFare).IntTotalTarifaConTA_Usd)");
            dtPTC_FareInfo.Columns.Add("IntTotalSumaTarifaConTAPasajeros_Usd", typeof(decimal), "Sum(Child(PTC_FareInfo_PTC_FareBreakdown).IntTotalTarifaConTAPasajeros_Usd)");
            decimal dValorDolar = Convert.ToDecimal(clsSesiones.GET_USD_SABRE());
            /*RECORREMOS TODAS LAS RELACIONES HASTA LLEGAR PASSENGER FARE*/
            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItineraryPricingInfo_PTC_FareInfo"))
                    {
                        foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("PTC_FareInfo_PTC_FareBreakdown"))
                        {
                            foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                            {
                                switch (EnumTipoVuelo)
                                {
                                    case Enum_TipoVuelo.Nacional:
                                        try
                                        {
                                            if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString().Equals(sMonedaCop))
                                            {
                                                drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound((drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()));
                                            }
                                            else
                                            {
                                                drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound((drRelacionCuatro.GetChildRows("PassengerFare_EquivFare")[0]["DecimalEquivFare"].ToString()));
                                            }
                                        }
                                        catch
                                        { drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()); ; }
                                        break;
                                    case Enum_TipoVuelo.Internacional:

                                        try
                                        { drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_EquivFare")[0]["Amount"].ToString()); }
                                        catch
                                        { drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()); }
                                        try
                                        { drRelacionCuatro["intBaseFareUSD"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()); }
                                        catch
                                        { drRelacionCuatro["intBaseFareUSD"] = "0"; }

                                        break;
                                    default:
                                        break;
                                }

                                try
                                { drRelacionCuatro["strTipoMonedaBaseFare"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["CurrencyCode"].ToString(); }
                                catch
                                { drRelacionCuatro["strTipoMonedaBaseFare"] = "***"; }

                                try
                                { drRelacionCuatro["intBaseFare_Decimal"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalPlaces"].ToString(); }
                                catch
                                { drRelacionCuatro["intBaseFare_Decimal"] = "0"; }

                                try
                                { drRelacionCuatro["intEquivFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_EquivFare")[0]["DecimalEquivFare"].ToString()); }
                                catch
                                { drRelacionCuatro["intEquivFare"] = "0"; }
                                try
                                { drRelacionCuatro["intEquivFare_Decimal"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalPlaces"].ToString(); }
                                catch
                                { drRelacionCuatro["intEquivFare_Decimal"] = "0"; }

                                try
                                { drRelacionCuatro["intTotalFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_TotalFare")[0]["Amount"].ToString()); }
                                catch
                                { drRelacionCuatro["intTotalFare"] = "0"; }

                                try
                                { drRelacionCuatro["strTipoMonedaTotalFare"] = drRelacionCuatro.GetChildRows("PassengerFare_TotalFare")[0]["CurrencyCode"].ToString(); }
                                catch
                                { drRelacionCuatro["strTipoMonedaTotalFare"] = "***"; }

                                try
                                { drRelacionCuatro["intTotalFare_Decimal"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalPlaces"].ToString(); }
                                catch
                                { drRelacionCuatro["intTotalFare_Decimal"] = "0"; }

                                try
                                { drRelacionCuatro["intTPA_Extensions"] = drRelacionCuatro.GetChildRows("PassengerFare_TPA_Extensions")[0]["TPA_Extensions_Id"].ToString(); }
                                catch
                                { drRelacionCuatro["intTPA_Extensions"] = "0"; }

                                foreach (DataRow drTipoPasajero in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerTypeQuantity"))
                                {
                                    try
                                    { drRelacionCuatro["strCodTipoPasajero"] = drTipoPasajero["Code"].ToString(); }
                                    catch
                                    { drRelacionCuatro["strCodTipoPasajero"] = "ADT"; }

                                    try
                                    { drRelacionCuatro["intCantidad"] = drTipoPasajero["Quantity"].ToString(); }
                                    catch
                                    { drRelacionCuatro["intCantidad"] = "0"; }
                                    try
                                    { drRelacionCuatro["strTipoPasajero"] = drTipoPasajero["Code"].ToString(); }
                                    catch
                                    { drRelacionCuatro["strTipoPasajero"] = "ADT"; }
                                    try
                                    { drRelacionCuatro["strDetalleTipo"] = drTipoPasajero["Code"].ToString(); }
                                    catch
                                    { drRelacionCuatro["strDetalleTipo"] = "ADT"; }
                                }
                                if (clsValidaciones.GetKeyOrAdd("CotizaInfante", "true").ToUpper().Equals("FALSE"))
                                {
                                    if (EnumTipoVuelo.Equals(Enum_TipoVuelo.Nacional))
                                    {
                                        if (drRelacionCuatro["strCodTipoPasajero"].ToString().Equals("INF"))
                                        {
                                            drRelacionCuatro["intBaseFare"] = "0";
                                            foreach (DataRow drTaxes in drRelacionCuatro.GetChildRows("PassengerFare_Taxes"))
                                            {
                                                foreach (DataRow drTax in drTaxes.GetChildRows("Taxes_Tax"))
                                                {
                                                    drTax["Amount"] = 0;
                                                    drTax["Tax_Amount_Usd"] = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void ModificarTablaPassengerFareCotiza(DataSet dsSabreAir, Enum_TipoVuelo EnumTipoVuelo)
        {
            DataTable dtPassengerFare = dsSabreAir.Tables["PassengerFare"];
            DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
            DataTable dtPassengerTypeQuantity = dsSabreAir.Tables["PassengerTypeQuantity"];
            DataTable dtTaxes = dsSabreAir.Tables["Taxes"];
            DataTable dtPTC_FareBreakdown = dsSabreAir.Tables["PTC_FareBreakdown"];
            DataTable dtBaseFare = dsSabreAir.Tables["BaseFare"];
            DataTable dtEquivFare = dsSabreAir.Tables["EquivFare"];
            DataTable dtTax = dsSabreAir.Tables["Tax"];

            ///*AGREGAMOS LA COLUMNA DE TASA EQUIVALENTE EN USD*/
            try { dtTax.Columns.Add("Tax_Amount_Usd", typeof(decimal)); }
            catch { }
            try { dtTax.Columns.Add("CurrencyCode", typeof(string), "COP"); }
            catch { }
            /*AGREGAMOS LA COLUMNA A LA TABLA DEL LISTADO DE TASAS*/
            dtTaxes.Columns.Add("intTotalTax", typeof(decimal), "Sum(Child(Taxes_Tax).Amount)");
            /*TOTAL DE LA TA + ITA*/
            dtTaxes.Columns.Add("intTotalTax_Ta_Iva_Usd", typeof(decimal), "ISNULL(Sum(Child(Taxes_Tax).Tax_Amount_Usd), 0)");
            dtBaseFare.Columns.Add("DecimalBaseFare", typeof(decimal), "Convert(Amount, 'System.Decimal')");
            // Se adicionan columnas para la tasa
            //dtTax.Columns.Add("strTaxAmountView", typeof(string));
            //dtTax.AcceptChanges();

            // Se adicionan columnas para la tasa

            try
            {
                dtTax.Columns.Add("strTaxAmountView", typeof(string));
            }
            catch { }
            try
            {
                dtTax.Columns.Add("strCurrencyCodeView", typeof(string));
            }
            catch { }
            dtTax.AcceptChanges();
            dtTaxes.Columns.Add("strTotalTaxView", typeof(string));

            /*AGREGAMOS LA COLUMNA A LA TABLA DEL LISTADO DE TASAS*/
            dtEquivFare.Columns.Add("DecimalEquivFare", typeof(decimal), "Convert(Amount, 'System.Decimal')");

            /*AGREGAMOS LA TABLA DE PASAJEROS */
            DataTable dtPasajeros = CreateTablePassenger();
            /*AGREGAMOS LA TABLA AL DATASET*/
            dsSabreAir.Tables.Add(dtPasajeros);

            StringBuilder strExpression_strTipoPasajero = new StringBuilder();
            /*VALIDACION DEL CODIGO DE TIPO PASAJERO*/
            strExpression_strTipoPasajero.Append("IIF(Code = 'ADT'");
            strExpression_strTipoPasajero.Append(",'Adulto'");
            strExpression_strTipoPasajero.Append(",IIF(Code = 'CNN'");
            strExpression_strTipoPasajero.Append(",'Niño'");
            for (int c = iEdadMin; c <= iEdadMAx; c++)
            {
                if (c < 10)
                    strExpression_strTipoPasajero.Append(",IIF(Code = 'C0" + c.ToString() + "'");
                else
                    strExpression_strTipoPasajero.Append(",IIF(Code = 'C" + c.ToString() + "'");

                strExpression_strTipoPasajero.Append(",'Niño " + c.ToString() + " Años'");
            }
            strExpression_strTipoPasajero.Append(",IIF(Code = 'INF'");
            strExpression_strTipoPasajero.Append(",'Infante'");
            strExpression_strTipoPasajero.Append(",Code");
            strExpression_strTipoPasajero.Append(")))");
            for (int i = iEdadMin; i <= iEdadMAx; i++)
            {
                strExpression_strTipoPasajero.Append(")");
            }
            /*AGREGA COLUMNA DE TIPO PASAJERO A LA TABLA DE TIPOS DE PASAJEROS Y CATIDADES*/
            //dtPassengerTypeQuantity.Columns.Add("strTipoPasajero", typeof(string), strExpression_strTipoPasajero.ToString());
            dtPassengerTypeQuantity.Columns.Add("strTipoPasajero", typeof(string));
            dtPassengerTypeQuantity.Columns.Add("strDetalleTipo", typeof(string));

            /*VALIDACION DEL CODIGO DE TIPO PASAJERO*/
            strExpression_strTipoPasajero = new StringBuilder();
            strExpression_strTipoPasajero.Append("IIF(strCodTipoPasajero = 'ADT'");
            strExpression_strTipoPasajero.Append(",'Adulto'");
            strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'CNN'");
            strExpression_strTipoPasajero.Append(",'Niño'");
            for (int c = iEdadMin; c <= iEdadMAx; c++)
            {
                if (c < 10)
                    strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'C0" + c.ToString() + "'");
                else
                    strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'C" + c.ToString() + "'");

                strExpression_strTipoPasajero.Append(",'Niño " + c.ToString() + " Años'");
            }
            strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'INF'");
            strExpression_strTipoPasajero.Append(",'Infante'");
            strExpression_strTipoPasajero.Append(",strCodTipoPasajero");
            strExpression_strTipoPasajero.Append(")))");
            for (int i = iEdadMin; i <= iEdadMAx; i++)
            {
                strExpression_strTipoPasajero.Append(")");
            }
            /*AGREGAMOS COLUMNAS A LA TABLA DE TARIFAS*/
            dtPassengerFare.Columns.Add("intTPA_Extensions", typeof(int));
            dtPassengerFare.Columns.Add("strCodTipoPasajero", typeof(string));
            dtPassengerFare.Columns.Add("strDetalleTipo", typeof(string));

            //dtPassengerFare.Columns.Add("strTipoPasajero", typeof(string), strExpression_strTipoPasajero.ToString());
            dtPassengerFare.Columns.Add("strTipoPasajero", typeof(string));
            dtPassengerFare.Columns.Add("intCantidad", typeof(int));
            dtPassengerFare.Columns.Add("intEquivFare", typeof(decimal));
            dtPassengerFare.Columns.Add("intEquivFare_Decimal", typeof(decimal));
            dtPassengerFare.Columns.Add("intBaseFare", typeof(decimal));

            dtPassengerFare.Columns.Add("intBaseFareUSD", typeof(decimal));

            dtPassengerFare.Columns.Add("intBaseFare_Decimal", typeof(int));
            dtPassengerFare.Columns.Add("intTotalFare", typeof(decimal));
            dtPassengerFare.Columns.Add("intTotalFare_Decimal", typeof(int)).DefaultValue = 0;

            dtPassengerFare.Columns.Add("strTipoMonedaBaseFare", typeof(string));
            dtPassengerFare.Columns.Add("strTipoMonedaTotalFare", typeof(string));

            /*IMPUESTOS TASAS TOTAL*/
            dtPassengerFare.Columns.Add("IntTotalImpuestosTasas", typeof(decimal), "ISNULL(Sum(Child(PassengerFare_Taxes).intTotalTax),0)");
            /*TOTAL TA + ITA EN DOLARES*/
            dtPassengerFare.Columns.Add("IntTotalImpuestos_Ta_Iva_Usd_Usd", typeof(decimal), "ISNULL(Sum(Child(PassengerFare_Taxes).intTotalTax_Ta_Iva_Usd),0)");

            dtPassengerFare.Columns.Add("strBaseFareView", typeof(string));
            dtPassengerFare.Columns.Add("strTotalImpuestosTasasView", typeof(string));
            dtPassengerFare.Columns.Add("strTotalTarifaConTaXPersonaView", typeof(string));
            dtPassengerFare.Columns.Add("strTotalTarifaConTaView", typeof(string));
            dtPassengerFare.Columns.Add("strTipoMonedaTotalFareView", typeof(string));

            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion == clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano"))
            {
                switch (EnumTipoVuelo)
                {
                    case Enum_TipoVuelo.Nacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intBaseFare) * intCantidad) /" + clsSesiones.GET_USD_SABRE());
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) /" + clsSesiones.GET_USD_SABRE());
                        break;
                    case Enum_TipoVuelo.Internacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intEquivFare) * intCantidad) / " + clsSesiones.GET_USD_SABRE());
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare)/ " + clsSesiones.GET_USD_SABRE());
                        break;
                    default:
                        break;
                }
            }
            else if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion == clsValidaciones.GetKeyOrAdd("MonedaDolar"))
            {
                switch (EnumTipoVuelo)
                {
                    case Enum_TipoVuelo.Nacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intEquivFare) * intCantidad) ");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare) ");
                        break;
                    case Enum_TipoVuelo.Internacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intBaseFare) * intCantidad) ");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) ");
                        break;
                    default:
                        break;
                }
            }

            /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE X CANTIDAD DE PERSONAS*/
            dtPTC_FareBreakdown.Columns.Add("IntTotalTarifaConTAPasajeros", typeof(decimal), "Sum(Child(PTC_FareBreakdown_PassengerFare).IntTotalTarifaConTA)");
            /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE X CANTIDAD DE PERSONAS EN USD*/
            dtPTC_FareBreakdown.Columns.Add("IntTotalTarifaConTAPasajeros_Usd", typeof(decimal), "Sum(Child(PTC_FareBreakdown_PassengerFare).IntTotalTarifaConTA_Usd)");
            decimal dValorDolar = Convert.ToDecimal(clsSesiones.GET_USD_SABRE());
            /*RECORREMOS TODAS LAS RELACIONES HASTA LLEGAR PASSENGER FARE*/
            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                {
                    foreach (DataRow drRelacionTres in drFilaRelacionUno.GetChildRows("AirItineraryPricingInfo_PTC_FareBreakdown"))
                    {
                        foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                        {
                            switch (EnumTipoVuelo)
                            {
                                case Enum_TipoVuelo.Nacional:
                                    try
                                    {
                                        if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString().Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano")))
                                        {
                                            drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound((drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()));
                                        }
                                        else
                                        {
                                            drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound((drRelacionCuatro.GetChildRows("PassengerFare_EquivFare")[0]["DecimalEquivFare"].ToString()));
                                        }
                                    }
                                    catch
                                    { drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()); ; }
                                    break;
                                case Enum_TipoVuelo.Internacional:

                                    try
                                    { drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_EquivFare")[0]["Amount"].ToString()); }
                                    catch
                                    { drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()); }
                                    try
                                    { drRelacionCuatro["intBaseFareUSD"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()); }
                                    catch
                                    { drRelacionCuatro["intBaseFareUSD"] = "0"; }

                                    break;
                                default:
                                    break;
                            }

                            try
                            { drRelacionCuatro["strTipoMonedaBaseFare"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["CurrencyCode"].ToString(); }
                            catch
                            { drRelacionCuatro["strTipoMonedaBaseFare"] = "***"; }

                            try
                            { drRelacionCuatro["intBaseFare_Decimal"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalPlaces"].ToString(); }
                            catch
                            { drRelacionCuatro["intBaseFare_Decimal"] = "0"; }

                            try
                            { drRelacionCuatro["intEquivFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_EquivFare")[0]["DecimalEquivFare"].ToString()); }
                            catch
                            { drRelacionCuatro["intEquivFare"] = "0"; }
                            try
                            { drRelacionCuatro["intEquivFare_Decimal"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalPlaces"].ToString(); }
                            catch
                            { drRelacionCuatro["intEquivFare_Decimal"] = "0"; }

                            try
                            { drRelacionCuatro["intTotalFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_TotalFare")[0]["Amount"].ToString()); }
                            catch
                            { drRelacionCuatro["intTotalFare"] = "0"; }

                            try
                            { drRelacionCuatro["strTipoMonedaTotalFare"] = drRelacionCuatro.GetChildRows("PassengerFare_TotalFare")[0]["CurrencyCode"].ToString(); }
                            catch
                            { drRelacionCuatro["strTipoMonedaTotalFare"] = "***"; }

                            try
                            { drRelacionCuatro["intTotalFare_Decimal"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalPlaces"].ToString(); }
                            catch
                            { drRelacionCuatro["intTotalFare_Decimal"] = "0"; }

                            try
                            { drRelacionCuatro["intTPA_Extensions"] = drRelacionCuatro.GetChildRows("PassengerFare_TPA_Extensions")[0]["TPA_Extensions_Id"].ToString(); }
                            catch
                            { drRelacionCuatro["intTPA_Extensions"] = "0"; }

                            foreach (DataRow drTipoPasajero in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerTypeQuantity"))
                            {
                                try
                                { drRelacionCuatro["strCodTipoPasajero"] = drTipoPasajero["Code"].ToString(); }
                                catch
                                { drRelacionCuatro["strCodTipoPasajero"] = "ADT"; }

                                try
                                { drRelacionCuatro["intCantidad"] = drTipoPasajero["Quantity"].ToString(); }
                                catch
                                { drRelacionCuatro["intCantidad"] = "0"; }
                                try
                                { drRelacionCuatro["strTipoPasajero"] = drTipoPasajero["Code"].ToString(); }
                                catch
                                { drRelacionCuatro["strTipoPasajero"] = "ADT"; }
                                try
                                { drRelacionCuatro["strDetalleTipo"] = drTipoPasajero["Code"].ToString(); }
                                catch
                                { drRelacionCuatro["strDetalleTipo"] = "ADT"; }
                            }
                            if (clsValidaciones.GetKeyOrAdd("CotizaInfante", "true").ToUpper().Equals("FALSE"))
                            {
                                if (EnumTipoVuelo.Equals(Enum_TipoVuelo.Nacional))
                                {
                                    if (drRelacionCuatro["strCodTipoPasajero"].ToString().Equals("INF"))
                                    {
                                        drRelacionCuatro["intBaseFare"] = "0";
                                        foreach (DataRow drTaxes in drRelacionCuatro.GetChildRows("PassengerFare_Taxes"))
                                        {
                                            foreach (DataRow drTax in drTaxes.GetChildRows("Taxes_Tax"))
                                            {
                                                drTax["Amount"] = 0;
                                                drTax["Tax_Amount_Usd"] = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ModificarTablaPassengerFareCode(DataSet dsSabreAir)
        {
            DataTable dtPassengerFare = dsSabreAir.Tables["PassengerFare"];
            DataTable dtPassengerTypeQuantity = dsSabreAir.Tables["PassengerTypeQuantity"];

            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            /*RECORREMOS TODAS LAS RELACIONES HASTA LLEGAR PASSENGER FARE*/
            List<VO_Pasajero> Lvo_Pasajero = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros;
           
            int hasCNN = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros.Count;

            #region Valiacion si existe Child en la busqueda
            //foreach (VO_Pasajero ICNN in vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros)
            //{
            //    if (ICNN.SCodigo.Equals("CNN"))
            //    {
            //        hasCNN = 1;
            //    } if (ICNN.SCodigo.Equals("CNN"))
            //    {
            //        hasCNN = 1;
            //    }
            //}
            #endregion

            foreach (DataRow drPassengerFare in dtPassengerFare.Rows)
            {
                for (int i = 0; i < vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros.Count; i++)
                {
                    string sCodigoTipo = drPassengerFare["strCodTipoPasajero"].ToString();
                    string sEdad = "";
                    if (sCodigoTipo.Contains("C0"))
                    {
                        sEdad = " (" + sCodigoTipo.Substring(sCodigoTipo.Length - 1, 1) + " Años)";
                        sCodigoTipo = "CNN";                        
                    }
                    else if (sCodigoTipo.Contains("C1"))
                    {
                        sEdad = " (" + sCodigoTipo.Substring(sCodigoTipo.Length - 2, 2) + " Años)";
                        sCodigoTipo = "CNN";
                    }
                    if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCodigo.Equals(sCodigoTipo))
                    {
                        drPassengerFare["strDetalleTipo"] = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCodeGen;
                        drPassengerFare["strTipoPasajero"] = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SDetalle + sEdad;
                        drPassengerFare["strCodTipoPasajero"] = sCodigoTipo;//"CNN";
                    }
                 
                }
               
            }
           
            foreach (DataRow drPassengerTypeQuantity in dtPassengerTypeQuantity.Rows)
            {
                for (int i = 0; i < vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros.Count; i++)
                {
                    string sCodigoTipoPax = drPassengerTypeQuantity["Code"].ToString();
                    string sEdad = "";
                    if (sCodigoTipoPax.Contains("C0"))
                    {
                        sEdad = " (" + sCodigoTipoPax.Substring(sCodigoTipoPax.Length - 1, 1) + " Años)";
                        sCodigoTipoPax = "CNN";
                    }
                    else if (sCodigoTipoPax.Contains("C1"))
                    {
                        sEdad = " (" + sCodigoTipoPax.Substring(sCodigoTipoPax.Length - 2, 2) + " Años)";
                        sCodigoTipoPax = "CNN";
                    }
                    if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCodigo.Equals(sCodigoTipoPax))
                    {
                        drPassengerTypeQuantity["strDetalleTipo"] = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCodeGen;
                        drPassengerTypeQuantity["strTipoPasajero"] = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SDetalle + sEdad;
                        drPassengerTypeQuantity["Code"] = sCodigoTipoPax;//"CNN";
                    }

                   
                }
               
            }
        }


        public DataTable CreateTablePassenger()
        {
            DataTable dtPasajeros = new DataTable("dtPasajeros");
            dtPasajeros.Columns.Add("strTipoPasajero", typeof(string));
            dtPasajeros.Columns.Add("strCode", typeof(string));
            dtPasajeros.Columns.Add("strDetalleTipo", typeof(string));
            dtPasajeros.Columns.Add("intIdPasajero", typeof(int)).AutoIncrement = true;
            dtPasajeros.Columns["intIdPasajero"].AutoIncrementSeed = 1;
            dtPasajeros.Columns["intIdPasajero"].AutoIncrementStep = 1;
            dtPasajeros.PrimaryKey = new DataColumn[] { dtPasajeros.Columns["intIdPasajero"] };
            dtPasajeros.Columns.Add("strTrato", typeof(String));
            dtPasajeros.Columns.Add("strTelefono", typeof(String));
            dtPasajeros.Columns.Add("strEmail", typeof(String));
            dtPasajeros.Columns.Add("intEdad", typeof(String));
            dtPasajeros.Columns.Add("blInfante", typeof(Boolean), "IIF(strDetalleTipo = 'INF',TRUE,FALSE)");
            dtPasajeros.Columns.Add("strPrimerNombre", typeof(string));
            dtPasajeros.Columns.Add("strPrimerApellido", typeof(string));
            dtPasajeros.Columns.Add("intPTC_FareBreakdown_Id", typeof(int));
            dtPasajeros.Columns.Add("strPasajeroFrecuente", typeof(string));
            dtPasajeros.Columns.Add("strAerolinea", typeof(string));
            dtPasajeros.Columns.Add("strFechaNacimiento", typeof(string));
            dtPasajeros.Columns.Add("strTipoDocumento", typeof(string));
            dtPasajeros.Columns.Add("strDocumento", typeof(string));
            dtPasajeros.Columns.Add("strFee", typeof(string));
            dtPasajeros.Columns.Add("strGenero", typeof(string));
            return dtPasajeros;
        }
        private void ModificarDG(DataSet dsSabreAir)
        {
            ////Utils.Utils cUtil = new Utils.Utils();
            //string sEstructura = cUtil.DatasetStructura(dsSabreAir);
            DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
            string sRutaImagen = clsValidaciones.ObtenerUrlImages();
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            Enum_TipoVuelo eTipoSalida = vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida;
            Enum_TipoVuelo eTipoVuelo = vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo;
            bool bDg = true;
            if (eTipoVuelo.Equals(Enum_TipoVuelo.Internacional))
            {
                if (eTipoSalida.Equals(Enum_TipoVuelo.Nacional))
                {
                    bDg = false;
                    string sCodeImpSalida = clsValidaciones.GetKeyOrAdd("CodeSabreDG", "DG");
                    string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");

                    foreach (DataRow drPricedItinerary in dtItinerario.Rows)
                    {
                        drPricedItinerary["strTextoDG"] = "Esta tarifa no incluye impuesto de salida de " + sPaisDefault;
                        drPricedItinerary["bolImpuestos"] = false;

                        drPricedItinerary["imgOferta"] = sRutaImagen + "spacer.gif";
                        drPricedItinerary["imgConvenio"] = sRutaImagen + "spacer.gif";

                        foreach (DataRow drAirItineraryPricingInfo in drPricedItinerary.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                        {
                            foreach (DataRow drPTC_FareInfo in drAirItineraryPricingInfo.GetChildRows("AirItineraryPricingInfo_PTC_FareInfo"))
                            {
                                foreach (DataRow drPTC_FareBreakdown in drPTC_FareInfo.GetChildRows("PTC_FareInfo_PTC_FareBreakdown"))
                                {
                                    foreach (DataRow dr_PassengerFare in drPTC_FareBreakdown.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                                    {
                                        foreach (DataRow drTaxes in dr_PassengerFare.GetChildRows("PassengerFare_Taxes"))
                                        {
                                            foreach (DataRow drTax in drTaxes.GetChildRows("Taxes_Tax"))
                                            {
                                                if (drTax["TaxCode"].ToString().Substring(0, 2).Equals(sCodeImpSalida))
                                                {
                                                    drPricedItinerary["strTextoDG"] = string.Empty;
                                                    drPricedItinerary["bolImpuestos"] = true;
                                                }
                                            }
                                        }
                                        foreach (DataRow drTPAExtensions in dr_PassengerFare.GetChildRows("PassengerFare_TPA_Extensions"))
                                        {
                                            foreach (DataRow drText in drTPAExtensions.GetChildRows("TPA_Extensions_Text"))
                                            {
                                                for (int i = 0; i < drText.ItemArray.Length; i++)
                                                {
                                                    if (drText[i].ToString().Contains(CONTAIN))
                                                    {
                                                        drPricedItinerary["imgConvenio"] = sRutaImagen + "convenios.gif";
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (bDg)
            {
                foreach (DataRow drPricedItinerary in dtItinerario.Rows)
                {
                    drPricedItinerary["bolImpuestos"] = true;

                    drPricedItinerary["imgOferta"] = sRutaImagen + "spacer.gif";
                    drPricedItinerary["imgConvenio"] = sRutaImagen + "spacer.gif";
                }
            }
        }
        private void ModificarDGCotiza(DataSet dsSabreAir)
        {
            DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
            string sRutaImagen = clsValidaciones.ObtenerUrlImages();
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            Enum_TipoVuelo eTipoSalida = vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida;
            Enum_TipoVuelo eTipoVuelo = vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo;
            bool bDg = true;
            if (eTipoVuelo.Equals(Enum_TipoVuelo.Internacional))
            {
                if (eTipoSalida.Equals(Enum_TipoVuelo.Nacional))
                {
                    bDg = false;
                    string sCodeImpSalida = clsValidaciones.GetKeyOrAdd("CodeSabreDG", "DG");
                    string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");

                    foreach (DataRow drPricedItinerary in dtItinerario.Rows)
                    {
                        drPricedItinerary["strTextoDG"] = "Esta tarifa no incluye impuesto de salida de " + sPaisDefault;
                        drPricedItinerary["bolImpuestos"] = false;

                        drPricedItinerary["imgOferta"] = sRutaImagen + "spacer.gif";
                        drPricedItinerary["imgConvenio"] = sRutaImagen + "spacer.gif";

                        foreach (DataRow drAirItineraryPricingInfo in drPricedItinerary.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                        {
                            foreach (DataRow drPTC_FareBreakdown in drAirItineraryPricingInfo.GetChildRows("AirItineraryPricingInfo_PTC_FareBreakdown"))
                            {
                                foreach (DataRow dr_PassengerFare in drPTC_FareBreakdown.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                                {
                                    foreach (DataRow drTaxes in dr_PassengerFare.GetChildRows("PassengerFare_Taxes"))
                                    {
                                        foreach (DataRow drTax in drTaxes.GetChildRows("Taxes_Tax"))
                                        {
                                            if (drTax["TaxCode"].ToString().Substring(0, 2).Equals(sCodeImpSalida))
                                            {
                                                drPricedItinerary["strTextoDG"] = string.Empty;
                                                drPricedItinerary["bolImpuestos"] = true;
                                            }
                                        }
                                    }
                                    foreach (DataRow drTPAExtensions in dr_PassengerFare.GetChildRows("PassengerFare_TPA_Extensions"))
                                    {
                                        foreach (DataRow drText in drTPAExtensions.GetChildRows("TPA_Extensions_Text"))
                                        {
                                            for (int i = 0; i < drText.ItemArray.Length; i++)
                                            {
                                                if (drText[i].ToString().Contains(CONTAIN))
                                                {
                                                    drPricedItinerary["imgConvenio"] = sRutaImagen + "convenios.gif";
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (bDg)
            {
                foreach (DataRow drPricedItinerary in dtItinerario.Rows)
                {
                    drPricedItinerary["bolImpuestos"] = true;

                    drPricedItinerary["imgOferta"] = sRutaImagen + "spacer.gif";
                    drPricedItinerary["imgConvenio"] = sRutaImagen + "spacer.gif";
                }
            }
        }
        private void VerificarTaxCotiza(DataSet dsSabreAir)
        {
            DataTable dtTax = dsSabreAir.Tables["Tax"];

            /*AGREGAMOS LA COLUMNA DE TASA EQUIVALENTE EN USD*/
            try { dtTax.Columns.Add("Tax_Amount_Usd", typeof(decimal)); }
            catch { }
            try { dtTax.Columns.Add("CurrencyCode", typeof(string)); }
            catch { }
            dtTax.AcceptChanges();

            DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
            try
            {
                foreach (DataRow drPricedItinerary in dtItinerario.Rows)
                {
                    foreach (DataRow drAirItineraryPricingInfo in drPricedItinerary.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                    {
                        foreach (DataRow drPTC_FareBreakdown in drAirItineraryPricingInfo.GetChildRows("AirItineraryPricingInfo_PTC_FareBreakdown"))
                        {
                            foreach (DataRow dr_PassengerFare in drPTC_FareBreakdown.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                            {
                                foreach (DataRow drTaxes in dr_PassengerFare.GetChildRows("PassengerFare_Taxes"))
                                {
                                    foreach (DataRow drTax in drTaxes.GetChildRows("Taxes_Tax"))
                                    {
                                        try
                                        {
                                            if (drTax["CurrencyCode"].ToString().Length.Equals(0))
                                            {
                                                drTax["CurrencyCode"] = "COP";
                                            }
                                        }
                                        catch { drTax["CurrencyCode"] = "COP"; }
                                        try
                                        {
                                            if (drTax["DecimalPlaces"].ToString().Length.Equals(0))
                                            {
                                                drTax["DecimalPlaces"] = 0;
                                            }
                                        }
                                        catch { drTax["DecimalPlaces"] = 0; }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
        private void AdicionarWs(DataSet dsSabreAir)
        {
            try
            {
                DataTable dtPricedItinerary = dsSabreAir.Tables[TABLA_PRICEDITINERARY];
                DataTable dtSegmentItinerary = dsSabreAir.Tables[TABLA_SEGMENT];
                DataTable dtOrigenDestination = dsSabreAir.Tables[TABLA_ORIGENDESTINATION];
                string sWebServices = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
                /*AGREGAMOS COLUMNAS A LATABLA DE SEGMENTOS*/
                if (dtPricedItinerary != null)
                {
                    dtPricedItinerary.Columns.Add(COLUMN_WEBSERVICES, typeof(string));
                    foreach (DataRow drPricedItinerary in dtPricedItinerary.Rows)
                    {
                        drPricedItinerary[COLUMN_WEBSERVICES] = sWebServices;
                    }
                    dtPricedItinerary.AcceptChanges();
                }
                if (dtOrigenDestination != null)
                {
                    dtOrigenDestination.Columns.Add(COLUMN_WEBSERVICES, typeof(string));
                    foreach (DataRow drOrigenDestination in dtOrigenDestination.Rows)
                    {
                        drOrigenDestination[COLUMN_WEBSERVICES] = sWebServices;
                    }
                    dtOrigenDestination.AcceptChanges();
                }
                if (dtSegmentItinerary != null)
                {
                    dtSegmentItinerary.Columns.Add(COLUMN_WEBSERVICES, typeof(string));
                    dtSegmentItinerary.Columns.Add(COLUMN_CONDICION, typeof(string));
                    dtSegmentItinerary.Columns.Add(COLUMN_RUTA, typeof(string));
                    foreach (DataRow drSegmentItinerary in dtSegmentItinerary.Rows)
                    {
                        drSegmentItinerary[COLUMN_WEBSERVICES] = sWebServices;
                    }
                    dtSegmentItinerary.AcceptChanges();
                }
                dsSabreAir.AcceptChanges();
            }
            catch { }
        }
        /// <summary>
        /// metodo pendiente por revision
        ///  se modifico este metodo porq  estava enviando el segmento ARUNK, pero al armar el dataset se estaba presentando el problema, 
        ///  porque estaba tomando el destino del segmento 2 yen este caso era el ARUNK que es null.
        ///   /// <param name="sCodeCity">Codigo Iata de la ciudad</param>
        /// <returns>dsData, DatatSet de resultados</returns>
        /// <remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-12-22
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:José Faustino Posas        
        /// Fecha:2012-10-09      
        /// Descripción:    
        /// </summary>
        /// <param name="dsSabreAir"></param>
        private void ModificarTablaFlightSegmento(DataSet dsSabreAir)
        {
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            DataTable dtFlightSegmento = dsSabreAir.Tables["FlightSegment"];
            DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
            StringBuilder strExpresion = new StringBuilder();
         
            int iClases = vo_OTA_AirLowFareSearchLLSRQ.LsClase.Count;

            clsIATAVirtual objVirtual = new clsIATAVirtual();

            string sIda = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo;
            string sRegreso = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo;
            // Se incluye para validar el tipo de trayecto, si es el de ida o el de regreso


            try
            {
                if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Count > 1)
                {
                    sRegreso = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Count - 1].Vo_AeropuertoOrigen.SCodigo;
                }
            }
            catch { }
            //Check if FROM  && to are virtual IATA  from XML 
            objVirtual = objVirtual.sObtenerIataVirtual(sIda,
                                                      sRegreso);
            sIda = objVirtual.sIda;
            sRegreso = objVirtual.sVuelta;


            string sTipoRefereAir = clsValidaciones.GetKeyOrAdd("AEROPUERTOS", "AEROPUERTOS");
            string sSeparadorDecimal = clsValidaciones.SeparadorDecimal();
            string sRutaAirLine = clsValidaciones.RutaImagesAirGen();
            string sMonedaUsd = clsValidaciones.GetKeyOrAdd("MonedaDolar", "USD");
            string sMonedaView = clsValidaciones.GetKeyOrAdd("MonedaView", "COP");
            /*CONDICION DE LA CLASE*/
            strExpresion.Append("IIF(ResBookDesigCode = 'Y'");
            strExpresion.Append(",'Económica'");
            strExpresion.Append(",IIF(ResBookDesigCode = 'C'");
            strExpresion.Append(",'Ejecutiva'");
            strExpresion.Append(",IIF(ResBookDesigCode = 'F'");
            strExpresion.Append(",'Primera Clase'");
            strExpresion.Append(",'' + ResBookDesigCode");
            strExpresion.Append(")))");
            /*AGREGAMOS COLUMNAS A LATABLA DE SEGMENTOS*/
            dtFlightSegmento.Columns.Add("strDepartureAirport", typeof(string));
            dtFlightSegmento.Columns.Add("strArrivalAirport", typeof(string));
            dtFlightSegmento.Columns.Add("strOperatingAirline", typeof(string));
            dtFlightSegmento.Columns.Add("strEquipment", typeof(string));
            dtFlightSegmento.Columns.Add("strMarketingAirline", typeof(string));
            dtFlightSegmento.Columns.Add("strMarketingCabin", typeof(string));
            dtFlightSegmento.Columns.Add("strClase", typeof(string), strExpresion.ToString());
            dtFlightSegmento.Columns.Add("strTPA_Extensions", typeof(string));
            dtFlightSegmento.Columns.Add("strCodeContext", typeof(string));
            dtFlightSegmento.Columns.Add("strTipoTrayecto", typeof(string));
            dtFlightSegmento.Columns.Add("dtmFechaSalida", typeof(DateTime)).Expression = "DepartureDatetime";
            dtFlightSegmento.Columns.Add("dtmFechaLlegada", typeof(DateTime)).Expression = "ArrivalDatetime";
            dtFlightSegmento.Columns.Add("strNombre_Aerolinea", typeof(string));
            dtFlightSegmento.Columns.Add("intId_Aerolinea", typeof(int));
            dtFlightSegmento.Columns.Add("strAeropuerto_Llegada");
            dtFlightSegmento.Columns.Add("IntId_Aeropuerto_Llegada");
            dtFlightSegmento.Columns.Add("IntId_Pais_Llegada");
            dtFlightSegmento.Columns.Add("Code_Aeropuerto_Llegada");
            dtFlightSegmento.Columns.Add("strCiudad_Llegada");
            dtFlightSegmento.Columns.Add("strAeropuerto_Salida");
            dtFlightSegmento.Columns.Add("IntId_Aeropuerto_Salida");
            dtFlightSegmento.Columns.Add("IntId_Pais_Salida");
            dtFlightSegmento.Columns.Add("Code_Aeropuerto_Salida");
            dtFlightSegmento.Columns.Add("strCiudad_Salida");
            dtFlightSegmento.Columns.Add("strParadas", typeof(string));
            dtFlightSegmento.Columns.Add("strEstiloParada", typeof(string));
            dtFlightSegmento.Columns.Add("strDescripcionParadas", typeof(string));
            dtFlightSegmento.Columns.Add("IntPrecioDesde", typeof(decimal)).DefaultValue = "0";
            dtFlightSegmento.Columns.Add("str_Tipo_Moneda", typeof(string), "'" + vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion + "'");

            dtFlightSegmento.Columns.Add("strTrayecto", typeof(string));

            /*AGREGAMOS COLUMNAS A LA TABLA DE ITINERARIOS*/
            dtItinerario.Columns.Add("IntTotalPesos", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("IntTotalUsd", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("IntTotalDolares", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("str_Tipo_Moneda", typeof(string), "'" + vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion + "'");
            dtItinerario.Columns.Add("str_Tipo_MonedaUsd", typeof(string), "'" + sMonedaUsd + "'");
            dtItinerario.Columns.Add("IntPrecioDesde", typeof(decimal)).DefaultValue = "0";

            // Se incluyen columnas para visualizar
            dtItinerario.Columns.Add("strTotalView", typeof(string));
            dtItinerario.Columns.Add("strTipoMonedaView", typeof(string));
            dtItinerario.Columns.Add("strPrecioDesdeView", typeof(string));

            dtItinerario.Columns.Add("bolImpuestos", typeof(bool));
            dtItinerario.Columns.Add("strTextoDG", typeof(string));
            dtItinerario.Columns.Add("imgOferta", typeof(string));
            dtItinerario.Columns.Add("imgConvenio", typeof(string));
            // Se adicionan para ordenamientos
            dtItinerario.Columns.Add("strNombre_Aerolinea", typeof(string));
            dtItinerario.Columns.Add("StopQuantity", typeof(string));
            dtItinerario.Columns.Add("strMarketingAirline", typeof(string));
            dtItinerario.Columns.Add("IntValorBono", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("IntFactor", typeof(decimal)).DefaultValue = "1";
            dtItinerario.Columns.Add("IntPrecioOferta", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("IntPrecioOfertaPax", typeof(decimal)).DefaultValue = "0";

            dtFlightSegmento.Columns.Add("urlImagenAerolinea", typeof(string), "'" + sRutaAirLine + "'+ strMarketingAirline + '.gif' ");

            bool bIda = true;
            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                string sIdaTemp = string.Empty;
                string sRegresoTemp = string.Empty;
                int iParadasGen = 0;
                int iAirLine = 0;
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("PricedItinerary_AirItinerary"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItinerary_OriginDestinationOptions"))
                    {
                        foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("OriginDestinationOptions_OriginDestinationOption"))
                        {
                            foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("OriginDestinationOption_FlightSegment"))
                            {
                                try
                                { drRelacionCuatro["strDepartureAirport"] = drRelacionCuatro.GetChildRows("FlightSegment_DepartureAirport")[0]["LocationCode"].ToString(); }
                                catch
                                { drRelacionCuatro["strDepartureAirport"] = "**"; }
                                try
                                { drRelacionCuatro["strCodeContext"] = drRelacionCuatro.GetChildRows("FlightSegment_DepartureAirport")[0]["CodeContext"].ToString(); }
                                catch
                                { drRelacionCuatro["strCodeContext"] = "**"; }
                                try
                                { drRelacionCuatro["strArrivalAirport"] = drRelacionCuatro.GetChildRows("FlightSegment_ArrivalAirport")[0]["LocationCode"].ToString(); }
                                catch
                                { drRelacionCuatro["strArrivalAirport"] = "**"; }
                                try
                                { drRelacionCuatro["strOperatingAirline"] = drRelacionCuatro.GetChildRows("FlightSegment_OperatingAirline")[0]["Code"].ToString(); }
                                catch
                                { drRelacionCuatro["strOperatingAirline"] = "**"; }
                                try
                                { drRelacionCuatro["strEquipment"] = drRelacionCuatro.GetChildRows("FlightSegment_Equipment")[0]["AirEquipType"].ToString(); }
                                catch
                                { drRelacionCuatro["strEquipment"] = "**"; }
                                try
                                { drRelacionCuatro["strMarketingAirline"] = drRelacionCuatro.GetChildRows("FlightSegment_MarketingAirline")[0]["Code"].ToString(); }
                                catch
                                { drRelacionCuatro["strMarketingAirline"] = "**"; }
                                try
                                {
                                    bool bEntraClase = true;
                                    foreach (DataRow drRelacionCuatro1 in drRelacionCuatro.GetChildRows("FlightSegment_MarketingCabin"))
                                    {
                                        foreach (string sClases in vo_OTA_AirLowFareSearchLLSRQ.LsClase)
                                        {
                                            if (sClases.Equals(drRelacionCuatro1["CabinType"].ToString()))
                                            {
                                                drRelacionCuatro["strMarketingCabin"] = drRelacionCuatro1["CabinType"].ToString();
                                                bEntraClase = false;
                                            }
                                        }
                                    }
                                    if (bEntraClase)
                                        drRelacionCuatro["strMarketingCabin"] = drRelacionCuatro.GetChildRows("FlightSegment_MarketingCabin")[0]["CabinType"].ToString();
                                }
                                catch
                                {
                                }
                                try
                                { drRelacionCuatro["strTPA_Extensions"] = drRelacionCuatro.GetChildRows("FlightSegment_TPA_Extensions")[0]["TPA_Extensions_Id"].ToString(); }
                                catch
                                { drRelacionCuatro["strTPA_Extensions"] = "**"; }
                                drRelacionCuatro["strParadas"] = "Directo";
                                drRelacionCuatro["strEstiloParada"] = "SinParada";
                                int iParadas = 0;
                                foreach (DataRow drRelacionCinco in drRelacionCuatro.GetChildRows("FlightSegment_TPA_Extensions"))
                                {
                                    try
                                    {
                                        if (sIda.Contains(drRelacionCuatro["strDepartureAirport"].ToString()))
                                        {
                                            bIda = true;
                                            iParadasGen = 0;
                                        }
                                        if (sRegreso.Contains(drRelacionCuatro["strDepartureAirport"].ToString()))
                                        {
                                            bIda = false;
                                            iParadasGen = 0;
                                        }
                                        if (bIda)
                                        {
                                            drRelacionCuatro["strTrayecto"] = "I";
                                        }
                                        else
                                        {
                                            drRelacionCuatro["strTrayecto"] = "R";
                                        }
                                        if (drRelacionCuatro["StopQuantity"] != null)
                                        {
                                            iParadas += int.Parse(drRelacionCuatro["StopQuantity"].ToString());
                                            //iParadasGen += int.Parse(drRelacionCuatro["StopQuantity"].ToString());
                                        }
                                        if (drRelacionCuatro["MarriageGrp"].ToString().Equals("I"))
                                        {
                                            //iParadas += int.Parse(drRelacionCuatro["StopQuantity"].ToString());
                                            iParadasGen++;
                                        }
                                    }
                                    catch { }
                                    if (drRelacionCuatro["StopQuantity"].ToString().Equals("0"))
                                    {
                                        foreach (DataRow drRelacionSiete in drRelacionCinco.GetChildRows("TPA_Extensions_ConnectionIndicator"))
                                        {
                                            try
                                            { drRelacionCuatro["strParadas"] = drRelacionCuatro["StopQuantity"].ToString(); }
                                            catch
                                            { drRelacionCuatro["strParadas"] = "0"; }
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            drRelacionCuatro["strParadas"] = drRelacionCuatro["StopQuantity"].ToString();
                                            drRelacionCuatro["strEstiloParada"] = "ConParada";
                                        }
                                        catch
                                        { drRelacionCuatro["strParadas"] = "0"; }

                                        foreach (DataRow drRelacionSeis in drRelacionCinco.GetChildRows("TPA_Extensions_IntermediatePointInfo"))
                                        {
                                            foreach (DataRow drRelacionOcho in drRelacionSeis.GetChildRows("IntermediatePointInfo_DateTime"))
                                            {
                                                string sHoursI = drRelacionOcho["ElapsedTime"].ToString();
                                                string sHoursD = drRelacionOcho["Duration"].ToString();
                                                string[] slHoursI = null;
                                                string[] slHoursD = null;

                                                if (sHoursI.Contains("."))
                                                    slHoursI = clsValidaciones.Lista(sHoursI, ".");

                                                if (sHoursI.Contains(":"))
                                                    slHoursI = clsValidaciones.Lista(sHoursI, ":");

                                                if (sHoursD.Contains("."))
                                                    slHoursD = clsValidaciones.Lista(sHoursD, ".");

                                                if (sHoursD.Contains(":"))
                                                    slHoursD = clsValidaciones.Lista(sHoursD, ":");

                                                int iHora = int.Parse(slHoursI[0]) + int.Parse(slHoursD[0]);
                                                int iMinuto = int.Parse(slHoursI[1]) + int.Parse(slHoursD[1]);
                                                int iMinutoT = iMinuto;
                                                if (iMinuto > 60)
                                                {
                                                    iMinutoT = iMinuto - 60;
                                                    iHora++;
                                                }

                                                string sHora = iHora.ToString();
                                                if (iHora < 10)
                                                    sHora = "0" + sHora;

                                                string sMinutoT = iMinutoT.ToString();
                                                if (iMinutoT < 10)
                                                    sMinutoT = "0" + sMinutoT;

                                                string sHoursT = sHora + "." + sMinutoT;

                                                drRelacionCuatro["ElapsedTime"] = sHoursT;
                                                //drRelacionOcho["ElapsedTime"] = drRelacionCuatro["ElapsedTime"];

                                                //otblRefere.Get(sTipoRefereAir, drRelacionSeis["LocationCode"].ToString());
                                                string TextoSeg = string.Empty;
                                                //if (otblRefere.Respuesta)
                                                //{
                                                //    TextoSeg = "Aeropuerto:      " + otblRefere.strRefere.Value + " - " + otblRefere.strDetalle.Value + " " + otblRefere.strValorAdic.Value + "\n";
                                                //    TextoSeg += "Tiempo inicial:  " + drRelacionOcho["ElapsedTime"].ToString() + "\n";
                                                //    TextoSeg += "Tiempo parada:   " + drRelacionOcho["Duration"].ToString() + "\n";
                                                //    TextoSeg += "Tiempo total:    " + drRelacionCuatro["ElapsedTime"].ToString() + "\n";
                                                //    TextoSeg += "Llegada:         " + drRelacionOcho["ArrivalDateTime"].ToString() + "\n";
                                                //    TextoSeg += "Salida:          " + drRelacionOcho["DepartureDateTime"].ToString() + "\n";
                                                //}
                                                //else
                                                //{
                                                //    TextoSeg = "Aeropuerto:       " + drRelacionSeis["LocationCode"].ToString() + "\n";
                                                //    TextoSeg += "Tiempo inicial:  " + drRelacionOcho["ElapsedTime"].ToString() + "\n";
                                                //    TextoSeg += "Tiempo parada:   " + drRelacionOcho["Duration"].ToString() + "\n";
                                                //    TextoSeg += "Tiempo total:    " + drRelacionCuatro["ElapsedTime"].ToString() + "\n";
                                                //    TextoSeg += "Llegada:         " + drRelacionOcho["ArrivalDateTime"].ToString() + "\n";
                                                //    TextoSeg += "Salida:          " + drRelacionOcho["DepartureDateTime"].ToString() + "\n";
                                                //}
                                                drRelacionCuatro["strDescripcionParadas"] = TextoSeg;
                                            }
                                        }
                                    }
                                }
                                try
                                { drItinerario["StopQuantity"] = iParadasGen.ToString(); }
                                catch
                                { drItinerario["StopQuantity"] = "0"; }
                                if (iAirLine.Equals(0))
                                {
                                    try
                                    {
                                        drItinerario["strMarketingAirline"] = drRelacionCuatro["strMarketingAirline"];
                                    }
                                    catch
                                    { drItinerario["strMarketingAirline"] = "**"; }
                                }
                                iAirLine++;
                            }
                        }
                    }
                }
            }
        }

        #region Otalowfareserach 19 reult multi
        /// <summary>
        /// Metodo que modifica el DS de vuelos
        /// </summary>
        /// <param name="dsSabreAir">Dataset</param>
        /// <param name="EnumTipoTrayecto">Tipo Trayecto</param>
        /// <param name="EnumTipoVuelo">Tipo Vuelo</param>
        /// <remarks> 
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          Juan Camilo Diaz
        /// Fecha:          2012-04-17
        /// Descripción:    Se adiciona validacion para view de ofertas y vuelos normales
        /// -------------------       
        /// </remarks>
        public void ModificarDatasetSabreAir(DataSet dsSabreAir, Enum_TipoTrayecto EnumTipoTrayecto, Enum_TipoVuelo EnumTipoVuelo)
        {
            clsParametros cParametros = new clsParametros();
            /*METODO QUE INICIA LA MODIFICAION DEL DATASET DE SABRE*/
            if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
            {
                /*OBTENEMOS EL TIPO DE VUELO*/
                try
                {
                    /*ESTABLECEMOS LA CULTURA PARA EL DATASET*/
                    dsSabreAir.Locale = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                }
                catch
                {
                }
                try
                {
                    AdicionarWs(dsSabreAir);
                }
                catch
                {
                }
                try
                {
                    GetMarckup(dsSabreAir);
                }
                catch
                {
                }
                try
                {
                    /*MODIFICAMOS LA TABLA DE FlightSegmento CREAMOS MAS COLUMNAS Y AGREGAMOS VALIDACIONES*/
                    ModificarTablaFlightSegmentoMulti(dsSabreAir);
                }
                catch
                {
                }
               
                try
                {
                    ModificarDGMulti(dsSabreAir);
                }
                catch
                {
                }
                /*VERIFICAMOS LOS VALORES NACIONALES E INTERNACIONALES*/
                try
                {
                    /*MODIFICAMOS LA TABLA DE PassengerFare CREAMOS MAS COLUMNAS Y AGREGAMOS VALIDACIONES*/
                    Modificar_Tarifas_Equivalentes(dsSabreAir, EnumTipoVuelo);
                }
                catch
                {
                }

                try
                {
                    /*MODIFICAMOS LA TABLA DE PassengerFare CREAMOS MAS COLUMNAS Y AGREGAMOS VALIDACIONES*/
                    ModificarTablaPassengerFareMulti(dsSabreAir, EnumTipoVuelo);
                }
                catch
                {
                }
                try
                {
                    ModificarTablaPassengerFareCodeMulti(dsSabreAir);
                }
                catch
                {
                }
                try
                {
                    /*MODIFICAMOS EL DATASET DE SABRE AGREGAMOS MAS TABLAS Y RELACIONES*/
                    SetRelacionesDsSabreAir(dsSabreAir);
                }
                catch
                {

                }

                try
                {
                    /*VALIDAMOS EL TIPO DE TRAYECTO SI ES NACIONAL O INTERNACIONAL FlightSegment*/
                    SetTipoTrayecto(dsSabreAir);
                }
                catch
                {
                }
                try
                {
                    /*AGREGAMOS LA TASA ADMINISTRATIVA*/
                    GetTasaAdmin(dsSabreAir, EnumTipoTrayecto, EnumTipoVuelo);
                }
                catch
                {
                }
                try
                {
                    /*AGREGAMOS EL VALOR TOTAL A LA TABLA DE ITINERAIO*/
                    ModificarItinerarioMulti(dsSabreAir);
                }
                catch
                {
                }
                try
                {
                    ModificarConsecutivo(dsSabreAir);
                }
                catch
                {
                }
                try
                {
                    ModificarValueView(dsSabreAir);
                }
                catch { }
                try
                {
                    /*ACEPTAMOS LOS CAMBIOS*/
                    dsSabreAir.AcceptChanges();
                    /*ASIGNAMOS EL DATASET A LA VARIABLE DE LA CLASE*/
                    this.dsSabreAir = dsSabreAir;
                  

                   
                }
                catch
                {
                }
            }
        }
        
        /// <summary>
        /// pendiente por revision
        /// hceron
        //24052013
        /// </summary>
        /// <param name="dsSabreAir"></param>
        private void ModificarTablaFlightSegmentoMulti(DataSet dsSabreAir)
        {
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            DataTable dtFlightSegmento = dsSabreAir.Tables["FlightSegment"];
            DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
            StringBuilder strExpresion = new StringBuilder();
           
            int iClases = vo_OTA_AirLowFareSearchLLSRQ.LsClase.Count;

            // Se incluye para validar el tipo de trayecto, si es el de ida o el de regreso
            string sIda = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo;
            string sRegreso = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo;
            try
            {
                if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Count > 1)
                {
                    sRegreso = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas.Count - 1].Vo_AeropuertoOrigen.SCodigo;
                }
            }
            catch { }

            string sTipoRefereAir = clsValidaciones.GetKeyOrAdd("AEROPUERTOS", "AEROPUERTOS");
            string sSeparadorDecimal = clsValidaciones.SeparadorDecimal();
            string sRutaAirLine = clsValidaciones.RutaImagesAirGen();
            string sMonedaUsd = clsValidaciones.GetKeyOrAdd("MonedaDolar", "USD");
            string sMonedaView = clsValidaciones.GetKeyOrAdd("MonedaView", "COP");
            /*CONDICION DE LA CLASE*/
            strExpresion.Append("IIF(strMarketingCabin = 'Y'");
            strExpresion.Append(",'Económica'");
            strExpresion.Append(",IIF(strMarketingCabin = 'C'");
            strExpresion.Append(",'Ejecutiva'");
            strExpresion.Append(",IIF(strMarketingCabin = 'F'");
            strExpresion.Append(",'Primera Clase'");
            strExpresion.Append(",'' + strMarketingCabin");
            strExpresion.Append(")))");
            /*AGREGAMOS COLUMNAS A LATABLA DE SEGMENTOS*/
            dtFlightSegmento.Columns.Add("strDepartureAirport", typeof(string));
            dtFlightSegmento.Columns.Add("strArrivalAirport", typeof(string));
            dtFlightSegmento.Columns.Add("strOperatingAirline", typeof(string));
            dtFlightSegmento.Columns.Add("strEquipment", typeof(string));
            dtFlightSegmento.Columns.Add("strMarketingAirline", typeof(string));
            dtFlightSegmento.Columns.Add("strMarketingCabin", typeof(string));
            dtFlightSegmento.Columns.Add("strClase", typeof(string), strExpresion.ToString());
            dtFlightSegmento.Columns.Add("strTPA_Extensions", typeof(string));
            dtFlightSegmento.Columns.Add("strCodeContext", typeof(string));
            dtFlightSegmento.Columns.Add("strTipoTrayecto", typeof(string));
            dtFlightSegmento.Columns.Add("dtmFechaSalida", typeof(DateTime)).Expression = "DepartureDatetime";
            dtFlightSegmento.Columns.Add("dtmFechaLlegada", typeof(DateTime)).Expression = "ArrivalDatetime";
            dtFlightSegmento.Columns.Add("strNombre_Aerolinea", typeof(string));
            dtFlightSegmento.Columns.Add("intId_Aerolinea", typeof(int));
            dtFlightSegmento.Columns.Add("strAeropuerto_Llegada");
            dtFlightSegmento.Columns.Add("IntId_Aeropuerto_Llegada");
            dtFlightSegmento.Columns.Add("IntId_Pais_Llegada");
            dtFlightSegmento.Columns.Add("Code_Aeropuerto_Llegada");
            dtFlightSegmento.Columns.Add("strCiudad_Llegada");
            dtFlightSegmento.Columns.Add("strAeropuerto_Salida");
            dtFlightSegmento.Columns.Add("IntId_Aeropuerto_Salida");
            dtFlightSegmento.Columns.Add("IntId_Pais_Salida");
            dtFlightSegmento.Columns.Add("Code_Aeropuerto_Salida");
            dtFlightSegmento.Columns.Add("strCiudad_Salida");
            dtFlightSegmento.Columns.Add("strParadas", typeof(string));
            dtFlightSegmento.Columns.Add("strEstiloParada", typeof(string));
            dtFlightSegmento.Columns.Add("strDescripcionParadas", typeof(string));
            dtFlightSegmento.Columns.Add("IntPrecioDesde", typeof(decimal)).DefaultValue = "0";
            dtFlightSegmento.Columns.Add("str_Tipo_Moneda", typeof(string), "'" + vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion + "'");

            dtFlightSegmento.Columns.Add("strTrayecto", typeof(string));

            /*AGREGAMOS COLUMNAS A LA TABLA DE ITINERARIOS*/
            dtItinerario.Columns.Add("IntTotalPesos", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("IntTotalUsd", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("IntTotalDolares", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("str_Tipo_Moneda", typeof(string), "'" + vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion + "'");
            dtItinerario.Columns.Add("str_Tipo_MonedaUsd", typeof(string), "'" + sMonedaUsd + "'");
            dtItinerario.Columns.Add("IntPrecioDesde", typeof(decimal)).DefaultValue = "0";

            // Se incluyen columnas para visualizar
            dtItinerario.Columns.Add("strTotalView", typeof(string));
            dtItinerario.Columns.Add("strTipoMonedaView", typeof(string));
            dtItinerario.Columns.Add("strPrecioDesdeView", typeof(string));

            dtItinerario.Columns.Add("bolImpuestos", typeof(bool));
            dtItinerario.Columns.Add("strTextoDG", typeof(string));
            dtItinerario.Columns.Add("imgOferta", typeof(string));
            dtItinerario.Columns.Add("imgConvenio", typeof(string));
            // Se adicionan para ordenamientos
            dtItinerario.Columns.Add("strNombre_Aerolinea", typeof(string));
            dtItinerario.Columns.Add("StopQuantity", typeof(string));
            dtItinerario.Columns.Add("strMarketingAirline", typeof(string));
            dtItinerario.Columns.Add("IntValorBono", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("IntFactor", typeof(decimal)).DefaultValue = "1";
            dtItinerario.Columns.Add("IntPrecioOferta", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("IntPrecioOfertaPax", typeof(decimal)).DefaultValue = "0";

            dtFlightSegmento.Columns.Add("urlImagenAerolinea", typeof(string), "'" + sRutaAirLine + "'+ strMarketingAirline + '.gif' ");

            bool bIda = true;
            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                string sIdaTemp = string.Empty;
                string sRegresoTemp = string.Empty;
                int iParadasGen = 0;
                int iAirLine = 0;
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("PricedItinerary_AirItinerary"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItinerary_OriginDestinationOptions"))
                    {
                        foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("OriginDestinationOptions_OriginDestinationOption"))
                        {
                            foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("OriginDestinationOption_FlightSegment"))
                            {
                                try
                                { drRelacionCuatro["strDepartureAirport"] = drRelacionCuatro.GetChildRows("FlightSegment_DepartureAirport")[0]["LocationCode"].ToString(); }
                                catch
                                { drRelacionCuatro["strDepartureAirport"] = "**"; }
                                try
                                { drRelacionCuatro["strCodeContext"] = drRelacionCuatro.GetChildRows("FlightSegment_DepartureAirport")[0]["CodeContext"].ToString(); }
                                catch
                                { drRelacionCuatro["strCodeContext"] = "**"; }
                                try
                                { drRelacionCuatro["strArrivalAirport"] = drRelacionCuatro.GetChildRows("FlightSegment_ArrivalAirport")[0]["LocationCode"].ToString(); }
                                catch
                                { drRelacionCuatro["strArrivalAirport"] = "**"; }
                                try
                                { drRelacionCuatro["strOperatingAirline"] = drRelacionCuatro.GetChildRows("FlightSegment_OperatingAirline")[0]["Code"].ToString(); }
                                catch
                                { drRelacionCuatro["strOperatingAirline"] = "**"; }
                                try
                                { drRelacionCuatro["strEquipment"] = drRelacionCuatro.GetChildRows("FlightSegment_Equipment")[0]["AirEquipType"].ToString(); }
                                catch
                                { drRelacionCuatro["strEquipment"] = "**"; }
                                try
                                { drRelacionCuatro["strMarketingAirline"] = drRelacionCuatro.GetChildRows("FlightSegment_MarketingAirline")[0]["Code"].ToString(); }
                                catch
                                { drRelacionCuatro["strMarketingAirline"] = "**"; }
                                try
                                {
                                    bool bEntraClase = true;
                                    foreach (DataRow drRelacionCuatro1 in drRelacionCuatro.GetChildRows("FlightSegment_MarketingCabin"))
                                    {
                                        foreach (string sClases in vo_OTA_AirLowFareSearchLLSRQ.LsClase)
                                        {
                                            if (sClases.Equals(drRelacionCuatro1["CabinType"].ToString()))
                                            {
                                                drRelacionCuatro["strMarketingCabin"] = drRelacionCuatro1["CabinType"].ToString();
                                                bEntraClase = false;
                                            }
                                        }
                                    }
                                    if (bEntraClase)
                                        drRelacionCuatro["strMarketingCabin"] = drRelacionCuatro.GetChildRows("FlightSegment_MarketingCabin")[0]["CabinType"].ToString();
                                }
                                catch
                                {
                                }
                                try
                                { drRelacionCuatro["strTPA_Extensions"] = drRelacionCuatro.GetChildRows("FlightSegment_TPA_Extensions")[0]["TPA_Extensions_Id"].ToString(); }
                                catch
                                { drRelacionCuatro["strTPA_Extensions"] = "**"; }
                                drRelacionCuatro["strParadas"] = "Directo";
                                drRelacionCuatro["strEstiloParada"] = "SinParada";
                                int iParadas = 0;
                                foreach (DataRow drRelacionCinco in drRelacionCuatro.GetChildRows("FlightSegment_TPA_Extensions"))
                                {
                                    try
                                    {
                                        if (drRelacionCuatro["strDepartureAirport"].ToString().Equals(sIda))
                                        {
                                            bIda = true;
                                            iParadasGen = 0;
                                        }
                                        if (drRelacionCuatro["strDepartureAirport"].ToString().Equals(sRegreso))
                                        {
                                            bIda = false;
                                            iParadasGen = 0;
                                        }
                                        if (bIda)
                                        {
                                            drRelacionCuatro["strTrayecto"] = "I";
                                        }
                                        else
                                        {
                                            drRelacionCuatro["strTrayecto"] = "R";
                                        }
                                        if (drRelacionCuatro["StopQuantity"] != null)
                                        {
                                            iParadas += int.Parse(drRelacionCuatro["StopQuantity"].ToString());
                                            //iParadasGen += int.Parse(drRelacionCuatro["StopQuantity"].ToString());
                                        }
                                        if (drRelacionCuatro["MarriageGrp"].ToString().Equals("I"))
                                        {
                                            //iParadas += int.Parse(drRelacionCuatro["StopQuantity"].ToString());
                                            iParadasGen++;
                                        }
                                    }
                                    catch { }
                                    if (drRelacionCuatro["StopQuantity"].ToString().Equals("0"))
                                    {
                                        foreach (DataRow drRelacionSiete in drRelacionCinco.GetChildRows("TPA_Extensions_ConnectionIndicator"))
                                        {
                                            try
                                            { drRelacionCuatro["strParadas"] = drRelacionCuatro["StopQuantity"].ToString(); }
                                            catch
                                            { drRelacionCuatro["strParadas"] = "0"; }
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            drRelacionCuatro["strParadas"] = drRelacionCuatro["StopQuantity"].ToString();
                                            drRelacionCuatro["strEstiloParada"] = "ConParada";
                                        }
                                        catch
                                        { drRelacionCuatro["strParadas"] = "0"; }

                                        foreach (DataRow drRelacionSeis in drRelacionCinco.GetChildRows("TPA_Extensions_IntermediatePointInfo"))
                                        {
                                            foreach (DataRow drRelacionOcho in drRelacionSeis.GetChildRows("IntermediatePointInfo_DateTime"))
                                            {
                                                string sHoursI = drRelacionOcho["ElapsedTime"].ToString();
                                                string sHoursD = drRelacionOcho["Duration"].ToString();
                                                string[] slHoursI = null;
                                                string[] slHoursD = null;

                                                if (sHoursI.Contains("."))
                                                    slHoursI = clsValidaciones.Lista(sHoursI, ".");

                                                if (sHoursI.Contains(":"))
                                                    slHoursI = clsValidaciones.Lista(sHoursI, ":");

                                                if (sHoursD.Contains("."))
                                                    slHoursD = clsValidaciones.Lista(sHoursD, ".");

                                                if (sHoursD.Contains(":"))
                                                    slHoursD = clsValidaciones.Lista(sHoursD, ":");

                                                int iHora = int.Parse(slHoursI[0]) + int.Parse(slHoursD[0]);
                                                int iMinuto = int.Parse(slHoursI[1]) + int.Parse(slHoursD[1]);
                                                int iMinutoT = iMinuto;
                                                if (iMinuto > 60)
                                                {
                                                    iMinutoT = iMinuto - 60;
                                                    iHora++;
                                                }

                                                string sHora = iHora.ToString();
                                                if (iHora < 10)
                                                    sHora = "0" + sHora;

                                                string sMinutoT = iMinutoT.ToString();
                                                if (iMinutoT < 10)
                                                    sMinutoT = "0" + sMinutoT;

                                                string sHoursT = sHora + "." + sMinutoT;

                                                drRelacionCuatro["ElapsedTime"] = sHoursT;
                                                //drRelacionOcho["ElapsedTime"] = drRelacionCuatro["ElapsedTime"];

                                                //otblRefere.Get(sTipoRefereAir, drRelacionSeis["LocationCode"].ToString());
                                                string TextoSeg = string.Empty;
                                                //if (otblRefere.Respuesta)
                                                //{
                                                //    TextoSeg = "Aeropuerto:      " + otblRefere.strRefere.Value + " - " + otblRefere.strDetalle.Value + " " + otblRefere.strValorAdic.Value + "\n";
                                                //    TextoSeg += "Tiempo inicial:  " + drRelacionOcho["ElapsedTime"].ToString() + "\n";
                                                //    TextoSeg += "Tiempo parada:   " + drRelacionOcho["Duration"].ToString() + "\n";
                                                //    TextoSeg += "Tiempo total:    " + drRelacionCuatro["ElapsedTime"].ToString() + "\n";
                                                //    TextoSeg += "Llegada:         " + drRelacionOcho["ArrivalDateTime"].ToString() + "\n";
                                                //    TextoSeg += "Salida:          " + drRelacionOcho["DepartureDateTime"].ToString() + "\n";
                                                //}
                                                //else
                                                //{
                                                //    TextoSeg = "Aeropuerto:       " + drRelacionSeis["LocationCode"].ToString() + "\n";
                                                //    TextoSeg += "Tiempo inicial:  " + drRelacionOcho["ElapsedTime"].ToString() + "\n";
                                                //    TextoSeg += "Tiempo parada:   " + drRelacionOcho["Duration"].ToString() + "\n";
                                                //    TextoSeg += "Tiempo total:    " + drRelacionCuatro["ElapsedTime"].ToString() + "\n";
                                                //    TextoSeg += "Llegada:         " + drRelacionOcho["ArrivalDateTime"].ToString() + "\n";
                                                //    TextoSeg += "Salida:          " + drRelacionOcho["DepartureDateTime"].ToString() + "\n";
                                                //}
                                                drRelacionCuatro["strDescripcionParadas"] = TextoSeg;
                                            }
                                        }
                                    }
                                }
                                try
                                { drItinerario["StopQuantity"] = iParadasGen.ToString(); }
                                catch
                                { drItinerario["StopQuantity"] = "0"; }
                                if (iAirLine.Equals(0))
                                {
                                    try
                                    {
                                        drItinerario["strMarketingAirline"] = drRelacionCuatro["strMarketingAirline"];
                                    }
                                    catch
                                    { drItinerario["strMarketingAirline"] = "**"; }
                                }
                                iAirLine++;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// hceron 
        /// Modify dtatset to 19 result multi 
        /// 24052013
        /// </summary>
        /// <param name="dsSabreAir"></param>
        private void ModificarDGMulti(DataSet dsSabreAir)
        {
            ////Utils.Utils cUtil = new Utils.Utils();
            //string sEstructura = cUtil.DatasetStructura(dsSabreAir);
            DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
            string sRutaImagen = clsValidaciones.ObtenerUrlImages();
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            Enum_TipoVuelo eTipoSalida = vo_OTA_AirLowFareSearchLLSRQ.ETipoSalida;
            Enum_TipoVuelo eTipoVuelo = vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo;
            bool bDg = true;
            if (eTipoVuelo.Equals(Enum_TipoVuelo.Internacional))
            {
                if (eTipoSalida.Equals(Enum_TipoVuelo.Nacional))
                {
                    bDg = false;
                    string sCodeImpSalida = clsValidaciones.GetKeyOrAdd("CodeSabreDG", "DG");
                    string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");

                    foreach (DataRow drPricedItinerary in dtItinerario.Rows)
                    {
                        drPricedItinerary["strTextoDG"] = "Esta tarifa no incluye impuesto de salida de " + sPaisDefault;
                        drPricedItinerary["bolImpuestos"] = false;

                        drPricedItinerary["imgOferta"] = sRutaImagen + "spacer.gif";
                        drPricedItinerary["imgConvenio"] = sRutaImagen + "spacer.gif";

                        foreach (DataRow drAirItineraryPricingInfo in drPricedItinerary.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                        {
                            foreach (DataRow drPTC_FareInfo in drAirItineraryPricingInfo.GetChildRows("AirItineraryPricingInfo_PTC_FareInfo"))
                            {
                                foreach (DataRow drPTC_FareBreakdown in drPTC_FareInfo.GetChildRows("PTC_FareInfo_PTC_FareBreakdown"))
                                {
                                    foreach (DataRow dr_PassengerFare in drPTC_FareBreakdown.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                                    {
                                        foreach (DataRow drTaxes in dr_PassengerFare.GetChildRows("PassengerFare_Taxes"))
                                        {
                                            foreach (DataRow drTax in drTaxes.GetChildRows("Taxes_Tax"))
                                            {
                                                if (drTax["TaxCode"].ToString().Substring(0, 2).Equals(sCodeImpSalida))
                                                {
                                                    drPricedItinerary["strTextoDG"] = string.Empty;
                                                    drPricedItinerary["bolImpuestos"] = true;
                                                }
                                            }
                                        }
                                        foreach (DataRow drTPAExtensions in dr_PassengerFare.GetChildRows("PassengerFare_TPA_Extensions"))
                                        {
                                            foreach (DataRow drText in drTPAExtensions.GetChildRows("TPA_Extensions_Text"))
                                            {
                                                for (int i = 0; i < drText.ItemArray.Length; i++)
                                                {
                                                    if (drText[i].ToString().Contains(CONTAIN))
                                                    {
                                                        drPricedItinerary["imgConvenio"] = sRutaImagen + "convenios.gif";
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (bDg)
            {
                foreach (DataRow drPricedItinerary in dtItinerario.Rows)
                {
                    drPricedItinerary["bolImpuestos"] = true;

                    drPricedItinerary["imgOferta"] = sRutaImagen + "spacer.gif";
                    drPricedItinerary["imgConvenio"] = sRutaImagen + "spacer.gif";
                }
            }
        }


        /// <summary>
        ///  hceron
        ///  24052013
        ///  manage to multidestination
        /// </summary>
        /// <param name="dsSabreAir"></param>
        /// <param name="EnumTipoVuelo"></param>
        private void ModificarTablaPassengerFareMulti(DataSet dsSabreAir, Enum_TipoVuelo EnumTipoVuelo)
        {
            DataTable dtPassengerFare = dsSabreAir.Tables["PassengerFare"];
            DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
            DataTable dtPassengerTypeQuantity = dsSabreAir.Tables["PassengerTypeQuantity"];
            DataTable dtTaxes = dsSabreAir.Tables["Taxes"];
            DataTable dtPTC_FareBreakdown = dsSabreAir.Tables["PTC_FareBreakdown"];
            DataTable dtPTC_FareInfo = dsSabreAir.Tables["PTC_FareInfo"];
            DataTable dtBaseFare = dsSabreAir.Tables["BaseFare"];
            DataTable dtEquivFare = dsSabreAir.Tables["EquivFare"];
            DataTable dtTax = dsSabreAir.Tables["Tax"];
            string sMonedaCop = clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP");
            string sMonedaUsd = clsValidaciones.GetKeyOrAdd("MonedaDolar", "USD");

            /*AGREGAMOS LA COLUMNA DE TASA EQUIVALENTE EN USD*/
            dtTax.Columns.Add("Tax_Amount_Usd", typeof(decimal));
            /*AGREGAMOS LA COLUMNA A LA TABLA DEL LISTADO DE TASAS*/
            dtTaxes.Columns.Add("intTotalTax", typeof(decimal), "Sum(Child(Taxes_Tax).Amount)");
            /*TOTAL DE LA TA + ITA*/
            dtTaxes.Columns.Add("intTotalTax_Ta_Iva_Usd", typeof(decimal), "ISNULL(Sum(Child(Taxes_Tax).Tax_Amount_Usd), 0)");
            dtBaseFare.Columns.Add("DecimalBaseFare", typeof(decimal), "Convert(Amount, 'System.Decimal')");

            // Se adicionan columnas para la tasa
            dtTax.Columns.Add("strTaxAmountView", typeof(string));
            dtTax.Columns.Add("strCurrencyCodeView", typeof(string));
            dtTax.AcceptChanges();
            dtTaxes.Columns.Add("strTotalTaxView", typeof(string));

            /*AGREGAMOS LA COLUMNA A LA TABLA DEL LISTADO DE TASAS*/
            dtEquivFare.Columns.Add("DecimalEquivFare", typeof(decimal), "Convert(Amount, 'System.Decimal')");

            /*AGREGAMOS LA TABLA DE PASAJEROS */
            DataTable dtPasajeros = CreateTablePassenger();
            /*AGREGAMOS LA TABLA AL DATASET*/
            dsSabreAir.Tables.Add(dtPasajeros);

            StringBuilder strExpression_strTipoPasajero = new StringBuilder();
            /*VALIDACION DEL CODIGO DE TIPO PASAJERO*/
            strExpression_strTipoPasajero.Append("IIF(Code = 'ADT'");
            strExpression_strTipoPasajero.Append(",'Adulto'");
            strExpression_strTipoPasajero.Append(",IIF(Code = 'CNN'");
            strExpression_strTipoPasajero.Append(",'Niño'");
            for (int c = iEdadMin; c <= iEdadMAx; c++)
            {
                if (c < 10)
                    strExpression_strTipoPasajero.Append(",IIF(Code = 'C0" + c.ToString() + "'");
                else
                    strExpression_strTipoPasajero.Append(",IIF(Code = 'C" + c.ToString() + "'");

                strExpression_strTipoPasajero.Append(",'Niño " + c.ToString() + " Años'");
            }
            strExpression_strTipoPasajero.Append(",IIF(Code = 'INF'");
            strExpression_strTipoPasajero.Append(",'Infante'");
            strExpression_strTipoPasajero.Append(",Code");
            strExpression_strTipoPasajero.Append(")))");
            for (int i = iEdadMin; i <= iEdadMAx; i++)
            {
                strExpression_strTipoPasajero.Append(")");
            }
            /*AGREGA COLUMNA DE TIPO PASAJERO A LA TABLA DE TIPOS DE PASAJEROS Y CATIDADES*/
            //dtPassengerTypeQuantity.Columns.Add("strTipoPasajero", typeof(string), strExpression_strTipoPasajero.ToString());
            dtPassengerTypeQuantity.Columns.Add("strTipoPasajero", typeof(string));
            dtPassengerTypeQuantity.Columns.Add("strDetalleTipo", typeof(string));

            /*VALIDACION DEL CODIGO DE TIPO PASAJERO*/
            strExpression_strTipoPasajero = new StringBuilder();
            strExpression_strTipoPasajero.Append("IIF(strCodTipoPasajero = 'ADT'");
            strExpression_strTipoPasajero.Append(",'Adulto'");
            strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'CNN'");
            strExpression_strTipoPasajero.Append(",'Niño'");
            for (int c = iEdadMin; c <= iEdadMAx; c++)
            {
                if (c < 10)
                    strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'C0" + c.ToString() + "'");
                else
                    strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'C" + c.ToString() + "'");

                strExpression_strTipoPasajero.Append(",'Niño " + c.ToString() + " Años'");
            }
            strExpression_strTipoPasajero.Append(",IIF(strCodTipoPasajero = 'INF'");
            strExpression_strTipoPasajero.Append(",'Infante'");
            strExpression_strTipoPasajero.Append(",strCodTipoPasajero");
            strExpression_strTipoPasajero.Append(")))");
            for (int i = iEdadMin; i <= iEdadMAx; i++)
            {
                strExpression_strTipoPasajero.Append(")");
            }
            /*AGREGAMOS COLUMNAS A LA TABLA DE TARIFAS*/
            dtPassengerFare.Columns.Add("intTPA_Extensions", typeof(int));
            dtPassengerFare.Columns.Add("strDetalleTipo", typeof(string));
            dtPassengerFare.Columns.Add("strCodTipoPasajero", typeof(string));
            //dtPassengerFare.Columns.Add("strTipoPasajero", typeof(string), strExpression_strTipoPasajero.ToString());
            dtPassengerFare.Columns.Add("strTipoPasajero", typeof(string));
            dtPassengerFare.Columns.Add("intCantidad", typeof(int));
            dtPassengerFare.Columns.Add("intEquivFare", typeof(decimal));
            dtPassengerFare.Columns.Add("intEquivFare_Decimal", typeof(decimal));
            dtPassengerFare.Columns.Add("intBaseFare", typeof(decimal));

            dtPassengerFare.Columns.Add("intBaseFareUSD", typeof(decimal));

            dtPassengerFare.Columns.Add("intBaseFare_Decimal", typeof(int));
            dtPassengerFare.Columns.Add("intTotalFare", typeof(decimal));
            dtPassengerFare.Columns.Add("intTotalFare_Decimal", typeof(int)).DefaultValue = 0;

            dtPassengerFare.Columns.Add("strTipoMonedaBaseFare", typeof(string));
            dtPassengerFare.Columns.Add("strTipoMonedaTotalFare", typeof(string));

            /*IMPUESTOS TASAS TOTAL*/
            dtPassengerFare.Columns.Add("IntTotalImpuestosTasas", typeof(decimal), "ISNULL(Sum(Child(PassengerFare_Taxes).intTotalTax),0)");
            /*TOTAL TA + ITA EN DOLARES*/
            dtPassengerFare.Columns.Add("IntTotalImpuestos_Ta_Iva_Usd_Usd", typeof(decimal), "ISNULL(Sum(Child(PassengerFare_Taxes).intTotalTax_Ta_Iva_Usd),0)");

            dtPassengerFare.Columns.Add("strBaseFareView", typeof(string));
            dtPassengerFare.Columns.Add("strTotalImpuestosTasasView", typeof(string));
            dtPassengerFare.Columns.Add("strTotalTarifaConTaXPersonaView", typeof(string));
            dtPassengerFare.Columns.Add("strTotalTarifaConTaView", typeof(string));
            dtPassengerFare.Columns.Add("strTipoMonedaTotalFareView", typeof(string));

            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion == sMonedaCop)
            {
                switch (EnumTipoVuelo)
                {
                    case Enum_TipoVuelo.Nacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intBaseFare) * intCantidad) /" + clsSesiones.GET_USD_SABRE());
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) /" + clsSesiones.GET_USD_SABRE());
                        break;
                    case Enum_TipoVuelo.Internacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intEquivFare) * intCantidad) / " + clsSesiones.GET_USD_SABRE());
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare)/ " + clsSesiones.GET_USD_SABRE());
                        break;
                    default:
                        break;
                }
            }
            else if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion == sMonedaUsd)
            {
                switch (EnumTipoVuelo)
                {
                    case Enum_TipoVuelo.Nacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intEquivFare) * intCantidad) ");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intEquivFare) ");
                        break;
                    case Enum_TipoVuelo.Internacional:
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) * intCantidad");
                        /*CALCULO VALOR TOTAL CON IMPUESTOS + TA EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTA_Usd", typeof(decimal), "((IntTotalImpuestosTasas + intBaseFare) * intCantidad) ");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare)");
                        /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE EN DOLARES*/
                        dtPassengerFare.Columns.Add("IntTotalTarifaConTaXPersona_Usd", typeof(decimal), "(IntTotalImpuestosTasas + intBaseFare) ");
                        break;
                    default:
                        break;
                }
            }

            /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE X CANTIDAD DE PERSONAS*/
            dtPTC_FareBreakdown.Columns.Add("IntTotalTarifaConTAPasajeros", typeof(decimal), "Sum(Child(PTC_FareBreakdown_PassengerFare).IntTotalTarifaConTA)");
            dtPTC_FareInfo.Columns.Add("IntTotalSumaTarifaConTAPasajeros", typeof(decimal), "Sum(Child(PTC_FareInfo_PTC_FareBreakdown).IntTotalTarifaConTAPasajeros)");
            /*TOTAL POR PERSONA CON IMPUESTOS +  TA + BASEFARE X CANTIDAD DE PERSONAS EN USD*/
            dtPTC_FareBreakdown.Columns.Add("IntTotalTarifaConTAPasajeros_Usd", typeof(decimal), "Sum(Child(PTC_FareBreakdown_PassengerFare).IntTotalTarifaConTA_Usd)");
            dtPTC_FareInfo.Columns.Add("IntTotalSumaTarifaConTAPasajeros_Usd", typeof(decimal), "Sum(Child(PTC_FareInfo_PTC_FareBreakdown).IntTotalTarifaConTAPasajeros_Usd)");
            decimal dValorDolar = Convert.ToDecimal(clsSesiones.GET_USD_SABRE());
            /*RECORREMOS TODAS LAS RELACIONES HASTA LLEGAR PASSENGER FARE*/
            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItineraryPricingInfo_PTC_FareInfo"))
                    {
                        foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("PTC_FareInfo_PTC_FareBreakdown"))
                        {
                            foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                            {
                                switch (EnumTipoVuelo)
                                {
                                    case Enum_TipoVuelo.Nacional:
                                        try
                                        {
                                            if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.ToString().Equals(sMonedaCop))
                                            {
                                                drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound((drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()));
                                            }
                                            else
                                            {
                                                drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound((drRelacionCuatro.GetChildRows("PassengerFare_EquivFare")[0]["DecimalEquivFare"].ToString()));
                                            }
                                        }
                                        catch
                                        { drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()); ; }
                                        break;
                                    case Enum_TipoVuelo.Internacional:

                                        try
                                        { drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_EquivFare")[0]["Amount"].ToString()); }
                                        catch
                                        { drRelacionCuatro["intBaseFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()); }
                                        try
                                        { drRelacionCuatro["intBaseFareUSD"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalBaseFare"].ToString()); }
                                        catch
                                        { drRelacionCuatro["intBaseFareUSD"] = "0"; }

                                        break;
                                    default:
                                        break;
                                }

                                try
                                { drRelacionCuatro["strTipoMonedaBaseFare"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["CurrencyCode"].ToString(); }
                                catch
                                { drRelacionCuatro["strTipoMonedaBaseFare"] = "***"; }

                                try
                                { drRelacionCuatro["intBaseFare_Decimal"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalPlaces"].ToString(); }
                                catch
                                { drRelacionCuatro["intBaseFare_Decimal"] = "0"; }

                                try
                                { drRelacionCuatro["intEquivFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_EquivFare")[0]["DecimalEquivFare"].ToString()); }
                                catch
                                { drRelacionCuatro["intEquivFare"] = "0"; }
                                try
                                { drRelacionCuatro["intEquivFare_Decimal"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalPlaces"].ToString(); }
                                catch
                                { drRelacionCuatro["intEquivFare_Decimal"] = "0"; }

                                try
                                { drRelacionCuatro["intTotalFare"] = clsValidaciones.getDecimalNotRound(drRelacionCuatro.GetChildRows("PassengerFare_TotalFare")[0]["Amount"].ToString()); }
                                catch
                                { drRelacionCuatro["intTotalFare"] = "0"; }

                                try
                                { drRelacionCuatro["strTipoMonedaTotalFare"] = drRelacionCuatro.GetChildRows("PassengerFare_TotalFare")[0]["CurrencyCode"].ToString(); }
                                catch
                                { drRelacionCuatro["strTipoMonedaTotalFare"] = "***"; }

                                try
                                { drRelacionCuatro["intTotalFare_Decimal"] = drRelacionCuatro.GetChildRows("PassengerFare_BaseFare")[0]["DecimalPlaces"].ToString(); }
                                catch
                                { drRelacionCuatro["intTotalFare_Decimal"] = "0"; }

                                try
                                { drRelacionCuatro["intTPA_Extensions"] = drRelacionCuatro.GetChildRows("PassengerFare_TPA_Extensions")[0]["TPA_Extensions_Id"].ToString(); }
                                catch
                                { drRelacionCuatro["intTPA_Extensions"] = "0"; }

                                foreach (DataRow drTipoPasajero in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerTypeQuantity"))
                                {
                                    try
                                    { drRelacionCuatro["strCodTipoPasajero"] = drTipoPasajero["Code"].ToString(); }
                                    catch
                                    { drRelacionCuatro["strCodTipoPasajero"] = "ADT"; }

                                    try
                                    { drRelacionCuatro["intCantidad"] = drTipoPasajero["Quantity"].ToString(); }
                                    catch
                                    { drRelacionCuatro["intCantidad"] = "0"; }
                                    try
                                    { drRelacionCuatro["strTipoPasajero"] = drTipoPasajero["Code"].ToString(); }
                                    catch
                                    { drRelacionCuatro["strTipoPasajero"] = "ADT"; }
                                    try
                                    { drRelacionCuatro["strDetalleTipo"] = drTipoPasajero["Code"].ToString(); }
                                    catch
                                    { drRelacionCuatro["strDetalleTipo"] = "ADT"; }
                                }
                                if (clsValidaciones.GetKeyOrAdd("CotizaInfante", "true").ToUpper().Equals("FALSE"))
                                {
                                    if (EnumTipoVuelo.Equals(Enum_TipoVuelo.Nacional))
                                    {
                                        if (drRelacionCuatro["strCodTipoPasajero"].ToString().Equals("INF"))
                                        {
                                            drRelacionCuatro["intBaseFare"] = "0";
                                            foreach (DataRow drTaxes in drRelacionCuatro.GetChildRows("PassengerFare_Taxes"))
                                            {
                                                foreach (DataRow drTax in drTaxes.GetChildRows("Taxes_Tax"))
                                                {
                                                    drTax["Amount"] = 0;
                                                    drTax["Tax_Amount_Usd"] = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }



        /// <summary>
        ///  hceron
        ///  24052013
        ///  manage to multidestination
        /// </summary>
        /// <param name="dsSabreAir"></param>
        private void ModificarTablaPassengerFareCodeMulti(DataSet dsSabreAir)
        {
            DataTable dtPassengerFare = dsSabreAir.Tables["PassengerFare"];
            DataTable dtPassengerTypeQuantity = dsSabreAir.Tables["PassengerTypeQuantity"];

            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            /*RECORREMOS TODAS LAS RELACIONES HASTA LLEGAR PASSENGER FARE*/
            List<VO_Pasajero> Lvo_Pasajero = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros;
            foreach (DataRow drPassengerFare in dtPassengerFare.Rows)
            {
                for (int i = 0; i < vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros.Count; i++)
                {
                    string sCodigoTipo = drPassengerFare["strCodTipoPasajero"].ToString();
                    if (sCodigoTipo.Contains("C0"))
                        sCodigoTipo = "CNN";
                    if (sCodigoTipo.Contains("C1"))
                        sCodigoTipo = "CNN";
                    if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCodigo.Equals(sCodigoTipo))
                    {
                        drPassengerFare["strDetalleTipo"] = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCodeGen;
                        drPassengerFare["strTipoPasajero"] = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SDetalle;
                    }
                }
            }
            foreach (DataRow drPassengerTypeQuantity in dtPassengerTypeQuantity.Rows)
            {
                for (int i = 0; i < vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros.Count; i++)
                {
                    string sCodigoTipoPax = drPassengerTypeQuantity["Code"].ToString();
                    if (sCodigoTipoPax.Contains("C0"))
                        sCodigoTipoPax = "CNN";
                    if (sCodigoTipoPax.Contains("C1"))
                        sCodigoTipoPax = "CNN";
                    if (vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCodigo.Equals(sCodigoTipoPax))
                    {
                        drPassengerTypeQuantity["strDetalleTipo"] = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SCodeGen;
                        drPassengerTypeQuantity["strTipoPasajero"] = vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros[i].SDetalle;
                    }
                }
            }
        }

        /// <summary>
        /// hceron to multudestination
        /// 24052013
        /// </summary>
        /// <param name="dsSabreAir"></param>
        private void ModificarItinerarioMulti(DataSet dsSabreAir)
        {
            bool bEntra = false;
            string sValidaMatriz = clsValidaciones.GetKeyOrAdd("bValidaMatriz", "False");
            if (sValidaMatriz.ToUpper().Equals("TRUE"))
                bEntra = true;

            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();

            DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
            DataTable dtFlightSegmento = dsSabreAir.Tables["FlightSegment"];
            string sMonedaCop = clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP");
            string sMonedaUsd = clsValidaciones.GetKeyOrAdd("MonedaDolar", "USD");

            foreach (DataRow drFilaItinerario in dtPricedItinerary.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drFilaItinerario.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItineraryPricingInfo_PTC_FareInfo"))
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.Equals(sMonedaCop))
                        {
                            drFilaItinerario["IntTotalPesos"] = drRelacionDos["IntTotalSumaTarifaConTAPasajeros"];
                            try
                            { drFilaItinerario["IntTotalUsd"] = drRelacionDos["IntTotalSumaTarifaConTAPasajeros"]; }
                            catch
                            { drFilaItinerario["IntTotalUsd"] = 0; }
                        }
                        else if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.Equals(sMonedaUsd))
                        {
                            drFilaItinerario["IntTotalPesos"] = clsValidaciones.getDecimalNotRound(drRelacionDos["IntTotalSumaTarifaConTAPasajeros"].ToString());
                            try
                            { drFilaItinerario["IntTotalUsd"] = clsValidaciones.getDecimalNotRound(drRelacionDos["IntTotalSumaTarifaConTAPasajeros"].ToString()); }
                            catch
                            { drFilaItinerario["IntTotalUsd"] = 0; }
                        }
                    }
                }
            }
            DataView dvFlightSegment = new DataView(dtPricedItinerary);
            string[] sAirLine = new string[5];
            sAirLine[0] = "strMarketingAirline";
            sAirLine[1] = "strNombre_Aerolinea";
            sAirLine[2] = "IntPrecioDesde";
            sAirLine[3] = "StopQuantity";
            sAirLine[4] = "str_Tipo_Moneda";

            DataTable dtFiltro = dvFlightSegment.ToTable(true, sAirLine);
            dtFiltro.TableName = "dtFilter";
            if (bEntra)
            {
                foreach (DataRow drFiltro in dtFiltro.Rows)
                {
                    decimal dPrecioMin = 0;
                    foreach (DataRow drFilaItinerario in dtPricedItinerary.Rows)
                    {
                        try
                        {
                            if (drFilaItinerario["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                            {
                                if (drFilaItinerario["StopQuantity"].ToString().Trim().Equals(drFiltro["StopQuantity"].ToString().Trim()))
                                {
                                    if (dPrecioMin.Equals(0))
                                    {
                                        dPrecioMin = Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString());
                                    }
                                    else
                                    {
                                        if (dPrecioMin > Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString()))
                                        {
                                            dPrecioMin = Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                    drFiltro["IntPrecioDesde"] = dPrecioMin;
                }
                foreach (DataRow drFiltro in dtFiltro.Rows)
                {
                    foreach (DataRow drFlightSegment in dtFlightSegmento.Rows)
                    {
                        if (drFlightSegment["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                        {
                            if (drFlightSegment["StopQuantity"].ToString().Trim().Equals(drFiltro["StopQuantity"].ToString().Trim()))
                            {
                                drFlightSegment["IntPrecioDesde"] = drFiltro["IntPrecioDesde"];
                            }
                        }
                    }
                }
                foreach (DataRow drFiltro in dtFiltro.Rows)
                {
                    foreach (DataRow drPricedItinerary in dtPricedItinerary.Rows)
                    {
                        if (drPricedItinerary["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                        {
                            if (drPricedItinerary["StopQuantity"].ToString().Trim().Equals(drFiltro["StopQuantity"].ToString().Trim()))
                            {
                                drPricedItinerary["IntPrecioDesde"] = drFiltro["IntPrecioDesde"];
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (DataRow drFiltro in dtFiltro.Rows)
                {
                    decimal dPrecioMin = 0;
                    foreach (DataRow drFilaItinerario in dtPricedItinerary.Rows)
                    {
                        try
                        {
                            if (drFilaItinerario["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                            {
                                if (dPrecioMin.Equals(0))
                                {
                                    dPrecioMin = Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString());
                                }
                                else
                                {
                                    if (dPrecioMin > Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString()))
                                    {
                                        dPrecioMin = Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString());
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                    drFiltro["IntPrecioDesde"] = dPrecioMin;
                }
                foreach (DataRow drFiltro in dtFiltro.Rows)
                {
                    foreach (DataRow drFlightSegment in dtFlightSegmento.Rows)
                    {
                        if (drFlightSegment["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                        {
                            drFlightSegment["IntPrecioDesde"] = drFiltro["IntPrecioDesde"];
                        }
                    }
                }
                foreach (DataRow drFiltro in dtFiltro.Rows)
                {
                    foreach (DataRow drPricedItinerary in dtPricedItinerary.Rows)
                    {
                        if (drPricedItinerary["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                        {
                            drPricedItinerary["IntPrecioDesde"] = drFiltro["IntPrecioDesde"];
                        }
                    }
                }
            }
            try
            {
                dsSabreAir.Tables.Add(dtFiltro);
            }
            catch { }
        }

        /// <summary>
        /// Search paxes with 19 result in muldestionation
        /// hceron multi
        /// 25052013
        /// </summary>
        /// <param name="PricedItinerary_Id"></param>
        /// <param name="dsSabreAir"></param>
        /// <returns></returns>
        private DataTable GetDtPassengerTypeQuantityMulti(string PricedItinerary_Id, DataSet dsSabreAir)
        {
            DataRow drFilaEncontrada = dsSabreAir.Tables["PricedItinerary"].Rows.Find(PricedItinerary_Id);

            DataTable dtTablaRetornada = ClonarTabla(dsSabreAir, "PassengerTypeQuantity");

            foreach (DataRow drFilaRelacionUno in drFilaEncontrada.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
            {
                foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItineraryPricingInfo_PTC_FareInfo"))
                {
                    foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("PTC_FareInfo_PTC_FareBreakdown"))
                    {
                        foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerTypeQuantity"))
                        {
                            dtTablaRetornada.Rows.Add(drRelacionCuatro.ItemArray);
                        }
                    }
                }
            }
            return dtTablaRetornada;
        }

        #endregion


        /// <summary>
        /// manejo de SegmentoHoras
        /// </summary>
        /// <param name="dsSabreAir"></param>
        private void ModificarTablaFlightSegmentoHoras(DataSet dsSabreAir)
        {
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            DataTable dtFlightSegmento = dsSabreAir.Tables["FlightSegment"];
            DataTable dtOriginDestinationOptions = dsSabreAir.Tables["OriginDestinationOptions"];
            DataTable dtOriginDestinationOption = dsSabreAir.Tables["OriginDestinationOption"];
            StringBuilder strExpresion = new StringBuilder();
          
            string sRutaAirLine = clsValidaciones.RutaImagesAirGen();
            /*CONDICION DE LA CLASE*/
            strExpresion.Append("IIF(strMarketingCabin = 'Y'");
            strExpresion.Append(",'Económica'");
            strExpresion.Append(",IIF(strMarketingCabin = 'C'");
            strExpresion.Append(",'Ejecutiva'");
            strExpresion.Append(",IIF(strMarketingCabin = 'F'");
            strExpresion.Append(",'Primera Clase'");
            strExpresion.Append(",'' + strMarketingCabin");
            strExpresion.Append(")))");
            /*AGREGAMOS COLUMNAS A LATABLA DE SEGMENTOS*/
            dtFlightSegmento.Columns.Add("strDepartureAirport", typeof(string));
            dtFlightSegmento.Columns.Add("strArrivalAirport", typeof(string));
            dtFlightSegmento.Columns.Add("strOperatingAirline", typeof(string));
            dtFlightSegmento.Columns.Add("strEquipment", typeof(string));
            dtFlightSegmento.Columns.Add("strMarketingAirline", typeof(string));
            dtFlightSegmento.Columns.Add("strMarketingCabin", typeof(string));
            dtFlightSegmento.Columns.Add("strClase", typeof(string), strExpresion.ToString());
            dtFlightSegmento.Columns.Add("strTPA_Extensions", typeof(string));
            dtFlightSegmento.Columns.Add("strCodeContext", typeof(string));
            dtFlightSegmento.Columns.Add("strTipoTrayecto", typeof(string));
            dtFlightSegmento.Columns.Add("dtmFechaSalida", typeof(DateTime)).Expression = "DepartureDatetime";
            dtFlightSegmento.Columns.Add("dtmFechaLlegada", typeof(DateTime)).Expression = "ArrivalDatetime";
            dtFlightSegmento.Columns.Add("strNombre_Aerolinea", typeof(string));
            dtFlightSegmento.Columns.Add("intId_Aerolinea", typeof(int));
            dtFlightSegmento.Columns.Add("strAeropuerto_Llegada");
            dtFlightSegmento.Columns.Add("IntId_Aeropuerto_Llegada");
            dtFlightSegmento.Columns.Add("IntId_Pais_Llegada");
            dtFlightSegmento.Columns.Add("Code_Aeropuerto_Llegada");
            dtFlightSegmento.Columns.Add("strCiudad_Llegada");
            dtFlightSegmento.Columns.Add("strAeropuerto_Salida");
            dtFlightSegmento.Columns.Add("IntId_Aeropuerto_Salida");
            dtFlightSegmento.Columns.Add("IntId_Pais_Salida");
            dtFlightSegmento.Columns.Add("Code_Aeropuerto_Salida");
            dtFlightSegmento.Columns.Add("strCiudad_Salida");
            dtFlightSegmento.Columns.Add("strParadas", typeof(string));
            dtFlightSegmento.Columns.Add("strEstiloParada", typeof(string));
            dtFlightSegmento.Columns.Add("strDescripcionParadas", typeof(string));
            dtFlightSegmento.Columns.Add("strClaseId", typeof(string));
            dtFlightSegmento.Columns.Add("IntPrecioDesde", typeof(decimal)).DefaultValue = "0";
            dtFlightSegmento.Columns.Add("str_Tipo_Moneda", typeof(string), "'" + vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion + "'");
            try
            {
                dtFlightSegmento.Columns.Add("ElapsedTime", typeof(string));
            }
            catch { }

            /*AGREGAMOS COLUMNAS A LA TABLA DE ITINERARIOS*/
            dtOriginDestinationOption.Columns.Add("SequenceNumber", typeof(string));
            dtOriginDestinationOption.Columns.Add("RPH", typeof(string));
            dtOriginDestinationOption.Columns.Add("strMarketingCabin", typeof(string));
            dtOriginDestinationOption.Columns.Add("IntPrecioDesde", typeof(decimal)).DefaultValue = "0";
            dtOriginDestinationOption.Columns.Add("IntPrecioOferta", typeof(decimal)).DefaultValue = "0";
            dtOriginDestinationOption.Columns.Add("IntPrecioOfertaPax", typeof(decimal)).DefaultValue = "0";

            dtFlightSegmento.Columns.Add("urlImagenAerolinea", typeof(string), "'" + sRutaAirLine + "'+ strMarketingAirline + '.gif' ");

            foreach (DataRow drRelacionDos in dtOriginDestinationOptions.Rows)
            {
                foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("OriginDestinationOptions_OriginDestinationOption"))
                {
                    foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("OriginDestinationOption_FlightSegment"))
                    {
                        try
                        {
                            if (drRelacionTres["RPH"].ToString().Length.Equals(0))
                                drRelacionTres["RPH"] = drRelacionCuatro["RPH"].ToString();
                        }
                        catch
                        { drRelacionTres["RPH"] = "0"; }
                        try
                        { drRelacionCuatro["strDepartureAirport"] = drRelacionCuatro.GetChildRows("FlightSegment_DepartureAirport")[0]["LocationCode"].ToString(); }
                        catch
                        { drRelacionCuatro["strDepartureAirport"] = "**"; }
                        try
                        { drRelacionCuatro["strCodeContext"] = drRelacionCuatro.GetChildRows("FlightSegment_DepartureAirport")[0]["CodeContext"].ToString(); }
                        catch
                        { drRelacionCuatro["strCodeContext"] = "**"; }
                        try
                        { drRelacionCuatro["strArrivalAirport"] = drRelacionCuatro.GetChildRows("FlightSegment_ArrivalAirport")[0]["LocationCode"].ToString(); }
                        catch
                        { drRelacionCuatro["strArrivalAirport"] = "**"; }
                        try
                        { drRelacionCuatro["strOperatingAirline"] = drRelacionCuatro.GetChildRows("FlightSegment_OperatingAirline")[0]["Code"].ToString(); }
                        catch
                        { drRelacionCuatro["strOperatingAirline"] = "**"; }
                        try
                        { drRelacionCuatro["strEquipment"] = drRelacionCuatro.GetChildRows("FlightSegment_Equipment")[0]["AirEquipType"].ToString(); }
                        catch
                        { drRelacionCuatro["strEquipment"] = "**"; }
                        try
                        { drRelacionCuatro["strMarketingAirline"] = drRelacionCuatro.GetChildRows("FlightSegment_MarketingAirline")[0]["Code"].ToString(); }
                        catch
                        { drRelacionCuatro["strMarketingAirline"] = "**"; }
             
                        try
                        {
                            bool bEntraClase = true;
                            foreach (DataRow drRelacionCuatro1 in drRelacionCuatro.GetChildRows("FlightSegment_BookingClassAvail"))
                            {
                                foreach (string sClases in vo_OTA_AirLowFareSearchLLSRQ.LsClase)
                                {
                                    if (sClases.Equals(drRelacionCuatro1["ResBookDesigCode"].ToString()))
                                    {
                                        drRelacionCuatro["strMarketingCabin"] = drRelacionCuatro1["ResBookDesigCode"].ToString();
                                        bEntraClase = false;
                                        break;
                                    }
                                }
                                if (!bEntraClase)
                                    break;
                            }
                            if (bEntraClase)
                                drRelacionCuatro["strMarketingCabin"] = drRelacionCuatro.GetChildRows("FlightSegment_BookingClassAvail")[0]["ResBookDesigCode"].ToString();
                        }
                        catch
                        {
                            drRelacionCuatro["strMarketingCabin"] = "**";
                        }
                        try
                        {
                            drRelacionTres["strMarketingCabin"] = drRelacionCuatro["strMarketingCabin"];
                        }
                        catch { }
                        try
                        { drRelacionCuatro["strTPA_Extensions"] = drRelacionCuatro.GetChildRows("FlightSegment_TPA_Extensions")[0]["TPA_Extensions_Id"].ToString(); }
                        catch
                        { drRelacionCuatro["strTPA_Extensions"] = "**"; }
                        drRelacionCuatro["strParadas"] = "Directo";
                        drRelacionCuatro["strEstiloParada"] = "SinParada";
                        foreach (DataRow drRelacionCinco in drRelacionCuatro.GetChildRows("FlightSegment_TPA_Extensions"))
                        {
                            if (drRelacionCuatro["StopQuantity"].ToString().Equals("0"))
                            {
                                try
                                {
                                    drRelacionCuatro["strParadas"] = drRelacionCuatro["StopQuantity"].ToString();
                                }
                                catch
                                { drRelacionCuatro["strParadas"] = "0"; }
                            }
                            else
                            {
                                try
                                {
                                    drRelacionCuatro["strParadas"] = drRelacionCuatro["StopQuantity"].ToString();
                                    drRelacionCuatro["strEstiloParada"] = "ConParada";
                                }
                                catch
                                { drRelacionCuatro["strParadas"] = "0"; }

                            }
                        }
                    }
                    try
                    {
                        int iSecuence = int.Parse(drRelacionTres["OriginDestinationOption_Id"].ToString()) + 1;
                        drRelacionTres["SequenceNumber"] = iSecuence.ToString();
                    }
                    catch
                    { }
                }
            }
        }
   
        private void ModificarTablaFlightSegmentoCotiza(DataSet dsSabreAir)
        {
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
            string sMonedaUsd = clsValidaciones.GetKeyOrAdd("MonedaDolar", "USD");
            /*AGREGAMOS COLUMNAS A LA TABLA DE ITINERARIOS*/
            dtItinerario.Columns.Add("IntTotalPesos", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("IntTotalUsd", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("IntTotalDolares", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("str_Tipo_Moneda", typeof(string), "'" + vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion + "'");
            dtItinerario.Columns.Add("str_Tipo_MonedaUsd", typeof(string), "'" + sMonedaUsd + "'");
            dtItinerario.Columns.Add("IntPrecioDesde", typeof(decimal)).DefaultValue = "0";

            // Se incluyen columnas para visualizar
            dtItinerario.Columns.Add("strTotalView", typeof(string));
            dtItinerario.Columns.Add("strTipoMonedaView", typeof(string));
            dtItinerario.Columns.Add("strPrecioDesdeView", typeof(string));
            dtItinerario.Columns.Add("IntPrecioOferta", typeof(decimal)).DefaultValue = "0";
            dtItinerario.Columns.Add("IntPrecioOfertaPax", typeof(decimal)).DefaultValue = "0";

            dtItinerario.Columns.Add("bolImpuestos", typeof(bool));
            dtItinerario.Columns.Add("strTextoDG", typeof(string));
            dtItinerario.Columns.Add("imgOferta", typeof(string));
            dtItinerario.Columns.Add("imgConvenio", typeof(string));
        }
        private void SetRelacionesDsSabreAir(DataSet dsSabreAir)
        {
            /*AGREGAMOS LAS COLUMNAS A LA TABLA DE SEGMENTOS*/
            DataTable dtFlightSegment = dsSabreAir.Tables["FlightSegment"];
            DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];

            /*CREAMOS UNA TABLA PARA AEROLINEAS*/
            DataTable dtAirline = new DataTable("dtAirline");
            dtAirline.Columns.Add("strNombre_Aerolinea");
            dtAirline.Columns.Add("intId_Aerolinea");
            dtAirline.Columns.Add("Code");

            /*CREAMOS UNA TABLA PARA AEROPUERTOS*/
            DataTable dtAeropuertos = new DataTable("dtAeropuertos");
            dtAeropuertos.Columns.Add("strAeropuerto");
            dtAeropuertos.Columns.Add("IntId_Aeropuerto");
            dtAeropuertos.Columns.Add("IntId_Pais");
            dtAeropuertos.Columns.Add("Code_Aeropuerto");
            dtAeropuertos.Columns.Add("strCiudad");

            DataTable dtTasaAdministrativa = new DataTable("TasaAdministrativa");


            /*AGREGAMOS LAS TABLAS AL DATASET*/
            dsSabreAir.Tables.Add(dtAirline);
            dsSabreAir.Tables.Add(dtAeropuertos);
            dsSabreAir.Tables.Add(dtTasaAdministrativa);

            /*OBTENEMOS LAS AEROLINEAS DE LOS RESULTADOS DE SABRE*/
            DataTable dtAerolineas = GetDtAerolineas(dsSabreAir);
                       

            /*CONSULTAMOS LAS AEROLINEAS*/
            foreach (DataRow drFilaAeroline in dtAerolineas.Rows)
            {
                try
                {
                    /*CONSULTA LAS REFERENCIAS DE AEROLINEAS DE SABRE Y SI NO EXISTE LA CREA*/
                    DataTable dtaerolinea=ConsultarAerolinea(drFilaAeroline["Code"].ToString());
                    DataRow drNewRow = dtAirline.NewRow();
                    if (dtaerolinea.Rows.Count > 0)
                    {
                        drNewRow["strNombre_Aerolinea"] = dtaerolinea.Rows[0]["NAME"].ToString();
                        drNewRow["intId_Aerolinea"] = dtaerolinea.Rows[0]["INTCONAIR"].ToString();
                        drNewRow["Code"] = dtaerolinea.Rows[0]["CODE"].ToString();
                    }
                    else
                    {
                        drNewRow["strNombre_Aerolinea"] = "0";
                        drNewRow["intId_Aerolinea"] = "0";
                        drNewRow["Code"] = drFilaAeroline["Code"].ToString();
                    }
                    dtAirline.Rows.Add(drNewRow);
                }
                catch (Exception Ex)
                { 
                    /*throw new Exception(Ex.Message);*/
                    ExceptionHandled.Publicar("La aerolinea con codigo " + drFilaAeroline["Code"].ToString() + " no existe en la BD");
                }
            }
            DataTable dtCitys = GetDtCiudades(dsSabreAir);
            /*LIMPIAMOS LAS REFERENCIAS ANTERIORES*/
            /*CONSULTAMOS LAS CIUDADES DE SALIDA*/
            foreach (DataRow drFila in dtCitys.Rows)
            {
                /*CONSULTA LAS REFERENCIAS DE CIUDADES Y SI NO EXISTE LA CREA*/
                string Idioma = clsSesiones.getIdioma();
                if (Idioma.Equals(""))
                    Idioma = clsValidaciones.GetKeyOrAdd("sIdioma", "es");

                DataTable dtCiudad = new CsConsultasVuelos().Consultatabla("EXEC SPConsultaCiudadAeropuerto '" + drFila["Code"].ToString() + "','" + Idioma + "'");
                DataRow drNewRow = dtAeropuertos.NewRow();
                if (dtCiudad.Rows.Count > 0)
                {
                    drNewRow["strAeropuerto"] = dtCiudad.Rows[0]["STRAIRPORT"].ToString();
                    drNewRow["IntId_Aeropuerto"] = dtCiudad.Rows[0]["STRCOUNTRY"].ToString();
                    drNewRow["IntId_Pais"] = dtCiudad.Rows[0]["STRCOUNTRY"].ToString();
                    drNewRow["Code_Aeropuerto"] = dtCiudad.Rows[0]["STRCODE"].ToString();
                    drNewRow["strCiudad"] = dtCiudad.Rows[0]["STRDESCRIPTION"].ToString();
                }
                else
                {
                    drNewRow["strAeropuerto"] = drFila["Code"].ToString();
                    drNewRow["IntId_Aeropuerto"] = "0";
                    drNewRow["IntId_Pais"] = "0";
                    drNewRow["Code_Aeropuerto"] = drFila["Code"].ToString();
                    drNewRow["strCiudad"] = drFila["Code"].ToString();
                }
                dtAeropuertos.Rows.Add(drNewRow);
            }
            /*RELACIONES AEROPUERTOS*/
            dsSabreAir.Relations.Add("dtAeropuertos_dtFlightSegment_Departure", dtAeropuertos.Columns["Code_Aeropuerto"],
                dtFlightSegment.Columns["strDepartureAirport"]);

            dsSabreAir.Relations.Add("dtAeropuertos_dtFlightSegment_Arrival", dtAeropuertos.Columns["Code_Aeropuerto"],
                dtFlightSegment.Columns["strArrivalAirport"]);

            /*RELACIONES DE AEROLINEAS*/
            dsSabreAir.Relations.Add("dtAirline_dtFlightSegment", dtAirline.Columns["Code"],
                dtFlightSegment.Columns["strMarketingAirline"]);

            /*RELACIONES DE AEROLINEAS */
            try
            {
                dsSabreAir.Relations.Add("dtAirline_dtPricedItinerary", dtAirline.Columns["Code"],
               dtPricedItinerary.Columns["strMarketingAirline"]);
            }
            catch { }


            dsSabreAir.AcceptChanges();

            /*LLENAMOS LAS COLUMNAS RELACIONADAS */
            dtFlightSegment.Columns["strNombre_Aerolinea"].Expression = "Parent(dtAirline_dtFlightSegment).strNombre_Aerolinea";
            dtFlightSegment.Columns["intId_Aerolinea"].Expression = "Parent(dtAirline_dtFlightSegment).intId_Aerolinea";

            dtPricedItinerary.Columns["strNombre_Aerolinea"].Expression = "Parent(dtAirline_dtPricedItinerary).strNombre_Aerolinea";

            dtFlightSegment.Columns["strAeropuerto_Llegada"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Arrival).strAeropuerto";
            dtFlightSegment.Columns["IntId_Aeropuerto_Llegada"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Arrival).IntId_Aeropuerto";
            dtFlightSegment.Columns["IntId_Pais_Llegada"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Arrival).IntId_Pais";
            dtFlightSegment.Columns["Code_Aeropuerto_Llegada"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Arrival).Code_Aeropuerto";
            dtFlightSegment.Columns["strCiudad_Llegada"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Arrival).strCiudad";

            dtFlightSegment.Columns["strAeropuerto_Salida"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Departure).strAeropuerto";
            dtFlightSegment.Columns["IntId_Aeropuerto_Salida"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Departure).IntId_Aeropuerto";
            dtFlightSegment.Columns["IntId_Pais_Salida"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Departure).IntId_Pais";
            dtFlightSegment.Columns["Code_Aeropuerto_Salida"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Departure).Code_Aeropuerto";
            dtFlightSegment.Columns["strCiudad_Salida"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Departure).strCiudad";
        }

        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="dsSabreAir"></param>
        private void SetRelacionesDsSabreAirHora(DataSet dsSabreAir)
        {
            /*AGREGAMOS LAS COLUMNAS A LA TABLA DE SEGMENTOS*/
            DataTable dtFlightSegment = dsSabreAir.Tables["FlightSegment"];

            /*CREAMOS UNA TABLA PARA AEROLINEAS*/
            DataTable dtAirline = new DataTable("dtAirline");
            dtAirline.Columns.Add("strNombre_Aerolinea");
            dtAirline.Columns.Add("intId_Aerolinea");
            dtAirline.Columns.Add("Code");

            /*CREAMOS UNA TABLA PARA AEROPUERTOS*/
            DataTable dtAeropuertos = new DataTable("dtAeropuertos");
            dtAeropuertos.Columns.Add("strAeropuerto");
            dtAeropuertos.Columns.Add("IntId_Aeropuerto");
            dtAeropuertos.Columns.Add("IntId_Pais");
            dtAeropuertos.Columns.Add("Code_Aeropuerto");
            dtAeropuertos.Columns.Add("strCiudad");

            /*AGREGAMOS LAS TABLAS AL DATASET*/
            dsSabreAir.Tables.Add(dtAirline);
            dsSabreAir.Tables.Add(dtAeropuertos);

            /*OBTENEMOS LAS AEROLINEAS DE LOS RESULTADOS DE SABRE*/
            DataTable dtAerolineas = GetDtAerolineas(dsSabreAir);
            DataTable dtReferencias = new DataTable();
           
           

            /*CONSULTAMOS LAS AEROLINEAS*/
            foreach (DataRow drFilaAeroline in dtAerolineas.Rows)
            {
                try
                {

                    DataTable dt = new CsConsultasVuelos().Consultatabla(drFilaAeroline["Code"].ToString(), "TBLAIRLINE", new string[3] { "INTCONAIR", "CODE", "NAME" }, "CODE");
                    DataRow drNewRow = dtAirline.NewRow();
                    if (dt.Rows.Count > 0)
                    {
                        drNewRow["strNombre_Aerolinea"] = dt.Rows[0][2].ToString();
                        drNewRow["intId_Aerolinea"] = dt.Rows[0][0].ToString();
                        drNewRow["Code"] = dt.Rows[0][1].ToString();
                    }
                    else
                    {
                        drNewRow["strNombre_Aerolinea"] = drFilaAeroline["Code"].ToString();
                        drNewRow["intId_Aerolinea"] = "0";
                        drNewRow["Code"] = drFilaAeroline["Code"].ToString();
                    }
                    dtAirline.Rows.Add(drNewRow);
                }
                catch (Exception Ex)
                { throw new Exception(Ex.Message); }
            }
            DataTable dtCitys = GetDtCiudades(dsSabreAir);
            /*LIMPIAMOS LAS REFERENCIAS ANTERIORES*/
            /*CONSULTAMOS LAS CIUDADES DE SALIDA*/
            try
            {
                foreach (DataRow drFila in dtCitys.Rows)
                {
                    /*CONSULTA LAS REFERENCIAS DE CIUDADES Y SI NO EXISTE LA CREA*/
                   
                    DataTable dt = new CsConsultasVuelos().Consultatabla("EXEC SPCONSULTAAEROPUERTO '" + drFila["Code"].ToString()+"'");
                    DataRow drNewRow = dtAeropuertos.NewRow();
                    if (dt.Rows.Count > 0)
                    {
                        drNewRow["strAeropuerto"] = dt.Rows[0][2].ToString();
                        drNewRow["IntId_Aeropuerto"] = dt.Rows[0][2].ToString();
                        drNewRow["IntId_Pais"] = dt.Rows[0][1].ToString();
                        drNewRow["Code_Aeropuerto"] = dt.Rows[0][0].ToString();
                        drNewRow["strCiudad"] = dt.Rows[0][2].ToString();
                    }
                    else
                    {
                        drNewRow["strAeropuerto"] = drFila["Code"].ToString();
                        drNewRow["IntId_Aeropuerto"] = "0";
                        drNewRow["IntId_Pais"] = "0";
                        drNewRow["Code_Aeropuerto"] = drFila["Code"].ToString();
                        drNewRow["strCiudad"] = drFila["Code"].ToString();
                    }
                    dtAeropuertos.Rows.Add(drNewRow);
                }
            }
            catch { }
            /*RELACIONES AEROPUERTOS*/
            dsSabreAir.Relations.Add("dtAeropuertos_dtFlightSegment_Departure", dtAeropuertos.Columns["Code_Aeropuerto"],
                dtFlightSegment.Columns["strDepartureAirport"]);

            dsSabreAir.Relations.Add("dtAeropuertos_dtFlightSegment_Arrival", dtAeropuertos.Columns["Code_Aeropuerto"],
                dtFlightSegment.Columns["strArrivalAirport"]);

            /*RELACIONES DE AEROLINEAS*/
            dsSabreAir.Relations.Add("dtAirline_dtFlightSegment", dtAirline.Columns["Code"],
                dtFlightSegment.Columns["strMarketingAirline"]);

            dsSabreAir.AcceptChanges();

            /*LLENAMOS LAS COLUMNAS RELACIONADAS */
            dtFlightSegment.Columns["strNombre_Aerolinea"].Expression = "Parent(dtAirline_dtFlightSegment).strNombre_Aerolinea";
            dtFlightSegment.Columns["intId_Aerolinea"].Expression = "Parent(dtAirline_dtFlightSegment).intId_Aerolinea";

            dtFlightSegment.Columns["strAeropuerto_Llegada"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Arrival).strAeropuerto";
            dtFlightSegment.Columns["IntId_Aeropuerto_Llegada"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Arrival).IntId_Aeropuerto";
            dtFlightSegment.Columns["IntId_Pais_Llegada"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Arrival).IntId_Pais";
            dtFlightSegment.Columns["Code_Aeropuerto_Llegada"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Arrival).Code_Aeropuerto";
            dtFlightSegment.Columns["strCiudad_Llegada"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Arrival).strCiudad";

            dtFlightSegment.Columns["strAeropuerto_Salida"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Departure).strAeropuerto";
            dtFlightSegment.Columns["IntId_Aeropuerto_Salida"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Departure).IntId_Aeropuerto";
            dtFlightSegment.Columns["IntId_Pais_Salida"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Departure).IntId_Pais";
            dtFlightSegment.Columns["Code_Aeropuerto_Salida"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Departure).Code_Aeropuerto";
            dtFlightSegment.Columns["strCiudad_Salida"].Expression = "Parent(dtAeropuertos_dtFlightSegment_Departure).strCiudad";
        }
        private void SetTipoTrayecto(DataSet dsSabreAir)
        {
            DataTable dtFlightSegmento = dsSabreAir.Tables["FlightSegment"];
            string sIdioma = clsSesiones.getIdioma();

            if (sIdioma.Equals(""))
                sIdioma = clsValidaciones.GetKeyOrAdd("sIdioma", "es");

            string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");
            string sPais = clsValidaciones.GetKeyOrAdd("Paises", "Pais");
            try
            {
                DataTable dtpais = new CsConsultasVuelos().SPConsultaTabla("SPConsultapais",new string[2] {sPaisDefault,sIdioma});
                if (dtpais.Rows.Count > 0)
                {
                    string strCodigoCOL = dtpais.Rows[0]["INTCODE"].ToString();
                    dtFlightSegmento.Columns["strTipoTrayecto"].Expression = "IIF(IntId_Pais_Salida = '" + strCodigoCOL + "' AND IntId_Pais_Llegada = '" + strCodigoCOL + "','Nacional','Internacional' )";
                }
                else
                {
                    dtFlightSegmento.Columns["strTipoTrayecto"].Expression = "Internacional";
                }
            }
            catch
            {
                dtFlightSegmento.Columns["strTipoTrayecto"].Expression = "Internacional";
            }
        }
        private void ModificarValueView(DataSet dsSabreAir)
        {
            try
            {
                decimal dFactor = 1;
               
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
                DataTable dtPassengerFare = dsSabreAir.Tables["PassengerFare"];
                DataTable dtTax = dsSabreAir.Tables["Tax"];

                string sMonedaView = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion;
                if (clsValidaciones.GetKeyOrAdd("bValidaConvert", "False").ToUpper().Equals("TRUE"))
                    sMonedaView = clsValidaciones.GetKeyOrAdd("MonedaView", "COP");
                if (vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda.Equals(Enum_OrigenBusqueda.OfertasFijas))
                {
                    string sIdOferta = "0";
                    try
                    {
                        if (HttpContext.Current.Request.QueryString["IdOferta"] != null)
                        {
                            sIdOferta = HttpContext.Current.Request.QueryString["IdOferta"].ToString();
                        }
                    }
                    catch { }
                    DataSet dsData = new DataSet();
                  

                    string TipoPlan = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");

                    dsData = null;
                    if (dsData != null)
                    {
                        decimal dTotalView = Convert.ToDecimal(dsData.Tables[0].Rows[0]["dblPrecioDesde"].ToString());
                        decimal dTotalPaxView = Convert.ToDecimal(dsData.Tables[0].Rows[0]["dblPrecioDesde"].ToString());
                        int iCountPax = 0;

                        foreach (VO_Pasajero voPax in vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros)
                        {
                            try
                            {
                                iCountPax += int.Parse(voPax.SCantidad.ToString());
                            }
                            catch { }
                        }
                        if (iCountPax.Equals(0))
                            iCountPax = 1;

                        dTotalView = dTotalPaxView * iCountPax;

                        foreach (DataRow drItinerario in dtItinerario.Rows)
                        {
                            drItinerario["strTotalView"] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                            drItinerario["strTipoMonedaView"] = sMonedaView;
                            drItinerario["strPrecioDesdeView"] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                            drItinerario["IntPrecioOfertaPax"] = dTotalPaxView;
                            drItinerario["IntPrecioOferta"] = dTotalView;
                        }
                        foreach (DataRow drPassengerFare in dtPassengerFare.Rows)
                        {
                            drPassengerFare["strTotalImpuestosTasasView"] = "0";
                            drPassengerFare["strBaseFareView"] = dTotalPaxView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strTotalTarifaConTaXPersonaView"] = dTotalPaxView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strTotalTarifaConTaView"] = dTotalPaxView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strTipoMonedaTotalFareView"] = sMonedaView;
                        }
                        foreach (DataRow drTax in dtTax.Rows)
                        {
                            drTax["strTaxAmountView"] = "0";
                            drTax["strCurrencyCodeView"] = sMonedaView;
                        }
                    }
                    else
                    {
                        foreach (DataRow drItinerario in dtItinerario.Rows)
                        {
                            decimal dTotalView = Convert.ToDecimal(drItinerario["IntTotalPesos"].ToString()) / dFactor;
                            dTotalView = clsValidaciones.getDecimalRound(dTotalView.ToString());

                            drItinerario["strTotalView"] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                            drItinerario["strTipoMonedaView"] = sMonedaView;
                            drItinerario["strPrecioDesdeView"] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                            drItinerario["IntPrecioOfertaPax"] = 0;
                            drItinerario["IntPrecioOferta"] = 0;
                        }
                        foreach (DataRow drPassengerFare in dtPassengerFare.Rows)
                        {
                            decimal dTotalImpuestosTasasView = Convert.ToDecimal(drPassengerFare["IntTotalImpuestosTasas"].ToString()) / dFactor;
                            decimal dBaseFareView = Convert.ToDecimal(drPassengerFare["intBaseFare"].ToString()) / dFactor;
                            decimal dTotalTarifaConTaXPersonaView = Convert.ToDecimal(drPassengerFare["intTotalTarifaConTaXPersona"].ToString()) / dFactor; ;
                            decimal dTotalTarifaConTaView = Convert.ToDecimal(drPassengerFare["intTotalTarifaConTa"].ToString()) / dFactor;

                            dTotalImpuestosTasasView = clsValidaciones.getDecimalRound(dTotalImpuestosTasasView.ToString());
                            dBaseFareView = clsValidaciones.getDecimalRound(dBaseFareView.ToString());
                            dTotalTarifaConTaXPersonaView = clsValidaciones.getDecimalRound(dTotalTarifaConTaXPersonaView.ToString());
                            dTotalTarifaConTaView = clsValidaciones.getDecimalRound(dTotalTarifaConTaView.ToString());

                            drPassengerFare["strTotalImpuestosTasasView"] = dTotalImpuestosTasasView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strBaseFareView"] = dBaseFareView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strTotalTarifaConTaXPersonaView"] = dTotalTarifaConTaXPersonaView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strTotalTarifaConTaView"] = dTotalTarifaConTaView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strTipoMonedaTotalFareView"] = sMonedaView;
                        }
                        foreach (DataRow drTax in dtTax.Rows)
                        {
                            decimal dTaxAmountView = Convert.ToDecimal(drTax["Amount"].ToString()) / dFactor;
                            dTaxAmountView = clsValidaciones.getDecimalRound(dTaxAmountView.ToString());

                            drTax["strTaxAmountView"] = dTaxAmountView.ToString(FORMATO_NUMEROS_VIEW);
                            drTax["strCurrencyCodeView"] = sMonedaView;
                        }
                    }
                }
                else
                {
                    foreach (DataRow drItinerario in dtItinerario.Rows)
                    {
                        decimal dTotalView = Convert.ToDecimal(drItinerario["IntTotalPesos"].ToString()) / dFactor;
                        dTotalView = clsValidaciones.getDecimalRound(dTotalView.ToString());

                        drItinerario["strTotalView"] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                        drItinerario["strTipoMonedaView"] = sMonedaView;
                        drItinerario["strPrecioDesdeView"] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                        drItinerario["IntPrecioOfertaPax"] = 0;
                        drItinerario["IntPrecioOferta"] = 0;
                    }
                    foreach (DataRow drPassengerFare in dtPassengerFare.Rows)
                    {
                        decimal dTotalImpuestosTasasView = Convert.ToDecimal(drPassengerFare["IntTotalImpuestosTasas"].ToString()) / dFactor;
                        decimal dBaseFareView = Convert.ToDecimal(drPassengerFare["intBaseFare"].ToString()) / dFactor;
                        decimal dTotalTarifaConTaXPersonaView = Convert.ToDecimal(drPassengerFare["intTotalTarifaConTaXPersona"].ToString()) / dFactor; ;
                        decimal dTotalTarifaConTaView = Convert.ToDecimal(drPassengerFare["intTotalTarifaConTa"].ToString()) / dFactor;

                        dTotalImpuestosTasasView = clsValidaciones.getDecimalRound(dTotalImpuestosTasasView.ToString());
                        dBaseFareView = clsValidaciones.getDecimalRound(dBaseFareView.ToString());
                        dTotalTarifaConTaXPersonaView = clsValidaciones.getDecimalRound(dTotalTarifaConTaXPersonaView.ToString());
                        dTotalTarifaConTaView = clsValidaciones.getDecimalRound(dTotalTarifaConTaView.ToString());

                        drPassengerFare["strTotalImpuestosTasasView"] = dTotalImpuestosTasasView.ToString(FORMATO_NUMEROS_VIEW);
                        drPassengerFare["strBaseFareView"] = dBaseFareView.ToString(FORMATO_NUMEROS_VIEW);
                        drPassengerFare["strTotalTarifaConTaXPersonaView"] = dTotalTarifaConTaXPersonaView.ToString(FORMATO_NUMEROS_VIEW);
                        drPassengerFare["strTotalTarifaConTaView"] = dTotalTarifaConTaView.ToString(FORMATO_NUMEROS_VIEW);
                        drPassengerFare["strTipoMonedaTotalFareView"] = sMonedaView;
                    }
                    foreach (DataRow drTax in dtTax.Rows)
                    {
                        decimal dTaxAmountView = Convert.ToDecimal(drTax["Amount"].ToString()) / dFactor;
                        dTaxAmountView = clsValidaciones.getDecimalRound(dTaxAmountView.ToString());

                        drTax["strTaxAmountView"] = dTaxAmountView.ToString(FORMATO_NUMEROS_VIEW);
                        drTax["strCurrencyCodeView"] = sMonedaView;
                    }
                }
                dtItinerario.AcceptChanges();
                dtPassengerFare.AcceptChanges();
                dtTax.AcceptChanges();
            }
            catch { }
        }
        private void ModificarValueCotizaView(DataSet dsSabreAir)
        {
            try
            {
                decimal dFactor = 1;
               
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"];
                DataTable dtPassengerFare = dsSabreAir.Tables["PassengerFare"];
                DataTable dtTax = dsSabreAir.Tables["Tax"];

                string sMonedaView = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion;
                if (clsValidaciones.GetKeyOrAdd("bValidaConvert", "False").ToUpper().Equals("TRUE"))
                    sMonedaView = clsValidaciones.GetKeyOrAdd("MonedaView", "COP");
                if (vo_OTA_AirLowFareSearchLLSRQ.EOrigenBusqueda.Equals(Enum_OrigenBusqueda.OfertasFijas))
                {
                    string sIdOferta = "0";
                    try
                    {
                        if (HttpContext.Current.Request.QueryString["IdOferta"] != null)
                        {
                            sIdOferta = HttpContext.Current.Request.QueryString["IdOferta"].ToString();
                        }
                    }
                    catch { }
                    DataSet dsData = new DataSet();
                  

                    string TipoPlan = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");

                    //dsData = cPlanes.OfertasRutas(int.Parse(sIdOferta), TipoPlan, false);
                    if (dsData.Tables.Count > 0)
                    {
                        decimal dTotalView = Convert.ToDecimal(dsData.Tables[0].Rows[0]["dblPrecioDesde"].ToString());
                        decimal dTotalPaxView = Convert.ToDecimal(dsData.Tables[0].Rows[0]["dblPrecioDesde"].ToString());
                        int iCountPax = 0;

                        foreach (VO_Pasajero voPax in vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros)
                        {
                            try
                            {
                                iCountPax += int.Parse(voPax.SCantidad.ToString());
                            }
                            catch { }
                        }
                        if (iCountPax.Equals(0))
                            iCountPax = 1;

                        dTotalView = dTotalPaxView * iCountPax;

                        foreach (DataRow drItinerario in dtItinerario.Rows)
                        {
                            drItinerario["strTotalView"] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                            drItinerario["strTipoMonedaView"] = sMonedaView;
                            drItinerario["strPrecioDesdeView"] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                            drItinerario["IntPrecioOfertaPax"] = dTotalPaxView;
                            drItinerario["IntPrecioOferta"] = dTotalView;
                        }
                        foreach (DataRow drPassengerFare in dtPassengerFare.Rows)
                        {
                            drPassengerFare["strTotalImpuestosTasasView"] = "0";
                            drPassengerFare["strBaseFareView"] = dTotalPaxView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strTotalTarifaConTaXPersonaView"] = dTotalPaxView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strTotalTarifaConTaView"] = dTotalPaxView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strTipoMonedaTotalFareView"] = sMonedaView;
                        }
                        foreach (DataRow drTax in dtTax.Rows)
                        {
                            drTax["strTaxAmountView"] = "0";
                            drTax["strCurrencyCodeView"] = sMonedaView;
                        }
                    }
                    else
                    {
                        foreach (DataRow drItinerario in dtItinerario.Rows)
                        {
                            decimal dTotalView = Convert.ToDecimal(drItinerario["IntTotalPesos"].ToString()) / dFactor;
                            dTotalView = clsValidaciones.getDecimalRound(dTotalView.ToString());

                            drItinerario["strTotalView"] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                            drItinerario["strTipoMonedaView"] = sMonedaView;
                            drItinerario["strPrecioDesdeView"] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                            drItinerario["IntPrecioOfertaPax"] = 0;
                            drItinerario["IntPrecioOferta"] = 0;
                        }
                        foreach (DataRow drPassengerFare in dtPassengerFare.Rows)
                        {
                            decimal dTotalImpuestosTasasView = Convert.ToDecimal(drPassengerFare["IntTotalImpuestosTasas"].ToString()) / dFactor;
                            decimal dBaseFareView = Convert.ToDecimal(drPassengerFare["intBaseFare"].ToString()) / dFactor;
                            decimal dTotalTarifaConTaXPersonaView = Convert.ToDecimal(drPassengerFare["intTotalTarifaConTaXPersona"].ToString()) / dFactor; ;
                            decimal dTotalTarifaConTaView = Convert.ToDecimal(drPassengerFare["intTotalTarifaConTa"].ToString()) / dFactor;

                            dTotalImpuestosTasasView = clsValidaciones.getDecimalRound(dTotalImpuestosTasasView.ToString());
                            dBaseFareView = clsValidaciones.getDecimalRound(dBaseFareView.ToString());
                            dTotalTarifaConTaXPersonaView = clsValidaciones.getDecimalRound(dTotalTarifaConTaXPersonaView.ToString());
                            dTotalTarifaConTaView = clsValidaciones.getDecimalRound(dTotalTarifaConTaView.ToString());

                            drPassengerFare["strTotalImpuestosTasasView"] = dTotalImpuestosTasasView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strBaseFareView"] = dBaseFareView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strTotalTarifaConTaXPersonaView"] = dTotalTarifaConTaXPersonaView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strTotalTarifaConTaView"] = dTotalTarifaConTaView.ToString(FORMATO_NUMEROS_VIEW);
                            drPassengerFare["strTipoMonedaTotalFareView"] = sMonedaView;
                        }
                        foreach (DataRow drTax in dtTax.Rows)
                        {
                            decimal dTaxAmountView = Convert.ToDecimal(drTax["Amount"].ToString()) / dFactor;
                            dTaxAmountView = clsValidaciones.getDecimalRound(dTaxAmountView.ToString());

                            drTax["strTaxAmountView"] = dTaxAmountView.ToString(FORMATO_NUMEROS_VIEW);
                            drTax["strCurrencyCodeView"] = sMonedaView;
                        }
                    }
                }
                else
                {
                    foreach (DataRow drItinerario in dtItinerario.Rows)
                    {
                        decimal dTotalView = Convert.ToDecimal(drItinerario["IntTotalPesos"].ToString()) / dFactor;
                        dTotalView = clsValidaciones.getDecimalRound(dTotalView.ToString());

                        drItinerario["strTotalView"] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                        drItinerario["strTipoMonedaView"] = sMonedaView;
                        drItinerario["strPrecioDesdeView"] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                        drItinerario["IntPrecioOfertaPax"] = 0;
                        drItinerario["IntPrecioOferta"] = 0;
                    }
                    foreach (DataRow drPassengerFare in dtPassengerFare.Rows)
                    {
                        decimal dTotalImpuestosTasasView = Convert.ToDecimal(drPassengerFare["IntTotalImpuestosTasas"].ToString()) / dFactor;
                        decimal dBaseFareView = Convert.ToDecimal(drPassengerFare["intBaseFare"].ToString()) / dFactor;
                        decimal dTotalTarifaConTaXPersonaView = Convert.ToDecimal(drPassengerFare["intTotalTarifaConTaXPersona"].ToString()) / dFactor; ;
                        decimal dTotalTarifaConTaView = Convert.ToDecimal(drPassengerFare["intTotalTarifaConTa"].ToString()) / dFactor;

                        dTotalImpuestosTasasView = clsValidaciones.getDecimalRound(dTotalImpuestosTasasView.ToString());
                        dBaseFareView = clsValidaciones.getDecimalRound(dBaseFareView.ToString());
                        dTotalTarifaConTaXPersonaView = clsValidaciones.getDecimalRound(dTotalTarifaConTaXPersonaView.ToString());
                        dTotalTarifaConTaView = clsValidaciones.getDecimalRound(dTotalTarifaConTaView.ToString());

                        drPassengerFare["strTotalImpuestosTasasView"] = dTotalImpuestosTasasView.ToString(FORMATO_NUMEROS_VIEW);
                        drPassengerFare["strBaseFareView"] = dBaseFareView.ToString(FORMATO_NUMEROS_VIEW);
                        drPassengerFare["strTotalTarifaConTaXPersonaView"] = dTotalTarifaConTaXPersonaView.ToString(FORMATO_NUMEROS_VIEW);
                        drPassengerFare["strTotalTarifaConTaView"] = dTotalTarifaConTaView.ToString(FORMATO_NUMEROS_VIEW);
                        drPassengerFare["strTipoMonedaTotalFareView"] = sMonedaView;
                    }
                    foreach (DataRow drTax in dtTax.Rows)
                    {
                        decimal dTaxAmountView = Convert.ToDecimal(drTax["Amount"].ToString()) / dFactor;
                        dTaxAmountView = clsValidaciones.getDecimalRound(dTaxAmountView.ToString());

                        drTax["strTaxAmountView"] = dTaxAmountView.ToString(FORMATO_NUMEROS_VIEW);
                        drTax["strCurrencyCodeView"] = sMonedaView;
                    }
                }
                dtItinerario.AcceptChanges();
                dtPassengerFare.AcceptChanges();
                dtTax.AcceptChanges();
            }
            catch { }
        }
        /// <summary>
        /// Metodo que modifica la vista de ofertas areas (Puntos)
        /// </summary>
        /// <param name="dsSabreAir">Dataset vuelos</param>
        /// <remarks> 
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-04-17
        /// -------------------
        /// Control de Cambios
        /// -------------------           
        /// </remarks>
        public DataTable ClonarTabla(DataSet dsSabreAir, string strNombreTabla)
        {/*METODO QUE CLONA LAS COLUMNAS DE UNA TABLA SIN LOS CONSTRAIS NI RELACIONES*/
            DataTable dtRetorno = new DataTable();

            for (int c = 0; c < dsSabreAir.Tables[strNombreTabla].Columns.Count; c++)
            {
                dtRetorno.Columns.Add(dsSabreAir.Tables[strNombreTabla].Columns[c].ColumnName, dsSabreAir.Tables[strNombreTabla].Columns[c].DataType);
            }
            return dtRetorno;
        }
        private DataTable GetDtFlightSegmento(string PricedItinerary_Id, DataSet dsSabreAir)
        {
            DataRow drFilaEncontrada = dsSabreAir.Tables["PricedItinerary"].Rows.Find(PricedItinerary_Id);

            DataTable dtTablaRetornada = ClonarTabla(dsSabreAir, "FlightSegment");

            foreach (DataRow drFilaRelacionUno in drFilaEncontrada.GetChildRows("PricedItinerary_AirItinerary"))
            {
                foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItinerary_OriginDestinationOptions"))
                {
                    foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("OriginDestinationOptions_OriginDestinationOption"))
                    {
                        foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("OriginDestinationOption_FlightSegment"))
                        {
                            dtTablaRetornada.Rows.Add(drRelacionCuatro.ItemArray);
                        }
                    }
                }
            }
            return dtTablaRetornada;
        }
        private DataTable GetDtFlightSegmentoHoras(string PricedItinerary_Id, DataSet dsSabreAir)
        {
            DataRow drFilaEncontrada = dsSabreAir.Tables["OriginDestinationOption"].Rows.Find(PricedItinerary_Id);

            DataTable dtTablaRetornada = ClonarTabla(dsSabreAir, "FlightSegment");

            foreach (DataRow drRelacionCuatro in drFilaEncontrada.GetChildRows("OriginDestinationOption_FlightSegment"))
            {
                dtTablaRetornada.Rows.Add(drRelacionCuatro.ItemArray);
            }
            return dtTablaRetornada;
        }
        public DataTable GetDtFlightSegmentoHorasCotiza()
        {
            DataSet dsDataSelect = clsSesiones.GetDatasetSelectSabreAir();
            DataTable dtTablaRetornada = dsDataSelect.Tables["FlightSegment"];
            return dtTablaRetornada;
        }
        private DataTable GetDtPassengerFare(string PTC_FareBreakdown_Id, DataSet dsSabreAir)
        {
            DataRow drFilaEncontrada = dsSabreAir.Tables["PTC_FareBreakdown"].Rows.Find(PTC_FareBreakdown_Id);
            DataTable dtTablaRetornada = ClonarTabla(dsSabreAir, "PassengerFare");
            foreach (DataRow drRelacionUno in drFilaEncontrada.GetChildRows("PTC_FareBreakdown_PassengerFare"))
            {
                dtTablaRetornada.Rows.Add(drRelacionUno.ItemArray);
            }
            return dtTablaRetornada;
        }
        private DataTable GetDtAerolineas(DataSet dsSabreAir)
        {
            DataTable dtMarketingAirline = dsSabreAir.Tables["MarketingAirline"];

            return dtMarketingAirline.DefaultView.ToTable(true, "Code");
        }
        private DataTable GetDtCiudades(DataSet dsSabreAir)
        {
            DataTable dtMarketingAirline = dsSabreAir.Tables["FlightSegment"];
            DataTable dtDatosVista = dtMarketingAirline.DefaultView.ToTable(true, "strDepartureAirport", "strArrivalAirport");
            DataTable dtAeropuertos = new DataTable();
            dtAeropuertos.Columns.Add("Code");
            dtAeropuertos.PrimaryKey = new DataColumn[] { dtAeropuertos.Columns["Code"] };

            for (int i = 0; i < dtDatosVista.Rows.Count; i++)
            {
                try
                {
                    dtAeropuertos.Rows.Add(dtDatosVista.Rows[i]["strDepartureAirport"].ToString());
                }
                catch
                { }
                try
                {
                    dtAeropuertos.Rows.Add(dtDatosVista.Rows[i]["strArrivalAirport"].ToString());
                }
                catch
                { }
            }
            return dtAeropuertos;
        }
        private DataTable GetDtPassengerFareTax(string PassengerFare_Id, DataSet dsSabreAir)
        {
            DataRow drFilaEncontrada = dsSabreAir.Tables["PassengerFare"].Rows.Find(PassengerFare_Id);
            DataTable dtTablaRetornada = new DataTable();
            try
            {
                try
                {
                    dtTablaRetornada = dsSabreAir.Tables["Tax"].Clone();
                }
                catch
                {
                    dtTablaRetornada = CrearTablaTax();
                }


                string sTANacional = clsValidaciones.GetKeyOrAdd("TAN", "TAN");
                string sITANacional = clsValidaciones.GetKeyOrAdd("IVA_TAN", "ITAN");
                string sTAInterNacional = clsValidaciones.GetKeyOrAdd("TAI", "TAI");
                string sITAInterNacional = clsValidaciones.GetKeyOrAdd("IVA_TAI", "ITAI");


                string sTA = clsValidaciones.GetKeyOrAdd("TASA_ADMINISTRATIVA", "TA");
                string sITA = clsValidaciones.GetKeyOrAdd("IVA_TA", "ITA");
                string sTextoTA = clsValidaciones.GetKeyOrAdd("TEXTO_TA", "Tarifa Administrativa");
                string sTextoITA = clsValidaciones.GetKeyOrAdd("TEXTO_IVA_TA", "IVA Tarifa Administrativa");
                string sFEE = clsValidaciones.GetKeyOrAdd("FEE", "FEE");
                string sIFEE = clsValidaciones.GetKeyOrAdd("FEE_IMPUESTO", "IFEE");
                string sADFE = clsValidaciones.GetKeyOrAdd("FEE_Adicional", "ADFE");
                string sIADFE = clsValidaciones.GetKeyOrAdd("IVA_FEE_Adicional", "IADFE");
                string sFEENAL = clsValidaciones.GetKeyOrAdd("FEENAL", "FEENAL");
                string sIFEENAL = clsValidaciones.GetKeyOrAdd("IVA_FEENAL", "IFEENAL");
                string sTextoFEENAL = clsValidaciones.GetKeyOrAdd("TEXTO_FEENAL", "APT");
                string sTextoIFEENAL = clsValidaciones.GetKeyOrAdd("TEXTO_IVA_FEENAL", "IVA APT");
                string sFEEINAL = clsValidaciones.GetKeyOrAdd("FEEINTERNAL", "FEEINAL"); ;
                string sIFEEINAL = clsValidaciones.GetKeyOrAdd("IVA_FEEINTERNAL", "IFEEINAL");
                string sTextoFEEINAL = clsValidaciones.GetKeyOrAdd("TEXTO_FEEINAL", "APT");
                string sTextoIFEEINAL = clsValidaciones.GetKeyOrAdd("TEXTO_IVA_FEEINAL", "IVA APT");

                string sCodeIva = clsValidaciones.GetKeyOrAdd("CodeSabreYS", "YS");
                string sCodeTasaAir = clsValidaciones.GetKeyOrAdd("CodeSabreCO", "CO");
                string sCodeImpSalida = clsValidaciones.GetKeyOrAdd("CodeSabreDG", "DG");
                string sCodeImpComb = clsValidaciones.GetKeyOrAdd("CodeSabreYQ", "YQ");

                string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");

                StringBuilder strExpression = new StringBuilder();
                /*VALIDACION DE TITULOS DE TASAS*/
                strExpression.Append("IIF(TaxCode LIKE '" + sCodeIva + "*'");
                strExpression.Append(",'IVA'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sCodeTasaAir + "*'");
                strExpression.Append(",'Tasas Aeroportuarias'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sCodeImpSalida + "*'");
                strExpression.Append(",'Impuesto Salida " + sPaisDefault + "*'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sCodeImpComb + "*'");
                strExpression.Append(",'Impuesto combustible'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sTA + "'");
                strExpression.Append(",'" + sTextoTA + "'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sITA + "'");
                strExpression.Append(",'" + sTextoITA + "'");
                //----------------
                strExpression.Append(",IIF(TaxCode LIKE '" + sTANacional + "'");
                strExpression.Append(",'" + sTextoTA + "'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sITANacional + "'");
                strExpression.Append(",'" + sTextoITA + "'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sTAInterNacional + "'");
                strExpression.Append(",'" + sTextoTA + "'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sITAInterNacional + "'");
                strExpression.Append(",'" + sTextoITA + "'");
                //----------------
                strExpression.Append(",IIF(TaxCode LIKE '" + sFEE + "'");
                strExpression.Append(",'Fee'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sIFEE + "'");
                strExpression.Append(",'Impuesto Fee'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sADFE + "'");
                strExpression.Append(",'FeeAdicional'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sIADFE + "'");
                strExpression.Append(",'IVA Fee Adicional'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sFEENAL + "'");
                strExpression.Append(",'" + sTextoFEENAL + "'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sIFEENAL + "'");
                strExpression.Append(",'" + sTextoIFEENAL + "'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sFEEINAL + "'");
                strExpression.Append(",'" + sTextoFEEINAL + "'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sIFEEINAL + "'");
                strExpression.Append(",'" + sTextoIFEEINAL + "'");
                strExpression.Append(",'OTROS'");
                strExpression.Append("))))))))))))))))))");
                try
                {
                    dtTablaRetornada.Columns.Add("strNombre_Impuesto", typeof(string), strExpression.ToString());
                    dtTablaRetornada.Columns.Add("strCurrencyCodeView", typeof(string));
                }
                catch { }
                foreach (DataRow drTaxes in drFilaEncontrada.GetChildRows("PassengerFare_Taxes"))
                {
                    foreach (DataRow drTax in drTaxes.GetChildRows("Taxes_Tax"))
                    {
                        //validacion para el tamaño del arreglo pues la fila contenia una columna mas alojada en el medio 
                        object[] taxes = new object[drTax.ItemArray.Length - 1];
                        int Indicador = 0;
                        for (int i = 0; i < drTax.ItemArray.Length; i++)
                        {
                            if (i != 5)
                                taxes[Indicador++] = drTax.ItemArray[i];
                        }
                        //-----------------------------------------------------
                        dtTablaRetornada.Rows.Add(taxes);
                    }
                }
            }
            catch (Exception Ex)
            {
                string exep = Ex.Message;
            }
            return dtTablaRetornada;
        }
        private DataTable GetDtPassengerFareTaxCotiza(string PassengerFare_Id, DataSet dsSabreAir)
        {
            DataRow drFilaEncontrada = dsSabreAir.Tables["PassengerFare"].Rows.Find(PassengerFare_Id);
            DataTable dtTablaRetornada = new DataTable();
            try
            {
                try
                {
                    dtTablaRetornada = dsSabreAir.Tables["Tax"].Clone();
                }
                catch
                {
                    dtTablaRetornada = CrearTablaTaxCotiza();
                }
                string sTA = clsValidaciones.GetKeyOrAdd("TASA_ADMINISTRATIVA", "TA");
                string sITA = clsValidaciones.GetKeyOrAdd("IVA_TA", "ITA");
                string sTextoTA = clsValidaciones.GetKeyOrAdd("TEXTO_TA", "Tarifa Administrativa");
                string sTextoITA = clsValidaciones.GetKeyOrAdd("TEXTO_IVA_TA", "IVA Tarifa Administrativa");
                string sFEE = clsValidaciones.GetKeyOrAdd("FEE", "FEE");
                string sIFEE = clsValidaciones.GetKeyOrAdd("FEE_IMPUESTO", "IFEE");
                string sADFE = clsValidaciones.GetKeyOrAdd("FEE_Adicional", "ADFE");
                string sIADFE = clsValidaciones.GetKeyOrAdd("IVA_FEE_Adicional", "IADFE");
                string sFEENAL = clsValidaciones.GetKeyOrAdd("FEENAL", "FEENAL");
                string sIFEENAL = clsValidaciones.GetKeyOrAdd("IVA_FEENAL", "IFEENAL");
                string sTextoFEENAL = clsValidaciones.GetKeyOrAdd("TEXTO_FEENAL", "APT");
                string sTextoIFEENAL = clsValidaciones.GetKeyOrAdd("TEXTO_IVA_FEENAL", "IVA APT");
                string sFEEINAL = clsValidaciones.GetKeyOrAdd("FEEINTERNAL", "FEEINAL"); ;
                string sIFEEINAL = clsValidaciones.GetKeyOrAdd("IVA_FEEINTERNAL", "IFEEINAL");
                string sTextoFEEINAL = clsValidaciones.GetKeyOrAdd("TEXTO_FEEINAL", "APT");
                string sTextoIFEEINAL = clsValidaciones.GetKeyOrAdd("TEXTO_IVA_FEEINAL", "IVA APT");

                string sCodeIva = clsValidaciones.GetKeyOrAdd("CodeSabreYS", "YS");
                string sCodeTasaAir = clsValidaciones.GetKeyOrAdd("CodeSabreCO", "CO");
                string sCodeImpSalida = clsValidaciones.GetKeyOrAdd("CodeSabreDG", "DG");
                string sCodeImpComb = clsValidaciones.GetKeyOrAdd("CodeSabreYQ", "YQ");

                string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");

                StringBuilder strExpression = new StringBuilder();
                /*VALIDACION DE TITULOS DE TASAS*/
                strExpression.Append("IIF(TaxCode LIKE '" + sCodeIva + "*'");
                strExpression.Append(",'IVA'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sCodeTasaAir + "*'");
                strExpression.Append(",'Tasas Aeroportuarias'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sCodeImpSalida + "*'");
                strExpression.Append(",'Impuesto Salida " + sPaisDefault + "*'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sCodeImpComb + "*'");
                strExpression.Append(",'Impuesto combustible'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sTA + "'");
                strExpression.Append(",'" + sTextoTA + "'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sITA + "'");
                strExpression.Append(",'" + sTextoITA + "'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sFEE + "'");
                strExpression.Append(",'Fee'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sIFEE + "'");
                strExpression.Append(",'Impuesto Fee'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sADFE + "'");
                strExpression.Append(",'FeeAdicional'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sIADFE + "'");
                strExpression.Append(",'IVA Fee Adicional'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sFEENAL + "'");
                strExpression.Append(",'" + sTextoFEENAL + "'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sIFEENAL + "'");
                strExpression.Append(",'" + sTextoIFEENAL + "'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sFEEINAL + "'");
                strExpression.Append(",'" + sTextoFEEINAL + "'");
                strExpression.Append(",IIF(TaxCode LIKE '" + sIFEEINAL + "'");
                strExpression.Append(",'" + sTextoIFEEINAL + "'");
                strExpression.Append(",'OTROS'");
                strExpression.Append("))))))))))))))");
                try
                {
                    dtTablaRetornada.Columns.Add("strNombre_Impuesto", typeof(string), strExpression.ToString());
                }
                catch { }
                foreach (DataRow drTaxes in drFilaEncontrada.GetChildRows("PassengerFare_Taxes"))
                {
                    foreach (DataRow drTax in drTaxes.GetChildRows("Taxes_Tax"))
                    {
                        dtTablaRetornada.Rows.Add(drTax.ItemArray);
                    }
                }
            }
            catch (Exception Ex)
            {
                string exep = Ex.Message;
            }
            return dtTablaRetornada;
        }
        private DataTable GetDtPassengerFareTax(DataSet dsSabreAir)
        {
            DataTable dtTablaRetornada = new DataTable();
            try
            {

                dtTablaRetornada = dsSabreAir.Tables["Tax"];

                try
                {
                    dtTablaRetornada.Columns.Add("strNombre_Impuesto", typeof(string));
                }
                catch { }
                try
                {
                    string sTA = clsValidaciones.GetKeyOrAdd("TASA_ADMINISTRATIVA", "TA");
                    string sITA = clsValidaciones.GetKeyOrAdd("IVA_TA", "ITA");
                    string sTextoTA = clsValidaciones.GetKeyOrAdd("TEXTO_TA", "Tarifa Administrativa");
                    string sTextoITA = clsValidaciones.GetKeyOrAdd("TEXTO_IVA_TA", "IVA Tarifa Administrativa");
                    string sFEE = clsValidaciones.GetKeyOrAdd("FEE", "FEE");
                    string sIFEE = clsValidaciones.GetKeyOrAdd("FEE_IMPUESTO", "IFEE");
                    string sADFE = clsValidaciones.GetKeyOrAdd("FEE_Adicional", "ADFE");
                    string sIADFE = clsValidaciones.GetKeyOrAdd("IVA_FEE_Adicional", "IADFE");
                    string sFEENAL = clsValidaciones.GetKeyOrAdd("FEENAL", "FEENAL");
                    string sIFEENAL = clsValidaciones.GetKeyOrAdd("IVA_FEENAL", "IFEENAL");
                    string sTextoFEENAL = clsValidaciones.GetKeyOrAdd("TEXTO_FEENAL", "APT");
                    string sTextoIFEENAL = clsValidaciones.GetKeyOrAdd("TEXTO_IVA_FEENAL", "IVA APT");
                    string sFEEINAL = clsValidaciones.GetKeyOrAdd("FEEINTERNAL", "FEEINAL"); ;
                    string sIFEEINAL = clsValidaciones.GetKeyOrAdd("IVA_FEEINTERNAL", "IFEEINAL");
                    string sTextoFEEINAL = clsValidaciones.GetKeyOrAdd("TEXTO_FEEINAL", "APT");
                    string sTextoIFEEINAL = clsValidaciones.GetKeyOrAdd("TEXTO_IVA_FEEINAL", "IVA APT");

                    string sCodeIva = clsValidaciones.GetKeyOrAdd("CodeSabreYS", "YS");
                    string sCodeTasaAir = clsValidaciones.GetKeyOrAdd("CodeSabreCO", "CO");
                    string sCodeImpSalida = clsValidaciones.GetKeyOrAdd("CodeSabreDG", "DG");
                    string sCodeImpComb = clsValidaciones.GetKeyOrAdd("CodeSabreYQ", "YQ");

                    string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");

                    StringBuilder strExpression = new StringBuilder();
                    /*VALIDACION DE TITULOS DE TASAS*/
                    strExpression.Append("IIF(TaxCode LIKE '" + sCodeIva + "*'");
                    strExpression.Append(",'IVA'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sCodeTasaAir + "*'");
                    strExpression.Append(",'Tasas Aeroportuarias'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sCodeImpSalida + "*'");
                    strExpression.Append(",'Impuesto Salida " + sPaisDefault + "*'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sCodeImpComb + "*'");
                    strExpression.Append(",'Impuesto combustible'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sTA + "'");
                    strExpression.Append(",'" + sTextoTA + "'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sITA + "'");
                    strExpression.Append(",'" + sTextoITA + "'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sFEE + "'");
                    strExpression.Append(",'Fee'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sIFEE + "'");
                    strExpression.Append(",'Impuesto Fee'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sADFE + "'");
                    strExpression.Append(",'FeeAdicional'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sIADFE + "'");
                    strExpression.Append(",'IVA Fee Adicional'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sFEENAL + "'");
                    strExpression.Append(",'" + sTextoFEENAL + "'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sIFEENAL + "'");
                    strExpression.Append(",'" + sTextoIFEENAL + "'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sFEEINAL + "'");
                    strExpression.Append(",'" + sTextoFEEINAL + "'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sIFEEINAL + "'");
                    strExpression.Append(",'" + sTextoIFEEINAL + "'");
                    strExpression.Append(",'OTROS'");
                    strExpression.Append("))))))))))))))");

                    for (int i = 0; i < dtTablaRetornada.Rows.Count; i++)
                    {
                        if (dtTablaRetornada.Rows[i]["strNombre_Impuesto"].ToString().Length.Equals(0))
                        {
                            if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Contains(sCodeIva))
                            {
                                dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "IVA";
                            }
                            else
                            {
                                if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Contains(sCodeTasaAir))
                                {
                                    dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "Tasas Aeroportuarias";
                                }
                                else
                                {
                                    if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Contains(sCodeImpSalida))
                                    {
                                        dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "Impuesto Salida " + sPaisDefault + "*";
                                    }
                                    else
                                    {
                                        if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Contains(sCodeImpComb))
                                        {
                                            dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "Impuesto combustible";
                                        }
                                        else
                                        {
                                            if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sTA))
                                            {
                                                dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "Tarifa Administrativa";
                                            }
                                            else
                                            {
                                                if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sITA))
                                                {
                                                    dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "IVA Tarifa Administrativa";
                                                }
                                                else
                                                {
                                                    if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sFEE))
                                                    {
                                                        dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "Fee";
                                                    }
                                                    else
                                                    {
                                                        if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sIFEE))
                                                        {
                                                            dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "Impuesto Fee";
                                                        }
                                                        else
                                                        {
                                                            if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sADFE))
                                                            {
                                                                dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "FeeAdicional";
                                                            }
                                                            else
                                                            {
                                                                if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sIADFE))
                                                                {
                                                                    dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "IVA Fee Adicional";
                                                                }
                                                                else
                                                                {
                                                                    if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sFEENAL))
                                                                    {
                                                                        dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "APT";
                                                                    }
                                                                    else
                                                                    {
                                                                        if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sIFEENAL))
                                                                        {
                                                                            dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "IVA APT";
                                                                        }
                                                                        else
                                                                        {
                                                                            dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "OTROS";
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
            }
            catch { }
            return dtTablaRetornada;
        }
        private DataTable GetDtPassengerFareTaxCotiza(DataSet dsSabreAir)
        {
            DataTable dtTablaRetornada = new DataTable();
            try
            {

                dtTablaRetornada = dsSabreAir.Tables["Tax"];

                try
                {
                    dtTablaRetornada.Columns.Add("strNombre_Impuesto", typeof(string));
                }
                catch { }
                try
                {
                    string sTA = clsValidaciones.GetKeyOrAdd("TASA_ADMINISTRATIVA", "TA");
                    string sITA = clsValidaciones.GetKeyOrAdd("IVA_TA", "ITA");
                    string sTextoTA = clsValidaciones.GetKeyOrAdd("TEXTO_TA", "Tarifa Administrativa");
                    string sTextoITA = clsValidaciones.GetKeyOrAdd("TEXTO_IVA_TA", "IVA Tarifa Administrativa");
                    string sFEE = clsValidaciones.GetKeyOrAdd("FEE", "FEE");
                    string sIFEE = clsValidaciones.GetKeyOrAdd("FEE_IMPUESTO", "IFEE");
                    string sADFE = clsValidaciones.GetKeyOrAdd("FEE_Adicional", "ADFE");
                    string sIADFE = clsValidaciones.GetKeyOrAdd("IVA_FEE_Adicional", "IADFE");
                    string sFEENAL = clsValidaciones.GetKeyOrAdd("FEENAL", "FEENAL");
                    string sIFEENAL = clsValidaciones.GetKeyOrAdd("IVA_FEENAL", "IFEENAL");
                    string sTextoFEENAL = clsValidaciones.GetKeyOrAdd("TEXTO_FEENAL", "APT");
                    string sTextoIFEENAL = clsValidaciones.GetKeyOrAdd("TEXTO_IVA_FEENAL", "IVA APT");
                    string sFEEINAL = clsValidaciones.GetKeyOrAdd("FEEINTERNAL", "FEEINAL"); ;
                    string sIFEEINAL = clsValidaciones.GetKeyOrAdd("IVA_FEEINTERNAL", "IFEEINAL");
                    string sTextoFEEINAL = clsValidaciones.GetKeyOrAdd("TEXTO_FEEINAL", "APT");
                    string sTextoIFEEINAL = clsValidaciones.GetKeyOrAdd("TEXTO_IVA_FEEINAL", "IVA APT");

                    string sCodeIva = clsValidaciones.GetKeyOrAdd("CodeSabreYS", "YS");
                    string sCodeTasaAir = clsValidaciones.GetKeyOrAdd("CodeSabreCO", "CO");
                    string sCodeImpSalida = clsValidaciones.GetKeyOrAdd("CodeSabreDG", "DG");
                    string sCodeImpComb = clsValidaciones.GetKeyOrAdd("CodeSabreYQ", "YQ");

                    string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");

                    StringBuilder strExpression = new StringBuilder();
                    /*VALIDACION DE TITULOS DE TASAS*/
                    strExpression.Append("IIF(TaxCode LIKE '" + sCodeIva + "*'");
                    strExpression.Append(",'IVA'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sCodeTasaAir + "*'");
                    strExpression.Append(",'Tasas Aeroportuarias'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sCodeImpSalida + "*'");
                    strExpression.Append(",'Impuesto Salida " + sPaisDefault + "*'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sCodeImpComb + "*'");
                    strExpression.Append(",'Impuesto combustible'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sTA + "'");
                    strExpression.Append(",'" + sTextoTA + "'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sITA + "'");
                    strExpression.Append(",'" + sTextoITA + "'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sFEE + "'");
                    strExpression.Append(",'Fee'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sIFEE + "'");
                    strExpression.Append(",'Impuesto Fee'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sADFE + "'");
                    strExpression.Append(",'FeeAdicional'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sIADFE + "'");
                    strExpression.Append(",'IVA Fee Adicional'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sFEENAL + "'");
                    strExpression.Append(",'" + sTextoFEENAL + "'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sIFEENAL + "'");
                    strExpression.Append(",'" + sTextoIFEENAL + "'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sFEEINAL + "'");
                    strExpression.Append(",'" + sTextoFEEINAL + "'");
                    strExpression.Append(",IIF(TaxCode LIKE '" + sIFEEINAL + "'");
                    strExpression.Append(",'" + sTextoIFEEINAL + "'");
                    strExpression.Append(",'OTROS'");
                    strExpression.Append("))))))))))))))");

                    for (int i = 0; i < dtTablaRetornada.Rows.Count; i++)
                    {
                        if (dtTablaRetornada.Rows[i]["strNombre_Impuesto"].ToString().Length.Equals(0))
                        {
                            if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Contains(sCodeIva))
                            {
                                dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "IVA";
                            }
                            else
                            {
                                if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Contains(sCodeTasaAir))
                                {
                                    dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "Tasas Aeroportuarias";
                                }
                                else
                                {
                                    if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Contains(sCodeImpSalida))
                                    {
                                        dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "Impuesto Salida " + sPaisDefault + "*";
                                    }
                                    else
                                    {
                                        if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Contains(sCodeImpComb))
                                        {
                                            dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "Impuesto combustible";
                                        }
                                        else
                                        {
                                            if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sTA))
                                            {
                                                dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "Tarifa Administrativa";
                                            }
                                            else
                                            {
                                                if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sITA))
                                                {
                                                    dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "IVA Tarifa Administrativa";
                                                }
                                                else
                                                {
                                                    if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sFEE))
                                                    {
                                                        dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "Fee";
                                                    }
                                                    else
                                                    {
                                                        if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sIFEE))
                                                        {
                                                            dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "Impuesto Fee";
                                                        }
                                                        else
                                                        {
                                                            if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sADFE))
                                                            {
                                                                dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "FeeAdicional";
                                                            }
                                                            else
                                                            {
                                                                if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sIADFE))
                                                                {
                                                                    dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "IVA Fee Adicional";
                                                                }
                                                                else
                                                                {
                                                                    if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sFEENAL))
                                                                    {
                                                                        dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "APT";
                                                                    }
                                                                    else
                                                                    {
                                                                        if (dtTablaRetornada.Rows[i]["TaxCode"].ToString().Equals(sIFEENAL))
                                                                        {
                                                                            dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "IVA APT";
                                                                        }
                                                                        else
                                                                        {
                                                                            dtTablaRetornada.Rows[i]["strNombre_Impuesto"] = "OTROS";
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
            }
            catch { }
            return dtTablaRetornada;
        }
        public DataTable GetDtFlightSegmento(string PricedItinerary_Id)
        {
            DataSet dsSabreAir = this.dsSabreAir;

            if (dsSabreAir.Tables.Count == 0)
            {
                dsSabreAir = clsSesiones.GetDatasetSabreAir();
            }
            DataTable dtRespuesta = new DataTable();

            if (dsSabreAir != null)
            {
                dtRespuesta = GetDtFlightSegmento(PricedItinerary_Id, dsSabreAir);
            }
            return dtRespuesta;
        }
        public DataTable GetDtFlightSegmentoHoras(string PricedItinerary_Id)
        {
            DataSet dsSabreAir = this.dsSabreAir;

            if (dsSabreAir.Tables.Count == 0)
            {
                dsSabreAir = clsSesiones.GetDatasetSabreAir();
            }
            DataTable dtRespuesta = new DataTable();

            if (dsSabreAir != null)
            {
                dtRespuesta = GetDtFlightSegmentoHoras(PricedItinerary_Id, dsSabreAir);
            }
            return dtRespuesta;
        }
        public DataTable GetDtPassengerFare(string PTC_FareBreakdown_Id)
        {
            DataTable dtRespuesta = new DataTable();

            if (this.dsSabreAir.Tables.Count == 0)
            {
                this.dsSabreAir = dsSabreAir = clsSesiones.GetDatasetSabreAir();
            }

            if (dsSabreAir != null)
            {
                dtRespuesta = GetDtPassengerFare(PTC_FareBreakdown_Id, this.dsSabreAir);
            }
            return dtRespuesta;
        }
        public DataTable GetDtPassengerFareTax(string PassengerFare_Id)
        {
            DataTable dtRespuesta = new DataTable();
            try
            {
                if (dsSabreAir.Tables.Count == 0)
                {
                    dsSabreAir = clsSesiones.GetDatasetSabreAir();
                }
                if (dsSabreAir != null)
                {
                    dtRespuesta = GetDtPassengerFareTax(PassengerFare_Id, this.dsSabreAir);
                }
            }
            catch { }
            return dtRespuesta;
        }
        public DataTable GetDtPassengerFareTaxCotiza(string PassengerFare_Id)
        {
            DataTable dtRespuesta = new DataTable();
            try
            {
                if (dsSabreAir.Tables.Count == 0)
                {
                    dsSabreAir = clsSesiones.GetDatasetSabreAir();
                }
                if (dsSabreAir != null)
                {
                    dtRespuesta = GetDtPassengerFareTaxCotiza(PassengerFare_Id, this.dsSabreAir);
                }
            }
            catch { }
            return dtRespuesta;
        }
        public DataTable GetDtPassengerFareTax()
        {
            DataTable dtRespuesta = new DataTable();
            try
            {
                if (dsSabreAir.Tables.Count == 0)
                {
                    dsSabreAir = clsSesiones.GetDatasetSabreAir();
                }
                if (dsSabreAir != null)
                {
                    dtRespuesta = GetDtPassengerFareTax(this.dsSabreAir);
                }
            }
            catch { }
            return dtRespuesta;
        }
        public DataTable GetDtPassengerFareTaxCotiza()
        {
            DataTable dtRespuesta = new DataTable();
            try
            {
                if (dsSabreAir.Tables.Count == 0)
                {
                    dsSabreAir = clsSesiones.GetDatasetSabreAir();
                }
                if (dsSabreAir != null)
                {
                    dtRespuesta = GetDtPassengerFareTaxCotiza(this.dsSabreAir);
                }
            }
            catch { }
            return dtRespuesta;
        }
        public DataTable GetDtPassengerTypeQuantity(string PricedItinerary_Id)
        {
            DataTable dtRespuesta = new DataTable();

            if (dsSabreAir.Tables.Count == 0)
            {
                dsSabreAir = clsSesiones.GetDatasetSabreAir();
            }
            if (dsSabreAir != null)
            {
                dtRespuesta = GetDtPassengerTypeQuantity(PricedItinerary_Id, this.dsSabreAir);
            }
            return dtRespuesta;
        }
        public DataTable GetDtPassengerTypeQuantity()
        {
            DataTable dtRespuesta = new DataTable();

            if (dsSabreAir.Tables.Count == 0)
            {
                dsSabreAir = clsSesiones.GetDatasetSabreAir();
            }
            if (dsSabreAir != null)
            {
                dtRespuesta = GetDtPassengerTypeQuantity(this.dsSabreAir);
            }
            return dtRespuesta;
        }
        private DataTable GetDtPassengerTypeQuantity(string PricedItinerary_Id, DataSet dsSabreAir)
        {
            DataRow drFilaEncontrada = dsSabreAir.Tables["PricedItinerary"].Rows.Find(PricedItinerary_Id);

            DataTable dtTablaRetornada = ClonarTabla(dsSabreAir, "PassengerTypeQuantity");

            foreach (DataRow drFilaRelacionUno in drFilaEncontrada.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
            {
                foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItineraryPricingInfo_PTC_FareBreakdowns"))
                {
                    foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("PTC_FareBreakdowns_PTC_FareBreakdown"))
                    {
                        foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerTypeQuantity"))
                        {
                            dtTablaRetornada.Rows.Add(drRelacionCuatro.ItemArray);
                        }
                    }
                }
            }
            //hceron verify if multidestination
            //25022013
            if (dtTablaRetornada != null && dtTablaRetornada.Rows.Count == 0)
            {
                dtTablaRetornada = GetDtPassengerTypeQuantityMulti(PricedItinerary_Id,  dsSabreAir);
            }
            return dtTablaRetornada;
        }
        private DataTable GetDtPassengerTypeQuantity(DataSet dsSabreAir)
        {
            DataRow[] drFilaEncontrada = dsSabreAir.Tables["PricedItinerary"].Select();
            DataTable dtTablaRetornada = ClonarTabla(dsSabreAir, "PassengerTypeQuantity");

            foreach (DataRow drFilaRelacionUno in drFilaEncontrada)
            {
                foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                {
                    foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("AirItineraryPricingInfo_PTC_FareBreakdowns"))
                    {
                        foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerTypeQuantity"))
                        {
                            dtTablaRetornada.Rows.Add(drRelacionCuatro.ItemArray);
                        }
                    }
                }
            }
            return dtTablaRetornada;
        }
        private void ModificarItinerario(DataSet dsSabreAir)
        {
            bool bEntra = false;
            string sValidaMatriz = clsValidaciones.GetKeyOrAdd("bValidaMatriz", "False");
            if (sValidaMatriz.ToUpper().Equals("TRUE"))
                bEntra = true;

            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();

            DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
            DataTable dtFlightSegmento = dsSabreAir.Tables["FlightSegment"];
            string sMonedaCop = clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP");
            string sMonedaUsd = clsValidaciones.GetKeyOrAdd("MonedaDolar", "USD");

            foreach (DataRow drFilaItinerario in dtPricedItinerary.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drFilaItinerario.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItineraryPricingInfo_PTC_FareBreakdowns"))
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.Equals(sMonedaCop))
                        {
                            drFilaItinerario["IntTotalPesos"] = drRelacionDos["IntTotalSumaTarifaConTAPasajeros"];
                            try
                            { drFilaItinerario["IntTotalUsd"] = drRelacionDos["IntTotalSumaTarifaConTAPasajeros"]; }
                            catch
                            { drFilaItinerario["IntTotalUsd"] = 0; }
                        }
                        else if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.Equals(sMonedaUsd))
                        {
                            drFilaItinerario["IntTotalPesos"] = clsValidaciones.getDecimalNotRound(drRelacionDos["IntTotalSumaTarifaConTAPasajeros"].ToString());
                            try
                            { drFilaItinerario["IntTotalUsd"] = clsValidaciones.getDecimalNotRound(drRelacionDos["IntTotalSumaTarifaConTAPasajeros"].ToString()); }
                            catch
                            { drFilaItinerario["IntTotalUsd"] = 0; }
                        }
                    }
                }
            }
            DataView dvFlightSegment = new DataView(dtPricedItinerary);
            string[] sAirLine = new string[5];
            sAirLine[0] = "strMarketingAirline";
            sAirLine[1] = "strNombre_Aerolinea";
            sAirLine[2] = "IntPrecioDesde";
            sAirLine[3] = "StopQuantity";
            sAirLine[4] = "str_Tipo_Moneda";

            DataTable dtFiltro = dvFlightSegment.ToTable(true, sAirLine);
            dtFiltro.TableName = "dtFilter";
            if (bEntra)
            {
                foreach (DataRow drFiltro in dtFiltro.Rows)
                {
                    decimal dPrecioMin = 0;
                    foreach (DataRow drFilaItinerario in dtPricedItinerary.Rows)
                    {
                        try
                        {
                            if (drFilaItinerario["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                            {
                                if (drFilaItinerario["StopQuantity"].ToString().Trim().Equals(drFiltro["StopQuantity"].ToString().Trim()))
                                {
                                    if (dPrecioMin.Equals(0))
                                    {
                                        dPrecioMin = Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString());
                                    }
                                    else
                                    {
                                        if (dPrecioMin > Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString()))
                                        {
                                            dPrecioMin = Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                    drFiltro["IntPrecioDesde"] = dPrecioMin;
                }
                foreach (DataRow drFiltro in dtFiltro.Rows)
                {
                    foreach (DataRow drFlightSegment in dtFlightSegmento.Rows)
                    {
                        if (drFlightSegment["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                        {
                            if (drFlightSegment["StopQuantity"].ToString().Trim().Equals(drFiltro["StopQuantity"].ToString().Trim()))
                            {
                                drFlightSegment["IntPrecioDesde"] = drFiltro["IntPrecioDesde"];
                            }
                        }
                    }
                }
                foreach (DataRow drFiltro in dtFiltro.Rows)
                {
                    foreach (DataRow drPricedItinerary in dtPricedItinerary.Rows)
                    {
                        if (drPricedItinerary["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                        {
                            if (drPricedItinerary["StopQuantity"].ToString().Trim().Equals(drFiltro["StopQuantity"].ToString().Trim()))
                            {
                                drPricedItinerary["IntPrecioDesde"] = drFiltro["IntPrecioDesde"];
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (DataRow drFiltro in dtFiltro.Rows)
                {
                    decimal dPrecioMin = 0;
                    foreach (DataRow drFilaItinerario in dtPricedItinerary.Rows)
                    {
                        try
                        {
                            if (drFilaItinerario["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                            {
                                if (dPrecioMin.Equals(0))
                                {
                                    dPrecioMin = Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString());
                                }
                                else
                                {
                                    if (dPrecioMin > Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString()))
                                    {
                                        dPrecioMin = Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString());
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                    drFiltro["IntPrecioDesde"] = dPrecioMin;
                }
                foreach (DataRow drFiltro in dtFiltro.Rows)
                {
                    foreach (DataRow drFlightSegment in dtFlightSegmento.Rows)
                    {
                        if (drFlightSegment["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                        {
                            drFlightSegment["IntPrecioDesde"] = drFiltro["IntPrecioDesde"];
                        }
                    }
                }
                foreach (DataRow drFiltro in dtFiltro.Rows)
                {
                    foreach (DataRow drPricedItinerary in dtPricedItinerary.Rows)
                    {
                        if (drPricedItinerary["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                        {
                            drPricedItinerary["IntPrecioDesde"] = drFiltro["IntPrecioDesde"];
                        }
                    }
                }
            }
            try
            {
                dsSabreAir.Tables.Add(dtFiltro);
            }
            catch { }
        }
        private void ModificarConsecutivo(DataSet dsSabreAir)
        {
            try
            {
                DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
                DataTable dtPricedItineraryTemp = dsSabreAir.Tables["PricedItinerary"];
                try
                {
                    dtPricedItinerary.Columns.Add("Consecutivo", typeof(int));
                }
                catch { }
                int iPos = 1;
                DataView dtvPricedItinerary = new DataView(dtPricedItineraryTemp);
                try
                {
                    dtvPricedItinerary.Sort = "IntTotalPesos";
                    dtPricedItineraryTemp = dtvPricedItinerary.ToTable();
                    dtPricedItineraryTemp.AcceptChanges();
                }
                catch
                { }
                foreach (DataRow drFilaItinerarioTemp in dtPricedItineraryTemp.Rows)
                {
                    foreach (DataRow drFilaItinerario in dtPricedItinerary.Rows)
                    {
                        if (drFilaItinerarioTemp["PricedItinerary_Id"].Equals(drFilaItinerario["PricedItinerary_Id"]))
                        {
                            try
                            {
                                drFilaItinerario["Consecutivo"] = iPos;
                            }
                            catch { }
                            iPos++;
                        }
                    }
                }
                dtPricedItinerary.AcceptChanges();
                dsSabreAir.AcceptChanges();
            }
            catch { }
        }
        private void ActualizaConsecutivo(DataTable dtPricedItinerary)
        {
            try
            {
                DataTable dtPricedItineraryTemp = dtPricedItinerary;
                int iPos = 1;
                DataView dtvPricedItinerary = new DataView(dtPricedItineraryTemp);
                try
                {
                    dtvPricedItinerary.Sort = "IntTotalPesos";
                    dtPricedItineraryTemp = dtvPricedItinerary.ToTable();
                    dtPricedItineraryTemp.AcceptChanges();
                }
                catch
                { }
                foreach (DataRow drFilaItinerarioTemp in dtPricedItineraryTemp.Rows)
                {
                    foreach (DataRow drFilaItinerario in dtPricedItinerary.Rows)
                    {
                        if (drFilaItinerarioTemp["PricedItinerary_Id"].Equals(drFilaItinerario["PricedItinerary_Id"]))
                        {
                            try
                            {
                                drFilaItinerario["Consecutivo"] = iPos;
                            }
                            catch { }
                            iPos++;
                        }
                    }
                }
                dtPricedItinerary.AcceptChanges();
            }
            catch { }
        }
        private void ModificarConsecutivoHoras(DataSet dsSabreAir)
        {
            try
            {
                DataTable dtPricedItinerary = dsSabreAir.Tables["OriginDestinationOption"];
                DataTable dtPricedItineraryTemp = dsSabreAir.Tables["OriginDestinationOption"];
                try
                {
                    dtPricedItinerary.Columns.Add("Consecutivo", typeof(int));
                }
                catch { }
                int iPos = 1;
                foreach (DataRow drFilaItinerarioTemp in dtPricedItineraryTemp.Rows)
                {
                    foreach (DataRow drFilaItinerario in dtPricedItinerary.Rows)
                    {
                        if (drFilaItinerarioTemp["OriginDestinationOption_Id"].Equals(drFilaItinerario["OriginDestinationOption_Id"]))
                        {
                            try
                            {
                                drFilaItinerario["Consecutivo"] = iPos;
                            }
                            catch { }
                            iPos++;
                        }
                    }
                }
                dtPricedItinerary.AcceptChanges();
                dsSabreAir.AcceptChanges();
            }
            catch { }
        }
        private void ModificarItinerarioCotiza(DataSet dsSabreAir)
        {
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();

            DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
            DataTable dtFlightSegmento = dsSabreAir.Tables["FlightSegment"];

            foreach (DataRow drFilaItinerario in dtPricedItinerary.Rows)
            {
                decimal dTotalCop = 0;
                decimal dTotalUsd = 0;

                foreach (DataRow drFilaRelacionUno in drFilaItinerario.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItineraryPricingInfo_PTC_FareBreakdown"))
                    {
                        if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.Equals(clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano")))
                        {
                            dTotalCop += clsValidaciones.getDecimalNotRound(drRelacionDos["IntTotalTarifaConTAPasajeros"].ToString());
                            try
                            { dTotalUsd += clsValidaciones.getDecimalNotRound(drRelacionDos["IntTotalTarifaConTAPasajeros"].ToString()); }
                            catch
                            { }
                        }
                        else if (vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion.Equals(clsValidaciones.GetKeyOrAdd("MonedaDolar")))
                        {
                            dTotalCop += clsValidaciones.getDecimalNotRound(drRelacionDos["IntTotalTarifaConTAPasajeros"].ToString());
                            try
                            { dTotalUsd += clsValidaciones.getDecimalNotRound(drRelacionDos["IntTotalTarifaConTAPasajeros"].ToString()); }
                            catch
                            { }
                        }
                    }
                }
                drFilaItinerario["IntTotalPesos"] = dTotalCop;
                try
                { drFilaItinerario["IntTotalUsd"] = dTotalUsd; }
                catch
                { drFilaItinerario["IntTotalUsd"] = 0; }
            }
            DataView dvFlightSegment = new DataView(dtFlightSegmento);
            string[] sAirLine = new string[2];
            sAirLine[0] = "strMarketingAirline";
            sAirLine[1] = "IntPrecioDesde";
            DataTable dtFiltro = dvFlightSegment.ToTable(true, sAirLine);

            foreach (DataRow drFiltro in dtFiltro.Rows)
            {
                decimal dPrecioMin = 0;
                foreach (DataRow drFilaItinerario in dtPricedItinerary.Rows)
                {
                    try
                    {
                        if (drFilaItinerario["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                        {
                            if (dPrecioMin.Equals(0))
                            {
                                dPrecioMin = Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString());
                            }
                            else
                            {
                                if (dPrecioMin > Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString()))
                                {
                                    dPrecioMin = Convert.ToDecimal(drFilaItinerario["IntTotalPesos"].ToString());
                                }
                            }
                        }
                    }
                    catch { }
                }
                drFiltro["IntPrecioDesde"] = dPrecioMin;
            }
            foreach (DataRow drFiltro in dtFiltro.Rows)
            {
                foreach (DataRow drFlightSegment in dtFlightSegmento.Rows)
                {
                    if (drFlightSegment["strMarketingAirline"].ToString().Trim().Equals(drFiltro["strMarketingAirline"].ToString().Trim()))
                    {
                        drFlightSegment["IntPrecioDesde"] = drFiltro["IntPrecioDesde"];
                    }
                }
            }
        }
        private DataTable GetDtGetItinerario(string PricedItinerary_Id, DataSet dsSabreAir)
        {
            DataRow drFilaEncontrada = dsSabreAir.Tables["PricedItinerary"].Rows.Find(PricedItinerary_Id);

            DataTable dtTablaRetornada = ClonarTabla(dsSabreAir, "PricedItinerary");

            dtTablaRetornada.Rows.Add(drFilaEncontrada.ItemArray);

            return dtTablaRetornada;
        }
        public DataTable GetDtGetItinerario(string strRPH)
        {
            DataTable dtRespuesta = new DataTable();

            if (dsSabreAir.Tables.Count == 0)
            {
                this.dsSabreAir = clsSesiones.GetDatasetSabreAir();
            }
            if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
            {

                dtRespuesta = GetDtGetItinerario(strRPH, dsSabreAir);

            }
            return dtRespuesta;
        }
        public DataTable GetDtGetItinerarioHora(string strRPH)
        {
            DataTable dtRespuesta = new DataTable();

            if (dsSabreAir.Tables.Count == 0)
            {
                this.dsSabreAir = clsSesiones.GetDatasetSabreAir();
            }
            if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
            {

                dtRespuesta = GetDtGetItinerarioHora(strRPH, dsSabreAir);

            }
            return dtRespuesta;
        }
        private DataTable GetDtGetItinerarioHora(string PricedItinerary_Id, DataSet dsSabreAir)
        {
            DataRow drFilaEncontrada = dsSabreAir.Tables["OriginDestinationOption"].Rows.Find(PricedItinerary_Id);

            DataTable dtTablaRetornada = ClonarTabla(dsSabreAir, "OriginDestinationOption");

            dtTablaRetornada.Rows.Add(drFilaEncontrada.ItemArray);

            return dtTablaRetornada;
        }
        private void GetDtGetItinerarioHora(string PricedItinerary_Id, DataSet dsSabreAir, DataTable dtTable)
        {
            DataRow drFilaEncontrada = dsSabreAir.Tables["OriginDestinationOption"].Rows.Find(PricedItinerary_Id);

            foreach (DataRow drRelacion in drFilaEncontrada.GetChildRows("OriginDestinationOption_FlightSegment"))
            {
                dtTable.Rows.Add(drRelacion.ItemArray);
            }
        }
        private DataTable GetDtSegmento(int idSegmento, DataTable dtTable)
        {
            string sSelect = "intSelect=" + idSegmento.ToString();
            DataTable dtRespuesta = dtTable.Clone();
            dtRespuesta.Clear();
            try
            {
                DataRow[] drFilaEncontrada = dtTable.Select(sSelect);

                foreach (DataRow drRelacion in drFilaEncontrada)
                {
                    dtRespuesta.Rows.Add(drRelacion.ItemArray);
                }
            }
            catch { }
            return dtRespuesta;
        }
        public DataSet GetDsSabreAir()
        {
            /*RETORNAMOS TODO EL DATASET GUARDADO EN SESSION*/
            DataSet dsRespuesta = new DataSet();
            dsRespuesta = clsSesiones.GetDatasetSabreAir();
            return dsRespuesta;
        }
        private void GetTasaAdmin(DataSet dsSabreAir, Enum_TipoTrayecto EnumTipoTrayecto, Enum_TipoVuelo EnumTipoVuelo)
        {
           
            string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL").ToString();
            string sPais = setTomarPaisPseudo();
            if (sPaisDefault.Equals(sPais))
            {
                GetTaNacional(dsSabreAir, EnumTipoTrayecto, sIdioma, sAplicacion, EnumTipoVuelo);
                GetFeeNal(dsSabreAir, sIdioma, sAplicacion, EnumTipoTrayecto, EnumTipoVuelo);
            }
                //switch (EnumTipoVuelo)
                //{
                //    case Enum_TipoVuelo.Nacional:
                //        GetTaNacional(dsSabreAir, EnumTipoTrayecto, sIdioma, sAplicacion, EnumTipoVuelo);
                //        GetFeeNal(dsSabreAir, sIdioma, sAplicacion, EnumTipoTrayecto, EnumTipoVuelo);
                //        break;
                //    case Enum_TipoVuelo.Internacional:
                //        GetTaInternacional(dsSabreAir, sIdioma, sAplicacion);
                //        GetFeeInterNal(dsSabreAir, sIdioma, sAplicacion);
                //        break;
                //    default:
                //        break;
                //}
            //}
            //else
            //{
            //    GetFee(dsSabreAir, sIdioma, sAplicacion);
            //}
        }
        private string setTomarPaisPseudo()
        {
            string sPais = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL").ToString();

            VO_Credentials vo_Credentials = csReferencias.csCredenciales(Enum_ProveedorWebServices.Sabre);
            try
            {
                if (vo_Credentials.PccPais != null)
                    sPais = vo_Credentials.PccPais;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Resupera PccPais";
                ExceptionHandled.Publicar(cParametros);
            }
            return sPais;
        }
        private void GetMarckup(DataSet dsSabreAir)
        {
            try
            {
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                decimal dMarckup = 0;
                decimal dMarckupUsd = decimal.Parse(clsValidaciones.GetKeyOrAdd("dMarckupUsd", "0"));
                bool bUsd = false;
                try
                {
                    if (vo_OTA_AirLowFareSearchLLSRQ != null)
                        dMarckup = vo_OTA_AirLowFareSearchLLSRQ.DMarkUp;
                }
                catch { }
                if (dMarckup > 0)
                {
                    DataTable dtTarifas = dsSabreAir.Tables["BaseFare"];
                    DataTable dtEquivFare = dsSabreAir.Tables["EquivFare"];
                    DataTable dtTax = dsSabreAir.Tables["Tax"];
                    for (int t = 0; t < dtTarifas.Rows.Count; t++)
                    {
                        /*ADICIONAMOS LA TARIFA*/
                        decimal dValor = clsValidaciones.getDecimalNotRound(dtTarifas.Rows[t]["Amount"].ToString());
                        dValor = dValor / (1 - (dMarckup / 100));
                        dtTarifas.Rows[t]["Amount"] = clsValidaciones.getDecimalRound(dValor.ToString());
                    }
                    for (int e = 0; e < dtEquivFare.Rows.Count; e++)
                    {
                        /*ADICIONAMOS LA TARIFA*/
                        decimal dValor = clsValidaciones.getDecimalNotRound(dtEquivFare.Rows[e]["Amount"].ToString());
                        dValor = dValor / (1 - (dMarckup / 100));
                        dtEquivFare.Rows[e]["Amount"] = clsValidaciones.getDecimalRound(dValor.ToString());
                    }
                    for (int c = 0; c < dtTax.Rows.Count; c++)
                    {
                        if (dtTax.Rows[c]["TaxCode"].ToString().Contains("YS"))
                        {
                            decimal dValor = clsValidaciones.getDecimalNotRound(dtTax.Rows[c]["Amount"].ToString());
                            dValor = dValor / (1 - (dMarckup / 100));
                            dtTax.Rows[c]["Amount"] = clsValidaciones.getDecimalRound(dValor.ToString());
                        }
                    }
                    dtTarifas.AcceptChanges();
                    dtEquivFare.AcceptChanges();
                    dtTax.AcceptChanges();
                }
            }
            catch { }
        }
        private void GetFeeNal(DataSet dsSabreAir, string strIdioma, string strAplicacion, Enum_TipoTrayecto EnumTipoTrayecto, Enum_TipoVuelo Enum_TipoVuelo)
        {

            string strTrayecto = String.Empty;
            string strTpodestino = String.Empty;
            string strTpoFee = String.Empty;
            string strTpoIvaFee = String.Empty;
            bool bMultidestino = false;
            switch (EnumTipoTrayecto)
            {
                case Enum_TipoTrayecto.Ida:
                    strTrayecto = "O";
                    break;
                case Enum_TipoTrayecto.IdaRegreso:
                    strTrayecto = "R";
                    break;
                case Enum_TipoTrayecto.Multidestino:
                    strTrayecto = "R";
                    bMultidestino = true;
                    break;
                default:
                    break;
            }

            if (Enum_TipoVuelo.ToString().Trim().ToUpper().Equals("NACIONAL"))
            {
                strTpoFee = clsValidaciones.GetKeyOrAdd("FEENAL", "FEENAL");
                strTpoIvaFee = clsValidaciones.GetKeyOrAdd("IFEENAL", "IFEENAL");
            }
            else
            {
                strTpoFee = clsValidaciones.GetKeyOrAdd("FEEINAL", "FEEINAL");
                strTpoIvaFee = clsValidaciones.GetKeyOrAdd("IVA_FEEINTERNAL", "IFEEINAL");
                strTrayecto = clsValidaciones.GetKeyOrAdd("TpotrayectoCombinado", "C");
            }


            strTpodestino = new CsConsultasVuelos().ConsultaCodigo(Enum_TipoVuelo.ToString().Trim().ToUpper(), "TBLTPODESTINO", "INTCODE", "STRDESCRIPTION");
            strTrayecto = new CsConsultasVuelos().ConsultaCodigo(strTrayecto, "TBLTPOTRAYECTO", "INTCODE", "STRCODE");

            decimal dValorDolar = Convert.ToDecimal(clsSesiones.GET_USD_SABRE());
            string[] sColsFilter = new string[1];
            sColsFilter[0] = "strMarketingAirline";
            DataTable dtPassengerFare = dsSabreAir.Tables["PassengerFare"];
            DataTable dtItin = dsSabreAir.Tables["PricedItinerary"];
            DataTable dtTax = dsSabreAir.Tables["Tax"];
            DataTable dtTaxes = dsSabreAir.Tables["Taxes"];
            DataView dvAirline = new DataView(dtItin);
            DataTable tblAirFilter = dvAirline.ToTable(true, sColsFilter);
            string sEmpresa = csReferencias.csEmpresa();

            string sMonedaCop = clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP");
            string sMonedaUsd = clsValidaciones.GetKeyOrAdd("MonedaDolar", "USD");
            string sMonedaLoc = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
            string sFEENAL = clsValidaciones.GetKeyOrAdd("FEENAL", "FEENAL");
            string sIFEENAL = clsValidaciones.GetKeyOrAdd("IFEENAL", "IFEENAL");

            DataSet dstDatos = new DataSet();
            string sPorcentajeTarifa = clsValidaciones.GetKeyOrAdd("IndicadorValorPorcentajeTarifa", "1");
            string sNetoTarifa = clsValidaciones.GetKeyOrAdd("IndicadorValorNetoTarifa", "0");
            string sNetoTotal = clsValidaciones.GetKeyOrAdd("IndicadorValorNetoTotal", "2");
            string sPorcentajeTotal = clsValidaciones.GetKeyOrAdd("IndicadorValorPorcentajeTotal", "3");

            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            string sMonedaCotiza = vo_OTA_AirLowFareSearchLLSRQ.SCodMonedaCotizacion;
            csVuelos cVuelos = new csVuelos();


            //----------------------------- VALIDACION DE INSERCION DE LAS TARIFAS PERO EN LA AEROLINEA CORRESPONDIENTE
            if (!bMultidestino)
            {
                for (int i = 0; i < tblAirFilter.Rows.Count; i++)
                {
                    foreach (DataRow drItinerario in dtItin.Rows)
                    {
                        foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                        {
                            foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItineraryPricingInfo_PTC_FareBreakdowns"))
                            {
                                foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("PTC_FareBreakdowns_PTC_FareBreakdown"))
                                {
                                    foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                                    {
                                        try
                                        {
                                            if (drItinerario["strMarketingAirline"].ToString().Equals(tblAirFilter.Rows[i][0].ToString()))
                                            {
                                                DataTable dtDatos = null;
                                                if (Enum_TipoVuelo.ToString().Trim().ToUpper().Equals("NACIONAL"))
                                                {
                                                    dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFeeEmpresa", new string[6] { sEmpresa, tblAirFilter.Rows[i][0].ToString(), strTpodestino, strTrayecto, "0", "0" });
                                                }
                                                else
                                                {
                                                    dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFeeEmpresaDestinos", new string[6] { sEmpresa, tblAirFilter.Rows[i][0].ToString(), strTpodestino, strTrayecto, clsValidaciones.getDecimalNotRound(drRelacionCuatro["intBaseFare"].ToString()).ToString(), "0" });
                                                }

                                                if (dtDatos == null)
                                                {

                                                    //ExceptionHandled.Publicar("FEE No encontrada datos Empresa:" + sEmpresa + " Aerolinea:" + tblAirFilter.Rows[i][0].ToString() + " Tipodestino:" + strTpodestino + " Tpotrayecto:" + strTrayecto);
                                                    if (Enum_TipoVuelo.ToString().Trim().ToUpper().Equals("NACIONAL"))
                                                    {
                                                        dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFeeEmpresa", new string[6] { sEmpresa, "00", strTpodestino, strTrayecto, clsValidaciones.getDecimalNotRound(drRelacionCuatro["intBaseFare"].ToString()).ToString(), "1" });

                                                    }
                                                    else
                                                    {
                                                        dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFeeEmpresaDestinos", new string[6] { sEmpresa, tblAirFilter.Rows[i][0].ToString(), strTpodestino, strTrayecto, clsValidaciones.getDecimalNotRound(drRelacionCuatro["intBaseFare"].ToString()).ToString(), "2" });
                                                    }
                                                    //if (dstDatos.Tables.Count== 0)
                                                    //    ExceptionHandled.Publicar("FEE No encontrada datos Empresa:" + sEmpresa + " Aerolinea:00  Tipodestino:" + strTpodestino + " Tpotrayecto:" + strTrayecto + " ValorRango:" + clsValidaciones.getDecimalNotRound(drRelacionCuatro["intBaseFare"].ToString()));
                                                }

                                                if (dtDatos != null)
                                                {

                                                    decimal dBaseFareUSD = 0;
                                                    string intBaseFare = "0";

                                                    dBaseFareUSD = clsValidaciones.getDecimalNotRound(drRelacionCuatro["intBaseFare"].ToString());
                                                    intBaseFare = clsValidaciones.getDecimalBD(clsValidaciones.getDecimal(drRelacionCuatro["intBaseFare"].ToString()).ToString()).ToString();




                                                    decimal Ta_ = 0;
                                                    decimal Impuestos_ = default(decimal); ;

                                                    //----------------------


                                                    Ta_ = clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dblvalor"].ToString());

                                                    Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString()) / 100);


                                                    if (sMonedaCotiza.Equals(sMonedaCop) && dtDatos.Rows[0]["Moneda"].ToString().Equals(sMonedaUsd))
                                                    {
                                                        Ta_ = Ta_ * dValorDolar;

                                                        Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString()) / 100);


                                                    }
                                                    else
                                                    {
                                                        if (sMonedaCotiza.Equals(sMonedaUsd) && dtDatos.Rows[0]["Moneda"].ToString().Equals(sMonedaCop))
                                                        {
                                                            Ta_ = Ta_ / dValorDolar;

                                                            Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString()) / 100);
                                                            Impuestos_ = Impuestos_ / dValorDolar;

                                                        }
                                                    }
                                                    if (sMonedaCotiza.Equals(sMonedaCop))
                                                    {
                                                        Impuestos_ = clsValidaciones.getDecimalRound(Impuestos_.ToString());
                                                        Ta_ = clsValidaciones.getDecimalRound(Ta_.ToString());
                                                    }
                                                    else
                                                    {
                                                        Impuestos_ = clsValidaciones.getDecimalNotRound(Impuestos_.ToString());
                                                        Ta_ = clsValidaciones.getDecimalNotRound(Ta_.ToString());
                                                    }

                                                    decimal COPTa_ = Ta_;
                                                    decimal COPImpuestos_ = Impuestos_;

                                                    if (COPTa_ > 0)
                                                    {
                                                        string sIdTax = GetIdTaxPax(dtTaxes, drRelacionCuatro["PassengerFare_Id"].ToString());
                                                        if (!drRelacionCuatro["intBaseFare"].ToString().Equals("0"))
                                                        {
                                                            /*AGREGAMOS TA*/
                                                            DataRow drNewRowTA = dtTax.NewRow();
                                                            drNewRowTA["TaxCode"] = strTpoFee;
                                                            drNewRowTA["CurrencyCode"] = sMonedaCotiza;
                                                            drNewRowTA["Amount"] = COPTa_;
                                                            drNewRowTA["Tax_Amount_Usd"] = Ta_;
                                                            drNewRowTA["DecimalPlaces"] = 0;
                                                            drNewRowTA["Taxes_Id"] = sIdTax;
                                                            try
                                                            {
                                                                drNewRowTA["TaxName"] = "";
                                                                drNewRowTA["Tax_text"] = "";
                                                            }
                                                            catch { }
                                                            dtTax.Rows.Add(drNewRowTA);

                                                            if (COPImpuestos_ != 0)
                                                            {
                                                                /*AGREGAMOS IVA TA*/
                                                                DataRow drNewRowTAIVA = dtTax.NewRow();
                                                                drNewRowTAIVA["TaxCode"] = strTpoIvaFee;
                                                                drNewRowTAIVA["CurrencyCode"] = sMonedaCotiza;
                                                                drNewRowTAIVA["Amount"] = COPImpuestos_;
                                                                drNewRowTAIVA["Tax_Amount_Usd"] = Impuestos_;
                                                                drNewRowTAIVA["DecimalPlaces"] = 0;
                                                                drNewRowTAIVA["Taxes_Id"] = sIdTax;
                                                                try
                                                                {
                                                                    drNewRowTAIVA["TaxName"] = "";
                                                                    drNewRowTAIVA["Tax_text"] = "";
                                                                }
                                                                catch { }
                                                                dtTax.Rows.Add(drNewRowTAIVA);
                                                                dtTax.AcceptChanges();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            /*AGREGAMOS TA*/
                                                            DataRow drNewRowTA = dtTax.NewRow();
                                                            drNewRowTA["TaxCode"] = strTpoFee;
                                                            drNewRowTA["CurrencyCode"] = sMonedaCotiza;
                                                            drNewRowTA["Amount"] = 0;
                                                            drNewRowTA["DecimalPlaces"] = 0;
                                                            drNewRowTA["Taxes_Id"] = sIdTax;
                                                            try
                                                            {
                                                                drNewRowTA["TaxName"] = "";
                                                                drNewRowTA["Tax_text"] = "";
                                                            }
                                                            catch { }
                                                            dtTax.Rows.Add(drNewRowTA);

                                                            if (Impuestos_ != 0)
                                                            {
                                                                /*AGREGAMOS IVA TA*/
                                                                DataRow drNewRowTAIVA = dtTax.NewRow();
                                                                drNewRowTAIVA["TaxCode"] = strTpoIvaFee;
                                                                drNewRowTAIVA["CurrencyCode"] = sMonedaCotiza;
                                                                drNewRowTAIVA["Amount"] = 0;
                                                                drNewRowTAIVA["DecimalPlaces"] = 0;
                                                                drNewRowTAIVA["Taxes_Id"] = sIdTax;
                                                                try
                                                                {
                                                                    drNewRowTAIVA["TaxName"] = "";
                                                                    drNewRowTAIVA["Tax_text"] = "";
                                                                }
                                                                catch { }
                                                                dtTax.Rows.Add(drNewRowTAIVA);
                                                                dtTax.AcceptChanges();
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //ExceptionHandled.Publicar("FEE No encontrada datos Empresa:" + sEmpresa + " Aerolinea:00  Tipodestino:" + strTpodestino + " Tpotrayecto:" + strTrayecto + " ValorRango:" + clsValidaciones.getDecimalNotRound(drRelacionCuatro["intBaseFare"].ToString()));
                                                }
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                #region Multpledetino

                for (int i = 0; i < tblAirFilter.Rows.Count; i++)
                {
                    foreach (DataRow drItinerario in dtItin.Rows)
                    {
                        foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                        {
                            foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItineraryPricinginfo_PTC_FareInfo"))
                            {
                                foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("PTC_FareInfo_PTC_FareBreakdown"))
                                {
                                    foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                                    {
                                        try
                                        {
                                            if (drItinerario["strMarketingAirline"].ToString().Equals(tblAirFilter.Rows[i][0].ToString()))
                                            {
                                                DataTable dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFeeEmpresa", new string[6] { sEmpresa, tblAirFilter.Rows[i][0].ToString(), strTpodestino, strTrayecto, "0", "0" });
                                                if (dtDatos == null)
                                                {

                                                    //ExceptionHandled.Publicar("FEE No encontrada datos Empresa:" + sEmpresa + " Aerolinea:" + tblAirFilter.Rows[i][0].ToString() + " Tipodestino:" + strTpodestino + " Tpotrayecto:" + strTrayecto);

                                                    dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFeeEmpresa", new string[6] { sEmpresa, "00", strTpodestino, strTrayecto, clsValidaciones.getDecimalNotRound(drRelacionCuatro["intBaseFare"].ToString()).ToString(), "1" });

                                                    //if (dstDatos.Tables.Count== 0)
                                                    //    ExceptionHandled.Publicar("FEE No encontrada datos Empresa:" + sEmpresa + " Aerolinea:00  Tipodestino:" + strTpodestino + " Tpotrayecto:" + strTrayecto + " ValorRango:" + clsValidaciones.getDecimalNotRound(drRelacionCuatro["intBaseFare"].ToString()));
                                                }

                                                if (dtDatos != null)
                                                {

                                                    decimal dBaseFareUSD = 0;
                                                    string intBaseFare = "0";

                                                    dBaseFareUSD = clsValidaciones.getDecimalNotRound(drRelacionCuatro["intBaseFare"].ToString());
                                                    intBaseFare = clsValidaciones.getDecimalBD(clsValidaciones.getDecimal(drRelacionCuatro["intBaseFare"].ToString()).ToString()).ToString();




                                                    decimal Ta_ = 0;
                                                    decimal Impuestos_ = default(decimal); ;

                                                    //----------------------


                                                    Ta_ = clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dblvalor"].ToString());
                                                    Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString()) / 100);


                                                    if (sMonedaCotiza.Equals(sMonedaCop) && dtDatos.Rows[0]["Moneda"].ToString().Equals(sMonedaUsd))
                                                    {
                                                        Ta_ = Ta_ * dValorDolar;
                                                        Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString()) / 100);


                                                    }
                                                    else
                                                    {
                                                        if (sMonedaCotiza.Equals(sMonedaUsd) && dtDatos.Rows[0]["Moneda"].ToString().Equals(sMonedaCop))
                                                        {
                                                            Ta_ = Ta_ / dValorDolar;

                                                            Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString()) / 100);
                                                            Impuestos_ = Impuestos_ / dValorDolar;

                                                        }
                                                    }
                                                    if (sMonedaCotiza.Equals(sMonedaCop))
                                                    {
                                                        Impuestos_ = clsValidaciones.getDecimalRound(Impuestos_.ToString());
                                                        Ta_ = clsValidaciones.getDecimalRound(Ta_.ToString());
                                                    }
                                                    else
                                                    {
                                                        Impuestos_ = clsValidaciones.getDecimalNotRound(Impuestos_.ToString());
                                                        Ta_ = clsValidaciones.getDecimalNotRound(Ta_.ToString());
                                                    }

                                                    decimal COPTa_ = Ta_;
                                                    decimal COPImpuestos_ = Impuestos_;

                                                    if (COPTa_ > 0)
                                                    {
                                                        string sIdTax = GetIdTaxPax(dtTaxes, drRelacionCuatro["PassengerFare_Id"].ToString());
                                                        if (!drRelacionCuatro["intBaseFare"].ToString().Equals("0"))
                                                        {
                                                            /*AGREGAMOS TA*/
                                                            DataRow drNewRowTA = dtTax.NewRow();
                                                            drNewRowTA["TaxCode"] = strTpoFee;
                                                            drNewRowTA["CurrencyCode"] = sMonedaCotiza;
                                                            drNewRowTA["Amount"] = COPTa_;
                                                            drNewRowTA["Tax_Amount_Usd"] = Ta_;
                                                            drNewRowTA["DecimalPlaces"] = 0;
                                                            drNewRowTA["Taxes_Id"] = sIdTax;
                                                            try
                                                            {
                                                                drNewRowTA["TaxName"] = "";
                                                                drNewRowTA["Tax_text"] = "";
                                                            }
                                                            catch { }
                                                            dtTax.Rows.Add(drNewRowTA);

                                                            if (COPImpuestos_ != 0)
                                                            {
                                                                /*AGREGAMOS IVA TA*/
                                                                DataRow drNewRowTAIVA = dtTax.NewRow();
                                                                drNewRowTAIVA["TaxCode"] = strTpoIvaFee;
                                                                drNewRowTAIVA["CurrencyCode"] = sMonedaCotiza;
                                                                drNewRowTAIVA["Amount"] = COPImpuestos_;
                                                                drNewRowTAIVA["Tax_Amount_Usd"] = Impuestos_;
                                                                drNewRowTAIVA["DecimalPlaces"] = 0;
                                                                drNewRowTAIVA["Taxes_Id"] = sIdTax;
                                                                try
                                                                {
                                                                    drNewRowTAIVA["TaxName"] = "";
                                                                    drNewRowTAIVA["Tax_text"] = "";
                                                                }
                                                                catch { }
                                                                dtTax.Rows.Add(drNewRowTAIVA);
                                                                dtTax.AcceptChanges();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            /*AGREGAMOS TA*/
                                                            DataRow drNewRowTA = dtTax.NewRow();
                                                            drNewRowTA["TaxCode"] = strTpoFee;
                                                            drNewRowTA["CurrencyCode"] = sMonedaCotiza;
                                                            drNewRowTA["Amount"] = 0;
                                                            drNewRowTA["DecimalPlaces"] = 0;
                                                            drNewRowTA["Taxes_Id"] = sIdTax;
                                                            try
                                                            {
                                                                drNewRowTA["TaxName"] = "";
                                                                drNewRowTA["Tax_text"] = "";
                                                            }
                                                            catch { }
                                                            dtTax.Rows.Add(drNewRowTA);

                                                            if (Impuestos_ != 0)
                                                            {
                                                                /*AGREGAMOS IVA TA*/
                                                                DataRow drNewRowTAIVA = dtTax.NewRow();
                                                                drNewRowTAIVA["TaxCode"] = strTpoIvaFee;
                                                                drNewRowTAIVA["CurrencyCode"] = sMonedaCotiza;
                                                                drNewRowTAIVA["Amount"] = 0;
                                                                drNewRowTAIVA["DecimalPlaces"] = 0;
                                                                drNewRowTAIVA["Taxes_Id"] = sIdTax;
                                                                try
                                                                {
                                                                    drNewRowTAIVA["TaxName"] = "";
                                                                    drNewRowTAIVA["Tax_text"] = "";
                                                                }
                                                                catch { }
                                                                dtTax.Rows.Add(drNewRowTAIVA);
                                                                dtTax.AcceptChanges();
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //ExceptionHandled.Publicar("FEE No encontrada datos Empresa:" + sEmpresa + " Aerolinea:00  Tipodestino:" + strTpodestino + " Tpotrayecto:" + strTrayecto + " ValorRango:" + clsValidaciones.getDecimalNotRound(drRelacionCuatro["intBaseFare"].ToString()));
                                                }
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                    }

                }


                #endregion

            }


        }
        private string GetIdTaxPax(DataTable dtTaxPax, string idPax)
        {
            string sidTax = "0";
            try
            {
                string sWhere = "PassengerFare_Id = " + idPax;
                /*creamos la vista para hacer el filtro del id de la tasa*/
                DataView vistaDatosId = new DataView(dtTaxPax);
                vistaDatosId.RowFilter = sWhere;
                /*pasamos los datos filtrados a un DataTable*/
                DataTable dtTablaFiltradaId = vistaDatosId.ToTable();
                if (dtTablaFiltradaId != null && dtTablaFiltradaId.Rows.Count > 0)
                    sidTax = dtTablaFiltradaId.Rows[0]["Taxes_Id"].ToString();
            }
            catch { }
            return sidTax;
        }
        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="dsSabreAir"></param>
        /// <param name="EnumTipoTrayecto"></param>
        /// <param name="strIdioma"></param>
        /// <param name="strAplicacion"></param>
        /// <param name="EnumTipoVuelo"></param>
        private void GetTaNacional(DataSet dsSabreAir, Enum_TipoTrayecto EnumTipoTrayecto, string strIdioma, string strAplicacion, Enum_TipoVuelo EnumTipoVuelo)
        {
            try
            {
                decimal dValorDolar = Convert.ToDecimal(clsSesiones.GET_USD_SABRE());
                DataTable dtPassengerFare = dsSabreAir.Tables["PassengerFare"];
                DataTable dtTax = dsSabreAir.Tables["Tax"];
                DataTable dtPaxTax = dsSabreAir.Tables["Taxes"];
                DataTable dtItin = dsSabreAir.Tables["PricedItinerary"];

                DataView dvAirline = new DataView(dtItin);
                string[] sColsFilter = new string[1];
                sColsFilter[0] = "strMarketingAirline";
                DataTable tblAirFilter = dvAirline.ToTable(true, sColsFilter);
                string sTA = clsValidaciones.GetKeyOrAdd("TAN", "TAN");
                string sFEE = clsValidaciones.GetKeyOrAdd("TASA_ADMINISTRATIVA", "TA");
                string sIFEE = clsValidaciones.GetKeyOrAdd("IVA_TA", "ITA");
                string sMonedaCop = clsValidaciones.GetKeyOrAdd("MonedaPesoColombiano", "COP");
                string sMonedaLoc = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                string sMonedaUsd = clsValidaciones.GetKeyOrAdd("MonedaDolar", "USD");
                string sEmpresa = csReferencias.csEmpresa();
                if (clsValidaciones.GetKeyOrAdd("validavalorestaempresapadre", "true").Trim().ToUpper().Equals("TRUE"))
                {
                    sEmpresa = clsValidaciones.GetKeyOrAdd("idempresa", "6");
                }
                String sAerolinea = clsSesiones.getAerolineaValidadora();
                //bool bValidaRutas = false;
                DataSet dstDatos = new DataSet();
                bool validaresp = false;
                bool bitNacional = true;
                string strTrayecto = String.Empty;
                string strTpodestino = String.Empty;
                string strTpoTA = String.Empty;
                bool bMultidestino = false;
                switch (EnumTipoTrayecto)
                {
                    case Enum_TipoTrayecto.Ida:
                        strTrayecto = "O";
                        break;
                    case Enum_TipoTrayecto.IdaRegreso:
                        strTrayecto = "R";
                        break;
                    case Enum_TipoTrayecto.Multidestino:
                        strTrayecto = "R";
                        bMultidestino = true;
                        break;
                    default:
                        break;
                }

                if (EnumTipoVuelo.ToString().Trim().ToUpper().Equals("NACIONAL"))
                {
                    strTpoTA = clsValidaciones.GetKeyOrAdd("TAN", "TAN");
                    sIFEE = clsValidaciones.GetKeyOrAdd("IVA_TAN", "ITAN");
                }
                else
                {
                    strTpoTA = clsValidaciones.GetKeyOrAdd("TAI", "TAI");
                    sIFEE = clsValidaciones.GetKeyOrAdd("IVA_TAI", "ITAI");
                    strTrayecto = clsValidaciones.GetKeyOrAdd("TpotrayectoCombinado", "C");
                }

                strTpodestino = new CsConsultasVuelos().ConsultaCodigo(EnumTipoVuelo.ToString().Trim().ToUpper(), "TBLTPODESTINO", "INTCODE", "STRDESCRIPTION");
                strTrayecto = new CsConsultasVuelos().ConsultaCodigo(strTrayecto, "TBLTPOTRAYECTO", "INTCODE", "STRCODE");

                StringBuilder pstrSql = new StringBuilder();
                VO_OTA_AirLowFareSearchLLSRQ Vo_ParamBusqueda = clsSesiones.getParametrosAirBargain();
                List<VO_OriginDestinationInformation> lRutas = Vo_ParamBusqueda.Lvo_Rutas;
                string sMonedaCotiza = Vo_ParamBusqueda.SCodMonedaCotizacion;
                decimal Ta_ = 0;
                decimal Impuestos_ = default(decimal);
                decimal COPTa_ = Ta_;
                decimal COPImpuestos_ = Impuestos_;
                bool bCobraIva = true;

                #region Cotizacion RutaAirline
                //DataTable dtTablaFiltradaTa = null;

                if (!bMultidestino)
                {


                    for (int i = 0; i < tblAirFilter.Rows.Count; i++)
                    {
                        #region Consultas
                        //bool bTrayecto = false;
                        //bool bAerolinea = false;
                        pstrSql = new StringBuilder();

                        string sOrigen = lRutas[0].Vo_AeropuertoOrigen.SCodigo;
                        string sDestino = lRutas[0].Vo_AeropuertoDestino.SCodigo;
                        DataTable dtDatos = null;

                        if (EnumTipoVuelo.ToString().Trim().ToUpper().Equals("NACIONAL"))
                        {
                            dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConultaTAEmpresaOrigenDestino", new string[8] { sEmpresa, strTpodestino, strTrayecto, sOrigen, sDestino, tblAirFilter.Rows[i][0].ToString(), "1", "0" });
                        }


                        if (dtDatos == null && EnumTipoVuelo.ToString().Trim().ToUpper().Equals("NACIONAL"))
                        {

                            dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConultaTAEmpresa", new string[6] { sEmpresa, tblAirFilter.Rows[i][0].ToString(), strTpodestino, strTrayecto, "0", "0" });

                        }


                        //dstDatos = new csGenerales().ConsultaTabla("EXEC SPConultaTAEmpresa " + sEmpresa + "," + tblAirFilter.Rows[i][0].ToString() + "," + strTpodestino + "," + strTrayecto+","+"0,0");            

                        #endregion
                        string sPorcentaje = clsValidaciones.GetKeyOrAdd("IndicadorValorPorcentaje", "1");
                        csVuelos cVuelos = new csVuelos();
                        #region insercion tblTax
                        if (dtDatos == null)
                            validaresp = true;
                        else
                            validaresp = false;


                        #region calculo

                        Ta_ = 0;
                        Impuestos_ = default(decimal);

                        if (!validaresp)
                        {
                            Ta_ = clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dblValor"].ToString());
                            if (bCobraIva)
                                Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString())) / 100;

                            if (sMonedaCotiza.Equals(sMonedaCop) && dtDatos.Rows[0]["Moneda"].ToString().Equals(sMonedaUsd))
                            {
                                Ta_ = Ta_ * dValorDolar;
                                if (bCobraIva)
                                {
                                    Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString())) / 100;
                                }
                            }
                            else
                            {
                                if (sMonedaCotiza.Equals(sMonedaUsd) && dtDatos.Rows[0]["Moneda"].ToString().Equals(sMonedaCop))
                                {
                                    Ta_ = Ta_ / dValorDolar;
                                    if (bCobraIva)
                                    {
                                        Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString())) / 100;

                                    }
                                }
                            }
                        }
                        else
                        {
                            //ExceptionHandled.Publicar("TA No encontrada datos Empresa:" + sEmpresa + " Aerolinea:" + tblAirFilter.Rows[i][0].ToString() + " Tipodestino:" + strTpodestino + " Tpotrayecto:" + strTrayecto);
                            Ta_ = 0;
                            Impuestos_ = 0;
                        }

                        if (sMonedaCotiza.Equals(sMonedaCop))
                        {
                            Impuestos_ = clsValidaciones.getDecimalRound(Impuestos_.ToString());
                            Ta_ = clsValidaciones.getDecimalRound(Ta_.ToString());
                        }
                        else
                        {
                            Impuestos_ = clsValidaciones.getDecimalNotRound(Impuestos_.ToString());
                            Ta_ = clsValidaciones.getDecimalNotRound(Ta_.ToString());
                        }


                        #endregion

                        COPTa_ = Ta_;
                        COPImpuestos_ = Impuestos_;

                        #region Aerolinea
                        //----------------------------- VALIDACION DE INSERCION DE LAS TARIFAS PERO EN LA AEROLINEA CORRESPONDIENTE
                        foreach (DataRow drItinerario in dtItin.Rows)
                        {
                            foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                            {
                                foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItineraryPricingInfo_PTC_FareBreakdowns"))
                                {
                                    foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("PTC_FareBreakdowns_PTC_FareBreakdown"))
                                    {
                                        foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                                        {
                                            if (drItinerario["strMarketingAirline"].ToString().Equals(tblAirFilter.Rows[i][0].ToString()))
                                            {

                                                #region calculo

                                                decimal dBaseFareUSD = clsValidaciones.getDecimal(drRelacionCuatro["intBaseFare"].ToString());
                                                string intBaseFareUSD = clsValidaciones.getDecimalBD(clsValidaciones.getDecimal(drRelacionCuatro["intBaseFare"].ToString()).ToString()).ToString();

                                                if (!EnumTipoVuelo.ToString().Trim().ToUpper().Equals("NACIONAL"))
                                                {
                                                    bitNacional = false;
                                                    dBaseFareUSD = clsValidaciones.getDecimal(drRelacionCuatro["intBaseFareUSD"].ToString());
                                                    dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConultaTAEmpresaOrigenDestino", new string[8] { sEmpresa, strTpodestino, strTrayecto, sOrigen, sDestino, tblAirFilter.Rows[i][0].ToString(), "5", dBaseFareUSD.ToString() });

                                                    if (dtDatos == null)
                                                    {
                                                        dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConultaTAEmpresaOrigenDestino", new string[8] { sEmpresa, strTpodestino, strTrayecto, sOrigen, sDestino, tblAirFilter.Rows[i][0].ToString(), "2", dBaseFareUSD.ToString() });

                                                        if (dtDatos == null)
                                                        {
                                                            dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConultaTAEmpresaOrigenDestino", new string[8] { sEmpresa, strTpodestino, strTrayecto, sOrigen, sDestino, tblAirFilter.Rows[i][0].ToString(), "3", dBaseFareUSD.ToString() });
                                                        }
                                                    }

                                                }



                                                if (validaresp)
                                                {
                                                    //dstDatos = new csGenerales().ConsultaTabla("EXEC SPConultaTAEmpresa " + sEmpresa + "," + tblAirFilter.Rows[i][0].ToString() + "," + strTpodestino + "," + strTrayecto + ",1," + dBaseFareUSD);
                                                    if (bitNacional)
                                                    {
                                                        dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConultaTAEmpresa", new string[6] { sEmpresa, tblAirFilter.Rows[i][0].ToString(), strTpodestino, strTrayecto, "1", dBaseFareUSD.ToString() });
                                                    }
                                                    if (dtDatos != null)
                                                    {
                                                        Ta_ = clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dblValor"].ToString());
                                                        if (bCobraIva)
                                                            Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString())) / 100;

                                                        if (sMonedaCotiza.Equals(sMonedaCop) && dtDatos.Rows[0]["Moneda"].ToString().Equals(sMonedaUsd))
                                                        {
                                                            Ta_ = Ta_ * dValorDolar;
                                                            if (bCobraIva)
                                                            {
                                                                Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString())) / 100;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (sMonedaCotiza.Equals(sMonedaUsd) && dtDatos.Rows[0]["Moneda"].ToString().Equals(sMonedaCop))
                                                            {
                                                                Ta_ = Ta_ / dValorDolar;
                                                                if (bCobraIva)
                                                                {
                                                                    Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString())) / 100;
                                                                }
                                                            }
                                                        }
                                                        if (sMonedaCotiza.Equals(sMonedaCop))
                                                        {
                                                            Impuestos_ = clsValidaciones.getDecimalRound(Impuestos_.ToString());
                                                            Ta_ = clsValidaciones.getDecimalRound(Ta_.ToString());
                                                        }
                                                        else
                                                        {
                                                            Impuestos_ = clsValidaciones.getDecimalNotRound(Impuestos_.ToString());
                                                            Ta_ = clsValidaciones.getDecimalNotRound(Ta_.ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //ExceptionHandled.Publicar("TA No encontrada datos Empresa:" + sEmpresa + " Aerolinea:" + tblAirFilter.Rows[i][0].ToString() + " Tipodestino:" + strTpodestino + " Tpotrayecto:" + strTrayecto);
                                                        Ta_ = 0;
                                                        Impuestos_ = 0;

                                                    }
                                                    COPTa_ = Ta_;
                                                    COPImpuestos_ = Impuestos_;




                                                }
                                                #endregion


                                                string sIdTax = GetIdTaxPax(dtPaxTax, drRelacionCuatro["PassengerFare_Id"].ToString());
                                                if (!drRelacionCuatro["intBaseFare"].ToString().Equals("0"))
                                                {
                                                    /*AGREGAMOS TA*/
                                                    DataRow drNewRowTA = dtTax.NewRow();
                                                    drNewRowTA["TaxCode"] = strTpoTA;
                                                    drNewRowTA["CurrencyCode"] = sMonedaCotiza;
                                                    drNewRowTA["Amount"] = COPTa_;
                                                    drNewRowTA["Tax_Amount_Usd"] = Ta_;
                                                    drNewRowTA["DecimalPlaces"] = 0;
                                                    drNewRowTA["Taxes_Id"] = sIdTax;
                                                    try
                                                    {
                                                        drNewRowTA["TaxName"] = "";
                                                        drNewRowTA["Tax_text"] = "";
                                                    }
                                                    catch { }
                                                    dtTax.Rows.Add(drNewRowTA);

                                                    /*AGREGAMOS IVA TA*/
                                                    DataRow drNewRowTAIVA = dtTax.NewRow();
                                                    drNewRowTAIVA["TaxCode"] = sIFEE;
                                                    drNewRowTAIVA["CurrencyCode"] = sMonedaCotiza;
                                                    drNewRowTAIVA["Amount"] = COPImpuestos_;
                                                    drNewRowTAIVA["Tax_Amount_Usd"] = Impuestos_;
                                                    drNewRowTAIVA["DecimalPlaces"] = 0;
                                                    drNewRowTAIVA["Taxes_Id"] = sIdTax;
                                                    try
                                                    {
                                                        drNewRowTAIVA["TaxName"] = "";
                                                        drNewRowTAIVA["Tax_text"] = "";
                                                    }
                                                    catch { }
                                                    dtTax.Rows.Add(drNewRowTAIVA);
                                                    dtTax.AcceptChanges();
                                                }
                                                else
                                                {
                                                    /*AGREGAMOS TA*/
                                                    DataRow drNewRowTA = dtTax.NewRow();
                                                    drNewRowTA["TaxCode"] = strTpoTA;
                                                    drNewRowTA["CurrencyCode"] = sMonedaCotiza;
                                                    drNewRowTA["Amount"] = 0;
                                                    drNewRowTA["DecimalPlaces"] = 0;
                                                    drNewRowTA["Taxes_Id"] = sIdTax;
                                                    try
                                                    {
                                                        drNewRowTA["TaxName"] = "";
                                                        drNewRowTA["Tax_text"] = "";
                                                    }
                                                    catch { }
                                                    dtTax.Rows.Add(drNewRowTA);

                                                    /*AGREGAMOS IVA TA*/
                                                    DataRow drNewRowTAIVA = dtTax.NewRow();
                                                    drNewRowTAIVA["TaxCode"] = sIFEE;
                                                    drNewRowTAIVA["CurrencyCode"] = sMonedaCotiza;
                                                    drNewRowTAIVA["Amount"] = 0;
                                                    drNewRowTAIVA["DecimalPlaces"] = 0;
                                                    drNewRowTAIVA["Taxes_Id"] = sIdTax;
                                                    try
                                                    {
                                                        drNewRowTAIVA["TaxName"] = "";
                                                        drNewRowTAIVA["Tax_text"] = "";
                                                    }
                                                    catch { }
                                                    dtTax.Rows.Add(drNewRowTAIVA);
                                                    dtTax.AcceptChanges();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        #endregion
                    }
                }
                else
                {
                    #region Multpledetino
                    for (int i = 0; i < tblAirFilter.Rows.Count; i++)
                    {
                        #region Consultas
                        //bool bTrayecto = false;
                        //bool bAerolinea = false;
                        pstrSql = new StringBuilder();

                        //dstDatos = new csGenerales().ConsultaTabla("EXEC SPConultaTAEmpresa " + sEmpresa + "," + tblAirFilter.Rows[i][0].ToString() + "," + strTpodestino + "," + strTrayecto+","+"0,0");            
                        DataTable dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConultaTAEmpresa", new string[6] { sEmpresa, tblAirFilter.Rows[i][0].ToString(), strTpodestino, strTrayecto, "0", "0" });
                        #endregion

                        string sPorcentaje = clsValidaciones.GetKeyOrAdd("IndicadorValorPorcentaje", "1");
                        csVuelos cVuelos = new csVuelos();


                        #region insercion tblTax
                        if (dtDatos == null)
                            validaresp = true;
                        else
                            validaresp = false;


                        #region calculo

                        Ta_ = 0;
                        Impuestos_ = default(decimal);

                        if (!validaresp)
                        {
                            Ta_ = clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dblValor"].ToString());
                            if (bCobraIva)
                                Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString())) / 100;

                            if (sMonedaCotiza.Equals(sMonedaCop) && dtDatos.Rows[0]["Moneda"].ToString().Equals(sMonedaUsd))
                            {
                                Ta_ = Ta_ * dValorDolar;
                                if (bCobraIva)
                                {
                                    Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString())) / 100;
                                }
                            }
                            else
                            {
                                if (sMonedaCotiza.Equals(sMonedaUsd) && dtDatos.Rows[0]["Moneda"].ToString().Equals(sMonedaCop))
                                {
                                    Ta_ = Ta_ / dValorDolar;
                                    if (bCobraIva)
                                    {
                                        Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString())) / 100;

                                    }
                                }
                            }
                        }
                        else
                        {
                            //ExceptionHandled.Publicar("TA No encontrada datos Empresa:" + sEmpresa + " Aerolinea:" + tblAirFilter.Rows[i][0].ToString() + " Tipodestino:" + strTpodestino + " Tpotrayecto:" + strTrayecto);
                            Ta_ = 0;
                            Impuestos_ = 0;
                        }

                        if (sMonedaCotiza.Equals(sMonedaCop))
                        {
                            Impuestos_ = clsValidaciones.getDecimalRound(Impuestos_.ToString());
                            Ta_ = clsValidaciones.getDecimalRound(Ta_.ToString());
                        }
                        else
                        {
                            Impuestos_ = clsValidaciones.getDecimalNotRound(Impuestos_.ToString());
                            Ta_ = clsValidaciones.getDecimalNotRound(Ta_.ToString());
                        }


                        #endregion

                        COPTa_ = Ta_;
                        COPImpuestos_ = Impuestos_;

                        #region Aerolinea
                        //----------------------------- VALIDACION DE INSERCION DE LAS TARIFAS PERO EN LA AEROLINEA CORRESPONDIENTE
                        foreach (DataRow drItinerario in dtItin.Rows)
                        {
                            foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("PricedItinerary_AirItineraryPricingInfo"))
                            {
                                foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("AirItineraryPricinginfo_PTC_FareInfo"))
                                {
                                    foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("PTC_FareInfo_PTC_FareBreakdown"))
                                    {
                                        foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                                        {
                                            if (drItinerario["strMarketingAirline"].ToString().Equals(tblAirFilter.Rows[i][0].ToString()))
                                            {

                                                #region calculo

                                                decimal dBaseFareUSD = clsValidaciones.getDecimal(drRelacionCuatro["intBaseFare"].ToString());
                                                string intBaseFareUSD = clsValidaciones.getDecimalBD(clsValidaciones.getDecimal(drRelacionCuatro["intBaseFare"].ToString()).ToString()).ToString();
                                                if (validaresp)
                                                {
                                                    //dstDatos = new csGenerales().ConsultaTabla("EXEC SPConultaTAEmpresa " + sEmpresa + "," + tblAirFilter.Rows[i][0].ToString() + "," + strTpodestino + "," + strTrayecto + ",1," + dBaseFareUSD);
                                                    dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConultaTAEmpresaOrigenDestino", new string[8] { sEmpresa, strTpodestino, strTrayecto, "Ning", "Ning", tblAirFilter.Rows[i][0].ToString(), "4", dBaseFareUSD.ToString() });
                                                    //dtDatos = new CsConsultasVuelos().SPConsultaTabla("SPConultaTAEmpresa", new string[6] { sEmpresa, tblAirFilter.Rows[i][0].ToString(), strTpodestino, strTrayecto, "1", dBaseFareUSD.ToString() });
                                                    if (dtDatos != null)
                                                    {
                                                        Ta_ = clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dblValor"].ToString());
                                                        if (bCobraIva)
                                                            Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString())) / 100;

                                                        if (sMonedaCotiza.Equals(sMonedaCop) && dtDatos.Rows[0]["Moneda"].ToString().Equals(sMonedaUsd))
                                                        {
                                                            Ta_ = Ta_ * dValorDolar;
                                                            if (bCobraIva)
                                                            {
                                                                Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString())) / 100;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (sMonedaCotiza.Equals(sMonedaUsd) && dtDatos.Rows[0]["Moneda"].ToString().Equals(sMonedaCop))
                                                            {
                                                                Ta_ = Ta_ / dValorDolar;
                                                                if (bCobraIva)
                                                                {
                                                                    Impuestos_ = (Ta_ * clsValidaciones.getDecimalNotRound(dtDatos.Rows[0]["dbliva"].ToString())) / 100;
                                                                }
                                                            }
                                                        }
                                                        if (sMonedaCotiza.Equals(sMonedaCop))
                                                        {
                                                            Impuestos_ = clsValidaciones.getDecimalRound(Impuestos_.ToString());
                                                            Ta_ = clsValidaciones.getDecimalRound(Ta_.ToString());
                                                        }
                                                        else
                                                        {
                                                            Impuestos_ = clsValidaciones.getDecimalNotRound(Impuestos_.ToString());
                                                            Ta_ = clsValidaciones.getDecimalNotRound(Ta_.ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //ExceptionHandled.Publicar("TA No encontrada datos Empresa:" + sEmpresa + " Aerolinea:" + tblAirFilter.Rows[i][0].ToString() + " Tipodestino:" + strTpodestino + " Tpotrayecto:" + strTrayecto);
                                                        Ta_ = 0;
                                                        Impuestos_ = 0;

                                                    }
                                                    COPTa_ = Ta_;
                                                    COPImpuestos_ = Impuestos_;




                                                }
                                                #endregion


                                                string sIdTax = GetIdTaxPax(dtPaxTax, drRelacionCuatro["PassengerFare_Id"].ToString());
                                                if (!drRelacionCuatro["intBaseFare"].ToString().Equals("0"))
                                                {
                                                    /*AGREGAMOS TA*/
                                                    DataRow drNewRowTA = dtTax.NewRow();
                                                    drNewRowTA["TaxCode"] = strTpoTA;
                                                    drNewRowTA["CurrencyCode"] = sMonedaCotiza;
                                                    drNewRowTA["Amount"] = COPTa_;
                                                    drNewRowTA["Tax_Amount_Usd"] = Ta_;
                                                    drNewRowTA["DecimalPlaces"] = 0;
                                                    drNewRowTA["Taxes_Id"] = sIdTax;
                                                    try
                                                    {
                                                        drNewRowTA["TaxName"] = "";
                                                        drNewRowTA["Tax_text"] = "";
                                                    }
                                                    catch { }
                                                    dtTax.Rows.Add(drNewRowTA);

                                                    /*AGREGAMOS IVA TA*/
                                                    DataRow drNewRowTAIVA = dtTax.NewRow();
                                                    drNewRowTAIVA["TaxCode"] = sIFEE;
                                                    drNewRowTAIVA["CurrencyCode"] = sMonedaCotiza;
                                                    drNewRowTAIVA["Amount"] = COPImpuestos_;
                                                    drNewRowTAIVA["Tax_Amount_Usd"] = Impuestos_;
                                                    drNewRowTAIVA["DecimalPlaces"] = 0;
                                                    drNewRowTAIVA["Taxes_Id"] = sIdTax;
                                                    try
                                                    {
                                                        drNewRowTAIVA["TaxName"] = "";
                                                        drNewRowTAIVA["Tax_text"] = "";
                                                    }
                                                    catch { }
                                                    dtTax.Rows.Add(drNewRowTAIVA);
                                                    dtTax.AcceptChanges();
                                                }
                                                else
                                                {
                                                    /*AGREGAMOS TA*/
                                                    DataRow drNewRowTA = dtTax.NewRow();
                                                    drNewRowTA["TaxCode"] = strTpoTA;
                                                    drNewRowTA["CurrencyCode"] = sMonedaCotiza;
                                                    drNewRowTA["Amount"] = 0;
                                                    drNewRowTA["DecimalPlaces"] = 0;
                                                    drNewRowTA["Taxes_Id"] = sIdTax;
                                                    try
                                                    {
                                                        drNewRowTA["TaxName"] = "";
                                                        drNewRowTA["Tax_text"] = "";
                                                    }
                                                    catch { }
                                                    dtTax.Rows.Add(drNewRowTA);

                                                    /*AGREGAMOS IVA TA*/
                                                    DataRow drNewRowTAIVA = dtTax.NewRow();
                                                    drNewRowTAIVA["TaxCode"] = sIFEE;
                                                    drNewRowTAIVA["CurrencyCode"] = sMonedaCotiza;
                                                    drNewRowTAIVA["Amount"] = 0;
                                                    drNewRowTAIVA["DecimalPlaces"] = 0;
                                                    drNewRowTAIVA["Taxes_Id"] = sIdTax;
                                                    try
                                                    {
                                                        drNewRowTAIVA["TaxName"] = "";
                                                        drNewRowTAIVA["Tax_text"] = "";
                                                    }
                                                    catch { }
                                                    dtTax.Rows.Add(drNewRowTAIVA);
                                                    dtTax.AcceptChanges();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        #endregion

                    }
                    #endregion
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "GetTaInternacional";
                cParametros.ViewMessage.Add("Ocurrio un error calculando la TA");
                cParametros.Sugerencia.Add("Ocurrio un error calculando la TA");
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void GetCrearDatasetSelectSabre(int iPos, string sRPH)
        {
            try
            {
                DataSet dsSabreAir = GetDsSabreAir();
                DataSet dsData = new DataSet();

                DataColumn dcintSelect = new DataColumn("intSelect", typeof(int));
                DataColumn dcstrTipoSeleccion = new DataColumn("strTipoSeleccion", typeof(string));
                DataColumn dcSequenceNumber = new DataColumn("SequenceNumber", typeof(string));
                DataColumn dcbolUltimo = new DataColumn("bolUltimo", typeof(bool));

                DataColumn dcintSelect2 = new DataColumn("intSelect", typeof(int));
                DataColumn dcstrTipoSeleccion2 = new DataColumn("strTipoSeleccion", typeof(string));

                DataTable tblTable = new DataTable();
                DataTable tblTableSelect = new DataTable();
                tblTable.TableName = "tblSelect";

                if (iPos.Equals(0))
                {
                    tblTable.Columns.Add(dcintSelect);
                    tblTable.Columns.Add(dcstrTipoSeleccion);
                    tblTable.Columns.Add(dcSequenceNumber);
                    tblTable.Columns.Add(dcbolUltimo);

                    tblTable.Rows.Clear();

                    DataRow drFila = tblTable.NewRow();

                    tblTable.Rows.Add(drFila);
                    tblTable.AcceptChanges();

                    tblTable.Rows[tblTable.Rows.Count - 1]["intSelect"] = iPos;
                    tblTable.Rows[tblTable.Rows.Count - 1]["strTipoSeleccion"] = "Seleccione el itinerario de Ida";
                    tblTable.Rows[tblTable.Rows.Count - 1]["bolUltimo"] = false;

                    dsData.Tables.Add(tblTable);
                }
                else
                {
                    if (iPos.Equals(1))
                    {
                        tblTable.Columns.Add(dcintSelect);
                        tblTable.Columns.Add(dcstrTipoSeleccion);
                        tblTable.Columns.Add(dcSequenceNumber);
                        tblTable.Columns.Add(dcbolUltimo);

                        tblTable.Rows.Clear();

                        DataRow drFila = tblTable.NewRow();

                        tblTable.Rows.Add(drFila);
                        tblTable.AcceptChanges();

                        tblTable.Rows[tblTable.Rows.Count - 1]["intSelect"] = iPos;
                        tblTable.Rows[tblTable.Rows.Count - 1]["strTipoSeleccion"] = "Itinerario de Ida";
                        tblTable.Rows[tblTable.Rows.Count - 1]["bolUltimo"] = false;

                        tblTableSelect = ClonarTabla(dsSabreAir, "FlightSegment");
                        tblTableSelect.Clear();
                        tblTableSelect.TableName = "FlightSegment";
                        tblTableSelect.Columns.Add(dcintSelect2);
                        tblTableSelect.Columns.Add(dcstrTipoSeleccion2);

                        GetDtGetItinerarioHora(sRPH, dsSabreAir, tblTableSelect);

                        tblTableSelect.AcceptChanges();
                        for (int i = 0; i < tblTableSelect.Rows.Count; i++)
                        {
                            tblTableSelect.Rows[i]["intSelect"] = iPos;
                        }
                        tblTableSelect.Rows[tblTableSelect.Rows.Count - 1]["strTipoSeleccion"] = "Seleccione el itinerario de Regreso";
                    }
                    else
                    {
                        DataSet dsDataSelect = clsSesiones.GetDatasetSelectSabreAir();
                        DataTable tblTable1 = dsDataSelect.Tables["tblSelect"];
                        DataTable tblTableSelect1 = dsDataSelect.Tables["FlightSegment"];

                        tblTable1.Rows.Clear();

                        DataRow drFila = tblTable1.NewRow();

                        tblTable1.Rows.Add(drFila);
                        tblTable1.AcceptChanges();

                        tblTable1.Rows[tblTable1.Rows.Count - 1]["intSelect"] = iPos;
                        tblTable1.Rows[tblTable1.Rows.Count - 1]["strTipoSeleccion"] = "Itinerario de Ida";
                        tblTable1.Rows[tblTable1.Rows.Count - 1]["SequenceNumber"] = "RPH";
                        tblTable1.Rows[tblTable1.Rows.Count - 1]["bolUltimo"] = true;

                        GetDtGetItinerarioHora(sRPH, dsSabreAir, tblTableSelect1);

                        tblTableSelect1.AcceptChanges();

                        for (int i = 0; i < tblTableSelect1.Rows.Count; i++)
                        {
                            if (tblTableSelect1.Rows[i]["intSelect"].ToString().Length.Equals(0))
                                tblTableSelect1.Rows[i]["intSelect"] = iPos;
                            if (!tblTableSelect1.Rows[i]["strTipoSeleccion"].ToString().Length.Equals(0))
                                tblTableSelect1.Rows[i]["strTipoSeleccion"] = "Itinerario de Regreso";

                        }
                        tblTable = tblTable1.Copy();
                        tblTableSelect = tblTableSelect1.Copy();
                    }
                    try
                    {
                        VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                        if (vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida.Count.Equals(0))
                        {
                            List<string> lAerolineaPreferida = new List<string>();
                            lAerolineaPreferida.Add(tblTableSelect.Rows[tblTableSelect.Rows.Count - 1]["strMarketingAirline"].ToString());

                            vo_OTA_AirLowFareSearchLLSRQ.LsAerolineaPreferida = lAerolineaPreferida;
                            clsSesiones.setParametrosAirBargain(vo_OTA_AirLowFareSearchLLSRQ);
                        }
                        GetAirBook(tblTableSelect, iPos);
                    }
                    catch { }
                    dsData.Tables.Add(tblTable);
                    dsData.Tables.Add(tblTableSelect);
                }
                clsSesiones.SetDatasetSelectSabreAir(dsData);
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        public void GetCrearDatasetSelectSabrePlanes(string sRPH)
        {
            try
            {
                DataSet dsSabreAir = GetDsSabreAir();
                clsSesiones.SetDatasetSelectSabreAir(dsSabreAir);
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        private void GetAirBook(DataTable dtItinerario, int iItinerario)
        {
            try
            {
                VO_OTA_AirBookRQ vo_OTA_AirBookRQ = new VO_OTA_AirBookRQ();
                VO_OrigenDestinationOption vo_OrigenDestinationOption = new VO_OrigenDestinationOption();
                List<VO_OrigenDestinationOption> lvo_OrigenDestinationOption = new List<VO_OrigenDestinationOption>();
                List<VO_AirItinerary> lvo_AirItinerary = new List<VO_AirItinerary>();
                string strTipoPlan = clsValidaciones.GetKeyOrAdd("Aereo_WSTame", "AIR_TAME");

                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                int iCantidad = 0;
                foreach (VO_Pasajero Pasajero in vo_OTA_AirLowFareSearchLLSRQ.Lvo_Pasajeros)
                {
                    if (Pasajero.SCodigo != "INF")
                    {
                        iCantidad += int.Parse(Pasajero.SCantidad);
                    }
                }

                if (clsSesiones.getParametrosAirHoras() != null)
                {
                    vo_OTA_AirBookRQ = clsSesiones.getParametrosAirHoras();
                    lvo_OrigenDestinationOption = vo_OTA_AirBookRQ.Lvo_OrigenDestinationOption;
                }

                vo_OTA_AirBookRQ.IRutaActual = iItinerario;
                try
                {
                    if (dtItinerario.Rows.Count > 0)
                        vo_OTA_AirBookRQ.TipoWs = dtItinerario.Rows[0][COLUMN_WEBSERVICES].ToString();
                }
                catch { }
                DataTable dtSegmento = new DataTable();
                dtSegmento = GetDtSegmento(iItinerario, dtItinerario);
                int iCount = dtSegmento.Rows.Count;
                if (iCount > 0)
                {
                    for (int i = 0; i < iCount; i++)
                    {
                        VO_AirItinerary vo_AirItinerary = new VO_AirItinerary();
                        VO_Aeropuerto vo_AeropuertoOrigen = new VO_Aeropuerto();
                        VO_Aeropuerto vo_AeropuertoDestino = new VO_Aeropuerto();

                        vo_AeropuertoOrigen.SContexto = "IATA";
                        vo_AeropuertoDestino.SContexto = "IATA";
                        vo_AeropuertoOrigen.SCodigo = dtSegmento.Rows[i]["strDepartureAirport"].ToString();
                        vo_AeropuertoDestino.SCodigo = dtSegmento.Rows[i]["strArrivalAirport"].ToString();

                        vo_AirItinerary.BAirBook = true;
                        vo_AirItinerary.SActionCode = "NN";
                        vo_AirItinerary.SAirEquip = dtSegmento.Rows[i]["strEquipment"].ToString();
                        vo_AirItinerary.SClase = dtSegmento.Rows[i]["strMarketingCabin"].ToString();
                        vo_AirItinerary.SFechaLlegada = RetornaFecha(dtSegmento.Rows[i]["dtmFechaLlegada"].ToString());
                        vo_AirItinerary.SFechaSalida = RetornaFecha(dtSegmento.Rows[i]["dtmFechaSalida"].ToString());
                        vo_AirItinerary.SMarketingAirLine = dtSegmento.Rows[i]["strMarketingAirline"].ToString();
                        vo_AirItinerary.SNroPassenger = iCantidad.ToString();
                        vo_AirItinerary.SNroVuelo = dtSegmento.Rows[i]["FlightNumber"].ToString();
                        vo_AirItinerary.SOperatingAirLine = dtSegmento.Rows[i]["strOperatingAirline"].ToString();

                        vo_AirItinerary.Vo_AeropuertoOrigen = vo_AeropuertoOrigen;
                        vo_AirItinerary.Vo_AeropuertoDestino = vo_AeropuertoDestino;

                        lvo_AirItinerary.Add(vo_AirItinerary);
                    }
                    vo_OrigenDestinationOption.IItinerary = iItinerario;
                    vo_OrigenDestinationOption.Lvo_AirItinerary = lvo_AirItinerary;

                    lvo_OrigenDestinationOption.Add(vo_OrigenDestinationOption);
                    vo_OTA_AirBookRQ.Lvo_OrigenDestinationOption = lvo_OrigenDestinationOption;

                    clsSesiones.setParametrosAirHoras(vo_OTA_AirBookRQ);
                }
            }
            catch { }
        }
        private string RetornaFecha(string sFecha)
        {
            string sFechaRetorno = sFecha;
            try
            {
                string[] sFechaLista = clsValidaciones.Lista(sFecha, " ");
                sFechaRetorno = clsValidaciones.ConverFecha(sFechaLista[0], Enum_FormatoFecha.YMD, "-");
                if (sFechaLista.Length.Equals(1))
                {
                    sFechaRetorno += "T0:00";
                }
                else
                {
                    sFechaRetorno += "T" + sFechaLista[1];
                }
            }
            catch { }
            return sFechaRetorno;
        }
        #endregion

        #region [RESULTADOS]
        /*METODOS PARA LA PAGINA DE RESULTADOS DE VUELOS*/
        public void InicializarPagina(UserControl ucResultados, DataSet dsSabreAir)
        {
            try
            {
                DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
                string sValidaParadas = clsValidaciones.GetKeyOrAdd("bValidaParadas", "False");
                if (sValidaParadas.ToUpper().Equals("TRUE"))
                {
                    VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                    if (vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas.Equals("0"))
                    {
                        string sWhere = "StopQuantity = '0'";
                        dtPricedItinerary = clsDataNet.dsDataWhere(sWhere, dtPricedItinerary);
                        ActualizaConsecutivo(dtPricedItinerary);
                    }
                }
                DataView dtvPricedItinerary = new DataView(dtPricedItinerary);
                try
                {
                    dtvPricedItinerary.Sort = "Consecutivo";
                    dtPricedItinerary = dtvPricedItinerary.ToTable();
                    dtPricedItinerary.AcceptChanges();
                }
                catch
                { }

                Repeater rptItinerario = ucResultados.FindControl("rptItinerario") as Repeater;
                rptItinerario.DataSource = dtPricedItinerary;
                rptItinerario.DataBind();
                LlenarFiltro(ucResultados, dsSabreAir);
                CargarSegmentos(rptItinerario);
            }
            catch { }
        }
        /// <summary>
        /// Metodo que muestra los resultados de vuelos utilizando el Bargain Finder Max
        /// aplicamos filtro por defualt SequenceNumber
        /// </summary>
        /// <param name="ucResultados"></param>
        /// <param name="dsSabreAir"></param>
        public void InicializarPaginaBFM(UserControl ucResultados, DataSet dsSabreAir, string sOrder = "SequenceNumber ASC", string sFilter = "0")
        {
            try
            {
                DataTable dtPricedItineraryFilter = dsSabreAir.Tables["PricedItinerary"];

                dtPricedItineraryFilter.DefaultView.Sort = sOrder;
                DataTable dtPricedItinerary = dtPricedItineraryFilter.DefaultView.ToTable();

                if (sFilter != ("0"))
                {
                    dtPricedItinerary = clsDataNet.dsDataWhere(sFilter, dtPricedItinerary);

                }




                string sValidaParadas = clsValidaciones.GetKeyOrAdd("bValidaParadas", "False");
                if (sValidaParadas.ToUpper().Equals("TRUE"))
                {
                    VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                    if (vo_OTA_AirLowFareSearchLLSRQ.SMaximasParadas.Equals("0"))
                    {
                        string sWhere = "StopQuantity = '0'";
                        dtPricedItinerary = clsDataNet.dsDataWhere(sWhere, dtPricedItinerary);
                        ActualizaConsecutivo(dtPricedItinerary);
                    }
                }
                DataView dtvPricedItinerary = new DataView(dtPricedItinerary);
                //try
                //{
                //    dtvPricedItinerary.Sort = "Consecutivo";
                //    dtPricedItinerary = dtvPricedItinerary.ToTable();
                //    dtPricedItinerary.AcceptChanges();
                //}
                //catch
                //{ }

                DataTable dtItinFiltrado = FiltrarSegmentosItinBFM(dtPricedItinerary);
                Repeater rptItinerario = ucResultados.FindControl("rptItinerarioBFM") as Repeater;
                rptItinerario.DataSource = dtItinFiltrado;
                rptItinerario.DataBind();


                ///jvargas validacion de vuelos sin itinerario
                ///
                DataTable tblSegmentosTotal = null;
                DataTable dtClone = null;
                DataTable dtItinerarios = null;
                if (dtItinFiltrado.Rows.Count > 0)
                {
                    try
                    {
                        dtItinerarios = dtItinFiltrado;
                        dtClone = dtItinFiltrado.Clone();
                    }
                    catch { }

                    for (int c = 0; c < rptItinerario.Items.Count; c++)
                    {
                        try
                        {
                            tblSegmentosTotal = null;
                            Label lblTotalOriginal = (Label)rptItinerario.Items[c].FindControl("lblTotalOriginal");
                            Label lblAerolinea = (Label)rptItinerario.Items[c].FindControl("lblAerolinea");

                            if (lblTotalOriginal != null && lblAerolinea != null)
                            {
                                DataTable tblItinFiltrados = FiltrarCodigosItinBFM(dtPricedItinerary, lblTotalOriginal.Text, lblAerolinea.Text);

                                //DataTable tblSegmentosTotal = null;
                                for (int d = 0; d < tblItinFiltrados.Rows.Count; d++)
                                {
                                    if (tblSegmentosTotal == null)
                                        tblSegmentosTotal = GetDtFlightSegmento(tblItinFiltrados.Rows[d]["PricedItinerary_Id"].ToString());
                                    else
                                        tblSegmentosTotal.Merge(GetDtFlightSegmento(tblItinFiltrados.Rows[d]["PricedItinerary_Id"].ToString()));
                                }

                                List<string> Vuelos = new List<string>();
                                DataTable dtSegm = FiltrarSegmentosTrayectoBFM(tblSegmentosTotal, "I");

                                DataTable dtSegmReg = FiltrarSegmentosTrayectoBFM(tblSegmentosTotal, "R");

                                var query = (from dt in dtSegm.AsEnumerable()
                                             select new
                                             {
                                                 Code = dt.Field<string>("FlightNumber")
                                             }
                                            ).ToList().Distinct();

                                for (int li = 0; li < query.ToList().Count; li++)
                                {
                                    dtClone.Rows.Add(dtItinerarios.Rows[c].ItemArray);
                                }
                            }
                        }
                        catch { }
                    }
                }
              

                if (dtClone.Rows.Count > 0)
                {
                    dtItinFiltrado = dtClone;

                    try
                    {
                        dtItinFiltrado.DefaultView.Sort = sOrder;
                    }
                    catch
                    {
                        dtItinFiltrado.DefaultView.Sort = "IntTotalPesos ASC";
                    }
                    dtItinFiltrado = dtItinFiltrado.DefaultView.ToTable();
                    rptItinerario.DataSource = dtItinFiltrado;
                    rptItinerario.DataBind();
                }


                //


                //devemos de verificar el filtro en el header
                Stopwatch stopWatch = new Stopwatch();
                TimeSpan ts;
                stopWatch.Start();
                //LlenarFiltro(ucResultados, dsSabreAir);
                stopWatch.Stop();

                ts = stopWatch.Elapsed;
                stopWatch.Start();

                CargarSegmentosBFM(rptItinerario, dtPricedItinerary);
                ts = stopWatch.Elapsed;

            }
            catch { }
        }
        /// <summary>
        /// Metodo que filtra los itinerarios de las diferentes aerolineas agrupadas por precio
        /// </summary>
        /// <param name="tblItinerarios"></param>
        /// <returns></returns>
        private DataTable FiltrarSegmentosItinBFM(DataTable tblItinerarios)
        {
            DataView view = new DataView(tblItinerarios);
            view.Sort = "IntTotalPesos ASC";
            DataTable distinctValues = view.ToTable(true, "IntTotalPesos", "intTotalUsd", "strMarketingAirline", "strTextoDG", "str_Tipo_Moneda");
            return distinctValues;
        }
        /// <summary>
        /// Metodo que filtra los segmentos segun el trayecto (ida o regreso)
        /// </summary>
        /// <param name="tblItinerarios"></param>
        /// <returns></returns>
        private DataTable FiltrarSegmentosTrayectoBFM(DataTable tblSegmentos, string sTrayecto)
        {
            DataView view = new DataView(tblSegmentos);
            //fILTRAMOS TAMBINE MarriageGrp para traer los de ida y poderlo modificar en presentacion
            view.RowFilter = " strTrayecto = '" + sTrayecto + "' and MarriageGrp='O'";
            view.Sort = "DepartureDateTime ASC";
            view = new DataView(view.ToTable());
            DataTable distinctValues = view.ToTable(true, "DepartureDateTime", "ArrivalDateTime", "StopQuantity", "RPH", "InfoSource", "FlightNumber",
                "TourOperatorFlightID", "ResBookDesigCode", "ActionCode", "NumberInParty", "ElapsedTime", "MarriageGrp", /*"FlightSegment_Id",+*/
                "OriginDestinationOption_Id", "WS", "Condicion", "Ruta", "strDepartureAirport", "strArrivalAirport", "strOperatingAirline", "strEquipment",
                "strMarketingAirline", "strMarketingCabin", "strClase", /*"strTPA_Extensions",*/ "strCodeContext", "strTipoTrayecto", "dtmFechaSalida",
                "dtmFechaLLegada", "strNombre_Aerolinea", "intId_Aerolinea", "strAeropuerto_Llegada", "intId_Aeropuerto_Llegada", "intId_Pais_Llegada",
                "Code_Aeropuerto_Llegada", "strCiudad_Llegada", "strAeropuerto_Salida", "intId_Aeropuerto_Salida", "intId_Pais_Salida", "Code_Aeropuerto_Salida",
                "strCiudad_Salida", "strParadas", "strEstiloParada", "strDescripcionParadas", "intPrecioDesde", "str_Tipo_Moneda", "strTrayecto", "urlImagenAerolinea");

            view = new DataView(distinctValues);
            DataTable distincODOptions = view.ToTable(true, "OriginDestinationOption_Id");
            DataTable distinctValuesRetorno = distinctValues.Clone();
            bool bIndicadorExiste = false;
            clsCache cCache = new csCache().cCache();
            foreach (DataRow dtDatos in distinctValues.Rows)
            {
                bIndicadorExiste = false;
                foreach (DataRow dtDatosDef in distinctValuesRetorno.Rows)
                {
                    if (dtDatos["OriginDestinationOption_Id"].ToString().Equals(dtDatosDef["OriginDestinationOption_Id"].ToString()) &&
                        (!cCache.AeropuertoDestino.SCodigo.Equals(dtDatos["strDepartureAirport"].ToString()) && !cCache.AeropuertoOrigen.SCodigo.Equals(dtDatos["strDepartureAirport"].ToString())))
                    {
                        bIndicadorExiste = true;
                        break;
                    }
                }
                if (!bIndicadorExiste)
                {
                    distinctValuesRetorno.Rows.Add(dtDatos.ItemArray);
                }
            }
            return distinctValuesRetorno;
        }
        /// <summary>
        /// Metodo que filtra los itinerarios pertenecientes a una aerolinea con un precio especifico
        /// </summary>
        /// <param name="tblItinerarios"></param>
        /// <returns></returns>
        private DataTable FiltrarCodigosItinBFM(DataTable tblItinerarios, string sValor, string sAerolinea)
        {

            DataView view = new DataView(tblItinerarios);
            view.RowFilter = " intTotalPesos = " + sValor.Replace(",", ".") + " AND strMarketingAirline = '" + sAerolinea + "'";
            return view.ToTable();
        }
        public void InicializarPaginaHoras(UserControl ucResultados, DataSet dsSabreAir)
        {
            try
            {
                Repeater rptItinerario = ucResultados.FindControl("rptItinerario") as Repeater;
                Repeater rptSeleccion = ucResultados.FindControl("rptSeleccion") as Repeater;
                try
                {
                    if (dsSabreAir != null)
                    {
                        DataTable dtPricedItinerary = dsSabreAir.Tables["OriginDestinationOption"];
                        DataView dtvPricedItinerary = new DataView(dtPricedItinerary);
                        try
                        {
                            dtvPricedItinerary.Sort = "Consecutivo";
                            dtPricedItinerary = dtvPricedItinerary.ToTable();
                            dtPricedItinerary.AcceptChanges();
                        }
                        catch
                        { }
                        rptItinerario.DataSource = dtPricedItinerary;
                        rptItinerario.DataBind();
                        CargarSegmentosHoras(rptItinerario);
                    }
                    else
                    {
                        rptItinerario.DataSource = null;
                        rptItinerario.DataBind();
                    }
                }
                catch { }
                try
                {
                    DataSet dsData = clsSesiones.GetDatasetSelectSabreAir();
                    if (rptSeleccion != null)
                    {
                        try
                        {
                            if (dsData.Tables.Count > 1)
                            {
                                rptSeleccion.DataSource = dsData.Tables["tblSelect"].DefaultView;
                                rptSeleccion.DataBind();
                                bool bActivo = false;
                                try
                                {                                   
                                    bActivo = bool.Parse(dsData.Tables["tblSelect"].Rows[0]["bolUltimo"].ToString());                                    
                                }
                                catch { }

                                CargarSegmentosItinerario(rptSeleccion, dsData.Tables["FlightSegment"]);
                                if (bActivo)
                                {
                                    /*NOMBRE DE USUARIO  Y TOTAL*/
                                    Label lblNombreUsuario = ucResultados.FindControl("lblNombreUsuario") as Label;
                                    Label lblTotal = ucResultados.FindControl("lblTotal") as Label;
                                    Label lblMonedaTotal = ucResultados.FindControl("lblMonedaTotal") as Label;
                                    Label lblTotalTiquete = ucResultados.FindControl("lblValTiquete") as Label;

                                    DataSet dsDtSabre = clsSesiones.GetDatasetSabreAir();

                                    Repeater rptItinerarioDetalle = ucResultados.FindControl("rptItinerarioDetalle") as Repeater;
                                    DataTable dtItinerario = dsDtSabre.Tables["PricedItinerary"]; ;

                                    if (dtItinerario != null && dtItinerario.Rows.Count > 0)
                                    {
                                        rptItinerarioDetalle.DataSource = dtItinerario;
                                        rptItinerarioDetalle.DataBind();
                                        /*TOTAL EN PESOS*/
                                        if (lblTotal != null)
                                            lblTotal.Text = Convert.ToDecimal(dtItinerario.Rows[0]["IntTotalPesos"].ToString()).ToString("###,###");
                                        /*TOTAL EN PESOS IROTAMA*/
                                        if (lblTotalTiquete != null)
                                            lblTotalTiquete.Text = Convert.ToDecimal(dtItinerario.Rows[0]["IntTotalPesos"].ToString()).ToString("C");
                                        /*TIPO MONEDA*/
                                        if (lblMonedaTotal != null)
                                            lblMonedaTotal.Text = dtItinerario.Rows[0]["str_Tipo_Moneda"].ToString();

                                        CargarTiposPasajeros(rptItinerarioDetalle);
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }
            catch { }
        }
        public void InicializarPaginaPlanes(UserControl ucResultados, DataSet dsSabreAir)
        {
            try
            {
                Repeater rptItinerarioDetalle = ucResultados.FindControl("rptItinerarioDetalle") as Repeater;
                DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"]; ;

                if (dtItinerario != null && dtItinerario.Rows.Count > 0)
                {
                    rptItinerarioDetalle.DataSource = dtItinerario;
                    rptItinerarioDetalle.DataBind();
                    CargarTiposPasajeros(rptItinerarioDetalle);
                }
            }
            catch { }
        }
        /*METODOS PARA LA PAGINA DE RESULTADOS DE VUELOS*/
        private void LlenarFiltro(UserControl ucResultados, DataSet dsSabreAir)
        {
            try
            {
                Repeater dtFiltroAir = ucResultados.FindControl("dtFiltroAir") as Repeater;
                if (dtFiltroAir != null)
                {
                    bool bEntra = false;
                    string sValidaMatriz = clsValidaciones.GetKeyOrAdd("bValidaMatriz", "False");
                    if (sValidaMatriz.ToUpper().Equals("TRUE"))
                        bEntra = true;

                    string sRutaAirLine = clsValidaciones.RutaImagesAirGen();
                    string strMarketingAirline = "strMarketingAirline";
                    string urlImagenAerolinea = "urlImagenAerolinea";
                    string strNombre_Aerolinea = "strNombre_Aerolinea";
                    string IntPrecioDesde = "IntPrecioDesde";
                    string str_Tipo_Moneda = "str_Tipo_Moneda";
                    string strStopQuantity = "StopQuantity";
                    string strStopQuantity_1 = "StopQuantity_1";
                    string strStopQuantity_2 = "StopQuantity_2";
                    string IntPrecioDesde_1 = "IntPrecioDesde_1";
                    string IntPrecioDesde_2 = "IntPrecioDesde_2";

                    DataTable dtFiltro = dsSabreAir.Tables["dtFilter"];
                    DataView dvFlightSegment = new DataView(dtFiltro);

                    DataTable dtResultado = dvFlightSegment.ToTable();
                    DataTable dtResultadoAir = dvFlightSegment.ToTable();
                    DataTable dtResultadoTemp = dtResultado.Clone();

                    dtResultadoTemp.Columns.Add(urlImagenAerolinea, typeof(string));

                    dtResultadoTemp.Columns.Add(IntPrecioDesde_1, typeof(decimal));
                    dtResultadoTemp.Columns.Add(IntPrecioDesde_2, typeof(decimal));

                    dtResultadoTemp.Columns.Add(strStopQuantity_1, typeof(string));
                    dtResultadoTemp.Columns.Add(strStopQuantity_2, typeof(string));

                    DataRow drTableRow = dtResultadoTemp.NewRow();
                    drTableRow[strMarketingAirline] = "0";
                    drTableRow[urlImagenAerolinea] = sRutaAirLine + "Todas.gif";
                    drTableRow[strNombre_Aerolinea] = "Todas";
                    drTableRow[str_Tipo_Moneda] = "COP";
                    drTableRow[IntPrecioDesde] = 0;
                    drTableRow[strStopQuantity] = "--";
                    drTableRow[IntPrecioDesde_1] = 0;
                    drTableRow[strStopQuantity_1] = "--";
                    drTableRow[IntPrecioDesde_2] = 0;
                    drTableRow[strStopQuantity_2] = "--";

                    dtResultadoTemp.Rows.Add(drTableRow);

                    foreach (DataRow rowAir in dtResultadoAir.Rows)
                    {
                        bool bAirLine = true;

                        foreach (DataRow rowAirTemp in dtResultadoTemp.Rows)
                        {
                            if (rowAir[strMarketingAirline].ToString().Equals(rowAirTemp[strMarketingAirline].ToString()))
                            {
                                bAirLine = false;
                                break;
                            }
                        }
                        if (bAirLine)
                        {
                            DataRow drTableRow1 = dtResultadoTemp.NewRow();

                            drTableRow1[strMarketingAirline] = rowAir[strMarketingAirline];
                            drTableRow1[strNombre_Aerolinea] = rowAir[strNombre_Aerolinea];
                            drTableRow1[str_Tipo_Moneda] = rowAir[str_Tipo_Moneda];
                            drTableRow1[urlImagenAerolinea] = sRutaAirLine + rowAir[strMarketingAirline].ToString() + ".gif";
                            if (bEntra)
                            {
                                for (int c = 0; c < 3; c++)
                                {
                                    foreach (DataRow row in dtResultado.Rows)
                                    {
                                        if (rowAir[strMarketingAirline].ToString().Equals(row[strMarketingAirline].ToString()))
                                        {
                                            if (c.Equals(0))
                                            {
                                                if (row[strStopQuantity].ToString().Equals("0"))
                                                {
                                                    drTableRow1[IntPrecioDesde] = row[IntPrecioDesde];
                                                    drTableRow1[strStopQuantity] = Convert.ToDecimal(row[IntPrecioDesde].ToString()).ToString("###,##0");
                                                    break;
                                                }
                                                else
                                                {
                                                    drTableRow1[IntPrecioDesde] = 0;
                                                    drTableRow1[strStopQuantity] = "--";
                                                }
                                            }
                                            else
                                            {
                                                if (c.Equals(1))
                                                {
                                                    if (row[strStopQuantity].ToString().Equals("1"))
                                                    {
                                                        drTableRow1[IntPrecioDesde_1] = row[IntPrecioDesde];
                                                        drTableRow1[strStopQuantity_1] = Convert.ToDecimal(row[IntPrecioDesde].ToString()).ToString("###,##0");
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        drTableRow1[IntPrecioDesde_1] = 0;
                                                        drTableRow1[strStopQuantity_1] = "--";
                                                    }
                                                }
                                                else
                                                {
                                                    if (int.Parse(row[strStopQuantity].ToString()) > 1)
                                                    {
                                                        drTableRow1[IntPrecioDesde_2] = row[IntPrecioDesde];
                                                        drTableRow1[strStopQuantity_2] = Convert.ToDecimal(row[IntPrecioDesde].ToString()).ToString("###,##0");
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        drTableRow1[IntPrecioDesde_2] = 0;
                                                        drTableRow1[strStopQuantity_2] = "--";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                drTableRow1[IntPrecioDesde] = rowAir[IntPrecioDesde];
                            }
                            dtResultadoTemp.Rows.Add(drTableRow1);
                        }
                    }
                    dtResultadoTemp.AcceptChanges();
                    dtFiltroAir.DataSource = dtResultadoTemp;
                    dtFiltroAir.DataBind();
                }
            }
            catch { }
        }
        public void OrdenarResultados(UserControl ucResultados, String str_Criterio)
        {
            DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
            DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
            DataView dvPricedItinerary = new DataView(dtPricedItinerary);
            try
            {
                dvPricedItinerary.Sort = str_Criterio;
                dtPricedItinerary = dvPricedItinerary.ToTable();

            }
            catch (Exception)
            { ExceptionHandled.Publicar("No se realizo el ordenamiento"); }
            Repeater rptItinerario = ucResultados.FindControl("rptItinerario") as Repeater;
            rptItinerario.DataSource = dtPricedItinerary;
            rptItinerario.DataBind();

            LlenarFiltro(ucResultados, dsSabreAir);

            CargarSegmentos(rptItinerario);
        }
        /// <summary>
        /// Se Ordenan los resultados de acuerdo al diseño
        /// datalis ida
        /// datalist vuelta
        /// hceron
        /// </summary>
        /// <param name="ucResultados"></param>
        /// <param name="str_Criterio"></param>
        public void OrdenarResultadosBFM(UserControl ucResultados, String str_Criterio)
        {
            DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
            InicializarPaginaBFM(ucResultados, dsSabreAir, str_Criterio);
        }
        public DataTable ConsultarAerolinea(string strCode)
        {
            DataTable dtAerolineas = new DataTable();
            try
            {
                string sConsulta = "EXEC SPCONSULTAAEROLINEA '" + strCode + "'";
                dtAerolineas = new CsConsultasVuelos().Consultatabla(sConsulta);
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "ConsultarAerolinea";
                ExceptionHandled.Publicar(cParametros);                
            }
           
            return dtAerolineas;
        }
        /*METODOS PARA LA PAGINA DE RESULTADOS DE VUELOS*/
        public void FiltrarResultados(UserControl ucResultados, String str_Criterio)
        {
            DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
            DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
            if (!str_Criterio.Equals("0"))
            {
                try
                {
                    string sWhere = "strMarketingAirline = '" + str_Criterio + "'";
                    dtPricedItinerary = clsDataNet.dsDataWhere(sWhere, dtPricedItinerary);
                }
                catch (Exception)
                { ExceptionHandled.Publicar("No se realizo el filtro"); }
            }
            Repeater rptItinerario = ucResultados.FindControl("rptItinerario") as Repeater;
            rptItinerario.DataSource = dtPricedItinerary;
            rptItinerario.DataBind();
            CargarSegmentos(rptItinerario);
        }
        #region Otros filtros
        #region filtros BFM

        /// <summary>
        /// Filter por criterios
        /// hceron
        /// </summary>
        /// <param name="ucResultados"></param>
        /// <param name="str_Criterio"></param>
        public void FiltrarResultadosAerolineaBFM(Control ucResultados, String str_Criterio)
        {
            DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
            //findControl Result to load Repeter
            InicializarPaginaBFM((UserControl)ucResultados.FindControl("ucResultadoVuelos"), dsSabreAir, "SequenceNumber ASC", str_Criterio);
        }
        #endregion


        public void FiltrarResultadosOtros(Control ucResultados, String str_Criterio)
        {
            DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
            DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
            if (!str_Criterio.Equals("0"))
            {
                try
                {
                    //  string sWhere = "strMarketingAirline";
                    dtPricedItinerary = clsDataNet.dsDataOrder(str_Criterio, dtPricedItinerary);


                }
                catch (Exception)
                { ExceptionHandled.Publicar("No se realizo el filtro"); }
            }
            Repeater rptItinerario = ucResultados.FindControl("ucResultadoVuelos").FindControl("rptItinerario") as Repeater;
            rptItinerario.DataSource = dtPricedItinerary;
            rptItinerario.DataBind();
            CargarSegmentos(rptItinerario);
        }


        public void FiltrarResultadosAerolinea(Control ucResultados, String str_Criterio)
        {
            DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
            DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
            if (!str_Criterio.Equals("0"))
            {
                try
                {
                    //  string sWhere = "strMarketingAirline";
                    dtPricedItinerary = clsDataNet.dsDataWhere(str_Criterio, dtPricedItinerary);


                }
                catch (Exception)
                { ExceptionHandled.Publicar("No se realizo el filtro"); }
            }
            Repeater rptItinerario = ucResultados.FindControl("ucResultadoVuelos").FindControl("rptItinerario") as Repeater;
            rptItinerario.DataSource = dtPricedItinerary;
            rptItinerario.DataBind();
            CargarSegmentos(rptItinerario);
        }

        /// <summary>
        /// Traemos los resultados a patir del criterio de horas en le vuelo
        /// </summary>
        /// <param name="ucResultados"></param>
        /// <param name="str_Criterio"></param>
        public void FiltrarResultadosHoras(UserControl ucResultados, string str_Criterio, string CiudadSalida)
        {
            System.Data.DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
            DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
            char[] delimiterChars = { '|' };


            if (!str_Criterio.Equals("0"))
            {
                string[] sFilter = str_Criterio.Split(delimiterChars);
                int iFechaI = Convert.ToInt16(sFilter[0]);
                int iFechafin = Convert.ToInt16(sFilter[1]);

                try
                {

                    IEnumerable<DataRow> query = from PricedItinerary in dsSabreAir.Tables["PricedItinerary"].AsEnumerable()
                                                 from AirItinerary in dsSabreAir.Tables["AirItinerary"].AsEnumerable()
                                                 from OriginDestinationOptions in dsSabreAir.Tables["OriginDestinationOptions"].AsEnumerable()
                                                 from OriginDestinationOption in dsSabreAir.Tables["OriginDestinationOption"].AsEnumerable()
                                                 from FlightSegment in dsSabreAir.Tables["FlightSegment"].AsEnumerable()
                                                 where (PricedItinerary.Field<int>("PricedItinerary_Id") == AirItinerary.Field<int>("PricedItinerary_Id"))
                                                        & (AirItinerary.Field<int>("AirItinerary_Id") == OriginDestinationOptions.Field<int>("AirItinerary_Id"))
                                                        & (OriginDestinationOptions.Field<int>("OriginDestinationOptions_Id") == OriginDestinationOption.Field<int>("OriginDestinationOptions_Id"))
                                                        & (OriginDestinationOption.Field<int>("OriginDestinationOption_Id") == FlightSegment.Field<int>("OriginDestinationOption_Id"))
                                                      & Convert.ToDateTime(FlightSegment.Field<string>("DepartureDateTime")).Hour >= iFechaI
                                                     & Convert.ToDateTime(FlightSegment.Field<string>("DepartureDateTime")).Hour <= iFechafin
                                                     & FlightSegment.Field<string>("strDepartureAirport") == CiudadSalida
                                                 select PricedItinerary;

                    IEnumerable<DataRow> Querufilter = query.Distinct();
                    dtPricedItinerary = Querufilter.CopyToDataTable<DataRow>();



                }
                catch (Exception)
                { ExceptionHandled.Publicar("No se realizo el filtro"); }
            }
            Repeater rptItinerario = ucResultados.FindControl("rptItinerario") as Repeater;
            rptItinerario.DataSource = dtPricedItinerary;
            rptItinerario.DataBind();
            CargarSegmentos(rptItinerario);
        }

        #endregion
        public void OrdenarResultadosHoras(UserControl ucResultados, String str_Criterio)
        {
            DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
            DataTable dtFlightSegment = dsSabreAir.Tables["FlightSegment"];
            DataView dtvFlightSegment = new DataView(dtFlightSegment);
            try
            {
                dtvFlightSegment.Sort = str_Criterio;
                dtFlightSegment = dtvFlightSegment.ToTable();

            }
            catch (Exception)
            { ExceptionHandled.Publicar("No se realizo el ordenamiento"); }

            DataTable dtPricedItinerary = dsSabreAir.Tables["OriginDestinationOption"];
            Repeater rptItinerario = ucResultados.FindControl("rptItinerario") as Repeater;
            rptItinerario.DataSource = dtPricedItinerary;
            rptItinerario.DataBind();
            CargarSegmentosHoras(rptItinerario);
            LlenarFiltro(ucResultados, dsSabreAir);
        }
        private void CargarSegmentos(Repeater rptItinerario)
        {
            DataTable dtItinerario = rptItinerario.DataSource as DataTable;

            for (int c = 0; c < dtItinerario.Rows.Count; c++)
            {
                DataList dtlSegmentos = rptItinerario.Items[c].FindControl("dtlSegmentos") as DataList;
                dtlSegmentos.DataSource = GetDtFlightSegmento(dtItinerario.Rows[c]["PricedItinerary_Id"].ToString());
                dtlSegmentos.DataBind();

                try
                {
                    Repeater RptTiposPasajeros = rptItinerario.Items[c].FindControl("RptTiposPasajeros") as Repeater;
                    if (RptTiposPasajeros != null)
                    {
                       RptTiposPasajeros.DataSource = GetDtPassengerTypeQuantity(dtItinerario.Rows[c]["PricedItinerary_Id"].ToString());
                        RptTiposPasajeros.DataBind();
                        /*OBTENEMOS LAS TARIFAS POR TIPO PAX*/
                        CargarTarifasResultados(RptTiposPasajeros, dtItinerario.Rows[c]["PricedItinerary_Id"].ToString());
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Mostramos de manera modal la descripcion  total del vuelo
        /// </summary>
        /// <param name="rptItinerario"></param>
        public void CargarSegmentosModal(Repeater rptItinerario, string sIdaVuelta)//I,R
        {
            DataTable dtItinerario = rptItinerario.DataSource as DataTable;
            DataTable dtSegmnet = null;
            for (int c = 0; c < dtItinerario.Rows.Count; c++)
            {
                Repeater dtlSegmentos = rptItinerario.Items[c].FindControl("rptDetalleModal") as Repeater;
                dtSegmnet = GetDtFlightSegmento(dtItinerario.Rows[c]["PricedItinerary_Id"].ToString());


                string sWhere = "strTrayecto = '" + sIdaVuelta + "'";
                dtSegmnet = clsDataNet.dsDataWhere(sWhere, dtSegmnet);
                dtlSegmentos.DataSource = dtSegmnet;

                dtlSegmentos.DataBind();

                //try
                //{
                //    DataList dtlTiposPasajeros = rptItinerario.Items[c].FindControl("dtlTiposPasajeros") as DataList;
                //    if (dtlTiposPasajeros != null)
                //    {
                //        dtlTiposPasajeros.DataSource = GetDtPassengerTypeQuantity(dtItinerario.Rows[c]["PricedItinerary_Id"].ToString());
                //        dtlTiposPasajeros.DataBind();
                //        /*OBTENEMOS LAS TARIFAS POR TIPO PAX*/
                //        CargarTarifasResultados(dtlTiposPasajeros, dtItinerario.Rows[c]["PricedItinerary_Id"].ToString());
                //    }
                //}
                //catch { }
            }
        }



        private void OcultarSegmentosRptBFM(DataList rptSegmentos)
        {
            Label lblCiudadSalida = null;
            Label lblCiudadLlegada = null;
            Label lblCiudadSalidaCod = null;
            Label lblCiudadLlegadaCod = null;
            Label lblFechaSalida = null;
            for (int c = 0; c < rptSegmentos.Items.Count; c++)
            {
                if (c > 0)
                {
                    lblCiudadSalida = (Label)rptSegmentos.Items[c].FindControl("lblCiudadSalida");
                    lblCiudadLlegada = (Label)rptSegmentos.Items[c].FindControl("lblCiudadLlegada");
                    lblCiudadSalidaCod = (Label)rptSegmentos.Items[c].FindControl("lblCiudadSalidaCod");
                    lblCiudadLlegadaCod = (Label)rptSegmentos.Items[c].FindControl("lblCiudadLlegadaCod");
                    lblFechaSalida = (Label)rptSegmentos.Items[c].FindControl("lblFechaSalida");

                    if (lblCiudadSalida != null)
                        lblCiudadSalida.Visible = false;

                    if (lblCiudadLlegada != null)
                        lblCiudadLlegada.Visible = false;

                    if (lblCiudadSalidaCod != null)
                        lblCiudadSalidaCod.Visible = false;

                    if (lblCiudadLlegadaCod != null)
                        lblCiudadLlegadaCod.Visible = false;

                    if (lblFechaSalida != null)
                        lblFechaSalida.Visible = false;
                }
            }
        }
        private void OcultarSegmentosRptBFM(Repeater rptSegmentos)
        {
            Label lblCiudadSalida = null;
            Label lblCiudadLlegada = null;
            Label lblCiudadSalidaCod = null;
            Label lblCiudadLlegadaCod = null;
            Label lblFechaSalida = null;
            for (int c = 0; c < rptSegmentos.Items.Count; c++)
            {
                if (c > 0)
                {
                    lblCiudadSalida = (Label)rptSegmentos.Items[c].FindControl("lblCiudadSalida");
                    lblCiudadLlegada = (Label)rptSegmentos.Items[c].FindControl("lblCiudadLlegada");
                    lblCiudadSalidaCod = (Label)rptSegmentos.Items[c].FindControl("lblCiudadSalidaCod");
                    lblCiudadLlegadaCod = (Label)rptSegmentos.Items[c].FindControl("lblCiudadLlegadaCod");
                    lblFechaSalida = (Label)rptSegmentos.Items[c].FindControl("lblFechaSalida");

                    if (lblCiudadSalida != null)
                        lblCiudadSalida.Visible = false;

                    if (lblCiudadLlegada != null)
                        lblCiudadLlegada.Visible = false;

                    if (lblCiudadSalidaCod != null)
                        lblCiudadSalidaCod.Visible = false;

                    if (lblCiudadLlegadaCod != null)
                        lblCiudadLlegadaCod.Visible = false;

                    if (lblFechaSalida != null)
                        lblFechaSalida.Visible = false;
                }
            }
        }

        /// <summary>
        /// Se mdofica para reciba  Datatable y realize validacion para vuelos sin itinerarios
        /// jvargas
        /// </summary>
        /// <param name="rptItinerario"></param>
        /// <param name="dtItinerario">Itenerario del   que lo invoque</param>
        private void CargarSegmentosBFM(Repeater rptItinerario, DataTable dtItinerario)
        {
            //DataTable dtItinerario = dsSabreAir.Tables["PricedItinerary"]; 
            Repeater RptSegmentosIda = null;
            Repeater RptSegmentosReg = null;
            DataTable dtClone = null;
            DataTable dtItinerarios = null;
            bool valida = true;
            bool validaini = true;
            if (rptItinerario.DataSource != null)
            {
                dtItinerarios = (DataTable)rptItinerario.DataSource;
                dtClone = dtItinerarios.Clone();
            }

            for (int c = 0; c < rptItinerario.Items.Count; c++)
            {
                try
                {


                    Label lblTotalOriginal = (Label)rptItinerario.Items[c].FindControl("lblTotalOriginal");
                    Label lblAerolinea = (Label)rptItinerario.Items[c].FindControl("lblAerolinea");
                  


                    if (lblTotalOriginal != null && lblAerolinea != null)
                    {
                        DataTable tblItinFiltrados = FiltrarCodigosItinBFM(dtItinerario, lblTotalOriginal.Text, lblAerolinea.Text);
                        RptSegmentosIda = rptItinerario.Items[c].FindControl("RptSegmentosIda") as Repeater;
                        RptSegmentosReg = rptItinerario.Items[c].FindControl("RptSegmentosReg") as Repeater;
                        DataTable tblSegmentosTotal = null;
                        for (int d = 0; d < tblItinFiltrados.Rows.Count; d++)
                        {
                            if (tblSegmentosTotal == null)
                                tblSegmentosTotal = GetDtFlightSegmento(tblItinFiltrados.Rows[d]["PricedItinerary_Id"].ToString());
                            else
                                tblSegmentosTotal.Merge(GetDtFlightSegmento(tblItinFiltrados.Rows[d]["PricedItinerary_Id"].ToString()));
                        }
                        DataTable dtSegmIda = FiltrarSegmentosTrayectoBFM(tblSegmentosTotal, "I");
                        VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                        if (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto.ToString().ToUpper().Equals("IDAREGRESO"))
                        {
                            DataTable dtSegmRegValida = FiltrarSegmentosTrayectoBFM(tblSegmentosTotal, "R");
                            if (dtSegmRegValida.Rows.Count == 0)
                            {                               
                                dtSegmIda = null;
                                valida = false;
                                validaini = false;
                            }
                        }


                        if (RptSegmentosIda != null && dtSegmIda.Rows.Count > 0 && validaini)
                        {
                            List<string> Vuelos = new List<string>();
                            DataTable dtSegm = FiltrarSegmentosTrayectoBFM(tblSegmentosTotal, "I");

                            DataTable dtSegmReg = FiltrarSegmentosTrayectoBFM(tblSegmentosTotal, "R");

                            var query = (from dt in dtSegm.AsEnumerable()
                                         select new
                                         {
                                             Code = dt.Field<string>("FlightNumber")
                                         }
                                        ).ToList().Distinct();

                            DataTable dtSegmReg1 = dtSegmReg.Clone();
                            DataTable dtSegm1 = null;
                            if (query.ToList().Count > 1)
                            {
                                for (int li = 0; li < query.ToList().Count; li++)
                                {
                                    if (li > 0)
                                    {
                                        c++;
                                    }
                                    dtSegmReg1 = dtSegmReg.Clone();
                                    RptSegmentosIda = rptItinerario.Items[c].FindControl("RptSegmentosIda") as Repeater;
                                    RptSegmentosReg = rptItinerario.Items[c].FindControl("RptSegmentosReg") as Repeater;

                                    DataView dv = new DataView(dtSegm);
                                    dv.RowFilter = "FlightNumber ='" + query.ToArray()[li].Code + "'";
                                    DataTable dt = dv.ToTable();
                                    dtSegm1 = dt;
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        int Origendestination = Convert.ToInt32(dr["OriginDestinationOption_id"]);
                                        Origendestination++;
                                        DataView dvReg = new DataView(dtSegmReg);
                                        dvReg.RowFilter = "OriginDestinationOption_id='" + Origendestination + "'";

                                        foreach (DataRow dir in dvReg.ToTable().Rows)
                                        {
                                            dtSegmReg1.Rows.Add(dir.ItemArray);
                                        }
                                    }

                                    try
                                    {

                                        RptSegmentosIda.DataSource = dtSegm1;
                                        RptSegmentosIda.DataBind();

                                        if (RptSegmentosIda.Items.Count == 0)
                                        {
                                            RptSegmentosIda.DataSource = dtSegmReg1;
                                            RptSegmentosIda.DataBind();
                                            OcultarSegmentosRptBFM(RptSegmentosIda);
                                        }
                                        else
                                        {
                                            if (RptSegmentosReg != null)
                                            {
                                                RptSegmentosReg.DataSource = dtSegmReg1;
                                                RptSegmentosReg.DataBind();
                                                OcultarSegmentosRptBFM(RptSegmentosReg);
                                            }
                                        }
                                        OcultarSegmentosRptBFM(RptSegmentosIda);

                                        Repeater RptTiposPasajeros = rptItinerario.Items[c].FindControl("RptTiposPasajeros") as Repeater;
                                        if (RptTiposPasajeros != null)
                                        {
                                            //Se filtra por la aerolinea------------------------
                                            DataView dvFiltroAirline = new DataView(dtItinerario);
                                            dvFiltroAirline.RowFilter = "strMarketingAirline = '" + lblAerolinea.Text + "' AND " + " intTotalPesos = " + lblTotalOriginal.Text.Replace(",", ".");

                                            DataTable tblFiltro = dvFiltroAirline.ToTable();
                                            //--------------------------------------------------
                                            if (tblFiltro.Rows.Count > 0)
                                                RptTiposPasajeros.DataSource = GetDtPassengerTypeQuantity(tblFiltro.Rows[0]["PricedItinerary_Id"].ToString());
                                            else
                                                RptTiposPasajeros.DataSource = GetDtPassengerTypeQuantity(dtItinerario.Rows[c]["PricedItinerary_Id"].ToString());

                                            RptTiposPasajeros.DataBind();
                                            /*OBTENEMOS LAS TARIFAS POR TIPO PAX*/
                                            CargarTarifasResultados(RptTiposPasajeros, dtItinerario.Rows[c]["PricedItinerary_Id"].ToString());
                                        }
                                    }
                                    catch { }
                                    LoadStyle(RptSegmentosIda, RptSegmentosReg);
                                    valida = false;
                                }

                                var prodCountQuery = from prod in dtSegmReg1.AsEnumerable()
                                                     group prod by prod.Field<string>("FlightNumber") into grouping
                                                     where grouping.Count() >= 1
                                                     select new
                                                     {
                                                         grouping.Key,
                                                         ProductCount = grouping.Count()
                                                     };
                                DataTable dtfiltros = new DataTable();
                                dtfiltros.Columns.Add("Vuelo", typeof(String));
                                dtfiltros.Columns.Add("Coincidencias", typeof(int));
                                DataRow drfiltros = dtfiltros.NewRow();
                                foreach (var prodCount in prodCountQuery)
                                {
                                    drfiltros["Vuelo"] = prodCount.Key;
                                    drfiltros["Coincidencias"] = prodCount.ProductCount.ToString();
                                    dtfiltros.Rows.Add(drfiltros.ItemArray);
                                }
                            }
                            else
                            {
                                valida = true;
                            }

                            if (query.ToList().Count == 1)
                            {
                                RptSegmentosIda.DataSource = FiltrarSegmentosTrayectoBFM(tblSegmentosTotal, "I");
                                RptSegmentosIda.DataBind();

                                if (RptSegmentosIda.Items.Count == 0)
                                {
                                    RptSegmentosIda.DataSource = FiltrarSegmentosTrayectoBFM(tblSegmentosTotal, "R");
                                    RptSegmentosIda.DataBind();
                                    OcultarSegmentosRptBFM(RptSegmentosIda);
                                }
                                else
                                {
                                    if (RptSegmentosReg != null)
                                    {
                                        RptSegmentosReg.DataSource = FiltrarSegmentosTrayectoBFM(tblSegmentosTotal, "R");
                                        RptSegmentosReg.DataBind();
                                        OcultarSegmentosRptBFM(RptSegmentosReg);
                                    }
                                }
                                OcultarSegmentosRptBFM(RptSegmentosIda);
                            }
                        }
                        if (valida)
                        {
                            try
                            {
                                if (dtSegmIda.Rows.Count == 0)
                                {
                                    RptSegmentosIda.DataSource = FiltrarSegmentosTrayectoBFM(tblSegmentosTotal, "I");
                                    RptSegmentosIda.DataBind();

                                    if (RptSegmentosIda.Items.Count == 0)
                                    {
                                        RptSegmentosIda.DataSource = FiltrarSegmentosTrayectoBFM(tblSegmentosTotal, "R");
                                        RptSegmentosIda.DataBind();
                                        OcultarSegmentosRptBFM(RptSegmentosIda);
                                    }
                                    else
                                    {
                                        if (RptSegmentosReg != null)
                                        {
                                            RptSegmentosReg.DataSource = FiltrarSegmentosTrayectoBFM(tblSegmentosTotal, "R");
                                            RptSegmentosReg.DataBind();
                                            OcultarSegmentosRptBFM(RptSegmentosReg);
                                        }
                                    }
                                    OcultarSegmentosRptBFM(RptSegmentosIda);
                                }


                                Repeater RptTiposPasajeros = rptItinerario.Items[c].FindControl("RptTiposPasajeros") as Repeater;
                                if (RptTiposPasajeros != null)
                                {
                                    //Se filtra por la aerolinea------------------------
                                    DataView dvFiltroAirline = new DataView(dtItinerario);
                                    dvFiltroAirline.RowFilter = "strMarketingAirline = '" + lblAerolinea.Text + "' AND " + " intTotalPesos = " + lblTotalOriginal.Text.Replace(",", ".");

                                    DataTable tblFiltro = dvFiltroAirline.ToTable();
                                    //--------------------------------------------------
                                    if (tblFiltro.Rows.Count > 0)
                                        RptTiposPasajeros.DataSource = GetDtPassengerTypeQuantity(tblFiltro.Rows[0]["PricedItinerary_Id"].ToString());
                                    else
                                        RptTiposPasajeros.DataSource = GetDtPassengerTypeQuantity(dtItinerario.Rows[c]["PricedItinerary_Id"].ToString());

                                    RptTiposPasajeros.DataBind();
                                    /*OBTENEMOS LAS TARIFAS POR TIPO PAX*/
                                    CargarTarifasResultados(RptTiposPasajeros, dtItinerario.Rows[c]["PricedItinerary_Id"].ToString());
                                }
                            }
                            catch { }
                        }
                    }
                    if (valida)
                    {
                        LoadStyle(RptSegmentosIda, RptSegmentosReg);
                    }
                }
                catch { }
            }



        }
        /// <summary>
        /// Show olnly one image Top
        /// hceron
        /// </summary>
        /// <param name="dtlSegmentosIda"></param>
        /// <param name="dtlSegmentosReg"></param>
        private void LoadStyle(Repeater RptSegmentosIda, Repeater  RptSegmentosReg)
        {
            Image sAirplane = null;
            Image iNext = null;
            Image sAirplaneTRIP = null;
            Image iNextTRIP = null;
            HtmlControl tHr = null;
            Label lSentido = null;
            int iVisible = 0;

            if (RptSegmentosIda != null && RptSegmentosIda.Items.Count > 0)
            {
                sAirplane = (Image)RptSegmentosIda.Items[0].FindControl("ImgIda");

                sAirplane.ImageUrl = "~/App_Themes/Imagenes/avionIda.png";
                iNext = (Image)RptSegmentosIda.Items[0].FindControl("iNext");
                iNext.ImageUrl = "~/App_Themes/Imagenes/icon-Next.png";
                sAirplane.Visible = true;
                iNext.Visible = true;
                //Last reord visible per segmnet
                iVisible = showDvide(RptSegmentosIda, "tblIda");

                tHr = (HtmlControl)RptSegmentosIda.Items[iVisible].FindControl("tblIda").FindControl("trIda");
                tHr.Visible = true;

                lSentido = (Label)RptSegmentosIda.Items[0].FindControl("tblIda").FindControl("lblIda");
                lSentido.Visible = true;
            }
            //else
            //{
            //    if (sAirplane != null) sAirplane.Visible = false;
            //    if (iNext != null) iNext.Visible = false;
            //}
            if (RptSegmentosReg != null && RptSegmentosReg.Items.Count > 0)
            {
                sAirplaneTRIP = (Image)RptSegmentosReg.Items[0].FindControl("ImgVuelta");
                sAirplaneTRIP.ImageUrl = "~/App_Themes/Imagenes/avionVenida.png";
                iNextTRIP = (Image)RptSegmentosReg.Items[0].FindControl("iNext");
                iNextTRIP.ImageUrl = "~/App_Themes/Imagenes/icon-Next.png";

                sAirplaneTRIP.Visible = true;
                iNextTRIP.Visible = true;

                lSentido = (Label)RptSegmentosReg.Items[0].FindControl("lblVuelta");
                lSentido.Visible = true;
            }
            //else
            //{
            //    if (sAirplaneTRIP != null) sAirplaneTRIP.Visible = false;
            //    if (iNextTRIP != null) iNextTRIP.Visible = false;
            //}
        }
        /// <summary>
        /// last Record visible
        /// </summary>
        /// <param name="dtlSegmentos"></param>
        /// <param name="sSeg"></param>
        /// <returns></returns>
        private int showDvide(DataList dtlSegmentos, string sSeg)
        {
            int imax = 0;
            for (int i = 0; i < (dtlSegmentos).Items.Count; i++)
            {
                HtmlTable tblIda = (HtmlTable)(dtlSegmentos).Items[i].FindControl(sSeg);//"tblIda");
                if (tblIda.Visible && imax < i)
                {
                    imax = i;
                }

            }
            return imax;
        }
        private int showDvide(Repeater RptSegmentos, string sSeg)
        {
            int imax = 0;
            for (int i = 0; i < (RptSegmentos).Items.Count; i++)
            {
                HtmlTable tblIda = (HtmlTable)(RptSegmentos).Items[i].FindControl(sSeg);//"tblIda");
                if (tblIda.Visible && imax < i)
                {
                    imax = i;
                }

            }
            return imax;
        }

        private void CargarSegmentosHoras(Repeater rptItinerario)
        {
            DataTable dtItinerario = rptItinerario.DataSource as DataTable;

            for (int c = 0; c < dtItinerario.Rows.Count; c++)
            {
                DataList dtlSegmentos = rptItinerario.Items[c].FindControl("dtlSegmentos") as DataList;
                dtlSegmentos.DataSource = GetDtFlightSegmentoHoras(dtItinerario.Rows[c]["OriginDestinationOption_Id"].ToString());
                dtlSegmentos.DataBind();
            }
        }
        private void CargarSegmentosItinerario(Repeater rptItinerario, DataTable dtItinerario)
        {
            try
            {
                DataTable dtSegmentoIda = new DataTable();
                dtSegmentoIda = GetDtSegmento(1, dtItinerario);
                if (dtSegmentoIda.Rows.Count > 0)
                {
                    Repeater RptSegmentosIda = rptItinerario.Items[0].FindControl("RptSegmentosIda") as Repeater;
                    RptSegmentosIda.DataSource = dtSegmentoIda.DefaultView;
                    RptSegmentosIda.DataBind();
                }
                DataTable dtSegmentoRegreso = new DataTable();
                dtSegmentoRegreso = GetDtSegmento(2, dtItinerario);
                if (dtSegmentoRegreso.Rows.Count > 0)
                {
                    DataList dtlSegmentosRegreso = rptItinerario.Items[0].FindControl("dtlSegmentosRegreso") as DataList;
                    dtlSegmentosRegreso.DataSource = dtSegmentoRegreso.DefaultView;
                    dtlSegmentosRegreso.DataBind();
                }
            }
            catch { }
        }
        private void CargarTarifasResultados(Repeater RptTipoPax, string intIdPricedIinerary)
        {
            
            DataTable dtTipoPax = RptTipoPax.DataSource as DataTable;
            decimal ValorConImpuestos = 0;

            for (int c = 0; c < dtTipoPax.Rows.Count; c++)
            {
                decimal Impuestos = 0;
                Label lblValorSinImp = (Label)RptTipoPax.Items[c].FindControl("lblValorSinImp");
                Repeater RptTarifas = RptTipoPax.Items[c].FindControl("RptTarifas") as Repeater;
                DataTable dtTarifas = GetDtPassengerFare(dtTipoPax.Rows[c]["PTC_FareBreakdown_Id"].ToString());
                RptTarifas.DataSource = dtTarifas;
                RptTarifas.DataBind();
                CargarImpuestos(RptTarifas, dtTarifas);
                ValorConImpuestos = (Convert.ToDecimal(dtTarifas.Rows[0]["intCantidad"].ToString()) * Convert.ToDecimal(dtTarifas.Rows[0]["intTotalTarifaConTAXPersona"].ToString()));

                if (HttpContext.Current.Session["Sumaimpuestos"] != null)
                {
                    Impuestos = Convert.ToDecimal(HttpContext.Current.Session["Sumaimpuestos"].ToString());
                    Impuestos=Convert.ToDecimal(dtTarifas.Rows[0]["intCantidad"].ToString()) * Impuestos;
                }
                else
                    Impuestos = 0;


                if (lblValorSinImp != null)
                {                    
                    lblValorSinImp.Text = (ValorConImpuestos - Impuestos).ToString();
                }
            }

            HttpContext.Current.Session["Sumaimpuestos"] = null;

        }

        private void CargarImpuestos(Repeater RptTarifas, DataTable dtTarifas)
        {
            for (int c = 0; c < dtTarifas.Rows.Count; c++)
            {
                try
                {
                    Repeater RptImpuestos = RptTarifas.Items[c].FindControl("RptImpuestos") as Repeater;
                    DataTable dt_Tasas = GetDtPassengerFareTax(dtTarifas.Rows[c]["PassengerFare_Id"].ToString());
                    DataTable dt_Tasas_Total = Filtrar_Impuestos(dt_Tasas);
                    DataTable dt_Tasas_TotalCopia = new DataTable();
                    decimal sComision = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("ValorComision", "1"));

                    if (clsValidaciones.GetKeyOrAdd("Sumaimpuestos", "FALSE").ToUpper().Equals("TRUE"))
                    {
                        int cargos = 0;
                        int TA = 0;


                        for (int i = 0; i < dt_Tasas_Total.Rows.Count; i++)
                        {


                            if (dt_Tasas_Total.Rows[i]["taxcode"].ToString() != "ITA" && dt_Tasas_Total.Rows[i]["taxcode"].ToString() != "TA")
                            {

                                cargos = cargos + int.Parse(dt_Tasas_Total.Rows[i]["amount"].ToString());

                            }
                            else
                            {

                                TA = TA + int.Parse(dt_Tasas_Total.Rows[i]["amount"].ToString());

                            }

                        }
                        for (int a = 0; a < dt_Tasas_Total.Rows.Count; a++)
                        {
                            if (dt_Tasas_Total.Rows[a]["taxcode"].ToString() == "CO1" || dt_Tasas_Total.Rows[a]["taxcode"].ToString() == "CO4")
                                dt_Tasas_Total.Rows[a]["amount"] = cargos.ToString();
                            //dt_Tasas_Total.Rows[a]["strnombre_impuesto"] = clsValidaciones.GetKeyOrAdd("NombreImpuetos","Imp y tasas");


                            if (dt_Tasas_Total.Rows[a]["taxcode"].ToString() == "TA")
                                dt_Tasas_Total.Rows[a]["amount"] = TA.ToString();
                            //dt_Tasas_Total.Rows[a]["strnombre_impuesto"] = clsValidaciones.GetKeyOrAdd("NombreTA", "Cargos");
                        }

                        for (int b = 0; b < dt_Tasas_Total.Rows.Count; b++)
                        {
                            if (dt_Tasas_Total.Rows[b]["taxcode"].ToString() != "CO1" && dt_Tasas_Total.Rows[b]["taxcode"].ToString() != "TA")
                            {
                                dt_Tasas_Total.Rows[b].Delete();
                            }
                            if (b == dt_Tasas_Total.Rows.Count - 1 && dt_Tasas_Total.Rows.Count > 2)
                            {
                                for (int cont = 0; cont < dt_Tasas_Total.Rows.Count; cont++)
                                {
                                    if (dt_Tasas_Total.Rows.Count > 2)
                                    {
                                        if (dt_Tasas_Total.Rows[cont]["taxcode"].ToString() != "CO1" && dt_Tasas_Total.Rows[cont]["taxcode"].ToString() != "TA" && dt_Tasas_Total.Rows[cont]["taxcode"].ToString() != "CO4")
                                        {
                                            dt_Tasas_Total.Rows[cont].Delete();
                                        }
                                    }
                                }


                            }

                        }

                        var query = (from dInfo in dt_Tasas_Total.AsEnumerable()
                                     select dInfo);

                        dt_Tasas_TotalCopia = query.CopyToDataTable<DataRow>();
                        for (int d = 0; d < dt_Tasas_TotalCopia.Rows.Count; d++)
                        {
                            if (dt_Tasas_TotalCopia.Rows[d]["taxcode"].Equals("CO1") || dt_Tasas_TotalCopia.Rows[d]["taxcode"].Equals("CO4"))
                            {
                                dt_Tasas_TotalCopia.Rows[d]["strnombre_impuesto"] = clsValidaciones.GetKeyOrAdd("NombreImpuestos", "Imp y tasas");
                            }
                            else if (dt_Tasas_TotalCopia.Rows[d]["taxcode"].Equals("TA"))
                            {
                                dt_Tasas_TotalCopia.Rows[d]["strnombre_impuesto"] = clsValidaciones.GetKeyOrAdd("NombreImpuestoTA", "Cargos");
                            }

                        }


                    }
                    if (dt_Tasas_TotalCopia.Rows.Count > 0 && clsValidaciones.GetKeyOrAdd("Sumaimpuestos", "FALSE").ToUpper().Equals("TRUE"))
                    {
                        if (dt_Tasas_TotalCopia.Rows.Count == 2)
                        {
                            dt_Tasas_TotalCopia.Rows[0]["Amount"] = int.Parse(dt_Tasas_TotalCopia.Rows[0]["Amount"].ToString()) + int.Parse(dt_Tasas_TotalCopia.Rows[1]["Amount"].ToString());
                            dt_Tasas_TotalCopia.Rows[1].Delete();

                        }
                        RptImpuestos.DataSource = dt_Tasas_TotalCopia;

                    }
                    else
                    {
                        if (dt_Tasas_Total != null)
                        {
                            string opcion = clsValidaciones.GetKeyOrAdd("CalculoTopFlingt", "1");
                            decimal SumaImpuestos=0;
                            if (opcion.Equals("1"))
                            {
                                for (int i = 0; i < dt_Tasas_Total.Rows.Count; i++)
                                {
                                    if (dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("ITAN") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("TAN") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("IFEENAL") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("FEENAL") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("ITAI") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("TAI") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("IFEEINAL") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("FEEINAL"))
                                    {
                                        SumaImpuestos += Convert.ToDecimal(dt_Tasas_Total.Rows[i]["Amount"].ToString());
                                       
                                    }
                                }
                                 SumaImpuestos += ((Convert.ToDecimal(dtTarifas.Rows[0]["intbasefare"].ToString()) * sComision) / 100);
                                 HttpContext.Current.Session["Sumaimpuestos"] = SumaImpuestos;
                            }
                            else if (opcion.Equals("2"))
                            {
                                for (int i = 0; i < dt_Tasas_Total.Rows.Count; i++)
                                {
                                    if (dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("ITAN") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("TAN") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("ITAI") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("TAI"))
                                    {
                                        SumaImpuestos += Convert.ToDecimal(dt_Tasas_Total.Rows[i]["Amount"].ToString());
                                    }
                                }
                                SumaImpuestos += ((Convert.ToDecimal(dtTarifas.Rows[0]["intbasefare"].ToString()) * sComision) / 100);
                                HttpContext.Current.Session["Sumaimpuestos"] = SumaImpuestos;
                            }
                            else if (opcion.Equals("3"))
                            {
                                for (int i = 0; i < dt_Tasas_Total.Rows.Count; i++)
                                {
                                    if (dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("IFEENAL") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("FEENAL") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("IFEEINAL") || dt_Tasas_Total.Rows[i]["taxcode"].ToString().Equals("FEEINAL"))
                                    {
                                        SumaImpuestos += Convert.ToDecimal(dt_Tasas_Total.Rows[i]["Amount"].ToString());
                                       
                                    }
                                }
                                SumaImpuestos += ((Convert.ToDecimal(dtTarifas.Rows[0]["intbasefare"].ToString()) * sComision) / 100);
                                HttpContext.Current.Session["Sumaimpuestos"] = SumaImpuestos;
                            }
                        
                        }

                        RptImpuestos.DataSource = dt_Tasas_Total;

                    }
                    RptImpuestos.DataBind();
                }
                catch { }
            }
        }
        private void CargarImpuestosCotiza(Repeater RptTarifas, DataTable dtTarifas)
        {           
            for (int c = 0; c < dtTarifas.Rows.Count; c++)
            {
                try
                {
                    Repeater RptImpuestos = RptTarifas.Items[c].FindControl("RptImpuestos") as Repeater;
                    DataTable dt_Tasas = GetDtPassengerFareTaxCotiza(dtTarifas.Rows[c]["PassengerFare_Id"].ToString());
                    DataTable dt_Tasas_Total = Filtrar_Impuestos(dt_Tasas);
                    RptImpuestos.DataSource = dt_Tasas_Total;
                    RptImpuestos.DataBind();           

                }
                catch { }
            }
            
        }
        private static DataTable Filtrar_Impuestos(DataTable dt_Tasas)
        {
            /*AGRUPAMOS LOS IMPUESTOS QUE DICEN 'OTROS'*/
            DataView dtv_Filtro_Otros = new DataView(dt_Tasas);
            DataView dtvs_Filtro_Tasas = new DataView(dt_Tasas);
            dtv_Filtro_Otros.RowFilter = "strNombre_Impuesto = 'OTROS'";
            DataTable dt_Tasas_Otros = dtv_Filtro_Otros.ToTable();
            try
            {
                dt_Tasas_Otros.Columns.Add("dblSumTotal").Expression = "Sum(Amount)";
            }
            catch { }
            dtvs_Filtro_Tasas.RowFilter = "strNombre_Impuesto <> 'OTROS'";
            DataTable dt_Tasas_Total = dtvs_Filtro_Tasas.ToTable();
            if (dt_Tasas_Otros.Rows.Count > 0)
            {
                DataRow drFila = dt_Tasas_Total.NewRow();
                drFila["TaxCode"] = dt_Tasas_Otros.Rows[0]["TaxCode"].ToString();
                drFila["Amount"] = dt_Tasas_Otros.Rows[0]["dblSumTotal"].ToString();
                drFila["CurrencyCode"] = dt_Tasas_Otros.Rows[0]["CurrencyCode"].ToString();
                drFila["DecimalPlaces"] = dt_Tasas_Otros.Rows[0]["DecimalPlaces"].ToString();
                drFila["Taxes_Id"] = dt_Tasas_Otros.Rows[0]["Taxes_Id"].ToString();
                drFila["strNombre_Impuesto"] = dt_Tasas_Otros.Rows[0]["strNombre_Impuesto"].ToString();
                dt_Tasas_Total.Rows.Add(drFila);
            }
            return dt_Tasas_Total;
        }
        private static DataTable Filtrar_ImpuestosCotiza(DataTable dt_Tasas)
        {
            /*AGRUPAMOS LOS IMPUESTOS QUE DICEN 'OTROS'*/
            try
            {
                dt_Tasas.Columns.Add("dblSumTotal").Expression = "Sum(Amount)";
            }
            catch { }
            return dt_Tasas;
        }
        public DataTable CrearTablaTax()
        {
            const string TABLA_TAX = "Tax";

            const string COLUMN_CODE = "TaxCode";
            const string COLUMN_AMOUNT = "Amount";
            const string COLUMN_CURRENCY_CODE = "CurrencyCode";
            const string COLUMN_DECIMAL_PLACES = "DecimalPlaces";
            const string COLUMN_TAX_TEXT = "Tax_text";
            const string COLUMN_TAXES_ID = "Taxes_Id";
            const string COLUMN_TAX_AMOUNT_USD = "Tax_Amount_Usd";
            //const string COLUMN_NOMBRE_IMPUESTO = "strNombreImpuesto";

            DataTable tblTax = new DataTable(TABLA_TAX);

            tblTax.Columns.Add(COLUMN_CODE, typeof(string));
            tblTax.Columns.Add(COLUMN_AMOUNT, typeof(decimal));
            tblTax.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
            tblTax.Columns.Add(COLUMN_DECIMAL_PLACES, typeof(decimal));
            tblTax.Columns.Add(COLUMN_TAX_TEXT, typeof(string));
            tblTax.Columns.Add(COLUMN_TAXES_ID, typeof(int));
            tblTax.Columns.Add(COLUMN_TAX_AMOUNT_USD, typeof(decimal));
            //try
            //{
            //    tblTax.Columns.Add(COLUMN_NOMBRE_IMPUESTO, typeof(string));
            //}
            //catch { }
            tblTax.AcceptChanges();
            return tblTax;
        }
        public DataTable CrearTablaTaxCotiza()
        {
            const string TABLA_TAX = "Tax";

            const string COLUMN_CODE = "TaxCode";
            const string COLUMN_AMOUNT = "Amount";
            const string COLUMN_NAME = "TaxName";
            const string COLUMN_DECIMAL_PLACES = "DecimalPlaces";
            const string COLUMN_TAX_TEXT = "Tax_text";
            const string COLUMN_TAXES_ID = "Taxes_Id";
            const string COLUMN_TAX_AMOUNT_USD = "Tax_Amount_Usd";
            const string COLUMN_CURRENCY_CODE = "CurrencyCode";
            //const string COLUMN_NOMBRE_IMPUESTO = "strNombreImpuesto";

            DataTable tblTax = new DataTable(TABLA_TAX);

            tblTax.Columns.Add(COLUMN_CODE, typeof(string));
            tblTax.Columns.Add(COLUMN_AMOUNT, typeof(decimal));
            tblTax.Columns.Add(COLUMN_NAME, typeof(string));
            tblTax.Columns.Add(COLUMN_DECIMAL_PLACES, typeof(decimal));
            tblTax.Columns.Add(COLUMN_TAX_TEXT, typeof(string));
            tblTax.Columns.Add(COLUMN_TAXES_ID, typeof(int));
            tblTax.Columns.Add(COLUMN_TAX_AMOUNT_USD, typeof(decimal));
            tblTax.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
            //try
            //{
            //    tblTax.Columns.Add(COLUMN_NOMBRE_IMPUESTO, typeof(string));
            //}
            //catch { }
            tblTax.AcceptChanges();
            return tblTax;
        }
        public DataTable ConsultarAeropuerto(string sPais)
        {
            DataTable dtpais = new DataTable();
            try
            {
                string sConsulta = "EXEC SPCONSULTAAEROPUERTO " + sPais;
                dtpais = new CsConsultasVuelos().Consultatabla(sConsulta);

            }
            catch( Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "ConsultarPais";
                ExceptionHandled.Publicar(cParametros);
            }
            return dtpais; 
        }
        #endregion

        #region [RESERVA]

        /*METODOS PARA LA PAGINA DE RESERVAS DE VUELOS*/
        public bool InicializarPaginaReserva(UserControl ucControl, List<VO_Passenger> objPasajeros)
        {
            this.objPasajeros = objPasajeros;
            clsParametros cParametros = new clsParametros();
            bool bValidacion = true;
            try
            {
                if (ucControl.Request.QueryString["ITIID"] != null && !ucControl.Request.QueryString["ITIID"].Equals(string.Empty))
                {
                    /*ENCONTRAMOS LOS DATALIST Y REPETIDORES*/

                    DataList dtlPasajeros = (DataList)ucControl.FindControl("dtlPasajeros");
                    Repeater rptItinerario = (Repeater)ucControl.FindControl("rptItinerario");
                    /*NOMBRE DE USUARIO  Y TOTAL*/
                    Label lblNombreUsuario = ucControl.FindControl("lblNombreUsuario") as Label;
                    Label lblTotal = ucControl.FindControl("lblTotal") as Label;
                    Label lblMonedaTotal = ucControl.FindControl("lblMonedaTotal") as Label;
                    Label lblTotalTiquete = ucControl.FindControl("lblValTiquete") as Label;
                    clsCache cCache = new csCache().cCache();

                    string sRph = ucControl.Request.QueryString["ITIID"];
                    string sTipoWs = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
                    try
                    {
                       
                        string sWhere = "SequenceNumber = " + sRph;
                        DataSet dsSabreAir = clsSesiones.GetDatasetSabreAir();
                        DataTable dtPricedItinerary = dsSabreAir.Tables["PricedItinerary"];
                        DataTable dtTablaRetornada = clsDataNet.dsDataWhere(sWhere, dtPricedItinerary);
                        sRph = dtTablaRetornada.Rows[0]["PricedItinerary_Id"].ToString();
                        sTipoWs = dtTablaRetornada.Rows[0][COLUMN_WEBSERVICES].ToString();
                        
                    }
                    catch { }
                    if (lblTotal == null)
                        lblTotal = ucControl.FindControl("lblTotalCarrito") as Label;

                    if (lblNombreUsuario != null)
                        lblNombreUsuario.Text = cCache.Empresa;// (ucControl).Nombre;

                    if (rptItinerario != null)
                    {
                        DataTable dtItinerario = GetDtGetItinerario(sRph);

                        if (dtItinerario != null && dtItinerario.Rows.Count > 0)
                        {
                            rptItinerario.DataSource = dtItinerario;
                            rptItinerario.DataBind();
                            /*CONDICIONES*/
                            //Load_Condiciones_Reserva(rptItinerario, sTipoWs, ucControl);
                            /*TOTAL EN PESOS*/
                            if (lblTotal != null)
                                lblTotal.Text = Convert.ToDecimal(dtItinerario.Rows[0]["IntTotalPesos"].ToString()).ToString("###,###.##");
                            /*TOTAL EN PESOS IROTAMA*/
                            if (lblTotalTiquete != null)
                                lblTotalTiquete.Text = Convert.ToDecimal(dtItinerario.Rows[0]["IntTotalPesos"].ToString()).ToString("C");
                            /*TIPO MONEDA*/
                            if (lblMonedaTotal != null)
                                lblMonedaTotal.Text = dtItinerario.Rows[0]["str_Tipo_Moneda"].ToString();

                            DataTable dtPassengerQuantity = GetDtPassengerTypeQuantity(sRph);
                            DataTable dtSegmento = CargarSegmentos(rptItinerario, dtItinerario.Rows[0]["PricedItinerary_Id"].ToString(), ucControl);

                            /*CARGAMOS LOS CRITERIOS DE BUSQUEDA*/
                            HttpContext.Current.Session["$tsegmentos"] = dtSegmento;
                            CargarCriteriosBusqueda(ucControl, dtSegmento, dtPassengerQuantity);

                            CargarTiposPasajeros(rptItinerario, dtItinerario.Rows[0]["PricedItinerary_Id"].ToString());

                            CargarPasajeros(dtItinerario.Rows[0]["PricedItinerary_Id"].ToString(), dtlPasajeros);
                        }
                        else
                        {
                            bValidacion = false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                ExceptionHandled.Publicar(cParametros);
            }
            return bValidacion;
        }       
        public void InicializarPaginaReservaCotiza(UserControl ucControl, List<VO_Passenger> objPasajeros)
        {
            this.objPasajeros = objPasajeros; 

            /*ENCONTRAMOS LOS DATALIST Y REPETIDORES*/
            Repeater rptItinerario = ucControl.FindControl("rptItinerario") as Repeater;
            DataList dtlPasajeros = ucControl.FindControl("dtlPasajeros") as DataList;
            /*NOMBRE DE USUARIO  Y TOTAL*/
            Label lblNombreUsuario = ucControl.FindControl("lblNombreUsuario") as Label;
            Label lblTotal = ucControl.FindControl("lblTotal") as Label;
            Label lblMonedaTotal = ucControl.FindControl("lblMonedaTotal") as Label;
            Label lblTotalTiquete = ucControl.FindControl("lblValTiquete") as Label;
            DataSet dsData = clsSesiones.GetDatasetSelectSabreAir();
            DataSet dsDtSabre = clsSesiones.GetDatasetSabreAir();

            clsCache cCache = new csCache().cCache();
            if (lblTotal == null)
                lblTotal = ucControl.FindControl("lblTotalCarrito") as Label;

            if (lblNombreUsuario != null)
                lblNombreUsuario.Text = cCache.Empresa;
            string sTipoWs = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");

            if (rptItinerario != null)
            {
                DataTable dtItinerario = dsDtSabre.Tables["PricedItinerary"]; ;

                if (dtItinerario != null && dtItinerario.Rows.Count > 0)
                {
                    rptItinerario.DataSource = dtItinerario;
                    rptItinerario.DataBind();
                    /*CONDICIONES*/
                    //Load_Condiciones_Reserva(rptItinerario, sTipoWs, ucControl);
                    /*TOTAL EN PESOS*/
                    if (lblTotal != null)
                        lblTotal.Text = Convert.ToDecimal(dtItinerario.Rows[0]["IntTotalPesos"].ToString()).ToString("###,###.##");
                    /*TOTAL EN PESOS IROTAMA*/
                    if (lblTotalTiquete != null)
                        lblTotalTiquete.Text = Convert.ToDecimal(dtItinerario.Rows[0]["IntTotalPesos"].ToString()).ToString("C");
                    /*TIPO MONEDA*/
                    if (lblMonedaTotal != null)
                        lblMonedaTotal.Text = dtItinerario.Rows[0]["str_Tipo_Moneda"].ToString();

                    DataTable dtPassengerQuantity = GetDtPassengerTypeQuantity();
                    DataTable dtSegmento = CargarSegmentosCotiza(rptItinerario, ucControl);

                    /*CARGAMOS LOS CRITERIOS DE BUSQUEDA*/
                    CargarCriteriosBusqueda(ucControl, dtSegmento, dtPassengerQuantity);

                    CargarTiposPasajeros(rptItinerario);

                    CargarPasajerosCotiza(dtlPasajeros);
                }
            }
        }
       
        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="ucControl"></param>
        /// <param name="dtSegmento"></param>
        /// <param name="dtPassengerQuantity"></param>
        private void CargarCriteriosBusqueda(UserControl ucControl, DataTable dtSegmento, DataTable dtPassengerQuantity)
        {
            Label lblAdultos = ucControl.FindControl("lblAdultos") as Label;
            Label lblninos = ucControl.FindControl("lblninos") as Label;
            Label lblInfates = ucControl.FindControl("lblInfates") as Label;
            Label lblOrigen = ucControl.FindControl("lblOrigen") as Label;
            Label lblDestino = ucControl.FindControl("lblDestino") as Label;
            Label lblFechaRegreso = ucControl.FindControl("lblFechaRegreso") as Label;
            Label lblFechaSalida = ucControl.FindControl("lblFechaSalida") as Label;
            Label lblHoraRegreso = ucControl.FindControl("lblHoraRegreso") as Label;
            Label lblHoraSalida = ucControl.FindControl("lblHoraSalida") as Label;
           
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
            if (dtSegmento != null && dtPassengerQuantity != null && dtSegmento.Rows.Count > 0)
            {
                if (vo_OTA_AirLowFareSearchLLSRQ.CodigoPlan == null)
                {
                    if (lblAdultos != null)
                    {
                        int iCantAdulto = 0;
                        try
                        {
                            foreach (DataRow drFila in dtPassengerQuantity.Select("strDetalleTipo LIKE 'AD*'"))
                                iCantAdulto += int.Parse(drFila["Quantity"].ToString());
                        }
                        catch
                        {
                            foreach (DataRow drFila in dtPassengerQuantity.Select("Code LIKE 'AD*'"))
                                iCantAdulto += int.Parse(drFila["Quantity"].ToString());
                        }
                        lblAdultos.Text = iCantAdulto.ToString();
                    }
                    if (lblninos != null)
                    {
                        int iCantNino = 0;
                        try
                        {
                            foreach (DataRow drFila in dtPassengerQuantity.Select("strDetalleTipo LIKE 'C*'"))
                                iCantNino += int.Parse(drFila["Quantity"].ToString());
                        }
                        catch
                        {
                            foreach (DataRow drFila in dtPassengerQuantity.Select("Code LIKE 'C*'"))
                                iCantNino += int.Parse(drFila["Quantity"].ToString());
                        }
                        lblninos.Text = iCantNino.ToString();
                    }
                    if (lblInfates != null)
                    {
                        int iCantinfante = 0;
                        try
                        {
                            foreach (DataRow drFila in dtPassengerQuantity.Select("strDetalleTipo = 'INF'"))
                                iCantinfante += int.Parse(drFila["Quantity"].ToString());
                        }
                        catch
                        {
                            foreach (DataRow drFila in dtPassengerQuantity.Select("Code = 'INF'"))
                                iCantinfante += int.Parse(drFila["Quantity"].ToString());
                        }
                        lblInfates.Text = iCantinfante.ToString();
                    }
                }
                else
                {
                   
                    string sTipoProducto = clsValidaciones.GetKeyOrAdd("Producto", "ProductoID");
                    string sProductoId = clsValidaciones.GetKeyOrAdd("ProductoRelacionOfertasWS", "tblOfertasWS");
                    string iProducto = "0";
                    //otblRefere.Get(sTipoProducto, sProductoId);
                    //if (otblRefere.Respuesta)
                    //    iProducto = otblRefere.intidRefere.Value;

                    string sIdioma = clsSesiones.getIdioma();
                    int iAplicacion = clsSesiones.getAplicacion();

                    //DataTable dtData = cPlanes.ConsultarRelacionesPlanPax(vo_OTA_AirLowFareSearchLLSRQ.CodigoPlan, iAplicacion, iProducto, sIdioma);
                    DataTable dtData = null;
                    string sWhere = string.Empty;

                    if (lblAdultos != null)
                    {
                        sWhere = "strRefere = 'ADT'";
                        DataTable dtDataTemp = clsDataNet.dsDataWhere(sWhere, dtData);

                        foreach (DataRow drFila in dtPassengerQuantity.Select("Code LIKE '" + dtDataTemp.Rows[0]["strValor"].ToString() + "'"))
                            lblAdultos.Text = drFila["Quantity"].ToString();
                    }
                    if (lblninos != null)
                    {
                        sWhere = "strRefere = 'CNN'";
                        DataTable dtDataTemp = clsDataNet.dsDataWhere(sWhere, dtData);

                        int iCantNino = 0;
                        try
                        {
                            foreach (DataRow drFila in dtPassengerQuantity.Select("Code LIKE '" + dtDataTemp.Rows[0]["strValor"].ToString() + "'"))
                                iCantNino += int.Parse(drFila["Quantity"].ToString());
                        }
                        catch { }
                        lblninos.Text = iCantNino.ToString();
                    }
                    if (lblInfates != null)
                    {
                        sWhere = "strRefere = 'INF'";
                        DataTable dtDataTemp = clsDataNet.dsDataWhere(sWhere, dtData);

                        foreach (DataRow drFila in dtPassengerQuantity.Select("Code = '" + dtDataTemp.Rows[0]["strValor"].ToString() + "'"))
                            lblInfates.Text = drFila["Quantity"].ToString();
                    }
                }
                if (lblOrigen != null)
                {
                    lblOrigen.Text = new CsConsultasVuelos().ConsultaCodigo(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoOrigen.SCodigo, "AIRLINE", "NAME", "CODE");
                }
                if (lblDestino != null)
                {
                    lblDestino.Text = new CsConsultasVuelos().ConsultaCodigo(vo_OTA_AirLowFareSearchLLSRQ.Lvo_Rutas[0].Vo_AeropuertoDestino.SCodigo, "AIRLINE", "NAME", "CODE");
                }
                if (lblFechaSalida != null)
                    lblFechaSalida.Text = clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(dtSegmento.Rows[0]["DepartureDateTime"]).ToString("yyyy/MM/dd"), "-");

                if (lblFechaRegreso != null)
                    lblFechaRegreso.Text = clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(dtSegmento.Rows[dtSegmento.Rows.Count - 1]["ArrivalDateTime"]).ToString("yyyy/MM/dd"), "-");

                if (lblHoraSalida != null)
                    lblHoraSalida.Text = Convert.ToDateTime(dtSegmento.Rows[0]["DepartureDateTime"]).ToLongTimeString();

                if (lblHoraRegreso != null)
                    lblHoraRegreso.Text = Convert.ToDateTime(dtSegmento.Rows[dtSegmento.Rows.Count - 1]["ArrivalDateTime"]).ToLongTimeString();

            }
        }
        private DataTable CargarSegmentos(Repeater rptItinerario, string PricedItinerary_Id, UserControl PageSource)
        {
            DataTable dtSegmento = new DataTable();
            for (int c = 0; c < rptItinerario.Items.Count; c++)
            {
                Repeater rptDetalleSegmento = rptItinerario.Items[c].FindControl("rptDetalle") as Repeater;
                try
                {
                    dtSegmento = GetDtFlightSegmento(PricedItinerary_Id);
                }
                catch
                {
                }

                Label lblRecord = null;
                Label lblFechaLimiteTiqueteo = null;
                if (dtSegmento != null &&
                    dtSegmento.Rows.Count != 0)
                {
                    
                    lblRecord = PageSource.FindControl("lblRecord") as Label;                   
                    lblFechaLimiteTiqueteo = PageSource.FindControl("lblFechaLimiteTiqueteo") as Label;
                }

                if (lblRecord != null)
                {
                    lblRecord.Text = rptItinerario.Page.Request.QueryString["RECORD"];
                }
                try
                {/*ESTABLECEMOS LA FECHA LIMITE DE TIQUETEO*/
                    if (lblFechaLimiteTiqueteo != null)
                    {
                        DateTime dtFecha = clsSesiones.GET_TICKETE();
                        string sFormato = clsValidaciones.sFormatoFechaBD;
                        lblFechaLimiteTiqueteo.Text = clsValidaciones.ConverYMDtoDMMY(dtFecha.ToString(sFormato), "-");
                        lblFechaLimiteTiqueteo.Text += "  " + dtFecha.ToLongTimeString();
                    }
                }
                catch
                { }

                if (dtSegmento != null && dtSegmento.Rows.Count > 0)
                {
                    rptDetalleSegmento.DataSource = dtSegmento;
                    rptDetalleSegmento.DataBind();
                }
            }
            return dtSegmento;
        }
        private DataTable CargarSegmentosCotiza(Repeater rptItinerario, UserControl PageSource)
        {
            DataSet dsData = clsSesiones.GetDatasetSelectSabreAir();
            DataTable dtSegmento = new DataTable();
            for (int c = 0; c < rptItinerario.Items.Count; c++)
            {
                Repeater rptDetalleSegmento = rptItinerario.Items[c].FindControl("rptDetalle") as Repeater;
                dtSegmento = dsData.Tables["FlightSegment"];
              
                Label lblRecord = PageSource.FindControl("lblRecord") as Label;
               
                Label lblFechaLimiteTiqueteo = PageSource.FindControl("lblFechaLimiteTiqueteo") as Label;

                if (lblRecord != null)
                {
                    lblRecord.Text = rptItinerario.Page.Request.QueryString["RECORD"];
                }
                try
                {/*ESTABLECEMOS LA FECHA LIMITE DE TIQUETEO*/
                    if (lblFechaLimiteTiqueteo != null)
                    {
                        lblFechaLimiteTiqueteo.Text = clsValidaciones.ConverYMDtoDMMY(clsSesiones.GET_TICKETE().ToString(clsValidaciones.sFormatoFechaBD), "-");
                        lblFechaLimiteTiqueteo.Text += "  " + clsSesiones.GET_TICKETE().ToLongTimeString();
                    }

                }
                catch
                { }

                if (dtSegmento != null && dtSegmento.Rows.Count > 0)
                {
                    rptDetalleSegmento.DataSource = dtSegmento;

                    rptDetalleSegmento.DataBind();
                }
            }
            return dtSegmento;
        }
        private void CargarTiposPasajeros(Repeater rptItinerario, string PricedItinerary_Id)
        {
            for (int c = 0; c < rptItinerario.Items.Count; c++)
            {
                Repeater RptTiposPasajeros = rptItinerario.Items[c].FindControl("RptTiposPasajeros") as Repeater;

                if (RptTiposPasajeros == null)
                    return;

                DataTable dtTiposPasajeros = GetDtPassengerTypeQuantity(PricedItinerary_Id);

                if (dtTiposPasajeros != null && dtTiposPasajeros.Rows.Count > 0)
                {
                    RptTiposPasajeros.DataSource = dtTiposPasajeros;

                    RptTiposPasajeros.DataBind();

                    CargarTarifas(RptTiposPasajeros);
                }
            }
        }
        private void CargarTiposPasajeros(Repeater rptItinerario)
        {
            for (int c = 0; c < rptItinerario.Items.Count; c++)
            {
                Repeater RptTiposPasajeros = rptItinerario.Items[c].FindControl("RptTiposPasajeros") as Repeater;

                if (RptTiposPasajeros == null)
                    return;

                DataTable dtTiposPasajeros = GetDtPassengerTypeQuantity();

                if (dtTiposPasajeros != null && dtTiposPasajeros.Rows.Count > 0)
                {
                    RptTiposPasajeros.DataSource = dtTiposPasajeros;

                    RptTiposPasajeros.DataBind();

                    CargarTarifasCotiza(RptTiposPasajeros);
                }
            }
        }
        private void CargarTiposPasajerosCotiza(Repeater RptTiposPasajeros)
        {
            if (RptTiposPasajeros == null)
                return;

            DataTable dtTiposPasajeros = GetDtPassengerTypeQuantity();

            if (dtTiposPasajeros != null && dtTiposPasajeros.Rows.Count > 0)
            {
                RptTiposPasajeros.DataSource = dtTiposPasajeros;

                RptTiposPasajeros.DataBind();

                CargarTarifasCotiza(RptTiposPasajeros);
            }
        }
        private void CargarTarifas(Repeater RptTiposPasajeros)
        {
            /*TIPOS PAX*/
            DataTable dtTipoPax = RptTiposPasajeros.DataSource as DataTable;

            for (int c = 0; c < dtTipoPax.Rows.Count; c++)
            {
                Repeater RptTarifas = RptTiposPasajeros.Items[c].FindControl("RptTarifas") as Repeater;
                /*PASSENGER FARE*/
                DataTable dtTarifas = GetDtPassengerFare(dtTipoPax.Rows[c]["PTC_FareBreakdown_Id"].ToString());

                if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                {
                    RptTarifas.DataSource = dtTarifas;

                    RptTarifas.DataBind();

                    CargarImpuestos(RptTarifas, dtTarifas.Rows[0]["PassengerFare_Id"].ToString());
                }
            }
        }
        private void CargarTarifasCotiza(Repeater RptTiposPasajeros)
        {
            /*TIPOS PAX*/
            DataTable dtTipoPax = RptTiposPasajeros.DataSource as DataTable;

            for (int c = 0; c < dtTipoPax.Rows.Count; c++)
            {
                Repeater RptTarifas = RptTiposPasajeros.Items[c].FindControl("RptTarifas") as Repeater;
                /*PASSENGER FARE*/
                DataTable dtTarifas = GetDtPassengerFare(dtTipoPax.Rows[c]["PTC_FareBreakdown_Id"].ToString());

                if (dtTarifas != null && dtTarifas.Rows.Count > 0)
                {
                    RptTarifas.DataSource = dtTarifas;

                    RptTarifas.DataBind();

                    CargarImpuestosCotiza(RptTarifas, dtTarifas);
                }
            }
        }
        private void CargarImpuestos(Repeater RptTarifas, string PassengerFare_Id)
        {
            for (int c = 0; c < RptTarifas.Items.Count; c++)
            {
                Repeater RptImpuestos = RptTarifas.Items[c].FindControl("RptImpuestos") as Repeater;
                DataTable dtImpuestos = GetDtPassengerFareTax(PassengerFare_Id);
                if (dtImpuestos != null && dtImpuestos.Rows.Count > 0)
                {
                    DataTable dt_Tasas_Total = Filtrar_Impuestos(dtImpuestos);
                    RptImpuestos.DataSource = dt_Tasas_Total;
                    RptImpuestos.DataBind();
                }
            }
        }
        private void CargarPasajeros(string PricedItinerary_Id, DataList dtlPasajeros)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                DataTable dtPassengerTypeQuantity = GetDtPassengerTypeQuantity(PricedItinerary_Id);

                if (dtlPasajeros == null)
                    return;

                if (dtPassengerTypeQuantity != null)
                {
                    DataTable dtPasajeros = this.dsSabreAir.Tables["dtPasajeros"];
                    /*LIMPIAMOS LOS DATOS DE LA TABLA*/
                    dtPasajeros.Clear();

                    for (int k = 0; k < dtPassengerTypeQuantity.Rows.Count; k++)
                    {
                        int intCantidad = default(int);

                        intCantidad = Convert.ToInt32(dtPassengerTypeQuantity.Rows[k]["Quantity"]);

                        for (int i = 0; i < intCantidad; i++)
                        {
                            /*AGREGAMOS EDADES A PASAJEROS*/
                            DataRow drNewFila = AgregarEdadesPasajeros(dtPassengerTypeQuantity, dtPasajeros, k);
                            /*AGREGAMOS EL TIPO DE PASAJERO Y EL ID DEL LA TABLA PADRE*/
                            drNewFila["strTipoPasajero"] = dtPassengerTypeQuantity.Rows[k]["strTipoPasajero"].ToString();
                            drNewFila["strCode"] = dtPassengerTypeQuantity.Rows[k]["Code"].ToString();
                            drNewFila["strDetalleTipo"] = dtPassengerTypeQuantity.Rows[k]["strDetalleTipo"].ToString();
                            drNewFila["intPTC_FareBreakdown_Id"] = dtPassengerTypeQuantity.Rows[k]["PTC_FareBreakdown_Id"].ToString();
                            dtPasajeros.Rows.Add(drNewFila);
                        }
                    }
                    if (dtPasajeros != null && dtPasajeros.Rows.Count > 0)
                    {
                        /*GUARDAMOS EL DATASET OTRA VEZ*/
                        clsSesiones.SetDatasetSabreAir(dsSabreAir);
                        /*LLENAMOS EL REPETIDOR*/
                        if (dtlPasajeros != null)
                        {
                            dtlPasajeros.DataSource = dtPasajeros;
                            dtlPasajeros.DataBind();
                            /*VALIDACIONES DE PASAJEROS*/
                            if (dtlPasajeros != null && dtPasajeros != null)
                            {
                                ValidacionesPasajeros(dtlPasajeros, dtPasajeros);
                            }
                            else
                            {
                                cParametros.Id = 0;
                                if (dtlPasajeros == null)
                                {
                                    ExceptionHandled.Publicar("dtlPasajeros Datos venian nulos");
                                }
                                else if (dtPasajeros == null)
                                {
                                    ExceptionHandled.Publicar("dtPasajeros Datos venian nulos");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                ExceptionHandled.Publicar(cParametros);
            }
        }
        private void CargarPasajerosCotiza(DataList dtlPasajeros)
        {
            DataTable dtPassengerTypeQuantity = GetDtPassengerTypeQuantity();

            if (dtlPasajeros == null)
                return;

            if (dtPassengerTypeQuantity != null)
            {
                DataTable dtPasajeros = this.dsSabreAir.Tables["dtPasajeros"];
                /*LIMPIAMOS LOS DATOS DE LA TABLA*/
                dtPasajeros.Clear();

                for (int k = 0; k < dtPassengerTypeQuantity.Rows.Count; k++)
                {
                    int intCantidad = default(int);

                    intCantidad = Convert.ToInt32(dtPassengerTypeQuantity.Rows[k]["Quantity"]);

                    for (int i = 0; i < intCantidad; i++)
                    {
                        /*AGREGAMOS EDADES A PASAJEROS*/
                        DataRow drNewFila = AgregarEdadesPasajeros(dtPassengerTypeQuantity, dtPasajeros, k);
                        /*AGREGAMOS EL TIPO DE PASAJERO Y EL ID DEL LA TABLA PADRE*/
                       
                        drNewFila["strTipoPasajero"] = dtPassengerTypeQuantity.Rows[k]["strTipoPasajero"].ToString();
                        drNewFila["strCode"] = dtPassengerTypeQuantity.Rows[k]["Code"].ToString();
                        drNewFila["strDetalleTipo"] = dtPassengerTypeQuantity.Rows[k]["strDetalleTipo"].ToString();
                        drNewFila["intPTC_FareBreakdown_Id"] = dtPassengerTypeQuantity.Rows[k]["PTC_FareBreakdown_Id"].ToString();
                        dtPasajeros.Rows.Add(drNewFila);
                    }
                }
                if (dtPasajeros != null && dtPasajeros.Rows.Count > 0)
                {
                    /*GUARDAMOS EL DATASET OTRA VEZ*/
                    clsSesiones.SetDatasetSabreAir(this.dsSabreAir);
                    /*LLENAMOS EL REPETIDOR*/
                    dtlPasajeros.DataSource = dtPasajeros;
                    dtlPasajeros.DataBind();
                    /*VALIDACIONES DE PASAJEROS*/
                    if (dtlPasajeros != null && dtPasajeros != null)
                    {
                        ValidacionesPasajeros(dtlPasajeros, dtPasajeros);
                    }
                    else
                    {
                        if (dtlPasajeros == null)
                        {
                            ExceptionHandled.Publicar("dtlPasajeros Datos venian nulos");
                        }
                        else if (dtPasajeros == null)
                        {
                            ExceptionHandled.Publicar("dtPasajeros Datos venian nulos");
                        }
                    }
                }
            }
        }
        private void CargarPasajeros(DataList dtlPasajeros)
        {
            dtlPasajeros.DataSource = GetDtPasajeros();

            dtlPasajeros.DataBind();
        }
        private DataRow AgregarEdadesPasajeros(DataTable dtPassengerTypeQuantity, DataTable dtPasajeros, int k)
        {
            clsParametros cParametros = new clsParametros();
            DataRow drNewFila = dtPasajeros.NewRow();
            try
            {
                int iPosPax = objPasajeros.Count;
                if (k > iPosPax)
                    k = 0;
                if (objPasajeros.Count > 0)
                {
                    if (dtPassengerTypeQuantity.Rows[k]["Code"].Equals(objPasajeros[k].Pos))
                    {
                        if (objPasajeros[k].Edad.Count > 0)
                        {
                            drNewFila["intEdad"] = objPasajeros[k].Edad[0].Edad;
                        }
                        else
                        {
                            drNewFila["intEdad"] = 0;
                        }
                    }
                    else
                    {
                        drNewFila["intEdad"] = 0;
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                ExceptionHandled.Publicar(cParametros);
                drNewFila["intEdad"] = 0;
            }
            return drNewFila;
        }
        /// <summary>
        /// metodo pendiente por revision
        /// Metodo que llena los controles del datalist de pasajeros
        /// </summary>
        /// <param name="dtlPasajeros">DataList de pasajeros</param>
        /// <param name="dtPasajeros">DataTable de pasajeros</param>
        /// <remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-08
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          José Faustino Posas
        /// Fecha:          2012-01-03
        /// Descripción:    Se incluye el pasajero frecuente
        /// -------------------
        /// Autor:          Juan Camilo Diaz
        /// Fecha:          2012-04-26
        /// Descripción:    se incluye validacion para valor en blanco de genero en el formulario de reserva
        /// </remarks>
        private void ValidacionesPasajeros(DataList dtlPasajeros, DataTable dtPasajeros)
        {/*METODO QUE HACE LAS VALIDACIONES DE PASAJEROS*/

            /*ENCONTRAMOS EL USERCONTROL*/
            clsParametros cParametros = new clsParametros();
            Utils.Utils cUtil = new Utils.Utils();
            string sTipoSexo = clsValidaciones.GetKeyOrAdd("Sexo", "SX");
            string sAirLine = clsValidaciones.GetKeyOrAdd("AIRLINESABRE", "AIRLINESABRE");
            TextBox txtTelefono2 = null;
            clsCache cCache = new csCache().cCache();
            try
            {
                UserControl ucControl = dtlPasajeros.Parent as UserControl;
                if (ucControl != null)
                {
                    txtTelefono2 = ucControl.FindControl("txtTelefono2") as TextBox;
                }
                if (cCache != null && txtTelefono2 != null)
                {
                    if (cCache.Passenger != null && cCache.Passenger.Count > 0)
                    {
                        txtTelefono2.Text = clsValidaciones.RetornaNumero(cCache.Passenger[0].RespTelefono);
                    }
                    else
                    {
                        txtTelefono2.Text = clsValidaciones.RetornaNumero(cCache.Telefono);
                    }
                }
                else if(txtTelefono2 != null)
                {
                    txtTelefono2.Text = clsValidaciones.RetornaNumero(RecuperarCache(ucControl).Telefono);
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                ExceptionHandled.Publicar(cParametros);
            }
            try
            {

                string sIdioma = string.Empty;

                if (cCache.Idioma != "" && cCache.Idioma != null)
                {
                    sIdioma = cCache.Idioma;
                }
                else
                {
                    sIdioma = clsValidaciones.GetKeyOrAdd("strIdioma", "es");
                
                }

                DataTable dtData = new CsConsultasVuelos().SPConsultaTabla("SPCONSULTAGENERO", new string[1] { sIdioma });
                DataTable dtDataDoc = new CsConsultasVuelos().SPConsultaTabla("SPConsultaTpoidentifica", new string[1] { sIdioma });
                int iPos = 1;
                for (int d = 0; d < dtlPasajeros.Items.Count; d++)
                {
                    TextBox txtEdad1 = dtlPasajeros.Items[d].FindControl("txtEdad1") as TextBox;
                    TextBox txtNombre1 = dtlPasajeros.Items[d].FindControl("txtNombre1") as TextBox;
                    TextBox txtApellido1 = dtlPasajeros.Items[d].FindControl("txtApellido1") as TextBox;
                    TextBox txtTelefono1 = dtlPasajeros.Items[d].FindControl("txtTelefono1") as TextBox;
                    TextBox txtDocumento1 = dtlPasajeros.Items[d].FindControl("txtDocumento1") as TextBox;
                    DropDownList ddlTipoDoc = dtlPasajeros.Items[d].FindControl("ddlTipoDoc") as DropDownList;
                    DropDownList ddlTrato1 = dtlPasajeros.Items[d].FindControl("ddlTrato1") as DropDownList;
                    DropDownList ddlGenero = dtlPasajeros.Items[d].FindControl("ddlGenero") as DropDownList;
                    DropDownList ddlAirline = dtlPasajeros.Items[d].FindControl("ddlAirline") as DropDownList;
                    TextBox txtFee = dtlPasajeros.Items[d].FindControl("txtFee") as TextBox;
                    Label lblFee = dtlPasajeros.Items[d].FindControl(" lblFee") as Label;
                    Literal ltrPosPax = dtlPasajeros.Items[d].FindControl("ltrPosPax") as Literal;

                    TextBox txtDia = dtlPasajeros.Items[d].FindControl("txtDia") as TextBox;
                    TextBox txtMes = dtlPasajeros.Items[d].FindControl("txtMes") as TextBox;
                    TextBox txtYear = dtlPasajeros.Items[d].FindControl("txtYear") as TextBox;

                    if (ltrPosPax != null)
                    {
                        ltrPosPax.Text = "Pasajero " + iPos.ToString();
                    }
                    if (ddlGenero != null)
                    {
                        
                            if (dtData != null)
                            {
                                if (clsValidaciones.GetKeyOrAdd("ValorBlancoGenero", "False").ToUpper().Equals("TRUE"))
                                    clsControls.LlenaControl(ddlGenero, dtData, "STRDESCRIPCION", "INTCODE", true);
                                else
                                    clsControls.LlenaControl(ddlGenero, dtData, "STRDESCRIPCION", "INTCODE", false);
                            }
                        
                    }

                    if (ddlTipoDoc != null)
                    {
                        if (dtDataDoc != null)
                        {
                            if (clsValidaciones.GetKeyOrAdd("ValorBlancoDoc", "False").ToUpper().Equals("TRUE"))
                                clsControls.LlenaControl(ddlTipoDoc, dtDataDoc, "STRDESCRIPCION", "INTCODE", true);
                            else
                                clsControls.LlenaControl(ddlTipoDoc, dtDataDoc, "STRDESCRIPCION", "INTCODE", false);
                        
                        }
                     
                    }
                
                    /*ESTABLECEMOS LOS DATOS DEL PRIMER PASAJERO CON LOS DATOS DE LA SESSION*/
                    if (d == 0)
                    {
                        if (cCache != null)
                        {
                            if (cCache.Passenger != null && cCache.Passenger.Count > 0)
                            {
                                txtNombre1.Text = cCache.Passenger[0].RespNombre;
                                txtApellido1.Text = cCache.Passenger[0].RespApellido;
                                if (txtDocumento1 != null)
                                    txtDocumento1.Text = cCache.Passenger[0].RespDocumento;
                                if (ddlTipoDoc != null)
                                    ddlTipoDoc.SelectedValue = cCache.Passenger[0].RespTipoDoc;
                                if (ddlGenero != null)
                                    ddlGenero.SelectedValue = cCache.Passenger[0].RespGenero;
                                if (txtYear != null)
                                {
                                    string[] sFecha = clsValidaciones.FechaPax(cCache.Passenger[0].RespFechaNac);
                                    txtYear.Text = sFecha[0];
                                    txtMes.Text = sFecha[1];
                                    txtDia.Text = sFecha[2];
                                }
                                else
                                {
                                    if (txtEdad1 != null)
                                    {
                                        txtEdad1.Text = cCache.Passenger[0].RespFechaNac;
                                    }
                                }
                            }
                            else
                            {
                                if (cCache.Viajero != "0")
                                {
                                    //tblContactos otblContactos = new tblContactos();
                                    //otblContactos.Get(cCache.Viajero);
                                    if (cCache.Viajero != "0")
                                    {
                                        //txtNombre1.Text = otblContactos.strNombre.Value;
                                        //txtApellido1.Text = otblContactos.strApellido.Value;
                                        if (txtDocumento1 != null)
                                            //txtDocumento1.Text = otblContactos.strIdentificacion.Value;
                                        if (ddlTipoDoc != null)
                                            //ddlTipoDoc.SelectedValue = otblContactos.intTipoIdentifica.Value;
                                        if (ddlGenero != null)
                                        {

                                            if (dtData != null)
                                            {
                                                if (dtData.Rows.Count > 0)
                                                    ddlGenero.SelectedValue = dtData.Rows[0][0].ToString();
                                            }
                                        }
                                        if (txtYear != null)
                                        {
                                            //string[] sFecha = clsValidaciones.FechaPax(otblContactos.dtmFechaNac.Value, sFormatoFecha);
                                            //txtYear.Text = sFecha[0];
                                            //txtMes.Text = sFecha[1];
                                            //txtDia.Text = sFecha[2];
                                        }
                                        else
                                        {
                                            if (txtEdad1 != null)
                                            {
                                                //txtEdad1.Text = clsValidaciones.ConverFecha(otblContactos.dtmFechaNac.Value, sFormatoFechaBD, sFormatoFecha);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        txtNombre1.Text = cCache.Nombres;
                                        txtApellido1.Text = cCache.Apellidos;
                                        if (txtDocumento1 != null)
                                            txtDocumento1.Text = cCache.Identificacion;
                                        if (ddlTipoDoc != null)
                                            ddlTipoDoc.SelectedValue = cCache.TipoIdentificacion;
                                        if (txtYear != null)
                                        {
                                            string[] sFecha = clsValidaciones.FechaPax(cCache.FechaNac);
                                            txtYear.Text = sFecha[0];
                                            txtMes.Text = sFecha[1];
                                            txtDia.Text = sFecha[2];
                                        }
                                        else
                                        {
                                            if (txtEdad1 != null)
                                            {
                                                txtEdad1.Text = clsValidaciones.ConverFecha(cCache.FechaNac, sFormatoFechaBD, sFormatoFecha);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    txtNombre1.Text = cCache.Nombres;
                                    txtApellido1.Text = cCache.Apellidos;
                                    if (txtDocumento1 != null)
                                        txtDocumento1.Text = cCache.Identificacion;
                                    if (ddlTipoDoc != null)
                                        ddlTipoDoc.SelectedValue = cCache.TipoIdentificacion;
                                    if (txtYear != null)
                                    {
                                        string[] sFecha = clsValidaciones.FechaPax(cCache.FechaNac);
                                        txtYear.Text = sFecha[0];
                                        txtMes.Text = sFecha[1];
                                        txtDia.Text = sFecha[2];
                                    }
                                    else
                                    {
                                        if (txtEdad1 != null)
                                        {
                                            txtEdad1.Text = clsValidaciones.ConverFecha(cCache.FechaNac, sFormatoFechaBD, sFormatoFecha);
                                        }
                                    }
                                }
                            }
                        }
                        try
                        {
                            string sTipoPagina = clsValidaciones.GetKeyOrAdd("Cliente", "UF");
                            if (sTipoPagina.ToUpper().Equals("TOKEN"))
                            {

                                if (txtDocumento1 != null)
                                    txtDocumento1.Text = string.Empty;
                                if (txtEdad1 != null)
                                    txtEdad1.Text = sFormatoFecha.ToUpper();
                            }
                        }
                        catch { }
                    }
                    if (d > 0)
                    {
                        if (dtPasajeros.Rows[d]["strTipoPasajero"].ToString().Equals(dtPasajeros.Rows[d - 1]["strTipoPasajero"].ToString()))
                        {
                            if (lblFee != null)
                                lblFee.Visible = false;
                            if (txtFee != null)
                                txtFee.Visible = false;
                        }
                    }
                    /*ESTABLECEMOS LOS DATOS DEL LOS NIÑOS*/
                    if (ddlTrato1 != null)
                    {
                        if (dtPasajeros.Rows[d]["strTipoPasajero"].ToString().Equals("CNN"))
                        {
                            ddlTrato1.Enabled = false;
                        }/*ESTABLECEMOS LOS DATOS DEL LOS INFANTES*/
                        if (dtPasajeros.Rows[d]["strTipoPasajero"].ToString().Equals("INF"))
                        {
                            ddlTrato1.Enabled = false;
                        }
                        else
                        {
                            ddlTrato1.Enabled = true;
                        }
                        if (ddlGenero != null)
                        {
                            if (ddlGenero.SelectedValue.ToString().Contains("F"))
                            {
                                ddlTrato1.Items[1].Selected = true;
                            }
                            else
                            {
                                ddlTrato1.Items[0].Selected = true;
                            }
                        }
                    }
                    iPos++;
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public DataTable GetDtPasajeros()
        {
            DataTable dtPasajeros = new DataTable();

            if (dsSabreAir.Tables.Count == 0)
            {
                this.dsSabreAir = clsSesiones.GetDatasetSabreAir();
            }

            if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
            {
                dtPasajeros = this.dsSabreAir.Tables["dtPasajeros"];
            }
            return dtPasajeros;
        }
        public void GuardarDatosPasajeros(UserControl ucControl)
        {
            DataList dtlPasajeros = ucControl.FindControl("dtlPasajeros") as DataList;
            UserControl ucregistro = (UserControl)ucControl.FindControl("ucregistro");            
            TextBox txtemail =null;
            TextBox txtTelefono = null;

            if (ucregistro != null)
            {
                txtemail = (TextBox)ucregistro.FindControl("txtMailPersonal");
                txtTelefono = (TextBox)ucregistro.FindControl("txtTelefono");
            }
           
            Utils.Utils oUtilidad = new Utils.Utils();
            TextBox txtTelefono2 = ucControl.FindControl("txtTelefono2") as TextBox;
            List<VO_Passenger> lvo_Passenger = new List<VO_Passenger>();
            if (dsSabreAir.Tables.Count == 0)
            {
                this.dsSabreAir = clsSesiones.GetDatasetSabreAir();
            }
            if (dsSabreAir != null && dsSabreAir.Tables.Count > 0)
            {
                DataTable dtPasajeros = this.dsSabreAir.Tables["dtPasajeros"];
                int iPos = 1;
                for (int d = 0; d < dtlPasajeros.Items.Count; d++)
                {
                    VO_Passenger vo_Passenger = new VO_Passenger();
                    vo_Passenger.Pos = iPos.ToString();
                    TextBox txtNombre1 = dtlPasajeros.Items[d].FindControl("txtNombre1") as TextBox;
                    TextBox txtApellido1 = dtlPasajeros.Items[d].FindControl("txtApellido1") as TextBox;
                    TextBox txtTelefono1 = dtlPasajeros.Items[d].FindControl("txtTelefono1") as TextBox;
                    TextBox txtEdad1 = dtlPasajeros.Items[d].FindControl("txtEdad1") as TextBox;
                    TextBox txtFechaNacimiento1 = dtlPasajeros.Items[d].FindControl("txtFechaNacimiento") as TextBox;
                    TextBox txtPasajeroFrec1 = dtlPasajeros.Items[d].FindControl("txtPasajeroFrec") as TextBox;
                    TextBox txtDocumento = dtlPasajeros.Items[d].FindControl("txtDocumento") as TextBox;
                    TextBox txtDocumento1 = dtlPasajeros.Items[d].FindControl("txtDocumento1") as TextBox;
                    TextBox txtFee = dtlPasajeros.Items[d].FindControl("txtFee") as TextBox;
                    DropDownList ddlTrato1 = dtlPasajeros.Items[d].FindControl("ddlTrato1") as DropDownList;
                    DropDownList ddlTipoDoc1 = dtlPasajeros.Items[d].FindControl("ddlTipoDoc") as DropDownList;
                    DropDownList ddlGenero1 = dtlPasajeros.Items[d].FindControl("ddlGenero") as DropDownList;
                    DropDownList ddlAirline = dtlPasajeros.Items[d].FindControl("ddlAirline") as DropDownList;
                    TextBox txtDia = dtlPasajeros.Items[d].FindControl("txtDia") as TextBox;
                    TextBox txtMes = dtlPasajeros.Items[d].FindControl("txtMes") as TextBox;
                    TextBox txtYear = dtlPasajeros.Items[d].FindControl("txtYear") as TextBox;

                    /*GUARDAMOS TELEFONO,NOMBRE Y APELLIDO DATO DEL FORMULARIO*/
                    string sFechaNac = string.Empty;
                    string sNombre = string.Empty;
                    sNombre = clsValidaciones.CambiarCaracter(txtNombre1.Text);
                    string sApellido = clsValidaciones.CambiarCaracter(txtApellido1.Text);
                    if (sNombre.Contains(" "))
                    {
                        int iPosCont = sNombre.IndexOf(" ");
                        sNombre = sNombre.Substring(0, iPosCont);
                    }
                  

                    if (txtTelefono2 != null)
                        dtPasajeros.Rows[d]["strTelefono"] = clsValidaciones.RetornaNumero(txtTelefono2.Text.TrimEnd());
                    else
                        dtPasajeros.Rows[d]["strTelefono"] = "";
                    dtPasajeros.Rows[d]["strPrimerNombre"] = sNombre.TrimEnd();
                    dtPasajeros.Rows[d]["strPrimerApellido"] = sApellido.TrimEnd();
                    dtPasajeros.Rows[d]["strTrato"] = ddlTrato1.SelectedItem.Text.TrimEnd();
                    dtPasajeros.Rows[d]["strEmail"] = RecuperarCache(ucControl).User.TrimEnd();
                    if (txtemail != null)
                    {
                        dtPasajeros.Rows[d]["strEmail"] = txtemail.Text;
                    }
                    ExceptionHandled.Publicar(dtPasajeros.Rows[d]["strEmail"].ToString()+"jsn");
                   

                    if (txtTelefono2 != null)
                        vo_Passenger.RespTelefono = clsValidaciones.RetornaNumero(txtTelefono2.Text.TrimEnd());
                    else
                        vo_Passenger.RespTelefono = "";

                    vo_Passenger.RespNombre = sNombre.TrimEnd();
                    vo_Passenger.RespApellido = sApellido.TrimEnd();
                    vo_Passenger.RespTrato = ddlTrato1.SelectedItem.Text.TrimEnd();
                    vo_Passenger.RespEmail = RecuperarCache(ucControl).User.TrimEnd();

                    if (txtTelefono1 != null)
                    {
                        dtPasajeros.Rows[d]["strTelefono"] = clsValidaciones.RetornaNumero(txtTelefono1.Text.TrimEnd());
                        vo_Passenger.RespTelefono = clsValidaciones.RetornaNumero(txtTelefono1.Text.TrimEnd());
                    }
                    else if (txtTelefono != null)
                    {
                        dtPasajeros.Rows[d]["strTelefono"] = clsValidaciones.RetornaNumero(txtTelefono.Text.TrimEnd());
                        vo_Passenger.RespTelefono = clsValidaciones.RetornaNumero(txtTelefono.Text.TrimEnd());
                    }

                    if (txtFechaNacimiento1 != null)
                    {
                        sFechaNac = clsValidaciones.ConverFecha(txtFechaNacimiento1.Text.Trim(), sFormatoFecha, "/", Enum_FormatoFecha.YMD, "/");
                        sFechaNac = clsValidaciones.setFechaSabreNac(sFechaNac);
                        dtPasajeros.Rows[d]["strFechaNacimiento"] = sFechaNac;
                        vo_Passenger.RespFechaNac = sFechaNac;
                    }
                    if (txtYear != null)
                    {
                        if (!txtYear.Text.Length.Equals(0))
                        {
                            string sYear = txtYear.Text;
                            string sMes = txtMes.Text;
                            string sDia = txtDia.Text;

                            if (sMes.Length.Equals(1))
                                sMes = "0" + sMes;

                            if (sDia.Length.Equals(1))
                                sDia = "0" + sDia;

                            sFechaNac = sYear + "/" + sMes + "/" + sDia;
                            sFechaNac = clsValidaciones.setFechaSabreNac(sFechaNac);
                        }
                        dtPasajeros.Rows[d]["strFechaNacimiento"] = sFechaNac;
                        vo_Passenger.RespFechaNac = sFechaNac;
                    }
                    if (txtEdad1 != null)
                    {
                        sFechaNac = clsValidaciones.ConverFecha(txtEdad1.Text, sFormatoFecha, Enum_FormatoFecha.YMD);
                        sFechaNac = clsValidaciones.setFechaSabreNac(sFechaNac);
                        dtPasajeros.Rows[d]["strFechaNacimiento"] = sFechaNac;

                      
                    }
                    if (ddlGenero1 != null)
                    {
                        dtPasajeros.Rows[d]["strGenero"] = ddlGenero1.SelectedItem.Value.Trim();
                        vo_Passenger.RespGenero = ddlGenero1.SelectedItem.Value.Trim();
                    }
                    if (ddlTipoDoc1 != null)
                    {
                        dtPasajeros.Rows[d]["strTipoDocumento"] = ddlTipoDoc1.SelectedItem.Value.Trim();
                        vo_Passenger.RespTipoDoc = ddlTipoDoc1.SelectedItem.Value.Trim();
                    }
                    if (txtPasajeroFrec1 != null)
                    {
                        dtPasajeros.Rows[d]["strPasajeroFrecuente"] = txtPasajeroFrec1.Text.Trim();
                        vo_Passenger.RespFrecuente = txtPasajeroFrec1.Text.Trim();
                    }
                    if (ddlAirline != null)
                    {
                        dtPasajeros.Rows[d]["strAerolinea"] = ddlAirline.SelectedItem.Value.Trim();
                        vo_Passenger.RespAirLine = ddlAirline.SelectedItem.Value.Trim();
                    }

                    if (txtDocumento != null)
                    {
                        dtPasajeros.Rows[d]["strDocumento"] = txtDocumento.Text.Trim();
                        vo_Passenger.RespDocumento = txtDocumento.Text.Trim();
                    }
                    if (txtDocumento1 != null)
                    {
                        dtPasajeros.Rows[d]["strDocumento"] = txtDocumento1.Text.Trim();
                        vo_Passenger.RespDocumento = txtDocumento1.Text.Trim();
                    }
                    if (txtFee != null)
                        dtPasajeros.Rows[d]["strFee"] = clsValidaciones.CambiarCaracter(txtFee.Text);

                    lvo_Passenger.Add(vo_Passenger);
                    iPos++;
                }
                try
                {
                    csCache.GuardarPaxCache(lvo_Passenger);
                }
                catch { }
                /*GUARDAMOS EL DATASET MODIFICADO*/
                clsSesiones.SetDatasetSabreAir(dsSabreAir);
            }
        }
        public clsCache RecuperarCache(UserControl ucControl)
        {
            return new csCache().cCache();
        }
        public string GetPrecioDolar()
        {
            String strUsdIata = string.Empty;
            DataSet dstDatos = new DataSet();

            StringBuilder strConsulta = new StringBuilder();

            strConsulta.AppendLine("SELECT DAY(dtmFechaUsd) AS DIA,");
            strConsulta.AppendLine("MONTH(dtmFechaUsd) AS MES,");
            strConsulta.AppendLine("YEAR(dtmFechaUsd) AS ANIO,* FROM dbo.tblUsdIata");
            strConsulta.AppendLine("WHERE   DAY(dtmFechaUsd) = DAY(GETDATE())");
            strConsulta.AppendLine("AND MONTH(dtmFechaUsd) =  MONTH(GETDATE())");
            strConsulta.AppendLine("AND YEAR(dtmFechaUsd) =  YEAR(GETDATE())");
            try
            {
                dstDatos = sdSql.Select(strConsulta.ToString());
                strUsdIata = dstDatos.Tables[0].Rows[0]["decValorUsd"].ToString();
            }
            catch
            {
                strUsdIata = string.Empty;
            }
            return strUsdIata;
        }
        public void InsertUsdIata(string strFecha, string strHora, string strValorUsdIata)
        {
            string strInsert = "INSERT INTO dbo.tblUsdIata (dtmFechaUsd,decValorUsd,dtmHora) VALUES('" + strFecha + "','" + strValorUsdIata + "','" + strHora + "')";
            sdSql.UpdateInsert(strInsert);
        }
        #endregion

        #region CMSReservas

        public clsParametros GuardarReservaSabre(DataSet dsSabre, clsCache cCache, string Proyecto, string Contacto, string Reserva)
        {
            try
            {
                ModificarTablasReserva(dsSabre);
                csReservas cReser = new csReservas();
                DataSet dsReserva = cReser.CrearTablaReserva();
                DataTable tblReser = dsReserva.Tables["tblReserva"];
              
                tblReser.Rows.Add(tblReser.NewRow());
                tblReser.Rows[tblReser.Rows.Count - 1].AcceptChanges();
                tblReser.Rows[tblReser.Rows.Count - 1]["strCodigo"] = "0";
                LlenarTablaReserva(dsSabre, cCache, Proyecto, Contacto, Reserva, tblReser);
                String int_Id_Estado = new CsConsultasVuelos().ConsultaCodigo(clsValidaciones.GetKeyOrAdd("EstadoReservaInicialAereo", "HK"),"TBLESTADOS_RESERVA","INTCODE","STRCODE");

                if (tblReser.Rows.Count > 0)
                {
                    DataTable tblTrans = dsReserva.Tables["tblTransac"];
                    LlenarTablaTransac(Reserva, tblReser.Rows[0]["intTipoPLan"].ToString(), tblReser.Rows[0]["intCodigoPlan"].ToString(),
                        int_Id_Estado, tblTrans, dsSabre, dsReserva.Tables["tblReserva"]);

                    if (ValidarIdaVuelta(dsSabre))
                        GetTasaAdmin(dsSabre, Enum_TipoTrayecto.IdaRegreso, getValidarTipoTrayecto(tblTrans));
                    else
                        GetTasaAdmin(dsSabre, Enum_TipoTrayecto.Ida, getValidarTipoTrayecto(tblTrans));

                    DataTable tblPasajero = dsReserva.Tables["tblPax"];
                    LlenarTablaPax(Reserva, tblPasajero, dsSabre);

                    DataTable tblFare = dsReserva.Tables["tblTarifa"];
                    LlenarTablaTarifa(Reserva, tblFare, dsSabre);

                    DataTable tblImp = dsReserva.Tables["tblTax"];
                    LlenarTablaImpuestos(Reserva, tblImp, dsSabre);
                }
                if (dsReserva.Tables["tblReserva"].Rows.Count == 0 || dsReserva.Tables["tblTransac"].Rows.Count == 0 ||
                    dsReserva.Tables["tblPax"].Rows.Count == 0 || dsReserva.Tables["tblTarifa"].Rows.Count == 0 ||
                    dsReserva.Tables["tblTax"].Rows.Count == 0)
                {
                    clsParametros cParametros = new clsParametros();
                    cParametros.Id = 0;
                    cParametros.ViewMessage.Add("La informacion obtenida de sabre no es suficiente");
                    cParametros.Sugerencia.Add("asegurese de que la reserva no se encuentra cancelada o comuniquese con el administrador");
                    cParametros.Metodo = "Guardar Reserva";
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.Tipo = clsTipoError.DataBase;
                    ExceptionHandled.Publicar(cParametros);
                    return cParametros;
                }
                else
                {
                    clsParametros Resultados = cReser.GuardaReservaGen(dsReserva);
                    return Resultados;
                }
            }
            catch
            {
                return null;
            }

        }


        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="dsSabre"></param>
        /// <param name="cCache"></param>
        /// <param name="Proyecto"></param>
        /// <param name="Contacto"></param>
        /// <param name="Reserva"></param>
        /// <param name="tblReser"></param>
        public void LlenarTablaReserva(DataSet dsSabre, clsCache cCache, string Proyecto, string Contacto, string Reserva, DataTable tblReser)
        {
            string strConexion = clsSesiones.getConexion(); ;
            csGenerales Generales = new csGenerales();
            Generales.Conexion = strConexion;
            String strCodigoPlan = "0";
            try
            {
              

                strCodigoPlan = new CsConsultasVuelos().ConsultaCodigo(clsValidaciones.GetKeyOrAdd("Aereo_WS"),"TBLTPOSERVICIO","INTID","STRCODIGO");
            }
            catch
            {
                strCodigoPlan = "0";
            }
           
            /*TIPO PLAN*/
            String strTipoPlan = clsValidaciones.GetKeyOrAdd("Aereo_WS");
            String idRefereTipoPlan = new CsConsultasVuelos().ConsultaCodigo(strTipoPlan,"TBLTPOSERVICIO","INTID","STRCODIGO"); 
            /*ESTADO SOLICITADA*/
            String int_Id_Estado = new CsConsultasVuelos().ConsultaCodigo(clsValidaciones.GetKeyOrAdd("EstadoProyectoInicial", "SL"),"TBLESTADOS_RESERVA","INTCODE","STRCODE");
            tblReser.Rows[tblReser.Rows.Count - 1]["intAplicacion"] = cCache.Aplicacion;
            if (Proyecto != "" && Proyecto != "0")
                tblReser.Rows[tblReser.Rows.Count - 1]["intProyecto"] = Proyecto;
            else
                tblReser.Rows[tblReser.Rows.Count - 1]["intProyecto"] = "0";
            tblReser.Rows[tblReser.Rows.Count - 1]["intContacto"] = Contacto;
            tblReser.Rows[tblReser.Rows.Count - 1]["intCliente"] = "0";

            DataTable dtItinerario = dsSabre.Tables["TravelItinerary"];

            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("TravelItinerary_ItineraryInfo"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("ItineraryInfo_Ticketing"))
                    {
                        tblReser.Rows[tblReser.Rows.Count - 1]["dtmVencimiento"] = drRelacionDos["TicketTimeLimit"].ToString().Replace("T", " ");
                    }
                }
            }
            tblReser.Rows[tblReser.Rows.Count - 1]["intEstado"] = int_Id_Estado;
            tblReser.Rows[tblReser.Rows.Count - 1]["intResponsable"] = Contacto;
            tblReser.Rows[tblReser.Rows.Count - 1]["intTipoPLan"] = idRefereTipoPlan;
            tblReser.Rows[tblReser.Rows.Count - 1]["strReserva"] = Reserva;

            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("TravelItinerary_UpdatedBy"))
                {
                    tblReser.Rows[tblReser.Rows.Count - 1]["dtmFecha"] = drFilaRelacionUno["CreateDateTime"].ToString().Replace("T", " ").Replace("-", "/");
                }
            }
            tblReser.Rows[tblReser.Rows.Count - 1]["intCodigoPlan"] = strCodigoPlan;
            tblReser.Rows[tblReser.Rows.Count - 1]["intFormaPago"] = "0";
            tblReser.Rows[tblReser.Rows.Count - 1]["intEstadoPago"] = "0";
            tblReser.Rows[tblReser.Rows.Count - 1]["intCentroC"] = "0";
            tblReser.Rows[tblReser.Rows.Count - 1]["intConsecRes"] = "0";
        }

        public void LlenarTablaTransac(string Reserva, string TipoPlan, string CodigoPlan, string Estado, DataTable tblTrans, DataSet dsSabre, DataTable tblReser)
        {
            string strConexion = clsSesiones.getConexion();
            DataTable dtItinerario = dsSabre.Tables["TravelItinerary"];
            csGenerales Generales = new csGenerales();
            Generales.Conexion = strConexion;

            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("TravelItinerary_ItineraryInfo"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("ItineraryInfo_ReservationItems"))
                    {
                        foreach (DataRow drRelacionCuatro in drRelacionDos.GetChildRows("ReservationItems_Item"))
                        {
                            foreach (DataRow drRelacionCinco in drRelacionCuatro.GetChildRows("Item_Air"))
                            {
                                tblTrans.Rows.Add(tblTrans.NewRow());
                                tblTrans.Rows[tblTrans.Rows.Count - 1].AcceptChanges();
                                tblTrans.Rows[tblTrans.Rows.Count - 1]["strCodigo"] = "0";
                                tblTrans.Rows[tblTrans.Rows.Count - 1]["strReserva"] = Reserva;
                                tblTrans.Rows[tblTrans.Rows.Count - 1]["intTipoPlan"] = TipoPlan;
                                tblTrans.Rows[tblTrans.Rows.Count - 1]["intCodigoPlan"] = CodigoPlan;
                                try
                                {
                                    tblTrans.Rows[tblTrans.Rows.Count - 1]["intSegmento"] = int.Parse(drRelacionCinco["RPH"].ToString()).ToString();
                                }
                                catch
                                {
                                    tblTrans.Rows[tblTrans.Rows.Count - 1]["intSegmento"] = "0";
                                }
                                tblTrans.Rows[tblTrans.Rows.Count - 1]["strReserva"] = Reserva;

                                foreach (DataRow drRelacionSeisDep in drRelacionCinco.GetChildRows("Air_DepartureAirport"))
                                {
                                    try
                                    {
                                        tblTrans.Rows[tblTrans.Rows.Count - 1]["intOrigen"] = Generales.ConsultarIdRefere(drRelacionSeisDep["LocationCode"].ToString(), clsValidaciones.GetKeyOrAdd("AEROPUERTOS"));
                                    }
                                    catch
                                    {
                                        tblTrans.Rows[tblTrans.Rows.Count - 1]["intOrigen"] = "0";
                                    }
                                }
                                foreach (DataRow drRelacionSeisArr in drRelacionCinco.GetChildRows("Air_ArrivalAirport"))
                                {
                                    try
                                    {
                                        tblTrans.Rows[tblTrans.Rows.Count - 1]["intDestino"] = Generales.ConsultarIdRefere(drRelacionSeisArr["LocationCode"].ToString(), clsValidaciones.GetKeyOrAdd("AEROPUERTOS"));
                                    }
                                    catch
                                    {
                                        tblTrans.Rows[tblTrans.Rows.Count - 1]["intDestino"] = "0";
                                    }
                                }
                                if (int.Parse(drRelacionCinco["RPH"].ToString()) == 1)
                                {
                                    tblReser.Rows[tblReser.Rows.Count - 1]["dtmFechaIni"] = drRelacionCinco["DepartureDateTime"].ToString().Replace("T", " ").Replace("-", "/");
                                }
                                if (int.Parse(drRelacionCinco["RPH"].ToString()) == dsSabre.Tables["Air"].Rows.Count)
                                {
                                    tblReser.Rows[tblReser.Rows.Count - 1]["dtmFechaFin"] = drRelacionCinco["ArrivalDateTime"].ToString().Replace("T", " ").Replace("-", "/");
                                }

                                try
                                {
                                    string[] FechaHora = drRelacionCinco["DepartureDateTime"].ToString().Replace("T", " ").Replace("-", "/").Split(' ');
                                    tblTrans.Rows[tblTrans.Rows.Count - 1]["dtmFechaIni"] = FechaHora[0];
                                    tblTrans.Rows[tblTrans.Rows.Count - 1]["strHoraIni"] = FechaHora[1].Substring(0, 5);
                                    if (int.Parse(drRelacionCinco["RPH"].ToString()) == 1)
                                    {
                                        tblReser.Rows[tblReser.Rows.Count - 1]["dtmVencimiento"] = drRelacionCinco["DepartureDateTime"].ToString().Replace("T", " ").Replace("-", "/");
                                        tblReser.Rows[tblReser.Rows.Count - 1]["dtmFechaLimitePago"] = drRelacionCinco["DepartureDateTime"].ToString().Replace("T", " ").Replace("-", "/");
                                    }

                                }
                                catch
                                {
                                    tblTrans.Rows[tblTrans.Rows.Count - 1]["dtmFechaIni"] = "1900/01/01";
                                    tblTrans.Rows[tblTrans.Rows.Count - 1]["strHoraIni"] = "12:00";
                                }

                                try
                                {
                                    string[] FechaHora = drRelacionCinco["ArrivalDateTime"].ToString().Replace("T", " ").Replace("-", "/").Split(' ');
                                    tblTrans.Rows[tblTrans.Rows.Count - 1]["dtmFechaFin"] = FechaHora[0];
                                    tblTrans.Rows[tblTrans.Rows.Count - 1]["strHoraFin"] = FechaHora[1].Substring(0, 5);
                                }
                                catch
                                {
                                    tblTrans.Rows[tblTrans.Rows.Count - 1]["dtmFechaFin"] = "1900/01/01";
                                    tblTrans.Rows[tblTrans.Rows.Count - 1]["strHoraFin"] = "12:00";
                                }
                                foreach (DataRow drRelacionSeisAerol in drRelacionCinco.GetChildRows("Air_MarketingAirline"))
                                {
                                    try
                                    {
                                        tblTrans.Rows[tblTrans.Rows.Count - 1]["intProveedor"] = Generales.ConsultarIdRefere(drRelacionSeisAerol["Code"].ToString(), clsValidaciones.GetKeyOrAdd("AIRLINESABRE"));
                                    }
                                    catch
                                    {
                                        tblTrans.Rows[tblTrans.Rows.Count - 1]["intProveedor"] = "0";
                                    }
                                }
                                tblTrans.Rows[tblTrans.Rows.Count - 1]["intCantidadPersonas"] = dsSabre.Tables["PersonName"].Rows.Count.ToString();
                                tblTrans.Rows[tblTrans.Rows.Count - 1]["intTipoAcomodacion"] = "0";
                                tblTrans.Rows[tblTrans.Rows.Count - 1]["intTipoHabitacion"] = "0";
                                tblTrans.Rows[tblTrans.Rows.Count - 1]["strOperador"] = drRelacionCinco["FlightNumber"].ToString();
                                tblTrans.Rows[tblTrans.Rows.Count - 1]["intEstado"] = Estado;
                                tblTrans.Rows[tblTrans.Rows.Count - 1]["intConsecRes"] = "0";
                            }
                        }

                    }
                }
            }
        }

        public void LlenarTablaPax(string Reserva, DataTable tblPax, DataSet dsSabre)
        {
            string strConexion = clsSesiones.getConexion();
            DataTable dtItinerario = dsSabre.Tables["TravelItinerary"];
            csGenerales Generales = new csGenerales();
            Generales.Conexion = strConexion;

            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("TravelItinerary_CustomerInfos"))
                {
                    foreach (DataRow drFilaRelacionDos in drFilaRelacionUno.GetChildRows("CustomerInfos_CustomerInfo"))
                    {
                        foreach (DataRow drFilaRelacionTres in drFilaRelacionDos.GetChildRows("CustomerInfo_Customer"))
                        {
                            foreach (DataRow drFilaRelacionCuatro in drFilaRelacionTres.GetChildRows("Customer_PersonName"))
                            {
                                tblPax.Rows.Add(tblPax.NewRow());
                                tblPax.Rows[tblPax.Rows.Count - 1].AcceptChanges();
                                tblPax.Rows[tblPax.Rows.Count - 1]["strCodigo"] = "0";
                                tblPax.Rows[tblPax.Rows.Count - 1]["intCodigoPax"] = Convert.ToString(int.Parse(drFilaRelacionCuatro["PersonName_Id"].ToString()) + 1);
                                if (drFilaRelacionCuatro["GivenName"].ToString().Trim().Substring(drFilaRelacionCuatro["GivenName"].ToString().Trim().Length - 3, 3).Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN")))
                                {
                                    tblPax.Rows[tblPax.Rows.Count - 1]["intTipoPax"] = Generales.ConsultarIdRefere(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN"));
                                }
                                else
                                {
                                    if (drFilaRelacionCuatro["GivenName"].ToString().Trim().Substring(drFilaRelacionCuatro["GivenName"].ToString().Trim().Length - 3, 3).Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroInfante", "INF")))
                                    {
                                        tblPax.Rows[tblPax.Rows.Count - 1]["intTipoPax"] = Generales.ConsultarIdRefere(clsValidaciones.GetKeyOrAdd("TipoPasajeroInfante", "INF"));
                                    }
                                    else
                                    {
                                        tblPax.Rows[tblPax.Rows.Count - 1]["intTipoPax"] = Generales.ConsultarIdRefere(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT"));
                                    }
                                }

                                tblPax.Rows[tblPax.Rows.Count - 1]["strNombre"] = drFilaRelacionCuatro["GivenName"].ToString().Trim().Substring(0, drFilaRelacionCuatro["GivenName"].ToString().Trim().Length - 3) + " " + drFilaRelacionCuatro["Surname"].ToString();
                                tblPax.Rows[tblPax.Rows.Count - 1]["dtmFechaNac"] = "1900/01/01";
                                tblPax.Rows[tblPax.Rows.Count - 1]["intEdad"] = "0";
                                tblPax.Rows[tblPax.Rows.Count - 1]["intConsecRes"] = "0";
                            }
                        }
                    }
                }
            }
        }

        public void LlenarTablaTarifa(string Reserva, DataTable tblTarif, DataSet dsSabre)
        {
            string strConexion = clsSesiones.getConexion();
            DataTable dtItinerario = dsSabre.Tables["TravelItinerary"];
            csGenerales Generales = new csGenerales();
            Generales.Conexion = strConexion;
            bool InsertadoNino = false;
            bool InsertadoInfante = false;
            bool InsertadoAdulto = false;

            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("TravelItinerary_ItineraryInfo"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("ItineraryInfo_ReservationItems"))
                    {
                        foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("ReservationItems_Item"))
                        {
                            foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("Item_Air"))
                            {
                                foreach (DataRow drRelacionCinco in drRelacionCuatro.GetChildRows("Air_ItemPricing"))
                                {
                                    foreach (DataRow drRelacionSeis in drRelacionCinco.GetChildRows("ItemPricing_AirFareInfo"))
                                    {
                                        foreach (DataRow drRelacionSiete in drRelacionSeis.GetChildRows("AirFareInfo_PTC_FareBreakdown"))
                                        {
                                            string TpoPax = "";
                                            foreach (DataRow drRelacionOchoTpoPax in drRelacionSiete.GetChildRows("PTC_FareBreakdown_PassengerTypeQuantity"))
                                            {
                                                TpoPax = drRelacionOchoTpoPax["Code"].ToString();
                                                if (TpoPax.Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT")) && InsertadoAdulto == false ||
                                                TpoPax.Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN")) && InsertadoNino == false ||
                                                TpoPax.Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroInfante", "INF")) && InsertadoInfante == false)
                                                {
                                                    if (TpoPax.Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT")))
                                                        InsertadoAdulto = true;
                                                    if (TpoPax.Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN")))
                                                        InsertadoNino = true;
                                                    if (TpoPax.Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroInfante", "INF")))
                                                        InsertadoInfante = true;
                                                    DataRow drRelacionSieteAtras = drRelacionOchoTpoPax.GetParentRow("PTC_FareBreakdown_PassengerTypeQuantity");
                                                    foreach (DataRow drRelacionOchoPaxFare in drRelacionSieteAtras.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                                                    {
                                                        float ValorFare = 0;
                                                        string Moneda = "";
                                                        if (dsSabre.Tables["EquivFare"].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow drRelacionNueveFare in drRelacionOchoPaxFare.GetChildRows("PassengerFare_EquivFare"))
                                                            {
                                                                ValorFare = float.Parse(drRelacionNueveFare["Amount"].ToString());
                                                                Moneda = drRelacionNueveFare["CurrencyCode"].ToString();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            foreach (DataRow drRelacionNueveFare in drRelacionOchoPaxFare.GetChildRows("PassengerFare_BaseFare"))
                                                            {
                                                                ValorFare = float.Parse(drRelacionNueveFare["Amount"].ToString());
                                                                Moneda = drRelacionNueveFare["CurrencyCode"].ToString();
                                                            }
                                                        }
                                                        tblTarif.Rows.Add(tblTarif.NewRow());
                                                        tblTarif.Rows[tblTarif.Rows.Count - 1].AcceptChanges();
                                                        tblTarif.Rows[tblTarif.Rows.Count - 1]["strCodigo"] = "0";
                                                        tblTarif.Rows[tblTarif.Rows.Count - 1]["strReserva"] = Reserva;
                                                        try
                                                        {
                                                            tblTarif.Rows[tblTarif.Rows.Count - 1]["intTipoPax"] = Generales.ConsultarIdRefere(TpoPax);
                                                        }
                                                        catch
                                                        {
                                                            tblTarif.Rows[tblTarif.Rows.Count - 1]["intTipoPax"] = "0";
                                                        }
                                                        tblTarif.Rows[tblTarif.Rows.Count - 1]["intCodigfare"] = "0";
                                                        try
                                                        {
                                                            tblTarif.Rows[tblTarif.Rows.Count - 1]["intMoneda"] = Generales.ConsultarIdRefere(Moneda);
                                                        }
                                                        catch
                                                        {
                                                            tblTarif.Rows[tblTarif.Rows.Count - 1]["intMoneda"] = "0";
                                                        }
                                                        tblTarif.Rows[tblTarif.Rows.Count - 1]["dblValor"] = ValorFare;
                                                        float Impuestos = 0;
                                                        foreach (DataRow drRelacionNueveTax in drRelacionOchoPaxFare.GetChildRows("PassengerFare_Taxes"))
                                                        {
                                                            foreach (DataRow drRelacionDiezTax in drRelacionNueveTax.GetChildRows("Taxes_Tax"))
                                                            {
                                                                try
                                                                {
                                                                    Impuestos = Impuestos + float.Parse(drRelacionDiezTax["Amount"].ToString());
                                                                }
                                                                catch
                                                                {
                                                                    Impuestos = Impuestos + 0;
                                                                }
                                                            }
                                                        }
                                                        tblTarif.Rows[tblTarif.Rows.Count - 1]["dblTax"] = Impuestos;
                                                        float Total = 0;
                                                        foreach (DataRow drRelacionNueveTotal in drRelacionOchoPaxFare.GetChildRows("PassengerFare_TotalFare"))
                                                        {
                                                            Total = float.Parse(drRelacionNueveTotal["Amount"].ToString());
                                                        }
                                                        tblTarif.Rows[tblTarif.Rows.Count - 1]["dblTotal"] = ValorFare + Impuestos;
                                                        tblTarif.Rows[tblTarif.Rows.Count - 1]["intConsecRes"] = "0";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void LlenarTablaImpuestos(string Reserva, DataTable tblImp, DataSet dsSabre)
        {
            string strConexion = clsSesiones.getConexion();
            DataTable dtItinerario = dsSabre.Tables["TravelItinerary"];
            csGenerales Generales = new csGenerales();
            Generales.Conexion = strConexion;
            bool InsertadoNino = false;
            bool InsertadoInfante = false;
            bool InsertadoAdulto = false;

            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("TravelItinerary_ItineraryInfo"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("ItineraryInfo_ReservationItems"))
                    {
                        foreach (DataRow drRelacionTres in drRelacionDos.GetChildRows("ReservationItems_Item"))
                        {
                            foreach (DataRow drRelacionCuatro in drRelacionTres.GetChildRows("Item_Air"))
                            {
                                foreach (DataRow drRelacionCinco in drRelacionCuatro.GetChildRows("Air_ItemPricing"))
                                {
                                    foreach (DataRow drRelacionSeis in drRelacionCinco.GetChildRows("ItemPricing_AirFareInfo"))
                                    {
                                        foreach (DataRow drRelacionSiete in drRelacionSeis.GetChildRows("AirFareInfo_PTC_FareBreakdown"))
                                        {
                                            string TpoPax = "";
                                            foreach (DataRow drRelacionOchoTpoPax in drRelacionSiete.GetChildRows("PTC_FareBreakdown_PassengerTypeQuantity"))
                                            {
                                                TpoPax = drRelacionOchoTpoPax["Code"].ToString();
                                                if (TpoPax.Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT")) && InsertadoAdulto == false ||
                                                TpoPax.Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN")) && InsertadoNino == false ||
                                                TpoPax.Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroInfante", "INF")) && InsertadoInfante == false)
                                                {
                                                    if (TpoPax.Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroAdulto", "ADT")))
                                                        InsertadoAdulto = true;
                                                    if (TpoPax.Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroNino", "CNN")))
                                                        InsertadoNino = true;
                                                    if (TpoPax.Equals(clsValidaciones.GetKeyOrAdd("TipoPasajeroInfante", "INF")))
                                                        InsertadoInfante = true;
                                                    DataRow drRelacionSieteAtras = drRelacionOchoTpoPax.GetParentRow("PTC_FareBreakdown_PassengerTypeQuantity");
                                                    foreach (DataRow drRelacionOchoPaxFare in drRelacionSieteAtras.GetChildRows("PTC_FareBreakdown_PassengerFare"))
                                                    {
                                                       
                                                        foreach (DataRow drRelacionNueveTax in drRelacionOchoPaxFare.GetChildRows("PassengerFare_Taxes"))
                                                        {
                                                            foreach (DataRow drRelacionDiezTax in drRelacionNueveTax.GetChildRows("Taxes_Tax"))
                                                            {
                                                                tblImp.Rows.Add(tblImp.NewRow());
                                                                tblImp.Rows[tblImp.Rows.Count - 1].AcceptChanges();
                                                                tblImp.Rows[tblImp.Rows.Count - 1]["strCodigo"] = "0";
                                                                tblImp.Rows[tblImp.Rows.Count - 1]["intCodigFare"] = "0";
                                                                try
                                                                {
                                                                    tblImp.Rows[tblImp.Rows.Count - 1]["intTipoPax"] = Generales.ConsultarIdRefere(TpoPax);
                                                                }
                                                                catch
                                                                {
                                                                    tblImp.Rows[tblImp.Rows.Count - 1]["intTipoPax"] = "0";
                                                                }
                                                                try
                                                                {
                                                                    
                                                                     DataTable dt = new DataTable();
                                                                    string scode=new CsConsultasVuelos().ConsultaCodigo(drRelacionDiezTax["TaxCode"].ToString(),"TBLIMPUESTOSSABRE","INTCODE","STRCODE");
                                                                    
                                                                    tblImp.Rows[tblImp.Rows.Count - 1]["intCodigoTax"] = scode;
                                                                }
                                                                catch
                                                                {
                                                                    tblImp.Rows[tblImp.Rows.Count - 1]["intCodigoTax"] = "0";
                                                                }
                                                                try
                                                                {
                                                                    tblImp.Rows[tblImp.Rows.Count - 1]["intMoneda"] = Generales.ConsultarIdRefere(drRelacionDiezTax["CurrencyCode"].ToString());
                                                                }
                                                                catch
                                                                {
                                                                    tblImp.Rows[tblImp.Rows.Count - 1]["intMoneda"] = "0";
                                                                }
                                                                try
                                                                {
                                                                    tblImp.Rows[tblImp.Rows.Count - 1]["dblValorTax"] = float.Parse(drRelacionDiezTax["Amount"].ToString());
                                                                }
                                                                catch
                                                                {
                                                                    tblImp.Rows[tblImp.Rows.Count - 1]["dblValorTax"] = 0;
                                                                }
                                                                tblImp.Rows[tblImp.Rows.Count - 1]["dblPorcent"] = 0;
                                                                tblImp.Rows[tblImp.Rows.Count - 1]["intConsecRes"] = "0";
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ModificarTablasReserva(DataSet dsSabre)
        {
            DataTable tblPaxFare = dsSabre.Tables["PassengerFare"];
            if (!tblPaxFare.Columns.Contains("intBaseFareUSD"))
                tblPaxFare.Columns.Add("intBaseFareUSD", typeof(decimal));

            for (int i = 0; i < tblPaxFare.Rows.Count; i++)
            {
                tblPaxFare.Rows[i]["intBaseFareUSD"] = "0";
            }

            DataTable tblTax = dsSabre.Tables["Tax"];
            try { tblTax.Columns.Add("Tax_Amount_Usd", typeof(decimal)); }
            catch { }
            try { tblTax.Columns.Add("CurrencyCode", typeof(string)); }
            catch { }
        }

        public bool ValidarIdaVuelta(DataSet dsSabre)
        {
            string Origen = "";
            string Destino = "";
            DataTable dtItinerario = dsSabre.Tables["TravelItinerary"];
            foreach (DataRow drItinerario in dtItinerario.Rows)
            {
                foreach (DataRow drFilaRelacionUno in drItinerario.GetChildRows("TravelItinerary_ItineraryInfo"))
                {
                    foreach (DataRow drRelacionDos in drFilaRelacionUno.GetChildRows("ItineraryInfo_ReservationItems"))
                    {
                        foreach (DataRow drRelacionCuatro in drRelacionDos.GetChildRows("ReservationItems_Item"))
                        {
                            foreach (DataRow drRelacionCinco in drRelacionCuatro.GetChildRows("Item_Air"))
                            {
                                if (int.Parse(drRelacionCinco["RPH"].ToString()) == 1)
                                {
                                    foreach (DataRow drRelacionSeisDep in drRelacionCinco.GetChildRows("Air_DepartureAirport"))
                                    {
                                        try
                                        {
                                            Origen = drRelacionSeisDep["LocationCode"].ToString();
                                        }
                                        catch
                                        {
                                            Origen = "0";
                                        }
                                    }
                                }
                                if (int.Parse(drRelacionCinco["RPH"].ToString()) == dsSabre.Tables["Air"].Rows.Count)
                                {
                                    foreach (DataRow drRelacionSeisArr in drRelacionCinco.GetChildRows("Air_ArrivalAirport"))
                                    {
                                        try
                                        {
                                            Destino = drRelacionSeisArr["LocationCode"].ToString();
                                        }
                                        catch
                                        {
                                            Destino = "0";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (Origen.Equals(Destino))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// metodo pendiente por verificar funcionalidad
        /// </summary>
        /// <param name="tblTransac"></param>
        /// <returns></returns>
        public Enum_TipoVuelo getValidarTipoTrayecto(DataTable tblTransac)
        {
           
            Enum_TipoVuelo eTipoVuelo = Enum_TipoVuelo.Nacional;
            string strConexion = clsSesiones.getConexion();           
            string sPaisDefault = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");
            string sPais = clsValidaciones.GetKeyOrAdd("Paises", "Pais");
            
            try
            {
                if (tblTransac.Rows.Count > 0)
                {
                    string strCodigoCOL = new CsConsultasVuelos().ConsultaCodigo(sPaisDefault,"TBLPAIS","INTCODE","STRCOUNTRYCODE");

                    foreach (DataRow drFilaSegmnento in tblTransac.Rows)
                    {
                      
                        //otblRefere.Get(drFilaSegmnento["intOrigen"].ToString());
                        string strOrigen = drFilaSegmnento["intOrigen"].ToString();

                        //otblRefere.Get(drFilaSegmnento["intDestino"].ToString());
                        string strDestino = drFilaSegmnento["intDestino"].ToString();


                        if (!(strOrigen.Equals(strCodigoCOL) && strDestino.Equals(strCodigoCOL)))
                        {
                            eTipoVuelo = Enum_TipoVuelo.Internacional;
                            break;
                        }
                    }
                }
                else
                {
                    eTipoVuelo = Enum_TipoVuelo.Internacional;
                }
            }
            catch
            {
                eTipoVuelo = Enum_TipoVuelo.Internacional;
            }
            return eTipoVuelo;
        }

        #endregion

        #endregion
    }

}
        #endregion