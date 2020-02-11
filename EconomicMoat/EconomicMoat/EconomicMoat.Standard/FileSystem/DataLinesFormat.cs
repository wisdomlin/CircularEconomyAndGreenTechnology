using System;
using System.Collections.Generic;

namespace EconomicMoat.Standard
{
    /// <summary>
    /// 共有幾個欄位
    /// 各欄位叫什麼名字
    /// 各欄位是什麼意義
    /// 各欄位是什麼型態
    /// </summary>
    public class DataLinesFormat
    {
        UInt16 field0;
        UInt16 field1;
        DateTime field2;
        Int16 field3;
        SByte field4;

        Dictionary<string, int> StringIndexLookUpTable;

        public DataLinesFormat()
        {
            StringIndexLookUpTable = new Dictionary<string, int>();
            StringIndexLookUpTable.Add("STAID", 0);
            StringIndexLookUpTable.Add("SOUID", 1);
            StringIndexLookUpTable.Add("DATE", 2);
            StringIndexLookUpTable.Add("TG", 3);
            StringIndexLookUpTable.Add("Q_TG", 4);
        }

        public void TryParseAndAnalysisDataLines(string[] splits)
        {
            bool canConvert = false;
            int i = 0;
            UInt16 field0;
            canConvert = UInt16.TryParse(splits[i++], out field0);
            if (canConvert == true)
            {

            }
            else
                Console.WriteLine("not a valid string");

            UInt16 field1;
            canConvert = UInt16.TryParse(splits[i++], out field1);
            if (canConvert == true)
            {

            }
            else
                Console.WriteLine("not a valid string");

            DateTime field2;
            canConvert = DateTime.TryParseExact(splits[i++], "yyyyMMdd",
                null, System.Globalization.DateTimeStyles.None, out field2);
            if (canConvert == true)
            {

            }
            else
                Console.WriteLine("not a valid string");

            Int16 field3;
            canConvert = Int16.TryParse(splits[i++], out field3);
            if (canConvert == true)
            {
                //int Threshold = AnalysisLogicAsThreshold;
                //if (field3 >= Threshold)
                //    Console.WriteLine("{0}, {1}", field2.ToString("yyyyMMdd"), field3);
            }
            else
                Console.WriteLine("not a valid string");

            SByte field4 = 0;
            canConvert = SByte.TryParse(splits[i++], out field4);
            if (canConvert == true)
            {

            }
            else
                Console.WriteLine("not a valid string");
        }

        internal int GetFieldStringIndex(string sID)
        {
            StringIndexLookUpTable.TryGetValue(sID, out int sIndex);
            return sIndex;
        }

        internal Int16 Parse(string[] splits, string sID)
        {
            bool canConvert = Int16.TryParse(splits[GetFieldStringIndex(sID)], out Int16 field3);
            if (canConvert == true)
            {
                //int Threshold = AnalysisLogicAsThreshold;
                //if (field3 >= Threshold)
                //    Console.WriteLine("{0}, {1}", field2.ToString("yyyyMMdd"), field3);
                return field3;
            }
            else
            {
                Console.WriteLine("not a valid string");
                throw new Exception();
            }
        }
    }
}