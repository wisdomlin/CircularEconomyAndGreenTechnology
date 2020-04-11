using System;
using System.Collections.Generic;

namespace EconomicMoat.Standard
{
    /// <summary>
    /// DatalineEntityFormat specifies: FieldName, ValueAddress, ValueType
    /// </summary>
    public class DatalineEntityFormat
    {
        internal Dictionary<string, (int ValueAddress, string ValueType)> FormatLookUpTable;
        public string[] LineSplits;

        public DatalineEntityFormat()
        {
            FormatLookUpTable = new Dictionary<string, (int, string)>();

            // Specify (FieldName, ValueAddress, ValueType) in Subclass.

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