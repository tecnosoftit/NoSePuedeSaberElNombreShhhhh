using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;

public class VO_ResultTravelItineraryAddInfo_Itinerario
{
    #region [ ATRIBUTOS ]

		private int id_;
        private string estado_;
        private string origen_;
        private string destino_;
        private string salida_;
        private string llegada_;
        private string numeroVuelo_;
        private string numeroPasajeros_;
        private string aerolinea_;
        private string clase_;
        private string recordAerolinea_; 

	#endregion

    #region [ CONSTRUCTOR ]

        public VO_ResultTravelItineraryAddInfo_Itinerario(int id_, string estado_, string origen_,
                                                     string destino_, string salida_, string llegada_,
                                                     string numeroVuelo_, string numeroPasajeros_,
                                                     string aerolinea_, string clase_, string recordAerolinea_
                                                     )
	    {
            this.id_ = id_;
            this.estado_ = estado_;
            this.origen_ = origen_;
            this.destino_ = destino_;
            this.salida_ = salida_;
            this.llegada_ = llegada_;
            this.numeroVuelo_ = numeroVuelo_;
            this.numeroPasajeros_ = numeroPasajeros_;
            this.aerolinea_ = aerolinea_;
            this.clase_ = clase_;
            this.recordAerolinea_ = recordAerolinea_;
        }

    #endregion

    #region [ PROPIEDADES ]

        public int Id_
        {
            get { return id_; }
            set { id_ = value; }
        }

        public string Estado_
        {
            get { return estado_; }
            set { estado_ = value; }
        }

        public string Origen_
        {
            get { return origen_; }
            set { origen_ = value; }
        }

        public string Destino_
        {
            get { return destino_; }
            set { destino_ = value; }
        }

        public string Salida_
        {
            get { return salida_; }
            set { salida_ = value; }
        }

        public string Llegada_
        {
            get { return llegada_; }
            set { estado_ = value; }
        }

        public string NumeroVuelo_
        {
            get { return numeroVuelo_; }
            set { numeroVuelo_ = value; }
        }

        public string NumeroPasajeros_
        {
            get { return numeroPasajeros_; }
            set { numeroPasajeros_ = value; }
        }

        public string Aerolinea_
        {
            get { return aerolinea_; }
            set { aerolinea_ = value; }
        }

        public string Clase_
        {
            get { return clase_; }
            set { clase_ = value; }
        }

        public string RecordAerolinea_
        {
            get { return recordAerolinea_; }
            set { recordAerolinea_ = value; }
        }

    #endregion

    #region [ DESSTRUCTOR ]

        ~VO_ResultTravelItineraryAddInfo_Itinerario() { }

    #endregion
}
