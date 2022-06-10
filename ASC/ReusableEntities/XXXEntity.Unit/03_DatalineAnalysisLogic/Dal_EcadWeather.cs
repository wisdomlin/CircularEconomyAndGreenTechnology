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

            // Address Linking
            int AddrSTAID = Def.GetValueAddress("STAID");
            int AddrSOUID = Def.GetValueAddress("SOUID");
            int AddrDATE = Def.GetValueAddress("DATE");
            int AddrTG = Def.GetValueAddress("TG");
            int AddrQ_TG = Def.GetValueAddress("Q_TG");

            // Value String Accessing
            string sValSTAID = LineSplits[AddrSTAID].Trim();
            string sValSOUID = LineSplits[AddrSOUID].Trim();
            string sValDATE = LineSplits[AddrDATE].Trim();
            string sValTG = LineSplits[AddrTG].Trim();
            string sValQ_TG = LineSplits[AddrQ_TG].Trim();

            if (sValDATE.CompareTo("20050101") >= 0 && sValDATE.CompareTo("20190930") <= 0)
            {
                STAID.Add(sValSTAID);
                SOUID.Add(sValSOUID);
                DATE.Add(sValDATE);
                Double.TryParse(sValTG, out double ParseResult);
                Val.Add(ParseResult);
                Q_Val.Add(sValQ_TG);
            }    
        }
    }
}
