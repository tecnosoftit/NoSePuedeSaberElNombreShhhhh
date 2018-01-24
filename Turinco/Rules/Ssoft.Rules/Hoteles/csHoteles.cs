using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ssoft.ManejadorExcepciones;
using System.Web.UI;
using Ssoft.Utils;
using System.Web.UI.WebControls;
using System.Web;
using Ssoft.ValueObjects;
using Ssoft.Ssoft.ValueObjects.Hoteles;
using Ssoft.DataNet;
using System.Data;
using SsoftQuery.Hoteles;
using System.Web.UI.HtmlControls;
using Ssoft.Rules.Reservas;
using SsoftQuery.Vuelos;
using Ssoft.Sql;

namespace Ssoft.Rules.Hoteles
{
    public class csHoteles
    {
        #region [ Definiciones ]
        //NOMBRES TABLAS

        // Tablas reservas
        csConsultasHoteles cHoteles = new csConsultasHoteles();
        private const string TABLA_ROOM = "HotelRoom";
        private const string TABLA_IMAGES = "HotelImages";
        private const string TABLA_DINING = "HotelDining";
        private const string TABLA_LOCATION = "HotelLocations";
        private const string TABLA_PAYMENTSFEATURES = "PaymentsFeatures";
        private const string TABLA_ROOMFEATURES = "RoomsFeatures";
        private const string TABLA_AMENITYFEATURES = "AmenityFeatures";
        private const string TABLA_GENERALITIES = "HotelGenaralities";
        private const string TABLA_PARAMETERS = "ParametersHotelDetail";
        private const string TABLA_HOTELDISTANCE = "HotelDistances";

        public const string TABLA_MASTER = "tblReserva";
        public const string TABLA_SEGMENTOS = "tblTransac";
        public const string TABLA_PAX = "tblPax";
        public const string TABLA_TARIFA = "tblTarifa";
        public const string TABLA_TASAS = "tblTax";
        public const string TABLA_HABITACIONES = "tblHabitaciones";

        public const string TABLA_DISCOUNT = "Discount";

        // Tablas WebServices
        private const string TABLA_HOTEL_VALUE_AVAIL = "HotelValuedAvailRS";
        private const string TABLA_AUDIT_DATA = "AuditData";
        private const string TABLA_PAGINATION_DATA = "PaginationData";
        private const string TABLA_SERVICE_HOTEL = "ServiceHotel";
        private const string TABLA_CONTRACT_LIST = "ContractList";
        private const string TABLA_CONTRACT = "Contract";
        private const string TABLA_INCOMING_OFFICE = "IncomingOffice";
        private const string TABLA_CLASSIFICATION = "Classification";
        private const string TABLA_DATE_FROM = "DateFrom";
        private const string TABLA_DATE_TO = "DateTo";
        private const string TABLA_CURRENCY = "Currency";
        private const string TABLA_HOTEL_INFO = "HotelInfo";
        private const string TABLA_IMAGE_LIST = "ImageList";
        private const string TABLA_IMAGE = "Image";
        private const string TABLA_CATEGORY = "Category";
        private const string TABLA_DESTINATION = "Destination";
        private const string TABLA_ZONE_LIST = "ZoneList";
        private const string TABLA_ZONE = "Zone";
        private const string TABLA_CHILD_AGE = "ChildAge";
        private const string TABLA_POSITION = "Position";
        private const string TABLA_AVAILABLE_ROOM = "AvailableRoom";
        private const string TABLA_HOTEL_OCCUPANCY = "HotelOccupancy";
        private const string TABLA_OCCUPANCY = "Occupancy";
        private const string TABLA_HOTEL_ROOM = "HotelRoom";
        private const string TABLA_BOARD = "Board";
        private const string TABLA_ROOM_TYPE = "RoomType";
        private const string TABLA_PRICE = "Price";
        private const string TABLA_ADDRESS = "Address";

        // Tablas Adicionales de ServivesAdd
        private const string TABLA_PURCHASE = "Purchase";
        private const string TABLA_AGENCY = "Agency";
        private const string TABLA_SERVICE_LIST = "ServiceList";
        private const string TABLA_SERVICE = "Service";
        private const string TABLA_SUPLIER = "Supplier";
        private const string TABLA_ADITIONAL_COST_LIST = "AdditionalCostList";
        private const string TABLA_ADITIONAL_COST = "AdditionalCost";
        private const string TABLA_MODIFICATION_POLICY_LIST = "ModificationPolicyList";
        private const string TABLA_MODIFICATION_POLICY = "ModificationPolicy";
        private const string TABLA_GUEST_LIST = "GuestList";
        private const string TABLA_CUSTOMER = "Customer";
        private const string TABLA_BIRTH_DATE = "BirthDate";
        private const string TABLA_CANCELATION_POLICY = "CancellationPolicy";
        private const string TABLA_DATE_TIME_FROM = "DateTimeFrom";
        private const string TABLA_DATE_TIME_TO = "DateTimeTo";

        // Tablas Adicionales de Confirmacion
        private const string TABLA_PURCHASE_CONFIRMRS = "PurchaseConfirmRS";
        private const string TABLA_REFERENCE = "Reference";
        private const string TABLA_CREATION_DATE = "CreationDate";
        private const string TABLA_HOLDER = "Holder";
        private const string TABLA_COMMENT_LIST = "CommentList";
        private const string TABLA_COMMENT = "Comment";
        private const string TABLA_HOTEL_ROOM_EXTRA_INFO = "HotelRoomExtraInfo";
        private const string TABLA_EXTENDED_DATE = "ExtendedData";
        private const string TABLA_PAYMENT_DATA = "PaymentData";
        private const string TABLA_PAYMENT_TYPE = "PaymentType";

        // datos Adionales del hotel, para consulta en la base de datos local
        private const string DISTANCIAS_ID = "40";
        private const string HABITACION_ID = "60";
        private const string INSTALACIONES_ID = "70";

        // RELACIONES
        private const string RELACION_CONTRACT_INCOMINGOFFICE = "Contract_IncomingOffice";
        private const string RELACION_CONTRACT_CLASIFICATION = "Contract_Classification";
        private const string RELACION_CONTRACTLIST_CONTRACT = "ContractList_Contract";
        private const string RELACION_IMAGELIST_IMAGE = "ImageList_Image";
        private const string RELACION_ZONELIST_ZONE = "ZoneList_Zone";
        private const string RELACION_DESTINATION_ZONELIST = "Destination_ZoneList";
        private const string RELACION_HOTELINFO_IMAGELIST = "HotelInfo_ImageList";
        private const string RELACION_HOTELINFO_CATEGORY = "HotelInfo_Category";
        private const string RELACION_HOTELINFO_DESTINATION = "HotelInfo_Destination";
        private const string RELACION_HOTELINFO_CHILDAGE = "HotelInfo_ChildAge";
        private const string RELACION_HOTELINFO_POSITION = "HotelInfo_Position";
        private const string RELACION_HOTELOCUPANCY_OCUPANCY = "HotelOccupancy_Occupancy";
        private const string RELACION_HOTELROOM_BOARD = "HotelRoom_Board";
        private const string RELACION_HOTELROOM_ROOMTYPE = "HotelRoom_RoomType";
        private const string RELACION_HOTELROOM_PRICE = "HotelRoom_Price";
        private const string RELACION_AVAILABLEROOM_HOTELOCUPANCY = "AvailableRoom_HotelOccupancy";
        private const string RELACION_AVAILABLEROOM_HOTELROOM = "AvailableRoom_HotelRoom";
        private const string RELACION_SERVICEHOTEL_CONTRACTLIST = "ServiceHotel_ContractList";
        private const string RELACION_SERVICEHOTEL_DATE_FROM = "ServiceHotel_DateFrom";
        private const string RELACION_SERVICEHOTEL_DATE_TO = "ServiceHotel_DateTo";
        private const string RELACION_SERVICEHOTEL_CURRENCY = "ServiceHotel_Currency";
        private const string RELACION_SERVICEHOTEL_HOTELINFO = "ServiceHotel_HotelInfo";
        private const string RELACION_SERVICEHOTEL_AVAILABLEROOM = "ServiceHotel_AvailableRoom";
        private const string RELACION_HOTELVALUEAVAIL_AUDITDATA = "HotelValuedAvailRS_AuditData";
        private const string RELACION_HOTELVALUEAVAIL__PAGINATIONDATA = "HotelValuedAvailRS_PaginationData";
        private const string RELACION_HOTELVALUEAVAIL_SERVICEHOTEL = "HotelValuedAvailRS_ServiceHotel";


        // RELACIONES ADICIONALES DE SERVICVESADD
        private const string RELACION_ADITIONALCOST_PRICE = "AdditionalCost_Price";
        private const string RELACION_ADITIONALCOSTLIST_ADITIONALCOST = "AdditionalCostList_AdditionalCost";
        private const string RELACION_MODIFICATIONPOLICYLIST_MODIFICATIONPOLICY = "ModificationPolicyList_ModificationPolicy";
        private const string RELACION_CUSTOMER_BIRTHDATE = "Customer_BirthDate";
        private const string RELACION_GUESTLIST_CUSTOMER = "GuestList_Customer";
        private const string RELACION_OCUPANCY_GUESTLIST = "Occupancy_GuestList";
        private const string RELACION_PRICE_DFATETIMEFROM = "Price_DateTimeFrom";
        private const string RELACION_PRICE_DFATETIMETO = "Price_DateTimeTo";
        private const string RELACION_CANCELATIONPOLICY_PRICE = "CancellationPolicy_Price";
        private const string RELACION_HOTELROOM_CNCELATIONPOLICY = "HotelRoom_CancellationPolicy";
        private const string RELACION_SERVICE_CONTRACTLIST = "Service_ContractList";
        private const string RELACION_SERVICE_SUPPLIER = "Service_Supplier";
        private const string RELACION_SERVICE_DATEFROM = "Service_DateFrom";
        private const string RELACION_SERVICE_DATETO = "Service_DateTo";
        private const string RELACION_SERVICE_CURRENCY = "Service_Currency";
        private const string RELACION_SERVICE_ADITIONALCOSTLIST = "Service_AdditionalCostList";
        private const string RELACION_SERVICE_MODIFICATIONPOLICYLIST = "Service_ModificationPolicyList";
        private const string RELACION_SERVICE_HOTELINFO = "Service_HotelInfo";
        private const string RELACION_SERVICE_AVAILABLEROOM = "Service_AvailableRoom";
        private const string RELACION_SERVICELIST_SERVICE = "ServiceList_Service";
        private const string RELACION_PURCHASE_AGENCY = "Purchase_Agency";
        private const string RELACION_PURCHASE_SERVICELIST = "Purchase_ServiceList";
        private const string RELACION_PURCHASE_CURRENCY = "Purchase_Currency";
        private const string RELACION_SERVICEADDRS_AUDITDATA = "ServiceAddRS_AuditData";
        private const string RELACION_SERVICEADDRS_PURCHASE = "ServiceAddRS_Purchase";

        // RELACIONES ADICIONALES DE CONFIRMACION
        private const string RELACION_REFERENCE_INCOMINGOFFICE = "Reference_IncomingOffice";
        private const string RELACION_COMMENTLIST_COMMENT = "CommentList_Comment";
        private const string RELACION_CONTRACT_COMMENTLIST = "Contract_CommentList";
        private const string RELACION_HOTELROOMEXTRAINFO_EXTENDEDATA = "HotelRoomExtraInfo_ExtendedData";
        private const string RELACION_HOTELROOM_HOTELROOMEXTRAINFO = "HotelRoom_HotelRoomExtraInfo";
        private const string RELACION_SERVICES_REFERENCE = "Service_Reference";
        private const string RELACION_SERVICES_COMMENTOLIST = "Service_CommentList";
        private const string RELACION_PAYMENTDATA_PAYMENTTYPE = "PaymentData_PaymentType";
        private const string RELACION_PURCHASE_REFERENCE_ = "Purchase_Reference";
        private const string RELACION_PURCHASE_CREATIONDATE = "Purchase_CreationDate";
        private const string RELACION_PURCHASE_HOLDER = "Purchase_Holder";
        private const string RELACION_PURCHASE_PAYMENTDATA = "Purchase_PaymentData";
        private const string RELACION_PURCHASECONFIRMRS_AUDITDATA = "PurchaseConfirmRS_AuditData";
        private const string RELACION_PURCHASECONFIRMRS_PURCHASE = "PurchaseConfirmRS_Purchase";

        // Columnas

        // TABLA_HOTEL_VALUE_AVAIL
        private const string COLUMN_HOTEL_VALUE_AVAIL = "HotelValuedAvailRS_Id";
        private const string COLUMN_TOTAL_ITEMS = "totalItems";

        public const string COLUMN_AMOUNT_VIEW_TEXT = "Texto_de_la_conversion_de_la_moneda";
        public const string COLUMN_CURRENCY_VIEW_TEXT = "Texto_de_la_Moneda_convertida";

        // TABLA_PAGINATION_DATA
        private const string COLUMN_CURRENT_PAGE = "currentPage";
        private const string COLUMN_TOTAL_PAGES = "totalPages";
        private const string COLUMN_RESULT_PAGE = "resultPage";

        // TABLA_SERVICE_HOTEL
        private const string COLUMN_SERVICE_HOTEL_ID = "ServiceHotel_Id";
        private const string COLUMN_AVAIL_TOKEN = "availToken";

        // TABLA_CONTRACT_LIST
        private const string COLUMN_CONTRACT_LIST_ID = "ContractList_Id";

        // TABLA_CONTRACT
        private const string COLUMN_CONTRACT_ID = "Contract_Id";
        private const string COLUMN_CONTRACT_NAME = "Contract_Name";

        // TABLA_INCOMING_OFFICE
        private const string COLUMN_INCOMING_CODE = "Incoming_Code";

        // TABLA_CLASSIFICATION
        private const string COLUMN_CODE = "code";
        private const string COLUMN_CLASIFICATION_TEXT = "Classification_Text";
        private const string COLUMN_CLASIFICATION_CODE = "Classification_Code";
        private const string COLUMN_OfertaNewOcupancy = "OfertaNewOcupancy";
        private const string COLUMN_Classification_Text = "Classification_Text";

        // TABLA_CURRENCY
        private const string COLUMN_CURRENCY_TEST = "Currency_Text";
        private const string COLUMN_CURRENCY_CODE = "Currency_Code";

        // TABLA_HOTEL_INFO
        private const string COLUMN_NAME = "Name";
        private const string COLUMN_HOTEL_INFO_ID = "HotelInfo_Id";
        private const string COLUMN_OFERTA = "Oferta";
        private const string COLUMN_DETALLES_URL = "DetallesURL";
        private const string COLUMN_DESCRIPTION = "description";
        private const string COLUMN_DESCRIPTION_LONG = "description_Long";
        private const string COLUMN_ADDRESS = "Address";
        private const string COLUMN_HOTEL_INFO_CODE = "HotelInfo_Code";
        private const string COLUMN_HOTEL_TELEPHONE_NUMBER = "Hotel_Telephone_Number";
        private const string COLUMN_HOTELCODE = "Code";
        private const string COLUMN_HOTEL_PHONE_TYPE = "PhoneType";
        private const string COLUMN_HOTEL_PHONE_NUMBRE = "Number_";
        private const string COLUMN_WS_SELECT = "WsSelect";
        private const string COLUMN_AGENCY_COMISION = "AgencyComission";
        private const string COLUMN_OBSERVACIONES = "Observaciones";
        public const string COLUMN_IVA = "Iva";
        public const string COLUMN_NON_REEMBOLSABLE = "Reembolsable"; // Si es 0, es reembolsable, si es 1 es no reembolsable
        public const string COLUMN_STREET_NAME = "StreetName";

        // TABLA_IMAGE_LIST
        private const string COLUMN_IMAGE_LIST_ID = "ImageList_Id";

        // TABLA_IMAGE
        private const string COLUMN_TYPE = "Type";
        private const string COLUMN_ORDER = "Order";
        private const string COLUMN_VISUALIZATION_ORDER = "VisualizationOrder";
        private const string COLUMN_URL = "Url";
        private const string COLUMN_IMAGEN_URL = "Hotel_Photo";

        // TABLA_CATEGORY
        private const string COLUMN_SHORT_NAME = "shortname";
        private const string COLUMN_CATEGORY_TEXT = "Category_Text";
        private const string COLUMN_CATEGORY_CODE = "Category_Code";

        // TABLA_DESTINATION
        private const string COLUMN_DESTINATION_ID = "Destination_Id";
        private const string COLUMN_DESTINATION = "Destination";
        private const string COLUMN_DESTINATION_NAME = "Destination_Name";
        private const string COLUMN_DESTINATION_CODE = "Destination_Code";

        // TABLA_ZONE_LIST
        private const string COLUMN_ZONE_LIST_ID = "ZoneList_Id";

        // TABLA_ZONE
        private const string COLUMN_ZONE_TEXT = "Zone_Text";
        //private const string COLUMN_ZONE_CODE = "Zone_Code";

        // TABLA_CHILD_AGE
        private const string COLUMN_AGE_FROM = "ageFrom";
        private const string COLUMN_AGE_TO = "ageTo";

        // TABLA_POSITION
        private const string COLUMN_LATITUDE = "latitude";
        private const string COLUMN_LONGITUDE = "longitude";

        // TABLA_AVAILABLE_ROOM
        private const string COLUMN_AVAILABLE_ROOM_ID = "AvailableRoom_Id";

        // TABLA_HOTEL_OCCUPANCY
        private const string COLUMN_ROOM_COUNT_TEXT = "RoomCountText";
        private const string COLUMN_ROOM_COUNT = "RoomCount";
        private const string COLUMN_HOTEL_OCCUPANCY_ID = "HotelOccupancy_Id";

        // TABLA_OCCUPANCY
        private const string COLUMN_ADULT_COUNT = "AdultCount";
        private const string COLUMN_CHILD_COUNT = "ChildCount";

        // TABLA_HOTEL_ROOM
        private const string COLUMN_HOTEL_ROOM_OCUPATION = "Id";
        private const string COLUMN_HOTEL_ROOM_ID = "HotelRoom_Id";
        private const string COLUMN_SHRUI = "SHRUI";
        private const string COLUMN_AVAIL_COUNT = "availCount";
        private const string COLUMN_ON_REQUEST = "onRequest";
        private const string COLUMN_POLITICA = "Politica";
        private const string COLUMN_PENALIZACION = "Penalizacion";
        private const string COLUMN_SELECCION = "Seleccion";

        // TABLA_BOARD
        private const string COLUMN_BOARD_TEXT = "Board_Text";
        private const string COLUMN_BOARD_CODE = "Board_Code";
        private const string COLUMN_BOARD_SHORT_NAME = "Board_shortname";

        // TABLA_ROOM_TYPE
        private const string COLUMN_CHARASTERISTIC = "characteristic";
        private const string COLUMN_ROOM_TYPE_TEXT = "RoomType_Text";
        private const string COLUMN_iOfertaPromosImg = "iOfertaPromosImg";
        private const string COLUMN_iOfertaPromosText = "iOfertaPromosText";

        // TABLA_PRICE
        private const string COLUMN_AMOUNT = "Amount";
        private const string COLUMN_AMOUNT_TEXT = "AmountText";
        private const string COLUMN_PRICE_ID = "Price_Id";

        // TABLA_DATE_FROM
        private const string COLUMN_DATE = "Date";
        private const string COLUMN_DATE_FROM = "Date_From";
        private const string COLUMN_DATE_FROM_FORMAT = "Date_From_YMD";

        // TABLA_DATE_TO
        private const string COLUMN_DATE_TO = "Date_To";
        private const string COLUMN_DATE_TO_FORMAT = "Date_To_YMD";

        //// COLUMNAS DE TABLAS ADICIONALES DE CONFIRMACION

        // TABLA_PURCHASE
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

        // TABLA_AGENCY
        private const string COLUMN_BRANCH = "Branch";
        private const string COLUMN_AGENCY_CODE = "Agency_Code";

        // TABLA_SERVICE_LIST
        private const string COLUMN_SERVICE_LIST_ID = "ServiceList_Id";

        // TABLA_SERVICE
        private const string COLUMN_SERVICE_ID = "Service_Id";
        private const string COLUMN_TOTAL_AMOUNT = "TotalAmount";
        private const string COLUMN_SPUI = "SPUI";
        private const string COLUMN_SERVICE_STATUS = "Service_Status";
        private const string COLUMN_TOTAL_AMOUNT_TEXT = "TotalAmount_Text";

        // TABLA_SUPLIER
        private const string COLUMN_VAT_NUMBER = "vatNumber";

        // TABLA_ADITIONAL_COST_LIST
        private const string COLUMN_ADITIONAL_COST_LIST_ID = "AdditionalCostList_Id";

        // TABLA_ADITIONAL_COST
        private const string COLUMN_ADITIONAL_COST_ID = "AdditionalCost_Id";
        private const string COLUMN_ADITIONAL_TYPE = "AdditionalType";

        // TABLA_MODIFICATION_POLICY_LIST
        private const string COLUMN_ = "ModificationPolicyList_Id";

        // TABLA_MODIFICATION_POLICY
        private const string COLUMN_MODIFICATION_POLICY_TEXT = "ModificationPolicy_Text";

        // TABLA_GUEST_LIST
        private const string COLUMN_GUEST_LIST_ID = "GuestList_Id";

        // TABLA_CUSTOMER
        private const string COLUMN_CUSTOMERID = "CustomerId";
        private const string COLUMN_AGE = "Age";
        private const string COLUMN_CUSTOMER_NAME = "CustomerName";
        private const string COLUMN_LASTNAME = "LastName";
        private const string COLUMN_CUSTOMER_ID = "Customer_Id";
        private const string COLUMN_COUNTRY_CODE = "CountryCode";

        // TABLA_BIRTH_DATE

        // TABLA_CANCELATION_POLICY
        private const string COLUMN_CANCELATION_POLICY_ID = "CancellationPolicy_Id";
        private const string COLUMN_CANCELATION_TOTAL_DAYS = "Cancellation_TotalDay";
        private const string COLUMN_CANCELATION_AMOUNT_DAY = "Cancellation_AmountDay";
        private const string COLUMN_CANCELATION_AMOUNT_DAY_TEXT = "Cancellation_AmountDayText";
        private const string COLUMN_CANCELATION_TOTAL_AMOUNT = "Cancellation_TotalAmount";
        private const string COLUMN_CANCELATION_TOTAL_AMOUNT_TEXT = "Cancellation_TotalAmountText";
        private const string COLUMN_CANCELATION_PRICE_ID = "Cancellation_PriceId";

        // TABLA_DATE_TIME_FROM
        private const string COLUMN_DATE_TIME = "time";
        private const string COLUMN_DATE_TIME_FROM = "DateTimeFrom";
        private const string COLUMN_DATE_TIME_FROM_FORMAT = "DateTimeFrom_YMD";

        // TABLA_DATE_TIME_TO
        private const string COLUMN_DATE_TIME_TO = "DateTimeTo";
        private const string COLUMN_DATE_TIME_TO_FORMAT = "DateTimeTo_YMD";

        // TABLA_PURCHASE_CONFIRMRS
        private const string COLUMN_PURCHASE_CONFIRMRS_ID = "PurchaseConfirmRS_Id";
        private const string COLUMN_ECHO_TOKEN = "echoToken";

        // TABLA_REFERENCE
        private const string COLUMN_FILE_NUMBER = "FileNumber";
        private const string COLUMN_REFERENCE_ID = "Reference_Id";

        // TABLA_COMMENT_LIST
        private const string COLUMN_COMMENT_LIST_ID = "CommentList_Id";

        // TABLA_COMMENT
        private const string COLUMN_COMMENT_TEXT = "Comment_Text";

        // TABLA_HOTEL_ROOM_EXTRA_INFO
        private const string COLUMN_HOTEL_ROOM_ESTRA_INFO_ID = "HotelRoomExtraInfo_Id";

        // TABLA_EXTENDED_DATE
        private const string COLUMN_VALUE = "Value";

        // TABLA_PAYMENT_DATA
        private const string COLUMN_PAYMENT_DATA_ID = "PaymentData_Id";

        //TABLA HABITACIONES 
        private const string COLUMN_ID_HABITACION = "id";
        //TABLAS FACILIDADES
        private const string ROOM_CURRENCY = "currency";
        private const string ROOM_PRICE = "price";
        private const string ROOM_PRICE_TOTAL = "priceTotal";
        private const string ROOM_PRICE_ADD = "priceAdd";
        private const string ROOM_PRICE_EXTRA = "priceExtra";
        private const string ROOM_DIAS = "dias";
        private const string TABLA_ROOM_HAB = "tarifashabitacion";
        private const string TABLA_ROOM_PRICE = "tarifaXNoche";
        private const string TABLA_ROOM_PRICE_ADIC = "tarifas";
        private const string TABLA_HOTEL = "hoteles";
        public const string TABLA_OPCIONES = "opciones";
        private const string TABLA_FEATURE = "Feature";
        private const string TABLA_FACILIDADES = "tblFacilidades";
        private const string TABLA_INSTALACIONES = "tblInstalaciones";
        private const string TABLA_IMAGEN = "tblImage";
        private const string TABLA_FACILITIES_IMAGEN = "Image";
        private const string COLUMN_HOTEL_CODE = "Hotel_Code";
        private const string COLUMN_FACILITIES_CODE = "Code";
        private const string COLUMN_FACILITIES = "Facilidad";
        private const string COLUMN_NAME_GROUP = "name_group";
        private const string COLUMN_CAR_DISTANCE = "CarDistance";
        private const string COLUMN_CONCEPT = "Concept";
        private const string COLUMN_FACILITIES_DESCRIPTION = "Description";
        private const string COLUMN_IMAGE_NAME = "Name";
        private const string COLUMN_IMAGEPATH = "ImagePath";
        private const string COLUMN_FACILITIES_IMAGEPATH = "URL";

        // TABLE DISCOUNT
        public const string COLUMN_DISCOUNT_TYPE = "type";
        public const string COLUMN_DISCOUNT_FROM = "from";
        public const string COLUMN_DISCOUNT_TO = "to";
        public const string COLUMN_DISCOUNT_TYPEVALUE = "typeValue";
        public const string COLUMN_DISCOUNT_VALUE = "value";
        public const string COLUMN_DISCOUNT_NAME = "name";

        // TABLE OCUPANCY
        public const string COLUMN_OCUPANCY_AVGNIGHT = "avrNightPrice";
        public const string COLUMN_OCUPANCY_OCCUPID = "occupId";
        public const string COLUMN_OCUPANCY_BEDDING = "bedding";
        public const string COLUMN_OCUPANCY_MAXCHILD = "maxChild";
        public const string COLUMN_OCUPANCY_MAXGUEST = "maxGuests";
        public const string COLUMN_OCUPANCY_OCCUPPRICE = "occupPrice";
        public const string COLUMN_OCUPANCY_PUBLISH = "isPublish";
        public const string COLUMN_OCUPANCY_TAX = "tax";
        public const string COLUMN_OCUPANCY_GUEST = "iGuest";
        public const string COLUMN_OCUPANCY_BEDS = "iBest";

        //Cotelco
        private const string COLUMN_NAME_COTELCO = "ds_hotel";
        private const string COLUMN_X = "am_coor_x";
        private const string COLUMN_Y = "am_coor_y";
        private const string COLUMN_CATEGORIA_COTELCO = "cd_category";
        private const string COLUMN_CATEGORIA_COTELCO_TEX = "ds_category";

        // ORDENAMIENTOS
        public const string ORDEN_HOTEL_AMOUNT = "Amount";
        public const string ORDEN_HOTEL_NAME = "Name";
        public const string ORDEN_HOTEL_CATEGORY = "Category_Code";

        csDisenioHoteles cDisenio = new csDisenioHoteles();
        private const string FORMATO_NUMEROS = "#,##0.00";
        private const string FORMATO_NUMEROS_SD = "#,##0";
        private const string FORMATO_FECHA = "yyyy/MM/dd";

        private static string sFormatoFecha = clsSesiones.getFormatoFecha();
        private static string sFormatoFechaBD = clsSesiones.getFormatoFechaBD();

        protected string _Conexion = default(string);

        private static string FORMATO_FECHA_BD = clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");
        protected DataSql dConsulta = new DataSql();
        protected DataSet dsConsulta = new DataSet();

        #endregion


        public clsParametros CargarBusqueda(UserControl PageSource, Enum_WebServices eWebServices)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                TextBox txtFechaIngreso = (TextBox)PageSource.FindControl("txtFechaIngreso");
                TextBox txtFechaSalida = (TextBox)PageSource.FindControl("txt2HFechaSalida");
                TextBox txtCiudad = (TextBox)PageSource.FindControl("txtCiudadDestino");
                TextBox txtOrigen = (TextBox)PageSource.FindControl("txtOrigen");
                TextBox txtDocumento = (TextBox)PageSource.FindControl("txtDocumento");
                DropDownList ddlTipoPlan = (DropDownList)PageSource.FindControl("ddlTipoPlan");
                DropDownList ddlTarifa = (DropDownList)PageSource.FindControl("ddlTarifa");
                DropDownList ddlZone = (DropDownList)PageSource.FindControl("ddlZone");
                HiddenField hdfHotel = (HiddenField)PageSource.FindControl("hdfHotel");

                DropDownList cmbNoches = (DropDownList)PageSource.FindControl("cmbNoches");

                RadioButton rbtnSi = (RadioButton)PageSource.FindControl("rbtnSi");
                RadioButton rbtnNo = (RadioButton)PageSource.FindControl("rbtnNo");
                DropDownList ddlTipoAlimentacion = (DropDownList)PageSource.FindControl("ddlTipoAlimentacion");
                DropDownList ddlCiudades = (DropDownList)PageSource.FindControl("ddlCiudades");

                VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = new VO_HotelValuedAvailRQ();

                List<VO_Passenger> cPassenger = new List<VO_Passenger>();

                switch (eWebServices)
                {
                    case Enum_WebServices.HotelInterNal:
                        cPassenger = CargarInfo(PageSource);
                        break;

                    case Enum_WebServices.HotelBedsHotel:
                        cPassenger = CargarInfo(PageSource);
                        break;
                    case Enum_WebServices.CotelcoHotel:
                        cPassenger = CargarInfoCotelco(PageSource);
                        break;

                    case Enum_WebServices.SabreHotel:
                        cPassenger = CargarInfo(PageSource);
                        break;
                    case Enum_WebServices.TouricoHotel:
                        cPassenger = CargarInfo(PageSource);
                        break;
                }

                clsSesiones.setPassenger(cPassenger);
                Utils.Utils oUtilidad = new Utils.Utils();

                string sFechaIni = string.Empty;
                string sFechaFin = string.Empty;
                try
                {
                    string[] arrFecha = txtFechaIngreso.Text.Split('/');
                    DateTime fechaIngreso =
                        new DateTime(
                        int.Parse(arrFecha[2]),
                       int.Parse(arrFecha[0]),
                      int.Parse(arrFecha[1]));

                    if (cmbNoches != null)
                    {
                        fechaIngreso.AddDays(double.Parse(cmbNoches.SelectedItem.Value));
                    }

                }
                catch (Exception) { }

                sFechaIni = clsValidaciones.ConverFecha(txtFechaIngreso.Text, sFormatoFecha, Enum_FormatoFecha.YMD_);
                sFechaFin = clsValidaciones.ConverFecha(txtFechaSalida.Text, sFormatoFecha, Enum_FormatoFecha.YMD_);
                string sFechaIniCalculo = clsValidaciones.ConverFecha(txtFechaIngreso.Text, sFormatoFecha, Enum_FormatoFecha.YMD);
                string sFechaFinCalculo = clsValidaciones.ConverFecha(txtFechaSalida.Text, sFormatoFecha, Enum_FormatoFecha.YMD);

                int iDias = clsValidaciones.CalcularDias(sFechaIniCalculo, sFechaFinCalculo);
                vo_HotelValuedAvailRQ.CheckInDate = sFechaIni;
                vo_HotelValuedAvailRQ.CheckOutDate = sFechaFin;

                switch (eWebServices)
                {
                    case Enum_WebServices.HotelBedsHotel:
                        if (txtCiudad != null && txtCiudad.Text != "")
                        {
                            vo_HotelValuedAvailRQ.Destination = txtCiudad.Text.Substring(0, 3);
                            vo_HotelValuedAvailRQ.DetalleCiudad = txtCiudad.Text;
                        }
                        break;
                    case Enum_WebServices.CotelcoHotel:
                        if (ddlCiudades != null && ddlCiudades.SelectedValue != "")
                        {
                            vo_HotelValuedAvailRQ.Destination = ddlCiudades.SelectedValue.ToString();
                            vo_HotelValuedAvailRQ.DetalleCiudad = ddlCiudades.SelectedItem.Text.ToString();
                        }
                        break;
                    case Enum_WebServices.SabreHotel:
                        if (txtCiudad != null && txtCiudad.Text != "")
                        {
                            vo_HotelValuedAvailRQ.Destination = txtCiudad.Text.Substring(0, 3);
                            vo_HotelValuedAvailRQ.DetalleCiudad = txtCiudad.Text;
                        }
                        break;
                    case Enum_WebServices.HotelInterNal:
                        if (txtCiudad != null && txtCiudad.Text != "")
                        {
                            vo_HotelValuedAvailRQ.Destination = txtCiudad.Text.Substring(0, 3);
                            vo_HotelValuedAvailRQ.DetalleCiudad = txtCiudad.Text;
                        }
                        break;
                    case Enum_WebServices.TouricoHotel:
                        if (txtCiudad != null && txtCiudad.Text != "")
                        {
                            vo_HotelValuedAvailRQ.Destination = txtCiudad.Text.Substring(0, 3);
                            vo_HotelValuedAvailRQ.DetalleCiudad = txtCiudad.Text;
                        }
                        break;
                }

                if (txtOrigen != null && txtOrigen.Text != "")
                { vo_HotelValuedAvailRQ.CodCiudadOrigen = txtOrigen.Text.Substring(0, 3); }

                try
                {
                    if (HttpContext.Current.Request.QueryString["IdOferta"] != null)
                    {
                        string sTemp = HttpContext.Current.Request.QueryString["IdOferta"].ToString();
                        if (sTemp.Equals("1"))
                        {
                            if (hdfHotel != null)
                            {
                                if (!hdfHotel.Value.Length.Equals(0))
                                {
                                    vo_HotelValuedAvailRQ.CodHotel = hdfHotel.Value;
                                }
                            }
                        }
                    }
                }
                catch { }
                if (ddlZone != null)
                {
                    if (!ddlZone.SelectedValue.Equals("0"))
                    {
                        vo_HotelValuedAvailRQ.Type = Enum_TypeZone.SIMPLE;
                        vo_HotelValuedAvailRQ.Zone = ddlZone.SelectedValue;
                    }
                }
                if (rbtnSi != null && rbtnSi.Checked)
                {
                    vo_HotelValuedAvailRQ.Documento = txtDocumento.Text;
                    vo_HotelValuedAvailRQ.MiembroSemper = Enum_MiembroSemper.si;
                }
                if (rbtnNo != null && rbtnNo.Checked)
                { vo_HotelValuedAvailRQ.MiembroSemper = Enum_MiembroSemper.no; }

