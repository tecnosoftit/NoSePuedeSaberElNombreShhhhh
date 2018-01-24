using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ssoft.Utils;
using Ssoft.ValueObjects;

namespace Ssoft.Ssoft.ValueObjects.Hoteles
{
    public class VO_HotelValuedAvailRQ
    {
        #region [ ATRIBUTOS ]
        private VO_Credentials vCredentials;
        private string sPaginationData;
        private int iCurrentPage;
        private string sCheckInDate;
        private string sCheckOutDate;
        private string sDestination;
        private List<VO_HotelOccupancy> vlHotelOccupancy;
        private int iTotalRoom;
        private int iTotalAdult;
        private int iTotalChild;
        private int iTotalInf;
        private int iTotalJunior;
        private int iTotalNights;
        private string sCodCiudad;
        private string sCodCiudadOrigen;
        private string sDetalleCiudad;
        private string sCodHotel;
        private string sCodPlan;
        private Enum_TipoPlan eTipoPlan;
        private string sDocumento;
        private string sClase;
        private Enum_MiembroSemper eMiembroSemper;
        private string sTipoalimentacion;
        private string sZone;
        private Enum_TypeZone eType;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_HotelValuedAvailRQ()
        {
            this.iTotalRoom = 1;
            this.iTotalAdult = 1;
            this.iTotalChild = 0;
            this.iTotalInf = 0;
            this.iCurrentPage = 0;
        }

        public VO_HotelValuedAvailRQ(
            VO_Credentials vCredentials,
            string sPaginationData,
            string sCheckInDate,
            string sCheckOutDate,
            string sDestination,
            List<VO_HotelOccupancy> vlHotelOccupancy)
        {
            this.vCredentials = vCredentials;
            this.sPaginationData = sPaginationData;
            this.sCheckInDate = sCheckInDate;
            this.sCheckOutDate = sCheckOutDate;
            this.sDestination = sDestination;
            this.vlHotelOccupancy = vlHotelOccupancy;
        }
        #endregion

        #region [ PROPIEADES ]
        public VO_Credentials Credentials
        {
            get { return vCredentials; }
            set { vCredentials = value; }
        }
        public string PaginationData
        {
            get { return sPaginationData; }
            set { sPaginationData = value; }
        }
        public int CurrentPage
        {
            get { return iCurrentPage; }
            set { iCurrentPage = value; }
        }
        public string CheckInDate
        {
            get { return sCheckInDate; }
            set { sCheckInDate = value; }
        }
        public string CheckOutDate
        {
            get { return sCheckOutDate; }
            set { sCheckOutDate = value; }
        }
        public string Destination
        {
            get { return sDestination; }
            set { sDestination = value; }
        }
        public List<VO_HotelOccupancy> lHotelOccupancy
        {
            get { return vlHotelOccupancy; }
            set { vlHotelOccupancy = value; }
        }
        public int TotalRoom
        {
            get { return iTotalRoom; }
            set { iTotalRoom = value; }
        }
        public int TotalAdult
        {
            get { return iTotalAdult; }
            set { iTotalAdult = value; }
        }
        public int TotalChild
        {
            get { return iTotalChild; }
            set { iTotalChild = value; }
        }
        public int TotalInf
        {
            get { return iTotalInf; }
            set { iTotalInf = value; }
        }
        public int TotalJunior
        {
            get { return iTotalJunior; }
            set { iTotalJunior = value; }
        }
        public int TotalNights
        {
            get { return iTotalNights; }
            set { iTotalNights = value; }
        }
        public string CodCiudad
        {
            get { return sCodCiudad; }
            set { sCodCiudad = value; }
        }
        public string CodCiudadOrigen
        {
            get { return sCodCiudadOrigen; }
            set { sCodCiudadOrigen = value; }
        }
        public string DetalleCiudad
        {
            get { return sDetalleCiudad; }
            set { sDetalleCiudad = value; }
        }
        public string CodHotel
        {
            get { return sCodHotel; }
            set { sCodHotel = value; }
        }
        public string CodPlan
        {
            get { return sCodPlan; }
            set { sCodPlan = value; }
        }
        public Enum_TipoPlan TipoPlan
        {
            get { return eTipoPlan; }
            set { eTipoPlan = value; }
        }
        public string Documento
        {
            get { return sDocumento; }
            set { sDocumento = value; }
        }
        public string Clase
        {
            get { return sClase; }
            set { sClase = value; }
        }
        public Enum_MiembroSemper MiembroSemper
        {
            get { return eMiembroSemper; }
            set { eMiembroSemper = value; }
        }
        public string TipoAlimentacion
        {
            get { return sTipoalimentacion; }
            set { sTipoalimentacion = value; }
        }
        public string Zone
        {
            get { return sZone; }
            set { sZone = value; }
        }
        public Enum_TypeZone Type
        {
            get { return eType; }
            set { eType = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_HotelValuedAvailRQ() { }
        #endregion
    }
}
