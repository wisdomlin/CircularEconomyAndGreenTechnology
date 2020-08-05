using System;
using System.Collections.Generic;
using System.Text;

namespace Asc
{
    public class GenericValue
    {
        public string sValueType;
        public string sValueString;

        public GenericValue(string sValueType, string sValueString)
        {
            this.sValueType = sValueType;
            this.sValueString = sValueString;
        }

        internal int CompareTo(GenericValue SV)
        {
            int Res = 0;
            switch (SV.sValueType.ToUpper())
            {
                case "UINT16":
                    {
                        UInt16.TryParse(this.sValueString, out UInt16 PvParseResult);
                        UInt16.TryParse(SV.sValueString, out UInt16 SvParseResult);
                        Res = PvParseResult.CompareTo(SvParseResult);
                        break;
                    }
                case "INT16":
                    {
                        Int16.TryParse(this.sValueString, out Int16 PvParseResult);
                        Int16.TryParse(SV.sValueString, out Int16 SvParseResult);
                        Res = PvParseResult.CompareTo(SvParseResult);
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
