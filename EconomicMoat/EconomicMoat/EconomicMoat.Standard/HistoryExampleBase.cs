using System;
using System.Collections.Generic;

namespace EconomicMoat.Standard
{
    public class HistoryExampleBase
    {
        public Dictionary<Tuple<string>, string> _base;
        public HistoryExampleBase()
        {
            _base = new Dictionary<Tuple<string>, string>();
            _base.Add(Tuple.Create("1757, July, Europe"), "");
            _base.Add(Tuple.Create("1906, NA, UK"), "");
            _base.Add(Tuple.Create("1911, NA, UK"), "");
            _base.Add(Tuple.Create("1955, NA, UK"), "");
            _base.Add(Tuple.Create("1976, NA, UK"), "");
            _base.Add(Tuple.Create("1990, NA, UK"), "");
            _base.Add(Tuple.Create("1995, NA, UK"), "");
            _base.Add(Tuple.Create("2003, NA, Europe"), "");
            _base.Add(Tuple.Create("2006, NA, Europe"), "");
            _base.Add(Tuple.Create("2007, NA, Europe"), "");
            _base.Add(Tuple.Create("2010, NA, Europe"), "");
            _base.Add(Tuple.Create("2013, NA, UK"), "");
            _base.Add(Tuple.Create("2014, NA, Sweden"), "");
            _base.Add(Tuple.Create("2018, NA, UK"), "");
            _base.Add(Tuple.Create("2018, NA, Europe"), "");
            _base.Add(Tuple.Create("2019, June, Europe"), "");
            _base.Add(Tuple.Create("2019, July, Europe"), "");
        }

        public bool contains(string id)
        {
            return _base.ContainsKey(Tuple.Create(id));
        }

    }
}
