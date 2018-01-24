using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.Utils;

namespace Ssoft.Sql
{
    public class field
    {
        private string gName = string.Empty;
        private string gNameTable = string.Empty;
        private string gDescription = string.Empty;
        private string gType = string.Empty;
        private int gLength = 10;
        private bool gNull = true;
        private string gValueDefaul = string.Empty;
        private string gValue = string.Empty;
        private TipoCampo gTypeDat = TipoCampo.String;

        public field()
        {
        }

        public string Name
        {
            get { return gName; }
            set { gName = value; }
        }

        public string NameTable
        {
            get { return gNameTable; }
            set { gNameTable = value; }
        }

        public string Description
        {
            get { return gDescription; }
            set { gDescription = value; }
        }

        public string Type
        {
            get { return gType; }
            set { gType = value; }
        }

        public int Length
        {
            get { return gLength; }
            set { gLength = value; }
        }

        public bool Null
        {
            get { return gNull; }
            set { gNull = value; }
        }

        public string ValueDefaul
        {
            get { return gValueDefaul; }
            set { gValueDefaul = value; }
        }

        public string Value
        {
            get { return gValue; }
            set { gValue = value; }
        }

        public TipoCampo TypeDat
        {
            get { return gTypeDat; }
            set { gTypeDat = value; }
        }
    }
}
