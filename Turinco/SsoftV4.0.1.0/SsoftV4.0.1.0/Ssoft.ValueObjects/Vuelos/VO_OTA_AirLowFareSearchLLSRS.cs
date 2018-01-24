using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// Contiene el response del OTA_AirLowFareSearch
    /// </summary>
    public class VO_OTA_AirLowFareSearchLLSRS
    {
        #region [ ATRIBUTOS ]
        private string sRPH;
        private List<VO_SegmentoAereo> lvo_SegmentoAereo;
        private VO_Impuesto vo_TarifaTotalSabre;
        private VO_Impuesto vo_TarifaTotal;
        private VO_TA vo_TA;
        private List<VO_DetalleTarifa> lvo_DetalleTarifa;
        private List<VO_ReglasTarifa> lvo_ReglasTarifa;


        #endregion

        #region [ CONSTRUCTOR ]
        public VO_OTA_AirLowFareSearchLLSRS()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #endregion

        #region [ PROPIEADES ]
        public string SRPH
        {
            get { return sRPH; }
            set { sRPH = value; }
        }
        public List<VO_SegmentoAereo> Lvo_SegmentoAereo
        {
            get { return lvo_SegmentoAereo; }
            set { lvo_SegmentoAereo = value; }
        }
        public VO_Impuesto Vo_TarifaTotalSabre
        {
            get { return vo_TarifaTotalSabre; }
            set { vo_TarifaTotalSabre = value; }
        }
        public VO_Impuesto Vo_TarifaTotal
        {
            get { return vo_TarifaTotal; }
            set { vo_TarifaTotal = value; }
        }
        public VO_TA Vo_TA
        {
            get { return vo_TA; }
            set { vo_TA = value; }
        }
        public List<VO_DetalleTarifa> Lvo_DetalleTarifa
        {
            get { return lvo_DetalleTarifa; }
            set { lvo_DetalleTarifa = value; }
        }
        public List<VO_ReglasTarifa> Lvo_ReglasTarifa
        {
            get { return lvo_ReglasTarifa; }
            set { lvo_ReglasTarifa = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_OTA_AirLowFareSearchLLSRS() { }
        #endregion

    }
}
