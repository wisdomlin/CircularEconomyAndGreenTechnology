using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Asc
{
    public class Dal_FaoFpi : DatalineAnalysisLogic
    {
        public Dal_FaoFpi(DatalineEntityFormat _Def) : base(_Def)
        {

        }

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
            Double ParseResult;

            int Addr = Def.GetValueAddress("Date");
            DateList.Add(LineSplits[Addr]);

            Addr = Def.GetValueAddress("Food Price Index");
            Double.TryParse(LineSplits[Addr], out ParseResult);
            FpiList.Add(ParseResult);

            Addr = Def.GetValueAddress("Meat");
            Double.TryParse(LineSplits[Addr], out ParseResult);
            MpiList.Add(ParseResult);

            Addr = Def.GetValueAddress("Dairy");
            Double.TryParse(LineSplits[Addr], out ParseResult);
            DpiList.Add(ParseResult);

            Addr = Def.GetValueAddress("Cereals");
            Double.TryParse(LineSplits[Addr], out ParseResult);
            CpiList.Add(ParseResult);

            Addr = Def.GetValueAddress("Oils");
            Double.TryParse(LineSplits[Addr], out ParseResult);
            OpiList.Add(ParseResult);

            Addr = Def.GetValueAddress("Sugar");
            Double.TryParse(LineSplits[Addr], out ParseResult);
            SpiList.Add(ParseResult);
        }
    }
}
