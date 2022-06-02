using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Asc
{
    public class Dal_EuroStatPrice : DatalineAnalysisLogic
    {
        // TODO: Unified with QuickMixTank and by List<T> common structure in Qmt 
        public ConcurrentDictionary<string, List<double>> dicListFpi
            = new ConcurrentDictionary<string, List<double>>();

        public ConcurrentDictionary<string, List<string>> dicListDate
            = new ConcurrentDictionary<string, List<string>>();

        
        public Dal_EuroStatPrice(DatalineEntityFormat _Def) : base(_Def)
        {
            
        }

        internal override void CustomizedAnalyze(string Line)
        {
            string[] LineSplits = Line.Split(Def.Delimiters, StringSplitOptions.RemoveEmptyEntries);

            // Address Linking
            int AddrTIME = Def.GetValueAddress("TIME");
            int AddrCOICOP = Def.GetValueAddress("COICOP");
            int AddrValue = Def.GetValueAddress("Value");

            // Value String Accessing
            string sValTIME = LineSplits[AddrTIME].Trim();
            string sValCOICOP = LineSplits[AddrCOICOP].Trim();
            string sValValue = LineSplits[AddrValue].Trim();

            if (dicListFpi.TryGetValue(sValCOICOP, out List<double> _ListFpi))
            {
                // If key found, directly add
                Double.TryParse(sValValue, out double ParseResult);
                _ListFpi.Add(ParseResult);
            }
            else
            {
                // If key not found, create and add
                List<double> _NewListFpi = new List<double>();
                dicListFpi.TryAdd(sValCOICOP, _NewListFpi);
                Double.TryParse(sValValue, out double ParseResult);
                _NewListFpi.Add(ParseResult);
            }

            if (dicListDate.TryGetValue(sValCOICOP, out List<string> _ListDate))
            {
                // If key found, directly add
                _ListDate.Add(sValTIME);
            }
            else
            {
                // If key not found, create and add
                List<string> _NewListDate = new List<string>();
                dicListDate.TryAdd(sValCOICOP, _NewListDate);
                _NewListDate.Add(sValTIME);
            }
        }
    }
}
