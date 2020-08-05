using System;
using System.Collections.Generic;
using System.Text;

namespace Asc
{
    /// <summary>
    /// Specify your CSV File Structure by HeaderLineStartAt, DataLinesStartAt, and FooterLinesCount.
    /// </summary>
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

        /// <summary>
        /// Default: HeaderLineStartAt = 1; DataLinesStartAt = 2; FooterLinesCount = 0;
        /// </summary>
        public CsvFileStructure()
        {

        }
    }
}
