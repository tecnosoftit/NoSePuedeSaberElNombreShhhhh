using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;

namespace Ssoft.ValueObjects
{
    public class VO_ParametrosTarifas
    {
        #region [ ATRIBUTOS ]
        private VO_FechaGeneral vFechaIni;
        private VO_FechaGeneral vFechaFin;
        private string sDias;
        private string sCategoria;
		 private string sMoneda;
         private string sCodPlan;
         private string sGroupServices;
         private string sCodCiudad;
         private string sCodPais;
         private string sOcupacionWs;
         private List<VO_OcupacionPlanes> lvOcupacion;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_ParametrosTarifas()
        {
        }

        #endregion

        #region [ PROPIEADES ]
        public VO_FechaGeneral FechaIni
        {
            get { return vFechaIni; }
            set { vFechaIni = value; }
        }
        public VO_FechaGeneral FechaFin
        {
            get { return vFechaFin; }
            set { vFechaFin = value; }
        }
        public string Dias
        {
            get { return sDias; }
            set { sDias = value; }
        }
        public string Categoria
        {
            get { return sCategoria; }
            set { sCategoria = value; }
        }
		public string Moneda
        {
            get { return sMoneda; }
            set { sMoneda = value; }
        }
        public string GroupServices
        {
            get { return sGroupServices; }
            set { sGroupServices = value; }
        }
        public string CodCiudad
        {
            get { return sCodCiudad; }
            set { sCodCiudad = value; }
        }
        public string CodPais
        {
            get { return sCodPais; }
            set { sCodPais = value; }
        }
        public string CodPlan
        { 
            get { return sCodPlan; }
            set { sCodPlan = value; }
        }
        public string OcupacionWs
        {
            get { return sOcupacionWs; }
            set { sOcupacionWs = value; }
        }
        public List<VO_OcupacionPlanes> Ocupacion
        {
            get { return lvOcupacion; }
            set { lvOcupacion = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_ParametrosTarifas() { }
        #endregion
    }
    public class VO_FechaGeneral
    {
        #region [ ATRIBUTOS ]
        private string sDay;
        private string sMonth;
        private string sYear;
        private string sDate;
        private string sSeparador;
        private Enum_FormatoFecha eFormato;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_FechaGeneral()
        {
            eFormato = Enum_FormatoFecha.YMD;
            sSeparador = clsValidaciones.SeparadorFecha();
        }

        #endregion

        #region [ PROPIEADES ]
        public string Day
        {
            get { return sDay; }
            set { sDay = value; }
        }
        public string Month
        {
            get { return sMonth; }
            set { sMonth = value; }
        }
        public string Year
        {
            get { return sYear; }
            set { sYear = value; }
        }
        public string Date
        {
            get { return sDate; }
            set { sDate = value; }
        }
        public string Separador
        {
            get { return sSeparador; }
            set { sSeparador = value; }
        }
        public Enum_FormatoFecha Formato
        {
            get { return eFormato; }
            set { eFormato = value; }
        }
        #endregion

        #region [ METODOS ]
        public void FechaGeneral()
        {
            try
            {
                if (sYear != null)
                {
                    if (sYear.Length < 4)
                        sYear = "20" + sYear;
                    if (sMonth.Length < 2)
                        sMonth = "0" + sMonth;
                    if (sDay.Length < 2)
                        sDay = "0" + sDay;

                    switch (eFormato)
                    {
                        case Enum_FormatoFecha.DMY:
                            sDate = sDate + sSeparador + sMonth + sSeparador + sYear;
                            break;

                        case Enum_FormatoFecha.MDY:
                            sDate = sMonth + sSeparador + sDay + sSeparador + sYear;
                            break;

                        case Enum_FormatoFecha.YDM:
                            sDate = sYear + sSeparador + sDay + sSeparador + sMonth;
                            break;

                        case Enum_FormatoFecha.YMD:
                            sDate = sYear + sSeparador + sMonth + sSeparador + sDay;
                            break;

                        case Enum_FormatoFecha.DYM:
                            sDate = sDay + sSeparador + sYear + sSeparador + sMonth;
                            break;

                        case Enum_FormatoFecha.MYD:
                            sDate = sMonth + sSeparador + sYear + sSeparador + sDay;
                            break;
                    }
                }
            }
            catch { }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_FechaGeneral() { }
        #endregion
    }
    public class VO_OcupacionPlanes
    {
        #region [ ATRIBUTOS ]
        private int iAdultos;
        private int iJunior;
        private int iNinos;
        private int iInfantes;
        private int iBebes;
        private int iAdultoMayor;
        private int iAdicionales;
        private List<Int32> liEdades;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_OcupacionPlanes()
        {
        }

        #endregion

        #region [ PROPIEADES ]
        public int Adultos
        {
            get { return iAdultos; }
            set { iAdultos = value; }
        }
        public int Junior
        {
            get { return iJunior; }
            set { iJunior = value; }
        }
        public int Ninos
        {
            get { return iNinos; }
            set { iNinos = value; }
        }
        public int Infantes
        {
            get { return iInfantes; }
            set { iInfantes = value; }
        }
        public int Bebes
        {
            get { return iBebes; }
            set { iBebes = value; }
        }
        public int AdultoMayor
        {
            get { return iAdultoMayor; }
            set { iAdultoMayor = value; }
        }
        public int Adicionales
        {
            get { return iAdicionales; }
            set { iAdicionales = value; }
        }
        public List<Int32> Edades
        {
            get { return liEdades; }
            set { liEdades = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_OcupacionPlanes() { }
        #endregion
    }
}
