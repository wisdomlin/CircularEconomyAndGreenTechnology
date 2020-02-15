using System;
using System.Collections.Generic;

namespace EconomicMoat.Standard
{
    public class Def_TG : DatalineEntityFormat
    {
        public Def_TG()
        {
            FormatLookUpTable = new Dictionary<string, (int, string)>();

            // Specify (FieldName, ValueAddress, ValueType) Here:
            FormatLookUpTable.Add("STAID", (ValueAddress: 0, ValueType: "UInt16"));
            FormatLookUpTable.Add("SOUID", (ValueAddress: 1, ValueType: "UInt16"));
            FormatLookUpTable.Add("DATE", (ValueAddress: 2, ValueType: "DateTime"));
            FormatLookUpTable.Add("TG", (ValueAddress: 3, ValueType: "Int16"));
            FormatLookUpTable.Add("Q_TG", (ValueAddress: 4, ValueType: "SByte"));
        }
    }
}
