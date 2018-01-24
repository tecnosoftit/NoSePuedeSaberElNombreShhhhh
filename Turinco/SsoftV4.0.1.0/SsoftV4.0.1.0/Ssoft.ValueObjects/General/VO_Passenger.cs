using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;

namespace Ssoft.ValueObjects
{
    public class VO_Passenger
    {
        #region [ ATRIBUTOS ]

        private string sPos;
        private string sAdulto;
        private string sNino;
        private string sInfante;
        private string sJunnior = "0";



        private List<clsEdad> cEdad;
        private string sRespTipo;
        private string sRespTrato;
        private string sRespNombre;
        private string sRespApellido;
        private string sRespTelefono;
        private string sRespCelular;
        private string sRespEmail;
        private string sRespTipoDoc;
        private string sRespDocumento;
        private string sRespGenero;
        private string sRespFechaNac;
        private string sRespFrecuente;
        private string sRespAirLine;
        private string sPreferencias;
        private bool bActivo;
        private int iRoomCount;

        #endregion

        #region [ CONSTRUCTOR ]

        public VO_Passenger()
        {
        }

        #endregion

        #region [ PROPIEADES ]
        public string Pos
        {
            get { return sPos; }
            set { sPos = value; }
        }
        public string Adulto
        {
            get { return sAdulto; }
            set { sAdulto = value; }
        }
        public string Nino
        {
            get { return sNino; }
            set { sNino = value; }
        }
        public string Infante
        {
            get { return sInfante; }
            set { sInfante = value; }
        }
        public string Junnior
        {
            get { return sJunnior; }
            set { sJunnior = value; }
        }
        public List<clsEdad> Edad
        {
            get { return cEdad; }
            set { cEdad = value; }
        }
        public string RespTipo
        {
            get { return sRespTipo; }
            set { sRespTipo = value; }
        }
        public string RespTrato
        {
            get { return sRespTrato; }
            set { sRespTrato = value; }
        }
        public string RespNombre
        {
            get { return sRespNombre; }
            set { sRespNombre = value; }
        }
        public string RespApellido
        {
            get { return sRespApellido; }
            set { sRespApellido = value; }
        }
        public string RespTelefono
        {
            get { return sRespTelefono; }
            set { sRespTelefono = value; }
        }
        public string RespCelular
        {
            get { return sRespCelular; }
            set { sRespCelular = value; }
        }
        public string RespEmail
        {
            get { return sRespEmail; }
            set { sRespEmail = value; }
        }
        public string RespTipoDoc
        {
            get { return sRespTipoDoc; }
            set { sRespTipoDoc = value; }
        }
        public string RespDocumento
        {
            get { return sRespDocumento; }
            set { sRespDocumento = value; }
        }
        public string RespFrecuente
        {
            get { return sRespFrecuente; }
            set { sRespFrecuente = value; }
        }
        public string RespAirLine
        {
            get { return sRespAirLine; }
            set { sRespAirLine = value; }
        }
        public string RespFechaNac
        {
            get { return sRespFechaNac; }
            set { sRespFechaNac = value; }
        }
        public string RespGenero
        {
            get { return sRespGenero; }
            set { sRespGenero = value; }
        }
        public string Preferencias
        {
            get { return sPreferencias; }
            set { sPreferencias = value; }
        }

        public bool Activo
        {
            get { return bActivo; }
            set { bActivo = value; }
        }

        public int RoomCount
        {
            get { return iRoomCount; }
            set { iRoomCount = value; }
        }

        #endregion

        #region [ DESTRUCTOR ]
        ~VO_Passenger() { }
        #endregion
    }
    public class clsEdad
    {
        #region [ ATRIBUTOS ]

        private string sPos;
        private Enum_TipoPassenger eTipo;
        private Enum_TipoEdad eClase;
        private string sEdad;

        #endregion

        #region [ CONSTRUCTOR ]

        public clsEdad()
        {
        }

        #endregion

        #region [ PROPIEADES ]
        public string Pos
        {
            get { return sPos; }
            set { sPos = value; }
        }
        public Enum_TipoPassenger Tipo
        {
            get { return eTipo; }
            set { eTipo = value; }
        }
        public Enum_TipoEdad Clase
        {
            get { return eClase; }
            set { eClase = value; }
        }
        public string Edad
        {
            get { return sEdad; }
            set { sEdad = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~clsEdad() { }
        #endregion
    }
    public class clsContarPax
    {
        #region [ ATRIBUTOS ]

        private int iRooms;
        private int iAdultos;
        private int iNinos;
        private int iInfantes;
        private int iPaxs;
        #endregion

        #region [ CONSTRUCTOR ]

        public clsContarPax()
        {
            List<VO_Passenger> lvo_Passenger = clsSesiones.getPassenger();
            this.Rooms = 0;
            this.Adultos = 0;
            this.Ninos = 0;
            this.Infantes = 0;
            this.Paxs = 0;
            int iPos = lvo_Passenger.Count;

            for (int i = 0; i < iPos; i++)
            {
                this.Rooms++;
                try { this.Adultos += int.Parse(lvo_Passenger[i].Adulto); }
                catch { }
                try { this.Ninos += int.Parse(lvo_Passenger[i].Nino); }
                catch { }
                try { this.Infantes += int.Parse(lvo_Passenger[i].Infante); }
                catch { }
            }
            this.Paxs = this.Adultos + this.Ninos + this.Infantes;
        }

        #endregion

        #region [ PROPIEADES ]
        public int Rooms
        {
            get { return iRooms; }
            set { iRooms = value; }
        }
        public int Adultos
        {
            get { return iAdultos; }
            set { iAdultos = value; }
        }
        public int Ninos
        {
            get { return iNinos; }
            set { iNinos = value; }
        }
        public int Infantes
        {
            get { return iInfantes; }
            set { iInfantes = value; }
        }
        public int Paxs
        {
            get { return iPaxs; }
            set { iPaxs = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~clsContarPax() { }
        #endregion
    }
}
