using System;
using System.Collections.Generic;

namespace Asc
{
    public class Def_FaoFpi : DatalineEntityFormat
    {
        /// <summary>
        /// Csv File Format for CSV File
        /// </summary>
        public Def_FaoFpi(char[] _Delimiters) : base(_Delimiters)
        {
            // Specify (FieldName, ValueAddress, ValueType) Here:
            LookUpTable.Add("Date", (ValueAddress: 0, ValueType: "DateTime"));
            LookUpTable.Add("Food Price Index", (ValueAddress: 1, ValueType: "Double"));
            LookUpTable.Add("Meat", (ValueAddress: 2, ValueType: "Double"));
            LookUpTable.Add("Dairy", (ValueAddress: 3, ValueType: "Double"));
            LookUpTable.Add("Cereals", (ValueAddress: 4, ValueType: "Double"));
            LookUpTable.Add("Oils", (ValueAddress: 5, ValueType: "Double"));
            LookUpTable.Add("Sugar", (ValueAddress: 6, ValueType: "Double"));
        }
    }
}