                if (ddlTipoPlan != null && ddlTipoPlan.SelectedValue.ToString() == "1")
                { vo_HotelValuedAvailRQ.TipoPlan = Enum_TipoPlan.Alojamiento; }
                else if (ddlTipoPlan != null && ddlTipoPlan.SelectedValue.ToString() == "2")
                { vo_HotelValuedAvailRQ.TipoPlan = Enum_TipoPlan.AlojamientoyTiquete; }

                if (ddlTarifa != null && ddlTarifa.SelectedValue != "-")
                { vo_HotelValuedAvailRQ.Clase = ddlTarifa.SelectedValue; }

                if (ddlTarifa != null && ddlTarifa.SelectedValue == "-")
                { vo_HotelValuedAvailRQ.Clase = "Y"; }
                CargarHotelOccupancy(vo_HotelValuedAvailRQ);
                vo_HotelValuedAvailRQ.TotalNights = iDias;
                vo_HotelValuedAvailRQ.PaginationData = "1";

                clsSesiones.setParametrosHotel(vo_HotelValuedAvailRQ);
                cParametros.Id = 1;
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.ViewMessage.Add("Por favor verifique los parametros de búsqueda");
                cParametros.Sugerencia.Add("Intente de nuevo");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        private List<VO_Passenger> CargarInfo(UserControl PageSource)
        {
            DropDownList cmbHabitacionesInterno = (DropDownList)PageSource.FindControl("cmbHabitacionesInterno");
            DropDownList cmbHabitaciones = (DropDownList)PageSource.FindControl("cmbHabitaciones");
            DropDownList cmbAdultos1 = (DropDownList)PageSource.FindControl("cmbAdultos1");
            DropDownList cmbAdultos2 = (DropDownList)PageSource.FindControl("cmbAdultos2");
            DropDownList cmbAdultos3 = (DropDownList)PageSource.FindControl("cmbAdultos3");
            DropDownList cmbAdultos4 = (DropDownList)PageSource.FindControl("cmbAdultos4");
            DropDownList cmbAdultos5 = (DropDownList)PageSource.FindControl("cmbAdultos5");
            DropDownList cmbAdultos6 = (DropDownList)PageSource.FindControl("cmbAdultos6");
            DropDownList cmbAdultos7 = (DropDownList)PageSource.FindControl("cmbAdultos7");
            DropDownList cmbAdultos8 = (DropDownList)PageSource.FindControl("cmbAdultos8");
            DropDownList cmbAdultos9 = (DropDownList)PageSource.FindControl("cmbAdultos9");
            DropDownList cmbNiños1 = (DropDownList)PageSource.FindControl("cmbNiños1");
            DropDownList cmbNiños2 = (DropDownList)PageSource.FindControl("cmbNiños2");
            DropDownList cmbNiños3 = (DropDownList)PageSource.FindControl("cmbNiños3");
            DropDownList cmbNiños4 = (DropDownList)PageSource.FindControl("cmbNiños4");
            DropDownList cmbNiños5 = (DropDownList)PageSource.FindControl("cmbNiños5");
            DropDownList cmbNiños6 = (DropDownList)PageSource.FindControl("cmbNiños6");
            DropDownList cmbNiños7 = (DropDownList)PageSource.FindControl("cmbNiños7");
            DropDownList cmbNiños8 = (DropDownList)PageSource.FindControl("cmbNiños8");
            DropDownList cmbNiños9 = (DropDownList)PageSource.FindControl("cmbNiños9");
            DropDownList ddlEdad11 = (DropDownList)PageSource.FindControl("ddlEdad11");
            DropDownList ddlEdad12 = (DropDownList)PageSource.FindControl("ddlEdad12");
            DropDownList ddlEdad21 = (DropDownList)PageSource.FindControl("ddlEdad21");
            DropDownList ddlEdad22 = (DropDownList)PageSource.FindControl("ddlEdad22");
            DropDownList ddlEdad31 = (DropDownList)PageSource.FindControl("ddlEdad31");
            DropDownList ddlEdad32 = (DropDownList)PageSource.FindControl("ddlEdad32");
            DropDownList ddlEdad41 = (DropDownList)PageSource.FindControl("ddlEdad41");
            DropDownList ddlEdad42 = (DropDownList)PageSource.FindControl("ddlEdad42");
            DropDownList ddlEdad51 = (DropDownList)PageSource.FindControl("ddlEdad51");
            DropDownList ddlEdad52 = (DropDownList)PageSource.FindControl("ddlEdad52");
            DropDownList ddlEdad61 = (DropDownList)PageSource.FindControl("ddlEdad61");
            DropDownList ddlEdad62 = (DropDownList)PageSource.FindControl("ddlEdad62");
            DropDownList ddlEdad71 = (DropDownList)PageSource.FindControl("ddlEdad71");
            DropDownList ddlEdad72 = (DropDownList)PageSource.FindControl("ddlEdad72");
            DropDownList ddlEdad81 = (DropDownList)PageSource.FindControl("ddlEdad81");
            DropDownList ddlEdad82 = (DropDownList)PageSource.FindControl("ddlEdad82");
            DropDownList ddlEdad91 = (DropDownList)PageSource.FindControl("ddlEdad91");
            DropDownList ddlEdad92 = (DropDownList)PageSource.FindControl("ddlEdad92");

            int val = int.Parse(cmbHabitaciones.SelectedValue);
            List<VO_Passenger> roomInfo = new List<VO_Passenger>();

            DropDownList[] ddlAdultos = new DropDownList[9];
            DropDownList[] ddlNinios = new DropDownList[9];
            //DropDownList[] ddlEdades = new DropDownList[18];
            DropDownList[,] ddlEdades = new DropDownList[9, 2];
            int iCon = 0;
            int iConEdad = 0;

            ddlAdultos[0] = cmbAdultos1;
            ddlAdultos[1] = cmbAdultos2;
            ddlAdultos[2] = cmbAdultos3;
            ddlAdultos[3] = cmbAdultos4;
            ddlAdultos[4] = cmbAdultos5;
            ddlAdultos[5] = cmbAdultos6;
            ddlAdultos[6] = cmbAdultos7;
            ddlAdultos[7] = cmbAdultos8;
            ddlAdultos[8] = cmbAdultos9;

            ddlNinios[0] = cmbNiños1;
            ddlNinios[1] = cmbNiños2;
            ddlNinios[2] = cmbNiños3;
            ddlNinios[3] = cmbNiños4;
            ddlNinios[4] = cmbNiños5;
            ddlNinios[5] = cmbNiños6;
            ddlNinios[6] = cmbNiños7;
            ddlNinios[7] = cmbNiños8;
            ddlNinios[8] = cmbNiños9;

            ddlEdades[0, 0] = ddlEdad11;
            ddlEdades[0, 1] = ddlEdad12;
            ddlEdades[1, 0] = ddlEdad21;
            ddlEdades[1, 1] = ddlEdad22;
            ddlEdades[2, 0] = ddlEdad31;
            ddlEdades[2, 1] = ddlEdad32;
            ddlEdades[3, 0] = ddlEdad41;
            ddlEdades[3, 1] = ddlEdad42;
            ddlEdades[4, 0] = ddlEdad51;
            ddlEdades[4, 1] = ddlEdad52;
            ddlEdades[5, 0] = ddlEdad61;
            ddlEdades[5, 1] = ddlEdad62;
            ddlEdades[6, 0] = ddlEdad71;
            ddlEdades[6, 1] = ddlEdad72;
            ddlEdades[7, 0] = ddlEdad81;
            ddlEdades[7, 1] = ddlEdad82;
            ddlEdades[8, 0] = ddlEdad91;
            ddlEdades[8, 1] = ddlEdad92;

            int iPos = 1;

            if (cmbHabitaciones != null)
            {
                while (int.Parse(cmbHabitaciones.SelectedValue) > iCon)
                {
                    VO_Passenger cPass = new VO_Passenger();
                    cPass.Adulto = ddlAdultos[iCon].SelectedValue;
                    cPass.Nino = ddlNinios[iCon].SelectedValue;
                    cPass.Infante = "0";
                    cPass.Pos = iPos.ToString();
                    cPass.Activo = true;
                    cPass.RoomCount = 1;

                    List<clsEdad> roomEdad = new List<clsEdad>();

                    bool bEdad = true;
                    iConEdad = 0;
                    while (int.Parse(ddlNinios[iCon].SelectedValue) > iConEdad)
                    {
                        clsEdad cEdad = new clsEdad();

                        cEdad.Pos = iPos.ToString();
                        cEdad.Tipo = Enum_TipoPassenger.Ninio;
                        cEdad.Clase = Enum_TipoEdad.Anios;
                        cEdad.Edad = ddlEdades[iCon, iConEdad].SelectedValue.ToString();
                        roomEdad.Add(cEdad);
                        iConEdad++;
                    }
                    if (bEdad)
                    {
                        clsEdad cEdad = new clsEdad();

                        cEdad.Pos = iPos.ToString();
                        cEdad.Tipo = Enum_TipoPassenger.Adulto;
                        cEdad.Clase = Enum_TipoEdad.Anios;
                        cEdad.Edad = "0";
                        roomEdad.Add(cEdad);
                    }
                    iPos++;
                    iCon++;
                    cPass.Edad = roomEdad;
                    roomInfo.Add(cPass);
                }
            }
            if (cmbHabitacionesInterno != null)
            {
                while (int.Parse(cmbHabitacionesInterno.SelectedValue) > iCon)
                {
                    VO_Passenger cPass = new VO_Passenger();
                    cPass.Adulto = ddlAdultos[iCon].SelectedValue;
                    cPass.Nino = ddlNinios[iCon].SelectedValue;
                    cPass.Infante = "0";
                    cPass.Pos = iPos.ToString();
                    cPass.Activo = true;
                    cPass.RoomCount = 1;

                    List<clsEdad> roomEdad = new List<clsEdad>();
                    iConEdad = 0;
                    bool bEdad = true;

                    while (int.Parse(ddlNinios[iCon].SelectedValue) > iConEdad)
                    {
                        clsEdad cEdad = new clsEdad();

                        cEdad.Pos = iPos.ToString();
                        cEdad.Tipo = Enum_TipoPassenger.Ninio;
                        cEdad.Clase = Enum_TipoEdad.Anios;
                        cEdad.Edad = ddlEdades[iCon, iConEdad].SelectedValue.ToString();
                        roomEdad.Add(cEdad);
                        iConEdad++;
                    }
                    if (bEdad)
                    {
                        clsEdad cEdad = new clsEdad();

                        cEdad.Pos = iPos.ToString();
                        cEdad.Tipo = Enum_TipoPassenger.Adulto;
                        cEdad.Clase = Enum_TipoEdad.Anios;
                        cEdad.Edad = "0";
                        roomEdad.Add(cEdad);
                    }
                    iPos++;
                    iCon++;
                    cPass.Edad = roomEdad;
                    roomInfo.Add(cPass);
                }
            }

            return Ordenar(roomInfo);
        }
        private List<VO_Passenger> Ordenar(List<VO_Passenger> lRomInfo)
        {
            List<VO_Passenger> roomInfoTemp = new List<VO_Passenger>();
            int iHabitacion = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < lRomInfo.Count; j++)
                {
                    if (int.Parse(lRomInfo[j].Nino).Equals(i))
                    {
                        lRomInfo[j].Pos = iHabitacion.ToString();
                        roomInfoTemp.Add(lRomInfo[j]);
                        iHabitacion++;
                    }
                }
            }
            int iCount = roomInfoTemp.Count;
            int iAd = 0;
            int iCn = 0;
            int iIn = 0;

