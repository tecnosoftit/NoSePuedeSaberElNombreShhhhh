﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.18444.
// 
#pragma warning disable 1591

namespace WS_SsoftTotalTrip.HotelConfirm {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="HotelConfirmServiceSoap", Namespace="http://www.totaltrip.com")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Pax))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(WSSecurity))]
    public partial class HotelConfirmService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HotelConfirmOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public HotelConfirmService() {
            this.Url = global::WS_SsoftTotalTrip.Properties.Settings.Default.WS_SsoftTotalTrip_HotelConfirm_HotelConfirmService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event HotelConfirmCompletedEventHandler HotelConfirmCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.totaltrip.com/HotelConfirm", RequestNamespace="http://www.totaltrip.com", ResponseNamespace="http://www.totaltrip.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public HotelConfirmRS HotelConfirm(HotelConfirmRQ rq) {
            object[] results = this.Invoke("HotelConfirm", new object[] {
                        rq});
            return ((HotelConfirmRS)(results[0]));
        }
        
        /// <remarks/>
        public void HotelConfirmAsync(HotelConfirmRQ rq) {
            this.HotelConfirmAsync(rq, null);
        }
        
        /// <remarks/>
        public void HotelConfirmAsync(HotelConfirmRQ rq, object userState) {
            if ((this.HotelConfirmOperationCompleted == null)) {
                this.HotelConfirmOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHotelConfirmOperationCompleted);
            }
            this.InvokeAsync("HotelConfirm", new object[] {
                        rq}, this.HotelConfirmOperationCompleted, userState);
        }
        
        private void OnHotelConfirmOperationCompleted(object arg) {
            if ((this.HotelConfirmCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HotelConfirmCompleted(this, new HotelConfirmCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public partial class HotelConfirmRQ : WSSecurity {
        
        private HotelFare hotelFareField;
        
        private string languageField;
        
        /// <comentarios/>
        public HotelFare HotelFare {
            get {
                return this.hotelFareField;
            }
            set {
                this.hotelFareField = value;
            }
        }
        
        /// <comentarios/>
        public string Language {
            get {
                return this.languageField;
            }
            set {
                this.languageField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public partial class HotelFare {
        
        private int idField;
        
        private Hotel hotField;
        
        private string tripIdField;
        
        private bool extraNightOptionField;
        
        private string resIdField;
        
        private decimal agencyCommissionField;
        
        private decimal ivaField;
        
        private System.DateTime tTLField;
        
        private HotelRoom[] roomsField;
        
        private ContentType contentTypeField;
        
        private string essentailInfoField;
        
        private string payableByTextField;
        
        /// <comentarios/>
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <comentarios/>
        public Hotel Hot {
            get {
                return this.hotField;
            }
            set {
                this.hotField = value;
            }
        }
        
        /// <comentarios/>
        public string TripId {
            get {
                return this.tripIdField;
            }
            set {
                this.tripIdField = value;
            }
        }
        
        /// <comentarios/>
        public bool ExtraNightOption {
            get {
                return this.extraNightOptionField;
            }
            set {
                this.extraNightOptionField = value;
            }
        }
        
        /// <comentarios/>
        public string ResId {
            get {
                return this.resIdField;
            }
            set {
                this.resIdField = value;
            }
        }
        
        /// <comentarios/>
        public decimal AgencyCommission {
            get {
                return this.agencyCommissionField;
            }
            set {
                this.agencyCommissionField = value;
            }
        }
        
        /// <comentarios/>
        public decimal Iva {
            get {
                return this.ivaField;
            }
            set {
                this.ivaField = value;
            }
        }
        
        /// <comentarios/>
        public System.DateTime TTL {
            get {
                return this.tTLField;
            }
            set {
                this.tTLField = value;
            }
        }
        
        /// <comentarios/>
        public HotelRoom[] Rooms {
            get {
                return this.roomsField;
            }
            set {
                this.roomsField = value;
            }
        }
        
        /// <comentarios/>
        public ContentType ContentType {
            get {
                return this.contentTypeField;
            }
            set {
                this.contentTypeField = value;
            }
        }
        
        /// <comentarios/>
        public string EssentailInfo {
            get {
                return this.essentailInfoField;
            }
            set {
                this.essentailInfoField = value;
            }
        }
        
        /// <comentarios/>
        public string PayableByText {
            get {
                return this.payableByTextField;
            }
            set {
                this.payableByTextField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public partial class Hotel {
        
        private string addressField;
        
        private string chainCodeField;
        
        private int hotelIdField;
        
        private int hotelCodeField;
        
        private string hotelNameField;
        
        private int sourceField;
        
        private string phoneField;
        
        private string descriptionField;
        
        private string longDescriptionField;
        
        private string propertyMessageField;
        
        private string cityCodeField;
        
        private string ratingField;
        
        private string latitudeField;
        
        private string longitudeField;
        
        private string localityField;
        
        private string neighborhoodField;
        
        private string area1Field;
        
        private string area2Field;
        
        private int confidenceFactorField;
        
        private string[] ammenitiesField;
        
        private string[] imagesField;
        
        /// <comentarios/>
        public string Address {
            get {
                return this.addressField;
            }
            set {
                this.addressField = value;
            }
        }
        
        /// <comentarios/>
        public string ChainCode {
            get {
                return this.chainCodeField;
            }
            set {
                this.chainCodeField = value;
            }
        }
        
        /// <comentarios/>
        public int HotelId {
            get {
                return this.hotelIdField;
            }
            set {
                this.hotelIdField = value;
            }
        }
        
        /// <comentarios/>
        public int HotelCode {
            get {
                return this.hotelCodeField;
            }
            set {
                this.hotelCodeField = value;
            }
        }
        
        /// <comentarios/>
        public string HotelName {
            get {
                return this.hotelNameField;
            }
            set {
                this.hotelNameField = value;
            }
        }
        
        /// <comentarios/>
        public int Source {
            get {
                return this.sourceField;
            }
            set {
                this.sourceField = value;
            }
        }
        
        /// <comentarios/>
        public string Phone {
            get {
                return this.phoneField;
            }
            set {
                this.phoneField = value;
            }
        }
        
        /// <comentarios/>
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <comentarios/>
        public string LongDescription {
            get {
                return this.longDescriptionField;
            }
            set {
                this.longDescriptionField = value;
            }
        }
        
        /// <comentarios/>
        public string PropertyMessage {
            get {
                return this.propertyMessageField;
            }
            set {
                this.propertyMessageField = value;
            }
        }
        
        /// <comentarios/>
        public string CityCode {
            get {
                return this.cityCodeField;
            }
            set {
                this.cityCodeField = value;
            }
        }
        
        /// <comentarios/>
        public string Rating {
            get {
                return this.ratingField;
            }
            set {
                this.ratingField = value;
            }
        }
        
        /// <comentarios/>
        public string Latitude {
            get {
                return this.latitudeField;
            }
            set {
                this.latitudeField = value;
            }
        }
        
        /// <comentarios/>
        public string Longitude {
            get {
                return this.longitudeField;
            }
            set {
                this.longitudeField = value;
            }
        }
        
        /// <comentarios/>
        public string Locality {
            get {
                return this.localityField;
            }
            set {
                this.localityField = value;
            }
        }
        
        /// <comentarios/>
        public string Neighborhood {
            get {
                return this.neighborhoodField;
            }
            set {
                this.neighborhoodField = value;
            }
        }
        
        /// <comentarios/>
        public string Area1 {
            get {
                return this.area1Field;
            }
            set {
                this.area1Field = value;
            }
        }
        
        /// <comentarios/>
        public string Area2 {
            get {
                return this.area2Field;
            }
            set {
                this.area2Field = value;
            }
        }
        
        /// <comentarios/>
        public int ConfidenceFactor {
            get {
                return this.confidenceFactorField;
            }
            set {
                this.confidenceFactorField = value;
            }
        }
        
        /// <comentarios/>
        public string[] Ammenities {
            get {
                return this.ammenitiesField;
            }
            set {
                this.ammenitiesField = value;
            }
        }
        
        /// <comentarios/>
        public string[] Images {
            get {
                return this.imagesField;
            }
            set {
                this.imagesField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public partial class HotelConfirmRS {
        
        private HotelFare hotelFareField;
        
        /// <comentarios/>
        public HotelFare HotelFare {
            get {
                return this.hotelFareField;
            }
            set {
                this.hotelFareField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(HotelRoomPax))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public partial class Pax {
        
        private int ageField;
        
        private PaxType paxTypeField;
        
        /// <comentarios/>
        public int Age {
            get {
                return this.ageField;
            }
            set {
                this.ageField = value;
            }
        }
        
        /// <comentarios/>
        public PaxType PaxType {
            get {
                return this.paxTypeField;
            }
            set {
                this.paxTypeField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public enum PaxType {
        
        /// <comentarios/>
        Adult,
        
        /// <comentarios/>
        Child,
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public partial class HotelRoomPax : Pax {
        
        private string firstNameField;
        
        private string lastNameField;
        
        private string emailAddressField;
        
        private string phoneNumberField;
        
        private string docNumberField;
        
        private string nationalityField;
        
        private string residencyField;
        
        /// <comentarios/>
        public string FirstName {
            get {
                return this.firstNameField;
            }
            set {
                this.firstNameField = value;
            }
        }
        
        /// <comentarios/>
        public string LastName {
            get {
                return this.lastNameField;
            }
            set {
                this.lastNameField = value;
            }
        }
        
        /// <comentarios/>
        public string EmailAddress {
            get {
                return this.emailAddressField;
            }
            set {
                this.emailAddressField = value;
            }
        }
        
        /// <comentarios/>
        public string PhoneNumber {
            get {
                return this.phoneNumberField;
            }
            set {
                this.phoneNumberField = value;
            }
        }
        
        /// <comentarios/>
        public string DocNumber {
            get {
                return this.docNumberField;
            }
            set {
                this.docNumberField = value;
            }
        }
        
        /// <comentarios/>
        public string Nationality {
            get {
                return this.nationalityField;
            }
            set {
                this.nationalityField = value;
            }
        }
        
        /// <comentarios/>
        public string Residency {
            get {
                return this.residencyField;
            }
            set {
                this.residencyField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public partial class CancelationPolicy {
        
        private OffsetDropTime offsetDropTimeField;
        
        private int multiplierField;
        
        private OffsetTimeUnit offsetTimeUnitField;
        
        private decimal amountField;
        
        private decimal taxAmountField;
        
        private string currencyField;
        
        private bool feesInclusiveField;
        
        private int nmbrOfNightsField;
        
        /// <comentarios/>
        public OffsetDropTime OffsetDropTime {
            get {
                return this.offsetDropTimeField;
            }
            set {
                this.offsetDropTimeField = value;
            }
        }
        
        /// <comentarios/>
        public int Multiplier {
            get {
                return this.multiplierField;
            }
            set {
                this.multiplierField = value;
            }
        }
        
        /// <comentarios/>
        public OffsetTimeUnit OffsetTimeUnit {
            get {
                return this.offsetTimeUnitField;
            }
            set {
                this.offsetTimeUnitField = value;
            }
        }
        
        /// <comentarios/>
        public decimal Amount {
            get {
                return this.amountField;
            }
            set {
                this.amountField = value;
            }
        }
        
        /// <comentarios/>
        public decimal TaxAmount {
            get {
                return this.taxAmountField;
            }
            set {
                this.taxAmountField = value;
            }
        }
        
        /// <comentarios/>
        public string Currency {
            get {
                return this.currencyField;
            }
            set {
                this.currencyField = value;
            }
        }
        
        /// <comentarios/>
        public bool FeesInclusive {
            get {
                return this.feesInclusiveField;
            }
            set {
                this.feesInclusiveField = value;
            }
        }
        
        /// <comentarios/>
        public int NmbrOfNights {
            get {
                return this.nmbrOfNightsField;
            }
            set {
                this.nmbrOfNightsField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public enum OffsetDropTime {
        
        /// <comentarios/>
        BeforeArrival,
        
        /// <comentarios/>
        AfterBooking,
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public enum OffsetTimeUnit {
        
        /// <comentarios/>
        Year,
        
        /// <comentarios/>
        Month,
        
        /// <comentarios/>
        Week,
        
        /// <comentarios/>
        Day,
        
        /// <comentarios/>
        Hour,
        
        /// <comentarios/>
        Second,
        
        /// <comentarios/>
        FullDuration,
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public partial class EffectiveDate {
        
        private System.DateTime dateField;
        
        private decimal amountBeforeTaxField;
        
        private decimal amountAfterTaxField;
        
        private decimal discountBeforeTaxField;
        
        private decimal discountAfterTaxField;
        
        private string currencyField;
        
        private string providerCurrencyField;
        
        private decimal rOEField;
        
        /// <comentarios/>
        public System.DateTime Date {
            get {
                return this.dateField;
            }
            set {
                this.dateField = value;
            }
        }
        
        /// <comentarios/>
        public decimal AmountBeforeTax {
            get {
                return this.amountBeforeTaxField;
            }
            set {
                this.amountBeforeTaxField = value;
            }
        }
        
        /// <comentarios/>
        public decimal AmountAfterTax {
            get {
                return this.amountAfterTaxField;
            }
            set {
                this.amountAfterTaxField = value;
            }
        }
        
        /// <comentarios/>
        public decimal DiscountBeforeTax {
            get {
                return this.discountBeforeTaxField;
            }
            set {
                this.discountBeforeTaxField = value;
            }
        }
        
        /// <comentarios/>
        public decimal DiscountAfterTax {
            get {
                return this.discountAfterTaxField;
            }
            set {
                this.discountAfterTaxField = value;
            }
        }
        
        /// <comentarios/>
        public string Currency {
            get {
                return this.currencyField;
            }
            set {
                this.currencyField = value;
            }
        }
        
        /// <comentarios/>
        public string ProviderCurrency {
            get {
                return this.providerCurrencyField;
            }
            set {
                this.providerCurrencyField = value;
            }
        }
        
        /// <comentarios/>
        public decimal ROE {
            get {
                return this.rOEField;
            }
            set {
                this.rOEField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public partial class HotelRoomType {
        
        private string roomTypeField;
        
        private string roomRateField;
        
        private string ratePlanTypeField;
        
        private string roomDescField;
        
        private string invBlockCodeField;
        
        private decimal amountIncludedBoardSurchargeField;
        
        private System.Nullable<System.DateTime> tTLField;
        
        private EffectiveDate[] effectiveDatesField;
        
        private string roomPromoDescField;
        
        /// <comentarios/>
        public string RoomType {
            get {
                return this.roomTypeField;
            }
            set {
                this.roomTypeField = value;
            }
        }
        
        /// <comentarios/>
        public string RoomRate {
            get {
                return this.roomRateField;
            }
            set {
                this.roomRateField = value;
            }
        }
        
        /// <comentarios/>
        public string RatePlanType {
            get {
                return this.ratePlanTypeField;
            }
            set {
                this.ratePlanTypeField = value;
            }
        }
        
        /// <comentarios/>
        public string RoomDesc {
            get {
                return this.roomDescField;
            }
            set {
                this.roomDescField = value;
            }
        }
        
        /// <comentarios/>
        public string InvBlockCode {
            get {
                return this.invBlockCodeField;
            }
            set {
                this.invBlockCodeField = value;
            }
        }
        
        /// <comentarios/>
        public decimal AmountIncludedBoardSurcharge {
            get {
                return this.amountIncludedBoardSurchargeField;
            }
            set {
                this.amountIncludedBoardSurchargeField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> TTL {
            get {
                return this.tTLField;
            }
            set {
                this.tTLField = value;
            }
        }
        
        /// <comentarios/>
        public EffectiveDate[] EffectiveDates {
            get {
                return this.effectiveDatesField;
            }
            set {
                this.effectiveDatesField = value;
            }
        }
        
        /// <comentarios/>
        public string RoomPromoDesc {
            get {
                return this.roomPromoDescField;
            }
            set {
                this.roomPromoDescField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public partial class HotelRoom {
        
        private HotelRoomType[] roomTypesField;
        
        private CancelationPolicy[] cancelationPoliciesField;
        
        private HotelRoomPax[] paxesField;
        
        /// <comentarios/>
        public HotelRoomType[] RoomTypes {
            get {
                return this.roomTypesField;
            }
            set {
                this.roomTypesField = value;
            }
        }
        
        /// <comentarios/>
        public CancelationPolicy[] CancelationPolicies {
            get {
                return this.cancelationPoliciesField;
            }
            set {
                this.cancelationPoliciesField = value;
            }
        }
        
        /// <comentarios/>
        public HotelRoomPax[] Paxes {
            get {
                return this.paxesField;
            }
            set {
                this.paxesField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(HotelConfirmRQ))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public abstract partial class WSSecurity {
        
        private string usernameField;
        
        private string passwordField;
        
        /// <comentarios/>
        public string Username {
            get {
                return this.usernameField;
            }
            set {
                this.usernameField = value;
            }
        }
        
        /// <comentarios/>
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.totaltrip.com")]
    public enum ContentType {
        
        /// <comentarios/>
        Exclusive,
        
        /// <comentarios/>
        NonExclusive,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void HotelConfirmCompletedEventHandler(object sender, HotelConfirmCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HotelConfirmCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HotelConfirmCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public HotelConfirmRS Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((HotelConfirmRS)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591