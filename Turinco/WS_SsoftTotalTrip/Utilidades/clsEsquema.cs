using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Schema;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;
using WS_SsoftTotalTrip.HotelShop;
using WS_SsoftTotalTrip.GetHotelInfos;
using WS_SsoftTotalTrip.HotelConfirm;
using WS_SsoftTotalTrip.HotelRes;
using WS_SsoftTotalTrip.HotelReservationInfo;
using WS_SsoftTotalTrip.CancelHotel;
using Ssoft.ValueObjects;
using Ssoft.Ssoft.ValueObjects.Hoteles;

namespace WS_SsoftTotalTrip.Utilidades
{
    public class clsEsquema
    {
        #region [ Definiciones ]
        //NOMBRES TABLAS
        // Manejo de errores

        public const string TABLA_ERROR = "ErrorSsoft";

        public const string COLUMN_MESSAGE = "Message";

        // Tablas WebServices
        public const string TABLA_HOTEL_INFO = "HotelInfo";
        private const string TABLA_HOTEL_OCCUPANCY = "HotelOccupancy";
        public const string TABLA_HOTEL_ROOM = "HotelRoom";
        private const string TABLA_ROOM_TYPE = "RoomType";

        private const string TABLA_HOTEL_VALUE_AVAIL = "HotelValuedAvailRS";
        private const string TABLA_PAGINATION_DATA = "PaginationData";

        private const string TABLA_FEATURE = "Feature";
        private const string TABLA_FACILIDADES = "tblFacilidades";
        private const string TABLA_INSTALACIONES = "tblInstalaciones";
        private const string TABLA_IMAGEN = "tblImage";
        private const string TABLA_FACILITIES_IMAGEN = "Image";

        private const string TABLA_ADITIONAL_COST_LIST = "AdditionalCostList";
        private const string TABLA_ADITIONAL_COST = "AdditionalCost";
        public const string TABLA_CANCELATION_POLICY = "CancellationPolicy";

        public const string TABLA_PRICE = "Price";

        private const string TABLA_DATE_TIME_FROM = "DateTimeFrom";
        private const string TABLA_SUPPLIER = "Supplier";
        private const string TABLA_COMMENT = "Comment";
        private const string TABLA_ZONE = "Zone";

        // TABLA_HOTEL_VALUE_AVAIL
        private const string COLUMN_HOTEL_VALUE_AVAIL = "HotelValuedAvailRS_Id";
        private const string COLUMN_TOTAL_ITEMS = "totalItems";

        // TABLA_PAGINATION_DATA
        private const string COLUMN_CURRENT_PAGE = "currentPage";
        private const string COLUMN_TOTAL_PAGES = "totalPages";
        private const string COLUMN_RESULT_PAGE = "resultPage";

        // TABLA_HOTEL_INFO
        public const string COLUMN_HOTELCODE = "Code";
        private const string COLUMN_NAME = "Name";
        private const string COLUMN_HOTEL_INFO_ID = "HotelInfo_Id";
        private const string COLUMN_CATEGORY_TEXT = "Category_Text";
        private const string COLUMN_CATEGORY_CODE = "Category_Code";
        private const string COLUMN_DESTINATION_NAME = "Destination_Name";
        public const string COLUMN_DESTINATION_CODE = "Destination_Code";
        private const string COLUMN_CLASIFICATION_TEXT = "Classification_Text";
        private const string COLUMN_CLASIFICATION_CODE = "Classification_Code";
        private const string COLUMN_LATITUDE = "latitude";
        private const string COLUMN_LONGITUDE = "longitude";
        private const string COLUMN_ZONE_TEXT = "Zone_Text";
        private const string COLUMN_URL = "Url";
        private const string COLUMN_IMAGEN_URL = "Hotel_Photo";
        private const string COLUMN_OFERTA = "Oferta";
        private const string COLUMN_DETALLES_URL = "DetallesURL";
        private const string COLUMN_DESCRIPTION = "description";
        private const string COLUMN_DESCRIPTION_LONG = "description_Long";
        private const string COLUMN_ADDRESS = "Address";
        private const string COLUMN_CURRENCY_TEST = "Currency_Text";
        private const string COLUMN_CURRENCY_CODE = "Currency_Code";
        public const string COLUMN_AMOUNT = "Amount";
        private const string COLUMN_AMOUNT_TEXT = "AmountText";
        private const string COLUMN_HOTEL_INFO_CODE = "HotelInfo_Code";
        private const string COLUMN_HOTEL_TELEPHONE_NUMBER = "Hotel_Telephone_Number";
        private const string COLUMN_HOTEL_PHONE_TYPE = "PhoneType";
        private const string COLUMN_HOTEL_PHONE_NUMBRE = "Number_";
        private const string COLUMN_WS_SELECT = "WsSelect";
        public const string COLUMN_COUNTRY_CODE = "Country_Code";
        public const string COLUMN_COUNTRY__NAME = "Country_Name";
        public const string COLUMN_AGENCY_COMISION = "AgencyComission";
        public const string COLUMN_OBSERVACIONES = "Observaciones";
        public const string COLUMN_IVA = "Iva";
        public const string COLUMN_NON_REEMBOLSABLE = "Reembolsable"; // Si es 0, es reembolsable, si es 1 es no reembolsable
        
        private const string COLUMN_HOTEL_CODE = "Hotel_Code";
        private const string COLUMN_FACILITIES_CODE = "Code";
        private const string COLUMN_FACILITIES = "Facilidad";
        private const string COLUMN_NAME_GROUP = "name_group";
        private const string COLUMN_CAR_DISTANCE = "CarDistance";
        private const string COLUMN_CONCEPT = "Concept";
        private const string COLUMN_FACILITIES_DESCRIPTION = "Description";
        private const string COLUMN_IMAGEPATH = "ImagePath";
        private const string COLUMN_FACILITIES_IMAGEPATH = "URL";
        private const string COLUMN_FACILITIES_GROUP = "group";
        public const string COLUMN_SOURCE = "Source"; // Se adiciona para incluir varios proveedores desde totaltrip
        public const string COLUMN_HOTELVALIDA = "ValidaDetalle";

        public const string COLUMN_HOTEL_ID_ITEM = "ItemId";

        // TABLA_HOTEL_ROOM
        private const string COLUMN_HOTEL_ROOM_OCUPATION = "Id";
        public const string COLUMN_HOTEL_ROOM_ID = "HotelRoom_Id";
        public const string COLUMN_ROOM_ID = "roomId";
        public const string COLUMN_SHRUI = "SHRUI";
        private const string COLUMN_AVAIL_COUNT = "availCount";
        private const string COLUMN_ON_REQUEST = "onRequest";
        private const string COLUMN_POLITICA = "Politica";
        private const string COLUMN_PENALIZACION = "Penalizacion";
        private const string COLUMN_SELECCION = "Seleccion";
        public const string COLUMN_CHARASTERISTIC = "characteristic";
        public const string COLUMN_ROOM_TYPE_TEXT = "RoomType_Text";
        private const string COLUMN_BOARD_TEXT = "Board_Text";
        private const string COLUMN_BOARD_CODE = "Board_Code";
        private const string COLUMN_BOARD_SHORT_NAME = "Board_shortname";
        private const string COLUMN_DATE_FROM = "Date_From";
        private const string COLUMN_DATE_FROM_FORMAT = "Date_From_YMD";
        private const string COLUMN_DATE_TO = "Date_To";
        private const string COLUMN_DATE_TO_FORMAT = "Date_To_YMD";
        private const string COLUMN_ROOM_COUNT_TEXT = "RoomCountText";
        private const string COLUMN_ROOM_COUNT = "RoomCount";
        private const string COLUMN_HOTEL_OCCUPANCY_ID = "HotelOccupancy_Id";
        private const string COLUMN_ADULT_COUNT = "AdultCount";
        private const string COLUMN_CHILD_COUNT = "ChildCount";

        public const string COLUMN_TYPE = "Type";
        private const string COLUMN_SERVICE_HOTEL_ID = "ServiceHotel_Id";
        private const string COLUMN_AVAIL_TOKEN = "availToken";
        private const string COLUMN_CONTRACT_LIST_ID = "ContractList_Id";
        private const string COLUMN_CONTRACT_ID = "Contract_Id";
        private const string COLUMN_CONTRACT_NAME = "Contract_Name";
        private const string COLUMN_INCOMING_CODE = "Incoming_Code";
        public const string COLUMN_CONFIRM_RATEPLANTYPE = "RatePlanType";

        private const string COLUMN_VAT_NUMBER = "vatNumber";
        private const string COLUMN_FILE_NUMBER = "FileNumber";
        private const string COLUMN_PNR = "PNR";// Se adiciona para incluir varios proveedores desde totaltrip

        private const string COLUMN_TIPO_MK = "tipoMk";
        private const string COLUMN_VALOR_MK = "valorMk";

        public const string COLUMN_ROOM_AMOUNT_SURCHARGE = "AmountIncludedBoardSurcharge";// Se adiciona para incluir varios proveedores desde totaltrip
        public const string COLUMN_ROOM_TTL = "TTL";// Se adiciona para incluir varios proveedores desde totaltrip

        // Cancelacion
        private const string COLUMN_CANCELATION_POLICY_ID = "CancellationPolicy_Id";
        public const string COLUMN_CANCELATION_TOTAL_DAYS = "Cancellation_TotalDay";
        private const string COLUMN_CANCELATION_AMOUNT_DAY = "Cancellation_AmountDay";
        private const string COLUMN_CANCELATION_AMOUNT_DAY_TEXT = "Cancellation_AmountDayText";
        private const string COLUMN_CANCELATION_TOTAL_AMOUNT = "Cancellation_TotalAmount";
        private const string COLUMN_CANCELATION_TOTAL_AMOUNT_TEXT = "Cancellation_TotalAmountText";
        private const string COLUMN_CANCELATION_PRICE_ID = "Cancellation_PriceId";

        public const string COLUMN_CANCELATION_FEES = "FeesInclusive";
        public const string COLUMN_CANCELATION_MULTIPLIER = "MultiPlier";
        public const string COLUMN_CANCELATION_DROPTIME = "OffSetDropTime";
        public const string COLUMN_CANCELATION_TIMEOUT = "OffSetTimeUnit";
        public const string COLUMN_CANCELATION_TAXAMOUNT = "TaxAmount";
        private const string COLUMN_CANCELATION_TOTAL_VALUE = "Cancellation_AmountDay";

        // TABLA_DATE_TIME_FROM
        private const string COLUMN_DATE_TIME = "time";
        private const string COLUMN_DATE_TIME_FROM = "DateTimeFrom";
        private const string COLUMN_DATE_TIME_FROM_FORMAT = "DateTimeFrom_YMD";

        // TABLA_DATE_TIME_TO
        private const string COLUMN_DATE_TIME_TO = "DateTimeTo";
        private const string COLUMN_DATE_TIME_TO_FORMAT = "DateTimeTo_YMD";

        // TABLA_PRICE
        private const string COLUMN_PRICE_ID = "Price_Id";

        private const string COLUMN_STATUS = "Status";
        private const string COLUMN_PURCHASE_ID = "Purchase_Id";
        private const string COLUMN_LANGUAJE = "Language";
        private const string COLUMN_TOTAL_PRICE = "TotalPrice";
        private const string COLUMN_TOTAL_PRICE_TEXT = "TotalPriceText";
        private const string COLUMN_PURCHASE_TOKEN = "purchaseToken";
        private const string COLUMN_TIME_TO_EXPIRATION = "timeToExpiration";
        private const string COLUMN_SERVICEADDRS_ID = "ServiceAddRS_Id";
        private const string COLUMN_PURCHASE_STATUS = "Purchase_Status";
        private const string COLUMN_CREATION_USER = "CreationUser";
        private const string COLUMN_AGENCY_REFERENCE = "AgencyReference";
        private const string COLUMN_SERVICE_STATUS = "Service_Status";
        private const string COLUMN_COMMENT_TEXT = "Comment_Text";

        public const string COLUMN_CURRENCY = "Currency";// Se adiciona para incluir varios proveedores desde totaltrip
        public const string COLUMN_PROVIDER_CURRENCY = "ProviderCurrency";// Se adiciona para incluir varios proveedores desde totaltrip
        public const string COLUMN_ROE = "ROE";// Se adiciona para incluir varios proveedores desde totaltrip

        // TABLA_ZONE
        private const string COLUMN_ZONE_NAME = "ZoneName";
        private const string COLUMN_ZONE_CODE = "ZoneCode";
        private const string COLUMN_ZONE_LEVEL = "ZoneLevel";

        // Price
        public const string COLUMN_DATE = "Date";
        public const string COLUMN_PRICE_AMOUN_AFTER_TAX = "AmountAfterTax";
        public const string COLUMN_PRICE_AMOUN_BEFORE_TAX = "AmountBeforeTax";
        public const string COLUMN_PRICE_DISCOUNT_AFTER_TAX = "DiscountAfterTax";
        public const string COLUMN_PRICE_DISCOUNT_BEFORE_TAX = "DiscountBeforeTax";

        public const string COLUMN_PRICE_AMOUN_AFTER_TAX_FEE = "AmountAfterTaxFee";
        public const string COLUMN_PRICE_AMOUN_BEFORE_TAX_FEE = "AmountBeforeTaxFee";
        public const string COLUMN_PRICE_DISCOUNT_AFTER_TAX_FEE = "DiscountAfterTaxFee";
        public const string COLUMN_PRICE_DISCOUNT_BEFORE_TAX_FEE = "DiscountBeforeTaxFee";
        //private const string COLUMN_HOTEL_ROOM_ID = "HotelRoom_Id";

        private const string COLUMN_SUPPLIER_NAME = "name";
        private const string COLUMN_SUPPLIER_VATNUMBER = "vatNumber";
        private const string COLUMN_TOTAL_AMOUNT = "TotalAmount";

        // Formatos
        private const string FORMATO_NUMEROS = "#,##0.00";
        private const string FORMATO_NUMEROS_SD = "#,##0";
        private static string FORMATO_FECHA_BD = clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");
        private static string FORMATO_FECHA = clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");
        private string FORMATO_NUMEROS_VIEW = clsValidaciones.GetKeyOrAdd("sFormatoView", "#,##0");   
        private string sMonedaHotel = clsValidaciones.GetKeyOrAdd("MonedaHotel","USD");
    

        #endregion

