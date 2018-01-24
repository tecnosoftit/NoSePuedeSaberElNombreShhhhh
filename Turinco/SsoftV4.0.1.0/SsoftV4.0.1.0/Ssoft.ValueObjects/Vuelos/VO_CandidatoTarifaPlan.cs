using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;

namespace Ssoft.ValueObjects
{
    public class VO_CandidatoTarifaPlan
    {
        private string strCodTarifaPlanNegociado;
        private string strIDTarifaPlanNegociado;
        private string strHotelTarifaPlanNegociado;
        private Enum_RatePlanCandidatesTypeTPA_ExtensionsPromotionalSpotCodeType eTipoPromocion;
        private bool bReprimirTarifa;

        public string StrCodTarifaPlanNegociado
        {
            get { return strCodTarifaPlanNegociado; }
            set { strCodTarifaPlanNegociado = value; }
        }
        public string StrIDTarifaPlanNegociado
        {
            get { return strIDTarifaPlanNegociado; }
            set { strIDTarifaPlanNegociado = value; }
        }
        public string StrHotelTarifaPlanNegociado
        {
            get { return strHotelTarifaPlanNegociado; }
            set { strHotelTarifaPlanNegociado = value; }
        }
        public Enum_RatePlanCandidatesTypeTPA_ExtensionsPromotionalSpotCodeType ETipoPromocion
        {
            get { return eTipoPromocion; }
            set { eTipoPromocion = value; }
        }
        public bool BReprimirTarifa
        {
            get { return bReprimirTarifa; }
            set { bReprimirTarifa = value; }
        }

        public VO_CandidatoTarifaPlan()
        {

        }
    }
}
