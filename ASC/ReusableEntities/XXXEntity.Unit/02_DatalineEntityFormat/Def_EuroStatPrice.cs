using System;
using System.Collections.Generic;

namespace Asc
{
    public class Def_EuroStatPrice : DatalineEntityFormat
    {
        /// <summary>
        /// Csv File Format for CSV File
        /// </summary>
        public Def_EuroStatPrice(char[] _Delimiters) : base(_Delimiters)
        {
            // Specify (FieldName, ValueAddress, ValueType) Here:
            LookUpTable.Add("TIME", (ValueAddress: 0, ValueType: "DateTime"));
            LookUpTable.Add("GEO", (ValueAddress: 1, ValueType: "String"));
            LookUpTable.Add("UNIT", (ValueAddress: 2, ValueType: "String"));
            LookUpTable.Add("INDX", (ValueAddress: 3, ValueType: "String"));
            LookUpTable.Add("COICOP", (ValueAddress: 4, ValueType: "String"));
            LookUpTable.Add("Value", (ValueAddress: 5, ValueType: "Double"));
            LookUpTable.Add("Flag and Footnotes", (ValueAddress: 6, ValueType: "String"));
        }
    }
}
