using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{
    public class SpecValue
    {
        public string sValueType;
        public string sValueString;

        public SpecValue(string sValueType, string sValueString)
        {
            this.sValueType = sValueType;
            this.sValueString = sValueString;
        }
    }
}
