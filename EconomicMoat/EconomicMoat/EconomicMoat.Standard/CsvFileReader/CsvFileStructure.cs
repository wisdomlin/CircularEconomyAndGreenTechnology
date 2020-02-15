using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{
    public class CsvFileStructure
    {
        public enum LocationInFile 
        { 
            HeadingLines, 
            HeaderLines, 
            DataLines, 
            FooterLines 
        }

        public int HeaderLineStartAt;
        public int DataLinesStartAt;
        public int FooterLinesCount;

        public CsvFileStructure()
        {
            HeaderLineStartAt = 1;
            DataLinesStartAt = 2;
            FooterLinesCount = 0;
        }
    }
}
