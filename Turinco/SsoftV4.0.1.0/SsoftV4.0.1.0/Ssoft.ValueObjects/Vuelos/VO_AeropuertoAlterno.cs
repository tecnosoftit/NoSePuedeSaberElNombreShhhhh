using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// permite especificar un una lista de aeropuertos opciopnales para una ciudad determinada
    /// </summary>
    public class VO_AeropuertoAlterno
    {

        #region [ ATRIBUTOS ]

        private string sSegmento;
        private VO_Aeropuerto vo_AeropuertoBase;
        private List<VO_Aeropuerto> lvo_AeropuertoesAlternativas;
        private string sMillaje;

        #endregion

        #region [ CONSTRUCTOR ]

        public VO_AeropuertoAlterno()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sSegmento">La linea especifica de la busqueda.</param>
        /// <param name="vo_AeropuertoBase">ciudad que permitira el cambio de ciudad.</param>
        /// <param name="lvo_AeropuertoesAlternativas">aeropuertos permitidos en la busqueda.</param>
        /// <param name="sMillaje">Kilometraje de aproximacion al aeropuerto base.</param>
        public VO_AeropuertoAlterno(
            string sSegmento,
            VO_Aeropuerto vo_AeropuertoBase,
            List<VO_Aeropuerto> lvo_AeropuertoesAlternativas,
            string sMillaje)
        {
            this.sSegmento = sSegmento;
            this.vo_AeropuertoBase = vo_AeropuertoBase;
            this.lvo_AeropuertoesAlternativas = lvo_AeropuertoesAlternativas;
            this.sMillaje = sMillaje;
        }

        #endregion

        #region [ PROPIEADES ]
        public string SSegmento
        {
            get { return sSegmento; }
            set { sSegmento = value; }
        }
        public VO_Aeropuerto Vo_AeropuertoBase
        {
            get { return vo_AeropuertoBase; }
            set { vo_AeropuertoBase = value; }
        }
        public List<VO_Aeropuerto> Lvo_AeropuertoesAlternativas
        {
            get { return lvo_AeropuertoesAlternativas; }
            set { lvo_AeropuertoesAlternativas = value; }
        }
        public string SMillaje
        {
            get { return sMillaje; }
            set { sMillaje = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_AeropuertoAlterno() { }
        #endregion
    }
}
