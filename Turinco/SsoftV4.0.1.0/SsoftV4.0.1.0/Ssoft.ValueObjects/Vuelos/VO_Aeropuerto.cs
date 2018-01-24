using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    /// <summary>
    /// Descripción breve de VO_Ciudad
    /// </summary>
    public class VO_Aeropuerto
    {
        #region [ ATRIBUTOS ]

        private string sCodigo;
        private string sContexto;//EJ. IATA
        private string sDetalle;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_Aeropuerto()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public VO_Aeropuerto(
           string sCodigo,
           string sContexto)
        {
            this.sCodigo = sCodigo;
            this.sContexto = sContexto;
        }
        #endregion

        #region [ PROPIEADES ]
        public string SCodigo
        {
            get { return sCodigo; }
            set { sCodigo = value; }
        }
        public string SContexto
        {
            get { return sContexto; }
            set { sContexto = value; }
        }
        public string SDetalle
        {
            get { return sDetalle; }
            set { sDetalle = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_Aeropuerto() { }
        #endregion
    }
}
