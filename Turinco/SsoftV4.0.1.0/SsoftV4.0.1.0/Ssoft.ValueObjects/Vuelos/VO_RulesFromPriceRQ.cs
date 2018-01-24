using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    public class VO_RulesFromPriceRQ
    {
        #region [ ATRIBUTOS ]

        private List<string> lsRuleCategoryNumber;
        private VO_Pasajero vPassenger;
        private List<VO_SegmentSelect> lvoSegmentSelect;
        private string sFareBasisCode;
        private string sMDRSubset;

        #endregion

        #region [ CONSTRUCTOR ]
        public VO_RulesFromPriceRQ()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public VO_RulesFromPriceRQ(
            List<string> lsRuleCategoryNumber,
            VO_Pasajero vPassenger,
            List<VO_SegmentSelect> lvoSegmentSelect,
            string sFareBasisCode,
            string sMDRSubset)
        {
            this.lsRuleCategoryNumber = lsRuleCategoryNumber;
            this.vPassenger = vPassenger;
            this.lvoSegmentSelect = lvoSegmentSelect;
            this.sFareBasisCode = sFareBasisCode;
            this.sMDRSubset = sMDRSubset;
        }
        #endregion

        #region [ PROPIEADES ]
        public List<string> LsRuleCategoryNumber
        {
            get { return lsRuleCategoryNumber; }
            set { lsRuleCategoryNumber = value; }
        }
        public VO_Pasajero VPassenger
        {
            get { return vPassenger; }
            set { vPassenger = value; }
        }
        public List<VO_SegmentSelect> LvoSegmentSelect
        {
            get { return lvoSegmentSelect; }
            set { lvoSegmentSelect = value; }
        }
        public string SFareBasisCode
        {
            get { return sFareBasisCode; }
            set { sFareBasisCode = value; }
        }
        public string SMDRSubset
        {
            get { return sMDRSubset; }
            set { sMDRSubset = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_RulesFromPriceRQ() { }
        #endregion
    }
}
