using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    public class VO_Credentials
    {
        #region [ ATRIBUTOS ]
        /* Credenciales */
        private string sType;
        private string sUser;
        private string sPassword;
        private string sIpcc;
        private string sPcc;
        private string sPccDefault;
        private int iPseudo;
        private int iPseudoDefault;
        private string sPccPais;
        private string sPccDefaultPais;
        private string sLanguage;
        private string sSessionId;
        private string sUrlWebServices;
        private string sUrlWebServicesNew;
        private string sUrlWebServicesRes;
        private string sFrom;
        private string sTo;
        private string sQNumber;
        private string sDominio;
        private string sConversacion;
        private string sMensaje;
        private string sNameEspace;
        private string sSnapCode;
        private string sPseudoPerfil;
        private string sUrlRetorno;
        private string sCodigoTerminal;
        private string sCodigoUnico;

        /* Agencia */
        private string sAgencia_Nombre;
        private string sAgencia_Direccion;
        private string sAgencia_Ciudad;
        private string sAgencia_CodigoPostal;
        private string sAgencia_CodigoEstado;
        private string sAgencia_CodigoPais;
        private string sAgencia_CodigoArea;
        private string sAgencia_CodigoLocalizacion;
        private string sAgencia_Telefono;
        private string sAgencia_TiketeId;
        private string sAgencia_TiketeManual;
        private string sAgencia_TiketTimeLimit;

        /* Especiales */
        private int iSegmentoFuturo;
        private int iTimeLimit;

        /* POL */
        private string sPlantillaVisa;
        private string sPlantillaDiners;
        private string sPlantillaAmex;
        private string sPlantillaMaster;
        private string sPlantillaPse;
        private string sExtra2;
        private string sDireccionEnvio;
        private string sCiudadEnvio;
        private string sPrueba;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_Credentials()
        {
        }

        public VO_Credentials(
            string sUser,
            string sPassword)
        {
            this.sUser = sUser;
            this.sPassword = sPassword;
        }
        #endregion

        #region [ PROPIEADES ]
        public string Type
        {
            get { return sType; }
            set { sType = value; }
        }
        public string User
        {
            get { return sUser; }
            set { sUser = value; }
        }
        public string Password
        {
            get { return sPassword; }
            set { sPassword = value; }
        }
        public string Language
        {
            get { return sLanguage; }
            set { sLanguage = value; }
        }
        public string SessionId
        {
            get { return sSessionId; }
            set { sSessionId = value; }
        }
        public string UrlWebServices
        {
            get { return sUrlWebServices; }
            set { sUrlWebServices = value; }
        }
        public string UrlWebServicesNew
        {
            get { return sUrlWebServicesNew; }
            set { sUrlWebServicesNew = value; }
        }
        public string UrlWebServicesRes
        {
            get { return sUrlWebServicesRes; }
            set { sUrlWebServicesRes = value; }
        }
        public string Ipcc
        {
            get { return sIpcc; }
            set { sIpcc = value; }
        }
        public string Pcc
        {
            get { return sPcc; }
            set { sPcc = value; }
        }
        public string PccDefault
        {
            get { return sPccDefault; }
            set { sPccDefault = value; }
        }
        public int Pseudo
        {
            get { return iPseudo; }
            set { iPseudo = value; }
        }
        public int PseudoDefault
        {
            get { return iPseudoDefault; }
            set { iPseudoDefault = value; }
        }
        public string PccPais
        {
            get { return sPccPais; }
            set { sPccPais = value; }
        }
        public string PccDefaultPais
        {
            get { return sPccDefaultPais; }
            set { sPccDefaultPais = value; }
        }
        public string From
        {
            get { return sFrom; }
            set { sFrom = value; }
        }
        public string To
        {
            get { return sTo; }
            set { sTo = value; }
        }
        public string QNumber
        {
            get { return sQNumber; }
            set { sQNumber = value; }
        }
        public string Dominio
        {
            get { return sDominio; }
            set { sDominio = value; }
        }
        public string Conversacion
        {
            get { return sConversacion; }
            set { sConversacion = value; }
        }
        public string Mensaje
        {
            get { return sMensaje; }
            set { sMensaje = value; }
        }

        public string SnapCode
        {
            get { return sSnapCode; }
            set { sSnapCode = value; }
        }

        public string PseudoPerfil
        {
            get { return sPseudoPerfil; }
            set { sPseudoPerfil = value; }
        }

        public string UrlRetorno
        {
            get { return sUrlRetorno; }
            set { sUrlRetorno = value; }
        }

        public string CodigoTerminal
        {
            get { return sCodigoTerminal; }
            set { sCodigoTerminal = value; }
        }

        public string CodigoUnico
        {
            get { return sCodigoUnico; }
            set { sCodigoUnico = value; }
        }

        public string Agencia_Nombre
        {
            get { return sAgencia_Nombre; }
            set { sAgencia_Nombre = value; }
        }

        public String Agencia_Direccion
        {
            get { return sAgencia_Direccion; }
            set { sAgencia_Direccion = value; }
        }

        public String Agencia_Ciudad
        {
            get { return sAgencia_Ciudad; }
            set { sAgencia_Ciudad = value; }
        }

        public String Agencia_CodigoPostal
        {
            get { return sAgencia_CodigoPostal; }
            set { sAgencia_CodigoPostal = value; }
        }

        public String Agencia_CodigoEstado
        {
            get { return sAgencia_CodigoEstado; }
            set { sAgencia_CodigoEstado = value; }
        }

        public String Agencia_CodigoPais
        {
            get { return sAgencia_CodigoPais; }
            set { sAgencia_CodigoPais = value; }
        }

        public String Agencia_CodigoArea
        {
            get { return sAgencia_CodigoArea; }
            set { sAgencia_CodigoArea = value; }
        }

        public String Agencia_CodigoLocalizacion
        {
            get { return sAgencia_CodigoLocalizacion; }
            set { sAgencia_CodigoLocalizacion = value; }
        }

        public String Agencia_Telefono
        {
            get { return sAgencia_Telefono; }
            set { sAgencia_Telefono = value; }
        }

        public String Agencia_TiketeId
        {
            get { return sAgencia_TiketeId; }
            set { sAgencia_TiketeId = value; }
        }

        public String Agencia_TiketeManual
        {
            get { return sAgencia_TiketeManual; }
            set { sAgencia_TiketeManual = value; }
        }

        public String Agencia_TiketTimeLimit
        {
            get { return sAgencia_TiketTimeLimit; }
            set { sAgencia_TiketTimeLimit = value; }
        }
        public String NameEspace
        {
            get { return sNameEspace; }
            set { sNameEspace = value; }
        }

        public int SegmentoFuturo
        {
            get { return iSegmentoFuturo; }
            set { iSegmentoFuturo = value; }
        }
        public int TimeLimit
        {
            get { return iTimeLimit; }
            set { iTimeLimit = value; }
        }

        private string passwordUser;
        public string PasswordUser
        {
            get { return passwordUser; }
            set { passwordUser = value; }
        }

        private string loginUser;
        public string LoginUser
        {
            get { return loginUser; }
            set { loginUser = value; }
        }

        private int codigoCiudadLogin;
        public int CodigoCiudadLogin
        {
            get
            {
                return codigoCiudadLogin;
            }
            set
            {
                codigoCiudadLogin = value;
            }
        }

        private bool codigoSpecifiedCiudadLogin;
        public bool CodigoSpecifiedCiudadLogin
        {
            get
            {
                return codigoSpecifiedCiudadLogin;
            }
            set
            {
                codigoSpecifiedCiudadLogin = value;
            }
        }

        private string tipoDocumentoUser;
        public string TipoDocumentoUser
        {
            get
            {
                return tipoDocumentoUser;
            }
            set
            {
                tipoDocumentoUser = value;
            }
        }

        public string PlantillaVisa
        {
            get
            {
                return sPlantillaVisa;
            }
            set
            {
                sPlantillaVisa = value;
            }
        }

        public string PlantillaDiners
        {
            get
            {
                return sPlantillaDiners;
            }
            set
            {
                sPlantillaDiners = value;
            }
        }

        public string PlantillaAmex
        {
            get
            {
                return sPlantillaAmex;
            }
            set
            {
                sPlantillaAmex = value;
            }
        }

        public string PlantillaMaster
        {
            get
            {
                return sPlantillaMaster;
            }
            set
            {
                sPlantillaMaster = value;
            }
        }

        public string PlantillaPse
        {
            get
            {
                return sPlantillaPse;
            }
            set
            {
                sPlantillaPse = value;
            }
        }

        public string Extra2
        {
            get
            {
                return sExtra2;
            }
            set
            {
                sExtra2 = value;
            }
        }

        public string DireccionEnvio
        {
            get
            {
                return sDireccionEnvio;
            }
            set
            {
                sDireccionEnvio = value;
            }
        }

        public string CiudadEnvio
        {
            get
            {
                return sCiudadEnvio;
            }
            set
            {
                sCiudadEnvio = value;
            }
        }

        public string Prueba
        {
            get
            {
                return sPrueba;
            }
            set
            {
                sPrueba = value;
            }
        }


        #endregion

        #region [ DESTRUCTOR ]
        ~VO_Credentials() { }
        #endregion
    }
}
