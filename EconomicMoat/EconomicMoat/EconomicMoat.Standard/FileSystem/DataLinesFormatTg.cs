using System;

namespace EconomicMoat.Standard
{
    public class DataLinesFormat
    {
        virtual public void TryParseAndAnalysisDataLines(string[] splits)
        {
            // Do nothing
        }
    }

    public class DataLinesFormatTg : DataLinesFormat
    {

        public int AnalysisLogicAsThreshold; 


        override public void TryParseAndAnalysisDataLines(string[] splits)
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
                int Threshold = AnalysisLogicAsThreshold;
                if (field3 >= Threshold)
                    Console.WriteLine("{0}, {1}", field2.ToString("yyyyMMdd"), field3);
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
    }
}