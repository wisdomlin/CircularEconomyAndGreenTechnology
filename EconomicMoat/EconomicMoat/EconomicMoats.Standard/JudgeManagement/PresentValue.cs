using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{
    public class PresentValue
    {
        public string sValueType;
        public string sValueString;

        // All Value Types
        //Int16 value_Int16;
        //UInt16 value_UInt16;

        public PresentValue(string sValueType, string sValueString)
        {
            this.sValueType = sValueType;
            this.sValueString = sValueString;
        }

        internal int CompareTo(SpecValue spec)
        {
            int Res = 0;
            switch (spec.sValueType.ToUpper())
            {
                case "UINT16":
                    {
                        UInt16.TryParse(this.sValueString, out UInt16 DataValueResult);
                        UInt16.TryParse(spec.sValueString, out UInt16 SpecResult);
                        Res = DataValueResult.CompareTo(SpecResult);
                        break;
                    }
                case "INT16":
                    {
                        Int16.TryParse(this.sValueString, out Int16 DataValueResult);
                        Int16.TryParse(spec.sValueString, out Int16 SpecResult);
                        Res = DataValueResult.CompareTo(SpecResult);
                        break;
                    }
                default:
                    {
                        //sInput.GenericTryParse<UInt16>(out UInt16 result);
                        //DataValue = result;
                        break;
                    }
            }
            return Res;
        }
    }
}
