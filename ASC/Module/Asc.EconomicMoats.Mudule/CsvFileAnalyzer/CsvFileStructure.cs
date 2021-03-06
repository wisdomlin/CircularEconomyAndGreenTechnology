﻿using System;
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

        /// <summary>
        /// 1 means starting from the first line.
        /// </summary>
        public int HeaderLineStartAt;
        /// <summary>
        /// 2 means starting from the second line.
        /// </summary>
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
