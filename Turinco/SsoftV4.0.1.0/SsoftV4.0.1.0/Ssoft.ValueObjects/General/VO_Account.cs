using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;
using System.Xml.Serialization;
using System.IO;

namespace Ssoft.ValueObjects
{
    [Serializable]
    public class VO_Account
    {
        #region [ ATRIBUTOS ]
        //TokenRQ
        private string sReward = string.Empty;
        private string sIdentificador = string.Empty;
        private string sToken = string.Empty;

        //TokenRS
        private string sPin = string.Empty;
        private string sImagenTipoTC = string.Empty;
        private string sImagenBanco = string.Empty;
        private string sTipoTC = string.Empty;
        private string sBanco = string.Empty;
        private string sNombre = string.Empty;
        private string sApellido = string.Empty;
        private string sIdioma = string.Empty;
        //private Enum_ProductoVasa eProducto;
        //private Enum_UsuarioVasa eTipoUsuario;
        private string sEmail = string.Empty;
        //private bool bEstado;

        //        private VO_TokenRS vToken;
        private int iContacto;
        private string sEstado = string.Empty;
        private int iCompraPuntos;
        private bool bValidaCompraPuntos = false;
        private int iTotalPuntos;
        private int iPuntosExpirar;
        private string sFechaExpirar = string.Empty;
        private string sMoneda = string.Empty;
        private decimal dValorPuntos;
        private int iAbonaPuntos;
        private int iSaldoPuntos;
        private int iPuntosTransaccion;
        private string sMedioRedencion = string.Empty;
        private float dRedemptionFee;
        private float dCostoPremio;
        private decimal dValorBanco;
        private string sProducto = string.Empty;
        private string sProveedor = string.Empty;
        private float dHandlingcost;
        private int iIdRedencion;
        private string sConfirmation = string.Empty;
        private string sDescriptionError = string.Empty;

        #endregion

        #region [ CONSTRUCTOR ]

        public VO_Account() { }

        #endregion

        #region [ PROPIEADAES ]

        //TokenRQ
        public string Reward
        {
            get { return sReward; }
            set { sReward = value; }
        }

        public string Identificador
        {
            get { return sIdentificador; }
            set { sIdentificador = value; }
        }

        public string Token
        {
            get { return sToken; }
            set { sToken = value; }
        }

        //TokenRS
        public string Pin
        {
            get { return sPin; }
            set { sPin = value; }
        }

        public string Nombre
        {
            get { return sNombre; }
            set { sNombre = value; }
        }

        public string Apellido
        {
            get { return sApellido; }
            set { sApellido = value; }
        }

        public string Idioma
        {
            get { return sIdioma; }
            set { sIdioma = value; }
        }

        //public Enum_UsuarioVasa TipoUsuario
        //{
        //    get { return eTipoUsuario; }
        //    set { eTipoUsuario = value; }
        //}

        public string Email
        {
            get { return sEmail; }
            set { sEmail = value; }
        }

        //public VO_TokenRS Token
        //{
        //    get { return vToken; }
        //    set { vToken = value; }
        //}

        public int Contacto
        {
            get { return iContacto; }
            set { iContacto = value; }
        }

        public int CompraPuntos
        {
            get { return iCompraPuntos; }
            set { iCompraPuntos = value; }
        }

        public bool ValidaCompraPuntos
        {
            get { return bValidaCompraPuntos; }
            set { bValidaCompraPuntos = value; }
        }

        public int TotalPuntos
        {
            get { return iTotalPuntos; }
            set { iTotalPuntos = value; }
        }

        public string Moneda
        {
            get { return sMoneda; }
            set { sMoneda = value; }
        }

        public string Estado
        {
            get { return sEstado; }
            set { sEstado = value; }
        }

        public decimal ValorPuntos
        {
            get { return dValorPuntos; }
            set { dValorPuntos = value; }
        }

        public int AbonaPuntos
        {
            get { return iAbonaPuntos; }
            set { iAbonaPuntos = value; }
        }

