using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Asc
{
    public class Dal_EuroStatFrench : DatalineAnalysisLogic
    {
        public ConcurrentDictionary<string, List<double>> dicListFpi = new ConcurrentDictionary<string, List<double>>();
        //public List<string> DateList = new List<string>();
        public ConcurrentDictionary<string, List<string>> dicListDate = new ConcurrentDictionary<string, List<string>>();

        public Dal_EuroStatFrench()
        {
            // Add dicListFpi
            //dicListFpi.TryAdd("Food", new List<double>());
            //dicListFpi.TryAdd("Bread and cereals", new List<double>());
            //dicListFpi.TryAdd("Bread", new List<double>());
            //dicListFpi.TryAdd("Meat", new List<double>());
            //dicListFpi.TryAdd("Beef and veal", new List<double>());
            //dicListFpi.TryAdd("Pork", new List<double>());
            //dicListFpi.TryAdd("Lamb and goat", new List<double>());
            //dicListFpi.TryAdd("Poultry", new List<double>());
            //dicListFpi.TryAdd("Fish and seafood", new List<double>());
            //dicListFpi.TryAdd("Milk, cheese and eggs", new List<double>());
            //dicListFpi.TryAdd("Fresh whole milk", new List<double>());
            //dicListFpi.TryAdd("Yoghurt", new List<double>());
            //dicListFpi.TryAdd("Cheese and curd", new List<double>());
            //dicListFpi.TryAdd("Eggs", new List<double>());
            //dicListFpi.TryAdd("Oils and fats", new List<double>());
            //dicListFpi.TryAdd("Butter", new List<double>());
            //dicListFpi.TryAdd("Olive oil", new List<double>());
            //dicListFpi.TryAdd("Other edible oils", new List<double>());
            //dicListFpi.TryAdd("Fruit", new List<double>());
            //dicListFpi.TryAdd("Vegetables", new List<double>());
            //dicListFpi.TryAdd("Potatoes", new List<double>());
            //dicListFpi.TryAdd("Sugar", new List<double>());
            //dicListFpi.TryAdd("Coffee, tea and cocoa", new List<double>());
            //dicListFpi.TryAdd("Fruit and vegetables juices", new List<double>());
            //dicListFpi.TryAdd("Wine from grapes", new List<double>());
            //dicListFpi.TryAdd("Beer", new List<double>());

            // Add DateList
            // Assume: No Date Data Lost

            //for (int Year = 2005; Year <= 2019; Year++)
            //{
            //    for (int Month = 1; Month <= 12; Month++)
            //    {
            //        string Date = Year.ToString() + "M" + Month.ToString("00");
            //        DateList.Add(Date);
            //    }
            //}
        }

        internal override void CustomizedAnalyze(string Line)
        {
            string[] LineSplits = Line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (dicListFpi.TryGetValue(LineSplits[4].Trim(), out List<double> _ListFpi))
            {
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
