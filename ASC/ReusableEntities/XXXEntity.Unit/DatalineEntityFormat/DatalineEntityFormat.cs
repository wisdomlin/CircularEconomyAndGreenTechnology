using System;
using System.Collections.Generic;

namespace Asc
{
    /// <summary>
    /// DatalineEntityFormat specifies: FieldName, ValueAddress, ValueType
    /// Future: should follow ML.NET LoadColumn way..
    /// </summary>
    public class DatalineEntityAndFormat
    {
        internal Dictionary<string, (int ValueAddress, string ValueType)> LookUpTable;
        public string[] LineSplits;

        public DatalineEntityAndFormat()
        {
            LookUpTable = new Dictionary<string, (int, string)>();
        }

        public int GetValueAddress(string FieldName)
        {
            bool hasFound = LookUpTable.TryGetValue(FieldName, out var tuple);
            if (hasFound)
            {
                return tuple.ValueAddress;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public string GetValueType(string FieldName)
        {
            bool hasFound = LookUpTable.TryGetValue(FieldName, out var tuple);
            if (hasFound)
            {
                return tuple.ValueType;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// TODO: to be deleted (after replacing LineSplits with Delimiters)
        /// </summary>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public string GetValueString(string FieldName)
        {
            string ValueString = LineSplits[GetValueAddress(FieldName)];
            return ValueString;
        }
    }
}