        public DataSet GetDatasetHotelShop(HotelShopRS oResponse)
        {
            DataSet dsResultados = new DataSet();
            try
            {
                setHotelInfo(dsResultados, oResponse);
                clsSesiones.setResultadoHotel(dsResultados);
            }
            catch
            {
            }
            return dsResultados;
        }
        public clsParametros GetDatasetHotelReserva(DataSet dsData, HotelResRS oResponse)
        {
            clsParametros cParametros = new clsParametros();
            DataTable dtHotelRoom = dsData.Tables[TABLA_HOTEL_ROOM];
            try
            {
                cParametros.Id = 1;
                foreach (DataRow drFila in dtHotelRoom.Rows)
                {
                    drFila[COLUMN_FILE_NUMBER] = oResponse.TotalTripId;
                    drFila[COLUMN_VAT_NUMBER] = oResponse.ResId;
                    drFila[COLUMN_INCOMING_CODE] = oResponse.TripId;
                   
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.Complemento = "Resultados de Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda: ");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        public clsParametros GetDatasetConfirmaReserva(DataSet dsData, HotelConfirmRS oResponse, bool bCambioTarifa)
        {
            string sObservaciones = string.Empty;
            int iReembolsable = 0;
            clsParametros cParametros = new clsParametros();
            DataTable dtHotelInfo = dsData.Tables[TABLA_HOTEL_INFO];
            DataTable dtHotelCancelacion = dsData.Tables[TABLA_CANCELATION_POLICY];
            DataTable dtHotelRoom = dsData.Tables[TABLA_HOTEL_ROOM];
            DataTable dtPriceDate = dsData.Tables[TABLA_PRICE];

            DataTable dtCancelationDate = dsData.Tables[TABLA_DATE_TIME_FROM];
            DataTable dtSupplier = dsData.Tables[TABLA_SUPPLIER];
            DataTable dtComment = dsData.Tables[TABLA_COMMENT];

            decimal dFactor = 1;
            try
            {
                clsCache cCache = new csCache().cCache();
                
            }
            catch { }

            VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();
            if (bCambioTarifa)
            {
                dtHotelRoom.Clear();
                dtPriceDate.Clear();
                sObservaciones = "!! La tarifa ha cambiado !!";
            }
            int iDiasCancel = -3;
            try { iDiasCancel = int.Parse(clsValidaciones.GetKeyOrAdd("DiasPenalizacion", "3")) * -1; }
            catch { }
            DateTime dtDate = DateTime.Now;
            string sDate = dtDate.ToString(FORMATO_FECHA_BD);
            string sDateFormat = dtDate.ToString(FORMATO_FECHA);
            cParametros.Id = 1;
            string sWsSelect = clsValidaciones.GetKeyOrAdd("WS_HOTEL_TT", "WS_TT");
            decimal dIncremento = (1 + Convert.ToDecimal(clsValidaciones.GetKeyOrAdd("IncrementoHotelTT", "10")) / 100);
            string sBoard = clsValidaciones.GetKeyOrAdd("SinAlimentacion", "SH");
            decimal dMarckupUsd = decimal.Parse(clsValidaciones.GetKeyOrAdd("dMarckupUsd", "0"));

            string sDateFrom = clsValidaciones.ConverFechaSinSeparadorYMD(vo_HotelValuedAvailRQ.CheckInDate);
            string sDateTo = clsValidaciones.ConverFechaSinSeparadorYMD(vo_HotelValuedAvailRQ.CheckOutDate);

            try
            {
                int iRoomId = 1000;
                int iAumento = 1000;
                //TRAEMOS LOS RESULTADOS DE BUSQUEDA GUARDADOS 
                if (oResponse.HotelFare.Rooms.Length > 0)
                {
                    string sMonedaView = sMonedaHotel;
                    string sMoneda = sMonedaHotel;
                  

                    for (int i = 0; i < oResponse.HotelFare.Rooms.Length; i++)
                    {
                        if (oResponse.HotelFare.Rooms[i].RoomTypes.Length > 0)
                        {
                            if (oResponse.HotelFare.Rooms[i].RoomTypes[0].EffectiveDates.Length > 0)
                            {
                                dtDate = oResponse.HotelFare.Rooms[i].RoomTypes[0].EffectiveDates[0].Date.AddDays(iDiasCancel);
                                sDate = dtDate.ToString(FORMATO_FECHA_BD);
                                sDateFormat = dtDate.ToString(FORMATO_FECHA);
                            }
                            if (bCambioTarifa)
                            {
                                for (int k = 0; k < oResponse.HotelFare.Rooms[i].RoomTypes.Length; k++)
                                {
                                    string sTipoMk = "P";
                                    decimal dValorTotal = 0;
                                    decimal dValorAntes = 0;

                                    decimal dValorTotalUSD = 0;
                                    decimal dValorAntesUSD = 0;

                                    decimal dValorTotalIni = 0;
                                    decimal dValorAntesIni = 0;

                                    DataRow drFilaRoom = dtHotelRoom.NewRow();

                                    drFilaRoom[COLUMN_WS_SELECT] = sWsSelect;
                                    drFilaRoom[COLUMN_HOTELCODE] = oResponse.HotelFare.Hot.HotelCode;
                                    drFilaRoom[COLUMN_HOTEL_INFO_CODE] = oResponse.HotelFare.Hot.HotelCode;
                                    drFilaRoom[COLUMN_NAME] = oResponse.HotelFare.Hot.HotelName;
                                    drFilaRoom[COLUMN_HOTEL_ROOM_ID] = iRoomId;
                                    try
                                    {
                                        drFilaRoom[COLUMN_HOTEL_INFO_ID] = int.Parse(dtHotelInfo.Rows[i][COLUMN_HOTEL_INFO_ID].ToString());
                                    }
                                    catch {
                                        drFilaRoom[COLUMN_HOTEL_INFO_ID] = int.Parse(dtHotelInfo.Rows[0][COLUMN_HOTEL_INFO_ID].ToString());
                                    }
                                    drFilaRoom[COLUMN_HOTEL_OCCUPANCY_ID] = k;
                                    drFilaRoom[COLUMN_HOTEL_ROOM_OCUPATION] = i + 1;

                                    drFilaRoom[COLUMN_CONFIRM_RATEPLANTYPE] = oResponse.HotelFare.Rooms[i].RoomTypes[k].RatePlanType;
                                    drFilaRoom[COLUMN_SHRUI] = oResponse.HotelFare.Rooms[i].RoomTypes[k].RoomRate;
                                    drFilaRoom[COLUMN_ROOM_TYPE_TEXT] = oResponse.HotelFare.Rooms[i].RoomTypes[k].RoomDesc;
                                    drFilaRoom[COLUMN_TYPE] = oResponse.HotelFare.Rooms[i].RoomTypes[k].RoomType;
                                    drFilaRoom[COLUMN_CHARASTERISTIC] = oResponse.HotelFare.Rooms[i].RoomTypes[k].InvBlockCode;

                                    drFilaRoom[COLUMN_ROOM_AMOUNT_SURCHARGE] = oResponse.HotelFare.Rooms[i].RoomTypes[k].AmountIncludedBoardSurcharge;
                                    drFilaRoom[COLUMN_ROOM_TTL] = oResponse.HotelFare.Rooms[i].RoomTypes[k].TTL.ToString();

                                    for (int m = 0; m < oResponse.HotelFare.Rooms[i].RoomTypes[k].EffectiveDates.Length; m++)
                                    {
                                        DataRow drFilaPrice = dtPriceDate.NewRow();

                                        decimal dValorTotalDate = 0;
                                        decimal dValorAntesDate = 0;
                                        decimal dValorDiscountDate = 0;
                                        decimal dValorDiscountAntesDate = 0;

                                        decimal dValorTotalDateFee = 0;
                                        decimal dValorAntesDateFee = 0;

                                        decimal dValorTotalDateFeeUSD = 0;
                                        decimal dValorAntesDateFeeUSD = 0;

                                        decimal dValorTotalDateFeeIni = 0;
                                        decimal dValorAntesDateFeeIni = 0;

                                        dValorTotalDate = clsValidaciones.getDecimalNotRound(oResponse.HotelFare.Rooms[i].RoomTypes[k].EffectiveDates[m].AmountAfterTax.ToString());
                                        dValorAntesDate = clsValidaciones.getDecimalNotRound(oResponse.HotelFare.Rooms[i].RoomTypes[k].EffectiveDates[m].AmountBeforeTax.ToString());
                                        dValorDiscountDate = clsValidaciones.getDecimalNotRound(oResponse.HotelFare.Rooms[i].RoomTypes[k].EffectiveDates[m].DiscountAfterTax.ToString());
                                        dValorDiscountAntesDate = clsValidaciones.getDecimalNotRound(oResponse.HotelFare.Rooms[i].RoomTypes[k].EffectiveDates[m].DiscountBeforeTax.ToString());

                                        dValorTotalDateFee = clsValidaciones.getDecimalNotRound((dValorTotalDate * dIncremento).ToString());
                                        dValorAntesDateFee = clsValidaciones.getDecimalNotRound((dValorAntesDate * dIncremento).ToString());

                                        dValorTotalDateFeeUSD = clsValidaciones.getDecimalNotRound((dValorTotalDate + dMarckupUsd).ToString());
                                        dValorAntesDateFeeUSD = clsValidaciones.getDecimalNotRound((dValorAntesDate + dMarckupUsd).ToString());

                                        dValorTotalDateFeeIni = clsValidaciones.getDecimalNotRound((dValorTotalDate).ToString());
                                        dValorAntesDateFeeIni = clsValidaciones.getDecimalNotRound((dValorAntesDate).ToString());

                                        drFilaPrice[COLUMN_HOTEL_ROOM_ID] = iRoomId;
                                        drFilaPrice[COLUMN_DATE] = oResponse.HotelFare.Rooms[i].RoomTypes[k].EffectiveDates[m].Date.ToString(FORMATO_FECHA_BD);

                                        drFilaPrice[COLUMN_PRICE_AMOUN_AFTER_TAX] = dValorTotalDate;
                                        drFilaPrice[COLUMN_PRICE_AMOUN_BEFORE_TAX] = dValorAntesDate;
                                        drFilaPrice[COLUMN_PRICE_DISCOUNT_AFTER_TAX] = dValorDiscountDate;
                                        drFilaPrice[COLUMN_PRICE_DISCOUNT_BEFORE_TAX] = dValorDiscountAntesDate;

                                        drFilaPrice[COLUMN_PRICE_AMOUN_AFTER_TAX_FEE] = dValorTotalDateFee;
                                        drFilaPrice[COLUMN_PRICE_AMOUN_BEFORE_TAX_FEE] = dValorAntesDateFee;
                                        drFilaPrice[COLUMN_PRICE_DISCOUNT_AFTER_TAX_FEE] = dValorDiscountDate;
                                        drFilaPrice[COLUMN_PRICE_DISCOUNT_BEFORE_TAX_FEE] = dValorDiscountAntesDate;

                                        dValorTotal += dValorTotalDateFee;
                                        dValorAntes += dValorAntesDateFee;

                                        dValorTotalUSD += dValorTotalDateFeeUSD;
                                        dValorAntesUSD += dValorAntesDateFeeUSD;

                                        dValorTotalIni += dValorTotalDateFeeIni;
                                        dValorAntesIni += dValorAntesDateFeeIni;

                                        dtPriceDate.Rows.Add(drFilaPrice);
                                    }
                                    decimal dValorDiferencia = dValorTotal - dValorTotalIni;

                                    //if (dValorDiferencia > dMarckupUsd)
                                    //{
                                    //    dValorTotal = dValorTotalIni + dMarckupUsd;
                                    //    dValorAntes = dValorAntesIni + dMarckupUsd;
                                    //    sTipoMk = "V";
                                    //}
                                    dValorTotal = clsValidaciones.getDecimalRound(dValorTotal.ToString(), 0, true);
                                    dValorAntes = clsValidaciones.getDecimalRound(dValorAntes.ToString(), 0, true);

                                    decimal dTotalView = dValorTotal / dFactor;
                                    dTotalView = clsValidaciones.getDecimalRound(dTotalView.ToString());

                                    drFilaRoom[COLUMN_CURRENCY_TEST] = sMonedaView;
                                    drFilaRoom[COLUMN_CURRENCY_CODE] = sMoneda;
                                    drFilaRoom[COLUMN_AMOUNT] = dValorTotal;
                                    drFilaRoom[COLUMN_AMOUNT_TEXT] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                                    drFilaRoom[COLUMN_TOTAL_PRICE_TEXT] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                                    drFilaRoom[COLUMN_TOTAL_PRICE] = dValorTotal;
                                    drFilaRoom[COLUMN_TOTAL_AMOUNT] = dValorTotal;
                                    drFilaRoom[COLUMN_TIPO_MK] = sTipoMk;
                                    if (sTipoMk.Equals("P"))
                                    {
                                        drFilaRoom[COLUMN_VALOR_MK] = dIncremento;
                                    }
                                    else
                                    {
                                        drFilaRoom[COLUMN_VALOR_MK] = dMarckupUsd;
                                    }


                                    drFilaRoom[COLUMN_BOARD_CODE] = "";
                                    drFilaRoom[COLUMN_BOARD_TEXT] = "";

                                    drFilaRoom[COLUMN_BOARD_SHORT_NAME] = sBoard;
                                    drFilaRoom[COLUMN_HOTEL_INFO_ID] = iAumento;

                                    drFilaRoom[COLUMN_DATE_FROM] = sDateFrom;
                                    drFilaRoom[COLUMN_DATE_TO] = sDateTo;
                                    drFilaRoom[COLUMN_DATE_FROM_FORMAT] = sDateFrom;
                                    drFilaRoom[COLUMN_DATE_TO_FORMAT] = sDateTo;

                                    drFilaRoom[COLUMN_ADULT_COUNT] = vo_HotelValuedAvailRQ.TotalAdult;
                                    drFilaRoom[COLUMN_CHILD_COUNT] = vo_HotelValuedAvailRQ.TotalChild;
                                    drFilaRoom[COLUMN_ROOM_COUNT] = vo_HotelValuedAvailRQ.TotalRoom;


                                    dtHotelRoom.Rows.Add(drFilaRoom);

                                    iRoomId++;
                                }
                            }
                            else
                            {

                                for (int b = 0; b < oResponse.HotelFare.Rooms.Length; b++)
                                {
                                    if (oResponse.HotelFare.Rooms[b].RoomTypes.Length > 0)
                                    {
                                        for (int k = 0; k < oResponse.HotelFare.Rooms[b].RoomTypes.Length; k++)
                                        {
                                            dtHotelRoom.Rows[b][COLUMN_CHARASTERISTIC] = oResponse.HotelFare.Rooms[b].RoomTypes[k].InvBlockCode;
                                            dtHotelRoom.Rows[b][COLUMN_ROOM_TTL] = oResponse.HotelFare.Rooms[b].RoomTypes[k].TTL;
                                        }
                                    }
                                }
                            }
                        }
                        if (oResponse.HotelFare.Rooms[i].CancelationPolicies.Length > 0)
                        {
                            for (int j = 0; j < oResponse.HotelFare.Rooms[i].CancelationPolicies.Length; j++)
                            {
                                DataRow drFilaCancelacion = dtHotelCancelacion.NewRow();

                                decimal dValorCancel = 0;
                                decimal dValorCancelFee = 0;
                                dValorCancel = clsValidaciones.getDecimalNotRound(oResponse.HotelFare.Rooms[i].CancelationPolicies[j].Amount.ToString());
                                dValorCancel += clsValidaciones.getDecimalNotRound(oResponse.HotelFare.Rooms[i].CancelationPolicies[j].TaxAmount.ToString());
                                dValorCancelFee = dValorCancel * dIncremento;
                                dValorCancelFee = clsValidaciones.getDecimalRound(dValorCancelFee.ToString(), 0, true);

                                decimal dTotalView = dValorCancelFee / dFactor;
                                dTotalView = clsValidaciones.getDecimalRound(dTotalView.ToString());

                                drFilaCancelacion[COLUMN_CANCELATION_TOTAL_AMOUNT] = dValorCancelFee;
                                drFilaCancelacion[COLUMN_CANCELATION_TOTAL_AMOUNT_TEXT] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                                drFilaCancelacion[COLUMN_CANCELATION_TOTAL_DAYS] = oResponse.HotelFare.Rooms[i].CancelationPolicies[j].NmbrOfNights;
                                drFilaCancelacion[COLUMN_CURRENCY_TEST] = sMonedaView;
                                drFilaCancelacion[COLUMN_CURRENCY_CODE] = sMoneda;
                                drFilaCancelacion[COLUMN_CANCELATION_AMOUNT_DAY] = dValorCancelFee;
                                drFilaCancelacion[COLUMN_CANCELATION_AMOUNT_DAY_TEXT] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                                drFilaCancelacion[COLUMN_CANCELATION_PRICE_ID] = j;

                                drFilaCancelacion[COLUMN_DATE_TIME_FROM] = sDate;
                                drFilaCancelacion[COLUMN_DATE] = sDate;
                                drFilaCancelacion[COLUMN_DATE_TIME] = sDate;
                                drFilaCancelacion[COLUMN_DATE_TIME_TO] = sDate;
                                drFilaCancelacion[COLUMN_DATE_TIME_FROM_FORMAT] = sDate;
                                drFilaCancelacion[COLUMN_DATE_TIME_TO_FORMAT] = sDate;

                                drFilaCancelacion[COLUMN_AMOUNT] = dValorCancelFee;
                                drFilaCancelacion[COLUMN_CANCELATION_TOTAL_VALUE] = dValorCancelFee;

                                drFilaCancelacion[COLUMN_CANCELATION_FEES] = oResponse.HotelFare.Rooms[i].CancelationPolicies[j].FeesInclusive;
                                drFilaCancelacion[COLUMN_CANCELATION_MULTIPLIER] = oResponse.HotelFare.Rooms[i].CancelationPolicies[j].Multiplier;
                                drFilaCancelacion[COLUMN_CANCELATION_DROPTIME] = oResponse.HotelFare.Rooms[i].CancelationPolicies[j].OffsetDropTime;
                                drFilaCancelacion[COLUMN_CANCELATION_TIMEOUT] = oResponse.HotelFare.Rooms[i].CancelationPolicies[j].OffsetTimeUnit;
                                drFilaCancelacion[COLUMN_CANCELATION_TAXAMOUNT] = oResponse.HotelFare.Rooms[i].CancelationPolicies[j].TaxAmount;
                                if (sObservaciones.Length > 0)
                                {
                                    sObservaciones = sObservaciones + "<br /><br />";
                                }

                                int iHorasPenalidad = oResponse.HotelFare.Rooms[i].CancelationPolicies[j].Multiplier;
                                string sTipoTime = " horas ";
                                HotelConfirm.OffsetTimeUnit oOffsetTimeUnit = oResponse.HotelFare.Rooms[i].CancelationPolicies[j].OffsetTimeUnit;


                                if (oOffsetTimeUnit.Equals(HotelConfirm.OffsetTimeUnit.Hour))
                                {
                                    if (iHorasPenalidad > 72)
                                    {
                                        iHorasPenalidad = iHorasPenalidad / 24;
                                        sTipoTime = " dias ";
                                    }
                                }
                                else
                                {
                                    sTipoTime = " dias ";
                                }

                                if (iHorasPenalidad >= 365)
                                {
                                    sObservaciones = "!! ESTA TARIFA ES NO REEMBOLSABLE !!";
                                    iReembolsable = 1;
                                }
                                else
                                {
                                    if (oResponse.HotelFare.Rooms[i].CancelationPolicies[j].OffsetDropTime.Equals(HotelConfirm.OffsetDropTime.BeforeArrival))
                                    {
                                        sObservaciones = sObservaciones + "Cancelación de una reserva dentro " + iHorasPenalidad.ToString() + sTipoTime + "de checkin incurrirá en una penalidad de" + " USD " + dValorCancelFee.ToString(FORMATO_NUMEROS_SD);
                                    }
                                    else
                                    {
                                        if (oResponse.HotelFare.Rooms[i].CancelationPolicies[j].OffsetDropTime.Equals(HotelConfirm.OffsetDropTime.AfterBooking))
                                        {
                                            sObservaciones = sObservaciones + "Cancelación de una reserva" + iHorasPenalidad.ToString() + sTipoTime + "después de la confirmación de la reserva incurrirá en una penalidad de" + " USD " + dValorCancelFee.ToString(FORMATO_NUMEROS_SD);
                                        }
                                    }
                                }
                                try
                                {
                                    drFilaCancelacion[COLUMN_HOTEL_ROOM_ID] = int.Parse(dtHotelRoom.Rows[i][COLUMN_HOTEL_ROOM_ID].ToString());
                                }
                                catch { drFilaCancelacion[COLUMN_HOTEL_ROOM_ID] = i; }

                                dtHotelCancelacion.Rows.Add(drFilaCancelacion);

                                DataRow drFilaCancelacionDate = dtCancelationDate.NewRow();

                                drFilaCancelacionDate[COLUMN_DATE] = sDate;
                                try
                                {
                                    drFilaCancelacionDate[COLUMN_HOTEL_ROOM_ID] = int.Parse(dtHotelRoom.Rows[i][COLUMN_HOTEL_ROOM_ID].ToString());
                                }
                                catch { drFilaCancelacionDate[COLUMN_HOTEL_ROOM_ID] = i; }

                                dtCancelationDate.Rows.Add(drFilaCancelacionDate);
                            }
                        }
                    }
                    dtHotelInfo.Rows[0][COLUMN_OBSERVACIONES] = sObservaciones;
                    dtHotelInfo.Rows[0][COLUMN_NON_REEMBOLSABLE] = iReembolsable;
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Message = "No se encontraron resultados";
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    cParametros.Complemento = "Resultados de Hoteles";
                    cParametros.ViewMessage.Add("No existen resultados para esta búsqueda: ");
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    ExceptionHandled.Publicar(cParametros);
                   
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.Complemento = "Resultados de Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda: " + cParametros.Message);
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                ExceptionHandled.Publicar(cParametros);
                clsError.getError(dsData, cParametros);
            }
            return cParametros;
        }
        public clsParametros GetDatasetRoomSelect(DataSet dsData)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {
                DataTable dtHotelInfo = dsData.Tables[TABLA_HOTEL_INFO];
                DataTable dtHotelRoom = dsData.Tables[TABLA_HOTEL_ROOM];
                DataTable dtPriceDate = dsData.Tables[TABLA_PRICE];
                int n = 0;
                if (dtHotelRoom.Rows.Count > 0)
                {
                    while (n < dtHotelRoom.Rows.Count)
                    {
                        bool bEliminar = false;
                        if (!dtHotelRoom.Rows[n][COLUMN_SELECCION].ToString().Equals("1"))
                        {
                            bEliminar = true;
                        }
                        if (bEliminar)
                        {
                            dtHotelRoom.Rows.Remove(dtHotelRoom.Rows[n]);
                            n = 0;
                        }
                        else
                        {
                            n++;
                        }
                    }
                    dtHotelRoom.AcceptChanges();
                    n = 0;
                    if (dtHotelInfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtHotelRoom.Rows.Count; i++)
                        {
                            while (n < dtHotelInfo.Rows.Count)
                            {
                                bool bEliminar = false;
                                if (!dtHotelInfo.Rows[n][COLUMN_HOTEL_INFO_ID].ToString().Equals(dtHotelRoom.Rows[i][COLUMN_HOTEL_INFO_ID].ToString()))
                                {
                                    bEliminar = true;
                                }
                                if (bEliminar)
                                {
                                    dtHotelInfo.Rows.Remove(dtHotelInfo.Rows[n]);
                                    n = 0;
                                }
                                else
                                {
                                    n++;
                                }
                            }
                        }
                    }
                    dtHotelInfo.AcceptChanges();
                    n = 0;
                    if (dtPriceDate.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtHotelRoom.Rows.Count; i++)
                        {
                            while (n < dtPriceDate.Rows.Count)
                            {
                                if (dtPriceDate.Rows[n][COLUMN_HOTEL_ROOM_ID].ToString().Equals(dtHotelRoom.Rows[i][COLUMN_HOTEL_ROOM_ID].ToString()))
                                {
                                    dtPriceDate.Rows[n][COLUMN_SELECCION] = "1";
                                }
                                else
                                {
                                    dtPriceDate.Rows[n][COLUMN_SELECCION] = "0";
                                }
                                n++;
                            }
                        }
                            while (n < dtPriceDate.Rows.Count)
                            {
                                bool bEliminar = false;
                                if (!dtPriceDate.Rows[n][COLUMN_SELECCION].ToString().Equals("1"))
                                {
                                    bEliminar = true;
                                }
                                if (bEliminar)
                                {
                                    dtPriceDate.Rows.Remove(dtPriceDate.Rows[n]);
                                    n = 0;
                                }
                                else
                                {
                                    n++;
                                }
                        }
                    }
                    dtPriceDate.AcceptChanges();
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.Complemento = "Seleccion de RoomType";
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        /// <summary>
        /// Metodo para crear el objeto de cancelacion
        /// </summary>
        /// <param name="oResponse">Objeto devuelto por el webservices</param>
        /// <returns>cParametros, objeto de error</returns>
        /// <remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-12-22
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:          
        /// Descripción:    
        /// </remarks>
        public clsParametros GetDatasetHotelCancel(HotelCancelRS oResponse)
        {
            clsParametros cParametros = new clsParametros();
            DataTable dtHotelRoom = setTableHotelRoom();
            try
            {
                cParametros.Id = 1;
                cParametros.DatoAdic = oResponse.CancellationFeeAmount.ToString();
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.DatoAdic = "0";
                cParametros.Message = Ex.Message.ToString();
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = "HotelCancelRQ";
                cParametros.Complemento = "Error al cancelar la reserva";
                cParametros.ViewMessage.Add("No se realizo la cancelacion de la reserva");
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                cParametros.Code = "503";
                cParametros.ValidaInfo = false;
                cParametros.MessageBD = true;
                cParametros.TipoWs = Enum_ProveedorWebServices.TotalTrip;

                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        public void GetDatasetHotelInfoWs(DataSet dsData, HotelInfoRS oResponse, string sCountry, string sCodeCountry, string sCity, string sCodeCity)
        {
            clsParametros cParametros = new clsParametros();
            DataTable dtHotelInfo = setTableHotelInfo();
            DataTable dtHotelRoom = setTableHotelRoom();
            DataTable dtHotelOcupancy = setTableHotelOcupancy();
            DataTable dtHotelValueAvail = setTableHotelValueAvail();
            DataTable dtHotelPaginacionData = setTableHotelPaginacionData();

            DataTable dtHotelFacility = setTableHotelFacility();
            DataTable dtHotelImages = setTableHotelImages();
            DataTable dtHotelInstalaciones = setTableHotelInstalaciones();

            DataTable dtCancalacion = setTableCancelation();
            DataTable dtCostoAdicional = setTableCosteAditional();
            DataTable dtPriceDate = setTablePriceDate();

            DataTable dtCancelationDate = setTableCancelationDate();
            DataTable dtSupplier = setTableSupplier();
            DataTable dtComment = setTableComment();

            string sWsSelect = clsValidaciones.GetKeyOrAdd("WS_HOTEL_TT", "WS_TT");
            int iAumento = 1000;

            try
            {
                //TRAEMOS LOS RESULTADOS DE BUSQUEDA GUARDADOS 
                if (oResponse.HotelInfos.Length > 0)
                {
                    for (int i = 0; i < oResponse.HotelInfos.Length; i++)
                    {
                        DataRow drFila = dtHotelInfo.NewRow();

                        drFila[COLUMN_WS_SELECT] = sWsSelect;
                        drFila[COLUMN_COUNTRY_CODE] = sCodeCountry;
                        drFila[COLUMN_COUNTRY__NAME] = sCountry;
                        drFila[COLUMN_HOTELCODE] = oResponse.HotelInfos[i].HotelCode;
                        drFila[COLUMN_HOTEL_INFO_ID] = iAumento;
                        drFila[COLUMN_HOTEL_INFO_CODE] = oResponse.HotelInfos[i].HotelCode;
                        drFila[COLUMN_NAME] = oResponse.HotelInfos[i].HotelName;
                        if (oResponse.HotelInfos[i].Rating != null)
                        {
                            if (oResponse.HotelInfos[i].Rating.Length > 0)
                            {
                                drFila[COLUMN_CATEGORY_CODE] =oResponse.HotelInfos[i].Rating.ToString().Trim().TrimEnd().TrimStart();
                                drFila[COLUMN_CATEGORY_TEXT] = oResponse.HotelInfos[i].Rating.ToString().Trim().TrimEnd().TrimStart() + " Estrellas";
                            }
                            else
                            {
                                drFila[COLUMN_CATEGORY_CODE] = "0";
                                drFila[COLUMN_CATEGORY_TEXT] = "Sin Clasificacion";
                            }
                        }
                        drFila[COLUMN_DESTINATION_CODE] = oResponse.HotelInfos[i].CityCode;
                        drFila[COLUMN_DESTINATION_NAME] = sCity;
                        drFila[COLUMN_ZONE_TEXT] = oResponse.HotelInfos[i].CityCode;
                        drFila[COLUMN_CLASIFICATION_CODE] = oResponse.HotelInfos[i].Rating;
                        drFila[COLUMN_CLASIFICATION_TEXT] = oResponse.HotelInfos[i].Rating;
                        drFila[COLUMN_DETALLES_URL] = oResponse.HotelInfos[i].HotelCode;

                        drFila[COLUMN_DESCRIPTION] = oResponse.HotelInfos[i].Description;
                        drFila[COLUMN_ADDRESS] = oResponse.HotelInfos[i].Address;

                        if (oResponse.HotelInfos[i].Images.Length > 0)
                            drFila[COLUMN_IMAGEN_URL] = oResponse.HotelInfos[i].Images[0].ToString();

                       

                        drFila[COLUMN_DETALLES_URL] = "~/presentacion/Detalle_Hotel.aspx?ID=" + oResponse.HotelInfos[i].HotelCode.ToString();


                        drFila[COLUMN_LATITUDE] = oResponse.HotelInfos[i].Latitude;
                        drFila[COLUMN_LONGITUDE] = oResponse.HotelInfos[i].Longitude;
                        drFila[COLUMN_DESCRIPTION] = oResponse.HotelInfos[i].Description;
                        drFila[COLUMN_DESCRIPTION_LONG] = oResponse.HotelInfos[i].LongDescription;
                        drFila[COLUMN_ADDRESS] = oResponse.HotelInfos[i].Address;
                        drFila[COLUMN_HOTEL_TELEPHONE_NUMBER] = oResponse.HotelInfos[i].Phone;
                        dtHotelInfo.Rows.Add(drFila);

                        if (oResponse.HotelInfos[i].Images.Length > 0)
                        {
                            drFila[COLUMN_IMAGEN_URL] = oResponse.HotelInfos[i].Images[0].ToString();
                            for (int h = 0; h < oResponse.HotelInfos[i].Images.Length; h++)
                            {
                                int iPosGaleria = h + 1;
                                DataRow drFilaImage = dtHotelImages.NewRow();

                                drFilaImage[COLUMN_HOTEL_INFO_CODE] = oResponse.HotelInfos[i].HotelCode;
                                drFilaImage[COLUMN_HOTEL_INFO_ID] = drFila[COLUMN_HOTEL_INFO_ID];
                                drFilaImage[COLUMN_FACILITIES_DESCRIPTION] = "Galeria " + iPosGaleria.ToString();
                                drFilaImage[COLUMN_FACILITIES_IMAGEPATH] = oResponse.HotelInfos[i].Images[h].ToString();

                                dtHotelImages.Rows.Add(drFilaImage);
                            }
                        }
                        if (oResponse.HotelInfos[i].Ammenities.Length > 0)
                        {
                            for (int k = 0; k < oResponse.HotelInfos[i].Ammenities.Length; k++)
                            {
                                DataRow drFilaFacility = dtHotelFacility.NewRow();

                                drFilaFacility[COLUMN_HOTEL_INFO_CODE] = oResponse.HotelInfos[i].HotelCode;
                                drFilaFacility[COLUMN_HOTEL_INFO_ID] = drFila[COLUMN_HOTEL_INFO_ID];
                                drFilaFacility[COLUMN_FACILITIES_GROUP] = 60;
                                drFilaFacility[COLUMN_FACILITIES_CODE] = oResponse.HotelInfos[i].Ammenities[k].ToString();
                                drFilaFacility[COLUMN_FACILITIES_DESCRIPTION] = oResponse.HotelInfos[i].Ammenities[k].ToString();
                                drFilaFacility[COLUMN_NAME] = oResponse.HotelInfos[i].Ammenities[k].ToString();

                                dtHotelFacility.Rows.Add(drFilaFacility);
                            }
                        }
                        iAumento++;
                    }
                    dsData.Tables.Add(dtHotelInfo);
                    dsData.Tables.Add(dtHotelRoom);
                    dsData.Tables.Add(dtHotelOcupancy);
                    dsData.Tables.Add(dtHotelValueAvail);
                    dsData.Tables.Add(dtHotelPaginacionData);
                    dsData.Tables.Add(dtHotelFacility);
                    dsData.Tables.Add(dtHotelImages);
                    dsData.Tables.Add(dtHotelInstalaciones);
                    dsData.Tables.Add(dtCancalacion);
                    dsData.Tables.Add(dtCostoAdicional);
                    dsData.Tables.Add(dtPriceDate);
                    dsData.Tables.Add(dtCancelationDate);
                    dsData.Tables.Add(dtSupplier);
                    dsData.Tables.Add(dtComment);
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Message = "No se encontraron resultados";
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    cParametros.Complemento = "Resultados de Hoteles";
                    cParametros.ViewMessage.Add("No existen resultados para esta búsqueda: " + cParametros.Message);
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    ExceptionHandled.Publicar(cParametros);
                    clsError.getError(dsData, cParametros);
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.Complemento = "Resultados de Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda: " + cParametros.Message);
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                ExceptionHandled.Publicar(cParametros);
                clsError.getError(dsData, cParametros);
            }
        }
        public void GetDatasetHotelInfo(DataSet dsData, HotelInfoRS oResponse)
        {
            clsParametros cParametros = new clsParametros();
            DataTable dtHotelInfo = dsData.Tables[TABLA_HOTEL_INFO];
            DataTable dtHotelImages = dsData.Tables[TABLA_FACILITIES_IMAGEN];
            DataTable dtHotelFacility = dsData.Tables[TABLA_INSTALACIONES];
            try
            {
                //TRAEMOS LOS RESULTADOS DE BUSQUEDA GUARDADOS 
                if (oResponse.HotelInfos.Length > 0)
                {
                    for (int i = 0; i < oResponse.HotelInfos.Length; i++)
                    {
                        foreach (DataRow drFila in dtHotelInfo.Rows)
                        {
                            if (drFila[COLUMN_HOTELCODE].ToString().Equals(oResponse.HotelInfos[i].HotelCode.ToString()))
                            {
                                drFila[COLUMN_LATITUDE] = oResponse.HotelInfos[i].Latitude;
                                drFila[COLUMN_LONGITUDE] = oResponse.HotelInfos[i].Longitude;
                                drFila[COLUMN_DESCRIPTION] = oResponse.HotelInfos[i].Description;
                                drFila[COLUMN_DESCRIPTION_LONG] = oResponse.HotelInfos[i].LongDescription;
                                drFila[COLUMN_ADDRESS] = oResponse.HotelInfos[i].Address;
                                drFila[COLUMN_HOTEL_TELEPHONE_NUMBER] = oResponse.HotelInfos[i].Phone;
                                if (oResponse.HotelInfos[i].Images.Length > 0)
                                {
                                    drFila[COLUMN_IMAGEN_URL] = oResponse.HotelInfos[i].Images[0].ToString();
                                    for (int h = 0; h < oResponse.HotelInfos[i].Images.Length; h++)
                                    {
                                        int iPosGaleria = h + 1;
                                        DataRow drFilaImage = dtHotelImages.NewRow();

                                        drFilaImage[COLUMN_HOTEL_INFO_CODE] = oResponse.HotelInfos[i].HotelCode;
                                        drFilaImage[COLUMN_HOTEL_INFO_ID] = drFila[COLUMN_HOTEL_INFO_ID];
                                        drFilaImage[COLUMN_FACILITIES_DESCRIPTION] = "Galeria " + iPosGaleria.ToString();
                                        drFilaImage[COLUMN_FACILITIES_IMAGEPATH] = oResponse.HotelInfos[i].Images[h].ToString();

                                        dtHotelImages.Rows.Add(drFilaImage);
                                    }
                                }
                                if (oResponse.HotelInfos[i].Ammenities.Length > 0)
                                {
                                    for (int k = 0; k < oResponse.HotelInfos[i].Ammenities.Length; k++)
                                    {
                                        DataRow drFilaFacility = dtHotelFacility.NewRow();

                                        drFilaFacility[COLUMN_HOTEL_INFO_CODE] = oResponse.HotelInfos[i].HotelCode;
                                        drFilaFacility[COLUMN_HOTEL_INFO_ID] = drFila[COLUMN_HOTEL_INFO_ID];
                                        drFilaFacility[COLUMN_FACILITIES_GROUP] = 60;
                                        drFilaFacility[COLUMN_FACILITIES_CODE] = oResponse.HotelInfos[i].Ammenities[k].ToString();
                                        drFilaFacility[COLUMN_FACILITIES_DESCRIPTION] = oResponse.HotelInfos[i].Ammenities[k].ToString();
                                        drFilaFacility[COLUMN_NAME] = oResponse.HotelInfos[i].Ammenities[k].ToString();

                                        dtHotelFacility.Rows.Add(drFilaFacility);
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Message = "No se encontraron resultados";
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    cParametros.Complemento = "Resultados de Hoteles";
                    cParametros.ViewMessage.Add("No existen resultados para esta búsqueda: " + cParametros.Message);
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    ExceptionHandled.Publicar(cParametros);
                    clsError.getError(dsData, cParametros);
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.Complemento = "Resultados de Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda: " + cParametros.Message);
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                ExceptionHandled.Publicar(cParametros);
                clsError.getError(dsData, cParametros);
            }
        }
        public clsResultados GetDatasetHotel(DataSet dsData, HotelReservationInfoRS oResponse)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            cParametros.Id = 1;
            DataTable dtHotelInfo = setTableHotelInfo();
            DataTable dtHotelRoom = setTableHotelRoom();
            DataTable dtHotelOcupancy = setTableHotelOcupancy();
            DataTable dtHotelValueAvail = setTableHotelValueAvail();
            DataTable dtHotelPaginacionData = setTableHotelPaginacionData();

            DataTable dtHotelFacility = setTableHotelFacility();
            DataTable dtHotelImages = setTableHotelImages();
            DataTable dtHotelInstalaciones = setTableHotelInstalaciones();

            DataTable dtCancalacion = setTableCancelation();
            DataTable dtCostoAdicional = setTableCosteAditional();
            DataTable dtPriceDate = setTablePriceDate();

            DataTable dtCancelationDate = setTableCancelationDate();
            DataTable dtSupplier = setTableSupplier();
            DataTable dtComment = setTableComment();

            VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();

            string sDateFrom = clsValidaciones.ConverFechaSinSeparadorYMD(vo_HotelValuedAvailRQ.CheckInDate);
            string sDateTo = clsValidaciones.ConverFechaSinSeparadorYMD(vo_HotelValuedAvailRQ.CheckOutDate);
            string sWsSelect = clsValidaciones.GetKeyOrAdd("WS_HOTEL_TT", "WS_TT");
            decimal dIncremento = (1 + Convert.ToDecimal(clsValidaciones.GetKeyOrAdd("IncrementoHotelTT", "10")) / 100);
            string sBoard = clsValidaciones.GetKeyOrAdd("SinAlimentacion", "SH");
            try
            {
                //TRAEMOS LOS RESULTADOS DE BUSQUEDA GUARDADOS 
                int iRoomId = 0;
                int iTotalResult = 1;
                int iTotalPorPagina = int.Parse(clsValidaciones.RetornaNumero(clsValidaciones.GetKeyOrAdd("ResulHotelPagina", "20")));
                int iTotalPaginas = iTotalResult / iTotalPorPagina;
                if (clsValidaciones.GetKeyOrAdd("PaginacionFija", "false").ToString().ToUpper().Equals("TRUE") && iTotalPaginas > Convert.ToInt32(clsValidaciones.GetKeyOrAdd("CantidadPaginas", "8")))
                {
                    iTotalPaginas = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("CantidadPaginas", "8"));
                    iTotalPorPagina = iTotalResult / iTotalPaginas;
                }

                DataRow drFilaVA = dtHotelValueAvail.NewRow();

                drFilaVA[COLUMN_HOTEL_VALUE_AVAIL] = 0;
                drFilaVA[COLUMN_TOTAL_ITEMS] = iTotalResult;
                dtHotelValueAvail.Rows.Add(drFilaVA);

                DataRow drFilaPD = dtHotelPaginacionData.NewRow();

                drFilaPD[COLUMN_CURRENT_PAGE] = iTotalPorPagina;
                drFilaPD[COLUMN_TOTAL_PAGES] = iTotalPaginas;
                dtHotelPaginacionData.Rows.Add(drFilaPD);

                decimal dValorMinimo = 0;
                DataRow drFila = dtHotelInfo.NewRow();

                drFila[COLUMN_WS_SELECT] = sWsSelect;
                drFila[COLUMN_HOTELCODE] = oResponse.HotelFare.Hot.HotelCode;
                drFila[COLUMN_HOTEL_INFO_ID] = 0;
                drFila[COLUMN_HOTEL_INFO_CODE] = oResponse.HotelFare.Hot.HotelCode;
                drFila[COLUMN_NAME] = oResponse.HotelFare.Hot.HotelName;
                if (oResponse.HotelFare.Hot.Rating != null)
                {
                    drFila[COLUMN_CATEGORY_CODE] = oResponse.HotelFare.Hot.Rating.ToString() + "EST";
                    drFila[COLUMN_CATEGORY_TEXT] = oResponse.HotelFare.Hot.Rating.ToString() + " ESTRELLAS";
                }
                else
                {
                    drFila[COLUMN_CATEGORY_CODE] = "0EST";
                    drFila[COLUMN_CATEGORY_TEXT] = "0 ESTRELLAS";
                }
                drFila[COLUMN_DESTINATION_CODE] = oResponse.HotelFare.Hot.CityCode;
                drFila[COLUMN_DESTINATION_NAME] = oResponse.HotelFare.Hot.CityCode;
                drFila[COLUMN_ZONE_TEXT] = oResponse.HotelFare.Hot.CityCode;
                drFila[COLUMN_DETALLES_URL] = oResponse.HotelFare.Hot.HotelCode;
                drFila[COLUMN_HOTEL_TELEPHONE_NUMBER] = oResponse.HotelFare.Hot.Phone;

                drFila[COLUMN_DESCRIPTION] = oResponse.HotelFare.Hot.Description;
                drFila[COLUMN_ADDRESS] = oResponse.HotelFare.Hot.Address;
                if (oResponse.HotelFare.Hot.Images.Length > 0)
                    drFila[COLUMN_IMAGEN_URL] = oResponse.HotelFare.Hot.Images[0].ToString();

                drFila[COLUMN_DATE_FROM] = sDateFrom;
                drFila[COLUMN_DATE_FROM_FORMAT] = sDateFrom;
                drFila[COLUMN_DATE_TO] = sDateTo;
                drFila[COLUMN_DATE_TO_FORMAT] = sDateTo;

                drFila[COLUMN_DETALLES_URL] = "~/presentacion/Detalle_Hotel.aspx?ID=" + oResponse.HotelFare.Hot.HotelCode.ToString();
                string sMonedaView =sMonedaHotel;
                string sMoneda = sMonedaHotel;
               

                decimal dFactor = 1;
                try
                {
                    clsCache cCache = new csCache().cCache();
                    
                }
                catch { }
               
                for (int h = 0; h < oResponse.HotelFare.Rooms.Length; h++)
                {
                    for (int k = 0; k < oResponse.HotelFare.Rooms[h].RoomTypes.Length; k++)
                    {
                        decimal dValorTotal = 0;
                        decimal dValorAntes = 0;

                        DataRow drFilaRoom = dtHotelRoom.NewRow();

                        try
                        {
                            if (oResponse.HotelFare.Rooms[h].RoomTypes[k].RoomRate.ToString().Contains("SP"))
                            {
                                drFila[COLUMN_OFERTA] = "../App_Themes/Imagenes/oferta.gif";
                            }
                            else
                            {
                                drFila[COLUMN_OFERTA] = "../App_Themes/Imagenes/spacer.gif";
                            }
                        }
                        catch { drFila[COLUMN_OFERTA] = "../App_Themes/Imagenes/spacer.gif"; }

                        drFilaRoom[COLUMN_WS_SELECT] = sWsSelect;
                        drFilaRoom[COLUMN_HOTELCODE] = oResponse.HotelFare.Hot.HotelCode;
                        drFilaRoom[COLUMN_HOTEL_INFO_CODE] = oResponse.HotelFare.Hot.HotelCode;
                        drFilaRoom[COLUMN_NAME] = oResponse.HotelFare.Hot.HotelName;
                        drFilaRoom[COLUMN_HOTEL_ROOM_ID] = iRoomId;
                        drFilaRoom[COLUMN_HOTEL_INFO_ID] = 0;
                        drFilaRoom[COLUMN_HOTEL_OCCUPANCY_ID] = k;
                        drFilaRoom[COLUMN_HOTEL_ROOM_OCUPATION] = h + 1;

                        drFilaRoom[COLUMN_CONFIRM_RATEPLANTYPE] = oResponse.HotelFare.Rooms[h].RoomTypes[k].RatePlanType;
                        drFilaRoom[COLUMN_SHRUI] = oResponse.HotelFare.Rooms[h].RoomTypes[k].RoomRate;
                        drFilaRoom[COLUMN_ROOM_TYPE_TEXT] = oResponse.HotelFare.Rooms[h].RoomTypes[k].RoomDesc;
                        drFilaRoom[COLUMN_TYPE] = oResponse.HotelFare.Rooms[h].RoomTypes[k].RoomType;
                        drFilaRoom[COLUMN_CHARASTERISTIC] = oResponse.HotelFare.Rooms[h].RoomTypes[k].InvBlockCode;

                        for (int m = 0; m < oResponse.HotelFare.Rooms[h].RoomTypes[k].EffectiveDates.Length; m++)
                        {
                            DataRow drFilaPrice = dtPriceDate.NewRow();
                            decimal dValorTotalDate = 0;
                            decimal dValorAntesDate = 0;
                            decimal dValorDiscountDate = 0;
                            decimal dValorDiscountAntesDate = 0;

                            decimal dValorTotalDateFee = 0;
                            decimal dValorAntesDateFee = 0;

                            dValorTotalDate = clsValidaciones.getDecimalNotRound(oResponse.HotelFare.Rooms[h].RoomTypes[k].EffectiveDates[m].AmountAfterTax.ToString());
                            dValorAntesDate = clsValidaciones.getDecimalNotRound(oResponse.HotelFare.Rooms[h].RoomTypes[k].EffectiveDates[m].AmountBeforeTax.ToString());
                            dValorDiscountDate = clsValidaciones.getDecimalNotRound(oResponse.HotelFare.Rooms[h].RoomTypes[k].EffectiveDates[m].DiscountAfterTax.ToString());
                            dValorDiscountAntesDate = clsValidaciones.getDecimalNotRound(oResponse.HotelFare.Rooms[h].RoomTypes[k].EffectiveDates[m].DiscountBeforeTax.ToString());

                            dValorTotalDateFee = clsValidaciones.getDecimalNotRound((dValorTotalDate * dIncremento).ToString());
                            dValorAntesDateFee = clsValidaciones.getDecimalNotRound((dValorAntesDate * dIncremento).ToString());

                            drFilaPrice[COLUMN_HOTEL_ROOM_ID] = iRoomId;
                            drFilaPrice[COLUMN_DATE] = oResponse.HotelFare.Rooms[h].RoomTypes[k].EffectiveDates[m].Date.ToString(FORMATO_FECHA_BD);

                            drFilaPrice[COLUMN_PRICE_AMOUN_AFTER_TAX] = dValorTotalDate;
                            drFilaPrice[COLUMN_PRICE_AMOUN_BEFORE_TAX] = dValorAntesDate;
                            drFilaPrice[COLUMN_PRICE_DISCOUNT_AFTER_TAX] = dValorDiscountDate;
                            drFilaPrice[COLUMN_PRICE_DISCOUNT_BEFORE_TAX] = dValorDiscountAntesDate;

                            drFilaPrice[COLUMN_PRICE_AMOUN_AFTER_TAX_FEE] = dValorTotalDateFee;
                            drFilaPrice[COLUMN_PRICE_AMOUN_BEFORE_TAX_FEE] = dValorAntesDateFee;
                            drFilaPrice[COLUMN_PRICE_DISCOUNT_AFTER_TAX_FEE] = dValorDiscountDate;
                            drFilaPrice[COLUMN_PRICE_DISCOUNT_BEFORE_TAX_FEE] = dValorDiscountAntesDate;

                            dValorTotal += dValorTotalDateFee;
                            dValorAntes += dValorAntesDateFee;

                            dtPriceDate.Rows.Add(drFilaPrice);
                        }
                        dValorTotal = clsValidaciones.getDecimalRound(dValorTotal.ToString(), 0, true);
                        dValorAntes = clsValidaciones.getDecimalRound(dValorAntes.ToString(), 0, true);

                        decimal dTotalViewRoom = dValorTotal / dFactor;

                        drFilaRoom[COLUMN_CURRENCY_TEST] = sMonedaView;
                        drFilaRoom[COLUMN_CURRENCY_CODE] = sMoneda;
                        drFilaRoom[COLUMN_AMOUNT] = dValorTotal;
                        drFilaRoom[COLUMN_AMOUNT_TEXT] = dTotalViewRoom.ToString(FORMATO_NUMEROS_VIEW);
                        drFilaRoom[COLUMN_TOTAL_PRICE_TEXT] = dTotalViewRoom.ToString(FORMATO_NUMEROS_VIEW);
                        drFilaRoom[COLUMN_TOTAL_PRICE] = dValorTotal;
                        drFilaRoom[COLUMN_TOTAL_AMOUNT] = dValorTotal;

                        if (k.Equals(0))
                        {
                            dValorMinimo = dValorTotal;
                        }
                        else
                        {
                            if (dValorMinimo > dValorTotal)
                            {
                                dValorMinimo = dValorTotal;
                            }
                        }
                        drFilaRoom[COLUMN_BOARD_CODE] = "";
                        drFilaRoom[COLUMN_BOARD_TEXT] = "";
                        drFilaRoom[COLUMN_BOARD_SHORT_NAME] = sBoard;
                        drFilaRoom[COLUMN_HOTEL_INFO_ID] = 0;
                        drFilaRoom[COLUMN_DATE_FROM] = sDateFrom;
                        drFilaRoom[COLUMN_DATE_TO] = sDateTo;
                        drFilaRoom[COLUMN_DATE_FROM_FORMAT] = sDateFrom;
                        drFilaRoom[COLUMN_DATE_TO_FORMAT] = sDateTo;
                        drFilaRoom[COLUMN_ADULT_COUNT] = 1;
                        drFilaRoom[COLUMN_CHILD_COUNT] = 0;
                        drFilaRoom[COLUMN_ROOM_COUNT] = 0;

                        dtHotelRoom.Rows.Add(drFilaRoom);

                        DataRow drFilaOcupancy = dtHotelOcupancy.NewRow();

                        drFilaOcupancy[COLUMN_HOTEL_ROOM_ID] = iRoomId;
                        drFilaOcupancy[COLUMN_HOTEL_INFO_ID] = 0;
                        drFilaOcupancy[COLUMN_HOTEL_OCCUPANCY_ID] = k;
                        drFilaOcupancy[COLUMN_ROOM_COUNT] = h + 1;
                        int iPaxAdult = 0;
                        int iPaxChild = 0;

                        for (int l = 0; l < oResponse.HotelFare.Rooms[h].Paxes.Length; l++)
                        {
                            if (oResponse.HotelFare.Rooms[h].Paxes[l].PaxType.Equals(HotelShop.PaxType.Adult))
                                iPaxAdult++;
                            else
                                iPaxChild++;
                        }
                        drFilaOcupancy[COLUMN_ADULT_COUNT] = iPaxAdult;
                        drFilaOcupancy[COLUMN_CHILD_COUNT] = iPaxChild;

                        dtHotelOcupancy.Rows.Add(drFilaOcupancy);

                        DataRow drFilaSupplier = dtSupplier.NewRow();

                        drFilaSupplier[COLUMN_HOTEL_ROOM_ID] = iRoomId;
                        drFilaSupplier[COLUMN_SUPPLIER_NAME] = "";
                        drFilaSupplier[COLUMN_SUPPLIER_VATNUMBER] = "";

                        dtSupplier.Rows.Add(drFilaSupplier);

                        DataRow drFilaComment = dtComment.NewRow();

                        drFilaComment[COLUMN_HOTEL_ROOM_ID] = iRoomId;
                        drFilaComment[COLUMN_COMMENT_TEXT] = "";

                        dtComment.Rows.Add(drFilaComment);

                        iRoomId++;
                    }
                }
                decimal dTotalView = dValorMinimo / dFactor;

                drFila[COLUMN_CURRENCY_CODE] = sMoneda;
                drFila[COLUMN_CURRENCY_TEST] = sMonedaView;
                drFila[COLUMN_AMOUNT] = dValorMinimo;
                drFila[COLUMN_AMOUNT_TEXT] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                dtHotelInfo.Rows.Add(drFila);
                clsError.getTablaError(dsData);
                dsData.Tables.Add(dtHotelInfo);
                dsData.Tables.Add(dtHotelRoom);
                dsData.Tables.Add(dtHotelOcupancy);
                dsData.Tables.Add(dtHotelValueAvail);
                dsData.Tables.Add(dtHotelPaginacionData);
                dsData.Tables.Add(dtHotelFacility);
                dsData.Tables.Add(dtHotelImages);
                dsData.Tables.Add(dtHotelInstalaciones);
                dsData.Tables.Add(dtCancalacion);
                dsData.Tables.Add(dtCostoAdicional);
                dsData.Tables.Add(dtPriceDate);
                dsData.Tables.Add(dtCancelationDate);
                dsData.Tables.Add(dtSupplier);
                dsData.Tables.Add(dtComment);
                cResultados.dsResultados = dsData;
                cResultados.Error = cParametros;
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.Complemento = "Resultados de Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda: " + cParametros.Message);
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                ExceptionHandled.Publicar(cParametros);
                cResultados.Error = cParametros;
                clsError.getError(dsData, cParametros);
            }
            return cResultados;
        }
        public static string setConexionWs(string sConexion)
        {
            string sConexionAnt = sConexion;
            try
            {
                VO_Credentials vo_Credentials = clsCredenciales.Credenciales(Enum_ProveedorWebServices.TotalTrip);

                string sConexionWs = vo_Credentials.UrlWebServices;
                string[] slConexion = clsValidaciones.Lista(sConexionWs, "/");
                string sCaracter = "-";
                if (slConexion != null)
                {
                    int iPos = slConexion.Length - 1;
                    sCaracter = slConexion[iPos];
                }
                int iPosBuscar = sConexionWs.IndexOf(sCaracter);
                int iPosBuscarAnt = sConexionAnt.IndexOf(sCaracter);
                if (iPosBuscar > 0 && iPosBuscarAnt > 0)
                {
                    sConexionAnt = sConexionWs.Substring(0, iPosBuscar) + sConexionAnt.Substring(iPosBuscarAnt);
                }
                string sPaginaIdioma = clsValidaciones.GetKeyOrAdd("sPaginaIdioma", "False");
                if (sPaginaIdioma.ToUpper().Equals("TRUE"))
                {
                    string sIdioma = clsSesiones.getIdioma();
                    if (!sIdioma.ToUpper().Equals("ES"))
                    {
                        sConexionAnt = sConexionAnt.Replace("mmh", sIdioma);
                    }
                }
            }
            catch { }
            return sConexionAnt;
        }
        private void setHotelInfo(DataSet dsData, HotelShopRS oResponse)
        {
            clsParametros cParametros = new clsParametros();
            DataTable dtHotelInfo = setTableHotelInfo();
            DataTable dtHotelRoom = setTableHotelRoom();
            DataTable dtHotelOcupancy = setTableHotelOcupancy();
            DataTable dtHotelValueAvail = setTableHotelValueAvail();
            DataTable dtHotelPaginacionData = setTableHotelPaginacionData();

            DataTable dtHotelFacility = setTableHotelFacility();
            DataTable dtHotelImages = setTableHotelImages();
            DataTable dtHotelInstalaciones = setTableHotelInstalaciones();

            DataTable dtCancalacion = setTableCancelation();
            DataTable dtCostoAdicional = setTableCosteAditional();
            DataTable dtPriceDate = setTablePriceDate();

            DataTable dtCancelationDate = setTableCancelationDate();
            DataTable dtSupplier = setTableSupplier();
            DataTable dtComment = setTableComment();
            DataTable dtZone = setTableZone();

            VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();
            int TotalRoom = vo_HotelValuedAvailRQ.TotalRoom;

            string sDateFrom = clsValidaciones.ConverFechaSinSeparadorYMD(vo_HotelValuedAvailRQ.CheckInDate);
            string sDateTo = clsValidaciones.ConverFechaSinSeparadorYMD(vo_HotelValuedAvailRQ.CheckOutDate);
            string sWsSelect = clsValidaciones.GetKeyOrAdd("WS_HOTEL_TT", "WS_TT");
            decimal dIncremento = (1 + Convert.ToDecimal(clsValidaciones.GetKeyOrAdd("IncrementoHotelTT", "10")) / 100);
            string sBoard = clsValidaciones.GetKeyOrAdd("SinAlimentacion", "SH");
            decimal dMarckupUsd = decimal.Parse(clsValidaciones.GetKeyOrAdd("dMarckupUsd", "0"));
            try
            {
                //TRAEMOS LOS RESULTADOS DE BUSQUEDA GUARDADOS 
                if (oResponse.HotelFares.Length > 0)
                {
                    decimal dFactor = 1;
                    try
                    {
                        clsCache cCache = new csCache().cCache();
                        
                    }
                    catch { }

                    string sMonedaView = sMonedaHotel;
                    string sMoneda =sMonedaHotel;
                  

                    int iRoomId = 1000;
                    int iTotalResult = oResponse.HotelFares.Length;
                    int iTotalPorPagina = int.Parse(clsValidaciones.RetornaNumero(clsValidaciones.GetKeyOrAdd("ResulHotelPagina", "20")));
                    int iTotalPaginas = iTotalResult / iTotalPorPagina;
                    if (clsValidaciones.GetKeyOrAdd("PaginacionFija", "false").ToString().ToUpper().Equals("TRUE") && iTotalPaginas > Convert.ToInt32(clsValidaciones.GetKeyOrAdd("CantidadPaginas", "8")))
                    {
                        iTotalPaginas = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("CantidadPaginas", "10"));
                        iTotalPorPagina = iTotalResult / iTotalPaginas;
                    }

                    DataRow drFilaVA = dtHotelValueAvail.NewRow();

                    drFilaVA[COLUMN_HOTEL_VALUE_AVAIL] = 0;
                    drFilaVA[COLUMN_TOTAL_ITEMS] = iTotalResult;
                    dtHotelValueAvail.Rows.Add(drFilaVA);

                    DataRow drFilaPD = dtHotelPaginacionData.NewRow();
                    int iAumento = 1000;
                    drFilaPD[COLUMN_CURRENT_PAGE] = 1;
                    drFilaPD[COLUMN_TOTAL_PAGES] = iTotalPaginas;
                    drFilaPD[COLUMN_RESULT_PAGE] = iTotalPorPagina;
                    dtHotelPaginacionData.Rows.Add(drFilaPD);

                    for (int i = 0; i < oResponse.HotelFares.Length; i++)
                    {
                        iAumento++;
                        decimal dValorMinimo = 0;
                        DataRow drFila = dtHotelInfo.NewRow();

                        drFila[COLUMN_WS_SELECT] = sWsSelect;
                        drFila[COLUMN_HOTELCODE] = oResponse.HotelFares[i].Hot.HotelCode;
                        drFila[COLUMN_HOTEL_INFO_ID] = iAumento;
                        drFila[COLUMN_HOTEL_INFO_CODE] = oResponse.HotelFares[i].Hot.HotelCode;
                        drFila[COLUMN_NAME] = oResponse.HotelFares[i].Hot.HotelName;
                        if (oResponse.HotelFares[i].Hot.Rating != null)
                        {
                            drFila[COLUMN_CATEGORY_CODE] = oResponse.HotelFares[i].Hot.Rating.Trim().TrimStart().TrimEnd().ToString() + "EST";
                            drFila[COLUMN_CATEGORY_TEXT] = oResponse.HotelFares[i].Hot.Rating.ToString().Trim().TrimStart().TrimEnd() + "EST";                            

                        }

                        if (oResponse.HotelFares[i].Hot.Ammenities.Length > 0)
                        {
                            for (int b = 0; b < oResponse.HotelFares[i].Hot.Ammenities.Length; b++)
                            {
                                DataRow drFila1 = dtHotelFacility.NewRow();
                                drFila1[COLUMN_HOTEL_INFO_CODE] = oResponse.HotelFares[i].Hot.HotelCode;
                                drFila1[COLUMN_HOTEL_INFO_ID] = iAumento;                                
                                drFila1[COLUMN_FACILITIES_GROUP] = 60;
                                drFila1[COLUMN_FACILITIES_CODE] = oResponse.HotelFares[i].Hot.Ammenities[b].ToString();
                                drFila1[COLUMN_FACILITIES_DESCRIPTION] = oResponse.HotelFares[i].Hot.Ammenities[b].ToString();
                                drFila1[COLUMN_NAME] = oResponse.HotelFares[i].Hot.Ammenities[b].ToString();

                                dtHotelFacility.Rows.Add(drFila1);
                            }
                        }

                        drFila[COLUMN_DESTINATION_CODE] = oResponse.HotelFares[i].Hot.CityCode;
                        drFila[COLUMN_DESTINATION_NAME] = oResponse.HotelFares[i].Hot.CityCode;
                        drFila[COLUMN_ZONE_TEXT] = oResponse.HotelFares[i].Hot.CityCode;
                      
                        drFila[COLUMN_DETALLES_URL] = oResponse.HotelFares[i].Hot.HotelCode;

                        drFila[COLUMN_DESCRIPTION] = oResponse.HotelFares[i].Hot.Description;
                        drFila[COLUMN_ADDRESS] = oResponse.HotelFares[i].Hot.Address;
                        drFila[COLUMN_HOTEL_TELEPHONE_NUMBER] = oResponse.HotelFares[i].Hot.Phone;

                        if (oResponse.HotelFares[i].Hot.Images.Length > 0)
                            drFila[COLUMN_IMAGEN_URL] = oResponse.HotelFares[i].Hot.Images[0].ToString();

                        drFila[COLUMN_DATE_FROM] = sDateFrom;
                        drFila[COLUMN_DATE_FROM_FORMAT] = sDateFrom;
                        drFila[COLUMN_DATE_TO] = sDateTo;
                        drFila[COLUMN_DATE_TO_FORMAT] = sDateTo;

                        drFila[COLUMN_DETALLES_URL] = "~/presentacion/Detalle_Hotel.aspx?ID=" + oResponse.HotelFares[i].Hot.HotelCode.ToString();
                        drFila[COLUMN_AGENCY_COMISION] = clsValidaciones.getDecimalNotRound(oResponse.HotelFares[i].AgencyCommission.ToString());
                        drFila[COLUMN_IVA] = clsValidaciones.getDecimalNotRound(oResponse.HotelFares[i].Iva.ToString());

                        for (int h = 0; h < oResponse.HotelFares[i].Rooms.Length; h++)
                        {
                            for (int k = 0; k < oResponse.HotelFares[i].Rooms[h].RoomTypes.Length; k++)
                            {
                                bool bNoReembolsable = true;
                                if (clsValidaciones.GetKeyOrAdd("bEliminaReembolsables", "True").ToUpper().Equals("TRUE"))
                                {
                                    if (oResponse.HotelFares[i].Rooms[h].RoomTypes[k].RoomDesc.ToUpper().Contains("NO REEMB"))
                                    {
                                        bNoReembolsable = false;
                                    }
                                    if (oResponse.HotelFares[i].Rooms[h].RoomTypes[k].RoomDesc.ToUpper().Contains("NON REFUND"))
                                    {
                                        bNoReembolsable = false;
                                    }
                                    if (oResponse.HotelFares[i].Rooms[h].RoomTypes[k].RoomDesc.ToUpper().Contains("ADVANCE"))
                                    {
                                        bNoReembolsable = false;
                                    }
                                    if (oResponse.HotelFares[i].Rooms[h].RoomTypes[k].RoomDesc.ToUpper().Contains("Nonrefundable"))
                                    {
                                        bNoReembolsable = false;
                                    }
                                    if (oResponse.HotelFares[i].Rooms[h].RoomTypes[k].RoomDesc.ToUpper().Contains("Nonrefundable".ToUpper()))
                                    {
                                        bNoReembolsable = false;
                                    }
                                    if (oResponse.HotelFares[i].Rooms[h].RoomTypes[k].RoomDesc.ToUpper().Contains("NON-REFUNDABLE"))
                                    {
                                        bNoReembolsable = false;
                                    }
                                    
                                     
                                }
                                if (bNoReembolsable)
                                {
                                    string sTipoMk = "P";
                                    decimal dValorTotal = 0;
                                    decimal dValorAntes = 0;

                                    decimal dValorTotalUSD = 0;
                                    decimal dValorAntesUSD = 0;

                                    decimal dValorTotalIni = 0;
                                    decimal dValorAntesIni = 0;

                                   
                                    DataRow drFilaRoom = dtHotelRoom.NewRow();

                                    try
                                    {
                                        if (oResponse.HotelFares[i].Rooms[h].RoomTypes[k].RoomRate.ToString().Contains("SP"))
                                        {
                                            drFila[COLUMN_OFERTA] = "../App_Themes/Imagenes/oferta.gif";
                                        }
                                        else
                                        {
                                            drFila[COLUMN_OFERTA] = "../App_Themes/Imagenes/spacer.gif";
                                        }
                                    }
                                    catch { drFila[COLUMN_OFERTA] = "../App_Themes/Imagenes/spacer.gif"; }

                                    drFilaRoom[COLUMN_WS_SELECT] = sWsSelect;
                                    drFilaRoom[COLUMN_HOTELCODE] = oResponse.HotelFares[i].Hot.HotelCode;
                                    drFilaRoom[COLUMN_HOTEL_INFO_CODE] = oResponse.HotelFares[i].Hot.HotelCode;
                                    drFilaRoom[COLUMN_NAME] = oResponse.HotelFares[i].Hot.HotelName;
                                    drFilaRoom[COLUMN_HOTEL_ROOM_ID] = iRoomId;
                                    drFilaRoom[COLUMN_HOTEL_INFO_ID] = iAumento;
                                    drFilaRoom[COLUMN_HOTEL_OCCUPANCY_ID] = k;
                                    drFilaRoom[COLUMN_HOTEL_ROOM_OCUPATION] = h + 1;

                                    drFilaRoom[COLUMN_CONFIRM_RATEPLANTYPE] = oResponse.HotelFares[i].Rooms[h].RoomTypes[k].RatePlanType;
                                    drFilaRoom[COLUMN_SHRUI] = oResponse.HotelFares[i].Rooms[h].RoomTypes[k].RoomRate;
                                    drFilaRoom[COLUMN_ROOM_TYPE_TEXT] = oResponse.HotelFares[i].Rooms[h].RoomTypes[k].RoomDesc;
                                    drFilaRoom[COLUMN_TYPE] = oResponse.HotelFares[i].Rooms[h].RoomTypes[k].RoomType;
                                    drFilaRoom[COLUMN_CHARASTERISTIC] = oResponse.HotelFares[i].Rooms[h].RoomTypes[k].InvBlockCode;

                                    drFilaRoom[COLUMN_ROOM_AMOUNT_SURCHARGE] = oResponse.HotelFares[i].Rooms[h].RoomTypes[k].AmountIncludedBoardSurcharge;
                                    drFilaRoom[COLUMN_ROOM_TTL] = oResponse.HotelFares[i].Rooms[h].RoomTypes[k].TTL.ToString();

                                    for (int m = 0; m < oResponse.HotelFares[i].Rooms[h].RoomTypes[k].EffectiveDates.Length; m++)
                                    {
                                       
                                        DataRow drFilaPrice = dtPriceDate.NewRow();

                                        decimal dValorTotalDate = 0;
                                        decimal dValorAntesDate = 0;
                                        decimal dValorDiscountDate = 0;
                                        decimal dValorDiscountAntesDate = 0;

                                        decimal dValorTotalDateFee = 0;
                                        decimal dValorAntesDateFee = 0;

                                        decimal dValorTotalDateFeeUSD = 0;
                                        decimal dValorAntesDateFeeUSD = 0;

                                        decimal dValorTotalDateFeeIni = 0;
                                        decimal dValorAntesDateFeeIni = 0;

                                        dValorTotalDate = clsValidaciones.getDecimalNotRound(oResponse.HotelFares[i].Rooms[h].RoomTypes[k].EffectiveDates[m].AmountAfterTax.ToString());
                                        dValorAntesDate = clsValidaciones.getDecimalNotRound(oResponse.HotelFares[i].Rooms[h].RoomTypes[k].EffectiveDates[m].AmountBeforeTax.ToString());
                                        dValorDiscountDate = clsValidaciones.getDecimalNotRound(oResponse.HotelFares[i].Rooms[h].RoomTypes[k].EffectiveDates[m].DiscountAfterTax.ToString());
                                        dValorDiscountAntesDate = clsValidaciones.getDecimalNotRound(oResponse.HotelFares[i].Rooms[h].RoomTypes[k].EffectiveDates[m].DiscountBeforeTax.ToString());

                                        dValorTotalDateFee = clsValidaciones.getDecimalNotRound((dValorTotalDate * dIncremento).ToString());
                                        dValorAntesDateFee = clsValidaciones.getDecimalNotRound((dValorAntesDate * dIncremento).ToString());

                                        dValorTotalDateFeeUSD = clsValidaciones.getDecimalNotRound((dValorTotalDate + dMarckupUsd).ToString());
                                        dValorAntesDateFeeUSD = clsValidaciones.getDecimalNotRound((dValorAntesDate + dMarckupUsd).ToString());

                                        dValorTotalDateFeeIni = clsValidaciones.getDecimalNotRound((dValorTotalDate).ToString());
                                        dValorAntesDateFeeIni = clsValidaciones.getDecimalNotRound((dValorAntesDate).ToString());

                                        drFilaPrice[COLUMN_HOTEL_ROOM_ID] = iRoomId;
                                        drFilaPrice[COLUMN_DATE] = oResponse.HotelFares[i].Rooms[h].RoomTypes[k].EffectiveDates[m].Date.ToString(FORMATO_FECHA_BD);

                                        drFilaPrice[COLUMN_PRICE_AMOUN_AFTER_TAX] = dValorTotalDate;
                                        drFilaPrice[COLUMN_PRICE_AMOUN_BEFORE_TAX] = dValorAntesDate;
                                        drFilaPrice[COLUMN_PRICE_DISCOUNT_AFTER_TAX] = dValorDiscountDate;
                                        drFilaPrice[COLUMN_PRICE_DISCOUNT_BEFORE_TAX] = dValorDiscountAntesDate;

                                        drFilaPrice[COLUMN_PRICE_AMOUN_AFTER_TAX_FEE] = dValorTotalDateFee;
                                        drFilaPrice[COLUMN_PRICE_AMOUN_BEFORE_TAX_FEE] = dValorAntesDateFee;
                                        drFilaPrice[COLUMN_PRICE_DISCOUNT_AFTER_TAX_FEE] = dValorDiscountDate;
                                        drFilaPrice[COLUMN_PRICE_DISCOUNT_BEFORE_TAX_FEE] = dValorDiscountAntesDate;

                                        try
                                        {
                                            drFilaPrice[COLUMN_CURRENCY] = oResponse.HotelFares[i].Rooms[h].RoomTypes[k].EffectiveDates[m].Currency;
                                            drFilaPrice[COLUMN_PROVIDER_CURRENCY] = oResponse.HotelFares[i].Rooms[h].RoomTypes[k].EffectiveDates[m].ProviderCurrency;
                                            drFilaPrice[COLUMN_ROE] = oResponse.HotelFares[i].Rooms[h].RoomTypes[k].EffectiveDates[m].ROE;
                                        }
                                        catch { }

                                        dValorTotal += dValorTotalDateFee;
                                        dValorAntes += dValorAntesDateFee;

                                        dValorTotalUSD += dValorTotalDateFeeUSD;
                                        dValorAntesUSD += dValorAntesDateFeeUSD;

                                        dValorTotalIni += dValorTotalDateFeeIni;
                                        dValorAntesIni += dValorAntesDateFeeIni;

                                        dtPriceDate.Rows.Add(drFilaPrice);
                                    }
                                    decimal dValorDiferencia = dValorTotal - dValorTotalIni;
                                    if (dMarckupUsd > 0)
                                    {
                                        if (dValorDiferencia > dMarckupUsd)
                                        {
                                            dValorTotal = dValorTotalIni + dMarckupUsd;
                                            dValorAntes = dValorAntesIni + dMarckupUsd;
                                            sTipoMk = "V";
                                        }
                                    }
                                    dValorTotal = clsValidaciones.getDecimalRound(dValorTotal.ToString(), 0, true);
                                    dValorAntes = clsValidaciones.getDecimalRound(dValorAntes.ToString(), 0, true);                                                                  

                                    decimal dTotalViewRoom = dValorTotal / dFactor;
                                    dTotalViewRoom = clsValidaciones.getDecimalRound(dTotalViewRoom.ToString());

                                    drFilaRoom[COLUMN_CURRENCY_TEST] = sMonedaView;
                                    drFilaRoom[COLUMN_CURRENCY_CODE] = sMoneda;
                                    drFilaRoom[COLUMN_AMOUNT] = dValorTotal;
                                    drFilaRoom[COLUMN_AMOUNT_TEXT] = dTotalViewRoom.ToString(FORMATO_NUMEROS_VIEW);
                                    drFilaRoom[COLUMN_TOTAL_PRICE_TEXT] = dTotalViewRoom.ToString(FORMATO_NUMEROS_VIEW);
                                    drFilaRoom[COLUMN_TOTAL_PRICE] = dValorTotal;
                                    drFilaRoom[COLUMN_TOTAL_AMOUNT] = dValorTotal;
                                    drFilaRoom[COLUMN_TIPO_MK] = sTipoMk;
                                    if (sTipoMk.Equals("P"))
                                    {
                                        drFilaRoom[COLUMN_VALOR_MK] = dIncremento;
                                    }
                                    else
                                    {
                                        drFilaRoom[COLUMN_VALOR_MK] = dMarckupUsd;
                                    }

                                    if (dValorMinimo.Equals(0))
                                    {
                                        dValorMinimo = dValorTotal;
                                    }
                                    else
                                    {
                                        if (dValorMinimo > dValorTotal)
                                        {
                                            dValorMinimo = dValorTotal;
                                        }
                                    }
                                  
                                    drFilaRoom[COLUMN_BOARD_CODE] = "";
                                    drFilaRoom[COLUMN_BOARD_TEXT] = "";
                                    
                                    drFilaRoom[COLUMN_BOARD_SHORT_NAME] = sBoard;
                                    drFilaRoom[COLUMN_HOTEL_INFO_ID] = iAumento;
                                   
                                    drFilaRoom[COLUMN_DATE_FROM] = sDateFrom;
                                    drFilaRoom[COLUMN_DATE_TO] = sDateTo;
                                    drFilaRoom[COLUMN_DATE_FROM_FORMAT] = sDateFrom;
                                    drFilaRoom[COLUMN_DATE_TO_FORMAT] = sDateTo;
                                   
                                    drFilaRoom[COLUMN_ADULT_COUNT] = vo_HotelValuedAvailRQ.TotalAdult;
                                    drFilaRoom[COLUMN_CHILD_COUNT] = vo_HotelValuedAvailRQ.TotalChild;
                                    drFilaRoom[COLUMN_ROOM_COUNT] = vo_HotelValuedAvailRQ.TotalRoom;
                                 

                                    dtHotelRoom.Rows.Add(drFilaRoom);

                                    DataRow drFilaOcupancy = dtHotelOcupancy.NewRow();

                                    drFilaOcupancy[COLUMN_HOTEL_ROOM_ID] = iRoomId;
                                    drFilaOcupancy[COLUMN_HOTEL_INFO_ID] = iAumento;
                                    drFilaOcupancy[COLUMN_HOTEL_OCCUPANCY_ID] = k;
                                    drFilaOcupancy[COLUMN_ROOM_COUNT] = h + 1;
                                    int iPaxAdult = 0;
                                    int iPaxChild = 0;

                                    for (int l = 0; l < oResponse.HotelFares[i].Rooms[h].Paxes.Length; l++)
                                    {
                                        if (oResponse.HotelFares[i].Rooms[h].Paxes[l].PaxType.Equals(HotelShop.PaxType.Adult))
                                            iPaxAdult++;
                                        else
                                            iPaxChild++;
                                    }
                                    drFilaOcupancy[COLUMN_ADULT_COUNT] = iPaxAdult;
                                    drFilaOcupancy[COLUMN_CHILD_COUNT] = iPaxChild;

                                    dtHotelOcupancy.Rows.Add(drFilaOcupancy);

                                    DataRow drFilaSupplier = dtSupplier.NewRow();

                                    drFilaSupplier[COLUMN_HOTEL_ROOM_ID] = iRoomId;
                                    drFilaSupplier[COLUMN_SUPPLIER_NAME] = "";
                                    drFilaSupplier[COLUMN_SUPPLIER_VATNUMBER] = "";

                                    dtSupplier.Rows.Add(drFilaSupplier);

                                    DataRow drFilaComment = dtComment.NewRow();

                                    drFilaComment[COLUMN_HOTEL_ROOM_ID] = iRoomId;
                                    drFilaComment[COLUMN_COMMENT_TEXT] = "";

                                    dtComment.Rows.Add(drFilaComment);

                                    iRoomId++;
                                }
                            }

                            try
                            {
                                drFila[COLUMN_SOURCE] = oResponse.HotelFares[i].Hot.Source;
                                drFila[COLUMN_LATITUDE] = oResponse.HotelFares[i].Hot.Latitude;
                                drFila[COLUMN_LONGITUDE] = oResponse.HotelFares[i].Hot.Longitude;
                                drFila[COLUMN_DESCRIPTION] = oResponse.HotelFares[i].Hot.Description;
                                drFila[COLUMN_DESCRIPTION_LONG] = oResponse.HotelFares[i].Hot.LongDescription;
                                drFila[COLUMN_ADDRESS] = oResponse.HotelFares[i].Hot.Address;
                                drFila[COLUMN_HOTEL_TELEPHONE_NUMBER] = oResponse.HotelFares[i].Hot.Phone;
                            }
                            catch { }
                        }
                        decimal dTotalView = dValorMinimo / dFactor;
                        dTotalView = clsValidaciones.getDecimalRound(dTotalView.ToString());
                        drFila[COLUMN_CURRENCY_CODE] = sMoneda;
                        drFila[COLUMN_CURRENCY_TEST] = sMonedaView;
                        drFila[COLUMN_AMOUNT] = dValorMinimo;
                        drFila[COLUMN_AMOUNT_TEXT] = dTotalView.ToString(FORMATO_NUMEROS_VIEW);
                        dtHotelInfo.Rows.Add(drFila);
                    }
                    if (oResponse.Polygons.Length > 0)
                    {
                        for (int p = 0; p < oResponse.Polygons.Length; p++)
                        {
                            DataRow drFilaZone = dtZone.NewRow();
                            drFilaZone[COLUMN_ZONE_CODE] = oResponse.Polygons[p].Id;
                            drFilaZone[COLUMN_ZONE_NAME] = oResponse.Polygons[p].Text;
                            drFilaZone[COLUMN_ZONE_LEVEL] = oResponse.Polygons[p].Level.ToString();
                            dtZone.Rows.Add(drFilaZone);
                        }
                    }
                    clsError.getTablaError(dsData);
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Message = "No se encontraron resultados";
                    cParametros.Severity = clsSeveridad.Alta;
                    cParametros.Tipo = clsTipoError.WebServices;
                    cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    cParametros.Complemento = "Resultados de Hoteles";
                    cParametros.ViewMessage.Add("No existen resultados para esta búsqueda: " + cParametros.Message);
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    ExceptionHandled.Publicar(cParametros);
                    clsError.getError(dsData, cParametros);
                }
                dsData.Tables.Add(dtHotelInfo);
                dsData.Tables.Add(dtHotelRoom);
                dsData.Tables.Add(dtHotelOcupancy);
                dsData.Tables.Add(dtHotelValueAvail);
                dsData.Tables.Add(dtHotelPaginacionData);
                dsData.Tables.Add(dtHotelFacility);
                dsData.Tables.Add(dtHotelImages);
                dsData.Tables.Add(dtHotelInstalaciones);
                dsData.Tables.Add(dtCancalacion);
                dsData.Tables.Add(dtCostoAdicional);
                dsData.Tables.Add(dtPriceDate);
                dsData.Tables.Add(dtCancelationDate);
                dsData.Tables.Add(dtSupplier);
                dsData.Tables.Add(dtComment);
                dsData.Tables.Add(dtZone);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.WebServices;
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.Complemento = "Resultados de Hoteles";
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.ViewMessage.Add("No existen resultados para esta búsqueda: " + cParametros.Message);
                cParametros.Sugerencia.Add("Por favor intente de nuevo");
                ExceptionHandled.Publicar(cParametros);
                clsError.getError(dsData, cParametros);
            }
        }
        private DataTable setTableHotelInfo()
        {
            DataTable dtData = new DataTable(TABLA_HOTEL_INFO);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_HOTEL_INFO_ID, typeof(string));
                dtData.Columns.Add(COLUMN_HOTELCODE, typeof(string));
                dtData.Columns.Add(COLUMN_NAME, typeof(string));
                dtData.Columns.Add(COLUMN_HOTEL_INFO_CODE, typeof(int));
                dtData.Columns.Add(COLUMN_CATEGORY_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_CATEGORY_TEXT, typeof(string));
                dtData.Columns.Add(COLUMN_DESTINATION_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_DESTINATION_NAME, typeof(string));
                dtData.Columns.Add(COLUMN_ZONE_TEXT, typeof(string));
                dtData.Columns.Add(COLUMN_CLASIFICATION_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_CLASIFICATION_TEXT, typeof(string));
                dtData.Columns.Add(COLUMN_LATITUDE, typeof(string));
                dtData.Columns.Add(COLUMN_LONGITUDE, typeof(string));
                dtData.Columns.Add(COLUMN_IMAGEN_URL, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_FROM, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_FROM_FORMAT, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_TO, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_TO_FORMAT, typeof(string));
                dtData.Columns.Add(COLUMN_DETALLES_URL, typeof(string));
                dtData.Columns.Add(COLUMN_OFERTA, typeof(string));
                dtData.Columns.Add(COLUMN_DESCRIPTION, typeof(string));
                dtData.Columns.Add(COLUMN_DESCRIPTION_LONG, typeof(string));
                dtData.Columns.Add(COLUMN_ADDRESS, typeof(string));
                dtData.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_CURRENCY_TEST, typeof(string));
                dtData.Columns.Add(COLUMN_AMOUNT, typeof(decimal));
                dtData.Columns.Add(COLUMN_AMOUNT_TEXT, typeof(string));
                dtData.Columns.Add(COLUMN_HOTEL_TELEPHONE_NUMBER, typeof(string));
                dtData.Columns.Add(COLUMN_WS_SELECT, typeof(string));
                dtData.Columns.Add(COLUMN_COUNTRY_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_COUNTRY__NAME, typeof(string));
                dtData.Columns.Add(COLUMN_AGENCY_COMISION, typeof(decimal));
                dtData.Columns.Add(COLUMN_IVA, typeof(decimal));
                dtData.Columns.Add(COLUMN_OBSERVACIONES, typeof(string));
                dtData.Columns.Add(COLUMN_NON_REEMBOLSABLE, typeof(int));
                dtData.Columns.Add(COLUMN_HOTEL_ID_ITEM, typeof(string));

                dtData.Columns.Add(COLUMN_SOURCE, typeof(int));
                dtData.Columns.Add(COLUMN_HOTELVALIDA, typeof(int));
            }
            catch { }
            return dtData;
        }
        private DataTable setTableHotelRoom()
        {
            DataTable dtData = new DataTable(TABLA_HOTEL_ROOM);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_HOTEL_ROOM_ID, typeof(int));
                dtData.Columns.Add(COLUMN_HOTEL_INFO_ID, typeof(int));
                dtData.Columns.Add(COLUMN_HOTELCODE, typeof(string));
                dtData.Columns.Add(COLUMN_NAME, typeof(string));
                dtData.Columns.Add(COLUMN_HOTEL_INFO_CODE, typeof(int));
                dtData.Columns.Add(COLUMN_ROOM_TYPE_TEXT, typeof(string));
                dtData.Columns.Add(COLUMN_TYPE, typeof(string));
                dtData.Columns.Add(COLUMN_CHARASTERISTIC, typeof(string));
                dtData.Columns.Add(COLUMN_CURRENCY_TEST, typeof(string));
                dtData.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_AMOUNT, typeof(decimal));
                dtData.Columns.Add(COLUMN_AMOUNT_TEXT, typeof(string));
                dtData.Columns.Add(COLUMN_SERVICE_HOTEL_ID, typeof(int));
                dtData.Columns.Add(COLUMN_BOARD_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_BOARD_TEXT, typeof(string));
                dtData.Columns.Add(COLUMN_BOARD_SHORT_NAME, typeof(string));
                dtData.Columns.Add(COLUMN_AVAIL_TOKEN, typeof(string));
                dtData.Columns.Add(COLUMN_INCOMING_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_CONTRACT_NAME, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_FROM, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_TO, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_FROM_FORMAT, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_TO_FORMAT, typeof(string));
                dtData.Columns.Add(COLUMN_DESTINATION_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_URL, typeof(string));
                dtData.Columns.Add(COLUMN_ADULT_COUNT, typeof(int));
                dtData.Columns.Add(COLUMN_CHILD_COUNT, typeof(int));
                dtData.Columns.Add(COLUMN_HOTEL_OCCUPANCY_ID, typeof(int));
                dtData.Columns.Add(COLUMN_ROOM_COUNT, typeof(int));
                dtData.Columns.Add(COLUMN_HOTEL_ROOM_OCUPATION, typeof(int));
                dtData.Columns.Add(COLUMN_SHRUI, typeof(string));
                dtData.Columns.Add(COLUMN_SERVICE_STATUS, typeof(string));
                dtData.Columns.Add(COLUMN_POLITICA, typeof(string));
                dtData.Columns.Add(COLUMN_COMMENT_TEXT, typeof(string));
                dtData.Columns.Add(COLUMN_TOTAL_PRICE_TEXT, typeof(string));
                dtData.Columns.Add(COLUMN_TOTAL_PRICE, typeof(decimal));
                dtData.Columns.Add(COLUMN_CONFIRM_RATEPLANTYPE, typeof(string));
                dtData.Columns.Add(COLUMN_WS_SELECT, typeof(string));
                dtData.Columns.Add(COLUMN_FILE_NUMBER, typeof(string));
                dtData.Columns.Add(COLUMN_VAT_NUMBER, typeof(string));
                dtData.Columns.Add(COLUMN_TOTAL_AMOUNT, typeof(decimal));
                dtData.Columns.Add(COLUMN_TIPO_MK, typeof(string));
                dtData.Columns.Add(COLUMN_VALOR_MK, typeof(decimal));

                dtData.Columns.Add(COLUMN_ROOM_AMOUNT_SURCHARGE, typeof(decimal));
                dtData.Columns.Add(COLUMN_ROOM_TTL, typeof(string));

                dtData.Columns.Add(COLUMN_PNR, typeof(string));

                #region [ PRIMARY KEY ]
                dtData.PrimaryKey = new DataColumn[] { dtData.Columns[COLUMN_HOTEL_ROOM_ID] };
                #endregion

            }
            catch { }
            return dtData;
        }
        private DataTable setTableHotelOcupancy()
        {
            DataTable dtData = new DataTable(TABLA_HOTEL_OCCUPANCY);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_ROOM_COUNT, typeof(int));
                dtData.Columns.Add(COLUMN_HOTEL_OCCUPANCY_ID, typeof(int));
                dtData.Columns.Add(COLUMN_HOTEL_ROOM_ID, typeof(int));
                dtData.Columns.Add(COLUMN_SERVICE_HOTEL_ID, typeof(int));
                dtData.Columns.Add(COLUMN_HOTEL_INFO_ID, typeof(int));
                dtData.Columns.Add(COLUMN_ADULT_COUNT, typeof(int));
                dtData.Columns.Add(COLUMN_CHILD_COUNT, typeof(int));
            }
            catch { }
            return dtData;
        }
        private DataTable setTableHotelValueAvail()
        {
            DataTable dtData = new DataTable(TABLA_HOTEL_VALUE_AVAIL);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_HOTEL_VALUE_AVAIL, typeof(int));
                dtData.Columns.Add(COLUMN_TOTAL_ITEMS, typeof(int));
            }
            catch { }
            return dtData;
        }
        private DataTable setTableHotelPaginacionData()
        {
            DataTable dtData = new DataTable(TABLA_PAGINATION_DATA);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_CURRENT_PAGE, typeof(int));
                dtData.Columns.Add(COLUMN_TOTAL_PAGES, typeof(int));
                dtData.Columns.Add(COLUMN_RESULT_PAGE, typeof(int));
            }
            catch { }
            return dtData;
        }
        private DataTable setTableHotelImages()
        {
            DataTable dtData = new DataTable(TABLA_FACILITIES_IMAGEN);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_HOTEL_INFO_ID, typeof(int));
                dtData.Columns.Add(COLUMN_HOTEL_INFO_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_FACILITIES_DESCRIPTION, typeof(string));
                dtData.Columns.Add(COLUMN_FACILITIES_IMAGEPATH, typeof(string));
            }
            catch { }
            return dtData;
        }
        private DataTable setTableHotelInstalaciones()
        {
            DataTable dtData = new DataTable(TABLA_INSTALACIONES);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_HOTEL_INFO_ID, typeof(int));
                dtData.Columns.Add(COLUMN_HOTEL_INFO_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_FACILITIES_GROUP, typeof(int));

                dtData.Columns.Add(COLUMN_FACILITIES_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_FACILITIES_DESCRIPTION, typeof(string));
                dtData.Columns.Add(COLUMN_NAME, typeof(string));
            }
            catch { }
            return dtData;
        }
        private DataTable setTableHotelFacility()
        {
            DataTable dtData = new DataTable(TABLA_FEATURE);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_HOTEL_INFO_ID, typeof(int));
                dtData.Columns.Add(COLUMN_HOTEL_INFO_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_FACILITIES_GROUP, typeof(int));

                dtData.Columns.Add(COLUMN_FACILITIES_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_FACILITIES_DESCRIPTION, typeof(string));
                dtData.Columns.Add(COLUMN_NAME, typeof(string));
            }
            catch { }
            return dtData;
        }
        private DataTable setTableCosteAditional()
        {
            DataTable dtData = new DataTable(TABLA_ADITIONAL_COST);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_HOTEL_INFO_ID, typeof(int));
                dtData.Columns.Add(COLUMN_HOTEL_INFO_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_FACILITIES_GROUP, typeof(int));
                dtData.Columns.Add(COLUMN_CURRENCY_TEST, typeof(string));
                dtData.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_PRICE_ID, typeof(int));
                dtData.Columns.Add(COLUMN_AMOUNT, typeof(decimal));
                dtData.Columns.Add(COLUMN_AMOUNT_TEXT, typeof(string));
            }
            catch { }
            return dtData;
        }
        private DataTable setTableCancelation()
        {
            DataTable dtData = new DataTable(TABLA_CANCELATION_POLICY);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_HOTEL_ROOM_ID, typeof(int));

                dtData.Columns.Add(COLUMN_CURRENCY_TEST, typeof(string));
                dtData.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_DATE, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_TIME_FROM, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_TIME, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_TIME_TO, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_TIME_FROM_FORMAT, typeof(string));
                dtData.Columns.Add(COLUMN_DATE_TIME_TO_FORMAT, typeof(string));
                dtData.Columns.Add(COLUMN_CANCELATION_TOTAL_DAYS, typeof(int));
                dtData.Columns.Add(COLUMN_CANCELATION_AMOUNT_DAY, typeof(decimal));
                dtData.Columns.Add(COLUMN_CANCELATION_AMOUNT_DAY_TEXT, typeof(string));
                dtData.Columns.Add(COLUMN_CANCELATION_TOTAL_AMOUNT, typeof(decimal));
                dtData.Columns.Add(COLUMN_CANCELATION_TOTAL_AMOUNT_TEXT, typeof(string));
                dtData.Columns.Add(COLUMN_CANCELATION_PRICE_ID, typeof(int));

                dtData.Columns.Add(COLUMN_CANCELATION_FEES, typeof(bool));
                dtData.Columns.Add(COLUMN_CANCELATION_MULTIPLIER, typeof(int));
                dtData.Columns.Add(COLUMN_CANCELATION_DROPTIME, typeof(HotelConfirm.OffsetDropTime));
                dtData.Columns.Add(COLUMN_CANCELATION_TIMEOUT, typeof(HotelConfirm.OffsetTimeUnit));
                dtData.Columns.Add(COLUMN_CANCELATION_TAXAMOUNT, typeof(decimal));
                dtData.Columns.Add(COLUMN_AMOUNT, typeof(decimal));
                dtData.Columns.Add(COLUMN_CANCELATION_TOTAL_VALUE, typeof(decimal));
            }
            catch { }
            return dtData;
        }
        private DataTable setTablePriceDate()
        {
            DataTable dtData = new DataTable(TABLA_PRICE);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_HOTEL_ROOM_ID, typeof(int));
                dtData.Columns.Add(COLUMN_DATE, typeof(string));
                dtData.Columns.Add(COLUMN_PRICE_AMOUN_AFTER_TAX, typeof(decimal));
                dtData.Columns.Add(COLUMN_PRICE_AMOUN_BEFORE_TAX, typeof(decimal));
                dtData.Columns.Add(COLUMN_PRICE_DISCOUNT_AFTER_TAX, typeof(decimal));
                dtData.Columns.Add(COLUMN_PRICE_DISCOUNT_BEFORE_TAX, typeof(decimal));
                dtData.Columns.Add(COLUMN_PRICE_AMOUN_AFTER_TAX_FEE, typeof(decimal));
                dtData.Columns.Add(COLUMN_PRICE_AMOUN_BEFORE_TAX_FEE, typeof(decimal));
                dtData.Columns.Add(COLUMN_PRICE_DISCOUNT_AFTER_TAX_FEE, typeof(decimal));
                dtData.Columns.Add(COLUMN_PRICE_DISCOUNT_BEFORE_TAX_FEE, typeof(decimal));
                dtData.Columns.Add(COLUMN_SELECCION, typeof(string));

                dtData.Columns.Add(COLUMN_CURRENCY, typeof(string));
                dtData.Columns.Add(COLUMN_PROVIDER_CURRENCY, typeof(string));
                dtData.Columns.Add(COLUMN_ROE, typeof(decimal));
            }
            catch { }
            return dtData;
        }
        private DataTable setTableCancelationDate()
        {
            DataTable dtData = new DataTable(TABLA_DATE_TIME_FROM);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_HOTEL_ROOM_ID, typeof(int));
                dtData.Columns.Add(COLUMN_DATE, typeof(string));
            }
            catch { }
            return dtData;
        }
        private DataTable setTableSupplier()
        {
            DataTable dtData = new DataTable(TABLA_SUPPLIER);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_HOTEL_ROOM_ID, typeof(int));
                dtData.Columns.Add(COLUMN_SUPPLIER_NAME, typeof(string));
                dtData.Columns.Add(COLUMN_SUPPLIER_VATNUMBER, typeof(string));
            }
            catch { }
            return dtData;
        }
        private DataTable setTableComment()
        {
            DataTable dtData = new DataTable(TABLA_COMMENT);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_HOTEL_ROOM_ID, typeof(int));
                dtData.Columns.Add(COLUMN_COMMENT_TEXT, typeof(string));
                dtData.Columns.Add(COLUMN_TYPE, typeof(string));
            }
            catch { }
            return dtData;
        }
        private DataTable setTableZone()
        {
            DataTable dtData = new DataTable(TABLA_ZONE);
            try
            {
                // Adicionamos las columnas
                dtData.Columns.Add(COLUMN_ZONE_CODE, typeof(string));
                dtData.Columns.Add(COLUMN_ZONE_NAME, typeof(string));
                dtData.Columns.Add(COLUMN_ZONE_LEVEL, typeof(string));
            }
            catch { }
            return dtData;
        }
    }
    public class clsXML
    {
        public static void ClaseXML(HotelShopRQ csClase, string strArchivoXML)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(HotelShopRQ));
            StreamWriter WriterRQ = new StreamWriter(strArchivoXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public static void ClaseXML(HotelShopRS csClase, string strArchivoXML)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(HotelShopRS));
            StreamWriter WriterRQ = new StreamWriter(strArchivoXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public static void ClaseXML(HotelConfirmRQ csClase, string strArchivoXML)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(HotelConfirmRQ));
            StreamWriter WriterRQ = new StreamWriter(strArchivoXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public static void ClaseXML(HotelConfirmRS csClase, string strArchivoXML)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(HotelConfirmRS));
            StreamWriter WriterRQ = new StreamWriter(strArchivoXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public static void ClaseXML(HotelResRQ csClase, string strArchivoXML)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(HotelResRQ));
            StreamWriter WriterRQ = new StreamWriter(strArchivoXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public static void ClaseXML(HotelResRS csClase, string strArchivoXML)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(HotelResRS));
            StreamWriter WriterRQ = new StreamWriter(strArchivoXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public static void ClaseXML(HotelReservationInfoRQ csClase, string strArchivoXML)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(HotelReservationInfoRQ));
            StreamWriter WriterRQ = new StreamWriter(strArchivoXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public static void ClaseXML(HotelReservationInfoRS csClase, string strArchivoXML)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(HotelReservationInfoRS));
            StreamWriter WriterRQ = new StreamWriter(strArchivoXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public static void ClaseXML(HotelShopService csClase, string strArchivoXML)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(HotelShopService));
            StreamWriter WriterRQ = new StreamWriter(strArchivoXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public static void ClaseXML(HotelConfirmService csClase, string strArchivoXML)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(HotelConfirmService));
            StreamWriter WriterRQ = new StreamWriter(strArchivoXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public static void ClaseXML(HotelCancelRQ csClase, string strArchivoXML)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(HotelCancelRQ));
            StreamWriter WriterRQ = new StreamWriter(strArchivoXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public static void ClaseXML(HotelCancelRS csClase, string strArchivoXML)
        {
            XmlSerializer SerializerRQ = new XmlSerializer(typeof(HotelCancelRS));
            StreamWriter WriterRQ = new StreamWriter(strArchivoXML);
            try
            {
                SerializerRQ.Serialize(WriterRQ, csClase);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
    }
}
