using System;
using System.Collections.Generic;

namespace Asc
{
    public class Def_FaoFpi : DatalineEntityAndFormat
    {
        /// <summary>
        /// Dataline Entity Format for CSV File
        /// </summary>
        public Def_FaoFpi()
        {
            // Specify (FieldName, ValueAddress, ValueType) Here:
            LookUpTable.Add("STAID", (ValueAddress: 0, ValueType: "UInt16"));
            LookUpTable.Add("SOUID", (ValueAddress: 1, ValueType: "UInt16"));
            LookUpTable.Add("DATE", (ValueAddress: 2, ValueType: "DateTime"));
            LookUpTable.Add("TG", (ValueAddress: 3, ValueType: "Int16"));
            LookUpTable.Add("Q_TG", (ValueAddress: 4, ValueType: "SByte"));
        }
    }
}
