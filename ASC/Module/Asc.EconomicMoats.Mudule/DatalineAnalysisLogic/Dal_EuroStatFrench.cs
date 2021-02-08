using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Asc
{
    public class Dal_EuroStatFrench : DatalineAnalysisLogic
    {
        // TODO: Unified with QuickMixTank and by List<T> common structure in Qmt 
        public ConcurrentDictionary<string, List<double>> dicListFpi
            = new ConcurrentDictionary<string, List<double>>();

        public ConcurrentDictionary<string, List<string>> dicListDate
            = new ConcurrentDictionary<string, List<string>>();

        public Dal_EuroStatFrench()
        {

        }

        internal override void CustomizedAnalyze(string Line)
        {
            string[] LineSplits = Line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);

            // LineSplits[4] is "COICOP", like Food, Bread, ...
            if (dicListFpi.TryGetValue(LineSplits[4].Trim(), out List<double> _ListFpi))
            {
                // Key found
                Double.TryParse(LineSplits[5], out double ParseResult);
                _ListFpi.Add(ParseResult);
            }
            else
            {
                // Key not found
                List<double> _NewListFpi = new List<double>();
                dicListFpi.TryAdd(LineSplits[4].Trim(), _NewListFpi);
                Double.TryParse(LineSplits[5], out double ParseResult);
                _NewListFpi.Add(ParseResult);
            }

            if (dicListDate.TryGetValue(LineSplits[4].Trim(), out List<string> _ListDate))
            {
                _ListDate.Add(LineSplits[0]);
            }
            else
            {
                // Key not found
                List<string> _NewListDate = new List<string>();
                dicListDate.TryAdd(LineSplits[4].Trim(), _NewListDate);
                _NewListDate.Add(LineSplits[0]);
            }
        }
    }
}
