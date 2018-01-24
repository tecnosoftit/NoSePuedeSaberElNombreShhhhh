using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;

public class VO_ResultTravelItineraryAddInfo_Pasajeros
{
    #region [ ATRIBUTOS ]

        private string nombre_;
        private string apellido_;
        private string numero_;
        private string tipo_;

    #endregion

    #region [ CONSTRUCTOR ]

        public VO_ResultTravelItineraryAddInfo_Pasajeros(string nombre_, string apellido_, string numero_, string tipo_)
	    {
            this.nombre_ = nombre_;
            this.apellido_ = apellido_;
            this.numero_ = numero_;
            this.tipo_ = tipo_;
        }

    #endregion

    #region [ PROPIEADES ]

        public string Nombre_
        {
            get { return nombre_; }
            set { nombre_ = value; }
        }

        public string Apellido_
        {
            get { return apellido_; }
            set { apellido_ = value; }
        }

        public string Numero_
        {
            get { return numero_; }
            set { numero_ = value; }
        }

        public string Tipo_
        {
            get { return tipo_; }
            set { tipo_ = value; }
        }

    #endregion

    #region [ DESTRUCTOR ]

        ~VO_ResultTravelItineraryAddInfo_Pasajeros() { }

    #endregion
}
