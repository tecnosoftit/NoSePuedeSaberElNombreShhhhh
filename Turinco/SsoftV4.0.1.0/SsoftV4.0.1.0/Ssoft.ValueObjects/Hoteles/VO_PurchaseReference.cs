//Version 4.0.1.1
using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    public class VO_PurchaseReference
    {
        #region [ ATRIBUTOS ]
        private VO_Credentials vCredentials;
        private string sFileNumber;
        private string sIncomingOffice;
        private string sComent;
        private List<string> slLista;
        
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_PurchaseReference()
        {
        }

        public VO_PurchaseReference(
            VO_Credentials vCredentials,
            string sFileNumber,
            string sIncomingOffice)
        {
            this.vCredentials = vCredentials;
            this.sFileNumber = sFileNumber;
            this.sIncomingOffice = sIncomingOffice;
        }
        #endregion

        #region [ PROPIEADES ]
        public VO_Credentials Credentials
        {
            get { return vCredentials; }
            set { vCredentials = value; }
        }
        public string FileNumber
        {
            get { return sFileNumber; }
            set { sFileNumber = value; }
        }
        public string IncomingOffice
        {
            get { return sIncomingOffice; }
            set { sIncomingOffice = value; }
        }
        public string Coment
        {
            get { return sComent; }
            set { sComent = value; }
        }
        public List<string> Lista
        {
            get { return slLista; }
            set { slLista = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_PurchaseReference() { }
        #endregion    
    }
}
