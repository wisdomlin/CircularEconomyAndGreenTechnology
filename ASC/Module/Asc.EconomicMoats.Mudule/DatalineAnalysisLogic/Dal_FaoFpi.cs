﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Asc
{
    public class Dal_FaoFpi : DatalineAnalysisLogic
    {
        public List<string> DateList = new List<string>();
        public List<double> FpiList = new List<double>();
        public List<double> MpiList = new List<double>();
        public List<double> DpiList = new List<double>();
        public List<double> CpiList = new List<double>();
        public List<double> OpiList = new List<double>();
        public List<double> SpiList = new List<double>();

        internal override void CustomizedAnalyze(string Line)
        {
            string[] LineSplits = Line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
            DateList.Add(LineSplits[0]);

            Double ParseResult;
            Double.TryParse(LineSplits[1], out ParseResult);
            FpiList.Add(ParseResult);
            Double.TryParse(LineSplits[2], out ParseResult);
            MpiList.Add(ParseResult);
            Double.TryParse(LineSplits[3], out ParseResult);
            DpiList.Add(ParseResult);
            Double.TryParse(LineSplits[4], out ParseResult);
            CpiList.Add(ParseResult);
            Double.TryParse(LineSplits[5], out ParseResult);
            OpiList.Add(ParseResult);
            Double.TryParse(LineSplits[6], out ParseResult);
            SpiList.Add(ParseResult);
        }
    }
}
