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
        internal Dictionary<string, (int ValueAddress, string ValueType)> FormatLookUpTable;
        public string[] LineSplits;

        public DatalineEntityAndFormat()
        {
            FormatLookUpTable = new Dictionary<string, (int, string)>();
        }

        public int GetValueAddress(string FieldName)
        {
            bool hasFound = FormatLookUpTable.TryGetValue(FieldName, out var tuple);
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
            bool hasFound = FormatLookUpTable.TryGetValue(FieldName, out var tuple);
            if (hasFound)
            {
                return tuple.ValueType;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public string GetValueString(string FieldName)
        {
            string ValueString = LineSplits[GetValueAddress(FieldName)];
            return ValueString;
        }
    }
}