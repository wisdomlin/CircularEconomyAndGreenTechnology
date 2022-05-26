using System;
using System.Collections.Generic;

namespace Asc
{
    public class Def_TG : DatalineEntityFormat
    {
        /// <summary>
        /// 1 Csv File Format  for CSV File
        /// </summary>
        public Def_TG(char[] _Delimiters) : base(_Delimiters)
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