        public int SaldoPuntos
        {
            get { return iSaldoPuntos; }
            set { iSaldoPuntos = value; }
        }

        public int PuntosTransaccion
        {
            get { return iPuntosTransaccion; }
            set { iPuntosTransaccion = value; }
        }

        public string MedioRedencion
        {
            get { return sMedioRedencion; }
            set { sMedioRedencion = value; }
        }

        public float RedemptionFee
        {
            get { return dRedemptionFee; }
            set { dRedemptionFee = value; }
        }

        public float CostoPremio
        {
            get { return dCostoPremio; }
            set { dCostoPremio = value; }
        }

        public decimal ValorBanco
        {
            get { return dValorBanco; }
            set { dValorBanco = value; }
        }

        public string Producto
        {
            get { return sProducto; }
            set { sProducto = value; }
        }

        public string Proveedor
        {
            get { return sProveedor; }
            set { sProveedor = value; }
        }

        public float Handlingcost
        {
            get { return dHandlingcost; }
            set { dHandlingcost = value; }
        }

        public int IdRedencion
        {
            get { return iIdRedencion; }
            set { iIdRedencion = value; }
        }

        public string Confirmation
        {
            get { return sConfirmation; }
            set { sConfirmation = value; }
        }

        public int PuntosExpirar
        {
            get { return iPuntosExpirar; }
            set { iPuntosExpirar = value; }
        }

        public string FechaExpirar
        {
            get { return sFechaExpirar; }
            set { sFechaExpirar = value; }
        }

        public string DescriptionError
        {
            get { return sDescriptionError; }
            set { sDescriptionError = value; }
        }

        //Datos Vasa
        public string ImagenTipoTC
        {
            get { return sImagenTipoTC; }
            set { sImagenTipoTC = value; }
        }
        public string ImagenBanco
        {
            get { return sImagenBanco; }
            set { sImagenBanco = value; }
        }
        public string TipoTC
        {
            get { return sTipoTC; }
            set { sTipoTC = value; }
        }
        public string Banco
        {
            get { return sBanco; }
            set { sBanco = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]

        ~VO_Account() { }

        #endregion
    }
    public class clsAccount
    {
        #region [ CONSTRUCTOR ]
        public clsAccount()
        {
        }
        #endregion
                #region [ METODOS ]

        public static void GuardaAccount(VO_Account csAccount)
        {
            string idSession = new clsCacheControl().RecuperarSesionId();
            string FileCache = "vo_Account_" + idSession;
            string strPathXML = clsValidaciones.CacheTempCrea();

            XmlSerializer SerializerRQ = new XmlSerializer(typeof(VO_Account));
            StreamWriter WriterRQ = new StreamWriter(strPathXML + FileCache + ".xml");
            try
            {
                SerializerRQ.Serialize(WriterRQ, csAccount);
                WriterRQ.Flush();
                WriterRQ.Close();
            }
            catch
            {
                WriterRQ.Flush();
                WriterRQ.Close();
            }
        }
        public static void EliminarAccount()
        {
            try
            {
                new csCacheParam().cEliminaXml("vo_Account");
            }
            catch { }
        }
        public VO_Account RecuperarAccount()
        {
            VO_Account vo_Account = new VO_Account();
            try
            {
                string idSession = new clsCacheControl().RecuperarSesionId();
                string FileCache = "vo_Account_" + idSession;
                string strPathXML = clsValidaciones.CacheTempCrea();

                TextReader txtReader = new StreamReader(strPathXML + FileCache + ".xml");

                XmlSerializer SerializerRS = new XmlSerializer(typeof(VO_Account));

                vo_Account = (VO_Account)SerializerRS.Deserialize(txtReader);

                txtReader.Close();
                txtReader.Dispose();
            }
            catch
            { vo_Account = null; }
            return vo_Account;
        }

        #endregion
        #region [ DESTRUCTOR ]

        ~clsAccount() { }

        #endregion
    }
}
