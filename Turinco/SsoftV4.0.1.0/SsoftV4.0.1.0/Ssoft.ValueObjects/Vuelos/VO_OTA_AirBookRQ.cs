using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;

namespace Ssoft.ValueObjects
{
    public class VO_OTA_AirBookRQ
    {
        #region [ ATRIBUTOS ]

        private int iRutaActual;//Rutas a cotizar
        private List<VO_OrigenDestinationOption> lvo_OrigenDestinationOption;//Rutas a cotizar
        private string sTipoWs;//Tipo Ws Tame o Sabre

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_OTA_AirBookRQ()
        {
        }

        #endregion

        #region [ PROPIEADES ]

        public int IRutaActual
        {
            get { return iRutaActual; }
            set { iRutaActual = value; }
        }
        public List<VO_OrigenDestinationOption> Lvo_OrigenDestinationOption
        {
            get { return lvo_OrigenDestinationOption; }
            set { lvo_OrigenDestinationOption = value; }
        }
        public string TipoWs
        {
            get { return sTipoWs; }
            set { sTipoWs = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_OTA_AirBookRQ() { }
        #endregion
    }
}
