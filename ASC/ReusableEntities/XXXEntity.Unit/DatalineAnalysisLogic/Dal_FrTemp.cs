using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Asc
{
    public class Dal_FrTemp : DatalineAnalysisLogic
    {
        public List<string> STAID = new List<string>();
        public List<string> SOUID = new List<string>();
        public List<string> DATE = new List<string>();
        public List<double> TG = new List<double>();
        public List<string> Q_TG = new List<string>();

        public Dal_FrTemp()
        {

        }

        internal override void CustomizedAnalyze(string Line)
        {
            string[] LineSplits = Line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (LineSplits[2].CompareTo("20050101") >= 0 && LineSplits[2].CompareTo("20190930") <= 0)
            {
                STAID.Add(LineSplits[0]);
                SOUID.Add(LineSplits[1]);
                DATE.Add(LineSplits[2]);
                Double.TryParse(LineSplits[3], out double ParseResult);
                TG.Add(ParseResult);
                Q_TG.Add(LineSplits[4]);
            }    


            //if (dicListFpi.TryGetValue(LineSplits[4].Trim(), out List<double> _ListFpi))
            //{
            //    Double.TryParse(LineSplits[5], out double ParseResult);
            //    _ListFpi.Add(ParseResult);
            //}
            //else
            //{
            //    // Key not found
            //    List<double> _NewListFpi = new List<double>();
            //    dicListFpi.TryAdd(LineSplits[4].Trim(), _NewListFpi);
            //    Double.TryParse(LineSplits[5], out double ParseResult);
            //    _NewListFpi.Add(ParseResult);
            //}

            //if (dicListDate.TryGetValue(LineSplits[4].Trim(), out List<string> _ListDate))
            //{
            //    _ListDate.Add(LineSplits[0]);
            //}
            //else
            //{
            //    // Key not found
            //    List<string> _NewListDate = new List<string>();
            //    dicListDate.TryAdd(LineSplits[4].Trim(), _NewListDate);
            //    _NewListDate.Add(LineSplits[0]);
            //}

            //dicList.Add(LineSplits[0]);

            //Double ParseResult;
            //Double.TryParse(LineSplits[1], out ParseResult);
            //FpiList.Add(ParseResult);
            //Double.TryParse(LineSplits[2], out ParseResult);
            //MpiList.Add(ParseResult);
            //Double.TryParse(LineSplits[3], out ParseResult);
            //DpiList.Add(ParseResult);
            //Double.TryParse(LineSplits[4], out ParseResult);
            //CpiList.Add(ParseResult);
            //Double.TryParse(LineSplits[5], out ParseResult);
            //OpiList.Add(ParseResult);
            //Double.TryParse(LineSplits[6], out ParseResult);
            //SpiList.Add(ParseResult);
        }
    }
}
