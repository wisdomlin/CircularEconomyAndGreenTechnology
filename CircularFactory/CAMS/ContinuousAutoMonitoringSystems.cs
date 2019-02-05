using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SamplingItemBase;

namespace CAMS
{
    abstract public class ContinuousAutoMonitoringSystems
    {      
        protected ContinuousAutoMonitoringSystems()
        {
            ConstructSamplingItemList();
        }

        private void ConstructSamplingItemList()
        {
            XDocument doc = XDocument.Load(
                AppDomain.CurrentDomain.BaseDirectory + "CAMS_SamplingItemList.xml");
            var query =
                from c in doc.Descendants("CAMS_SamplingItem")
                where (string)c.Attribute("CAMS") == "CAMS_ParticulateMatters"
                select c;

            foreach (XElement e in query)
            {
                SamplingItem item = SamplingItemFactory.CreateSamplingItem((string)
                    e.Attribute("SamplingItem"));
                item.SamplingFreqBySeconds = (int)
                    e.Attribute("SamplingFrequency");
                SamplingItemList.Add(item);
            }
        }

        private List<SamplingItem> _SamplingItemList;
        public List<SamplingItem> SamplingItemList
        {
            get
            {
                if (this._SamplingItemList == null)
                    _SamplingItemList = new List<SamplingItem>();
                return _SamplingItemList;
            }
            set
            {
                _SamplingItemList = value;
            }
        }
    }
}