            for (int k = 0; k < iCount; k++)
            {
                if (roomInfoTemp[k].Activo)
                {
                    List<int> lCantidad = new List<int>();

                    iAd = int.Parse(roomInfoTemp[k].Adulto);
                    iCn = int.Parse(roomInfoTemp[k].Nino);
                    iIn = int.Parse(roomInfoTemp[k].Infante);
                    //for (int h = 0; h < iCount; h++)
                    //{
                    //    if (!k.Equals(h))
                    //    {
                    //        if (roomInfoTemp[h].Adulto.Equals(iAd.ToString()) && roomInfoTemp[h].Nino.Equals(iCn.ToString()) && roomInfoTemp[h].Infante.Equals(iIn.ToString()))
                    //        {
                    //            lCantidad.Add(h);
                    //            roomInfoTemp[h].Activo = false;
                    //        }
                    //    }
                    //}
                    if (lCantidad.Count > 0)
                    {
                        roomInfoTemp[k].RoomCount = lCantidad.Count + 1;
                        if (roomInfoTemp[k].Edad.Count > 0)
                        {
                            List<clsEdad> roomEdad = new List<clsEdad>();
                            for (int s = 0; s < roomInfoTemp[k].Edad.Count; s++)
                            {
                                roomEdad.Add(roomInfoTemp[k].Edad[s]);
                            }
                            for (int s = 0; s < lCantidad.Count; s++)
                            {
                                for (int m = 0; m < roomInfoTemp[lCantidad[s]].Edad.Count; m++)
                                {
                                    roomEdad.Add(roomInfoTemp[lCantidad[s]].Edad[m]);
                                }
                            }
                            roomInfoTemp[k].Edad = roomEdad;
                        }
                    }
                }
            }
            iHabitacion = 1;
            for (int k = 0; k < iCount; k++)
            {
                if (!roomInfoTemp[k].Activo)
                {
                    roomInfoTemp[k].Pos = "99";
                }
                else
                {
                    roomInfoTemp[k].Pos = iHabitacion.ToString();
                    iHabitacion++;
                }
            }
            return roomInfoTemp;
        }
        private List<VO_Passenger> CargarInfoCotelco(UserControl PageSource)
        {
            DropDownList cmbHabitaciones = (DropDownList)PageSource.FindControl("cmbHabitaciones");
            DropDownList cmbAdultos1 = (DropDownList)PageSource.FindControl("cmbAdultos1");
            //DropDownList cmbAdultos2 = (DropDownList)PageSource.FindControl("cmbAdultos2");
            //DropDownList cmbAdultos3 = (DropDownList)PageSource.FindControl("cmbAdultos3");
            //DropDownList cmbAdultos4 = (DropDownList)PageSource.FindControl("cmbAdultos4");
            //DropDownList cmbAdultos5 = (DropDownList)PageSource.FindControl("cmbAdultos5");
            //DropDownList cmbAdultos6 = (DropDownList)PageSource.FindControl("cmbAdultos6");
            //DropDownList cmbAdultos7 = (DropDownList)PageSource.FindControl("cmbAdultos7");
            //DropDownList cmbAdultos8 = (DropDownList)PageSource.FindControl("cmbAdultos8");
            DropDownList cmbNiños1 = (DropDownList)PageSource.FindControl("cmbNiños1");
            //DropDownList cmbNiños2 = (DropDownList)PageSource.FindControl("cmbNinos2");
            //DropDownList cmbNiños3 = (DropDownList)PageSource.FindControl("cmbNinos3");
            //DropDownList cmbNiños4 = (DropDownList)PageSource.FindControl("cmbNinos4");
            //DropDownList cmbNiños5 = (DropDownList)PageSource.FindControl("cmbNinos5");
            //DropDownList cmbNiños6 = (DropDownList)PageSource.FindControl("cmbNinos6");
            //DropDownList cmbNiños7 = (DropDownList)PageSource.FindControl("cmbNinos7");
            //DropDownList cmbNiños8 = (DropDownList)PageSource.FindControl("cmbNinos8");
            DropDownList cmbBebes1 = (DropDownList)PageSource.FindControl("cmbBebes1");
            //DropDownList cmbBebes2 = (DropDownList)PageSource.FindControl("cmbBebes2");
            //DropDownList cmbBebes3 = (DropDownList)PageSource.FindControl("cmbBebes3");
            //DropDownList cmbBebes4 = (DropDownList)PageSource.FindControl("cmbBebes4");
            //DropDownList cmbBebes5 = (DropDownList)PageSource.FindControl("cmbBebes5");
            //DropDownList cmbBebes6 = (DropDownList)PageSource.FindControl("cmbBebes6");
            //DropDownList cmbBebes7 = (DropDownList)PageSource.FindControl("cmbBebes7");
            //DropDownList cmbBebes8 = (DropDownList)PageSource.FindControl("cmbBebes8");
            //DropDownList ddlMultiEdad1 = (DropDownList)PageSource.FindControl("ddlMultiEdad1");
            //DropDownList ddlMultiEdad2 = (DropDownList)PageSource.FindControl("ddlMultiEdad2");
            //DropDownList ddlMultiEdad3 = (DropDownList)PageSource.FindControl("ddlMultiEdad3");
            //DropDownList ddlMultiEdad4 = (DropDownList)PageSource.FindControl("ddlMultiEdad4");
            //DropDownList ddlMultiEdad5 = (DropDownList)PageSource.FindControl("ddlMultiEdad5");
            //DropDownList ddlMultiEdad6 = (DropDownList)PageSource.FindControl("ddlMultiEdad6");
            //DropDownList ddlMultiMeses1 = (DropDownList)PageSource.FindControl("ddlMultiMeses1");
            //DropDownList ddlMultiMeses2 = (DropDownList)PageSource.FindControl("ddlMultiMeses2");
            //DropDownList ddlMultiMeses3 = (DropDownList)PageSource.FindControl("ddlMultiMeses3");
            //DropDownList ddlMultiMeses4 = (DropDownList)PageSource.FindControl("ddlMultiMeses4");
            //DropDownList ddlMultiMeses5 = (DropDownList)PageSource.FindControl("ddlMultiMeses5");
            //DropDownList ddlMultiMeses6 = (DropDownList)PageSource.FindControl("ddlMultiMeses6");

            int val = int.Parse(cmbHabitaciones.SelectedValue);
            List<VO_Passenger> roomInfo = new List<VO_Passenger>();

            DropDownList[] ddlAdultos = new DropDownList[1];
            DropDownList[] ddlNinios = new DropDownList[1];
            DropDownList[] ddlBebes = new DropDownList[1];
            //DropDownList[] ddlAdultos = new DropDownList[8];
            //DropDownList[] ddlNinios = new DropDownList[8];
            //DropDownList[] ddlBebes = new DropDownList[8];
            //DropDownList[] ddlEdades = new DropDownList[6];
            //DropDownList[] ddlMeses = new DropDownList[6];
            int iCon = 0;


            ddlAdultos[0] = cmbAdultos1;
            //ddlAdultos[1] = cmbAdultos2;
            //ddlAdultos[2] = cmbAdultos3;
            //ddlAdultos[3] = cmbAdultos4;
            //ddlAdultos[4] = cmbAdultos5;
            //ddlAdultos[5] = cmbAdultos6;
            //ddlAdultos[6] = cmbAdultos7;
            //ddlAdultos[7] = cmbAdultos8;
            if (cmbNiños1 != null)
            {
                ddlNinios[0] = cmbNiños1;
            }
            //ddlNinios[1] = cmbNiños2;
            //ddlNinios[2] = cmbNiños3;
            //ddlNinios[3] = cmbNiños4;
            //ddlNinios[4] = cmbNiños5;
            //ddlNinios[5] = cmbNiños6;
            //ddlNinios[6] = cmbNiños7;
            //ddlNinios[7] = cmbNiños8;
            if (cmbBebes1 != null)
            {
                ddlBebes[0] = cmbBebes1;
            }
            //ddlBebes[1] = cmbBebes2;
            //ddlBebes[2] = cmbBebes3;
            //ddlBebes[3] = cmbBebes4;
            //ddlBebes[4] = cmbBebes5;
            //ddlBebes[5] = cmbBebes6;
            //ddlBebes[6] = cmbBebes7;
            //ddlBebes[7] = cmbBebes8;

            //ddlEdades[0] = ddlMultiEdad1;
            //ddlEdades[1] = ddlMultiEdad2;
            //ddlEdades[2] = ddlMultiEdad3;
            //ddlEdades[3] = ddlMultiEdad4;
            //ddlEdades[4] = ddlMultiEdad5;
            //ddlEdades[5] = ddlMultiEdad6;

            //ddlMeses[0] = ddlMultiMeses1;
            //ddlMeses[1] = ddlMultiMeses2;
            //ddlMeses[2] = ddlMultiMeses3;
            //ddlMeses[3] = ddlMultiMeses4;
            //ddlMeses[4] = ddlMultiMeses5;
            //ddlMeses[5] = ddlMultiMeses6;

            int iPos = 1;

            while (int.Parse(cmbHabitaciones.SelectedValue) > iCon)
            {
                VO_Passenger cPass = new VO_Passenger();
                cPass.Adulto = ddlAdultos[iCon].SelectedValue;
                if (cmbNiños1 != null)
                {
                    cPass.Nino = ddlNinios[iCon].SelectedValue;
                }
                else
                {
                    cPass.Nino = "0";
                }
                if (cmbBebes1 != null)
                {
                    cPass.Infante = ddlBebes[iCon].SelectedValue;
                }
                else
                {
                    cPass.Infante = "0";
                }
                cPass.Pos = iPos.ToString();
                cPass.Activo = true;
                cPass.RoomCount = 1;

                List<clsEdad> roomEdad = new List<clsEdad>();


                bool bEdad = true;

                if (bEdad)
                {
                    clsEdad cEdad = new clsEdad();

                    cEdad.Pos = iPos.ToString();
                    cEdad.Tipo = Enum_TipoPassenger.Adulto;
                    cEdad.Clase = Enum_TipoEdad.Anios;
                    cEdad.Edad = "0";
                    roomEdad.Add(cEdad);
                }
                iPos++;
                iCon++;
                cPass.Edad = roomEdad;
                roomInfo.Add(cPass);
            }
            return Ordenar(roomInfo);
        }
        private void CargarHotelOccupancy(VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ)
        {
            List<VO_Passenger> cPassenger = clsSesiones.getPassenger();
            List<VO_HotelOccupancy> lvo_HotelOccupancy = new List<VO_HotelOccupancy>();

            int iTotalAD = 0;
            int iTotalCN = 0;
            int iTotalIF = 0;
            int iTotalJnr = 0;
            int intHabitaciones = cPassenger.Count;
            int iNumAD = 0;
            int iNumCN = 0;
            int iNumIF = 0;
            int iNumJnr = 0;
            int iContador = 1;

            string sAdulto = clsValidaciones.GetKeyOrAdd("AdultoHB", "AD");
            string sInfante = clsValidaciones.GetKeyOrAdd("InfanteHB", "CH");
            string sNino = clsValidaciones.GetKeyOrAdd("NinoHB", "CH");
            string sJunior = clsValidaciones.GetKeyOrAdd("JuniorHB", "JNR");

            for (int i = 0; i < intHabitaciones; i++)
            {
                if (cPassenger[i].Activo)
                {
                    iNumAD = int.Parse(cPassenger[i].Adulto);
                    iNumCN = int.Parse(cPassenger[i].Nino);
                    iNumIF = int.Parse(cPassenger[i].Infante);
                    iNumJnr = int.Parse(cPassenger[i].Junnior);

                    iTotalAD += iNumAD;
                    iTotalCN += iNumCN;
                    iTotalIF += iNumIF;
                    iTotalJnr += iNumJnr;

                    VO_HotelOccupancy vo_HotelOccupancy = new VO_HotelOccupancy();
                    vo_HotelOccupancy.RoomCount = cPassenger[i].RoomCount;

                    VO_Occupancy vo_Occupancy = new VO_Occupancy();
                    vo_Occupancy.AdultCount = iNumAD;
                    vo_Occupancy.ChildCount = iNumCN;
                    vo_Occupancy.BabyCount = iNumIF;
                    vo_Occupancy.JuniorCount = iNumJnr;

                    List<VO_Customer> lvo_GuestList = new List<VO_Customer>();
                    for (int k = 0; k < cPassenger[i].Edad.Count; k++)
                    {
                        if (cPassenger[i].Edad[k].Tipo == Enum_TipoPassenger.Ninio)
                        {
                            VO_Customer vo_GuestList = new VO_Customer(sNino, iContador, int.Parse(cPassenger[i].Edad[k].Edad), string.Empty, string.Empty);
                            lvo_GuestList.Add(vo_GuestList);
                        }
                    }

                    for (int j = 0; j < cPassenger[i].Edad.Count; j++)
                    {
                        if (cPassenger[i].Edad[j].Tipo == Enum_TipoPassenger.Infante)
                        {
                            VO_Customer vo_GuestList = new VO_Customer(sInfante, iContador, int.Parse(cPassenger[i].Edad[j].Edad), string.Empty, string.Empty);
                            lvo_GuestList.Add(vo_GuestList);
                        }
                    }
                    for (int j = 0; j < cPassenger[i].Edad.Count; j++)
                    {
                        if (cPassenger[i].Edad[j].Tipo == Enum_TipoPassenger.Junior)
                        {
                            VO_Customer vo_GuestList = new VO_Customer(sJunior, iContador, int.Parse(cPassenger[i].Edad[j].Edad), string.Empty, string.Empty);
                            lvo_GuestList.Add(vo_GuestList);
                        }
                    }

                    VO_Customer vo_GuestListI = new VO_Customer(sAdulto, iContador, 30, string.Empty, string.Empty);
                    lvo_GuestList.Add(vo_GuestListI);
                    vo_Occupancy.lGuestList = lvo_GuestList;
                    vo_HotelOccupancy.Occupancy = vo_Occupancy;
                    lvo_HotelOccupancy.Add(vo_HotelOccupancy);
                    iContador++;
                }
                vo_HotelValuedAvailRQ.lHotelOccupancy = lvo_HotelOccupancy;
                vo_HotelValuedAvailRQ.TotalRoom = cPassenger.Count;
                vo_HotelValuedAvailRQ.TotalAdult = iTotalAD;
                vo_HotelValuedAvailRQ.TotalChild = iTotalCN;
                vo_HotelValuedAvailRQ.TotalInf = iTotalIF;
                vo_HotelValuedAvailRQ.TotalJunior = iTotalJnr;
            }
        }
        public void setResultados(UserControl PageSource, string sOrder, string sEstrellas)
        {
            DataSet dsResultados = setTabla();
            //Utils.Utils cUtil = new Utils.Utils();
            if (dsResultados != null)
            {
                VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();
                DataTable dtHotelInfo = dsResultados.Tables[TABLA_HOTEL_INFO];
                DataTable dtHotelRoom = dsResultados.Tables[TABLA_HOTEL_ROOM];
                DataTable dtHotelOcupancy = dsResultados.Tables[TABLA_HOTEL_OCCUPANCY];
                DataTable dtHabitacion = dsResultados.Tables[TABLA_HABITACIONES];
                DataTable dtInstalaciones = dsResultados.Tables[TABLA_INSTALACIONES];
                Repeater rptHoteles = (Repeater)PageSource.FindControl("rptHotel");
                Label lblCiudad = (Label)PageSource.FindControl("lblCiudad");
                Label lblFechaSalida = (Label)PageSource.FindControl("lblFechaSalida");
                Label lblFechaLlegada = (Label)PageSource.FindControl("lblFechaLlegada");
                Label lblHuespedes = (Label)PageSource.FindControl("lblHuespedes");
                Label lblResultados = (Label)PageSource.FindControl("lblResultados");
                Image iOfertaPromosImg = (Image)PageSource.FindControl("iOfertaPromosImg");
                Button btnNext = (Button)PageSource.FindControl("btnNext");
                Button btnBack = (Button)PageSource.FindControl("btnBack");

                Label lblAdultos = (Label)PageSource.FindControl("lblAcomodacion");

                Repeater RptPag = (Repeater)PageSource.FindControl("RptPagina");
                Repeater RptPagInf = (Repeater)PageSource.FindControl("RptInf");

                if (lblCiudad != null)
                    lblCiudad.Text = dtHotelInfo.Rows[0][COLUMN_DESTINATION_NAME].ToString();
                if (lblFechaSalida != null)
                    lblFechaSalida.Text = clsValidaciones.ConverYMDtoDMMY(dtHotelInfo.Rows[0][COLUMN_DATE_FROM_FORMAT].ToString(), " ");
                if (lblFechaLlegada != null)
                    lblFechaLlegada.Text = clsValidaciones.ConverYMDtoDMMY(dtHotelInfo.Rows[0][COLUMN_DATE_TO_FORMAT].ToString(), " ");
                if (lblResultados != null)
                    lblResultados.Text = dsResultados.Tables[TABLA_HOTEL_VALUE_AVAIL].Rows[0][COLUMN_TOTAL_ITEMS].ToString();

                if (lblAdultos != null)
                {
                    lblAdultos.Text = string.Empty;
                    for (int j = 0; j < dsResultados.Tables[TABLA_HABITACIONES].Rows.Count; j++)
                    {
                        if (!lblAdultos.Text.Length.Equals(0))
                        {
                            lblAdultos.Text += "</P>";
                        }
                        lblAdultos.Text += dsResultados.Tables[TABLA_HABITACIONES].Rows[j][COLUMN_ADULT_COUNT].ToString() + " Adultos / " + dsResultados.Tables[TABLA_HABITACIONES].Rows[j][COLUMN_CHILD_COUNT].ToString() + " Niños ";
                        lblAdultos.Text += " X " + dsResultados.Tables[TABLA_HABITACIONES].Rows[j][COLUMN_ROOM_COUNT].ToString() + " habitaciones";

                    }
                }
                int iTotalxPag = dtHotelInfo.Rows.Count;

                int iPos = 0;
                int iTotal = 0;

                if (clsValidaciones.GetKeyOrAdd("paginacionHTotaltrip", "false").ToUpper().Trim().Equals("TRUE"))
                {
                    iPos = int.Parse(clsValidaciones.GetKeyOrAdd("cantpaginacionHTotaltrip", "5"));
                    iTotal = iTotalxPag / iPos;
                }
                else
                {
                    iPos = int.Parse(dsResultados.Tables[TABLA_PAGINATION_DATA].Rows[0][COLUMN_RESULT_PAGE].ToString());
                    iTotal = int.Parse(dsResultados.Tables[TABLA_PAGINATION_DATA].Rows[0][COLUMN_TOTAL_PAGES].ToString());
                }
                int iPosPag = 0;
                int iCurrentPag = 0;
                try
                {
                    iPosPag = int.Parse(vo_HotelValuedAvailRQ.PaginationData.ToString());
                    iCurrentPag = vo_HotelValuedAvailRQ.CurrentPage;
                }
                catch { }

                clsSesiones.setPage(iPosPag.ToString());
                clsSesiones.setPageMax(iTotal.ToString());
                try
                {
                    btnNext.Enabled = true;
                    btnBack.Enabled = true;

                    if (iPosPag == 1)
                    {
                        btnBack.Enabled = false;
                    }
                    if (iPosPag == iTotal)
                    {
                        btnNext.Enabled = false;
                    }
                }
                catch { }
                string sOrdenTable = sOrder;
                DataTable dtHotelInfoAux = dtHotelInfo;
                if (sEstrellas != null && sEstrellas != "")
                {


                    string[] arrQuery = sEstrellas.Split(new char[] { '|' });


                    if (arrQuery.Count() > 1)
                    {
                        //Filtros para Cargar lose servicios

                        if (arrQuery[1].Equals("1"))//Filtro Servicios
                        {
                            var qRooms = (from Pl in dtInstalaciones.AsEnumerable()
                                          where (Pl.Field<String>("Code")) == (arrQuery[0])
                                          select new
                                          {
                                              IdHotel = Pl.Field<Int32>("HotelInfo_id")
                                          });


                            string sWhere = string.Empty;
                            foreach (var Id in qRooms)
                            {
                                if (sWhere == string.Empty)
                                {
                                    sWhere = " HotelInfo_id=" + Id.IdHotel;
                                }
                                else
                                {
                                    sWhere = sWhere + "" + " OR HotelInfo_id=" + Id.IdHotel;
                                }


                            }
                            dtHotelInfoAux = clsDataNet.dsDataWhereOrder(sWhere, sOrdenTable, dtHotelInfo);
                        }

                        //Filtro Nombre
                        if (arrQuery[1].Equals("2"))
                        {
                            string sValue = "Name LIKE '" + arrQuery[0] + "%'";
                            dtHotelInfoAux = clsDataNet.dsDataWhereOrder(sValue, sOrdenTable, dtHotelInfo);


                        }



                    }
                    else
                    {

                        if (clsValidaciones.IS_NUMERIC(sEstrellas.Substring(0, 1)))
                        {

                            dtHotelInfoAux = clsDataNet.dsDataWhereOrder(COLUMN_CATEGORY_CODE + "='" + sEstrellas + "'", sOrdenTable, dtHotelInfo);
                        }
                        else
                        {
                            dtHotelInfoAux = clsDataNet.dsDataWhereOrder(COLUMN_NAME + " LIKE '%" + sEstrellas + "%'", sOrdenTable, dtHotelInfo);
                        }
                    }
                    rptHoteles.DataSource = null;
                    rptHoteles.DataBind();
                    setRepeater(dtHotelInfoAux, rptHoteles, sOrdenTable, RptPag, iPos, iTotal, iPosPag, iCurrentPag, RptPagInf);

                }
                else
                {
                    dtHotelInfoAux = clsDataNet.dsDataOrder(sOrdenTable, dtHotelInfo);

                    setRepeater(dtHotelInfoAux, rptHoteles, sOrdenTable, RptPag, iPos, iTotal, iPosPag, iCurrentPag, RptPagInf);
                }



                int i = 0;
                int iPosIni = iCurrentPag * iPos;
                int iCountTable = 0;
                foreach (DataRow drHotelInfo in dtHotelInfoAux.Rows)
                {
                    if (iCountTable >= iPosIni)
                    {
                        if (i < iPos)
                        {
                            int iDias = clsValidaciones.CalcularDias(drHotelInfo[COLUMN_DATE_FROM_FORMAT].ToString(), drHotelInfo[COLUMN_DATE_TO_FORMAT].ToString());
                            DateTime dtFecha = clsValidaciones.RetornaFecha(drHotelInfo[COLUMN_DATE_TO_FORMAT].ToString());

                            Repeater rptOcupacion = (Repeater)rptHoteles.Items[i].FindControl("rptOcupacion");

                            Repeater RptEstrellas = (Repeater)rptHoteles.Items[i].FindControl("RptEstrellas");

                            DataRow[] drRooms = dtHotelRoom.Select(COLUMN_HOTEL_INFO_ID + "='" + drHotelInfo[COLUMN_HOTEL_INFO_ID].ToString() + "'");

                            setEstrellas(drHotelInfo[COLUMN_CATEGORY_CODE].ToString(), drHotelInfo[COLUMN_CATEGORY_TEXT].ToString(), RptEstrellas);

                            setOcupacion(rptOcupacion, dtHabitacion, drRooms, dtHotelOcupancy);
                        }
                        i++;
                    }
                    iCountTable++;
                }

            }
        }
        public DataSet setTabla()
        {
            DataSet dsResultados = clsSesiones.getResultadoHotel();
            bool bHotel = true;
            try
            {
                if (dsResultados.Tables[TABLA_HOTEL_INFO].Rows[0][COLUMN_WS_SELECT].ToString().Equals(clsValidaciones.GetKeyOrAdd("WS_HOTEL_TT", "WS_TT")))
                {
                    bHotel = false;
                }
                if (dsResultados.Tables[TABLA_HOTEL_INFO].Rows[0][COLUMN_WS_SELECT].ToString().Equals(clsValidaciones.GetKeyOrAdd("WS_HOTEL_TRC", "WS_TRC")))
                {
                    bHotel = false;
                }
            }
            catch { }
            if (bHotel)
            {
                setHotelInfo(dsResultados);
                setRoomType(dsResultados);
                setOcupancy(dsResultados);
            }
            setTablaHabitaciones(dsResultados);
            clsSesiones.setResultadoHotel(dsResultados);
            return dsResultados;
        }
        public DataSet setTablaReserva()
        {
            DataSet dsResultados = clsSesiones.getReservaHotel();
            bool bHotel = true;
            try
            {
                if (dsResultados.Tables[TABLA_HOTEL_INFO].Rows[0][COLUMN_WS_SELECT].ToString().Equals(clsValidaciones.GetKeyOrAdd("WS_HOTEL_TT", "WS_TT")))
                {
                    bHotel = false;
                }
                if (dsResultados.Tables[TABLA_HOTEL_INFO].Rows[0][COLUMN_WS_SELECT].ToString().Equals(clsValidaciones.GetKeyOrAdd("WS_HOTEL_TRC", "WS_TRC")))
                {
                    bHotel = false;
                }
            }
            catch { }
            if (bHotel)
            {
                setHotel(dsResultados);
                setHotelRoom(dsResultados);
                setPenalRoom(dsResultados);
                setAditionalRoom(dsResultados);
            }
            clsSesiones.setReservaHotel(dsResultados);
            return dsResultados;
        }
        public void setHotelInfo(DataSet dsResultados)
        {
            //Utils.Utils cUtil = new Utils.Utils();
            string sURL = string.Empty;
            string sImagenPath = clsValidaciones.RutaImagesGen();
            try
            {
                // Traemos la tabla
                DataTable dtHotelInfo = dsResultados.Tables[TABLA_HOTEL_INFO];
                DataTable dtPageData = dsResultados.Tables[TABLA_PAGINATION_DATA];

                // Adicionamos las columnas
                dtHotelInfo.Columns.Add(COLUMN_CATEGORY_CODE, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_CATEGORY_TEXT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DESTINATION_CODE, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DESTINATION_NAME, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_ZONE_TEXT, typeof(string));
                //dtHotelInfo.Columns.Add(COLUMN_ZONE_CODE, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_CLASIFICATION_CODE, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_CLASIFICATION_TEXT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_LATITUDE, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_LONGITUDE, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_IMAGEN_URL, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DATE_FROM, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DATE_FROM_FORMAT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DATE_TO, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DATE_TO_FORMAT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DETALLES_URL, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_OFERTA, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DESCRIPTION, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DESCRIPTION_LONG, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_ADDRESS, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_CURRENCY_TEST, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_AMOUNT, typeof(decimal));
                dtHotelInfo.Columns.Add(COLUMN_AMOUNT_TEXT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_WS_SELECT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_AGENCY_COMISION, typeof(decimal));
                dtHotelInfo.Columns.Add(COLUMN_OBSERVACIONES, typeof(string));
                string sWsSelect = clsValidaciones.GetKeyOrAdd("WS_HOTEL_HB", "HOTBED");

                // Define el numero de resultados por la pagina
                dtPageData.Columns.Add(COLUMN_RESULT_PAGE, typeof(int));
                int iResult = dtHotelInfo.Rows.Count;
                foreach (DataRow drPageData in dtPageData.Rows)
                {
                    drPageData[COLUMN_RESULT_PAGE] = iResult;
                }

                // almacenamos datos
                foreach (DataRow drHotelInfo in dtHotelInfo.Rows)
                {
                    foreach (DataRow drCategory in drHotelInfo.GetChildRows(RELACION_HOTELINFO_CATEGORY))
                    {
                        drHotelInfo[COLUMN_CATEGORY_CODE] = drCategory[COLUMN_CODE];
                        drHotelInfo[COLUMN_CATEGORY_TEXT] = drCategory[COLUMN_CATEGORY_TEXT];
                    }
                    foreach (DataRow drPosition in drHotelInfo.GetChildRows(RELACION_HOTELINFO_POSITION))
                    {
                        drHotelInfo[COLUMN_LATITUDE] = drPosition[COLUMN_LATITUDE];
                        drHotelInfo[COLUMN_LONGITUDE] = drPosition[COLUMN_LONGITUDE];
                    }
                    foreach (DataRow drImageList in drHotelInfo.GetChildRows(RELACION_HOTELINFO_IMAGELIST))
                    {
                        foreach (DataRow drImage in drImageList.GetChildRows(RELACION_IMAGELIST_IMAGE))
                        {
                            drImage[COLUMN_URL] = drImage[COLUMN_URL].ToString().Replace("small/", "");
                            drHotelInfo[COLUMN_IMAGEN_URL] = drImage[COLUMN_URL];
                            break;
                        }
                    }
                    foreach (DataRow drDestination in drHotelInfo.GetChildRows(RELACION_HOTELINFO_DESTINATION))
                    {
                        foreach (DataRow drZoneList in drDestination.GetChildRows(RELACION_DESTINATION_ZONELIST))
                        {
                            foreach (DataRow drZone in drZoneList.GetChildRows(RELACION_ZONELIST_ZONE))
                            {
                                drHotelInfo[COLUMN_ZONE_TEXT] = drZone[COLUMN_ZONE_TEXT].ToString();
                                drHotelInfo[COLUMN_DESTINATION_CODE] = drDestination[COLUMN_CODE];
                                drHotelInfo[COLUMN_DESTINATION_NAME] = drDestination[COLUMN_NAME];
                                break;
                            }
                        }
                    }
                    foreach (DataRow drServiceHotel in drHotelInfo.GetParentRows(RELACION_SERVICEHOTEL_HOTELINFO))
                    {
                        foreach (DataRow drCurrency in drServiceHotel.GetChildRows(RELACION_SERVICEHOTEL_CURRENCY))
                        {
                            drHotelInfo[COLUMN_CURRENCY_CODE] = drCurrency[COLUMN_CODE];
                            drHotelInfo[COLUMN_CURRENCY_TEST] = drCurrency[COLUMN_CURRENCY_TEST];
                        }
                        decimal dPrice = 0;
                        decimal dPriceMin = 0;
                        foreach (DataRow drAvailableRoom in drServiceHotel.GetChildRows(RELACION_SERVICEHOTEL_AVAILABLEROOM))
                        {
                            foreach (DataRow drHotelRoom in drAvailableRoom.GetChildRows(RELACION_AVAILABLEROOM_HOTELROOM))
                            {
                                foreach (DataRow drPrice in drHotelRoom.GetChildRows(RELACION_HOTELROOM_PRICE))
                                {
                                    if (dPriceMin == 0)
                                    {
                                        dPriceMin = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(clsValidaciones.getDecimalRound(drPrice[COLUMN_AMOUNT].ToString()) / decimal.Parse(clsValidaciones.GetKeyOrAdd("Incremento")))));
                                    }
                                    dPrice = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(clsValidaciones.getDecimalRound(drPrice[COLUMN_AMOUNT].ToString()) / decimal.Parse(clsValidaciones.GetKeyOrAdd("Incremento")))));//Convert.ToDecimal(clsValidaciones.getDecimalRound(drPrice[COLUMN_AMOUNT].ToString()));
                                    if (dPrice <= dPriceMin)
                                    {
                                        drHotelInfo[COLUMN_AMOUNT] = dPrice;
                                        drHotelInfo[COLUMN_AMOUNT_TEXT] = Convert.ToDecimal(drHotelInfo[COLUMN_AMOUNT].ToString()).ToString(FORMATO_NUMEROS_SD);
                                        dPriceMin = Convert.ToDecimal(drHotelInfo[COLUMN_AMOUNT].ToString());
                                    }
                                }
                            }
                        }
                        foreach (DataRow drContractListList in drServiceHotel.GetChildRows(RELACION_SERVICEHOTEL_CONTRACTLIST))
                        {
                            foreach (DataRow drContrat in drContractListList.GetChildRows(RELACION_CONTRACTLIST_CONTRACT))
                            {
                                foreach (DataRow drClasification in drContrat.GetChildRows(RELACION_CONTRACT_CLASIFICATION))
                                {
                                    drHotelInfo[COLUMN_CLASIFICATION_CODE] = drClasification[COLUMN_CODE];
                                    drHotelInfo[COLUMN_CLASIFICATION_TEXT] = drClasification[COLUMN_CLASIFICATION_TEXT];
                                    //try
                                    //{
                                    //    if (drClasification[COLUMN_CODE].Equals("NOR"))
                                    //    {
                                    //        drHotelInfo[COLUMN_OFERTA] = sImagenPath + "spacer.gif";
                                    //    }
                                    //    else if (drClasification[COLUMN_CODE].Equals("SPE"))
                                    //    {
                                    //        drHotelInfo[COLUMN_OFERTA] = sImagenPath + "oferta.png";
                                    //    }
                                    //    else if (drClasification[COLUMN_CODE].Equals("NRF"))
                                    //    {
                                    //        drHotelInfo[COLUMN_OFERTA] = sImagenPath + "nfr.png";
                                    //    }
                                    //    else
                                    //    {
                                    //        drHotelInfo[COLUMN_OFERTA] = sImagenPath + "extra.png";
                                    //    }
                                    //}
                                    //catch { drHotelInfo[COLUMN_OFERTA] = sImagenPath + "spacer.gif"; }
                                }
                            }
                        }
                        foreach (DataRow drdateFrom in drServiceHotel.GetChildRows(RELACION_SERVICEHOTEL_DATE_FROM))
                        {
                            drHotelInfo[COLUMN_DATE_FROM] = drdateFrom[COLUMN_DATE];
                            drHotelInfo[COLUMN_DATE_FROM_FORMAT] = clsValidaciones.ConverYMDtoYMD(drdateFrom[COLUMN_DATE].ToString());
                        }
                        foreach (DataRow drdateTo in drServiceHotel.GetChildRows(RELACION_SERVICEHOTEL_DATE_TO))
                        {
                            drHotelInfo[COLUMN_DATE_TO] = drdateTo[COLUMN_DATE];
                            drHotelInfo[COLUMN_DATE_TO_FORMAT] = clsValidaciones.ConverYMDtoYMD(drdateTo[COLUMN_DATE].ToString());
                        }
                    }
                    sURL = clsValidaciones.ObtenerUrlRutaPage("../Presentacion/Detalle_Hotel.aspx?ID=" + drHotelInfo[COLUMN_CODE].ToString());
                    drHotelInfo[COLUMN_DETALLES_URL] = sURL;
                    drHotelInfo[COLUMN_DESCRIPTION] = sDescription(drHotelInfo[COLUMN_CODE].ToString(), "CAS");
                    drHotelInfo[COLUMN_DESCRIPTION_LONG] = drHotelInfo[COLUMN_DESCRIPTION];
                    drHotelInfo[COLUMN_WS_SELECT] = sWsSelect;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
            }
        }
        public void setRepeater(DataTable tblDatos, Repeater rpt, string sOrden, Repeater RptPag, int PageSize, int? Pagina, int Pos, int CurrentPage, Repeater RptInf)
        {
            PagedDataSource pagedData = new PagedDataSource();
            try
            {
                tblDatos.DefaultView.Sort = sOrden;
            }
            catch { }
            pagedData.DataSource = tblDatos.DefaultView;
            pagedData.AllowPaging = true;
            pagedData.PageSize = PageSize;
            try
            {
                pagedData.CurrentPageIndex = CurrentPage;
            }
            catch
            {
                pagedData.CurrentPageIndex = 0;
            }

            rpt.DataSource = pagedData;
            rpt.DataBind();
            if (Pagina != null)
            {
                setPagina(RptPag, Pagina, Pos);
                setPagina(RptInf, Pagina, Pos);
            }
            else
            {
                setPagina(RptPag, pagedData.PageCount, Pos);
                setPagina(RptInf, pagedData.PageCount, Pos);
            }


        }
        public void setPagina(Repeater RptPag, int? iTotal, int iPos)
        {
            try
            {
                string sPag = "tblPaginador";
                string stext = "text";
                string svalue = "value";
                string sclass = "class";

                DataTable tblPag = new DataTable(sPag);
                DataColumn dctext = new DataColumn(stext);
                DataColumn dcvalue = new DataColumn(svalue);
                DataColumn dcclass = new DataColumn(sclass);

                tblPag.Columns.Add(dctext);
                tblPag.Columns.Add(dcvalue);
                tblPag.Columns.Add(dcclass);

                for (int j = 1; j <= iTotal; j++)
                {
                    DataRow filaPag = tblPag.NewRow();
                    filaPag[stext] = j.ToString();
                    filaPag[svalue] = j.ToString();
                    if (iPos == j)
                    {
                        filaPag[sclass] = "csPintar";
                    }
                    else
                    {
                        filaPag[sclass] = "csNoPintar";
                    }

                    tblPag.Rows.Add(filaPag);
                }
                RptPag.DataSource = tblPag;
                RptPag.DataBind();
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        public void setEstrellas(string sNivelEstrellas, string sEstrellasTexto, Repeater RptEstrellas)
        {
            try
            {
                if (RptEstrellas != null)
                {
                    int Pos = sNivelEstrellas.IndexOf("E");
                    if (Pos > -1)
                        sNivelEstrellas = sNivelEstrellas.Remove(Pos);

                    Pos = sNivelEstrellas.IndexOf("H");
                    if (Pos > -1)
                        sNivelEstrellas = sNivelEstrellas.Remove(0, 1);

                    if (!String.IsNullOrEmpty(sNivelEstrellas))
                    {
                        string[] sNumEs = null;

                        if (sNivelEstrellas.Contains("_"))
                        {
                            sNumEs = sNivelEstrellas.Split(new char[] { '_' });
                        }
                        else
                        {
                            if (sNivelEstrellas.Contains("."))
                            {
                                sNumEs = sNivelEstrellas.Split(new char[] { '.' });
                            }
                            else
                            {
                                sNumEs = sNivelEstrellas.Split(new char[] { ',' });
                            }
                        }
                        int iNum = 0;
                        try
                        {
                            iNum = int.Parse(sNumEs[0]);
                        }
                        catch { }

                        int iConStar = 0;
                        string sCamposStyle = "style";
                        string sCamposCategoria = "Categoria";
                        string sEstrella = "stars";
                        string sEstrellaMedia = "stars2";
                        string sSinEstrella = "starsNo";
                        DataTable tblEstrellas = new DataTable("estrellas");
                        DataColumn dcStyle = new DataColumn(sCamposStyle);
                        DataColumn dcCategory = new DataColumn(sCamposCategoria);
                        tblEstrellas.Columns.Add(dcStyle);
                        tblEstrellas.Columns.Add(dcCategory);

                        while (iNum > iConStar)
                        {
                            DataRow drEstrella = tblEstrellas.NewRow();
                            drEstrella[sCamposStyle] = sEstrella;
                            tblEstrellas.Rows.Add(drEstrella);
                            iConStar++;
                        }

                        if (sNumEs.Length > 1)
                        {
                            DataRow drEstrella = tblEstrellas.NewRow();
                            drEstrella[sCamposStyle] = sEstrellaMedia;
                            tblEstrellas.Rows.Add(drEstrella);
                        }
                        if (iNum.Equals(0))
                        {
                            DataRow drEstrella = tblEstrellas.NewRow();
                            drEstrella[sCamposStyle] = sSinEstrella;
                            drEstrella[sCamposCategoria] = sEstrellasTexto;

                            tblEstrellas.Rows.Add(drEstrella);
                        }
                        RptEstrellas.DataSource = tblEstrellas;
                        RptEstrellas.DataBind();
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        private void setOcupacion(Repeater rptOcupacion, DataTable dtOcupacion, DataRow[] drRoom, DataTable dtOcupancy)
        {
            DataTable tblTypeRoom = new DataTable(TABLA_ROOM_TYPE);
            DataColumn dcHotelId = new DataColumn(COLUMN_HOTEL_INFO_ID);
            DataColumn dcHotelRoomId = new DataColumn(COLUMN_HOTEL_ROOM_ID);
            DataColumn dcRoomTypeCode = new DataColumn(COLUMN_CODE);
            DataColumn dcRoomType = new DataColumn(COLUMN_ROOM_TYPE_TEXT);
            DataColumn dcCurrency = new DataColumn(COLUMN_CURRENCY_CODE);
            DataColumn dcCurrencyTest = new DataColumn(COLUMN_CURRENCY_TEST);
            DataColumn dcPrice = new DataColumn(COLUMN_AMOUNT_TEXT);
            DataColumn dcCharacteristic = new DataColumn(COLUMN_CHARASTERISTIC);
            DataColumn iOfertaPromosImg = new DataColumn(COLUMN_iOfertaPromosImg);
            DataColumn iOfertaPromosText = new DataColumn(COLUMN_iOfertaPromosText);

            try
            {
                tblTypeRoom.Columns.Add("blanco", typeof(string)).DefaultValue = string.Empty;
            }
            catch (Exception) { }
            tblTypeRoom.Columns.Add(dcHotelId);
            tblTypeRoom.Columns.Add(dcHotelRoomId);
            tblTypeRoom.Columns.Add(dcRoomTypeCode);
            tblTypeRoom.Columns.Add(dcRoomType);
            tblTypeRoom.Columns.Add(dcCurrency);
            tblTypeRoom.Columns.Add(dcCurrencyTest);
            tblTypeRoom.Columns.Add(dcPrice);
            tblTypeRoom.Columns.Add(dcCharacteristic);
            tblTypeRoom.Columns.Add(iOfertaPromosImg);
            tblTypeRoom.Columns.Add(iOfertaPromosText);

            try
            {
                cDisenio.setRepeater(dtOcupacion, rptOcupacion);
                for (int i = 0; i < rptOcupacion.Items.Count; i++)
                {
                    Repeater rptPrecios = rptOcupacion.Items[i].FindControl("rptPrecios") as Repeater;
                    Repeater rptTiposHabitacion = (Repeater)rptOcupacion.Items[i].FindControl("rptTiposHabitacion");
                    RadioButtonList rblOcupacion = rptOcupacion.Items[i].FindControl("rblOcupacion") as RadioButtonList;
                    Label lblidHabitacion = (Label)rptOcupacion.Items[i].FindControl("lblidHabitacion");

                    foreach (DataRow drDisponibilidad in drRoom)
                    {
                        if (drDisponibilidad[COLUMN_HOTEL_ROOM_OCUPATION].ToString() == lblidHabitacion.Text)
                        {
                            DataRow filaTarifa = tblTypeRoom.NewRow();
                            filaTarifa[dcHotelId] = drDisponibilidad[COLUMN_HOTEL_INFO_ID];
                            filaTarifa[dcHotelRoomId] = drDisponibilidad[COLUMN_HOTEL_ROOM_ID];
                            filaTarifa[dcRoomTypeCode] = drDisponibilidad[COLUMN_CODE];
                            filaTarifa[dcRoomType] = drDisponibilidad[COLUMN_ROOM_TYPE_TEXT] + "  " + drDisponibilidad[COLUMN_BOARD_TEXT];
                            filaTarifa[dcCurrency] = drDisponibilidad[COLUMN_CURRENCY_CODE];
                            filaTarifa[dcCurrencyTest] = drDisponibilidad[COLUMN_CURRENCY_TEST];
                            filaTarifa[dcPrice] = drDisponibilidad[COLUMN_AMOUNT_TEXT];
                            filaTarifa[dcCharacteristic] = drDisponibilidad[COLUMN_CHARASTERISTIC];
                            filaTarifa[iOfertaPromosImg] = drDisponibilidad[COLUMN_OfertaNewOcupancy];
                            filaTarifa[iOfertaPromosText] = drDisponibilidad[COLUMN_Classification_Text];

                            tblTypeRoom.Rows.Add(filaTarifa);
                        }
                    }
                    cDisenio.setRepeater(tblTypeRoom, rptTiposHabitacion);

                    if (rblOcupacion != null)
                    {
                        rblOcupacion.DataSource = tblTypeRoom;
                        rblOcupacion.DataValueField = "HotelRoom_Id";
                        rblOcupacion.DataTextField = "blanco";
                        rblOcupacion.DataBind();

                    }

                    setTiposHabitacion(rptTiposHabitacion, drRoom, tblTypeRoom, dtOcupancy);
                    tblTypeRoom.Clear();
                }
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        public void setAcomodacion(int iGuest, int iCama, Repeater RptGuest, Repeater RptCama)
        {
            try
            {
                string sStyleGuest = "styleGuest";
                string sStyleCama = "styleCama";

                DataTable tblGuest = new DataTable("Guest");
                DataColumn dcStyleGuest = new DataColumn(sStyleGuest);
                tblGuest.Columns.Add(dcStyleGuest);

                DataTable tblCama = new DataTable("Cama");
                DataColumn dcStyleCama = new DataColumn(sStyleCama);
                tblCama.Columns.Add(dcStyleCama);

                for (int i = 0; i < iGuest; i++)
                {
                    DataRow filaGuest = tblGuest.NewRow();
                    filaGuest[sStyleGuest] = sStyleGuest;
                    tblGuest.Rows.Add(filaGuest);
                }

                for (int c = 0; c < iCama; c++)
                {
                    DataRow filaCama = tblCama.NewRow();
                    filaCama[sStyleCama] = sStyleCama;
                    tblCama.Rows.Add(filaCama);
                }

                RptGuest.DataSource = tblGuest;
                RptGuest.DataBind();

                RptCama.DataSource = tblCama;
                RptCama.DataBind();
            }
            catch
            {
            }
        }
        public void setTiposHabitacion(Repeater rptTiposHabitacion, DataRow[] drRoom, DataTable dtTiposHabitacion, DataTable dtOcupancy)
        {
            try
            {

                string sBoard = clsValidaciones.GetKeyOrAdd("SinAlimentacion", "SH");
                for (int i = 0; i < rptTiposHabitacion.Items.Count; i++)
                {
                    Repeater rptDesayuno = (Repeater)rptTiposHabitacion.Items[i].FindControl("rptDesayuno");
                    string hotelRoomId = dtTiposHabitacion.Rows[i][COLUMN_HOTEL_ROOM_ID].ToString();
                    foreach (DataRow drDesayuno in drRoom)
                    {
                        if (drDesayuno[COLUMN_HOTEL_ROOM_ID].ToString().Equals(hotelRoomId))
                        {
                            if (drDesayuno[COLUMN_BOARD_SHORT_NAME].ToString() != sBoard)
                            {
                                setDesayuno(rptDesayuno, drDesayuno);
                            }
                        }
                    }

                }
                try
                {
                    for (int i = 0; i < rptTiposHabitacion.Items.Count; i++)
                    {
                        DataRow[] drOcupancy = dtOcupancy.Select(COLUMN_HOTEL_ROOM_ID + "=" + dtTiposHabitacion.Rows[i][COLUMN_HOTEL_ROOM_ID].ToString());
                        int iGues = int.Parse(drOcupancy[0][COLUMN_OCUPANCY_GUEST].ToString());
                        int iCama = int.Parse(drOcupancy[0][COLUMN_OCUPANCY_BEDS].ToString());

                        Repeater RptGuest = (Repeater)rptTiposHabitacion.Items[i].FindControl("RptGuest");
                        Repeater RptCama = (Repeater)rptTiposHabitacion.Items[i].FindControl("RptCama");
                        setAcomodacion(iGues, iCama, RptGuest, RptCama);
                    }
                }
                catch { }

            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        private void setDesayuno(Repeater rptDesayuno, DataRow drDesayuno)
        {
            try
            {
                DataTable tblDesayuno = new DataTable(TABLA_BOARD);
                DataColumn dcHotelId = new DataColumn(COLUMN_HOTEL_INFO_ID);
                DataColumn dcCode = new DataColumn(COLUMN_BOARD_CODE);
                DataColumn dcDesc = new DataColumn(COLUMN_BOARD_TEXT);
                DataColumn dcHotelRoomId = new DataColumn("HotelRoom_Id");

                tblDesayuno.Columns.Add(dcHotelId);
                tblDesayuno.Columns.Add(dcCode);
                tblDesayuno.Columns.Add(dcDesc);
                tblDesayuno.Columns.Add(dcHotelRoomId);

                DataRow filaTarifa = tblDesayuno.NewRow();
                filaTarifa[dcHotelId] = drDesayuno[COLUMN_HOTEL_INFO_ID];
                filaTarifa[dcCode] = drDesayuno[COLUMN_BOARD_CODE];
                filaTarifa[dcDesc] = drDesayuno[COLUMN_BOARD_TEXT];
                filaTarifa[dcHotelRoomId] = drDesayuno[dcHotelRoomId.ColumnName];

                tblDesayuno.Rows.Add(filaTarifa);

                cDisenio.setRepeater(tblDesayuno, rptDesayuno);
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        public void setRoomType(DataSet dsResultados)
        {
            try
            {
                // Traemos la tabla
                DataTable dtHotelRoom = dsResultados.Tables[TABLA_HOTEL_ROOM];
                string sImagenPath = clsValidaciones.RutaImagesGen();
                //Utils.Utils cUtil = new Utils.Utils();

                // Adicionamos las columnas
                dtHotelRoom.Columns.Add(COLUMN_ROOM_TYPE_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_TYPE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CHARASTERISTIC, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CURRENCY_TEST, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_AMOUNT, typeof(decimal));
                dtHotelRoom.Columns.Add(COLUMN_AMOUNT_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_SERVICE_HOTEL_ID, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_BOARD_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_BOARD_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_BOARD_SHORT_NAME, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_HOTEL_INFO_ID, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_AVAIL_TOKEN, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_INCOMING_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CONTRACT_NAME, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_FROM, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_TO, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_FROM_FORMAT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_TO_FORMAT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_HOTEL_INFO_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DESTINATION_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_URL, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_ADULT_COUNT, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_CHILD_COUNT, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_HOTEL_OCCUPANCY_ID, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_ROOM_COUNT, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_HOTEL_ROOM_OCUPATION, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_WS_SELECT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CLASIFICATION_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CLASIFICATION_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_OfertaNewOcupancy, typeof(string));
                string sWsSelect = clsValidaciones.GetKeyOrAdd("WS_HOTEL_HB", "HOTBED");

                List<VO_Passenger> lvo_Passenger = clsSesiones.getPassenger();

                //Image iOfertaPromos = new Image();

                dsResultados.Tables[TABLA_ROOM_TYPE].Columns["type"].ColumnName = "iOfertaPromosImg";

                // almacenamos datos
                foreach (DataRow drHotelRoom in dtHotelRoom.Rows)
                {
                    foreach (DataRow drRoomType in drHotelRoom.GetChildRows(RELACION_HOTELROOM_ROOMTYPE))
                    {
                        drHotelRoom[COLUMN_ROOM_TYPE_TEXT] = drRoomType[COLUMN_ROOM_TYPE_TEXT];
                        drHotelRoom[COLUMN_TYPE] = drRoomType[COLUMN_iOfertaPromosImg];
                        drHotelRoom[COLUMN_CODE] = drRoomType[COLUMN_CODE];
                        drHotelRoom[COLUMN_CHARASTERISTIC] = drRoomType[COLUMN_CHARASTERISTIC];

                        foreach (DataRow drAvailableRoom in drHotelRoom.GetParentRows(RELACION_AVAILABLEROOM_HOTELROOM))
                        {
                            foreach (DataRow drServiceHotel in drAvailableRoom.GetParentRows(RELACION_SERVICEHOTEL_AVAILABLEROOM))
                            {
                                foreach (DataRow drHotelInfo in drServiceHotel.GetChildRows(RELACION_SERVICEHOTEL_HOTELINFO))
                                {
                                    drHotelRoom[COLUMN_CLASIFICATION_CODE] = drHotelInfo[COLUMN_CLASIFICATION_CODE];
                                    drHotelRoom[COLUMN_CLASIFICATION_TEXT] = drHotelInfo[COLUMN_CLASIFICATION_TEXT];
                                    try
                                    {
                                        if (drHotelRoom[COLUMN_CLASIFICATION_CODE].Equals("NOR"))
                                        {
                                            drHotelRoom[COLUMN_OfertaNewOcupancy] = sImagenPath + "spacer.gif";
                                            drRoomType[COLUMN_iOfertaPromosImg] = drHotelRoom[COLUMN_OfertaNewOcupancy];
                                        }
                                        else if (drHotelRoom[COLUMN_CLASIFICATION_CODE].Equals("SPE"))
                                        {
                                            drHotelRoom[COLUMN_OfertaNewOcupancy] = sImagenPath + "oferta.png";
                                            drRoomType[COLUMN_iOfertaPromosImg] = drHotelRoom[COLUMN_OfertaNewOcupancy];
                                        }
                                        else if (drHotelRoom[COLUMN_CLASIFICATION_CODE].Equals("NRF"))
                                        {
                                            drHotelRoom[COLUMN_OfertaNewOcupancy] = sImagenPath + "nfr.png";
                                            drRoomType[COLUMN_iOfertaPromosImg] = drHotelRoom[COLUMN_OfertaNewOcupancy];
                                        }
                                        else
                                        {
                                            drHotelRoom[COLUMN_OfertaNewOcupancy] = sImagenPath + "extra.png";
                                            drRoomType[COLUMN_iOfertaPromosImg] = drHotelRoom[COLUMN_OfertaNewOcupancy];
                                        }
                                    }
                                    catch
                                    {
                                        drHotelRoom[COLUMN_OfertaNewOcupancy] = sImagenPath + "spacer.gif";
                                        drRoomType[COLUMN_iOfertaPromosImg] = drHotelRoom[COLUMN_OfertaNewOcupancy];
                                    }
                                }
                            }
                        }

                    }
                    foreach (DataRow drPrice in drHotelRoom.GetChildRows(RELACION_HOTELROOM_PRICE))
                    {
                        decimal dPrice = Convert.ToDecimal(clsValidaciones.getDecimalRound(drPrice[COLUMN_AMOUNT].ToString()));
                        drHotelRoom[COLUMN_AMOUNT] = clsValidaciones.getDecimalRound(Convert.ToString(dPrice / decimal.Parse(clsValidaciones.GetKeyOrAdd("Incremento"))));
                        drHotelRoom[COLUMN_AMOUNT_TEXT] = Convert.ToDecimal(drHotelRoom[COLUMN_AMOUNT].ToString()).ToString(FORMATO_NUMEROS_SD);
                    }
                    foreach (DataRow drBoard in drHotelRoom.GetChildRows(RELACION_HOTELROOM_BOARD))
                    {
                        drHotelRoom[COLUMN_BOARD_TEXT] = drBoard[COLUMN_BOARD_TEXT];
                        drHotelRoom[COLUMN_BOARD_CODE] = drBoard[COLUMN_CODE];
                        drHotelRoom[COLUMN_BOARD_SHORT_NAME] = drBoard[COLUMN_SHORT_NAME];
                    }
                    foreach (DataRow drAvailableRoom in drHotelRoom.GetParentRows(RELACION_AVAILABLEROOM_HOTELROOM))
                    {
                        foreach (DataRow drServiceHotel in drAvailableRoom.GetParentRows(RELACION_SERVICEHOTEL_AVAILABLEROOM))
                        {
                            foreach (DataRow drHotelInfo in drServiceHotel.GetChildRows(RELACION_SERVICEHOTEL_HOTELINFO))
                            {
                                drHotelRoom[COLUMN_HOTEL_INFO_ID] = drHotelInfo[COLUMN_HOTEL_INFO_ID];
                                drHotelRoom[COLUMN_DATE_FROM] = drHotelInfo[COLUMN_DATE_FROM];
                                drHotelRoom[COLUMN_DATE_TO] = drHotelInfo[COLUMN_DATE_TO];
                                drHotelRoom[COLUMN_HOTEL_INFO_CODE] = drHotelInfo[COLUMN_CODE];
                                drHotelRoom[COLUMN_DATE_FROM_FORMAT] = clsValidaciones.ConverYMDtoYMD(drHotelInfo[COLUMN_DATE_FROM].ToString());
                                drHotelRoom[COLUMN_DATE_TO_FORMAT] = clsValidaciones.ConverYMDtoYMD(drHotelInfo[COLUMN_DATE_TO].ToString());
                            }
                            foreach (DataRow drCurrency in drServiceHotel.GetChildRows(RELACION_SERVICEHOTEL_CURRENCY))
                            {
                                drHotelRoom[COLUMN_CURRENCY_CODE] = drCurrency[COLUMN_CODE];
                                drHotelRoom[COLUMN_CURRENCY_TEST] = drCurrency[COLUMN_CURRENCY_TEST];
                            }
                            foreach (DataRow drContractList in drServiceHotel.GetChildRows(RELACION_SERVICEHOTEL_CONTRACTLIST))
                            {
                                foreach (DataRow drContract in drContractList.GetChildRows(RELACION_CONTRACTLIST_CONTRACT))
                                {
                                    foreach (DataRow drIncomingOffice in drContract.GetChildRows(RELACION_CONTRACT_INCOMINGOFFICE))
                                    {
                                        drHotelRoom[COLUMN_INCOMING_CODE] = drIncomingOffice[COLUMN_CODE];
                                    }
                                    drHotelRoom[COLUMN_CONTRACT_NAME] = drContract[COLUMN_NAME];
                                }
                            }
                            drHotelRoom[COLUMN_SERVICE_HOTEL_ID] = drServiceHotel[COLUMN_SERVICE_HOTEL_ID];
                            drHotelRoom[COLUMN_AVAIL_TOKEN] = drServiceHotel[COLUMN_AVAIL_TOKEN];
                        }
                        foreach (DataRow drHotelOcupancy in drAvailableRoom.GetChildRows(RELACION_AVAILABLEROOM_HOTELOCUPANCY))
                        {
                            foreach (DataRow drOcupancy in drHotelOcupancy.GetChildRows(RELACION_HOTELOCUPANCY_OCUPANCY))
                            {
                                drHotelRoom[COLUMN_ADULT_COUNT] = drOcupancy[COLUMN_ADULT_COUNT];
                                drHotelRoom[COLUMN_CHILD_COUNT] = drOcupancy[COLUMN_CHILD_COUNT];
                            }
                            drHotelRoom[COLUMN_HOTEL_OCCUPANCY_ID] = drHotelOcupancy[COLUMN_HOTEL_OCCUPANCY_ID];
                            drHotelRoom[COLUMN_ROOM_COUNT] = drHotelOcupancy[COLUMN_ROOM_COUNT];
                        }
                    }
                    drHotelRoom[COLUMN_URL] = drHotelRoom[COLUMN_SHRUI].ToString() + "|" + drHotelRoom[COLUMN_BOARD_CODE].ToString() + "|" + drHotelRoom[COLUMN_CODE].ToString() + "|" + drHotelRoom[COLUMN_CHARASTERISTIC].ToString() + "|";
                    drHotelRoom[COLUMN_WS_SELECT] = sWsSelect;
                }
                try
                {
                    guardarHabitacionesSeleccion(dtHotelRoom, lvo_Passenger);
                }
                catch
                {
                    guardarHabitacionesSeleccionDif(dtHotelRoom, lvo_Passenger);
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
            }
        }
        public void setOcupancy(DataSet dsResultados)
        {
            try
            {
                // Traemos la tabla
                DataTable dtHotelOcupancy = dsResultados.Tables[TABLA_HOTEL_OCCUPANCY];

                // Adicionamos las columnas
                dtHotelOcupancy.Columns.Add(COLUMN_HOTEL_ROOM_ID, typeof(int));
                dtHotelOcupancy.Columns.Add(COLUMN_SERVICE_HOTEL_ID, typeof(int));
                dtHotelOcupancy.Columns.Add(COLUMN_HOTEL_INFO_ID, typeof(int));
                dtHotelOcupancy.Columns.Add(COLUMN_ADULT_COUNT, typeof(int));
                dtHotelOcupancy.Columns.Add(COLUMN_CHILD_COUNT, typeof(int));

                // almacenamos datos
                foreach (DataRow drHotelOcupancy in dtHotelOcupancy.Rows)
                {
                    foreach (DataRow drOcupancy in drHotelOcupancy.GetChildRows(RELACION_HOTELOCUPANCY_OCUPANCY))
                    {
                        drHotelOcupancy[COLUMN_ADULT_COUNT] = drOcupancy[COLUMN_ADULT_COUNT];
                        drHotelOcupancy[COLUMN_CHILD_COUNT] = drOcupancy[COLUMN_CHILD_COUNT];
                    }
                    foreach (DataRow drAvailableRoom in drHotelOcupancy.GetParentRows(RELACION_AVAILABLEROOM_HOTELOCUPANCY))
                    {
                        foreach (DataRow drServiceHotel in drAvailableRoom.GetParentRows(RELACION_SERVICEHOTEL_AVAILABLEROOM))
                        {
                            foreach (DataRow drHotelInfo in drServiceHotel.GetChildRows(RELACION_SERVICEHOTEL_HOTELINFO))
                            {
                                drHotelOcupancy[COLUMN_HOTEL_INFO_ID] = drHotelInfo[COLUMN_HOTEL_INFO_ID];
                                drHotelOcupancy[COLUMN_SERVICE_HOTEL_ID] = drServiceHotel[COLUMN_SERVICE_HOTEL_ID];
                                drHotelOcupancy[COLUMN_AVAILABLE_ROOM_ID] = drAvailableRoom[COLUMN_AVAILABLE_ROOM_ID];
                            }
                        }
                        foreach (DataRow drHotelRoom in drAvailableRoom.GetChildRows(RELACION_AVAILABLEROOM_HOTELROOM))
                        {
                            drHotelOcupancy[COLUMN_HOTEL_ROOM_ID] = drHotelRoom[COLUMN_HOTEL_ROOM_ID];
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
            }
        }
        public void setHotel(DataSet dsResultados)
        {

            string sURL = string.Empty;
            string sImagenPath = clsValidaciones.RutaImagesGen();
            try
            {
                // Traemos la tabla
                DataTable dtHotelInfo = dsResultados.Tables[TABLA_HOTEL_INFO];

                // Adicionamos las columnas
                dtHotelInfo.Columns.Add(COLUMN_CATEGORY_CODE, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_CATEGORY_TEXT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DESTINATION_CODE, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DESTINATION_NAME, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_ZONE_TEXT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DATE_FROM, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DATE_FROM_FORMAT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DATE_TO, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DATE_TO_FORMAT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DESCRIPTION, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DESCRIPTION_LONG, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_ADDRESS, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_IMAGEN_URL, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_WS_SELECT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_AGENCY_COMISION, typeof(decimal));
                dtHotelInfo.Columns.Add(COLUMN_OBSERVACIONES, typeof(string));
                string sWsSelect = clsValidaciones.GetKeyOrAdd("WS_HOTEL_HB", "HOTBED");

                // almacenamos datos
                foreach (DataRow drHotelInfo in dtHotelInfo.Rows)
                {
                    foreach (DataRow drCategory in drHotelInfo.GetChildRows(RELACION_HOTELINFO_CATEGORY))
                    {
                        drHotelInfo[COLUMN_CATEGORY_CODE] = drCategory[COLUMN_CODE];
                        drHotelInfo[COLUMN_CATEGORY_TEXT] = drCategory[COLUMN_CATEGORY_TEXT];
                    }
                    foreach (DataRow drDestination in drHotelInfo.GetChildRows(RELACION_HOTELINFO_DESTINATION))
                    {
                        foreach (DataRow drZoneList in drDestination.GetChildRows(RELACION_DESTINATION_ZONELIST))
                        {
                            foreach (DataRow drZone in drZoneList.GetChildRows(RELACION_ZONELIST_ZONE))
                            {
                                drHotelInfo[COLUMN_ZONE_TEXT] = drZone[COLUMN_ZONE_TEXT];
                                drHotelInfo[COLUMN_DESTINATION_CODE] = drDestination[COLUMN_CODE];
                                drHotelInfo[COLUMN_DESTINATION_NAME] = drDestination[COLUMN_NAME];
                                break;
                            }
                        }
                    }
                    foreach (DataRow drServiceHotel in drHotelInfo.GetParentRows(RELACION_SERVICE_HOTELINFO))
                    {
                        foreach (DataRow drdateFrom in drServiceHotel.GetChildRows(RELACION_SERVICE_DATEFROM))
                        {
                            drHotelInfo[COLUMN_DATE_FROM] = drdateFrom[COLUMN_DATE];
                            drHotelInfo[COLUMN_DATE_FROM_FORMAT] = clsValidaciones.ConverYMDtoYMD(drdateFrom[COLUMN_DATE].ToString());
                        }
                        foreach (DataRow drdateTo in drServiceHotel.GetChildRows(RELACION_SERVICE_DATETO))
                        {
                            drHotelInfo[COLUMN_DATE_TO] = drdateTo[COLUMN_DATE];
                            drHotelInfo[COLUMN_DATE_TO_FORMAT] = clsValidaciones.ConverYMDtoYMD(drdateTo[COLUMN_DATE].ToString());
                        }
                    }
                    drHotelInfo[COLUMN_DESCRIPTION] = new csConsultasHoteles().sDescription(drHotelInfo[COLUMN_CODE].ToString(), "CAS");
                    drHotelInfo[COLUMN_DESCRIPTION_LONG] = drHotelInfo[COLUMN_DESCRIPTION];
                    drHotelInfo[COLUMN_IMAGEN_URL] = sImagenPath + "spacer.gif";
                    drHotelInfo[COLUMN_WS_SELECT] = sWsSelect;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                cMensaje.Metodo = "setHotel";
                ExceptionHandled.Publicar(cMensaje);
            }
        }
        public void setHotelRoom(DataSet dsResultados)
        {
            try
            {
                // Traemos la tabla
                DataTable dtHotelRoom = dsResultados.Tables[TABLA_HOTEL_ROOM];


                // Adicionamos las columnas
                dtHotelRoom.Columns.Add(COLUMN_ROOM_TYPE_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_TYPE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CHARASTERISTIC, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_AMOUNT, typeof(decimal));
                dtHotelRoom.Columns.Add(COLUMN_AMOUNT_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_SERVICE_ID, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_PURCHASE_STATUS, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_PURCHASE_ID, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_PURCHASE_TOKEN, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_LANGUAJE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_TOTAL_PRICE, typeof(decimal));
                dtHotelRoom.Columns.Add(COLUMN_TOTAL_PRICE_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_AGENCY_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_BRANCH, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_SERVICE_STATUS, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_TOTAL_AMOUNT, typeof(decimal));
                dtHotelRoom.Columns.Add(COLUMN_TOTAL_AMOUNT_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_SPUI, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_BOARD_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_BOARD_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_HOTEL_INFO_ID, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_NAME, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_INCOMING_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CONTRACT_NAME, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CONTRACT_ID, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_HOTEL_INFO_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CURRENCY_TEST, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_COMMENT_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_POLITICA, typeof(string));

                dtHotelRoom.Columns.Add(COLUMN_DATE_FROM, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_FROM_FORMAT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_TO, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_TO_FORMAT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_ROOM_COUNT, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_WS_SELECT, typeof(string));
                string sWsSelect = clsValidaciones.GetKeyOrAdd("WS_HOTEL_HB", "HOTBED");

                DateTime dtFechaPolitica = DateTime.Now.AddDays(1);
                string sFechaPolitica = clsValidaciones.ConverYMDtoDMMY(dtFechaPolitica.ToString(FORMATO_FECHA), "/");
                // almacenamos datos
                foreach (DataRow drHotelRoom in dtHotelRoom.Rows)
                {
                    foreach (DataRow drRoomType in drHotelRoom.GetChildRows(RELACION_HOTELROOM_ROOMTYPE))
                    {
                        drHotelRoom[COLUMN_ROOM_TYPE_TEXT] = drRoomType[COLUMN_ROOM_TYPE_TEXT];
                        drHotelRoom[COLUMN_TYPE] = drRoomType[COLUMN_TYPE];
                        drHotelRoom[COLUMN_CODE] = drRoomType[COLUMN_CODE];
                        drHotelRoom[COLUMN_CHARASTERISTIC] = drRoomType[COLUMN_CHARASTERISTIC];
                    }
                    foreach (DataRow drPrice in drHotelRoom.GetChildRows(RELACION_HOTELROOM_PRICE))
                    {
                        decimal dPrice = Convert.ToDecimal(clsValidaciones.getDecimalRound(drPrice[COLUMN_AMOUNT].ToString()));
                        drHotelRoom[COLUMN_AMOUNT] = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(dPrice / decimal.Parse(clsValidaciones.GetKeyOrAdd("Incremento")))));
                        drHotelRoom[COLUMN_AMOUNT_TEXT] = Convert.ToDecimal(drHotelRoom[COLUMN_AMOUNT].ToString()).ToString(FORMATO_NUMEROS_SD);//dPriceCP.ToString(FORMATO_NUMEROS_SD);
                    }
                    foreach (DataRow drBoard in drHotelRoom.GetChildRows(RELACION_HOTELROOM_BOARD))
                    {
                        drHotelRoom[COLUMN_BOARD_TEXT] = drBoard[COLUMN_BOARD_TEXT];
                        drHotelRoom[COLUMN_BOARD_CODE] = drBoard[COLUMN_CODE];
                    }

                    foreach (DataRow drAvailableRoom in drHotelRoom.GetParentRows(RELACION_AVAILABLEROOM_HOTELROOM))
                    {
                        foreach (DataRow drHotelOcupancy in drAvailableRoom.GetChildRows(RELACION_AVAILABLEROOM_HOTELOCUPANCY))
                        {
                            drHotelRoom[COLUMN_ROOM_COUNT] = drHotelOcupancy[COLUMN_ROOM_COUNT];
                        }
                        foreach (DataRow drService in drAvailableRoom.GetParentRows(RELACION_SERVICE_AVAILABLEROOM))
                        {
                            string sFecha = string.Empty;

                            foreach (DataRow drDateFrom in drService.GetChildRows(RELACION_SERVICE_DATEFROM))
                            {
                                drHotelRoom[COLUMN_DATE_FROM] = drDateFrom[COLUMN_DATE];
                                sFecha = clsValidaciones.ConverYMDtoYMD(drDateFrom[COLUMN_DATE].ToString());
                                drHotelRoom[COLUMN_DATE_FROM_FORMAT] = sFecha;
                            }
                            foreach (DataRow drDateTo in drService.GetChildRows(RELACION_SERVICE_DATETO))
                            {
                                drHotelRoom[COLUMN_DATE_TO] = drDateTo[COLUMN_DATE];
                                sFecha = clsValidaciones.ConverYMDtoYMD(drDateTo[COLUMN_DATE].ToString());
                                drHotelRoom[COLUMN_DATE_TO_FORMAT] = sFecha;
                            }
                            foreach (DataRow drHotelInfo in drService.GetChildRows(RELACION_SERVICE_HOTELINFO))
                            {
                                drHotelRoom[COLUMN_HOTEL_INFO_CODE] = drHotelInfo[COLUMN_CODE];
                                drHotelRoom[COLUMN_HOTEL_INFO_ID] = drHotelInfo[COLUMN_HOTEL_INFO_ID];
                            }
                            foreach (DataRow drCurrency in drService.GetChildRows(RELACION_SERVICE_CURRENCY))
                            {
                                drHotelRoom[COLUMN_CURRENCY_CODE] = drCurrency[COLUMN_CODE];
                                drHotelRoom[COLUMN_CURRENCY_TEST] = drCurrency[COLUMN_CURRENCY_TEST];
                            }
                            foreach (DataRow drContractList in drService.GetChildRows(RELACION_SERVICE_CONTRACTLIST))
                            {
                                foreach (DataRow drContract in drContractList.GetChildRows(RELACION_CONTRACTLIST_CONTRACT))
                                {
                                    foreach (DataRow drIncomingOffice in drContract.GetChildRows(RELACION_CONTRACT_INCOMINGOFFICE))
                                    {
                                        drHotelRoom[COLUMN_INCOMING_CODE] = drIncomingOffice[COLUMN_CODE];
                                    }
                                    drHotelRoom[COLUMN_CONTRACT_NAME] = drContract[COLUMN_NAME];
                                    drHotelRoom[COLUMN_CONTRACT_ID] = drContract[COLUMN_CONTRACT_ID];

                                    try
                                    {
                                        foreach (DataRow drCommentList in drContract.GetChildRows(RELACION_CONTRACT_COMMENTLIST))
                                        {
                                            foreach (DataRow drComment in drCommentList.GetChildRows(RELACION_COMMENTLIST_COMMENT))
                                            {
                                                drHotelRoom[COLUMN_COMMENT_TEXT] = drComment[COLUMN_COMMENT_TEXT];
                                            }
                                        }
                                    }
                                    catch { }
                                }
                            }
                            foreach (DataRow drServiceList in drService.GetParentRows(RELACION_SERVICELIST_SERVICE))
                            {
                                foreach (DataRow drPurchase in drServiceList.GetParentRows(RELACION_PURCHASE_SERVICELIST))
                                {
                                    drHotelRoom[COLUMN_PURCHASE_STATUS] = drPurchase[COLUMN_STATUS];
                                    drHotelRoom[COLUMN_PURCHASE_ID] = drPurchase[COLUMN_PURCHASE_ID];
                                    drHotelRoom[COLUMN_LANGUAJE] = drPurchase[COLUMN_LANGUAJE];
                                    decimal dPriceTP = Convert.ToDecimal(clsValidaciones.getDecimalRound(drPurchase[COLUMN_TOTAL_PRICE].ToString()));
                                    drHotelRoom[COLUMN_TOTAL_PRICE] = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(dPriceTP / decimal.Parse(clsValidaciones.GetKeyOrAdd("Incremento")))));
                                    drHotelRoom[COLUMN_TOTAL_PRICE_TEXT] = Convert.ToDecimal(drHotelRoom[COLUMN_TOTAL_PRICE].ToString()).ToString(FORMATO_NUMEROS_SD);//dPriceTP.ToString(FORMATO_NUMEROS_SD);
                                    drHotelRoom[COLUMN_PURCHASE_TOKEN] = drPurchase[COLUMN_PURCHASE_TOKEN];
                                    foreach (DataRow drAgency in drPurchase.GetChildRows(RELACION_PURCHASE_AGENCY))
                                    {
                                        drHotelRoom[COLUMN_AGENCY_CODE] = drAgency[COLUMN_CODE];
                                        drHotelRoom[COLUMN_BRANCH] = drAgency[COLUMN_BRANCH];
                                    }
                                }
                            }
                            drHotelRoom[COLUMN_SERVICE_ID] = drService[COLUMN_SERVICE_ID];
                            drHotelRoom[COLUMN_SERVICE_STATUS] = drService[COLUMN_STATUS];
                            drHotelRoom[COLUMN_SPUI] = drService[COLUMN_SPUI];
                            decimal dPriceT = Convert.ToDecimal(clsValidaciones.getDecimalRound(drService[COLUMN_TOTAL_AMOUNT].ToString()));
                            drHotelRoom[COLUMN_TOTAL_AMOUNT] = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(dPriceT / decimal.Parse(clsValidaciones.GetKeyOrAdd("Incremento")))));
                            drHotelRoom[COLUMN_TOTAL_AMOUNT_TEXT] = Convert.ToDecimal(drHotelRoom[COLUMN_TOTAL_AMOUNT].ToString()).ToString(FORMATO_NUMEROS_SD);//dPriceT.ToString(FORMATO_NUMEROS_SD);
                        }
                    }
                    drHotelRoom[COLUMN_POLITICA] = sFechaPolitica;
                    drHotelRoom[COLUMN_WS_SELECT] = sWsSelect;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
            }
        }
        public void setPenalRoom(DataSet dsResultados)
        {
            try
            {
                // Traemos la tabla
                DataTable dtCancelationPolicy = dsResultados.Tables[TABLA_CANCELATION_POLICY];

                int iDiasCancel = -3;
                try { iDiasCancel = int.Parse(clsValidaciones.GetKeyOrAdd("DiasPenalizacion", "3")) * -1; }
                catch { }
                // Adicionamos las columnas
                dtCancelationPolicy.Columns.Add(COLUMN_CURRENCY_TEST, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_DATE_TIME_FROM, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_DATE_TIME, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_DATE_TIME_TO, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_DATE_TIME_FROM_FORMAT, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_DATE_TIME_TO_FORMAT, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_CANCELATION_TOTAL_DAYS, typeof(int));
                dtCancelationPolicy.Columns.Add(COLUMN_CANCELATION_AMOUNT_DAY, typeof(decimal));
                dtCancelationPolicy.Columns.Add(COLUMN_CANCELATION_AMOUNT_DAY_TEXT, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_CANCELATION_TOTAL_AMOUNT, typeof(decimal));
                dtCancelationPolicy.Columns.Add(COLUMN_CANCELATION_TOTAL_AMOUNT_TEXT, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_CANCELATION_PRICE_ID, typeof(int));
                // almacenamos datos
                foreach (DataRow drCancelationPolicy in dtCancelationPolicy.Rows)
                {
                    foreach (DataRow drPriceCancelation in drCancelationPolicy.GetChildRows(RELACION_CANCELATIONPOLICY_PRICE))
                    {
                        string sFechaIni = string.Empty;
                        string sFechaFin = string.Empty;
                        drCancelationPolicy[COLUMN_CANCELATION_PRICE_ID] = drPriceCancelation[COLUMN_PRICE_ID];
                        decimal dPriceCP = Convert.ToDecimal(clsValidaciones.getDecimalRound(drPriceCancelation[COLUMN_AMOUNT].ToString()));
                        drCancelationPolicy[COLUMN_CANCELATION_AMOUNT_DAY] = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(dPriceCP / Convert.ToDecimal(clsValidaciones.GetKeyOrAdd("incremento")))));
                        drCancelationPolicy[COLUMN_CANCELATION_AMOUNT_DAY_TEXT] = Convert.ToDecimal(drCancelationPolicy[COLUMN_CANCELATION_AMOUNT_DAY].ToString()).ToString(FORMATO_NUMEROS_SD);//dPriceCP.ToString(FORMATO_NUMEROS_SD);
                        decimal dPriceCPTotal = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(dPriceCP / Convert.ToDecimal(clsValidaciones.GetKeyOrAdd("incremento"))))); //dPriceCP;
                        foreach (DataRow drDateTimeFrom in drPriceCancelation.GetChildRows(RELACION_PRICE_DFATETIMEFROM))
                        {
                            drCancelationPolicy[COLUMN_DATE_TIME_FROM] = drDateTimeFrom[COLUMN_DATE];
                            drCancelationPolicy[COLUMN_DATE_TIME] = drDateTimeFrom[COLUMN_DATE_TIME];
                            sFechaIni = clsValidaciones.ConverYMDtoYMD(drDateTimeFrom[COLUMN_DATE].ToString());
                            try
                            {
                                DateTime dtFechaIni = DateTime.Parse(sFechaIni).AddDays(iDiasCancel);
                                sFechaIni = dtFechaIni.ToString(FORMATO_FECHA);
                            }
                            catch { }
                            drCancelationPolicy[COLUMN_DATE_TIME_FROM_FORMAT] = sFechaIni;
                        }
                        foreach (DataRow drDateTimeTo in drPriceCancelation.GetChildRows(RELACION_PRICE_DFATETIMETO))
                        {
                            drCancelationPolicy[COLUMN_DATE_TIME_TO] = drDateTimeTo[COLUMN_DATE];
                            sFechaFin = clsValidaciones.ConverYMDtoYMD(drDateTimeTo[COLUMN_DATE].ToString());
                            try
                            {
                                DateTime dtFechaFin = DateTime.Parse(sFechaFin).AddDays(iDiasCancel);
                                sFechaFin = dtFechaFin.ToString(FORMATO_FECHA);
                            }
                            catch { }
                            drCancelationPolicy[COLUMN_DATE_TIME_TO_FORMAT] = sFechaFin;
                        }
                        int iDias = 1;
                        try
                        {
                            if (!sFechaIni.Length.Equals(0))
                            {
                                iDias = clsValidaciones.CalcularDias(sFechaIni, sFechaFin);

                                dPriceCPTotal = dPriceCP;
                            }
                        }
                        catch { }
                        drCancelationPolicy[COLUMN_CANCELATION_TOTAL_DAYS] = iDias;
                        drCancelationPolicy[COLUMN_CANCELATION_TOTAL_AMOUNT] = dPriceCPTotal;
                        drCancelationPolicy[COLUMN_CANCELATION_TOTAL_AMOUNT_TEXT] = dPriceCPTotal.ToString(FORMATO_NUMEROS_SD);
                    }
                    foreach (DataRow drHotelRoom in drCancelationPolicy.GetParentRows(RELACION_HOTELROOM_CNCELATIONPOLICY))
                    {
                        drCancelationPolicy[COLUMN_CURRENCY_CODE] = drHotelRoom[COLUMN_CURRENCY_CODE];
                        drCancelationPolicy[COLUMN_CURRENCY_TEST] = drHotelRoom[COLUMN_CURRENCY_TEST];
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.Metodo = "setPenalRoom";
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
            }
        }
        public void setAditionalRoom(DataSet dsResultados)
        {
            try
            {
                //// Traemos la tabla
                DataTable dtAditionalCost = dsResultados.Tables[TABLA_ADITIONAL_COST];


                // Adicionamos las columnas
                dtAditionalCost.Columns.Add(COLUMN_CURRENCY_TEST, typeof(string));
                dtAditionalCost.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
                dtAditionalCost.Columns.Add(COLUMN_PRICE_ID, typeof(int));
                dtAditionalCost.Columns.Add(COLUMN_AMOUNT, typeof(decimal));
                dtAditionalCost.Columns.Add(COLUMN_AMOUNT_TEXT, typeof(string));
                // almacenamos datos
                foreach (DataRow drAditionalCost in dtAditionalCost.Rows)
                {
                    foreach (DataRow drPriceAditional in drAditionalCost.GetChildRows(RELACION_ADITIONALCOST_PRICE))
                    {
                        drAditionalCost[COLUMN_PRICE_ID] = drPriceAditional[COLUMN_PRICE_ID];
                        decimal dPriceCP = Convert.ToDecimal(clsValidaciones.getDecimalRound(drPriceAditional[COLUMN_AMOUNT].ToString()));
                        drAditionalCost[COLUMN_AMOUNT] = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(dPriceCP / decimal.Parse(clsValidaciones.GetKeyOrAdd("Incremento")))));
                        drAditionalCost[COLUMN_AMOUNT_TEXT] = Convert.ToDecimal(drAditionalCost[COLUMN_AMOUNT].ToString()).ToString(FORMATO_NUMEROS_SD);
                    }
                    foreach (DataRow drAditionalCostList in drAditionalCost.GetParentRows(RELACION_ADITIONALCOSTLIST_ADITIONALCOST))
                    {
                        foreach (DataRow drServices in drAditionalCostList.GetParentRows(RELACION_SERVICE_ADITIONALCOSTLIST))
                        {
                            foreach (DataRow drCurrency in drServices.GetChildRows(RELACION_SERVICE_CURRENCY))
                            {
                                drAditionalCost[COLUMN_CURRENCY_CODE] = drCurrency[COLUMN_CODE];
                                drAditionalCost[COLUMN_CURRENCY_TEST] = drCurrency[COLUMN_CURRENCY_TEST];
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.Metodo = "setAditionalRoom";
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
            }
        }
        public void setTablaHabitaciones(DataSet dsResultados)
        {
            try
            {
                bool bEntra = true;
                try
                {
                    if (dsResultados.Tables[TABLA_HABITACIONES].Rows.Count > 0)
                    {
                        bEntra = false;
                    }
                }
                catch { }
                //CREAMOS LA TABLA HABITACIONES PARA ALMACENAR LA ACOMODACIONDE LA BUSQUEDA
                if (bEntra)
                {
                    DataTable dtHabitacion = new DataTable(TABLA_HABITACIONES);
                    DataColumn dcIdHabitacion = new DataColumn(COLUMN_ID_HABITACION);
                    DataColumn dcAdulCount = new DataColumn(COLUMN_ADULT_COUNT);
                    DataColumn dcChildCount = new DataColumn(COLUMN_CHILD_COUNT);
                    DataColumn dcRoomCount = new DataColumn(COLUMN_ROOM_COUNT);
                    DataColumn dcRoomCountText = new DataColumn(COLUMN_ROOM_COUNT_TEXT);

                    //TRAEMOS LOS PARAMETROS DE BUSQUEDA GUARDADOS EN SESION
                    VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = new VO_HotelValuedAvailRQ();
                    vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();

                    dtHabitacion.Columns.Add(dcIdHabitacion);
                    dtHabitacion.Columns.Add(dcAdulCount);
                    dtHabitacion.Columns.Add(dcChildCount);
                    dtHabitacion.Columns.Add(dcRoomCount);
                    dtHabitacion.Columns.Add(dcRoomCountText);

                    for (int i = 0; i < vo_HotelValuedAvailRQ.lHotelOccupancy.Count; i++)
                    {
                        //LLENAMOS LA TABLA CON LO SPARAMETROS DE LA BUSQUEDA
                        DataRow drFila = dtHabitacion.NewRow();
                        drFila[COLUMN_ID_HABITACION] = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.lGuestList[0].CustomerId.ToString();
                        drFila[COLUMN_ADULT_COUNT] = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.AdultCount.ToString();
                        drFila[COLUMN_CHILD_COUNT] = vo_HotelValuedAvailRQ.lHotelOccupancy[i].Occupancy.ChildCount.ToString();
                        drFila[COLUMN_ROOM_COUNT] = vo_HotelValuedAvailRQ.lHotelOccupancy[i].RoomCount.ToString();
                        if (vo_HotelValuedAvailRQ.lHotelOccupancy[i].RoomCount > 1)
                        {
                            drFila[COLUMN_ROOM_COUNT_TEXT] = " * Numero de Habitaciones: " + vo_HotelValuedAvailRQ.lHotelOccupancy[i].RoomCount.ToString();
                        }
                        dtHabitacion.Rows.Add(drFila);
                    }
                    dsResultados.Tables.Add(dtHabitacion);
                }
            }
            catch { }
        }
        public void guardarHabitacionesSeleccion(DataTable dtHotel, List<VO_Passenger> lvo_Passenger)
        {
            string TipoHab = string.Empty;
            string Characteristic = string.Empty;
            foreach (VO_Passenger vo_Passenger in lvo_Passenger)
            {
                if (vo_Passenger.Activo)
                {
                    for (int i = 0; i < dtHotel.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (dtHotel.Rows[i][COLUMN_HOTEL_ROOM_OCUPATION].ToString() == "")
                            {
                                TipoHab = dtHotel.Rows[i][COLUMN_CODE].ToString();
                                Characteristic = dtHotel.Rows[i][COLUMN_CHARASTERISTIC].ToString();
                                dtHotel.Rows[i][COLUMN_HOTEL_ROOM_OCUPATION] = vo_Passenger.Pos;
                            }
                        }
                        else
                        {
                            TipoHab = dtHotel.Rows[i - 1][COLUMN_CODE].ToString();
                            Characteristic = dtHotel.Rows[i - 1][COLUMN_CHARASTERISTIC].ToString();
                            if (vo_Passenger.Adulto.Equals(dtHotel.Rows[i][COLUMN_ADULT_COUNT].ToString()) && vo_Passenger.Nino.Equals(dtHotel.Rows[i][COLUMN_CHILD_COUNT].ToString()))
                            {
                                if (dtHotel.Rows[i][COLUMN_SERVICE_HOTEL_ID].ToString() == dtHotel.Rows[i - 1][COLUMN_SERVICE_HOTEL_ID].ToString())
                                {
                                    if (dtHotel.Rows[i][COLUMN_HOTEL_ROOM_OCUPATION].ToString() == "")
                                    {
                                        if (dtHotel.Rows[i][COLUMN_CODE].ToString() == TipoHab && dtHotel.Rows[i][COLUMN_CHARASTERISTIC].ToString() != Characteristic)
                                            dtHotel.Rows[i][COLUMN_HOTEL_ROOM_OCUPATION] = vo_Passenger.Pos;
                                        else
                                        {
                                            if (dtHotel.Rows[i - 1][COLUMN_HOTEL_ROOM_OCUPATION].ToString() == lvo_Passenger.Count.ToString())
                                                dtHotel.Rows[i][COLUMN_HOTEL_ROOM_OCUPATION] = "1";
                                            else
                                                dtHotel.Rows[i][COLUMN_HOTEL_ROOM_OCUPATION] = Convert.ToString(int.Parse(vo_Passenger.Pos) + int.Parse(dtHotel.Rows[i - 1][COLUMN_HOTEL_ROOM_OCUPATION].ToString()));
                                        }
                                    }
                                }
                                else
                                {
                                    if (dtHotel.Rows[i][COLUMN_HOTEL_ROOM_OCUPATION].ToString() == "")
                                    {
                                        dtHotel.Rows[i][COLUMN_HOTEL_ROOM_OCUPATION] = vo_Passenger.Pos;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void guardarHabitacionesSeleccionDif(DataTable dtHotel, List<VO_Passenger> lvo_Passenger)
        {
            string TipoHab = string.Empty;
            string Characteristic = string.Empty;
            foreach (VO_Passenger vo_Passenger in lvo_Passenger)
            {
                if (vo_Passenger.Activo)
                {
                    for (int i = 0; i < dtHotel.Rows.Count; i++)
                    {

                        if (vo_Passenger.Adulto.Equals(dtHotel.Rows[i][COLUMN_ADULT_COUNT].ToString()) && vo_Passenger.Nino.Equals(dtHotel.Rows[i][COLUMN_CHILD_COUNT].ToString())
                            && dtHotel.Rows[i][COLUMN_ADULT_COUNT].ToString() != "")
                        {
                            dtHotel.Rows[i][COLUMN_HOTEL_ROOM_OCUPATION] = vo_Passenger.Pos;
                        }
                    }
                }
            }
        }
        public void CargarNameOccupancy()
        {
            List<VO_Passenger> cPassenger = clsSesiones.getPassenger();
            List<VO_HotelOccupancy> lvo_HotelOccupancy = getActualizarOcupancy();
            VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();
            csHoteles cHoteles = new csHoteles();
            VO_Customer Ocupansy = new VO_Customer();
            HttpContext.Current.Session["TableID"] = null;
            HttpContext.Current.Session["TableIDCH"] = null;

            DataSet dsResultados = cHoteles.setTablaReserva();
            DataTable dtRescostumer = dsResultados.Tables["Customer"];
            DataTable dtRescostumerCH = dsResultados.Tables["Customer"];
            DataView dvResp2 = new DataView(dtRescostumerCH);
            dvResp2.RowFilter = "type = 'CH'";
            dtRescostumerCH = dvResp2.ToTable();

            DataView dvResp = new DataView(dtRescostumer);
            dvResp.RowFilter = "type <> 'CH'";
            dtRescostumer = dvResp.ToTable();

            HttpContext.Current.Session["TableID"] = dtRescostumer;
            HttpContext.Current.Session["TableIDCH"] = dtRescostumerCH;

            string sEmail = string.Empty;
            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {
                sEmail = cCache.Email;
            }
            string sAdulto = clsValidaciones.GetKeyOrAdd("AdultoHB", "AD");
            int intHabitaciones = lvo_HotelOccupancy.Count;
            int f = 0;
            int t = 0;
            int childs = 0;
            for (int i = 0; i < intHabitaciones; i++)
            {
                int c = 1;


                int iContador = lvo_HotelOccupancy[i].Occupancy.lGuestList.Count;
                for (int a = 1; a < int.Parse(lvo_HotelOccupancy[i].Occupancy.AdultCount.ToString()); a++)
                {
                    if (a < int.Parse(lvo_HotelOccupancy[i].Occupancy.AdultCount.ToString()))
                    {
                        try
                        {
                            lvo_HotelOccupancy[i].Occupancy.lGuestList.Add(Ocupansy);
                            int contador = lvo_HotelOccupancy[i].Occupancy.lGuestList.Count - 1;
                            lvo_HotelOccupancy[i].Occupancy.lGuestList[contador].Type = "AD";
                            lvo_HotelOccupancy[i].Occupancy.lGuestList[contador].Name = "";
                            lvo_HotelOccupancy[i].Occupancy.lGuestList[contador].LastName = "";
                        }
                        catch
                        { }

                    }
                }

                for (int b = 0; b < lvo_HotelOccupancy[i].Occupancy.lGuestList.Count; b++)
                {
                    if (lvo_HotelOccupancy[i].Occupancy.lGuestList[b].Type.Equals("CH"))
                    {
                        string hab = ValidaHabitacion(i);
                        string adul = ValidaNinos(c);
                        lvo_HotelOccupancy[i].Occupancy.lGuestList[b].Name = "";
                        lvo_HotelOccupancy[i].Occupancy.lGuestList[b].LastName = "";
                        lvo_HotelOccupancy[i].Occupancy.lGuestList[b].CustomerId = int.Parse(dtRescostumerCH.Rows[childs]["CustomerId"].ToString());
                        c++;
                        childs++;
                    }
                }


                for (int j = 0; j < iContador; j++)
                {
                    if (lvo_HotelOccupancy[i].Occupancy.lGuestList[j].Type.Equals(sAdulto))
                    {
                        lvo_HotelOccupancy[i].Occupancy.lGuestList[j].Name = cPassenger[f].RespNombre;
                        lvo_HotelOccupancy[i].Occupancy.lGuestList[j].LastName = cPassenger[f].RespApellido;
                        lvo_HotelOccupancy[i].Occupancy.lGuestList[j].Email = sEmail;
                        lvo_HotelOccupancy[i].Occupancy.lGuestList[j].CustomerId = int.Parse(dtRescostumer.Rows[f]["CustomerId"].ToString());
                        try
                        {
                            if (!cPassenger[f].RespEmail.Length.Equals(0))
                            {
                                lvo_HotelOccupancy[i].Occupancy.lGuestList[j].Email = cPassenger[f].RespEmail;
                            }
                        }
                        catch { }
                        lvo_HotelOccupancy[i].Occupancy.lGuestList[j].Phone = cPassenger[f].RespTelefono;
                        f++;
                    }
                }
            }
            vo_HotelValuedAvailRQ.lHotelOccupancy = lvo_HotelOccupancy;
            clsSesiones.setParametrosHotel(vo_HotelValuedAvailRQ);
        }
        private List<VO_HotelOccupancy> getActualizarOcupancy()
        {
            List<VO_HotelOccupancy> lHotelOccupancy = clsSesiones.getParametrosHotel().lHotelOccupancy;
            VO_Customer customer = new VO_Customer();
            return lHotelOccupancy;
        }
        private string ValidaaDultos(int habitacion)
        {
            switch (habitacion)
            {
                case 1:
                    return "ONE";

                case 2:
                    return "TWO";

                case 3:
                    return "THREE";

                case 4:
                    return "FOUR";
            }
            return "";
        }
        private string ValidaHabitacion(int habitacion)
        {
            switch (habitacion)
            {
                case 0:
                    return "ONE";

                case 1:
                    return "TWO";

                case 2:
                    return "THREE";

                case 3:
                    return "FOUR";

                case 4:
                    return "FIVE";

                case 5:
                    return "SIX";

                case 6:
                    return "SEVEN";

                case 7:
                    return "EIGHT";

                case 8:
                    return "NINE";
            }
            return "";
        }
        private string ValidaNinos(int habitacion)
        {
            switch (habitacion)
            {
                case 1:
                    return "ONE";

                case 2:
                    return "TWO";
            }
            return "";
        }
        public void GuardarTablaOActualizarTabla(string IdentificadorUnico, DataTable tbldatos, string strNombreTabla)
        {
            DataSet dsTablaDatosCarrito = new DataSet();
            DataTable tblDatosCarrito = new DataTable();
            try
            {
                tbldatos.DataSet.Tables.Remove(tbldatos);
            }
            catch (Exception)
            { }
            try
            {
                dsTablaDatosCarrito = RecuperarDataSet(IdentificadorUnico);
                try
                {
                    dsTablaDatosCarrito.Tables.Remove(strNombreTabla);
                }
                catch (Exception)
                { }

                tbldatos.TableName = strNombreTabla;
                dsTablaDatosCarrito.Tables.Add(tbldatos);
                GuardarDataSetOActualizarDataSet(IdentificadorUnico, dsTablaDatosCarrito);
            }
            catch (Exception)
            {
                dsTablaDatosCarrito = new DataSet();
                tbldatos.TableName = strNombreTabla;
                dsTablaDatosCarrito.Tables.Add(tbldatos);
                GuardarDataSetOActualizarDataSet(IdentificadorUnico, dsTablaDatosCarrito);
            }
        }
        public DataSet RecuperarDataSet(string IdentificadorUnico)
        {
            DataSet dsDatos = new DataSet();
            try
            {
                clsSerializer dsSerializado = new clsSerializer();
                dsDatos = dsSerializado.XMLDataset(IdentificadorUnico);
            }
            catch (Exception)
            {
                dsDatos = null;
            }
            return dsDatos;
        }
        public void GuardarDataSetOActualizarDataSet(string IdentificadorUnico, DataSet dsDatos)
        {
            clsSerializer dsSerializado = new clsSerializer();
            dsSerializado.DatasetXML(dsDatos, IdentificadorUnico);
        }
        public void setDetalleConfirma(UserControl PageSource, bool bReserva, bool bCarrito)
        {


            clsParametros cParametros = new clsParametros();
            try
            {

                DataSet dsResultados = new DataSet();
                if (bReserva)
                {
                    dsResultados = clsSesiones.getConfirmaHotel();
                }
                else
                {
                    dsResultados = setTablaReserva();
                }

                if (dsResultados != null)
                {
                    DateTime dtFecha = DateTime.Now;
                    DataSet dsComodidades = new DataSet();

                    DataTable dtHotelInfo = dsResultados.Tables[TABLA_HOTEL_INFO];
                    DataTable dtHotelRoom = dsResultados.Tables[TABLA_HOTEL_ROOM];
                    DataTable dtCancelationPolicy = dsResultados.Tables[TABLA_CANCELATION_POLICY];
                    DataTable dtAditionalCost = dsResultados.Tables[TABLA_ADITIONAL_COST];
                    DataTable dtPriceGens = dsResultados.Tables[TABLA_PRICE];
                    DataTable dtDatesAndTimes = dsResultados.Tables[TABLA_DATE_TIME_FROM];

                    DataRow[] drHotelInfo = dtHotelInfo.Select();
                    DataRow[] drHotelRoom = dtHotelRoom.Select();


                    HiddenField TotalCarritoSinFormato = (HiddenField)PageSource.FindControl("TotalCarritoSinFormato");
                    Label lblTTipoPasajero = (Label)PageSource.FindControl("lblTTipoPasajero");
                    Label lblTTrato = (Label)PageSource.FindControl("lblTTrato");
                    Label lblTFechaNacimiento = (Label)PageSource.FindControl("lblTFechaNacimiento");
                    Label lblTPrimeroNombre = (Label)PageSource.FindControl("lblTPrimeroNombre");
                    Label lblTPrimerApellido = (Label)PageSource.FindControl("lblTPrimerApellido");
                    TextBox txtMarkupAfiliado = (TextBox)PageSource.FindControl("txtMarkupAfiliado");
                    TextBox txtvalorApagarReseva = (TextBox)PageSource.FindControl("txtvalorApagar");

                    Repeater rptHabitaciones = (Repeater)PageSource.FindControl("rptHabitaciones");
                    Repeater RptEstrellas = (Repeater)PageSource.FindControl("RptEstrellas");
                    Repeater RptAdicional = (Repeater)PageSource.FindControl("RptAdicional");
                    Repeater RptPenalizacionGara = (Repeater)PageSource.FindControl("RptPenalizacionGara");
                    Label lblUsuario = (Label)PageSource.FindControl("lblNombreUsuario");
                    clsCache cCache = clsSesiones.getCache();
                    if (lblUsuario != null)
                        lblUsuario.Text = cCache.Nombres.ToString();
                    Image iImagen = (Image)PageSource.FindControl("iImagen");
                    Image iOferta = (Image)PageSource.FindControl("iOferta");
                    Label lblNombre = (Label)PageSource.FindControl("lblNombre");
                    Label lblDireccion = (Label)PageSource.FindControl("lblDireccion");

                    Label lblDescripcion = (Label)PageSource.FindControl("lblDescripcion");
                    Label lblMoneda = (Label)PageSource.FindControl("lblMoneda");
                    Label lblPrecioTotal = (Label)PageSource.FindControl("lblPrecioTotal");
                    Button btnCancelar = (Button)PageSource.FindControl("btnCancelar");
                    // btnCancelar.Text = "Regresar";

                    HtmlTable tblInfoAdicional = (HtmlTable)PageSource.FindControl("tblInfoAdicional");
                    Label lblVatNumber = (Label)PageSource.FindControl("lblVatNumber");
                    Label lblIconming = (Label)PageSource.FindControl("lblIconming");
                    Label lblLocNumber = (Label)PageSource.FindControl("lblLocNumber");

                    Label lblFechaPago = (Label)PageSource.FindControl("lblFechaPago");
                    Label lblTFechaPago = (Label)PageSource.FindControl("lblTFechaPago");

                    Label lblObservaciones = (Label)PageSource.FindControl("lblObservaciones");
                    if (tblInfoAdicional != null)
                        tblInfoAdicional.Visible = false;
                    try
                    {
                        if (lblFechaPago != null)
                        {
                            lblFechaPago.Text = clsValidaciones.CalculaVctoDia(int.Parse(clsValidaciones.GetKeyOrAdd("DiasLimitePagoHotel", "2")));
                        }
                    }
                    catch
                    {
                        if (lblTFechaPago != null)
                        {
                            lblTFechaPago.Visible = false;
                        }
                    }
                    if (lblObservaciones != null)
                    {
                        try
                        {
                            lblObservaciones.Text = drHotelInfo[0][COLUMN_OBSERVACIONES].ToString();
                        }
                        catch { }
                    }
                    if (bReserva)
                    {
                        Button btnFinalizar = (Button)PageSource.FindControl("btnFinalizar");
                        if (btnFinalizar != null)
                            btnFinalizar.Visible = false;
                        Label lblRG = (Label)PageSource.FindControl("lblRG");
                        if (lblRG != null)
                            lblRG.Text = drHotelRoom[0][COLUMN_FILE_NUMBER].ToString();
                        if (btnCancelar != null)
                            btnCancelar.Text = "Terminar";
                        if (tblInfoAdicional != null)
                            tblInfoAdicional.Visible = true;
                        if (lblVatNumber != null)
                            lblVatNumber.Text = drHotelRoom[0][COLUMN_VAT_NUMBER].ToString();
                        if (lblIconming != null)
                            lblIconming.Text = drHotelRoom[0][COLUMN_INCOMING_CODE].ToString();
                        if (lblLocNumber != null)
                            lblLocNumber.Text = drHotelRoom[0][COLUMN_VAT_NUMBER].ToString();
                    }

                    iImagen.ImageUrl = drHotelInfo[0][COLUMN_IMAGEN_URL].ToString();
                    lblNombre.Text = drHotelInfo[0][COLUMN_NAME].ToString();
                    lblDireccion.Text = drHotelInfo[0][COLUMN_DESTINATION_NAME].ToString() + "&nbsp;&nbsp;&nbsp;" + drHotelInfo[0][COLUMN_ADDRESS].ToString();
                    lblDescripcion.Text = drHotelInfo[0][COLUMN_DESCRIPTION].ToString();
                    lblMoneda.Text = drHotelRoom[0][COLUMN_CURRENCY_CODE].ToString();
                    lblPrecioTotal.Text = drHotelRoom[0][COLUMN_TOTAL_PRICE_TEXT].ToString();

                    decimal dcTotal = 0;
                    foreach (DataRow dri in dtHotelRoom.Rows)
                    {
                        dcTotal = dcTotal + Convert.ToDecimal(dri[COLUMN_AMOUNT].ToString());
                    }
                    decimal dcTotal_1 = dcTotal;
                    decimal total_1 = Convert.ToDecimal(lblPrecioTotal.Text.ToString());
                    try
                    {
                        if (dcTotal_1 > total_1)
                        {
                            decimal dDif = dcTotal_1 - total_1;
                            dcTotal = dcTotal - dDif;
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    lblPrecioTotal.Text = dcTotal.ToString();

                    if (RptPenalizacionGara != null)
                    {
                        try
                        {
                            foreach (RepeaterItem item in RptPenalizacionGara.Items)
                            {
                                Label lblTxtHoraLimite = (Label)item.FindControl("lblTxtHoraLimite");
                                Label lblTxtFechaLimite = (Label)item.FindControl("lblTxtFechaLimite");
                                Label lbllblTxtPlataLimite = (Label)item.FindControl("lbllblTxtPlataLimite");
                                Label lbllblTxtCurrencyLimite = (Label)item.FindControl("lbllblTxtCurrencyLimite");

                                foreach (DataRow dri in dtHotelRoom.Rows)
                                {
                                    int i = 0;
                                    String timeText = dtDatesAndTimes.Rows[i]["time"].ToString();
                                    //string time = "0";
                                    string[] arr2 = new string[timeText.Length];
                                    int j = 0;
                                    foreach (char c in timeText)
                                    {
                                        arr2[j] = c.ToString();
                                        j++;
                                    }
                                    String time = "";
                                    for (j = 0; j < timeText.Length; j++)
                                    {
                                        if (j % 2 == 0 && j != 0)
                                        {
                                            time = time + ":";
                                        }
                                        time = time + arr2[j] + "";
                                    }

                                    lblTxtHoraLimite.Text = time;
                                    String DateText = dtDatesAndTimes.Rows[i]["date"].ToString();
                                    string[] arr3 = new string[DateText.Length];
                                    j = 0;
                                    foreach (char c in DateText)
                                    {
                                        arr3[j] = c.ToString();
                                        j++;
                                    }
                                    String Date1 = "";
                                    string dia = "", mes = "", ano = "";
                                    for (j = 0; j < DateText.Length; j++)
                                    {
                                        if (j < 4)
                                        {
                                            ano = ano + arr3[j] + "";
                                        }
                                        if (j >= 4 && j < 6)
                                        {
                                            mes = mes + arr3[j] + "";
                                        }
                                        if (j >= 6)
                                        {
                                            dia = dia + arr3[j] + "";
                                        }
                                    }
                                    Date1 = dia + "/" + mes + "/" + ano;
                                    lblTxtFechaLimite.Text = Date1.ToString();
                                    lbllblTxtPlataLimite.Text = dtPriceGens.Rows[4]["Amount"].ToString();
                                    lbllblTxtCurrencyLimite.Text = lblMoneda.Text;
                                    i++;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }

                    if (lblMoneda.Text.Equals("USD"))
                    {
                        string strPrecio = new csConsultasHoteles().sTasasDeCambio(lblMoneda.Text, clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP"), lblPrecioTotal.Text);
                        if (strPrecio != null && strPrecio != "")
                        {
                            lblMoneda.ToolTip = lblMoneda.Text + " " + lblPrecioTotal.Text;
                            lblPrecioTotal.ToolTip = lblMoneda.Text + " " + lblPrecioTotal.Text;
                            lblMoneda.Text = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                            lblPrecioTotal.Text = strPrecio;
                        }

                    }

                    if (!bReserva)
                    {
                        if (txtMarkupAfiliado != null)
                        {
                            if (txtMarkupAfiliado.Text == "")
                            {
                                txtMarkupAfiliado.Text = "0";
                            }
                        }
                        if (txtvalorApagarReseva != null)
                        {
                            if (txtvalorApagarReseva.Text == "")
                            {
                                txtvalorApagarReseva.Text = lblPrecioTotal.Text;
                            }
                        }
                        if (TotalCarritoSinFormato != null)
                        {
                            TotalCarritoSinFormato.Value = lblPrecioTotal.Text;
                        }
                    }
                    if (iOferta != null)
                        iOferta.ImageUrl = drHotelInfo[0][COLUMN_OFERTA].ToString();

                    try
                    {
                        Label lblGarantia = (Label)PageSource.FindControl("lblGarantia");
                        lblGarantia.Text = drHotelRoom[0][COLUMN_COMMENT_TEXT].ToString();
                    }
                    catch { }

                    setEstrellas(drHotelInfo[0][COLUMN_CATEGORY_CODE].ToString(), drHotelInfo[0][COLUMN_CATEGORY_TEXT].ToString(), RptEstrellas);
                    setHabitaciones(rptHabitaciones, drHotelRoom, dtCancelationPolicy);

                    if (RptAdicional != null)
                        cDisenio.setRepeater(dtAditionalCost, RptAdicional);
                    setCondicionesGenreales(PageSource);

                    if (bReserva)
                    {

                        Panel pError = (Panel)PageSource.FindControl("pError");
                        csReservaWs cReservas = new csReservaWs();
                        string sIdReservaCorreo = string.Empty;
                        TextBox txtvalorApagar = (TextBox)PageSource.FindControl("txtvalorApagar");
                        TextBox txtMarkup = (TextBox)PageSource.FindControl("txtMarkupAfiliado");

                        if (txtvalorApagar != null)
                        {
                            HttpContext.Current.Session["$ValorTotalHotel"] = txtvalorApagar.Text;
                        }
                        if (txtMarkup != null)
                        {
                            HttpContext.Current.Session["$ValorMarkupHotel"] = txtMarkup.Text;
                        }
                        if (lblMoneda != null)
                        {
                            HttpContext.Current.Session["$ValorMoneda"] = lblMoneda.Text;
                        }

                        cParametros = HotelBedsHoteles(cCache, bCarrito);
                        sIdReservaCorreo = clsValidaciones.GetKeyOrAdd("Correo", "Correo").ToString();

                        if (cParametros.Id.Equals(0))
                        {
                            Limpiar(cParametros, pError);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Aplication;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Error al Mostrar el detalle de confirmacion de hoteles";
                cParametros.ViewMessage.Add("Su reserva no fue confirmada");
                cParametros.Sugerencia.Add("Por favor comuniquese con el administrador");
                cParametros.Complemento = "Error en detalleconfirma";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void setHabitaciones
           (Repeater rptHabitaciones,
           DataRow[] drRooms,
           DataTable dtCancelationPolicy)
        {
            try
            {
                List<VO_Passenger> cPassenger = clsSesiones.getPassenger();
                int iHabitacion = cPassenger.Count;


                string sHabitaciones = "tblHabitacion";
                string sseqNum = "seqNum";
                string sNombre = "nombreHuesped";
                string sPrimerNombre = "Nombre";
                string sApellido = "Apellido";
                string sNumHuesped = "numHuespedes";
                string sTelefono = "telefonoHuesped";
                string sTipoHabitacion = "tipoHabitacion";
                string sCheckIn = "checkIn";
                string sCheckOut = "checkOut";
                string sEstado = "estado";
                string sRequerimientos = "requerimientos";
                string sPolitica = COLUMN_POLITICA;
                string sHotelId = COLUMN_HOTEL_ROOM_ID;
                string sAmount = COLUMN_AMOUNT;
                string sAmountText = COLUMN_AMOUNT_TEXT;
                string sCurrency = COLUMN_CURRENCY_CODE;
                string sRegimen = "Regimen";

                DataTable tblHabitacion = new DataTable(sHabitaciones);
                DataColumn dchotelId = new DataColumn(sHotelId);
                DataColumn dcseqNum = new DataColumn(sseqNum);
                DataColumn dcnombre = new DataColumn(sNombre);
                DataColumn dcnumHuesped = new DataColumn(sNumHuesped);
                DataColumn dctelefono = new DataColumn(sTelefono);
                DataColumn dctipoHabitacion = new DataColumn(sTipoHabitacion);
                DataColumn dccheckIn = new DataColumn(sCheckIn);
                DataColumn dccheckOut = new DataColumn(sCheckOut);
                DataColumn dcestado = new DataColumn(sEstado);
                DataColumn dcrequerimientos = new DataColumn(sRequerimientos);
                DataColumn dcpolitica = new DataColumn(sPolitica);
                DataColumn dcamount = new DataColumn(sAmount);
                DataColumn dcamountText = new DataColumn(sAmountText);
                DataColumn dccurrency = new DataColumn(sCurrency);
                DataColumn dcsRegimen = new DataColumn(sRegimen);
                DataColumn dcsPrimerNombre = new DataColumn(sPrimerNombre);
                DataColumn dcsApellido = new DataColumn(sApellido);

                tblHabitacion.Columns.Add(dchotelId);
                tblHabitacion.Columns.Add(dcseqNum);
                tblHabitacion.Columns.Add(dcnombre);
                tblHabitacion.Columns.Add(dcsPrimerNombre);
                tblHabitacion.Columns.Add(dcsApellido);
                tblHabitacion.Columns.Add(dcnumHuesped);
                tblHabitacion.Columns.Add(dctelefono);
                tblHabitacion.Columns.Add(dctipoHabitacion);
                tblHabitacion.Columns.Add(dccheckIn);
                tblHabitacion.Columns.Add(dccheckOut);
                tblHabitacion.Columns.Add(dcestado);
                tblHabitacion.Columns.Add(dcrequerimientos);
                tblHabitacion.Columns.Add(dcpolitica);
                tblHabitacion.Columns.Add(dcamount);
                tblHabitacion.Columns.Add(dcamountText);
                tblHabitacion.Columns.Add(dccurrency);
                tblHabitacion.Columns.Add(dcsRegimen);

                int i = 1;
                List<int> ilPos = new List<int>();
                for (int h = 0; h < drRooms.Length; h++)
                {
                    int iContar = int.Parse(drRooms[h][COLUMN_ROOM_COUNT].ToString());
                    for (int m = 0; m < iContar; m++)
                    {
                        ilPos.Add(h);
                    }
                }
                for (int j = 0; j < iHabitacion; j++)
                {
                    DataRow fila = tblHabitacion.NewRow();
                    fila[sseqNum] = i.ToString();
                    fila[sPrimerNombre] = cPassenger[j].RespNombre;
                    fila[sApellido] = cPassenger[j].RespApellido;
                    fila[sNombre] = cPassenger[j].RespNombre + " " + cPassenger[j].RespApellido;
                    fila[sNumHuesped] = cPassenger[j].Adulto + " / " + cPassenger[j].Nino;
                    fila[sTelefono] = cPassenger[j].RespTelefono;
                    fila[sRequerimientos] = cPassenger[j].Preferencias;


                    fila[sTipoHabitacion] = drRooms[ilPos[j]][COLUMN_ROOM_TYPE_TEXT].ToString();
                    fila[sCheckIn] = clsValidaciones.ConverYMDtoDMMY(drRooms[ilPos[j]][COLUMN_DATE_FROM_FORMAT].ToString(), "/");
                    fila[sCheckOut] = clsValidaciones.ConverYMDtoDMMY(drRooms[ilPos[j]][COLUMN_DATE_TO_FORMAT].ToString(), "/");
                    fila[sEstado] = drRooms[ilPos[j]][COLUMN_SERVICE_STATUS].ToString();
                    fila[sPolitica] = drRooms[ilPos[j]][COLUMN_POLITICA].ToString();
                    fila[sHotelId] = drRooms[ilPos[j]][COLUMN_HOTEL_ROOM_ID].ToString();
                    fila[sAmount] = drRooms[ilPos[j]][COLUMN_AMOUNT].ToString();
                    fila[sAmountText] = drRooms[ilPos[j]][COLUMN_AMOUNT_TEXT].ToString();
                    fila[sCurrency] = drRooms[ilPos[j]][COLUMN_CURRENCY_CODE].ToString();
                    fila[sRegimen] = drRooms[ilPos[j]][COLUMN_BOARD_TEXT].ToString();

                    tblHabitacion.Rows.Add(fila);
                    i++;
                }
                actualizarPasajeros(tblHabitacion);
                DataSet dsResultados = clsSesiones.getResultadoHotel();
                cDisenio.setRepeater(tblHabitacion, rptHabitaciones);
                DataRow[] drHotelRooms = tblHabitacion.Select();

                int k = 0;
                foreach (DataRow drHotelRoom in drHotelRooms)
                {
                    Repeater RptPenalizacion = (Repeater)rptHabitaciones.Items[k].FindControl("RptPenalizacion");
                    DataRow[] drCancelationPolicy = dtCancelationPolicy.Select(COLUMN_HOTEL_ROOM_ID + "=" + drHotelRoom[COLUMN_HOTEL_ROOM_ID].ToString() + "");
                    setCancelaciones(RptPenalizacion, drCancelationPolicy);
                    k++;
                }
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        private void actualizarPasajeros(DataTable dtHabitacion)
        {
            DataSet dsResultados = clsSesiones.getResultadoHotel();
            for (int i = 0; i < dtHabitacion.Rows.Count; i++)
            {
                if (dtHabitacion.Rows[i]["nombreHuesped"].ToString() == "")
                {
                    dsResultados.Tables["tblHabitacion"].Rows[i]["nombre"] = dtHabitacion.Rows[i]["Nombre"].ToString();
                    dsResultados.Tables["tblHabitacion"].Rows[i]["apellido"] = dtHabitacion.Rows[i]["Apellido"].ToString();
                }
            }
            clsSesiones.setResultadoHotel(dsResultados);
        }
        public void setCancelaciones
          (Repeater RptPenalizacion,
          DataRow[] drCancelationPolicys)
        {
            clsParametros cParametros = new clsParametros();
            try
            {

                DataTable tblCancelationPolicy = new DataTable(TABLA_CANCELATION_POLICY);
                DataColumn dcDateFrom = new DataColumn(COLUMN_DATE_TIME_FROM_FORMAT);
                DataColumn dcDateTo = new DataColumn(COLUMN_DATE_TIME_TO_FORMAT);
                DataColumn dcCurrencyCode = new DataColumn(COLUMN_CURRENCY_CODE);
                DataColumn dcTotalAmount = new DataColumn(COLUMN_CANCELATION_TOTAL_AMOUNT_TEXT);

                tblCancelationPolicy.Columns.Add(dcDateFrom);
                tblCancelationPolicy.Columns.Add(dcDateTo);
                tblCancelationPolicy.Columns.Add(dcCurrencyCode);
                tblCancelationPolicy.Columns.Add(dcTotalAmount);

                foreach (DataRow drCancelationPolicy in drCancelationPolicys)
                {
                    DataRow filaTarifa = tblCancelationPolicy.NewRow();

                    int Dias = int.Parse(clsValidaciones.GetKeyOrAdd("DiasPenalizacion", "3"));
                    filaTarifa[dcDateFrom] = drCancelationPolicy[COLUMN_DATE_TIME_FROM_FORMAT];
                    filaTarifa[dcDateTo] = drCancelationPolicy[COLUMN_DATE_TIME_TO_FORMAT];
                    filaTarifa[dcCurrencyCode] = drCancelationPolicy[COLUMN_CURRENCY_CODE];
                    filaTarifa[dcTotalAmount] = clsValidaciones.getDecimalRound(Convert.ToString(Convert.ToDecimal(drCancelationPolicy[COLUMN_CANCELATION_TOTAL_AMOUNT_TEXT].ToString())));
                    tblCancelationPolicy.Rows.Add(filaTarifa);
                }
                cDisenio.setRepeater(tblCancelationPolicy, RptPenalizacion);
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }

        private void setCondicionesGenreales(UserControl PageSource)
        {
            Label lblCondicones = (Label)PageSource.FindControl("lblCondicones");

            clsCache cCache = new csCache().cCache();
            if (lblCondicones == null)
                return;

            string sidioma = clsSesiones.getIdioma();
            string sAplicacion = clsSesiones.getAplicacion().ToString();
            string sTipoHotelBeds = clsValidaciones.GetKeyOrAdd("TipoHotelesHotelBeds", "HOTBED");
            string sTextoCondicionesPlan = clsValidaciones.GetKeyOrAdd("TextoCondicionesPlan", "");
            string sProductoRelacionPlanes = clsValidaciones.GetKeyOrAdd("ProductoRelacionPlanes", "Planes");
            string Condiciones = string.Empty;
        }
        private clsParametros HotelBedsHoteles(clsCache cCache, Boolean bCarrito)
        {
            clsResultados cResultados = new clsResultados();
            csReservas cReserva = new csReservas();
            try
            {
                cResultados = GenerarEstructura();
                if (!cResultados.Error.Id.Equals(0))
                {

                    if (!bCarrito)
                        setGuardarReservaHotel(cResultados, cCache);
                    if (!cResultados.Error.Id.Equals(0))
                    {
                        cResultados.Error = EnviarACarroCompras(cResultados, cCache);
                    }
                }
            }
            catch (Exception Ex)
            {
                cResultados.Error.Id = 0;
                cResultados.Error.Message = Ex.Message.ToString();
                cResultados.Error.Source = Ex.Source.ToString();
                cResultados.Error.Tipo = clsTipoError.Aplication;
                cResultados.Error.Severity = clsSeveridad.Moderada;
                cResultados.Error.StackTrace = Ex.StackTrace.ToString();
                cResultados.Error.Complemento = "Error al crear la estructura de reservas";
                cResultados.Error.ViewMessage.Add("Error al guardar en la base de datos");
                cResultados.Error.Sugerencia.Add("Por favor comuniquese con el administrador");
                ExceptionHandled.Publicar(cResultados.Error);
            }
            return cResultados.Error;
        }
        private clsResultados GenerarEstructura()
        {
            clsResultados cResultados = new clsResultados();
            clsParametros cParametros = new clsParametros();
            csReservas cReserva = new csReservas();
            try
            {
                cResultados.dsResultados = cReserva.CrearTablaReserva();
                cParametros.Id = 1;
                cResultados.Error = cParametros;
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Aplication;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Error al crear la estructura de reservas";
                cParametros.ViewMessage.Add("Error al guardar en la base de datos");
                cParametros.Sugerencia.Add("Por favor comuniquese con el administrador");
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
        public void setGuardarReservaHotel(clsResultados cResultados, clsCache cCache)
        {
            DataSet dsResultados = clsSesiones.getConfirmaHotel();
            DataTable dtHotelRoom = dsResultados.Tables[TABLA_HOTEL_ROOM];
            DataTable dtHotelInfo = dsResultados.Tables[TABLA_HOTEL_INFO];
            string sFechaIni = dtHotelInfo.Rows[0][COLUMN_DATE_FROM_FORMAT].ToString();
            string sFechaFin = dtHotelInfo.Rows[0][COLUMN_DATE_TO_FORMAT].ToString();
            string sRecord = dtHotelRoom.Rows[0][COLUMN_FILE_NUMBER].ToString();

            setMaster(cResultados, cCache, sFechaIni, sFechaFin, sRecord);
            setItinerario(cResultados, dsResultados, sFechaIni, sFechaFin, sRecord);
            setPax(cResultados, sRecord);
            setFareTax(cResultados, dsResultados, sRecord);
        }
        private void setMaster(clsResultados cResultados, clsCache cCache, string sFechaIni, string sFechaFin, string sRecord)
        {
            if (cResultados.dsResultados != null)
            {

                DataRow drMaster = cResultados.dsResultados.Tables[TABLA_MASTER].NewRow();


                drMaster["intProyecto"] = clsSesiones.getProyecto();
                drMaster["intFormaPago"] = "0";
                drMaster["intEstadoPago"] = "0";
                string strEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPagoPendiente", "PP");
                strEstadoPago = new CsConsultasVuelos().ConsultaCodigo(strEstadoPago, "tblEstadosPago", "intCodigo", "strCode");

                //otblRefere.Get(clsValidaciones.GetKeyOrAdd("FormasPago", "FP"), clsValidaciones.GetKeyOrAdd("Efectivo", "EFE"));
                drMaster["intFormaPago"] = "1";

                //otblRefere.Get(clsValidaciones.GetKeyOrAdd("EstadoPago", "EstadoPago"), clsValidaciones.GetKeyOrAdd("EstadoPagoPendiente", "PP"));
                drMaster["intEstadoPago"] = strEstadoPago;

                drMaster["dtmFechaLimitePago"] = sFechaIni;

                drMaster["intContacto"] = cCache.Contacto;
                drMaster["intCliente"] = cCache.Contacto;
                drMaster["dtmFechaIni"] = sFechaIni;
                drMaster["dtmFechaFin"] = sFechaFin;
                drMaster["dtmVencimiento"] = sFechaIni;

                string strEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoProyectoInicial", "SS");
                strEstadoReserva = new CsConsultasVuelos().ConsultaCodigo(strEstadoReserva, "tblEstados_Reserva", "intCodigo", "strCode");
                //otblRefere.Get(clsValidaciones.GetKeyOrAdd("EstadoReserva", "EstadoReserva"), clsValidaciones.GetKeyOrAdd("EstadoReservaConfirmada", "HK"));
                drMaster["intEstado"] = strEstadoReserva;

                drMaster["intResponsable"] = "0";

                string strTpoplan = clsValidaciones.GetKeyOrAdd("Tpoplanhoteles", "HINTER");
                strTpoplan = new CsConsultasVuelos().ConsultaCodigo(strTpoplan, "tblTpoServicio", "intCodigo", "strCode");
                //otblRefere.Get(clsValidaciones.GetKeyOrAdd("TiposPlan", "TipoPlan"), "HOT");
                drMaster["intTipoPlan"] = strTpoplan;

                drMaster["strReserva"] = sRecord;
                drMaster["dtmFecha"] = DateTime.Now.ToString("yyyy/MM/dd");
                drMaster["intCodigoPlan"] = "0";
                drMaster["strCodigo"] = "0";
                drMaster["intConsecRes"] = "1";

                cResultados.dsResultados.Tables[TABLA_MASTER].Rows.Add(drMaster);
            }
        }
        private void setItinerario(clsResultados cResultados, DataSet dsResultados, string sFechaIni, string sFechaFin, string sRecord)
        {
            if (cResultados.dsResultados != null)
            {
                clsContarPax cContarPax = new clsContarPax();

                int iContador = 0;
                int iPos = 1;
                DataTable dtHotelRoom = dsResultados.Tables[TABLA_HOTEL_ROOM];
                DataTable dtHotelInfo = dsResultados.Tables[TABLA_HOTEL_INFO];
                string sCodeDestino = dtHotelInfo.Rows[0][COLUMN_DESTINATION_CODE].ToString();
                string sNameDestino = dtHotelInfo.Rows[0][COLUMN_DESTINATION_NAME].ToString();

                string sAirPort = clsValidaciones.GetKeyOrAdd("AEROPUERTOS", "AEROPUERTOS");
                string sTipoHotelesHotelBeds = clsValidaciones.GetKeyOrAdd("TipoHotelesHotelBeds", "HOTBED");
                string sTipoPlan = clsValidaciones.GetKeyOrAdd("TiposPlan", "TipoPlan");
                string sEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoReserva", "EstadoReserva");
                string sEstadoReservaConfirmada = clsValidaciones.GetKeyOrAdd("EstadoReservaConfirmada", "HK");

                foreach (DataRow drHotelRoom in dtHotelRoom.Rows)
                {
                    DataRow drTransac = cResultados.dsResultados.Tables[TABLA_SEGMENTOS].NewRow();
                    string strTpoplan = clsValidaciones.GetKeyOrAdd("Tpoplanhoteles", "HINTER");
                    strTpoplan = new CsConsultasVuelos().ConsultaCodigo(strTpoplan, "tblTpoServicio", "intCodigo", "strCode");

                    drTransac["strReserva"] = sRecord;

                    drTransac["intTipoPlan"] = strTpoplan;

                    drTransac["intCodigoPlan"] = strTpoplan;
                    drTransac["intSegmento"] = iPos.ToString();

                    string sOrigen = new CsConsultasVuelos().ConsultaCodigo(sCodeDestino, "TblIata", "intCode", "strCode");
                    //otblRefere.Get(sAirPort, sCodeDestino, sNameDestino);
                    drTransac["intOrigen"] = sOrigen;

                    drTransac["intDestino"] = sOrigen;
                    drTransac["dtmFechaIni"] = sFechaIni;
                    drTransac["dtmFechaFin"] = sFechaFin;
                    drTransac["strHoraIni"] = "15:00";
                    drTransac["strHoraFin"] = "15:00";

                    drTransac["intProveedor"] = dtHotelRoom.Rows[0][COLUMN_HOTEL_INFO_CODE].ToString();
                    drTransac["intTipoAcomodacion"] = "0";
                    drTransac["intTipoHabitacion"] = "0";
                    drTransac["strOperador"] = dtHotelRoom.Rows[0][COLUMN_ROOM_TYPE_TEXT].ToString();
                    drTransac["strConfirma"] = sRecord;

                    string strEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoProyectoInicial", "SS");
                    strEstadoReserva = new CsConsultasVuelos().ConsultaCodigo(strEstadoReserva, "tblEstados_Reserva", "intCodigo", "strCode");
                    drTransac["intEstado"] = strEstadoReserva;

                    //drTransac["intEstado"] = clsValidaciones.GetKeyOrAdd("EstadoSolicitada"].ToString();
                    drTransac["strCodigo"] = "0";
                    drTransac["intConsecRes"] = "1";
                    drTransac["strObservacion"] = "";
                    drTransac["intCantidadPersonas"] = cContarPax.Paxs.ToString();

                    cResultados.dsResultados.Tables[TABLA_SEGMENTOS].Rows.Add(drTransac);

                    iContador++;
                    iPos++;
                }
            }
        }
        private void setPax
           (clsResultados cResultados, string sRecord)
        {
            if (cResultados.dsResultados != null)
            {
                List<VO_Passenger> cPassenger = clsSesiones.getPassenger();

                int iContadorPax = cPassenger.Count;


                int iHabitacion = 1;
                string sTipoPasajero = clsValidaciones.GetKeyOrAdd("TipoPasajero", "Tip_Pasajero");
                for (int j = 0; j < iContadorPax; j++)
                {
                    DataRow drPax = cResultados.dsResultados.Tables[TABLA_PAX].NewRow();
                    drPax["strReserva"] = sRecord;
                    drPax["intCodigoPax"] = iContadorPax.ToString();

                    //otblRefere.Get(sTipoPasajero, "H" + iHabitacion.ToString(), "Habitacion " + iHabitacion.ToString());
                    drPax["intTipoPax"] = "12";
                    drPax["strNombre"] = cPassenger[j].RespNombre.ToString() + " " + cPassenger[j].RespApellido.ToString();
                    drPax["strCodigo"] = "0";
                    drPax["intConsecRes"] = "1";
                    if (!cPassenger[j].Edad.Count.Equals(0))
                        drPax["intEdad"] = cPassenger[j].Edad[0].Edad.ToString();
                    iHabitacion++;
                    cResultados.dsResultados.Tables[TABLA_PAX].Rows.Add(drPax);
                }
            }
        }
        private void setFareTax(clsResultados cResultados, DataSet dsResultados, string sRecord)
        {
            if (cResultados.dsResultados != null)
            {
                List<VO_Passenger> cPassenger = clsSesiones.getPassenger();


                int iContadorPax = cPassenger.Count;

                string sSabreTax = clsValidaciones.GetKeyOrAdd("SABRETAX", "SABRETAX");
                string sTipoHotelesHotelBeds = clsValidaciones.GetKeyOrAdd("TipoHotelesHotelBeds", "HOTBED");
                string sTipoMoneda = clsValidaciones.GetKeyOrAdd("Moneda", "Moneda");

                DataTable dtHotelRoom = dsResultados.Tables[TABLA_HOTEL_ROOM];
                DataTable dtAditionalCost = dsResultados.Tables[TABLA_ADITIONAL_COST];

                int iHabitacion = 1;
                for (int j = 0; j < iContadorPax; j++)
                {

                    decimal iTotalImpuestos = 0;
                    decimal iTarifa = 0;

                    DataRow drTarifa = cResultados.dsResultados.Tables[TABLA_TARIFA].NewRow();
                    //otblRefere.Get(sTipoMoneda, dtHotelRoom.Rows[0][COLUMN_CURRENCY_CODE].ToString(), dtHotelRoom.Rows[0][COLUMN_CURRENCY_TEST].ToString());
                    string strMoneda = dtHotelRoom.Rows[0][COLUMN_CURRENCY_CODE].ToString();
                    strMoneda = new CsConsultasVuelos().ConsultaCodigo(strMoneda, "tblMonedas", "intCode", "strCode");

                    //string sMoneda = otblRefere.intidRefere.Value;
                    string sTipoPasajero = String.Empty;
                    sTipoPasajero = "1";
                    //otblRefere.Get(sTipoPasajero, "H" + iHabitacion.ToString(), "Habitacion " + iHabitacion.ToString());
                    //string strTipoPax = otblRefere.intidRefere.Value;

                    iTarifa = clsValidaciones.getDecimalRound(dtHotelRoom.Rows[j][COLUMN_AMOUNT_TEXT].ToString());

                    foreach (DataRow drAditionalCost in dtAditionalCost.Rows)
                    {

                        decimal iImpuestos = clsValidaciones.getDecimalRound(drAditionalCost[COLUMN_AMOUNT_TEXT].ToString());

                        iTotalImpuestos += iImpuestos;

                        DataRow drTax = cResultados.dsResultados.Tables[TABLA_TASAS].NewRow();
                        drTax["intCodigFare"] = "0";

                        //otblRefere.Get(sSabreTax, drAditionalCost[COLUMN_TYPE].ToString(), drAditionalCost[COLUMN_TYPE].ToString());
                        drTax["intCodigoTax"] = "33";
                        drTax["intTipoPax"] = sTipoPasajero;
                        drTax["intMoneda"] = strMoneda;
                        drTax["dblValorTax"] = clsValidaciones.getDecimalNotGroup(iImpuestos.ToString());
                        drTax["dblPorcent"] = "0";
                        drTax["strCodigo"] = "0";
                        drTax["intConsecRes"] = "1";
                        cResultados.dsResultados.Tables[TABLA_TASAS].Rows.Add(drTax);
                    }
                    decimal iTotalPasajero = iTotalImpuestos + iTarifa;

                    drTarifa["strReserva"] = sRecord;
                    drTarifa["intTipoPax"] = sTipoPasajero;
                    drTarifa["intCodigFare"] = "0";
                    drTarifa["intMoneda"] = strMoneda;
                    drTarifa["dblTax"] = clsValidaciones.getDecimalNotGroup(iTotalImpuestos.ToString());
                    drTarifa["dblValor"] = clsValidaciones.getDecimalNotGroup(iTarifa.ToString());
                    drTarifa["dblTotal"] = clsValidaciones.getDecimalNotGroup(iTotalPasajero.ToString());
                    drTarifa["strCodigo"] = "0";
                    drTarifa["intConsecRes"] = "1";
                    cResultados.dsResultados.Tables[TABLA_TARIFA].Rows.Add(drTarifa);

                    iHabitacion++;
                }
            }
        }
        private void Limpiar(clsParametros sMensaje, Panel pError)
        {
            clsErrorMensaje cError = new clsErrorMensaje();
            cError.getError(sMensaje, pError);
        }
        private clsParametros EnviarACarroCompras(clsResultados cResultados, clsCache cCache)
        {
            const string TABLA_HOTEL_ROOM = "HotelRoom";
            const string TABLA_HOTEL_INFO = "HotelInfo";

            const string COLUMN_DATE_FROM_FORMAT = "Date_From_YMD";
            const string COLUMN_DATE_TO_FORMAT = "Date_To_YMD";
            const string COLUMN_FILE_NUMBER = "FileNumber";
            const string COLUMN_DESTINATION_NAME = "Destination_Name";
            const string COLUMN_DESTINATION_CODE = "Destination_Code";
            const string COLUMN_HOTEL_INFO_CODE = "HotelInfo_Code";
            const string TABLA_ADITIONAL_COST = "AdditionalCost";
            const string TABLA_CANCELATION_POLICY = "CancellationPolicy";
            const string TABLA_DATE_TIME_FROM = "DateTimeFrom";
            const string COLUMN_AMOUNT_TEXT = "AmountText";
            const string COLUMN_TOTAL_AMOUNT = "TotalAmount";
            const string COLUMN_NAME = "Name";
            const string COLUMN_ZONE_TEXT = "Zone_Text";

            const string COLUMN_ROOM_TYPE_TEXT = "RoomType_Text";
            const string COLUMN_BOARD_TEXT = "Board_Text";
            const string COLUMN_TYPE = "Type";
            const string COLUMN_ROOM_COUNT = "RoomCount";
            const string COLUMN_ADULT_COUNT = "AdultCount";
            const string COLUMN_CHILD_COUNT = "ChildCount";
            const string COLUMN_DATE = "date";
            const string COLUMN_CANCELATION_TOTAL_VALUE = "Cancellation_AmountDay";
            const string COLUMN_CATEGORY_TEXT = "Category_Text";
            const string TABLA_SUPPLIER = "Supplier";
            const string COLUMN_SUPPLIER_NAME = "name";
            const string COLUMN_SUPPLIER_VATNUMBER = "vatNumber";
            const string TABLA_COMMENT = "Comment";
            const string COLUMN_COMMENT_TEXT = "Comment_Text";
            const string COLUMN_HOTEL_TELEPHONE_NUMBER = "Hotel_Telephone_Number";
            const string COLUMN_OBSERVACIONES = "Observaciones";
            const string COLUMN_INCOMING_CODE = "Incoming_Code";


            string strNombreCarroCompras = string.Empty;

            strNombreCarroCompras = "CarritoCompras";

            DataSet dsResultados = clsSesiones.getConfirmaHotel();
            DataTable dtHotelRoom = dsResultados.Tables[TABLA_HOTEL_ROOM];
            DataTable dtHotelInfo = dsResultados.Tables[TABLA_HOTEL_INFO];
            DataTable dtAditionalCost = dsResultados.Tables[TABLA_ADITIONAL_COST];
            DataTable dtCancellationPolicy = dsResultados.Tables[TABLA_DATE_TIME_FROM];
            DataTable dtcancellation = dsResultados.Tables[TABLA_CANCELATION_POLICY];
            DataTable dtSupplier = dsResultados.Tables[TABLA_SUPPLIER];
            DataTable dtComment = dsResultados.Tables[TABLA_COMMENT];

            string sFechaIni = dtHotelInfo.Rows[0][COLUMN_DATE_FROM_FORMAT].ToString();
            string sFechaFin = dtHotelInfo.Rows[0][COLUMN_DATE_TO_FORMAT].ToString();
            string sCodeDestino = dtHotelInfo.Rows[0][COLUMN_DESTINATION_CODE].ToString();
            string sNameDestino = dtHotelInfo.Rows[0][COLUMN_DESTINATION_NAME].ToString();
            string sRecord = dtHotelRoom.Rows[0][COLUMN_FILE_NUMBER].ToString();

            clsParametros objParametros = new clsParametros();
            string strConexion = clsValidaciones.GetKeyOrAdd("strConexion");

            try
            {
                string idSesion = string.Empty;
                csCarrito objCarritoCompras = null;

                objCarritoCompras = new csCarrito("Reserva" + cCache.SessionID, strNombreCarroCompras);


                /*CODIGO DEL PLAN*/
                String strCodigoPlan = "0";

                string sSabreTax = clsValidaciones.GetKeyOrAdd("SABRETAX", "SABRETAX");
                string sTipoHotelesHotelBeds = clsValidaciones.GetKeyOrAdd("WS_HOTEL_HB", "HOTBED");
                string sTipoPlan = clsValidaciones.GetKeyOrAdd("TiposPlan", "TipoPlan");
                string sEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoReserva", "EstadoReserva");
                string sEstadoReservaConfirmada = clsValidaciones.GetKeyOrAdd("EstadoReservaConfirmada", "HK");
                string sMoneda = clsValidaciones.GetKeyOrAdd("Moneda", "Moneda");
                string sAirPort = clsValidaciones.GetKeyOrAdd("AEROPUERTOS", "AEROPUERTOS");
                string sDiasPenalizacion = clsValidaciones.GetKeyOrAdd("DiasPenalizacion", "2");
                string sTipoPasajero = clsValidaciones.GetKeyOrAdd("TipoPasajero", "Tip_Pasajero");
                string sRemark = clsValidaciones.GetKeyOrAdd("remark", "DatHot");

                string sIdioma = clsSesiones.getIdioma();
                string sAplicacion = clsSesiones.getAplicacion().ToString();

                try
                {
                    if (!dtHotelInfo.Rows[0]["WsSelect"].ToString().Length.Equals(0))
                        sTipoHotelesHotelBeds = dtHotelInfo.Rows[0]["WsSelect"].ToString();
                }
                catch { }

                try
                {

                    string strTpoplan = clsValidaciones.GetKeyOrAdd("Tpoplanhoteles", "HINTER");
                    strTpoplan = new CsConsultasVuelos().ConsultaCodigo(strTpoplan, "tblTpoServicio", "intid", "strCodigo");
                    strCodigoPlan = strTpoplan;
                }
                catch (Exception)
                {
                    strCodigoPlan = "0";
                }

                /*TIPO PLAN*/
                String strTipoPlan = sTipoHotelesHotelBeds;


                String idRefereTipoPlan = "3";
                /*ESTADO SOLICITADA*/
                string strEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoProyectoInicial", "SS");
                strEstadoReserva = new CsConsultasVuelos().ConsultaCodigo(strEstadoReserva, "tblEstados_Reserva", "intCode", "strCode");
                String int_Id_EstadoSolicitada = strEstadoReserva;

                /*MONEDA*/
                String strTipoMoneda = dtHotelRoom.Rows[0]["Currency_Code"].ToString();
                if (HttpContext.Current.Session["$ValorMoneda"] != null)
                {
                    strTipoMoneda = HttpContext.Current.Session["$ValorMoneda"].ToString();
                    HttpContext.Current.Session["$ValorMoneda"] = null;
                }
                if (strTipoMoneda.Trim().ToUpper().Equals("US DOLLAR"))
                    strTipoMoneda = "USD";


                string strMoneda = new CsConsultasVuelos().ConsultaCodigo(strTipoMoneda, "tblMonedas", "intCode", "strCode");
                String id_Refere_Tipo_Moneda = strMoneda;

                /*TABLA DE SEGMENTOS*/
                DataTable dtSegmento = dtHotelRoom;

                /*NUMERO PASAJEROS*/
                clsContarPax cContarPax = new clsContarPax();
                Int32 intNumeroPasajeros = Convert.ToInt32(cContarPax.Paxs.ToString());
                List<VO_Passenger> cPassenger = clsSesiones.getPassenger();
                int segmento = 1;
                int Pos = 0;
                /*CANTIDAD DE HABITACIONES*/
                VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = clsSesiones.getParametrosHotel();
                objCarritoCompras.IntNumeroHabitaciones = vo_HotelValuedAvailRQ.TotalRoom.ToString();
                objCarritoCompras.StrNombrePlan = dtHotelInfo.Rows[0][COLUMN_NAME].ToString();
                /*CANTIDAD DE NOCHES*/
                objCarritoCompras.IntNumeroNoches = vo_HotelValuedAvailRQ.TotalNights.ToString();
                /*GUARDAMOS LOS DATOS BASICOS DE LOS SEGMENTOS*/
                foreach (DataRow drFilaSegmento in dtHotelRoom.Rows)
                {
                    /*RECORD*/
                    objCarritoCompras.StrCodigoReserva = sRecord;
                    objCarritoCompras.StrConfirmacion = sRecord;
                    objCarritoCompras.IntCodigoPlan = strCodigoPlan;
                    /*FECHA LIMITE DE TIQUETEO*/
                    int Dias = int.Parse(sDiasPenalizacion);
                    string sdtFecha_vencimiento = DateTime.Now.ToString(FORMATO_FECHA_BD);
                    try
                    {
                        sdtFecha_vencimiento = clsValidaciones.CalcularFechaDias(clsValidaciones.ConverYMDtoYMD(dtCancellationPolicy.Rows[0][COLUMN_DATE].ToString()), -Dias);
                    }
                    catch { }
                    objCarritoCompras.StrFechaVencimiento = clsValidaciones.ConverYMDtoMDY(sdtFecha_vencimiento, "/");
                    /*IDENTIFICADOR DEL PLAN*/
                    objCarritoCompras.StrIdentificadorDelPlan = strTipoPlan;
                    /*CANTIDAD PASAJEROS*/
                    objCarritoCompras.IntcantidadPersonas = intNumeroPasajeros.ToString();
                    objCarritoCompras.StrPasajeros = intNumeroPasajeros.ToString();
                    /*TIPO PLAN*/
                    objCarritoCompras.StrTipoPlan = strTipoPlan;
                    objCarritoCompras.IntTipoPlan = idRefereTipoPlan.ToString();
                    /*MONEDA*/
                    objCarritoCompras.StrTipoMoneda = strTipoMoneda;
                    objCarritoCompras.IntTipoMoneda = id_Refere_Tipo_Moneda.ToString();
                    /*TOTAL VALOR*/
                    objCarritoCompras.IntValorTotal = dtHotelRoom.Rows[0][COLUMN_TOTAL_AMOUNT].ToString();


                    try
                    {
                        if (dtcancellation.Rows.Count > 0)
                            objCarritoCompras.IntValorPenalidad = dtcancellation.Rows[0][COLUMN_CANCELATION_TOTAL_VALUE].ToString();
                        else
                            objCarritoCompras.IntValorPenalidad = "0";
                    }
                    catch { }
                    /*RPH*/
                    objCarritoCompras.IntSegmento = segmento.ToString();
                    /*ORIGEN Y DESTINO*/
                    if (dtHotelInfo != null)
                    {
                        string idIATA = new CsConsultasVuelos().ConsultaCodigo(dtHotelInfo.Rows[0]["Destination_Code"].ToString(), "TBLIATA", "INTCITY", "STRCODE");
                        if (idIATA != null)
                        {
                            if (idIATA != "")
                            {
                                objCarritoCompras.IntOrigen = idIATA;
                                objCarritoCompras.IntDestino = idIATA;
                            }
                            else
                            {
                                objCarritoCompras.IntOrigen = "1";
                                objCarritoCompras.IntDestino = "2";
                            }
                        }
                        else
                        {
                            objCarritoCompras.IntOrigen = "1";
                            objCarritoCompras.IntDestino = "2";
                        }


                        objCarritoCompras.StrOrigen = dtHotelInfo.Rows[0]["Destination_Code"].ToString();
                        objCarritoCompras.StrDestino = dtHotelInfo.Rows[0]["Destination_Code"].ToString();
                    }
                    else
                    {
                        objCarritoCompras.IntOrigen = "1";
                        objCarritoCompras.IntDestino = "2";
                        objCarritoCompras.StrOrigen = "MIA";
                        objCarritoCompras.StrDestino = "MIA";
                    }

                    foreach (DataRow drFilaSegmentos in dtSegmento.Rows)
                    {
                        objCarritoCompras.StrCiudad += objCarritoCompras.StrOrigen;
                    }
                    objCarritoCompras.StrAcomodacion = intNumeroPasajeros.ToString();
                    /*OBTENEMOS LA FECHA DE SALIDA*/
                    objCarritoCompras.StrFechaInicial = clsValidaciones.ConverYMDtoMDY(sFechaIni, "/");
                    /*OBTENEMOS LA FECHA DE LLEGADA*/
                    objCarritoCompras.StrFechaFinal = clsValidaciones.ConverYMDtoMDY(sFechaFin, "/");
                    /*LA HORA*/
                    objCarritoCompras.StrHoraIni = "15:00";
                    objCarritoCompras.StrHoraFin = "15:00";
                    /*ID AEROLINEA*/
                    objCarritoCompras.IntProveedor = dtHotelRoom.Rows[0][COLUMN_HOTEL_INFO_CODE].ToString();
                    /*ESTADO SOLICITADA*/
                    objCarritoCompras.IntEstado = int_Id_EstadoSolicitada;
                    /*CODIGO DEL LA AEROLINEA*/
                    objCarritoCompras.StrObservacion = "Tipo Habitacion: " + dtHotelRoom.Rows[Pos][COLUMN_ROOM_TYPE_TEXT].ToString() + "     Alimentacion: " + dtHotelRoom.Rows[Pos][COLUMN_BOARD_TEXT].ToString() + "    Adultos: " + dtHotelRoom.Rows[Pos][COLUMN_ADULT_COUNT].ToString() + "      Niños: " + dtHotelRoom.Rows[Pos][COLUMN_CHILD_COUNT].ToString();
                    objCarritoCompras.StrOperador = dtHotelInfo.Rows[0][COLUMN_NAME].ToString();
                    objCarritoCompras.StrCodigo = "0";
                    objCarritoCompras.StrDetalles = "";
                    objCarritoCompras.StrRestricciones = "";
                    objCarritoCompras.StrBeneFicios = "";
                    objCarritoCompras.StrEncuenta = "";
                    objCarritoCompras.StrZonaGeografica = "";
                    try
                    {
                        objCarritoCompras.StrEncuenta = dtHotelInfo.Rows[0][COLUMN_OBSERVACIONES].ToString();
                    }
                    catch { }
                    try
                    {
                        objCarritoCompras.StrLocalizadorExt = dtHotelInfo.Rows[0][COLUMN_INCOMING_CODE].ToString();
                    }
                    catch { }

                    if (objCarritoCompras.StrLocalizadorExt.Equals("0") || objCarritoCompras.StrLocalizadorExt.Equals("") && HttpContext.Current.Session["Incoming_Code"] != null)
                    {
                        objCarritoCompras.StrLocalizadorExt = HttpContext.Current.Session["Incoming_Code"].ToString();
                    }

                    objCarritoCompras.AddFields();
                    segmento++;
                    Pos++;
                }
                /*GUARDAMOS TIPO DE PASAJERO*/
                List<int> ilPos = new List<int>();
                for (int h = 0; h < dtHotelRoom.Rows.Count; h++)
                {
                    int iContar = int.Parse(dtHotelRoom.Rows[h][COLUMN_ROOM_COUNT].ToString());
                    for (int m = 0; m < iContar; m++)
                    {
                        ilPos.Add(h);
                    }
                }

                List<int> ilPosPen = new List<int>();
                for (int h = 0; h < dtcancellation.Rows.Count; h++)
                {

                    ilPosPen.Add(h);

                }

                for (int c = 0; c < cPassenger.Count; c++)
                {
                    //objtblRefere.Get(sTipoPasajero, "H" + (c + 1).ToString(), "Habitacion " + (c + 1).ToString());
                    String str_Tipo_Pasajero = "ADT";
                    String id_Refere_Tipo_Pasajero = "1";
                    Decimal dbl_total_Sin_impuestos_tasas = 0;
                    Decimal dbl_total_impuestos_tasas = 0;
                    Decimal dbl_total_Con_impuestos_tasas = 0;
                    Decimal dblValorPenalidad = 0;
                    string dblDescuento = "0";






                    if (int.Parse(dtHotelRoom.Rows[0][COLUMN_ROOM_COUNT].ToString()) > 1)
                    {
                        dbl_total_Con_impuestos_tasas = clsValidaciones.getDecimalRound(Convert.ToString(clsValidaciones.getDecimalNotGroup(dtHotelRoom.Rows[ilPosPen[0]][COLUMN_AMOUNT_TEXT].ToString()) / Decimal.Parse(dtHotelRoom.Rows[0][COLUMN_ROOM_COUNT].ToString())));
                        try
                        {
                            if (dtcancellation.Rows.Count > 0)
                                dblValorPenalidad = clsValidaciones.getDecimalRound(Convert.ToString(clsValidaciones.getDecimalNotGroup(dtcancellation.Rows[ilPosPen[0]][COLUMN_CANCELATION_TOTAL_VALUE].ToString()) / Decimal.Parse(dtHotelRoom.Rows[0][COLUMN_ROOM_COUNT].ToString())));
                            else
                                dblValorPenalidad = 0;
                        }
                        catch { }
                    }
                    else
                    {
                        dbl_total_Con_impuestos_tasas = clsValidaciones.getDecimalRound(dtHotelRoom.Rows[ilPos[c]][COLUMN_AMOUNT_TEXT].ToString());


                        try
                        {
                            if (dtcancellation.Rows.Count > 0)
                                dblValorPenalidad = clsValidaciones.getDecimalRound(dtcancellation.Rows[ilPosPen[c]][COLUMN_CANCELATION_TOTAL_VALUE].ToString());
                            else
                                dblValorPenalidad = 0;
                        }
                        catch { }
                    }


                    dbl_total_Sin_impuestos_tasas = dbl_total_Con_impuestos_tasas - dbl_total_impuestos_tasas;

                    objCarritoCompras.SaveTipoPax(
                        str_Tipo_Pasajero,
                        Convert.ToInt32(id_Refere_Tipo_Pasajero),
                        clsValidaciones.getDecimalBD(dbl_total_Con_impuestos_tasas.ToString()).ToString(),
                        clsValidaciones.getDecimalBD(dbl_total_Sin_impuestos_tasas.ToString()).ToString(),
                        clsValidaciones.getDecimalBD(dbl_total_impuestos_tasas.ToString()).ToString(),
                        dblDescuento,
                        dblValorPenalidad.ToString(),
                        Convert.ToInt32(objCarritoCompras.IntSegmento));

                }

                /*GUARDAMOS CADA PASAJERO*/
                for (int i = 0; i < cPassenger.Count; i++)
                {
                    //objtblRefere.Get(sTipoPasajero, "H" + (i + 1).ToString(), "Habitacion " + (i + 1).ToString());
                    String strTipoPasajero = "ADT";
                    String idRefereTipoPasajero = "1";
                    String str_Nombre_Completo = cPassenger[i].RespNombre.ToString() + " " + cPassenger[i].RespApellido.ToString();
                    String str_Telefono = "0";
                    Int32 intEdad = Convert.ToInt32(cPassenger[i].Edad[0].Edad);
                    objCarritoCompras.SavePerson(str_Nombre_Completo, null, strTipoPasajero, Convert.ToInt32(idRefereTipoPasajero), intEdad, 0, str_Telefono, Convert.ToInt32(objCarritoCompras.IntSegmento), null);
                }
                /*GUARDAMOS LAS TASAS DE CADA TIPO PASAJERO*/
                for (int i = 0; i < cPassenger.Count; i++)
                {
                    /*OBTENEMOS EL IDREFERE TIPO PAX*/
                    //objtblRefere.Get(sTipoPasajero, "H" + (i + 1).ToString(), "Habitacion " + (i + 1).ToString());
                    //String str_Tipo_Pasajero = "ADT";
                    String id_Refere_Tipo_Pasajero = "1";

                    foreach (DataRow drFilaTax in dtAditionalCost.Rows)
                    {
                        try
                        {
                            String strRefereTasa = drFilaTax[COLUMN_TYPE].ToString();
                            //String intIdCodigoTax = drFilaTax[COLUMN_TYPE].ToString();
                            String intIdCodigoTax = new CsConsultasVuelos().EjecutaProcedimiento("SPConsultaImpSabre", new string[1] { drFilaTax["TaxCode"].ToString() });
                            //objtblRefere.Get(sSabreTax, drFilaTax[COLUMN_TYPE].ToString(), drFilaTax[COLUMN_TYPE].ToString());

                            String intIdValorTax = clsValidaciones.getDecimalRound(drFilaTax[COLUMN_AMOUNT_TEXT].ToString()).ToString();
                            /*OBTENEMOS EL IDREFERE DE LA TASA*/
                            objCarritoCompras.AddTasa(intIdCodigoTax, "0", clsValidaciones.getDecimalBD(intIdValorTax), id_Refere_Tipo_Moneda, id_Refere_Tipo_Pasajero);
                        }
                        catch { }
                    }
                }

                /*TIPO PLAN*/
                //objtblRefere.Get(sTipoPlan, strTipoPlan);

                if (dtHotelInfo != null)
                {

                    objCarritoCompras.Saveremark(int.Parse("1"), 1, "Nombre Hotel", dtHotelInfo.Rows[0][COLUMN_NAME].ToString());
                    objCarritoCompras.Saveremark(int.Parse("2"), 2, "Direccion Hotel", dtHotelInfo.Rows[0][COLUMN_DESTINATION_NAME].ToString() + " " + dtHotelInfo.Rows[0][COLUMN_ZONE_TEXT].ToString());
                    if (dtComment != null)
                    {
                        if (dtComment.Rows[0][COLUMN_TYPE].ToString() != "SERVICE")
                            objCarritoCompras.Saveremark(int.Parse("3"), 3, "Contract", dtComment.Rows[0][COLUMN_COMMENT_TEXT].ToString());
                        else
                            objCarritoCompras.Saveremark(int.Parse("3"), 3, "Contract", "");
                    }
                    objCarritoCompras.Saveremark(int.Parse("4"), 4, "Estrellas", dtHotelInfo.Rows[0][COLUMN_CATEGORY_TEXT].ToString());
                    objCarritoCompras.Saveremark(int.Parse("5"), 5, "SupplierName", dtSupplier.Rows[0][COLUMN_SUPPLIER_NAME].ToString());
                    objCarritoCompras.Saveremark(int.Parse("6"), 6, "SupplierVatNumber", dtSupplier.Rows[0][COLUMN_SUPPLIER_VATNUMBER].ToString());
                    objCarritoCompras.Saveremark(int.Parse("7"), 7, "Telefono", dtHotelInfo.Rows[0][COLUMN_HOTEL_TELEPHONE_NUMBER].ToString());
                }

                /*GUARDAMOS EN XML*/
                objCarritoCompras.Save();
                /*GUARDAMOS LOS DATOS GENERALES DEL PROYECTO*/
                GuardarDatosProyecto();
                csReservas csRes = new csReservas();
                csRes.Conexion = strConexion;
                objParametros = csRes.GuardaReservaGen(objCarritoCompras.GetDsReservas());
                /*GUARDAMOS EL SQL QUE GENERO*/
                ExceptionHandled.Publicar(objParametros.Complemento);
                /*ACUTALIZAMO EL CODIGO DE PROYECTO*/
                if (clsSesiones.getProyecto() == "0")
                    clsSesiones.setProyecto(objParametros.DatoAdicArr[0].ToString());
                /*ACTUALIZAMOS EL CODIGO DEL INSERCION*/
                objCarritoCompras.Save_Update("1");
                clsSesiones.setPantalleRespuestaLogin("");

            }
            catch (Exception Ex)
            {
                objParametros.Id = 0;
                objParametros.Code = "0";
                objParametros.Metodo = Ex.TargetSite.Name;
                objParametros.Severity = clsSeveridad.Alta;
                objParametros.Source = Ex.Source;
                objParametros.StackTrace = Ex.StackTrace;
                objParametros.Sugerencia.Add("Se genero un error inesperado");
                objParametros.Message = Ex.Message;
                objParametros.Info = Ex.HelpLink;
                objParametros.InnerException = Ex.InnerException.Message;
                objParametros.Complemento = "Envio Carrito Hoteles";
                ExceptionHandled.Publicar(objParametros);
            }

            return objParametros;
        }
        private void GuardarDatosProyecto()
        {
            /*FECHA LIMITE DE PAGO*/
            DateTime dPlazo = clsSesiones.GET_TICKETE();
            const string strNombreCarroCompras = "CarritoCompras";
            clsCache cCache = new csCache().cCache();
            csCarrito csCarCompras = new csCarrito("Reserva" + cCache.SessionID, strNombreCarroCompras);
            string idRecord = clsSesiones.getProyecto();

            //Para el estado de la reserva
            string strEstadoReserva = clsValidaciones.GetKeyOrAdd("EstadoReservaConfirmada", "HK");
            strEstadoReserva = new CsConsultasVuelos().ConsultaCodigo(strEstadoReserva, "tblEstados_Reserva", "intCodigo", "strCode");
            //otblRefere.Get(clsValidaciones.GetKeyOrAdd("EstadoReserva", "EstadoReserva"), clsValidaciones.GetKeyOrAdd("EstadoReservaConfirmada", "HK"));
            string sEstado = string.Empty;
            string sFormaPago = string.Empty;
            string sEstadoPago = string.Empty;
            if (strEstadoReserva != null)
                sEstado = strEstadoReserva;
            //Para la forma de Pago
            string strEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPagoPendiente", "PP");
            strEstadoPago = new CsConsultasVuelos().ConsultaCodigo(strEstadoPago, "tblEstadosPago", "intCodigo", "strCode");

            //otblRefere.Get(clsValidaciones.GetKeyOrAdd("FormasPago", "FP"), clsValidaciones.GetKeyOrAdd("Efectivo", "efe"));
            //if (otblRefere.Respuesta == true)
            sFormaPago = "1";
            //Para el estado del Pago
            //otblRefere.Get(clsValidaciones.GetKeyOrAdd("EstadoPago", "EstadoPago"), clsValidaciones.GetKeyOrAdd("EstadoPagoPendiente", "PP"));
            if (strEstadoPago != null)
                sEstadoPago = strEstadoPago;

            csCarCompras.SaveDataProject(idRecord, cCache.Contacto, cCache.Contacto, "0", sEstado, sFormaPago, sEstadoPago);

        }
        public void setDetalle(DataSet dsResultados, UserControl PageSource, string strId)
        {
            if (dsResultados != null)
            {
                string sImagenPath = clsValidaciones.RutaImagesGen();
                string strWhereRoom = string.Empty;


                DateTime dtFecha = DateTime.Now;
                DataSet dsGaleria = new DataSet();

                DataTable dtComodidades = dsResultados.Tables[TABLA_FACILIDADES];
                DataTable dtOpciones = dsResultados.Tables[TABLA_OPCIONES];
                DataTable dtHotels = dsResultados.Tables[TABLA_HOTEL];
                DataTable dtRoom = setRoom(dsResultados);

                DataRow[] drHoteles = dtHotels.Select();
                DataRow[] drRooms = dtRoom.Select();

                Repeater rptTiposHabitacion = (Repeater)PageSource.FindControl("rptTiposHabitacion");
                Repeater RptEstrellas = (Repeater)PageSource.FindControl("RptEstrellas");
                Repeater RptGaleria = (Repeater)PageSource.FindControl("RptGaleria");
                Repeater RptComodidadesH = (Repeater)PageSource.FindControl("RptComodidadesH");
                Repeater RptComodidadesR = (Repeater)PageSource.FindControl("RptComodidadesR");


                Image iImagen = (Image)PageSource.FindControl("iImagen");
                Label lblNombre = (Label)PageSource.FindControl("lblNombre");
                Label lblDireccion = (Label)PageSource.FindControl("lblDireccion");
                Label lblUbicacion = (Label)PageSource.FindControl("lblUbicacion");
                Label lblPolitica = (Label)PageSource.FindControl("lblPolitica");
                Label lblDescripcion = (Label)PageSource.FindControl("lblDescripcion");
                Label lblComodidadesR = (Label)PageSource.FindControl("lblComodidadesR");
                Label lblComodidadesH = (Label)PageSource.FindControl("lblComodidadesH");
                Label lblGaleria = (Label)PageSource.FindControl("lblGaleria");

                Panel pMapa = (Panel)PageSource.FindControl("pMapa");

                iImagen.ImageUrl = sImagenPath + "spacer.gif";
                iImagen.Width = new Unit(0);
                iImagen.Height = new Unit(0);
                lblNombre.Text = drHoteles[0]["name"].ToString();
                lblDireccion.Text = drHoteles[0]["address"].ToString();



                setTiposHabitacion(rptTiposHabitacion, drRooms);
                try
                {
                    if (dsGaleria.Tables[0].Rows.Count > 0)
                    {
                        setGaleria(dsGaleria.Tables[0], RptGaleria);
                        setEstrellas(dsGaleria.Tables[0].Rows[0]["Category"].ToString(), dsGaleria.Tables[0].Rows[0]["Category"].ToString(), RptEstrellas);
                        lblGaleria.Text = "Galeria";
                        for (int i = 0; i < dsGaleria.Tables[0].Rows.Count; i++)
                        {
                            if (dsGaleria.Tables[0].Rows[i]["Code"].ToString() == "GEN")
                            {
                                iImagen.ImageUrl = dsGaleria.Tables[0].Rows[i]["ImagePath"].ToString();
                                iImagen.Width = new Unit(80);
                                iImagen.Height = new Unit(80);
                            }
                        }
                    }
                }
                catch { }

                try
                {
                    if (dtComodidades.Rows.Count > 0)
                    {
                        setFacility(dtComodidades, RptComodidadesH);
                        lblComodidadesH.Text = "Comodidades";
                    }
                }
                catch { }
                try
                {
                    if (dtOpciones.Rows.Count > 0)
                    {
                        setFacility(dtOpciones, RptComodidadesR);
                        lblComodidadesR.Text = "Otros";
                    }
                }
                catch { }
                if (!drHoteles[0]["latitude"].ToString().Trim().Length.Equals(0))
                {
                    setMapa(pMapa, drHoteles[0]);
                    lblUbicacion.Text = "Ubicación";
                }
            }
        }
        public void setDetalle(UserControl PageSource, string strId, DataSet dsDetalleHotel)
        {
            //DataSet dsData = clsSesiones.getResultadoHotel();
            //if (dsData != null)
            //{
            //    DateTime now = DateTime.Now;
            //    DataSet dsHotel = new DataSet();
            //    DataTable table = dsData.Tables["HotelInfo"];
            //    DataTable table2 = dsData.Tables["HotelRoom"];
            //    DataTable dtOcupancy = dsData.Tables["HotelOccupancy"];
            //    DataTable dtOcupacion = dsData.Tables["tblHabitaciones"];
            //    string filterExpression = "code='" + strId + "'";
            //    DataRow[] rowArray = table.Select(filterExpression);
            //    int length = rowArray.Length;
            //    if (length.Equals(0))
            //    {
            //        filterExpression = "code = " + strId;
            //        rowArray = table.Select(filterExpression);
            //    }
            //    string str2 = "HotelInfo_Id='" + rowArray[0]["HotelInfo_Id"].ToString() + "'";
            //    DataRow[] drRoom = table2.Select(str2);
            //    length = drRoom.Length;
            //    if (length.Equals(0))
            //    {
            //        str2 = "HotelInfo_Id = " + rowArray[0]["HotelInfo_Id"].ToString();
            //        drRoom = table.Select(filterExpression);
            //    }
            //    Repeater repeater = (Repeater)PageSource.FindControl("rptTiposHabitacion");
            //    Repeater rptOcupacion = (Repeater)PageSource.FindControl("rptOcupacion");
            //    Repeater RptEstrellas = (Repeater)PageSource.FindControl("RptEstrellas");
            //    Repeater RptGaleria = (Repeater)PageSource.FindControl("RptGaleria");
            //    Repeater RptComodidadesH = (Repeater)PageSource.FindControl("RptComodidadesH");
            //    Repeater RptComodidadesR = (Repeater)PageSource.FindControl("RptComodidadesR");

            //    Image image = (Image)PageSource.FindControl("iImagen");
            //    Label label = (Label)PageSource.FindControl("lblNombre");
            //    Label label2 = (Label)PageSource.FindControl("lblDireccion");
            //    Label label3 = (Label)PageSource.FindControl("lblTelefono");
            //    Label label4 = (Label)PageSource.FindControl("lblPolitica");
            //    Label label5 = (Label)PageSource.FindControl("lblDescripcion");
            //    Label label6 = (Label)PageSource.FindControl("lblComodidadesR");
            //    Label label7 = (Label)PageSource.FindControl("lblComodidadesH");
            //    Panel pMapa = (Panel)PageSource.FindControl("pMapa");
            //    image.ImageUrl = rowArray[0]["Hotel_Photo"].ToString();
            //    label.Text = rowArray[0]["Name"].ToString();
            //    label2.Text = rowArray[0]["Destination_Name"].ToString() + "&nbsp;&nbsp;&nbsp;" + rowArray[0]["Zone_Text"].ToString() + "&nbsp;&nbsp;&nbsp;" + rowArray[0]["Address"].ToString();
            //    label5.Text = rowArray[0]["description_Long"].ToString();
            //    if (rowArray[0]["WsSelect"].ToString().Equals(clsValidaciones.GetKeyOrAdd("WS_HOTEL_TT", "WS_TT")))
            //    {
            //        dsHotel = this.dsHotelFacility(dsData, strId);
            //    }
            //    else if (rowArray[0]["WsSelect"].ToString().Equals(clsValidaciones.GetKeyOrAdd("WS_HOTEL_TRC", "WS_TRC")))
            //    {
            //        dsHotel = this.dsHotelFacility(dsData, strId);
            //    }

            //    this.setEstrellas(rowArray[0]["Category_Code"].ToString(), rowArray[0]["Category_Text"].ToString(), RptEstrellas);
            //    int num = clsValidaciones.CalcularDias(rowArray[0]["Date_To_YMD"].ToString(), rowArray[0]["Date_From_YMD"].ToString());
            //    now = clsValidaciones.RetornaFecha(rowArray[0]["Date_To_YMD"].ToString());
            //    this.setOcupacion(rptOcupacion, dtOcupacion, drRoom, dtOcupancy);
            //    if (dsHotel.Tables.Count >= 3)
            //    {
            //        this.setGaleria(dsHotel.Tables[2], RptGaleria);
            //        this.setFacility(dsHotel, RptComodidadesH, RptComodidadesR);
            //        try
            //        {
            //            label7.Text = dsHotel.Tables[0].Rows[0]["name_group"].ToString();
            //        }
            //        catch
            //        {
            //        }
            //        try
            //        {
            //            label6.Text = dsHotel.Tables[1].Rows[0]["name_group"].ToString();
            //        }
            //        catch
            //        {
            //        }
            //        for (int i = 0; i < RptGaleria.Items.Count; i++)
            //        {
            //            Label label8 = (Label)RptGaleria.Items[i].FindControl("lblGaleria");
            //            try
            //            {
            //                if (label8.Text.Length.Equals(0))
            //                {
            //                    label8.Text = "Galeria";
            //                }
            //            }
            //            catch
            //            {
            //            }
            //        }
            //    }
            //    this.setMapa(pMapa, rowArray[0]);
            //}
            DataSet dsResultados = clsSesiones.getResultadoHotel();
            //Utils.Utils cUtil = new Utils.Utils();

            if (dsResultados != null)
            {
                DateTime dtFecha = DateTime.Now;
                DataSet dsComodidades = new DataSet();
                DataTable dtHotelAddress = dsDetalleHotel.Tables[TABLA_ADDRESS];
                DataTable dtHotelInfo = dsResultados.Tables[TABLA_HOTEL_INFO];
                DataTable dtHotelRoom = dsResultados.Tables[TABLA_HOTEL_ROOM];
                DataTable dtHotelOcupancy = dsResultados.Tables[TABLA_HOTEL_OCCUPANCY];
                DataTable dtHabitacion = dsResultados.Tables[TABLA_HABITACIONES];

                string sWhereH = COLUMN_CODE + "='" + strId + "'";
                DataRow[] drHoteles = dtHotelInfo.Select(sWhereH);
                if (drHoteles.Length.Equals(0))
                {
                    sWhereH = COLUMN_CODE + " = " + strId;
                    drHoteles = dtHotelInfo.Select(sWhereH);
                }

                string sWhereR = COLUMN_HOTEL_INFO_ID + "='" + drHoteles[0][COLUMN_HOTEL_INFO_ID].ToString() + "'";
                DataRow[] drRooms = dtHotelRoom.Select(sWhereR);
                if (drRooms.Length.Equals(0))
                {
                    sWhereR = COLUMN_HOTEL_INFO_ID + " = " + drHoteles[0][COLUMN_HOTEL_INFO_ID].ToString();
                    drRooms = dtHotelInfo.Select(sWhereH);
                }

                Repeater rptTiposHabitacion = (Repeater)PageSource.FindControl("rptTiposHabitacion");
                Repeater rptOcupacion = (Repeater)PageSource.FindControl("rptOcupacion");
                DataList dlEstrellas = (DataList)PageSource.FindControl("dlEstrellas");
                Repeater RptGaleria = (Repeater)PageSource.FindControl("RptGaleria");
                Repeater RptComodidadesH = (Repeater)PageSource.FindControl("RptComodidadesH");
                Repeater RptComodidadesR = (Repeater)PageSource.FindControl("RptComodidadesR");

                Image iImagen = (Image)PageSource.FindControl("iImagen");
                Label lblNombre = (Label)PageSource.FindControl("lblNombre");
                Label lblDireccion = (Label)PageSource.FindControl("lblDireccion");
                Label lblTelefono = (Label)PageSource.FindControl("lblTelefono");
                Label lblPolitica = (Label)PageSource.FindControl("lblPolitica");
                Label lblDescripcion = (Label)PageSource.FindControl("lblDescripcion");
                Label lblComodidadesR = (Label)PageSource.FindControl("lblComodidadesR");
                Label lblComodidadesH = (Label)PageSource.FindControl("lblComodidadesH");

                Panel pMapa = (Panel)PageSource.FindControl("pMapa");

                iImagen.ImageUrl = drHoteles[0][COLUMN_IMAGEN_URL].ToString();
                lblNombre.Text = drHoteles[0][COLUMN_NAME].ToString();
                lblDireccion.Text = drHoteles[0][COLUMN_DESTINATION_NAME].ToString() + "&nbsp;&nbsp;&nbsp;" + drHoteles[0][COLUMN_ZONE_TEXT].ToString() + "&nbsp;&nbsp;&nbsp;" + dtHotelAddress.Rows[0][COLUMN_STREET_NAME].ToString();
                lblDescripcion.Text = drHoteles[0][COLUMN_DESCRIPTION_LONG].ToString();

                //dsComodidades = dsHotelbedsFacility("CAS", strId, false);
                if (drHoteles[0][COLUMN_WS_SELECT].ToString().Equals(clsValidaciones.GetKeyOrAdd("WS_HOTEL_TT", "WS_TT")))
                {
                    dsComodidades = dsHotelFacility(dsResultados, strId);
                }
                else
                {
                    if (drHoteles[0][COLUMN_WS_SELECT].ToString().Equals(clsValidaciones.GetKeyOrAdd("WS_HOTEL_TRC", "WS_TRC")))
                    {
                        dsComodidades = dsHotelFacility(dsResultados, strId);
                    }
                    else
                    {
                        clsResultados cResultados = clsSesiones.getResultados();
                        if (cResultados.Error.Id.Equals(0))
                        {
                            dsComodidades = dsHotelbedsFacility("CAS", strId, false);
                        }
                        else
                        {
                            dsComodidades = dsHotelbedsFacility(cResultados.dsResultados, strId);
                        }
                    }
                }
                setEstrellas(drHoteles[0][COLUMN_CATEGORY_CODE].ToString(), drHoteles[0][COLUMN_CATEGORY_TEXT].ToString(), dlEstrellas);

                int iDias = clsValidaciones.CalcularDias(drHoteles[0][COLUMN_DATE_TO_FORMAT].ToString(), drHoteles[0][COLUMN_DATE_FROM_FORMAT].ToString());
                dtFecha = clsValidaciones.RetornaFecha(drHoteles[0][COLUMN_DATE_TO_FORMAT].ToString());

                setOcupacion(rptOcupacion, dtHabitacion, drRooms, dtHotelOcupancy);

                if (dsComodidades.Tables.Count >= 3)
                {
                    setGaleria(dsComodidades.Tables[2], RptGaleria);
                    setFacility(dsComodidades, RptComodidadesH, RptComodidadesR);
                    try
                    {
                        lblComodidadesH.Text = dsComodidades.Tables[0].Rows[0]["name_group"].ToString();
                    }
                    catch { }
                    try
                    {
                        lblComodidadesR.Text = dsComodidades.Tables[1].Rows[0]["name_group"].ToString();
                    }
                    catch { }
                    for (int i = 0; i < RptGaleria.Items.Count; i++)
                    {
                        Label lblGaleria = (Label)RptGaleria.Items[i].FindControl("lblGaleria");
                        try
                        {
                            if (lblGaleria.Text.Length.Equals(0))
                                lblGaleria.Text = "Galeria";
                        }
                        catch { }
                    }
                }
                setMapa(pMapa, drHoteles[0]);
            }
        }
        public void setEstrellas(string sNivelEstrellas, string sEstrellasTexto, DataList dlEstrellas)
        {
            try
            {
                if (dlEstrellas != null)
                {
                    int Pos = sNivelEstrellas.IndexOf("E");
                    if (Pos > -1)
                        sNivelEstrellas = sNivelEstrellas.Remove(Pos);

                    Pos = sNivelEstrellas.IndexOf("H");
                    if (Pos > -1)
                        sNivelEstrellas = sNivelEstrellas.Remove(0, 1);

                    if (!String.IsNullOrEmpty(sNivelEstrellas))
                    {
                        string[] sNumEs = null;

                        if (sNivelEstrellas.Contains("_"))
                        {
                            sNumEs = sNivelEstrellas.Split(new char[] { '_' });
                        }
                        else
                        {
                            if (sNivelEstrellas.Contains("."))
                            {
                                sNumEs = sNivelEstrellas.Split(new char[] { '.' });
                            }
                            else
                            {
                                sNumEs = sNivelEstrellas.Split(new char[] { ',' });
                            }
                        }
                        int iNum = 0;
                        try
                        {
                            iNum = int.Parse(sNumEs[0]);
                        }
                        catch { }

                        int iConStar = 0;
                        string sCamposStyle = "style";
                        string sCamposCategoria = "Categoria";
                        string sEstrella = "stars";
                        string sEstrellaMedia = "stars2";
                        string sSinEstrella = "starsNo";
                        DataTable tblEstrellas = new DataTable("estrellas");
                        DataColumn dcStyle = new DataColumn(sCamposStyle);
                        DataColumn dcCategory = new DataColumn(sCamposCategoria);
                        tblEstrellas.Columns.Add(dcStyle);
                        tblEstrellas.Columns.Add(dcCategory);

                        while (iNum > iConStar)
                        {
                            DataRow drEstrella = tblEstrellas.NewRow();
                            drEstrella[sCamposStyle] = sEstrella;
                            tblEstrellas.Rows.Add(drEstrella);
                            iConStar++;
                        }

                        if (sNumEs.Length > 1)
                        {
                            DataRow drEstrella = tblEstrellas.NewRow();
                            drEstrella[sCamposStyle] = sEstrellaMedia;
                            tblEstrellas.Rows.Add(drEstrella);
                        }
                        if (iNum.Equals(0))
                        {
                            DataRow drEstrella = tblEstrellas.NewRow();
                            drEstrella[sCamposStyle] = sSinEstrella;
                            drEstrella[sCamposCategoria] = sEstrellasTexto;

                            tblEstrellas.Rows.Add(drEstrella);
                        }
                        dlEstrellas.DataSource = tblEstrellas;
                        dlEstrellas.DataBind();
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        public DataSet dsHotelFacility(DataSet dsData, string idHotel)
        {
            DataTable dtHotelFacilities = dsData.Tables[TABLA_FEATURE];
            DataTable dtHotelInstalaciones = dsData.Tables[TABLA_INSTALACIONES];

            DataTable dtImagenes = dsData.Tables[TABLA_FACILITIES_IMAGEN];
            DataSet dsHotelBedsFacilities = new DataSet();
            try
            {
                DataTable dtFacilities = new DataTable(TABLA_FACILIDADES);
                DataTable dtInstalaciones = new DataTable(TABLA_INSTALACIONES);
                DataTable dtImagen = new DataTable(TABLA_IMAGEN);

                DataColumn dcFHotelCode = new DataColumn(COLUMN_HOTEL_CODE);
                DataColumn dcFCode = new DataColumn(COLUMN_FACILITIES_CODE);
                DataColumn dcFFacilidad = new DataColumn(COLUMN_FACILITIES);
                DataColumn dcFGroup = new DataColumn(COLUMN_NAME_GROUP);
                DataColumn dcFCarDistance = new DataColumn(COLUMN_CAR_DISTANCE);
                DataColumn dcFConcept = new DataColumn(COLUMN_CONCEPT);
                DataColumn dcFValue = new DataColumn(COLUMN_VALUE);

                DataColumn dcIHotelCode = new DataColumn(COLUMN_HOTEL_CODE);
                DataColumn dcICode = new DataColumn(COLUMN_FACILITIES_CODE);
                DataColumn dcIFacilidad = new DataColumn(COLUMN_FACILITIES);
                DataColumn dcIGroup = new DataColumn(COLUMN_NAME_GROUP);
                DataColumn dcICarDistance = new DataColumn(COLUMN_CAR_DISTANCE);
                DataColumn dcIConcept = new DataColumn(COLUMN_CONCEPT);
                DataColumn dcIValue = new DataColumn(COLUMN_VALUE);

                DataColumn dcName = new DataColumn(COLUMN_IMAGE_NAME);
                DataColumn dcImagePath = new DataColumn(COLUMN_IMAGEPATH);

                dtFacilities.Columns.Add(dcFHotelCode);
                dtFacilities.Columns.Add(dcFCode);
                dtFacilities.Columns.Add(dcFFacilidad);
                dtFacilities.Columns.Add(dcFGroup);
                dtFacilities.Columns.Add(dcFCarDistance);
                dtFacilities.Columns.Add(dcFConcept);
                dtFacilities.Columns.Add(dcFValue);

                dtInstalaciones.Columns.Add(dcIHotelCode);
                dtInstalaciones.Columns.Add(dcICode);
                dtInstalaciones.Columns.Add(dcIFacilidad);
                dtInstalaciones.Columns.Add(dcIGroup);
                dtInstalaciones.Columns.Add(dcICarDistance);
                dtInstalaciones.Columns.Add(dcIConcept);
                dtInstalaciones.Columns.Add(dcIValue);

                dtImagen.Columns.Add(dcName);
                dtImagen.Columns.Add(dcImagePath);

                DataRow[] drInstalaciones = dtHotelInstalaciones.Select(COLUMN_HOTEL_INFO_CODE + "='" + idHotel + "'");
                if (drInstalaciones.Length.Equals(0))
                    drInstalaciones = dtHotelInstalaciones.Select(COLUMN_HOTEL_INFO_CODE + " = " + idHotel);

                DataRow[] drFacilidades = dtHotelFacilities.Select(COLUMN_HOTEL_INFO_CODE + "='" + idHotel + "'");
                if (drFacilidades.Length.Equals(0))
                    drFacilidades = dtHotelFacilities.Select(COLUMN_HOTEL_INFO_CODE + " = " + idHotel);

                DataRow[] drImagenes = dtImagenes.Select(COLUMN_HOTEL_INFO_CODE + "='" + idHotel + "'");
                if (drImagenes.Length.Equals(0))
                    drImagenes = dtImagenes.Select(COLUMN_HOTEL_INFO_CODE + " = " + idHotel);


                foreach (DataRow drInstalacion in drInstalaciones)
                {
                    DataRow drFilaInst = dtInstalaciones.NewRow();
                    drFilaInst[COLUMN_HOTEL_CODE] = idHotel;
                    drFilaInst[COLUMN_FACILITIES_CODE] = drInstalacion[COLUMN_FACILITIES_CODE].ToString();
                    drFilaInst[COLUMN_FACILITIES] = drInstalacion[COLUMN_FACILITIES_DESCRIPTION].ToString();
                    drFilaInst[COLUMN_NAME_GROUP] = "Instalaciones";
                    drFilaInst[COLUMN_CAR_DISTANCE] = "";
                    drFilaInst[COLUMN_CONCEPT] = "";
                    drFilaInst[COLUMN_VALUE] = "0";

                    dtInstalaciones.Rows.Add(drFilaInst);
                }
                foreach (DataRow drFacilidad in drFacilidades)
                {
                    DataRow drFilaFac = dtFacilities.NewRow();
                    drFilaFac[COLUMN_HOTEL_CODE] = idHotel;
                    drFilaFac[COLUMN_FACILITIES_CODE] = drFacilidad[COLUMN_FACILITIES_CODE].ToString();
                    drFilaFac[COLUMN_FACILITIES] = drFacilidad[COLUMN_FACILITIES_DESCRIPTION].ToString();
                    drFilaFac[COLUMN_NAME_GROUP] = "Comodidades";
                    drFilaFac[COLUMN_CAR_DISTANCE] = "";
                    drFilaFac[COLUMN_CONCEPT] = "";
                    drFilaFac[COLUMN_VALUE] = "0";

                    dtFacilities.Rows.Add(drFilaFac);
                }
                foreach (DataRow drFilaImage in drImagenes)
                {
                    DataRow drFila = dtImagen.NewRow();
                    drFila[COLUMN_IMAGE_NAME] = drFilaImage[COLUMN_FACILITIES_DESCRIPTION].ToString();
                    drFila[COLUMN_IMAGEPATH] = drFilaImage[COLUMN_FACILITIES_IMAGEPATH].ToString();

                    dtImagen.Rows.Add(drFila);
                }
                dsHotelBedsFacilities.Tables.Add(dtInstalaciones);
                dsHotelBedsFacilities.Tables.Add(dtFacilities);
                dsHotelBedsFacilities.Tables.Add(dtImagen);
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
            return dsHotelBedsFacilities;
        }
        private DataTable setRoom(DataSet dsResultados)
        {
            string sWhere = string.Empty;
            DataTable dtRoomHabit = dsResultados.Tables[TABLA_ROOM_HAB];
            DataTable dtRoomPrice = dsResultados.Tables[TABLA_ROOM_PRICE];
            DataTable dtRoomProceAdic = dsResultados.Tables[TABLA_ROOM_PRICE_ADIC];
            DataTable dtHotels = dsResultados.Tables[TABLA_HOTEL];

            DataTable dtRoom = new DataTable(TABLA_ROOM);

            dtRoom = dtRoomHabit.Clone();

            DataColumn dcCurrency = new DataColumn(ROOM_CURRENCY);
            DataColumn dcPrice = new DataColumn(ROOM_PRICE);
            DataColumn dcPriceAdd = new DataColumn(ROOM_PRICE_ADD);
            DataColumn dcPriceExtra = new DataColumn(ROOM_PRICE_EXTRA);
            DataColumn dcDias = new DataColumn(ROOM_DIAS);
            DataColumn dcPriceTotal = new DataColumn(ROOM_PRICE_TOTAL);

            dtRoom.Columns.Add(dcCurrency);
            dtRoom.Columns.Add(dcPrice);
            dtRoom.Columns.Add(dcPriceAdd);
            dtRoom.Columns.Add(dcPriceExtra);
            dtRoom.Columns.Add(dcDias);
            dtRoom.Columns.Add(dcPriceTotal);

            Utils.Utils oUtilidad = new Utils.Utils();
            string sFecha1 = string.Empty;
            string sFecha2 = string.Empty;

            if (dtHotels.Rows[0]["Inicio"].ToString().Contains("T"))
                sFecha1 = dtHotels.Rows[0]["Inicio"].ToString().Remove(dtHotels.Rows[0]["Inicio"].ToString().IndexOf("T"));
            else
                sFecha1 = dtHotels.Rows[0]["Inicio"].ToString();

            if (dtHotels.Rows[0]["Fin"].ToString().Contains("T"))
                sFecha2 = dtHotels.Rows[0]["Fin"].ToString().Remove(dtHotels.Rows[0]["Fin"].ToString().IndexOf("T"));
            else
                sFecha2 = dtHotels.Rows[0]["Fin"].ToString();


            int iDias = clsValidaciones.CalcularDias(sFecha1, sFecha2);

            int iCount = 9;

            DataRow[] drRooms = dtRoomHabit.Select();

            foreach (DataRow drRoom in drRooms)
            {
                DataRow filaTarifa = dtRoom.NewRow();
                for (int j = 0; j < iCount; j++)
                {
                    try
                    {
                        filaTarifa[j] = drRoom[j];
                    }
                    catch { }
                }
                sWhere = "rph='" + drRoom["rph"].ToString() + "'";


                DataRow[] drRoomsPrice = dtRoomPrice.Select(sWhere);
                foreach (DataRow drRoomPrice in drRoomsPrice)
                {
                    filaTarifa[dcCurrency] = drRoomPrice["codMoneda"];
                    if (drRoomPrice["valor"].ToString().Length.Equals(0))
                    {
                        filaTarifa[dcPrice] = "0";
                        filaTarifa[dcPriceTotal] = "0";
                    }
                    else
                    {
                        filaTarifa[dcPrice] = clsValidaciones.getDecimalRound(drRoomPrice["valor"].ToString()).ToString(FORMATO_NUMEROS);
                        filaTarifa[dcPriceTotal] = clsValidaciones.getDecimalRound(Convert.ToString(clsValidaciones.getDecimalRound(drRoomPrice["valor"].ToString()) * iDias)).ToString(FORMATO_NUMEROS);
                    }
                    filaTarifa[dcDias] = iDias.ToString();
                }

                DataRow[] drRoomsPriceAdd = dtRoomProceAdic.Select(sWhere);
                foreach (DataRow drRoomPriceAdd in drRoomsPriceAdd)
                {
                    if (drRoomPriceAdd["adicionalValorAdulto"].ToString().Length.Equals(0))
                    {
                        filaTarifa[dcPriceAdd] = "0";
                    }
                    else
                    {
                        filaTarifa[dcPriceAdd] = clsValidaciones.getDecimalRound(drRoomPriceAdd["adicionalValorAdulto"].ToString()).ToString(FORMATO_NUMEROS);
                    }
                    if (drRoomPriceAdd["adicionalValorPersonaExtra"].ToString().Length.Equals(0))
                    {
                        filaTarifa[dcPriceExtra] = "0";
                    }
                    else
                    {
                        filaTarifa[dcPriceExtra] = clsValidaciones.getDecimalRound(drRoomPriceAdd["adicionalValorPersonaExtra"].ToString()).ToString(FORMATO_NUMEROS);
                    }
                }

                dtRoom.Rows.Add(filaTarifa);
            }
            return dtRoom;
        }
        public void setTiposHabitacion(Repeater rptTiposHabitacion, DataRow[] drRoom)
        {
            try
            {
                rptTiposHabitacion.DataSource = drRoom;
                rptTiposHabitacion.DataBind();
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        public void setGaleria(DataTable dtHotel, Repeater RptGaleria)
        {
            try
            {
                RptGaleria.DataSource = dtHotel.DefaultView;
                RptGaleria.DataBind();
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        private void setFacility(DataTable dtHotel, Repeater RptComodidadesH)
        {
            try
            {
                RptComodidadesH.DataSource = dtHotel.DefaultView;
                RptComodidadesH.DataBind();
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        private void setFacility(DataSet dsHotel, Repeater RptComodidadesH, Repeater RptComodidadesR)
        {
            try
            {
                RptComodidadesH.DataSource = dsHotel.Tables[0].DefaultView;
                RptComodidadesH.DataBind();

                RptComodidadesR.DataSource = dsHotel.Tables[1].DefaultView;
                RptComodidadesR.DataBind();
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        public void setMapa(Panel pMapa, DataRow drHotel)
        {
            try
            {
                clsMapa cMapa = new clsMapa();
                cMapa.Latitud = drHotel[COLUMN_LATITUDE].ToString();
                cMapa.Longitud = drHotel[COLUMN_LONGITUDE].ToString();
                cMapa.Width = "590";
                cMapa.Height = "350";
                cMapa.Encabezado = drHotel[COLUMN_NAME].ToString();
                cMapa.Mapa = pMapa;
                cMapa.setMapa();
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
        }
        public void setDatosHuespedes(UserControl PageSource, int iHabitacion, clsCache cCache, int sId)
        {
            DataSet dsResultados = clsSesiones.getResultadoHotel();
            string sHabitaciones = "tblHabitacion";
            DataTable tblHabitacion = new DataTable(sHabitaciones);

            try
            {
                string sWhere = COLUMN_SELECCION + "=" + sId + "";
                DataTable dtRoom = dsResultados.Tables[TABLA_HOTEL_ROOM];

                DataRow[] drRooms = dtRoom.Select(sWhere);


                string sseqNum = "seqNum";
                string sNombre = "nombre";
                string sApellido = "apellido";
                string sTelefono = "telefono";

                DataColumn dcseqNum = new DataColumn(sseqNum);
                DataColumn dcnombre = new DataColumn(sNombre);
                DataColumn dcapellido = new DataColumn(sApellido);
                DataColumn dctelefono = new DataColumn(sTelefono);

                tblHabitacion.Columns.Add(dcseqNum);
                tblHabitacion.Columns.Add(dcnombre);
                tblHabitacion.Columns.Add(dcapellido);
                tblHabitacion.Columns.Add(dctelefono);

                int i = 1;
                for (int j = 0; j < iHabitacion; j++)
                {
                    DataRow fila = tblHabitacion.NewRow();
                    fila[sseqNum] = i.ToString();
                    if (j == 0)
                    {
                        fila[sNombre] = cCache.Nombres.ToString();
                        fila[sApellido] = cCache.Apellidos.ToString();
                        fila[sTelefono] = cCache.Telefono.ToString();
                    }
                    tblHabitacion.Rows.Add(fila);
                    i++;
                }

                DataSet dsDatos = new DataSet("dsHabitaciones");
                if (!dsResultados.Tables.Contains(sHabitaciones))
                {
                    dsResultados.Tables.Add(tblHabitacion);
                }
                dsResultados.AcceptChanges();
                clsSesiones.setResultadoHotel(dsResultados);

            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
            }
        }
        public DataSet getHotels(string sCodeCity)
        {

            try
            {
                dsConsulta = cHoteles.ConsultaGetHotels(sCodeCity);
            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex);
            }
            return dsConsulta;
        }
        public DataSet dsZones(string sZone)
        {

            try
            {

                dsConsulta = cHoteles.ConsultadsZones(sZone);
            }
            catch
            {
            }
            return dsConsulta;
        }
        public DataSet setTablaConfirma()
        {
            DataSet dsResultados = clsSesiones.getConfirmaHotel();
            bool bHotel = true;
            try
            {
                if (dsResultados.Tables[TABLA_HOTEL_INFO].Rows[0][COLUMN_WS_SELECT].ToString().Equals(clsValidaciones.GetKeyOrAdd("WS_HOTEL_TT", "WS_TT")))
                {
                    bHotel = false;
                }
                if (dsResultados.Tables[TABLA_HOTEL_INFO].Rows[0][COLUMN_WS_SELECT].ToString().Equals(clsValidaciones.GetKeyOrAdd("WS_HOTEL_TRC", "WS_TRC")))
                {
                    bHotel = false;
                }
            }
            catch { }
            if (bHotel)
            {
                setHotelConfirma(dsResultados);
                setHotelRoomConfirma(dsResultados);
                setHotelPenalConfirma(dsResultados);
                setAditionalRoomConfirma(dsResultados);
            }
            clsSesiones.setConfirmaHotel(dsResultados);
            return dsResultados;
        }
        public void setHotelConfirma(DataSet dsResultados)
        {

            string sURL = string.Empty;
            string sImagenPath = clsValidaciones.RutaImagesGen();
            try
            {
                // Traemos la tabla
                DataTable dtHotelInfo = dsResultados.Tables[TABLA_HOTEL_INFO];

                // Adicionamos las columnas
                dtHotelInfo.Columns.Add(COLUMN_CATEGORY_CODE, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_CATEGORY_TEXT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DESTINATION_CODE, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DESTINATION_NAME, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_ZONE_TEXT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DATE_FROM, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DATE_FROM_FORMAT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DATE_TO, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DATE_TO_FORMAT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DESCRIPTION, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_DESCRIPTION_LONG, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_ADDRESS, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_IMAGEN_URL, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_HOTEL_TELEPHONE_NUMBER, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_WS_SELECT, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_AGENCY_COMISION, typeof(decimal));
                dtHotelInfo.Columns.Add(COLUMN_OBSERVACIONES, typeof(string));
                dtHotelInfo.Columns.Add(COLUMN_IVA, typeof(decimal));
                dtHotelInfo.Columns.Add(COLUMN_NON_REEMBOLSABLE, typeof(int));
                string sWsSelect = clsValidaciones.GetKeyOrAdd("WS_HOTEL_HB", "HOTBED");

                // almacenamos datos
                foreach (DataRow drHotelInfo in dtHotelInfo.Rows)
                {
                    foreach (DataRow drCategory in drHotelInfo.GetChildRows(RELACION_HOTELINFO_CATEGORY))
                    {
                        drHotelInfo[COLUMN_CATEGORY_CODE] = drCategory[COLUMN_CODE];
                        drHotelInfo[COLUMN_CATEGORY_TEXT] = drCategory[COLUMN_CATEGORY_TEXT];
                    }
                    foreach (DataRow drDestination in drHotelInfo.GetChildRows(RELACION_HOTELINFO_DESTINATION))
                    {
                        foreach (DataRow drZoneList in drDestination.GetChildRows(RELACION_DESTINATION_ZONELIST))
                        {
                            foreach (DataRow drZone in drZoneList.GetChildRows(RELACION_ZONELIST_ZONE))
                            {
                                drHotelInfo[COLUMN_ZONE_TEXT] = drZone[COLUMN_ZONE_TEXT];
                                drHotelInfo[COLUMN_DESTINATION_CODE] = drDestination[COLUMN_CODE];
                                drHotelInfo[COLUMN_DESTINATION_NAME] = drDestination[COLUMN_NAME];
                                break;
                            }
                        }
                    }
                    foreach (DataRow drServiceHotel in drHotelInfo.GetParentRows(RELACION_SERVICE_HOTELINFO))
                    {
                        foreach (DataRow drdateFrom in drServiceHotel.GetChildRows(RELACION_SERVICE_DATEFROM))
                        {
                            drHotelInfo[COLUMN_DATE_FROM] = drdateFrom[COLUMN_DATE];
                            drHotelInfo[COLUMN_DATE_FROM_FORMAT] = clsValidaciones.ConverYMDtoYMD(drdateFrom[COLUMN_DATE].ToString());
                        }
                        foreach (DataRow drdateTo in drServiceHotel.GetChildRows(RELACION_SERVICE_DATETO))
                        {
                            drHotelInfo[COLUMN_DATE_TO] = drdateTo[COLUMN_DATE];
                            drHotelInfo[COLUMN_DATE_TO_FORMAT] = clsValidaciones.ConverYMDtoYMD(drdateTo[COLUMN_DATE].ToString());
                        }
                    }
                    drHotelInfo[COLUMN_DESCRIPTION] = sDescription(drHotelInfo[COLUMN_CODE].ToString(), "CAS");
                    drHotelInfo[COLUMN_DESCRIPTION_LONG] = drHotelInfo[COLUMN_DESCRIPTION];
                    try
                    {
                        drHotelInfo[COLUMN_IMAGEN_URL] = dsHotelbedsFacility("CAS", drHotelInfo[COLUMN_CODE].ToString(), true).Tables[0].Rows[0]["ImagePath"].ToString();
                    }
                    catch
                    {
                        drHotelInfo[COLUMN_IMAGEN_URL] = sImagenPath + "spacer.gif";
                    }
                    DataSet dsDatosHotel = dsHotelInfo(drHotelInfo[COLUMN_HOTELCODE].ToString());
                    if (dsDatosHotel != null && dsDatosHotel.Tables.Count > 0)
                    {
                        if (dsDatosHotel.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsDatosHotel.Tables[0].Rows.Count; i++)
                            {
                                if (dsDatosHotel.Tables[0].Rows[i][COLUMN_HOTEL_PHONE_TYPE].ToString() == "phoneHotel")
                                {
                                    drHotelInfo[COLUMN_HOTEL_TELEPHONE_NUMBER] = dsDatosHotel.Tables[0].Rows[i][COLUMN_HOTEL_PHONE_NUMBRE].ToString();
                                }
                            }
                        }
                    }
                    drHotelInfo[COLUMN_WS_SELECT] = sWsSelect;
                    drHotelInfo[COLUMN_NON_REEMBOLSABLE] = 0;
                    drHotelInfo[COLUMN_IVA] = 0;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
            }
        }
        public void setHotelRoomConfirma(DataSet dsResultados)
        {
            try
            {
                // Traemos la tabla
                DataTable dtHotelRoom = dsResultados.Tables[TABLA_HOTEL_ROOM];
                DataTable dtCancelation_Pollicy = dsResultados.Tables[TABLA_DATE_TIME_FROM];


                // Adicionamos las columnas
                dtHotelRoom.Columns.Add(COLUMN_ROOM_TYPE_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_TYPE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CHARASTERISTIC, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_AMOUNT, typeof(decimal));
                dtHotelRoom.Columns.Add(COLUMN_AMOUNT_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_SERVICE_ID, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_PURCHASE_STATUS, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_PURCHASE_ID, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_PURCHASE_TOKEN, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_LANGUAJE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_TOTAL_PRICE, typeof(decimal));
                dtHotelRoom.Columns.Add(COLUMN_TOTAL_PRICE_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_AGENCY_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_BRANCH, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_SERVICE_STATUS, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_TOTAL_AMOUNT, typeof(decimal));
                dtHotelRoom.Columns.Add(COLUMN_TOTAL_AMOUNT_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_SPUI, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_BOARD_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_BOARD_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_HOTEL_INFO_ID, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_NAME, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_INCOMING_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CONTRACT_NAME, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CONTRACT_ID, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_HOTEL_INFO_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CURRENCY_TEST, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CANCELATION_POLICY_ID, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_DATE_TIME_FROM, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_TIME, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_TIME_TO, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_TIME_FROM_FORMAT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_TIME_TO_FORMAT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CANCELATION_TOTAL_DAYS, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_CANCELATION_AMOUNT_DAY, typeof(decimal));
                dtHotelRoom.Columns.Add(COLUMN_CANCELATION_AMOUNT_DAY_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CANCELATION_TOTAL_AMOUNT, typeof(decimal));
                dtHotelRoom.Columns.Add(COLUMN_CANCELATION_TOTAL_AMOUNT_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_CANCELATION_PRICE_ID, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_FILE_NUMBER, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_POLITICA, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_COMMENT_TEXT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_VAT_NUMBER, typeof(string));
                DateTime dtFechaPolitica = DateTime.Now.AddDays(1);
                string sFechaPolitica = clsValidaciones.ConverYMDtoDMMY(dtFechaPolitica.ToString(FORMATO_FECHA), "/");
                if (clsValidaciones.GetKeyOrAdd("MonedaView", "FALSE").ToUpper().Equals("TRUE"))
                {
                    dtHotelRoom.Columns.Add(COLUMN_AMOUNT_VIEW_TEXT, typeof(decimal));
                    dtHotelRoom.Columns.Add(COLUMN_CURRENCY_VIEW_TEXT, typeof(string));

                }
                dtHotelRoom.Columns.Add(COLUMN_DATE_FROM, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_FROM_FORMAT, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_TO, typeof(string));
                dtHotelRoom.Columns.Add(COLUMN_DATE_TO_FORMAT, typeof(string));

                dtHotelRoom.Columns.Add(COLUMN_HOTEL_ROOM_OCUPATION, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_ROOM_COUNT, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_ADULT_COUNT, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_CHILD_COUNT, typeof(int));
                dtHotelRoom.Columns.Add(COLUMN_WS_SELECT, typeof(string));
                string sWsSelect = clsValidaciones.GetKeyOrAdd("WS_HOTEL_HB", "HOTBED");
                // almacenamos datos
                int iCount = 1;
                foreach (DataRow drHotelRoom in dtHotelRoom.Rows)
                {
                    foreach (DataRow drRoomType in drHotelRoom.GetChildRows(RELACION_HOTELROOM_ROOMTYPE))
                    {
                        drHotelRoom[COLUMN_ROOM_TYPE_TEXT] = drRoomType[COLUMN_ROOM_TYPE_TEXT];
                        drHotelRoom[COLUMN_TYPE] = drRoomType[COLUMN_TYPE];
                        drHotelRoom[COLUMN_CODE] = drRoomType[COLUMN_CODE];
                        drHotelRoom[COLUMN_CHARASTERISTIC] = drRoomType[COLUMN_CHARASTERISTIC];
                    }
                    foreach (DataRow drPrice in drHotelRoom.GetChildRows(RELACION_HOTELROOM_PRICE))
                    {
                        decimal dPrice = Convert.ToDecimal(clsValidaciones.getDecimalRound(drPrice[COLUMN_AMOUNT].ToString()));
                        drHotelRoom[COLUMN_AMOUNT] = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(dPrice / Convert.ToDecimal(clsValidaciones.GetKeyOrAdd("incremento")))));
                        drHotelRoom[COLUMN_AMOUNT_TEXT] = Convert.ToDecimal(drHotelRoom[COLUMN_AMOUNT].ToString()).ToString(FORMATO_NUMEROS_SD);//dPrice.ToString(FORMATO_NUMEROS_SD);
                    }
                    foreach (DataRow drBoard in drHotelRoom.GetChildRows(RELACION_HOTELROOM_BOARD))
                    {
                        drHotelRoom[COLUMN_BOARD_TEXT] = drBoard[COLUMN_BOARD_TEXT];
                        drHotelRoom[COLUMN_BOARD_CODE] = drBoard[COLUMN_CODE];
                    }
                    foreach (DataRow drAvailableRoom in drHotelRoom.GetParentRows(RELACION_AVAILABLEROOM_HOTELROOM))
                    {
                        foreach (DataRow drHotelOcupancy in drAvailableRoom.GetChildRows(RELACION_AVAILABLEROOM_HOTELOCUPANCY))
                        {
                            drHotelRoom[COLUMN_ROOM_COUNT] = drHotelOcupancy[COLUMN_ROOM_COUNT];
                            foreach (DataRow drOcupancy in drHotelOcupancy.GetChildRows(RELACION_HOTELOCUPANCY_OCUPANCY))
                            {
                                drHotelRoom[COLUMN_ADULT_COUNT] = drOcupancy[COLUMN_ADULT_COUNT];
                                drHotelRoom[COLUMN_CHILD_COUNT] = drOcupancy[COLUMN_CHILD_COUNT];
                            }
                        }
                        foreach (DataRow drService in drAvailableRoom.GetParentRows(RELACION_SERVICE_AVAILABLEROOM))
                        {
                            string sFecha = string.Empty;

                            foreach (DataRow drDateFrom in drService.GetChildRows(RELACION_SERVICE_DATEFROM))
                            {
                                drHotelRoom[COLUMN_DATE_FROM] = drDateFrom[COLUMN_DATE];
                                sFecha = clsValidaciones.ConverYMDtoYMD(drDateFrom[COLUMN_DATE].ToString());
                                drHotelRoom[COLUMN_DATE_FROM_FORMAT] = sFecha;
                            }
                            foreach (DataRow drDateTo in drService.GetChildRows(RELACION_SERVICE_DATETO))
                            {
                                drHotelRoom[COLUMN_DATE_TO] = drDateTo[COLUMN_DATE];
                                sFecha = clsValidaciones.ConverYMDtoYMD(drDateTo[COLUMN_DATE].ToString());
                                drHotelRoom[COLUMN_DATE_TO_FORMAT] = sFecha;
                            }
                            foreach (DataRow drHotelInfo in drService.GetChildRows(RELACION_SERVICE_HOTELINFO))
                            {
                                drHotelRoom[COLUMN_HOTEL_INFO_CODE] = drHotelInfo[COLUMN_CODE];
                                drHotelRoom[COLUMN_HOTEL_INFO_ID] = drHotelInfo[COLUMN_HOTEL_INFO_ID];
                            }
                            foreach (DataRow drCurrency in drService.GetChildRows(RELACION_SERVICE_CURRENCY))
                            {
                                drHotelRoom[COLUMN_CURRENCY_CODE] = drCurrency[COLUMN_CODE];
                                drHotelRoom[COLUMN_CURRENCY_TEST] = drCurrency[COLUMN_CURRENCY_TEST];
                            }
                            foreach (DataRow drSupplier in drService.GetChildRows(RELACION_SERVICE_SUPPLIER))
                            {
                                drHotelRoom[COLUMN_VAT_NUMBER] = drSupplier[COLUMN_VAT_NUMBER];
                            }
                            foreach (DataRow drContractList in drService.GetChildRows(RELACION_SERVICE_CONTRACTLIST))
                            {
                                foreach (DataRow drContract in drContractList.GetChildRows(RELACION_CONTRACTLIST_CONTRACT))
                                {
                                    foreach (DataRow drIncomingOffice in drContract.GetChildRows(RELACION_CONTRACT_INCOMINGOFFICE))
                                    {
                                        drHotelRoom[COLUMN_INCOMING_CODE] = drIncomingOffice[COLUMN_CODE];
                                    }
                                    drHotelRoom[COLUMN_CONTRACT_NAME] = drContract[COLUMN_NAME];
                                    drHotelRoom[COLUMN_CONTRACT_ID] = drContract[COLUMN_CONTRACT_ID];
                                    try
                                    {
                                        foreach (DataRow drCommentList in drContract.GetChildRows(RELACION_CONTRACT_COMMENTLIST))
                                        {
                                            foreach (DataRow drComment in drCommentList.GetChildRows(RELACION_COMMENTLIST_COMMENT))
                                            {
                                                drHotelRoom[COLUMN_COMMENT_TEXT] = drComment[COLUMN_COMMENT_TEXT];
                                            }
                                        }
                                    }
                                    catch { }
                                }
                            }
                            foreach (DataRow drServiceList in drService.GetParentRows(RELACION_SERVICELIST_SERVICE))
                            {
                                foreach (DataRow drPurchase in drServiceList.GetParentRows(RELACION_PURCHASE_SERVICELIST))
                                {
                                    drHotelRoom[COLUMN_PURCHASE_STATUS] = drPurchase[COLUMN_STATUS];
                                    drHotelRoom[COLUMN_PURCHASE_ID] = drPurchase[COLUMN_PURCHASE_ID];
                                    drHotelRoom[COLUMN_LANGUAJE] = drPurchase[COLUMN_LANGUAJE];
                                    decimal dPriceTP = Convert.ToDecimal(clsValidaciones.getDecimalRound(drPurchase[COLUMN_TOTAL_PRICE].ToString()));
                                    drHotelRoom[COLUMN_TOTAL_PRICE] = clsValidaciones.getDecimalRound(Convert.ToString(dPriceTP / Convert.ToDecimal(clsValidaciones.GetKeyOrAdd("Incremento"))));
                                    drHotelRoom[COLUMN_TOTAL_PRICE_TEXT] = Convert.ToDecimal(drHotelRoom[COLUMN_TOTAL_PRICE].ToString()).ToString(FORMATO_NUMEROS_SD);// dPriceTP.ToString(FORMATO_NUMEROS_SD);
                                    drHotelRoom[COLUMN_PURCHASE_TOKEN] = drPurchase[COLUMN_PURCHASE_TOKEN];


                                    if (clsValidaciones.GetKeyOrAdd("MonedaView", "FALSE").ToUpper().Equals("TRUE"))
                                    {
                                        string tasa = sTasasDeCambio(clsValidaciones.GetKeyOrAdd("MonedaviewSimbol", "VEF"), clsValidaciones.GetKeyOrAdd("MonedaDolar", "USD"), drHotelRoom[COLUMN_TOTAL_PRICE].ToString());
                                        drHotelRoom[COLUMN_AMOUNT_VIEW_TEXT] = ((double.Parse(tasa)) * (double.Parse(drHotelRoom[COLUMN_TOTAL_PRICE_TEXT].ToString())));
                                        drHotelRoom[COLUMN_CURRENCY_VIEW_TEXT] = clsValidaciones.GetKeyOrAdd("Monedaviewtext", "VEF");

                                    }

                                    foreach (DataRow drAgency in drPurchase.GetChildRows(RELACION_PURCHASE_AGENCY))
                                    {
                                        drHotelRoom[COLUMN_AGENCY_CODE] = drAgency[COLUMN_CODE];
                                        drHotelRoom[COLUMN_BRANCH] = drAgency[COLUMN_BRANCH];
                                    }
                                    foreach (DataRow drReference in drPurchase.GetChildRows(RELACION_PURCHASE_REFERENCE_))
                                    {
                                        drHotelRoom[COLUMN_FILE_NUMBER] = drReference[COLUMN_FILE_NUMBER];

                                    }
                                }
                            }
                            drHotelRoom[COLUMN_SERVICE_ID] = drService[COLUMN_SERVICE_ID];
                            drHotelRoom[COLUMN_SERVICE_STATUS] = drService[COLUMN_STATUS];
                            drHotelRoom[COLUMN_SPUI] = drService[COLUMN_SPUI];
                            decimal dPriceT = Convert.ToDecimal(clsValidaciones.getDecimalRound(drService[COLUMN_TOTAL_AMOUNT].ToString()));
                            drHotelRoom[COLUMN_TOTAL_AMOUNT] = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(dPriceT / Convert.ToDecimal(clsValidaciones.GetKeyOrAdd("Incremento")))));
                            drHotelRoom[COLUMN_TOTAL_AMOUNT_TEXT] = Convert.ToDecimal(drHotelRoom[COLUMN_TOTAL_AMOUNT].ToString()).ToString(FORMATO_NUMEROS_SD);// dPriceT.ToString(FORMATO_NUMEROS_SD);
                        }
                    }
                    drHotelRoom[COLUMN_POLITICA] = sFechaPolitica;
                    drHotelRoom[COLUMN_HOTEL_ROOM_OCUPATION] = iCount;
                    drHotelRoom[COLUMN_WS_SELECT] = sWsSelect;
                    iCount++;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
            }
        }
        public void setHotelPenalConfirma(DataSet dsResultados)
        {
            try
            {
                //// Traemos la tabla
                DataTable dtCancelationPolicy = dsResultados.Tables[TABLA_CANCELATION_POLICY];

                int iDiasCancel = -3;
                try { iDiasCancel = int.Parse(clsValidaciones.GetKeyOrAdd("DiasPenalizacion", "3")) * -1; }
                catch { }

                // Adicionamos las columnas
                dtCancelationPolicy.Columns.Add(COLUMN_CURRENCY_TEST, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_DATE_TIME_FROM, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_DATE_TIME, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_DATE_TIME_TO, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_DATE_TIME_FROM_FORMAT, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_DATE_TIME_TO_FORMAT, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_CANCELATION_TOTAL_DAYS, typeof(int));
                dtCancelationPolicy.Columns.Add(COLUMN_CANCELATION_AMOUNT_DAY, typeof(decimal));
                dtCancelationPolicy.Columns.Add(COLUMN_CANCELATION_AMOUNT_DAY_TEXT, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_CANCELATION_TOTAL_AMOUNT, typeof(decimal));
                dtCancelationPolicy.Columns.Add(COLUMN_CANCELATION_TOTAL_AMOUNT_TEXT, typeof(string));
                dtCancelationPolicy.Columns.Add(COLUMN_CANCELATION_PRICE_ID, typeof(int));
                if (clsValidaciones.GetKeyOrAdd("MonedaView", "FALSE").ToUpper().Equals("TRUE"))
                {
                    dtCancelationPolicy.Columns.Add(COLUMN_AMOUNT_VIEW_TEXT, typeof(decimal));
                    dtCancelationPolicy.Columns.Add(COLUMN_CURRENCY_VIEW_TEXT, typeof(string));
                }
                // almacenamos datos
                foreach (DataRow drCancelationPolicy in dtCancelationPolicy.Rows)
                {
                    foreach (DataRow drPriceCancelation in drCancelationPolicy.GetChildRows(RELACION_CANCELATIONPOLICY_PRICE))
                    {
                        string sFechaIni = string.Empty;
                        string sFechaFin = string.Empty;
                        drCancelationPolicy[COLUMN_CANCELATION_PRICE_ID] = drPriceCancelation[COLUMN_PRICE_ID];
                        decimal dPriceCP = Convert.ToDecimal(clsValidaciones.getDecimalRound(drPriceCancelation[COLUMN_AMOUNT].ToString()));
                        drCancelationPolicy[COLUMN_CANCELATION_AMOUNT_DAY] = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(dPriceCP / Convert.ToDecimal(clsValidaciones.GetKeyOrAdd("incremento")))));
                        drCancelationPolicy[COLUMN_CANCELATION_AMOUNT_DAY_TEXT] = Convert.ToDecimal(drCancelationPolicy[COLUMN_CANCELATION_AMOUNT_DAY].ToString()).ToString(FORMATO_NUMEROS_SD);//dPriceCP.ToString(FORMATO_NUMEROS_SD);
                        decimal dPriceCPTotal = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(dPriceCP / Convert.ToDecimal(clsValidaciones.GetKeyOrAdd("incremento"))))); //dPriceCP;
                        int Dias = int.Parse(clsValidaciones.GetKeyOrAdd("DiasPenalizacion", "0"));
                        foreach (DataRow drDateTimeFrom in drPriceCancelation.GetChildRows(RELACION_PRICE_DFATETIMEFROM))
                        {
                            drCancelationPolicy[COLUMN_DATE_TIME_FROM] = clsValidaciones.GetKeyOrAdd("DiasPenalizacion", "0") == "0" || DateTime.Parse(clsValidaciones.CalcularFechaDias(drCancelationPolicy[COLUMN_DATE_TIME_FROM_FORMAT].ToString(), -Dias)) < DateTime.Now ? DateTime.Now.ToString("MM/dd/yyyy") : clsValidaciones.ConverYMDtoMDY(clsValidaciones.CalcularFechaDias(drCancelationPolicy[COLUMN_DATE_TIME_FROM_FORMAT].ToString(), -Dias), "/");//clsValidaciones.GetKeyOrAdd("DiasPenalizacion") == "0" ? DateTime.Now.ToString("MM/dd/yyyy") : new Utils.Utils().CalcularFechaDias(new Utils.Utils().ConverYMDtoYMD(drDateTimeFrom[COLUMN_DATE].ToString()), -Dias);
                            drCancelationPolicy[COLUMN_DATE_TIME] = drDateTimeFrom[COLUMN_DATE_TIME];
                            sFechaIni = clsValidaciones.ConverYMDtoYMD(/*drDateTimeFrom[COLUMN_DATE]*/drCancelationPolicy[COLUMN_DATE_TIME_FROM].ToString());
                            try
                            {
                                DateTime dtFechaIni = DateTime.Parse(sFechaIni).AddDays(iDiasCancel);
                                sFechaIni = dtFechaIni.ToString(FORMATO_FECHA);
                            }
                            catch { }
                            drCancelationPolicy[COLUMN_DATE_TIME_FROM_FORMAT] = sFechaIni;
                        }
                        foreach (DataRow drDateTimeTo in drPriceCancelation.GetChildRows(RELACION_PRICE_DFATETIMETO))
                        {
                            drCancelationPolicy[COLUMN_DATE_TIME_TO] = drDateTimeTo[COLUMN_DATE];
                            sFechaFin = clsValidaciones.ConverYMDtoYMD(drDateTimeTo[COLUMN_DATE].ToString());
                            try
                            {
                                DateTime dtFechaFin = DateTime.Parse(sFechaFin).AddDays(iDiasCancel);
                                sFechaFin = dtFechaFin.ToString(FORMATO_FECHA);
                            }
                            catch { }
                            drCancelationPolicy[COLUMN_DATE_TIME_TO_FORMAT] = sFechaFin;
                        }
                        int iDias = 1;
                        try
                        {
                            if (!sFechaIni.Length.Equals(0))
                            {
                                iDias = clsValidaciones.CalcularDias(sFechaIni, sFechaFin);
                                dPriceCPTotal = dPriceCP;
                            }
                        }
                        catch { }
                        drCancelationPolicy[COLUMN_CANCELATION_TOTAL_DAYS] = iDias;
                        drCancelationPolicy[COLUMN_CANCELATION_TOTAL_AMOUNT] = dPriceCPTotal;
                        drCancelationPolicy[COLUMN_CANCELATION_TOTAL_AMOUNT_TEXT] = dPriceCPTotal.ToString(FORMATO_NUMEROS_SD);
                    }
                    foreach (DataRow drHotelRoom in drCancelationPolicy.GetParentRows(RELACION_HOTELROOM_CNCELATIONPOLICY))
                    {
                        drCancelationPolicy[COLUMN_CURRENCY_CODE] = drHotelRoom[COLUMN_CURRENCY_CODE];
                        drCancelationPolicy[COLUMN_CURRENCY_TEST] = drHotelRoom[COLUMN_CURRENCY_TEST];
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
            }
        }
        public void setAditionalRoomConfirma(DataSet dsResultados)
        {
            try
            {
                // Traemos la tabla
                DataTable dtAditionalCost = dsResultados.Tables[TABLA_ADITIONAL_COST];

                // Adicionamos las columnas
                dtAditionalCost.Columns.Add(COLUMN_CURRENCY_TEST, typeof(string));
                dtAditionalCost.Columns.Add(COLUMN_CURRENCY_CODE, typeof(string));
                dtAditionalCost.Columns.Add(COLUMN_PRICE_ID, typeof(int));
                dtAditionalCost.Columns.Add(COLUMN_AMOUNT, typeof(decimal));
                dtAditionalCost.Columns.Add(COLUMN_AMOUNT_TEXT, typeof(string));
                // almacenamos datos
                foreach (DataRow drAditionalCost in dtAditionalCost.Rows)
                {
                    foreach (DataRow drPriceAditional in drAditionalCost.GetChildRows(RELACION_ADITIONALCOST_PRICE))
                    {
                        drAditionalCost[COLUMN_PRICE_ID] = drPriceAditional[COLUMN_PRICE_ID];
                        decimal dPriceCP = Convert.ToDecimal(clsValidaciones.getDecimalRound(drPriceAditional[COLUMN_AMOUNT].ToString()));
                        drAditionalCost[COLUMN_AMOUNT] = Convert.ToDecimal(clsValidaciones.getDecimalRound(Convert.ToString(dPriceCP / Convert.ToDecimal(clsValidaciones.GetKeyOrAdd("incremento")))));
                        drAditionalCost[COLUMN_AMOUNT_TEXT] = Convert.ToDecimal(drAditionalCost[COLUMN_AMOUNT].ToString()).ToString(FORMATO_NUMEROS_SD);// dPriceCP.ToString(FORMATO_NUMEROS_SD);
                    }
                    foreach (DataRow drAditionalCostList in drAditionalCost.GetParentRows(RELACION_ADITIONALCOSTLIST_ADITIONALCOST))
                    {
                        foreach (DataRow drServices in drAditionalCostList.GetParentRows(RELACION_SERVICE_ADITIONALCOSTLIST))
                        {
                            foreach (DataRow drCurrency in drServices.GetChildRows(RELACION_SERVICE_CURRENCY))
                            {
                                drAditionalCost[COLUMN_CURRENCY_CODE] = drCurrency[COLUMN_CODE];
                                drAditionalCost[COLUMN_CURRENCY_TEST] = drCurrency[COLUMN_CURRENCY_TEST];
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Library;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                cMensaje.ViewMessage.Add("No existen resultados con estos parametros");
                cMensaje.Sugerencia.Add("Por favor realice otra búsqueda");
                ExceptionHandled.Publicar(cMensaje);
            }
        }
        public string sDescription(string sHotelCode, string sLenguaje)
        {
            string sDatos = string.Empty;
            try
            {

                dsConsulta = cHoteles.ConsultadsDescription(sHotelCode, sLenguaje);
                sDatos = dsConsulta.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
            }
            return sDatos;
        }
        public DataSet dsHotelbedsFacility(string sIdioma, string idHotel, bool bSoloImagenes)
        {

            try
            {
                dsConsulta = cHoteles.ConsultadsHotelbedsFacility(sIdioma, idHotel, bSoloImagenes);
            }
            catch
            {
            }
            return dsConsulta;
        }
        public DataSet dsHotelbedsFacility(DataSet dtHotelbedsFacility, string idHotel)
        {
            DataTable dtHotelbedsFacilities = dtHotelbedsFacility.Tables[TABLA_FEATURE];
            DataTable dtImagenes = dtHotelbedsFacility.Tables[TABLA_FACILITIES_IMAGEN];
            DataSet dsHotelBedsFacilities = new DataSet();
            try
            {
                DataTable dtFacilities = new DataTable(TABLA_FACILIDADES);
                DataTable dtInstalaciones = new DataTable(TABLA_INSTALACIONES);
                DataTable dtImagen = new DataTable(TABLA_IMAGEN);

                DataColumn dcFHotelCode = new DataColumn(COLUMN_HOTEL_CODE);
                DataColumn dcFCode = new DataColumn(COLUMN_FACILITIES_CODE);
                DataColumn dcFFacilidad = new DataColumn(COLUMN_FACILITIES);
                DataColumn dcFGroup = new DataColumn(COLUMN_NAME_GROUP);
                DataColumn dcFCarDistance = new DataColumn(COLUMN_CAR_DISTANCE);
                DataColumn dcFConcept = new DataColumn(COLUMN_CONCEPT);
                DataColumn dcFValue = new DataColumn(COLUMN_VALUE);

                DataColumn dcIHotelCode = new DataColumn(COLUMN_HOTEL_CODE);
                DataColumn dcICode = new DataColumn(COLUMN_FACILITIES_CODE);
                DataColumn dcIFacilidad = new DataColumn(COLUMN_FACILITIES);
                DataColumn dcIGroup = new DataColumn(COLUMN_NAME_GROUP);
                DataColumn dcICarDistance = new DataColumn(COLUMN_CAR_DISTANCE);
                DataColumn dcIConcept = new DataColumn(COLUMN_CONCEPT);
                DataColumn dcIValue = new DataColumn(COLUMN_VALUE);

                DataColumn dcName = new DataColumn(COLUMN_IMAGE_NAME);
                DataColumn dcImagePath = new DataColumn(COLUMN_IMAGEPATH);

                dtFacilities.Columns.Add(dcFHotelCode);
                dtFacilities.Columns.Add(dcFCode);
                dtFacilities.Columns.Add(dcFFacilidad);
                dtFacilities.Columns.Add(dcFGroup);
                dtFacilities.Columns.Add(dcFCarDistance);
                dtFacilities.Columns.Add(dcFConcept);
                dtFacilities.Columns.Add(dcFValue);

                dtInstalaciones.Columns.Add(dcIHotelCode);
                dtInstalaciones.Columns.Add(dcICode);
                dtInstalaciones.Columns.Add(dcIFacilidad);
                dtInstalaciones.Columns.Add(dcIGroup);
                dtInstalaciones.Columns.Add(dcICarDistance);
                dtInstalaciones.Columns.Add(dcIConcept);
                dtInstalaciones.Columns.Add(dcIValue);

                dtImagen.Columns.Add(dcName);
                dtImagen.Columns.Add(dcImagePath);



                if (dtHotelbedsFacilities != null)
                {
                    DataRow[] drInstalaciones = dtHotelbedsFacilities.Select("group=70");

                    foreach (DataRow drInstalacion in drInstalaciones)
                    {
                        DataRow drFilaInst = dtInstalaciones.NewRow();
                        drFilaInst[COLUMN_HOTEL_CODE] = idHotel;
                        drFilaInst[COLUMN_FACILITIES_CODE] = drInstalacion[COLUMN_FACILITIES_CODE].ToString();
                        if (drInstalacion[7].ToString() == "Y")
                        {
                            drFilaInst[COLUMN_FACILITIES] = drInstalacion[COLUMN_FACILITIES_DESCRIPTION].ToString() + "*";
                        }
                        else
                        {
                            drFilaInst[COLUMN_FACILITIES] = drInstalacion[COLUMN_FACILITIES_DESCRIPTION].ToString();
                        }
                        drFilaInst[COLUMN_NAME_GROUP] = drInstalacion[COLUMN_NAME].ToString();
                        drFilaInst[COLUMN_CAR_DISTANCE] = "";
                        drFilaInst[COLUMN_CONCEPT] = "";
                        drFilaInst[COLUMN_VALUE] = clsValidaciones.getDecimalRound(drInstalacion[COLUMN_VALUE].ToString());

                        dtInstalaciones.Rows.Add(drFilaInst);
                    }
                }
                if (dtHotelbedsFacilities != null)
                {

                    DataRow[] drFacilidades = dtHotelbedsFacilities.Select("group=60");
                    foreach (DataRow drFacilidad in drFacilidades)
                    {
                        DataRow drFilaFac = dtFacilities.NewRow();
                        drFilaFac[COLUMN_HOTEL_CODE] = idHotel;
                        drFilaFac[COLUMN_FACILITIES_CODE] = drFacilidad[COLUMN_FACILITIES_CODE].ToString();
                        if (drFacilidad[7].ToString() == "Y")
                        {
                            drFilaFac[COLUMN_FACILITIES] = drFacilidad[COLUMN_FACILITIES_DESCRIPTION].ToString() + "*";
                        }
                        else
                        {
                            drFilaFac[COLUMN_FACILITIES] = drFacilidad[COLUMN_FACILITIES_DESCRIPTION].ToString();
                        }
                        drFilaFac[COLUMN_NAME_GROUP] = drFacilidad[COLUMN_NAME].ToString();
                        drFilaFac[COLUMN_CAR_DISTANCE] = "";
                        drFilaFac[COLUMN_CONCEPT] = "";
                        drFilaFac[COLUMN_VALUE] = clsValidaciones.getDecimalRound(drFacilidad[COLUMN_VALUE].ToString());

                        dtFacilities.Rows.Add(drFilaFac);
                    }
                }
                foreach (DataRow drFilaImage in dtImagenes.Rows)
                {
                    DataRow drFila = dtImagen.NewRow();
                    drFila[COLUMN_IMAGE_NAME] = drFilaImage[COLUMN_FACILITIES_DESCRIPTION].ToString();
                    drFila[COLUMN_IMAGEPATH] = drFilaImage[COLUMN_FACILITIES_IMAGEPATH].ToString();

                    dtImagen.Rows.Add(drFila);
                }
                dsHotelBedsFacilities.Tables.Add(dtInstalaciones);
                dsHotelBedsFacilities.Tables.Add(dtFacilities);
                dsHotelBedsFacilities.Tables.Add(dtImagen);
            }
            catch
            {
            }
            return dsHotelBedsFacilities;
        }
        private DataSet dsHotelInfo(string HotelCode)
        {
            try
            {
                dsConsulta = cHoteles.ConsultadsHotelInfo(HotelCode);
            }
            catch { }

            return dsConsulta;
        }
        public string sTasasDeCambio(string strCodeOrigen, string strCodeDestino, string strValor)
        {

            string strTasa = null;
            string strEmpresa = "6";

            try
            {
                if (new csCache() != null)
                {
                    strEmpresa = new csCache().cCache().Empresa;
                }

                strCodeOrigen = new CsConsultasVuelos().ConsultaCodigo(strCodeOrigen, "tblMonedas", "intCode", "strCode");
                strCodeDestino = new CsConsultasVuelos().ConsultaCodigo(strCodeDestino, "tblMonedas", "intCode", "strCode");

                dsConsulta = dConsulta.ConsultaTabla("SELECT DBLTASA FROM TBLTASAS INNER JOIN TBLMONEDAS AS MONEDAORIGEN ON MONEDAORIGEN.INTCODE=TBLTASAS.INTMONEDAORIGEN INNER JOIN TBLMONEDAS AS MONEDADESTINO ON MONEDADESTINO.INTCODE=TBLTASAS.INTMONEDADESTINO WHERE INTEMPRESA='" + strEmpresa + "' AND  TBLTASAS.INTMONEDAORIGEN='" + strCodeOrigen + "' AND TBLTASAS.INTMONEDADESTINO='" + strCodeDestino + "'  ORDER BY DTMFECHACREACION DESC");

                if (dsConsulta.Tables.Count > 0)
                {
                    if (dsConsulta.Tables[0].Rows.Count > 0)
                    {
                        strTasa = (Convert.ToDecimal(dsConsulta.Tables[0].Rows[0][0].ToString()) * Convert.ToDecimal(strValor)).ToString();
                    }
                    else
                    {
                        strTasa = null;
                    }
                }
                else
                {
                    strTasa = null;
                }

            }
            catch
            {
                strTasa = null;
            }


            return strTasa;

        }
        public DataTable dsHotelbedsCity(string sIdioma)
        {
            DataTable dtDatos = new DataTable();
            try
            {
                dtDatos = cHoteles.ConsultadsHotelbedsCity(sIdioma);
            }
            catch
            {
                dtDatos = null;
            }
            return dtDatos;
        }
    }
}
