using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Asc
{
    public class Dal_EcadWeather : DatalineAnalysisLogic
    {
        public List<string> STAID = new List<string>();
        public List<string> SOUID = new List<string>();
        public List<string> DATE = new List<string>();
        public List<double> Val = new List<double>();
        public List<string> Q_Val = new List<string>();

        public Dal_EcadWeather(DatalineEntityFormat _Def) : base(_Def)
        {

        }

        internal override void CustomizedAnalyze(string Line)
        {
            string[] LineSplits = Line.Split(Def.Delimiters, StringSplitOptions.RemoveEmptyEntries);

            //if (LineSplits[2].CompareTo("20050101") >= 0 && LineSplits[2].CompareTo("20191231") <= 0)
            if (LineSplits[2].CompareTo("20050101") >= 0 && LineSplits[2].CompareTo("20190930") <= 0)
            {
                STAID.Add(LineSplits[0]);
                SOUID.Add(LineSplits[1]);
                DATE.Add(LineSplits[2]);
                Double.TryParse(LineSplits[3], out double ParseResult);
                Val.Add(ParseResult);
                Q_Val.Add(LineSplits[4]);
            }    
        }
    }
}
