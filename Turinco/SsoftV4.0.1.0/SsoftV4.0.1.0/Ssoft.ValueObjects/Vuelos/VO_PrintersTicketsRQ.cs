//Version 4.0.1.1
using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// Clase para guardar los datos correspondientes a la emision de tiquetes en sabre
    /// </summary>
    /// Autor:          José Faustino Posas
    /// Company:        Ssoft Colombia
    /// Fecha:          2011-10-07
    /// -------------------
    /// Control de Cambios
    /// -------------------
    /// Autor:          
    /// Fecha:          
    /// Descripción:
    public class VO_PrintersTicketsRQ
    {
        #region [ ATRIBUTOS ]
        private string sCountrycode = "CO";
        private string sPrtTicket = string.Empty;
        private string sPrtItinerario = string.Empty;
        private string sPrtInvoice = string.Empty;
        private string sRecord = string.Empty;
        private bool bEmision = true;
        private string sHorarios = string.Empty;
        private string sEndPQNumber = string.Empty;
        private List<string> sPQNumber = new List<string>();
        private decimal dCommisionPercent = 0;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_PrintersTicketsRQ()
        {
        }

        #endregion

        #region [ PROPIEADES ]
        public string Countrycode
        {
            get { return sCountrycode; }
            set { sCountrycode = value; }
        }
        public string PrtTicket
        {
            get { return sPrtTicket; }
            set { sPrtTicket = value; }
        }
        public string PrtItinerario
        {
            get { return sPrtItinerario; }
            set { sPrtItinerario = value; }
        }
        public string PrtInvoice
        {
            get { return sPrtInvoice; }
            set { sPrtInvoice = value; }
        }
        public bool Emision
        {
            get { return bEmision; }
            set { bEmision = value; }
        }
        public string Horarios
        {
            get { return sHorarios; }
            set { sHorarios = value; }
        }
        public string EndPQNumber
        {
            get { return sEndPQNumber; }
            set { sEndPQNumber = value; }
        }
        public List<string> PQNumber
        {
            get { return sPQNumber; }
            set { sPQNumber = value; }
        }
        public decimal CommisionPercent
        {
            get { return dCommisionPercent; }
            set { dCommisionPercent = value; }
        }
        public string Record
        {
            get { return sRecord; }
            set { sRecord = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_PrintersTicketsRQ() { }
        #endregion
    }
}
