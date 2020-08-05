using System;
using System.Collections.Generic;

namespace Asc
{
    public class Def_TG : DatalineEntityAndFormat
    {
        /// <summary>
        /// Dataline Entity Format for TG CSV File
        /// </summary>
        public Def_TG()
        {
            // Specify (FieldName, ValueAddress, ValueType) Here:
            FormatLookUpTable.Add("STAID", (ValueAddress: 0, ValueType: "UInt16"));
            FormatLookUpTable.Add("SOUID", (ValueAddress: 1, ValueType: "UInt16"));
            FormatLookUpTable.Add("DATE", (ValueAddress: 2, ValueType: "DateTime"));
            FormatLookUpTable.Add("TG", (ValueAddress: 3, ValueType: "Int16"));
            FormatLookUpTable.Add("Q_TG", (ValueAddress: 4, ValueType: "SByte"));
        }
    }
}
