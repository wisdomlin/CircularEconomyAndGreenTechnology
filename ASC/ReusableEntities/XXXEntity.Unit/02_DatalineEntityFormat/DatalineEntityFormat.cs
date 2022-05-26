using System;
using System.Collections.Generic;

namespace Asc
{
    /// <summary>
    /// DatalineEntityFormat specifies: FieldName, ValueAddress, ValueType
    /// Future: should follow ML.NET LoadColumn way..
    /// </summary>
    public class DatalineEntityFormat
    {
        public DatalineEntityFormat(char[] _Delimiters)
        {
            Delimiters = _Delimiters;
            LookUpTable = new Dictionary<string, (int ValueAddress, string ValueType)>();
        }

        internal Dictionary<string, (int ValueAddress, string ValueType)> LookUpTable;
        public char[] Delimiters;

        // TODO: to be delected, and split outside DatalineEntityFormat
        public string[] LineSplits;


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