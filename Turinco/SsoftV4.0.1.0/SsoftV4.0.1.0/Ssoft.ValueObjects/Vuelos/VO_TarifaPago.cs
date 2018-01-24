using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;

namespace Ssoft.ValueObjects
{
    public class VO_TarifaPago
    {
        #region [ ATRIBUTOS ]
        private VO_Pasajero vo_Pasajero;
        private string sTarifa;
        private string sImpuestos;
        private string sTotal;
        private List<VO_Impuesto> lvo_Impuestos;
        private VO_TA vo_TaTotal;
        private string sAerolineaValidadora;
        private Enum_TipoVuelo eTipoVuelo;
        private string sNombre;
        private string sEmail;
        private int iPos;
        #endregion

        #region [ CONSTRUCTOR ]
        public VO_TarifaPago()
        {
        }
        #endregion

        #region [ PROPIEADES ]
        public VO_Pasajero Pasajero
        {
            get { return vo_Pasajero; }
            set { vo_Pasajero = value; }
        }
        public string Tarifa
        {
            get { return sTarifa; }
            set { sTarifa = value; }
        }
        public string Impuestos
        {
            get { return sImpuestos; }
            set { sImpuestos = value; }
        }
        public string Total
        {
            get { return sTotal; }
            set { sTotal = value; }
        }
        public List<VO_Impuesto> LImpuestos
        {
            get { return lvo_Impuestos; }
            set { lvo_Impuestos = value; }
        }
        public string AerolineaValidadora
        {
            get { return sAerolineaValidadora; }
            set { sAerolineaValidadora = value; }
        }
        public VO_TA TaTotal
        {
            get { return vo_TaTotal; }
            set { vo_TaTotal = value; }
        }
        public Enum_TipoVuelo TipoVuelo
        {
            get { return eTipoVuelo; }
            set { eTipoVuelo = value; }
        }
        public string Nombre
        {
            get { return sNombre; }
            set { sNombre = value; }
        }
        public string Email
        {
            get { return sEmail; }
            set { sEmail = value; }
        }
        public int Pos
        {
            get { return iPos; }
            set { iPos = value; }
        }

        #endregion

        #region [ DESTRUCTOR ]
        ~VO_TarifaPago() { }
        #endregion
    }
}
