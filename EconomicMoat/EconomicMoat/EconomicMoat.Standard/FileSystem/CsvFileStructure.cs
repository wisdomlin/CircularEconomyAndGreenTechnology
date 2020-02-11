using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{
    public class CsvFileStructure
    {
        public enum Locations 
        { 
            HeadingLines, 
            HeaderLines, 
            DataLines, 
            FooterLines 
        }

        public int HeaderLineStartAt;
        public int DataLinesStartAt;
        public int FooterLinesStartAt;

        /// <summary>
        /// Constant Value = -1
        /// </summary>
        public int NOT_APPLIED = -1;

        public CsvFileStructure()
        {
            HeaderLineStartAt = 21;
            DataLinesStartAt = 22;
            FooterLinesStartAt = NOT_APPLIED;
        }
    }
}
